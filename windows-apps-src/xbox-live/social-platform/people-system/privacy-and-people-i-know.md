---
title: Privacy and People I know
author: KevinAsgari
description: Learn about the privacy model of the Xbox Live social platform service.
ms.assetid: 9031ca37-bab7-44b1-ae40-fca7459f5f59
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, social platform, privacy, anonymous
localizationpriority: medium
---

# Privacy and People I know

A core feature of the new People system is that it provides for both anonymous and non-anonymous relationships; users can have real-name friends as well as random Gamertag relationships they've encountered on Xbox Live that they put on their People list. This means there are two different sets of rights that must be supported: one to allow trusted real-world friends to see sensitive information, and a second to enable users to add random Gamertag relationships yet feel safe they won't reveal anything sensitive.

The prior Xbox Live privacy model provided the ability to restrict access to information about an account based on a 3-bucket system; a given setting such as online status could be set to "Everyone", "Friends Only", or "Blocked". For example, think of a 14 year old girl on Xbox Live. She's got a set of her school friends that she completely trusts, and then she's added some random guys she barely knows after playing a few rounds of Halo with them. The previous model meant that everyone on her list could only be given the same level of access; there was no way to let her school friends see her bio or gameplay history without also exposing that to the people she's added that she barely knows; it ends up forcing conservatism with what is shared with others, or hesitation about who is added to the list.

The new privacy model remains structurally the same, but moves to a 4-bucket system: "Everyone", "People on my List", "People I Know", or "Blocked". The "People I Know" bucket contains people the user allows to access real-name information. Privacy rights for "People I Know" are a strict subset of "People on my List"; in other words, granting rights to the latter also gives the rights to the former.
