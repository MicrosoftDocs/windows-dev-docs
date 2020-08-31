---
ms.assetid: B0AD0B8E-867E-4403-9CF6-43C81F3C30CA
description: Use this method in the Microsoft Store submission API to retrieve package flight information for an app that is registered to your Partner Center account.
title: Get package flights for an app
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, flights, package flights
ms.localizationpriority: medium
---
# Get package flights for an app

Use this method in the Microsoft Store submission API to list the package flights for an app that is registered to your Partner Center account. For more information about package flights, see [Package flights](../publish/package-flights.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/listflights` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

|  Name  |  Type  |  Description  |  Required  |
|------|------|------|------|
|  applicationId  |  string  |  The Store ID of the app for which you want to retrieve the package flights. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |  Yes  |
|  top  |  int  |  The number of items to return in the request (that is, the number of package flights to return). If your account has more package flights than the value you specify in the query, the response body includes a relative URI path that you can append to the method URI to request the next page of data.  |  No  |
|  skip  |  int  |  The number of items to bypass in the query before returning the remaining items. Use this parameter to page through data sets. For example, top=10 and skip=0 retrieves items 1 through 10, top=10 and skip=10 retrieves items 11 through 20, and so on.  |  No  |


### Request body

Do not provide a request body for this method.

### Request examples

The following example demonstrates how to list all the package flights for an app.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/listflights HTTP/1.1
Authorization: Bearer <your access token>
```

The following example demonstrates how to list the first package flight for an app.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/listflights?top=1 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body returned by a successful request for the first package flight for an app with three total package flights. For more details about the values in the response body, see the following section.

```json
{
  "value": [
    {
      "flightId": "7bfc11d5-f710-47c5-8a98-e04bb5aad310",
      "friendlyName": "myflight",
      "lastPublishedFlightSubmission": {
        "id": "1152921504621086517",
        "resourceLocation": "flights/7bfc11d5-f710-47c5-8a98-e04bb5aad310/submissions/1152921504621086517"
      },
      "pendingFlightSubmission": {
        "id": "1152921504621215786",
        "resourceLocation": "flights/7bfc11d5-f710-47c5-8a98-e04bb5aad310/submissions/1152921504621215786"
      },
      "groupIds": [
        "1152921504606962205"
      ],
      "rankHigherThan": "Non-flighted submission"
    }
  ],
  "totalCount": 3
}
```

### Response body

| Value      | Type   | Description       |
|------------|--------|---------------------|
| @nextLink  | string | If there are additional pages of data, this string contains a relative path that you can append to the base `https://manage.devcenter.microsoft.com/v1.0/my/` request URI to request the next page of data. For example, if the *top* parameter of the initial request body is set to 2 but there are 4 package flights for the app, the response body will include a @nextLink value of `applications/{applicationid}/listflights/?skip=2&top=2`, which indicates that you can call `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationid}/listflights/?skip=2&top=2` to request the next 2 package flights. |
| value      | array  | An array of objects that provide information about package flights for the specified app. For more information about the data in each object, see [Flight resource](get-app-data.md#flight-object).               |
| totalCount | int    | The total number of rows in the data result for the query (that is, the total number of package flights for the specified app).   |


## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 404  | No package flights were found. |
| 409  | The app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported).  |


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get all apps](get-all-apps.md)
* [Get an app](get-an-app.md)
* [Get add-ons for an app](get-add-ons-for-an-app.md)