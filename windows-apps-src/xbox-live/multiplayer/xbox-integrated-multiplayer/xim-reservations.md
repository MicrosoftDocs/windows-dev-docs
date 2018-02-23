---
title: XIM Out-of-band Reservations
author: KevinAsgari
description: Describes how to use Xbox Integrated Multiplayer (XIM) as a dedicated chat solution via out-of-band reservations.
ms.assetid: 0ed26d19-defb-414d-a414-c4877bd0ed37
ms.author: kevinasg
ms.date: 01/28/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, xbox integrated multiplayer, xim, chat
ms.localizationpriority: low
---
# Using XIM as a dedicated chat solution via out-of-band reservations

Most apps use XIM to handle every aspect of getting players together. After all, a focus on assembling all the features needed to support common multiplayer scenarios end-to-end is the reason it's called "Xbox Integrated Multiplayer". However some apps that implement their own multiplayer solutions using dedicated Internet servers would also like the advantages of establishing reliable, low latency, low cost peer-to-peer chat communication. XIM recognizes this need, and supports an extension mode which takes advantage of XIM's simplified peer communication to augment external player management happening outside of the XIM API. Instead of moving into a XIM network through social means or matchmaking, players move using "reservations", guaranteed placeholders for particular users that are exchanged "out-of-band" through the app's external player rendezvous mechanism.

Aside from the move process, XIM networks managed using out-of-band reservations are effectively the same as any other XIM network. All communication functions work identically. However, matchmaking and social discovery API methods are necessarily disabled for XIM networks managed using out-of-band reservations since they would conflict with the app's own external implementation. You can't send invites from such a XIM network, for example.

>XIM is optimized to provide a simple end-to-end solution. Therefore, not all complex topologies or scenarios may be a perfect fit for out-of-band reservations. If you have questions about whether or how to leverage XIM's communication features, contact your Microsoft representative.

The subsequent topics describe how to leverage out-of-band reservations in XIM. Because of the relatively few differences from "standard" XIM usage described in previous sections, some discussion is abbreviated. Familiarity with [Using XIM](using-xim.md) is recommended.

Topics:

