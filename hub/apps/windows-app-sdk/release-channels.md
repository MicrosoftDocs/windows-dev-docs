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
| **🔄️** | [Experimental](experimental-channel.md) | This channel includes experimental features that are in early stages of development. Experimental features may be removed from the next release, or may never be released. | Targeting monthly | No | 0.8.0-preview (5/27/2021) |

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

## Servicing and support

The Windows App SDK has a lifecycle. A lifecycle begins when a version or service is released and ends when it's no longer supported. Knowing key dates in this lifecycle helps you make informed decisions about when to upgrade or make other changes to your software. The Windows App SDK is governed by the [Microsoft Modern Lifecycle](/lifecycle/policies/modern).

This table describes support type, supported patch version and end of support date for the Windows App SDK: 

| Windows App SDK version | Original release date  | Latest patch version  | Patch release date | Support level | End of support |
|---|---|---|---|---|---|
|0.8| 6/24/2021 |0.8.1|7/13/2021|Current | 6/24/2022 |
|0.5| 3/29/2021 | 0.5.8 | 7/13/2021 | Maintenance | 11/1/2021 |

#### Support levels 

Microsoft provides **Current** and **Maintenance** levels of support, which are defined as:

- **Current**:  support means this is the latest current stable release. These releases will receive fixes more frequently.
- **Maintenance**: support means the release is no longer the latest stable release, but it will still receive critical fixes. The bar for fixes in Maintenance releases will be higher than the bar for fixes to the Current release.

Both types of releases receive critical fixes throughout their lifecycle, for security, reliability, or to add support for new operating system versions. You must stay up to date with the latest patches to qualify for support.

#### End of support

End of support refers to the date when Microsoft no longer provides fixes, updates, or online technical assistance. End of support may also be referred to as 'end of life' or abbreviated 'EOL'. This is the time to make sure you have the latest available update installed.

Updates are cumulative, with each update built upon all of the updates that preceded it. A device needs to install the latest update to remain supported. Updates may include new features, fixes (security and/or non-security), or a combination of both. Not all features in an update will work on all devices. Update availability may vary, for example by country, region, network connectivity, or hardware capabilities (including, for example, free disk space).

#### How to get help

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