---
title: Using Game Chat 2
author: KevinAsgari
description: Learn how to use Xbox Live Game Chat 2 to add voice communication to your game.
ms.author: kevinasg
ms.date: 3/20/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, game chat 2, game chat, voice communication
ms.localizationpriority: low
---

# Using Game Chat 2 (C++)

This is a brief walkthrough on using Game Chat 2's C++ API. Game developers wanting to access Game Chat 2 through C# should see [Use Game Chat 2 WinRT Projections](using-game-chat-2-winrt.md).

1. [Prerequisites](#prerequisites)
2. [Initialization](#initialization)
3. [Configuring users](#configuring-users)
4. [Processing data frames](#processing-data-frames)
5. [Processing state changes](#processing-state-changes)
6. [Text chat](#text-chat)
7. [Accessibility](#accessibility)
8. [UI](#ui)
9. [Muting](#muting)
10. [Bad reputation auto-mute](#bad-reputation-auto-mute)
11. [Privilege and privacy](#privilege-and-privacy)
12. [Cleanup](#cleanup)
13. [Failure model](#failure-model)
14. [How to configure popular scenarios](#how-to-configure-popular-scenarios)

## Prerequisites

Before you get started coding with Game Chat 2, you must have configured your app's AppXManifest to declare the "microphone" device capability. AppXManifest capabilities are described in more detail in their respective sections of the platform documentation; the following snippet shows the "microphone" device capability node that should exist under the Package/Capabilities node or else chat will be blocked:

```xml
 <?xml version="1.0" encoding="utf-8"?>
 <Package ...>
   <Identity ... />
   ...
   <Capabilities>
     <DeviceCapability Name="microphone" />
   </Capabilities>
 </Package>
```

Compiling Game Chat 2 requires including the primary GameChat2.h header. In order to link properly, your project must also include GameChat2Impl.h in at least one compilation unit (a common precompiled header is recommended since these stub function implementations are small and easy for the compiler to generate as "inline").

The Game Chat 2 interface does not require a project to choose between compiling with C++/CX versus traditional C++; it can be used with either. The implementation also doesn't throw exceptions as a means of non-fatal error reporting so you can consume it easily from exception-free projects if preferred. The implementation does, however, throw exceptions as a means of fatal error reporting (see [Failure model](#failure) for more detail).

## Initialization

You begin interacting with the library by initializing the Game Chat 2 singleton instance with parameters that apply to the lifetime of the singleton's initialization.

```cpp
chat_manager::singleton_instance().initialize(...);
```

## Configuring users

Once the instance is initialized, you must add the local users to the Game Chat 2 instance. In this example, User A will represent a local user.

```cpp
chat_user* chatUserA = chat_manager::singleton_instance().add_local_user(<user_a_xuid>);
```

You must also add the remote users and identifiers that will be used to represent the remote "Endpoint" that the user is on. An "Endpoint" is an instance of the app running on a remote device. In this example, User B is on Endpoint X, Users C and D are on Endpoint Y. Endpoint X is arbitrarily assigned identifier "1" and Endpoint Y is arbitrarily assigned identifier "2". You inform Game Chat 2 of the remote users with these calls:

```cpp
chat_user* chatUserB = chat_manager::singleton_instance().add_remote_user(<user_b_xuid>, 1);
chat_user* chatUserC = chat_manager::singleton_instance().add_remote_user(<user_c_xuid>, 2);
chat_user* chatUserD = chat_manager::singleton_instance().add_remote_user(<user_d_xuid>, 2);
```

Now you configure the communication relationship between each remote user and the local user. In this example, suppose that User A and User B are on the same team and bidirectional communication is allowed. `c_communicationRelationshipSendAndReceiveAll` is a constant defined in GameChat2.h to represent bi-directional communication. Set User A's relationship to User B with:

```cpp
chatUserA->local()->set_communication_relationship(chatUserB, c_communicationRelationshipSendAndReceiveAll);
```

Now suppose that Users C and D are 'spectators' and should be allowed to listen to User A, but not speak. `c_communicationRelationshipSendAll` is a constant defined in GameChat2.h to represent this unidirectional communication. Set the relationships with:

```cpp
chatUserA->local()->set_communication_relationship(chatUserC, c_communicationRelationshipSendAll);
chatUserA->local()->set_communication_relationship(chatUserD, c_communicationRelationshipSendAll);
```

If at any point there are remote users that have been added to the singleton instance but haven't been configured to communicate with any local users - that's okay! This may be expected in scenarios where users are determining teams or can change speaking channels arbitrarily. Game Chat 2 will only cache information (e.g. privacy relationships and reputation) for users that have been added to the instance, so it's useful to inform Game Chat 2 of all possible users even if they can't speak to any local users at a particular point in time.

Finally, suppose that User D has left the game and should be removed from the local Game Chat 2 instance. That can be done with the following call:

```cpp
chat_manager::singleton_instance().remove_user(chatUserD);
```

Calling `chat_manager::remove_user()` may invalidate the user object. If you are using [real-time audio manipulation](real-time-audio-manipulation.md), please refer to the [Chat user lifetimes](real-time-audio-manipulation.md#chat-user-lifetimes) documentation for further information. Otherwise, the user object is invalidated immediately when `chat_manager::remove_user()` is called. A subtle restriction on when users can be removed is detailed in [Processing state changes](#state).

## Processing data frames

Game Chat 2 does not have its own transport layer; this must be provided by the app. This plugin is managed by the app's regular, frequent calls to the `chat_manager::start_processing_data_frames()` and `chat_manager::finish_processing_data_frames()` pair of methods. These methods are how Game Chat 2 provides outgoing data to the app. They're designed to operate quickly such that they can be polled frequently on a dedicated networking thread. This provides a convenient place to retrieve all queued data without worrying about the unpredictability of network timing or multi-threaded callback complexity.

When `chat_manager::start_processing_data_frames()` is called, all queued data is reported in an array of `game_chat_data_frame` structure pointers. Apps should iterate over the array, inspect the target "endpoints", and use the app's networking layer to deliver the data to the appropriate remote app instances. Once finished with all the `game_chat_data_frame` structures, the array should be passed back to Game Chat 2 to release the resources by calling `chat_manager:finish_processing_data_frames()`. For example:

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

## Processing state changes

Game Chat 2 provides updates to the app, such as received text messages, through the app's regular, frequent calls to the `chat_manager::start_processing_state_changes()` and `chat_manager::finish_processing_state_changes()` pair of methods. They're designed to operate quickly such that they can be called every graphics frame in your UI rendering loop. This provides a convenient place to retrieve all queued changes without worrying about the unpredictability of network timing or multi-threaded callback complexity.

When `chat_manager::start_processing_state_changes()` is called, all queued updates are reported in an array of `game_chat_state_change` structure pointers. Apps should iterate over the array, inspect the base structure for its more specific type, cast the base structure to the corresponding more detailed type, and then handle that update as appropriate. Once finished with all `game_chat_state_change` objects currently available, that array should be passed back to Game Chat 2 to release the resources by calling `chat_manager::finish_processing_state_changes()`. For example:

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

Because `chat_manager::remove_user()` immediately invalidates the memory associated with a user object, and state changes may contain pointers to user objects, `chat_manager::remove_user()` must not be called while processing state changes.

## Text chat

To send text chat, use `chat_user::chat_user_local::send_chat_text()`. For example:

```cpp
chatUserA->local()->send_chat_text(L"Hello");
```

Game Chat 2 will generate a data frame containing this message; the target endpoints for the data frame will be those associated with users that have been configured to receive text from the local user. When the data is processed by the remote endpoints, the message will be exposed via a `game_chat_text_chat_received_state_change`. As with voice chat, privilege and privacy restrictions are respected for text chat. If a pair of users have been configured to allow text chat, but privilege or privacy restrictions disallow that communication, the text message will be dropped.

Supporting text chat input and display is required for accessibility (see [Accessibility](#access) for more details).

## Accessibility

Supporting text chat input and display is required. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, users may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because users may configure the system to use speech-to-text. These preferences can be detected on local users by calling the `chat_user::chat_user_local::text_to_speech_conversion_preference_enabled()` and `chat_user::chat_user_local::speech_to_text_conversion_preference_enabled()` methods respectively, and you may wish to conditionally enable text mechanisms.

### Text-to-speech

When a user has text-to-speech enabled, `chat_user::chat_user_local::text_to_speech_conversion_preference_enabled()` will return 'true'. When this state is detected, the app must provide a method of text input. Once you have the text input provided by a real or virtual keyboard, pass the string to the `chat_user::chat_user_local::synthesize_text_to_speech()` method. Game Chat 2 will detect and synthesize audio data based on the string and the user's accessible voice preference. For example:

```cpp
chat_userA->local()->synthesize_text_to_speech(L"Hello");
```

The audio synthesized as part of this operation will be transported to all users that have been configured to receive audio from this local user. If `chat_user::chat_user_local::synthesize_text_to_speech()` is called on a user who does not have text-to-speech enabled Game Chat 2 will take no action.

### Speech-to-text

When a user has speech-to-text enabled, `chat_user::chat_user_local::speech_to_text_conversion_preference_enabled()` will return true. When this state is detected, the app must be prepared to provide UI associated with transcribed chat messages. Game Chat 2 will automatically transcribe each remote user's audio and expose it via a `game_chat_transcribed_chat_received_state_change`.

> `Windows::Xbox::UI::Accessibility` is an Xbox One class specifically designed to provide simple rendering of in-game text chat with a focus on speech-to-text assistive technologies.

### Speech-to-text performance considerations

When speech-to-text is enabled, the Game Chat 2 instance on each remote device initiates a web socket connection with the speech services endpoint. Each remote Game Chat 2 client uploads audio to the speech services endpoint through this websocket; the speech services endpoint occasionally returns a transcription message to the remote device. The remote device then sends the transcription message (i.e. a text message) to the local device, where the transcribed message is given by Game Chat 2 to the app to render.

Therefore, the primary performance cost of speech-to-text is network usage. Most of the network traffic is the upload of encoded audio. The websocket uploads audio that has already been encoded by Game Chat 2 in the “normal” voice chat path; the app has control over the bitrate via `chat_manager::set_audio_encoding_type_and_bitrate`.

## UI

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

## Muting

The `chat_user::chat_user_local::set_microphone_muted()` method can be used to toggle the mute state of a local user's microphone. When the microphone is muted, no audio from that microphone will be captured. If the user is on a shared device, such as Kinect, the mute state applies to all users.

The `chat_user::chat_user_local::microphone_muted()` method can be used to retrieve the mute state of a local user's microphone. This method only reflects whether the local user's microphone has been muted in software via a call to `chat_user::chat_user_local::set_microphone_muted()`; this method does not reflect a hardware mute controlled, for instance, via a button on the user's headset. There is no method for retrieving the hardware mute state of a user's audio device through Game Chat 2.

The `chat_user::chat_user_local::set_remote_user_muted()` method can be used to toggle the mute state of a remote user in relation to a particular local user. When the remote user is muted, the local user won't hear any audio or receive any text messages from the remote user.

## Bad reputation auto-mute

Typically, remote users will start off unmuted. Game Chat 2 will start the users in a muted state when (1) the remote user isn't friends with the local user, and (2) the remote user has a bad reputation flag. When users are muted due to this operation, `chat_user::chat_indicator()` will return `game_chat_user_chat_indicator::reputation_restricted`. This state will be overridden by the first call to `chat_user::chat_user_local::set_remote_user_muted()` that includes the remote user as the target user.

## Privilege and privacy

On top of the communication relationship configured by the game, Game Chat 2 enforces privilege and privacy restrictions. Game Chat 2 performs privilege and privacy restriction lookups when a user is first added; the user's `chat_user::chat_indicator()` will always return `game_chat_user_chat_indicator::silent` until those operations complete. If communication with a user is affected by a privilege or privacy restriction, the user's `chat_user::chat_indicator()` will return `game_chat_user_chat_indicator::platform_restricted`. Platform communication restrictions apply to both voice and text chat; there will never be an instance where text chat is blocked by a platform restriction but voice chat is not, or vice versa.

`chat_user::chat_user_local::get_effective_communication_relationship()` can be used to help distinguish when users can't communicate due to incomplete privilege and privacy operations. It returns the communication relationship enforced by Game Chat 2 in the form of `game_chat_communication_relationship_flags` and the reason the relationship may not be equal to the configured relationship in the form of `game_chat_communication_relationship_adjuster`. For example, if the lookup operations are still in progress, the `game_chat_communication_relationship_adjuster` will be `game_chat_communication_relationship_adjuster::intializing`. This method is expected to be used in development and debugging scenarios; it should not be used to influence UI (see [UI](#UI)).

## Cleanup

When the app no longer needs communications via Game Chat 2, you should call `chat_manager::cleanup()`. This allows Game Chat 2 to reclaim resources that were allocated to manage the communications.

## Failure model

The Game Chat 2 implementation doesn't throw exceptions as a means of non-fatal error reporting so you can consume it easily from exception-free projects if preferred. Game Chat 2 does, however, throw exceptions to inform about fatal errors. These errors are a result of API misuse, such as adding a user to the Game Chat instance before initializing the instance or accessing a user object after it has been removed from the Game Chat 2 instance. These errors are expected to be caught early in development and can be corrected by modifying the pattern used to interact with Game Chat 2. When such an error occurs, a hint as to what caused the error is printed to the debugger before the exception is raised.

## How to configure popular scenarios

### Push-to-talk

Push-to-talk should be implemented with `chat_user::chat_user_local::set_microphone_muted()`. Call `set_microphone_muted(false)` to allow speech and `set_microphone_muted(true)` to restrict it. This method will provide the lowest latency response from Game Chat 2.

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
