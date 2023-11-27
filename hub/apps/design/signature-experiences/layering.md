---
description: How materials and surfaces are arranged to create depth and hierarchy in Windows 11
title: Layering and elevation in Windows 11
ms.assetid: E00B6D9A-C8AA-4E6E-ADC4-13303AC290D9
ms.date: 06/24/2021
ms.topic: article
keywords: windows 11, design, ui, uiux, layering, elevation, shadows
ms.localizationpriority: medium
---

# Layering and elevation in Windows 11

Windows 11 uses layering and elevation as its foundation for app hierarchy. Hierarchy communicates important information about how to navigate within an app while keeping the user's attention focused on the most important content. Layering and elevation are powerful visual cues that modernize experiences and help them feel coherent within Windows.

## Layering

:::row:::
    :::column:::
        ![An application window with a single content area](images/layering_LayeringMain1.svg)
    :::column-end:::
    :::column:::
        ![An application window with multiple content areas](images/layering_LayeringMain2.svg)
    :::column-end:::
:::row-end:::

Layering is the concept of overlapping one surface with another, creating two or more visually distinguished areas within the same application.  

> [!NOTE]
> Layering in Windows 11 is tightly coupled with the use of materials. Please reference the [materials section](materials.md) for specific guidance on how those are applied.

Windows 11 uses a two-layer system for applications. These two layers create hierarchy and provide clarity, keeping users focused on what's most important.

- The **base** layer is an app's foundation. It is the bottommost layer of every app, and contains controls related to app menus, commands, and navigation.
- The **content** layer focuses the user on the app's central experience. The content layer may be on contiguous element, or separated into cards that segment content.

## Elevation

![A variety of overlapping UI elements, each at a different elevation](images/layering_elevation_hero_1880.png)

Elevation is the depth component of the spatial relationship one surface has to another with respect to their position on the desktop. When two or more objects occupy the same location on the screen, only the object with the highest elevation will be rendered at that location.

Shadows and contour (outlines) are used on controls and surfaces to subtly communicate an object's elevation, and to help draw focus where needed within an experience. Windows 11 uses the following values to express elevation with shadow and contour.

:::row:::
    :::column:::
        ![An application window](images/layering_elevation_window_940.png)
    :::column-end:::
    :::column span="":::
        **Window**<br>Elevation value: 128<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A modal dialog box](images/layering_elevation_dialog_940.png)
    :::column-end:::
    :::column span="1":::
        **Dialog**<br>Elevation value: 128<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A flyout menu](images/layering_elevation_flyout_940.png)
    :::column-end:::
    :::column span="1":::
        **Flyout**<br>Elevation value: 32<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A tooltip for a combo box](images/layering_elevation_tooltip_940.png)
    :::column-end:::
    :::column span="1":::
        **Tooltip**<br>Elevation value: 16<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A UI surface that contains several content areas](images/layering_elevation_card_940.png)
    :::column-end:::
    :::column span="1":::
        **Card**<br>Elevation value: 8<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A combo box](images/layering_elevation_control_940.png)
    :::column-end:::
    :::column span="1":::
        **Control**<br>Elevation value: 2<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![An empty UI surface](images/layering_elevation_layer_940.png)
    :::column-end:::
    :::column span="1":::
        **Layer**<br>Elevation value: 1<br>Stroke width: 1
    :::column-end:::
:::row-end:::



Controls in Windows 11 vary their elevation and contour to indicate state. The intensity of the rendered shadow changes depending on the theme at parity of value.

:::row:::
    :::column:::
        ![A button in the default state](images/layering_elevation_control_rest_940.png)
    :::column-end:::
    :::column span="1":::
        **Rest**<br>Elevation value: 2<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A button in the hover state](images/layering_elevation_control_hover_940.png)
    :::column-end:::
    :::column span="1":::
        **Hover**<br>Elevation value: 2<br>Stroke width: 1
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        ![A button in the pressed state](images/layering_elevation_control_pressed_940.png)
    :::column-end:::
    :::column span="1":::
        **Pressed**<br>Elevation value: 1<br>Stroke width: 1
    :::column-end:::
:::row-end:::
