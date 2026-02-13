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

This Hello World guide walks you through setting up your WinUI and Windows App SDK development environment and creating your first app. To develop WinUI apps, you need:

- [Visual Studio 2026](/visualstudio/ide/) with the required workloads for WinUI and Windows App SDK
- [Developer Mode](/windows/advanced-settings/developer-mode) enabled on your device

## 1. Set up your development environment

### [WinGet Configuration](#tab/wingetconfig)

[WinGet Configuration files](../../package-manager/configuration/index.md) automate the process of setting up your development environment.

Run the following command in PowerShell to set up your environment:

```powershell
winget configure -f http://aka.ms/winui-config
```

### [Manual installation](#tab/manual)

#### Enable Developer Mode

Windows includes a Developer Mode that adjusts security settings to let you run and test apps you're building. Enable Developer Mode before building, deploying, and testing your app with Visual Studio.

> [!TIP]
> If you don't enable Developer Mode now, Visual Studio prompts you to enable it when you try to build your app.

To enable Developer Mode:

* Open Windows Settings and navigate to the **[System > Advanced](ms-settings:developers)** page.
* Toggle the **Developer Mode** switch to **On** and confirm your choice in the confirmation dialog.

For more information about Developer Mode, see [Settings for developers](/windows/advanced-settings/developer-mode).

#### Install Visual Studio and the required workloads for WinUI and Windows App SDK

Download and install the latest Visual Studio using the link below. For details, see [Install Visual Studio](/visualstudio/install/install-visual-studio).

> [!div class="button"]
> [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

##### Required workloads and components

In the Visual Studio Installer, select the following workloads on the **Workloads** tab. If Visual Studio is already installed, open the installer and select **Modify** to add them.

* **For C# app development** using the Windows App SDK, select **WinUI application development**.

:::image type="content" source="images/hello-world/vs-workload-winui.png" alt-text="A screenshot of the Visual Studio installer UI with the WinUI application development workload selected.":::

* **For C++ app development**, select the **C++ WinUI app development tools** under the **WinUI application development** node in the **Installation details** pane (This will also select any additional required components.)

> [!NOTE]
> _In Visual Studio 17.10 - 17.12, this workload is called **Windows application development**._

---

## 2. Create and launch your first WinUI app

1. Open Visual Studio and select **Create a new project**.
:::image type="content" source="images/hello-world/start-project.png" alt-text="Create a new project":::

1. Search for **WinUI**, select the **WinUI Blank App (Packaged)** C# project template, and select **Next**.
:::image type="content" source="images/hello-world/create-project.png" alt-text="Blank, packaged WinUI C# desktop app":::

1. Enter a project name and select **Create**.
:::image type="content" source="images/hello-world/configure-project.png" alt-text="Specify project details":::

1. Press **Start** (**F5**) to build and run your app.
:::image type="content" source="images/hello-world/start-click.png" alt-text="Build and run your project":::

   Your app builds, deploys, and launches in debug mode:

   :::image type="content" source="images/hello-world/click-me.png" border="false" alt-text="Hello World project built and running":::

You've built and launched your first WinUI app! 🎉

## Next steps

> [!div class="nextstepaction"]
> [Build your first app with WinUI](../tutorials/winui-notes/intro.md)

* **[WinUI Gallery app](https://apps.microsoft.com/detail/9p3jfpwwdzrc)** — Explore WinUI controls and features in action.
  [!INCLUDE [winui-3-gallery](../../includes/winui-3-gallery.md)]
* **[WinUI fundamentals](../develop/index.md)** — Learn the building blocks of WinUI apps.
* **[Fluent Design](../design/index.md)** — Design beautiful, accessible Windows apps.
* **[Samples and tools](samples.md)** — Find code samples to accelerate your development.
