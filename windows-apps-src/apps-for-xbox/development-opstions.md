---
title: Xbox Media App Development Options
description: This section describes possible architectures and development environments for building Xbox media applications
ms.date: 04/21/2022
ms.topic: article
keywords: Xbox
ms.author: HiHaile
author: Hilina-H
---
# Xbox Media App Development Options 
### App development models
In general, there are 3 recommended patterns for writing a media app for Xbox:
1.	Using a website hosted in a WebView – You write a thin C# application which loads a website in a full-screen [WebView](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.WebView?view=winrt-22000). Most of your app’s UI is written in web technologies like JavaScript and HTML, hosted on a server you manage. (Please note that WebView2 is not yet supported on Xbox.)
You might choose this option if: You are most comfortable working in web technologies, or you want to rapidly deploy changes to your UI without going through the app store publishing process.

2.	Using the native UWP API – Your app is built in XAML and C#, using [MediaElement](https://docs.microsoft.com/en-us/windows/apps/design/controls/media-playback) and the [MediaPlayer API](https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/play-audio-and-video-with-mediaplayer) to play content.
You might choose this option if: You are most comfortable working in C#, or you want better performance than you can achieve with web technologies alone.

3.	Using Media Foundation API – Your app is built in [C++/Cx](https://docs.microsoft.com/en-us/cpp/cppcx/visual-c-language-reference-c-cx?view=msvc-170) or [C++/WinRT](https://docs.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/) using the lower-level [Microsoft Media Foundation API](https://docs.microsoft.com/en-us/windows/win32/medfound/microsoft-media-foundation-sdk)Through [DirectX](https://docs.microsoft.com/en-us/windows/win32/directx)you have full control over the rendering pipeline. This option requires the most effort. 
You might choose this option if: You want peak performance and a high degree of customization around how your app presents media. 
### Choosing the right version of Visual Studio 

The primary IDE used for Xbox app development is Visual Studio. You should familiarize yourself with the section in this document on App Development Models before deciding which version to install. 

If you are developing your UI using mostly web technologies hosted in a WebView, it is best to use Visual Studio 2017. It is the only version with comprehensive JavaScript debugging tools which work with the original WebView control. 

[Visual Studio Older Downloads](https://visualstudio.microsoft.com/vs/older-downloads/)

If you are developing your UI primarily in XAML or using technologies like MediaFoundation and DirectX to build your UI from scratch, you can use any version from 2017 onwards. It is recommended that you use the latest release. 

[Download Visual Studio tools](https://visualstudio.microsoft.com/downloads/) 

NOTE: It can be tricky to fully downgrade your development environment after using a later version, so it’s recommended that you only install the version you need. 

### Installing the right components 

Once you’ve chosen a Visual Studio version, follow these instructions to get it set up: 

[Getting started with UWP app development on Xbox One - UWP application](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/getting-started)

### Deploying your app to Xbox 

Follow these instructions to put your PC and Xbox into developer mode, and pair Visual Studio with your Xbox so that you can deploy your app to it: 

[Getting started with UWP app development on Xbox One - UWP application](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/getting-started)

### Custom build automation 

If you want to use a different application as your main IDE, or if you want to build your own custom build tools, apps can be built using the command line: 

[Package from the command line - MSIX](https://docs.microsoft.com/en-us/windows/msix/package/manual-packaging-root)

You can also use Azure Pipelines to set up automated builds for your application: 

[Set up automated build for your UWP app - UWP application](https://docs.microsoft.com/en-us/windows/uwp/packaging/auto-build-package-uwp-apps)

### Manual side-loading 

If you want to side-load your application onto your Xbox manually, you will need to first generate a signed appxpackage either using the command line tools described under Custom build automation, or from Visual Studio: 

[Packaging MSIX apps - MSIX](https://docs.microsoft.com/en-us/windows/msix/package/packaging-uwp-apps)

Then, you can use the Home tab of the Xbox Device Portal to side-load your app and its dependencies: 

[Device Portal for Xbox - UWP application](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/device-portal-xbox)

