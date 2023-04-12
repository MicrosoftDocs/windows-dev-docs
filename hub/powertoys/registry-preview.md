---
title: PowerToys Registry Preview for Windows
description: Registry Preview is a utility to visualize and edit Windows Registry files.
ms.date: 03/30/2023
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, Registry Preview, Win]
---

# Registry Preview

PowerToys **Registry Preview** simplifies the process of visualizing and editing complex Windows Registry files. It also allows you to write registry changes to the Windows Registry.


![Registry Preview screenshot.](../images/pt-registrypreview.png)

## Getting started

### Enable

To start using Registry Preview, enable it in the PowerToys Settings (Registry Preview section).

### How to activate

Select one or more .reg files in Windows File Explorer. Right-click on the selected file(s), choose **Show more options** from the menu to expand your list of menu options, then select **Preview** to open Registry Preview. **Registry Preview** can also be launched from PowerToys Settings' Registry Preview section.

**Note:** Currently, there is a 10MB file limit for opening Windows Registry files with **Registry Preview**. If a file contains invalid content, **Registry Preview** will show the appropriate message. 

## How to use

After opening a Windows Registry file, the file content is shown on the left side of the **Registry Preview**. This content can be updated at any time.

**Visual tree**: In top-right area of the **Registry Preview** window, you will find a visual tree representation of the registry keys listed in the file. This visual tree will be automatically updated on each file content change inside the app.

**Registry key values**: Select a specific registry key in the visual tree for the values of that registry key to display in the bottom-right area of the **Registry Preview** window.

**Edit file...**: Select `Edit file...` to open the file in the `Notepad` app.

**Reload from file**: Select `Reload from file` to reload file content if the loaded file is being edited outside of the **Registry Preview**.

**Write to Registry**: Select `Write to Registry` to save any changes listed in the loaded file to the Windows Registry. The Windows Registry Editor will open and ask for confirmation before writing changes. The User Account Control (UAC) prompt, a Windows security feature designed to mitigate the impact of malware, will appear and need approval to open Windows Registry Editor.
