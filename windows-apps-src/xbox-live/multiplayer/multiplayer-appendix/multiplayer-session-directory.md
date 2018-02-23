---
title: Multiplayer Session Directory
author: KevinAsgari
description: Learn about the Xbox Live Multiplayer Session Directory (MPSD).
ms.assetid: a9b029ea-4cc1-485a-8253-e1c74184f35e
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, mpsd, multiplayer session directory.
ms.localizationpriority: low
---

# Multiplayer Session Directory (MPSD)

This article contains the following sections:

* MPSD Overview
* MPSD Change Notification Handling and Disconnect Detection
* MPSD Handles to Sessions
* Synchronization of Session Updates
* Calling MPSD
* MPSD Session Templates
* Multiplayer Session Explorer

## Session Overview

### What is MPSD?

Multiplayer session directory (MPSD) is a service operating in the Xbox Live cloud that centralizes a game's multiplayer system metadata across multiple clients. It is wrapped by the **MultiplayerService Class**.

MPSD allows titles to share the basic information needed to connect a group of users. It ensures that session functionality is synchronized and consistent. It coordinates with the shell and console operating system in sending/accepting invites and in being joined via the gamer card.


### MPSD Sessions

An MPSD session is represented by the **MultiplayerSession Class** as the scenario in which one or more people use a game. A session is stored by MPSD as a secure JSON document residing in the Xbox Live cloud. Specifically, an MPSD session has the following characteristics:   Is created and managed by titles.

-   Has a unique URI. For more information, see **Session Directory URIs**.
-   Enables connectivity among users, called session members.
-   Stores data useful for enabling game play, for example, per-member attributes, game settings, bootstrapping information, and game server information.

MPSD supports several session variations for use in setting up multiplayer games. Every session contains players' Xbox user identifiers (XUIDs) and secure device association address data.

-   Game session, used as the pattern for game play. A game session can be peer-to-peer, peer-to-host, peer-to-server, or a hybrid of these types.
-   Ticket session, a helper session used to track the state of a match during matchmaking. It is often also a lobby session, and can sometimes be a game session. See [SmartMatch Matchmaking](smartmatch-matchmaking.md).
-   Target session, a helper session created during matchmaking to represent the matched game play. It is almost always also a game session. See [SmartMatch Matchmaking](smartmatch-matchmaking.md).
-   Lobby session, a helper session used to accommodate invited players who are waiting to join a game session. Many titles create both a lobby session and a game session. For more information, see **Managing players in your title**.

## MPSD Change Notification Handling and Disconnect Detection

MPSD enables clients to connect to it using the real-time activity service web socket. The connection is used to:

-   Send down brief notifications (shoulder taps) when session changes occur, based on event subscriptions that titles initiate.
-   Detect user disconnections.
-   Set users as inactive and subsequently remove them from the session, based on disconnect detection.


### Making User Connections

The XSAPI library manages the connection between the client and MPSD. The title first calls the **MultiplayerService.EnableMultiplayerSubscriptions Method**. This method tells XSAPI that the client intends to use a real-time activity connection for multiplayer purposes. Then, when the title makes its first call to the **MultiplayerService.WriteSessionAsync Method** or the **MultiplayerService.WriteSessionByHandleAsync Method**, with the current user set to the Active state, a connection is created and hooked up to MPSD.

| Note                                                                                                                         |
|-------------------------------------------------------------------------------------------------------------------------------------------|
| To enable session notifications and detect disconnections the session template has to set the connectionRequiredForActiveMembers to true. |

| Note                                                                                                                                                                                                                                                                                                                            |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| In former versions of XSAPI, titles called the **RealTimeActivityService.ConnectAsync Method** to create user connections to the real-time activity service. For the 2015 Multiplayer, this method does nothing, and the connection is created on demand. |

### Subscribing for Session Changes

MPSD uses a "shoulder tap" as a lightweight notification that something of interest has changed. The title should retrieve the modified resource to determine the exact nature of the change. With subscriptions enabled, the title can subscribe for shoulder taps on session changes with a call to the **MultiplayerSession.SetSessionChangeSubscription Method**. See [How to: Subscribe for MPSD Session Change Notifications](multiplayer-how-tos.md).


