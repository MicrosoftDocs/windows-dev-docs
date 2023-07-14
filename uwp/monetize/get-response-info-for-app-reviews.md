---
title: Get response info for reviews
description: Use this method in the Microsoft Store reviews API to determine whether you can respond to a particular review, or whether you can respond to any review for a given app.
ms.assetid: fb6bb856-7a1b-4312-a602-f500646a3119
ms.date: 07/14/2023
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store reviews API, response info
ms.localizationpriority: medium
---

# Get response info for reviews

> [!IMPORTANT]
> The *Microsoft Store reviews API*, as documented in this topic, is currently not in a working state. Instead of using the APIs, you can achieve the same task(s) by [using Partner Center](/windows/apps/publish/respond-to-customer-reviews).

If you want to programmatically respond to a customer review of your app, you can use this method in the Microsoft Store reviews API to first determine whether you have permission to respond to the review. You cannot respond to reviews submitted by customers who have chosen not to receive review responses. After you confirm that you can respond to the review, you can then use the [submit responses to app reviews](submit-responses-to-app-reviews.md) method to programmatically respond to it.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](respond-to-reviews-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](respond-to-reviews-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID of the review you want to check to determine whether you can respond to it. Review IDs are available in the response data of the [get app reviews](get-app-reviews.md) method in the Microsoft Store analytics API and in the [offline download](/windows/apps/publish/download-analytic-reports) of the [Reviews report](/windows/apps/publish/reviews-report).

## Request

### Request syntax

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/reviews/{reviewId}/apps/{applicationId}/responses/info``` |

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |

### Request parameters

| Parameter        | Type   | Description                                     |  Required  |
|---------------|--------|--------------------------------------------------|--------------|
| applicationId | string | The Store ID of the app that contains the review for which you want to determine whether you can respond to. The Store ID is available on the [App identity page](/windows/apps/publish/view-app-identity-details) in Partner Center. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| reviewId | string | The ID of the review you want to respond to (this is a GUID). Review IDs are available in the response data of the [get app reviews](get-app-reviews.md) method in the Microsoft Store analytics API and in the [offline download](/windows/apps/publish/download-analytic-reports) of the [Reviews report](/windows/apps/publish/reviews-report). <br/>If you omit this parameter, the response body for this method will indicate whether you have permissions to respond to any reviews for the specified app. |  No  |

### Request example

The following examples how to use this method to determine whether you can respond to a given review.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/reviews/6be543ff-1c9c-4534-aced-af8b4fbe0316/apps/9WZDNCRFJ3Q8/responses/info HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

### Response body

| Value      | Type   | Description    |  
|------------|--------|-----------------------|
| CanRespond      | Boolean  | The value **true** indicates that you can respond to the specified review, or that you have permissions to respond to any review for the specified app. Otherwise, this value is **false**.       |
| DefaultSupportEmail  | string |  Your app's [support email address](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-msix#support-contact-info) as specified in your app's Store listing. If you did not specify a support email address, this field is empty.    |
 
### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "CanRespond": true,
  "DefaultSupportEmail": "support@contoso.com"
}
```

## Related topics

* [Submit responses to reviews using the Microsoft Store analytics API](submit-responses-to-app-reviews.md)
* [Respond to customer reviews using Partner Center](/windows/apps/publish/respond-to-customer-reviews)
* [Respond to reviews using Microsoft Store services](respond-to-reviews-using-windows-store-services.md)
* [Get app reviews](get-app-reviews.md)
