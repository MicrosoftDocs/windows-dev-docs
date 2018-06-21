---
author: normesta
Description: This article contains known issues with the Desktop Bridge.
Search.Product: eADQiWindows 10XVcnh
title: Known Issues (Desktop Bridge)
ms.author: normesta
ms.date: 06/20/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 71f8ffcb-8a99-4214-ae83-2d4b718a750e
ms.localizationpriority: medium
---

# Known Issues (Desktop Bridge)

This article contains known issues with the Desktop Bridge.

<a id="app-converter" />

## Known Issues with the Desktop App Converter

### E_CREATING_ISOLATED_ENV_FAILED an E_STARTING_ISOLATED_ENV_FAILED errors    

If you receive either of these errors, make sure that you're using a valid base image from the [download center](https://aka.ms/converterimages).
If you’re using a valid base image, try using ``-Cleanup All`` in your command.
If that does not work, please send us your logs at converter@microsoft.com to help us investigate.

### New-ContainerNetwork: The object already exists error

You might receive this error when you setup a new base image. This can happen if you have a Windows Insider flight on a developer machine that previously had the Desktop App Converter installed.

To resolve this issue, try running the command `Netsh int ipv4 reset` from an elevated command prompt, and then reboot your machine.

### Your .NET app is compiled with the "AnyCPU" build option and fails to install

This can happen if the main executable or any of the dependencies were placed anywhere in the **Program Files** or **Windows\System32** folder hierarchy.

To resolve this issue, try using your architecture-specific desktop installer (32 bit or 64 bit) to generate a Windows app package.

### Publishing public side-by-side Fusion assemblies won't work

 During install, an application can publish public side-by-side Fusion assemblies, accessible to any other process. During process activation context creation, these assemblies are retrieved by a system process named CSRSS.exe. When this is done for a converted process, activation context creation and module loading of these assemblies will fail. The side-by-side Fusion assemblies are registered in the following locations:
  + Registry: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SideBySide\Winners`
  + File System: %windir%\\SideBySide

This is a known limitation and no workaround currently exists. That said, Inbox assemblies, like ComCtl, are shipped with the OS, so taking a dependency on them is safe.

### Error found in XML. The 'Executable' attribute is invalid - The value 'MyApp.EXE' is invalid according to its datatype

This can happen if the executables in your application have a capitalized **.EXE** extension. Although, the casing of this extension shouldn't affect whether your app runs, this can cause the DAC to generate this error.

To resolve this issue, try specifying the **-AppExecutable** flag when you package, and use the lower case ".exe" as the extension of your main executable (For example: MYAPP.exe).    Alternately you can change the casing for all executables in your app from lowercase to uppercase (For example: from .EXE to .exe).

### Corrupted or malformed Authenticode signatures

This section contains details on how to identify issues with Portable Executable (PE) files in your Windows app package that may contain corrupted or malformed Authenticode signatures. Invalid Authenticode signatures on your PE files, which may be in any binary format (e.g. .exe, .dll, .chm, etc.), will prevent your package from being signed properly, and thus prevent it from being deployable from an Windows app package.

The location of the Authenticode signature of a PE file is specified by the Certificate Table entry in the Optional Header Data Directories and the associated Attribute Certificate Table. During signature verification, the information specified in these structures is used to locate the signature on a PE file. If these values get corrupted then it is possible for a file to appear to be invalidly signed.

For the Authenticode signature to be correct, the following must be true of the Authenticode signature:

- The start of the **WIN_CERTIFICATE** entry in the PE file cannot extend past the end of the executable
- The **WIN_CERTIFCATE** entry should be located at the end of the image
- The size of the **WIN_CERTIFICATE** entry must be positive
- The **WIN_CERTIFICATE**entry must start after the **IMAGE_NT_HEADERS32** structure for 32-bit executables and IMAGE_NT_HEADERS64 structure for 64-bit executables

For more details, please refer to the [Authenticode Portal Executable specification](http://download.microsoft.com/download/9/c/5/9c5b2167-8017-4bae-9fde-d599bac8184a/Authenticode_PE.docx) and the [PE file format specification](https://msdn.microsoft.com/windows/hardware/gg463119.aspx).

Note that SignTool.exe can output a list of the corrupted or malformed binaries when attempting to sign an Windows app package. To do this, enable verbose logging by setting the environment variable APPXSIP_LOG to 1 (e.g., ```set APPXSIP_LOG=1``` ) and re-run SignTool.exe.

To fix these malformed binaries, ensure they conform to the requirements above.

## You receive the error	MSB4018	The "GenerateResource" task failed unexpectedly

This can happen when trying to convert satellite assemblies to Package Resource Index (PRI) files.

We are aware of this issue and are working on a more long term solution. As a temporary workaround, you can disable the resource generator by adding this line of XML to the first PropertyGroup element in hosting project file:

``<AppxGeneratePrisForPortableLibrariesEnabled>false</AppxGeneratePrisForPortableLibrariesEnabled>``

## Blue screen with error code 0x139 (KERNEL_SECURITY_CHECK_FAILURE)

After installing or launching certain apps from the Microsoft Store, your machine may unexpectedly reboot with the error: **0x139 (KERNEL\_SECURITY\_CHECK\_ FAILURE)**.

Known affected apps include Kodi, JT2Go, Ear Trumpet, Teslagrad, and others.

A [Windows update (Version 14393.351 - KB3197954)](https://support.microsoft.com/kb/3197954) was released on 10/27/16 that includes important fixes that address this issue. If you encounter this problem, update your machine. If you are not able to update your PC because your machine restarts before you can log in, you should use system restore to recover your system to a point earlier than when you installed one of the affected apps. For information on how to use system restore, see [Recovery options in Windows 10](https://support.microsoft.com/help/12415/windows-10-recovery-options).

If updating does not fix the problem or you aren't sure how to recover your PC, please contact [Microsoft Support](https://support.microsoft.com/contactus/).

If you are a developer, you may want to prevent the installation of your packaged application on versions of Windows that do not include this update. Note that by doing this your app will not be available to users that have not yet installed the update. To limit the availability of your app to users that have installed this update, modify your AppxManifest.xml file as follows:

```<TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.14393.351" MaxVersionTested="10.0.14393.351"/>```

