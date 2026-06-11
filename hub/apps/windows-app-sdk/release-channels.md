---
title: Windows App SDK release channels
description: Learn about the Experimental, Preview, and Stable release channels used to ship the latest version of the Windows App SDK.
ms.topic: article
ms.date: 05/21/2026
keywords: windows win32, windows app development, project reunion, windows app sdk, release channels
ms.localizationpriority: medium
---

# Windows App SDK release channels

The latest version of the Windows App SDK ships via three release channels: Experimental, Preview, and Stable. The following table provides an overview of these release channels.

|   | Channel | Description | Release cadence | Supported? | Latest release |
|--:|--|--|--|:--:|--|
| **✅** | **Stable** | Production-ready channel intended for apps in market. Includes only stable, supported APIs suitable for long-term use. | Major releases no more than every six months <br/> *(plus minor/patch servicing updates as needed)* | Yes | **2.2.0** <br/> Released: 06/09/2026 <br/> [Release notes](./release-notes/windows-app-sdk-2-0.md?pivots=stable#version-220) |
| **❇️** | **Preview** | Early look at the next Stable release. May introduce breaking API changes before final stabilization. | At least one preview for each Stable release | No | **2.0 Preview2 (2.0.0-preview2)** <br/> Released: 03/31/2026 <br/> [Release notes](./release-notes/windows-app-sdk-2-0.md?pivots=preview#version-20-preview-2-200-preview2) |
| **🔄** | **Experimental** | Early-stage features under active development. APIs may change, be removed, or never ship. Intended for exploration and feedback only. | Published as needed to gather feedback on prototypes and early designs | No | **2.2 Experimental 9 (2.2.2-experimental9)** <br/> Released: 06/09/2026 <br/> [Release notes](./release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-22-experimental-9-222-experimental9) |


For a comprehensive list of all current and previous releases of the Windows App SDK, including download locations, see [Downloads for the Windows App SDK](downloads.md).

## Features available by release channel

The following table shows which features are currently available in each release channel.

| Feature                                                                                                                  | ✅ Stable | ❇️ Preview | 🔄️ Experimental |
| ------------------------------------------------------------------------------------------------------------------------ | ----------------------------------------- | --------------------------------------------- | -------------------------------------------------------- |
| [Deployment guide for framework-dependent packaged apps](deploy-packaged-apps.md)                                        | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [Deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md) | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [WinUI](../winui/winui3/index.md)                                                                                      | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [Text rendering](dwritecore.md)                                                                                          | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [Manage resources](mrtcore/mrtcore-overview.md)                                                                          | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [App lifecycle: App instancing](applifecycle/applifecycle-instancing.md)                                                 | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [App lifecycle: Rich activation](applifecycle/applifecycle-rich-activation.md)                                           | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [App lifecycle: Power management](applifecycle/applifecycle-power.md)                                                    | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [Manage app windows](windowing/windowing-overview.md)                                                                    | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [Push notifications](notifications/push-notifications/index.md)                                                          | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [App notifications](notifications/app-notifications/index.md)                                                            | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |
| [Windows Widgets](../design/widgets/index.md)                                                                            | :heavy_check_mark: Available              | :heavy_check_mark: Available                  | :heavy_check_mark: Available                             |

## Windows App SDK release servicing policy

The following policies define the servicing you can expect when you use a given Windows App SDK release.

### Release lifecycle

The Windows App SDK has a lifecycle. A lifecycle begins when a version or service is released and ends when it's no longer supported. Knowing key dates in this release lifecycle helps you make informed decisions about when to upgrade or make other changes to your software.

| Windows App SDK version | Original release date | Latest patch version | Patch release date | Support level  | End of servicing |
| ----------------------- | --------------------- | -------------------- | ------------------ | -------------- | ---------------- |
| 2.0                     | 04/29/2026            | 2.1.3                | 05/21/2026         | Current        | 04/29/2027       |
| 1.8                     | 09/09/2025            | 1.8.260529003        | 06/09/2026         | Maintenance    | 09/09/2026       |
| 1.7                     | 03/18/2025            | 1.7.260224002        | 03/10/2026         | Out of Support | 03/18/2026       |
| 1.6                     | 09/04/2024            | 1.6.250602001        | 06/10/2025         | Out of Support | 09/04/2025       |
| 1.5                     | 02/29/2024            | 1.5.250108004        | 01/15/2025         | Out of Support | 02/28/2025       |
| 1.4                     | 08/29/2023            | 1.4.240802001        | 08/13/2024         | Out of Support | 08/29/2024       |
| 1.3                     | 04/12/2023            | 1.3.230724000        | 07/25/2023         | Out of Support | 04/12/2024       |
| 1.2                     | 11/10/2022            | 1.2.230313.1         | 03/15/2023         | Out of Support | 11/10/2023       |
| 1.1                     | 05/24/2022            | 1.1.5                | 09/14/2022         | Out of Support | 05/24/2023       |
| 1.0                     | 11/16/2021            | 1.0.4                | 06/14/2022         | Out of Support | 11/16/2022       |
| 0.8                     | 06/24/2021            | 0.8.12               | 08/03/2022         | Out of Support | 06/24/2022       |
| 0.5                     | 03/29/2021            | 0.5.9                | 08/10/2021         | Out of Support | 11/01/2021       |

### Servicing  

Customers can choose *Current* releases or *Maintenance* releases. Both types of releases receive critical fixes throughout their lifecycle, for security, compatibility, and reliability. You must stay up to date with the latest patches to qualify for support.

The quality of all releases is the same. The only difference is the servicing time frame.

#### Servicing for Current releases  

Current releases are the latest stable release and they receive fixes more frequently. During the servicing period, Windows App SDK current release is updated to improve functional capabilities and mitigate security vulnerabilities.

Functional improvements are typically very targeted, and may address the following:

- Resolve reported crashes.
- Resolve severe performance issues.
- Resolve functional bugs in key scenarios.
- Add support for a new operating system versions.

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

- [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI project](../winui/winui3/create-your-first-winui3-app.md)
