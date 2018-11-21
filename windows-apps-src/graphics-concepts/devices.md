---
title: Devices
description: A Direct3D device is the rendering component of Direct3D. A device encapsulates and stores the rendering state, performs transformations and lighting operations, and rasterizes an image to a surface.
ms.assetid: BC903462-A32A-46BA-8411-FB294F5D2CD9
keywords:
- Devices
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Devices


A Direct3D device is the rendering component of Direct3D. A device encapsulates and stores the rendering state, performs transformations and lighting operations, and rasterizes an image to a surface.

Architecturally, Direct3D devices contain a transformation module, a lighting module, and a rasterizing module, as the following diagram shows.

![diagram of the direct3d device architecture](images/d3ddev.png)

Direct3D supports two main types of Direct3D devices:

-   A hal device with hardware-accelerated rasterization and shading with both hardware and software vertex processing
-   A reference device

These devices are two separate drivers. Software and reference devices are represented by software drivers, and the hal device is represented by a hardware driver. The most common way to take advantage of these devices is to use the hal device for shipping applications, and the reference device for feature testing. These are provided by third parties to emulate particular devices - for example, developmental hardware that has not yet been released.

The Direct3D device that an application creates must correspond to the capabilities of the hardware on which the application is running. Direct3D provides rendering capabilities, either by accessing 3D hardware that is installed in the computer or by emulating the capabilities of 3D hardware in software. Therefore, Direct3D provides devices for both hardware access and software emulation.

Hardware-accelerated devices give much better performance than software devices. The hal device type is available on all Direct3D supported graphic adapters. In most cases, applications target computers that have hardware acceleration and rely on software emulation to accommodate lower-end computers.

With the exception of the reference device, software devices do not always support the same features as a hardware device. Applications should always query for device capabilities to determine which features are supported.

Because the behavior of the software and reference devices provided with Direct3D 9 is identical to that of the hal device, application code authored to work with the hal device will work with the software or reference devices without modifications. The provided software or reference device behavior is identical to that of the hal device, but the device capabilities do vary, and a particular software device may implement a much smaller set of capabilities.

## <span id="in-this-section"></span>In this section


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
<td align="left"><p><a href="device-types.md">Device types</a></p></td>
<td align="left"><p>Direct3D device types include Hardware Abstraction Layer (hal) devices and the reference rasterizer.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="windowed-vs--full-screen-mode.md">Windowed vs. full-screen mode</a></p></td>
<td align="left"><p>Direct3D applications can run in either of two modes: windowed or full-screen. In <em>windowed mode</em>, the application shares the available desktop screen space with all running applications. In <em>full-screen mode</em>, the window that the application runs in covers the entire desktop, hiding all running applications (including your development environment).</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="lost-devices.md">Lost devices</a></p></td>
<td align="left"><p>A Direct3D device can be in either an operational state or a lost state. The <em>operational</em> state is the normal state of the device in which the device runs and presents all rendering as expected. The device makes a transition to the <em>lost</em> state when an event, such as the loss of keyboard focus in a full-screen application, causes rendering to become impossible.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="swap-chains.md">Swap chains</a></p></td>
<td align="left"><p>A swap chain is a collection of buffers that are used for displaying frames to the user. Each time an application presents a new frame for display, the first buffer in the swap chain takes the place of the displayed buffer. This process is called <em>swapping</em> or <em>flipping</em>.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="introduction-to-rasterization-rules.md">Introduction to rasterization rules</a></p></td>
<td align="left"><p>Often, the points specified for vertices do not precisely match the pixels on the screen. When this happens, Direct3D applies triangle rasterization rules to decide which pixels apply to a given triangle.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Direct3D Graphics Learning Guide](index.md)

 

 




