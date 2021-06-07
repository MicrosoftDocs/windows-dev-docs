---
description: This guide shows you how to get starting creating UWP apps with a WinUI 3 UI.
title: Get started with WinUI 3 for UWP apps (Preview)
ms.date: 03/19/2021
ms.topic: article
keywords: windows 10, uwp, winui
ms.localizationpriority: high
ms.custom: 19H1
---

# Get started with WinUI 3 for UWP apps (Preview)

You can create a Universal Windows Platform (UWP) app with a user interface built entirely on WinUI 3 using project templates included with [WinUI 3 - Project Reunion 0.8 Preview](release-notes/release-notes-08-preview.md). When you create apps using these project templates, the entire user interface of your application is implemented using windows, controls, and styles provided by WinUI 3. For a complete list of supported WinUI 3 project templates, see [Project templates for WinUI 3](winui-project-templates-in-visual-studio.md#project-templates-for-winui-3).

WinUI 3 ships as a part of the Project Reunion package. For more information about Project Reunion, see [Build desktop Windows apps with Project Reunion](../../project-reunion/index.md).

> [!NOTE] 
> WinUI 3 support for building UWP apps is currently in preview, and is not production-ready. You will not be able to ship WinUI 3 UWP apps to the Microsoft Store.

## Prerequisites

To use the WinUI 3 for UWP Preview project templates, configure your development computer by following the directions found in the [Set up your development environment](../../project-reunion/set-up-your-development-environment.md) guide for Project Reunion. 

> [!NOTE]
> You must download the [Project Reunion (**Preview**) VSIX](https://aka.ms/projectreunion/previewdownload) to get the UWP Preview project templates and build UWP apps with WinUI 3. 

## Create a "WinUI 3 app in UWP" for C#

1. Using Visual Studio 2019, create a new project.
   - If Visual Studio is running already, select **File** -> **New** -> **Project**.

       :::image type="content" source="images/WinUI-and-UWP/vs2019-menu-file-new-project.png" alt-text="Visual Studio 2019 - File -> New -> Project menu":::

   - Otherwise, launch Visual Studio and select **Create a new project**.

       :::image type="content" source="images/WinUI-and-UWP/vs2019-splash-new-project.png" alt-text="Visual Studio 2019 - Create a new project":::

2. In the **Create a new project** dialog, select **C#**, **Windows**, and **WinUI**, respectively from the project drop-down filters.

3. Select the **[Experimental] Blank App (WinUI in UWP)** project type and click **Next**.

    :::image type="content" source="images/WinUI-and-UWP/vs2019-create-new-project-dialog.png" alt-text="Visual Studio 2019 - Create a new project dialog":::

4. Enter a project name, choose any other options as desired, and click **Create**.

    :::image type="content" source="images/WinUI-and-UWP/vs2019-configure-new-project-dialog.png" alt-text="Screenshot of the Configure your new project dialog box with the Location text box and the Create option highlighted.":::

5. In the following dialog box, set the **Target version** to Windows 10, version 2004 (build 19041) and **Minimum version** to Windows 10, version 1809 (build 17763) and then click **OK**.

    :::image type="content" source="images/WinUI-min-target-version.png" alt-text="Target and Min Version dialog":::

6. Visual Studio generates the **WinUI in UWP** project with the following objects:

    - ***Project name* (Universal Windows)**: Contains your application code. This is the default startup project for your project solution.

        :::image type="content" source="images/WinUI-and-UWP/vs2019-project.png" alt-text="Screenshot of the Solution Explorer panel with the Universal Windows solution highlighted.":::

    - **Package.appxmanifest**: Contains info the system needs to deploy, display, or update your app. For more details, see [App package manifest](/uwp/schemas/appxpackage/appx-package-manifest)

        :::image type="content" source="images/WinUI-and-UWP/vs2019-file-package-manifest.png" alt-text="Visual Studio 2019 - App package manifest":::

    - **App.xaml/App.xaml.cs**: Code files that define the `Application` class, which represents your app instance.

        :::image type="content" source="images/WinUI-and-UWP/vs2019-file-app-xaml.png" alt-text="Visual Studio 2019 - App.xaml file":::

        :::image type="content" source="images/WinUI-and-UWP/vs2019-file-app-xaml-cs.png" alt-text="Visual Studio 2019 - App.xaml.cs file":::

    - **MainPage.xaml/MainPage.xaml.cs**: Code files that represent the main windows displayed by your app. These classes derive from types in the **Microsoft.UI.Xaml** namespace provided by WinUI.

        :::image type="content" source="images/WinUI-and-UWP/vs2019-file-mainpage-xaml.png" alt-text="Visual Studio 2019 - MainPage.xaml file":::

        :::image type="content" source="images/WinUI-and-UWP/vs2019-file-mainpage-xaml-cs.png" alt-text="Visual Studio 2019 - MainPage.xaml.cs file":::

7. To add a new item to your app project, right-click the ***Project name* (Universal Windows)** project node in **Solution Explorer** and select **Add** -> **New Item**. In the **Add New Item** dialog box, select the **WinUI** tab, choose the item you want to add, and then click **Add**. For more details about the available items, see [Item templates for WinUI 3](winui-project-templates-in-visual-studio.md#item-templates-for-winui-3).

    :::image type="content" source="images/WinUI-and-UWP/vs2019-add-new-item-dialog.png" alt-text="Visual Studio 2019 - Add new item dialog":::

8. Build, deploy, and launch your app to see what it looks like.

    1. You can debug your app on the local machine, in a simulator or emulator, or on a remote device. Select your target device from the drop down.

        :::image type="content" source="images/WinUI-and-UWP/vs2019-menu-target-device.png" alt-text="Screenshot of the Local Machine dropdown list.":::

    1. Press F5, click the **Build** button, or select **Debug -> Start Debugging** to build and run your solution and confirm the app runs without errors.

        :::image type="content" source="images/WinUI-and-UWP/vs2019-project-running.png" alt-text="Screenshot of the app running showing the Click Me button.":::

## Known issues and limitations

See the [Limitations and known issues](index.md#limitations-and-known-issues) section of [Windows UI Library 3 - Project Reunion 0.5 Preview](release-notes/winui3-project-reunion-0.5-preview.md).

## Related topics

- [Windows UI Library 3 - Project Reunion 0.5 Preview](release-notes/winui3-project-reunion-0.5-preview.md)
- [Create your first app](/windows/uwp/get-started/your-first-app)
