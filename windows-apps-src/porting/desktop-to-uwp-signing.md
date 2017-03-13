---
author: normesta
Description: This article explains how to sign a desktop app you converted to the Universal Windows Platform (UWP).
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge Sign
ms.author: normesta
ms.date: 03/09/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 232c3012-71ff-4f76-a81e-b1758febb596
---

# Desktop to UWP Bridge: Sign

This article explains how to sign a desktop app you converted to the Universal Windows Platform (UWP). You must sign your Windows app package with a certificate before you can deploy it.

## Automatically sign using the Desktop App Converter (DAC)

Use the ```-Sign``` flag when running the DAC to automatically sign your Windows app package. For more details, see [Desktop App Converter Preview](desktop-to-uwp-run-desktop-app-converter.md).

## Manually sign using SignTool.exe

First, create a certificate using MakeCert.exe. If you are asked to enter a password, select "None."

```cmd
C:\> MakeCert.exe -r -h 0 -n "CN=<publisher_name>" -eku 1.3.6.1.5.5.7.3.3 -pe -sv <my.pvk> <my.cer>
```

Next, copy your public and private key information into the certificate using pvk2pfx.exe.

```cmd
C:\> pvk2pfx.exe -pvk <my.pvk> -spc <my.cer> -pfx <my.pfx>
```
Lastly, use SignTool.exe to sign your Windows app package with the certificate.

```cmd
C:\> signtool.exe sign -f <my.pfx> -fd SHA256 -v .\<outputAppX>.appx
```

For additional details, see [How to sign an app package using SignTool](https://msdn.microsoft.com/library/windows/desktop/jj835835.aspx).

All three tools above are included with the Microsoft Windows 10 SDK. To call them directly, call the ```C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat``` script from a command prompt.

## Common errors

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

## See also

- [SignTool](https://msdn.microsoft.com/library/windows/desktop/aa387764.aspx)
- [SignTool.exe (Sign Tool)](https://msdn.microsoft.com/library/8s9b9yaz.aspx)
- [How to sign an app package using SignTool](https://msdn.microsoft.com/library/windows/desktop/jj835835.aspx)
