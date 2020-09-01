---
Description: View details related to the unique identity assigned to your app by the Microsoft Store, and get a link to your app's Store listing.
title: View app identity details
ms.assetid: 86F05A79-EFBC-4705-9A71-3A056323AC65
ms.date: 10/02/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# View app identity details


You can view details related to the unique identity assigned to your app by the Microsoft Store on its **App identity** pages. You can also get a link to your app's Store listing on this page.

To find this info, navigate to one of your apps, then expand **App management** in the left navigation menu. Select **App identity** to view these details.


## Values to include in your app package manifest

The following values must be included in your package manifest. If you [use Microsoft Visual Studio to build your packages](/windows/msix/package/packaging-uwp-apps), and are signed in with the same Microsoft account that you have associated with your developer account, these details are included automatically. If you're building your package manually, you'll need to add these items:

-   **Package/Identity/Name**
-   **Package/Identity/Publisher**
-   **Package/Properties/PublisherDisplayName**

For more info, see [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) in the [package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).

Together, these elements declare the identity of your app, establishing the "package family" to which all of its packages belong. Individual packages will have additional details, such as architecture and version.


## Additional values for package family

The following values are additional values that refer to your app's package family, but are not included in your manifest.

-   **Package Family Name (PFN)**: This value is used with certain Windows APIs.
-   **Package SID**: You'll need this value to send WNS notifications to your app. For more info, see [Windows Push Notification Services (WNS) overview](../design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview.md).


## Link to your app's listing

The direct link to your app's page can be shared to help your customers find the app in the Store. This link is in the format **`https://www.microsoft.com/store/apps/<your app's Store ID>`**. When a customer clicks this link, it opens the web-based listing page for your app. On Windows devices, the Store app will also launch and display your app's listing.

Your app's **Store ID** is also shown in this section. This Store ID can be used to [generate Store badges](https://developer.microsoft.com/store/badges) or otherwise identify your app.

The **Store protocol link** can be used to link directly to your app in the Store without opening a browser, such as when you are linking from within an app. For more info, see [Link to your app](link-to-your-app.md).



 

 