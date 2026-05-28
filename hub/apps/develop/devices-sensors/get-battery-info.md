---
title: Get battery information
description: Learn how to get a battery report that includes detailed battery information (such as the charge, capacity, and status of a battery or aggregate of batteries), and handle state changes to any items in the report.
ms.date: 05/27/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Get battery information

This topic describes how to get a *battery report* that includes detailed battery information (such as the charge, capacity, and status of a battery or aggregate of batteries), and handle state changes to any items in the report.

Code examples are from the basic battery app that's listed at the end of this topic.

> [!div class="checklist"]
>
> - **Important APIs:** [BatteryReport](/uwp/api/Windows.Devices.Power.BatteryReport), [Windows.Devices.Power](/uwp/api/Windows.Devices.Power), [DeviceInformation.FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync)

## Get aggregate battery report

Some devices have more than one battery and it's not always obvious how each battery contributes to the overall energy capacity of the device. This is where the [AggregateBattery](/uwp/api/windows.devices.power.battery.aggregatebattery) class comes in. The *aggregate battery* represents all battery controllers connected to the device and can provide a single overall [BatteryReport](/uwp/api/Windows.Devices.Power.BatteryReport) object.

> [!NOTE]
> A [Battery](/uwp/api/Windows.Devices.Power.Battery) class actually corresponds to a battery controller. Depending on the device, sometimes the controller is attached to the physical battery and sometimes it's attached to the device enclosure. Thus, it's possible to create a battery object even when no batteries are present. Other times, the battery object may be `null`.

Once you have an aggregate battery object, call [GetReport](/uwp/api/windows.devices.power.battery.getreport) to get the corresponding [BatteryReport](/uwp/api/Windows.Devices.Power.BatteryReport).

```csharp
private void RequestAggregateBatteryReport()
{
    // Create aggregate battery object.
    var aggBattery = Battery.AggregateBattery;

    // Get report.
    var report = aggBattery.GetReport();

    // Update UI.
    AddReportUI(BatteryReportPanel, report, aggBattery.DeviceId);
}
```

## Get individual battery reports

You can also create a [BatteryReport](/uwp/api/Windows.Devices.Power.BatteryReport) object for individual batteries. Use [GetDeviceSelector](/uwp/api/windows.devices.power.battery.getdeviceselector) with the [FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) method to obtain a collection of [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) objects that represent any battery controllers that are connected to the device. Then, using the [Id](/uwp/api/Windows.Devices.Enumeration.DeviceInformation.id) property of the desired `DeviceInformation` object, create a corresponding [Battery](/uwp/api/Windows.Devices.Power.Battery) with the [FromIdAsync](/uwp/api/windows.devices.power.battery.fromidasync) method. Finally, call [GetReport](/uwp/api/windows.devices.power.battery.getreport) to get the individual battery report.

This example shows how to create a battery report for all batteries connected to the device.

```csharp
private async Task RequestIndividualBatteryReports()
{
    // Find batteries.
    DeviceInformationCollection deviceInfo =
        await DeviceInformation.FindAllAsync(Battery.GetDeviceSelector());
    foreach (DeviceInformation device in deviceInfo)
    {
        try
        {
            // Create battery object.
            Battery battery = await Battery.FromIdAsync(device.Id);

            // Get report.
            BatteryReport report = battery.GetReport();

            // Update UI.
            AddReportUI(BatteryReportPanel, report, battery.DeviceId);
        }
        catch { /* Add error handling, as applicable. */ }
    }
}
```

## Access report details

The [BatteryReport](/uwp/api/Windows.Devices.Power.BatteryReport) object provides a lot of battery information. For more info, see the API reference for its properties:

- [Status](/uwp/api/windows.devices.power.batteryreport.status) (a [BatteryStatus](/uwp/api/windows.system.power.batterystatus) enumeration value)
- [ChargeRateInMilliwatts](/uwp/api/windows.devices.power.batteryreport.chargerateinmilliwatts)
- [DesignCapacityInMilliwattHours](/uwp/api/windows.devices.power.batteryreport.designcapacityinmilliwatthours)
- [FullChargeCapacityInMilliwattHours](/uwp/api/windows.devices.power.batteryreport.fullchargecapacityinmilliwatthours)
- [RemainingCapacityInMilliwattHours](/uwp/api/windows.devices.power.batteryreport.remainingcapacityinmilliwatthours). 

This example shows some of the battery report properties used by the basic battery app that's provided later in this topic.

