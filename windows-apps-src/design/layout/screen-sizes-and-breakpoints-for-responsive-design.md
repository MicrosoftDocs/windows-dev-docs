---
author: mijacobs
title: Screen sizes and break points for responsive design
description: Rather than optimizing your UI the many devices across the Windows 10 ecosystem, we recommended designing for a few key width categories called breakpoints.
ms.author: mijacobs
ms.date: 08/30/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

#  Screen sizes and breakpoints

UWP apps can run on any device running Windows 10, which includes phones, tablets, desktops, TVs, and more. With a huge number of device targets and screen sizes across the Windows 10 ecosystem, rather than optimizing your UI for each device, we recommended designing for a few key width categories (also called "breakpoints"): 
- Small (smaller than 640px)
- Medium (641px to 1007px)
- Large (1008px and larger)

> [!TIP]
> When designing for specific breakpoints, design for the amount of screen space available to your app (the app's window), not the screen size. When the app is running full-screen, the app window is the same size as the screen, but when the app is not full-screen, the window is smaller than the screen.

## Breakpoints
This table describes the different size classes and breakpoints.

![responsive design breakpoints](images/rsp-design/rspd-breakpoints.png)

<table>
<thead>
<tr class="header">
<th align="left">Size class</th>
<th align="left">Breakpoints</th>
<th align="left">Screen size (diagonal)</th>
<th align="left">Devices</th>
<th align="left">Window sizes</th>
</tr>
</thead>
<tbody>
<tr class="even">
<td style="vertical-align:top;">Small</td>
<td style="vertical-align:top;">640px or less</td>
<td style="vertical-align:top;">4&quot; to 6&quot;</td>
<td style="vertical-align:top;">Phones</td>
<td style="vertical-align:top;">320x569, 360x640, 480x854</td>
</tr>
<tr class="odd">
<td style="vertical-align:top;">Medium</td>
<td style="vertical-align:top;">641px to 1007px</td>
<td style="vertical-align:top;">7&quot; to 12&quot;</td>
<td style="vertical-align:top;">Phablets, tablets, TVs</td>
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
- Consider further tailoring for [TV experiences](http://go.microsoft.com/fwlink/?LinkId=760736).

### Large
- Set left and right window margins to 24px to create a visual separation between the left and right edges of the app window.
- Put command elements like [app bars](../controls-and-patterns/app-bars.md) at the top of the app window.
- Use up to three columns/regions.
- Show the search box.
- Put the [navigation pane](../controls-and-patterns/navigationview.md) into docked mode so that it always shows.

>[!TIP] 
> With [**Continuum for Phones**](http://go.microsoft.com/fwlink/p/?LinkID=699431), users can connect compatible Windows 10 mobile devices to a monitor, mouse and keyboard to make their phones work like laptops. Keep this new capability in mind when designing for specific breakpoints - a mobile phone will not always stay in the size class.

## Effective pixels and scale factor

UWP apps automatically scale your UI to guarantee that your app will be legible on all Windows 10 devices. Windows automatically scales for each display based on its DPI (dots-per-inch) and the viewing distance of the device. Users can override the default value and by going to **Settings** > **Display** > **Scale and layout** settings page. 
