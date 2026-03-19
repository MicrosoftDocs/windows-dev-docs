---
description: This article lists the MIME type strings supported for the Windows APIs that query for supported media playback features.
title: Supported media type strings for querying media feature support
ms.date: 03/18/2026
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Supported media type strings for querying media feature support

This article lists the MIME type strings supported for the Windows APIs that query for supported media playback types. The terms "content type" and "type" are well known historically as MIME typeThese base strings are consistent with those used in the HTML5 [HTMLMediaElement.canPlayType](https://developer.mozilla.org/docs/Web/API/HTMLMediaElement/canPlayType) method. These strings allow developers to query for whether specified media content, codecs, and features are supported.

The content type strings are defined in the following specifications.

- [RFC 2045](https://www.rfc-editor.org/rfc/rfc2045) This specification defines Content-Type strings that are used to specify media type and subtype identifiers.
- [RFC 6381](https://www.rfc-editor.org/rfc/rfc6381) This specification defines codec identifiers are used to specify codecs. 
- [RFC 2045](https://www.rfc-editor.org/rfc/rfc4281) This specification allows for additional, custom parameters as modifiers in the form of `";<parameter>=<name>[=<value>] [,<name>[=<value>]"`. RFC 2045 compliant parsers must ignore these parameters if not recognized. For the feature queries, `<parameter>` is named feature.
- [RFC 4281](https://www.rfc-editor.org/rfc/rfc4281)  This specification enables extensions to query for additional features.

The Windows implementation requires the RFC 2045 media type and subtype identifiers, for example `"video/mp4"`, and RFC 6381 codec parameter `codec="<video codec>[,<audio codec>]"` to always be present in order to provide valid query results. Some Windows APIs support an additional element of `feature=<features>`. The following tables list the supported strings that are supported by Windows APIs for each element of the type string.

The content type string format described in this article are used by the following APIs.

**WinRT APIs**

- [ProtectionCapabilities.IsTypeSupported](/uwp/api/windows.media.protection.protectioncapabilities.istypesupported)
- [Windows.UI.Xaml.Controls.MediaElement.CanPlayType(System.String)](/uwp/api/windows.ui.xaml.controls.mediaelement.canplaytype)


**Microsoft Media Foundation APIs**

- [IMFContentDecryptionModuleFactory::IsTypeSupported](/windows/win32/api/mfcontentdecryptionmodule/nf-mfcontentdecryptionmodule-imfcontentdecryptionmodulefactory-istypesupported)
- [IMFExtendedDRMTypeSupport::IsTypeSupportedEx](/windows/win32/api/mfmediaengine/nf-mfmediaengine-imfextendeddrmtypesupport-istypesupportedex)
- [IMFMediaEngineClassFactoryEx::IsTypeSupported](/windows/win32/api/mfmediaengine/nf-mfmediaengine-imfmediaengineclassfactoryex-istypesupported)
- [IMFMediaSourceExtension::IsTypeSupported](/windows/win32/api/mfmediaengine/nf-mfmediaengine-imfmediasourceextension-istypesupported)
- [IMFMediaEngine::CanPlayType](/windows/win32/api/mfmediaengine/nf-mfmediaengine-imfmediaengine-canplaytype)
- [IMFMediaEngineExtension::CanPlayType](/windows/win32/api/mfmediaengine/nf-mfmediaengine-imfmediaengineextension-canplaytype)

Many of these APIs return enumeration values that express the result as "NotSupported", "Maybe", or "Probably". For WinRT APIs there are the [ProtectionCapabilityResult](/uwp/api/windows.media.protection.protectioncapabilityresult) and [MediaCanPlayResponse](/uwp/api/windows.ui.xaml.media.mediacanplayresponse) enumerations. For Media Foundation there is the [MF_MEDIA_ENGINE_CANPLAY](/windows/win32/api/mfmediaengine/ne-mfmediaengine-mf_media_engine_canplay) enumeration. Some Media Foundation APIs simply return a boolean value indicating a binary supported or unsupported result.


## Media type and subtype

Windows APIs only support content type strings with the media type `"video"` and the subtype/container of `"mp4"`.


| Value | Description | Remarks |
|-------|-------------|----------------|
| "video/mp4" | Video media type and MPEG-4 subtype/container. | |



## Video codecs

| Value | Description | Remarks |
|-------|-------------|----------------|
| "avc1" | H.264 | |
| "hvc1" | HEVC  | |
| "hev1" | HEVC  | |
| "vp9" | VP9 | |
| "vp09" | VP9 | |
| "avc3" | H.264 | |
| "av1" | AOMedia Video 1 | |
| "av01" | AOMedia Video 1 | |


## Features

|Item  |Sub-system  |Feature Name  |Feature Value |Description |Mandatory for this subsystem | Remarks |
|---------|---------|---------|---------|---------|---------|--------|
|1a     |Video Decode   |decode-res-x         |Non-negative number in pixels         |Does the video decoder support this maximum resolution in the X-axis?         |Y       | |
|1b     |Video Decode   |decode-res-y         |Non-negative number in pixels         |Does the video decoder support this maximum resolution in the Y-axis?         |Y       | |
|1c     |Video Decode   |decode-bitrate         |Positive number in kilobits per second (Kbps)         |Does the video decoder support this maximum bitrate?         |Y       | |
|1d     |Video Decode   |decode-fps         |24, 25, 29.97, 30, 50, 59.94, or 60         |Does the video decoded support this maximum frames per second (FPS) value?         |Y       | |
|1e     |Video Decode   |decode-bpc (decode-bpp is deprecated)         |0, 8, 10, or 12         |Can the video decoder consume this per-pixel color depth?         |Y       | |
|1f     |Video Decode   |decoder-hardware-acceleration         |1 or no value as true         |Is DXVA hardware acceleration available regardless of an OS decoder being present?         |N       | Support introduced in Windows 10, version 1709 |
|1g     |Video Decode   |decoder-software-acceleration          |1 or no value as true         |Is an OS decoder present capable of decoding the stream?         |N       | Support introduced in Windows 10, version 1709 |
|1h     |Video Decode   |decoder-software-requires-hardware         |1 or no value as true         |Does the OS decoder’s functionality require that DXVA hardware acceleration is present?         |N       |  Support introduced in Windows 10, version 1709 |
|2a     |Video Display 1|display-res-x         |Non-negative number in pixels         |Does at least one intersecting display support this resolution in the X-axis? See [Intersection algorithm for resolution](#intersection-algorithm-for-resolution).        |Y       | |
|2b     |Video Display 1|display-res-y         |Non-negative number in pixels         |Does at least one intersecting display support this resolution in the Y-axis?         |Y       | See [Intersection algorithm for resolution](#intersection-algorithm-for-resolution).  |
|2c     |Video Display 1|display-refreshrate         |24, 25, 29.97, 30, 50, 59.94, or 60         |Is the display configured (as understood by Windows) for at least the requested refresh rate?         |N       | |
|2d     |Video Display 1|display-bpc (display-bpp is deprecated)         |8 or 10         |Do all intersecting displays with ≥ required resolution realize at least this color depth?         |N       | |
|3     |Video Display 2<sup>*</sup>|hdr         |1 (supported)         |Does the target support High Dynamic Range (HDR) rendering         |Y       | |
|4     |Video Output Protection|hdcp         |0 (off), 1 (on without HDCP 2.2 Type 1 restriction), 2 (on with HDCP 2.2 Type 1 restriction         |Do all intersecting enabled displays support at least the request protection level?         |Y       | |
|5     |Video General: Efficiency</sup>|efficiency-setting         |0 (off = no restriction), 1 (on = limit resolution when on battery power)         |Does the user want battery life, streaming overhead, and/or download speed in preference to highest resolution?        |Y       | Support introduced in Windows 10, version 1709. See [Resolution with efficiency setting enabled](#resolution-with-efficiency-setting-enabled). |
|6a     |Video Decryption|encryption-type         |“cenc” or “cbcs”        |Is this encryption type supported for decryption with the specified codec / key-system? If value is unspecified, default value of "cenc" is used.        |N       | |
|6b     |Video Decryption|encryption-iv-size         |8 or 16 |Is this Initialization Vector (IV) size (in bytes) supported for decryption with the specified codec / key-system? If value is unspecified, default value of 8 is used.        |N       | |
| 7 | Audio Render | audio-endpoint-codec | An audio codec string. See [Supported audio endpoint codecs] (#supported-audio-endpoint-codecs) | Is this audio codec supported natively by the audio endpoint? | N | Support introduced in Windows 10, build 1803.. Only supported for Microsoft Media Foundation APIs. Unsupported for WinRT APIs. |



### Intersection algorithm for resolution

The intersection algorithm is:

1. Find all displays where application user interface video client region has pixels.
2. Find all graphics adapters driving the displays from step 1.  For a hardware DRM query, this set of adapters is filtered to only those adapters having hardware DRM support.
3. Find all displays connected to the graphics adapter(s) from step 2.

### Resolution with efficiency setting enabled

It is up to the content provider to choose the resolution limit to use when this policy is on. A 1080p limit is recommended, but 720p may be used.  Note that the input for this policy comes from the Video Settings user interface page added in Windows 10, version 1709. 

## Audio codecs

| Value | Description | Remarks |
|-------|-------------|---------|
| "mp4a" | MPEG-4 Audio | |
| "ac-3" | Dolby Digital | |
| "ec-3" | Dolby Digital Plus | |
| "ac-4" | Dolby AC-4 | |
| "flac" | FLAC | |
| "mp3" | MP3 | |

### Supported audio endpoint codecs

Some audio playback scenarios require the audio endpoint to support a given audio codec or feature natively. The *audio-endpoint-codec* extension is useful for applications and streaming services, allowing them to figure out dynamically whether they should send stereo audio or 5.1 (because the device supports 5.1), and therefore control used bandwidth while maximizing audio quality. It also exposes information on whether the audio endpoint is connected to a device that can decode the audio format in hardware instead of relying on the software decoder on the system.

The `audio-endpoint-codec` query differs from the `codecs` query because it determines whether the audio endpoint device connected to the PC supports the specified format. So, for example, if a PC has the software decoder for the AC3 (Dolby Digital) codec, the `codecs=ac-3` query will succeed. If the PC is using basic headphones as the audio endpoint, the `audio-endpoint-codec=DD` query will fail. But if the same PC is then connected to an audio/video receiver that can decode AC3 format, the `audio-endpoint-codec=DD` query will pass.

Support for *audio-endpoint-codec* was introduced in Windows 10, build 1803.

The following is an example content type string using *audio-endpoint-codec* checking for the audio endpoint hardware support for the Dolby Digital decoder.

`'video/mp4; codecs="avc1,mp4a"; features="audio-endpoint-codec=DD"'`

The following is an example content type string checking for the number of channels supported by the audio endpoint.

`'video/mp4; features="audio-endpoint-codec=PCM2.0"'`

Note that if the endpoint supports more or the same number of channels as specified, the check will succeed. So, if the system is configured for 5.1 audio, then checks for "PCM2.0" and "PCM5.1" will pass, but a check for "PCM7.1" will return a failure.


| Codec string | Description | Remarks |
|--------------|-------------|---------|
| DD             | Dolby Digital            |        |
| DD+ | Dolby Digital Plus | | 
| DD+JOC             | Dolby Digital + JOC compressed spatial audio            |         |
| DTS |   DTS compressed audio          |        |
| DTSHD |   DTS-HD compressed audio          |         |
| PCM2.0 |  Uncompressed 2-channel audio           |         |
| PCM5.1 |  Uncompressed 5.1 channel audio           |         |
| PCM7.1 |  Uncompressed 7.1 channel audio          |         |
| AC3 | Dolby Digital | |

> [!NOTE]
> The `codecs=ac-3` query and the `audio-endpoint-codec=DD` query check for different capabilities despite both referencing the Dolby Digital codec. The `codecs` query checks whether a software decoder is present on the system that can decode the format, while the `audio-endpoint-codec` query checks whether the connected audio endpoint device natively supports the format. The result of one query does not affect the other — a system can have a software decoder without a capable endpoint or a capable endpoint without a software decoder.

### Audio capability query examples

Windows supports enhanced MIME CanPlayType queries that allow a client to discover the audio capabilities of a device. You can query the CanPlayType/IsTypeSupported API for audio capabilities to find out what codecs are available on the machine, the number of speakers the system is configured for, and the capabilities of the connected audio endpoint device.

**Querying for audio codec support**

Use the `codecs` parameter to determine whether the system has a software decoder that can decode the specified audio format into PCM and play it through the connected speaker or headphone configuration.

`video/mp4; codecs="ac-3"`

This query checks whether there is a software decoder that can decode AC-3 (Dolby Digital) audio.

**Querying for audio endpoint device capabilities**

Use the `audio-endpoint-codec` feature to determine the capabilities of the audio endpoint device, such as whether the system is connected to an audio/video receiver (AVR) that can decode a specific audio format.

`video/mp4; codecs="avc3,mp4a";features="audio-endpoint-codec=DD"`

This query checks whether the connected audio endpoint device, such as an AVR, can play Dolby Digital audio.

**Querying for speaker configuration**

Use the `audio-endpoint-codec` feature with a PCM codec string to discover the number of speakers the system is configured for.

`'video/mp4; features="audio-endpoint-codec=PCM5.1"'`

This query checks whether the system is configured for 5.1 speakers and can play 5.1-channel PCM audio. If the endpoint supports an equal or greater number of channels than specified, the check succeeds.

### Example audio capability matrix

When systems with Dolby Atmos or headphones that support Virtual Surround Sound (VSS) are considered, the number of possible hardware and software combinations can be very large. The following table shows the query results for `audio-endpoint-codec` and `codecs` across representative device configurations.

| # | Device configuration | Dolby SW Decoder | DD | DD+ | DD+JOC | PCM2.0 | PCM5.1 | PCM7.1 | codecs=ac-3 |
|---|---------------------|---------------|:---:|:---:|:------:|:------:|:------:|:------:|:-----------:|
| 1 | Headphones with Atmos, VSS enabled | Installed | ✗ | ✗ | ✓ | ✓ | ✓ | ✓ | ✓ |
| 2 | Headphones without Atmos, VSS disabled | Installed | ✗ | ✗ | ✗ | ✓ | ✗ | ✗ | ✓ |
| 3 | Headphones without Atmos, VSS enabled | Installed | ✗ | ✗ | ✗ | ✓ | ✓ | ✓ | ✓ |
| 4 | Headphones without Atmos, VSS enabled | Not installed | ✗ | ✗ | ✗ | ✓ | ✓ | ✓ | ✗ |
| 5 | 5.1 speakers (no AVR) | Installed | ✗ | ✗ | ✗ | ✓ | ✓ | ✗ | ✓ |
| 6 | 5.1 speakers (no AVR) | Not installed | ✗ | ✗ | ✗ | ✓ | ✓ | ✗ | ✗ |
| 7 | Atmos-capable AVR, configured for 5.1 | Not installed | ✓ | ✓ | ✓ | ✓ | ✓ | ✗ | ✗ |
| 8 | Non-Atmos AVR, configured for 5.1 | Installed | ✓ | ✓ | ✗ | ✓ | ✓ | ✗ | ✓ |

The DD, DD+, DD+JOC, PCM2.0, PCM5.1, and PCM7.1 columns show the results of the `audio-endpoint-codec` feature query.

