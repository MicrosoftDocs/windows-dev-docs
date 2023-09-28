---
title: How to author a WinGet Configuration file
description: Learn how to create a WinGet Configuration.
ms.date: 05/23/2023
ms.topic: overview
---

# How to author a WinGet Configuration file

To create a WinGet Configuration file:

1. Create a YAML file following the WinGet Configuration [file naming convention](#file-naming-convention).
2. Familiarize yourself with the [format of a WinGet Configuration file](#file-format) and link the current [file schema](https://aka.ms/configuration-dsc-schema/).
3. Determine the list of [Assertions](#assertions-section) (required preconditions) and [Resources](#resources-section) (the list of required installations and setting configurations to get the machine's development environment to the desired state) to include in the file.
4. Identify the PowerShell modules and Desired State Configuration (DSC) Resources needed to accomplish your desired configuration tasks.
5. Determine the directives and settings needed for each configuration resource.
6. Determine the dependencies for each resource.

## File format

Windows Package Manager uses manifests (YAML files) to locate and install packages for Windows users. WinGet Configuration files use the same YAML style format, adding a JSON schema specification to help define the structure and validation of the file. To further assist in detecting whether the format of your WinGet Configuration file is valid, we recommend using [Visual Studio Code](https://code.visualstudio.com/download) with the [YAML extension](https://marketplace.visualstudio.com/items?itemName=redhat.vscode-yaml) by RedHat to support proper syntax, help detect any formatting errors, provide hover support and auto-completion (when linked to the JSON schema file), and ensure valid formatting.

### File naming convention

The convention for naming a WinGet Configuration file is `configuration.dsc.yaml`. For Git-based projects the default configuration should be stored in a "configurations" directory at: `./configurations/configuration.dsc.yaml`.

### Sections of a WinGet Configuration file

A WinGet Configuration file is separated into two primary sections:

1. **Assertions**: The preconditions required to run the configuration.
2. **Resources**: The list of software and tools to install, the configuration settings for those installs, and the configurations settings for the Windows operating system.

### Assertions section

The list of assertions cover the preconditions (or prerequisites) required for the resources listed in this WinGet Configuration file to succeed on the machine running the file. Assertions can be completed in parallel and do not require any sequential order.

An example assertion:

- **OS version**: A minimum version of the operating system* installed on the machine. As features are added over time to the OS, some are backported to support earlier versions and some are not. It is always helpful to check for a minimum OS version to determine whether a specific tool or feature may be supported that is required for the configuration. For example, WinGet (Windows Package Manager) requires a minimum of Windows 10, version 1809 or newer. Any older versions of Windows do not support WinGet. **It is possible for PowerShell DSC Resources to change the state of the system, but it would not be appropriate to call Windows Update and modify the OS version in the project configuration for an open-source project.*

If an assertion returns “false” to indicate the system is not in the desired state, any [Resource](#resources-section) identifying that assertion as a dependency using the `dependsOn` field will be skipped and fail to run. In this case, even though no configuration changes were applied to the Windows environment, this configuration would be considered a successful outcome.

### Resources section

The list of Resources cover all of the software, tools, packages, etc. that need to be installed and the configurations settings for your Windows operating system or installed applications. Each resource will need to be given a name, description of the directive to be carried out and the PowerShell module that will be responsible for carrying out that directive, and any associated settings or dependencies.

## Example WinGet Configuration file

The following is an example WinGet Configuration `configuration.dsc.yaml` formatted file:

```yml
# yaml-language-server: $schema=https://aka.ms/configuration-dsc-schema/0.2
properties:
  assertions:
    - resource: Microsoft.Windows.Developer/OsVersion
      directives:
        description: Verify min OS version requirement
        allowPrerelease: true
      settings:
        MinVersion: '10.0.22000'
  resources:
    - resource: Microsoft.Windows.Developer/DeveloperMode
      directives:
        description: Enable Developer Mode
        allowPrerelease: true
      settings:
        Ensure: Present
    - resource: Microsoft.WinGet.DSC/WinGetPackage
      id: vsPackage
      directives:
        description: Install Visual Studio 2022 Community
        allowPrerelease: true
      settings:
        id: Microsoft.VisualStudio.2022.Community
        source: winget
    - resource: Microsoft.VisualStudio.DSC/VSComponents
      dependsOn:
        - vsPackage
      directives:
        description: Install required VS workloads from vsconfig file
        allowPrerelease: true
      settings:
        productId: Microsoft.VisualStudio.Product.Community
        channelId: VisualStudio.17.Release
        vsConfigFile: '${WinGetConfigRoot}\..\.vsconfig'
        includeRecommended: true
  configurationVersion: 0.2.0
```

The components of this file consist of:

1. **Schema**: The first line in your configuration file should contain the following comment: `# yaml-language-server: $schema=https://aka.ms/configuration-dsc-schema/<most recent schema version #>` to establish the DSC schema being followed by the file. To find the most recent version of the WinGet Configuration schema, go to [https://aka.ms/configuration-dsc-schema/](https://aka.ms/configuration-dsc-schema/). The most recent schema number at the time of this example is `0.2`, so the schema was entered as: `# yaml-language-server: $schema=https://aka.ms/configuration-dsc-schema/0.2`.

2. **Properties**: The root node for a configuration file is “properties” which must contain a configuration version (`configurationVersion: 0.2.0` in this example). This version should be updated in accordance with updates to the configuration file. The properties node should contain an `assertions` node and a `resources` node

3. **Assertions**: List the preconditions (or prerequisites) required for this configuration in this section.

4. **Resource**: Both the "Assertions" and "Resources" list sections consist of individual `resource` nodes to represent the set up task. The `resource` should be given the name of the PowerShell module followed by the name of the module's DSC resource that will be invoked to apply your desired state: `{ModuleName}/{DscResource}`. Each resource must include **directives** and **settings**. Optionally, it can also include an **id** value. When applying a configuration, WinGet will know to install the module from the [PowerShell Gallery](https://www.powershellgallery.com/packages) and invoke the specified [DSC resource](/powershell/dsc/concepts/resources).

5. **Directives**: The `directives` section provides information about the module and the resource. This section should include a `description` value to describe the configuration task being accomplished by the module. The `allowPrerelease` value enables you to choose whether or not the configuration will be allowed (`true`) to use "Prerelease" modules from the [PowerShell Gallery](https://www.powershellgallery.com/packages).

6. **Settings**: The `settings` value of a resource represents the collection of name-value pairs being passed to the PowerShell DSC Resource. Settings could represent anything from whether Developer Mode is enabled, to applying a reg key, or to establishing a particular network setting.

7. **Dependencies**: The `dependsOn` value of a resource determines whether any other assertion or resource must be complete prior to beginning this task. If the dependency failed, this resource will also automatically fail.

8. **ID**: A unique identifier for the particular resource instance. The id value can be used if another resource has a dependency on this resource being applied first.

## Organizing the Resources section

There are multiple approaches to consider when determining how to organize the Resources section of your WinGet Configuration file. You can organize your list of files by:

- **Execution order**: Organizing your list of resources according to the logical order in which they should be executed. This approach can help the user to understand and follow along with the automation steps being performed once the file is run - what is installed first, second, what setting is updated third, etc.
- **Possibility of failure**: Organizing your list of resources according to the likelihood of a potential failure can help users to catch issues early-on in the configuration process and help them understand why remaining steps may fail, enabling them to identify and make necessary changes before much time is invested.
- **Grouping similar resource types**: Organizing your list of resources by grouping together similar resource types is a common approach in software engineering methodologies and may be the most familiar to you or to other developers utilizing your configuration file.

We recommend including a README.md file with any Open Source published WinGet Configuration file that includes the organizational approach of the file structure.

## Using the variable ${WinGetConfigRoot}
Certain DSC resources may take in a parameter that specifies the path of a file. Instead of specifying the full path, you can use the variable `${WinGetConfigRoot}` to define the working directory where the [`winget configure` command](../winget/configure.md) is being executed and append the relative path to point to that file. This is useful for generalizing a configuration file so that it is machine agnostic. The `Microsoft.VisualStudio.DSC/VSComponents` resource in the example above showcases this functionality by utilizing the `${WinGetConfigRoot}` to point to a .vsconfig file in a project's root directory. This also means that the user should ensure that the target file exists at the relative path based on the current working directory before executing the [`winget configure` command](../winget/configure.md).

## Where to find PowerShell DSC Resource modules

Check out the list of ready-to-use ("inbox") [PowerShell Desired State Configuration Resources](/powershell/dsc/reference/psdscresources/overview#resources) that are supported by Microsoft, including:

- [Environment](/powershell/dsc/reference/psdscresources/resources/environment/environment): Manage an environment variable for a machine or process.
- [MsiPackage](/powershell/dsc/reference/psdscresources/resources/msipackage/msipackage): Install or uninstall an MSI package.
- [Registry](/powershell/dsc/reference/psdscresources/resources/registry/registry): Manage a registry key or value.
- [Script](/powershell/dsc/reference/psdscresources/resources/script/script): Run PowerShell script blocks.
- [Service](/powershell/dsc/reference/psdscresources/resources/service/service): Manage a Windows service.
- [WindowsFeature](/powershell/dsc/reference/psdscresources/resources/windowsfeature/windowsfeature): Install or uninstall a Windows role or feature.
- [WindowsProcess](/powershell/dsc/reference/psdscresources/resources/windowsprocess/windowsprocess): Start or stop a Windows process.

You can also find PowerShell DSC Resource modules in the [PowerShell Gallery](https://www.powershellgallery.com/packages). This gallery hosts hundreds of PowerShell Modules containing Desired State Configuration (DSC) resources submitted by the user community. You can filter search results by applying the “DSC Resource” filter under “Categories”. This repository is **not** verified by Microsoft and contains resources from a variety of authors and publishers. PowerShell modules should always be reviewed for security and credibility before being used as any arbitrary scripting can be included. See [How to check the trustworthiness of a WinGet Configuration file](check.md) for more tips on creating a trustworthy WinGet Configuration file.
