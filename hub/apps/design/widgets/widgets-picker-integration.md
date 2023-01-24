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

Each widget must provide a screenshot image that is displayed as a preview in the widget picker when the widget has focus. The screenshot is specified by the widget provider in the package manifest for the app. For technical information on how to specify an image file to use for the widget screenshot, see [Implement a widget provider in a win32 app](../../develop/widgets/implement-widget-provider-win32.md) and [Widget provider package manifest XML format](../../develop/widgets/widget-provider-manifest.md).

The following screenshot illustrates the placement of the screenshot image within the widget picker.

![Screenshot of the Widgets Board. The widget picker is active and a widget screenshot image is being displayed.](images/widgets-picker-screenshot.png)

## Screenshot image requirements 

In order to provide a consistent user experience, widget screenshots for the widget picker must follow the following guidelines.

* The screenshot must show the the light-themed, medium-sized version of your widget.
* The image should be 300 pixels wide and 304 pixels tall.
* The image should have transparent, rounded corners.
* If your widget supports right-to-left (RTL) languages, you should provide a separate image for those languages that uses an RTL design.

The following is an example of a widget screenshot image. 

![An example of a widget screenshot image.](./images/widgets-example-screenshot.png)



