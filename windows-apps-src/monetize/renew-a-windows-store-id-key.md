---
ms.assetid: 3569C505-8D8C-4D85-B383-4839F13B2466
description: Learn how to renew an expired Microsoft Store ID key using the renew method in the Microsoft Store collection and purchase APIs.
title: Renew a Microsoft Store ID key
ms.date: 03/19/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store collection API, Microsoft Store purchase API, Microsoft Store ID key, renew
ms.localizationpriority: medium
---
# Renew a Microsoft Store ID key


Use this method to renew a Microsoft Store key. When you [generate a Microsoft Store ID key](view-and-grant-products-from-a-service.md#step-4), the key is valid for 90 days. After the key expires, you can use the expired key to renegotiate a new key by using this method.

## Prerequisites


To use this method, you will need:

* An Azure AD access token that has the audience URI value `https://onestore.microsoft.com`.
* An expired Microsoft Store ID key that was [generated from client-side code in your app](view-and-grant-products-from-a-service.md#step-4).

For more information, see [Manage product entitlements from a service](view-and-grant-products-from-a-service.md).

## Request

### Request syntax

| Key type    | Method | Request URI                                              |
|-------------|--------|----------------------------------------------------------|
| Collections | POST   | ```https://collections.mp.microsoft.com/v6.0/b2b/keys/renew``` |
| Purchase    | POST   | ```https://purchase.mp.microsoft.com/v6.0/b2b/keys/renew```    |


### Request header

| Header         | Type   | Description                                                                                           |
|----------------|--------|-------------------------------------------------------------------------------------------------------|
| Host           | string | Must be set to the value **collections.mp.microsoft.com** or **purchase.mp.microsoft.com**.           |
| Content-Length | number | The length of the request body.                                                                       |
| Content-Type   | string | Specifies the request and response type. Currently, the only supported value is **application/json**. |


### Request body

| Parameter     | Type   | Description                       | Required |
|---------------|--------|-----------------------------------|----------|
| serviceTicket | string | The Azure AD access token.        | Yes      |
| key           | string | The expired Microsoft Store ID key. | Yes       |


### Request example

```syntax
POST https://collections.mp.microsoft.com/v6.0/b2b/keys/renew HTTP/1.1
Content-Length: 2774
Content-Type: application/json
Host: collections.mp.microsoft.com

{
    "serviceTicket": "eyJ0eXAiOiJKV1QiLCJhb….",
    "Key": "eyJ0eXAiOiJKV1QiLCJhbG…."
}
```

## Response


### Response body

| Parameter | Type   | Description                                                                                                            |
|-----------|--------|------------------------------------------------------------------------------------------------------------------------|
| key       | string | The refreshed Microsoft Store key that can be used in future calls to the Microsoft Store collections API or purchase API. |


### Response example

```syntax
HTTP/1.1 200 OK
Content-Length: 1646
Content-Type: application/json
MS-CorrelationId: bfebe80c-ff89-4c4b-8897-67b45b916e47
MS-RequestId: 1b5fa630-d672-4971-b2c0-3713f4ea6c85
MS-CV: xu2HW6SrSkyfHyFh.0.0
MS-ServerId: 030011428
Date: Tue, 13 Sep 2015 07:31:12 GMT

{
    "key":"eyJ0eXAi….."
}
```

## Error codes


| Code | Error        | Inner error code           | Description   |
|------|--------------|----------------------------|---------------|
| 401  | Unauthorized | AuthenticationTokenInvalid | The Azure AD access token is invalid. In some cases the details of the ServiceError will contain more information, such as when the token is expired or the *appid* claim is missing. |
| 401  | Unauthorized | InconsistentClientId       | The *clientId* claim in the Microsoft Store ID key and the *appid* claim in the Azure AD access token do not match.                                                                     |


## Related topics


* [Manage product entitlements from a service](view-and-grant-products-from-a-service.md)
* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Grant free products](grant-free-products.md)
