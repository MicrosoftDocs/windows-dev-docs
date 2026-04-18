---
title: Set up your environment and create your first WinUI project
description: List of steps to get started developing Windows apps with WinUI and the Windows App SDK.
ms.topic: how-to
ms.date: 02/18/2026
keywords: windows, desktop development
ms.localizationpriority: medium
ms.collection: windows11
---

# Quick start: Set up your environment and create a WinUI 3 project

This quick start guide walks you through setting up your WinUI and Windows App SDK development environment and creating your first app. To learn more about how [Visual Studio and its templates](../dev-tools/visual-studio.md) support WinUI development, see the Visual Studio overview. To develop WinUI apps, you need:

- [Visual Studio 2026](/visualstudio/ide/) with the required workloads for WinUI and Windows App SDK
- [Developer Mode](/windows/advanced-settings/developer-mode) enabled on your device

## Set up your development environment

#### [WinGet Configuration](#tab/wingetconfig)

Open [Windows Terminal](/windows/terminal/) and run the following command in PowerShell to automatically set up your environment using a [WinGet Configuration file](../../package-manager/configuration/index.md). This will:

- Install Visual Studio 2026 with the required workloads
- Enable Developer Mode

```powershell
winget configure -f https://aka.ms/winui-config
```

To review the config file and learn more, see its [README](https://github.com/microsoft/winget-dsc/blob/main/samples/Configuration%20files/Learn%20tutorials/WinUI/README.md) on GitHub.

#### [Manual installation](#tab/manual)

#### Enable Developer Mode

Windows includes a [Developer Mode](/windows/advanced-settings/developer-mode) that adjusts security settings to let you run and test apps you're building. Enable Developer Mode before building, deploying, and testing your app with Visual Studio.

To enable Developer Mode:

* Open Windows Settings and navigate to the **[System > Advanced](ms-settings:developers)** page.
* Toggle the **Developer Mode** switch to **On** and confirm your choice in the confirmation dialog.

#### Install Visual Studio and the required WinUI and Windows App SDK workloads

Download and install the latest Visual Studio using the link below. For details, see [Install Visual Studio](/visualstudio/install/install-visual-studio).

> [!div class="button"]
> [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

##### Required workloads and components

In the Visual Studio Installer, select the following workloads on the **Workloads** tab. If Visual Studio is already installed, open the installer and select **Modify** to add them.

* **For C# app development** using the Windows App SDK, select **WinUI application development**.

:::image type="content" source="images/hello-world/vs-workload-winui.png" alt-text="A screenshot of the Visual Studio installer UI with the WinUI application development workload selected.":::

* **For C++ app development**, select the **C++ WinUI app development tools** under the **WinUI application development** node in the **Installation details** pane (This will also select any additional required components.)

> [!TIP]
> If you don't see WinUI templates after installing Visual Studio, open the Visual Studio Installer, select **Modify**, and confirm the **WinUI application development** workload is checked. Restart Visual Studio after modifying the installation.

---

## Create and launch your first WinUI app

1. Open Visual Studio and select **Create a new project**.

2. Search for **WinUI**, select the **WinUI Blank App (Packaged)** C# project template, and select **Next**.

   :::image type="content" source="images/hello-world/create-project.png" lightbox="images/hello-world/create-project.png" alt-text="Blank, packaged WinUI C# desktop app":::

3. Enter a project name and select **Create**.

   :::image type="content" source="images/hello-world/configure-project.png" lightbox="images/hello-world/configure-project.png" alt-text="Specify project details":::

4. Press **Start** (**F5**) to build and run your app.

   :::image type="content" source="images/hello-world/start-click.png" alt-text="Build and run your project":::

   Your app builds, deploys, and launches in debug mode:

   :::image type="content" source="images/hello-world/click-me.png" border="false" alt-text="Hello World project built and running":::

   You've built and launched your first WinUI app! 🎉

## Next steps

:::row:::
    :::column:::
        [![Hello WinUI](../winui/winui3/images/hero-hello-winui.png)](../tutorials/winui-notes/intro.md)<br>
        **[Build your first WinUI app](../tutorials/winui-notes/intro.md)**<br>
        Ready to go further? Follow the step-by-step tutorial to build a full WinUI app.
    :::column-end:::
    :::column:::
        [![WinUI 3 Gallery](../winui/winui3/images/winui-gallery.png)](../dev-tools/samples.md#winui-3-gallery)<br>
        **[WinUI 3 Gallery](../dev-tools/samples.md#winui-3-gallery)**<br>
        Explore interactive examples of WinUI controls, features, and functionality.
    :::column-end:::
    :::column:::
        [![Samples icon](../images/tile-samples.png)](../dev-tools/samples.md)<br>
        **[Samples and resources](../dev-tools/samples.md)**<br>
        Browse code samples, starter projects, and tools to accelerate your development.
    :::column-end:::
:::row-end:::
