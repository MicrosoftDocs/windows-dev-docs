---
ms.assetid: A4C6098B-6CB9-4FAF-B2EA-50B03D027FF1
description: Use this method in the Microsoft Store targeted offers API to get the targeted offers that are available for the current user in the context of the current app.
title: Get targeted offers
ms.date: 10/10/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store targeted offers API, get targeted offers
ms.localizationpriority: medium
---
# Get targeted offers

Use this method to get the targeted offers that are available for the current user, based on whether or not the user is part of the customer segment for the targeted offer. For more information, see [Manage targeted offers using Store services](manage-targeted-offers-using-windows-store-services.md).

## Prerequisites

To use this method, you need to first [obtain a Microsoft Account token](manage-targeted-offers-using-windows-store-services.md#obtain-a-microsoft-account-token) for the current signed-in user of your app. You must pass this token in the ```Authorization``` request header for this method. This token is used by the Store to get targeted offers for the current user.

## Request


### Request syntax

| Method | Request URI                                                                |
|--------|----------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user``` |


### Request header

| Header        | Type   | Description  |
|---------------|--------|--------------|
| Authorization | string | Required. The Microsoft Account token for the current signed-in user of your app in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

This method has no URI or query parameters.

### Request example

```syntax
GET https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user HTTP/1.1
Authorization: Bearer <Microsoft Account token>
```

## Response

This method returns a JSON-formatted response body that contains an array of objects with the following fields. Each object in the array represents the targeted offers that are available for the specified user as part of a particular customer segment.

| Field      | Type   | Description         |
|------------|--------|------------------|
| offers      | array  | An array of product IDs for the add-ons that are associated with the targeted offers that are available for the current user. These product IDs are specified in the **Targeted offers** page for your app in Partner Center.            |
| trackingId  | string | A GUID that you can optionally use to keep track of the targeted offer in your own code or services. |


### Example

The following example demonstrates an example JSON response body for this request.

```json
[
  {
    "offers": [
      "10x gold coins",
      "100x gold coins"
    ],
    "trackingId": "5de5dd29-6dce-4e68-b45e-d8ee6c2cd203"
  }
]
```

## Related topics

* [Manage targeted offers using Store services](manage-targeted-offers-using-windows-store-services.md)

 

 
