---
description: Manage and view details related to each of your apps in Partner Center, and configure services such as A/B testing and maps.
title: Product management and services
ms.assetid: 99DA2BC1-9B5D-4746-8BC0-EC723D516EEF
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Product Management and services

You can manage and view details related to each of your apps in [Partner Center](https://partner.microsoft.com/dashboard), and configure services such as maps or WNS.

When working with an app in Partner Center, you'll see sections in the left navigation menu for **Services** and **Product management**. You can expand these sections to access the functionality described below.

## Product management

The **Product management** section lets you view identity and package details and manage names for your app.

### 1. Product identity

This page shows you details related to your app's unique identity within the Store, including the URL(s) to link to your app's listing.

For more info, see [View app identity details](view-app-identity-details.md).

### 2. Manage app names

This is where you can view all of the names that you've reserved for your app. You can reserve additional names here, or delete names you're no longer using.

For more info, see [Manage app name reservations](partner-center/manage-app-name-reservations.md).

### 3. Manage packages

This page lets you view details related to all of your published packages.

> [!NOTE]
> You won't see any info here until your app has been published.

The name, version, and architecture of each package is shown. Click **Details** to show additional info such as supported language, app capabilities, and file sizes. The info you see for each package may vary depending on its targeted operating system and other factors. 

Developers with OEM permissions can also [generate preinstall packages](generate-preinstall-packages-for-oems.md) from the **Manages packages** page.

### 4. WNS/MPNS

The **WNS/MPNS** section provides options to help you create and send notifications to your app's customers. 

> [!TIP]
> For UWP apps, we suggest using the **Notifications** feature in Partner Center. This feature lets you send notifications to all of your app's customers, or to a targeted subset of your Windows 10 or Windows 11 customers who meet the criteria you’ve defined in a [customer group](create-customer-groups.md).

Depending on your app's package type and its specific requirements, you can also use: 

-   **Windows Push Notification Services (WNS)** lets you send toast, tile, badge, and raw updates from your own cloud service. For more info, see [Windows Push Notification Services (WNS) overview](/windows/apps/design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview).
 
## Services

The **Services** section lets you manage several different services for your apps.

### 1. Xbox Live

If you are publishing a game, you can enable the [Xbox Live Creators Program](https://www.xbox.com/developers/creators-program) on this page. This lets you start configuring and testing Xbox Live features, and eventually publish your Xbox Live Creators Program game.

For more info, see [Xbox services overview](/gaming/gdk/docs/services/fundamentals/live-xbl-overview).

### 2. Maps

To use map services in apps targeting Windows 10 or Windows 11, visit the [Bing Maps Dev Center](https://www.bingmapsportal.com/). For info about how to request a maps authentication key from the Bing Maps Developer Center and add it to your app, see [Request a maps authentication key](/windows/uwp/maps-and-location/authentication-key) for more info. 

### 3. Product collections and purchases

To use the Microsoft Store collection API and the Microsoft Store purchase API to access ownership information for apps and add-ons, you need to enter the associated Microsoft Entra ID client IDs here. Note that it may take up to 16 hours for these changes to take effect.

For more info, see [Manage product entitlements from a service](/windows/uwp/monetize/view-and-grant-products-from-a-service).

### 4. Administrator consent

If your product integrates with Microsoft Entra ID and calls APIs that request either [application permissions or delegated permissions](/graph/permissions-reference) that require administrator consent, enter your Microsoft Entra ID Client ID here. This lets administrators who acquire the app for their organization grant consent for your product to act on behalf of all users in the tenant.

For more info, see [Requesting consent for an entire tenant](/azure/active-directory/develop/v2-permissions-and-consent#requesting-consent-for-an-entire-tenant).