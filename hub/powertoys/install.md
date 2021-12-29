---
title: Install PowerToys
description: Install PowerToys, a set of utilities for customizing Windows, using an executable file or package manager (WinGet, Chocolatey, Scoop).
ms.date: 12/03/2021
ms.topic: quickstart
ms.localizationpriority: high
no-loc: [PowerToys, Windows, Chocolatey, Scoop]
---

# Install PowerToys

We recommend installing PowerToys via GitHub or Microsoft Store, but alternative install methods are also listed if you prefer using a package manager.

## Install with Windows executable file via GitHub

> [!div class="nextstepaction"]
> [Install PowerToys](https://aka.ms/installpowertoys)

To install PowerToys using a Windows executable file:

1. Visit the [Microsoft PowerToys GitHub releases page](https://aka.ms/installpowertoys).
2. Select the **Assets** drop-down menu to display the files for the release.
3. Select the `PowerToysSetup-0.##.#-x64.exe` file to download the PowerToys executable installer.
4. Once downloaded, open the executable file and follow the installation prompts.

## Requirements

- Windows 11 or Windows 10 v1903 (18362) or newer.
- [.NET Core 3.1.20 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-desktop-3.1.20-windows-x64-installer) or a newer 3.1.x runtime. The installer will handle this if not present.
- x64 architecture currently supported. ARM support to become available at a later date.

To ensure that your machine meets these requirements, check your Windows version and build number by selecting <kbd>⊞ Win</kbd>+<kbd>R</kbd>, then type `winver` and press <kbd>OK</kbd>. Or enter the `ver` command in Windows Command Prompt. You can [update to the latest Windows version](ms-settings:windowsupdate) in the **Windows Settings**.

## Alternative Install Methods

- [Microsoft Store](#install-with-microsoft-store)
- [Windows Package Manager](#install-with-windows-package-manager)
- [Community-driven install tools](#community-driven-install-tools) _(Not officially supported)_

### Install with Microsoft Store

Install from the [Microsoft Store's PowerToys page](https://aka.ms/getPowertoys). You must be using the [new Microsoft Store](https://blogs.windows.com/windowsExperience/2021/06/24/building-a-new-open-microsoft-store-on-windows-11/) which will be available for both Windows 11 and Windows 10.

## Install with Windows Package Manager

To install PowerToys using the [Windows Package Manager](../package-manager/winget/index.md), it is as simple as running the following command from the command line / PowerShell:

```powershell
winget install Microsoft.PowerToys --source winget
```

## Community-driven install tools

These community-driven alternative install methods are not officially supported and the PowerToys team does not update or manage these packages.

### Install with Chocolatey

To install PowerToys using [Chocolatey](https://chocolatey.org/), run the following command from your command line / PowerShell:

```powershell
choco install powertoys
```

To upgrade PowerToys, run:

```powershell
choco upgrade powertoys
```

If you have issues when installing/upgrading, visit the [PowerToys package on Chocolatey.org](https://chocolatey.org/packages/powertoys) and follow the [Chocolatey triage process](https://chocolatey.org/docs/package-triage-process).

### Install with Scoop

To install PowerToys using [Scoop](https://scoop.sh/), run the following command from the command line / PowerShell:

```powershell
scoop bucket add extras
scoop install powertoys
```

To update PowerToys, run the following command from the command line / PowerShell:

```powershell
scoop update powertoys
```

If you have issues when installing/updating, file an issue in the [Scoop repo on GitHub](https://github.com/lukesampson/scoop/issues).

## Post Install

After successfully installing PowerToys, an overview window will display with introductory guidance on each of the available utilities.

## Updates

PowerToys uses an auto-updater that checks for new versions when the app is running. If enabled, a toast notification will appear when an update is available. Updates can also be checked for manually from the PowerToys Settings, under the General page.

![PowerToys Update](../images/powertoys-updates.png)
