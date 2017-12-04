---
author: drewbatgit
ms.assetid: EFCF84D0-2F4C-454D-97DA-249E9EAA806C
description: The SystemMediaTransportControls class enables your app to use the system media transport controls that are built into Windows and to update the metadata that the controls display about the media your app is currently playing.
title: Manual control of the System Media Transport Controls
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Manual control of the System Media Transport Controls


Starting with Windows 10, version 1607, UWP apps that use the [**MediaPlayer**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer) class to play media are automatically integrated with the System Media Transport Controls (SMTC) by default. This is the recommended way of interacting with the SMTC for most scenarios. For more information on customizing the SMTC's default integration with **MediaPlayer**, see [Integrate with the System Media Transport Controls](integrate-with-systemmediatransportcontrols.md).

There are a few scenarios where you may need to implement manual control of the SMTC. These include if you are using a [**MediaTimelineController**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaTimelineController) to control playback of one or more media players. Or if you are using multiple media players and only want to have one instance of SMTC for your app. You must manually control the SMTC if you are using [**MediaElement**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaElement) to play media.

## Set up transport controls
If you are using **MediaPlayer** to play media, you can get an instance of the [**SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.SystemMediaTransportControls) class by accessing the [**MediaPlayer.SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer.SystemMediaTransportControls) property. If you are going to manually control the SMTC, you should disable the automatic integration provided by **MediaPlayer** by setting the [**CommandManager.IsEnabled**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager.IsEnabled) property to false.

> [!NOTE] 
> If you disable the [**MediaPlaybackCommandManager**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager) of the [**MediaPlayer**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlayer) by setting [**IsEnabled**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackCommandManager.IsEnabled) to false, it will break the link between the **MediaPlayer** the [**TransportControls**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement.TransportControls) provided by the **MediaPlayerElement**, so the built-in transport controls will no longer automatically control the playback of the player. Instead, you must implement your own controls to control the **MediaPlayer**.

[!code-cs[InitSMTCMediaPlayer](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetInitSMTCMediaPlayer)]

