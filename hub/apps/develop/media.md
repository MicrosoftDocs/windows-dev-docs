---
title: Media features for developers in Windows 11
description: This page provides guidance for choosing media features for application development scenarios such as audio and video capture and playback.
ms.topic: article
ms.date: 09/12/2019
keywords: Media in Windows 11, media, video, audio, win32 media apps, UWP media apps, WPF media apps, WinForms media apps
ms.author: drewbat
author: drewbatgit
---

# Media features for developers in Windows 11

![Speech hero image](images/hero-speech-composite-small.png)

Media is an important component for a wide variety of applications from entertainment and art to education and accessibility. Windows provides several different media frameworks that enable you to implement common scenarios such as audio and video playback, capture, and transcoding. This article lists the different API sets that you can use to create media applications on Windows and provides some guidance in picking the framework that's right for your application. 

## Media frameworks for Windows


:::row:::
   :::column:::
        [Microsoft Media Foundation](/windows/win32/medfound/microsoft-media-foundation-sdk)

        Lorem ipsum
   :::column-end:::
   :::column:::
       [Windows Runtime APIs](/uwp/api/)

        Lorem ipsum
   :::column-end:::
:::row-end:::

:::row:::
   :::column:::
        [Windows Core Audio APIs (WASAPI)](/windows/win32/coreaudio/core-audio-apis-in-windows-vista)

        Lorem ipsum
   :::column-end:::
   :::column:::
        [DirectSound](/previous-versions/windows/desktop/ee416960(v=vs.85))

        Lorem ipsum
   :::column-end:::
   :::column:::
        [XAudio2](/windows/win32/xaudio2/xaudio2-apis-portal)

        Lorem ipsum
   :::column-end:::
:::row-end:::

## Media APIs by application scenario 

## Load and capture media content


### MediaEngine file playback 

I'm not sure why this is in "load and capture" rather than "render" below. I don't see any how-to content for MediaEngine. The MF basic playback scenario uses MediaSession: [How to Play Media Files with Media Foundation](/windows/win32/medfound/how-to-play-unprotected-media-files). Is this a gap in our content?


### SourceReader & SinkWriter

Lorem ipsum. 
[Using the Source Reader to Process Media Data](/windows/win32/medfound/processing-media-data-with-the-source-reader) 
[Sink Writer](/windows/win32/medfound/sink-writer)

### MediaStreamSource 

I know this is a gap in our content. We don't have a simple MSS how-to and the sample is old.
[/uwp/api/Windows.Media.Core.MediaStreamSource?view=winrt-22000](/uwp/api/Windows.Media.Core.MediaStreamSource?view=winrt-22000)



### CaptureEngine for camera capture

I don't see any how-to content for "CaptureEngine" the MF docs have [Audio/Video Capture in Media Foundation](/windows/win32/medfound/audio-video-capture-in-media-foundation)

### Network source & sink

I found this article for network sources:
[Network Source Features](/windows/win32/medfound/network-source-features)

And the parent article:
[Networking in Media Foundation](/windows/win32/medfound/networking-in-media-foundation)

I couldn't find anything explicitly for "Network Sink" but I found this:
[Media Sinks](/windows/win32/medfound/media-sinks)

### Camera capture preview rendering

I'm familiar with the WinRT APis for this. I don't know if there is also a recommended MF version
[Display the camera preview](/windows/uwp/audio-video-camera/simple-camera-preview-access)

### CameraCaptureUI 

Lorem ipsum
[Capture photos and video with the Windows built-in camera UI](/windows/uwp/audio-video-camera/capture-photos-and-video-with-cameracaptureui)


### MediaCapture 

Lorem ipsum
 [Basic photo, video, and audio capture with MediaCapture](/windows/uwp/audio-video-camera/basic-photo-video-and-audio-capture-with-mediacapture) 

### MediaFrameReader 

Lorem ipsum
[Process media frames with MediaFrameReader](/windows/uwp/audio-video-camera/basic-photo-video-and-audio-capture-with-mediacapture) 

### Capturing an Audio Stream with WASAPI

Lorem ipsum
[Capturing a Stream](/windows/win32/coreaudio/capturing-a-stream)

## Rendering Audio


### Render an audio stream with WASAPI

[Rendering a Stream](/windows/win32/coreaudio/rendering-a-stream)

### Render audio with DirectSound

