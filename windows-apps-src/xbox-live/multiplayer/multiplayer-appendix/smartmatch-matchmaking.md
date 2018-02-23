---
title: SmartMatch Matchmaking
author: KevinAsgari
description: Learn about the Xbox Live SmartMatch matchmaking service for matching players in a multiplayer game.
ms.assetid: ba0c1ecb-e928-4e86-9162-8cb456b697ff
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, matchmaking, smartmatch
ms.localizationpriority: low
---

# SmartMatch Matchmaking

This article has the following sections
* Introduction to SmartMatch
* SmartMatch Runtime Operations
* Configuring SmartMatch for your Title
* Defining team rules during SmartMatch configuration
* Target Session Initialization and QoS

## Introduction to SmartMatch

The XSAPI provides a matchmaking service, called SmartMatch, that is wrapped by the [Multiplayer Manager API](../multiplayer-manager/multiplayer-manager-api-overview.md).  For advanced API usage, you can refer to the **MatchmakingService Class**, but if you find you have a matchmaking scenario not possible to implement using the Multiplayer Manager, please provide feedback to us via your DAM.  Regardless of which API you use, the conceptual information in this article applies.

SmartMatch matchmaking groups players based on user information and the matchmaking request for the users who want to play together. Matchmaking is server-based, meaning that users provide a request to the service, and they are later notified when a match is found. When building a title for Xbox One, you can use SmartMatch as described in [Using SmartMatch Matchmaking](using-smartmatch-matchmaking.md). alternatively, you can your own matchmaking service as described in [using your own matchmaking service](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/multiplayer-and-networking/using-your-own-matchmaking-service). Note that accessing this link requires that you have a Microsoft dev center account which is enabled for Xbox Live development.

### About SmartMatch

The matchmaking service works closely with MPSD to simplify matchmaking. It allows titles to easily do matchmaking in the background, for example while the user is playing single-player within the title.

Individuals or groups that want to enter matchmaking create a match ticket session, then request the matchmaking service to find other players with whom to set up a match. This results in the creation of a temporary "match ticket" residing within the matchmaking service for a period of time.

The matchmaking service chooses sessions to play together based on configuration, statistics stored for each player, and any additional information given at the time of the match request. The service then creates a match target session that contains all players who have been matched, and notifies the users' titles of the match.

When the target session is ready, titles perform QoS checks to confirm that the group can play together, and then begin play if all is well. During the QoS process and matchmade game play, titles keep the session state up to date within MPSD, and they receive notifications from MPSD about changes to the session. Such changes include users coming and going, and changes to the session arbiter.


### Match Ticket Session

A match ticket session represents the clients for the players who want to make a match. It is created based on a game, or on a group of strangers who are in a lobby together, or on other title-specific groupings of players. In some cases, the ticket session might be a game session already in progress that is looking for more players.


### Match Ticket

Submitting a ticket session to matchmaking results in the creation of a match ticket that tracks the matchmaking attempt. Attributes in the ticket, for example, game map or player level, along with attributes of the players in the ticket session, are used to determine the match.
#### Hoppers

Hoppers are logical places where match tickets are collected. Only tickets within the same hopper can be matched. A title can have multiple hoppers. For example, a title might create one hopper for which player skill is the most important item for matching. It might use another hopper in which players are only matched if they have purchased the same downloadable content.


#### Hopper Rules

Hopper rules provide definitions of the criteria that the matchmaking service uses for deciding on the players to group together. There are two types of rules:   MUST rules -- have to be satisfied for match tickets to be considered compatible.
-   SHOULD rules -- a match ticket matching a rule is favored over one that does not.

Within each of these categories, there are several specific types of rules. For more information, see runtime operation configuration information in **SmartMatch Runtime Operations**.


### Match Target Session

Once a matched group has been found, the service creates a match target session and reserves places for all the players from the ticket sessions that are matched together.


## SmartMatch Runtime Operations



| Note                                                                                                                                  |
|----------------------------------------------------------------------------------------------------------------------------------------------------|
| For information about using SmartMatch matchmaking in your title, see [Using SmartMatch Matchmaking](using-smartmatch-matchmaking.md). |


### Creating a Match Ticket Session and a Match Ticket

A client designated as the matchmaking "scout" sets up a match ticket session to represent a group of players who want to enter matchmaking. All players in this group join the session using the MultiplayerSession.Join method. Once the session is populated with players, the title submits the session to the matchmaking service using the MatchmakingService.CreateMatchTicketAsync method, which creates a match ticket that represents the session.

