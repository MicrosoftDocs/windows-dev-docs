---

title: Use the orientation sensor
description: Learn how to use the orientation sensors to determine the device orientation.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Use the orientation sensor

Learn how to use the orientation sensors to determine the device orientation.

**Important APIs**

- [**Windows.Devices.Sensors**](/uwp/api/Windows.Devices.Sensors)
- [**OrientationSensor**](/uwp/api/Windows.Devices.Sensors.OrientationSensor)
- [**SimpleOrientation**](/uwp/api/Windows.Devices.Sensors.SimpleOrientation)

## Prerequisites

You should be familiar with Extensible Application Markup Language (XAML), Microsoft Visual C#, and events.

The device or emulator that you're using must support an orientation sensor.

## Create an OrientationSensor app

An orientation sensor is one of the several types of environmental sensors that allow apps to respond to changes in the device orientation.

There are two different types of orientation sensor APIs included in the [**Windows.Devices.Sensors**](/uwp/api/Windows.Devices.Sensors) namespace: [**OrientationSensor**](/uwp/api/Windows.Devices.Sensors.OrientationSensor) and [**SimpleOrientation**](/uwp/api/Windows.Devices.Sensors.SimpleOrientation). While both of these sensors are orientation sensors, that term is overloaded and they are used for very different purposes. However, since both are orientation sensors, they are both covered in this article.

The [**OrientationSensor**](/uwp/api/Windows.Devices.Sensors.OrientationSensor) API is used for 3-D apps two obtain a quaternion and a rotation matrix. A quaternion can be most easily understood as a rotation of a point \[x,y,z\] about an arbitrary axis (contrasted with a rotation matrix, which represents rotations around three axes). The mathematics behind quaternions is fairly exotic in that it involves the geometric properties of complex numbers and mathematical properties of imaginary numbers, but working with them is simple, and frameworks like DirectX support them. A complex 3-D app can use the Orientation sensor to adjust the user's perspective. This sensor combines input from the accelerometer, gyrometer, and compass.

The [**SimpleOrientation**](/uwp/api/Windows.Devices.Sensors.SimpleOrientation) API is used to determine the current device orientation in terms of definitions like portrait up, portrait down, landscape left, and landscape right. It can also detect if a device is face-up or face-down. Rather than returning properties like "portrait up" or "landscape left", this sensor returns a rotation value: "Not rotated", "Rotated90DegreesCounterclockwise", and so on. The following table maps common orientation properties to the corresponding sensor reading.

| Orientation     | Corresponding sensor reading      |
|-----------------|-----------------------------------|
| Portrait Up     | NotRotated                        |
| Landscape Left  | Rotated90DegreesCounterclockwise  |
| Portrait Down   | Rotated180DegreesCounterclockwise |
| Landscape Right | Rotated270DegreesCounterclockwise |

> [!NOTE]
> For a more complete implementation, see:
>
> - [Orientation sensor sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/OrientationSensor)
> - [Simple orientation sensor sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/SimpleOrientationSensor)

### Instructions

- Create a new project, choosing a **Blank App (Universal Windows)** from the **Visual C#** project templates.

- Open your project's MainPage.xaml.cs file and replace the existing code with the following.

```csharp
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    using Windows.UI.Core;
    using Windows.Devices.Sensors;

    // The Blank Page item template is documented at https://go.microsoft.com/fwlink/p/?linkid=234238

    namespace App1
    {
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class MainPage : Page
        {
            private OrientationSensor _sensor;

            private async void ReadingChanged(object sender, OrientationSensorReadingChangedEventArgs e)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    OrientationSensorReading reading = e.Reading;

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

            public MainPage()
            {
                this.InitializeComponent();
                _sensor = OrientationSensor.GetDefault();

                // Establish the report interval for all scenarios
                uint minReportInterval = _sensor.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                _sensor.ReportInterval = reportInterval;

                // Establish event handler
                _sensor.ReadingChanged += new TypedEventHandler<OrientationSensor, OrientationSensorReadingChangedEventArgs>(ReadingChanged);
            }
        }
    }
```

You'll need to rename the namespace in the previous snippet with the name you gave your project. For example, if you created a project named **OrientationSensorCS**, you'd replace `namespace App1` with `namespace OrientationSensorCS`.

- Open the file MainPage.xaml and replace the original contents with the following XML.

