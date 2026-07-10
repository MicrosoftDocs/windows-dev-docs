---
title: Profile-Guided Optimization for desktop apps
description: Learn how to use Profile-Guided Optimization (PGO) with your C++ Windows App SDK desktop application for better runtime performance.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Profile-Guided Optimization for desktop apps

Profile-Guided Optimization (PGO) is a compiler optimization technique that uses runtime profiling data to produce more efficient native code. When you use PGO, the compiler rearranges code based on how your app actually runs, resulting in better instruction cache utilization, smaller code size, and faster execution.

> [!IMPORTANT]
> Desktop Windows App SDK apps use the standard MSVC PGO workflow. Unlike UWP apps, desktop apps do not require manually copying `pgort140.dll` into the app package — the PGO runtime library is available through the standard C/C++ runtime.

## How PGO works

PGO is a three-step process:

1. **Instrument** — Build your app with instrumentation enabled. The compiler inserts probes that record how your code executes.
2. **Profile** — Run the instrumented app and exercise your most important scenarios. The profiling data is saved to `.pgc` files.
3. **Optimize** — Rebuild your app using the profiling data. The compiler optimizes code layout, inlining, and branching based on actual runtime behavior.

## Step-by-step guide

### 1. Configure for Release

Set your solution configuration to **Release** (or another configuration that produces optimized code). PGO has no benefit when applied to debug builds.

### 2. Enable Whole Program Optimization

In your project properties, go to **C/C++** > **Optimization** and verify that **Whole Program Optimization** (`/GL`) is enabled.

### 3. Instrument the build

Go to **Linker** > **Optimization** and set **Link Time Code Generation** to **Profile Guided Optimization - Instrument** (`/LTCG:PGInstrument`).

Build your solution. A `.pgd` file is generated alongside your build output.

### 4. Run your app and collect profile data

Run your app and exercise the scenarios you want to optimize — startup, navigation, data processing, or any performance-critical workflow. While the app runs, use the `pgosweep.exe` tool (included in the MSVC toolset) to collect profiling data:

```console
pgosweep.exe MyApp.exe MyApp!Scenario1.pgc
```

You can collect multiple `.pgc` files for different scenarios.

### 5. Merge profile data (optional)

By default, all `.pgc` files placed alongside the `.pgd` file are merged automatically during the optimize step. To weight certain scenarios differently, use the `pgomgr.exe` tool:

```console
pgomgr.exe -merge:3 MyApp!CoreScenario.pgc MyApp.pgd
```

This gives the `CoreScenario` run three times the priority of other runs.

### 6. Optimize the build

Go back to **Linker** > **Optimization** and set **Link Time Code Generation** to **Profile Guided Optimization - Optimize** (`/LTCG:PGOptimize`).

Rebuild the solution. The linker automatically merges all `.pgc` files and produces an optimized binary based on the profiling data.

## Tips for effective PGO

- **Profile realistic scenarios.** The quality of PGO optimization depends on how representative your profiling runs are. Exercise the code paths your users will actually hit.
- **Combine multiple runs.** Collect profiling data from several different usage scenarios to cover a broad range of code paths.
- **Re-profile when code changes significantly.** Profile data becomes stale when you make major changes to your codebase. Re-instrument and re-profile periodically.
- **Measure the impact.** Compare startup time, throughput, and memory usage before and after PGO to verify the optimization is worthwhile.

## Related content

- [Profile-Guided Optimizations (MSVC)](/cpp/build/profile-guided-optimizations)
- [Tools for profiling and performance](profiling-tools.md)
- [Plan and measure performance](planning-measuring-performance.md)
