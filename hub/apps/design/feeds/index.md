---
author: drewbatgit
description: Learn how to implement an app that provides Windows Feeds, displayed on the widgets board built into Windows 11.
title: Windows Feeds
ms.author: drewbat
ms.date: 11/01/2023
ms.topic: article
keywords: windows 11, widgets
ms.localizationpriority: medium
---

# Feeds overview

The Windows Widgets Board shows both widgets and feeds. Starting with (release TBD), third party developers can provide feed content for the Widgets Board.

![A screenshot showing the Windows Widgets Board showing feeds.](./images/feeds-screenshot.png)

## Feeds terminology

| Term | Definition |
|------|------------|
| Widgets Board | The Widgets Board is a Windows 11 system component that is displayed over the desktop when the user clicks the Widgets icon on the taskbar, uses the Windows+W shortcut, or swipes from the left edge of the screen. The Widgets Board displays widgets and feeds and manages their layout the on the board. |
| Feed | TBD |
| Widget provider | A widget provider is a Windows app that provides content to be displayed in the widget. The widget provider owns the content, layout, and interactive elements of the widget.  |


### Feeds design guidance 

[I'm guessing this section should just be cut, but leaving it here for now, in case we want to adapt for feeds.]

The visual experience of a widget includes visual elements and interaction elements that are defined using the Adaptive Cards JSON format. The [Adaptive Cards Designer](https://www.adaptivecards.io/designer/) provides a real-time editor for designing adaptive cards as well as templates for the supported widget sizes and themes. It's important that your widget's design adhere the Windows Widget design principles to help ensure that the Widgets Board provides a consistent and familiar experience for all widgets.

For a high-level walkthrough of the visual elements of a widget see [Widget states and built-in UI components](widgets-states-and-ui.md).


## Widget principles

To create great Windows Widgets, consider the following principles as you design and develop your widgets:

### Glanceable 

Users can take a quick peek to get the most value out of the widget. They only need to click on it if they want richer details or deeper interactions. 
 
### Dependable 

Surface frequently used information instantly to save users time in repeating those steps. Drive consistent re-engagement to your app.

### Useful 

Elevate the most useful and relevant information. 

### Personal 

Provide personalized content and build an emotional connection with customers. Widgets should never contain ads. Customers are in control of their widget content and layout. 

### Focused 

Each widget should generally focus on one main task or scenario. Widgets are not intended to replace your apps and websites. 

### Fresh 

Content should dynamically refresh based on available context. It is up to date and provides the right content at the right time. 
  

## Planning your app's widget experience

[I'm guessing this section should just be cut, but leaving it here for now, in case we want to adapt for feeds.]

1. Based on your understanding of your customers, identify the most important content or most useful actions that your users would love to have quick access to without opening your app or website. Consider the principles enumerated in the [Widget principles](#widget-principles) section and think about how they can apply to your app. 
1. Your app can support multiple individual widgets. Determine the number of separate widgets you want to support so that each widget focuses on a specific purpose.
1. Determine the content you want to include for each widget. A single widget can support three different sizes; small, medium, and large. For each widget, think about what content would bring the most value to users and your business needs. For each size from small to large, the purpose of the widget should remain the same, but the amount of information displayed should expand with larger sizes. We recommend that widget providers implement all widget sizes to give users flexibility when customizing the widget layout. 
1. Think about the user interactions your widget will support. Users can click on the widget title or any click targets that you’ve defined on the widget. These interactions can activate deep-link shortcuts into your app or web site that take users directly to what they're interested in, so that they don’t have to navigate from the root of your app. Consider the different navigational models offered.  
1. Apps must implement a widget provider that implements the back-end functionality to send your widget's layout and data to the widgets board to be displayed. Currently you can implement a widget provider using a packaged Win32 desktop app or a Progressive Web App (PWA). For more information on creating a Win32 widget provider, see [Widget service providers](../../develop/widgets/widget-providers.md). For information on PWA widget providers, see [Build PWA-driven widgets](/microsoft-edge/progressive-web-apps-chromium/how-to/widgets).



## In this section

[Feeds design fundamentals](feeds-design-fundamentals.md)

