---
ms.assetid: 7B6A99C6-AC86-41A1-85D0-3EB39A7211B6
description: Use this method in the Microsoft Store submission API to retrieve all add-on data for all the apps that are registered to your Partner Center account.
title: Get all add-ons
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, add-ons, in-app products, IAPs
ms.localizationpriority: medium
---
# Get all add-ons

Use this method in the Microsoft Store submission API to retrieve data for all add-ons for all the apps that are registered to your Partner Center account.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/inappproducts` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

All request parameters are optional for this method. If you call this method without parameters, the response contains data for all add-ons for all apps that are registered to your account.

|  Parameter  |  Type  |  Description  |  Required  |
|------|------|------|------|
|  top  |  int  |  The number of items to return in the request (that is, the number of add-ons to return). If your account has more add-ons than the value you specify in the query, the response body includes a relative URI path that you can append to the method URI to request the next page of data.  |  No  |
|  skip  |  int  |  The number of items to bypass in the query before returning the remaining items. Use this parameter to page through data sets. For example, top=10 and skip=0 retrieves items 1 through 10, top=10 and skip=10 retrieves items 11 through 20, and so on.  |  No  |


### Request body

Do not provide a request body for this method.

### Request examples

The following example demonstrates how to retrieve all add-on data for all the apps that are registered to your account.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/inappproducts HTTP/1.1
Authorization: Bearer <your access token>
```

The following example demonstrates how to retrieve the first 10 add-ons only.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/inappproducts?top=10 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body returned by a successful request for the first 5 add-ons that are registered to a developer account with 1072 total add-ons. For brevity, this example only shows the data for the first two add-ons returned by the request. For more details about the values in the response body, see the following section.

```json
{
  "@nextLink": "inappproducts/?skip=5&top=5",
  "value": [
    {
      "applications": {
        "value": [
          {
            "id": "9NBLGGH4R315",
            "resourceLocation": "applications/9NBLGGH4R315"
          }
        ],
        "totalCount": 1
      },
      "id": "9NBLGGH4TNMP",
      "productId": "a8b8310b-fa8d-4da0-aca0-577ef6dce4dd",
      "productType": "Consumable",
      "pendingInAppProductSubmission": {
        "id": "1152921504621243619",
        "resourceLocation": "inappproducts/9NBLGGH4TNMP/submissions/1152921504621243619"
      },
      "lastPublishedInAppProductSubmission": {
        "id": "1152921504621243705",
        "resourceLocation": "inappproducts/9NBLGGH4TNMP/submissions/1152921504621243705"
      }
    },
    {
      "applications": {
        "value": [
          {
            "id": "9NBLGGH4R315",
            "resourceLocation": "applications/9NBLGGH4R315"
          }
        ],
        "totalCount": 1
      },
      "id": "9NBLGGH4TNMN",
      "productId": "6a3c9788-a350-448a-bd32-16160a13018a",
      "productType": "Consumable",
      "pendingInAppProductSubmission": {
        "id": "1152921504621243538",
        "resourceLocation": "inappproducts/9NBLGGH4TNMN/submissions/1152921504621243538"
      },
      "lastPublishedInAppProductSubmission": {
        "id": "1152921504621243106",
        "resourceLocation": "inappproducts/9NBLGGH4TNMN/submissions/1152921504621243106"
      }
    },

  // Other add-ons omitted for brevity...
  ],
  "totalCount": 1072
}
```

### Response body

| Value      | Type   | Description                                                                                                                                                                                                                                                                         |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| @nextLink  | string | If there are additional pages of data, this string contains a relative path that you can append to the base `https://manage.devcenter.microsoft.com/v1.0/my/` request URI to request the next page of data. For example, if the *top* parameter of the initial request body is set to 10 but there are 100 add-ons registered to your account, the response body will include a @nextLink value of `inappproducts?skip=10&top=10`, which indicates that you can call `https://manage.devcenter.microsoft.com/v1.0/my/inappproducts?skip=10&top=10` to request the next 10 add-ons. |
| value            | array  |  An array that contains objects that provide information about each add-on. For more information, see [add-on resource](manage-add-ons.md#add-on-object).   |
| totalCount   | int  | The number of app objects in the *value* array of the response body.     |


## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 404  | No add-ons were found. |
| 409  | The apps or add-ons use Partner Center features that are [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported).  |


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage add-on submissions](manage-add-on-submissions.md)
* [Get an add-on](get-an-add-on.md)
* [Create an add-on](create-an-add-on.md)
* [Delete an add-on](delete-an-add-on.md)
