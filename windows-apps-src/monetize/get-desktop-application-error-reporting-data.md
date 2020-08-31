---
description: Use this method in the Microsoft Store analytics API to get aggregate error reporting data for a desktop application for a given date range and other optional filters.
title: Get error reporting data for your desktop application
ms.date: 09/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, errors, desktop application
ms.localizationpriority: medium
---
# Get error reporting data for your desktop application

Use this method in the Microsoft Store analytics API to get aggregate error reporting data for a desktop application that you have added to the [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program). This method can only retrieve errors that occurred in the last 30 days. This information is also available in the [Health report](/windows/desktop/appxpkg/windows-desktop-application-program) for desktop applications in Partner Center.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/failurehits``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the desktop application for which you want to retrieve error reporting data. To get the product ID of a desktop application, open any [analytics report for your desktop application in Partner Center](/windows/desktop/appxpkg/windows-desktop-application-program) (such as the **Health report**) and retrieve the product ID from the URL. |  Yes  |
| startDate | date | The start date in the date range of error reporting data to retrieve, in the format ```mm/dd/yyyy```. The default is the current date.<p/><p/>**Note:**&nbsp;&nbsp;This method can only retrieve errors that occurred in the last 30 days.  |  No  |
| endDate | date | The end date in the date range of error reporting data to retrieve, in the format ```mm/dd/yyyy```. The default is the current date.   |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:<p/><ul><li><strong>fileName</strong></li><li><strong>applicationVersion</strong></li><li><strong>failureName</strong></li><li><strong>failureHash</strong></li><li><strong>symbol</strong></li><li><strong>osVersion</strong></li><li><strong>osBuild</strong></li><li><strong>osRelease</strong></li><li><strong>eventType</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li><li><strong>productName</strong></li><li><strong>date</strong></li></ul> | No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: **day**, **week**, or **month**. If unspecified, the default is **day**. If you specify **week** or **month**, the *failureName* and *failureHash* values are limited to 1000 buckets.<p/>  | No |
| orderby | string | A statement that orders the result data values. The syntax is *orderby=field [order],field [order],...*. The *field* parameter can be one of the following strings:<ul><li><strong>fileName</strong></li><li><strong>applicationVersion</strong></li><li><strong>failureName</strong></li><li><strong>failureHash</strong></li><li><strong>symbol</strong></li><li><strong>osVersion</strong></li><li><strong>osBuild</strong></li><li><strong>osRelease</strong></li><li><strong>eventType</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li><li><strong>productName</strong></li><li><strong>date</strong></li></ul>The *order* parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**.</p><p>Here is an example *orderby* string: *orderby=date,market*</p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li>**failureName**</li><li>**failureHash**</li><li>**symbol**</li><li>**osVersion**</li><li>**eventType**</li><li>**market**</li><li>**deviceType**</li></ul><p>The returned data rows will contain the fields specified in the *groupby* parameter as well as the following:</p><ul><li>**date**</li><li>**applicationId**</li><li>**applicationName**</li><li>**eventCount**</li></ul><p>The *groupby* parameter can be used with the *aggregationLevel* parameter. For example: *&amp;groupby=failureName,market&amp;aggregationLevel=week*</p></p> |  No  |


### Request example

