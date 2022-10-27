---
title: PowerToys File Locksmith utility for Windows
description: A Windows shell extension for checking which files are in use and by which processes.
ms.date: 10/26/2022
ms.topic: article
no-loc: [PowerToys, Windows, File Locksmith, Win]
---

# File Locksmith utility

File Locksmith is a Windows shell extension for checking which files are in use and by which processes. After installing PowerToys, right-click on one or more selected files in File Explorer, and then select **What's using this file?** from the menu.

![File Locksmith Demo.](../images/powertoys-file-locksmith.gif)

## Activation and use

To activate File Locksmith, select one or more files or directories in File Explorer. If a directory is selected, all of its subfiles and subdirectories will be scanned as well. When File Locksmith is activated, it will scan all running processes which it can access and check which files they are using. Processes that are being run by a different user cannot be accessed and may be missing from the list of results. To scan all processes, select the **Restart as administrator** button.

![Restart File Locksmith as administrator.](../images/powertoys-file-locksmith-restart-as-admin.png)

After scanning, a list of processes will be displayed. You can select the **End task** button to terminate the process, or select the expander to show more information.
File Locksmith will automatically remove terminated processes from the list, whether or not this action was done from File Locksmith. To manually refresh the list of processes, select the **Reload** button.
