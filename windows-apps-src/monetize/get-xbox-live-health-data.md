---
description: Use this method in the Microsoft Store analytics API to get Xbox Live health data.
title: Get Xbox Live health data
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox Live analytics, health, client errors
ms.localizationpriority: medium
---
# Get Xbox Live health data


Use this method in the Microsoft Store analytics API to get health data for your [Xbox Live-enabled game](/gaming/xbox-live/index.md). This information is also available in the [Xbox analytics report](../publish/xbox-analytics-report.md) in Partner Center.

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
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you want to retrieve Xbox Live health data.  |  Yes  |
| metricType | string | A string that specifies the type of Xbox Live analytics data to retrieve. For this method, specify the value **callingpattern**.  |  Yes  |
| startDate | date | The start date in the date range of health data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of health data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter | string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. You can specify the following fields from the response body:<p/><ul><li><strong>deviceType</strong></li><li><strong>packageVersion</strong></li><li><strong>sandboxId</strong></li></ul> | No   |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields from the response body:<p/><ul><li><strong>date</strong></li><li><strong>deviceType</strong></li><li><strong>packageVersion</strong></li><li><strong>sandboxId</strong></li></ul><p/>If you specify one or more *groupby* fields, any other *groupby* fields you do not specify will have the value **All** in the response body. |  No  |


### Request example

The following example demonstrates a request for getting health data for your Xbox Live-enabled game. Replace the *applicationId* value with the Store ID for your game.


```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics?applicationId=9NBLGGGZ5QDR&metrictype=callingpattern&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain health data. For more information about the data in each object, see the following table.                                                                                                                      |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.   |


Elements in the *Value* array contain the following values.

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| applicationId       | string | The Store ID of the game for which you are retrieving health data.     |
| date                | string | The first date in the date range for the health data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| deviceType          | string | One of the following strings that specifies the type of device on which your game was used:<p/><ul><li><strong>XboxOne</strong></li><li><strong>WindowsOneCore</strong> (this value indicates a PC)</li><li><strong>Unknown</strong></li></ul>  |
| sandboxId     | string |   The sandbox ID created for the game. This can be the value RETAIL or the ID for a private sandbox.   |
| packageVersion     | string |  The four-part package version for the game.  |
| callingPattern     | object |  A [callingPattern](#callingpattern) object that provides service responses, devices, and user data for each status code returned by each Xbox Live service used by your game for the specified date range.     |


### callingPattern

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| service      | string  |   The name of the Xbox Live service that the health data relates to.       |
| endpoint      | string  |   The endpoint of the Xbox Live service that the health data relates to.        |
| httpStatusCode      | string  |  The HTTP status code for this set of health data.<p/><p/>**Note**&nbsp;&nbsp;The status code **429E** indicates that the service call succeeded only because [fine grained rate limiting](/gaming/xbox-live/using-xbox-live/best-practices/fine-grained-rate-limiting.md) was exempt during the call. Fine grained rate limited could be enforced in the future if the service experiences high volume, and in that case the call would result in an [HTTP 429 status code](/gaming/xbox-live/using-xbox-live/best-practices/fine-grained-rate-limiting.md#http-429-response-object).         |
| serviceResponses      | number  | The number of service responses that returned the specified status code.         |
| uniqueDevices      | number  |  The number of unique devices that called the service and received the specified status code.       |
| uniqueUsers      | number  |   The number of unique users who received the specified status code.       |


### Response example

The following example demonstrates an example JSON response body for this request. The service names and endpoints shown in this example are not real, and are used for example purposes only.

```json
{
  "Value": [
    {
      "applicationId": "9NBLGGGZ5QDR",
      "date": "2018-01-30",
      "deviceType": "All",
      "sandboxId": "RETAIL",
      "packageVersion": "Unknown",
      "callingpattern": [
        {
          "service": "userstats",
          "endpoint": "UserStats.BatchReadHandler.POST",
          "httpStatusCode": "200",
          "serviceResponses": 160891,
          "uniqueDevices": 410,
          "uniqueUsers": 410
        },
        {
          "service": "userstats",
          "endpoint": "UserStats.BatchStatReadHandler.GET",
          "httpStatusCode": "200",
          "serviceResponses": 422,
          "uniqueDevices": 0,
          "uniqueUsers": 30
        }
      ],
    }
  ],
  "@nextLink": null,
  "TotalCount": 1
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get Xbox Live analytics data](get-xbox-live-analytics.md)
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live Game Hub data](get-xbox-live-game-hub-data.md)
* [Get Xbox Live club data](get-xbox-live-club-data.md)
* [Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)