---
author: drewbatgit
ms.assetid: F28162D4-AACC-4EE0-B243-5878F870F87F
description: Handle system-supported metadata cues during media playback
title: System-supported timed metadata cues
ms.author: drewbat
ms.date: 04/18/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, metadata, cue, speech, chapter
ms.localizationpriority: medium
---

# System-supported timed metadata cues
This article describes how to take advantage of several formats of timed metadata that may be embedded in media files or streams. UWP apps can register for events that are raised by the media pipeline during playback whenever these metadata cues are encountered. Using the [**DataCue**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.DataCue) class, apps can implement their own custom metadata cues, but this article focuses on several metadata standards that are automatically detected by the media pipeline, including:

* Image-based subtitles in VobSub format
* Speech cues, including word boundaries, sentence boundaries, and Speech Synthesis Markup Language (SSML) bookmarks
* Chapter cues
* Extended M3U comments
* ID3 tags
* Fragmented mp4 emsg boxes


This article builds on the concepts discussed  in the article [Media items, playlists, and tracks](media-playback-with-mediasource.md), which includes the basics of working with the [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource), [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem), and [**TimedMetadataTrack**](https://msdn.microsoft.com/library/windows/apps/dn956580) classes and general guidance for using timed metadata in your app.

The basic implementation steps are the same for all of the different types of timed metadata described in this article:

1. Create a [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) and then a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) for the content to be played.
2. Register for the [**MediaPlaybackItem.TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs as the sub-tracks of the media item are resolved by the media pipeline.
3. Register for the [**TimedMetadataTrack.CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) and [**TimedMetadataTrack.CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) events for the timed metadata tracks you want to use.
4. In the **CueEntered** event handler, update your UI based on the metadata passed in the event args. You can update the UI again, to remove the current subtitle text for example, in the **CueExited** event.

In this article, handling each type of metadata is shown as a distinct scenario, but it's possible to handle (or ignore) different types of metadata using mostly shared code. You can check the [**TimedMetadataKind**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) property of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) object at multiple points in the process. So, for example, you could choose to register for the **CueEntered** event for metadata tracks that have the value **TimedMetadataKind.ImageSubtitle**, but not for tracks that have the value **TimedMetadataKind.Speech**. Or instead, you could register a handler for all metadata track types and then check the **TimedMetadataKind** value inside the **CueEntered** handler to determine what action to take in response to the cue.

## Image-based subtitles
Starting with Windows 10, version 1703, UWP apps can support external image-based subtitles in VobSub format. To use this feature, first create a [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) object for the media content for which image subtitles will be displayed. Next, create a [**TimedTextSource**](https://docs.microsoft.com/uwp/api/windows.media.core.timedtextsource) object by calling [**CreateFromUriWithIndex**](https://docs.microsoft.com/uwp/api/windows.media.core.timedtextsource.CreateFromUriWithIndex) or [**CreateFromStreamWithIndex**](https://docs.microsoft.com/uwp/api/windows.media.core.timedtextsource.CreateFromStreamWithIndex), passing in the Uri of the .sub file containing the subtitle image data and the .idx file containing the timing information for the subtitles. Add the **TimedTextSource** to the **MediaSource** by adding it to the source's [**ExternalTimedTextSources**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.ExternalTimedTextSources) collection. Create a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

[!code-cs[ImageSubtitleLoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetImageSubtitleLoadContent)]

Register for the image subtitle metadata events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForImageSubtitles**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForImageSubtitles**.

[!code-cs[ImageSubtitleTracksChanged](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetImageSubtitleTracksChanged)]

After registering for the image subtitle metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement).

[!code-cs[ImageSubtitlePlay](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetImageSubtitlePlay)]

In the **RegisterMetadataHandlerForImageSubtitles** helper method, get an instance of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Register for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

[!code-cs[RegisterMetadataHandlerForImageSubtitles](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetRegisterMetadataHandlerForImageSubtitles)]

In the handler for the **CueEntered** event, you can check the [**TimedMetadataKind**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) propery of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) object passed into the handler to see if the metadata is for image subtitles. This is necessary if you are using the same data cue event handler for multiple types of metadata. If the associated metadata track is of type **TimedMetadataKind.ImageSubtitle**, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediacueeventargs) to an [**ImageCue**](https://docs.microsoft.com/uwp/api/windows.media.core.imagecue). The [**SoftwareBitmap**](https://docs.microsoft.com/uwp/api/windows.media.core.imagecue.SoftwareBitmap) property of the **ImageCue** contains a [**SoftwareBitmap**](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.softwarebitmap) representation of the subtitle image. Create a [**SoftwareBitmapSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource) and call [**SetBitmapAsync**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource.SetBitmapAsync) to assign the image to a XAML [**Image**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.image) control. The [**Extent**](https://docs.microsoft.com/uwp/api/windows.media.core.imagecue.Extent) and [**Position**](https://docs.microsoft.com/uwp/api/windows.media.core.imagecue.Position) properties of the **ImageCue** provide information about the size and position of the subtitle image.

[!code-cs[ImageSubtitleCueEntered](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetImageSubtitleCueEntered)]

## Speech cues
Starting with Windows 10, version 1703, UWP apps can register to receive events in response to word boundaries, sentence boundaries, and Speech Synthesis Markup Language (SSML) bookmarks in played media. This allows you to play audio streams generated with the [**SpeechSynthesizer**](https://docs.microsoft.com/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer) class and update your UI based on these events, such as displaying the text of the currently playing word or sentence.

The example shown in this section uses a class member variable to store a text string that will be synthesized and played back.

[!code-cs[SpeechInputText](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetSpeechInputText)]

Create a new instance of the **SpeechSynthesizer** class. Set the [**IncludeWordBoundaryMetadata**](https://docs.microsoft.com/uwp/api/windows.media.speechsynthesis.speechsynthesizeroptions.IncludeWordBoundaryMetadata) and [**IncludeSentenceBoundaryMetadata**](https://docs.microsoft.com/uwp/api/windows.media.speechsynthesis.speechsynthesizeroptions.IncludeSentenceBoundaryMetadata) options for the synthesizer to **true** to specify that the metadata should be included in the generated media stream. Call [**SynthesizeTextToStreamAsync**](https://docs.microsoft.com/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer.SynthesizeTextToStreamAsync) to generate a stream containing the synthesized speech and corresponding metadata. Create a [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) and a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) from the synthesized stream.

[!code-cs[SynthesizeSpeech](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetSynthesizeSpeech)]

Register for the speech metadata events using the **MediaPlaybackItem** object. This example uses a helper method, **RegisterMetadataHandlerForSpeech**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**.  In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForSpeech**.

[!code-cs[SpeechTracksChanged](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetSpeechTracksChanged)]

After registering for the speech metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement).

[!code-cs[SpeechPlay](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetSpeechPlay)]

In the **RegisterMetadataHandlerForSpeech** helper method, get an instance of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Register for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

[!code-cs[RegisterMetadataHandlerForWords](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetRegisterMetadataHandlerForWords)]

In the handler for the **CueEntered** event, you can check the [**TimedMetadataKind**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) propery of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) object passed into the handler to see if the metadata is speech. This is necessary if you are using the same data cue event handler for multiple types of metadata. If the associated metadata track is of type **TimedMetadataKind.Speech**, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediacueeventargs) to a [**SpeechCue**](https://docs.microsoft.com/uwp/api/windows.media.core.speechcue). For speech cues, the type of speech cue included in the metadata track is determined by checking the **Label** property. The value of this property will be "SpeechWord" for word boundaries, "SpeechSentence" for sentence boundaries, or "SpeechBookmark" for SSML bookmarks. In this example, we check for the "SpeechWord" value, and if this value is found, the [**StartPositionInInput**](https://docs.microsoft.com/uwp/api/windows.media.core.speechcue.StartPositionInInput) and [**EndPositionInInput**](https://docs.microsoft.com/uwp/api/windows.media.core.speechcue.EndPositionInInput) properties of the **SpeechCue** are used to determine location within the input text of the word currently being played back. This example simply outputs each word to the debug output.

[!code-cs[SpeechWordCueEntered](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetSpeechWordCueEntered)]

## Chapter cues
Starting with Windows 10, version 1703, UWP apps can register for cues that correspond to chapters within a media item. To use this feature, create a [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) object for the media content and then create a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

[!code-cs[ChapterCueLoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetChapterCueLoadContent)]

Register for the chapter metadata events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForChapterCues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForChapterCues**.

[!code-cs[ChapterCueTracksChanged](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetChapterCueTracksChanged)]

After registering for the chapter metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement).

