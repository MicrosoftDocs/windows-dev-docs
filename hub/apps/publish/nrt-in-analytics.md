---
description: What’s New - Near Real Time Insights in Analytics for Win32 Apps
title: What’s New - Near Real Time Insights in Analytics for Win32 Apps
ms.date: 11/24/2025
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, analytics, acquisition, release, distribution, telemetry, trends
ms.localizationpriority: medium
---

# What’s new: Near Real Time Insights in Analytics for Win32 Apps

Developers have consistently requested lower data latency for analytics reports in Partner Center. With the launch of Near Real-Time Insights in Partner Center Analytics, acquisitions data is now refreshed in about 3 hours instead of 30, delivering a faster and more responsive experience. We’ve also added a new ‘Recent Data’ view that updates hourly. 

> [!NOTE]
> These capabilities are currently available for Win32 apps and will be extended to MSIX apps soon. 

### Overview

Near Real-Time Insights deliver fresher acquisition data in Store Developer Analytics, enabling developers to track performance and respond to trends within hours rather than days. The experience builds on a new telemetry foundation and a dedicated “Recent data” view designed for short‑window, high‑frequency analysis.

### Why This Matters to Developers

#### Faster, fresher acquisition data
Win32 acquisition data now flows through a near real time pipeline, cutting typical delay from about 30 hours down to approximately 3 hours for new events to appear in analytics. This tighter feedback loop helps developers validate releases, campaigns, and distribution changes while they are still in flight, rather than waiting for next‑day reports.

#### “Recent data” view with hourly updates

:::image type="content" source="images/nrt-recent-data.png" lightbox="images/nrt-recent-data.png" alt-text="A screenshot showing view recent data banner in Partner Center.":::

A new Recent data section in the Acquisitions report (_Apps and Games -> Select Your App -> View Analytics -> Installs_) surfaces Last 24 hours and Last 48 hours views that refresh using hourly telemetry streams.

:::image type="content" source="images/nrt-filter.png" lightbox="images/nrt-filter.png" alt-text="A screenshot showing duation filter for analytics in Partner Center.":::

This view is optimized for quick checks on live activity, enabling developers to observe hourly trends instead of daily trends for the same metrics previously available under other durations.

### Benefits for Win32 analytics

The combination of lower latency and improved telemetry unlocks several practical benefits for Win32 developers. Developers can make faster decisions on promotion performance, detect anomalies or drops early, and trust that acquisition trends are no longer underreported by prior sampling behavior.
