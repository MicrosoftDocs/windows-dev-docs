---
description: Use this method in the Microsoft Store analytics API to get Xbox Live concurrent usage data.
title: Get Xbox Live concurrent usage data
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox Live analytics, concurrent usage
ms.localizationpriority: medium
---
# Get Xbox Live concurrent usage data


Use this method in the Microsoft Store analytics API to get near real-time usage data (with 5-15 minutes latency) about the average number of customers playing your [Xbox Live-enabled game](/gaming/xbox-live/index.md) every minute, hour, or day during a specified time range. This information is also available in the [Xbox analytics report](../publish/xbox-analytics-report.md) in Partner Center.

> [!IMPORTANT]
> This method only supports games for Xbox or games that use Xbox Live services. These games must go through the [concept approval process](../gaming/concept-approval.md), which includes games published by [Microsoft partners](/gaming/xbox-live/developer-program-overview.md#microsoft-partners) and games submitted via the [ID@Xbox program](/gaming/xbox-live/developer-program-overview.md#id). This method does not currently support games published via the [Xbox Live Creators Program](/gaming/xbox-live/get-started-with-creators/get-started-with-xbox-live-creators.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters


| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you want to retrieve Xbox Live concurrent usage data.  |  Yes  |
| metricType | string | A string that specifies the type of Xbox Live analytics data to retrieve. For this method, specify the value **concurrency**.  |  Yes  |
| startDate | date | The start date in the date range of concurrent usage data to retrieve. See the *aggregationLevel* description for default behavior. |  No  |
| endDate | date | The end date in the date range of concurrent usage data to retrieve. See the *aggregationLevel* description for default behavior. |  No  |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: **minute**, **hour**, or **day**. If unspecified, the default is **day**. <p/><p/>If you do not specify *startDate* or *endDate*, the response body defaults to the following: <ul><li>**minute**: The last 60 records of available data.</li><li>**hour**: The last 24 records of available data.</li><li>**day**: The last 7 records of available data.</li></ul><p/>The following aggregation levels have size limits on the number of records that can be returned. The records will be truncated if the requested time span is too large. <ul><li>**minute**: Up to 1440 records (24 hours of data).</li><li>**hour**: Up to 720 records (30 days of data).</li><li>**day**: Up to 60 records (60 days of data).</li></ul>  |  No  |


### Request example

The following example demonstrates a request for getting concurrent usage data for your Xbox Live-enabled game. This request retrieves data for every minute between February 1 2018 and February 2 2018. Replace the *applicationId* value with the Store ID for your game.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics?applicationId=9NBLGGGZ5QDR&metrictype=concurrency&aggregationLevel=hour&startDate=2018-02-01&endData=2018-02-02 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The response body contains an array of objects that each contain one set of concurrent usage data for a specified minute, hour, or day. Each object contains the following values.

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Count      | number  | The average number of customers playing your Xbox Live-enabled for the specified minute, hour, or day. <p/><p/>**Note**&nbsp;&nbsp;A value of 0 indicates either that there were no concurrent users during the specified interval, or that there was a failure while collecting concurrent user data for the game during the specified interval. |
| Date  | string | The date and time that specifies the minute, hour or day during which the concurrent usage data occurred.  |
| SeriesName | string    | This always has the value **UserConcurrency**. |


### Response example

The following example demonstrates an example JSON response body for this request with data aggregation by minute.

```json
[   {
        "Count": 418.0,
        "Date": "2018-02-02T04:42:13.65Z",
        "SeriesName": "UserConcurrency"
    }, {
        "Count": 418.0,
        "Date": "2018-02-02T04:43:13.65Z",
        "SeriesName": "UserConcurrency"
    }, {
        "Count": 415.0,
        "Date": "2018-02-02T04:44:13.65Z",
        "SeriesName": "UserConcurrency"
    }, {
        "Count": 412.0,
        "Date": "2018-02-02T04:45:13.65Z",
        "SeriesName": "UserConcurrency"
    }, {
        "Count": 414.0,
        "Date": "2018-02-02T04:46:13.65Z",
        "SeriesName": "UserConcurrency"
    }
]
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get Xbox Live analytics data](get-xbox-live-analytics.md)
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live health data](get-xbox-live-health-data.md)
* [Get Xbox Live game hub data](get-xbox-live-game-hub-data.md)
* [Get Xbox Live club data](get-xbox-live-club-data.md)
* [Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)