---
Description: If you enable customers to use your app for free during a trial period, you can entice your customers to upgrade to the full version of your app by excluding or limiting some features during the trial period.
title: Exclude or limit features in a trial version
ms.assetid: 1B62318F-9EF5-432A-8593-F3E095CA7056
keywords: windows 10, uwp, trial, in-app purchase, IAP, Windows.ApplicationModel.Store
ms.date: 08/25/2017
ms.topic: article


ms.localizationpriority: medium
---
# Exclude or limit features in a trial version

If you enable customers to use your app for free during a trial period, you can entice your customers to upgrade to the full version of your app by excluding or limiting some features during the trial period. Determine which features should be limited before you begin coding, then make sure that your app only allows them to work when a full license has been purchased. You can also enable features, such as banners or watermarks, that are shown only during the trial, before a customer buys your app.

> [!IMPORTANT]
> This article demonstrates how to use members of the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace to implement trial functionality. This namespace is no longer being updated with new features, and we recommend that you use the [Windows.Services.Store](/uwp/api/windows.services.store) namespace instead. The **Windows.Services.Store** namespace supports the latest add-on types, such as Store-managed consumable add-ons and subscriptions, and is designed to be compatible with future types of products and features supported by Partner Center and the Store. The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. For more information about implementing trial functionality using the **Windows.Services.Store** namespace, see [this article](implement-a-trial-version-of-your-app.md).

## Prerequisites

A Windows app in which to add features for customers to buy.

## Step 1: Pick the features you want to enable or disable during the trial period

The current license state of your app is stored as properties of the [LicenseInformation](/uwp/api/Windows.ApplicationModel.Store.LicenseInformation) class. Typically, you put the functions that depend on the license state in a conditional block, as we describe in the next step. When considering these features, make sure you can implement them in a way that will work in all license states.

Also, decide how you want to handle changes to the app's license while the app is running. Your trial app can be full-featured, but have in-app ad banners where the paid-for version doesn't. Or, your trial app can disable certain features, or display regular messages asking the user to buy it.

Think about the type of app you're making and what a good trial or expiration strategy is for it. For a trial version of a game, a good strategy is to limit the amount of game content that a user can play. For a trial version of a utility, you might consider setting an expiration date, or limiting the features that a potential buyer can use.

For most non-gaming apps, setting an expiration date works well, because users can develop a good understanding of the complete app. Here are a few common expiration scenarios and your options for handling them.

-   **Trial license expires while the app is running**

    If the trial expires while your app is running, your app can:

    -   Do nothing.
    -   Display a message to your customer.
    -   Close.
    -   Prompt your customer to buy the app.

    The best practice is to display a message with a prompt for buying the app, and if the customer buys it, continue with all features enabled. If the user decides not to buy the app, close it or remind them to buy the app at regular intervals.

-   **Trial license expires before the app is launched**

    If the trial expires before the user launches the app, your app won't launch. Instead, users see a dialog box that gives them the option to purchase your app from the Store.

-   **Customer buys the app while it is running**

    If the customer buys your app while it is running, here are some actions your app can take.

    -   Do nothing and let them continue in trial mode until they restart the app.
    -   Thank them for buying or display a message.
    -   Silently enable the features that are available with a full-license (or disable the trial-only notices).

If you want to detect the license change and take some action in your app, you must add an event handler for this as described in the next step.

## Step 2: Initialize the license info

When your app is initializing, get the [LicenseInformation](/uwp/api/Windows.ApplicationModel.Store.LicenseInformation) object for your app as shown in this example. We assume that **licenseInformation** is a global variable or field of type **LicenseInformation**.

For now, you will get simulated license information by using [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) instead of [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp). Before you submit the release version of your app to the **Store**, you must replace all **CurrentAppSimulator** references in your code with **CurrentApp**.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/TrialVersion.cs" id="InitializeLicenseTest":::

Next, add an event handler to receive notifications when the license changes while the app is running. The app's license could change if the trial period expires or the customer buys the app through a Store, for example.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/TrialVersion.cs" id="InitializeLicenseTestWithEvent":::

## Step 3: Code the features in conditional blocks

When the license change event is raised, your app must call the License API to determine if the trial status has changed. The code in this step shows how to structure your handler for this event. At this point, if a user bought the app, it is a good practice to provide feedback to the user that the licensing status has changed. You might need to ask the user to restart the app if that's how you've coded it. But make this transition as seamless and painless as possible.

This example shows how to evaluate an app's license status so that you can enable or disable a feature of your app accordingly.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/TrialVersion.cs" id="ReloadLicense":::

## Step 4: Get an app's trial expiration date

Include code to determine the app's trial expiration date.

The code in this example defines a function to get the expiration date of the app's trial license. If the license is still valid, display the expiration date with the number of days left until the trial expires.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/TrialVersion.cs" id="DisplayTrialVersionExpirationTime":::

## Step 5: Test the features using simulated calls to the License API

Now, test your app using simulated data. **CurrentAppSimulator** gets test-specific licensing info from an XML file called WindowsStoreProxy.xml, located in %UserProfile%\\AppData\\local\\packages\\&lt;package name&gt;\\LocalState\\Microsoft\\Windows Store\\ApiData. You can edit WindowsStoreProxy.xml to change the simulated expiration dates for your app and for its features. Test all your possible expiration and licensing configurations to make sure everything works as intended. For more info, see [Using the WindowsStoreProxy.xml file with CurrentAppSimulator](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#proxy).

If this path and file don't exist, you must create them, either during installation or at run-time. If you try to access the [CurrentAppSimulator.LicenseInformation](/uwp/api/windows.applicationmodel.store.currentappsimulator.licenseinformation) property without WindowsStoreProxy.xml present in that specific location, you will get an error.

## Step 6: Replace the simulated License API methods with the actual API

After you test your app with the simulated license server, and before you submit your app to a Store for certification, replace **CurrentAppSimulator** with **CurrentApp**, as shown in the next code sample.

> [!IMPORTANT]
> Your app must use the **CurrentApp** object when you submit your app to a Store or it will fail certification.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/TrialVersion.cs" id="InitializeLicenseRetailWithEvent":::

## Step 7: Describe how the free trial works to your customers

Be sure to explain how your app will behave during and after the free trial period so your customers won't be surprised by your app's behavior.

For more info about describing your app, see [Create app descriptions](../publish/create-app-store-listings.md).

## Related topics

* [Store sample (demonstrates trials and in-app purchases)](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store)
* [Set app pricing and availability](../publish/set-app-pricing-and-availability.md)
* [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp)
* [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator)
 

 
