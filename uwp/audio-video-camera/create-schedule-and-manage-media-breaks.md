---
ms.assetid: 0309c7a1-8e4c-4326-813a-cbd9f8b8300d
description: This article shows you how to create, schedule, and manage media breaks to your media playback app.
title: Create, schedule, and manage media breaks
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Create, schedule, and manage media breaks

This article shows you how to create, schedule, and manage media breaks to your media playback app. Media breaks are typically used to insert audio or video ads into media content. Starting with Windows 10, version 1607, you can use the [**MediaBreakManager**](/uwp/api/Windows.Media.Playback.MediaBreakManager) class to quickly and easily add media breaks to any [**MediaPlaybackItem**](/uwp/api/Windows.Media.Playback.MediaPlaybackItem) that you play with a [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer).


After you schedule one or more media breaks, the system will automatically play your media content at the specified time during playback. The **MediaBreakManager** provides events so that your app can react when media breaks start, end, or when they are skipped by the user. You can also access a [**MediaPlaybackSession**](/uwp/api/Windows.Media.Playback.MediaPlaybackSession) for your media breaks to monitor events such as download and buffering progress updates.

## Schedule media breaks
Every **MediaPlaybackItem** object has its own [**MediaBreakSchedule**](/uwp/api/Windows.Media.Playback.MediaBreakSchedule) that you use to configure the media breaks that will play when the item is played. The first step for using media breaks in your app is to create a [**MediaPlaybackItem**](/uwp/api/Windows.Media.Playback.MediaPlaybackItem) for your main playback content. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetMoviePlaybackItem":::

For more information about working with **MediaPlaybackItem**, [**MediaPlaybackList**](/uwp/api/Windows.Media.Playback.MediaPlaybackList) and other fundamental media playback APIs, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

The next example shows how to add a preroll break to the **MediaPlaybackItem**, which means that the system will play the media break before playing the playback item to which the break belongs. First a new [**MediaBreak**](/uwp/api/Windows.Media.Playback.MediaBreak) object is instantiated. In this example, the constructor is called with [**MediaBreakInsertionMethod.Interrupt**](/uwp/api/Windows.Media.Playback.MediaBreakInsertionMethod), meaning that the main content will be paused while the break content is played. 

Next, a new **MediaPlaybackItem** is created for the content that will be played during the break, such as an ad. The [**CanSkip**](/uwp/api/windows.media.playback.mediaplaybackitem.canskip) property of this playback item is set to false. This means that the user will not be able to skip the item using the built-in media controls. Your app can still choose to skip the add programmatically by calling [**SkipCurrentBreak**](/uwp/api/windows.media.playback.mediabreakmanager.skipcurrentbreak). 

The media break's [**PlaybackList**](/uwp/api/windows.media.playback.mediabreak.playbacklist) property is a [**MediaPlaybackList**](/uwp/api/Windows.Media.Playback.MediaPlaybackList) that allows you to play multiple media items as a playlist. Add one or more **MediaPlaybackItem** objects from the list's **Items** collection to include them in the media break's playlist.

Finally, schedule the media break by using the main content playback item's [**BreakSchedule**](/uwp/api/windows.media.playback.mediaplaybackitem.breakschedule) property. Specify the break to be a preroll break by assigning it to the [**PrerollBreak**](/uwp/api/windows.media.playback.mediabreakschedule.prerollbreak) property of the schedule object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetPreRollBreak":::

Now you can play back the main media item, and the media break that you created will play before the main content. Create a new [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) object and optionally set the [**AutoPlay**](/uwp/api/windows.media.playback.mediaplayer.autoplay) property to true to start playback automatically. Set the [**Source**](/uwp/api/windows.media.playback.mediaplayer.source) property of the **MediaPlayer** to your main content playback item. It's not required, but you can assign the **MediaPlayer** to a [**MediaPlayerElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) to render the media in a XAML page. For more information about using **MediaPlayer**, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetPlay":::

Add a postroll break that plays after the **MediaPlaybackItem** containing your main content finishes playing, by using the same technique as a preroll break, except that you assign your **MediaBreak** object to the [**PostrollBreak**](/uwp/api/windows.media.playback.mediabreakschedule.postrollbreak) property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetPostRollBreak":::

You can also schedule one or more midroll breaks that play at a specified time within the playback of the main content. In the following example, the [**MediaBreak**](/uwp/api/Windows.Media.Playback.MediaBreak) is created with the constructor overload that accepts a **TimeSpan** object, which specifies the time within the playback of the main media item when the break will be played. Again, [**MediaBreakInsertionMethod.Interrupt**](/uwp/api/Windows.Media.Playback.MediaBreakInsertionMethod) is specified to indicate that the main content's playback will be paused while the break plays. The midroll break is added to the schedule by calling [**InsertMidrollBreak**](/uwp/api/windows.media.playback.mediabreakschedule.insertmidrollbreak). You can get a read-only list of the current midroll breaks in the schedule by accessing the [**MidrollBreaks**](/uwp/api/windows.media.playback.mediabreakschedule.midrollbreaks) property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetMidrollBreak":::

The next midroll break example shown uses the [**MediaBreakInsertionMethod.Replace**](/uwp/api/Windows.Media.Playback.MediaBreakInsertionMethod) insertion method, which means that the system will continue processing the main content while the break is playing. This option is typically used by live streaming media apps where you don't want the content to pause and fall behind the live stream while the ad is played. 

