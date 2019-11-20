---
Description: This topic describes the use of contact geometry for touch targeting and provides best practices for targeting in Windows Runtime apps.
title: Targeting
ms.assetid: 93ad2232-97f3-42f5-9e45-3fc2143ac4d2
label: Targeting
template: detail.hbs
ms.date: 03/18/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Guidelines for touch targets

All interactive UI elements in your Universal Windows Platform (UWP) application must be large enough for users to accurately access and use, regardless of device type or input method.

Supporting touch input (and the relatively imprecise nature of the touch contact area) requires further optimization with respect to target size and control layout as the larger, more complex set of input data reported by the touch digitizer is used to determine the user's intended (or most likely) target.

All UWP controls have been designed with default touch target sizes and layouts that enable you to build visually balanced and appealing apps that are comfortable, easy to use, and inspire confidence.

In this topic, we describe these default behaviors so you can design your app for maximum usability using both platform controls and custom controls (should your app require them).

> **Important APIs**: [**Windows.UI.Core**](https://docs.microsoft.com/uwp/api/Windows.UI.Core), [**Windows.UI.Input**](https://docs.microsoft.com/uwp/api/Windows.UI.Input), [**Windows.UI.Xaml.Input**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Input)

## Fluent Standard sizing

*Fluent Standard sizing* was created to provide a balance between information density and user comfort. Effectively, all items on the screen align to a 40x40 effective pixels (epx) target, which lets UI elements align to a grid and scale appropriately based on system level scaling.

> [!NOTE]
>For more info on effective pixels and scaling, see [Introduction to UWP app design](../basics/design-and-ui-intro.md#effective-pixels-and-scaling)
>
> For more info on system level scaling, see [Alignment, margin, padding](../layout/alignment-margin-padding.md).

## Fluent Compact sizing

Applications can display a higher level of information density with *Fluent Compact sizing*. Compact sizing aligns UI elements to a 32x32 epx target, which lets UI elements to align to a tighter grid and scale appropriately based on system level scaling.

### Examples

Compact sizing can be applied at the page or grid level.

### Page level

```xaml
<Page.Resources>
    <ResourceDictionary Source="ms-appx:///Microsoft.UI.Xaml/DensityStyles/Compact.xaml" />
</Page.Resources>
```

### Grid level

```xaml
<Grid>
    <Grid.Resources>
        <ResourceDictionary Source="ms-appx:///Microsoft.UI.Xaml/DensityStyles/Compact.xaml" />
    </Grid.Resources>
</Grid>
```

## Target size

In general, set your touch target size to 7.5mm square range (40x40 pixels on a 135 PPI display at a 1.0x scaling plateau). Typically, UWP controls align with 7.5mm touch target (this can vary based on the specific control and any common usage patterns). See [Control size and density](../style/spacing.md) for more detail.

These target size recommendations can be adjusted as required by your particular scenario. Here are some things to consider:

- Frequency of Touches - consider making targets that are repeatedly or frequently pressed larger than the minimum size.
- Error Consequence - targets that have severe consequences if touched in error should have greater padding and be placed further from the edge of the content area. This is especially true for targets that are touched frequently.
- Position in the content area.
- Form factor and screen size.
- Finger posture.
- Touch visualizations.

## Related articles

- [Introduction to UWP app design](../basics/design-and-ui-intro.md)
- [Control size and density](../style/spacing.md)
- [Alignment, margin, padding](../layout/alignment-margin-padding.md)

### Samples

- [Basic input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicInput)
- [Low latency input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LowLatencyInput)
- [User interaction mode sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/UserInteractionMode)
- [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals)

### Archive samples

- [Input: XAML user input events sample](https://code.msdn.microsoft.com/windowsapps/Input-3dff271b)
- [Input: Device capabilities sample](https://code.msdn.microsoft.com/windowsapps/Input-device-capabilities-31b67745)
- [Input: Touch hit testing sample](https://code.msdn.microsoft.com/windowsapps/Touch-Hit-Testing-sample-5e35c690)
- [XAML scrolling, panning, and zooming sample](https://code.msdn.microsoft.com/windowsapps/xaml-scrollviewer-pan-and-949d29e9)
- [Input: Simplified ink sample](https://code.msdn.microsoft.com/windowsapps/Input-simplified-ink-sample-11614bbf)
- [Input: Windows 8 gestures sample](https://docs.microsoft.com/samples/browse/?redirectedfrom=MSDN-samples)
- [Input: Manipulations and gestures (C++) sample](https://code.msdn.microsoft.com/windowsapps/Manipulations-and-gestures-362b6b59)
- [DirectX touch input sample](https://code.msdn.microsoft.com/windowsapps/Simple-Direct3D-Touch-f98db97e)