| Note                                                                                                                                                        |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| The SmartMatch service updates the /servers/matchmaking/properties/system/status field in the ticket session to "Searching" during the CreateMatchTicketAsync operation. |

The response from the match ticket creation method is a CreateMatchTicketResponse object. This response contains the match ticket ID, a GUID that can be used to cancel matchmaking by deleting the ticket. Additionally, an average wait time for the hopper is included in the response to use in setting player expectations.

| Note                                                                                                                                                                                                                                                                                                                                 |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Unless there is specific game stat data that needs to be preserved, it is recommended that titles use the PreserveSessionMode.Never value when calling CreateMatchTicketAsync. Using PreserveSessionMode.Never allows faster matchmaking, and removes the need to have user interface options for hosting a game opposed to searching for a game. |


### Setting Matchmaking Attributes on the Session and Players

When submitting the ticket session, the title sets session and player attributes that the matchmaking service uses to group the session with other sessions. Attributes can be specified at the ticket level or at the per-member level.


### Setting Matchmaking Attributes at the Match Ticket Level

The title submits attributes at the match ticket level in the ticketAttributesJson parameter of the MatchmakingService.CreateMatchTicketAsync method. Attributes specified at the ticket level override the same attributes specified at the per-member level.


#### Setting Matchmaking Attributes at the Per-member Level

The title specifies per-member attributes on each member within the match ticket session. These are set by calling the MultiplayerSession.SetCurrentUserMemberCustomPropertyJson method, using a property name of "matchAttrs". This call places the attributes in the /members/{index}/properties/custom/matchAttrs field on each player within the ticket session.

The matchmaking process "flattens" each per-member into a single ticket-level attribute, based on the flatten method specified for that attribute in the XDP configuration UI for the hopper.


### Making the Match

With the ticket session and the match ticket set up, the matchmaking service matches the represented ticket session with other ticket sessions representing other groups, and creates or identifies a match target session. The service also creates reservations for the matched players in the target session, and then marks the ticket sessions as matched. MPSD then notifies the title of this change to the ticket session.

Now the title must initialize the target session in order to confirm that enough players have shown up, and perform quality of service (QoS) checks to ensure that they can connect to one another successfully. If initialization and/or QoS fails, the title marks the ticket session to be resubmitted to matchmaking so that another group can be found. For more information on this process, see **Target Session Initialization and QoS**.

During match activity, the following changes are made to the JSON objects for the session:

-   /servers/matchmaking/properties/system/status field set to "found".
-   /servers/matchmaking/properties/system/targetSessionRef field set to target session.
-   /members/{index}/properties/custom/matchAttrs field for each ticket session copied to the members/{index}/constants/custom/matchmakingResult/playerAttrs field.
-   For each player, ticket attributes copied from the ticketAttributes field in the match ticket to /members/{index}/constants/custom/matchmakingResult/ticketAttrs field.


### Maintaining the Match Ticket

The matchmaking service uses a snapshot of the ticket session at the time when the match ticket is created for the session. Thus, if any players join or leave the ticket session, the title must use the matchmaking service to delete and recreate the match ticket.


### Filling Spots in an In-Progress Game Session

A title can reuse an existing game session as a match ticket session to find more players to join a game that's already in progress, or fill a game session after a round has completed and some players left. This process is known as "backfill".

One way to perform backfill is to create a match ticket that will "preserve" the in-progress session, by calling **MatchmakingService.CreateMatchTicketAsync Method** with the preserveSession parameter set to PreserveSessionMode.Always. The matchmaking service then ensures that the existing session preserved throughout the matchmaking process, and becomes the resultant target session.

There are potential drawbacks to this pattern, as two sessions with preserveSession set to Always will not be able to match with each other since they cannot be combined. This can result in very slow backfill matchmaking. To avoid this scenario, a title should only use PreserveSessionMode.Always if it requires preservation of game state. Otherwis set PreserveSessionMode.Never and migrate the players to the new targetSession when a match is found.


### Deleting the Match Ticket

To delete the match ticket the title calls **MatchmakingService.DeleteMatchTicketAsync Method**. On deletion of the ticket the matchmaking service:

1.  Stops matchmaking for the users in the ticket session.
2.  Updates the /servers/matchmaking/properties/system/status field in the ticket session to "canceled".


