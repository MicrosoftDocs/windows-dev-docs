---
title: Constants for use with the Bootstrapper C# APIs
description: The following constants are for use with the Bootstrapper C# APIs.
ms.topic: article
ms.date: 07/25/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, C#, interop, Bootstrapper, Bootstrapper API, constants
ms.localizationpriority: low
---

# Constants for use with the Bootstrapper C# APIs

The following constants are for use with the Bootstrapper C# APIs.

## Definition

Namespace: **Microsoft.WindowsAppSDK**

Assembly: Microsoft.WindowsAppRuntime.Release.Net.dll

## Microsoft.WindowsAppSDK.Release class

| Constant | Value | Description |
|-|-|-|
| public const string Channel | \[release-dependent\] | The Windows App SDK release's channel; for example, "preview", or empty string for stable. |
| public const string FormattedVersionShortTag | \[release-dependent\] | The Windows App SDK release's short-form version tag, formatted for concatenation when constructing identifiers; for example, "-p2", or empty string for stable. |
| public const string FormattedVersionTag | \[release-dependent\] | The Windows App SDK release's version tag, formatted for concatenation when constructing identifiers; for example, "-preview2", or empty string for stable. |
| public const ushort Major | \[release-dependent\] | The major version of the Windows App SDK release. |
| public const uint MajorMinor | \[release-dependent\] | The major and minor version of the Windows App SDK release, encoded as a uint32_t (0xMMMMNNNN where M=major, N=minor). |
| public const ushort Minor | \[release-dependent\] | The minor version of the Windows App SDK release. |
| public const ushort Patch | \[release-dependent\] | The patch version of the Windows App SDK release. |
| public const string VersionShortTag | \[release-dependent\] | The Windows App SDK release's short-form version tag; for example, "p2", or empty string for stable. |
| public const string VersionTag | \[release-dependent\] | The Windows App SDK release's version tag; for example, "preview2", or empty string for stable. |

## Microsoft.WindowsAppSDK.Runtime.Identity class

| Constant | Value | Description |
|-|-|-|
| public const string Publisher | "CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" | The Windows App SDK runtime's package identity's Publisher. |
| public const string PublisherId | "8wekyb3d8bbwe" | The Windows App SDK runtime's package identity's PublisherId. |

## Microsoft.WindowsAppSDK.Runtime.Packages.DDLM.Arm64 class

| Constant | Value | Description |
|-|-|-|
| public const string PackageFamilyName | \[release-dependent\] | The Windows App SDK runtime's Dynamic Dependency Lifetime Manager (DDLM) package's family name, for arm64. |

## Microsoft.WindowsAppSDK.Runtime.Packages.DDLM.X64 class

| Constant | Value | Description |
|-|-|-|
| public const string PackageFamilyName | \[release-dependent\] | The Windows App SDK runtime's Dynamic Dependency Lifetime Manager (DDLM) package's family name, for x64. |

## Microsoft.WindowsAppSDK.Runtime.Packages.DDLM.X86 class

| Constant | Value | Description |
|-|-|-|
| public const string PackageFamilyName | \[release-dependent\] | The Windows App SDK runtime's Dynamic Dependency Lifetime Manager (DDLM) package's family name, for x86. |

## Microsoft.WindowsAppSDK.Runtime.Packages.Framework class

| Constant | Value | Description |
|-|-|-|
| public const string PackageFamilyName | "Microsoft.WindowsAppRuntime.1.1-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Framework package's family name. |

## Microsoft.WindowsAppSDK.Runtime.Packages.Main class

| Constant | Value | Description |
|-|-|-|
| public const string PackageFamilyName | "MicrosoftCorporationII.WinAppRuntime.Main.1.1-p1_8wekyb3d8bbwe" | The Windows App SDK runtime's Main package's family name. |

## Microsoft.WindowsAppSDK.Runtime.Packages.Singleton class

| Constant | Value | Description |
|-|-|-|
| public const string PackageFamilyName | "Microsoft.WindowsAppRuntime.Singleton-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Singleton package's family name. |

## Microsoft.WindowsAppSDK.Runtime.Version class

| Constant | Value | Description |
|-|-|-|
| public const ushort Major | \[release-dependent\] | The major version of the Windows App SDK runtime; for example, 1000. |
| public const ushort Minor | \[release-dependent\] | The minor version of the Windows App SDK runtime; for example, 446. |
| public const ushort Build | \[release-dependent\] | The build version of the Windows App SDK runtime; for example, 804. |
| public const ushort Revision | \[release-dependent\] | The revision version of the Windows App SDK runtime; for example, 0. |
| public const ulong UInt64 | \[release-dependent\] | The version of the Windows App SDK runtime, as a uint64l for example, 0x03E801BE03240000. |
| public const string DotQuadString | \[release-dependent\] | The version of the Windows App SDK runtime, as a string (const wchar_t*); for example, "1000.446.804.0". |

## See also

* [Bootstrapper C# APIs](../index.md)
