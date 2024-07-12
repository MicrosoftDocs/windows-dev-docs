---
title: Security and identity
description: This article provides an index of development features that are related to security and identity scenarios in Windows apps.
ms.topic: article
ms.date: 07/12/2024
---

# Security and identity

This article provides an index of development features that are related to scenarios involving security and identity in Windows apps.

> [!NOTE]
> The [Windows App SDK](../windows-app-sdk/index.md) currently does not provide APIs related to security and identity scenarios.

## Windows OS features

Windows 10 and later OS releases provide a wide variety of APIs related to graphics scenarios for apps. These features are available via a combination of WinRT and Win32 (C++ and COM) APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk).

### WinRT APIs

The following articles provide information about features available via WinRT APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Security](/windows/uwp/security) | Learn about the breadth of security features for Windows apps.  |
| [Authentication and user identity](/windows/uwp/security/authentication-and-user-identity) | Windows apps have several options for user authentication, ranging from simple single sign-on (SSO) using Web authentication broker to highly secure two-factor authentication. |
| [Cryptography](/windows/uwp/security/cryptography) | Learn about cryptography features available to Windows apps. |
| [Windows Hello](./security/windows-hello.md) | This article describes the Windows Hello technology and discusses how developers can implement this technology to protect their apps and backend services. It highlights specific capabilities of Windows Hello that help mitigate threats from conventional credentials and provides guidance about designing and deploying these technologies as part of your packaged Windows apps. |
| [Create a Windows Hello login app](./security/windows-hello-login.md) | Part 1 of a complete walkthrough on how to create a packaged Windows app that uses Windows Hello as an alternative to traditional username and password authentication systems. |
| [Create a Microsoft Passport login service](./security/windows-hello-auth-service.md) | Part 2 of a complete walkthrough on how to use Windows Hello as an alternative to traditional username and password authentication systems in packaged Windows apps. |

### Win32 (C++ and COM) APIs

The following articles provide information about features available via Win32 (C++ and COM) APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Security and identity](/windows/win32/security) | Learn about the breadth of security features available to Windows apps via Win32 APIs. |
| [Authentication](/windows/win32/secauthn/authentication-portal) | Learn about authentication features available via Win32 APIs. |
| [Cryptography](/windows/win32/seccng/cng-portal) | Learn about cryptography features available via Win32 APIs. |

## .NET features

The .NET SDK also provides APIs related to security and identity scenarios for WPF and Windows Forms apps. The security and cryptography APIs in .NET can also be used in C# WinUI apps.

| Article | Description |
|---------|-------------|
| [Security in .NET](/dotnet/standard/security/)  | Learn about security concepts and features for all .NET apps.  |
| [Security (WPF)](/dotnet/desktop/wpf/security-wpf) | Learn about security concepts and features for WPF apps. |
| [Windows Forms Security](/dotnet/desktop/winforms/windows-forms-security) | Learn about security concepts and features for Windows Forms apps. |
