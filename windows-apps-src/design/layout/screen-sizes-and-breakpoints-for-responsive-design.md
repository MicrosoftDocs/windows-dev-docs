---
author: mijacobs
title: Screen sizes and break points for responsive design
description: .
ms.assetid: BF42E810-CDC8-47D2-9C30-BAA19DCBE2DA
label: Screen sizes and break points
template: detail.hbs
op-migration-status: ready
ms.author: mijacobs
ms.date: 08/30/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

#  Screen sizes and break points for responsive design



The number of device targets and screen sizes across the Windows 10 ecosystem is too great to worry about optimizing your UI for each one. Instead, we recommended designing for a few key width categories (also called "breakpoints"): small (640px and smaller), medium (641px to 1007px), and large (1008px and larger).

> [!TIP]
> When designing for specific breakpoints, design for the amount of screen space available to your app (the app's window). When the app is running full-screen, the app window is the same size as the screen, but in other cases, it's smaller.
 

## Breakpoints
This table describes the different size classes and provides general recommendations for tailoring for those size classes.

![responsive design breakpoints](images/rsp-design/rspd-breakpoints.png)

<table>

<thead>
<tr class="header">
<th align="left">Size class</th>
<th align="left">small</th>
<th align="left">medium</th>
<th align="left">large</th>
</tr>
</thead>
<tbody>
<tr class="even">
<td style="vertical-align:top;">Window width breakpoints in effective pixels</td>
<td style="vertical-align:top;">640px or less</td>
<td style="vertical-align:top;">641px to 1007px</td>
<td style="vertical-align:top;">1008px or greater</td>
</tr>
<tr class="odd">
<td style="vertical-align:top;">Typical screen size (diagonal)</td>
<td style="vertical-align:top;">4&quot; to 6&quot;</td>
<td style="vertical-align:top;">7&quot; to 12&quot;, or TVs</td>
<td style="vertical-align:top;">13&quot; and larger</td>
</tr>
<tr class="even">
<td style="vertical-align:top;">Typical devices</td>
<td style="vertical-align:top;">Phones</td>
<td style="vertical-align:top;">Phablets, tablets, TVs</td>
<td style="vertical-align:top;">PCs, laptops, Surface Hubs</td>
</tr>
<tr class="odd">
<td style="vertical-align:top;">Common window sizes in effective pixels</td>
<td style="vertical-align:top;">320x569, 360x640, 480x854</td>
<td style="vertical-align:top;">960x540</td>
<td style="vertical-align:top;">1024x640, 1366x768, 1920x1080</td>
</tr>


<tr class="odd">
<td style="vertical-align:top;">General recommendations</td>
<td style="vertical-align:top;"><ul>
<li>Set left and right window margins to 12px to create a visual separation between the left and right edges of the app window.</li>
<li>Dock [app bars](../controls-and-patterns/app-bars.md) to the bottom of the window for improved reachability</li>
<li>Use one column/region at a time</li>
<li>Use an icon to represent search (don't show a search box).</li>
<li>Put the [navigation pane](../controls-and-patterns/navigationview.md) in overlay mode to conserve screen space.</li>
<li>If you're using the [master details pattern](../controls-and-patterns/master-details.md), use the stacked presentation mode to save screen space.</li>
</ul></td>
<td style="vertical-align:top;"><ul>
<li>Set left and right window margins to 24px to create a visual separation between the left and right edges of the app window.</li>
<li>Put command elements like [app bars](../controls-and-patterns/app-bars.md) at the top of the app window.</li>
<li>Up to two columns/regions</li>
<li>Show the search box.</li>
<li>Put the [navigation pane](../controls-and-patterns/navigationview.md) into sliver mode so a narrow strip of icons always shows.</li>
<li>Consider further tailoring for [TV experiences](http://go.microsoft.com/fwlink/?LinkId=760736).</li>
</ul></td>
<td style="vertical-align:top;"><ul>
<li>Set left and right window margins to 24px to create a visual separation between the left and right edges of the app window.</li>
<li>Put command elements like [app bars](../controls-and-patterns/app-bars.md) at the top of the app window.</li>
<li>Up to three columns/regions</li>
<li>Show the search box.</li>
<li>Put the [navigation pane](../controls-and-patterns/navigationview.md) into docked mode so that it always shows.</li>
</ul></td>
</tr>
</tbody>
</table>

With [**Continuum for Phones**](http://go.microsoft.com/fwlink/p/?LinkID=699431), users can connect compatible Windows 10 mobile devices to a monitor, mouse and keyboard to make their phones work like laptops. Keep this new capability in mind when designing for specific breakpoints - a mobile phone will not always stay in the small size class.

## Effective pixels and scale factor

The scale factor determines the size of text and UI elements on the screen. Larger values increase the number of pixels the system uses to draw certain UI elements. Windows automatically selects a scale factor for each display based on its DPI (dots-per-inch) and the viewing distance of the device. Users can override the default value and by going to **Settings** > **Display** > **Scale and layout** settings page. 


 
