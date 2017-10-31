---
title: Using XIM (C#)
author: KevinAsgari
description: Learn how to use Xbox Integrated Multiplayer (XIM) with C#.
ms.author: kevinasg
ms.date: 09/22/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---
# Using XIM (C#)

> [!div class="op_single_selector" title1="Language"]
> - [C++](using-xim.md)
> - [C#](using-xim-cs.md)

This is a brief walkthrough on using XIM's C# API. Game developers wanting to access XIM through C++ should see [Using XIM (C++)](using-xim.md).

1. [Prerequisites](#prereq)
2. [Initialization and startup](#init)
3. [Asynchronous operations and processing state changes](#async)
4. [Basic xim_player handling](#player)
5. [Enabling friends to join and inviting them](#invites)
6. [Sending and receiving messages](#send)
7. [Basic matchmaking and moving to another XIM network with others](#basicmatch)
8. [Leaving a XIM network and cleaning up](#leave)
9. [Working with chat](#chat)
10. [Configuring custom player and network properties](#properties)
11. [Matchmaking using per-player skill or role](#roles)
12. [Player teams and configuring chat targets](#teams)
13. [Automatic background filling of player slots ("backfill" matchmaking)](#backfill)

## Prerequisites <a name="prereq">

Before you get started coding with XIM, there are two prerequisites. First, you must have configured your app's AppXManifest with standard multiplayer networking capabilities and you must have configured its "network manifest" to declare the necessary traffic pattern templates used by XIM.

> AppXManifest capabilities and network manifests are described in more detail in their respective sections of the platform documentation; the typical XIM-specific XML to paste is provided at [XIM Project Configuration](xim-manifest.md).

Second, you'll need to have two pieces of application identity information available: the assigned Xbox Live title ID and service configuration ID provided as part of provisioning your application for access to the Xbox Live service. See your Microsoft representative for more information on acquiring these. These pieces of information will be used during initialization.

## Initialization and startup <a name="init">


You begin interacting with the library by initializing the XIM static class properties with your Xbox Live service configuration ID string and title ID number. The following example assumes the values already reside in 'myServiceConfigurationId' and 'myTitleId' variables respectively:

```cs
XboxIntegratedMultiplayer.ServiceConfigurationId = myServiceConfigurationId;
XboxIntegratedMultiplayer.TitleId = myTitleId;
```

Once initialized, the app should retrieve the Xbox User ID strings for all users currently on the local device that will participate, and pass them to a `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()` call. The following sample code assumes a single user has pressed a controller button expressing intent to play and the Xbox User ID string associated with the user has already been retrieved into a 'myXuid' variable:

```cs
XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds(new List<string>() { myXuid });
```

A call to `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()` immediately sets the Xbox User IDs associated with the local users that should be added to the XIM network. This list of Xbox User IDs will be used in all future network operations until the list changes through another call to `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()`.


In this case there is no XIM network at all yet, so you must begin moving to a XIM network to get that process started. The best practice if the user doesn't already have a specific XIM network in mind is to simply move to a new, empty one that you allow the user's friends to join, as a sort of "lobby" from which they can collaborate to select their next multiplayer activity (such as entering matchmaking together). An example starting to move just the local users previously added to such an empty XIM network with room for up to 8 total players would be:


```cs
XboxIntegratedMultiplayer.MovetoNewNetwork(8, XimPlayersToMove.BringOnlyLocalPlayers);
```
Now the asynchronous move operation will begin, and you can learn of its eventual results by regularly processing state changes.

## Asynchronous operations and processing state changes <a name="async">

The heart of XIM is the app’s regular, frequent calls to the `XboxIntegratedMultiplayer.GetStateChanges()` method. This method is how XIM is informed that the app is ready to handle updates to multiplayer state, and how XIM provides those updates by returning a `XimStateChangeCollection` object containing all queued updates. `XboxIntegratedMultiplayer.GetStateChanges()` is designed to operate quickly such that it can be called every graphics frame in your UI rendering loop. This provides a convenient place to retrieve all queued changes without worrying about the unpredictability of network timing or multi-threaded callback complexity. The XIM API is actually optimized for this single-threaded pattern and guarantees its state will remain unchanged while a `XimStateChangeCollection` object is being process and hasn’t been disposed.

The `XimStateChangeCollection` is a collection of `IXimStateChange` objects. Apps should iterate over the array, inspect the `IXimStateChange` for its more specific type, cast the `IXimStateChange` type to the corresponding more detailed type, and then handle that update as appropriate. Once finished with all the `IXimStateChange` objects currently available, that array should be passed back to XIM to release the resources by calling `XimStateChangeCollection.Dispose()`. The `using` statement is recommended so that the resources are guaranteed to be disposed after processing. For example:

```cs
using (var stateChanges = XboxIntegratedMultiplayer.GetStateChanges())
{
    foreach (var stateChange in stateChanges)
    {
        switch (stateChange.Type)
        {
            case XimStateChangeType.PlayerJoined:
                HandlePlayerJoined((XimPlayerJoinedStateChange)stateChange);
                break;

            case XimStateChangeType.PlayerLeft:
                HandlePlayerLeft((XimPlayerLeftStateChange)stateChange);
                break;

            ...
        }
    }
}
```

Now that you have your basic processing loop, you can handle the state changes associated with the initial `XboxIntegratedMultiplayer.MoveToNewNetwork()` operation. Every XIM network move operation will begin with a `XimMoveToNetworkStartingStateChange`. If the move fails for any reason, then your app will be provided a `XimNetworkExitedStateChange`, which is the common failure handling mechanism for any asynchronous fatal error that prevents you from moving to a XIM network or disconnects you from the current XIM network. Otherwise, the move will complete with a `XimMoveToNetworkSucceededStateChange` after all the state has been finalized and all the players have been successfully added to the XIM network.


## Basic IXimPlayer handling <a name="player">

Assuming the example of moving a single local user to a new XIM network succeeded, your app has also been provided a `XimPlayerJoinedStateChange` for a local `XimLocalPlayer` object. This object will remain valid until the corresponding `XimPlayerLeftStateChange` for it has been provided and returned via `XimStateChangeCollection.Dispose()`. Your app will always be provided a `XimPlayerLeftStateChange` for every `XimPlayerJoinedStateChange`. You can also retrieve a list of all `IXimPlayer` objects in the XIM network at any time by using `XboxIntegratedMultiplayer.GetPlayers()`.

The `IXimPlayer` object has many helpful properties and methods, such as `IXimPlayer.Gamertag()` for retrieving the current Xbox Live Gamertag string associated with the player for display purposes. If the `IXimPlayer` is local to the device, then IXimPlayer.Local will return true. A local `IXimPlayer` can be cast to a `XimLocalPlayer`, which has additional methods only available to local players.

Of course, the most important state for players is not the common information that XIM knows, but what your specific app wants to track, and since you likely have your own object for that, you'll want to link the `IXimPlayer` object to yours so that any time XIM reports a `IXimPlayer` you can quickly get to your state without having to perform a lookup by setting a custom player context object. The following example assumes an object containing your private state is in the variable 'myPlayerStateObject' and the newly added `IXimPlayer` object is in the variable 'newXimPlayer':

```cs
newXimPlayer.CustomPlayerContext = myPlayerStateObject;
```

This saves the specified object with the player object locally (it is never transferred over the network to remote devices where the memory would not be valid). You'll then be able to always get back to your object by retrieving the custom context and casting it back to your object like the following example:

```cs
myPlayerStateObject = (MyPlayerState)(newXimPlayer.CustomPlayerContext);
```

You can change this custom player context at any time.
With this basic player handling, you're now ready to enable remote users to join this XIM network through existing social relationships with the local users.

## Enabling friends to join and inviting them<a name="invites">

For privacy and security, all new XIM networks are automatically configured by default to not be joinable by any additional players, and it's up to the app to explicitly allow them once it is ready. The following example shows how to use `XboxIntegratedMultiplayer.SetAllowedPlayerJoins()` to begin allowing new local users to join as players, as well other users that have been invited or that are being "followed" (an Xbox Live social relationship):

```cs
XboxIntegratedMultiplayer.SetAllowedPlayerJoins(XimAllowedPlayerJoins.LocalInvitedOrFollowed);
```

This happens asynchronously. Once complete, a `XimAllowedPlayerJoinsChangedStateChange` is provided to notify you that its value has changed from its default of `XimAllowedPlayerJoins.None`. You can query the new value then or at any by using `XboxIntegratedMultiplayer.AllowedPlayerJoins`.

Now the local player may want to send out invitations to remote users to join this XIM network. This is trivially accomplished by calling `XimLocalPlayer.ShowInviteUI()` to launch the system invitation UI where the local user can select people and send invitations.

The system invitation UI will then display, and once the user has sent the invitations (or otherwise dismissed the UI), a `XimShowUICompletedStateChange` will be provided. Alternatively, your app can send the invitations directly using `XimLocalPlayer.InviteUsers()`. Either way, the remote users will receive an Xbox Live invitation message wherever they are signed in, and can choose to accept. This will launch your app on those devices if it isn't already running, and "protocol activate" it with the event arguments that can be used to move to this same XIM network. See the platform documentation for more information on activation itself. The following example shows how to take the event arguments and call `XboxIntegratedMultiplayer.ExtractProtocolActivationInformation()` to determine if they're applicable to XIM, assuming you've already retrieved the raw URI string from `Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs` to a variable 'uriString':

```cs
XimProtocolActivationInformation protocolActivationInformation;
XboxIntegratedMultiplayer.ExtractProtocolActivationInformation(uriString, out protocolActivationInformation);
if (protocolActivationInformation != null)
{
    // It is a XIM activation.
}
```

If it is a XIM activation, then you will want to ensure the local user identified in the 'LocalXboxUserId' property of the `XimProtocolActivationInformation` object is signed in and is among the users specified to `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()`. Then you can initiate moving to the specified XIM network with a call to `XboxIntegratedMultiplayer.MoveToNetworkUsingProtocolActivatedEventArgs()` using the same URI string. For example:

```cs
XboxIntegratedMultiplayer.MoveToNetworkUsingProtocolActivatedEventArgs(uriString);
```

Also note that "followed" remote users can navigate to the local user's player card in the system UI and initiate a join attempt themselves without an invitation (assuming you've allowed such player joins as shown above). These will protocol activate your app just like invites and don't need to be handled any differently.

Moving to a XIM network using protocol activation is identical to moving to a new XIM network like was done earlier. The only difference is that when the move succeeds, the moving device will have been provided both local and remote player `XimPlayerJoinedStateChange` objects representing the applicable players. And naturally, the device that was already in the XIM network won't be moving, but will see the new device's users be added as players with additional `XimPlayerJoinedStateChange` object.


At this point, voice and text chat communication is automatically enabled among the players on these different devices in this XIM network. You're now fully ready for multiplayer and any app-specific messages you want to send.

## Sending and receiving messages <a name="send">


XIM and its underlying components do all the tedious work of establishing secure communication channels over the Internet so you don't have to worry about connectivity problems or being able to reach some but not all players. If there are any fundamental peer-to-peer connectivity issues, moving to a XIM network will not succeed. Otherwise you can be sure that all instances of your app on all the devices will be informed of every `IXimPlayer`, and can send messages to any of them. The following example assumes a 'sendingPlayer' variable is a valid local player object, and sends a message structure that has been copied into the 'msgBuffer' to all players (local or remote) in the XIM network (by not passing a list of specific players), with guaranteed, sequential delivery:

```cs
SendingPlayer.SendDataToOtherPlayers(msgBuffer, null, XimSendType.GuaranteedAndSequential);
```

All recipients of the message will be provided a `XimPlayertoPlayerDataReceivedStateChange` that includes a copy of the data, as well as the `IXimPlayer` object that sent it and a list of the `IXimPlayer` objects that are locally receiving it.


Of course, guaranteed, sequential delivery is convenient, but it can also be an inefficient send type, since XIM needs to retransmit or delay it if packets are dropped/misordered by the Internet. Be sure to consider using the other send types for messages that your app can tolerate losing or having arrive out of order.

Since message data comes from a remote machine, the best practice is to clearly defined the data formats, such as packing multi-byte values in a particular byte order ("endianness"), and to validate the data before acting on it. XIM provides network-level security so you should not implement any additional encryption or signature scheme, but it is always wise to be robust for "defense-in-depth", to protect against accidental application bugs, or to handle different versions of your application protocol coexisting gracefully (during development, content updates, etc.).



The user's Internet connection is also a limited, ever-changing resource. Be sure to use efficient message data formats and avoid designs that send every UI frame. You can learn more about the current quality of the path between two players by inspecting the `XimPlayer.NetworkPathInformation` property. The property includes the estimated round trip latency and how many messages are still queued locally because the connection can't support transmitting more data at the moment. You should reduce the rate at which you're sending data if you see that the queues are backing up.

The returned structure includes the estimated round trip latency and how many messages are still queued locally because the connection can't support transmitting more data at the moment.

The `NetworkPathInformation.RoundTripLatency` field represents the latency of the underlying network and XIM's estimated latency without queuing. Effective latency increases as `XimNetworkPathInformation.SendQueueSizeInMessages` grows and XIM works through the queue.

Choose a reasonable point to start throttling calls to `SendDataToOtherPlayers` based on the game's usage and requirements. Every message in the send queue represents an increase in the effective network latency.

A value close to XIM’s max limit (currently 3500 messages) is far too high for most games and  likely represents several seconds of data waiting to be sent depending on the rate of calling `SendDataToOtherPlayers` and how big each data payload is. Instead, choose a number that takes into account the game's latency requirements along with how jittery the game's `SendDataToOtherPlayers` calling pattern is.


## Basic matchmaking and moving to another XIM network with others <a name="basicmatch">

You can further expand the experience for a group of friends by moving the players to a XIM network that also has strangers-- opponents from around the world who are brought together using the Xbox Live matchmaking service based on similar interests. The most basic form is calling `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` on one of the devices with a populated `MatchmakingConfiguration` object, taking players from the current XIM network along with it. The following example initiates a move using matchmaking configured to find a total of 8 players for a no-teams free-for-all (although if 8 aren't found, 2-7 players are also acceptable), using an app-specific game mode constant uint defined by the value MYGAMEMODE_DEATHMATCH that will only match with other players specifying that same value, and bringing all socially-joined players from the current XIM network:

```cs
XimMatchmakingConfiguration matchmakingConfiguration = new XimMatchmakingConfiguration()
{
    TeamMatchmakingMode = XimTeamMatchmakingMode.NoTeams8PlayersMinimum2;
    CustomGameMode = MYGAMEMODE_DEATHMATCH;
};

XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking(matchmakingConfiguration, XimPlayersToMove.BringExistingSocialPlayers);
```

Like earlier moves, this will provide an initial `MoveToNetworkStartingStateChange` on all devices, and a `MoveToNetworkSucceededStateChange` once the move completes successfully. Since this is a move from one XIM network to another, one difference is that there are already existing `IXimPlayer` objects added for local and remote users, and these will remain for all players that are moving together to the new XIM network. Chat and data communication among them will continue to work uninterrupted while matchmaking is in progress (which can be a lengthy process, depending on the number of potential players in the matchmaking pool that have called `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` as well). A `XimMatchmakingProgressUpdatedStateChange` will be provided periodically throughout the operation to keep you and your users informed of the current status. When the match has been found, the additional players are added to the XIM network with the typical `PlayerJoinedStateChange` and the move completes.

Once you've finished the multiplayer experience with this set of "matchmade" players, you can repeat the process to move to a different XIM network with another round of matchmaking. You'll see each player that joined via the prior `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` operation provide a `PlayerLeftStateChange` to indicate that their `IXimPlayer` objects are no longer in the same XIM network, and only the players that had joined via social means, `XboxIntegratedMultiplayer.MoveToNetworkUsingProtocolActivatedEventArgs()` or `XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableXboxUserId`, will remain while the new matchmaking takes place (assuming you specify `XimPlayersToMove.BringExistingSocialPlayers` again; specifying `XimPlayersToMove.BringOnlyLocalPlayers` will disconnect from even those remote players, and just the local players will remain). A different set of strangers will be added when the second move operation completes.

Alternatively, you can move to a completely new XIM network with just the non-matchmade players (or just local players) before deciding the next matchmaking configuration/multiplayer activity. The following example demonstrates having a device call `XboxIntegratedMultiplayer.MoveToNewNetwork()` for a XIM network with a maximum of 8 players again, but this time taking the existing socially-joined players as well:

```cs
XboxIntegratedMultiplayer.MovetoNewNetwork(8, XimPlayersToMove.BringExistingSocialPlayers);
```

A `MoveToNetworkStartingStateChange` and `MoveToNetworkSucceededStateChange` will be provided to all participating devices, along with a `PlayerLeftStateChange` for the matchmade players staying behind (those devices similarly see a `PlayerLeftStateChange` for each player that is moving).

You can continue moving from XIM network to XIM network using matchmaking (or not) in this manner as many times as desired.

For performance, the Xbox Live service will not try to match groups of players on devices that are unlikely to be able to establish any direct peer-to-peer connections. If you're developing in a network environment that's not properly configured to support standard Xbox Live multiplayer, the move to new network using matchmaking operation might continue indefinitely without matching even when you're certain you have sufficient players meeting the matchmaking criteria who are all moving and all using devices in the same local environment. Be sure to run the multiplayer connectivity test in the network settings area/Xbox application and follow its recommendations if it reports trouble, particularly regarding a "Strict NAT". However, if your network administrator is unable to make the necessary environment changes, you can unblock your matchmaking testing on Xbox One development kits by configuring XIM to allow matching "Strict NAT" devices without at least one "Open NAT" device. This is done by placing a file called "xim_disable_matchmaking_nat_rule" (contents don't matter) at the root of the "title scratch" drive on all Xbox One consoles. One example way to do that is by executing the following from an XDK command prompt before launching your app, replacing the placeholder "{console_name_or_ip_address}" for each console as appropriate:

```bat

echo.>%TEMP%\emptyfile.txt
copy %TEMP%\emptyfile.txt \\{console_name_or_ip_address}\TitleScratch\xim_disable_matchmaking_nat_rule
del %TEMP%\emptyfile.txt

```

This development workaround is currently only available for Xbox One exclusive resource applications and not for universal Windows applications. Also note that consoles that are using this setting will never match with devices that don't have the file present, regardless of network environment, so be sure to add or remove the file everywhere.

## Leaving a XIM network and cleaning up <a name="leave">
When the local users are done participating in a XIM network, often they will simply move back to a new XIM network that allows local users, invites, and "followed" users to join it so they can continue coordinating with their friends to find the next activity. But if the user is completely done with all multiplayer experiences, then your app may want to begin leaving the XIM network altogether and return to the state as if only `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds` had been called. This is done using the `XboxIntegratedMultiplayer.LeaveNetwork()` method:

```cs
 XboxIntegratedMultiplayer.LeaveNetwork();
```

This method begins the process of asynchronously disconnecting from the other participants gracefully. This will cause the remote devices to be provided a `PlayerLeftStateChange` for the local player(s), and the local device will be provided a `PlayerLeftStateChange` for each player, local or remote. When all disconnect operations have finished, a final `NetworkExitedStateChange` will be provided.

Invoking `XboxIntegratedMultiplayer.LeaveNetwork()` and waiting for the `NetworkExitedStateChange` in order to exit a XIM network gracefully is always highly recommended when a `NetworkExitedStateChange` has not already been provided.

## Working with chat <a name="chat">
Voice and text chat communication are automatically enabled among players in a XIM network. XIM handles interacting with all voice headset and microphone hardware for you. Your app doesn't need to do much for chat, but it does have one requirement regarding text chat: supporting input and display. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, players may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because players may configure the system to use speech-to-text. These preferences can be detected on local players by accessing the `XimLocalPlayer.ChatTextToSpeechConversionPreferenceEnabled` and `XimLocalPlayer.ChatSpeechToTextConversionPreferenceEnabled` fields respectively, and you may wish to conditionally enable text mechanisms. But consider making text input and display options that are always available.


> `Windows::Xbox::UI::Accessability` is an Xbox One class specifically designed to provide simple rendering of in-game text chat with a focus on speech-to-text assistive technologies.

Once you have text input provided by a real or virtual keyboard, pass the string to the `XimLocalPlayer.SendChatText()` method. The following code shows sending an example hard-coded string from a local `IXIMPlayer` object pointed to by the variable 'localPlayer':

```cs
localPlayer.SendChatText(text);
```

This chat text is delivered to all players in the XIM network that can receive chat communication from the originating local player. It might be synthesized to speech audio and it might be provided as a `XimChatTextReceivedStateChange`. Your app should make a copy of any text string received and display it along with some identification of the originating player for an appropriate amount of time (or in a scrollable window).

There are also some best practices regarding chat. It's recommended that anywhere players are shown, particularly in a list of gamertags such as a scoreboard, that you also display muted/speaking icons as feedback for the user. This is done by accessing `IXimPlayer.ChatIndicator` to retrieve a `XboxIntegratedMultiplayer.XimPlayerChatIndicator` representing the current, instantaneous status of chat for that player.

The value reported by `IXimPlayer.ChatIndicator` is expected to change frequently as players start and stop talking, for example. It is designed to support apps polling it every UI frame as a result.

Another best practice is to support muting players. XIM automatically handles system muting initiated by users through player cards, but apps should support game-specific transient muting that can be performed within the game UI via the `IXimPlayer.ChatMuted` property. The following example begins muting a remote `IXIMPlayer` object pointed to by the variable 'remotePlayer' so that no voice chat is heard and no text chat is received from it:

```cs
remotePlayer.ChatMuted = true;
```

The muting takes effect immediately and there is no XIM state change associated with it.
Mutes remain in effect for as long as the `IXIMPlayer` exists, including when moving to a new XIM network with the player. It is not persisted if the player leaves and the same user rejoins (as a new `IXIMPlayer` instance).

Players typically start in the unmuted state. If your app wants to start a player in the muted state for gameplay reasons, it can set `IXIMPlayer.ChatMuted` on the `IXIMPlayer` object before finishing processing the associated `PlayerJoinedStateChange`, and XIM will guarantee there will be no period of time where voice audio from the player can be heard.

An automatic mute check based on player reputation occurs when a remote player joins the XIM network. If the player has a bad reputation flag, the player is automatically muted. Muting only affects local state and therefore persists if a player moves across networks. The automatic reputation-based mute check is performed once and not re-evaluated again for as long as the `IXIMPlayer` remains valid.

## Configuring custom player and network properties <a name="properties">
Most app data exchanges happen with the `XimLocalPlayer.SendDataToOtherPlayers()` method since it allows the most control over who receives it and when, how it should deal with packet loss, and so on. However there are times where it would be nice for players to share basic, rarely changing state about themselves with others with minimal fuss. For example, each player might have a fixed string representing the character model selected before entering multiplayer that all players use to render their in-game representation. XIM provides a "custom player properties" convenience feature for app-defined name and value null terminated string pairs that can be applied to the local player and automatically propagated to all devices whenever they are changed. Their current values are also automatically provided to new participating devices when they join a XIM network and see the player added. These can be configured by calling `XimLocalPlayer.SetPlayerCustomProperty()` with the name and value strings, like in the following example that sets a property named "model" to have the value "brute" on a local `IXIMPlayer` object pointed to by the variable 'localPlayer':

```cs
localPlayer.SetPlayerCustomProperty("model","brute");
```

Changes to player properties will cause a `XimPlayerCustomPropertiesChangedStateChange` to be provided to all devices, alerting them to the names of properties that have changed. The value for a given name can be retrieved on any player, local or remote, with `IXIMPlayer.GetPlayerCustomProperty()`. The following example retrieves the value for a property named "model" from a `IXIMPlayer` pointed to by the variable 'ximPlayer':

```cs
string propertyValue = localPlayer.GetPlayerCustomProperty("model");
```

Setting a new value for a given property name will replace any existing value, and a null value string pointer is treated the same as an empty value string, which is the same as the property not having been specified yet. Otherwise the names and values aren't interpreted by XIM; it's up to the app to validate the string contents as needed.

This convenience feature is also available for the XIM network as a whole via "custom network properties". These work identically to custom player properties, except they're set on the XIM singleton object with `XboxIntegratedMultiplayer.SetNetworkCustomProperty()`. The following example sets a "map" property to have the value "stronghold":

```cs
XboxIntegratedMultiplayer.SetNetworkCustomProperty("map", "stronghold");
```

Changes to network properties will cause a `NetworkCustomPropertiesChanged` to be provided to all devices, alerting them to the names of properties that have changed. The value for a given name can be retrieved with `XboxIntegratedMultiplayer.GetNetworkCustomProperty()`, like in the following example that retrieves the value for a property named "map":

```
string property = XboxIntegratedMultiplayer.GetNetworkCustomProperty("map")
```
Just like custom player properties, setting a value for a given custom network property name will replace the existing value, and null, unset, or cleared values are always treated the same: as non-null empty strings.

Custom player properties are always reset when moving from one XIM network to another, and newly created XIM networks always start with no properties set. However, new players joining an existing XIM network will see the custom properties set on existing players and on the XIM network itself.

Custom player and network properties are intended as a convenience for state that doesn't change frequently. They have more internal synchronization overhead than the `XimLocalPlayer.SendDataToOtherPlayers()` method, so you should still use direct sends instead for state like player positions that are rapidly replaced.


## Matchmaking using per-player skill or role <a name="roles">

Matching players by common interest in a particular app-specified game mode is a good base strategy. As the pool of available players grows, you should consider also matching players based on their personal skill or experience with your game so that veteran players can enjoy the challenge of healthy competition with other veterans, while newer players can grow by competing against others with similar abilities.


To do this, start by providing the skill level for all local players in their per-player matchmaking configuration structure specified in calls to `XimLocalPlayer.SetMatchmakingConfiguration()` prior to starting to move to a XIM network using matchmaking. Skill level is an app-specific concept and the number is not interpreted by XIM, except that matchmaking will first try to find players with the same skill value, and then periodically widen its search in increments of +/- 10 to try to find other players declaring skill values within a range around that skill. The following example assumes that the local `XimLocalPlayer` object, whose variable is 'localPlayer', has an associated app-specific integer skill value retrieved from local or Xbox Live storage into a variable called 'playerSkillValue':

```cs
var config = new XimPlayerMatchmakingConfiguration();
config.Skill = playerSkillValue;
localPlayer.SetMatchmakingConfiguration(config);
```

When this completes, all participants will be provided a `PlayerMatchmakingConfigurationStateChange` indicating this `IXIMPlayer` has changed its per-player matchmaking configuration. The new value can be retrieved by calling `IXIMPlayer.MatchmakingConfiguration`.

When all players have non-null matchmaking configuration applied, you can move to a XIM network using matchmaking with a value of true for the `RequirePlayerMatchmakingConfiguration` field of the `MatchmakingConfiguration` structure specified to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`. The following example populates a matchmaking configuration that will find a total of 2-8 players for a no-teams free-for-all, using an app-specific game mode constant Uint64 defined by the value MYGAMEMODE_DEATHMATCH that will only match with other players specifying that same value, and that requires per-player matchmaking configuration:

```cs
XimMatchmakingConfiguration matchmakingConfiguration = new XimMatchmakingConfiguration()
{
    TeamMatchmakingMode = XimTeamMatchmakingMode.NoTeams8PlayersMinimum2;
    CustomGameMode = MYGAMEMODE_DEATHMATCH;
    RequirePlayerMatchmakingConfiguration = true;
};
```

When this structure is provided to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`, the move operation will start normally as long as players moving have called `XimLocalPlayer.SetMatchmakingConfiguration()` with a non-null `XimPlayerMatchmakingConfiguration` object. If any player hasn't, then the matchmaking process will be paused and all participants will be provided a `XimMatchmakingProgressUpdatedStateChange` with a `WaitingForPlayerMatchmakingConfiguration` value. This includes players that subsequently join the XIM network through a previously sent invitation or through other social means (e.g., a call to `XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableXboxUserId()`) before matchmaking has completed. Once all players have supplied their `XimPlayerMatchmakingConfiguration` objects, matchmaking will resume.

Another method of using per-player matchmaking configuration to improve users' matchmaking experience is through the use of required player roles. This is best suited to games that provide selectable character types that encourage different cooperative play styles; that is, types that don't simply alter in-game graphical representation, but control complementary, impactful attributes such as defensive "healers" vs. close-in "melee" offense vs. distant "range" attack support. Users' personalities mean they may prefer to play as a particular specialization. But if your game is designed such that it's functionally not possible to complete objectives without at least one person fulfilling each role, sometimes it's better to match such players together first than to match any players together then require them to negotiate play styles among themselves once gathered. You can do this by first defining a unique bit flag representing each role to be specified in a given player's `XimPlayerMatchmakingConfiguration` structure. The following example sets an app-specific MYROLEBITFLAG_HEALER int role value for the local `XimLocalPlayer` object, whose variable is 'localPlayer':

```cs
var config = new XimPlayerMatchmakingConfiguration();
config.Roles = MYROLEBITFLAG_HEALER;
localPlayer.SetMatchmakingConfiguration(config);
```

All participants will be provided a `PlayerMatchmakingConfigurationStateChange` for this player as described for skill above. The global `XimMatchmakingConfiguration` structure specified to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` should then have all the required roles flags combined using bitwise-OR, and a value of true for the `RequirePlayerMatchmakingConfiguration` field.

Skill and role can be used together. If only one is desired, specify a value of 0 for the other. This is because all players declaring they have a `PlayerMatchmakingConfiguration` skill value of 0 will always match each other, and if no bits are non-zero in the `Player` required_roles field, then no role bits are needed in order to match.

Once the `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` or any other XIM network move operation has completed, all players' `XimPlayerMatchmakingConfiguration` objects will automatically be cleared to  null (with an accompanying `XimPlayerMatchmakingConfigurationChangedStateChange` notification). If you plan to move to another XIM network using matchmaking that requires per-player configuration, you'll need to call `XimLocalPlayer.SetMatchmakingConfiguration()` again with a object containing the most up-to-date information.

## Player teams and configuring chat targets <a name="teams">

Multiplayer gaming often involves players organized onto opposing teams. XIM makes it easy to assign teams when matchmaking by using a `XimTeamMatchmakingMode` value requesting two or more teams in the specified configuration. The following example initiates a move using matchmaking configured to find a total of 8 players to place on two teams of 4 (although if 4 aren't found, 1-3 players are also acceptable), using an app-specific game mode constant uint64_t defined by the value MYGAMEMODE_CAPTURETHEFLAG that will only match with other players specifying that same value, and bringing all socially-joined players from the current XIM network:

```cs
XimMatchmakingConfiguration matchmakingConfiguration = new XimMatchmakingConfiguration()
{
     TeamMatchmakingMode = XimTeamMatchmakingMode.TwoTeams4v4Minimum1PerTeam;
     CustomGameMode = MYGAMEMODE_CAPTURETHEFLAG;
};

XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking(matchmakingConfiguration, XimPlayersToMove.BringExistingSocialPlayers);
```
When such a XIM network move operation completes, the players will be assigned a team index value 1 through {n} corresponding to the {n} teams requested. A player's team index value is retrieved via `IXIMPlayer.TeamIndex`. The following example retrieves the team index for a XIM player object stored in the 'ximPlayer' variable:

```cs
byte playerIndex = ximPlayer.TeamIndex;
```

For the preferred user experience (not to mention reduced opportunity for negative player behavior), the Xbox Live matchmaking service will never split players who are moving to a XIM network together onto different teams.

The team index value assigned initially by matchmaking is only a recommendation and the app can change it for local players at any time using `XimLocalPlayer.SetTeamIndex()`. This can also be called in XIM networks that don't use matchmaking at all.

All devices are informed that the player has a new team index value in effect when they're provided a `XimPlayerTeamIndexChangedStateChange` for that player.

When using a `XimTeamMatchmakingMode` with two or more teams, players will never be assigned a team index value of zero by the call to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`. This is in contrast to players that are added to the XIM network with any other configuration or type of move operation (such as through a protocol activation resulting from accepting an invitation), who will always have a zero team index. It may be helpful to treat team index 0 as a special "unassigned" team.

The true meaning of any particular team index value is up to the app. XIM doesn't interpret them except for equality comparisons with respect to chat target configuration. If the chat target configuration reported by `XboxIntegratedMultiplayer.ChatTargets` is currently `XimChatTargets.SameTeamIndexOnly`, then any given player will only exchange chat communication with another if the two have the same value reported by `IXIMPlayer.TeamIndex` (and privacy/ policy also permit it).

To be conservative and support competitive scenarios, newly created XIM networks are automatically configured to default to `XimChatTargets.SameTeamIndexOnly`. However, chatting with vanquished opponents on the other team may be desirable, for example, in a post-game "lobby". You can instruct XIM to allow everyone to talk to everyone else where privacy and policy permit) by calling `XboxIntegratedMultiplayer.SetChatTargets()`. The following sample begins configuring all participants in the XIM network to use a `XimChatTargets.AllPlayers` value:

```cs
XboxIntegratedMultiplayer.SetChatTargets(XimChatTargets.AllPlayers)
```

All participants are informed that a new target setting is in effect when they're provided a `XimChatTargetsChangedStateChange`.

As noted earlier, most XIM network move types will initially assign all players the team index value of zero. This means a configuration of `XimChatTargets.SameTeamIndexOnly` is likely indistinguishable from `XimChatTargets.AllPlayers` by default. However, players that move to a XIM network using matchmaking will have differing team index values if the matchmaking configuration's `TeamMatchmakingMode` value declared two or more teams. You can also call `XimLocalPlayer.SetTeamIndex()` at any time as shown above. If your app is using non-zero team index values through either of these methods, don't forget to manage the current chat targets setting appropriately.

Matchmaking evaluates required per-player roles independently from teams. Therefore it's not recommended to use both teams and required roles as simultaneous matchmaking configuration criteria because the teams will be balanced by player count, not by fulfilled player roles.

## Automatic background filling of player slots ("backfill" matchmaking) <a name="backfill">

Disparate groups of players calling `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` at the same time gives the Xbox Live matchmaking service the greatest flexibility to organize them into new, optimal XIM networks quickly. However, some gameplay scenarios would like to keep a particular XIM network intact, and only matchmake additional players just to fill vacant player slots. XIM supports configuring matchmaking to operate in an automatic background filling mode, or "backfilling", by using the `XboxIntegratedMultiplayer.SetBackfillMatchMakingConfiguration()` method. The following example configures backfill matchmaking to try to find a total of 8 players for a no-teams free-for-all (although if 8 aren't found, 2-7 players are also acceptable), using an app-specific game mode constant uint64_t defined by the value MYGAMEMODE_DEATHMATCH that will only match with other players specifying that same value:

```cs
XimMatchmakingConfiguration matchmakingConfiguration = new XimMatchmakingConfiguration()
{
     TeamMatchmakingMode = XimTeamMatchmakingMode.NoTeams8PlayersMinimum2;
     CustomGameMode = MYGAMEMODE_DEATHMATCH;
};

XboxIntegratedMultiplayer.SetBackfillMatchMakingConfiguration(matchmakingConfiguration);
```

This makes the existing XIM network available to devices calling `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` in the normal manner. Those devices see no behavior change. The participants in the backfilling XIM network will not move, but will be provided a `XimBackfillMatchmakingConfigurationChangedStateChange` signifying backfill turning on, as well as multiple `XimMatchmakingProgressUpdatedStateChange` notifications when applicable. Any matchmade player will be added to the XIM network using the normal `XimPlayerJoinedStateChange`.

Backfill matchmaking remains in progress indefinitely, although it won't try to add players if the XIM network already has the maximum number of players specified by the `TeamMatchmakingMode` value. Backfilling can be disabled by calling `XboxIntegratedMultiplayer.SetBackfillMatchMakingConfiguration()` again with null:

```cs
XboxIntegratedMultiplayer.SetBackfillMatchMakingConfiguration(null);
```

A corresponding `XimBackfillMatchmakingConfigurationChangedStateChange` will be provided to all devices, and once this asynchronous process has completed, a final `XimMatchmakingProgressUpdatedStateChange` will be provided with `MatchmakingStatus.None` to signify that no further matchmade players will be added to the XIM network.

When enabling backfill matchmaking with a `XimTeamMatchmakingMode` value that declares two or more teams, all existing players must have a valid team index that is between 1 and the number of teams. This includes players who have called `XimLocalPlayer.SetTeamIndex()` to specify a custom value or who have joined using an invitation or through other social means (e.g., a call to `XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableXboxUserId()`) and have been added with a default team index value of 0. If any player doesn't have a valid team index, then the matchmaking process will be paused and all participants will be provided a `XimMatchmakingProgressUpdatedStateChange` with a `MatchmakingStatus.WaitingForPlayerTeamIndex` value. Once all players have supplied or corrected their team index values with `XimLocalPlayer.SetTeamIndex()`, backfill matchmaking will resume.

Similarly, when enabling backfill matchmaking with a `MatchmakingConfiguration` structure with the `RequirePlayerMatchmakingConfiguration` field set to true for roles or skill, then all players must have specified a non-null per-player matchmaking configuration. If any player hasn't, then the matchmaking process will be paused and all participants will be provided a `XimMatchmakingProgressUpdatedStateChange` with a `XimMatchMakingStatus.WaitingForPlayerMatchmakingConfiguration` value. Once all players have supplied their `XimPlayerMatchmakingConfiguration` objects, backfill matchmaking will resume.
