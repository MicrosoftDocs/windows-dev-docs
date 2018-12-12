---
title: Player Stats

description: Intro to Stats 2017
ms.date: 07/02/2018
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, player stats, leaderboards, stats 2017
ms.localizationpriority: medium
---
# Stats 2017

Stats 2017 allows developers to configure stats for individual players signifying progress and prowess in game. These stats are a social tool that will allow players to be more competitive with their friends and the larger title's community, as well as showcasing some of the capabilities and challenges your title has to offer. If you have a racing game with featured statistics like longest drift and best hang time, you can communicate the type of racing game your players can expect. Seeing how players stack against their friends and the community at large will give them more incentive to buy and play your title. Players will see featured statistics on the title's GameHub. Featured Stats may also periodically appear in pinned content blocks that users may add to their Home view.

Stats 2017 operates by accepting stat values as key value pairs from your title for a player and storing that stat value so that it can be displayed at a later time. Stats 2017 is meant to support Xbox Live leaderboard scenarios by saving stats about individual players so they can be compared and ranked on a leaderboard later. Stats 2017 accepts values sent to it with little to no validation so it is up to your title to handle all of the logic which determines the correct value for a stat.

## Configured stats and featured leaderboards

Stats are configured on the [Windows Development Center dashboard](https://developer.microsoft.com/en-us/dashboard/windows/overview). In order to configure stats you must already have a title configured. If you do not have a title configured you can learn how to do so by reading [Create and test a new Creator's title](../get-started-with-creators/create-and-test-a-new-creators-title.md).  The Stats you configure in Partner Center will appear in your title's GameHub as pictured:

The *Feature Stats* are highlighted in yellow in the image below.
![Official Club Page Social Leaderboard](../images/omega/gamehub_featuredstats.png)


On Xbox One, users are able to pin games directly to their Home screen to quickly find relevant information, community driven content, and developer posts. The stats you configure may also appear as a part of pinned Home content. Stats will show the player along with a slightly higher ranked friend, encouraging them to play their way up the leaderboard. Leaderboards in pinned content would appear as highlighted in the following image.

![Halo 5 Pinned Leaderboard](../images/stats/Halo_5_Pinned_Leaderboard.png)

Featured Stats compare in game progress based on configured statistics with other friends who also play the title. This encourages more play time for your title when friends compete for high scores or simply bond over a shared experience. Featured Stat leaderboards are monthly leaderboards which are reset on the first day of the month.

Developers are limited to having no more than 20 featured stats for their title, but there is no requirement for developers to include Featured Stats in their title.

## Further Reading
Learn to configure stats with [Configuring Featured Stats and Leaderboards 2017](../configure-xbl/dev-center/featured-stats-and-leaderboards.md)
Learn how to update stat values with [Updating Stats 2017](player-stats-updating.md)