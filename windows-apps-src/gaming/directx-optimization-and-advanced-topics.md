---
title: Optimization and advanced topics for DirectX games
description: See articles that give information about optimizing your DirectX game performance and other advanced topics.
ms.assetid: b5f29fb2-3bcf-43b2-9a68-f8819473bf62
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, game, directx, optimize, multisampling, swap chains
ms.localizationpriority: medium
---
# Optimization and advanced topics for DirectX games

This section provides information about optimizing your DirectX game performance and other advanced topics.

Asynchronous programming for games topic discusses the various points to consider when you want to use asynchronous programming to parallelize some of the components and use multithreading to maximise the use of a powerful GPU.

Handle device removed scenarios in Direct3D 11 topic uses a walkthrough to explain how games developed using Direct3D 11 can detect and respond to situations where the graphics adapter is reset, removed, or changed.

Multisampling in UWP apps topic provides an overview of how to use multi-sample antialiasing, a graphics technique to reduce the appearance of aliased edges in UWP games built with Direct3D.

Optimize input and rendering loop topic provides guidance on how to choose the right input event processing option to manage input latency and optimize the rendering loop.

Reduce latency with DXGI 1.3 swap chains topic explains how to reduce effective frame latency by waiting for the swap chain to signal the appropriate time to begin rendering a new frame.

Swap chain scaling and overlays topic explains how to improve rendering times by using scaled swap chains to render real-time game content at a lower resolution than the display is natively capable of. 
It also explains how to create overlay swap chains for devices with the hardware overlay capability; this technique can be used to alleviate the issue of a scaled down UI resulting from the use of swap chain scaling.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="asynchronous-programming-directx-and-cpp.md">Asynchronous programming for games</a></p></td>
<td align="left"><p>Understand asynchronous programming and threading with DirectX.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="handling-device-lost-scenarios.md">Handle device removed scenarios in Direct3D 11</a></p></td>
<td align="left"><p>Recreate the Direct3D and DXGI device interface chain when the graphics adapter is removed or reinitialized.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="multisampling--multi-sample-anti-aliasing--in-windows-store-apps.md">Multisampling in UWP apps</a></p></td>
<td align="left"><p>Use multisampling in UWP games built using Direct3D.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="optimize-performance-for-windows-store-direct3d-11-apps-with-coredispatcher.md">Optimize input and rendering loop</a></p></td>
<td align="left"><p>Reduce input latency and optimize the rendering loop.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="reduce-latency-with-dxgi-1-3-swap-chains.md">Reduce latency with DXGI 1.3 swap chains</a></p></td>
<td align="left"><p>Use DXGI 1.3 to reduce the effective frame latency.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="multisampling--scaling--and-overlay-swap-chains.md">Swap chain scaling and overlays</a></p></td>
<td align="left"><p>Create scaled swap chains for faster rendering on mobile devices, and use overlay swap chains to increase the visual quality.</p></td>
</tr>
</tbody>
</table>