---
ms.assetid: f0c0325e-ad61-4238-a096-c37802db3d3b
description: Use this method in the Microsoft Store analytics API to get detailed data for a specific error for your app.
title: Get details for an error in your app
ms.date: 06/05/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, errors, details
ms.localizationpriority: medium
---
# Get details for an error in your app

Use this method in the Microsoft Store analytics API to get detailed data for a specific error for your app in JSON format. This method can only retrieve details for errors that occurred in the last 30 days. Detailed error data is also available in the **Failures** section of the [Health report](/windows/apps/publish/health-report) in Partner Center.

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


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The Store ID of the app for which you want to retrieve detailed error data. The Store ID is available on the [App identity page](/windows/apps/publish/view-app-identity-details) in Partner Center. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| failureHash | string | The unique ID of the error for which you want to get detailed info. To get this value for the error you are interested in, use the [get error reporting data](get-error-reporting-data.md) method and use the **failureHash** value in the response body of that method. |  Yes  |
| startDate | date | The start date in the date range of detailed error data to retrieve. The default is 30 days before the current date.</p></p>**Note:**&nbsp;&nbsp;This method can only retrieve details for errors that occurred in the last 30 days. |  No  |
| endDate | date | The end date in the date range of detailed error data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10 and skip=0 retrieves the first 10 rows of data, top=10 and skip=10 retrieves the next 10 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:</p><ul><li><strong>market</strong></li><li><strong>date</strong></li><li><strong>cabId</strong></li><li><strong>cabExpirationTime</strong></li><li><strong>deviceType</strong></li><li><strong>deviceModel</strong></li><li><strong>osVersion</strong></li><li><strong>osRelease</strong></li><li><strong>packageVersion</strong></li><li><strong>osBuild</strong></li></ul> | No   |
| orderby | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>market</strong></li><li><strong>date</strong></li><li><strong>cabId</strong></li><li><strong>cabExpirationTime</strong></li><li><strong>deviceType</strong></li><li><strong>deviceModel</strong></li><li><strong>osVersion</strong></li><li><strong>osRelease</strong></li><li><strong>packageVersion</strong></li><li><strong>osBuild</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |


### Request example

