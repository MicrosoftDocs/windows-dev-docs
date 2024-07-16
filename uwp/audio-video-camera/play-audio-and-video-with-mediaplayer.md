---
ms.assetid: 58af5e9d-37a1-4f42-909c-db7cb02a0d12
description: This article shows you how to play media in your Universal Windows app with MediaPlayer.
title: Play audio and video with MediaPlayer
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Play audio and video with MediaPlayer

This article shows you how to play media in your Universal Windows app using the  [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) class. With Windows 10, version 1607, significant improvements were made to the media playback APIs, including a simplified single-process design for background audio, automatic integration with the System Media Transport Controls (SMTC), the ability to synchronize multiple media players, the ability to render video frames to a Windows.UI.Composition surface, and an easy interface for creating and scheduling media breaks in your content. To take advantage of these improvements, the recommended best practice for playing media is to use the **MediaPlayer** class instead of **MediaElement** for media playback. The lightweight XAML control, [**MediaPlayerElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement), has been introduced to allow you render media content in a XAML page. Many of the playback control and status APIs provided by **MediaElement** are now available through the new [**MediaPlaybackSession**](/uwp/api/Windows.Media.Playback.MediaPlaybackSession) object. **MediaElement** continues to function to support backwards compatibility, but no additional features will be added to this class.

This article will walk you through the **MediaPlayer** features that a typical media playback app will use. Note that **MediaPlayer** uses the [**MediaSource**](/uwp/api/Windows.Media.Core.MediaSource) class as a container for all media items. This class allows you to load and play media from many different sources, including local files, memory streams, and network sources, all using the same interface. There are also higher-level classes that work with **MediaSource**, such as [**MediaPlaybackItem**](/uwp/api/Windows.Media.Playback.MediaPlaybackItem) and [**MediaPlaybackList**](/uwp/api/Windows.Media.Playback.MediaPlaybackList), that provide more advanced features like playlists and the ability to manage media sources with multiple audio, video, and metadata tracks. For more information on **MediaSource** and related APIs, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

