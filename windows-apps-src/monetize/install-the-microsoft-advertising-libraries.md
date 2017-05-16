---
author: mcleanbyron
ms.assetid: 3aeddb83-5314-447b-b294-9fc28273cd39
description: Learn about how to install the Microsoft Advertising SDK.
title: Install the Microsoft Advertising SDK
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, install, SDK, libraries
---

# Install the Microsoft Advertising SDK

For Universal Windows Platform (UWP) apps for Windows 10, the Microsoft advertising libraries are included in the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp). This SDK is an extension to Visual Studio 2015 and later versions.

> [!NOTE]
> If you have installed Windows 10 SDK (14393) or later, you must also install the WinJS library if you want to add ads to a JavaScript/HTML UWP app. This library used to be included in previous versions of the Windows 10 SDK, but starting with the Windows 10 SDK (14393) this library must be installed separately. To install WinJS, see [Get WinJS](http://try.buildwinjs.com/download/GetWinJS/).

For XAML and JavaScript/HTML apps for Windows 8.1 and Windows Phone 8.x, the Microsoft advertising libraries are included in the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk). This SDK is an extension to Visual Studio 2015 and Visual Studio 2013.

<span id="references" />
## Add the assembly reference to your project

After you install the Microsoft Advertising SDK, follow these instructions to reference the appropriate library in your project to use the advertising APIs.

1. Open your project in Visual Studio.
    > [!NOTE]
    > If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft advertising library in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

2. In **Solution Explorer**, right click **References** and select **Add Reference…**

3. In **Reference Manager**, select one of the following references depending on your project type:

    -   For a Universal Windows Platform (UWP) project: Expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (for XAML apps) or **Microsoft Advertising SDK for JavaScript** (for apps built using JavaScript and HTML).

    -   For a Windows 8.1 project: Expand **Windows 8.1**, click **Extensions**, and then select the check box next to **Ad Mediator SDK for Windows 8.1 XAML** (for XAML apps) or **Microsoft Advertising SDK for Windows 8.1 Native (JS)** (for apps built using JavaScript and HTML).

    -   For a Windows Phone 8.1 project: Expand **Windows Phone 8.1**, click **Extensions**, and then select the check box next to **Ad Mediator SDK for Windows Phone 8.1 XAML** (for XAML apps) or **Microsoft Advertising SDK for Windows Phone 8.1 Native (JS)** (for apps built using JavaScript and HTML).

4.  In **Reference Manager**, click OK.

For walkthroughs that show how to get started using the advertising APIs, see the following articles:

* [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
* [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md)
* [Interstitial ads](interstitial-ads.md)

<span id="framework" />
## Understanding framework packages in the Microsoft Advertising SDK for UWP apps

The Microsoft.Advertising.dll library in the Microsoft Advertising SDK for UWP apps is configured as a *framework package*. This library contains the advertising APIs in the [Microsoft.Advertising](https://msdn.microsoft.com/library/windows/apps/mt313187.aspx) and [Microsoft.Advertising.WinRT.UI](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.aspx) namespaces.

Because this library is a framework package, this means that after a user installs a version of your app that uses this library, this library is automatically updated on their device through Windows Update whenever we publish a new version of the library with fixes and performance improvements. This helps to ensure that your customers always have the latest available version of the library installed on their devices.

If we release a new version of the SDK that introduces new APIs or features in this library, you will need to install the latest version of the SDK to use those features. In this scenario, you would also need to publish your updated app to the Store.
