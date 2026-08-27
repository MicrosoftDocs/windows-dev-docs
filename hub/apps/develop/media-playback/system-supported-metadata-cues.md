---
description: Learn how to take advantage of several formats of timed metadata that may be embedded in media files or streams.
title: System-supported timed metadata cues
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, winui, metadata, cue, speech, chapter
ms.localizationpriority: medium
---

# System-supported timed metadata cues

This article describes how to take advantage of several formats of timed metadata that may be embedded in media files or streams. WinUI apps can register for events that are raised by the media pipeline during playback whenever these metadata cues are encountered. Using the [**DataCue**](/uwp/api/Windows.Media.Core.DataCue) class, apps can implement their own custom metadata cues, but this article focuses on several metadata standards that are automatically detected by the media pipeline, including:

* Image-based subtitles in VobSub format
* Speech cues, including word boundaries, sentence boundaries, and Speech Synthesis Markup Language (SSML) bookmarks
* Chapter cues
* Extended M3U comments
* ID3 tags
* Fragmented mp4 emsg boxes


This article builds on the concepts discussed  in the article [Media items, playlists, and tracks](media-playback-with-mediasource.md), which includes the basics of working with the [**MediaSource**](/uwp/api/windows.media.core.mediasource), [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem), and [**TimedMetadataTrack**](/uwp/api/Windows.Media.Core.TimedMetadataTrack) classes and general guidance for using timed metadata in your app.

The basic implementation steps are the same for all of the different types of timed metadata described in this article:

