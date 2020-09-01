---
ms.assetid: 2CC2E526-DACB-4008-9539-DA3D0C190290
description: A quick overview of the networking technologies available for a UWP developer, with suggestions on how to choose the technologies that are right for your app.
title: Which networking technology?'
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Which networking technology?


A quick overview of the networking technologies available for a UWP developer, with suggestions on how to choose the technologies that are right for your app.

## Sockets

Use [Sockets](sockets.md) when you are communicating with another device and want to use your own protocol.

Two implementations of sockets are available for Universal Windows Platform (UWP) developers: [**Windows.Networking.Sockets**](/uwp/api/Windows.Networking.Sockets), and [Winsock](/windows/desktop/WinSock/windows-sockets-start-page-2). If you are writing new code, then Windows.Networking.Sockets has the advantage of being a modern API, designed for use by UWP developers. If you are using cross-platform networking libraries or other existing Winsock code, or prefer the Winsock API, then use that.

### When to use sockets

-   Both sockets implementations enable you to communicate with other devices using protocols of your own choice, using TCP or UDP.

-   Choose the sockets API that best meets your needs based on experience and any existing code you might be using.

### When not to use sockets

-   Don't implement your own HTTP(S) stack using sockets. Use [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) instead.
-   If WebSockets (the [**StreamWebSocket**](/uwp/api/Windows.Networking.Sockets.StreamWebSocket) and [**MessageWebSocket**](/uwp/api/Windows.Networking.Sockets.MessageWebSocket) classes) meet your communications needs (TCP to/from a web server), consider using them rather than spend your own time and development resources implementing similar functionality with sockets.

## Websockets

The [WebSockets](websockets.md) protocol defines a mechanism for fast, secure two-way communication between a client and a server over the web. Data is transferred immediately over a full-duplex single socket connection, allowing messages to be sent and received from both endpoints in real time. WebSockets are ideal for use in real-time gaming where instant social network notifications and up-to-date displays of information (like game statistics ) need to be secure and use fast data transfer. UWP developers can use the [**StreamWebSocket**](/uwp/api/Windows.Networking.Sockets.StreamWebSocket) and [**MessageWebSocket**](/uwp/api/Windows.Networking.Sockets.MessageWebSocket) classes to connect with servers that support the Websocket protocol.

### When to use Websockets

-   When you want to send and receive data on an ongoing basis between a device and a server.

### When not to use Websockets

-   If you are sending or receiving data infrequently, you might find it simpler to make individual HTTP requests from the device to the server, rather than establish and maintain a WebSocket connection.
-   WebSockets may not be suitable for very high-volume situations. Consider modeling your data flows and simulating your traffic through WebSockets before committing to using them in your design.

## HttpClient

Use [HttpClient](httpclient.md) (and the rest of the [**Windows.Web.Http**](/uwp/api/Windows.Web.Http) namespace API) when you are using HTTP(S) to communicate with a web service or a web server.

### When to use HttpClient

-   When using HTTP(S) to communicate with web services.
-   When uploading or downloading a small number of smaller files.
-   If WebSockets (the [**StreamWebSocket**](/uwp/api/Windows.Networking.Sockets.StreamWebSocket) and [**MessageWebSocket**](/uwp/api/Windows.Networking.Sockets.MessageWebSocket) classes) meet your communications needs (TCP to/from a web server), and the web server in question supports WebSockets, consider using them rather than spend your own time and development resources implementing similar functionality with HttpClient.
-   When you are streaming content over the network.

### When not to use HttpClient

-   If you are transferring large files, or large numbers of files, consider using background transfers instead.
-   If you want to be able to restrict upload/download limits based on connection type, or if you want to save progress and resume upload/download after an interruption, you must use background transfers.
-   If you are communicating between two devices and neither one is designed to act as an HTTP(S) server, you should use sockets. Do not attempt to implement your own HTTP server and use [HttpClient](httpclient.md) to communicate with it.

## Background transfers

Use the [background transfer API](background-transfers.md) when you want to reliably transfer files over the network. The background transfer API provides advanced upload and download features that run in the background during app suspension and persist beyond app termination. The API monitors network status and automatically suspends and resumes transfers when connectivity is lost, and transfers are also Data Sense-aware and Battery Sense-aware, meaning that download activity adjusts based on your current connectivity and device battery status. These capabilities are essential when your app is running on mobile or battery-powered devices. The API is ideal for uploading and downloading large files using HTTP(S). FTP is also supported, but only for downloads.

A new background transfer feature in Windows 10 is the ability to trigger post-processing when a file transfer has completed, so that you can update local catalogs, activate other apps, or notify the user when a download is complete.

### When to use background transfers

