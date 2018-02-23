---
title: Using SmartMatch Matchmaking
author: KevinAsgari
description: Learn how to use Xbox Live SmartMatch to match players in a multiplayer game.
ms.assetid: 10b6413e-51d9-4fec-9110-5e258d291040
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, matchmaking, smartmatch
ms.localizationpriority: low
---

# Using SmartMatch Matchmaking

The following flow chart illustrates the SmartMatch matchmaking process.

![](../../images/multiplayer/Multiplayer_2015_SmartMatch_Matchmaking.png)

## Creating a Match Ticket Session and a Match Ticket

Before starting matchmaking, a matchmaking "scout" sets up a match ticket session to represent the group of people who would like to enter matchmaking together. All users in this group join the session using the **MultiplayerSession.Join Method (String, Boolean, Boolean)**.

Once the ticket session has been created and populated with players, the title submits the session to the matchmaking service using the **MatchmakingService.CreateMatchTicketAsync Method**. This method creates a match ticket that represents the ticket session, and updates the /servers/matchmaking/properties/system/status field in the ticket session to "searching". For more information, see [How to: Create a Match Ticket](multiplayer-how-tos.md).

The response from the match ticket creation method is a **CreateMatchTicketResponse Class** object. The response contains the match ticket ID, a GUID that can be used can be used to cancel matchmaking by deleting the ticket. The response also contains an average wait time for the hopper, which can be used to set user expectations.


## Setting Matchmaking Attributes on the Session and Players

When submitting a session to matchmaking, the title can set attributes that the matchmaking service uses to group the session with other sessions. Attributes can be specified at the ticket level or at the per-member level.


### Setting Matchmaking Attributes at the Match Ticket Level

The title submits attributes at the match ticket level in the *ticketAttributesJson* parameter of the **MatchmakingService.CreateMatchTicketAsync** method. Attributes specified at the ticket level override the same attributes specified at the per-member level.


### Setting Matchmaking Attributes at the Per-member Level

The title specifies per-member attributes on each member within the match ticket session. These are set by calling the **MultiplayerSession.SetCurrentUserMemberCustomPropertyJson Method**, using a property name of "matchAttrs". This call places the attributes in the /members/{index}/properties/custom/matchAttrs field on each player within the ticket session.

The matchmaking process "flattens" per-member each into a single ticket-level attribute, based on the flatten method specified for that attribute in the XDP configuration UI for the hopper.


## Making the Match

With the ticket session and the match ticket set up, the matchmaking service matches the represented ticket session with other ticket sessions representing other groups, and creates or identifies a match target session. The service also creates reservations for the matched players in the target session, and then marks the ticket sessions as matched. MPSD notifies the title of this change to the ticket session.

Now the title must then take steps to initialize the target session in order to confirm that enough players have shown up, and perform quality of service (QoS) checks to ensure that they can connect to one another successfully. If initialization and/or QoS fails, the title marks the ticket session for resubmission to matchmaking so that another group can be found. For more information on the processes, see [Target Session Initialization and QoS](smartmatch-matchmaking.md).

During match activity, the following changes are made to the JSON objects for the session:

-   /servers/matchmaking/properties/system/status field set to "found"
-   /servers/matchmaking/properties/system/targetSessionRef field set to target session
-   /members/{index}/properties/custom/matchAttrs field for each ticket session copied to the /members/{index}/constants/custom/matchmakingResult/playerAttrs field
-   For each player, ticket attributes copied from the ticketAttributes field in the match ticket to /members/{index}/constants/custom/matchmakingResult/ticketAttrs field


## Maintaining the Match Ticket

The matchmaking service uses a snapshot of the ticket session at the time when the match ticket is created for the session. Thus, if any players join or leave the ticket session, the title must use the matchmaking service to delete and recreate the match ticket.


## Reusing the Game Session as a Match Ticket Session

| Important                                                                                                                                                                                                                       |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| It is important to realize that two sessions with *preserveSession* set to Always will not be able to match with each other, since they cannot be combined. The matchmaking flow used by the title should take this fact into consideration. |

