---
title: Screen capture
description: The Windows.Graphics.Capture namespace provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences.
ms.assetid: 349C959D-9C74-44E7-B5F6-EBDB5CA87B9F
ms.date: 10/18/2023
ms.topic: article
dev_langs:
- csharp
- vb
keywords: windows 10, uwp, screen capture
ms.localizationpriority: medium
---
# Screen capture

Starting in Windows 10, version 1803, the [Windows.Graphics.Capture](/uwp/api/windows.graphics.capture) namespace provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences.

With screen capture, developers invoke secure system UI for end users to pick the display or application window to be captured, and a yellow notification border is drawn by the system around the actively captured item. In the case of multiple simultaneous capture sessions, a yellow border is drawn around each item being captured.

> [!NOTE]
> The screen capture APIs are only supported on Windows devices and Windows Mixed Reality immersive headsets.

This article describes capturing a single image of the display or application window. For information on encoding frames captured from the screen to a video file, see [Screen capture to video](screen-capture-video.md)

## Add the screen capture capability

The APIs found in the **Windows.Graphics.Capture** namespace require a general capability to be declared in your application's manifest:

1. Open **Package.appxmanifest** in the **Solution Explorer**.
2. Select the **Capabilities** tab.
3. Check **Graphics Capture**.

![Graphics Capture](images/screen-capture-1.png)

## Launch the system UI to start screen capture

Before launching the system UI, you can check to see if your application is currently able to take screen captures. There are several reasons why your application might not be able to use screen capture, including if the device does not meet hardware requirements or if the application targeted for capture blocks screen capture. Use the **IsSupported** method in the [GraphicsCaptureSession](/uwp/api/windows.graphics.capture.graphicscapturesession) class to determine if UWP screen capture is supported:

```csharp
// This runs when the application starts.
public void OnInitialization()
{
    if (!GraphicsCaptureSession.IsSupported())
    {
        // Hide the capture UI if screen capture is not supported.
        CaptureButton.Visibility = Visibility.Collapsed;
    }
}
```

```vb
Public Sub OnInitialization()
    If Not GraphicsCaptureSession.IsSupported Then
        CaptureButton.Visibility = Visibility.Collapsed
    End If
End Sub
```

Once you've verified that screen capture is supported, use the [GraphicsCapturePicker](/uwp/api/windows.graphics.capture.graphicscapturepicker) class to invoke the system picker UI. The end user uses this UI to select the display or application window of which to take screen captures. The picker will return a [GraphicsCaptureItem](/uwp/api/windows.graphics.capture.graphicscaptureitem) that will be used to create a **GraphicsCaptureSession**:

```csharp
public async Task StartCaptureAsync()
{
    // The GraphicsCapturePicker follows the same pattern the
    // file pickers do.
    var picker = new GraphicsCapturePicker();
    GraphicsCaptureItem item = await picker.PickSingleItemAsync();

    // The item may be null if the user dismissed the
    // control without making a selection or hit Cancel.
    if (item != null)
    {
        // We'll define this method later in the document.
        StartCaptureInternal(item);
    }
}
```

```vb
Public Async Function StartCaptureAsync() As Task
    ' The GraphicsCapturePicker follows the same pattern the
    ' file pickers do.
    Dim picker As New GraphicsCapturePicker
    Dim item As GraphicsCaptureItem = Await picker.PickSingleItemAsync()

    ' The item may be null if the user dismissed the
    ' control without making a selection or hit Cancel.
    If item IsNot Nothing Then
        StartCaptureInternal(item)
    End If
End Function
```

Because this is UI code, it needs to be called on the UI thread. If you're calling it from the code-behind for a page of your application (like **MainPage.xaml.cs**) this is done for you automatically, but if not, you can force it to run on the UI thread with the following code:

```csharp
CoreWindow window = CoreApplication.MainView.CoreWindow;

await window.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
{
    await StartCaptureAsync();
});
```

```vb
Dim window As CoreWindow = CoreApplication.MainView.CoreWindow
Await window.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                 Async Sub() Await StartCaptureAsync())
```

## Create a capture frame pool and capture session