This was in the outline, but the content is in the archive section of the docs. Is this current technology that devs should be using?

[Playing Sounds](/previous-versions/ms804971(v=msdn.10))

### Render audio with Media Foundation Streaming Audio Renderer (SAR

Lorem ipsum
[Streaming Audio Renderer](/windows/win32/medfound/streaming-audio-renderer)

### Render audio with XAudio2

Lorem ipsum
[How to: Play a Sound with XAudio2](/windows/win32/xaudio2/how-to--play-a-sound-with-xaudio2)

### Spatial sound

Lorem ipsum
[Render Spatial Sound Using Spatial Audio Objects](/windows/win32/coreaudio/render-spatial-sound-using-spatial-audio-objects)

### Audio graphs

Lorem ipsum
[Audio graphs](/windows/uwp/audio-video-camera/audio-graphs) | |

## Rendering Video

## MediaEngine

MediaEngine was listed above in capture/load. Again, I don't see any conceptual content for it.

[IMFMediaEngine interface (mfmediaengine.h)](/windows/win32/api/mfmediaengine/nn-mfmediaengine-imfmediaengine)

### MediaPlayer and MediaPlayerElement

[Play audio and video with MediaPlayer](/windows/uwp/audio-video-camera/play-audio-and-video-with-mediaplayer) 

### Direct rendering from a source

I'm not sure what this one is referring to.

### Frame server mode 

This is the WinRT content for frame server mode. Is this what you were talking about?

