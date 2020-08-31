---
ms.assetid: 64F7FC51-E8AC-4098-9C5F-0172E4724B5C
title: Performance
description: Users expect their apps to remain responsive, to feel natural, and not to drain their battery.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Performance


Users expect their apps to remain responsive, to feel natural, and not to drain their battery. Technically, performance is a non-functional requirement but treating performance as a feature will help you deliver on your users' expectations. Specifying goals, and measuring, are key factors. Determine what your performance-critical scenarios are; define what good performance mean. Then measure early and often enough throughout the lifecycle of your project to be confident you'll hit your goals.. This section shows you how to organize your performance workflow, fix animation glitches and frame rate problems, and tune your startup time, page navigation time, and memory usage.

If you haven't done so already, a step that we've seen result in significant performance improvements is just porting your app to target Windows 10. Several XAML optimizations (for example, [{x:Bind}](../xaml-platform/x-bind-markup-extension.md)) are only available in Windows 10 apps. See [Porting apps to Windows 10](../porting/index.md) and the //build/ session [Moving to the Universal Windows Platform](https://channel9.msdn.com/Events/Build/2015/3-741).

| Topic | Description |
|-------|-------------|
| [Planning for performance](planning-and-measuring-performance.md) | Users expect their apps to remain responsive, to feel natural, and not to drain their battery. Technically, performance is a non-functional requirement but treating performance as a feature will help you deliver on your users' expectations. Specifying goals, and measuring, are key factors. Determine what your performance-critical scenarios are; define what good performance mean. Then measure early and often enough throughout the lifecycle of your project to be confident you'll hit your goals. |
| [Optimize background activity](optimize-background-activity.md) | Create UWP apps that work with the system to use background tasks in a battery-efficient way. |
| [ListView and GridView UI optimization](optimize-gridview-and-listview.md) | Improve [<strong>GridView</strong>](/uwp/api/Windows.UI.Xaml.Controls.GridView) performance and startup time through UI virtualization, element reduction, and progressive updating of items. |
| [ListView and GridView data virtualization](listview-and-gridview-data-optimization.md) | Improve [<strong>GridView</strong>](/uwp/api/Windows.UI.Xaml.Controls.GridView) performance and startup time through data virtualization. |
| [Improve garbage collection performance](improve-garbage-collection-performance.md) | Universal Windows Platform (UWP) apps written in C# and Visual Basic get automatic memory management from the .NET garbage collector. This section summarizes the behavior and performance best practices for the .NET garbage collector in UWP apps. |
| [Keep the UI thread responsive](keep-the-ui-thread-responsive.md) | Users expect an app to remain responsive while it does computation, regardless of the type of machine. This means different things for different apps. For some, this translates to providing more realistic physics, loading data from disk or the web faster, quickly presenting complex scenes and navigating between pages, finding directions in a snap, or rapidly processing data. Regardless of the type of computation, users want their app to act on their input and eliminate instances where it appears unresponsive while it &quot;thinks&quot;. |
| [Optimize your XAML markup](optimize-xaml-loading.md) | Parsing XAML markup to construct objects in memory is time-consuming for a complex UI. Here are some things you can do to improve XAML markup parse and load time and memory efficiency for your app. | 
| [Optimize your XAML layout](optimize-your-xaml-layout.md) | Layout can be an expensive part of a XAML app—both in CPU usage and memory overhead. Here are some simple steps you can take to improve the layout performance of your XAML app. | 
| [MVVM and language performance tips](mvvm-performance-tips.md) | This topic discusses some performance considerations related to your choice of software design patterns, and programming language. |
| [Best practices for your app's startup performance](best-practices-for-your-app-s-startup-performance.md) | Create UWP apps with optimal startup times by improving the way you handle launch and activation. |
| [Optimize animations, media, and images](optimize-animations-and-media.md) | Create Universal Windows Platform (UWP) apps with smooth animations, high frame rate, and high-performance media capture and playback. |
| [Optimize suspend/resume](optimize-suspend-resume.md) | Create UWP apps that streamline their use of the process lifetime system to resume efficiently after suspension or termination. |
| [Optimize file access](optimize-file-access.md) | Create UWP apps that access the file system efficiently, avoiding performance issues due to disk latency and memory/CPU cycles. |
| [Windows Runtime components and optimizing interop](windows-runtime-components-and-optimizing-interop.md) | Create UWP apps that use UWP Components and interop between native and managed types while avoiding interop performance issues. |
| [Tools for profiling and performance](tools-for-profiling-and-performance.md) | Microsoft provides several tools to help you improve the performance of your UWP app.|