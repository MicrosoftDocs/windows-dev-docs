---
title: Windows app performance and fundamentals overview
description: Learn about ways to optimize the performance of your Windows apps.
ms.topic: concept-article
ms.date: 02/27/2025
ms.reviewer: adityar
ms.localizationpriority: medium
#Customer intent: As a Windows application developer, I want to learn how to measure and improve my application's performance so that my users have a better experience.
---

# Windows app performance and fundamentals overview

Improving the performance and fundamentals of your application gives it a feeling of polish and craftsmanship, while saving your customers time, money, battery life, and development effort. Your app will consume less power, improving battery life and reducing carbon emissions. Apps run more smoothly on less expensive hardware. Your customers' productivity increases.

This page provides an overview of the technologies and development tools for measuring, understanding, and refining the performance of your Windows application.

## What is application performance and why is it important?

Performance is the measure of how effectively your application uses the system's resources to do what you've designed it to do. It covers different aspects of how your program interacts with the underlying device, including:

* CPU usage
* Memory consumption
* Power consumption
* Network and storage utilization
* Animation performance

All of these properties have an element of cost associated with them: for example, how much CPU does my application use? How much of the user's bandwidth will it consume? How fast does this particular page of my application load?

Users expect performance as a fundamental property of the software they use. They want their applications to be responsive and make efficient use of their system's resources. Applications that exhibit poor performance cause frustration, which can lead to reduced user engagement. To provide your customers with the best possible experience, it is therefore crucial to make performance a regular part of your development workflow.

## When should you measure application performance?

Application performance can span many stages of the development process. It has implications on everything ranging from your choice of data structure to the technology that you choose for building your application. Keep performance in mind as you are developing your application, and plan to do regular performance testing as part of updating and maintaining your application.

## How to approach performance measurement

Here are some suggestions for how to approach testing your application for performance.

* **Leverage your knowledge of your application.** Understanding the most common scenarios for your users will enable you to spend your time wisely on optimizing the right things. If you have data available on how users interact with your application, this would be a great time to look at it.
  * Where are your users spending most of their time?
  * What are the most important things that a customer will do with your software?
  * What are your application's hardware requirements?
* **Set performance goals for your most important user scenarios.**
* **Be precise about what you are trying to optimize.** Is it CPU? Battery? Network throughput?
* **Select the tools you will use to do your measurements.**
* **Apply a scientific mindset when testing.** Create benchmarks in a controlled environment. Then, make your change, and re-measure to see how your changes have affected your application's behavior.
* **Add regression testing into your test environment.** This will ensure that your performance metrics don't regress over time.

### Intertwining metrics

While you will typically focus on one area of performance during your analysis, be aware that areas are often intertwined. An improvement in one can cascade into an improvement in the other areas.

For example, fixing power consumption is frequently a synchronization problem. Reducing memory usage can result in reducing the time spent using the CPU. There can also be situations where additional resources spent in one area yield a more impactful improvement in another area—for instance, increasing memory consumption can decrease network or storage utilization through caching.

The decision to make a change depends on what is most important for your customers.

## What tools can I use to measure application performance?

There are a variety of different options available for measuring the performance of your Windows application.

If you are not sure what tools to choose, check out the article: [Choosing between Visual Studio Performance Profiler and Windows Performance Toolkit](../../performance/choose-between-tools.md).

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

Interested in learning how performance engineering intersects with sustainability? Check out the [Principles of Green Software Engineering](https://principles.green/) and Microsoft's [Sustainable Software blog](https://devblogs.microsoft.com/sustainable-software/).

* [Measuring Your Application Power and Carbon Impact](https://devblogs.microsoft.com/sustainable-software/measuring-your-application-power-and-carbon-impact-part-1/)

* [Principles of Sustainable Software Engineering](/training/modules/sustainable-software-engineering-overview)

* [Role of Performance Engineering in Designing Carbon Efficient Applications](https://devblogs.microsoft.com/sustainable-software/role-of-performance-engineering-techniques-in-designing-carbon-efficient-applications/)

* [Green Energy Efficient Progressive Web Apps](https://devblogs.microsoft.com/sustainable-software/green-energy-efficient-progressive-web-apps/).

