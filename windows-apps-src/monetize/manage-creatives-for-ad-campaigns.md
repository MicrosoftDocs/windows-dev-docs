---
ms.assetid: c5246681-82c7-44df-87e1-a84a926e6496
description: Use this method in the Microsoft Store promotions API to manage creatives for promotional ad campaigns.
title: Manage creatives
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store promotions API, ad campaigns
ms.localizationpriority: medium
---
# Manage creatives

Use these methods in the Microsoft Store promotions API to upload your own custom creatives to use in promotional ad campaigns or get an existing creative. A creative may be associated with one or more delivery lines, even across ad campaigns, provided it always represents the same app.

For more information about the relationship between creatives and ad campaigns, delivery lines, and targeting profiles, see [Run ad campaigns using Microsoft Store services](run-ad-campaigns-using-windows-store-services.md#call-the-windows-store-promotions-api).

> [!NOTE]
> When using this API to upload your own creative, the maximum allowed size for your creative is 40 KB. If you submit a creative file larger than this, this API will not return an error, but the campaign will not successfully be created.

## Prerequisites

To use these methods, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](run-ad-campaigns-using-windows-store-services.md#prerequisites) for the Microsoft Store promotions API.
* [Obtain an Azure AD access token](run-ad-campaigns-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for these methods. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.


## Request

These methods have the following URIs.

| Method type | Request URI     |  Description  |
|--------|-----------------------------|---------------|
| POST   | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/creative``` |  Creates a new creative.  |
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/creative/{creativeId}``` |  Gets the creative specified by *creativeId*.  |

> [!NOTE]
> This API currently does not support a PUT method.


### Header

| Header        | Type   | Description         |
|---------------|--------|---------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |
| Tracking ID   | GUID   | Optional. An ID that tracks the call flow.                                  |


### Request body

The POST method requires a JSON request body with the required fields of a [Creative](#creative) object.


### Request examples

The following example demonstrates how to call the POST method to create a creative. In this example, the *content* value has been shortened for brevity.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/promotion/creative HTTP/1.1
Authorization: Bearer <your access token>

{
  "name": "Contoso App Campaign - Creative 1",
  "content": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAgGB...other base64 data shortened for brevity...",
  "height": 80,
  "width": 480,
  "imageAttributes":
  {
    "imageExtension": "PNG"
  }
}
```

The following example demonstrates how to call the GET method to retrieve a creative.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/creative/106851  HTTP/1.1
Authorization: Bearer <your access token>
```


## Response

These methods return a JSON response body with a [Creative](#creative) object that contains information about the creative that was created or retrieved. The following example demonstrates a response body for these methods. In this example, the *content* value has been shortened for brevity.

```json
{
    "Data": {
        "id": 106126,
        "name": "Contoso App Campaign - Creative 2",
        "content": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAgGB...other base64 data shortened for brevity...",
        "height": 50,
        "width": 300,
        "format": "Banner",
        "imageAttributes":
        {
          "imageExtension": "PNG"
        },
        "storeProductId": "9nblggh42cfd"
    }
}
```


<span id="creative"/>

## Creative object

The request and response bodies for these methods contain the following fields. This table shows which fields are read-only (meaning that they cannot be changed in the PUT method) and which fields are required in the request body for the POST method.

| Field        | Type   |  Description      |  Read only  | Default  |  Required for POST |  
|--------------|--------|---------------|------|-------------|------------|
|  id   |  integer   |  The ID of the creative.     |   Yes    |      |    No   |       
|  name   |  string   |   The name of the creative.    |    No   |      |  Yes     |       
|  content   |  string   |  The content of the creative image, in Base64-encoded format.<br/><br/>**Note**&nbsp;&nbsp;The maximum allowed size for your creative is 40 KB. If you submit a creative file larger than this, this API will not return an error, but the campaign will not successfully be created.     |  No     |      |   Yes    |       
|  height   |  integer   |   The height of the creative.    |    No    |      |   Yes    |       
|  width   |  integer   |  The width of the creative.     |  No    |     |    Yes   |       
|  landingUrl   |  string   |  If you are using a campaign tracking service such as AppsFlyer, Kochava, Tune, or Vungle to measure install analytics for your app, assign your tracking URL in this field when you call the POST method (if specified, this value must be a valid URI). If you are not using a campaign tracking service, omit this value when you call the POST method (in this case, this URL will be created automatically).   |  No    |     |   Yes    |
|  format   |  string   |   The ad format. Currently, the only supported value is **Banner**.    |   No    |  Banner   |  No     |       
|  imageAttributes   | [ImageAttributes](#image-attributes)    |   Provides attributes for the creative.     |   No    |      |   Yes    |       
|  storeProductId   |  string   |   The [Store ID](in-app-purchases-and-trials.md#store-ids) for the app that this ad campaign is associated with. An example Store ID for a product is 9nblggh42cfd.    |   No    |    |  No     |   |  


<span id="image-attributes"/>

## ImageAttributes object

| Field        | Type   |  Description      |  Read-only  | Default value  | Required for POST |  
|--------------|--------|---------------|------|-------------|------------|
|  imageExtension   |   string  |   One of the following values: **PNG** or **JPG**.    |    No   |      |   Yes    |       |


## Related topics

* [Run ad campaigns using Microsoft Store Services](run-ad-campaigns-using-windows-store-services.md)
* [Manage ad campaigns](manage-ad-campaigns.md)
* [Manage delivery lines for ad campaigns](manage-delivery-lines-for-ad-campaigns.md)
* [Manage targeting profiles for ad campaigns](manage-targeting-profiles-for-ad-campaigns.md)
* [Get ad campaign performance data](get-ad-campaign-performance-data.md)
