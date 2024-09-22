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

Use this method in the Microsoft Store analytics API to get aggregate usage data (not including Xbox multiplayer) in JSON format for an application during a given date range (last 90 days only) and other optional filters. This information is also available in the [Usage report](/windows/apps/publish/usage-report) in Partner Center.

Telemetry for Anaheim based PWA apps is collected at Device level. So metrics related to users will not be available for such products. Please refer to the below Usage values section for more details on the validity of fields.

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
| orderby       | string | A statement that orders the result data values. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li>**date**</li><li>**applicationId**</li><li>**applicationName**</li><li>**market**</li><li>**packageVersion**</li><li>**deviceType**</li><li>**subscriptionName**</li><li>**monthlySessionCount**</li><li>**engagementDurationMinutes**</li><li>**monthlyActiveUsers**</li><li>**monthlyActiveDevices**</li><li>**monthlyNewUsers**</li><li>**averageDailyActiveUsers**</li><li>**averageDailyActiveDevices**</li><li>**monthlyNewDevices**</li></ul><p>The <em>order</em> parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No        |
| groupby       | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields from the response body:<ul><li>**applicationName**</li><li>**subscriptionName**</li><li>**deviceType**</li><li>**packageVersion**</li><li>**market**</li><li>**date**</li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li>**applicationId**</li><li>**subscriptionName**</li><li>**monthlySessionCount**</li><li>**engagementDurationMinutes**</li><li>**monthlyActiveUsers**</li><li>**monthlyActiveDevices**</li><li>**monthlyNewUsers**</li><li>**averageDailyActiveUsers**</li><li>**averageDailyActiveDevices**</li><li>**monthlyNewDevices**</li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=ageGroup,market&amp;aggregationLevel=week</em></p> |  No        |


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

| Value                     | Type    | Description                                                                                 | UWP Availability| Anaheim based PWA Availability| Xbox Availability|
|---------------------------|---------|---------------------------------------------------------------------------------------------|---------|---------|---------|
| date                      | string  | The first date in the date range for the usage data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range.                          |Yes|Yes|Yes    |
| applicationId             | string  | The Store ID of the app for which you are retrieving usage data.                            |Yes|Yes|Yes    |
| applicationName           | string  | The display name of the app.                                                                |Yes|Yes|Yes    |
| market                    | string  | The ISO 3166 country code of the market where the customer used your app.                   |Yes|Yes|Yes    |
| packageVersion            | string  | The version of the package where usage occurred.                                            |Yes|Yes|No |
| deviceType                | string  | One of the following strings that specifies the type of device where usage occurred:<ul><li>**PC**</li><li>**Phone**</li><li>**Console-Xbox One**</li><li>**Console-Xbox Series X**</li><li>**Tablet**</li><li>**IoT**</li><li>**Server**</li><li>**Holographic**</li><li>**Unknown**</li></ul>                                                                                                                           |Yes|Yes|Yes  |
| subscriptionName          | string  | Indicates if usage was through Xbox Game Pass.                                              |Yes|No|No  |
| monthlySessionCount       | long    | The number of user sessions during that month.                                              |Yes|Yes|Yes    |
| engagementDurationMinutes | double  | The minutes where users are actively using your app measured by a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity.                               |Yes|Yes|Yes   |
| monthlyActiveUsers        | long    | The number of customers using the app that month.                                           |Yes|No|Yes |
| monthlyActiveDevices      | long    | The number of devices running your app for a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity.                                                        |Yes|Yes|Yes   |
| monthlyNewUsers           | long    | The number of customers who used your app for the first time that month.                    |Yes|No|Yes |
| averageDailyActiveUsers   | double  | The average number of customers using the app on a daily basis.                             |Yes|No|Yes |
| averageDailyActiveDevices | double  | The average number of devices used to interact with your app by all users on a daily basis. |Yes|Yes|Yes    |
| monthlyNewDevices | long  | The number of devices which used the app for the first time in that month. |No|Yes|No |

