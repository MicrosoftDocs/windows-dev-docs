---
author: laurenhughes
title: Sign an app package using SignTool
description: Use SignTool to manually sign an app package with a certificate.
ms.author: lahugh
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 171f332d-2a54-4c68-8aa0-52975d975fb1
ms.localizationpriority: medium
---

# Sign an app package using SignTool


**SignTool** is a command line tool used to digitally sign an app package or bundle with a certificate. The certificate can either be created by the user (for testing purposes) or issued by a company (for distribution). Signing an app package provides the user with verification that the app's data has not been modified after it was signed while also confirming the identity of the user or company that signed it. **SignTool** can sign encrypted or unencrypted app packages and bundles.

> [!IMPORTANT] 
> If you used Visual Studio to develop your app, it's recommended that you use the Visual Studio wizard to create and sign your app package. For more information, see [Package a UWP app with Visual Studio](https://msdn.microsoft.com/windows/uwp/packaging/packaging-uwp-apps).

For more information about code signing and certificates in general, see [Introduction to Code Signing](https://msdn.microsoft.com/library/windows/desktop/aa380259.aspx#introduction_to_code_signing).

## Prerequisites
- **A packaged app**  
    To learn more about manually creating an app package, see [Create an app package with the MakeAppx.exe tool](https://msdn.microsoft.com/windows/uwp/packaging/create-app-package-with-makeappx-tool). 

- **A valid signing certificate**  
    For more information about creating or importing a valid signing certificate, see [Create or import a certificate for package signing](https://msdn.microsoft.com/windows/uwp/packaging/create-certificate-package-signing).

- **SignTool.exe**  
    Based on your installation path of the SDK, this is where **SignTool** is on your Windows 10 PC:
    - x86: C:\Program Files (x86)\Windows Kits\10\bin\x86\SignTool.exe
    - x64: C:\Program Files (x86)\Windows Kits\10\bin\x64\SignTool.exe

## Using SignTool

**SignTool** can be used to sign files, verify signatures or timestamps, remove signatures, and more. For the purpose of signing an app package, we will focus on the **sign** command. For full information on **SignTool**, see the [SignTool](https://msdn.microsoft.com/library/windows/desktop/aa387764.aspx) reference page. 

### Determine the hash algorithm
When using **SignTool** to sign your app package or bundle, the hash algorithm used in **SignTool** must be the same algorithm you used to package your app. For example, if you used **MakeAppx.exe** to create your app package with the default settings, you must specify SHA256 when using **SignTool** since that's the default algorithm used by **MakeAppx.exe**.

To find out which hash algorithm was used while packaging your app, extract the contents of the app package and inspect the AppxBlockMap.xml file. To learn how to unpack/extract an app package, see [Extract files from a package or bundle](https://msdn.microsoft.com/windows/uwp/packaging/create-app-package-with-makeappx-tool#extract-files-from-a-package-or-bundle). The hash method is in the BlockMap element and has this format:
```
<BlockMap xmlns="http://schemas.microsoft.com/appx/2010/blockmap" 
HashMethod="http://www.w3.org/2001/04/xmlenc#sha256">
```

This table shows each HashMethod value and its corresponding hash algorithm:


| HashMethod value                              | Hash Algorithm |
|-----------------------------------------------|----------------|
| http://www.w3.org/2001/04/xmlenc#sha256       | SHA256         |
| http://www.w3.org/2001/04/xmldsig-more#sha384 | SHA384         |
| http://www.w3.org/2001/04/xmlenc#sha512       | SHA512         |

> [!NOTE]
> Since **SignTool**'s default algorithm is SHA1 (not available in **MakeAppx.exe**), you must always specify a hash algorithm when using **SignTool**.

### Sign the app package

Once you have all of the prerequisites and you've determined which hash algorithm was used to package your app, you're ready to sign it. 

The general command line syntax for **SignTool** package signing is:
```
SignTool sign [options] <filename(s)>
```

The certificate used to sign your app must be either a .pfx file or be installed in a certificate store.

To sign your app package with a certificate from a .pfx file, use the following syntax:
```
SignTool sign /fd <Hash Algorithm> /a /f <Path to Certificate>.pfx /p <Your Password> <File path>.appx
```
Note that the `/a` option allows **SignTool** to choose the best certificate automatically.

If your certificate is not a .pfx file, use the following syntax:
```
SignTool sign /fd <Hash Algorithm> /n <Name of Certificate> <File Path>.appx
```

Alternatively, you can specify the SHA1 hash of the desired certificate instead of &lt;Name of Certificate&gt; using this syntax:
```
SignTool sign /fd <Hash Algorithm> /sha1 <SHA1 hash> <File Path>.appx
```

Note that some certificates do not use a password. If your certificate does not have a password, omit "/p &lt;Your Password&gt;" from the sample commands.

Once your app package is signed with a valid certificate, you're ready to upload your package to the Store. For more guidance on uploading and submitting apps to the Store, see [App submissions](https://msdn.microsoft.com/windows/uwp/publish/app-submissions).

## Common errors and troubleshooting
The most common types of errors when using **SignTool** are internal and typically look something like this:

```
SignTool Error: An unexpected internal error has occurred.
Error information: "Error: SignerSign() failed." (-2147024885 / 0x8007000B) 
```

If the error code starts with 0x8008, such as 0x80080206 (APPX_E_CORRUPT_CONTENT), the package being signed is invalid. If you get this type of error you must rebuild the package and run **SignTool** again.

**SignTool** has a debug option available to show certificate errors and filtering. To use the debugging feature, place the `/debug` option directly after `sign`, followed by the full **SignTool** command.
```
SignTool sign /debug [options]
``` 

A more common error is 0x8007000B. For this type of error, you can find more information in the event log.
 
To find more information in the event log:
- Run Eventvwr.msc
- Open the event log: Event Viewer (Local) -> Applications and Services Logs -> Microsoft -> Windows -> AppxPackagingOM -> Microsoft-Windows-AppxPackaging/Operational
- Find the most recent error event

The internal error 0x8007000B usually corresponds to one of these values:

| **Event ID** | **Example event string** | **Suggestion** |
|--------------|--------------------------|----------------|
| 150          | error 0x8007000B: The app manifest publisher name (CN=Contoso) must match the subject name of the signing certificate (CN=Contoso, C=US). | The app manifest publisher name must exactly match the subject name of the signing.               |
| 151          | error 0x8007000B: The signature hash method specified (SHA512) must match the hash method used in the app package block map (SHA256).     | The hashAlgorithm specified in the /fd parameter is incorrect. Rerun **SignTool** using hashAlgorithm that matches the app package block map (used to create the app package)  |
| 152          | error 0x8007000B: The app package contents must validate against its block map.                                                           | The app package is corrupt and needs to be rebuilt to generate a new block map. For more about creating an app package, see [Create an app package with the MakeAppx.exe tool](https://msdn.microsoft.com/windows/uwp/packaging/create-app-package-with-makeappx-tool) |