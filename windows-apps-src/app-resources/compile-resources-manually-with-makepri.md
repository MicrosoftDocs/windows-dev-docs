---

Description: MakePri.exe is a command line tool that you can use to create and dump PRI files. It is integrated as part of MSBuild within Microsoft Visual Studio, but it could be useful to you for creating packages manually or with a custom build system.
title: Compile resources manually with MakePri.exe
template: detail.hbs
ms.date: 10/23/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---

# Compile resources manually with MakePri.exe

MakePri.exe is a command line tool that you can use to create and dump PRI files. It is integrated as part of MSBuild within Microsoft Visual Studio, but it could be useful to you for creating packages manually or with a custom build system.

> [!NOTE]
> MakePri.exe is installed when you check the **Windows SDK for UWP Managed Apps** option while installing the Windows Software Development Kit. It is installed to the path `%WindowsSdkDir%bin\<WindowsTargetPlatformVersion>\x64\makepri.exe` (as well as in folders named for the other architectures). For example, `C:\Program Files (x86)\Windows Kits\10\bin\10.0.17713.0\x64\makepri.exe`.

## In this section
|Topic|Description|
|-|-|
| [MakePri.exe command-line options](makepri-exe-command-options.md) | MakePri.exe has the set of commands `createconfig`, `dump`, `new`, `resourcepack`, and `versioned`. This topic details the command-line options for their use. |
| [MakePri.exe configuration file](makepri-exe-configuration.md) | This topic describes the schema of the MakePri.exe XML configuration file. |
| [MakePri.exe format-specific indexers](makepri-exe-format-specific-indexers.md) | This topic describes the format-specific indexers used by the MakePri.exe tool to generate its index of resources. |

## MakePri.exe command-line options

MakePri.exe has the set of commands `createconfig`, `dump`, `new`, `resourcepack`, and `versioned`. For details of their use, see [MakePri.exe command-line options](makepri-exe-command-options.md).

## MakePri.exe configuration

The PRI XML configuration file dictates how and what resources are indexed. The schema of the configuration XML is described in [MakePri.exe configuration](makepri-exe-configuration.md).

## Format-specific indexers

MakePri.exe is typically used with the `new`, `versioned`, and `resourcepack` options. In those cases it indexes source files to generate an index of resources. MakePri.exe uses various individual indexers to read different source resource files or containers for resources. The simplest indexer is the folder indexer, which indexes the contents of a folder for resources such as `.jpg` or `.png` images. For more info, see [MakePri.exe format-specific indexers](makepri-exe-format-specific-indexers.md).

## MakePri.exe warnings and error messages

### Resources found for language(s) '<language(s)>' but no resources found for default language(s): '<language(s)>'. Change the default language or qualify resources with the default language.

This warning is displayed when MakePri.exe or MSBuild discovers files or string resources for a given named resource that appear to be marked with language qualifiers, but no candidate is found for a default language. The process for using qualifiers in file and folder names is described in [Tailor your resources for language, scale, and other qualifiers](tailor-resources-lang-scale-contrast.md). A file or folder may have a language name in it, but no resources are discovered that are qualified for the exact default language. For example, if a project uses "en-US" as the default language and has a file named "de/logo.png", but does not have any files that are marked with the default language "en-US", this warning will appear. In order to remove this warning, either file(s) or string resource(s) should be qualified with the default language, or the default language should be changed. To change the default language, with your solution open in Visual Studio, open `Package.appxmanifest`. On the Application tab, confirm that the Default language is set appropriately (for example, "en" or "en-US").

### No default or neutral resource given for '<resource identifier>'. The application may throw an exception for certain user configurations when retrieving the resources.

This warning is displayed when MakePri.exe or MSBuild discovers files or resources that appear to be marked with language qualifiers for which the resources are unclear. There are qualifiers, but there is no guarantee that a particular resource candidate can be returned for that resource identifier at run time. If no resource candidate for a particular language, homeregion, or other qualifier can be found that is a default or will always match the context of a user, this warning will be displayed. At run time, for particular user configurations such as a user's language preferences or home location (**Settings** > **Time & Language** > **Region & language**), the APIs used to retrieve the resource may throw an unexpected exception. In order to remove this warning, default resources should be provided, such as a resource in the project's default language or global home region (homeregion-001).

## Using MakePri.exe in a build system

Build systems should use the MakePri.exe `new`, `versioned`, or `resourcepack` command, depending on the type of project being built. Build systems that create a fresh PRI file should use the `new` command. Build systems that must ensure compatibility of internal offsets through iterations can use the `versioned` command. Build systems that must create a PRI file that contains additional variants of resources, with validation to ensure that no new resources are added for that variant, should use the `resourcepack` command.

Build systems that require explicit control over source files that get indexed can use the ResFiles indexer instead of indexing a folder. Build systems can also use multiple index passes with different [format-specific indexers](makepri-exe-format-specific-indexers.md) to generate a single PRI file.

Build systems can also use the PRI format-specific indexer to add pre-built PRI files into the PRI for the package from other components, such as class libraries, assemblies, SDKs, and DLLs.

When PRI files are built for other components, class libraries, assemblies, DLLs, and SDKs, the **initialPath** configuration should be used to ensure component resources have their own sub resource maps that don't conflict with the app they're included in.

## Related topics
* [MakePri.exe command-line options](makepri-exe-command-options.md)
* [MakePri.exe configuration](makepri-exe-configuration.md)
* [MakePri.exe format-specific indexers](makepri-exe-format-specific-indexers.md)
* [Tailor your resources for language, scale, and other qualifiers](tailor-resources-lang-scale-contrast.md)
