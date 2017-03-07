---
author: mukin
title: Magnetic Stripe Reader
description: This article contains information about the magnetic stripe reader point of service family of devices
ms.author: wdg-dev-content
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Magnetic Stripe Reader

Enables application developers to access [magnetic stripe readers](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereader) to retrieve data from magnetic stripe enabled cards such as credit/debit cards, loyalty cards, access cards, etc.

## Requirements
Applications which require this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Device support
### USB
### Supported vendor specific
Windows provides support for the following magnetic stripe readers from Magtek and IDTech based on their Vendor ID and Product ID (VID/PID).

| Manufacturer | 	Model(s) |	Part Number |
|--------------|-----------|--------------|
| IDTech | SecureMag (VID:0ACD PID:2010) | IDRE-3x5xxxx |
| |	MiniMag (VID:0ACD PID:0500) |	IDMB-3x5xxxx |
| Magtek | MagneSafe (VID:0801 PID:0011) |	210730xx |
| |	Dynamag (VID:0801 PID:0002) |	210401xx |

### Custom vendor specific
Windows supports implementation of additional vendor specific drivers to support additional magnetic stripe readers. Please check with your magnetic stripe reader manufacturer for availability.

## Examples
See the magnetic stripe reader sample for an example implementation.
+	[Magnetic stripe reader sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MagneticStripeReader)
