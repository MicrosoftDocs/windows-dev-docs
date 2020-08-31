---
description: Use this method in the Microsoft Store analytics API to get Xbox Live Game Hub data.
title: Get Xbox Live game hub data
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox Live analytics, Game Hubs
ms.localizationpriority: medium
---
# Get Xbox Live Game Hub data


Use this method in the Microsoft Store analytics API to get Game Hub data for your [Xbox Live-enabled game](/gaming/xbox-live/index.md). This information is also available in the [Xbox analytics report](../publish/xbox-analytics-report.md) in Partner Center.

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
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you want to retrieve Xbox Live Game Hub data.  |  Yes  |
| metricType | string | A string that specifies the type of Xbox Live analytics data to retrieve. For this method, specify the value **communitymanagergamehub**.  |  Yes  |
| startDate | date | The start date in the date range of Game Hub data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of Game Hub data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |


### Request example

The following example demonstrates a request for getting Game Hub data for your Xbox Live-enabled game. Replace the *applicationId* value with the Store ID for your game.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics?applicationId=9NBLGGGZ5QDR&metrictype=communitymanagergamehub&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain Game Hub data for each date in the specified date range. For more information about the data in each object, see the following table.                                                                                                                      |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.  |


Elements in the *Value* array contain the following values.

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| date                | string | The date for the Game Hub data in this object. |
| applicationId       | string | The Store ID of the game for which you are retrieving Game Hub data.     |
| gameHubLikeCount     | number |   The number of likes added to the Game Hub page on the specified date.   |
| gameHubCommentCount          | number |  The number of comments added to the Game Hub page for your app on the specified date.  |
| gameHubShareCount           | number | The number of times the Game Hub page for your app was shared by customers on the specified date.   |
| gameHubFollowerCount          | number | The number of all-time followers for the Game Hub page for your app.   |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2018-01-04",
      "applicationId": "9NBLGGGZ5QDR",
      "gameHubLikeCount": 10,
      "gameHubCommentCount": 1,
      "gameHubShareCount": 0,
      "gameHubFollowerCount": 15924
    },
    {
      "date": "2018-01-05",
      "applicationId": "9NBLGGGZ5QDR",
      "gameHubLikeCount": 12,
      "gameHubCommentCount": 1,
      "gameHubShareCount": 0,
      "gameHubFollowerCount": 15931
    }
  ],
  "@nextLink": null,
  "TotalCount": 26
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get Xbox Live analytics data](get-xbox-live-analytics.md)
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live health data](get-xbox-live-health-data.md)
* [Get Xbox Live club data](get-xbox-live-club-data.md)
* [Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)