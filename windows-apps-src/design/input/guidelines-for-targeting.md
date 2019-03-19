---
Description: This topic describes the use of contact geometry for touch targeting and provides best practices for targeting in Windows Runtime apps.
title: Targeting
ms.assetid: 93ad2232-97f3-42f5-9e45-3fc2143ac4d2
label: Targeting
template: detail.hbs
ms.date: 03/11/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Guidelines for touch targets
The default touch target size for UWP is about creating confidence and comfort for users. The size of elements on the screen has been optimized in UWP allowing developers to create well balanced applications, while ensuring users are can interact with on screen elements with ease.

## Fluent standard sizing

*Fluent standard sizing* was created to provide a balance between information density and user comfort. Effectively, all items on the screen align to a 40x40 effective pixels (epx) target, which lets UI elements align to a grid and scale appropriately based on system level scaling.

In general, set your touch target size to 7.5mm square range (40x40 pixels on a 135 PPI display at a 1.0x scaling plateau). On average UWP controls align with 7.5mm touch target. Based on the specific control and common usage patterns the touch target may vary.

> [!NOTE]
>For more info on effective pixels and scaling, see [Introduction to UWP app design](../basics/design-and-ui-intro.md#effective-pixels-and-scaling)
>
> For more info on system level scaling, see [Alignment, margin, padding](../layout/alignment-margin-padding.md).

## Fluent compact sizing

Applications can display a higher level of information density with *Fluent compact sizing*. Compact sizing aligns UI elements to a 32x32 epx target, which lets UI elements to align to a tighter grid and scale appropriately based on system level scaling.

## Target size

In general, set your touch target size to 7.5mm square range (40x40 pixels on a 135 PPI display at a 1.0x scaling plateau). On average UWP controls align with 7.5mm touch target (this can vary based on the specific control and any common usage patterns).

These target size recommendations can be adjusted as required by your particular scenario. Here are some things to consider:

- Frequency of Touches - consider making targets that are repeatedly or frequently pressed larger than the minimum size.
- Error Consequence - targets that have severe consequences if touched in error should have greater padding and be placed further from the edge of the content area. This is especially true for targets that are touched frequently.
- Position in the content area.
- Form factor and screen size.
- Finger posture.
- Touch visualizations.

## Related articles

- [Introduction to UWP app design](../basics/design-and-ui-intro.md)
- [Alignment, margin, padding](../layout/alignment-margin-padding.md)

### Samples

- [Basic input sample](https://go.microsoft.com/fwlink/p/?LinkID=620302)
- [Low latency input sample](https://go.microsoft.com/fwlink/p/?LinkID=620304)
- [User interaction mode sample](https://go.microsoft.com/fwlink/p/?LinkID=619894)
- [Focus visuals sample](https://go.microsoft.com/fwlink/p/?LinkID=619895)

### Archive samples

- [Input: XAML user input events sample](https://go.microsoft.com/fwlink/p/?linkid=226855)
- [Input: Device capabilities sample](https://go.microsoft.com/fwlink/p/?linkid=231530)
- [Input: Touch hit testing sample](https://go.microsoft.com/fwlink/p/?linkid=231590)
- [XAML scrolling, panning, and zooming sample](https://go.microsoft.com/fwlink/p/?linkid=251717)
- [Input: Simplified ink sample](https://go.microsoft.com/fwlink/p/?linkid=246570)
- [Input: Windows 8 gestures sample](https://go.microsoft.com/fwlink/p/?LinkId=264995)
- [Input: Manipulations and gestures (C++) sample](https://go.microsoft.com/fwlink/p/?linkid=231605)
- [DirectX touch input sample](https://go.microsoft.com/fwlink/p/?LinkID=231627)
