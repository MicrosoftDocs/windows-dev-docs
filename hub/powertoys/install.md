---
title: Install PowerToys
description: Install PowerToys, a set of utilities for customizing Windows 10, using an executable file or package manager (WinGet, Chocolatey, Scoop).
ms.date: 12/02/2020
ms.topic: quickstart
ms.localizationpriority: medium
---

# Install PowerToys

We recommend installing PowerToys using the Windows executable button linked below, but alternative install methods are also listed if you prefer using a package manager.

## Install with Windows executable file

> [!div class="nextstepaction"]
> [Install PowerToys](https://aka.ms/installpowertoys)

To install PowerToys using a Windows executable file:

1. Visit the [Microsoft PowerToys GitHub releases page](https://github.com/microsoft/PowerToys/releases/).
2. Browse the list of stable and experimental versions of PowerToys that are available.
3. Select the **Assets** drop-down menu to display the files for the release.
4. Select the `PowerToysSetup-0.##.#-x64.exe` file to download the PowerToys executable installer.
5. Once downloaded, open the executable file and follow the installation prompts.

## Requirements

- Windows 10 1803 (build 17134) or later.
- [.NET Core 3.1 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.4-windows-x64-installer). The PowerToys installer will handle this requirement.
- x64 architecture currently supported. ARM and x86 support to become available at a later date.

To ensure that your machine meets these requirements, check your Windows 10 version and build number by selecting the **⊞ Win** *(Windows key)* + **R**, then type **winver**, select **OK**. (Or enter the `ver` command in Windows Command Prompt). You can [update to the latest Windows version](ms-settings:windowsupdate) in the **Settings** menu.

## Alternative Install Methods

<!--  - **[Windows executable .exe file](#install-with-windows-executable-file)** *(Recommended)* -->
- [Windows Package Manager](#install-with-windows-package-manager-preview) *(Preview)*
- [Community-driven install tools](#community-driven-install-tools) *(Not officially supported)*

## Install with Windows Package Manager (Preview)

To install PowerToys using the Windows Package Manager (WinGet) preview:

1. Download PowerToys from [Windows Package Manager](https://github.com/microsoft/winget-cli/releases).
2. Run the following command from the command line / PowerShell:

```powershell
WinGet install powertoys
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
scoop install powertoys
```

To update PowerToys, run the following command from the command line / PowerShell:

```powershell
scoop update powertoys
```

If you have issues when installing/updating, file an issue in the [Scoop repo on GitHub](https://github.com/lukesampson/scoop/issues).
