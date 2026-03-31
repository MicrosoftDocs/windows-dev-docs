---
title: Windows SDK overview
description: Learn about the Windows SDK, benefits it provides to developers, what is ready for developers now, and how to give feedback.
ms.topic: article
ms.date: 03/27/2026
keywords: windows win32, desktop development, Windows App SDK, windows sdk
ms.localizationpriority: medium
---

# Windows SDK

:::image type="icon" source="images/windows-sdk-hero.png":::

The Windows SDK (10.0.28000) for Windows 11 contains the latest platform headers, libraries, WinRT metadata, and build tools required to develop Windows applications. It supports both UWP and Win32 development and can target Windows 11, version 26H1, in addition to previous Windows releases. By using this SDK, developers gain access to the full set of Windows APIs and capabilities needed to build native and performant applications.

Additionally, the [Windows App SDK](../windows-app-sdk/index.md) builds on top of the Windows SDK by offering a consistent set of modern Windows APIs for desktop apps, allowing developers to adopt new capabilities without depending on a specific Windows version.

> [!div class="nextstepaction"]
> [Download Windows SDK](./downloads.md)

To see what's new, check out the [release notes](./release-notes.md).

 ### Release channels

 The Windows SDK is available in the following channels:

| Release channel | Description | Includes experimental APIs |
|--|--|--|
| [**Stable**](./downloads.md) | Default SDK release intended for production use. Includes all current stable APIs and a limited set of experimental APIs. | ✅ |
| [**Preview**](./downloads.md#preview-releases) | Preview SDK built on Windows Insider Preview releases. Provides access to newer and upcoming APIs, including experimental ones. | ✅ |

 ### Support and servicing

 | SDK Version | Status | EOS Date | Notes |
 |-------------|--------|----------|-------|
 | 28000+ |Supported | | |
 | 26100+ |Supported | | |
 | 22621 | Out of support | 2025-11-12 | Aligned with 22621 Enterprise OS EOS |
 | 22000 | Out of support | | |
 | 20348 | Out of support |  | Patched version shipped |
 | 19041 | Out of support | 2025-10-14 | |
 | 18362 and before | Out of support | | |
     
### System requirements
The Windows SDK has the following minimum system requirements:

#### Supported operating systems
- Windows 11, version 21h2 or higher: Home, Pro, Education, and Enterprise (LTSC is not supported for UWP)
- Windows 10, version 1507 or higher: Home, Pro, Education, and Enterprise (LTSB/LTSC and S mode are not supported for UWP)
- Windows Server 2022, Windows Server 2019, Windows Server 2016, and Windows Server 2012 R2 (Command line only)
- Windows 8.1
- Windows 7 SP1

(Not all tools are supported on earlier operating systems)

#### Hardware requirements
- 1.6 GHz or faster processor
- 1 GB of RAM
- 4 GB of available hard disk space

#### Additional SDK requirements
Installation on Windows 8.1 and earlier operating systems requires an Update for [Universal C Runtime in Windows](https://support.microsoft.com/topic/update-for-universal-c-runtime-in-windows-c0514201-7fe6-95a3-b0a5-287930f3560c). To install through Windows Update, make sure you install the latest recommended updates and patches from Microsoft Update before you install the Windows SDK.
