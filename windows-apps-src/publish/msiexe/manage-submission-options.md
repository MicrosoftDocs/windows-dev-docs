---
description: The manage listing page for your MSI or EXE app lets create links to your app on the web and Microsoft Store, and lets you control the visibiltiy of your app on the store.
title: Manage your MSI or EXE app's store listing
ms.assetid: DB63CF18-E3C9-4D02-8941-14699A7E8091
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, game settings, display mode, system requirements, hardware requirements, minimum hardware, recommended hardware, privacy policy, support contact info, app website, support info
ms.localizationpriority: medium
---

# Manage your MSI or EXE app's store listing

> [!NOTE]
> MSI and EXE support in the Microsoft Store is currently in a limited public preview phase. As the size of the preview expands, we'll be adding new participants from the wait list. To join the wait list, click [here](https://aka.ms/storepreviewwaitlist).

## Link to your app's listing

The direct link to your app's page can be shared to help your customers find the app in the Store. This link is in the format `https://apps.microsoft.com/store/detail/<your app's Store ID>`. When a customer clicks this link, it opens the web-based listing page for your app.

Your app's Store ID is also shown in this section. This Store ID can be used to [generate Store badges](https://developer.microsoft.com/store/badges) or otherwise identify your app.

You can help customers discover your app by linking to your app's listing in the Microsoft Store.

### Linking to your app's Store listing with the Microsoft Store badge

You can link directly to your app's listing with a custom badge to let customers know your app is in the Microsoft Store.

To create your badge, visit the [Microsoft Store badges](https://developer.microsoft.com/store/badges) page. You'll need to have your app's 12-character Store ID to generate the badge and link.

> [!NOTE]
> See [App marketing guidelines](../app-marketing-guidelines.md) for info and requirements related to your use of the Microsoft Store badge.

### Linking directly to your app in the Microsoft Store

The Store protocol link can be used to link directly to your app in the Store without opening a browser by using the ms-windows-store: URI scheme.

These links are useful if you know your users are on a Windows device and you want them to arrive directly at the listing page in the Store. For example, you might want to use this link after checking user agent strings in a browser to confirm that the user's operating system supports the Store.

To use this URI scheme to link directly to your app's Store listing, append your app's Store ID to this link:

`ms-windows-store://pdp/?ProductId=<your app's Store ID>`
