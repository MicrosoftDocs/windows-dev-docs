---

author: jnHs
Description: If your app uses ad mediation or displays banner or interstitial ads using the Microsoft Store Services SDK, use the Monetize with ads page to manage your use of ads.
title: Monetize with ads
ms.assetid: 09970DE3-461A-4E2A-88E3-68F2399BBCC8
ms.author: wdg-dev-content
ms.date: 06/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Monetize with ads

Each app in your dashboard includes a **Monetization** &gt; **Monetize with ads** page. You can manage your use of ads for the following scenarios on this page:

* Your UWP app uses an [AdControl](https://msdn.microsoft.com/en-us/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx), [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx), or [NativeAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.nativead.aspx) from the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp).
* Your Windows 8.x or Windows Phone 8.x app uses an **AdControl** or **InterstitialAd** from the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk).
* Your Windows 8.x or Windows Phone 8.x app uses an **AdMediatorControl** from the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk).

<span id="create-ad-unit" />
## Create ad units

Use this section to create an ad unit for the following scenarios:

* Your app shows banner ads by using an **AdControl**. For more information, see [AdControl in XAML and .NET](../monetize/adcontrol-in-xaml-and--net.md) and [AdControl in HTML5 and JavaScript](../monetize/adcontrol-in-html-5-and-javascript.md).
* Your app shows interstitial video ads or interstitial banner ads by using an **InterstitialAd**. For more information, see [Interstitial ads](../monetize/interstitial-ads.md).
* Your app shows native ads by using a **NativeAd**. For more information, see [Native ads](../monetize/native-ads.md).

For more information about working with ad units in your app, see [Set up ad units in your app](../monetize/set-up-ad-units-in-your-app.md).

> [!IMPORTANT]
> You can use each ad unit in only one app. If you use an ad unit in more than one app, ads will not be served for that ad unit.

To create an ad unit:

1.  In the **Ad unit name** field, enter a name for the ad unit. This can be any descriptive string that you want to use to identify the ad unit for reporting purposes.
2.  In the **Ad unit type** drop-down, select the type of ad unit that corresponds to the ads you are showing in your control. The available options are: **Banner**, **Banner interstitial**, **Video interstitial**, and **Native**.
3.  In the **Device family** drop-down, select the device family targeted by the app in which your ad unit will be used. The available options are: **UWP (Windows 10)**, **PC/Tablet (Windows 8.1)**, or **Mobile (Windows Phone 8.x)**.
4.  Click **Create ad unit**.

The new ad unit appears at the top of the list in the **Available ad units** section on this page.

> [!NOTE]
> The ability to create **Native** ad units is currently available only to select developers who are participating in a pilot program, but we intend to make this feature available to all developers soon. If you are interested in joining our pilot program, reach out to us at aiacare@microsoft.com.

> [!NOTE]
> If your Windows 8.x or Windows Phone 8.x app uses an **AdMediatorControl** to show banner ads, you do not need to request ad units here. In this scenario, the ad units are automatically generated for you.

<span id="available-ad-units" />
## Available ad units

Your ad units appear in a table at the bottom of this section. For each ad unit you will see an **Application ID** and an **Ad unit ID**. To show ads in your app, you'll need to use these values in your code:

