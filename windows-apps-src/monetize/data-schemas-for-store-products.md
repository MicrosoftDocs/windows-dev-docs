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
ms.localizationpriority: medium
---

# Data schemas for Store products

When you submit a product such as an app or add-on to the Store, the Store maintains a comprehensive set of data for the product and its licenses. In your app's code, you can programmatically access some of this data by using properties in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace. For example, you can retrieve the description and price of the current app or an add-on for the current app by using the [StoreProduct.Description](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct.Description) and [StoreProduct.Price](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct.Price) properties.

However, much of the data for products in the Store is not exposed by predefined properties in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace. To access the complete data for a product in your code, you can use the following general properties instead:

* [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct.ExtendedJsonData)
* [StoreSku.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storesku.ExtendedJsonData)
* [StoreAvailability.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeavailability.ExtendedJsonData)
*	[StoreCollectionData.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storecollectiondata.ExtendedJsonData)
*	[StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense.ExtendedJsonData)
* [StoreLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storelicense.ExtendedJsonData)
*	[StorePurchaseProperties.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storepurchaseproperties.ExtendedJsonData)

These properties return the complete data for the corresponding object as a JSON-formatted string. This article provides the complete schema for the data returned by these properties.

> [!NOTE]
> Products in the Store are organized in a hierarchy of [product](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct), [SKU](https://docs.microsoft.com/uwp/api/windows.services.store.storesku), and [availability](https://docs.microsoft.com/uwp/api/windows.services.store.storeavailability) objects. For more information, see [Products, SKUs, and availabilities](in-app-purchases-and-trials.md#products-skus).

## Schema for StoreProduct, StoreSku, StoreAvailability, and StoreCollectionData

The following schema describes the JSON-formatted string returned by [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct.ExtendedJsonData). The [StoreSku.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storesku.ExtendedJsonData), [StoreAvailability.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeavailability.ExtendedJsonData), and [StoreCollectionData.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storecollectiondata.ExtendedJsonData) properties return only the portions of the schema that are defined under the ```DisplaySkuAvailabilities\Sku```, ```DisplaySkuAvailabilities\Availabilities```, and ```DisplaySkuAvailabilities\Sku\CollectionData``` paths, respectively.

For an example of a JSON-formatted string returned by [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct.ExtendedJsonData), see [this section](#product-example).

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreProduct.ExtendedJsonData.json#L1-L729)]

<span id="product-example" />

### Example

The following example demonstrates a JSON-formatted string returned by the [StoreProduct.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct.ExtendedJsonData) property for app.

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreProduct.ExtendedJsonDataExample.json#L1-L268)]

## Schema for StoreAppLicense and StoreLicense

The following schema describes the JSON-formatted string returned by [StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense.ExtendedJsonData). The [StoreLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storelicense.ExtendedJsonData) property returns only the portions of the schema that are defined under the ```productAddOns``` path.

For an example of a JSON-formatted string returned by [StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense.ExtendedJsonData), see [this section](#license-example).

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreAppLicense.ExtendedJsonData.json#L1-L80)]

<span id="license-example" />

### Example

The following example demonstrates a JSON-formatted string returned by the [StoreAppLicense.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense.ExtendedJsonData) property for app.

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StoreAppLicense.ExtendedJsonDataExample.json#L1-L28)]

## Schema for StorePurchaseProperties

The following schema describes the JSON-formatted string returned by [StorePurchaseProperties.ExtendedJsonData](https://docs.microsoft.com/uwp/api/windows.services.store.storepurchaseproperties.ExtendedJsonData).

[!code[ExtendedJsonDataSchema](./code/InAppPurchasesAndLicenses_RS1/json/StorePurchaseProperties.ExtendedJsonData.json#L1-L12)]

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
