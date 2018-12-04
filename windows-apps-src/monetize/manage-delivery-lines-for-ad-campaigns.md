---
ms.assetid: dc632a4c-ce48-400b-8e6e-1dddbd13afff
description: Use this method in the Microsoft Store promotions API to manage delivery lines for promotional ad campaigns.
title: Manage delivery lines
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store promotions API, ad campaigns
ms.localizationpriority: medium
---
# Manage delivery lines

Use these methods in the Microsoft Store promotions API to create one or more *delivery lines* to buy inventory and deliver your ads for a promotional ad campaign. For each delivery line, you can set targeting, set your bid price, and decide how much you want to spend by setting a budget and linking to creatives you want to use.

For more information about the relationship between delivery lines and ad campaigns, targeting profiles, and creatives, see [Run ad campaigns using Microsoft Store services](run-ad-campaigns-using-windows-store-services.md#call-the-windows-store-promotions-api).

>**Note**&nbsp;&nbsp;Before you can successfully create delivery lines for ad campaigns using this API, you must first [create one paid ad campaign using the **Ad campaigns** page in Partner Center](../publish/create-an-ad-campaign-for-your-app.md), and you must add at least one payment instrument on this page. After you do this, you will be able to successfully create billable delivery lines for ad campaigns using this API. Ad campaigns you create using the API will automatically bill the default payment instrument chosen on the **Ad campaigns** page in Partner Center.

## Prerequisites

To use these methods, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](run-ad-campaigns-using-windows-store-services.md#prerequisites) for the Microsoft Store promotions API.

  > [!NOTE]
  > As part of the prerequisites, be sure that you [create at least one paid ad campaign in Partner Center](../publish/create-an-ad-campaign-for-your-app.md) and that you add at least one payment instrument for the ad campaign in Partner Center. Delivery lines you create using this API will automatically bill the default payment instrument chosen on the **Ad campaigns** page in Partner Center.

* [Obtain an Azure AD access token](run-ad-campaigns-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for these methods. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

These methods have the following URIs.

| Method type | Request URI         |  Description  |
|--------|---------------------------|---------------|
| POST   | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/line``` |  Creates a new delivery line.  |
| PUT    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/line/{lineId}``` |  Edits the delivery line specified by *lineId*.  |
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/line/{lineId}``` |  Gets the delivery line specified by *lineId*.  |


### Header

| Header        | Type   | Description         |
|---------------|--------|---------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |
| Tracking ID   | GUID   | Optional. An ID that tracks the call flow.                                  |


### Request body

The POST and PUT methods require a JSON request body with the required fields of a [Delivery line](#line) object and any additional fields you want to set or change.


### Request examples

The following example demonstrates how to call the POST method to create a delivery line.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/promotion/line HTTP/1.1
Authorization: Bearer <your access token>

{
    "name": "Contoso App Campaign - Paid Line",
    "configuredStatus": "Active",
    "startDateTime": "2017-01-19T12:09:34Z",
    "endDateTime": "2017-01-31T23:59:59Z",
    "bidAmount": 0.4,
    "dailyBudget": 20,
    "targetProfileId": {
        "id": 310021746
    },
    "creatives": [
        {
            "id": 106851
        }
    ],
    "campaignId": 31043481,
    "minMinutesPerImp ": 1
}
```

The following example demonstrates how to call the GET method to retrieve a delivery line.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/line/31019990  HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

These methods return a JSON response body with a [Delivery line](#line) object that contains information about the delivery line that was created, updated, or retrieved. The following example demonstrates a response body for these methods.

```json
{
    "Data": {
        "id": 31043476,
        "name": "Contoso App Campaign - Paid Line",
        "configuredStatus": "Active",
        "effectiveStatus": "Active",
        "effectiveStatusReasons": [
            "{\"ValidationStatusReasons\":null}"
        ],
        "startDateTime": "2017-01-19T12:09:34Z",
        "endDateTime": "2017-01-31T23:59:59Z",
        "createdDateTime": "2017-01-17T10:28:34Z",
        "bidType": "CPM",
        "bidAmount": 0.4,
        "dailyBudget": 20,
        "targetProfileId": {
            "id": 310021746
        },
        "creatives": [
            {
                "id": 106126
            }
        ],
        "campaignId": 31043481,
        "minMinutesPerImp ": 1,
        "pacingType ": "SpendEvenly",
        "currencyId ": 732
    }
}

```


<span id="line"/>

## Delivery line object

The request and response bodies for these methods contain the following fields. This table shows which fields are read-only (meaning that they cannot be changed in the PUT method) and which fields are required in the request body for the POST or PUT methods.

| Field        | Type   |  Description      |  Read only  | Default  | Required for POST/PUT |   
|--------------|--------|---------------|------|-------------|------------|
|  id   |  integer   |  The ID of the delivery line.     |   Yes    |      |  No      |    
|  name   |  string   |   The name of the delivery line.    |    No   |      |  POST     |     
|  configuredStatus   |  string   |  One of the following values that specifies the status of the delivery line specified by the developer: <ul><li>**Active**</li><li>**Inactive**</li></ul>     |  No     |      |   POST    |       
|  effectiveStatus   |  string   |   One of the following values that specifies the effective status of the delivery line based on system validation: <ul><li>**Active**</li><li>**Inactive**</li><li>**Processing**</li><li>**Failed**</li></ul>    |    Yes   |      |  No      |      
|  effectiveStatusReasons   |  array   |  One or more of the following values that specify the reason for the effective status of the delivery line: <ul><li>**AdCreativesInactive**</li><li>**ValidationFailed**</li></ul>      |  Yes     |     |    No    |           
|  startDatetime   |  string   |  The start date and time for the delivery line, in ISO 8601 format. This value cannot be changed if it is already in the past.     |    No   |      |    POST, PUT     |
|  endDatetime   |  string   |  The end date and time for the delivery line, in ISO 8601 format. This value cannot be changed if it is already in the past.     |   No    |      |  POST, PUT     |
|  createdDatetime   |  string   |  The date and time the delivery line was created, in ISO 8601 format.      |    Yes   |      |  No      |
|  bidType   |  string   |  A value that specifies the bidding type of the delivery line. Currently, the only supported value is **CPM**.      |    No   |  CPM    |  No     |
|  bidAmount   |  decimal   |  The bid amount to be used for bidding any ad request.      |    No   |  The average CPM value based on target markets (this value is revised periodically).    |    No    |  
|  dailyBudget   |  decimal   |  The daily budget for the delivery line. Either *dailyBudget* or *lifetimeBudget* must be set.      |    No   |      |   POST, PUT (if *lifetimeBudget* is not set)       |
|  lifetimeBudget   |  decimal   |   The lifetime budget for the delivery line. Either lifetimeBudget* or *dailyBudget* must be set.      |    No   |      |   POST, PUT  (if *dailyBudget* is not set)    |
|  targetingProfileId   |  object   |  On object that identifies the [targeting profile](manage-targeting-profiles-for-ad-campaigns.md#targeting-profile) that describes the users, geographies and inventory types that you want to target for this delivery line. This object consists of a single *id* field that specifies the ID of the targeting profile.     |    No   |      |  No      |  
|  creatives   |  array   |  One or more objects that represent the [creatives](manage-creatives-for-ad-campaigns.md#creative) that are associated with the delivery line. Each object in this field consists of a single *id* field that specifies the ID of a creative.      |    No   |      |   No     |  
|  campaignId   |  integer   |  The ID of the parent ad campaign.      |    No   |      |   No     |  
|  minMinutesPerImp   |  integer   |  Specifies the minimum time interval (in minutes) between two impressions shown to the same user from this delivery line.      |    No   |  4000    |  No      |  
|  pacingType   |  string   |  One of the following values that specify the pacing type: <ul><li>**SpendEvenly**</li><li>**SpendAsFastAsPossible**</li></ul>      |    No   |  SpendEvenly    |  No      |
|  currencyId   |  integer   |  The ID of the currency of the campaign.      |    Yes   |  The currency of the developer account (you do not need to specify this field in POST or PUT calls)    |   No     |      |


## Related topics

* [Run ad campaigns using Microsoft Store Services](run-ad-campaigns-using-windows-store-services.md)
* [Manage ad campaigns](manage-ad-campaigns.md)
* [Manage targeting profiles for ad campaigns](manage-targeting-profiles-for-ad-campaigns.md)
* [Manage creatives for ad campaigns](manage-creatives-for-ad-campaigns.md)
* [Get ad campaign performance data](get-ad-campaign-performance-data.md)
