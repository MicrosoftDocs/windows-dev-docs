---
ms.assetid: b7333924-d641-4ba5-92a2-65925b44ccaa
description: This article shows you how to play media while your app is running in the background.
title: Play media in the background
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Play media in the background
This article shows you how to configure your app so that media continues to play when your app moves from the foreground to the background. This means that even after the user has minimized your app, returned to the home screen, or has navigated away from your app in some other way, your app can continue to play audio. 

Scenarios for background audio playback include:

-   **Long-running playlists:** The user briefly brings up a foreground app to select and start a playlist, after which the user expects the playlist to continue playing in the background.

-   **Using task switcher:** The user briefly brings up a foreground app to start playing audio, then switches to another open app using the task switcher. The user expects the audio to continue playing in the background.

The background audio implementation described in this article will allow your app to run universally on all Windows devices including Mobile, Desktop, and Xbox.

> [!NOTE]
> The code in this article was adapted from the UWP [Background Audio sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundMediaPlayback).

## Explanation of one-process model
With Windows 10, version 1607, a new single-process model has been introduced that greatly simplifies the process of enabling background audio. Previously, your app was required to manage a background process in addition to your foreground app and then manually communicate state changes between the two processes. Under the new model, you simply add the background audio capability to your app manifest, and your app will automatically continue playing audio when it moves to the background. Two new application lifecycle events, [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) and [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground) let your app know when it is entering and leaving the background. When your app moves into the transitions to or from the background, the memory constraints enforced by the system may change, so you can use these events to check your current memory consumption and free up resources in order to stay below the limit.

By eliminating the complex cross-process communication and state management, the new model allows you to implement background audio much more quickly with a significant reduction in code. However, the two-process model is still supported in the current release for backwards compatibility. For more information, see [Legacy background audio model](legacy-background-media-playback.md).

## Requirements for background audio
Your app must meet the following requirements for audio playback while your app is in the background.

* Add the **Background Media Playback** capability to your app manifest, as described later in this article.
* If your app disables the automatic integration of **MediaPlayer** with the System Media Transport Controls (SMTC), such as by setting the [**CommandManager.IsEnabled**](/uwp/api/windows.media.playback.mediaplaybackcommandmanager.isenabled) property to false, then you must implement manual integration with the SMTC in order to enable background media playback. You must also manually integrate with SMTC if you are using an API other than **MediaPlayer**, such as  [**AudioGraph**](/uwp/api/Windows.Media.Audio.AudioGraph), to play audio if you want to have the audio continue to play when your app moves to the background. The minimum SMTC integration requirements are described in the "Use the system media transport controls for background audio" section of [Manual control of the System Media Transport Controls](system-media-transport-controls.md).
* While your app is in the background, you must stay under the memory usage limits set by the system for background apps. Guidance for managing memory while in the background is provided later in this article.

## Background media playback manifest capability
To enable background audio, you must add the background media playback capability to the app manifest file, Package.appxmanifest. 

**To add capabilities to the app manifest using the manifest designer**

1.  In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2.  Select the **Capabilities** tab.
3.  Select the **Background Media Playback** check box.

To set the capability by manually editing the app manifest xml, first make sure that the *uap3* namespace prefix is defined in the **Package** element. If not, add it as shown below.
```xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  IgnorableNamespaces="uap uap3 mp">
```

Next, add the  *backgroundMediaPlayback* capability to the **Capabilities** element:
```xml
<Capabilities>
    <uap3:Capability Name="backgroundMediaPlayback"/>
</Capabilities>
```

## Handle transitioning between foreground and background
When your app moves from the foreground to the background, the [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) event is raised. And when your app returns to the foreground, the [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground) event is raised. Because these are app lifecycle events, you should register handlers for these events when your app is created. In the default project template, this means adding it to the **App** class constructor in App.xaml.cs. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BackgroundAudio_RS1/cs/App.xaml.cs" id="SnippetRegisterEvents":::

Create a variable to track whether you are currently running in the background.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BackgroundAudio_RS1/cs/App.xaml.cs" id="SnippetDeclareBackgroundMode":::

When the [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) event is raised, set the tracking variable to indicate that you are currently running in the background. You should not perform long-running tasks in the **EnteredBackground** event because this may cause the transition to the background to appear slow to the user.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BackgroundAudio_RS1/cs/App.xaml.cs" id="SnippetEnteredBackground":::

In the [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground) event handler, you should set the tracking variable to indicate that your app is no longer running in the background.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BackgroundAudio_RS1/cs/App.xaml.cs" id="SnippetLeavingBackground":::

### Memory management requirements
The most important part of handling the transition between foreground and background is managing the memory that your app uses. Because running in the background will reduce the memory resources your app is allowed to retain by the system, you should also register for the [**AppMemoryUsageIncreased**](/uwp/api/windows.system.memorymanager.appmemoryusageincreased) and [**AppMemoryUsageLimitChanging**](/uwp/api/windows.system.memorymanager.appmemoryusagelimitchanging) events. When these events are raised, you should check your app's current memory usage and the current limit, and then reduce your memory usage if needed. For information about reducing your memory usage while running in the background, see [Free memory when your app moves to the background](../launch-resume/reduce-memory-usage.md).

## Network availability for background media apps
All network aware media sources, those that are not created from a stream or a file, will keep the network connection active while retrieving remote content, and they release it when they are not. [**MediaStreamSource**](/uwp/api/Windows.Media.Core.MediaStreamSource), specifically, relies on the application to correctly report the correct buffered range to the platform using [**SetBufferedRange**](/uwp/api/windows.media.core.mediastreamsource.setbufferedrange). After the entire content is fully buffered, the network will no longer be reserved on the app’s behalf.

If you need to make network calls that occur in the background when media is not downloading, these must be wrapped in an appropriate task like [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger) or [**TimeTrigger**](/uwp/api/Windows.ApplicationModel.Background.TimeTrigger). For more information, see [Support your app with background tasks](../launch-resume/support-your-app-with-background-tasks.md).

## Related topics
* [Media playback](media-playback.md)
* [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md)
* [Integrate with the System Media Transport Controls](integrate-with-systemmediatransportcontrols.md)
* [Background Audio sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundMediaPlayback)

 

 
