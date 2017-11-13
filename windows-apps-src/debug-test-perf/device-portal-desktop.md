---
author: PatrickFarley
ms.assetid: 5c34c78e-9ff7-477b-87f6-a31367cd3f8b
title: Device Portal for Desktop
description: Learn how the Windows Device Portal opens up diagnostics and automation on your Windows desktop.
ms.author: pafarley
ms.date: 11/10/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Device Portal for Desktop

Starting in Windows 10, Version 1607, additional developer features are available for desktop. These features are available only when Developer Mode is enabled. For information about how to enable Developer Mode, see [Enable your device for development](../get-started/enable-your-device-for-development.md).

> [!IMPORTANT]
> Sometimes, due to network or compatibility issues, Developer Mode won't install correctly on your device. See the [relevant section of Enable your device for development](https://docs.microsoft.com/windows/uwp/get-started/enable-your-device-for-development#failure-to-install-developer-mode-package) for help troubleshooting these issues.

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

- Apps manager
- File explorer
- Running Processes
- Performance
- Debug
- Event Tracing for Windows (ETW)
- Performance tracing
- Device manager
- Networking
- Crash data
- Features
- Mixed Reality
- Streaming Install Debugger
- Location
- Scratch

## Registry-based configuration for Device Portal

If you would like to select port numbers for Device Portal (such as 80 and 443), you can set the following regkeys:

- Under `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WebManagement\Service`
	- `UseDynamicPorts`: A required DWORD. Set this to 0 in order to retain the port numbers you've chosen.
	- `HttpPort`: A required DWORD. Contains the port number that Device Portal will listen for HTTP connections on.	
	- `HttpsPort`: A required DWORD. Contains the port number that Device Portal will listen for HTTPS connections on.
	
Under the same regkey path, you can also turn off the authentication requirement:
- `UseDefaultAuthorizer` - `0` for disabled, `1` for enabled.  
	- This controls both the basic auth requirement for each connection and the redirect from HTTP to HTTPS.  
	
## Command line options for Device Portal
From an administrative prompt, you can enable and configure parts of Device Portal. To see the latest set of commands supported on your build, you can run `webmanagement /?`

- `sc start webmanagement` or `sc stop webmanagement` 
	- Turn the service on or off. This still requires Developer Mode to be enabled. 
- `-Credentials <username> <password>` 
	- Set a username and password for Device Portal. The username must conform to Basic Auth standards, so cannot contain a colon (:) and should be built out of standard ASCII characters e.g. [a-zA-Z0-9] as browsers do not parse the full character set in a standard way.  
- `-DeleteSSL` 
	- This resets the SSL certificate cache used for HTTPS connections. If you encounter TLS connection errors that cannot be bypassed (as opposed to the expected certificate warning), this option may fix the problem for you. 
- `-SetCert <pfxPath> <pfxPassword>`
	- See [Provisioning Device Portal with a custom SSL certificate](https://docs.microsoft.com/windows/uwp/debug-test-perf/device-portal-ssl) for details.  
	- This allows you to install your own SSL certificate to fix the SSL warning page that is typically seen in Device Portal. 
- `-Debug <various options for authentication, port selection, and tracing level>`
	- Run a standalone version of Device Portal with a specific configuration and visible debug messages. This is most useful for building a [packaged plugin](https://docs.microsoft.com/windows/uwp/debug-test-perf/device-portal-plugin). 
	- See the [MSDN Magazine article](https://msdn.microsoft.com/en-us/magazine/mt826332.aspx) for details on how to run this as System to fully test your packaged plugin. 

