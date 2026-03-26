---
description: Learn how you can remove an MSIX app from the Store.
title: Removing an app from the Store
ms.date: 03/26/2026
ms.topic: article
ms.localizationpriority: medium
---

# Removing an MSIX app from the Store

At times, you may want to stop offering an app to customers, effectively "unpublishing" it. To do so, navigate to the **Store presence** card on the **App overview** page. You will see that your product is currently available in the Microsoft Store. Click on **Modify availability**, select **Make product unavailable** and click on **Apply**. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it (unless they have a [promotional code](../../generate-promotional-codes.md) and are using a Windows 10 or Windows 11 device).

:::image type="content" source="images/new-overview-make-product-unavailable.png" lightbox="images/new-overview-make-product-unavailable.png" alt-text="A screenshot of the new msix overview page showing how to make product unavailable in Store":::

> [!IMPORTANT]
> This option will override any [visibility](./visibility-options.md#discoverability) settings that you have selected in your submissions.

This option has the same effect as if you created a submission and chose **Make this product available but not discoverable in the Store** with the **Stop acquisition** option. However, it does not require you to create a new submission.

Note that any customers who already have the app will still be able to use it and can download it again (and could even get updates if you submit new packages later).

After making the app unavailable, you'll still see it in Partner Center. If you decide to offer the app to customers again, you can click **Make product available** from the banner on the **App overview** page or you can navigate to **Store presence** card on **App overview** page and **Modify availability** for your product. After you confirm, the app will be available to new customers (unless restricted by the settings in your last submission) within a few hours.

:::image type="content" source="images/new-overview-make-product-available.png" lightbox="images/new-overview-make-product-available.png" alt-text="A screenshot of the new msix overview page showing how to make product available in Store":::

> [!NOTE]
> If you want to keep your app available, but don't want to continuing offering it to new customers on a particular OS version, you can create a new submission and remove all packages for the OS version on which you want to prevent new acquisitions.
