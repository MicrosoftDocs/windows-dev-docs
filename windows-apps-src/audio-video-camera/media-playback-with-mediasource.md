---
ms.assetid: C5623861-6280-4352-8F22-80EB009D662C
description: This article shows you how to use MediaSource, which provides a common way to reference and play back media from different sources such as local or remote files, and exposes a common model for accessing media data, regardless of the underlying media format.
title: Media items, playlists, and tracks
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Media items, playlists, and tracks


 This article shows you how to use the [**MediaSource**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaSource) class, which provides a common way to reference and play back media from different sources such as local or remote files and exposes a common model for accessing media data, regardless of the underlying media format. The [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackItem) class extends the functionality of **MediaSource**, allowing you to manage and select from multiple audio, video, and metadata tracks contained in a media item. [**MediaPlaybackList**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackList) allows you to create playback lists from one or more media playback items.


## Create and play a MediaSource

Create a new instance of **MediaSource** by calling one of the factory methods exposed by the class:

-   [**CreateFromAdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromadaptivemediasource)
-   [**CreateFromIMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromimediasource)
-   [**CreateFromMediaStreamSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfrommediastreamsource)
-   [**CreateFromMseStreamSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfrommsestreamsource)
-   [**CreateFromStorageFile**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromstoragefile)
-   [**CreateFromStream**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromstream)
-   [**CreateFromStreamReference**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromstreamreference)
-   [**CreateFromUri**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromuri)
-   [**CreateFromDownloadOperation**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromdownloadoperation)

After creating a **MediaSource** you can play it with a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlayer) by setting the [**Source**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer.source) property. Starting with Windows 10, version 1607, you can assign a **MediaPlayer** to a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) by calling [**SetMediaPlayer**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement.setmediaplayer) in order to render the media player content in a XAML page. This is the preferred method over using **MediaElement**. For more information on using **MediaPlayer**, see [**Play audio and video with MediaPlayer**](play-audio-and-video-with-mediaplayer.md).

The following example shows how to play back a user-selected media file in a **MediaPlayer** using **MediaSource**.

