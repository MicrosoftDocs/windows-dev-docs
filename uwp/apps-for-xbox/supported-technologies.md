---
title: Supported technologies on Xbox
description: Media formats and technologies supported on Xbox
ms.date: 06/30/2022
ms.topic: article
keywords: Xbox
---
# Supported technologies on Xbox
This section contains information about the various media formats and technologies that are supported on Xbox. 
## Streaming formats
The following streaming formats are supported:




| Format     | Notes |
|------------|-------------|
|MPEG-DASH| This is our [recommended](../audio-video-camera/adaptive-streaming.md)  streaming format |
|HLS|  |
|Smooth Streaming|Inbox Smooth components work (but not the legacy SDK). Consider switching to DASH if possible. |
|Custom streams using MSE/EME| MSE is supported via Webview.  |

## Video codecs 


|Codec  | Max resolution and frame rate | Highest profile supported | Console support |
|--- | --- | --- | --- |
|H264 | 1920X1080 @ 60fps| High |All Consoles |
|HEVC | 3840x2160 @ 60fps| Main, Main10| All Consoles (except Xbox One) |
|VP9 |3840x2160 @ 60fps|Profile 2 (8 & 10 bit) | Xbox One X, Xbox Series X & Xbox Series S |
|VC1/MPEG2/MPEG4| 1920x1080 @ 60fps|  | All Consoles |

## DRM
PlayReady is the only supported DRM format available on Xbox. Other formats (such as WideVine or FairPlay) are not supported. PlayReady is codec agnostic and can support the Xbox codecs' supported resolution. See this documentation for more information on PlayReady:

[https://www.microsoft.com/playready/features](https://www.microsoft.com/playready/features/productSuite/#feature-section)

## Samples and Documentation
Here is a list of helpful documentation and sample code which you may wish to reference as you develop your application.
NOTE: Several of the samples linked here are part of larger projects with a lot of shared code. You will need to clone the entire repository in order to build them. You may also need to install specific versions of the Windows SDK in order to build them. You should be able to do so using the Visual Studio Installer.

### General UWP Documentation

[XAML controls gallery sample](https://github.com/microsoft/WinUI-Gallery/tree/winui2)

[UWP samples](https://github.com/Microsoft/Windows-universal-samples)

- [WebView control sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/XamlWebView)
- [Video playback sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/VideoPlayback)
- [Video playback synchronization sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/VideoPlaybackSynchronization)
- [System media transport controls sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/SystemMediaTransportControls)
- [Background media playback sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BackgroundMediaPlayback)
- [Adaptive streaming sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AdaptiveStreaming)

[Application lifecycle docs](../launch-resume/app-lifecycle.md)

[App package manifest docs](/uwp/schemas/appxpackage/appx-package-manifest)
###  Media Playback Documentation
[Media playback docs (for C# and WebView-based applications)](../audio-video-camera/media-playback.md)

[Spatial sound docs](/windows/win32/coreaudio/spatial-sound)

[Playready DRM](../audio-video-camera/playready-client-sdk.md)
### Xbox Documentation
[Media App for Xbox samples (for JavaScript based applications)](https://github.com/microsoft/Media-App-Samples-for-Xbox)

[UWP development on Xbox docs](../xbox-apps/index.md)

[TVHelpers libraries (for JavaScript based applications)](https://github.com/Microsoft/TVHelpers)