---
title: JavaScript Object Notation (JSON) Object Reference
assetID: 8efcc6f3-d88a-1b15-bcfc-d79a24581b0a
permalink: en-us/docs/xboxlive/rest/atoc-xboxlivews-reference-json.html
author: KevinAsgari
description: ' JavaScript Object Notation (JSON) Object Reference'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# JavaScript Object Notation (JSON) Object Reference
 
JavaScript Object Notation (JSON) is a lightweight, standards-based, object-oriented notation for encapsulating data on the web.
 
Xbox Live Services defines JSON objects that are used in requests to, and responses from, the service. This section provides reference information about each JSON object used with Xbox Live Services.
 
<a id="ID4EHB"></a>

 
## In this section

[Achievement (JSON)](json-achievementv2.md)

&nbsp;&nbsp;An Achievement object (version 2).

[ActivityRecord (JSON)](json-activityrecord.md)

&nbsp;&nbsp;A formatted and localized string about one or more users' rich presence.

[ActivityRequest (JSON)](json-activityrequest.md)

&nbsp;&nbsp;A request for information about one or more users' rich presence.

[AggregateSessionsResponse (JSON)](json-aggregatesessionsresponse.md)

&nbsp;&nbsp;Contains aggregated data for a user's fitness sessions.

[BatchRequest (JSON)](json-batchrequest.md)

&nbsp;&nbsp;An array of properties with which to filter presence information, such as users, devices, and titles.

[DeviceEndpoint (JSON)](json-deviceendpoint.md)

[DeviceRecord (JSON)](json-devicerecord.md)

&nbsp;&nbsp;Information about a device, including its type and the titles active on it.

[Feedback (JSON)](json-feedback.md)

&nbsp;&nbsp;Contains feedback information about a player.

[GameClip (JSON)](json-gameclip.md)

[GameClipsServiceErrorResponse (JSON)](json-gameclipsserviceerrorresponse.md)

&nbsp;&nbsp;An optional part of the response to the /users/{ownerId}/scids/{scid}/clips/{gameClipId}/uris/format/{gameClipUriType} API.

[GameClipThumbnail (JSON)](json-gameclipthumbnail.md)

&nbsp;&nbsp;Contains the information related to an individual thumbnail. There can be multiple sizes per clip, and it is up to the client to select the proper one for display.

[GameClipUri (JSON)](json-gameclipuri.md)

[GameMessage (JSON)](json-gamemessage.md)

&nbsp;&nbsp;A JSON object defining data for a message in a game session's message queue.

[GameResult (JSON)](json-gameresult.md)

&nbsp;&nbsp;A JSON object representing data that describes the results of a game session.

[GameSession (JSON)](json-gamesession.md)

&nbsp;&nbsp;A JSON object representing game data for a multiplayer session.

[GameSessionSummary (JSON)](json-gamesessionsummary.md)

&nbsp;&nbsp;A JSON object representing summary data for a game session.

[GetClipResponse (JSON)](json-getclipresponse.md)

&nbsp;&nbsp;Wraps the game clip.

[HopperStatsResults (JSON)](json-hopperstatsresults.md)

&nbsp;&nbsp;A JSON object representing the statistics for a hopper.

[InitialUploadRequest (JSON)](json-initialuploadrequest.md)

&nbsp;&nbsp;The body of a POST GameClip upload request.

[InitialUploadResponse (JSON)](json-initialuploadresponse.md)

[inventoryItem (JSON)](json-inventoryitem.md)

&nbsp;&nbsp;The core inventory item represents the standard item on which an entitlement can be granted.

[LastSeenRecord (JSON)](json-lastseenrecord.md)

&nbsp;&nbsp;Information about when the system last saw a user, available when the user has no valid DeviceRecord.

[MatchTicket (JSON)](json-matchticket.md)

&nbsp;&nbsp;A JSON object representing a match ticket, used by players to locate other players through the multiplayer session directory (MPSD).

[MediaAsset (JSON)](json-mediaasset.md)

&nbsp;&nbsp;The media assets associated with the achievement or its rewards.

[MediaRecord (JSON)](json-mediarecord.md)

[MediaRequest (JSON)](json-mediarequest.md)

[MultiplayerActivityDetails (JSON)](json-multiplayeractivitydetails.md)

&nbsp;&nbsp;A JSON object representing the **Microsoft.Xbox.Services.Multiplayer.MultiplayerActivityDetails**.

[MultiplayerSessionReference (JSON)](json-multiplayersessionreference.md)

&nbsp;&nbsp;A JSON object representing the **MultiplayerSessionReference**. 

[MultiplayerSessionRequest (JSON)](json-multiplayersessionrequest.md)

&nbsp;&nbsp;The request JSON object passed for an operation on a **MultiplayerSession** object.

[MultiplayerSession (JSON)](json-multiplayersession.md)

&nbsp;&nbsp;A JSON object representing the **MultiplayerSession**. 

[PagingInfo (JSON)](json-paginginfo.md)

&nbsp;&nbsp;Contains paging information for results that are returned in pages of data.

[PeopleList (JSON)](json-peoplelist.md)

