---
author: awkoren
Description: Run the Desktop Converter App to convert a Windows desktop application (like Win32, WPF, and Windows Forms) to a Universal Windows Platform (UWP) app.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge Desktop App Converter
ms.author: alkoren
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 74c84eb6-4714-4e12-a658-09cb92b576e3
---

# Desktop to UWP Bridge: Desktop App Converter

[Get the Desktop App Converter](https://aka.ms/converter)

The Desktop App Converter (DAC) is a tool that enables you to bring your existing desktop apps written for .NET 4.6.1 or Win32 to the Universal Windows Platform (UWP). You can run your desktop installers through the converter in an unattended (silent) mode and obtain an AppX package that you can install by using the Add-AppxPackage PowerShell cmdlet on your development machine.

The Desktop App Converter is available now in the [Windows Store](https://aka.ms/converter).

The converter runs the desktop installer in an isolated Windows environment using a clean base image provided as part of the converter download. It captures any registry and file system I/O made by the desktop installer and packages it as part of the output. The converter outputs an AppX with package identity and the ability to call a vast range of WinRT APIs.

## What's new

The latest version of the DAC is v1.0.9.0. New in this update: 

* No-Installer conversion: If your app is installed using xcopy or you’re familiar with the changes your app’s installer makes to the system, you can run conversion without an installer by setting the -Installer parameter to the root directory of your app files.
* App package validation: Use the new `-Verify` flag to validate your converted app package against Desktop Bridge and Store requirements


## System requirements

### Operating system

+ Windows 10 Anniversary Update (10.0.14393.0 and later) Pro or Enterprise edition.

### Hardware configuration

+ 64 bit (x64) processor
+ Hardware-assisted virtualization
+ Second Level Address Translation (SLAT)

### Required resources

+ [Windows Software Development Kit (SDK) for Windows 10](https://go.microsoft.com/fwlink/?linkid=821375)

## Set up the Desktop App Converter

*(These steps are not required for no-installer conversion)*

The Desktop App Converter relies on the latest Windows 10 features. Please ensure that you're on the Windows 10 Anniversary Update (14393.0) or later builds.

1.	Download the [DesktopAppConverter from the Windows Store](https://aka.ms/converter) and the [base image .wim file that matches your build](https://aka.ms/converterimages).  
2.	Run the DesktopAppConverter as admin. You can do this from the start menu by by right-clicking the tile and selecting *Run as administrator* from under *More*, or from the taskbar by right-clicking the tile, right clicking a second time on the app name that pops up, and then selecting *Run as administrator.*
3.	From the app console window, run ```Set-ExecutionPolicy bypass```.
4.	Set up the converter by running ```DesktopAppConverter.exe -Setup -BaseImage .\BaseImage-1XXXX.wim -Verbose``` from the app console window.
5.	If running the previous command prompts you to reboot, please restart your machine.

## Run the Desktop App Converter

+ **Store download**: Use ```DesktopAppConverter.exe``` to run the converter.

### Usage

```CMD
DesktopAppConverter.exe
-Installer <String> [-InstallerArguments <String>] [-InstallerValidExitCodes <Int32>]
-Destination <String>
-PackageName <String>
-Publisher <String>
-Version <Version>
[-ExpandedBaseImage <String>]
[-AppExecutable <String>]
[-AppFileTypes <String>]
[-AppId <String>]
[-AppDisplayName <String>]
[-AppDescription <String>]
[-PackageDisplayName <String>]
[-PackagePublisherDisplayName <String>]
[-MakeAppx]
[-LogFile <String>]
[<CommonParameters>]  
```

### Examples

The following examples shows how to convert a desktop app named *MyApp* by *MyPublisher* to a Windows app package.

#### No-installer conversion 

With No-installer conversion, the `-Installer` parameter points to the root directory of your app files and the `-AppExecutable` parameter is required. 

```cmd
DesktopAppConverter.exe -Installer C:\Installer\MyApp\ -AppExecutable MyApp.exe -Destination C:\Output\MyApp -PackageName "MyApp" -Publisher "CN=MyPublisher" -Version 0.0.0.1 -MakeAppx -Sign -Verbose
```

#### Installer based conversion

With installer based conversion, `-Installer` points to your app's setup installer.

```cmd
DesktopAppConverter.exe -Installer C:\Installer\MyAppSetup.exe -InstallerArguments "/S" -Destination C:\Output\MyApp -PackageName "MyApp" -Publisher "CN=MyPublisher" -Version 0.0.0.1 -MakeAppx -Sign -Verbose
```

## Deploy your converted AppX

Use the [Add-AppxPackage](https://technet.microsoft.com/library/hh856048.aspx) cmdlet in PowerShell to deploy a signed app package (.appx) to a user account. 

You can use the ```-Sign``` flag in the Desktop App Converter (v0.1.24) to auto-sign your converted app. Alternatively, refer to [Sign your converted desktop app](desktop-to-uwp-signing.md) to learn how to self-sign AppX packages.

You can also utilize the ```-Register``` parameter of the Add-AppXPackage PowerShell cmdlet to install from a 
folder of unpackaged files during the development process. 

For more information on deploying and debugging your converted app, see [Deploy and debug your converted UWP app](desktop-to-uwp-deploy-and-debug.md). 

## Sign your .Appx Package

The Add-AppxPackage cmdlet requires that the application package (.appx) being deployed must be signed. Use ```-Sign``` flag as part of the converter command line or SignTool.exe, which ships in the Microsoft Windows 10 SDK, to sign the .appx package.

For additional details on how to sign your .appx package, see [Sign your converted desktop app](desktop-to-uwp-signing.md). 

Note: If you try to sign a package using the autogenerated certificate, you'll need to use the default password "123456".

## Modify VFS Folder and Registry Hive (Optional)

The desktop App Converter takes a very conservative approach to filtering out files and system noise in the container.  This is not required, but after conversion you can:

1. Review the VFS folder and delete any files that are not needed by your installer.
2. Review the contents of Reg.dat and delete any keys that are not installed/needed by the app.

If you make any changes to your converted app (including the ones above), you don't need to run the Converter again; you can manually repackage your app using the MakeAppx tool and the appxmanifest.xml file the DAC generates for your app. For help, see [Manually convert your app to UWP using the Desktop Bridge](desktop-to-uwp-manual-conversion.md).

## Caveats

1. The Windows 10 build on the host machine must match the base image that you obtained as part of the Desktop App Converter download.  
2. Ensure that the desktop installer is in an independent directory, because the converter copies all of the directory's content to the isolated Windows environment.  
3. Currently, the Desktop App Converter supports running the conversion process on a 64-bit operating system only. You can deploy the converted .appx packages to a 64-bit (x64) OS only.  
4. Desktop App Converter requires the desktop installer to run under unattended mode. Ensure that you pass the silent flag for your installer to the converter by using the *-InstallerArguments* parameter.
5. Publishing public SxS Fusion assemblies won't work. During install, an application can publish public side-by-side Fusion assemblies, accessible to any other process. During process activation context creation, these assemblies are retrieved by a system process named CSRSS.exe. When this is done for a converted process, activation context creation and module loading of these assemblies will fail. Inbox assemblies, like ComCtl, are shipped with the OS, so taking a dependency on them is safe. The SxS Fusion assemblies are registered in the following locations:
  + Registry: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SideBySide\Winners`
  + File System: %windir%\\SideBySide

## Known issues

* We are currently investigating the following errors occuring on some OS builds:
    
    * ```E_CREATTING_ISOLATED_ENV_FAILED```
    * ```E_STARTING_ISOLATED_ENV_FAILED```
    
    If you're running into either of these errors, please ensure you are using a valid base image from the [download center](https://aka.ms/converterimages). If you’re using a valid .wim, please send us your logs at converter@microsoft.com to help us investigate. 

* If you receive a Windows Insider flight on a developer machine that previously had the Desktop App Converter installed, you may receive the error `New-ContainerNetwork: The object already exists` when you setup the new base image. As a workaround, run the command `Netsh int ipv4 reset` from an elevated command prompt, then reboot your machine. 

* A .NET app compiled with "AnyCPU" build option will fail to install if the main executable or any of the dependencies were placed under "Program Files" or "Windows\System32". As a workaround, please use your architecture specific desktop installer (32 bit or 64 bit) to successfully generate an AppX package.

## Telemetry from Desktop App Converter

Desktop App Converter may collect information about you and your use of the software and send this info to Microsoft. You can learn more about Microsoft's data collection and use in the product documentation and in the [Microsoft Privacy Statement](http://go.microsoft.com/fwlink/?LinkId=521839). You agree to comply with all applicable provisions of the Microsoft Privacy Statement.

By default, telemetry will be enabled for the Desktop App Converter. Add the following registry key to configure telemetry to a desired setting:  

```cmd
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\DesktopAppConverter
```
+ Add or edit the *DisableTelemetry* value by using a DWORD set to 1.
+ To enable telemetry, remove the key or set the value to 0.

## Desktop App Converter usage

Here's a list of parameters to the Desktop App Converter. You can also view this list by running:   

```CMD
Get-Help DesktopAppConverter.exe -detailed
```

### Setup Parameters  

|Parameter|Description|
|---------|-----------|
|```-Setup [<SwitchParameter>]``` | Runs DesktopAppConverter in setup mode. Setup mode supports expanding a provided base image.|
|```-BaseImage <String>``` | Full path to an unexpanded base image. This parameter is required if -Setup is specified.|
|```-LogFile <String>``` [optional] | Specifies a log file. If omitted, a log file temporary location will be created.|
|```-NatSubnetPrefix <String>``` [optional] | Prefix value to be used for the Nat instance. Typically, you would want to change this only if your host machine is attached to the same subnet range as the converter's NetNat. You can query the current converter NetNat config by using the **Get-NetNat** cmdlet. |
|```-NoRestart [<SwitchParameter>]``` | Don't prompt for reboot when running setup (reboot is required to enable the container feature). |

### Conversion Parameters  

|Parameter|Description|
|---------|-----------|
|```-AppInstallPath <String> [optional]``` | The full path to your application's root folder for the installed files if it were installed (e.g., "C:\Program Files (x86)\MyApp").| 
|```-Destination <String>``` | The desired destination for the converter's appx output - DesktopAppConverter can create this location if it doesn't already exist.|
|```-Installer <String>``` | The path to the installer for your application - must be able to run unattended/silently. No-installer conversion, this is the path to the root directory of your app files. |
|```-InstallerArguments <String>``` [optional] | A comma-separated list or string of arguments to force your installer to run unattended/silently. This parameter is optional if your installer is an msi. To get a log from your installer, supply the logging argument for the installer here and use the path ```<log_folder>```, which is a token that the converter replaces with the appropriate path. <br><br>**NOTE: The unattended/silent flags and log arguments will vary between installer technologies.** <br><br>An example usage for this parameter: ```-InstallerArguments "/silent /log <log_folder>\install.log"``` Another example that doesn't produce a log file may look like: ```-InstallerArguments "/quiet", "/norestart"``` Again, you must literally direct any logs to the token path ```<log_folder>``` if you want the converter to capture it and put it in the final log folder.|
|```-InstallerValidExitCodes <Int32>``` [optional] | A comma-separated list of exit codes that indicate your installer ran successfully (for example: 0, 1234, 5678).  By default this is 0 for non-msi, and 0, 1641, 3010 for msi.|

### Appx Identity Parameters  

|Parameter|Description|
|---------|-----------|
|```-PackageName <String>``` | The name of your Universal Windows App package
|```-Publisher <String>``` | The publisher of your Universal Windows App package
|```-Version <Version>``` | The version number for your Universal Windows App package

### Optional Appx Manifest Parameters  

|Parameter|Description|
|---------|-----------|
|```-AppExecutable <String> [optional]``` [optional] | The name of your application's main executable (eg "MyApp.exe"). This parameter is required for a no-installer conversion. |
|```-AppFileTypes <String>``` [optional] | A comma-separated list of file types which the application will be associated with (eg. ".txt, .doc", without the quotes).|
|```-AppId <String>``` [optional] | Specifies a value to set Application Id to in the appx manifest. If it is not specified, it will be set to the value passed in for *PackageName*.|
|```-AppDisplayName <String>``` [optional] | Specifies a value to set Application Display Name to in the appx manifest. If it is not specified, it will be set to the value passed in for *PackageName*. |
|```-AppDescription <String>``` [optional] | Specifies a value to set Application Description to in the appx manifest. If it is not specified, it will be set to the value passed in for *PackageName*.|
|```-PackageDisplayName <String>``` [optional] | Specifies a value to set Package Display Name to in the appx manifest. If it is not specified, it will be set to the value passed in for *PackageName*. |
|```-PackagePublisherDisplayName <String>``` [optional] | Specifies a value to set Package Publisher Display Name to in the appx manifest. If it is not specified, it will be set to the value passed in for *Publisher*. |

### Other Conversion Parameters  

|Parameter|Description|
|---------|-----------|
|```-ExpandedBaseImage <String>``` [optional] | Full path to an already expanded base image.|
|```-MakeAppx [<SwitchParameter>]``` [optional] | A switch that, when present, tells this script to call MakeAppx on the output. |
|```-LogFile <String>``` [optional] | Specifies a log file. If omitted, a log file temporary location will be created. |
| ```-Sign [<SwitchParameter>] [optional]``` | Tells this script to sign the output appx. This switch should be present alongside the switch ```-MakeAppx```. 
|```<Common parameters>``` | This cmdlet supports the common parameters: *Verbose*, *Debug*, *ErrorAction*, *ErrorVariable*, *WarningAction*, *WarningVariable*, *OutBuffer*, *PipelineVariable*, and *OutVariable*. For more info, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216). |
| ```-Verify [<SwitchParameter>] [optional]``` | A switch that, when present, tells the DAC to validate the converted app package against Desktop Bridge and Windows Store requirements. The result is a validation report "VerifyReport.xml", which is best visualized in a browser. This switch should be present alongside the switch `-MakeAppx`.

### Cleanup Parameters

|Parameter|Description|
|---------|-----------|
|```Cleanup [<Option>]``` | Runs cleanup for the DesktopAppConverter artifacts. There are 3 valid options for the Cleanup mode. |
|```Cleanup All``` | Deletes all expanded base images, removes any temporary converter files, removes the container network, and disables the optional Windows feature, Containers. |
|```Cleanup WorkDirectory``` | Removes all the temporary converter files. |
|```Cleanup ExpandedImage``` | Deletes all the expanded base images installed on your host machine. |

### Package Architecture

The Desktop App Converter now supports creation of both x86 and x64 app packages that you can install and run on x86 and amd64 machines. Note the Desktop App Converter still needs to run on an AMD64 machine to perform a successful conversion.

|Parameter|Description|
|---------|-----------|
|```-PackageArch <String>``` | Generates a package with the specified architecture. Valid options are 'x86' or 'x64'; for example, -PackageArch x86. This parameter is optional. If unspecified, the DesktopAppConverter will try to auto-detect package architecture. If auto-detection fails, it will default to x64 package. 

### Running the PEHeaderCertFixTool

During the conversion process, the DesktopAppConverter automatically runs the PEHeaderCertFixTool in order to fixup any corrupted PE headers. However, you can also run the PEHeaderCertFixTool on a UWP appx, loose files or a specific binary. Example usage: 

```CMD
PEHeaderCertFixTool.exe <binary file>|<.appx package>|<folder> [/c] [/v]
 /c   -- check for corrupted certificate but do not fix (optional)
 /v   -- verbose (optional)
example1: PEHeaderCertFixTool app.exe
example2: PEHeaderCertFixTool c:\package.appx /c
example3: PEHeaderCertFixTool c:\myapp /c /v
```

## Language support

The Desktop App Converter does not support Unicode; thus, no Chinese characters or non-ASCII characters can be used with the tool.

## See also

+ [Bringing Desktop Apps to the UWP Using Desktop App Converter](https://channel9.msdn.com/events/Build/2016/P504)
+ [Project Centennial: Bringing Existing Desktop Applications to the Universal Windows Platform](https://channel9.msdn.com/events/Build/2016/B829)  