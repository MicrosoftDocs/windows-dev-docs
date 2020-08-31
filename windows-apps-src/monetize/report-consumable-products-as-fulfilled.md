---
ms.assetid: E9BEB2D2-155F-45F6-95F8-6B36C3E81649
description: Use this method in the Microsoft Store collection API to report a consumable product as fulfilled for a given customer. Before a user can repurchase a consumable product, your app or service must report the consumable product as fulfilled for that user.
title: Report consumable products as fulfilled
ms.date: 03/19/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store collection API, fulfill, consumable
ms.localizationpriority: medium
---
# Report consumable products as fulfilled

Use this method in the Microsoft Store collection API to report a consumable product as fulfilled for a given customer. Before a user can repurchase a consumable product, your app or service must report the consumable product as fulfilled for that user.

There are two ways you can use this method to report a consumable product as fulfilled:

* Provide the item ID of the consumable (as returned in the **itemId** parameter of a [query for products](query-for-products.md)), and a unique tracking ID that you provide. If the same tracking ID is used for multiple tries, the same result will be returned even if the item is already consumed. If you aren't certain if a consume request was successful, your service should resubmit consume requests with the same tracking ID. The tracking ID will always be tied to that consume request and can be resubmitted indefinitely.
* Provide the product ID (as returned in the **productId** parameter of a [query for products](query-for-products.md)) and a transaction ID that is obtained from one of the sources listed in the description for the **transactionId** parameter in the request body section below.

## Prerequisites


To use this method, you will need:

* An Azure AD access token that has the audience URI value `https://onestore.microsoft.com`.
* A Microsoft Store ID key that represents the identity of the user for whom you want to report a consumable product as fulfilled.

For more information, see [Manage product entitlements from a service](view-and-grant-products-from-a-service.md).

## Request


### Request syntax

| Method | Request URI                                                   |
|--------|---------------------------------------------------------------|
| POST   | ```https://collections.mp.microsoft.com/v6.0/collections/consume``` |


### Request header

| Header         | Type   | Description                                                                                           |
|----------------|--------|-------------------------------------------------------------------------------------------------------|
| Authorization  | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;.                           |
| Host           | string | Must be set to the value **collections.mp.microsoft.com**.                                            |
| Content-Length | number | The length of the request body.                                                                       |
| Content-Type   | string | Specifies the request and response type. Currently, the only supported value is **application/json**. |


### Request body

| Parameter     | Type         | Description         | Required |
|---------------|--------------|---------------------|----------|
| beneficiary   | UserIdentity | The user for which this item is being consumed. For more information, see the following table.        | Yes      |
| itemId        | string       | The *itemId* value returned by a [query for products](query-for-products.md). Use this parameter with *trackingId*      | No       |
| trackingId    | guid         | A unique tracking ID provided by developer. Use this parameter with *itemId*.         | No       |
| productId     | string       | The *productId* value returned by a [query for products](query-for-products.md). Use this parameter with *transactionId*   | No       |
| transactionId | guid         | A transaction ID value that is obtained from one of the following sources. Use this parameter with *productId*.<ul><li>The [TransactionID](/uwp/api/windows.applicationmodel.store.purchaseresults.transactionid) property of the [PurchaseResults](/uwp/api/Windows.ApplicationModel.Store.PurchaseResults) class.</li><li>The app or product receipt that is returned by [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync), [RequestAppPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestapppurchaseasync), or [GetAppReceiptAsync](/uwp/api/windows.applicationmodel.store.currentapp.getappreceiptasync).</li><li>The *transactionId* parameter returned by a [query for products](query-for-products.md).</li></ul>   | No       |


The UserIdentity object contains the following parameters.

| Parameter            | Type   | Description       | Required |
|----------------------|--------|-------------------|----------|
| identityType         | string | Specify the string value **b2b**.    | Yes      |
| identityValue        | string | The [Microsoft Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user for whom you want to report a consumable product as fulfilled.      | Yes      |
| localTicketReference | string | The requested identifier for the returned response. We recommend that you use the same value as the *userId*  [claim](view-and-grant-products-from-a-service.md#claims-in-a-microsoft-store-id-key) in the Microsoft Store ID key. | Yes      |


### Request examples

The following example uses *itemId* and *trackingId*.

```syntax
POST https://collections.mp.microsoft.com/v6.0/collections/consume HTTP/1.1
Authorization: Bearer eyJ0eXAiOiJKV1…..
Host: collections.mp.microsoft.com
Content-Length: 2050
Content-Type: application/json

{
    "beneficiary": {
        "localTicketReference": "testreference",
        "identityValue": "eyJ0eXAiOi…..",
        "identityType": "b2b"
    },
    "itemId": "44c26106-4979-457b-af34-609ae97a084f",
    "trackingId": "44db79ca-e31d-49e9-8896-fa5c7f892b40"
}
```

The following example uses *productId* and *transactionId*.

```syntax
POST https://collections.mp.microsoft.com/v6.0/collections/consume HTTP/1.1
Authorization: Bearer eyJ0eXAiOiJKV1……
Content-Length: 1880
Content-Type: application/json
Host: collections.md.mp.microsoft.com

{
    "beneficiary" : {
        "localTicketReference" : "testReference",
        "identityValue" : "eyJ0eXAiOiJ…..",
        "identitytype" : "b2b"
    },
    "productId" : "9NBLGGH5WVP6",
    "transactionId" : "08a14c7c-1892-49fc-9135-190ca4f10490"
}
```


## Response

No content will be returned if the consumption was executed successfully.

### Response example

```syntax
HTTP/1.1 204 No Content
Content-Length: 0
MS-CorrelationId: 386f733d-bc66-4bf9-9b6f-a1ad417f97f0
MS-RequestId: e488cd0a-9fb6-4c2c-bb77-e5100d3c15b1
MS-CV: 5.1
MS-ServerId: 030011326
Date: Tue, 22 Sep 2015 20:40:55 GMT
```

## Error codes


| Code | Error        | Inner error code           | Description           |
|------|--------------|----------------------------|-----------------------|
| 401  | Unauthorized | AuthenticationTokenInvalid | The Azure AD access token is invalid. In some cases the details of the ServiceError will contain more information, such as when the token is expired or the *appid* claim is missing. |
| 401  | Unauthorized | PartnerAadTicketRequired   | An Azure AD access token was not passed to the service in the authorization header.                                                                                                   |
| 401  | Unauthorized | InconsistentClientId       | The *clientId* claim in the Microsoft Store ID key in the request body and the *appid* claim in the Azure AD access token in the authorization header do not match.                     |

<span/> 

## Related topics

* [Manage product entitlements from a service](view-and-grant-products-from-a-service.md)
* [Query for products](query-for-products.md)
* [Grant free products](grant-free-products.md)
* [Renew a Microsoft Store ID key](renew-a-windows-store-id-key.md)