The following examples demonstrate several requests for getting detailed error data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failuredetails?applicationId=9NBLGGGZ5QDR&failureHash=00001111-aaaa-2222-bbbb-3333cccc4444&startDate=2016-11-05&endDate=2016-11-06&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failuredetails?applicationId=9NBLGGGZ5QDR&failureHash=00001111-aaaa-2222-bbbb-3333cccc4444&startDate=2016-11-05&endDate=2016-11-06&top=10&skip=0&filter=market eq 'US' and deviceType eq 'Windows.Desktop' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type    | Description    |
|------------|---------|------------|
| Value      | array   | An array of objects that contain detailed error data. For more information about the data in each object, see the [error detail values](#error-detail-values) section below.          |
| @nextLink  | string  | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10 but there are more than 10 rows of errors for the query. |
| TotalCount | integer | The total number of rows in the data result for the query.        |


<span id="error-detail-values"></span>

### Error detail values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description     |
|-----------------|---------|----------------------------|
| applicationId   | string  | The Store ID of the app for which you retrieved detailed error data.      |
| failureHash     | string  | The unique identifier for the error.     |
| failureName     | string  | The name of the failure, which is made up of four parts: one or more problem classes, an exception/bug check code, the name of the image where the failure occurred, and the associated function name.           |
| date            | string  | The first date in the date range for the error data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| cabId           | string  | The unique ID of the CAB file that is associated with this error.   |
| cabExpirationTime  | string  | The date and time when the CAB file is expired and can no longer be downloaded, in ISO 8601 format.   |
| market          | string  | The ISO 3166 country code of the device market.     |
| osBuild         | string  | The build number of the OS on which the error occurred.       |
| packageVersion  | string  | The version of the app package that is associated with this error.    |
| deviceModel           | string  | A string that specifies the model of the device on which the app was running when the error occurred.   |
| osVersion       | string  | One of the following strings that indicates the OS version on which the error occurred:<ul><li><strong>Windows Phone 7.5</strong></li><li><strong>Windows Phone 8</strong></li><li><strong>Windows Phone 8.1</strong></li><li><strong>Windows Phone 10</strong></li><li><strong>Windows 8</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Windows 11</strong></li><li><strong>Unknown</strong></li></ul>    |
| osRelease       | string  |  One of the following strings that specifies the OS release or flighting ring (as a subpopulation within OS version) on which the error occurred.</p><p>For Windows 11: <strong>Version 2110</strong></p><p>For Windows 10:</p><ul><li><strong>Version 1507</strong></li><li><strong>Version 1511</strong></li><li><strong>Version 1607</strong></li><li><strong>Version 1703</strong></li><li><strong>Version 1709</strong></li><li><strong>Version 1803</strong></li><li><strong>Release Preview</strong></li><li><strong>Insider Fast</strong></li><li><strong>Insider Slow</strong></li></ul></p><p>For Windows Server 1709:</p><ul><li><strong>RTM</strong></li></ul><p>For Windows Server 2016:</p><ul><li><strong>Version 1607</strong></li></ul><p>For Windows 8.1:</p><ul><li><strong>Update 1</strong></li></ul><p>For Windows 7:</p><ul><li><strong>Service Pack 1</strong></li></ul><p>If the OS release or flighting ring is unknown, this field has the value <strong>Unknown</strong>.</p>    |
| deviceType      | string  | One of the following strings that specifies the type of the device on which the app was running when the error occurred:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console-Xbox One</strong></li><li><strong>Console-Xbox Series X</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul>     |
| cabDownloadable           | Boolean  | Indicates whether the CAB file is downloadable for this user.   |

> [!NOTE]
> This method can only retrieve details for errors that occurred in the last 30 days.

### Request and Response example

The following code snippets demonstrates some example request and JSON response body for those request.

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failuredetails?applicationId=9NBLGGGZ5QDR&failureHash=012345-5dbc9-b12f-c124-9d9810f05d8b&startDate=2022-06-30&endDate=2022-07-28&top=10&skip=0
HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "date": "2022-07-12 00:00:00",
            "cabExpirationTime": "2022-08-16 01:37:00",
            "cabDownloadable": false,
            "applicationId": "9NBLGGGZ5QDR",
            "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
            "failureName": "MOAPPLICATION_HANG_cfffffff_Microsoft.Contoso!HANG_QUIESCE",
            "cabId": "1180087848576586304",
            "market": "MX",
            "osBuild": "10.0.19043",
            "packageVersion": "2.5.2.34894",
            "deviceModel": "Dell Inc.-Inspiron 15-3567",
            "osVersion": "Windows 10",
            "osRelease": "Version 21H1",
            "osArchitecture": "x64",
            "deviceType": "PC",
            "cpuManufacturer": "Intel",
            "cpuFamilyName": "Core i5",
            "cpuName": "Intel Core i5-7200U CPU @ 2.50GHz",
            "praid": "app",
            "flightRing": "",
            "sandboxId": "retail"
        },
        {
            "date": "2022-07-13 00:00:00",
            "cabExpirationTime": "2022-08-17 13:35:53",
            "cabDownloadable": true,
            "applicationId": "9NBLGGGZ5QDR",
            "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
            "failureName": "MOAPPLICATION_HANG_cfffffff_Microsoft.Contoso!HANG_QUIESCE",
            "cabId": "2058585545558157474",
            "market": "RO",
            "osBuild": "10.0.22622",
            "packageVersion": "2.5.2.34894",
            "deviceModel": "Dell Inc.-Vostro 5502",
            "osVersion": "Windows 11",
            "osRelease": "External",
            "osArchitecture": "x64",
            "deviceType": "PC",
            "cpuManufacturer": "Intel",
            "cpuFamilyName": "Core i5",
            "cpuName": "11th Gen Intel Core i5-1135G7 @ 2.40GHz",
            "praid": "app",
            "flightRing": "external",
            "sandboxId": "retail"
        },
        {
            "date": "2022-07-14 00:00:00",
            "cabExpirationTime": "2022-08-18 07:27:06",
            "cabDownloadable": false,
            "applicationId": "9NBLGGGZ5QDR",
            "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
            "failureName": "MOAPPLICATION_HANG_cfffffff_Microsoft.Contoso!HANG_QUIESCE",
            "cabId": "1940204079766793391",
            "market": "IN",
            "osBuild": "10.0.19044",
            "packageVersion": "2.5.2.34894",
            "deviceModel": "Generic Desktop",
            "osVersion": "Windows 10",
            "osRelease": "Version 21H2",
            "osArchitecture": "x64",
            "deviceType": "PC",
            "cpuManufacturer": "Intel",
            "cpuFamilyName": "Pentium",
            "cpuName": "Intel Pentium CPU G630 @ 2.70GHz",
            "praid": "app",
            "flightRing": "",
            "sandboxId": "retail"
        },
        {
            "date": "2022-07-17 00:00:00",
            "cabExpirationTime": "2022-08-21 10:04:16",
            "cabDownloadable": true,
            "applicationId": "9NBLGGGZ5QDR",
            "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
            "failureName": "MOAPPLICATION_HANG_cfffffff_Microsoft.Contoso!HANG_QUIESCE",
            "cabId": "1197051093472061859",
            "market": "ES",
            "osBuild": "10.0.22621",
            "packageVersion": "2.5.2.34894",
            "deviceModel": "Microsoft Corporation-Surface Pro 3",
            "osVersion": "Windows 11",
            "osRelease": "External",
            "osArchitecture": "x64",
            "deviceType": "PC",
            "cpuManufacturer": "Intel",
            "cpuFamilyName": "Core i7",
            "cpuName": "Intel Core i7-4650U CPU @ 1.70GHz",
            "praid": "app",
            "flightRing": "external",
            "sandboxId": "retail"
        },
        {
            "date": "2022-07-20 00:00:00",
            "cabExpirationTime": "2022-08-24 12:40:05",
            "cabDownloadable": false,
            "applicationId": "9NBLGGGZ5QDR",
            "failureHash": "012345-5dbc9-b12f-c124-9d9810f05d8b",
            "failureName": "MOAPPLICATION_HANG_cfffffff_Microsoft.Contoso!HANG_QUIESCE",
            "cabId": "1332886311327579782",
            "market": "RU",
            "osBuild": "6.3.9600",
            "packageVersion": "2.5.2.34894",
            "deviceModel": "ASUSTeK COMPUTER INC.-K75VJ",
            "osVersion": "Windows 8.1",
            "osRelease": "RTM",
            "osArchitecture": "x64",
            "deviceType": "PC",
            "cpuManufacturer": "Intel",
            "cpuFamilyName": "Core i7",
            "cpuName": "Intel Core i7-3630QM CPU @ 2.40GHz",
            "praid": "app",
            "flightRing": "",
            "sandboxId": ""
        }
    ],
    "TotalCount": 5
}
```

## Related topics

* [Health report](/windows/apps/publish/health-report)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data](get-error-reporting-data.md)
* [Get the stack trace for an error in your app](get-the-stack-trace-for-an-error-in-your-app.md)
* [Download the CAB file for an error in your app](download-the-cab-file-for-an-error-in-your-app.md)
