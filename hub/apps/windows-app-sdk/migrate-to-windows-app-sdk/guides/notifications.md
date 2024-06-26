---
title: Push notifications functionality migration
description: This topic contains migration guidance in the push notifications feature area.
ms.topic: article
ms.date: 10/07/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, push, notifications
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Push notifications functionality migration

This topic contains migration guidance in the push notifications feature area. 

> [!IMPORTANT]
> Only raw push notifications and app push notifications are currently supported. Badge push notifications and tile push notifications are not supported. 

## Summary of API and/or feature differences

Push notifications can be broken down into these four separate stages.

| Stage | UWP | Windows App SDK|
|--------|-----|----------------|
| Identity | Partner Center (MSA) | Azure App Registration (AAD) |
| Channel request | Asynchronous| Asynchronous<br/>Azure App Registration Id<br/>Retry logic built in (up to 5 retries)  |
| Activation | In-process, PushTrigger\*, COM activation\*  | In-process, COM activation, ShellExecute |
| Sending push notifications | Uses login.live.com endpoint to receive an access token | Uses the `https://login.microsoftonline.com/{tenantID}/oauth2/token` endpoint for token request |

\* Supported for Windows 10, version 2004 (10.0; Build 19041), and later.

## Identity setup

In the Windows App SDK, the push notifications feature uses identity from Azure App Registration (AAD), which removes the requirement of having a Package Family Name (PFN) from Partner Center in order to use push notifications.

* For a **UWP app**, you sign up and register the application in [Windows Store Partner Center](/azure/notification-hubs/notification-hubs-windows-store-dotnet-get-started-wns-push-notification#create-an-app-in-windows-store).
* For a **Windows App SDK app**, you create an Azure account, and create an [Azure App Registration (AAD)](../../notifications/push-notifications/push-quickstart.md#configure-your-apps-identity-in-azure-active-directory-aad).

### Channel requests

Channel request are handled asynchronously, and require the Azure AppID GUID and Azure tenantID (you receive the Azure AppID and tenant ID from an AAD app registration). You use the Azure AppID for your identity in place of the Package Family Name (PFN) that a UWP app uses. In case the request runs into a retryable error, the notification platform will attempt multiple retries.

A Windows App SDK app can check the status of a channel request.

### Activation

See the Windows App SDK registration and activation steps at [Configure your app to receive push notifications](../../notifications/push-notifications/push-quickstart.md#configure-your-app-to-receive-push-notifications). 

## Sending push notifications

A Windows App SDK app must request the access token from the AAD endpoint, rather than from the MSA endpoint.

### Access Token Request

For **a UWP app**:

```http
POST /accesstoken.srf HTTP/1.1
Host: login.live.com
Content-Type: application/x-www-form-urlencoded
Cookie: MSCC=73.140.231.96-US
Content-Length: 112

grant_type=client_credentials&client_id=<AppID_Here>&client_secret=<Client_Secret_Here>&scope=notify.windows.com
```

For a **Windows App SDK app** (AAD access token request):

```http
POST /{tenantID}/oauth2/v2.0/token Http/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded
Content-Length: 160

grant_type=client_credentials&client_id=<Azure_App_Registration_AppId_Here>&client_secret=<Azure_App_Registration_Secret_Here>&resource=https://wns.windows.com/
```

### HTTP Post to WNS

When it comes to sending a HTTP POST request to WNS, there are no changes from UWP. The access token is still passed in the authorization header.

```http
POST /?token=[ChannelURI] HTTP/1.1
Host: dm3p.notify.windows.com
Content-Type: application/octet-stream
X-WNS-Type: wns/raw
Authorization: Bearer [your access token]
Content-Length: 46

{ Sync: "Hello from the Contoso App Service" }
```

## See Also

* [Windows App SDK and suppported Windows releases](../../support.md)
