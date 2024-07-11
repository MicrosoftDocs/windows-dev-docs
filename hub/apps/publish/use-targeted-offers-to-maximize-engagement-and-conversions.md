---
description: Target specific segments of your customers with personalized content to increase engagement, retention, and monetization.
title: Use targeted offers to maximize engagement and conversions
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, targeted offers, offers, notifications
ms.localizationpriority: medium
---
# Use targeted offers to maximize engagement and conversions

Target specific segments of your customers with attractive, personalized content to increase engagement, retention, and monetization.

> [!IMPORTANT]
> Targeted offers can only be used with UWP apps that include add-ons.

## Targeted offer overview

At a high level, you need to do three things to use targeted offers:

1. **Create the offer in [Partner Center](https://partner.microsoft.com/dashboard).** Navigate to the **Engage > Targeted offers** page to create offers. More info about this process is described below.
2. **Implement the in-app offer experience.** Use the *Microsoft Store targeted offers API* in your app's code to retrieve the available offers for a given user. You'll also need to create the in-app experience for the targeted offer. For more info, see [Manage targeted offers using Store services](/windows/uwp/monetize/manage-targeted-offers-using-windows-store-services).
3. **Submit your app to the Store.** Your app must be published with the in-app offer experience in place in order for the offer(s) be made available to customers.

After you complete these steps, customers using your app will see the offers that are available to them at that time, based in their membership in the segment(s) associated with your offers. Please note that while we’ll make every effort to show all available offers to your customers, there may occasionally be issues that impact offer availability.


## To create and send a targeted offer

1.  In [Partner Center](https://partner.microsoft.com/dashboard), expand **Engage** in the left navigation menu, then select **Targeted offers**.
2.  On the **Targeted offers** page, review the available offers. Select **Create new offer** for any offer you wish to implement.

    > [!NOTE]
    > The available offers you will see may vary over time and based on account criteria.

3.  In the new row that appears below the available offers, choose the product (app) in which the offer will be available. Then, select the add-on that you want to associate with the offer.
4.  Repeat steps 2 and 3 if you'd like to create additional offers. You can implement the same offer type more than once for the same app, as long as you select different add-ons for each offer. Additionally, you can associate the same add-on with more than one offer type.
5.  When you are finished creating offers, click **Save**.

After you've implemented your offers, you can return to the **Targeted offers** page in Partner Center to view the total conversions for each offer.

If you decide not to use an offer (or if you no longer want to keep using it, click **Delete.**

> [!IMPORTANT]
> Be sure you have published the code to retrieve the available offers for a given user, and to create the in-app experience. For more info, see [Manage targeted offers using Store services](/windows/uwp/monetize/manage-targeted-offers-using-windows-store-services).
>
> When considering the content of your targeted offers, keep in mind that, as with all app content, the content in your offers must comply with the Store [Content Policies](/windows/apps/publish/store-policies-and-code-of-conduct).
>
> Also be aware that if a customer who uses your app (and is signed in with their Microsoft account at the time the segment membership is determined) gives their device to someone else to use, the other person may see offers that were targeted at the original customer.
