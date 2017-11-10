---
author: mcleanbyron
description: Describes the extended JSON data schema for Store products in the Windows.Services.Store namespace.
title: Data schemas for Store products
ms.author: mcleans
ms.date: 09/26/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ExtendedJsonData, Store products, schema
localizationpriority: medium
---

# Data schemas for Store products

When you submit a product such as an app or add-on to the Store, the Store maintains a comprehensive set of data for the product and its licenses. In your app's code, you can programmatically access some of this data by using properties in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace. For example, you can retrieve the description and price of the current app or an add-on for the current app by using the [StoreProduct.Description](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_Description) and [StoreProduct.Price](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_Price) properties.

However, much of the data for products in the Store is not exposed by predefined properties in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace. To access the complete data for a product in your code, you can use the following general properties instead:

* [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_ExtendedJsonData_)
* [StoreSku.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storesku#Windows_Services_Store_StoreSku_ExtendedJsonData_)
* [StoreAvailability.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeavailability#Windows_Services_Store_StoreAvailability_ExtendedJsonData_)
*	[StoreCollectionData.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storecollectiondata#Windows_Services_Store_StoreCollectionData_ExtendedJsonData_)
*	[StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense#Windows_Services_Store_StoreAppLicense_ExtendedJsonData_)
* [StoreLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storelicense#Windows_Services_Store_StoreLicense_ExtendedJsonData_)
*	[StorePurchaseProperties.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storepurchaseproperties#Windows_Services_Store_StorePurchaseProperties_ExtendedJsonData_)

These properties return the complete data for the corresponding object as a JSON-formatted string. This article provides the complete schema for the data returned by these properties.

> [!NOTE]
> Products in the Store are organized in a hierarchy of [product](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct), [SKU](https://docs.microsoft.com/uwp/api/windows.services.store.storesku), and [availability](https://docs.microsoft.com/uwp/api/windows.services.store.storeavailability) objects. For more information, see [Products, SKUs, and availabilities](in-app-purchases-and-trials.md#products-skus).

## Schema for StoreProduct, StoreSku, StoreAvailability, and StoreCollectionData

The following schema describes the JSON-formatted string returned by [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_ExtendedJsonData_). The [StoreSku.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storesku#Windows_Services_Store_StoreSku_ExtendedJsonData_), [StoreAvailability.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeavailability#Windows_Services_Store_StoreAvailability_ExtendedJsonData_), and [StoreCollectionData.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storecollectiondata#Windows_Services_Store_StoreCollectionData_ExtendedJsonData_) properties return only the portions of the schema that are defined under the ```DisplaySkuAvailabilities\Sku```, ```DisplaySkuAvailabilities\Availabilities```, and ```DisplaySkuAvailabilities\Sku\CollectionData``` paths, respectively.

For an example of a JSON-formatted string returned by [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_ExtendedJsonData_), see [this section](#product-example).

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreProduct.ExtendedJsonData.json#L1-L603)]

<span id="product-example" />
### Example

The following example demonstrates a JSON-formatted string returned by the [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_ExtendedJsonData_) property for app.

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreProduct.ExtendedJsonDataExample.json#L1-L268)]

## Schema for StoreAppLicense and StoreLicense

The following schema describes the JSON-formatted string returned by [StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense#Windows_Services_Store_StoreAppLicense_ExtendedJsonData_). The [StoreLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storelicense#Windows_Services_Store_StoreLicense_ExtendedJsonData_) property returns only the portions of the schema that are defined under the ```productAddOns``` path.

For an example of a JSON-formatted string returned by [StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense#Windows_Services_Store_StoreAppLicense_ExtendedJsonData_), see [this section](#license-example).

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreAppLicense.ExtendedJsonData.json#L1-L80)]

<span id="license-example" />
### Example

The following example demonstrates a JSON-formatted string returned by the [StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense#Windows_Services_Store_StoreAppLicense_ExtendedJsonData_) property for app.

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreAppLicense.ExtendedJsonDataExample.json#L1-L28)]

## Schema for StorePurchaseProperties

The following schema describes the JSON-formatted string returned by [StorePurchaseProperties.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storepurchaseproperties#Windows_Services_Store_StorePurchaseProperties_ExtendedJsonData_).

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StorePurchaseProperties.ExtendedJsonData.json#L1-L12)]

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
