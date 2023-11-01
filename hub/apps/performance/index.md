---
title: Windows app performance and fundamentals overview
description: Learn about ways to optimize the performance of your Windows apps.
ms.topic: article
ms.date: 05/16/2022
ms.author: mattwoj
author: mattwojo
ms.reviewer: adityar
ms.localizationpriority: medium
#Customer intent: As a Windows application developer, I want to learn how to measure and improve my application's performance so that my users have a better experience.
---

# Windows app performance and fundamentals overview

Improving the performance and fundamentals of your application gives it a feeling of polish and craftsmanship, while saving your customers time, money, battery life, and development effort. Your app will consume less power, improving battery life and reducing carbon emissions. Apps run more smoothly on less expensive hardware. Your customers' productivity increases.

This page provides an overview of the technologies and development tools for measuring, understanding, and refining the performance of your Windows application. If you are writing a UWP application, be sure to also consult the [UWP Performance](/windows/uwp/debug-test-perf/performance-and-xaml-ui) documentation.

## What is application performance and why is it important?

Performance in the context of an application usually revolves around _cost_. How long is it going to take to complete a particular task? How much of the system's resources will be used?

The answers to these questions play a fundamental role in the quality of a user's experience with an application (you can likely recall times where as a user, you have felt frustrated at an application for its poor performance!). As a developer, by keeping performance in mind, you will ensure that users of your applications don't experience that same frustration.

To learn more about the importance of performance and suggestions on how to get started, see this [introduction to the world of performance](introduction.md).

## What tools can I use to measure application performance?

There are a variety of different options available for measuring the performance of your Windows application.

If you are not sure what tools to choose, check out the article: [Choosing between Visual Studio Performance Profiler and Windows Performance Toolkit](./choose-between-tools.md).

### ![Visual Studio Icon](./images/vs.png) Visual Studio Performance Profiler

Visual Studio offers tooling to help you monitor your application and give you insights within your source. Visit the resources below to learn about how you can use these tools to optimize your code right from your development environment.

* [Measure app performance in Visual Studio](/visualstudio/profiling/)

* [Case Study: Using Visual Studio Profiler to reduce memory allocations in the Windows Terminal console host startup path](https://devblogs.microsoft.com/visualstudio/case-study-using-visual-studio-profiler-to-reduce-memory-allocations-in-the-windows-terminal-console-host-startup-path/)

* [Case Study: Double Performance in under 30 minutes](https://devblogs.microsoft.com/visualstudio/case-study-double-performance-in-under-30-minutes/)

### ![Windows Performance Analyzer Icon](./images/wpa.png) Windows Performance Toolkit

[Windows Performance Recorder](/windows-hardware/test/wpt/windows-performance-recorder) and [Windows Performance Analyzer](/windows-hardware/test/wpt/windows-performance-analyzer) enable detailed monitoring and analysis of your application and the entire system using [Event Tracing for Windows (ETW)](/windows-hardware/test/wpt/event-tracing-for-windows). See the links below on how to get started.

* [Getting Started with the Windows Performance Toolkit](/windows-hardware/test/wpt/)

* [Windows Performance Recorder Intro](https://devblogs.microsoft.com/performance-diagnostics/wpr-intro/)

* [Windows Performance Analyzer Intro](https://devblogs.microsoft.com/performance-diagnostics/wpa-intro/)

### ![PerfView Icon](./images/dotnet-logo.png) PerfView

PerfView is an open source monitoring and analysis tool created by the .NET team for investigating .NET performance issues. Because of its ability to decode .NET symbols and managed memory, it is an ideal choice for managed applications.

* [PerfView GitHub Repository](https://github.com/Microsoft/perfview)

* PerfView Tutorial Series

### ![SizeBench Icon](./images/sizebench.png) SizeBench

SizeBench is a utility that helps you investigate and reduce the size of your compiled native code binaries (DLLs, EXEs, and other PE files).

* [SizeBench: A new tool for analyzing Windows binary size](https://devblogs.microsoft.com/performance-diagnostics/sizebench-a-new-tool-for-analyzing-windows-binary-size/).

* [Microsoft Store Download Link](https://aka.ms/SizeBench)

## Additional Resources

### Blogs and news

Go behind the scenes with developer blogs, written by our performance experts to empower building the best version of your app.

* [.NET Performance Blog](https://devblogs.microsoft.com/dotnet/category/performance/)

* [Visual Studio Performance Blogs](https://devblogs.microsoft.com/visualstudio/tag/performance-profiler/)

* [Windows Performance and Diagnostics Blog](https://devblogs.microsoft.com/performance-diagnostics/)

### Community and support

* Performance problems in your compile-debug-test loop? Report them in the [Windows Dev Performance repo](https://github.com/microsoft/Windows-Dev-Performance) on GitHub.

### Performance and Sustainability

Performance engineering intersects directly with the sustainable software movement. Most electrical grids burn fossil fuels to generate electricity. As your application runs on a PC, it consumes incremental power that may be small for a single user but adds up as your user base grows.

Interested in learning how performance engineering intersects with sustainability? 🌍 Check out the [Principles of Green Software Engineering](https://principles.green/) and Microsoft's [Sustainable Software blog](https://devblogs.microsoft.com/sustainable-software/).

* [Measuring Your Application Power and Carbon Impact](https://devblogs.microsoft.com/sustainable-software/measuring-your-application-power-and-carbon-impact-part-1/)

* [Principles of Sustainable Software Engineering](/training/modules/sustainable-software-engineering-overview)

* [Role of Performance Engineering in Designing Carbon Efficient Applications](https://devblogs.microsoft.com/sustainable-software/role-of-performance-engineering-techniques-in-designing-carbon-efficient-applications/)

* [Green Energy Efficient Progressive Web Apps](https://devblogs.microsoft.com/sustainable-software/green-energy-efficient-progressive-web-apps/).

### Our content road map

Performance can be a tricky part of your development process without the right guidance. Ensuring that the right documentation is available for app developers is key to creating faster apps. The road map below details the next pages to be added to this doc set, and in the order that they will be published. If you see something you like or something that’s missing, let us know on GitHub via the Feedback links at the bottom of this page! Our goal is to curate a well-rounded and educational doc set for app performance that meets your needs, so we’d love to hear from you. Please feel welcome to submit feedback for this page below with your recommendations for what you would like to see covered in regard to Windows application performance.

|Topic |Description|
|----------|-----------|
|Introduction to performance areas|Descriptions of what performance means in the context of CPU, memory, GPU, etc.  |
|Identifying what to measure|Depending on your workload, environment, and other factors, certain areas of performance may be more of a focus than others. Learn what to measure and when with this doc|
|Performance Testing Cycle|Step with us through the performance testing lifecycle which includes setting up your test environment, analyzing your results and making product improvements|
|Understanding the different performance tools|This doc will introduce more performance tooling and go over the use cases and best practices for using them|
|Case Studies|A series of end-to-end scenarios and their journeys through the performance testing cycle|