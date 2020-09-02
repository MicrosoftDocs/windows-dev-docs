---
title: Reduce latency with DXGI 1.3 swap chains
description: Use DXGI 1.3 to reduce the effective frame latency by waiting for the swap chain to signal the appropriate time to begin rendering a new frame.
ms.assetid: c99b97ed-a757-879f-3d55-7ed77133f6ce
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, latency, dxgi, swap chains, directx
ms.localizationpriority: medium
---

# Reduce latency with DXGI 1.3 swap chains

Use DXGI 1.3 to reduce the effective frame latency by waiting for the swap chain to signal the appropriate time to begin rendering a new frame. Games typically need to provide the lowest amount of latency possible from the time the player input is received, to when the game responds to that input by updating the display. This topic explains a technique available starting in Direct3D 11.2 that you can use to minimize the effective frame latency in your game.

## How does waiting on the back buffer reduce latency?

With the flip model swap chain, back buffer "flips" are queued whenever your game calls [**IDXGISwapChain::Present**](/windows/win32/api/dxgi/nf-dxgi-idxgiswapchain-present). When the rendering loop calls Present(), the system blocks the thread until it is done presenting a prior frame, making room to queue up the new frame, before it actually presents. This causes extra latency between the time the game draws a frame and the time the system allows it to display that frame. In many cases, the system will reach a stable equilibrium where the game is always waiting almost a full extra frame between the time it renders and the time it presents each frame. It's better to wait until the system is ready to accept a new frame, then render the frame based on current data and queue the frame immediately.

Create a waitable swap chain with the [**DXGI\_SWAP\_CHAIN\_FLAG\_FRAME\_LATENCY\_WAITABLE\_OBJECT**](/windows/win32/api/dxgi/ne-dxgi-dxgi_swap_chain_flag) flag. Swap chains created this way can notify your rendering loop when the system is actually ready to accept a new frame. This allows your game to render based on current data and then put the result in the present queue right away.

## Step 1. Create a waitable swap chain

Specify the [**DXGI\_SWAP\_CHAIN\_FLAG\_FRAME\_LATENCY\_WAITABLE\_OBJECT**](/windows/win32/api/dxgi/ne-dxgi-dxgi_swap_chain_flag) flag when you call [**CreateSwapChainForCoreWindow**](/windows/win32/api/dxgi1_2/nf-dxgi1_2-idxgifactory2-createswapchainforcorewindow).

```cpp
swapChainDesc.Flags = DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT; // Enable GetFrameLatencyWaitableObject().
```

> [!NOTE]
> In contrast to some flags, this flag can't be added or removed using [**ResizeBuffers**](/windows/win32/api/dxgi/nf-dxgi-idxgiswapchain-resizebuffers). DXGI returns an error code if this flag is set differently from when the swap chain was created.

```cpp
// If the swap chain already exists, resize it.
HRESULT hr = m_swapChain->ResizeBuffers(
    2, // Double-buffered swap chain.
    static_cast<UINT>(m_d3dRenderTargetSize.Width),
    static_cast<UINT>(m_d3dRenderTargetSize.Height),
    DXGI_FORMAT_B8G8R8A8_UNORM,
    DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT // Enable GetFrameLatencyWaitableObject().
    );
```

## Step 2. Set the frame latency

Set the frame latency with the [**IDXGISwapChain2::SetMaximumFrameLatency**](/windows/win32/api/dxgi1_3/nf-dxgi1_3-idxgiswapchain2-setmaximumframelatency) API, instead of calling [**IDXGIDevice1::SetMaximumFrameLatency**](/windows/win32/api/dxgi/nf-dxgi-idxgidevice1-setmaximumframelatency).

By default, the frame latency for waitable swap chains is set to 1, which results in the least possible latency but also reduces CPU-GPU parallelism. If you need increased CPU-GPU parallelism to achieve 60 FPS - that is, if the CPU and GPU each spend less than 16.7 ms a frame processing rendering work, but their combined sum is greater than 16.7 ms — set the frame latency to 2. This allows the GPU to process work queued up by the CPU during the previous frame, while at the same time allowing the CPU to submit rendering commands for the current frame independently.

