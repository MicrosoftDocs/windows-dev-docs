---
author: mcleanbyron
ms.assetid: ca92bed1-ad9e-4947-ad91-87d12de727c0
description: Review the release notes for the Microsoft advertising libraries in the Microsoft Store Services SDK.
title: Release notes for the Microsoft advertising libraries
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, release notes
---

# Release notes for the Microsoft advertising libraries




This section provides release notes for the current release of the Microsoft advertising libraries in the Microsoft Store Services SDK (for UWP apps) and the Microsoft Advertising SDK for Windows and Windows Phone 8.x (for Windows 8.1 and Windows Phone 8.x apps). These libraries support XAML and JavaScript/HTML apps for Windows 10, Windows 8.1, Windows Phone 8.1 and Windows Phone 8.

## Installation


The Microsoft advertising libraries are available as part of the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk) (for UWP apps) and the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) (for Windows 8.1 and Windows Phone 8.x apps). For more information about installing the SDKs and the libraries that are included in them, see [Install the Microsoft advertising libraries](install-the-microsoft-advertising-libraries.md).

To get the Microsoft advertising assemblies for Windows Phone 8.x Silverlight projects, install the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk), open your project in Visual Studio, and then go to **Project** > **Add Connected Service** > **Ad Mediator** to automatically download the assemblies. After doing this, you can remove the ad mediator references from your project if you do not want to use ad mediation. For more information, see [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md).


## Uninstall previous versions

Before you install the Microsoft Store Services SDK (for UWP apps) or the Microsoft Advertising SDK for Windows and Windows Phone 8.x (for Windows 8.1 and Windows Phone 8.x apps), it is highly recommended that you uninstall all prior instances of the Microsoft Universal Ad Client SDK or the Microsoft Advertising SDK.

## Target architecture-specific build outputs

When using the Microsoft advertising libraries, you cannot target **Any CPU** in your project. If your project targets the **Any CPU** platform, you may see a warning in your project after you add a reference to the Microsoft advertising libraries. To remove this warning, update your project to use an architecture-specific build output (for example, **x86**). For more information, see [Known issues](known-issues-for-the-advertising-libraries.md).

## C++ Support

The Microsoft advertising libraries (which include the **AdControl** and **InterstitialAd** classes) support apps written in C++ and DirectX using Windows Runtime Interoperability (*interop*). For more information and examples about programming using XAML and C++, see [Type System](https://msdn.microsoft.com/library/windows/apps/xaml/hh755822.aspx).

## No toolbox control

In the current release of the Microsoft advertising libraries in the Microsoft Store Services SDK or the Microsoft Advertising SDK for Windows and Windows Phone 8.x, there is no toolbox control for dragging an **AdControl** or **InterstitialAd** to a design surface in your app. For instructions about adding these controls in your markup and code, see the [developer walkthroughs](developer-walkthroughs.md).

## Latitude and longitude properties no longer available

The **AdControl** class no longer has **Latitude** and **Longitude** properties for UWP apps. Instead, code built into the ad control will detect and send these values to the ad servers on the app’s behalf.

## Important notice

Be sure to read the end user license agreement (EULA) in its entirety. See the topic [Important notice - EULA](important-notice-eula.md).

 

 