[!code-cs[ChapterCuePlay](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetChapterCuePlay)]

In the **RegisterMetadataHandlerForChapterCues** helper method, get an instance of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Register for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

[!code-cs[RegisterMetadataHandlerForChapterCues](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetRegisterMetadataHandlerForChapterCues)]

In the handler for the **CueEntered** event, you can check the [**TimedMetadataKind**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) propery of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) object passed into the handler to see if the metadata is for chapter cues. This is necessary if you are using the same data cue event handler for multiple types of metadata. If the associated metadata track is of type **TimedMetadataKind.Chapter**, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediacueeventargs) to an [**ChapterCue**](https://docs.microsoft.com/uwp/api/windows.media.core.chaptercue). The [**Title**](https://docs.microsoft.com/uwp/api/windows.media.core.chaptercue.Title) property of the **ChapterCue** contains the title of the chapter that has just been reached in playback.

[!code-cs[ChapterCueEntered](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetChapterCueEntered)]

### Seek to the next chapter using chapter cues
In addition to receiving notifications when the current chapter changes in a playing item, you can also use chapter cues to seek to the next chapter within a playing item. The example method shown below takes as arguments a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) and a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) representing the currently playing media item. The [**TimedMetadataTracks**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracks) collection is searched to see if any of the tracks have [**TimedMetadataKind**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) propery of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) value of **TimedMetadataKind.Chapter**.  If a chapter track is found, the method loops through each cue in the track's [**Cues**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.Cues) collection to find the first cue that has a [**StartTime**](https://docs.microsoft.com/uwp/api/windows.media.core.chaptercue.StartTime) greater than the current [**Position**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacksession.Position) of the media player's playback session. Once the correct cue is found, the position of the playback session is updated and the chapter title is updated in the UI.

