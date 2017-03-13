---
author: mcleanbyron
ms.assetid:
description: Use this method in the Windows Store purchase API to change the billing state of a subscription for a user.
title: Change the billing state of a subscription for a user
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Windows Store purchase API, subscriptions
---

# Change the billing state of a subscription for a user

Use this method in the Windows Store purchase API to change the billing state of a subscription add-on for a given user. You can cancel, extend, or refund a subscription, or toggle the setting to automatically renew the subscription at the end of the subscription period.

> [!NOTE]
> This method can only be used by developer accounts that have been provisioned by Microsoft to be able to create subscription add-ons for Universal Windows Platform (UWP) apps. Subscription add-ons are currently not available to most developer accounts.

## Prerequisites

To use this method, you will need:

* An Azure AD access token that was created with the `https://onestore.microsoft.com` audience URI.
* A Windows Store ID key that represents the identity of the user who has an entitlement to the subscription you want to change.

For more information, see [Manage product entitlements from a service](view-and-grant-products-from-a-service.md).

## Request


### Request syntax

| Method | Request URI                                            |
|--------|--------------------------------------------------------|
| POST   | ```https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/{recurrenceId}/change``` |

<span/> 

### Request header

| Header         | Type   | Description   |
|----------------|--------|-------------|
| Authorization  | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;.                           |
| Host           | string | Must be set to the value **purchase.mp.microsoft.com**.                                            |
| Content-Length | number | The length of the request body.                                                                       |
| Content-Type   | string | Specifies the request and response type. Currently, the only supported value is **application/json**. |

<span/>

### Request parameters

| Name         | Type  | Description   |  Required  |
|----------------|--------|-------------|-----------|
| recurrenceId | string | The ID of the subscription you want to change. To get this ID, call the [get subscriptions for a user](get-subscriptions-for-a-user.md) method, identify the response body entry that represents the subscription add-on you want to change, and use the value of the **Id** field for the entry.     | Yes      |

<span/>

### Request body

| Field      | Type   | Description   | Required |
|----------------|--------|---------------|----------|
| b2bKey         | string | The [Windows Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user whose subscription you want to change.     | Yes      |
| changeType     | string |  One of the following strings that identifies the type of change you want to make:<ul><li>**Cancel**: Cancels the subscription.</li><li>**Extend**: Extends the subscription. If you specify this value, you must also include the *extensionTimeInDays* parameter.</li><li>**Refund**: Refunds the subscription to the customer.</li><li>**ToggleAutoRenew**: If the subscription is configured to automatically renew at the end of each period, this value disables automatic renewal. Otherwise, this value turns on automatic subscription renewal. </li></ul>   | Yes      |
| extensionTimeInDays  | string  | If the *changeType* parameter has the value **Extend**, this parameter specifies the number of days to extend the subscription. |  Yes, if *changeType* has the value **Extend**; otherwise, no.  ||

<span/>

### Request example

The following example demonstrates how to use this method to extend the subscription period by 5 days. Replace the *b2bKey* value with the [Windows Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user whose subscription you want to change.

```json
POST https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/query HTTP/1.1
Authorization: Bearer <your access token>
Content-Type: application/json
Host: https://purchase.mp.microsoft.com

{
  "b2bKey":  "eyJ0eXAiOiJ...",
  "changeType": "Extend",
  "extensionTimeInDays": "5"
}
```


## Response

This method returns a JSON response body that contains information about the subscription add-on that was modified, including any fields that were modified. The following example demonstrates a response body for this method.

```json
{
  "items": [
    {
      "autoRenew":true,
      "beneficiary":"pub:gFVuEBiZHPXonkYvtdOi+tLE2h4g2Ss0ZId0RQOwzDg=",
      "expirationTime":"2017-06-16T03:07:49.2552941+00:00",
      "id":"mdr:1055518331358128:bc0cb6960acd4515a0e1d638192d77b7:77d5ebee-0310-4d23-b204-83e8613baaac",
      "lastModified":"2017-01-10T21:08:13.1459644+00:00",
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

| Value        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------|
| autoRenew | Boolean |  Indicates whether the subscription is configured to automatically renew at the end of the current subscription period.   |
| beneficiary | string |  The ID of the beneficiary of the entitlement that is associated with this subscription.   |
| expirationTime | string | The date and time the subscription will expire, in ISO 8601 format. This field is only available when the subscription is in certain states. The expiration time usually indicates when the current state expires. For example, for an active subscription, the expiration date indicates when the next automatic renewal will occur.    |
| id | string |  The ID of the subscription. Use this value to indicate which subscription you want to modify when you call the [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md) method.    |
| isTrial | Boolean |  Indicates whether the subscription is a trial.     |
| lastModified | string |  The date and time the subscription was last modified, in ISO 8601 format.      |
| market | string | The country code (in two-letter ISO 3166-1 alpha-2 format) in which the user acquired the subscription.      |
| productId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [product](in-app-purchases-and-trials.md#products-skus-and-availabilities) that represents the subscription add-on in the Windows Store catalog. An example Store ID for a product is 9NBLGGH42CFD.     |
| skuId | string |  The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [SKU](in-app-purchases-and-trials.md#products-skus-and-availabilities) that represents the subscription add-on the Windows Store catalog. An example Store ID for a SKU is 0010.    |
| startTime | string |  The start date and time for the subscription, in ISO 8601 format.     |
| recurrenceState | string  |  One of the following values:<ul><li>**None**:&nbsp;&nbsp;This indicates a perpetual subscription.</li><li>**Active**:&nbsp;&nbsp;The subscription is active.</li><li>**Inactive**:&nbsp;&nbsp;The subscription is inactive; the user turned off the automatic renew option for the subscription or the last subscription cycle has ended.</li><li>**Canceled**:&nbsp;&nbsp;The subscription has been canceled. This state is also used for refunds.</li><li>**InDunning**:&nbsp;&nbsp;The subscription is in *dunning* (that is, the subscription is nearing expiration, and Microsoft is trying to acquire funds to automatically renew the subscription).</li><li>**Failed**:&nbsp;&nbsp;This is the terminal state. After a subscription enters this state, the user must repurchase the subscription to activate it again. Any subscription can enter this state when the dunning period expires and there is no way to recover this subscription, or when the developer turns off the automatic renew option and the subscription can no longer be renewed.</li></ul>      |
| cancellationDate | string   |  The date and time the user's subscription was cancelled, in ISO 8601 format.     |


## Related topics


* [Manage product entitlements from a service](view-and-grant-products-from-a-service.md)
* [Get subscriptions for a user](get-subscriptions-for-a-user.md)
* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Renew a Windows Store ID key](renew-a-windows-store-id-key.md)
