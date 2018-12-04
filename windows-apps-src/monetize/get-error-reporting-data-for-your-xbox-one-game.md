---
description: Use this method in the Microsoft Store analytics API to get aggregate error reporting data for a given date range and other optional filters.
title: Get error reporting data for your Xbox One game
ms.date: 11/06/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, errors
ms.localizationpriority: medium
---
# Get error reporting data for your Xbox One game

Use this method in the Microsoft Store analytics API to get aggregate error reporting data for your Xbox One game that was ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics Partner Center dashboard.

You can retrieve additional error information by using the [get details for an error in your Xbox One game](get-details-for-an-error-in-your-xbox-one-game.md), [get the stack trace for an error in your Xbox One game](get-the-stack-trace-for-an-error-in-your-xbox-one-game.md), and [download the CAB file for an error in your Xbox One game](download-the-cab-file-for-an-error-in-your-xbox-one-game.md) methods.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/failurehits``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the Xbox One game for which you are retrieving error reporting data. To get the product ID of your game, navigate to your game in the Xbox Developer Portal (XDP) and retrieve the product ID from the URL. Alternatively, if you download your health data from the Windows Partner Center analytics report, the product ID is included in the .tsv file. |  Yes  |
| startDate | date | The start date in the date range of error reporting data to retrieve. The default is the current date. If *aggregationLevel* is **day**, **week**, or **month**, this parameter should specify a date in the format ```mm/dd/yyyy```. If *aggregationLevel* is **hour**, this parameter can specify a date in the format ```mm/dd/yyyy``` or a date and time in the format ```yyyy-mm-dd hh:mm:ss```.  |  No  |
| endDate | date | The end date in the date range of error reporting data to retrieve. The default is the current date. If *aggregationLevel* is **day**, **week**, or **month**, this parameter should specify a date in the format ```mm/dd/yyyy```. If *aggregationLevel* is **hour**, this parameter can specify a date in the format ```mm/dd/yyyy``` or a date and time in the format ```yyyy-mm-dd hh:mm:ss```. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:<p/><ul><li>**applicationName**</li><li>**failureName**</li><li>**failureHash**</li><li>**symbol**</li><li>**osVersion**</li><li>**osRelease**</li><li>**eventType**</li><li>**market**</li><li>**deviceType**</li><li>**packageName**</li><li>**packageVersion**</li><li>**date**</li></ul> | No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: **hour**, **day**, **week**, or **month**. If unspecified, the default is **day**. If you specify **week** or **month**, the *failureName* and *failureHash* values are limited to 1000 buckets.<p/><p/>**Note:**&nbsp;&nbsp;If you specify **hour**, you can retrieve error data only from the previous 72 hours. To retrieve error data older than 72 hours, specify **day** or one of the other aggregation levels.  | No |
| orderby | string | A statement that orders the result data values. The syntax is *orderby=field [order],field [order],...*. The *field* parameter can be one of the following strings:<ul><li>**applicationName**</li><li>**failureName**</li><li>**failureHash**</li><li>**symbol**</li><li>**osVersion**</li><li>**osRelease**</li><li>**eventType**</li><li>**market**</li><li>**deviceType**</li><li>**packageName**</li><li>**packageVersion**</li><li>**date**</li></ul><p>The *order* parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**.</p><p>Here is an example *orderby* string: *orderby=date,market*</p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li>**failureName**</li><li>**failureHash**</li><li>**symbol**</li><li>**osVersion**</li><li>**eventType**</li><li>**market**</li><li>**deviceType**</li><li>**packageName**</li><li>**packageVersion**</li></ul><p>The returned data rows will contain the fields specified in the *groupby* parameter as well as the following:</p><ul><li>**date**</li><li>**applicationId**</li><li>**applicationName**</li><li>**deviceCount**</li><li>**eventCount**</li></ul><p>The *groupby* parameter can be used with the *aggregationLevel* parameter. For example: *&amp;groupby=failureName,market&amp;aggregationLevel=week*</p></p> |  No  |