1. Create a [**MediaSource**](/uwp/api/windows.media.core.mediasource) and then a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) for the content to be played.
2. Register for the [**MediaPlaybackItem.TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs as the sub-tracks of the media item are resolved by the media pipeline.
3. Register for the [**TimedMetadataTrack.CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) and [**TimedMetadataTrack.CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) events for the timed metadata tracks you want to use.
4. In the **CueEntered** event handler, update your UI based on the metadata passed in the event args. You can update the UI again, to remove the current subtitle text for example, in the **CueExited** event.

In this article, handling each type of metadata is shown as a distinct scenario, but it's possible to handle (or ignore) different types of metadata using mostly shared code. You can check the [**TimedMetadataKind**](/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) property of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) object at multiple points in the process. So, for example, you could choose to register for the **CueEntered** event for metadata tracks that have the value **TimedMetadataKind.ImageSubtitle**, but not for tracks that have the value **TimedMetadataKind.Speech**. Or instead, you could register a handler for all metadata track types and then check the **TimedMetadataKind** value inside the **CueEntered** handler to determine what action to take in response to the cue.

## Image-based subtitles

WinUI apps can support external image-based subtitles in VobSub format. To use this feature, first create a [**MediaSource**](/uwp/api/windows.media.core.mediasource) object for the media content for which image subtitles will be displayed. Next, create a [**TimedTextSource**](/uwp/api/windows.media.core.timedtextsource) object by calling [**CreateFromUriWithIndex**](/uwp/api/windows.media.core.timedtextsource.CreateFromUriWithIndex) or [**CreateFromStreamWithIndex**](/uwp/api/windows.media.core.timedtextsource.CreateFromStreamWithIndex), passing in the Uri of the .sub file containing the subtitle image data and the .idx file containing the timing information for the subtitles. Add the **TimedTextSource** to the **MediaSource** by adding it to the source's [**ExternalTimedTextSources**](/uwp/api/windows.media.core.mediasource.ExternalTimedTextSources) collection. Create a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

```csharp
var contentUri = new Uri("http://contoso.com/content.mp4");
var mediaSource = MediaSource.CreateFromUri(contentUri);

var subUri = new Uri("http://contoso.com/content.sub");
var idxUri = new Uri("http://contoso.com/content.idx");
var timedTextSource = TimedTextSource.CreateFromUriWithIndex(subUri, idxUri);
mediaSource.ExternalTimedTextSources.Add(timedTextSource);

var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

Register for the image subtitle metadata events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForImageSubtitles**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForImageSubtitles**.

```csharp
mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
{
    if (args.CollectionChange == CollectionChange.ItemInserted)
    {
        RegisterMetadataHandlerForImageSubtitles(sender, (int)args.Index);
    }
    else if (args.CollectionChange == CollectionChange.Reset)
    {
        for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
        {
            if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                RegisterMetadataHandlerForImageSubtitles(sender, index);
        }
    }
};

for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
{
    RegisterMetadataHandlerForImageSubtitles(mediaPlaybackItem, index);
}
```

After registering for the image subtitle metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement).

```csharp
mediaPlayer = new MediaPlayer();
mediaPlayerElement.SetMediaPlayer(mediaPlayer);
mediaPlayer.Source = mediaPlaybackItem;
mediaPlayer.Play();
```

In the **RegisterMetadataHandlerForImageSubtitles** helper method, get an instance of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Register for the [**CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

```csharp
private void RegisterMetadataHandlerForImageSubtitles(MediaPlaybackItem item, int index)
{
    var timedTrack = item.TimedMetadataTracks[index];
    timedTrack.CueEntered += metadata_ImageSubtitleCueEntered;
    timedTrack.CueExited += metadata_ImageSubtitleCueExited;
    item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);

}
```

In the handler for the **CueEntered** event, you can check the [**TimedMetadataKind**](/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) property of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) object passed into the handler to see if the metadata is for image subtitles. This is necessary if you are using the same data cue event handler for multiple types of metadata. If the associated metadata track is of type **TimedMetadataKind.ImageSubtitle**, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](/uwp/api/windows.media.core.mediacueeventargs) to an [**ImageCue**](/uwp/api/windows.media.core.imagecue). The [**SoftwareBitmap**](/uwp/api/windows.media.core.imagecue.SoftwareBitmap) property of the **ImageCue** contains a [**SoftwareBitmap**](/uwp/api/windows.graphics.imaging.softwarebitmap) representation of the subtitle image. Create a [**SoftwareBitmapSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource) and call [**SetBitmapAsync**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource.SetBitmapAsync) to assign the image to a XAML [**Image**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) control. The [**Extent**](/uwp/api/windows.media.core.imagecue.Extent) and [**Position**](/uwp/api/windows.media.core.imagecue.Position) properties of the **ImageCue** provide information about the size and position of the subtitle image.

```csharp
private void metadata_ImageSubtitleCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
{
    // Check in case there are different tracks and the handler was used for more tracks
    if (timedMetadataTrack.TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
    {
        var cue = args.Cue as ImageCue;
        if (cue != null)
        {
            DispatcherQueue.TryEnqueue(async () =>
            {
                var source = new SoftwareBitmapSource();
                await source.SetBitmapAsync(cue.SoftwareBitmap);
                subtitleImage.Source = source;
                subtitleImage.Width = cue.Extent.Width;
                subtitleImage.Height = cue.Extent.Height;
                subtitleImage.SetValue(Canvas.LeftProperty, cue.Position.X);
                subtitleImage.SetValue(Canvas.TopProperty, cue.Position.Y);
            });
        }
    }
}
```

## Speech cues

WinUI apps can register to receive events in response to word boundaries, sentence boundaries, and Speech Synthesis Markup Language (SSML) bookmarks in played media. This allows you to play audio streams generated with the [**SpeechSynthesizer**](/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer) class and update your UI based on these events, such as displaying the text of the currently playing word or sentence.

The example shown in this section uses a class member variable to store a text string that will be synthesized and played back.

```csharp
string inputText = "In the lake heading for the mountain, the flea swims";
```

Create a new instance of the **SpeechSynthesizer** class. Set the [**IncludeWordBoundaryMetadata**](/uwp/api/windows.media.speechsynthesis.speechsynthesizeroptions.IncludeWordBoundaryMetadata) and [**IncludeSentenceBoundaryMetadata**](/uwp/api/windows.media.speechsynthesis.speechsynthesizeroptions.IncludeSentenceBoundaryMetadata) options for the synthesizer to **true** to specify that the metadata should be included in the generated media stream. Call [**SynthesizeTextToStreamAsync**](/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer.SynthesizeTextToStreamAsync) to generate a stream containing the synthesized speech and corresponding metadata. Create a [**MediaSource**](/uwp/api/windows.media.core.mediasource) and a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) from the synthesized stream.

```csharp
var synthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

// Enable word marker generation (false by default).
synthesizer.Options.IncludeWordBoundaryMetadata = true;
synthesizer.Options.IncludeSentenceBoundaryMetadata = true;

var stream = await synthesizer.SynthesizeTextToStreamAsync(inputText);
var mediaSource = MediaSource.CreateFromStream(stream, "");
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

Register for the speech metadata events using the **MediaPlaybackItem** object. This example uses a helper method, **RegisterMetadataHandlerForSpeech**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**.  In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForSpeech**.

```csharp
// Since the tracks are added later we will
// monitor the tracks being added and subscribe to the ones of interest
mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
{
    if (args.CollectionChange == CollectionChange.ItemInserted)
    {
        RegisterMetadataHandlerForSpeech(sender, (int)args.Index);
    }
    else if (args.CollectionChange == CollectionChange.Reset)
    {
        for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
        {
            RegisterMetadataHandlerForSpeech(sender, index);
        }
    }
};

// If tracks were available at source resolution time, itterate through and register:
for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
{
    RegisterMetadataHandlerForSpeech(mediaPlaybackItem, index);
}
```

After registering for the speech metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement).

