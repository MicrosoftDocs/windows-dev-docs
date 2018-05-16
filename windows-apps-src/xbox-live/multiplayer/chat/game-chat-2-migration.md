---
title: Game Chat 2 Migration
author: KevinAsgari
description: Learn how to migrate existing Game Chat code to use Game Chat 2.
ms.author: kevinasg
ms.date: 5/2/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, game chat 2, game chat, voice communication
ms.localizationpriority: low
---

# Migration from Game Chat to Game Chat 2

This document details the similarities between Game Chat and Game Chat 2 and how to migrate from Game Chat to Game Chat 2. As such, it is for titles that have an existing Game Chat implementation that wish to migrate to Game Chat 2. If you don't already have a Game Chat implementation, the suggested starting point is [Using Game Chat 2](using-game-chat-2.md). This document contains the following topics:

1. [Preface](#preface)
2. [Prerequisites](#prerequisites)
3. [Initialization](#initialization)
4. [Configuring Users](#configuring-users)
5. [Processing data](#processing-data)
6. [Processing events](#processing-events)
7. [Text chat](#text-chat)
8. [Accessibility](#accessibility)
9. [UI](#UI)
10. [Muting](#muting)
11. [Bad reputation auto-mute](#bad-reputation-auto-mute)
12. [Privilege and privacy](#privilege-and-privacy)
13. [Cleanup](#cleanup)
14. [Failure model and debugging](#failure-model-and-debugging)
15. [How to configure popular scenarios](#how-to-configure-popular-scenarios)
16. [Pre-encode and post-decode audio manipulation](#pre-encode-and-post-decode-audio-manipulation)

## Preface

The original Game Chat API is a WinRT API that exposed a concept of chat users and voice channels to assist in the implementation of Xbox Live in-game voice chat scenarios. The Game Chat API is built on top of the Chat API, which itself is a WinRT API that exposed a concept of chat users and voice channels while requiring low-level management of audio devices. Game Chat 2 is the successor to the original Game Chat and Chat APIs - it was designed to be a simpler API for basic chat scenarios, such as team communication, while simultaneously providing more flexibility for advanced chat scenarios, such as broadcast-style communication and real-time audio manipulation. Both Game Chat and Game Chat 2 fill the same niche: the APIs each provide a convenient method for integrating Xbox Live enabled, in-game voice chat, while the title provides a transport layer for transmitting data packets to and from remote instances of Game Chat or Game Chat 2.

The Game Chat 2 API has many advantages over the original Game Chat and Chat APIs. Some highlights include:
* A flexible user-oriented API that is not restricted to the "channel" model.
* Bandwidth improvements due to packet filtering by both data sources and data receivers.
* An internal restriction to 2 long-lived threads with app-configurable affinity.
* An API available in both C++ and WinRT forms.
* Simplified consumption model aligning with a "do work" pattern.
* Memory hooks to redirect Game Chat 2 allocations through a custom allocator.
* Identical UWP + Exclusive Resource Application (ERA) headers for a more convenient cross-plat development experience.

This document details the similarities between Game Chat and Game Chat 2 and how to migrate from Game Chat to the Game Chat 2 C++ API. If you are interested in migration from Game Chat to the Game Chat 2 WinRT API, it is suggested that you read this document to understand how to map Game Chat concepts to Game Chat 2, and then see [Using Game Chat 2 WinRT Projections](using-game-chat-2-winrt.md) for the patterns specific to WinRT. The sample code for the original Game Chat in this document will use C++/CX.

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

### Game Chat

Interacting with the original Game Chat is done through the `ChatManager` class. The following example shows how to construct a `ChatManager` instance using default parameters:

```cppwinrt
auto chatManager = ref new ChatManager();
```

### Game Chat 2

All interaction with Game Chat 2 is done through Game Chat 2's `chat_manager` singleton. The singleton must be initialized before any meaningful interaction with the library can occur. The singleton requires that the maximum number of concurrent local and remote chat users be specified at initialization time; this is because Game Chat 2 pre-allocates memory proportional to the expected number of users. The following example shows how to intialize the singleton instance when the maximum number of concurrent local and remote chat users will be four:

```cpp
chat_manager::singleton_instance().initialize(4);
```

There are several optional parameters that can be specified during initialization as well, such as the default render volume of remote chat users and whether Game Chat 2 should perform automatic speech-to-text conversion. For more detail on these parameters, refer to the documentation of `chat_manager::initialize()`.

## Configuring users

### Game Chat

Adding local users to the original Game Chat API is done through `ChatManager::AddLocalUserToChatChannelAsync()`. Adding the user to multiple chat channels requires multiple calls, each specifying a different channel. The following example shows how to add the user with xuid "myXuid" to channel 0:

```cppwinrt
auto asyncOperation = chatManager->AddLocalUserToChatChannelAsync(0, L"myXuid");
``` 

Remote users are not added to the instance directly. When a remote device appears in the title's network, the title creates an object that represents the remote device and informs Game Chat of the new device via `ChatManager::HandleNewRemoteConsole()`. The title must also provide a method of comparing the objects representing the remote devices to Game Chat by implementing `CompareUniqueConsoleIdentifiersHandler`. The following example shows how to provide this delegate to Game Chat, assuming `Platform::String` objects are used to represent remote devices and a new device represented by string `L"1"` has joined the title's network:

```cppwinrt
auto token = chatManager->OnCompareUniqueConsoleIdentifiers += 
    ref new CompareUniqueConsoleIdentifiersHandler( 
        [this](Platform::Object^ obj1, Platform::Object^ obj2) 
    {
        return (static_cast<Platform::String^>(obj1)->Equals(static_cast<Platform::String^>(obj2)));
    });

...

Platform::String^ newDeviceIdentifier = ref new Platform::String(L"1");
chatManager->HandleNewRemoteConsole(newDeviceIdentifier);
```

Once Game Chat has been informed of this device, it will generate packets containing information about any local users and provide these packets to the title for transmission to the Game Chat instance on the remote device. Similarly, the Game Chat instance on the remote device will generate packets containing information about users on that remote device that the title will transmit to the local instance of Game Chat. When the local instance receives packets containing information about new remote users, the new remote users are added to the local instance's list of users, available through `ChatManager::GetUsers()`.

Removing users from the Game Chat instance is performed through similar calls to `ChatManager::RemoveLocalUserFromChatChannelAsync()` and `ChatManager::RemoveRemoteConsoleAsync()`

### Game Chat 2

Adding local users to Game Chat 2 is done synchronously through `chat_manager::add_local_user()`. In this example, User A will represent a local user with Xbox User Id `L"myLocalXboxUserId"`:

```cpp
chat_user* chatUserA = chat_manager::singleton_instance().add_local_user(L"myLocalXboxUserId");
```

Notice that the user is not added to a particular channel - Game Chat 2 uses a concept of "communication relationships" rather than channels to manage whether users can speak to and hear each other. The method of configuring communication relationships is addressed later in this section. 

Similar to local users, remote users are added synchronously to the local Game Chat 2 instance. The remote users must simultaneously be associated with identifiers that will be used to represent the remote device. Game Chat 2 refers to an instance of the app running on a remote device as an "Endpoint". In this example, User B will be a user with Xbox User Id `L"remoteXboxUserId"` on an endpoint represented by the integer `1`.

```cpp
chat_user* chatUserB = chat_manager::singleton_instance().add_remote_user(L"remoteXboxUserId", 1);
```

Once the users have been added to the instance, you should configure the "communication relationship" between each remote user and each local user. In this example, suppose that User A and User B are on the same team and bidirectional communication is allowed. `c_communicationRelationshiSendAndReceiveAll` is a constant defined in GameChat2.h to represent bi-directional communication. The relationship can be configured with:

```cpp
chatUserA->local()->set_communication_relationship(chatUserB, c_communicationRelationshipSendAndReceiveAll);
```

Finally, suppose that User B has left the game and should be removed from the local Game Chat instance. That is performed synchronously with the following call:

```cpp
chat_manager::singleton_instance().remove_user(chatUserD);
```

Refer to [Using Game Chat 2 - Configuring Users](using-game-chat-2.md#configuring-users) for more detailed examples or the reference for individual API methods for more information.

## Processing data

### Game Chat
 
Game Chat does not have its own transport layer; this must be provided by the app. Outgoing packets are handled by subscribing to the `OnOutgoingChatPacketReady` event and inspecting the arguments to determine the packet destination and transport requirements. The following example shows how to subscribe to the event and forward the arguments the `HandleOutgoingPacket()` method implemented by the title:

```cppwinrt
auto token = chatManager->OnOutgoingChatPacketReady +=
    ref new Windows::Foundation::EventHandler<Microsoft::Xbox::GameChat::ChatPacketEventArgs^>(
        [this](Platform::Object^, Microsoft::Xbox::GameChat::ChatPacketEventArgs^ args)
    {
        this->HandleOutgoingPacket(args);
    });
```

Incoming packets are submitted to Game Chat via `ChatManager::ProcessingIncomingChatMessage()`. The raw packet buffer and the remote device identifier must be provided. The following example shows how to submit a packet that is stored in the local `packetBuffer` and remote device identifier stored in the local variable `remoteIdentifier`.

```cppwinrt
Platform::String^ remoteIdentifier = /* The identifier associated with the device that generated this packet */
Windows::Storage::Streams::IBuffer^ packetBuffer = /* The incoming chat packet */

chatManager->ProcessIncomingChatMessage(packetBuffer, remoteIdentifier);
```

### Game Chat 2

Similarly, Game Chat 2 does not have its own transport layer; this must be provided by the app. Outgoing packets are handled by the app's regular, frequent calls to the `chat_manager::start_processing_data_frames()` and `chat_manager::finish_processing_data_frames()` pair of methods. These methods are how Game Chat 2 provides outgoing data to the app. They're designed to operate quickly such that they can be polled frequently on a dedicated networking thread. This provides a convenient place to retrieve all queued data without worrying about the unpredictability of network timing or multi-threaded callback complexity.

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

Incoming data is submitted to Game Chat 2 via `chat_manager::processing_incoming_data()`. The data buffer and the remote endpoint identifier must be provided. The following example shows how to submit a data packet that is stored in the local variable `dataFrame` and the remote endpoint identifier stored in the local variable `remoteEndpointIdentifier`:

```cpp
uin64_t remoteEndpointIdentier = /* The identifier associated with the endpoint that generated this packet */
uint32_t dataFrameSize = /* The size of the incoming data frame, in bytes */
uint8_t* dataFrame = /* A pointer to the buffer containing the incoming data */

chatManager::singleton_instance().process_incoming_data(remoteEndpointIdentifier, dataFrameSize, dataFrame);
```

## Processing events

### Game Chat

Game Chat uses an eventing model to inform the app when something of interest occurs - such as the receipt of a text message or the changing of a user's accessibility preference. The app must subscribe to and implement a handler for each event of interest. This example shows how to subscribe to the `OnTextMessageReceived` event and forward the arguments to the `OnTextChatReceived()` or `OnTranscribedChatReceived()` methods implemented by the app:

```cppwinrt
auto token = chatManager->OnTextMessageReceived +=
    ref new Windows::Foundation::EventHandler<Microsoft::Xbox::GameChat::TextMessageReceivedEventArgs^>(
        [this](Platform::Object^, Microsoft::Xbox::GameChat::TextMessageReceivedEventArgs^ args)
    {
        if (args->ChatTextMessageType != ChatTextMessageType::TranscribedSpeechMessage)
        {
            this->OnTextChatReceived(args);
        }
        else
        {
            this->OnTranscribedChatReceived(args);
        }
    });
```

### Game Chat 2

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

### Game Chat 

To send text chat with Game Chat, `GameChatUser::GenerateTextMessage()` can be used. The following example shows how to send a chat text message with a local chat user represented by the `chatUser` variable:

```cppwinrt
chatUser->GenerateTextMessage(L"Hello", false);
```

The second boolean parameter controls text-to-speech conversion. For more details, see [Accessibility](#accessibilityGame Chat then generates a chat packet containing this message. Remote instances of Game Chat will be notified of the text message via the `OnTextMessageReceived` event.

### Game Chat 2

To send text chat with Game Chat 2, use `chat_user::chat_user_local::send_chat_text()`. This following example shows how to send a chat text message with a local chat user represented by the `chatUser` variable:

```cpp
chatUser->local()->send_chat_text(L"Hello");
```

Game Chat 2 will generate a data frame containing this message; the target endpoints for the data frame will be those associated with users that have been configured to receive text from the local user. When the data is processed by the remote endpoints, the message will be exposed via a `game_chat_text_chat_received_state_change`. As with voice chat, privilege and privacy restrictions are respected for text chat. If a pair of users have been configured to allow text chat, but privilege or privacy restrictions disallow that communication, the text message will be silently dropped.

Supporting text chat input and display is required for accessibility (see [Accessibility](#accessibility) for more details).

## Accessibility

Supporting text chat input and display is required for both Game Chat and Game Chat 2. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, users may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because users may configure the system to use speech-to-text. Both Game Chat and Game Chat 2 provide methods of detecting and respecting a user's accessibility preferences; you may wish to conditionally enable text mechanisms based on these settings.

### Text-to-speech - Game Chat

When a user has text-to-speech enabled, `GameChatUser::HasRequestedSynthesizedAudio()` will return true. When this state is detected, `GameChatUser::GenerateTextMessage()` will additionally generate text-to-speech audio that is inserted into the audio stream associated with the local user. The following example shows how to send a chat text message with a local user represented by the `chatUser` variable:

```cppwinrt
chatUser->GenerateTextMessage(L"Hello", true);
```

The second boolean parameter is used to allow the app to override the speech-to-text preference - 'true' indicates that Game Chat should generate text-to-speech audio when the user's text-to-speech preference has been enabled, while 'false' indicates that Game Chat should never generate text-to-speech audio from the message.

### Text-to-speech - Game Chat 2

When a user has text-to-speech enabled, `chat_user::chat_user_local::text_to_speech_conversion_preference_enabled()` will return 'true'. When this state is detected, the app must provide a method of text input. Once you have the text input provided by a real or virtual keyboard, pass the string to the `chat_user::chat_user_local::synthesize_text_to_speech()` method. Game Chat 2 will detect and synthesize audio data based on the string and the user's accessible voice preference. For example:

```cpp
chat_userA->local()->synthesize_text_to_speech(L"Hello");
```

The audio synthesized as part of this operation will be transported to all users that have been configured to receive audio from this local user. If `chat_user::chat_user_local::synthesize_text_to_speech()` is called on a user who does not have text-to-speech enabled Game Chat 2 will take no action.

### Speech-to-text - Game Chat

When a user has speech-to-text enabled, `GameChatUser::HasRequestSynthesizedAudio()` will return 'true'. When this state is detected, Game Chat will automatically transcribe the audio of each remote user's audio and expose it via the `OnTextMessageReceived` event. When the `OnTextMessageReceived` event fires due to the receipt of a transcription message, the `TextMessageReceivedEventArgs` will indicate a message type of `ChatTextMessageType::TranscribedSpeechMessage`.

### Speech-to-text - Game Chat 2

When a user has speech-to-text enabled, `chat_user::chat_user_local::speech_to_text_conversion_preference_enabled()` will return true. When this state is detected, the app must be prepared to provide UI associated with transcribed chat messages. Game Chat 2 will automatically transcribe each remote user's audio and expose it via a `game_chat_transcribed_chat_received_state_change`.

> `Windows::Xbox::UI::Accessibility` is an Xbox One class specifically designed to provide simple rendering of in-game text chat with a focus on speech-to-text assistive technologies.

Refer to [Using Game Chat 2 - Speech-to-text performance considerations](using-game-chat-2.md#speech-to-text-performance-considerations) for more details on speech-to-text performance considerations.

## UI

It's recommended that anywhere players are shown, particularly in a list of gamertags such as a scoreboard, that you also display muted/speaking icons as feedback for the user. Game Chat 2 introduces a coalesced indicator that provides a simpler means of determining the appropriate UI elements to show.

### Game Chat

A `GameChatUser` has three properties that are commonly inspected when determining appropriate UI elements - `GameChatUser::TalkingMode`, `GameChatUser::IsMuted`, and `GameChatUser::RestrictionMode`. The following example demonstrates a possible heuristic for determining a particular icon constant vlaue to assign to an `iconToShow` variable from a `GameChatUser` object pointed to by the variable 'chatUser'.

```cppwinrt
if (chatUser->RestrictionMode != None)
{
    if (!chatUser->IsMuted)
    {
        if (chatUser->TalkingMode != NotTalking)
        {
            iconToShow = Icon_ActiveSpeaker;
        }
        else
        {
            iconToShow = Icon_InactiveSpeaker;
        }
    }
    else
    {
        iconToShow = Icon_MutedSpeaker;
    }
}
else
{
    iconToShow = Icon_RestrictedSpeaker;
}
```

### Game Chat 2

Game Chat 2 has a coalesced `game_chat_user_chat_indicator` used to represent the current, instantaneous status of chat for a player. This value is retrieved by calling `chat_user::chat_indicator()`. The following example demonstrates retrieving the indicator value for a `chat_user` object pointed to by the variable 'chatUser' to determine a particular icon constant value to assign to an 'iconToShow' variable:

```cpp
switch (chatUser->chat_indicator())
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

## Muting

## Game Chat

Muting or unmuting a user in Game Chat is performed through a call to `ChatManager::MuteUserFromAllChannels()` or `ChatManager::UnMuteUIserFromAllChannels()`, respectively. The mute state of a user can be retrieved by inspecting `GameChatUser::IsMuted` or `GameChatUser::IsLocalUserMuted`.

## Game Chat 2

The `chat_user::chat_user_local::set_microphone_muted()` method can be used to toggle the mute state of a local user's microphone. When the microphone is muted, no audio from that microphone will be captured. If the user is on a shared device, such as Kinect, the mute state applies to all users.

The `chat_user::chat_user_local::microphone_muted()` method can be used to retrieve the mute state of a local user's microphone. This method only reflects whether the local user's microphone has been muted in software via a call to `chat_user::chat_user_local::set_microphone_muted()`; this method does not reflect a hardware mute controlled, for instance, via a button on the user's headset. There is no method for retrieving the hardware mute state of a user's audio device through Game Chat 2.

The `chat_user::chat_user_local::set_remote_user_muted()` method can be used to toggle the mute state of a remote user in relation to a particular local user. When the remote user is muted, the local user won't hear any audio or receive any text messages from the remote user.

## Bad reputation auto-mute

Typically, remote users will start off unmuted. Both Game Chat and Game Chat 2 have a "bad reputation auto-mute" feature. This means that users will start off in a muted state when (1) the remote user isn't friends with a local user, and (2) the remote user has a bad reputation flag. Game Chat 2 provides feedback when a user is muted due to this feature. Refer to [Using Game Chat 2 - Bad reputation auto-mute](using-game-chat-2.md#bad-reputation-auto-mute) for more information.

## Privilege and privacy

Both Game Chat and Game Chat 2 enforce Xbox Live privilege and privacy restrictions on top of any channel or communication relationships managed by the app. Game Chat 2 additionally provides diagnostic information to determine exactly how the restriction is impacting the direction of audio (e.g. whether audio an audio restriction is uni- or bi-directional).

### Game Chat

Game Chat exposed privilege and privacy information throught the `RestrictionMode` property. It can be retrieved by inspecting `GameChatUser::RestrictionMode`.

### Game Chat 2

Game Chat 2 performs privilege and privacy restriction lookups when a user is first added; the user's `chat_user::chat_indicator()` will always return `game_chat_user_chat_indicator::silent` until those operations complete. If communication with a user is affected by a privilege or privacy restriction, the user's `chat_user::chat_indicator()` will return `game_chat_user_chat_indicator::platform_restricted`. Platform communication restrictions apply to both voice and text chat; there will never be an instance where text chat is blocked by a platform restriction but voice chat is not, or vice versa.

`chat_user::chat_user_local::get_effective_communication_relationship()` can be used to help distinguish when users can't communicate due to incomplete privilege and privacy operations. It returns the communication relationship enforced by Game Chat 2 in the form of `game_chat_communication_relationship_flags` and the reason the relationship may not be equal to the configured relationship in the form of `game_chat_communication_relationship_adjuster`. For example, if the lookup operations are still in progress, the `game_chat_communication_relationship_adjuster` will be `game_chat_communication_relationship_adjuster::intializing`. This method is expected to be used in development and debugging scenarios; it should not be used to influence UI (see [UI](#UI)).

## Cleanup

The original Game Chat's `ChatManager` is a WinRT runtime class - a reference counted interface. As such, it's memory resources are reclaimed when the last reference count drops to zero and it cleans up. Interaction with Game Chat 2's C++ API is done through a singleton instance; when the app no longer needs communications via Game Chat 2, you should call `chat_manager::cleanup()`. This allows Game Chat 2 immediately release all resources that were allocated to manage communications. For details on Game Chat 2's WinRT API and resource management, see [Using Game Chat 2 WinRT Projections](using-game-chat-2-winrt.md#cleanup).

## Failure model and debugging

The original Game Chat is a WinRT API; thus, it reported errors through exceptions. Non-fatal errors or warnings are reported through an optional debug callback. Game Chat 2's C++ API doesn't throw exceptions as a means of non-fatal error reporting so you can consume it easily from exception-free projects if preferred. Game Chat 2 does, however, throw exceptions to inform about fatal errors. These errors are a result of API misuse, such as adding a user to the Game Chat instance before initializing the instance or accessing a user object after it has been removed from the Game Chat instance. These errors are expected to be caught early in development and can be corrected by modifying the pattern used to interact with Game Chat 2. When such an error occurs, a hint as to what caused the error is printed to the debugger before the exception is raised. Game Chat 2's WinRT API follows the WinRT pattern of reporting errors through exceptions.

## How to configure popular scenarios

Refer to [Using Game Chat 2 - How to configure popular scenarios](using-game-chat-2.md#how-to-configure-popular-scenarios) for examples on how to configure popular scenarios such as push-to-talk, teams, and broadcast-style communication scenarios.

## Pre-encode and post-decode audio manipulation

The original Game Chat allowed for audio manipulation by allowing access to raw microphone audio through the `OnPreEncodeAudioBuffer` and `OnPostDecodeAudioBuffers` events. Game Chat 2 exposes this feature through a polling model. For more information, refer to [Real-time audio manipulation](real-time-audio-manipulation.md).