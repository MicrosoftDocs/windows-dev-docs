---
title: POST (/users/xuid({xuid})/deleteuserdata)
assetID: 8be13ff9-5d42-43a1-f2fa-d550d845a552
permalink: en-us/docs/xboxlive/rest/uri-usersxuiddeleteuserdatapost.html
author: KevinAsgari
description: ' POST (/users/xuid({xuid})/deleteuserdata)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/users/xuid({xuid})/deleteuserdata)
Completely resets the reputation data for a test user. For testing only.

  * [Remarks](#ID4EQ)
  * [URI parameters](#ID4E5)
  * [Authorization](#ID4EJB)
  * [Required Request Headers](#ID4E3B)
  * [HTTP status codes](#ID4EHC)
  * [Response body](#ID4EJF)

<a id="ID4EQ"></a>


## Remarks

Calling this API will remove all Feedback items and reputation data from a user. Partners may call this API against any sandbox except Retail. The Enforcement Team may call this API with any Sandbox ID.

The domain for these URIs is `reputation.xboxlive.com`. This URI is always called on port 10443.

<a id="ID4E5"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| xuid| 64-bit unsigned integer| Xbox User ID (XUID) of the user whose data is being deleted.|

<a id="ID4EJB"></a>


## Authorization

For the Retail sandbox, **PartnerClaim** from the Enforcement team.

For all other sandboxes, **PartnerClaim** and **SandboxIdClaim**.

<a id="ID4E3B"></a>


## Required Request Headers

**Content-Type: application/json** and **X-Xbl-Contract-Version** (current version is 101).

<a id="ID4EHC"></a>


## HTTP status codes

The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).

| Code| Reason phrase| Description|
| --- | --- | --- | --- | --- | --- |
| 200| OK| The session was successfully retrieved.|
| 400| Bad Request| Service could not understand malformed request. Typically an invalid parameter.|
| 401| Unauthorized| The request requires user authentication.|
| 404| Not Found| The specified resource could not be found.|
| 500| Internal Server Error| The server encountered an unexpected condition which prevented it from fulfilling the request.|
| 503| Service Unavailable| Request has been throttled, try the request again after the client-retry value in seconds (e.g. 5 seconds later).|

<a id="ID4EJF"></a>


## Response body

None on success; otherwise, a [ServiceError (JSON)](../../json/json-serviceerror.md) document.

<a id="ID4EWF"></a>


## See also

<a id="ID4EYF"></a>


##### Parent

[/users/xuid({xuid})/deleteuserdata](uri-usersxuiddeleteuserdata.md)
