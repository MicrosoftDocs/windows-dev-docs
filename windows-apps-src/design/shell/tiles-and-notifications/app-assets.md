---
author: mijacobs
Description: App icon assets, which appear in a variety of forms throughout the Windows 10 operating system, are the calling cards for your Universal Windows Platform (UWP) app.
title: Tile and icon assets
ms.assetid: D6CE21E5-2CFA-404F-8679-36AA522206C7
label: Tile and icon assets
template: detail.hbs
ms.author: mijacobs
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Guidelines for tile and icon assets

 


App icon assets, which appear in a variety of forms throughout the Windows 10 operating system, are the calling cards for your Universal Windows Platform (UWP) app. These guidelines detail where app icon assets appear in the system, and provide in-depth design tips on how to create the most polished icons.

![windows 10 start and tiles](images/assetguidance01.jpg)

## Adaptive scaling


First, a brief overview on adaptive scaling to better understand how scaling works with assets. Windows 10 introduces an evolution of the existing scaling model. In addition to scaling vector content, there is a unified set of scale factors that provides a consistent size for UI elements across a variety of screen sizes and display resolutions. The scale factors are also compatible with the scale factors of other operating systems such as iOS and Android, which makes it easier to share assets between these platforms.

The Store picks the assets to download based in part on the DPI of the device. Only the assets that best match the device are downloaded.

## Tile elements


The basic components of a Start tile consist of a back plate, an icon, a branding bar, margins, and an app title:

![breakdown of tile elements](images/assetguidance02.png)

The branding bar at the bottom of a tile is where the app name, badging, and counter (if used) appear:

![branding bar in tile](images/assetguidance03.png)

The height of the branding bar is based on the scale factor of the device on which it appears:

| Scale factor | Pixels |
|--------------|--------|
| 100%         | 32     |
| 125%         | 40     |
| 150%         | 48     |
| 200%         | 64     |
| 400%         | 128    |

 

The system sets tile margins and cannot be modified. Most content appears inside the margins, as seen in this example:

![tile margins](images/assetguidance04.png)

Margin width is based on the scale factor of the device on which it appears:

| Scale factor | Pixels |
|--------------|--------|
| 100%         | 8      |
| 125%         | 10     |
| 150%         | 12     |
| 200%         | 16     |
| 400%         | 32     |

 

## Tile assets


Each tile asset is the same size as the tile on which it is placed. You can brand your app's tiles with two different representations of an asset:

1. An icon or logo centered with padding. This lets the back plate color show through:

![tile and back plate](images/assetguidance05.png)

2. A full-bleed, branded tile without padding:

![tile showing full bleed](images/assetguidance06.png)

For consistency across devices, each tile size (small, medium, wide, and large) has its own sizing relationship. In order to achieve a consistent icon placement across tiles, we recommend a few basic padding guidelines for the following tile sizes. The area where the two purple overlays intersect represents the ideal footprint for an icon. Although icons won't always fit inside the footprint, the visual volume of an icon should be roughly equivalent to the provided examples.

Small tile sizing:

![small tile sizing example](images/assetguidance07a.png)

Medium tile sizing:

![medium tile sizing example](images/assetguidance07b.png)

Wide tile sizing:

![wide tile sizing example](images/assetguidance07c.png)

Large tile sizing:

![large tile sizing example](images/assetguidance07d.png)

In this example, the icon is too large for the tile:

![icon too big for tile](images/assetguidance08a.png)

In this example, the icon is too small for the tile:

![icon too small for tile](images/assetguidance08b.png)

The following padding ratios are optimal for horizontally or vertically oriented icons.

For small tiles, limit the icon width and height to 66% of the tile size:

![small tile sizing ratios](images/assetguidance09.png)

For medium tiles, limit the icon width to 66% and height to 50% of tile size. This prevents overlapping of elements in the branding bar:

![medium tile sizing ratios](images/assetguidance10.png)

For wide tiles, limit the icon width to 66% and height to 50% of tile size. This prevents overlapping of elements in the branding bar:

![wide tile sizing ratios](images/assetguidance11.png)

For large tiles, limit the icon width to 66% and height to 50% of tile size:

![large tile size ratios](images/assetguidance12.png)

Some icons are designed to be horizontally or vertically oriented, while others have more complex shapes that prevent them from fitting squarely within the target dimensions. Icons that appear to be centered can be weighted to one side. In this case, parts of an icon may hang outside the recommended footprint, provided it occupies the same visual weight as a squarely fitted icon:

