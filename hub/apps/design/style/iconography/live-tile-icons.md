---
title: Icons for live tiles
description: How to create app icons/logos that represent your app in the Start menu, app tiles, the taskbar, the Microsoft Store, and more. 
keywords: windows 10, uwp
author: hickeys
ms.author: hickeys
design-contact: judysa
ms.date: 10/29/2021
ms.topic: article
ms.localizationpriority: medium
---

# Icons for live tiles

Apps running on versions of Windows that use Live Tiles can either explicitly provide a set of Live Tile images, or rely on Windows to select an app icon of an appropriate resolution and display it on a Live Tile. Apps that choose to provide tile images should provide images for all tile sizes at all common scale factors.

| Tile size | Scale factor | File name                |
|-----------|--------------|--------------------------|
| Small     | 100          | SmallTile.scale-100.png  |
| Small     | 125          | SmallTile.scale-125.png  |
| Small     | 150          | SmallTile.scale-150.png  |
| Small     | 200          | SmallTile.scale-200.png  |
| Small     | 400          | SmallTile.scale-400.png  |
| ---       |              |                          |
| Medium    | 100          | MediumTile.scale-100.png |
| Medium    | 125          | MediumTile.scale-125.png |
| Medium    | 150          | MediumTile.scale-150.png |
| Medium    | 200          | MediumTile.scale-200.png |
| Medium    | 400          | MediumTile.scale-400.png |
| ---       |              |                          |
| Wide      | 100          | WideTile.scale-100.png   |
| Wide      | 125          | WideTile.scale-125.png   |
| Wide      | 150          | WideTile.scale-150.png   |
| Wide      | 200          | WideTile.scale-200.png   |
| Wide      | 400          | WideTile.scale-400.png   |
| ---       |              |                          |
| Large     | 100          | LargeTile.scale-100.png  |
| Large     | 125          | LargeTile.scale-125.png  |
| Large     | 150          | LargeTile.scale-150.png  |
| Large     | 200          | LargeTile.scale-200.png  |
| Large     | 400          | LargeTile.scale-400.png  |

### Live tile background

Prior to to Windows 10 **18H2 MAYBE**, apps could specify the color of their live tile in the [VisualElement](/uwp/schemas/appxpackage/appxmanifestschema/element-visualelements) section of the application manifest. Windows 10 **18H2 MAYBE** introduced theme-aware live tiles, which automatically select a background color based on the user's theme. Apps targeting Windows 10 will need icons that look good on both types of live tiles. 

#### Creating an icon-based live tile

:::image type="content" source="images/transparent-background-tile.png" alt-text="{alt-text}":::

The easiest way to look great on live tiles is to just just display your app's icon against a transparent background. This is the "standard" way to do Live Tiles. To achieve this look, you can either create tile images with transparent backgrounds, or just let Windows display your app's icon on a regular Live Tile.

#### Creating a full bleed Live Tile image

:::image type="content" source="images/full-bleed-tile.png" alt-text="{alt-text}":::

When required, apps can create full-bleed Live Tile images to fully customize their Live Tile. Typically, this functionality is used by games. Non-game apps should generally not use full-bleed tiles, because it will make your live tile stand out awkwardly compared to "standard" icon-based tiles. Full bleed tiles can be used to achieve several looks.

**Background customization** - Apps can create a standard live tile image, but create a specific background color by replacing the transparent background with the desired color.

:::image type="content" source="images/transparent-background-tile.png" alt-text="{alt-text}":::
:::image type="content" source="images/transparent-background-tile.png" alt-text="{alt-text}":::

> [!NOTE]
> The second copy of this image should have a non-transparent background to show the change I'm describing

**Large icons** - Apps using a full bleed tile can make their icon any size they want. To do this, simply make your icon the size you would like it to display when the tile is shown.

> [!WARNING]
> App icons should never take up the complete tile. Be sure to use at least 16% margins on each side.
>
> |           | Image                                                                     |
> |-----------|---------------------------------------------------------------------------|
> | Do this:  | :::image type="content" source="images/full-bleed-margins.png" alt-text="{alt-text}":::|
> | Not this: | :::image type="content" source="images/full-bleed-no-margin.png" alt-text="{alt-text}"::: |

**Image tiles** - Some apps, usually games, might want to display a full-bleed image instead of an icon. In this case, do not use margins - the image should take up all available space.

|           | Image                                                                     |
|-----------|---------------------------------------------------------------------------|
| Do this:  | **NEED IMAGE** |
| Not this: | **NEED IMAGE** |
