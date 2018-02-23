---
title: Session template constants
author: KevinAsgari
description: Describes the system constants defined in Xbox Live multiplayer session templates.
ms.assetid: d51b2f12-1c56-4261-8692-8f73459dc462
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, session template
ms.localizationpriority: low
---

# Session template constants

The following tables describe the predefined elements of a multiplayer session template, using the session template version 107.

## system

system constant  | Description | valid values | default value
--|-- | -- | --
version | The version of the session template. | 1 - n | none
maxMembersCount | The number of total session member slots supported for the multiplayer activity. | 1 - 100 for a normal session, 101+ for a large session | 100
visibility | The visibility state of the session, which indicates if other users can see and/or join the session. | private, visible, open | open
inviteProtocol | Setting this constant to "game" enables invitees to receive a toast notification when they are invited to the session. | game, tournamentgame, chat, gameparty | none
reservedRemovalTimeout  | The timeout for a member reservation, in milliseconds. A value of 0  indicates an immediate timeout. If the timeout is null, it is considered infinite. | 0 - n, null | 30000
inactiveRemovalTimeout  | The timeout for a member to be considered inactive, in milliseconds. A value of 0 indicates an immediate timeout. If the timeout is null, it is considered infinite. | 0 - n, null | 0
readyRemovalTimeout | The timeout for a member to be considered ready, in milliseconds. A value of 0 indicates an immediate timeout. If the timeout is null, it is considered infinite. | 0 - n, null | 180000
sessionEmptyTimeout | The timeout for an empty session, in milliseconds. A value of 0 indicates an immediate timeout. If the timeout is null, it is considered infinite. | 0 - n, null | 0
[**capabilities**](#capabilities) | Specifies the capabilities of the session. See the capabilities section below. | n/a | n/a
[**metrics**](#metrics) | Specifies a set of title defined quality of service requirements, such as latency and bandwidth speed, that members in the session must satisfy.  | n/a | n/a
[**memberInitialization**](#memberInitialization) | Specifies the timeouts and initialization requirements that are enforced when new members join the session. See member initialization section below. | n/a | n/a
[**peerToPeerRequirements**](#peerToPeerRequirements) | Specifies the network quality of service requirements for peer to peer mesh connections. See the peer to peer requirements section below. |n/a | n/a
[**peerToHostRequirements**](#peerToHostRequirements) | Specifies the network quality of service requirements for peer to host connections. See the peer to host requirements section below. | n/a | n/a
[**measurementServerAddresses**](#measurementserveraddresses) | Specifies a collection of potential datacenters that are used to determine QoS measurements. See the measurementServerAddresses section below. | n/a | n/a
[**cloudComputePackage**](#cloudComputePackage) | ? | n/a | n/a
[**arbitration**](#arbitration) | Specifies the timeouts for members to submit arbitration results in tournaments. See the cloudComputePackage section below. | n/a | n/a
[**broadcastViewerTitleIds**](#broadcastViewerTitleIds) | Specifies a list of title IDs that should always have read access to the session. See the broadcastViewerTitleIds section below. | n/a | n/a
[**ownershipPolicies**](#ownershipPolicies) | Specifies the policies relating to session ownership. See the OwnershipPolicies section below. | n/a | n/a


## capabilities
Capabilities are boolean values that are optionally set in the session template. If no capabilities are needed, an empty 'capabilities' object should be in the template in order to prevent capabilities from being specified on session creation, unless the title desires dynamic session capabilities.

capability |  description | valid values | default value
-- | -- | -- | -- |
connectivity | Indicates if the session supports peer connectivity. If this value is false, then the session can't enable any metrics and the session members can't set their SecureDeviceAddress. Can't be set on large sessions. | true, false | false
suppressPresenceActivityCheck | If true, turns off presence checks. | true, false | false
gameplay | Indicates whether the session represents actual gameplay, as opposed to setup/menu time like a lobby or matchmaking. If true, then the session is in gameplay mode. | true, false | false
large | Indicates if the session is a large session (more than 100 members). Large sessions are not supported for use with multiplayer manager. | true, false | false
connectionRequiredForActiveMembers | Indicates if a connection is required in order for a member be active. | true, false | false
cloudCompute | Enables clients to request that a cloud compute instance be allocated on behalf of the session. | true, false | false
autoPopulateServerCandidates | Automatically calculate and set 'serverConnectionStringCandidates' from 'serverMeasurements'. This capability can't be set on large sessions. | true, false | false
userAuthorizationStyle | Indicates if the session supports calls from platforms without strong title identity. This capability can't be set on large sessions.</br></br>Setting the `userAuthorizationStyle` capability to `true` defaults the `readRestriction` and `joinRestriction`of the session to `local` instead of `none`. This means that titles must use search handles or transfer handles to join a game session.| true, false | false
crossplay | Indicates that the session supports cross play between PC and Xbox One devices. | true, false | false
broadcast | Indicates that the session represents a broadcast. The name of the session must be the xuid of the broadcaster. Requires the "large" capability. | true, false | false
team | Indicates that the session represents a tournament team. This capability can't be set on 'large' or 'gameplay' sessions. | true, false | false
arbitration | Indicates that the session must be created by a service principal that adds the 'arbitration' server entry. Can't be set on 'large' sessions, but requires 'gameplay'. | true, false | false
hasOwners | Indicates that the session has a security policy based on certain members being owners. | true, false | false
searchable | Indicates that the session can be a target session of a search handle. If the 'userAuthorizationStyle' capability is set, then the 'searchable' capability can't be set if the 'hasOwners' capability is not set. | true, false | false

Example:

```json
"capabilities": {
    "connectivity": true,  
    "suppressPresenceActivityCheck": true,
    "gameplay": true,
    "large": true,
    "connectionRequiredForActiveMembers": true,
    "cloudCompute": true,
    "autoPopulateServerCandidates": true,
    "userAuthorizationStyle": true,
    "crossPlay": true,  
    "broadcast": true,  
    "team": true,   
    "arbitration": true,   
    "hasOwners": true,   
    "searchable": true  
},
```

## metrics
If the `metrics` properties are not specified, they default to the values that are needed to satisfy the quality of service requirements.  
If they are specified, then the values must be sufficient to satisfy the quality of service requirements.
This element is only valid if the session has the `connectivity` capability set.

metric | Description | valid values | default value
-- | -- | -- | --
latency | | true, false | see Description
bandwidthDown | | true, false | see Description
bandwidthUp | | true, false | see Description
custom | | true, false | see Description

Example:
```json
"metrics": {
    "latency": true,
    "bandwidthDown": true,
    "bandwidthUp": true,
    "custom": true
},
```

## memberInitialization
If a `memberInitialization` object is set, the session expects the client system or title to perform initialization following session creation and/or as new members join the session.  
The timeouts and initialization stages are automatically tracked by the session, including QoS measurements if any metrics are set.  
These timeouts override the session's reservation and ready timeouts for members that have 'initializationEpisode' set.  
Can't be specified on large sessions.

element  | Description | valid values | default value
-- | -- | -- | --
joinTimeout | Indicates the number of milliseconds that a member has to join the session. Reservations of users who fail to join are removed.</br>**Note:** The default duration is sufficient for normal title execution, but it may lead to join timeouts if a title is being debugged during the MPSD flow. For these scenarios override and increase this default value for the session.| 0 - n | 10000
measurementTimeout | Indicates the number of milliseconds that a session member has to upload measurements. A member who fails to upload measurements is marked with a failure reason of "timeout".  | 0 - n | 30000
evaluationTimeout | Indicates the number of milliseconds that an external evaluation has to upload measurements. | 0 -n | 5000
externalEvaluation | If true, indicates that the title code performs the evaluation of who an join based on QoS measurements. The multiplayer service does not perform any QoS logic, and the title is responsible for advancing the initialization stage. Titles do not typically need this. | true, false | false
membersNeededToStart | The number of members needed to start the session, for initialization episode zero only. | 1 - maxMembersCount | 1

Example:
```json
"memberInitialization": {
    "joinTimeout": 10000,  
    "measurementTimeout": 30000,  
    "evaluationTimeout": 5000,
    "externalEvaluation": false,
    "membersNeededToStart": 1
},
```


## peerToPeerRequirements

peer to peer network requirements | Description | default value
-- | -- |--
latencyMaximum | The maximum latency, in milliseconds, between any two clients. | 250
bandwidthMinimum | The minimum bandwidth in kilobits per second between any two clients. | 10000

Example:
```json
"peerToPeerRequirements": {
    "latencyMaximum": 250,  
    "bandwidthMinimum": 10000
},
```


## peerToHostRequirements

peer to host network requirements | Description | valid values | default value
-- | -- | -- | --
latencyMaximum | The maximum latency, in milliseconds, for the peer to host connection. | | 250
bandwidthDownMinimum | The minimum bandwidth in kilobits per second for information sent from the host to the peer. | | 100000
bandwidthUpMinimum | The minimum bandwidth in kilobits per second for information sent from the peer to the host. | | 1000
hostSelectionMetric | Indicates which metric is used to select the host. | bandwidthup, bandwidthdown, bandwidth, and latency | latency

Example:
```json
"peerToHostRequirements": {
    "latencyMaximum": 250,
    "bandwidthDownMinimum": 100000,
    "bandwidthUpMinimum": 1000,  
    "hostSelectionMetric": "bandwidthup"
},
```

## measurementServerAddresses
The set of potential server connection strings that should be evaluated. The connection strings must be lower case.
Can't be specified on large sessions.

The connection strings are defined in the following format:

`"<server name>" : {deviceAddress}`

Where the device address is described as follows:

server connection string | Description
-- | --
secureDeviceAddress | The base-64 encoded secure device address of the server

Example:
```json
"measurementServerAddresses": {
    "server farm a": {
        "secureDeviceAddress": "r5Y="
    },
    "datacenter b": {
        "secureDeviceAddress": "rwY="
    }
},
```

## cloudComputePackage
Specifies the properties of the cloud compute package to allocate. Requires that the `cloudCompute` capability is set.

cloud compute property | Description
-- | -- | -- | --
titleId | Indicates the title ID of the cloud compute package to allocate.
gsiSet | Indicates the GSI set of the cloud compute package to allocate.
variant | Indicates the variant of the cloud compute package to allocate.

Example:
```json
"cloudComputePackage": {
    "titleId": "4567",
    "gsiSet": "128ce92a-45d0-4319-8a7e-bd8e940114ec",
    "vaiant": "30ebca60-d96e-4629-930b-6957aa6bfbfa"
},
```

## arbitration
Specifies the timeouts for the arbitration process. Requires that the `arbitration` capability is set. The arbitration start time is defined in a session in the */servers/arbitration/constants/system/startTime* element.

timeout | Description | valid values | default
-- | -- | -- | --
forfeitTimeout | Indicates the time, in milliseconds from the arbitration start time, that a TBD | 0 - n | 60000
arbitrationTimeout | Indicates the time, in milliseconds from the arbitration start time, that the arbitration result times out. This value can't be less than the `forfeitTimeout` value | 0 - n | 300000

Example:
```json
"arbitration": {
    "forfeitTimeout": 60000,
    "arbitrationTimeout": 300000
},
```

## broadcastViewerTitleIds

Specifies an array of the title IDs of the titles that should always have read access to the broadcast session.

Example:
```json
"broadcastViewerTitleIds" : ["34567", "8910"],
```

## ownershipPolicies
Specifies how to handle a session when the last owner leaves the session. Requires that the `hasOwners` capability is set.

ownership policy | Description | valid values | default
-- | -- | -- | --
Migration | Indicates the behavior that occurs when the last owner leaves the session. If the migration policy is set to "endsession", expire the session. If the migration policy is set to "oldest", select the member with the oldest join time to become the new owner of the session. | "oldest", "endsession" | "endsession"

Example:
```json
"ownershipPolicies": {
     "migration": "oldest"
}
```
