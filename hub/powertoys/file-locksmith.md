---
title: File Locksmith Utility for Windows - PowerToys
description: Learn how to use File Locksmith, a PowerToys utility that checks which files are in use and identifies blocking processes in Windows. Unlock files easily with this shell extension.
ms.date: 10/14/2026
ms.topic: how-to
no-loc: [PowerToys, Windows, File Locksmith, Win]
# Customer intent: Learn how to configure and use the File Locksmith utility in PowerToys.
---

# File Locksmith utility

File Locksmith is a PowerToys utility that helps you identify which processes are using specific files or directories in Windows. This shell extension allows you to easily unlock files that are being used by other processes, making file management more efficient.

:::image type="content" source="images/file-locksmith/file-locksmith.gif" alt-text="An animated GIF of File Locksmith utility interface showing process list and file usage information.":::

## How to activate and use File Locksmith

To activate File Locksmith, open PowerToys and turn on the **Enable File Locksmith** toggle. Select one or more files or directories in Windows File Explorer. If a directory is selected, all of its files and subdirectories will be scanned as well.

To open File Locksmith to see which processes are using one or more file(s), right-click on the selected file(s), select **Show more options** to expand the list of menu options, then select **Unlock with File Locksmith**.

When File Locksmith is opened, it will scan all of the running processes that it can access, checking which files the processes are using. Processes that are being run by a different user cannot be accessed and may be missing from the list of results. To scan all processes, select **Restart as administrator**.

:::image type="content" source="images/file-locksmith/restart-as-admin.png" alt-text="Screenshot of File Locksmith restart as administrator button for accessing all processes.":::

After scanning, a list of processes will be displayed. Select **End task** to terminate the process, or select the expander to show more information. File Locksmith will automatically remove terminated processes from the list, whether or not this action was done via File Locksmith. To manually refresh the list of processes, select **Reload**.

## Command-line reference

The File Locksmith CLI lets you identify and manage processes that are locking files from the command line. PowerToys adds its `bin` folder to your `PATH`, so you can run `PowerToys.FileLocksmith.CLI.exe` from any folder in Windows Terminal, PowerShell, or Command Prompt. If a terminal was already open when you installed PowerToys, open a new one so it picks up the updated `PATH`.

| Command | Description |
| :--- | :--- |
| `<path>` | **Required**. One or more file or directory paths to check. You can specify multiple paths separated by spaces. |
| `--kill` | Terminates (kills) all processes that are currently locking the specified files. |
| `--json` | Outputs the results in structured **JSON** format instead of human-readable text. Useful for automation and scripts. |
| `--wait` | **Blocks execution** and waits until the specified files are released. Unless you also specify `--timeout`, the command will not exit until the files are unlocked. |
| `--timeout <milliseconds>` | Use with `--wait` to stop waiting after the specified number of milliseconds. If the files are still locked when the timeout expires, the command returns exit code `1`. |
| `--help` | Displays the help message with usage instructions. |

**Usage example**

```powershell
# Check which processes are locking a specific file:
PowerToys.FileLocksmith.CLI.exe "C:\Users\Docs\report.docx"

# Check multiple files and get the output in JSON format for parsing:
PowerToys.FileLocksmith.CLI.exe --json "C:\File1.txt" "C:\Folder\File2.dll"

# Block script execution until a file is released (useful in build scripts):
PowerToys.FileLocksmith.CLI.exe --wait "C:\bin\output.exe"

# Wait up to 30 seconds for a file to be released:
PowerToys.FileLocksmith.CLI.exe --wait --timeout 30000 "C:\bin\output.exe"

# Kill all processes that are locking a specific file:
PowerToys.FileLocksmith.CLI.exe --kill "C:\LockedFile.dat"
```

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
