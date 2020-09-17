---
ms.assetid: 0A360481-B649-4E90-9BC4-4449BA7445EF
description: Query for audio and video encoders and decoders installed on a device.
title: Query for installed codecs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, codec, encoder, decoder, query
ms.localizationpriority: medium
---
# Query for codecs installed on a device
The **[CodecQuery](/uwp/api/windows.media.core.codecquery)** class allows you to query for codecs installed on the current device. The list of codecs that are included with Windows 10 for different device families are listed in the article [Supported codecs](supported-codecs.md), but since users and apps can install additional codecs on a device, you may want to query for codec support at runtime to determine what codecs are available on the current device.

The CodecQuery API is a member of the **[Windows.Media.Core](/uwp/api/windows.media.core)** namespace, so you will need to include this namespace in your app.

The CodecQuery API is a member of the **[Windows.Media.Core](/uwp/api/windows.media.core)** namespace, so you will need to include this namespace in your app.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/TranscodeWin10/cs/MainPage.xaml.cs" id="SnippetCodecQueryUsing":::

Initialize a new instance of the **CodecQuery** class by calling the constructor.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/TranscodeWin10/cs/MainPage.xaml.cs" id="SnippetNewCodecQuery":::

The **[FindAllAsync](/uwp/api/windows.media.core.codecquery.findallasync)** method returns all installed codecs that match the supplied parameters. These parameters include a **[CodecKind](/uwp/api/windows.media.core.codeckind)** value specifying whether you are querying for audio or video codecs or both, a **[CodecCategory](/uwp/api/windows.media.core.codeccategory)** value specifying whether you are querying for encoders or decoders, and a string that represents the media encoding subtype for which you are querying, such as H.264 video or MP3 audio.

Specify empty string or null for the subtype value to return codecs for all subtypes. The following example lists all of the video encoders installed on the device.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/TranscodeWin10/cs/MainPage.xaml.cs" id="SnippetFindAllEncoders":::

The subtype string you pass into **FindAllAsync** can either be a string representation of the subtype GUID which is defined by the system or a FOURCC code for the subtype. The set of supported media subtype GUIDs are listed in the articles [Audio Subtype GUIDs](/windows/desktop/medfound/audio-subtype-guids) and [Video Subtype GUIDs](/windows/desktop/medfound/video-subtype-guids), but the **[CodecSubtypes](/uwp/api/windows.media.core.codecsubtypes)** class provides properties that return the GUID values for each supported subtype. For more information on FOURCC codes, see [FOURCC Codes](/windows/desktop/DirectShow/fourcc-codes) 

The following example specifies the FOURCC code "H264" to determine if there is an H.264 video decoder installed on the device. You could perform this query before attempting to play back H.264 video content. You can also handle unsupported codecs at playback time. For more information, see [Handle unsupported codecs and unknown errors when opening media items](./media-playback-with-mediasource.md#handle-unsupported-codecs-and-unknown-errors-when-opening-media-items).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/TranscodeWin10/cs/MainPage.xaml.cs" id="SnippetIsH264Supported":::

The following example queries to determine if a FLAC audio encoder is installed on the current device and, if so, a **[MediaEncodingProfile](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile)** is created for the subtype which could be used for capturing audio to a file or transcoding audio from another format to a FLAC audio file.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/TranscodeWin10/cs/MainPage.xaml.cs" id="SnippetIsFLACSupported":::

## Related topics

* [Media playback](media-playback.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Transcode media files](transcode-media-files.md)
* [Supported codecs](supported-codecs.md)
 

 
