:::image type="content" source="images/msix-pricing-overview.png" lightbox="images/msix-pricing-overview.png" alt-text="A screenshot showing the overview of Pricing and availability section for MSIX/PWA app.":::

## Markets

The Microsoft Store reaches customers in over 240 countries and regions around the world. By default, we’ll offer your app in all possible markets. If you prefer, you can choose the specific markets in which you'd like to offer your app.

For more info, see [Define market selection](../../../apps/publish/publish-your-app/market-selection.md).

## Visibility

The **Visibility** section allows you to set restrictions on how your app can be discovered and acquired, including whether people can find your app in the Store or see its Store listing at all.

For more info, see [Choose visibility options](../../../apps/publish/publish-your-app/visibility-options.md).

## Schedule

By default (unless you have selected one of the **Make this app available but not discoverable in the Store** options in the Visibility section), your app will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section.

For more info, see [Configure precise release scheduling](../../../apps/publish/publish-your-app/release-schedule.md).

## Discoverability

The selections in the Discoverability section indicate how customers can discover and acquire your app.

### Make this product available and discoverable in the Store

This is the default option. Leave this option selected if you want your app to be listed in the Store for customers to find via the app's direct link and/or by other methods, including searching, browsing, and inclusion in curated lists.

### Make this product available but not discoverable in the Store

When you select this option, your app can’t be found in the Store by customers searching or browsing; the only way to get to your app’s listing is by a direct link.

## Pricing

You are required to select a base price for your app (unless you have selected the **Stop acquisition** option under **Make this app available but not discoverable in the Store** in the Visibility section), choosing either **Free** or one of the available price tiers. You can also schedule price changes to indicate the date and time at which your app’s price should change. Additionally, you have the option to customize these changes for specific markets. Microsoft periodically updates the recommended prices, to account for currency fluctuations in different markets. When a recommended price changes, the pricing area will show a warning indicator if the prices you’ve selected are not aligned with the new recommended values. The prices in your products will not change, you are in control of when and if you want to update these prices.

For more info, see [Set and schedule app pricing](../../../apps/publish/publish-your-app/schedule-pricing-changes.md).

## Free trial

Many developers choose to allow customers to try out their app for free using the trial functionality provided by the Store. By default, **No free trial** is selected, and there will be no trial for your app. If you’d like to offer a trial, you can select a value from the **Free trial** dropdown. See [Implement a trial version of your app](/windows/uwp/monetize/implement-a-trial-version-of-your-app) for more information.

There are two types of trial you can choose, and you have the option to configure the date and time when the trial should start and stop being offered.

### Time-limited

Choose **Time-limited** to allow customers to try your app for free for a certain number of days: **1 day**, **7 days**, **15 days**, or **30 days**. You can limit features by adding code to exclude or limit features in the trial version, or you can let customers access the full functionality during that period of time.

> [!NOTE]
> Time-limited trials are not shown to customers on Windows 10 build 10.0.10586 or earlier, or to customers on Windows Phone 8.1 and earlier.

### Unlimited

Choose **Unlimited** to let customers access your app for free indefinitely. You'll want to encourage them to purchase the full version, so make sure to add code to exclude or limit features in the trial version.

### Start and end dates

By default, your trial will be available as soon as your app is published, and it will never stop being offered. If you’d like, you can specify the date and time that your trial should start to be offered and when it should stop being offered.

>[!NOTE]
> These dates only apply for customers on Windows 10 or Windows 11 (including Xbox). If your app is available to customers on earlier OS versions, the trial will be offered to those customers for as long as your product is available.

To set dates for when your trial should be offered to customers on Windows 10 or Windows 11, change the **Starts on** and/or **Ends on** dropdown to **at**, then choose the date and time. If you do so, you can either choose **UTC** so that the time you select will be Universal Coordinated Time (UTC) time, or choose **Local** so that these times will be used in each time zone associated with a market. (Note that for markets that include more than one time zone, only one time zone in that market will be used. For the United States, the Eastern time zone is used.) You can select **Customize for specific markets** if you want to set different dates for any market(s).

## Sale pricing

If you want to offer your app at a reduced price for a limited period of time, you can create and schedule a sale.

For more info, see [Put apps and add-ons on sale](../../../apps/publish/put-apps-and-add-ons-on-sale.md).

## Organizational licensing

By default, your app may be offered to organizations to purchase in volume. You can indicate whether and how your app can be offered in this section.

For more info, see [Organizational licensing options](../../../apps/publish/organizational-licensing.md).

## Publish date

Previously, the **Publish date** section appeared on this page. This functionality can now be found on the [Submission options](../../../apps/publish/publish-your-app/create-app-submission.md) page, in the **Publishing hold options** section.
