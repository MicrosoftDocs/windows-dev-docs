---
description: An overview of geometry and spacing in Windows 11
title: Geometry in Windows 11
ms.assetid: E9E66189-2C81-406D-9C89-8AFBEE0BCC47
ms.date: 06/24/2021
ms.topic: article
keywords: windows 11, design, ui, uiux, geometry, gutters, margins, corners, rounded corners, corner radius
ms.localizationpriority: medium
---

# Geometry in Windows 11

![Rounded corners and element spacing in Windows 11](images/geometry_hero_1880.png)

Geometry describes the shape, size and position of UI elements on screen. These fundamental design elements help experiences feel coherent across the entire design system.

Windows 11 geometry has been crafted to support modern app experiences. Progressively rounded corners, nested elements, and consistent gutters combine to create a soft, calm, and approachable effect that emphasizes unity of purpose and ease of use.

## Rounded corners

![Dialog with rounded corners](images/geometry_rounded_corners_1880.png)

Windows 11 applies rounded corners to all top-level app windows. The same applies to most common controls such as Button and ListView. (For more information, see [Use the latest common controls](../../get-started/make-apps-great-for-windows.md#4-use-the-latest-common-controls).) 

Windows 11 uses three levels of rounding depending on what UI component is being rounded and how that component is arranged relative to neighboring elements.

| Corner radius | Usage                     |
|---------------|---------------------------|
| 8px           | Top-level containers such as app windows, flyouts and dialogs are rounded using an 8px corner radius. |
| 4px           | In-page elements such as buttons and list backplates are rounded using a 4px corner radius.           |
| 0px           | Straight edges that intersect with other straight edges are not rounded.                              |
| 0px           | Window corners are not rounded when windows are snapped or maximized.                                 |
