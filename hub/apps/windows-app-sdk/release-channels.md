---
title: Windows App SDK release channels
description: Learn about the Experimental, Preview, and Stable release channels used to ship the latest version of the Windows App SDK.
ms.topic: article
ms.date: 09/06/2022
keywords: windows win32, windows app development, project reunion, windows app sdk, release channels
ms.localizationpriority: medium
---

# Windows App SDK release channels

The latest version of the Windows App SDK ships via three release channels: Experimental, Preview, and Stable. The following table provides an overview of these release channels.

> [!NOTE]
> When you [Install tools for the Windows App SDK](set-up-your-development-environment.md), install the release channel that best serves your development scenario.

|         | Channel                                                 | Description                                                                                                                                                                | Release cadence                                                                    | Supported? | Latest release                                                                                      |
|---------|---------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------|------------|-----------------------------------------------------------------------------------------------------|
| **✅**   | Stable ([release notes](stable-channel.md))             | This channel is supported for use by apps in production environments. It includes only stable APIs.                                                                        | No more frequent than every six months<br>(+ servicing)                                   | Yes        | [1.6.0 (1.6.240829007)](stable-channel.md#version-16) (09/04/2024)                                                 |
| **❇️**  | Preview ([release notes](preview-channel.md))           | This channel provides a preview of the next stable release. There may be breaking API changes between a given preview channel release and the next stable release.         | At least one preview per stable version                                           | No         | [1.6.0-preview2](preview-channel.md#version-16-preview-2-160-preview2) (08/22/2024)                 |
| **🔄️** | Experimental ([release notes](experimental-channel.md)) | This channel includes experimental features that are in early stages of development. Experimental features may be removed from the next release, or may never be released. | As needed when requiring feedback for features in early design or prototype stages | No         | [1.6.0-experimental2](experimental-channel.md#version-16-experimental-160-experimental2) (07/02/2024) |

For a comprehensive list of all current and previous releases of the Windows App SDK, including download locations, see [Downloads for the Windows App SDK](downloads.md).

## Features available by release channel

The following table shows which features are currently available in each release channel.

| Feature | ✅&nbsp;&nbsp;[Stable](stable-channel.md) | ❇️&nbsp;&nbsp;[Preview](preview-channel.md) |🔄️&nbsp;&nbsp;[Experimental](experimental-channel.md) |
|-|-|-|-|
| [Deployment guide for framework-dependent packaged apps](deploy-packaged-apps.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [Deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [WinUI 3](../winui/winui3/index.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [Text rendering](dwritecore.md) | :heavy_check_mark: Available| :heavy_check_mark: Available | :heavy_check_mark: Available |
| [Manage resources](mrtcore/mrtcore-overview.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [App lifecycle: App instancing](applifecycle/applifecycle-instancing.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [App lifecycle: Rich activation](applifecycle/applifecycle-rich-activation.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [App lifecycle: Power management](applifecycle/applifecycle-power.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [Manage app windows](windowing/windowing-overview.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [Push notifications](notifications/push-notifications/index.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [App notifications](notifications/app-notifications/index.md) | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available |
| [Windows Widgets](../design/widgets/index.md)   | :heavy_check_mark: Available | :heavy_check_mark: Available | :heavy_check_mark: Available   |

## Windows App SDK release servicing policy

The following policies define the servicing you can expect when you use a given Windows App SDK release.

### Release lifecycle

The Windows App SDK has a lifecycle. A lifecycle begins when a version or service is released and ends when it's no longer supported. Knowing key dates in this release lifecycle helps you make informed decisions about when to upgrade or make other changes to your software.

| Windows App SDK version | Original release date | Latest patch version | Patch release date | Support level | End of servicing |
|-------------------------|-----------------------|----------------------|--------------------|---------------|----------------  |
| 1.6                     | 09/04/2024            | 1.6.240829007        | 09/04/2024         | Current       | 09/04/2025       |
| 1.5                     | 02/29/2024            | 1.5.240802000        | 08/13/2024         | Maintenance   | 02/28/2025       |
| 1.4                     | 08/29/2023            | 1.4.240802001        | 08/13/2024         | Out of Support| 08/29/2024       |
| 1.3                     | 04/12/2023            | 1.3.230724000        | 07/25/2023         | Out of Support| 04/12/2024       |
| 1.2                     | 11/10/2022            | 1.2.230313.1         | 03/15/2023         | Out of Support| 11/10/2023       |
| 1.1                     | 05/24/2022            | 1.1.5                | 09/14/2022         | Out of Support| 05/24/2023       |
| 1.0                     | 11/16/2021            | 1.0.4                | 06/14/2022         | Out of Support| 11/16/2022       |
| 0.8                     | 6/24/2021             | 0.8.12               | 08/03/2022         | Out of Support| 6/24/2022        |
| 0.5                     | 3/29/2021             | 0.5.9                | 8/10/2021          | Out of Support| 11/1/2021        |

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

- The Windows App SDK uses GitHub Issues to track bugs and feature requests. Search the existing issues before filing new issues to avoid duplicates. For new issues, [file your bug or feature request](https://github.com/microsoft/WindowsAppSDK/issues) as a new issue.
- For help and questions about using the Windows App SDK, search for existing questions or post a new question on our [GitHub Discussions page](https://github.com/microsoft/WindowsAppSDK/discussions).
- Technical support for the use of the Windows App SDK may be available from Microsoft Customer Support Services (CSS). If you're a Premier or Unified Support customer, reach out to your account manager for further assistance. Otherwise, visit the [Support For Business](https://support.serviceshub.microsoft.com/supportforbusiness/onboarding?origin=%2Fsupportforbusiness%2Fcreate%3FsapId%3D2510d164-8500-6eba-dda3-5b6ade9cad01) site to open a new support case for the Windows App SDK.  

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
