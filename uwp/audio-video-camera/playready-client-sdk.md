---
ms.assetid: DD8FFA8C-DFF0-41E3-8F7A-345C5A248FC2
description: This topic describes how to add PlayReady protected media content to your Universal Windows Platform (UWP) app.
title: PlayReady DRM
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# PlayReady DRM



This topic describes how to add PlayReady protected media content to your Universal Windows Platform (UWP) app.

PlayReady DRM enables developers to create UWP apps capable of providing PlayReady content to the user while enforcing the access rules defined by the content provider. This section describes changes made to Microsoft PlayReady DRM for Windows 10 and how to modify your PlayReady UWP app to support the changes made from the previous Windows 8.1 version to the Windows 10 version.
 
| Topic                                                                     | Description                                                                                                                                                                                                                                                                             |
|---------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Hardware DRM](hardware-drm.md)                                           | This topic provides an overview of how to add PlayReady hardware-based digital rights management (DRM) to your UWP app.                                                                                                                                                                 |
| [Adaptive streaming with PlayReady](adaptive-streaming-with-playready.md) | This article describes how to add adaptive streaming of multimedia content with Microsoft PlayReady content protection to a Universal Windows Platform (UWP) app. This feature currently supports playback of Http Live Streaming (HLS) and Dynamic Streaming over HTTP (DASH) content. |

## What's new in PlayReady DRM

The following list describes the new features and changes made to PlayReady DRM for Windows 10.

-   Added hardware digital rights management (HWDRM).

    Hardware-based content protection support enables secure playback of high definition (HD) and ultra-high definition (UHD) content on multiple device platforms. Key material (including private keys, content keys, and any other key material used to derive or unlock said keys), and decrypted compressed and uncompressed video samples are protected by leveraging hardware security. When Hardware DRM is being used, neither unknown enabler (play to unknown / play to unknown with downres) has meaning as the HWDRM pipeline always knows the output being used. For more information, see [Hardware DRM](hardware-drm.md).

