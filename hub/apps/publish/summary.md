---
description: What’s new - Summary
title: What’s new - Summary
ms.date: 11/24/2025
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, analytics, installs, crash, hang, sessions, ratings
ms.localizationpriority: medium
---

# What’s new: Summary

The new Partner Center Summary page offers developers a powerful, consolidated overview of app health and performance metrics across all apps. This intuitive dashboard lets you instantly understand key indicators over the last 30 days, helping you stay on top of your app ecosystem without sifting through multiple reports. 

### Redesigned dashboard focused on key metrics

:::image type="content" source="images/summary.png" lightbox="images/summary.png" alt-text="A screenshot showing revamped summary page in Partner Center.":::

* **Installs**: See exactly how many new installs your app has received in the past 30 days, helping you identify surges or dips in user interest. Understand adoption and retention with evolving trends over time. 
* **Monthly Active Devices (MAD)**: Get a transparent count of engaged users to measure ongoing app adoption and reach. This helps you gauge both the popularity and sustained usage of your app.
* **Sessions**: Monitor session volume to uncover how often users interact with your app. Visual trends help you pinpoint engagement peaks and seasonal usage patterns.
* **Average Engagement Duration**: Discover how much time users spend in your app, allowing you to evaluate feature stickiness and overall satisfaction.
* **Crash Rate & Hang Rate**: View app stability briefly with real-time monitoring of crash and hang anomalies. Proactively address issues to improve user experience and reduce frustration.
* **Rating Score & Total Ratings**: Access real user feedback to track satisfaction, monitor rating trends, and manage your app’s reputation in the marketplace.

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
