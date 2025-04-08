---
title: Provision Windows Device Portal with a custom SSL certificate
description: Learn how to provision Windows Device Portal with a custom certificate for use in HTTPS communication.
ms.date: 01/28/2022
ms.topic: article
ms.localizationpriority: medium
ms.custom: 19H1
---

# Provision Windows Device Portal with a custom SSL certificate

Windows Device Portal (WDP) provides a way for device administrators to install a custom certificate for use in HTTPS communication.

While you can do this on your own PC, this feature is primarily intended for enterprises that have an existing certificate infrastructure in place.

For example, a company might have a certificate authority (CA) that it uses to sign certificates for intranet websites served over HTTPS. This feature stands on top of that infrastructure.

## Overview

By default, WDP generates a self-signed root CA, and then uses that to sign SSL certificates for every endpoint it is listening on. This includes `localhost`, `127.0.0.1`, and `::1` (IPv6 localhost).

Also included are the device's hostname (for example, `https://LivingRoomPC`) and each link-local IP address assigned to the device (up to two [IPv4, IPv6] per network adaptor).
You can see the link-local IP addresses for a device by looking at the Networking tool in the WDP. They'll start with `10.` or `192.` for IPv4, or `fe80:` for IPv6.

In the default setup, a certificate warning may appear in your browser because of the untrusted root CA. Specifically, the SSL cert provided by WDP is signed by a root CA that the browser or PC doesn't trust. This can be fixed by creating a new trusted root CA.

## Create a root CA

This should only be done if your company (or home) doesn't have a certificate infrastructure set up, and should only be done once. The following PowerShell script creates a root CA called _WdpTestCA.cer_. Installing this file to the local machine's Trusted Root Certification Authorities will cause your device to trust SSL certs that are signed by this root CA. You can (and should) install this .cer file on each PC that you want to connect to WDP.  

```PowerShell
$CN = "PickAName"
$OutputPath = "c:\temp\"

# Create root certificate authority
$FilePath = $OutputPath + "WdpTestCA.cer"
$Subject =  "CN="+$CN
$rootCA = New-SelfSignedCertificate -certstorelocation cert:\currentuser\my -Subject $Subject -HashAlgorithm "SHA512" -KeyUsage CertSign,CRLSign
$rootCAFile = Export-Certificate -Cert $rootCA -FilePath $FilePath
```

Once this is created, you can use the _WdpTestCA.cer_ file to sign SSL certs.

## Create an SSL certificate with the root CA

SSL certificates have two critical functions: securing your connection through encryption and verifying that you are actually communicating with the address displayed in the browser bar (Bing.com, 192.168.1.37, etc.) and not a malicious third party.

The following PowerShell script creates an SSL certificate for the `localhost` endpoint. Each endpoint that WDP listens on needs its own certificate; you can replace the `$IssuedTo` argument in the script with each of the different endpoints for your device: the hostname, localhost, and the IP addresses.

```PowerShell
$IssuedTo = "localhost"
$Password = "PickAPassword"
$OutputPath = "c:\temp\"
$rootCA = Import-Certificate -FilePath C:\temp\WdpTestCA.cer -CertStoreLocation Cert:\CurrentUser\My\

# Create SSL cert signed by certificate authority
$IssuedToClean = $IssuedTo.Replace(":", "-").Replace(" ", "_")
$FilePath = $OutputPath + $IssuedToClean + ".pfx"
$Subject = "CN="+$IssuedTo
$cert = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -Subject $Subject -DnsName $IssuedTo -Signer $rootCA -HashAlgorithm "SHA512"
$certFile = Export-PfxCertificate -cert $cert -FilePath $FilePath -Password (ConvertTo-SecureString -String $Password -Force -AsPlainText)
```

If you have multiple devices, you can reuse the localhost .pfx files, but you'll still need to create IP address and hostname certificates for each device separately.

When the bundle of .pfx files is generated, you will need to load them into the WDP.

## Provision Windows Device Portal with the certification(s)

For each .pfx file that you've created for a device, you'll need to run the following command from an elevated command prompt.

```cmd
WebManagement.exe -SetCert <Path to .pfx file> <password for pfx>
```

See below for example usage:

```cmd
WebManagement.exe -SetCert localhost.pfx PickAPassword
WebManagement.exe -SetCert --1.pfx PickAPassword
WebManagement.exe -SetCert MyLivingRoomPC.pfx PickAPassword
```

Once you have installed the certificates, simply restart the service so the changes can take effect:

```cmd
sc stop webmanagement
sc start webmanagement
```

> [!TIP]
> IP addresses can change over time.
Many networks use DHCP to give out IP addresses, so devices don't always get the same IP address they had previously. If you've created a certificate for an IP address on a device and that device's address has changed, WDP will generate a new certificate using the existing self-signed cert, and it will stop using the one you created. This will cause the cert warning page to appear in your browser again. For this reason, we recommend connecting to your devices through their hostnames, which you can set in the Windows Device Portal. These will stay the same regardless of IP addresses.
