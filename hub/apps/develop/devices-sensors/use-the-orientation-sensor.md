---
title: Use the orientation sensor
description: Learn how to use the orientation sensors to determine the device orientation.
ms.date: 05/26/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Use the orientation sensors

Learn how to use the orientation sensors to determine the device orientation.

This example creates a simple app that relies on an orientation sensor as an input device. An orientation sensor is one of the several types of environmental sensors that allow apps to respond to changes in the device orientation.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors), [OrientationSensor](/uwp/api/Windows.Devices.Sensors.OrientationSensor), [SimpleOrientationSensor](/uwp/api/Windows.Devices.Sensors.SimpleOrientationSensor)

> [!NOTE]
> This article focuses on code that demonstrates how to use an orientation sensor. For an overview of the orientation sensors, see [Sensors: Orientation sensor](sensors.md#orientation-sensor).

## Prerequisites

You should be familiar with the orientation sensor and its uses. See [Sensors: Orientation sensor](sensors.md#orientation-sensor).

The device that you're using must support an orientation sensor.

## Orientation sensor types

There are two different types of orientation sensor APIs included in the [Windows.Devices.Sensors](/uwp/api/Windows.Devices.Sensors) namespace: [OrientationSensor](/uwp/api/Windows.Devices.Sensors.OrientationSensor) and [SimpleOrientation](/uwp/api/Windows.Devices.Sensors.SimpleOrientation). While both of these sensors are orientation sensors, that term is overloaded and they are used for very different purposes. However, since both are orientation sensors, they are both covered in this article.

The [OrientationSensor](/uwp/api/Windows.Devices.Sensors.OrientationSensor) API is used for 3-D apps two obtain a quaternion and a rotation matrix. A quaternion can be most easily understood as a rotation of a point \[x,y,z\] about an arbitrary axis (contrasted with a rotation matrix, which represents rotations around three axes). The mathematics behind quaternions is fairly exotic in that it involves the geometric properties of complex numbers and mathematical properties of imaginary numbers, but working with them is simple, and frameworks like DirectX support them. A complex 3-D app can use the Orientation sensor to adjust the user's perspective. This sensor combines input from the accelerometer, gyrometer, and compass.

The [SimpleOrientationSensor](/uwp/api/Windows.Devices.Sensors.SimpleOrientationSensor) API is used to determine the current physical orientation of the device in terms of definitions like portrait up, portrait down, landscape left, and landscape right. It can also detect if a device is face-up or face-down. Rather than returning properties like "portrait up" or "landscape left", this sensor returns a rotation value: "Not rotated", "Rotated90DegreesCounterclockwise", and so on. The following table maps common orientation properties to the corresponding sensor reading.

| Orientation     | Corresponding sensor reading      |
|-----------------|-----------------------------------|
| Portrait Up     | NotRotated                        |
| Landscape Left  | Rotated90DegreesCounterclockwise  |
| Portrait Down   | Rotated180DegreesCounterclockwise |
| Landscape Right | Rotated270DegreesCounterclockwise |

## Sample code - orientation sensor

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class OrientationSensorPage : Page
    {
        private OrientationSensor? orientationSensor;

        public OrientationSensorPage()
        {
            InitializeComponent();

            // Get the default orientation sensor object.
            orientationSensor = OrientationSensor.GetDefault();

            if (orientationSensor != null)
            {
                // Establish the report interval.
                uint minReportInterval = orientationSensor.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                orientationSensor.ReportInterval = reportInterval;

                // Assign an event handler for the reading-changed event.
                orientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
            }
            else
            {
                statusBar.Message = "No orientation sensor was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current orientation
        // reading to the text blocks on the XAML page.
        private void OrientationSensor_ReadingChanged(OrientationSensor sender, OrientationSensorReadingChangedEventArgs args)
        {
            DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                OrientationSensorReading reading = args.Reading;
                // Quaternion values
                txtQuaternionX.Text = String.Format("{0,8:0.00000}", reading.Quaternion.X);
                txtQuaternionY.Text = String.Format("{0,8:0.00000}", reading.Quaternion.Y);
                txtQuaternionZ.Text = String.Format("{0,8:0.00000}", reading.Quaternion.Z);
                txtQuaternionW.Text = String.Format("{0,8:0.00000}", reading.Quaternion.W);

                // Rotation Matrix values
                txtM11.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M11);
                txtM12.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M12);
                txtM13.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M13);
                txtM21.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M21);
                txtM22.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M22);
                txtM23.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M23);
                txtM31.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M31);
                txtM32.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M32);
                txtM33.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M33);
            });
        }
    }
}
```

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    <Grid Margin="24">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="Auto" MinWidth="66"/>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="Auto" MinWidth="66"/>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="Auto" MinWidth="66"/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
        </Grid.RowDefinitions>
        <TextBlock Text="M11:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM11" Grid.Column="1" Text="---"/>
        <TextBlock Text="M12:" Grid.Row="1" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM12" Grid.Column="1" Grid.Row="1" Text="---"/>
        <TextBlock Text="M13:" Grid.Row="2" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM13" Grid.Column="1" Grid.Row="2" Text="---"/>

        <TextBlock Text="M21:" Grid.Column="2" Grid.Row="0" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM21" Grid.Column="3" Grid.Row="0" Text="---"/>
        <TextBlock Text="M22:" Grid.Column="2" Grid.Row="1" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM22" Grid.Column="3" Grid.Row="1" Text="---"/>
        <TextBlock Text="M23:" Grid.Column="2" Grid.Row="2" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM23" Grid.Column="3" Grid.Row="2" Text="---"/>

        <TextBlock Text="M31:" Grid.Column="4" Grid.Row="0" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM31" Grid.Column="5" Grid.Row="0" Text="---"/>
        <TextBlock Text="M32:" Grid.Column="4" Grid.Row="1" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM32" Grid.Column="5" Grid.Row="1" Text="---"/>
        <TextBlock Text="M33:" Grid.Column="4" Grid.Row="2" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtM33" Grid.Column="5" Grid.Row="2" Text="---"/>

    </Grid>
    <Grid Margin="24" Grid.Row="1">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="Auto"/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
            <RowDefinition Height="44"/>
        </Grid.RowDefinitions>

        <TextBlock Text="Quaternion X:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtQuaternionX" Grid.Column="1" Grid.Row="0" Text="---"/>
        <TextBlock Text="Quaternion Y:" Grid.Row="1" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtQuaternionY" Grid.Column="1" Grid.Row="1" Text="---"/>
        <TextBlock Text="Quaternion Z:" Grid.Row="2" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtQuaternionZ" Grid.Column="1" Grid.Row="2" Text="---"/>
        <TextBlock Text="Quaternion W:" Grid.Row="3" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtQuaternionW" Grid.Column="1" Grid.Row="3" Text="---"/>
    </Grid>

    <InfoBar x:Name="statusBar" Grid.Row="2"/>
</Grid>
```

