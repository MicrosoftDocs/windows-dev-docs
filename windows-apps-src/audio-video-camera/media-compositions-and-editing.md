---
author: drewbatgit
ms.assetid: C4DB495D-1F91-40EF-A55C-5CABBF3269A2
description: The APIs in the Windows.Media.Editing namespace allow you to quickly develop apps that enable the users to create media compositions from audio and video source files.
title: Media compositions and editing
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Media compositions and editing



This article shows you how to use the APIs in the [**Windows.Media.Editing**](https://msdn.microsoft.com/library/windows/apps/dn640565) namespace to quickly develop apps that enable the users to create media compositions from audio and video source files. Features of the framework include the ability to programmatically append multiple video clips together, add video and image overlays, add background audio, and apply both audio and video effects. Once created, media compositions can be rendered into a flat media file for playback or sharing, but compositions can also be serialized to and deserialized from disk, allowing the user to load and modify compositions that they have previously created. All of this functionality is provided in an easy-to-use Windows Runtime interface that dramatically reduces the amount and complexity of code required to perform these tasks when compared to the low-level [Microsoft Media Foundation](https://msdn.microsoft.com/library/windows/desktop/ms694197) API.

## Create a new media composition

The [**MediaComposition**](https://msdn.microsoft.com/library/windows/apps/dn652646) class is the container for all of the media clips that make up the composition and is responsible for rendering the final composition, loading and saving compositions to disc, and providing a preview stream of the composition so that the user can view it in the UI. To use **MediaComposition** in your app, include the [**Windows.Media.Editing**](https://msdn.microsoft.com/library/windows/apps/dn640565) namespace as well as the [**Windows.Media.Core**](https://msdn.microsoft.com/library/windows/apps/dn278962) namespace that provides related APIs that you will need.

[!code-cs[Namespace1](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetNamespace1)]


The **MediaComposition** object will be accessed from multiple points in your code, so typically you will declare a member variable in which to store it.

[!code-cs[DeclareMediaComposition](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetDeclareMediaComposition)]

The constructor for **MediaComposition** takes no arguments.

[!code-cs[MediaCompositionConstructor](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetMediaCompositionConstructor)]

## Add media clips to a composition

Media compositions typically contain one or more video clips. You can use a [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/hh738369) to allow the user to select a video file. Once the file has been selected, create a new [**MediaClip**](https://msdn.microsoft.com/library/windows/apps/dn652596) object to contain the video clip by calling [**MediaClip.CreateFromFileAsync**](https://msdn.microsoft.com/library/windows/apps/dn652607). Then you add the clip to the **MediaComposition** object's [**Clips**](https://msdn.microsoft.com/library/windows/apps/dn652648) list.

[!code-cs[PickFileAndAddClip](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetPickFileAndAddClip)]

-   Media clips appear in the **MediaComposition** in the same order as they appear in [**Clips**](https://msdn.microsoft.com/library/windows/apps/dn652648) list.

-   A **MediaClip** can only be included in a composition once. Attempting to add a **MediaClip** that is already being used by the composition will result in an error. To reuse a video clip multiple times in a composition, call [**Clone**](https://msdn.microsoft.com/library/windows/apps/dn652599) to create new **MediaClip** objects which can then be added to the composition.

-   Universal Windows apps do not have permission to access the entire file system. The [**FutureAccessList**](https://msdn.microsoft.com/library/windows/apps/br207457) property of the [**StorageApplicationPermissions**](https://msdn.microsoft.com/library/windows/apps/br207456) class allows your app to store a record of a file that has been selected by the user so that you can retain permissions to access the file. The **FutureAccessList** has a maxium of 1000 entries, so your app needs to manage the list to make sure it does not become full. This is especially important if you plan to support loading and modifying previously created compositions.

-   A **MediaComposition** supports video clips in MP4 format.

-   If a video file contains multiple embedded audio tracks, you can select which audio track is used in the composition by setting the [**SelectedEmbeddedAudioTrackIndex**](https://msdn.microsoft.com/library/windows/apps/dn652627) property.

-   Create a **MediaClip** with a single color filling the entire frame by calling [**CreateFromColor**](https://msdn.microsoft.com/library/windows/apps/dn652605) and specifying a color and a duration for the clip.

-   Create a **MediaClip** from an image file by calling [**CreateFromImageFileAsync**](https://msdn.microsoft.com/library/windows/apps/dn652610) and specifying an image file and a duration for the clip.

-   Create a **MediaClip** from a [**IDirect3DSurface**](https://msdn.microsoft.com/library/windows/apps/dn965505) by calling [**CreateFromSurface**](https://msdn.microsoft.com/library/dn764774) and specifying a surface and a duration from the clip.

## Preview the composition in a MediaElement

To enable the user to view the media composition, add a [**MediaPlayerElement**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement) to the XAML file that defines your UI.

[!code-xml[MediaElement](./code/MediaEditing/cs/MainPage.xaml#SnippetMediaElement)]

Declare a member variable of type [**MediaStreamSource**](https://msdn.microsoft.com/library/windows/apps/dn282716).


[!code-cs[DeclareMediaStreamSource](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetDeclareMediaStreamSource)]

Call the **MediaComposition** object's [**GeneratePreviewMediaStreamSource**](https://msdn.microsoft.com/library/windows/apps/dn652674) method to create a **MediaStreamSource** for the composition. Create a [**MediaSource**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Core.MediaSource) object by calling the factory method [**CreateFromMediaStreamSource**](https://msdn.microsoft.com/library/windows/apps/dn930907) and assign it to the [**Source**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement.Source) property of the **MediaPlayerElement**. Now the composition can be viewed in the UI.


[!code-cs[UpdateMediaElementSource](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetUpdateMediaElementSource)]

-   The **MediaComposition** must contain at least one media clip before calling [**GeneratePreviewMediaStreamSource**](https://msdn.microsoft.com/library/windows/apps/dn652674), or the returned object will be null.

-   The **MediaElement** timeline is not automatically updated to reflect changes in the composition. It is recommended that you call both **GeneratePreviewMediaStreamSource** and set the **MediaPlayerElement** **Source** property every time you make a set of changes to the composition and want to update the UI.

It is recommended that you set the **MediaStreamSource** object and the [**Source**](https://msdn.microsoft.com/library/windows/apps/br227419) property of the **MediaPlayerElement** to null when the user navigates away from the page in order to release associated resources.

[!code-cs[OnNavigatedFrom](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetOnNavigatedFrom)]

## Render the composition to a video file

To render a media composition to a flat video file so that it can be shared and viewed on other devices, you will need to use APIs from the [**Windows.Media.Transcoding**](https://msdn.microsoft.com/library/windows/apps/br207105) namespace. To update the UI on the progress of the async operation, you will also need APIs from the [**Windows.UI.Core**](https://msdn.microsoft.com/library/windows/apps/br208383) namespace.

[!code-cs[Namespace2](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetNamespace2)]

After allowing the user to select an output file with a [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871), render the composition to the selected file by calling the **MediaComposition** object's [**RenderToFileAsync**](https://msdn.microsoft.com/library/windows/apps/dn652690). The rest of the code in the following example simply follows the pattern of handling an [**AsyncOperationWithProgress**](https://msdn.microsoft.com/library/windows/desktop/br205807).

[!code-cs[RenderCompositionToFile](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetRenderCompositionToFile)]

-   The [**MediaTrimmingPreference**](https://msdn.microsoft.com/library/windows/apps/dn640561) allows you to prioritize speed of the transcoding operation versus the precision of trimming of adjacent media clips. **Fast** causes transcoding to be faster with lower-precision trimming, **Precise** causes transcoding to be slower but with more precise trimming.

## Trim a video clip

Trim the duration of a video clip in a composition by setting the [**MediaClip**](https://msdn.microsoft.com/library/windows/apps/dn652596) objects [**TrimTimeFromStart**](https://msdn.microsoft.com/library/windows/apps/dn652637) property, the [**TrimTimeFromEnd**](https://msdn.microsoft.com/library/windows/apps/dn652634) property, or both.

[!code-cs[TrimClipBeforeCurrentPosition](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetTrimClipBeforeCurrentPosition)]

-   Your can use any UI that you want to let the user specify the start and end trim values. The example above uses the [**Position**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackSession.Position) property of the [**MediaPlaybackSession**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Playback.MediaPlaybackSession) associated with the **MediaPlayerElement** to first determine which **MediaClip** is playing back at the current position in the composition by checking the [**StartTimeInComposition**](https://msdn.microsoft.com/library/windows/apps/dn652629) and [**EndTimeInComposition**](https://msdn.microsoft.com/library/windows/apps/dn652618). Then the **Position** and **StartTimeInComposition** properties are used again to calculate the amount of time to trim from the beginning of the clip. The **FirstOrDefault** method is an extension method from the **System.Linq** namespace that simplifies the code for selecting items from a list.
-   The [**OriginalDuration**](https://msdn.microsoft.com/library/windows/apps/dn652625) property of the **MediaClip** object lets you know the duration of the media clip without any clipping applied.
-   The [**TrimmedDuration**](https://msdn.microsoft.com/library/windows/apps/dn652631) property lets you know the duration of the media clip after trimming is applied.
-   Specifying a trimming value that is larger than the original duration of the clip does not throw an error. However, if a composition contains only a single clip and that is trimmed to zero length by specifying a large trimming value, a subsequent call to [**GeneratePreviewMediaStreamSource**](https://msdn.microsoft.com/library/windows/apps/dn652674) will return null, as if the composition has no clips.

## Add a background audio track to a composition

To add a background track to a composition, load an audio file and then create a [**BackgroundAudioTrack**](https://msdn.microsoft.com/library/windows/apps/dn652544) object by calling the factory method [**BackgroundAudioTrack.CreateFromFileAsync**](https://msdn.microsoft.com/library/windows/apps/dn652561). Then, add the **BackgroundAudioTrack** to the composition's [**BackgroundAudioTracks**](https://msdn.microsoft.com/library/windows/apps/dn652647) property.

[!code-cs[AddBackgroundAudioTrack](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetAddBackgroundAudioTrack)]

-   A **MediaComposition** supports background audio tracks in the following formats: MP3, WAV, FLAC

-   A background audio track

-   As with video files, you should use the [**StorageApplicationPermissions**](https://msdn.microsoft.com/library/windows/apps/br207456) class to preserve access to files in the composition.

-   As with **MediaClip**, a **BackgroundAudioTrack** can only be included in a composition once. Attempting to add a **BackgroundAudioTrack** that is already being used by the composition will result in an error. To reuse an audio track multiple times in a composition, call [**Clone**](https://msdn.microsoft.com/library/windows/apps/dn652599) to create new **MediaClip** objects which can then be added to the composition.

-   By default, background audio tracks begin playing at the start of the composition. If multiple background tracks are present, all of the tracks will begin playing at the start of the composition. To cause a background audio track to be begin playback at another time, set the [**Delay**](https://msdn.microsoft.com/library/windows/apps/dn652563) property to the desired time offset.

## Add an overlay to a composition

Overlays allow you to stack multiple layers of video on top of each other in a composition. A composition can contain multiple overlay layers, each of which can include multiple overlays. Create a [**MediaOverlay**](https://msdn.microsoft.com/library/windows/apps/dn764793) object by passing a **MediaClip** into its constructor. Set the position and opacity of the overlay, then create a new [**MediaOverlayLayer**](https://msdn.microsoft.com/library/windows/apps/dn764795) and add the **MediaOverlay** to its [**Overlays**](https://msdn.microsoft.com/library/windows/desktop/dn280411) list. Finally, add the **MediaOverlayLayer** to the composition's [**OverlayLayers**](https://msdn.microsoft.com/library/windows/apps/dn764791) list.

[!code-cs[AddOverlay](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetAddOverlay)]

-   Overlays within a layer are z-ordered based on their order in their containing layer's **Overlays** list. Higher indices within the list are rendered on top of lower indices. The same is true of overlay layers within a composition. A layer with higher index in the composition's **OverlayLayers** list will be rendered on top of lower indices.

-   Because overlays are stacked on top of each other instead of being played sequentially, all overlays start playback at the beginning of the composition by default. To cause an overlay to be begin playback at another time, set the [**Delay**](https://msdn.microsoft.com/library/windows/apps/dn764810) property to the desired time offset.

## Add effects to a media clip

Each **MediaClip** in a composition has a list of audio and video effects to which multiple effects can be added. The effects must implement [**IAudioEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn608044) and [**IVideoEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn608047) respectively. The following example uses the current **MediaPlayerElement** position to choose the currently viewed **MediaClip** and then creates a new instance of the [**VideoStabilizationEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn926762) and appends it to the media clip's [**VideoEffectDefinitions**](https://msdn.microsoft.com/library/windows/apps/dn652643) list.

[!code-cs[AddVideoEffect](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetAddVideoEffect)]

## Save a composition to a file

Media compositions can be serialized to a file to be modified at a later time. Pick an output file and then call the [**MediaComposition**](https://msdn.microsoft.com/library/windows/apps/dn652646) method [**SaveAsync**](https://msdn.microsoft.com/library/windows/apps/dn640554) to save the composition.

[!code-cs[SaveComposition](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetSaveComposition)]

## Load a composition from a file

Media compositions can be deserialized from a file to allow the user to view and modify the composition. Pick a composition file and then call the [**MediaComposition**](https://msdn.microsoft.com/library/windows/apps/dn652646) method [**LoadAsync**](https://msdn.microsoft.com/library/windows/apps/dn652684) to load the composition.

[!code-cs[OpenComposition](./code/MediaEditing/cs/MainPage.xaml.cs#SnippetOpenComposition)]

-   If a media file in the composition is not in a location that can be accessed by your app and is not in the [**FutureAccessList**](https://msdn.microsoft.com/library/windows/apps/br207457) property of the [**StorageApplicationPermissions**](https://msdn.microsoft.com/library/windows/apps/br207456) class for your app, an error will be thrown when loading the composition.

 

 




