---
title: Debugging and troubleshooting issues with the winget tool
description: Provides information on logging and winget diagnostics.
ms.date: 10/01/2021
ms.topic: article
ms.localizationpriority: medium
---

# Debugging and troubleshooting issues with the winget tool

When Windows Package Manager is installing, searching or listing applications, sometimes it is necessary to look at the log files to understand the behavior better.

## Logs

Windows Package Manager by default creates log files when executing commands. These log files are located here:

```CMD
> Logs: %LOCALAPPDATA%\Packages\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\LocalState\DiagOutputDir
```

By navigating to this folder you will find the logs the *winget* tool has written.

### --verbose-logs

If you need more comprehenive log files, that provide the complete communication with the CDNs and sources, include **--verbose-logs** on the command line as well.  Here are some examples of using the **--verbose-logs** option:

```CMD
> winget install vscode --verbose-logs
> winget search -n visual --verbose-logs
> winget source add -n mysource -t Microsoft.REST -a https://www.contoso.org --verbose-logs
```

## Known issues

A list of known issues with sources and behaviors is kept up to date in the [Windows Package Manager Client repository](https://www.github.com/microsoft/winget-cli).  If you encounter issues when using the winget tool, go [here](https://github.com/microsoft/winget-cli/tree/master/doc/troubleshooting) for troubleshooting.
