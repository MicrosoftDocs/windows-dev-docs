---
description: The Windows.Graphics.Capture namespace provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences.
title: Screen capture
ms.date: 05/13/2026
ms.topic: how-to
dev_langs:
- csharp
keywords: windows, winui, screen capture, graphics capture
ms.localizationpriority: medium
---

# Screen capture

The [Windows.Graphics.Capture](/uwp/api/windows.graphics.capture) namespace provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences.

With screen capture, developers invoke secure system UI for end users to pick the display or application window to be captured, and a yellow notification border is drawn by the system around the actively captured item. In the case of multiple simultaneous capture sessions, a yellow border is drawn around each item being captured.

> [!NOTE]
> The screen capture APIs are only supported on Windows desktop devices and Windows Mixed Reality immersive headsets.

This article describes capturing a single image of the display or application window.

## Check for screen capture support

Before attempting to capture, check whether the current device supports screen capture. Use the [**IsSupported**](/uwp/api/windows.graphics.capture.graphicscapturesession.issupported) method on [**GraphicsCaptureSession**](/uwp/api/windows.graphics.capture.graphicscapturesession) to determine if screen capture is available:

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/screen-capture-winui/cs/ScreenCaptureWinUI/MainWindow.xaml.cs" id="SnippetCheckSupport":::

There are several reasons why screen capture might not be supported, including if the device doesn't meet hardware requirements.

## Launch the system UI to start screen capture

Use the [**GraphicsCapturePicker**](/uwp/api/windows.graphics.capture.graphicscapturepicker) class to invoke the system picker UI. The end user uses this UI to select the display or application window to capture. The picker returns a [**GraphicsCaptureItem**](/uwp/api/windows.graphics.capture.graphicscaptureitem) that is used to create a capture session.

In a WinUI 3 app, you must initialize the picker with the window handle before calling [**PickSingleItemAsync**](/uwp/api/windows.graphics.capture.graphicscapturepicker.picksingleitemasync):

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/screen-capture-winui/cs/ScreenCaptureWinUI/MainWindow.xaml.cs" id="SnippetStartCapture":::

## Create a capture frame pool and capture session

Using the **GraphicsCaptureItem**, create a [**Direct3D11CaptureFramePool**](/uwp/api/windows.graphics.capture.direct3d11captureframepool) with your D3D device, a supported pixel format (**DXGI\_FORMAT\_B8G8R8A8\_UNORM**), number of desired frames (which can be any integer), and frame size. The **Size** property of the **GraphicsCaptureItem** class can be used as the size of your frame:

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/screen-capture-winui/cs/ScreenCaptureWinUI/MainWindow.xaml.cs" id="SnippetCreateFramePool":::

> [!NOTE]
> On systems with Windows HD color enabled, the content pixel format might not necessarily be **DXGI\_FORMAT\_B8G8R8A8\_UNORM**. To avoid pixel overclipping (i.e. the captured content looks washed out) when capturing HDR content, consider using **DXGI\_FORMAT\_R16G16B16A16\_FLOAT** for every component in the capturing pipeline, including the [**Direct3D11CaptureFramePool**](/uwp/api/windows.graphics.capture.direct3d11captureframepool), the target destination such as [**CanvasBitmap**](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm). Depending on the need, additional processing such as saving to HDR content format or HDR-to-SDR tone mapping might be required. This article focuses on SDR content capturing. For more information, see [Using DirectX with high dynamic range displays and advanced color](/windows/win32/direct3darticles/high-dynamic-range).

Once the user has explicitly given consent to capturing an application window or display in the system UI, the **GraphicsCaptureItem** can be associated with multiple capture sessions. This way your application can choose to capture the same item for various experiences.

## Acquire capture frames

With your frame pool and capture session created, call [**StartCapture**](/uwp/api/windows.graphics.capture.graphicscapturesession.startcapture) on your **GraphicsCaptureSession** instance to notify the system to start sending capture frames to your app.

To acquire these capture frames, which are [**Direct3D11CaptureFrame**](/uwp/api/windows.graphics.capture.direct3d11captureframe) objects, use the [**Direct3D11CaptureFramePool.FrameArrived**](/uwp/api/windows.graphics.capture.direct3d11captureframepool.framearrived) event:

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/screen-capture-winui/cs/ScreenCaptureWinUI/MainWindow.xaml.cs" id="SnippetFrameArrived":::

It's recommended to avoid doing heavy work on the UI thread for **FrameArrived**, as this event fires every time a new frame is available. If you choose to listen to **FrameArrived** on the UI thread, be mindful of how much work you're doing every time the event fires.

