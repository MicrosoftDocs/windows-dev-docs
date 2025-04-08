---
title: Graphics diagnostics tools
description: Learn how to get and use the graphics diagnostics features including Graphics Debugging, Graphics Frame Analysis, and GPU Usage in Visual Studio.
ms.assetid: 629ea462-18ed-a333-07e9-cc87ea2dcd93
ms.date: 11/20/2019
ms.topic: article
keywords: windows 10, uwp, games, graphics, diagnostics, tools, directx
ms.localizationpriority: medium
---

# Graphics diagnostics tools

The graphics diagnostic tools are available from within Windows 10 as an optional feature. To use the graphics diagnostic features (provided in the runtime and Visual Studio) to develop DirectX apps or games, add the optional Graphics Tools feature.

1. Go to **Settings** > **System** > **Optional features** (if on a version older than Windows 10 22H2, navigate to **Settings** > **Apps** > **Apps & features** > **Optional features** instead).

1. If **Graphics Tools** is already listed under **Added features**, then you're done. Otherwise, click **Add a feature**.
1. Search for and/or select **Graphics Tools**, and then click **Add**.

Graphics diagnostics features include the ability to create Direct3D debug devices (via Direct3D SDK Layers) in the DirectX runtime, plus Graphics Debugging, Frame Analysis, and GPU Usage.

-   Graphics Debugging lets you trace the Direct3D calls being made by your app. Then, you can replay those calls, inspect parameters, debug and experiment with shaders, and visualize graphics assets to diagnose rendering issues. You can take logs on Windows PCs, simulators, or devices, and then play them back on different hardware.
- Graphics Frame Analysis in Visual Studio runs on a graphics debugging log, and gathers baseline timing for the Direct3D draw calls. It then performs a set of experiments by modifying various graphics settings, and produces a table of timing results. You can use this data to understand graphics performance issues in your app, and you can review results of the various experiments to identify opportunities for performance improvements.
-   GPU Usage in Visual Studio allows you to monitor GPU use in real time. It collects and analyzes the timing data of the workloads being handled by the CPU and GPU, so that you can determine where any bottlenecks are.

## Related topics

[Graphics Diagnostics Overview in Visual Studio](/visualstudio/debugger/overview-of-visual-studio-graphics-diagnostics?view=vs-2015&preserve-view=true)