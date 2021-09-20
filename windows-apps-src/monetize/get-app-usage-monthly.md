---
ms.assetid: 4E4CB1E3-D213-4324-91E4-7D4A0EA19C53
description: Use this method in the Microsoft Store analytics API to get monthly app usage data for a given date range and other optional filters.
title: Get monthly app usage
ms.date: 08/15/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, usage
ms.localizationpriority: medium
---
# Get monthly app usage

Use this method in the Microsoft Store analytics API to get aggregate usage data (not including Xbox multiplayer) in JSON format for an application during a given date range (last 90 days only) and other optional filters. This information is also available in the [Usage report](../publish/usage-report.md) in Partner Center.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

### Request syntax

| Method | Request URI                                                                 |
|--------|-----------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagemonthly``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter     | Type   |  Description                                                                                                    |  Required  |
|---------------|--------|-----------------------------------------------------------------------------------------------------------------|------------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve review data. |  Yes       |
| startDate     | date   | The start date in the date range of review data to retrieve. The default is the current date.                   |  No        |
| endDate       | date   | The end date in the date range of review data to retrieve. The default is the current date.                     |  No        |
| top           | int    | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data.                          |  No        |
| skip          | int    | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on.                         |  No        |  
| filter        |string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the eq or ne operators, and statements can be combined using and or or. String values must be surrounded by single quotes in the filter parameter. You can specify the following fields from the response body: <ul><li>**market**</li><li>**deviceType**</li><li>**packageVersion**</li></ul>                                                                                                                                              | No         |  
| orderby       | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li>**date**</li><li>**applicationId**</li><li>**applicationName**</li><li>**market**</li><li>**packageVersion**</li><li>**deviceType**</li><li>**subscriptionName**</li><li>**monthlySessionCount**</li><li>**engagementDurationMinutes**</li><li>**monthlyActiveUsers**</li><li>**monthlyActiveDevices**</li><li>**monthlyNewUsers**</li><li>**averageDailyActiveUsers**</li><li>**averageDailyActiveDevices**</li></ul><p>The <em>order</em> parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No        |
| groupby       | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields from the response body:<ul><li>**applicationName**</li><li>**subscriptionName**</li><li>**deviceType**</li><li>**packageVersion**</li><li>**market**</li><li>**date**</li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li>**applicationId**</li><li>**subscriptionName**</li><li>**monthlySessionCount**</li><li>**engagementDurationMinutes**</li><li>**monthlyActiveUsers**</li><li>**monthlyActiveDevices**</li><li>**monthlyNewUsers**</li><li>**averageDailyActiveUsers**</li><li>**averageDailyActiveDevices**</li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=ageGroup,market&amp;aggregationLevel=week</em></p> |  No        |


### Request example

The following example demonstrates a request for getting monthly app usage data. Replace the *applicationId* value with the Store ID for your app.

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagemonthly?applicationId=XXXXXXXXXXXX&startDate=2018-06-01&endDate=2018-07-01 HTTP/1.1  
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description                                                                                                                         |
|------------|--------|-------------------------------------------------------------------------------------------------------------------------------------|
| Value      | array  | An array of objects that contain aggregate usage data. For more information about the data in each object, see the following table. |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of reviews data for the query.                 |
| TotalCount | int    | The total number of rows in the data result for the query.                                                                          |

 
### Usage values

Elements in the *Value* array contain the following values.

| Value                     | Type    | Description                                                                                 |
|---------------------------|---------|---------------------------------------------------------------------------------------------|
| date                      | string  | The first date in the date range for the usage data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range.                          |
| applicationId             | string  | The Store ID of the app for which you are retrieving usage data.                            |
| applicationName           | string  | The display name of the app.                                                                |
| market                    | string  | The ISO 3166 country code of the market where the customer used your app.                   |
| packageVersion            | string  | The version of the package where usage occurred.                                            |
| deviceType                | string  | One of the following strings that specifies the type of device where usage occurred:<ul><li>**PC**</li><li>**Phone**</li><li>**Console-Xbox One**</li><li>**Console-Xbox Series X**</li><li>**Tablet**</li><li>**IoT**</li><li>**Server**</li><li>**Holographic**</li><li>**Unknown**</li></ul>                                                                                                                           |
| subscriptionName          | string  | Indicates if usage was through Xbox Game Pass.                                              |
| monthlySessionCount       | long    | The number of user sessions during that month.                                              |
| engagementDurationMinutes | double  | The minutes where users are actively using your app measured by a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity.                               |
| monthlyActiveUsers        | long    | The number of customers using the app that month.                                           |
| monthlyActiveDevices      | long    | The number of devices running your app for a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity.                                                        |
| monthlyNewUsers           | long    | The number of customers who used your app for the first time that month.                    |
| averageDailyActiveUsers   | double  | The average number of customers using the app on a daily basis.                             |
| averageDailyActiveDevices | double  | The average number of devices used to interact with your app by all users on a daily basis. |


### Response example

The following example demonstrates an example JSON response body for this request.

```http
{
  "Value": [
    {
      "date": "2018-06-01",
      "applicationId": "XXXXXXXXXXXX",
      "applicationName": "My App",
      "market": "All",
      "packageVersion": "All",
      "deviceType": "All",
      "subscriptionName": "All",
      "monthlySessionCount": 582973,
      "engagementDurationMinutes": 16941418.7,
      "monthlyActiveUsers": 139604,
      "monthlyActiveDevices": 132296,
      "monthlyNewUsers": 88127,
      "averageDailyActiveUsers": 9099.23,
      "averageDailyActiveDevices": 8999.0
    },
    {
      "date": "2018-07-01",
      "applicationId": "XXXXXXXXXXXX",
      "applicationName": "My App",
      "market": "All",
      "packageVersion": "All",
      "deviceType": "All",
      "subscriptionName": "All",
      "monthlySessionCount": 681460,
      "engagementDurationMinutes": 21656645.3,
      "monthlyActiveUsers": 130481,
      "monthlyActiveDevices": 123583,
      "monthlyNewUsers": 78465,
      "averageDailyActiveUsers": 8257.55,
      "averageDailyActiveDevices": 8170.58
    }
  ],
  "@nextLink": null,
  "TotalCount": 2
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get daily app ussage](get-app-usage-daily.md)
* [Get app acquisitions](get-app-acquisitions.md)
* [Get add-on acquisitions](get-in-app-acquisitions.md)
* [Get error reporting data](get-error-reporting-data.md)
