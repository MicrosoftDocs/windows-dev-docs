---
title: Audio for games
description: Learn how to develop and incorporate music and sounds into your DirectX game, and how to process the audio signals to create dynamic and positional sounds.
ms.assetid: ab29297a-9588-c79b-24c5-3b94b85e74a8
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, audio, directx
ms.localizationpriority: medium
---

# Audio for games

Learn how to develop and incorporate music and sounds into your DirectX game, and how to process the audio signals to create dynamic and positional sounds.

For audio programming, we recommend using either the [XAudio2](/windows/win32/xaudio2/xaudio2-apis-portal) library in DirectX, or the Windows Runtime [Audio graphs](../audio-video-camera/audio-graphs.md) APIs. We use XAudio2 here. XAudio2 is a low-level audio library that provides a signal processing and mixing foundation for games, and it supports a variety of formats.

You can also implement simple sounds and music playback with [Microsoft Media Foundation](/windows/desktop/medfound/microsoft-media-foundation-sdk). Microsoft Media Foundation is designed for the playback of media files and streams, both audio and video, but can also be used in games, and is particularly useful for cinematic scenes or non-interactive components of your game.

## Concepts at a glance

Here are a few audio programming concepts we use in this section.

-   Signals are the basic unit of sound programming, analogous to pixels in graphics. The digital signal processors (DSPs) that process them are like the pixel shaders of game audio. They can transform signals, or combine them, or filter them. By programming to the DSPs, you can alter your game's sound effects and music with as little or as much complexity as you need.
-   Voices are the submixed composites of two or more signals. There are 3 types of XAudio2 voice objects: source, submix, and mastering voices. Source voices operate on audio data provided by the client. Source and submix voices send their output to one or more submix or mastering voices. Submix and mastering voices mix the audio from all voices feeding them, and operate on the result. Mastering voices write audio data to an audio device.
-   Mixing is the process of combining several discrete voices, such as the sound effects and the background audio that are played back in a scene, into a single stream. Submixing is the process of combining several discrete signals, such as the component sounds of an engine noise, and creating a voice.
-   Audio formats. Music and sound effects can be stored in a variety of digital formats for your game. There are uncompressed formats, like WAV, and compressed formats like MP3 and OGG. The more a sample is compressed -- typically designated by its bit rate, where the lower the bit rate is, the more lossy the compression -- the worse fidelity it has. Fidelity can vary across compression schemes and bit rates, so experiment with them to find what works best for your game.
-   Sample rate and quality. Sounds can be sampled at different rates, and sounds sampled at a lower rate have much poorer fidelity. The sample rate for CD quality is 44.1 Khz (44100 Hz). If you don't need high fidelity for a sound, you can choose a lower sample rate. Higher rates may be appropriate for professional audio applications, but you probably don't need them unless your game demands professional fidelity sound.
-   Sound emitters (or sources). In XAudio2, sound emitters are locations that emit a sound, be it a mere blip of a background noise or a snarling rock track played by an in-game jukebox. You specify emitters by world coordinates.
-   Sound listeners. A sound listener is often the player, or perhaps an AI entity in a more advanced game, that processes the sounds received from a listener. You can submix that sound into the audio stream for playback to the player, or you can use it to take a specific in-game action, like awakening an AI guard marked as a listener.

## Design considerations

Audio is a tremendously important part of game design and development. Many gamers can recall a mediocre game elevated to legendary status just because of a memorable soundtrack, or great voice work and sound mixing, or overall stellar audio production. Music and sound define a game's personality, and establish the main motive that defines the game and makes it stand apart from other similar games. The effort you spend designing and developing your game's audio profile will be well worth it.

Positional 3D audio can add a level of immersion beyond that provided by 3D graphics. If you are developing a complex game that simulates a world, or which demands a cinematic style, consider using 3D positional audio techniques to really draw the player in.

## DirectX audio development roadmap

### XAudio2 conceptual resources

