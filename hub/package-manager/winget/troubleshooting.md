---
title: Debugging and troubleshooting issues with WinGet
description: Provides information on logging and WinGet diagnostics.
ms.date: 07/19/2026
ms.topic: troubleshooting-general
no-loc: [winget, install, list, search, settings, source, --info, --logs, --open-logs, --verbose, --verbose-logs]
---

# Debugging and troubleshooting issues with the WinGet tool

If WinGet does not appear to be installed correctly, follow these steps from a PowerShell command prompt: 

```PowerShell
Install-PackageProvider -Name NuGet -Force | Out-Null
Install-Module -Name Microsoft.WinGet.Client -Force -Repository PSGallery | Out-Null
Repair-WinGetPackageManager -Force -Latest
```

When WinGet commands are failing, sometimes it is necessary to look at the log files to better understand the behavior.

## WinGet Logs

Windows Package Manager by default creates log files when executing commands. These logs contain information that can aid in debugging issues with WinGet. Log file cleanup behavior is configurable via the [logging settings](./settings.md#logging-settings) in your settings file. By default, log files older than 7 days or exceeding 128 MB total are automatically removed, and individual log files wrap at 16 MB. Use the `logging.file` settings to adjust these limits.

Use the command [`winget --info`](./info.md) to find the directory path to your WinGet log files. The default path for WinGet log files is:

```CMD
%LOCALAPPDATA%\Packages\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\LocalState\DiagOutputDir
```

You can include the **--logs** or **--open-logs** option to any command to open the logs directory after the command completes. Here are some examples of using the **--logs** option:

```CMD
> winget list --logs
> winget source update --open-logs
```

### --verbose-logs

If you need more comprehensive log files, that provide the complete communication with the CDNs and sources, include **--verbose** or **--verbose-logs** on the command line as well.  Here are some examples of using the **--verbose-logs** option:

```CMD
> winget install vscode --verbose-logs
> winget search -n visual --verbose-logs
> winget source add -n mysource -t Microsoft.REST -a https://www.contoso.org --verbose
```

### settings

You can specify the default logging level for WinGet to use in your WinGet Settings file. The **settings** command will open the settings.json file in your default JSON editor.

Example with verbose logging:
```JSON
{
    "$schema": "https://aka.ms/winget-settings.schema.json",
    "logging": {
        "level": "verbose"
    }
}
```

## Known issues

A list of known issues with sources and behaviors is kept up to date in the [Windows Package Manager Client repository](https://www.github.com/microsoft/winget-cli).  If you encounter issues when using the WinGet tool, go [here](https://github.com/microsoft/winget-cli/tree/master/doc/troubleshooting) for troubleshooting.

## Exit codes

The WinGet tool returns exit codes to indicate success or failure of the command.  Find a table of exit codes and their meanings in the ["Return codes" file of the Windows Package Manager Client repository](https://github.com/microsoft/winget-cli/blob/master/doc/windows/package-manager/winget/returnCodes.md).

The WinGet **error** command accepts errors from "Exit codes" and displays a description for known error codes for WinGet, MSIX, and MSI installers. Many .exe-based installers have non-standard error codes and may not be displayed.

```CMD
> winget error 1603
```

### Scope for specific user vs machine-wide

Not all installers support installing in “user” scope vs. “machine” scope consistently.

- [MSIX-based packages](/windows/msix/overview): Reliable WinGet behavior.
- [MSI-based packages](/windows/win32/msi/installation-package) typically support reliable WinGet configurations, but in some cases, are nested inside an .exe-based installer so there may be more variability.
- [EXE-based installers](https://stackoverflow.com/questions/3886455/whats-the-difference-between-an-exe-and-a-msi-installer) behavior around scope is not necessarily deterministic. In some cases the arguments to specify scope are not available, and in other cases the installer may make the determination based on whether the user is a member of the local administrators group. Packages installed in user scope may still require UAC (User Account Control) authorization from an administrator.

See more details on [scope-related issues](https://github.com/microsoft/winget-cli/issues?q=is%3Aissue+is%3Aopen+label%3Aarea-scope) in the WinGet product repository on GitHub.

### 403 Forbidden error

A 403 Forbidden error may occur when attempting to download a package using the WinGet tool. This issue can arise if an Independent Software Vendor (ISV) opts not have their product distributed by a package manager service like WinGet.

The server responsible for initiating the download typically checks for a user agent string included with the download request to identify the device or client (e.g., browser, WinGet). If you can download the installer using your browser, but encounter issues with WinGet, it is possible that the ISV has blocked the WinGet user agent string.

The user agent string for WinGet has the following format:

`winget-cli WindowsPackageManager/{Client Version} DesktopAppInstaller/Microsoft.DesktopAppInstaller {AppInstaller Version}`

Example: 

`winget-cli WindowsPackageManager/1.9.25200 DesktopAppInstaller/Microsoft.DesktopAppInstaller v1.24.25200.0`

### System Context

WinGet is delivered via the App Installer as a packaged application. MSIX (packaged) applications depend on the package being registered for the user. As packages can be registered for any user except NT AUTHORITY\SYSTEM (aka LocalSystem, aka System), the WinGet CLI is not supported in the system context. The Microsoft.WinGet.Client PowerShell module can be used in the system context with applications that are installed machine wide.
