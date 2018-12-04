---
ms.assetid: F37C2CEC-9ED1-4F9E-883D-9FBB082504D4
description: Use this method in the Microsoft Store purchase API to change the billing state of a subscription for a user.
title: Change the billing state of a subscription for a user
ms.date: 08/01/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store purchase API, subscriptions
ms.localizationpriority: medium
---
# Change the billing state of a subscription for a user

Use this method in the Microsoft Store purchase API to change the billing state of a subscription add-on for a given user. You can cancel, extend, refund, or disable automatic renewal for a subscription.

> [!NOTE]
> This method can only be used by developer accounts that have been provisioned by Microsoft to be able to create subscription add-ons for Universal Windows Platform (UWP) apps. Subscription add-ons are currently not available to most developer accounts.

## Prerequisites

To use this method, you will need:

* An Azure AD access token that has the audience URI value `https://onestore.microsoft.com`.
* A Microsoft Store ID key that represents the identity of the user who has an entitlement to the subscription you want to change.

For more information, see [Manage product entitlements from a service](view-and-grant-products-from-a-service.md).

## Request


### Request syntax

| Method | Request URI                                            |
|--------|--------------------------------------------------------|
| POST   | ```https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/{recurrenceId}/change``` |


### Request header

| Header         | Type   | Description   |
|----------------|--------|-------------|
| Authorization  | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;.                           |
| Host           | string | Must be set to the value **purchase.mp.microsoft.com**.                                            |
| Content-Length | number | The length of the request body.                                                                       |
| Content-Type   | string | Specifies the request and response type. Currently, the only supported value is **application/json**. |


### Request parameters

| Name         | Type  | Description   |  Required  |
|----------------|--------|-------------|-----------|
| recurrenceId | string | The ID of the subscription you want to change. To get this ID, call the [get subscriptions for a user](get-subscriptions-for-a-user.md) method, identify the response body entry that represents the subscription add-on you want to change, and use the value of the **id** field for the entry.     | Yes      |


### Request body

| Field      | Type   | Description   | Required |
|----------------|--------|---------------|----------|
| b2bKey         | string | The [Microsoft Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user whose subscription you want to change.     | Yes      |
| changeType     | string |  One of the following strings that identifies the type of change you want to make:<ul><li>**Cancel**: Cancels the subscription.</li><li>**Extend**: Extends the subscription. If you specify this value, you must also include the *extensionTimeInDays* parameter.</li><li>**Refund**: Refunds the subscription to the customer.</li><li>**ToggleAutoRenew**: Disables automatic renewal for the subscription. If automatic renewal is currently disabled for the subscription, this value does nothing.</li></ul>   | Yes      |
| extensionTimeInDays  | string  | If the *changeType* parameter has the value **Extend**, this parameter specifies the number of days to extend the subscription. |  Yes, if *changeType* has the value **Extend**; otherwise, no.  ||


### Request example

The following example demonstrates how to use this method to extend the subscription period by 5 days. Replace the *b2bKey* value with the [Microsoft Store ID key](view-and-grant-products-from-a-service.md#step-4) that represents the identity of the user whose subscription you want to change.

```json
POST https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/mdr:0:bc0cb6960acd4515a0e1d638192d77b7:77d5ebee-0310-4d23-b204-83e8613baaac/change HTTP/1.1
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
      "id":"mdr:0:bc0cb6960acd4515a0e1d638192d77b7:77d5ebee-0310-4d23-b204-83e8613baaac",
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
| expirationTimeWithGrace | string | The date and time the subscription will expire including the grace period, in ISO 8601 format. This value indicates when the user will lose access to the subscription after the subscription has failed to automatically renew.    |
| id | string |  The ID of the subscription. Use this value to indicate which subscription you want to modify when you call the [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md) method.    |
| isTrial | Boolean |  Indicates whether the subscription is a trial.     |
| lastModified | string |  The date and time the subscription was last modified, in ISO 8601 format.      |
| market | string | The country code (in two-letter ISO 3166-1 alpha-2 format) in which the user acquired the subscription.      |
| productId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [product](in-app-purchases-and-trials.md#products-skus-and-availabilities) that represents the subscription add-on in the Microsoft Store catalog. An example Store ID for a product is 9NBLGGH42CFD.     |
| skuId | string |  The [Store ID](in-app-purchases-and-trials.md#store-ids) for the [SKU](in-app-purchases-and-trials.md#products-skus-and-availabilities) that represents the subscription add-on the Microsoft Store catalog. An example Store ID for a SKU is 0010.    |
| startTime | string |  The start date and time for the subscription, in ISO 8601 format.     |
| recurrenceState | string  |  One of the following values:<ul><li>**None**:&nbsp;&nbsp;This indicates a perpetual subscription.</li><li>**Active**:&nbsp;&nbsp;The subscription is active and the user is entitled to use the services.</li><li>**Inactive**:&nbsp;&nbsp;The subscription is past the expiration date, and the user turned off the automatic renew option for the subscription.</li><li>**Canceled**:&nbsp;&nbsp;The subscription has been purposefully terminated before the expiration date, with or without a refund.</li><li>**InDunning**:&nbsp;&nbsp;The subscription is in *dunning* (that is, the subscription is nearing expiration, and Microsoft is trying to acquire funds to automatically renew the subscription).</li><li>**Failed**:&nbsp;&nbsp;The dunning period is over and the subscription failed to renew after several attempts.</li></ul><p>**Note:**</p><ul><li>**Inactive**/**Canceled**/**Failed** are terminal states. When a subscription enters one of these states, the user must repurchase the subscription to activate it again. The user is not entitled to use the services in these states.</li><li>When a subscription is **Canceled**, the expirationTime will be updated with the date and time of the cancellation.</li><li>The ID of the subscription will remain the same during its entire lifetime. It will not change if the auto-renew option is turned on or off. If a user repurchases a subscription after reaching a terminal state, a new subscription ID will be created.</li><li>The ID of a subscription should be used to execute any operation on an individual subscription.</li><li>When a user repurchases a subscription after cancelling or discontinuing it, if you query the results for the user you will get two entries: one with the old subscription ID in a terminal state, and one with the new subscription ID in an active state.</li><li>It's always a good practice to check both recurrenceState and expirationTime, since updates to recurrenceState can potentially be delayed by a few minutes (or occasionally hours).       |
| cancellationDate | string   |  The date and time the user's subscription was cancelled, in ISO 8601 format.     |


## Related topics


* [Manage product entitlements from a service](view-and-grant-products-from-a-service.md)
* [Get subscriptions for a user](get-subscriptions-for-a-user.md)
* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Renew a Microsoft Store ID key](renew-a-windows-store-id-key.md)
