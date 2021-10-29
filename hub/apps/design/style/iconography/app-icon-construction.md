---
title: Construct your Windows app's icon
description: How to create app icons/logos that represent your app in the Start menu, app tiles, the taskbar, the Microsoft Store, and more. 
keywords: windows 10, uwp
author: hickeys
ms.author: hickeys
design-contact: judysa
ms.date: 10/29/2021
ms.topic: article
ms.localizationpriority: medium
---

# Construct your Windows app's icon

Once you've designed your app's icon, you need to create the icon files themselves. Because Windows supports multiple themes, display resolutions, and scale factors, you should provide multiple versions of your icon to make sure it looks great on every device, at any size.

## Icon sizes

Windows will display your app icon at a variety of sizes depending on where your icon is being displayed and the user's display scale settings. The following table lists all the possible sizes that Windows may use to display your icon.

:::image type="content" source="images/icon-sizes.png" alt-text="{alt-text}":::

| Icon size | Comments                                                          |
|-----------|-------------------------------------------------------------------|
| 16x16     | Context menu and system tray icons at 100% scale factor           |
| 20x20     |                                                                   |
| 24x24     | Taskbar icons at 100% scale factor                                |
| 30x30     |                                                                   |
| 32x32     | Live tile icon size at 100% scale factor (size of icon, not tile) |
| 36x36     |                                                                   |
| 40x40     |                                                                   |
| 48x48     | Taskbar icons at 200% scale factor                                |
| 60x60     |                                                                   |
| 64x64     | Live tile icon size at 200% scale factor (size of icon, not tile) |
| 72x72     |                                                                   |
| 80x80     |                                                                   |
| 96x96     | Taskbar icons at 400% scale factor                                |
| 256x256   |                                                                   |

### Icon scaling

Whenever Windows displays your app's icon, it will select the icon file with dimensions closest to the space in which the icon will be displayed. If your app provides an exact fit, Windows will use that size. If not, Windows will automatically select a close match and scale it to look great. Including more icon sizes with your app means Windows will more often have a pixel-perfect match, and reduce the amount of scaling applied to scaled icons.

> [!NOTE]
> Apps should have, at the bare minimum, three icon sizes: 24x24, 32x32, and 256x256. This covers the two most common icon sizes, and by providing a 256px icon, ensures Windows will only ever scale your icon down; never up.

There are two specific times when Windows will use a scaled icon, even when an icon of the correct size is available.

1. When displaying a Win32 app's icon in the taskbar, Windows will select the 32x32 icon and scale it down to 24x24.
2. When displaying a WinUI app's icon on a medium live tile, Windows will plate your app's 48x48 icon against a 150x150 tile, and scale the images down to a 32x32 icon on a 100x100 tile.  

## Icon variations

Windows uses different types of icon assets in different scenarios.

- **App list icons** are used in the Start Menu's All Apps list, and in...
- **Dark mode icons** are used in the Start Menu and taskbar when Windows is in Dark Mode. These icon files are identified via their filename, which must be of the form `name_altform-unplated`.
- **Light mode icons** are used in the Start Menu and taskbar when Windows is in Light Mode. These icon files are identified via their filename, which must be of the form `name_altform-lightunplated`.
- **Search result list icons**
- **Splash screen icons**
- **Badge logo icons**

### Complete list of icons and variations

| Icon size | Variation  | Filename                          |
|-----------|------------|-----------------------------------|
| 16x16     | standard   | 16x16.png                         |
| 16x16     | dark mode  | 16x16_altform-unplated.png        |
| 16x16     | light mode | 16x16_altform-lightunplated.png   |
| ---       |            |                                   |  
| 20x20     | standard   | 20x20.png                         |
| 20x20     | dark mode  | 20x20_altform-unplated.png        |
| 20x20     | light mode | 20x20_altform-lightunplated.png   |
| ---       |            |                                   |  
| 24x24     | standard   | 24x24.png                         |
| 24x24     | dark mode  | 24x24_altform-unplated.png        |
| 24x24     | light mode | 24x24_altform-lightunplated.png   |
| ---       |            |                                   |  
| 30x30     | standard   | 30x30.png                         |
| 30x30     | dark mode  | 30x30_altform-unplated.png        |
| 30x30     | light mode | 30x30_altform-lightunplated.png   |
| ---       |            |                                   |  
| 32x32     | standard   | 32x32.png                         |
| 32x32     | dark mode  | 32x32_altform-unplated.png        |
| 32x32     | light mode | 32x32_altform-lightunplated.png   |
| ---       |            |                                   |  
| 36x36     | standard   | 36x36.png                         |
| 36x36     | dark mode  | 36x36_altform-unplated.png        |
| 36x36     | light mode | 36x36_altform-lightunplated.png   |
| ---       |            |                                   |  
| 40x40     | standard   | 40x40.png                         |
| 40x40     | dark mode  | 40x40_altform-unplated.png        |
| 40x40     | light mode | 40x40_altform-lightunplated.png   |
| ---       |            |                                   |  
| 48x48     | standard   | 48x48.png                         |
| 48x48     | dark mode  | 48x48_altform-unplated.png        |
| 48x48     | light mode | 48x48_altform-lightunplated.png   |
| ---       |            |                                   |  
| 60x60     | standard   | 60x60.png                         |
| 60x60     | dark mode  | 60x60_altform-unplated.png        |
| 60x60     | light mode | 60x60_altform-lightunplated.png   |
| ---       |            |                                   |  
| 64x64     | standard   | 64x64.png                         |
| 64x64     | dark mode  | 64x64_altform-unplated.png        |
| 64x64     | light mode | 64x64_altform-lightunplated.png   |
| ---       |            |                                   |  
| 72x72     | standard   | 72x72.png                         |
| 72x72     | dark mode  | 72x72_altform-unplated.png        |
| 72x72     | light mode | 72x72_altform-lightunplated.png   |
| ---       |            |                                   |  
| 80x80     | standard   | 80x80.png                         |
| 80x80     | dark mode  | 80x80_altform-unplated.png        |
| 80x80     | light mode | 80x80_altform-lightunplated.png   |
| ---       |            |                                   |  
| 96x96     | standard   | 96x96.png                         |
| 96x96     | dark mode  | 96x96_altform-unplated.png        |
| 96x96     | light mode | 96x96_altform-lightunplated.png   |
| ---       |            |                                   |  
| 256x256   | standard   | 256x256.png                       |
| 256x256   | dark mode  | 256x256_altform-unplated.png      |
| 256x256   | light mode | 256x256_altform-lightunplated.png |

### Transparent background

Icons look best with a transparent background. If your app's branding requires your icon be plated on a background, that's okay too. However, you'll have to re-implement some theming functionality that transparent icons get for free. For example, you might provide a version of your app's icon plated on a white background for use in dark mode, and a black background for use in light mode.

