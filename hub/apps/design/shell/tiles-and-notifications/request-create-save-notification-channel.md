---
title: How to request, create, and save a notification channel 
description: How to request, create, and save a notification channel
ms.topic: article
ms:assetid: 6C35026D-B162-45a6-8CCB-C2D999E95CEE
ms:mtpsurl: https://msdn.microsoft.com/library/Hh868221(v=Win.10)
ms:contentKeyID: 45725025
ms.date: 09/1/2021
mtps_version: v=Win.10
dev_langs:
- csharp
---

# How to request, create, and save a notification channel 

You can open a channel Uniform Resource Identifier (URI) over which your app can receive push notifications. You can then send the channel to your server which uses it to send push notifications, and close it when you no longer need it. A channel is a unique address that represents a single user on a single device, for a specific app or secondary tile.

You should request a new channel each time that your app is launched, and update the cloud server when the URI changes. For more details, see Remarks.

**Important**  Notification channels automatically expire after 30 days.

## What you need to know

### Technologies

  - Windows Runtime

### Prerequisites

  - Familiarity with push notification and Windows Push Notification Services (WNS) concepts, requirements, and operation. These are discussed in the [Windows Push Notification Services (WNS) overview](.\windows-push-notification-services--wns--overview.md).

## Instructions

### Step 1: Add namespace declarations

Windows.UI.Notifications includes the toast APIs.

``` csharp
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.Networking.PushNotifications;
```

### Step 2: Request a channel URI

This example requests a channel URI. The request is made to the Notification Client Platform, which in turn requests the channel URI from WNS. When the request is complete, the returned value is a [**PushNotificationChannel**](https://msdn.microsoft.com/library/BR241283) object that contains the URI.

``` csharp
PushNotificationChannel channel = null;

try
{
    channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
}

catch (Exception ex)
{ 
    // Could not create a channel. 
}
```

### Step 3: Send the channel URI to your server

The channel URI is packaged in an HTTP POST request and sent to the server.

**Important**  You should send this information to your server in a secure manner. You should require the app to authenticate itself with the server when it transmits the channel URI. Encrypt the information and use a secure protocol such as HTTPS.

 

``` csharp
String serverUrl = "http://www.contoso.com";

// Create the web request.
HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(serverUrl);
webRequest.Method = "POST";
webRequest.ContentType = "application/x-www-form-urlencoded";
byte[] channelUriInBytes = System.Text.Encoding.UTF8.GetBytes("ChannelUri=" + channel.Uri);

// Write the channel URI to the request stream.
Stream requestStream = await webRequest.GetRequestStreamAsync();
requestStream.Write(channelUriInBytes, 0, channelUriInBytes.Length);

try
{
    // Get the response from the server.
    WebResponse response = await webRequest.GetResponseAsync();
    StreamReader requestReader = new StreamReader(response.GetResponseStream());
    String webResponse = requestReader.ReadToEnd();
}

catch (Exception ex)
{
    // Could not send channel URI to server.
}
```

## Remarks

### Requesting channels

You should request a new channel each time your app is invoked, by using the following logic:

1.  Request a channel.
2.  Compare the new channel with your previous channel. If the channel is the same, you don't need to take any further action. Note that this requires your app to store the channel locally each time the app successfully sends it to your service, so that you have the channel to compare against later.
3.  If the channel has changed, send the new channel to your web service. Include error handling logic that always sends a new channel in the following cases:
      - Your app has never sent a channel to the web service before.
      - Your app's last attempt to send the channel to the web service was not successful.

Different calls to the [**CreatePushNotificationChannelForApplicationAsync**](https://msdn.microsoft.com/library/BR241285) method do not always return a different channel. If the channel has not changed since the last call, your app should conserve effort and Internet traffic by not resending that same channel to your service. An app can have multiple valid channel URIs at the same time. Because each unique channel remains valid until it expires, there is no harm in requesting a new channel because it does not affect the expiration time of any previous channels.

By requesting a new channel each time your app is invoked, you maximize your chances of always having access to a valid channel. This is particularly important if it is vital to your tile or toast scenario that content always be live. If you are concerned that a user might not run your app more than once every 30 days, you can implement a background task to execute your channel request code on a regular basis.

### Handling errors in channel requests

The call to the [**CreatePushNotificationChannelForApplicationAsync**](https://msdn.microsoft.com/library/BR241285) method can fail if the Internet is not available. To handle this, add retry logic to the code shown in step 2. We recommend three attempts with a 10-second delay between each unsuccessful attempt. If all three attempts fail, your app should wait until the next time the user launches it to try again.

### Closing channels

Your app can immediately stop the delivery of notifications on all channels by calling the [**PushNotificationChannel.Close**](https://msdn.microsoft.com/library/BR241289) method. While it will not be common for your app to do so, there might be certain scenarios in which you want to stop all notification delivery to your app. For instance, if your app has the concept of user accounts and a user logs out of that app, it is reasonable to expect that the tile no longer shows that user's personal information. To successfully clear the tile of content and stop the delivery of notifications, you should do the following:

1.  Stop all tile updates by calling the [**PushNotificationChannel.Close**](https://msdn.microsoft.com/library/BR241289) method on any of your notification channels that are delivering tile, toast, badge or raw notifications to a user. Calling the **Close** method ensures that no further notifications for that user can be delivered to the client.
2.  Clear the contents of the tile by calling the [**TileUpdater.Clear**](https://msdn.microsoft.com/library/BR208629) method to remove the previous user's data from the tile.