![three centered icons](images/assetguidance13.png)

With full-bleed assets, take into account elements that interact within the margins and edges of the tiles. Maintain margins of at least 16% of the height or width of the tile. This percentage represents double the width of the margins at the smallest tile sizes:

![full-bleed tile with margins](images/assetguidance14.png)

In this example, margins are too tight:

![full bleed tile with margins that are too small](images/assetguidance15.png)

## Tile assets in list views


Tiles can also appear in a list view. Sizing guidelines for tile assets that appear in list views are a bit different than tile assets previously outlined. This section details those sizing specifics.

![tile assets in a list view](images/assetguidance16.png)

Limit icon width and height to 75% of the tile size:

![sizing of icon of list view tile](images/assetguidance17.png)

For vertical and horizontal icon formats, limit width and height to 75% of the tile size:

![sizing of icon of list view tile](images/assetguidance18.png)

For full bleed artwork of important brand elements, maintain margins of at least 12.5%:

![full-bleed artwork in list view tile](images/assetguidance19.png)

In this example, the icon is too big inside its tile:

![icon that is too big for tile](images/assetguidance20a.png)

In this example, the icon is too small inside its tile:

![icon that is too small for tile](images/assetguidance20b.png)

## Target-based assets


Target-based assets are for icons and tiles that appear on the Windows taskbar, task view, ALT+TAB, snap-assist, and the lower-right corner of Start tiles. You don't have to add padding to these assets; Windows adds padding if needed. These assets should account for a minimum footprint of 16 pixels. Here's an example of these assets as they appear in icons on the Windows taskbar:

![assets in windows taskbar](images/assetguidance21.png)

Although these UI will use a target-based asset on top of a colored backplate by default, you may use a target-based unplated asset as well. Unplated assets should be created with the possibility that they may appear on various background colors:

![unplated and plated assets](images/assetguidance22.png)

These are size recommendations for target-based assets, at 100% scale:

![target-based asset sizing at 100% scale](images/assetguidance23.png)

## Splash screen assets


The splash screen image can be given either as a direct path to an image file or as a resource. By using a resource reference, you can supply images of different scales so that Windows can choose the best size for the device and screen resolution. You can also supply high contrast images for accessibility and localized images to match different UI languages.