### Handling Shoulder Taps

When a change to a session matches the title's subscription for that session, MPSD notifies the title of the change using the **RealTimeActivityService.MultiplayerSessionChanged Event**. The title should then retrieve the session and compare the retrieved version of the session with the previous cached view, and take actions appropriately.


### Handling Notifications of Connection State Changes

The title can also be notified about changes in the health of the connection to MPSD. Two events signal these changes:   ** RealTimeActivityService.MultiplayerSubscriptionsLost Event ** -- fired when the title's connection to MPSD using the real-time activity service is lost. When this event occurs, the title should shut down the multiplayer.
-   ** RealTimeActivityService.ConnectionStateChanged Event ** -- fired upon a temporary change in the health of the title's connection to the real-time activity service. The title is not required to take any action upon receiving this event, but it might be useful to use the event for diagnostic purposes.


### Disconnecting Clients

Clients for your title disconnect from MPSD when the title disables notifications with a call to the **RealTimeActivityService.DisableMultiplayerSubscriptions Method**. Shortly after this call, the **MultiplayerSubscriptionsLost** event fires, indicating that a client has disconnected from MPSD.

| Note                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| In earlier multiplayer versions, titles called the ** RealTimeActivityService.Disconnect Method ** to disconnect from the real-time activity service. For the 2015 Multiplayer, this method does nothing. The disconnection occurs automatically after **DisableMultiplayerSubscriptions** is called, if there are no users of the web socket connection, for example, a real-time activity service subscription to presence. |


### Disconnect Detection

MPSD uses its disconnect detection feature to quickly find out when a user disconnects ungracefully. An ungraceful disconnect might occur when a player's network fails, or when a title crashes. MPSD changes the disconnected player's state from Active to Inactive, and notifies other session members of the change as appropriate, based on the members' subscriptions to the session.

## MPSD Handles to Sessions

An MPSD session handle is an abstract and immutable reference to a session that can also contain additional typed data. It is similar to a file handle. All handles have a handle ID (GUID) and a full session reference consisting of service configuration ID (SCID), session template, and session name. A handle cannot be updated, but it can be created, read, and deleted.

| Note                                                                                                                                   |
|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| A handle can point to a session that does not exist. Creating a handle using a nonexistent session name does not cause a new session to be created. |


### Handle Types

2015 Multiplayer supports the handle types described in this section.

#### Invite Handle

An invite handle represents an invitation (invite) to a specific person. Type-specific data includes source person, target person, and context string describing the invite, for example, a specific game mode.

An invite handle grants read-write access to an open session. If the session is closed, the handle grants read-only session access.

| Note                                                |
|------------------------------------------------------------------|
| MPSD can create an invite even if the session is full or closed. |


#### Activity Handle

An activity handle indicates what a user is doing at the moment. The user's activity is represented by the **MultiplayerActivityDetails Class**.

| Note                                                                                                                                                                                                                      |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| An activity handle can also be explicitly deleted, but only by the owning title for a specific user. This deletion is done using the **MultiplayerService.ClearActivityAsync Method**. |


### Creating an Invite Handle

To create an invite handle, the title calls the **MultiplayerService.SendInvitesAsync Method**. The method sends an invite to the specified users in the form of a UI toast that the recipients can act on to accept the invite.


### Creating an Activity Handle

To create an activity handle, the title calls the **MultiplayerService.SetActivityAsync Method**. MPSD sets the new handle ID as the session member's bound activity. If there was a previous bound activity, MPSD deletes the corresponding handle. When the active member becomes inactive or leaves the session, MPSD deletes the bound activity handle.


### Using Handles

The title uses handles when a user accepts an invite (invite handle) and when a user joins a friend's current activity (activity handle). In both of these cases, the title must:

1.  Get the handle ID from the title activation parameters.
2.  Create a local MPSD session object and join it as active.
3.  Write the session, passing in the appropriate handle.

## Synchronization of Session Updates

Because a session is a shared resource that can be created or updated by any of its members, there is the potential for conflicting writes. This can lead to unexpected outcomes, for example, one title can unknowingly overwrite the changes made by another title. MPSD's approach to resolving these conflicts is to support optimistic concurrency and a read-modify-write pattern.

