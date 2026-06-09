---
title: PowerToys Crop And Lock for Windows
description: Crop And Lock to crop a current application into a smaller window or just create a thumbnail.
ms.date: 08/20/2025
ms.topic: concept-article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, Crop And Lock, Win]
# Customer intent: Learn about the Crop And Lock feature in PowerToys for Windows.
---

# Crop And Lock

**Crop And Lock** is a PowerToys utility that helps you focus on specific parts of your applications by creating smaller, cropped windows. You can create live thumbnails that mirror changes from the original window, or extract portions of applications into standalone windows for better multitasking and screen real estate management.

This feature is particularly useful when you need to monitor specific areas of applications while working with other programs, or when you want to create custom window layouts that better suit your workflow.

:::image type="content" source="images/crop-and-lock/crop-and-lock.gif" alt-text="Animation of the PowerToys Crop And Lock utility demonstrating window cropping and thumbnail features.":::

## Get started with Crop And Lock

### How to use the utility

To start using Crop And Lock, enable it in PowerToys Settings.

Once enabled, focus a Window and press the "Thumbnail" shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd>) or the "Reparent" shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>R</kbd>) to select an area of the window to crop.

> [!TIP]
> Use <kbd>Esc</kbd> to cancel the crop selection.

After you've selected the area of the window, a new window will appear and behave according to the selected crop mode.

Select the **Close** button of the cropped window to close it and restore the original window.

## Crop modes

### Thumbnail

Creates a window that shows the selected area of the original window. Any changes to the original window's selected area will be reflected on the thumbnail, but the original application can't be controlled through the thumbnail. This mode has the best compatibility across apps.

### Reparent

Creates a window that replaces the original window, showing only the selected area. The application will now be controlled through the cropped window. Closing the cropped window will restore the original window.

Not every window will react well to being contained in another application so this mode has many compatibility issues. It's advisable to use the "Thumbnail" mode instead if you find that a window isn't responding well to being cropped with the "Reparent" mode.

### Screenshot

Creates a window that shows the selected area of the original window as a frozen snapshot. This mode has good compatibility across apps as well.

## Known issues

Crop And Lock currently has the following known issues:

- Cropping maximized or full-screen windows in "Reparent" mode might not work. It's recommended to resize the window to fill the screen corners instead.
- Some UWP apps won't react well to being cropped in "Reparent" mode. Windows Calculator is a notable example of this.
- Applications that use sub-windows or tabs can react poorly to being cropped in "Reparent" mode. Notepad and OneNote are notable examples of applications that react poorly.

## Settings

The following settings are available for the Crop And Lock utility:

| Setting | Description |
| :--- | :--- |
| **Thumbnail shortcut** | The customizable keyboard command to activate the thumbnail mode for cropping and creating a thumbnail view. |
| **Reparent shortcut** | The customizable keyboard command to activate the reparent mode for cropping and reparenting to a new window. |
| **Screenshot shortcut** | The customizable keyboard command to create a cropped, static screenshot of another window. Unlike the thumbnail mode, the screenshot does not update with the original window's content. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