-   Use background transfers to reliably transfer large files, or large numbers of files.
-   Use background transfers with background transfer completion groups when you want to post-process file transfers with a background task.
-   Use background transfers if you want to be able to resume a transfer in progress after a network interruption.
-   Use background transfers if you want to be able to change transfer behavior based on network conditions like being on a metered data plan.

### When not to use background transfers

-   If you are transferring a small number of small files, and you don't need to do any post-processing when the transfer is complete, consider using [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) PUT or POST methods.
-   If you want to stream data and use it locally as it arrives, use [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient).

## Additional network-related technologies

### Connection quality

The [**Windows.Networking.Connectivity**](/uwp/api/Windows.Networking.Connectivity) API enables you to access network connectivity, cost, and usage information. For more information about using this API, see [Accessing network connection state and managing network costs](/previous-versions/windows/apps/hh452983(v=win.10))

### DNS Service Discovery

The [**Windows.Networking.ServiceDiscovery.Dnssd**](/uwp/api/Windows.Networking.ServiceDiscovery.Dnssd) API enables you to advertise a network service to other devices on the network using the DNS-SD protocol described in IETF [RFC 2782](https://www.rfc-archive.org/getrfc.php?rfc=2782).

### Communicating over Bluetooth

Among other things, the [**Windows.Devices.Bluetooth**](/uwp/api/Windows.Devices.Bluetooth) API enables you to use Bluetooth to connect to other devices and transfer data. For more information, see [Send or receive files with RFCOMM](../devices-sensors/send-or-receive-files-with-rfcomm.md).

### Push notifications (WNS)

The [**Windows.Networking.PushNotifications**](/uwp/api/Windows.Networking.PushNotifications) API enables you to use the Windows Notification Service (WNS) to receive push notifications over the network. For more information about using this API, see [Windows Push Notification Services (WNS) overview](../design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview.md)

### Near field communications

The [**Windows.Networking.Proximity**](/uwp/api/Windows.Networking.Proximity) API enables you to use near-field communications for apps that use proximity or tap with devices to enable easy data transfer. For more information about using this API, see [Supporting proximity and tapping](/previous-versions/windows/apps/hh465229(v=win.10)).

### RSS/Atom feeds

The [**Windows.Web.Syndication**](/uwp/api/Windows.Web.Syndication) API enables you to manage syndication feeds using RSS and Atom formats. For more information about using this API, see [RSS/Atom feeds](web-feeds.md).

### Wi-Fi enumeration and connection control

The [**Windows.Devices.WiFi**](/uwp/api/Windows.Devices.WiFi) API enables you to enumerate Wi-Fi adapters, scan for available Wi-Fi networks, and connect an adapter to a network.

### Radio control

The [**Windows.Devices.Radios**](/uwp/api/Windows.Devices.Radios) API allows you to find and control radios on the local device, including Wi-Fi and Bluetooth.

### Wi-Fi Direct

The [**Windows.Devices.WiFiDirect**](/uwp/api/Windows.Devices.WiFiDirect) API allows you to connect and communicate with other local devices using Wi-Fi Direct to create ad-hoc local wireless networks.

### Wi-Fi Direct services

The [**Windows.Devices.WiFiDirect.Services**](/uwp/api/Windows.Devices.WiFiDirect.Services) API enables you to provide Wi-Fi Direct services and connect to them. Wi-Fi Direct Services are the way that one device on a Wi-Fi direct ad-hoc network (a Service Advertiser) offers capabilities to another device (a Service Seeker) over a Wi-Fi Direct connection.

### Mobile operators

Windows 10 exposes to a wide developer audience some APIs that have previously only been exposed to device manufacturers and mobile operators. Note that while these APIs are exposed now, they are also gated by specific app capabilities that must be approved by Microsoft before an app can be published. Actual use of these APIs will still be limited primarily to device manufacturers and mobile operators.

### Network operations

The [**Windows.Networking.NetworkOperators**](/uwp/api/Windows.Networking.NetworkOperators) API deals primarily with the configuration and provisioning of phones. As such, permission to use the capabilities that control it are limited to device manufacturers and telecom providers.

### SMS

The [**Windows.Devices.Sms**](/uwp/api/Windows.Devices.Sms) namespace deals with SMS and related messages as low-level entities. It is provided for use by mobile operators for app-directed SMS use, and is controlled by a capability that will not be approved for use by most app developers. If you are writing an app to deal with messages, you should use the [**Windows.ApplicationModel.Chat**](/uwp/api/Windows.ApplicationModel.Chat) API instead, as it is designed to handle not just SMS messages, but also messages from other sources such as realtime chat apps, enabling a much richer chat/messaging experience.