A title can reuse an existing game session as a match ticket session to find more players to join a game that's already in progress. To enable this, the title must create the match ticket by calling **CreateMatchTicketAsync** with the *preserveSession* parameter set to **PreserveSessionMode.Always**. The matchmaking service then ensures that the existing session used for the ticket is preserved throughout the matchmaking process, and becomes the resulting target session.


## Deleting the Match Ticket

To delete the match ticket, the title calls **MatchmakingService.DeleteMatchTicketAsync Method**. Deletion of the ticket:

1.  Stops matchmaking for the users in the ticket session.
2.  Updates the /servers/matchmaking/properties/system/status field in the ticket session to "canceled".


## Performing Matchmaking for Games Using Xbox Live Compute

Here are the high-level steps that take place to get a user matchmade into an Xbox Live Compute-based game. A similar flow should apply for games hosted by third parties.
1.  The scout creates a ticket session to represent the group. This session contains a list of potential datacenters, located in the session configuration in /constants/system/measurementServerAddresses. It comes from either the session template if the datacenter list is static, or from the client writing it up at session creation after getting it from Xbox Live Compute first. This session also contains gsiSetId, gameVariantId, and maxAllowedPlayers values in the targetSessionConstants/custom/gameServerPlatform object.
2.  All other clients in the group join the ticket session.
3.  All members of the group download the measurementServerAddresses values from the /constants/system object for the ticket session, ping them using the platform API, and upload an ordered list of preferred datacenters to the session, as defined in /members/{index}/properties/system/serverMeasurements.

| Note                                                                                                                                                                                                                                                                                                     |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| The title can set and retrieve the measurementServerAddresses values from the session using the **MultiplayerSession.SetMeasurementServerAddresses** method and the **MultiplayerSessionConstants.MeasurementServerAddressesJson Property**. |

4.  The scout calls **CreateMatchTicketAsync**, passing in a reference to the ticket session.

| Note                                                                                                                                                                                                         |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| If ticket session objects have mismatched constants, the create ticket method might not succeed. This can be avoided by adding a MUST rule to the hopper, to prevent matching with players who have mismatched constants. |

If the **MatchTicketDetailsResponse.PreserveSession Property** is set to Never, the matchmaking service copies the server measurements from each member into the internal representation of the ticket. It flattens the server measurements of the members of the ticket into a single server measurements collection for the ticket, stored in the internal representation of the ticket as a "special" ticket attribute.

If **MatchTicketDetailsResponse.PreserveSession** is set to Always, the server measurements are not used. Instead, the matchmaking service copies the /properties/system/matchmaking/serverConnectionString value for the session into the internal representation of the ticket, as a serverMeasurements collection of size 1 with latency zero.

5.  The matchmaking service matches the ticket session with others representing other groups, taking the server measurement collections into account. It tries to match the group with other groups that have the same datacenters preferred highly.
6.  Once a matched group has been found, the matchmaking service creates or identifies a target session and adds all the players from the ticket sessions that are matched together. The service writes the final flattened server measurements for the matched group into /properties/system/serverConnectionStringCandidates. It writes the originally submitted server measurements for each newly-added member in the target session to /members/{index}/constants/system/matchmakingResult/serverMeasurements.
7.  All clients perform initialization on the target session as discussed above. However, because the clients will be connecting to Xbox Live Compute, they do not perform QoS with one another to confirm connectivity.
8.  Some or all clients call the **GameServerPlatformService.AllocateClusterAsync Method**. The first one triggers the allocation, while the others receive only an acknowledgment. The method gets the target session from MPSD and chooses a datacenter based on the datacenter preferences in the target session, as well as load and other Xbox Live Compute-specific knowledge.
9.  All clients play.


## See also

[SmartMatch Runtime Operations](smartmatch-matchmaking.md)

[SmartMatch Matchmaking](smartmatch-matchmaking.md)

**Microsoft.Xbox.Services.Matchmaking Namespace**

**Microsoft.Xbox.Services.Multiplayer Namespace**
