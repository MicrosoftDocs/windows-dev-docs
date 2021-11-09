---
description: This guide shows you how to use the Windows App SDK to get starting creating .NET and C++ apps with a WinUI 3 UI.
title: Create a new project that uses the Windows App SDK 
ms.date: 10/05/2021
zone_pivot_groups: winui3-version
ms.topic: article
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, Windows UI Library
ms.localizationpriority: high
ms.custom: 19H1
---

# Create a WinUI 3 app

::: zone pivot="winui3-packaged"

### Key concepts

[!INCLUDE [Packaged apps, Unpackaged apps](../../windows-app-sdk/includes/glossary/packaged-unpackaged-include.md)]


## Instructions for WinUI 3 packaged apps

To create a WinUI 3 desktop app with C# and .NET 5 using Windows App SDK 1.0 Preview 3:

1. In Visual Studio, select **File** -> **New** -> **Project**.

2. In the project drop-down filters, select **C#**, **Windows**, and **WinUI**, respectively.

3. Select one of the following project types and click **Next**.

    - **Blank App, Packaged (WinUI 3 in Desktop)**: Creates a desktop C# .NET app with a WinUI-based user interface. The generated project is configured with the package manifest and other support needed to build the app into an [MSIX package](/windows/msix/overview) without the use of a separate packaging project. For more information about this project type, see [Package your app using single-project MSIX](../../windows-app-sdk/single-project-msix.md).

        > [!NOTE]
        > If you installed the Windows App SDK 1.0 Preview 2 with Visual Studio 2019, this project template has a known issue that results in a build error. To resolve this issue, install the [Single-project MSIX Packaging Tools for Visual Studio 2019 VSIX extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) after you install the Windows App SDK 1.0 Preview 2.

    - **Blank App, Packaged with WAP (WinUI 3 in Desktop)**: Creates a desktop C# .NET app with a WinUI-based user interface. The generated solution includes a separate [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an [MSIX package](/windows/msix/overview).

    :::image type="content" source="images/WinUI3-csharp-newproject-1.0-later.png" alt-text="Screenshot of Create a new project wizard with the Blank App Packaged (Win UI in Desktop) option highlighted." lightbox="images/WinUI3-csharp-newproject-1.0-later.png":::

4. Enter a project name, choose any other options as desired, and click **Create**.

5. In the following dialog box, set the **Target version** to Windows 10, version 2004 (build 19041) and **Minimum version** to Windows 10, version 1809 (build 17763) and then click **OK**.

    ![Target and Min Version](images/WinUI3-minversion.png)

6. At this point, Visual Studio generates one or more projects:

    - **_Project name_ (Desktop)**: This project contains your app's code. The **App.xaml** file and **App.xaml.cs** code-behind file define an `Application` class that represents your app instance. The **MainWindow.xaml** file and **MainWindow.xaml.cs** code-behind file define a `MainWindow` class that represents the main window displayed by your app. These classes derive from types in the **Microsoft.UI.Xaml** namespace provided by WinUI.

        If you used the **Blank App, Packaged (WinUI 3 in Desktop)** project template, this project also includes the package manifest for building the app into an [MSIX package](/windows/msix/overview).

        ![Screenshot of Visual Studio showing the Solution Explorer pane and the contents of the Main Windows X A M L dot C S file for single project M S I X.](images/WinUI-csharp-appproject-1.0-later.png)

    - **_Project name_ (Package)**: This project is generated only if you use the **Blank App, Packaged with WAP (WinUI 3 in Desktop)** project template. This project is a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an [MSIX package](/windows/msix/overview). This project contains the [package manifest](/uwp/schemas/appxpackage/uapmanifestschema/schema-root) for your app, and it is the startup project for your solution by default.

        ![Screenshot of Visual Studio showing the Solution Explorer pane and the contents of the Package app x manifest file.](images/WinUI-csharp-packageproject.png)

7. Enable deployment for your project in **Configuration Manager**. If you do not follow these steps to enable deployment, you will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager".

    1. Select **Build** -> **Configuration Manager**.
    2. In **Configuration Manager**, click the **Deploy** check box for every combination of configuration and platform (for example, **Debug** and **x86**, **Debug** and **arm64**, **Release** and **x64**, and more).
        > [!NOTE]
        > Be sure to use the **Active solution configuration** and **Active solution platform** drop-downs at the top instead of the **Configuration** and **Platform** drop-downs in the same row as the **Deploy** check box.

        ![Enabling Deploy in Configuration Manager](../../windows-app-sdk/images/single-project-configmanager.png)

