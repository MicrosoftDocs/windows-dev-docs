---
ms.assetid: 2967C757-9D8A-4B37-8AA4-A325F7A060C5
description: Use this method in the Microsoft Store analytics API to get review data for a given date range and other optional filters.
title: Get app reviews
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, reviews
ms.localizationpriority: medium
---
# Get app reviews


Use this method in the Microsoft Store analytics API to get review data in JSON format for a given date range and other optional filters. This information is also available in the [Reviews report](../publish/reviews-report.md) in Partner Center.

After you retrieve reviews, you can use the [get response info for app reviews](get-response-info-for-app-reviews.md) and [submit responses to app reviews](submit-responses-to-app-reviews.md) methods in the Microsoft Store reviews API to programmatically respond to reviews.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

### Request syntax

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/reviews``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|---------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve review data.  |  Yes  |
| startDate | date | The start date in the date range of review data to retrieve. The default is the current date. |  No  |
| endDate | date | The end date in the date range of review data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. For more information, see the [filter fields](#filter-fields) section below. | No   |
| orderby | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>osVersion</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li><li><strong>isRevised</strong></li><li><strong>packageVersion</strong></li><li><strong>deviceModel</strong></li><li><strong>productFamily</strong></li><li><strong>deviceScreenResolution</strong></li><li><strong>isTouchEnabled</strong></li><li><strong>reviewerName</strong></li><li><strong>reviewTitle</strong></li><li><strong>reviewText</strong></li><li><strong>helpfulCount</strong></li><li><strong>notHelpfulCount</strong></li><li><strong>responseDate</strong></li><li><strong>responseText</strong></li><li><strong>deviceRAM</strong></li><li><strong>deviceStorageCapacity</strong></li><li><strong>rating</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |


### Filter fields

The *filter* parameter of the request contains one or more statements that filter the rows in the response. Each statement contains a field and value that are associated with the **eq** or **ne** operators, and some fields also support the **contains**, **gt**, **lt**, **ge**, and **le** operators. Statements can be combined using **and** or **or**.

Here is an example *filter* string: *filter=contains(reviewText,'great') and contains(reviewText,'ads') and deviceRAM lt 2048 and market eq 'US'*

For a list of the supported fields and support operators for each field, see the following table. String values must be surrounded by single quotes in the *filter* parameter.

| Fields        | Supported operators   |  Description        |
|---------------|--------|-----------------|
| market | eq, ne | A string that contains the ISO 3166 country code of the device market. |
| osVersion  | eq, ne  | One of the following strings:<ul><li><strong>Windows Phone 7.5</strong></li><li><strong>Windows Phone 8</strong></li><li><strong>Windows Phone 8.1</strong></li><li><strong>Windows Phone 10</strong></li><li><strong>Windows 8</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Unknown</strong></li></ul>  |
| deviceType  | eq, ne  | One of the following strings:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console-Xbox One</strong></li><li><strong>Console-Xbox Series X</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul>  |
| isRevised  | eq, ne  | Specify <strong>true</strong> to filter for reviews that have been revised; otherwise <strong>false</strong>.  |
| packageVersion  | eq, ne  | The version of the app package that was reviewed.  |
| deviceModel  | eq, ne  | The type of device on which the app was reviewed.  |
| productFamily  | eq, ne  | One of the following strings:<ul><li><strong>PC</strong></li><li><strong>Tablet</strong></li><li><strong>Phone</strong></li><li><strong>Wearable</strong></li><li><strong>Server</strong></li><li><strong>Collaborative</strong></li><li><strong>Other</strong></li></ul>  |
| deviceRAM  | eq, ne, gt, lt, ge, le  | The physical RAM, in MB.  |
| deviceScreenResolution  | eq, ne  | The device screen resolution in the format &quot;<em>width</em> x <em>height</em>&quot;.   |
| deviceStorageCapacity  | eq, ne, gt, lt, ge, le   | The capacity of the primary storage disk, in GB.  |
| isTouchEnabled  | eq, ne  | Specify <strong>true</strong> to filter for touch-enabled devices; otherwise <strong>false</strong>.   |
| reviewerName  | eq, ne  |  The reviewer name. |
| rating  | eq, ne, gt, lt, ge, le  | The app rating, in stars.  |
| reviewTitle  | eq, ne, contains  | The title of the review.  |
| reviewText  | eq, ne, contains  |  The text contents of the review. |
| helpfulCount  | eq, ne  |  The number of times the review was marked helpful. |
| notHelpfulCount  | eq, ne  | The number of times the review was marked not helpful.  |
| responseDate  | eq, ne  | The date that the response was submitted.  |
| responseText  | eq, ne, contains  | The text contents of the response.  |
| id  | eq, ne  | The ID of the review (this is a GUID).        |


### Request example

The following examples demonstrate several requests for getting review data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/reviews?applicationId=9NBLGGGZ5QDR&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/reviews?applicationId=9NBLGGGZ5QDR&startDate=8/1/2015&endDate=8/31/2015&skip=0&filter=contains(reviewText,'great') and contains(reviewText,'ads') and deviceRAM lt 2048 and market eq 'US' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description      |
|------------|--------|------------------|
| Value      | array  | An array of objects that contain review data. For more information about the data in each object, see the [review values](#review-values) section below.       |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of reviews data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.  |

 
### Review values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description       |
|-----------------|---------|-------------------|
| date            | string  | The first date in the date range for the review data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| applicationId   | string  | The Store ID of the app for which you are retrieving review data.         |
| applicationName | string  | The display name of the app.    |
| market          | string  | The ISO 3166 country code of the market where the review was submitted.        |
| osVersion       | string  | The OS version on which the review was submitted. For a list of the supported strings, see the [filter fields](#filter-fields) section above.            |
| deviceType      | string  | The type of device on which the review was submitted. For a list of the supported strings, see the [filter fields](#filter-fields) section above.            |
| isRevised       | Boolean | The value **true** indicates that the review was revised; otherwise **false**.   |
| packageVersion  | string  | The version of the app package that was reviewed.        |
| deviceModel        | string  |The type of device on which the app was reviewed.     |
| productFamily      | string  | The device family name. For a list of the supported strings, see the [filter fields](#filter-fields) section above.   |
| deviceRAM       | number  | The physical RAM, in MB.    |
| deviceScreenResolution       | string  | The device screen resolution in the format "*width* x *height*".    |
| deviceStorageCapacity | number | The capacity of the primary storage disk, in GB. |
| isTouchEnabled | Boolean | The value **true** indicates that touch is enabled; otherwise **false**. |
| reviewerName | string | The reviewer name. |
| rating | number | The app rating, in stars. |
| reviewTitle | string | The title of the review. |
| reviewText | string | The text contents of the review. |
| helpfulCount | number | The number of times the review was marked helpful. |
| notHelpfulCount | number | The number of times the review was marked not helpful. |
| responseDate | string | The date a response was submitted. |
| responseText | string | The text contents of the response. |
| id | string | The ID of the review (this is a GUID). You can use this ID in the [get response info for app reviews](get-response-info-for-app-reviews.md) and [submit responses to app reviews](submit-responses-to-app-reviews.md) methods. |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2015-07-29",
      "applicationId": "9NBLGGGZ5QDR",
      "applicationName": "Contoso demo",
      "market": "US",
      "osVersion": "10.0.10240.16410",
      "deviceType": "PC",
      "isRevised": true,
      "packageVersion": "",
      "deviceModel": "Microsoft Corporation-Virtual Machine",
      "productFamily": "PC",
      "deviceRAM": -1,
      "deviceScreenResolution": "1024 x 768",
      "deviceStorageCapacity": 51200,
      "isTouchEnabled": false,
      "reviewerName": "ALeksandra",
      "rating": 5,
      "reviewTitle": "Love it",
      "reviewText": "Great app for demos and great dev response.",
      "helpfulCount": 0,
      "notHelpfulCount": 0,
      "responseDate": "2015-08-07T01:50:22.9874488Z",
      "responseText": "1",
      "id": "6be543ff-1c9c-4534-aced-af8b4fbe0316"
    }
  ],
  "@nextLink": null,
  "TotalCount": 1
}
```

## Related topics

* [Reviews report](../publish/reviews-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get response info for app reviews](get-response-info-for-app-reviews.md)
* [Submit responses to app reviews](submit-responses-to-app-reviews.md)
* [Get app acquisitions](get-app-acquisitions.md)
* [Get add-on acquisitions](get-in-app-acquisitions.md)
* [Get error reporting data](get-error-reporting-data.md)
* [Get app ratings](get-app-ratings.md)
