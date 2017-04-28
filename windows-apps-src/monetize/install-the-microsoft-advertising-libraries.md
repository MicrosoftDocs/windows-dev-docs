---
author: mcleanbyron
ms.assetid: 3aeddb83-5314-447b-b294-9fc28273cd39
description: Learn about how to install the Microsoft advertising libraries.
title: Install the Microsoft advertising libraries
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, install, SDK, libraries
---

# Install the Microsoft advertising libraries




For Universal Windows Platform (UWP) apps for Windows 10, the Microsoft advertising libraries are included in the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp). This SDK is an extension to Visual Studio 2015 and later versions.

> **Note**&nbsp;&nbsp;If you have installed Windows 10 SDK (14393) or later, you must also install the WinJS library if you want to add ads to a JavaScript/HTML UWP app. This library used to be included in previous versions of the Windows 10 SDK, but starting with the Windows 10 SDK (14393) this library must be installed separately. To install WinJS, see [Get WinJS](http://try.buildwinjs.com/download/GetWinJS/).

For XAML and JavaScript/HTML apps for Windows 8.1 and Windows Phone 8.x, the Microsoft advertising libraries are included in the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk). This SDK is an extension to Visual Studio 2015 and Visual Studio 2013.

For Windows Phone Silverlight 8.x apps, the Microsoft advertising libraries are available in a NuGet package that you can download and install to your project. For more information, see [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md).

<span id="framework" />
## Understanding framework packages in the Microsoft Advertising SDK for UWP apps

The Microsoft.Advertising.dll library in the Microsoft Advertising SDK for UWP apps is configured as a *framework package*. This library contains the advertising APIs in the [Microsoft.Advertising](https://msdn.microsoft.com/library/windows/apps/mt313187.aspx) and [Microsoft.Advertising.WinRT.UI](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.aspx) namespaces.

Because this library is a framework package, this means that after a user installs a version of your app that uses this library, this library is automatically updated on their device through Windows Update whenever we publish a new version of the library with fixes and performance improvements. This helps to ensure that your customers always have the latest available version of the library installed on their devices.

If we release a new version of the SDK that introduces new APIs or features in this library, you will need to install the latest version of the SDK to use those features. In this scenario, you would also need to publish your updated app to the Store.

## Advertising library names

Before you can use the any of the advertising controls in your app code, you must reference the appropriate library in your project. The following tables lists the names of each of the libraries as they appear in in the **Reference Manager** dialog box in Visual Studio.


<table>
	<thead>
		<tr><th>Control name</th><th>Project type</th><th>Library name in Reference Manager</th><th>Version number</th></tr>
	</thead>
	<tbody>
    <tr>
			<td rowspan="3">**AdControl** and **InterstitialAd** (XAML)</td>
			<td>UWP</td>
			<td>Microsoft Advertising SDK for XAML</td>
			<td>10.0</td>
		</tr>
		<tr>
			<td>Windows 8.1</td>
			<td>Ad Mediator SDK for Windows 8.1 XAML</td>
			<td>1.0</td>
		</tr>
		<tr>
			<td>Windows Phone 8.1</td>
			<td>Ad Mediator SDK for Windows Phone 8.1 XAML</td>
			<td>1.0</td>
		</tr>
    <tr>
			<td rowspan="3">**AdControl** and **InterstitialAd** (JavaScript/HTML)</td>
			<td>UWP</td>
			<td>Microsoft Advertising SDK for JavaScript</td>
			<td>10.0</td>
		</tr>
		<tr>
			<td>Windows 8.1</td>
			<td>Microsoft Advertising SDK for Windows 8.1 Native (JS)</td>
			<td>8.5</td>
		</tr>
		<tr>
			<td>Windows Phone 8.1</td>
			<td>Microsoft Advertising SDK for Windows Phone 8.1 Native (JS)</td>
			<td>8.5</td>
		</tr>
    <tr>
			<td rowspan="3">**AdMediatorControl** (XAML only)</td>
			<td>UWP</td>
			<td>Microsoft Advertising Universal SDK</td>
			<td>1.0</td>
		</tr>
		<tr>
			<td>Windows 8.1</td>
			<td>Ad Mediator SDK for Windows 8.1 XAML</td>
			<td>1.0</td>
		</tr>
		<tr>
			<td>Windows Phone 8.1</td>
			<td>Ad Mediator SDK for Windows Phone 8.1 XAML</td>
			<td>1.0</td>
		</tr>
	</tbody>
</table>

 

 

 
