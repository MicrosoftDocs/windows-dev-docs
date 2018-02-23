---
title: Achievements 2017 on XDP
author: KevinAsgari
description: Achievements 2017
ms.assetid: d424db04-328d-470c-81d3-5d4b82cb792f
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Configure Achievements 2017 on XDP

In the Achievements 2017 system, the only configuration needs for an achievement are its name, locked & unlocked descriptions, display image, and reward information. (Note: Valid achievement rewards still include: Gamerscore, art rewards, and in-game rewards.)

<span id="_Enable_Simplified_Achievements" class="anchor"></span>

## Enable Achievements 2017

The Achievements system used by your title is managed at the product level.  

Developers may switch their products between Simplified and Cloud-Powered Achievements systems at any time prior to publishing into RETAIL. Upon switching Achievements systems, in either direction, all of your title’s configured & published achievements (and challenges, if applicable) will be deleted from every sandbox. 

Once a title’s service configuration has been published to RETAIL, its Achievements system is permanently set and cannot be changed. **No exceptions can be made. This is required for both technical & policy reasons.**

1.  From your product page in XDP, navigate to **Product Setup**.
![Screenshot of the product page in XDP](../../images/omega/simplified-achievements-1.png)

2.  Select **Product Details**.
![Screenshot of the product setup page in XDP](../../images/omega/simplified-achievements-2.png)

1.  Switch the **Achievements configuration system** toggle to *Achievements 2017.*
![Screenshot of the product details page showing the toggle between Achievements 2017 and Achievements 2013](../../images/omega/simplified-achievements-3.png)

1.  You will receive a warning that all of your title’s achievements will be deleted in all sandboxes. If you are OK with the deletion of your existing achievements in all sandboxes, click **Save**.
![Screenshot showing the warning](../../images/omega/simplified-achievements-4.png)

## Configure an Achievement

1.  Enable Achievements 2017 for your title.

2.  Navigate to **Service Configuration** and select **Achievements**.
![Screenshot of the service configuration tasks page in XDP](../../images/omega/simplified-achievements-5.png)

1.  Enter the achievement display details.

    *Note: These strings are used for display in the XDP UI. The final strings that will be shown to users must be configured in the “Localized Strings” service configuration option (step 5).*<br>
![Screenshot of the Add New Achievement dialog in XDP](../../images/omega/simplified-achievements-6.png)

1.  To add Gamerscore, Artwork, or In-App reward onto the achievement, click **New** under the **Rewards** section.
![Screenshot of the Edit Reward dialog in XDP](../../images/omega/simplified-achievements-7.png)

1.  If supplying localized strings for your achievement names & descriptions, navigate to **Localized Strings.**

    *Note: Don’t forget to define your English localized strings. Otherwise, your users in non-USA countries who prefer English text may not get the expected result.*<br>
![Screenshot of the Localized Strings page in XDP](../../images/omega/simplified-achievements-8.png)

1.  To compare your recent changes to the currently published service configuration data, navigate to **Compare Data** and select the desired sandboxes for comparison.
![Screenshot of Compare Data page on XDP](../../images/omega/simplified-achievements-9.png)

1.  When ready to publish & test in your dev sandbox, return to **Service Configuration** and click the **Publish** button.
![Screenshot of the Service Configuration Tasks page on XDP showing the Publish button](../../images/omega/simplified-achievements-10.png)

1.  Choose the destination sandbox where you want to test (likely the same sandbox where you drafted the achievements).

    Select the *Events, Stat Rules, Achievements…* checkbox under Service Configuration.

    Click **Submit.**

![Screenshot of the Publishing Approval page on XDP](../../images/omega/simplified-achievements-11.png)