Alternatively, you can manually pull frames with the [**Direct3D11CaptureFramePool.TryGetNextFrame**](/uwp/api/windows.graphics.capture.direct3d11captureframepool.trygetnextframe) method until you get all of the frames that you need.

The **Direct3D11CaptureFrame** object contains the properties **ContentSize**, **Surface**, and **SystemRelativeTime**. The **SystemRelativeTime** is QPC ([QueryPerformanceCounter](/windows/desktop/api/profileapi/nf-profileapi-queryperformancecounter)) time that can be used to synchronize other media elements.

## Process capture frames

Each frame from the **Direct3D11CaptureFramePool** is checked out when calling **TryGetNextFrame**, and checked back in according to the lifetime of the **Direct3D11CaptureFrame** object. For managed applications, it's recommended to use the **Direct3D11CaptureFrame.Dispose** method. **Direct3D11CaptureFrame** implements [**IDisposable**](/dotnet/api/system.idisposable), so disposing the frame returns the buffer to the pool.

Applications should not save references to **Direct3D11CaptureFrame** objects, nor should they save references to the underlying Direct3D surface after the frame has been checked back in.

In this example, each frame is converted to a [**CanvasBitmap**](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm), which is part of the [Win2D](https://microsoft.github.io/Win2D/WinUI3/html/Introduction.htm) library:

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/screen-capture-winui/cs/ScreenCaptureWinUI/MainWindow.xaml.cs" id="SnippetProcessFrame":::

The underlying Direct3D surface is always the size specified when creating (or recreating) the **Direct3D11CaptureFramePool**. If content is larger than the frame, the contents are clipped to the size of the frame. If the content is smaller than the frame, then the rest of the frame contains undefined data. It's recommended that applications copy out a sub-rect using the **ContentSize** property for that **Direct3D11CaptureFrame** to avoid showing undefined content.

## Save a screenshot

Once you have a **CanvasBitmap**, you can save it as an image file. The following example saves the current frame as a PNG file using a [**FileSavePicker**](/uwp/api/Windows.Storage.Pickers.FileSavePicker). In a WinUI 3 app, you must initialize the picker with the window handle:

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/screen-capture-winui/cs/ScreenCaptureWinUI/MainWindow.xaml.cs" id="SnippetSaveScreenshot":::

## React to capture item resizing or device lost

During the capture process, applications may wish to change aspects of their **Direct3D11CaptureFramePool**. This includes providing a new Direct3D device, changing the size of the frame buffers, or even changing the number of buffers within the pool. In each of these scenarios, the [**Recreate**](/uwp/api/windows.graphics.capture.direct3d11captureframepool.recreate) method on the **Direct3D11CaptureFramePool** object is the recommended approach.

When **Recreate** is called, all existing frames are discarded. This is to prevent handing out frames whose underlying Direct3D surfaces belong to a device that the application may no longer have access to. For this reason, it may be wise to process all pending frames before calling **Recreate**.

## WinUI 3 considerations

When migrating screen capture code from UWP to WinUI 3 (Windows App SDK), keep the following differences in mind:

- **Window handle initialization** — Pickers like [**GraphicsCapturePicker**](/uwp/api/windows.graphics.capture.graphicscapturepicker) and [**FileSavePicker**](/uwp/api/Windows.Storage.Pickers.FileSavePicker) must be initialized with the window handle using [**InitializeWithWindow**](/windows/apps/develop/ui-input/retrieve-hwnd). For more info, see [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd).
- **Composition APIs on the UI thread** — In WinUI 3, [**CanvasComposition**](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Composition_CanvasComposition.htm) surface operations (such as drawing to a **CompositionDrawingSurface**) must be dispatched to the UI thread using [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue). Frame capture and bitmap creation can happen on the frame pool's background thread, but updating the composition visual must happen on the UI thread.
- **Namespace changes** — Use `Microsoft.UI.Composition`, `Microsoft.UI.Xaml.Hosting`, and `Microsoft.UI.Dispatching` instead of their `Windows.UI` counterparts. The `Windows.Graphics.Capture` namespace remains unchanged.

## See also

- [Windows.Graphics.Capture namespace](/uwp/api/windows.graphics.capture)
- [Win2D for WinUI 3](https://microsoft.github.io/Win2D/WinUI3/html/Introduction.htm)
- [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd)
- [Graphics](/windows/apps/develop/graphics) — Index of graphics development features for Windows apps
- [DirectX Graphics Samples on GitHub](https://github.com/microsoft/DirectX-Graphics-Samples)
