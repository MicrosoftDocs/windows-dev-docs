---
ms.assetid: B071F6BC-49D3-4E74-98EA-0461A1A55EFB
description: If you have a catalog of apps and add-ons, you can use the Microsoft Store collection API and Microsoft Store purchase API to access ownership information for these products from your services.
title: Manage product entitlements from a service
ms.date: 08/01/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store collection API, Microsoft Store purchase API, view products, grant products
ms.localizationpriority: medium
---
# Manage product entitlements from a service

If you have a catalog of apps and add-ons, you can use the *Microsoft Store collection API* and *Microsoft Store purchase API* to access entitlement information for these products from your services. An *entitlement* represents a customer's right to use an app or add-on that is published through the Microsoft Store.

These APIs consist of REST methods that are designed to be used by developers with add-on catalogs that are supported by cross-platform services. These APIs enable you to do the following:

-   Microsoft Store collection API: [Query for products owned by a user](query-for-products.md) and [report a consumable product as fulfilled](report-consumable-products-as-fulfilled.md).
-   Microsoft Store purchase API: [Grant a free product to a user](grant-free-products.md), [get subscriptions for a user](get-subscriptions-for-a-user.md), and [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md).

> [!NOTE]
> The Microsoft Store collection API and purchase API use Azure Active Directory (Azure AD) authentication to access customer ownership information. To use these APIs, you (or your organization) must have an Azure AD directory and you must have [Global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permission for the directory. If you already use Microsoft 365 or other business services from Microsoft, you already have Azure AD directory.

## Overview

The following steps describe the end-to-end process for using the Microsoft Store collection API and purchase API:

1.  [Configure an application in Azure AD](#step-1).
2.  [Associate your Azure AD application ID with your app in Partner Center](#step-2).
3.  In your service, [create Azure AD access tokens](#step-3) that represent your publisher identity.
4.  In your client Windows app, [create a Microsoft Store ID key](#step-4) that represents the identity of the current user, and pass this key back to your service.
5.  After you have the required Azure AD access token and Microsoft Store ID key, [call the Microsoft Store collection API or purchase API from your service](#step-5).

This end-to-end process involves two software components that perform different tasks:

* **Your service**. This is an application that runs securely in the context of your business environment, and it can be implemented using any development platform you choose. Your service is responsible for creating the Azure AD access tokens needed for the scenario and for calling the REST URIs for the Microsoft Store collection API and purchase API.
* **Your client Windows app**. This is the app for which you want to access and manage customer entitlement information (including add-ons for the app). This app is responsible for creating the Microsoft Store ID keys you need to call the Microsoft Store collection API and purchase API from your service.

<span id="step-1"/>

## Step 1: Configure an application in Azure AD

Before you can use the Microsoft Store collection API or purchase API, you must create an Azure AD Web application, retrieve the tenant ID and application ID for the application, and generate a key. The Azure AD Web application represents the service from which you want to call the Microsoft Store collection API or purchase API. You need the tenant ID, application ID and key to generate Azure AD access tokens that you need to call the API.

> [!NOTE]
> You only need to perform the tasks in this section one time. After you update your Azure AD application manifest and you have your tenant ID, application ID and client secret, you can reuse these values any time you need to create a new Azure AD access token.

1.  If you haven't done so already, follow the instructions in [Integrating Applications with Azure Active Directory](/azure/active-directory/develop/active-directory-integrating-applications) to register a **Web app / API** application with Azure AD.
    > [!NOTE]
    > When you register your application, you must choose **Web app / API** as the application type so that you can retrieve a key (also called a *client secret*) for your application. In order to call the Microsoft Store collection API or purchase API, you must provide a client secret when you request an access token from Azure AD in a later step.

2.  In the [Azure Management Portal](https://portal.azure.com/), navigate to **Azure Active Directory**. Select your directory, click **App registrations** in the left navigation pane, and then select your application.
3.  You are taken to the application's main registration page. On this page, copy the **Application ID** value for use later.
4.  Create a key that you will need later (this is all called a *client secret*). In the left pane, click **Settings** and then **Keys**. On this page, complete the steps to [create a key](/azure/active-directory/develop/active-directory-integrating-applications#to-add-application-credentials-or-permissions-to-access-web-apis). Copy this key for later use.
5.  Add several required audience URIs to your [application manifest](/azure/active-directory/develop/active-directory-application-manifest). In the left pane, click **Manifest**. Click **Edit**, replace the `"identifierUris"` section with the following text, and then click **Save**.

    ```json
    "identifierUris" : [                                
            "https://onestore.microsoft.com",
            "https://onestore.microsoft.com/b2b/keys/create/collections",
            "https://onestore.microsoft.com/b2b/keys/create/purchase"
        ],
    ```

    These strings represent the audiences supported by your application. In a later step, you will create Azure AD access tokens that are associated with each of these audience values.

<span id="step-2"/>

## Step 2: Associate your Azure AD application ID with your client app in Partner Center

Before you can use the Microsoft Store collection API or purchase API to configure the ownership and purchases for your app or add-on, you must associate your Azure AD application ID with the app (or the app that contains the add-on) in Partner Center.

> [!NOTE]
> You only need to perform this task one time.

1.  Sign in to [Partner Center](https://partner.microsoft.com/dashboard) and select your app.
2.  Go to the **Services** &gt; **Product collections and purchases** page and enter your Azure AD application ID into one of the available **Client ID** fields.

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

To create the access tokens, use the OAuth 2.0 API in your service by following the instructions in [Service to Service Calls Using Client Credentials](/azure/active-directory/azuread-dev/v1-oauth2-client-creds-grant-flow) to send an HTTP POST to the ```https://login.microsoftonline.com/<tenant_id>/oauth2/token``` endpoint. Here is a sample request.

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

* For the *client\_id* and *client\_secret* parameters, specify the application ID and the client secret for your application that you retrieved from the [Azure Management Portal](https://portal.azure.com/). Both of these parameters are required in order to create an access token with the level of authentication required by the Microsoft Store collection API or purchase API.

* For the *resource* parameter, specify one of the audience URIs listed in the [previous section](#access-tokens), depending on the type of access token you are creating.

After your access token expires, you can refresh it by following the instructions [here](/azure/active-directory/azuread-dev/v1-protocols-oauth-code#refreshing-the-access-tokens). For more details about the structure of an access token, see [Supported Token and Claim Types](/azure/active-directory/develop/id-tokens).

<span id="step-4"/>

## Step 4: Create a Microsoft Store ID key

Before you can call any method in the Microsoft Store collection API or purchase API, your app must create a Microsoft Store ID key and send it to your service. This key is a JSON Web Token (JWT) that represents the identity of the user whose product ownership information you want to access. For more information about the claims in this key, see [Claims in a Microsoft Store ID key](#claims-in-a-microsoft-store-id-key).

Currently, the only way to create a Microsoft Store ID key is by calling a Universal Windows Platform (UWP) API from client code in your app. The generated key represents the identity of the user who is currently signed in to the Microsoft Store on the device.

> [!NOTE]
> Each Microsoft Store ID key is valid for 90 days. After a key expires, you can [renew the key](renew-a-windows-store-id-key.md). We recommend that you renew your Microsoft Store ID keys rather than creating new ones.

<span />

### To create a Microsoft Store ID key for the Microsoft Store collection API

Follow these steps to create a Microsoft Store ID key that you can use with the Microsoft Store collection API to [query for products owned by a user](query-for-products.md) or [report a consumable product as fulfilled](report-consumable-products-as-fulfilled.md).

1.  Pass the Azure AD access token that has the audience URI value `https://onestore.microsoft.com/b2b/keys/create/collections` from your service to your client app. This is one of the tokens you created [earlier in step 3](#step-3).

2.  In your app code, call one of these methods to retrieve a Microsoft Store ID key:

  * If your app uses the [StoreContext](/uwp/api/Windows.Services.Store.StoreContext) class in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to manage in-app purchases, use the [StoreContext.GetCustomerCollectionsIdAsync](/uwp/api/windows.services.store.storecontext.getcustomercollectionsidasync) method.

  * If your app uses the [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) class in the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace to manage in-app purchases, use the [CurrentApp.GetCustomerCollectionsIdAsync](/uwp/api/windows.applicationmodel.store.currentapp.getcustomercollectionsidasync) method.

    Pass your Azure AD access token to the *serviceTicket* parameter of the method. If you maintain anonymous user IDs in the context of services that you manage as the publisher of the current app, you can also pass a user ID to the *publisherUserId* parameter to associate the current user with the new Microsoft Store ID key (the user ID will be embedded in the key). Otherwise, if you don't need to associate a user ID with the Microsoft Store ID key, you can pass any string value to the *publisherUserId* parameter.

3.  After your app successfully creates a Microsoft Store ID key, pass the key back to your service.

<span />

### To create a Microsoft Store ID key for the Microsoft Store purchase API

Follow these steps to create a Microsoft Store ID key that you can use with the Microsoft Store purchase API to [grant a free product to a user](grant-free-products.md), [get subscriptions for a user](get-subscriptions-for-a-user.md), or [change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md).

1.  Pass the Azure AD access token that has the audience URI value `https://onestore.microsoft.com/b2b/keys/create/purchase` from your service to your client app. This is one of the tokens you created [earlier in step 3](#step-3).

2.  In your app code, call one of these methods to retrieve a Microsoft Store ID key:

  * If your app uses the [StoreContext](/uwp/api/Windows.Services.Store.StoreContext) class in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to manage in-app purchases, use the [StoreContext.GetCustomerPurchaseIdAsync](/uwp/api/windows.services.store.storecontext.getcustomerpurchaseidasync) method.

  * If your app uses the [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) class in the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace to manage in-app purchases, use the [CurrentApp.GetCustomerPurchaseIdAsync](/uwp/api/windows.applicationmodel.store.currentapp.getcustomerpurchaseidasync) method.

    Pass your Azure AD access token to the *serviceTicket* parameter of the method. If you maintain anonymous user IDs in the context of services that you manage as the publisher of the current app, you can also pass a user ID to the *publisherUserId* parameter to associate the current user with the new Microsoft Store ID key (the user ID will be embedded in the key). Otherwise, if you don't need to associate a user ID with the Microsoft Store ID key, you can pass any string value to the *publisherUserId* parameter.

3.  After your app successfully creates a Microsoft Store ID key, pass the key back to your service.

### Diagram

The following diagram illustrates the process of creating a Microsoft Store ID key.

  ![Create Windows Store ID key](images/b2b-1.png)

<span id="step-5"/>

## Step 5: Call the Microsoft Store collection API or purchase API from your service

After your service has a Microsoft Store ID key that enables it to access a specific user's product ownership information, your service can call the Microsoft Store collection API or purchase API by following these instructions:

* [Query for products](query-for-products.md)
* [Report consumable products as fulfilled](report-consumable-products-as-fulfilled.md)
* [Grant free products](grant-free-products.md)
* [Get subscriptions for a user](get-subscriptions-for-a-user.md)
* [Change the billing state of a subscription for a user](change-the-billing-state-of-a-subscription-for-a-user.md)

For each scenario, pass the following information to the API:

-   In the request header, pass the Azure AD access token that has the audience URI value `https://onestore.microsoft.com`. This is one of the tokens you created [earlier in step 3](#step-3). This token represents your publisher identity.
-   In the request body, pass the Microsoft Store ID key you retrieved [earlier in step 4](#step-4) from client-side code in your app. This key represents the identity of the user whose product ownership information you want to access.

### Diagram

The following diagram describes the process of calling a method in the Microsoft Store collection API or purchase API from your service.

  ![Call collections or puchase API](images/b2b-2.png)

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
* [Integrating Applications with Azure Active Directory](/azure/active-directory/develop/quickstart-register-app)
* [Understanding the Azure Active Directory application manifest]( https://go.microsoft.com/fwlink/?LinkId=722500)
* [Supported Token and Claim Types](/azure/active-directory/develop/id-tokens)