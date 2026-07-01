---
description: View a list of links to articles about the networking and web services technologies available for Windows developers.
title: Networking and web services
ms.date: 06/25/2026
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
keywords: windows app sdk, winui, networking
---
# Networking and web services

Windows provides networking APIs for common scenarios such as making HTTP requests, working with WebSockets for real-time communication, and using TCP/UDP sockets for custom protocols. The primary APIs are in the `Windows.Networking.Sockets` and `Windows.Web.Http` namespaces — these are Windows Runtime (WinRT) APIs that work in both UWP and WinUI 3 (Windows App SDK) desktop apps. WinUI 3 apps targeting .NET can also use `System.Net.Http.HttpClient` for HTTP requests.

The following networking and web services technologies are available for Windows developers.

| Topic | Description |
| - | - |
| [Networking basics](networking-basics.md) | Things you must do for any network-enabled app. |
| [Which networking technology?](which-networking-technology.md) | An overview of the networking technologies available for a Windows developer, with suggestions on how to choose the technologies that are right for your app. |
| [Network communications in the background](network-communications-in-the-background.md) | To continue network communication while your app runs in the background, use background tasks and either socket broker or control channel triggers. |
| [Sockets](sockets.md) | Sockets are a low-level data transfer technology on top of which many networking protocols are implemented. Windows offers TCP and UDP socket classes for client-server or peer-to-peer applications, whether connections are long-lived or an established connection is not required. |
| [WebSockets](websockets.md) | WebSockets provide a mechanism for fast, secure, two-way communication between a client and a server over the web using HTTP(S), and supporting both UTF-8 and binary messages. |
| [HttpClient](httpclient.md) | Use [Windows.Web.Http](/uwp/api/Windows.Web.Http) namespace API to send and receive information using the HTTP 2.0 and HTTP 1.1 protocols. |
| [RSS/Atom feeds](web-feeds.md) | Retrieve or create the most current and popular Web content using syndicated feeds generated according to the RSS and Atom standards using features in the [Windows.Web.Syndication](/uwp/api/Windows.Web.Syndication) namespace. |
| [Background transfers](background-transfers.md) | Use the background transfer API to copy files reliably over the network. |
