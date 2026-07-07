---
title: AnimatedVisualPlayer
description: Display and control Lottie animations in WinUI 3 apps using the AnimatedVisualPlayer control and the LottieGen code generator.
template: detail.hbs
ms.topic: how-to
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# AnimatedVisualPlayer

The `AnimatedVisualPlayer` control plays vector-based animations in a WinUI 3 app. It is designed for Lottie animations — high-quality, scalable animations created in Adobe After Effects or other applications and exported to JSON format.

Use `AnimatedVisualPlayer` when you need full programmatic control over playback: play, pause, stop, reverse, loop, or seek to a specific frame. For animations tied to visual state changes (like hover effects on buttons), use [`AnimatedIcon`](animated-icon.md) instead.

> [!div class="checklist"]
>
> - **Important APIs**: [AnimatedVisualPlayer class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.animatedvisualplayer)

## Prerequisites

To use Lottie animations with `AnimatedVisualPlayer`, you need:

- The [Lottie-Windows NuGet package](https://www.nuget.org/packages/CommunityToolkit.WinUI.Lottie) from the Windows Community Toolkit
- A Lottie JSON file exported from Adobe After Effects (or a compatible tool)
- The [LottieGen tool](/windows/communitytoolkit/animations/lottie-scenarios/getting_started_codegen) to convert the JSON file into a C# or C++/WinRT class

The `LottieGen` tool takes a `.json` Lottie file as input and generates a class that implements `IAnimatedVisualSource`. You then reference that class in XAML as the animation source.

## Add an animated visual player

Place an `AnimatedVisualPlayer` in XAML and set its source to the generated Lottie class:

```xaml
<AnimatedVisualPlayer x:Name="Player" AutoPlay="False">
    <animatedvisuals:LottieLogo1/>
</AnimatedVisualPlayer>
```

Where `animatedvisuals` is the XML namespace for the namespace containing your generated Lottie class.

Set `AutoPlay="True"` if you want the animation to start immediately when the control loads.

## Control playback

Use the `PlayAsync`, `Pause`, `Resume`, and `Stop` methods to control the animation:

```csharp
// Play the animation forward
Player.PlaybackRate = 1;
await Player.PlayAsync(fromProgress: 0, toProgress: 1, looped: false);

// Pause and resume
Player.Pause();
Player.Resume();

// Stop and reset to the first frame
Player.Stop();

// Play in reverse
Player.PlaybackRate = -1;
await Player.PlayAsync(fromProgress: 1, toProgress: 0, looped: false);
```

The `PlaybackRate` property is live — you can change it while an animation is playing. A value of `1` plays forward at normal speed; `-1` plays in reverse.

> [!NOTE]
> Calling `Pause` does not complete the `PlayAsync` task. Call `Stop` to complete it and reset to the initial frame.

## Loop an animation

To play an animation in a continuous loop, set the `looped` parameter on `PlayAsync` to `true`:

```csharp
await Player.PlayAsync(fromProgress: 0, toProgress: 1, looped: true);
```

## Is this the right control?

Use `AnimatedVisualPlayer` when you need:
- Full control over animation playback (play, pause, stop, reverse, seek)
- Looping animations that run independently of control state
- Animations that play once in response to a one-time event

Use [`AnimatedIcon`](animated-icon.md) instead when:
- The animation responds to visual state transitions (hover, pressed, focused)
- You are adding an icon to a control that accepts `IconElement` or `IconElementSource`

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see AnimatedVisualPlayer in action](winui3gallery://item/AnimatedVisualPlayer)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

## Related articles

- [AnimatedIcon](animated-icon.md)
- [Lottie-Windows overview](/windows/communitytoolkit/animations/lottie)
- [LottieGen getting started](/windows/communitytoolkit/animations/lottie-scenarios/getting_started_codegen)
