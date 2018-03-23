---
author: drewbatgit
ms.assetid: eb690f2b-3bf8-4a65-99a4-2a3a8c7760b7
description: This article shows you how to interact with the System Media Transport Controls.
title: Integrate with the System Media Transport Controls
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Integrate with the System Media Transport Controls

This article shows you how to interact with the System Media Transport Controls (SMTC). The SMTC is a set of controls that are common to all Windows 10 devices and that provide a consistent way for users to control media playback for all running apps that use [**MediaPlayer**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer) for playback.

For a complete sample that demonstrates integration with the SMTC, see [System Media Tranport Controls sample on github](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/SystemMediaTransportControls).
            		
## Automatic integration with SMTC
Starting with Windows 10, version 1607, UWP apps that use the [**MediaPlayer**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer) class to play media are automatically integrated with the SMTC by default. Simply instantiate a new instance of **MediaPlayer** and assign a [**MediaSource**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Core.MediaSource), [**MediaPlaybackItem**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackItem), or [**MediaPlaybackList**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackList) to the player's [**Source**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer.Source) property and the user will see your app name in the SMTC and can play, pause, and move through your playback lists by using the SMTC controls. 

Your app can create and use multiple **MediaPlayer** objects at once. For each active **MediaPlayer** instance in your app, a separate tab is created in the SMTC, allowing the user to switch between your active media players and those of other running apps. Whichever media player is currently selected in the SMTC is the one that the controls will affect.

For more information on using **MediaPlayer** in your app, including binding it to a [**MediaPlayerElement**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement) in your XAML page, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md). 

