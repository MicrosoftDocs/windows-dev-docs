---
title: Custom video effects
description: Learn how to create a Windows Runtime component that implements the IBasicVideoEffect interface to create custom video effects for your app.
ms.topic: how-to
ms.date: 07/10/2026
author: GrantMeStrength
ms.author: jken
---

# Custom video effects

This article describes how to create a Windows Runtime component that implements the [IBasicVideoEffect](/uwp/api/Windows.Media.Effects.IBasicVideoEffect) interface to create custom effects for video streams. You can use custom effects with [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture) and [MediaComposition](/uwp/api/Windows.Media.Editing.MediaComposition).

> [!NOTE]
> The `IBasicVideoEffect` interface is a Windows Runtime API in the `Windows.Media.Effects` namespace, and the interface members are the same ones you implemented in UWP. However, WinUI 3 desktop apps don't have the **Windows Runtime Component** project template that UWP projects used. Instead, you author the effect using a C#/WinRT class library, and you must explicitly register the component for Windows Runtime activation, as described in this article.

## Add a Windows Runtime component for your video effect

WinUI 3 desktop apps use [C#/WinRT](/windows/apps/develop/platform/csharp-winrt/) to author Windows Runtime components, instead of the UWP-only **Windows Runtime Component** project template.

1. Right-click your solution in **Solution Explorer** and select **Add** > **New Project**.
2. Select the **Class Library** project template. Name the project *VideoEffectComponent*.
3. In *VideoEffectComponent.csproj*, set the target framework to match your WinUI 3 app and mark the project as a Windows Runtime component:

    ```xml
    <PropertyGroup>
        <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
        <CsWinRTComponent>true</CsWinRTComponent>
    </PropertyGroup>
    ```

4. Install the latest [Microsoft.Windows.CsWinRT](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT/) NuGet package in the *VideoEffectComponent* project.
5. Add a project reference from your main WinUI 3 app to this component project.
6. Rename the default class file to *ExampleVideoEffect.cs*.

For more information about authoring components this way, see [Walkthrough—Create a C#/WinRT component](/windows/apps/develop/platform/csharp-winrt/create-windows-runtime-component-cswinrt).

## Register the effect component for activation

`VideoEffectDefinition` activates your effect by its Windows Runtime activatable class ID (the full type name you pass to `typeof(...).FullName`). Unless you register that class ID, activation fails at run time with a "class not registered" exception, even though the code compiles. How you register the class depends on whether your app is packaged.

### Packaged apps

Add an `<Extensions>` entry to `Package.appxmanifest` that declares the effect as an in-process activatable class hosted by `WinRT.Host.dll`, which is the hosting assembly that C#/WinRT adds to your build output:

```xml
<Extensions>
    <Extension Category="windows.activatableClass.inProcessServer">
        <InProcessServer>
            <Path>WinRT.Host.dll</Path>
            <ActivatableClass
                ActivatableClassId="VideoEffectComponent.ExampleVideoEffect"
                ThreadingModel="both" />
        </InProcessServer>
    </Extension>
</Extensions>
```

> [!NOTE]
> `ActivatableClassId` must exactly match the namespace-qualified class name you pass to `VideoEffectDefinition`.

### Unpackaged apps

Unpackaged apps don't have a `Package.appxmanifest`, so you register the activatable class in an application manifest file instead. Add a new text file named *YourApp.exe.manifest* to your app project, set its **Content** property to **True** so it's copied to the output directory, and add the same class registration in this format:

```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
    <assemblyIdentity version="1.0.0.0" name="YourApp"/>
    <file name="WinRT.Host.dll">
        <activatableClass
            name="VideoEffectComponent.ExampleVideoEffect"
            threadingModel="both"
            xmlns="urn:schemas-microsoft-com:winrt.v1" />
    </file>
</assembly>
```

For more information about hosting and registering C#/WinRT components, see [Managed component hosting](https://github.com/microsoft/CsWinRT/blob/master/docs/hosting.md) on the C#/WinRT GitHub repo.

## Implement the IBasicVideoEffect interface using software processing

Your video effect must implement all methods and properties of the [IBasicVideoEffect](/uwp/api/Windows.Media.Effects.IBasicVideoEffect) interface. This section shows a software-processing implementation.

### Class definition and namespaces

```csharp
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Effects;
using Windows.Media.MediaProperties;

namespace VideoEffectComponent
{
    public sealed class ExampleVideoEffect : IBasicVideoEffect
    {
        private VideoEncodingProperties _encodingProperties;
        private IPropertySet _configuration;
        private double _fadeValue = 0.5;

        // The following members implement the IBasicVideoEffect and
        // IMediaExtension interfaces. Each member is explained in its own
        // section later in this article.
        public void SetEncodingProperties(
            VideoEncodingProperties encodingProperties,
            Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice device)
        {
            _encodingProperties = encodingProperties;
        }

        public void SetProperties(IPropertySet configuration)
        {
            _configuration = configuration;

            if (configuration != null &&
                configuration.TryGetValue("FadeValue", out object value))
            {
                _fadeValue = (double)value;
            }
        }

        public void ProcessFrame(ProcessVideoFrameContext context)
        {
            // See ProcessFrame method — software processing later in
            // this article for the full implementation.
        }

        public void DiscardQueuedFrames()
        {
            // Reset any cached frame data
        }

        public void Close(MediaEffectClosedReason reason)
        {
            // Clean up resources
        }

        public bool IsReadOnly => false;

        public bool TimeIndependent => true;

        public IReadOnlyList<VideoEncodingProperties> SupportedEncodingProperties
        {
            get
            {
                var properties = new List<VideoEncodingProperties>();
                properties.Add(new VideoEncodingProperties
                {
                    Subtype = "ARGB32"
                });
                return properties;
            }
        }

        public MediaMemoryTypes SupportedMemoryTypes => MediaMemoryTypes.Cpu;
    }
}
```

> [!NOTE]
> The `ExampleVideoEffect` class must be declared inside the `VideoEffectComponent` namespace shown here, because the `typeof(VideoEffectComponent.ExampleVideoEffect).FullName` calls later in this article, and the `ActivatableClassId` value in the manifest registration, depend on this exact namespace-qualified name. The sections below walk through each interface member in detail; the `ProcessFrame` method shown here is a placeholder that's replaced with the full pixel-processing implementation in [ProcessFrame method — software processing](#processframe-method--software-processing).

### Close method

The system calls [Close](/uwp/api/windows.media.effects.ibasicvideoeffect.close) when the effect shuts down. Use this method to dispose of any resources you created.

```csharp
public void Close(MediaEffectClosedReason reason)
{
    // Clean up resources
}
```

### DiscardQueuedFrames method

The system calls [DiscardQueuedFrames](/uwp/api/windows.media.effects.ibasicvideoeffect.discardqueuedframes) when your effect should reset. Use this to dispose of previously cached frames.

```csharp
public void DiscardQueuedFrames()
{
    // Reset any cached frame data
}
```

### IsReadOnly property

The [IsReadOnly](/uwp/api/windows.media.effects.ibasicvideoeffect.isreadonly) property tells the system whether your effect writes to the output. If your effect only analyzes frames, set this to `true` so the system copies frames from input to output.

```csharp
public bool IsReadOnly
{
    get => false;
}
```

> [!TIP]
> When `IsReadOnly` is `true`, the system copies the input frame to the output frame before [ProcessFrame](/uwp/api/windows.media.effects.ibasicvideoeffect.processframe) is called. You can still write to the output frames in `ProcessFrame`.

### SetEncodingProperties method

The system calls [SetEncodingProperties](/uwp/api/windows.media.effects.ibasicvideoeffect.setencodingproperties) to tell your effect the encoding properties for the video stream. This method also provides a reference to the Direct3D device for hardware rendering.

```csharp
private Windows.Media.MediaProperties.VideoEncodingProperties _encodingProperties;

public void SetEncodingProperties(
    VideoEncodingProperties encodingProperties,
    Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice device)
{
    _encodingProperties = encodingProperties;
}
```

### SupportedEncodingProperties property

The system checks [SupportedEncodingProperties](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedencodingproperties) to determine which encoding properties your effect supports.

```csharp
public IReadOnlyList<VideoEncodingProperties> SupportedEncodingProperties
{
    get
    {
        var properties = new List<VideoEncodingProperties>();
        properties.Add(new VideoEncodingProperties
        {
            Subtype = "ARGB32"
        });
        return properties;
    }
}
```

> [!NOTE]
> If you return an empty list of `VideoEncodingProperties` objects, the system defaults to ARGB32 encoding.

### SupportedMemoryTypes property

The [SupportedMemoryTypes](/uwp/api/windows.media.effects.ibasicvideoeffect.supportedmemorytypes) property determines whether your effect accesses video frames in software memory or GPU memory.

```csharp
public MediaMemoryTypes SupportedMemoryTypes
{
    get => MediaMemoryTypes.Cpu;
}
```

If you return `MediaMemoryTypes.Cpu`, the system passes frames as [SoftwareBitmap](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects. If you return `MediaMemoryTypes.Gpu`, the system passes frames as [IDirect3DSurface](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) objects.

### TimeIndependent property

Set [TimeIndependent](/uwp/api/windows.media.effects.ibasicvideoeffect.timeindependent) to `true` if your effect does not require uniform timing. This allows the system to optimize performance.

```csharp
public bool TimeIndependent
{
    get => true;
}
```

### SetProperties method

The [SetProperties](/uwp/api/windows.media.imediaextension.setproperties) method allows the calling app to pass configuration parameters to your effect.

```csharp
private double _fadeValue = 0.5;
private Windows.Foundation.Collections.IPropertySet _configuration;

public void SetProperties(IPropertySet configuration)
{
    _configuration = configuration;

    if (configuration != null &&
        configuration.TryGetValue("FadeValue", out object value))
    {
        _fadeValue = (double)value;
    }
}
```

### ProcessFrame method — software processing

The [ProcessFrame](/uwp/api/windows.media.effects.ibasicvideoeffect.processframe) method is where your effect modifies the image data. This method is called once per frame and receives a [ProcessVideoFrameContext](/uwp/api/Windows.Media.Effects.ProcessVideoFrameContext) object with input and output [VideoFrame](/uwp/api/Windows.Media.VideoFrame) objects.

To access the raw pixel data of a `SoftwareBitmap`, use COM interop. Add the following interface definition in your effect namespace:

```csharp
[ComImport]
[System.Runtime.InteropServices.Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
unsafe interface IMemoryBufferByteAccess
{
    void GetBuffer(out byte* buffer, out uint capacity);
}
```

> [!NOTE]
> This technique accesses a native, unmanaged image buffer. You must configure your project to allow unsafe code. In the project properties, select the **Build** tab and enable **Allow unsafe code**.

The following example dims each pixel in the frame by the configured fade value:

```csharp
public unsafe void ProcessFrame(ProcessVideoFrameContext context)
{
    using (BitmapBuffer inputBuffer = context.InputFrame
        .SoftwareBitmap.LockBuffer(BitmapBufferAccessMode.Read))
    using (BitmapBuffer outputBuffer = context.OutputFrame
        .SoftwareBitmap.LockBuffer(BitmapBufferAccessMode.Write))
    {
        using (var inputRef = inputBuffer.CreateReference())
        using (var outputRef = outputBuffer.CreateReference())
        {
            byte* inputBytes;
            uint inputCapacity;
            ((IMemoryBufferByteAccess)inputRef)
                .GetBuffer(out inputBytes, out inputCapacity);

            byte* outputBytes;
            uint outputCapacity;
            ((IMemoryBufferByteAccess)outputRef)
                .GetBuffer(out outputBytes, out outputCapacity);

            var inputPlane =
                inputBuffer.GetPlaneDescription(0);

            for (int i = 0;
                 i < inputPlane.Height;
                 i++)
            {
                for (int j = 0;
                     j < inputPlane.Width;
                     j++)
                {
                    int offset = inputPlane.StartIndex
                        + inputPlane.Stride * i
                        + 4 * j;

                    // Apply fade to B, G, R channels
                    // (skip alpha at offset+3)
                    outputBytes[offset + 0] = (byte)(
                        inputBytes[offset + 0] * _fadeValue);
                    outputBytes[offset + 1] = (byte)(
                        inputBytes[offset + 1] * _fadeValue);
                    outputBytes[offset + 2] = (byte)(
                        inputBytes[offset + 2] * _fadeValue);
                    outputBytes[offset + 3] =
                        inputBytes[offset + 3]; // alpha
                }
            }
        }
    }
}
```

## Hardware processing with Win2D

For GPU-based processing, use [Win2D](https://microsoft.github.io/Win2D/html/Introduction.htm) instead of software bitmap manipulation. When using hardware processing:

1. Add the **Microsoft.Graphics.Win2D** NuGet package to your effect project.
2. Return `MediaMemoryTypes.Gpu` from `SupportedMemoryTypes`.
3. Save the Direct3D device reference from `SetEncodingProperties`.
4. In `ProcessFrame`, create a `CanvasDevice` from the Direct3D device and use Win2D drawing operations on the output frame's `Direct3DSurface`.

> [!NOTE]
> For WinUI 3 projects, use the **Microsoft.Graphics.Win2D** package rather than the older **Win2D.uwp** package.

## Add the effect to a video stream

Add the video effect to a [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture) video stream:

```csharp
var effectDefinition = new VideoEffectDefinition(
    typeof(VideoEffectComponent.ExampleVideoEffect).FullName);

await _mediaCapture.AddVideoEffectAsync(
    effectDefinition,
    MediaStreamType.VideoPreview);
```

To pass configuration properties:

```csharp
var properties = new PropertySet();
properties["FadeValue"] = 0.7;

var effectDefinition = new VideoEffectDefinition(
    typeof(VideoEffectComponent.ExampleVideoEffect).FullName,
    properties);
```

## Add the effect to a media composition

Add the video effect to a clip in a [MediaComposition](/uwp/api/Windows.Media.Editing.MediaComposition):

```csharp
var effectDefinition = new VideoEffectDefinition(
    typeof(VideoEffectComponent.ExampleVideoEffect).FullName);

mediaClip.VideoEffectDefinitions.Add(effectDefinition);
```

## Related content

- [Custom audio effects](custom-audio-effects.md)
- [Camera](/windows/apps/develop/camera/camera)
- [Effects for video capture](/windows/apps/develop/camera/effects-for-video-capture)
