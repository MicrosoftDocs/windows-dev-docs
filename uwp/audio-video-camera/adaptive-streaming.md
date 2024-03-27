---
ms.assetid: AE98C22B-A071-4206-ABBB-C0F0FB7EF33C
description: This article describes how to add playback of adaptive streaming multimedia content to a Universal Windows Platform (UWP) app. This feature currently supports playback of Http Live Streaming (HLS) and Dynamic Streaming over HTTP (DASH) content.
title: Adaptive streaming
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Adaptive streaming


This article describes how to add playback of adaptive streaming multimedia content to a Universal Windows Platform (UWP) app. This feature supports playback of Http Live Streaming (HLS) and Dynamic Streaming over HTTP (DASH) content. 

Starting with Windows 10, version 1803, Smooth Streaming is supported by  **[AdaptiveMediaSource](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource)**. Note that for Smooth Streaming, only H264 and WVC1 codecs are supported. Other manifest types do not have this limitation.

For a list of supported HLS protocol tags, see [HLS tag support](hls-tag-support.md). 

For a list of supported DASH profiles, see [DASH profile support](dash-profile-support.md). 

> [!NOTE] 
> The code in this article was adapted from the UWP [Adaptive streaming sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/AdaptiveStreaming).

## Simple adaptive streaming with MediaPlayer and MediaPlayerElement

To play adaptive streaming media in a UWP app, create a **Uri** object pointing to a DASH or HLS manifest file. Create an instance of the [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) class. Call [**MediaSource.CreateFromUri**](/uwp/api/windows.media.core.mediasource.createfromuri) to create a new **MediaSource** object and then set that to the [**Source**](/uwp/api/windows.media.playback.mediaplayer.source) property of the **MediaPlayer**. Call [**Play**](/uwp/api/windows.media.playback.mediaplayer.play) to start playback of the media content.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetDeclareMediaPlayer":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetManifestSourceNoUI":::

The above example will play the audio of the media content but it doesn't automatically render the content in your UI. Most apps that play video content will want to render the content in a XAML page.  To do this, add a [**MediaPlayerElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) control to your XAML page.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml" id="SnippetMediaPlayerElementXAML":::

Call [**MediaSource.CreateFromUri**](/uwp/api/windows.media.core.mediasource.createfromuri) to create a **MediaSource** from the URI of a DASH or HLS manifest file. Then set the [**Source**](/uwp/api/windows.ui.xaml.controls.mediaelement.sourceproperty) property of the **MediaPlayerElement**. The **MediaPlayerElement** will automatically create a new **MediaPlayer** object for the content. You can call **Play** on the **MediaPlayer** to start playback of the content.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetManifestSource":::

> [!NOTE] 
> Starting with Windows 10, version 1607, it is recommended that you use the **MediaPlayer** class to play media items. The **MediaPlayerElement** is a lightweight XAML control that is used to render the content of a **MediaPlayer** in a XAML page. The **MediaElement** control continues to be supported for backwards compatibility. For more information about using **MediaPlayer** and **MediaPlayerElement** to play media content, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md). For information about using **MediaSource** and related APIs to work with media content, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Adaptive streaming with AdaptiveMediaSource

If your app requires more advanced adaptive streaming features, such as providing custom HTTP headers, monitoring the current download and playback bitrates, or adjusting the ratios that determine when the system switches bitrates of the adaptive stream, use the **[AdaptiveMediaSource](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource)** object.

The adaptive streaming APIs are found in the [**Windows.Media.Streaming.Adaptive**](/uwp/api/Windows.Media.Streaming.Adaptive) namespace. The examples in this article use APIs from the following namespaces.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetAdaptiveStreamingUsing":::

## Initialize an AdaptiveMediaSource from a URI.

Initialize the **AdaptiveMediaSource** with the URI of an adaptive streaming manifest file by calling [**CreateFromUriAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.createfromuriasync). The [**AdaptiveMediaSourceCreationStatus**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSourceCreationStatus) value returned from this method lets you know if the media source was created successfully. If so, you can set the object as the stream source for your **MediaPlayer** by creating a **MediaSource** object by calling  [**MediaSource.CreateFromAdaptiveMediaSource**](/uwp/api/Windows.Media.Core.MediaSource.AdaptiveMediaSource), and then assigning it to the media player's [**Source**](/uwp/api/windows.media.playback.mediaplayer.Source) property. In this example, the [**AvailableBitrates**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.availablebitrates) property is queried to determine the maximum supported bitrate for this stream, and then that value is set as the initial bitrate. This example also registers handlers for the several **AdaptiveMediaSource** events that are discussed later in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetInitializeAMS":::

