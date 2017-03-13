---
author: mcleanbyron
ms.assetid:
description: Use this method in the Windows Store purchase API to get the subscriptions that a given user has entitlements to use.
title: Get subscriptions for a user
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Windows Store purchase API, subscriptions
---

# Get subscriptions for a user

Use this method in the Windows Store purchase API to get the subscription add-ons that a given user has entitlements to use.

> [!NOTE]
> This method can only be used by developer accounts that have been provisioned by Microsoft to be able to create subscription add-ons for Universal Windows Platform (UWP) apps. Subscription add-ons are currently not available to most developer accounts.

## Prerequisites

To use this method, you will need:

* An Azure AD access token that was created with the `https://onestore.microsoft.com` audience URI.
* A Windows Store ID key that represents the identity of the user whose subscriptions you want to get.

For more information, see [Manage product entitlements from a service](view-and-grant-products-from-a-service.md).

## Request


### Request syntax

| Method | Request URI                                            |
|--------|--------------------------------------------------------|
| POST   | ```https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/query``` |

<span/> 

### Request header

| Header         | Type   | Description      |
|----------------|--------|-------------------|
| Authorization  | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;.                           |
| Host           | string | Must be set to the value **purchase.mp.microsoft.com**.                                            |
| Content-Length | number | The length of the request body.                                                                       |
| Content-Type   | string | Specifies the request and response type. Currently, the only supported value is **application/json**. |

<span/>

### Request body

| Parameter      | Type   | Description     | Required |
|----------------|--------|-----------------|----------|
| b2bKey         | string | The [Windows Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user whose subscriptions you want to get.  | Yes      |
| continuationToken |  string     |  If the user has entitlements to multiple subscriptions, the response body returns a continuation token when the page limit is reached. Provide that continuation token here in subsequent calls to retrieve remaining products.    | No      |
| pageSize       | string | The maximum number of subscriptions to return in one response. The default is 25.     |  No      |

<span/>

### Request example

The following example demonstrates how to use this method to get the subscription add-ons that a given user has entitlements to use. Replace the *b2bKey* value with the [Windows Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user whose subscriptions you want to get.

```json
POST https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/query HTTP/1.1
Authorization: Bearer <your access token>
Content-Type: application/json
Host: https://purchase.mp.microsoft.com

{
  "b2bKey":  "eyJ0eXAiOiJ..."
}
```


## Response

This method returns a JSON response body that contains a collection of data objects that describe the subscription add-ons that the user has entitlements to use. The following example demonstrates the response body for a user who has an entitlement for one subscription.

```json
{
  "items": [
    {
      "autoRenew":true,
      "beneficiary":"pub:gFVuEBiZHPXonkYvtdOi+tLE2h4g2Ss0ZId0RQOwzDg=",
      "expirationTime":"2017-06-11T03:07:49.2552941+00:00",
      "id":"mdr:1055518331358128:bc0cb6960acd4515a0e1d638192d77b7:77d5ebee-0310-4d23-b204-83e8613baaac",
      "lastModified":"2017-01-08T21:07:51.1459644+00:00",
      "market":"US",
      "productId":"9NBLGGH52Q8X",
      "skuId":"0024",
      "startTime":"2017-01-10T21:07:49.2552941+00:00",
      "recurrenceState":"Active"
    }
  ]
}
```


### Response body

The response body contains the following data.

| Value        | Type   | Description            |
|---------------|--------|---------------------|
| items | array | An array of objects that contain data about each subscription add-on that the specified user has an entitlement to use. For more information about the data in each object, see the following table.  |

Each object in the *items* array contains the following values.

| Value        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------|
| autoRenew | Boolean |  Indicates whether the subscription is configured to automatically renew at the end of the current subscription period.   |
| beneficiary | string |  The ID of the beneficiary of the entitlement that is associated with this subscription.   |
| expirationTime | string | The date and time the subscription will expire, in ISO 8601 format. This field is only available when the subscription is in certain states. The expiration time usually indicates when the current state expires. For example, for an active subscription, the expiration date indicates when the next automatic renewal will occur.    |
| id | string |  The ID of the subscription. Use this value to indicate which subscription you want to modify when you call the [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md) method.    |
| isTrial | Boolean |  Indicates whether the subscription is a trial.     |
| lastModified | string |  The date and time the subscription was last modified, in ISO 8601 format.      |
| market | string | The country code (in two-letter ISO 3166-1 alpha-2 format) in which the user acquired the subscription.      |
| productId | string |  The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [product](in-app-purchases-and-trials.md#products-skus-and-availabilities) that represents the subscription add-on in the Windows Store catalog. An example Store ID for a product is 9NBLGGH42CFD.     |
| skuId | string |  The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [SKU](in-app-purchases-and-trials.md#products-skus-and-availabilities) that represents the subscription add-on the Windows Store catalog. An example Store ID for a SKU is 0010.    |
| startTime | string |  The start date and time for the subscription, in ISO 8601 format.     |
| recurrenceState | string  |  One of the following values:<ul><li>**None**:&nbsp;&nbsp;This indicates a perpetual subscription.</li><li>**Active**:&nbsp;&nbsp;The subscription is active.</li><li>**Inactive**:&nbsp;&nbsp;The subscription is inactive; the user turned off the automatic renew option for the subscription or the last subscription cycle has ended.</li><li>**Canceled**:&nbsp;&nbsp;The subscription has been canceled. This state is also used for refunds.</li><li>**InDunning**:&nbsp;&nbsp;The subscription is in *dunning* (that is, the subscription is nearing expiration, and Microsoft is trying to acquire funds to automatically renew the subscription).</li><li>**Failed**:&nbsp;&nbsp;This is the terminal state. After a subscription enters this state, the user must repurchase the subscription to activate it again. Any subscription can enter this state when the dunning period expires and there is no way to recover this subscription, or when the developer turns off the automatic renew option and the subscription can no longer be renewed.</li></ul>     |
| cancellationDate | string   |  The date and time the user's subscription was cancelled, in ISO 8601 format.     |


## Related topics

* [Manage product entitlements from a service](view-and-grant-products-from-a-service.md)
* [Change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md)
* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Renew a Windows Store ID key](renew-a-windows-store-id-key.md)