Synchronization of session updates by MPSD use two related high-level implementation patterns:

-   Arbiter updates shared portions of the session. If your implementation involves a single arbiter, you can avoid using synchronized updates for most write operations. The title can avoid synchronization for (1) any update that the arbiter makes to shared portions of the session, unless they are related to communicating the arbiter's identity, and (2) any update that a title makes to the member area within the session.

| Note                                                                                                                                                                                                                                                                                                                                              |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Although the above update types do not need synchronization, it is still important to synchronize any updates to the ** MultiplayerSessionProperties.HostDeviceToken Property **. That property is used to communicate the identity of the arbiter, for example, as part of arbiter migration. |

-   All clients update shared portions of the session. In this case, all updates to shared portions of the session must be synchronized. However, titles can still write to their own member areas without synchronization.


### Session Update Synchronization Using the Multiplayer WinRT API

The following multiplayer WinRT API methods implement optimistic concurrency:

-   **MultiplayerService.WriteSessionAsync Method**
-   **MultiplayerService.WriteSessionByHandleAsync Method**
-   **MultiplayerService.TryWriteSessionAsync Method**
-   **MultiplayerService.TryWriteSessionByHandleAsync Method**

Each write method accepts a **MultiplayerSessionWriteMode Enumeration** value. Passing the value SynchronizedUpdate makes use of optimistic concurrency for updates.

Other values in the enumeration help resolve potential conflicts upon the initial creation of a session. Any write to a portion of the MPSD session that can potentially be written to by another title must use a synchronized update. However, not all writes must be protected.

If your title attempts to write the local session object to MPSD using one of the write session methods and receives an HTTP/412 status code, it should refresh the local copy by issuing a **MultiplayerService.GetCurrentSessionAsync Method** call to get the latest server version of the session before attempting the write again. Otherwise, the local session document continues to contain the bad data and the calls to write the session continue to fail.

| Note                                                                                                                                                                                  |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| If the title is using one of the **TryWrite\*** methods, the updated session is returned in the case of an HTTP/412 status code. This behavior avoids the need to call **GetCurrentSessionAsync**. |

When the title calls one of the write session methods, an updated version of the session might be returned. If it is, the title should switch its local cached copy to this new version in a thread-safe way.


### Session Update Synchronization Using the Multiplayer REST API

MPSD supports optimistic concurrency in session updates through REST functionality by using the HTTP "if-match" header with ETag settings and the read-modify-write pattern. The ETag passed in the write request should be the one that the MPSD returns with the previous read request.

## Calling MPSD

There are two ways for the title to access MPSD in order to use the multiplayer system and matchmaking:
-   Recommended. Use the multiplayer WinRT API, which contains classes that act as wrappers for RESTful functionality. See **Microsoft.Xbox.Services.Multiplayer Namespace**. For SmartMatch matchmaking, use the matchmaking WinRT API, represented by the **Microsoft.Xbox.Services.Matchmaking Namespace**.
-   Use direct standard HTTP calls to the multiplayer and matchmaking REST APIs, included in the **Xbox Live Services RESTful Reference**. The applicable URIs are described in **Session Directory URIs** (for multiplayer) and **Matchmaking URIs** (for matchmaking). Related JSON objects are described in the **JavaScript Object Notation (JSON) Object Reference**.


### Using the Multiplayer WinRT API to Call MPSD

The recommended way to call MPSD is to use the multiplayer WinRT API and the matchmaking WinRT API.

| Note                                                                                             |
|---------------------------------------------------------------------------------------------------------------|
| The XDK samples are written using the multiplayer and matchmaking WinRT APIs and the other elements of XSAPI. |

Use of wrapper code for the underlying REST functionality allows for a more traditional approach to using client-side API methods without having to handle HTTP traffic for each call. Both a binary and a source for XSAPI are shipped with the XDK. You can use the binary directly, or modify the source and incorporate it into your title as needed.


### Using the Multiplayer REST API to Interact with MPSD

