---
author: Xansky
description: Use this method in the Microsoft Store analytics API to get detailed data for a specific error for your Xbox One game.
title: Get details for an error in your Xbox One game
ms.author: mhopkins
ms.date: 11/06/2018
ms.topic: article


keywords: windows 10, uwp, Store services, Microsoft Store analytics API, errors, details
ms.localizationpriority: medium
---

# Get details for an error in your Xbox One game

Use this method in the Microsoft Store analytics API to get detailed data for a specific error for your Xbox One game that was ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics Dev Center dashboard. This method can only retrieve details for errors that occurred in the last 30 days.

Before you can use this method, you must first use the [get error reporting data for your Xbox One game](get-error-reporting-data-for-your-xbox-one-game.md) method to retrieve the ID of the error for which you want to get detailed info.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID of the error for which you want to get detailed info. To get this ID, use the [get error reporting data for your Xbox One game](get-error-reporting-data-for-your-xbox-one-game.md) method and use the **failureHash** value in the response body of that method.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/failuredetails``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the Xbox One game for which you are retrieving error details. To get the product ID of your game, navigate to your game in the Xbox Developer Portal (XDP) and retrieve the product ID from the URL. Alternatively, if you download your health data from the Windows Dev Center analytics report, the product ID is included in the .tsv file. |  Yes  |
| failureHash | string | The unique ID of the error for which you want to get detailed info. To get this value for the error you are interested in, use the [get error reporting data for your Xbox One game](get-error-reporting-data-for-your-xbox-one-game.md) method and use the **failureHash** value in the response body of that method. |  Yes  |
| startDate | date | The start date in the date range of detailed error data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of detailed error data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10 and skip=0 retrieves the first 10 rows of data, top=10 and skip=10 retrieves the next 10 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:<p/><ul><li><strong>market</strong></li><li><strong>date</strong></li><li><strong>cabId</strong></li><li><strong>cabExpirationTime</strong></li><li><strong>deviceType</strong></li><li><strong>deviceModel</strong></li><li><strong>osVersion</strong></li><li><strong>osRelease</strong></li><li><strong>packageVersion</strong></li><li><strong>osBuild</strong></li></ul> | No   |
| orderby | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>market</strong></li><li><strong>date</strong></li><li><strong>cabId</strong></li><li><strong>cabExpirationTime</strong></li><li><strong>deviceType</strong></li><li><strong>deviceModel</strong></li><li><strong>osVersion</strong></li><li><strong>osRelease</strong></li><li><strong>packageVersion</strong></li><li><strong>osBuild</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |


### Request example

The following examples demonstrate several requests for getting detailed error data for an Xbox One game. Replace the *applicationId* value with the product ID for your game.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/failuredetails?applicationId=BRRT4NJ9B3D1&failureHash=012e33e3-dbc9-b12f-c124-9d9810f05d8b&startDate=2016-11-05&endDate=2016-11-06&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/failuredetails?applicationId=BRRT4NJ9B3D1&failureHash=012e33e3-dbc9-b12f-c124-9d9810f05d8b&startDate=2016-11-05&endDate=2016-11-06&top=10&skip=0&filter=market eq 'US' and deviceType eq 'Windows.Desktop' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type    | Description    |
|------------|---------|------------|
| Value      | array   | An array of objects that contain detailed error data. For more information about the data in each object, see the [error detail values](#error-detail-values) section below.          |
| @nextLink  | string  | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10 but there are more than 10 rows of errors for the query. |
| TotalCount | integer | The total number of rows in the data result for the query.        |


<span id="error-detail-values"/>

### Error detail values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description     |
|-----------------|---------|----------------------------|
| applicationId   | string  | The product ID of the Xbox One game for which you retrieved detailed error data.      |
| failureHash     | string  | The unique identifier for the error.     |
| failureName     | string  | The name of the failure, which is made up of four parts: one or more problem classes, an exception/bug check code, the name of the image where the failure occurred, and the associated function name.           |
| date            | string  | The first date in the date range for the error data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| cabId           | string  | The unique ID of the CAB file that is associated with this error.   |
| cabExpirationTime  | string  | The date and time when the CAB file is expired and can no longer be downloaded, in ISO 8601 format.   |
| market          | string  | The ISO 3166 country code of the device market.     |
| osBuild         | string  | The build number of the OS on which the error occurred.       |
| packageVersion  | string  | The version of the game package that is associated with this error.    |
| deviceModel           | string  | One of the following strings that specifies the Xbox One console on which the game was running when the error occurred.<p/><ul><li><strong>Microsoft-Xbox One</strong></li><li><strong>Microsoft-Xbox One S</strong></li><li><strong>Microsoft-Xbox One X</strong></li></ul>  |
| osVersion       | string  | The OS version on which the error occurred. This is always the value **Windows 10**.    |
| osRelease       | string  |  One of the following strings that specifies the Windows 10 OS release or flighting ring (as a subpopulation within OS version) on which the error occurred.<p/><ul><li><strong>Version 1507</strong></li><li><strong>Version 1511</strong></li><li><strong>Version 1607</strong></li><li><strong>Version 1703</strong></li><li><strong>Version 1709</strong></li><li><strong>Version 1803</strong></li><li><strong>Release Preview</strong></li><li><strong>Insider Fast</strong></li><li><strong>Insider Slow</strong></li></ul><p>If the OS release or flighting ring is unknown, this field has the value <strong>Unknown</strong>.</p>    |
| deviceType      | string  | The type of device on which the error occurred. This is always the value **Console**.     |
| cabDownloadable           | Boolean  | Indicates whether the CAB file is downloadable for this user.   |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "applicationId": "BRRT4NJ9B3D1",
      "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
      "failureName": "STOWED_EXCEPTION_System.UriFormatException_exe!ContosoSports.GroupedItems+_ItemView_ItemClick_d__9.MoveNext",
      "date": "2018-02-05 09:11:25",
      "cabId": "133637331323",
      "cabExpirationTime": "2016-12-05 09:11:25",
      "market": "US",
      "osBuild": "10.0.17134",
      "packageVersion": "1.0.2.6",
      "deviceModel": "Microsoft-Xbox One",
      "osVersion": "Windows 10",
      "osRelease": "Version 1803",
      "deviceType": "Console",
      "cabDownloadable": false
    }
  ],
  "@nextLink": null,
  "TotalCount": 1
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data for your Xbox One game](get-error-reporting-data-for-your-xbox-one-game.md)
* [Get the stack trace for an error in your Xbox One game](get-the-stack-trace-for-an-error-in-your-xbox-one-game.md)
* [Download the CAB file for an error in your Xbox One game](download-the-cab-file-for-an-error-in-your-xbox-one-game.md)
