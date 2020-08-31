---
description: Use this method in the Microsoft Store analytics API to get Xbox Live multiplayer data.
title: Get Xbox Live multiplayer data
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox Live analytics, multiplayer
ms.localizationpriority: medium
---
# Get Xbox Live multiplayer data


Use this method in the Microsoft Store analytics API to get multiplayer data for your [Xbox Live-enabled game](/gaming/xbox-live/index.md) on a daily or monthly basis. This information is also available in the [Xbox analytics report](../publish/xbox-analytics-report.md) in Partner Center.

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
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you want to retrieve Xbox Live multiplayer data.  |  Yes  |
| metricType | string | A string that specifies the type of Xbox Live analytics data to retrieve. For this method, specify the value **multiplayerdaily** to get daily multiplayer data or **multiplayermonthly** to get monthly multiplayer data.  |  Yes  |
| startDate | date | The start date in the date range of multiplayer data to retrieve. For **multiplayerdaily**, the default is 3 months before the current date. For **multiplayermonthly**, the default is 1 year before the current date. |  No  |
| endDate | date | The end date in the date range of multiplayer data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter | string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:<p/><ul><li><strong>deviceType</strong></li><li><strong>packageVersion</strong></li><li><strong>market</strong></li><li><strong>subscriptionName</strong></li></ul> | No   |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields from the response body:<p/><ul><li><strong>date</strong></li><li><strong>deviceType</strong></li><li><strong>packageVersion</strong></li><li><strong>market</strong></li><li><strong>subscriptionName</strong></li></ul><p/>If you specify one or more *groupby* fields, any other *groupby* fields you do not specify will have the value **All** in the response body. |  No  |


### Request example

The following example demonstrates a request for getting multiplayer data for your Xbox Live-enabled game. Replace the *applicationId* value with the Store ID for your game.


```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics?applicationId=9NBLGGGZ5QDR&metrictype=multiplayerdaily&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain multiplayer data, where each object represents a set of data for the specified daily or monthly time period and organized by the specified **filter** and **groupby** values. For more information about the data in each object, see the [Daily multiplayer analytics](#daily-multiplayer-analytics) and [Monthly multiplayer analytics](#monthly-multiplayer-analytics) sections.     |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query. |


### Daily multiplayer analytics

Elements in the *Value* array contain the following values when you request daily multiplayer analytics data (that is, when you specify **multiplayerdaily** for the **metricType** parameter).

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| date                | string | The date for the multiplayer data. |
| applicationId       | string | The Store ID of the game for which you are retrieving multiplayer data.     |
| applicationName       | string |  The name of the game for which you are retrieving multiplayer data.     |
| market       | string | The two-letter ISO 3166 country code of the market where the multiplayer data came from.       |
| packageVersion     | string |  The four-part package version for the game.  |
| deviceType          | string | One of the following strings that specifies the type of device where the multiplayer data came from:<p/><ul><li><strong>Console</strong></li><li><strong>PC</strong></li><li>**Unknown**</li></ul>  |
| subscriptionName     | string |  The name of the subscription used for the multiplayer data. Possible values include **Xbox Game Pass** and **""** (for no subscription).  |
| dailySessionCount     | number |  The number of multiplayer sessions for the game on the specified date.  |
| engagementDurationMinutes     | number |  The total number of minutes that customers were engaged with multiplayer sessions for the game on the specified date.  |
| dailyActiveUsers     | number |  The total number of active multiplayer users for the game on the specified date.  |
| dailyActiveDevices     | number |  The total number of active devices that played multiplayer sessions for the game on the specified date.  |
| dailyNewUsers     | number |  The total number of new multiplayer users for the game on the specified date.  |
| monthlyActiveUsers     | number |  The total number of active multiplayer users for the month in which the specified date occurred.  |
| monthlyActiveDevices     | number | The total number of active devices that played multiplayer sessions for the game for the month in which the specified date occurred.   |
| monthlyNewUsers     | number |  The total number of new multiplayer users for the game for the month in which the specified date occurred.  |


### Monthly multiplayer analytics

Elements in the *Value* array contain the following values when you request monthly multiplayer analytics data (that is, when you specify **multiplayermonthly** for the **metricType** parameter).

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| date                | string | The first date of the month for the multiplayer data. |
| applicationId       | string | The Store ID of the game for which you are retrieving multiplayer data.     |
| applicationName       | string |  The name of the game for which you are retrieving multiplayer data.     |
| market       | string | The two-letter ISO 3166 country code of the market where the multiplayer data came from.       |
| packageVersion     | string |  The four-part package version for the game.  |
| deviceType          | string | One of the following strings that specifies the type of device where the multiplayer data came from:<p/><ul><li><strong>Console</strong></li><li><strong>PC</strong></li><li>**Unknown**</li></ul>  |
| subscriptionName     | string |  The name of the subscription used for the multiplayer data. Possible values include **Xbox Game Pass** and **""** (for no subscription).  |
| monthlySessionCount     | number |  The number of multiplayer sessions for the game during the specified month.   |
| engagementDurationMinutes     | number |  The total number of minutes that customers were engaged with multiplayer sessions for the game during the specified month.  |
| monthlyActiveUsers     | number | The total number of active multiplayer users during the specified month.   |
| monthlyActiveDevices     | number | The total number of active devices that played multiplayer sessions for the game during the specified month.   |
| monthlyNewUsers     | number |  The total number of new multiplayer users for the game during the specified month.  |
| averageDailyActiveUsers     | number |  The average number of daily active multiplayer users for the game during the specified month.  |
| averageDailyActiveDevices     | number |  The average number of active devices that played multiplayer sessions for the game during the specified month.  |


### Response example

The following example demonstrates an example JSON response body for the daily metrics variant of this request (that is, when you specify **multiplayerdaily** for the **metricType** parameter).

```json
{
  "Value": [
    {
      "date": "2018-01-07",
      "applicationId": "9NBLGGGZ5QDR",
      "applicationName": "Contoso Sports",
      "market": "All",
      "packageVersion": "",
      "deviceType": "All",
      "subscriptionName": "All",
      "dailySessionCount": 976711,
      "engagementDurationMinutes": 16836064.5,
      "dailyActiveUsers": 180377,
      "dailyActiveDevices": 153359,
      "dailyNewUsers": 8638,
      "monthlyActiveUsers": 779984,
      "monthlyActiveDevices": 606495,
      "monthlyNewUsers": 212093
    },
    {
      "date": "2018-01-05",
      "applicationId": "9NBLGGGZ5QDR",
      "applicationName": "Contoso Sports",
      "market": "All",
      "packageVersion": "",
      "deviceType": "All",
      "subscriptionName": "All",
      "dailySessionCount": 857433,
      "engagementDurationMinutes": 14087724.5,
      "dailyActiveUsers": 166630,
      "dailyActiveDevices": 143065,
      "dailyNewUsers": 9646,
      "monthlyActiveUsers": 764947,
      "monthlyActiveDevices": 595368,
      "monthlyNewUsers": 204248
    },
  ],
  "@nextLink": null,
  "TotalCount":2
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get Xbox Live analytics data](get-xbox-live-analytics.md)
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live health data](get-xbox-live-health-data.md)
* [Get Xbox Live Game Hub data](get-xbox-live-game-hub-data.md)
* [Get Xbox Live club data](get-xbox-live-club-data.md)