---
description: What's new - Ratings & Reviews in Partner Center
title: What's new - Ratings & Reviews in Partner Center 
ms.date: 03/16/2026
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, analytics, partner center, ratings and reviews, customer feedback, customer reviews, app reviews, win32, MSI, MSIX, EXE
ms.localizationpriority: medium
---

# What's new - Ratings & Reviews in Partner Center

The Ratings & Reviews experience in Partner Center provides a streamlined way to analyze customer feedback for Windows apps, with a consistent and aligned set of insights available for both Win32 (MSI/EXE) and MSIX apps. 

Recent updates provide a unified set of experiences for both Win32 and MSIX, thus ensuring the same Ratings & Reviews capabilities, layout, and analysis views are available regardless of app packaging. The experience brings together ratings breakdowns, trends over time, geographical distribution, and detailed customer reviews into a single view to help you understand customer sentiment and respond to feedback more effectively. 

### What’s included in the experience 

The Ratings & Reviews dashboard presents a consolidated view of customer feedback across supported Windows app types. The experience is designed to help you move quickly from a high‑level view to detailed review data using the below insights:

#### Ratings breakdown



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
