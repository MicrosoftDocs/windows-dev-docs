---
title: Notifications functionality migration
description: This topic contains migration guidance in the notifications feature area.
ms.topic: article
ms.date: 10/07/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, push, notifications
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Notifications functionality migration

This topic contains migration guidance in the notifications feature area.

> [!IMPORTANT]
> Only raw push notifications are currently supported.

## Summary of API and/or feature differences

Push notifications can be broken down into these four separate stages.

| Stage | UWP | Windows App SDK|
|--------|-----|----------------|
| Identity | Partner Center (MSA) | Azure App Registration (AAD) |
| Channel request | Synchronous| Asynchronous<br/>Azure App Registration Id<br/>Retry logic built in (up to 5 retries)  |
| Activation | In-process, PushTrigger\*, COM activation\*  | In-process, PushTrigger\*, COM activation\*, Protocol activation |
| Sending push notifications | Uses login.live.com endpoint to receive an access token | Uses the `https://login.microsoftonline.com/common/oauth2/token` endpoint for token request |

\* Supported for Windows 10, version 2004 (10.0; Build 19041), and later.

## Identity setup

In the Windows App SDK, the push notifications feature uses identity from Azure App Registration (AAD), which removes the requirement of having a Package Family Name (PFN) from Partner Center in order to use push notifications.

* For **a UWP app**, you sign up and register the application in [Windows Store Partner Center](/azure/notification-hubs/notification-hubs-windows-store-dotnet-get-started-wns-push-notification#create-an-app-in-windows-store).
* For **a Windows App SDK app**, you create an Azure account, and create an [Azure App Registration (AAD)](/windows/apps/windows-app-sdk/notifications/push/push-quickstart#configure-your-apps-identity-in-azure-active-directory).

### Channel requests

Channel request are handled asynchronously, and require the GUID as part of the parameter (you receive the GUID from an AAD app registration). You use the GUID for your identity in place of the PFN that a UWP app uses. In case the request runs into a retryable error, the notification platform will attempt multiple retries.

A Windows App SDK app can check the status of a channel request.

## Activation

Similar to UWP, push notifications in the Windows App SDK support background task activation via [**PushNotificationTrigger**](/uwp/api/windows.applicationmodel.background.pushnotificationtrigger) and COM Activation (see [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks)). Support for this is currently available only for packaged apps.

* **A UWP app**, can be activated via an in-process or out-of-process background task.
* For a **packaged Windows App SDK app**, background task registration is handled by the Windows App SDK for **PushNotificationTrigger**.
* If you're migrating from a UWP app to an **unpackaged Windows App SDK app**, then you'll be activated via protocol activation in contrast to activation from a background task.

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
POST /common/oauth2/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded
Cookie: fpc=Ar4SqAsP2l9DsWjyp3SGu3Gjwv2tAQAAAOe6sNgOAAAA
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