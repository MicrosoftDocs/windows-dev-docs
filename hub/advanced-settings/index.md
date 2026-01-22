---
title: Advanced Windows Settings
description: Learn about the settings provided in the Advanced page of Windows settings.
ms.reviewer: cinnamon
ms.topic: article
ms.date: 01/22/2026
---

# Advanced Windows Settings

**Advanced Windows settings** is a redesign of the original **For Developers** page in Windows settings with additional settings to help you be more productive.

## What's included

- **Developer Mode**: Unlock tools and features for building, deploying, and testing apps on Windows. See [Settings for developers](developer-mode.md).
- **Sudo for Windows**: Run elevated commands from an unelevated console session. Available on Windows 11, version 24H2 and later. See [Sudo for Windows](sudo/index.md).
- **File Explorer version control integration (PREVIEW)**: View branch, commit, and author details directly in File Explorer. See [File Explorer version control integration](fe-version-control.md).

## Find Advanced settings

- Open Settings and navigate to **System > Advanced**.

## Availability notes

- In Windows 11, version 25H2 and later, the former **For developers** settings are now surfaced under the **Advanced** page.
- Sudo for Windows is available in Windows 11, version 24H2 and later. See [Sudo for Windows](sudo/index.md).
- File Explorer version control integration is currently available via the Windows Insider Beta Channel. See [File Explorer version control integration (PREVIEW)](fe-version-control.md) for enrollment and setup details.

## Troubleshooting

- **Options missing or disabled**: Devices managed by an organization may apply policies that disable Advanced settings toggles. See related [Group Policy guidance](../dev-drive/group-policy.md).
- **Administrator required**: Enabling Developer Mode requires local administrator privileges.
- **Feature not found**: Ensure your device is updated to a supported Windows version. Preview features may require joining a Windows Insider channel.

## Feedback and contributions

- The Windows Advanced Settings system component is open source. Share feedback or feature requests by opening an issue on [GitHub Issues](https://github.com/microsoft/windowsAdvancedSettings/issues).



## FAQ

### Why was For Developers renamed to Advanced?

Most of the settings available within the For Developers page are useful for other advanced Windows users as well. In order to help all users discover these settings, the page was redesigned and renamed to Advanced.

## Windows Advanced Settings system component open source repository

To provide enhanced functionality to the Advanced settings page, there is an open-source system component that enables features like [File Explorer version control integration](fe-version-control.md). We welcome your contributions and feedback, and the source code for the Windows Advanced Settings system component is available on [GitHub](https://github.com/microsoft/windowsAdvancedSettings).
