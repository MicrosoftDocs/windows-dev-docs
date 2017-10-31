---
author: laurenhughes
title: Create a certificate for package signing
description: Create and export a certificate for app package signing with PowerShell tools.
ms.author: lahugh
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 7bc2006f-fc5a-4ff6-b573-60933882caf8
localizationpriority: medium
---

# Create a certificate for package signing


This article explains how to create and export a certificate for app package signing using PowerShell tools. It's recommended that you use Visual Studio for [Packaging UWP apps](https://msdn.microsoft.com/windows/uwp/packaging/packaging-uwp-apps), but you can still package a Store ready app manually if you did not use Visual Studio to develop your app.

> [!IMPORTANT] 
> If you used Visual Studio to develop your app, it's recommended that you use the Visual Studio wizard to import a certificate and sign your app package. For more information, see [Package a UWP app with Visual Studio](https://msdn.microsoft.com/windows/uwp/packaging/packaging-uwp-apps).

## Prerequisites

- **A packaged or unpackaged app**  
An app containing an AppxManifest.xml file. You will need to reference the manifest file while creating the certificate that will be used to sign the final app package. For details on how to manually package an app, see [Create an app package with the MakeAppx.exe tool](https://msdn.microsoft.com/windows/uwp/packaging/create-app-package-with-makeappx-tool).

- **Public Key Infrastructure (PKI) Cmdlets**  
You need PKI cmdlets to create and export your signing certificate. For more information, see [Public Key Infrastructure Cmdlets](https://technet.microsoft.com/library/hh848636.aspx).

## Create a self signed certificate

A self signed certificate is useful for testing your app before you're ready to publish it to the store. Follow the steps outlined in this section to create a self signed certificate.

### Determine the subject of your packaged app  

To use a certificate to sign your app package, the "Subject" in the certificate **must** match the "Publisher" section in your app's manifest.

For example, the "Identity" section in your app's AppxManifest.xml file should look something like this:
```
  <Identity Name="Contoso.AssetTracker" 
    Version="1.0.0.0" 
    Publisher="CN=Contoso Software, O=Contoso Corporation, C=US"/>
```

The "Publisher", in this case, is "CN=Contoso Software, O=Contoso Corporation, C=US" which needs to be used for creating your certificate. 

### Use **New-SelfSignedCertificate** to create a certificate
Use the **New-SelfSignedCertificate** PowerShell cmdlet to create a self signed certificate. **New-SelfSignedCertificate** has several parameters for customization, but for the purpose of this article, we'll focus on creating a simple certificate that will work with **SignTool**. For more examples and uses of this cmdlet, see [New-SelfSignedCertificate](https://technet.microsoft.com/library/hh848633.aspx).

Based on the AppxManifest.xml file from the previous example, you should use the following syntax to create a certificate. In an elevated PowerShell prompt:
```
New-SelfSignedCertificate -Type Custom -Subject "CN=Contoso Software, O=Contoso Corporation, C=US" -KeyUsage DigitalSignature -FriendlyName <Your Friendly Name> -CertStoreLocation "Cert:\LocalMachine\My"
```

After running this command, the certificate will be added to the local certificate store, as specified in the "-CertStoreLocation" parameter. The result of the commmand will also produce the certificate's thumbprint.  

**Note**  
You can view your certificate in a PowerShell window by using the following commands:
```
Set-Location Cert:\LocalMachine\My
Get-ChildItem | Format-Table Subject, FriendlyName, Thumbprint
```
This will display all of the certificates in your local store.

## Export a certificate 

To export the certificate in the local store to a Personal Information Exchange (PFX) file, use the **Export-PfxCertificate** cmdlet.

When using **Export-PfxCertificate**, you must either create and use a password or use the "-ProtectTo" parameter to specify which users or groups can access the file without a password. Note that an error will be displayed if you don't use either the "-Password" or "-ProtectTo" parameter.

- **Password usage**
```
$pwd = ConvertTo-SecureString -String <Your Password> -Force -AsPlainText 
Export-PfxCertificate -cert "Cert:\LocalMachine\My\<Certificate Thumbprint>" -FilePath <FilePath>.pfx -Password $pwd
```

- **ProtectTo usage**
```
Export-PfxCertificate -cert Cert:\LocalMachine\My\<Certificate Thumbprint> -FilePath <FilePath>.pfx -ProtectTo <Username or group name>
```

After you create and export your certificate, you're ready to sign your app package with **SignTool**. For the next step in the manual packaging process, see [Sign an app package using SignTool](https://msdn.microsoft.com/windows/uwp/packaging/sign-app-package-using-signtool).

## Security Considerations 
By adding a certificate to [local machine certificate stores](https://msdn.microsoft.com/windows/hardware/drivers/install/local-machine-and-current-user-certificate-stores), you affect the certificate trust of all users on the computer. It is recommended that you remove those certificates when they are no longer necessary to prevent them from being used to compromise system trust.