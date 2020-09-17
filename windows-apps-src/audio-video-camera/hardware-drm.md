---
ms.assetid: A7E0DA1E-535A-459E-9A35-68A4150EE9F5
description: This topic provides an overview of how to add PlayReady hardware-based digital rights management (DRM) to your Universal Windows Platform (UWP) app.
title: Hardware DRM
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Hardware DRM


This topic provides an overview of how to add PlayReady hardware-based digital rights management (DRM) to your Universal Windows Platform (UWP) app.

> [!NOTE] 
> Hardware-based PlayReady DRM is supported on a multitude of devices, including both Windows and non-Windows devices such as TV sets, phones, and tablets. For a Windows device to support PlayReady Hardware DRM, it must be running Windows 10 and have a supported hardware configuration.

Increasingly, content providers are moving towards hardware-based protections for granting permission to play back full high value content in apps. Robust support for a hardware implementation of the cryptographic core has been added to PlayReady to meet this need. This support enables secure playback of high definition (1080p) and ultra-high definition (UHD) content on multiple device platforms. Key material (including private keys, content keys, and any other key material used to derive or unlock said keys), and decrypted compressed and uncompressed video samples are protected by leveraging hardware security.

## Windows TEE implementation

This topic provides a brief overview of how Windows 10 implements the trusted execution environment (TEE).

The details of the Windows TEE implementation is out of scope for this document. However, a brief discussion of the difference between the standard porting kit TEE port and the Windows port will be beneficial. Windows implements the OEM proxy layer and transfers the serialized PRITEE functions calls to a user mode driver in the Windows Media Foundation subsystem. This will eventually get routed to either the Windows TrEE (Trusted Execution Environment) driver or the OEM’s graphics driver. The details of either of these approaches is out of scope for this document. The following diagram shows the general component interaction for the Windows port. If you want to develop a Windows PlayReady TEE implementation, you can contact <WMLA@Microsoft.com>.

![windows tee component diagram](images/windowsteecomponentdiagram720.jpg)

## Considerations for using hardware DRM

