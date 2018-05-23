---
title: Using XIM (C#)
author: KevinAsgari
description: Learn how to use Xbox Integrated Multiplayer (XIM) with C#.
ms.author: kevinasg
ms.date: 04/24/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox integrated multiplayer
ms.localizationpriority: low
---
# Using XIM (C#)

> [!div class="op_single_selector" title1="Language"]
> - [C++](using-xim.md)
> - [C#](using-xim-cs.md)

This is a brief walkthrough on using XIM's C# API. Game developers wanting to access XIM through C++ should see [Using XIM (C++)](using-xim.md).
- [Using XIM (C#)](#using-xim-c)
    - [Prerequisites](#prerequisites)
    - [Initialization and startup](#initialization-and-startup)
    - [Asynchronous operations and processing state changes](#asynchronous-operations-and-processing-state-changes)
    - [Basic IXimPlayer handling](#basic-iximplayer-handling)
    - [Enabling friends to join and inviting them](#enabling-friends-to-join-and-inviting-them)
    - [Basic matchmaking and moving to another XIM network with others](#basic-matchmaking-and-moving-to-another-xim-network-with-others)
    - [Disabling matchmaking NAT rule for debugging purposes](#disabling-matchmaking-nat-rule-for-debugging-purposes)
    - [Leaving a XIM network and cleaning up](#leaving-a-xim-network-and-cleaning-up)
    - [Sending and receiving messages](#sending-and-receiving-messages)
    - [Assessing data pathway quality](#assessing-data-pathway-quality)
    - [Sharing data using player custom properties](#sharing-data-using-player-custom-properties)
    - [Sharing data using network custom properties](#sharing-data-using-network-custom-properties)
    - [Matchmaking using per-player skill](#matchmaking-using-per-player-skill)
    - [Matchmaking using per-player role](#matchmaking-using-per-player-role)
    - [How XIM works with player teams](#how-xim-works-with-player-teams)
    - [Working with chat](#working-with-chat)
    - [Muting players](#muting-players)
    - [Configuring chat targets using player teams](#configuring-chat-targets-using-player-teams)
    - [Automatic background filling of player slots ("backfill" matchmaking)](#automatic-background-filling-of-player-slots-backfill-matchmaking)
    - [Querying joinable networks](#querying-joinable-networks)

## Prerequisites

Before you get started coding with XIM, there are two prerequisites.

1. You must have configured your app's AppXManifest with standard multiplayer networking capabilities and you must have configured the network manifest portion to declare the necessary traffic pattern templates used by XIM.

    AppXManifest capabilities and network manifests are described in more detail in their respective sections of the platform documentation; the typical XIM-specific XML to paste is provided at [XIM Project Configuration](xim-manifest.md).

1. You'll need to have two pieces of application identity information available:

    * The assigned Xbox Live title ID.
    * The service configuration ID provided as part of provisioning your application for access to the Xbox Live service.

    See your Microsoft representative for more information on acquiring these. These pieces of information will be used during initialization.

## Initialization and startup

You begin interacting with the library by initializing the XIM static class properties with your Xbox Live service configuration ID string and title ID number. If you're also using the Xbox Services API, you may find it convenient to retrieve these from `Microsoft.Xbox.Services.XboxLiveAppConfiguration`. The following example assumes the values already reside in 'myServiceConfigurationId' and 'myTitleId' variables respectively:

```cs
XboxIntegratedMultiplayer.ServiceConfigurationId = myServiceConfigurationId;
XboxIntegratedMultiplayer.TitleId = myTitleId;
```

Once initialized, the app should retrieve the Xbox User ID strings for all users currently on the local device that will participate, and pass them to a `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()` call. The following sample code assumes a single user has pressed a controller button expressing intent to play and the Xbox User ID string associated with the user has already been retrieved into a 'myXuid' variable:

```cs
XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds(new List<string>() { myXuid });
```

A call to `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()` immediately sets the Xbox User IDs associated with the local users that should be added to the XIM network. This list of Xbox User IDs will be used in all future network operations until the list changes through another call to `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()`.

In the case where no XIM network exists at all yet, the first step is to move to a XIM network in order to get that process started. If the user doesn't already have a specific XIM network in mind, the best practice is to simply move to a new, empty network. This allows the user's friends to join the network as a sort of "lobby" from which they can collaborate together to select their next multiplayer activity (such as entering matchmaking).

The following is an example of how to move just the local users previously added with  `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()` to an empty XIM network with room for up to 8 total players:

```cs
XboxIntegratedMultiplayer.MovetoNewNetwork(8, XimPlayersToMove.BringOnlyLocalPlayers);
```

The call to `XboxIntegratedMultiplayer.MovetoNewNetwork()` initiates an asynchronous move operation that concludes with a state change event that developers should regularly process for.

## Asynchronous operations and processing state changes

The heart of XIM is the app's regular, frequent calls to the `XboxIntegratedMultiplayer.GetStateChanges()` method. This method is how XIM is informed that the app is ready to handle updates to multiplayer state, and how XIM provides those updates by returning a `XimStateChangeCollection` object containing all queued updates. `XboxIntegratedMultiplayer.GetStateChanges()` is designed to operate quickly such that it can be called every graphics frame in your UI rendering loop. This provides a convenient place to retrieve all queued changes without worrying about the unpredictability of network timings or multi-threaded callback complexity. The XIM API is actually optimized for this single-threaded pattern. It guarantees its state will remain unchanged while a `XimStateChangeCollection` object is being process and hasn't been disposed.

The `XimStateChangeCollection` is a collection of `IXimStateChange` objects.
Apps should:

1. Iterate over the array.
1. Inspect the `IXimStateChange` for its more specific type.
1. Cast the `IXimStateChange` type to the corresponding more detailed type.
1. Handle that update as appropriate.

Once finished with all the `IXimStateChange` objects currently available, that array should be passed back to XIM to release the resources by calling `XimStateChangeCollection.Dispose()`. The `using` statement is recommended so that the resources are guaranteed to be disposed after processing. For example:

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

Now that you have your basic processing loop, you can handle the state changes associated with the initial `XboxIntegratedMultiplayer.MoveToNewNetwork()` operation. Every XIM network move operation will begin with a `XimMoveToNetworkStartingStateChange`. If the move fails for any reason, then your app will be provided a `XimNetworkExitedStateChange`, which is the common failure handling mechanism for any asynchronous error that prevents you from moving to a XIM network or disconnects you from the current XIM network. Otherwise, the move will complete with a `XimMoveToNetworkSucceededStateChange` after all the state has been finalized and all the players have been successfully added to the XIM network.

When a user does not have the platform privilege for playing with others in a multiplayer session, XIM will send to a device attempting to create or join a XIM network a `XimNetworkExitedStateChange` with the reason `UserMultiplayerRestricted`. This happens when any of the local users set with `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()` have an Xbox Live multiplayer restriction. Please handle appropriately by informing the user of the issue.

## Basic IXimPlayer handling

Assuming the example of moving a single local user to a new XIM network succeeded, your app will be provided a `XimPlayerJoinedStateChange` for a local `XimLocalPlayer` object. This object will remain valid until the corresponding `XimPlayerLeftStateChange` for it has been provided and returned via `XimStateChangeCollection.Dispose()`. Your app will always be provided a `XimPlayerLeftStateChange` for every `XimPlayerJoinedStateChange`.

You can also retrieve a list of all `IXimPlayer` objects in the XIM network at any time by using `XboxIntegratedMultiplayer.GetPlayers()`.

The `IXimPlayer` object has many helpful properties and methods, such as `IXimPlayer.Gamertag()` for retrieving the current Xbox Live Gamertag string associated with the player for display purposes. If the `IXimPlayer` is local to the device, then IXimPlayer.Local will return true. A local `IXimPlayer` can be cast to a `XimLocalPlayer`, which has additional methods only available to local players.

Of course, the most important state for players is not the common information that XIM knows, but what your specific app wants to track. Since you likely have your own construct for that tracking information, you'll want to link the `IXimPlayer` object to your own player construct object so that any time XIM reports a `IXimPlayer`, you can quickly retrieve your state without having to perform a lookup by setting a custom player context object. The following example assumes an object containing your private state is in the variable 'myPlayerStateObject' and the newly added `IXimPlayer` object is in the variable 'newXimPlayer':

```cs
newXimPlayer.CustomPlayerContext = myPlayerStateObject;
```

This saves the specified object with the player object locally (it is never transferred over the network to remote devices where the memory would not be valid). You'll then be able to always get back to your object by retrieving the custom context and casting it back to your object like the following example:

```cs
myPlayerStateObject = (MyPlayerState)(newXimPlayer.CustomPlayerContext);
```

You can change this custom player context at any time.

## Enabling friends to join and inviting them

For privacy and security, all new XIM networks are automatically configured by default to be only joinable by local players, and it's up to the app to explicitly allow others once it is ready. The following example shows how to use `XboxIntegratedMultiplayer.NetworkConfiguration` to retrieve the current network configuration and update joinability using `XboxIntegratedMultiplayer.SetNetworkConfiguration()` to begin allowing new local users to join as players, as well as other users that have been invited or that are being "followed" (an Xbox Live social relationship) by players already in the XIM network:

```cs
XimNetworkConfiguration currentConfiguration = new XimNetworkConfiguration(XboxIntegratedMultiplayer.NetworkConfiguration);
currentConfiguration.AllowedPlayerJoins = XimAllowedPlayerJoins.Local | XimAllowedPlayerJoins.Invited | XimAllowedPlayerJoins.Followed;
XboxIntegratedMultiplayer.SetNetworkConfiguration(currentConfiguration);
```

`XboxIntegratedMultiplayer.SetNetworkConfiguration()` executes asynchronously. Once the previous code sample call completes, a `XimNetworkConfigurationChangedStateChange` is provided to notify the app that the joinability value has changed from its default of `XimAllowedPlayerJoins.None`. You can then query the new value by checking the value of `XboxIntegratedMultiplayer.NetworkConfiguration.AllowedPlayerJoins`.

`XboxIntegratedMultiplayer.NetworkConfiguration.AllowedPlayerJoins` can be checked while the device is in a XIM network to determine the joinability of the network.

Should one of the local players want to send out invitations to remote users to join this XIM network, the app can call `XimLocalPlayer.ShowInviteUI()` to launch the system invitation UI. Here, the local user can select people they wish to invite and send out invitations.

```cs
XimLocalPlayer.ShowInviteUI();
```

After the above statement executes, the system invitation UI will display. Once the user has sent the invitations (or otherwise dismissed the UI), a `xim_show_invite_ui_completed_state_change` will be provided. Alternatively, your app can send the invitations directly using `XimLocalPlayer.InviteUsers()` without bringing up the system invitation UI.

Either way, the remote users will receive an Xbox Live invitation messages wherever they are signed in, and can choose to accept. This will launch your app on those devices if it isn't already running, and "protocol activate" it with the event arguments that can be used to move to this same XIM network.

See platform documentation for more information on protocol activation itself.

The following example shows how a call `XboxIntegratedMultiplayer.ExtractProtocolActivationInformation()` determines if the event arguments are applicable to XIM. This assumes you've retrieved the raw URI string from `Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs` to a variable 'uriString':

```cs
XimProtocolActivationInformation protocolActivationInformation;
XboxIntegratedMultiplayer.ExtractProtocolActivationInformation(uriString, out protocolActivationInformation);
if (protocolActivationInformation != null)
{
    // It is a XIM activation.
}
```

If it is a XIM activation, then you will want to ensure the local user identified in the 'LocalXboxUserId' property of the `XimProtocolActivationInformation` object is signed in and is among the users specified to `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds()`. You can then initiate moving to the specified XIM network with a call to `XboxIntegratedMultiplayer.MoveToNetworkUsingProtocolActivatedEventArgs()` using the same URI string. For example:

```cs
XboxIntegratedMultiplayer.MoveToNetworkUsingProtocolActivatedEventArgs(uriString);
```

Also note that "followed" remote users can navigate to the local user's player card in the system UI and initiate a join attempt themselves without an invitation (assuming you've allowed such player joins as shown above). This action will also protocol activate your app just like invites do and are handled in the same way.

Moving to a XIM network using protocol activation is identical to moving to a new XIM network as shown in [Initialization and startup](#initialization-and-startup). The only difference is that when the move succeeds, the moving device will be provided both local and remote player `XimPlayerJoinedStateChange` objects representing the applicable players. Naturally, the original device that was already in the XIM network won't be moving, but will see the new device's users be added as players through additional `XimPlayerJoinedStateChange` objects.

Once players across different devices are joined together in a XIM network, they can initiate multiplayer scenarios, communicate over voice and text automatically, and send app-specific messages.

## Basic matchmaking and moving to another XIM network with others

You can further expand the networking experience for a group of friends by moving the players to a XIM network that also has strangers. Strangers are opponents from around the world who are brought together using the Xbox Live matchmaking service based on similar interests.

A simple way to initiate basic matchmaking with XIM is by calling `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` on one of the devices with a populated `MatchmakingConfiguration` object, taking players from the current XIM network along with it.

The following example initiates a move using a matchmaking configuration set up to find a total of 8 players for a no-teams free-for-all match. If 8 total players aren't found, the system might still match 2-7 players together. This example uses an app-defined constant, of type uint, named MYGAMEMODE_DEATHMATCH, that represents the game-mode to filter off of. XIM's matchmaking will only match this network with other players specifying that same value and brings along all socially-joined players from the current XIM network when providing the second parameter `XimPlayersToMove.BringExistingSocialPlayers` to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`:

```cs
var matchmakingConfiguration = new XimMatchmakingConfiguration();
matchmakingConfiguration.TeamConfiguration.TeamCount = 1;
matchmakingConfiguration.TeamConfiguration.MinPlayerCountPerTeam = 2;
matchmakingConfiguration.TeamConfiguration.MaxPlayerCountPerTeam = 8;
matchmakingConfiguration.CustomGameMode = MYGAMEMODE_DEATHMATCH;
XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking(matchmakingConfiguration, XimPlayersToMove.BringExistingSocialPlayers);
```

Like earlier moves, this will provide an initial `MoveToNetworkStartingStateChange` on all devices, and a `MoveToNetworkSucceededStateChange` once the move completes successfully. Since this is a move from one XIM network to another, one difference is that there are already existing `IXimPlayer` objects added for local and remote users, and these will remain for all players that are moving together to the new XIM network. Chat and data communication among players will continue to work uninterrupted while matchmaking is in progress.

Matchmaking can be a lengthy process depending on the number of potential players in the matchmaking pool that have also called `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`.

A `XimMatchmakingProgressUpdatedStateChange` will be provided periodically throughout the operation to keep you and your users informed of the current status. When the match has been found, the additional players are added to the XIM network with the typical `PlayerJoinedStateChange` and the move completes.

Once you've finished the multiplayer experience with this set of "matchmade" players, you can repeat the process to move to a different XIM network with another round of matchmaking. You'll see each player that joined via the prior `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` operation provide a `PlayerLeftStateChange` to indicate that their `IXimPlayer` objects are no longer in the same XIM network.

If when you choose to initiate matchmaking again using `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`, you specify `XimPlayersToMove.BringExistingSocialPlayers`, the players that joined via social entry points (`XboxIntegratedMultiplayer.MoveToNetworkUsingProtocolActivatedEventArgs()` or `xXboxIntegratedMultiplayer.MoveToNetworkUsingJoinableXboxUserId`) will remain while the new matchmaking takes place. Alternatively, specifying `XimPlayersToMove.BringOnlyLocalPlayers` will disconnect socially connected remote players, leaving just the local players to remain. In both cases, a different set of strangers will be added when the second move operation completes.

You also have the choice to move the non-matchmade players (or just local players) to a completely new XIM network while deciding the next matchmaking configuration/multiplayer activity.

The following example demonstrates how a device would call `XboxIntegratedMultiplayer.MoveToNewNetwork()` for a XIM network with a maximum of 8 players, but this time taking the existing socially-joined players as well:

```cs
XboxIntegratedMultiplayer.MovetoNewNetwork(8, XimPlayersToMove.BringExistingSocialPlayers);
```

A `MoveToNetworkStartingStateChange` and `MoveToNetworkSucceededStateChange` will be provided to all participating devices, along with a `PlayerLeftStateChange` for the matchmade that were left behind. On those devices, they will similarly see a `PlayerLeftStateChange` for each player that is moved out of the network.

You can continue moving from XIM network to XIM network in the above-described manner as many times as desired.

For performance, the Xbox Live service will not try to match groups of players on devices that are unlikely to be able to establish any direct peer-to-peer connections. If you're developing in a network environment that's not properly configured to support standard Xbox Live multiplayer, the move to new network using matchmaking operation might continue indefinitely without successful matching even when you're certain you have sufficient players meeting the matchmaking criteria who are all moving and all using devices in the same local environment. Be sure to run the multiplayer connectivity test in the network settings area/Xbox application and follow its recommendations if it reports trouble, particularly regarding a "Strict NAT".

## Disabling matchmaking NAT rule for debugging purposes

If your network administrator is unable to make the necessary environment changes to support XIM's NAT rules, you can unblock your matchmaking testing on Xbox One development kits by configuring XIM to allow matching "Strict NAT" devices without at least one "Open NAT" device. This is done by placing a file called "xim_disable_matchmaking_nat_rule" (contents don't matter) at the root of the "title scratch" drive on all Xbox One consoles. One example way to do that is by executing the following from an XDK command prompt before launching your app, replacing the placeholder "{console_name_or_ip_address}" for each console as appropriate:

```bat
echo.>%TEMP%\emptyfile.txt
copy %TEMP%\emptyfile.txt \\{console_name_or_ip_address}\TitleScratch\xim_disable_matchmaking_nat_rule
del %TEMP%\emptyfile.txt
```

> [!NOTE]
> This development workaround is currently only available for Xbox One exclusive resource applications and is not supported for Universal Windows Applications. Also note that consoles that are using this setting will never match with devices that don't also have this file present, regardless of network environment, so be sure to add and remove the file everywhere.

## Leaving a XIM network and cleaning up

When the local users are done participating in a XIM network, often they will simply move back to a new XIM network that allows local users, invites, and "followed" users to join it so they can continue coordinating with their friends to find the next activity. But if the user is completely done with all multiplayer experiences, then your app may want to begin leaving the XIM network altogether and return to the state as if only `XboxIntegratedMultiplayer.SetIntendedLocalXboxUserIds` had been called. This is done using the `XboxIntegratedMultiplayer.LeaveNetwork()` method:

```cs
 XboxIntegratedMultiplayer.LeaveNetwork();
```

This method begins the process of asynchronously disconnecting from the other participants gracefully. This will cause the remote devices to be provided a `PlayerLeftStateChange` for each local player that disconnected. The local device will be also be provided a `PlayerLeftStateChange` for each local and remote player that has disconnected. When all disconnect operations have finished, a final `NetworkExitedStateChange` will be provided.

Invoking `XboxIntegratedMultiplayer.LeaveNetwork()` and waiting for the `NetworkExitedStateChange` in order to exit a XIM network gracefully is always highly recommended when a `NetworkExitedStateChange` has not already been provided.

## Sending and receiving messages

XIM and its underlying components do all the tedious work of establishing secure communication channels over the Internet so you don't have to worry about connectivity problems or being able to reach some but not all players. If there are any fundamental peer-to-peer connectivity issues, moving to a XIM network will not succeed.

When a XIM network is formed and confirmed with `XimMoveToNetworkSucceededStateChange`, all instances of your app across all joined devices are guaranteed to be informed of every `IXimPlayer` connected to the XIM network, and can send messages to any of them.

Let's demonstrate how to send messages across the XIM network. The following example assumes the 'sendingPlayer' variable is a valid local player object. The example uses 'sendingPlayer''s method `SendDataToOtherPlayers()` to send a message structure that has been copied into the 'msgBuffer' to all players (local or remote) in the XIM network. Please note that it goes to all players because a list of specific players was not passed into the method:

```cs
SendingPlayer.SendDataToOtherPlayers(msgBuffer, null, XimSendType.GuaranteedAndSequential);
```

All recipients of the message will be provided a `XimPlayertoPlayerDataReceivedStateChange` that includes a copy of the data, as well as the `IXimPlayer` object that sent it and a list of the `IXimPlayer` objects that are locally receiving it.

Of course, guaranteed, sequential delivery is convenient, but it can also be an inefficient send type, since XIM needs to retransmit or delay messages if packets are dropped or disordered by the Internet. Be sure to consider using the other send types for messages that your app can tolerate losing or having arrive out of order. Since message data comes from a remote machine, the best practice is to have clearly defined data formats, such as packing multi-byte values in a particular byte order ("endianness"). The app should also validate the data before acting on it.

XIM provides network-level security so there is no need for additional encryption or signature schemes. However, we recommend always to employ "defense-in-depth" in order to protect against accidental application bugs and to be able to handle different versions of your application protocol coexisting gracefully (during development, content updates, etc.). Users' Internet connections can be limited and ever-changing resources. We strongly recommend the use of efficient message data formats and avoiding designs that send every UI frame.

## Assessing data pathway quality

To learn about the current quality of the data pathway between two players by inspecting the `XimPlayer.NetworkPathInformation` property. You can learn more about the current quality of the path between two players by inspecting the `XimPlayer.NetworkPathInformation` property. The property includes the estimated round trip latency and how many messages are still queued locally for transmission in the case that the connection can't support transmitting more data for that moment.

If you see that the queues are backing up for a particular 'IXimPlayer', you should reduce the rate at which you're sending data.

The `NetworkPathInformation.RoundTripLatency` field represents the latency of the underlying network and XIM's estimated latency without queuing. Effective latency increases as `XimNetworkPathInformation.SendQueueSizeInMessages` grows and XIM works through the queue.

Choose a reasonable point to start throttling calls to `SendDataToOtherPlayers` based on the game's usage and requirements. Every message in the send queue represents an increase in the effective network latency.

A value close to XIM's max limit (currently 3500 messages) is far too high for most games and likely represents several seconds of data waiting to be sent depending on the rate of calling `SendDataToOtherPlayers` and how big each data payload is. Instead, choose a number that takes into account the game's latency requirements along with how jittery the game's `SendDataToOtherPlayers` calling pattern is.

## Sharing data using player custom properties

Most app data exchanges happen with the `XimLocalPlayer.SendDataToOtherPlayers()` method since it allows the most control over who receives it and when, how it should deal with packet loss, and so on. However there are times when it would be nice for players to share basic, rarely changing state about themselves with others with minimal fuss. For example, each player might have a fixed string representing the character model selected before entering multiplayer that all players use to render their in-game representation.

For data that doesn't change very often about a player, XIM provides player custom properties. These properties consist of an app-defined name and value, which are null-terminated string pairs that can be applied to the local player and that get automatically propagated to all devices whenever they are changed.

Custom player properties have more internal synchronization overhead than the `XimLocalPlayer.SendDataToOtherPlayers()` method, so for rapidly changing data (i.e. player position), you should still use direct sends.

Player custom properties key-value pairs have their current values automatically provided to new participating devices when these devices join a XIM network and see the player added. Values can be set by calling `XimLocalPlayer.SetPlayerCustomProperty()` with name and value strings, like in the following example that sets a property named "model" to have the value "brute" on a local `IXIMPlayer` object pointed to by the variable 'localPlayer':

```cs
localPlayer.SetPlayerCustomProperty("model","brute");
```

Changes to player properties will cause a `XimPlayerCustomPropertiesChangedStateChange` to be provided to all devices, alerting them to the names of properties that have changed. The value for a given name can be retrieved on any player, local or remote, with `IXIMPlayer.GetPlayerCustomProperty()`. The following example retrieves the value for a property named "model" from a `IXIMPlayer` pointed to by the variable 'ximPlayer':

```cs
string propertyValue = localPlayer.GetPlayerCustomProperty("model");
```

Setting a new value for a given property name will replace any existing value. Setting a property name to a value of a null value string pointer is treated the same as an empty value string, which is the same as the property not having been specified yet. Names and values aren't interpreted by XIM, therefore it's left on the app to validate the string contents as needed.

Custom player properties are always reset when moving from one XIM network to another. However, new players that join the XIM network will receive current custom player properties for all players who have some set.

## Sharing data using network custom properties

Network custom properties are intended as a convenience synchronizing data specific to the XIM network which doesn't change frequently. Network custom properties work identically to [player custom properties](#sharing-data-using-player-custom-properties), except they're set on the XIM singleton object with `XboxIntegratedMultiplayer.SetNetworkCustomProperty()`. The following example sets a "map" property to have the value "stronghold":

```cs
XboxIntegratedMultiplayer.SetNetworkCustomProperty("map", "stronghold");
```

Changes to network properties will cause a `NetworkCustomPropertiesChanged` to be provided to all devices, alerting them to the names of properties that have changed. The value for a given name can be retrieved with `XboxIntegratedMultiplayer.GetNetworkCustomProperty()`, like in the following example that retrieves the value for a property named "map":

```cs
string property = XboxIntegratedMultiplayer.GetNetworkCustomProperty("map")
```

Just like [player custom properties](#sharing-data-using-player-custom-properties), setting a new value for a given property name will replace any existing value. Setting a property name to a value of a null value string pointer is treated the same as an empty value string, which is the same as the property not having been specified yet. Names and values aren't interpreted by XIM, therefore it's left on the app to validate the string contents as needed.

Newly created XIM networks always start with no network custom properties set. However, new players that join an existing XIM network will receive the current values of the network custom properties set for that XIM network.

## Matchmaking using per-player skill

Matching players by common interest in a particular app-specified game mode is a good base strategy. As the pool of available players grows, you should consider also matching players based on their personal skill or experience with your game so that veteran players can enjoy the challenge of healthy competition with other veterans, while newer players can grow by competing against others with similar abilities.

To do this, start by providing the skill level for all local players in their per-player roles and skill configuration structure specified in calls to `XimLocalPlayer.SetRolesAndSkillConfiguration()` prior to starting to move to a XIM network using matchmaking. Skill level is an app-specific concept and the number is not interpreted by XIM, except that matchmaking will first try to find players with the same skill value, and then periodically widen its search in increments of +/- 10 to try to find other players declaring skill values within a range around that skill. The following example assumes that the local `XimLocalPlayer` object, whose variable is 'localPlayer', has an associated app-specific integer skill value retrieved from local or Xbox Live storage into a variable called 'playerSkillValue':

```cs
var config = new XimPlayerRolesAndSkillConfiguration();
config.Skill = playerSkillValue;
localPlayer.SetRolesAndSkillConfiguration(config);
```

When this completes, all participants will be provided a `XimPlayerRolesAndSkillConfigurationChangedStateChange` indicating this `IXIMPlayer` has changed its per-player roles and skill configuration. The new value can be retrieved by calling `IXIMPlayer.RolesAndSkillConfiguration`. When all players have non-null matchmaking configuration applied, you can move to a XIM network using matchmaking with a value of true for the `RequirePlayerRolesAndSkillConfiguration` field of the `XimMatchmakingConfiguration` structure specified to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`.

The following example populates a matchmaking configuration to find a total of 2-8 players for a no-teams free-for-all. Additionally, this example uses an app-defined constant, which is of type Uint64 and named MYGAMEMODE_DEATHMATCH, that represents the game-mode to filter off of. This configures matchmaking to match the players of the XIM network with other players specifying those same values, as well as requiring per-player matchmaking configuration.

```cs
XimMatchmakingConfiguration matchmakingConfiguration = new XimMatchmakingConfiguration();
matchmakingConfiguration.TeamConfiguration.TeamCount = 1;
matchmakingConfiguration.TeamConfiguration.MinPlayerCountPerTeam = 2;
matchmakingConfiguration.TeamConfiguration.MaxPlayerCountPerTeam = 8;
matchmakingConfiguration.CustomGameMode = MYGAMEMODE_DEATHMATCH;
matchmakingConfiguration.RequirePlayerRolesAndSkillConfiguration = true;
```

When this structure is provided to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`, the move operation will start normally as long as players moving have called `XimLocalPlayer.SetRolesAndSkillConfiguration()` with a non-null `XimPlayerRolesAndSkillConfiguration` object. If any player hasn't, then the matchmaking process will be paused and all participants will be provided a `XimMatchmakingProgressUpdatedStateChange` with a `WaitingForPlayerRolesAndSkillConfiguration` value. This includes players that subsequently join the XIM network through a previously sent invitation or through other social means (e.g., a call to `XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableXboxUserId()`) before matchmaking has completed. Once all players have supplied their `XimPlayerRolesAndSkillConfiguration` objects, matchmaking will resume.

Matchmaking using per-player skill can also be combined with matchmaking user per-player role, as explained in the next section. If only one is desired, you can specify a value of 0 for the other. This is because all players declaring they have a `XimPlayerRolesAndSkillConfiguration` skill value of 0 will always match each other.

Once the `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` or any other XIM network move operation has completed, all players' `XimPlayerRolesAndSkillConfiguration` structures will automatically be cleared to a null pointer (with an accompanying `XimPlayerRolesAndSkillConfigurationChangedStateChange` notification). If you plan to move to another XIM network using matchmaking that requires per-player configuration, you'll need to call `XimLocalPlayer.SetRolesAndSkillConfiguration()` again with a object containing the most up-to-date information.

## Matchmaking using per-player role

Another method of using per-player roles and skill configuration to improve users' matchmaking experience is through the use of required player roles. This is best suited to games that provide selectable character types that encourage different cooperative play styles. These character types are ones which don't simply alter in-game graphical representation and, instead, alter the gameplay style for the player. Users' may prefer to play as a particular specialization. However, if your game is designed such that it's functionally not possible to complete objectives without at least one person fulfilling each role, sometimes it's better to match such players together first than to match any players together then require them to negotiate play styles among themselves once gathered. You can do this by first defining a unique bit flag representing each role to be specified in a given player's `XimPlayerRolesAndSkillConfiguration` structure.

The following example sets an app-specific role value, which is of type byte and named MYROLEBITFLAG_HEALER, for the local `XimLocalPlayer` object, whose pointer is 'localPlayer':

```cs
var config = new XimPlayerRolesAndSkillConfiguration();
config.Roles = MYROLEBITFLAG_HEALER;
localPlayer.SetRolesAndSkillConfiguration(config);
```

When this completes, all participants will be provided a `XimPlayerRolesAndSkillConfigurationChangedStateChange` indicating this `IXimPlayer` has changed its per-player roles and skill configuration. The new value can be retrieved by calling `IXimPlayer.RolesAndSkillConfiguration`.

The global `XimMatchmakingConfiguration` structure specified to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` should then have all the required roles flags combined using bitwise-OR, and a value of true for the `RequirePlayerRolesAndSkillConfiguration` field.

Matchmaking using per-player role can also be combined with matchmaking user per-player skill. If only one is desired, specify a value of 0 for the other. This is because all players declaring they have a `XimPlayerRolesAndSkillConfiguration` skill value of 0 will always match each other; and, if all bits are zero in the `XimMatchmakingConfiguration.RequiredRoles` field, then no role bits are needed in order to match.

Once the `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` or any other XIM network move operation has completed, all players' `XimPlayerRolesAndSkillConfiguration` objects will automatically be cleared to null (with an accompanying `XimPlayerRolesAndSkillConfigurationChangedStateChange` notification). If you plan to move to another XIM network using matchmaking that requires per-player configuration, you'll need to call `XimLocalPlayer.SetRolesAndSkillConfiguration()` again with a object containing the most up-to-date information.

## How XIM works with player teams

Multiplayer gaming often involves players organized onto opposing teams. XIM makes it easy to assign teams when matchmaking by setting `XimMatchmakingConfiguration.TeamConfiguration`. The following example initiates a move using matchmaking configured to find a total of 8 players to place on two teams of 4 (although if 4 aren't found, 1-3 players are also acceptable). Additionally, this example uses an app-defined constant, which is of type uint and named MYGAMEMODE_CAPTURETHEFLAG, that represents the game-mode to filter off of.  Also, the configuration is set up to bring along all socially-joined players from the current XIM network:

```cs
var matchmakingConfiguration = new XimMatchmakingConfiguration();
matchmakingConfiguration.TeamConfiguration.TeamCount = 2;
matchmakingConfiguration.TeamConfiguration.MinPlayerCountPerTeam = 1;
matchmakingConfiguration.TeamConfiguration.MaxPlayerCountPerTeam = 4;
XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking(matchmakingConfiguration, XimPlayersToMove.BringExistingSocialPlayers);
```

When such a XIM network move operation completes, the players will be assigned a team index value 1 through {n} corresponding to the {n} teams requested. A player's team index value is retrieved via `IXIMPlayer.TeamIndex`. When using a `XimMatchmakingConfiguration.TeamConfiguration` with two or more teams, players will never be assigned a team index value of zero by the call to `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()`. This is in contrast to players that are added to the XIM network with any other configuration or type of move operation (such as through a protocol activation resulting from accepting an invitation), who will always have a zero team index. It may be helpful to treat team index 0 as a special "unassigned" team.

The following example retrieves the team index for a XIM player object stored in the 'ximPlayer' variable:

```cs
byte playerIndex = ximPlayer.TeamIndex;
```

For the preferred user experience (not to mention reduced opportunity for negative player behavior), the Xbox Live matchmaking service will never split players who are moving to a XIM network together onto different teams.

The team index value assigned initially by matchmaking is only a recommendation and the app can change it for local players at any time using `XimLocalPlayer.SetTeamIndex()`. This can also be called in XIM networks that don't use matchmaking at all. The following example configures a XIM local player object stored in the 'ximLocalPlayer' variable to have a new team index value of one:

```cs
ximLocalPlayer.SetTeamIndex(1);
```

All devices are informed that the player has a new team index value in effect when they're provided a `XimPlayerTeamIndexChangedStateChange` for that player. You can call `XimLocalPlayer.SetTeamIndex()` at any time.

Matchmaking evaluates required per-player roles independently from teams. Therefore it's not recommended to use both teams and required roles as simultaneous matchmaking configuration criteria because the teams will be balanced by player count, not by fulfilled player roles.

## Working with chat

Voice and text chat communication are automatically enabled among players in a XIM network. XIM handles interacting with all voice headset and microphone hardware for you. Your app doesn't need to do much for chat, but it does have one requirement regarding text chat: supporting input and display. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, players may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because players may configure the system to use speech-to-text.

If you wish to conditionally enable text mechanisms, these preferences can be detected on local players by accessing the `XimLocalPlayer.ChatTextToSpeechConversionPreferenceEnabled` and `XimLocalPlayer.ChatSpeechToTextConversionPreferenceEnabled` fields respectively, and you may wish to conditionally enable text mechanisms. However, we recommend that you consider making text input and display options that are always available.

`Windows::Xbox::UI::Accessability` is an Xbox One class specifically designed to provide simple rendering of in-game text chat with a focus on speech-to-text assistive technologies.

Once you have text input provided by a real or virtual keyboard, pass the string to the `XimLocalPlayer.SendChatText()` method. The following code shows sending an example hard-coded string from a local `IXIMPlayer` object pointed to by the variable 'localPlayer':

```cs
localPlayer.SendChatText(text);
```

This chat text is delivered to all players in the XIM network that can receive chat communication from the originating local player as a `XimChatTextReceivedStateChange`. It might be synthesized to speech audio if TTS is enabled.

Your app should make a copy of any text string received and display it along with some identification of the originating player for an appropriate amount of time (or in a scrollable window).

There are also some best practices regarding chat. It's recommended that anywhere players are shown, particularly in a list of gamertags such as a scoreboard, that you also display muted/speaking icons as feedback for the user. This is done by accessing `IXimPlayer.ChatIndicator` to retrieve a `XboxIntegratedMultiplayer.XimPlayerChatIndicator` representing the current, instantaneous status of chat for that player.

The value reported by `IXimPlayer.ChatIndicator` is expected to change frequently as players start and stop talking, for example. It is designed to support apps polling it every UI frame as a result.

> [!NOTE]
> If a local user doesn't have sufficient communications privileges due to their device settings, `XimLocalPlayer.ChatIndicator` will return a `XboxIntegratedMultiplayer.XimPlayerChatIndicator` with value `XIM_PLAYER_CHAT_INDICATOR_PLATFORM_RESTRICTED`. The expectation to meet requirements for the platform is that the app show iconography indicating a platform restriction for voice chat or messaging, and a message to user indicating the issue. One example message we recommend is: "Sorry, you're not allowed to chat right now."

## Muting players

Another best practice is to support muting players. XIM automatically handles system muting initiated by users through player cards, but apps should support game-specific transient muting that can be performed within the game UI via the `IXimPlayer.ChatMuted` property. The following example begins muting a remote `IXIMPlayer` object pointed to by the variable 'remotePlayer' so that no voice chat is heard and no text chat is received from it:

```cs
remotePlayer.ChatMuted = true;
```

The muting takes effect immediately and there is no XIM state change associated with it. It can be changing the value of the `IXimPlayer.ChatMuted` property to false. The following example unmutes a remote `IXIMPlayer` object pointed to by the variable 'remotePlayer':

```cs
remotePlayer.ChatMuted = false;
```

Mutes remain in effect for as long as the `IXIMPlayer` exists, including when moving to a new XIM network with the player. It is not persisted if the player leaves and the same user rejoins (as a new `IXIMPlayer` instance).

Players typically start in the unmuted state. If your app wants to start a player in the muted state for gameplay reasons, it can set `IXIMPlayer.ChatMuted` on the `IXIMPlayer` object before finishing processing the associated `PlayerJoinedStateChange`, and XIM will guarantee there will be no period of time where voice audio from the player can be heard.

An automatic mute check based on player reputation occurs when a remote player joins the XIM network. If the player has a bad reputation flag, the player is automatically muted. Muting only affects local state and therefore persists if a player moves across networks. The automatic reputation-based mute check is performed once and not re-evaluated again for as long as the `IXIMPlayer` remains valid.

## Configuring chat targets using player teams

As stated in the [How XIM works with player teams](#how-xim-works-with-player-teams) section of this document, the true meaning of any particular team index value is up to the app. XIM doesn't interpret them except for equality comparisons with respect to chat target configuration. If the chat target configuration reported by `XboxIntegratedMultiplayer.ChatTargets` is currently `XimChatTargets.SameTeamIndexOnly`, then any given player will only exchange chat communication with another if the two have the same value reported by `IXIMPlayer.TeamIndex` (and privacy/ policy also permit it).

To be conservative and support competitive scenarios, newly created XIM networks are automatically configured to default to `XimChatTargets.SameTeamIndexOnly`. However, chatting with vanquished opponents on the other team may be desirable, for example, in a post-game "lobby". You can instruct XIM to allow everyone to talk to everyone else (where privacy and policy permit) by calling `XboxIntegratedMultiplayer.SetChatTargets()`. The following sample begins configuring all participants in the XIM network to use a `XimChatTargets.AllPlayers` value:

```cs
XboxIntegratedMultiplayer.SetChatTargets(XimChatTargets.AllPlayers)
```

All participants are informed that a new target setting is in effect when they're provided a `XimChatTargetsChangedStateChange`.

As noted earlier, most XIM network move types will initially assign all players the team index value of zero. This means a configuration of `XimChatTargets.SameTeamIndexOnly` is likely indistinguishable from `XimChatTargets.AllPlayers` by default. However, players that move to a XIM network using matchmaking will have differing team index values if the matchmaking configuration's `TeamMatchmakingMode` value declared two or more teams. You can also call `XimLocalPlayer.SetTeamIndex()` at any time as shown above. If your app is using non-zero team index values through either of these methods, don't forget to manage the current chat targets setting appropriately.

## Automatic background filling of player slots ("backfill" matchmaking)

Disparate groups of players calling `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` at the same time gives the Xbox Live matchmaking service the greatest flexibility to organize them into new, optimal XIM networks quickly. However, some gameplay scenarios would like to keep a particular XIM network intact, and only matchmake additional players just to fill vacant player slots. XIM supports configuring matchmaking to operate in an automatic background filling mode, or "backfilling", by calling `XboxIntegratedMultiplayer.SetNetworkConfiguration()` with a `XimNetworkConfiguration` that has `XimAllowedPlayerJoins.Matchmade` flag set on its `XimNetworkConfiguration.AllowedPlayerJoins` property.

The following example configures backfill matchmaking to try to find a total of 8 players for a no-teams free-for-all (although if 8 aren't found, 2-7 players are also acceptable), using an app-specific game mode constant uint64_t defined by the value MYGAMEMODE_DEATHMATCH that will only match with other players specifying that same value:

```cs
var networkConfiguration = new XimNetworkConfiguration(XboxIntegratedMultiplayer.NetworkConfiguration);
networkConfiguration.AllowedPlayerJoins |= XimAllowedPlayerJoins.Matchmade;
networkConfiguration.TeamConfiguration.TeamCount = 1;
networkConfiguration.TeamConfiguration.MinPlayerCountPerTeam = 2;
networkConfiguration.TeamConfiguration.MaxPlayerCountPerTeam = 8;
networkConfiguration.CustomGameMode = MYGAMEMODE_DEATHMATCH;
XboxIntegratedMultiplayer.SetNetworkConfiguration(networkConfiguration);
```

This makes the existing XIM network available to devices calling `XboxIntegratedMultiplayer.MoveToNetworkUsingMatchmaking()` in the normal manner. Those devices see no behavior change. The participants in the backfilling XIM network will not move, but will be provided a `XimNetworkConfigurationChangedStateChange` signifying backfill turning on, as well as multiple `XimMatchmakingProgressUpdatedStateChange` notifications when applicable. Any matchmade player will be added to the XIM network using the normal `XimPlayerJoinedStateChange`.

By default, backfill matchmaking remains enabled indefinitely, although it won't try to add players if the XIM network already has the maximum number of players specified by the `XimNetworkConfiguration.TeamConfiguration` setting. Backfilling can be disabled by setting `XimNetworkConfiguration.AllowedPlayerJoins` not including `XimAllowedPlayerJoins.Matchmade`:

```cs
var networkConfiguration = new XimNetworkConfiguration(XboxIntegratedMultiplayer.NetworkConfiguration);
networkConfiguration.AllowedPlayerJoins &= ~XimAllowedPlayerJoins.Matchmade;
XboxIntegratedMultiplayer.SetNetworkConfiguration(networkConfiguration);
```

A corresponding `XimNetworkConfigurationChangedStateChange` will be provided to all devices, and once this asynchronous process has completed, a final `XimMatchmakingProgressUpdatedStateChange` will be provided with `MatchmakingStatus.None` to signify that no further matchmade players will be added to the XIM network.

When enabling backfill matchmaking with a `XimNetworkConfiguration.TeamConfiguration` setting that declares two or more teams, all existing players must have a valid team index that is between 1 and the number of teams. This includes players who have called `XimLocalPlayer.SetTeamIndex()` to specify a custom value or who have joined using an invitation or through other social means (e.g., a call to `XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableXboxUserId()`) and have been added with a default team index value of 0. If any player doesn't have a valid team index, then the matchmaking process will be paused and all participants will be provided a `XimMatchmakingProgressUpdatedStateChange` with a `MatchmakingStatus.WaitingForPlayerTeamIndex` value. Once all players have supplied or corrected their team index values with `XimLocalPlayer.SetTeamIndex()`, backfill matchmaking will resume. More information can be found in the [How XIM works with player teams](#how-xim-works-with-player-teams) section of this document.

Similarly, when enabling backfill matchmaking with a `XimNetworkConfiguration` structure with the `RequirePlayerRolesAndSkillConfiguration` field set to true, then all players must have specified a non-null per-player matchmaking configuration. If any player hasn't, then the matchmaking process will be paused and all participants will be provided a `XimMatchmakingProgressUpdatedStateChange` with a `XimMatchMakingStatus.WaitingForPlayerRolesAndSkillConfiguration` value. Once all players have supplied their `XimPlayerRolesAndSkillConfiguration` objects, backfill matchmaking will resume. More information can be found in the [Matchmaking using per-player skill](#matchmaking-using-per-player-skill) and [Matchmaking using per-player role](#matchmaking-using-per-player-role) sections of this document.

## Querying joinable networks

While matchmaking is a great way to connect players together quickly, sometimes it's best to allow players to discover joinable networks using custom search criteria, and select the network they wish to join. This can be particularly advantageous when a game session might have a large set of configurable game rules and player preferences. To do this, an existing network must first be made queryable by enabling `XimAllowedPlayerJoins.Queried` joinability and configuring the network information available to others outside the network through a call to `XboxIntegratedMultiplayer.SetNetworkConfiguration()`.

The following example enables `XimAllowedPlayerJoins.Queried` joinability, sets network configuration with a team configuration that allows a total of 1-8 players together in 1 team, an app-specific game mode constant uint64_t defined by the value GAME_MODE_BRAWL, a description "cat and sheep's boxing match", an app-specific map index constant uint32_t defined by the value MAP_KITCHEN and includes tags "chatrequired", "easy", "spectatorallowed":

```cs
string[] tags = { "chatrequired", "easy", "spectatorallowed" };
var networkConfiguration = new XimNetworkConfiguration(XboxIntegratedMultiplayer.NetworkConfiguration);
networkConfiguration.AllowedPlayerJoins |= XimAllowedPlayerJoins.Queried;
networkConfiguration.TeamConfiguration.TeamCount = 1;
networkConfiguration.TeamConfiguration.MinPlayerCountPerTeam = 1;
networkConfiguration.TeamConfiguration.MaxPlayerCountPerTeam = 8;
networkConfiguration.CustomGameMode = GAME_MODE_BRAWL;
networkConfiguration.Description = "Cat and sheep's boxing match";
networkConfiguration.MapIndex = MAP_KITCHEN;
networkConfiguration.SetTags(tags);
XboxIntegratedMultiplayer.SetNetworkConfiguration(networkConfiguration);
```

Other players outside the network can then find the network by calling xim::start_joinable_network_query() with a set of filters that match the network information in the previous xim::set_network_configuration() call. The following example starts a joinable network query with the game mode filter option that will only query for networks using the app-specific game mode defined by value GAME_MODE_BRAWL:

```cs
XimJoinableNetworkQueryFilters queryFilters = new XimJoinableNetworkQueryFilters();
queryFilters.CustomGameModeFilter = GAME_MODE_BRAWL;
XboxIntegratedMultiplayer.StartJoinableNetworkQuery(queryFilters);
```

Here is another example that uses the tag filters option to query for networks having tag "easy" and "spectatorallowed" in their public queryable configuration:

```cs
string[] tagFilters = { "easy", "spectatorallowed" };
XimJoinableNetworkQueryFilters queryFilters = new XimJoinableNetworkQueryFilters();
queryFilters.SetTagFilters(tagFilters);
XboxIntegratedMultiplayer.StartJoinableNetworkQuery(queryFilters);
```

Different filter options can also be combined. The following example that uses both the game mode filter option and tag filter option to start a query for networks that both have the app-specific game mode constant GAME_MODE_BRAWL and tag "easy":

```cs
string[] tagFilters = { "easy" };
XimJoinableNetworkQueryFilters queryFilters = new XimJoinableNetworkQueryFilters();
queryFilters.CustomGameModeFilter = GAME_MODE_BRAWL;
queryFilters.SetTagFilters(tagFilters);
XboxIntegratedMultiplayer.StartJoinableNetworkQuery(queryFilters);
```

If the query operation succeeds, the app will receive a xim_start_joinable_network_query_completed_state_change from which the app can retrieve a list of joinable networks. The app will also continuously receive `XimJoinableNetworkUpdatedStateChange` for additional joinable networks or any changes that happen to the returned list of joinable networks until it is stopped either manually or automatically. The in-progress query can be stopped manually by calling `XboxIntegratedMultiplayer.StopJoinableNetworkQuery()`. It will be stopped automatically when calling `XboxIntegratedMultiplayer.StartJoinableNetworkQuery()` to start a new query.

The app can try to join a network in the list of joinable networks by calling `XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableNetworkInformation()`. The following example assumes you are trying to join a network referenced by 'selectedNetwork' which is not secured by a passcode (so we are passing empty string to the second parameter):

```cs
XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableNetworkInformation(selectedNetwork, "");
```

When enabling network query with a `XimNetworkConfiguration.TeamConfiguration` that declares two or more teams, players joined by calling XboxIntegratedMultiplayer.MoveToNetworkUsingJoinableNetworkInformation() will have a default team index value of 0.