```cpp
// Swapchains created with the DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT flag use their
// own per-swapchain latency setting instead of the one associated with the DXGI device. The
// default per-swapchain latency is 1, which ensures that DXGI does not queue more than one frame
// at a time. This both reduces latency and ensures that the application will only render after
// each VSync, minimizing power consumption.
//DX::ThrowIfFailed(
//    swapChain2->SetMaximumFrameLatency(1)
//    );
```

## Step 3. Get the waitable object from the swap chain

Call [**IDXGISwapChain2::GetFrameLatencyWaitableObject**](/windows/win32/api/dxgi1_3/nf-dxgi1_3-idxgiswapchain2-getframelatencywaitableobject) to retrieve the wait handle. The wait handle is a pointer to the waitable object. Store this handle for use by your rendering loop.

```cpp
// Get the frame latency waitable object, which is used by the WaitOnSwapChain method. This
// requires that swap chain be created with the DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT
// flag.
m_frameLatencyWaitableObject = swapChain2->GetFrameLatencyWaitableObject();
```

## Step 4. Wait before rendering each frame

Your rendering loop should wait for the swap chain to signal via the waitable object before it begins rendering every frame. This includes the first frame rendered with the swap chain. Use [**WaitForSingleObjectEx**](/windows/win32/api/synchapi/nf-synchapi-waitforsingleobjectex), providing the wait handle retrieved in Step 2, to signal the start of each frame.

The following example shows the render loop from the DirectXLatency sample:

```cpp
while (!m_windowClosed)
{
    if (m_windowVisible)
    {
        // Block this thread until the swap chain is finished presenting. Note that it is
        // important to call this before the first Present in order to minimize the latency
        // of the swap chain.
        m_deviceResources->WaitOnSwapChain();

        // Process any UI events in the queue.
        CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

        // Update app state in response to any UI events that occurred.
        m_main->Update();

        // Render the scene.
        m_main->Render();

        // Present the scene.
        m_deviceResources->Present();
    }
    else
    {
        // The window is hidden. Block until a UI event occurs.
        CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
    }
}
```

The following example shows the WaitForSingleObjectEx call from the DirectXLatency sample:

```cpp
// Block the current thread until the swap chain has finished presenting.
void DX::DeviceResources::WaitOnSwapChain()
{
    DWORD result = WaitForSingleObjectEx(
        m_frameLatencyWaitableObject,
        1000, // 1 second timeout (shouldn't ever occur)
        true
        );
}
```

## What should my game do while it waits for the swap chain to present?

If your game doesn’t have any tasks that block on the render loop, letting it wait for the swap chain to present can be advantageous because it saves power, which is especially important on mobile devices. Otherwise, you can use multithreading to accomplish work while your game is waiting for the swap chain to present. Here are just a few tasks that your game can complete:

-   Process network events
-   Update the AI
-   CPU-based physics
-   Deferred-context rendering (on supported devices)
-   Asset loading

For more information about multithreaded programming in Windows, see the following related topics.

## Related topics

* [DirectX latency sample application](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/DirectX%20latency%20sample)
* [**IDXGISwapChain2::GetFrameLatencyWaitableObject**](/windows/win32/api/dxgi1_3/nf-dxgi1_3-idxgiswapchain2-getframelatencywaitableobject)
* [**WaitForSingleObjectEx**](/windows/win32/api/synchapi/nf-synchapi-waitforsingleobjectex)
* [**Windows.System.Threading**](/uwp/api/Windows.System.Threading)
* [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md)
* [Processes and Threads](/windows/win32/procthread/processes-and-threads)
* [Synchronization](/windows/win32/sync/synchronization)
* [Using Event Objects (Windows)](/windows/win32/sync/using-event-objects)