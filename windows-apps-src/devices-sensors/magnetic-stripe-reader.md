author: mukin
title: Magnetic Stripe Reader
description: This article contains information about the magnetic stripe reader point of service family of devices
ms.author: mukin
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid:

# Magnetic Stripe Reader

Enables application developers to access magnetic stripe readers to retrieve data from magnetic stripe enabled cards such as credit/debit cards, loyalty cards, access cards, etc.

This topic covers the following:
+	Members
+	Requirements
+	Device support

## Members
The magnetic stripe reader device type has these types of members:
•	Classes
•	Enumerations

### Classes
| Class |	Description |
|-------|-------------|
| [ClaimedMagneticStripeReader](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader) | Represents the claimed magnetic stripe reader. |
| [MagneticStripeReader](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereader) | Represents the magnetic stripe reader device. |
| [MagneticStripeReaderAamvaCardDataReceivedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderaamvacarddatareceivedeventargs) | Provides the American Association of Motor Vehicle Administrators (AAMVA) card data from the AamvaCardDataReceived event. |
| [MagneticStripeReaderBankCardDataReceivedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderbankcarddatareceivedeventargs) | Provides bank card data from the BankCardDataReceived event. |
| [MagneticStripeReaderCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadercapabilities) | Provides capabilities information for the magnetic stripe reader. |
| [MagneticStripeReaderCardTypes](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadercardtypes) | Contains the card type of the recently swiped card. |
| [MagneticStripeReaderEncryptionAlgorithms](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderencryptionalgorithms) | Contains the encryption algorithm supported by the device. |
| [MagneticStripeReaderErrorOccurredEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadererroroccurredeventargs) | Provides error information for the ErrorOccurred event. |
| [MagneticStripeReaderReport](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderreport) | Contains data from the recently swiped card. |
| [MagneticStripeReaderStatusUpdatedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderstatusupdatedeventargs) | Provides information about an operation status change. |
| [MagneticStripeReaderTrackData](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadertrackdata) | Contains the track data obtained following a card swipe. |
| [MagneticStripeReaderVendorSpecificCardDataReceivedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadervendorspecificcarddatareceivedeventargs) | Provides data for the recently swiped vendor card. |

### Enumerations
| Enumeration |	Description |
|-------------|-------------|
| [MagneticStripeReaderAuthenticationLevel](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderauthenticationlevel) | Defines the constants that indicates the level of support for magnetic stripe reader authentication protocol: NotSupported, Optional, or Required. |
| [MagneticStripeReaderAuthenticationProtocol](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderauthenticationprotocol) | Defines the constants that indicates the authentication protocol supported by the device. |
| [MagneticStripeReaderErrorReportingType](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadererrorreportingtype) | Defines the constants that indicates the error reporting type for the device. |
| [MagneticStripeReaderStatus](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereaderstatus) | Defines the constants that indicates the device authentication status. |
| [MagneticStripeReaderTrackErrorType](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadertrackerrortype) | Defines the constants that indicates the track error type. |
| [MagneticStripeReaderTrackIds](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.magneticstripereadertrackids) | Defines the constants that indicates the device track ID to read. |

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