XAudio2 is the audio mixing library for DirectX, and is primarily intended for developing high performance audio engines for games. For game developers who want to add sound effects and background music to their modern games, XAudio2 offers an audio graph and mixing engine with low-latency and support for dynamic buffers, synchronous sample-accurate playback, and implicit source rate conversion.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-introduction">Introduction to XAudio2</a></p></td>
<td align="left"><p>The topic provides a list of the audio programming features supported by XAudio2.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/getting-started">Getting Started with XAudio2</a></p></td>
<td align="left"><p>This topic provides information on key XAudio2 concepts, XAudio2 versions, and the RIFF audio format.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/common-audio-concepts">Common Audio Programming Concepts</a></p></td>
<td align="left"><p>This topic provides an overview of common audio concepts with which an audio developer should be familiar.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-voices">XAudio2 Voices</a></p></td>
<td align="left"><p>This topic contains an overview of XAudio2 voices, which are used to submix, operate on, and master audio data.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-callbacks">XAudio2 Callbacks</a></p></td>
<td align="left"><p>This topic covers the XAudio 2 callbacks, which are used to prevent breaks in the audio playback.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/audio-graphs">XAudio2 Audio Graphs</a></p></td>
<td align="left"><p>This topic covers the XAudio2 audio processing graphs, which take a set of audio streams from the client as input, process them, and deliver the final result to an audio device.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-audio-effects">XAudio2 Audio Effects</a></p></td>
<td align="left"><p>The topic covers XAudio2 audio effects, which take incoming audio data and perform some operation on the data (such as a reverb effect) before passing it on.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-streaming-audio-data">Streaming Audio Data with XAudio2</a></p></td>
<td align="left"><p>This topic covers audio streaming with XAudio2.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/x3daudio">X3DAudio</a></p></td>
<td align="left"><p>this topic covers X3DAudio, an API used in conjunction with XAudio2 to create the illusion of a sound coming from a point in 3D space.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/programming-reference">XAudio2 Programming Reference</a></p></td>
<td align="left"><p>This section contains the complete reference for the XAudio2 APIs.</p></td>
</tr>
</tbody>
</table>

### XAudio2 "how to" resources

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--initialize-xaudio2">How to: Initialize XAudio2</a></p></td>
<td align="left"><p>Learn how to initialize XAudio2 for audio playback by creating an instance of the XAudio2 engine, and creating a mastering voice.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--load-audio-data-files-in-xaudio2">How to: Load Audio Data Files in XAudio2</a></p></td>
<td align="left"><p>Learn how to populate the structures required to play audio data in XAudio2.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--play-a-sound-with-xaudio2">How to: Play a Sound with XAudio2</a></p></td>
<td align="left"><p>Learn how to play previously-loaded audio data in XAudio2.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--use-submix-voices">How to: Use Submix Voices</a></p></td>
<td align="left"><p>Learn how to set groups of voices to send their output to the same submix voice.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--use-source-voice-callbacks">How to: Use Source Voice Callbacks</a></p></td>
<td align="left"><p>Learn how to use XAudio2 source voice callbacks.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--use-engine-callbacks">How to: Use Engine Callbacks</a></p></td>
<td align="left"><p>Learn how to use XAudio2 engine callbacks.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--build-a-basic-audio-processing-graph">How to: Build a Basic Audio Processing Graph</a></p></td>
<td align="left"><p>Learn how to create an audio processing graph, constructed from a single mastering voice and a single source voice.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--dynamically-add-or-remove-voices-from-an-audio-graph">How to: Dynamically Add or Remove Voices From an Audio Graph</a></p></td>
<td align="left"><p>Learn how to add or remove submix voices from a graph that has been created following the steps in <a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--build-a-basic-audio-processing-graph">How to: Build a Basic Audio Processing Graph</a>.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--create-an-effect-chain">How to: Create an Effect Chain</a></p></td>
<td align="left"><p>Learn how to apply an effect chain to a voice to allow custom processing of the audio data for that voice.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--create-an-xapo">How to: Create an XAPO</a></p></td>
<td align="left"><p>Learn how to implement <a href="https://docs.microsoft.com/windows/desktop/api/xapo/nn-xapo-ixapo"><strong>IXAPO</strong></a> to create an XAudio2 audio processing object (XAPO).</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--add-run-time-parameter-support-to-an-xapo">How to: Add Run-time Parameter Support to an XAPO</a></p></td>
<td align="left"><p>Learn how to add run-time parameter support to an XAPO by implementing the <a href="https://docs.microsoft.com/windows/desktop/api/xapo/nn-xapo-ixapoparameters"><strong>IXAPOParameters</strong></a> interface.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--use-an-xapo-in-xaudio2">How to: Use an XAPO in XAudio2</a></p></td>
<td align="left"><p>Learn how to use an effect implemented as an XAPO in an XAudio2 effect chain.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--use-xapofx-in-xaudio2">How to: Use XAPOFX in XAudio2</a></p></td>
<td align="left"><p>Learn how to use one of the effects included in XAPOFX in an XAudio2 effect chain.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--stream-a-sound-from-disk">How to: Stream a Sound from Disk</a></p></td>
<td align="left"><p>Learn how to stream audio data in XAudio2 by creating a separate thread to read an audio buffer, and to use callbacks to control that thread.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--integrate-x3daudio-with-xaudio2">How to: Integrate X3DAudio with XAudio2</a></p></td>
<td align="left"><p>Learn how to use X3DAudio to provide the volume and pitch values for XAudio2 voices as well as the parameters for the XAudio2 built-in reverb effect.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/how-to--group-audio-methods-as-an-operation-set">How to: Group Audio Methods as an Operation Set</a></p></td>
<td align="left"><p>Learn how to use XAudio2 operation sets to make a group of method calls take effect at the same time.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/xaudio2/debugging-audio-glitches-in-xaudio2">Debugging Audio Glitches in XAudio2</a></p></td>
<td align="left"><p>Learn how to set the debug logging level for XAudio2.</p></td>
</tr>
</tbody>
</table>

