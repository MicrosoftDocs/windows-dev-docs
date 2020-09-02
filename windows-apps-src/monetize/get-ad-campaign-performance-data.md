---
ms.assetid: A26A287C-B4B0-49E9-BB28-6F02472AE1BA
description: Use this method in the Microsoft Store analytics API to get aggregate ad campaign performance data for the specified application during a given date range and other optional filters.
title: Get ad campaign performance data
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, ad campaigns
ms.localizationpriority: medium
---
# Get ad campaign performance data


Use this method in the Microsoft Store analytics API to get an aggregate summary of promotional ad campaign performance data for your applications during a given date range and other optional filters. This method returns the data in JSON format.

This method returns the same data that is provided by the [Ad campaign report](/windows/uwp/publish/ad-campaign-report) in Partner Center. For more information about ad campaigns, see [Create an ad campaign for your app](../publish/create-an-ad-campaign-for-your-app.md).

To create, update, or retrieve details for ad campaigns, you can use the [Manage ad campaigns](manage-ad-campaigns.md) methods in the [Microsoft Store promotions API](run-ad-campaigns-using-windows-store-services.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                              |
|--------|--------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/promotion``` |


### Request header

| Header        | Type   | Description                |
|---------------|--------|---------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

To retrieve ad campaign performance data for a specific app, use the *applicationId* parameter. To retrieve ad performance data for all apps that are associated with your developer account, omit the *applicationId* parameter.

| Parameter     | Type   | Description     | Required |
|---------------|--------|-----------------|----------|
| applicationId   | string    | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve ad campaign performance data. |    No      |
|  startDate  |  date   |  The start date in the date range of ad campaign performance data to retrieve, in the format YYYY/MM/DD. The default is the current date minus 30 days.   |   No    |
| endDate   |  date   |  The end date in the date range of ad campaign performance data to retrieve, in the format YYYY/MM/DD. The default is the current date minus one day.   |   No    |
| top   |  int   |  The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data.   |   No    |
| skip   | int    |  The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on.   |   No    |
| filter   |  string   |  One or more statements that filter the rows in the response. The only supported filter is **campaignId**. Each statement can use the **eq** or **ne** operators, and statements can be combined using **and** or **or**.  Here is an example *filter* parameter: ```filter=campaignId eq '100023'```.   |   No    |
|  aggregationLevel  |  string   | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: <strong>day</strong>, <strong>week</strong>, or <strong>month</strong>. If unspecified, the default is <strong>day</strong>.    |   No    |
| orderby   |  string   |  <p>A statement that orders the result data values for the ad campaign performance data. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:</p><ul><li><strong>date</strong></li><li><strong>campaignId</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,campaignId</em></p>   |   No    |
|  groupby  |  string   |  <p>A statement that applies data aggregation only to the specified fields. You can specify the following fields:</p><ul><li><strong>campaignId</strong></li><li><strong>applicationId</strong></li><li><strong>date</strong></li><li><strong>currencyCode</strong></li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=applicationId&amp;aggregationLevel=week</em></p>   |   No    |


### Request example

The following example demonstrates several requests for getting ad campaign performance data.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/promotion?aggregationLevel=week&groupby=applicationId,campaignId,date  HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/promotion?applicationId=9NBLGGH0XK8Z&startDate=2015/1/20&endDate=2016/8/31&skip=0&filter=campaignId eq '31007388' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description  |
|------------|--------|---------------|
| Value      | array  | An array of objects that contain aggregate ad campaign performance data. For more information about the data in each object, see the [campaign performance object](#campaign-performance-object) section below.          |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 5 but there are more than 5 items of data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.                                |


<span id="campaign-performance-object" />


### Campaign performance object

Elements in the *Value* array contain the following values.

| Value               | Type   | Description            |
|---------------------|--------|------------------------|
| date                | string | The first date in the date range for the ad campaign performance data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| applicationId       | string | The Store ID of the app for which you are retrieving ad campaign performance data.                     |
| campaignId     | string | The ID of the ad campaign.           |
| lineId     | string |    The ID of the ad campaign [delivery line](manage-delivery-lines-for-ad-campaigns.md) that generated this performance data.        |
| currencyCode              | string | The currency code of the campaign budget.              |
| spend          | string |  The budget amount that has been spent for the ad campaign.     |
| impressions           | long | The number of ad impressions for the campaign.        |
| installs              | long | The number of app installs related to the campaign.   |
| clicks            | long | The number of ad clicks for the campaign.      |
| iapInstalls            | long | The number of add-on (also called in-app purchase or IAP) installs related to the campaign.      |
| activeUsers            | long | The number of users who have clicked an ad that is part of the campaign and returned to the app.      |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2015-04-12",
      "applicationId": "9WZDNCRFJ31Q",
      "campaignId": "4568",
      "lineId": "0001",
      "currencyCode": "USD",
      "spend": 700.6,
      "impressions": 200,
      "installs": 30,
      "clicks": 8,
      "iapInstalls": 0,
      "activeUsers": 0
    },
    {
      "date": "2015-05-12",
      "applicationId": "9WZDNCRFJ31Q",
      "campaignId": "1234",
      "lineId": "0002",
      "currencyCode": "USD",
      "spend": 325.3,
      "impressions": 20,
      "installs": 2,
      "clicks": 5,
      "iapInstalls": 0,
      "activeUsers": 0
    }
  ],
  "@nextLink": "promotion?applicationId=9NBLGGGZ5QDR&aggregationLevel=day&startDate=2015/1/20&endDate=2016/8/31&top=2&skip=2",
  "TotalCount": 1917
}
```

## Related topics

* [Create an ad campaign for your app](../publish/create-an-ad-campaign-for-your-app.md)
* [Run ad campaigns using Microsoft Store services](run-ad-campaigns-using-windows-store-services.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)