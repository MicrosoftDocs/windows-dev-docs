---
description: This article explains how apps can detect and respond to system-initiated changes in audio stream levels using AudioStateMonitor.
title: Detect and respond to audio state changes
ms.date: 05/12/2026
ms.topic: how-to
---

# Detect and respond to audio state changes

Your app can detect when the system lowers or mutes the audio level of an audio stream your app is using. You can receive notifications for capture and render streams, for a particular audio device and audio category, or for a [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) object your app is using for media playback. For example, the system may lower, or "duck", the audio playback level when an alarm is ringing or when another app opens a communications audio stream (such as a VoIP call).

> [!NOTE]
> On desktop, audio ducking is triggered when the system detects an active communications audio stream—for example, a call using the [**AudioRenderCategory.Communications**](/uwp/api/windows.media.render.audiorendercategory) category. The behavior is controlled by the **Communications** tab in the Windows Sound settings panel. Some communication apps (such as Microsoft Teams) manage their own audio mixing and may not trigger system-level ducking.

The pattern for handling audio state changes is the same for all supported audio streams. First, create an instance of the [**AudioStateMonitor**](/uwp/api/windows.media.audio.audiostatemonitor) class. In the following example, the app is using the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class to capture audio for game chat. A factory method is called to get an audio state monitor associated with the game chat audio capture stream of the default communications device. Next, a handler is registered for the [**SoundLevelChanged**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevelchanged) event, which is raised when the audio level for the associated stream is changed by the system.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/audio-state-winui/cs/AudioStateWinUI/MainWindow.xaml.cs" id="SnippetDeviceIdCategoryVars":::

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/audio-state-winui/cs/AudioStateWinUI/MainWindow.xaml.cs" id="SnippetSoundLevelDeviceIdCategory":::

In the **SoundLevelChanged** event handler, check the [**SoundLevel**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevel) property of the **AudioStateMonitor** sender passed into the handler to determine the new audio level for the stream. In this example, the app stops capturing audio when the sound level is muted and resumes capturing when the audio level returns to full volume.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/audio-state-winui/cs/AudioStateWinUI/MainWindow.xaml.cs" id="SnippetGameChatSoundLevelChanged":::

For more information on capturing audio with **MediaCapture**, see [Basic photo, video, and audio capture with MediaCapture](../camera/basic-photo-capture.md).

Every instance of the [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) class has an [**AudioStateMonitor**](/uwp/api/windows.media.playback.mediaplayer.audiostatemonitor) associated with it that you can use to detect when the system changes the volume level of the content currently being played. You may decide to handle audio state changes differently depending on what type of content is being played. For example, you may decide to pause playback of a podcast when the audio is lowered, but continue playback if the content is music.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/audio-state-winui/cs/AudioStateWinUI/MainWindow.xaml.cs" id="SnippetAudioStateVars":::

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/audio-state-winui/cs/AudioStateWinUI/MainWindow.xaml.cs" id="SnippetSoundLevelChanged":::

For more information on using **MediaPlayer**, see [Play audio and video with MediaPlayer](../media-playback/play-audio-and-video-with-mediaplayer.md).

## Related topics

- [Basic photo, video, and audio capture with MediaCapture](../camera/basic-photo-capture.md)
- [Play audio and video with MediaPlayer](../media-playback/play-audio-and-video-with-mediaplayer.md)
