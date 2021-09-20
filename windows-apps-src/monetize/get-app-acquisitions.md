---
ms.assetid: C1E42E8B-B97D-4B09-9326-25E968680A0F
description: Use this method in the Microsoft Store analytics API to get aggregate acquisition data for an application during a given date range and other optional filters.
title: Get app acquisitions
ms.date: 03/23/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, app acquisitions
ms.localizationpriority: medium
---
# Get app acquisitions


Use this method in the Microsoft Store analytics API to get aggregate acquisition data in JSON format for an application during a given date range and other optional filters. This information is also available in the [Acquisitions report](../publish/acquisitions-report.md) in Partner Center.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/appacquisitions``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve acquisition data.  |  Yes  |
| startDate | date | The start date in the date range of acquisition data to retrieve. The default is the current date. |  No  |
| endDate | date | The end date in the date range of acquisition data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter | string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. For example, *filter=market eq 'US' and gender eq 'm'*. <p/><p/>You can specify the following fields from the response body:<p/><ul><li><strong>acquisitionType</strong></li><li><strong>ageGroup</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>orderName</strong></li></ul> | No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: <strong>day</strong>, <strong>week</strong>, or <strong>month</strong>. If unspecified, the default is <strong>day</strong>. | No |
| orderby | string | A statement that orders the result data values for each acquisition. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>acquisitionType</strong></li><li><strong>ageGroup</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>orderName</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li><strong>date</strong></li><li><strong>applicationName</strong></li><li><strong>acquisitionType</strong></li><li><strong>ageGroup</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>orderName</strong></li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li><strong>date</strong></li><li><strong>applicationId</strong></li><li><strong>acquisitionQuantity</strong></li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=ageGroup,market&amp;aggregationLevel=week</em></p> |  No  |

### Request example

The following example demonstrates several requests for getting app acquisition data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/appacquisitions?applicationId=9NBLGGGZ5QDR&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0  HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/appacquisitions?applicationId=9NBLGGGZ5QDR&startDate=8/1/2015&endDate=8/31/2015&skip=0&filter=market eq 'US' and gender eq 'm'  HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain aggregate acquisition data for the app. For more information about the data in each object, see the [acquisition values](#acquisition-values) section below.                                                                                                                      |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of acquisition data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.                 |


### Acquisition values

Elements in the *Value* array contain the following values.

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| date                | string | The first date in the date range for the acquisition data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| applicationId       | string | The Store ID of the app for which you are retrieving acquisition data.     |
| applicationName     | string | The display name of the app.   |
| deviceType          | string | One of the following strings that specifies the type of device on which the acquisition occurred:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console-Xbox One</strong></li><li><strong>Console-Xbox Series X</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul>    |
| orderName           | string | The name of the order.  |
| storeClient         | string | One of the following strings that indicates the version of the Store where the acquisition occurred:<ul><li>**Windows Phone Store (client)**</li><li>**Microsoft Store (client)** (or **Windows Store (client)** if querying for data before March 23, 2018)</li><li>**Microsoft Store (web)** (or **Windows Store (web)** if querying for data before March 23, 2018)</li><li>**Volume purchase by organizations**</li><li>**Other**</li></ul>                                                                                            |
| osVersion           | string | One of the following strings that specifies the OS version on which the acquisition occurred:<ul><li><strong>Windows Phone 7.5</strong></li><li><strong>Windows Phone 8</strong></li><li><strong>Windows Phone 8.1</strong></li><li><strong>Windows Phone 10</strong></li><li><strong>Windows 8</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Unknown</strong></li></ul>  |
| market              | string | The ISO 3166 country code of the market where the acquisition occurred.  |
| gender              | string | One of the following strings that specifies the gender of the user who made the acquisition:<ul><li><strong>m</strong></li><li><strong>f</strong></li><li><strong>Unknown</strong></li></ul>    |
| ageGroup            | string | One of the following strings that specifies the age group of the user who made the acquisition:<ul><li><strong>less than 13</strong></li><li><strong>13-17</strong></li><li><strong>18-24</strong></li><li><strong>25-34</strong></li><li><strong>35-44</strong></li><li><strong>44-55</strong></li><li><strong>greater than 55</strong></li><li><strong>Unknown</strong></li></ul>  |
| acquisitionType     | string | One of the following strings that indicates the type of acquisition:<ul><li><strong>Free</strong></li><li><strong>Trial</strong></li><li><strong>Paid</strong></li><li><strong>Promotional code</strong></li><li><strong>Iap</strong></li><li><strong>Subscription Iap</strong></li><li><strong>Private Audience</strong></li><li><strong>Pre Order</strong></li><li><strong>Xbox Game Pass</strong> (or <strong>Game Pass</strong> if querying for data before March 23, 2018)</li><li><strong>Disk</strong></li><li><strong>Prepaid Code</strong></li></ul>   |
| acquisitionQuantity | number | The number of acquisitions that occurred during the specified aggregation level.    |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2016-02-01",
      "applicationId": "9NBLGGGZ5QDR",
      "applicationName": "Contoso Demo",
      "deviceType": "Phone",
      "orderName": "",
      "storeClient": "Windows Phone Store (client)",
      "osVersion": "Windows Phone 8.1",
      "market": "IT",
      "gender": "m",
      "ageGroup": "0-17",
      "acquisitionType": "Free",
      "acquisitionQuantity": 1
    }
  ],
  "@nextLink": "appacquisitions?applicationId=9NBLGGGZ5QDR&aggregationLevel=day&startDate=2015/01/01&endDate=2016/02/01&top=1&skip=1&orderby=date desc",
  "TotalCount": 466766
}
```

## Related topics

* [Acquisitions report](../publish/acquisitions-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get app acquisition funnel data](get-acquisition-funnel-data.md)
* [Get app conversions by channel](get-app-conversions-by-channel.md)
* [Get add-on acquisitions](get-in-app-acquisitions.md)
