---
Description: Manage and view details related to each of your apps in Partner Center, and configure services such as A/B testing and maps.
title: App management and services
ms.assetid: 99DA2BC1-9B5D-4746-8BC0-EC723D516EEF
ms.date: 03/21/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# App management and services

You can manage and view details related to each of your apps in [Partner Center](https://partner.microsoft.com/dashboard), and configure services such as notifications, A/B testing, and maps.

When working with an app in Partner Center, you'll see sections in the left navigation menu for **Services** and **App management**. You can expand these sections to access the functionality described below.

## Services

The **Services** section lets you manage several different services for your apps.

## Xbox Live

If you are publishing a game, you can enable the [Xbox Live Creators Program](https://www.xbox.com/developers/creators-program) on this page. This lets you start configuring and testing Xbox Live features, and eventually publish your Xbox Live Creators Program game.

For more info, see [Get started with the Xbox Live Creators Program](/gaming/xbox-live/get-started-with-creators/get-started-with-xbox-live-creators) and [Create a new Xbox Live Creators Program title and publish to the test environment](/gaming/xbox-live/get-started-with-creators/create-and-test-a-new-creators-title).

## Experimentation

Use the **Experimentation** page to create and run experiments for your Universal Windows Platform (UWP) apps with A/B testing. In an A/B test, you measure the effectiveness of feature changes (or variations) in your app on some customers before you enable the changes for everyone.

For more info, see [Run app experiments with A/B testing](../monetize/run-app-experiments-with-a-b-testing.md).

## Maps

To use map services in apps targeting Windows 10 or Windows 8.x, visit the [Bing Maps Dev Center](https://www.bingmapsportal.com/). For info about how to request a maps authentication key from the Bing Maps Developer Center and add it to your app, see [Request a maps authentication key](../maps-and-location/authentication-key.md) for more info. 

Use the **Maps** page only for previously-published apps for Windows Phone 8.1 and earlier. To use map services in these apps, you'll need to request a map service application ID and a token to include in your app's code. When you click **Get token**, we'll generate a Map service Application ID (**ApplicationID**) and Map service Authentication Token (**AuthenticationToken**) for your app. Be sure to add these values to your code before you package and submit your app. For more info, see [How to add a Map control to a page (Windows Phone 8.1)](/previous-versions/windows/apps/jj207033(v=vs.105)).

## Product collections and purchases

To use the Microsoft Store collection API and the Microsoft Store purchase API to access ownership information for apps and add-ons, you need to enter the associated Azure AD client IDs here. Note that it may take up to 16 hours for these changes to take effect.

For more info, see [Manage product entitlements from a service](../monetize/view-and-grant-products-from-a-service.md).

## Administrator consent

If your product integrates with Azure AD and calls APIs that request either [application permissions or delegated permissions](/graph/permissions-reference) that require administrator consent, enter your Azure AD Client ID here. This lets administrators who acquire the app for their organization grant consent for your product to act on behalf of all users in the tenant.

For more info, see [Requesting consent for an entire tenant](/azure/active-directory/develop/v2-permissions-and-consent#requesting-consent-for-an-entire-tenant).

## App management

The **App management** section lets you view identity and package details and manage names for your app.

## App identity

This page shows you details related to your app's unique identity within the Store, including the URL(s) to link to your app's listing.

For more info, see [View app identity details](view-app-identity-details.md).

## Manage app names

This is where you can view all of the names that you've reserved for your app. You can reserve additional names here, or delete names you're no longer using.

For more info, see [Manage app names](manage-app-names.md).

## Current packages

This page lets you view details related to all of your published packages.

> [!NOTE]
> You won't see any info here until your app has been published.

The name, version, and architecture of each package is shown. Click **Details** to show additional info such as supported language, app capabilities, and file sizes. The info you see for each package may vary depending on its targeted operating system and other factors. 

Developers with OEM permissions can also [generate preinstall packages](generate-preinstall-packages-for-oems.md) from the **Current packages** page.

## WNS/MPNS

The **WNS/MPNS** section provides options to help you create and send notifications to your app's customers. 

> [!TIP]
> For UWP apps, we suggest using the **Notifications** feature in Partner Center. This feature lets you send notifications to all of your app's customers, or to a targeted subset of your Windows 10 customers who meet the criteria you’ve defined in a [customer segment](create-customer-segments.md). For more info, see [Send notifications to your app's customers](send-push-notifications-to-your-apps-customers.md).

Depending on your app's package type and its specific requirements, you can also use one of the following options: 

-   **Windows Push Notification Services (WNS)** lets you send toast, tile, badge, and raw updates from your own cloud service. For more info, see [Windows Push Notification Services (WNS) overview](../design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview.md).

-   **Microsoft Azure Mobile Apps** lets you send push notifications, authenticate and manage app users, and store app data in the cloud. For more info, see the [Mobile Apps documentation](/azure/app-service-mobile/).

-   **Microsoft Push Notifications Service (MPNS)** can be used with previously published .xap packages for Windows Phone. You can send a limited number of unauthenticated notifications without doing any configuration here, although we recommend using authenticated notifications to avoid throttling limits. If you're using MPNS, you'll need to upload a certificate to the field provided on the **WNS/MPNS** page. For more info, see [Setting up an authenticated web service to send push notifications for Windows Phone 8](/previous-versions/windows/apps/ff941099(v=vs.105)).
 

 