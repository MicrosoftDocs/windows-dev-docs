---
author: drewbatgit
Description: This article describes how to create a Windows Runtime component that implements the IBasicVideoEffect interface to allow you to create custom effects for video streams.
MS-HAID: dev\_audio\_vid\_camera.custom\_video\_effects
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Custom video effects
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 40a6bd32-a756-400f-ba34-2c5f507262c0
ms.localizationpriority: medium
---

# Custom video effects




This article describes how to create a Windows Runtime component that implements the [**IBasicVideoEffect**](https://msdn.microsoft.com/library/windows/apps/dn764788) interface to create custom effects for video streams. Custom effects can be used with several different Windows Runtime APIs including [MediaCapture](https://msdn.microsoft.com/library/windows/apps/br241124), which provides access to a device's camera, and [**MediaComposition**](https://msdn.microsoft.com/library/windows/apps/dn652646), which allows you to create complex compositions out of media clips.

## Add a custom effect to your app


A custom video effect is defined in a class that implements the [**IBasicVideoEffect**](https://msdn.microsoft.com/library/windows/apps/dn764788) interface. This class can't be included directly in your app's project. Instead, you must use a Windows Runtime component to host your video effect class.

**Add a Windows Runtime component for your video effect**

1.  In Microsoft Visual Studio, with your solution open, go to the **File** menu and select **Add-&gt;New Project**.
2.  Select the **Windows Runtime Component (Universal Windows)** project type.
3.  For this example, name the project *VideoEffectComponent*. This name will be referenced in code later.
4.  Click **OK**.
5.  The project template creates a class called Class1.cs. In **Solution Explorer**, right-click the icon for Class1.cs and select **Rename**.
6.  Rename the file to *ExampleVideoEffect.cs*. Visual Studio will show a prompt asking if you want to update all references to the new name. Click **Yes**.
7.  Open **ExampleVideoEffect.cs** and update the class definition to implement the [**IBasicVideoEffect**](https://msdn.microsoft.com/library/windows/apps/dn764788) interface.

[!code-cs[ImplementIBasicVideoEffect](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetImplementIBasicVideoEffect)]


You need to include the following namespaces in your effect class file in order to access all of the types used in the examples in this article.

[!code-cs[EffectUsing](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetEffectUsing)]


## Implement the IBasicVideoEffect interface using software processing


Your video effect must implement all of the methods and properties of the [**IBasicVideoEffect**](https://msdn.microsoft.com/library/windows/apps/dn764788) interface. This section walks you through a simple implementation of this interface that uses software processing.

### Close method

The system will call the [**Close**](https://msdn.microsoft.com/library/windows/apps/dn764789) method on your class when the effect should shut down. You should use this method to dispose of any resources you have created. The argument to the method is a [**MediaEffectClosedReason**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Effects.MediaEffectClosedReason) that lets you know whether the effect was closed normally, if an error occurred, or if the effect does not support the required encoding format.

[!code-cs[Close](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetClose)]


### DiscardQueuedFrames method

The [**DiscardQueuedFrames**](https://msdn.microsoft.com/library/windows/apps/dn764790) method is called when your effect should reset. A typical scenario for this is if your effect stores previously processed frames to use in processing the current frame. When this method is called, you should dispose of the set of previous frames you saved. This method can be used to reset any state related to previous frames, not only accumulated video frames.


[!code-cs[DiscardQueuedFrames](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetDiscardQueuedFrames)]



### IsReadOnly property

The [**IsReadOnly**](https://msdn.microsoft.com/library/windows/apps/dn764792) property lets the system know if your effect will write to the output of the effect. If your app does not modify the video frames (for example, an effect that only performs analysis of the video frames), you should set this property to true, which will cause the system to efficiently copy the frame input to the frame output for you.

> [!TIP]
> When the [**IsReadOnly**](https://msdn.microsoft.com/library/windows/apps/dn764792) property is set to true, the system copies the input frame to the output frame before [**ProcessFrame**](https://msdn.microsoft.com/library/windows/apps/dn764794) is called. Setting the **IsReadOnly** property to true does not restrict you from writing to the effect's output frames in **ProcessFrame**.


[!code-cs[IsReadOnly](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetIsReadOnly)]

### SetEncodingProperties method

The system calls [**SetEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/dn919884) on your effect to let you know the encoding properties for the video stream upon which the effect is operating. This method also provides a reference to the Direct3D device used for hardware rendering. The usage of this device is shown in the hardware processing example later in this article.

[!code-cs[SetEncodingProperties](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetSetEncodingProperties)]


### SupportedEncodingProperties property

The system checks the [**SupportedEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/dn764799) property to determine which encoding properties are supported by your effect. Note that if the consumer of your effect can't encode video using the properties you specify, it will call [**Close**](https://msdn.microsoft.com/library/windows/apps/dn764789) on your effect and will remove your effect from the video pipeline.


[!code-cs[SupportedEncodingProperties](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetSupportedEncodingProperties)]


> [!NOTE] 
> If you return an empty list of [**VideoEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/hh701217) objects from **SupportedEncodingProperties**, the system will default to ARGB32 encoding.

 

### SupportedMemoryTypes property

The system checks the [**SupportedMemoryTypes**](https://msdn.microsoft.com/library/windows/apps/dn764801) property to determine whether your effect will access video frames in software memory or in hardware (GPU) memory. If you return [**MediaMemoryTypes.Cpu**](https://msdn.microsoft.com/library/windows/apps/dn764822), your effect will be passed input and output frames that contain image data in [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/dn887358) objects. If you return **MediaMemoryTypes.Gpu**, your effect will be passed input and output frames that contain image data in [**IDirect3DSurface**](https://msdn.microsoft.com/library/windows/apps/dn965505) objects.

[!code-cs[SupportedMemoryTypes](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetSupportedMemoryTypes)]


> [!NOTE]
> If you specify [**MediaMemoryTypes.GpuAndCpu**](https://msdn.microsoft.com/library/windows/apps/dn764822), the system will use either GPU or system memory, whichever is more efficient for the pipeline. When using this value, you must check in the [**ProcessFrame**](https://msdn.microsoft.com/library/windows/apps/dn764794) method to see whether the [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/dn887358) or [**IDirect3DSurface**](https://msdn.microsoft.com/library/windows/apps/dn965505) passed into the method contains data, and then process the frame accordingly.

 

### TimeIndependent property

The [**TimeIndependent**](https://msdn.microsoft.com/library/windows/apps/dn764803) property lets the system know if your effect does not require uniform timing. When set to true, the system can use optimizations that enhance effect performance.

[!code-cs[TimeIndependent](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetTimeIndependent)]

### SetProperties method

The [**SetProperties**](https://msdn.microsoft.com/library/windows/apps/br240986) method allows the app that is using your effect to adjust effect parameters. Properties are passed as an [**IPropertySet**](https://msdn.microsoft.com/library/windows/apps/br226054) map of property names and values.


[!code-cs[SetProperties](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetSetProperties)]


This simple example will dim the pixels in each video frame according to a specified value. A property is declared and TryGetValue is used to get the value set by the calling app. If no value was set, a default value of .5 is used.

[!code-cs[FadeValue](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetFadeValue)]


### ProcessFrame method

The [**ProcessFrame**](https://msdn.microsoft.com/library/windows/apps/dn764794) method is where your effect modifies the image data of the video. The method is called once per frame and is passed a [**ProcessVideoFrameContext**](https://msdn.microsoft.com/library/windows/apps/dn764826) object. This object contains an input [**VideoFrame**](https://msdn.microsoft.com/library/windows/apps/dn930917) object that contains the incoming frame to be processed and an output **VideoFrame** object to which you write image data that will be passed on to rest of the video pipeline. Each of these **VideoFrame** objects has a [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/dn930926) property and a [**Direct3DSurface**](https://msdn.microsoft.com/library/windows/apps/dn930920) property, but which of these can be used is determined by the value you returned from the [**SupportedMemoryTypes**](https://msdn.microsoft.com/library/windows/apps/dn764801) property.

This example shows a simple implementation of the **ProcessFrame** method using software processing. For more information about working with [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/dn887358) objects, see [Imaging](imaging.md). An example **ProcessFrame** implementation using hardware processing is shown later in this article.

Accessing the data buffer of a **SoftwareBitmap** requires COM interop, so you should include the **System.Runtime.InteropServices** namespace in your effect class file.

[!code-cs[COMUsing](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetCOMUsing)]


Add the following code inside the namespace for your effect to import the interface for accessing the image buffer.

[!code-cs[COMImport](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetCOMImport)]


> [!NOTE]
> Because this technique accesses a native, unmanaged image buffer, you will need to configure your project to allow unsafe code.
> 1.  In Solution Explorer, right-click the VideoEffectComponent project and select **Properties**.
> 2.  Select the **Build** tab.
> 3.  Select the **Allow unsafe code** check box.

 

Now you can add the **ProcessFrame** method implementation. First, this method obtains a [**BitmapBuffer**](https://msdn.microsoft.com/library/windows/apps/dn887325) object from both the input and output software bitmaps. Note that the output frame is opened for writing and the input for reading. Next, an [**IMemoryBufferReference**](https://msdn.microsoft.com/library/windows/apps/dn921671) is obtained for each buffer by calling [**CreateReference**](https://msdn.microsoft.com/library/windows/apps/dn949046). Then, the actual data buffer is obtained by casting the **IMemoryBufferReference** objects as the COM interop interface defined above, **IMemoryByteAccess**, and then calling **GetBuffer**.

Now that the data buffers have been obtained, you can read from the input buffer and write to the output buffer. The layout of the buffer is obtained by calling [**GetPlaneDescription**](https://msdn.microsoft.com/library/windows/apps/dn887330), which provides information on the width, stride, and initial offset of the buffer. The bits per pixel is determined by the encoding properties set previously with the [**SetEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/dn919884) method. The buffer format information is used to find the index into the buffer for each pixel. The pixel value from the source buffer is copied into the target buffer, with the color values being multiplied by the FadeValue property defined for this effect to dim them by the specified amount.

[!code-cs[ProcessFrameSoftwareBitmap](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffect.cs#SnippetProcessFrameSoftwareBitmap)]


## Implement the IBasicVideoEffect interface using hardware processing


Creating a custom video effect by using hardware (GPU) processing is almost identical to using software processing as described above. This section will show the few differences in an effect that uses hardware processing. This example uses the Win2D Windows Runtime API. For more information about using Win2D, see the [Win2D documentation](http://go.microsoft.com/fwlink/?LinkId=519078).

Use the following steps to add the Win2D NuGet package to the project you created as described in the **Add a custom effect to your app** section at the beginning of this article.

**To add the Win2D NuGet package to your effect project**

1.  In **Solution Explorer**, right-click the **VideoEffectComponent** project and select **Manage NuGet Packages**.
2.  At the top of the window, select the **Browse** tab.
3.  In the search box, enter **Win2D**.
4.  Select **Win2D.uwp**, and then select **Install** in the right pane.
5.  The **Review Changes** dialog shows you the package to be installed. Click **OK**.
6.  Accept the package license.

In addition to the namespaces included in the basic project setup, you will need to include the following namespaces provided by Win2D.

[!code-cs[UsingWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetUsingWin2D)]


Because this effect will use GPU memory for operating on the image data, you should return [**MediaMemoryTypes.Gpu**](https://msdn.microsoft.com/library/windows/apps/dn764822) from the [**SupportedMemoryTypes**](https://msdn.microsoft.com/library/windows/apps/dn764801) property.

[!code-cs[SupportedMemoryTypesWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetSupportedMemoryTypesWin2D)]


Set the encoding properties that your effect will support with the [**SupportedEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/dn764799) property. When working with Win2D, you must use ARGB32 encoding.

[!code-cs[SupportedEncodingPropertiesWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetSupportedEncodingPropertiesWin2D)]


Use the [**SetEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/dn919884) method to create a new Win2D **CanvasDevice** object from the [**IDirect3DDevice**](https://msdn.microsoft.com/library/windows/apps/dn895092) passed into the method.

[!code-cs[SetEncodingPropertiesWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetSetEncodingPropertiesWin2D)]


The [**SetProperties**](https://msdn.microsoft.com/library/windows/apps/br240986) implementation is identical to the previous software processing example. This example uses a **BlurAmount** property to configure a Win2D blur effect.

[!code-cs[SetPropertiesWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetSetPropertiesWin2D)]

[!code-cs[BlurAmountWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetBlurAmountWin2D)]


The last step is to implement the [**ProcessFrame**](https://msdn.microsoft.com/library/windows/apps/dn764794) method that actually processes the image data.

Using Win2D APIs, a **CanvasBitmap** is created from the input frame's [**Direct3DSurface**](https://msdn.microsoft.com/library/windows/apps/dn930920) property. A **CanvasRenderTarget** is created from the output frame's **Direct3DSurface** and a **CanvasDrawingSession** is created from this render target. A new Win2D **GaussianBlurEffect** is initialized, using the **BlurAmount** property our effect exposes via [**SetProperties**](https://msdn.microsoft.com/library/windows/apps/br240986). Finally, the **CanvasDrawingSession.DrawImage** method is called to draw the input bitmap to the render target using the blur effect.

[!code-cs[ProcessFrameWin2D](./code/VideoEffect_Win10/cs/VideoEffectComponent/ExampleVideoEffectWin2D.cs#SnippetProcessFrameWin2D)]


## Adding your custom effect to your app


To use your video effect from your app, you must add a reference to the effect project to your app.

1.  In Solution Explorer, under your app project, right-click **References** and select **Add reference**.
2.  Expand the **Projects** tab, select **Solution**, and then select the check box for your effect project name. For this example, the name is *VideoEffectComponent*.
3.  Click **OK**.

### Add your custom effect to a camera video stream

You can set up a simple preview stream from the camera by following the steps in the article [Simple camera preview access](simple-camera-preview-access.md). Following those steps will provide you with an initialized [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/br241124) object that is used to access the camera's video stream.

To add your custom video effect to a camera stream, first create a new [**VideoEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn608055) object, passing in the namespace and class name for your effect. Next, call the **MediaCapture** object's [**AddVideoEffect**](https://msdn.microsoft.com/library/windows/apps/dn878035) method to add your effect to the specified stream. This example uses the [**MediaStreamType.VideoPreview**](https://msdn.microsoft.com/library/windows/apps/br226640) value to specify that the effect should be added to the preview stream. If your app supports video capture, you could also use **MediaStreamType.VideoRecord** to add the effect to the capture stream. **AddVideoEffect** returns an [**IMediaExtension**](https://msdn.microsoft.com/library/windows/apps/br240985) object representing your custom effect. You can use the SetProperties method to set the configuration for your effect.

After the effect has been added, [**StartPreviewAsync**](https://msdn.microsoft.com/library/windows/apps/br226613) is called to start the preview stream.

[!code-cs[AddVideoEffectAsync](./code/VideoEffect_Win10/cs/VideoEffect_Win10/MainPage.xaml.cs#SnippetAddVideoEffectAsync)]



### Add your custom effect to a clip in a MediaComposition

For general guidance for creating media compositions from video clips, see [Media compositions and editing](media-compositions-and-editing.md). The following code snippet shows the creation of a simple media composition that uses a custom video effect. A [**MediaClip**](https://msdn.microsoft.com/library/windows/apps/dn652596) object is created by calling [**CreateFromFileAsync**](https://msdn.microsoft.com/library/windows/apps/dn652607), passing in a video file that was selected by the user with a [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/br207847), and the clip is added to a new [**MediaComposition**](https://msdn.microsoft.com/library/windows/apps/dn652646). Next a new [**VideoEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn608055) object is created, passing in the namespace and class name for your effect to the constructor. Finally, the effect definition is added to the [**VideoEffectDefinitions**](https://msdn.microsoft.com/library/windows/apps/dn652643) collection of the **MediaClip** object.


[!code-cs[AddEffectToComposition](./code/VideoEffect_Win10/cs/VideoEffect_Win10/MainPage.xaml.cs#SnippetAddEffectToComposition)]


## Related topics
* [Simple camera preview access](simple-camera-preview-access.md)
* [Media compositions and editing](media-compositions-and-editing.md)
* [Win2D documentation](http://go.microsoft.com/fwlink/p/?LinkId=519078)
* [Media playback](media-playback.md)