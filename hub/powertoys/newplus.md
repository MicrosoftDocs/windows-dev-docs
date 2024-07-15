---
title: PowerToys New+ for Windows
description: A tool that enables you to create files and folders from an easily personalized set of templates, directly from the File Explorer context menu.
ms.date: 07/13/2024
ms.topic: article
no-loc: [PowerToys, Windows, New+, New, NewPlus, Win]
---

# New+

**PowerToys New+** enables you to create files and folders from a personalized set of templates, directly from the File Explorer context menu.

## Getting started

### How to enable New+

To start using New+, enable New+ in the PowerToys settings.

### How to create a new object using New+

To create a new item within a folder, right-click on the folder to bring up the context menu. From there, click on the “New+” option and then select the template you were looking for.

### How to add or customize a new template to New+

To create a new template, start by right-clicking on the folder. This will open a context menu where you can select the ‘New+’ option. From there, choose ‘Open templates’ to access the “Templates” folder. In this folder, you have the freedom to add, edit, or rename objects as per your needs. It’s important to note that the objects you add here will be displayed on the ‘New+’ menu in a sorted order, with folders always appearing first. This provides the ability to find and select your templates.

## Settings

### <a name="template_location"></a>Templates

#### Templates location

After the enablement toggle, the New+ Templates location setting is likely the most interesting one. By default, the template location is in your local app data folder, specifically at %localappdata%\Microsoft\PowerToys\NewPlus\Templates. However, these templates will not roam with you across devices. If you want a common set of templates across devices, a popular option is to change the template location to a folder that is synced with a cloud drive, such as OneDrive. This way, you can access your templates from any device.

### Display options

#### Hide template filename extensions

The option enables you to toggle the display of filename extensions. When this option is toggled off, a file named “filename.ext” will be displayed with its extension, appearing as “filename.ext”. However, when this option is toggled on (the default), the template will be displayed without its extension, appearing simply as “filename”.

#### Hide template filename starting digits, spaces and dots

The option enables you to toggle the display of starting digits, spaces and dots. When this option is toggled off (the default), a file named “1. filename” will be displayed as is. However, when this option is toggled on, the template will be displayed as “filename”.
This is useful when using digits, spaces and dots at the beginning of filenames to control the display order of templates.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
