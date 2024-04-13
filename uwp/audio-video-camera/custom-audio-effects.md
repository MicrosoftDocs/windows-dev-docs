---
description: This article describes how to create a Windows Runtime component that implements the IBasicAudioEffect interface to allow you to create custom effects for audio streams.
title: Custom audio effects
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 360faf3f-7e73-4db4-8324-3391f801d827
ms.localizationpriority: medium
---
# Custom audio effects

This article describes how to create a Windows Runtime component that implements the [**IBasicAudioEffect**](/uwp/api/Windows.Media.Effects.IBasicAudioEffect) interface to create custom effects for audio streams. Custom effects can be used with several different Windows Runtime APIs including [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture), which provides access to a device's camera, [**MediaComposition**](/uwp/api/Windows.Media.Editing.MediaComposition), which allows you to create complex compositions out of media clips, and [**AudioGraph**](/uwp/api/Windows.Media.Audio.AudioGraph) which allows you to quickly assemble a graph of various audio input, output, and submix nodes.

## Add a custom effect to your app


A custom audio effect is defined in a class that implements the [**IBasicAudioEffect**](/uwp/api/Windows.Media.Effects.IBasicAudioEffect) interface. This class can't be included directly in your app's project. Instead, you must use a Windows Runtime component to host your audio effect class.

**Add a Windows Runtime component for your audio effect**

1.  In Microsoft Visual Studio, with your solution open, go to the **File** menu and select **Add-&gt;New Project**.
2.  Select the **Windows Runtime Component (Universal Windows)** project type.
3.  For this example, name the project *AudioEffectComponent*. This name will be referenced in code later.
4.  Click **OK**.
5.  The project template creates a class called Class1.cs. In **Solution Explorer**, right-click the icon for Class1.cs and select **Rename**.
6.  Rename the file to *ExampleAudioEffect.cs*. Visual Studio will show a prompt asking if you want to update all references to the new name. Click **Yes**.
7.  Open **ExampleAudioEffect.cs** and update the class definition to implement the [**IBasicAudioEffect**](/uwp/api/Windows.Media.Effects.IBasicAudioEffect) interface.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetImplementIBasicAudioEffect":::

You need to include the following namespaces in your effect class file in order to access all of the types used in the examples in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetEffectUsing":::

## Implement the IBasicAudioEffect interface

Your audio effect must implement all of the methods and properties of the [**IBasicAudioEffect**](/uwp/api/Windows.Media.Effects.IBasicAudioEffect) interface. This section walks you through a simple implementation of this interface to create a basic echo effect.

### SupportedEncodingProperties property

The system checks the [**SupportedEncodingProperties**](/uwp/api/windows.media.effects.ibasicaudioeffect.supportedencodingproperties) property to determine which encoding properties are supported by your effect. Note that if the consumer of your effect can't encode audio using the properties you specify, the system will call [**Close**](/uwp/api/windows.media.effects.ibasicaudioeffect.close) on your effect and will remove your effect from the audio pipeline. In this example,  [**AudioEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.AudioEncodingProperties) objects are created and added to the returned list to support 44.1 kHz and 48 kHz, 32-bit float, mono encoding.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetSupportedEncodingProperties":::

### SetEncodingProperties method

The system calls [**SetEncodingProperties**](/uwp/api/windows.media.effects.ibasicvideoeffect.setencodingproperties) on your effect to let you know the encoding properties for the audio stream upon which the effect is operating. In order to implement an echo effect, this example uses a buffer to store one second of audio data. This method provides the opportunity to initialize the size of the buffer to the number of samples in one second of audio, based on the sample rate in which the audio is encoded. The delay effect also uses an integer counter to keep track of the current position in the delay buffer. Since **SetEncodingProperties** is called whenever the effect is added to the audio pipeline, this is a good time to initialize that value to 0. You may also want to capture the **AudioEncodingProperties** object passed into this method to use elsewhere in your effect.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetDeclareEchoBuffer":::
:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetSetEncodingProperties":::


### SetProperties method

The [**SetProperties**](/uwp/api/windows.media.imediaextension.setproperties) method allows the app that is using your effect to adjust effect parameters. Properties are passed as an [**IPropertySet**](/uwp/api/Windows.Foundation.Collections.IPropertySet) map of property names and values.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetSetProperties":::

This simple example will mix the current audio sample with a value from the delay buffer according the value of the **Mix** property. A property is declared and TryGetValue is used to get the value set by the calling app. If no value was set, a default value of .5 is used. Note that this property is read-only. The property value must be set using **SetProperties**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetMixProperty":::

### ProcessFrame method

The [**ProcessFrame**](/uwp/api/windows.media.effects.ibasicaudioeffect.processframe) method is where your effect modifies the audio data of the stream. The method is called once per frame and is passed a [**ProcessAudioFrameContext**](/uwp/api/Windows.Media.Effects.ProcessAudioFrameContext) object. This object contains an input [**AudioFrame**](/uwp/api/Windows.Media.AudioFrame) object that contains the incoming frame to be processed and an output **AudioFrame** object to which you write audio data that will be passed on to rest of the audio pipeline. An audio frame is a buffer of audio samples representing a short slice of audio data.

Accessing the data buffer of a **AudioFrame** requires COM interop, so you should include the **System.Runtime.InteropServices** namespace in your effect class file and then add the following code inside the namespace for your effect to import the interface for accessing the audio buffer.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetComImport":::

> [!NOTE]
> Because this technique accesses a native, unmanaged image buffer, you will need to configure your project to allow unsafe code.
> 1.  In Solution Explorer, right-click the AudioEffectComponent project and select **Properties**.
> 2.  Select the **Build** tab.
> 3.  Select the **Allow unsafe code** check box.

 

