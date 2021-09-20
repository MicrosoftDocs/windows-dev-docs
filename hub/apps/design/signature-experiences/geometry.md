---
author: hickeys
description: An overview of geometry and spacing in Windows 11
title: Geometry in Windows 11
ms.assetid: E9E66189-2C81-406D-9C89-8AFBEE0BCC47
ms.author: hickeys
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

## Spacing and gutters

The use of consistently sized spacing and gutters semantically groups an experience into separate components. These values map to our rounded corner logic and together help create a cohesive and usable layout.

:::row:::
    :::column:::
        ![Two buttons separated by 8 pixels](images/geometry_spacing_buttons_626.png)
    :::column-end:::
    :::column span="1":::
        **8px** between buttons
    :::column-end:::
:::row-end:::

:::row:::
    :::column span="1":::
        ![A button and a flyout separated by 8 pixels](images/geometry_spacing_flyout.svg)
    :::column-end:::
    :::column span="1":::
        **8px** between buttons and flyouts
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A control and a header separated by 8 pixels](images/geometry_spacing_header.svg)
    :::column-end:::
    :::column span="1":::
        **8px** between control and header
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A Control and a label separated by 12 pixels](images/geometry_Spacing_Label.svg)
    :::column-end:::
    :::column span="1":::
        **12px** between control and label
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![Two content areas separated by 12 pixels](images/geometry_Spacing_Cards.svg)
    :::column-end:::
    :::column span="1":::
        **12px** between content areas
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A surface containing text with 12 pixel gutters on both sides](images/geometry_Spacing_Margins.svg)
    :::column-end:::
    :::column span="1":::
        **12px** between surface and edge text
    :::column-end:::
:::row-end:::