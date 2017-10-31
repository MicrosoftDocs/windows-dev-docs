---
title: Xbox 360 Friends Backward Compatibility
author: KevinAsgari
description: Learn how the Xbox Live social platform provides backwards compatibility with the Xbox 360 Friends system.
ms.assetid: aeca67b0-2e24-4f3b-b8ff-74823ee0fb36
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, friends, xbox 360, social platform, people system
localizationpriority: medium
---

# Xbox 360 Friends Backward Compatibility

The Xbox Live People system hosts both Xbox 360 friends and the new People model within the same service infrastructure, and both are part of one back-end list that is kept for each user. It is not the case that Xbox 360 friends are one-time "copied" into the new People system and then managed completely separately.

## How Xbox 360 Friends Work

Entities contained within the People system are a strict superset of legacy Xbox 360 friends; all Xbox 360 friends are always present within a user's People list, although the People list may contain many more contacts than the 100-contact limit that still exists for legacy clients. The People service is fully backwards-compatible with the previous Xbox 360 friends service with no visible changes to prior callers; the new service is currently live in production handling all Xbox 360 friends-related management today. An app's view of the world is based on the APIs they use; calling legacy APIs returns only legacy Xbox 360 friend relationships, while calling People APIs returns the superset People view of the world.

Using clients that call the new People system, users can add as many one-way relationships as they want. None of these relationships appear in Xbox 360. Adding a new relationship through a Xbox 360 client causes that relationship to appear in a client that calls the new People APIs. This "discrepancy" exists by design so that new clients are not required to build management user experience for the subset of 100 people that might appear in Xbox 360, or to manage the legacy invite flow. New clients are required only to "think" about the world in the new way, and do not require legacy behavior knowledge. A new People client could choose to realize the user has legacy friends on their People list, and then ask at the time of "add" if the user also wants to send a legacy friend invite. This complicates the simplicity of adding people to new clients substantially, and is unlikely to happen. The understood trade-off is dual management of any adds that occur from a People-based client; users must then add the same person through a legacy client. It also means that someone who has only ever used a new client might over time have many People relationships, but have a completely empty friends list if they visit a legacy client.

Removing a friend through Xbox 360 means that the person is also removed from clients that use the new People system. Unlike the asymmetric "add" behavior from above, removing a person through a People system client also removes them from the legacy Xbox 360 friends list. This is required because the worst case scenario around removal is harassment; users on either legacy or new clients who are having real issues with someone should not have to remove the person on both clients. Removal of relationships happens much less frequently than adding new ones, and is purposefully interpreted to mean that the person is removed from all clients. The understood edge case that suffers is when a user is removing someone from Xbox 360 only to free up space in their 100-contact limit, and would prefer to keep the contact on their People list in new clients. Ideally, the Xbox 360 experience would give a user with a nearly-full friends list the option of keeping the relationship in newer clients; in reality, implementing this scenario requires updates to Xbox 360 to make it forward-aware, as well as non-trivial work on behalf of the app to support a clear minority of users.
