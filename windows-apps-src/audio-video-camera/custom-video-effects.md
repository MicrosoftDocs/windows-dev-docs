---
Description: This article describes how to create a Windows Runtime component that implements the IBasicVideoEffect interface to allow you to create custom effects for video streams.
MS-HAID: dev\_audio\_vid\_camera.custom\_video\_effects
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Custom video effects
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 40a6bd32-a756-400f-ba34-2c5f507262c0
ms.localizationpriority: medium
---
# Custom video effects




This article describes how to create a Windows Runtime component that implements the [**IBasicVideoEffect**](/uwp/api/Windows.Media.Effects.IBasicVideoEffect) interface to create custom effects for video streams. Custom effects can be used with several different Windows Runtime APIs including [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture), which provides access to a device's camera, and [**MediaComposition**](/uwp/api/Windows.Media.Editing.MediaComposition), which allows you to create complex compositions out of media clips.

## Add a custom effect to your app


A custom video effect is defined in a class that implements the [**IBasicVideoEffect**](/uwp/api/Windows.Media.Effects.IBasicVideoEffect) interface. This class can't be included directly in your app's project. Instead, you must use a Windows Runtime component to host your video effect class.

**Add a Windows Runtime component for your video effect**

1.  In Microsoft Visual Studio, with your solution open, go to the **File** menu and select **Add-&gt;New Project**.
2.  Select the **Windows Runtime Component (Universal Windows)** project type.
3.  For this example, name the project *VideoEffectComponent*. This name will be referenced in code later.
4.  Click **OK**.
5.  The project template creates a class called Class1.cs. In **Solution Explorer**, right-click the icon for Class1.cs and select **Rename**.
6.  Rename the file to *ExampleVideoEffect.cs*. Visual Studio will show a prompt asking if you want to update all references to the new name. Click **Yes**.
7.  Open **ExampleVideoEffect.cs** and update the class definition to implement the [**IBasicVideoEffect**](/uwp/api/Windows.Media.Effects.IBasicVideoEffect) interface.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetImplementIBasicVideoEffect":::


You need to include the following namespaces in your effect class file in order to access all of the types used in the examples in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetEffectUsing":::


## Implement the IBasicVideoEffect interface using software processing


Your video effect must implement all of the methods and properties of the [**IBasicVideoEffect**](/uwp/api/Windows.Media.Effects.IBasicVideoEffect) interface. This section walks you through a simple implementation of this interface that uses software processing.

### Close method

The system will call the [**Close**](/uwp/api/windows.media.effects.ibasicvideoeffect.close) method on your class when the effect should shut down. You should use this method to dispose of any resources you have created. The argument to the method is a [**MediaEffectClosedReason**](/uwp/api/Windows.Media.Effects.MediaEffectClosedReason) that lets you know whether the effect was closed normally, if an error occurred, or if the effect does not support the required encoding format.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetClose":::


### DiscardQueuedFrames method

The [**DiscardQueuedFrames**](/uwp/api/windows.media.effects.ibasicvideoeffect.discardqueuedframes) method is called when your effect should reset. A typical scenario for this is if your effect stores previously processed frames to use in processing the current frame. When this method is called, you should dispose of the set of previous frames you saved. This method can be used to reset any state related to previous frames, not only accumulated video frames.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetDiscardQueuedFrames":::



### IsReadOnly property

The [**IsReadOnly**](/uwp/api/windows.media.effects.ibasicvideoeffect.isreadonly) property lets the system know if your effect will write to the output of the effect. If your app does not modify the video frames (for example, an effect that only performs analysis of the video frames), you should set this property to true, which will cause the system to efficiently copy the frame input to the frame output for you.

> [!TIP]
> When the [**IsReadOnly**](/uwp/api/windows.media.effects.ibasicvideoeffect.isreadonly) property is set to true, the system copies the input frame to the output frame before [**ProcessFrame**](/uwp/api/windows.media.effects.ibasicvideoeffect.processframe) is called. Setting the **IsReadOnly** property to true does not restrict you from writing to the effect's output frames in **ProcessFrame**.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetIsReadOnly":::

