---
author: shawjohn
Description: The Installs report in the Windows Dev Center dashboard lets you see how many times your app has been successfully installed on Windows 10 devices.
title: Installs report
ms.author: johnshaw
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, app, installs, installation, report, analytics
ms.assetid: 46c08fd2-00bd-4be5-b29f-01a3b5fea4c2
---

# Installs report

The **Installs** report in the Windows Dev Center dashboard lets you see how many times customers have successfully installed your app on Windows 10 devices. You can view this data in your dashboard, or [download the report](download-analytic-reports.md) to view offline. Alternatively, you can programmatically retrieve this data by using the [get app installs](../monetize/get-app-installs.md) method in the [Windows Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).


## Apply filters


Near the top of the page, you can expand **Apply filters** to filter all of the data on this page by date, device type, and/or by package version.

-   **Date**: The default filter is **Last 30 days**, but you can expand this up to **Last 12 months**.
-   **Device type**: The default filter is **All devices**, but you can select a specific device type (**PC**, **Phone**, **Tablet**, **Virtual machine**, **IoT**, **Holographic**, **Console**, **Other**, or **Unknown**).
-   **Package version**: The default filter is **All versions**, but you can select a specific package version.


## Installs daily


The **Installs daily** chart shows the total number of daily installations of your app over the selected period of time.

The install total includes:
-   **Installs on multiple Windows 10 devices.** For example, if a customer installs your app on two Windows 10 PCs and one Windows 10 phone, that counts as three installs.
-   **Reinstalls.** For example, if a customer installs your app today, uninstalls your app tomorrow, and then reinstalls your app next month, that counts as two installs.

The install total does not include or reflect:
-   **Installs on non-Windows 10 devices.** For example, if a customer installs your app on a device that isn’t running Windows 10, we don’t count that install.
-   **Uninstalls.** For example, if a customer uninstalls your app, we don’t subtract that from the total number of installs.
-   **Updates.** For example, if a customer installs your app today, and then installs an app update a week later, that only counts as one install (not two).
-   **Preinstalls.** For example, if a customer buys a device that has your app preinstalled, we don’t count that as an install.
-   **System-initiated installs.** For example, if Windows installs your app automatically for some reason, we don’t count that as an install.

> **Note**  Currently, you can’t programmatically retrieve **Installs daily** data through an API.

## Markets


The **Markets** chart shows the total number of installs over the selected period of time by market. By default, we show data for all markets. However, you can filter this by a specific market.


## Package version


The **Package version** chart shows the total number of installs over the selected period of time by package version.



 

 
