---
description: This article explains how to load and save image files using BitmapDecoder and BitmapEncoder and how to use the SoftwareBitmap object to represent bitmap images.
title: Create, edit, and save bitmap images
ms.date: 08/23/2026
ms.topic: article
keywords: windows, winui, bitmap, imaging, softwarebitmap
ms.localizationpriority: medium
---

# Create, edit, and save bitmap images

This article explains how to load and save image files using [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) and [**BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder) and how to use the [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) object to represent bitmap images.

The **SoftwareBitmap** class is a versatile API that can be created from multiple sources including image files, [**WriteableBitmap**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.writeablebitmap) objects, Direct3D surfaces, and code. **SoftwareBitmap** allows you to easily convert between different pixel formats and alpha modes, and allows low-level access to pixel data. Also, **SoftwareBitmap** is a common interface used by multiple features of Windows, including:

-   [**CapturedFrame**](/uwp/api/Windows.Media.Capture.CapturedFrame) allows you to get frames captured by the camera as a **SoftwareBitmap**.

-   [**VideoFrame**](/uwp/api/Windows.Media.VideoFrame) allows you to get a **SoftwareBitmap** representation of a **VideoFrame**.

-   [**FaceDetector**](/uwp/api/Windows.Media.FaceAnalysis.FaceDetector) allows you to detect faces in a **SoftwareBitmap**.

The sample code in this article uses APIs from the following namespaces.

```csharp
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
```

## Create a SoftwareBitmap from an image file with BitmapDecoder

To create a [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) from a file, get an instance of [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) containing the image data. This example uses a [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) to allow the user to select an image file.

```csharp
private async Task<StorageFile?> PickInputFileAsync()
{
    var picker = new FileOpenPicker();

    // Initialize the picker with the window handle (required for WinUI 3).
    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
    WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

    picker.ViewMode = PickerViewMode.Thumbnail;
    picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
    picker.FileTypeFilter.Add(".jpg");
    picker.FileTypeFilter.Add(".jpeg");
    picker.FileTypeFilter.Add(".png");
    picker.FileTypeFilter.Add(".bmp");

    return await picker.PickSingleFileAsync();
}
```

Call the [**OpenAsync**](/uwp/api/windows.storage.istoragefile.openasync) method of the **StorageFile** object to get a random access stream containing the image data. Call the static method [**BitmapDecoder.CreateAsync**](/uwp/api/windows.graphics.imaging.bitmapdecoder.createasync) to get an instance of the [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) class for the specified stream. Call [**GetSoftwareBitmapAsync**](/uwp/api/windows.graphics.imaging.bitmapdecoder.getsoftwarebitmapasync) to get a [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) object containing the image.

```csharp
private async Task<SoftwareBitmap> CreateSoftwareBitmapFromFileAsync(StorageFile file)
{
    using IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);

    // Create a decoder from the image file.
    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

    // Get the SoftwareBitmap representation of the file.
    SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

    return softwareBitmap;
}
```

## Save a SoftwareBitmap to a file with BitmapEncoder

To save a **SoftwareBitmap** to a file, get an instance of **StorageFile** to which the image will be saved. This example uses a [**FileSavePicker**](/uwp/api/Windows.Storage.Pickers.FileSavePicker) to allow the user to select an output file.

```csharp
private async Task<StorageFile?> PickOutputFileAsync()
{
    var picker = new FileSavePicker();

    // Initialize the picker with the window handle (required for WinUI 3).
    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
    WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

    picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
    picker.SuggestedFileName = "output";
    picker.FileTypeChoices.Add("JPEG Image", new List<string> { ".jpg" });
    picker.FileTypeChoices.Add("PNG Image", new List<string> { ".png" });

    return await picker.PickSaveFileAsync();
}
```

Call the [**OpenAsync**](/uwp/api/windows.storage.istoragefile.openasync) method of the **StorageFile** object to get a random access stream to which the image will be written. Call the static method [**BitmapEncoder.CreateAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.createasync) to get an instance of the [**BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder) class for the specified stream. The first parameter to **CreateAsync** is a GUID representing the codec that should be used to encode the image. **BitmapEncoder** class exposes a property containing the ID for each codec supported by the encoder, such as [**JpegEncoderId**](/uwp/api/windows.graphics.imaging.bitmapencoder.jpegencoderid).