This example also uses an overload of the [**MediaPlaybackItem**](/uwp/api/Windows.Media.Playback.MediaPlaybackItem) constructor that accepts two [**TimeSpan**](/uwp/api/Windows.Foundation.TimeSpan) parameters. The first parameter specifies the starting point within the media break item where playback will begin. The second parameter specifies the duration for which the media break item will be played. So, in the following example, the **MediaBreak** will begin playing at 20 minutes into the main content. When it plays, the media item will start 30 seconds from the beginning of the break media item and will play for 15 seconds before the main media content resumes playing.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetMidrollBreak2":::

## Skip media breaks
As mentioned previously in this article, the [**CanSkip**](/uwp/api/windows.media.playback.mediaplaybackitem.canskip) property of a **MediaPlaybackItem** can be set to prevent the user from skipping the content with the built-in controls. However, you can call [**SkipCurrentBreak**](/uwp/api/windows.media.playback.mediabreakmanager.skipcurrentbreak) from your code at any time to skip the current break.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetSkipButtonClick":::

## Handle MediaBreak events

There are several events related to media breaks that you can register for in order to take action based on the changing state of media breaks.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetRegisterMediaBreakEvents":::

The [**BreakStarted**](/uwp/api/windows.media.playback.mediabreakmanager.breakstarted) is raised when a media break starts. You may want to update your UI to let the user know that media break content is playing. This example uses the [**MediaBreakStartedEventArgs**](/uwp/api/Windows.Media.Playback.MediaBreakStartedEventArgs) passed into the handler to get a reference to the media break that started. Then the [**CurrentItemIndex**](/uwp/api/windows.media.playback.mediaplaybacklist.currentitemindex) property is used to determine which media item in the media break's playlist is being played. Then the UI is updated to show the user the current ad index and the number of ads remaining in the break. Remember that updates to the UI must be made on the UI thread, so the call should be made inside a call to [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync). 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetBreakStarted":::

[**BreakEnded**](/uwp/api/windows.media.playback.mediabreakmanager.breakended) is raised when all of the media items in the break have finished playing or have been skipped over. You can use the handler for this event to update the UI to indicate that media break content is no longer playing.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetBreakEnded":::

The **BreakSkipped** event is raised when the user presses the *Next* button in the built-in UI during playback of an item for which [**CanSkip**](/uwp/api/windows.media.playback.mediaplaybackitem.canskip) is true, or when you skip a break in your code by calling [**SkipCurrentBreak**](/uwp/api/windows.media.playback.mediabreakmanager.skipcurrentbreak).

The following example uses the [**Source**](/uwp/api/windows.media.playback.mediaplayer.source) property of the **MediaPlayer** to get a reference to the media item for the main content. The skipped media break belongs to the break schedule of this item. Next, the code checks to see if the media break that was skipped is the same as the media break set to the [**PrerollBreak**](/uwp/api/windows.media.playback.mediabreakschedule.prerollbreak) property of the schedule. If so, this means that the preroll break was the break that was skipped, and in this case, a new midroll break is created and scheduled to play 10 minutes into the main content.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetBreakSkipped":::

[**BreaksSeekedOver**](/uwp/api/windows.media.playback.mediabreakmanager.breaksseekedover) is raised when the playback position of the main media item passes over the scheduled time for one or more media breaks. The following example checks to see if more than one media break was seeked over, if the playback position was moved forward, and if it was moved forward less than 10 minutes. If so, the first break that was seeked over, obtained from the [**SeekedOverBreaks**](/uwp/api/windows.media.playback.mediabreakseekedovereventargs.seekedoverbreaks) collection exposed by the event args, is played immediately with a call to the [**PlayBreak**](/uwp/api/windows.media.playback.mediabreakmanager.playbreak) method of the **MediaPlayer.BreakManager**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetBreakSeekedOver":::


## Access the current playback session
The [**MediaPlaybackSession**](/uwp/api/Windows.Media.Playback.MediaPlaybackSession) object uses the **MediaPlayer** class to provide data and events related to the currently playing media content. The [**MediaBreakManager**](/uwp/api/Windows.Media.Playback.MediaBreakManager) also has a **MediaPlaybackSession** that you can access to get data and events specifically related to the media break content that is being played. Information you can get from the playback session includes the current playback state, playing or paused, and the current playback position within the content. You can use the [**NaturalVideoWidth**](/uwp/api/windows.media.playback.mediaplaybacksession.naturalvideowidth) and [**NaturalVideoHeight**](/uwp/api/windows.media.playback.mediaplaybacksession.naturalvideoheight) properties and the [**NaturalVideoSizeChanged**](/uwp/api/windows.media.playback.mediaplaybacksession.naturalvideosizechanged) to adjust your video UI if the media break content has a different aspect ratio than your main content. You can also receive events such as [**BufferingStarted**](/uwp/api/windows.media.playback.mediaplaybacksession.bufferingstarted), [**BufferingEnded**](/uwp/api/windows.media.playback.mediaplaybacksession.bufferingended), and [**DownloadProgressChanged**](/uwp/api/windows.media.playback.mediaplaybacksession.downloadprogresschanged) that can provide valuable telemetry about the performance of your app.

The following example registers a handler for the **BufferingProgressChanged event**; in the event handler, it updates the UI to show the current buffering progress.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetRegisterBufferingProgressChanged":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaBreaks_RS1/cs/MainPage.xaml.cs" id="SnippetBufferingProgressChanged":::

## Related topics
* [Media playback](media-playback.md)
* [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md)
* [Manual control of the System Media Transport Controls](system-media-transport-controls.md)

 

 
