---
ms.assetid: 1599605B-4243-4081-8D14-40F6F7734E25
description: Use this method in the Microsoft Store analytics API to get aggregate acquisition data for an add-on during a given date range and other optional filters.
title: Get add-on acquisitions
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, add-on acquisitions
ms.localizationpriority: medium
---
# Get add-on acquisitions

Use this method in the Microsoft Store analytics API to get aggregate acquisition data for add-ons for your app in JSON format during a given date range and other optional filters. This information is also available in the [Add-on acquisitions report](../publish/add-on-acquisitions-report.md) in Partner Center.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                                |
|--------|----------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/inappacquisitions``` |


### Request header

| Header        | Type   | Description          |
|---------------|--------|--------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

The *applicationId* or *inAppProductId* parameter is required. To retrieve acquisition data for all add-ons registered to the app, specify the *applicationId* parameter. To retrieve acquisition data for a single add-on, specify the *inAppProductId* parameter. If you specify both, the *applicationId* parameter is ignored.

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve add-on acquisition data.  |  Yes  |
| inAppProductId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the add-on for which you want to retrieve acquisition data.  | Yes  |
| startDate | date | The start date in the date range of add-on acquisition data to retrieve. The default is the current date. |  No  |
| endDate | date | The end date in the date range of add-on acquisition data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter |string  | One or more statements that filter the rows in the response. For more information, see the [filter fields](#filter-fields) section below. | No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: <strong>day</strong>, <strong>week</strong>, or <strong>month</strong>. If unspecified, the default is <strong>day</strong>. | No |
| orderby | string | A statement that orders the result data values for each add-on acquisition. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>acquisitionType</strong></li><li><strong>ageGroup</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>orderName</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li><strong>date</strong></li><li><strong>applicationName</strong></li><li><strong>inAppProductName</strong></li><li><strong>acquisitionType</strong></li><li><strong>ageGroup</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>orderName</strong></li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li><strong>date</strong></li><li><strong>applicationId</strong></li><li><strong>inAppProductId</strong></li><li><strong>acquisitionQuantity</strong></li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=ageGroup,market&amp;aggregationLevel=week</em></p> |  No  |


### Filter fields

The *filter* parameter of the request contains one or more statements that filter the rows in the response. Each statement contains a field and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. Here are some example *filter* parameters:

-   *filter=market eq 'US' and gender eq 'm'*
-   *filter=(market ne 'US') and (gender ne 'Unknown') and (gender ne 'm') and (market ne 'NO') and (ageGroup ne 'greater than 55' or ageGroup ne ‘less than 13’)*

For a list of the supported fields, see the following table. String values must be surrounded by single quotes in the *filter* parameter.

| Fields        |  Description        |
|---------------|-----------------|
| acquisitionType | One of the following strings:<ul><li><strong>free</strong></li><li><strong>trial</strong></li><li><strong>paid</strong></li><li><strong>promotional code</strong></li><li><strong>iap</strong></li></ul> |
| ageGroup | One of the following strings:<ul><li><strong>less than 13</strong></li><li><strong>13-17</strong></li><li><strong>18-24</strong></li><li><strong>25-34</strong></li><li><strong>35-44</strong></li><li><strong>44-55</strong></li><li><strong>greater than 55</strong></li><li><strong>Unknown</strong></li></ul> |
| storeClient | One of the following strings:<ul><li><strong>Windows Phone Store (client)</strong></li><li><strong>Microsoft Store (client)</strong></li><li><strong>Microsoft Store (web)</strong></li><li><strong>Volume purchase by organizations</strong></li><li><strong>Other</strong></li></ul> |
| gender | One of the following strings:<ul><li><strong>m</strong></li><li><strong>f</strong></li><li><strong>Unknown</strong></li></ul> |
| market | A string that contains the ISO 3166 country code of the market where the acquisition occurred. |
| osVersion | One of the following strings:<ul><li><strong>Windows Phone 7.5</strong></li><li><strong>Windows Phone 8</strong></li><li><strong>Windows Phone 8.1</strong></li><li><strong>Windows Phone 10</strong></li><li><strong>Windows 8</strong></li><li><strong>Windows 8.1</strong></li><li><strong>Windows 10</strong></li><li><strong>Unknown</strong></li></ul> |
| deviceType | One of the following strings:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console-Xbox One</strong></li><li><strong>Console-Xbox Series X</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul> |
| orderName | A string that specifies the name of the order for the promotional code that was used to acquire the add-on (this only applies if the user acquired the add-on by redeeming a promotional code). |


### Request example

The following examples demonstrates several requests for getting add-on acquisition data. Replace the *inAppProductId* and *applicationId* values with the appropriate Store ID for your add-on or app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/inappacquisitions?inAppProductId=9NBLGGGZ5QDR&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/inappacquisitions?applicationId=9NBLGGGZ5QDR&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/inappacquisitions?inAppProductId=9NBLGGGZ5QDR&startDate=1/1/2015&endDate=7/3/2015&top=100&skip=0&filter=market ne 'US' and gender ne 'Unknown' and gender ne 'm' and market ne 'NO' and ageGroup ne '>55' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description         |
|------------|--------|------------------|
| Value      | array  | An array of objects that contain aggregate add-on acquisition data. For more information about the data in each object, see the [add-on acquisition values](#add-on-acquisition-values) section below.                                                                                                              |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of add-on acquisition data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.    |


<span id="add-on-acquisition-values" />

### Add-on acquisition values

Elements in the *Value* array contain the following values.

| Value               | Type    | Description        |
|---------------------|---------|---------------------|
| date                | string  | The first date in the date range for the acquisition data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| inAppProductId      | string  | The Store ID of the add-on for which you are retrieving acquisition data.                                                                                                                                                                 |
| inAppProductName    | string  | The display name of the add-on. This value only appears in the response data if the *aggregationLevel* parameter is set to **day**, unless you specify the **inAppProductName** field in the *groupby* parameter.                                                                                                                                                                                                            |
| applicationId       | string  | The Store ID of the app for which you want to retrieve add-on acquisition data.                                                                                                                                                           |
| applicationName     | string  | The display name of the app.                                                                                                                                                                                                             |
| deviceType          | string  | The type of device that completed the acquisition. For a list of the supported strings, see the [filter fields](#filter-fields) section above.                                                                                                  |
| orderName           | string  | The name of the order.                                                                                                                                                                                                                   |
| storeClient         | string  | The version of the Store where the acquisition occurred. For a list of the supported strings, see the [filter fields](#filter-fields) section above.                                                                                            |
| osVersion           | string  | The OS version on which the acquisition occurred. For a list of the supported strings, see the [filter fields](#filter-fields) section above.                                                                                                   |
| market              | string  | The ISO 3166 country code of the market where the acquisition occurred.                                                                                                                                                                  |
| gender              | string  | The gender of the user who made the acquisition. For a list of the supported strings, see the [filter fields](#filter-fields) section above.                                                                                                    |
| ageGroup            | string  | The age group of the user who made the acquisition. For a list of the supported strings, see the [filter fields](#filter-fields) section above.                                                                                                 |
| acquisitionType     | string  | The type of acquisition (free, paid, and so on). For a list of the supported strings, see the [filter fields](#filter-fields) section above.                                                                                                    |
| acquisitionQuantity | integer | The number of acquisitions that occurred.                        |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2015-01-02",
      "inAppProductId": "9NBLGGH3LHKL",
      "inAppProductName": "Contoso add-on 7",
      "applicationId": "9NBLGGGZ5QDR",
      "applicationName": "Contoso Demo",
      "deviceType": "Phone",
      "orderName": "",
      "storeClient": "Windows Phone Store (client)",
      "osVersion": "Windows Phone 8.1",
      "market": "GB",
      "gender": "m",
      "ageGroup": "50orover",
      "acquisitionType": "iap",
      "acquisitionQuantity": 1
    }
  ],
  "@nextLink": "inappacquisitions?applicationId=9NBLGGGZ5QDR&inAppProductId=&aggregationLevel=day&startDate=2015/01/01&endDate=2016/02/01&top=1&skip=1",
  "TotalCount": 33677
}
```

## Related topics

* [Add-on acquisitions report](../publish/add-on-acquisitions-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get add-on conversions by channel](get-add-on-conversions-by-channel.md)
* [Get app acquisitions](get-app-acquisitions.md)
* [Get app acquisition funnel data](get-acquisition-funnel-data.md)
* [Get app conversions by channel](get-app-conversions-by-channel.md)

 

 
