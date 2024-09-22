---
title: Choosing between Visual Studio Performance Profiler and Windows Performance Toolkit
description: Learn how to select between Visual Studio Performance Profiler and Windows Performance Toolkit for general purpose performance analysis activities on Windows
ms.topic: article
ms.date: 11/05/2021
ms.localizationpriority: medium
---

# Choosing among Visual Studio Performance Profiler, Windows Performance Toolkit, and PerfView

This guide provides a general comparison of the primary general-purpose performance profiling technologies at Microsoft: Visual Studio Performance Profiler, Windows Performance Toolkit, and PerfView.

These tools can help you to diagnose and understand the performance characteristics of your applications on Windows. The goal of this guide is to provide an overview of when to use one over another, highlighting each of their strengths, and offering a brief overview of the functionality they contain.

## Overview

[Visual Studio Performance Profiler](/visualstudio/profiling) is created by the Visual Studio team for quickly understanding performance characteristics of an application under development inside the existing Visual Studio project system. Its strength is integrating tightly with the active development project, simplifying analysis of the most common performance scenarios, and quick, easy collection of only a single application.

[Windows Performance Toolkit](/windows-hardware/test/wpt) is created by the Windows team for understanding system wide characteristics of the entire PC at once. While it grew up from the need to analyze hardware and drivers, it is very effective on understanding software problems as well. Its strength is around gathering large quantities of information from the entire machine at once, so multi-process issues, those dealing with hardware or drivers, and complex scenarios are well-matched for these tools.

