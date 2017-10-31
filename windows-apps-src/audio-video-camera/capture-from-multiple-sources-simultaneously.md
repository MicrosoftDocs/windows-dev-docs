---
author: drewbatgit
ms.assetid: 
description: This article shows you how to capture video from multiple sources simulataneously to a single file with multiple embedded video tracks.
title: Capture from multiple sources using MediaFrameSourceGroup
ms.author: drewbat
ms.date: 09/12/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, capture, video
localizationpriority: medium
---

# Capture from multiple sources using MediaFrameSourceGroup

This article shows you how to capture video from multiple sources simultaneously to a single file with multiple embedded video tracks. Starting with RS3, you can specify multiple **[VideoStreamDescriptor](https://docs.microsoft.com/uwp/api/windows.media.core.videostreamdescriptor)** objects for a single **[MediaEncodingProfile](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile)**. This enables you to encode multiple streams simultaneously to a single file. The video streams that are encoded in this operation must be included in a single **[MediaFrameSourceGroup](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourcegroup)** which specifies a set of cameras on the current device that can be used at the same time. 

For information on using **[MediaFrameSourceGroup](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourcegroup)** with the **[MediaFrameReader](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframereader)** class to enable real-time computer vision scenarios that use multiple cameras, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md).

The rest of this article will walk you through the steps of recording video from two color cameras to a single file with multiple video tracks.

## Find available sensor groups
A **MediaFrameSourceGroup** represents a collection of frame sources, typically cameras, that can be accessed simulataneously. The set of available frame source groups is different for each device, so the first step in this example is to get the list of available frame source groups and finding one that contains the necessary cameras for the scenario, which in this case requires two color cameras.

The **[MediaFrameSourceGroup.FindAllAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourcegroup#Windows_Media_Capture_Frames_MediaFrameSourceGroup_FindAllAsync)** method returns all source groups available on the current device. Each returned **MediaFrameSourceGroup** has a list of **[MediaFrameSourceInfo](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourceinfo)** objects that describes each frame source in the group. A Linq query is used to find a source group that contains two color cameras, one on the front panel and one on the back. An anonymous object is returned that contains the selected **MediaFrameSourceGroup** and the **MediaFrameSourceInfo** for each color camera. Instead of using Linq syntax, you could instead loop through each group, and then each **MediaFrameSourceInfo** to look for a group that meets your requirements.

Note that not every device will contain a source group that contains two color cameras, so you should check to make sure that a source group was found before trying to capture video.

[!code-cs[MultiRecordFindSensorGroups](./code/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs#SnippetMultiRecordFindSensorGroups)]

## Initialize the MediaCapture object
The **[MediaCapture](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture)** class is the primary class that is used for most audio, video, and photo capture operations in UWP apps. Initialize the object by calling **[InitializeAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture#Windows_Media_Capture_MediaCapture_InitializeAsync)**, passing in a **[MediaCaptureInitializationSettings](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacaptureinitializationsettings)** object that contains initialization parameters. In this example, the only specified setting is the **[SourceGroup](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacaptureinitializationsettings#Windows_Media_Capture_MediaCaptureInitializationSettings_SourceGroup)** property, which is set to the **MediaFrameSourceGroup** that was retrieved in the previous code example.

For information on other operations you can perform with **MediaCapture** and other UWP app features for capturing media, see [Camera](camera.md).

[!code-cs[MultiRecordInitMediaCapture](./code/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs#SnippetMultiRecordInitMediaCapture)]

## Create a MediaEncodingProfile
The **[MediaEncodingProfile](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile)** class tells the media capture pipeline how captured audio and video should be encoded as they are written to a file. For typical capture and transcoding scenarios, this class provides a set of static methods for creating common profiles, like **[CreateAvi](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile#Windows_Media_MediaProperties_MediaEncodingProfile_CreateAvi_Windows_Media_MediaProperties_VideoEncodingQuality_)** and **[CreateMp3](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile#Windows_Media_MediaProperties_MediaEncodingProfile_CreateMp3_Windows_Media_MediaProperties_AudioEncodingQuality_)**. For this example, an encoding profile is manually created using an Mpeg4 container and H264 video encoding. Video encoding settings are specified using a **[VideoEncodingProperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.videoencodingproperties)** object. For each color camera used in this scenario, a **VideoStreamDescriptor** object is configured. The descriptor is constructed with the **VideoEncodingProperties** object specifying the encoding. The **[Label](https://docs.microsoft.com/uwp/api/windows.media.core.videostreamdescriptor#Windows_Media_Core_VideoStreamDescriptor_Label)** property of the **VideoStreamDescriptor** must be set to the ID of the media frame source that will be captured to the stream. This is how the capture pipeline knows which stream descriptor and encoding properties should be used for each camera. The ID of the frame source is exposed by the **[MediaFrameSourceInfo](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourceinfo)** objects that were found in the previous section, when a **MediaFrameSourceGroup** was selected.


Starting with RS3, you can set multiple encoding properties on a **MediaEncodingProfile** by calling **[SetVideoTracks](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile#Windows_Media_MediaProperties_MediaEncodingProfile_SetVideoTracks_Windows_Foundation_Collections_IIterable_Windows_Media_Core_VideoStreamDescriptor__)**. You can retrieve the list of video stream descriptors by calling **[GetVideoTracks](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile#Windows_Media_MediaProperties_MediaEncodingProfile_GetVideoTracks)**. Note that if you set the **[Video](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile#Windows_Media_MediaProperties_MediaEncodingProfile_Video)** property, which stores a single stream descriptor, the descriptor list you set by calling **SetVideoTracks** will be replaced with a list containing the single descriptor you specified.


[!code-cs[MultiRecordMediaEncodingProfile](./code/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs#SnippetMultiRecordMediaEncodingProfile)]

## Record using the multi-stream MediaEncodingProfile
The final step in this example is to initiate video capture by calling **[StartRecordToStorageFileAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture#Windows_Media_Capture_MediaCapture_StartRecordToStorageFileAsync_Windows_Media_MediaProperties_MediaEncodingProfile_Windows_Storage_IStorageFile_)**, passing in the **StorageFile** to which the captured media is written, and the **MediaEncodingProfile** created in the previous code example. After waiting a few seconds, the recording is stopped with a call to **[StopRecordAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture#Windows_Media_Capture_MediaCapture_StopRecordAsync)**.

[!code-cs[MultiRecordToFile](./code/SimpleCameraPreview_Win10/cs/MainPage.MultiRecord.xaml.cs#SnippetMultiRecordToFile)]

When the operation is complete, a video file will have been created that contains the video captured from each camera encoded as a separate stream within the file. For information on playing media files containing multiple video tracks, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
* [Media items, playlists, and tracks](media-playback-with-mediasource.md)


 

 




