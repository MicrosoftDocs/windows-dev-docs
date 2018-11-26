---
Description: You can promote your app or add-on in the Microsoft Store by putting it on sale for a limited time.
title: Put apps and add-ons on sale
ms.assetid: 71ABA960-0CDC-4E35-A1C8-1D34B6673817
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Put apps and add-ons on sale

You can promote your app or add-on in the Microsoft Store by putting it on sale for a limited time. You can choose to offer the product either at a lower price tier or with a percentage-based discount. And you can choose whether to offer the sale to everyone, or make it an exclusive offer for customers who own one of your other products.

> [!NOTE]
> Sale pricing is not supported for subscription add-ons.

When you use the **Sale pricing** section of the **Pricing and availability** page of a submission to temporarily lower the price of your app or add-on, customers viewing your Store listing will see strikethrough pricing indicating that the price has been reduced (as opposed to a [scheduled price change](set-and-schedule-app-pricing.md#schedule-price-changes), which can lower or raise the price without displaying it as a change in the Store). 

During the time period that your product is on sale, customers will be able to purchase it at the lower price during the time period that you have selected. If you lower the price to **Free**, they can download it without paying at all during the sale period.

> [!IMPORTANT]
> Sale pricing is only shown to your customers on Windows 10 devices, including Xbox One. Sales that you offer only to owners of one of your other products are only shown to customers on Windows 10, version 1607 or later.
> 
> On other operating systems, customers will see the regular price for your app or add-on, and won't be able to purchase it at the sale price. You can always change a price by choosing a different price tier in a new submission, but it will not be displayed as a limited-time sale.


## Scheduling a sale

Sales are scheduled as part of the submission for an app or add-on. If you want to schedule a sale for an app or add-on that has already been published, you'll need to create a new submission, even if that is the only change you want to make.

**To schedule a sale**

1. On the **Pricing and availability** page of an in-progress app or add-on submission, go to the **Sale pricing** section.
2. Select **Show options**, and then select **New sale**.
3. The **Market selection** popup window will appear, allowing you to create a *market group* that will specify the market(s) in which the sale should be offered. You can click **Select all** to offer the sale to every market in which your app is available, select one individual market, or select multiple markets. You can optionally enter a name for your market group. When you’ve made your selections, click **Create**. (To edit the markets in the group later, click its name.)

   > [!NOTE]
   > Market selections that you make in the Sale pricing section will not affect the markets in which the app is offered; these selections only determine whether a sale price is offered, and in which markets. If you set sale pricing for a market in which your app is not available, this won't cause the app to become available in that market.
4. Choose one of the following options to specify the type of discount:
   - **Price**: Use this option to select a lower price tier at which your app will be offered. You can change the currency drop-down to select the price in whichever currency you prefer. (The price will be converted to the corresponding tier for each currency. For more info, see [Pricing](set-app-pricing-and-availability.md).)
   - **Percentage**: Use this option to select the percentage for a discount that will be applied to your app. The same discount percentage is used for all currencies.
5. In the **Offered to** row, choose from one of the available options, including:
   - **Everyone**: The sale will be offered to all customers.
   - **Owners of**: The sale will be offered to customers who already own one of your apps. You can select from your published apps from the drop-down that appears. You must have one or more published apps in order for this option to be available.

  > [!IMPORTANT]
  > If you select **Owners of**, the sale will only be visible to customers on Windows 10, version 1607 or later.

   - **Known user group**: The sale will be offered to the people in the [known user group](create-known-user-groups.md) you select. You must already have created the known user group in order for this option to be available.
   - **Segment**: The sale will be offered to the people in the customer segment you select. You can use a  [segment that you have already created](create-customer-segments.md) here. You can also choose **First time payers** to offer the sale only to customers who have never purchased anything in the Store. We offer this segment here because we've found that after a customer makes their first Store purchase, they often continue to make more purchases, so this can be a great group to entice with sale pricing.
6. Enter the date and time for the start and end of the sale period. Choose one of the following time zone options:
   - **UTC**: The time you select will be Universal Coordinated Time (UTC) time, so that the sale occurs at the same time everywhere.
   - **Local**: The time you select will be the used in each time zone associated with a market. (Note that for markets that include more than one time zone, only one time zone in that market will be used. For the United States, the Eastern time zone is used.)
7. To schedule an additional sale, select **New sale**. Otherwise, select **Save** at the bottom of the **Pricing and availability page**, then select **Submit to the Store** from the submission overview.

> [!NOTE]
> It's possible to select a price tier that is higher than your app's base price. However, sale pricing will only be shown to customers if the sale price is lower than the regular price of the app in that market.
>
> Selecting a price that is higher than your app's base price might be appropriate for your sale if you've already set custom prices in certain markets that are higher than your app's base price, and you want to temporarily lower the price in those markets (but the sale price is still higher than the app's base price). If your selections would result in the price of the app being raised in a certain market, we won't show that (higher) price to customers in that market; they will continue to see the app at its previous (lower) price. We'll also show customers the lowest price available if you schedule separate overlapping sales with different prices.

## Changing or canceling a scheduled sale

To revise or cancel a sale that you've previously scheduled for an app or add-on, you'll need to create a new submission and submit it to the Store.

**To edit a scheduled sale**

1.  On the **Pricing and availability** page of an in-progress app or add-on submission, go to the **Sale Pricing** section.
2.  Find the sale that you want to update, then make your changes.
3.  Click **Save** at the bottom of the **Pricing and availability** page, then click **Submit to the Store** from the submission overview.

After your submission goes through the certification process, the changes will take effect.

> [!IMPORTANT]
> If a sale has already started, you won't be able to edit the start date. While you can edit the end date, we recommend that you don't edit a sale to end sooner than its original end date. It can be frustrating to your potential customers if you end a sale before the date that was originally published (since customers see the scheduled end date when viewing your app's Store listing).

 **To cancel a sale that hasn't started yet**

1.  On the **Pricing and availability** page of an in-progress app or add-on submission, go to the **Sale pricing** section.
2.  Find the sale that you want to cancel and click **Remove**.
3.  Click **Save** at the bottom of the **Pricing and availability** page, then click **Submit to the Store** from the submission overview. As long as the sale hasn't started by the time the new submission completes the certification process, the removed sale won't run at all.




