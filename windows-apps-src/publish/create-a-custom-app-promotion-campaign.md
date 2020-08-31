---
description: In addition to creating an ad campaign for your app that will run in Windows apps, you can promote your app using other channels.
title: Create a custom app promotion campaign
ms.assetid: 7C9BF73E-B811-4FC7-B1DD-4A0C2E17E95D
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, custom, app, promotion, campaign
ms.localizationpriority: medium
---
# Create a custom app promotion campaign

In addition to creating an [ad campaign for your app](create-an-ad-campaign-for-your-app.md) that will run in Windows apps, you can also promote your app using other channels. For example, you can promote your app using a third-party app marketing provider, or you might post links to your app on social media sites. These activities are called *custom campaigns*.

If you run custom campaigns for your app, you can track the relative performance of each campaign by creating a different URL for each custom campaign, where each URL contains a different *campaign ID*. When a customer running Windows 10 clicks a URL that contains a campaign ID, Microsoft associates the click with the corresponding custom campaign and makes this data available to you in [Partner Center](https://partner.microsoft.com/dashboard).

> [!IMPORTANT]
> This data is only tracked for customers on Windows 10. Customers using other operating systems can still follow the link to your app's listing, but data about those customers' activities will not be included.

There are two main types of data associated with custom campaigns: *page views* for your app's Store listing, and *conversions*. A conversion is an app acquisition that results from a customer viewing your app's Store listing page from a URL that includes a custom campaign ID. For more details about conversions, see [Understanding how app acquisitions qualify as conversions](#understanding-how-acquisitions-qualify-as-conversions) in this topic.

You can retrieve custom campaign performance data for your app in the following ways:

* You can view data about page views and conversions for your app or add-on from the **App page views and conversions by campaign ID** and **Total campaign conversions** charts in the [Acquisitions report](acquisitions-report.md).
* If your app is a Universal Windows Platform (UWP) app, you can use APIs in the Windows SDK to programmatically retrieve the custom campaign ID that resulted in a conversion.

## Example custom campaign scenario

Consider a game developer who has finished building a new game and would like to promote it to players of her existing games. She posts the announcement of the new game release on her Facebook page, including a link to the game's Store listing. Many of her players also follow her on Twitter, so she also tweets an announcement with the link to the game's Store listing.

To track the success of each of these promotion channels, the developer creates two variants of the URL to the game's Store listing:

* The URL she will post to her Facebook page includes the custom campaign ID `my-facebook-campaign`

* The URL she will post to Twitter includes the custom campaign ID `my-twitter-campaign`

As her Facebook and Twitter followers click the URLs, Microsoft tracks each click and associates it with the corresponding custom campaign. Subsequent qualifying acquisitions of the game and any add-on purchases are associated with the custom campaign and reported as conversions.

<span id="conversions" />

## Understanding how acquisitions qualify as conversions

A custom campaign *conversion* is an acquisition that results from a customer clicking a URL that is promoted via a custom campaign. There are different scenarios for qualifying as a conversion for the **App page views and conversions by campaign ID** and **Total campaign conversions** charts in the [Acquisitions report](acquisitions-report.md) and for qualifying as a conversion for [programmatically retrieving the campaign ID](#programmatically).

### Qualifying conversions in the Acquisitions report

The following scenarios qualify as a conversion for the **App page views and conversions by campaign ID** and **Total campaign conversions** charts in the [Acquisitions report](acquisitions-report.md):

* A customer *with or without a recognized Microsoft account* clicks an app URL that contains a custom campaign ID and is redirected to the Store listing for the app. Then, that same customer acquires the app within 24 hours after they first clicked the Microsoft Store URL with the custom campaign ID.

* If the customer acquires the app on a different device than the one on which they clicked the URL with the custom campaign ID, the conversion will only be counted if the customer is signed in with the same Microsoft account as when they clicked the URL.

> [!NOTE]
> For app acquisitions that are counted as conversions for a custom campaign, any add-on purchases in that app are also counted as conversions for the same custom campaign.

### Qualifying conversions when programmatically retrieving the campaign ID

To qualify as a conversion when programmatically retrieving the campaign ID associated with the app, the following conditions must be met:

* On a device running **Windows 10, version 1607, or later**: A customer (whether signed in to a recognized Microsoft account or not) clicks a URL that contains a custom campaign ID and is redirected to the Store listing page for the app. The customer acquires the app while viewing the Store listing as a result of clicking the URL.

* On a device running **Windows 10, version 1511, or earlier**: A customer (who must be signed in with a recognized Microsoft account) clicks a URL that contains a custom campaign ID and is redirected to the Store listing page for the app. The customer acquires the app while viewing the Store listing as a result of clicking the URL. On these versions of Windows 10, the user must be signed in with a recognized Microsoft account in order for the acquisition to qualify as a conversion when programmatically retrieving the campaign ID.

> [!NOTE]
> If the customer leaves the Store listing page, but returns to the page with 24 hours (either on the same device, or on a different device when signed in with the same a Microsoft account) and acquires the app, this **will** qualify as a conversion in the **App page views and conversions by campaign ID** and **Total campaign conversions** charts in the [Acquisitions report](acquisitions-report.md). However, this **will not** qualify as a conversion if you programmatically retrieve the campaign ID.

## Embed a custom campaign ID to your app's Microsoft Store page URL

To create a Microsoft Store page URL for your app with a custom campaign ID:

1.  Create an ID string for your custom campaign. This string can contain up to 100 characters, although we recommend that you define short campaign IDs that are easily identifiable.

   > [!NOTE]
   > The campaign ID string may be visible to other developers when they view the [Acquisitions report](acquisitions-report.md) for their apps. This can occur when a customer clicks your custom campaign ID to enter the Store and purchases another developer’s app within the same session, thus attributing that conversion to your campaign ID. That developer will see how many conversions of their own app resulted from an initial click on your campaign ID, including the name of the campaign ID, but they will not see any data about how many users purchased your own apps (or apps from any other developers) after clicking your campaign ID.

2.  Get the link for your app's Store listing in HTML or protocol format.

    * Use the HTML URL if you want customers to navigate to your app's web-based Store listing in a browser on any operating system. On Windows devices, the Store app will also launch and display your app's listing. This URL has the format **`https://www.microsoft.com/store/apps/*your app ID*`**. For example, the HTML URL for Skype is `https://www.microsoft.com/store/apps/9wzdncrfj364`. You can find this URL on your [App identity](view-app-identity-details.md#link-to-your-apps-listing) page.

    * Use the protocol format if you are promoting your app from within other Windows apps that are running on a device or computer with the UWP app installed, or when you know that your customers are on a device which supports the Microsoft Store. This link will go directly to your app's Store listing without opening a browser. This URL has the format **`ms-windows-store://pdp/?PRODUCTID=*your app id*`**. For example, the protocol URL for Skype is `ms-windows-store://pdp/?PRODUCTID=9wzdncrfj364`.

3.  Append the following string to the end of the URL for your app:

    * For an HTML format URL, append **`?cid=*my custom campaign ID*`**. For example, if Skype introduces a campaign ID with the value **custom\_campaign**, the new URL including the campaign ID would be: `https://www.microsoft.com/store/apps/skype/9wzdncrfj364?cid=custom\_campaign`.

    * For a protocol format URL, append **`&cid=*my custom campaign ID*`**. For example, if Skype introduces a campaign ID with the value **custom\_campaign**, the new protocol URL including the campaign ID would be: `ms-windows-store://pdp/?PRODUCTID=9wzdncrfj364&cid=custom\_campaign`.

<span id="programmatically" />

## Programmatically retrieve the custom campaign ID for an app

If your app is a UWP app, you can programmatically retrieve the custom campaign ID associated with an app's acquisition by using APIs in the Windows SDK. These APIs make many analytics and monetization scenarios possible. For example, you can find out if the current user acquired your app after discovering it through your Facebook campaign, and then customize the app experience accordingly. Alternatively, if you are using a third-party app marketing provider, you can send data back to the provider.

These APIs will return a campaign ID string only if the customer clicked your URL with the embedded campaign ID, viewed the Microsoft Store page for your app, and then acquires your app without leaving the Store listing page. If the user leaves the page and then later returns and acquires the app, this will not [qualify as a conversion](#conversions) when using these APIs.

There are different APIs for you to use depending on the version of Windows 10 that your app targets:

* Windows 10, version 1607, or later: Use the [**StoreContext**](/uwp/api/windows.services.store.storecontext) class in the **Windows.Services.Store** namespace. When using this API, you can retrieve custom campaign IDs for any [qualified acquisitions](#conversions), whether or not the user is signed in with a recognized Microsoft account.

* Windows 10, version 1511, or earlier: Use the [**CurrentApp**](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) class in the **Windows.ApplicationModel.Store** namespace. When using this API, you can only retrieve custom campaign IDs for [qualified acquisitions](#conversions) where the user is signed in with a recognized Microsoft account.

> [!NOTE]
> Although the **Windows.ApplicationModel.Store** namespace is available in all versions of Windows 10, we recommend that you use the APIs in the **Windows.Services.Store** namespace if your app targets Windows 10, version 1607, or later. For more information about the differences between these namespaces, see [In-app purchases and trials](../monetize/in-app-purchases-and-trials.md#choose-namespace). The following code example shows how to structure your code to use both APIs in the same project.

### Code example

The following code example shows how to retrieve the custom campaign ID. This example uses both sets of APIs in the **Windows.Services.Store** and **Windows.ApplicationModel.Store** namespaces by using [version adaptive code](../debug-test-perf/version-adaptive-code.md). By following this process, your code can run on any version of Windows 10. To use this code, the target OS version of your project must be **Windows 10 Anniversary Edition (10.0; Build 14394)** or later, although the minimum OS version can be an earlier version.

``` csharp
// This example assumes the code file has using statements for
// System.Linq, System.Threading.Tasks, Windows.Data.Json,
// and Windows.Services.Store.
public async Task<string> GetCampaignId()
{
    // Use APIs in the Windows.Services.Store namespace if they are available
    // (the app is running on a device with Windows 10, version 1607, or later).
    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(
         "Windows.Services.Store.StoreContext"))
    {
        StoreContext context = StoreContext.GetDefault();

        // Try to get the campaign ID for users with a recognized Microsoft account.
        StoreProductResult result = await context.GetStoreProductForCurrentAppAsync();
        if (result.Product != null)
        {
            StoreSku sku = result.Product.Skus.FirstOrDefault(s => s.IsInUserCollection);

            if (sku != null)
            {
                return sku.CollectionData.CampaignId;
            }
        }

        // Try to get the campaign ID from the license data for users without a
        // recognized Microsoft account.
        StoreAppLicense license = await context.GetAppLicenseAsync();
        JsonObject json = JsonObject.Parse(license.ExtendedJsonData);
        if (json.ContainsKey("customPolicyField1"))
        {
            return json["customPolicyField1"].GetString();
        }

        // No campaign ID was found.
        return String.Empty;
    }
    // Fall back to using APIs in the Windows.ApplicationModel.Store namespace instead
    // (the app is running on a device with Windows 10, version 1577, or earlier).
    else
    {
#if DEBUG
        return await Windows.ApplicationModel.Store.CurrentAppSimulator.GetAppPurchaseCampaignIdAsync();
#else
        return await Windows.ApplicationModel.Store.CurrentApp.GetAppPurchaseCampaignIdAsync() ;
#endif
    }
}
```

This code does the following:

1. First, it checks to see if the [**StoreContext**](/uwp/api/windows.services.store.storecontext) class in the **Windows.Services.Store** namespace is available on the current device (this means the device is running Windows 10, version 1607, or later). If so, the code proceeds to use this class.

2. Next, it attempts to get the custom campaign ID for the case where the current user has a recognized Microsoft account. To do this, the code gets a [**StoreSku**](/uwp/api/Windows.Services.Store.StoreSku) object that represents the current app SKU, and then it accesses the [**CampaignId**](/uwp/api/windows.services.store.storecollectiondata.CampaignId) property to retrieve the campaign ID, if one is available.
3. The code then attempts to retrieve the campaign ID for the case where the current user does not have a recognized Microsoft account. In this case, the campaign ID is embedded in the app license. The code retrieves the license by using the [**GetAppLicenseAsync**](/uwp/api/windows.services.store.storecontext.GetAppLicenseAsync) method and then parses the JSON contents of the license for the value of a key named *customPolicyField1*. This value contains the campaign ID.

4. If the [**StoreContext**](/uwp/api/windows.services.store.storecontext) class in the **Windows.Services.Store** namespace is not available, the code then falls back to using the [**GetAppPurchaseCampaignIdAsync**](/uwp/api/Windows.ApplicationModel.Store.CurrentApp#Windows_ApplicationModel_Store_CurrentApp_GetAppPurchaseCampaignIdAsync) method in the **Windows.ApplicationModel.Store** namespace to retrieve the custom campaign ID (this namespace is available in all versions of Windows 10, including version 1511 and earlier). Note that when using this method, you can only retrieve custom campaign IDs for [qualified acquisitions](#conversions) where the user has a recognized Microsoft account.

### Specify the campaign ID in the proxy file for the Windows.ApplicationModel.Store namespace

The **Windows.ApplicationModel.Store** namespace includes [**CurrentAppSimulator**](/uwp/api/windows.applicationmodel.store.currentappsimulator), a special class that simulates Store operations for testing your code before you submit your app to the Store. This class retrieves data from [a local file named Windows.StoreProxy.xml file](../monetize/in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#using-the-windowsstoreproxyxml-file-with-currentappsimulator). The previous code example shows how to include use both **CurrentApp** and **CurrentAppSimulator** in debug and non-debug code in your project. To test this code in a debug environment, add an **AppPurchaseCampaignId** element to the WindowsStoreProxy.xml file on your development computer, as shown in the following example. When you run the app, the [**GetAppPurchaseCampaignIdAsync**](/uwp/api/windows.applicationmodel.store.currentappsimulator.GetAppPurchaseCampaignIdAsync) method will always return this value.

``` xml
<CurrentApp>
    ...
    <AppPurchaseCampaignId>your custom campaign ID</AppPurchaseCampaignId>
</CurrentApp>
```

The **Windows.Services.Store** namespace does not provide a class that you can use to simulate license info during testing. Instead, you must publish an app to the Store and download that app to your development device to use its license for testing. For more information, see [In-app purchases and trials](../monetize/in-app-purchases-and-trials.md#testing).

## Test your custom campaign

Before you promote a custom campaign URL, we recommend that you test your custom campaign by doing the following:

1.  Sign in to a Microsoft account on the device you are using for testing.

2.  Click your custom campaign URL. Make sure you are taken to your app page, and then close the UWP app or the browser page.

3.  Click the URL several more times, closing the UWP app or the browser page after each visit to your app's page. During **one** of the visits to your app's page, acquire your app to generate a conversion. Count the total number of times you clicked the URL.

4. Confirm whether the expected page views and conversions appear in the **App page views and conversions by campaign ID** and **Total campaign conversions** charts in the [Acquisitions report](acquisitions-report.md), and test your app's code to confirm whether it can successfully retrieve the campaign ID using the APIs described above.