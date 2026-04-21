---
title: File Locksmith Utility for Windows - PowerToys
description: Learn how to use File Locksmith, a PowerToys utility that checks which files are in use and identifies blocking processes in Windows. Unlock files easily with this shell extension.
ms.date: 08/20/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, File Locksmith, Win]
# Customer intent: Learn how to configure and use the File Locksmith utility in PowerToys.
---

# File Locksmith utility

File Locksmith is a PowerToys utility that helps you identify which processes are using specific files or directories in Windows. This shell extension allows you to easily unlock files that are being used by other processes, making file management more efficient.

:::image type="content" source="images/powertoys-file-locksmith.gif" alt-text="An animated GIF of File Locksmith utility interface showing process list and file usage information.":::

## How to activate and use File Locksmith

To activate File Locksmith, open PowerToys and turn on the **Enable File Locksmith** toggle. Select one or more files or directories in Windows File Explorer. If a directory is selected, all of its files and subdirectories will be scanned as well.

To open File Locksmith to see which processes are using one or more file(s), right-click on the selected file(s), select **Show more options** to expand the list of menu options, then select **Unlock with File Locksmith**.

When File Locksmith is opened, it will scan all of the running processes that it can access, checking which files the processes are using. Processes that are being run by a different user cannot be accessed and may be missing from the list of results. To scan all processes, select **Restart as administrator**.

:::image type="content" source="images/powertoys-file-locksmith-restart-as-admin.png" alt-text="Screenshot of File Locksmith restart as administrator button for accessing all processes.":::

After scanning, a list of processes will be displayed. Select **End task** to terminate the process, or select the expander to show more information. File Locksmith will automatically remove terminated processes from the list, whether or not this action was done via File Locksmith. To manually refresh the list of processes, select **Reload**.

## Command-line reference

The File Locksmith CLI lets you identify and manage processes that are locking files from the command line.

| Command | Description |
| :--- | :--- |
| `<path>` | **Required**. One or more file or directory paths to check. You can specify multiple paths separated by spaces. |
| `--kill` | Terminates (kills) all processes that are currently locking the specified files. |
| `--json` | Outputs the results in structured **JSON** format instead of human-readable text. Useful for automation and scripts. |
| `--wait` | **Blocks execution** and waits until the specified files are released. The command will not exit until the files are unlocked. |
| `--help` | Displays the help message with usage instructions. |

**Usage example**
```powershell
# Check which processes are locking a specific file:
FileLocksmithCLI.exe "C:\Users\Docs\report.docx"

# Check multiple files and get the output in JSON format for parsing:
FileLocksmithCLI.exe --json "C:\File1.txt" "C:\Folder\File2.dll"

# Block script execution until a file is released (useful in build scripts):
FileLocksmithCLI.exe --wait "C:\bin\output.exe"

# Kill all processes that are locking a specific file:
FileLocksmithCLI.exe --kill "C:\LockedFile.dat"
```

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
