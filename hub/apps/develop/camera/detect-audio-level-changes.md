---
title: "Detect and respond to audio level changes by the system"
description: Learn how to detect and respond to audio level changes by the system.  
ms.topic: article
ms.date: 08/07/2024
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
#customer intent: As a developer, I want to detect and respond to audio level changes by the system in a Windows app using WinUI 3.
---

# Detect and respond to audio level changes by the system

Learn how to detect and respond to audio level changes by the system. Starting with Windows 10, version 1803, your app can detect when the system lowers or mutes the audio level of your app's audio capture and audio render streams. For example, the system may mute your app's streams when it goes into the background.

To learn about capturing audio using the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md). For information about audio playback using [MediaPlayer](/uwp/api/windows.media.playback.mediaplayer), see TBD.


The [**AudioStateMonitor**](/uwp/api/windows.media.audio.audiostatemonitor) class allows you to register to receive an event when the system modifies the volume of an audio capture or render stream. Get an instance of **AudioStateMonitor** for monitoring audio capture streams by calling [**CreateForCaptureMonitoring**](/uwp/api/windows.media.audio.audiostatemonitor.createforcapturemonitoring#Windows_Media_Audio_AudioStateMonitor_CreateForCaptureMonitoring). Get an instance for monitoring audio render streams by calling [**CreateForRenderMonitoring**](/uwp/api/windows.media.audio.audiostatemonitor.createforrendermonitoring). Register a handler for the [**SoundLevelChanged**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevelchanged) event of each monitor to be notified when the audio for the corresponding stream category is changed by the system.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetAudioStateVars":::

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetRegisterAudioStateMonitor":::

In the **SoundLevelChanged** handler for the capture stream, you can check the [**SoundLevel**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevel) property of the **AudioStateMonitor** sender to determine the new sound level. Note that a capture stream should never be lowered, or "ducked", by the system. It should only ever be muted or switched back to full volume. If the audio stream is muted, you can stop a capture in progress. If the audio stream is restored to full volume, you can start capturing again.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCaptureSoundLevelChanged":::

The following code example illustrates an implementation of the **SoundLevelChanged** handler for audio rendering. Depending on your app scenario, and the type of content you are playing, you may want to pause audio playback when the sound level is ducked.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetRenderSoundLevelChanged":::

## Related topics

[Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)

