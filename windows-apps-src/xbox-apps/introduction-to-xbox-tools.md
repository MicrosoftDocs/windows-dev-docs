---
author: Mtoepke
title: Introduction to Xbox One tools
description: The Xbox One-specific tool Dev Home, using the Windows Device Portal.
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 6eaf376f-0d7c-49de-ad78-38e689b43658
---

# Introduction to Xbox One tools

This section covers the Xbox One-specific tool _Dev Home_, using the Windows Device Portal.

## Dev Home

_Dev Home_ is a tools experience on the Xbox One Development Kit designed to aid developer productivity. Dev Home offers functionality to manage and configure your dev kit.

For more information about Dev Home, see [Developer Home on the Console](dev-home.md).

## Windows Device Portal
Windows Device Portal (WDP) is a OneCore device management tool that allows a browser-based device management experience.

> [!NOTE]
> For more information on WDP, see the [Windows Device Portal overview](../debug-test-perf/device-portal.md).

To enable WDP on your Xbox One console:

1. Select the Dev Home tile on the home screen.

  ![Select Dev Home tile](images/windowsdeviceportal_1.png)

2. Within Dev Home, navigate to the **Remote management** tool.

  ![Remote management tool](images/windowsdeviceportal_2.png)

3. Select __Manage Windows Device Portal__, and then press __A__.
4. Select the __Enable Windows Device Portal__ check box.
5. Enter a __Username__ and __Password__, and save them. These are used to authenticate access to your dev kit from a browser.
6. Close the __Settings__ page, and note the URL listed on the _Remote Management_ tool to connect.
7. Enter the URL in your browser, and then sign in with the credentials you configured.
8. You will receive a warning about the certificate that was provided, similar to the following screenshot, because the security certificate signed by your Xbox One console is not considered a well-known trusted publisher. Click **Continue to this website** to access the Windows Device Portal.

  ![Security certificate warning](images/security_cert_warning.jpg)

## Xbox Dev Mode Companion
Xbox Dev Mode Companion is a tool that allows you to work on your console without leaving your PC. The app allows you to view the console screen and send input to it. For more information, see [Xbox Dev Mode Companion](xbox-dev-mode-companion.md).

## See also
- [How to use Fiddler with Xbox One when developing for UWP](uwp-fiddler.md)
- [Windows Device Portal overview](../debug-test-perf/device-portal.md)
- [UWP on Xbox One](index.md)


----