```csharp
mediaPlayer = new MediaPlayer();
mediaPlayerElement.SetMediaPlayer(mediaPlayer);
mediaPlayer.Source = mediaPlaybackItem;
mediaPlayer.Play();
```

In the **RegisterMetadataHandlerForSpeech** helper method, get an instance of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Register for the [**CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

```csharp
private void RegisterMetadataHandlerForSpeech(MediaPlaybackItem item, int index)
{
    var timedTrack = item.TimedMetadataTracks[index];
    timedTrack.CueEntered += metadata_SpeechCueEntered;
    timedTrack.CueExited += metadata_SpeechCueExited;
    item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);

}
```

In the handler for the **CueEntered** event, you can check the [**TimedMetadataKind**](/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) property of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) object passed into the handler to see if the metadata is speech. This is necessary if you are using the same data cue event handler for multiple types of metadata. If the associated metadata track is of type **TimedMetadataKind.Speech**, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](/uwp/api/windows.media.core.mediacueeventargs) to a [**SpeechCue**](/uwp/api/windows.media.core.speechcue). For speech cues, the type of speech cue included in the metadata track is determined by checking the **Label** property. The value of this property will be "SpeechWord" for word boundaries, "SpeechSentence" for sentence boundaries, or "SpeechBookmark" for SSML bookmarks. In this example, we check for the "SpeechWord" value, and if this value is found, the [**StartPositionInInput**](/uwp/api/windows.media.core.speechcue.StartPositionInInput) and [**EndPositionInInput**](/uwp/api/windows.media.core.speechcue.EndPositionInInput) properties of the **SpeechCue** are used to determine location within the input text of the word currently being played back. This example simply outputs each word to the debug output.

```csharp
private void metadata_SpeechCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
{
    // Check in case there are different tracks and the handler was used for more tracks
    if (timedMetadataTrack.TimedMetadataKind == TimedMetadataKind.Speech)
    {
        var cue = args.Cue as SpeechCue;
        if (cue != null)
        {
            if (timedMetadataTrack.Label == "SpeechWord")
            {
                // Do something with the cue
                System.Diagnostics.Debug.WriteLine($"{cue.StartPositionInInput} - {cue.EndPositionInInput}: {inputText.Substring((int)cue.StartPositionInInput, ((int)cue.EndPositionInInput - (int)cue.StartPositionInInput) + 1)}");
            }
        }
    }
}
```

## Chapter cues

WinUI apps can register for cues that correspond to chapters within a media item. To use this feature, create a [**MediaSource**](/uwp/api/windows.media.core.mediasource) object for the media content and then create a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

