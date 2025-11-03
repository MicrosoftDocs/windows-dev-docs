---
description: What’s new - Improved Health Report in Partner Center
title: What’s new - Improved Health Report in Partner Center
ms.date: 08/01/2025
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, analytics, health, crash, failure, hang, stack trace
ms.localizationpriority: medium
---

# What’s new: Improved Health Report in Partner Center

Quality is essential to the success of your app or game on Microsoft Store. To help you monitor and address quality issues more effectively, we will be introducing significant updates to the Health report in Partner Center.
These enhancements are designed to make it easier to identify and analyse failures impacting your customers.

To understand about the improvements in health report for MSIX apps, you can watch the following video.

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=f0541d1e-8db1-4885-afd0-6a6117b06d72]

For more details, refer to the details below.


### Redesigned dashboard focused on key metrics

:::image type="content" source="images/revamped-health-report.png" lightbox="images/revamped-health-report.png" alt-text="A screenshot showing revamped health report in Partner Center.":::

The updated dashboard will bring the most important information to the forefront with key metrics being displayed at the top of the page, making it easier to track quality at a glance.
In the sections below the main dashboard, you’ll find additional metrics and trends over time. You will also be able to compare how your quality metrics evolve with each app update, helping you identify performance improvements or regressions.

### New quality metrics

To provide better visibility into customer impact, we’re introducing following new metrics:

#### Devices affected
This metric will show the number of unique devices per day experiencing crashes, hangs, or other failures like memory failures. If multiple failures occur on the same device in a single day, it will be counted as one device—helping you gauge the breadth of the issue across your user base.

#### Crash rate
The crash rate metric will represent the percentage of daily unique devices that experienced at least one crash. It is calculated by taking the number of unique devices affected by crash divided by the total number of active devices that day. This metric is calculated based on the data collected from devices who have opted in to share optional diagnostic data.

:::image type="content" source="images/failure-rate.png" lightbox="images/failure-rate.png" alt-text="A screenshot showing new Crash Rate and Hang Rate metric in revamped Health report.":::

#### Hang rate
Similar to crash rate, the hang rate metric will indicate the percentage of daily unique devices that experienced at least one hang (when app or game is unresponsiveness). This is also calculated using data from devices that have opted in to share optional diagnostic data.

Together, crash rate and hang rate metrics will help you understand the severity of failures affecting your customers.

### New tools for deeper analysis
To help you investigate and resolve issues more efficiently, we’ve added several new tools to the Health report:

#### New visualization for comparison by app version
You can now compare failure metrics across different versions of your app to track if there are any performance improvements or regressions between releases. You can also compare your failure metrics across different architectures and operating systems to detect patterns and identify the root causes of issues faster.

:::image type="content" source="images/failure-distribution.png" lightbox="images/failure-distribution.png" alt-text="A screenshot showing new visualization for comparing metrics by different dimensions like app version, OS version, architecture.":::

#### Multi-filter support
The simultaneous use of multiple filters is now supported, including multiple app versions, OS versions, device types and more, allowing for more precise and targeted analysis.
