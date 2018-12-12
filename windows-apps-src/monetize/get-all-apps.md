---
ms.assetid: 2BCFF687-DC12-49CA-97E4-ACEC72BFCD9B
description: Use this method in the Microsoft Store submission API to retrieve information about all apps that are registered to your Partner Center account.
title: Get all apps
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, apps
ms.localizationpriority: medium
---
# Get all apps


Use this method in the Microsoft Store submission API to retrieve data for the apps that are registered to your Partner Center account.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

All request parameters are optional for this method. If you call this method without parameters, the response contains data for the first 10 apps that are registered to your account.

|  Parameter  |  Type  |  Description  |  Required  |
|------|------|------|------|
|  top  |  int  |  The number of items to return in the request (that is, the number of apps to return). If your account has more apps than the value you specify in the query, the response body includes a relative URI path that you can append to the method URI to request the next page of data.  |  No  |
|  skip  |  int  |  The number of items to bypass in the query before returning the remaining items. Use this parameter to page through data sets. For example, top=10 and skip=0 retrieves items 1 through 10, top=10 and skip=10 retrieves items 11 through 20, and so on.  |  No  |


### Request body

Do not provide a request body for this method.

### Request examples

The following example demonstrates how to retrieve the first 10 apps that are registered to your account.

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications HTTP/1.1
Authorization: Bearer <your access token>
```

The following example demonstrates how to retrieve information about all the apps that are registered to your account. First, get the top 10 apps:

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications?top=10 HTTP/1.1
Authorization: Bearer <your access token>
```

Then recursively call `GET https://manage.devcenter.microsoft.com/v1.0/my/{@nextLink}` until `{@nextlink}` is null or doesn't exist in the response. For example:

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications?skip=10&top=10 HTTP/1.1
Authorization: Bearer <your access token>
```
  
```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications?skip=20&top=10 HTTP/1.1
Authorization: Bearer <your access token>
```

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications?skip=30&top=10 HTTP/1.1
Authorization: Bearer <your access token>
```

If you already know the total number of apps you have in your account, you can simply pass that number in the **top** parameter to get information about all your apps.

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications?top=23 HTTP/1.1
Authorization: Bearer <your access token>
```


## Response

The following example demonstrates the JSON response body returned by a successful request for the first 10 apps that are registered to a developer account with 21 total apps. For brevity, this example only shows the data for the first two apps returned by the request. For more details about the values in the response body, see the following section.

```json
{
  "@nextLink": "applications?skip=10&top=10",
  "value": [
    {
      "id": "9NBLGGH4R315",
      "primaryName": "Contoso sample app",
      "packageFamilyName": "5224ContosoDeveloper.ContosoSampleApp_ng6try80pwt52",
      "packageIdentityName": "5224ContosoDeveloper.ContosoSampleApp",
      "publisherName": "CN=…",
      "firstPublishedDate": "2016-03-11T01:32:11.0747851Z",
      "pendingApplicationSubmission": {
        "id": "1152921504621134883",
        "resourceLocation": "applications/9NBLGGH4R315/submissions/1152921504621134883"
      }
    },
    {
      "id": "9NBLGGH29DM8",
      "primaryName": "Contoso sample app 2",
      "packageFamilyName": "5224ContosoDeveloper.ContosoSampleApp2_ng6try80pwt52",
      "packageIdentityName": "5224ContosoDeveloper.ContosoSampleApp2",
      "publisherName": "CN=…",
      "firstPublishedDate": "2016-03-12T01:49:11.0747851Z",
      "lastPublishedApplicationSubmission": {
        "id": "1152921504621225621",
        "resourceLocation": "applications/9NBLGGH29DM8/submissions/1152921504621225621"
      }
      // Next 8 apps are omitted for brevity ...
    }
  ],
  "totalCount": 21
}
```

### Response body

| Value      | Type   | Description                                                                                                                                                                                                                                                                         |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| value      | array  | An array of objects that contain information about each app that is registered to your account. For more information about the data in each object, see [Application resource](get-app-data.md#application_object).                                                                                                                           |
| @nextLink  | string | If there are additional pages of data, this string contains a relative path that you can append to the base `https://manage.devcenter.microsoft.com/v1.0/my/` request URI to request the next page of data. For example, if the *top* parameter of the initial request body is set to 10 but there are 20 apps registered to your account, the response body will include a @nextLink value of `applications?skip=10&top=10`, which indicates that you can call `https://manage.devcenter.microsoft.com/v1.0/my/applications?skip=10&top=10` to request the next 10 apps. |
| totalCount | int    | The total number of rows in the data result for the query (that is, the total number of apps that are registered to your account).                                                |


## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 404  | No apps were found. |
| 409  | The apps use Partner Center features that are [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported).  |


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get an app](get-an-app.md)
* [Get package flights for an app](get-flights-for-an-app.md)
* [Get add-ons for an app](get-add-ons-for-an-app.md)
