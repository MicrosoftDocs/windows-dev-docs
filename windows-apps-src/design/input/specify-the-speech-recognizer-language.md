---
author: Karl-Bridge-Microsoft
Description: Learn how to select an installed language to use for speech recognition.
title: Specify the speech recognizer language
ms.assetid: 4C463A1B-AF6A-46FD-A839-5D6724955B38
label: Specify the speech recognizer language
template: detail.hbs
keywords: speech, voice, speech recognition, natural language, dictation, input, user interaction
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Specify the speech recognizer language


Learn how to select an installed language to use for speech recognition.

> **Important APIs**: [**SupportedTopicLanguages**](https://msdn.microsoft.com/library/windows/apps/dn653251), [**SupportedGrammarLanguages**](https://msdn.microsoft.com/library/windows/apps/dn653250), [**Language**](https://msdn.microsoft.com/library/windows/apps/br206804)


Here, we enumerate the languages installed on a system, identify which is the default language, and select a different language for recognition.

**Prerequisites:**

This topic builds on [Speech recognition](speech-recognition.md).

You should have a basic understanding of speech recognition and recognition constraints.

If you're new to developing Universal Windows Platform (UWP) apps, have a look through these topics to get familiar with the technologies discussed here.

-   [Create your first app](https://msdn.microsoft.com/library/windows/apps/bg124288)
-   Learn about events with [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/mt185584)

**User experience guidelines:**

For helpful tips about designing a useful and engaging speech-enabled app, see [Speech design guidelines](https://msdn.microsoft.com/library/windows/apps/dn596121) .

## Identify the default language


A speech recognizer uses the system speech language as its default recognition language. This language is set by the user on the device Settings &gt; System &gt; Speech &gt; Speech Language screen.

We identify the default language by checking the [**SystemSpeechLanguage**](https://msdn.microsoft.com/library/windows/apps/dn653252) static property.

```CSharp
var language = SpeechRecognizer.SystemSpeechLanguage; 
```

## Confirm an installed language


Installed languages can vary between devices. You should verify the existence of a language if you depend on it for a particular constraint.

**Note**  A reboot is required after a new language pack is installed. An exception with error code SPERR\_NOT\_FOUND (0x8004503a) is raised if the specified language is not supported or has not finished installing.

 

Determine the supported languages on a device by checking one of two static properties of the [**SpeechRecognizer**](https://msdn.microsoft.com/library/windows/apps/dn653226) class:

-   [**SupportedTopicLanguages**](https://msdn.microsoft.com/library/windows/apps/dn653251)—The collection of [**Language**](https://msdn.microsoft.com/library/windows/apps/br206804) objects used with predefined dictation and web search grammars.

-   [**SupportedGrammarLanguages**](https://msdn.microsoft.com/library/windows/apps/dn653250)—The collection of [**Language**](https://msdn.microsoft.com/library/windows/apps/br206804) objects used with a list constraint or a Speech Recognition Grammar Specification (SRGS) file.

## Specify a language


To specify a language, pass a [**Language**](https://msdn.microsoft.com/library/windows/apps/br206804) object in the [**SpeechRecognizer**](https://msdn.microsoft.com/library/windows/apps/dn653226) constructor.

Here, we specify "en-US" as the recognition language.


```CSharp
var language = new Windows.Globalization.Language(“en-US”); 
var recognizer = new SpeechRecognizer(language); 
```

## Remarks


A topic constraint can be configured by adding a [**SpeechRecognitionTopicConstraint**](https://msdn.microsoft.com/library/windows/apps/dn631446) to the [**Constraints**](https://msdn.microsoft.com/library/windows/apps/dn653241) collection of the [**SpeechRecognizer**](https://msdn.microsoft.com/library/windows/apps/dn653226) and then calling [**CompileConstraintsAsync**](https://msdn.microsoft.com/library/windows/apps/dn653240). A [**SpeechRecognitionResultStatus**](https://msdn.microsoft.com/library/windows/apps/dn631433) of **TopicLanguageNotSupported** is returned if the recognizer is not initialized with a supported topic language.

A list constraint is configured by adding a [**SpeechRecognitionListConstraint**](https://msdn.microsoft.com/library/windows/apps/dn631421) to the [**Constraints**](https://msdn.microsoft.com/library/windows/apps/dn653241) collection of the [**SpeechRecognizer**](https://msdn.microsoft.com/library/windows/apps/dn653226) and then calling [**CompileConstraintsAsync**](https://msdn.microsoft.com/library/windows/apps/dn653240). You cannot specify the language of a custom list directly. Instead, the list will be processed using the language of the recognizer.

An SRGS grammar is an open-standard XML format represented by the [**SpeechRecognitionGrammarFileConstraint**](https://msdn.microsoft.com/library/windows/apps/dn631412) class. Unlike custom lists, you can specify the language of the grammar in the SRGS markup. [**CompileConstraintsAsync**](https://msdn.microsoft.com/library/windows/apps/dn653240) fails with a [**SpeechRecognitionResultStatus**](https://msdn.microsoft.com/library/windows/apps/dn631433) of **TopicLanguageNotSupported** if the recognizer is not initialized to the same language as the SRGS markup.

## Related articles

**Developers**

* [Speech interactions](speech-interactions.md)

**Designers**

* [Speech design guidelines](https://msdn.microsoft.com/library/windows/apps/dn596121)

**Samples**

* [Speech recognition and speech synthesis sample](http://go.microsoft.com/fwlink/p/?LinkID=619897)
 

 