> [!NOTE] 
> Windows 10 N and Windows 10 KN editions do not include the media features required to use **MediaPlayer** for playback. These features can be installed manually. For more information, see [Media feature pack for Windows 10 N and Windows 10 KN editions](https://support.microsoft.com/help/3010081/media-feature-pack-for-windows-10-n-and-windows-10-kn-editions).

## Play a media file with MediaPlayer  
Basic media playback with **MediaPlayer** is very simple to implement. First, create a new instance of the **MediaPlayer** class. Your app can have multiple **MediaPlayer** instances active at once. Next, set the [**Source**](/uwp/api/windows.media.playback.mediaplayer.source) property of the player to an object that implements the [**IMediaPlaybackSource**](/uwp/api/Windows.Media.Playback.IMediaPlaybackSource), such as a [**MediaSource**](/uwp/api/Windows.Media.Core.MediaSource), a [**MediaPlaybackItem**](/uwp/api/Windows.Media.Playback.MediaPlaybackItem), or a [**MediaPlaybackList**](/uwp/api/Windows.Media.Playback.MediaPlaybackList). In this example, a **MediaSource** is created from a file in the app's local storage, and then a **MediaPlaybackItem** is created from the source and then assigned to the player's **Source** property.

Unlike **MediaElement**, **MediaPlayer** does not automatically begin playback by default. You can start playback by calling [**Play**](/uwp/api/windows.media.playback.mediaplayer.play), by setting the [**AutoPlay**](/uwp/api/windows.media.playback.mediaplayer.autoplay) property to true, or waiting for the user to initiate playback with the built-in media controls.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSimpleFilePlayback":::

When your app is done using a **MediaPlayer**, you should call the [**Close**](/uwp/api/windows.media.playback.mediaplayer.close) method (projected to **Dispose** in C#) to clean up the resources used by the player.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetCloseMediaPlayer":::

## Use MediaPlayerElement to render video in XAML
You can play media in a **MediaPlayer** without displaying it in XAML, but many media playback apps will want to render the media in a XAML page. To do this, use the lightweight [**MediaPlayerElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) control. Like **MediaElement**, **MediaPlayerElement** allows you to specify whether the built-in transport controls should be shown.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml" id="SnippetMediaPlayerElementXAML":::

You can set the **MediaPlayer** instance that the element is bound to by calling [**SetMediaPlayer**](/uwp/api/windows.ui.xaml.controls.mediaplayerelement.setmediaplayer).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSetMediaPlayer":::

You can also set the playback source on the **MediaPlayerElement** and the element will automatically create a new **MediaPlayer** instance that you can access using the [**MediaPlayer**](/uwp/api/windows.ui.xaml.controls.mediaplayerelement.mediaplayer) property.

> [!NOTE]
> Setting MediaPlayerElement properties will set the corresponding properties on its underlying **MediaPlayer**. You have the option to use the underlying **MediaPlayer** directly instead of using MediaPlayerElement properties. Be aware that using **MediaPlayer** directly where an equivalent **MediaPlayerElement** property could otherwise be used can cause unexpected behavior. This is because the MediaPlayerElement is not aware of everything happening to its underlying **MediaPlayer**. For example, if you set the source directly on **MediaPlayer**, then **MediaPlayerElement** [Source](m/uwp/api/windows.ui.xaml.controls.mediaplayerelement.source) property will not reflect the change. For this reason, you must be consistent in using **MediaPlayerElement** properties or directly using the underlying **MediaPlayer**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetGetPlayerFromElement":::

> [!NOTE] 
> If you disable the [**MediaPlaybackCommandManager**](/uwp/api/Windows.Media.Playback.MediaPlaybackCommandManager) of the [**MediaPlayer**](/uwp/api/Windows.Media.Playback.MediaPlayer) by setting [**IsEnabled**](/uwp/api/windows.media.playback.mediaplaybackcommandmanager.isenabled) to false, it will break the link between the **MediaPlayer** the [**TransportControls**](/uwp/api/windows.ui.xaml.controls.mediaplayerelement.transportcontrols) provided by the **MediaPlayerElement**, so the built-in transport controls will no longer automatically control the playback of the player. Instead, you must implement your own controls to control the **MediaPlayer**.

The **MediaPlayer** is detached from **MediaPlayerElement** when the **MediaPlayerElement** is destroyed or when a new **MediaPlayer** is set using [SetMediaPlayer](/uwp/api/windows.ui.xaml.controls.mediaplayerelement.setmediaplayer). When detached, **MediaPlayerElement** treats the underlying **MediaPlayer** differently depending on whether it was created by **MediaPlayerElement** or set using **SetMediaPlayer**.

If the **MediaPlayer** was created by **MediaPlayerElement**, it will properly [Close](/uwp/api/windows.media.playback.mediaplayer.close) the **MediaPlayer** for you. If the **MediaPlayer** was set on **MediaPlayerElement** using **SetMediaPlayer**, you are responsible for ensuring the **MediaPlayer** is properly closed. Failing to do so may result in fatal playback errors in **MediaPlayer**. The following code snippet shows how to properly detach and close in code.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetDisposeMediaPlayer":::

## Common MediaPlayer tasks
This section shows you how to use some of the features of the **MediaPlayer**.

### Set the audio category
Set the [**AudioCategory**](/uwp/api/windows.media.playback.mediaplayer.audiocategory) property of a **MediaPlayer** to one of the values of the [**MediaPlayerAudioCategory**](/uwp/api/Windows.Media.Playback.MediaPlayerAudioCategory) enumeration to let the system know what kind of media you are playing. Games should categorize their music streams as **GameMedia** so that game music mutes automatically if another application plays music in the background. Music or video applications should categorize their streams as **Media** or **Movie** so they will take priority over **GameMedia** streams.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSetAudioCategory":::

### Output to a specific audio endpoint
By default, the audio output from a **MediaPlayer** is routed to the default audio endpoint for the system, but you can specify a specific audio endpoint that the **MediaPlayer** should use for output. In the example below, [**MediaDevice.GetAudioRenderSelector**](/uwp/api/windows.media.devices.mediadevice.getaudiorenderselector) returns a string that uniquely identifies the audio render category of devices. Next, the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) method [**FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) is called to get a list of all available devices of the selected type. You may programmatically determine which device you want to use or add the returned devices to a [**ComboBox**](/uwp/api/Windows.UI.Xaml.Controls.ComboBox) to allow the user to select a device.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSetAudioEndpointEnumerate":::

In the [**SelectionChanged**](/uwp/api/windows.ui.xaml.controls.primitives.selector.selectionchanged) event for the devices combo box, the [**AudioDevice**](/uwp/api/windows.media.playback.mediaplayer.audiodevice) property of the **MediaPlayer** is set to the selected device, which was stored in the [**Tag**](/uwp/api/windows.ui.xaml.frameworkelement.tag) property of the **ComboBoxItem**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSetAudioEndpontSelectionChanged":::

### Playback session
As described previously in this article, many of the functions that are exposed by the **MediaElement** class have been moved to the [**MediaPlaybackSession**](/uwp/api/Windows.Media.Playback.MediaPlaybackSession) class. This includes information about the playback state of the player, such as the current playback position, whether the player is paused or playing, and the current playback speed. **MediaPlaybackSession** also provides several events to notify you when the state changes, including the current buffering and download status of content being played and the natural size and aspect ratio of the currently playing video content.

The following example shows you how to implement a button click handler that skips 10 seconds forward in the content. First, the **MediaPlaybackSession** object for the player is retrieved with the [**PlaybackSession**](/uwp/api/windows.media.playback.mediaplayer.playbacksession) property. Next the [**Position**](/uwp/api/windows.media.playback.mediaplaybacksession.position) property is set to the current playback position plus 10 seconds.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSkipForwardClick":::

The next example illustrates using a toggle button to toggle between normal playback speed and 2X speed by setting the [**PlaybackRate**](/uwp/api/windows.media.playback.mediaplaybacksession.playbackrate) property of the session.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSpeedChecked":::

Starting with Windows 10, version 1803, you can set the rotation with which video is presented in the **MediaPlayer** in increments of 90 degrees.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSetRotation":::

### Detect expected and unexpected buffering
The **MediaPlaybackSession** object described in the previous section provides two events for detecting when the currently playing media file begins and ends buffering, **[BufferingStarted](/uwp/api/windows.media.playback.mediaplaybacksession.BufferingStarted)** and **[BufferingEnded](/uwp/api/windows.media.playback.mediaplaybacksession.BufferingEnded)**. This allows you to update your UI to show the user that buffering is occurring. Initial buffering is expected when a media file is first opened or when the user switches to a new item in a playlist. Unexpected buffering can occur when the network speed degrades or if the content management system providing the content experiences technical issues. Starting with RS3, you can use the **BufferingStarted** event to determine if the buffering event is expected or if it is unexpected and interrupting playback. You can use this information as telemetry data for your app or media delivery service. 

Register handlers for the **BufferingStarted** and **BufferingEnded** events to receive buffering state notifications.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetRegisterBufferingHandlers":::

In the **BufferingStarted** event handler, cast the event args passed into the event to a **[MediaPlaybackSessionBufferingStartedEventArgs](/uwp/api/windows.media.playback.mediaplaybacksessionbufferingstartedeventargs)** object and check the **[IsPlaybackInterruption](/uwp/api/windows.media.playback.mediaplaybacksessionbufferingstartedeventargs.IsPlaybackInterruption)** property. If this value is true, the buffering that triggered the event is unexpected and interrupting playback. Otherwise, it is expected initial buffering. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetBufferingHandlers":::


### Pinch and zoom video
**MediaPlayer** allows you to specify the source rectangle within video content that should be rendered, effectively allowing you to zoom into video. The rectangle you specify is relative to a normalized rectangle (0,0,1,1) where 0,0 is the upper left hand of the frame and 1,1 specifies the full width and height of the frame. So, for example, to set the zoom rectangle so that the top-right quadrant of the video is rendered, you would specify the rectangle (.5,0,.5,.5).  It is important that you check your values to make sure that your source rectangle is within the (0,0,1,1) normalized rectangle. Attempting to set a value outside of this range will cause an exception to be thrown.

To implement pinch and zoom using multi-touch gestures, you must first specify which gestures you want to support. In this example, scale and translate gestures are requested. The [**ManipulationDelta**](/uwp/api/windows.ui.xaml.uielement.manipulationdelta) event is raised when one of the subscribed gestures occurs. The [**DoubleTapped**](/uwp/api/windows.ui.xaml.uielement.doubletapped) event will be used to reset the zoom to the full frame. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetRegisterPinchZoomEvents":::

Next, declare a **Rect** object that will store the current zoom source rectangle.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetDeclareSourceRect":::

The **ManipulationDelta** handler adjusts either the scale or the translation of the zoom rectangle. If the delta scale value is not 1, it means that the user performed a pinch gesture. If the value is greater than 1, the source rectangle should be made smaller to zoom into the content. If the value is less than 1, then the source rectangle should be made bigger to zoom out. Before setting the new scale values, the resulting rectangle is checked to make sure it lies entirely within the (0,0,1,1) limits.

If the scale value is 1, then the translation gesture is handled. The rectangle is simply translated by the number of pixels in gesture divided by the width and height of the control. Again, the resulting rectangle is checked to make sure it lies within the (0,0,1,1) bounds.

Finally, the [**NormalizedSourceRect**](/uwp/api/windows.media.playback.mediaplaybacksession.normalizedsourcerect) of the **MediaPlaybackSession** is set to the newly adjusted rectangle, specifying the area within the video frame that should be rendered.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetManipulationDelta":::

In the [**DoubleTapped**](/uwp/api/windows.ui.xaml.uielement.doubletapped) event handler, the source rectangle is set back to (0,0,1,1) to cause the entire video frame to be rendered.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetDoubleTapped":::

**NOTE** This section describes touch input. Touchpad sends pointer events and will not send Manipulation events.

### Handling policy-based playback degradation

In some circumstances the system may degrade the playback of a media item, such as reducing the resolution (constriction), based on a policy rather than a performance issue. For example, video may be degraded by the system if it is being played using an unsigned video driver. You can call [**MediaPlaybackSession.GetOutputDegradationPolicyState**](/uwp/api/windows.media.playback.mediaplaybacksession.getoutputdegradationpolicystate#Windows_Media_Playback_MediaPlaybackSession_GetOutputDegradationPolicyState) to determine if and why this policy-based degradation is occurring and alert the user or record the reason for telemetry purposes.

The following example shows an implementation of a handler for the **MediaPlayer.MediaOpened** event that is raised when the player opens a new media item. **GetOutputDegradationPolicyState** is called on the **MediaPlayer** passed into the handler. The value of [**VideoConstrictionReason**](/uwp/api/windows.media.playback.mediaplaybacksessionoutputdegradationpolicystate.videoconstrictionreason#Windows_Media_Playback_MediaPlaybackSessionOutputDegradationPolicyState_VideoConstrictionReason) indicates the policy reason that the video is constricted. If the value isn't **None**, this example logs the degradation reason for telemetry purposes. This example also shows setting the bitrate of the **AdaptiveMediaSource** currently being played to the lowest bandwidth to save data usage, since the video is constricted and won't be displayed at high resolution anyway. For more information on using **AdaptiveMediaSource**, see [Adaptive streaming](adaptive-streaming.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetPolicyDegradation":::
		
## Use MediaPlayerSurface to render video to a Windows.UI.Composition surface
Starting with Windows 10, version 1607, you can use **MediaPlayer** to render video to an [**ICompositionSurface**](/uwp/api/Windows.UI.Composition.ICompositionSurface), which allows the player to interoperate with the APIs in the [**Windows.UI.Composition**](/uwp/api/Windows.UI.Composition) namespace. The composition framework allows you to work with graphics in the visual layer between XAML and the low-level DirectX graphics APIs. This enables scenarios like rendering video into any XAML control. For more information on using the composition APIs, see [Visual Layer](../composition/visual-layer.md).

The following example illustrates how to render video player content onto a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) control. The media player-specific calls in this example are [**SetSurfaceSize**](/uwp/api/windows.media.playback.mediaplayer.setsurfacesize) and [**GetSurface**](/uwp/api/windows.media.playback.mediaplayer.getsurface). **SetSurfaceSize** tells the system the size of the buffer that should be allocated for rendering content. **GetSurface** takes a [**Compositor**](/uwp/api/Windows.UI.Composition.Compositor) as an argument and retreives an instance of the [**MediaPlayerSurface**](/uwp/api/Windows.Media.Playback.MediaPlayerSurface) class. This class provides access to the **MediaPlayer** and **Compositor** used to create the surface and exposes the surface itself through the [**CompositionSurface**](/uwp/api/windows.media.playback.mediaplayersurface.compositionsurface) property.

The rest of the code in this example creates a [**SpriteVisual**](/uwp/api/Windows.UI.Composition.SpriteVisual) to which the video is rendered and sets the size to the size of the canvas element that will display the visual. Next a [**CompositionBrush**](/uwp/api/Windows.UI.Composition.CompositionBrush) is created from the [**MediaPlayerSurface**](/uwp/api/Windows.Media.Playback.MediaPlayerSurface) and assigned to the [**Brush**](/uwp/api/windows.ui.composition.spritevisual.brush) property of the visual. Next a [**ContainerVisual**](/uwp/api/Windows.UI.Composition.ContainerVisual) is created and the **SpriteVisual** is inserted at the top of its visual tree. Finally, [**SetElementChildVisual**](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.setelementchildvisual) is called to assign the container visual to the **Canvas**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetCompositor":::
		
## Use MediaTimelineController to synchronize content across multiple players.
As discussed previously in this article, your app can have several **MediaPlayer** objects active at a time. By default, each **MediaPlayer** you create operates independently. For some scenarios, such as synchronizing a commentary track to a video, you may want to synchronize the player state, playback position, and playback speed of multiple players. Starting with Windows 10, version 1607, you can implement this behavior by using the [**MediaTimelineController**](/uwp/api/Windows.Media.MediaTimelineController) class.

### Implement playback controls
The following example shows how to use a **MediaTimelineController** to control two instances of **MediaPlayer**. First, each instance of the **MediaPlayer** is instantiated and the **Source** is set to a media file. Next, a new **MediaTimelineController** is created. For each **MediaPlayer**, the [**MediaPlaybackCommandManager**](/uwp/api/Windows.Media.Playback.MediaPlaybackCommandManager) associated with each player is disabled by setting the [**IsEnabled**](/uwp/api/windows.media.playback.mediaplaybackcommandmanager.isenabled) property to false. And then the [**TimelineController**](/uwp/api/windows.media.playback.mediaplayer.timelinecontroller) property is set to the timeline controller object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetDeclareMediaTimelineController":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSetTimelineController":::

**Caution** The [**MediaPlaybackCommandManager**](/uwp/api/Windows.Media.Playback.MediaPlaybackCommandManager) provides automatic integration between **MediaPlayer** and the System Media Transport Controls (SMTC), but this automatic integration can't be used with media players that are controlled with a **MediaTimelineController**. Therefore you must disable the command manager for the media player before setting the player's timeline controller. Failure to do so will result in an exception being thrown with the following message: "Attaching Media Timeline Controller is blocked because of the current state of the object." For more information on media player integration with the SMTC, see [Integrate with the System Media Transport Controls](integrate-with-systemmediatransportcontrols.md). If you are using a **MediaTimelineController** you can still control the SMTC manually. For more information, see [Manual control of the System Media Transport Controls](system-media-transport-controls.md).

Once you have attached a **MediaTimelineController** to one or more media players, you can control the playback state by using the methods exposed by the controller. The following example calls [**Start**](/uwp/api/windows.media.mediatimelinecontroller.start) to begin playback of all associated media players at the beginning of the media.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetPlayButtonClick":::

This example illustrates pausing and resuming all of the attached media players.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetPauseButtonClick":::

To fast-forward all connected media players, set the playback speed to a value greater that 1.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetFastForwardButtonClick":::

The next example shows how to use a **Slider** control to show the current playback position of the timeline controller relative to the duration of the content of one of the connected media players. First, a new **MediaSource** is created and a handler for the [**OpenOperationCompleted**](/uwp/api/windows.media.core.mediasource.openoperationcompleted) of the media source is registered. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetCreateSourceWithOpenCompleted":::

The **OpenOperationCompleted** handler is used as an opportunity to discover the duration of the media source content. Once the duration is determined, the maximum value of the **Slider** control is set to the total number of seconds of the media item. The value is set inside a call to [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) to make sure it is run on the UI thread.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetDeclareDuration":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetOpenCompleted":::

Next, a handler for the timeline controller's  [**PositionChanged**](/uwp/api/windows.media.mediatimelinecontroller.positionchanged) event is registered. This is called periodically by the system, approximately 4 times per second.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetRegisterPositionChanged":::

In the handler for **PositionChanged**, the slider value is updated to reflect the current position of the timeline controller.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetPositionChanged":::

### Offset the playback position from the timeline position
In some cases you may want the playback position of one or more media players associated with a timeline controller to be offset from the other players. You can do this by setting the [**TimelineControllerPositionOffset**](/uwp/api/windows.media.playback.mediaplayer.timelinecontrollerpositionoffset) property of the **MediaPlayer** object you want to be offset. The following example uses the durations of the content of two media players to set the minimum and maximum values of two slider control to plus and minus the length of the item.  

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetOffsetSliders":::

In the [**ValueChanged**](/uwp/api/windows.ui.xaml.controls.primitives.rangebase.valuechanged) event for each slider, the **TimelineControllerPositionOffset** for each player is set to the corresponding value.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetTimelineOffset":::

Note that if the offset value of a player maps to a negative playback position, the clip will remain paused until the offset reaches zero and then playback will begin. Likewise, if the offset value maps to a playback position greater than the duration of the media item, the final frame will be shown, just as it does when a single media player reached the end of its content.

## Play spherical video with MediaPlayer
Starting with Windows 10, version 1703, **MediaPlayer** supports equirectangular projection for spherical video playback. Spherical video content is no different from regular, flat video in that **MediaPlayer** will render the video as long as the video encoding is supported. For spherical video that contains a metadata tag that specifies that the video uses equirectangular projection, **MediaPlayer** can render the video using a specified field-of-view and view orientation. This enables scenarios such as virtual reality video playback with a head-mounted display or simply allowing the user to pan around within spherical video content using the mouse or keyboard input.

To play back spherical video, use the steps for playing back video content described previously in this article. The one additional step is to register a handler for the [**MediaPlayer.MediaOpened**](/uwp/api/Windows.Media.Playback.MediaPlayer#Windows_Media_Playback_MediaPlayer_MediaOpened) event. This event gives you an opportunity to enable and control the spherical video playback parameters.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetOpenSphericalVideo":::

In the **MediaOpened** handler, first check the frame format of the newly opened media item by checking the [**PlaybackSession.SphericalVideoProjection.FrameFormat**](/uwp/api/windows.media.playback.mediaplaybacksphericalvideoprojection.FrameFormat) property. If this value is [**SphericaVideoFrameFormat.Equirectangular**](/uwp/api/windows.media.mediaproperties.sphericalvideoframeformat), then the system can automatically project the video content. First, set the [**PlaybackSession.SphericalVideoProjection.IsEnabled**](/uwp/api/windows.media.playback.mediaplaybacksphericalvideoprojection.IsEnabled) property to **true**. You can also adjust properties such as the view orientation and field of view that the media player will use to project the video content. In this example, the field of view is set to a wide value of 120 degrees by setting the [**HorizontalFieldOfViewInDegrees**](/uwp/api/windows.media.playback.mediaplaybacksphericalvideoprojection.HorizontalFieldOfViewInDegrees) property.

If the video content is spherical, but is in a format other than equirectangular, you can implement your own projection algorithm using the media player's frame server mode to receive and process individual frames.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSphericalMediaOpened":::

The following example code illustrates how to adjust the spherical video view orientation using the left and right arrow keys.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSphericalOnKeyDown":::

If your app supports playlists of video, you may want to identify playback items that contain spherical video in your UI. Media playlists are discussed in detail in the article, [Media items, playlists, and tracks](media-playback-with-mediasource.md). The following example shows creating a new playlist, adding an item, and registering a handler for the [**MediaPlaybackItem.VideoTracksChanged**](/uwp/api/windows.media.playback.mediaplaybackitem.VideoTracksChanged) event, which occurs when the video tracks for a media item are resolved.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSphericalList":::

In the **VideoTracksChanged** event handler, get the encoding properties for any added video tracks by calling [**VideoTrack.GetEncodingProperties**](/uwp/api/windows.media.core.videotrack.GetEncodingProperties). If the [**SphericalVideoFrameFormat**](/uwp/api/windows.media.mediaproperties.videoencodingproperties.SphericalVideoFrameFormat) property of the encoding properties is a value other than [**SphericaVideoFrameFormat.None**](/uwp/api/windows.media.mediaproperties.sphericalvideoframeformat), then the video track contains spherical video and you can update your UI accordingly if you choose.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSphericalTracksChanged":::

## Use MediaPlayer in frame server mode
Starting with Windows 10, version 1703, you can use **MediaPlayer** in frame server mode. In this mode, the **MediaPlayer** does not automatically render frames to an associated **MediaPlayerElement**. Instead, your app copies the current frame from the **MediaPlayer** to an object that implements [**IDirect3DSurface**](/uwp/api/windows.graphics.directx.direct3d11.idirect3dsurface). The primary scenario this feature enables is using pixel shaders to process video frames provided by the **MediaPlayer**. Your app is responsible for displaying each frame after processing, such as by showing the frame in a XAML [**Image**](/uwp/api/windows.ui.xaml.controls.image) control.

In the following example, a new **MediaPlayer** is initialized and video content is loaded. Next, a handler for [**VideoFrameAvailable**](/uwp/api/windows.media.playback.mediaplayer.VideoFrameAvailable) is registered. Frame server mode is enabled by setting the **MediaPlayer** object's [**IsVideoFrameServerEnabled**](/uwp/api/windows.media.playback.mediaplayer.IsVideoFrameServerEnabled) property to **true**. Finally, media playback is started with a call to [**Play**](/uwp/api/windows.media.playback.mediaplayer.Play).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetFrameServerInit":::

The next example shows a handler for **VideoFrameAvailable** that uses [Win2D](https://github.com/Microsoft/Win2D) to add a simple blur effect to each frame of a video and then displays the processed frames in a XAML [Image](/uwp/api/windows.ui.xaml.controls.image) control.

Whenever the **VideoFrameAvailable** handler is called, the [**CopyFrameToVideoSurface**](/uwp/api/windows.media.playback.mediaplayer.copyframetovideosurface) method is used to copy the contents of the frame to an [**IDirect3DSurface**](/uwp/api/windows.graphics.directx.direct3d11.idirect3dsurface). You can also use [**CopyFrameToStereoscopicVideoSurfaces**](/uwp/api/windows.media.playback.mediaplayer.copyframetostereoscopicvideosurfaces) to copy 3D content into two surfaces, for processing the left eye and right eye content separately. To get an object that implements **IDirect3DSurface**  this example creates a [**SoftwareBitmap**](/uwp/api/windows.graphics.imaging.softwarebitmap) and then uses that object to create a Win2D **CanvasBitmap**, which implements the necessary interface. A **CanvasImageSource** is a Win2D object that can be used as the source for an **Image** control, so a new one is created and set as the source for the **Image** in which the content will be displayed. Next, a **CanvasDrawingSession** is created. This is used by Win2D to render the blur effect.

Once all of the necessary objects have been instantiated, **CopyFrameToVideoSurface** is called, which copies the current frame from the **MediaPlayer** into the **CanvasBitmap**. Next, a Win2D **GaussianBlurEffect** is created, with the **CanvasBitmap** set as the source of the operation. Finally, **CanvasDrawingSession.DrawImage** is called to draw the source image, with the blur effect applied, into the **CanvasImageSource** that has been associated with **Image** control, causing it to be drawn in the UI.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetVideoFrameAvailable":::

For more information on Win2D, see the [Win2D GitHub repository](https://github.com/Microsoft/Win2D). To try out the sample code shown above, you will need to add the Win2D NuGet package to your project with the following instructions.

**To add the Win2D NuGet package to your effect project**

1.  In **Solution Explorer**, right-click your project and select **Manage NuGet Packages**.
2.  At the top of the window, select the **Browse** tab.
3.  In the search box, enter **Win2D**.
4.  Select **Win2D.uwp**, and then select **Install** in the right pane.
5.  The **Review Changes** dialog shows you the package to be installed. Click **OK**.
6.  Accept the package license.

## Detect and respond to audio level changes by the system
Starting with Windows 10, version 1803, your app can detect when the system lowers or mutes the audio level of a currently playing **MediaPlayer**. For example, the system may lower, or "duck", the audio playback level when an alarm is ringing. The system will mute your app when it goes into the background if your app has not declared the *backgroundMediaPlayback* capability in the app manifest. The [**AudioStateMonitor**](/uwp/api/windows.media.audio.audiostatemonitor) class allows you to register to receive an event when the system modifies the volume of an audio stream. Access the **AudioStateMonitor** property of a **MediaPlayer** and register a handler for the [**SoundLevelChanged**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevelchanged) event to be notified when the audio level for that **MediaPlayer** is changed by the system.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetRegisterAudioStateMonitor":::

When handling the **SoundLevelChanged** event, you may take different actions depending on the type of content being played. If you are currently playing music, then you may want to let the music continue to play while the volume is ducked. If you are playing a podcast, however, you likely want to pause playback while the audio is ducked so the user doesn't miss any of the content.

This example declares a variable to track whether the currently playing content is a podcast, it is assumed that you set this to the appropriate value when selecting the content for the **MediaPlayer**. We also create a class variable to track when we pause playback programmatically when the audio level changes.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetAudioStateVars":::

In the **SoundLevelChanged** event handler, check the [**SoundLevel**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevel) property of the **AudioStateMonitor** sender to determine the new sound level. This example checks to see if the new sound level is full volume, meaning the system has stopped muting or ducking the volume, or if the sound level has been lowered but is playing non-podcast content. If either of these are true and the content was previously paused programmatically, playback is resumed. If the new sound level is muted or if the current content is a podcast and the sound level is low, playback is paused, and the variable is set to track that the pause was initiated programmatically.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetSoundLevelChanged":::

The user may decide that they want to pause or continue playback, even if the audio is ducked by the system. This example shows event handlers for a play and a pause button. In the pause button click handler is paused, if playback had already been paused programmatically, then we update the variable to indicate that the user has paused the content. In the play button click handler, we resume playback and clear our tracking variable.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaPlayer_RS1/cs/MainPage.xaml.cs" id="SnippetButtonUserClick":::

## Related topics
* [Media playback](media-playback.md)
* [Media items, playlists, and tracks](media-playback-with-mediasource.md)
* [Integrate with the System Media Transport Controls](integrate-with-systemmediatransportcontrols.md)
* [Create, schedule, and manage media breaks](create-schedule-and-manage-media-breaks.md)
* [Play media in the background](background-audio.md)





 

 
