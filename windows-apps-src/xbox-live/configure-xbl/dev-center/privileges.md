---
title: Privileges Configuration on Dev Center
author: aablackm
description: Learn how to configure privileges on Windows Dev Center
ms.author: aablackm
ms.date: 04/09/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: low
keywords: Xbox Live, Xbox, games, uwp, windows 10, Xbox one, privileges, Windows Dev Center
---
# Configure Privileges on Windows Development Center

The Privileges configuration page dictates whether or not gamers will be restricted from streaming your title to streaming services like [Mixer](https://mixer.com/). By default your game will not restrict broadcasting on any streaming platform, changes to this page are only required if you would like to restrict broadcasting. You can restrict broadcasting in two ways. You may either disable broadcasting everywhere by checking the box in the **Default** section, or you can restrict broadcasting by Sandbox by adding a sandbox in the **Sandbox overrides** section.

Checking the box in the **Default** section restricts broadcasting for this title on all services and sandboxes.

![default broadcasting restricted](../../images/dev-center/privileges/default-privileges-check.JPG)

In order to restrict broadcasting on a particular sandbox click **Add** button in the **Sandbox overrides** section. Choose the target sandbox from the dropdown list and check the box underneath to restrict broadcasting for that title on the chosen sandbox. Sandbox overrides can be unchecked or deleted to remove restrictions on broadcasting.

![sandbox broadcasting restricted](../../images/dev-center/privileges/sandbox-privileges-check.JPG)

Click the **Save** button to keep any configuration changes made for these settings.

> [!NOTE]
> Checking the box to disable broadcasting will only prohibit streaming done through Xbox consoles or the Windows game bar on PC. Checking the box on this page does not prevent the use of capture cards or other external capture or streaming services.