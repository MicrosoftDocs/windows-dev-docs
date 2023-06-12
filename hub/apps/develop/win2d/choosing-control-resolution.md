---
title: Choosing control resolution
description: An explanation of how to configure the resolution used by Win2D's XAML controls.
ms.date: 05/26/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Choosing control resolution

This article explains how to configure the resolution used by Win2D's XAML controls. It explains how to:

- Make Win2D controls run at a fixed resolution.
- Adjust control DPI to improve performance by rendering fewer pixels.

## Resolution and control sizing

"Resolution", as used in this document, is another word for the size of a bitmap. It consists of a width, and height.

The objects that Win2D's XAML controls draw to have a resolution. They also have a DPI. An object's DPI is a measurement of how dense the pixels of that object are, when drawn. DPI behaves like a scale factor- a high DPI increases the number of pixels that comprise the drawn object. On the other hand, lowering the DPI of an object means that it will span fewer pixels. For more information about Win2D's handling of DPI in general, see [this page](dpi-and-dips.md).

DPI-independent size is sometimes called "logical size". And a DPI-dependent size, in pixels, is called "physical size".

In terms of resolution and sizing, a control's default behavior when it's loaded is:
- The control's logical size is determined by its layout, as determined by where it falls in the XAML tree.
- A DPI is queried from the environment. The control's DPI is set to that.
- The amount of physical pixels that comprise the control's drawable area is determined by the control's size, scaled by its DPI.
  - On high DPI, the physical size will be greater (more pixels) compared to the logical size.
  - On low DPI, the physical size will be smaller (fewer pixels) compared to the logical size.
  - On default DPI, the physical size and logical size of the drawable area are the same.
- The control's drawing resource (`CanvasImageSource` for `CanvasControl`, `CanvasVirtualImageSource` for `CanvasVirtualControl` and `CanvasSwapChain` for `CanvasAnimatedControl`) is set to match the size and DPI of the control.

Most Win2D operations are in dips (DPI-independent units), and Win2D's XAML controls' drawing resources are automatically sized to take DPI into account. This means applications can often ignore DPI. Sizes and co-ordinates are always DPI-independent unless specified otherwise. An application can hard-code various sizes and co-ordinates at which things are drawn into the controls, and that content gets scaled when the app is run in environments with different DPIs.

But for some applications, the default behavior isn't sufficient. This article outlines a couple scenarios where the default is not sufficient, and what apps can do to fix it.

## Scenario: the control's contents must be a fixed, lower-than-normal resolution

This scenario may arise, for instance, on a 2D sprite game that should always render at a fixed 640x480 resolution, regardless of what actual display hardware it is run on.

Solving this doesn't strictly require writing any new Win2D code at all.

The [`Viewbox`](/uwp/api/Windows.UI.Xaml.Controls.Viewbox) XAML object lets you constrain the sizes of its child visual elements, automatically adding scaling, with letterboxing or pillarboxing to preseve aspect ratios as necessary.

Simply ensure your `CanvasControl`, `CanvasVirtualControl` or `CanvasAnimatedControl` is a child element of a `ViewBox`, and restrict the size of that control.

For example, to constrain the size of the control to 320 pixels wide, and 224 pixels high, regardless of DPI, then instead of:

```XAML
<canvas:CanvasAnimatedControl/>
```

Use:

```XAML
<Viewbox>
    <canvas:CanvasAnimatedControl  Width="320" Height="224"/>
</Viewbox>
```

If your app should not preserve the aspect ratio by adding letterboxing/pillarboxing, then add the `Stretch` attribute:

```XAML
<Viewbox Stretch="Fill">
    <canvas:CanvasAnimatedControl Width="320" Height="224"/>
</Viewbox>
```

Note that the scaling performed by the `Viewbox` element does not guarantee any control over the interpolation mode. The filtering method may look like `CanvasInterpolationMode.Linear`, or something similar. If your app needs a particular interpolation mode, then don't use `ViewBox` with a fixed-size control. Instead, draw to an intermediate, fixed-size `CanvasRenderTarget`, and use the desired interpolation mode to draw the scaled intermediate to the target.

## Scenario: the app cannot perform well at high resolutions

Some devices have very high-resolution displays, but their graphics processing unit is not powerful enough to animate that many pixels smoothly. Developers may not be readily aware of how their apps perform on these devices without testing them.

One option is to use the `DpiScale` property of the control to reduce the control's DPI.

For example, to fix the control at half-resolution, use:

```XAML
<canvas:CanvasAnimatedControl DpiScale="0.5" />
```

The actual DPI scale factor depends upon the needs of your app. One option is to compute a scale factor that will fix the app's DPI at 96, and no higher.

For example:

```csharp
float dpiLimit = 96.0f;

if(control.Dpi > dpiLimit)
{
    control.DpiScale = dpiLimit / control.Dpi;
}
```

To ensure this setting works across DPI changes, the application should subscribe to [`DisplayInformation.DpiChanged`](https://msdn.microsoft.com/library/windows/apps/windows.graphics.display.displayinformation.dpichanged) and use this logic in the handler to set the DPI scale against the new DPI.

This saves the app some perf overhead, exploiting the fact that users may not be able to easily percieve the reduced resolution on a high-DPI display.

The scaling performed in having a lower-than-native resolution control resource cannot guarantee control over the interpolation mode, similar to `ViewBox` mentioned above. If your app needs a particular interpolation mode, use an intermediate instead.