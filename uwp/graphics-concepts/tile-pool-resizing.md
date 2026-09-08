---
title: Tile pool resizing
description: Find the shared Direct3D 11 guidance for growing and shrinking tile pools and managing tiled resource mappings in a UWP app.
ms.assetid: A54A06DC-BDDB-42DC-85E8-C64241100ED5
keywords:
- Tile pool resizing
ms.date: 09/08/2026
ms.topic: article
ms.localizationpriority: medium
---
# Tile pool resizing

When you use Direct3D 11 tiled resources in a UWP app, the tile pool resizing rules are the same as for other Direct3D 11 apps. The UWP graphics topics refer to these resources as *streaming resources*.

For the shared guidance on `ID3D11DeviceContext2::ResizeTilePool`, see [Tile pool resizing (Direct3D 11)](/windows/win32/direct3d11/tile-pool-resizing). That article explains how growing or shrinking a pool affects existing mappings and when the display driver can release memory. In particular, shrinking the pool alone doesn't release memory that is still referenced by resource mappings.

## <span id="related-topics"></span>Related topics

[Mappings are into a tile pool](mappings-are-into-a-tile-pool.md)
