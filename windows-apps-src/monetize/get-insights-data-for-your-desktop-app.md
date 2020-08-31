---
description: Use this method in the Microsoft Store analytics API to get insights data for your desktop application.
title: Get insights data for your desktop application
ms.date: 07/31/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, insights
ms.localizationpriority: medium
ms.custom: RS5
---
# Get insights data for your desktop application

Use this method in the Microsoft Store analytics API to get insights data related to health metrics for a desktop application that you have added to the [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program). This data is also available in the [Health report](/windows/desktop/appxpkg/windows-desktop-application-program#health-report) for desktop applications in Partner Center.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/insights``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the desktop application for which you want to get insights data. To get the product ID of a desktop application, open any [analytics report for your desktop application in Partner Center](/windows/desktop/appxpkg/windows-desktop-application-program) (such as the **Health report**) and retrieve the product ID from the URL. If you do not specify this parameter, the response body will contain insights data for all apps registered to your account.  |  No  |
| startDate | date | The start date in the date range of insights data to retrieve. The default is 30 days before the current date. |  No  |
| endDate | date | The end date in the date range of insights data to retrieve. The default is the current date. |  No  |
| filter | string  | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the *filter* parameter. For example, *filter=dataType eq 'acquisition'*. <p/><p/>Currently this method only supports the filter **health**.  | No   |

### Request example

The following example demonstrates a request for getting insights data. Replace the *applicationId* value with the appropriate value for your desktop application.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/insights?applicationId=10238467886765136388&startDate=6/1/2018&endDate=6/15/2018&filter=dataType eq 'health' HTTP/1.1
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
| applicationId       | string | The product ID of the desktop application for which you retrieved insights data.     |
| insightDate                | string | The date on which we identified the change in a specific metric. This date represents the end of the week in which we detected a significant increase or decrease in a metric compared to the week before that. |
| dataType     | string | A string that specifies the general analytics area that this insight informs. Currently, this method only supports **health**.    |
| insightDetail          | array | One or more [InsightDetail values](#insightdetail-values) that represent the details for current insight.    |


### InsightDetail values

| Value               | Type   | Description                           |
|---------------------|--------|-------------------------------------------|
| FactName           | string | A string that indicates the metric that the current insight or current dimension describes. Currently, this method only supports the value **HitCount**.  |
| SubDimensions         | array |  One or more objects that describe a single metric for the insight.   |
| PercentChange            | string |  The percentage that the metric changed across your entire customer base.  |
| DimensionName           | string |  The name of the metric described in the current dimension. Examples include **EventType**, **Market**, **DeviceType**, and **PackageVersion**.   |
| DimensionValue              | string | The value of the metric that is described in the current dimension. For example, if **DimensionName** is **EventType**, **DimensionValue** might be **crash** or **hang**.   |
| FactValue     | string | The absolute value of the metric on the date the insight was detected.  |
| Direction | string |  The direction of the change (**Positive** or **Negative**).   |
| Date              | string |  The date on which we identified the change related to the current insight or the current dimension.   |

### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "applicationId": "9NBLGGGZ5QDR",
      "insightDate": "2018-06-03T00:00:00",
      "dataType": "health",
      "insightDetail": [
        {
          "FactName": "HitCount",
          "SubDimensions": [
            {
              "FactName:": "HitCount",
              "PercentChange": "21",
              "DimensionValue:": "DE",
              "FactValue": "109",
              "Direction": "Positive",
              "Date": "6/3/2018 12:00:00 AM",
              "DimensionName": "Market"
            }
          ],
          "DimensionValue": "crash",
          "Date": "6/3/2018 12:00:00 AM",
          "DimensionName": "EventType"
        },
        {
          "FactName": "HitCount",
          "SubDimensions": [
            {
              "FactName:": "HitCount",
              "PercentChange": "71",
              "DimensionValue:": "JP",
              "FactValue": "112",
              "Direction": "Positive",
              "Date": "6/3/2018 12:00:00 AM",
              "DimensionName": "Market"
            }
          ],
          "DimensionValue": "hang",
          "Date": "6/3/2018 12:00:00 AM",
          "DimensionName": "EventType"
        },
      ],
      "insightId": "9CY0F3VBT1AS942AFQaeyO0k2zUKfyOhrOHc0036Iwc="
    }
  ],
  "@nextLink": null,
  "TotalCount": 2
}
```

## Related topics

* [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program)
* [Health report](/windows/desktop/appxpkg/windows-desktop-application-program#health-report)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)