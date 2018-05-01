---
author: jnHs
Description: You can set the precise date and time that your app should become available in the Store, giving you greater flexibility and the ability to customize dates for different markets.
title: Configure precise release scheduling
ms.author: wdg-dev-content
ms.date: 05/02/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, schedule, release date, dates, launch
ms.localizationpriority: high
---

# Configure precise release scheduling

The **Schedule** section on the [Pricing and availability](set-app-pricing-and-availability.md) page lets you set the precise date and time that your app should become available in the Store, giving you greater flexibility and the ability to customize dates for different markets.

> [!NOTE]
> Although this topic refers to apps, release scheduling for add-on submissions uses the same process.

You can additionally opt to set a date when the product should no longer be available in the Store. Note that this means that the product can no longer be found in the Store via searching or browsing, but any customer with a direct link can see the product's Store listing. They can only download it if they already own the product or if they have a [promotional code](generate-promotional-codes.md) and are using a Windows 10 device.

By default (unless you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](choose-visibility-options.md#discoverability) section), your app will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section.

Note that you won't be able to configure dates in the **Schedule** section if you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](choose-visibility-options.md#discoverability) section, because your app won't be released to customers, so there is no release date to configure.

> [!IMPORTANT]
> The dates you specify in the Schedule section only apply to customers on Windows 10.
>
>If your app supports earlier OS versions, customers on those OS versions will see your app’s listing as soon as it passes certification and completes the publishing process, even if you have selected a later release date. Any **Stop acquisition** date you select will not apply to those customers; they will still be able to acquire the app (unless you submit an update with a new selection in the [Visibility](choose-visibility-options.md#discoverability) section, or if you select **Make app unavailable** from the **App overview** page).


## Base schedule

Selections you make for the Base schedule will apply to all markets in which your app is available, unless you later add dates for specific markets (or market groups) by selecting [Customize for specific markets](#customize-the-schedule-for-specific-markets).

You’ll see two options here: **Release** and **Stop acquisition**. 

## Release

In the **Release** drop-down, you can set when you want your app to be available in the Store. This means that the app is discoverable in the Store via searching or browsing, and that customers can view its Store listing and acquire the app.

>[!NOTE]
> After your app has been published and has become available in the Store, you will no longer be able to select a **Release** date (since the app will already have been released).

Here are the options you can configure for a product’s **Release** schedule:
- **as soon as possible**: The product will release as soon as it is certified and published. This is the default option.
- **at**: The product will release on the date and time that you select. You additionally have two options:
   - **UTC**: The time you select will be Universal Coordinated Time (UTC) time, so that the app releases at the same time everywhere.
   - **Local**: The time you select will be the used in each time zone associated with a market. (Note that for markets that include more than one time zone, only one time zone in that market will be used. For the United States, the Eastern time zone is used.)
- **not scheduled**: The app will not be available in the Store. If you choose this option, you can make the app available in the Store later by creating a new submission and choosing one of the other options.


## Stop acquisition

In the **Stop acquisition** dropdown, you can set a date and time when you want to stop allowing new customers to acquire it from the Store or discover its listing. This can be useful if you want to precisely control when an app will no longer be offered to new customers, such as when you are coordinating availability between more than one of your apps.

By default, **Stop acquisition** is set to never. To change this, select **at** in the drop-down and specify a date and time, as described above. At the date and time you select, customers will no longer be able to acquire the app.

It's important to understand that this option has the same impact as selecting **Make this app discoverable but not available** in the [Visibility](choose-visibility-options.md#discoverability) section and choosing **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before or have a promotional code and are using a Windows 10 device.** To completely stop offering an app to new customers, click **Make app unavailable** from the App overview page. For more info, see [Removing an app from the Store](guidance-for-app-package-management.md#removing-an-app-from-the-store).

> [!TIP]
> If you select a date to **Stop acquisition**, and later decide you'd like to make the app available again, you can create a new submission and change **Stop acquisition** back to **Never**. The app will become available again after your updated submission is published.

## Customize the schedule for specific markets 

By default, the options you select above will apply to all markets in which your app is offered. To customize the price for specific markets, click **Customize for specific markets**. The **Market selection** pop-up window will appear, listing all of the markets in which you’ve chosen to make your app available. If you excluded any markets in the [Markets](define-pricing-and-market-selection.md) section, those markets will not be shown. 

To add a schedule for one market, select it and click **Save**. You’ll then see the same **Release** and **Stop acquisition** options described above, but the selections you make will only apply to that market.

To add a schedule that will apply to multiple markets, you’ll create a *market group*. To do so, select the markets you wish to include, then enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) For example, if you want to create a market group for North America, you can select **Canada**, **Mexico**, and **United States**, and name it **North America** or another name that you choose. When you’re finished creating your market group, click **Save**. You’ll then see the same **Release** and **Stop acquisition** options described above, but the selections you make will only apply to that market group.

To add a custom schedule for an additional market, or an additional market group, just click **Customize for specific markets** again and repeat these steps. To change the markets included in a market group, select its name. To remove the custom schedule for a market group (or individual market), click **Remove**.

> [!NOTE]
> A market can’t belong to more than one of the market groups you use in the **Schedule** section. 










