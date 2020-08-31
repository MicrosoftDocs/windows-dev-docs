---
ms.assetid: E59FB6FE-5318-46DF-B050-73F599C3972A
description: Use this method in the Microsoft Store submission API to retrieve information about the in-app purchases for an app that is registered to your Partner Center.
title: Get add-ons for an app
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, add-ons, in-app products, IAPs
ms.localizationpriority: medium
---
# Get add-ons for an app

Use this method in the Microsoft Store submission API to list the add-ons for an app that is registered to your Partner Center account.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/listinappproducts` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters


|  Name  |  Type  |  Description  |  Required  |
|------|------|------|------|
|  applicationId  |  string  |  The Store ID of the app for which you want to retrieve the add-ons. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |  Yes  |
|  top  |  int  |  The number of items to return in the request (that is, the number of add-ons to return). If the app has more add-ons than the value you specify in the query, the response body includes a relative URI path that you can append to the method URI to request the next page of data.  |  No  |
|  skip |  int  | The number of items to bypass in the query before returning the remaining items. Use this parameter to page through data sets. For example, top=10 and skip=0 retrieves items 1 through 10, top=10 and skip=10 retrieves items 11 through 20, and so on.   |  No  |


### Request body

Do not provide a request body for this method.

### Request examples

The following example demonstrates how to list all the add-ons for an app.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/listinappproducts HTTP/1.1
Authorization: Bearer <your access token>
```

The following example demonstrates how to list the first 10 add-ons for an app.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/listinappproducts?top=10 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body returned by a successful request for the first 10 add-ons for an app with 53 total add-ons. For brevity, this example only shows the data for the first three add-ons returned by the request. For more details about the values in the response body, see the following section.

```json
{
  "@nextLink": "applications/9NBLGGH4R315/listinappproducts/?skip=10&top=10",
  "value": [
    {
      "inAppProductId": "9NBLGGH4TNMP"
    },
    {
      "inAppProductId": "9NBLGGH4TNMN"
    },
    {
      "inAppProductId": "9NBLGGH4TNNR"
    },
    // Next 7 add-ons  are omitted for brevity ...
  ],
  "totalCount": 53
}
```

### Response body

| Value      | Type   | Description                                                                                                                                                                                                                                                                         |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| @nextLink  | string | If there are additional pages of data, this string contains a relative path that you can append to the base `https://manage.devcenter.microsoft.com/v1.0/my/` request URI to request the next page of data. For example, if the *top* parameter of the initial request body is set to 10 but there are 50 add-ons for the app, the response body will include a @nextLink value of `applications/{applicationid}/listinappproducts/?skip=10&top=10`, which indicates that you can call `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationid}/listinappproducts/?skip=10&top=10` to request the next 10 add-ons. |
| value      | array  | An array of objects that list the Store ID of each add-on for the specified app. For more information about the data in each object, see [add-on resource](get-app-data.md#add-on-object).                                                                                                                           |
| totalCount | int    | The total number of rows in the data result for the query (that is, the total number of add-ons for the specified app).    |


## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 404  | No add-ons were found. |
| 409  | The add-ons use Partner Center features that are [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported).  |


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get all apps](get-all-apps.md)
* [Get an app](get-an-app.md)
* [Get package flights for an app](get-flights-for-an-app.md)