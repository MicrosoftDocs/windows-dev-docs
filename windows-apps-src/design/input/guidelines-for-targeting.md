---
author: Karl-Bridge-Microsoft
Description: This topic describes the use of contact geometry for touch targeting and provides best practices for targeting in Windows Runtime apps.
title: Targeting
ms.assetid: 93ad2232-97f3-42f5-9e45-3fc2143ac4d2
label: Targeting
template: detail.hbs
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Guidelines for targeting


Touch targeting in Windows uses the full contact area of each finger that is detected by a touch digitizer. The larger, more complex set of input data reported by the digitizer is used to increase precision when determining the user's intended (or most likely) target.

> **Important APIs**: [**Windows.UI.Core**](https://msdn.microsoft.com/library/windows/apps/br208383), [**Windows.UI.Input**](https://msdn.microsoft.com/library/windows/apps/br242084), [**Windows.UI.Xaml.Input**](https://msdn.microsoft.com/library/windows/apps/br227994)

This topic describes the use of contact geometry for touch targeting and provides best practices for targeting in UWP apps.

## Measurements and scaling


To remain consistent across different screen sizes and pixel densities, all target sizes are represented in physical units (millimeters). Physical units can be converted to pixels by using the following equation:

Pixels = Pixel Density × Measurement

The following example uses this formula to calculate the pixel size of a 9 mm target on a 135 pixel per inch (PPI) display at a 1x scaling plateau:

Pixels = 135 PPI × 9 mm

Pixels = 135 PPI × (0.03937 inches per mm × 9 mm)

Pixels = 135 PPI × 0.35433 inches

Pixels = 48 pixels

This result must be adjusted according to each scaling plateau defined by the system.

## Thresholds


Distance and time thresholds may be used to determine the outcome of an interaction.

For example, when a touch-down is detected, a tap is registered if the object is dragged less than 2.7 mm from the touch-down point and the touch is lifted within 0.1 second or less of the touch-down. Moving the finger beyond this 2.7 mm threshold results in the object being dragged and either selected or moved (for more information, see [Guidelines for cross-slide](guidelines-for-cross-slide.md)). Depending on your app, holding the finger down for longer than 0.1 second may cause the system to perform a self-revealing interaction (for more information, see [Guidelines for visual feedback](guidelines-for-visualfeedback.md)).

## Target sizes


In general, set your touch target size to 9 mm square or greater (48x48 pixels on a 135 PPI display at a 1.0x scaling plateau). Avoid using touch targets that are less than 7 mm square.

The following diagram shows how target size is typically a combination of a visual target, actual target size, and any padding between the actual target and other potential targets.

![diagram showing the recommended sizes for the visual target, actual target, and padding.](images/targeting-size.png)

The following table lists the minimum and recommended sizes for the components of a touch target.

<table>
<colgroup>
<col width="33%" />
<col width="33%" />
<col width="33%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Target component</th>
<th align="left">Minimum size</th>
<th align="left">Recommended size</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">Padding</td>
<td align="left">2 mm</td>
<td align="left">Not applicable.</td>
</tr>
<tr class="even">
<td align="left">Visual target size</td>
<td align="left">&lt; 60% of actual size</td>
<td align="left">90-100% of actual size
<p>Most users won't realize a visual target is touchable if it's less than 4.2 mm square (60% of the recommended minimum target size of 7 mm).</p></td>
</tr>
<tr class="odd">
<td align="left">Actual target size</td>
<td align="left">7 mm square</td>
<td align="left">Greater than or equal to 9 mm square (48 x 48 px @ 1x)</td>
</tr>
<tr class="even">
<td align="left">Total target size</td>
<td align="left">11 x 11 mm (approximately 60 px: three 20-px grid units @ 1x)</td>
<td align="left">13.5 x 13.5 mm (72 x 72 px @ 1x)
<p>This implies that the size of the actual target and padding combined should be larger than their respective minimums.</p></td>
</tr>
</tbody>
</table>

 

These target size recommendations can be adjusted as required by your particular scenario. Some of the considerations that went into these recommendations include:

-   Frequency of Touches: Consider making targets that are repeatedly or frequently pressed larger than the minimum size.
-   Error Consequence: Targets that have severe consequences if touched in error should have greater padding and be placed further from the edge of the content area. This is especially true for targets that are touched frequently.
-   Position in the content area
-   Form factor and screen size
-   Finger posture
-   Touch visualizations
-   Hardware and touch digitizers

## Targeting assistance


Windows provides targeting assistance to support scenarios where the minimum size or padding recommendations presented here are not applicable; for example, hyperlinks on a webpage, calendar controls, drop down lists and combo boxes, or text selection.

These targeting platform improvements and user interface behaviors work together with visual feedback (disambiguation UI) to improve user accuracy and confidence. For more information, see [Guidelines for visual feedback](guidelines-for-visualfeedback.md).

If a touchable element must be smaller than the recommended minimum target size, the following techniques can be used to minimize the targeting issues that result.

## Tethering


Tethering is a visual cue (a connector from a contact point to the bounding rectangle of an object) used to indicate to a user that they are connected to, and interacting with, an object even though the input contact isn't directly in contact with the object. This can occur when:

-   A touch contact was first detected within some proximity threshold to an object and this object was identified as the most likely target of the contact.
-   A touch contact was moved off an object but the contact is still within a proximity threshold.

This feature is not exposed to UWP app using JavaScript developers.

## Scrubbing


Scrubbing means to touch anywhere within a field of targets and slide to select the desired target without lifting the finger until it is over the desired target. This is also referred to as "take-off activation", where the object that is activated is the one that was last touched when the finger was lifted from the screen.

Use the following guidelines when you design scrubbing interactions:

-   Scrubbing is used in conjunction with disambiguation UI. For more information, see [Guidelines for visual feedback](guidelines-for-visualfeedback.md).
-   The recommended minimum size for a scrubbing touch target is 20 px (3.75 mm @ 1x size).
-   Scrubbing takes precedence when performed on a pannable surface, such as a webpage.
-   Scrubbing targets should be close together.
-   An action is canceled when the user drags a finger off a scrubbing target.
-   Tethering to a scrubbing target is specified if the actions performed by the target are non-destructive, such as switching between dates on a calendar.
-   Tethering is specified in a single direction, horizontally or vertically.

## Related articles


**Samples**
* [Basic input sample](http://go.microsoft.com/fwlink/p/?LinkID=620302)
* [Low latency input sample](http://go.microsoft.com/fwlink/p/?LinkID=620304)
* [User interaction mode sample](http://go.microsoft.com/fwlink/p/?LinkID=619894)
* [Focus visuals sample](http://go.microsoft.com/fwlink/p/?LinkID=619895)

**Archive samples**
* [Input: XAML user input events sample](http://go.microsoft.com/fwlink/p/?linkid=226855)
* [Input: Device capabilities sample](http://go.microsoft.com/fwlink/p/?linkid=231530)
* [Input: Touch hit testing sample](http://go.microsoft.com/fwlink/p/?linkid=231590)
* [XAML scrolling, panning, and zooming sample](http://go.microsoft.com/fwlink/p/?linkid=251717)
* [Input: Simplified ink sample](http://go.microsoft.com/fwlink/p/?linkid=246570)
* [Input: Windows 8 gestures sample](http://go.microsoft.com/fwlink/p/?LinkId=264995)
* [Input: Manipulations and gestures (C++) sample](http://go.microsoft.com/fwlink/p/?linkid=231605)
* [DirectX touch input sample](http://go.microsoft.com/fwlink/p/?LinkID=231627)
 

 




