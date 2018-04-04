---
author: drewbatgit
ms.assetid: 0A360481-B649-4E90-9BC4-4449BA7445EF
description: Query for audio and video encoders and decoders installed on a device.
title: Query for installed codecs
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, codec, encoder, decoder, query
ms.localizationpriority: medium
---

# Query for codecs installed on a device
The **[CodecQuery](https://docs.microsoft.com/uwp/api/windows.media.core.codecquery)** class allows you to query for codecs installed on the current device. The list of codecs that are included with Windows 10 for different device families are listed in the article [Supported codecs](supported-codecs.md), but since users and apps can install additional codecs on a device, you may want to query for codec support at runtime to determine what codecs are available on the current device.

The CodecQuery API is a member of the **[Windows.Media.Core](https://docs.microsoft.com/uwp/api/windows.media.core)** namespace, so you will need to include this namespace in your app.

The CodecQuery API is a member of the **[Windows.Media.Core](https://docs.microsoft.com/uwp/api/windows.media.core)** namespace, so you will need to include this namespace in your app.

[!code-cs[CodecQueryUsing](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetCodecQueryUsing)]

Initialize a new instance of the **CodecQuery** class by calling the constructor.

[!code-cs[NewCodecQuery](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetNewCodecQuery)]

The **[FindAllAsync](https://docs.microsoft.com/uwp/api/windows.media.core.codecquery.findallasync)** method returns all installed codecs that match the supplied parameters. These parameters include a **[CodecKind](https://docs.microsoft.com/uwp/api/windows.media.core.codeckind)** value specifying whether you are querying for audio or video codecs or both, a **[CodecCategory](https://docs.microsoft.com/uwp/api/windows.media.core.codeccategory)** value specifying whether you are querying for encoders or decoders, and a string that represents the media encoding subtype for which you are querying, such as H.264 video or MP3 audio.

Specify empty string or null for the subtype value to return codecs for all subtypes. The following example lists all of the video encoders installed on the device.

[!code-cs[FindAllEncoders](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetFindAllEncoders)]

The subtype string you pass into **FindAllAsync** can either be a string representation of the subtype GUID which is defined by the system or a FOURCC code for the subtype. The set of supported media subtype GUIDs are listed in the articles [Audio Subtype GUIDs](https://msdn.microsoft.com/library/windows/desktop/aa372553(v=vs.85).aspx) and [Video Subtype GUIDs](https://msdn.microsoft.com/library/windows/desktop/aa370819(v=vs.85).aspx), but the **[CodecSubtypes](https://docs.microsoft.com/uwp/api/windows.media.core.codecsubtypes)** class provides properties that return the GUID values for each supported subtype. For more information on FOURCC codes, see [FOURCC Codes](https://msdn.microsoft.com/library/windows/desktop/dd375802(v=vs.85).aspx) 

The following example specifies the FOURCC code "H264" to determine if there is an H.264 video decoder installed on the device. You could perform this query before attempting to play back H.264 video content. You can also handle unsupported codecs at playback time. For more information, see [Handle unsupported codecs and unknown errors when opening media items](https://docs.microsoft.com/windows/uwp/audio-video-camera/media-playback-with-mediasource#handle-unsupported-codecs-and-unknown-errors-when-opening-media-items).

[!code-cs[IsH264Supported](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetIsH264Supported)]

The following example queries to determine if a FLAC audio encoder is installed on the current device and, if so, a **[MediaEncodingProfile](https://docs.microsoft.com/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile)** is created for the subtype which could be used for capturing audio to a file or transcoding audio from another format to a FLAC audio file.

[!code-cs[IsFLACSupported](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetIsFLACSupported)]

## Related topics

* [Media playback](media-playback.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Transcode media files](transcode-media-files.md)
* [Supported codecs](supported-codecs.md)
 

 