If you open "Package.appxmanifest" in a text editor, the [**SplashScreen**](https://docs.microsoft.com/uwp/schemas/appxpackage/appxmanifestschema/element-splashscreen) element appears as a child of the [**VisualElements**](https://docs.microsoft.com/uwp/schemas/appxpackage/appxmanifestschema/element-visualelements) element. The default splash screen markup in the manifest file looks like this in a text editor:

```XML
<uap:SplashScreen Image="Assets\SplashScreen.png" /></code></pre></td>
</tr>
</tbody>
</table>
```

The splash screen asset is centered by whichever device it appears on:

![sizing of splash screen asset](images/assetguidance27.png)

## High-contrast assets


High-contrast mode makes use of separate sets of assets for high-contrast white (white background with black text) and high-contrast black (black background with white text). If you don't provide high-contrast assets for your app, standard assets will be used.

If your app's standard assets provide an acceptable viewing experience when rendered on a black-and-white background, then your app should look at least satisfactory in high-contrast mode. If your standard assets don't afford an acceptable viewing experience when rendered on a black-and-white background, consider specifically including high-contrast assets. These examples illustrate the two types of high-contrast assets:

![high-contrast ratio asset examples](images/assetguidance28.png)

If you decide to provide high-contrast assets, you need to include both sets—both white-on-black and black-on-white. When including these assets in your package, you could create a "contrast-black" folder for white-on-black assets, and a "contrast-white" folder for black-on-white assets.

## Asset size tables


At a bare minimum, we strongly recommend that you provide assets for the 100, 200, and 400 scale factors. Providing assets for all scale factors will provide the optimal user experience.

<br/>

<table>
<thead>
<tr><th colspan="3">Small tile (Square71x71Logo)</th></tr>
</thead>
<tbody>
<tr>
    <td width="20%">100% scale</td>
    <td width="20%">71x71</td>
    <td>Square71x71Logo.scale-100.png</td>
</tr>
<tr>
    <td>125% scale</td>
    <td>89x89</td>
    <td>Square71x71Logo.scale-125.png</td>
</tr>
<tr>
    <td>150% scale</td>
    <td>107x107</td>
    <td>Square71x71Logo.scale-150.png</td>
</tr>
<tr>
    <td>200% scale</td>
    <td>142x142</td>
    <td>Square71x71Logo.scale-200.png</td>
</tr>
<tr>
    <td>400% scale</td>
    <td>284x284</td>
    <td>Square71x71Logo.scale-400.png</td>
</tr>
</tbody>
</table>

<br/>

<table>
<thead>
<tr><th colspan="3">Medium tile (Square150x150Logo)</th></tr>
</thead>
<tbody>
<tr>
    <td width="20%">100% scale</td>
    <td width="20%">150x150</td>
    <td>Square150x150Logo.scale-100.png</td>
</tr>
<tr>
    <td>125% scale</td>
    <td>188x188</td>
    <td>Square150x150Logo.scale-125.png</td>
</tr>
<tr>
    <td>150% scale</td>
    <td>225x225</td>
    <td>Square150x150Logo.scale-150.png</td>
</tr>
<tr>
    <td>200% scale</td>
    <td>300x300</td>
    <td>Square150x150Logo.scale-200.png</td>
</tr>
<tr>
    <td>400% scale</td>
    <td>600x600</td>
    <td>Square150x150Logo.scale-400.png</td>
</tr>
</tbody>
</table>

<br/>

<table>
<thead>
<tr><th colspan="3">Wide tile (Wide310x150Logo)</th></tr>
</thead>
<tbody>
<tr>
    <td width="20%">100% scale</td>
    <td width="20%">310x150</td>
    <td>Wide310x150Logo.scale-100.png</td>
</tr>
<tr>
    <td>125% scale</td>
    <td>388x188</td>
    <td>Wide310x150Logo.scale-125.png</td>
</tr>
<tr>
    <td>150% scale</td>
    <td>465x225</td>
    <td>Wide310x150Logo.scale-150.png</td>
</tr>
<tr>
    <td>200% scale</td>
    <td>620x300</td>
    <td>Wide310x150Logo.scale-200.png</td>
</tr>
<tr>
    <td>400% scale</td>
    <td>1240x600</td>
    <td>Wide310x150Logo.scale-400.png</td>
</tr>
</tbody>
</table>

<br/>

<table>
<thead>
<tr><th colspan="3">Large tile (Square310x310Logo)</th></tr>
</thead>
<tbody>
<tr>
    <td width="20%">100% scale</td>
    <td width="20%">310x310</td>
    <td>Square310x310Logo.scale-100.png</td>
</tr>
<tr>
    <td>125% scale</td>
    <td>388x388</td>
    <td>Square310x310Logo.scale-125.png</td>
</tr>
<tr>
    <td>150% scale</td>
    <td>465x465</td>
    <td>Square310x310Logo.scale-150.png</td>
</tr>
<tr>
    <td>200% scale</td>
    <td>620x620</td>
    <td>Square310x310Logo.scale-200.png</td>
</tr>
<tr>
    <td>400% scale</td>
    <td>1240x1240</td>
    <td>Square310x310Logo.scale-400.png</td>
</tr>
</tbody>
</table>

<br/>

<table>
<thead>
<tr><th colspan="3">App list icon (Square44x44Logo)</th></tr>
</thead>
<tbody>
<tr>
    <td width="20%">100% scale</td>
    <td width="20%">44x44</td>
    <td>Square44x44Logo.scale-100.png</td>
</tr>
<tr>
    <td>125% scale</td>
    <td>55x55</td>
    <td>Square44x44Logo.scale-125.png</td>
</tr>
<tr>
    <td>150% scale</td>
    <td>66x66</td>
    <td>Square44x44Logo.scale-150.png</td>
</tr>
<tr>
    <td>200% scale</td>
    <td>88x88</td>
    <td>Square44x44Logo.scale-200.png</td>
</tr>
<tr>
    <td>400% scale</td>
    <td>176x176</td>
    <td>Square44x44Logo.scale-400.png</td>
</tr>
</tbody>
</table>

<br/>

<table>
<thead>
<tr><th colspan="3">Splash screen (SplashScreen)</th></tr>
</thead>
<tbody>
<tr>
    <td width="20%">100% scale</td>
    <td width="20%">620x300</td>
    <td>SplashScreen.scale-100.png</td>
</tr>
<tr>
    <td>125% scale</td>
    <td>775x375</td>
    <td>SplashScreen.scale-125.png</td>
</tr>
<tr>
    <td>150% scale</td>
    <td>930x450</td>
    <td>SplashScreen.scale-150.png</td>
</tr>
<tr>
    <td>200% scale</td>
    <td>1240x600</td>
    <td>SplashScreen.scale-200.png</td>
</tr>
<tr>
    <td>400% scale</td>
    <td>2480x1200</td>
    <td>SplashScreen.scale-400.png</td>
</tr>
</tbody>
</table>

<br/>
 

**Target-based assets**

Target-based assets are used across multiple scale factors. The element name for target-based assets is **Square44x44Logo**. We strongly recommend submitting the following assets as a bare minimum:

16x16, 24x24, 32x32, 48x48, 256x256

The following table lists all target-based asset sizes and corresponding file name examples:

| Asset size | File name example                  |
|------------|------------------------------------|
| 16x16\*    | Square44x44Logo.targetsize-16.png  |
| 24x24\*    | Square44x44Logo.targetsize-24.png  |
| 32x32\*    | Square44x44Logo.targetsize-32.png  |
| 48x48\*    | Square44x44Logo.targetsize-48.png  |
| 256x256\*  | Square44x44Logo.targetsize-256.png |
| 20x20      | Square44x44Logo.targetsize-20.png  |
| 30x30      | Square44x44Logo.targetsize-30.png  |
| 36x36      | Square44x44Logo.targetsize-36.png  |
| 40x40      | Square44x44Logo.targetsize-40.png  |
| 60x60      | Square44x44Logo.targetsize-60.png  |
| 64x64      | Square44x44Logo.targetsize-64.png  |
| 72x72      | Square44x44Logo.targetsize-72.png  |
| 80x80      | Square44x44Logo.targetsize-80.png  |
| 96x96      | Square44x44Logo.targetsize-96.png  |

 

\* Submit these asset sizes as a baseline

## Asset types


Listed here are all asset types, their uses, and recommended file names.

**Tile assets**

-   Centered assets are generally used on the Start to showcase your app.
-   File name format: [Square\Wide]\*x\*Logo.scale-\*.png
-   Impacted apps: Every UWP app
-   Uses:
    -   Default Start tiles (desktop and mobile)
    -   Action center (desktop and mobile)
    -   Task switcher (mobile)
    -   Share picker (mobile)
    -   Picker (mobile)
    -   Store

**Scalable list assets with plate**

-   These assets are used on surfaces that request scale factors. Assets either get plated by the system or come with their own background color if the app includes that.
-   File name format: Square44x44Logo.scale-\*.png
-   Impacted apps: Every UWP app
-   Uses:
    -   Start all apps list (desktop)
    -   Start most-frequently used list (desktop)
    -   Task manager (desktop)
    -   Cortana search results
    -   Start all apps list (mobile)
    -   Settings

**Target-size list assets with plate**

-   These are fixed asset sizes that don't scale with plateaus. Mostly used for legacy experiences. Assets are checked by the system.
-   File name format: Square44x44Logo.targetsize-\*.png
-   Impacted apps: Every UWP app
-   Uses:
    -   Start jump list (desktop)
    -   Start lower corner of tile (desktop)
    -   Shortcuts (desktop)
    -   Control Panel (desktop)

**Target-size list assets without plate**

-   These are assets that don't get plated or scaled by the system.
-   File name format: Square44x44Logo.targetsize-\*\_altform-unplated.png
-   Impacted apps: Every UWP app
-   Uses:
    -   Taskbar and taskbar thumbnail (desktop)
    -   Taskbar jumplist
    -   Task view
    -   ALT+TAB

**File extension assets**

-   These are assets specific to file extensions. They appear next to Win32-style file association icons in File Explorer and must be theme-agnostic. Sizing is different on desktop and mobile platforms.
-   File name format: \*LogoExtensions.targetsize-\*.png
-   Impacted apps: Music, Video, Photos, Microsoft Edge, Microsoft Office
-   Uses:
    -   File Explorer
    -   Cortana
    -   Various UI surfaces (desktop)

**Splash screen**

-   The asset that appears on your app's splash screen. Automatically scales on both desktop and mobile platforms.
-   File name format: SplashScreen.scale-*.png
-   Impacted apps: Every UWP app
-   Uses:
    -   App's splash screen