Use the [**SetSoftwareBitmap**](/uwp/api/windows.graphics.imaging.bitmapencoder.setsoftwarebitmap) method to set the image that will be encoded. You can set values of the [**BitmapTransform**](/uwp/api/Windows.Graphics.Imaging.BitmapTransform) property to apply basic transforms to the image while it is being encoded. The [**IsThumbnailGenerated**](/uwp/api/windows.graphics.imaging.bitmapencoder.isthumbnailgenerated) property determines whether a thumbnail is generated by the encoder. Note that not all file formats support thumbnails, so if you use this feature, you should catch the unsupported operation error that will be thrown if thumbnails are not supported.

Call [**FlushAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.flushasync) to cause the encoder to write the image data to the specified file.

```csharp
private async Task SaveSoftwareBitmapToFileAsync(SoftwareBitmap softwareBitmap, StorageFile outputFile)
{
    using IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

    // Create an encoder with the desired format.
    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

    // Set the software bitmap.
    encoder.SetSoftwareBitmap(softwareBitmap);

    // Set additional encoding parameters (optional).
    encoder.BitmapTransform.ScaledWidth = (uint)softwareBitmap.PixelWidth;
    encoder.BitmapTransform.ScaledHeight = (uint)softwareBitmap.PixelHeight;
    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant;
    encoder.IsThumbnailGenerated = true;

    try
    {
        await encoder.FlushAsync();
    }
    catch (Exception ex)
    {
        const int WINCODEC_ERR_UNSUPPORTEDOPERATION = unchecked((int)0x88982F81);
        switch (ex.HResult)
        {
            case WINCODEC_ERR_UNSUPPORTEDOPERATION:
                // If the encoder does not support thumbnail generation,
                // disable it and try again.
                encoder.IsThumbnailGenerated = false;
                break;

            default:
                throw;
        }
    }

    if (!encoder.IsThumbnailGenerated)
    {
        await encoder.FlushAsync();
    }
}
```

You can specify additional encoding options when you create the **BitmapEncoder** by creating a new [**BitmapPropertySet**](/uwp/api/Windows.Graphics.Imaging.BitmapPropertySet) object and populating it with one or more [**BitmapTypedValue**](/uwp/api/Windows.Graphics.Imaging.BitmapTypedValue) objects representing the encoder settings. For a list of supported encoder options, see [BitmapEncoder options reference](/windows/uwp/audio-video-camera/bitmapencoder-options-reference).

```csharp
private async Task SaveWithEncodingOptionsAsync(SoftwareBitmap softwareBitmap, StorageFile outputFile)
{
    using IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

    // Create encoding options with a specific image quality.
    var propertySet = new BitmapPropertySet();
    var qualityValue = new BitmapTypedValue(0.9, Windows.Foundation.PropertyType.Single);
    propertySet.Add("ImageQuality", qualityValue);

    // Create the encoder with the encoding options.
    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(
        BitmapEncoder.JpegEncoderId, stream, propertySet);

    encoder.SetSoftwareBitmap(softwareBitmap);
    await encoder.FlushAsync();
}
```

## Use SoftwareBitmap with a XAML Image control

To display an image within a XAML page using the [**Image**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) control, first define an **Image** control in your XAML page.

```xml
<Image x:Name="imageControl"
       Grid.Row="2"
       Stretch="Uniform"/>
```

Currently, the **Image** control only supports images that use BGRA8 encoding and pre-multiplied or no alpha channel. Before attempting to display an image, test to make sure it has the correct format, and if not, use the **SoftwareBitmap** static [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) method to convert the image to the supported format.

Create a new [**SoftwareBitmapSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource) object. Set the contents of the source object by calling [**SetBitmapAsync**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource.setbitmapasync), passing in a **SoftwareBitmap**. Then you can set the [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image.source) property of the **Image** control to the newly created **SoftwareBitmapSource**.

