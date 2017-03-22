---
author: drewbatgit
ms.assetid: AE98C22B-A071-4206-ABBB-C0F0FB7EF33C
description: This article describes how to add playback of adaptive streaming multimedia content to a Universal Windows Platform (UWP) app. This feature currently supports playback of Http Live Streaming (HLS) and Dynamic Streaming over HTTP (DASH) content.
title: Adaptive streaming
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Adaptive streaming

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

This article describes how to add playback of adaptive streaming multimedia content to a Universal Windows Platform (UWP) app. This feature currently supports playback of Http Live Streaming (HLS) and Dynamic Streaming over HTTP (DASH) content.

For a list of supported HLS protocol tags, see [HLS tag support](hls-tag-support.md). 

> [!NOTE] 
> The code in this article was adapted from the UWP [Adaptive streaming sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/AdaptiveStreaming).

## Simple adaptive streaming with MediaPlayer and MediaPlayerElement

To play adaptive streaming media in a UWP app, create a **Uri** object pointing to a DASH or HLS manifest file. Create an instance of the [**MediaPlayer**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer) class. Call [**MediaSource.CreateFromUri**](https://msdn.microsoft.com/library/windows/apps/dn930912) to create a new **MediaSource** object and then set that to the [**Source**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer.Source) property of the **MediaPlayer**. Call [**Play**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer.Play) to start playback of the media content.

[!code-cs[DeclareMediaPlayer](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetDeclareMediaPlayer)]

[!code-cs[ManifestSourceNoUI](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetManifestSourceNoUI)]

The above example will play the audio of the media content but it doesn't automatically render the content in your UI. Most apps that play video content will want to render the content in a XAML page.  To do this, add a [**MediaPlayerElement**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement) control to your XAML page.

[!code-xml[MediaPlayerElementXAML](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml#SnippetMediaPlayerElementXAML)]

Call [**MediaSource.CreateFromUri**](https://msdn.microsoft.com/library/windows/apps/dn930912) to create a **MediaSource** from the URI of a DASH or HLS manifest file. Then set the [**Source**](https://msdn.microsoft.com/library/windows/apps/br227420) property of the **MediaPlayerElement**. The **MediaPlayerElement** will automatically create a new **MediaPlayer** object for the content. You can call **Play** on the **MediaPlayer** to start playback of the content.

[!code-cs[ManifestSource](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetManifestSource)]

> [!NOTE] 
> Starting with Windows 10, version 1607, it is recommended that you use the **MediaPlayer** class to play media items. The **MediaPlayerElement** is a lightweight XAML control that is used to render the content of a **MediaPlayer** in a XAML page. The **MediaElement** control continues to be supported for backwards compatibility. For more information about using **MediaPlayer** and **MediaPlayerElement** to play media content, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md). For information about using **MediaSource** and related APIs to work with media content, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Adaptive streaming with AdaptiveMediaSource

If your app requires more advanced adaptive streaming features, such as providing custom HTTP headers, monitoring the current download and playback bitrates, or adjusting the ratios that determine when the system switches bitrates of the adaptive stream, use the [**AdaptiveMediaSource**](https://msdn.microsoft.com/library/windows/apps/dn946912) object.

The adaptive streaming APIs are found in the [**Windows.Media.Streaming.Adaptive**](https://msdn.microsoft.com/library/windows/apps/dn931279) namespace.

[!code-cs[AdaptiveStreamingUsing](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetAdaptiveStreamingUsing)]

Initialize the **AdaptiveMediaSource** with the URI of an adaptive streaming manifest file by calling [**CreateFromUriAsync**](https://msdn.microsoft.com/library/windows/apps/dn931261). The [**AdaptiveMediaSourceCreationStatus**](https://msdn.microsoft.com/library/windows/apps/dn946917) value returned from this method lets you know if the media source was created successfully. If so, you can set the object as the stream source for your **MediaPlayer** by creating a **MediaSource** object by calling  [**MediaSource.CreateFromAdaptiveMediaSource**](https://docs.microsoft.com/en-us/uwp/api/Windows.Media.Core.MediaSource#Windows_Media_Core_MediaSource_CreateFromAdaptiveMediaSource_Windows_Media_Streaming_Adaptive_AdaptiveMediaSource_), and then assigning it to the media player's [**Source**](https://docs.microsoft.com/en-us/uwp/api/windows.media.playback.mediaplayer#Windows_Media_Playback_MediaPlayer_Source) property. In this example, the [**AvailableBitrates**](https://msdn.microsoft.com/library/windows/apps/dn931257) property is queried to determine the maximum supported bitrate for this stream, and then that value is set as the inital bitrate. This example also registers handlers for the [**DownloadRequested**](https://msdn.microsoft.com/library/windows/apps/dn931272), [**DownloadBitrateChanged**](https://msdn.microsoft.com/library/windows/apps/dn931269), and [**PlaybackBitrateChanged**](https://msdn.microsoft.com/library/windows/apps/dn931278) events which are discussed later in this article.

[!code-cs[InitializeAMS](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetInitializeAMS)]

If you need to set custom HTTP headers for getting the manifest file, you can create an [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) object, set the desired headers, and then pass the object into the overload of **CreateFromUriAsync**.

[!code-cs[InitializeAMSWithHttpClient](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetInitializeAMSWithHttpClient)]

The [**DownloadRequested**](https://msdn.microsoft.com/library/windows/apps/dn931272) event is raised when the system is about to retrieve a resource from the server. The [**AdaptiveMediaSourceDownloadRequestedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn946935) passed into the event handler exposes properties that provide information about the resource being requested such as the type and URI of the resource.

You can use the **DownloadRequested** event handler to modify the resource request by updating the properties of the [**AdaptiveMediaSourceDownloadResult**](https://msdn.microsoft.com/library/windows/apps/dn946942) object provided by the event args. In the example below, the URI from which the resource will be retrieved is modified by updating the [**ResourceUri**](https://msdn.microsoft.com/library/windows/apps/dn931250) properties of the result object.

You can override the content of the requested resource by setting the [**Buffer**](https://msdn.microsoft.com/library/windows/apps/dn946943) or [**InputStream**](https://msdn.microsoft.com/library/windows/apps/dn931249) properties of the result object. In the example below, the contents of the manifest resource are replaced by setting the **Buffer** property. Note that if you are updating the resource request with data that is obtained asynchronously, such as retrieving data from a remote server or asynchronous user authentication, you must call [**AdaptiveMediaSourceDownloadRequestedEventArgs.GetDeferral**](https://msdn.microsoft.com/library/windows/apps/dn946936) to get a deferral and then call [**Complete**](https://msdn.microsoft.com/library/windows/apps/dn946934) when the operation is complete to signal the system that the download request operation can continue.

[!code-cs[AMSDownloadRequested](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetAMSDownloadRequested)]

The **AdaptiveMediaSource** object provides events that allow you to react when the download or playback bitrates change. In this example, the current bitrates are simply updated in the UI. Note that you can modify the ratios that determine when the system switches bitrates of the adaptive stream. For more information, see the [**AdvancedSettings**](https://msdn.microsoft.com/library/windows/apps/mt628697) property.

[!code-cs[AMSBitrateEvents](./code/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs#SnippetAMSBitrateEvents)]

## Related topics
* [Media playback](media-playback.md)
* [HLS tag support](hls-tag-support.md) 
* [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md)
* [Play media in the background](background-audio.md) 





