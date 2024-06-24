---
ms.assetid: D1F233EC-24B5-4F84-A92F-2030753E608E
description: Use this method in the Microsoft Store collection API to get all the products that a customer owns for apps that are associated with your Azure AD client ID. You can scope your query to a particular product, or use other filters.
title: Query for products
ms.date: 04/22/2021
ms.topic: article
keywords: windows 10, uwp, Microsoft Store collection API, view products
ms.localizationpriority: medium
---
# Query for products


Use this method in the Microsoft Store collection API to get all the products that a customer owns for apps that are associated with your Azure AD client ID. You can scope your query to a particular product, or use other filters.

This method is designed to be called by your service in response to a message from your app. Your service should not regularly poll for all users on a schedule.

The [Microsoft.StoreServices library](https://github.com/microsoft/Microsoft-Store-Services) provides the functionality of this method through the StoreServicesClient.CollectionsQueryAsync API.

## Prerequisites


To use this method, you will need:

* An Azure AD access token that has the audience URI value `https://onestore.microsoft.com`.
* A Microsoft Store ID key that represents the identity of the user whose products you want to get.

For more information, see [Manage product entitlements from a service](view-and-grant-products-from-a-service.md).

## Request

### Request syntax

| Method | Request URI                                                 |
|--------|-------------------------------------------------------------|
| POST   | ```https://collections.mp.microsoft.com/v6.0/collections/query``` |


### Request header

| Header         | Type   | Description                                                                                           |
|----------------|--------|-------------------------------------------------------------------------------------------------------|
| Authorization  | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;.                           |
| Host           | string | Must be set to the value **collections.mp.microsoft.com**.                                            |
| Content-Length | number | The length of the request body.                                                                       |
| Content-Type   | string | Specifies the request and response type. Currently, the only supported value is **application/json**. |


### Request body

| Parameter         | Type         | Description         | Required |
|-------------------|--------------|---------------------|----------|
| beneficiaries     | list&lt;UserIdentity&gt; | A list of UserIdentity objects that represent the users being queried for products. For more information, see the table below.    | Yes      |
| continuationToken | string       | If there are multiple sets of products, the response body returns a continuation token when the page limit is reached. Provide that continuation token here in subsequent calls to retrieve remaining products.       | No       |
| maxPageSize       | number       | The maximum number of products to return in one response. The default and maximum value is 100.                 | No       |
| modifiedAfter     | datetime     | If specified, the service only returns products that have been modified after this date.        | No       |
| parentProductId   | string       | If specified, the service only returns add-ons that correspond to the specified app.      | No       |
| productSkuIds     | list&lt;ProductSkuId&gt; | If specified, the service only returns products applicable to the provided product/SKU pairs. For more information, see the table below.      | No       |
| productTypes      | list&lt;string&gt;       | Specifies which products types to return in the query results. Supported product types are **Application**, **Durable**, **Game**, and **UnmanagedConsumable**.     | Yes       |
| validityType      | string       | When set to **All**, all products for a user will be returned, including expired items. When set to **Valid**, only products that are valid at this point in time are returned (that is, they have an active status, start date &lt; now, and end date is &gt; now). | No       |


The UserIdentity object contains the following parameters.

| Parameter            | Type   |  Description      | Required |
|----------------------|--------|----------------|----------|
| identityType         | string | Specify the string value **b2b**.    | Yes      |
| identityValue        | string | The [Microsoft Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user for whom you want to query products.  | Yes      |
| localTicketReference | string | The requested identifier for the returned products. Returned items in the response body will have a matching *localTicketReference*. We recommend that you use the same value as the *userId* claim in the Microsoft Store ID key. | Yes      |


The ProductSkuId object contains the following parameters.

| Parameter | Type   | Description          | Required |
|-----------|--------|----------------------|----------|
| productId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) for a [product](in-app-purchases-and-trials.md#products-skus-and-availabilities) in the Microsoft Store catalog. An example Store ID for a product is 9NBLGGH42CFD. | Yes      |
| skuId     | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) for a product's [SKU](in-app-purchases-and-trials.md#products-skus-and-availabilities) in the Microsoft Store catalog. An example Store ID for a SKU is 0010.       | Yes      |


### Request example

```syntax
POST https://collections.mp.microsoft.com/v6.0/collections/query HTTP/1.1
Authorization: Bearer eyJ0eXAiOiJKV1Q…….
Host: collections.mp.microsoft.com
Content-Length: 2531
Content-Type: application/json

{
  "maxPageSize": 100,
  "beneficiaries": [
    {
      "localTicketReference": "1055521810674918",
      "identityValue": "eyJ0eXAiOiJ……",
      "identityType": "b2b"
    }
  ],
  "modifiedAfter": "\/Date(-62135568000000)\/",
  "productSkuIds": [
    {
      "productId": "9NBLGGH5WVP6",
      "skuId": "0010"
    }
  ],
  "productTypes": [
    "UnmanagedConsumable"
  ],
  "validityType": "All"
}
```

## Response


### Response body

| Parameter         | Type                     | Description          | Required |
|-------------------|--------------------------|-----------------------|----------|
| continuationToken | string                   | If there are multiple sets of products, this token is returned when the page limit is reached. You can specify this continuation token in subsequent calls to retrieve remaining products. | No       |
| items             | CollectionItemContractV6 | An array of products for the specified user. For more information, see the table below.        | No       |


The CollectionItemContractV6 object contains the following parameters.

| Parameter            | Type               | Description            | Required |
|----------------------|--------------------|-------------------------|----------|
| acquiredDate         | datetime           | The date on which the user acquired the item.                  | Yes      |
| campaignId           | string             | The campaign ID that was provided at purchase time for this item.                  | No       |
| devOfferId           | string             | The offer ID from an in-app purchase.              | No       |
| endDate              | datetime           | The end date of the item.              | Yes      |
| fulfillmentData      | list\<string>       | N/A         | No       |
| inAppOfferToken      | string             | The developer-specified product ID string that is assigned to the item in Partner Center. An example product ID is *product123*. | No       |
| itemId               | string             | An ID that identifies this collection item from other items the user owns. This ID is unique per product.   | Yes      |
| localTicketReference | string             | The ID of the previously supplied *localTicketReference* in the request body.                  | Yes      |
| modifiedDate         | datetime           | The date this item was last modified.              | Yes      |
| orderId              | string             | If present, the order ID of which this item was obtained.              | No       |
| orderLineItemId      | string             | If present, the line item of the particular order for which this item was obtained.              | No       |
| ownershipType        | string             | The string *OwnedByBeneficiary*.   | Yes      |
| productId            | string             | The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [product](in-app-purchases-and-trials.md#products-skus-and-availabilities) in the Microsoft Store catalog. An example Store ID for a product is 9NBLGGH42CFD.          | Yes      |
| productType          | string             | One of the following product types: **Application**, **Durable**, and **UnmanagedConsumable**.        | Yes      |
| purchasedCountry     | string             | N/A   | No       |
| purchaser            | IdentityContractV6 | If present, this represents the identity of the purchaser of the item. See the details for this object below.        | No       |
| quantity             | number             | The quantity of the item. Currently, this will always be 1.      | No       |
| skuId                | string             | The [Store ID](in-app-purchases-and-trials.md#store-ids) for the product's [SKU](in-app-purchases-and-trials.md#products-skus-and-availabilities) in the Microsoft Store catalog. An example Store ID for a SKU is 0010.     | Yes      |
| skuType              | string             | Type of the SKU. Possible values include **Trial**, **Full**, and **Rental**.        | Yes      |
| startDate            | datetime           | The date that the item starts being valid.       | Yes      |
| status               | string             | The status of the item. Possible values include **Active**, **Expired**, **Revoked**, and **Banned**.    | Yes      |
| tags                 | list\<string>       | N/A    | Yes      |
| transactionId        | guid               | The transaction ID as a result of the purchase of this item. Can be used for reporting an item as fulfilled.      | Yes      |


The IdentityContractV6 object contains the following parameters.

| Parameter     | Type   | Description                                                                        | Required |
|---------------|--------|------------------------------------------------------------------------------------|----------|
| identityType  | string | Contains the value *pub*.                                                      | Yes      |
| identityValue | string | The string value of the *publisherUserId* from the specified Microsoft Store ID key. | Yes      |


### Response example

```syntax
HTTP/1.1 200 OK
Content-Length: 7241
Content-Type: application/json
MS-CorrelationId: 699681ce-662c-4841-920a-f2269b2b4e6c
MS-RequestId: a9988cf9-652b-4791-beba-b0e732121a12
MS-CV: xu2HW6SrSkyfHyFh.0.1
MS-ServerId: 020022359
Date: Tue, 22 Sep 2015 20:28:18 GMT

{
  "items" : [
    {
      "acquiredDate" : "2015-09-22T19:22:51.2068724+00:00",
      "devOfferId" : "f9587c53-540a-498b-a281-8a349491ed47",
      "endDate" : "9999-12-31T23:59:59.9999999+00:00",
      "fulfillmentData" : [],
      "inAppOfferToken" : "consumable2",
      "itemId" : "4b8fbb13127a41f299270ea668681c1d",
      "localTicketReference" : "1055521810674918",
      "modifiedDate" : "2015-09-22T19:22:51.2513155+00:00",
      "orderId" : "4ba5960d-4ec6-4a81-ac20-aafce02ddf31",
      "ownershipType" : "OwnedByBeneficiary",
      "productId" : "9NBLGGH5WVP6",
      "productType" : "UnmanagedConsumable",
      "purchaser" : {
        "identityType" : "pub",
        "identityValue" : "user123"
      },
      "skuId" : "0010",
      "skuType" : "Full",
      "startDate" : "2015-09-22T19:22:51.2068724+00:00",
      "status" : "Active",
      "tags" : [],
      "transactionId" : "4ba5960d-4ec6-4a81-ac20-aafce02ddf31"
    }
  ]
}
```

## Related topics

* [Manage product entitlements from a service](view-and-grant-products-from-a-service.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Grant free products](grant-free-products.md)
* [Renew a Microsoft Store ID key](renew-a-windows-store-id-key.md)
* [Microsoft.StoreServices library (GitHub)](https://github.com/microsoft/Microsoft-Store-Services) 