-   If your app shows banner ads, assign these values to the [ApplicationId](https://msdn.microsoft.com/library/mt313174.aspx) and [AdUnitId](https://msdn.microsoft.com/library/mt313171.aspx) properties of your [AdControl](https://msdn.microsoft.com/library/mt313154.aspx) object. For more information, see [AdControl in XAML and .NET](../monetize/adcontrol-in-xaml-and--net.md) and [AdControl in HTML5 and JavaScript](../monetize/adcontrol-in-html-5-and-javascript.md).
-   If your app shows video interstitial ads, pass these values to the [RequestAd](https://msdn.microsoft.com/library/mt313192.aspx) method of your [InterstitialAd](https://msdn.microsoft.com/library/mt313189.aspx) object. For more information, see [Interstitial ads](../monetize/interstitial-ads.md).
-   If your app shows native ads, pass these values to the *applicationId* and *adUnitId* parameters of the [NativeAdsManager](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.nativeadsmanager.nativeadsmanager.aspx) constructor. For more information, see [Native ads](../monetize/native-ads.md).

<span id="mediation" />
## Ad mediation

If your app is a UWP app for Windows 10, you can use the options in this section to enable ad mediation for a UWP ad unit that is associated with a banner, interstitial, or native ad in your app. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks and non-revenue generating ads for Microsoft app promotion campaigns. We take care of mediating banner ad requests from the ad networks you choose.

If you have a UWP ad unit that is already associated with a banner, interstitial, or native ad in your app, enabling ad mediation requires no code changes in your app.

> [!NOTE]
> This section describes the **Ad mediation** options for UWP app packages. If your app package targets Windows 8.x or Windows Phone 8.x and uses the **AdMediatorControl** from the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk), the **Ad mediation** section in the dashboard displays a different set of options. For more information about configuring mediation settings for a Windows 8.x or Windows Phone 8.x app package that uses **AdMediatorControl**, see [this article](https://msdn.microsoft.com/library/windows/apps/mt219689).

To configure ad mediation settings for a UWP ad unit in your app:

1. In the **Configure mediation for** drop-down, select the UWP app package that contains the banner, interstitial, or native ad unit you want to configure.

2. In the **Ad unit type** drop down, select the type of ad unit that you want to configure (**Banner**, **Banner interstitial**, **Video interstitial**, or **Native**).

3. In the **Ad unit** drop-down, select the name of the UWP ad unit that you want to configure.
    > [!NOTE]
    > When you enable ad mediation for a UWP ad unit, you do not need to obtain an ad unit from third-party ad networks. Our ad mediation service automatically creates any necessary third-party ad units.

4. By default, the **Let Microsoft choose the best mediation settings for your app** check box is selected. This option uses machine-learning algorithms to automatically choose the ad mediation settings for your app to help you maximize your ad revenue across the markets your app supports. We recommend that you use this option. Otherwise, if you want to choose your own ad mediation settings, clear this check box.
    > [!NOTE]
    > The remaining steps in this section are only applicable if you clear this check box and choose your own ad mediation settings.

5. In the **Target** drop-down, choose **Baseline** to configure the default configuration for your ad mediation settings. This default configuration will be applied to all markets, except for markets where you define market-specific configurations.

6. Next, specify the ratio of ads you want to show in your control from paid networks (which pay you revenue for impressions) and other ad networks (which do not pay you revenue for impressions). To do this, enter a value between 0 and 100 in the **Weight** fields for **Paid ad networks** and **Other ad networks**.  

7. In the **Paid ad networks** section, select the check box in the **Active** column for each paid network you want to use, and then use the arrows in the **Rank** column to order the networks by rank (this specifies how often each network should be used by your control).

    The following paid networks are currently supported. Note that some of these networks are [not available in all markets](#network-markets).

    -   **AOL and AppNexus**. This is a Microsoft-managed ad network that serves ads through our partner networks, AOL and AppNexus.
        > [!NOTE]
        > **AOL and AppNexus** is always ranked first in the **Paid ad networks** list for banner ad units, and it cannot be changed to a lower ranking for these types of ads.

    -   **Microsoft App install ads**. Select this option to serve app install ads or app re-engagement ads created by other developers in the Windows ecosystem.  

    -   **AppNexus (direct)**: Select this option to serve video interstitial ads from [AppNexus](https://www.appnexus.com).

    -   **Smaato**: Select this option to serve ads from [Smaato](https://www.smaato.com/).

    -   **SpotX**: Select this option to serve ads from [SpotX](https://www.spotx.tv/).

    -   **smartclip**: Select this option to serve ads from [smartclip](http://www.smartclip.com/).

    -   **Taboola**: Select this option to serve ads from [Taboola](https://www.taboola.com/).

8. In the **Other ad networks** section, select the check box in the **Active** column for each other network you want to use, and then use the arrows in the **Rank** column to order the networks by rank (this specifies how often each network should be used by your control). The networks in this section do not earn you revenue for ad impressions. Instead, these networks show ads from sources such as app promotion campaigns.

    The following other networks are currently supported:

    -   **Microsoft House ads**. If you [create a promotional ad campaign for one of your apps](create-an-ad-campaign-for-your-app.md) and configure this campaign as a [house ad campaign](about-house-ads.md), select this options to show ads from this campaign.

    -   **Microsoft Community ads**. If you [create a promotional ad campaign for one of your apps](create-an-ad-campaign-for-your-app.md) and configure this campaign as a [community ad campaign](about-community-ads.md), select this options to show ads from this campaign.

9. For each market where you want to override the default mediation configuration, select the market in the **Target** drop-down, and update the ad network selections and ranking.

10. Click **Save**.

<span id="network-markets" />
### Supported markets for ad networks

The available ad networks serve ads in all [supported markets](define-pricing-and-market-selection.md#windows-store-consumer-markets) for UWP apps, with the following exceptions.

|  Ad network  |  Supported markets  |
|--------------|---------------------|
| Smaato | Brazil, Canada, France, Germany, Italy, Japan, Spain, United Kingdom, United States |
| smartclip | Austria, Belgium, Denmark, Finland, Germany, Italy, Netherlands, Norway, Sweden, Switzerland  |


## Microsoft affiliate ads

Check the box in this section if you want to show Microsoft affiliate ads in your app. If you check this box, ads for products in the Store, including music, games, movies, apps, hardware and software, will be served to your app when no ads from other ad networks are available. When customers click the ads and buy products in the Store within a given attribution window, you will earn a commission on approved purchases.

If you change this selection, you do not need to republish your app for the changes to take effect. For more information about Microsoft affiliate ads, see [About affiliate ads](about-affiliate-ads.md).

## COPPA compliance

For purposes of the Children's Online Privacy Protection Act (“COPPA”), you must notify Microsoft if your app is directed at children under the age of 13. If you use Dev Center to indicate to Microsoft that your app is directed at children under the age of 13, Microsoft will take steps to disable its behavioral advertising services when delivering advertising into your app. If your app is directed at children under the age of 13, you have certain obligations under COPPA.

For more information on your obligations under COPPA, please see [this page](http://go.microsoft.com/fwlink/p/?linkid=536558).
