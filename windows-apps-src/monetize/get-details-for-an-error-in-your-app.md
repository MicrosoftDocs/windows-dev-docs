---
author: mcleanbyron
ms.assetid: f0c0325e-ad61-4238-a096-c37802db3d3b
description: Use this method in the Microsoft Store analytics API to get detailed data for a specific error for your app.
title: Get details for an error in your app
ms.author: mcleans
ms.date: 06/16/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, errors, details
ms.localizationpriority: medium
---

# Get details for an error in your app

Use this method in the Microsoft Store analytics API to get detailed data for a specific error for your app in JSON format. This method can only retrieve details for errors that occurred in the last 30 days. Detailed error data is also available in the **Failures** section of the [Health report](../publish/health-report.md) in the Windows Dev Center dashboard.

Before you can use this method, you must first use the [get error reporting data](get-error-reporting-data.md) method to retrieve the ID of the error for which you want to get detailed info.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID of the error for which you want to get detailed info. To get this ID, use the [get error reporting data](get-error-reporting-data.md) method and use the **failureHash** value in the response body of that method.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/failuredetails``` |

<span/> 

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |

<span/> 

### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The Store ID of the app for which you want to retrieve detailed error data. The Store ID is available on the [App identity page](../publish/view-app-identity-details.md) of the Dev Center dashboard. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| failureHash | string | The unique ID of the error for which you want to get detailed info. To get this value for the error you are interested in, use the [get error reporting data](get-error-reporting-data.md) method and use the **failureHash** value in the response body of that method. |  Yes  |
| startDate | date | The start date in the date range of detailed error data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of detailed error data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10 and skip=0 retrieves the first 10 rows of data, top=10 and skip=10 retrieves the next 10 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. For more information, see the [filter fields](#filter-fields) section below. | No   |
| orderby | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>market</strong></li><li><strong>cabId</strong></li><li><strong>cabExpirationTime</strong></li><li><strong>deviceType</strong></li><li><strong>deviceModel</strong></li><li><strong>osVersion</strong></li><li><strong>packageVersion</strong></li><li><strong>osBuild</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |

<span/>
 
### Filter fields

The *filter* parameter of the request contains one or more statements that filter the rows in the response. Each statement contains a field and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. Here are some example *filter* parameters:

-   *filter=market eq 'US' and osVersion eq 'Windows 10'*
-   *filter=market ne 'US' and osVersion ne 'Windows 8'*

For a list of the supported fields, see the following table. String values must be surrounded by single quotes in the *filter* parameter.

| Fields        |  Description        |
|---------------|-----------------|
| osVersion | One of the following strings:<ul><li><strong>Windows Phone 7.5</strong></li><li><strong>Windows Phone 8</strong></li><li><strong>Windows Phone 8.1</strong></li><li><strong>Windows Phone 10</strong></li><li><strong>Windows 8</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Unknown</strong></li></ul> |
| osBuild | The build number of the OS on which the app was running when the error occurred. |
| market | A string that contains the ISO 3166 country code of the market of the device on which the app was running when the error occurred. |
| deviceType | One of the following strings that specifies the type of the device on which the app was running when the error occurred:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul> |
| deviceModel | A string that specifies the model of the device on which the app was running when the error occurred. |
| cabId | The unique ID of the CAB file that is associated with this error. |
| cabExpirationTime | The date and time when the CAB file is expired and can no longer be downloaded, in ISO 8601 format. |
| packageVersion | The version of the app package that is associated with this error. |

<span/> 

### Request example

The following examples demonstrate several requests for getting detailed error data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failuredetails?applicationId=9NBLGGGZ5QDR&failureHash=012e33e3-dbc9-b12f-c124-9d9810f05d8b&startDate=2016-11-05&endDate=2016-11-06&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failuredetails?applicationId=9NBLGGGZ5QDR&failureHash=012e33e3-dbc9-b12f-c124-9d9810f05d8b&startDate=2016-11-05&endDate=2016-11-06&top=10&skip=0&filter=market eq 'US' and deviceType eq 'Windows.Desktop' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type    | Description    |
|------------|---------|------------|
| Value      | array   | An array of objects that contain detailed error data. For more information about the data in each object, see the [error detail values](#error-detail-values) section below.          |
| @nextLink  | string  | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10 but there are more than 10 rows of errors for the query. |
| TotalCount | inumber | The total number of rows in the data result for the query.        |

<span id="error-detail-values"/>
### Error detail values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description     |
|-----------------|---------|----------------------------|
| date            | string  | The first date in the date range for the error data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| applicationId   | string  | The Store ID of the app for which you retrieved detailed error data.      |
| failureName     | string  | The name of the error. This is the same name that appears in the **Failures** section of the [Health report](../publish/health-report.md) in the Windows Dev Center dashboard.            |
| failureHash     | string  | The unique identifier for the error.     |
| osVersion       | string  | The OS version on which the error occurred.    |
| market          | string  | The ISO 3166 country code of the device market.     |
| deviceType      | string  | The type of device that on which the error occurred.     |
| packageVersion  | string  | The version of the app package that is associated with this error.    |
| osBuild         | string  | The build number of the OS on which the error occurred.       |
| cabId           | string  | The unique ID of the CAB file that is associated with this error.   |
| cabExpirationTime  | string  | The date and time when the CAB file is expired and can no longer be downloaded, in ISO 8601 format.   |
| deviceModel           | string  | A string that specifies the model of the device on which the app was running when the error occurred.   |
| cabDownloadable           | Boolean  | Indicates whether the CAB file is downloadable for this user.   |

<span/> 

### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "applicationId": "9NBLGGGZ5QDR ",
      "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
      "failureName": "STOWED_EXCEPTION_System.UriFormatException_exe!ContosoGame.GroupedItems+_ItemView_ItemClick_d__9.MoveNext",
      "date": "2015-02-05 09:11:25",
      "cabId": "133637331323",
      "cabExpirationTime": "2016-12-05 09:11:25",
      "market": "US",
      "osBuild": "10.0.10240",
      "packageVersion": "1.0.2.6",
      "deviceModel": "Contoso Computer",
      "osVersion": "Windows 10",
      "deviceType": "Windows.Desktop",
      "cabDownloadable": false
    }
  ],
  "@nextLink": null,
  "TotalCount": 1
}
```

## Related topics

* [Health report](../publish/health-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data](get-error-reporting-data.md)
* [Get the stack trace for an error in your app](get-the-stack-trace-for-an-error-in-your-app.md)
* [Download the CAB file for an error in your app](download-the-cab-file-for-an-error-in-your-app.md)
