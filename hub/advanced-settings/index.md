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

| Category | Setting | Description |
|---|---|---|
| **Taskbar** | End Task | Enable end task in taskbar by right click |
| **File Explorer** | File Explorer | Adjust settings for a more developer-friendly experience using File Explorer |
| | ↳ Show file extensions | Show file extensions in File Explorer |
| | ↳ Show hidden and system files | Show hidden and system files in File Explorer |
| | ↳ Show full path in title bar | Show the full path in the File Explorer title bar |
| | ↳ Show option to run as different user in Start | Show option to run as a different user in the Start menu |
| | ↳ Show empty drives | Show empty drives in File Explorer |
| **File Explorer** | File Explorer + version control | View branch, commit, and author details directly in File Explorer. See [File Explorer version control integration](fe-version-control.md) |
| **File Explorer** | Enable long paths | Remove MAX_PATH limitations from common Win32 file and directory functions |
| **Virtual Workspace** | Remote Desktop | Enable Remote Desktop and ensure machine availability |
| **Virtual Workspace** | Virtual Workspaces | Adjust settings for virtual workspaces |
| **Terminal** | Terminal | Choose the default terminal app to host command-line apps |
| **Terminal** | PowerShell | Turn on these settings to execute PowerShell scripts |
| **Terminal** | Enable sudo | Enable the sudo command. See [Sudo for Windows](sudo/index.md) |
| **For developers** | Developer Mode | Install apps from any source, including loose files. See [Settings for developers](developer-mode.md) |
| **For developers** | Device Portal | Turn on remote diagnostics over local area network connections |
| **Dev Drive** | Dev Drive | A storage volume optimized for performance in developer scenarios |

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
