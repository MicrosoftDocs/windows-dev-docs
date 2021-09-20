---
ms.assetid: ca92bed1-ad9e-4947-ad91-87d12de727c0
description: View release notes for the Microsoft advertising libraries that support XAML and JavaScript/HTML apps for Windows 10, Windows 8.1, Windows Phone 8.1 and Windows Phone 8.
title: Release notes for the advertising libraries
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, release notes
ms.localizationpriority: medium
---
# Release notes for the advertising libraries

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This section provides release notes for the current release of the Microsoft advertising libraries. These libraries support XAML and JavaScript/HTML apps for Windows 10, Windows 8.1, Windows Phone 8.1 and Windows Phone 8.

## Installation


The Microsoft advertising libraries are available as part of the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK). For more information about installing the SDK, see [Install the Microsoft Advertising SDK](install-the-microsoft-advertising-libraries.md).

## Uninstall previous versions

Before you install the latest Microsoft Advertising SDK, it is highly recommended that you uninstall all prior instances of the SDK. For more information, see [Install the Microsoft Advertising SDK](install-the-microsoft-advertising-libraries.md).

## Target architecture-specific build outputs

When using the Microsoft advertising libraries, you cannot target **Any CPU** in your project. If your project targets the **Any CPU** platform, you may see a warning in your project after you add a reference to the Microsoft advertising libraries. To remove this warning, update your project to use an architecture-specific build output (for example, **x86**). For more information, see [Known issues](known-issues-for-the-advertising-libraries.md).

## C++ Support

The Microsoft advertising libraries (which include the **AdControl** and **InterstitialAd** classes) support apps written in C++ and DirectX using Windows Runtime Interoperability (*interop*). For more information and examples about programming using XAML and C++, see [Type System](/cpp/cppcx/type-system-c-cx).

## No toolbox control

In the current release of the Microsoft advertising libraries in the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK), there is no toolbox control for dragging an **AdControl** or **InterstitialAd** to a design surface in your app. For instructions about adding these controls in your markup and code, see the [developer walkthroughs](developer-walkthroughs.md).

## Latitude and longitude properties no longer available

The **AdControl** class no longer has **Latitude** and **Longitude** properties for UWP apps. Instead, code built into the ad control will detect and send these values to the ad servers on the app’s behalf.


 

 