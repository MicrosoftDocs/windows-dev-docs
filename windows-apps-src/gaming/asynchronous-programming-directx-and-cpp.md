---
title: Asynchronous programming (DirectX and C++)
description: This topic covers various points to consider when you are using asynchronous programming and threading with DirectX.
ms.assetid: 17613cd3-1d9d-8d2f-1b8d-9f8d31faaa6b
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, asynchronous programming, directx
ms.localizationpriority: medium
---
# Asynchronous programming (DirectX and C++)



This topic covers various points to consider when you are using asynchronous programming and threading with DirectX.

## Async programming and DirectX


If you're just learning about DirectX, or even if you're experienced with it, consider putting all your graphics processing pipeline on one thread. In any given scene in a game, there are common resources such as bitmaps, shaders, and other assets that require exclusive access. These same resources require that you synchronize any access to these resources across the parallel threads. Rendering is a difficult process to parallelize across multiple threads.

However, if your game is sufficiently complex, or if you are looking to get improved performance, you can use asynchronous programming to parallelize some of the components that are not specific to your rendering pipeline. Modern hardware features multiple core and hyperthreaded CPUs, and your app should take advantage of this! You can ensure this by using asynchronous programming for some of the components of your game that don't need direct access to the Direct3D device context, such as:

-   file I/O
-   physics
-   AI
-   networking
-   audio
-   controls
-   XAML-based UI components

Your app can handle these components on multiple concurrent threads. File I/O, especially asset loading, benefits greatly from asynchronous loading, because your game or app can be in an interactive state while several (or several hundred) megabytes of assets are being loaded or streamed. The easiest way to create and manage these threads is by using the [Parallel Patterns Library](/cpp/parallel/concrt/parallel-patterns-library-ppl) and the **task** pattern, as contained in the **concurrency** namespace defined in PPLTasks.h. Using the [Parallel Patterns Library](/cpp/parallel/concrt/parallel-patterns-library-ppl) takes direct advantage of multiple core and hyperthreaded CPUs, and can improve everything from perceived load times to the hitches and lags that come with intensive CPU calculations or network processing.

> **Note**   In a Universal Windows Platform (UWP) app, the user interface runs entirely in a single-threaded apartment (STA). If you are creating a UI for your DirectX game using [XAML interop](directx-and-xaml-interop.md), you can only access the controls by using the STA.

 

## Multithreading with Direct3D devices


Multithreading for device contexts is only available on graphics devices that support a Direct3D feature level of 11\_0 or higher. However, you might want to maximize the use of the powerful GPU in many platforms, such as dedicated gaming platforms. In the simplest case, you might want to separate the rendering of a heads-up display (HUD) overlay from the 3D scene rendering and projection and have both components use separate parallel pipelines. Both threads must use the same [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) to create and manage the resource objects (the textures, meshes, shaders, and other assets), though, which is single-threaded, and which requires that you implement some sort of synchronization mechanism (such as critical sections) to access it safely. And, while you can create separate command lists for the device context on different threads (for deferred rendering), you cannot play those command lists back simultaneously on the same **ID3D11DeviceContext** instance.

Now, your app can also use [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device), which is safe for multithreading, to create resource objects. So, why not always use **ID3D11Device** instead of [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext)? Well, currently, driver support for multithreading might not be available for some graphics interfaces. You can query the device and find out if it does support multithreading, but if you are looking to reach the broadest audience, you might stick with single-threaded **ID3D11DeviceContext** for resource object management. That said, when the graphics device driver doesn't support multithreading or command lists, Direct3D 11 attempts to handle synchronized access to the device context internally; and if command lists are not supported, it provides a software implementation. As a result, you can write multithreaded code that will run on platforms with graphics interfaces that lack driver support for multithreaded device context access.

If your app supports separate threads for processing command lists and for displaying frames, you probably want to keep the GPU active, processing the command lists while displaying frames in a timely fashion without perceptible stutter or lag. In this case, you could use a separate [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) for each thread, and to share resources (like textures) by creating them with the D3D11\_RESOURCE\_MISC\_SHARED flag. In this scenario, [**ID3D11DeviceContext::Flush**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-flush) must be called on the processing thread to complete the execution of the command list prior to displaying the results of processing the resource object in the display thread.

## Deferred rendering


Deferred rendering records graphics commands in a command list so that they can be played back at some other time, and is designed to support rendering on one thread while recording commands for rendering on additional threads. After these commands are completed, they can be executed on the thread that generates the final display object (frame buffer, texture, or other graphics output).

Create a deferred context using [**ID3D11Device::CreateDeferredContext**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createdeferredcontext) (instead of [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) or [**D3D11CreateDeviceAndSwapChain**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdeviceandswapchain), which create an immediate context). For more info, see [Immediate and Deferred Rendering](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-render).

## Related topics


* [Introduction to Multithreading in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-intro)

 

 