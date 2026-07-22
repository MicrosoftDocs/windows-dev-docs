---
title: Deploy an app through loose file registration
description: Use loose file layout registration to validate and share your Windows app during development without building a full MSIX package.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Deploy an app through loose file registration

Loose file registration lets you validate and share your app during development without going through the full packaging process. Register a folder containing your app's content, and Windows treats it as an installed package.

> [!WARNING]
> Loose file layout registration is for development and testing only. Do not use this approach for production distribution. Always perform final validation on a packaged app that is signed with a trusted certificate.

## What is a loose file layout?

A loose file layout places your app's content in a folder instead of building an MSIX package. The package contents are "loosely" available in a folder and not packaged.

## Advantages

- **Quick validation** — because the app files are already unpacked, you can register the layout and launch the app immediately. Use the app as designed without waiting for a packaging step.
- **Easy network distribution** — if the loose files are on a network share, you can send the share path to other users who have network access. They register the layout and run the app, which lets multiple people validate the app concurrently.
- **Collaboration** — loose file registration lets developers and designers work on visual assets while the app is registered. Users see updated static assets when they relaunch the app. To update code or dynamically created content, you must recompile the app.

## Prerequisites

- Your devices must run Windows 10 version 1809 (build 17763) or later.
- Enable [developer mode](/windows/apps/get-started/enable-your-device-for-development) and [device discovery](/windows/apps/get-started/enable-your-device-for-development#device-discovery) on all devices.

> [!IMPORTANT]
> Loose file registration is available only on devices that support the SMB (Network Share) protocol.

## Register with WinAppDeployCmd

If you use SDK tools corresponding to Windows 10 version 1809 or later, use the `WinAppDeployCmd` command in a command prompt:

```console
WinAppDeployCmd.exe registerfiles -remotedeploydir <NetworkPath> -ip <IPAddress> -pin <PIN>
```

- **NetworkPath** — the path to the app's loose files.
- **IPAddress** — the IP address of the target machine.
- **PIN** — a PIN if required for authentication. See [Device Discovery](/windows/apps/get-started/enable-your-device-for-development#device-discovery) to learn how to get a PIN.

## Register with Windows Device Portal

1. Connect to Device Portal by following the steps in the [Windows Device Portal overview](/windows/uwp/debug-test-perf/device-portal).
2. In the **Apps Manager** tab, select **Register from Network Share**.
3. Enter the network share path to the loose file layout.
4. If the host device doesn't have access to the network share, enter the required credentials when prompted.
5. After registration is complete, launch the app.

You can also register optional packages by selecting the **I want to specify optional packages** checkbox and providing the network share paths.

## Register with PowerShell

Windows PowerShell enables you to register loose file layouts on the local device. To register a layout on a remote device, use one of the other methods.

```powershell
Add-AppxPackage -Register <path-to-manifest-file>
```

## Troubleshooting

| Issue | Solution |
|---|---|
| **Mapped network drives** | Mapped drives are not supported for loose file registration. Use the full network share path instead (for example, `\\server\share\myapp`). |
| **Registration failure** | Verify that the device has access to the file layout. If the layout is on a network share, verify that the device has the necessary permissions. |
| **Visual asset changes not loading** | The app loads visual assets at launch time. If you modify assets after launching, relaunch the app to see the changes. |

## Related content

- [Enable your device for development](/windows/apps/get-started/enable-your-device-for-development)
