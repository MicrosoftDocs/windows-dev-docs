---
ms.assetid: 
description: This article explains how to use the SoftwareBitmap class with the Open Source Computer Vision Library (OpenCV).
title: Process bitmaps with OpenCV
ms.date: 03/19/2018
ms.topic: article
keywords: windows 10, uwp, opencv, softwarebitmap
ms.localizationpriority: medium
---
# Process bitmaps with OpenCV

This article explains how to use the **[SoftwareBitmap](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap)** class, which is used by many different Windows Runtime APIs to represent images, with the Open Source Computer Vision Library (OpenCV), an open source, native code library that provides a wide variety of image processing algorithms. 

The examples in this article walk you through creating a native code Windows Runtime component that can be used from a UWP app, including apps that are created using C#. This helper component will expose a single method, **Blur**, which will use OpenCV's blur image processing function. The component implements private methods that get a pointer to the underlying image data buffer which can be used directly by the OpenCV library, making it simple to extend the helper component to implement other OpenCV processing features. 

* For an introduction to using **SoftwareBitmap**, see [Create, edit, and save bitmap images](imaging.md). 
* To learn how to use the OpenCV library, go to [https://opencv.org](https://opencv.org).
* To see how to use the OpenCV helper component shown in this article with **[MediaFrameReader](/uwp/api/windows.media.capture.frames.mediaframereader)** to implement real-time image processing of frames from a camera, see [Use OpenCV with MediaFrameReader](use-opencv-with-mediaframereader.md).
* For a complete code example that implements some different effects, see the [Camera Frames + OpenCV Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraOpenCV) in the Windows Universal Samples GitHub repo.

> [!NOTE] 
> The technique used by the OpenCVHelper component, described in detail in this article, requires that the image data to be processed resides in CPU memory, not GPU memory. So, for APIs that allow you to request the memory location of images, such as the **[MediaCapture](/uwp/api/windows.media.capture.mediacapture)** class, you should specify CPU memory.

## Create a helper Windows Runtime component for OpenCV interop

### 1. Add a new native code Windows Runtime component project to your solution

1. Add a new project to your solution in Visual Studio by right-clicking your solution in Solution Explorer and selecting **Add->New Project**. 
2. Under the **Visual C++** category, select **Windows Runtime Component (Universal Windows)**. For this example, name the project "OpenCVBridge" and click **OK**. 
3. In the **New Windows Universal Project** dialog, select the target and minimum OS version for your app and click **OK**.
4. Right-click the autogerenated file Class1.cpp in Solution Explorer and select **Remove**, when the confirmation dialog pops up, choose **Delete**. Then delete the Class1.h header file.
5. Right-click the OpenCVBridge project icon and select **Add->Class...**. In the **Add Class** dialog, input "OpenCVHelper" in the **Class Name** field and then click **OK**. Code will be added to the created class files in a later step.

### 2. Add the OpenCV NuGet packages to your component project

1. Right-click the OpenCVBridge project icon in Solution Explorer and select **Manage NuGet Packages...**
2. When the NuGet Package Manager dialog opens, select the **Browse** tab and type "OpenCV.Win" in the search box.
3. Select "OpenCV.Win.Core" and click **Install**. In the **Preview** dialog, click **OK**.
4. Use the same procedure to install the "OpenCV.Win.ImgProc" package.

>[!NOTE]
>OpenCV.Win.Core and OpenCV.Win.ImgProc are not regularly updated and do not pass the Store compliance checks, therefore these packages are intended for experimentation only.

### 3. Implement the OpenCVHelper class

Paste the following code into the OpenCVHelper.h header file. This code includes OpenCV header files for the *Core* and *ImgProc* packages we installed and declares three methods that will be shown in the following steps.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/OpenCVBridge/OpenCVHelper.h" id="SnippetOpenCVHelperHeader":::

Delete the existing contents of the OpenCVHelper.cpp file and then add the following include directives. 

:::code language="cpp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/OpenCVBridge/OpenCVHelper.cpp" id="SnippetOpenCVHelperInclude":::

After the include directives, add the following **using** directives. 

:::code language="cpp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/OpenCVBridge/OpenCVHelper.cpp" id="SnippetOpenCVHelperUsing":::

Next, add the method **GetPointerToPixelData** to OpenCVHelper.cpp. This method takes a **[SoftwareBitmap](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap)** and, through a series of conversions, gets a COM interface representation of the pixel data through which we can get a pointer to the underlying data buffer as a **char** array. 

First a **[BitmapBuffer](/uwp/api/windows.graphics.imaging.bitmapbuffer)** containing the pixel data is obtained by calling **[LockBuffer](/uwp/api/windows.graphics.imaging.softwarebitmap.lockbuffer)**, requesting a read/write buffer so that the OpenCV library can modify that pixel data.  **[CreateReference](/uwp/api/windows.graphics.imaging.bitmapbuffer.CreateReference)** is called to get an **[IMemoryBufferReference](/uwp/api/windows.foundation.imemorybufferreference)** object. Next, the **IMemoryBufferByteAccess** interface is cast as an **IInspectable**, the base interface of all Windows Runtime classes, and **[QueryInterface](/windows/desktop/api/unknwn/nf-unknwn-iunknown-queryinterface(q_))** is called to get an **[IMemoryBufferByteAccess](/previous-versions/mt297505(v=vs.85))** COM interface that will allow us to obtain the pixel data buffer as a **char** array. Finally, populate the **char** array by calling **[IMemoryBufferByteAccess::GetBuffer](/windows/desktop/WinRT/imemorybufferbyteaccess-getbuffer)**. If any of the conversion steps in this method fail, the method returns **false**, indicating that further processing can't continue.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/OpenCVBridge/OpenCVHelper.cpp" id="SnippetOpenCVHelperGetPointerToPixelData":::

Next, add the method **TryConvert** method shown below. This method takes a **SoftwareBitmap** and attempts to convert it to a **Mat** object, which is the matrix object OpenCV uses to represent image data buffers. This method calls the **GetPointerToPixelData** method defined above to get a **char** array representation of the pixel data buffer. If this succeeds, the constructor for the **Mat** class is called, passing in the pixel width and height obtained from the source **SoftwareBitmap** object. 

> [!NOTE] 
> This example specifies the CV_8UC4 constant as the pixel format for the created **Mat** object. This means that the **SoftwareBitmap** passed into this method must have a **[BitmapPixelFormat](/uwp/api/windows.graphics.imaging.softwarebitmap.BitmapPixelFormat)** property value of  **[BGRA8](/uwp/api/Windows.Graphics.Imaging.BitmapPixelFormat)** with premultiplied alpha, the equivalent of CV_8UC4, to work with this example.

A shallow copy of the created **Mat** object is returned from the method so that further processing operates on the same data pixel data buffer referenced by the **SoftwareBitmap** and not a copy of this buffer.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/OpenCVBridge/OpenCVHelper.cpp" id="SnippetOpenCVHelperTryConvert":::

Finally, this example helper class implements a single image processing method, **Blur**, which simply uses the **TryConvert** method defined above to retrieve a **Mat** object representing the source bitmap and the target bitmap for the blur operation, and then calls the **blur** method from the OpenCV ImgProc library. The other parameter to **blur** specifies the size of the blur effect in the X and Y directions.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/OpenCVBridge/OpenCVHelper.cpp" id="SnippetOpenCVHelperBlur":::


## A simple SoftwareBitmap OpenCV example using the helper component
Now that the OpenCVBridge component has been created, we can create a simple C# app that uses the OpenCV **blur** method to modify a **SoftwareBitmap**. To access the Windows Runtime component from your UWP app, you must first add a reference to the component. In Solution Explorer, right-click the **References** node under your UWP app project and select **Add reference...**. In the Reference Manager dialog, select **Projects->Solution**. Check the box next to the OpenCVBridge project and the click **OK**.

The example code below allows the user to select an image file and then uses **[BitmapDecoder](/uwp/api/windows.graphics.imaging.bitmapencoder)** to create a **SoftwareBitmap** representation of the image. For more information on working with **SoftwareBitmap**, see [Create, edit, and save bitmap images](./imaging.md).

As discussed previously in this article, the **OpenCVHelper** class requires that all provided **SoftwareBitmap** images be encoded using the BGRA8 pixel format with premultiplied alpha values, so if the image is not already in this format, the example code calls **[Convert](/uwp/api/windows.graphics.imaging.softwarebitmap.BitmapAlphaMode)** to convert the image into the expected format.

Next, a **SoftwareBitmap** is created to be used as the target of the blur operation. The input image properties are used as arguments to the constructor to create a bitmap with matching format.

A new instance of **OpenCVHelper** is created, and the **Blur** method is called, passing in the source and target bitmaps. Finally, a **SoftwareBitmapSource** is created to assign the output image to a XAML **Image** control.

This sample code uses APIs from the following namespaces, in addition to the namespaces included by the default project template.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.OpenCV.xaml.cs" id="SnippetOpenCVMainPageUsing":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.OpenCV.xaml.cs" id="SnippetOpenCVBlur":::

## Related topics

* [BitmapEncoder options reference](bitmapencoder-options-reference.md)
* [Image Metadata](image-metadata.md)
 

 
