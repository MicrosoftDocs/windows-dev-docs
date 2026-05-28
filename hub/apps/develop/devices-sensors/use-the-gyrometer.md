---
title: Use the gyrometer
description: Learn how to use the Gyrometer API to integrate gyrometer input in your app that detects changes in user movement like angular velocity and rotational motion.
ms.date: 05/26/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Use the gyrometer

Learn how to use the gyrometer to detect changes in user movement.

Gyrometers compliment accelerometers as game controllers. The accelerometer can measure linear motion while the gyrometer measures angular velocity or rotational motion.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors), [Gyrometer](/uwp/api/Windows.Devices.Sensors.Gyrometer)

> [!NOTE]
> This article focuses on code that demonstrates how to use a gyrometer. For an overview of the gyrometer sensor, see [Sensors: Gyrometer](sensors.md#gyrometer).

## Prerequisites

You should be familiar with the gyrometer sensor and its uses. See [Sensors: Gyrometer](sensors.md#gyrometer).

The device that you're using must support a gyrometer.

## Sample code

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class GyrometerPage : Page
    {
        private Gyrometer? gyrometer;

        public GyrometerPage()
        {
            InitializeComponent();

            // Get the default gyrometer sensor object.
            gyrometer = Gyrometer.GetDefault();

            if (gyrometer != null)
            {
                // Establish the report interval.
                uint minReportInterval = gyrometer.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                gyrometer.ReportInterval = reportInterval;

                // Assign an event handler for the reading-changed event.
                gyrometer.ReadingChanged += Gyrometer_ReadingChanged;
            }
            else
            {
                statusBar.Message = "No gyrometer was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current gyrometer reading to
        // the three axis text blocks on the XAML page.
        private void Gyrometer_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                GyrometerReading reading = args.Reading;
                txtXAxis.Text = String.Format("{0,5:0.00}", reading.AngularVelocityX);
                txtYAxis.Text = String.Format("{0,5:0.00}", reading.AngularVelocityY);
                txtZAxis.Text = String.Format("{0,5:0.00}", reading.AngularVelocityZ);
            });
        }
    }
}
```

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition/>
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

When the app runs, you can change the gyrometer values by moving the device.

The previous example demonstrates the essential code you need to write in order to integrate gyrometer input into your app.

### Connect to the sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.gyrometer.getdefault) method to establish a connection with the default gyrometer.

```csharp
private Gyrometer? gyrometer;
// ...
gyrometer = Gyrometer.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.gyrometer.fromidasync) to create a [Gyrometer](/uwp/api/Windows.Devices.Sensors.gyrometer) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no gyrometer sensor is detected, the status message is updated to inform the user.

### Set the report interval

The app establishes the report interval within the MainPage method. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = gyrometer.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
gyrometer.ReportInterval = reportInterval;
```

### Read sensor data

The new gyrometer data is captured in the [ReadingChanged](/uwp/api/windows.devices.sensors.gyrometer.readingchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text blocks found in the XAML for the corresponding page.

```csharp
gyrometer.ReadingChanged += Gyrometer_ReadingChanged;
// ...

private void Gyrometer_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
{
    DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        GyrometerReading reading = args.Reading;
        txtXAxis.Text = String.Format("{0,5:0.00}", reading.AngularVelocityX);
        txtYAxis.Text = String.Format("{0,5:0.00}", reading.AngularVelocityY);
        txtZAxis.Text = String.Format("{0,5:0.00}", reading.AngularVelocityZ);
    });
}
```

 ## Related topics

- [Gyrometer sample (UWP)](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/Gyrometer)
