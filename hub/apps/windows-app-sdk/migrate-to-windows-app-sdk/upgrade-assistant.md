---
title: Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant
description: The [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) is a command-line tool that can assist with migrating a C# UWP app to a [WinUI 3](../../winui/index.md) app that uses the Windows App SDK.
ms.topic: article
ms.date: 10/05/2023
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, .NET Upgrade Assistant, Upgrade, Assistant, UWP, 
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant

The .NET Upgrade Assistant (see [Overview of the .NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview)) is a Visual Studio extension (recommended), and a command-line tool, that can assist with migrating a C# Universal Windows Platform (UWP) app to a [WinUI 3](/windows/apps/winui/) app that uses the Windows App SDK.

Our roadmap for UWP support in the .NET Upgrade Assistant includes further tooling improvements, and adding migration support for new features. If you find issues related to the .NET Upgrade Assistant, then you can file them within Visual Studio by selecting **Help** > **Send Feedback** > **Report a Problem**.

Also see the [Upgrade Assistant](https://github.com/dotnet/upgrade-assistant) GitHub repository. Options for running the tool on the command-line are documented there.

## Install the .NET Upgrade Assistant

You can install the .NET Upgrade Assistant as a Visual Studio extension or as a .NET command-line tool. For more info, see [Install the .NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-install).

## Summary

When you use the .NET Upgrade Assistant to migrate your UWP app, here are the high-level steps and stages in the migration process that the tool carries out.

* Optionally copies your project, and migrates the copy; leaving your original project unchanged.
* Optionally migrates your project in-place (in the same folders and files, without renaming folders); and doesn't make a copy.
* Upgrades your project from the .NET Framework project format to the latest .NET SDK project format.
* Cleans up NuGet package references. In addition to the packages referenced by your app, the `packages.config` file contains references to the dependencies of those packages. For example, if you added reference to package **A**, which depends on package **B**, then both packages would be referenced in the `packages.config` file. In the new project system, only the reference to package **A** is required. So this step analyzes the package references, and removes those that aren't required. Your app is still referencing .NET Framework assemblies. Some of those assemblies might be available as NuGet packages. So this step analyzes those assemblies, and references the appropriate NuGet package.
* Targets .NET 6, and the Windows App SDK.
* Changes the target framework moniker (TFM) (see [Target frameworks in SDK-style projects](/dotnet/standard/frameworks)) from .NET Framework to the suggested SDK. For example, `net6.0-windows`.
* Migrates your UWP source code from WinUI 2 to WinUI 3, performing source-specific code changes.
* Adds/updates any template, config, and code files. For example, adding necessary publishing profiles, `App.xaml.cs`, `MainWindow.xaml.cs`, `MainWindow.xaml`, and others.
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

> [!NOTE]
> You can see a case study of the PhotoLab sample being fully manually migrated in [A Windows App SDK migration of the UWP PhotoLab sample app](./case-study-1.md).

1. Begin by cloning or downloading the PhotoLab sample source code from the link above.

> [!TIP]
> Be aware that after we've used the tool to automate the migration of the app, additional manual effort will be needed to complete the migration.

1. Open the PhotoLab solution in Visual Studio.

1. Having installed the .NET Upgrade Assistant extension (see [Install the .NET Upgrade Assistant](#install-the-net-upgrade-assistant) earlier in this topic), right-click on the project in **Solution Explorer**, and click **Upgrade**.

1. Choose the **Upgrade project to a newer .NET version** option.

1. Choose the **In-place project upgrade** option.

1. Choose a target framework.

1. Click **Upgrade selection**.

1. The .NET Upgrade Assistant runs, and uses the Visual Studio **Output** window to print out info and status as it goes.

You can monitor the progress bar until the upgrade operation completes.

Code migration for the **PhotoLab** sample app includes:

* Changes to Content Dialog and File Save picker APIs.
* XAML update for the Animations package.
* Showing warning messages, and adding **Task List** TODOs in `DetailPage.xaml`, `DetailPage.xaml.cs`, and `MainPage.xaml.cs` for custom back button.
* Implementing the back button functionality and adding a **Task List** TODO to customize XAML button.
* A link to documentation is provided that you can use to learn more about back button implementation.

The version numbers in your resulting `.csproj` will be slightly different, but essentially it will look like this (with some of the build configuration property groups removed for brevity):

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

As you can see, the project is now referencing the Windows App SDK, WinUI 3, and .NET 6. Now that **PhotoLab** has been migrated, you can take advantage of all of the new features that WinUI 3 apps have to offer, and grow your app with the platform.

Also, the .NET Upgrade Assistant adds analyzers to the project that assist with continuing with the upgrade process. For example, the **Microsoft.DotNet.UpgradeAssistant.Extensions.Default.Analyzers** NuGet package.

## Follow-up manual migration

At this point you can open the migrated **PhotoLab** solution or project, and see the changes that have been made in the source code. The project needs a little more work to finish hooking things up before the WinUI 3 version builds, runs, and behaves like the UWP version.

See the **Task List** in Visual Studio (**View** > **Task List**) for TODOs that you should action to manually complete the migration.

It's possible that the UWP (.NET Framework) version of your app contained library references that your project isn't actually using. You'll need to analyze each reference, and determine whether or not it's required. The tool might also have added or upgraded a NuGet package reference to the wrong version.

The Upgrade Assistant doesn't edit the `Package.appxmanifest`, which will need some edits in order for the app to launch:

1. Add this namespace on the root \<Package\> element:

```
xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
```

2. Edit the \<Application\> element from `EntryPoint="appnamehere.App"` to `EntryPoint="$targetentrypoint$"`

4. Replace any specified `Capability` with this:

```xml
<rescap:Capability Name="runFullTrust" />
```

In your `.csproj` file, you might need to edit the project file to set `<OutputType>WinExe</OutputType>` and `<UseMaui>False</UseMaui>`.

To use many of the XAML controls, ensure that your `app.xaml` file includes the `<XamlControlsResources>`, such as in this example:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
            <!-- Other merged dictionaries here -->
        </ResourceDictionary.MergedDictionaries>
        <!-- Other app resources here -->
    </ResourceDictionary>
</Application.Resources>
```

## Troubleshooting tips

There are several known problems that can occur when using the .NET Upgrade Assistant. In some cases, these problems are with the [try-convert tool](https://github.com/dotnet/try-convert) that the .NET Upgrade Assistant uses internally.

But for more troubleshooting tips and known issues, see the [Upgrade Assistant](https://github.com/dotnet/upgrade-assistant) GitHub repository.
