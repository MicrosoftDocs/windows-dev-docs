---
description: Viewing detailed analytics for your MSI or EXE apps in Partner Center.
title: Analyze app performance for your MSI or EXE apps
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, win32
ms.localizationpriority: medium
---

# Analyze app performance for your MSI or EXE apps

You can view detailed analytics for your MSI or EXE apps in [Partner Center](https://partner.microsoft.com/dashboard/apps-and-games/overview). Statistics and charts let you know how your apps are performing in the store, from how many customers you've reached to how they're using your app and what they have to say about it. You can also find metrics on app health, app usage, and more.

You can view analytic reports right in Partner Center or download the reports you need, to analyze your data offline. When viewing your analytic reports, you'll see an arrow icon within each chart for which you can download data. Click the arrow to generate a downloadable .tsv file, which you can open in Microsoft Excel or another program that supports tab-separated values (TSV) files. 

### Overview report 

To view key analytics about your most downloaded MSI or EXE apps, expand Analytics and select Overview. By default, the overview page shows information across a cross section of metrics such as acquisition, Usage, Health and Ratings and reviews. On the overview page, you can view charts on page views, Installs, Launches, Engagement, Crashes and Average ratings. If you wish to go into more details, click on the deep link below each chart on the overview page to visit the detailed chart page where you can apply filters.

### Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is 1 Month (30 days), but you can choose to show data for 3 or 6 months.  

### View individual reports for each app 
In this section you'll find details about the info presented in each of the following reports: 

- [Installs report](#installs-report)
- [Usage report](#usage-report)
- [Health report](#health-report)
- [Ratings & Reviews report](./ratings-reviews-performance.md)

## Installs report

The Installs report in [Partner Center](https://partner.microsoft.com/dashboard/apps-and-games/overview) lets you see who has installed your app. You can view this data in Partner Center or download the report to view offline by clicking on an arrow icon on each chart for which you can download data. Click the arrow to generate a downloadable .tsv file, which you can open in Microsoft Excel or another program that supports tab-separated values (TSV) files.

> [!NOTE]
> In this report, an install refers to the app being installed on a Windows 10 or Windows 11 device. 

### Apply filters 

Near the top of the page, you can select the time period for which you want to show data. The default selection is 1 Month (30 days), but you can choose to show data for 3 or 6 months. 
You can also expand Filters to filter all of the data on this page by market and/or by device type.

- **Device Architecture**: The default setting is All architectures. If you want to show data for Installs from a certain device architecture only (such as x86, x64, arm, arm64, amd and amd64), you can choose a specific one here.

- **Device type**: The default setting is All devices types. If you want to show data for Installs from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

- **OS Version**: The default setting is All OS versions. If you want to show data for Installs from a certain device type only (Windows 10, Windows 11), you can choose a specific one here. 

- **App Version**: The default setting is All App Versions. If you want to show data for Installs from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some charts allow you to apply additional filters.

### Page Views

The Page views chart shows how many times we have detected that customers have visited your app product page in Microsoft Store on Windows 10 or Windows 11 devices over the selected period of time. The total number is shown, along with a chart showing page views by day or week (depending on the duration you've selected). 
 The page views total includes page views on multiple Windows 10 or Windows 11 devices. For example, if the same customer views your app product page in Microsoft Store on two Windows 10 or Windows 11 PCs that counts as two page views.

The page views total does not include or reflect This does not include data from customers who have opted out of providing this information to Microsoft.

### Installs

The Installs chart shows how many times we have detected that customer have successfully installed your app on Windows 10 or Windows 11 devices over the selected period of time. The total number is shown, along with a chart showing installs by day or week (depending on the duration you've selected).

The install total includes:

- **Installs** on multiple Windows 10 or Windows 11 devices. For example, if the same customer installs your app on two Windows 10 or Windows 11 PCs that counts as two installs. 

- **Reinstalls**. For example, if a customer installs your app today, uninstalls your app tomorrow, and then reinstalls your app next month, that counts as two installs. 

The install total does not include or reflect:

- **Uninstalls**. When a customer uninstalls your app from their device, we don’t subtract that from the total number of installs.

- **Updates**. For example, if a customer installs your app today, and then installs an app update a week later, that only counts as one install.

- **Preinstalls**. If a customer buys a device that has your app preinstalled, we don’t count that as an install.

- **System-initiated installs**. If Windows installs your app automatically for some reason, we don’t count that as an install.
  
### Installer return codes

The Installer return codes chart shows how many times we have detected a non-zero return code from the app installer(MSI or EXE) on Windows 10 or Windows 11 devices over the selected period of time. The total number is shown, along with a chart showing Installer return codes by day or week (depending on the duration you've selected).

### Install funnel

The Install funnel shows you how many customers completed each step of the funnel, from viewing the Store page to using the app, along with the conversion rate. This data can help you identify areas where you might want to invest more to increase your acquisitions, installs, or usage.

The steps in the funnel are: 

- **Page views**: This number represents the total views of your app's Store listing, including people who aren't signed in with a Microsoft account. This does not include data from customers who have opted out of providing this information to Microsoft.

- **Installs**: The number of customers who installed the app.
 
- **Launches**: The number of customers who used the app after installing it.


### Launches

The Launches chart shows how many times we have detected that customer have used your app on Windows 10 or Windows 11 devices over the selected period of time. The total number is shown, along with a chart showing launches by day or week (depending on the duration you've selected). Multiple launches in a given day is counted as one launch(depending on the duration you’ve selected).

## Usage report 
The Usage report in Partner Center lets you see how customers on Windows 10 or Windows 11 are using your app. You can view this data in Partner Center or download the report to view offline by clicking on an arrow icon on each chart for which you can download data. Click the arrow to generate a downloadable .tsv file, which you can open in Microsoft Excel or another program that supports tab-separated values (TSV) files.

### Apply filters 
Near the top of the page, you can select the time period for which you want to show data. The default selection is 1 Month (30 days), but you can choose to show data for 3 or 6 months. 
You can also expand Filters to filter all of the data on this page by market and/or by device type. 

- **Device Architecture**: The default setting is All architectures. If you want to show data for Usage from a certain device architecture only (such as x86, x64, arm, arm64, amd and amd64), you can choose a specific one here.

- **Device type**: The default setting is All devices types. If you want to show data for Usage from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

- **OS Version**: The default setting is All OS versions. If you want to show data for Usage from a certain device type only (Windows 10, Windows 11), you can choose a specific one here. 

- **App Version**: The default setting is All App Versions. If you want to show data for Usage from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some charts allow you to apply additional filters.

### Usage

The Usage chart shows details about how your customers are using your app over the selected period of time. Note this chart does not track unique users for your app or unique user sessions (that is, a user is represented in this chart whether they used your app just once or multiple times).

This chart has separate tabs that you can view, showing usage by day or week (depending on the duration you've selected).

- **Active Devices**: Shows the number of daily devices used to interact with your app across all devices.

- **Engagement**: Shows the average engagement minutes per device (average duration of all user sessions).

## Health report

The Health report in Partner Center lets you get data related to the performance and quality of your app, including crashes and unresponsive events. You can view this data in Partner Center, or download the report to view offline. Where applicable, you can view stack traces and/or CAB files for further debugging.

### Apply filters
 
Near the top of the page, you can select the time period for which you want to show data. The default selection is 1 Month (30 days), but you can choose to show data for 3 or 6 months. 
You can also expand Filters to filter all of the data on this page by market and/or by device type.

- **Device Architecture**: The default setting is All architectures. If you want to show data for health report from a certain device architecture only (such as x86, x64, arm, arm64, amd and amd64), you can choose a specific one here.

- **Device type**: The default setting is All devices types. If you want to show data for health report from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

- **OS Version**: The default setting is All OS versions. If you want to show data for health report from a certain device type only (Windows 10, Windows 11), you can choose a specific one here.

- **App Version**: The default setting is All App Versions. If you want to show data for health report from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some sections also allow you to apply additional filters.

### Failure hits

The Failure hits chart shows the number of daily crashes and events that customers experienced when using your app during the selected period of time. Each type of event that your app experienced is tracked separately: crashes, hangs, and memory failures.

### Failures

The Failures chart shows the total number of crashes and events over the selected period of time by failure name. Each failure name is made up of four parts: one or more problem classes, an exception/bug check code, the name of the image/driver where the failure occurred, and the associated function name. By default, we show you the failure that had the most hits on top and continue downward from there. You can reverse this order by toggling the arrow in the Hits column of this chart. For each failure, we also show its percentage of the total number of failures.
