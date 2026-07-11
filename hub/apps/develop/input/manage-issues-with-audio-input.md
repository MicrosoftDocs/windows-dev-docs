---
title: Manage issues with audio input in Windows apps
description: Learn how to detect and handle audio input quality issues during speech recognition in a Windows App SDK desktop app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Manage issues with audio input

Learn how to handle audio input quality problems during speech recognition in your Windows App SDK app.

> [!IMPORTANT]
> Speech recognition requires MSIX package identity. The `Windows.Media.SpeechRecognition` APIs are available only when your app runs with package identity (packaged or packaged with external location). Unpackaged apps cannot use these APIs.

## Key APIs

- [SpeechRecognizer](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer)
- [RecognitionQualityDegrading](/uwp/api/windows.media.speechrecognition.speechrecognizer.recognitionqualitydegrading)
- [SpeechRecognitionAudioProblem](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionAudioProblem)

## Overview

Audio input quality can affect the accuracy of speech recognition. Background noise, muffled microphones, and low volume are common causes of recognition failure. Handle the `RecognitionQualityDegrading` event to let the user know when audio quality may prevent accurate recognition.

## Handle quality degradation

The `RecognitionQualityDegrading` event fires when audio issues may interfere with speech recognition. Use the `SpeechRecognitionQualityDegradingEventArgs.Problem` property to determine the specific issue.

Because this event fires on a background thread, use `DispatcherQueue.TryEnqueue` to update the UI:

```csharp
private void SpeechRecognizer_RecognitionQualityDegrading(
    SpeechRecognizer sender,
    SpeechRecognitionQualityDegradingEventArgs args)
{
    dispatcherQueue.TryEnqueue(() =>
    {
        // Show a message or indicator to the user.
        // args.Problem identifies the specific audio quality issue.
        switch (args.Problem)
        {
            case SpeechRecognitionAudioProblem.TooFast:
                statusTextBlock.Text = "Try speaking more slowly.";
                break;
            case SpeechRecognitionAudioProblem.TooSlow:
                statusTextBlock.Text = "Try speaking a bit faster.";
                break;
            case SpeechRecognitionAudioProblem.TooQuiet:
                statusTextBlock.Text = "Try speaking louder or move closer to the microphone.";
                break;
            case SpeechRecognitionAudioProblem.TooLoud:
                statusTextBlock.Text = "Try speaking more quietly or move farther from the microphone.";
                break;
            case SpeechRecognitionAudioProblem.TooNoisy:
                statusTextBlock.Text = "Background noise is interfering with recognition. Try a quieter environment.";
                break;
            case SpeechRecognitionAudioProblem.None:
            default:
                statusTextBlock.Text = "";
                break;
        }
    });
}
```

To use this handler, subscribe to the event after creating your recognizer:

```csharp
speechRecognizer.RecognitionQualityDegrading += SpeechRecognizer_RecognitionQualityDegrading;
```

## UI thread considerations

In WinUI 3, use `DispatcherQueue` instead of `CoreDispatcher` to marshal calls to the UI thread. Get a reference to the `DispatcherQueue` for the current thread in your page constructor or initialization logic:

```csharp
private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue =
    Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
```

Then use `dispatcherQueue.TryEnqueue(...)` in your event handlers to safely update UI elements.

## Related articles

- [Speech recognition](speech-recognition.md)
- [Set speech recognition timeouts](set-speech-recognition-timeouts.md)
- [Enable continuous dictation](enable-continuous-dictation.md)
