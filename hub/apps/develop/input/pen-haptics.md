---
title: Pen haptic feedback in Windows apps
description: Learn how to use haptic feedback from pen devices to provide tactile inking and interaction experiences in your Windows App SDK desktop app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/10/2026
---

# Pen haptic (tactile) feedback

Learn how to use haptic feedback from pen devices to provide more natural and tactile interactions in your Windows App SDK app.

Windows 11 introduced haptic feedback for digital pens, letting users feel their pen interacting with the app's UI. When referring to this feature, *haptic* is used in developer APIs and documentation, while *tactile* is the user-facing name shown in Windows Settings.

## Key APIs

- [PenDevice](/uwp/api/windows.devices.input.pendevice)
- [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller)
- [KnownSimpleHapticsControllerWaveforms](/uwp/api/windows.devices.haptics.knownsimplehapticscontrollerwaveforms)
- [PenDeviceInterop](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop) (Windows App SDK)

## Haptic feedback types

Two types of haptic feedback are available:

- **Inking feedback** — Simulates the feel of writing or drawing tools (pen, marker, pencil, highlighter) through continuous vibrations while the pen touches the screen.
- **Interaction feedback** — Short tactile signals for user actions such as hovering over a button, clicking, or confirming a completed action.

## Detect pen input

Register for the `PointerEntered` event and check whether the pointer device type is a pen:

```csharp
private void InputObserver_PointerEntered(object sender, PointerRoutedEventArgs e)
{
    if (e.Pointer.PointerDeviceType != Microsoft.UI.Input.PointerDeviceType.Pen)
    {
        return;
    }

    // Pen detected — check for haptic support.
}
```

## Get the PenDevice in the Windows App SDK

In a Windows App SDK app, use `PenDeviceInterop` &mdash; a Windows App SDK interop class &mdash; to get the WinRT `PenDevice` object from a pointer point. `PenDevice`, `SimpleHapticsController`, and `KnownSimpleHapticsControllerWaveforms` are WinRT types from the `Windows.Devices.Input` and `Windows.Devices.Haptics` namespaces; only the interop call itself comes from the Windows App SDK's `Microsoft.UI.Input.Interop` namespace:

```csharp
using Microsoft.UI.Input.Interop;
using Windows.Devices.Haptics;
using Windows.Devices.Input;
using Windows.Foundation.Metadata;

private void InputObserver_PointerEntered(object sender, PointerRoutedEventArgs e)
{
    if (e.Pointer.PointerDeviceType != Microsoft.UI.Input.PointerDeviceType.Pen)
    {
        return;
    }

    var penDevice = PenDeviceInterop.FromPointerPoint(e.GetCurrentPoint(null));

    if (penDevice == null)
    {
        return; // Pen does not support advanced features.
    }

    // SimpleHapticsController requires Windows 11. Check for API presence
    // before accessing it so the app doesn't throw on Windows 10.
    if (!ApiInformation.IsPropertyPresent(
        "Windows.Devices.Input.PenDevice", "SimpleHapticsController"))
    {
        return; // Haptic feedback requires Windows 11.
    }

    var hapticsController = penDevice.SimpleHapticsController;
    if (hapticsController == null)
    {
        return; // Pen does not support haptic feedback.
    }

    // Configure haptic feedback (see examples below).
}
```

> [!NOTE]
> In UWP apps, use `PenDevice.GetFromPointerId` instead. The `PenDeviceInterop` class is specific to the Windows App SDK.

## Inking waveforms

Inking waveforms play continuously while the pen contacts the screen. Required waveforms are guaranteed to be supported by any haptic pen.

| Waveform | Description | Required |
|---|---|---|
| InkContinuous | Default ball-point pen feel. Fallback for unsupported waveforms. | Yes |
| BrushContinuous | Brush tool | No |
| PencilContinuous | Pencil tool | No |
| MarkerContinuous | Marker tool | No |
| ChiselMarkerContinuous | Chisel marker / highlighter | No |
| EraserContinuous | Eraser tool | No |
| GalaxyPenContinuous | Special ink (multi-colored brush) | No |

## Interaction waveforms

Interaction waveforms provide short, direct feedback for UI actions.

| Waveform | Description | Required |
|---|---|---|
| Click | Default short click. Fallback for unsupported interactions. | Yes |
| Error | Action failed or error occurred | No |
| Hover | Started hovering over an interactive element | No |
| Press | Pressed an interactive element | No |
| Release | Released an interactive element | No |
| Success | Action succeeded | No |
| BuzzContinuous | Continuous buzzing | No |
| RumbleContinuous | Continuous rumbling | No |

## Send haptic feedback

Iterate through `SupportedFeedback` to find the desired waveform, then call `SendHapticFeedback`:

```csharp
SimpleHapticsControllerFeedback currentWaveform = null;

// Try to use BrushContinuous, fall back to InkContinuous.
foreach (var waveform in hapticsController.SupportedFeedback)
{
    if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.BrushContinuous)
    {
        currentWaveform = waveform;
        break;
    }
}

if (currentWaveform == null)
{
    foreach (var waveform in hapticsController.SupportedFeedback)
    {
        if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.InkContinuous)
        {
            currentWaveform = waveform;
            break;
        }
    }
}

if (currentWaveform != null)
{
    hapticsController.SendHapticFeedback(currentWaveform);
}
```

## Stop haptic feedback

Always stop haptic feedback when the pointer exits the element. Register for `PointerExited` on the same element:

```csharp
private void Canvas_PointerExited(object sender, PointerRoutedEventArgs e)
{
    hapticsController?.StopFeedback();
}
```

> [!NOTE]
> Some pens stop haptics automatically when the pen leaves the screen. However, not all pens do this, so always call `StopFeedback` explicitly.

## Customize haptic feedback

Some pens support additional customizations. Check support before using them:

| Property | Check support with |
|---|---|
| Intensity | `IsIntensitySupported` |
| Play count | `IsPlayCountSupported` |
| Play duration | `IsPlayDurationSupported` |
| Replay pause interval | `IsReplayPauseIntervalSupported` |

```csharp
if (hapticsController.IsIntensitySupported)
{
    hapticsController.SendHapticFeedback(currentWaveform, 0.75); // 75% intensity
}
```

## Related articles

- [Pen and stylus interactions](pen-and-stylus-interactions.md)
