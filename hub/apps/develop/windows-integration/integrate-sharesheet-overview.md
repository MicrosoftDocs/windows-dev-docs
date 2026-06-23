---
description: Learn how to implement Share on Windows - integrate your app with the Windows Share Sheet to let users share content across Windows apps and services.
title: "Share on Windows: integrate the Windows Share Sheet"
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 06/22/2026
ms.localizationpriority: medium
keywords: share on windows, windows share, share sheet, windows 11 share, sharesheet, file sharing, share button, packaged apps, pwa, c++
#customer intent: As a Windows developer, I want to learn how to integrate share options in my Windows app so that users can share content with other Windows apps.
---

# Share on Windows: integrate the Windows Share Sheet

The Windows Share Sheet is a system-provided UI that enables users to share content from your app with other Windows apps, or to receive content from other apps. It's available in the Windows shell and accessible from any app that supports sharing.

These pages are organized by **developer task**, not by packaging model. Whether you're building a packaged app (MSIX), Progressive Web App (PWA), or unpackaged Win32 app, you'll find step-by-step guidance in the Send or Receive pages below. Packaging model differences are covered within each task guide.

## Quick start: choose your task

| Task | What you'll do | Start here |
|--|--|--|
| **Send content** | Let users share files, text, links, or images from your app to others | [Send content from your app](integrate-sharesheet-send.md) |
| **Receive content** | Register your app to receive shared files and data from other apps | [Receive content in your app](integrate-sharesheet-receive.md) |
| **Appear in suggestions row** | Surface your app's contacts in the Share Sheet suggestions row | [Appear in suggestions row](integrate-sharesheet-contacts.md) |

## In this section

| Topic | Description |
|--|--|
| [Send content from your app](integrate-sharesheet-send.md) | Implement the Share contract so users can send content from your app. Covers packaged, PWA, and unpackaged apps. |
| [Receive content in your app](integrate-sharesheet-receive.md) | Register as a Share Target and handle incoming shared content. Covers packaged, PWA, and unpackaged apps. |
| [Appear in suggestions row](integrate-sharesheet-contacts.md) | Surface your app's contacts in the Share Sheet suggestions row by storing them with the Cross-device People API. |
| [People on Windows (Cross-device People API)](cross-device-people-api.md) | Store your app's contacts in Windows so they surface across People experiences, including the Share Sheet. |
| [DataFormat & FileType reference](dataformat-reference.md) | Reference table mapping DataFormats to send/receive APIs, plus a FileType-by-app-category cheat sheet for declaring manifest capabilities. |

## See also

- [Communication - Windows apps](/windows/apps/develop/communication/)
- [People on Windows (Cross-device People API)](cross-device-people-api.md)
- [DataFormat & FileType reference](dataformat-reference.md)
- [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
- [Create your first WinUI project](/windows/apps/winui/winui3/create-your-first-winui3-app)
- [Migrate from UWP to the Windows App SDK](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Advantages and Disadvantages of packaging an application - Deployment overview](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app)
- [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/)
- [Share Contract Implementation for Windows App SDK](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [Share Contract Implementation for Apps Packaged with External Location](https://github.com/microsoft/AppModelSamples/blob/master/Samples/PackageWithExternalLocation/cs/PhotoStoreDemo/StartUp.cs)
