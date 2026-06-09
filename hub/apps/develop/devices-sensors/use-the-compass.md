---
title: Use the compass
description: Learn how to use the Universal Windows Platform (UWP) Compass API to determine the current heading in a navigation app.
ms.date: 05/26/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Use the compass

Learn how to use the compass to determine the current heading.

This example creates a simple app that relies on a the compass as an input device. An app can retrieve the current heading with respect to magnetic, or true, north. Navigation apps use the compass to determine the direction a device is facing and then orient a map accordingly.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors), [Compass](/uwp/api/Windows.Devices.Sensors.Compass)

> [!NOTE]
> This article focuses on code that demonstrates how to use a compass. For an overview of the compass sensor, see [Sensors: Compass](sensors.md#compass).

## Prerequisites

You should be familiar with the compass sensor and its uses. See [Sensors: Compass](sensors.md#compass).

The device or emulator that you're using must support a compass.

## Sample code

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class CompassPage : Page
    {
        private Compass? compass;

        public CompassPage()
        {
            InitializeComponent();

            // Get the default compass object.
            compass = Compass.GetDefault(); 

            if (compass != null)
            {
                // Establish the report interval for all scenarios.
                uint minReportInterval = compass.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                compass.ReportInterval = reportInterval;

                // Assign an event handler for the reading-changed event.
                compass.ReadingChanged += Compass_ReadingChanged;
            }
            else
            {
                statusBar.Message = "No compass was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current compass
        // reading to the text blocks on the XAML page.
        private void Compass_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                CompassReading reading = args.Reading;
                txtMagnetic.Text = String.Format("{0,5:0.00}", reading.HeadingMagneticNorth);
                if (reading.HeadingTrueNorth.HasValue)
                    txtNorth.Text = String.Format("{0,5:0.00}", reading.HeadingTrueNorth);
                else
                    txtNorth.Text = "No reading.";
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
        </Grid.RowDefinitions>
        <TextBlock Text="Magnetic Heading:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtMagnetic" Grid.Column="1" Text="---"/>

        <TextBlock Grid.Row="1" Text="True North Heading:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtNorth" Grid.Column="1" Grid.Row="1" Text="---"/>
    </Grid>

    <InfoBar x:Name="statusBar" Grid.Row="1"/>
</Grid>
```

When the app runs, you can change the compass values by moving the device.

The previous example demonstrates the essential code you need to write in order to integrate compass input into your app.

### Connect to the sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.compass.getdefault) method to establish a connection with the default compass.

```csharp
private Compass? compass;
// ...
compass = Compass.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.compass.fromidasync) to create a [Compass](/uwp/api/Windows.Devices.Sensors.Compass) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no compass sensor is detected, the status message is updated to inform the user.

### Set the report interval

The report interval is set within the page's constructor. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = compass.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
compass.ReportInterval = reportInterval;
```

### Read sensor data

The new compass data is captured in the [ReadingChanged](/uwp/api/windows.devices.sensors.compass.readingchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text blocks found in the XAML for the corresponding page.

```csharp
compass.ReadingChanged += Compass_ReadingChanged;
// ...

private void Compass_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
{
    DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        CompassReading reading = args.Reading;
        txtMagnetic.Text = String.Format("{0,5:0.00}", reading.HeadingMagneticNorth);
        if (reading.HeadingTrueNorth.HasValue)
            txtNorth.Text = String.Format("{0,5:0.00}", reading.HeadingTrueNorth);
        else
            txtNorth.Text = "No reading.";
    });
}
```
 
 ## Related topics

- [Compass sample (UWP)](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/Compass)
