---
ms.assetid: A1A0D99A-DCBF-4A14-80B9-7106BEF045EC
description: You can use the Windows.Media.Transcoding APIs to transcode video files from one format to another.
title: Transcode media files
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Transcode media files



You can use the [**Windows.Media.Transcoding**](https://docs.microsoft.com/uwp/api/Windows.Media.Transcoding) APIs to transcode video files from one format to another.

*Transcoding* is the conversion of a digital media file, such as a video or audio file, from one format to another. This is usually done by decoding and then re-encoding the file. For example, you might convert a Windows Media file to MP4 so that it can be played on a portable device that supports MP4 format. Or, you might convert a high-definition video file to a lower resolution. In that case, the re-encoded file might use the same codec as the original file, but it would have a different encoding profile.

## Set up your project for transcoding

In addition to the namespaces referenced by the default project template, you will need to reference these namespaces in order to transcode media files using the code in this article.

[!code-cs[Using](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetUsing)]

## Select source and destination files

The way that your app determines the source and destination files for transcoding depends on your implementation. This example uses a [**FileOpenPicker**](https://docs.microsoft.com/uwp/api/Windows.Storage.Pickers.FileOpenPicker) and a [**FileSavePicker**](https://docs.microsoft.com/uwp/api/Windows.Storage.Pickers.FileSavePicker) to allow the user to pick a source and a destination file.

[!code-cs[TranscodeGetFile](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetTranscodeGetFile)]

## Create a media encoding profile

The encoding profile contains the settings that determine how the destination file will be encoded. This is where you have the greatest number of options when you transcode a file.

The [**MediaEncodingProfile**](https://docs.microsoft.com/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) class provides static methods for creating predefined encoding profiles:

### Methods for creating Audio-only encoding profiles

Method  |Profile  |
---------|---------|
[**CreateAlac**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createalac)     |Apple Lossless Audio Codec (ALAC) audio         |
[**CreateFlac**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createflac)     |Free Lossless Audio Codec (FLAC) audio.         |
[**CreateM4a**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createm4a)     |AAC audio (M4A)         |
[**CreateMp3**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp3)     |MP3 audio         |
[**CreateWav**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createwav)     |WAV audio         |
[**CreateWmv**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createwmv)     |Windows Media Audio (WMA)         |

### Methods for creating audio / video encoding profiles

Method  |Profile  |
---------|---------|
[**CreateAvi**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createavi) |AVI |
[**CreateHevc**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createhevc) |High Efficiency Video Coding (HEVC) video, also known as H.265 video |
[**CreateMp4**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4) |MP4 video (H.264 video plus AAC audio) |
[**CreateWmv**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createwmv) |Windows Media Video (WMV) |


The following code creates a profile for MP4 video.

[!code-cs[TranscodeMediaProfile](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetTranscodeMediaProfile)]

The static [**CreateMp4**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4) method creates an MP4 encoding profile. The parameter for this method gives the target resolution for the video. In this case, [**VideoEncodingQuality.hd720p**](https://docs.microsoft.com/uwp/api/Windows.Media.MediaProperties.VideoEncodingQuality) means 1280 x 720 pixels at 30 frames per second. ("720p" stands for 720 progressive scan lines per frame.) The other methods for creating predefined profiles all follow this pattern.

Alternatively, you can create a profile that matches an existing media file by using the [**MediaEncodingProfile.CreateFromFileAsync**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createfromfileasync) method. Or, if you know the exact encoding settings that you want, you can create a new [**MediaEncodingProfile**](https://docs.microsoft.com/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) object and fill in the profile details.

## Transcode the file

To transcode the file, create a new [**MediaTranscoder**](https://docs.microsoft.com/uwp/api/Windows.Media.Transcoding.MediaTranscoder) object and call the [**MediaTranscoder.PrepareFileTranscodeAsync**](https://docs.microsoft.com/uwp/api/windows.media.transcoding.mediatranscoder.preparefiletranscodeasync) method. Pass in the source file, the destination file, and the encoding profile. Then call the [**TranscodeAsync**](https://docs.microsoft.com/uwp/api/windows.media.transcoding.preparetranscoderesult.transcodeasync) method on the [**PrepareTranscodeResult**](https://docs.microsoft.com/uwp/api/Windows.Media.Transcoding.PrepareTranscodeResult) object that was returned from the async transcode operation.

[!code-cs[TranscodeTranscodeFile](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetTranscodeTranscodeFile)]

## Respond to transcoding progress

You can register events to respond when the progress of the asynchronous [**TranscodeAsync**](https://docs.microsoft.com/uwp/api/windows.media.transcoding.preparetranscoderesult.transcodeasync) changes. These events are part of the async programming framework for Universal Windows Platform (UWP) apps and are not specific to the transcoding API.

[!code-cs[TranscodeCallbacks](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetTranscodeCallbacks)]


## Encode a metadata stream
Starting with Windows 10, version 1803, you can include timed metadata when transcoding media files. Unlike the video transcoding examples above, which use the built-in media encoding profile creation methods, like [**MediaEncodingProfile.CreateMp4**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4), you must manually create the metadata encoding profile to support the type of metadata you are encoding.

This first step in creating a metadata incoding profile is to create a [**TimedMetadataEncodingProperties**] object that describes the encoding of the metadata to be transcoded. The Subtype property is a GUID that specifies the type of the metadata. The encoding details for each metadata type is proprietary and is not provided by Windows. In this example, the GUID for GoPro metadata (gprs) is used. Next, [**SetFormatUserData**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.timedmetadataencodingproperties.setformatuserdata) is called to set a binary blob of data describing the stream format that is specific to the metadata format. Next, a **TimedMetadataStreamDescriptor**(https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatastreamdescriptor) is created from the encoding properites, and a track label and name are to allow an application reading the endcoded stream to identify the metadata stream and optionally display the stream name in the UI. 
 
[!code-cs[GetStreamDescriptor](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetGetStreamDescriptor)]

After creating the **TimedMetadataStreamDescriptor**, you can create a **MediaEncodingProfile** that describes the video, audio, and metadata to be encoded in the file. The **TimedMetadataStreamDescriptor** created in the last example is passed into this example helper function and is added to the **MediaEncodingProfile** by calling [**SetTimedMetadataTracks**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.settimedmetadatatracks).

[!code-cs[GetMediaEncodingProfile](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetGetMediaEncodingProfile)]
 

 




