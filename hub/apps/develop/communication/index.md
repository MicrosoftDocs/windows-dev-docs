---
title: Communication features for Windows apps
description: Discover communication features and APIs for Windows apps, including networking, data sharing, and interprocess communication. Learn how to implement these capabilities.
ms.topic: concept-article
ms.date: 07/12/2026
# Customer intent: As a Windows app developer, I want to learn about communication features and APIs that I can use in my Windows apps.
---

# Communication

The Windows App SDK doesn't include its own IPC or networking APIs. Instead, Windows App SDK desktop apps use the mechanisms provided by the Windows SDK and the .NET runtime: OS-level IPC (such as named pipes, out-of-process COM, shared memory, and RPC), and networking APIs from Win32 (Winsock, WinHTTP), WinRT (such as `Windows.Networking.Sockets` and `Windows.Web.Http`), or .NET (such as `HttpClient` and `System.Net.Sockets`).

Because these apps run as full-trust Win32 processes, the Win32 IPC and networking APIs work without any additional SDK dependency, whether or not the app has package identity. The WinRT networking APIs also work in this configuration; note that some other WinRT APIs do require package identity.

For sharing data between apps, Windows 11 also supports integration with the [Windows Share Sheet](../windows-integration/integrate-sharesheet-overview.md). A packaged or unpackaged desktop app can *send* content to the Share Sheet by using the `IDataTransferManagerInterop` pattern. To *receive* shared content by registering as a Share Target, an unpackaged app must first be granted package identity.

## Windows OS features

Windows 10 and later OS releases provide a wide variety of APIs related to communication scenarios for apps. These features are available via a combination of WinRT and Win32 (C++ and COM) APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk).

#### WinRT APIs

The following articles provide information about features available via WinRT APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Copy and paste](copy-and-paste.md) | Learn how to implement copy and paste functionality in your WinUI, UWP, or other desktop app using the clipboard. |
| [App-to-app communication](/windows/uwp/app-to-app/) | Learn how to share data between apps, including how to use the Share contract, copy and paste, drag and drop, and app services. |
| [Interprocess communication](interprocess-communication.md) | Learn about ways to perform interprocess communication (IPC) between Windows App SDK desktop apps and other Win32 applications. |
| [Networking and web services](../networking/index.md) | Learn about networking and web services technologies that are available to apps. |
| [Sharing named objects](sharing-named-objects.md) | Learn how to share named objects between packaged desktop apps and unpackaged Win32 applications. |

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

[Interprocess communication](interprocess-communication.md)

[Develop Windows desktop apps](../index.md)

[Integrate Share options in your Windows app](../windows-integration/integrate-sharesheet-overview.md)