Details regarding the Windows Update can be found at:
* https://support.microsoft.com/kb/3197954
* https://support.microsoft.com/help/12387/windows-10-update-history

## Common errors that can appear when you sign your app

### Publisher and cert mismatch causes Signtool error "Error: SignerSign() Failed" (-2147024885/0x8007000b)

The Publisher entry in the Windows app package manifest must match the Subject of the certificate you are signing with.  You can use any of the following methods to view the subject of the cert.

**Option 1: Powershell**

Run the following PowerShell command. Either .cer or .pfx can be used as the certificate file, as they have the same publisher information.

```ps
(Get-PfxCertificate <cert_file>).Subject
```

**Option 2: File Explorer**

Double-click the certificate in File Explorer, select the *Details* tab, and then the *Subject* field in the list. You can then copy the contents.

**Option 3: CertUtil**

Run **certutil** from the the command line on the PFX file and copy the *Subject* field from the output.

```cmd
certutil -dump <cert_file.pfx>
```

<a id="bad-pe-cert" />

### Bad PE certificate (0x800700C1)

This can happen when your package contains a binary that has a corrupted certificate. Here's some of the reasons why this can happen:

* The start of the certificate is not at the end of an image.  

* The size of the certificate isn't positive.

* The certificate start isn't after the `IMAGE_NT_HEADERS32` structure for a 32-bit executable or after the `IMAGE_NT_HEADERS64` structure for a 64-bit executable.

* The certificate pointer isn't properly aligned for a WIN_CERTIFICATE structure.

To find files that contain a bad PE cert, open a **Command Prompt**, and set the environment variable named `APPXSIP_LOG` to a value of 1.

```
set APPXSIP_LOG=1
```

Then, from the **Command Prompt**, sign your application again. For example:

```
signtool.exe sign /a /v /fd SHA256 /f APPX_TEST_0.pfx C:\Users\Contoso\Desktop\pe\VLC.appx
```

Information about files that contain a bad PE cert will appear in the **Console Window**. For example:

```
...

ERROR: [AppxSipCustomLoggerCallback] File has malformed certificate: uninstall.exe

...   
```
## Next Steps

**Find answers to your questions**

Have questions? Ask us on Stack Overflow. Our team monitors these [tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge). You can also ask us [here](https://social.msdn.microsoft.com/Forums/en-US/home?filter=alltypes&sort=relevancedesc&searchTerm=%5BDesktop%20Converter%5D).

**Give feedback or make feature suggestions**

See [UserVoice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial).