1. [Moving to a new out-of-band reservation XIM network](#moving)
1. [Adding players to a XIM network managed using out-of-band reservations](#adding)
1. [Configuring chat targets in a XIM network managed using out-of-band reservations](#targets)
1. [Removing players from a XIM network managed using out-of-band reservations](#remove)
1. [Cleaning up a XIM network managed using out-of-band reservations](#clean)

## Moving to a new out-of-band reservation XIM network

To begin using out-of-band reservations, one of your gathered participants must move into a new XIM network created in this mode. The selection of which participating peer device is up to you. You may already have a concept of game host or server, which is a natural choice for starting the process, but this is not required. We do recommend choosing a device that reports an "Open" network access type to achieve the fastest connectivity setup time. See the `Windows::Networking::XboxLive` platform documentation for more information.

Moving to a XIM network managed through out-of-band reservations is done by initializing XIM and declaring the intended local Xbox User IDs as seen in the standard XIM usage walkthrough, but instead of calling a method like `xim::move_to_new_network()`, call `xim::move_to_network_using_out_of_band_reservation()` with a null reservation string. For example:

```cpp
 xim::singleton_instance().initialize(myServiceConfigurationId, myTitleId);
 xim::singleton_instance().set_intended_local_xbox_user_ids(1, &myXuid);
 xim::singleton_instance().move_to_network_using_out_of_band_reservation(nullptr);
```

The standard `xim_move_to_network_starting_state_change`, `xim_player_joined_state_change`, and `xim_move_to_network_succeeded_state_change` will then be provided over time while processing the state changes in the typical `xim::start_processing_state_changes()` and `xim::finish_processing_state_changes()` loop. For example:

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
             MyHandlePlayerJoined(static_cast<const xim_player_joined_state_change *>(stateChange));
             break;
         }

         case xim_state_change_type::player_left:
         {
             MyHandlePlayerLeft(static_cast<const xim_player_left_state_change *>(stateChange));*
             break;
         }

         ...
     }
 }
 xim::singleton_instance().finish_processing_state_changes(stateChanges);
```

Once the initial device has processed the state changes and had its player(s) successfully moved into the XIM network, it must create reservations for the additional users.

## Adding players to a XIM network managed using out-of-band reservations

XIM networks that are managed using out-of-band reservations always report a value of `xim_allowed_player_joins::out_of_band_reservation` from the `xim::allowed_player_joins()` method; they're closed to all players except those with spots reserved for their Xbox User IDs by calling `xim::create_out_of_band_reservation()`. `xim::create_out_of_band_reservation()` takes an array of users, so you can create such reservations for your externally gathered players all at once or over time. Also, users that already have players participating in the XIM network are ignored, so you can also provide additional Xbox User IDs as a complete replacement set or as delta changes, whichever is convenient. The following example assumes you already have your fully gathered set of Xbox User IDs string pointers into an array variable 'xboxUserIds' with 'xboxUserIdCount' number elements:

```cpp
 xim::singleton_instance().create_out_of_band_reservation(xboxUserIdCount, xboxUserIds);
```

This begins the asynchronous process of creating a reservations for the specified Xbox User IDs. When the operation completes, XIM will provided a `xim_create_out_of_band_reservation_completed_state_change` that reports success or failure. If successful, a reservation string will be made available for your system to provide to those Xbox User IDs provided to the operation. Reservation strings created successfully are valid for only a certain amount of time. That time is returned within `xim_create_out_of_band_reservation_completed_state_change`.

With a valid reservation string, your "out-of-band" external mechanism used to gather the players outside of XIM can be used to distribute the string to those enumerated. For example, if you're using the Multiplayer Session Directory (MPSD) Xbox Live service, you can write this string as a custom property in the session document (note: the reservation string will always contain only a limited set of characters that are safe for use in JSON without any need for escaping or Base64 encoding).

Once the other users have their reservation strings, they can then begin moving to the XIM network using it as the parameter to `xim::move_to_network_using_out_of_band_reservation()`. The following example assumes the reservation string has been extracted to a variable named 'reservationString'.

```cpp
xim::singleton_instance().move_to_network_using_out_of_band_reservation(reservationString);

```

The move operations executes asynchronously just like for the initial device that specified a null pointer for the reservation string. State changes `xim_move_to_network_starting_state_change`, `xim_player_joined_state_change`, and `xim_move_to_network_succeeded_state_change` will be generated by the move. When the move is successful, both local and remote players will be added. Existing devices on the XIM network will be provided a `xim_player_joined_state_change` for these new players. At this point, voice and text chat communication is automatically enabled among the players on these different devices in this XIM network (where privacy and policy permit).

Should the move operation fail because of an invalid reservation string, XIM will return a `xim_network_exited_state_change` with the reason `xim_network_exit_reason::invalid_reservation`. This can happen due to several reasons:

1. The title tries to call `xim::move_to_network_using_out_of_band_reservation()` on the remote device before the `xim_create_out_of_band_reservation_completed_state_change` is returned on the host device
1. The title tries to call `xim::move_to_network_using_out_of_band_reservation()` on the remote device after the reservation has expired.
1. The title tries to call `xim::move_to_network_using_out_of_band_reservation()` on the remote device multiple times while only calling `xim::create_out_of_band_reservation()` once.

Regardless of success or failure, reservations are consumed by move operations that use them. Therefore, after each use attempt, the host should generate a new reservation string by calling `xim::create_out_of_band_reservation()` again.

## Configuring chat targets in a XIM network managed using out-of-band reservations

XIM networks that are managed using out-of-band reservations behave identically to traditional XIM networks with respect to chat communication. To support competitive scenarios, newly created XIM networks are automatically configured to only support chat with other players that are members of the same team; that is, the configured value reported by `xim::chat_targets()` by default is `xim_chat_targets::same_team_index_only`. This can be changed to allow everyone to talk to everyone else (where privacy and policy permit) by calling `xim::set_chat_targets()`. The following example begins configuring everyone in the XIM network to use a `xim_chat_targets::all_players` value:

```cpp
xim::singleton_instance().set_chat_targets(xim_chat_targets::all_players);
```

All participants are informed that a new target setting in effect when they're provided a `xim_chat_targets_changed_state_change`.

Every player in XIM networks managed using out-of-band reservations is initially configured with the same team index value, zero. This means a configuration of `xim_chat_targets::same_team_index_only` is indistinguishable from `xim_chat_targets::all_players` by default. However, you can change a local player's team index at any time by calling `xim_player::xim_local::set_team_index()`. The following example configures a player pointer 'localPlayer' to have a new team index value of one:

```cpp
 localPlayer->local()->set_team_index(1);
```

All devices are informed that the player has a new team index value in effect when they're provided a xim_player_team_index_changed_state_change for that player. If the chat target configuration is currently xim_chat_targets::same_team_index_only, then other players with that same new team index will begin hearing voice and being provided text chat (privacy and policy permitting) from the changing player and vice versa. Players with the old team index will stop exchanging such chat communication. If the chat target configuration is currently xim_chat_targets::all_players, then team index has no impact on who can chat with whom.

## Removing players from a XIM network managed using out-of-band reservations

You're managing the roster of players externally when using out-of-band reservations, so naturally you may need to remove players from the XIM network. The typical way is for apps to leverage the same game host that was used to create the XIM network and subsequent reservations originally to also manage player removal, and do so by calling `xim::kick_player()`. This removes the specified player and all players on that same device from the XIM network. The following example assumes the app has determined that it wants to remove the `xim_player` object pointed to by the 'playerToRemove' variable:

```cpp
xim::singleton_instance().kick_player(playerToRemove);
```

The applicable player (or players) will be provided any necessary `xim_player_left_state_change` for every player and a `xim_network_exited_state_change` indicating that they have been kicked from the network. Alternatively, each player can call `xim::leave_network()` to exit on their own for the same effect.

Be aware that XIM peer-to-peer communication may make its own determination at any time that a player has left the XIM network due to environmental difficulties. Your app should be prepared to handle an "unsolicited" `xim_player_left_state_change` and reconcile any discrepancy between XIM's state and your external player management scheme in a way appropriate for your particular app.

## Cleaning up a XIM network managed using out-of-band reservations

Any players that haven't already been kicked from the XIM network and want to return to the state as if only xim::initialize() and `xim::set_intended_local_xbox_user_ids()` had been called, can begin doing so using the `xim::leave_network()` method:

```cpp
xim::singleton_instance().leave_network();
```

This begins asynchronously disconnecting from the other participants. This will cause the remote devices to be provided a `xim_player_left_state_change` for the local player(s), and the local device will be provided a `xim_player_left_state_change` for each player, local or remote. When all graceful disconnection has finished, a final `xim_network_exited_state_change` will be provided. The app can then call `xim::cleanup()` to free all resources and return to the uninitialized state:

```cpp
 xim::singleton_instance().cleanup();
```

Invoking `xim::leave_network()` and waiting for the `xim_network_exited_state_change` in order to exit a XIM network gracefully is always highly recommended when a `xim_network_exited_state_change` has not already been provided. Calling `xim::cleanup()` directly may cause communication performance problems for the remaining participants while they're forced to time out messages to the device that simply "disappeared".
