---
ms.assetid: C09F4B7C-6324-4973-980A-A60035792EFC
description: Use this method in the Microsoft Store submission API to create a new add-on submission for an app that is registered to Partner Center.
title: Create an add-on submission
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, create add-on submission, in-app product, IAP
ms.localizationpriority: medium
---

# Create an add-on submission

Use this method in the Microsoft Store submission API to create a new add-on (also known as in-app product or IAP) submission for an app that is registered to your Partner Center account. After you successfully create a new submission by using this method, [update the submission](update-an-add-on-submission.md) to make any necessary changes to the submission data, and then [commit the submission](commit-an-add-on-submission.md) for ingestion and publishing.

For more information about how this method fits into the process of creating an add-on submission by using the Microsoft Store submission API, see [Manage add-on submissions](manage-add-on-submissions.md).

> [!NOTE]
> This method creates a submission for an existing add-on. To create an add-on, use the [Create an add-on](create-an-add-on.md) method.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Create an add-on for one of your apps. You can do this in Partner Center, or you can do this by using the [Create an add-on](create-an-add-on.md) method.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| POST    | `https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{inAppProductId}/submissions` |

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |

### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| inAppProductId | string | Required. The Store ID of the add-on for which you want to create a submission. The Store ID is available in Partner Center, and it is included in the response data for requests to [Create an add-on](create-an-add-on.md) or [get add-on details](get-all-add-ons.md).  |

### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to create a new submission for an add-on.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/9NBLGGH4TNMP/submissions HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body for a successful call to this method. The response body contains information about the new submission. For more details about the values in the response body, see [add-on submission resource](manage-add-on-submissions.md#add-on-submission-object).

```json
{
  "id": "1152921504621243680",
  "contentType": "EMagazine",
  "keywords": [
    "books"
  ],
  "lifetime": "FiveDays",
  "listings": {
    "en": {
      "description": "English add-on description",
      "icon": {
        "fileName": "add-on-en-us-listing2.png",
        "fileStatus": "Uploaded"
      },
      "title": "Add-on Title (English)"
    },
    "ru": {
      "description": "Russian add-on description",
      "icon": {
        "fileName": "add-on-ru-listing.png",
        "fileStatus": "Uploaded"
      },
      "title": "Add-on Title (Russian)"
    }
  },
  "pricing": {
    "marketSpecificPricings": {
      "RU": "Tier3",
      "US": "Tier4",
    },
    "sales": [
      {
         "name": "Sale1",
         "basePriceId": "Free",
         "startDate": "2016-05-21T18:40:11.7369008Z",
         "endDate": "2016-05-22T18:40:11.7369008Z",
         "marketSpecificPricings": {
            "RU": "NotAvailable"
         }
      }
    ],
    "priceId": "Free",
    "isAdvancedPricingModel": true
  },
  "targetPublishDate": "2016-03-15T05:10:58.047Z",
  "targetPublishMode": "Immediate",
  "tag": "SampleTag",
  "visibility": "Public",
  "status": "PendingCommit",
  "statusDetails": {
    "errors": [
      {
        "code": "None",
        "details": "string"
      }
    ],
    "warnings": [
      {
        "code": "ListingOptOutWarning",
        "details": "You have removed listing language(s): []"
      }
    ],
    "certificationReports": [
      {
      }
    ]
  },
  "fileUploadUrl": "https://productingestionbin1.blob.core.windows.net/ingestion/26920f66-b592-4439-9a9d-fb0f014902ec?sv=2014-02-14&sr=b&sig=usAN0kNFNnYE2tGQBI%2BARQWejX1Guiz7hdFtRhyK%2Bog%3D&se=2016-06-17T20:45:51Z&sp=rwl",
  "friendlyName": "Submission 2"
}
```

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 400  | The submission could not be created because the request is invalid. |
| 409  | The submission could not be created because of the current state of the app, or the app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |   

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage add-on submissions](manage-add-on-submissions.md)
* [Get an add-on submission](get-an-add-on-submission.md)
* [Commit an add-on submission](commit-an-add-on-submission.md)
* [Update an add-on submission](update-an-add-on-submission.md)
* [Delete an add-on submission](delete-an-add-on-submission.md)
* [Get the status of an add-on submission](get-status-for-an-add-on-submission.md)
