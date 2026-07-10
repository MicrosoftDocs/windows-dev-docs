---
title: Speech recognition in Windows apps
description: Use speech recognition to provide input, specify actions or commands, and accomplish tasks in your Windows App SDK app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Speech recognition in Windows apps

Use speech recognition to provide input, specify actions or commands, and accomplish tasks in your Windows App SDK app.

> [!IMPORTANT]
> Speech recognition requires MSIX package identity. The `Windows.Media.SpeechRecognition` APIs are available only when your app runs with package identity (packaged or packaged with external location). Unpackaged apps cannot use these APIs.

## Key APIs

- [Windows.Media.SpeechRecognition](/uwp/api/Windows.Media.SpeechRecognition)
- [SpeechRecognizer](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer)
- [SpeechRecognitionResult](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult)

## Overview

Speech recognition consists of a speech runtime, recognition APIs for programming the runtime, ready-to-use grammars for dictation and web search, and a default system UI that helps users discover and use speech recognition features.

## Configure speech recognition

To support speech recognition, the user must connect and enable a microphone on their device and accept the Microsoft Privacy Policy granting permission for your app to use it.

Set the **Microphone** device capability in your app's package manifest to prompt the user for microphone access permission. For more information, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations).

If the user grants access, your app appears in the approved applications list at **Settings > Privacy > Microphone**. Because the user can disable this setting at any time, confirm that your app has microphone access before you attempt to use it.

If you also want to support dictation or other speech recognition services (such as a [predefined grammar](#predefined-grammars) defined in a topic constraint), confirm that **Online speech recognition** (**Settings > Privacy > Speech**) is enabled.

The following example shows how to check whether a microphone is present and whether your app has permission to use it.

```csharp
public class AudioCapturePermissions
{
    private static int NoCaptureDevicesHResult = -1072845856;

    /// <summary>
    /// Checks whether the microphone is available and the app has permission to use it.
    /// Perform this check every time the app gets focus, because the user can change
    /// the setting while the app is suspended or not in focus.
    /// </summary>
    /// <returns>True if the microphone is available.</returns>
    public static async Task<bool> RequestMicrophonePermission()
    {
        try
        {
            var settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio,
                MediaCategory = MediaCategory.Speech
            };
            var capture = new MediaCapture();
            await capture.InitializeAsync(settings);
        }
        catch (TypeLoadException)
        {
            // Media player components are not available.
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            // Permission to use the audio capture device is denied.
            return false;
        }
        catch (Exception exception)
        {
            if (exception.HResult == NoCaptureDevicesHResult)
            {
                // No audio capture devices are present on this system.
                return false;
            }
            throw;
        }
        return true;
    }
}
```

## Recognize speech input

A *constraint* defines the words and phrases (vocabulary) that an app recognizes in speech input. Constraints are at the core of speech recognition and give your app greater control over recognition accuracy.

You can use the following types of constraints.

### Predefined grammars

Predefined dictation and web-search grammars provide speech recognition without requiring you to author a grammar. When you use these grammars, a remote web service performs the recognition and returns the results to the device.

The default free-text dictation grammar recognizes most words and phrases that a user can say in a particular language, and is optimized for short phrases. If you don't specify any constraints for your [SpeechRecognizer](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer), the predefined dictation grammar is used.

The web-search grammar contains a large number of words and phrases, optimized for terms that people typically use when searching the web.

> [!NOTE]
> Predefined dictation and web-search grammars can be large, and because they are online (not on the device), performance might not be as fast as with a custom grammar installed locally.

These predefined grammars recognize up to 10 seconds of speech input and require no authoring effort. However, they require a network connection.

See [SpeechRecognitionTopicConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint).

### Programmatic list constraints

Programmatic list constraints provide a lightweight approach to creating simple grammars by using a list of words or phrases. A list constraint works well for recognizing short, distinct phrases. Specifying all words in a grammar improves recognition accuracy because the speech recognition engine only needs to process speech to confirm a match. The list can also be updated programmatically.

See [SpeechRecognitionListConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint).

### SRGS grammars

A Speech Recognition Grammar Specification (SRGS) grammar is a static document that uses the XML format defined by the [SRGS Version 1.0](https://www.w3.org/TR/speech-grammar/). An SRGS grammar gives you the greatest control over the speech recognition experience by letting you capture multiple semantic meanings in a single recognition.

See [SpeechRecognitionGrammarFileConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionGrammarFileConstraint).

### Voice command constraints

Use a Voice Command Definition (VCD) XML file to define the commands that the user can say to initiate actions when activating your app. See [SpeechRecognitionVoiceCommandDefinitionConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionVoiceCommandDefinitionConstraint).

To get started with constraints, see [Define custom recognition constraints](define-custom-recognition-constraints.md).

## Basic recognition example

The following example creates a speech recognizer, compiles the default dictation constraints, and starts listening for speech input.

```csharp
private async void StartRecognizing_Click(object sender, RoutedEventArgs e)
{
    var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

    // Compile the default dictation grammar.
    await speechRecognizer.CompileConstraintsAsync();

    // Start recognition.
    Windows.Media.SpeechRecognition.SpeechRecognitionResult result =
        await speechRecognizer.RecognizeAsync();

    // Display the recognized text.
    if (result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
    {
        resultTextBlock.Text = result.Text;
    }
}
```

> [!NOTE]
> This example uses `RecognizeAsync` (without UI). If you want to use the built-in recognition UI, call `RecognizeWithUIAsync` instead. See `SpeechRecognizerUIOptions` for customization options.

## Customize the recognition UI

When your app calls [SpeechRecognizer.RecognizeWithUIAsync](/uwp/api/windows.media.speechrecognition.speechrecognizer.recognizewithuiasync), the system displays several screens in sequence:

- For predefined grammars (dictation or web search): **Listening** → **Thinking** → **Heard you say** (or error).
- For list or SRGS constraints: **Listening** → **Did you say** (if ambiguous) → **Heard you say** (or error).

Use the [SpeechRecognizerUIOptions](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizerUIOptions) class (obtained through `SpeechRecognizer.UIOptions`) to customize the **Listening** screen:

```csharp
private async void WeatherSearch_Click(object sender, RoutedEventArgs e)
{
    var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

    speechRecognizer.RecognitionQualityDegrading += SpeechRecognizer_RecognitionQualityDegrading;

    var webSearchGrammar = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(
        Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "webSearch");

    speechRecognizer.UIOptions.AudiblePrompt = "Say what you want to search for...";
    speechRecognizer.UIOptions.ExampleText = @"Ex. 'weather for London'";
    speechRecognizer.Constraints.Add(webSearchGrammar);

    await speechRecognizer.CompileConstraintsAsync();

    Windows.Media.SpeechRecognition.SpeechRecognitionResult result =
        await speechRecognizer.RecognizeWithUIAsync();

    resultTextBlock.Text = result.Text;
}
```

## Related articles

- [Speech interactions](speech-interactions.md)
- [Enable continuous dictation](enable-continuous-dictation.md)
- [Define custom recognition constraints](define-custom-recognition-constraints.md)
- [Specify the speech recognizer language](specify-the-speech-recognizer-language.md)
- [Set speech recognition timeouts](set-speech-recognition-timeouts.md)
- [Manage issues with audio input](manage-issues-with-audio-input.md)
