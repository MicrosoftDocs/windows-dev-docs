---
description: Use Partner Center analytics to understand how your apps and games published through the Microsoft Store are performing, including acquisition, usage, and health data.
title: Overview
author: Sankalpm-1
ms.author: sankalpm
ms.date: 07/15/2026
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msix, uwp, pwa, unpackaged app, desktop app, traditional desktop app, analytics, insights, Windows app analytics
ms.localizationpriority: medium
---

# Analyze performance for apps and games

Use Partner Center analytics to understand how your apps and games published through the Microsoft Store are performing. Partner Center helps you understand how customers discover, acquire, use, and experience your app or game. These reports provide actionable insights across the entire customer journey, from acquisition and installation through engagement, app health, and customer feedback.

> [!NOTE]
> To use the Microsoft Store analytics API to access your **MSIX** app analytics data programmatically, see [Access analytics data using Store services](/windows/uwp/monetize/access-analytics-data-using-windows-store-services).

Use these reports to:

* Measure app and add-on acquisition performance.
* Track user engagement and retention trends.
* Monitor app quality, reliability, and stability.
* Analyze customer ratings and reviews.
* Identify opportunities to improve product quality and customer satisfaction.
* Discover AI-powered insights and trends that can help guide product decisions.

The reports in this section work together to provide a comprehensive view of your app's performance and help you make data-driven decisions throughout your app's lifecycle.

## Available reports

| Report                                                                                                 | Description          |
|--------------------------------------------------------------------------------------------------------|----------------------|
| [Summary report](/windows/apps/publish/analyze-app-performance/summary-report)  | View a consolidated overview of the most important acquisition, usage, health, and customer feedback metrics for your app. |
| [Acquisitions report](/windows/apps/publish/analyze-app-performance/acquisitions-report)    | See how many people have seen and installed your app in Store. You can also review data for different acquisition channels, markets and platform details in this report. |
| [Add-on acquisition report](/windows/apps/publish/analyze-app-performance/add-on-acquisitions-report)          | See how many add-ons you've sold, along with demographic and platform details. |
| [Usage report](/windows/apps/publish/analyze-app-performance/usage-report)          | See how customers on Windows 10 or Windows 11 (including Xbox) are using your app, including data about custom events that you've defined. |
| [Health report](/windows/apps/publish/analyze-app-performance/health-report)        | Get data related to the performance and quality of your app, including crashes and unresponsive events. |
| [Ratings & Reviews report](/windows/apps/publish/analyze-msi-exe/ratings-reviews-performance)       | See the rating and reviews your customers have left for your app and provide responses to let customers know you’re listening to their feedback. |
| [Insights report](/windows/apps/publish/actionable-analytics-insights)       | See meaningful insights about your app like significant changes (increases or decreases) that we detected over the last 30 days in your acquisitions and health data. |

## Frequently asked questions

1. **How can I track my app’s performance and user engagement in Partner Center?**
  
     Partner Center provides a powerful analytics dashboard that shows how your app is performing after publication. Key reports include:
  
    **Analytics UI for MSIX and MSI apps:** Offers built-in visual reports for:
    - Overview (summary)
    - Acquisition (downloads)
    - Usage (engagement data)
    - Health (crashes and errors)
    - Ratings & Reviews
  
    **Analytics API for MSIX apps:** Programmatic access to your analytics data, useful for integration with your internal tools or dashboards. Allows automated reporting and data retrieval.
  
    **Download Hub (exportable reports) for MSIX apps:** Enables you to download detailed reports (as TSV or CSV files) from each analytics section for deeper offline analysis.
  
    Reports can be viewed directly in Partner Center or downloaded as TSV files for offline review. These insights help you understand your app’s adoption, identify trends, and spot potential areas for improvement.

2. **What functionality is offered in the Download hub for MSIX apps?**

    The Download hub allows you to export analytics data directly from Partner Center for offline analysis. On each analytics page, you can download detailed data (like installs, usage, crashes, or reviews) as TSV or CSV files. Additionally, you can use APIs for automated, scheduled data retrieval.