Using the **GraphicsCaptureItem**, you will create a [Direct3D11CaptureFramePool](/uwp/api/windows.graphics.capture.direct3d11captureframepool) with your D3D device, supported pixel format (**DXGI\_FORMAT\_B8G8R8A8\_UNORM**), number of desired frames (which can be any integer), and frame size. The **ContentSize** property of the **GraphicsCaptureItem** class can be used as the size of your frame:

> [!NOTE]
> On systems with Windows HD color enabled, the content pixel format might not necessarily be **DXGI\_FORMAT\_B8G8R8A8\_UNORM**. To avoid pixel overclipping (i.e. the captured content looks washed out) when capturing HDR content, consider using **DXGI\_FORMAT\_R16G16B16A16\_FLOAT** for every component in the capturing pipeline, including the [Direct3D11CaptureFramePool](/uwp/api/windows.graphics.capture.direct3d11captureframepool), the target destination such as [CanvasBitmap](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm). Depends on the need, additional processing such as saving to HDR content format or HDR-to-SDR tone mapping might be required. This article will focus on SDR content capturing. For more information, please see [Using DirectX with high dynamic range Displays and Advanced Color](/windows/win32/direct3darticles/high-dynamic-range).

```csharp
private GraphicsCaptureItem _item;
private Direct3D11CaptureFramePool _framePool;
private CanvasDevice _canvasDevice;
private GraphicsCaptureSession _session;

public void StartCaptureInternal(GraphicsCaptureItem item)
{
    _item = item;

    _framePool = Direct3D11CaptureFramePool.Create(
        _canvasDevice, // D3D device
        DirectXPixelFormat.B8G8R8A8UIntNormalized, // Pixel format
        2, // Number of frames
        _item.Size); // Size of the buffers
}
```

```vb
WithEvents CaptureItem As GraphicsCaptureItem
WithEvents FramePool As Direct3D11CaptureFramePool
Private _canvasDevice As CanvasDevice
Private _session As GraphicsCaptureSession

Private Sub StartCaptureInternal(item As GraphicsCaptureItem)
    CaptureItem = item

    FramePool = Direct3D11CaptureFramePool.Create(
        _canvasDevice, ' D3D device
        DirectXPixelFormat.B8G8R8A8UIntNormalized, ' Pixel format
        2, '  Number of frames
        CaptureItem.Size) ' Size of the buffers
End Sub
```

Next, get an instance of the **GraphicsCaptureSession** class for your **Direct3D11CaptureFramePool** by passing the **GraphicsCaptureItem** to the **CreateCaptureSession** method:

```csharp
_session = _framePool.CreateCaptureSession(_item);
```

```vb
_session = FramePool.CreateCaptureSession(CaptureItem)
```

Once the user has explicitly given consent to capturing an application window or display in the system UI, the **GraphicsCaptureItem** can be associated to multiple **CaptureSession** objects. This way your application can choose to capture the same item for various experiences.

To capture multiple items at the same time, your application must create a capture session for each item to be captured, which requires invoking the picker UI for each item that is to be captured.

## Acquire capture frames

With your frame pool and capture session created, call the **StartCapture** method on your **GraphicsCaptureSession** instance to notify the system to start sending capture frames to your app:

```csharp
_session.StartCapture();
```

```vb
_session.StartCapture()
```

To acquire these capture frames, which are [Direct3D11CaptureFrame](/uwp/api/windows.graphics.capture.direct3d11captureframe) objects, you can use the **Direct3D11CaptureFramePool.FrameArrived** event:

```csharp
_framePool.FrameArrived += (s, a) =>
{
    // The FrameArrived event fires for every frame on the thread that
    // created the Direct3D11CaptureFramePool. This means we don't have to
    // do a null-check here, as we know we're the only one  
    // dequeueing frames in our application.  

    // NOTE: Disposing the frame retires it and returns  
    // the buffer to the pool.
    using (var frame = _framePool.TryGetNextFrame())
    {
        // We'll define this method later in the document.
        ProcessFrame(frame);
    }  
};
```

```vb
Private Sub FramePool_FrameArrived(sender As Direct3D11CaptureFramePool, args As Object) Handles FramePool.FrameArrived
    ' The FrameArrived event is raised for every frame on the thread
    ' that created the Direct3D11CaptureFramePool. This means we
    ' don't have to do a null-check here, as we know we're the only
    ' one dequeueing frames in our application.  

    ' NOTE Disposing the frame retires it And returns  
    ' the buffer to the pool.

    Using frame = FramePool.TryGetNextFrame()
        ProcessFrame(frame)
    End Using
End Sub
```

