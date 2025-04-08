---
description: System requirements page helps you indicate if certain hardware features are required or recommended to run and interact with your MSIX app properly.
title: System requirements for MSIX app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# System requirements for MSIX app

In this section, you have the option to indicate if certain hardware features are required or recommended to run and interact with your app properly. You can check the box (or indicate the appropriate option) for each hardware item where you would like to specify **Minimum hardware** and/or **Recommended hardware**.

:::image type="content" source="images/msix-system-requirements.png" lightbox="images/msix-system-requirements.png" alt-text="A screenshot showing the system requirements table section for MSIX/PWA app.":::

If you make selections for **Recommended hardware**, those items will be displayed in your product's Store listing as recommended hardware for customers on Windows 10, version 1607 or later. Customers on earlier OS versions will not see this info.

If you make selections for **Minimum hardware**, those items will be displayed in your product's Store listing as required hardware for customers on Windows 10, version 1607 or later. Customers on earlier OS versions will not see this info. The Store may also display a warning to customers who are viewing your app's listing on a device that doesn’t have the required hardware. This won't prevent people from downloading your app on devices that don't have the appropriate hardware, but they won't be able to rate or review your app on those devices.

The behavior for customers will vary depending on the specific requirements and the customer's version of Windows:

- **For customers on Windows 10, version 1607 or later:**
  - All minimum and recommended requirements will be displayed in the Store listing.
  - The Store will check for all minimum requirements and will display a warning to customers on a device that doesn't meet the requirements.
- **For customers on earlier versions of Windows 10:**
  - For most customers, all minimum and recommended hardware requirements will be displayed in the Store listing (though customers viewing an older versions of the Store client will only see the minimum hardware requirements).
  - The Store will attempt to verify items that you designate as **Minimum hardware**, with the exception of **Memory**, **DirectX**, **Video memory**, **Graphics**, and **Processor**; none of those will be verified, and customers won't see any warning on devices which don't meet those requirements.

We also recommend adding runtime checks for the specified hardware into your app, since the Store may not always be able to detect that a customer's device is missing the selected feature(s) and they could still be able to download your app even if a warning is displayed. If you want to completely prevent your UWP app from being downloaded on a device which doesn't meet minimum requirements for memory or DirectX level, you can designate the minimum requirements in a [StoreManifest XML file](/uwp/schemas/storemanifest/storemanifestschema2015/schema-root).

> [!TIP]
> If your product requires additional items that aren't listed in this section in order to run properly, such as 3D printers or USB devices, you can also enter [additional system requirements](./add-and-edit-store-listing-info.md#additional-system-requirements) when you create your Store listing.
