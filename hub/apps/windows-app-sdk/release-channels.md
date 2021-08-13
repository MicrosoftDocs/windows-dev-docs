---
title: Windows App SDK release channels
description: Learn about the Windows App SDK's release channels.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, project reunion, windows app sdk, release channels
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Windows App SDK release channels

The Windows App SDK provides the three release channels. When you [set up your development environment](set-up-your-development-environment.md), install the release channel that best serves your development scenario.

|   | Channel | Description | Release cadence | Supported? | Latest release |
|---|---|---|---|---|---|
| **✅** | [Stable](stable-channel.md)  | This channel is supported for use by apps in production environments. It only includes stable APIs. | No faster than every four months<br>(+ servicing) | Yes | 0.8.0 (6/24/2021) |
| **❇️** | [Preview](experimental-channel.md) | This channel provides a preview of the next stable release. There may be breaking API changes between a given preview channel release and the next stable release. | Targeting monthly | No | Coming soon |
| **🔄️** | [Experimental](experimental-channel.md) | This channel includes experimental features that are in early stages of development. Experimental features may be removed from the next release, or may never be released. | Targeting monthly | No | 1.0.0-experimental1 (8/9/2021) |

> [!NOTE]
> The Windows App SDK was previously known by the code name **Project Reunion**. Some SDK assets such as the VSIX extension and NuGet packages still use the code name, but these assets will be renamed in a future release. Some areas of the documentation still use **Project Reunion** when referring to an existing asset or a specified earlier release.

## Features available by release channel

The following table shows which features are currently available in each release channel. To learn more about what's coming next, [see our roadmap](https://github.com/microsoft/ProjectReunion/blob/main/docs/roadmap.md).

| Feature | ✅&nbsp;&nbsp;[Stable](stable-channel.md) | ❇️&nbsp;&nbsp;[Preview](preview-channel.md) |🔄️&nbsp;&nbsp;[Experimental](experimental-channel.md) |
|--|--|--|--|
| [MSIX desktop app support](deploy-packaged-apps.md) | :heavy_check_mark: Available | N/A  |  :heavy_check_mark: Available |
| [Unpackaged desktop app support](deploy-unpackaged-apps.md) | :x: Not available |  N/A  |  :heavy_check_mark: Available |
| [UWP app support](../winui/winui3/create-your-first-winui3-app.md) | :x: Not available |  N/A  |  :heavy_check_mark: Available |
| [WinUI 3](../winui/winui3/index.md) |  :heavy_check_mark: Available for MSIX desktop<br>❌ Not available for UWP and unpackaged desktop | N/A  | :heavy_check_mark: Available for MSIX desktop and UWP<br>❌ Not available for unpackaged desktop |
| [Text rendering](dwritecore.md) | :heavy_check_mark: Available| N/A  | :heavy_check_mark: Available |
| [Manage resources](mrtcore/mrtcore-overview.md) | :heavy_check_mark: Available | N/A  | :heavy_check_mark: Available |
| [App lifecycle](applifecycle/applifecycle-rich-activation.md) | :x: Not available | N/A  | :heavy_check_mark: Available |
| [Manage app windows](windowing/windowing-overview.md) | :x: Not available | N/A  | :heavy_check_mark: Available |
| [Push notifications](notifications/push/index.md) | :x: Not available | N/A  | :heavy_check_mark: Available |

## Windows App SDK release policy

The following policies define the servicing you can expect when you use a given Windows App SDK release.

### Release lifecycle

The Windows App SDK has a lifecycle. A lifecycle begins when a version or service is released and ends when it's no longer supported. Knowing key dates in this release lifecycle helps you make informed decisions about when to upgrade or make other changes to your software.

| Windows App SDK version | Original release date  | Latest patch version  | Patch release date | Support level | End of support |
|---|---|---|---|---|---|
|0.8| 6/24/2021 |0.8.2|8/13/2021|Current | 6/24/2022 |
|0.5| 3/29/2021 | 0.5.8 | 7/13/2021 | Maintenance | 11/1/2021 |

### Servicing  

Customers can choose *Current* releases or *Maintenance* releases. Both types of releases receive critical fixes throughout their lifecycle, for security, compatibility, and reliability. You must stay up to date with the latest patches to qualify for support.

The quality of all releases is the same. The only difference is the servicing time frame.

#### Servicing for Current releases  

Current releases are the latest stable release and they receive fixes more frequently. During the servicing period, Windows App SDK current release is updated to improve functional capabilities and mitigate security vulnerabilities.

Functional improvements are typically very targeted, and may address the following:

- Resolve reported crashes.
- Resolve severe performance issues.
- Resolve functional bugs in key scenarios.
- Add support for a new operating system versions

#### Servicing for Maintenance releases

Maintenance releases are no longer the latest stable release, but they will still receive critical fixes. The bar for fixes in Maintenance releases will be higher than the bar for fixes to the Current release.

After the maintenance period ends, the release is out of support.

## Microsoft support for the Windows App SDK

The Windows App SDK is governed by the [Microsoft Modern Lifecycle](/lifecycle/policies/modern).

Although the Windows App SDK is currently backward compatible to Windows 10 version 1809, the Windows App SDK is supported by Microsoft only on the [Windows releases still in support](/lifecycle/products/windows-10-enterprise-and-education).

Support has two key benefits:

- Patches are provided for free, as required for functional or security issues.
- You can [contact Microsoft support](https://support.serviceshub.microsoft.com/supportforbusiness/onboarding?origin=%2Fsupportforbusiness%2Fcreate%3FsapId%3D2510d164-8500-6eba-dda3-5b6ade9cad01) to request help, potentially at a cost.

Support is conditional on using the latest Windows App SDK patch update and a supported operating system.

### End of support

End of support refers to the date when Microsoft no longer provides fixes, updates, or online technical assistance. End of support may also be referred to as 'end of life' or abbreviated 'EOL'. This is the time to make sure you have the latest available update installed.

Updates are cumulative, with each update built upon all of the updates that preceded it. A device needs to install the latest update to remain supported. Updates may include new features, fixes (security and/or non-security), or a combination of both. Not all features in an update will work on all devices. Update availability may vary, for example by country, region, network connectivity, or hardware capabilities (including, for example, free disk space).

Your use of out-of-support Windows App SDK versions may put your applications at risk. You are strongly recommended to not use out-of-support software.

## How to get help

- The Windows App SDK uses GitHub Issues to track bugs and feature requests. Search the existing issues before filing new issues to avoid duplicates. For new issues, [file your bug or feature request](https://github.com/microsoft/ProjectReunion/issues) as a new issue.
- For help and questions about using the Windows App SDK, search for existing questions or post a new question on our [GitHub Discussions page](https://github.com/microsoft/ProjectReunion/discussions).
- Technical support for the use of the Windows App SDK may be available from Microsoft Customer Support Services (CSS). If you are a Premier or Unified Support customer, reach out to your account manager for further assistance. Otherwise, visit the [Support For Business](https://support.serviceshub.microsoft.com/supportforbusiness/onboarding?origin=%2Fsupportforbusiness%2Fcreate%3FsapId%3D2510d164-8500-6eba-dda3-5b6ade9cad01) site to open a new support case for the Windows App SDK.  

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Experimental channel](experimental-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)