### Request example

The following examples demonstrate several requests for getting Xbox One game error reporting data. Replace the *applicationId* value with the  product ID for your game.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/failurehits?applicationId=BRRT4NJ9B3D1&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/failurehits?applicationId=BRRT4NJ9B3D1&startDate=8/1/2015&endDate=8/31/2015&skip=0&filter=market eq 'US' and deviceType eq 'Console' HTTP/1.1
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
| date            | string  | The first date in the date range for the error data, in the format ```yyyy-mm-dd```. If the request specifies a single day, this value is that date. If the request specifies a longer date range, this value is the first date in that date range. For requests that specify an *aggregationLevel* value of **hour**, this value also includes a time value in the format ```hh:mm:ss``` in the local time zone in which the error occurred.  |
| applicationId   | string  | The product ID of the Xbox One game for which you want to retrieve error data.   |
| applicationName | string  | The display name of the game.   |
| failureName     | string  | The name of the failure, which is made up of four parts: one or more problem classes, an exception/bug check code, the name of the image where the failure occurred, and the associated function name.  |
| failureHash     | string  | The unique identifier for the error.   |
| symbol          | string  | The symbol assigned to this error. |
| osVersion       | string  | The OS version on which the error occurred. This is always the value **Windows 10**.  |
| osRelease       | string  |  One of the following strings that specifies the Windows 10 OS release or flighting ring (as a subpopulation within OS version) on which the error occurred.<p/><ul><li><strong>Version 1507</strong></li><li><strong>Version 1511</strong></li><li><strong>Version 1607</strong></li><li><strong>Version 1703</strong></li><li><strong>Version 1709</strong></li><li><strong>Version 1803</strong></li><li><strong>Release Preview</strong></li><li><strong>Insider Fast</strong></li><li><strong>Insider Slow</strong></li></ul><p>If the OS release or flighting ring is unknown, this field has the value <strong>Unknown</strong>.</p>    |
| eventType       | string  | One of the following strings:<ul><li>**crash**</li><li>**hang**</li><li>**memory failure**</li></ul>      |
| market          | string  | The ISO 3166 country code of the device market.   |
| deviceType      | string  | The type of device on which the error occurred. This is always the value **Console**.    |
| packageName     | string  | The unique name of the game package that is associated with this error.      |
| packageVersion  | string  | The version of the game package that is associated with this error.   |
| deviceCount     | integer | The number of unique devices that correspond to this error for the specified aggregation level.  |
| eventCount      | integer | The number of events that are attributed to this error for the specified aggregation level.      |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2017-03-09",
      "applicationId": "BRRT4NJ9B3D1",
      "applicationName": "Contoso Sports",
      "failureName": "APPLICATION_FAULT_8013150a_StoreWrapper.ni.DLL!70475e55",
      "failureHash": "5a6b2170-1661-ed47-24d7-230fed0077af",
      "symbol": "storewrapper_ni!70475e55",
      "osVersion": "Windows 10",
      "osRelease": "Version 1803",
      "eventType": "crash",
      "market": "US",
      "deviceType": "Console",
      "packageName": "",
      "packageVersion": "0.0.0.0",
      "deviceCount": 0.0,
      "eventCount": 1.0
    }
  ],
  "@nextLink": "failurehits?applicationId=BRRT4NJ9B3D1&aggregationLevel=week&startDate=2017/03/01&endDate=2016/02/01&top=1&skip=1",
  "TotalCount": 21
}

```

## Related topics

* [Get details for an error in your Xbox One game](get-details-for-an-error-in-your-xbox-one-game.md)
* [Get the stack trace for an error in your Xbox One game](get-the-stack-trace-for-an-error-in-your-xbox-one-game.md)
* [Download the CAB file for an error in your Xbox One game](download-the-cab-file-for-an-error-in-your-xbox-one-game.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
