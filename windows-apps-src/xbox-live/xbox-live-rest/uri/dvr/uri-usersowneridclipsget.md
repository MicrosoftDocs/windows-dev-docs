---
title: GET (/users/{ownerId}/clips)
assetID: da972b4e-bc38-66f5-2222-5e79d7c8a183
permalink: en-us/docs/xboxlive/rest/uri-usersowneridclipsget.html
author: KevinAsgari
description: ' GET (/users/{ownerId}/clips)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GET (/users/{ownerId}/clips)
Retrieve list of user's clips.
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.

  * [Remarks](#ID4EX)
  * [URI parameters](#ID4EEB)
  * [Query string parameters](#ID4EPB)
  * [Request body](#ID4EPE)
  * [Required Response Headers](#ID4E1E)
  * [Optional Response Headers](#ID4ENH)
  * [Response body](#ID4EOAAC)
  * [Related URIs](#ID4EABAC)

<a id="ID4EX"></a>


## Remarks

This API enables various ways to list a user's own clips as well as other users' clips that are stored in the service. Several entry points return data from different levels and allow for filtering via query parameters. If the XUID in the claim matches the owner specified in the URI, then the user's own clips are returned after content isolation checks. If the owner in the URI does not match the claim XUID, then the specified user's clips are returned based on privacy checks and content isolation checks against the requesting XUID.

Queries are optimized per user per service configuration id (scid). Specifying further filters or sort orders other than the defaults as specified below can in some circumstances take longer to return. This is more evident for larger sets of videos per user.

There is no batch API for getting multiple users' list within the same API call. The recommended pattern (currently) from the SLS Architects is to query for each user in turn.

<a id="ID4EEB"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| ownerId| string| User identity of the user whose resource is being accessed. Supported formats: "me" or "xuid(123456789)". Maximum length: 16.|

<a id="ID4EPB"></a>


## Query string parameters

| Parameter| Type| Description|
| --- | --- | --- | --- | --- | --- |
| skipItems| 32-bit signed integer| Optional. Return the items starting at N+1 in the collection (i.e., skip N items).|
| continuationToken| string| Optional. Return the items starting at the given continuation token. The continuationToken parameter takes precedence over skipItems if both are provided. In other words, the skipItems parameter is ignored if continuationToken parameter is present. Maximum size: 36.|
| maxItems| 32-bit signed integer| Optional. Maximum number of items to return from the collection (can be combined with skipItems and continuationToken to return a range of items). The service may provide a default value if maxItems is not present and may return fewer than maxItems (even if the last page of results has not yet been returned).|
| order| Unicode character| Optional. Specifies if list is returned in (D)escending (highest value first) or (A)scending (lowest value first) order. Default: D.|
| type| GameClipTypes| Optional. Comma-delimited set of the type of clips to return. Default: All.|
| eventId| string| Optional. Comma-delimited set of eventIDs to filter results by. Default: Null.|
| qualifer| string| Optional. Specifies the order qualifier to be used for getting the clips. <ul><li>created - specifies the clips are returned in order of date into the system</li><li>rating - [Top Rated] - specifies the clips are returned by their rating value</li><li>views - [Most Viewed] - specifies the clips are returned by number of views</li></ul><br/> Maximum size: 12. Default: "created".| 

<a id="ID4EPE"></a>


## Request body

There are no required members for this request.

<a id="ID4E1E"></a>


## Required Response Headers

| Header| Type| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.|
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.|
| Cache-Control| string| Polite request to specify caching behavior.|
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.|
| Retry-After| string| Instructs client to try again later in the case of an unavailable server.|
| Vary| string| Instructs downstream proxies how to cache responses.|

<a id="ID4ENH"></a>


## Optional Response Headers

| Header| Type| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Etag| string| Used for cache optimization. Example: "686897696a7c876b7e".|

<a id="ID4EOAAC"></a>


## Response body

<a id="ID4EUAAC"></a>


### Sample response


```cpp
{
       "gameClips":
       [
           {
               "xuid": "2716903703773872",
               "clipName": "[en-US] Localized Greatest Moment Name",
               "titleName": "[en-US] Localized Title Name",
               "gameClipLocale": "en-US",
               "gameClipId": "6873f6cf-af12-48a5-8be6-f3dfc3f61538-000",
               "state": "Published",
               "dateRecorded": "2013-06-14T01:02:55.4918465Z",
               "lastModified": "2013-06-14T01:05:41.3652693Z",
               "userCaption": "Set by user!",
               "type": "DeveloperInitiated",
               "source": "Console",
               "visibility": "Public",
               "durationInSeconds": 30,
               "scid": "00000000-0000-0012-0023-000000000070",
               "titleId": 354975,
               "rating": 3.75,
               "ratingCount": 245,
               "views": 7453,
               "titleData": "",
               "systemProperties": "",
               "savedByUser": false,
               "achievementId": "AchievementId",
               "greatestMomentId": "GreatestMomentId",
               "thumbnails": [
                   {
                       "uri": "http://localhost/users/xuid(2716903703773872)/scids/00000000-0000-0012-0023-000000000070/clips/6873f6cf-af12-48a5-8be6-f3dfc3f61538-000/thumbnails/large",
                       "fileSize": 637293,
                       "thumbnailType": "Large"
                   },
                   {
                       "uri": "http://localhost/users/xuid(2716903703773872)/scids/00000000-0000-0012-0023-000000000070/clips/6873f6cf-af12-48a5-8be6-f3dfc3f61538-000/thumbnails/small",
                       "fileSize": 163998,
                       "thumbnailType": "Small"
                   }
               ],
               "gameClipUris": [
                   {
                       "uri": "http://localhost/897f65a9-63f0-45a0-926f-05a3155c04fc/GameClip-Original_4000.ism/manifest",
                       "uriType": "SmoothStreaming",
                       "expiration": "2013-06-14T01:10:08.73652Z"
                   },
                   {
                       "uri": "http://localhost/897f65a9-63f0-45a0-926f-05a3155c04fc/GameClip-Original_4000.ism/manifest(format=m3u8-aapl)",
                       "uriType": "Ahls",
                       "expiration": "2013-06-14T01:10:08.73652Z"
                   },
                   {
                       "uri": "http://localhost/users/xuid(2716903703773872)/scids/00000000-0000-0012-0023-000000000070/clips/6873f6cf-af12-48a5-8be6-f3dfc3f61538-000",
                       "fileSize": 88820,
                       "uriType": "Download",
                       "expiration": "2999-12-31T11:59:40Z"
                   }
               ]
           }
       ],
   "pagingInfo":
       {
           "continuationToken": null,
           "totalItems": 1
       }
   }

```


<a id="ID4EABAC"></a>


## Related URIs

The following URI is identical to the primary one in this document, but with an extra path parameter to specify a SCID. Only that user's clips for that SCID will be returned. The requesting user must have access to the requested SCID, otherwise HTTP error 403 will be returned.

   * **/users/{ownerId}/scids/{scid}/clips**

<a id="ID4ENBAC"></a>


## See also

<a id="ID4EPBAC"></a>


##### Parent

[/users/{ownerId}/clips](uri-usersowneridclips.md)
