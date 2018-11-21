---
ms.assetid: 235EBA39-8F64-4499-9833-4CCA9C737477
description: Use this method in the Microsoft Store analytics API to get aggregate ad performance data for an application during a given date range and other optional filters.
title: Get ad performance data
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, ads, performance
ms.localizationpriority: medium
---
# Get ad performance data


Use this method in the Microsoft Store analytics API to get aggregate ad performance data for your applications during a given date range and other optional filters. This method returns the data in JSON format.

This method returns the same data that is provided by the [Advertising performance report](../publish/advertising-performance-report.md) in Partner Center.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

For more information, see [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md).

## Request


### Request syntax

| Method | Request URI                                                              |
|--------|--------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/adsperformance``` |


### Request header

| Header        | Type   | Description           |
|---------------|--------|--------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

To retrieve ad performance data for a specific app, use the *applicationId* parameter. To retrieve ad performance data for all apps that are associated with your developer account, omit the *applicationId* parameter.

| Parameter     | Type   | Description     | Required |
|---------------|--------|-----------------|----------|
| applicationId   | string    | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve ad performance data.  |    No      |
| startDate   | date    | The start date in the date range of ad performance data to retrieve, in the format YYYY/MM/DD. The default is the current date minus 30 days. |    No      |
| endDate   | date    | The end date in the date range of ad performance data to retrieve, in the format YYYY/MM/DD. The default is the current date minus one day. |    No      |
| top   | int    | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |    No      |
| skip   | int    | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |    No      |
| filter   | string    | One or more statements that filter the rows in the response. For more information, see the [filter fields](#filter-fields) section below. |    No      |
| aggregationLevel   | string    | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: <strong>day</strong>, <strong>week</strong>, or <strong>month</strong>. If unspecified, the default is <strong>day</strong>. |    No      |
| orderby   | string    | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li><li><strong>adUnitId</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |    No      |
| groupby   | string    | A statement that applies data aggregation only to the specified fields. You can specify the following fields:</p><ul><li><strong>applicationId</strong></li><li><strong>applicationName</strong></li><li><strong>date</strong></li><li><strong>accountCurrencyCode</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li><li><strong>adUnitName</strong></li><li><strong>adUnitId</strong></li><li><strong>pubCenterAppName</strong></li><li><strong>adProvider</strong></li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=applicationId&amp;aggregationLevel=week</em></p> |    No      |


### Filter fields

The *filter* parameter of the request body contains one or more statements that filter the rows in the response. Each statement contains a field and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. Here is an example *filter* parameter:

-   *filter=market eq 'US' and deviceType eq 'phone'*

For a list of the supported fields, see the following table. String values must be surrounded by single quotes in the *filter* parameter.

| Field | Description                                                              |
|--------|--------------------------------------------------------------------------|
| market    | A string that contains the ISO 3166 country code of the market where the ads were served. |
| deviceType    | One of the following strings: <strong>PC/Tablet</strong> or <strong>Phone</strong>. |
| adUnitId    | A string that specifies an ad unit ID to apply to the filter. |
| pubCenterAppName    | A string that specifies the pubCenter name for the current app to apply to the filter. |
| adProvider    | A string that specifies an ad provider name to apply to the filter. |
| date    | A string that specifies a date in YYYY/MM/DD format to apply to the filter. |


### Request example

The following example demonstrates several requests for getting ad performance data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/adsperformance?applicationId=9NBLGGH4R315&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0  HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/adsperformance?applicationId=9NBLGGH4R315&startDate=8/1/2015&endDate=8/31/2015&skip=0&$filter=market eq 'US' and deviceType eq 'phone’ eq 'US'; and gender eq 'm'  HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description                                                                                                                                                                                                                                                                            |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Value      | array  | An array of objects that contain aggregate ad performance data. For more information about the data in each object, see the [ad performance values](#ad-performance-values) section below.                                                                                                                      |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 5 but there are more than 5 items of data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.                          |


### Ad performance values

Elements in the *Value* array contain the following values.

| Value               | Type   | Description                                                                                                                                                                                                                              |
|---------------------|--------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| date                | string | The first date in the date range for the ad performance data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| applicationId       | string | The Store ID of the app for which you are retrieving ad performance data.     |
| applicationName     | string | The display name of the app.                         |
| adUnitId           | string | The ID of the ad unit.        |
| adUnitName           | string | The name of the ad unit, as specified by the developer in Partner Center.              |
| adProvider           |  string  |  The name of the ad provider   |
| deviceType          | string | The type of device on which the ads were served. For a list of the supported strings, see the [filter fields](#filter-fields) section above.                              |
| market              | string | The ISO 3166 country code of the market where the ads were served.             |
| accountCurrencyCode     | string | The currency code for the account.        |
| pubCenterAppName       |  string  |   The name of the pubCenter app that is associated with the app in Partner Center.   |
| adProviderRequests        | int | The number of ad requests for the specified ad provider.                 |
| impressions           | int | The number of ad impressions.        |
| clicks            | int | The number of ad clicks.       |
| revenueInAccountCurrency       | number | The revenue, in the currency for the country/region of the account.       |
| requests              | int | The number of ad requests.                 |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2015-03-09",
      "applicationId": "9NBLGGH4R315",
      "applicationName": "Contoso Demo",
      "market": "US",
      "deviceType": "phone",
      "adUnitId":"10765920",
      "adUnitName":"TestAdUnit",
      "revenueInAccountCurrency": 10.0,
      "impressions": 1000,
      "requests": 10000,
      "clicks": 1,
      "accountCurrencyCode":"USD"
    },
    {
      "date": "2015-03-09",
      "applicationId": "9NBLGGH4R315",
      "applicationName": "Contoso Demo",
      "market": "US",
      "deviceType": "phone",
      "adUnitId":"10795110",
      "adUnitName":"TestAdUnit2",
      "revenueInAccountCurrency": 20.0,
      "impressions": 2000,
      "requests": 20000,
      "clicks": 3,
      "accountCurrencyCode":"USD"
    },
  ],
  "@nextLink": "adsperformance?applicationId=9NBLGGH4R315&aggregationLevel=week&startDate=2015/03/01&endDate=2016/02/01&top=2&skip=2",
  "TotalCount": 191753
}

```

## Related topics

* [Advertising performance report](../publish/advertising-performance-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
