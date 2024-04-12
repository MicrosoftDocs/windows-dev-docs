---
title: PowerToys Crop And Lock for Windows
description: Crop And Lock to crop a current application into a smaller window or just create a thumbnail.
ms.date: 09/08/2023
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, Crop And Lock, Win]
---

# Crop And Lock

PowerToys **Crop And Lock** allows you to crop a current application into a smaller window or just create a thumbnail. Focus the target window and press the shortcut to start cropping.

![Crop And Lock screenshot](../images/powertoys-crop-and-lock.gif)

## Getting started

### How to use

To start using Crop And Lock, enable it in the PowerToys Settings (**Crop And Lock** tab).

Once enabled, focus a Window and press the "Thumbnail" shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd>) or the "Reparent" shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>R</kbd>) to select an area of the window to crop.

> [!TIP]
> Use <kbd>Esc</kbd> to cancel the crop selection.

After you've selected the area of the window, a new window will appear and behave according to the selected crop mode.

Select the **Close** button of the cropped window to close it and restore the original window.

## Crop modes

### Thumbnail

Creates a window that shows the selected area of the original window. Any changes to the original window's selected area will be reflected on the thumbnail, but the original application can't be controlled through the thumbnail. This mode has the most compatibility with other applications.

### Reparent

Creates a window that replaces the original window, showing only the selected area. The application will now be controlled through the cropped window. Closing the cropped window will restore the original window. Not every window will react well to being contained on another application so this mode has many compatibility issues. It's advisable to use the "Thumbnail" mode instead if you find that a windows isn't reacting well to being cropped with the "Reparent" mode.

## Known issues

- Cropping maximized or full-screen windows in "Reparent" mode might not work. It's recommended to resize the window to fill the screen corners instead.
- Some UWP apps won't react well to being cropped in "Reparent" mode. The Windows Calculator is a notable example of this.
- Applications that use sub-windows or tabs can react poorly to being cropped in "Reparent" mode. Notepad and OneNote are notable examples of applications that react poorly.