[!code-cs[GoToNextChapter](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetGoToNextChapter)]

## Extended M3U comments
Starting with Windows 10, version 1703, UWP apps can register for cues that correspond to comments within a Extended M3U manifest file. This example uses [**AdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) to play the media content. For more information, see [Adaptive Streaming](adaptive-streaming.md). Create an **AdaptiveMediaSource** for the content by calling [**CreateFromUriAsync**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromUriAsync) or [**CreateFromStreamAsync**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromStreamAsync). Create a  [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) object by calling [**CreateFromAdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.CreateFromAdaptiveMediaSource) and then create a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

[!code-cs[EXTM3ULoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEXTM3ULoadContent)]

Register for the M3U metadata events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForEXTM3UCues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForEXTM3UCues**.

[!code-cs[EXTM3UCueTracksChanged](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEXTM3UCueTracksChanged)]

After registering for the M3U metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement).

[!code-cs[EXTM3UCuePlay](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEXTM3UCuePlay)]

In the **RegisterMetadataHandlerForEXTM3UCues** helper method, get an instance of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Check the DispatchType property of the metadata track, which will have a value of "EXTM3U" if the track represents M3U comments. Register for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

[!code-cs[RegisterMetadataHandlerForEXTM3UCues](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetRegisterMetadataHandlerForEXTM3UCues)]

In the handler for the **CueEntered** event, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediacueeventargs) to an [**DataCue**](https://docs.microsoft.com/uwp/api/windows.media.core.datacue).  Check to make sure the **DataCue** and the [**Data**](https://docs.microsoft.com/uwp/api/windows.media.core.datacue.Data) property of the cue are not null. Extended EMU comments are provided in the form of UTF-16, little endian, null terminated strings. Create a new **DataReader** to read the cue data by calling [**DataReader.FromBuffer**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.FromBuffer). Set the [**UnicodeEncoding**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.UnicodeEncoding) property of the reader to [**Utf16LE**](https://docs.microsoft.com/uwp/api/windows.storage.streams.unicodeencoding) to read the data in the correct format. Call [**ReadString**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.ReadString) to read the data, specifying half of the length of the **Data** field, because each character is two bytes in size, and subtract one to remove the trailing null character. In this example, the M3U comment is simply written to the debug output.

[!code-cs[EXTM3UCueEntered](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEXTM3UCueEntered)]

## ID3 tags
Starting with Windows 10, version 1703, UWP apps can register for cues that correspond to ID3 tags within Http Live Streaming (HLS) content. This example uses [**AdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) to play the media content. For more information, see [Adaptive Streaming](adaptive-streaming.md). Create an **AdaptiveMediaSource** for the content by calling [**CreateFromUriAsync**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromUriAsync) or [**CreateFromStreamAsync**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromStreamAsync). Create a  [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) object by calling [**CreateFromAdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.CreateFromAdaptiveMediaSource) and then create a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

[!code-cs[EXTM3ULoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEXTM3ULoadContent)]

Register for the ID3 tag events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForID3Cues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForID3Cues**.

[!code-cs[ID3LoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetID3LoadContent)]

After registering for the ID3 metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement).

