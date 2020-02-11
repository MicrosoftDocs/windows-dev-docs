---
title: Device types
description: Direct3D device types include Hardware Abstraction Layer (hal) devices and the reference rasterizer.
ms.assetid: 64084B23-10C0-4541-8E93-FB323385D2F0
keywords:
- Device types
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Device types


Direct3D device types include Hardware Abstraction Layer (hal) devices and the reference rasterizer.

## <span id="HAL_Device"></span><span id="hal_device"></span><span id="HAL_DEVICE"></span>HAL Device


The primary device type is the hal device, which supports hardware accelerated rasterization and both hardware and software vertex processing. If the computer on which your application is running is equipped with a display adapter that supports Direct3D, your application should use it for Direct3D operations. Direct3D hal devices implement all or part of the transformation, lighting, and rasterizing modules in hardware.

Applications do not access graphics adapters directly. They call Direct3D functions and methods. Direct3D accesses the hardware through the hal. If the computer that your application is running on supports the hal, it will gain the best performance by using a hal device.

## <span id="Reference_Device"></span><span id="reference_device"></span><span id="REFERENCE_DEVICE"></span>Reference Device


Direct3D supports an additional device type called a reference device or reference rasterizer. Unlike a software device, the reference rasterizer supports every Direct3D feature. This device is intended to be used for debugging purposes and is therefore only available on machines where the DirectX SDK has been installed. Because these features are implemented for accuracy rather than speed and are implemented in software, the results are not very fast. The reference rasterizer does make use of special CPU instructions whenever it can, but it is not intended for retail applications. Use the reference rasterizer only for feature testing or demonstration purposes.

## <span id="HAL_vs_REF"></span><span id="hal_vs_ref"></span><span id="HAL_VS_REF"></span>HAL vs. REF Devices


HAL (Hardware Abstraction Layer) devices and REF (REFerence rasterizer) devices are the two main types of Direct3D device; the first is based around the hardware support, and is very fast but might not support everything; while the second uses no hardware acceleration, so is very slow, but is guaranteed to support the entire set of Direct3D features, in the correct way. In general you'll only ever need to use HAL devices, but if you're using some advanced feature that your graphics card does not support then you might need to fall back to REF.

The other time you might want to use REF is if the HAL device is producing strange results - that is, you're sure your code is correct, but the result is not what you're expecting. The REF device is guaranteed to behave correctly, so you may wish to test your application on the REF device and see if the strange behavior continues. If it doesn't, it means that either (a) your application is assuming that the graphics card supports something that it doesn't, or (b) it's a driver bug. If it still doesn't work with the REF device, then it's an application bug.

## <span id="Hardware_vs_Software"></span><span id="hardware_vs_software"></span><span id="HARDWARE_VS_SOFTWARE"></span>Hardware vs. Software Vertex Processing


Hardware versus Software vertex processing only really applies to HAL devices. When you push vertices through the pipeline, they need to be transformed (by the world, view, and projection matrices in turn) and lit (by D3D's built-in lights) - this processing stage is known as T&L (for Transformation & Lighting). Hardware vertex processing means this is done in hardware, if the hardware supports it; ergo, Software vertex processing is done in software. The general practice is to try creating a Hardware T&L device first, and if that fails try Mixed, and if that fails try Software. (If software fails, give up and exit with an error.)

## <span id="related-topics"></span>Related topics


[Devices](devices.md)

 

 




