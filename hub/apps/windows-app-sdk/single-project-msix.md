---
title: Package your app using single-project MSIX
description: This article provides instructions for how to generate a MSIX desktop app via a single project in Visual Studio.
ms.topic: article
ms.date: 10/05/2021
keywords: windows, win32, desktop development, Windows App SDK, msix, packaging project, single project, single project msix, winui 3
ms.localizationpriority: medium
---

# Package your app using single-project MSIX

Single-project MSIX is a feature that enables you to build MSIX-packaged desktop apps with the Windows App SDK without using a separate packaging project. This feature is currently available as a standalone Visual Studio extension that you can use for these scenarios:

- Create new desktop projects using the **Blank App, Packaged (WinUI 3 in Desktop)** templates in the Windows App SDK. These projects are configured to build your app into an MSIX package without the use of a separate packaging project.
- Modify existing desktop WinUI 3 projects created using the Windows App SDK so that the separate packaging project is no longer needed.

![Comparing packaging project to single project](images/single-project-overview.png)

## Overview

This section introduces some important details about the single-project MSIX feature.

### Benefits

Before the introduction of the single-project MSIX feature, you needed two projects in your solution to build an MSIX-packaged desktop app: your app project and a separate [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net). The single-project MSIX feature enables you to develop and build your app using just your app project. This provides a cleaner project structure and more straightforward development experience. For example, you no longer need to select the separate packaging project as your startup project.

### Supported project types