```csharp
var contentUri = new Uri("http://contoso.com/content.mp4");
var mediaSource = MediaSource.CreateFromUri(contentUri);
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

Register for the chapter metadata events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForChapterCues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForChapterCues**.

```csharp
mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
{
    if (args.CollectionChange == CollectionChange.ItemInserted)
    {
        RegisterMetadataHandlerForChapterCues(sender, (int)args.Index);
    }
    else if (args.CollectionChange == CollectionChange.Reset)
    {
        for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
        {
            if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                RegisterMetadataHandlerForChapterCues(sender, index);
        }
    }
};

for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
{
    RegisterMetadataHandlerForChapterCues(mediaPlaybackItem, index);
}
```

After registering for the chapter metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement).

```csharp
mediaPlayer = new MediaPlayer();
mediaPlayerElement.SetMediaPlayer(mediaPlayer);
mediaPlayer.Source = mediaPlaybackItem;
mediaPlayer.Play();
```

In the **RegisterMetadataHandlerForChapterCues** helper method, get an instance of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Register for the [**CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

```csharp
private void RegisterMetadataHandlerForChapterCues(MediaPlaybackItem item, int index)
{
    var timedTrack = item.TimedMetadataTracks[index];
    timedTrack.CueEntered += metadata_ChapterCueEntered;
    timedTrack.CueExited += metadata_ChapterCueExited;
    item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
}
```

In the handler for the **CueEntered** event, you can check the [**TimedMetadataKind**](/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) property of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) object passed into the handler to see if the metadata is for chapter cues. This is necessary if you are using the same data cue event handler for multiple types of metadata. If the associated metadata track is of type **TimedMetadataKind.Chapter**, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](/uwp/api/windows.media.core.mediacueeventargs) to an [**ChapterCue**](/uwp/api/windows.media.core.chaptercue). The [**Title**](/uwp/api/windows.media.core.chaptercue.Title) property of the **ChapterCue** contains the title of the chapter that has just been reached in playback.

```csharp
private void metadata_ChapterCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
{
    // Check in case there are different tracks and the handler was used for more tracks
    if (timedMetadataTrack.TimedMetadataKind == TimedMetadataKind.Chapter)
    {
        var cue = args.Cue as ChapterCue;
        if (cue != null)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                chapterTitleTextBlock.Text = cue.Title;
            });
        }
    }
}
```

### Seek to the next chapter using chapter cues

In addition to receiving notifications when the current chapter changes in a playing item, you can also use chapter cues to seek to the next chapter within a playing item. The example method shown below takes as arguments a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) and a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) representing the currently playing media item. The [**TimedMetadataTracks**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracks) collection is searched to see if any of the tracks have [**TimedMetadataKind**](/uwp/api/windows.media.core.timedmetadatatrack.TimedMetadataKind) property of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) value of **TimedMetadataKind.Chapter**.  If a chapter track is found, the method loops through each cue in the track's [**Cues**](/uwp/api/windows.media.core.timedmetadatatrack.Cues) collection to find the first cue that has a [**StartTime**](/uwp/api/windows.media.core.chaptercue.StartTime) greater than the current [**Position**](/uwp/api/windows.media.playback.mediaplaybacksession.Position) of the media player's playback session. Once the correct cue is found, the position of the playback session is updated and the chapter title is updated in the UI.

```csharp
private void GoToNextChapter(MediaPlayer player, MediaPlaybackItem item)
{
    // Find the chapters track if one exists
    TimedMetadataTrack chapterTrack = item.TimedMetadataTracks.FirstOrDefault(track => track.TimedMetadataKind == TimedMetadataKind.Chapter);
    if (chapterTrack == null)
    {
        return;
    }

    // Find the first chapter that starts after current playback position
    TimeSpan currentPosition = player.PlaybackSession.Position;
    foreach (ChapterCue cue in chapterTrack.Cues)
    {
        if (cue.StartTime > currentPosition)
        {
            // Change player position to chapter start time
            player.PlaybackSession.Position = cue.StartTime;

            // Display chapter name
            chapterTitleTextBlock.Text = cue.Title;
            break;
        }
    }
}
```

## Extended M3U comments

WinUI apps can register for cues that correspond to comments within a Extended M3U manifest file. This example uses [**AdaptiveMediaSource**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) to play the media content. For more information, see [Adaptive Streaming](adaptive-streaming.md). Create an **AdaptiveMediaSource** for the content by calling [**CreateFromUriAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromUriAsync) or [**CreateFromStreamAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromStreamAsync). Create a  [**MediaSource**](/uwp/api/windows.media.core.mediasource) object by calling [**CreateFromAdaptiveMediaSource**](/uwp/api/windows.media.core.mediasource.CreateFromAdaptiveMediaSource) and then create a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

```csharp
AdaptiveMediaSourceCreationResult result =
    await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
{
    // TODO: Handle adaptive media source creation errors.
    return;
}
var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

