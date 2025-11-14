---
title: Get started with Windows development using WinUI
description: List of steps to get started developing Windows apps with WinUI and the Windows App SDK.
ms.topic: how-to
ms.date: 11/4/2025
keywords: windows, desktop development
ms.localizationpriority: medium
ms.collection: windows11
---

# Setup and tooling for WinUI

Welcome to Windows app development. This guide will take you through the steps needed to begin creating your first app with WinUI. It will also point you to resources that will help you learn more about Windows development. If you want a step-by-step guide to setting up your developer environment and building your first WinUI app with the latest tools, please see [WinUI 101](/training/modules/winui-101/).


## 1. Enable Developer Mode

Windows has a special mode for developers that adjusts security settings so you can run the apps you're working on. You need to enable Developer Mode before you can build, deploy, and test your app by using Visual Studio.

> [!TIP]
> If you don't enable Developer Mode now, Visual Studio prompts you to enable it when you try to build your app.

To enable Developer Mode:

* Open Windows Settings and navigate to the **[System > Advanced](ms-settings:developers)** page.
* Toggle the **Developer Mode** switch to **On** and confirm your choice in the confirmation dialog.

For more information about Developer Mode, see [Settings for developers](/windows/advanced-settings/developer-mode).


## 2. Install Visual Studio and required workloads

Use [Visual Studio](/visualstudio/ide/), Microsoft’s powerful IDE, to build, debug, and deploy your WinUI app. It offers ready-to-use project templates for Windows and other platforms to help you get started quickly.

The free **Visual Studio Community Edition** includes everything you need to develop apps. Larger teams or enterprises may require **Professional** or **Enterprise** editions. Learn more in [What is Visual Studio?](/visualstudio/get-started/visual-studio-ide) and [system requirements for Windows app development](../windows-app-sdk/system-requirements.md).

#### [WinGet](#tab/winget)

Install the required tools and workloads by running one of the following commands in the console.  
The command opens the Visual Studio Installer with any missing workloads preselected — just choose **Modify** to install them.  

If you prefer, you can replace *Community* with *Professional* or *Enterprise* to install those editions instead.

**For C# app development**
```console
winget install "Visual Studio Community 2022"  --override "--add Microsoft.VisualStudio.Workload.ManagedDesktop Microsoft.VisualStudio.ComponentGroup.WindowsAppSDK.Cs" -s msstore
```

**For C++ app development**
```console
winget install "Visual Studio Community 2022"  --override "--add Microsoft.VisualStudio.Workload.NativeDesktop  Microsoft.VisualStudio.ComponentGroup.WindowsAppSDK.Cpp"  -s msstore
```

#### [Manual installation](#tab/manual)

Use the following link to download and install the latest Visual Studio. The installer walks you through the steps, but if you need detailed instructions, see [Install Visual Studio](/visualstudio/install/install-visual-studio).

> [!div class="button"]
> [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

#### Required workloads and components

While installing Visual Studio, install the workloads and components required for developing with WinUI and the Windows App SDK. After installation, you can open the Visual Studio Installer app and select **Modify** to add workloads and components.

On the **Workloads** tab of the Visual Studio Installer app, select the following workloads and components:

* **For C# app development** using the Windows App SDK, select **WinUI application development**.

:::image type="content" source="images/hello-world/vs-workload-winui.png" alt-text="A screenshot of the Visual Studio installer UI with the WinUI application development workload selected.":::

* **For C++ app development**, select the **C++ WinUI app development tools** under the **WinUI application development** node in the **Installation details** pane (This will also select any additional required components.)

> [!NOTE]
> _In Visual Studio 17.10 - 17.12, this workload is called **Windows application development**._

---

## 3. Create and launch your first WinUI app

Visual Studio project templates include all the files you need to quickly create your app. In fact, after you create your project from a WinUI app template, you already have an app that you can run, and then add your code to.

To create a new project by using the WinUI C# Blank App project template:

1. Open Visual Studio and select **Create a new project** from the launch page. (If Visual Studio is already open to the editor, select **File** > **New** > **Project**):
  :::image type="content" source="images/hello-world/start-project.png" alt-text="Create a new project":::

1. Search for `WinUI` and select the `WinUI Blank App (Packaged)` C# project template, then select **Next**:
  :::image type="content" source="images/hello-world/create-project.png" alt-text="Blank, packaged WinUI 3 C# desktop app":::

1. Specify a project name, then select **Create**. You can optionally specify a solution name and directory, or leave the defaults. In this image, the `Hello World` project belongs to a `Hello World` solution, which lives in `C:\Projects\`:
  :::image type="content" source="images/hello-world/configure-project.png" alt-text="Specify project details":::

    > [!NOTE]
    > If you want to use this project to build the complete app in the Next steps section, name the project `WinUINotes`.

1. Select the **Debug** "Start" button to build and run your project:<br/>
  :::image type="content" source="images/hello-world/start-click.png" alt-text="Build and run your project":::<br/>
  Your project will build, be deployed to your local machine, and run in debug mode:<br/>
  :::image type="content" source="images/hello-world/click-me.png" border="false" alt-text="Hello World project built and running":::

1. To stop debugging, close the app window, or select the debug "Stop" button in Visual Studio.

Congratulations, you've just built your first WinUI app! Continue with the next steps below to explore more.

## Next steps

> [!div class="nextstepaction"]
> [Build your first notetaking app with WinUI](../tutorials/winui-notes/intro.md)

* To get an idea of what WinUI offers, check out the WinUI Gallery app.
  [!INCLUDE [winui-3-gallery](../../includes/winui-3-gallery.md)]
* Learn more about [WinUI fundamentals](../develop/index.md).
* Explore [Fluent Design](../design/index.md) principles.
* Find [samples and tools](samples.md) to help you develop apps more efficiently.