> [!NOTE]
> If a field is not valid for a specific product, then the value to those fields will be sent as 0 or null in the response. Computation of new users and new devices is done with the rolling window of 3 years, i.e., if a user/device doesn’t use the application for 3 years and above, they will be treated as new user/device after 3 years.

### Request and Response example

The following code snippets demonstrates some example request and JSON response body for those request.

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagemonthly?applicationId=9NBLGGGZ5QDR
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
            "engagementDurationMinutes": 2828568.3,
            "monthlyActiveUsers": 165249,
            "monthlyActiveDevices": 165753,
            "monthlyNewUsers": 95787,
            "monthlySessionCount": 314263,
            "averageDailyActiveUsers": 192.506111111111,
            "averageDailyActiveDevices": 191.199722222222,
            "monthlyNewDevices": 0
        }
    ],
    "TotalCount": 1
}
```

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/usagemonthly?applicationId=9NBLGGGZ5QDR&startDate=2022-06-01&endDate=2022-07-01&groupby=applicationName,subscriptionName,deviceType,packageVersion,market,date&top=10&skip=0
HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "1.0.0.4957",
            "market": "JP",
            "engagementDurationMinutes": 0.3,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 1,
            "averageDailyActiveUsers": 0.03,
            "averageDailyActiveDevices": 0.03,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "1.0.0.4957",
            "market": "MX",
            "engagementDurationMinutes": 2.7,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 1,
            "averageDailyActiveUsers": 0.03,
            "averageDailyActiveDevices": 0.03,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "1.0.0.4957",
            "market": "Unknown",
            "engagementDurationMinutes": 0.1,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 1,
            "averageDailyActiveUsers": 0.03,
            "averageDailyActiveDevices": 0.03,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "1.0.0.4957",
            "market": "US",
            "engagementDurationMinutes": 0.2,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 1,
            "averageDailyActiveUsers": 0.03,
            "averageDailyActiveDevices": 0.03,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AE",
            "engagementDurationMinutes": 3.2,
            "monthlyActiveUsers": 2,
            "monthlyActiveDevices": 2,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 3,
            "averageDailyActiveUsers": 0.07,
            "averageDailyActiveDevices": 0.07,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AO",
            "engagementDurationMinutes": 1.2,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 1,
            "averageDailyActiveUsers": 0.03,
            "averageDailyActiveDevices": 0.03,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AR",
            "engagementDurationMinutes": 574.7,
            "monthlyActiveUsers": 39,
            "monthlyActiveDevices": 39,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 53,
            "averageDailyActiveUsers": 1.43,
            "averageDailyActiveDevices": 1.43,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AT",
            "engagementDurationMinutes": 5.3,
            "monthlyActiveUsers": 4,
            "monthlyActiveDevices": 4,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 9,
            "averageDailyActiveUsers": 0.17,
            "averageDailyActiveDevices": 0.17,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AU",
            "engagementDurationMinutes": 434.7,
            "monthlyActiveUsers": 22,
            "monthlyActiveDevices": 22,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 36,
            "averageDailyActiveUsers": 0.9,
            "averageDailyActiveDevices": 0.9,
            "monthlyNewDevices": 0
        },
        {
            "date": "2022-06-01",
            "applicationId": "9NBLGGGZ5QDR",
            "applicationName": "Contoso Demo",
            "subscriptionName": "null",
            "deviceType": "PC",
            "packageVersion": "2.5.2.34894",
            "market": "AZ",
            "engagementDurationMinutes": 13.5,
            "monthlyActiveUsers": 1,
            "monthlyActiveDevices": 1,
            "monthlyNewUsers": 0,
            "monthlySessionCount": 1,
            "averageDailyActiveUsers": 0.03,
            "averageDailyActiveDevices": 0.03,
            "monthlyNewDevices": 0
        }
    ],
    "TotalCount": 10
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get daily app ussage](get-app-usage-daily.md)
* [Get app acquisitions](get-app-acquisitions.md)
* [Get add-on acquisitions](get-in-app-acquisitions.md)
* [Get error reporting data](get-error-reporting-data.md)