Register for the M3U metadata events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForEXTM3UCues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForEXTM3UCues**.

```csharp
mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
{
    if (args.CollectionChange == CollectionChange.ItemInserted)
    {
        RegisterMetadataHandlerForEXTM3UCues(sender, (int)args.Index);
    }
    else if (args.CollectionChange == CollectionChange.Reset)
    {
        for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
        {
            if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                RegisterMetadataHandlerForEXTM3UCues(sender, index);
        }
    }
};

for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
{
    RegisterMetadataHandlerForEXTM3UCues(mediaPlaybackItem, index);
}
```

After registering for the M3U metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement).

```csharp
mediaPlayer = new MediaPlayer();
mediaPlayerElement.SetMediaPlayer(mediaPlayer);
mediaPlayer.Source = mediaPlaybackItem;
mediaPlayer.Play();
```

In the **RegisterMetadataHandlerForEXTM3UCues** helper method, get an instance of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Check the DispatchType property of the metadata track, which will have a value of "EXTM3U" if the track represents M3U comments. Register for the [**CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

```csharp
private void RegisterMetadataHandlerForEXTM3UCues(MediaPlaybackItem item, int index)
{
    var timedTrack = item.TimedMetadataTracks[index];
    var dispatchType = timedTrack.DispatchType;

    if (String.Equals(dispatchType, "EXTM3U", StringComparison.OrdinalIgnoreCase))
    {
        timedTrack.Label = "EXTM3U comments";
        timedTrack.CueEntered += metadata_EXTM3UCueEntered;
        timedTrack.CueExited += metadata_EXTM3UCueExited;
        item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
    }
}
```

In the handler for the **CueEntered** event, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](/uwp/api/windows.media.core.mediacueeventargs) to an [**DataCue**](/uwp/api/windows.media.core.datacue).  Check to make sure the **DataCue** and the [**Data**](/uwp/api/windows.media.core.datacue.Data) property of the cue are not null. Extended EMU comments are provided in the form of UTF-16, little endian, null terminated strings. Create a new **DataReader** to read the cue data by calling [**DataReader.FromBuffer**](/uwp/api/windows.storage.streams.datareader.FromBuffer). Set the [**UnicodeEncoding**](/uwp/api/windows.storage.streams.datareader.UnicodeEncoding) property of the reader to [**Utf16LE**](/uwp/api/windows.storage.streams.unicodeencoding) to read the data in the correct format. Call [**ReadString**](/uwp/api/windows.storage.streams.datareader.ReadString) to read the data, specifying half of the length of the **Data** field, because each character is two bytes in size, and subtract one to remove the trailing null character. In this example, the M3U comment is simply written to the debug output.

```csharp
private void metadata_EXTM3UCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
{
    var dataCue = args.Cue as DataCue;
    if (dataCue != null && dataCue.Data != null)
    {
        // The payload is a UTF-16 Little Endian null-terminated string.
        // It is any comment line in a manifest that is not part of the HLS spec.
        var dr = Windows.Storage.Streams.DataReader.FromBuffer(dataCue.Data);
        dr.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
        var m3uComment = dr.ReadString(dataCue.Data.Length / 2 - 1);
        System.Diagnostics.Debug.WriteLine(m3uComment);
    }
}
```

## ID3 tags

