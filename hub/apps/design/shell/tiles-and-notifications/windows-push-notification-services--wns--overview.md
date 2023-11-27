---
description: The Windows Push Notification Services (WNS) enables third-party developers to send toast, tile, badge, and raw updates from their own cloud service. This provides a mechanism to deliver new updates to your users in a power-efficient and dependable way.
title: Windows Push Notification Services (WNS) overview
ms.assetid: 2125B09F-DB90-4515-9AA6-516C7E9ACCCD
template: detail.hbs
ms.date: 09/28/2020
ms.topic: article
ms.custom: contperfq1
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Windows Push Notification Services (WNS) overview 

The Windows Push Notification Services (WNS) enable third-party developers to send toast, tile, badge, and raw updates from their own cloud service. This provides a mechanism to deliver new updates to your users in a power-efficient and dependable way.

## How it works

The following diagram shows the complete data flow for sending a push notification. It involves these steps:

1.  Your app requests a push notification channel from WNS.
2.  Windows asks WNS to create a notification channel. This channel is returned to the calling device in the form of a Uniform Resource Identifier (URI).
3.  The notification channel URI is returned by WNS to your app.
4.  Your app sends the URI to your own cloud service. You then store the URI on your own cloud service so that you can access the URI when you send notifications. The URI is an interface between your own app and your own service; it's your responsibility to implement this interface with safe and secure web standards.
5.  When your cloud service has an update to send, it notifies WNS using the channel URI. This is done by issuing an HTTP POST request, including the notification payload, over Secure Sockets Layer (SSL). This step requires authentication.
6.  WNS receives the request and routes the notification to the appropriate device.

![wns data flow diagram for push notification](images/wns-diagram-01.jpg)

## Registering your app and receiving the credentials for your cloud service

Before you can send notifications using WNS, your app must be registered with the Store Dashboard, as described [here](/azure/notification-hubs/notification-hubs-windows-store-dotnet-get-started-wns-push-notification).

## Requesting a notification channel

