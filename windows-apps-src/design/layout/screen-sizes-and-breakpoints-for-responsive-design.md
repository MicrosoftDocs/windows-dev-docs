---
title: Screen sizes and break points for responsive design
description: Rather than optimizing your UI the many devices across the Windows 10 ecosystem, we recommended designing for a few key width categories called breakpoints.
ms.date: 08/30/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: RS5
---
#  Screen sizes and breakpoints

Windows apps can run on any device running Windows, which includes phones, tablets, desktops, TVs, and more. With a huge number of device targets and screen sizes across the Windows 10 ecosystem, rather than optimizing your UI for each device, we recommended designing for a few key width categories (also called "breakpoints"): 
- Small (smaller than 640px)
- Medium (641px to 1007px)
- Large (1008px and larger)

> [!TIP]
> When designing for specific breakpoints, design for the amount of screen space available to your app (the app's window), not the screen size. When the app is running full-screen, the app window is the same size as the screen, but when the app is not full-screen, the window is smaller than the screen.

## Breakpoints
This table describes the different size classes and breakpoints.

![Responsive design breakpoints](images/breakpoints/size-classes.svg)

<table>
<thead>
<tr class="header">
<th align="left">Size class</th>
<th align="left">Breakpoints</th>
<th align="left">Typical screen size (diagonal)</th>
<th align="left">Devices</th>
<th align="left">Window sizes</th>
</tr>
</thead>
<tbody>
<tr class="even">
<td style="vertical-align:top;">Small</td>
<td style="vertical-align:top;">640px or less</td>
<td style="vertical-align:top;">4&quot; to 6&quot;; 20&quot; to 65&quot;</td>
<td style="vertical-align:top;">Phones, TVs</td>
<td style="vertical-align:top;">320x569, 360x640, 480x854</td>
</tr>
<tr class="odd">
<td style="vertical-align:top;">Medium</td>
<td style="vertical-align:top;">641px to 1007px</td>
<td style="vertical-align:top;">7&quot; to 12&quot;</td>
<td style="vertical-align:top;">Phablets, tablets</td>
<td style="vertical-align:top;">960x540</td>
</tr>
<tr class="even">
<td style="vertical-align:top;">Large</td>
<td style="vertical-align:top;">1008px or greater</td>
<td style="vertical-align:top;">13&quot; and larger</td>
<td style="vertical-align:top;">PCs, laptops, Surface Hubs</td>
<td style="vertical-align:top;">1024x640, 1366x768, 1920x1080</td>
</tr>
</tbody>
</table>

## Why are TVs considered "small"? 

While most TVs are physically quite large (40 to 65 inches is common) and have high resolutions (HD or 4k), designing for a 1080P TV that you view from 10 feet away is different from designing for a 1080p monitor sitting a foot away on your desk. When you account for distance, the TV's 1080 pixels are more like a 540-pixel monitor that's much closer.

UWP's effective pixel system automatically takes viewing distance in account for you. When you specify a size for a control or a breakpoint range, you're actually using "effective" pixels. For example, if you create responsive code for 1080 pixels and higher, a 1080 monitor will use that code, but a 1080p TV will not--because although a 1080p TV has 1080 physical pixels, it only has 540 effective pixels. Which makes designing for a TV similar to designing for a phone.

## Effective pixels and scale factor

UWP apps automatically scale your UI to guarantee that your app will be legible on all Windows 10 devices. Windows automatically scales for each display based on its DPI (dots-per-inch) and the viewing distance of the device. Users can override the default value and by going to **Settings** > **Display** > **Scale and layout** settings page. 


## General recommendations

### Small
- Set left and right window margins to 12px to create a visual separation between the left and right edges of the app window.
- Dock [app bars](../controls-and-patterns/app-bars.md) to the bottom of the window for improved reachability.
- Use one column/region at a time.
- Use an icon to represent search (don't show a search box).
- Put the [navigation pane](../controls-and-patterns/navigationview.md) in overlay mode to conserve screen space.
- If you're using the [master details pattern](../controls-and-patterns/master-details.md), use the stacked presentation mode to save screen space.

### Medium
- Set left and right window margins to 24px to create a visual separation between the left and right edges of the app window.
- Put command elements like [app bars](../controls-and-patterns/app-bars.md) at the top of the app window.
- Use up to two columns/regions.
- Show the search box.
- Put the [navigation pane](../controls-and-patterns/navigationview.md) into sliver mode so a narrow strip of icons always shows.
- Consider further tailoring for [TV experiences](../devices/designing-for-tv.md).

### Large
- Set left and right window margins to 24px to create a visual separation between the left and right edges of the app window.
- Put command elements like [app bars](../controls-and-patterns/app-bars.md) at the top of the app window.
- Use up to three columns/regions.
- Show the search box.
- Put the [navigation pane](../controls-and-patterns/navigationview.md) into docked mode so that it always shows.