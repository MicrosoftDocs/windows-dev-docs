---
author: drewbatgit
ms.assetid: CB924E17-C726-48E7-A445-364781F4CCA1
description: This article shows how to use the APIs in the Windows.Media.Audio namespace to create audio graphs for audio routing, mixing, and processing scenarios.
title: Audio graphs
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Audio graphs



This article shows how to use the APIs in the [**Windows.Media.Audio**](https://msdn.microsoft.com/library/windows/apps/dn914341) namespace to create audio graphs for audio routing, mixing, and processing scenarios.

An *audio graph* is a set of interconnected audio nodes through which audio data flows. 

- *Audio input nodes* supply audio data to the graph from audio input devices, audio files, or from custom code. 

- *Audio output nodes* are the destination for audio processed by the graph. Audio can be routed out of the graph to audio output devices, audio files, or custom code. 

- *Submix nodes* take audio from one or more nodes and combine them into a single output that can be routed to other nodes in the graph. 

After all of the nodes have been created and the connections between them set up, you simply start the audio graph and the audio data flows from the input nodes, through any submix nodes, to the output nodes. This model makes scenarios such as recording from a device's microphone to an audio file, playing audio from a file to a device's speaker, or mixing audio from multiple sources quick and easy to implement.

Additional scenarios are enabled with the addition of audio effects to the audio graph. Every node in an audio graph can be populated with zero or more audio effects that perform audio processing on the audio passing through the node. There are several built-in effects such as echo, equalizer, limiting, and reverb that can be attached to an audio node with just a few lines of code. You can also create your own custom audio effects that work exactly the same as the built-in effects.

> [!NOTE]
> The [AudioGraph UWP sample](http://go.microsoft.com/fwlink/?LinkId=619481) implements the code discussed in this overview. You can download the sample to see the code in context or to use as a starting point for your own app.

## Choosing Windows Runtime AudioGraph or XAudio2

The Windows Runtime audio graph APIs offer functionality that can also be implemented by using the COM-based [XAudio2 APIs](https://msdn.microsoft.com/library/windows/desktop/hh405049). The following are features of the Windows Runtime audio graph framework that differ from XAudio2.

The Windows Runtime audio graph APIs:

-   Are significantly easier to use than XAudio2.
-   Can be used from C# in addition to being supported for C++.
-   Can use audio files, including compressed file formats, directly. XAudio2 only operates on audio buffers and does not provide any file I/O capabilities.
-   Can use the low-latency audio pipeline in Windows 10.
-   Support automatic endpoint switching when default endpoint parameters are used. For example, if the user switches from a device's speaker to a headset, the audio is automatically redirected to the new input.

## AudioGraph class

The [**AudioGraph**](https://msdn.microsoft.com/library/windows/apps/dn914176) class is the parent of all nodes that make up the graph. Use this object to create instances of all of the audio node types. Create an instance of the **AudioGraph** class by initializing an [**AudioGraphSettings**](https://msdn.microsoft.com/library/windows/apps/dn914185) object containing configuration settings for the graph, and then calling [**AudioGraph.CreateAsync**](https://msdn.microsoft.com/library/windows/apps/dn914216). The returned [**CreateAudioGraphResult**](https://msdn.microsoft.com/library/windows/apps/dn914273) gives access to the created audio graph or provides an error value if audio graph creation fails.

[!code-cs[DeclareAudioGraph](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareAudioGraph)]

[!code-cs[InitAudioGraph](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetInitAudioGraph)]

-   All audio node types are created by using the Create\* methods of the **AudioGraph** class.
-   The [**AudioGraph.Start**](https://msdn.microsoft.com/library/windows/apps/dn914244) method causes the audio graph to start processing audio data. The [**AudioGraph.Stop**](https://msdn.microsoft.com/library/windows/apps/dn914245) method stops audio processing. Each node in the graph can be started and stopped independently while the graph is running, but no nodes are active when the graph is stopped. [**ResetAllNodes**](https://msdn.microsoft.com/library/windows/apps/dn914242) causes all nodes in the graph to discard any data currently in their audio buffers.
-   The [**QuantumStarted**](https://msdn.microsoft.com/library/windows/apps/dn914241) event occurs when the graph is starting the processing of a new quantum of audio data. The [**QuantumProcessed**](https://msdn.microsoft.com/library/windows/apps/dn914240) event occurs when the processing of a quantum is completed.

-   The only [**AudioGraphSettings**](https://msdn.microsoft.com/library/windows/apps/dn914185) property that is required is [**AudioRenderCategory**](https://msdn.microsoft.com/library/windows/apps/dn297724). Specifying this value allows the system to optimize the audio pipeline for the specified category.
-   The quantum size of the audio graph determines the number of samples that are processed at one time. By default, the quantum size is 10 ms based at the default sample rate. If you specify a custom quantum size by setting the [**DesiredSamplesPerQuantum**](https://msdn.microsoft.com/library/windows/apps/dn914205) property, you must also set the [**QuantumSizeSelectionMode**](https://msdn.microsoft.com/library/windows/apps/dn914208) property to **ClosestToDesired** or the supplied value is ignored. If this value is used, the system will choose a quantum size as close as possible to the one you specify. To determine the actual quantum size, check the [**SamplesPerQuantum**](https://msdn.microsoft.com/library/windows/apps/dn914243) of the **AudioGraph** after it has been created.
-   If you only plan to use the audio graph with files and don't plan to output to an audio device, it is recommended that you use the default quantum size by not setting the [**DesiredSamplesPerQuantum**](https://msdn.microsoft.com/library/windows/apps/dn914205) property.
-   The [**DesiredRenderDeviceAudioProcessing**](https://msdn.microsoft.com/library/windows/apps/dn958522) property determines the amount of processing the primary render device performs on the output of the audio graph. The **Default** setting allows the system to use the default audio processing for the specified audio render category. This processing can significantly improve the sound of audio on some devices, particularly mobile devices with small speakers. The **Raw** setting can improve performance by minimizing the amount of signal processing performed, but can result in inferior sound quality on some devices.
-   If the [**QuantumSizeSelectionMode**](https://msdn.microsoft.com/library/windows/apps/dn914208) is set to **LowestLatency**, the audio graph will automatically use **Raw** for [**DesiredRenderDeviceAudioProcessing**](https://msdn.microsoft.com/library/windows/apps/dn958522).
- Starting with Windows 10, version 1803, you can set the [**AudioGraphSettings.MaxPlaybackSpeedFactor**](https://docs.microsoft.com/uwp/api/windows.media.audio.audiographsettings.maxplaybackspeedfactor) property to set a maximum value used for the [**AudioFileInputNode.PlaybackSpeedFactor**](https://docs.microsoft.com/uwp/api/windows.media.audio.audiofileinputnode.playbackspeedfactor), [**AudioFrameInputNode.PlaybackSpeedFactor**](https://docs.microsoft.com/uwp/api/windows.media.audio.audioframeinputnode.playbackspeedfactor), and [**MediaSourceInputNode.PlaybackSpeedFactor**](https://docs.microsoft.com/uwp/api/windows.media.audio.mediasourceinputnode.playbackspeedfactor) properties. When an audio graph supports a playback speed factor greater than 1, the system must allocate additional memory in order to maintain a sufficient buffer of audio data. For this reason, setting **MaxPlaybackSpeedFactor** to the lowest value required by your app will reduce the memory consumption of your app. If your app will only play back content at normal speed, it is recommended that you set MaxPlaybackSpeedFactor to 1.
-   The [**EncodingProperties**](https://msdn.microsoft.com/library/windows/apps/dn958523) determines the audio format used by the graph. Only 32-bit float formats are supported.
-   The [**PrimaryRenderDevice**](https://msdn.microsoft.com/library/windows/apps/dn958524) sets the primary render device for the audio graph. If you don't set this, the default system device is used. The primary render device is used to calculate the quantum sizes for other nodes in the graph. If there are no audio render devices present on the system, audio graph creation will fail.

You can let the audio graph use the default audio render device or use the [**Windows.Devices.Enumeration.DeviceInformation**](https://msdn.microsoft.com/library/windows/apps/br225393) class to get a list of the system's available audio render devices by calling [**FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/br225432) and passing in the audio render device selector returned by [**Windows.Media.Devices.MediaDevice.GetAudioRenderSelector**](https://msdn.microsoft.com/library/windows/apps/br226817). You can choose one of the returned **DeviceInformation** objects programatically or show UI to allow the user to select a device and then use it to set the [**PrimaryRenderDevice**](https://msdn.microsoft.com/library/windows/apps/dn958524) property.

[!code-cs[EnumerateAudioRenderDevices](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetEnumerateAudioRenderDevices)]

##  Device input node

A device input node feeds audio into the graph from an audio capture device connected to the system, such as a microphone. Create a [**DeviceInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914082) object that uses the system's default audio capture device by calling [**CreateDeviceInputNodeAsync**](https://msdn.microsoft.com/library/windows/apps/dn914218). Provide an [**AudioRenderCategory**](https://msdn.microsoft.com/library/windows/apps/dn297724) to allow the system to optimize the audio pipeline for the specified category.

[!code-cs[DeclareDeviceInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareDeviceInputNode)]


[!code-cs[CreateDeviceInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateDeviceInputNode)]

If you want to specify a specific audio capture device for the device input node, you can use the [**Windows.Devices.Enumeration.DeviceInformation**](https://msdn.microsoft.com/library/windows/apps/br225393) class to get a list of the system's available audio capture devices by calling [**FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/br225432) and passing in the audio render device selector returned by [**Windows.Media.Devices.MediaDevice.GetAudioRenderSelector**](https://msdn.microsoft.com/library/windows/apps/br226817). You can choose one of the returned **DeviceInformation** objects programmatically or show UI to allow the user to select a device and then pass it into [**CreateDeviceInputNodeAsync**](https://msdn.microsoft.com/library/windows/apps/dn914218).

[!code-cs[EnumerateAudioCaptureDevices](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetEnumerateAudioCaptureDevices)]

##  Device output node

A device output node pushes audio from the graph to an audio render device, such as speakers or a headset. Create a [**DeviceOutputNode**](https://msdn.microsoft.com/library/windows/apps/dn914098) by calling [**CreateDeviceOutputNodeAsync**](https://msdn.microsoft.com/library/windows/apps/dn958525). The output node uses the [**PrimaryRenderDevice**](https://msdn.microsoft.com/library/windows/apps/dn958524) of the audio graph.

[!code-cs[DeclareDeviceOutputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareDeviceOutputNode)]

[!code-cs[CreateDeviceOutputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateDeviceOutputNode)]

##  File input node

A file input node allows you to feed data from an audio file into the graph. Create an [**AudioFileInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914108) by calling [**CreateFileInputNodeAsync**](https://msdn.microsoft.com/library/windows/apps/dn914226).

[!code-cs[DeclareFileInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareFileInputNode)]


[!code-cs[CreateFileInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateFileInputNode)]

-   File input nodes support the following file formats: mp3, wav, wma, m4a.
-   Set the [**StartTime**](https://msdn.microsoft.com/library/windows/apps/dn914130) property to specify the time offset into the file where playback should begin. If this property is null, the beginning of the file is used. Set the [**EndTime**](https://msdn.microsoft.com/library/windows/apps/dn914118) property to specify the time offset into the file where playback should end. If this property is null, the end of the file is used. The start time value must be lower than the end time value, and the end time value must be less than or equal to the duration of the audio file, which can be determined by checking the [**Duration**](https://msdn.microsoft.com/library/windows/apps/dn914116) property value.
-   Seek to a position in the audio file by calling [**Seek**](https://msdn.microsoft.com/library/windows/apps/dn914127) and specifying the time offset into the file to which the playback position should be moved. The specified value must be within the [**StartTime**](https://msdn.microsoft.com/library/windows/apps/dn914130) and [**EndTime**](https://msdn.microsoft.com/library/windows/apps/dn914118) range. Get the current playback position of the node with the read-only [**Position**](https://msdn.microsoft.com/library/windows/apps/dn914124) property.
-   Enable looping of the audio file by setting the [**LoopCount**](https://msdn.microsoft.com/library/windows/apps/dn914120) property. When non-null, this value indicates the number of times the file will be played in after the initial playback. So, for example, setting **LoopCount** to 1 will cause the file to be played 2 times in total, and setting it to 5 will cause the file to be played 6 times in total. Setting **LoopCount** to null causes the file to be looped indefinitely. To stop looping, set the value to 0.
-   Adjust the speed at which the audio file is played back by setting the [**PlaybackSpeedFactor**](https://msdn.microsoft.com/library/windows/apps/dn914123). A value of 1 indicates the original speed of the file, .5 is half-speed, and 2 is double speed.

##  MediaSource input node

The [**MediaSource**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaSource) class provides a common way to reference media from different sources and exposes a common model for accessing media data regardless of the underlying media format which could be a file on disk, a stream, or an adaptive streaming network source. A [**MediaSourceAudioInputNode](https://docs.microsoft.com/uwp/api/windows.media.audio.mediasourceaudioinputnode) node lets you direct audio data from a **MediaSource** into the audio graph. Create a **MediaSourceAudioInputNode** by calling [**CreateMediaSourceAudioInputNodeAsync**](https://docs.microsoft.com/uwp/api/windows.media.audio.audiograph.createmediasourceaudioinputnodeasync#Windows_Media_Audio_AudioGraph_CreateMediaSourceAudioInputNodeAsync_Windows_Media_Core_MediaSource_), passing in a **MediaSource** object representing the content you wish to play. A [**CreateMediaSourceAudioInputNodeResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createmediasourceaudioinputnoderesult) is returned which you can use to determine the status of the operation by checking the [**Status**](https://docs.microsoft.com/uwp/api/windows.media.audio.createmediasourceaudioinputnoderesult.status) property. If the status is **Success**, you can get the created **MediaSourceAudioInputNode** by accessing the [**Node**](https://docs.microsoft.com/uwp/api/windows.media.audio.createmediasourceaudioinputnoderesult.node) property. The following example shows the creation of a node from an AdaptiveMediaSource object representing content streaming over the network. For more inforation on working with **MediaSource**, see [Media items, playlists, and tracks](media-playback-with-mediasource.md). For more information on streaming media content over the internet, see [Adaptive streaming](adaptive-streaming.md).

[!code-cs[DeclareMediaSourceInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareMediaSourceInputNode)]

[!code-cs[CreateMediaSourceInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateMediaSourceInputNode)]

To receive a notification when playback has reached the end of the **MediaSource** content, register a handler for the [**MediaSourceCompleted**](https://docs.microsoft.com/uwp/api/windows.media.audio.mediasourceaudioinputnode.mediasourcecompleted) event. 

[!code-cs[RegisterMediaSourceCompleted](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetRegisterMediaSourceCompleted)]

[!code-cs[MediaSourceCompleted](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetMediaSourceCompleted)]

While playing a file from diskis likely to always complete successfully, media streamed from a network source may fail during playback due to a change in network connection or other issues that are outside the control of the audio graph. If a **MediaSource** becomes unplayable during playback, the audio graph will raise the [**UnrecoverableErrorOccurred**](https://docs.microsoft.com/uwp/api/windows.media.audio.audiograph.unrecoverableerroroccurred) event. You can use the handler for this event to stop and dispose of the audio graph and then reinitialize your graph. 

[!code-cs[RegisterUnrecoverableError](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetRegisterUnrecoverableError)]

[!code-cs[UnrecoverableError](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetUnrecoverableError)]

##  File output node

A file output node lets you direct audio data from the graph into an audio file. Create an [**AudioFileOutputNode**](https://msdn.microsoft.com/library/windows/apps/dn914133) by calling [**CreateFileOutputNodeAsync**](https://msdn.microsoft.com/library/windows/apps/dn914227).

[!code-cs[DeclareFileOutputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareFileOutputNode)]


[!code-cs[CreateFileOutputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateFileOutputNode)]

-   File output nodes support the following file formats: mp3, wav, wma, m4a.
-   You must call [**AudioFileOutputNode.Stop**](https://msdn.microsoft.com/library/windows/apps/dn914144) to stop the node's processing before calling [**AudioFileOutputNode.FinalizeAsync**](https://msdn.microsoft.com/library/windows/apps/dn914140) or an exception will be thrown.

##  Audio frame input node

An audio frame input node allows you to push audio data that you generate in your own code into the audio graph. This enables scenarios like creating a custom software synthesizer. Create an [**AudioFrameInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914147) by calling [**CreateFrameInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914230).

[!code-cs[DeclareFrameInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareFrameInputNode)]


[!code-cs[CreateFrameInputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateFrameInputNode)]

The [**FrameInputNode.QuantumStarted**](https://msdn.microsoft.com/library/windows/apps/dn958507) event is raised when the audio graph is ready to begin processing the next quantum of audio data. You supply your custom generated audio data from within the handler to this event.

[!code-cs[QuantumStarted](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetQuantumStarted)]

-   The [**FrameInputNodeQuantumStartedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn958533) object passed into the **QuantumStarted** event handler exposes the [**RequiredSamples**](https://msdn.microsoft.com/library/windows/apps/dn958534) property that indicates how many samples the audio graph needs to fill up the quantum to be processed.
-   Call [**AudioFrameInputNode.AddFrame**](https://msdn.microsoft.com/library/windows/apps/dn914148) to pass an [**AudioFrame**](https://msdn.microsoft.com/library/windows/apps/dn930871) object filled with audio data into the graph.
- A new set of APIs for using **MediaFrameReader** with audio data were introduced in Windows 10, version 1803. These APIs allow you to obtain **AudioFrame** objects from a media frame source, which can be passed into a **FrameInputNode** using the **AddFrame** method. For more information, see [Process audio frames with MediaFrameReader](process-audio-frames-with-mediaframereader.md).
-   An example implementation of the **GenerateAudioData** helper method is shown below.

To populate an [**AudioFrame**](https://msdn.microsoft.com/library/windows/apps/dn930871) with audio data, you must get access to the underlying memory buffer of the audio frame. To do this you must initialize the **IMemoryBufferByteAccess** COM interface by adding the following code within your namespace.

[!code-cs[ComImportIMemoryBufferByteAccess](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetComImportIMemoryBufferByteAccess)]

The following code shows an example implementation of a **GenerateAudioData** helper method that creates an [**AudioFrame**](https://msdn.microsoft.com/library/windows/apps/dn930871) and populates it with audio data.

[!code-cs[GenerateAudioData](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetGenerateAudioData)]

-   Because this method accesses the raw buffer underlying the Windows Runtime types, it must be declared using the **unsafe** keyword. You must also configure your project in Microsoft Visual Studio to allow the compilation of unsafe code by opening the project's **Properties** page, clicking the **Build** property page, and selecting the **Allow Unsafe Code** checkbox.
-   Initialize a new instance of [**AudioFrame**](https://msdn.microsoft.com/library/windows/apps/dn930871), in the **Windows.Media** namespace, by passing in the desired buffer size to the constructor. The buffer size is the number of samples multiplied by the size of each sample.
-   Get the [**AudioBuffer**](https://msdn.microsoft.com/library/windows/apps/dn958454) of the audio frame by calling [**LockBuffer**](https://msdn.microsoft.com/library/windows/apps/dn930878).
-   Get an instance of the [**IMemoryBufferByteAccess**](https://msdn.microsoft.com/library/windows/desktop/mt297505) COM interface from the audio buffer by calling [**CreateReference**](https://msdn.microsoft.com/library/windows/apps/dn958457).
-   Get a pointer to raw audio buffer data by calling [**IMemoryBufferByteAccess.GetBuffer**](https://msdn.microsoft.com/library/windows/desktop/mt297506) and cast it to the sample data type of the audio data.
-   Fill the buffer with data and return the [**AudioFrame**](https://msdn.microsoft.com/library/windows/apps/dn930871) for submission into the audio graph.

##  Audio frame output node

An audio frame output node allows you to receive and process audio data output from the audio graph with custom code that you create. An example scenario for this is performing signal analysis on the audio output. Create an [**AudioFrameOutputNode**](https://msdn.microsoft.com/library/windows/apps/dn914166) by calling [**CreateFrameOutputNode**](https://msdn.microsoft.com/library/windows/apps/dn914233).

[!code-cs[DeclareFrameOutputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetDeclareFrameOutputNode)]

[!code-cs[CreateFrameOutputNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateFrameOutputNode)]

The [**AudioGraph.QuantumStarted**](https://docs.microsoft.com/uwp/api/Windows.Media.Audio.AudioGraph.QuantumStarted) event is raised when the audio graph has begins processing a quantum of audio data. You can access the audio data from within the handler for this event. 

> [!NOTE]
> If you want to retrieve audio frames on a regular cadence, synchronized with the audio graph, call [AudioFrameOutputNode.GetFrame](https://docs.microsoft.com/uwp/api/windows.media.audio.audioframeoutputnode.GetFrame) from within the synchronous **QuantumStarted** event handler. The **QuantumProcessed** event is raised asynchronously after the audio engine has completed audio processing, which means its cadence may be irregular. Therefore you should not use the **QuantumProcessed** event for synchronized processing of audio frame data.

[!code-cs[SnippetQuantumStartedFrameOutput](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetQuantumStartedFrameOutput)]

-   Call [**GetFrame**](https://msdn.microsoft.com/library/windows/apps/dn914171) to get an [**AudioFrame**](https://msdn.microsoft.com/library/windows/apps/dn930871) object filled with audio data from the graph.
-   An example implementation of the **ProcessFrameOutput** helper method is shown below.

[!code-cs[ProcessFrameOutput](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetProcessFrameOutput)]

-   Like the audio frame input node example above, you will need to declare the **IMemoryBufferByteAccess** COM interface and configure your project to allow unsafe code in order to access the underlying audio buffer.
-   Get the [**AudioBuffer**](https://msdn.microsoft.com/library/windows/apps/dn958454) of the audio frame by calling [**LockBuffer**](https://msdn.microsoft.com/library/windows/apps/dn930878).
-   Get an instance of the **IMemoryBufferByteAccess** COM interface from the audio buffer by calling [**CreateReference**](https://msdn.microsoft.com/library/windows/apps/dn958457).
-   Get a pointer to raw audio buffer data by calling **IMemoryBufferByteAccess.GetBuffer** and cast it to the sample data type of the audio data.

## Node connections and submix nodes

All input nodes types expose the **AddOutgoingConnection** method that routes the audio produced by the node to the node that is passed into the method. The following example connects an [**AudioFileInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914108) to an [**AudioDeviceOutputNode**](https://msdn.microsoft.com/library/windows/apps/dn914098), which is a simple setup for playing an audio file on the device's speaker.

[!code-cs[AddOutgoingConnection1](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetAddOutgoingConnection1)]

You can create more than one connection from an input node to other nodes. The following example adds another connection from the [**AudioFileInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914108) to an [**AudioFileOutputNode**](https://msdn.microsoft.com/library/windows/apps/dn914133). Now, the audio from the audio file is played to the device's speaker and is also written out to an audio file.

[!code-cs[AddOutgoingConnection2](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetAddOutgoingConnection2)]

Output nodes can also receive more than one connection from other nodes. In the following example a connection is made from a [**AudioDeviceInputNode**](https://msdn.microsoft.com/library/windows/apps/dn914082) to the [**AudioDeviceOutput**](https://msdn.microsoft.com/library/windows/apps/dn914098) node. Because the output node has connections from the file input node and the device input node, the output will contain a mix of audio from both sources. **AddOutgoingConnection** provides an overload that lets you specify a gain value for the signal passing through the connection.

[!code-cs[AddOutgoingConnection3](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetAddOutgoingConnection3)]

Although output nodes can accept connections from multiple nodes, you may want to create an intermediate mix of signals from one or more nodes before passing the mix to an output. For example, you may want to set the level or apply effects to a subset of the audio signals in a graph. To do this, use the [**AudioSubmixNode**](https://msdn.microsoft.com/library/windows/apps/dn914247). You can connect to a submix node from one or more input nodes or other submix nodes. In the following example, a new submix node is created with [**AudioGraph.CreateSubmixNode**](https://msdn.microsoft.com/library/windows/apps/dn914236). Then, connections are added from a file input node and a frame output node to the submix node. Finally, the submix node is connected to a file output node.

[!code-cs[CreateSubmixNode](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateSubmixNode)]

## Starting and stopping audio graph nodes

When [**AudioGraph.Start**](https://msdn.microsoft.com/library/windows/apps/dn914244) is called, the audio graph begins processing audio data. Every node type provides **Start** and **Stop** methods that cause the individual node to start or stop processing data. When [**AudioGraph.Stop**](https://msdn.microsoft.com/library/windows/apps/dn914245) is called, all audio processing in the all nodes is stopped regardless of the state of individual nodes, but the state of each node can be set while the audio graph is stopped. For example, you could call **Stop** on an individual node while the graph is stopped and then call **AudioGraph.Start**, and the individual node will remain in the stopped state.

All node types expose the **ConsumeInput** property that, when set to false, allows the node to continue audio processing but stops it from consuming any audio data being input from other nodes.

All node types expose the **Reset** method that causes the node to discard any audio data currently in its buffer.

## Adding audio effects

The audio graph API allows you to add audio effects to every type of node in a graph. Output nodes, input nodes, and submix nodes can each have an unlimited number of audio effects, limited only by the capabilities of the hardware.The following example demonstrates adding the built-in echo effect to a submix node.

[!code-cs[AddEffect](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetAddEffect)]

-   All audio effects implement [**IAudioEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn608044). Every node exposes an **EffectDefinitions** property representing the list of effects applied to that node. Add an effect by adding it's definition object to the list.
-   There are several effect definition classes that are provided in the **Windows.Media.Audio** namespace. These include:
    -   [**EchoEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn914276)
    -   [**EqualizerEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn914287)
    -   [**LimiterEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn914306)
    -   [**ReverbEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn914313)
-   You can create your own audio effects that implement [**IAudioEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn608044) and apply them to any node in an audio graph.
-   Every node type exposes a **DisableEffectsByDefinition** method that disables all effects in the node's **EffectDefinitions** list that were added using the specified definition. **EnableEffectsByDefinition** enables the effects with the specified definition.

## Spatial audio
Starting with Windows 10, version 1607, **AudioGraph** supports spatial audio, which allows you to specify the location in 3D space from which audio from any input or submix node is emitted. You can also specify a shape and direction in which audio is emitted,a velocity that will be used to Doppler shift the node's audio, and define a decay model that describes how the audio is attenuated with distance. 

To create an emitter, you can first create a shape in which the sound is projected from the emitter, which can be a cone or omnidirectional. The [**AudioNodeEmitterShape**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeEmitterShape) class provides static methods for creating each of these shapes. Next, create a decay model. This defines how the volume of the audio from the emitter decreases as the distance from the listener increases. The [**CreateNatural**](https://msdn.microsoft.com/library/windows/apps/mt711740) method creates a decay model that emulates the natural decay of sound using a distance squared falloff model. Finally, create an [**AudioNodeEmitterSettings**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeEmitterSettings) object. Currently, this object is only used to enable and disable velocity-based Doppler attenuation of the emitter's audio. Call the [**AudioNodeEmitter**](https://msdn.microsoft.com/library/windows/apps/mt694324.aspx) constructor, passing in the initialization objects you just created. By default, the emitter is placed at the origin, but you can set the position of the emitter with the [**Position**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeEmitter.Position) property.

> [!NOTE]
> Audio node emitters can only process audio that is formatted in mono with a sample rate of 48kHz. Attempting to use stereo audio or audio with a different sample rate will result in an exception.

You assign the emitter to an audio node when you create it by using the overloaded creation method for the type of node you want. In this example, [**CreateFileInputNodeAsync**](https://msdn.microsoft.com/library/windows/apps/dn914225) is used to create a file input node from a specified file and the [**AudioNodeEmitter**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeEmitter) object you want to associate with the node.

[!code-cs[CreateEmitter](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetCreateEmitter)]

The [**AudioDeviceOutputNode**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioDeviceOutputNode) that outputs audio from the graph to the user has a listener object, accessed with the [**Listener**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioDeviceOutputNode.Listener) property, which represents the location, orientation, and velocity of the user in the 3D space. The positions of all of the emitters in the graph are relative to the position and orientation of the emitter object. By default, the listener is located at the origin (0,0,0) facing forward along the Z axis, but you can set it's position and orientation with the [**Position**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeListener.Position) and [**Orientation**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeListener.Orientation) properties.

[!code-cs[Listener](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetListener)]

You can update the location, velocity, and direction of emitters at runtime to simulate the movement of an audio source through 3D space.

[!code-cs[UpdateEmitter](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetUpdateEmitter)]

You can also update the location, velocity, and orientation of the listener object at runtime to simulate the movement of the user through 3D space.

[!code-cs[UpdateListener](./code/AudioGraph/cs/MainPage.xaml.cs#SnippetUpdateListener)]

By default, spatial audio is calculated using Microsoft's head-relative transfer function (HRTF) algorithm to attenuate the audio based on its shape, velocity, and position relative to the listener. You can set the [**SpatialAudioModel**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Audio.AudioNodeEmitter.SpatialAudioModel) property to **FoldDown** to use a simple stereo mix method of simulating spatial audio that is less accurate but requires less CPU and memory resources.

## See also
- [Media playback](media-playback.md)
 

 




