---
title: Lost devices
description: A Direct3D device can be in either an operational state or a lost state.
ms.assetid: 1639CC02-8000-4208-AA95-91C1F0A3B08D
keywords:
- Lost devices
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Lost devices


A Direct3D device can be in either an operational state or a lost state. The *operational* state is the normal state of the device in which the device runs and presents all rendering as expected. The device makes a transition to the *lost* state when an event, such as the loss of keyboard focus in a full-screen application, causes rendering to become impossible. The lost state is characterized by the silent failure of all rendering operations, which means that the rendering methods can return success codes even though the rendering operations fail.

By design, the full set of scenarios that can cause a device to become lost is not specified. Some typical examples include loss of focus, such as when the user presses ALT+TAB or when a system dialog is initialized. Devices can also be lost due to a power management event, or when another application assumes full-screen operation. In addition, any failure from reseting a device puts the device into a lost state.

All methods that derive from [**IUnknown**](/windows/desktop/api/unknwn/nn-unknwn-iunknown) are guaranteed to work after a device is lost. After device loss, each function generally has the following three options:

-   Fail with a "device lost" error - This means that the application needs to recognize that the device was lost, so that the application identifies that something isn't happening as expected.
-   Silently fail, returning S\_OK or any other return code - If a function silently fails, the application generally can't distinguish between the result of "success" and a "silent failure."
-   Return a return code.

## <span id="Responding_to_a_Lost_Device"></span><span id="responding_to_a_lost_device"></span><span id="RESPONDING_TO_A_LOST_DEVICE"></span>Responding to a Lost Device


A lost device must re-create resources (including video memory resources) after it has been reset. If a device is lost, the application queries the device to see if it can be restored to the operational state. If not, the application waits until the device can be restored.

If the device can be restored, the application prepares the device by destroying all video-memory resources and any swap chains. Resetting is the only procedure that has an effect when a device is lost, and is the only way by which an application can change the device from a lost to an operational state. Reset will fail unless the application releases all resources that are allocated, including render targets and depth-stencil surfaces.

For the most part, the high-frequency calls of Direct3D do not return any information about whether the device has been lost. The application can continue to call rendering methods, without receiving notification of a lost device. Internally, these operations are discarded until the device is reset to the operational state.

## <span id="Locking_Operations"></span><span id="locking_operations"></span><span id="LOCKING_OPERATIONS"></span>Locking Operations


Internally, Direct3D does enough work to ensure that a lock operation will succeed after a device is lost. However, it is not guaranteed that the video-memory resource's data will be accurate during the lock operation. It is guaranteed that no error code will be returned. This allows applications to be written without concern for device loss during a lock operation.

## <span id="Resources"></span><span id="resources"></span><span id="RESOURCES"></span>Resources


Resources can consume video memory. Because a lost device is disconnected from the video memory owned by the adapter, it is not possible to guarantee allocation of video memory when the device is lost. As a result, all resource creation methods are implemented to succeed, but do in fact allocate only dummy system memory. Because any video-memory resource must be destroyed before the device is resized, there is no issue of over-allocating video memory. These dummy surfaces allow lock and copy operations to appear to function normally until the application discovers that the device has been lost.

All video memory must be released before a device can be reset from a lost state to an operational state. Other state data is automatically destroyed by the transition to an operational state.

You are encouraged to develop applications with a single code path to respond to device loss. This code path is likely to be similar, if not identical, to the code path taken to initialize the device at startup.

## <span id="Retrieved_Data"></span><span id="retrieved_data"></span><span id="RETRIEVED_DATA"></span>Retrieved Data


Direct3D allows applications to validate texture and render states against single pass rendering by the hardware.

Direct3D also allows applications to copy generated or previously written images from video-memory resources to nonvolatile system-memory resources. Because the source images of such transfers might be lost at any time, Direct3D allows such copy operations to fail when the device is lost.

Copy operations can fail as there is no primary surface when the device is lost. Creating swap chains can also fail because a back buffer can't be created when the device is lost.

## <span id="related-topics"></span>Related topics


[Devices](devices.md)

 

 