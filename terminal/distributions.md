---
title: Windows Terminal Distribution Types
description: Learn how to use the different distributions of Windows Terminal.
ms.date: 02/03/2025
ms.topic: how-to 
---

# Windows Terminal Distribution Types

Windows Terminal is distributed via [GitHub releases] in a variety of formats:

- Packaged, or "MSIX bundle"
    - This is the oldest and best-supported distribution of Windows Terminal.
    - The packaged distribution can be installed via the `.msixbundle` file provided on the [GitHub releases] page or
      through the Microsoft Store ([Stable](https://aka.ms/terminal), [Preview](https://aka.ms/terminal-preview)).
    - Installation via MSIX bundle may require network connectivity to download dependency packages from the Store.
    - When installed via MSIX bundle, Terminal will receive automatic updates through the Store.
- Preinstallation Kit
    - A [preinstallation kit] is available for system integrators and OEMs interested in preinstalling Windows Terminal
      on a Windows image.
    - More information is available in the [DISM documentation on preinstallation]. Users who do not intend to
      preinstall Windows Terminal should continue using the Packaged distribution.
    - When installed via preinstallation kit, Terminal will receive automatic updates through the Store.
- Unpackaged, or "ZIP" (new in 1.17 stable)
    - This distribution method was not officially supported until stable channel version 1.17.
    - The unpackaged distribution does not receive automatic updates, which puts you in control of exactly when new
      versions are installed.
- Portable
    - A variant of the unpackaged distribution, where Terminal stores its settings in a nearby directory.
    - [Learn more about configuring Portable mode.](#windows-terminal-portable)

## Distribution feature comparison

|                                            | Packaged                 | Preinstallation Kit | Unpackaged       | Portable                      |
| ------------------------------------------ | ------------------------ | ------------------- | ---------------- | ----------------------------- |
| **Automatic updates**                      | ✅                       | ✅                  | ❌               | ❌                            |
| **Automatic architecture selection**       | ✅                       | ✅                  | ❌               | ❌                            |
| **Can be set as your default terminal**    | ✅                       | ✅                  | ❌               | ❌                            |
| **"Open in Terminal" context menu**        | ✅                       | ✅                  | ❌               | ❌                            |
| **Automatic start on login option**        | ✅                       | ✅                  | _manual_         | _manual_                      |
| **Double-click installation**              | ✅                       | ❌                  | ❌               | ❌                            |
| **Installation on non-networked machines** | ❌                       | ✅                  | ✅               | ✅                            |
| **Preinstallation in a Windows image**     | ❌                       | ✅                  | _as plain files_ | _as plain files_              |
| **User-controlled installation path**      | ❌                       | ❌                  | ✅               | ✅                            |
| **Double-click activatable**               | ❌                       | ❌                  | ✅               | ✅                            |
| **Settings storage location**              | User folder, per package | (same as packaged)  | `%LOCALAPPDATA%` | Next to `WindowsTerminal.exe` |

## Windows Terminal Portable

Windows Terminal supports being deployed in ["Portable mode"]. Portable mode ensures
that all data created and maintained by Windows Terminal is saved next to the application so that it can be more easily
moved across different environments.

Portable mode is supported by the unpackaged "ZIP" distribution.

This is an officially-supported mode of execution where Windows Terminal stores its settings in a `settings` folder next
to `WindowsTerminal.exe`.

Portable mode is not supported in the packaged or preinstallation kit distributions of Windows Terminal.

Portable mode will only run on Windows 10 version 2004 (10.019041) or higher. 

### Why use Portable mode?

The unpackaged and portable mode distributions of Windows Terminal allow you to use Terminal without installing it
globally, e.g. on systems where you may not have permission to install MSIX packages or download software from the
Microsoft Store.

Portable mode allows you to carry around or archive a preconfigured installation of Windows Terminal and run it from
a network share, cloud drive or USB flash drive. Any such installation is self-contained and will not interfere with
other installed distributions of Windows Terminal.

### Enabling Portable mode

Portable mode needs to be enabled manually. After unzipping the Windows Terminal download, create a file named `.portable` next to `WindowsTerminal.exe`.

> [!NOTE]
> Windows Terminal will not automatically reload its settings when you create the portable mode marker file.
> This change will only apply after you relaunch Terminal.

Windows Terminal will automatically create a directory named `settings` in which it will store both settings and runtime
state such as window layouts.

![Windows Terminal portable mode disclaimer example](./images/portable-mode.png)

### Disabling Portable mode

You can restore Portable mode unpackaged installation to its original configuration, where settings are stored in
`%LOCALAPPDATA%\Microsoft\Windows Terminal`, by removing the `.portable` marker file from the directory containing
`WindowsTerminal.exe`.

If you wish to reenable portable mode, you can create a new `.portable` marker file next to `WindowsTerminal.exe`.

### Upgrading a Portable mode Install

You can upgrade a portable mode installation of Windows Terminal by moving the `.portable` marker file and the
`settings` directory to a newly-extracted unpackaged version of Windows Terminal.

### Portable mode FAQs

#### Why don't ms-appdata URLs work in Portable mode?
Prior to portable mode, a common practice to reference images in `settings.json` would be to use `ms-appdata:///Local`. 

Portable mode offers a self-contained Terminal installation, where user data and application data are stored in the same place. As there is no separate user data folder, references to such folder (e.g. with `ms-appdata`) will not work.

To refer to paths relative to the application install directory, use an `ms-appx:` URL.

To refer to paths relative to the settings directory, use the environment variable `%WT_SETTINGS_DIR%`.

["Portable mode"]: https://en.wikipedia.org/wiki/Portable_application
[GitHub releases]: https://github.com/microsoft/terminal/releases
[preinstallation kit]: /windows/msix/desktop/deploy-preinstalled-apps
[DISM documentation on preinstallation]: /windows-hardware/manufacture/desktop/preinstall-apps-using-dism
