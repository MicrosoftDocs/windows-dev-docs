---
description: Use this method in the Microsoft Store analytics API to get acquisition data for an add-on subscription during a given date range and other optional filters.
title: Get subscription add-on acquisitions
ms.date: 01/25/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, subscriptions
---
# Get subscription add-on acquisitions

Use this method in the Microsoft Store analytics API to get aggregate acquisition data for add-on subscriptions for your app during a given date range and other optional filters.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                                |
|--------|----------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/subscriptions``` |


### Request header

| Header        | Type   | Description          |
|---------------|--------|--------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to retrieve subscription add-on acquisition data. |  Yes  |
| subscriptionProductId  | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the subscription add-on for which you want to retrieve acquisition data. If you do not specify this value, this method returns acquisition data for all subscription add-ons for the specified app.  | No  |
| startDate | date | The start date in the date range of subscription add-on acquisition data to retrieve. The default is the current date. |  No  |
| endDate | date | The end date in the date range of subscription add-on acquisition data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 100. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=100 and skip=0 retrieves the first 100 rows of data, top=100 and skip=100 retrieves the next 100 rows of data, and so on. |  No  |
| filter | string  | One or more statements that filter the response body. Each statement can use the **eq** or **ne** operators, and statements can be combined using **and** or **or**. You can specify the following strings in the filter statements (these correspond to [values in the response body](#subscription-acquisition-values)): <ul><li><strong>date</strong></li><li><strong>subscriptionProductName</strong></li><li><strong>applicationName</strong></li><li><strong>skuId</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li></ul><p>Here is an example *filter* parameter: <em>filter=date eq '2017-07-08'</em>.</p> | No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: <strong>day</strong>, <strong>week</strong>, or <strong>month</strong>. If unspecified, the default is <strong>day</strong>. | No |
| orderby | string | A statement that orders the result data values for each subscription add-on acquisition. The syntax is <em>orderby=field [order],field [order],...</em>. The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>subscriptionProductName</strong></li><li><strong>applicationName</strong></li><li><strong>skuId</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li><strong>date</strong></li><li><strong>subscriptionProductName</strong></li><li><strong>applicationName</strong></li><li><strong>skuId</strong></li><li><strong>market</strong></li><li><strong>deviceType</strong></li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>groupby=market&amp;aggregationLevel=week</em></p> |  No  |


### Request example

The following examples demonstrates how to get subscription add-on acquisition data. Replace the *applicationId* value with the appropriate Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/subscriptions?applicationId=9NBLGGGZ5QDR&startDate=2017-07-07&endDate=2017-07-08 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description         |
|------------|--------|------------------|
| Value      | array  | An array of objects that contain aggregate subscription add-on acquisition data. For more information about the data in each object, see the [subscription acquisition values](#subscription-acquisition-values) section below.             |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 100 but there are more than 100 rows of subscription add-on acquisition data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.       |


<span id="subscription-acquisition-values" />

### Subscription acquisition values

Elements in the *Value* array contain the following values.

| Value               | Type    | Description        |
|---------------------|---------|---------------------|
| date                | string  | The first date in the date range for the acquisition data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| subscriptionProductId      | string  | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the subscription add-on for which you are retrieving acquisition data.    |
| subscriptionProductName    | string  | The display name of the subscription add-on.         |
| applicationId       | string  | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you are retrieving subscription add-on acquisition data.   |
| applicationName     | string  | The display name of the app.     |
| skuId     | string  | The ID of the [SKU](in-app-purchases-and-trials.md#products-skus) of the subscription add-on for which you are retrieving acquisition data.     |
| deviceType          | string  |  One of the following strings that specifies the type of device that completed the acquisition:<ul><li><strong>PC</strong></li><li><strong>Phone</strong></li><li><strong>Console-Xbox One</strong></li><li><strong>Console-Xbox Series X</strong></li><li><strong>IoT</strong></li><li><strong>Holographic</strong></li><li><strong>Unknown</strong></li></ul>       |
| market           | string  | The ISO 3166 country code of the market where the acquisition occurred.     |
| currencyCode         | string  | The currency code in ISO 4217 format for gross sales before taxes.       |
| grossSalesBeforeTax           | integer  | The gross sales in the local currency specified by the *currencyCode* value.     |
| totalActiveCount             | integer  | The number of total active subscriptions during the specified time period. This is equivalent to the sum of the *goodStandingActiveCount*, *pendingGraceActiveCount*, *graceActiveCount*, and *lockedActiveCount* values.  |
| totalChurnCount              | integer  | The total count of subscriptions that were deactivated during the specified time period. This is equivalent to the sum of the *billingChurnCount*, *nonRenewalChurnCount*, *refundChurnCount*, *chargebackChurnCount*, *earlyChurnCount*, and *otherChurnCount* values.   |
| newCount            | integer  | The number of new subscription acquisitions during the specified time period, including trials.    |
| renewCount     | integer  | The number of subscription renewals during the specified time period, including user-initiated renewals and automatic renewals.        |
| goodStandingActiveCount | integer | The number of subscriptions that were active during the specified time period and where the expiration date is >= the *endDate* value for the query.    |
| pendingGraceActiveCount | integer | The number of subscriptions that were active during the specified time period but had a billing failure, and where the subscription expiration date is >= the *endDate* value for the query.     |
| graceActiveCount | integer | The number of subscriptions that were active during the specified time period but had a billing failure, and where:<ul><li>The subscription expiration date is < the *endDate* value for the query.</li><li>The end of the grace period is >= the *endDate* value.</li></ul>        |
| lockedActiveCount | integer | The number of subscriptions that were in *dunning* (that is, the subscription is nearing expiration and Microsoft is trying to acquire funds to automatically renew the subscription) during the specified time period, and where:<ul><li>The subscription expiration date is < the *endDate* value for the query.</li><li>The end of the grace period is <= the *endDate* value.</li></ul>        |
| billingChurnCount | integer | The number of subscriptions that were deactivated during the specified time period because of a failure to process a billing charge and where the subscriptions were previously in dunning.        |
| nonRenewalChurnCount | integer | The number of subscriptions that were deactivated during the specified time period because they were not renewed.        |
| refundChurnCount | integer | The number of subscriptions that were deactivated during the specified time period because they were refunded.        |
| chargebackChurnCount | integer | The number of subscriptions that were deactivated during the specified time period because of a chargeback.        |
| earlyChurnCount | integer | The number of subscriptions that were deactivated during the specified time period while they were in good standing.        |
| otherChurnCount | integer | The number of subscriptions that were deactivated during the specified time period for other reasons.        |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2017-07-08",
      "subscriptionProductId": "9KDLGHH6R365",
      "subscriptionProductName": "Contoso App Subscription with One Month Free Trial",
      "applicationId": "9NBLGGH4R315",
      "applicationName": "Contoso App",
      "skuId": "0020",
      "market": "Unknown",
      "deviceType": "PC",
      "currencyCode": "USD",
      "grossSalesBeforeTax": 0.0,
      "totalActiveCount": 1,
      "totalChurnCount": 0,
      "newCount": 0,
      "renewCount": 0,
      "goodStandingActiveCount": 1,
      "pendingGraceActiveCount": 0,
      "graceActiveCount": 0,
      "lockedActiveCount": 0,
      "billingChurnCount": 0,
      "nonRenewalChurnCount": 0,
      "refundChurnCount": 0,
      "chargebackChurnCount": 0,
      "earlyChurnCount": 0,
      "otherChurnCount": 0
    },
    {
      "date": "2017-07-08",
      "subscriptionProductId": "9JJFDHG4R478",
      "subscriptionProductName": "Contoso App Monthly Subscription",
      "applicationId": "9NBLGGH4R315",
      "applicationName": "Contoso App",
      "skuId": "0020",
      "market": "US",
      "deviceType": "PC",
      "currencyCode": "USD",
      "grossSalesBeforeTax": 0.0,
      "totalActiveCount": 1,
      "totalChurnCount": 0,
      "newCount": 0,
      "renewCount": 0,
      "goodStandingActiveCount": 1,
      "pendingGraceActiveCount": 0,
      "graceActiveCount": 0,
      "lockedActiveCount": 0,
      "billingChurnCount": 0,
      "nonRenewalChurnCount": 0,
      "refundChurnCount": 0,
      "chargebackChurnCount": 0,
      "earlyChurnCount": 0,
      "otherChurnCount": 0
    }
  ],
  "@nextLink": null,
  "TotalCount": 2
}
```

## Related topics

* [Add-on acquisitions report](../publish/add-on-acquisitions-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)

 

 
