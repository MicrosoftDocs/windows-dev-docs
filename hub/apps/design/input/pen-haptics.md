---
description: Build Windows apps that support haptic feedback from pen devices to provide more natural and tactile interactions and experiences.
title: Pen interactions and haptic feedback
label: Pen interactions and haptic feedback
template: detail.hbs
keywords: Windows, ink, user interaction, input, pen, haptic, tactile
ms.date: 09/17/2021
ms.topic: article
ms.localizationpriority: medium
---

# Pen interactions and haptic (tactile) feedback

Windows has long supported digital pens that let users interact with their devices in a natural, direct fashion and to express their creativity through rich writing and drawing experiences using digital ink.

With Windows 11, a new capability is being introduced that makes the digital pen experience even more natural and compelling: When using a pen that supports "haptic feedback", users can actually feel their pen interacting in a tactile manner with the user interface (UI) of an app.

> [!NOTE]
> When referring to this new feature, "haptic" is used throughout the developer APIs and related documentation, while "tactile" is the friendly name presented to users for setting feedback preferences in Windows Settings.

Haptic feedback experiences supported in Windows 11 include *inking feedback* and *interaction feedback*:

- Inking feedback simulates the feel of various types of writing or drawing tools (such as pen, marker, pencil, highlighter, and so on) through continuous vibrations while the pen is in contact with the screen. By default, the [Windows Ink Platform](pen-and-stylus-interactions.md) supports haptic feedback for all drawing tools (this topic covers how to provide a custom inking solution beyond the one supported by Windows Ink).
- Interaction feedback, on the other hand, is direct feedback based on key user actions such as hovering over or clicking a button, responding to the completion of an action, or to draw the user's attention.

Typically, five steps are required to fully support haptic feedback:

- Detect pen input.
- Determine if the current pen and device support haptic feedback and, if so, what haptic feedback features it supports.
- Decide on the haptic feedback signal to send.
- Send the haptic feedback.
- Stop the haptic feedback

## Detect pen input

To detect and isolate pen input, you must first register for the [PointerEntered](/uwp/api/windows.ui.xaml.uielement.pointerentered) event and then check whether the [PointerDeviceType](/uwp/api/windows.devices.input.pointerdevice.pointerdevicetype) is a [pen](/uwp/api/windows.devices.input.pointerdevicetype).

The following code shows how to check the pointer device type within a PointerEntered event. For this example, if the input is not from a pen we simply return from the event handler. Otherwise, we check the pen capabilities and configure the haptic feedback.

```csharp

private void InputObserver_PointerEntered(object sender, PointerRoutedEventArgs e)
{
    ...
    
    // If the current Pointer device is not a pen, exit.
    if (e.Pointer.PointerDeviceType != PointerDeviceType.Pen) 
    {
       return;
    }
    
    ...    
}
```

## Determine support for haptic feedback

Not all pens and digitizers support haptic feedback, and the pens that do won't necessarily support all haptic feedback features described in this topic. As such, it is important to programmatically confirm which features are supported by the active pen.

In a continuation of the preceding example, we show how to check whether the active pen supports haptic feedback.

We first attempt to retrieve a [PenDevice](/uwp/api/windows.devices.input.pendevice) object from the current [PointerId](/uwp/api/windows.ui.input.pointerpoint.pointerid). If a [PenDevice](/uwp/api/windows.devices.input.pendevice) cannot be obtained, we simply return from the event handler.

If a [PenDevice](/uwp/api/windows.devices.input.pendevice) was obtained, we test if it supports a [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) property. If not, we again simply return from the event handler.

```csharp
// Attempt to retrieve the PenDevice from the current PointerId.
penDevice = PenDevice.GetFromPointerId(e.Pointer.PointerId);

// If a PenDevice cannot be retrieved based on the PointerId, it does not support 
// advanced pen features, such as haptic feedback. 
if (penDevice == null)
{
    return;
}

// Check to see if the current PenDevice supports haptic feedback by seeing if it 
// has a SimpleHapticsController.
hapticsController = penDevice.SimpleHapticsController;
if (hapticsController == null)
{
    return;
}
```

The [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) retrieved in the preceding example is used in subsequent examples to query haptic capabilities and to send/stop haptic feedback.