When an app that is capable of receiving push notifications runs, it must first request a notification channel through the [**CreatePushNotificationChannelForApplicationAsync**](/uwp/api/Windows.Networking.PushNotifications.PushNotificationChannelManager#Windows_Networking_PushNotifications_PushNotificationChannelManager_CreatePushNotificationChannelForApplicationAsync_System_String_). For a full discussion and example code, see [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh465412(v=win.10)). This API returns a channel URI that is uniquely linked to the calling application and its tile, and through which all notification types can be sent.

After the app has successfully created a channel URI, it sends it to its cloud service, together with any app-specific metadata that should be associated with this URI.

### Important notes

-   We do not guarantee that the notification channel URI for an app will always remain the same. We advise that the app requests a new channel every time it runs and updates its service when the URI changes. The developer should never modify the channel URI and should consider it as a black-box string. At this time, channel URIs expire after 30 days. If your Windows 10 app will periodically renew its channel in the background then you can download the [Push and periodic notifications sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Push%20and%20periodic%20notifications%20client-side%20sample%20(Windows%208)) for Windows 8.1 and re-use its source code and/or the pattern it demonstrates.
-   The interface between the cloud service and the client app is implemented by you, the developer. We recommend that the app go through an authentication process with its own service and transmit data over a secure protocol such as HTTPS.
-   It is important that the cloud service always ensures that the channel URI uses the domain "notify.windows.com". The service should never push notifications to a channel on any other domain. If the callback for your app is ever compromised, a malicious attacker could submit a channel URI to spoof WNS. Without inspecting the domain, your cloud service could potentially disclose information to this attacker unknowingly. The subdomain of the channel URI is subject to change and should not be considered when validating the channel URI.
-   If your cloud service attempts to deliver a notification to an expired channel, WNS will return [response code 410](/previous-versions/windows/apps/hh465435(v=win.10)). In response to that code, your service should no longer attempt to send notifications to that URI.

## Authenticating your cloud service

To send a notification, the cloud service must be authenticated through WNS. The first step in this process occurs when you register your app with the Microsoft Store Dashboard. During the registration process, your app is given a Package security identifier (SID) and a secret key. This information is used by your cloud service to authenticate with WNS.

The WNS authentication scheme is implemented using the client credentials profile from the [OAuth 2.0](https://tools.ietf.org/html/draft-ietf-oauth-v2-23) protocol. The cloud service authenticates with WNS by providing its credentials (Package SID and secret key). In return, it receives an access token. This access token allows a cloud service to send a notification. The token is required with every notification request sent to the WNS.

At a high level, the information chain is as follows:

1.  The cloud service sends its credentials to WNS over HTTPS following the OAuth 2.0 protocol. This authenticates the service with WNS.
2.  WNS returns an access token if the authentication was successful. This access token is used in all subsequent notification requests until it expires.

![wns diagram for cloud service authentication](images/wns-diagram-02.jpg)

In the authentication with WNS, the cloud service submits an HTTP request over Secure Sockets Layer (SSL). The parameters are supplied in the "application/x-www-for-urlencoded" format. Supply your Package SID in the "client\_id" field and your secret key in the "client\_secret" field as shown in the following example. For syntax details, see the [access token request](/previous-versions/windows/apps/hh465435(v=win.10)) reference.

> [!NOTE]
> This is just an example, not cut-and-paste code that you can successfully use in your own code. 

``` http
 POST /accesstoken.srf HTTP/1.1
 Content-Type: application/x-www-form-urlencoded
 Host: https://login.live.com
 Content-Length: 211
 
 grant_type=client_credentials&client_id=ms-app%3a%2f%2fS-1-15-2-2972962901-2322836549-3722629029-1345238579-3987825745-2155616079-650196962&client_secret=Vex8L9WOFZuj95euaLrvSH7XyoDhLJc7&scope=notify.windows.com
```

The WNS authenticates the cloud service and, if successful, sends a response of "200 OK". The access token is returned in the parameters included in the body of the HTTP response, using the "application/json" media type. After your service has received the access token, you are ready to send notifications.

The following example shows a successful authentication response, including the access token. For syntax details, see [Push notification service request and response headers](/previous-versions/windows/apps/hh465435(v=win.10)).

``` http
 HTTP/1.1 200 OK   
 Cache-Control: no-store
 Content-Length: 422
 Content-Type: application/json
 
 {
     "access_token":"EgAcAQMAAAAALYAAY/c+Huwi3Fv4Ck10UrKNmtxRO6Njk2MgA=", 
     "token_type":"bearer"
 }
```

### Important notes

-   The OAuth 2.0 protocol supported in this procedure follows draft version V16.
-   The OAuth Request for Comments (RFC) uses the term "client" to refer to the cloud service.
-   There might be changes to this procedure when the OAuth draft is finalized.
-   The access token can be reused for multiple notification requests. This allows the cloud service to authenticate just once to send many notifications. However, when the access token expires, the cloud service must authenticate again to receive a new access token.

## Sending a notification


Using the channel URI, the cloud service can send a notification whenever it has an update for the user.

The access token described above can be reused for multiple notification requests; the cloud server is not required to request a new access token for every notification. If the access token has expired, the notification request will return an error. We recommended that you do not try to re-send your notification more than once if the access token is rejected. If you encounter this error, you will need to request a new access token and resend the notification. For the exact error code, see [Push notification response codes](/previous-versions/windows/apps/hh465435(v=win.10)).

1.  The cloud service makes an HTTP POST to the channel URI. This request must be made over SSL and contains the necessary headers and the notification payload. The authorization header must include the acquired access token for authorization.

    An example request is shown here. For syntax details, see [Push notification response codes](/previous-versions/windows/apps/hh465435(v=win.10)).

    For details on composing the notification payload, see [Quickstart: Sending a push notification](/previous-versions/windows/apps/hh868252(v=win.10)). The payload of a tile, toast, or badge push notification is supplied as XML content that adheres to their respective defined [Adaptive tiles schema](adaptive-tiles-schema.md) or [Legacy tiles schema](/uwp/schemas/tiles/tiles-xml-schema-portal). The payload of a raw notification does not have a specified structure. It is strictly app-defined.

    ``` http
     POST https://cloud.notify.windows.com/?token=AQE%bU%2fSjZOCvRjjpILow%3d%3d HTTP/1.1
     Content-Type: text/xml
     X-WNS-Type: wns/tile
     Authorization: Bearer EgAcAQMAAAAALYAAY/c+Huwi3Fv4Ck10UrKNmtxRO6Njk2MgA=
     Host: cloud.notify.windows.com
     Content-Length: 24

     <body>
     ....
    ```

2.  WNS responds to indicate that the notification has been received and will be delivered at the next available opportunity. However, WNS does not provide end-to-end confirmation that your notification has been received by the device or application.

This diagram illustrates the data flow:

![wns diagram for sending a notification](images/wns-diagram-03.jpg)

### Important notes

-   WNS does not guarantee the reliability or latency of a notification.
-   Notifications should never include confidential, sensitive, or personal data.
-   To send a notification, the cloud service must first authenticate with WNS and receive an access token.
-   An access token only allows a cloud service to send notifications to the single app for which the token was created. One access token cannot be used to send notifications across multiple apps. Therefore, if your cloud service supports multiple apps, it must provide the correct access token for the app when pushing a notification to each channel URI.
-   When the device is offline, by default WNS will store one of each notification type (tile, badge, toast) for each channel URI and no raw notifications.  
-   In scenarios where the notification content is personalized to the user, WNS recommends that the cloud service immediately send those updates when those are received. Examples of this scenario include social media feed updates, instant communication invitations, new message notifications, or alerts. As an alternative, you can have scenarios in which the same generic update is frequently delivered to a large subset of your users; for example, weather, stock, and news updates. WNS guidelines specify that the frequency of these updates should be at most one every 30 minutes. The end user or WNS may determine more frequent routine updates to be abusive.
-   Windows Notification Platform maintains a periodic data connection with WNS to keep the socket alive and healthy. If there are no applications requesting or using notification channels then the socket will not be created.

## Expiration of tile and badge notifications

By default, tile and badge notifications expire three days after being downloaded. When a notification expires, the content is removed from the tile or queue and is no longer shown to the user. It's a best practice to set an expiration (using a time that makes sense for your app) on all tile and badge notifications so that your tile's content doesn't persist longer than it is relevant. An explicit expiration time is essential for content with a defined lifespan. This also assures the removal of stale content if your cloud service stops sending notifications, or if the user disconnects from the network for an extended period.

Your cloud service can set an expiration for each notification by setting the X-WNS-TTL HTTP header to specify the time (in seconds) that your notification will remain valid after it is sent. For more information, see [Push notification service request and response headers](/previous-versions/windows/apps/hh465435(v=win.10)).

For example, during a stock market's active trading day, you can set the expiration for a stock price update to twice that of your sending interval (such as one hour after receipt if you are sending notifications every half-hour). As another example, a news app might determine that one day is an appropriate expiration time for a daily news tile update.

## Push notifications and battery saver

Battery saver extends battery life by limiting background activity on the device. Windows 10 lets the user set battery saver to turn on automatically when the battery drops below a specified threshold. When battery saver is on, the receipt of push notifications is disabled to save energy. But there are a couple exceptions to this. The following Windows 10 battery saver settings (found in the **Settings** app) allow your app to receive push notifications even when battery saver is on.

-   **Allow push notifications from any app while in battery saver**: This setting lets all apps receive push notifications while battery saver is on. Note that this setting applies only to Windows 10 for desktop editions (Home, Pro, Enterprise, and Education).
-   **Always allowed**: This setting lets specific apps run in the background while battery saver is on - including receiving push notifications. This list is maintained manually by the user.

There is no way to check the state of these two settings, but you can check the state of battery saver. In Windows 10, use the [**EnergySaverStatus**](/uwp/api/Windows.System.Power.PowerManager.EnergySaverStatus) property to check battery saver state. Your app can also use the [**EnergySaverStatusChanged**](/uwp/api/Windows.System.Power.PowerManager.EnergySaverStatusChanged) event to listen for changes to battery saver.

If your app depends heavily on push notifications, we recommend notifying users that they may not receive notifications while battery saver is on and to make it easy for them to adjust **battery saver settings**. Using the battery saver settings URI scheme in Windows 10, `ms-settings:batterysaver-settings`, you can provide a convenient link to the Settings app.

> [!TIP]
> When notifying the user about battery saver settings, we recommend providing a way to suppress the message in the future. For example, the `dontAskMeAgainBox` checkbox in the following example persists the user's preference in [**LocalSettings**](/uwp/api/Windows.Storage.ApplicationData.LocalSettings).

Here's an example of how to check whether battery saver is turned on in Windows 10. This example notifies the user and launches the Settings app to **battery saver settings**. The `dontAskAgainSetting` lets the user suppress the message if they don't want to be notified again.

```csharp
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.System.Power;
...
...
async public void CheckForEnergySaving()
{
   //Get reminder preference from LocalSettings
   bool dontAskAgain;
   var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
   object dontAskSetting = localSettings.Values["dontAskAgainSetting"];
   if (dontAskSetting == null)
   {  // Setting does not exist
      dontAskAgain = false;
   }
   else
   {  // Retrieve setting value
      dontAskAgain = Convert.ToBoolean(dontAskSetting);
   }
   
   // Check if battery saver is on and that it's okay to raise dialog
   if ((PowerManager.EnergySaverStatus == EnergySaverStatus.On)
         && (dontAskAgain == false))
   {
      // Check dialog results
      ContentDialogResult dialogResult = await saveEnergyDialog.ShowAsync();
      if (dialogResult == ContentDialogResult.Primary)
      {
         // Launch battery saver settings (settings are available only when a battery is present)
         await Launcher.LaunchUriAsync(new Uri("ms-settings:batterysaver-settings"));
      }

      // Save reminder preference
      if (dontAskAgainBox.IsChecked == true)
      {  // Don't raise dialog again
         localSettings.Values["dontAskAgainSetting"] = "true";
      }
   }
}
```

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Storage.h>
#include <winrt/Windows.System.h>
#include <winrt/Windows.System.Power.h>
#include <winrt/Windows.UI.Xaml.h>
#include <winrt/Windows.UI.Xaml.Controls.h>
#include <winrt/Windows.UI.Xaml.Navigation.h>
using namespace winrt;
using namespace winrt::Windows::Foundation;
using namespace winrt::Windows::Storage;
using namespace winrt::Windows::System;
using namespace winrt::Windows::System::Power;
using namespace winrt::Windows::UI::Xaml;
using namespace winrt::Windows::UI::Xaml::Controls;
using namespace winrt::Windows::UI::Xaml::Navigation;
...
winrt::fire_and_forget CheckForEnergySaving()
{
    // Get reminder preference from LocalSettings.
    bool dontAskAgain{ false };
    auto localSettings = ApplicationData::Current().LocalSettings();
    IInspectable dontAskSetting = localSettings.Values().Lookup(L"dontAskAgainSetting");
    if (!dontAskSetting)
    {
        // Setting doesn't exist.
        dontAskAgain = false;
    }
    else
    {
        // Retrieve setting value
        dontAskAgain = winrt::unbox_value<bool>(dontAskSetting);
    }

    // Check whether battery saver is on, and whether it's okay to raise dialog.
    if ((PowerManager::EnergySaverStatus() == EnergySaverStatus::On) && (!dontAskAgain))
    {
        // Check dialog results.
        ContentDialogResult dialogResult = co_await saveEnergyDialog().ShowAsync();
        if (dialogResult == ContentDialogResult::Primary)
        {
            // Launch battery saver settings
            // (settings are available only when a battery is present).
            co_await Launcher::LaunchUriAsync(Uri(L"ms-settings:batterysaver-settings"));
        }

        // Save reminder preference.
        if (dontAskAgainBox().IsChecked())
        {
            // Don't raise the dialog again.
            localSettings.Values().Insert(L"dontAskAgainSetting", winrt::box_value(true));
        }
    }
}
```

This is the XAML for the [**ContentDialog**](/uwp/api/Windows.UI.Xaml.Controls.ContentDialog) featured in this example.

```xaml
<ContentDialog x:Name="saveEnergyDialog"
               PrimaryButtonText="Open battery saver settings"
               SecondaryButtonText="Ignore"
               Title="Battery saver is on."> 
   <StackPanel>
      <TextBlock TextWrapping="WrapWholeWords">
         <LineBreak/><Run>Battery saver is on and you may 
          not receive push notifications.</Run><LineBreak/>
         <LineBreak/><Run>You can choose to allow this app to work normally
         while in battery saver, including receiving push notifications.</Run>
         <LineBreak/>
      </TextBlock>
      <CheckBox x:Name="dontAskAgainBox" Content="OK, got it."/>
   </StackPanel>
</ContentDialog>
```

## Related topics

* [Send a local tile notification](sending-a-local-tile-notification.md)
* [Quickstart: Sending a push notification](/previous-versions/windows/apps/hh868252(v=win.10))
* [How to update a badge through push notifications](/previous-versions/windows/apps/hh465450(v=win.10))
* [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh465412(v=win.10))
* [How to intercept notifications for running applications](/previous-versions/windows/apps/jj709907(v=win.10))
* [How to authenticate with the Windows Push Notification Service (WNS)](/previous-versions/windows/apps/hh465407(v=win.10))
* [Push notification service request and response headers](/previous-versions/windows/apps/hh465435(v=win.10))
* [Guidelines and checklist for push notifications]()
* [Raw notifications](/previous-versions/windows/apps/hh761488(v=win.10))
