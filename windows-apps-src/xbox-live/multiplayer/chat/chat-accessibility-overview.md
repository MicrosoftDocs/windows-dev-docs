---
author: KevinAsgari
title: Accessible in-game chat overview
description: Learn about Xbox Live Game Chat accessibility features.
ms.assetid: ecd84fa1-9b28-414b-b5e1-daba68de9472
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, game chat, accessibility, text to speech, speech to text
ms.localizationpriority: low
---

#  Accessible in-game chat overview

Here's an overview of the accessible chat features provided by the **GameChat** component.

To help you improve the accessibility of in-game communication, we have provided two new features&mdash;Text-to-Speech (TTS) and Speech-to-Text (STT).

You should consider enabling both features to make your title more accessible to gamers with hearing or speech impairments.

## Text-to-speech (TTS)

TTS describes the action of converting text into speech; text messages are read out using a synthesized voice. Using TTS in **GameChat**, your title can automatically synthesize audio streams that read out text streams sent by a user. Both text and synthesized audio streams are sent to other active chat users. This functionality enables gamers with speech impairments to actively engage in game chat even when your title does not have a text-based chat system.

As the **GameChat** component does not provide any other APIs for capturing text input from the player, your title is still responsible for either providing a custom text-entry field or enabling the virtual keyboard provided by the Xbox platform.

**HasRequestedSynthesizedAudio** is a new property in the **ChatUser** class, part of the **Microsoft.Xbox.GameChat** namespace. This value is set by the user through their **Ease of Access** settings for Xbox.

When a user joins an active chat session, your title needs to listen for the **OnAccessibilitySettingsChanged** event in the **ChatUser** class. This event is raised when any accessibility settings for chat has been changed. When **OnAccessibilitySettingsChanged** event is raised, check for the value of **HasRequestedSynthesizedAudio**.

If **HasRequestedSynthesizedAudio** is set to **TRUE**, this means that TTS is enabled and a user has requested for text messages sent to all active users to include a voice packet that reads out the text message.

This table shows how the TTS setting of a user, indicated by property value of **HasRequestedSynthesizedAudio**, affect the way text messages in chat are processed.

|HasRequestedSynthesizedAudio  |Type of data sent to all remote users                                                 |
|------------------------------|--------------------------------------------------------------------------------------|
|**FALSE**                     |Only text                                                                             |
|**TRUE**                      |Both text and synthesized voice                                                       |

**allowTextToSpeechConversion** is an input parameter of the **GenerateTextMessage** API. This API is also part of the **ChatUser** class in the **Microsoft.Xbox.GameChat** namespace. This parameter is set by your title and determines whether or not it wishes to take advantage of the built-in functionality that converts text to speech.

If **allowTextToSpeechConversion** is **TRUE**, **GameChat** will automatically process the text message to generate a synthesized voice packet. The synthesized voice data is created using the user's selected voice profile in their settings (Zira, Marcus, or David).
If it is set to **FALSE**, this means that your title plans to handle the conversion on your own or do not want **GameChat** to auto-generate these synthesized voice packets on your behalf.

Since the property value of **allowTextToSpeechConversion** determines whether or not text is converted to speech automatically by **GameChat**, this table shows how it impacts the eventual types of data sent to all remote users.

|allowTextToSpeechConversion   |Type of data sent to all remote users                                                                                                               |
|------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
|**FALSE**                     |Only text is sent regardless if TTS is enabled or not                                                                                               |
|**TRUE**                      |Both text and synthesized voice are sent only if **HasRequestedSynthesizedAudio** is **TRUE**                                                       |

### Example flow

When user joins a chat session, your title checks the value of the **HasRequestedSynthesizedAudio** property in the **ChatUser** class. This value indicates whether or not the user wishes to send text as speech.

When `HasRequestedSynthesizedAudio = True`:

* Present a way to enable the user to enter text to allow for TTS conversion.

* Call the **GenerateTextMessage** function, passing in the text message from the user and whether a voice synthesis of the text message is required as input parameters. In most cases, the second parameter will always be true.

