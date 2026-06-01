---
description: Learn how to implement Share on Windows — integrate your app with the Windows Share Sheet to let users share content across Windows apps and services.
title: "Share on Windows: integrate the Windows Share Sheet"
ms.topic: overview
ms.date: 05/30/2026
ms.localizationpriority: medium
keywords: share on windows, windows share, share sheet, windows 11 share, sharesheet, file sharing, share button, packaged apps, pwa, c++
#customer intent: As a Windows developer, I want to learn how to integrate share options in my Windows app so that users can share content with other Windows apps.
---

# Share on Windows: integrate the Windows Share Sheet

The Windows Share Sheet is a system-provided UI that enables users to share content from your app with other Windows apps. The Share Sheet is available in the Windows shell and is accessible from any app that supports sharing. It provides a consistent and familiar experience for users, and it's a great way to increase the discoverability of your app.

Integrating share options in your Windows app can significantly enhance user experience by allowing seamless content sharing across different applications. Whether you're developing a new app or updating an existing one, this guide will provide you with the necessary steps and code samples to implement the Windows Share feature effectively.

In this comprehensive guide, you'll learn how to add the share feature to your packaged or unpackaged Windows app, implement file sharing methods, and show your app on the share sheet. You can leverage share options in a .NET, Windows C++, or PWA app, and this guide will help you get started. By following these steps, you can ensure that your app leverages the full potential of the Windows Share feature, making it more interactive and user-friendly.

## In this section

| Topic | Description |
|--|--|
| [Integrate packaged apps with Windows Share](integrate-sharesheet-packaged.md) | Discover how to integrate packaged apps with the Windows Share Sheet. |
| [Integrate Progressive Web Apps (PWAs) with Windows Share](integrate-sharesheet-pwa.md) | Discover how to integrate a Progressive Web App (PWA) with the Windows Share Sheet. |
| [Integrate unpackaged apps with Windows Share](integrate-sharesheet-unpackaged.md) | Discover how to integrate unpackaged apps with the Windows Share Sheet. |
| [Share data - WinUI apps](/windows/apps/develop/windows-integration/integrate-sharesheet-overview) | Learn how to share data between WinUI apps. |

## See also

- [Communication - Windows apps](/windows/apps/develop/communication/)
- [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
- [Create your first WinUI project](/windows/apps/winui/winui3/create-your-first-winui3-app)
- [Migrate from UWP to the Windows App SDK](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Advantages and Disadvantages of packaging an application - Deployment overview](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app)
- [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/)
- [Share Contract Implementation for Windows App SDK](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [Share Contract Implementation for Apps Packaged with External Location](https://github.com/microsoft/AppModelSamples/blob/master/Samples/PackageWithExternalLocation/cs/PhotoStoreDemo/StartUp.cs)
