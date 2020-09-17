---
title: Alternate push channels using VAPID in UWP
description: Directions for using alternate push channels with the VAPID protocol from a Windows app
ms.date: 01/10/2017
ms.topic: article
keywords: windows 10, uwp, WinRT API, WNS
localizationpriority: medium
---
# Alternate push channels using VAPID in Windows 
Starting in the Fall Creators Update, Windows apps can use VAPID authentication to send push notifications.  

> [!NOTE]
> These APIs are intended for web browsers that are hosting other websites and creating channels on their behalf.  If you are looking to add webpush notifications to your web app, we recommend you follow the W3C and WhatWG standards for creating a service worker and sending a notification.

## Introduction
The introduction of the web push standard allows websites can act more like apps, sending notifications even when users aren’t on the website.

The VAPID authentication protocol was created to allow websites to authenticate with push servers in a vendor agnostic way. With all vendors using the VAPID protocol, websites can send push notifications without knowing the browser on which it is running. This is a significant improvement over implementing a different push protocol for each platform. 

Windows apps can use VAPID to send push notifications with these advantages, as well. These protocols can save development time for new apps and simplify cross platform support for existing apps. Additionally, enterprise apps or sideloaded apps can now send notifications without registering in the Microsoft Store. Hopefully, this will open up new ways to engage with users across all platforms.  

## Alternate channels 
In UWP, these VAPID channels are called alternate channels and provide similar functionality to a web push channel. They can trigger an app background task to run, enable message encryption, and allow for multiple channels from a single app. For more information about the difference between the different channel types, please see [Choosing the right channel](channel-types.md).

Using alternate channels is a great way to access push notifications if your app can’t use a primary channel or if you want to share code between your website and app. Setting up a channel is easy and familiar to anyone who has either used the web push standard or worked with Windows push notifications before.

## Code example

The basic process of setting up an alternate channel for a Windows app is similar to setting up a primary or secondary channel. First, register for a channel with the [WNS server](windows-push-notification-services--wns--overview.md). Then, register to run as a background task. After the notification is sent and the background task is triggered, handle the event.  

### Get a channel 
To create an alternate channel, the app must provide two pieces of information: the public key for its server and the name of the channel it is creating. The details about the server keys are available in the web push spec, but we recommend using a standard web push library on the server to generate the keys.  

The channel ID is particularly important because an app can create multiple alternate channels. Each channel must be identified by a unique ID that will be included with any notification payloads sent along that channel.  

```csharp
private async void AppCreateVAPIDChannelAsync(string appChannelId, IBuffer applicationServerKey) 
{ 
    // From the spec: applicationServer Key (p256dh):  
    //               An Elliptic curve Diffie–Hellman public key on the P-256 curve 
    //               (that is, the NIST secp256r1 elliptic curve).   
    //               The resulting key is an uncompressed point in ANSI X9.62 format             
    // ChannelId is an app provided value for it to identify the channel later.  
    // case of this app it is from the set { "Football", "News", "Baseball" } 
    PushNotificationChannel webChannel = await PushNotificationChannelManager.GetDefault().CreateRawPushNotificationChannelWithAlternateKeyForApplicationAsync(applicationServerKey, appChannelId); 
 
    //Save the channel  
    AppUpdateChannelMapping(appChannelId, webChannel); 
             
    //Tell our web service that we have a new channel to push notifications to 
    AppPassChannelToSite(webChannel.Uri); 
} 
```
The app sends the channel back up to its server and saves it locally. Saving the channel ID locally allows the app to differentiate between channels and renew channels in order to prevent them from expiring.

Like every other type of push notification channel, web channels can expire. To prevent channels from expiring without your app knowing, create a new channel every time your app is launched.    

### Register for a background task 

Once your app has created an alternate channel, it should register to receive the notifications either in the foreground or the background. The example below registers to use the one-process model to receive the notifications in the background.  

```csharp
var builder = new BackgroundTaskBuilder(); 
builder.Name = "Push Trigger"; 
builder.SetTrigger(new PushNotificationTrigger()); 
BackgroundTaskRegistration task = builder.Register(); 
```
### Receive the notifications 

Once the app has registered to receive the notifications, it needs to be able to process the incoming notifications. Since a single app can register multiple channels, be sure to check the channel ID before processing the notification.  

```csharp
protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args) 
{ 
    base.OnBackgroundActivated(args); 
    var raw = args.TaskInstance.TriggerDetails as RawNotification; 
    switch (raw.ChannelId) 
    { 
        case "Football": 
            AppPostFootballScore(raw.Content); 
            break; 
        case "News": 
            AppProcessNewsItem(raw.Content); 
            break; 
        case "Baseball": 
            AppProcessBaseball(raw.Content); 
            break; 
 
        default: 
            //We don't have the channelID registered, should only happen in the case of a 
            //caching issue on the application server 
            break; 
    }                           
} 
```

Note that if the notification is coming from a primary channel, then the channel ID will not be set.  

## Note on encryption 

You can use whatever encryption scheme you find more useful for your app. In some cases, it is enough to rely on the TLS standard between the server and any Windows device. In other cases, it might make more sense to use the web push encryption scheme, or another scheme of your design.  

If you wish to use another form of encryption, the key is the use the raw.Headers property. It contains all of the encryption headers that were included in the POST request to the push server. From there, your app can use the keys to decrypt the message.  

## Related topics
- [Notification channel types](channel-types.md)
- [Windows Push Notification Services (WNS)](windows-push-notification-services--wns--overview.md)
- [PushNotificationChannel class](/uwp/api/windows.networking.pushnotifications.pushnotificationchannel)
- [PushNotificationChannelManager class](/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanager)