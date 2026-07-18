---
title: Provision Windows Device Portal with a custom SSL certificate
description: Learn how to provision Windows Device Portal with a custom SSL certificate for use in HTTPS communication on Windows Desktop devices.
ms.date: 07/06/2026
ms.topic: how-to
ms.localizationpriority: medium
author: GrantMeStrength
ms.author: jken
---

# Provision Windows Device Portal with a custom SSL certificate

Windows Device Portal (WDP) provides a way for device administrators to install a custom certificate for use in HTTPS communication.

While you can do this on your own PC, this feature is primarily intended for enterprises that have an existing certificate infrastructure in place.

For example, a company might have a certificate authority (CA) that it uses to sign certificates for intranet websites served over HTTPS. This feature stands on top of that infrastructure.

## Overview

By default, WDP generates a self-signed root CA, and then uses that to sign SSL certificates for every endpoint it is listening on. This includes `localhost`, `127.0.0.1`, and `::1` (IPv6 localhost).

Also included are the device's hostname (for example, `https://LivingRoomPC`) and each link-local IP address assigned to the device (up to two [IPv4, IPv6] per network adapter).
You can see the link-local IP addresses for a device by looking at the Networking tool in the WDP. They'll start with `10.` or `192.` for IPv4, or `fe80:` for IPv6.

In the default setup, a certificate warning may appear in your browser because of the untrusted root CA. Specifically, the SSL cert provided by WDP is signed by a root CA that the browser or PC doesn't trust. This can be fixed by creating a new trusted root CA.

## Create a root CA

This should only be done if your company (or home) doesn't have a certificate infrastructure set up, and should only be done once. Run this script from an **elevated (admin) PowerShell** prompt because it writes to the `LocalMachine\My` certificate store. The script creates a root CA called _WdpTestCA.cer_. Installing this file to the local machine's Trusted Root Certification Authorities will cause your device to trust SSL certs that are signed by this root CA. You can (and should) install this .cer file on each PC that you want to connect to WDP.

```PowerShell
$CN = "PickAName"
$OutputPath = "c:\temp\"

# Create root certificate authority (marked as a CA via Basic Constraints)
$FilePath = $OutputPath + "WdpTestCA.cer"
$Subject = "CN=" + $CN
$rootCA = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -Subject $Subject -HashAlgorithm "SHA512" -KeyUsage CertSign,CRLSign -TextExtension @("2.5.29.19={critical}{text}CA=true")
$rootCAFile = Export-Certificate -Cert $rootCA -FilePath $FilePath
```

The root CA now lives in `LocalMachine\My` (with its private key). The exported _WdpTestCA.cer_ is the public certificate you install into Trusted Root Certification Authorities on each connecting PC.

## Create an SSL certificate with the root CA

SSL certificates have two critical functions: securing your connection through encryption and verifying that you are actually communicating with the address displayed in the browser bar (Bing.com, 192.168.1.37, etc.) and not a malicious third party.

The following PowerShell script creates an SSL certificate for the `localhost` endpoint. Each endpoint that WDP listens on needs its own certificate; you can replace the `$IssuedTo` argument in the script with each of the different endpoints for your device: the hostname, localhost, and the IP addresses. This script also requires an **elevated PowerShell** prompt. The `Get-ChildItem` filter uses the same CN (`PickAName`) you chose in step 1 — keep them in sync.

```PowerShell
$IssuedTo = "localhost"
$Password = "PickAPassword"
$OutputPath = "c:\temp\"

# Retrieve the root CA (with its private key) from the store where it was created.
# Match the CN you used above.
$rootCA = Get-ChildItem -Path cert:\localmachine\my | Where-Object { $_.Subject -eq "CN=PickAName" -and $_.HasPrivateKey } | Select-Object -First 1

# Create SSL cert signed by certificate authority
$IssuedToClean = $IssuedTo.Replace(":", "-").Replace(" ", "_")
$FilePath = $OutputPath + $IssuedToClean + ".pfx"
$Subject = "CN=" + $IssuedTo
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

Once you have installed the certificates, restart the service so the changes can take effect:

```cmd
sc stop webmanagement
sc start webmanagement
```

> [!TIP]
> IP addresses can change over time.
> Many networks use DHCP to give out IP addresses, so devices don't always get the same IP address they had previously. If you've created a certificate for an IP address on a device and that device's address has changed, WDP will generate a new certificate using the existing self-signed cert, and it will stop using the one you created. This will cause the cert warning page to appear in your browser again. For this reason, we recommend connecting to your devices through their hostnames, which you can set in the Windows Device Portal. These will stay the same regardless of IP addresses.

## See also

- [Windows Device Portal overview](device-portal.md)
- [Device Portal for Desktop](device-portal-desktop.md)
- [Windows Device Portal core API reference](/windows/uwp/debug-test-perf/device-portal-api-core)
