---
author: mcleanbyron
ms.assetid: 19DB80B4-165F-4A88-9B31-17D1C454AEA6
description: Use this method in the Windows Store targeted offers API to claim the purchase of a targeted offer for the current user.
title: Claim a targeted offer
ms.author: mcleans
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Store services, Windows Store targeted offers API, claim targeted offers
---

# Claim a targeted offer

Use this method in the Windows Store offers API to submit a claim to the Store for a targeted offer that is associated with a Microsoft-sponsored promotion that pays a bonus to developers. If the targeted offer is not associated with a promotion, you do not need to call this method. For more information, see [Manage targeted offers using Store services](manage-targeted-offers-using-windows-store-services.md).

> [!NOTE]
> The ability to associate a targeted offer with a Microsoft-sponsored promotion and then submit a claim for the offer is currently not available to all developer accounts.

## Prerequisites

To use this method, you need to first do the following:

* [Obtain a Microsoft Account token](manage-targeted-offers-using-windows-store-services.md#obtain-a-microsoft-account-token) for the current signed-in user of your app. You must pass this token in the ```Authorization``` request header for this method. This token is used by the Store to claim the targeted offer for the current user.
* Call the [get targeted offers](get-targeted-offers.md) method to get the targeted offers that are currently available the current user, including the tracking ID to use to claim a targeted offer for the current user.

## Request


### Request syntax

| Method | Request URI                                                                |
|--------|----------------------------------------------------------------------------|
| POST    | ```https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user``` |

<span/> 

### Request header

| Header        | Type   | Description          |
|---------------|--------|--------------|
| Authorization | string | Required. The Microsoft Account token for the current signed-in user of your app in the form **Bearer** &lt;*token*&gt;. |

<span/> 

### Request parameters

This method has no URI or query parameters.

### Request body

This method requires a request body with the following fields.

| Field      | Type   | Description         |
|------------|--------|------------------|
| storeOffer      | object | An object that contains one element in the array that is returned by the [get targeted offers](get-targeted-offers.md) method, including the *offers* and *trackingId* fields. This object is used by the Store to track the conversion for the targeted offer.             |
| receipt  | string | One of the following values that provides confirmation that the customer purchased the add-on that is associated with the targeted offer:<ul><li>If you used one of the **RequestPurchaseAsync** methods in the [Windows.Services.Store](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store) namespace to purchase the add-on, assign the order ID of the add-on purchase to this field.</li><li>If you used the [RequestProductPurchaseAsync](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store.CurrentApp#Windows_ApplicationModel_Store_CurrentApp_RequestProductPurchaseAsync_System_String_) method in the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace to purchase the add-on, assign the receipt for the add-on purchase to this field.</li></ul>For more information, see [Submit a claim for a targeted offer](manage-targeted-offers-using-windows-store-services.md#claim-targeted-offer). |

<span/>

### Request example

The following examples demonstrates how to claim a targeted offer for an add-on purchase that was completed by using the [RequestProductPurchaseAsync](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store.CurrentApp#Windows_ApplicationModel_Store_CurrentApp_RequestProductPurchaseAsync_System_String_) method in the **Windows.ApplicationModel.Store** namespace. This example passes the receipt for the add-on purchase to the *receipt* field of the request body.

```syntax
POST https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user HTTP/1.1
Authorization: Bearer <your access token>
Content-Type: application/json
{
  "storeOffer": {
    "offers": [
      "10x gold coins",
      "100x gold coins"
    ],
    "trackingId": "5de5dd29-6dce-4e68-b45e-d8ee6c2cd203"
  },
  "receipt": "<?xml version=\"1.0\"?><Receipt Version=\"2.0\" CertificateId=\"A656B9B1B3AA509EEA30222E6D5E7DBDA9822DCD\" xmlns=\"http://schemas.microsoft.com/windows/2012/store/receipt\"><ProductReceipt PurchasePrice=\"USD0\" PurchaseDate=\"2016-07-13T17:20:41.509Z\" Id=\"6bbf4366-6fb2-8be8-7947-92fd5f683530\" AppId=\"55428GreenlakeApps.CurrentAppSimulatorEventTest_z7q3q7z11crfr\" ProductId=\"Durable_AddOn_Free\" ProductType=\"Durable\" PublisherUserId=\"haTh5hLAA//HcCdC4oHTCxfbpNyAUpycpJBLxdU0MuA=\" MicrosoftProductId=\"47b43b6a-f2ca-40f6-a18f-9846405134b6\" MicrosoftAppId=\"47b43b6a-f2ca-40f6-a18f-9846405134b6\" ExpirationDate=\"2016-07-14T17:20:41.509Z\" /><Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256\" /><Reference URI=\"\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" /></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#sha256\" /><DigestValue>4Fjn+dWh7ooUSz8V6KBORO+BJiyFZi75nWvmadsUIVU=</DigestValue></Reference></SignedInfo><SignatureValue>0+pOMaCpVqmeU0J2fRwGn2b6INkXxOKtIr39m/Rr7xXMSCudSer1nqs/RUWIKhUxspyu08zS4UW0L48TRP7LyzE4lBl5KCDYtdHffqivqWi0Hd5fR1W6Y+tlP7fLXEqkEoLUIjl0Zp9vCFilAPwDdwF7R5HoLZdpF1brYHHkkK/LsaoDlDVVfIU8EQf/YnYQJDwh4dYGBQLrQg/dCaW1MVu98IVJgRvaFCzYWRq+3NCJnHvw00K9DKYkM/1dydy/b7n5QfrczLF3q1aFLC3n5SbPbsbVXJXofp6yewjulCMjnSVI5Jf5XZ3vzCjQEZUFJPJjrHzrmVqDCCt8pQR5yg==</SignatureValue></Signature></Receipt>"
}
```

## Response

This method does not return a response body. The response header includes an HTTP status code that indicates whether the request succeeded or failed.

### Error codes

| Code | Error        | Description   |
|------|--------------|---------------|
| 401  | Unauthorized | The Microsoft Account token could not be validated. |

<span/> 

## Related topics

* [Manage targeted offers using Store services](manage-targeted-offers-using-windows-store-services.md)
* [Get targeted offers](get-targeted-offers.md)

 

 
