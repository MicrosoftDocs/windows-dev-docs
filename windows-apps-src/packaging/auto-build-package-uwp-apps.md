---
title: Set up automated builds for your UWP app
description: How to configure your automate builds to produce sideload and/or Store packages.
ms.date: 09/30/2018
ms.topic: article
keywords: windows 10, uwp
ms.assetid: f9b0d6bd-af12-4237-bc66-0c218859d2fd
ms.localizationpriority: medium
---
# Set up automated builds for your UWP app

You can use Azure DevOps Pipelines to create automated builds for UWP projects.
In this article, we’ll look at different ways to do that.  We’ll also show you how to perform these tasks by using the command line so that you can integrate with any other build system.

## Create a new pipeline in Azure Dev Ops

You will need to [Signup for Azure Pipelines](https://docs.microsoft.com/azure/devops/pipelines/get-started/pipelines-sign-up).

You can configure pipelines from different source control system as described in the [Get Started guide](https://docs.microsoft.com/azure/devops/pipelines/get-started-yaml).

## Set up an automated build

We’ll start with the default UWP build definition that’s available in Azure Dev Ops and then show you how to configure the pipeline.

In the list of build definition templates, choose the *Universal Windows Platform* template.

![Select the UWP template](images/select-yaml-template.png)

This template includes the basic configuration to build your UWP project:

```yaml
trigger:
- master

pool:
  vmImage: 'VS2017-Win2016'

variables:
  solution: '**/*.sln'
  buildPlatform: 'x86|x64|ARM'
  buildConfiguration: 'Release'
  appxPackageDir: '$(build.artifactStagingDirectory)\AppxPackages\\'

steps:
- task: NuGetToolInstaller@0

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: VSBuild@1
  inputs:
    platform: 'x86'
    solution: '$(solution)'
    configuration: '$(buildConfiguration)'
    msbuildArgs: '/p:AppxBundlePlatforms="$(buildPlatform)" /p:AppxPackageDir="$(appxPackageDir)" /p:AppxBundle=Always /p:UapAppxPackageBuildMode=StoreUpload'

```

The default template tries to sign the package with the certificate specified in the `.csproj` file. If you want to sign your package during the build you must have access to the private key, otherwise you can disable signing by adding the parameter `/p:AppxPackageSigningEnabled=false` to the `msbuildArgs` section in the yaml file.

**Add the certificate of your project to a source code repository**

Pipelines works with both Azure Repos Git and TFVC based code repositories.
If you use a Git repository, add the certificate file of your project to the repository so that the build agent can sign the app package. If you don’t do this, the Git repository will ignore the certificate file.
To add the certificate file to your repository, right-click the certificate file in Solution Explorer, and then in the shortcut menu, choose the Add Ignored File to Source Control command.

![how to include a certificate](images/building-screen1.png)

We’ll discuss [advanced certificate management](#certificates-best-practices) later in this guide.

#### Configure the Build solution build task

This task compiles any solution that’s in the working folder to binaries and produces the output app package file.
This task uses MSbuild arguments.  You’ll have to specify the value of those arguments. Use the following table as a guide.

|**MSBuild Argument**|**Value**|**Description**|
|--------------------|---------|---------------|
|AppxPackageDir|$(Build.ArtifactStagingDirectory)\AppxPackages|Defines the folder to store the generated artifacts.|
|AppxBundlePlatforms|$(Build.BuildPlatform)|Allows you to define the platforms to include in the bundle.|
|AppxBundle|Always|Creates an appxbundle with the appx files for the platform specified.|
|UapAppxPackageBuildMode|StoreUpload|Generates the `.appxupload` file and the _Test folder for sideloading.. |
|UapAppxPackageBuildMode|CI|Generates the appxupload file only|
|UapAppxPackageBuildMode|SideloadOnly|Generates the _Test folder for sideloading only|

If you want to build your solution by using the command line, or by using any other build system, run msbuild with these arguments.

```ps
/p:AppxPackageDir="$(Build.ArtifactStagingDirectory)\AppxPackages\\"
/p:UapAppxPackageBuildMode=StoreUpload
/p:AppxBundlePlatforms="$(Build.BuildPlatform)"
/p:AppxBundle=Always
```

The parameters defined with the $() syntax are variables defined in the build definition, and will change in other build systems.

![default variables](images/building-screen5.png)

To view all predefined variables, see [Use build variables.](https://docs.microsoft.com/azure/devops/pipelines/build/variables)

#### Configure the Publish Artifact build task

The default UWP pipeline does not save the generated artifacts, to add the publish capabilities to your yaml definition add the next tasks:

```yaml
- task: CopyFiles@2
  displayName: 'Copy Files to: $(build.artifactstagingdirectory)'
  inputs:
    SourceFolder: '$(system.defaultworkingdirectory)'
    Contents: '**\bin\$(BuildConfiguration)\**'
    TargetFolder: '$(build.artifactstagingdirectory)'

- task: PublishBuildArtifacts@1
  displayName: 'Publish Artifact: drop'
  inputs:
    PathtoPublish: '$(build.artifactstagingdirectory)'
```

You can see the generated artifacts in the `Artifacts` option of the build results page.

![artifacts](images/building-screen6.png)

Because we’ve set the `UapAppxPackageBuildMode` property to `StoreUpload`, the artifacts folder includes the package for submission to the Store (.appxupload). Note that you can also submit a regular app pacakge (.appx/.msix) or an app bundle (.appxbundle/.msixbundle) to the Store. For the purposes of this article, we'll use the .appxupload file.



#### Address errors that appear when you bundle more than one app in the same solution

If you add more than one UWP project to your solution, and then try to create a bundle, you might receive an error like this one:

```ps
MakeAppx(0,0): Error : Error info: error 80080204: The package with file name "AppOne.UnitTests_0.1.2595.0_x86.appx" and package full name "8ef641d1-4557-4e33-957f-6895b122f1e6_0.1.2595.0_x86__scrj5wvaadcy6" is not valid in the bundle because it has a different package family name than other packages in the bundle
```

This error appears because at the solution level, it’s not clear which app should appear in the bundle.
To resolve this issue, open each project file and add the following properties at the end of the first `<PropertyGroup>` element:

|**Project**|**Properties**|
|-------|----------|
|App|`<AppxBundle>Always</AppxBundle>`|
|UnitTests|`<AppxBundle>Never</AppxBundle>`|

Then, remove the `AppxBundle` msbuild argument from the build step.

## Related Topics

- [Build your .NET app for Windows](https://www.visualstudio.com/docs/build/get-started/dot-net)
- [Packaging UWP apps](https://msdn.microsoft.com/windows/uwp/packaging/packaging-uwp-apps)
- [Sideload LOB apps in Windows 10](https://technet.microsoft.com/itpro/windows/deploy/sideload-apps-in-windows-10)
- [Create a certificate for package signing](https://docs.microsoft.com/windows/uwp/packaging/create-certificate-package-signing)
