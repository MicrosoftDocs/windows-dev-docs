---
title: Xbox Media Application Architecture 
description: Architure decisions you should consider while designing an Xbox media application
ms.date: 05/18/2022
ms.topic: article
keywords: Xbox
---
# Xbox Media Application Architecture
All Xbox apps are packaged using the [Universal Windows Platform](../develop/index.md) but this does not mean that all Xbox applications must be written in XAML and C#. This section will go through several high-level architectural decisions you will need to make before you can start writing your Xbox application.

For general guidance on developing UWP applications for Xbox see this documentation:

[UWP on Xbox One - UWP applications](../xbox-apps/index.md)
### App development models
In general, there are 2 recommended patterns for writing a media app for Xbox:
1.    Using a website hosted in a WebView – You write a thin C# application which loads a website in a full-screen [WebView](/uwp/api/windows.ui.xaml.controls.webview). Most of your app's UI is written in web technologies like JavaScript and HTML, hosted on a server you manage. (Please note that WebView2 is not yet supported on Xbox.)
You might choose this option if: You are most comfortable working in web technologies, or you want to rapidly deploy changes to your UI without going through the app store publishing process.

2.    Using the native UWP API – Your app is built in XAML and C#, C++/Cx, or C++/WinRT, using [MediaElement](/windows/apps/design/controls/media-playback) and the [MediaPlayer API](../audio-video-camera/play-audio-and-video-with-mediaplayer.md) to play content.
You might choose this option if: You are most comfortable working in C#, or you want better performance than you can achieve with web technologies alone.


### User model
Applications on Xbox can operate under one of two different user models: Single User Application (SUA) or Multi-user Application (MUA).
SUAs run in the context of a single Xbox user, storing app data on a user-by-user basis. When the user account being used on the Xbox changes, SUAs are relaunched and run in the new user's context.

MUAs are run in the context of a generic user account and are not relaunched when the current user account changes. The app data is shared among all users who log into the system.

For more information, read the documentation here:
[Introduction to multi-user applications - UWP applications](../xbox-apps/multi-user-applications.md)

## Additional features
This section contains details on several additional features that you may want to consider using in your Xbox media application.
### Background audio
Your app can register to play audio in the background while other apps or games are running on the Xbox.

The documentation on enabling your app to run in the background can be found here:
[Play media in the background - UWP applications](../audio-video-camera/background-audio.md)

You will also want to integrate with the System Media Transport Controls, which allow the user to manipulate playback in the Xbox Guide without having to re-launch your app:

[Integrate with the System Media Transport Controls - UWP applications](../audio-video-camera/integrate-with-systemmediatransportcontrols.md)


### WebView audio playback
If you are hosting your application in a WebView, you have two options when it comes to where you play the background audio:
1.    You can play it using an HTML audio element within the WebView itself
2.    You can use [WebView.AddWebAllowedObject](/uwp/api/windows.ui.xaml.controls.webview.addweballowedobject) to allow your JavaScript code to call into C#, and do the playback using the C# [MediaPlayer](/uwp/api/windows.media.playback.mediaplayer) API

Approach #2 is strongly recommended because it allows your app to dispose the memory used by the WebView when it enters the background. This is much easier than trying to control your background memory usage without disposing of the WebView.

### Light and dark mode
Users can select a preference for light and dark themes in the Xbox settings menu. Your app can check the user's preference and render the app appropriately. For more information, see this documentation:

[Application.RequestedTheme Property (Windows.UI.Xaml) - Windows UWP applications](/uwp/api/windows.ui.xaml.application.requestedtheme)

### DIAL protocol support (Google Assistant, etc)
The DIAL protocol allows secondary devices (like tablets and phones) to launch content on your Xbox and automatically pair with it. For more details, see this documentation:
[Windows.Media.DialProtocol Namespace - Windows UWP applications](/uwp/api/windows.media.dialprotocol)

### Media remote
If you plan to support media remote functionality and use the controls on the media remote to control media playback, you will need to integrate SMTC into your code. For more details, see this documentation.

[SystemMediaTransportControls Class (Windows.Media) - Windows UWP applications | Microsoft Docs](/uwp/api/windows.media.systemmediatransportcontrols)

### Request ratings
There is an API you can call to request users to rate your application. This can help to bolster your ratings:

[Request ratings and reviews for your app - UWP applications](../monetize/request-ratings-and-reviews.md)

### Globalization and localization
If you plan to ship your application in multiple languages or markets, this guidance may be helpful:

[Globalization and localization - Windows apps](/windows/apps/design/globalizing/globalizing-portal)

If you are building a native application in C++ or C#, you can use a resources file to make the localization process easier:

[Localize strings in your UI and app package manifest - UWP applications](../app-resources/localize-strings-ui-manifest.md)
