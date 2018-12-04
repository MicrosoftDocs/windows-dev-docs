---
title: Hazard tracking versus tile pool resources
description: For non-streaming resources, Direct3D can prevent certain hazard conditions during rendering, but because hazard tracking would be at a tile level for streaming resources, tracking hazard conditions during rendering of streaming resources might be too expensive.
ms.assetid: 8B0C73D3-3F77-41E8-B17D-C595DEE39E49
keywords:
- Hazard tracking versus tile pool resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Hazard tracking versus tile pool resources


For non-streaming resources, Direct3D can prevent certain hazard conditions during rendering, but because hazard tracking would be at a tile level for streaming resources, tracking hazard conditions during rendering of streaming resources might be too expensive.

For example, for non-streaming resources, the runtime doesn't allow any given SubResource to be bound as an input (such as a Shader Resource View) and as an output (such as a Render Target View) at the same time If such a case is encountered, the runtime unbinds the input. This tracking overhead in the runtime is cheap and is done at the subresource level. One of the benefits of this tracking overhead is to minimize the chances of applications accidentally depending on hardware shader execution order. Hardware shader execution order can vary if not on a given graphics processing unit (GPU), then certainly across different GPUs.

Tracking how resources are bound might be too expensive for streaming resources because tracking is at a tile level. New issues arise such as possibly validating away attempts to render to a Render Target View with one tile mapped to multiple areas in the surface simultaneously. If it turns out this per-tile hazard tracking is too expensive for the runtime, ideally this would at least be an option in the debug layer.

An application must inform the display driver when it has issued a write or read operation to a streaming resource that references tile pool memory that will also be referenced by separate streaming resources in upcoming read or write operations that it is expecting the first operation to complete before the following operations can begin.

## <span id="related-topics"></span>Related topics


[Mappings are into a tile pool](mappings-are-into-a-tile-pool.md)

 

 