### Matchmaking for Games Using Xbox Live Compute

The following example demonstrates the high level matchmaking using Live Compute services. A similar approach can apply to games hosted on third-party resources.

1.  The 'scout' client creates a ticket session to represent the group. This session contains a list of potential datacenters, located in the session configuration in /constants/system/measurementServerAddresses. It comes from either the session template, if the datacenter list is static, or from the client at session creation after receiving it from the Live Compute service. This session also contains gsiSetId, gameVariantId, and maxAllowedPlayers values in the targetSessionConstants/custom/gameServerPlatform object.
2.  All other clients in the group join the ticket session.
3.  All members of the group download the measurementServerAddresses values from the /constants/system object for the ticket session. Each member then ping each address using the platform API and upload an ordered list of preferred data centers to the session, as defined in /members/{index}/properties/system/serverMeasurements.

| Note                                                                                                                                                                                                                                                                                                                                                                       |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| The title can set and retrieve the measurementServerAddresses values from the session using **MultiplayerSession.SetMeasurementServerAddresses Method** and the **MultiplayerSessionConstants.MeasurementServerAddressesJson Property**. |

4.  The 'scout' client calls **MatchmakingService.CreateMatchTicketAsync Method**, passing in a reference to the ticket session.

| Note                                                                                                                                                                                                  |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| If ticket session objects have mismatched constants, the CreateMatchTicket method may fail. This can be avoided by adding a MUST rule to the hopper and prevent the matching of players with mismatched constants. |

5.  If the **MatchTicketDetailsResponse.PreserveSession Property** is set to Never, the matchmaking service copies the server measurements from each member into the internal representation of the ticket before flattening the collected them into a single collection for the ticket and stored as a "special" ticket attribute.
6.  If **MatchTicketDetailsResponse.PreserveSession Property** is set to Always, the server measurements are not used. Instead, the matchmaking service copies the /properties/system/matchmaking/serverConnectionString value for the session into the internal representation of the ticket, as a serverMeasurements collection of size 1 with latency zero.
7.  The matchmaking service matches the ticket session with others representing other groups, taking the server measurement collections into account. It tries to match the group with other groups that have the same datacenters preferred highly.
8.  Once a matched group has been found, the matchmaking service creates or identifies a target session and adds all the players from the ticket sessions that are matched together. The service writes the final flattened server measurements for the matched group into /properties/system/serverConnectionStringCandidates. It then writes the original server measurements for each new member in the target session to /members/{index}/constants/system/matchmakingResult/serverMeasurements.
9.  All clients perform initialization on the target session as discussed above. However, because the clients will be connecting to Live Compute, they do not perform QoS with one another to confirm connectivity.
10. Some or all clients call **GameServerPlatformService.AllocateClusterAsync Method**. The first one triggers the allocation, while the others receive only an acknowledgement. The method gets the target session from MPSD and chooses a dataceter based on the datacenter preferences in the target session, as well as load and other Live Compute-specific knowledge.
11. All clients play.

## Configuring SmartMatch for your Title


### Configuration of SmartMatch Matchmaking Runtime Operations