* Respond to **OnOutgoingChatPacketReady** events which will now contain both the raw text and synthesized voice data.

When receiving chat packages with synthesized voice data:

* Call **ProcessIncomingChatMessage** as data is being received.

* For each chat package processed, **GameChat** will raise the **OnTextMessageReceived** event which includes the **ChatTextMessageType** argument. This argument provides information about the type of message received.

* If the message type is identified as a **ChatSynthesizedVoiceDataMessage**, a synthesized voice message will also be processed in the same way as recorded voice packets.

## Speech-to-text (STT)

STT describes the action of taking voice and transcribing it into text. In **GameChat**, users can receive text transcriptions from other users who are speaking (using voice) in a chat to communicate. This functionality enables gamers with hearing impairment to engage in voice chat, and also enables all users to participate in voice chat even if they have turned game volume off.

**HasRequestedTranscription** is a new property in the **ChatUser** class, part of the **Microsoft.Xbox.GameChat** namespace. This value is set by the user through their **Ease of Access** settings for Xbox.

When a user joins an active chat session, your title needs to listen for the **OnAccessibilitySettingsChanged** event in the **ChatUser** class. This event is raised when any accessibility settings for chat has been changed. When **OnAccessibilitySettingsChanged** event is raised, check for the value of **HasRequestedTranscription**.

If **HasRequestedTranscription** is set to **TRUE**, this means that STT is enabled and all remote users will have their voice data sent to Microsoft Speech Services for transcription prior to sending both the voice and text responses to the local user.

Titles planning to render text chat using customized UI may want to check for this value in order to prepare their screen in advance, but it is not required.

This table shows how the STT setting of a user, indicated by property value of **HasRequestedTranscription**, affect voice messages sent from other active chat users.

|HasRequestedTranscription     |Type of data received by user                                                         |
|------------------------------|--------------------------------------------------------------------------------------|
|**FALSE**                     |Only voice                                                                            |
|**TRUE**                      |Both transcribed text and voice                                                       |


### To render a transcribed text message

When STT is enabled and the user receives a transcribed text message, which will be of type **TranscribedText**, your title must render the text to the screen by using one of two methods:

* Title-callable UI (TCUI)

  You can render the transcribed text on your title's screen by using the TCUI, which is provided by shell and the platform. The UI is an overlay that has a fixed height and width. It can be positioned in one of eight possible positions on the screen, as shown here.

  ![Eight possible render positions for TCUI](../../images/multiplayer/tcui-render-positions.png)

  Your title will need to send three pieces of info to the API for display: the name of the speaker, the transcribed text, and an identifier to indicate how the text message was created (that is, through actual text input or via STT). For text messages created through actual text input, the keyboard identifier is displayed; for transcribed text messages, the headset identifier is displayed. Because this is a shell-based overlay, it inherits all shell behaviors, and users cannot interact with it.

  For titles developed on the Universal Windows Platform (UWP), this overlay is expected to be available in the next major update. Until then, you can use in-game UI (described next) to render transcribed text messages.

* In-game UI

  If you prefer your title to have more control over the UI experience across different devices (Xbox, PC, or any Windows 10 devices), you can create custom in-game UI to display chat text instead.

### Example flow

On launch, you  may check the **HasRequestedTranscription** property from the **ChatUser** class. This setting tells your title whether the user has enabled the speech-to-text feature, and therefore how to handle voice and text data sent to the user during the chat session.

When a user with STT enabled receives chat packages with transcribed text strings:
* **ChatPackets** sent to the user include both voice data and the transcribed string text received after the voice data has been processed.
* For each chat package processed, the **OnTextMessageReceived** event is raised. This event includes the **ChatTextMessageType** argument, which provides info about the type of message received. If the message type is identified as a **TranscribedText**, the transcribed text message is rendered on screen using one of the two methods listed previously&mdash;TCUI or an equivalent in-game UI.

## Related links

* [GameChat overview](gamechat-overview.md)