-   PlayReady is no longer an appX framework component, but instead is an in-box operating system component. The namespace was changed from **Microsoft.Media.PlayReadyClient** to [**Windows.Media.Protection.PlayReady**](/uwp/api/Windows.Media.Protection.PlayReady).
-   The following headers defining the PlayReady error codes are now part of the Windows Software Development Kit (SDK): Windows.Media.Protection.PlayReadyErrors.h and Windows.Media.Protection.PlayReadyResults.h.
-   Provides proactive acquisition of non-persistent licenses.

    Previous versions of PlayReady DRM did not support proactive acquisition of non-persistent licenses. This capability has been added to this version. This can decrease the time to first frame. For more information, see [Proactively Acquire a Non-Persistent License Before Playback](#proactively-acquire-a-non-persistent-license-before-playback).

-   Provides acquisition of multiple licenses in one message.

    Allows the client app to acquire multiple non-persistent licenses in one license acquisition message. This can decrease the time to first frame by acquiring licenses for multiple pieces of content while the user is still browsing your content library; this prevents a delay for license acquisition when the user selects the content to play. In addition, it allows audio and video streams to be encrypted to separate keys by enabling a content header that includes multiple key identifiers (KIDs); this enables a single license acquisition to acquire all licenses for all streams within a content file instead of having to use custom logic and multiple license acquisition requests to achieve the same result.

-   Added real time expiration support, or limited duration license (LDL).

    Provides the ability to set real-time expiration on licenses and smoothly transition from an expiring license to another (valid) license in the middle of playback. When combined with acquisition of multiple licenses in one message, this allows an app to acquire several LDLs asynchronously while the user is still browsing the content library and only acquire a longer duration license once the user has selected content to playback. Playback will then start more quickly (because a license is already available) and, since the app will have acquired a longer duration license by the time the LDL expires, smoothly continue playback to the end of the content without interruption.

-   Added non-persistent license chains.
-   Added support for time-based restrictions (including expiration, expire after first play, and real time expiration) on non-persistent licenses.
-   Added HDCP Type 1 (version 2.2 on Windows 10) policy support.

    See [Things to Consider](#things-to-consider) for more information.

-   Miracast is now implicit as an output.
-   Added secure stop.

    Secure stop provides the means for a PlayReady device to confidently assert to a media streaming service that media playback has stopped for any given piece of content. This capability ensures your media streaming services provide accurate enforcement and reporting of usage limitations on different devices for a given account.

-   Added audio and video license separation.

    Separate tracks prevent video from being decoded as audio; enabling more robust content protection. Emerging standards are requiring separate keys for audio and visual tracks.

-   Added MaxResDecode.

    This feature was added to limit playback of content to a maximum resolution even when in possession of a more capable key (but not a license). It supports cases where multiple stream sizes are encoded with a single key.

The following new interfaces, classes, and enumerations were added to PlayReady DRM:

-   [**IPlayReadyLicenseAcquisitionServiceRequest**](/uwp/api/Windows.Media.Protection.PlayReady.IPlayReadyLicenseAcquisitionServiceRequest) interface
-   [**IPlayReadyLicenseSession**](/uwp/api/Windows.Media.Protection.PlayReady.IPlayReadyLicenseSession) interface
-   [**IPlayReadySecureStopServiceRequest**](/uwp/api/Windows.Media.Protection.PlayReady.IPlayReadySecureStopServiceRequest) interface
-   [**PlayReadyLicenseSession**](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadyLicenseSession) class
-   [**PlayReadySecureStopIterable**](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadySecureStopIterable) class
-   [**PlayReadySecureStopIterator**](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadySecureStopIterator) class
-   [**PlayReadyHardwareDRMFeatures**](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadyHardwareDRMFeatures) enumerator

A new sample has been created to demonstrate how to use the new features of PlayReady DRM. The sample can be downloaded from the [Code Samples Browser](/samples/microsoft/windows-universal-samples/playready/).

## Things to consider

-   PlayReady DRM now supports HDCP Type 1 (supported in HDCP version 2.1 or later). PlayReady carries an HDCP Type Restriction policy in the license for the device to enforce. On Windows 10, this policy will enforce that HDCP 2.2 or later is engaged. This feature can be enabled in your PlayReady Server v3.0 SDK license (the server controls this policy in the license using the HDCP Type Restriction GUID). For more information, see the [PlayReady Compliance and Robustness Rules](https://www.microsoft.com/playready/licensing/compliance/).
-   Windows Media Video (also known as VC-1) is not supported in hardware DRM (see [Override Hardware DRM](hardware-drm.md#override-hardware-drm)).
-   PlayReady DRM now supports the High Efficiency Video Coding (HEVC /H.265) video compression standard. To support HEVC, your app must use Common Encryption Scheme (CENC) version 2 content which includes leaving the content's slice headers in the clear. Refer to ISO/IEC 23001-7 Information technology -- MPEG systems technologies -- Part 7: Common encryption in ISO base media file format files (Spec version ISO/IEC 23001-7:2015 or later is required.) for more information. Microsoft also recommends using CENC version 2 for all HWDRM content. In addition, some hardware DRM will support HEVC and some will not (see [Override Hardware DRM](hardware-drm.md#override-hardware-drm)).
-   To take advantage of certain new PlayReady 3.0 features (including, but not limited to, SL3000 for hardware-based clients, acquiring multiple non-persistent licenses in one license acquisition message, and time-based restrictions on non-persistent licenses), the PlayReady server is required to be the Microsoft PlayReady Server Software Development Kit v3.0.2769 Release version or later.
-   Depending on the Output Protection Policy specified in the content license, media playback may fail for end users if their connected output does not support those requirements. The following table lists the set of common errors that occur as a result. For more information, see the [PlayReady Compliance and Robustness Rules](https://www.microsoft.com/playready/licensing/compliance/).

| Error                                                   | Value      | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
|---------------------------------------------------------|------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ERROR\_GRAPHICS\_OPM\_OUTPUT\_DOES\_NOT\_SUPPORT\_HDCP  | 0xC0262513 | The license's Output Protection Policy requires the monitor to engage HDCP, but HDCP was unable to be engaged.                                                                                                                                                                                                                                                                                                                                                                                              |
| MF\_E\_POLICY\_UNSUPPORTED                              | 0xC00D7159 | The license's Output Protection Policy requires the monitor to engage HDCP Type 1, but HDCP Type 1 was unable to be engaged.                                                                                                                                                                                                                                                                                                                                                                                |
| DRM\_E\_TEE\_OUTPUT\_PROTECTION\_REQUIREMENTS\_NOT\_MET | 0x8004CD22 | This error code only occurs when running under hardware DRM. The license's Output Protection Policy requires the monitor to engage HDCP or to reduce the content's effective resolution, but HDCP was unable to be engaged and the content's effective resolution could not be reduced because hardware DRM does not support reducing the content's resolution. Under software DRM, the content plays. See [Considerations for Using Hardware DRM](hardware-drm.md#considerations-for-using-hardware-drm). |
| ERROR\_GRAPHICS\_OPM\_NOT\_SUPPORTED                    | 0xC0262500 | The graphics driver does not support Output Protection. For example, the monitor is connected through VGA or an appropriate graphics driver for the digital output is not installed. In the latter case, the typical driver that is installed is the Microsoft Basic Display Adapter and installing an appropriate graphics driver will resolve the issue.                                                                                                                                                  |

## Output protection

The following section describes the behavior when using PlayReady DRM for Windows 10 with output protection policies in a PlayReady license.

PlayReady DRM supports output protection levels contained in the **Microsoft PlayReady Extensible Media Rights Specification**. This document can be found in the documentation pack that comes with PlayReady licensed products.

> [!NOTE]
> The allowed values for output protection levels that can be set by a licensing server are governed by the [PlayReady Compliance Rules](https://www.microsoft.com/playready/licensing/compliance/).

PlayReady DRM allows playback of content with output protection policies only on output connectors as specified in the PlayReady Compliance Rules. For more information about output connector terms specified in the PlayReady Compliance Rules, see [Defined Terms for PlayReady Compliance and Robustness Rules](https://www.microsoft.com/playready/licensing/compliance/).

This section focuses on output protection scenarios with PlayReady DRM for Windows 10 and PlayReady Hardware DRM for Windows 10, which is also available on some Windows clients. With PlayReady HWDRM, all output protections are enforced from within the Windows TEE implementation (see [Hardware DRM](hardware-drm.md)). As a result, some behaviors differ from when using PlayReady SWDRM (software DRM):

* Support for Output Protection Level (OPL) for Uncompressed Digital Video 270: PlayReady HWDRM for Windows 10 doesn't support down-resolution and will enforce that HDCP (High-bandwidth Digital Content Protection) is engaged. It is recommended that high definition content for HWDRM have an OPL greater than 270 (although it is not required). Additionally, you should set HDCP type restriction in the license (HDCP version 2.2 or later).
* Unlike SWDRM, with HWDRM, output protections are enforced on all monitors based on the least capable monitor. For example, if the user has two monitors connected where one supports HDCP and the other doesn't, playback will fail if the license requires HDCP even if the content is only being rendered on the monitor that supports HDCP. In SWDRM, content will play back as long as it's only being rendered on the monitor that supports HDCP.
* HWDRM is not guaranteed to be used by the client and secure unless the following conditions are met by the content keys and licenses:
    * The license used for the video content key must have a minimum security level of 3000.
    * Audio must be encrypted to a different content key than video, and the license used for audio must have a minimum security level of 2000. Alternatively, audio could be left in the clear.
* All SWDRM scenarios require that the minimum security level of the PlayReady license used for the audio and/or video content key is lower or equal to 2000.

### Output protection levels

The following table outlines the mappings between various OPLs in the PlayReady license and how PlayReady DRM for Windows 10 enforces them.

#### Video

<table>
    <tr>
        <th rowspan="2">OPL</th>
        <th>Compressed digital video</th>
        <th colspan="2">Uncompressed digital video</th>
        <th>Analog TV</th>
    </tr>
    <tr>
        <th>Any</th>
        <th colspan="2">HDMI, DVI, DisplayPort, MHL</th>
        <th>Component, Composite</th>
    </tr>
    <tr>
        <th>100</th>
        <td rowspan="6">N/A\*</td>
        <td colspan="2">Passes content</td>
        <td>Passes content</td>
    </tr>
    <tr>
        <th>150</th>
        <td colspan="2" rowspan="2">N/A\*</td>
        <td>Passes content when CGMS-A CopyNever is engaged or if CGMS-A can't be engaged</td>
    </tr>
    <tr>
        <th>200</th>
        <td>Passes content when CGMS-A CopyNever is engaged</td>
    </tr>
    <tr>
        <th>250</th>
        <td colspan="2">Attempts to engage HDCP, but passes content regardless of result</td>
        <td rowspan="5">N/A\*</td>
    </tr>
    <tr>
        <th>270</th>
        <td><b>SWDRM</b>: Attempts to engage HDCP. If HDCP fails to engage, the PC will constrain the effective resolution to 520,000 pixels per frame and pass the content</td>
        <td><b>HWDRM</b>: Passes content with HDCP. If HDCP fails to engage, playback to HDMI/DVI ports is blocked</td>
    </tr>
    <tr>
        <th>300</th>
        <td colspan="2">
            <p>
                **When HDCP type restriction is NOT defined:** Passes content with HDCP. If HDCP fails to engage, playback to HDMI/DVI ports is blocked.
            </p>
            <p>
                **When HDCP type restriction IS defined**: Passes content with HDCP 2.2 and content stream type set to 1. If HDCP fails to engage or content stream type can't be set to 1, playback to HDMI/DVI ports is blocked.
            </p>
        </td>
    </tr>
    <tr>
        <th>400</th>
        <td rowspan="2">Windows 10 never passes compressed digital video content to outputs, regardless of the subsequent OPL value. For more information about compressed digital video content, see the <a href="https://www.microsoft.com/playready/licensing/compliance/">Compliance Rules for PlayReady Products</a>.</td>
        <td colspan="2" rowspan="2">N/A\*</td>
    </tr>
    <tr>
        <th>500</th>
    </tr>
</table>
<br/>

\* Not all values for output protection levels can be set by a licensing server. For more information, see the [PlayReady Compliance Rules](https://www.microsoft.com/playready/licensing/compliance/).

#### Audio

<table>
    <tr>
        <th rowspan="2">OPL</th>
        <th>Compressed digital audio</th>
        <th>Uncompressed digital audio</th>
        <th>Analog or USB audio</th>
    </tr>
    <tr>
        <th>HDMI, DisplayPort, MHL</th>
        <th>HDMI, DisplayPort, MHL</th>
        <th>Any</th>
    </tr>
    <tr>
        <th>100</th>
        <td rowspan="3">Passes content</td>
        <td>Passes content</td>
        <td rowspan="5">Passes content</td>
    </tr>
    <tr>
        <th>150</th>
        <td rowspan="4">Does NOT pass content</td>
    </tr>
    <tr>
        <th>200</th>
    </tr>
    <tr>
        <th>250</th>
        <td>Passes content when HDCP is engaged on HDMI, DisplayPort, or MHL, or when SCMS is engaged and set to CopyNever</td>
    </tr>
    <tr>
        <th>300</th>
        <td>Passes content when HDCP is engaged on HDMI, DisplayPort, or MHL</td>
    </tr>
</table>
<br/>

### Miracast

PlayReady DRM allows you to play content over Miracast output as soon as HDCP 2.0 or later is engaged. On Windows 10, however, Miracast is considered a *digital* output. For more information about Miracast scenarios, see the [PlayReady Compliance Rules](https://www.microsoft.com/playready/licensing/compliance/). The following table outlines the mappings between various OPLs in the PlayReady license and how PlayReady DRM enforces them on Miracast outputs.

<table>
    <tr>
        <th>OPL</th>
        <th>Compressed digital audio</th>
        <th>Uncompressed digital audio</th>
        <th>Compressed digital video</th>
        <th>Uncompressed digital video</th>
    </tr>
    <tr>
        <th>100</th>
        <td rowspan="4">Passes content when HDCP 2.0 or later is engaged. If it fails to engage, it does NOT pass content</td>
        <td>Passes content when HDCP 2.0 or later is engaged. If it fails to engage, it does NOT pass content</td>
        <td rowspan="6">N/A\*</td>
        <td>Passes content when HDCP 2.0 or later is engaged. If it fails to engage, it does NOT pass content</td>
    </tr>
    <tr>
        <th>150</th>
        <td rowspan="3">Does NOT pass content</td>
        <td rowspan="2">N/A\*</td>
    </tr>
    <tr>
        <th>200</th>
    </tr>
    <tr>
        <th>250</th>
        <td rowspan="2">Passes content when HDCP 2.0 or later is engaged. If it fails to engage, it does NOT pass content</td>
    </tr>
    <tr>
        <th>270</th>
        <td colspan="2">N/A\*</td>
    </tr>
    <tr>
        <th>300</th>
        <td>Passes content when HDCP 2.0 or later is engaged. If it fails to engage, it does NOT pass content</td>
        <td>Does NOT pass content</td>
        <td>
            <p>
                **When HDCP type restriction is NOT defined:** Passes content when HDCP 2.0 or later is engaged. If it fails to engage, it does NOT pass content.
            </p>
            <p>
                **When HDCP type restriction IS defined:** Passes content with HDCP 2.2 and content stream type set to 1. If HDCP fails to engage or content stream type can't be set to 1, it does NOT pass content.
            </p>        
        </td>
    </tr>
    <tr>
        <th>400</th>
        <td rowspan="2" colspan="2">N/A\*</td>
        <td rowspan="2">Windows 10 never passes compressed digital video content to outputs, regardless of the subsequent OPL value. For more information about compressed digital video content, see the <a href="https://www.microsoft.com/playready/licensing/compliance/">Compliance Rules for PlayReady Products</a>.</td>
        <td rowspan="2">N/A\*</td>
    </tr>
    <tr>
        <th>500</th>
    </tr>
</table>
<br/>

\* Not all values for output protection levels can be set by a licensing server. For more information, see the [PlayReady Compliance Rules](https://www.microsoft.com/playready/licensing/compliance/).

### Additional explicit output restrictions

The following table describes the PlayReady DRM for Windows 10 implementation of explicit digital video output protection restrictions.

<table>
    <tr>
        <th>Scenario</th>
        <th>GUID</th>
        <th>If...</th>
        <th>Then...</th>
    </tr>
    <tr>
        <th>Maximum effective resolution decode size</th>
        <td>9645E831-E01D-4FFF-8342-0A720E3E028F</td>
        <td>Connected output is: digital video output, Miracast, HDMI, DVI, etc.</td>
        <td>
            <p>
                Passes content when constrained to:  
            </p>
            <ul>
                <li>(a) the width of the frame must be less than or equal to the maximum frame width in pixels and the height of the frame less than or equal to the maximum frame height in pixels, or</li>
                <li>(b) the height of the frame must be less than or equal to the maximum frame width in pixels and the width of the frame less than or equal to the maximum frame height in pixels</li>
            </ul>                   
        </td>
    </tr>
    <tr>
        <th>HDCP type restriction</th>
        <td>ABB2C6F1-E663-4625-A945-972D17B231E7</td>
        <td>Connected output is: digital video output, Miracast, HDMI, DVI, etc.</td>
        <td>Passes content with HDCP 2.2 and the content stream type set to 1. If HDCP 2.2 fails to engage or the content stream type can't be set to 1, it does NOT pass content. Uncompressed digital video output protection level of a value greater than or equal to 271 must also be specified</td>
    </tr>
</table>
<br/>

The following table describes the PlayReady DRM for Windows 10 implementation of explicit analog video output protection restrictions.

<table>
    <tr>
        <th>Scenario</th>
        <th>GUID</th>
        <th>If...</th>
        <th colspan="2">Then...</th>
    </tr>
    <tr>
        <th>Analog computer monitor</th>
        <td>D783A191-E083-4BAF-B2DA-E69F910B3772</td>
        <td>Connected output is: VGA, DVI&ndash;analog, etc.</td>
        <td><b>SWDRM:</b> PC will constrain effective resolution to 520,000 epx per frame and pass content</td>
        <td><b>HWDRM:</b> Does NOT pass content</td>
    </tr>
    <tr>
        <th>Analog component</th>
        <td>811C5110-46C8-4C6E-8163-C0482A15D47E</td>
        <td>Connected output is: component</td>
        <td><b>SWDRM:</b> PC will constrain effective resolution to 520,000 epx per frame and pass content</td>
        <td><b>HWDRM:</b> Does NOT pass content</td>
    </tr>
    <tr>
        <th rowspan="2">Analog TV outputs</th>
        <td>2098DE8D-7DDD-4BAB-96C6-32EBB6FABEA3</td>
        <td>Analog TV OPL is less than 151</td>
        <td colspan="2">CGMS-A must be engaged</td>
    </tr>
    <tr>
        <td>225CD36F-F132-49EF-BA8C-C91EA28E4369</td>
        <td>Analog TV OPL is less than 101 and license doesn't contain 2098DE8D-7DDD-4BAB-96C6-32EBB6FABEA3</td>
        <td colspan="2">CGMS-A engagement must be attempted, but content may play regardless of result</td>
    </tr>
    <tr>
        <th>Automatic gain control and color stripe</th>
        <td>C3FD11C6-F8B7-4D20-B008-1DB17D61F2DA</td>
        <td>Passing content with resolution less than or equal to 520,000 px to analog TV output</td>
        <td colspan="2">Sets AGC only for component video and PAL mode when resolution is less than 520,000 px and sets AGC and color stripe information for NTSC when resolution is less than 520,000 px, according to table 3.5.7.3. in Compliance Rules</td>
    </tr>
    <tr>
        <th>Digital-only output</th>
        <td>760AE755-682A-41E0-B1B3-DCDF836A7306</td>
        <td>Connected output is analog</td>
        <td colspan="2">Does not pass content</td>
    </tr>
</table>
<br/>

> [!NOTE]
> When using an adapter dongle such as "Mini DisplayPort to VGA" for playback, Windows 10 sees the output as digital video output, and can't enforce analog video policies.

The following table describes the PlayReady DRM for Windows 10 implementation that enables playing in other circumstances.

<table>
    <tr>
        <th>Scenario</th>
        <th>GUID</th>
        <th>If...</th>
        <th colspan="2">Then...</th>
    </tr>
    <tr>
        <th>Unknown output</th>
        <td>786627D8-C2A6-44BE-8F88-08AE255B01A7</td>
        <td>If output can't reasonably be determined, or OPM can't be established with graphics driver</td>
        <td><b>SWDRM:</b> Passes content</td>
        <td><b>HWDRM:</b> Does NOT pass content</td>
    </tr>
    <tr>
        <th>Unknown output with constriction</th>
        <td>B621D91F-EDCC-4035-8D4B-DC71760D43E9</td>
        <td>If output can't reasonably be determined, or OPM can't be established with graphics driver</td>
        <td><b>SWDRM:</b> PC will constrain effective resolution to 520,000 epx per frame and pass content</td>
        <td><b>HWDRM:</b> Does NOT pass content</td>
    </tr>
</table>
<br/>

## Prerequisites

Before you begin creating your PlayReady-protected UWP app, the following software needs to be installed on your system:

-   Windows 10.
-   If you are compiling any of the samples for PlayReady DRM for UWP apps, you must use Microsoft Visual Studio 2015 or later to compile the samples. You can still use Microsoft Visual Studio 2013 to compile any of the samples from PlayReady DRM for Windows 8.1 Store Apps.

<!--This is no longer available-->
<!--If you are planning to play back MPEG-2/H.262 content on your app, you must also download and install [Windows 8.1 Media Center Pack](https://windows.microsoft.com/windows-8/feature-packs).-->

## PlayReady UWP app migration guide

This section includes information on how to migrate your existing PlayReady Windows 8.x Store apps to Windows 10.

The namespace for PlayReady UWP apps on Windows 10 was changed from **Microsoft.Media.PlayReadyClient** to [**Windows.Media.Protection.PlayReady**](/uwp/api/Windows.Media.Protection.PlayReady). This means that you will need to search and replace the old namespace with the new one in your code. You will still be referencing a winmd file. It is part of windows.media.winmd on the Windows 10 operating system. It is in windows.winmd as part of the TH’s Windows SDK. For UWP, it’s referenced in windows.foundation.univeralappcontract.winmd.

To play back PlayReady-protected high definition (HD) content (1080p) and ultra-high definition (UHD) content, you will need to implement PlayReady hardware DRM. For information on how to implement PlayReady hardware DRM, see [Hardware DRM](hardware-drm.md).

Some content is not supported in hardware DRM. For information on disabling hardware DRM and enabling software DRM, see [Override Hardware DRM](hardware-drm.md#override-hardware-drm).

Regarding the media protection manager, make sure your code has the following settings if it doesn’t already:

```cs
var mediaProtectionManager = new Windows.Media.Protection.MediaProtectionManager();

mediaProtectionManager.Properties["Windows.Media.Protection.MediaProtectionSystemId"] = 
             '{F4637010-03C3-42CD-B932-B48ADF3A6A54}'
var cpsystems = new Windows.Foundation.Collections.PropertySet();
cpsystems["{F4637010-03C3-42CD-B932-B48ADF3A6A54}"] = 
                "Windows.Media.Protection.PlayReady.PlayReadyWinRTTrustedInput";
mediaProtectionManager.Properties["Windows.Media.Protection.MediaProtectionSystemIdMapping"] = cpsystems;

mediaProtectionManager.Properties["Windows.Media.Protection.MediaProtectionContainerGuid"] = 
                "{9A04F079-9840-4286-AB92-E65BE0885F95}";
```

## Proactively acquire a non-persistent license before playback

This section describes how to acquire non-persistent licenses proactively before playback begins.

In previous versions of PlayReady DRM, non-persistent licenses could only be acquired reactively during playback. In this version, you can acquire non-persistent licenses proactively before playback begins.

1.  Proactively create a playback session where the non-persistent license can be stored. For example:

    ```cs
    var cpsystems = new Windows.Foundation.Collections.PropertySet();       
    cpsystems["{F4637010-03C3-42CD-B932-B48ADF3A6A54}"] = "Windows.Media.Protection.PlayReady.PlayReadyWinRTTrustedInput"; // PlayReady

    var pmpSystemInfo = new Windows.Foundation.Collections.PropertySet();
    pmpSystemInfo["Windows.Media.Protection.MediaProtectionSystemId"] = "{F4637010-03C3-42CD-B932-B48ADF3A6A54}";
    pmpSystemInfo["Windows.Media.Protection.MediaProtectionSystemIdMapping"] = cpsystems;
    var pmpServer = new Windows.Media.Protection.MediaProtectionPMPServer( pmpSystemInfo );
    ```

2.  Tie that playback session to the license acquisition class. For example:

    ```cs
    var licenseSessionProperties = new Windows.Foundation.Collections.PropertySet();
    licenseSessionProperties["Windows.Media.Protection.MediaProtectionPMPServer"] = pmpServer;
    var licenseSession = new Windows.Media.Protection.PlayReady.PlayReadyLicenseSession( licenseSessionProperties );
    ```

3.  Create a license service request. For example:

    ```cs
    var laSR = licenseSession.CreateLAServiceRequest();
    ```

4.  Perform the license acquisition using the service request created from step 3. The license will be stored in the playback session.
5.  Tie the playback session to the media source for playback. For example:

    ```cs
    licenseSession.configureMediaProtectionManager( mediaProtectionManager );
    videoPlayer.msSetMediaProtectionManager( mediaProtectionManager );
    ```
    
## Query for protection capabilities
Starting with Windows 10, version 1703, you can query HW DRM capabilities, such as decode codecs, resolution, and output protections (HDCP). Queries are performed with the [**IsTypeSupported**](/uwp/api/windows.media.protection.protectioncapabilities.istypesupported) method which takes a string representing the capabilities for which support is queried and a string specifying the key system to which the query applies. For a list of supported string values, see the API reference page for [**IsTypeSupported**](/uwp/api/windows.media.protection.protectioncapabilities.istypesupported). The following code example illustrates the usage of this method.  

```cs
using namespace Windows::Media::Protection;

ProtectionCapabilities^ sr = ref new ProtectionCapabilities();

ProtectionCapabilityResult result = sr->IsTypeSupported(
L"video/mp4; codecs=\"avc1.640028\"; features=\"decode-bpp=10,decode-fps=29.97,decode-res-x=1920,decode-res-y=1080\"",
L"com.microsoft.playready");

switch (result)
{
    case ProtectionCapabilityResult::Probably:
    // Queue up UHD HW DRM video
    break;

    case ProtectionCapabilityResult::Maybe:
    // Check again after UI or poll for more info.
    break;

    case ProtectionCapabilityResult::NotSupported:
    // Do not queue up UHD HW DRM video.
    break;
}
```
## Add secure stop

This section describes how to add secure stop to your UWP app.

Secure stop provides the means for a PlayReady device to confidently assert to a media streaming service that media playback has stopped for any given piece of content. This capability ensures your media streaming services provide accurate enforcement and reporting of usage limitations on different devices for a given account.

There are two primary scenarios for sending a secure stop challenge:

-   When the media presentation stops because end of content was reached or when the user stopped the media presentation somewhere in the middle.
-   When the previous session ends unexpectedly (for example, due to a system or app crash). The app will need to query, either at startup or shutdown, for any outstanding secure stop sessions and send challenge(s) separate from any other media playback.

For a sample implementation of secure stop, see the **securestop.cs** file in the PlayReady sample located at the [Code Sample Browser](/samples/microsoft/windows-universal-samples/playready/).

## Use PlayReady DRM on Xbox One

To use PlayReady DRM in a UWP app on Xbox One, you will first need to register your [Partner Center](https://partner.microsoft.com/dashboard) account that you're using to publish the app for authorization to use PlayReady. You can do this in one of two ways:

* Have your contact at Microsoft request permission.
* Apply for authorization by sending your Partner Center account and company name to [pronxbox@microsoft.com](mailto:pronxbox@microsoft.com).

Once you receive authorization, you'll need to add an additional `<DeviceCapability>` to the app manifest. You'll have to add this manually because there is currently no setting available in the App Manifest Designer. Follow these steps to configure it:

1. With the project open in Visual Studio, open the **Solution Explorer** and right-click **Package.appxmanifest**.
2. Select **Open With...**, choose **XML (Text) Editor**, and click **OK**.
3. Between the `<Capabilities>` tags, add the following `<DeviceCapability>`:

    ```xml
    <DeviceCapability Name="6a7e5907-885c-4bcb-b40a-073c067bd3d5" />
    ```

4. Save the file.

Finally, there is one last consideration when using PlayReady on Xbox One: on development kits, there is an SL150 limit (that is, they can't play SL2000 or SL3000 content). Retail devices are able to play content with higher security levels, but to test your app on a dev kit, you'll need to use SL150 content. You can test this content in one of the following ways:

* Use curated test content that requires SL150 licenses.
* Implement logic so that only certain authenticated test accounts are able to acquire SL150 licenses for certain content.

Use the approach that makes the most sense for your company and your product.


## See also
- [Media playback](media-playback.md)
