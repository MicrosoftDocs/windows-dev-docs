---
author: jnHs
Description: The Health report in the Windows Dev Center dashboard lets you get data related to the performance and quality of your app, including crashes and unresponsive events.
title: Health report
ms.assetid: 4F671543-1E91-4E59-88A3-638E3E64539A
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Health report


The **Health** report in the Windows Dev Center dashboard lets you get data related to the performance and quality of your app, including crashes and unresponsive events. You can view this data in your dashboard (**Analytics** > **Health**), or [download the report](download-analytic-reports.md) to view offline. Where applicable, you can view stack traces for further debugging. Alternatively, you can programmatically retrieve this data by using the [Windows Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).


> **Note**  If you had previously published apps and viewed performance data, you may notice an increased number of crashes and events reported here. This is because we are able to include more data in this report to give you a more complete picture.

## Apply filters


Near the top of the page, you can expand **Apply filters** to filter all of the data on this page by date range, package version, device type, and/or OS version.

-   **Date**: The default filter is **Last 72 hours**, but you can expand this up to **Last 30 days**.
-   **Package version**: The default setting is **All versions**. If your app includes more than one package version, you can choose a specific one.
-   **Device type**: The default is **All devices**, but you can choose a specific device (**PC**, **Phone**, **Console**, **IoT**, **Holographic**, or **Unknown**).
-   **OS version**: The default is **All OS versions**, but you can choose a specific OS version.

The info in all of the charts listed below will reflect the period of time selected in the **Apply filters** section. By default this will include data for all of your package versions, unless you've used the **Apply filters** to choose only one.

## Total crashes and events


The **Failure hits** chart (formerly known as **Total crashes and events**) shows the number of daily crashes and events that customers experienced when using your app during the selected period of time. Each type of event that your app experienced is tracked separately: crashes, hangs, JavaScript exceptions, or memory failures.

You can optionally filter the results by market and/or by OS version.

## Markets


The **Markets** chart shows the total number of crashes and events over the selected period of time by market. By default, we show you the market that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart.

## Package version


The **Package version** chart shows the total number of crashes and events over the selected period of time by package version. By default, we show you the package version that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart.

## Failures


The **Failures** chart shows the total number of crashes and events over the selected period of time by failure name. By default, we show you the failure that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart. For each failure, we also show its percentage of the total number of failures.

To display the **Failure details** report for a particular failure, select the failure name. If you have included PDB symbol files, the **Failure details** report includes the number of failure hits over the last month, as well as a failure log that lists occurrence details (date, package version, device type, device model, OS build) and a link to the stack trace.

 

 