### SetEncodingProperties method

The system calls [**SetEncodingProperties**](/uwp/api/windows.media.effects.ibasicvideoeffect.setencodingproperties) on your effect to let you know the encoding properties for the video stream upon which the effect is operating. This method also provides a reference to the Direct3D device used for hardware rendering. The usage of this device is shown in the hardware processing example later in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetSetEncodingProperties":::


### SupportedEncodingProperties property

The system checks the [**SupportedEncodingProperties**](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedencodingproperties) property to determine which encoding properties are supported by your effect. Note that if the consumer of your effect can't encode video using the properties you specify, it will call [**Close**](/uwp/api/windows.media.effects.ibasicvideoeffect.close) on your effect and will remove your effect from the video pipeline.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetSupportedEncodingProperties":::


> [!NOTE] 
> If you return an empty list of [**VideoEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.VideoEncodingProperties) objects from **SupportedEncodingProperties**, the system will default to ARGB32 encoding.

 

### SupportedMemoryTypes property

The system checks the [**SupportedMemoryTypes**](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedmemorytypes) property to determine whether your effect will access video frames in software memory or in hardware (GPU) memory. If you return [**MediaMemoryTypes.Cpu**](/uwp/api/Windows.Media.Effects.MediaMemoryTypes), your effect will be passed input and output frames that contain image data in [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects. If you return **MediaMemoryTypes.Gpu**, your effect will be passed input and output frames that contain image data in [**IDirect3DSurface**](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) objects.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetSupportedMemoryTypes":::


> [!NOTE]
> If you specify [**MediaMemoryTypes.GpuAndCpu**](/uwp/api/Windows.Media.Effects.MediaMemoryTypes), the system will use either GPU or system memory, whichever is more efficient for the pipeline. When using this value, you must check in the [**ProcessFrame**](/uwp/api/windows.media.effects.ibasicvideoeffect.processframe) method to see whether the [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) or [**IDirect3DSurface**](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) passed into the method contains data, and then process the frame accordingly.

 

### TimeIndependent property

The [**TimeIndependent**](/uwp/api/windows.media.effects.ibasicvideoeffect.timeindependent) property lets the system know if your effect does not require uniform timing. When set to true, the system can use optimizations that enhance effect performance.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetTimeIndependent":::

### SetProperties method

The [**SetProperties**](/uwp/api/windows.media.imediaextension.setproperties) method allows the app that is using your effect to adjust effect parameters. Properties are passed as an [**IPropertySet**](/uwp/api/Windows.Foundation.Collections.IPropertySet) map of property names and values.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetSetProperties":::


This simple example will dim the pixels in each video frame according to a specified value. A property is declared and TryGetValue is used to get the value set by the calling app. If no value was set, a default value of .5 is used.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetFadeValue":::


### ProcessFrame method

The [**ProcessFrame**](/uwp/api/windows.media.effects.ibasicvideoeffect.processframe) method is where your effect modifies the image data of the video. The method is called once per frame and is passed a [**ProcessVideoFrameContext**](/uwp/api/Windows.Media.Effects.ProcessVideoFrameContext) object. This object contains an input [**VideoFrame**](/uwp/api/Windows.Media.VideoFrame) object that contains the incoming frame to be processed and an output **VideoFrame** object to which you write image data that will be passed on to rest of the video pipeline. Each of these **VideoFrame** objects has a [**SoftwareBitmap**](/uwp/api/windows.media.videoframe.softwarebitmap) property and a [**Direct3DSurface**](/uwp/api/windows.media.videoframe.direct3dsurface) property, but which of these can be used is determined by the value you returned from the [**SupportedMemoryTypes**](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedmemorytypes) property.

This example shows a simple implementation of the **ProcessFrame** method using software processing. For more information about working with [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects, see [Imaging](imaging.md). An example **ProcessFrame** implementation using hardware processing is shown later in this article.

Accessing the data buffer of a **SoftwareBitmap** requires COM interop, so you should include the **System.Runtime.InteropServices** namespace in your effect class file.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetCOMUsing":::


Add the following code inside the namespace for your effect to import the interface for accessing the image buffer.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetCOMImport":::


> [!NOTE]
> Because this technique accesses a native, unmanaged image buffer, you will need to configure your project to allow unsafe code.
> 1.  In Solution Explorer, right-click the VideoEffectComponent project and select **Properties**.
> 2.  Select the **Build** tab.
> 3.  Select the **Allow unsafe code** check box.

 

Now you can add the **ProcessFrame** method implementation. First, this method obtains a [**BitmapBuffer**](/uwp/api/Windows.Graphics.Imaging.BitmapBuffer) object from both the input and output software bitmaps. Note that the output frame is opened for writing and the input for reading. Next, an [**IMemoryBufferReference**](/uwp/api/Windows.Foundation.IMemoryBufferReference) is obtained for each buffer by calling [**CreateReference**](/uwp/api/windows.graphics.imaging.bitmapbuffer.createreference). Then, the actual data buffer is obtained by casting the **IMemoryBufferReference** objects as the COM interop interface defined above, **IMemoryByteAccess**, and then calling **GetBuffer**.

Now that the data buffers have been obtained, you can read from the input buffer and write to the output buffer. The layout of the buffer is obtained by calling [**GetPlaneDescription**](/uwp/api/windows.graphics.imaging.bitmapbuffer.getplanedescription), which provides information on the width, stride, and initial offset of the buffer. The bits per pixel is determined by the encoding properties set previously with the [**SetEncodingProperties**](/uwp/api/windows.media.effects.ibasicvideoeffect.setencodingproperties) method. The buffer format information is used to find the index into the buffer for each pixel. The pixel value from the source buffer is copied into the target buffer, with the color values being multiplied by the FadeValue property defined for this effect to dim them by the specified amount.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs" id="SnippetProcessFrameSoftwareBitmap":::


## Implement the IBasicVideoEffect interface using hardware processing


Creating a custom video effect by using hardware (GPU) processing is almost identical to using software processing as described above. This section will show the few differences in an effect that uses hardware processing. This example uses the Win2D Windows Runtime API. For more information about using Win2D, see the [Win2D documentation](https://microsoft.github.io/Win2D/html/Introduction.htm).

Use the following steps to add the Win2D NuGet package to the project you created as described in the **Add a custom effect to your app** section at the beginning of this article.

**To add the Win2D NuGet package to your effect project**

1.  In **Solution Explorer**, right-click the **VideoEffectComponent** project and select **Manage NuGet Packages**.
2.  At the top of the window, select the **Browse** tab.
3.  In the search box, enter **Win2D**.
4.  Select **Win2D.uwp**, and then select **Install** in the right pane.
5.  The **Review Changes** dialog shows you the package to be installed. Click **OK**.
6.  Accept the package license.

In addition to the namespaces included in the basic project setup, you will need to include the following namespaces provided by Win2D.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetUsingWin2D":::


Because this effect will use GPU memory for operating on the image data, you should return [**MediaMemoryTypes.Gpu**](/uwp/api/Windows.Media.Effects.MediaMemoryTypes) from the [**SupportedMemoryTypes**](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedmemorytypes) property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetSupportedMemoryTypesWin2D":::


Set the encoding properties that your effect will support with the [**SupportedEncodingProperties**](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedencodingproperties) property. When working with Win2D, you must use ARGB32 encoding.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetSupportedEncodingPropertiesWin2D":::


Use the [**SetEncodingProperties**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) method to create a new Win2D **CanvasDevice** object from the [**IDirect3DDevice**](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice) passed into the method.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetSetEncodingPropertiesWin2D":::


The [**SetProperties**](/uwp/api/windows.media.imediaextension.setproperties) implementation is identical to the previous software processing example. This example uses a **BlurAmount** property to configure a Win2D blur effect.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetSetPropertiesWin2D":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetBlurAmountWin2D":::


The last step is to implement the [**ProcessFrame**](/uwp/api/windows.media.effects.ibasicvideoeffect.processframe) method that actually processes the image data.

Using Win2D APIs, a **CanvasBitmap** is created from the input frame's [**Direct3DSurface**](/uwp/api/windows.media.videoframe.direct3dsurface) property. A **CanvasRenderTarget** is created from the output frame's **Direct3DSurface** and a **CanvasDrawingSession** is created from this render target. A new Win2D **GaussianBlurEffect** is initialized, using the **BlurAmount** property our effect exposes via [**SetProperties**](/uwp/api/windows.media.imediaextension.setproperties). Finally, the **CanvasDrawingSession.DrawImage** method is called to draw the input bitmap to the render target using the blur effect.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs" id="SnippetProcessFrameWin2D":::


## Adding your custom effect to your app


To use your video effect from your app, you must add a reference to the effect project to your app.

1.  In Solution Explorer, under your app project, right-click **References** and select **Add reference**.
2.  Expand the **Projects** tab, select **Solution**, and then select the check box for your effect project name. For this example, the name is *VideoEffectComponent*.
3.  Click **OK**.

### Add your custom effect to a camera video stream

You can set up a simple preview stream from the camera by following the steps in the article [Simple camera preview access](simple-camera-preview-access.md). Following those steps will provide you with an initialized [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object that is used to access the camera's video stream.

To add your custom video effect to a camera stream, first create a new [**VideoEffectDefinition**](/uwp/api/Windows.Media.Effects.VideoEffectDefinition) object, passing in the namespace and class name for your effect. Next, call the **MediaCapture** object's [**AddVideoEffect**](/uwp/api/windows.media.capture.mediacapture.addvideoeffectasync) method to add your effect to the specified stream. This example uses the [**MediaStreamType.VideoPreview**](/uwp/api/Windows.Media.Capture.MediaStreamType) value to specify that the effect should be added to the preview stream. If your app supports video capture, you could also use **MediaStreamType.VideoRecord** to add the effect to the capture stream. **AddVideoEffect** returns an [**IMediaExtension**](/uwp/api/Windows.Media.IMediaExtension) object representing your custom effect. You can use the SetProperties method to set the configuration for your effect.

After the effect has been added, [**StartPreviewAsync**](/uwp/api/windows.media.capture.mediacapture.startpreviewasync) is called to start the preview stream.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffect_Win10/MainPage.xaml.cs" id="SnippetAddVideoEffectAsync":::



### Add your custom effect to a clip in a MediaComposition

For general guidance for creating media compositions from video clips, see [Media compositions and editing](media-compositions-and-editing.md). The following code snippet shows the creation of a simple media composition that uses a custom video effect. A [**MediaClip**](/uwp/api/Windows.Media.Editing.MediaClip) object is created by calling [**CreateFromFileAsync**](/uwp/api/windows.media.editing.mediaclip.createfromfileasync), passing in a video file that was selected by the user with a [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker), and the clip is added to a new [**MediaComposition**](/uwp/api/Windows.Media.Editing.MediaComposition). Next a new [**VideoEffectDefinition**](/uwp/api/Windows.Media.Effects.VideoEffectDefinition) object is created, passing in the namespace and class name for your effect to the constructor. Finally, the effect definition is added to the [**VideoEffectDefinitions**](/uwp/api/windows.media.editing.mediaclip.videoeffectdefinitions) collection of the **MediaClip** object.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/VideoEffect_Win10/cs/VideoEffect_Win10/MainPage.xaml.cs" id="SnippetAddEffectToComposition":::


## Related topics
* [Simple camera preview access](simple-camera-preview-access.md)
* [Media compositions and editing](media-compositions-and-editing.md)
* [Win2D documentation](https://microsoft.github.io/Win2D/html/Introduction.htm)
* [Media playback](media-playback.md)
