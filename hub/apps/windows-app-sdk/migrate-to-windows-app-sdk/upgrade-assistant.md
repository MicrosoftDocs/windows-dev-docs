---
title: Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant
description: The [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) is a command-line tool that can assist with migrating a C# UWP app to a [Windows UI Library (WinUI) 3](../../winui/index.md) app that uses the Windows App SDK.
ms.topic: article
ms.date: 09/12/2022
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, .NET Upgrade Assistant, Upgrade, Assistant, UWP, 
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant

The [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) is a command-line tool that can assist with migrating a C# Universal Windows Platform (UWP) app to a [Windows UI Library (WinUI) 3](../../winui/index.md) app that uses the Windows App SDK.

Also see the [Upgrade Assistant](https://github.com/dotnet/upgrade-assistant) GitHub repository. Command-line options for running the tool are documented there.

## Install the .NET Upgrade Assistant

For info about installing the tool, see [Overview of the .NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview).

## Summary

When you use the .NET Upgrade Assistant to migrate your UWP app, here are the high-level steps and stages in the migration process that the tool carries out.

* Makes a backup (a copy) of your project in a new folder.
* Migrates your project in-place, in the same folders and files, without renaming folders.
* Upgrades your project to the latest SDK format, and cleans up NuGet package references.
* Targets .NET 6, and the Windows App SDK.
* Upgrades from WinUI 2 to WinUI 3.
* Adds new template files such as `App.Xaml`, `MainWindow.Xaml`, and publishing profiles.
* Update namespaces, and adds **MainPage** navigation.
* Attempts to detect and fix APIs that are different between UWP and the Windows App SDK, and uses **Task List** TODOs to mark APIs that are no longer supported.

As it runs, the tool also aims to provide migration guidance in the form of warning messages within the tool's output, and **Task List** TODOs in the form of comments within your project's source code (for example, for cases where completely automated migration of your UWP source code isn't possible). A typical **Task List** TODO includes a link to a topic in this migration documentation. As the developer, you're always in control of the migration process.

> [!TIP]
> To see all of the TODOs that the tool has generated, look in the **Task list** in Visual Studio.

> [!NOTE]
> After the tool has finished running, there are some follow-up steps you can choose to do if needed. You can move your code from `App.xaml.old.cs` to `App.xaml.cs`; and you can restore `AssemblyInfo.cs` from the backup that the tool creates.

## What the tool supports

This release of the .NET Upgrade Assistant is currently in preview, and is receiving frequent updates. The tool currently supports only the C# programming language; not C++. And in most cases with this release, your project will require additional effort from you to complete the migration.

The tool aims to migrate your project and code so that it compiles. But some features require you to investigate and fix them (via **Task List** TODOs). For more information about what to consider before migrating, see [What's supported when migrating from UWP to WinUI 3](./what-is-supported.md).

Because of the following limitations of the current release of the .NET Upgrade Assistant, you might choose to wait for a future release before migrating your app:

* Migrating from [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview) APIs isn't supported.
* Migrating from [**AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow.trycreateasync)-related APIs isn't supported.

Where possible, the tool tries to generate a warning; and it intentionally causes your code not to compile until you change it.

* Custom views aren't supported. For example, you won't receive a warning or a fix for a custom dialog that extends [**MessageDialog**](/uwp/api/windows.ui.popups.messagedialog), and calls an API incorrectly.
* Windows Runtime Components aren't supported.
- Multi-window apps might not be migrated correctly.
- A project that follows a non-standard file structure (such as `App.xaml` and `App.xaml.cs` not being in the root folder) might not be migrated correctly.

The [Upgrade Assistant GitHub repository](https://github.com/dotnet/upgrade-assistant) documents troubleshooting tips and known issues. If you find any issues while using the tool, please report them in that same GitHub repository, tagging them with an area tag of `UWP`. We appreciate it!

> [!NOTE]
> For guidance about the migration process&mdash;and the differences between UWP and Windows App SDK features and APIs&mdash;see [Migrate from UWP to the Windows App SDK](./migrate-to-windows-app-sdk-ovw.md).

> [!TIP]
> You can see what version of the tool you have by issuing the command `upgrade-assistant --version`.

## Test drive the tool with the UWP PhotoLab sample

Let's take the .NET Upgrade Assistant for a test-drive.

As source material, we'll be migrating the UWP [PhotoLab sample](https://github.com/microsoft/Windows-appsample-photo-lab.git) application. PhotoLab is a sample app for viewing and editing image files. It demonstrates XAML layout, data binding, and UI customization features.

Begin by cloning or downloading the PhotoLab sample source code from the link above.

Be aware that after we've used the tool to automate the migration of the app, additional manual effort will be needed to complete the migration.

> [!NOTE]
> You can see a case study of the PhotoLab sample being fully migrated manually in [A Windows App SDK migration of the UWP PhotoLab sample app](./case-study-1.md).

### The analysis stage

> [!NOTE]
> In the current release of the .NET Upgrade Assistant, the `analyze` command is still in development, and it isn't yet working as intended.

The `analyze` command performs a simplified dry run of migrating your app. This stage might provide insights as to what changes you might need to make to your project before going ahead with the migration proper.

At a command prompt, navigate to the folder where the `.sln` file of the PhotoLab sample is. As shown below, to perform the analyis stage, you issue the command `upgrade-assistant analyze`, and pass in the name of the project or solution you want to analyze. In this test drive, we want to analyze `PhotoLab.sln`.

```console
> upgrade-assistant analyze PhotoLab.sln

[17:36:32 INF] Loaded 8 extensions
[17:36:34 INF] Using MSBuild from C:\Program Files\dotnet\sdk\6.0.400\
[17:36:34 INF] Using Visual Studio install from D:\Program Files\Microsoft Visual Studio\2022\Enterprise [v17]
[17:36:39 INF] Writing output to D:\Windows-appsample-photo-lab-master\AnalysisReport.sarif
[17:36:40 INF] Skip minimum dependency check because Windows App SDK cannot work with targets lower than already recommended TFM.
[17:36:40 INF] Recommending Windows TFM net6.0-windows because the project either has Windows-specific dependencies or builds to a WinExe
[17:36:40 INF] Marking package Microsoft.NETCore.UniversalWindowsPlatform for removal based on package mapping configuration UWP
[17:36:40 INF] Adding package Microsoft.WindowsAppSDK based on package mapping configuration UWP
[17:36:40 INF] Adding package Microsoft.Graphics.Win2D based on package mapping configuration UWP
[17:36:40 INF] Marking package Microsoft.UI.Xaml for removal based on package mapping configuration UWP
[17:36:41 WRN] No version of Microsoft.Toolkit.Uwp.UI.Animations found that supports ["net6.0-windows"]; leaving unchanged
[17:36:41 INF] Package Microsoft.UI.Xaml, Version=2.4.2 does not support the target(s) net6.0-windows but a newer version (2.8.1) does.
[17:36:41 INF] Package Microsoft.WindowsAppSDK, Version=1.1.0 does not support the target(s) net6.0-windows but a newer version (1.1.4) does.
[17:36:41 INF] Reference to .NET Upgrade Assistant analyzer package (Microsoft.DotNet.UpgradeAssistant.Extensions.Default.Analyzers, version 0.4.336902) needs to be added
[17:36:42 INF] Adding Microsoft.Windows.Compatibility 6.0.0 helps with speeding up the upgrade process for Windows-based APIs
[17:36:44 WRN] Unable to find a supported WinUI nuget package for Microsoft.Toolkit.Uwp.UI.Animations. Skipping this package.
[17:36:45 INF] Running analyzers on PhotoLab
[17:36:54 INF] Identified 0 diagnostics in project PhotoLab
[17:36:54 INF] Winforms Updater not applicable to the project(s) selected
[17:36:54 INF] Analysis Complete, the report is available at D:\Windows-appsample-photo-lab-master\AnalysisReport.sarif
```

There's quite a bit of internal diagnostic information in the output, but some information is helpful. Notice that the analysis indicates that the migration will recommend that the project target the `net6.0-windows` target framework moniker (TFM) (see [Target frameworks in SDK-style projects](/dotnet/standard/frameworks)). A console application would probably get the recommendation to upgrade to TFM `net6.0` directly, unless it used some Windows-specific libraries.

For PhotoLab, the output indicates that no changes need to be made to the project before migrating.

> [!TIP]
> When you're analyzing your own UWP projects, if any errors or warnings are reported, then we recommend that you take care of them before you move on to the next stage.

### The migration stage

As shown below, to perform the migration stage, you issue the command `upgrade-assistant upgrade`, and pass in the name of the project or solution you want to migrate. In this test drive, we want to migrate `PhotoLab.sln`.

So, still at a command prompt, and still navigated to the folder where the `.sln` file of the PhotoLab sample is, issue this command:

```console
upgrade-assistant upgrade PhotoLab.sln
```

> [!TIP]
> For this test drive, the solution contains just one project. Alternatively, you can pass the name of a project to the tool, instead of the name of a solution. However, if you pass the name of a solution that contains multiple projects, then the tool will ask you to indicate which project is the startup project (the tool calls it the *upgrade entrypoint*). Based on that project, the tool creates a dependency graph, and it suggests an order in which to upgrade the projects.

The .NET Upgrade Assistant runs and prints out the migration steps it will perform. Take this opportunity to read over the list of steps to get an idea for what's involved in the migration process.

After the list of steps, the tool prints out a menu of commands for you to choose from. You can apply or skip the next step (for example, the first step is to back up your UWP project). Or you can get more information about the next step, adjust logging settings, or stop the upgrade and quit.

You can enter a number, and press <kbd>Enter</kbd>. Or just press <kbd>Enter</kbd> to select the first command in the menu.

As each step begins, the tool might provide information about what will likely happen if you apply the step.

#### Back up project

In this step, either accept the default path (just press <kbd>Enter</kbd>), or enter a custom path.

After the step, press <kbd>Enter</kbd> again to continue.

When the tool moves on to the next step, it prints out the same set of steps again, with indications of which steps are complete, and which steps are yet to take place.

After the list of steps, the tool again prints out the menu of commands for you to choose from.

> [!TIP]
> If you want to leave the tool to run unattended without needing to repeatedly interact with it, then you can run the tool in non-interactive mode. To do that, provide the `--non-interactive` command-line option. However, when you run `upgrade-assistant` in (the default) interactive mode, you have control over the changes/upgrades performed on your projects. Whereas using `upgrade-assistant` with `--non-interactive` can leave your project in a broken state. We advise you to use the option at your own discretion. All command-line options for running the tool are documented on the [Upgrade Assistant](https://github.com/dotnet/upgrade-assistant) GitHub repository.

#### Convert project file to SDK style

In this step, the project is upgraded from the .NET Framework project format to the .NET SDK project format. Here's some typical output from this step.

```console
[17:39:52 INF] Applying upgrade step Convert project file to SDK style
[17:39:52 INF] Converting project file format with try-convert, version 0.4.336902+3799b6849a9457619660a355ca9111c050b0ef79
[17:39:53 INF] Skip minimum dependency check because Windows App SDK cannot work with targets lower than already recommended TFM.
[17:39:53 INF] Recommending Windows TFM net6.0-windows because the project either has Windows-specific dependencies or builds to a WinExe
[17:39:55 INF] Converting project D:\Windows-appsample-photo-lab-master\PhotoLab\PhotoLab.csproj to SDK style
[17:39:55 INF] Project file converted successfully! The project may require additional changes to build successfully against the new .NET target.
[17:40:00 INF] Upgrade step Convert project file to SDK style applied successfully
```

> [!TIP]
> When you're migrating your own UWP projects, we recommend that you pay attention to the output of each step. If any errors or warnings are reported, then we recommend that you take care of them before you move on to the next step.

#### Clean up NuGet package references

In this step (and its potentially many sub-steps), the tool cleans up NuGet package references.

In addition to the packages referenced by your app, the `packages.config` file contains references to the dependencies of those packages. For example, if you added reference to package **A**, which depends on package **B**, then both packages would be referenced in the `packages.config` file. In the new project system, only the reference to package **A** is required. So this step analyzes the package references, and removes those that aren't required.

Your app is still referencing .NET Framework assemblies. Some of those assemblies might be available as NuGet packages. So this step analyzes those assemblies, and references the appropriate NuGet package.

> [!TIP]
> Again, when you're migrating your own UWP projects, pay attention to the output to see if there's any action for you.

#### Update TFM

The tool next changes the target framework moniker (TFM) (see [Target frameworks in SDK-style projects](/dotnet/standard/frameworks)) from .NET Framework to the suggested SDK. In this example, it's `net6.0-windows`.

```console
[17:44:52 INF] Initializing upgrade step Update TFM
[17:44:53 INF] Skip minimum dependency check because Windows App SDK cannot work with targets lower than already recommended TFM.
[17:44:53 INF] Recommending Windows TFM net6.0-windows10.0.19041 because the project either has Windows-specific dependencies or builds to a WinExe
```

#### Update NuGet Packages

Next, the tool updates the project's NuGet packages to the versions that support the updated TFM, `net6.0-windows`.

```console
[17:44:53 INF] Initializing upgrade step Update NuGet Packages
[17:44:53 INF] Initializing upgrade step Duplicate reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Package map reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Target compatibility reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Upgrade assistant reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Windows Compatibility Pack Analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step MyDotAnalyzer reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Newtonsoft.Json reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Windows App SDK package analysis
[17:44:53 INF] No package updates needed
[17:44:53 INF] Initializing upgrade step Transitive reference analyzer
[17:44:53 INF] No package updates needed
[17:44:53 INF] Applying upgrade step Update NuGet Packages
[17:44:53 INF] Upgrade step Update NuGet Packages applied successfully
```

#### Add template files

> [!TIP]
> When you're analyzing your own UWP projects, the tool might automatically skip the next few steps if it determines that there isn't anything to do for the particular project.

This step involves update any template , config, and code files. In this example, the tool automatically adds necessary publish profiles, `App.xaml.cs`, `MainWindow.xaml.cs`, `MainWindow.xaml`, and others.

```console
[17:44:53 INF] Initializing upgrade step Add template files
[17:44:54 INF] 9 expected template items needed
[17:49:44 INF] Applying upgrade step Add template files
[17:49:44 INF] Added template file app.manifest
[17:49:44 INF] Added template file Properties\launchSettings.json
[17:49:44 INF] Added template file Properties\PublishProfiles\win10-arm64.pubxml
[17:49:44 INF] Added template file Properties\PublishProfiles\win10-x64.pubxml
[17:49:44 INF] Added template file Properties\PublishProfiles\win10-x86.pubxml
[17:49:44 INF] File already exists, moving App.xaml.cs to App.xaml.old.cs
[17:49:44 INF] Added template file App.xaml.cs
[17:49:44 INF] Added template file MainWindow.xaml.cs
[17:49:44 INF] Added template file MainWindow.xaml
[17:49:44 INF] Added template file UWPToWinAppSDKUpgradeHelpers.cs
[17:49:44 INF] 9 template items added
[17:49:44 INF] Upgrade step Add template files applied successfully
```

#### Update Windows Desktop Project (UWP-specific changes)

In this step the tool updates the UWP project to the new Windows Desktop project.

> [!IMPORTANT]
> You can choose to skip the sub-step for back button insertion if that's best for your project. Inserting back button functionality might cause your UI to behave differently. If that happens, then remove the **StackPanel** that's inserted as a parent of the back button, and reposition the back button where it seems best.

```console
[17:56:53 INF] Applying upgrade step Update WinUI namespaces
[17:56:53 INF] Upgrade step Update WinUI namespaces applied successfully

[17:58:45 INF] Applying upgrade step Update WinUI Project Properties
[17:58:46 INF] Upgrade step Update WinUI Project Properties applied successfully

[17:59:11 INF] Applying upgrade step Update package.appxmanifest
[17:59:11 INF] Upgrade step Update package.appxmanifest applied successfully

[17:59:11 INF] Applying upgrade step Update package.appxmanifest
[17:59:11 INF] Upgrade step Update package.appxmanifest applied successfully

[17:59:37 INF] Applying upgrade step Remove unnecessary files
[17:59:37 INF] Deleting .\source\repos\Windows-appsample-photo-lab\PhotoLab\Properties\AssemblyInfo.cs as it is not required for Windows App SDK projects.
[17:59:37 INF] Upgrade step Remove unnecessary files applied successfully

[18:00:22 INF] Applying upgrade step Update animations xaml
[18:00:22 INF] Upgrade step Update animations xaml applied successfully

[18:00:42 INF] Applying upgrade step Insert back button in XAML
[18:00:42 INF] Upgrade step Insert back button in XAML applied successfully
[18:00:42 INF] Applying upgrade step Update Windows Desktop Project
[18:00:42 INF] Upgrade step Update Windows Desktop Project applied successfully
```

#### Update source code

In this important step, the tool will try to migrate your UWP source code to WinUI 3, performing source-specific code changes.

Code migration for the **PhotoLab** sample app includes:

* Changes to Content Dialog and File Save picker APIs.
* XAML update for the Animations package.
* Showing warning messages, and adding **Task List** TODOs in `DetailPage.xaml`, `DetailPage.xaml.cs`, and `MainPage.xaml.cs` for custom back button.
* Implementing the back button functionality and adding a **Task List** TODO to customize XAML button.
* A link to documentation is provided that you can use to learn more about back button implementation.

Here's the consolidated output from the next few substeps:

```console
[18:05:45 INF] Applying upgrade step Apply fix for UA307: Custom back button implementation is needed
[18:05:45 WRN] D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\MainPage.xaml.cs
            TODO UA307 Default back button in the title bar does not exist in WinUI3 apps.
            The tool has generated a custom back button in the MainWindow.xaml.cs file.
            Feel free to edit its position, behavior and use the custom back button instead.
            Read: https://learn.microsoft.com/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/case-study-1#restoring-back-button-functionality
[18:05:45 INF] Diagnostic UA307 fixed in D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\MainPage.xaml.cs
[18:05:45 WRN] D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\DetailPage.xaml.cs
            TODO UA307 Default back button in the title bar does not exist in WinUI3 apps.
            The tool has generated a custom back button in the MainWindow.xaml.cs file.
            Feel free to edit its position, behavior and use the custom back button instead.
            Read: https://learn.microsoft.com/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/case-study-1#restoring-back-button-functionality
[18:05:45 INF] Diagnostic UA307 fixed in D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\DetailPage.xaml.cs
[18:05:45 INF] Running analyzers on PhotoLab
[18:05:48 INF] Identified 4 diagnostics in project PhotoLab
[18:05:48 WRN] D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\DetailPage.xaml.cs
            TODO UA307 Default back button in the title bar does not exist in WinUI3 apps.
            The tool has generated a custom back button in the MainWindow.xaml.cs file.
            Feel free to edit its position, behavior and use the custom back button instead.
            Read: https://learn.microsoft.com/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/case-study-1#restoring-back-button-functionality
[18:05:48 INF] Diagnostic UA307 fixed in D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\DetailPage.xaml.cs
[18:05:48 INF] Running analyzers on PhotoLab
[18:05:51 INF] Identified 3 diagnostics in project PhotoLab
[18:05:51 INF] Upgrade step Apply fix for UA307: Custom back button implementation is needed applied successfully

[18:06:06 INF] Applying upgrade step Apply fix for UA309: ContentDialog API needs to set XamlRoot
[18:06:06 INF] Diagnostic UA309 fixed in D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\DetailPage.xaml.cs
[18:06:06 INF] Diagnostic UA309 fixed in D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\MainPage.xaml.cs
[18:06:06 INF] Running analyzers on PhotoLab
[18:06:09 INF] Identified 1 diagnostics in project PhotoLab
[18:06:09 INF] Upgrade step Apply fix for UA309: ContentDialog API needs to set XamlRoot applied successfully

[18:06:27 INF] Applying upgrade step Apply fix for UA310: Classes that implement IInitializeWithWindow need to be initialized with Window Handle
[18:06:27 INF] Diagnostic UA310 fixed in D:\VisualStudioProjects\Windows-appsample-photo-lab-master\PhotoLab\DetailPage.xaml.cs
[18:06:27 INF] Running analyzers on PhotoLab
[18:06:31 INF] Identified 0 diagnostics in project PhotoLab
[18:06:31 INF] Applying upgrade step Update source code
[18:06:31 INF] Upgrade step Update source code applied successfully
[18:06:31 INF] Upgrade step Apply fix for UA310: Classes that implement IInitializeWithWindow need to be initialized with Window Handle applied successfully
```

#### Move to next project, and Finalize upgrade

For PhotoLab, there are no more projects to migrate. But when you're analyzing your own UWP projects, that might not always be the case (in which case the tool would now let you select which project to upgrade next). Since there are no more projects to upgrade, the tool takes you to the **Finalize upgrade** step:

```console
[18:07:00 INF] Applying upgrade step Finalize upgrade
[18:07:00 INF] Upgrade step Finalize upgrade applied successfully

[18:07:04 INF] Upgrade has completed. Please review any changes.
[18:07:04 INF] Deleting upgrade progress file at D:\Windows-appsample-photo-lab-master\.upgrade-assistant
```

At this point, after most of the migration from UWP app to WinUI 3 app has been done, the resulting `.csproj` file looks like this (with some of the build configuration property groups removed for brevity):

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
    <Platform Condition=" '$(Platform)' == '' ">x86</Platform>
    <OutputType>WinExe</OutputType>
    <DefaultLanguage>en-US</DefaultLanguage>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    <RuntimeIdentifiers>win10-x86;win10-x64;win10-arm64</RuntimeIdentifiers>
    <UseWinUI>true</UseWinUI>
    <ApplicationManifest>app.manifest</ApplicationManifest>
    <EnableMsixTooling>true</EnableMsixTooling>
    <Platforms>x86;x64;arm64</Platforms>
    <PublishProfile>win10-$(Platform).pubxml</PublishProfile>
  </PropertyGroup>
  <ItemGroup>
    <AppxManifest Include="Package.appxmanifest">
      <SubType>Designer</SubType>
    </AppxManifest>
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.1.0" />
    <PackageReference Include="Microsoft.Graphics.Win2D" Version="1.0.0.30" />
    <PackageReference Include="Microsoft.DotNet.UpgradeAssistant.Extensions.Default.Analyzers" Version="0.4.346201">
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Windows.Compatibility" Version="6.0.0" />
    <PackageReference Include="CommunityToolkit.WinUI.UI.Animations" Version="7.1.2" />
  </ItemGroup>
  <ItemGroup>
    <Compile Remove="App.xaml.old.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="App.xaml.old.cs" />
  </ItemGroup>
</Project>
```

As you can, the project is now referencing the Windows App SDK, WinUI 3, and .NET 6. Now that **PhotoLab** has been migrated, you can take advantage of all of the new features that WinUI 3 apps have to offer, and grow your app with the platform.

Also, the .NET Upgrade Assistant adds analyzers to the project that assist with continuing with the upgrade process. For example, the **Microsoft.DotNet.UpgradeAssistant.Extensions.Default.Analyzers** NuGet package.

## Follow-up manual migration

At this point you can open the migrated **PhotoLab** solution or project, and see the changes that have been made in the source code. The project does build and run, but it needs a little more work to finish hooking things up before the WinUI 3 version looks and behaves like the UWP version.

See the **Task List** in Visual Studio (**View** > **Task List**) for TODOs that you should action to manually complete the migration.

It's possible that the UWP (.NET Framework) version of your app contained library references that your project isn't actually using. You'll need to analyze each reference, and determine whether or not it's required. The tool might also have added or upgraded a NuGet package reference to the wrong version.

## Troubleshooting tips

There are several known problems that can occur when using the .NET Upgrade Assistant. In some cases, these problems are with the [try-convert tool](https://github.com/dotnet/try-convert) that the .NET Upgrade Assistant uses internally.

But for more troubleshooting tips and known issues, see the [Upgrade Assistant](https://github.com/dotnet/upgrade-assistant) GitHub repository.