---
title: Address space available for streaming resources
description: This section specifies the virtual address space that is available for streaming resources.
ms.assetid: 145EB4A3-3910-4126-BC7E-A4CF53E2A098
keywords:
- Address space available for streaming resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Address space available for streaming resources


This section specifies the virtual address space that is available for streaming resources.

On 64-bit operating systems, at least 40 bits of virtual address space (1 Terabyte) is available.

For 32-bit operating systems, the address space is 32 bit (4 GB). For 32-bit ARM systems, individual streaming resource creation can fail if the allocation would use more than 27 bits of address space (128 MB). This includes any hidden padding in the address space the hardware may use for mipmaps, packed tile padding, and possibly padding surface dimensions to powers of 2.

On graphics systems with a separate page table for the graphics processing unit (GPU), most of this address space will be available to GPU resources made by the application, though GPU allocations made by the display driver fit in the same space.

On future systems with a page table shared between the CPU and GPU, the available address space is shared between all CPU and GPU allocations in a process.

## <span id="related-topics"></span>Related topics


[Streaming resource creation parameters](streaming-resource-creation-parameters.md)

 

 




