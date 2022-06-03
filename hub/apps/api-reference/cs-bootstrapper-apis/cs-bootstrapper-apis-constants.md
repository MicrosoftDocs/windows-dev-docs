---
title: Constants for use with the Bootstrapper C# APIs
description: The following constants are for use with the Bootstrapper C# APIs.
ms.topic: article
ms.date: 04/21/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, C#, interop, Bootstrapper, Bootstrapper API, constants
ms.author: stwhi
author: stevewhims
ms.localizationpriority: low
---

# Constants for use with the Bootstrapper C# APIs

The following constants are for use with the Bootstrapper C# APIs.

## Definition

Namespace: **Microsoft.WindowsAppSDK**

Assembly: Microsoft.WindowsAppRuntime.Bootstrap.Net.dll

| Namespace (within Microsoft.WindowsAppSDK) | Constant | Value | Description |
|-|-|-|-|
| **Release** | public const ushort Major | \[release-dependent\] | The major version of the Windows App SDK release. |
| **Release** | public const ushort Minor | \[release-dependent\] | The minor version of the Windows App SDK release. |
| **Release** | public const ushort Patch | \[release-dependent\] | The patch version of the Windows App SDK release. |
| **Release** | public const uint MajorMinor | \[release-dependent\] | The major and minor version of the Windows App SDK release, encoded as a uint32_t (0xMMMMNNNN where M=major, N=minor). |
| **Release** | public const string Channel | \[release-dependent\] | The Windows App SDK release's channel; for example, "preview", or empty string for stable. |
| **Release** | public const string VersionTag | \[release-dependent\] | The Windows App SDK release's version tag; for example, "preview2", or empty string for stable. |
| **Release** | public const string VersionShortTag | \[release-dependent\] | The Windows App SDK release's short-form version tag; for example, "p2", or empty string for stable. |
| **Runtime.Identity** | public const string Publisher | "CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" | The Windows App SDK runtime's package identity's Publisher. |
| **Runtime.Identity** | public const string PublisherId | "8wekyb3d8bbwe" | The Windows App SDK runtime's package identity's PublisherId. |
| **Runtime.Version** | public const ushort Major | \[release-dependent\] | The major version of the Windows App SDK runtime; for example, 1000. |
| **Runtime.Version** | public const ushort Minor | \[release-dependent\] | The minor version of the Windows App SDK runtime; for example, 446. |
| **Runtime.Version** | public const ushort Build | \[release-dependent\] | The build version of the Windows App SDK runtime; for example, 804. |
| **Runtime.Version** | public const ushort Revision | \[release-dependent\] | The revision version of the Windows App SDK runtime; for example, 0. |
| **Runtime.Version** | public const ulong UInt64 | \[release-dependent\] | The version of the Windows App SDK runtime, as a uint64l for example, 0x03E801BE03240000. |
| **Runtime.Version** | public const string DotQuadString | \[release-dependent\] | The version of the Windows App SDK runtime, as a string (const wchar_t*); for example, "1000.446.804.0". |
| **Runtime.Packages.Framework** | public const string PackageFamilyName | "Microsoft.WindowsAppRuntime.1.1-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Framework package's family name. |
| **Runtime.Packages.Main** | public const string PackageFamilyName | "MicrosoftCorporationII.WinAppRuntime.Main.1.1-p1_8wekyb3d8bbwe" | The Windows App SDK runtime's Main package's family name. |
| **Runtime.Packages.Singleton** | public const string PackageFamilyName | "Microsoft.WindowsAppRuntime.Singleton-preview1_8wekyb3d8bbwe" | The Windows App SDK runtime's Singleton package's family name. |

## See also

* [Bootstrapper C# APIs](../index.md)
