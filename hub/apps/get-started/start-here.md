---
title: Setup for WinUI
description: List of steps to get started developing Windows apps with WinUI and the Windows App SDK.
ms.topic: how-to
ms.date: 12/18/2025
keywords: windows, desktop development
ms.localizationpriority: medium
ms.collection: windows11
---

# Quickstart for WinUI and Windows App SDK

This Hello World guide walks you through setting up your WinUI and Windows App SDK development environment in Visual Studio and creating your first app.

## 1. Enable Developer Mode

Windows includes a Developer Mode that adjusts security settings to let you run and test apps you're building. Enable Developer Mode before building, deploying, and testing your app with Visual Studio.

> [!TIP]
> If you don't enable Developer Mode now, Visual Studio prompts you to enable it when you try to build your app.

To enable Developer Mode:

* Open Windows Settings and navigate to the **[System > Advanced](ms-settings:developers)** page.
* Toggle the **Developer Mode** switch to **On** and confirm your choice in the confirmation dialog.

For more information about Developer Mode, see [Settings for developers](/windows/advanced-settings/developer-mode).

## 2. Install Visual Studio and the required workloads for WinUI and Windows App SDK

Use [Visual Studio](/visualstudio/ide/), Microsoft's IDE, to build, debug, and deploy WinUI apps. Visual Studio includes ready-to-use project templates for Windows that help you get started quickly.

The free **Visual Studio Community** edition includes everything you need to develop apps. **Professional** and **Enterprise** editions are available for larger teams or organizations. For more details, see [What is Visual Studio?](/visualstudio/get-started/visual-studio-ide) and the [system requirements for Windows app development](../windows-app-sdk/system-requirements.md).

### [WinGet](#tab/winget)

Install the required tools and workloads by running one of the following commands in a console.  
The command opens the Visual Studio Installer with any missing workloads preselected — select **Modify** to install them.

You can replace *Community* with *Professional* or *Enterprise* to install a different Visual Studio edition.

**For C# app development**

```console
winget install "Visual Studio Community 2022"  --override "--add Microsoft.VisualStudio.Workload.ManagedDesktop Microsoft.VisualStudio.ComponentGroup.WindowsAppSDK.Cs" -s msstore
```

**For C++ app development**

```console
winget install "Visual Studio Community 2022"  --override "--add Microsoft.VisualStudio.Workload.NativeDesktop  Microsoft.VisualStudio.ComponentGroup.WindowsAppSDK.Cpp"  -s msstore
```

### [Manual installation](#tab/manual)

Download and install the latest Visual Studio using the link below. For details, see [Install Visual Studio](/visualstudio/install/install-visual-studio).

> [!div class="button"]
> [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

#### Required workloads and components

When installing Visual Studio, include the workloads and components required for developing with WinUI and the Windows App SDK. After installation, you can open the Visual Studio Installer and select **Modify** to add workloads or components.

On the **Workloads** tab in the Visual Studio Installer, select the following workloads and components:

* **For C# app development** using the Windows App SDK, select **WinUI application development**.

:::image type="content" source="images/hello-world/vs-workload-winui.png" alt-text="A screenshot of the Visual Studio installer UI with the WinUI application development workload selected.":::

* **For C++ app development**, select the **C++ WinUI app development tools** under the **WinUI application development** node in the **Installation details** pane (This will also select any additional required components.)

> [!NOTE]
> _In Visual Studio 17.10 - 17.12, this workload is called **Windows application development**._

---

## 3. Create and launch your first WinUI app

Visual Studio project templates include everything you need to create an app quickly. When you create a project from a WinUI app template, you start with a runnable app that you can extend with your own code.

To create a new project using the **WinUI C# Blank App** project template:

1. Open Visual Studio and select **Create a new project** on the launch page. If Visual Studio is already open, select **File** > **New** > **Project**.
:::image type="content" source="images/hello-world/start-project.png" alt-text="Create a new project":::

1. Search for **WinUI**, select the **WinUI Blank App (Packaged)** C# project template, and then select **Next**.
:::image type="content" source="images/hello-world/create-project.png" alt-text="Blank, packaged WinUI 3 C# desktop app":::

1. Specify a project name, then select **Create**. You can optionally specify a solution name and directory, or leave the defaults. In this image, the `Hello World` project belongs to a `Hello World` solution, which lives in `C:\Projects\`:
:::image type="content" source="images/hello-world/configure-project.png" alt-text="Specify project details":::

1. Press the **Start** button to build and run your project:<br/>
:::image type="content" source="images/hello-world/start-click.png" alt-text="Build and run your project":::<br/>

  Your project will build, be deployed to your local machine, and run in debug mode:<br/>

  :::image type="content" source="images/hello-world/click-me.png" border="false" alt-text="Hello World project built and running":::

Congratulations, you've just built your first WinUI app!

## Next steps

> [!div class="nextstepaction"]
> [Build your first app with WinUI](../tutorials/winui-notes/intro.md)

* To get an idea of what WinUI offers, check out the WinUI Gallery app.
  [!INCLUDE [winui-3-gallery](../../includes/winui-3-gallery.md)]

* Learn more about [WinUI fundamentals](../develop/index.md).
* Explore [Fluent Design](../design/index.md) principles.
* Find [samples and tools](samples.md) to help you develop apps more efficiently.
