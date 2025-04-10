---
description: You can indicate whether and how your app can be offered for volume purchases through the Microsoft Store for Business and Microsoft Store for Education in the Organizational licensing section of an app submission.
title: Organizational licensing options
ms.assetid: 1EB139B0-67E7-4F66-AAEF-491B1E52E96F
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, store for business, store for education, organizational, volume licensing, enterprise, education store, business store, volume purchase, bulk
localizationpriority: high

---

# Organizational licensing options

You can indicate whether and how your app can be offered for volume purchases through Microsoft Store for Business and Microsoft Store for Education in the **Organizational licensing** section of the [Pricing and availability](publish-your-app/msix/price-and-availability.md#organizational-licensing) page of an app submission.

Through these settings, you can opt to allow your app to be made available to organizations (business and educational) who acquire and deploy multiple licenses for their users, providing an opportunity to increase your reach to organizations across Windows 10 device types, including PCs, tablets and phones.

You will also need to allow organizational licensing for any [line-of-business (LOB) apps](distribute-lob-apps-to-enterprises.md) that you publish directly to enterprises.

> [!NOTE]
> Selections for each of your apps are configured independently from each other. You may change your preferences for an app at any time by creating a new submission, and your changes will take effect after the submission completes the [certification process](publish-your-app/msix/app-certification-process.md).

> [!IMPORTANT]
> Submissions that use the [Microsoft Store submission API](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services) won't be made available to Microsoft Store for Business and Microsoft Store for Education. To make your app available for volume purchases by organizations, you must create and submit your submissions in Partner Center.


## Allowing your app to be offered to organizations

By default, the box labeled **Make my app available to organizations with Store-managed (online) licensing and distribution** is checked. This means that you wish your app to be available for inclusion in catalogs of apps that will be made available to organizations for volume acquisition, with app licenses managed through the Store's online licensing system.

> [!NOTE]
> This does not guarantee that your app will be made available to all organizations.

If you prefer not to allow us to offer your app to organizations for volume acquisition, uncheck this box. Note that this change will only take place after the app completes the certification process. If any organizations had previously acquired licenses to your app, those licenses will still be valid, and the people who have the app already can continue to use it.

> [!TIP]
> To publish line-of-business (LOB) apps exclusively to a specific organization, you can set up an enterprise association and allow the organization to add the apps directly their private store. For more info, see [Distribute LOB apps to enterprises](distribute-lob-apps-to-enterprises.md).


## Allowing disconnected (offline) licensing

Many organizations need apps enabled for offline licensing. For example, some organizations need to deploy apps to devices which rarely or never connect to the internet. If you want to allow your app to be made available to these customers, check the box labeled **Allow organization-managed (offline) licensing and distribution for organizations**.

Note that this box is **unchecked** by default. You must check the box to allow us to make your app available to verified organizations who will install it using organization-managed (offline) licensing. Organizations must go through additional validation in order to install paid apps to their end users in this way.

Offline licensing allows organizations to acquire your app on a volume basis, and then install the app without requiring each device to contact the Store's licensing system. The organization is able to download your app's package along with a license which lets them install it to devices (via their own management tools or by preloading apps on OS images) without notifying the Store when a particular license has been used. Enabling this scenario greatly increases deployment flexibility, and it may substantially increase the attractiveness of your app with these customers.

> [!IMPORTANT]
> Offline licensing is not supported for .xap packages.

 
## Paid app support

Currently, developer accounts located in certain markets are able to offer paid apps for volume acquisition through Microsoft Store for Business. 

> [!NOTE]
> In some markets, the price shown for an app in Microsoft Store for Business or Microsoft Store for Education may be different than the price shown to retail customers in the Microsoft Store for the same price tier. Payout of proceeds from organizational purchases works just the same as it does for consumer purchases of your app. For more info, see [Getting paid](/partner-center/marketplace-get-paid) and the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905). For a list of markets where Microsoft Store for Business and Microsoft Store for Education are available, see [Microsoft Store for Business and Microsoft Store for Education overview](/windows/manage/windows-store-for-business-overview#supported-markets).

If your country or region is not listed below, your paid apps currently will not be offered in Microsoft Store for Business and Microsoft Store for Education. If this is the case, the organizational licensing selections you make for your paid apps may be applied at a later time, as we may add support for submissions from additional developer account markets in the future.

At this time, developers located in the following countries and regions can distribute paid apps to organizational customers via Microsoft Store for Business and Microsoft Store for Education:

- Austria
- Belgium
- Bulgaria
- Canada
- Croatia
- Cyprus
- Czechia
- Denmark
- Estonia
- Finland
- France
- Germany
- Greece
- Hungary
- Ireland
- Isle of Man
- Italy
- Latvia
- Liechtenstein
- Lithuania
- Luxembourg
- Malta
- Monaco
- Netherlands
- Norway
- Poland
- Portugal
- Romania
- Slovakia
- Slovenia
- Spain
- Sweden
- Switzerland
- United Kingdom
- United States