It is recommended to avoid using the UI thread if possible for **FrameArrived**, as this event will be raised every time a new frame is available, which will be frequent. If you do choose to listen to **FrameArrived** on the UI thread, be mindful of how much work you're doing every time the event fires.

Alternatively, you can manually pull frames with the **Direct3D11CaptureFramePool.TryGetNextFrame** method until you get all of the frames that you need.

The **Direct3D11CaptureFrame** object contains the properties **ContentSize**, **Surface**, and **SystemRelativeTime**. The **SystemRelativeTime** is QPC ([QueryPerformanceCounter](/windows/desktop/api/profileapi/nf-profileapi-queryperformancecounter)) time that can be used to synchronize other media elements.

## Process capture frames

Each frame from the **Direct3D11CaptureFramePool** is checked out when calling **TryGetNextFrame**, and checked back in according to the lifetime of the **Direct3D11CaptureFrame** object. For native applications, releasing the **Direct3D11CaptureFrame** object is enough to check the frame back in to the frame pool. For managed applications, it is recommended to use the **Direct3D11CaptureFrame.Dispose** (**Close** in C++) method. **Direct3D11CaptureFrame** implements the [IClosable](/uwp/api/Windows.Foundation.IClosable) interface, which is projected as [IDisposable](/dotnet/api/system.idisposable) for C# callers.

Applications should not save references to **Direct3D11CaptureFrame** objects, nor should they save references to the underlying Direct3D surface after the frame has been checked back in.

While processing a frame, it is recommended that applications take the [ID3D11Multithread](/windows/desktop/api/d3d11_4/nn-d3d11_4-id3d11multithread) lock on the same device that is associated with the **Direct3D11CaptureFramePool** object.

The underlying Direct3D surface will always be the size specified when creating (or recreating) the **Direct3D11CaptureFramePool**. If content is larger than the frame, the contents are clipped to the size of the frame. If the content is smaller than the frame, then the rest of the frame contains undefined data. It is recommended that applications copy out a sub-rect using the **ContentSize** property for that **Direct3D11CaptureFrame** to avoid showing undefined content.

## Take a screenshot

