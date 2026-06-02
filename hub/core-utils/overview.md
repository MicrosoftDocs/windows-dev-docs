---
title: Coreutils for Windows overview
description: A set of UNIX-style core utilities for Windows that lets developers run the same commands and scripts they already use on Linux, macOS, and WSL.
ms.topic: overview
ms.date: 05/14/2026
---

# Coreutils for Windows

[Coreutils for Windows](https://github.com/microsoft/coreutils) is a Microsoft-maintained set of UNIX-style command-line utilities that run natively on Windows — the same commands and pipelines you use on Linux, macOS, and WSL. It ships as a single multi-call binary that exposes each utility under its standard name (`cat.exe`, `grep.exe`, `find.exe`, and so on), giving you the everyday tools developers already use on other platforms to script, automate, and process text. For the full list, see [Commands](commands.md).

The goal is to remove friction when moving between Linux, macOS, WSL, containers, and Windows. The same commands, flags, and pipelines work the same way, so existing scripts and habits carry over without translation. Each command supports the standard `--help` flag for full syntax and options.

:::image type="content" source="images/core-utils.png" alt-text="Screenshot of Coreutils for Windows running in a terminal." border="false":::

<div class="buttons margin-top-xs margin-bottom-sm">
    <a class="button button-sm button-filled button-primary" href="https://github.com/microsoft/coreutils" target="_blank" rel="noopener">
        <span class="icon" aria-hidden="true"><span class="docon docon-brand-github"></span></span>
        <span>Coreutils for Windows on GitHub</span>
    </a>
</div>

## Install

Install Coreutils with [WinGet](/windows/package-manager/):

```powershell
winget install Microsoft.Coreutils
```

Or [download from GitHub](https://github.com/microsoft/coreutils/releases/latest).

## How it works

The utilities are implemented in Rust on top of the [uutils/coreutils](https://github.com/uutils/coreutils) project — the same cross-platform reimplementation of GNU coreutils that ships in modern Linux distributions. Microsoft maintains a Windows-focused build that bundles `coreutils`, `findutils` (`find`, `xargs`), and a GNU-compatible `grep` together as a single package. It also includes integrated ports of the original DOS `sort` and `find`, so existing CMD scripts that rely on `/switch`-style syntax keep working alongside the UNIX-style versions. For details, see [Shell conflicts](https://github.com/microsoft/coreutils#shell-conflicts).

## Related content

- [Windows Subsystem for Linux](/windows/wsl/)
- [Windows Terminal](/windows/terminal/)
- [Sudo for Windows](../advanced-settings/sudo/index.md)
- [WinGet](/windows/package-manager/)