The current release of the single-project MSIX feature supports [WinUI 3 in Desktop projects](../winui/winui3/winui-project-templates-in-visual-studio.md#project-templates-for-winui-3) (C# and C++) in the Windows App SDK.

### Limitations

Single-project MSIX only supports a single executable in the generated MSIX package. If you need to combine multiple executables into a single MSIX package, you must continue using a Windows Application Packaging Project in your solution.

## Install the single-project MSIX packaging tools

The installation process for the single-project MSIX packaging tools depends on the version of the Windows App SDK that you're using.

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you’ve already installed one of these previews, see [how to resolve the issue](preview-channel.md#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](preview-channel.md#version-10-preview-3-100-preview3) instead. 

- **Windows App SDK 1.0 Preview 2 and later:** The single-project MSIX packaging tools are included with the Windows App SDK extension for Visual Studio. For these releases, the tools include project templates you can use to create new MSIX-packaged WinUI 3 apps. For installation instructions for the SDK, see [Install the Windows App SDK extension for Visual Studio](set-up-your-development-environment.md?tabs=preview#4-install-the-windows-app-sdk-extension-for-visual-studio).

    > [!NOTE]
    > If you install the C# version of the Windows App SDK 1.0 Preview 3 extension for Visual Studio 2019, you must also install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) separately. The **Blank App, Packaged (WinUI 3 in Desktop)** project template has a known issue that results in a build error unless you also install the single-project packaging tools extension. This issue does not affect other versions of the Windows App SDK 1.0 Preview 3 extension.

- **Windows App SDK 1.0 Preview 1 and earlier:** You must install the single-project MSIX packaging tools extension separately. For these releases, the tools do not include project templates; however, you can still use the tools to modify an existing desktop WinUI 3 project so that the separate packaging project is no longer needed.

  To install the single-project MSIX packaging tools extension:

  - **Visual Studio 2019:** Install the [Single-project MSIX Packaging Tools for Visual Studio 2019 VSIX extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools). The extension requires Visual Studio 2019 version 16.10.x or later.

  - **Visual Studio 2022:** Install the [Single-project MSIX Packaging Tools for Visual Studio 2022 VSIX extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingToolsDev17).

## Create a new project

If you're using Windows App SDK 1.0 Preview 2 or later, you can create a new WinUI 3-based project that includes single-project MSIX support by using the **Blank App, Packaged (WinUI 3 in Desktop)** template. For more information, see [Create your first WinUI 3 app](../winui/winui3/create-your-first-winui3-app.md#instructions-for-winui-3-packaged-desktop-apps).

## Modify an existing project

Follow these steps to modify an existing desktop WinUI 3 project created using the Windows App SDK so that the package manifest and other support needed to build an MSIX package are in the application project, not the packaging project.

### Step 1: Create or open an existing packaging project

If you already have a solution for a [WinUI 3 in Desktop](../winui/winui3/winui-project-templates-in-visual-studio.md#project-templates-for-winui-3) app that includes a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net), open that solution in Visual Studio now.

Otherwise, create a new WinUI 3 in Desktop project in Visual Studio by following the instructions in [Create your first WinUI 3 app](../winui/winui3/create-your-first-winui3-app.md).

![A project using the packaging project](images/single-project-packaging-project.png)

### Step 2: Edit the application project settings

Next, edit some configuration settings to use the single-project MSIX feature. There are different instructions depending on your project type and Visual Studio version.

#### [C#](#tab/csharp)

1. In **Solution Explorer**, double-click the project node for your application to open the **.csproj** file in the XML editor. Add the following XML to the main **\<PropertyGroup\>** element.

    ```xml
    <EnablePreviewMsixTooling>true</EnablePreviewMsixTooling>
    <PublishProfile>Properties\PublishProfiles\win10-$(Platform).pubxml</PublishProfile>
    ```

    When you are done, the **\<PropertyGroup\>** element should look similar to this.

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net5.0-windows10.0.19041.0</TargetFramework>
        ...
        <UseWinUI>true</UseWinUI>
        <EnablePreviewMsixTooling>true</EnablePreviewMsixTooling>
        <PublishProfile>Properties\PublishProfiles\win10-$(Platform).pubxml</PublishProfile>
      </PropertyGroup>
    ```

2. Save your changes and close the **.csproj** file.

3. Perform one of the following procedures, depending on your version of Visual Studio.

    **For Visual Studio 2019**:

    1. In **Solution Explorer**, right-click the project node for your application and select **Properties**.

    2. Select the **Debug** tab and set the **Launch** property to **MsixPackage**. You might have to select this twice if it reverts back on the first attempt.

        ![Enabling MsixProject option](images/single-project-msixpackageoption.png)

    3. Save your changes.

    **For Visual Studio 2022**:

    1. In **Solution Explorer**, right-click the **Properties** folder under the project node for your application and select **Add** > **New item**.

    2. Select **Text file**, name the new file **launchSettings.json**, and click **Add**. Make sure the new file is in the **Properties** folder of your application project.

    3. Copy the following settings to the new file. You are free to change the values as needed for your scenario. The **MyApp** value can be any string; it does not need to match the name of your application.

        ```json
        {
            "profiles": {
                "MyApp": {
                    "commandName": "MsixPackage",
                    "commandLineArgs": "", /* Command line arguments to pass to the app. */
                    "alwaysReinstallApp": false, /* Uninstall and then reinstall the app. All information about the app state is deleted. */
                    "remoteDebugEnabled": false, /* Indicates that the debugger should attach to a process on a remote machine. */
                    "allowLocalNetworkLoopbackProperty": true, /* Allow the app to make network calls to the device it is installed on. */
                    "authenticationMode": "Windows", /* The authentication scheme to use when connecting to the remote machine. */
                    "doNotLaunchApp": false, /* Do not launch the app, but debug my code when it starts. */
                    "remoteDebugMachine": "", /* The name of the remote machine. */
                    "nativeDebugging": false /* Enable debugging for managed and native code together, also known as mixed-mode debugging. */
                }
            }
        }
        ```

    4. Save and close the **launchSettings.json** file.

#### [C++](#tab/cpp)

1. In **Solution Explorer**, right-click the project node for your application and select **Unload Project**.

2. Right-click the project node again and select **Edit *project-name*.vcxproj** to open the **.vcxproj** file in the XML editor.

3. Make the following changes to the XML in the **.vcxproj** file:

    1. Add `<EnablePreviewMsixTooling>true</EnablePreviewMsixTooling>` to the main `<PropertyGroup>` element.
    2. Change the value of `<AppxPackage>` to `true`.
    3. Change the value of the `<AppContainerApplication>` element to `true`.

    When you're done, the contents of the **.vcxproj** file should look similar to this.

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <Project ToolsVersion="15.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
      <PropertyGroup Label="Globals">
        ...
        <MinimumVisualStudioVersion>16.0</MinimumVisualStudioVersion>
        <AppContainerApplication>true</AppContainerApplication>
        <AppxPackage>true</AppxPackage>
        <ApplicationType>Windows Store</ApplicationType>
        <ApplicationTypeRevision>10.0</ApplicationTypeRevision>
        <WindowsTargetPlatformVersion>10.0</WindowsTargetPlatformVersion>
        <WindowsTargetPlatformMinVersion>10.0.17763.0</WindowsTargetPlatformMinVersion>
        <UseWinUI>true</UseWinUI>
        <EnablePreviewMsixTooling>true</EnablePreviewMsixTooling>
      </PropertyGroup>
    ```

4. Save and close the **.vcxproj** file.

5. In **Solution Explorer**, right-click the project node and select **Reload Project**.

---

### Step 3: Move files to the application project

Next, move several important files to the application project. There are different instructions depending on your project type and Visual Studio version.

#### [C#](#tab/csharp)

1. In **File Explorer**, move the **Package.appxmanifest** file and the **Images** folder from your packaging project to your application project. Place this file and folder in the top level of the application project's folder hierarchy.
2. Remove the packaging project from your solution.

![Illustration of moving files to main app](images/single-project-move-to-one.png)

#### [C++](#tab/cpp)

1. In **File Explorer**, move the **Package.appxmanifest** file and the **Images** folder from your packaging project to your application project. Place this file and folder in the top level of the application project's folder hierarchy.
2. In Visual Studio, select the application project in **Solution Explorer** and click **Show all files**.
3. In **Solution Explorer**, select the **Package.appxmanifest** file and all files in the **Images** folder. Right-click the selected files and and select **Include in Project**.
4. Remove the packaging project from your solution.

---

### Step 4: Enable deploying in Configuration Manager

1. Select **Build** -> **Configuration Manager**.
2. In **Configuration Manager**, click the **Deploy** check box for every combination of configuration and platform (for example, **Debug** and **x86**, **Debug** and **arm64**, **Release** and **x64**, and more).
    > [!NOTE]
    > Be sure to use the **Active solution configuration** and **Active solution platform** drop-downs at the top instead of the **Configuration** and **Platform** drop-downs in the same row as the **Deploy** check box.

![Enabling Deploy in Configuration Manager](images/single-project-configmanager.png)

### Step 5: Deploy your app

Build and deploy your application project. Visual Studio will build your application into an MSIX package, install the package, and then run your application.

## Provide feedback

To send us your feedback, report problems, or ask questions about the single-project MSIX feature, post a discussion or issue on the [Windows App SDK GitHub repository](https://github.com/microsoft/WindowsAppSDK).