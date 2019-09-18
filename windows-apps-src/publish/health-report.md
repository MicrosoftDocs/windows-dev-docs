---
Description: The Health report in Partner Center lets you get data related to the performance and quality of your app, including crashes and unresponsive events.
title: Health report
ms.assetid: 4F671543-1E91-4E59-88A3-638E3E64539A
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, health, crashes, unresponsive events, app health, health data, stack trace, cab file, failure, failures, pdb, symbols
ms.localizationpriority: medium
---
# Health report

The **Health** report in [Partner Center](https://partner.microsoft.com/dashboard) lets you get data related to the performance and quality of your app, including crashes and unresponsive events. You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline. Where applicable, you can view stack traces and/or CAB files for further debugging.

Alternatively, you can programmatically retrieve the data in this report by using the [Microsoft Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).


## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **72H** (72 hours), but you can choose **30D** instead to show data over the last 30 days. Note that data is shown in your local time zone for the **72H** view and in UTC for the **30D** view.

You can also expand **Filters** to filter all of the data on this page by package version, market, and/or device type.

-   **Package version**: The default setting is **All**. If your app includes more than one package, you can choose a specific one here.
-   **Market**: The default filter is **All markets**, but you can limit the data to one or more markets.
-   **Device type**: The default setting is **All**, but you can choose to show data for only one specific device type. Note that the **Other** category includes devices where the make/model is recognized but we aren't able to include it into one of the pre-defined categories shown in this filter. For these devices, the device model can be viewed in the **Failure log** section of the **Failure details** report.  
-   **OS version**: The default is **All OS versions**, but you can choose a specific OS version.
-   **OS release version**: The default is **All OS release versions**, but you can choose a specific release version of the selected **OS version**.
-   **Sandbox**: The default is **Retail**, but for products that use multiple development sandboxes (such as games which integrate with Xbox Live), you can choose a specific one here. (If your product doesn't use sandboxes, this filter will show only **Retail** and won't be applicable.)
-   **Architecture**: The default is **All architectures**, but you can choose a specific system architecture type. This filter is only available when **30D** is selected.
-   **PRAID**: The default setting is **All**, but if you defined multiple package relative app IDs (PRAIDs) when creating your app package, you can choose to show only data related to one PRAID. This filter will not appear if you have not defined multiple PRAIDs.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some sections also allow you to apply additional filters.


## Failure hits

The **Failure hits** chart shows the number of daily crashes and events that customers experienced when using your app during the selected period of time. Each type of event that your app experienced is tracked separately: crashes, hangs, JavaScript exceptions, and memory failures.

When the **30D** time period is selected, you may see circle markers. These represent a significant increase or decrease in a given value that we think you'll want to know about. The date on which the circle appears represents the end of the week in which we detected a significant increase or decrease compared to the week before that. To see more details about what's changed, hover over the circle.  

> [!TIP]
> You can view more insights related to significant changes over the last 30 days in the [Insights report](insights-report.md).

## Failure hits by market

The **Failure hits by market** chart shows the total number of crashes and events over the selected period of time by market.

You can view this data in a visual **Map** form, or toggle the setting to view it in **Table** form. Table form will show five markets at a time, sorted either alphabetically or by highest/lowest number of user sessions. You can also download the data to view info for all markets together.


## Package version

The **Package version** chart shows the total number of crashes and events over the selected period of time by package version. By default, we show you the package version that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart.

## Failures

The **Failures** chart shows the total number of crashes and events over the selected period of time by failure name. Each failure name is made up of four parts: one or more problem classes, an exception/bug check code, the name of the image/driver where the failure occurred, and the associated function name. By default, we show you the failure that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the **Hits** column of this chart. For each failure, we also show its percentage of the total number of failures.

> [!TIP]
> At times, you may see an entry for **Unknown** in this section. This occurs when despite our best efforts, we are unable to collect full details for one or more failures, which will all be grouped together under **Unknown**. Most often, this occurs because of storage constraints, but it can also be a result of a device's privacy settings, network connection issues, partial/bad crash dumps, and other factors.
>
> If you see **!unknown** as part of a failure name, this means that symbols weren’t present, so we couldn’t identify the failure name. Be sure to include symbols in your package to get accurate failure analysis. See [Configure an app package](/windows/msix/package/packaging-uwp-apps#configure-an-app-package). In contrast, failure names that include **!unknown_error_in_** and **!unknown_function** mean that we weren’t able to gather complete details for various other reasons.

To display the **Failure details** report for a particular failure, select the failure name. If you have included symbol files, the **Failure details** report includes the number of failure hits over the last month, as well as a failure log that lists occurrence details (date, package version, device type, device model, OS build) and a link to the stack trace and/or CAB file, if available.

> [!TIP]
> CAB files will only be available when the failure occurred on a computer using a Windows Insider build, so not all failures will include the CAB download option. To show only failures that have CAB files, select **Failures with downloads** in the section filter. You can also click the **Links** header in the **Failure log** to sort the results so that failures which include CAB files appear at the top of the list.

On the **Failure details** page, you'll also see the **Stack prevalence** chart, which shows the top stacks that contributed to the failure, ordered by percentage, and the **Device configuration (30D)** chart, which provides details about the configuration of devices which experienced the failure. 


## Crash-free sessions and devices (30D)

The **Crash-free sessions and devices** chart shows the percent of devices or user sessions that did not experience a crash in the past 30 days. This info helps you understand how broadly your crashes are affecting your users. For example, an app could have 10,000 crashes in one day. If 90% of your devices are affected, then you would probably classify that as critical and act to fix it right away. However, if that only represents 5% of devices using your app, the priority might be lower.

This chart has two tabs:
- **Crash-free devices**: Shows the percentage of unique devices that did not experience a failure on each day (during the past 30 days).
- **Crash-free sessions**: Shows the percentage of unique user sessions that did not experience a failure on each day (during the past 30 days).


 

 