```xml
        <Page
        x:Class="App1.MainPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:App1"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">

        <Grid x:Name="LayoutRoot" Background="Black">
            <TextBlock HorizontalAlignment="Left" Height="28" Margin="4,4,0,0" TextWrapping="Wrap" Text="M11:" VerticalAlignment="Top" Width="46"/>
            <TextBlock HorizontalAlignment="Left" Height="23" Margin="4,36,0,0" TextWrapping="Wrap" Text="M12:" VerticalAlignment="Top" Width="39"/>
            <TextBlock HorizontalAlignment="Left" Height="24" Margin="4,72,0,0" TextWrapping="Wrap" Text="M13:" VerticalAlignment="Top" Width="39"/>
            <TextBlock HorizontalAlignment="Left" Height="31" Margin="4,118,0,0" TextWrapping="Wrap" Text="M21:" VerticalAlignment="Top" Width="39"/>
            <TextBlock HorizontalAlignment="Left" Height="24" Margin="4,160,0,0" TextWrapping="Wrap" Text="M22:" VerticalAlignment="Top" Width="39"/>
            <TextBlock HorizontalAlignment="Left" Height="24" Margin="8,201,0,0" TextWrapping="Wrap" Text="M23:" VerticalAlignment="Top" Width="35"/>
            <TextBlock HorizontalAlignment="Left" Height="23" Margin="4,234,0,0" TextWrapping="Wrap" Text="M31:" VerticalAlignment="Top" Width="39"/>
            <TextBlock HorizontalAlignment="Left" Height="28" Margin="4,274,0,0" TextWrapping="Wrap" Text="M32:" VerticalAlignment="Top" Width="46"/>
            <TextBlock HorizontalAlignment="Left" Height="21" Margin="4,322,0,0" TextWrapping="Wrap" Text="M33:" VerticalAlignment="Top" Width="39"/>
            <TextBlock x:Name="txtM11" HorizontalAlignment="Left" Height="19" Margin="43,4,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM12" HorizontalAlignment="Left" Height="23" Margin="43,36,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM13" HorizontalAlignment="Left" Height="15" Margin="43,72,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM21" HorizontalAlignment="Left" Height="20" Margin="43,114,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM22" HorizontalAlignment="Left" Height="19" Margin="43,156,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM23" HorizontalAlignment="Left" Height="16" Margin="43,197,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM31" HorizontalAlignment="Left" Height="17" Margin="43,230,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM32" HorizontalAlignment="Left" Height="19" Margin="43,270,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock x:Name="txtM33" HorizontalAlignment="Left" Height="21" Margin="43,322,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53"/>
            <TextBlock HorizontalAlignment="Left" Height="15" Margin="194,8,0,0" TextWrapping="Wrap" Text="Quaternion X:" VerticalAlignment="Top" Width="81"/>
            <TextBlock HorizontalAlignment="Left" Height="23" Margin="194,36,0,0" TextWrapping="Wrap" Text="Quaternion Y:" VerticalAlignment="Top" Width="81"/>
            <TextBlock HorizontalAlignment="Left" Height="15" Margin="194,72,0,0" TextWrapping="Wrap" Text="Quaternion Z:" VerticalAlignment="Top" Width="81"/>
            <TextBlock x:Name="txtQuaternionX" HorizontalAlignment="Left" Height="15" Margin="279,8,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="104"/>
            <TextBlock x:Name="txtQuaternionY" HorizontalAlignment="Left" Height="12" Margin="275,36,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="108"/>
            <TextBlock x:Name="txtQuaternionZ" HorizontalAlignment="Left" Height="19" Margin="275,68,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="89"/>
            <TextBlock HorizontalAlignment="Left" Height="21" Margin="194,96,0,0" TextWrapping="Wrap" Text="Quaternion W:" VerticalAlignment="Top" Width="81"/>
            <TextBlock x:Name="txtQuaternionW" HorizontalAlignment="Left" Height="12" Margin="279,96,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="72"/>

        </Grid>
    </Page>
```

You'll need to replace the first part of the class name in the previous snippet with the namespace of your app. For example, if you created a project named **OrientationSensorCS**, you'd replace `x:Class="App1.MainPage"` with `x:Class="OrientationSensorCS.MainPage"`. You should also replace `xmlns:local="using:App1"` with `xmlns:local="using:OrientationSensorCS"`.

- Press F5 or select **Debug** > **Start Debugging** to build, deploy, and run the app.

Once the app is running, you can change the orientation by moving the device or using the emulator tools.

- Stop the app by returning to Visual Studio and pressing Shift+F5 or select **Debug** > **Stop Debugging** to stop the app.

###  Explanation

The previous example demonstrates how little code you'll need to write in order to integrate orientation-sensor input in your app.

