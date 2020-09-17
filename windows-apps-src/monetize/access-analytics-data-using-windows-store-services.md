---
ms.assetid: 4BF9EF21-E9F0-49DB-81E4-062D6E68C8B1
description: Use the Microsoft Store analytics API to programmatically retrieve analytics data for apps that are registered to your or your organization''s Windows Partner Center account.
title: Access analytics data using Store services
ms.date: 03/06/2019
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API
ms.localizationpriority: medium
ms.custom: RS5
---
# Access analytics data using Store services

Use the *Microsoft Store analytics API* to programmatically retrieve analytics data for apps that are registered to your or your organization's Windows Partner Center account. This API enables you to retrieve data for app and add-on (also known as in-app product or IAP) acquisitions, errors, app ratings and reviews. This API uses Azure Active Directory (Azure AD) to authenticate the calls from your app or service.

The following steps describe the end-to-end process:

1.  Make sure that you have completed all the [prerequisites](#prerequisites).
2.  Before you call a method in the Microsoft Store analytics API, [obtain an Azure AD access token](#obtain-an-azure-ad-access-token). After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store analytics API before the token expires. After the token expires, you can generate a new token.
3.  [Call the Microsoft Store analytics API](#call-the-windows-store-analytics-api).

<span id="prerequisites" />

## Step 1: Complete prerequisites for using the Microsoft Store analytics API

Before you start writing code to call the Microsoft Store analytics API, make sure that you have completed the following prerequisites.

* You (or your organization) must have an Azure AD directory and you must have [Global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permission for the directory. If you already use Microsoft 365 or other business services from Microsoft, you already have Azure AD directory. Otherwise, you can [create a new Azure AD in Partner Center](../publish/associate-azure-ad-with-partner-center.md#create-a-brand-new-azure-ad-to-associate-with-your-partner-center-account) for no additional charge.

* You must associate an Azure AD application with your Partner Center account, retrieve the tenant ID and client ID for the application and generate a key. The Azure AD application represents the app or service from which you want to call the Microsoft Store analytics API. You need the tenant ID, client ID and key to obtain an Azure AD access token that you pass to the API.
    > [!NOTE]
    > You only need to perform this task one time. After you have the tenant ID, client ID and key, you can reuse them any time you need to create a new Azure AD access token.

To associate an Azure AD application with your Partner Center account and retrieve the required values:

1.  In Partner Center, [associate your organization's Partner Center account with your organization's Azure AD directory](../publish/associate-azure-ad-with-partner-center.md).

2.  Next, from the **Users** page in the **Account settings** section of Partner Center, [add the Azure AD application](../publish/add-users-groups-and-azure-ad-applications.md#add-azure-ad-applications-to-your-partner-center-account) that represents the app or service that you will use to access analytics data for your Partner Center account. Make sure you assign this application the **Manager** role. If the application doesn't exist yet in your Azure AD directory, you can [create a new Azure AD application in Partner Center](../publish/add-users-groups-and-azure-ad-applications.md#create-a-new-azure-ad-application-account-in-your-organizations-directory-and-add-it-to-your-partner-center-account).

3.  Return to the **Users** page, click the name of your Azure AD application to go to the application settings, and copy down the **Tenant ID** and **Client ID** values.

4. Click **Add new key**. On the following screen, copy down the **Key** value. You won't be able to access this info again after you leave this page. For more information, see [Manage keys for an Azure AD application](../publish/add-users-groups-and-azure-ad-applications.md#manage-keys).

<span id="obtain-an-azure-ad-access-token" />

## Step 2: Obtain an Azure AD access token

Before you call any of the methods in the Microsoft Store analytics API, you must first obtain an Azure AD access token that you pass to the **Authorization** header of each method in the API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can refresh the token so you can continue to use it in further calls to the API.

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

After your access token expires, you can refresh it by following the instructions [here](/azure/active-directory/azuread-dev/v1-protocols-oauth-code#refreshing-the-access-tokens).

<span id="call-the-windows-store-analytics-api" />

## Step 3: Call the Microsoft Store analytics API

After you have an Azure AD access token, you are ready to call the Microsoft Store analytics API. You must pass the access token to the **Authorization** header of each method.

### Methods for UWP apps and games
The following methods are available for apps and games acquisitions and add-on acquisitions: 

* [Get acquisitions data for your games and apps](acquisitions-data.md)
* [Get add-on acquisitions data for your games and apps](add-on-acquisitions-data.md)

### Methods for UWP apps 

The following analytics methods are available for UWP apps in Partner Center.

| Scenario       | Methods      |
|---------------|--------------------|
| Acquisitions, conversions, installs, and usage |  <ul><li>[Get app acquisitions](get-app-acquisitions.md) (legacy)</li><li>[Get app acquisition funnel data](get-acquisition-funnel-data.md) (legacy)</li><li>[Get app conversions by channel](get-app-conversions-by-channel.md)</li><li>[Get add-on acquisitions](get-in-app-acquisitions.md)</li><li>[Get subscription add-on acquisitions](get-subscription-acquisitions.md)</li><li>[Get add-on conversions by channel](get-add-on-conversions-by-channel.md)</li><li>[Get app installs](get-app-installs.md)</li><li>[Get daily app usage](get-app-usage-daily.md)</li><li>[Get monthly app usage](get-app-usage-monthly.md)</li></ul> |
| App errors | <ul><li>[Get error reporting data](get-error-reporting-data.md)</li><li>[Get details for an error in your app](get-details-for-an-error-in-your-app.md)</li><li>[Get the stack trace for an error in your app](get-the-stack-trace-for-an-error-in-your-app.md)</li><li>[Download the CAB file for an error in your app](download-the-cab-file-for-an-error-in-your-app.md)</li></ul> |
| Insights | <ul><li>[Get insights data for your app](get-insights-data-for-your-app.md)</li></ul>  |
| Ratings and reviews | <ul><li>[Get app ratings](get-app-ratings.md)</li><li>[Get app reviews](get-app-reviews.md)</li></ul> |
| In-app ads and ad campaigns | <ul><li>[Get ad performance data](get-ad-performance-data.md)</li><li>[Get ad campaign performance data](get-ad-campaign-performance-data.md)</li></ul> |

### Methods for desktop applications

The following analytics methods are available for use by developer accounts that belong to the [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program).

| Scenario       | Methods      |
|---------------|--------------------|
| Installs |  <ul><li>[Get desktop application installs](get-desktop-app-installs.md)</li></ul> |
| Blocks |  <ul><li>[Get upgrade blocks for your desktop application](get-desktop-block-data.md)</li><li>[Get upgrade block details for your desktop application](get-desktop-block-data-details.md)</li></ul> |
| Application errors |  <ul><li>[Get error reporting data for your desktop application](get-desktop-application-error-reporting-data.md)</li><li>[Get details for an error in your desktop application](get-details-for-an-error-in-your-desktop-application.md)</li><li>[Get the stack trace for an error in your desktop application](get-the-stack-trace-for-an-error-in-your-desktop-application.md)</li><li>[Download the CAB file for an error in your desktop application](download-the-cab-file-for-an-error-in-your-desktop-application.md)</li></ul> |
| Insights | <ul><li>[Get insights data for your desktop application](get-insights-data-for-your-desktop-app.md)</li></ul>  |

### Methods for Xbox Live services

The following additional methods are available for use by developer accounts with games that use [Xbox Live services](/gaming/xbox-live/developer-program-overview.md).

| Scenario       | Methods      |
|---------------|--------------------|
| General analytics |  <ul><li>[Get Xbox Live analytics data](get-xbox-live-analytics.md)</li><li>[Get Xbox Live achievements data](get-xbox-live-achievements-data.md)</li><li>[Get Xbox Live concurrent usage data](get-xbox-live-concurrent-usage-data.md)</li></ul> |
| Health analytics |  <ul><li>[Get Xbox Live health data](get-xbox-live-health-data.md)</li></ul> |
| Community analytics |  <ul><li>[Get Xbox Live Game Hub data](get-xbox-live-game-hub-data.md)</li><li>[Get Xbox Live club data](get-xbox-live-club-data.md)</li><li>[Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)</li></ul>  |

### Methods for hardware and drivers

Developer accounts that belong to the [Windows hardware dashboard program](/windows-hardware/drivers/dashboard/get-started-with-the-hardware-dashboard) have access to an additional set of methods for retrieving analytics data for hardware and drivers. For more information, see [Hardware dashboard API](/windows-hardware/drivers/dashboard/dashboard-api).

## Code example

The following code example demonstrates how to obtain an Azure AD access token and call the Microsoft Store analytics API from a C# console app. To use this code example, assign the *tenantId*, *clientId*, *clientSecret*, and *appID* variables to the appropriate values for your scenario. This example requires the [Json.NET package](https://www.newtonsoft.com/json) from Newtonsoft to deserialize the JSON data returned by the Microsoft Store analytics API.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Analytics/cs/Program.cs" id="AnalyticsApiExample":::

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
