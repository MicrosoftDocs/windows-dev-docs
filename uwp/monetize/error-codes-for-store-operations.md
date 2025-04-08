---
description: This article describes common error codes for Store operations for apps and add-ons, including in-app purchasing, licensing, and self-install app updates.
title: Error codes for Store operations
ms.date: 03/07/2024
ms.topic: article
keywords: windows 10, uwp, in-app purchases, IAPs, add-ons, error codes
ms.localizationpriority: medium
---
# Error codes for Store operations

<!-- confirm whether symbolic names are defined for app developers, or do they just handle direct error code values -->

This article describes common error codes that you might encounter while you are developing or testing Store-related operations in your app.

## In-app purchase error codes

The following error codes are related to in-app purchase operations.

|  Error code  |  Description  |
|--------------|---------------|
| 0x803F6100   | The in-app purchase could not be completed because Kid's Corner is active. To complete the purchase, sign in to the device with your Microsoft account and run the application again.               |
| 0x803F6101   | The specified app could not be found. The app may no longer be available in the Store, or you might have provided the wrong Store ID for the app.     |
| 0x803F6102   | The specified add-on could not be found. The add-on may no longer be available in the Store, or you  might have provided the wrong Store ID for the add-on.                                               |
| 0x803F6103   | The specified product could not be found. The product may no longer be available in the Store, or you might have provided the wrong Store ID for the product.                                          |
| 0x803F6104   | The in-app purchase could not be completed because you are running a trial version of the app. To complete in-app purchases, install the full version of the app.               |
| 0x803F6105   | The in-app purchase could not be completed because you are not signed in with your Microsoft account.                                              |
| 0x803F6107   | Something unexpected happened while processing the current operation.                                             |
| 0x803F6108   | The in-app purchase could not be completed because the app license is missing information. This error can occur when you side-load your app. To resolve this issue, uninstall the app and then reinstall it from the Store to refresh the app license.                                          |
| 0x803F6109   | The consumable add-on fulfillment could not be completed because the specified quantity is more than the remaining balance.        |
| 0x803F610A   | The specified provider type for the Store user account is not supported.                                            |
| 0x803F610B   | The specified Store operation is not supported.                                             |
| 0x803F610C   | The app does not support the specified background task contract.                                             |
| 0x80040001   | The provided list of add-on product IDs is invalid.                        |
| 0x80040002   | The provided list of keywords is invalid.                   |
| 0x80040003   | The fulfillment target is invalid.                       |

## Licensing error codes

The following error codes are related to licensing operations for apps or add-ons.

|  Error code  |  Description  |
|--------------|---------------|
| 0x803F700C   | The device is currently offline. To use this app while the device is offline, open your Store settings and toggle the **Offline Permissions** setting.            |
| 0x803F8001   | You do not have an entitlement for the product. You might be using a different Microsoft account than the one that was used to purchase the product.           |
| 0x803F8002   | Your entitlement for the product has expired.           |
| 0x803F8003   | Your entitlement for the product is in an invalid state that prevents a license from being created.   |
| 0x803F8009<br/>0x803F800A   | The trial period for the app has expired.   |
| 0x803F8190   |  The license doesn't allow the product to be used in the current country or region of your device.  |
| 0x803F81F5<br/>0x803F81F6<br/>0x803F81F7<br/>0x803F81F8<br/>0x803F81F9   |  You have reached the maximum number of devices that can be used with games and apps from the Store. To use this game or app on the current device, first remove another device from your account.  |
| 0x803F9000<br/>0x803F9001    |  The license is expired or corrupt. To help resolve this error, try running the [troubleshooter for Windows apps](https://support.microsoft.com/help/4027498/microsoft-store-fix-problems-with-apps) to reset the Store cache.     |
| 0x803F9006    |  The operation could not be completed because the user who is entitled to this product is not signed in to the device with their Microsoft account.            |
| 0x803F9008<br/>0x803F9009    |  Your device is offline. Your device needs to be online to use this product.            |
| 0x803F900A    |  The subscription has expired.            |


## Self-install update error codes

The following error codes are related to [self-installing package updates](../packaging/self-install-package-updates.md).

|  Error code  |  Description  |
|--------------|---------------|
| 0x803F6200   | User consent is required to download the package update.               |
| 0x803F6201   | User consent is required to download and install the package update.                                                  |
| 0x803F6203   | User consent is required to install the package update.                                         |
| 0x803F6204   | User consent is required to download the package update because the download will occur on a metered network connection.                                             |
| 0x803F6206   | User consent is required to download and install the package update because the download will occur on a metered network connection.     |


## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Enable subscription add-ons for your app](enable-subscription-add-ons-for-your-app.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
