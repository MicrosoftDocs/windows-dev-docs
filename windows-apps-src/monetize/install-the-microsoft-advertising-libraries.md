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




For Universal Windows Platform (UWP) apps for Windows 10, the Microsoft advertising libraries are included in the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk). This SDK is an extension to Visual Studio 2015 and later versions. For more information about installing this SDK, see [this article](microsoft-store-services-sdk.md).

> **Note**&nbsp;&nbsp;If you have installed Windows 10 SDK (14393) or later, you must also install the WinJS library if you want to add ads to a JavaScript/HTML UWP app. This library used to be included in previous versions of the Windows 10 SDK, but starting with the Windows 10 SDK (14393) this library must be installed separately. To install WinJS, see [Get WinJS](http://try.buildwinjs.com/download/GetWinJS/).

For XAML and JavaScript/HTML apps for Windows 8.1 and Windows Phone 8.x, the Microsoft advertising libraries are included in the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk). This SDK is an extension to Visual Studio 2015 and Visual Studio 2013.

For Windows Phone Silverlight 8.x apps, the Microsoft advertising libraries are available in a NuGet package that you can download and install to your project. For more information, see [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md).

## Library names for advertising


There are several different advertising libraries available in the Microsoft Store Services SDK and Microsoft Advertising SDK for Windows and Windows Phone 8.x:

* The Microsoft Store Services SDK includes the Microsoft advertising libraries (which provide the [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) and [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) classes for XAML and JavaScript/HTML apps).

* The Microsoft Advertising SDK for Windows and Windows Phone 8.x includes two sets of advertising libraries: the libraries for Microsoft advertising (which provide the [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) and [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx)  classes for XAML and JavaScript/HTML apps) and the libraries for ad mediation (which provide the **AdMediatorControl** class).

This documentation describes how to use the **AdControl** and **InterstitialAd** classes in the Microsoft advertising libraries to display banner or interstitial ads. For information about using ad mediation for Windows 8.1 and Windows Phone 8.x apps, see [Use ad mediation to maximize revenue](https://msdn.microsoft.com/library/windows/apps/xaml/dn864359.aspx).

>**Note**&nbsp;&nbsp;Ad mediation using the **AdMediatorControl** class is currently not supported for UWP apps for Windows 10. Server-side mediation is coming soon for UWP apps using the same APIs for banner ads (**AdControl**) and interstitial ads (**InterstitialAd**).

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

 

 

 
