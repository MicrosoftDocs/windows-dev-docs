---
author: jnHs
Description: The Health report in the Windows Dev Center dashboard lets you get data related to the performance and quality of your app, including crashes and unresponsive events.
title: Health report
ms.assetid: 4F671543-1E91-4E59-88A3-638E3E64539A
ms.author: wdg-dev-content
ms.date: 11/6/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: high
---

# Health report


The **Health** report in the Windows Dev Center dashboard lets you get data related to the performance and quality of your app, including crashes and unresponsive events. You can view this data in your dashboard, or [download the report](download-analytic-reports.md) to view offline. Where applicable, you can view stack traces and/or CAB files for further debugging.

Alternatively, you can programmatically retrieve the data in this report by using the [Microsoft Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).


## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **72H** (72 hours), but you can choose **30D** instead to show data over the last 30 days.

You can also expand **Filters** to filter all of the data on this page by package version, market, and/or device type.

-   **Package version**: The default setting is **All**. If your app includes more than one package, you can choose a specific one here.
-   **Market**: The default filter is **All markets**, but you can limit the data to acquisitions in one or more markets.
-   **Device type**: The default setting is **All**, but you can choose to show data for only one specific device type. Note that the **Other** category includes devices where the make/model is recognized but we aren't able to include it into one of the pre-defined categories shown in this filter. For these devices, the device model can be viewed in the **Failure log** section of the **Failure details** report.  
-   **OS version**: The default is **All OS versions**, but you can choose a specific OS version.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some sections also allow you to apply additional filters.


## Failure hits

The **Failure hits** chart shows the number of daily crashes and events that customers experienced when using your app during the selected period of time. Each type of event that your app experienced is tracked separately: crashes, hangs, JavaScript exceptions, and memory failures.


## Failure hits by market

The **Failure hits by market** chart shows the total number of crashes and events over the selected period of time by market.

You can view this data in a visual **Map** form, or toggle the setting to view it in **Table** form. Table form will show five markets at a time, sorted either alphabetically or by highest/lowest number of user sessions. You can also download the data to view info for all markets together.


## Package version

The **Package version** chart shows the total number of crashes and events over the selected period of time by package version. By default, we show you the package version that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart.

## Failures

The **Failures** chart shows the total number of crashes and events over the selected period of time by failure name. By default, we show you the failure that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart. For each failure, we also show its percentage of the total number of failures.

To display the **Failure details** report for a particular failure, select the failure name. If you have included PDB symbol files, the **Failure details** report includes the number of failure hits over the last month, as well as a failure log that lists occurrence details (date, package version, device type, device model, OS build) and a link to the stack trace and/or CAB file, if available.

> [!TIP]
> CAB files will only be available when the failure occurred on a computer using a Windows Insider build, so not all failures will include the CAB download option. You can click the **Links** header in the **Failure log** to sort the results so that failures which include CAB files appear at the top of the list.

 

 
