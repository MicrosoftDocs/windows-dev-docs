---
title: Multiplayer session details
author: KevinAsgari
description: Learn about the details of Xbox Live multiplayer sessions.
ms.assetid: 5aeae339-4a97-45f4-b0e7-e669c994f249
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer 2015, session, mpsd
ms.localizationpriority: low
---

# MPSD Session Details

This article contains the following sections:
* Session Overview
* Session Capabilities
* Session Size
* Session User States
* Visibility and Joinability
* Session Timeouts
* Multiple Signed-in Users on a Single Console
* Process Lifecycle Management
* Cleanup of Inactive Sessions
* Session Arbiter

## Session Overview

An MPSD session has a session name and is identified as an instance of a session template, which is a JSON document that provides default settings for the session. The template is part of an [Xbox Developer Portal (XDP)](https://xdp.xboxlive.com) service configuration with a service configuration identifier (SCID), which is a GUID. Service configurations are the developer-facing resources that XDP uses for ingestion, management, and security policy. When a session is accessed through MPSD, principal authorization is performed against the service configuration according to the access policies set by the developer through XDP. Secondary access checks, such as session membership validation, are performed at the session level when the session is loaded after access to the service configuration is authorized.

This topic assumes that your template uses contract version 107, which is the version used by the current MPSD for Xbox One. If you have defined templates based on contract version 105 (identical to 104), you must change these to support version 107. For instructions, see [Common Multiplayer 2015 migration issues](common-issues-when-adapting-multiplayer.md).

| Important                                                                                                                                                                                                                                                      |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Functionality that is set through a template cannot be changed through writes to the MPSD. To change the values, you must create and submit a new template with the necessary changes. Any items that are not set through a template can be changed through writes to MPSD. |

### Session Reference

Each MPSD session is uniquely referred to by a session reference, represented in the multiplayer WinRT API by the **MultiplayerSessionReference Class**. The session reference contains these string values:

-   Service configuration identifier (SCID)
-   Session template name
-   Session name

The session reference maps into the URI for identifying sessions as shown below. In the following example mapping, "authority" is sessiondirectory.xboxlive.com.

```HTTP
https://{authority}/serviceconfigs/{service-config-id}/sessiontemplates/{session-template-name}/sessions/{session-name}
```

### Elements of a Session

Each session contains groups of elements that enforce mutability and security rules, which vary by session element, along with read-only housekeeping information (metadata). This section describes the groups of session elements included in the JSON files to configure your session, and in the JSON file for the template that you choose.

| Note                                                                                                                                                   |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| If you are using WinRT wrappers for an HTTP/REST implementation, your session and template must define JSON objects that precisely reflect the WinRT functionality. |

Inside each of the element groups, there are two inner objects:

-   System objects -- these objects have a fixed schema that is enforced and interpreted by MPSD. They are validated and merged. Since MPSD defines and knows what they mean, it can act upon them. For the full definition of each of the system objects, see the references for the **MultiplayerSession Class** and the references for the **Session Directory URIs**.
-   Custom objects -- these objects are optional, and have no schema. They are used to store metadata concerning a multiplayer game. Since MPSD cannot interpret this data, it is not acted upon. Game data or saved information should be stored in title-managed storage (TMS). For more about TMS, see **Xbox Live Title Storage**.

Here's an example of a custom JSON object:
```JSON
    "custom": {
      "myField1": true,
      "myField2": "string",
      "myField3": 5.5,
      "myField4": { "myObject": null },
      "myField5": [ "my", "array" ]
    }
```

#### Session Constants

Session constants are set only at creation time, by the creator or by the session template. The /constants/system object is used to define constants for the multiplayer system as it is known through the MPSD. The WinRT wrapper associated with this object is represented by the **MultiplayerSessionConstants Class**.

The /constants/system object can define a number of items, including a capabilities object, a metrics object, a managedInitialization (template contract version 104/105) or memberInitialization (contract version 107) object, a peerToPeerRequirements object, a peerToHostRequirements object, and a measurementsServerAddresses object.


#### Session Properties

Use the /properties/system object to define session properties for MPSD. The WinRT wrapper associated with this object is the **MultiplayerSessionProperties Class**. Session properties are writable by session members at any time. Examples of session properties in JSON format are: joinRestriction, initializationSucceeded, and the matchmaking object. For an example of the use of this element group, see [Target Session Initialization and QoS](smartmatch-matchmaking.md).


#### Member Constants

Set the member constants at join time for each session member. The JSON object is /members/{index}/constants/system. The WinRT wrapper class representing a session member is the **MultiplayerSessionMember Class**.


## Member Properties

Member properties are writable only by a session member. They are set in the /members/{index}/properties/system object and reflect the elements of the **MultiplayerSessionMember Class**. Here is an example:

```
    {
      // These flags control the member status and "activeTitle", and are mutually exclusive (it's an error to set both to true).
      // For each, false is the same as not present. The default status is "inactive", i.e. neither present.
      "ready": true,
      "active": false,

      // Base-64 blob, or not present. Empty string is the same as not present.
      "secureDeviceAddress": "ryY=",

      // During member initialization, if any members in the list fail, this member will also fail.
      // Can't be set on large sessions.
      "initializationGroup": [ 5 ],

      // List of the groups I'm in and the encounters I just had.
      // An encounter is a brief interaction with a group. When an encounter is reported, it counts as retroactively joining the group 30 seconds ago and just now leaving.
      // Group names use the session name validation rules (e.g. case-insensitive).
      // On large sessions, groups are used to report who played with who (rather than just session membership). Members
      // that are active in at least one group together at the same time are counted as playing together.
      // Empty lists are the same as no value specified.
      // The set of encounters is a point-in-time property, so it's immediately consumed and will never appear on a response.
      "groups": [ "team-buzz", "posse.99" ],
      "encounters": [ "CoffeeShop-757093D8-E41F-49D0-BB13-17A49B20C6B9" ],

      // Optional list of role preferences the player has specified for role-based game modes.
      // All role names have to match across all members in the session. Role weights are
      // defined from 0-100.
      "RolePreference": { "medic": 75, "sniper": 25, "assault": 50, "support": 100 },

      // QoS measurements by lower-case device token.
      // Like all fields, "measurements" must be updated as a whole. It should be set once when measurement is complete, not incrementally.
      // Metrics can be omitted if they weren't successfully measured, i.e. the peer is unreachable.
      // If a "measurements" object is set, it can't contain an entry for the member's own address.
      "measurements": {
        "e69c43a8": {
          "bandwidthDown": 19342,  // Kilobits per second
          "bandwidthUp": 944,  // Kilobits per second
          "custom": { }
        }

      // QoS measurements by game-server connection string. Like all fields, "serverMeasurements" must be updated as a whole, so it should be set once when measurement is complete.
      // If empty, it means that none of the measurements completed within the "serverMeasurementTimeout".
      "serverMeasurements": {
        "server farm a": {
          "latency": 233  // Milliseconds
        }
      },

      // Subscriptions for shoulder taps on session changes. The "profile" indicates which session changes to tap as well as other properties of the registration like the min time between taps.
      // The subscription is named with a title-generated GUID that is also sent back with the tap as a context ID.
      // Subscriptions can be added and removed individually, without affecting other subscriptions in the "subscriptions" object.
      // To remove a subscription, set its context ID to null.
      // (Like the "ready" and "active" flags, the "subscriptions" data is copied out and maintained internally, so the normal replace-all rule on system fields doesn't apply to "subscriptions".)
      // Can't be set on large sessions.
      "subscriptions": {
        "961dc162-3a8c-4982-b58b-0347ed086bc9": {
          "profile": "party",  // Or "matchmaking", "initialization", "roster", "queuehost", or "queue"
          "onBehalfOfTitleId": "3948320593",  // Optional decimal title ID of the registered channel.  If not set the title ID is taken from the token.
        },
        "709fef70-4638-4b94-905b-24cb02706eb5": null
      }
    }
```

#### Server Elements

Servers are non-users that have joined or been invited into a session. The associated JSON objects are /servers/{server-name}/constants/system and /servers/{server-name}/properties/system. These objects are writable only by servers.

| Note                                                         |
|---------------------------------------------------------------------------|
| The /servers/{server-name}/constants/system object is not currently used. |


### Session Configuration

There are two ways to control the configuration of sessions:

-   Use session templates ingested through XDP or Windows Dev Center.
-   Use calls to the multiplayer and matchmaking WinRT APIs or REST APIs. You must still use a template but the template does not have to contain the values you want to configure. Note that your title cannot override the constants already set in the template.

A separate JSON document is provided to define the session itself. In addition, the developer must implement any WinRT wrapper functionality required for a particular title. The contents of the JSON documents and any WinRT wrapper code must reflect each other precisely, and must reflect the latest template contract version.

The schema for a session is versioned with the session version (major version) and the protocol revision (minor version). The versions are combined into the X-Xbl-Contract-Version header as "100 \* major + minor". For example, a v1.7 title includes the following header on every REST request, assuming the latest template contract version of 107: X-Xbl-Contract-Version: 107.

| Note                                                                                              |
|----------------------------------------------------------------------------------------------------------------|
| It is recommended for most titles (using XSAPI) to use contract version 105, and session template version 107. |


### Session Templates

Each session template is a JSON document, part of the service configuration, that defines the framework for the session being created and provides constants for the new session. For more information, see [MPSD Session Templates](../service-configuration/session-templates.md).

## Session Capabilities

Capabilities are constants in the MPSD session that configure behavior that the MPSD should apply to that session. You most commonly use XDP to set capabilities in the session template. They are set in the /constants/system/capabilities object. If no capabilities are needed, use an empty capabilities object.

| Note                                                                                                       |
|-------------------------------------------------------------------------------------------------------------------------|
| Titles almost never change or access session capabilities using the multiplayer WinRT API or the matchmaking WinRT API. |

Session capabilities are represented by the **MultiplayerSessionCapabilities Class**. They are boolean values that indicate what the session can support:

-   Connectivity
-   Game play
-   Large size
-   Connection required for active members

The **MultiplayerSessionConstants Class** defines the following properties that concern session capabilities:

-   **CapabilitiesConnectivity**
-   **CapabilitiesGameplay**
-   **CapabilitiesLarge**

| Note                                                                                                   |
|---------------------------------------------------------------------------------------------------------------------|
| If the title defines a dynamic session capability, the corresponding property is set to true for session constants. |

## Session Size

The size of an MPSD session is determined by the number of members in that session.


### Maximum Session Size

The maximum size of a session is the maximum number of session members it can accommodate. It is represented by the **MultiplayerSessionConstants.MaxMembersInSession Property**. The maximum member size is set in the /constants/system object.

The maximum session size is between 1 and 100 session members, and defaults to 100 if not set on creation. If the required size is over 100, the session is called a "large" session and is set in a special way.

Setting a maximum size for a session can cause an open slot to appear as full during certain disconnect scenarios. For example, if a player becomes disconnected as a result of a network or power failure, the delay is not immediately reflected in the session. The member is set to inactive using the disconnect detection feature described in [MPSD Change Notification Handling and Disconnect Detection](multiplayer-session-directory.md).

In comparison, a peer mesh that uses a heartbeat to detect a disconnection is often aware of a disconnect within two to three seconds and can open up the player slot immediately. However, the arbiter cannot remove other members.

### Large Sessions

A large MPSD session can have up to 1000 members, but it has some session features disabled, such as getting a list of all members. Session largeness is represented by the **MultiplayerSessionCapabilities.Large Property**. This property is set to true to indicate a large session, and the "large" capability is indicated in the /constants/system/capabilities object. For more information, see [Session Capabilities]().

## Session User States



MPSD defines a user state as the status of a user who has been added to a session. Possible user states are defined by the **MultiplayerSessionStatus Enumeration**. The user also is considered to have a status of "available" before being added to a session.

The **MultiplayerSession.SetCurrentUserStatus Method** can be used to change the session user state. This change can be made for REST by setting /members/{index}/properties/system correctly in the game session JSON document.


### Reserved User State

The user is placed in the Reserved user state when the arbiter has selected the user to fill one of the open slots within the session. In this state, the user has not yet officially accepted the invite to the session or joined the session to begin connecting with peers.


### Active User State

When a user is in the Active state, the title has joined the session on behalf of the user, and the user is actively participating in the session. The user continues in this state as long as he/she is playing the game.

When a title is first launched, it should check to see if the user is already a member of any sessions, typically by checking the session state. If the user is a session member, the title can jump straight into the game, and set any participating local members to user state Active.

A user should remain in the Active state while playing in the session. If a user leaves the session through the in-game UI, the user should be removed from the session with the **MultiplayerSession.Leave Method**. If the user is only temporarily away from the game, as when the title is constrained, the title should keep the user in the Active state for a reasonable amount of time. It is appropriate to change the user state to Inactive if the user has not returned after a title-specified period of time.


### Inactive User State

In the Inactive state, the user is not currently engaged with the game but still has a saved slot in the session. In other words, the user is "not active."

It is the user's own console that has responsibility for setting that user to user state Inactive in the session. The arbiter cannot perform this action. Example scenarios in which a user is put into the Inactive state include:

-   The title receives a Suspending event.
-   The user has been inactive (no input or controller response) for a period of time defined by the title. We recommend two minutes for a competitive multiplayer.
-   The title has been in constrained mode for more than two minutes, or for a period defined by the title. This constrained mode timeout period is the expected amount of time for which a user might be away from the title using a related app or other experience related to the title.
-   The user has been disconnected ungracefully from the session. See [MPSD Change Notification Handling and Disconnect Detection](multiplayer-session-directory.md).

If the title starts and the user state for a particular session member is set to Inactive, the title has been suspended or the user has been inactive for too long in the session. Because the title is launching again, the indication is that the user wants to continue with the game session to which he or she belongs. If the user's state is Active on title launch, this situation is probably due to a network disconnect or another scenario where the title was unable to set the user to Inactive before being interrupted. In both of these cases, your title should attempt to reconnect the user with the game and the other users to continue playing, or remove the user from the session.

### User State When the Session is Over

When the session is over, game play is discontinued. The title must allow all users to remove themselves using the **MultiplayerSession.Leave Method**. The session activities associated with the users are automatically cleared when they leave the session.

## Visibility and Joinability

Session access is controlled at the MPSD level by two settings: session visibility and session joinability. The visibility and joinability recommendations in this topic apply for the most common title scenarios. Titles should follow these settings, if possible, and they should use in-title logic to make the final, authoritative determination as to whether a new player is admitted into a session.


### Session Visibility

Session visibility is represented by a constant that is set at session creation. It is typically defined in the session template and determines which types of users have read and write access to a session. The possible values for session visibility are defined by the **MultiplayerSessionVisibility Enumeration**. The settings permitted for the visibility constant in a JSON file are open, visible, and private.


#### Recommended game session visibility: open

Open game sessions do not require player reservations, which simplifies the invite process. The arbiter does not reserve players in MPSD after an invite has been sent, but only tracks invited players locally. Thus, players can immediately connect to the arbiter and determine whether they should join a session, are rejected, or should wait (if waiting players are supported). The arbiter is the ultimate authority and responds and instructs the new member to either stay in or leave the session.

Using open game session visibility requires the invited player to launch a title and connect to the arbiter before the final decision has been made. It is acceptable to display an error message to the user if a session is full or if an invite has been rejected.

To establish a connection to the arbiter, a secure device address is required. The **MultiplayerSessionProperties.HostDeviceToken Property** is used to find out which session member is the current arbiter of a session, and which secure device address an invited player should use for connection.

### Session Joinability

Session joinability determines which types of users can join a session. It can be set dynamically during a session. The possible values for session joinability are:

-   None (default) -- there are no restrictions on who can join the session.
-   Local -- only local users can join the session.
-   Followed -- only local users and users who are followed by other session members can join the session without a reservation.

A session arbiter can create a private session through the joinability setting. Making joinability either local or followed restricts access to the session and makes it private.

Additionally, the arbiter should keep track of session joinability so that older session invites can be rejected at the host level if needed. For example, if any invited players have not arrived to join a session until the session is already full, the arbiter can instruct the joining players that the session has been locked and they need to leave the session automatically.

## Session Timeouts

Sessions can be changed by timers and other external events. Session timeouts define the periods for which session members can remain in specific states before they are automatically made inactive or removed from the session. MPSD also supports timeouts to manage session lifetime.

| Note                                                                                                                                                                                                                                                           |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Timeout settings are made in /constants/system/timeouts, or within the managed initialization object, for template contract version 104/105. For version 107 or later, the settings are made individually in /constants/system or within the managed initialization object. |

When a timer expires, MPSD does not automatically update the session and notify the arbiter at that instant of any changes. The session and timeout states are only updated immediately before a read or write request is sent. Immediate update ensures that the data returned is the most up to date.

| Note                                                                                                          |
|----------------------------------------------------------------------------------------------------------------------------|
| Session timeouts are not stacked, and only one is applied for a state transition against each session member on an update. |


### Currently Defined Timeouts

This section describes the timeouts that are currently defined by MPSD. All timeouts are specified in milliseconds. A value of 0 is allowed and indicates an immediate timeout. A timeout with no value is considered infinite. Since the timeouts have defaults, you should explicitly specify null for an infinite timeout.
#### evaluationTimeout

This timeout indicates the amount of time for a session member to make and upload the evaluation decision. If no decision is received, the decision counts as a failure. This timeout is placed in the managed initialization object.


#### inactiveRemovalTimeout

This timeout is set for a session member who has joined a session but is not currently engaged in the game. The member is removed from the session after 2 hours, by default.

| Note                                                                      |
|----------------------------------------------------------------------------------------|
| This timeout is designated the inactive timeout for template contract version 104/105. |

In many cases, we recommend setting the inactive timeout to 0, causing any user who is set to the inactive state to be removed immediately from the session and the corresponding slot to be cleared. This behavior is desirable for most competitive multiplayer games so that, if a user has gone inactive or reached an inactive state, a new player can be added quickly. For co-op or other multiplayer designs, you might want your title to allow users more time to reconnect if they are disconnected or not engaged in the title for periods of time. Note that no single solution fits all design scenarios.

#### joinTimeout

This timeout indicates the number of milliseconds that a user has to join the session. Reservations of users who fail to join are removed. This timeout is placed in the managed initialization object.


#### measurementTimeout

This timeout indicates the amount of time a session member has to upload measurements. A member who fails to upload measurements is marked with a failure reason of "timeout". This timeout is placed in the managed initialization object.

| Note                                                                                                                                                                              |
|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| During matchmaking, a 45-second timeout for QoS measurements is enforced. Therefore we recommend the use of a measurement timeout that is less than or equal to 30 seconds during matchmaking. |


#### readyRemovalTimeout

This timeout is set for a session member who has joined the session and is trying to get into the game. This usually means that the shell has joined the user on behalf of the title and the title is being launched. The member is removed from the session and placed in the inactive state after 3 minutes, by default.

| Note                                                          |
|----------------------------------------------------------------------------|
| This timeout is designated the ready timeout for contract version 104/105. |


#### reservedRemovalTimeout

This timeout is set for a session member who has been added to the session by someone else, but has not yet joined the session. The reservation is deleted and the member is considered inactive when the timeout expires. The default value is 30 seconds.

| Note                                                             |
|-------------------------------------------------------------------------------|
| This timeout is designated the reserved timeout for contract version 104/105. |


#### sessionEmptyTimeout

This timeout indicates the number of milliseconds after a session becomes empty when it is deleted. The default value is 0.

| Note                                                                 |
|-----------------------------------------------------------------------------------|
| This timeout is designated the sessionEmpty timeout for contract version 104/105. |


### Session Timeout Example

1.  A session is started with four players.
2.  Two players, A and B, are disconnected due to a power failure. Their status in the game remains Active.
3.  The other two players, C and D, quit properly using the **MultiplayerSession.Leave Method**.
4.  The session remains open, with Players A and B disconnected but still in the Active state.
5.  A few days later, Player A returns and starts the game.
6.  Player A's game checks for sessions that A is a member of (performs a read) and finds the orphaned session from a few days ago.
7.  The session does a presence check against the two players who are still in the session (A and B).
    1.  Because Player A is running the title, the presence check against Player A succeeds, and the player's Active state in the match stays the same.
    2.  Player B is not running the title. Thus, the presence check for Player B fails and the service sets Player B's state to Inactive. At this point, the inactive timeout starts for Player B.

8.  Player A exits the session properly using the **Leave** method.
9.  The inactive timeout expires for Player B, who is removed from the session on the next read or write by anyone.
10. The session now has zero members and is removed from the service.

If the inactive timeout for the example session is set to 0, Player B times out immediately after the presence check in step 7a and is probably removed by the session write. In this case, the session closes without the need of an additional read from or write to the session.


## Multiple Signed-in Users on a Single Console


When multiple users are signed in on the same console, it's possible for some users to be in a game session while other users are not in the session or are not active in the current title. Game invites can also be received and accepted for multiple users, having an impact on game session membership. A title needs to consider these points to be able to handle all session membership scenarios correctly.

In a common scenario, a new player signs in, becomes active in the game, and needs to be added to an existing game session. As with creating a new game session, a title should only add a user when it is appropriate during game play.

With multiple signed-in users, one or more users can also receive invites to another game session. Titles do not need to handle these scenarios in any specific way. Session state and member events notify the title of any updates to the game session and user membership.

To handle multiple signed-in users for an online session, the title subscribes for shoulder taps for all users, using a separate **XboxLiveContext Class** object for each user. The title uses the **MultiplayerSession.ChangeNumber Property** to determine particular changes in the session and ignore duplicate shoulder taps.


## Process Lifecycle Management


Just like a non-multiplayer title, a title in a multiplayer session can encounter title suspension and termination of process lifecycle events. The session arbiter should therefore save session state periodically. In case the arbiter is suspended, the title should attempt arbiter migration and save the game state as appropriate, so that a new arbiter can restore session state. It is then possible for a full multiplayer session to be suspended and resumed later, as long as the session is still valid in MPSD. Only one designated peer, typically the game host, should update the global game state.


### Storage of Game Metadata

A title stores game metadata in the MPSD session. Game metadata is the information needed to display session data and enable the title to find and join the game session. The title stores player-specific metadata in the custom properties section for the session member, for example, player color, preferred player weapon for the session, etc. Session-wide metadata, for example, current map, is stored in the global custom properties section of the MPSD session.


### Storage of Game State

Game state is stored in title-managed storage (TMS), using the **title storage service**. Storage using this location allows a title to migrate the arbiter without permission concerns. See [Migrating an Arbiter](migrating-an-arbiter.md).

| Note                                                                                                               |
|---------------------------------------------------------------------------------------------------------------------------------|
| The title should not attempt to save game state to TMS more frequently than once every 5 minutes, unless it is being suspended. |

## Cleanup of Inactive Sessions

If the sessionEmptyTimeout is set to 0, an MPSD session is automatically deleted when the last player leaves the session. To learn how to prevent an unused sessions from containing players after crash or disconnection, see [MPSD Change Notification Handling and Disconnect Detection.](multiplayer-session-directory.md). Improper handling of unused sessions after crash or disconnect can cause issues when a title is querying sessions for a player.

The recommended way to clean up inactive sessions is to have the title query all sessions for a particular user by calling the **MultiplayerService.GetSessionsAsync Method** and then evaluating the sessions. When it encounters a stale session, the title calls the **MultiplayerSession.Leave Method** for all local players in the session. This call drops the member count to 0 eventually and cleans up the sessions.

## Session Arbiter


Some multiplayer methods should only be called by one client within a game session. This client is one of the consoles participating in the session, called the arbiter, or the host. If at least one session member is in a game, the session should have an arbiter to monitor joins in progress.


### Setting the Arbiter

When it creates a session, the client designates one console as the arbiter. See [How to: Set an Arbiter for an MPSD Session](multiplayer-how-tos.md).


### Saving Session State

As discussed in **Process Lifecycle Management**, the arbiter should save session state periodically. A new arbiter must be able to restore session state in the case of arbiter migration by the title. For more information, see [Migrating an Arbiter](multiplayer-how-tos.md).


### Managing Game Session Members and Joins in Progress

The most important role of the session arbiter is to manage users coming into the game session to play. This includes handling game invites, notifying waiting players, and working with players who quit the game.


#### Receiving Notifications

The arbiter must listen for new players who want to join the game session with the **RealTimeActivityService.MultiplayerSessionChanged Event**.


#### Finding Players to Fill Empty Game Session Slots

The arbiter finds players to fill empty game session slots by one of these operations:   If your title uses a lobby session or another mechanism to allow delayed joins, find new session members using that mechanism.
-   Create another match ticket session.

See also [How to: Fill Open Session Slots During Matchmaking](multiplayer-how-tos.md).


#### Handling Invited Session Members

The arbiter must monitor invited session members and apply a minimum interval between invites to a single user. See also [How to: Send Game Invites](multiplayer-how-tos.md).