In our example, we convert each **Direct3D11CaptureFrame** into a [CanvasBitmap](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm), which is part of the [Win2D APIs](https://microsoft.github.io/Win2D/html/Introduction.htm).

```csharp
// Convert our D3D11 surface into a Win2D object.
CanvasBitmap canvasBitmap = CanvasBitmap.CreateFromDirect3D11Surface(
    _canvasDevice,
    frame.Surface);
```

Once we have the **CanvasBitmap**, we can save it as an image file. In the following example, we save it as a PNG file in the user's **Saved Pictures** folder.

```csharp
StorageFolder pictureFolder = KnownFolders.SavedPictures;
StorageFile file = await pictureFolder.CreateFileAsync("test.png", CreationCollisionOption.ReplaceExisting);

using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
{
    await canvasBitmap.SaveAsync(fileStream, CanvasBitmapFileFormat.Png, 1f);
}
```

## React to capture item resizing or device lost

During the capture process, applications may wish to change aspects of their **Direct3D11CaptureFramePool**. This includes providing a new Direct3D device, changing the size of the frame buffers, or even changing the number of buffers within the pool. In each of these scenarios, the **Recreate** method on the **Direct3D11CaptureFramePool** object is the recommended tool.

When **Recreate** is called, all existing frames are discarded. This is to prevent handing out frames whose underlying Direct3D surfaces belong to a device that the application may no longer have access to. For this reason, it may be wise to process all pending frames before calling **Recreate**.

## Putting it all together

The following code snippet is an end-to-end example of how to implement screen capture in a UWP application. In this sample, we have two buttons in the front-end: one calls **Button_ClickAsync**, and the other calls **ScreenshotButton_ClickAsync**.

> [!NOTE]
> This snippet uses [Win2D](https://microsoft.github.io/Win2D/html/Introduction.htm), a library for 2D graphics rendering. See their documentation for information about how to set it up for your project.

```csharp
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace ScreenCaptureTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Capture API objects.
        private SizeInt32 _lastSize;
        private GraphicsCaptureItem _item;
        private Direct3D11CaptureFramePool _framePool;
        private GraphicsCaptureSession _session;

        // Non-API related members.
        private CanvasDevice _canvasDevice;
        private CompositionGraphicsDevice _compositionGraphicsDevice;
        private Compositor _compositor;
        private CompositionDrawingSurface _surface;
        private CanvasBitmap _currentFrame;
        private string _screenshotFilename = "test.png";

        public MainPage()
        {
            this.InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            _canvasDevice = new CanvasDevice();

            _compositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(
                Window.Current.Compositor,
                _canvasDevice);

            _compositor = Window.Current.Compositor;

            _surface = _compositionGraphicsDevice.CreateDrawingSurface(
                new Size(400, 400),
                DirectXPixelFormat.B8G8R8A8UIntNormalized,
                DirectXAlphaMode.Premultiplied);    // This is the only value that currently works with
                                                    // the composition APIs.

            var visual = _compositor.CreateSpriteVisual();
            visual.RelativeSizeAdjustment = Vector2.One;
            var brush = _compositor.CreateSurfaceBrush(_surface);
            brush.HorizontalAlignmentRatio = 0.5f;
            brush.VerticalAlignmentRatio = 0.5f;
            brush.Stretch = CompositionStretch.Uniform;
            visual.Brush = brush;
            ElementCompositionPreview.SetElementChildVisual(this, visual);
        }

        public async Task StartCaptureAsync()
        {
            // The GraphicsCapturePicker follows the same pattern the
            // file pickers do.
            var picker = new GraphicsCapturePicker();
            GraphicsCaptureItem item = await picker.PickSingleItemAsync();

            // The item may be null if the user dismissed the
            // control without making a selection or hit Cancel.
            if (item != null)
            {
                StartCaptureInternal(item);
            }
        }

        private void StartCaptureInternal(GraphicsCaptureItem item)
        {
            // Stop the previous capture if we had one.
            StopCapture();

            _item = item;
            _lastSize = _item.Size;

            _framePool = Direct3D11CaptureFramePool.Create(
               _canvasDevice, // D3D device
               DirectXPixelFormat.B8G8R8A8UIntNormalized, // Pixel format
               2, // Number of frames
               _item.Size); // Size of the buffers

            _framePool.FrameArrived += (s, a) =>
            {
                // The FrameArrived event is raised for every frame on the thread
                // that created the Direct3D11CaptureFramePool. This means we
                // don't have to do a null-check here, as we know we're the only
                // one dequeueing frames in our application.  

                // NOTE: Disposing the frame retires it and returns  
                // the buffer to the pool.

                using (var frame = _framePool.TryGetNextFrame())
                {
                    ProcessFrame(frame);
                }
            };

            _item.Closed += (s, a) =>
            {
                StopCapture();
            };

            _session = _framePool.CreateCaptureSession(_item);
            _session.StartCapture();
        }

        public void StopCapture()
        {
            _session?.Dispose();
            _framePool?.Dispose();
            _item = null;
            _session = null;
            _framePool = null;
        }

        private void ProcessFrame(Direct3D11CaptureFrame frame)
        {
            // Resize and device-lost leverage the same function on the
            // Direct3D11CaptureFramePool. Refactoring it this way avoids
            // throwing in the catch block below (device creation could always
            // fail) along with ensuring that resize completes successfully and
            // isn’t vulnerable to device-lost.
            bool needsReset = false;
            bool recreateDevice = false;

            if ((frame.ContentSize.Width != _lastSize.Width) ||
                (frame.ContentSize.Height != _lastSize.Height))
            {
                needsReset = true;
                _lastSize = frame.ContentSize;
            }

            try
            {
                // Take the D3D11 surface and draw it into a  
                // Composition surface.

                // Convert our D3D11 surface into a Win2D object.
                CanvasBitmap canvasBitmap = CanvasBitmap.CreateFromDirect3D11Surface(
                    _canvasDevice,
                    frame.Surface);

                _currentFrame = canvasBitmap;

                // Helper that handles the drawing for us.
                FillSurfaceWithBitmap(canvasBitmap);
            }

            // This is the device-lost convention for Win2D.
            catch (Exception e) when (_canvasDevice.IsDeviceLost(e.HResult))
            {
                // We lost our graphics device. Recreate it and reset
                // our Direct3D11CaptureFramePool.  
                needsReset = true;
                recreateDevice = true;
            }

            if (needsReset)
            {
                ResetFramePool(frame.ContentSize, recreateDevice);
            }
        }

        private void FillSurfaceWithBitmap(CanvasBitmap canvasBitmap)
        {
            CanvasComposition.Resize(_surface, canvasBitmap.Size);

            using (var session = CanvasComposition.CreateDrawingSession(_surface))
            {
                session.Clear(Colors.Transparent);
                session.DrawImage(canvasBitmap);
            }
        }

        private void ResetFramePool(SizeInt32 size, bool recreateDevice)
        {
            do
            {
                try
                {
                    if (recreateDevice)
                    {
                        _canvasDevice = new CanvasDevice();
                    }

                    _framePool.Recreate(
                        _canvasDevice,
                        DirectXPixelFormat.B8G8R8A8UIntNormalized,
                        2,
                        size);
                }
                // This is the device-lost convention for Win2D.
                catch (Exception e) when (_canvasDevice.IsDeviceLost(e.HResult))
                {
                    _canvasDevice = null;
                    recreateDevice = true;
                }
            } while (_canvasDevice == null);
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            await StartCaptureAsync();
        }

        private async void ScreenshotButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            await SaveImageAsync(_screenshotFilename, _currentFrame);
        }

        private async Task SaveImageAsync(string filename, CanvasBitmap frame)
        {
            StorageFolder pictureFolder = KnownFolders.SavedPictures;

            StorageFile file = await pictureFolder.CreateFileAsync(
                filename,
                CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await frame.SaveAsync(fileStream, CanvasBitmapFileFormat.Png, 1f);
            }
        }
    }
}
```

```vb
Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI.Composition
Imports Windows.Graphics
Imports Windows.Graphics.Capture
Imports Windows.Graphics.DirectX
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Partial Public NotInheritable Class MainPage
    Inherits Page

    ' Capture API objects.
    WithEvents CaptureItem As GraphicsCaptureItem
    WithEvents FramePool As Direct3D11CaptureFramePool

    Private _lastSize As SizeInt32
    Private _session As GraphicsCaptureSession

    ' Non-API related members.
    Private _canvasDevice As CanvasDevice
    Private _compositionGraphicsDevice As CompositionGraphicsDevice
    Private _compositor As Compositor
    Private _surface As CompositionDrawingSurface

    Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub Setup()
        _canvasDevice = New CanvasDevice()
        _compositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(Window.Current.Compositor, _canvasDevice)
        _compositor = Window.Current.Compositor
        _surface = _compositionGraphicsDevice.CreateDrawingSurface(
            New Size(400, 400), DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied)
        Dim visual = _compositor.CreateSpriteVisual()
        visual.RelativeSizeAdjustment = Vector2.One
        Dim brush = _compositor.CreateSurfaceBrush(_surface)
        brush.HorizontalAlignmentRatio = 0.5F
        brush.VerticalAlignmentRatio = 0.5F
        brush.Stretch = CompositionStretch.Uniform
        visual.Brush = brush
        ElementCompositionPreview.SetElementChildVisual(Me, visual)
    End Sub

    Public Async Function StartCaptureAsync() As Task
        ' The GraphicsCapturePicker follows the same pattern the
        ' file pickers do.
        Dim picker As New GraphicsCapturePicker
        Dim item As GraphicsCaptureItem = Await picker.PickSingleItemAsync()

        ' The item may be null if the user dismissed the
        ' control without making a selection or hit Cancel.
        If item IsNot Nothing Then
            StartCaptureInternal(item)
        End If
    End Function

    Private Sub StartCaptureInternal(item As GraphicsCaptureItem)
        ' Stop the previous capture if we had one.
        StopCapture()

        CaptureItem = item
        _lastSize = CaptureItem.Size

        FramePool = Direct3D11CaptureFramePool.Create(
            _canvasDevice, ' D3D device
            DirectXPixelFormat.B8G8R8A8UIntNormalized, ' Pixel format
            2, '  Number of frames
            CaptureItem.Size) ' Size of the buffers

        _session = FramePool.CreateCaptureSession(CaptureItem)
        _session.StartCapture()
    End Sub

    Private Sub FramePool_FrameArrived(sender As Direct3D11CaptureFramePool, args As Object) Handles FramePool.FrameArrived
        ' The FrameArrived event is raised for every frame on the thread
        ' that created the Direct3D11CaptureFramePool. This means we
        ' don't have to do a null-check here, as we know we're the only
        ' one dequeueing frames in our application.  

        ' NOTE Disposing the frame retires it And returns  
        ' the buffer to the pool.

        Using frame = FramePool.TryGetNextFrame()
            ProcessFrame(frame)
        End Using
    End Sub

    Private Sub CaptureItem_Closed(sender As GraphicsCaptureItem, args As Object) Handles CaptureItem.Closed
        StopCapture()
    End Sub

    Public Sub StopCapture()
        _session?.Dispose()
        FramePool?.Dispose()
        CaptureItem = Nothing
        _session = Nothing
        FramePool = Nothing
    End Sub

    Private Sub ProcessFrame(frame As Direct3D11CaptureFrame)
        ' Resize and device-lost leverage the same function on the
        ' Direct3D11CaptureFramePool. Refactoring it this way avoids
        ' throwing in the catch block below (device creation could always
        ' fail) along with ensuring that resize completes successfully And
        ' isn't vulnerable to device-lost.

        Dim needsReset As Boolean = False
        Dim recreateDevice As Boolean = False

        If (frame.ContentSize.Width <> _lastSize.Width) OrElse
            (frame.ContentSize.Height <> _lastSize.Height) Then
            needsReset = True
            _lastSize = frame.ContentSize
        End If

        Try
            ' Take the D3D11 surface and draw it into a  
            ' Composition surface.

            ' Convert our D3D11 surface into a Win2D object.
            Dim bitmap = CanvasBitmap.CreateFromDirect3D11Surface(
                _canvasDevice,
                frame.Surface)

            ' Helper that handles the drawing for us.
            FillSurfaceWithBitmap(bitmap)
            ' This is the device-lost convention for Win2D.
        Catch e As Exception When _canvasDevice.IsDeviceLost(e.HResult)
            ' We lost our graphics device. Recreate it and reset
            ' our Direct3D11CaptureFramePool.  
            needsReset = True
            recreateDevice = True
        End Try

        If needsReset Then
            ResetFramePool(frame.ContentSize, recreateDevice)
        End If
    End Sub

    Private Sub FillSurfaceWithBitmap(canvasBitmap As CanvasBitmap)
        CanvasComposition.Resize(_surface, canvasBitmap.Size)

        Using session = CanvasComposition.CreateDrawingSession(_surface)
            session.Clear(Colors.Transparent)
            session.DrawImage(canvasBitmap)
        End Using
    End Sub

    Private Sub ResetFramePool(size As SizeInt32, recreateDevice As Boolean)
        Do
            Try
                If recreateDevice Then
                    _canvasDevice = New CanvasDevice()
                End If
                FramePool.Recreate(_canvasDevice, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, size)
                ' This is the device-lost convention for Win2D.
            Catch e As Exception When _canvasDevice.IsDeviceLost(e.HResult)
                _canvasDevice = Nothing
                recreateDevice = True
            End Try
        Loop While _canvasDevice Is Nothing
    End Sub

    Private Async Sub Button_ClickAsync(sender As Object, e As RoutedEventArgs) Handles CaptureButton.Click
        Await StartCaptureAsync()
    End Sub

End Class
```

## Record a video

If you want to record a video of your application, you can follow the walkthrough presented in the article [Screen capture to video](screen-capture-video.md). Or, you can use [Windows.Media.AppRecording namespace](/uwp/api/windows.media.apprecording). This is part of the Desktop extension SDK, so it only works on Windows desktops and requires that you add a reference to it from your project. See [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview) for more information.

## See also

* [Windows.Graphics.Capture Namespace](/uwp/api/windows.graphics.capture)
* [Screen capture to video](screen-capture-video.md)
