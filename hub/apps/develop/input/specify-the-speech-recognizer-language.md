---
title: Specify the speech recognizer language in Windows apps
description: Learn how to select and set the installed language for speech recognition in your Windows App SDK desktop application.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Specify the speech recognizer language

Learn how to select an installed language for speech recognition in your Windows App SDK app.

> [!IMPORTANT]
> Speech recognition requires MSIX package identity. The `Windows.Media.SpeechRecognition` APIs are available only when your app runs with package identity (packaged or packaged with external location). Unpackaged apps cannot use these APIs.

## Key APIs

- [SupportedTopicLanguages](/uwp/api/windows.media.speechrecognition.speechrecognizer.supportedtopiclanguages)
- [SupportedGrammarLanguages](/uwp/api/windows.media.speechrecognition.speechrecognizer.supportedgrammarlanguages)
- [Language](/uwp/api/Windows.Globalization.Language)

## Overview

By default, the speech recognizer uses the installed system language for speech recognition. Pass a `Windows.Globalization.Language` object to the `SpeechRecognizer` constructor to select a different language.

Your app should use a language supported on the user's device. Use the `SpeechRecognizer.SupportedTopicLanguages` or `SpeechRecognizer.SupportedGrammarLanguages` property to determine which languages are available.

## Set the recognition language

Specify the language by passing a `Language` object to the `SpeechRecognizer` constructor:

```csharp
var language = new Windows.Globalization.Language("en-US");
var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer(language);
```

## Determine supported languages

The following example shows how to check which topic languages are supported and display them:

```csharp
var topicLanguages = Windows.Media.SpeechRecognition.SpeechRecognizer.SupportedTopicLanguages;

foreach (var lang in topicLanguages)
{
    var languageTag = lang.LanguageTag;
    var displayName = lang.DisplayName;
    // Present these to the user for selection.
}
```

The following example checks which grammar languages are available:

```csharp
var grammarLanguages = Windows.Media.SpeechRecognition.SpeechRecognizer.SupportedGrammarLanguages;

foreach (var lang in grammarLanguages)
{
    var languageTag = lang.LanguageTag;
    var displayName = lang.DisplayName;
}
```

> [!NOTE]
> Topic languages (dictation and web search) are processed by an online service and are not limited by which speech language packs are installed locally. Grammar languages (list and SRGS constraints) are processed on-device and require the corresponding speech language pack to be installed.

## Related articles

- [Speech recognition](speech-recognition.md)
- [Define custom recognition constraints](define-custom-recognition-constraints.md)
