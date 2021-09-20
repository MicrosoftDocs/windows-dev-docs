---
ms.assetid: adb2fa45-e18f-4254-bd8b-a749a386e3b4
description: Learn how to use the AdControl class to display banner ads in a JavaScript/HTML app for Windows 10 (UWP).
title: AdControl in HTML 5 and JavaScript
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, AdControl, ad control, javascript, HTML
ms.localizationpriority: medium
---
# AdControl in HTML 5 and JavaScript

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This walkthrough shows how to use the [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) class to display banner ads in a Universal Windows Platform (UWP) JavaScript/HTML app for Windows 10.

For a complete sample project that demonstrates how to add banner ads to a JavaScript/HTML app, see the [advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising).

## Prerequisites

* Install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) with Visual Studio 2015 or a later release of Visual Studio. For installation instructions, see [this article](install-the-microsoft-advertising-libraries.md).

> [!NOTE]
> If you have installed the Windows 10 SDK version 10.0.14393 (Anniversary Update) or a later version of the Windows SDK, you must also install the [WinJS](https://github.com/winjs/winjs) library. This library used to be included in previous versions of the Windows SDK for Windows 10, but starting with the Windows 10 SDK version 10.0.14393 (Anniversary Update) this library must be installed separately. 

## Integrate a banner ad into your app

1. In Visual Studio, open your project or create a new project.

    > [!NOTE]
    > If you're using an existing project, open the Package.appxmanifest file in your project and ensure that the **Internet (Client)** capability is selected. Your app needs this capability to receive test ads and live ads.

2. If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft advertising library in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

3. Add a reference to the Microsoft Advertising SDK in your project:

    1. From the **Solution Explorer** window, right click **References**, and select **Add Reference…**
    2.  In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for JavaScript** (Version 10.0).
    3.  In **Reference Manager**, click OK.

6.  Open the index.html file (or other html file as appropriate for your project).

7.  In the **&lt;head&gt;** section, after the project’s JavaScript references of default.css and main.js, add the reference to ad.js.

    ``` HTML
    <!-- Advertising required references -->
    <script src="//Microsoft.Advertising.JavaScript/ad.js"></script>
    ```

    > [!NOTE]
    > This line must be placed in the **&lt;head&gt;** section after the include of main.js; otherwise, you will encounter an error when you build your project.

8.  Modify the **&lt;body&gt;** section in the default.html file (or other html file as appropriate for your project) to include the **div** for the **AdControl**. Assign the **applicationId** and **adUnitId** properties of the **AdControl** to the [test ad unit values](set-up-ad-units-in-your-app.md#test-ad-units). Also adjust the **height** and **width** so the control is one of the [supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md).

    > [!NOTE]
    > Every **AdControl** has a corresponding *ad unit* that is used by our services to serve ads to the control, and every ad unit consists of an *ad unit ID* and *application ID*. In these steps, you assign test ad unit ID and application ID values to your control. These test values can only be used in a test version of your app. Before you publish your app to the Store, you must [replace these test values with live values](#release) from Partner Center.

    ``` HTML
    <div id="myAd" style="position: absolute; top: 50px; left: 0px; width: 300px; height: 250px; z-index: 1"
        data-win-control="MicrosoftNSJS.Advertising.AdControl"
        data-win-options="{applicationId: '3f83fe91-d6be-434d-a0ae-7351c5a997f1', adUnitId: 'test'}">
    </div>
    ```

9.  Compile and run the app to see it with an ad.

The following example shows the complete index.html for a simple app.

``` HTML
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>AdControlExampleApp</title>
    <!-- WinJS references -->
    <link href="lib/winjs-4.0.1/css/ui-light.css" rel="stylesheet" />
    <script src="lib/winjs-4.0.1/js/base.js"></script>
    <script src="lib/winjs-4.0.1/js/ui.js"></script>
    <!-- AdControlExampleApp references -->
    <link href="css/default.css" rel="stylesheet" />
    <script src="js/main.js"></script>
    <!-- Required reference for AdControl -->
    <script src="//Microsoft.Advertising.JavaScript/ad.js"></script>
</head>
<body>
    <div id="myAd" style="position: absolute; top: 50px; left: 0px; width: 300px; height: 250px; z-index: 1"
      data-win-control="MicrosoftNSJS.Advertising.AdControl"
      data-win-options="{applicationId: '3f83fe91-d6be-434d-a0ae-7351c5a997f1', adUnitId: 'test'}">
    </div>
    <p>Content goes here</p>
</body>
</html>
```

### Create an AdControl programmatically in Javascript

The previous steps show how to declare an **AdControl** in your HTML markup. Alternatively, you can programmatically create an **AdControl** using JavaScript. This example assumes that you are using an existing **div** in your HTML with the ID **myAd**.

> [!div class="tabbedCodeSnippets"]
:::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdControlSamples/js/main.js" id="DeclareAdControl":::

This example assumes that you have already declared event handler methods named **myAdError**, **myAdRefreshed**, and **myAdEngagedChanged**.

If you use this code and do not see ads, you can try inserting an attribute of **position:relative** in the **div** that contains the **AdControl**. This will override the default setting of the **IFrame**. Ads will be displayed correctly, unless they are not being shown due to the value of this attribute. Note that new ad units may not be available for up to 30 minutes.

> [!NOTE]
> The *applicationId* and *adUnitId* values shown in this example are [test mode values](set-up-ad-units-in-your-app.md#test-ad-units). You must [replace these values with live values](set-up-ad-units-in-your-app.md#live-ad-units) from Partner Center before submitting your app for submission.

<span id="release" />

## Release your app with live ads

1. Make sure your use of banner ads in your app follows our [guidelines for banner ads](ui-and-user-experience-guidelines.md#guidelines-for-banner-ads).

1.  In Partner Center, go to the [In-app ads](../publish/in-app-ads.md) page and [create an ad unit](set-up-ad-units-in-your-app.md#live-ad-units). For the ad unit type, specify **Banner**. Make note of both the ad unit ID and the application ID.
    > [!NOTE]
    > The application ID values for test ad units and live UWP ad units have different formats. Test application ID values are GUIDs. When you create a live UWP ad unit in Partner Center, the application ID value for the ad unit always matches the Store ID for your app (an example Store ID value looks like 9NBLGGH4R315).

2. You can optionally enable ad mediation for the **AdControl** by configuring the settings in the [Mediation settings](../publish/in-app-ads.md#mediation) section on the [In-app ads](../publish/in-app-ads.md) page. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks such as Taboola and Smaato and ads for Microsoft app promotion campaigns.

3.  In your code, replace the test ad unit values (**applicationId** and **adUnitId**) with the live values you generated in Partner Center.

4.  [Submit your app](../publish/app-submissions.md) to the Store using Partner Center.

5.  Review your [advertising performance reports](../publish/advertising-performance-report.md) in Partner Center.             

<span id="manage" />

## Manage ad units for multiple ad controls in your app

You can use multiple **AdControl** objects in a single app (for example, each page in your app might host a different **AdControl** object). In this scenario, we recommend that you assign a different ad unit to each control. Using different ad units for each control enables you to separately [configure the mediation settings](../publish/in-app-ads.md#mediation) and get discrete [reporting data](../publish/advertising-performance-report.md) for each control. This also enables our services to better optimize the ads we serve to your app.

> [!IMPORTANT]
> You can use each ad unit in only one app. If you use an ad unit in more than one app, ads will not be served for that ad unit.

## Related topics

* [Guidelines for banner ads](ui-and-user-experience-guidelines.md#guidelines-for-banner-ads)
* [Advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising)
* [Set up ad units for your app](set-up-ad-units-in-your-app.md)
* [Error handling in JavaScript walkthrough](error-handling-in-javascript-walkthrough.md)
