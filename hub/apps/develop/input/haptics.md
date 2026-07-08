---
description: Implement haptic feedback in Windows apps using InputHapticsManager, with C# examples for waveforms, sliders, and alignment.
title: Implement haptic feedback in Windows apps
ms.date: 07/08/2026
ms.topic: how-to
author: GrantMeStrength
ms.author: jken
keywords: windows 11, haptics, haptic feedback, InputHapticsManager, C#, implementation, waveforms
ms.localizationpriority: medium
---

# Implement haptic feedback in Windows apps

This article explains how to add haptic feedback to your Windows app using the [InputHapticsManager](/uwp/api/windows.devices.haptics.inputhapticsmanager) API, including code examples for common interaction patterns.

For design guidance on when and why to use haptics, see [Haptics design guidelines](../../design/signature-experiences/haptics.md).

## Supported devices

Haptics are supported on input devices such as mice, touchpads, and pens. Availability varies by model and manufacturer. Always check for support at runtime before calling haptics APIs (see [Check for support](#check-for-support) below).

## Requirements

| Requirement | Details |
|---|---|
| **Namespace** | `Windows.Devices.Haptics` |
| **Minimum OS** | Windows 11, SDK [10.0.28000.1721](/windows/apps/whats-new/release-notes) (March 2026) |
| **Align / Collide / Step / Grow waveforms** | SDK [10.0.28000.1839](/windows/apps/whats-new/release-notes) (April 2026) |
| **API contract** | `UniversalApiContract` version 19.0 |

To target earlier OS versions, use `ApiInformation.IsTypePresent` to guard all calls (see code below).

## Check for support

Always verify that the API is available and that a haptic device is present before use. The API is not present on older OS versions, and not all input devices support haptics.

```csharp
using Windows.Devices.Haptics;
using Windows.Foundation.Metadata;

// Guard against older OS versions where the API is absent.
bool apiPresent = ApiInformation.IsTypePresent("Windows.Devices.Haptics.InputHapticsManager");

// Check that a supported haptic device is connected.
bool supported = apiPresent && InputHapticsManager.IsSupported();
```

## Trigger a waveform

Call `InputHapticsManager.GetForCurrentThread()` on the UI thread to obtain a manager instance scoped to the current thread's input focus. Device detection and routing are handled automatically—you don't need to enumerate devices yourself.

```csharp
using Windows.Devices.Haptics;

if (supported)
{
    var mgr = InputHapticsManager.GetForCurrentThread();

    // The second parameter is a fallback waveform used if the first is
    // unsupported. Passing 0 lets the system choose a device-appropriate
    // fallback. TrySendHapticWaveform returns false if neither waveform is
    // supported; this is a non-fatal condition.
    bool sent = mgr.TrySendHapticWaveform(
        KnownSimpleHapticsControllerWaveforms.Align,
        0);
}
```

> [!IMPORTANT]
> Call `GetForCurrentThread()` from the UI thread (or the thread that owns input focus). Calling it from a background thread will not route feedback to the correct device.

## Stop playback

Call `TryStopFeedback` when a drag or interaction ends to halt any ongoing haptic playback.

```csharp
using Windows.Devices.Haptics;

if (supported)
{
    InputHapticsManager
        .GetForCurrentThread()
        .TryStopFeedback();
}
```

## Implementation examples

### Object alignment

Alignment guides are common in design tools, presentation editors, and diagramming apps. Haptic feedback adds a second channel so users can *feel* when objects snap into place—reducing the need to zoom in to visually confirm alignment.

**When to fire:** Use the alignment guide's appearance as the trigger. When your snap logic shows a guide, fire the haptic. If no guide appears, don't fire. This keeps haptic events tightly coupled to visible system feedback.

**Avoid duplicates:** A shape can align to multiple guides simultaneously (for example, left edge and top edge). Treat this as a single interaction—track active guides and only fire when a *new* guide appears.

```csharp
using Windows.Devices.Haptics;

private readonly HashSet<int> _activeGuides = new();

// Call this after computing which alignment guides are currently visible.
void OnGuidesUpdated(IEnumerable<int> currentGuideIds)
{
    var currentGuides = currentGuideIds.ToHashSet();

    bool hasNewGuide = currentGuides.Except(_activeGuides).Any();

    // Update the tracked set in place (field is readonly).
    _activeGuides.Clear();
    _activeGuides.UnionWith(currentGuides);

    if (hasNewGuide && supported)
    {
        InputHapticsManager.GetForCurrentThread()
            .TrySendHapticWaveform(
                KnownSimpleHapticsControllerWaveforms.Align,
                0);
    }
}
```

### Sliders

Haptics improve slider interactions by anchoring feedback to meaningful positions. Always tie feedback to visible markers the user can understand—tick marks, named values, or range boundaries.

**When to fire:** Fire haptics as the thumb crosses each tick mark. Avoid continuous feedback between ticks, which can feel distracting.

```csharp
using Windows.Devices.Haptics;

double _previousValue;
const double TickInterval = 25.0;

void OnSliderValueChanged(double newValue)
{
    int previousTick = (int)(_previousValue / TickInterval);
    int currentTick  = (int)(newValue       / TickInterval);

    if (currentTick != previousTick && supported)
    {
        InputHapticsManager.GetForCurrentThread()
            .TrySendHapticWaveform(
                KnownSimpleHapticsControllerWaveforms.Step,
                0);
    }

    _previousValue = newValue;
}
```

**Refining the experience:** Scale haptic intensity based on slider position to help users sense *where* they are within the range. Use `TrySendHapticWaveformForPlayCount`, which accepts an explicit intensity value from `0.0` to `1.0`.

```csharp
void OnSliderValueChangedWithIntensity(double newValue, double maxValue)
{
    int previousTick = (int)(_previousValue / TickInterval);
    int currentTick  = (int)(newValue       / TickInterval);

    if (currentTick != previousTick && supported)
    {
        double intensity = newValue / maxValue; // Scale 0.0–1.0

        InputHapticsManager.GetForCurrentThread()
            .TrySendHapticWaveformForPlayCount(
                KnownSimpleHapticsControllerWaveforms.Step,
                0,          // fallback waveform
                intensity,
                1,          // playCount
                TimeSpan.Zero);
    }

    _previousValue = newValue;
}
```

### Drag interactions and intent detection

During a drag, an object can cross many alignment boundaries—page center, guides, other objects. Triggering haptics at every crossing can overwhelm users, especially during fast movements when they are repositioning rather than trying to be precise.

Use *intent detection* to fire feedback only when the user is trying to align deliberately, not just passing through.

#### Approach 1: Speed filter

Suppress haptics when the cursor is moving quickly, reserving feedback for slower, more deliberate movement.

- **Stabilize the signal.** Avoid using raw cursor deltas, which can be noisy across input types, DPI settings, and display configurations. Normalize and smooth the signal.
- **Choose a threshold carefully.** Validate across different devices, input types, display scales, and multi-display setups.

#### Approach 2: Timer debounce

When an object enters a trigger zone, start a short timer (50 ms is a reasonable starting point). If the object remains in the zone when the timer completes, treat it as intentional and fire the haptic. If the object moves on before the timer fires, suppress the feedback.

- **Simpler to implement** and tends to produce more stable behavior than speed filtering.
- A 50 ms delay is long enough to distinguish deliberate alignment from a quick pass, while remaining below the threshold of noticeable latency.

```csharp
using System;
using Microsoft.UI.Xaml;
using Windows.Devices.Haptics;

private DispatcherTimer? _alignTimer;

void OnObjectEnteredAlignmentZone()
{
    _alignTimer?.Stop();
    _alignTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
    _alignTimer.Tick += (_, _) =>
    {
        _alignTimer.Stop();
        if (supported)
        {
            InputHapticsManager.GetForCurrentThread()
                .TrySendHapticWaveform(
                    KnownSimpleHapticsControllerWaveforms.Align,
                    0);
        }
    };
    _alignTimer.Start();
}

void OnObjectLeftAlignmentZone()
{
    _alignTimer?.Stop();
}
```

#### Choosing between the two

| | Speed filter | Timer debounce |
|---|---|---|
| **How it works** | Reactive—measures movement in real time | Predictive—waits briefly to confirm intent |
| **Feel** | Can feel more immediate | Slightly delayed, but more consistent |
| **Tuning effort** | Higher—requires careful calibration | Lower—a single delay value is usually sufficient |
| **Recommended for** | Apps requiring highly responsive feedback | Most apps; good default starting point |

## Related articles

- [Haptics design guidelines](../../design/signature-experiences/haptics.md)
- [InputHapticsManager API reference](/uwp/api/windows.devices.haptics.inputhapticsmanager)
- [KnownSimpleHapticsControllerWaveforms](/uwp/api/windows.devices.haptics.knownsimplehapticscontrollerwaveforms)
- [Haptics design and implementation](https://microsoft.design/articles/haptics-design-and-implementation/) (microsoft.design)
- [Input primer](input-primer.md)
- [Touch interactions](touch-interactions.md)
