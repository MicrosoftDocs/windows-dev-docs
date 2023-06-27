---
title: Handling device lost
description: An explanation of how to correctly manage "device lost" scenarios with Win2D.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Handling device lost

"Device lost" refers to a situation where the GPU graphics device becomes unusable for further rendering. This can occur due to GPU hardware malfunction, driver bugs, driver software updates, or switching the app from one GPU to another. A lost device can no longer be used, and any attempt to do so from Win2D will throw an exception. To recover from this situation, the app must create a new device and then recreate all its graphics resources.

Not all apps bother trying to recover from device lost. This is a (hopefully!) rare situation, so some developers will just let their apps crash if it occurs. For those who prefer to handle device lost in a robust way, this article explains how.

## Device lost when using XAML controls

The Win2D controls ([`CanvasControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm), [`CanvasVirtualControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualControl.htm) and [`CanvasAnimatedControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl.htm)) attempt to automatically handle device lost on behalf of the app.

When device lost is detected, these controls will recreate their `CanvasDevice` and then raise the `CreateResources` event passing a [`CanvasCreateResourcesReason`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_CanvasCreateResourcesReason.htm) of [`NewDevice`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_CanvasCreateResourcesReason.htm). Apps should respond to this event by recreating all their graphics resources using the new device, and updating any data structures that may contain references to the old, no-longer-valid resources.

The controls can automatically catch and handle device lost exceptions that are thrown by their `CreateResources`, `Update`, `Draw`, or `RegionsInvalidated` event handlers. If you call Win2D drawing APIs from other places (for instance in a pointer or keyboard input event handler) then see the next section.

## Manually handling device lost

If you are [using Win2D without the built-in controls](https://microsoft.github.io/Win2D/WinUI2/html/WithoutControls.htm) or from outside the `CreateResources`, `Update` or `Draw` event handlers, then it is your responsibility to catch and report lost device exceptions. This is done with the [`IsDeviceLost(Int32)`](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasDevice_IsDeviceLost.htm) and [`RaiseDeviceLost()`](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasDevice_RaiseDeviceLost.htm) methods:

```csharp
try
{
    DrawStuff();
}
catch (Exception e) where canvasDevice.IsDeviceLost(e.ErrorCode)
{
    canvasDevice.RaiseDeviceLost();
}
```

Calling `RaiseDeviceLost` tells any controls that are sharing this device to initiate their lost device recovery path. If you got your device from somewhere other than a control, use the [`DeviceLost`](https://microsoft.github.io/Win2D/WinUI2/html/E_Microsoft_Graphics_Canvas_CanvasDevice_DeviceLost.htm) event to be informed when it is lost.

## How to test device lost handling

The easiest way to test that your app will do the right thing in response to device lost is to disable your hardware GPU while the app is running:

- Open Device Manager (Explorer window -> right-click 'This PC' -> 'Properties' -> 'Device Manager')
- Expand the 'Display adapters' node
- Right-click the entry for your GPU and click 'Disable'
This will bypass your hardware GPU, at which point Windows switches over to a WARP software rendering device, and all active apps experience a device lost.

Don't forget to reenable your GPU after performing this test! If you want to test device lost more than once, it's best to restart your app each time (always launch it with the GPU enabled). Repeatedly toggling the GPU on and off while an app is running doesn't reliably cause lost devices as it may just continue using the WARP software device.

While testing lost device handling, you probably also want to make sure your app deals correctly with dynamic [display DPI changes](./dpi-and-dips.md).