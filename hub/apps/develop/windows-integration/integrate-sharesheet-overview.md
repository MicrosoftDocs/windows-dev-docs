---
description: Learn how to integrate apps with the Windows Share.
title: An overview of app integration options with Windows Share
ms.topic: article
ms.date: 04/16/2024
ms.localizationpriority: medium
---

# App integration options with Windows Share

The Windows Share Sheet is a system-provided UI that enables users to share content from your app with other apps. The Share Sheet is available in the Windows shell and is accessible from any app that supports sharing. It provides a consistent and familiar experience for users, and it's a great way to increase the discoverability of your app.

## In this section

| Topic | Description |
|--|--|
| [Integrate packaged apps with Windows Share](integrate-sharesheet-packaged.md) | Discover how to integrate packaged apps with the Windows Share Sheet. |
| [Integrate Progressive Web Apps (PWAs) with Windows Share](integrate-sharesheet-pwa.md) | Discover how to integrate a Progressive Web App (PWA) with the Windows Share Sheet. |
| [Integrate unpackaged apps with Windows Share](integrate-sharesheet-unpackaged.md) | Discover how to integrate unpackaged apps with the Windows Share Sheet. |
| [Share data - UWP apps](/windows/uwp/app-to-app/share-data) | Learn how to share data between UWP apps. |

## Share registration videos

The following video demonstrates how an app with package identity can handle shared files:

> [!VIDEO https://learn-video.azurefd.net/vod/player?id=b09d23f4-db82-4df8-a236-5af04f7cf29a]

This video demonstraes how an unpackaged app without package identity won't appear as a Share Target:

> [!VIDEO https://learn-video.azurefd.net/vod/player?id=1cb658ce-30ec-49dc-baf0-04351771707d]

## See also

- [Communication - Windows apps](/windows/apps/develop/communication)
- [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
- [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)
- [Migrate from UWP to the Windows App SDK](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Advantages and Disadvantages of packaging an application - Deployment overview](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app)
- [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/)
- [Share Contract Implementation for WinAppSDK App](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [Share Contract Implementation for Apps Packaged with External Location](https://github.com/microsoft/AppModelSamples/blob/master/Samples/SparsePackages/PhotoStoreDemo/StartUp.cs)