```csharp
private async Task DisplaySoftwareBitmapAsync(SoftwareBitmap softwareBitmap)
{
    // SoftwareBitmap must be Bgra8 with premultiplied or no alpha
    // to display in a XAML Image control.
    if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
        softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
    {
        softwareBitmap = SoftwareBitmap.Convert(
            softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
    }

    // In WinUI 3, SoftwareBitmapSource is in Microsoft.UI.Xaml.Media.Imaging.
    var source = new SoftwareBitmapSource();
    await source.SetBitmapAsync(softwareBitmap);

    // Set the source of the Image control.
    imageControl.Source = source;
}
```

You can also use **SoftwareBitmapSource** to set a **SoftwareBitmap** as the [**ImageSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imagebrush.imagesource) for an [**ImageBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imagebrush).

## Create a SoftwareBitmap from a WriteableBitmap

You can create a **SoftwareBitmap** from an existing **WriteableBitmap** by calling [**SoftwareBitmap.CreateCopyFromBuffer**](/uwp/api/windows.graphics.imaging.softwarebitmap.createcopyfrombuffer) and supplying the **PixelBuffer** property of the **WriteableBitmap** to set the pixel data. The second argument lets you specify the pixel format for the newly created **SoftwareBitmap**. You can use the [**PixelWidth**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapsource.pixelwidth) and [**PixelHeight**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapsource.pixelheight) properties of the **WriteableBitmap** to specify the dimensions of the new image.

```csharp
private SoftwareBitmap ConvertWriteableBitmapToSoftwareBitmap(WriteableBitmap writeableBitmap)
{
    // Create a SoftwareBitmap from the WriteableBitmap's pixel buffer.
    SoftwareBitmap softwareBitmap = SoftwareBitmap.CreateCopyFromBuffer(
        writeableBitmap.PixelBuffer,
        BitmapPixelFormat.Bgra8,
        writeableBitmap.PixelWidth,
        writeableBitmap.PixelHeight);

    return softwareBitmap;
}
```

## Create or edit a SoftwareBitmap programmatically

So far this topic has addressed working with image files. You can also create a new **SoftwareBitmap** programmatically in code and use the same technique to access and modify the **SoftwareBitmap**'s pixel data.

Use the [**CopyFromBuffer**](/uwp/api/windows.graphics.imaging.softwarebitmap.copyfrombuffer) method to populate a **SoftwareBitmap** from a byte array, and [**CopyToBuffer**](/uwp/api/windows.graphics.imaging.softwarebitmap.copytobuffer) to copy pixel data out to a byte array for reading or modification. To use the **AsBuffer** extension method to wrap a byte array as an [**IBuffer**](/uwp/api/Windows.Storage.Streams.IBuffer), include the **System.Runtime.InteropServices.WindowsRuntime** namespace (this is included in the `SnippetNamespaces` using statements at the top of this article).

Create a new **SoftwareBitmap** with the pixel format and size you want. Allocate a byte array large enough to hold the pixel data, fill it with the desired values, and then call **CopyFromBuffer** to write the data into the bitmap.

```csharp
private SoftwareBitmap CreateGradientBitmap(int width, int height)
{
    // Create a new SoftwareBitmap programmatically.
    var softwareBitmap = new SoftwareBitmap(
        BitmapPixelFormat.Bgra8, width, height, BitmapAlphaMode.Premultiplied);

    // Allocate a byte array for the pixel data.
    int bytesPerPixel = 4; // BGRA8
    byte[] pixelData = new byte[width * height * bytesPerPixel];

    // Fill the bitmap with a gradient pattern.
    for (int row = 0; row < height; row++)
    {
        for (int col = 0; col < width; col++)
        {
            int pixelIndex = (row * width + col) * bytesPerPixel;

            // Blue channel: gradient left to right.
            pixelData[pixelIndex + 0] = (byte)((double)col / width * 255);
            // Green channel: gradient top to bottom.
            pixelData[pixelIndex + 1] = (byte)((double)row / height * 255);
            // Red channel: inverse diagonal gradient.
            pixelData[pixelIndex + 2] = (byte)(255 - (((double)(col + row)
                / (width + height)) * 255));
            // Alpha channel: fully opaque.
            pixelData[pixelIndex + 3] = 255;
        }
    }

    // Copy the pixel data into the SoftwareBitmap.
    softwareBitmap.CopyFromBuffer(pixelData.AsBuffer());

    return softwareBitmap;
}
```

