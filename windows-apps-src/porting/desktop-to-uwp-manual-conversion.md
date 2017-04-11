---
author: normesta
Description: Shows how to manually convert a Windows desktop application (like Win32, WPF, and Windows Forms) to a Universal Windows Platform (UWP) app.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge Manual Conversion
ms.author: normesta
ms.date: 03/09/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: e8c2a803-9803-47c5-b117-73c4af52c5b6
---

# Convert an app manually (Desktop to UWP Bridge)

Using the [Desktop App Converter (DAC)](desktop-to-uwp-run-desktop-app-converter.md) is convenient and automatic, and it's useful if there's any uncertainty about what your installer does. But if your app is installed by using xcopy, or if you're familiar with the changes that your app's installer makes to the system, you may want to create an app package and manifest manually. This article contains the steps for getting started. It also explains how to add unplated assets to your app, which is not covered by the DAC.

Here's how to get started with manual conversion. Alternatively, if you have a .NET app and are using Visual Studio, see the article [Desktop Bridge Packaging Guide for .NET Desktop apps with Visual Studio](desktop-to-uwp-packaging-dot-net.md).  

## Create a manifest by hand

Your _appxmanifest.xml_ file needs to have the following content (at the minimum). Change placeholders that are formatted like \*\*\*THIS\*\*\* to actual values for your application.

```XML
	<?xml version="1.0" encoding="utf-8"?>
	<Package
	   xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
	   xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
	   xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities">
	  <Identity Name="***YOUR_PACKAGE_NAME_HERE***"
		ProcessorArchitecture="x64"
		Publisher="CN=***COMPANY_NAME***, O=***ORGANIZATION_NAME***, L=***CITY***, S=***STATE***, C=***COUNTRY***"
		Version="***YOUR_PACKAGE_VERSION_HERE***" />
	  <Properties>
		<DisplayName>***YOUR_PACKAGE_DISPLAY_NAME_HERE***</DisplayName>
		<PublisherDisplayName>Reserved</PublisherDisplayName>
		<Description>No description entered</Description>
		<Logo>***YOUR_PACKAGE_RELATIVE_DISPLAY_LOGO_PATH_HERE***</Logo>
	  </Properties>
	  <Resources>
		<Resource Language="en-us" />
	  </Resources>
	  <Dependencies>
		<TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.14316.0" MaxVersionTested="10.0.14316.0" />
	  </Dependencies>
	  <Capabilities>
		<rescap:Capability Name="runFullTrust"/>
	  </Capabilities>
	  <Applications>
		<Application Id="***YOUR_PRAID_HERE***" Executable="***YOUR_PACKAGE_RELATIVE_EXE_PATH_HERE***" EntryPoint="Windows.FullTrustApplication">
		  <uap:VisualElements
		   BackgroundColor="#464646"
		   DisplayName="***YOUR_APP_DISPLAY_NAME_HERE***"
		   Square150x150Logo="***YOUR_PACKAGE_RELATIVE_PNG_PATH_HERE***"
		   Square44x44Logo="***YOUR_PACKAGE_RELATIVE_PNG_PATH_HERE***"
		   Description="***YOUR_APP_DESCRIPTION_HERE***" />
		</Application>
	  </Applications>
	</Package>
```

Have unplated assets you'd like to add? See the section on [unplated assets](#unplated-assets) later in this article for details on how.

<span id="make-appx" />
## Run the MakeAppX tool

Use the [App packager (MakeAppx.exe)](https://msdn.microsoft.com/library/windows/desktop/hh446767(v=vs.85).aspx) to generate a Windows app package for your project. MakeAppx.exe is included with the Windows 10 SDK.

To run MakeAppx, first ensure you've created an manifest file as described above.

Next, create a mapping file. The file should start with **[Files]**, then list each of your source files on disk followed by their destination path in the package. Here's an example:

```
[Files]
"C:\MyApp\StartPage.htm"     "default.html"
"C:\MyApp\readme.txt"        "doc\readme.txt"
"\\MyServer\path\icon.png"   "icon.png"
"MyCustomManifest.xml"       "AppxManifest.xml"
```

Finally, run the following command:

```cmd
MakeAppx.exe pack /f mapping_filepath /p filepath.appx
```
<span id="sign-appx" />
## Sign your Windows app package

The Add-AppxPackage cmdlet requires that the application package (.appx) being deployed must be signed. Use [SignTool.exe](https://msdn.microsoft.com/library/windows/desktop/aa387764(v=vs.85).aspx), which ships in the Microsoft Windows 10 SDK, to sign the Windows app package.

Example usage:

```cmd
C:\> MakeCert.exe -r -h 0 -n "CN=<publisher_name>" -eku 1.3.6.1.5.5.7.3.3 -pe -sv <my.pvk> <my.cer>
C:\> pvk2pfx.exe -pvk <my.pvk> -spc <my.cer> -pfx <my.pfx>
C:\> signtool.exe sign -f <my.pfx> -fd SHA256 -v .\<outputAppX>.appx
```
When you run MakeCert.exe and you're asked to enter a password, select **none**. For more info on certificates and signing, see the following:

- [How to: Create Temporary Certificates for Use During Development](https://msdn.microsoft.com/library/ms733813.aspx)
- [SignTool](https://msdn.microsoft.com/library/windows/desktop/aa387764.aspx)
- [SignTool.exe (Sign Tool)](https://msdn.microsoft.com/library/8s9b9yaz.aspx)

<span id="unplated-assets" />
## Add unplated assets

Here's how to optionally configure the 44x44 assets for your app that show up on the taskbar.

1. Obtain the correct 44x44 images and copy them into the folder that contains your images (i.e., Assets).

2. For each 44x44 image, create a copy in the same folder and append *.targetsize-44_altform-unplated* to the file name. You should have two copies of each icon, each named in a specific way. For example, after completing the process, your assets folder might contain *MYAPP_44x44.png* and *MYAPP_44x44.targetsize-44_altform-unplated.png* (note: the former is the icon referenced in the appxmanifest under VisualElements attribute *Square44x44Logo*).

3.	In the AppXManifest, set the BackgroundColor for every icon you are fixing to transparent. This attribute can be found under VisualElements for each application.

4.	Open CMD, change directory to the package's root folder, and create a priconfig.xml file by running the command ```makepri createconfig /cf priconfig.xml /dq en-US```.

5.	Using CMD, staying in the package’s root folder, create the resources.pri file(s) using the command ```makepri new /pr <PHYSICAL_PATH_TO_FOLDER> /cf <PHYSICAL_PATH_TO_FOLDER>\priconfig.xml```. For example, the command for your app might look like ```makepri new /pr c:\MYAPP /cf c:\MYAPP\priconfig.xml```.

6.	Package your Windows app package using the instructions in the next step to see the results.
