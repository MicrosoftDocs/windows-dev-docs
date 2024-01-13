---
title: PowerToys Registry Preview for Windows
description: Registry Preview is a utility to visualize and edit Windows Registry files.
ms.date: 08/03/2023
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, Registry Preview, Win]
---

# Registry Preview

PowerToys **Registry Preview** simplifies the process of visualizing and editing complex Windows Registry files. It can also write changes to the Windows Registry.

![Registry Preview screenshot.](../images/pt-registrypreview.png)

## Getting started

### Enabling Registry Preview

To start using Registry Preview, enable it in the PowerToys Settings (**Registry Preview** tab).

### Activating Registry Preview

Select one or more .reg files in Windows File Explorer. Right-click on the selected file(s), select **Show more options** from the menu to expand the list of menu options, then select **Preview** to open Registry Preview. **Registry Preview** can also be opened from PowerToys Settings' **Registry Preview** tab.

**Note:** Currently, there is a 10MB file limit for opening Windows Registry files with Registry Preview. It will show a message if a file contains invalid content.

## Using Registry Preview

After opening a Windows Registry file, the file content is shown. This content can be updated at any time.

On the other side there is a visual tree representation of the registry keys listed in the file. This visual tree will be automatically updated on each file content change inside the app.

Select a specific registry key in the visual tree to see the values of that registry key below it.

Select **Edit** to open the file in Notepad.

Select **Reload** to reload file content if the file is changed outside of the Registry Preview.

Select **Write to Registry** to save any changes listed in the Preview to the Windows Registry. The Windows Registry Editor will open and ask for confirmation before writing data.

Select **Open key** to open the Windows Registry Editor with whatever key you have highlighted in the treeview as the initial starting point.
