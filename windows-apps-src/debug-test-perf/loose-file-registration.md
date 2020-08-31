---
title: Deploy an app through loose file registration
description: This guide shows how to use the loose file layout to validate and share Windows 10 apps without needing to package them.
ms.date: 06/01/2018
ms.topic: article
keywords: windows 10, uwp, device portal, apps manager, deployment, sdk
ms.localizationpriority: medium
---
# Deploy an app through loose file registration 

This guide shows how to use the loose file layout to validate and share Windows 10 apps without needing to package them. Registering loose file layouts allows developers to quickly validate their apps without the need to package and install the apps. 

## What is a loose file layout?

Loose file layout is simply the act of placing app contents in a folder instead of going through the packaging process. The package contents are "loosely" available in a folder and not packaged. 

> [!WARNING]
> Loose file layout registration is for developers and designers to quickly validate their apps during active development. This approach shouldn't be used to "dogfood" or flight the app. We recommend that the final validation be performed on a packaged app that is signed with a trusted certificate. 

## Advantages of loose file registration

- **Quick validation** - Because the app files are already unpacked, users can quickly register the loose file layout and launch the app. Just like a regular app, the user will be able to use the app as it was designed. 
- **Easy in-network distribution** - If the loose files are located in a network share instead of a local drive, developers can send the network share location to other users who have access to the network, and they can register the loose file layout and run the app. This allows for multiple users to validate the app concurrently. 
- **Collaboration** - Loose file registration allows developers and designers to continue working on visual assets while the app is registered. Users will see these changes when they launch the app. Note that you can only change static assets in this manner. If you need to modify any code or dynamically created content, you must re-compile the app.

## How to register a loose file layout

Windows provides multiple developer tools to register loose file layouts on local and remote devices. You can choose from `WinDeployAppCmd` (Windows SDK Tool), Windows Device Portal, PowerShell, and [Visual Studio](./deploying-and-debugging-uwp-apps.md#register-layout-from-network). Below we will go over how to register loose files using these tools. But first, ensure that you have following setup:

- Your devices must be on the Windows 10 Creators Update (Build 14965) or later.
- You will need to enable [developer mode](../get-started/enable-your-device-for-development.md) and [device discovery](../get-started/enable-your-device-for-development.md#device-discovery) on all devices.

> [!IMPORTANT]
> Loose file registration is only available on devices that support the Network Share (SMB) Protocol: Desktop and Xbox. 

### Register with WinDeployAppCmd

If you are using the SDK tools corresponding to the Windows 10 Creators Update (Build 14965) or later, you can use the `WinDeployAppCmd` command in a Command Prompt.

```cmd
WinAppDeployCmd.exe registerfiles -remotedeploydir <Network Path> -ip <IP Address> -pin <target machine PIN>
```

**Network Path** – the path to the app's loose files.

**IP Address** – the IP Address of the target machine.

**target machine PIN** – A PIN, if required, to establish a connection with the target device. You will be prompted to retry with the `-pin` option if authentication is required. See [Device Discovery](../get-started/enable-your-device-for-development.md#device-discovery) to learn how to get a PIN.

### Windows Device Portal

Windows Device Portal is available on all Windows 10 devices and is used by developers to test and validate their work. It caters to all audiences of the developer community with its browser UX and REST endpoints. For more information on Device Portal, see the [Windows Device Portal overview](device-portal.md).

To register the loose file layout in Device Portal, follow these steps.

1. Connect to Device Portal by following the steps in the **Setup** section of the [Windows Device Portal overview](device-portal.md).
1. In the Apps Manager tab, select **Register from Network Share**.
1. Enter the network share path to the loose file layout. 
1. If the host device doesn't have access to the network share, there will be a prompt to enter the required credentials.
1. Once the registration is complete, you can launch the app.

On the Device Portal's Apps Manager page, you can also register optional loose file layouts for your main app by selecting the **I want to specify optional packages** checkbox and then specifying the network share paths of the optional apps. 

### PowerShell 

Windows PowerShell also enables you to register loose file layouts, but only on the local device. If you need to register a layout to a remote device, you will need to use one of the other methods. 

To register the loose file layout, launch PowerShell and enter the following.

```PowerShell
Add-AppxPackage -Register <path to manifest file>
```

## Troubleshooting

### Mapped network drives
Currently, mapped network drives aren't supported for loose file registrations. Refer to the mapped drive with full the network share path.

### Registration failure
The device on which the registration is taking place will need to have access to the file layout. If the file layout is hosted on a network share, ensure that the device has access. 

### Modifications to visual assets aren't being loaded in the app 
The app will load its visual assets at launch time. If modifications were made to the visual assets after launching the app, you must re-launch the app to view the latest changes.