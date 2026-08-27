---
description: You can use the Windows.Media.Transcoding APIs to transcode video files from one format to another.
title: Transcode media files
ms.date: 08/23/2026
ms.topic: article
keywords: windows, winui, transcode, media, video, audio
ms.localizationpriority: medium
---

# Transcode media files

You can use the [**Windows.Media.Transcoding**](/uwp/api/Windows.Media.Transcoding) APIs to transcode video files from one format to another.

*Transcoding* is the conversion of a digital media file, such as a video or audio file, from one format to another. This is usually done by decoding and then re-encoding the file. For example, you might convert a Windows Media file to MP4 so that it can be played on a portable device that supports MP4 format. Or, you might convert a high-definition video file to a lower resolution. In that case, the re-encoded file might use the same codec as the original file, but it would have a different encoding profile.

## Select source and destination files

The way that your app determines the source and destination files for transcoding depends on your implementation. This example uses a [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) and a [**FileSavePicker**](/uwp/api/Windows.Storage.Pickers.FileSavePicker) to allow the user to pick a source and a destination file.

```csharp
var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

// Initialize the picker with the window handle (required for WinUI desktop apps).
var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);

openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
openPicker.FileTypeFilter.Add(".wmv");
openPicker.FileTypeFilter.Add(".mp4");

StorageFile source = await openPicker.PickSingleFileAsync();

var savePicker = new Windows.Storage.Pickers.FileSavePicker();

WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);

savePicker.SuggestedStartLocation =
    Windows.Storage.Pickers.PickerLocationId.VideosLibrary;

savePicker.DefaultFileExtension = ".mp4";
savePicker.SuggestedFileName = "New Video";

savePicker.FileTypeChoices.Add("MPEG4", new string[] { ".mp4" });

StorageFile destination = await savePicker.PickSaveFileAsync();
```

## Create a media encoding profile

The encoding profile contains the settings that determine how the destination file will be encoded. This is where you have the greatest number of options when you transcode a file.

The [**MediaEncodingProfile**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) class provides static methods for creating predefined encoding profiles:

### Methods for creating Audio-only encoding profiles

Method  |Profile  |
---------|---------|
[**CreateAlac**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createalac)     |Apple Lossless Audio Codec (ALAC) audio         |
[**CreateFlac**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createflac)     |Free Lossless Audio Codec (FLAC) audio         |
[**CreateM4a**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createm4a)     |AAC audio (M4A)         |
[**CreateMp3**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp3)     |MP3 audio         |
[**CreateWav**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createwav)     |WAV audio         |
[**CreateWma**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createwma)     |Windows Media Audio (WMA)         |

### Methods for creating audio / video encoding profiles

Method  |Profile  |
---------|---------|
[**CreateAv1**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createav1) |AOMedia Video 1 (AV1) video |
[**CreateAvi**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createavi) |AVI |
[**CreateHevc**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createhevc) |High Efficiency Video Coding (HEVC) video, also known as H.265 video |
[**CreateMp4**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4) |MP4 video (H.264 video plus AAC audio) |
[**CreateVp9**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createvp9) |VP9 video |
[**CreateWmv**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createwmv) |Windows Media Video (WMV) |


The following code creates a profile for MP4 video.

```csharp
MediaEncodingProfile profile =
    MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD720p);
```

The static [**CreateMp4**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4) method creates an MP4 encoding profile. The parameter for this method gives the target resolution for the video. In this case, [**VideoEncodingQuality.hd720p**](/uwp/api/Windows.Media.MediaProperties.VideoEncodingQuality) means 1280 x 720 pixels at 30 frames per second. ("720p" stands for 720 progressive scan lines per frame.) The other methods for creating predefined profiles all follow this pattern.

Alternatively, you can create a profile that matches an existing media file by using the [**MediaEncodingProfile.CreateFromFileAsync**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createfromfileasync) method. Or, if you know the exact encoding settings that you want, you can create a new [**MediaEncodingProfile**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) object and fill in the profile details.

## Transcode the file

To transcode the file, create a new [**MediaTranscoder**](/uwp/api/Windows.Media.Transcoding.MediaTranscoder) object and call the [**MediaTranscoder.PrepareFileTranscodeAsync**](/uwp/api/windows.media.transcoding.mediatranscoder.preparefiletranscodeasync) method. Pass in the source file, the destination file, and the encoding profile. Then call the [**TranscodeAsync**](/uwp/api/windows.media.transcoding.preparetranscoderesult.transcodeasync) method on the [**PrepareTranscodeResult**](/uwp/api/Windows.Media.Transcoding.PrepareTranscodeResult) object that was returned from the async transcode operation.

```csharp
MediaTranscoder transcoder = new MediaTranscoder();

PrepareTranscodeResult prepareOp = await
    transcoder.PrepareFileTranscodeAsync(source, destination, profile);

if (prepareOp.CanTranscode)
{
    var transcodeOp = prepareOp.TranscodeAsync();

    transcodeOp.Progress +=
        new AsyncActionProgressHandler<double>(TranscodeProgress);
    transcodeOp.Completed +=
        new AsyncActionWithProgressCompletedHandler<double>(TranscodeComplete);
}
else
{
    switch (prepareOp.FailureReason)
    {
        case TranscodeFailureReason.CodecNotFound:
            System.Diagnostics.Debug.WriteLine("Codec not found.");
            break;
        case TranscodeFailureReason.InvalidProfile:
            System.Diagnostics.Debug.WriteLine("Invalid profile.");
            break;
        default:
            System.Diagnostics.Debug.WriteLine("Unknown failure.");
            break;
    }
}
```

## Respond to transcoding progress

You can register events to respond when the progress of the asynchronous [**TranscodeAsync**](/uwp/api/windows.media.transcoding.preparetranscoderesult.transcodeasync) changes. These events are part of the async programming framework for Windows apps and are not specific to the transcoding API.

```csharp
void TranscodeProgress(IAsyncActionWithProgress<double> asyncInfo, double percent)
{
    // Display or handle progress info.
}

void TranscodeComplete(IAsyncActionWithProgress<double> asyncInfo, AsyncStatus status)
{
    asyncInfo.GetResults();
    if (asyncInfo.Status == AsyncStatus.Completed)
    {
        // Display or handle complete info.
    }
    else if (asyncInfo.Status == AsyncStatus.Canceled)
    {
        // Display or handle cancel info.
    }
    else
    {
        // Display or handle error info.
    }
}
```
