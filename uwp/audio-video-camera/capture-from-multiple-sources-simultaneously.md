---
ms.assetid: 
description: This article shows you how to capture video from multiple sources simultaneously to a single file with multiple embedded video tracks.
title: Capture from multiple sources using MediaFrameSourceGroup
ms.date: 09/12/2017
ms.topic: article
keywords: windows 10, uwp, capture, video
ms.localizationpriority: medium
---
# Capture from multiple sources using MediaFrameSourceGroup

This article shows you how to capture video from multiple sources simultaneously to a single file with multiple embedded video tracks. Starting with RS3, you can specify multiple **[VideoStreamDescriptor](/uwp/api/windows.media.core.videostreamdescriptor)** objects for a single **[MediaEncodingProfile](/uwp/api/windows.media.mediaproperties.mediaencodingprofile)**. This enables you to encode multiple streams simultaneously to a single file. The video streams that are encoded in this operation must be included in a single **[MediaFrameSourceGroup](/uwp/api/windows.media.capture.frames.mediaframesourcegroup)** which specifies a set of cameras on the current device that can be used at the same time. 

For information on using **[MediaFrameSourceGroup](/uwp/api/windows.media.capture.frames.mediaframesourcegroup)** with the **[MediaFrameReader](/uwp/api/windows.media.capture.frames.mediaframereader)** class to enable real-time computer vision scenarios that use multiple cameras, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md).

The rest of this article will walk you through the steps of recording video from two color cameras to a single file with multiple video tracks.

## Find available sensor groups
A **MediaFrameSourceGroup** represents a collection of frame sources, typically cameras, that can be accessed simultaneously. The set of available frame source groups is different for each device, so the first step in this example is to get the list of available frame source groups and finding one that contains the necessary cameras for the scenario, which in this case requires two color cameras.

The **[MediaFrameSourceGroup.FindAllAsync](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.FindAllAsync)** method returns all source groups available on the current device. Each returned **MediaFrameSourceGroup** has a list of **[MediaFrameSourceInfo](/uwp/api/windows.media.capture.frames.mediaframesourceinfo)** objects that describes each frame source in the group. A Linq query is used to find a source group that contains two color cameras, one on the front panel and one on the back. An anonymous object is returned that contains the selected **MediaFrameSourceGroup** and the **MediaFrameSourceInfo** for each color camera. Instead of using Linq syntax, you could instead loop through each group, and then each **MediaFrameSourceInfo** to look for a group that meets your requirements.

Note that not every device will contain a source group that contains two color cameras, so you should check to make sure that a source group was found before trying to capture video.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs" id="SnippetMultiRecordFindSensorGroups":::

## Initialize the MediaCapture object
The **[MediaCapture](/uwp/api/windows.media.capture.mediacapture)** class is the primary class that is used for most audio, video, and photo capture operations in UWP apps. Initialize the object by calling **[InitializeAsync](/uwp/api/windows.media.capture.mediacapture.InitializeAsync)**, passing in a **[MediaCaptureInitializationSettings](/uwp/api/windows.media.capture.mediacaptureinitializationsettings)** object that contains initialization parameters. In this example, the only specified setting is the **[SourceGroup](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.SourceGroup)** property, which is set to the **MediaFrameSourceGroup** that was retrieved in the previous code example.

