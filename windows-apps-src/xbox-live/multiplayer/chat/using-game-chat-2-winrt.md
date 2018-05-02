---
title: Use Game Chat 2 WinRT Projections
author: KevinAsgari
description: Learn how to use Xbox Live Game Chat 2 with WinRT projections to add voice communication to your game.
ms.author: kevinasg
ms.date: 4/11/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, game chat 2, game chat, voice communication
ms.localizationpriority: low
---

# Using Game Chat 2 (WinRT Projections)

This is a brief walkthrough on using Game Chat 2's C# API. Game developers wanting to access Game Chat 2 through C++ should see [Using Game Chat 2](using-game-chat-2.md).

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

In order to consume Game Chat 2, you must include the [Microsoft.Xbox.Services.GameChat2 nuget package](https://www.nuget.org/packages/Microsoft.Xbox.Game.Chat.2.WinRT.UWP/).

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

## Initialization <a name="init">

You begin interacting with the library by instantiating a `GameChat2ChatManager` object with the maximum number of concurrent chat users expected to be added to the instance. To change this value, the `GameChat2ChatManager` object must be disposed and recreated with the desired value. You may only have one `GameChat2ChatManager` instantiated at a time.

```cs
GameChat2ChatManager myGameChat2ChatManager = new GameChat2ChatManager(<maxChatUserCount>);
```

The `GameChat2ChatManager` object has additional, optional properties that may be configured at anytime. The following code sample assumes that the variables will be given a value before they are used to set the properties on the `myGameChat2ChatManager` object.

```cs
GameChat2SpeechToTextConversionMode mySpeechToTextConversionMode;
GameChat2SharedDeviceCommunicationRelationshipResolutionMode mySharedDeviceResolutionMode;
GameChat2CommunicationRelationship myDefaultLocalToRemoteCommunicationRelationship;
float myDefaultAudioRenderVolume;
GameChat2AudioEncodingTypeAndBitrate myAudioEncodingTypeAndBitRate;

...

myGameChat2ChatManager.SpeechToTextConversionMode = mySpeechToTextConversionMode;
myGameChat2ChatManager.SharedDeviceResolutionMode = mySharedDeviceResolutionMode;
myGameChat2ChatManager.DefaultLocalToRemoteCommunicationRelationship = myDefaultLocalToRemoteCommunicationRelationship;
myGameChat2ChatManager.DefaultAudioRenderVolume = myDefaultAudioRenderVolume;
myGameChat2ChatManager.AudioEncodingTypeAndBitRate = myAudioEncodingTypeAndBitRate;
```

## Configuring users <a name="config">

Once the instance is initialized, you must add the local users to the GC2 instance. In this example, User A will represent a local user.

```cs
string userAXuid;

...

IGameChat2ChatUser userA = myGameChat2ChatManager.AddLocalUser(userAXuid);
```

You must then add the remote users and identifers that will be used to represent the remote "Endpoints" that each remote user is on. "Endpoints" are represented by objects owned by the app that implement the `IGameChat2Endpoint` interface. The following example shows a sample implementation of `MyEndpoint` class that implements the `IGameChat2Endpoint`.

```cs
class MyEndpoint : IGameChat2Endpoint
{
    private uint endpointIdentifier;
    public MyEndpoint(uint identifier)
    {
        endpointIdentifier = identifier;
    }

    // Implementing IsSameEndpointAs, the only method on the IGameChat2Endpoint interface
    public bool IsSameEndpointAs(IGameChat2Endpoint gameChat2Endpoint)
    {
        return endpointIdentifier == ((MyEndpoint)gameChat2Endpoint).endpointIdentifier;
    }
}
```

In the following code example, users B, C, and D are remote users being added. User B is on one remote device and users C and D are on another remote device. This example assumes that the variables will be set and that `myGameChat2ChatManager` is an instance of `GameChat2ChatManager` and that "MyEndpoint" is a class that implements `IGameChat2Endpoint`.

```cs
string userBXuid;
string userCXuid;
string userDXuid;
MyEndpoint myRemoteEndpointOne;
MyEndpoint myRemoteEndpointTwo;

...

IGameChat2ChatUser remoteUserB = myGameChat2ChatManager.AddRemoteUser(userBXuid, myRemoteEndpointOne);
IGameChat2ChatUser remoteUserC = myGameChat2ChatManager.AddRemoteUser(userCXuid, myRemoteEndpointTwo);
IGameChat2ChatUser remoteUserD = myGameChat2ChatManager.AddRemoteUser(userDXuid, myRemoteEndpointTwo);
```

Now you configure the communication relationship between each remote user and the local user. In this example, suppose that User A and User B are on the same team and bidirectional communication is allowed. `GameChat2CommunicationRelationship.SendAndReceiveAll` is defined to represent bi-directional communication. Set User A's relationship to User B with:

```cs
GameChat2ChatUserLocal localUserA = userA as GameChat2ChatUserLocal;
localUserA.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.SendAndReceiveAll);
```

Now suppose that Users C and D are 'spectators' and should be allowed to listen to User A, but not speak. `GameChat2CommunicationRelationship.ReceiveAll` is a defined this unidirectional communication. Set the relationships with:

```cs
localUserA.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.ReceiveAll);
localUserA.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.ReceiveAll);
```

If at any point there are remote users that have been added to the `GameChat2ChatManager` instance but haven't been configured to communicate with any local users - that's okay! This may be expected in scenarios where users are determining teams or can change speaking channels arbitrarily. Game Chat 2 will only cache information (e.g. privacy relationships and reputation) for users that have been added to the instance, so it's useful to inform Game Chat 2 of all possible users even if they can't speak to any local users at a particular point in time.

Finally, suppose that User D has left the game and should be removed from the local Game Chat 2 instance. That can be done with the following call:

```cs
remoteUserD.Remove();
```

The user object is invalidated immediately when `Remove()` is called. The last state of the user is cached when it is removed. Any informational methods called after the user object is invalidated will reflect the user's state before it was removed. Other methods will throw an error when called.

## Processing data frames <a name="data">

Game Chat 2 does not have its own transport layer; this must be provided by the app. This plugin is managed by the app's regular, frequent calls to the `GameChat2ChatManager.GetDataFrames()`. This method is how Game Chat 2 provides outgoing data to the app. It is designed to operate quickly such that it can be polled frequently on a dedicated networking thread. This provides a convenient place to retrieve all queued data without worrying about the unpredictability of network timing or multi-threaded callback complexity.

When `GameChat2ChatManager.GetDataFrames()` is called, all queued data is reported in a list of `IGameChat2DataFrame` objects. Apps should iterate over the list, inspect the `TargetEndpointIdentifiers`, and use the app's networking layer to deliver the data to the appropriate remote app instances. In this example, `HandleOutgoingDataFrame` is a function that sends the data in the `Buffer` to each user on each "Endpoint" specified in the `TargetEndpointIdentifiers` according to the `TransportRequirement`.

```cs
IReadOnlyList<IGameChat2DataFrame> frames = myGameChat2ChatManager.GetDataFrames();
foreach (IGameChat2DataFrame dataFrame in frames)
{
    HandleOutgoingDataFrame(
        dataFrame.Buffer,
        dataFrame.TargetEndpointIdentifiers,
        dataFrame.TransportRequirement
        );
}
```

The more frequently the data frames are processed, the lower the audio latency apparent to the end user will be. The audio is coalesced into 40 ms data frames; this is the suggested polling period.

## Processing state changes <a name="state">

Game Chat 2 provides updates to the app, such as received text messages, through the app's regular, frequent calls to the `GameChat2ChatManager.GetStateChanges()` method. It's designed to operate quickly such that it can be called every graphics frame in your UI rendering loop. This provides a convenient place to retrieve all queued changes without worrying about the unpredictability of network timing or multi-threaded callback complexity.

When `GameChat2ChatManager.GetStateChanges()` is called, all queued updates are reported in a list of `IGameChat2StateChange` objects. Apps should:
1. Iterate over the list
2. Inspect the base structure for its more specific type
3. Cast the base structure to the corresponding more detailed type
4. Handle that update as appropriate. 

```cs
IReadOnlyList<IGameChat2StateChange> stateChanges = myGameChat2ChatManager.GetStateChanges();
foreach (IGameChat2StateChange stateChange in stateChanges)
{
    switch (stateChange.Type)
    {
        case GameChat2StateChangeType.CommunicationRelationshipAdjusterChanged:
        {
            MyHandleCommunicationRelationshipAdjusterChanged(stateChange as GameChat2CommunicationRelationshipAdjusterChangedStateChange);
            break;
        }
        case GameChat2StateChangeType.TextChatReceived:
        {
            MyHandleTextChatReceived(stateChange as GameChat2TextChatReceivedStateChange);
            break;
        }

        ...
    }
}
```

## Text chat <a name="text">

To send text chat, use `GameChat2ChatUserLocal.SendChatText()`. For example:

```cs
localUserA.SendChatText("Hello");
```

Game Chat 2 will generate a data frame containing this message; the target endpoints for the data frame will be those associated with users that have been configured to receive text from the local user. When the data is processed by the remote endpoints, the message will be exposed via a `GameChat2TextChatReceivedStateChange`. As with voice chat, privilege and privacy restrictions are respected for text chat. If a pair of users have been configured to allow text chat, but privilege or privacy restrictions disallow that communication, the text message will be dropped.

Supporting text chat input and display is required for accessibility (see [Accessibility](#access) for more details).

## Accessibility <a name="access">

Supporting text chat input and display is required. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, users may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because users may configure the system to use speech-to-text. These preferences can be detected on local users by checking the `GameChat2ChatUserLocal.TextToSpeechConversionPreferenceEnabled` and `GameChat2ChatUserLocal.SpeechToTextConversionPreferenceEnabled` properties respectively, and you may wish to conditionally enable text mechanisms.

### Text-to-speech

When a user has text-to-speech enabled, `GameChat2ChatUserLocal.TextToSpeechConversionPreferenceEnabled` will be 'true'. When this state is detected, the app must provide a method of text input. Once you have the text input provided by a real or virtual keyboard, pass the string to the `GameChat2ChatUserLocal.SynthesizeTextToSpeech()` method. Game Chat 2 will detect and synthesize audio data based on the string and the user's accessible voice preference. For example:

```cs
localUserA.SynthesizeTextToSpeech("Hello");
```

The audio synthesized as part of this operation will be transported to all users that have been configured to receive audio from this local user. If `GameChat2ChatUserLocal.SynthesizeTextToSpeech()` is called on a user who does not have text-to-speech enabled Game Chat 2 will take no action.

### Speech-to-text

When a user has speech-to-text enabled, `GameChat2ChatUserLocal.SpeechToTextConversionPreferenceEnabled` will be true. When this state is detected, the app must be prepared to provide UI associated with transcribed chat messages. GC will automatically transcribe each remote user's audio and expose it via a `GameChat2TranscribedChatReceivedStateChange`.

### Speech-to-text performance considerations

When speech-to-text is enabled, the Game Chat 2 instance on each remote device initiates a web socket connection with the speech services endpoint. Each remote Game Chat 2 client uploads audio to the speech services endpoint through this websocket; the speech services endpoint occasionally returns a transcription message to the remote device. The remote device then sends the transcription message (i.e. a text message) to the local device, where the transcribed message is given by Game Chat 2 to the app to render.

Therefore, the primary performance cost of speech-to-text is network usage. Most of the network traffic is the upload of encoded audio. The websocket uploads audio that has already been encoded by Game Chat 2 in the “normal” voice chat path; the app has control over the bitrate via `GameChat2ChatManager.AudioEncodingTypeAndBitrate`.

## UI <a name="UI">

It's recommended that anywhere players are shown, particularly in a list of gamertags such as a scoreboard, that you also display muted/speaking icons as feedback for the user. The `IGameChat2ChatUser.ChatIndicator` property represents the current, instantaneous status of chat for that player. The following example demonstrates retrieving the indicator value for a `IGameChat2ChatUser` object pointed to by the variable 'userA' to determine a particular icon constant value to assign to an 'iconToShow' variable:

```cs
switch (userA.ChatIndicator)
{
    case GameChat2UserChatIndicator.Silent:
    {
        iconToShow = Icon_InactiveSpeaker;
        break;
    }
    case GameChat2UserChatIndicator.Talking:
    {
        iconToShow = Icon_ActiveSpeaker;
        break;
    }
    case GameChat2UserChatIndicator.LocalMicrophoneMuted:
    {
        iconToShow = Icon_MutedSpeaker;
        break;
    }
    
    ...
}
```

The value of `IGameChat2ChatUser.ChatIndicator` is expected to change frequently as players start and stop talking, for example. It is designed to support apps polling it every UI frame as a result.

## Muting <a name="mute">

The `GameChat2ChatUserLocal.MicrophoneMuted` property can be used to toggle the mute state of a local user's microphone. When the microphone is muted, no audio from that microphone will be captured. If the user is on a shared device, such as Kinect, the mute state applies to all users. This method does not reflect a hardware mute control (e.g. a button on the user's headset). There is no method for retrieving the hardware mute state of a user's audio device through Game Chat 2.

The `GameChat2ChatUserLocal.SetRemoteUserMuted()` method can be used to toggle the mute state of a remote user in relation to a particular local user. When the remote user is muted, the local user won't hear any audio or receive any text messages from the remote user.

## Bad reputation auto-mute <a name="automute">

Typically remote users will start off unmuted. Game Chat 2 will start the users in a muted state when (1) the remote user isn't friends with the local user, and (2) the remote user has a bad reputation flag. When users are muted due to this operation, `IGameChat2ChatUser.ChatIndicator` will return `GameChat2UserChatIndicator.ReputationRestricted`. This state will be overridden by the first call to `GameChat2ChatUserLocal.SetRemoteUserMuted()` that includes the remote user as the target user.

## Privilege and privacy <a name="priv">

On top of the communication relationship configured by the game, Game Chat 2 enforces privilege and privacy restrictions. Game Chat 2 performs privilege and privacy restriction lookups when a user is first added; the user's `IGameChat2ChatUser.ChatIndicator` will always return `GameChat2UserChatIndicator.Silent` until those operations complete. If communication with a user is affected by a privilege or privacy restriciton, the user's `IGameChat2ChatUser.ChatIndicator` will return `GameChat2UserChatIndicator.PlatformRestricted`. Platform communication restrictions apply to both voice and text chat; there will never be an instance where text chat is blocked by a platform restriction but voice chat is not, or vice versa.

`GameChat2ChatUserLocal.GetEffectiveCommunicationRelationship()` can be used to help distinguish when users can't communicate due to incomplete privilege and privacy operations. It returns the communication relationship enforced by Game Chat 2 in the form of `GameChat2CommunicationRelationship` and the reason the relationship may not be equal to the configured relationship in the form of `GameChat2CommunicationRelationshipAdjuster`. For example, if the lookup operations are still in progress, the `GameChat2CommunicationRelationshipAdjuster` will be `GameChat2CommunicationRelationshipAdjuster.Initializing`. This method is expected to be used in development and debugging scenarios; it should not be used to influence UI (see [UI](#UI)).

## Cleanup <a name="cleanup">

When the app no longer needs communications via Game Chat 2, you should call `GameChat2ChatManager.Dispose()`. This allows Game Chat 2 to reclaim resources that were allocated to manage the communications.

## How to configure popular scenarios

### Push-to-talk

Push-to-talk should be implemented with `GameChat2ChatUserLocal.MicrophoneMuted`. Set `MicrophoneMuted` to false to allow speech and `MicrophoneMuted` to true to restrict it. This property change will provide the lowest latency response from Game Chat 2.

### Teams

Suppose that User A and User B are on Team Blue, and User C and User D are on Team Red. Each user is in a unique instance of the app.

On User A's device:

```cs
localUserA.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.SendAndReceiveAll);
localUserA.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.None);
localUserA.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.None);
```

On User B's device:

```cs
localUserB.SetCommunicationRelationship(remoteUserA, GameChat2CommunicationRelationship.SendAndReceiveAll);
localUserB.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.None);
localUserB.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.None);
```

On User C's device:

```cs
localUserC.SetCommunicationRelationship(remoteUserA, GameChat2CommunicationRelationship.None);
localUserC.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.None);
localUserC.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.SendAndReceiveAll);
```

On User D's device:

```cs
localUserD.SetCommunicationRelationship(remoteUserA, GameChat2CommunicationRelationship.None);
localUserD.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.None);
localUserD.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.SendAndReceiveAll);
```

### Broadcast

Suppose that User A is the leader giving orders and Users B, C, and D can only listen. Each player is on a unique device.

On User A's device:

```cs
localUserA.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.SendAll);
localUserA.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.SendAll);
localUserA.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.SendAll);
```

On User B's device:

```cs
localUserB.SetCommunicationRelationship(remoteUserA, GameChat2CommunicationRelationship.ReceiveAll);
localUserB.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.None);
localUserB.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.None);
```

On User C's device:

```cs
localUserC.SetCommunicationRelationship(remoteUserA, GameChat2CommunicationRelationship.ReceiveAll);
localUserC.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.None);
localUserC.SetCommunicationRelationship(remoteUserD, GameChat2CommunicationRelationship.None);
```

On User D's device:

```cs
localUserD.SetCommunicationRelationship(remoteUserA, GameChat2CommunicationRelationship.ReceiveAll);
localUserD.SetCommunicationRelationship(remoteUserB, GameChat2CommunicationRelationship.None);
localUserD.SetCommunicationRelationship(remoteUserC, GameChat2CommunicationRelationship.None);
```