[Use MediaPlayer in frame server mode](/windows/uwp/audio-video-camera/play-audio-and-video-with-mediaplayer#use-mediaplayer-in-frame-server-mode)




## Effects

### Media Foundation Transforms (MFTs)

[Media Foundation Transforms](/windows/win32/medfound/media-foundation-transforms)

Not sure if we have how-to content for: Adding an effect MFT to MediaEngine
Not sure if we have how-to content for: Adding an effect MFT to SourceReader


### Custom video effects 

Lorem ipsum
[Custom video effects](/windows/uwp/audio-video-camera/custom-video-effects) 
 

### Custom audio effects 

Lorem ipsum
[Custom video effects](/windows/uwp/audio-video-camera/custom-video-effects) 


## Debugging

### Debuggung with MXA tool

I can't find much content. This page on the hardware site references it:
[Performance tools](/windows-hardware/test/weg/performance-tools)

## Content Protection


[PlayReady DRM](/windows/uwp/audio-video-camera/audio-graphs) 


[How to Play Protected Media Files](/windows/win32/medfound/how-to-play-protected-media-files)

### TBD

- mention that EVR is deprecated. Other deprecated APIs?
- Windows-classic-samples/Samples/CaptureEngineVideoCapture at main · microsoft/Windows-classic-samples · GitHub
TopoEdit

## Platform-specific documentation

The content in the remaining sections is from the speech page I copied as a template. We might consider whether we want to list the above features in a "per-platform" pivot. But I think it would probably be better to label each entry above with something like "Platforms: Win32, UWP"

:::row:::
   :::column:::
      ![Universal Windows Platform (UWP)](images/platform-uwp.png)

      **Universal Windows Platform (UWP)**

      Build media-enabled apps on the modern platform for Windows 10 applications and games, on any Windows device (including PCs, phones, Xbox One, HoloLens, and more), and publish them to the Microsoft Store.

      [Speech interactions](/windows/uwp/design/input/speech-interactions)

      [Speech recognition](/windows/uwp/design/input/speech-recognition)

      [Continuous dictation](/windows/uwp/design/input/enable-continuous-dictation)

      [Speech synthesis](/uwp/api/windows.media.speechsynthesis)

      [Conversational agents](/uwp/api/windows.applicationmodel.conversationalagent)

      [Cortana voice commands](/cortana/voice-commands/vcd)<br>
      (not supported in Windows 10 May 2020 Update and newer)
   :::column-end:::
   :::column:::
      ![Win32 platform apps](images/platform-win32.png)

      **Win32 platform**

      Develop speech-enabled applications for Windows desktop and Windows Server using the tools, information, and sample engines and applications provided here.

      [Microsoft Speech Platform - Software Development Kit (SDK) (Version 11)](https://www.microsoft.com/download/details.aspx?id=27226)
      
      [Microsoft Speech SDK, version 5.1](https://www.microsoft.com/download/details.aspx?id=10121)
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      ![.NET](images/platform-dotnet.png)

      **.NET Framework**

      Develop accessible apps and tools on the established platform for managed Windows applications with a XAML UI model and the .NET Framework.

      [System.Speech Programming Guide for .NET Framework](/previous-versions/office/developer/speech-technologies/hh361625(v=office.14))
   :::column-end:::
   :::column:::
      ![Azure speech services](images/platform-azure-speech.png)

      **Azure speech services**

      Integrate speech processing into apps and services.

      [Speech to text](https://azure.microsoft.com/services/cognitive-services/speech-to-text/)

      [Text to speech](https://azure.microsoft.com/services/cognitive-services/text-to-speech/)
      
      [Speech translation](https://azure.microsoft.com/services/cognitive-services/speech-translation/)

      [Speaker Recognition](https://azure.microsoft.com/en-us/services/cognitive-services/speaker-recognition/)

      [Voice-first virtual assistants](/azure/cognitive-services/speech-service/voice-first-virtual-assistants)
   :::column-end:::
:::row-end:::
:::row:::
   :::column span="2":::
      **Legacy features**

      Legacy, deprecated, and/or unsupported versions of Microsoft speech and conversation technology.
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [Cortana Skills Kit](/cortana/skills/)

      As part of our goal to transform the modern productivity experiences by embedding Cortana deeply into [Microsoft 365](/microsoft-365/admin/misc/cortana-integration), we are retiring the Cortana Skills Kit developer platform and all skills built on this platform.
   :::column-end:::
   :::column:::

      [Microsoft Agent](/windows/win32/lwef/microsoft-agent)

      [Microsoft Speech Application Software Development Kit (SASDK) Version 1.0](https://www.microsoft.com/download/details.aspx?id=2200)

      [Microsoft Speech API (SAPI) 5.3](/previous-versions/windows/desktop/ms723627(v=vs.85))

      [Microsoft Speech API (SAPI) 5.4](/previous-versions/windows/desktop/ee125663(v=vs.85))

      [The Bing Speech Recognition Control](/previous-versions/bing/speech/dn434583(v=msdn.10))
   :::column-end:::
:::row-end:::

## Samples

Download and run full Windows samples that demonstrate various accessibility features and functionality.

:::row:::
   :::column:::
      [Code sample browser](/samples/browse/?term=speech)

      The new samples browser (replaces the MSDN Code Gallery).
   :::column-end:::
   :::column:::
      [Windows classic samples on GitHub](https://github.com/microsoft/Windows-classic-samples/search?q=speech&unscoped_q=speech)

      These samples demonstrate the functionality and programming model for Windows and Windows Server. 
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [Universal Windows Platform (UWP) samples on GitHub](https://github.com/microsoft/Windows-universal-samples/search?q=speech&unscoped_q=speech)

      These samples demonstrate the API usage patterns for the Universal Windows Platform (UWP) in the Windows Software Development Kit (SDK) for Windows 10.
   :::column-end:::
   :::column:::
      [XAML Controls Gallery](https://github.com/microsoft/Xaml-Controls-Gallery)

      This app demonstrates the various Xaml controls supported in the Fluent Design System.
   :::column-end:::
:::row-end:::

## Videos

Various videos covering how to build Windows applications that incorporate speech interactions.

:::row:::
   :::column:::
      **Cortana and Speech Platform In Depth**
   :::column-end:::
   :::column:::
      **Cortana Extensibility in Universal Windows Apps**
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      > [!VIDEO https://channel9.msdn.com/Events/Build/2015/3-716/player]
   :::column-end:::
   :::column:::
      > [!VIDEO https://channel9.msdn.com/Events/Build/2015/2-691/player]
   :::column-end:::
:::row-end:::

## Other resources

:::row:::
   :::column:::
      **Blogs and news**

      The latest from the world of Microsoft speech.
   :::column-end:::
   :::column:::
      **Community and support**

      Where Windows developers and users meet and learn together.
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [In the news](https://news.microsoft.com/?s=speech)

      [Speech blogs](https://blogs.windows.com/windowsdeveloper/?s=speech)
   :::column-end:::
   :::column:::
      [Windows community - Speech](https://community.windows.com/search?q=speech)

      [Windows Speech Developer's Forum](https://social.msdn.microsoft.com/Forums/home?filter=alltypes&sort=firstpostdesc&searchTerm=speech)
   :::column-end:::
:::row-end:::