---
title: Submit responses to reviews
description: Use this method in the Microsoft Store reviews API to submit responses to reviews of your app.
ms.assetid: 038903d6-efab-4da6-96b5-046c7431e6e7
ms.date: 07/14/2023
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store reviews API, add-on acquisitions
ms.localizationpriority: medium
---

# Submit responses to reviews

> [!IMPORTANT]
> The *Microsoft Store reviews API*, as documented in this topic, is currently not in a working state. Instead of using the APIs, you can achieve the same task(s) by [using Partner Center](/windows/apps/publish/respond-to-customer-reviews).

Use this method in the Microsoft Store reviews API to programmatically respond to reviews of your app. When you call this method, you must specify the IDs of the reviews you want to respond to. Review IDs are available in the response data of the [get app reviews](get-app-reviews.md) method in the Microsoft Store analytics API and in the [offline download](/windows/apps/publish/download-analytic-reports) of the [Reviews report](/windows/apps/publish/reviews-report).

When a customer submits a review, they can choose not to receive responses to their review. If you try to respond to a review for which the customer chose not to receive responses, the response body of this method will indicate that the response attempt was unsuccessful. Before calling this method, you can optionally determine whether you are allowed to respond to a given review by using the [get response info for app reviews](get-response-info-for-app-reviews.md) method.

> [!NOTE]
> In addition to using this method to programmatically respond to reviews, you can alternatively respond to reviews [using Partner Center](/windows/apps/publish/respond-to-customer-reviews).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](respond-to-reviews-using-windows-store-services.md#prerequisites) for the Microsoft Store reviews API.
* [Obtain an Azure AD access token](respond-to-reviews-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the IDs of the reviews you want to respond to. Review IDs are available in the response data of the [get app reviews](get-app-reviews.md) method in the Microsoft Store analytics API and in the [offline download](/windows/apps/publish/download-analytic-reports) of the [Reviews report](/windows/apps/publish/reviews-report).

## Request

### Request syntax

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| POST    | ```https://manage.devcenter.microsoft.com/v1.0/my/reviews/responses``` |

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |

### Request parameters

This method has no request parameters.

### Request body

The request body has the following values.

| Value        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------|
| Responses | array | An array of objects that contain the response data you want to submit. For more information about the data in each object, see the following table. |

Each object in the *Responses* array contains the following values.

| Value        | Type   | Description           |  Required  |
|---------------|--------|-----------------------------|-----|
| ApplicationId | string |  The Store ID of the app with the review you want to respond to. The Store ID is available on the [App identity page](/windows/apps/publish/view-app-identity-details) of Partner Center. An example Store ID is 9WZDNCRFJ3Q8.   |  Yes  |
| ReviewId | string |  The ID of the review you want to respond to (this is a GUID). Review IDs are available in the response data of the [get app reviews](get-app-reviews.md) method in the Microsoft Store analytics API and in the [offline download](/windows/apps/publish/download-analytic-reports) of the [Reviews report](/windows/apps/publish/reviews-report).   |  Yes  |
| ResponseText | string | The response you want to submit. Your response must follow [these guidelines](/windows/apps/publish/respond-to-customer-reviews#guidelines-for-responses).   |  Yes  |
| SupportEmail | string | Your app's support email address, which the customer can use to contact you directly. This must be a valid email address.     |  Yes  |
| IsPublic | Boolean |  If you specify **true**, your response will be displayed in your app's Store listing, directly below the customer's review, and will be visible to all customers. If you specify **false** and the user hasn't opted out of receiving email responses, your response will be sent to the customer via email, and it will not be visible to other customers in your app's Store listing. If you specify **false** and the user has opted out of receiving email responses, an error will be returned.   |  Yes  |

### Request example

The following example demonstrates how to use this method to submit responses to several reviews.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/reviews/responses HTTP/1.1
Authorization: Bearer <your access token>
Content-Type: application/json
{
  "Responses": [
    {
      "ApplicationId": "9WZDNCRFJ3Q8",
      "ReviewId": "6be543ff-1c9c-4534-aced-af8b4fbe0316",
      "ResponseText": "Thank you for pointing out this bug. I fixed it and published an update, you should have the fix soon",
      "SupportEmail": "support@contoso.com",
      "IsPublic": true
    },
    {
      "ApplicationId": "9NBLGGH1RP08",
      "ReviewId": "80c9671a-96c2-4278-bcbc-be0ce5a32a7c",
      "ResponseText": "Thank you for submitting your review. Can you tell more about what you were doing in the app when it froze? Thanks very much for your help.",
      "SupportEmail": "support@contoso.com",
      "IsPublic": false
    }
  ]
}
```

## Response

### Response body

| Value        | Type   | Description            |
|---------------|--------|---------------------|
| Result | array | An array of objects that contain data about each response you submitted. For more information about the data in each object, see the following table.  |

Each object in the *Result* array contains the following values.

| Value        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------|
| ApplicationId | string |  The Store ID of the app with the review you responded to. An example Store ID is 9WZDNCRFJ3Q8.   |
| ReviewId | string |  The ID of the review you responded to. This is a GUID.   |
| Successful | string | The value **true** indicates that your response was sent successfully. The value **false** indicates that your response was unsuccessful.    |
| FailureReason | string | If **Successful** is **false**, this value contains a reason for the failure. If **Successful** is **true**, this value is empty.      |

### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Result": [
    {
      "ApplicationId": "9WZDNCRFJ3Q8",
      "ReviewId": "6be543ff-1c9c-4534-aced-af8b4fbe0316",
      "Successful": "true",
      "FailureReason": ""
    },
    {
      "ApplicationId": "9NBLGGH1RP08",
      "ReviewId": "80c9671a-96c2-4278-bcbc-be0ce5a32a7c",
      "Successful": "false",
      "FailureReason": "No Permission"
    }
  ]
}
```

## Related topics

* [Respond to customer reviews using Partner Center](/windows/apps/publish/respond-to-customer-reviews)
* [Respond to reviews using Microsoft Store services](respond-to-reviews-using-windows-store-services.md)
* [Get response info for app reviews](get-response-info-for-app-reviews.md)
* [Get app reviews](get-app-reviews.md)
