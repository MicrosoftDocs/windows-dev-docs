---
author: drewbat
description: Learn about the design guidelines for Windows Widgets
title: Windows Widgets design guidelines
ms.author: drewbatgit
ms.date: 07/07/2022
ms.topic: article
keywords: windows 11, widgets
ms.localizationpriority: medium
---


# Windows widgets design guidelines

This section provides guidance and resources for designing UI for Windows widgets. For developer guidance for implementing the widget provider app that provides the back-end functionality for Windows widgets, see [Widgets overview](../../develop/widgets/widgets-overview.md).

## Widget principles

To create great Windows Widgets, consider the following principles as you design and develop your widgets:

### Glanceable 

Users can take a quick peek to get the most value out of the widget. They only need to click on it if they want richer details or deeper interactions. 
 
### Dependable 

Surface frequently used information instantly to save users time in repeating those steps. Drive consistent re-engagement to your app or services.

### Useful 

Elevate the most useful and relevant information. 

### Personal 

Provide personalized content and build an emotional connection with customers. Widgets should never contain ads. Customers are in control of their widget content and layout. 

### Focused 

Each widget should generally focus on one main task or scenario. Widgets are not intended to replace your apps and websites. 

### Fresh 

Content should dynamically refresh based on available context. It is up to date and provides the right content at the right time. 
  

## Planning your app's widget experience

1. Based on your understanding of your customers, identify the most important content or most useful actions that your users would love to have quick access to without opening your app or website. Consider the principles enumerated in the **Widget principles** section, and think about how they can apply to your app or service. 
1. Your app or service can support multiple individual widgets on the Widgets Board. Determine the number of separate widgets you want to support so that each widget focuses on a specific purpose.
1. Determine the content you want to include for each widget. A single widget can support three different sizes; small, medium, and large. For each widget, think about what content would bring the most value to users and your business needs. For each size from small to large, the focus of a widget’s purpose should remain the same, but the amount of information displayed should expand with larger sizes. We recommend that all widger providers implement the small and medium sizes as these size are more likely to get prioritized by the dynamic ranker and are also more likely to be pinned by the user. 
1. Think about the user interactions your widget will support. Users can click on the widget title or any click targets that you’ve defined on the widget. These interactions can activate deeplink shortcuts into your app or web site that take users directly to what they're interested in, so that they don’t have to navigate from the root of your app or service. Consider the different navigational models offered [LINK TBD](tbd).  
1. Apps must implement a widget provider that implements the back-end functionality to send your widget's layout and data to the widget board to be displayed. Currently, widget providers can be implemented using a Win32 desktop app or a Progressive Web App (PWA). Determine the service model that works best with your app or service. For more information, see [Widgets overview](../../develop/widgets/widgets-overview.md).



## In this section

[Widget states and UI](widgets\widgets-states-and-ui.md)
[Widget design fundamentals](widgets-design-fundamentals.md)
[Widget interaction design](widgets-interaction-design.md)