Now you can add the **ProcessFrame** method implementation to your effect. First, this method obtains a [**AudioBuffer**](/uwp/api/Windows.Media.AudioBuffer) object from both the input and output audio frames. Note that the output frame is opened for writing and the input for reading. Next, an [**IMemoryBufferReference**](/uwp/api/Windows.Foundation.IMemoryBufferReference) is obtained for each buffer by calling [**CreateReference**](/uwp/api/windows.graphics.imaging.bitmapbuffer.createreference). Then, the actual data buffer is obtained by casting the **IMemoryBufferReference** objects as the COM interop interface defined above, **IMemoryByteAccess**, and then calling **GetBuffer**.

Now that the data buffers have been obtained, you can read from the input buffer and write to the output buffer. For each sample in the inputbuffer, the value is obtained and multiplied by 1 - **Mix** to set the dry signal value of the effect. Next, a sample is retrieved from the current position in the echo buffer and multiplied by **Mix** to set the wet value of the effect. The output sample is set to the sum of the dry and wet values. Finally, each input sample is stored in the echo buffer and the current sample index is incremented.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetProcessFrame":::



### Close method

The system will call the [**Close**](/uwp/api/windows.media.effects.ibasicaudioeffect.close) [**Close**](/uwp/api/windows.media.effects.ibasicaudioeffect.close) method on your class when the effect should shut down. You should use this method to dispose of any resources you have created. The argument to the method is a [**MediaEffectClosedReason**](/uwp/api/Windows.Media.Effects.MediaEffectClosedReason) that lets you know whether the effect was closed normally, if an error occurred, or if the effect does not support the required encoding format.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetClose":::

### DiscardQueuedFrames method

The [**DiscardQueuedFrames**](/uwp/api/windows.media.effects.ibasicvideoeffect.discardqueuedframes) method is called when your effect should reset. A typical scenario for this is if your effect stores previously processed frames to use in processing the current frame. When this method is called, you should dispose of the set of previous frames you saved. This method can be used to reset any state related to previous frames, not only accumulated audio frames.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetDiscardQueuedFrames":::

### TimeIndependent property

The TimeIndependent [**TimeIndependent**](/uwp/api/windows.media.effects.ibasicvideoeffect.timeindependent) property lets the system know if your effect does not require uniform timing. When set to true, the system can use optimizations that enhance effect performance.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetTimeIndependent":::

### UseInputFrameForOutput property

Set the [**UseInputFrameForOutput**](/uwp/api/windows.media.effects.ibasicaudioeffect.useinputframeforoutput) property to **true** to tell the system that your effect will write its output to the audio buffer of the  [**InputFrame**](/uwp/api/windows.media.effects.processaudioframecontext.inputframe) of the [**ProcessAudioFrameContext**](/uwp/api/Windows.Media.Effects.ProcessAudioFrameContext) passed into [**ProcessFrame**](/uwp/api/windows.media.effects.ibasicaudioeffect.processframe) instead of writing to the [**OutputFrame**](/uwp/api/windows.media.effects.processaudioframecontext.outputframe). 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/AudioEffectComponent/ExampleAudioEffect.cs" id="SnippetUseInputFrameForOutput":::


## Adding your custom effect to your app


To use your audio effect from your app, you must add a reference to the effect project to your app.

1.  In Solution Explorer, under your app project, right-click **References** and select **Add reference**.
2.  Expand the **Projects** tab, select **Solution**, and then select the check box for your effect project name. For this example, the name is *AudioEffectComponent*.
3.  Click **OK**

If your audio effect class is declared is a different namespace, be sure to include that namespace in your code file.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/MainPage.xaml.cs" id="SnippetUsingAudioEffectComponent":::

### Add your custom effect to an AudioGraph node
For general information about using audio graphs, see [Audio graphs](audio-graphs.md). The following code snippet shows you how to add the example echoe effect shown in this article to an audio graph node. First, a [**PropertySet**](/uwp/api/Windows.Foundation.Collections.PropertySet) is created and a value for the **Mix** property, defined by the effect, is set. Next, the [**AudioEffectDefinition**](/uwp/api/Windows.Media.Effects.AudioEffectDefinition) constructor is called, passing in the full class name of the custom effect type and the property set. Finally, the effect definition is added to the [**EffectDefinitions**](/uwp/api/windows.media.audio.audiofileinputnode.effectdefinitions) property of an existing [**FileInputNode**](/uwp/api/windows.media.audio.createaudiofileinputnoderesult.fileinputnode), causing the audio emitted to be processed by the custom effect. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioGraph/cs/MainPage.xaml.cs" id="SnippetAddCustomEffect":::

After it has been added to a node, the custom effect can be disabled by calling [**DisableEffectsByDefinition**](/uwp/api/windows.media.audio.audiofileinputnode.disableeffectsbydefinition) and passing in the **AudioEffectDefinition** object. For more information about using audio graphs in your app, see [AudioGraph](audio-graphs.md).

### Add your custom effect to a clip in a MediaComposition

The following code snippet demonstrates adding the custom audio effect to a video clip and a background audio track in a media composition. For general guidance for creating media compositions from video clips and adding background audio tracks, see [Media compositions and editing](media-compositions-and-editing.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/MediaEditing/cs/MainPage.xaml.cs" id="SnippetAddCustomAudioEffect":::



## Related topics
* [Simple camera preview access](simple-camera-preview-access.md)
* [Media compositions and editing](media-compositions-and-editing.md)
* [Win2D documentation](https://microsoft.github.io/Win2D/html/Introduction.htm)
* [Media playback](media-playback.md)

 
