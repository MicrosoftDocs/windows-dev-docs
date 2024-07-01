---
ms.assetid: 7CC11888-8DC6-4FEE-ACED-9FA476B2125E
description: Use the Microsoft Store submission API to programmatically create and manage submissions for apps that are registered to your Partner Center account.
title: Create and manage submissions
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API
ms.localizationpriority: medium
---

# Create and manage submissions

Use the *Microsoft Store submission API* to programmatically query and create submissions for apps, add-ons and package flights for your or your organization's Partner Center account. This API is useful if your account manages many apps or add-ons, and you want to automate and optimize the submission process for these assets. This API uses Azure Active Directory (Azure AD) to authenticate the calls from your app or service.

The following steps describe the end-to-end process of using the Microsoft Store submission API:

1.  Make sure that you have completed all the [prerequisites](#prerequisites).
3.  Before you call a method in the Microsoft Store submission API, [obtain an Azure AD access token](#obtain-an-azure-ad-access-token). After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token.
4.  [Call the Microsoft Store submission API](#call-the-windows-store-submission-api).

<span id="not_supported"></span>

> [!IMPORTANT]
> If you use this API to create a submission for an app, package flight, or add-on, be sure to make further changes to the submission only by using the API, rather than in Partner Center. If you use Partner Center to change a submission that you originally created by using the API, you will no longer be able to change or commit that submission by using the API. In some cases, the submission could be left in an error state where it cannot proceed in the submission process. If this occurs, you must delete the submission and create a new submission.

> [!IMPORTANT]
> You cannot use this API to publish submissions for [volume purchases through the Microsoft Store for Business and Microsoft Store for Education](/windows/apps/publish/organizational-licensing) or to publish submissions for [LOB apps](/windows/apps/publish/distribute-lob-apps-to-enterprises) directly to enterprises. For both of these scenarios, you must use publish the submission in Partner Center.

> [!NOTE]
> This API cannot be used with apps or add-ons that use mandatory app updates and Store-managed consumable add-ons. If you use the Microsoft Store submission API with an app or add-on that uses one of these features, the API will return a 409 error code. In this case, you must use Partner Center to manage the submissions for the app or add-on.

> [!NOTE]
> This API cannot be used with apps or add-ons that are on Pricing Version 2. You know a product is Pricing Version 2 if you can find a "Review price per market" button on the Pricing section of the Pricing and availability page. If you use the Microsoft Store submission API with an app or add-on that are on Pricing Version 2, the API will return a unknown tier for the pricing part. You can continue using this API to update modules other than Pricing and availability.

<span id="prerequisites"></span>

## Step 1: Complete prerequisites for using the Microsoft Store submission API

Before you start writing code to call the Microsoft Store submission API, make sure that you have completed the following prerequisites.

* You (or your organization) must have an Azure AD directory and you must have [Global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permission for the directory. If you already use Microsoft 365 or other business services from Microsoft, you already have Azure AD directory. Otherwise, you can [create a new Azure AD in Partner Center](/windows/apps/publish/partner-center/create-new-azure-ad-tenant) for no additional charge.

* You must [associate an Azure AD application with your Partner Center account](#associate-an-azure-ad-application-with-your-windows-partner-center-account) and obtain your tenant ID, client ID and key. You need these values to obtain an Azure AD access token, which you will use in calls to the Microsoft Store submission API.

* Prepare your app for use with the Microsoft Store submission API:

  * If your app does not yet exist in Partner Center, you must [create your app by reserving its name in Partner Center](/windows/apps/publish/publish-your-app/reserve-your-apps-name?pivots=store-installer-msix). You cannot use the Microsoft Store submission API to create an app in Partner Center; you must work in Partner Center to create it, and then after that you can use the API to access the app and programmatically create submissions for it. However, you can use the API to programmatically create add-ons and package flights before you create submissions for them.

  * Before you can create a submission for a given app using this API, you must first [create one submission for the app in Partner Center](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix), including answering the [age ratings](/windows/apps/publish/publish-your-app/age-ratings?pivots=store-installer-msix) questionnaire. After you do this, you will be able to programmatically create new submissions for this app using the API. You do not need to create an add-on submission or package flight submission before using the API for those types of submissions.

  * If you are creating or updating an app submission and you need to include an app package, [prepare the app package](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix).

  * If you are creating or updating an app submission and you need to include screenshots or images for the Store listing, [prepare the app screenshots and images](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix).

  * If you are creating or updating an add-on submission and you need to include an icon, [prepare the icon](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on).

<span id="associate-an-azure-ad-application-with-your-windows-partner-center-account"></span>

### How to associate an Azure AD application with your Partner Center account

Before you can use the Microsoft Store submission API, you must associate an Azure AD application with your Partner Center account, retrieve the tenant ID and client ID for the application and generate a key. The Azure AD application represents the app or service from which you want to call the Microsoft Store submission API. You need the tenant ID, client ID and key to obtain an Azure AD access token that you pass to the API.

> [!NOTE]
> You only need to perform this task one time. After you have the tenant ID, client ID and key, you can reuse them any time you need to create a new Azure AD access token.

1.  In Partner Center, [associate your organization's Partner Center account with your organization's Azure AD directory](/windows/apps/publish/partner-center/associate-azure-ad-with-partner-center).

2.  Next, from the **Users** page in the **Account settings** section of Partner Center, [add the Azure AD application](/windows/apps/publish/partner-center/manage-azure-ad-applications-in-partner-center) that represents the app or service that you will use to access submissions for your Partner Center account. Make sure you assign this application the **Manager** role. If the application doesn't exist yet in your Azure AD directory, you can [create a new Azure AD application in Partner Center](/windows/apps/publish/partner-center/manage-azure-ad-applications-in-partner-center#create-a-new-azure-ad-application-account-in-your-organizations-directory-and-add-it-to-your-partner-center-account).  

3.  Return to the **Users** page, click the name of your Azure AD application to go to the application settings, and copy down the **Tenant ID** and **Client ID** values.

4. Click **Add new key**. On the following screen, copy down the **Key** value. You won't be able to access this info again after you leave this page. For more information, see [Manage keys for an Azure AD application](/windows/apps/publish/partner-center/manage-azure-ad-applications-in-partner-center#manage-keys).

<span id="obtain-an-azure-ad-access-token"></span>

## Step 2: Obtain an Azure AD access token

Before you call any of the methods in the Microsoft Store submission API, you must first obtain an Azure AD access token that you pass to the **Authorization** header of each method in the API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can refresh the token so you can continue to use it in further calls to the API.

To obtain the access token, follow the instructions in [Service to Service Calls Using Client Credentials](/azure/active-directory/azuread-dev/v1-oauth2-client-creds-grant-flow) to send an HTTP POST to the ```https://login.microsoftonline.com/<tenant_id>/oauth2/token``` endpoint. Here is a sample request.

```json
POST https://login.microsoftonline.com/<tenant_id>/oauth2/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded; charset=utf-8

grant_type=client_credentials
&client_id=<your_client_id>
&client_secret=<your_client_secret>
&resource=https://manage.devcenter.microsoft.com
```

For the *tenant\_id* value in the POST URI and the *client\_id* and *client\_secret* parameters, specify the tenant ID, client ID and the key for your application that you retrieved from Partner Center in the previous section. For the *resource* parameter, you must specify ```https://manage.devcenter.microsoft.com```.

After your access token expires, you can fetch a new one by making the same HTTP call again.

For examples that demonstrate how to obtain an access token by using C#, Java, or Python code, see the Microsoft Store submission API [code examples](#code-examples).

<span id="call-the-windows-store-submission-api">

## Step 3: Use the Microsoft Store submission API

After you have an Azure AD access token, you can call methods in the Microsoft Store submission API. The API includes many methods that are grouped into scenarios for apps, add-ons, and package flights. To create or update submissions, you typically call multiple methods in the Microsoft Store submission API in a specific order. For information about each scenario and the syntax of each method, see the articles in the following table.

> [!NOTE]
> After you obtain an access token, you have 60 minutes to call methods in the Microsoft Store submission API before the token expires.

| Scenario       | Description                                                                 |
|---------------|----------------------------------------------------------------------|
| Apps |  Retrieve data for all the apps that are registered to your Partner Center account and create submissions for apps. For more information about these methods, see the following articles: <ul><li>[Get app data](get-app-data.md)</li><li>[Manage app submissions](manage-app-submissions.md)</li></ul> |
| Add-ons | Get, create, or delete add-ons for your apps, and then get, create, or delete submissions for the add-ons. For more information about these methods, see the following articles: <ul><li>[Manage add-ons](manage-add-ons.md)</li><li>[Manage add-on submissions](manage-add-on-submissions.md)</li></ul> |
| Package flights | Get, create, or delete package flights for your apps, and then get, create, or delete submissions for the package flights. For more information about these methods, see the following articles: <ul><li>[Manage package flights](manage-flights.md)</li><li>[Manage package flight submissions](manage-flight-submissions.md)</li></ul> |

<span id="code-samples"></span>

## Code examples

The following articles provide detailed code examples that demonstrate how to use the Microsoft Store submission API in several different programming languages:

* [C# sample: submissions for apps, add-ons, and flights](csharp-code-examples-for-the-windows-store-submission-api.md)
* [C# sample: app submission with game options and trailers](csharp-code-examples-for-submissions-game-options-and-trailers.md)
* [Java sample: submissions for apps, add-ons, and flights](java-code-examples-for-the-windows-store-submission-api.md)
* [Java sample: app submission with game options and trailers](java-code-examples-for-submissions-game-options-and-trailers.md)
* [Python sample: submissions for apps, add-ons, and flights](python-code-examples-for-the-windows-store-submission-api.md)
* [Python sample: app submission with game options and trailers](python-code-examples-for-submissions-game-options-and-trailers.md)

## StoreBroker PowerShell module

As an alternative to calling the Microsoft Store submission API directly, we also provide an open-source PowerShell module which implements a command-line interface on top of the API. This module is called [StoreBroker](https://github.com/Microsoft/StoreBroker). You can use this module to manage your app, flight, and add-on submissions from the command line instead of calling the Microsoft Store submission API directly, or you can simply browse the source to see more examples for how to call this API. The StoreBroker module is actively used within Microsoft as the primary way that many first-party applications are submitted to the Store.

For more information, see our [StoreBroker page on GitHub](https://github.com/Microsoft/StoreBroker).

## Troubleshooting

| Issue      | Resolution                                          |
|---------------|---------------------------------------------|
| After calling the Microsoft Store submission API from PowerShell, the response data for the API is corrupted if you convert it from JSON format to a PowerShell object using the [ConvertFrom-Json](/powershell/module/5.1/microsoft.powershell.utility/ConvertFrom-Json) cmdlet and then back to JSON format using the [ConvertTo-Json](/powershell/module/5.1/microsoft.powershell.utility/ConvertTo-Json) cmdlet. |  By default, the *-Depth* parameter for the [ConvertTo-Json](/powershell/module/5.1/microsoft.powershell.utility/ConvertTo-Json) cmdlet is set to 2 levels of objects, which is too shallow for most of the JSON objects that are returned by the Microsoft Store submission API. When you call the [ConvertTo-Json](/powershell/module/5.1/microsoft.powershell.utility/ConvertTo-Json) cmdlet, set the *-Depth* parameter to a larger number, such as 20. |

## Additional help

If you have questions about the Microsoft Store submission API or need assistance managing your submissions with this API, use the following resources:

* Ask your questions on our [forums](https://social.msdn.microsoft.com/Forums/windowsapps/home?forum=wpsubmit).
* Visit our [support page](https://developer.microsoft.com/windows/support) and request one of the assisted support options for Partner Center. If you are prompted to choose a problem type and category, choose **App submission and certification** and **Submitting an app**, respectively.  

## Related topics

* [Get app data](get-app-data.md)
* [Manage app submissions](manage-app-submissions.md)
* [Manage add-ons](manage-add-ons.md)
* [Manage add-on submissions](manage-add-on-submissions.md)
* [Manage package flights](manage-flights.md)
* [Manage package flight submissions](manage-flight-submissions.md)
