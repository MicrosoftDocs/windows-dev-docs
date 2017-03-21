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
---

# Query for installed codecs
This article shows you how to query for audio and video encoders and decoders that are installed on a device. The article [Supported codecs](supported-codecs.md) lists the codecs that are included in the OS for each device family. On an individual device, the user or an app may install additional codecs. The [CodecQuery](https://docs.microsoft.com/en-us/uwp/api/windows.media.core.codecquery) class can be used to list the current set of encoders and decoders that are installed on the device on which your app is running.

The **CodecQuery** class is included in the [windows.media.core](https://docs.microsoft.com/en-us/uwp/api/windows.media.core) namespace and provides a constructor that takes no arguments.

[!code-cs[CodecQueryUsing](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetCodecQueryUsing)]

[!code-cs[NewCodecQuery](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetNewCodecQuery)]

The [FindAllAsync](https://docs.microsoft.com/en-us/uwp/api/windows.media.core.codecquery#Windows_Media_Core_CodecQuery_FindAllAsync_Windows_Media_Core_CodecKind_Windows_Media_Core_CodecCategory_System_String_) method of the **CodecQuery** class returns all of the installed codecs that meet the specified parameters. These parameters include a value from the [CodecKind](https://docs.microsoft.com/en-us/uwp/api/windows.media.core.codeckind) enumeration which specifies whether audio or video codecs should be returned, and a [CodecCategory](https://docs.microsoft.com/en-us/uwp/api/windows.media.core.codeccategory) value which specifies whether audio or video codecs should be returned.

The final parameter is a string representing a media subtype, such as H264 video or FLAC audio, that must be supported by any codec returned from **FindAllAsync**. You can specify null or empty string for this parameter to return codecs for all media subtypes. The following example lists all video encoders installed on the current device.

[!code-cs[FindAllEncoders](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetFindAllEncoders)]

If you specify a media subtype, you must use the string representation of one of the subtype GUIDs listed in [Audio Subtype GUIDs](https://msdn.microsoft.com/library/windows/desktop/aa372553(v=vs.85).aspx) or [Video Subtype GUIDs](https://msdn.microsoft.com/library/windows/desktop/aa370819(v=vs.85).aspx). The [CodecSubtypes](https://docs.microsoft.com/en-us/uwp/api/windows.media.core.codecsubtypes) class provides properties for most supported media subtypes that return the string representation of the subtype GUID. You can also specify a FOURCC code for the media subtype parameter. For more information, see [FOURCC Codes](https://msdn.microsoft.com/library/windows/desktop/dd375802(v=vs.85).aspx). 

The following example uses the FOURCC code to query for video decoders that support H.264 video. An example scenario for this operation is to check to see if a video format is supported before attempting to play a video file.

[!code-cs[IsH264Supported](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetIsH264Supported)]

The following example queries for audio encoders that support the FLAC media subtype. This example then creates an AudioEncodinAudioEncodingProperties object and a MediaEncodingProfile for the media subtype. This could be used to record audio in the specified format or to transcode audio from another format. For more information, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md) and [Transcode media files](transcode-media-files.md).

[!code-cs[IsFLACSupported](./code/TranscodeWin10/cs/MainPage.xaml.cs#SnippetIsFLACSupported)]

## Related topics

* [Supported codecs](supported-codecs.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Transcode media files](transcode-media-files.md)
 

 




