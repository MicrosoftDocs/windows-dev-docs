---
author: normesta
Description: This article contains known issues with the Desktop to UWP Bridge.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge Known Issues
ms.author: normesta
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 71f8ffcb-8a99-4214-ae83-2d4b718a750e
---

# Known Issues (Desktop to UWP Bridge)

This article contains known issues with the Desktop to UWP Bridge.

## Blue screen with error code 0x139 (KERNEL_SECURITY_CHECK_FAILURE)

After installing or launching certain apps from the Windows Store, your machine may unexpectedly reboot with the error: **0x139 (KERNEL\_SECURITY\_CHECK\_ FAILURE)**.

Known affected apps include Kodi, JT2Go, Ear Trumpet, Teslagrad, and others.

A [Windows update (Version 14393.351 - KB3197954)](https://support.microsoft.com/kb/3197954) was released on 10/27/16 that includes important fixes that address this issue. If you encounter this problem, update your machine. If you are not able to update your PC because your machine restarts before you can log in, you should use system restore to recover your system to a point earlier than when you installed one of the affected apps. For information on how to use system restore, see [Recovery options in Windows 10](https://support.microsoft.com/help/12415/windows-10-recovery-options).

If updating does not fix the problem or you aren't sure how to recover your PC, please contact [Microsoft Support](https://support.microsoft.com/contactus/).

If you are a developer, you may want to prevent the installation of your Desktop Bridge apps on versions of Windows that do not include this update. Note that by doing this your app will not be available to users that have not yet installed the update. To limit the availability of your app to users that have installed this update, modify your AppxManifest.xml file as follows:

```<TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.14393.351" MaxVersionTested="10.0.14393.351"/>```

Details regarding the Windows Update can be found at:
* https://support.microsoft.com/kb/3197954
* https://support.microsoft.com/help/12387/windows-10-update-history
