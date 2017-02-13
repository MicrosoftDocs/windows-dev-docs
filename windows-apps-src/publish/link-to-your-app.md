---
author: jnHs
Description: You can help customers discover your app by linking to your app's Store listing.
title: Link to your app
ms.assetid: 5420B65C-7ECE-4364-8959-D1683684E146
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Link to your app


You can help customers discover your app by linking to your app's Store listing.

## Getting the link to your app's Store listing


You can find the link to your app's Store listing on the [App identity](view-app-identity-details.md) page, in the **App management** section of each app in your dashboard.

This link is in the format **`https://www.microsoft.com/store/apps/<your app's Store ID>`**

When a customer clicks this link, it will open the web-based listing page for your app. If your app is available for the customer's device, the Store app will also launch and display your app's listing.

> **Note**  Depending on what OS versions you're targeting, you may see more than one link here. All apps will show the URL for Windows 10, which will work for any OS. You may see additional links for Windows 8.1 and earlier and/or Windows Phone 8.1 and earlier, which will work only on the specified OS versions.

 

## Linking to your app's Store listing with the Windows Store badge


You can link directly to your app's listing with a custom badge to let customers know your app is in the Windows Store.

To create your badge, visit the [Windows Store badges](http://go.microsoft.com/fwlink/p/?LinkID=534236) page. You'll need to have your app's Store ID to use this form to generate the badge and link. This ID is the last 12 characters of the **URL for Windows 10** shown on the [App identity](view-app-identity-details.md) page in the **App management** section.

> **Note**  See [App marketing guidelines](app-marketing-guidelines.md) for more info on using the Windows Store badge.

 

## Linking directly to your app in the Windows Store


You can create a link that launches the Windows Store and goes directly to your app's listing page without opening a browser by using the **ms-windows-store:** URI scheme.

These links are useful if you know your users are on a Windows device and you want them to arrive directly at the listing page in the Store—for example, after checking user agent strings in a browser to confirm the user's operating system, or when you are already communicating via a UWP app, you may want to apply this protocol.

To use the Windows Store protocol to link directly to your app's Store listing, append your app's Store ID to this link:

`ms-windows-store://pdp/?ProductId=`

For more about using the Windows Store protocol, see [Launch the Windows Store app](../launch-resume/launch-store-app.md).

 

 