You will need to include the [**Windows.Media.Core**](https://docs.microsoft.com/uwp/api/Windows.Media.Core) and [**Windows.Media.Playback**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback) namespaces in order to complete this scenario.

[!code-cs[Using](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetUsing)]

Declare a variable of type **MediaSource**. For the examples in this article, the media source is declared as a class member so that it can be accessed from multiple locations.

[!code-cs[DeclareMediaSource](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetDeclareMediaSource)]

Declare a variable to store the **MediaPlayer** object and, if you want to render the media content in XAML, add a **MediaPlayerElement** control to your page.

[!code-cs[DeclareMediaPlayer](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetDeclareMediaPlayer)]

[!code-xml[MediaPlayerElement](./code/MediaSource_RS1/cs/MainPage.xaml#SnippetMediaPlayerElement)]

To allow the user to pick a media file to play, use a [**FileOpenPicker**](https://docs.microsoft.com/uwp/api/Windows.Storage.Pickers.FileOpenPicker). With the [**StorageFile**](https://docs.microsoft.com/uwp/api/Windows.Storage.StorageFile) object returned from the picker's [**PickSingleFileAsync**](https://docs.microsoft.com/uwp/api/windows.storage.pickers.fileopenpicker.picksinglefileasync) method, initialize a new MediaObject by calling [**MediaSource.CreateFromStorageFile**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromstoragefile). Finally, set the media source as the playback source for the **MediaElement** by calling the [**SetPlaybackSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaelement.setplaybacksource) method.

[!code-cs[PlayMediaSource](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetPlayMediaSource)]

By default, the **MediaPlayer** does not begin playing automatically when the media source is set. You can manually begin playback by calling [**Play**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer.play).

[!code-cs[Play](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetPlay)]

You can also set the [**AutoPlay**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer.autoplay) property of the **MediaPlayer** to true to tell the player to begin playing as soon as the media source is set.

[!code-cs[AutoPlay](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetAutoPlay)]

### Create a MediaSource from a DownloadOperation
Starting with Windows, version 1803, you can create a **MediaSource** object from a **DownloadOperation**.

[!code-cs[CreateMediaSourceFromDownload](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetCreateMediaSourceFromDownload)]

Note that while you can create a **MediaSource** from a download without starting it or setting its **IsRandomAccessRequired** property to true, you must do both of these things before attempting to attach the **MediaSource** to a **MediaPlayer** or **MediaPlayerElement** for playback.

[!code-cs[StartDownload](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetStartDownload)]


## Handle multiple audio, video, and metadata tracks with MediaPlaybackItem

Using a [**MediaSource**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaSource) for playback is convenient because it provides a common way to playback media from different kinds of sources, but more advanced behavior can be accessed by creating a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackItem) from the **MediaSource**. This includes the ability to access and manage multiple audio, video, and data tracks for a media item.

Declare a variable to store your **MediaPlaybackItem**.

[!code-cs[DeclareMediaPlaybackItem](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetDeclareMediaPlaybackItem)]

Create a **MediaPlaybackItem** by calling the constructor and passing in an initialized **MediaSource** object.

If your app supports multiple audio, video, or data tracks in a media playback item, register event handlers for the [**AudioTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.audiotrackschanged), [**VideoTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.videotrackschanged), or [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.timedmetadatatrackschanged) events.

Finally, set the playback source of the **MediaElement** or **MediaPlayer** to your **MediaPlaybackItem**.

[!code-cs[PlayMediaPlaybackItem](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetPlayMediaPlaybackItem)]

> [!NOTE] 
> A **MediaSource** can only be associated with a single **MediaPlaybackItem**. After creating a **MediaPlaybackItem** from a source, attempting to create another playback item from the same source will result in an error. Also, after creating a **MediaPlaybackItem** from a media source, you can't set the **MediaSource** object directly as the source for a **MediaPlayer** but should instead use the **MediaPlaybackItem**.

The [**VideoTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.videotrackschanged) event is raised after a **MediaPlaybackItem** containing multiple video tracks is assigned as a playback source, and can be raised again if the list of video tracks changes for the item changes. The handler for this event gives you the opportunity to update your UI to allow the user to switch between available tracks. This example uses a [**ComboBox**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ComboBox) to display the available video tracks.

[!code-xml[VideoComboBox](./code/MediaSource_RS1/cs/MainPage.xaml#SnippetVideoComboBox)]

In the **VideoTracksChanged** handler, loop through all of the tracks in the playback item's [**VideoTracks**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.videotracks) list. For each track, a new [**ComboBoxItem**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ComboBoxItem) is created. If the track does not already have a label, a label is generated from the track index. The [**Tag**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.frameworkelement.tag) property of the combo box item is set to the track index so that it can be identified later. Finally, the item is added to the combo box. Note that these operations are performed within a [**CoreDispatcher.RunAsync**](https://docs.microsoft.com/uwp/api/windows.ui.core.coredispatcher.runasync) call because all UI changes must be made on the UI thread and this event is raised on a different thread.

[!code-cs[VideoTracksChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetVideoTracksChanged)]

In the [**SelectionChanged**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.selector.selectionchanged) handler for the combo box, the track index is retrieved from the selected item's **Tag** property. Setting the [**SelectedIndex**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackvideotracklist.selectedindex) property of the media playback item's [**VideoTracks**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.videotracks) list causes the **MediaElement** or **MediaPlayer** to switch the active video track to the specified index.

[!code-cs[VideoTracksSelectionChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetVideoTracksSelectionChanged)]

Managing media items with multiple audio tracks works exactly the same as with video tracks. Handle the [**AudioTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.audiotrackschanged) to update your UI with the audio tracks found in the playback item's [**AudioTracks**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.audiotracks) list. When the user selects an audio track, set the [**SelectedIndex**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackaudiotracklist.selectedindex) property of the **AudioTracks** list to cause the **MediaElement** or **MediaPlayer** to switch the active audio track to the specified index.

[!code-xml[AudioComboBox](./code/MediaSource_RS1/cs/MainPage.xaml#SnippetAudioComboBox)]

[!code-cs[AudioTracksChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetAudioTracksChanged)]

[!code-cs[AudioTracksSelectionChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetAudioTracksSelectionChanged)]

In addition to audio and video, a **MediaPlaybackItem** object may contain zero or more [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedMetadataTrack) objects. A timed metadata track can contain subtitle or caption text, or it may contain custom data that is proprietary to your app. A timed metadata track contains a list of cues represented by objects that inherit from [**IMediaCue**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.IMediaCue), such as a [**DataCue**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.DataCue) or a [**TimedTextCue**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedTextCue). Each cue has a start time and a duration that determines when the cue is activated and for how long.

Similar to audio tracks and video tracks, the timed metadata tracks for a media item can be discovered by handling the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.timedmetadatatrackschanged) event of a **MediaPlaybackItem**. With timed metadata tracks, however, the user may want to enable more than one metadata track at a time. Also, depending on your app scenario, you may want to enable or disable metadata tracks automatically, without user intervention. For illustration purposes, this example adds a [**ToggleButton**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton) for each metadata track in a media item to allow the user to enable and disable the track. The **Tag** property of each button is set to the index of the associated metadata track so that it can be identified when the button is toggled.

[!code-xml[MetaStackPanel](./code/MediaSource_RS1/cs/MainPage.xaml#SnippetMetaStackPanel)]

[!code-cs[TimedMetadataTrackschanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetTimedMetadataTrackschanged)]

Because more than one metadata track can be active at a time, you don't simply set the active index for the metadata track list. Instead, call the **MediaPlaybackItem** object's [**SetPresentationMode**](https://docs.microsoft.com/previous-versions/windows/dn986977(v=win.10)) method, passing in the index of the track you want to toggle, and then providing a value from the [**TimedMetadataTrackPresentationMode**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.TimedMetadataTrackPresentationMode) enumeration. The presentation mode you choose depends on the implementation of your app. In this example, the metadata track is set to **PlatformPresented** when enabled. For text-based tracks, this means that the system will automatically display the text cues in the track. When the toggle button is toggled off, the presentation mode is set to **Disabled**, which means that no text is displayed and no cue events are raised. Cue events are discussed later in this article.

[!code-cs[ToggleChecked](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetToggleChecked)]

[!code-cs[ToggleUnchecked](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetToggleUnchecked)]

As you are processing the metadata tracks, you can access the set of cues within the track by accessing the [**Cues**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.cues) or [**ActiveCues**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.activecues) properties. You can do this to update your UI to show the cue locations for a media item.

## Handle unsupported codecs and unknown errors when opening media items
Starting with Windows 10, version 1607, you can check whether the codec required to playback a media item is supported or partially supported on the device on which your app is running. In the event handler for the **MediaPlaybackItem** tracks-changed events, such as [**AudioTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.audiotrackschanged), first check to see if the track change is an insertion of a new track. If so, you can get a reference to the track being inserted by using the index passed in the **IVectorChangedEventArgs.Index** parameter with the appropriate track collection of the **MediaPlaybackItem** parameter, such as the [**AudioTracks**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.audiotracks) collection.

Once you have a reference to the inserted track, check the [**DecoderStatus**](https://docs.microsoft.com/uwp/api/windows.media.core.audiotracksupportinfo.decoderstatus) of the track's [**SupportInfo**](https://docs.microsoft.com/uwp/api/windows.media.core.audiotrack.supportinfo) property. If the value is [**FullySupported**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaDecoderStatus), then the appropriate codec needed to play back the track is present on the device. If the value is [**Degraded**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaDecoderStatus), then the track can be played by the system, but the playback will be degraded in some way. For example, a 5.1 audio track may be played back as 2-channel stereo instead. If this is the case, you may want to update your UI to alert the user of the degradation. If the value is [**UnsupportedSubtype**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaDecoderStatus) or [**UnsupportedEncoderProperties**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaDecoderStatus), then the track can't be played back at all with the current codecs on the device. You may wish to alert the user and skip playback of the item or implement UI to allow the user to download the correct codec. The track's [**GetEncodingProperties**](https://docs.microsoft.com/uwp/api/windows.media.core.audiotrack.getencodingproperties) method can be used to determine the required codec for playback.

Finally, you can register for the track's [**OpenFailed**](https://docs.microsoft.com/uwp/api/windows.media.core.audiotrack.openfailed) event, which will be raised if the track is supported on the device but failed to open due to an unknown error in the pipeline.

[!code-cs[AudioTracksChanged_CodecCheck](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetAudioTracksChanged_CodecCheck)]

In the [**OpenFailed**](https://docs.microsoft.com/uwp/api/windows.media.core.audiotrack.openfailed) event handler, you can check to see if the **MediaSource** status is unknown, and if so, you can programatically select a different track to play, allow the user to choose a different track, or abandon playback.

[!code-cs[OpenFailed](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetOpenFailed)]

## Set display properties used by the System Media Transport Controls
Starting with Windows 10, version 1607, media played in a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlayer) is automatically integrated into the System Media Transport Controls (SMTC) by default. You can specify the metadata that will be displayed by the SMTC by updating the display properties for a **MediaPlaybackItem**. Get an object representing the display properties for an item by calling [**GetDisplayProperties**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.getdisplayproperties). Set whether the playback item is music or video by setting the [**Type**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaitemdisplayproperties.type) property. Then, set the properties of the object's [**VideoProperties**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaitemdisplayproperties.videoproperties) or [**MusicProperties**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaitemdisplayproperties.musicproperties). Call [**ApplyDisplayProperties**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.applydisplayproperties) to update the item's properties to the values you provided. Typically, an app will retrieve the display values dynamically from a web service, but the following example illustrates this process with hardcoded values.

[!code-cs[SetVideoProperties](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetSetVideoProperties)]

[!code-cs[SetMusicProperties](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetSetMusicProperties)]

## Add external timed text with TimedTextSource

For some scenarios, you may have external files that contains timed text associated with a media item, such as separate files that contain subtitles for different locales. Use the [**TimedTextSource**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedTextSource) class to load in external timed text files from a stream or URI.

This example uses a **Dictionary** collection to store a list of the timed text sources for the media item using the source URI and the **TimedTextSource** object as the key/value pair in order to identify the tracks after they have been resolved.

[!code-cs[TimedTextSourceMap](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetTimedTextSourceMap)]

Create a new **TimedTextSource** for each external timed text file by calling [**CreateFromUri**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromuri). Add an entry to the **Dictionary** for the timed text source. Add a handler for the [**TimedTextSource.Resolved**](https://docs.microsoft.com/uwp/api/windows.media.core.timedtextsource.resolved) event to handle if the item failed to load or to set additional properties after the item was loaded successfully.

Register all of your **TimedTextSource** objects with the **MediaSource** by adding them to the [**ExternalTimedTextSources**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.externaltimedtextsources) collection. Note that external timed text sources are added to directly the **MediaSource** and not the **MediaPlaybackItem** created from the source. To update your UI to reflect the external text tracks, register and handle the **TimedMetadataTracksChanged** event as described previously in this article.

[!code-cs[TimedTextSource](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetTimedTextSource)]

In the handler for the [**TimedTextSource.Resolved**](https://docs.microsoft.com/uwp/api/windows.media.core.timedtextsource.resolved) event, check the **Error** property of the [**TimedTextSourceResolveResultEventArgs**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedTextSourceResolveResultEventArgs) passed into the handler to determine if an error occurred while trying to load the timed text data. If the item was resolved successfully, you can use this handler to update additional properties of the resolved track. This example adds a label for each track based on the URI previously stored in the **Dictionary**.

[!code-cs[TimedTextSourceResolved](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetTimedTextSourceResolved)]

## Add additional metadata tracks

You can dynamically create custom metadata tracks in code and associate them with a media source. The tracks you create can contain subtitle or caption text, or they can contain your proprietary app data.

Create a new [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedMetadataTrack) by calling the constructor and specifying an ID, the language identifier, and a value from the [**TimedMetadataKind**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedMetadataKind) enumeration. Register handlers for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.cueentered) and [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.cueexited) events. These events are raised when the start time for a cue has been reached and when the duration for a cue has expired, respectively.

Create a new cue object, appropriate for the type of metadata track you created, and set the ID, start time, and duration for the track. This example creates a data track, so a set of [**DataCue**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.DataCue) objects are generated and a buffer containing app-specific data is provided for each cue. To register the new track, add it to the [**ExternalTimedMetadataTracks**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.externaltimedmetadatatracks) collection of the **MediaSource** object.

Starting with Windows 10, version 1703, the **DataCue.Properties** property exposes a [**PropertySet**](https://docs.microsoft.com/uwp/api/windows.foundation.collections.propertyset) that you can use to store custom properties in key/data pairs that can be retrieved in the **CueEntered** and **CueExited** events.  

[!code-cs[AddDataTrack](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetAddDataTrack)]

The **CueEntered** event is raised when a cue's start time has been reached as long as the associated track has a presentation mode of **ApplicationPresented**, **Hidden**, or **PlatformPresented.** Cue events are not raised for metadata tracks while the presentation mode for the track is **Disabled**. This example simply outputs the custom data associated with the cue to the debug window.

[!code-cs[DataCueEntered](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetDataCueEntered)]

This example adds a custom text track by specifying **TimedMetadataKind.Caption** when creating the track and using [**TimedTextCue**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.TimedTextCue) objects to add cues to the track.

[!code-cs[AddTextTrack](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetAddTextTrack)]

[!code-cs[TextCueEntered](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetTextCueEntered)]

## Play a list of media items with MediaPlaybackList

The [**MediaPlaybackList**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackList) allows you to create a playlist of media items, which are represented by **MediaPlaybackItem** objects.

**Note**  Items in a [**MediaPlaybackList**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackList) are rendered using gapless playback. The system will use provided metadata in MP3 or AAC encoded files to determine the delay or padding compensation needed for gapless playback. If the MP3 or AAC encoded files don't provide this metadata, then the system determines the delay or padding heuristically. For lossless formats, such as PCM, FLAC, or ALAC, the system takes no action because these encoders don't introduce delay or padding.

To get started, declare a variable to store your **MediaPlaybackList**.

[!code-cs[DeclareMediaPlaybackList](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetDeclareMediaPlaybackList)]

Create a **MediaPlaybackItem** for each media item you want to add to your list using the same procedure described previously in this article. Initialize your **MediaPlaybackList** object and add the media playback items to it. Register a handler for the [**CurrentItemChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacklist.currentitemchanged) event. This event allows you to update your UI to reflect the currently playing media item. You can also register for the [ItemOpened](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackList.ItemOpened) event, which is raised when an item in the list is successfully opened, and the [ItemFailed](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackList.ItemFailed) event, which is raised when an item in the list can't be opened.

Starting with Windows 10, version 1703, you can specify the maximum number of **MediaPlaybackItem** objects in the **MediaPlaybackList** that the system will keep open after they have been played by setting the [MaxPlayedItemsToKeepOpen](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackList.MaxPlayedItemsToKeepOpen) property. When a **MediaPlaybackItem** is kept open, playback of the item can start instantaneously when the user switches to that item because the item doesn't need to be reloaded. But keeping items open also increases the memory consumption of your app, so you should consider the balance between responsiveness and memory usage when setting this value. 

To enable playback of your list, set the playback source of the **MediaPlayer** to your **MediaPlaybackList**.

[!code-cs[PlayMediaPlaybackList](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetPlayMediaPlaybackList)]

In the **CurrentItemChanged** event handler, update your UI to reflect the currently playing item, which can be retrieved using the [**NewItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.currentmediaplaybackitemchangedeventargs.newitem) property of the [**CurrentMediaPlaybackItemChangedEventArgs**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.CurrentMediaPlaybackItemChangedEventArgs) object passed into the event. Remember that if you update the UI from this event, you should do so within a call to [**CoreDispatcher.RunAsync**](https://docs.microsoft.com/uwp/api/windows.ui.core.coredispatcher.runasync) so that the updates are made on the UI thread.

Starting with Windows 10, version 1703, you can check the [CurrentMediaPlaybackItemChangedEventArgs.Reason](https://docs.microsoft.com/uwp/api/windows.media.playback.currentmediaplaybackitemchangedeventargs.Reason) property to get a value that indicates the reason that the item changed, such as the app switching items programatically, the previously playing item reaching its end, or an error occurring.

[!code-cs[MediaPlaybackListItemChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetMediaPlaybackListItemChanged)]


Call [**MovePrevious**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacklist.moveprevious) or [**MoveNext**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacklist.movenext) to cause the media player to play the previous or next item in your **MediaPlaybackList**.

[!code-cs[PrevButton](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetPrevButton)]

[!code-cs[NextButton](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetNextButton)]

Set the [**ShuffleEnabled**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacklist.shuffleenabled) property to specify whether the media player should play the items in your list in random order.

[!code-cs[ShuffleButton](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetShuffleButton)]

Set the [**AutoRepeatEnabled**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacklist.autorepeatenabled) property to specify whether the media player should loop playback of your list.

[!code-cs[RepeatButton](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetRepeatButton)]


### Handle the failure of media items in a playback list
The [**ItemFailed**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacklist.itemfailed) event is raised when an item in the list fails to open. The [**ErrorCode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitemerror.errorcode) property of the [**MediaPlaybackItemError**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackItemError) object passed into the handler enumerates the specific cause of the failure when possible, including network errors, decoding errors, or encryption errors.

[!code-cs[ItemFailed](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetItemFailed)]

### Disable playback of items in a playback list
Starting with Windows 10, version 1703, you can disable playback of one or more items in a **MediaPlaybackItemList** by setting the [IsDisabledInPlaybackList](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackItem.IsDisabledInPlaybackList) property of a [MediaPlaybackItem](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.MediaPlaybackItem) to false. 

A typical scenario for this feature is for apps that play music streamed from the internet. The app can listen for changes in the network connection status of the device and disable playback of items that are not fully downloaded. In the following example, a handler is registered for the [NetworkInformation.NetworkStatusChanged](https://docs.microsoft.com/uwp/api/Windows.Networking.Connectivity.NetworkInformation.NetworkStatusChanged) event.

[!code-cs[RegisterNetworkStatusChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetRegisterNetworkStatusChanged)]

In the handler for **NetworkStatusChanged**, check to see if [GetInternetConnectionProfile](https://docs.microsoft.com/uwp/api/Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile) returns null, which indicates that the network is not connected. If this is the case, loop through all of the items in the playback list, and if the [TotalDownloadProgress](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TotalDownloadProgress) for the item is less than 1, meaning that the item has not fully downloaded, disable the item. If the network connection is enabled, loop through all of the items in the playback list and enable each item.

[!code-cs[NetworkStatusChanged](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetNetworkStatusChanged)]

### Defer binding of media content for items in a playback list by using MediaBinder
In the previous examples, a **MediaSource** is created from a file, URL, or stream, after which a **MediaPlaybackItem** is created and added to a **MediaPlaybackList**. For some scenarios, such as if the user is being charged for viewing content, you may want to defer the retrieval of the content of a **MediaSource** until the item in the playback list is ready to actually be played. To implement this scenario, create an instance of the [**MediaBinder**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaBinder) class. Set the [**Token**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaBinder.Token) property to an app-defined string that identifies the content for which you want to defer retrieval and then register a handler for the [**Binding**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaBinder.Binding) event. Next, create a **MediaSource** from the **Binder** by calling [**MediaSource.CreateFromMediaBinder**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfrommediabinder). Then, create a **MediaPlaybackItem** from the **MediaSource** and add it to the playback list as usual.

[!code-cs[InitMediaBinder](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetInitMediaBinder)]

When the system determines that the content associated with the **MediaBinder** needs to be retrieved, it will raise the **Binding** event. In the handler for this event, you can retrieve the **MediaBinder** instance from the [**MediaBindingEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs) passed into the event. Retrieve the string you specified for the **Token** property and use it to determine what content should be retrieved. The **MediaBindingEventArgs** provides methods for setting the bound content in several different representations, including [**SetStorageFile**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs.setstoragefile), [**SetStream**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs.setstream), [**SetStreamReference**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs.setstreamreference), and [**SetUri**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs.seturi). 

[!code-cs[BinderBinding](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetBinderBinding)]

Note that if you are performing asynchronous operations, such as web requests, in the **Binding** event handler, you should call the [**MediaBindingEventArgs.GetDeferral**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs.GetDeferral) method to instruct the system to wait for your operation to complete before continuing. Call [**Deferral.Complete**](https://docs.microsoft.com/uwp/api/windows.foundation.deferral.Complete) after your operation is complete to instruct the system to continue.

Starting with Windows 10, version 1703, you can supply an [**AdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) as bound content by calling [**SetAdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs.setadaptivemediasource). For more information on using adaptive streaming in your app, see [Adaptive streaming](adaptive-streaming.md).



## Related topics
* [Media playback](media-playback.md)
* [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md)
* [Integrate with the Sytem Media Transport Controls](integrate-with-systemmediatransportcontrols.md)
* [Play media in the background](background-audio.md)

