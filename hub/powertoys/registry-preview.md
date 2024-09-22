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

![Registry Preview screenshot](../images/pt-registrypreview.png)

## Getting started

### Enable

To start using Registry Preview, enable it in the PowerToys Settings.

### How to activate

Select one or more .reg files in Windows File Explorer. Right-click on the selected file(s) and select **Preview** to open Registry Preview. On Windows 11: select **Show more options** to expand the list of menu options. **Registry Preview** can also be opened from PowerToys Settings.

**Note:** Currently, there is a 10MB file limit for opening Windows Registry files with Registry Preview. It will show a message if a file contains invalid content.

## How to use

After opening a Windows Registry file, the content is shown. This content can be updated at any time.

On the other side there is a visual tree representation of the registry keys listed in the file. This visual tree will be updated on each file content change inside the app.

Select a registry key in the visual tree to see the values of that registry key below it.

Select **Edit** to open the file in Notepad.

Select **Reload** to reload file content in case the file is changed outside of the Registry Preview.

Select **Write to registry** to save the data listed in the Preview to the Windows Registry. The Windows Registry Editor will ask for confirmation before writing data.

Select **Open key** to open the Windows Registry Editor with the location of the key you have highlighted in the treeview as the initial starting point.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
