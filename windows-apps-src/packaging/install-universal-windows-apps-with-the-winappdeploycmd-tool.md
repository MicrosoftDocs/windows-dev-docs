---
ms.assetid: 6AA037C0-35ED-4B9C-80A3-5E144D7EE94B
title: Install apps with the WinAppDeployCmd.exe tool
description: Windows Application Deployment (WinAppDeployCmd.exe) is a command line tool that can use to deploy a Universal Windows Platform (UWP) app from a Windows 10 PC to any Windows 10 device.
ms.date: 09/30/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Install apps with the WinAppDeployCmd.exe tool

Windows Application Deployment (WinAppDeployCmd.exe) is a command line tool that can use to deploy a Universal Windows Platform (UWP) app from a Windows 10 PC to any Windows 10 device. You can use this tool to deploy an app package when the Windows 10 device is connected by USB or available on the same subnet without needing Microsoft Visual Studio or the solution for that app. You can also deploy the app without packaging first to a remote PC or Xbox One. This article describes how to install UWP apps using this tool.

You just need the Windows 10 SDK installed to run the WinAppDeployCmd tool from a command prompt or a script file. When you install an app with WinAppDeployCmd.exe, this uses the .appx/.msix file or AppxManifest(for loose files) to side-load your app onto a Windows 10 device. This command does not install the certificate required for your app. To run the app, the Windows 10 device must be in developer mode or already have the certificate installed.

To deploy to mobile devices, you must first create a package. For more information, see [here](/windows/msix/package/packaging-uwp-apps).

The **WinAppDeployCmd.exe** tool is located here on your Windows 10 PC: **C:\\Program Files (x86)\\Windows Kits\\10\\bin\\&lt;SDK Version&gt;\\x86\\WinAppDeployCmd.exe** (based on your installation path for the SDK).

> [!NOTE]
> In version 15063 and later of the SDK, the SDK is installed side by side within version-specific folders. Previous SDKs (prior to and including 14393) are written directly to the parent folder.

First, connect your Windows 10 device to the same subnet or connect it directly to your Windows 10 machine with a USB connection. Then use the following syntax and examples of this command later in this article to deploy your UWP app:

## WinAppDeployCmd syntax and options

This is the general syntax used for **WinAppDeployCmd.exe**:

```CMD
WinAppDeployCmd command -option <argument>
```

Here are some additional syntax examples for using various commands:

```CMD
WinAppDeployCmd devices
WinAppDeployCmd devices <x>
WinAppDeployCmd install -file <path> -ip <address>
WinAppDeployCmd install -file <path> -guid <address> -pin <p>
WinAppDeployCmd install -file <path> -ip <address> -dependency <a> <b> 
WinAppDeployCmd install -file <path> -guid <address> -dependency <a> <b>
WinAppDeployCmd uninstall -file <path>
WinAppDeployCmd uninstall -package <name>
WinAppDeployCmd update -file <path>
WinAppDeployCmd list -ip <address>
WinAppDeployCmd list -guid <address>
WinAppDeployCmd deployfiles -file <path> -remotedeploydir <remoterelativepath> -ip <address>
WinAppDeployCmd registerfiles -remotedeploydir <remoterelativepath> -ip <address>
WinAppDeployCmd addcreds -credserver <server> -credusername <username> -credpassword <password> -ip <address>
WinAppDeployCmd getcreds -credserver <server> -ip <address>
WinAppDeployCmd deletecreds -credserver <server> -ip <address>
```

You can install or uninstall an app on the target device, or you can update an app that's already installed. To keep data or settings saved by an app that's already installed, use the **update** options instead of the **install** options.

The following table describes the commands for **WinAppDeployCmd.exe**.

| **Command**  | **Description**                                                     |
|--------------|---------------------------------------------------------------------|
| devices      | Show the list of available network devices.                         |
| install      | Install a UWP app package to the target device.                     |
| update       | Update a UWP app that is already installed on the target device.    |
| list         | Show the list of UWP apps installed on the specified target device. |
| uninstall    | Uninstall the specified app package from the target device.         |
| deployfiles  | Copy over loose file app at the target path to the remote relative path on the device.|
| registerfiles| Register the loose file app at the remote deploy directory.         |
| addcreds     | Add credentials to an Xbox to allow it to access a network location for app registration.|
| getcreds     | Get network credentials for the target uses when running an application from a network share.|
| deletecreds  | Delete network credentials the target uses when running an application from a network share.|

The following table describes the options for **WinAppDeployCmd.exe**.