8. To add a new item to your app project, right-click the **_Project name_ (Desktop)** project node in **Solution Explorer** and select **Add** -> **New Item**. In the **Add New Item** dialog box, select the **WinUI** tab, choose the item you want to add, and then click **Add**. For more details about the available items, see [Item templates for WinUI 3](winui-project-templates-in-visual-studio.md#item-templates-for-winui-3).

    ![Screenshot of the Add New Item dialog box with the Installed > Visual C sharp Items > Win U I selected and the Blank Page option highlighted.](images/winui3-addnewitem.png)

9. Build and run your solution on your development computer to confirm that the app runs without errors.

::: zone-end

::: zone pivot="winui3-unpackaged"

### Key concepts

[!INCLUDE [Packaged apps, Unpackaged apps](../../windows-app-sdk/includes/glossary/packaged-unpackaged-include.md)]



## Instructions for WinUI 3 unpackaged apps

To create a WinUI 3 application without MSIX packaging, choose from one of the following sets of instructions depending on the project language and the version of the Windows App SDK you have installed.

1. Install the [Single-project MSIX Packaging Tools](/windows/apps/windows-app-sdk/single-project-msix#install-the-single-project-msix-packaging-tools).

2. Install the [Visual Studio 2019 C# extension](https://aka.ms/windowsappsdk/1.0-preview2/extension/VS2019/csharp) or [Visual Studio 2022 C# extension](https://aka.ms/windowsappsdk/1.0-preview2/extension/VS2022/csharp), depending on your version of Visual Studio.

3. Install the [Windows App SDK runtime and MSIX packages](../../windows-app-sdk/downloads.md). These are required to run and deploy your app.

4. Create a new app using the ["Blank App, Packaged (WinUI 3 in Desktop)"](#instructions-for-winui-3-packaged-apps) project template. Starting with a packaged app is required to use XAML diagnostics.

5. Add this property to the project file:

   ```xml
   <WindowsPackageType>None</WindowsPackageType>
   ```

    :::image type="content" source="images/winui-csharp-unpackaged-proj.png" alt-text="Visual Studio 2019 - C# Project file with WindowsPackageType set to None highlighted":::

6. Delete package.appxmanifest from project. 

    Otherwise, you will see this error: **Improper project configuration: WindowsPackageType is set to None, but AppxManifest is specified**.

    > [!NOTE]
    > You may need to close the Visual Studio solution to manually delete this file from the filesystem.
    :::image type="content" source="images/winui-csharp-unpackaged-appxmanifest.png" alt-text="Visual Studio 2019 - Solution explorer open with appxmanifest file highlighted":::

7. To debug in Visual Studio, change the debug properties from 'MsixPackage' to 'Project'.
   Otherwise, you'll see an error: "The project doesn't know how to run the profile …"

    > [!NOTE]
    > This isn't necessary if you execute the application (`.exe`) from the command line or Windows File explorer.

    - In **Visual Studio 2022**: Open the launchSettings.json and change the profile with 'MsixPackage' to 'Project'.

        ```json
        {
            "profiles": {
                "Preview3": {
                    "commandName": "Project"
                }
            }
        }
        ```

    - In **Visual Studio 2019 and Visual Studio 2022**: You can use the Visual Studio UI to change the launch settings:
  
      Open the Debug Properties and change the launch profile to 'Project'
  
      :::image type="content" source="images/winui-csharp-vs-debug.png" alt-text="Visual Studio 2019 - Start drop down with C# application debug properties highlighted":::

      :::image type="content" source="images/winui-csharp-vs-debugging-page.png" alt-text="Visual Studio 2019 - C# Application property page with debugger to launch property of Local Windows Debugger highlighted":::

7. If you haven't already done so, **install the Windows App SDK runtime and MSIX packages, which are required to run and deploy your app.**
    > [!div class="button"]
    > [Download latest installer & MSIX packages](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer)

8. Build and run. See the Windows App SDK tutorial for [deploying unpackaged C# apps](../../windows-app-sdk/tutorial-unpackaged-deployment.md) for additional deployment information. This tutorial will guide you through using the [bootstrapper API](../../windows-app-sdk/reference-framework-package-run-time.md) to initialize the [Bootstrapper](/windows/apps/windows-app-sdk/deployment-architecture#bootstrapper) component so your app can use Windows App SDK and WinUI 3 APIs. 

::: zone-end



## Next steps

Congratulations, you've created your first WinUI 3 app with the Windows App SDK. You are now ready to start your development journey. 

> [!div class="nextstepaction"]
> [Start development journey](../../develop/index.md)