When the app runs, you can change the orientation values by moving the device.

The previous example demonstrates the essential code you need to write in order to integrate orientation sensor input into your app.

### Connect to the sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.orientationsensor.getdefault) method to establish a connection with the default orientation sensor.

```csharp
private OrientationSensor? orientationSensor;
// ...
orientationSensor = OrientationSensor.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.orientationsensor.fromidasync) to create an[OrientationSensor](/uwp/api/Windows.Devices.Sensors.orientationsensor) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no orientation sensor sensor is detected, the status message is updated to inform the user.

### Set the report interval

The report interval is set within the page's constructor. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = orientationSensor.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
orientationSensor.ReportInterval = reportInterval;
```

### Read sensor data

The new orientation sensor data is captured in the [ReadingChanged](/uwp/api/windows.devices.sensors.orientationsensor.readingchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text blocks found in the XAML for the corresponding page.

```csharp
orientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
// ...

private void OrientationSensor_ReadingChanged(OrientationSensor sender, OrientationSensorReadingChangedEventArgs args)
{
    DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        OrientationSensorReading reading = args.Reading;
        // Quaternion values
        txtQuaternionX.Text = String.Format("{0,8:0.00000}", reading.Quaternion.X);
        txtQuaternionY.Text = String.Format("{0,8:0.00000}", reading.Quaternion.Y);
        txtQuaternionZ.Text = String.Format("{0,8:0.00000}", reading.Quaternion.Z);
        txtQuaternionW.Text = String.Format("{0,8:0.00000}", reading.Quaternion.W);

        // Rotation Matrix values
        txtM11.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M11);
        txtM12.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M12);
        txtM13.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M13);
        txtM21.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M21);
        txtM22.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M22);
        txtM23.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M23);
        txtM31.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M31);
        txtM32.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M32);
        txtM33.Text = String.Format("{0,8:0.00000}", reading.RotationMatrix.M33);
    });
}
```
## Sample code - simple orientation sensor

```csharp
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Sensors;

namespace DevicesDemo.Pages
{
    public sealed partial class SimpleOrientationPage : Page
    {
        private SimpleOrientationSensor? simpleOrientationSensor;

        public SimpleOrientationPage()
        {
            InitializeComponent();

            // Get the default simple orientation sensor object.
            simpleOrientationSensor = SimpleOrientationSensor.GetDefault();

            // Assign an event handler.
            if (simpleOrientationSensor != null)
            {
                // Assign an event handler for the reading-changed event.
                simpleOrientationSensor.OrientationChanged 
                    += SimpleOrientationSensor_OrientationChanged;
            }
            else
            {
                statusBar.Message = "No simple orientation sensor was found.";
                statusBar.Severity = InfoBarSeverity.Error;
                statusBar.IsOpen = true;
            }
        }

        // This event handler writes the current simple orientation
        // reading to the text block on the XAML page.
        private void SimpleOrientationSensor_OrientationChanged(SimpleOrientationSensor sender, 
            SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                switch (args.Orientation)
                {
                    case SimpleOrientation.NotRotated:
                        txtOrientation.Text = "Not Rotated";
                        break;
                    case SimpleOrientation.Rotated90DegreesCounterclockwise:
                        txtOrientation.Text = "Rotated 90 Degrees Counterclockwise";
                        break;
                    case SimpleOrientation.Rotated180DegreesCounterclockwise:
                        txtOrientation.Text = "Rotated 180 Degrees Counterclockwise";
                        break;
                    case SimpleOrientation.Rotated270DegreesCounterclockwise:
                        txtOrientation.Text = "Rotated 270 Degrees Counterclockwise";
                        break;
                    case SimpleOrientation.Faceup:
                        txtOrientation.Text = "Faceup";
                        break;
                    case SimpleOrientation.Facedown:
                        txtOrientation.Text = "Facedown";
                        break;
                    default:
                        txtOrientation.Text = "Unknown orientation";
                        break;
                }
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
        </Grid.RowDefinitions>
        <TextBlock Text="Orientation:" Style="{StaticResource LabelTextBlockStyle}"/>
        <TextBlock x:Name="txtOrientation" Grid.Column="1" Text="---"/>
    </Grid>

    <InfoBar x:Name="statusBar" Grid.Row="1"/>
</Grid>
```

When the app runs, you can change the orientation values by moving the device.

The previous example demonstrates the essential code you need to write in order to integrate simple-orientation sensor input into your app.

### Connect to the simple orientation sensor

Call the [GetDefault](/uwp/api/windows.devices.sensors.simpleorientationsensor.getdefault) method to establish a connection with the default orientation sensor.

```csharp
private SimpleOrientationSensor? simpleOrientationSensor;
// ...
simpleOrientationSensor = SimpleOrientationSensor.GetDefault();
```

You can also call [FromIdAsync](/uwp/api/windows.devices.sensors.simpleorientationsensor.fromidasync) to create a[SimpleOrientationSensor](/uwp/api/Windows.Devices.Sensors.simpleorientationsensor) object from a [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) value. For more info, see [Enumerate devices](enumerate-devices.md).

If no simple orientation sensor sensor is detected, the status message is updated to inform the user.

### Read the simple orientation sensor data

The new simple orientation sensor data is captured in the [OrientationChanged](/uwp/api/windows.devices.sensors.simpleorientationsensor.orientationchanged) event handler. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event. For this example, these new values are written to the text block found in the XAML for the corresponding page.

```csharp
simpleOrientationSensor.OrientationChanged 
    += SimpleOrientationSensor_OrientationChanged;
// ...

private void SimpleOrientationSensor_OrientationChanged(SimpleOrientationSensor sender,
    SimpleOrientationSensorOrientationChangedEventArgs args)
{
    DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        switch (args.Orientation)
        {
            case SimpleOrientation.NotRotated:
                txtOrientation.Text = "Not Rotated";
                break;
            case SimpleOrientation.Rotated90DegreesCounterclockwise:
                txtOrientation.Text = "Rotated 90 Degrees Counterclockwise";
                break;
            case SimpleOrientation.Rotated180DegreesCounterclockwise:
                txtOrientation.Text = "Rotated 180 Degrees Counterclockwise";
                break;
            case SimpleOrientation.Rotated270DegreesCounterclockwise:
                txtOrientation.Text = "Rotated 270 Degrees Counterclockwise";
                break;
            case SimpleOrientation.Faceup:
                txtOrientation.Text = "Faceup";
                break;
            case SimpleOrientation.Facedown:
                txtOrientation.Text = "Facedown";
                break;
            default:
                txtOrientation.Text = "Unknown orientation";
                break;
        }
    });
}

```

As an alternative to the `OrientationChanged` event, you can take a one-time reading of the current orientation by calling the [GetCurrentOrientation](/uwp/api/windows.devices.sensors.simpleorientationsensor.getcurrentorientation) method.

 ## Related topics

- [Orientation sensor sample (UWP)](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/OrientationSensor)
- [Simple orientation sensor sample (UWP)](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/SimpleOrientationSensor)
