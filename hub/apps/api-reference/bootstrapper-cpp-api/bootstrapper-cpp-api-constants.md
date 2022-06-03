---
title: Constants for use with the Bootstrapper C++ API
description: The following constants (for use with the Bootstrapper C++ API) are declared in `WindowsAppSDK-VersionInfo.h`.
ms.topic: article
ms.date: 04/21/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, app sdk, bootstrapper, bootstrapper api, C++, constants
ms.author: stwhi
author: stevewhims
ms.localizationpriority: low
---

# Constants for use with the Bootstrapper C++ API

The following constants (for use with the Bootstrapper C++ API) are declared in `WindowsAppSDK-VersionInfo.h`.

| Namespace (within Microsoft::WindowsAppSDK) | Constant | Value | Description |
|-|-|-|-|
| **Release** | constexpr uint16_t Major | \[release-dependent\] | The major version of the Windows App SDK release. |
| **Release** | constexpr uint16_t Minor | \[release-dependent\] | The minor version of the Windows App SDK release. |
| **Release** | constexpr uint16_t Patch | \[release-dependent\] | The patch version of the Windows App SDK release. |
| **Release** | constexpr uint32_t MajorMinor | \[release-dependent\] | The major and minor version of the Windows App SDK release, encoded as a uint32_t (0xMMMMNNNN where M=major, N=minor). |
| **Release** | constexpr PCWSTR Channel | \[release-dependent\] | The Windows App SDK release's channel; for example, L"preview", or empty string for stable. |
| **Release** | constexpr PCWSTR VersionTag | \[release-dependent\] | The Windows App SDK release's version tag; for example, L"preview2", or empty string for stable. |
| **Release** | constexpr PCWSTR VersionShortTag | \[release-dependent\] | The Windows App SDK release's short-form version tag; for example, L"p2", or empty string for stable. |
| **Runtime::Identity** | constexpr PCWSTR Publisher | L"CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" | The Windows App SDK runtime's package identity's Publisher. |
| **Runtime::Identity** | constexpr PCWSTR PublisherId | L"8wekyb3d8bbwe" | The Windows App SDK runtime's package identity's PublisherId. |
| **Runtime::Version** | constexpr uint16_t Major | \[release-dependent\] | The major version of the Windows App SDK runtime; for example, 1000. |
| **Runtime::Version** | constexpr uint16_t Minor | \[release-dependent\] | The minor version of the Windows App SDK runtime; for example, 446. |
| **Runtime::Version** | constexpr uint16_t Build | \[release-dependent\] | The build version of the Windows App SDK runtime; for example, 804. |
| **Runtime::Version** | constexpr uint16_t Revision | \[release-dependent\] | The revision version of the Windows App SDK runtime; for example, 0. |
| **Runtime::Version** | constexpr uint64_t UInt64 | \[release-dependent\] | The version of the Windows App SDK runtime, as a uint64l for example, 0x03E801BE03240000. |
| **Runtime::Version** | constexpr PCWSTR DotQuadString | \[release-dependent\] | The version of the Windows App SDK runtime, as a string (const wchar_t*); for example, L"1000.446.804.0". |
| **Runtime::Packages::Framework** | constexpr PCWSTR PackageFamilyName | "Microsoft.WindowsAppRuntime.1.1-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Framework package's family name. |
| **Runtime::Packages::Main** | constexpr PCWSTR PackageFamilyName | L"MicrosoftCorporationII.WinAppRuntime.Main.1.1-p1_8wekyb3d8bbwe" | The Windows App SDK runtime's Main package's family name. |
| **Runtime::Packages::Singleton** | constexpr PCWSTR PackageFamilyName | L"Microsoft.WindowsAppRuntime.Singleton-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Singleton package's family name. |

## Requirements
**Minimum supported SDK:** Windows App SDK version 1.1 Stable

**Namespace:** Microsoft::WindowsAppSDK

**Header:** WindowsAppSDK-VersionInfo.h

## See also

* [Bootstrapper C++ API](../index.md)
