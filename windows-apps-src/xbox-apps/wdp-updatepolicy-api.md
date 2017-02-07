---
author: payzer
title: Device Portal Xbox Developer devkit update policy API reference
description: Learn how to programatically set the update policy for your console.
---

NOTE: This API is coming in the next developer preview.

# System Update Policy API reference   
You can use this API to see which update policy is applied to your console and change the update policy to a new one.

IMPORTANT: Most consoles will receive an "access denied" response when trying to call this API. That is because not all development consoles have the ability to change their update policy.

This API affects the update policy for consoles in developer mode, not for retail consoles.

## Get the console update policy

**Request**

You can use the following request to get the update policy of your console.

Method      | Request URI
:------     | :-----
GET | /ext/update/policy
<br />
**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**   
The response is a JSON array containing the console's system update group memberships. Each object has the following fields:   

Id - (String) The ID of the update group.   
Friendly Name - (String) The display name for the update group.   
Description - ("String") The description for the update group.
IsDevKitGroup - (true | false) Indicates whether the update group is for developer builds.
ResourceSetID - (String) Ignore - used by the system update infrastructure.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

## Set a console's system update policy
You can use this API to switch the console's system update group membership.

Note: Consoles may only be in one system update group at a time.

**Request**

You can use the following request to set the system update group membership of a conole.

Method      | Request URI
:------     | :-----
POST | /ext/update/policy
<br />
**URI parameters**

- None

**Request headers**

- None

**Request body**   
The request body is JSON object containing the following fields:   
GroupIdToJoin - (String) The ID of the system update group you want the console to join.  
GroupIdToLeave - (String) The ID of the system update group you want the console to leave.

All fields are required.

The possible GroupIDs are:   
* No update - "b2dbed33-2845-44cc-a7a1-4a9afb29d8d9"   
* Latest production recovery - "7432ae99-8c09-48dd-99f9-a2697499e240"   
* Latest preview recovery - "a8153054-1a1b-47cc-acc9-9aed90c1f8db"    

**Response**   

- None

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

<br />
**Available device families**

* Windows Xbox

