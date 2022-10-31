---
description: Use this method in the Microsoft Store analytics API to get insights data for your app.
title: Get insights data
ms.date: 07/31/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, insights
ms.localizationpriority: medium
ms.custom: RS5
---
# Get insights data

Use this method in the Microsoft Store analytics API to get insights data related to acquisitions, health, and usage metrics for an app during a given date range and other optional filters. This information is also available in the [Insights report](/windows/apps/publish/insights-report) in Partner Center.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/insights``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve insights data. If you do not specify this parameter, the response body will contain insights data for all apps registered to your account.  |  No  |
| startDate | date | The start date in the date range of insights data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of insights data to retrieve. The default is the current date. |  No  |
| filter | string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. For example, *filter=dataType eq 'acquisition'*. <p/><p/>You can specify the following filter fields:<p/><ul><li><strong>acquisition</strong></li><li><strong>health</strong></li><li><strong>usage</strong></li></ul> | Yes   |

### Request example

The following example demonstrates a request for getting insights data. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/insights?applicationId=9NBLGGGZ5QDR&startDate=6/1/2018&endDate=6/15/2018&filter=dataType eq 'acquisition' or dataType eq 'health' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

### Response body

| Value      | Type   | Description                  |
|------------|--------|-------------------------------------------------------|
| Value      | array  | An array of objects that contain insights data for the app. For more information about the data in each object, see the [Insight values](#insight-values) section below.                                                                                                                      |
| TotalCount | int    | The total number of rows in the data result for the query.                 |


### Insight values

Elements in the *Value* array contain the following values.

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| applicationId       | string | The Store ID of the app for which you are retrieving insights data.     |
| insightDate                | string | The date on which we identified the change in a specific metric. This date represents the end of the week in which we detected a significant increase or decrease in a metric compared to the week before that. |
| dataType     | string | One of the following strings that specifies the general analytics area that this insight describes:<p/><ul><li><strong>acquisition</strong></li><li><strong>health</strong></li><li><strong>usage</strong></li></ul>   |
| insightDetail          | array | One or more [InsightDetail values](#insightdetail-values) that represent the details for current insight.    |


### InsightDetail values

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| FactName           | string | One of the following values that indicates the metric that the current insight or current dimension describes, based on the **dataType** value.<ul><li>For **health**, this value is always **HitCount**.</li><li>For **acquisition**, this value is always **AcquisitionQuantity**.</li><li>For **usage**, this value can be one of the following strings:<ul><li><strong>DailyActiveUsers</strong></li><li><strong>EngagementDurationMinutes</strong></li><li><strong>DailyActiveDevices</strong></li><li><strong>DailyNewUsers</strong></li><li><strong>DailySessionCount</strong></li></ul></ul>  |
| SubDimensions         | array |  One or more objects that describe a single metric for the insight.   |
| PercentChange            | string |  The percentage that the metric changed across your entire customer base.  |
| DimensionName           | string |  The name of the metric described in the current dimension. Examples include **EventType**, **Market**, **DeviceType**, **PackageVersion**, **AcquisitionType**, **AgeGroup** and **Gender**.   |
| DimensionValue              | string | The value of the metric that is described in the current dimension. For example, if **DimensionName** is **EventType**, **DimensionValue** might be **crash** or **hang**.   |
| FactValue     | string | The absolute value of the metric on the date the insight was detected.  |
| Direction | string |  The direction of the change (**Positive** or **Negative**).   |
| Date              | string |  The date on which we identified the change related to the current insight or the current dimension.   |

> [!NOTE]
> The Insights changes retrieved will be only for the last 30 days of acquisitions, health, and/or usage data.

### Request and Response example

The following code snippets demonstrates some example request and JSON response body for those request.

#### Sample Request 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/insights?applicationId=9NBLGGGZ5QDR&startDate=7/12/2022&endDate=7/29/2022&filter=dataType eq 'acquisition' or dataType eq 'health'
HTTP/1.1
Authorization: Bearer <your access token>
```
#### Sample Response

```json
{
    "Value": [
        {
            "id": "8cdb672c7893bd33a3dec48ededdc676602a1361f3209ab4f3e0982672fc198f",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-27T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-27 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "DeviceType",
                            "DimensionValue": "Tablet",
                            "Date": "2022-07-27 00:00:00",
                            "Direction": "Positive",
                            "PercentChange": 16.091954022988507,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 28.0
                        }
                    ]
                }
            ]
        },
        {
            "id": "6b8849ecb043c6e5ecdd1c20040c0a371e9ab176eff77f7312b0489e19379225",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-26T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-26 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "DeviceType",
                            "DimensionValue": "Tablet",
                            "Date": "2022-07-26 00:00:00",
                            "Direction": "Positive",
                            "PercentChange": 28.048780487804876,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 30.0
                        }
                    ]
                }
            ]
        },
        {
            "id": "93115e855fec507385bab17251f49c33bcbc1b62c603e5c7863e327ba94bf160",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-20T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-20 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "Market",
                            "DimensionValue": "GB",
                            "Date": "2022-07-20 00:00:00",
                            "Direction": "Negative",
                            "PercentChange": 3.3035714285714284,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 158.0
                        }
                    ]
                }
            ]
        },
        {
            "id": "4814abe710042a2798b453d8821909c246c389dde814e6678da7189211410604",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-24T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-24 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "Market",
                            "DimensionValue": "DO",
                            "Date": "2022-07-24 00:00:00",
                            "Direction": "Negative",
                            "PercentChange": 13.533834586466165,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 8.0
                        }
                    ]
                }
            ]
        },
        {
            "id": "7941070bb17904f3ef4e19de9659110cb52e18ecfb637ad724e2da749445a860",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-21T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-21 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "Market",
                            "DimensionValue": "AT",
                            "Date": "2022-07-21 00:00:00",
                            "Direction": "Positive",
                            "PercentChange": 0.0,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 17.0
                        }
                    ]
                },
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-21 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "Market",
                            "DimensionValue": "SE",
                            "Date": "2022-07-21 00:00:00",
                            "Direction": "Negative",
                            "PercentChange": 21.686746987951807,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 5.0
                        }
                    ]
                }
            ]
        },
        {
            "id": "55905f5458617b65669eb115cc28ebd7296841bd2ff3f8e2546ade1d5e93f68d",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-13T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "AcquisitionType",
                    "DimensionValue": "Free",
                    "Date": "2022-07-13 00:00:00",
                    "FactName": "AcquisitionQuantity",
                    "SubDimensions": [
                        {
                            "DimensionName": "Market",
                            "DimensionValue": "LK",
                            "Date": "2022-07-13 00:00:00",
                            "Direction": "Negative",
                            "PercentChange": 11.111111111111111,
                            "FactName": "AcquisitionQuantity",
                            "FactQuantity": 9.0
                        }
                    ]
                }
            ]
        },
        {
            "id": "9ce9317bf4d0f903de51a49d00a07cf4b4a8ac4457fd1e2886493bd38ceac0b7",
            "applicationId": "9NBLGGGZ5QDR",
            "insightDate": "2022-07-19T00:00:00",
            "dataType": "acquisition",
            "insightDetail": [
                {
                    "DimensionName": "Acquisition",
                    "DimensionValue": "Free",
                    "Date": "2022-07-19 00:00:00",
                    "Direction": "Negative",
                    "PercentChange": 12.188725230475788,
                    "FactName": "AcquisitionQuantity",
                    "FactQuantity": 2470,
                    "SubDimensions": []
                }
            ]
        }
    ],
    "TotalCount": 7
}
```

## Related topics

* [Insights report](/windows/apps/publish/insights-report)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
