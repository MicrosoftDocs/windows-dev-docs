---
title: Social Manager Memory and Performance
author: KevinAsgari
description: Describes memory and performance considerations when using Xbox Live social manager API manager.
ms.assetid: 2540145e-b8e2-4ab5-9390-65e2c9b19792
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, social manager, people
ms.localizationpriority: low
---
# Social Manager Memory and Performance Overview

## Memory
Social Manager now allows it's allocated persistent memory to be held in title space. A custom memory allocator for use by social manager can be specified by calling `xbox_live_services_settings::set_memory_allocation_hooks()`. This memory allocator hook will may also be used for future large memory allocations that the Xbox Live API (XSAPI) uses.

The worst case for memory allocation by social manager by default should be around 4.75 mb (1). There is some additional overhead that social manager may allocate based on RTA and HTTP updates. If creating an `xbox_social_user_group` from list, each added member will take up an additional 2.43 kb. If a user is added either via the `create_social_social_user_group_from_list`, `update_social_user_group`, or the user adding a friend outside of the title, this may cause a realloc to find contiguous memory space.

(1) The 4.75 mb comes from = 1000 `xbox_social_user` at 2.43 kb each * 2. The 2 is that social manager keeps a double memory buffer.

## Additional User Limits
Social Manager currently has a restriction of 100 added users to the graph. This is due to two problems:

1. The maximum number of RTA subscriptions that a user can have is 1100. If a local user has 1000 friends, that only leaves 100 for additional subscriptions.
2. The max amount of batch size of users that can be sent to PeopleHub is currently around 100.

Social Manager communicates this by not allowing a social user group from list to contain more than 100 users. There is a global limit of 100 total additional users that can be in the system at any time via `create_social_user_group_from_list`.

## Processing Time
Social Manager will have at worst case 1100 users. The performance characteristics of social manager on a Xbox One has a worst case of 0.3 ms to 0.5 ms for `do_work` depending on number of social graphs created. The typical case is 0.01 ms for with no groups created and up to 0.2  ms for a group with 1000 users in it.
