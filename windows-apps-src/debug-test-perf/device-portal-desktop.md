---
author: PatrickFarley
ms.assetid: 5c34c78e-9ff7-477b-87f6-a31367cd3f8b
title: Device Portal for Desktop
description: Learn how the Windows Device Portal opens up diagnostics and automation on your Windows desktop.
ms.author: pafarley
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Device Portal for Desktop

Starting in Windows 10, Version 1607, additional developer features are available for desktop. These features are available only when Developer Mode is enabled.

For information about how to enable Developer Mode, see [Enable your device for development](../get-started/enable-your-device-for-development.md).

Device Portal lets you view diagnostic information and interact with your desktop over HTTP from your browser. You can use Device Portal to do the following:
- See and manipulate a list of running processes
- Install, delete, launch, and terminate apps
- Change Wi-Fi profiles, view signal strength, and see ipconfig
- View live graphs of CPU, memory, I/O, network, and GPU usage
- Collect process dumps
- Collect ETW traces 
- Manipulate the isolated storage of sideloaded apps

## Set up device portal on Windows Desktop

### Turn on device portal

In the **Developer Settings** menu, with Developer Mode enabled, you can enable Device Portal.  

When you enable Device Portal, you must also create a username and password for Device Portal. Do not use your Microsoft account or other Windows credentials.  

After Device Portal is enabled, you will see links to it at the bottom of the **Settings** section. Take note of the port number applied to the end of the URL: this port number is randomly generated when Device Portal is enabled, but should remain consistent between reboots of the desktop. If you'd like to set the port numbers manually so they remain permanent, see [Setting port numbers](device-portal-desktop.md#setting-port-numbers).

You can choose from two ways to connect to Device Portal: local host and over the local network (including VPN).

**To connect to Device Portal**

1. In your browser, enter the address shown here for the connection type you're using.

    - Localhost: `http://127.0.0.1:PORT` or `http://localhost:PORT`

    Use this address to view Device Portal locally.
    
    - Local Network: `https://<The IP address of the desktop>:PORT`

    Use this address to connect over a local network.

HTTPS is required for authentication and secure communication.

If you are using Device Portal in a protected environment, like a test lab, where you trust everyone on your local network, have no personal information on the device, and have unique requirements, you can disable authentication. This enables unencrypted communication, and allows anyone with the IP address of your computer to control it.

## Device Portal pages

Device Portal on desktop provides the standard set of pages. For detailed descriptions, see [Windows Device Portal overview](device-portal.md).

- Apps
- Processes
- Performance
- Debugging
- Event Tracing for Windows (ETW)
- Performance tracing
- Devices
- Networking
- App File Explorer 

## Setting port numbers

If you would like to select port numbers for Device Portal (such as 80 and 443), you can set the following regkeys:

- Under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WebManagement\Service
	- UseDynamicPorts: A required DWORD. Set this to 0 in order to retain the port numbers you've chosen.
	- HttpPort: A required DWORD. Contains the port number that Device Portal will listen for HTTP connections on.	
	- HttpsPort: A required DWORD. Contains the port number that Device Portal will listen for HTTPS connections on.

## Failure to install Developer Mode package
Sometimes, due to network or compatibility issues, Developer Mode won't install correctly. The Developer Mode package is required for **remote** deployment to the PC -- using Device Portal from a browser or Device Discovery to enable SSH -- but not for local development.  Even if you encounter these issues, you can still deploy your app locally using Visual Studio. 

See the [Known Issues](https://social.msdn.microsoft.com/Forums/en-US/home?forum=Win10SDKToolsIssues&sort=relevancedesc&brandIgnore=True&searchTerm=%22device+portal%22) forum and the [Developer Mode page](https://docs.microsoft.com/windows/uwp/get-started/enable-your-device-for-development#failure-to-install-developer-mode-package) to find workarounds to these issues and more. 