You can also get an instance of the [**SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/dn278677) by calling [**GetForCurrentView**](https://msdn.microsoft.com/library/windows/apps/dn278708). You must get the object with this method if you are using **MediaElement** to play media.

[!code-cs[InitSMTCMediaElement](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetInitSMTCMediaElement)]

Enable the buttons that your app will use by setting the corresponding "is enabled" property of the **SystemMediaTransportControls** object, such as [**IsPlayEnabled**](https://msdn.microsoft.com/library/windows/apps/dn278714), [**IsPauseEnabled**](https://msdn.microsoft.com/library/windows/apps/dn278713), [**IsNextEnabled**](https://msdn.microsoft.com/library/windows/apps/dn278712), and [**IsPreviousEnabled**](https://msdn.microsoft.com/library/windows/apps/dn278715). See the **SystemMediaTransportControls** reference documentation for a complete list of available controls.

[!code-cs[EnableContols](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetEnableContols)]

Register a handler for the [**ButtonPressed**](https://msdn.microsoft.com/library/windows/apps/dn278706) event to receive notifications when the user presses a button.

[!code-cs[RegisterButtonPressed](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetRegisterButtonPressed)]

## Handle system media transport controls button presses

The [**ButtonPressed**](https://msdn.microsoft.com/library/windows/apps/dn278706) event is raised by the system transport controls when one of the enabled buttons is pressed. The [**Button**](https://msdn.microsoft.com/library/windows/apps/dn278685) property of the [**SystemMediaTransportControlsButtonPressedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn278683) passed into the event handler is a member of the [**SystemMediaTransportControlsButton**](https://msdn.microsoft.com/library/windows/apps/dn278681) enumeration that indicates which of the enabled buttons was pressed.

In order to update objects on the UI thread from the [**ButtonPressed**](https://msdn.microsoft.com/library/windows/apps/dn278706) event handler, such as a [**MediaElement**](https://msdn.microsoft.com/library/windows/apps/br242926) object, you must marshal the calls through the [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211). This is because the **ButtonPressed** event handler is not called from the UI thread and therefore an exception will be thrown if you attempt to modify the UI directly.

[!code-cs[SystemMediaTransportControlsButtonPressed](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetSystemMediaTransportControlsButtonPressed)]

## Update the system media transport controls with the current media status

You should notify the [**SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/dn278677) when the state of the media has changed so that the system can update the controls to reflect the current state. To do this, set the [**PlaybackStatus**](https://msdn.microsoft.com/library/windows/apps/dn278719) property to the appropriate [**MediaPlaybackStatus**](https://msdn.microsoft.com/library/windows/apps/dn278665) value from within the [**CurrentStateChanged**](https://msdn.microsoft.com/library/windows/apps/br227375) event of the [**MediaElement**](https://msdn.microsoft.com/library/windows/apps/br242926), which is raised when the media state changes.

[!code-cs[SystemMediaTransportControlsStateChange](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetSystemMediaTransportControlsStateChange)]

## Update the system media transport controls with media info and thumbnails

Use the [**SystemMediaTransportControlsDisplayUpdater**](https://msdn.microsoft.com/library/windows/apps/dn278686) class to update the media info that is displayed by the transport controls, such as the song title or the album art for the currently playing media item. Get an instance of this class with the [**SystemMediaTransportControls.DisplayUpdater**](https://msdn.microsoft.com/library/windows/apps/dn278707) property. For typical scenarios, the recommended way to pass the metadata is to call [**CopyFromFileAsync**](https://msdn.microsoft.com/library/windows/apps/dn278694), passing in the currently playing media file. The display updater will automatically extract the metadata and thumbnail image from the file.

Call the [**Update**](https://msdn.microsoft.com/library/windows/apps/dn278701) to cause the system media transport controls to update its UI with the new metadata and thumbnail.

[!code-cs[SystemMediaTransportControlsUpdater](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetSystemMediaTransportControlsUpdater)]

If your scenario requires it, you can update the metadata displayed by the system media transport controls manually by setting the values of the [**MusicProperties**](https://msdn.microsoft.com/library/windows/apps/dn278696), [**ImageProperties**](https://msdn.microsoft.com/library/windows/apps/dn278695), or [**VideoProperties**](https://msdn.microsoft.com/library/windows/apps/dn278702) objects exposed by the [**DisplayUpdater**](https://msdn.microsoft.com/library/windows/apps/dn278707) class.

[!code-cs[SystemMediaTransportControlsUpdaterManual](./code/SMTCWin10/cs/MainPage.xaml.cs#SystemMediaTransportControlsUpdaterManual)]

## Update the system media transport controls timeline properties

The system transport controls display information about the timeline of the currently playing media item, including the current playback position, the start time, and the end time of the media item. To update the system transport controls timeline properties, create a new [**SystemMediaTransportControlsTimelineProperties**](https://msdn.microsoft.com/library/windows/apps/mt218746) object. Set the properties of the object to reflect the current state of the playing media item. Call [**SystemMediaTransportControls.UpdateTimelineProperties**](https://msdn.microsoft.com/library/windows/apps/mt218760) to cause the controls to update the timeline.

[!code-cs[UpdateTimelineProperties](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetUpdateTimelineProperties)]

-   You must provide a value for the [**StartTime**](https://msdn.microsoft.com/library/windows/apps/mt218751), [**EndTime**](https://msdn.microsoft.com/library/windows/apps/mt218747) and [**Position**](https://msdn.microsoft.com/library/windows/apps/mt218755) in order for the system controls to display a timeline for your playing item.

-   [**MinSeekTime**](https://msdn.microsoft.com/library/windows/apps/mt218749) and [**MaxSeekTime**](https://msdn.microsoft.com/library/windows/apps/mt218748) allow you to specify the range within the timeline that the user can seek. A typical scenario for this is to allow content providers to include advertisement breaks in their media.

    You must set [**MinSeekTime**](https://msdn.microsoft.com/library/windows/apps/mt218749) and [**MaxSeekTime**](https://msdn.microsoft.com/library/windows/apps/mt218748) in order for the [**PositionChangeRequest**](https://msdn.microsoft.com/library/windows/apps/mt218755) to be raised.

-   It is recommended that you keep the system controls in sync with your media playback by updating these properties approximately every 5 seconds during playback and again whenever the state of playback changes, such as pausing or seeking to a new position.

## Respond to player property changes

There is a set of system transport controls properties that relate to the current state of the media player itself, rather than the state of the playing media item. Each of these properties is matched with an event that is raised when the user adjusts the associated control. These properties and events include:

| Property                                                                  | Event                                                                                                   |
|---------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------|
| [**AutoRepeatMode**](https://msdn.microsoft.com/library/windows/apps/mt218753) | [**AutoRepeatModeChangeRequested**](https://msdn.microsoft.com/library/windows/apps/mt218754) |
| [**PlaybackRate**](https://msdn.microsoft.com/library/windows/apps/mt218756)     | [**PlaybackRateChangeRequested**](https://msdn.microsoft.com/library/windows/apps/mt218757)     |
| [**ShuffleEnabled**](https://msdn.microsoft.com/library/windows/apps/mt218758) | [**ShuffleEnabledChangeRequested**](https://msdn.microsoft.com/library/windows/apps/mt218759) |

 
To handle user interaction with one of these controls, first register a handler for the associated event.

[!code-cs[RegisterPlaybackChangedHandler](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetRegisterPlaybackChangedHandler)]

In the handler for the event, first make sure that the requested value is within a valid and expected range. If it is, set the corresponding property on [**MediaElement**](https://msdn.microsoft.com/library/windows/apps/br242926) and then set the corresponding property on the [**SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/dn278677) object.

[!code-cs[PlaybackChangedHandler](./code/SMTCWin10/cs/MainPage.xaml.cs#SnippetPlaybackChangedHandler)]

-   In order for one of these player property events to be raised, you must set an initial value for the property. For example, [**PlaybackRateChangeRequested**](https://msdn.microsoft.com/library/windows/apps/mt218757) will not be raised until after you have set a value for the [**PlaybackRate**](https://msdn.microsoft.com/library/windows/apps/mt218756) property at least one time.

## Use the system media transport controls for background audio

If you are not using the automatic SMTC integration provided by **MediaPlayer** you must manually integrate with the SMTC to enable background audio. At a minimum, your app must enable the play and pause buttons by setting [**IsPlayEnabled**](https://msdn.microsoft.com/library/windows/apps/dn278714) and [**IsPauseEnabled**](https://msdn.microsoft.com/library/windows/apps/dn278713) to true. Your app must also handle the [**ButtonPressed**](https://msdn.microsoft.com/library/windows/apps/dn278706) event. If your app does not meet these requirements, audio playback will stop when your app moves to the background.

Apps that use the new one-process model for background audio should get an instance of the [**SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/dn278677) by calling [**GetForCurrentView**](https://msdn.microsoft.com/library/windows/apps/dn278708). Apps that use the legacy two-process model for background audio must use [**BackgroundMediaPlayer.Current.SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/dn926635) to get access to the SMTC from their background process.

For more information on playing audio in the background, see [Play media in the background](background-audio.md).

## Related topics
* [Media playback](media-playback.md)
* [Integrate with the System Media Transport Controls](integrate-with-systemmediatransportcontrols.md) 
* [System Media Tranport sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/SystemMediaTransportControls) 

 