| **Command**  | **Description**  |
|--------------|------------------|
| -h (-help)       | Show the commands, options and arguments. |
| -ip              | IP address of the target device. |
| -g (-guid)       | Unique identifier of the target device.|
| -d (-dependency) | (Optional) Specifies the dependency path for each of the package dependencies. If no path is specified, the tool searches for dependencies in the root directory for the app package and the SDK directories.|
| -f (-file)       | File path for the app package to install, update or uninstall.|
| -p (-package)    | The full package name for the app package to uninstall. (You can use the list command to find the full names for packages already installed on the device) |
| -pin             | A pin if it is required to establish a connection with the target device. (You will be prompted to retry with the -pin option if authentication is required) |
| -credserver      | The server name of the network credentials for use by the target. |
| -credusername    | The user name of the network credentials for use by the target. |
| -credpassword    | The password of the network credentials for use by the target. |
| -connecttimeout  | The timeout in seconds used when connecting to the device. |
| -remotedeploydir | Relative directory path/name to copy files over to on the remote device; This will be a well-known, automatically determined remote deployment folder. |
| -deleteextrafile | Switch to indicate whether existing files in the remote directory should be purged to match the source directory. |

The following table describes the options for **WinAppDeployCmd.exe**.

| **Argument**           | **Description**                                                              |
|------------------------|------------------------------------------------------------------------------|
| &lt;x&gt;              | Timeout in seconds. (Default is 10)                                          |
| &lt;address&gt;        | IP address or unique identifier of the target device.                        |
| &lt;a&gt;&lt;b&gt; ... | Dependency path for each of the app package dependencies.                    |
| &lt;p&gt;              | An alpha-numeric pin shown in the device settings to establish a connection. |
| &lt;path&gt;           | File system path.                                                            |
| &lt;name&gt;           | Full package name for the app package to uninstall.                          |
| &lt;server&gt;         | Server on the file network.                                                  |
| &lt;username&gt;       | User for the credentials with access to the server on the file network.      |
| &lt;password&gt;       | Password for the credentials with access to the server on the files network. |
| &lt;remotedeploydir&gt;| Directory on device relative to the deployment location                      |

## WinAppDeployCmd.exe examples

Here are some examples of how to deploy from the command-line using the syntax for **WinAppDeployCmd.exe**.

Shows the devices that are available for deployment. The command times out in 3 seconds.

``` CMD
WinAppDeployCmd devices 3
```

Installs the app from MyApp.appx package that is in your PC's Downloads directory to a Windows 10 device with an IP address of 192.168.0.1 with a PIN of A1B2C3 to establish a connection with the device

``` CMD
WinAppDeployCmd install -file "Downloads\MyApp.appx" -ip 192.168.0.1 -pin A1B2C3
```

Uninstalls the specified package (based on its full name) from a Windows 10 device with an IP address of 192.168.0.1. You can use the list command to see the full names of any packages that are installed on a device.

``` CMD
WinAppDeployCmd uninstall -package Company.MyApp_1.0.0.1_x64__qwertyuiop -ip 192.168.0.1
```

Updates the app that is already installed on the Windows 10 device with an IP address of 192.168.0.1 using the specified app package.

``` CMD
WinAppDeployCmd update -file "Downloads\MyApp.appx" -ip 192.168.0.1
```

Deploys the files of an app to a PC or Xbox with an IP address of 192.168.0.1 in the same folder as the AppxManifest to the app1_F5 directory under the deployment path of the device.

``` CMD
WinAppDeployCmd deployfiles -file "C:\apps\App1\AppxManifest.xml" -remotedeploydir app1_F5 -ip 192.168.0.1
```

Registers the app at the app1_F5 directory under the deployment path of the PC or Xbox at 192.168.0.1.

``` CMD
WinAppDeployCmd registerfiles -file app1_F5 -ip 192.168.0.1
```

## Using WinAppDeployCmd to set up Run from PC deployment on Xbox One

Run from PC allows you to deploy a UWP application to an Xbox One without copying the binaries over, instead the binaries are hosted on a network share on the same network as the Xbox.  In order to do this, you need a developer unlocked Xbox One, and a loose file UWP application on a network drive that the Xbox can access.

Run this to register the app:

``` CMD
WinAppDeployCmd registerfiles -ip <Xbox One IP> -remotedeploydir <location of app> -username <user for network> -password <password for user>

ex. WinAppDeployCmd register files -ip 192.168.0.1 -remotedeploydir \\driveA\myAppLocation -username admin -password A1B2C3
```
