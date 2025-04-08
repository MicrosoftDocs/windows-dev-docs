---
ms.assetid: 99DB5622-3700-4FB2-803B-DA447A1FD7B7
description: Use this method in the Microsoft Store analytics API to get daily app usage data for a given date range and other optional filters.
title: Get daily app usage
ms.date: 08/15/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, usage
ms.localizationpriority: medium
---
# Get daily app usage

Use this method in the Microsoft Store analytics API to get aggregate usage data (not including Xbox multiplayer) in JSON format for an application during a given date range (last 90 days only) and other optional filters. This information is also available in the [Usage report](/windows/apps/publish/usage-report) in Partner Center.

Telemetry for Anaheim based PWA apps is collected at Device level. So metrics related to users will not be available for such products. Please refer to the below Usage values section for more details on the validity of fields.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

### Request syntax

| Method | Request URI                                                               |
|--------|---------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagedaily``` |


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
| orderby       | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li>**date**</li><li>**applicationId**</li><li>**applicationName**</li><li>**market**</li><li>**packageVersion**</li><li>**deviceType**</li><li>**subscriptionName**</li><li>**dailySessionCount**</li><li>**engagementDurationMinutes**</li><li>**dailyActiveUsers**</li><li>**dailyActiveDevices**</li><li>**dailyNewUsers**</li><li>**monthlyActiveUsers**</li><li>**monthlyActiveDevices**</li><li>**monthlyNewUsers**</li><li>**dailyNewDevices**</li><li>**monthlyNewDevices**</li></ul><p>The <em>order</em> parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p>                                                                                                   |  No        |
| groupby       | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields from the response body: <ul><li>**applicationName**</li><li>**subscriptionName**</li><li>**deviceType**</li><li>**packageVersion**</li><li>**market**</li><li>**date**</li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li>**applicationId**</li><li>**subscriptionName**</li><li>**dailySessionCount**</li><li>**engagementDurationMinutes**</li><li>**dailyActiveUsers**</li><li>**dailyActiveDevices**</li><li>**dailyNewUsers**</li><li>**monthlyActiveUsers**</li><li>**monthlyActiveDevices**</li><li>**monthlyNewUsers**</li><li>**dailyNewDevices**</li><li>**monthlyNewDevices**</li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=ageGroup,market&amp;aggregationLevel=week</em></p>                                                                                                             |  No        |


### Request example

The following example demonstrates a request for getting daily app usage data. Replace the *applicationId* value with the Store ID for your app.

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagedaily?applicationId=XXXXXXXXXXXX&startDate=2018-08-10&endDate=2018-08-14 HTTP/1.1
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

| Value                     | Type    | Description    | UWP Availability| Anaheim based PWA Availability| Xbox Availability    |
|---------------------------|---------|---------------------------------------------------------------------------|---------|---------|---------|
| date                      | string  | The first date in the date range for the usage data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range.        |Yes|Yes|Yes  |
| applicationId             | string  | The Store ID of the app for which you are retrieving usage data.          |Yes|Yes|Yes  |
| applicationName           | string  | The display name of the app.                                              |Yes|Yes|Yes  |
| deviceType                | string  | One of the following strings that specifies the type of device where usage occurred:<ul><li>**PC**</li><li>**Phone**</li><li>**Console-Xbox One**</li><li>**Console-Xbox Series X**</li><li>**Tablet**</li><li>**IoT**</li><li>**Server**</li><li>**Holographic**</li><li>**Unknown**</li></ul>                                                                                                         |Yes|Yes|Yes    |
| packageVersion            | string  | The version of the package where usage occurred.                          |Yes|Yes|No   |
| market                    | string  | The ISO 3166 country code of the market where the customer used your app. |Yes|Yes|Yes  |
| subscriptionName          | string  | Indicates if usage was through Xbox Game Pass.                            |Yes|No|No    |
| dailySessionCount         | long    | The number of user sessions on that day.                                  |Yes|Yes|Yes  |
| engagementDurationMinutes | double  | The minutes where users are actively using your app measured by a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity.             |Yes|Yes|Yes |
| dailyActiveUsers          | long    | The number of customers using the app that day.                           |Yes|No|Yes   |
| dailyActiveDevices        | long    | The number of daily devices used to interact with your app by all users.  |Yes|Yes|Yes  |
| dailyNewUsers             | long    | The number of customers who used your app for the first time that day.    |Yes|No|Yes|
| monthlyActiveUsers        | long    | The number of customers using the app that month.                         |Yes|No|Yes   |
| monthlyActiveDevices      | long    | The number of devices running your app for a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity.                                      |Yes|Yes|Yes |
| monthlyNewUsers           | long    | The number of customers who used your app for the first time that month.  |Yes|No|Yes   |
| dailyNewDevices           | long    | The number of devices which used the app for the first time that day.  |No|Yes|No   |
| monthlyNewDevices         | long    | The number of devices which used the app for the first time in last 30 days.  |No|Yes|No    |

> [!NOTE]
> If a field is not valid for a specific product, then the value to those fields will be sent as 0 or null in the response. Computation of new users and new devices is done with the rolling window of 3 years, i.e., if a user/device doesn’t use the application for 3 years and above, they will be treated as new user/device after 3 years.

### Request and Response example

The following code snippets demonstrates some example request and JSON response body for those request.

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagedaily?applicationId=9NBLGGGZ5QDR
HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "deviceType": "All",
            "packageVersion": "All",
            "market": "All",
            "dailySessionCount": 6102,
            "engagementDurationMinutes": 75801.0,
            "dailyActiveUsers": 3866,
            "dailyActiveDevices": 3855,
            "dailyNewUsers": 2041,
            "monthlyActiveUsers": 85206,
            "monthlyActiveDevices": 88995,
            "monthlyNewUsers": 63184,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        }
    ],
    "TotalCount": 1
}
```

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagedaily?applicationId=9NBLGGGZ5QDR&startDate=06/19/2022&endDate=07/20/2022&top=10&skip=0&groupby=applicationName,subscriptionName,deviceType,packageVersion,market,date

HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "date": "2022-07-11",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "1.4.0.17837",
            "market": "BR",
            "dailySessionCount": 1,
            "engagementDurationMinutes": 35.2,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-07-04",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AD",
            "dailySessionCount": 1,
            "engagementDurationMinutes": 2.7,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-24",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AE",
            "dailySessionCount": 2,
            "engagementDurationMinutes": 2.9,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 2,
            "monthlyActiveDevices": 2,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-07-08",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AE",
            "dailySessionCount": 1,
            "engagementDurationMinutes": 0.5,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 2,
            "monthlyActiveDevices": 2,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-26",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AO",
            "dailySessionCount": 1,
            "engagementDurationMinutes": 1.2,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-19",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AR",
            "dailySessionCount": 1,
            "engagementDurationMinutes": 1.0,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 40,
            "monthlyActiveDevices": 40,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-20",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AR",
            "dailySessionCount": 2,
            "engagementDurationMinutes": 10.9,
            "dailyActiveUsers": 2,
            "dailyActiveDevices": 2,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 42,
            "monthlyActiveDevices": 42,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-21",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AR",
            "dailySessionCount": 6,
            "engagementDurationMinutes": 32.8,
            "dailyActiveUsers": 3,
            "dailyActiveDevices": 3,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 44,
            "monthlyActiveDevices": 43,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-22",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AR",
            "dailySessionCount": 1,
            "engagementDurationMinutes": 0.1,
            "dailyActiveUsers": 1,
            "dailyActiveDevices": 1,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 43,
            "monthlyActiveDevices": 42,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-23",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AR",
            "dailySessionCount": 2,
            "engagementDurationMinutes": 0.3,
            "dailyActiveUsers": 2,
            "dailyActiveDevices": 2,
            "dailyNewUsers": 0,
            "monthlyActiveUsers": 42,
            "monthlyActiveDevices": 42,
            "monthlyNewUsers": 0,
            "dailyNewDevices": 0,
            "monthlyNewDevices": 0
        }
    ],
    "@nextLink": "usagedaily?applicationId=9NBLGGGZ5QDR&startDate=2022/06/19&endDate=2022/07/21&groupby=applicationName,subscriptionName,deviceType,packageVersion,market,date&top=10&skip=10",
    "TotalCount": 765
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get monthly app ussage](get-app-usage-monthly.md)
* [Get app acquisitions](get-app-acquisitions.md)
* [Get add-on acquisitions](get-in-app-acquisitions.md)
* [Get error reporting data](get-error-reporting-data.md)
