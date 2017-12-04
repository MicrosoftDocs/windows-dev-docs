---
author: muhsinking
ms.assetid: 90BB59FC-90FE-453E-A8DE-9315E29EB98C
title: Get battery information
description: Learn how to get detailed battery information using APIs in the Windows.Devices.Power namespace.
ms.author: mukin
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Get battery information


** Important APIs **

-   [**Windows.Devices.Power**](https://msdn.microsoft.com/library/windows/apps/Dn895017)
-   [**DeviceInformation.FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/BR225432)

Learn how to get detailed battery information using APIs in the [**Windows.Devices.Power**](https://msdn.microsoft.com/library/windows/apps/Dn895017) namespace. A *battery report* ([**BatteryReport**](https://msdn.microsoft.com/library/windows/apps/Dn895005)) describes the charge, capacity, and status of a battery or aggregate of batteries. This topic demonstrates how your app can get battery reports and be notified of changes. Code examples are from the basic battery app that's listed at the end of this topic.

## Get aggregate battery report


Some devices have more than one battery and it's not always obvious how each battery contributes to the overall energy capacity of the device. This is where the [**AggregateBattery**](https://msdn.microsoft.com/library/windows/apps/Dn895011) class comes in. The *aggregate battery* represents all battery controllers connected to the device and can provide a single overall [**BatteryReport**](https://msdn.microsoft.com/library/windows/apps/Dn895005) object.

**Note**  A [**Battery**](https://msdn.microsoft.com/library/windows/apps/Dn895004) class actually corresponds to a battery controller. Depending on the device, sometimes the controller is attached to the physical battery and sometimes it's attached to the device enclosure. Thus, it's possible to create a battery object even when no batteries are present. Other times, the battery object may be **null**.

Once you have an aggregate battery object, call [**GetReport**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.battery.getreport) to get the corresponding [**BatteryReport**](https://msdn.microsoft.com/library/windows/apps/Dn895005).

```csharp
private void RequestAggregateBatteryReport()
{
    // Create aggregate battery object
    var aggBattery = Battery.AggregateBattery;

    // Get report
    var report = aggBattery.GetReport();

    // Update UI
    AddReportUI(BatteryReportPanel, report, aggBattery.DeviceId);
}
```

## Get individual battery reports

You can also create a [**BatteryReport**](https://msdn.microsoft.com/library/windows/apps/Dn895005) object for individual batteries. Use [**GetDeviceSelector**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.battery.getdeviceselector.aspx) with the [**FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/BR225432) method to obtain a collection of [**DeviceInformation**](https://msdn.microsoft.com/library/windows/apps/BR225393) objects that represent any battery controllers that are connected to the device. Then, using the **Id** property of the desired **DeviceInformation** object, create a corresponding [**Battery**](https://msdn.microsoft.com/library/windows/apps/Dn895004) with the [**FromIdAsync**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.battery.fromidasync.aspx) method. Finally, call [**GetReport**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.battery.getreport) to get the individual battery report.

This example shows how to create a battery report for all batteries connected to the device.

```csharp
async private void RequestIndividualBatteryReports()
{
    // Find batteries 
    var deviceInfo = await DeviceInformation.FindAllAsync(Battery.GetDeviceSelector());
    foreach(DeviceInformation device in deviceInfo)
    {
        try
        {
        // Create battery object
        var battery = await Battery.FromIdAsync(device.Id);

        // Get report
        var report = battery.GetReport();

        // Update UI
        AddReportUI(BatteryReportPanel, report, battery.DeviceId);
        }
        catch { /* Add error handling, as applicable */ }
    }
}
```

## Access report details

The [**BatteryReport**](https://msdn.microsoft.com/library/windows/apps/Dn895005) object provides a lot of battery information. For more info, see the API reference for its properties: **Status** (a [**BatteryStatus**](https://msdn.microsoft.com/library/windows/apps/Dn818458) enumeration), [**ChargeRateInMilliwatts**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.batteryreport.chargerateinmilliwatts.aspx), [**DesignCapacityInMilliwattHours**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.batteryreport.designcapacityinmilliwatthours.aspx), [**FullChargeCapacityInMilliwattHours**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.batteryreport.fullchargecapacityinmilliwatthours.aspx), and [**RemainingCapacityInMilliwattHours**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.batteryreport.remainingcapacityinmilliwatthours). This example shows some of the battery report properties used by the basic battery app, that's provided later in this topic.

```csharp
...
TextBlock txt3 = new TextBlock { Text = "Charge rate (mW): " + report.ChargeRateInMilliwatts.ToString() };
TextBlock txt4 = new TextBlock { Text = "Design energy capacity (mWh): " + report.DesignCapacityInMilliwattHours.ToString() };
TextBlock txt5 = new TextBlock { Text = "Fully-charged energy capacity (mWh): " + report.FullChargeCapacityInMilliwattHours.ToString() };
TextBlock txt6 = new TextBlock { Text = "Remaining energy capacity (mWh): " + report.RemainingCapacityInMilliwattHours.ToString() };
...
...
```

## Request report updates

The [**Battery**](https://msdn.microsoft.com/library/windows/apps/Dn895004) object triggers the [**ReportUpdated**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.battery.reportupdated) event when charge, capacity, or status of the battery changes. This typically happens immediately for status changes and periodically for all other changes. This example shows how to register for battery report updates.

```csharp
...
Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
...
```

## Handle report updates

When a battery update occurs, the [**ReportUpdated**](https://msdn.microsoft.com/library/windows/apps/windows.devices.power.battery.reportupdated) event passes the corresponding [**Battery**](https://msdn.microsoft.com/library/windows/apps/Dn895004) object to the event handler method. However, this event handler is not called from the UI thread. You'll need to use the [**Dispatcher**](https://msdn.microsoft.com/library/windows/apps/BR208211) object to invoke any UI changes, as shown in this example.

```csharp
async private void AggregateBattery_ReportUpdated(Battery sender, object args)
{
    if (reportRequested)
    {

        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            // Clear UI
            BatteryReportPanel.Children.Clear();


            if (AggregateButton.IsChecked == true)
            {
                // Request aggregate battery report
                RequestAggregateBatteryReport();
            }
            else
            {
                // Request individual battery report
                RequestIndividualBatteryReports();
            }
        });
    }
}
```

## Example: basic battery app

Test out these APIs by building the following basic battery app in Microsoft Visual Studio. From the Visual Studio start page, click **New Project**, and then under the **Visual C# &gt; Windows &gt; Universal** templates, create a new app using the **Blank App** template.

Next, open the file **MainPage.xaml** and copy the following XML into this file (replacing its original contents).

```xml
<Page
    x:Class="App1.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:App1"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <StackPanel Background="{ThemeResource ApplicationPageBackgroundThemeBrush}" >
        <StackPanel VerticalAlignment="Center" Margin="15,30,0,0" >
            <RadioButton x:Name="AggregateButton" Content="Aggregate results" GroupName="Type" IsChecked="True" />
            <RadioButton x:Name="IndividualButton" Content="Individual results" GroupName="Type" IsChecked="False" />
        </StackPanel>
        <StackPanel Orientation="Horizontal">
        <Button x:Name="GetBatteryReportButton" 
                Content="Get battery report" 
                Margin="15,15,0,0" 
                Click="GetBatteryReport"/>
        </StackPanel>
        <StackPanel x:Name="BatteryReportPanel" Margin="15,15,0,0"/>
    </StackPanel>
</Page>
```

If your app isn't named **App1**, you'll need to replace the first part of the class name in the previous snippet with the namespace of your app. For example, if you created a project named **BasicBatteryApp**, you'd replace `x:Class="App1.MainPage"` with `x:Class="BasicBatteryApp.MainPage"`. You should also replace `xmlns:local="using:App1"` with `xmlns:local="using:BasicBatteryApp"`.

Next, open your project's **MainPage.xaml.cs** file and replace the existing code with the following.

```csharp
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Devices.Enumeration;
using Windows.Devices.Power;
using Windows.UI.Core;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        bool reportRequested = false;
        public MainPage()
        {
            this.InitializeComponent();
            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
        }


        private void GetBatteryReport(object sender, RoutedEventArgs e)
        {
            // Clear UI
            BatteryReportPanel.Children.Clear();


            if (AggregateButton.IsChecked == true)
            {
                // Request aggregate battery report
                RequestAggregateBatteryReport();
            }
            else
            {
                // Request individual battery report
                RequestIndividualBatteryReports();
            }

            // Note request
            reportRequested = true;
        }

        private void RequestAggregateBatteryReport()
        {
            // Create aggregate battery object
            var aggBattery = Battery.AggregateBattery;

            // Get report
            var report = aggBattery.GetReport();

            // Update UI
            AddReportUI(BatteryReportPanel, report, aggBattery.DeviceId);
        }

        async private void RequestIndividualBatteryReports()
        {
            // Find batteries 
            var deviceInfo = await DeviceInformation.FindAllAsync(Battery.GetDeviceSelector());
            foreach(DeviceInformation device in deviceInfo)
            {
                try
                {
                // Create battery object
                var battery = await Battery.FromIdAsync(device.Id);

                // Get report
                var report = battery.GetReport();

                // Update UI
                AddReportUI(BatteryReportPanel, report, battery.DeviceId);
                }
                catch { /* Add error handling, as applicable */ }
            }
        }


        private void AddReportUI(StackPanel sp, BatteryReport report, string DeviceID)
        {
            // Create battery report UI
            TextBlock txt1 = new TextBlock { Text = "Device ID: " + DeviceID };
            txt1.FontSize = 15;
            txt1.Margin = new Thickness(0, 15, 0, 0);
            txt1.TextWrapping = TextWrapping.WrapWholeWords;

            TextBlock txt2 = new TextBlock { Text = "Battery status: " + report.Status.ToString() };
            txt2.FontStyle = Windows.UI.Text.FontStyle.Italic;
            txt2.Margin = new Thickness(0, 0, 0, 15);

            TextBlock txt3 = new TextBlock { Text = "Charge rate (mW): " + report.ChargeRateInMilliwatts.ToString() };
            TextBlock txt4 = new TextBlock { Text = "Design energy capacity (mWh): " + report.DesignCapacityInMilliwattHours.ToString() };
            TextBlock txt5 = new TextBlock { Text = "Fully-charged energy capacity (mWh): " + report.FullChargeCapacityInMilliwattHours.ToString() };
            TextBlock txt6 = new TextBlock { Text = "Remaining energy capacity (mWh): " + report.RemainingCapacityInMilliwattHours.ToString() };

            // Create energy capacity progress bar & labels
            TextBlock pbLabel = new TextBlock { Text = "Percent remaining energy capacity" };
            pbLabel.Margin = new Thickness(0,10, 0, 5);
            pbLabel.FontFamily = new FontFamily("Segoe UI");
            pbLabel.FontSize = 11;

            ProgressBar pb = new ProgressBar();
            pb.Margin = new Thickness(0, 5, 0, 0);
            pb.Width = 200;
            pb.Height = 10;
            pb.IsIndeterminate = false;
            pb.HorizontalAlignment = HorizontalAlignment.Left;

            TextBlock pbPercent = new TextBlock();
            pbPercent.Margin = new Thickness(0, 5, 0, 10);
            pbPercent.FontFamily = new FontFamily("Segoe UI");
            pbLabel.FontSize = 11;

            // Disable progress bar if values are null
            if ((report.FullChargeCapacityInMilliwattHours == null)||
                (report.RemainingCapacityInMilliwattHours == null))
            {
                pb.IsEnabled = false;
                pbPercent.Text = "N/A";
            }
            else
            {
                pb.IsEnabled = true;
                pb.Maximum = Convert.ToDouble(report.FullChargeCapacityInMilliwattHours);
                pb.Value = Convert.ToDouble(report.RemainingCapacityInMilliwattHours);
                pbPercent.Text = ((pb.Value / pb.Maximum) * 100).ToString("F2") + "%";
            }

            // Add controls to stackpanel
            sp.Children.Add(txt1);
            sp.Children.Add(txt2);
            sp.Children.Add(txt3);
            sp.Children.Add(txt4);
            sp.Children.Add(txt5);
            sp.Children.Add(txt6);
            sp.Children.Add(pbLabel);
            sp.Children.Add(pb);
            sp.Children.Add(pbPercent);
        }

        async private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            if (reportRequested)
            {

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    // Clear UI
                    BatteryReportPanel.Children.Clear();


                    if (AggregateButton.IsChecked == true)
                    {
                        // Request aggregate battery report
                        RequestAggregateBatteryReport();
                    }
                    else
                    {
                        // Request individual battery report
                        RequestIndividualBatteryReports();
                    }
                });
            }
        }
    }
}
```

If your app isn't named **App1**, you'll need to rename the namespace in the previous example with the name you gave your project. For example, if you created a project named **BasicBatteryApp**, you'd replace namespace `App1` with namespace `BasicBatteryApp`.

Finally, to run this basic battery app: on the **Debug** menu, click **Start Debugging** to test the solution.

**Tip**  To receive numeric values from the [**BatteryReport**](https://msdn.microsoft.com/library/windows/apps/Dn895005) object, debug your app on the **Local Machine** or an external **Device** (such as a Windows Phone). When debugging on a device emulator, the **BatteryReport** object returns **null** to the capacity and rate properties.

 

