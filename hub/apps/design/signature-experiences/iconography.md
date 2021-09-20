---
author: hickeys
description: App and system icons in Windows 11
title: Iconography in Windows 11
ms.assetid: EC94D54F-4C24-4E16-B8E0-08F3916C00F0
ms.author: hickeys
ms.date: 06/24/2021
ms.topic: article
keywords: windows 11, design, ui, uiux, icons, app icons, system icons, segoe fluent icons, segoe
ms.localizationpriority: medium
---

# Iconography in Windows 11

Iconography is a set of visual images and symbols that help users understand and navigate your app. Icons are used throughout the user interface as visual metaphors representing a concept, action, or status.

Windows 11 uses three different type of icons: application icons, system icons, and file type icons.

## Application icons

![An abstract application icon for a hypothetical maps app](images/iconography_hero_1880.png)

Application icons represent your app in the Windows Shell. They are primarily used to launch your application, but also represent your app wherever it appears in the Windows shell.

App icons should represent your app's core functionality through a metaphor. See [App Icons](../style/app-icons-and-logos.md) for more information about designing and constructing your app's icon.

## System icons

![A shopping cart icon from Segoe Fluent Icons](images/iconography_SystemIcons.svg)

Windows 11 introduces a new system icon font, [Segoe Fluent Icons](..\downloads\index.md#fonts). This new font compliments Windows 11's [geometry](geometry.md).

All glyphs in Segoe Fluent Icons are drawn in a Monoline style, which means they are created using a single stroke of 1 epx.

Glyphs in Segoe Fluent Icons follow three aesthetic principles:

1. **Minimal**: Glyphs contain only the details necessary to communicate the concept.
2. **Harmonious**: Glyphs are based on simple and geometric forms.
3. **Evolved**: Glyphs use modern metaphors that are easily understood.

### Sizing

![A properly sized printer icon](images/iconography_IconSizing.svg)

Segoe Fluent Icons font metrics were developed to match how designers and developers are used to working with SVG and bitmap icons.

Each font glyph is designed so that the footprint of the icon area is a square em.
A 16 epx font size icon is the equivalent of a 16x16 epx icon, making sizing and positioning more predictable.

### Anatomy

System icons glyphs can be visually constructed by combining a base icon with a modifier icon.

**Base icons** are the main element of a visual metaphor. Base elements should should occupy the entire icon footprint.

**Modifier icons** modify the meaning of the base icon. Modifier elements should be placed in one of the bottom quadrants of the icon footprint.

:::row:::
    :::column:::
        ![A file icon](images/iconography_Anatomy1.svg)
    :::column-end:::
    :::column span="3":::
        **Base icon only**<br>
        On its own, the paper sheet icon communicates the concept of a *file*
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        ![A file icon overlayed with an up arrow icon](images/iconography_Anatomy2.svg)
    :::column-end:::
    :::column span="3":::
        **Base icon + modifier icon**<br>
        Adding an up arrow to the file icon changes the meaning of the icon to represent an *uploaded file*
    :::column-end:::
:::row-end:::

### Layering

Icon layering is a technique used to overlap two different glyphs. We recommend using icon layering to create a different state of the same icon (e.g. active or selected state).

![A black and white folder icon plus a beige folder icon with no outlines equals a beige folder icon with a black outline](images/iconography_IconLayering.svg)

### Localization

Understand the cultural connotations of certain symbols in different cultures. While in most cases iconography doesn't require localization, certain icons may be acceptable in one culture but not in another. Validate your iconography choices with the context in which they will be used.
