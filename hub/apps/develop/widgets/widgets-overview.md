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

## Implementing a Windows Widget for your app or service

A widget implementation has three primary components:

### Widget UI templates 

The visual experience of a widget includes visual elements and interaction elements that are defined using the Adaptive Cards JSON format. The [Adaptive Cards Designer](https://www.adaptivecards.io/designer/) provides a real-time editor for designing adaptive cards as well as templates for the supported widget sizes and themes. It's important that your widget's design adhere the Windows Widget design principles to help ensure that the Widget Board provides a consistent and familiar experience for all widgets.

For a high-level walkthrough of the visual elements of a widget see [Widget states and built-in UI components](widget-states-and-ui.md).

To get started designing widgets for Windows, see [Design for Windows Widgets](widgets-design.md).

### Widget service provider

Widgets encourage discovery and engagement with your app or service by presenting current information that is focused, glanceable, and reflects the user's interest. The Adaptive Cards format enables dynamic binding of the data that populates the widget UI. To update your widget, your app or service will implement a widget service provider that responds to requests from the Widget Board and provides the data for your widget, in JSON format.

Windows Widgets currently support three different ways to implement you widget service provider. The method you choose depends on the type of app or service your widget is supporting.

| Service provider technology | Description |
|-----------------------------|-------------|
| UWP App Service    | TBD |
| COM server     | TBD |
| Web service     | TBD |

To get started implementing your widget service provider, see [Service providers for Windows Widgets](widgets-service-providers.md).

## Related articles

* [Widgets design overview](../../design/widgets/widgets-design-overview)
* [Implement a widget provider (Win32 apps)](implement-widget-provider-win32.md)
* [Implement a widget provider (Progressive Web apps)](tbd)
* [Debug widgets](tbd) 