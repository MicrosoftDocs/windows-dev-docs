---
title: "Quick start: Create your first WinUI 3 app"
description: Create, build, and run your first WinUI 3 app with the Windows App SDK using Visual Studio 2026 or the .NET command line.
ms.topic: quickstart
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
keywords: windows, desktop development
ms.localizationpriority: medium
ms.collection: windows11
---

# Quick start: Create your first WinUI 3 app

Create, build, and run your first WinUI 3 app. Choose **Visual Studio** for a full IDE experience with visual designer support, or **dotnet new** to work from the command line in any editor. Use the tabs below to switch between the two approaches.

#### [Visual Studio](#tab/visual-studio)

## Set up your development environment

To develop WinUI 3 apps with Visual Studio, you need [Visual Studio 2026](/visualstudio/ide/) with the required workloads and [Developer Mode](/windows/advanced-settings/developer-mode) enabled.

**Set up with WinGet (recommended)**

Open [Windows Terminal](/windows/terminal/) and run the following command in PowerShell to automatically install Visual Studio 2026 with the required workloads and enable Developer Mode using a [WinGet Configuration file](../../package-manager/configuration/index.md):

```powershell
winget configure -f https://aka.ms/winui-config
```

To review the config file and learn more, see its [README](https://github.com/microsoft/winget-dsc/blob/main/samples/Configuration%20files/Learn%20tutorials/WinUI/README.md) on GitHub.

> [!NOTE]
> If WinGet is not available in your environment, install it first:
> ```powershell
> Install-Module -Name Microsoft.WinGet.Client -Force
> Repair-WinGetPackageManager -AllUsers
> ```
> See [Using WinGet](../../package-manager/winget/index.md) for more information.

**Set up manually**

If you prefer to install tools manually:

1. Enable [Developer Mode](/windows/advanced-settings/developer-mode): open Windows Settings, navigate to **[System > Advanced](ms-settings:developers)**, and toggle **Developer Mode** to **On**.

2. [Download and install Visual Studio 2026](https://visualstudio.microsoft.com/downloads/). For details, see [Install Visual Studio](/visualstudio/install/install-visual-studio).

3. In the Visual Studio Installer, select the **WinUI application development** workload on the **Workloads** tab. For C++ development, also select **C++ WinUI app development tools** under that workload in the **Installation details** pane.

   :::image type="content" source="images/hello-world/vs-workload-winui.png" alt-text="A screenshot of the Visual Studio installer UI with the WinUI application development workload selected.":::

> [!TIP]
> If you don't see WinUI templates after installing Visual Studio, open the Visual Studio Installer, select **Modify**, and confirm the **WinUI application development** workload is checked. Restart Visual Studio after modifying the installation.

## Create and launch your first WinUI 3 app

1. Open Visual Studio 2026 and select **Create a new project**.

2. Search for **WinUI**, select the **WinUI Blank App (Packaged)** C# project template, and select **Next**.

   :::image type="content" source="images/hello-world/create-project.png" lightbox="images/hello-world/create-project.png" alt-text="Blank, packaged WinUI C# desktop app":::

3. Enter a project name and select **Create**.

   :::image type="content" source="images/hello-world/configure-project.png" lightbox="images/hello-world/configure-project.png" alt-text="Specify project details":::

4. Press **Start** (**F5**) to build and run your app.

   :::image type="content" source="images/hello-world/start-click.png" alt-text="Build and run your project":::

   Your app builds, deploys, and launches in debug mode:

   :::image type="content" source="images/hello-world/click-me.png" border="false" alt-text="Hello World project built and running":::

   You've built and launched your first WinUI 3 app! 🎉

#### [Command line](#tab/command-line)

## Prerequisites

- Windows 10 version 1809 (build 17763) or later
- [Developer Mode](/windows/advanced-settings/developer-mode) enabled (`ms-settings:developers`)
- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later (verify with `dotnet --version`)

## Create and run your first WinUI 3 app

1. Install the WinUI 3 project templates for `dotnet new` (run once):

   ```powershell
   dotnet new install Microsoft.WindowsAppSDK.WinUI.CSharp.Templates
   ```

2. Create a new project and navigate into it:

   ```powershell
   dotnet new winui -n MyWinUIApp
   cd MyWinUIApp
   ```

3. Build the project:

   ```powershell
   dotnet build
   ```

4. Run the app:

   ```powershell
   dotnet run
   ```

   The template includes `Microsoft.Windows.SDK.BuildTools.WinApp`, which hooks into the .NET CLI `run` target to register a debug identity and launch the app with MSIX package identity — no manual deployment step is required.

   An empty app window opens. You've built and launched your first WinUI 3 app! 🎉

---

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