The following examples demonstrate several requests for getting error reporting data. Replace the *applicationId* value with the product ID for your desktop application.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/failurehits?applicationId=10238467886765136388&startDate=1/1/2018&endDate=2/1/2018&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/failurehits?applicationId=10238467886765136388&startDate=8/1/2017&endDate=8/31/2017&skip=0&filter=market eq 'US' and deviceType eq 'PC' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type    | Description     |
|------------|---------|--------------|
| Value      | array   | An array of objects that contain aggregate error reporting data. For more information about the data in each object, see the [error values](#error-values) section below.     |
| @nextLink  | string  | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of errors for the query. |
| TotalCount | integer | The total number of rows in the data result for the query.     |


### Error values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description        |
|-----------------|---------|---------------------|
| date            | string  | The first date in the date range for the error data, in the format ```yyyy-mm-dd```. If the request specifies a single day, this value is that date. If the request specifies a longer date range, this value is the first date in that date range. For requests that specify an *aggregationLevel* value of **hour**, this value also includes a time value in the format ```hh:mm:ss```.  |
| applicationId   | string  | The product ID of the desktop application for which you retrieved error data.    |
| productName | string  | The display name of the desktop application as derived from the metadata of its associated executable(s).   |
| appName | string  |  TBD  |
| fileName | string  | The name of the executable file for the desktop application.   |
| failureName     | string  | The name of the failure, which is made up of four parts: one or more problem classes, an exception/bug check code, the name of the image where the failure occurred, and the associated function name.  |
| failureHash     | string  | The unique identifier for the error.   |
| symbol          | string  | The symbol assigned to this error. |
| osBuild       | string  | The four-part build number of the OS on which the error occurred.  |
| osVersion       | string  | One of the following strings that specifies the OS version on which the desktop application is installed:<p/><ul><li><strong>Windows 7</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Windows Server 2016</strong></li><li><strong>Windows Server 1709</strong></li><li><strong>Unknown</strong></li></ul>   |   
| osRelease | string  | One of the following strings that specifies the OS release or flighting ring (as a subpopulation within OS version) on which the error occurred.<p/><p>For Windows 10:</p><ul><li><strong>Version 1507</strong></li><li><strong>Version 1511</strong></li><li><strong>Version 1607</strong></li><li><strong>Version 1703</strong></li><li><strong>Version 1709</strong></li><li><strong>Version 1803</strong></li><li><strong>Release Preview</strong></li><li><strong>Insider Fast</strong></li><li><strong>Insider Slow</strong></li></ul><p/><p>For Windows Server 1709:</p><ul><li><strong>RTM</strong></li></ul><p>For Windows Server 2016:</p><ul><li><strong>Version 1607</strong></li></ul><p>For Windows 8.1:</p><ul><li><strong>Update 1</strong></li></ul><p>For Windows 7:</p><ul><li><strong>Service Pack 1</strong></li></ul><p>If the OS release or flighting ring is unknown, this field has the value <strong>Unknown</strong>.</p> |
| eventType       | string  | One of the following strings that indicates the type of error event:<ul><li>**crash**</li><li>**hang**</li><li>**memory**</li><li>**jse**</li></ul>       |
| market          | string  | The ISO 3166 country code of the device market.   |
| deviceType      | string  | One of the following strings that specifies the type of device on which the error occurred:<p/><ul><li><strong>PC</strong></li><li><strong>Server</strong></li><li><strong>Tablet</strong></li><li><strong>Unknown</strong></li></ul>    |
| applicationVersion     | string  |   The version of the application executable in which the error occurred.    |
| eventCount      | number | The number of events that are attributed to this error for the specified aggregation level.      |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2018-02-01",
      "applicationId": "10238467886765136388",
      "productName": "Contoso Demo",
      "appName": "Contoso Demo",
      "fileName": "contosodemo.exe",
      "failureName": "SVCHOSTGROUP_localservice_IN_PAGE_ERROR_c0000006_hardware_disk!Unknown",
      "failureHash": "11242ef3-ebd8-d525-838d-b5497b225695",
      "symbol": "hardware_disk!Unknown",
      "osBuild": "10.0.15063.850",
      "osVersion": "Windows 10",
      "osRelease": "Version 1703",
      "eventType": "crash",
      "market": "US",
      "deviceType": "PC",
      "applicationVersion": "2.2.2.0",
      "eventCount": 0.0012422360248447205
    }
  ],
  "@nextLink": "desktop/failurehits?applicationId=10238467886765136388&aggregationLevel=week&startDate=2018/02/01&endDate2018/02/08&top=1&skip=1",
  "TotalCount": 21
}

```

## Related topics

* [Health report](../publish/health-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get details for an error in your desktop application](get-details-for-an-error-in-your-desktop-application.md)
* [Get the stack trace for an error in your desktop application](get-the-stack-trace-for-an-error-in-your-desktop-application.md)
* [Download the CAB file for an error in your desktop application](download-the-cab-file-for-an-error-in-your-desktop-application.md)