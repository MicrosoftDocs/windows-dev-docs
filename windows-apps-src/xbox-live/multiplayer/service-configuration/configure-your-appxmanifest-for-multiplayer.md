---
title: Configure your AppXManifest for Multiplayer
author: KevinAsgari
description: Learn how to configure your UWP AppXManifest to enable Xbox Live multiplayer invites.
ms.assetid: 72f179e7-4705-4161-9b8a-4d6a1a05b8f7
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, protocol activation, multiplayer
ms.localizationpriority: low
---

# Configure your AppXManifest for Multiplayer

You need to make some updates to the .appxmanifest file in your Visual Studio project if the following conditions are true:
- You are developing a UWP
- You want to implement the ability for players to invite other users to your title

If you don't do this step, then your title will not get  protocol activated when a recipient player accepts an invitation to play.

## Open your Package.appxmanifest

Your Package.appxmanifest file is typically located in the same directory as your Visual Studio project's solution file.  Or you can find it in the solution explorer.

![](../../images/multiplayer/multiplayer_open_appxmanifest.png)

## Add new entry

You will need to add the following to the ```<Extensions>``` element under ```<Applications>``` in your Package.appxmanifest file

```
<Extensions>
  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="ms-xbl-multiplayer" />
  </uap:Extension>
</Extensions>
```

Eg:

![](../../images/multiplayer/multiplayer_appxmanifest_changes.png)

Save and rebuild your title.  To learn how to use the Multiplayer Manager to implement the ability to invite players to your title, please see [Play Multiplayer With Friends](../multiplayer-manager/play-multiplayer-with-friends.md)