WinUI apps can register for cues that correspond to ID3 tags within Http Live Streaming (HLS) content. This example uses [**AdaptiveMediaSource**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) to play the media content. For more information, see [Adaptive Streaming](adaptive-streaming.md). Create an **AdaptiveMediaSource** for the content by calling [**CreateFromUriAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromUriAsync) or [**CreateFromStreamAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromStreamAsync). Create a  [**MediaSource**](/uwp/api/windows.media.core.mediasource) object by calling [**CreateFromAdaptiveMediaSource**](/uwp/api/windows.media.core.mediasource.CreateFromAdaptiveMediaSource) and then create a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

```csharp
AdaptiveMediaSourceCreationResult result =
    await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
{
    // TODO: Handle adaptive media source creation errors.
    return;
}
var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

Register for the ID3 tag events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForID3Cues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForID3Cues**.

```csharp
AdaptiveMediaSourceCreationResult result =
    await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
{
    // TODO: Handle adaptive media source creation errors.
    return;
}
var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

After registering for the ID3 metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement).

```csharp
mediaPlayer = new MediaPlayer();
mediaPlayerElement.SetMediaPlayer(mediaPlayer);
mediaPlayer.Source = mediaPlaybackItem;
mediaPlayer.Play();
```


In the **RegisterMetadataHandlerForID3Cues** helper method, get an instance of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Check the DispatchType property of the metadata track, which will have a value containing the GUID string "15260DFFFF49443320FF49443320000F" if the track represents ID3 tags. Register for the [**CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.

```csharp
private void RegisterMetadataHandlerForID3Cues(MediaPlaybackItem item, int index)
{
    var timedTrack = item.TimedMetadataTracks[index];
    var dispatchType = timedTrack.DispatchType;

    if (String.Equals(dispatchType, "15260DFFFF49443320FF49443320000F", StringComparison.OrdinalIgnoreCase))
    {
        timedTrack.Label = "ID3 tags";
        timedTrack.CueEntered += metadata_ID3CueEntered;
        timedTrack.CueExited += metadata_ID3CueExited;
        item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
    }
}
```

In the handler for the **CueEntered** event, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](/uwp/api/windows.media.core.mediacueeventargs) to an [**DataCue**](/uwp/api/windows.media.core.datacue).  Check to make sure the **DataCue** and the [**Data**](/uwp/api/windows.media.core.datacue.Data) property of the cue are not null. Extended EMU comments are provided in the form raw bytes in the transport stream (see [ID3](https://en.wikipedia.org/wiki/ID3)). Create a new **DataReader** to read the cue data by calling [**DataReader.FromBuffer**](/uwp/api/windows.storage.streams.datareader.FromBuffer).  In this example, the header values from the ID3 tag are read from the cue data and written to the debug output.

```csharp
private void metadata_ID3CueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
{
    var dataCue = args.Cue as DataCue;
    if (dataCue != null && dataCue.Data != null)
    {
        // The payload is the raw ID3 bytes found in a TS stream
        // Ref: http://id3.org/id3v2.4.0-structure
        var dr = Windows.Storage.Streams.DataReader.FromBuffer(dataCue.Data);
        var header_ID3 = dr.ReadString(3);
        var header_version_major = dr.ReadByte();
        var header_version_minor = dr.ReadByte();
        var header_flags = dr.ReadByte();
        var header_tagSize = dr.ReadUInt32();

        System.Diagnostics.Debug.WriteLine($"ID3 tag data: major {header_version_major}, minor: {header_version_minor}");
    }
}
```

## Fragmented mp4 emsg boxes

WinUI apps can register for cues that correspond to emsg boxes within fragmented mp4 streams. An example usage of this type of metadata is for content providers to signal client applications to play an ad during live streaming content. This example uses [**AdaptiveMediaSource**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource) to play the media content. For more information, see [Adaptive Streaming](adaptive-streaming.md). Create an **AdaptiveMediaSource** for the content by calling [**CreateFromUriAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromUriAsync) or [**CreateFromStreamAsync**](/uwp/api/windows.media.streaming.adaptive.adaptivemediasource.CreateFromStreamAsync). Create a  [**MediaSource**](/uwp/api/windows.media.core.mediasource) object by calling [**CreateFromAdaptiveMediaSource**](/uwp/api/windows.media.core.mediasource.CreateFromAdaptiveMediaSource) and then create a [**MediaPlaybackItem**](/uwp/api/windows.media.playback.mediaplaybackitem) from the **MediaSource**.

```csharp
AdaptiveMediaSourceCreationResult result =
    await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
{
    // TODO: Handle adaptive media source creation errors.
    return;
}
var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

Register for the emsg box events using the **MediaPlaybackItem** object created in the previous step. This example uses a helper method, **RegisterMetadataHandlerForEmsgCues**, to register for the events. A lambda expression is used to implement a handler for the [**TimedMetadataTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.TimedMetadataTracksChanged) event, which occurs when the system detects a change in the metadata tracks associated with a **MediaPlaybackItem**. In some cases, the metadata tracks may be available when the playback item is initially resolved, so outside of the **TimedMetadataTracksChanged** handler, we also loop through the available metadata tracks and call **RegisterMetadataHandlerForEmsgCues**.

```csharp
AdaptiveMediaSourceCreationResult result =
    await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
{
    // TODO: Handle adaptive media source creation errors.
    return;
}
var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
```

After registering for the emsg box metadata events, the **MediaItem** is assigned to a [**MediaPlayer**](/uwp/api/windows.media.playback.mediaplayer) for playback within a [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement).

```csharp
mediaPlayer = new MediaPlayer();
mediaPlayerElement.SetMediaPlayer(mediaPlayer);
mediaPlayer.Source = mediaPlaybackItem;
mediaPlayer.Play();
```


In the **RegisterMetadataHandlerForEmsgCues** helper method, get an instance of the [**TimedMetadataTrack**](/uwp/api/windows.media.core.timedmetadatatrack) class by indexing into the **TimedMetadataTracks** collection of the **MediaPlaybackItem**. Check the DispatchType property of the metadata track, which will have a value of "emsg:mp4" if the track represents emsg boxes. Register for the [**CueEntered**](/uwp/api/windows.media.core.timedmetadatatrack.CueEntered) event and the [**CueExited**](/uwp/api/windows.media.core.timedmetadatatrack.CueExited) event. Then, you must call [**SetPresentationMode**](/uwp/api/windows.media.playback.mediaplaybacktimedmetadatatracklist.SetPresentationMode) on the playback item's **TimedMetadataTracks** collection, to instruct the system that the app wants to receive metadata cue events for this playback item.


```csharp
private void RegisterMetadataHandlerForEmsgCues(MediaPlaybackItem item, int index)
{
    var timedTrack = item.TimedMetadataTracks[index];
    var dispatchType = timedTrack.DispatchType;

    if (String.Equals(dispatchType, "emsg:mp4", StringComparison.OrdinalIgnoreCase))
    {
        timedTrack.Label = "mp4 Emsg boxes";
        timedTrack.CueEntered += metadata_EmsgCueEntered;
        timedTrack.CueExited += metadata_EmsgCueExited;
        item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
    }
}
```


In the handler for the **CueEntered** event, cast the data cue contained in the **Cue** property of the [**MediaCueEventArgs**](/uwp/api/windows.media.core.mediacueeventargs) to an [**DataCue**](/uwp/api/windows.media.core.datacue).  Check to make sure the **DataCue** object is not null. The properties of the emsg box are provided by the media pipeline as custom properties in the DataCue object's [**Properties**](/uwp/api/windows.media.core.datacue.Properties) collection. This example attempts to extract several different property values using the **TryGetValue** method. If this method returns null, it means the requested property is not present in the emsg box, so a default value is set instead.

The next part of the example illustrates the scenario where ad playback is triggered, which is the case when the *scheme_id_uri* property, obtained in the previous step, has a value of "urn:scte:scte35:2013:xml". For more information, see [https://dashif.org/identifiers/event_schemes/](https://dashif.org/identifiers/event_schemes/). Note that the standard recommends sending this emsg multiple times for redundancy, so this example maintains a list of the emsg IDs that have already been processed and only processes new messages. Create a new **DataReader** to read the cue data by calling [**DataReader.FromBuffer**](/uwp/api/windows.storage.streams.datareader.FromBuffer) and set the encoding to UTF-8 by setting the [**UnicodeEncoding**](/uwp/api/windows.storage.streams.datareader.UnicodeEncoding) property, then read the data. In this example, the message payload is written to the debug output. A real app would use the payload data to schedule the playback of an ad.

```csharp
private void metadata_EmsgCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
{
    var dataCue = args.Cue as DataCue;
    if (dataCue != null)
    {
        string scheme_id_uri = string.Empty;
        string value = string.Empty;
        UInt32 timescale = (UInt32)TimeSpan.TicksPerSecond;
        UInt32 presentation_time_delta = (UInt32)dataCue.StartTime.Ticks;
        UInt32 event_duration = (UInt32)dataCue.Duration.Ticks;
        UInt32 id = 0;
        Byte[] message_data = null;

        const string scheme_id_uri_key = "emsg:scheme_id_uri";
        object propValue = null;
        dataCue.Properties.TryGetValue(scheme_id_uri_key, out propValue);
        scheme_id_uri = propValue != null ? (string)propValue : "";

        const string value_key = "emsg:value";
        propValue = null;
        dataCue.Properties.TryGetValue(value_key, out propValue);
        value = propValue != null ? (string)propValue : "";

        const string timescale_key = "emsg:timescale";
        propValue = null;
        dataCue.Properties.TryGetValue(timescale_key, out propValue);
        timescale = propValue != null ? (UInt32)propValue : timescale;

        const string presentation_time_delta_key = "emsg:presentation_time_delta";
        propValue = null;
        dataCue.Properties.TryGetValue(presentation_time_delta_key, out propValue);
        presentation_time_delta = propValue != null ? (UInt32)propValue : presentation_time_delta;

        const string event_duration_key = "emsg:event_duration";
        propValue = null;
        dataCue.Properties.TryGetValue(event_duration_key, out propValue);
        event_duration = propValue != null ? (UInt32)propValue : event_duration;

        const string id_key = "emsg:id";
        propValue = null;
        dataCue.Properties.TryGetValue(id_key, out propValue);
        id = propValue != null ? (UInt32)propValue : 0;

        System.Diagnostics.Debug.WriteLine($"Label: {timedMetadataTrack.Label}, Id: {dataCue.Id}, StartTime: {dataCue.StartTime}, Duration: {dataCue.Duration}");
        System.Diagnostics.Debug.WriteLine($"scheme_id_uri: {scheme_id_uri}, value: {value}, timescale: {timescale}, presentation_time_delta: {presentation_time_delta}, event_duration: {event_duration}, id: {id}");

        if (dataCue.Data != null)
        {
            var dr = Windows.Storage.Streams.DataReader.FromBuffer(dataCue.Data);

            // Check if this is a SCTE ad message:
            // Ref:  http://dashif.org/identifiers/event-schemes/
            if (scheme_id_uri.ToLower() == "urn:scte:scte35:2013:xml")
            {
                // SCTE recommends publishing emsg more than once, so we avoid reprocessing the same message id:
                if (!processedAdIds.Contains(id))
                {
                    processedAdIds.Add(id);
                    dr.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    var scte35payload = dr.ReadString(dataCue.Data.Length);
                    System.Diagnostics.Debug.WriteLine($", message_data: {scte35payload}");
                    // TODO: ScheduleAdFromScte35Payload(timedMetadataTrack, presentation_time_delta, timescale, event_duration, scte35payload);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"This emsg.Id, {id}, has already been processed.");
                }
            }
            else
            {
                message_data = new byte[dataCue.Data.Length];
                dr.ReadBytes(message_data);
                // TODO: Use the 'emsg' bytes for something useful.
                System.Diagnostics.Debug.WriteLine($", message_data.Length: {message_data.Length}");
            }
        }
    }
}
```


## Related topics

* [Media playback](media-playback.md)
* [Media items, playlists, and tracks](media-playback-with-mediasource.md)


 
