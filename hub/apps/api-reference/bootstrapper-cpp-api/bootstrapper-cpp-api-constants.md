---
title: Constants for use with the Bootstrapper C++ API
description: The following constants (for use with the Bootstrapper C++ API) are declared in `WindowsAppSDK-VersionInfo.h`.
ms.topic: article
ms.date: 07/25/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, app sdk, bootstrapper, bootstrapper api, C++, constants
ms.localizationpriority: low
---

# Constants for use with the Bootstrapper C++ API

The following constants (for use with the Bootstrapper C++ API) are declared in `WindowsAppSDK-VersionInfo.h`.

## Microsoft::WindowsAppSDK::Release namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR Channel | \[release-dependent\] | The Windows App SDK release's channel; for example, L"preview", or empty string for stable. |
| constexpr PCWSTR FormattedVersionShortTag | \[release-dependent\] | The Windows App SDK release's short-form version tag, formatted for concatenation when constructing identifiers; for example, "-p2", or empty string for stable. |
| constexpr PCWSTR FormattedVersionTag | \[release-dependent\] | The Windows App SDK release's version tag, formatted for concatenation when constructing identifiers; for example, "-preview2", or empty string for stable. |
| constexpr uint16_t Major | \[release-dependent\] | The major version of the Windows App SDK release. |
| constexpr uint32_t MajorMinor | \[release-dependent\] | The major and minor version of the Windows App SDK release, encoded as a uint32_t (0xMMMMNNNN where M=major, N=minor). |
| constexpr uint16_t Minor | \[release-dependent\] | The minor version of the Windows App SDK release. |
| constexpr uint16_t Patch | \[release-dependent\] | The patch version of the Windows App SDK release. |
| constexpr PCWSTR VersionShortTag | \[release-dependent\] | The Windows App SDK release's short-form version tag; for example, L"p2", or empty string for stable. |
| constexpr PCWSTR VersionTag | \[release-dependent\] | The Windows App SDK release's version tag; for example, L"preview2", or empty string for stable. |

## Microsoft::WindowsAppSDK::Runtime::Identity namespace

Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR Publisher | L"CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" | The Windows App SDK runtime's package identity's Publisher. |
| constexpr PCWSTR PublisherId | L"8wekyb3d8bbwe" | The Windows App SDK runtime's package identity's PublisherId. |

## Microsoft::WindowsAppSDK::Runtime::Packages::Framework namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR PackageFamilyName | "Microsoft.WindowsAppRuntime.1.1-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Framework package's family name. |

## Microsoft::WindowsAppSDK::Runtime.Packages.DDLM.Arm64 namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR PackageFamilyName | \[release-dependent\] | The Windows App SDK runtime's Dynamic Dependency Lifetime Manager (DDLM) package's family name, for arm64. |

## Microsoft::WindowsAppSDK::Runtime.Packages.DDLM.X64 namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR PackageFamilyName | \[release-dependent\] | The Windows App SDK runtime's Dynamic Dependency Lifetime Manager (DDLM) package's family name, for x64. |

## Microsoft::WindowsAppSDK::Runtime.Packages.DDLM.X86 namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR PackageFamilyName | \[release-dependent\] | The Windows App SDK runtime's Dynamic Dependency Lifetime Manager (DDLM) package's family name, for x86. |

## Microsoft::WindowsAppSDK::Runtime::Packages::Main namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR PackageFamilyName | L"MicrosoftCorporationII.WinAppRuntime.Main.1.1-p1_8wekyb3d8bbwe" | The Windows App SDK runtime's Main package's family name. |

## Microsoft::WindowsAppSDK::Runtime::Packages::Singleton namespace

| Constant | Value | Description |
|-|-|-|
| constexpr PCWSTR PackageFamilyName | L"Microsoft.WindowsAppRuntime.Singleton-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Singleton package's family name. |

## Microsoft::WindowsAppSDK::Runtime::Version namespace

| Constant | Value | Description |
|-|-|-|
| constexpr uint16_t Build | \[release-dependent\] | The build version of the Windows App SDK runtime; for example, 804. |
| constexpr PCWSTR DotQuadString | \[release-dependent\] | The version of the Windows App SDK runtime, as a string (const wchar_t*); for example, L"1000.446.804.0". |
| constexpr uint16_t Major | \[release-dependent\] | The major version of the Windows App SDK runtime; for example, 1000. |
| constexpr uint16_t Minor | \[release-dependent\] | The minor version of the Windows App SDK runtime; for example, 446. |
| constexpr uint16_t Revision | \[release-dependent\] | The revision version of the Windows App SDK runtime; for example, 0. |
| constexpr uint64_t UInt64 | \[release-dependent\] | The version of the Windows App SDK runtime, as a uint64l for example, 0x03E801BE03240000. |

## Requirements
**Minimum supported SDK:** Windows App SDK version 1.1

**Namespace:** Microsoft::WindowsAppSDK

**Header:** WindowsAppSDK-VersionInfo.h

## See also

* [Bootstrapper C++ API](../index.md)