### Media Foundation resources

Media Foundation (MF) is a media platform for streaming audio and video playback. You can use the Media Foundation APIs to stream audio and video encoded and compressed with a variety of algorithms. It is not designed for real-time gameplay scenarios; instead, it provides powerful tools and broad codec support for more linear capture and presentation of audio and video components.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/about-the-media-foundation-sdk">About Media Foundation</a></p></td>
<td align="left"><p>This section contains general information about the Media Foundation APIs, and the tools available to support them.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/media-foundation-programming--essential-concepts">Media Foundation: Essential Concepts</a></p></td>
<td align="left"><p>This topic introduces some concepts that you will need to understand before writing a Media Foundation application.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/media-foundation-architecture">Media Foundation Architecture</a></p></td>
<td align="left"><p>This section describes the general design of Microsoft Media Foundation, as well as the media primitives and processing pipeline it uses.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/audio-video-capture">Audio/Video Capture</a></p></td>
<td align="left"><p>This topic describes how to use Microsoft Media Foundation to perform audio and video capture.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/audio-video-playback">Audio/Video Playback</a></p></td>
<td align="left"><p>This topic describes how to implement audio/video playback in your app.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/supported-media-formats-in-media-foundation">Supported Media Formats in Media Foundation</a></p></td>
<td align="left"><p>This topic lists the media formats that Microsoft Media Foundation supports natively. (Third parties can support additional formats by writing custom plug-ins.)</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/encoding-and-file-authoring">Encoding and File Authoring</a></p></td>
<td align="left"><p>This topic describes how to use Microsoft Media Foundation to perform audio and video encoding, and author media files.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/windows-media-codecs">Windows Media Codecs</a></p></td>
<td align="left"><p>This topic describes how to use the features of the Windows Media Audio and Video codecs to produce and consume compressed data streams.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/media-foundation-programming-reference">Media Foundation Programming Reference</a></p></td>
<td align="left"><p>This section contains reference information for the Media Foundation APIs.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/desktop/medfound/media-foundation-sdk-samples">Media Foundation SDK Samples</a></p></td>
<td align="left"><p>This section lists sample apps that demonstrate how to use Media Foundation.</p></td>
</tr>
</tbody>
</table>

### Windows Runtime XAML media types

If you are using [DirectX-XAML interop](/previous-versions/windows/apps/hh825871(v=win.10)), you can incorporate the Windows Runtime XAML media APIs into your UWP apps using DirectX with C++ for simpler game scenarios.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaElement"><strong>Windows.UI.Xaml.Controls.MediaElement</strong></a></p></td>
<td align="left"><p>XAML element that represents an object that contains audio, video, or both.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/uwp/audio-video-camera/index">Audio, video, and camera</a></p></td>
<td align="left"><p>Learn how to incorporate basic audio and video in your Universal Windows Platform (UWP) app.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/media-playback">MediaElement</a></p></td>
<td align="left"><p>Learn how to play a locally-stored media file in your UWP app.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/media-playback">MediaElement</a></p></td>
<td align="left"><p>Learn how to stream a media file with low-latency in your UWP app.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="https://docs.microsoft.com/windows/uwp/audio-video-camera/media-casting">Media casting</a></p></td>
<td align="left"><p>Learn how to use the Play To contract to stream media from your UWP app to another device.</p></td>
</tr>
</tbody>
</table>

## Reference

-   [XAudio2 Introduction](/windows/desktop/xaudio2/xaudio2-introduction)
-   [XAudio2 Programming Guide](/windows/desktop/xaudio2/programming-guide)
-   [Microsoft Media Foundation overview](/windows/desktop/medfound/microsoft-media-foundation-sdk)

## Related topics

-   [XAudio2 Programming Guide](/windows/desktop/xaudio2/programming-guide)