## Initialize an AdaptiveMediaSource using HttpClient

If you need to set custom HTTP headers for getting the manifest file, you can create an [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) object, set the desired headers, and then pass the object into the overload of **CreateFromUriAsync**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetInitializeAMSWithHttpClient":::

The [**DownloadRequested**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.downloadrequested) event is raised when the system is about to retrieve a resource from the server. The [**AdaptiveMediaSourceDownloadRequestedEventArgs**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSourceDownloadRequestedEventArgs) passed into the event handler exposes properties that provide information about the resource being requested such as the type and URI of the resource.

## Modify resource request properties using the DownloadRequested event

You can use the **DownloadRequested** event handler to modify the resource request by updating the properties of the [**AdaptiveMediaSourceDownloadResult**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSourceDownloadResult) object provided by the event args. In the example below, the URI from which the resource will be retrieved is modified by updating the [**ResourceUri**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadresult.resourceuri) properties of the result object. You can also rewrite the byte range offset and length for media segments or, as shown the example below, change the resource URI to download the full resource and set the byte range offset and length to null.

You can override the content of the requested resource by setting the [**Buffer**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadresult.buffer) or [**InputStream**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadresult.inputstream) properties of the result object. In the example below, the contents of the manifest resource are replaced by setting the **Buffer** property. Note that if you are updating the resource request with data that is obtained asynchronously, such as retrieving data from a remote server or asynchronous user authentication, you must call [**AdaptiveMediaSourceDownloadRequestedEventArgs.GetDeferral**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadrequestedeventargs.getdeferral) to get a deferral and then call [**Complete**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadrequesteddeferral.complete) when the operation is complete to signal the system that the download request operation can continue.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetAMSDownloadRequested":::

## Use bitrate events to manage and respond to bitrate changes

The **AdaptiveMediaSource** object provides events that allow you to react when the download or playback bitrates change. In this example, the current bitrates are simply updated in the UI. Note that you can modify the ratios that determine when the system switches bitrates of the adaptive stream. For more information, see the [**AdvancedSettings**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.advancedsettings) property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetAMSBitrateEvents":::

## Handle download completion and failure events
The **AdaptiveMediaSource** object raises the [**DownloadFailed**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource.DownloadFailed) event when the download of a requested resource fails. You can use this event to update your UI in response to the failure. You can also use the event to log statistical information about the download operation and the failure. 

The [**AdaptiveMediaSourceDownloadFailedEventArgs**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSourceDownloadFailedEventArgs) object passed into the event handler contains metadata about the failed resource download, such as the resource type, the URI of the resource, and the position within the stream where the failure occurred. The [**RequestId**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSourceDownloadFailedEventArgs.RequestId) gets a system-generated unique identifier for the request that can be use to correlate status information about an individual request across multiple events.

The [**Statistics**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSourceDownloadFailedEventArgs.Statistics) property returns a [**AdaptiveMediaSourceDownloadStatistics**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadstatistics) object which provides detailed information about the number of bytes received at the time of the event and the timing of various milestones in the download operation. You can log this information in order identify performance issues in your adaptive streaming implementation.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetAMSDownloadFailed":::


The  [**DownloadCompleted**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource.DownloadCompleted) event occurs when a resource download completes and provides similar data to the **DownloadFailed** event. Once again, a **RequestId** is provided for correlating events for a single request. Also, an **AdaptiveMediaSourceDownloadStatistics** object is provided to enable logging of download stats.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetAMSDownloadCompleted":::

