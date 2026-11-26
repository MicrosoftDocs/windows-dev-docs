---
description: Use this method in the Microsoft Store submission API to get the legacy price tiers and market-specific retail prices for an app submission.
title: Get price tiers for an app submission
author: GrantMeStrength
ms.author: jken
ms.date: 11/09/2026
ms.topic: how-to
keywords: windows 10, uwp, Microsoft Store submission API, app submission, price tiers, pricing
ms.localizationpriority: medium
---

# Get price tiers for an app submission

Use this method in the Microsoft Store submission API to get the legacy price tiers and market-specific retail prices for an existing app submission. You can use the returned price identifiers when you [update the app submission](update-an-app-submission.md).

For more information about managing app submissions, see [Manage app submissions](manage-app-submissions.md).

> [!NOTE]
> This method is read-only and applies to legacy pricing. It does not change prices or add support for Pricing Version 2, free-form prices, or scheduled sales.

## Prerequisites

To use this method, you need to first do the following:

* Complete the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain a Microsoft Entra ID access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header.
* Identify an existing submission for an app in your Partner Center account. You can get a submission ID from [Get an app](get-an-app.md) or [Create an app submission](create-an-app-submission.md).

## Request

This method has the following syntax.

| Method | Request URI |
|--------|-------------|
| GET | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/priceTiers` |

### Request header

| Header | Type | Description |
|--------|------|-------------|
| Authorization | string | Required. The Microsoft Entra ID access token in the form **Bearer** &lt;*token*&gt;. |

### Request parameters

| Name | Type | Description |
|------|------|-------------|
| applicationId | string | Required. The Store ID of the app that contains the submission. |
| submissionId | string | Required. The ID of the submission whose price table you want to get. |
| markets | string | Optional query parameter. A comma-separated list of market codes, such as `US,GB,IN`. You can also repeat the parameter, such as `markets=US&markets=GB`. Omit this parameter to return all valid markets in the submission's price table. |

Market codes are case-insensitive. The API trims surrounding whitespace and ignores duplicate codes. Both the *markets* array and each tier's *retailPrices* contain only the selected markets. Unknown or empty codes, including `markets=` or a trailing comma, return HTTP 400.

### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to get the price tiers for one market. Replace *applicationId* and *submissionId* with the IDs for your app and submission.

```http
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/priceTiers?markets=US HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates an abbreviated JSON response body for a successful call to this method. The amounts are illustrative, not a price guarantee.

```json
{
  "domainDataVersion": 1,
  "isAdvancedPricingModel": true,
  "markets": [
    {
      "marketCode": "US",
      "currencyCode": "USD"
    }
  ],
  "tiers": [
    {
      "tierId": 0,
      "priceId": "Free",
      "retailPrices": {
        "US": 0.0
      }
    },
    {
      "tierId": 1012,
      "priceId": "Tier1012",
      "retailPrices": {
        "US": 0.99
      }
    }
  ]
}
```

### Price tier table resource

This resource has the following values.

| Value | Type | Description |
|-------|------|-------------|
| domainDataVersion | integer | The version of the price table associated with the submission. |
| isAdvancedPricingModel | boolean | Whether the submission uses advanced legacy price tiers. This value is read-only and does not indicate Pricing Version 2 support. |
| markets | array | An array of [market resources](#market-resource) describing the selected markets and their currencies. |
| tiers | array | An array of [price tier resources](#price-tier-resource) containing the tiers and their market-specific retail prices. |

### Market resource

This resource has the following values.

| Value | Type | Description |
|-------|------|-------------|
| marketCode | string | The market code, such as `US`. This is the key used in *retailPrices* and in the submission's *pricing.marketSpecificPricings*. |
| currencyCode | string | The currency code for retail prices in this market, such as `USD`. |

### Price tier resource

This resource has the following values.

| Value | Type | Description |
|-------|------|-------------|
| tierId | integer | The numeric identifier of the tier, not its display label in Partner Center. The free tier has identifier `0`. |
| priceId | string or null | The price identifier supported by the submission's legacy pricing model, such as `Free` or `Tier1012`. This value is null or omitted when a row has no supported price identifier. Do not select such a row for a pricing update. |
| retailPrices | object | A dictionary whose keys are market codes and whose values are numeric retail prices in each market's currency. A missing market entry means no price is available for that tier and market; it does not mean the price is zero. |

Markets that share a currency can have different prices. Use the market code, not the currency code, to select an amount.

## Use a price tier

Read the table for the submission you want to update. Copy a non-null *priceId* into *pricing.priceId* for the base price, or into *pricing.marketSpecificPricings* for a market-specific price. Preserve the rest of the submission data, [update the submission](update-an-app-submission.md), and then [commit the submission](commit-an-app-submission.md).

Use the returned *priceId* rather than constructing one from *tierId*, a displayed tier label, or a monetary amount. The lookup does not change prices or publish the submission.

## Error codes

If the request cannot be successfully completed, the response contains one of the following HTTP error codes.

| Error code | Description |
|------------|-------------|
| 400 | The submission ID is zero or negative, or the market filter contains an empty or unsupported market code. |
| 401 | The request is not authenticated. |
| 403 | You do not have permission to access the app or developer account. |
| 404 | The submission could not be found, or the submission ID is malformed or outside the supported numeric range. |
| 500 | An error prevented the price table from being retrieved. |

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage app submissions](manage-app-submissions.md)
* [Get an app submission](get-an-app-submission.md)
* [Update an app submission](update-an-app-submission.md)
* [Commit an app submission](commit-an-app-submission.md)
