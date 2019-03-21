---
ms.assetid: 16D4C3B9-FC9B-46ED-9F87-1517E1B549FA
description: Use this method in the Microsoft Store submission API to delete an add-on for an app that is registered to your Partner Center account.
title: Delete an add-on
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, add-on, delete, in-app product, IAP
ms.localizationpriority: medium
---
# Delete an add-on

Use this method in the Microsoft Store submission API to delete an add-on (also known as in-app product or IAP) for an app that is registered to your Partner Center account.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| DELETE    | `https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{inAppProductId}` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| id | string | Required. The Store ID of the add-on to delete. The Store ID is available in Partner Center.  |


### Request body

Do not provide a request body for this method.


### Request example

The following example demonstrates how to delete an add-on.

```json
DELETE https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/9NBLGGH4TNMP HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

If successful, this method returns an empty response body.

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description                                                                                                                                                                           |
|--------|------------------|
| 400  | The request is invalid. |
| 404  | The specified add-on could not be found.  |
| 409  | The specified add-on was found but it could not be deleted in its current state, or the add-on uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |   


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get all add-ons](get-all-add-ons.md)
* [Get an add-on](get-an-add-on.md)
* [Create an add-on](create-an-add-on.md)