The app establishes a connection with the default orientation sensor in the **MainPage** method.

```csharp
_sensor = OrientationSensor.GetDefault();
```

The app establishes the report interval within the **MainPage** method. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = _sensor.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
_sensor.ReportInterval = reportInterval;
```

The new sensor data is captured in the **ReadingChanged** method. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event handler. The app registers this event handler on the following line.

```csharp
_sensor.ReadingChanged += new TypedEventHandler<OrientationSensor,
OrientationSensorReadingChangedEventArgs>(ReadingChanged);
```

These new values are written to the TextBlocks found in the project's XAML.

## Create a SimpleOrientation app

This section is divided into two subsections. The first subsection will take you through the steps necessary to create a simple orientation application from scratch. The following subsection explains the app you have just created.

### Instructions

- Create a new project, choosing a **Blank App (Universal Windows)** from the **Visual C#** project templates.

- Open your project's MainPage.xaml.cs file and replace the existing code with the following.

```csharp
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    using Windows.UI.Core;
    using Windows.Devices.Sensors;
    // The Blank Page item template is documented at https://go.microsoft.com/fwlink/p/?linkid=234238

    namespace App1
    {
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class MainPage : Page
        {
            // Sensor and dispatcher variables
            private SimpleOrientationSensor _simpleorientation;

            // This event handler writes the current sensor reading to
            // a text block on the app' s main page.

            private async void OrientationChanged(object sender, SimpleOrientationSensorOrientationChangedEventArgs e)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    SimpleOrientation orientation = e.Orientation;
                    switch (orientation)
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

            public MainPage()
            {
                this.InitializeComponent();
                _simpleorientation = SimpleOrientationSensor.GetDefault();

                // Assign an event handler for the sensor orientation-changed event
                if (_simpleorientation != null)
                {
                    _simpleorientation.OrientationChanged += new TypedEventHandler<SimpleOrientationSensor, SimpleOrientationSensorOrientationChangedEventArgs>(OrientationChanged);
                }
            }
        }
    }
```

You'll need to rename the namespace in the previous snippet with the name you gave your project. For example, if you created a project named **SimpleOrientationCS**, you'd replace `namespace App1` with `namespace SimpleOrientationCS`.

- Open the file MainPage.xaml and replace the original contents with the following XML.

```xml
    <Page
        x:Class="App1.MainPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:App1"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">

        <Grid x:Name="LayoutRoot" Background="#FF0C0C0C">
            <TextBlock HorizontalAlignment="Left" Height="24" Margin="8,8,0,0" TextWrapping="Wrap" Text="Current Orientation:" VerticalAlignment="Top" Width="101" Foreground="#FFF8F7F7"/>
            <TextBlock x:Name="txtOrientation" HorizontalAlignment="Left" Height="24" Margin="118,8,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="175" Foreground="#FFFEFAFA"/>

        </Grid>
    </Page>
```

You'll need to replace the first part of the class name in the previous snippet with the namespace of your app. For example, if you created a project named **SimpleOrientationCS**, you'd replace `x:Class="App1.MainPage"` with `x:Class="SimpleOrientationCS.MainPage"`. You should also replace `xmlns:local="using:App1"` with `xmlns:local="using:SimpleOrientationCS"`.

- Press F5 or select **Debug** > **Start Debugging** to build, deploy, and run the app.

Once the app is running, you can change the orientation by moving the device or using the emulator tools.

- Stop the app by returning to Visual Studio and pressing Shift+F5 or select **Debug** > **Stop Debugging** to stop the app.

### Explanation

The previous example demonstrates how little code you'll need to write in order to integrate simple-orientation sensor input in your app.

The app establishes a connection with the default sensor in the **MainPage** method.

```csharp
_simpleorientation = SimpleOrientationSensor.GetDefault();
```

The new sensor data is captured in the **OrientationChanged** method. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event handler. The app registers this event handler on the following line.

```csharp
_simpleorientation.OrientationChanged += new TypedEventHandler<SimpleOrientationSensor,
SimpleOrientationSensorOrientationChangedEventArgs>(OrientationChanged);
```

These new values are written to a TextBlock found in the project's XAML.

```csharp
<TextBlock HorizontalAlignment="Left" Height="24" Margin="8,8,0,0" TextWrapping="Wrap" Text="Current Orientation:" VerticalAlignment="Top" Width="101" Foreground="#FFF8F7F7"/>
 <TextBlock x:Name="txtOrientation" HorizontalAlignment="Left" Height="24" Margin="118,8,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="175" Foreground="#FFFEFAFA"/>
```