For information on other operations you can perform with **MediaCapture** and other UWP app features for capturing media, see [Camera](camera.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs" id="SnippetMultiRecordInitMediaCapture":::

## Create a MediaEncodingProfile
The **[MediaEncodingProfile](/uwp/api/windows.media.mediaproperties.mediaencodingprofile)** class tells the media capture pipeline how captured audio and video should be encoded as they are written to a file. For typical capture and transcoding scenarios, this class provides a set of static methods for creating common profiles, like **[CreateAvi](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createavi)** and **[CreateMp3](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp3)**. For this example, an encoding profile is manually created using an Mpeg4 container and H264 video encoding. Video encoding settings are specified using a **[VideoEncodingProperties](/uwp/api/windows.media.mediaproperties.videoencodingproperties)** object. For each color camera used in this scenario, a **VideoStreamDescriptor** object is configured. The descriptor is constructed with the **VideoEncodingProperties** object specifying the encoding. The **[Label](/uwp/api/windows.media.core.videostreamdescriptor.Label)** property of the **VideoStreamDescriptor** must be set to the ID of the media frame source that will be captured to the stream. This is how the capture pipeline knows which stream descriptor and encoding properties should be used for each camera. The ID of the frame source is exposed by the **[MediaFrameSourceInfo](/uwp/api/windows.media.capture.frames.mediaframesourceinfo)** objects that were found in the previous section, when a **MediaFrameSourceGroup** was selected.


Starting with Windows 10, version 1709, you can set multiple encoding properties on a **MediaEncodingProfile** by calling **[SetVideoTracks](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.setvideotracks)**. You can retrieve the list of video stream descriptors by calling **[GetVideoTracks](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.GetVideoTracks)**. Note that if you set the **[Video](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.Video)** property, which stores a single stream descriptor, the descriptor list you set by calling **SetVideoTracks** will be replaced with a list containing the single descriptor you specified.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs" id="SnippetMultiRecordMediaEncodingProfile":::

### Encode timed metadata in media files

Starting with Windows 10, version 1803, in addition to audio and video you can encode timed metadata into a media file for which the data format is supported. For example, GoPro metadata (gpmd) can be stored in MP4 files to convey the geographic location correlated with a video stream. 

Encoding metadata uses a pattern that is parallel to encoding audio or video. The [**TimedMetadataEncodingProperties**](/uwp/api/windows.media.mediaproperties.timedmetadataencodingproperties) class describes the type, subtype and encoding properties of the metadata, like **VideoEncodingProperties** does for video. The [**TimedMetadataStreamDescriptor**](/uwp/api/windows.media.core.timedmetadatastreamdescriptor) identifies a metadata stream, just as the **VideoStreamDescriptor** does for video streams.  

The following example shows how to initialize a **TimedMetadataStreamDescriptor** object. First, a **TimedMetadataEncodingProperties** object is created and the **Subtype** is set to a GUID that identifies the type of metadata that will be included in the stream. This example uses the GUID for GoPro metadata (gpmd). The [**SetFormatUserData**](/uwp/api/windows.media.mediaproperties.timedmetadataencodingproperties.setformatuserdata) method is called to set format-specific data. For MP4 files, the format-specific data is stored in the SampleDescription box (stsd). Next, a new **TimedMetadataStreamDescriptor** is created from the encoding properties. The **Label** and **Name** properties are set to identify the stream to be encoded. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs" id="SnippetGetStreamDescriptor":::

Call [**MediaEncodingProfile.SetTimedMetadataTracks**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.settimedmetadatatracks) to add the metadata stream descriptor to the encoding profile. The following example shows a helper method that takes two video stream descriptors, one audio stream descriptor, and one timed metadata stream descriptor and returns a **MediaEncodingProfile** that can be used to encode the streams.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs" id="SnippetGetMediaEncodingProfile":::

## Record using the multi-stream MediaEncodingProfile
The final step in this example is to initiate video capture by calling **[StartRecordToStorageFileAsync](/uwp/api/windows.media.capture.mediacapture.startrecordtostoragefileasync)**, passing in the **StorageFile** to which the captured media is written, and the **MediaEncodingProfile** created in the previous code example. After waiting a few seconds, the recording is stopped with a call to **[StopRecordAsync](/uwp/api/windows.media.capture.mediacapture.StopRecordAsync)**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs" id="SnippetMultiRecordToFile":::

When the operation is complete, a video file will have been created that contains the video captured from each camera encoded as a separate stream within the file. For information on playing media files containing multiple video tracks, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
* [Media items, playlists, and tracks](media-playback-with-mediasource.md)


 

 
