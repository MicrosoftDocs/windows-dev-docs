---
title: Hazard tracking versus tile pool resources
description: Find the shared Direct3D 11 guidance for tracking resource hazards and synchronizing access to tile pool memory in a UWP app.
ms.assetid: 8B0C73D3-3F77-41E8-B17D-C595DEE39E49
keywords:
- Hazard tracking versus tile pool resources
ms.date: 09/08/2026
ms.topic: article
ms.localizationpriority: medium
---
# Hazard tracking versus tile pool resources

When you use Direct3D 11 tiled resources in a UWP app, follow the same resource-hazard and synchronization rules as other Direct3D 11 apps. The UWP graphics topics refer to these resources as *streaming resources*.

For the shared explanation of subresource-level hazard tracking, aliased tile pool memory, and synchronization with `ID3D11DeviceContext2::TiledResourceBarrier`, see [Hazard tracking versus tile pool resources (Direct3D 11)](/windows/win32/direct3d11/hazard-tracking-versus-tile-pool-resources). That article contains the full guidance rather than a separate UWP copy.

## <span id="related-topics"></span>Related topics

[Mappings are into a tile pool](mappings-are-into-a-tile-pool.md)
