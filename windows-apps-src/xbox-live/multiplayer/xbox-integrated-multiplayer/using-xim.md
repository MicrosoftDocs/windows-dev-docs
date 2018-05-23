---
title: Using XIM (C++)
author: KevinAsgari
description: Learn how to use Xbox Integrated Multiplayer (XIM) with C++.
ms.author: kevinasg
ms.date: 04/24/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, xbox one, xbox integrated multiplayer
ms.localizationpriority: low
---
# Using XIM (C++)

> [!div class="op_single_selector" title1="Language"]
> - [C++](using-xim.md)
> - [C#](using-xim-cs.md)

This is a brief walkthrough on using XIM's C++ API. Game developers wanting to access XIM through C# should see [Using XIM (C#)](using-xim-cs.md).

- [Using XIM (C++)](#using-xim-c)
    - [Prerequisites](#prerequisites)
    - [Initialization and startup](#initialization-and-startup)
    - [Asynchronous operations and processing state changes](#asynchronous-operations-and-processing-state-changes)
    - [Handling XIM player objects](#handling-xim-player-objects)
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

Compiling XIM requires including the primary `XboxIntegratedMultiplayer.h` header. In order to link properly, your project must also include `XboxIntegratedMultiplayerImpl.h` in at least one compilation unit (a common precompiled header is recommended since these stub function implementations are small and easy for the compiler to generate as "inline").

As mentioned in the [XIM Overview](../xbox-integrated-multiplayer.md), the XIM interface does not require a project to choose between compiling with C++/CX versus traditional C++; it can be used with either. The implementation also doesn't throw exceptions as a means of non-fatal error reporting so you can consume it easily from exception-free projects, if preferred.

## Initialization and startup

You begin interacting with the library by initializing the XIM object singleton with your Xbox Live service configuration ID string and title ID number. If you're also using the Xbox Services API, you may find it convenient to retrieve these from `xbox::services::xbox_live_app_config`. The following example assumes the values already reside in 'myServiceConfigurationId' and 'myTitleId' variables respectively:

```cpp
xim::singleton_instance().initialize(myServiceConfigurationId, myTitleId);
```

Once initialized, the app should retrieve the Xbox User ID strings for all users currently on the local device that will participate, and pass them to a `xim::set_intended_local_xbox_user_ids()` call. The following sample code assumes a single user has pressed a controller button expressing intent to play and the Xbox User ID string associated with the user has already been retrieved into a 'myXuid' variable:

```cpp
xim::singleton_instance().set_intended_local_xbox_user_ids(1, &myXuid);
```

A call to `xim::set_intended_local_xbox_user_ids()` immediately sets the Xbox User IDs associated with the local users that should be added to the XIM network. This list of Xbox User IDs will be used in all future network operations until the list changes through another call to `xim::set_intended_local_xbox_user_ids()`.

In the case where no XIM network exists at all yet, the first step is to move to a XIM network in order to get that process started. If the user doesn't already have a specific XIM network in mind, the best practice is to simply move to a new, empty network. This allows the user's friends to join the network as a sort of "lobby" from which they can collaborate together to select their next multiplayer activity (such as entering matchmaking).

The following is an example of how to move just the local users previously added with  `xim::set_intended_local_xbox_user_ids()` to an empty XIM network with room for up to 8 total players:

```cpp
xim::singleton_instance().move_to_new_network(8, xim_players_to_move::bring_only_local_players);
```

The call to `xim::move_to_new_network()` initiates an asynchronous move operation that concludes with a state change event that developers should regularly process for.

## Asynchronous operations and processing state changes

The heart of XIM is the app's regular, frequent calls to the `xim::start_processing_state_changes()` and `xim::finish_processing_state_changes()` pair of methods. These methods are how XIM is informed that the app is ready to handle updates to multiplayer state, and how XIM provides those updates. They're designed to operate quickly such that they can be called every graphics frame in your UI rendering loop. This provides a convenient place to retrieve all queued changes without worrying about the unpredictability of network timings or multi-threaded callback complexity. The XIM API is actually optimized for this single-threaded pattern. It guarantees its state will remain unchanged outside of these two functions, so you can use it directly and efficiently.

For the same reason, all objects returned by the XIM API should *not* be considered thread-safe. The library has internal multithreading protection, but you will still need to implement your own locking if you require an arbitrary thread to access any of XIM's values. For example, in the case of having one thread walk the `xim::players()` list while another thread could be invoking either `xim::start_processing_state_changes()` or `xim::finish_processing_state_changes()` and altering the memory associated with that player list.

When `xim::start_processing_state_changes()` is called, all queued updates are reported in an array of `xim_state_change` structure pointers.

Apps should:

1. Iterate over the array.
1. Inspect the base structure for its more specific type.
1. Cast the base structure type to the corresponding more detailed type.
1. Handle that update as appropriate.

Once finished with all the `xim_state_change` structures currently available, that array should be passed back to XIM to release the resources by calling `xim::finish_processing_state_changes()`. For example:

```cpp
uint32_t stateChangeCount;
xim_state_change_array stateChanges;
xim::singleton_instance().start_processing_state_changes(&stateChangeCount, &stateChanges);
for (uint32_t stateChangeIndex = 0; stateChangeIndex < stateChangeCount; stateChangeIndex++)
{
   const xim_state_change * stateChange = stateChanges[stateChangeIndex];
   switch (stateChange->state_change_type)
   {
       case xim_state_change_type::player_joined:
       {
           MyHandlePlayerJoined(static_cast<const xim_player_joined_state_change*>(stateChange));
           break;
        }

       case xim_state_change_type::player_left:
       {
           MyHandlePlayerLeft(static_cast<const xim_player_left_state_change*>(stateChange));
           break;
       }

       ...
    }
 }
 xim::singleton_instance().finish_processing_state_changes(stateChanges);
```

Now that you have your basic processing loop, you can handle the state changes associated with the initial `xim::move_to_new_network()` operation. Every XIM network move operation will begin with a `xim_move_to_network_starting_state_change`. If the move fails for any reason, then your app will be provided a `xim_network_exited_state_change`, which is the common failure handling mechanism for any asynchronous error that prevents you from moving to a XIM network or disconnects you from the current XIM network. Otherwise, the move will complete with a `xim_move_to_network_succeeded_state_change` after all the state has been finalized and all the players have been successfully added to the XIM network.

When a user does not have the platform privilege for playing with others in a multiplayer session, XIM will send to a device attempting to create or join a XIM network a `xim_network_exited_state_change` with the reason `xim_network_exit_reason::user_multiplayer_restricted`. This happens when any of the local users set with `xim::set_intended_local_xbox_user_ids()` have an Xbox Live multiplayer restriction. Xbox ERA Apps using XIM should either call `Store::Product::CheckPrivilegeAsync` on local players before attempting to move them into a XIM network with attemptResolution set to true, or do so in response to receiving `xim_network_exit_reason::user_multiplayer_restricted` as a reason for a returned `xim_network_exited_state_change`.

## Handling XIM player objects

Assuming the example of moving a single local user to a new XIM network succeeded, your app will be provided a `xim_player_joined_state_change` for a local `xim_player` object. This object pointer will remain valid for as long as the player instance itself is valid. The player instance becomes invalid when the corresponding `xim_player_left_state_change` for that player instance has been returned via a call to  `xim::finish_processing_state_changes()`. Your app will always be provided a `xim_player_left_state_change` for every `xim_player_joined_state_change`.

You can also retrieve an array of all `xim_player` objects in the XIM network at any time by using `xim::get_players()`.

The `xim_player` object has many helpful methods, such as `xim_player::gamertag()` for retrieving the current Xbox Live Gamertag string associated with the player for display purposes. If the `xim_player` is local to the device, then it will also report a non-null `xim_player::xim_local` object pointer from `xim_player::local()`, which has additional methods only available to local players.

Of course, the most important state for players is not the common information that XIM knows, but what your specific app wants to track. Since you likely have your own construct for that tracking information, you'll want to link the `xim_player` object to your own player construct object so that any time XIM reports a `xim_player`, you can quickly retrieve your state without having to populate and look up a custom player context pointer. The following example assumes a pointer to your private state is in the variable 'myPlayerStateObject' and the newly added `xim_player` object is in the variable 'newXimPlayer':

```cpp
newXimPlayer->set_custom_player_context(myPlayerStateObject);
```

This saves the specified pointer value with the player object locally (it is never transferred over the network to remote devices where the memory would not be valid). You'll then be able to always get back to your object by retrieving the custom context and casting it back to your object like the following example:

```cpp
myPlayerStateObject = reinterpret_cast<MyPlayerState *>(newXimPlayer->custom_player_context());
```

You can change the custom player context pointer assigned to a `xim_player` at any time.

## Enabling friends to join and inviting them

For privacy and security, all new XIM networks are automatically configured by default to be only joinable by local players, and it's up to the app to explicitly allow others once it is ready. The following example shows how to use `xim::network_configuration()` to retrieve the current network configuration and update joinability using `xim::set_network_configuration()` to begin allowing new local users to join as players, as well as other users that have been invited or that are being "followed" (an Xbox Live social relationship) by players already in the XIM network:

```cpp
xim_network_configuration networkConfiguration = *xim::singleton_instance.network_configuration();
networkConfiguration.allowed_player_joins = xim_allowed_player_joins::local | xim_allowed_player_joins::invited | xim_allowed_player_joins::followed;
xim::singleton_instance().set_network_configuration(&networkConfiguration);
```

`xim::set_network_configuration()` executes asynchronoulsy. Once the previous code sample call completes, a `xim_network_configuration_changed_state_change` is provided to notify the app that the joinability value has changed from its default of `xim_allowed_player_joins::none`. You can then query for the new value by checking the `allowed_player_joins` property of the `xim_network_configuration` returned by `xim::network_configuration()`. 

The `allowed_player_joins` can be checked while the device is in a XIM network to determine the joinability of the network.

Should one of the local players want to send out invitations to remote users to join this XIM network, the app can call `xim_player::xim_local::show_invite_ui()` to launch the system invitation UI. Here, the local user can select people they wish to invite and send out invitations. The following example demonstrates how this works and assumes that the variable 'ximPlayer' points to a valid local `xim_player`:

```cpp
ximPlayer->local()->show_invite_ui();
```

After the above statement executes, the system invitation UI will display. Once the user has sent the invitations (or otherwise dismissed the UI), a `xim_show_invite_ui_completed_state_change` will be provided on the next call to `xim::start_processing_state_changes()`. Alternatively, your app can send the invitations directly using `xim_player::xim_local::invite_users()` without bringing up the system invitation UI.

Either way, remote users will receive Xbox Live invitation messages wherever they are signed in, which they can choose to accept or reject. If they accept, a protocol activation will initiate your app on those devices if it isn't already running, and the protocol activation will be delivered to your app with its event arguments that can be used to move to the corresponding XIM network.

See platform documentation for more information on protocol activation itself.

The following example shows how a call to `xim::extract_protocol_activation_information()` determines if the event arguments are applicable to XIM. This assumes you've retrieved the raw URI string from `Windows::ApplicationModel::Activation::ProtocolActivatedEventArgs` to a variable 'uriString':

```cpp
xim_protocol_activation_information activationInfo;
bool isXimActivation;
isXimActivation = xim::singleton_instance().extract_protocol_activation_information(uriString, &activationInfo);
```

If it is a XIM activation, then you will want to ensure the local user identified in the 'local_xbox_user_id' field of the populated `xim_protocol_activation_information` structure is signed in and is among the users specified to `xim::set_intended_local_xbox_user_ids()`. You can then initiate moving to the specified XIM network with a call to `xim::move_to_network_using_protocol_activated_event_args()` using the same URI string. For example:

```cpp
xim::singleton_instance().move_to_network_using_protocol_activated_event_args(uriString);
```

Also note that "followed" remote users can navigate to the local user's player card in the system UI and initiate a join attempt themselves without an invitation (assuming you've allowed such player joins as shown above). This action will also protocol activate your app just like invites do and are handled in the same way.

Moving to a XIM network using protocol activation is identical to moving to a new XIM network as shown in [Initialization and startup](#initialization-and-startup). The only difference is that when the move succeeds, the moving device will be provided both local and remote player `xim_player_joined_state_change` structures representing all applicable players. Naturally, the original device that was already in the XIM network won't be moving, but will see the new device's users be added as players through additional `xim_player_joined_state_change` structures.

Once players across different devices are joined together in a XIM network, they can initiate multiplayer scenarios, communicate over voice and text automatically, and send app-specific messages.

## Basic matchmaking and moving to another XIM network with others

You can further expand the networking experience for a group of friends by moving the players to a XIM network that also has strangers. Strangers are opponents from around the world who are brought together using the Xbox Live matchmaking service based on similar interests.

A simple way to initiate basic matchmaking with XIM is by calling `xim::move_to_network_using_matchmaking()` on one of the devices with a populated `xim_matchmaking_configuration` structure, taking players from the current XIM network along with it.

The following example initiates a move using a matchmaking configuration set up to find a total of 8 players for a no-teams free-for-all match. If 8 total players aren't found, the system might still match 2-7 players together. This example uses an app-defined constant, of type uint64_t, named MYGAMEMODE_DEATHMATCH, that represents the game-mode to filter off of. XIM's matchmaking will only match this network with other players specifying that same value and brings along all socially-joined players from the current XIM network when providing the second parameter `xim_players_to_move::bring_existing_social_players` to `xim::move_to_network_using_matchmaking()`:

```cpp
xim_matchmaking_configuration matchmakingConfiguration = { 0 };
matchmakingConfiguration.team_configuration.team_count = 1;
matchmakingConfiguration.team_configuration.min_player_count_per_team = 2;
matchmakingConfiguration.team_configuration.max_player_count_per_team = 8;
matchmakingConfiguration.custom_game_mode = MYGAMEMODE_DEATHMATCH;

xim::singleton_instance().move_to_network_using_matchmaking(matchmakingConfiguration, xim_players_to_move::bring_existing_social_players);
```

Like earlier moves, this matchmaking process will provide an initial `xim_move_to_network_starting_state_change` on all devices, and a `xim_move_to_network_succeeded_state_change` once the move completes successfully. Since this is a move from one XIM network to another, one difference is that there are already existing `xim_player` objects added for local and remote users, and these will remain for all players that are moving together to the new XIM network. Chat and data communication among players will continue to work uninterrupted while matchmaking is in progress.

Matchmaking can be a lengthy process depending on the number of potential players in the matchmaking pool that have also called `xim::move_to_network_using_matchmaking()`.

A `xim_matchmaking_progress_updated_state_change` will be provided periodically throughout the operation to keep you and your users informed of the current status. When the match has been found, the additional players are added to the XIM network with the typical `xim_player_joined_state_change` and the move completes.

Once you've finished the multiplayer experience with this set of "matchmade" players, you can repeat the process to move to a different XIM network with another round of matchmaking. You'll see each player that joined via the prior `xim::move_to_network_using_matchmaking()` operation provide a `xim_player_left_state_change` to indicate that their `xim_player` objects are no longer in the same XIM network.

If when you choose to initiate matchmaking again using `xim::move_to_network_using_matchmaking()`, you specify `xim_players_to_move::bring_existing_social_players`, the players that joined via social entry points (`xim::move_to_network_using_protocol_activated_event_args()` or `xim::move_to_network_using_joinable_xbox_user_id()`) will remain while the new matchmaking takes place. Alternatively, specifying `xim_players_to_move::bring_only_local_players` will disconnect socially connected remote players, leaving just the local players to remain. In both cases, a different set of strangers will be added when the second move operation completes.

You also have the choice to move the non-matchmade players (or just local players) to a completely new XIM network while deciding the next matchmaking configuration/multiplayer activity. The following example demonstrates how a device would call `xim::move_to_new_network()` for a XIM network with a maximum of 8 players, but this time taking the existing socially-joined players as well:

```cpp
xim::singleton_instance().move_to_new_network(8, xim_players_to_move::bring_existing_social_players);
```

A `xim_move_to_network_starting_state_change` and `xim_move_to_network_succeeded_state_change` will be provided to all participating devices, along with a `xim_player_left_state_change` for the matchmade players that were left behind. On those devices, they will similarly see a `xim_player_left_state_change` for each player that is moved out of the network.

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

When local users are done participating in a XIM network, often they will simply move into a new XIM network that allows local users, invited users, and "followed" users to join so they can continue coordinating with their friends in finding the next activity. But if the user is completely done with all multiplayer experiences, then your app may want to begin leaving the XIM network altogether and return to the state as if only `xim::initialize()` and `xim::set_intended_local_xbox_user_ids()` had been called. This is done using the `xim::leave_network()` method:

```cpp
xim::singleton_instance().leave_network();
```

This method begins the process of asynchronously disconnecting from the other participants gracefully. This will cause the remote devices to be provided a `xim_player_left_state_change` for each local player that disconnected. The local device will be also be provided a `xim_player_left_state_change` for each local and remote player that has disconnected. When all disconnect operations have finished, a final `xim_network_exited_state_change` will be provided. The app can then call `xim::cleanup()`` to free all resources and return to the uninitialized state:

```cpp
xim::singleton_instance().cleanup();
```

Invoking `xim::leave_network()` and waiting for the `xim_network_exited_state_change` in order to exit a XIM network gracefully is always highly recommended when a `xim_network_exited_state_change` has not already been provided.

If `xim::cleanup()` is called directly without `xim::leave_network()` being called, then communication performance problems for the remaining participants will occur since they will be forced to send non-deliverable messages to the device that simply "disappeared" by calling `xim::cleanup()`.

## Sending and receiving messages

XIM and its underlying components do all the tedious work of establishing secure communication channels over the Internet so you don't have to worry about connectivity problems or being able to reach some but not all players. If there are any fundamental peer-to-peer connectivity issues, moving to a XIM network will not succeed.

When a XIM network is formed and confirmed with `xim_move_to_network_succeeded_state_change`, all instances of your app across all joined devices are guaranteed to be informed of every `xim_player` connected to the XIM network, and can send messages to any of them.

Let's demonstrate how to send messages across the XIM network. The following example assumes the 'sendingPlayer' variable is a pointer to a valid local player object. 'sendingPlayer' invokes `local()` and calls `send_data_to_other_players()` to send the message structure 'msgData' to all players (both local and remote) with guaranteed, sequential delivery. Please note that it goes to all players because a list of specific players was not passed into the method:

```cpp
sendingPlayer->local()->send_data_to_other_players(sizeof(msgData), &msgData, 0, nullptr, xim_send_type::guaranteed_and_sequential);
```

Messages can be delivered to all players using `send_data_to_other_players()` by not passing an array of specific players to the method.

All recipients of the message are provided a `xim_player_to_player_data_received_state_change` that includes a pointer to a copy of the data, as well as pointers to the corresponding xim_player object that sent it and are locally receiving it.

Of course, guaranteed, sequential delivery is convenient, but it can also be an inefficient send type, since XIM needs to retransmit or delay messages if packets are dropped or disordered by the Internet. Be sure to consider using the other send types for messages that your app can tolerate losing or having arrive out of order. Since message data comes from a remote machine, the best practice is to have clearly defined data formats, such as packing multi-byte values in a particular byte order ("endianness"). The app should also validate the data before acting on it.

XIM provides network-level security so there is no need for additional encryption or signature schemes. However, we recommend always to employ "defense-in-depth" in order to protect against accidental application bugs and to be able to handle different versions of your application protocol coexisting gracefully (during development, content updates, etc.). Users' Internet connections can be limited and ever-changing resources. We strongly recommend the use of efficient message data formats and avoiding designs that send every UI frame.

## Assessing data pathway quality

To learn about the current quality of the data pathway between two players, call the `xim_player::network_path_information()` method. The following example retrieves a pointer to the `xim_network_path_information` structure for a `xim_player` pointer contained in the 'remotePlayer' variable:

```cpp
 const xim_network_path_information * networkPathInfo = remotePlayer->network_path_information();
```

The returned structure includes the estimated round trip latency and how many messages are still queued locally for transmission in the case that the connection can't support transmitting more data for that moment.

If you see that the queues are backing up for a particular 'xim_player', you should reduce the rate at which you're sending data.

The `xim_network_path_information::round_trip_latency_in_milliseconds` field represents the latency of the underlying network and XIM's estimated latency without queuing. Effective latency increases as `xim_network_path_information::send_queue_size_in_messages` grows and XIM works through the queue.

Choose a reasonable point to start throttling calls to `send_data_to_other_players` based on the game's usage and requirements. Every message in the send queue represents an increase in the effective network latency.

A value close to XIM’s max limit (currently 3500 messages) is far too high for most games and likely represents several seconds of data waiting to be sent depending on the rate of calling `send_data_to_other_players` and how big each data payload is. Instead, choose a number that takes into account the game's latency requirements along with how jittery the game's `send_data_to_other_players` calling pattern is.

## Sharing data using player custom properties

Most app data exchanges happen with the `xim_player::xim_local::send_data_to_other_players()` method since it allows the most control over who receives data, when they should receive that data, and how the system should deal with packet loss. However there are times when it would be nice for players to share basic, rarely changing state about themselves with others with minimal fuss. For example, each player might have a fixed string representing the character model selected before entering multiplayer that all players use to render their in-game representation.

For data that doesn't change very often about a player, XIM provides player custom properties. These properties consist of an app-defined name and value, which are null-terminated string pairs that can be applied to the local player and that get automatically propagated to all devices whenever they are changed.

Custom player properties have more internal synchronization overhead than the `xim_player::xim_local::send_data_to_other_players()` method, so for rapidly changing data (i.e. player position), you should still use direct sends.

Player custom properties key-value pairs have their current values automatically provided to new participating devices when these devices join a XIM network and see the player added. Values can be set by calling `xim_player::xim_local::set_player_custom_property()` with name and value strings. In the following example, we set a property named "model" to have the value "brute" on a local `xim_player` object pointed to by the variable 'localPlayer':

```cpp
localPlayer->local()->set_player_custom_property(L"model", L"brute");
```

Changes to player properties will cause a `xim_player_custom_properties_changed_state_change` to be provided to all devices, alerting them to the names of properties that have changed. The value for a given name can be retrieved on any player, local or remote, with `xim_player::get_player_custom_property()`. The following example retrieves the value for a property named "model" from a `xim_player` pointed to by the variable 'ximPlayer':

```cpp
PCWSTR modelName = ximPlayer->get_player_custom_property(L"model");
```

Setting a new value for a given property name will replace any existing value. Setting a property name to a value of a null value string pointer is treated the same as an empty value string, which is the same as the property not having been specified yet. Names and values aren't interpreted by XIM, therefore it's left on the app to validate the string contents as needed.

Custom player properties are always reset when moving from one XIM network to another. However, new players that join the XIM network will receive current custom player properties for all players who have some set.

## Sharing data using network custom properties

Network custom properties are intended as a convenience synchronizing data specific to the XIM network which doesn't change frequently. Network custom properties work identically to [player custom properties](#sharing-data-using-player-custom-properties), except they're set on the XIM singleton object with `xim::set_network_custom_property()`. The following example sets a "map" property to have the value "stronghold":

```cpp
xim::singleton_instance().set_network_custom_property(L"map", L"stronghold");
```

Changes to network properties will cause a `xim_network_custom_properties_changed_state_change` to be provided to all devices, alerting them to the names of properties that have changed. The value for a given name can be retrieved with `xim::get_network_custom_property()`, like in the following example that retrieves the value for a property named "map":

```cpp
PCWSTR mapName = xim::singleton_instance().get_network_custom_property(L"map");
```

Just like [player custom properties](#sharing-data-using-player-custom-properties), setting a new value for a given property name will replace any existing value. Setting a property name to a value of a null value string pointer is treated the same as an empty value string, which is the same as the property not having been specified yet. Names and values aren't interpreted by XIM, therefore it's left on the app to validate the string contents as needed.

Newly created XIM networks always start with no network custom properties set. However, new players that join an existing XIM network will receive the current values of the network custom properties set for that XIM network.

## Matchmaking using per-player skill

Matching players by common interest in a particular app-specified game mode is a good base strategy. As the pool of available players grows, you should consider also matching players based on their personal skill or experience with your game so that veteran players can enjoy the challenge of healthy competition with other veterans, while newer players can grow by competing against others with similar abilities.

To do this, start by providing the skill level for all local players in their per-player roles and skill configuration structure specified in calls to `xim_player::xim_local::set_roles_and_skill_configuration()` prior to starting to move to a XIM network using matchmaking. Skill level is an app-specific concept and the number is not interpreted by XIM, except that matchmaking will first try to find players with the same skill value, and then periodically widen its search in increments of +/- 10 to try to find other players declaring skill values within a range around that skill. The following example assumes that the local `xim_player` object, whose pointer is 'localPlayer', has an associated app-specific uint32_t skill value retrieved from local or Xbox Live storage into a variable called 'playerSkillValue':

```cpp
xim_player_roles_and_skill_configuration playerRolesAndSkillConfiguration = { 0 };
playerRolesAndSkillConfiguration.skill = playerSkillValue;

localPlayer->local()->set_roles_and_skill_configuration(&playerRolesAndSkillConfiguration);
```

When this completes, all participants will be provided a `xim_player_roles_and_skill_configuration_changed_state_change` indicating this `xim_player` has changed its per-player roles and skill configuration. The new value can be retrieved by calling `xim_player::roles_and_skill_configuration()`. When all players have non-null roles and skill configuration applied, you can move to a XIM network using matchmaking with a value of true for the `require_player_roles_and_skill_configuration` field of the `xim_matchmaking_configuration` structure specified to `xim::move_to_network_using_matchmaking()`. The following example populates a matchmaking configuration that will find a total of 2-8 players for a no-teams free-for-all, using an app-specific game mode constant uint64_t defined by the value MYGAMEMODE_DEATHMATCH that will only match with other players specifying that same value, and that requires per-player roles and skill configuration:

```cpp
xim_matchmaking_configuration matchmakingConfiguration = { 0 };
matchmakingConfiguration.team_configuration.team_count = 1;
matchmakingConfiguration.team_configuration.min_player_count_per_team = 2;
matchmakingConfiguration.team_configuration.max_player_count_per_team = 8;
matchmakingConfiguration.custom_game_mode = MYGAMEMODE_DEATHMATCH;
matchmakingConfiguration.require_player_roles_and_skill_configuration = true;
```

When this structure is provided to `xim::move_to_network_using_matchmaking()`, the move operation will start normally as long as players moving have called `xim_player::xim_local::set_roles_and_skill_configuration()` with a non-null `xim_player_roles_and_skill_configuration` pointer. If any player hasn't, then the matchmaking process will be paused and all participants will be provided a `xim_matchmaking_progress_updated_state_change` with a `xim_matchmaking_status::waiting_for_player_roles_and_skill_configuration` value. This includes players that subsequently join the XIM network through a previously sent invitation or through other means (e.g., a call to `xim::move_to_network_using_joinable_xbox_user_id()`) before matchmaking has completed. Once all players have supplied their `xim_player_roles_and_skill_configuration` structures, matchmaking will resume.

Matchmaking using per-player skill can also be combined with matchmaking user per-player role, as explained in the next section. If only one is desired, you can specify a value of 0 for the other. This is because all players declaring they have a `xim_player_roles_and_skill_configuration` skill value of 0 will always match each other.

Once the `xim::move_to_network_using_matchmaking()` or any other XIM network move operation has completed, all players' `xim_player_roles_and_skill_configuration` structures will automatically be cleared to a null pointer (with an accompanying `xim_player_roles_and_skill_configuration_changed_state_change` notification). If you plan to move to another XIM network using matchmaking that requires per-player configuration, you'll need to call `xim_player::xim_local::set_roles_and_skill_configuration()` again with a new structure pointer containing the most up-to-date information.

## Matchmaking using per-player role

Another method of using per-player roles and skill configuration to improve users' matchmaking experience is through the use of required player roles. This is best suited to games that provide selectable character types that encourage different cooperative play styles; that is, types that don't simply alter in-game graphical representation, but control complementary, impactful attributes such as defensive "healers" vs. close-in "melee" offense vs. distant "range" attack support. Users' personalities mean they may prefer to play as a particular specialization. But if your game is designed such that it's functionally not possible to complete objectives without at least one person fulfilling each role, sometimes it's better to match such players together first than to match any players together then require them to negotiate play styles among themselves once gathered. You can do this by first defining a unique bit flag representing each role to be specified in a given player's `xim_player_roles_and_skill_configuration` structure.

The following example sets an app-specific MYROLEBITFLAG_HEALER uint8_t role value for the local xim_player object, whose pointer is 'localPlayer':

```cpp
xim_player_roles_and_skill_configuration playerRolesAndSkillConfiguration = { 0 };
playerRolesAndSkillConfiguration.roles = MYROLEBITFLAG_HEALER;

localPlayer->local()->set_roles_and_skill_configuration(&playerRolesAndSkillConfiguration);
```

When this completes, all participants will be provided a `xim_player_roles_and_skill_configuration_changed_state_change` indicating this `xim_player` has changed its per-player role configuration. The new value can be retrieved by calling `xim_player::roles_and_skill_configuration()`.

The global `xim_matchmaking_configuration` structure specified to `xim::move_to_network_using_matchmaking()` should have all the required roles flags combined using bitwise-OR, and a value of true for the require_player_matchmaking_configuration field.

The following example populates a matchmaking configuration that will find a total of 3 players for a no-teams free-for-all. Additionally, this example uses an app-defined constant, which is of type uint64_t and named MYGAMEMODE_COOPERATIVE, that represents the game-mode to filter off of. Also, the configuration is set up to require per-player roles and skill configuration where at least one player fulfills each app-specific uint8_t roles which were bitwise-OR'd together and placed in the configuration (MYROLEBITFLAG_HEALER, MYROLEBITFLAG_MELEE, and MYROLEBITFLAG_RANGE):

```cpp
xim_matchmaking_configuration matchmakingConfiguration = { 0 };
matchmakingConfiguration.team_configuration.team_count = 1;
matchmakingConfiguration.team_configuration.min_player_count_per_team = 3;
matchmakingConfiguration.team_configuration.max_player_count_per_team = 3;
matchmakingConfiguration.custom_game_mode = MYGAMEMODE_COOPERATIVE;
matchmakingConfiguration.required_roles = MYROLEBITFLAG_HEALER | MYROLEBITFLAG_MELEE | MYROLEBITFLAG_RANGE;
matchmakingConfiguration.require_player_roles_and_skill_configuration = true;
```

When this structure is provided to `xim::move_to_network_using_matchmaking()`, the move operation will start as described above.

Matchmaking using per-player role can also be combined with matchmaking user per-player skill. If only one is desired, specify a value of 0 for the other. This is because all players declaring they have a `xim_player_roles_and_skill_configuration` skill value of 0 will always match each other; and, if all bits are zero in the `xim_matchmaking_configuration` required_roles field, then no role bits are needed in order to match.

Once the `xim::move_to_network_using_matchmaking()` or any other XIM network move operation has completed, all players' `xim_player_roles_and_skill_configuration` structures will automatically be cleared to a null pointer (with an accompanying `xim_player_roles_and_skill_configuration_changed_state_change` notification). If you plan to move to another XIM network using matchmaking that requires per-player configuration, you'll need to call `xim_player::xim_local::set_roles_and_skill_configuration()` again with a new structure pointer containing the most up-to-date information.

## How XIM works with player teams

Multiplayer gaming often involves players organized onto opposing teams. XIM makes it easy to assign teams when matchmaking by setting `xim_team_configuration`. The following example initiates a move using matchmaking configured to find a total of 8 players to place on two equal teams of 4 (although if 4 aren't found, 1-3 players are also acceptable), using an app-specific game mode constant uint64_t defined by the value MYGAMEMODE_CAPTURETHEFLAG that will only match with other players specifying that same value, and bringing all socially-joined players from the current XIM network:

```cpp
xim_matchmaking_configuration matchmakingConfiguration = { 0 };
matchmakingConfiguration.team_configuration.team_count = 2;
matchmakingConfiguration.team_configuration.min_player_count_per_team = 1;
matchmakingConfiguration.team_configuration.max_player_count_per_team = 4;
matchmakingConfiguration.custom_game_mode = MYGAMEMODE_CAPTURETHEFLAG;

xim::singleton_instance().move_to_network_using_matchmaking(matchmakingConfiguration, xim_players_to_move::bring_existing_social_players);
```

When such a XIM network move operation completes, the players will be assigned a team index value 1 through {n} corresponding to the {n} teams requested. The true meaning of any particular team index value is up to the app. A player's team index value is retrieved via `xim_player::team_index()`. When using a `xim_team_configuration` with two or more teams, players will never be assigned a team index value of zero by the call to `xim::move_to_network_using_matchmaking()`. This is in contrast to players that are added to the XIM network with any other configuration or type of move operation (such as through a protocol activation resulting from accepting an invitation), who will always have a zero team index. It may be helpful to treat team index 0 as a special "unassigned" team.

The following example retrieves the team index for a xim_player object whose pointer is in the 'ximPlayer' variable:

```cpp
uint8_t playerTeamIndex = ximPlayer->team_index();
```

For the preferred user experience (not to mention reduced opportunity for negative player behavior), the Xbox Live matchmaking service will never split players who are moving to a XIM network together onto different teams.

The team index value assigned initially by matchmaking is only a recommendation and the app can change it for local players at any time using `xim_player::xim_local::set_team_index()`. This can also be called in XIM networks that don't use matchmaking at all. The following example configures a player pointer 'localPlayer' to have a new team index value of one:

```cpp
localPlayer->local()->set_team_index(1);
```

All devices are informed that the player has a new team index value in effect when they're provided a `xim_player_team_index_changed_state_change` for that player. You can call `xim_player::xim_local::set_team_index()` at any time.

Matchmaking evaluates required per-player roles independently from teams. Therefore it's not recommended to use both teams and required roles as simultaneous matchmaking configuration criteria because the teams will be balanced by player count, not by fulfilled player roles.

## Working with chat

Voice and text chat communication are automatically enabled among players in a XIM network. XIM handles interacting with all voice headset and microphone hardware for you. Your app doesn't need to do much for chat, but it does have one requirement regarding text chat: supporting input and display. Text input is required because, even on platforms or game genres that historically haven't had widespread physical keyboard use, players may configure the system to use text-to-speech assistive technologies. Similarly, text display is required because players may configure the system to use speech-to-text.

These preferences can be detected on local players by calling `xim_player::xim_local::chat_text_to_speech_conversion_preference_enabled()` and `xim_player::xim_local::chat_speech_to_text_conversion_preference_enabled()`, and you may wish to conditionally enable text mechanisms. However, we recommend that you consider making text input and display options that are always available.

 `Windows::Xbox::UI::Accessability` is an Xbox One class specifically designed to provide simple rendering of in-game text chat with a focus on speech-to-text assistive technologies.

Once you have text input provided by a real or virtual keyboard, pass the string to the `xim_player::xim_local::send_chat_text()` method. The following code shows sending an example hard-coded string from a local `xim_player` object pointed to by the variable 'localPlayer':

```cpp
localPlayer->local()->send_chat_text(L"Example chat text");
```

This chat text is delivered to all players in the XIM network that can receive chat communication from the originating local player. It might be synthesized to speech audio and it might be provided as a `xim_chat_text_received_state_change`.

Your app should make a copy of any text string received and display it along with some identification of the originating player for an appropriate amount of time (or in a scrollable window).

There are also some best practices regarding chat. It's recommended that anywhere players are shown, particularly in a list of gamertags such as a scoreboard, that you also display muted/speaking icons as feedback for the user. This is done by calling `xim_player::chat_indicator()` to retrieve a `xim_player_chat_indicator` representing the current, instantaneous status of chat for that player. The following example demonstrates retrieving the indicator value for a `xim_player` object pointed to by the variable 'ximPlayer' to determine a particular icon constant value to assign to an 'iconToShow' variable:

```cpp
switch (ximPlayer->chat_indicator())
{
   case xim_player_chat_indicator::silent:
   {
       iconToShow = Icon_InactiveSpeaker;
       break;
   }

   case xim_player_chat_indicator::talking:
   {
       iconToShow = Icon_ActiveSpeaker;
       break;
   }

   case xim_player_chat_indicator::muted:
   {
       iconToShow = Icon_MutedSpeaker;
       break;
   }
   ...
}
```

The value reported by `xim_player::chat_indicator()` is expected to change frequently as players start and stop talking, for example. It is designed to support apps polling it every UI frame as a result.

> [!NOTE]
> If a local user doesn't have sufficient communications privileges due to their device settings, `xim_player::chat_indicator()` will return `xim_player_chat_indicator::platform_restricted`. The expectation to meet requirements for the platform is that the app show iconography indicating a platform restriction for voice chat or messaging, and a message to user indicating the issue. One example message we recommend is: "Sorry, you're not allowed to chat right now."

## Muting players

Another best practice is to support muting players. XIM automatically handles system muting initiated by users through player cards, but apps should support game-specific transient muting that can be performed within the game UI via the `xim_player::set_chat_muted()` method. The following example begins muting a remote `xim_player` object pointed to by the variable 'remotePlayer' so that no voice chat is heard and no text chat is received from it:

```cpp
remotePlayer->set_chat_muted(true);
```

The muting takes effect immediately and there is no `xim_state_change` associated with it. It can be undone by calling `xim_player::set_chat_muted()` again with the false value. The following example unmutes a remote `xim_player` object pointed to by the variable 'remotePlayer':

```cpp
remotePlayer->set_chat_muted(false);
```

Mutes remain in effect for as long as the `xim_player` exists, including when moving to a new XIM network with the player. It is not persisted if the player leaves and the same user rejoins (as a new `xim_player` instance).

Players typically start in the unmuted state. If your app wants to start a player in the muted state for gameplay reasons, it can call `xim_player::set_chat_muted()` on the `xim_player` object before finishing processing the associated `xim_player_joined_state_change`, and XIM will guarantee there will be no period of time where voice audio from the player can be heard.

An automatic mute check based on player reputation occurs when a remote player joins the XIM network. If the player has a bad reputation flag, the player is automatically muted. Muting only affects local state and therefore persists if a player moves across networks. The automatic reputation-based mute check is performed once and not re-evaluated again for as long as the `xim_player` remains valid.

## Configuring chat targets using player teams

As stated in the [How XIM works with player teams](#how-xim-works-with-player-teams) section of this document, the true meaning of any particular team index value is up to the app. XIM doesn't interpret them except for equality comparisons with respect to chat target configuration. If the chat target configuration reported by `xim::chat_targets()` is currently `xim_chat_targets::same_team_index_only`, then any given player will only exchange chat communication with another if the two have the same value reported by `xim_player::team_index()` (and privacy/ policy also permit it).

To be conservative and support competitive scenarios, newly created XIM networks are automatically configured to default to `xim_chat_targets::same_team_index_only`. However, chatting with vanquished opponents on the other team may be desirable, for example, in a post-game "lobby". You can instruct XIM to allow everyone to talk to everyone else (where privacy and policy permit) by calling `xim::set_chat_targets()`. The following sample begins configuring all participants in the XIM network to use a `xim_chat_targets::all_players` value:

```cpp
xim::singleton_instance().set_chat_targets(xim_chat_targets::all_players);
```

All participants are informed that a new target setting is in effect when they're provided a `xim_chat_targets_changed_state_change`.

As noted earlier, most XIM network move types will initially assign all players the team index value of zero. This means a configuration of `xim_chat_targets::same_team_index_only` is likely indistinguishable from `xim_chat_targets::all_players` by default. However, players that move to a XIM network using matchmaking will have differing team index values if the matchmaking configuration's `xim_team_matchmaking_mode` value declared two or more teams. You can also call `xim_player::xim_local::set_team_index()` at any time as shown above. If your app is using non-zero team index values through either of these methods, don't forget to manage the current chat targets setting appropriately.

## Automatic background filling of player slots ("backfill" matchmaking)

Disparate groups of players calling `xim::move_to_network_using_matchmaking()` at the same time gives the Xbox Live matchmaking service the greatest flexibility to organize them into new, optimal XIM networks quickly. However, some gameplay scenarios would like to keep a particular XIM network intact, and only matchmake additional players just to fill vacant player slots. XIM supports configuring matchmaking to operate in an automatic background filling mode, or "backfilling", by calling `xim::set_network_configuration()` with a `xim_network_configuration` that has `xim_allowed_player_joins::matchmade` flag set on its `xim_network_configuration::allowed_player_joins` property.

The following example configures backfill matchmaking to try to find a total of 8 players for a no-teams free-for-all (although if 8 aren't found, 2-7 players are also acceptable), using an app-specific game mode constant uint64_t defined by the value MYGAMEMODE_DEATHMATCH that will only match with other players specifying that same value:

```cpp
xim_network_configuration networkConfiguration = *xim::singleton_instance().network_configuration();
networkConfiguration.allowed_player_joins |= xim_allowed_player_joins::matchmade;
networkConfiguration.team_configuration.team_count = 1;
networkConfiguration.team_configuration.min_player_count_per_team = 2;
networkConfiguration.team_configuration.max_player_count_per_team = 8;
networkConfiguration.custom_game_mode = MYGAMEMODE_DEATHMATCH;

xim::singleton_instance().set_network_configuration(&networkConfiguration);
```

This makes the existing XIM network available to devices calling `xim::move_to_network_using_matchmaking()` in the normal manner. Those devices see no behavior change. The participants in the backfilling XIM network will not move, but will be provided a `xim_network_configuration_changed_state_change` signifying backfill turning on, as well as multiple `xim_matchmaking_progress_updated_state_change` notifications when applicable. Any matchmade player will be added to the XIM network using the normal `xim_player_joined_state_change`.

By default, backfill matchmaking remains in progress indefinitely, although it won't try to add players if the XIM network already has the maximum number of players specified by the xim_team_configuration setting. Backfilling can be disabled by setting xim_allowed_player_joins to not allow matchmade. The following example disables backfilling by clearing the xim_allowed_player_joins::matchmade flag while preserving all other existing flags and network configuration settings.

```cpp
xim_network_configuration networkConfiguration = *xim::singleton_instance().network_configuration();
networkConfiguration.allowed_player_joins &= ~xim_allowed_player_joins::matchmade;
xim::singleton_instance().set_network_configuration(&networkConfiguration);
```

A corresponding `xim_network_configuration_changed_state_change` will be provided to all devices, and once this asynchronous process has completed, a final `xim_matchmaking_progress_updated_state_change` will be provided with `xim_matchmaking_status::none` to signify that no further matchmade players will be added to the XIM network.

When enabling backfill matchmaking with a `xim_team_configuration` setting that declares two or more teams, all existing players must have a valid team index that is between 1 and the number of teams. This includes players who have called `xim_player::xim_local::set_team_index()` to specify a custom value or who have joined using an invitation or through other social means (e.g., a call to `xim::move_to_network_using_joinable_xbox_user_id`) and have been added with a default team index value of 0. If any player doesn't have a valid team index, then the matchmaking process will be paused and all participants will be provided a `xim_matchmaking_progress_updated_state_change` with a `xim_matchmaking_status::waiting_for_player_team_index` value. Once all players have supplied or corrected their team index values with `xim_player::xim_local::set_team_index()`, backfill matchmaking will resume. More information can be found in the [How XIM works with player teams](#how-xim-works-with-player-teams) section of this document.

Similarly, when enabling backfill matchmaking with a `xim_network_configuration` structure with the `require_player_roles_and_skill_configuration` field set to true for roles or skill, then all players must have specified a non-null per-player matchmaking configuration. If any player hasn't, then the matchmaking process will be paused and all participants will be provided a `xim_matchmaking_progress_updated_state_change` with a `xim_matchmaking_status::waiting_for_player_roles_and_skill_configuration` value. Once all players have supplied their `xim_player_roles_and_skill_configuration` structures, backfill matchmaking will resume. More information can be found in the [Matchmaking using per-player skill](#matchmaking-using-per-player-skill) and [Matchmaking using per-player role](#matchmaking-using-per-player-role) sections of this document.

## Querying joinable networks

While matchmaking is a great way to connect players together quickly, sometimes it's best to allow players to discover joinable networks using custom search criteria, and select the network they wish to join. This can be particularly advantageous when a game session might have a large set of configurable game rules and player preferences. To do this, an existing network must first be made queryable by enabling `xim_allowed_player_joins::queried` joinability and configuring the network information available to others outside the network through a call to `xim::set_network_configuration()`.

The following example enables `xim_allowed_player_joins::queried` joinability, sets network configuration with a team configuration that allows a total of 1-8 players together in 1 team, an app-specific game mode constant uint64_t defined by the value GAME_MODE_BRAWL, a description "cat and sheep's boxing match", an app-specific map index constant uint32_t defined by the value MAP_KITCHEN and includes tags "chatrequired", "easy", "spectatorallowed":

```cpp
PCWSTR tags[] = { L"chatrequired", L"easy", L"spectatorallowed" };
xim_network_configuration networkConfiguration = *xim::singleton_instance().network_configuration();
networkConfiguration.allowed_player_joins |= xim_allowed_player_joins::queried;
networkConfiguration.team_configuration.team_count = 1;
networkConfiguration.team_configuration.min_player_count_per_team = 1;
networkConfiguration.team_configuration.max_player_count_per_team = 8;
networkConfiguration.custom_game_mode = GAME_MODE_BRAWL;
networkConfiguration.description = L"cat and sheep's boxing match";
networkConfiguration.map_index = MAP_KITCHEN;
networkConfiguration.tag_count = _countof(tags);
networkConfiguration.tags = tags;

xim::set_network_configuration(&networkConfiguration);
```

Other players outside the network can then find the network by calling `xim::start_joinable_network_query()` with a set of filters that match the network information in the previous `xim::set_network_configuration()` call. The following example starts a joinable network query with the game mode filter option that will only query for networks using the app-specific game mode defined by value GAME_MODE_BRAWL:

```cpp
xim_joinable_network_query_filters queryFilters = { 0 };
queryFilters.custom_game_mode_filter = GAME_MODE_BRAWL;

xim::start_joinable_network_query(queryFilters);
```

Here is another example that uses the tag filters option to query for networks having tag "easy" and "spectatorallowed" in their public queryable configuration:

```cpp
PCWSTR tagFilters[] = { L"easy", L"spectatorallowed" };
xim_joinable_network_query_filters queryFilters = { 0 };
queryFilters.tag_filter_count = _countof(tagFilters);
queryFilters.tag_filters = tagFilters;

xim::start_joinable_network_query(queryFilters);
```

Different filter options can also be combined. The following example that uses both the game mode filter option and tag filter option to start a query for networks that both have the app-specific game mode constant GAME_MODE_BRAWL and tag "easy":

```cpp
PCWSTR tagFilters[] = { L"easy" };
xim_joinable_network_query_filters queryFilters = { 0 };
queryFilters.custom_game_mode_filter = GAME_MODE_BRAWL;
queryFilters.tag_filter_count = _countof(tagFilters);
queryFilters.tag_filters = tagFilters;

xim::start_joinable_network_query(queryFilters);
```

If the query operation succeeds, the app will receive a `xim_start_joinable_network_query_completed_state_change` from which the app can retrieve a list of joinable networks. The app will also continuously receive `xim_joinable_network_query_updated_state_change` for additional joinable networks or any changes that happen to the returned list of joinable networks until it is stopped either manually or automatically. The in-progress query can be stopped manually by calling `xim::stop_joinable_network_query()`. It will be stopped automatically when calling `xim::start_joinable_network_query()` to start a new query.

The app can try to join a network in the list of joinable networks by calling `xim::move_to_network_using_joinable_network_information()`. The following example assumes you are trying to join a `xim_joinable_network_information` pointed by pointer 'selectedNetwork' which is not secured by a passcode (so we are passing nullptr to the second parameter):

```cpp
xim::move_to_network_using_joinable_network_information(selectedNetwork, nullptr);
```

When enabling network query with a xim_team_configuration that declares two or more teams, players joined by calling `xim::move_to_network_using_joinable_network_information()` will have a default team index value of 0.