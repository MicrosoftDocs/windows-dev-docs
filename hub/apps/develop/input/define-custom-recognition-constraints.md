---
title: Define custom recognition constraints in Windows apps
description: Learn how to define custom speech recognition constraints including topic, list, and SRGS grammar types in a Windows App SDK app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Define custom recognition constraints

Learn how to define and use custom constraints for speech recognition in your Windows App SDK app.

> [!IMPORTANT]
> Speech recognition requires MSIX package identity. The `Windows.Media.SpeechRecognition` APIs are available only when your app runs with package identity (packaged or packaged with external location). Unpackaged apps cannot use these APIs.

## Key APIs

- [SpeechRecognitionTopicConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint)
- [SpeechRecognitionListConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint)
- [SpeechRecognitionGrammarFileConstraint](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionGrammarFileConstraint)

## Overview

Speech recognition requires at least one constraint to define a recognizable vocabulary. If you don't specify a constraint, the predefined dictation grammar is used. See [Speech recognition](speech-recognition.md).

Use the `SpeechRecognizer.Constraints` property to add constraints to a speech recognizer. Three types of constraints are available:

- **SpeechRecognitionTopicConstraint** — A constraint based on a predefined grammar (dictation or web search).
- **SpeechRecognitionListConstraint** — A constraint based on a list of words or phrases.
- **SpeechRecognitionGrammarFileConstraint** — A constraint defined in a Speech Recognition Grammar Specification (SRGS) file.

Each speech recognizer can have one constraint collection. The following combinations are valid:

- A single topic constraint (dictation or web search)
- A single topic constraint combined with a list constraint
- A combination of list constraints and/or grammar-file constraints

> [!IMPORTANT]
> Call `SpeechRecognizer.CompileConstraintsAsync` to compile the constraints before starting the recognition process.

## Specify a web-search grammar (SpeechRecognitionTopicConstraint)

```csharp
private async void WeatherSearch_Click(object sender, RoutedEventArgs e)
{
    var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

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

## Specify a programmatic list constraint (SpeechRecognitionListConstraint)

List constraints are a lightweight way to create simple grammars from a list of words or phrases. A list constraint works well for recognizing short, distinct phrases:

```csharp
private async void YesOrNo_Click(object sender, RoutedEventArgs e)
{
    var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

    string[] responses = { "Yes", "No" };

    var listConstraint = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(
        responses, "yesOrNo");

    speechRecognizer.UIOptions.ExampleText = @"Ex. 'yes', 'no'";
    speechRecognizer.Constraints.Add(listConstraint);

    await speechRecognizer.CompileConstraintsAsync();

    Windows.Media.SpeechRecognition.SpeechRecognitionResult result =
        await speechRecognizer.RecognizeWithUIAsync();

    resultTextBlock.Text = result.Text;
}
```

## Specify an SRGS grammar constraint (SpeechRecognitionGrammarFileConstraint)

SRGS grammars provide a full set of features for complex voice interaction, including specifying the order in which words must be spoken, combining words from multiple lists, linking to other grammars, and using semantic interpretation:

```csharp
private async void Colors_Click(object sender, RoutedEventArgs e)
{
    var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

    var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
        new Uri("ms-appx:///Colors.grxml"));
    var grammarFileConstraint =
        new Windows.Media.SpeechRecognition.SpeechRecognitionGrammarFileConstraint(
            storageFile, "colors");

    speechRecognizer.UIOptions.ExampleText = @"Ex. 'blue background', 'green text'";
    speechRecognizer.Constraints.Add(grammarFileConstraint);

    await speechRecognizer.CompileConstraintsAsync();

    Windows.Media.SpeechRecognition.SpeechRecognitionResult result =
        await speechRecognizer.RecognizeWithUIAsync();

    resultTextBlock.Text = result.Text;
}
```

Use the `.grxml` file extension for XML-based grammar documents that conform to SRGS rules. Set the file's **Build Action** to **Content** and **Copy to Output Directory** to **Copy always**.

For more information about SRGS elements and attributes, see the [SRGS Grammar XML Reference](/previous-versions/office/developer/speech-technologies/hh361653(v=office.14)).

## Manage constraints

After you load a constraint collection, you can enable or disable individual constraints by setting the `IsEnabled` property to `true` or `false`. The default is `true`.

Loading constraints once and then enabling or disabling them as needed is more efficient than loading, unloading, and compiling constraints for each recognition operation.

Restricting the number of active constraints improves both the performance and the accuracy of speech recognition. Decide which constraints to enable based on the phrases that your app can expect in the current context.

## Related articles

- [Speech recognition](speech-recognition.md)
- [Enable continuous dictation](enable-continuous-dictation.md)
- [Specify the speech recognizer language](specify-the-speech-recognizer-language.md)