All configuration of SmartMatch matchmaking occurs through the [Xbox Developer Portal (XDP)](https://xdp.xboxlive.com). Configuration uses the ServiceConfiguration-&gt;Multiplayer & Matchmaking section for a title.


#### Matchmaking Session Template Configuration

As discussed in [SmartMatch Matchmaking](), there are two types of session related to matchmaking, the match ticket session and the match target session. Basically, a ticket session is the input to the matchmaking service, while the target session is the output. When configuring session templates in XDP, you should create a template for each session type.

For a ticket session, you might use a dedicated template. Alternatively, you can reuse a template for a lobby session or other session not intended to be used for game play.

| Important                                                                                      |
|-------------------------------------------------------------------------------------------------------------|
| The ticket session must not have QoS checks enabled, and must not be marked with the "gameplay" capability. |

For a target session, you must use a template that is intended for matchmade game play. It should have settings that enable QoS checks between peers prior to the start of game play, and must be marked with the "gameplay" capability.

In the XDP UI, you can map each session to one or more hoppers, each containing rules that determine how sessions are matched together in that hopper. For more information, see Basic Hopper Configuration for Matchmaking.


#### Basic Hopper Configuration for Matchmaking

This section defines the fields used to configure basic hopper fields. After this configuration, you must configure the hopper rules, as described in Configuration of Hopper Rules.

![](../../images/multiplayer/session_template_hopper_edit.png)


###### Name

The name of the hopper that is used when submitting a session to matchmaking. This name must match the value passed as a parameter to the **CreateMatchTicketAsync** method during creation of the match ticket.


###### Min/Max Group Size

The minimum and maximum sizes for the player group that is to be created from sessions in the hopper. The matchmaking service attempts to create a matched group that is as large as possible, up to the maximum group size. However, it does create a matched group if it can assemble enough players to meet the minimum group size.


###### Should Rule Expansion Cycles

For a SHOULD rule, the matchmaking service attempts to increase the search space and relax the provided matchmaking rules over time if no successful match is found. This process is performed over multiple cycles, as specified using the Should Rule Expansion Cycles field. Upon the last expansion cycle, the SHOULD rules are dropped so that they no longer prevent tickets from matching. However, they are still used to determine the best match if multiple tickets are available. Only number and QoS types are expanded before they are dropped. See also Configuration of Hopper Rules in this topic.

Increasing the value of the Should Rule Explansion Cycles setting provides more cycles for SHOULD rule expansion, but also increases matchmaking duration. The default value is 3, which is generally sufficient for most configurations.

| Important                                                                                                                                                                        |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Expansion cycles occur at fixed time intervals of 5 seconds. Upon the last expansion cycle, all "Should" rules are no longer taken into account for the remainder of the matchmaking attempt. |

##### Ranked Hopper
Ordinarily SmartMatch will prevent blocked players from being matched. If Ranked Hopper is checked, this logic is bypassed to prevent players from using this system to avoid players of greater skill.

#### Configuration of Hopper Rules

This section defines the fields used to configure rules for a hopper.

###### Common Rule Fields

The fields defined in this section are common to all hopper rules.

**Rule Name**

The friendly name displayed for the rule for configuration purposes.

**Rule Type**

The rule type. Options are MUST and SHOULD. MUST rules have to be satisfied for successful matchmaking. SHOULD rules can be relaxed and/or removed to find a successful match. See Configuration of SHOULD Rule Expansion for more details on this process.


**Data Type**

The data type of the attribute of the matchmaking rule. Possible values are:   Number. Specify a simple 32-bit numerical value.
-   String. Specify a Unicode string of up to 128 characters.
-   Collection. Specify an array of strings. Use this value to identify downloadable content (DLC), squad membership, or role preference for players.
-   Quality of Service. Specify a custom data type for including latency QoS data in matchmaking. Only one such rule should be used per matchmaking hopper.

| Note |
|----------|
| Please contact your Developer Account Manager if this limit is problematic for your title. |

-   Total Value. Specify a custom data type that sums up submitted matchmaking values. You can use this value to ensure that the resulting sum is within a specific range or is an exact value.
-   Team. Specify a custom data type for the teams of players included in matchmaking requests. You can use this value to avoid splitting players within a single match ticket among multiple teams.


###### Data Type-specific Rule Fields

This section defines fields used to define rules that apply to some data types, but not to others. The XDP UI should be able to clarify which data types apply to particular rules.

**Allow Wildcards**

A value that indicates if the attribute can be omitted in the match ticket. If it is omitted, the ticket becomes compatible with any other ticket, regardless of the value for this attribute.


**Attribute Source**

The source of the data type value. Possible sources are:
- Title provided. The data value is submitted in the match ticket.
-   User stat instance. The data value is automatically retrieved from the UserStatistics service.


**Attribute Name**

The name of the attribute value source. It is either the property name in the match ticket or the name of a user statistic.


**Default Value**

The default value for the data type, if no value is specified or available for the matchmaking request. The default value is not applied when the Allow Wildcards field is selected and no value is specified.


**Weight**

The importance of the rule. The weight can be used to indicate which rules are prioritized during matchmaking and rule expansion. The weight value must be positive, and defaults to 1.


**Flatten Method**

Number data types only. A value that indicates how multiple values are combined to satisfy a match. It applies to multiple values for different players in a single match ticket and across multiple tickets. The possible values are:
-  Min/Max. Use the minimum or maximum value of multiple values from different match tickets.
-   Average. Use average value of multiple values from different match tickets.


**Max Diff**

Number data types only. The maximum acceptable numerical difference between two compared values to satisfy a rule. For a SHOULD rule, this value is the starting point for rule expansion.


**Set Operation**

Collection data types only. The operation to perform on matching the group of set values. The possible options are:
- Intersection. Match two collections based on the amount of intersection between them. This setting results in similar or identical collection values.
-   Difference. Match two collections based on the amount of difference between them.
-   Role Preference. Match collections based on the preferences for the role of a player in role-based game modes.


**Target Intersection**

Part of Set Operation configuration. The minimal intersection or maximum difference for two collections before they are matched.


**Network Topology**

Quality of service data type only. The network topology that is used for QoS. Possible values are Peer to Peer, Peer to Host, and Client/Server.


**Maximum Latency/Scaling Maximum**  
Quality of service data type only. The maximum latency for successful matchmaking within the specified network topology. This value is treated as a scaling value (as opposed to a required latency) when using a Client-Server Quality of service should rule.


| Note                                                                                                                                                           |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| In addition, default reputation rules are also applied to a hopper. These rules cannot be removed and are used to ensure correct handling of reputation during matchmaking. |


**Allow Waiting for Roles**

Collection Role Preferences data type only. Specifies if the match service holds the matchmaking ticket in order to fill all available roles.


###### Expansion Delta

Value indicating how much to relax the submitted rule for each expansion generation. The expansion delta is applied in addition to the Max Diff value. See Example 1 (Rule Expansion) for details.

You can also use the expansion delta to expand multiple number values at different speeds. This is not possible through the expansion cycle configuration setting because it applies to all rules. Instead, the approach is to use decimal expansion values, for example, 0.4. An expansion only occurs when a new integer is reached, which allows for different expansion speeds, even for the same number of expansion cycles.


###### QoS Expansion (Peer-to-Peer, Peer-to-Host)
For quality of service type expansion for peer games, the expansion delta cannot be configured. Instead, you should use one of the following expansion strategies.

<table>

<tr>

<tr>
<td>
<b>1. MaxLatency < 256.</b><p/><p/>

Expansion is performed at MaxLatency * Expansion Cycle.<p/>
For example, if the initial value is 200, then 200 is used in the first cycle, 400 in the second cycle, and so on.
</td>
</tr>

<tr>
<td>
<b>2. MaxLatency > or equal to 256</b><p/><p/>

Expansion linearly scales from 50 to MaxLatency - 256.<p/>
For example, if the initial value is 556, the value scales linearly from 50 to 300 over the number of cycles. That is, if six cycles were chosen, then the values would be 50, 100, 150, 200, 250, and 300. If five cycles were chosen, the values would be 50, 112.5, 175, 237.5, and 300.
</td>
</tr>

</tr>
</table>

##### QoS Expansion (Client-Server)
When using dedicated servers, the expansion is based on relative preference. Only most preferred servers will be considered in early expansion cycles. Over time, other, less preferred servers will be used. To ensure appropriate expansion, we require a value similar to MaxLatency, called Scaling Maximum. This Scaling Maximum should still be set to the largest acceptable ping time—however, this value gives a relative scale for the different server ping times provided by a player, as opposed to providing an absolute requirement for ping times. Note that you may intentionally exclude servers with unacceptable ping times by removing them from the list in the request.

#### Example 1 (Rule Expansion)

Player level is used for matchmaking, and players are matched loosely, based on the closeness of their levels. Players with the least amount of difference between their levels are preferred.

-   Player Level Rule
-   Rule Type: SHOULD
-   Data Type: Number
-   Max Diff: 1
-   Expansion Delta: 2
-   Flatten Method: Average

By default, the required difference between player levels is 1 or less. If a match is found within this difference, players are matched. If no initial match is found, the player level value is expanded by 2 for each iteration (three iterations by default). This scenario results in a matchmaking behavior for a player at level 20, as shown in the table below.

| Step                    | Level Value for Potential Match Candidates | Effective Level Distance for Successful Match |
|-----|
| Initial Submitted Value | 19-21                                      | 1                                             |
| Expansion Cycle 1       | 17-23                                      | 3                                             |
| Expansion Cycle 2       | 15-25                                      | 5                                             |
| Expansion Cycle 3       | 13-27                                      | 7                                             |

As the expansion cycles continue, the effective level distance for a successful match increases without altering the Max Diff value. Only the player level value is relaxed.


#### Example 2 (Collection Rule)

The game releases three types of DLC that are available for players. This matchmaking rule is applied to "DLC only" game play matchmaking, and a player should own at least one DLC to be matchmade with other players.

-   Player DLC Rule
-   Rule Type: MUST
-   Data Type: Collection
-   Set Operation: Intersection
-   Target Intersection: 1

Players evaluate their DLCs and submit the values shown in the next table in their match tickets.

| Note                                                                                                                                   |
|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| In the table below, the collection value indicates the ownership of the DLC. It is set to 1 if the DLC is available for the player and to 0 if not. |

| Player   | Collection Value | Matched with Players | Note           |
|----------|------------------|----------------------|----------------|
| Player 1 | { "1", "1", "1"} | 3                    | Owns all DLCs  |
| Player 2 | { "0", "0", "0"} | None                 | Owns no DLC    |
| Player 3 | { "1", "0", "0"} | 1                    | Owns first DLC |

If the target intersection in the example is set to 2, players 1 and 3 will not be matched, since the intersection between them is only 1.

#### Example 3 (Avoid previous players)
The title prefers avoiding a game with the player most recently played.
* Rule Type: MUST
* Data Type: Collection
* Set Operation: Difference
* Target Intersection: 0

## Defining team rules during SmartMatch configuration

### Configuring Team Rules

To set up the Team Rule, begin by creating one in XDP. Fill out the team sizes your game expects to create from the tickets matched in this hopper. For instance, if your game expects 4v4, you should create two entries, expecting a maximum size of 4 each, and a different name. There is a minimum team size as well--use this if a game can be played with fewer players on a team. Otherwise, the minimum and maximum should be the same value.


#### Using Team Rules

Once the Team Rule is configured, tickets within the hopper will be prevented from matching if there is no way to fit their groups into teams without causing a split. The rule will write the resulting team allocation to the target session, under members/constants/custom/matchmakingresult/initialTeam. Note that this is simply a suggested allocation--the title may find that by rearranging the players it may create a better game, while still preventing tickets from splitting into different teams.

If a ticket is created for a game that is in progress, the Team Rule will require additional information. Suppose, for instance, that 8 players are in a 4v4 game, when two players drop or are disconnected. The title would like to fill in those empty spots, but it cannot reshuffle the teams while the game is being played.

Attempts to fill games in progress are represented by tickets with the PreserveSession field set to "always". In cases like this, because teams have already been allocated to players, the title must specify the current team allocation, so Match will know how many spaces are open on each team. To supply the names of the teams each player is on, each player writes their team name into the game session, under members/me/properties/system/groups. This field is a JArray.

Once the above properties are written to the game session, one player creates a ticket for the session, in an attempt to find more players. When the ticket is fulfilled, Match will again write the suggested team of any players who join into members/constants/custom/matchmakingresult/initialTeam


###### Preferring even teams

Additionally, matches will be made with the largest teams first. This means that in a hypothetical 4v4 hopper, tickets of 4 players will be matched together first, until no tickets of 4 remain. Tickets of 3 then continue, pulling singletons as they need to, and so forth. In this manner, tickets of similar size will generally play against each other, if they are present and not prevented by other rules. Note that this gives the Team Rule fairly strong precedence over other rules. For example, suppose you have a limited population, consisting of one ticket of size 4 with high skill(A), one ticket of size 4 with low skill(B), and four tickets of size 1 with high skill(C-F). The Team Rule will cause Match to prefer A and B matching, as opposed to A, C, D, E, and F.


###### Should Variant

The Must rule prevents ticket splitting in all generations, and provides the prefer-even-teams sorting. The Should rule is identical until the last generation--once there, tickets may be split, although the prefer-even-teams sorting will still be active.

## Target Session Initialization and QoS

A group of players is matched into a target session by SmartMatch matchmaking, as described in [SmartMatch Matchmaking](). The title must take steps to confirm that enough players have joined that they can successfully connect to one another if they need to. This process is known as target session initialization.

For games using peer-to-peer network topologies, an important aspect of target session initialization is QoS measurement and evaluation. Associated operations are the measurement of latency and bandwidth between Xbox One consoles (or between consoles and servers), and the evaluation of the resulting measurements to determine whether the network connection between nodes is good.

The following flow chart illustrates how to implement the initialization of the target session and QoS operations.

![](../../images/multiplayer/Multiplayer_2015_Matchmaking_QoS.png)

### Managed Initialization

MPSD supports a feature called "managed initialization" through which it coordinates the target session initialization process across the clients involved in a session. MPSD automatically tracks the initialization stages and the associated timeouts for the session, and evaluates the connectivity among clients if needed. Managed initialization is represented by the **MultiplayerManagedInitialization Class**.

| Note                                                                                                                  |
|------------------------------------------------------------------------------------------------------------------------------------|
| It is highly recommended for your title using SmartMatch matchmaking to take advantage of the MPSD managed initialization feature. |


#### Managed Initialization Episodes and Stages

A target session undergoes managed initialization any time matchmaking adds new players to the session. SmartMatch adds session members as user state Reserved, meaning that each member takes up a slot but has not yet joined the session. Each group of new players triggers a new initialization episode.

Upon completion of initialization, each player either succeeds or fails the process. A player who succeeds in initializing can play using the target session. A player who fails must be resubmitted to matchmaking to be matched into another session. For cases where a session is submitted to matchmaking with the *preserveSession* parameter set to Always, the pre-existing members of the session do not undergo initialization, as MPSD assumes them to be properly set up.

Each managed initialization episode consists of these stages:

-   Joining -- session members write themselves to the session to move their user state from Reserved to Active, and upload basic data, such as secure device address.
-   Measurement -- for peer-based topologies, session members measure QoS to one another, and upload the results to the session.
-   Evaluation -- MPSD evaluates the results of the last two stages, and determines whether the session and/or members have successfully initialized.

The title code operates on the session to advance each user (and therefore the session) through the joining and measurement phases. It then can either start play or go back to matchmaking after the evaluation stage has succeeded or failed.


### Configuring the Target Session for Initialization

The title can configure the managed initialization process using constants in the target session being initialized. These constants are set under /constants/system in the session template with version 107, the recommended template version. Two types of configuration settings can be made: settings that configure the managed initialization process as a whole, and settings that configure QoS requirements. See [MPSD Session Templates](multiplayer-session-directory.md) for examples of session templates for common title scenarios.

| Note                                                                                                                              |
|------------------------------------------------------------------------------------------------------------------------------------------------|
| If QoS requirements are not defined in the target session initialization configuration the measurement stage during initialization is skipped. |


#### Configuring Managed Initialization as a Whole

Below are the fields to set to control managed initialization overall. They are part of the /constants/system/memberInitialization object:
- joinTimeout. Specifies how long MPSD waits for each member to join, after the initialization episode has begun. Default is 10 seconds.
-   measurementTimeout. Specifies how long MPSD waits for each member to upload QoS measurement results, after the measurement stage has begun. Default is 30000 seconds.
-   membersNeededToStart. Specifies the number of members who must succeed at initialization for the first initialization episode to succeed. Default is 1.

| Note                                              |
|----------------------------------------------------------------|
| If this threshold is not met, all members fail initialization. |


#### Configuring QoS Requirements

QoS is only needed during initialization if the title uses a peer-to-peer or peer-to-host topology. Each topology maps to a topology-specific constant under /constants/system/.
###### Configuring QoS Requirements for Peer-to-peer Topology

| Note                                                                                                                                                                                         |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| It is rare for titles to make QoS requirement settings for the peer-to-peer topology. These settings are very restrictive and cause problems for players with strict network address translations (NATs). |

Peer-to-peer topology QOS requirements are set in the peerToPeerRequirements object. Every client must be able to connect to every other client. The object has the following pertinent fields:
-   latencyMaximum. Specifies the maximum latency between any two clients.
-   bandwidthMinimum. Specifies the minimum bandwidth between any two clients.


###### Configuring QoS Requirements for Peer-to-host Topology

Peer-to-host topology QOS requirements are set in the peerToHostRequirements object. Every client must be able to connect to a single common host. If this object is configured and initialization succeeds, MPSD will create an initial list of clients that are potential hosts, known as the "host candidates." Here are the fields to set:
-   latencyMaximum. Specifies the maximum latency between each peer and the host.
-   bandwidthDownMinimum. Specifies the minimum downstream bandwidth between each peer and the host.
-   bandwidthUpMinimum. Specifies the minimum upstream bandwidth between each peer and the host.
-   hostSelectionMetric. Specifies the metric used to select the host.

## See also

[Configuring SmartMatch for your Title]()

[MPSD Session Templates](multiplayer-session-directory.md)

[SmartMatch Runtime Operations]()