> [!NOTE]
> If you are building apps with the [Windows App SDK Preview 1.0](../../windows-app-sdk/index.md), you can use [PenDevice interop](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop) ([PenDeviceInterop.FromPointerPoint(PointerPoint)](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop.frompointerpoint)) to access the system [PenDevice](/uwp/api/windows.devices.input.pendevice).
>
> ```csharp
> private void InputObserver_PointerEntered(PointerInputObserver sender, PointerEventArgs args)
> {
>     var penDevice = PenDeviceInterop.PenDeviceFromPointerPoint(args.CurrentPoint);
> }
> ```

The following sections describe the feedback features that haptic pens must support, as well as those that are optional. A required haptic feedback type can typically be used as a fallback instead of an optional feature.

### Inking waveforms

Inking waveforms play continuously while the pen is in contact with the screen, and attempt to simulate the feel of various writing or drawing tools.

| Feature | Description | Required / Optional |
|---|---|---|
| InkContinous waveform | Simulates the feel of inking with a physical ball point pen. This is the default fallback when an inking waveform is not supported by a haptic pen. | Required |
| BrushContinuous waveform | Continuous haptic signal when user selects brush as inking tool. | Optional |
| ChiselMarkerContinuous waveform | Continuous haptic signal when user selects chisel marker/highlighter as inking tool. | Optional |
| EraserContinuous waveform | Continuous haptic signal when user selects eraser as inking tool. | Optional |
| GalaxyContinuous waveform<br/>(the HID documentation and implementation guide refers to this waveform as *SparkleContinuous*) | Continuous haptic signal for special ink tools, such as a multi-colored brush. | Optional |
| MarkerContinuous waveform | Continuous haptic signal when user selects marker as inking tool. | Optional |
| PencilContinuous waveform | Continuous haptic signal when user selects pencil as inking tool. | Optional |

### Interaction waveforms

Interaction waveforms are typically short (exceptions noted in the following table), direct feedback waveforms generated on demand to confirm key actions such as hovering over or clicking a button, responding to the completion of an action, or to draw the user's attention.

| Feature | Description | Required / Optional |
|---|---|---|
| Click waveform | A short "click" feedback. This is the default fallback when an interaction waveform selected by the app is not supported by a haptic pen.  | Required |
| Error waveform | A strong signal to alert the user that an action failed, or an error has occurred.  | Optional |
| Hover waveform | Indicates that the user has started hovering over an interactive UI element. | Optional |
| Press waveform | Indicates when a user presses an interactive UI element in an incremental action (see Release). | Optional |
| Release waveform | Indicates when a user releases an interactive UI element in an incremental action (see Press).  | Optional |
| Success waveform | Strong signal to alert the user that an action succeeded. | Optional |
| BuzzContinuous waveform | Continuous buzzing sensation. | Optional |
| RumbleContinuous waveform | Continuous rumbling sensation. | Optional |

### Haptic feedback customizations

Some haptic pens can support the following customizations.

| Feature | Description | Required / Optional |
|---|---|---|
| Intensity | Sets the intensity of the haptic signal. | Optional |
| Play Count | Repeats a haptic signal a specified number of times. | Optional |
| Replay Pause Interval | Sets the time between each repeated playing of the haptic signal. | Optional |
| Play Duration | Sets the interval of time that a haptic signal is played. | Optional |

## Check for custom settings support

To check for Intensity, Play Count, Replay Pause Interval, and Play Duration support, use the following properties of the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller):

- [IsIntensitySupported](/uwp/api/windows.devices.haptics.simplehapticscontroller.isintensitysupported)
- [IsPlayCountSupported](/uwp/api/windows.devices.haptics.simplehapticscontroller.isplaycountsupported)
- [IsPlayDurationSupported](/uwp/api/windows.devices.haptics.simplehapticscontroller.isplaydurationsupported)
- [IsReplayPauseIntervalSupported](/uwp/api/windows.devices.haptics.simplehapticscontroller.isreplaypauseintervalsupported)

## Send and stop inking haptic feedback

Use the [SendHapticFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.sendhapticfeedback) method of the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) object to pass inking waveforms to the user's pen. This method supports passing in either a waveform or both a waveform with a customized intensity value (see [Customize haptic feedback](#customize-haptic-feedback)).

