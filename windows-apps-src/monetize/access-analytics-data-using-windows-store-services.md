---
author: mcleanbyron
ms.assetid: 4BF9EF21-E9F0-49DB-81E4-062D6E68C8B1
description: Use the Microsoft Store analytics API to programmatically retrieve analytics data for apps that are registered to your or your organization''s Windows Dev Center account.
title: Access analytics data using Store services
ms.author: mcleans
ms.date: 08/03/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Store services, Microsoft Store analytics API
ms.localizationpriority: medium
---

# Access analytics data using Store services

Use the *Microsoft Store analytics API* to programmatically retrieve analytics data for apps that are registered to your or your organization's Windows Dev Center account. This API enables you to retrieve data for app and add-on (also known as in-app product or IAP) acquisitions, errors, app ratings and reviews. This API uses Azure Active Directory (Azure AD) to authenticate the calls from your app or service.

The following steps describe the end-to-end process:

1.  Make sure that you have completed all the [prerequisites](#prerequisites).
2.  Before you call a method in the Microsoft Store analytics API, [obtain an Azure AD access token](#obtain-an-azure-ad-access-token). After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store analytics API before the token expires. After the token expires, you can generate a new token.
3.  [Call the Microsoft Store analytics API](#call-the-windows-store-analytics-api).

<span id="prerequisites" />
## Step 1: Complete prerequisites for using the Microsoft Store analytics API

Before you start writing code to call the Microsoft Store analytics API, make sure that you have completed the following prerequisites.

* You (or your organization) must have an Azure AD directory and you must have [Global administrator](http://go.microsoft.com/fwlink/?LinkId=746654) permission for the directory. If you already use Office 365 or other business services from Microsoft, you already have Azure AD directory. Otherwise, you can [create a new Azure AD in Dev Center](../publish/associate-azure-ad-with-dev-center.md#create-a-brand-new-azure-ad-to-associate-with-your-dev-center-account) for no additional charge.

* You must associate an Azure AD application with your Dev Center account, retrieve the tenant ID and client ID for the application and generate a key. The Azure AD application represents the app or service from which you want to call the Microsoft Store analytics API. You need the tenant ID, client ID and key to obtain an Azure AD access token that you pass to the API.
    > [!NOTE]
    > You only need to perform this task one time. After you have the tenant ID, client ID and key, you can reuse them any time you need to create a new Azure AD access token.

To associate an Azure AD application with your Dev Center account and retrieve the required values:

1.  In Dev Center, go to your **Account settings**, click **Manage users**, and [associate your organization's Dev Center account with your organization's Azure AD directory](../publish/associate-azure-ad-with-dev-center.md).

2.  In the **Manage users** page, click **Add Azure AD applications**, add the Azure AD application that represents the app or service that you will use to access analytics data for your Dev Center account, and assign it the **Manager** role. If this application already exists in your Azure AD directory, you can select it on the **Add Azure AD applications** page to add it to your Dev Center account. Otherwise, you can create a new Azure AD application on the **Add Azure AD applications** page. For more information, see [Add Azure AD applications to your Dev Center account](../publish/add-users-groups-and-azure-ad-applications.md#azure-ad-applications).

3.  Return to the **Manage users** page, click the name of your Azure AD application to go to the application settings, and copy down the **Tenant ID** and **Client ID** values.

4. Click **Add new key**. On the following screen, copy down the **Key** value. You won't be able to access this info again after you leave this page. For more information, see [Manage keys for an Azure AD application](../publish/add-users-groups-and-azure-ad-applications.md#manage-keys).

<span id="obtain-an-azure-ad-access-token" />
## Step 2: Obtain an Azure AD access token

Before you call any of the methods in the Microsoft Store analytics API, you must first obtain an Azure AD access token that you pass to the **Authorization** header of each method in the API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can refresh the token so you can continue to use it in further calls to the API.

To obtain the access token, follow the instructions in [Service to Service Calls Using Client Credentials](https://azure.microsoft.com/documentation/articles/active-directory-protocols-oauth-service-to-service/) to send an HTTP POST to the ```https://login.microsoftonline.com/<tenant_id>/oauth2/token``` endpoint. Here is a sample request.

```
POST https://login.microsoftonline.com/<tenant_id>/oauth2/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded; charset=utf-8

grant_type=client_credentials
&client_id=<your_client_id>
&client_secret=<your_client_secret>
&resource=https://manage.devcenter.microsoft.com
```

For the *tenant\_id* value in the POST URI and the *client\_id* and *client\_secret* parameters, specify the tenant ID, client ID and the key for your application that you retrieved from Dev Center in the previous section. For the *resource* parameter, you must specify ```https://manage.devcenter.microsoft.com```.

After your access token expires, you can refresh it by following the instructions [here](https://azure.microsoft.com/documentation/articles/active-directory-protocols-oauth-code/#refreshing-the-access-tokens).

<span id="call-the-windows-store-analytics-api" />
## Step 3: Call the Microsoft Store analytics API

After you have an Azure AD access token, you are ready to call the Microsoft Store analytics API. For information about the syntax of each method, see the following articles. You must pass the access token to the **Authorization** header of each method.

| Scenario       | Methods      |
|---------------|--------------------|
| Acquisitions, conversions, and installs |  <ul><li>[Get app acquisitions](get-app-acquisitions.md)</li><li>[Get app acquisition funnel data](get-acquisition-funnel-data.md)</li><li>[Get app conversions by channel](get-app-conversions-by-channel.md)</li><li>[Get add-on acquisitions](get-in-app-acquisitions.md)</li><li>[Get add-on conversions by channel](get-add-on-conversions-by-channel.md)</li><li>[Get app installs](get-app-installs.md)</li></ul> |
| App errors | <ul><li>[Get error reporting data](get-error-reporting-data.md)</li><li>[Get details for an error in your app](get-details-for-an-error-in-your-app.md)</li><li>[Get the stack trace for an error in your app](get-the-stack-trace-for-an-error-in-your-app.md)</li></ul> |
| Ratings and reviews | <ul><li>[Get app ratings](get-app-ratings.md)</li><li>[Get app reviews](get-app-reviews.md)</li></ul> |
| In-app ads and ad campaigns | <ul><li>[Get ad performance data](get-ad-performance-data.md)</li><li>[Get ad campaign performance data](get-ad-campaign-performance-data.md)</li></ul> |

The following additional methods are available for use by developer accounts that belong to the [Windows Hardware Dev Center program](https://msdn.microsoft.com/windows/hardware/drivers/dashboard/get-started-with-the-hardware-dashboard).

| Scenario       | Methods      |
|---------------|--------------------|
| Errors in Windows 10 drivers (for IHVs) |  <ul><li>[Get error reporting data for Windows 10 drivers](get-error-reporting-data-for-windows-10-drivers.md)</li><li>[Get details for a Windows 10 driver error](get-details-for-a-windows-10-driver-error.md)</li><li>[Download the CAB file for a Windows 10 driver error](download-the-cab-file-for-a-windows-10-driver-error.md)</li></ul> |
| Errors in Windows 7/Windows 8.x drivers (for IHVs) |  <ul><li>[Get error reporting data for Windows 7 and Windows 8.x drivers](get-error-reporting-data-for-windows-7-and-windows-8.x-drivers.md)</li><li>[Get details for a Windows 7 or Windows 8.x driver error](get-details-for-a-windows-7-or-windows-8.x-driver-error.md)</li><li>[Download the CAB file for a Windows 7 or Windows 8.x driver error](download-the-cab-file-for-a-windows-7-or-windows-8.x-driver-error.md)</li></ul> |
| Hardware errors (for OEMs) |  <ul><li>[Get OEM hardware error reporting data](get-oem-hardware-error-reporting-data.md)</li><li>[Get details for an OEM hardware error](get-details-for-an-oem-hardware-error.md)</li><li>[Download the CAB file for an OEM hardware error](download-the-cab-file-for-an-oem-hardware-error.md)</li></ul> |

## Code example

The following code example demonstrates how to obtain an Azure AD access token and call the Microsoft Store analytics API from a C# console app. To use this code example, assign the *tenantId*, *clientId*, *clientSecret*, and *appID* variables to the appropriate values for your scenario. This example requires the [Json.NET package](http://www.newtonsoft.com/json) from Newtonsoft to deserialize the JSON data returned by the Microsoft Store analytics API.

> [!div class="tabbedCodeSnippets"]
[!code-cs[AnalyticsApi](./code/StoreServicesExamples_Analytics/cs/Program.cs#AnalyticsApiExample)]

## Error responses

The Microsoft Store analytics API returns error responses in a JSON object that contains error codes and messages. The following example demonstrates an error response caused by an invalid parameter.

```json
{
    "code":"BadRequest",
    "data":[],
    "details":[],
    "innererror":{
        "code":"InvalidQueryParameters",
        "data":[
            "top parameter cannot be more than 10000"
        ],
        "details":[],
        "message":"One or More Query Parameters has invalid values.",
        "source":"AnalyticsAPI"
    },
    "message":"The calling client sent a bad request to the service.",
    "source":"AnalyticsAPI"
}
```