&nbsp;&nbsp;Collection of [Person](json-person.md) objects.

[PermissionCheckBatchRequest (JSON)](json-permissioncheckbatchrequest.md)

&nbsp;&nbsp;Collection of PermissionCheckBatchRequest objects.

[PermissionCheckBatchResponse (JSON)](json-permissioncheckbatchresponse.md)

&nbsp;&nbsp;The results of a batch permission check for a list of permission values for multiple users.

[PermissionCheckBatchUserResponse (JSON)](json-permissioncheckbatchuserresponse.md)

&nbsp;&nbsp;The reasons of a batch permission check for list of permission values for a single target user.

[PermissionCheckResponse (JSON)](json-permissioncheckresponse.md)

&nbsp;&nbsp;The results of a check from a single user for a single permission setting against a single target user.

[PermissionCheckResult (JSON)](json-permissioncheckresult.md)

&nbsp;&nbsp;The results of a check from a single user for a single permission setting against a single target user.

[Person (JSON)](json-person.md)

&nbsp;&nbsp;Metadata about a single Person in the People system.

[PersonSummary (JSON)](json-personsummary.md)

&nbsp;&nbsp;Collection of [Person (JSON)](json-person.md) objects.

[Player (JSON)](json-player.md)

&nbsp;&nbsp;Contains data for a player in a game session.

[PresenceRecord (JSON)](json-presencerecord.md)

&nbsp;&nbsp;Data about the online presence of a single user.

[Profile (JSON)](json-profile.md)

&nbsp;&nbsp;The personal profile settings for a user.

[Progression (JSON)](json-progression.md)

&nbsp;&nbsp;The user's progression toward unlocking the achievement.

[Property (JSON)](json-property.md)

&nbsp;&nbsp;Contains property data provided by the client for matchmaking request criteria.

[QueryClipsResponse (JSON)](json-queryclipsresponse.md)

&nbsp;&nbsp;Wraps the list of return game clips along with paging information for the list.

[quotaInfo (JSON)](json-quota.md)

&nbsp;&nbsp;Contains quota information about a title group.

[Requirement (JSON)](json-requirement.md)

&nbsp;&nbsp;The unlock criteria for the Achievement and how far the user is toward meeting them.

[ResetReputation (JSON)](json-resetreputation.md)

&nbsp;&nbsp;Contains the new base Reputation scores to which a user's existing scores should be changed.

[Reward (JSON)](json-reward.md)

&nbsp;&nbsp;The reward associated with the achievement.

[RichPresenceRequest (JSON)](json-richpresencerequest.md)

&nbsp;&nbsp;Request for information about which rich presence information should be used.

[ServiceError (JSON)](json-serviceerror.md)

&nbsp;&nbsp;Contains information about an error returned when a call to the service failed.

[ServiceErrorResponse (JSON)](json-serviceerrorresponse.md)

&nbsp;&nbsp;When a service error is encountered, an appropriate HTTP error code will be returned. Optionally, the service may also include a ServiceErrorResponse object as defined below. In production environments, less data may be included.

[SessionEntry (JSON)](json-sessionentry.md)

&nbsp;&nbsp;Contains data for a fitness session.

[TitleAssociation (JSON)](json-titleassociation.md)

&nbsp;&nbsp;A title that is associated with the achievement.

[TitleBlob (JSON)](json-titleblob.md)

&nbsp;&nbsp;Contains information about a title from storage.

[TitleRecord (JSON)](json-titlerecord.md)

&nbsp;&nbsp;Information about a title, including its name and a last-modified timestamp.

[TitleRequest (JSON)](json-titlerequest.md)

&nbsp;&nbsp;Request for information about a title.

[UpdateMetadataRequest (JSON)](json-updatemetadatarequest.md)

&nbsp;&nbsp;The metadata that should be updated for a clip.

[User (JSON)](json-user.md)

&nbsp;&nbsp;Contains user leaderboard data.

[UserClaims (JSON)](json-userclaims.md)

&nbsp;&nbsp;Returns information about the current authenticated user.

[UserList (JSON)](json-userlist.md)

&nbsp;&nbsp;A collection of [User](json-user.md) objects.

[UserSettings (JSON)](json-usersettings.md)

&nbsp;&nbsp;Returns settings for current authenticated user.

[UserTitle (JSON)](json-usertitlev2.md)

&nbsp;&nbsp;Contains user title data.

[VerifyStringResult (JSON)](json-verifystringresult.md)

&nbsp;&nbsp;Result codes corresponding to each string submitted to [/system/strings/validate](../uri/stringserver/uri-systemstringsvalidate.md).

[XuidList (JSON)](json-xuidlist.md)

&nbsp;&nbsp;List of XUIDs on which to perform an operation.
 
<a id="ID4ENH"></a>

 
## See also
 
<a id="ID4EPH"></a>

 
##### Parent 

[Xbox Live Services RESTful Reference](../atoc-xboxlivews-reference.md)

  
<a id="ID4EZH"></a>

 
##### External links [ECMA International Standard 262: ECMAScript Language Specification](http://www.ecma-international.org/publications/files/ECMA-ST/ECMA-262.pdf)

   