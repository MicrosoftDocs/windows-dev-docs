---
title: Build your first WinUI app
description: Install your tools, create and run a C# WinUI app with winapp CLI or Visual Studio, and make a small change in the same project.
author: GrantMeStrength
ms.author: jken
ms.topic: quickstart
ms.date: 09/19/2026
keywords: windows, desktop development, winui
ms.localizationpriority: medium
ms.collection: windows11
---

# Build your first WinUI app

Build your first WinUI app and see it come to life on Windows. This quickstart takes you from tool setup to a running app, then guides you through your first interface update.

## Before you start

### What you'll learn

> [!div class="checklist"]
>
> - Choose and set up your development tools.
> - Create and run a WinUI app.
> - Update the interface and see your change in the running app.

### Choose your development tools

Follow either recommended path through this quickstart:

- [**winapp CLI**](../dev-tools/winapp-cli/index.md): Work from a terminal in your preferred editor. This command-line workflow also works well for agentic coding.
- [**Visual Studio**](/visualstudio/ide/): Use an integrated development and debugging experience.

If your tools are already installed, go to [Creating your first app](#creating-your-first-app).

<a id="install-the-tools"></a>
<a id="set-up-your-development-environment"></a>

## Installing the tools

#### [winapp CLI](#tab/command-line)

1. Enable [Developer Mode](../../advanced-settings/developer-mode.md). You can open its settings with [Developer settings](ms-settings:developers).

2. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) and the [Windows App Development CLI](../dev-tools/winapp-cli/index.md). If you use [WinGet](../../package-manager/winget/index.md), run:

   ```powershell
   winget install --id Microsoft.DotNet.SDK.10 --exact --source winget
   winget install --id Microsoft.WinAppCLI --exact --source winget
   ```

   Skip an installation if the required tool is already installed.

3. Open a new terminal so it picks up the installed commands, then check the versions:

   ```powershell
   dotnet --version
   winapp --version
   ```

   Use .NET SDK 10 or later and winapp CLI 0.6 or later for this workflow.

> [!NOTE]
> winapp CLI is in public preview. `winapp new` manages the WinUI project templates, but it does not install the .NET SDK. See the [CLI 0.6 announcement](https://devblogs.microsoft.com/ifdef-windows/windows-app-development-cli-v0-6-create-new-winui-applications-sign-packages-with-azure-and-more/) for the new project and run commands.

#### [Visual Studio](#tab/visual-studio)

Install [Visual Studio 2026](/visualstudio/ide/) with the **WinUI application development** workload and enable [Developer Mode](../../advanced-settings/developer-mode.md).

### Set up with WinGet

Open [Windows Terminal](/windows/terminal/) and run this PowerShell command to install the required Visual Studio workloads and enable Developer Mode through a [WinGet Configuration file](../../package-manager/configuration/index.md):

```powershell
winget configure -f https://aka.ms/winui-config
```

You can review the configuration and its requirements in the [configuration README](https://github.com/microsoft/winget-dsc/blob/main/samples/Configuration%20files/Learn%20tutorials/WinUI/README.md).

### Set up manually

If you prefer manual setup or don't have WinGet:

1. Enable [Developer Mode](../../advanced-settings/developer-mode.md).
2. [Download and install Visual Studio 2026](https://visualstudio.microsoft.com/downloads/).
3. In the Visual Studio Installer, select the **WinUI application development** workload.

   :::image type="content" source="images/hello-world/vs-workload-winui.png" alt-text="Visual Studio Installer with the WinUI application development workload selected.":::

---

<a id="create-and-run-the-app"></a>
<a id="create-and-launch-the-app"></a>
<a id="creating-your-first-app"></a>

## Creating your first app

Use the workflow you installed above to create and launch the project.

#### [winapp CLI](#tab/command-line)

From a folder where you keep your projects, run:

```powershell
winapp new --name MyWinUIApp --template winui --use-defaults
cd MyWinUIApp
winapp run
```

`winapp new` creates the blank WinUI app. `winapp run` builds the project and launches it; you don't need a separate build or package-registration command.

#### [Visual Studio](#tab/visual-studio)

1. Open Visual Studio and select **Create a new project**.
2. Search for **WinUI**, select the **WinUI Blank App (Packaged)** C# template, and select **Next**.

   :::image type="content" source="images/hello-world/create-project.png" alt-text="The blank packaged WinUI C# project template in Visual Studio.":::

3. Name the project `MyWinUIApp` and select **Create**.
4. Press **F5** to build and run the app.

---

Your app opens a desktop window. Continue with **Make a change** below.

<a id="make-a-change"></a>

## Make a change

Keep the project you just created. Add a message to its existing interface:

1. Close the running app. Open `MainPage.xaml` in your editor. If your template doesn't contain that file, open `MainWindow.xaml` instead.
2. Find the layout panel that holds the app's content. In the current CLI blank template, `MainPage.xaml` contains an empty `<Grid />`. Replace that empty grid with:

   ```xaml
   <Grid>
       <TextBlock
           Text="Hello, WinUI!"
           HorizontalAlignment="Center"
           VerticalAlignment="Center" />
   </Grid>
   ```

   If the panel already contains controls, add only the `TextBlock` inside that panel. Keep the existing controls, names, and event handlers. Don't replace the surrounding `Page` or `Window`, or the frame and title bar in `MainWindow.xaml`.

3. Save the file. Run `winapp run` again from the project folder, or press **F5** in Visual Studio.

Congratulations! You've created, run, and updated your first WinUI app. The window now displays **Hello, WinUI!**

## Troubleshooting

| Problem | What to check |
|---|---|
| `winapp` or `dotnet` is not recognized. | Reopen the terminal after installation. Use the version commands in the CLI tab to confirm both tools are available. |
| `winapp new` is not recognized. | This workflow requires winapp CLI 0.6 or later. Update the CLI using the installation method you chose. |
| WinUI templates don't appear in Visual Studio. | Open the Visual Studio Installer, select **Modify**, and confirm that the **WinUI application development** workload is installed. Restart Visual Studio afterward. |
| The build reports an SDK or restore error. | Check the selected workflow's prerequisites and the first error in the build output. Use [Windows App SDK support](../windows-app-sdk/support.md) to check version requirements before changing project properties. |
| Deployment reports that Developer Mode is disabled. | Enable [Developer Mode](../../advanced-settings/developer-mode.md), then run the app again. |
| Your new message does not appear. | Save the XAML file and relaunch the project. In templates with a `MainPage.xaml`, add the message to that page rather than replacing the window's hosting frame. |

## Next steps

> [!div class="nextstepaction"]
> [Build a notes app](../tutorials/winui-notes/intro.md)

The Notes tutorial uses a separate `WinUINotes` project to teach pages, navigation, data binding, and local storage. You can also find focused control examples in [Resources](../dev-tools/index.md).
