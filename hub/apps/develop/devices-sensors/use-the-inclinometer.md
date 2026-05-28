---
title: Use the inclinometer
description: Learn how to create a basic app that uses the inclinometer input device to determine pitch, roll, and yaw.
ms.date: 05/26/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Use the inclinometer

Learn how to use the inclinometer to determine pitch, roll, and yaw.

This example creates a simple app that relies on a the inclinometer as an input device. Some 3-D games require an inclinometer as an input device. One common example is a flight simulator, which could map the three axes of the inclinometer (X, Y, and Z) to the elevator, aileron, and rudder inputs of the aircraft.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors), [Inclinometer](/uwp/api/Windows.Devices.Sensors.Inclinometer)

> [!NOTE]
> This article focuses on code that demonstrates how to use an inclinometer. For an overview of the inclinometer sensor, see [Sensors: Inclinometer](sensors.md#inclinometer).

## Prerequisites

You should be familiar with the inclinometer sensor and its uses. See [Sensors: Inclinometer](sensors.md#inclinometer).

The device or emulator that you're using must support an inclinometer.

## Sample code

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class InclinometerPage : Page
    {
        private Inclinometer? inclinometer;

        public InclinometerPage()
        {
            InitializeComponent();

            // Get the default inclinometer sensor object.
            inclinometer = Inclinometer.GetDefault();

            if (inclinometer != null)
            {
                // Establish the report interval.
                uint minReportInterval = inclinometer.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                inclinometer.ReportInterval = reportInterval;

                // Assign an event handler for the reading-changed event.
                inclinometer.ReadingChanged += Inclinometer_ReadingChanged;
            }
            else
            {
                statusBar.Message = "No inclinometer was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current inclinometer
        // reading to the text blocks on the XAML page.
        private void Inclinometer_ReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                InclinometerReading reading = args.Reading;
                txtPitch.Text = String.Format("{0,5:0.00}", reading.PitchDegrees);
                txtRoll.Text = String.Format("{0,5:0.00}", reading.RollDegrees);
                txtYaw.Text = String.Format("{0,5:0.00}", reading.YawDegrees);
            });
        }
    }
}
```

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition />
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    <Grid Margin="24">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
        </Grid.RowDefinitions>
        <TextBlock Text="Pitch:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtPitch" Grid.Column="1" Text="---"/>

        <TextBlock Grid.Row="1" Text="Roll:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtRoll" Grid.Column="1" Grid.Row="1" Text="---"/>

        <TextBlock Grid.Row="2" Text="Yaw:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtYaw" Grid.Column="1" Grid.Row="2" Text="---"/>
    </Grid>

    <InfoBar x:Name="statusBar" Grid.Row="1"/>
</Grid>
```

When the app runs, you can change the inclinometer values by moving the device.

The previous example demonstrates the essential code you need to write in order to integrate inclinometer input into your app.

### Connect to the sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.inclinometer.getdefault) method to establish a connection with the default inclinometer.

```csharp
private Inclinometer? inclinometer;
// ...
inclinometer = Inclinometer.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.inclinometer.fromidasync) to create an [Inclinometer](/uwp/api/Windows.Devices.Sensors.Inclinometer) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no inclinometer sensor is detected, the status message is updated to inform the user.

### Set the report interval

The report interval is set within the page's constructor. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = inclinometer.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
inclinometer.ReportInterval = reportInterval;
```

### Read sensor data

The new inclinometer data is captured in the [ReadingChanged](/uwp/api/windows.devices.sensors.inclinometer.readingchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text blocks found in the XAML for the corresponding page.

```csharp
inclinometer.ReadingChanged += Inclinometer_ReadingChanged;
// ...

private void Inclinometer_ReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
{
    DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        InclinometerReading reading = args.Reading;
        txtPitch.Text = String.Format("{0,5:0.00}", reading.PitchDegrees);
        txtRoll.Text = String.Format("{0,5:0.00}", reading.RollDegrees);
        txtYaw.Text = String.Format("{0,5:0.00}", reading.YawDegrees);
    });
}
```
 
 ## Related topics

- [Inclinometer sample (UWP)](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/Inclinometer)
