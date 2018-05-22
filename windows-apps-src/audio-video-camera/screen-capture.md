---
author: eliotcowley
title: Screen capture
description: The Windows.Graphics.Capture namespace provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences.
ms.assetid: 349C959D-9C74-44E7-B5F6-EBDB5CA87B9F
ms.author: elcowle
ms.date: 5/21/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, screen capture
ms.localizationpriority: medium
---

# Screen capture

Starting in Windows 10, version 1803, the [Windows.Graphics.Capture](https://docs.microsoft.com/uwp/api/windows.graphics.capture) namespace provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences.

With screen capture, developers invoke secure system UI for end users to pick the display or application window to be captured, and a yellow notification border is drawn by the system around the actively captured item. In the case of multiple simultaneous capture sessions, a yellow border is drawn around each item being captured.

> [!NOTE]
> The screen capture APIs require you to be running Windows 10 Pro or Enterprise.

## Add the screen capture capability

The APIs found in the **Windows.Graphics.Capture** namespace require a general capability to be declared in your application's manifest. You must add this directly to the file:
    
1. Right-click **Package.appxmanifest** in the **Solution Explorer**. 
2. Select **Open With...** 
3. Choose **XML (Text) Editor**. 
4. Select **OK**.
5. In the **Package** node, add the following attribute:
    `xmlns:uap6="http://schemas.microsoft.com/appx/manifest/uap/windows10/6"`
6. Also in the **Package** node, add the following to the **IgnorableNamespaces** attribute:
    `uap6`
7. In the **Capabilities** node, add the following element:
    `<uap6:Capability Name="graphicsCapture"/>`

## Launch the system UI to start screen capture

Before launching the system UI, you can check to see if your application is currently able to take screen captures. There are several reasons why your application might not be able to use screen capture, including if the device does not meet hardware requirements or if the application targeted for capture blocks screen capture. Use the **IsSupported** method in the [GraphicsCaptureSession](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscapturesession) class to determine if UWP screen capture is supported:

```cs
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

Once you've verified that screen capture is supported, use the [GraphicsCapturePicker](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscapturepicker) class to invoke the system picker UI. The end user uses this UI to select the display or application window of which to take screen captures. The picker will return a [GraphicsCaptureItem](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscaptureitem) that will be used to create a **GraphicsCaptureSession**:

```cs
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

## Create a capture frame pool and capture session

Using the **GraphicsCaptureItem**, you will create a [Direct3D11CaptureFramePool](https://docs.microsoft.com/uwp/api/windows.graphics.capture.direct3d11captureframepool) with your D3D device, supported pixel format (**DXGI\_FORMAT\_B8G8R8A8\_UNORM**), number of desired frames (which can be any integer), and frame size. The **ContentSize** property of the **GraphicsCaptureItem** class can be used as the size of your frame:

```cs
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

Next, get an instance of the **GraphicsCaptureSession** class for your **Direct3D11CaptureFramePool** by passing the **GraphicsCaptureItem** to the **CreateCaptureSession** method:

```cs
_session = _framePool.CreateCaptureSession(_item);
```

Once the user has explicitly given consent to capturing an application window or display in the system UI, the **GraphicsCaptureItem** can be associated to multiple **CaptureSession** objects. This way your application can choose to capture the same item for various experiences.

To capture multiple items at the same time, your application must create a capture session for each item to be captured, which requires invoking the picker UI for each item that is to be captured.

## Acquire capture frames

With your frame pool and capture session created, call the **StartCapture** method on your **GraphicsCaptureSession** instance to notify the system to start sending capture frames to your app:

```cs
_session.StartCapture();
```

To acquire these capture frames, which are [Direct3D11CaptureFrame](https://docs.microsoft.com/uwp/api/windows.graphics.capture.direct3d11captureframe) objects, you can use the **Direct3D11CaptureFramePool.FrameArrived** event:

```cs
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

It is recommended to avoid using the UI thread if possible for **FrameArrived**, as this event will be raised every time a new frame is available, which will be frequent. If you do choose to listen to **FrameArrived** on the UI thread, be mindful of how much work you're doing every time the event fires.

Alternatively, you can manually pull frames with the **Direct3D11CaptureFramePool.TryGetNextFrame** method until you get all of the frames that you need.

The **Direct3D11CaptureFrame** object contains the properties **ContentSize**, **Surface**, and **SystemRelativeTime**. The **SystemRelativeTime** is QPC ([QueryPerformanceCounter](https://msdn.microsoft.com/library/windows/desktop/ms644904)) time that can be used to synchronize other media elements.

## Processing capture frames

Each frame from the **Direct3D11CaptureFramePool** is checked out when calling **TryGetNextFrame**, and checked back in according to the lifetime of the **Direct3D11CaptureFrame** object. For native applications, releasing the **Direct3D11CaptureFrame** object is enough to check the frame back in to the frame pool. For managed applications, it is recommended to use the **Direct3D11CaptureFrame.Dispose** (**Close** in C++) method. **Direct3D11CaptureFrame** implements the [IClosable](https://docs.microsoft.com/uwp/api/Windows.Foundation.IClosable) interface, which is projected as [IDisposable](https://msdn.microsoft.com/library/system.idisposable.aspx) for C# callers.

Applications should not save references to **Direct3D11CaptureFrame** objects, nor should they save references to the underlying Direct3D surface after the frame has been checked back in.

While processing a frame, it is recommended that applications take the [ID3D11Multithread](https://msdn.microsoft.com/library/windows/desktop/mt644886) lock on the same device that is associated with the **Direct3D11CaptureFramePool** object.

The underlying Direct3D surface will always be the size specified when creating (or recreating) the **Direct3D11CaptureFramePool**. If content is larger than the frame, the contents are clipped to the size of the frame. If the content is smaller than the frame, then the rest of the frame contains undefined data. It is recommended that applications copy out a sub-rect using the **ContentSize** property for that **Direct3D11CaptureFrame** to avoid showing undefined content.

## React to capture item resizing or device lost

During the capture process, applications may wish to change aspects of their **Direct3D11CaptureFramePool**. This includes providing a new Direct3D device, changing the size of the frame buffers, or even changing the number of buffers within the pool. In each of these scenarios, the **Recreate** method on the **Direct3D11CaptureFramePool** object is the recommended tool.

When **Recreate** is called, all existing frames are discarded. This is to prevent handing out frames whose underlying Direct3D surfaces belong to a device that the application may no longer have access to. For this reason, it may be wise to process all pending frames before calling **Recreate**.

## Putting it all together

The following code snippet is an end-to-end example of how to implement screen capture in a UWP application:

```cs
using Microsoft.Graphics.Canvas;
using System;
using System.Threading.Tasks;
using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.UI.Composition;

namespace CaptureSamples 
{
    class Sample
    {
        // Capture API objects.
        private SizeInt32 _lastSize; 
        private GraphicsCaptureItem _item; 
        private Direct3D11CaptureFramePool _framePool; 
        private GraphicsCaptureSession _session; 

        // Non-API related members.
        private CanvasDevice _canvasDevice; 
        private CompositionDrawingSurface _surface; 

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
            _session.Start(); 
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
                var canvasBitmap = CanvasBitmap.CreateFromDirect3D11Surface( 
                    _canvasDevice, 
                    frame.Surface); 
 
                // Helper that handles the drawing for us, not shown. 
                FillSurfaceWithBitmap(_surface, canvasBitmap); 
            } 
            // This is the device-lost convention for Win2D.
            catch(Exception e) when (_canvasDevice.IsDeviceLost(e.HResult)) 
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
 
        private void ResetFramePool(Vector2 size, bool recreateDevice) 
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
                catch(Exception e) when (_canvasDevice.IsDeviceLost(e.HResult)) 
                { 
                    _canvasDevice = null; 
                    recreateDevice = true; 
                } 
            } while (_canvasDevice == null); 
        } 
    } 
} 
```

## See also

* [Windows.Graphics.Capture Namespace](https://docs.microsoft.com/uwp/api/windows.graphics.capture)