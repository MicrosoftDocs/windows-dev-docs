---
author: JnHs
Description: Target specific segments of your customers with personalized content to increase engagement, retention, and monetization.
title: Use targeted offers to maximize engagement and conversions
ms.author: wdg-dev-content
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, targeted offers, offers, notifications
---

# Use targeted offers to maximize engagement and conversions

Target specific segments of your customers with attractive, personalized content to increase engagement, retention, and monetization. In some cases, targeted offers may be associated with a Microsoft-sponsored promotion that pays a bonus to developers for each successful conversion, allowing you to earn even more.

> [!IMPORTANT]
> Targeted offers can only be used with UWP apps that include add-ons.

At a high-level, you need to do two things to use targeted offers to engage with your customers:
1. **Create the offer in your dashboard.** Navigate to the **Monetize > Targeted offers** page to create offers. More info about this process is described below.
2. **Implement the in-app offer experience.** Use the *Windows Store targeted offers API* in your app's code to retrieve the available offers for a given user, and to claim the offer (if associated with a promotion). You'll also need to create the in-app experience for the targeted offer. For more info, see [Manage targeted offers using Store services](../monetize/manage-targeted-offers-using-windows-store-services.md).

After you complete these steps, a customer using your app will see the offers that are available to them at that time, based in their membership in the segment associated with that offer. Please note that while we’ll make every effort to show all available offers to your customers, there may occasionally be issues that impact offer availability.

## To create and send a targeted offer

Follow these steps to create a targeted offer in the dashboard. Note that in order for targeted customers to see your offers, you'll need to submit your app to the Store after implementing the code to create the in-app offer experience(s), as described in [Manage targeted offers using Store services](../monetize/manage-targeted-offers-using-windows-store-services.md).

Note that the available offers you will see may vary over time and based on account criteria. Initially, you will see one or more offers using predefined segment criteria. Soon, we'll allow you to create targeted offers based on [customer segments that you define](create-customer-segments.md).


1.	In the [Windows Dev Center dashboard](https://developer.microsoft.com/dashboard/overview), select your app.
2.	In the left navigation menu, expand **Monetization**, and select **Targeted offers**.
3.	On the **Targeted offers** page, review the available offers. Select **Create new offer** for any offer you wish to implement. 
4.	In the new row that appears below the available offers, choose the add-on that you want to associate with the offer. Details about the offer, such as its content type and price, will be displayed.
5.	Repeat steps 3 and 4 if you'd like to create additional offers. Keep in mind that you can implement the same offer type more than once the same app, as long as you select different add-ons for each one. Additionally, you can associate the same add-on with more than one offer type.
6.      When you are finished creating offers, click **Save**.

After you've implemented the offer, you can view the number of total conversions for each offer on the **Targeted offers** page in your dashboard. 

If you decide not to use an offer (or if you no longer want to keep using it, click **Delete.**

Be sure to implement the code to retrieve the available offers for a given user, and to create the in-app experience. When considering the content of your targeted offers, keep in mind that, as with all app content, the content in your offers must comply with the Store [Content Policies](https://msdn.microsoft.com/library/windows/apps/dn764944.aspx#content_policies). Also remember that if a customer who uses your app (and is signed in with their Microsoft account at the time the segment membership is determined) later gives their device to someone to use, the other person may see the offer that was targeted at the original customer. 

