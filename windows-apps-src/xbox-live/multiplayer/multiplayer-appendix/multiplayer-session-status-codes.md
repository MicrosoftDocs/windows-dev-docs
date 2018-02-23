---
title: Multiplayer Session Status Codes
author: KevinAsgari
description: Describes the status codes returned from the Xbox Live service when requesting a multiplayer session.
ms.assetid: 4ab320d6-8050-41a9-9f00-faaad3b128fd
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer 2015, status codes, session
ms.localizationpriority: low
---

# Multiplayer Session Status Codes

This topic lists multiplayer status codes concerning sessions.

| Note                                                                                                         |
|---------------------------------------------------------------------------------------------------------------------------|
| The 4xx status codes returning the session always return the entire session, even if the URI points to a session element. |


| Status Code | String              | Content-Type     | Body    | Description |
|----|
| 200         | OK                  | application/json | session | Successfully read (GET) or updated (PUT).                                                                                                                                                                                                                                                                                                             |
| 201         | Created             | application/json | session | Successfully created.                                                                                                                                                                                                                                                                                                                                 |
| 202         | Accepted            | text/plain       | none    | The request was accepted, but has not been completed yet.                                                                                                                                                                                                                                                                                             |
| 204         | No content          |                  |         | On GET for a session, session does not exist. On GET of a session element, the session exists but the element does not. On PUT for a session, the session was deleted as a result of the PUT operation. On PUT or DELETE for a session element, the session existed when the operation began, but either the session or the element no longer exists. |
| 304         | Not modified        |                  |         | On GET with If-None-Match header, the session has not changed.                                                                                                                                                                                                                                                                                        |
| 400         | Bad request         | text/plain       | message | The request is assumed to be invalid on first examination. It is missing a required field or the JSON file is malformed. The body includes additional details.                                                                                                                                                                                        |
| 403         | Forbidden           | text/plain       | message | The request might be valid in some contexts, but is invalid for its context. Authorization has failed.                                                                                                                                                                                                                                                |
|             |                     | application/json | session | The session cannot be updated by the user, but can be read.                                                                                                                                                                                                                                                                                           |
| 404         | Not found           | text/plain       | message | The session cannot be accessed because the URI is invalid; the handle, SCID, or session template cannot be found; a hopper cannot be found; a session element cannot be accessed because the session does not exit; or the element lookup is invalid for the session.                                                                                 |
| 405         | Method not allowed  | text/plain       | message | The request URI is plausible, but the verb is wrong. For example, the request is for a POST operation when a PUT operation is needed.                                                                                                                                                                                                                 |
| 409         | Conflict            | text/plain       | message | The session could not be updated because the request is incompatible with the session. For example, constants in the request conflict with constants in the session or session template, or members other than the caller have been added to or removed from a large session.                                                                         |
| 412         | Precondition failed |                  |         | The If-Match header, or the If-None-Match header (for an operation other than GET), could not be satisfied.                                                                                                                                                                                                                                           |
|             |                     | application/json | session | The If-Match header could not be satisfied on a PUT or DELETE operation for an existing session. The current state of the session is returned along with the current ETag value.                                                                                                                                                                      |
| 429 | Too many requests | application/json | message | The service call was throttled due to exceeding the fine grained rate limiting restrictions. For more information, see [Fine Grained Rate Limiting](../../using-xbox-live/best-practices/fine-grained-rate-limiting.md). |
| 503         | Service unavailable | text/plain       | none    | The service is overloaded and the request should be retried later. This code includes a Retry-After header that the client should honor.                                                                                                                                                                                                              |
