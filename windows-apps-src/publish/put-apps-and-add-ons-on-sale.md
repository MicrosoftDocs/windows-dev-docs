---
author: jnHs
Description: You can promote your app or add-on in the Windows Store by putting it on sale for a limited time.
title: Put apps and add-ons on sale
ms.assetid: 71ABA960-0CDC-4E35-A1C8-1D34B6673817
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Put apps and add-ons on sale

You can promote your app or add-on in the Windows Store by putting it on sale for a limited time.

When you schedule a sale to temporarily lower the price of your app or add-on, customers viewing your Store listing will see that the price has been reduced, and they'll be able to purchase it at the lower price during the time period that you have selected. If you lower the price to **Free**, they can download it without paying at all during the sale period.

> **Note**  Sale pricing is only shown to your customers on Windows 10. On other operating systems, customers will see the regular price for your app or add-on. You can always change a price by choosing a different price tier in a new submission, but it will not be displayed as a limited-time sale.

## Scheduling a sale

Sales are scheduled as part of the submission for an app or add-on. If you want to schedule a sale for an app or add-on that has already been published, you'll need to create a new submission, even if that is the only change you want to make.

**To schedule a sale**

1.  On the **Pricing and availability** page of an in-progress app or add-on submission, go to the **Sale pricing** section.
2.  Click **New sale**.
3.  Enter the date and time for the start and end of the sale period. The times shown are in UTC.

   > **Note**  For add-ons, you can't schedule sales that overlap with each other.

4.  Choose your sale price from the drop-down list. You can pick any price, including **Free**.
5.  If you'd like to enter custom prices for this sale, click **Show custom market pricing options**. You can set custom sale prices per market (or exclude specific markets from the sale) here. For more info, see [Define pricing and market selection](define-pricing-and-market-selection.md).

    > **Note**  Market selections that you make in the **Sale pricing** section will not affect the markets in which the app is offered; these selections only determine whether a sale price is offered, and in which markets. If you set sale pricing for a market in which your app is not available, this won't cause the app to become available in that market.

6.  Click **Done** to save the scheduled sale.
7.  Click **Save** at the bottom of the **Pricing and availability** page, then click **Submit to the Store** from the submission overview.

> **Note**  It's possible to select a price tier that is higher than your app's base price. However, sale pricing will only be shown to customers if the sale price is lower than the regular price of the app in that market. Selecting a price that is higher than your app's base price might be appropriate for your sale if you've already set custom prices in certain markets that are higher than your app's base price, and you want to temporarily lower the price in those markets (but the sale price is still higher than the app's base price). If your selections would result in the price of the app being raised in a certain market, we won't show that (higher) price to customers in that market; they will continue to see the app at its previous (lower) price. We'll also show customers the lowest price available if you schedule separate overlapping sales with different prices.

## Changing or canceling a scheduled sale


To revise or cancel a sale that you've previously scheduled for an app or add-on, you'll need to create a new submission and submit it to the Store.

**To edit a scheduled sale**

1.  On the **Pricing and availability** page of an in-progress app or add-on submission, go to the **Sale pricing** section.
2.  Find the sale that you want to update, then click its price to edit the sale.
3.  Make your changes and then click **Done**.
4.  Click **Save** at the bottom of the **Pricing and availability** page, then click **Submit to the Store** from the submission overview.

After your submission goes through the certification process, the changes will take effect (even if the sale had already started).

> **Tip**  You can reuse a completed sale in a new submission by editing its start and end dates. This is especially useful if you’ve configured a sale with complicated custom market pricing.
 
**To cancel a scheduled sale**

1.  On the **Pricing and availability** page of an in-progress app or add-on submission, go to the **Sale pricing** section.
2.  Find the sale that you want to cancel and click **Delete** to remove it.
3.  Click **Save** at the bottom of the **Pricing and availability** page, then click **Submit to the Store** from the submission overview.

As long as the sale hasn't started by the time the submission completes the certification process, the deleted sale won't run at all. If you delete a sale that has already ended, the sale will simply be removed from your **Pricing and availability** page.

> **Important**   Since customers can see the scheduled end date when viewing your app's Store listing, we don't recommend deleting a sale after it's started. If you delete a sale that is already in progress, the sale will end when the submission completes the certification process, which can be frustrating to your potential customers.

