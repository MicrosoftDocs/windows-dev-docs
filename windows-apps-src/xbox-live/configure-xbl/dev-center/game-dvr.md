---
title: Game DVR
author: shrutimundra
description: Learn how to configure Xbox Live Game DVR strings on Windows Dev Center 2017
ms.assetid: e0f307d2-ea02-48ea-bcdf-828272a894d4
ms.author: kevinasg
ms.date: 10/30/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: low
keywords: Xbox live, Xbox, games, uwp, windows 10, Xbox one, Game DVR, Windows Dev Center
---

# Configuring Game DVR on Windows Dev Center

On Xbox One, one of the most popular features is Game DVR, which allows gamers easy access to recording, editing and sharing their most epic gaming moments. The Game DVR strings will appear as the title for any developer-created game DVR clips in your title. Configuring the string in the service will ensure that the correct localized version of that string shows up in any apps where that clip is featured. For example, if you wanted to create a clip when a user beats the final boss of your title, you would start by configuring a string called 'Boss Battle'. When making the call in your title code to create the clip, you would reference the ID.

You can use [Windows Dev Center](https://developer.microsoft.com/dashboard) to configure Game DVR strings that are associated with your game. Add configuration by doing the following:

1. Navigate to the **Game DVR** section for your title, located under **Services** > **Xbox Live** > **Game DVR**.
2. Click the **Create new string** button.
3. In the modal that pops up, enter the Game DVR string. Once completed, click **Confirm**.

![Image of the new game dvr string dialog](../../images/dev-center/game-dvr/game-dvr-1.png)
