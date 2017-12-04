---
author: mcleanbyron
ms.assetid: 252C44DF-A2B8-4F4F-9D47-33E423F48584
description: Use this method in the Microsoft Store analytics API to get aggregate error reporting data for a given date range and other optional filters.
title: Get error reporting data for your app
ms.author: mcleans
ms.date: 09/15/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, errors
ms.localizationpriority: medium
---

# Get error reporting data for your app

Use this method in the Microsoft Store analytics API to get aggregate error reporting data for your app in JSON format for a given date range and other optional filters. This information is also available in the **Failures** section of the [Health report](../publish/health-report.md) in the Windows Dev Center dashboard.

You can retrieve additional error information by using the [get error details](get-details-for-an-error-in-your-app.md), [get stack trace](get-the-stack-trace-for-an-error-in-your-app.md), and [download CAB file](download-the-cab-file-for-an-error-in-your-app.md) methods.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/failurehits``` |

<span/> 

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |

<span/> 

### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The Store ID of the app for which you want to retrieve error reporting data. The Store ID is available on the [App identity page](../publish/view-app-identity-details.md) of the Dev Center dashboard. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| startDate | date | The start date in the date range of error reporting data to retrieve. The default is the current date. If *aggregationLevel* is **day**, **week**, or **month**, this parameter should specify a date in the format ```mm/dd/yyyy```. If *aggregationLevel* is **hour**, this parameter can specify a date in the format ```mm/dd/yyyy``` or a date and time in the format ```yyyy-mm-dd hh:mm:ss```.  |  No  |
| endDate | date | The end date in the date range of error reporting data to retrieve. The default is the current date. If *aggregationLevel* is **day**, **week**, or **month**, this parameter should specify a date in the format ```mm/dd/yyyy```. If *aggregationLevel* is **hour**, this parameter can specify a date in the format ```mm/dd/yyyy``` or a date and time in the format ```yyyy-mm-dd hh:mm:ss```. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. For more information, see the [filter fields](#filter-fields) section below. | No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: **hour**, **day**, **week**, or **month**. If unspecified, the default is **day**. If you specify **week** or **month**, the *failureName* and *failureHash* values are limited to 1000 buckets.<p/><p/>**Note:**&nbsp;&nbsp;If you specify **hour**, you can retrieve error data only from the previous 72 hours. To retrieve error data older than 72 hours, specify **day** or one of the other aggregation levels.  | No |
| orderby | string | A statement that orders the result data values. The syntax is *orderby=field [order],field [order],...*. The *field* parameter can be one of the following strings:<ul><li>**date**</li><li>**failureName**</li><li>**failureHash**</li><li>**symbol**</li><li>**osVersion**</li><li>**eventType**</li><li>**market**</li><li>**deviceType**</li><li>**packageName**</li><li>**packageVersion**</li></ul><p>The *order* parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**.</p><p>Here is an example *orderby* string: *orderby=date,market*</p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li>**failureName**</li><li>**failureHash**</li><li>**symbol**</li><li>**osVersion**</li><li>**eventType**</li><li>**market**</li><li>**deviceType**</li><li>**packageName**</li><li>**packageVersion**</li></ul><p>The returned data rows will contain the fields specified in the *groupby* parameter as well as the following:</p><ul><li>**date**</li><li>**applicationId**</li><li>**applicationName**</li><li>**deviceCount**</li><li>**eventCount**</li></ul><p>The *groupby* parameter can be used with the *aggregationLevel* parameter. For example: *&amp;groupby=failureName,market&amp;aggregationLevel=week*</p></p> |  No  |

<span/>
 
### Filter fields

The *filter* parameter of the request contains one or more statements that filter the rows in the response. Each statement contains a field and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. Here are some example *filter* parameters:

-   *filter=market eq 'US' and gender eq 'm'*
-   *filter=(market ne 'US') and (gender ne 'Unknown') and (gender ne 'm') and (market ne 'NO') and (ageGroup ne 'greater than 55' or ageGroup ne ‘less than 13’)*

For a list of the supported fields, see the following table. String values must be surrounded by single quotes in the *filter* parameter.

| Fields        |  Description        |
|---------------|-----------------|
| failureName | The name of the error. |
| failureHash | The unique identifier for the error. |
| symbol | The symbol assigned to this error. |
| osVersion | One of the following strings:<ul><li>**Windows Phone 7.5**</li><li>**Windows Phone 8**</li><li>**Windows Phone 8.1**</li><li>**Windows Phone 10**</li><li>**Windows 8**</li><li>**Windows 8.1**</li><li>**Windows 10**</li><li>**Unknown**</li></ul> |
| eventType | One of the following strings:<ul><li>**crash**</li><li>**hang**</li><li>**memory**</li><li>**jse**</li></ul> |
| market | A string that contains the ISO 3166 country code of the market where the error occurred. |
| deviceType | One of the following strings:<ul><li>**PC**</li><li>**Phone**</li><li>**Console**</li><li>**IoT**</li><li>**Holographic**</li><li>**Unknown**</li></ul> |
| packageName | The unique name of the app package that is associated with this error. |
| packageVersion | The version of the app package that is associated with this error. |

<span/> 

### Request example

The following examples demonstrate several requests for getting error reporting data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failurehits?applicationId=9NBLGGGZ5QDR&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/failurehits?applicationId=9NBLGGGZ5QDR&startDate=8/1/2015&endDate=8/31/2015&skip=0&filter=market eq 'US' and deviceType eq 'phone' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type    | Description     |
|------------|---------|--------------|
| Value      | array   | An array of objects that contain aggregate error reporting data. For more information about the data in each object, see the [error values](#error-values) section below.     |
| @nextLink  | string  | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of errors for the query. |
| TotalCount | inumber | The total number of rows in the data result for the query.     |

<span/>

### Error values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description        |
|-----------------|---------|---------------------|
| date            | string  | The first date in the date range for the error data, in the format ```yyyy-mm-dd```. If the request specifies a single day, this value is that date. If the request specifies a longer date range, this value is the first date in that date range. For requests that specify an *aggregationLevel* value of **hour**, this value also includes a time value in the format ```hh:mm:ss```.  |
| applicationId   | string  | The Store ID of the app for which you want to retrieve error data.   |
| applicationName | string  | The display name of the app.   |
| failureName     | string  | The name of the error.  |
| failureHash     | string  | The unique identifier for the error.   |
| symbol          | string  | The symbol assigned to this error. |
| osVersion       | string  | The OS version on which the error occurred. For a list of the supported strings, see the [filter fields](#filter-fields) section above.  |
| eventType       | string  | The type of error event. For a list of the supported strings, see the [filter fields](#filter-fields) section above.      |
| market          | string  | The ISO 3166 country code of the device market.   |
| deviceType      | string  | The type of device on which the error occurred. For a list of the supported strings, see the [filter fields](#filter-fields) section above.    |
| packageName     | string  | The unique name of the app package that is associated with this error.      |
| packageVersion  | string  | The version of the app package that is associated with this error.   |
| eventCount      | inumber | The number of events that are attributed to this error for the specified aggregation level.      |
| deviceCount     | inumber | The number of unique devices that correspond to this error for the specified aggregation level.  |

<span/> 

### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2015-03-09",
      "applicationId": "9NBLGGGZ5QDR",
      "applicationName": "Contoso Demo",
      "failureName": "APPLICATION_FAULT_8013150a_StoreWrapper.ni.DLL!70475e55",
      "failureHash": "5a6b2170-1661-ed47-24d7-230fed0077af",
      "symbol": "storewrapper_ni!70475e55",
      "osVersion": "Windows Phone 8",
      "eventType": "crash",
      "market": "US",
      "deviceType": "mobile",
      "packageName": "",
      "packageVersion": "0.0.0.0",
      "deviceCount": 0.0,
      "eventCount": 1.0
    }
  ],
  "@nextLink": "failurehits?applicationId=9NBLGGGZ5QDR&aggregationLevel=week&startDate=2015/03/01&endDate=2016/02/01&top=1&skip=1",
  "TotalCount": 191753
}

```

## Related topics

* [Health report](../publish/health-report.md)
* [Get details for an error in your app](get-details-for-an-error-in-your-app.md)
* [Get the stack trace for an error in your app](get-the-stack-trace-for-an-error-in-your-app.md)
* [Download the CAB file for an error in your app](download-the-cab-file-for-an-error-in-your-app.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get app acquisitions](get-app-acquisitions.md)
* [Get add-on acquisitions](get-in-app-acquisitions.md)
* [Get app ratings](get-app-ratings.md)
* [Get app reviews](get-app-reviews.md)