[!code-cs[ID3CuePlay](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetID3CuePlay)]


In the **RegisterMetadataHandlerForID3Cues** helper method, get an instance of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Check the DispatchType property of the metadata track, which will have a value containing the GUID string "15260DFFFF49443320FF49443320000F" if the track represents ID3 tags. Register for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

[!code-cs[RegisterMetadataHandlerForID3Cues](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetRegisterMetadataHandlerForID3Cues)]

In the handler for the **CueEntered** event, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediacueeventargs) to an [**DataCue**](https://docs.microsoft.com/uwp/api/windows.media.core.datacue).  Check to make sure the **DataCue** and the [**Data**](https://docs.microsoft.com/uwp/api/windows.media.core.datacue.Data) property of the cue are not null. Extended EMU comments are provided in the form raw bytes in the transport stream (see [http://id3.org/id3v2.4.0-structure](http://id3.org/id3v2.4.0-structure)). Create a new **DataReader** to read the cue data by calling [**DataReader.FromBuffer**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.FromBuffer).  In this example, the header values from the ID3 tag are read from the cue data and written to the debug output.

[!code-cs[ID3CueEntered](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetID3CueEntered)]

## Fragmented mp4 emsg boxes
Starting with Windows 10, version 1703, UWP apps can register for cues that correspond to emsg boxes within fragmented mp4 streams. An example usage of this type of metadata is for content providers to signal client applications to play an ad during live streaming content. This example uses [**AdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) to play the media content. For more information, see [Adaptive Streaming](adaptive-streaming.md). Create an **AdaptiveMediaSource** for the content by calling [**CreateFromUriAsync**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromUriAsync) or [**CreateFromStreamAsync**](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromStreamAsync). Create a  [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) object by calling [**CreateFromAdaptiveMediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.CreateFromAdaptiveMediaSource) and then create a [**MediaPlaybackItem**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

[!code-cs[EmsgLoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEmsgLoadContent)]

Register for the emsg box events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForEmsgCues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForEmsgCues**.

[!code-cs[ID3LoadContent](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetID3LoadContent)]

After registering for the emsg box metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement).

[!code-cs[EmsgCuePlay](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEmsgCuePlay)]


In the **RegisterMetadataHandlerForEmsgCues** helper method, get an instance of the [**TimedMetadataTrack**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Check the DispatchType property of the metadata track, which will have a value of "emsg:mp4" if the track represents emsg boxes. Register for the [**CueEntered**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.


[!code-cs[RegisterMetadataHandlerForEmsgCues](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetRegisterMetadataHandlerForEmsgCues)]


In the handler for the **CueEntered** event, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](https://docs.microsoft.com/uwp/api/windows.media.core.mediacueeventargs) to an [**DataCue**](https://docs.microsoft.com/uwp/api/windows.media.core.datacue).  Check to make sure the **DataCue** object is not null. The properies of the emsg box are provided by the media pipeline as custom properties in the DataCue object's [**Properties**](https://docs.microsoft.com/uwp/api/windows.media.core.datacue.Properties) collection. This example attempts to extract several different property values using the **[TryGetValue](https://docs.microsoft.com/uwp/api/windows.foundation.collections.propertyset.trygetvalue)** method. If this method returns null, it means the requested propery is not present in the emsg box, so a default value is set instead.

The next part of the example illustrates the scenario where ad playback is triggered, which is the case when the *scheme_id_uri* property, obtained in the previous step, has a value of "urn:scte:scte35:2013:xml" (see [http://dashif.org/identifiers/event-schemes/](http://dashif.org/identifiers/event-schemes/)). Note that the standard recommends sending this emsg multiple times for redundancy, so this example maintains a list of the emsg IDs that have already been processed and only processes new messages. Create a new **DataReader** to read the cue data by calling [**DataReader.FromBuffer**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.FromBuffer) and set the encoding to UTF-8 by setting the [**UnicodeEncoding**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.UnicodeEncoding) property, then read the data. In this example, the message payload is written to the debug output. A real app would use the payload data to schedule the playback of an ad.

[!code-cs[EmsgCueEntered](./code/MediaSource_RS1/cs/MainPage_Cues.xaml.cs#SnippetEmsgCueEntered)]


## Related topics

* [Media playback](media-playback.md)
* [Media items, playlists, and tracks](media-playback-with-mediasource.md)


 




