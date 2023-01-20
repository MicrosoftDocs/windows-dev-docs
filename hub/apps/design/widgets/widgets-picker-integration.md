---
author: drewbatgit
description: Learn about the design elements required for integrating into the widget picker on the Widgets Board.
title: Integrate with the widget picker
ms.author: drewbat
ms.date: 01/19/2023
ms.topic: article
keywords: windows 11, widgets
ms.localizationpriority: medium
---

# Integrate with the widget picker

In the current release, the only Widgets host is the Widgets Board built into Windows 11. The Widgets Board displays widgets and manages their layout the on the board. It also provides a widget picker that allows the user to select which available widgets are visible on the board. This article describes the assets required for a widget to successfully integrate into the widget picker.

## Widget screenshot image

Each widget must provide a screenshot image that is displayed as a preview in the widget picker when the widget has focus. The screenshot is specified by the widget provider in the package manifest for the app. For more technical information on how to specify an image file to use for the widget screenshot, see [Implement a widget provider in a win32 app](../../develop/widgets/implement-widget-provider-win32.md) and [Widget provider package manifest XML format](../../develop/widgets/widget-provider-manifest.md).

:::image type="content" source="images/widgets-picker-screenshot.png" alt-text="Screenshot of the Widgets Board. The widget picker is active and a widget screenshot image is being displayed.":::

## Screenshot image requirements 

In order to provide a consistent user experience, widget screenshots for the widget picker must follow the following guidelines.

* The screenshot must show the the light-themed, medium-sized version of your widget.
* The image should be 300 pixels wide and 304 pixels tall.
* The image should have transparent, rounded corners.
* If your widget supports right-to-left (RTL) languages, you should provide a separate image for those languages that uses an RTL design.

If your app doesn't provide at least a light-themed, medium-sized screenshot, then a default widget placeholder image as the preview for your widget instead. Note that the screenshot element of the widget manifest is required. If you don't provide any screenshot image, your widget will not be displayed in the picker.

The following is an example of a widget screenshot image. 

:::image type="content" source="images/widgets-example-screenshot.png" alt-text="An example of a widget screenshot image.":::

