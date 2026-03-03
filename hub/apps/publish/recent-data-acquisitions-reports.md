---
description: What’s New - Recent data view in Acquisitions reports
title: What’s New - Recent data view in Acquisitions reports
ms.date: 11/24/2025
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, analytics, acquisitions, release, distribution, telemetry, trends, near real time, nrt, recent data
ms.localizationpriority: medium
---

# What’s New - Recent data view in Acquisitions reports

Developers have consistently requested lower data latency for analytics reports in Partner Center. With the launch of "Recent data" view in Acquisitions reports, data is now refreshed in about 3 hours instead of 30, delivering a faster and more responsive experience.

### Why This Matters to Developers

#### Faster, fresher acquisition data
Acquisition data now flows through a near real time pipeline, cutting typical delay from about 30 hours down to approximately 3 hours for new events to appear in analytics. This tighter feedback loop helps developers validate releases, campaigns, and distribution changes while they are still in flight, rather than waiting for next‑day reports.

#### “Recent data” view with hourly updates

:::image type="content" source="images/nrt-recent-data.png" lightbox="images/nrt-recent-data.png" alt-text="A screenshot showing view recent data banner in Partner Center.":::

The new [Recent data section in the Acquisitions report](https://partner.microsoft.com/dashboard/insights/analytics/store/acquisitions?viewSelected=48h) surfaces Last 24 hours and Last 48 hours views that refresh using hourly telemetry streams.

:::image type="content" source="images/nrt-filter.png" lightbox="images/nrt-filter.png" alt-text="A screenshot showing duation filter for analytics in Partner Center.":::

This view is optimized for quick checks on live activity, enabling developers to observe hourly trends instead of daily trends for the same metrics previously available under other durations.

### Benefits

The combination of lower latency and improved telemetry unlocks several practical benefits for developers. Developers can make faster decisions on promotion performance, detect anomalies or drops early, and trust that acquisition trends are no longer underreported by prior sampling behavior.

### Frequently Asked Questions (FAQs)

#### What metrics are available in the Recent data view?

The Recent data view includes a limited set of charts focused on near real time insights. These currently cover page views, installs, and campaign conversions. For paid apps, the view also includes acquisitions and gross sales.

#### How is the aggregation logic in the Recent data view different from the Daily view?

The Recent data view aggregates data at an hourly level, while the Daily view aggregates data at a daily level. This means the Recent data view counts unique devices per hour, not per day. If the same device performs the same action in multiple hours, each hour is counted separately.
 
For example, if a single device installs your app twice on the same day, once in the morning and once in the evening, the Recent data view will report two installs (one perh hour). In contrast, the Daily view will report one install for that day, as it counts unique devices across the entire day.

#### Does the Recent data view report complete and final numbers?

The data shown in the Recent data view may be partial or still completing, and it helps you understand early trends and directional movement in key metrics shortly after the activity occurs.