This topic provides a brief list of items that should be considered when developing apps designed to use hardware DRM. As explained in [PlayReady DRM](playready-client-sdk.md#output-protection), with PlayReady HWDRM for Windows 10, all output protections are enforced from within the Windows TEE implementation, which has some consequences on output protection behaviors:

-   **Support for output protection level (OPL) for uncompressed digital video 270:** PlayReady HWDRM for Windows 10 doesn't support down-resolution and will enforce that HDCP is engaged. We recommend that high definition content for HWDRM have an OPL greater than 270 (although it is not required). Additionally, we recommend that you set HDCP type restriction in the license (HDCP version 2.2 on Windows 10).
-   **Unlike software DRM (SWDRM), output protections are enforced on all monitors based on the least capable monitor.** For example, if the user has two monitors connected where one of the monitors supports HDCP and the other does not, playback will fail if the license requires HDCP even if the content is only being rendered on the monitor that supports HDCP. In software DRM, content would play back as long as it is only being rendered on the monitor that supports HDCP.
-   **HWDRM is not guaranteed to be used by the client and secure unless the following conditions are met** by the content keys and licenses:
    -   The license used for the video content key must have a Minimum Security level property of 3000.
    -   Audio must be encrypted to a different content key than video, and the license used for the audio must have a Minimum Security level property of 2000. Alternatively, audio could be left in the clear.
    
Additionally, you should take the following items into consideration when using HWDRM:

-   Protected Media Process (PMP) is not supported.
-   Windows Media Video (also known as VC-1) is not supported (see [Override hardware DRM](#override-hardware-drm)).
-   Multiple graphics processing units (GPUs) are not supported for persistent licenses.

To handle persistent licenses on machines with multiple GPUs, consider the following scenario:

1.  A customer buys a new machine with an integrated graphics card.
2.  The customer uses an app that acquires persistent licenses while using hardware DRM.
3.  The persistent license is now bound to that graphics card’s hardware keys.
4.  The customer then installs a new graphics card.
5.  All licenses in the hashed data store (HDS) are bound to the integrated video card, but the customer now wants to play back protected content using the newly-installed graphics card.

To prevent playback from failing because the licenses can’t be decrypted by the hardware, PlayReady uses a separate HDS for each graphics card that it encounters. This will cause PlayReady to attempt license acquisition for a piece of content where PlayReady would normally already have a license (that is, in the software DRM case or any case without a hardware change, PlayReady wouldn’t need to reacquire a license). Therefore, if the app acquires a persistent license while using hardware DRM, your app needs to be able to handle the case where that license is effectively “lost” if the end user installs (or uninstalls) a graphics card. Because this is not a common scenario, you may decide to handle the support calls when the content no longer plays after a hardware change rather than figure out how to deal with a hardware change in the client/server code.

## Override hardware DRM

This section describes how to override hardware DRM (HWDRM) if the content to be played back does not support hardware DRM.

By default, hardware DRM is used if the system supports it. However, some content is not supported in hardware DRM. One example of this is Cocktail content. Another example is any content that uses a video codec other than H.264 and HEVC. Another example is HEVC content, as some hardware DRM will support HEVC and some will not. Therefore, if you want to play a piece of content and hardware DRM doesn’t support it on the system in question, you may want to opt out of hardware DRM.

The following example shows how to opt-out of hardware DRM. You only need to do this before you switch. Also, make sure you don’t have any PlayReady object in memory, otherwise behavior is undefined.

```js
var applicationData = Windows.Storage.ApplicationData.current;
var localSettings = applicationData.localSettings.createContainer("PlayReady", Windows.Storage.ApplicationDataCreateDisposition.always);
localSettings.values["SoftwareOverride"] = 1;
```

To switch back to hardware DRM, set the **SoftwareOverride** value to **0**.

For every media playback, you need to set **MediaProtectionManager** to:

```js
mediaProtectionManager.properties["Windows.Media.Protection.UseSoftwareProtectionLayer"] = true;
```

The best way to tell if you are in hardware DRM or software DRM is to look at C:\\Users\\&lt;username&gt;\\AppData\\Local\\Packages\\&lt;application name&gt;\\LocalCache\\PlayReady\\\*

-   If there is an mspr.hds file, you are in software DRM.
-   If you have another \*.hds file, you are in hardware DRM.
-   You can delete the entire PlayReady folder and retry your test as well.

## Detect the type of hardware DRM

This section describes how to detect what type of hardware DRM is supported on the system.

You can use the [**PlayReadyStatics.CheckSupportedHardware**](/uwp/api/windows.media.protection.playready.playreadystatics.checksupportedhardware) method to determine whether the system supports a specific hardware DRM feature. For example:

```csharp
bool isFeatureSupported = PlayReadyStatics.CheckSupportedHardware(PlayReadyHardwareDRMFeatures.HEVC);
```

The [**PlayReadyHardwareDRMFeatures**](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadyHardwareDRMFeatures) enumeration contains the valid list of hardware DRM feature values that can be queried. To determine if hardware DRM is supported, use the **HardwareDRM** member in the query. To determine if the hardware supports the High Efficiency Video Coding (HEVC)/H.265 codec, use the **HEVC** member in the query.

You can also use the [**PlayReadyStatics.PlayReadyCertificateSecurityLevel**](/uwp/api/windows.media.protection.playready.playreadystatics.playreadycertificatesecuritylevel) property to get the security level of the client certificate to determine if hardware DRM is supported. Unless the returned certificate security level is greater than or equal to 3000, either the client is not individualized or provisioned (in which case this property returns 0) or hardware DRM is not in use (in which case this property returns a value that is less than 3000).

### Detecting support for AES128CBC hardware DRM
Starting with Windows 10, version 1709, you can detect support for AES128CBC hardware encryption on a device by calling **[PlayReadyStatics.CheckSupportedHardware](/uwp/api/windows.media.protection.playready.playreadystatics.checksupportedhardware)** and specifying the enumeration value [**PlayReadyHardwareDRMFeatures.Aes128Cbc**](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadyHardwareDRMFeatures). On previous versions of Windows 10, specifying this value will cause an exception to be thrown. For this reason, you should check for the presence of the enumeration value by calling **[ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent)** and specifying major contract version 5 before calling **CheckSupportedHardware**.

```csharp
bool supportsAes128Cbc = ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5);

if (supportsAes128Cbc)
{
    supportsAes128Cbc = PlayReadyStatics.CheckSupportedHardware(PlayReadyHardwareDRMFeatures.Aes128Cbc);
}
```

## See also
- [PlayReady DRM](playready-client-sdk.md)