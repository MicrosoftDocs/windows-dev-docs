---
author: mcleanbyron
ms.assetid: B071F6BC-49D3-4E74-98EA-0461A1A55EFB
description: If you have a catalog of apps and add-ons, you can use the Microsoft Store collection API and Microsoft Store purchase API to access ownership information for these products from your services.
title: Manage product entitlements from a service
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Microsoft Store collection API, Microsoft Store purchase API, view products, grant products
ms.localizationpriority: high
---

# Manage product entitlements from a service

If you have a catalog of apps and add-ons, you can use the *Microsoft Store collection API* and *Microsoft Store purchase API* to access entitlement information for these products from your services. An *entitlement* represents a customer's right to use an app or add-on that is published through the Microsoft Store.

These APIs consist of REST methods that are designed to be used by developers with add-on catalogs that are supported by cross-platform services. These APIs enable you to do the following:

-   Microsoft Store collection API: [Query for products owned by a user](query-for-products.md) and [report a consumable product as fulfilled](report-consumable-products-as-fulfilled.md).
-   Microsoft Store purchase API: [Grant a free product to a user](grant-free-products.md), [get subscriptions for a user](get-subscriptions-for-a-user.md), and [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md).

> [!NOTE]
> The Microsoft Store collection API and purchase API use Azure Active Directory (Azure AD) authentication to access customer ownership information. To use these APIs, you (or your organization) must have an Azure AD directory and you must have [Global administrator](http://go.microsoft.com/fwlink/?LinkId=746654) permission for the directory. If you already use Office 365 or other business services from Microsoft, you already have Azure AD directory.

## Overview

The following steps describe the end-to-end process for using the Microsoft Store collection API and purchase API:

1.  [Configure a Web application in Azure AD](#step-1).
2.  [Associate your Azure AD client ID with your application in the Windows Dev Center dashboard](#step-2).
3.  In your service, [create Azure AD access tokens](#step-3) that represent your publisher identity.
4.  In client-side code in your Windows app, [create a Microsoft Store ID key](#step-4) that represents the identity of the current user, and pass the Microsoft Store ID key back to your service.
5.  After you have the required Azure AD access token and Microsoft Store ID key, [call the Microsoft Store collection API or purchase API from your service](#step-5).

The following sections provide more details about each of these steps.

<span id="step-1"/>
## Step 1: Configure a Web application in Azure AD

Before you can use the Microsoft Store collection API or purchase API, you must create an Azure AD Web application, retrieve the tenant ID and client ID for the application, and generate a key. The Azure AD application represents the app or service from which you want to call the Microsoft Store collection API or purchase API. You need the tenant ID, client ID and key to obtain an Azure AD access token that you pass to the API.

> [!NOTE]
> You only need to perform the tasks in this section one time. After you update your Azure AD application manifest and you have your tenant ID, client ID and client secret, you can reuse these values any time you need to create a new Azure AD access token.

1.  Follow the instructions in [Integrating Applications with Azure Active Directory](http://go.microsoft.com/fwlink/?LinkId=722502) to add a Web application to Azure AD.
    > [!NOTE]
    > On the **Tell us about your application page**, make sure that you choose **Web application and/or web API**. This is required so that you can retrieve a key (also called a *client secret*) for your application. In order to call the Microsoft Store collection API or purchase API, you must provide a client secret when you request an access token from Azure AD in a later step.

2.  In the [Azure Management Portal](http://manage.windowsazure.com/), navigate to **Active Directory**. Select your directory, click the **Applications** tab at the top, and then select your application.
3.  Click the **Configure** tab. On this tab, obtain the client ID for your application and request a key (this is called a *client secret* in later steps).
4.  At the bottom of the screen, click **Manage manifest**. Download your Azure AD application manifest and replace the `"identifierUris"` section with the following text.

    ```json
    "identifierUris" : [                                
            "https://onestore.microsoft.com",
            "https://onestore.microsoft.com/b2b/keys/create/collections",
            "https://onestore.microsoft.com/b2b/keys/create/purchase"
        ],
    ```

    These strings represent the audiences supported by your application. In a later step, you will create Azure AD access tokens that are associated with each of these audience values. For more information about how to download your application manifest, see [Understanding the Azure Active Directory application manifest](http://go.microsoft.com/fwlink/?LinkId=722500).

5.  Save your application manifest and upload it to your application in the [Azure Management Portal](http://manage.windowsazure.com/).

<span id="step-2"/>
## Step 2: Associate your Azure AD client ID with your app in Windows Dev Center

Before you can use the Microsoft Store collection API or purchase API to operate on an app or add-on, you must associate your Azure AD client ID with the app (or the app that contains the add-on) in the Dev Center dashboard.

> [!NOTE]
> You only need to perform this task one time.

1.  Sign in to the [Dev Center dashboard](https://dev.windows.com/overview) and select your app.
2.  Go to the **Services** &gt; **Product collections and purchases** page and enter your Azure AD client ID into one of the available fields.

<span id="step-3"/>
## Step 3: Create Azure AD access tokens

Before you can retrieve a Microsoft Store ID key or call the Microsoft Store collection API or purchase API, your service must create several different Azure AD access tokens that represent your publisher identity. Each token will be used with a different API. The lifetime of each token is 60 minutes, and you can refresh them after they expire.

> [!IMPORTANT]
> Create Azure AD access tokens only in the context of your service, not in your app. Your client secret could be compromised if it is sent to your app.

<span id="access-tokens" />
### Understanding the different tokens and audience URIs

Depending on which methods you want to call in the Microsoft Store collection API or purchase API, you must create either two or three different tokens. Each access token is associated with a different audience URI (these are the same URIs that you previously added to the `"identifierUris"` section of the Azure AD application manifest).

  * In all cases, you must create a token with the `https://onestore.microsoft.com` audience URI. In a later step, you will pass this token to the **Authorization** header of methods in the Microsoft Store collection API or purchase API.
      > [!IMPORTANT]
      > Use the `https://onestore.microsoft.com` audience only with access tokens that are stored securely within your service. Exposing access tokens with this audience outside your service could make your service vulnerable to replay attacks.

  * If you want to call a method in the Microsoft Store collection API to [query for products owned by a user](query-for-products.md) or [report a consumable product as fulfilled](report-consumable-products-as-fulfilled.md), you must also create a token with the `https://onestore.microsoft.com/b2b/keys/create/collections` audience URI. In a later step, you will pass this token to a client method in the Windows SDK to request a Microsoft Store ID key that you can use with the Microsoft Store collection API.

  * If you want to call a method in the Microsoft Store purchase API to [grant a free product to a user](grant-free-products.md), [get subscriptions for a user](get-subscriptions-for-a-user.md), or [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md), you must also create a token with the `https://onestore.microsoft.com/b2b/keys/create/purchase` audience URI. In a later step, you will pass this token to a client method in the Windows SDK to request a Microsoft Store ID key that you can use with the Microsoft Store purchase API.

<span />
### Create the tokens

To create the access tokens, use the OAuth 2.0 API in your service by following the instructions in [Service to Service Calls Using Client Credentials](https://azure.microsoft.com/documentation/articles/active-directory-protocols-oauth-service-to-service) to send an HTTP POST to the ```https://login.microsoftonline.com/<tenant_id>/oauth2/token``` endpoint. Here is a sample request.

``` syntax
POST https://login.microsoftonline.com/<tenant_id>/oauth2/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded; charset=utf-8

grant_type=client_credentials
&client_id=<your_client_id>
&client_secret=<your_client_secret>
&resource=https://onestore.microsoft.com
```

For each token, specify the following parameter data:

* For the *client\_id* and *client\_secret* parameters, specify the client ID and the client secret for your application that you retrieved from the [Azure Management Portal](http://manage.windowsazure.com). Both of these parameters are required in order to create an access token with the level of authentication required by the Microsoft Store collection API or purchase API.

* For the *resource* parameter, specify one of the audience URIs listed in the [previous section](#access-tokens), depending on the type of access token you are creating.

After your access token expires, you can refresh it by following the instructions [here](https://azure.microsoft.com/documentation/articles/active-directory-protocols-oauth-code/#refreshing-the-access-tokens). For more details about the structure of an access token, see [Supported Token and Claim Types](http://go.microsoft.com/fwlink/?LinkId=722501).

<span id="step-4"/>
## Step 4: Create a Microsoft Store ID key

Before you can call any method in the Microsoft Store collection API or purchase API, your app must create a Microsoft Store ID key and send it to your service. This key is a JSON Web Token (JWT) that represents the identity of the user whose product ownership information you want to access. For more information about the claims in this key, see [Claims in a Microsoft Store ID key](#claims-in-a-microsoft-store-id-key).

Currently, the only way to create a Microsoft Store ID key is by calling a Universal Windows Platform (UWP) API from client code in your app. The generated key represents the identity of the user who is currently signed in to the Microsoft Store on the device.

> [!NOTE]
> Each Microsoft Store ID key is valid for 90 days. After a key expires, you can [renew the key](renew-a-windows-store-id-key.md). We recommend that you renew your Microsoft Store ID keys rather than creating new ones.

<span />
### To create a Microsoft Store ID key for the Microsoft Store collection API

Follow these steps to create a Microsoft Store ID key that you can use with the Microsoft Store collection API to [query for products owned by a user](query-for-products.md) or [report a consumable product as fulfilled](report-consumable-products-as-fulfilled.md).

1.  Pass the Azure AD access token that you created with the `https://onestore.microsoft.com/b2b/keys/create/collections` audience URI from your service to your client app.

2.  In your app code, call one of these methods to retrieve a Microsoft Store ID key:

  * If your app uses the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace to manage in-app purchases, use the [StoreContext.GetCustomerCollectionsIdAsync](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getcustomercollectionsidasync.aspx) method.

  * If your app uses the [CurrentApp](https://msdn.microsoft.com/library/windows/apps/hh779765) class in the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace to manage in-app purchases, use the [CurrentApp.GetCustomerCollectionsIdAsync](https://msdn.microsoft.com/library/windows/apps/mt608674) method.

    Pass your Azure AD access token to the *serviceTicket* parameter of the method. You can optionally pass an ID to the *publisherUserId* parameter that identifies the current user in the context of your services. If you maintain user IDs for your services, you can use this parameter to correlate these user IDs with the calls you make to the Microsoft Store collection API.

3.  After your app successfully creates a Microsoft Store ID key, pass the key back to your service.

<span />
### To create a Microsoft Store ID key for the Microsoft Store purchase API

Follow these steps to create a Microsoft Store ID key that you can use with the Microsoft Store purchase API to [grant a free product to a user](grant-free-products.md), [get subscriptions for a user](get-subscriptions-for-a-user.md), or [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md).

1.  Pass the Azure AD access token that you created with the `https://onestore.microsoft.com/b2b/keys/create/purchase` audience URI from your service to your client app.

2.  In your app code, call one of these methods to retrieve a Microsoft Store ID key:

  * If your app uses the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace to manage in-app purchases, use the [StoreContext.GetCustomerPurchaseIdAsync](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getcustomerpurchaseidasync.aspx) method.

  * If your app uses the [CurrentApp](https://msdn.microsoft.com/library/windows/apps/hh779765) class in the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace to manage in-app purchases, use the [CurrentApp.GetCustomerPurchaseIdAsync](https://msdn.microsoft.com/library/windows/apps/mt608675) method.

    Pass your Azure AD access token to the *serviceTicket* parameter of the method. You can optionally pass an ID to the *publisherUserId* parameter that identifies the current user in the context of your services. If you maintain user IDs for your services, you can use this parameter to correlate these user IDs with the calls you make to the Microsoft Store purchase API.

3.  After your app successfully creates a Microsoft Store ID key, pass the key back to your service.

<span id="step-5"/>
## Step 5: Call the Microsoft Store collection API or purchase API from your service

After your service has a Microsoft Store ID key that enables it to access a specific user's product ownership information, your service can call the Microsoft Store collection API or purchase API by following these instructions:

* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Grant free products](grant-free-products.md)
* [Get subscriptions for a user](get-subscriptions-for-a-user.md)
* [Change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md)

For each scenario, pass the following information to the API:

-   The Azure AD access token that you created earlier with the `https://onestore.microsoft.com` audience URI. This token represents your publisher identity. Pass this token in the request header.
-   The Microsoft Store ID key you retrieved from client-side code in your app. This key represents the identity of the user whose product ownership information you want to access.

## Claims in a Microsoft Store ID key

A Microsoft Store ID key is a JSON Web Token (JWT) that represents the identity of the user whose product ownership information you want to access. When decoded using Base64, a Microsoft Store ID key contains the following claims.

* `iat`:&nbsp;&nbsp;&nbsp;Identifies the time at which the key was issued. This claim can be used to determine the age of the token. This value is expressed as epoch time.
* `iss`:&nbsp;&nbsp;&nbsp;Identifies the issuer. This has the same value as the `aud` claim.
* `aud`:&nbsp;&nbsp;&nbsp;Identifies the audience. Must be one of the following values: `https://collections.mp.microsoft.com/v6.0/keys` or `https://purchase.mp.microsoft.com/v6.0/keys`.
* `exp`:&nbsp;&nbsp;&nbsp;Identifies the expiration time on or after which the key will no longer be accepted for processing anything except for renewing keys. The value of this claim is expressed as epoch time.
* `nbf`:&nbsp;&nbsp;&nbsp;Identifies the time at which the token will be accepted for processing. The value of this claim is expressed as epoch time.
* `http://schemas.microsoft.com/marketplace/2015/08/claims/key/clientId`:&nbsp;&nbsp;&nbsp;The client ID that identifies the developer.
* `http://schemas.microsoft.com/marketplace/2015/08/claims/key/payload`:&nbsp;&nbsp;&nbsp;An opaque payload (encrypted and Base64 encoded) that contains information that is intended only for use by Microsoft Store services.
* `http://schemas.microsoft.com/marketplace/2015/08/claims/key/userId`:&nbsp;&nbsp;&nbsp;A user ID that identifies the current user in the context of your services. This is the same value you pass into the optional *publisherUserId* parameter of the [method you use to create the key](#step-4).
* `http://schemas.microsoft.com/marketplace/2015/08/claims/key/refreshUri`:&nbsp;&nbsp;&nbsp;The URI that you can use to renew the key.

Here is an example of a decoded Microsoft Store ID key header.

```json
{
    "typ":"JWT",
    "alg":"RS256",
    "x5t":"agA_pgJ7Twx_Ex2_rEeQ2o5fZ5g"
}
```

Here is an example of a decoded Microsoft Store ID key claim set.

```json
{
    "http://schemas.microsoft.com/marketplace/2015/08/claims/key/clientId": "1d5773695a3b44928227393bfef1e13d",
    "http://schemas.microsoft.com/marketplace/2015/08/claims/key/payload": "ZdcOq0/N2rjytCRzCHSqnfczv3f0343wfSydx7hghfu0snWzMqyoAGy5DSJ5rMSsKoQFAccs1iNlwlGrX+/eIwh/VlUhLrncyP8c18mNAzAGK+lTAd2oiMQWRRAZxPwGrJrwiq2fTq5NOVDnQS9Za6/GdRjeiQrv6c0x+WNKxSQ7LV/uH1x+IEhYVtDu53GiXIwekltwaV6EkQGphYy7tbNsW2GqxgcoLLMUVOsQjI+FYBA3MdQpalV/aFN4UrJDkMWJBnmz3vrxBNGEApLWTS4Bd3cMswXsV9m+VhOEfnv+6PrL2jq8OZFoF3FUUpY8Fet2DfFr6xjZs3CBS1095J2yyNFWKBZxAXXNjn+zkvqqiVRjjkjNajhuaNKJk4MGHfk2rZiMy/aosyaEpCyncdisHVSx/S4JwIuxTnfnlY24vS0OXy7mFiZjjB8qL03cLsBXM4utCyXSIggb90GAx0+EFlVoJD7+ZKlm1M90xO/QSMDlrzFyuqcXXDBOnt7rPynPTrOZLVF+ODI5HhWEqArkVnc5MYnrZD06YEwClmTDkHQcxCvU+XUEvTbEk69qR2sfnuXV4cJRRWseUTfYoGyuxkQ2eWAAI1BXGxYECIaAnWF0W6ThweL5ZZDdadW9Ug5U3fZd4WxiDlB/EZ3aTy8kYXTW4Uo0adTkCmdLibw=",
    "http://schemas.microsoft.com/marketplace/2015/08/claims/key/userId": "infusQMLaYCrgtC0d/SZWoPB4FqLEwHXgZFuMJ6TuTY=",
    "http://schemas.microsoft.com/marketplace/2015/08/claims/key/refreshUri": "https://collections.mp.microsoft.com/v6.0/b2b/keys/renew",
    "iat": 1442395542,
    "iss": "https://collections.mp.microsoft.com/v6.0/keys",
    "aud": "https://collections.mp.microsoft.com/v6.0/keys",
    "exp": 1450171541,
    "nbf": 1442391941
}
```

## Related topics

* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Grant free products](grant-free-products.md)
* [Get subscriptions for a user](get-subscriptions-for-a-user.md)
* [Change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md)
* [Renew a Microsoft Store ID key](renew-a-windows-store-id-key.md)
* [Integrating Applications with Azure Active Directory](http://go.microsoft.com/fwlink/?LinkId=722502)
* [Understanding the Azure Active Directory application manifest]( http://go.microsoft.com/fwlink/?LinkId=722500)
* [Supported Token and Claim Types](http://go.microsoft.com/fwlink/?LinkId=722501)
