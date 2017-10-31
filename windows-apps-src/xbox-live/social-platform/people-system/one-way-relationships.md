---
title: One Way Relationships
author: KevinAsgari
description: Describes how Xbox Live social platform implements a one way relationship model.
ms.assetid: d5a1d311-6de3-4ccc-ab72-15b243e8c2ef
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, social platform, invite, add friends
localizationpriority: medium
---

# One Way Relationships

Xbox Live has moved toward a one-way relationship model. A user can follow anyone to more easily see what they're up to on Xbox Live, communicate with them and invite them to parties.

This model lets users grow their social graph more quickly and easily, instead of requiring the "other side" to confirm a friend request to establish a connection.

-   [People and People List](#people-and-people-list)
-   [Build your Xbox People Graph with Dev Accounts](#build-your-xbox-people-graph-with-dev-accounts)
-   [Following Other Users](#following-other-users)
-   [Privacy](#privacy)


## People and People List

On Xbox One, Xbox Live now has "People" and a "People List". The People system eliminates the previous 100 limit on Xbox Live; users can add as many people to their list as they want with no practical bound (though some relatively large technical bound certainly exists). Within the People service there is a limit to the number of People a user can have on their list; the exact number has not been established but it is likely in the thousands. Note that this number can change at any time, and is not a "firm" limit like the 100 friends in Xbox 360. No external system should hard code a maximum limit assumption, and because of this the current limit might never be explicitly defined.

The People system also introduces a 1-way relationship model; a user can add anyone to their People list and that person is instantly added without sending an invite that must be accepted. The core reason for moving to a 1-way model is overall simplicity; friend requests present another layer of things for the user to manage and often resulted in a multi-day lag until the other person sees the friend request and accepts it, then the sender has to realize that this has happened.


## Build your Xbox People Graph with Dev Accounts

You can use xbox.com to build an Xbox People graph with your development accounts for testing purposes.

First, send a friend request from DevAccountA:

1.  Go to http://www.xbox.com
2.  Click Sign in. Enter your credentials with DevAccountA.
3.  DevAccountA's profile page will load. Click the green Friends tile.
4.  In the "Find by Gamertag" box, type the gamertag for DevAccountB. Click Find.
5.  Click the Add Friend (+) button in the left column.
6.  Click Close on the confirmation message that appears.

Then, accept the request from DevAccountB:

1.  Sign out of xbox.com and click Sign in again. Enter the credentials for DevAccountB.
2.  DevAccountB's profile page will load. Click the green Messages tile.
3.  In the Friend Requests section, click the Accept button.

Now when you sign in with DevAccountA and invoke ShowSendInvitesAsync(), DevAccountB will appear in the list (and vice versa).

Alternatively, you can create a sample Xbox One application that calls **showProfileCardAsync()** for an arbitrary XUID. Clicking the "Follow" button on the Profile Card that appears when **showProfileCardAsync()** is called will cause the calling user to following the user who's XUID was provided.


## Following Other Users

A side-benefit of moving to the 1-way relationship model is that it opens up new scenarios around "celebrities" on Xbox Live that anyone can add to their People list. For example, a lot of users might add a well-known racing game player to their list so that they can more easily race against them on track times, see updates from that player as they climb leaderboards, or get notified when they create new tracks. The 1-way model also makes it easy to support "follow" type relationships where users subscribe to content such as music playlists from unknown people they discover who might have well-aligned music tastes.


## Privacy

Privacy concerns are often initially raised in the context of the new 1-way relationship model, if anyone can add me to their list then "stalk" me without me knowing, what can they see about me? The model here turns out to be no different than previously on Xbox Live; through the recent players list or simply typing a random Gamertag into Xbox.com anyone can see the information a user allows to be seen by "everyone" in the Xbox Live privacy settings. In the 1-way model, whatever information is scoped to "everyone" through privacy settings is what people get who add someone to their list that hasn't added them back.
