---
ms.assetid: 7b07a6ca-4be1-497c-a901-0a2da3762555
description: Use this method in the Microsoft Store promotions API to create, edit, and get promotional ad campaigns.
title: Manage ad campaigns
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store promotions API, ad campaigns
ms.localizationpriority: medium
---
# Manage ad campaigns

Use these methods in the [Microsoft Store promotions API](run-ad-campaigns-using-windows-store-services.md) to create, edit and get promotional ad campaigns for your app. Each campaign you create using this method can be associated with only one app.

>**Note**&nbsp;&nbsp;You can also create and manage ad campaigns using Partner Center, and campaigns that you create programmatically can be accessed in Partner Center. For more information about managing ad campaigns in Partner Center, see [Create an ad campaign for your app](../publish/create-an-ad-campaign-for-your-app.md).

When you use these methods to create or update a campaign, you typically also call one or more of the following methods to manage the *delivery lines*, *targeting profiles*, and *creatives* that are associated with the campaign. For more information about the relationship between campaigns, delivery lines, targeting profiles, and creatives, see [Run ad campaigns using Microsoft Store services](run-ad-campaigns-using-windows-store-services.md#call-the-windows-store-promotions-api).

* [Manage delivery lines for ad campaigns](manage-delivery-lines-for-ad-campaigns.md)
* [Manage targeting profiles for ad campaigns](manage-targeting-profiles-for-ad-campaigns.md)
* [Manage creatives for ad campaigns](manage-creatives-for-ad-campaigns.md)

## Prerequisites

To use these methods, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](run-ad-campaigns-using-windows-store-services.md#prerequisites) for the Microsoft Store promotions API.

  >**Note**&nbsp;&nbsp;As part of the prerequisites, be sure that you [create at least one paid ad campaign in Partner Center](../publish/create-an-ad-campaign-for-your-app.md) and that you add at least one payment instrument for the ad campaign in Partner Center. Delivery lines for ad campaigns you create using this API will automatically bill the default payment instrument chosen on the **Ad campaigns** page in Partner Center.

* [Obtain an Azure AD access token](run-ad-campaigns-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for these methods. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.


## Request

These methods have the following URIs.

| Method type | Request URI                                                      |  Description  |
|--------|------------------------------------------------------------------|---------------|
| POST   | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign``` |  Creates a new ad campaign.  |
| PUT    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign/{campaignId}``` |  Edits the ad campaign specified by *campaignId*.  |
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign/{campaignId}``` |  Gets the ad campaign specified by *campaignId*.  |
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign``` |  Queries for ad campaigns. See the [Parameters](#parameters) section for the supported query parameters.  |


### Header

| Header        | Type   | Description         |
|---------------|--------|---------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |
| Tracking ID   | GUID   | Optional. An ID that tracks the call flow.                                  |


<span id="parameters"/> 

### Parameters

The GET method to query for ad campaigns supports the following optional query parameters.

| Name        | Type   |  Description      |    
|-------------|--------|---------------|------|
| skip  |  int   | The number of rows to skip in the query. Use this parameter to page through data sets. For example, fetch=10 and skip=0 retrieves the first 10 rows of data, top=10 and skip=10 retrieves the next 10 rows of data, and so on.    |       
| fetch  |  int   | The number of rows of data to return in the request.    |       
| campaignSetSortColumn  |  string   | Orders the [Campaign](#campaign) objects in the response body by the specified field. The syntax is <em>CampaignSetSortColumn=field</em>, where the <em>field</em> parameter can be one of the following strings:</p><ul><li><strong>id</strong></li><li><strong>createdDateTime</strong></li></ul><p>The default is **createdDateTime**.     |     
| isDescending  |  Boolean   | Sorts the [Campaign](#campaign) objects in the response body in descending or ascending order.   |         
| storeProductId  |  string   | Use this value to return only the ad campaigns that are associated with the app with the specified [Store ID](in-app-purchases-and-trials.md#store-ids). An example Store ID for a product is 9nblggh42cfd.   |         
| label  |  string   | Use this value to return only the ad campaigns that include the specified *label* in the [Campaign](#campaign) object.    |       |    


### Request body

The POST and PUT methods require a JSON request body with the required fields of a [Campaign](#campaign) object and any additional fields you want to set or change.


### Request examples

The following example demonstrates how to call the POST method to create an ad campaign.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign HTTP/1.1
Authorization: Bearer <your access token>

{
    "name": "Contoso App Campaign",
    "storeProductId": "9nblggh42cfd",
    "configuredStatus": "Active",
    "objective": "DriveInstalls",
    "type": "Community"
}
```

The following example demonstrates how to call the GET method to retrieve a specific ad campaign.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign/31043481  HTTP/1.1
Authorization: Bearer <your access token>
```

The following example demonstrates how to call the GET method to query for a set of ad campaigns, sorted by the created date.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/campaign?storeProductId=9nblggh42cfd&fetch=100&skip=0&campaignSetSortColumn=createdDateTime HTTP/1.1
Authorization: Bearer <your access token>
```


## Response

These methods return a JSON response body with one or more [Campaign](#campaign) objects, depending on the method you called. The following example demonstrates a response body for the GET method for a specific campaign.

```json
{
    "Data": {
        "id": 31043481,
        "name": "Contoso App Campaign",
        "createdDate": "2017-01-17T10:12:15Z",
        "storeProductId": "9nblggh42cfd",
        "configuredStatus": "Active",
        "effectiveStatus": "Active",
        "effectiveStatusReasons": [
            "{\"ValidationStatusReasons\":null}"
        ],
        "labels": [],
        "objective": "DriveInstalls",
        "type": "Paid",
        "lines": [
            {
                "id": 31043476,
                "name": "Contoso App Campaign - Paid Line"
            }
        ]
    }
}
```


<span id="campaign"/>

## Campaign object

The request and response bodies for these methods contain the following fields. This table shows which fields are read-only (meaning that they cannot be changed in the PUT method) and which fields are required in the request body for the POST method.

| Field        | Type   |  Description      |  Read only  | Default  | Required for POST |  
|--------------|--------|---------------|------|-------------|------------|
|  id   |  integer   |  The ID of the ad campaign.     |   Yes    |      |  No     |       
|  name   |  string   |   The name of the ad campaign.    |    No   |      |  Yes     |       
|  configuredStatus   |  string   |  One of the following values that specifies the status of the ad campaign specified by the developer: <ul><li>**Active**</li><li>**Inactive**</li></ul>     |  No     |  Active    |   Yes    |       
|  effectiveStatus   |  string   |   One of the following values that specifies the effective status of the ad campaign based on system validation: <ul><li>**Active**</li><li>**Inactive**</li><li>**Processing**</li></ul>    |    Yes   |      |   No      |       
|  effectiveStatusReasons   |  array   |  One or more of the following values that specify the reason for the effective status of the ad campaign: <ul><li>**AdCreativesInactive**</li><li>**BillingFailed**</li><li>**AdLinesInactive**</li><li>**ValidationFailed**</li><li>**Failed**</li></ul>      |  Yes     |     |    No     |       
|  storeProductId   |  string   |  The [Store ID](in-app-purchases-and-trials.md#store-ids) for the app that this ad campaign is associated with. An example Store ID for a product is 9nblggh42cfd.     |   Yes    |      |  Yes     |       
|  labels   |  array   |   One or more strings that represent custom labels for the campaign. These labels be used for searching and tagging campaigns.    |   No    |  null    |    No     |       
|  type   | string    |  One of the following values that specifies the campaign type: <ul><li>**Paid**</li><li>**House**</li><li>**Community**</li></ul>      |   Yes    |      |   Yes    |       
|  objective   |  string   |  One of the following values that specifies the objective of the campaign: <ul><li>**DriveInstall**</li><li>**DriveReengagement**</li><li>**DriveInAppPurchase**</li></ul>     |   No    |  DriveInstall    |   Yes    |       
|  lines   |  array   |   One or more objects that identify the [delivery lines](manage-delivery-lines-for-ad-campaigns.md#line) that are associated with the ad campaign. Each object in this field consists of an *id* and *name* field that specifies the ID and name of the delivery line.     |   No    |      |    No     |       
|  createdDate   |  string   |  The date and time the ad campaign was created, in ISO 8601 format.     |  Yes     |      |     No    |       |


## Related topics

* [Run ad campaigns using Microsoft Store Services](run-ad-campaigns-using-windows-store-services.md)
* [Manage delivery lines for ad campaigns](manage-delivery-lines-for-ad-campaigns.md)
* [Manage targeting profiles for ad campaigns](manage-targeting-profiles-for-ad-campaigns.md)
* [Manage creatives for ad campaigns](manage-creatives-for-ad-campaigns.md)
* [Get ad campaign performance data](get-ad-campaign-performance-data.md)