The title, or its service, can use standard HTTP calls to the multiplayer REST API and the matchmaking REST API. When using REST functionality directly, the caller issues DELETE, PUT, POST, and GET calls against the session directory URIs for most actions. On a PUT request, the request body is merged into the existing session. If there is no existing session, the request body is used to create a new session, along with the session template stored in [Xbox Developer Portal (XDP)](https://xdp.xboxlive.com). All fields are optional, and only deltas must be specified. Therefore, {} is a valid PUT request with zero deltas.

To perform a hypothetical PUT request that returns the result of the merge without affecting the server's official copy of the session, you can append the query-string "?nocommit=true" to the PUT request.

The requests and responses for the multiplayer and matchmaking REST API methods are JSON documents. For a multiplayer session request structure, see **MultiplayerSessionRequest (JSON)**. An associated response structure is shown in **MultiplayerSession (JSON)**. The response structure frames the session members as a linked list and fills in other read-only properties of the session and its members.


### Querying for Sessions and Session Templates (REST)

Your titles can query for session information at the service configuration and the session template levels. This topic discusses queries that use the multiplayer REST API.


#### Query for Basic Session Information

You can set up queries for basic session information using the session directory and matchmaking URIs. The result of a query is a JSON array of session references, with some session data included inline. By default, a query retrieves up to 100 non-private sessions.

| Note                                                          |
|----------------------------------------------------------------------------|
| Every query must include either a keyword filter, an XUID filter, or both. |


#### Query for Session Templates

To retrieve the list of session templates for the SCID, as well as the details of a specific session template, use the GET method for one of the following URIs:
-   **/serviceconfigs/{scid}/sessiontemplates**
-   **/serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}**


#### Query for Session State

To query for session state, use the GET method for one of these URIs:
-   **/serviceconfigs/{scid}/sessions**
-   **/serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/sessions**

## Multiplayer Session Explorer

Multiplayer Session Explorer is a tool built into MPSD for browsing sessions, session templates, and localization strings. The tool is intended only to be used for development sandboxes.


### Accessing Multiplayer Session Explorer

| Note                                                                                                      |
|------------------------------------------------------------------------------------------------------------------------|
| To use the tool, you must be signed in. Your browsing is limited to sessions that have the signed-in user as a member. |

To access Multiplayer Session Explorer, open Internet Explorer on your Xbox One, press the **View** button, and enter <https://sessiondirectory.xboxlive.com/debug> in the **Address** field.

| Note                                                                                                                                                                                |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| You will receive an HTTP/404 status code if you attempt to access the tool in the RETAIL sandbox. For more about this code, see [Multiplayer Session Status Codes](multiplayer-session-status-codes.md). |


### Using Multiplayer Session Explorer


#### Open the Main Page

1.  Access the main page of the tool. It displays the security context (signed-in user and sandbox) and a list of service configuration IDs (SCIDs) in the sandbox.
2.  Press the **Menu** button to pin this page to Home so that you don't have to type the URI each time.


#### Display Available Sessions and Templates

1.  Click on an SCID in the tool to display a list of sessions in that SCID of which the signed-in user is a member.
2.  On this same page, you can click on the SCID and display the session templates and localization strings in the service configuration for the SCID. These items are ingested through [XDP](https://xdp.xboxlive.com).


#### Display the Full Contents of a Session

In Multiplayer Session Explorer, click on a session name to display the full contents of the corresponding session.

The session as shown by MPSD might differ from the response to a standard GET method for the session's URI for a few reasons:

-   The GET call might be using an older contract version in the X-Xbl-Contract-Version header. Session Explorer always displays the session using the most up-to-date contract version.
-   When a session is requested normally via GET, transformations and side-effects can be triggered, for example, expired timeouts. Session Explorer displays a snapshot of the session as it is stored, without executing any logic, transformations, or side-effects.
-   Since the nextTimer JSON object field is computed at the same time as the side-effects, it is not present on MPSD sessions.

## See also

[Session Overview](mpsd-session-details.md)

[Multiplayer Session Status Codes](multiplayer-session-status-codes.md)

[How to: Update a Multiplayer Session](multiplayer-how-tos.md)

[How to: Join an MPSD Session from a Title Activation](multiplayer-how-tos.md)

[How to: Subscribe for MPSD Session Change Notifications](multiplayer-how-tos.md)

[SmartMatch Matchmaking](smartmatch-matchmaking.md)