```csharp
TextBlock txt3 = new TextBlock { Text = "Charge rate (mW): " + report.ChargeRateInMilliwatts.ToString() };
TextBlock txt4 = new TextBlock { Text = "Design energy capacity (mWh): " + report.DesignCapacityInMilliwattHours.ToString() };
TextBlock txt5 = new TextBlock { Text = "Fully-charged energy capacity (mWh): " + report.FullChargeCapacityInMilliwattHours.ToString() };
TextBlock txt6 = new TextBlock { Text = "Remaining energy capacity (mWh): " + report.RemainingCapacityInMilliwattHours.ToString() };
```

## Request report updates

The [Battery](/uwp/api/Windows.Devices.Power.Battery) object triggers the [ReportUpdated](/uwp/api/windows.devices.power.battery.reportupdated) event when charge, capacity, or status of the battery changes. This typically happens immediately for status changes and periodically for all other changes. This example shows how to register for battery report updates.

```csharp
...
Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
...
```

## Handle report updates

When a battery update occurs, the [ReportUpdated](/uwp/api/windows.devices.power.battery.reportupdated) event passes the corresponding [Battery](/uwp/api/Windows.Devices.Power.Battery) object to the event handler method. However, this event handler is not called from the UI thread. You'll need to use the [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) object to invoke any UI changes, as shown in this example.

```csharp
private async void AggregateBattery_ReportUpdated(Battery sender, object args)
{
    if (reportRequested)
    {
        DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
        {
            await GetBatteryReport();
        });
    }
}

```

## Sample code: basic battery app

This sample demonstrates how to use the battery APIs to display battery info in the app's user interface (UI). It contains a minimal XAML UI, while the main report UI is created in code-behind.


```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <StackPanel x:Name="topPanel" Margin="24">
        <RadioButtons>
            <RadioButton x:Name="AggregateButton" Content="Aggregate results" IsChecked="True" />
            <RadioButton x:Name="IndividualButton" Content="Individual results"/>
        </RadioButtons>
        <Button Content="Get battery report" Click="GetReportButton_Click"  Margin="0,12,0,0"/>
    </StackPanel>

    <StackPanel x:Name="BatteryReportPanel" Grid.Row="1" Margin="24,0"/>
</Grid>
```

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Devices.Enumeration;
using Windows.Devices.Power;

namespace DevicesDemo.Pages
{
    public sealed partial class BatteryInfoPage : Page
    {
        bool reportRequested = false;

        public BatteryInfoPage()
        {
            InitializeComponent();

            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
        }

        private async void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            if (reportRequested)
            {
                DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
                {
                    await GetBatteryReport();
                });
            }
        }

        private async void GetReportButton_Click(object sender, RoutedEventArgs e)
        {
            await GetBatteryReport();
        }

        private async Task GetBatteryReport()
        {
            // Clear UI.
            BatteryReportPanel.Children.Clear();

            if (AggregateButton.IsChecked == true)
            {
                // Request aggregate battery report.
                RequestAggregateBatteryReport();
            }
            else
            {
                // Request individual battery report.
                await RequestIndividualBatteryReports();
            }

            // Note request.
            reportRequested = true;
        }

        private void RequestAggregateBatteryReport()
        {
            // Create aggregate battery object.
            Battery aggBattery = Battery.AggregateBattery;

            // Get report.
            BatteryReport report = aggBattery.GetReport();

            // Update UI.
            AddReportUI(BatteryReportPanel, report, aggBattery.DeviceId);
        }

        private async Task RequestIndividualBatteryReports()
        {
            // Find batteries.
            DeviceInformationCollection deviceInfo = 
                await DeviceInformation.FindAllAsync(Battery.GetDeviceSelector());
            foreach (DeviceInformation device in deviceInfo)
            {
                try
                {
                    // Create battery object.
                    Battery battery = await Battery.FromIdAsync(device.Id);

                    // Get report.
                    BatteryReport report = battery.GetReport();

                    // Update UI.
                    AddReportUI(BatteryReportPanel, report, battery.DeviceId);
                }
                catch { /* Add error handling, as applicable. */ }
            }
        }

        private void AddReportUI(StackPanel sp, BatteryReport report, string DeviceID)
        {
            // Create battery report UI.
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

            // Create energy capacity progress bar & labels.
            TextBlock pbLabel = new TextBlock { Text = "Percent remaining energy capacity" };
            pbLabel.Margin = new Thickness(0, 10, 0, 5);
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

            // Disable progress bar if values are null.
            if ((report.FullChargeCapacityInMilliwattHours == null) ||
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

            // Add controls to stackpanel.
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
    }
}
```

> [!TIP]
> To receive numeric values from the [BatteryReport](/uwp/api/Windows.Devices.Power.BatteryReport) object, debug your app on the Local Machine or an external Device. When debugging on a device emulator, the BatteryReport object returns null to the capacity and rate properties.
