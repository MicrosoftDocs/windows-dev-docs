---
title: Widget service providers
description: Learn how to implement a Windows widget service provider to support your app. 
ms.topic: article
ms.date: 07/06/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---



### Widget service providers

Windows widgets are small UI containers that display text and graphics from an app or web service. The Adaptive Cards format used by Windows widgets enables dynamic binding of the data that populates the widget UI. To update your widget, your app or service will implement a widget service provider that responds to requests from the widget board and returns JSON documents specifying both the visual template and the associated data for your widget.

For an overview of the Windows widgets experience and design guidance for creating your own widgets, see [Windows widgets](../../design/widgets/).

Currently you can implement widget providers either through a Win32 desktop app or a Progressive Web App (PWA). For more information see:

* [Implement a widget provider (Win32 apps)](implement-widget-provider-win32.md)
* [Implement a widget provider (PWA)](tbd)



## Related articles

* [Windows widgets](../../design/widgets/)
