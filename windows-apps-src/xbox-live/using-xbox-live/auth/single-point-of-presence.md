---
title: Single Point of Presence (SPOP)
author: KevinAsgari
description: Learn how to use Xbox Live Single Point of Presence (SPOP) to ensure that a title is played on only a single device at a time.
ms.assetid: 40f19319-9ccc-4d35-85eb-4749c2cf5ef8
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, spop, single point of presence
ms.localizationpriority: low
---

# Single Point of Presence (SPOP)

## Overview
Single Point of Presence (SPOP), is an Xbox Live enforced condition where a title can only be played on one device at a time. This is enforced for Xbox One XDK and UWP titles on any device.
An Xbox Live title, regardless of the device it is on, can kick a user who is signed into a title on another Xbox One or Windows 10 device.

## How to handle SPOP
SPOP should be handled by the title the same way as any other type of sign out event. The user will always be notified via UI when they do an action that would initiate an SPOP to verify that they would like to cause the title to be disconnected on the other device.

* For XDK titles, the `User::SignOutCompleted` event will trigger when this occurs.
* For UWP titles, they will be notified of the sign out through the `sign_out_complete` handler from the `xbox_live_user` class. See [Authentication for UWP projects](authentication-for-UWP-projects.md) for more detail
