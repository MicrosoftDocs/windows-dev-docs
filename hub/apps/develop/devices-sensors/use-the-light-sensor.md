---
title: Use the light sensor
description: Learn how to use the ambient light sensor to detect changes in lighting.
ms.date: 05/26/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Use the light sensor

Learn how to use the ambient light sensor to detect changes in lighting.

This example creates a simple app that relies on a the light sensor as an input device. An ambient light sensor is one of the several types of environmental sensors that allow apps to respond to changes in the user's environment.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors), [LightSensor](/uwp/api/Windows.Devices.Sensors.LightSensor)

> [!NOTE]
> This article focuses on code that demonstrates how to use a light sensor. For an overview of the light sensor, see [Sensors: Light sensor](sensors.md#light-sensor).

## Prerequisites

You should be familiar with the light sensor and its uses. See [Sensors: Light sensor](sensors.md#light-sensor).

The device or emulator that you're using must support an ambient light sensor.

## Sample code

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class LightSensorPage : Page
    {
        private LightSensor? lightSensor;

        public LightSensorPage()
        {
            InitializeComponent();

            // Get the default light sensor object.
            lightSensor = LightSensor.GetDefault();

            if (lightSensor != null)
            {
                // Establish the report interval.
                uint minReportInterval = lightSensor.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                lightSensor.ReportInterval = reportInterval;

                // Assign an event handler for the reading-changed event.
                lightSensor.ReadingChanged += LightSensor_ReadingChanged;
            }
            else
            {
                statusBar.Message = "No light sensor was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current light
        // reading to the text block on the XAML page.
        private void LightSensor_ReadingChanged(LightSensor sender, LightSensorReadingChangedEventArgs args)
        {
            DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                LightSensorReading reading = args.Reading;
                txtLuxValue.Text = String.Format("{0,5:0.00}", reading.IlluminanceInLux);
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
        <TextBlock Text="LUX Reading:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtLuxValue" Grid.Column="1" Text="---"/>
    </Grid>

    <InfoBar x:Name="statusBar" Grid.Row="1"/>
</Grid>
```

When the app runs, you can change the light values by covering and uncovering the light sensor.

The previous example demonstrates the essential code you need to write in order to integrate light sensor input into your app.

### Connect to the sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.lightsensor.getdefault) method to establish a connection with the default light sensor.

```csharp
private LightSensor? lightSensor;
// ...
lightSensor = LightSensor.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.lightsensor.fromidasync) to create a [LightSensor](/uwp/api/Windows.Devices.Sensors.LightSensor) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no light sensor sensor is detected, the status message is updated to inform the user.

### Set the report interval

The report interval is set within the page's constructor. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = lightSensor.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
lightSensor.ReportInterval = reportInterval;
```

### Read sensor data

The new light sensor data is captured in the [ReadingChanged](/uwp/api/windows.devices.sensors.lightsensor.readingchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text blocks found in the XAML for the corresponding page.

```csharp
lightSensor.ReadingChanged += LightSensor_ReadingChanged;
// ...

private void LightSensor_ReadingChanged(LightSensor sender, LightSensorReadingChangedEventArgs args)
{
    DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        LightSensorReading reading = args.Reading;
        txtLuxValue.Text = String.Format("{0,5:0.00}", reading.IlluminanceInLux);
    });
}
```
 
 ## Related topics

- [Light sensor sample (UWP)](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/LightSensor)
