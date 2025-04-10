---
title: Create icons using Visual Studio's asset generation tool
description: Use the Visual Studio asset generation tool to create a variety of icon files from just one image.
keywords: windows 10, uwp
design-contact: judysa
ms.date: 10/29/2021
ms.topic: article
ms.localizationpriority: medium
---

# Create icons using Visual Studio's asset generation tool

While handcrafting each icon file will create the best, most consistent user experience, teams running short on resources can take advantage of Visual Studio's Manifest Designer. This tool can create an entire set of app icons and tile images from a single image. This is useful to create an initial set of icons, but will not achieve the same result as handcrafting each icon file, as Visual Studio will have to scale your image to create the required image sizes.

## Launching the Manifest Designer

:::row:::
    :::column:::
        1. Use Visual Studio to open a WinUI or UWP project.
        2. In the **Solution Explorer**, double-click the Package.appxmanifest file.
    :::column-end:::
    :::column:::
        :::image type="content" source="images/package-appmanifest.png" alt-text="A diagram that shows a view of solution explorer highlighting the Package.appxmanifest file.":::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        3. Visual Studio displays the Manifest Designer.
    :::column-end:::
    :::column:::
            :::image type="content" source="images/manifest-properties.png" lightbox="images/manifest-properties.png" alt-text="A diagram that shows a view of the properties panel for a Package.appxmanifest file.":::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        4. Click the **Visual Assets** tab.
    :::column-end:::
    :::column:::
        :::image type="content" source="images/visual-assets-panel.png" lightbox="images/visual-assets-panel.png" alt-text="A diagram that shows a view of the visual assets panel.":::
    :::column-end:::
:::row-end:::

## Generating icons with the Manifest Designer

1. Click the `...` next to the Source field and select the image you want to use. For best results, use a vector-based image, Adobe Illustrator file, or PDF. If you're using a bitmap image, make sure it's at least 400 by 400 pixels so that you get sharp results.

2. In the Display Settings section, configure these options:
    - **Short name**: Specify a short name for your app.
    - **Show name**: Indicate whether you want to display the short name on medium, wide, or large tiles.
    - **Tile background**: Specify the hex value or a color name for the tile background color. For example, #464646. The default value is transparent. **NOTE:** This setting will be ignored on versions of Windows that support theme-aware Live Tiles.
    - **Splash screen background** (Optional): Specify the hex value or color name for the splash screen background.
3. Click **Generate**.

> [!NOTE]
> Visual Studio's Manifest Designer doesn't generate a badge logo by default. That's because your badge logo is unique and probably shouldn't match your other app icons. For more info, see [Badge notifications for Windows apps](/windows/uwp/design/shell/tiles-and-notifications/badges).
