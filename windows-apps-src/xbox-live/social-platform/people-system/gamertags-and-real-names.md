---
title: Gamertags and real names
author: KevinAsgari
description: Learn how the Xbox Live service uses real names and gamertags in the people system.
ms.assetid: 65700c2f-5e3b-4154-a5e8-b9732d6eee29
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, people system, social platform, gamertag, real name
localizationpriority: medium
---

# Gamertags and Real Names in the People System

Creating and maintaining a mental map of which real-world user is which Gamertag is already hard enough on Xbox Live where the size of the graph is typically greatly restricted. When social networks are integrated and the legacy 100 limit removed, users will have substantially more people on their list than today and the "which Gamertag is which real-world person" problem becomes significantly exacerbated.

Showing real names is optional, users must opt-into functionality that causes their real name to show. Real names are never shown to everyone on Xbox Live, they are only shown to people the user knows which are expressed either though connections on social networks or users may be able to manually indicate within Xbox Live the people they know who are allowed to see their real name or other more personal information. As with Xbox 360, users are always their Gamertag identity when interacting with random strangers though matchmaking or other experiences; moreover users don't have the option to appear as their real name to strangers.

Users are in control of how they appear in games and can decide that they want others to always see them as their Gamertag in games even if some of those people might have access to their real name information. At the time users opt-into the functionality that allows their real name to show (only to those they know), they are presented with the option to always appear as their Gamertag in games.

Selecting this option means that the name appearing on the screen above their head in the FPS will always consistently be their Gamertag instead of sometimes appearing as their real name to the people they know.

The Xbox Live Profile & Privacy services manage the name settings for titles automatically and take care of figuring out which people should see the real name for a given user and which people should see their Gamertag. This is done based on whether the user has opted-into the real name functionality, the relationship between the calling user and the target user, and privacy settings of both the caller and the target. The Xbox Live Profile service is the owner of display name information and must be called to get the proper elements to display for a list of users. Typically title developers have a list of XUIDs they want to render to the active user, and call the Profile service on behalf of the active user to get display data for each of the XUIDs. The Profile service has 3 key fields of interest with respect to displaying names for users:

 **Gamertag**   
Always the user's Gamertag; generally is not be used as titles must use AppDisplayName or GameDisplayName so that there is display name consistency across Xbox titles.

 **AppDisplayName**   
The string that most non-games should render for the user; this might be the Gamertag or might be the user's real name based on settings, privacy, and the relationship between the caller and the target.

 **GameDisplayName**   
The string that all games should render for the user; again might be the Gamertag or might be a real name; this field could be the Gamertag even though the AppDisplayName is a real name.

As mentioned above users control a setting that allows them to always appear as their Gamertag in games; this is the reason why there is a separate GameDisplayName field. If users have turned this setting on then the AppDisplayName might contain the user's real name, while GameDisplayName still contains the Gamertag, even if the querying friend has access to the user's real name information. As a result, games should always use the GameDisplayName while non-games should normally use the AppDisplayName; an example of an exception to the latter is an app that is a companion to a game experience and wants display consistency with what the user sees in the closely related game. It is up to the title developer to decide if it makes sense for their situation to use the GameDisplayName instead of the AppDisplayName based on the context of their application.

The set of characters that might exist in a Gamertag remains the same as in Xbox 360, but the set of characters that might exist in a real name is unfortunately unbound. Because the name on a user's Microsoft Account (MSA) is likely the real-name that is returned to titles, and MSA's allow any Unicode characters, it is possible that the AppDisplayName or GameDisplayName values from the Profile service return characters that are not included in the glyphs that title developers have in their fonts. Titles minimally need to support the 159 Unicode characters in the rages 0x0000-0x007E (Basic Latin) and 0x00C0-0x00FF (Latin Supplemental), beyond that it is up to titles what characters they include based on regions titles are designed for or released into. Titles must also support the ability to render an empty rectangle character for any Unicode characters returned aren't supported in the title's glyph set. The maximum length of a user's name that is returned from MSA is 195 Unicode characters (including the 0-terminator) at this point in time. This maximum length may change without notice and titles must not rely on it.
