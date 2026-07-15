---
description: The acquisitions report in Partner Center lets you see who acquired and installed your app, along with demographic and platform details.
title: Acquisitions report
author: Sankalpm-1
ms.author: sankalpm
ms.date: 07/15/2026
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, analytics, installs, acquisitions, page views, conversion, campaign, gross sales
ms.localizationpriority: medium
---

# Acquisitions report

The [Acquisitions report](https://partner.microsoft.com/dashboard/insights/analytics/store/acquisitions) shows how users discover and acquire your app or game, including breakdowns by page views, installs, conversion, and install success rate. Use this report to understand user acquisition patterns, optimize campaigns, and track the impact of marketing or product changes.

## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **Last 1 month**, but you can choose to show data for 3, 6, or 12 months, or for a custom data range that you specify.

> [!NOTE]
> In addition to the monthly views available in this report, you can now analyze near real-time acquisition data by using the [recent data view](/windows/apps/publish/recent-data-acquisitions-reports). 

You can also expand **Filters** to filter all of the data on this page by market, device type, OS version and/or campaign ID:

- **Market**: The default filter is **All markets**, but you can select one or more markets.
- **Device type**: The default setting is **All devices**. If you want to show data for acquisitions from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.
- **OS Version**: The default setting is **All OS Versions** (Windows 10 and Windows 11), but you can select a specific one here.
- **Campaign ID**: The default setting is all campaign IDs created for your app. If you want to show data from a certain set of campaigns only, you can choose one or more campaign IDs.

The information in all of the listed charts reflects the date range and any filters you select. Some sections also enable you to apply extra filters.  

- **Page Type**: The default filter is **All page types**, but you can select from a certain page type: Product Display Page (PDP), Mini-Product Display Page(Mini-PDP) or Store installer (Executable). The Product Display Page is shown to the user, and contains detailed description, screenshots, etc. The Mini-Product Display Page refers to the small dialog shown to user, which contains concise information like app icon, rating score. Store installer (Executable) shows the number of views for Store standalone installer from websites such as apps.microsoft.com and the Microsoft Store badge on developers' websites; you can select **More details** to view the detailed Product Display Page.
- **Install Type**: The default filter is **All install types**. If you want to show data for first time install(new installation) or reinstall, you can select a specific one here.
- **Package Version**: The default filter is **All install types**, but you can select a certain version of app package for which you want to view data.

### Source breakdown

You can also break down your data by different acquisition sources. By default, you can see data from All sources.

- **Microsoft Store on Windows**: Unique devices who saw your product's store listing by browsing or searching on the Microsoft Store app.
- **Microsoft Store web**: Unique devices who saw your app's Mini-Product Display Page by browsing or searching on Microsoft Store web page [apps.microsoft.com](https://apps.microsoft.com).
- **External web url**: Unique devices who installed your product from any Url on web without seeing your product listing in Store.
- **Custom campaign**: Unique devices who visited your product's Product Display Page or Mini-Product Display Page listing on Microsoft Store app or Microsoft Store web from a [custom campaign](/windows/apps/publish/create-a-custom-app-promotion-campaign).
- **Bing web search**: Unique devices who searched and installed your products from Bing web search.
- **Xbox app on Windows and Xbox**: Unique devices who searched and installed your products from Xbox app on Windows or Xbox console.
- **Windows Start**: Unique devices that installed your app from Windows Start. This breakdown is available only for Installs chart.
- **App restore**: Unique devices that restored apps and from previous PC via the Welcome Back page in Windows Out Of Box Experience. This breakdown is available only for Installs chart.

## Acquisition funnel

The **Acquisition funnel** shows how many devices completed each stage involved in your product's installation from Microsoft Store (including Xbox) from discovery, install attempts, acquisition (or purchase), successful installation to launches. This data can help you identify areas where you might want to invest more to increase your acquisitions, installs, or usage.

The steps in the funnel are:

- **Page views**: This number represents the total devices that viewed your app's product listing page in Store app, web-based Store or external webpage. It includes people who aren't signed in with a Microsoft account.
- **Install attempts**: The number of total devices that attempted to install or purchase your app from Store after viewing your app's product page. This is only available for apps.
- **Acquisitions**: For new installs, you can view the number of devices who obtained a license to your app (when signed in with their Microsoft account) within 48 hours of viewing its Store listing.
- **Installs**: The number of devices who successfully installed the app from Store after an install attempt. This doesn't include installs from Windows Start and App restore.
- **First time launches from Store**: The number of devices who launched the app from Store after installing it. This is only available for apps.

In this chart, _Source_ refers to the method by which a user arrived at your app's listing page. For example, a user can arrive at your app's listing page through the Microsoft Store on Windows, by browsing and searching, through a link from an external website, or by using a link from one of your custom campaigns. For more information about sources, see [source breakdown](#source-breakdown) details in this article.

You can optionally filter the results by Install type (new install or reinstall).

## Page Views

The **Page Views** chart enables you to see how unique devices on Windows 10 and Windows 11 arrived at your app's listing per day over the selected period of time.

A _page view_ means that a customer viewed your app's Store product listing page(Product Display Page) in Store app, Mini-Product Display Page or Store installer (executable) via the web-based Store or external webpages. This also includes ‘Others’ where your app’s product card was viewed in search. You can optionally filter the page views by the type of the page viewed & includes views by users who aren't signed in with a Microsoft account.

In this chart, _Source_ refers to the method in which a user arrived at your app's listing page. For example, a user can arrive at your app's listing page through the Microsoft Store on Windows, by browsing and searching, through a link from an external website, or by using a link from one of your custom campaigns. For more information about sources, see [source breakdown](#source-breakdown) details in this article.

Select the **More details** button to display a table that shows your app's data for the selected time period.

## Installs

The **Installs** chart shows how many devices successfully installed your app on Windows 10 or Windows 11 devices (including Xbox One consoles) over the selected period of time. This includes installations that happen from Store, Windows Start or App restore.

The install total includes:

- **New Installs**: For example, if your app was installed on a device for the first time.
- **Reinstalls**: For example, if a device installs your app today, uninstalls your app tomorrow, and then reinstalls your app next month, that counts as two installs.

The install total doesn't include or reflect:

- **Installs on non-Windows 10 or Windows 11 devices**. If your app supports earlier OS versions such as Windows 8.x or Windows Phone 8.x, we don't count any installs on those devices.

- **Uninstalls**: When a customer uninstalls your app from their device, we don't subtract that from the total number of installs.
- **Updates**: For example, if a customer installs your app today, and then installs an app update a week later, that only counts as one install.
- **Preinstalls**: If a customer buys a device that has your app preinstalled, we don't count that as an install.
- **System-initiated installs**: If Windows installs your app automatically for some reason, we don't count that as an install.

You can optionally filter the installs by a specific package version or [install types](#installs) (only for apps). You can also view installs by different sources (for example, through Microsoft Store on Windows by browsing and searching, a link from an external website, a link from one of your custom campaigns, etc.). For more information about sources, see to [source breakdown](#source-breakdown) details in this article.

## Install success rate
The **Install success rate** chart shows the percentage of unique devices that were able to install your product successfully without any installation errors. This is measured by dividing the number of successful installs by the sum of successful and failed installs on Microsoft Store. For example, if your app has 98 successful installs for the selected period, and 2 install failures, the install success rate will be 98%. This does not include user-initiated actions like cancels, pauses, etc. or network errors. 

## User-initiated aborts
The **User-initiated aborts** chart shows the number of unique devices where users interrupted the purchase or installation of your app by canceling the installation, including closing the sign-in dialog, pausing or suppressing the installation, or canceling the purchase (for paid products). This data can help you understand the drop-off between install attempts and successful installs.

## Install failures
The **Install Failures** chart displays the number of unique devices where the installation of your app was unsuccessful due to genuine installation issues. This excludes scenarios where users interrupted the installation or encountered network-related errors.

## Conversion (Installs by Page Views)

The **Conversion** chart lets you track conversions from Store (including Xbox), which shows how many devices installed your app after viewing the app listing page or Mini-Product Display Page in Microsoft Store(including Xbox) for the selected time period.  

In this chart, Source refers to the method in which a user arrived at your app's listing page. For example, a user can arrive at your app's listing page through the Microsoft Store on Windows, by browsing and searching, through a link from an external website, or by using a link from one of your custom campaigns. For more information about sources, see [source breakdown](#source-breakdown) details in this article.

Select **More details** to see page views and installs from each source.

## Campaign Conversion (Installs by Page views)

The **Campaign conversion** chart lets you track conversions from custom promotion campaigns, by calculating ratio of installs and page views from all of your custom promotion campaigns. To view the conversion from a specific campaign, you can select the campaign from the 'View by Campaign ID' dropdown. Select **More details** to see a tabular summary of page views, installs, and conversion from each custom promotion campaign for the time period that you select.

> [!NOTE]
> Customers could arrive at your app's listing by clicking on a custom campaign not created by you. We stamp every page view within a session with the campaign ID from which the customer first entered the Store. We then attribute conversions to that campaign ID for all acquisitions within 24 hours. Because of this, you may see a higher number of total conversions than the total conversions for your campaign IDs, and you may have conversions or add-on conversions that have zero page views.

## Acquisition

The **Acquisitions** chart shows the number of daily or weekly acquisitions (a new customer obtaining a license for your app) over the selected period of time. (When you use **Apply filters** to show data for a longer duration, the acquisition data is grouped by week.) Only acquisitions made by customers who are signed in with a valid Microsoft account are included in this chart. This chart is available for paid MSIX apps or MSIX games and might not exactly match with Acquisition funnel as funnel shows data at device-level.

## Gross Sales

**Gross sales** for your app are available in this chart, showing the total amount earned from app sales (in USD). This amount doesn't account for any refunds, reversals, chargeback, etc. and is only shown for paid products.

## Geographical spread

The Geographical spread section displays a tabular view of app installs grouped by country or region.

This table shows the following information for each market:
* Country or region where installs occurred
* Installs, representing the total number of installs from that market
* Percentage of total installs, representing the share of overall installs for the selected time range

The table is sortable and paginated, allowing you to browse (or download), search by country, and compare install volume across different markets.
