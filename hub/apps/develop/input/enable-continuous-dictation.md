---
title: Enable continuous dictation in Windows apps
description: Learn how to capture and recognize long-form continuous dictation speech input in your Windows App SDK desktop app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Enable continuous dictation

Learn how to capture and recognize long-form, continuous dictation speech input in your Windows App SDK app.

> [!IMPORTANT]
> Speech recognition requires MSIX package identity. The `Windows.Media.SpeechRecognition` APIs are available only when your app runs with package identity (packaged or packaged with external location). Unpackaged apps cannot use these APIs.

## Key APIs

- [SpeechContinuousRecognitionSession](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession)
- [ContinuousRecognitionSession](/uwp/api/windows.media.speechrecognition.speechrecognizer.continuousrecognitionsession)
- [SpeechRecognizer](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer)

## Overview

In [Speech recognition](speech-recognition.md), you learn how to capture and recognize short speech input by using `RecognizeAsync` or `RecognizeWithUIAsync`. For longer, continuous speech recognition sessions — such as dictation or email — use the `ContinuousRecognitionSession` property of a `SpeechRecognizer` to obtain a `SpeechContinuousRecognitionSession` object.

> [!NOTE]
> Dictation language support depends on the device where your app is running. For PCs and laptops, only en-US is recognized for dictation, while Xbox can recognize all languages supported by speech recognition. For more information, see [Specify the speech recognizer language](specify-the-speech-recognizer-language.md).

## Set up

Your app needs the following objects to manage a continuous dictation session:

- An instance of a `SpeechRecognizer` object.
- A reference to the UI dispatcher to update the UI during dictation.
- A way to track the accumulated words spoken by the user.

Declare a `SpeechRecognizer` instance and a `StringBuilder` to accumulate recognition results as fields of your page class:

```csharp
private SpeechRecognizer speechRecognizer;
private StringBuilder dictatedTextBuilder;
```

In WinUI 3, you use `DispatcherQueue` to dispatch UI updates from background threads (not `CoreDispatcher`):

```csharp
// Get the DispatcherQueue for the current thread (UI thread).
private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue =
    Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
```

## Initialize

During initialization, you:

1. Initialize the speech recognizer.
2. Compile the built-in dictation grammar (or add custom constraints).
3. Set up event listeners for recognition events.

```csharp
// Initialize the speech recognizer.
speechRecognizer = new SpeechRecognizer();

// Compile the default dictation grammar.
SpeechRecognitionCompilationResult result =
    await speechRecognizer.CompileConstraintsAsync();

// Subscribe to continuous recognition events.
speechRecognizer.ContinuousRecognitionSession.ResultGenerated +=
    ContinuousRecognitionSession_ResultGenerated;
speechRecognizer.ContinuousRecognitionSession.Completed +=
    ContinuousRecognitionSession_Completed;
speechRecognizer.HypothesisGenerated +=
    SpeechRecognizer_HypothesisGenerated;

dictatedTextBuilder = new StringBuilder();
```

## Handle recognition events

### ResultGenerated

The `ResultGenerated` event fires as the user speaks. The recognizer periodically passes a chunk of speech input. Check the `Confidence` property to decide whether to accept the result.

Because this event fires on a background thread, use `DispatcherQueue.TryEnqueue` to update the UI:

```csharp
private void ContinuousRecognitionSession_ResultGenerated(
    SpeechContinuousRecognitionSession sender,
    SpeechContinuousRecognitionResultGeneratedEventArgs args)
{
    if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
        args.Result.Confidence == SpeechRecognitionConfidence.High)
    {
        dictatedTextBuilder.Append(args.Result.Text + " ");

        dispatcherQueue.TryEnqueue(() =>
        {
            dictationTextBox.Text = dictatedTextBuilder.ToString();
            btnClearText.IsEnabled = true;
        });
    }
}
```

### Completed

The `Completed` event indicates that the continuous recognition session has ended. The session ends when you call `StopAsync` or `CancelAsync`, or when an error occurs, or when the user has stopped speaking.

```csharp
private void ContinuousRecognitionSession_Completed(
    SpeechContinuousRecognitionSession sender,
    SpeechContinuousRecognitionCompletedEventArgs args)
{
    if (args.Status != SpeechRecognitionResultStatus.Success)
    {
        dispatcherQueue.TryEnqueue(() =>
        {
            if (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded)
            {
                dictationTextBox.Text = dictatedTextBuilder.ToString();
            }
        });
    }
}
```

### HypothesisGenerated

Handle the `HypothesisGenerated` event to show interim results while the recognizer is still processing. This improves responsiveness by giving the user feedback before a final result is available:

```csharp
private void SpeechRecognizer_HypothesisGenerated(
    SpeechRecognizer sender,
    SpeechRecognitionHypothesisGeneratedEventArgs args)
{
    string hypothesis = args.Hypothesis.Text;
    string textboxContent = dictatedTextBuilder.ToString() + " " + hypothesis + " ...";

    dispatcherQueue.TryEnqueue(() =>
    {
        dictationTextBox.Text = textboxContent;
        btnClearText.IsEnabled = true;
    });
}
```

## Start and stop recognition

Check the recognizer state before starting or stopping a session:

```csharp
// Start continuous recognition.
if (speechRecognizer.State == SpeechRecognizerState.Idle)
{
    await speechRecognizer.ContinuousRecognitionSession.StartAsync();
}

// Stop continuous recognition (lets pending events complete).
if (speechRecognizer.State != SpeechRecognizerState.Idle)
{
    await speechRecognizer.ContinuousRecognitionSession.StopAsync();
}
```

To cancel immediately and discard pending results, call `CancelAsync` instead of `StopAsync`.

> [!NOTE]
> A `ResultGenerated` event might fire after you call `CancelAsync` due to multithreading. If you set private fields when canceling the recognition session, always validate their values in the `ResultGenerated` handler.

## Related articles

- [Speech recognition](speech-recognition.md)
- [Speech interactions](speech-interactions.md)
- [Define custom recognition constraints](define-custom-recognition-constraints.md)
