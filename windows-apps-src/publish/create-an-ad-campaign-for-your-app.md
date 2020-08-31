---
Description: You can create ad campaigns in Partner Center to help promote your app and grow your app's user base.
title: Create an ad campaign for your app
ms.assetid: 10D94929-92C4-4379-AA5F-6FEF879F2463
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ad, campaign, promote
ms.localizationpriority: medium
---
# Create an ad campaign for your app

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

You can create ad campaigns in [Partner Center](https://partner.microsoft.com/dashboard) to help promote your app and grow its user base. By default, we will choose the target audience for your ads based on the settings for your app in Partner Center, but you can optionally define your own audience. You can also use a default set of ad templates or upload your own ad designs. For more details about ad campaigns, see [Common questions about ad campaigns](common-questions.md).

You can create ad campaigns only for apps that have passed the final publishing phase of the [app certification process](the-app-certification-process.md).

> [!NOTE]
> This section of the documentation describes how to create an ad campaign in Partner Center. Other campaign options to create and manage ad campaigns programmatically include [Vungle](https://vungle.com/) and the [Microsoft Store promotions API](../monetize/run-ad-campaigns-using-windows-store-services.md).

## Instructions

Here's how to create an ad campaign to promote an app.

1.  From the left navigation menu of [Partner Center](https://partner.microsoft.com/dashboard), expand **Attract** and then select **Ad campaigns**.
2.  Select **Create campaign** (or if you have created campaigns before, select **New campaign**).
3.  On the next page, in the **Objective type** section, choose one of the following:
    * **Increase installs for your app**. Select this option if your ad campaign is intended to get people to install your app.
    * **Increase engagement in your app**. Select this option if your ad campaign is intended to get your customers to increase usage of your app. When you select this option, you can target your ad campaign at specific [customer segments](create-customer-segments.md) that you define.

4.  Select the app you want to promote with this campaign. Note that the app must be available in the Store already.
5.  Review the name provided for your campaign in the **Campaign name** field and make changes, if desired.
6.  Under **Campaign type**, choose one of these options:
    * **Paid ad**: These ads will run in any app that matches your app’s device and category. For new campaigns created after January 9, 2017, these ads will also appear within MSN.com, Outlook.com, Skype, and other Microsoft premium properties. App promotion campaigns that target apps and Microsoft premium properties are known as *universal* campaigns.
    * **Community ad (free)**: These ads will run in apps published by other developers who also create community ad campaigns. Before you can select this option, you must have opted in to showing community ads in the **Monetize** -> **In-app ads** page. For more information, see [About community ads](about-community-ads.md).
    * **House ad (free)**: These ads will only run in your apps that match the advertised app’s device type. House ads are free of charge. For more information, see [About house ads](about-house-ads.md).

7.  For paid ad campaigns, confirm the **Campaign duration** (the period of time in which your campaign budget will be spent). The default option is **Monthly**, which means that your campaign budget will be spent every month on a recurring basis until you stop the campaign. If you have a premium account, you can optionally choose **Custom** to specify a custom date and time range during which your campaign budget will be spent. For more info about premium accounts, see [Common questions about ad campaigns](common-questions.md#how-can-i-increase-the-maximum-monthly-budget-amount-allowed-for-my-ad-campaign).

8.  Confirm your budget and payment info. (If you are creating a house campaign or community campaign, these options will not appear, since these campaigns are free of charge.)
    * Under **Budget**, use the slider to set the amount of money you want to spend each month to run the ad (or the total budget, if you have selected a custom campaign duration).

        The monthly budget is prorated for the month in which the ad campaign is created. In other words, if you create an ad campaign halfway through a calendar month, you will be charged for half of your monthly budget for that month.

    * Specify a payment method for your ad campaign by clicking **Add new payment method** and fill in your account details. If you have already provided a payment instrument, you can select **Choose a different payment method** if you need to update it. The country/region of your payment method's billing address must match the country/region associated with your developer account.

    * If you have received a coupon from a Microsoft representative to pay for an ad campaign, click **Use a coupon**, enter the coupon code, and click **Apply** to apply the coupon to the campaign.

    When you are finished, click **Save and next** to continue to the **Audience** step. This step is not available for House ad campaigns, since they run only in your own apps.

9.  In the **Audience** page, we will show the audience settings that we recommend for your campaign. You can optionally adjust this info:
    * **Countries/regions**: Choose up to 5 countries or regions in which you want your ad to appear. For a list of supported countries or regions, see [Common questions about ad campaigns](common-questions.md#where-will-my-ad-appear).

    * **Devices**: Choose the device types on which you want these ads to appear. Only the device types supported by your app are shown.

    * **Surface**: Choose **Universal** to allow your ad to appear within apps, plus MSN.com, Outlook.com, Skype, and other Microsoft premium properties. Choose **App** if you only want your ad to appear within apps.

    * **Operating system**: Choose the operating system(s) on which your ad should appear. Only the operating systems supported by your app are shown.

    * **Gender**: Choose whether to restrict the audience for your ad by gender.

    * **Age range**: Select the age range(s) for your desired audience.

    This section also displays an **Estimated Reach** graph. This graph shows the audience you can expect to reach with your current targeting selections as a percentage of all Windows ad-enabled app users in the selected markets.

10.  If you chose **Increase engagement in your app** as your campaign objective, you can select one of your customer segments to target. Ads created using this campaign will be shown only to the customers who are included in the segment. Only one segment can be selected per ad campaign. For info about segments, see [Create customer segments](create-customer-segments.md). When you are finished, click **Save and next** to continue to the **Ad design** step. This step is not available for House ad campaigns, because they run only in your own apps.

11.  In the **Ad design** page, choose one of these options:
    * **Auto-generated**. This is the default option, and it allows you to create an ad from our default templates. You can make selections to customize your ad content, and we'll preview what your ad will look like based on your choices (updated automatically as you make selections).
        * In the **Language** drop-down, select the language for your ad. The text for the Microsoft Store badge will appear in the language you select.
        * To add an extra line of text to your ad, enter text in the **Custom tagline** field.
            > [!NOTE]
            > The text you enter here must be localized into the selected language. The custom tag line will be rejected if the text does not align with [Bing Ads policies](https://advertise.bingads.microsoft.com/bing-ads-policies). Consult this page for guidance on style and disallowed content.
        * To further customize the ad, expand **Customize ad design / See all ad sizes** and choose any of the following:
            * **Background color**. Choose from the available options.
            * **Images**. Choose one of the available images (taken from your app's Store listing).
            * **Show my app rating**. Select this checkbox if you want to show the app's rating.
            * **Show that my app is free**. If your app is free in all the selected markets, you have the option to select this checkbox.
            * **Call to action**. If you chose **Increase engagement in your app** as your campaign objective, you can set your ad's call to action button to **Open**, **Play**, **Read**, **Listen**, or **Shop**.  

    * **Custom**. Choose this option to use your own ad designs. Note that if you selected a customer segment earlier, you must use custom creatives. You can upload different files for each of the available ad sizes. The files must meet the following requirements and guidelines:
        * Each file must be a .png or .jpg that is 40 KB or smaller.
        * Your ad designs must meet the requirements specified in the [Microsoft Creative Acceptance Policy](https://about.ads.microsoft.com/solutions/ad-products/display-advertising/creative-acceptance-policies).
        * The content in your ad designs must be relevant to the app you are promoting. Ad designs that are not related to the app will not be distributed to ads in other apps.
        * All content in your ad designs should be clearly legible. For example, content should not be blurred, pixelated or stretched.

12.  If you have a [premium account](common-questions.md#how-can-i-increase-the-maximum-monthly-budget-amount-allowed-for-my-ad-campaign), you can use the **Destination URL** box to control what happens when a customer clicks your ad.
    * If you leave the box empty, when a customer clicks your ad, your app's Store listing will be displayed.
    * If you are using Adjust, Kochava, Tune, or Vungle to measure install analytics for your app, enter your install tracking URL. When you save the campaign, the tracking URL is validated to make sure that it resolves to the listing page for your app in the Microsoft Store. For more information about install tracking with these services, see the [Adjust](https://docs.adjust.com/en/), [Kochava](https://support.kochava.com/), [Tune](https://help.tune.com/hasoffers/), and [Vungle](https://support.vungle.com/hc/en-us) documentation.
    * If you chose **Increase engagement in your app** as your campaign objective, you can specify a [deep-link URI](../launch-resume/handle-uri-activation.md) to redirect customers in the selected segment to a specific page within your app.
    * If you specify any destination that is not your app description page or a page inside of your app, your campaign will automatically be paused.

13.  Finally, click **Review** to confirm your ad campaign's settings and, if it's a paid ad campaign, its budget and payment information. Click **Confirm** and your ads will typically start appearing on devices within a few hours.

## Review ad campaign performance

To see how your campaigns are performing, return to the **Ad campaigns** page. Select **Section filters** to scope what's included in the report by **Date**, **Campaign objective**, **App name**, **Campaign type**, or **Status**. In addition to seeing info about your campaign's **Impressions**, **Clicks**, **Conversions**, and **Spend**, you can use the report to **Pause** or **Resume** a campaign. For more info, see [Ad campaign report](/windows/uwp/publish/ad-campaign-report).

To edit a campaign, select its name in the list.