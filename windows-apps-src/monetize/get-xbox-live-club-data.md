---
description: Use this method in the Microsoft Store analytics API to get Xbox Live club data.
title: Get Xbox Live club data
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox Live analytics, clubs
ms.localizationpriority: medium
---
# Get Xbox Live club data

Use this method in the Microsoft Store analytics API to get club data for your [Xbox Live-enabled game](/gaming/xbox-live/index.md). This information is also available in the [Xbox analytics report](../publish/xbox-analytics-report.md) in Partner Center.

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
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you want to retrieve Xbox Live club data.  |  Yes  |
| metricType | string | A string that specifies the type of Xbox Live analytics data to retrieve. For this method, specify the value **communitymanagerclub**.  |  Yes  |
| startDate | date | The start date in the date range of club data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of club data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |


### Request example

The following example demonstrates a request for getting club data for your Xbox Live-enabled game. Replace the *applicationId* value with the Store ID for your game.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics?applicationId=9NBLGGGZ5QDR&metrictype=communitymanagerclub&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array that contains one [ProductData](#productdata) object that contains data for clubs related to your game and one [XboxwideData](#xboxwidedata) object that contains club data for all Xbox Live customers. This data is included for comparison purposes with the data for your game.  |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query. |


### ProductData

This resource contains club data for your game.

| Value           | Type    | Description        |
|-----------------|---------|------|
| date            |  string |   The date for the club data.   |
|  applicationId               |    string     |  The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you retrieved club data.   |
|  clubsWithTitleActivity               |    int     |  The number of clubs that are socially engaged with your game.   |     
|  clubsExclusiveToGame               |   int      |  The number of clubs that are socially engaged exclusively with your game.   |     
|  clubFacts               |   array      |   Contains one or more [ClubFacts](#clubfacts) objects about each of the clubs that are socially engaged with your game.   |


### XboxwideData

This resource contains average club data across all Xbox Live customers.

| Value           | Type    | Description        |
|-----------------|---------|------|
| date            |  string |   The date for the club data.   |
|  applicationId  |    string     |   In the **XboxwideData** object, this string is always the value **XBOXWIDE**.  |
|  clubsWithTitleActivity               |   int     |  On average, the number of clubs with customers that are socially engaged with an Xbox Live-enabled game.    |     
|  clubsExclusiveToGame               |   int      |  On average, the number of clubs that are socially engaged exclusively with an Xbox Live-enabled game.   |     
|  clubFacts               |   object      |  Contains one [ClubFacts](#clubfacts) object. This object is meaningless in the context of the **XboxwideData** object and has default values.  |


### ClubFacts

In the **ProductData** object, this object contains data for a specific club that has activity related to your game. In the **XboxwideData** object, this object is meaningless and has default values.

| Value           | Type    | Description        |
|-----------------|---------|--------------------|
|  name            |  string  |   In the **ProductData** object, this is the name of the club. In the **XboxwideData** object, this is always the value **XBOXWIDE**.           |
|  memberCount               |    int     | In the **ProductData** object, this is the number of members in the club, excluding non-members who are just visiting the club. In the **XboxwideData** object, this is always 0.    |
|  titleSocialActionsCount               |    int     |  In the **ProductData** object, this is the number of social actions that members in the club have performed that are related to your game. In the **XboxwideData** object, this is always 0   |
|  isExclusiveToGame               |    Boolean     |  In the **ProductData** object, this indicates whether the current club is socially engaged exclusively with your game. In the **XboxwideData** object, this is always true.  |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "ProductData": [
        {
          "date": "2018-01-30",
          "applicationId": "9NBLGGGZ5QDR",
          "clubsWithTitleActivity": 3,
          "clubsExclusiveToGame": 1,
          "clubFacts": [
            {
              "name": "Club Contoso Racing",
              "memberCount": 1,
              "titleSocialActionsCount": 1,
              "isExclusiveToGame": false
            },
            {
              "name": "Club Contoso Sports",
              "memberCount": 12,
              "titleSocialActionsCount": 9,
              "isExclusiveToGame": false
            },
            {
              "name": "Club Contoso Maze",
              "memberCount": 4,
              "titleSocialActionsCount": 2,
              "isExclusiveToGame": false
            }
          ]
        }
      ],
      "XboxwideData": [
        {
          "date": "2018-01-30",
          "applicationId": "XBOXWIDE",
          "clubsWithTitleActivity": 25,
          "clubsExclusiveToGame": 3,
          "clubFacts": [
            {
              "name": "XBOXWIDE",
              "memberCount": 0,
              "titleSocialActionsCount": 0,
              "isExclusiveToGame": true
            }
          ]
        }
      ]
    }
  ],
  "@nextLink": "gameanalytics?applicationId=9NBLGGGZ5QDR&metricType=communitymanagerclub&top=1&skip=1&startDate=2018/01/04&endDate=2018/02/02&orderby=date desc",
  "TotalCount": 27
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get Xbox Live analytics data](get-xbox-live-analytics.md)
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live health data](get-xbox-live-health-data.md)
* [Get Xbox Live game hub data](get-xbox-live-game-hub-data.md)
* [Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)