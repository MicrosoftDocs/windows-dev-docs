---
title: Achievement Rewards
author: KevinAsgari
description: Describes how you can configure an achievement to deliver rewards.
ms.assetid: b6fc5bdb-ba7b-4687-985e-894182f066da
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, achievement, rewards
ms.localizationpriority: low
---

# Achievement Rewards

The following diagram illustrates how a developer might manage the lifecycle of a title. The new achievement system is designed to elevate our familiar mechanic with much more flexibility—in how achievements are unlocked, in how and when they are added, and in what benefits they deliver to users—that empowers the developer to run the title as a service by adding value and maintaining user engagement over time.

##### Figure 1.   How a title might drive user behavior. #####
![rewarding_achievements](../images/omega/achievements_overview_01_drive_behavior.png)

### Flexible options for rewarding achievement ###
With Xbox One, we have expanded the achievement system to support more flexible reward options. Gamerscore remains as a valuable reward that tracks a single, common gaming score for the user across the Xbox Live ecosystem, but now you—the developer or publisher—can use achievements as a delivery mechanism for a much wider range of rewards, both within your title and outside of your title.

An achievement can be configured with multiple rewards, up to one reward of each reward type. An achievement can also be configured with no explicit reward; in such a case, the achievement's icon acts as a visual badge for the player who acquired the achievement.

Xbox Live supports the following types of rewards:

* Gamerscore
* Art
* In-app rewards

#### Gamerscore ####
We are committed to preserving the integrity of the gamerscore value that has been built up with our Xbox Live users. There is only one gamerscore per user! Any gamerscore that a user earns on existing Xbox Live platforms such as Xbox 360, Xbox One, or Windows 10 will count toward a single gamerscore for that user.

When a user unlocks a gamerscore achievement, Xbox Live automatically increases the user's gamerscore by the configured amount.

There are restrictions on which titles may offer gamerscore as a reward on their achievements. See the policy documents on https://developer.xboxlive.com/ for the latest information.

#### Art ####
Do you have some interesting concept art that your designers drew early in your title's inception phase? Do you have beautiful, high-resolution images that can decorate your hub application when players visit? Perhaps your app supports multiple skins? With the Art reward, you can power lush, beautiful experiences in your titles and beyond that your players must earn.

High-resolution concept art, early design drawings, specially created art assets, and other digital art assets may be offered as a reward to users for unlocking an achievement. These assets may be displayed within the Xbox One Dashboard experience and can be displayed in companion experiences simply by querying the Achievements service to retrieve the pertinent metadata.

#### In-app rewards ####
We are introducing in-app rewards in order to give developers much more flexibility and control over the rewards that an achievement offers. In-app rewards enable you to use achievements to deliver custom in-game rewards directly to your users without necessarily updating your title. You simply configure the achievement reward with a code, ID, or phrase that your title will recognize, and when the user unlocks the achievement, Xbox LIVE will send that code to your title, thereby informing the title of the reward to deliver to the user.

The reward itself is up to you, the developer. Reward ideas include:

* Extra in-game currency or points
* Access to a special character, weapon, or map
* A temporary experience multiplier.

### Configuring in-app rewards ###
Configuring an in-app reward for an achievement is fairly straightforward. The achievement owner must provide a reward name, a reward description, and a reward icon in addition to a reward value. This reward value is determined by the developer and must be something that either the game can interpret and properly handle OR that the user can enter as part of a title-specific reward redemption experience.

An example of a reward value that the game can interpret might be a 5-digit number or a special string which the game or the game's service knows maps to a particular in-game item. Developers may want to leverage the Title Managed Storage (TMS) service to make it easy to add new reward values over time that the game will understand how to read.

An example of a reward value that the user must submit might be a special code or string that the user enters into a redemption experience within the title, within a companion app, or on the developer's website.

### Redeeming in-app rewards ###
An in-app reward takes effect when the user redeems the reward within the game. Titles must be aware that a user has unlocked an achievement that is configured with an in-app reward so the title can properly deliver the reward to the user. To do this, titles should do the following:

1. Query the Achievements service upon title launch or title resume from suspension to see which unlocked achievements have in-app rewards and to get the reward code for each. This should always be done to make sure you catch any achievements that may have been unlocked while the title wasn’t running or on another console.  

    To query, you can use the RESTful Achievements URIs URIs or the APIs in the Microsoft.Xbox.Services.Achievements Namespace.

2. Register to receive a notification when one of your achievements is unlocked. This is optional, though probably desirable to most titles. Note that titles will only receive this notification if the title is actually running when the unlock happens. This is another reason why the previous step is important.

   To register for an achievement notification, use AchievementNotifier.GetTitleIdFilteredSource Method. This topic includes sample code that illustrates how to register.
