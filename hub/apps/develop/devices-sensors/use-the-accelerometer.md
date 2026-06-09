---
title: Use the accelerometer
description: Learn how to create a basic app that relies on a single sensor, the accelerometer, to respond to user movements.
ms.date: 05/26/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Use the accelerometer

Learn how to use the accelerometer to respond to user movement.

This example creates a simple app that relies on a the accelerometer as an input device. You can use the accelerometer to respond when a user moves the device. An app based on an accelerometer typically uses only one or two axes for input.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors), [Accelerometer](/uwp/api/Windows.Devices.Sensors.Accelerometer)

> [!NOTE]
> This article focuses on code that demonstrates how to use an accelerometer. For an overview of the accelerometer sensor, see [Sensors: Accelerometer](sensors.md#accelerometer).

## Prerequisites

You should be familiar with the accelerometer sensor and its uses. See [Sensors: Accelerometer](sensors.md#accelerometer).

The device that you're using must support an accelerometer.

## Sample code

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class AccelerometerPage : Page
    {
        private Accelerometer? accelerometer;

        public AccelerometerPage()
        {
            InitializeComponent();

            // Get the default accelerometer sensor object.
            accelerometer = Accelerometer.GetDefault();

            if (accelerometer != null)
            {
                // Establish the report interval.
                uint minReportInterval = accelerometer.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                accelerometer.ReportInterval = reportInterval;

                // Assign an event handler for the reading-changed event.
                accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }
            else
            {
                statusBar.Message = "No accelerometer was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current accelerometer reading to
        // the three acceleration text blocks on the XAML page.
        private void Accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                AccelerometerReading reading = args.Reading;
                txtXAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
                txtYAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationY);
                txtZAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationZ);
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
        <TextBlock Text="X-axis:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtXAxis" Grid.Column="1" Text="---"/>

        <TextBlock Grid.Row="1" Text="Y-axis:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtYAxis" Grid.Column="1" Grid.Row="1" Text="---"/>

        <TextBlock Grid.Row="2" Text="Z-axis:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtZAxis" Grid.Column="1" Grid.Row="2" Text="---"/>
    </Grid>

    <InfoBar x:Name="statusBar" Grid.Row="1"/>
</Grid>
```
When the app runs, you can change the accelerometer values by moving the device.

The previous example demonstrates the essential code you need to write in order to integrate accelerometer input into your app.

### Connect to the sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.accelerometer.getdefault) method to establish a connection with the default accelerometer.

```csharp
private Accelerometer? accelerometer;
// ...
accelerometer = Accelerometer.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.accelerometer.fromidasync) to create an [Accelerometer](/uwp/api/Windows.Devices.Sensors.Accelerometer) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no accelerometer sensor is detected, the status message is updated to inform the user.

### Set the report interval

The report interval is set within the page's constructor. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = accelerometer.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
accelerometer.ReportInterval = reportInterval;
```

### Read sensor data

The new accelerometer data is captured in the [ReadingChanged](/uwp/api/windows.devices.sensors.accelerometer.readingchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text blocks found in the XAML for the corresponding page.

```csharp
accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
// ...

private void Accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
{
    DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        AccelerometerReading reading = args.Reading;
        txtXAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
        txtYAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationY);
        txtZAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationZ);
    });
}
```

You can also use the [Shaken](/uwp/api/windows.devices.sensors.accelerometer.shaken) event as another input source. Support for the `Shaken` event is dependent upon hardware and driver support. In practice, very few accelerometers support the `Shaken` event.

 ## Related topics

- [Accelerometer sample (UWP)](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/Accelerometer)