[PerfView](https://github.com/microsoft/perfview/) is created by the .NET team for understanding the performance of .NET applications.  Like Windows Performance Toolkit, it can gather large quantities of information from the entire machine at once. It is differentiated by its ability to display very detailed information about .NET runtime services such as garbage collection, just-in-time compilation, and the managed thread pool. PerfView can be used for managed, native, and mixed-mode applications.

There is overlap between these tools. Often you can start exploring an issue in one tool and switch to another for a different view of the same scenario. Other times, one of the tools will be more effective than the others.

## Installation

The Visual Studio Performance Profiler is a component of Visual Studio itself and is installable through the same [installation wizard](/visualstudio/install/install-visual-studio) as the rest of the development environment.

The Windows Performance Toolkit is downloadable separately as a part of the [Windows Assessment and Deployment Kit](/windows-hardware/get-started/adk-install). However, the command-line Performance Recorder tool is pre-installed with Windows 10 and Windows 11 as `wpr.exe` available on the default path variable from shells like PowerShell and the Command Prompt.

The Performance Analyzer is available as an [application in the Microsoft Store](https://www.microsoft.com/store/productId/9N0W1B2BXGNZ).

PerfView is available as a standalone download in the [PerfView GitHub Releases](https://github.com/microsoft/perfview/releases) page. No installation is required.

### Customizing for advanced performance analysis scenarios

Windows Performance Toolkit additionally offers two extensibility points that can serve advanced performance analysis scenarios.

- The [Microsoft Performance Toolkit SDK](https://github.com/microsoft/microsoft-performance-toolkit-sdk) handles the processing of trace data and enables developers to build their own plugins to be viewed inside the Windows Performance Analyzer.
- [.NET TraceProcessing](../trace-processing/index.yml) allows the authoring of custom tools that can process trace information into resulting tables and is especially useful for automated analysis of bulk trace data.

PerfView is similarly based on the .NET [TraceEvent](https://www.nuget.org/packages/Microsoft.Diagnostics.Tracing.TraceEvent/) library for programmatically consuming performance traces. TraceEvent can be used independently of PerfView to perform custom analysis of performance trace data.

> [!NOTE]
> For performance analysis scenarios to work, you will need access to the [**symbols**](/windows/win32/debug/finding-symbols) that correlate with the Windows application being tested. When building with Visual Studio, they will be located the same as in the [**debugging scenario settings**](/visualstudio/profiling/optimize-profiler-settings#symbol-settings), either built with your solution or captured from symbol servers. When analyzing other libraries or components, you will have to locate the symbols for those components to complete your analysis.

## Considerations for choosing a tool

The tool to choose depends on the performance scenario that you are attempting to explore. A comparison of functionalities and traits among the tools is in the table below:

> [!TIP]
> For a general rule, start with *Visual Studio Profiling* when possible. Move on to *Windows Performance Toolkit* or *PerfView* when reaching the limits of what the Visual Studio tools can do.

|*Situation*|Visual Studio Performance Profiler|Windows Performance Toolkit|PerfView|
|--|--|--|--|
|[**General use considerations**](#general-use-considerations)|||
|Trace File Size|✔️|🆗|🆗|
|Acquisition of Tooling|✔️|✔️|✔️|
|Extensibility Kits|❌|✔️|✔️|
|[**Scope of work considerations**](#scope-of-work-considerations)|||
|Single Process or Project|✔️|🆗|🆗|
|Multiple Processes|❌|✔️|✔️|
|[**Hardware considerations**](#hardware-considerations)|||
|CPU Usage|✔️|✔️|✔️|
|GPU Usage|✔️|✔️|❌|
|Memory Usage|✔️|✔️|✔️|
|Device Input/Output|❌|✔️|✔️|
|Power Usage|❌|✔️|❌|
|System Handles|❌|✔️|✔️|
|[**Code language support considerations**](#code-language-support-considerations)|||
|Support for C/C++|✔️|✔️|✔️|
|Support for .NET|✔️|✔️|✔️|
|Support for JavaScript|🆗|🆗|🆗|
|[**Scenario considerations**](#scenario-considerations)|||
|Event Tracing for Windows|🆗|✔️|✔️|
|Composition|❌|✔️|❌|
|HTML/Edge/Internet Explorer/Webview|❌|✔️|❌|
|XAML/WinUI|🆗|✔️|✔️|
|Audio/Video pipelines and glitches|❌|✔️|❌|
|Database timing|✔️|❌|❌|
|Managed object allocation and garbage collection|✔️|❌|✔️|
|Custom Scenarios|❌|✔️|✔️|

### Key

- ✔️ Well supported: Designed for the intended task and achieves robust and detailed results.
- 🆗 Supported: May require additional configuration or steps to achieve the desired results. May contain a limited scope of action within the category.
- ❌ Not supported: Not designed for this use.

## General use considerations

For those just getting started with performance analysis, we recommend Visual Studio Performance Profiler as a well-integrated feature inside the existing Visual Studio development suite. We recommend using Visual Studio Performance Profiler over PerfView if it meets your needs.

For more complex system performance analysis that may require more power and versatility, we recommend Windows Performance Toolkit, which consists of two tools used to accomplish performance analysis tasks:

- The **Windows Performance Recorder**, available both as a command-line tool and with a graphical interface, is responsible for capturing the trace session.
- The **Windows Performance Analyzer** is opened later to post-process the collected data and provide a highly customizable analysis view.

A few of the benefits that Visual Studio Performance Profiler offers include:

- A **[good introduction](/visualstudio/profiling/performance-explorer)** to performance analysis in the major domains.
- It **handles many of the complexities of analysis and debugging automatically** based on the project configuration.
- It **automatically highlights major areas of concern**.
- It is **better for focusing on just one application**, with a smaller, more focused data set and a smaller collection scope.
- More specific focus translates to **less impact on other applications** and machine hardware while profiling occurs, a **reduced overall size to the tracing files** generated and stored, and an **increased processing speed** for reviewing information after collection is complete.
- Takes **less time to start and complete a trace**, with a faster ability to review and turn around because Visual Studio Performance Profiler is concerned only with the application and not the entire system.
- Data collection and resulting analysis are **all performed within Visual Studio**, with analysis pages launching automatically on the conclusion of collection. The report view also automatically tends to focus attention on hot spots or areas of action.

A few of the benefits that Windows Performance Toolkit offers include:

- **Ample [documentation](/windows-hardware/test/wpt/wpt-getting-started-portal) and [blogs](https://devblogs.microsoft.com/performance-diagnostics/)** are provided by the Windows Performance and Diagnostics teams to help you get started.
- It is **better for collecting very large files**, especially from systems that are busy with background tasks. It is a catch-all tool for collecting information that will then be filtered later in the Windows Performance Analyzer interface.
- **Ability to be customized for advanced performance analysis scenarios** using extensibility points. (See below)

A few of the benefits that PerfView offers include:

- Comprehensive **built-in documentation and Internet-accessible how-to videos** linked from within the app.
- It is **easy to deploy** to production environments by simply copying PerfView.exe.
- A **flight-recorder mode** for capturing hard-to-reproduce issues.
- Very detailed diagnostics for .NET runtime services.
- Extensible for custom views.

## Scope of work considerations

For analyses surrounding a **single application** and especially a **single process**, all of the tools are very capable for collecting and understanding performance. The advantage, however, would side with Visual Studio Performance Profiler, especially when the application source and project system is already available. The Visual Studio Performance Profiler engine is designed to collect CPU, GPU, and memory information from a binary in a similar streamlined fashion to the F5 debugging capability. While only focusing on the one application at hand, this collection mechanism offers a tighter turn around and developer loop.

Choose PerfView if Visual Studio doesn't have the necessary capabilities, can't be run due to collection requirements (common in production environments), or more detailed .NET diagnostic capabilities are needed.

For situations with **larger complexity** that include **multiple cross-process requests**, **hardware devices** and their **drivers**, or deep dives into **Windows platform technologies**, the Windows Performance Toolkit is the optimal choice.

## Hardware considerations

Visual Studio Performance Profiler, Windows Performance Toolkit, and PerfView can diagnose CPU and memory for major hardware components, with Visual Studio Performance Profiler and Windows Performance Toolkit additionally supporting GPU. The tools are generally well-matched at introductory analysis in these areas.

**CPU usage** can be analyzed with all three tools and is typically captured using sampling. The sampling captures stack traces from the application periodically and provides a ranking on how often they appear. Both tools can adjust this behavior to instead use instrumentation for exact accounting.

**GPU usage** can be analyzed with both Visual Studio Performance Profiler and Windows Performance Toolkit capturing a general overview of information.

**Memory usage** can be analyzed with all three tools collecting information on the heap space and the stacks associated with allocations in the heap.

When more complex scenarios arise, such as analyzing networking, disk, devices, handles, or overall power consumption of the system, Windows Performance Toolkit is more equipped to handle the analysis. This data is best collected directly from the operating system as it dispatches requests to the various hardware components. Windows Performance Toolkit is developed in tandem with the operating system, so it is ready and capable of collecting this more system-focused category of information.

## Code language support considerations

Windows Performance Toolkit is primarily focused on support for C and C++, as these languages are used in the Windows operating system codebase.

Visual Studio Performance Profiler support is focused on a wider range of programming languages, starting with .NET originating technologies like C# and ASP.NET then expanding outward.

PerfView supports .NET and native (C and C++) applications. It has deep knowledge of .NET runtimes, and capabilities around ASP.NET web workloads.

This is not to say that Visual Studio cannot analyze C or C++ code, or that Windows Performance Analyzer cannot analyze .NET applications, or that PerfView cannot analyze web applications. It is just best to start with the tool most closely matching the application being analyzed to take advantage of each tool's strengths.

## Scenario considerations

All of the tools contain several scenario-based options for studying application performance.

Visual Studio Performance Profiler options tend to focus on .NET, user interface (UI), and databases residing within the application being analyzed.

Windows Performance Toolkit tends to focus on operating system components and frameworks like [composition](/uwp/api/windows.ui.composition), browser views, and glitches in realtime processing pipelines.

For scenarios that require a more customized solution, Windows Performance Recorder can combine both system-wide collection and application-related collection data into a single recording session. This functionality enables an application developer to offer a complete solution to deploy to customers for requesting performance data from hardware that might differ from that used to develop the product. More information on this mechanism can be found at the [Authoring Custom Profiles](https://devblogs.microsoft.com/performance-diagnostics/authoring-custom-profiles-part-1/) blog series by the Windows Performance and Diagnostics team.

PerfView is targeted at deep investigations of .NET applications and runtimes, but is just as capable in multi-process and operating system-level investigations.

## Summary

Visual Studio Performance Profiler, Windows Performance Toolkit, and PerfView are three robust tools from Microsoft available to you for understanding your application's performance. Choosing which tool will best meet your needs requires a variety of considerations regarding your specific situation. We hope that this guide will provide the information needed for you to make wise performance analysis choices, but also welcome you to file feedback on this page below, or for issues specifically pertaining to Windows development performance, please file an issue on the [Windows Dev Performance repo](https://github.com/microsoft/Windows-Dev-Performance).