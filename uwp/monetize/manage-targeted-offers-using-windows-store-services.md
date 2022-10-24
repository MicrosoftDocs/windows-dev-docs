---
ms.assetid: 9F0A59A1-FAD7-4AD5-B78B-C1280F215D23
description: Use the Microsoft Store targeted offers API to get targeted offers that are available for the current user of your app.
title: Manage targeted offers using Store services
ms.date: 10/10/2017
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store targeted offers API, targeted offers
ms.localizationpriority: medium
---
# Manage targeted offers using Store services

If you create a *targeted offer* in the **Engage > Targeted offers** page for your app in Partner Center, use the *Microsoft Store targeted offers API* in your app's code to retrieve info that helps you implement the in-app experience for the targeted offer. For more information about targeted offers and how to create them in the dashboard, see [Use targeted offers to maximize engagement and conversions](../publish/use-targeted-offers-to-maximize-engagement-and-conversions.md).

The targeted offers API is a simple REST API that you can use to get the targeted offers that are available for the current user, based on whether or not the user is part of the customer segment for the targeted offer. To use this API in your app's code, follow these steps:

1.  [Get a Microsoft Account token](#obtain-a-microsoft-account-token) for the current signed-in user of your app.
2.  [Get the targeted offers for the current user](#get-targeted-offers).
3.  Implement the in-app purchase experience for the add-on that is associated with one of the targeted offers. For more information about implementing in-app purchases, see [this article](enable-in-app-purchases-of-apps-and-add-ons.md).

For a complete code example that demonstrates all of these steps, see the [code example](#code-example) at the end of this article. The following sections provide more details about each step.

<span id="obtain-a-microsoft-account-token" />

## Get a Microsoft Account token for the current user

In your app's code, get a Microsoft Account (MSA) token for the current signed-in user. You must pass this token in the ```Authorization``` request header for the Microsoft Store targeted offers API. This token is used by the Store to retrieve the targeted offers that are available for the current user.

To get the MSA token, use the [WebAuthenticationCoreManager](/uwp/api/windows.security.authentication.web.core.webauthenticationcoremanager) class to request a token using the scope ```devcenter_implicit.basic,wl.basic```. The following example demonstrates how to do this. This example is an excerpt from the [complete example](#code-example), and it requires **using** statements that are provided in the complete example.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs" id="GetMSAToken":::

For more information about getting MSA tokens, see [Web account manager](../security/web-account-manager.md).

<span id="get-targeted-offers" />

## Get the targeted offers for the current user

After you have an MSA token for the current user, call the GET method of the ```https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user``` URI to get the available targeted offers for the current user. For more information about this REST method, see [Get targeted offers](get-targeted-offers.md).

This method returns the product IDs of the add-ons that that are associated with the targeted offers that are available for the current user. With this information, you can offer one or more of the targeted offers as an in-app purchase to the user.

The following example demonstrates how to get the targeted offers for the current user. This example is an excerpt from the [complete example](#code-example). It requires the [Json.NET](https://www.newtonsoft.com/json) library from Newtonsoft and additional classes and **using** statements that are provided in the complete example.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs" id="GetTargetedOffers":::

<span id="code-example" />

## Complete code example

The following code example demonstrates the following tasks:

* Get an MSA token for the current user.
* Get all of the targeted offers for the current user by using the [Get targeted offers](get-targeted-offers.md) method.
* Purchase the add-on that is associated with a targeted offer.

This example requires the [Json.NET](https://www.newtonsoft.com/json) library from Newtonsoft. The example uses this library to serialize and deserialize JSON-formatted data.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs" id="GetTargetedOffersSample":::

## Related topics

* [Use targeted offers to maximize engagement and conversions](../publish/use-targeted-offers-to-maximize-engagement-and-conversions.md)
* [Get targeted offers](get-targeted-offers.md)
