---
title: PowerToys File Locksmith utility for Windows
description: A Windows shell extension for checking which files are in use and by which processes.
ms.date: 11/19/2024
ms.topic: article
no-loc: [PowerToys, Windows, File Locksmith, Win]
# Customer intent: Learn how to configure and use the File Locksmith utility in PowerToys.
---

# File Locksmith utility

File Locksmith is a Windows shell extension for checking which files are in use and by which processes.

![File Locksmith Demo](../images/powertoys-file-locksmith.gif)

## How to activate and use File Locksmith

To activate File Locksmith, open PowerToys and turn on the **Enable File Locksmith** toggle. Select one or more files or directories in Windows File Explorer. If a directory is selected, all of its files and subdirectories will be scanned as well.

To open File Locksmith to see which processes are using one or more file(s), right-click on the selected file(s), select **Show more options** to expand the list of menu options, then select **Unlock with File Locksmith**.

When File Locksmith is opened, it will scan all of the running processes that it can access, checking which files the processes are using. Processes that are being run by a different user cannot be accessed and may be missing from the list of results. To scan all processes, select **Restart as administrator**.

![Restart File Locksmith as administrator](../images/powertoys-file-locksmith-restart-as-admin.png)

After scanning, a list of processes will be displayed. Select **End task** to terminate the process, or select the expander to show more information. File Locksmith will automatically remove terminated processes from the list, whether or not this action was done via File Locksmith. To manually refresh the list of processes, select **Reload**.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
