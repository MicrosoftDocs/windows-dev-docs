---
description: Use this REST URI to get block data for a desktop application during a given date range and other optional filters.
title: Get upgrade blocks for your desktop application
ms.date: 07/11/2018
ms.topic: article
keywords: windows 10, desktop app blocks, Windows Desktop Application Program
localizationpriority: medium
ms.custom: RS5
---
# Get upgrade blocks for your desktop application

Use this REST URI to get info about Windows 10 devices on which your desktop application is blocking a Windows 10 upgrade from running. You can use this URI only for desktop applications that you have added to the [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program). This information is also available in the [Application blocks report](/windows/desktop/appxpkg/windows-desktop-application-program#application-blocks-report) for desktop applications in Partner Center.

To get details about device blocks for a specific executable in your desktop application, see [Get upgrade block details for your desktop application](get-desktop-block-data-details.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/blockhits```|


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the desktop application for which you want to retrieve block data. To get the product ID of a desktop application, open any [analytics report for your desktop application in Partner Center](/windows/desktop/appxpkg/windows-desktop-application-program) (such as the **Blocks report**) and retrieve the product ID from the URL. |  Yes  |
| startDate | date | The start date in the date range of block data to retrieve. The default is 90 days prior to the current date. |  No  |
| endDate | date | The end date in the date range of block data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter | string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:<p/><ul><li><strong>applicationVersion</strong></li><li><strong>architecture</strong></li><li><strong>blockType</strong></li><li><strong>deviceType</strong></li><li><strong>fileName</strong></li><li><strong>market</strong></li><li><strong>osRelease</strong></li><li><strong>osVersion</strong></li><li><strong>productName</strong></li><li><strong>targetOs</strong></li></ul> | No   |
| orderby | string | A statement that orders the result data values for each block. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following fields from the response body:<p/><ul><li><strong>applicationVersion</strong></li><li><strong>architecture</strong></li><li><strong>blockType</strong></li><li><strong>date</strong><li><strong>deviceType</strong></li><li><strong>fileName</strong></li><li><strong>market</strong></li><li><strong>osRelease</strong></li><li><strong>osVersion</strong></li><li><strong>productName</strong></li><li><strong>targetOs</strong></li><li><strong>deviceCount</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields from the response body:<p/><ul><li><strong>applicationVersion</strong></li><li><strong>architecture</strong></li><li><strong>blockType</strong></li><li><strong>deviceType</strong></li><li><strong>fileName</strong></li><li><strong>market</strong></li><li><strong>osRelease</strong></li><li><strong>osVersion</strong></li><li><strong>targetOs</strong></li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li><strong>applicationId</strong></li><li><strong>date</strong></li><li><strong>productName</strong></li><li><strong>deviceCount</strong></li></ul></p> |  No  |


### Request example

The following example demonstrates several requests for getting desktop application block data. Replace the *applicationId* value with the Product ID for your desktop application.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/blockhits?applicationId=5126873772241846776&startDate=2018-05-01&endDate=2018-06-07&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/blockhits?applicationId=5126873772241846776&startDate=2018-05-01&endDate=2018-06-07&filter=market eq 'US' and deviceType eq 'PC' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain aggregate block data. For more information about the data in each object, see the following table. |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of block data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query. |


Elements in the *Value* array contain the following values.

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| applicationId       | string | The product ID of the desktop application for which you retrieved block data. |
| date                | string | The date associated with the block hits value. |
| productName         | string | The display name of the desktop application as derived from the metadata of its associated executable(s). |
| fileName            | string | The executable that was blocked. |
| applicationVersion  | string | The version of the application executable that was blocked. |
| osVersion           | string | One of the following strings that specifies the OS version on which the desktop application is currently running:<p/><ul><li><strong>Windows 7</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Windows Server 2016</strong></li><li><strong>Windows Server 1709</strong></li><li><strong>Unknown</strong></li></ul>   |
| osRelease           | string | One of the following strings that specifies the OS release or flighting ring (as a subpopulation within OS version) on which the desktop application is currently running.<p/><p>For Windows 10:</p><ul><li><strong>Version 1507</strong></li><li><strong>Version 1511</strong></li><li><strong>Version 1607</strong></li><li><strong>Version 1703</strong></li><li><strong>Version 1709</strong></li><li><strong>Release Preview</strong></li><li><strong>Insider Fast</strong></li><li><strong>Insider Slow</strong></li></ul><p/><p>For Windows Server 1709:</p><ul><li><strong>RTM</strong></li></ul><p>For Windows Server 2016:</p><ul><li><strong>Version 1607</strong></li></ul><p>For Windows 8.1:</p><ul><li><strong>Update 1</strong></li></ul><p>For Windows 7:</p><ul><li><strong>Service Pack 1</strong></li></ul><p>If the OS release or flighting ring is unknown, this field has the value <strong>Unknown</strong>.</p> |
| market              | string | The ISO 3166 country code of the market in which the desktop application is blocked. |
| deviceType          | string | One of the following strings that specifies the type of device on which the desktop application is blocked:<p/><ul><li><strong>PC</strong></li><li><strong>Server</strong></li><li><strong>Tablet</strong></li><li><strong>Unknown</strong></li></ul> |
| blockType            | string | One of the following strings that specifies the type of block found on the device:<p/><ul><li><strong>Potential Sediment</strong></li><li><strong>Temporary Sediment</strong></li><li><strong>Runtime Notification</strong></li></ul><p/><p/>For more information about these block types and what they mean to developers and users, see the description of the [Application blocks report](/windows/desktop/appxpkg/windows-desktop-application-program#application-blocks-report).  |
| architecture        | string | The architecture of the device on which the block exists: <p/><ul><li><strong>ARM64</strong></li><li><strong>X86</strong></li></ul> |
| targetOs            | string | One of the following strings that specifies the Windows 10 OS release on which the desktop application is blocked from running: <p/><ul><li><strong>Version 1709</strong></li><li><strong>Version 1803</strong></li></ul> |
| deviceCount         | number | The number of distinct devices that have blocks at the specified aggregation level. |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
     "applicationId": "10238467886765136388",
     "date": "2018-06-03",
     "productName": "Contoso Demo",
     "fileName": "contosodemo.exe",
     "applicationVersion": "2.2.2.0",
     "osVersion": "Windows 8.1",
     "osRelease": "Update 1",
     "market": "ZA",
     "deviceType": "All",
     "blockType": "Runtime Notification",
     "architecture": "X86",
     "targetOs": "RS4",
     "deviceCount": 120
    }
  ],
  "@nextLink": "desktop/blockhits?applicationId=123456789&startDate=2018-01-01&endDate=2018-02-01&top=10000&skip=10000&groupby=applicationVersion,deviceType,osVersion,osRelease",
  "TotalCount": 23012
}
```

## Related topics

* [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program)
* [Get upgrade block details for your desktop application](get-desktop-block-data-details.md)