---
title: Widgets overview
description: Learn how to support your app by implementing one or more widgets that the user can view and interact with on the widgets board built into Windows 11. 
ms.topic: article
ms.date: 07/06/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Widgets overview

Windows Widgets are small UI containers that display text and graphics from an app or web service. Installed widgets are displayed in a grid in the Widgets Board a flyout plane that overlays the Windows desktop when the user clicks Widgets icon on the taskbar, uses the Windows+W shortcut, or swipes from the left edge of the screentaps the Widgets icon in the task bar. Widgets help people stay on top of what's important to them by aggregating personalized content and quick actions from the apps and web services they use. They are quickly consumable and actionable. Widgets are not meant to replace apps and websites, but rather provide frictionless access to most-needed information or often-used functionalities that people can read/trigger right away. When designing your widget, consider the kind of value it will bring to your consumers.  

## Widgets terminology

| Term | Definition |
|------|------------|
| Widgets board | The Widgets Board is a Windows 11 system application that is displayed over the desktop when the user clicks the Widgets icon on the taskbar, uses the Windows+W shortcut, or swipes from the left edge of the screen. The Widgets Board displays installed widgets and manages the layout the widgets are displayed on the board. |
| Widget | A widget is an [Adaptive Card](https://adaptivecards.io/) that presents important content or actions from an app or service. It allows users to access desired information instantly without the need to launch the associated app or website. 
Widget content is refreshed dynamically throughout the day to provide the user with current and interesting content that can be consumed at a glance. Widgets provide basic interactive features that allow the user to launch the associated app or web service for deeper engagement. Widgets are not intended to replace apps and websites.  |
| Widget provider | A widget provider is a Windows app or web service that provides content to be displayed in the widget. The widget provider owns the content, layout, and interactive elements of the widget.  |


### Widget design guidance 

The visual experience of a widget includes visual elements and interaction elements that are defined using the Adaptive Cards JSON format. The [Adaptive Cards Designer](https://www.adaptivecards.io/designer/) provides a real-time editor for designing adaptive cards as well as templates for the supported widget sizes and themes. It's important that your widget's design adhere the Windows Widget design principles to help ensure that the Widget Board provides a consistent and familiar experience for all widgets.

For a high-level walkthrough of the visual elements of a widget see [Widget states and built-in UI components](../../design/widgets/widgets-states-and-ui.mdwidget-states-and-ui.md).

To get started designing widgets for Windows, see [Design for Windows Widgets](../../design/widgets/widgets-design-overview.md).

### Widget service providers

The Adaptive Cards format used by Windows widgets enables dynamic binding of the data that populates the widget UI. To update your widget, your app or service will implement a widget service provider that responds to requests from the widget board and provides the both the visual template and the associated data for your widget, in JSON format.

Currently you can implement widget providers either through a Win32 desktop app or a Progressive Web App (PWA). For more information see:

* [Implement a widget provider (Win32 apps)](implement-widget-provider-win32.md)
* [Implement a widget provider (PWA)](tbd)

## Related articles

* [Widgets design overview](../../design/widgets/widgets-design-overview)