## Gather adaptive streaming telemetry data with AdaptiveMediaSourceDiagnostics
The **AdaptiveMediaSource** exposes a [**Diagnostics**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource) property which returns an 
[**AdaptiveMediaSourceDiagnostics**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnostics) object. Use this object to register for the [**DiagnosticAvailable**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnostics.DiagnosticAvailable) event. This event is intended to be used for telemetry collection and should not be used to modify app behavior at runtime. This diagnostic event is raised for many different reasons. Check the [**DiagnosticType**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnosticavailableeventargs.DiagnosticType) property of the [**AdaptiveMediaSourceDiagnosticAvailableEventArgs**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnosticavailableeventargs) object passed into the event to determine the reason that the event was raised. Potential reasons include errors accessing the requested resource and errors parsing the streaming manifest file. For a list of situations that can trigger a diagnostic event, see [**AdaptiveMediaSourceDiagnosticType**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnostictype). Like the arguments for other adaptive streaming events, the **AdaptiveMediaSourceDiagnosticAvailableEventArgs** provides a **RequestId** property for correlating request information between different events.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AdaptiveStreaming_RS1/cs/MainPage.xaml.cs" id="SnippetAMSDiagnosticAvailable":::

## Defer binding of adaptive streaming content for items in a playback list by using MediaBinder
The [**MediaBinder**](/uwp/api/Windows.Media.Core.MediaBinder) class allows you to defer binding of media content in a [**MediaPlaybackList**](/uwp/api/Windows.Media.Playback.MediaPlaybackList). Starting with Windows 10, version 1703, you can supply an [**AdaptiveMediaSource**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) as bound content. The process for deferred binding of an adaptive media source is largely the same as binding other types of media, which is described in [Media items, playlists, and tracks](media-playback-with-mediasource.md). 

Create a **MediaBinder** instance, set an app-defined [**Token**](/uwp/api/Windows.Media.Core.MediaBinder.Token) string to identify the content to be bound, and register for the [**Binding**](/uwp/api/Windows.Media.Core.MediaBinder.Binding) event. Create a **MediaSource** from the **Binder** by calling [**MediaSource.CreateFromMediaBinder**](/uwp/api/windows.media.core.mediasource.createfrommediabinder). Then, create a **MediaPlaybackItem** from the **MediaSource** and add it to the playback list.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaSource_RS1/cs/MainPage.xaml.cs" id="SnippetInitMediaBinder":::

In the **Binding** event handler, use the token string to identify the content to be bound and then create the adaptive media source by calling one of the overloads of **[CreateFromStreamAsync](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.createfromstreamasync)** or **[CreateFromUriAsync](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.createfromuriasync)**. Because these are asynchronous methods, you should first call the [**MediaBindingEventArgs.GetDeferral**](/uwp/api/windows.media.core.mediabindingeventargs.GetDeferral) method to instruct the system to wait for your operation to complete before continuing.  Set the adaptive media source as the bound content by calling **[SetAdaptiveMediaSource](/uwp/api/windows.media.core.mediabindingeventargs.setadaptivemediasource)**. Finally, call [**Deferral.Complete**](/uwp/api/windows.foundation.deferral.Complete) after your operation is complete to instruct the system to continue.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaSource_RS1/cs/MainPage.xaml.cs" id="SnippetBinderBindingAMS":::

If you want to register event handlers for the bound adaptive media source, you can do this in the handler for the [**CurrentItemChanged**](/uwp/api/windows.media.playback.mediaplaybacklist.CurrentItemChanged) event of the **MediaPlaybackList**. The [**CurrentMediaPlaybackItemChangedEventArgs.NewItem**](/uwp/api/windows.media.playback.currentmediaplaybackitemchangedeventargs.NewItem) property contains the new currently playing **MediaPlaybackItem** in the list. Get an instance of the **AdaptiveMediaSource** representing the new item by accessing the [**Source**](/uwp/api/Windows.Media.Playback.MediaPlaybackItem.Source) property of the **MediaPlaybackItem** and then the [**AdaptiveMediaSource**](/uwp/api/windows.media.core.mediasource.AdaptiveMediaSource) property of the media source. This property will be null if the new playback item is not an **AdaptiveMediaSource**, so you should test for null before attempting to register handlers for any of the object's events.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaSource_RS1/cs/MainPage.xaml.cs" id="SnippetAMSBindingCurrentItemChanged":::

## Related topics
* [Media playback](media-playback.md)
* [HLS tag support](hls-tag-support.md) 
* [Dash profile support](dash-profile-support.md) 
* [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md)
* [Play media in the background](background-audio.md) 
