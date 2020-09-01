---
ms.assetid: EE0C1B28-EF9C-4BD9-A3C0-BDF11E75C752
description: This article explains how UWP apps can detect and respond to system-initiated changes in audio stream levels
title: Detect and respond to audio state changes
ms.date: 04/03/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Detect and respond to audio state changes
Starting with Windows 10, version 1803, your app can detect when the system lowers or mutes the audio level of an audio stream your app is using. You can receive notifications for capture and render streams, for a particular audio device and audio category, or for a [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) object your app is using for media playback. For example, the system may lower, or "duck", the audio playback level when an alarm is ringing. The system will mute your app when it goes into the background if your app has not declared the *backgroundMediaPlayback* capability in the app manifest. 

The pattern for handling audio state changes is the same for all supported audio streams. First, create an instance of the [**AudioStateMonitor**](/uwp/api/windows.media.audio.audiostatemonitor) class. In the following example, the app is using the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class to capture audio for game chat. A factory method is called to get an audio state monitor associated with the game chat audio capture stream of the default communications device.  Next, a handler is registered for the [**SoundLevelChanged**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevelchanged) event, which will be raised when the audio level for the associated stream is changed by the system.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetDeviceIdCategoryVars":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetSoundLevelDeviceIdCategory":::

In the **SoundLevelChanged** event handler, check the [**SoundLevel**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevel) property of the **AudioStateMonitor** sender passed into the handler to determine what the new audio level for the stream is. In this example, the app stops capturing audio when the sound level is muted and resumes capturing when the audio level returns to full volume.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetGameChatSoundLevelChanged":::

For more information on capturing audio with **MediaCapture**, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md).

Every instance of the [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) class has an **AudioStateMonitor** associated with it that you can use to detect when the system changes the volume level of the content currently being played. You may decide to handle audio state changes differently depending on what type of content is being played. For example, you may decide to pause playback of a podcast when the audio is lowered, but continue playback if the content is music. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetAudioStateVars":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSoundLevelChanged":::

For more information on using **MediaPlayer**, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md). 

## Related topics
