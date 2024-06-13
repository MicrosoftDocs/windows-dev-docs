---
description: Use this method in the Microsoft Store analytics API to get acquisition funnel data for an application during a given date range and other optional filters.
title: Get app acquisition funnel data
ms.date: 08/04/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, acquisition, funnel
ms.localizationpriority: medium
---
# Get app acquisition funnel data

Use this method in the Microsoft Store analytics API to get acquisition funnel data for an application during a given date range and other optional filters. This information is also available in the [Acquisitions report](/windows/apps/publish/acquisitions-report#acquisition-funnel) in Partner Center.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/funnel``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve acquisition funnel data. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| startDate | date | The start date in the date range of acquisition funnel data to retrieve. The default is the current date. |  No  |
| endDate | date | The end date in the date range of acquisition funnel data to retrieve. The default is the current date. |  No  |
| filter | string  | One or more statements that filter the rows in the response. For more information, see the [filter fields](#filter-fields) section below. | No   |

 
### Filter fields

The *filter* parameter of the request contains one or more statements that filter the rows in the response. Each statement contains a field and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**.

The following filter fields are supported. String values must be surrounded by single quotes in the *filter* parameter.

| Fields        |  Description        |
|---------------|-----------------|
| campaignId | The ID string for a [custom app promotion campaign](/windows/apps/publish/create-a-custom-app-promotion-campaign) that is associated with the acquisition. |
| market | A string that contains the ISO 3166 country code of the market where the acquisition occurred. |
| deviceType | One of the following strings that specifies the device type on which the acquisition occurred:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console-Xbox One</strong></li><li><strong>Console-Xbox Series X</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul> |
| ageGroup | One of the following strings that specifies the age group of the user who completed the acquisition:<ul><li><strong>0 – 17</strong></li><li><strong>18 – 24</strong></li><li><strong>25 – 34</strong></li><li><strong>35 – 49</strong></li><li><strong>50 or over</strong></li><li><strong>Unknown</strong></li></ul> |
| gender | One of the following strings that specifies the gender of the user who completed the acquisition:<ul><li><strong>M</strong></li><li><strong>F</strong></li><li><strong>Unknown</strong></li></ul> |


### Request example

The following example demonstrates several requests for getting acquisition funnel data for an app. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/funnel?applicationId=9NBLGGGZ5QDR&startDate=1/1/2017&endDate=2/1/2017  HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/funnel?applicationId=9NBLGGGZ5QDR&startDate=8/1/2016&endDate=8/31/2016&filter=market eq 'US' and gender eq 'm'  HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain acquisition funnel data for the app. For more information about the data in each object, see the [funnel values](#funnel-values) section below.                  |
| TotalCount | int    | The total number of objects in the *Value* array.        |


### Funnel values

Objects in the *Value* array contain the following values.

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| MetricType                | string | One of the following strings that specifies the [type of funnel data](/windows/apps/publish/acquisitions-report#acquisition-funnel) that is included in this object:<ul><li><strong>PageView</strong></li><li><strong>Acquisition</strong></li><li><strong>Install</strong></li><li><strong>Usage</strong></li></ul> |
| UserCount       | string | The number of users who performed the funnel step specified by the *MetricType* value.             |


### Request and Response example

The following code snippets demonstrates some example request and JSON response body for those request.

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/funnel?applicationId=9NBLGGGZ5QDR
HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "MetricType": "PageView",
            "UserCount": 6214
        },
        {
            "MetricType": "Acquisition",
            "UserCount": 1502
        },
        {
            "MetricType": "Usage",
            "UserCount": 606
        },
        {
            "MetricType": "Install",
            "UserCount": 977
        }
    ],
    "TotalCount": 4
}
```

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/funnel?applicationId=9NBLGGGZ5QDR&startDate=12/19/2021&endDate=12/21/2021&filter=market eq 'US' and gender eq 'm'
HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "MetricType": "PageView",
            "UserCount": 10
        },
        {
            "MetricType": "Acquisition",
            "UserCount": 8
        },
        {
            "MetricType": "Usage",
            "UserCount": 5
        },
        {
            "MetricType": "Install",
            "UserCount": 3
        }
    ],
    "TotalCount": 4
}
```

## Related topics

* [Acquisitions report](/windows/apps/publish/acquisitions-report)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get app acquisitions](get-app-acquisitions.md)
