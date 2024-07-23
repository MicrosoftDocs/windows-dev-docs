---
description: Learn how your MSIX app's packages are made available to your customers, and how to manage specific package scenarios.
title: Guidance for app package management for MSIX app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Guidance for app package management for MSIX app

Learn how your app's packages are made available to your customers, and how to manage specific package scenarios.

## OS versions and package distribution

Different operating systems can run different types of packages. If more than one of your packages can run on a customer's device, the Microsoft Store will provide the best available match.

Generally speaking, later OS versions can run packages that target previous OS versions for the same device family. Windows 11 devices can run all previous supported OS versions (per device family). 

## Removing an app from the Store

At times, you may want to stop offering an app to customers, effectively "unpublishing" it. To do so, use the toggle button in **Store presence** card from the **App overview** page. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it (unless they have a [promotional code](../../generate-promotional-codes.md) and are using a Windows 10 or Windows 11 device).

:::image type="content" source="images/new-overview-make-product-unavailable.png" lightbox="images/new-overview-make-product-unavailable.png" alt-text="A screenshot of the new msix overview page showing how to make product unavailable in Store":::

> [!IMPORTANT]
> This option will override any [visibility](./visibility-options.md#discoverability) settings that you have selected in your submissions.

This option has the same effect as if you created a submission and chose **Make this product available but not discoverable in the Store** with the **Stop acquisition** option. However, it does not require you to create a new submission.

Note that any customers who already have the app will still be able to use it and can download it again (and could even get updates if you submit new packages later).

After making the app unavailable, you'll still see it in Partner Center. If you decide to offer the app to customers again, you can click **Make product available** from the banner on the **App overview** page or you can use the toggle button in **Store presence** card on **App overview** page. After you confirm, the app will be available to new customers (unless restricted by the settings in your last submission) within a few hours.

:::image type="content" source="images/new-overview-make-product-available.png" lightbox="images/new-overview-make-product-available.png" alt-text="A screenshot of the new msix overview page showing how to make product available in Store":::

> [!NOTE]
> If you want to keep your app available, but don't want to continuing offering it to new customers on a particular OS version, you can create a new submission and remove all packages for the OS version on which you want to prevent new acquisitions.

## Removing packages for a previously-supported device family

If you remove all packages for a certain device family (see [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview)) that your app previously supported, you'll be prompted to confirm that this is your intention before you can save your changes on the **Packages** page.

When you publish a submission that removes all of the packages that could run on a device family that your app previously supported, new customers will not be able to acquire the app on that device family. You can always publish another update later to provide packages for that device family again.

Be aware that even if you remove all of the packages that support a certain device family, any existing customers who have already installed the app on that type of device can still use it, and they will get any updates you provide later.
