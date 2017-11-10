---
title: Using Game Chat 2
author: KevinAsgari
description: Learn how to use Xbox Live Game Chat 2 to add voice communication to your game.
ms.author: tomco
ms.date: 10/20/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, game chat 2, game chat, voice communication
localizationpriority: medium
---

# Using Game Chat 2

This is a brief walkthrough on using Game Chat 2 (GC2), containing the following topics:

1. [Prerequisites](#prereq)
2. [Initialization](#init)
3. [Configuring users](#config)
4. [Processing data frames](#data)
5. [Processing state changes](#state)
6. [Text chat](#text)
7. [Accessibility](#access)
8. [UI](#UI)
9. [Muting](#mute)
10. [Bad reputation auto-mute](#automute)
11. [Privilege and privacy](#priv)
12. [Cleanup](#cleanup)
13. [How to configure popular scenarios](#how-to-configure-popular-scenarios)

## Prerequisites <a name="prereq">

Compiling GC2 requires including the primary GameChat2.h header. In order to link properly, your project must also include GameChat2Impl.h in at least one compilation unit (a common precompiled header is recommended since these stub function implementations are small and easy for the compiler to generate as "inline").

The GC2 interface does not require a project to choose between compiling with C++/CX versus traditional C++; it can be used with either. The implementation also doesn't throw exceptions as a means of non-fatal error reporting so you can consume it easily from exception-free projects if preferred.

## Initialization <a name="init">

You begin interacting with the library by initializing the GC2 object singleton with parameters that apply the lifetime of the singleton's initialization.

```cpp
chat_manager::singleton_instance().initialize(...);
```

## Configuring users <a name="config">

Once the instance is initialized, you must add the local users to the Game Chat instance. In this example, User A will represent a local user.

```cpp
chat_user* chatUserA = chat_manager::add_local_user(<user_a_xuid>);
```

You must also add the remote users and identifiers that will be used to represent the remote "Endpoint" that the user is on. An "Endpoint" is an instance of the app running on a remote device. In this example, User B is on Endpoint X, Users C and D are on Endpoint Y. Endpoint X is arbitrarily assigned identifier "1" and Endpoint Y is arbitrarily assigned identifier "2". You inform GC2 of the remote users with these calls:

```cpp
chat_user* chatUserB = chat_manager::add_remote_user(<user_b_xuid>, 1);
chat_user* chatUserC = chat_manager::add_remote_user(<user_c_xuid>, 2);
chat_user* chatUserD = chat_manager::add_remote_user(<user_d_xuid>, 2);
```

Now you configure the communication relationship between each remote user and the local user. In this example, suppose that User A and User B are on the same team and bidirectional communication is allowed. `c_communicationRelationshipSendAndReceiveAll` is a constant defined in GameChat2.h to represent bi-directional communication. Set User A's relationship to User B with:

```cpp
chatUserA->local()->set_communication_relationship(chatUserB, c_communicationRelationshipSendAndReceiveAll);
```

Now supposed that User C and D are 'spectators' and should be allowed to listen to User A, but not speak. `c_communicationRelationshipSendAll` is a constant defined in GameChat2.h to represent this unidirectional communication. Set the relationships with:


```cpp
chatUserA->local()->set_communication_relationship(chatUserC, c_communicationRelationshipSendAll);
chatUserA->local()->set_communication_relationship(chatUserD, c_communicationRelationshipSendAll);
```

If there are any remote users that can't communicate with any local users - that's okay! This may be expected in scenarios where users are determining teams or can change speaking channels arbitrarily. GC2 will only cache information (e.g. privacy relationships and reputation) for users that have been added, so it's useful to inform GC2 of all possible users even if they can't speak to any local users at a particular instance.

## Processing data frames <a name="data">

GC2 does not have its own transport layer; this must be provided by the app. This plugin is managed by the app's regular, frequent calls to the `chat_manager::start_processing_data_frames()` and `chat_manager::finish_processing_data_frames()` pair of methods. These methods are how GC2 provides outgoing data to the app. They're designed to operate quickly such that they can be polled frequently on a dedicated networking thread. This provides a convenient place to retrieve all queued data without worrying about the unpredictability of network timing or multi-threaded callback complexity.

When `chat_manager::start_processing_data_frames()` is called, all queued data is reported in an array of `game_chat_data_frame` structure pointers. Apps should iterate over the array, inspect the target "endpoints", and use the app's networking layer to deliver the data to the appropriate remote app instances. Once finished with all the `game_chat_data_frame` structures, the array should be passed back to GC2 to release the resources by calling `chat_manager:finish_processing_data_frames()`. For example:

```cpp
uint32_t dataFrameCount;
game_chat_data_frame_array dataFrames;
chat_manager::singleton_instance().start_processing_data_frames(&dataFrameCount, &dataFrames);
for (uint32_t dataFrameIndex = 0; dataFrameIndex < dataFrameCount; ++dataFrameIndex)
{
    game_chat_data_frame const* dataFrame = dataFrames[dataFrameIndex];
    HandleOutgoingDataFrame(
        dataFrame->packet_byte_count,
        dataFrame->packet_buffer,
        dataFrame->target_endpoint_identifier_count,
        dataFrame->target_endpoint_identifiers,
        dataFrame->transport_requirement
        );
}
chat_manager::singleton_instance().finish_processing_data_frames(dataFrames);
```

The more frequently the data frames are processed, the lower the audio latency apparent to the end user will be. The audio is coalesced into 40 ms data frames; this is the suggested polling period.

## Processing state changes <a name="state">

GC2 provides updates to the app, such as received text messages, through the app's regular, frequent calls to the `chat_manager::start_processing_data_frames()` and `chat_manager::finish_processing_data_frames()` pair of methods. They're designed to operate quickly such that they can be called every graphics frame in your UI rendering loop. This provides a convenient place to retrieve all queued changes without worrying about the unpredictability of network timing or multi-threaded callback complexity.

When `chat_manager::start_processing_data_frames()` is called, all queued updates are reported in an array of `game_chat_state_change` structure pointers. Apps should iterate over the array, inspect the base structure for its more specific type, cast the base structure to the corresponding more detailed type, and then handle that update as appropriate. Once finished with all `game_chat_state_change` objects currently available, that array should be passed back to GC2 to release the resources by calling `chat_manager::finish_processing_state_changes()`. For example:

```cpp
uint32_t stateChangeCount;
game_chat_state_change_array gameChatStateChanges;
chat_manager::singleton_instance().start_processing_state_changes(&stateChangeCount, &gameChatStateChanges);

for (uint32_t stateChangeIndex = 0; stateChangeIndex < stateChangeCount; ++stateChangeIndex)
{
    switch (gameChatStateChanges[stateChangeIndex]->state_change_type)
    {
        case game_chat_state_change_type::text_chat_received:
        {
            HandleTextChatReceived(static_cast<const game_chat_text_chat_received_state_change*>(gameChatStateChanges[stateChangeIndex]));
            break;
        }

        case Xs::game_chat_2::game_chat_state_change_type::transcribed_chat_received:
        {
            HandleTranscribedChatReceived(static_cast<const Xs::game_chat_2::game_chat_transcribed_chat_received_state_change*>(gameChatStateChanges[stateChangeIndex]));
            break;
        }

        ...
    }
}
chat_manager::singleton_instance().finish_processing_state_changes(gameChatStateChanges);
```

## Text chat <a name="text">

To send text chat, use `chat_user::chat_user_local::send_chat_text()`. For example:

```cpp
chatUserA->local()->send_chat_text(L"Hello");
```

GC2 will generate a data frame containing this message; the target endpoints for the data frame will be those associated with users that have been configured to receive text from the local user. When the data is processed by the remote endpoints, the message will be exposed via a `game_chat_text_chat_received_state_change`. As with voice chat, privilege and privacy restrictions are respected for text chat. If a pair of users have been configured to allow text chat, but privilege or privacy restrictions disallow that communication, the text message will be dropped.

Supporting text chat input and display is required for accessibility (see [Accessibility](#access) for more details).

## Accessibility <a name="access">

Supporting text chat input and display is required. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, users may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because users may configure the system to use speech-to-text. These preferences can be detected on local users by calling the `chat_user::chat_user_local::text_to_speech_conversion_preference_enabled()` and `chat_user::chat_user_local::speech_to_text_conversion_preference_enabled()` methods respectively, and you may wish to conditionally enable text mechanisms.

### Text-to-speech

When a user has text-to-speech enabled, `chat_user::chat_user_local::text_to_speech_conversion_preference_enabled()` will return 'true'. When this state is detected, the app must provide a method of text input. Once you have the text input provided by a real or virtual keyboard, pass the string to the `chat_user::chat_user_local::synthesize_text_to_speech()` method. GC2 will detect and synthesize audio data based on the string and the user's accessible voice preference. For example:

```cpp
chat_userA->local()->synthesize_text_to_speech(L"Hello");
```

The audio synthesized as part of this operation will be transported to all users that have been configured to receive audio from this local user. If `chat_user::chat_user_local::synthesize_text_to_speech()` is called on a user who does not have text-to-speech enabled GC2 will take no action.

### Speech-to-text

When a user has speech-to-text enabled, `chat_user::chat_user_local::speech_to_text_conversion_preference_enabled()` will return true. When this state is detected, the app must be prepared to provide UI associated with transcribed chat messages. GC will automatically transcribe each remote user's audio and expose it via a `game_chat_transcribed_chat_received_state_change`.

> `Windows::Xbox::UI::Accessibility` is an Xbox One class specifically designed to provide simple rendering of in-game text chat with a focus on speech-to-text assistive technologies.

### Speech-to-text performance considerations

When speech-to-text is enabled, the GC2 instance on each remote device initiates a web socket connection with the speech services endpoint. Each remote GC2 client uploads audio to the speech services endpoint through this websocket; the speech services endpoint occasionally returns a transcription message to the remote device. The remote device then sends the transcription message (i.e. a text message) to the local device, where the transcribed message is given by GC2 to the app to render.

Therefore, the primary performance cost of speech-to-text is network usage. Most of the network traffic is the upload of encoded audio. The websocket uploads audio that has already been encoded by GC2 in the “normal” voice chat path; the app has control over the bitrate via `chat_manager::set_audio_encoding_type_and_bitrate`.

## UI <a name="UI">

It's recommended that anywhere players are shown, particularly in a list of gamertags such as a scoreboard, that you also display muted/speaking icons as feedback for the user. This is done by calling `chat_user::chat_indicator()` to retrieve a `game_chat_user_chat_indicator` representing the current, instantaneous status of chat for that player. The following example demonstrates retrieving the indicator value for a `chat_user` object pointed to by the variable 'chatUserA' to determine a particular icon constant value to assign to an 'iconToShow' variable:

```cpp
switch (chatUserA->chat_indicator())
{
   case game_chat_user_chat_indicator::silent:
   {
       iconToShow = Icon_InactiveSpeaker;
       break;
   }

   case game_chat_user_chat_indicator::talking:
   {
       iconToShow = Icon_ActiveSpeaker;
       break;
   }

   case game_chat_user_chat_indicator::local_microphone_muted:
   {
       iconToShow = Icon_MutedSpeaker;
       break;
   }
   ...
}
```

The value reported by `xim_player::chat_indicator()` is expected to change frequently as players start and stop talking, for example. It is designed to support apps polling it every UI frame as a result.

## Muting <a name="mute">

The `chat_user::chat_user_local::set_microphone_muted()` method can be used to toggle the mute state of a local user's microphone. When the microphone is muted, no audio from that microphone will be captured. If the user is on a shared device, such as Kinect, the mute state applies to all users.

The `chat_user::chat_user_local::set_remote_user_muted()` method can be used to toggle the mute state of a remote user in relation to a particular local user. When the remote user is muted, the local user won't hear any audio or receive any text messages from the remote user.

## Bad reputation auto-mute <a name="automute">

Typically remote users will start off unmuted. GC2 will start the users in a muted state when (1) the remote user isn't friends with the local user, and (2) the remote user has a bad reputation flag. When users are muted due to this operation, `chat_user::chat_indicator()` will return `game_chat_user_chat_indicator::reputation_restricted`. This state will be overridden by the first call to `chat_user::chat_user_local::set_remote_user_muted()` that includes the remote user as the target user.

## Privilege and privacy <a name="priv">

On top of the communication relationship configured by the game, GC2 enforces privilege and privacy restrictions. GC2 performs privilege and privacy restriction lookups when a user is first added; the user's `chat_user::chat_indicator()` will always return `game_chat_user_chat_indicator::silent` until those operations complete. If communication with a user is affected by a privilege or privacy restriciton, the user's `chat_user::chat_indicator()` will return `game_chat_user_chat_indicator::platform_restricted`.

`chat_user::chat_user_local::get_effective_communication_relationship()` can be used to help distinguish when users can't commnicate due to incomplete privilege and privacy operations. It returns the communication relationship enforced by GC2 in the form of `game_chat_communication_relationship_flags` and the reason the relationship may not be equal to the configured relationship in the form of `game_chat_communication_relationship_adjuster`. For example, if the lookup operations are still in progress, the `game_chat_communication_relationship_adjuster` will be `game_chat_communication_relationship_adjuster::intializing`. This method is expected to be used in development and debugging scenarios; it should not be used to influence UI (see [UI](#UI)).

## Cleanup <a name="cleanup">

When the app no longer needs communications via GC2, you should call `chat_manager::cleanup()`. This allows GC2 to reclaim resources that were allocated to manage the communications.

## How to configure popular scenarios

### Push-to-talk

Push-to-talk should be implemented with `chat_user::chat_user_local::set_microphone_muted()`. Call `set_microphone_muted(false)` to allow speech and `set_microphone_muted(true)` to restrict it. This method will provide the lowest latency response from GC2.

### Teams

Suppose that User A and User B are on Team Blue, and User C and User D are on Team Red. Each user is in a unique instance of the app.

On User A's device:

```cpp
chatUserA->local()->set_communication_relationship(chatUserB, c_communicationRelationshipSendAndReceiveAll);
chatUserA->local()->set_communication_relationship(chatUserC, game_chat_communication_relationship_flags::none);
chatUserA->local()->set_communication_relationship(chatUserD, game_chat_communication_relationship_flags::none);
```

On User B's device:

```cpp
chatUserB->local()->set_communication_relationship(chatUserA, c_communicationRelationshipSendAndReceiveAll);
chatUserB->local()->set_communication_relationship(chatUserC, game_chat_communication_relationship_flags::none);
chatUserB->local()->set_communication_relationship(chatUserD, game_chat_communication_relationship_flags::none);
```

On User C's device:

```cpp
chatUserC->local()->set_communication_relationship(chatUserA, game_chat_communication_relationship_flags::none);
chatUserC->local()->set_communication_relationship(chatUserB, game_chat_communication_relationship_flags::none);
chatUserC->local()->set_communication_relationship(chatUserD, c_communicationRelationshipSendAndReceiveAll);
```

On User D's device:

```cpp
chatUserD->local()->set_communication_relationship(chatUserA, game_chat_communication_relationship_flags::none);
chatUserD->local()->set_communication_relationship(chatUserB, game_chat_communication_relationship_flags::none);
chatUserD->local()->set_communication_relationship(chatUserC, c_communicationRelationshipSendAndReceiveAll);
```

<!--TODO: Include table?-->

### Broadcast

Suppose that User A is the leader giving orders and Users B, C, and D can only listen. Each player is on a unique device.

On User A's device:

```cpp
chatUserA->local()->set_communication_relationship(chatUserB, c_communicationRelationshipSendAll);
chatUserA->local()->set_communication_relationship(chatUserC, c_communicationRelationshipSendAll);
chatUserA->local()->set_communication_relationship(chatUserD, c_communicationRelationshipSendAll);
```

On User B's device:

```cpp
chatUserB->local()->set_communication_relationship(chatUserA, c_communicationRelationshipReceiveAll);
chatUserB->local()->set_communication_relationship(chatUserC, game_chat_communication_relationship_flags::none);
chatUserB->local()->set_communication_relationship(chatUserD, game_chat_communication_relationship_flags::none);
```

On User C's device:

```cpp
chatUserC->local()->set_communication_relationship(chatUserA, c_communicationRelationshipReceiveAll);
chatUserC->local()->set_communication_relationship(chatUserB, game_chat_communication_relationship_flags::none);
chatUserC->local()->set_communication_relationship(chatUserD, game_chat_communication_relationship_flags::none);
```

On User D's device:

```cpp
chatUserD->local()->set_communication_relationship(chatUserA, c_communicationRelationshipReceiveAll);
chatUserD->local()->set_communication_relationship(chatUserB, game_chat_communication_relationship_flags::none);
chatUserD->local()->set_communication_relationship(chatUserC, game_chat_communication_relationship_flags::none);
```

<!--TODO: Include table?-->
