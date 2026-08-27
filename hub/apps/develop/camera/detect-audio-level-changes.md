---
title: "Detect and respond to audio level changes by the system"
description: Learn how to detect and respond to audio level changes by the system.  
ms.topic: article
ms.date: 08/23/2026
ms.localizationpriority: medium
#customer intent: As a developer, I want to detect and respond to audio level changes by the system in a Windows app using WinUI.
---

# Detect and respond to audio level changes by the system

Learn how to detect and respond to audio level changes by the system. Starting with Windows 10, version 1803, your app can detect when the system lowers or mutes the audio level of your app's audio capture and audio render streams. For example, the system may mute your app's streams when it goes into the background.

To learn about capturing audio using the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md). For information about audio playback using [MediaPlayer](/uwp/api/windows.media.playback.mediaplayer), see [Play audio and video with MediaPlayer](/windows/uwp/audio-video-camera/play-audio-and-video-with-mediaplayer).


The [**AudioStateMonitor**](/uwp/api/windows.media.audio.audiostatemonitor) class allows you to register to receive an event when the system modifies the volume of an audio capture or render stream. Get an instance of **AudioStateMonitor** for monitoring audio capture streams by calling [**CreateForCaptureMonitoring**](/uwp/api/windows.media.audio.audiostatemonitor.createforcapturemonitoring#Windows_Media_Audio_AudioStateMonitor_CreateForCaptureMonitoring). Get an instance for monitoring audio render streams by calling [**CreateForRenderMonitoring**](/uwp/api/windows.media.audio.audiostatemonitor.createforrendermonitoring). Register a handler for the [**SoundLevelChanged**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevelchanged) event of each monitor to be notified when the audio for the corresponding stream category is changed by the system.

```csharp
AudioStateMonitor captureAudioStateMonitor;
AudioStateMonitor renderAudioStateMonitor;
```

```csharp
captureAudioStateMonitor = AudioStateMonitor.CreateForCaptureMonitoring();
captureAudioStateMonitor.SoundLevelChanged += CaptureAudioStateMonitor_SoundLevelChanged;

renderAudioStateMonitor = AudioStateMonitor.CreateForRenderMonitoring();
renderAudioStateMonitor.SoundLevelChanged += RenderAudioStateMonitor_SoundLevelChanged;
```

In the **SoundLevelChanged** handler for the capture stream, you can check the [**SoundLevel**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevel) property of the **AudioStateMonitor** sender to determine the new sound level. Note that a capture stream should never be lowered, or "ducked", by the system. It should only ever be muted or switched back to full volume. If the audio stream is muted, you can stop a capture in progress. If the audio stream is restored to full volume, you can start capturing again.

```csharp
bool isCapturingAudio = false;
bool capturingStoppedForAudioState = false;

private void CaptureAudioStateMonitor_SoundLevelChanged(AudioStateMonitor sender, object args)
{
    switch (sender.SoundLevel)
    {
        case SoundLevel.Full:
            if (capturingStoppedForAudioState)
            {
                MyStartAudioCapture();
                capturingStoppedForAudioState = false;
            }
            break;
        case SoundLevel.Muted:
            if (isCapturingAudio)
            {
                MyStopAudioCapture();
                capturingStoppedForAudioState = true;
            }
            break;
        case SoundLevel.Low:
            // This should never happen for capture
            Debug.WriteLine("Unexpected audio state.");
            break;
    }
}
```

The following code example illustrates an implementation of the **SoundLevelChanged** handler for audio rendering. Depending on your app scenario, and the type of content you are playing, you may want to pause audio playback when the sound level is ducked.

```csharp
bool contentIsPodcast;
private void RenderAudioStateMonitor_SoundLevelChanged(AudioStateMonitor sender, object args)
{
    if ((sender.SoundLevel == SoundLevel.Full) ||
        (sender.SoundLevel == SoundLevel.Low && !contentIsPodcast))
    {
        m_mediaPlayer.Play();
    }
    else if ((sender.SoundLevel == SoundLevel.Muted) ||
         (sender.SoundLevel == SoundLevel.Low && contentIsPodcast))
    {
        // Pause playback if we're muted or if we're playing a podcast and are ducked
        m_mediaPlayer.Pause();
    }
}
```

## Related topics

[Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)

