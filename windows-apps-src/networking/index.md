---
author: stevewhims
ms.assetid: 7bb9fd81-8ab5-4f8d-a854-ce285b0669a4
description: Technologies for accessing the network and web services.
title: Networking and web services
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Networking and web services


The following networking and web services technologies are available for Universal Windows Platform (UWP) developers.

| Topic                                                                                   | Description                                                                      |
|-----------------------------------------------------------------------------------------|----------------------------------------------------------------------------------|
| [Networking basics](networking-basics.md)                                               | Things you must do for any network-enabled app.                     |
| [Which networking technology?](which-networking-technology.md)                          | A quick overview of the networking technologies available for a UWP developer, with suggestions on how to choose the technologies that are right for your app.               |
| [Network communications in the background](network-communications-in-the-background.md) | Apps use background tasks and two main mechanisms to maintain communications when they are not in the foreground: The socket broker, and control channel triggers.                  |
| [Sockets](sockets.md)                                                                   | You can use both [Windows.Networking.Sockets](https://msdn.microsoft.com/library/windows/apps/xaml/windows.networking.sockets.aspx) and [Winsock](https://msdn.microsoft.com/library/windows/desktop/ms737523) to communicate with other devices as a UWP app developer. This topic provides in-depth guidance on using the Windows.Networking.Sockets namespace to perform networking operations. |
| [WebSockets](websockets.md)                                                             | WebSockets provide a mechanism for fast, secure two-way communication between a client and a server over the web using HTTP(S).                 |
| [HttpClient](httpclient.md)                                                             | Use [Windows.Web.Http](https://msdn.microsoft.com/library/windows/apps/dn279692) namespace API to send and receive information using the HTTP 2.0 and HTTP 1.1 protocols.             |
| [RSS/Atom feeds](web-feeds.md)                                                          | Retrieve or create the most current and popular Web content using syndicated feeds generated according to the RSS and Atom standards using features in the [Windows.Web.Syndication](https://msdn.microsoft.com/library/windows/apps/br243632) namespace.                   |
| [Background transfers](background-transfers.md)                                         | Use the background transfer API to copy files reliably over the network.           |
