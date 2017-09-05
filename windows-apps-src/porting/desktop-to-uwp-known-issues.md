---
author: normesta
Description: This article contains known issues with the Desktop Bridge.
Search.Product: eADQiWindows 10XVcnh
title: Known Issues (Desktop Bridge)
ms.author: normesta
ms.date: 07/18/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 71f8ffcb-8a99-4214-ae83-2d4b718a750e
---

# Known Issues (Desktop Bridge)

This article contains known issues with the Desktop Bridge.

## Blue screen with error code 0x139 (KERNEL_SECURITY_CHECK_FAILURE)

After installing or launching certain apps from the Windows Store, your machine may unexpectedly reboot with the error: **0x139 (KERNEL\_SECURITY\_CHECK\_ FAILURE)**.

Known affected apps include Kodi, JT2Go, Ear Trumpet, Teslagrad, and others.

A [Windows update (Version 14393.351 - KB3197954)](https://support.microsoft.com/kb/3197954) was released on 10/27/16 that includes important fixes that address this issue. If you encounter this problem, update your machine. If you are not able to update your PC because your machine restarts before you can log in, you should use system restore to recover your system to a point earlier than when you installed one of the affected apps. For information on how to use system restore, see [Recovery options in Windows 10](https://support.microsoft.com/help/12415/windows-10-recovery-options).

If updating does not fix the problem or you aren't sure how to recover your PC, please contact [Microsoft Support](https://support.microsoft.com/contactus/).

If you are a developer, you may want to prevent the installation of your Desktop Bridge apps on versions of Windows that do not include this update. Note that by doing this your app will not be available to users that have not yet installed the update. To limit the availability of your app to users that have installed this update, modify your AppxManifest.xml file as follows:

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

<span id="known-issues-anchor" />
## Known issues with C#/VB.NET and C++ UWP projects

If you prefer to use a C# project to package your app, you need to be aware of the following known issues.

- Building the app in Debug mode results in the error: _Microsoft.Net.CoreRuntime.targets(235,5): error : Applications with custom entry point executables are not supported. Check Executable attribute of the Application element in the package manifest._

  To resolve this issue, use Release mode instead.

- Win32 Binaries stored in the root folder of the UWP project are removed in Release. The .NET Native compiler will remove those from the final package, resulting in a manifest validation error since the executable entry point can't be found.

  To resolve this issue, create a subfolder in your project to store win32 binaries.


## You receive the error	MSB4018	The "GenerateResource" task failed unexpectedly

This can happen when trying to convert satellite assemblies to Package Resource Index (PRI) files.

We are aware of this issue and are working on a more long term solution. As a temporary workaround, you can disable the resource generator by adding this line of XML to the first PropertyGroup element in hosting project file:

``<AppxGeneratePrisForPortableLibrariesEnabled>false</AppxGeneratePrisForPortableLibrariesEnabled>``