## Create a SoftwareBitmap from a Direct3D surface

To create a **SoftwareBitmap** object from a Direct3D surface, you must include the [**Windows.Graphics.DirectX.Direct3D11**](/uwp/api/Windows.Graphics.DirectX.Direct3D11) namespace in your project.

```csharp
using Windows.Graphics.DirectX.Direct3D11;
```

Call [**CreateCopyFromSurfaceAsync**](/uwp/api/windows.graphics.imaging.softwarebitmap.createcopyfromsurfaceasync) to create a new **SoftwareBitmap** from the surface. As the name indicates, the new **SoftwareBitmap** has a separate copy of the image data. Modifications to the **SoftwareBitmap** will not have any effect on the Direct3D surface.

```csharp
private async Task<SoftwareBitmap> CreateBitmapFromSurfaceAsync(IDirect3DSurface surface)
{
    // Create a SoftwareBitmap from a Direct3D surface.
    SoftwareBitmap softwareBitmap =
        await SoftwareBitmap.CreateCopyFromSurfaceAsync(surface);

    return softwareBitmap;
}
```

## Convert a SoftwareBitmap to a different pixel format

The **SoftwareBitmap** class provides the static method, [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert), that allows you to easily create a new **SoftwareBitmap** that uses the pixel format and alpha mode you specify from an existing **SoftwareBitmap**. Note that the newly created bitmap has a separate copy of the image data. Modifications to the new bitmap will not affect the source bitmap.

```csharp
private SoftwareBitmap ConvertBitmapPixelFormat(SoftwareBitmap softwareBitmap)
{
    // Convert the pixel format and alpha mode of the SoftwareBitmap.
    SoftwareBitmap convertedBitmap = SoftwareBitmap.Convert(
        softwareBitmap,
        BitmapPixelFormat.Bgra8,
        BitmapAlphaMode.Premultiplied);

    return convertedBitmap;
}
```

## Transcode an image file

You can transcode an image file directly from a [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) to a [**BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder). Create a [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream) from the file to be transcoded. Create a new **BitmapDecoder** from the input stream. Create a new [**InMemoryRandomAccessStream**](/uwp/api/Windows.Storage.Streams.InMemoryRandomAccessStream) for the encoder to write to and call [**BitmapEncoder.CreateForTranscodingAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.createfortranscodingasync), passing in the in-memory stream and the decoder object. Encode options are not supported when transcoding; instead you should use [**CreateAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.createasync). Any properties in the input image file that you do not specifically set on the encoder, will be written to the output file unchanged. Call [**FlushAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.flushasync) to cause the encoder to encode to the in-memory stream. Finally, seek the file stream and the in-memory stream to the beginning and call [**CopyAsync**](/uwp/api/windows.storage.streams.randomaccessstream.copyasync) to write the in-memory stream out to the file stream.

```csharp
private async Task TranscodeImageFileAsync(StorageFile inputFile, StorageFile outputFile)
{
    using IRandomAccessStream inputStream =
        await inputFile.OpenAsync(FileAccessMode.Read);
    using IRandomAccessStream outputStream =
        await outputFile.OpenAsync(FileAccessMode.ReadWrite);

    // Create a decoder for the input file.
    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(inputStream);

    // Create an encoder for transcoding to the output file.
    BitmapEncoder encoder =
        await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

    // Optionally apply transforms during transcoding.
    encoder.BitmapTransform.ScaledWidth = decoder.PixelWidth / 2;
    encoder.BitmapTransform.ScaledHeight = decoder.PixelHeight / 2;
    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant;

    await encoder.FlushAsync();
}
```

## Related topics

* [BitmapEncoder options reference](bitmapencoder-options-reference.md)
* [Image Metadata](image-metadata.md)