For more information on working with **MediaSource**, **MediaPlaybackItem**, and **MediaPlaybackList**, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Add metadata to be displayed by the SMTC
If you want add or modify the metadata that is displayed for your media items in the SMTC, such as a video or song title, you need to update the display properties for the **MediaPlaybackItem** representing your media item. First, get a reference to the [**MediaItemDisplayProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaItemDisplayProperties) object by calling [**GetDisplayProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackItem.GetDisplayProperties). Next, set the type of media, music or video, for the item with the [**Type**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaItemDisplayProperties.Type) property. Then you can populate the fields of the [**MusicProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaItemDisplayProperties.MusicProperties) or [**VideoProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaItemDisplayProperties.VideoProperties), depending on which media type you specified. Finally, update the metadata for the media item by calling [**ApplyDisplayProperties**](https://msdn.microsoft.com/library/windows/apps/mt489923).

[!code-cs[SetVideoProperties](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetSetVideoProperties)]

[!code-cs[SetMusicProperties](./code/MediaSource_RS1/cs/MainPage.xaml.cs#SnippetSetMusicProperties)]

## Use CommandManager to modify or override the default SMTC commands
Your app can modify or completely override the behavior of the SMTC controls with the [**MediaPlaybackCommandManager**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager) class. A command manager instance can be obtained for each instance of the **MediaPlayer** class by accessing the [**CommandManager**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer.CommandManager) property.

For every command, such as the *Next* command which by default skips to the next item in a **MediaPlaybackList**, the command manager exposes a received event, like [**NextReceived**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager.NextReceived), and an object that manages the behavior of the command, like [**NextBehavior**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager.NextBehavior). 

The following example registers a handler for the **NextReceived** event and for the [**IsEnabledChanged**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerCommandBehavior.IsEnabledChanged) event of the **NextBehavior**.

[!code-cs[AddNextHandler](./code/SMTC_RS1/cs/MainPage.xaml.cs#SnippetAddNextHandler)]

The following example illustrates a scenario where the app wants to disable the *Next* command after the user has clicked through five items in the playlist, perhaps requiring some user interaction before continuing playing content. Each ## the **NextReceived** event is raised, a counter is incremented. Once the counter reaches the target number, the [**EnablingRule**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerCommandBehavior.EnablingRule) for the *Next* command is set to [**Never**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaCommandEnablingRule), which disables the command. 

[!code-cs[NextReceived](./code/SMTC_RS1/cs/MainPage.xaml.cs#SnippetNextReceived)]

You can also set the command to **Always**, which means the command will always be enabled even if, for the *Next* command example, there are no more items in the playlist. Or you can set the command to **Auto**, where the system determines whether the command should be enabled based on the current content being played.

For the scenario described above, at some point the app will want to reenable the *Next* command and does so by setting the **EnablingRule** to **Auto**.

[!code-cs[EnableNextButton](./code/SMTC_RS1/cs/MainPage.xaml.cs#SnippetEnableNextButton)]

Because your app may have it's own UI for controlling playback while it is in the foreground, you can use the [**IsEnabledChanged**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerCommandBehavior.IsEnabledChanged) events to update your own UI to match the SMTC as commands are enabled or disabled by accessing the [**IsEnabled**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerCommandBehavior.IsEnabled) of the [**MediaPlaybackCommandManagerCommandBehavior**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerCommandBehavior) passed into the handler.

[!code-cs[IsEnabledChanged](./code/SMTC_RS1/cs/MainPage.xaml.cs#SnippetIsEnabledChanged)]

In some cases, you may want to completely override the behavior of an SMTC command. The example below illustrates a scenario where an app uses the *Next* and *Previous* commands to switch between internet radio stations instead of skipping between tracks in the current playlist. As in the previous example, a handler is registered for when a command is received, in this case it is the [**PreviousReceived**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager.PreviousReceived) event.

[!code-cs[AddPreviousHandler](./code/SMTC_RS1/cs/MainPage.xaml.cs#SnippetAddPreviousHandler)]

In the **PreviousReceived** handler, first a [**Deferral**](https://msdn.microsoft.com/library/windows/apps/Windows.Foundation.Deferral) is obtained by calling the  [**GetDeferral**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerPreviousReceivedEventArgs.GetDeferral) of the [**MediaPlaybackCommandManagerPreviousReceivedEventArgs**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerPreviousReceivedEventArgs) passed into the handler. This tells the system to wait for until the deferall is complete before executing the command. This is extremely important if you are going to make asynchronous calls in the handler. At this point, the example calls a custom method that returns a **MediaPlaybackItem** representing the previous radio station.

Next, the [**Handled**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManagerPreviousReceivedEventArgs.Handled) property is checked to make sure that the event wasn't already handled by another handler. If not, the **Handled** property is set to true. This lets the SMTC, and any other subscribed handlers, know that they should take no action to execute this command because it has already been handled. The code then sets the new source for the media player and starts the player.

Finally, [**Complete**](https://msdn.microsoft.com/library/windows/apps/Windows.Foundation.Deferral.Complete) is called on the deferral object to let the system know that you are done processing the command.

[!code-cs[PreviousReceived](./code/SMTC_RS1/cs/MainPage.xaml.cs#SnippetPreviousReceived)]
             	
## Manual control of the SMTC
As mentioned previously in this article, the SMTC will automatically detect and display information for every instance of **MediaPlayer** that your app creates. If you want to use multiple instances of **MediaPlayer** but want the SMTC to provide a single entry for your app, then you must manually control the behavior of the SMTC instead of relying on automatic integration. Also, if you are using [**MediaTimelineController**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaTimelineController) to control one or more media players, you must use manual SMTC integration. Also, if your app uses an API other than **MediaPlayer**, such as the [**AudioGraph**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioGraph) class, to play media, you must implement manual SMTC integration for the user to use the SMTC to control your app. For information on how to manually control the SMTC, see [Manual control of the System Media Transport Controls](system-media-transport-controls.md).



## Related topics
* [Media playback](media-playback.md)
* [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md)
* [Manual control of the System Media Transport Controls](system-media-transport-controls.md)
* [System Media Tranport Controls sample on github](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/SystemMediaTransportControls)
 

 