Call SendHapticFeedback and pass in an [inking waveform](#inking-waveforms) to configure the pen to start playing that waveform as soon as the tip of the pen touches anywhere on the screen. The waveform will continue playing until the pen is lifted or [StopFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.stopfeedback) is called, whichever happens first. We recommend doing this in the [PointerEntered](/uwp/api/windows.ui.xaml.uielement.pointerentered) event handler for the element in which you want haptics to be played. For example, an app with a custom inking implementation would do this in the PointerEntered method of its inking canvas.

To retrieve the desired [inking waveform](#inking-waveforms), you must iterate through the [SupportedFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.supportedfeedback) collection of the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller), ensuring it is supported by the active pen.

If it is not supported, you can either choose to not play anything at all or fall back to the [InkContinuous](#inking-waveforms) waveform, as that is guaranteed to be supported.

In the following example, we attempt to send the [BrushContinuous](/uwp/api/windows.devices.haptics.knownsimplehapticscontrollerwaveforms) waveform (but fall back to InkContinuous if BrushContinuous is not supported).

```csharp
SimpleHapticsControllerFeedback currentWaveform;

// Attempt to set the currentWaveform to BrushContinuous.
foreach (var waveform in hapticsController.SupportedFeedback)
{
    if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.BrushContinuous)
    {
        currentWaveform = waveform;
    }
} 

// If currentWaveform is null, it was not in the SupportedFeedback collection, so instead set 
// the waveform to InkContinuous.
if (currentWaveform == null)
{
    foreach (var waveform in hapticsController.SupportedFeedback)
    {
        if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.InkContinuous)
        {
            currentWaveform = waveform;
        }
    }
}

// Send the currentWaveform 
hapticsController.SendHapticFeedback(currentWaveform);
```

It is important that you also stop haptic feedback when the associated pointer exits the element you registered for haptic feedback. Otherwise, the waveform will continue attempting to play on the active pen.

> [!NOTE]
> Some pens may optionally stop haptics on their own when the pen leaves the range of the screen. However, it is not required for all pens to do this, so applications should always explicitly stop haptic feedback as described here.

To stop haptic feedback on an element, register for the [PointerExited](/uwp/api/windows.ui.xaml.uielement.pointerexited) event on same element as you registered the PointerEntered handler that sent the haptic signal. In that exited event handler, call [StopFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.stopfeedback) as shown here.

```csharp
hapticsController.StopFeedback();
```

## Send and stop interaction feedback

Sending Interaction feedback is quite similar to sending inking feedback.

Use the [SendHapticFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.sendhapticfeedback) method of the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) object to pass interaction waveforms to the user's pen. This method supports passing in either a waveform or both a waveform with a customized intensity value (see [Customize haptic feedback](#customize-haptic-feedback)).

Call SendHapticFeedback and pass in an [inking waveform](#inking-waveforms) to configure the pen to start playing that waveform immediately based on some interaction within your app (instead of when the tip of the pen touches the screen for inking feedback).

When using any of the non-continuous Interaction waveforms, it is not necessary to make a corresponding [StopFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.stopfeedback) call. You do still need to call [StopFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.stopfeedback) for the continuous Interaction waveforms.

> [!NOTE]
> Sending an interaction waveform when an inking waveform is being played will temporarily interrupt the inking waveform. The inking waveform will resume when the interaction waveform stops.

To retrieve the desired [interaction waveform](#inking-waveforms), you must iterate through the [SupportedFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.supportedfeedback) collection of the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller), ensuring it is supported by the active pen.

If it is not supported, you can either choose to not play anything at all or fall back to the [Click](#interaction-waveforms) waveform, as that is guaranteed to be supported.

In the following example, we attempt to send the [Error](/uwp/api/windows.devices.haptics.knownsimplehapticscontrollerwaveforms) waveform (but fall back to Click if Error is not supported).

```csharp
SimpleHapticsControllerFeedback currentWaveform;  

// Attempt to set the currentWaveform to BrushContinuous.
foreach (var waveform in hapticsController.SupportedFeedback)
{
    if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.Error)
    {
        currentWaveform = waveform;
    }
} 

// If currentWaveform is null, it was not in the SupportedFeedback collection, so instead set 
// the waveform to Click.
if (currentWaveform == null)
{
    foreach (var waveform in hapticsController.SupportedFeedback)
    {
        if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.Click)
        {
            currentWaveform = waveform;
        }
    }
} 

// Send the currentWaveform.
hapticsController.SendHapticFeedback(currentWaveform); 
```

## Customize haptic feedback

There are three ways to customize haptic feedback. The first is supported by both Inking and Interaction feedback, while the second and third are only supported by Interaction feedback.

1. Adjust the intensity of the feedback relative to the maximum system intensity setting. To do this, you must first check to ensure that the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) supports setting the intensity and then call [SendHapticFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.sendhapticfeedback) with the desired `Intensity` value.

    ```csharp
    if (hapticsController.IsIntensitySupported) 
    {
        foreach (var waveform in hapticsController.SupportedFeedback)
        {
            if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.Click)
            {
                double intensity = 0.75;
                hapticsController.SendHapticFeedback(waveform, intensity);
            }
        }
    }
    ```

2. Repeat the haptic signal a specified number of times. To do this, you must first check to ensure that the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) supports setting the intensity and then call [SendHapticFeedbackForPlayCount](/uwp/api/windows.devices.haptics.simplehapticscontroller.sendhapticfeedbackforplaycount) with the desired count value. You can also set both the intensity and the replay pause interval.

    > [!NOTE]
    > If the SimpleHapticsController does not support setting the intensity or the replay pause interval, the supplied values will be ignored.

    ```csharp
    if (hapticsController.IsPlayCountSupported && hapticsController.IsIntensitySupported && hapticsController.IsReplayPauseIntervalSupported)
    {
        foreach (var waveform in hapticsController.SupportedFeedback)
        {
            if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.Click)
            {
                double intensity = 0.75;
                int playCount = 3;
                System.TimeSpan pauseDuration = new System.TimeSpan(1000000);
                hapticsController.SendHapticFeedbackForPlayCount(currentWaveform, intensity, playCount, pauseDuration);
            }
        }
    }
    ```

3. Set the duration of the haptic signal.  To do this, you must first check to ensure that the [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) supports setting the play duration and then call [SendHapticFeedbackForDuration](/uwp/api/windows.devices.haptics.simplehapticscontroller.sendhapticfeedbackforduration) with the desired time interval value. You can also set the intensity.

    > [!NOTE]
    > If the SimpleHapticsController does not support setting the intensity, the supplied value will be ignored.

    ```csharp
    if (hapticsController.IsPlayDurationSupported && hapticsController.IsIntensitySupported)
    {
        foreach (var waveform in hapticsController.SupportedFeedback)
        {
            if (waveform.Waveform == KnownSimpleHapticsControllerWaveforms.RumbleContinuous)
            {
                double intensity = 0.75;
                System.TimeSpan playDuration = new System.TimeSpan(5000000);
                hapticsController.SendHapticFeedbackForDuration(currentWaveform, intensity, playDuration);
            }
        }
    }
    ```

## Examples

See the [Pen haptics sample](https://nam06.safelinks.protection.outlook.com/?url=https%3A%2F%2Fgithub.com%2Fmicrosoft%2FWindows-universal-samples%2Ftree%2Fdev%2FSamples%2FPenHaptics&data=04%7C01%7Ckbridge%40microsoft.com%7C2f06b98f321a40a2f94908d97e08126d%7C72f988bf86f141af91ab2d7cd011db47%7C1%7C0%7C637679395422669594%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C1000&sdata=EKfcBhnsRiy1nRG1U5As7QbjK81rZwjz3ihsz0rxV70%3D&reserved=0) for working examples of the following functionality:

- Get a [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) from pen input: Go from [PointerId](/uwp/api/windows.ui.input.pointerpoint.pointerid) to [PenDevice](/uwp/api/windows.devices.input.pendevice) to [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) (requires both a haptic-capable pen and a device that supports the pen).
- Check pen haptics capabilities: A [SimpleHapticsController](/uwp/api/windows.devices.haptics.simplehapticscontroller) exposes properties for pen hardware capabilities, including [IsIntensitySupported](/uwp/api/windows.devices.haptics.simplehapticscontroller.isintensitysupported), [IsPlayCountSupported](/uwp/api/windows.devices.haptics.simplehapticscontroller.isplaycountsupported), [SupportedFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.supportedfeedback), and so on.
- Start and stop haptic feedback: Use the [SendHapticFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.sendhapticfeedback) and [StopFeedback](/uwp/api/windows.devices.haptics.simplehapticscontroller.stopfeedback) methods appropriately.
- Trigger haptic feedback: Feedback for both *inking feedback* and *interaction feedback*.
