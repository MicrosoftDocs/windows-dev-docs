---
author: muhsinking
title: Magnetic stripe reader device support
description: This article contains information about the magnetic stripe reader point of service family of devices
ms.author: mukin
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Magnetic stripe reader device support

Windows contains a in-box class driver for USB connected Magnetic stripe readers, which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](http://www.usb.org/developers/hidpage/).

## Vendor specific support
Windows provides support for the following magnetic stripe readers from Magtek and IDTech based on their Vendor ID and Product ID (VID/PID).

| Manufacturer | 	Model(s) |	Part Number |
|--------------|-----------|--------------|
| IDTech | SecureMag (VID:0ACD PID:2010) | IDRE-3x5xxxx |
| |	MiniMag (VID:0ACD PID:0500) |	IDMB-3x5xxxx |
| Magtek | MagneSafe (VID:0801 PID:0011) |	210730xx |
| |	Dynamag (VID:0801 PID:0002) |	210401xx |

## Custom vendor specific
Windows supports implementation of additional vendor specific drivers to support additional magnetic stripe readers. Please check with your magnetic stripe reader manufacturer for availability.

## See also
+   [Windows.Devices.PointOfService namespace](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)
+   [MagneticStripeReader class](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereader)
+	[Magnetic stripe reader sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MagneticStripeReader)
