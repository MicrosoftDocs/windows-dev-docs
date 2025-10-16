---
title: Communication features for Windows apps
description: Discover communication features and APIs for Windows apps, including networking, data sharing, and interprocess communication. Learn how to implement these capabilities.
ms.topic: concept-article
ms.date: 10/13/2025
# Customer intent: As a Windows app developer, I want to learn about communication features and APIs that I can use in my Windows apps.
---

# Communication

Communication features enable Windows apps to share data, connect over networks, and interact with other applications. This article provides an index of development features for implementing communication scenarios in Windows apps.

> [!NOTE]
> The [Windows App SDK](../../windows-app-sdk/index.md) currently does not provide APIs related to communication scenarios. However, in Windows 11 you can share data between apps by integrating with the [Windows Share Sheet](../windows-integration/integrate-sharesheet-overview.md) in packaged and unpackaged desktop apps.

## Windows OS features

Windows 10 and later OS releases provide a wide variety of APIs related to communication scenarios for apps. These features are available via a combination of WinRT and Win32 (C++ and COM) APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk).

#### WinRT APIs

The following articles provide information about features available via WinRT APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Copy and paste](copy-and-paste.md) | Learn how to implement copy and paste functionality in your WinUI, UWP, or other desktop app using the clipboard. |
| [App-to-app communication](/windows/uwp/app-to-app/) | Learn how to share data between apps, including how to use the Share contract, copy and paste, drag and drop, and app services. |
| [Interprocess communication](/windows/uwp/communication/interprocess-communication) | Learn about ways to perform interprocess communication (IPC) between UWP apps, packaged desktop apps, and unpackaged desktop apps. |
| [Networking and web services](/windows/uwp/networking/) | Learn about networking and web services technologies that are available to apps. |
| [Sharing named objects](/windows/uwp/communication/sharing-named-objects) | Learn how to share named objects between UWP apps, packaged desktop apps, and unpackaged desktop apps. |

#### Win32 (C++ and COM) APIs

The following articles provide information about features available via Win32 (C++ and COM) APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Networking and Internet](/windows/win32/networking) | Learn about APIs, components, and services that support your app's use of networking and the Internet. |
| [Remote Procedure Call](/windows/win32/rpc/rpc-start-page) | Learn about using Remote Procedure Call (RPC) to create distributed client/server programs. |
| [Windows Sockets 2 (Winsock)](/windows/win32/winsock/windows-sockets-start-page-2) | Learn how to use Windows Sockets 2 (Winsock) to create advanced Internet, intranet, and other network-capable apps. |

## .NET features

The .NET SDK also provides APIs related to communication scenarios for WPF and Windows Forms apps.

| Article | Description |
|---------|-------------|
| [Network programming in the .NET Framework](/dotnet/framework/network-programming/) | Learn about building network-enabled apps using .NET. |
| [Networking in Windows Forms](/dotnet/framework/winforms/advanced/networking-in-windows-forms-applications) | Learn about additional networking scenarios for Windows Forms apps. |

## Related content

[App-to-app communication](/windows/uwp/app-to-app/)

[Develop Windows desktop apps](../index.md)

[Integrate Share options in your Windows app](../windows-integration/integrate-sharesheet-overview.md)
