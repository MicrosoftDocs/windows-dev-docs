---
title: Tools for profiling and performance
description: Learn about Visual Studio diagnostic tools and Windows Performance Toolkit for profiling and improving Windows App SDK app performance.
author: GrantMeStrength
ms.author: jken
ms.topic: article
ms.date: 07/08/2026
---

# Tools for profiling and performance

Microsoft provides several tools to help you improve the performance of your Windows App SDK application.

## Visual Studio diagnostic tools

Visual Studio includes built-in diagnostic tools that help you identify performance bottlenecks. These tools show you where your app code spends its time as your program runs.

- **CPU Usage** — identify functions and code paths that consume the most CPU time.
- **Memory Usage** — track your app's memory allocations and find memory leaks.
- **Performance Profiler** — access a comprehensive set of profiling tools from the **Debug** > **Performance Profiler** menu.

For more information, see [Analyze the performance of your apps using Visual Studio diagnostic tools](/visualstudio/profiling/profiling-feature-tour).

## XAML UI Responsiveness tool

The XAML UI Responsiveness tool in Visual Studio measures the performance impact of XAML operations within your app. Use it to identify layout, rendering, and loading issues that affect your app's responsiveness.

## Windows Performance Toolkit

The Windows Performance Toolkit (WPT) provides system-level analysis tools for measuring and optimizing performance:

- **Windows Performance Recorder (WPR)** — capture system-wide performance traces, including CPU, disk, and memory activity.
- **Windows Performance Analyzer (WPA)** — visualize and analyze traces captured by WPR, with detailed views of system information, touch manipulation events, disk I/O, and GPU cost.

For more information, see [Windows Performance Toolkit](/windows-hardware/test/wpt/).

## .NET diagnostic tools

For apps written in C# and .NET, additional diagnostic tools are available:

- **dotnet-counters** — monitor .NET runtime performance counters in real time.
- **dotnet-trace** — collect detailed .NET runtime traces for analysis.
- **dotnet-dump** — capture and analyze process dumps to diagnose memory issues.

For more information, see [.NET diagnostics tools](/dotnet/core/diagnostics/).

## Related content

- [Plan and measure performance](planning-measuring-performance.md)
- [Keep the UI thread responsive](keep-ui-thread-responsive.md)
- [Optimize your XAML markup](optimize-xaml-loading.md)
