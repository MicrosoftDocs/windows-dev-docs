---
ms.assetid: F90686F5-641A-42D9-BC44-EC6CA11B8A42
title: Use the accelerometer
description: Learn how to create a basic app that relies on a single sensor, the accelerometer, to respond to user movements.
ms.date: 06/06/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Use the accelerometer


**Important APIs**

-   [**Windows.Devices.Sensors**](/uwp/api/Windows.Devices.Sensors)
-   [**Accelerometer**](/uwp/api/Windows.Devices.Sensors.Accelerometer)

**Sample**

-   For a more complete implementation, see the [accelerometer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Accelerometer).

Learn how to use the accelerometer to respond to user movement.

A simple game app relies on a single sensor, the accelerometer, as an input device. These apps typically use only one or two axes for input; but they may also use the shake event as another input source.

## Prerequisites

You should be familiar with Extensible Application Markup Language (XAML), Microsoft Visual C#, and events.

The device or emulator that you're using must support an accelerometer.

## Create a simple accelerometer app

This section is divided into two subsections. The first subsection will take you through the steps necessary to create a simple accelerometer application from scratch. The following subsection explains the app you have just created.

### Instructions

-   Create a new project, choosing a **Blank App (Universal Windows)** from the **Visual C#** project templates.

-   Open your project's MainPage.xaml.cs file and replace the existing code with the following.

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

    // Required to support the core dispatcher and the accelerometer

    using Windows.UI.Core;
    using Windows.Devices.Sensors;

    namespace App1
    {

        public sealed partial class MainPage : Page
        {
            // Sensor and dispatcher variables
            private Accelerometer _accelerometer;

            // This event handler writes the current accelerometer reading to
            // the three acceleration text blocks on the app' s main page.

            private async void ReadingChanged(object sender, AccelerometerReadingChangedEventArgs e)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    AccelerometerReading reading = e.Reading;
                    txtXAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
                    txtYAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationY);
                    txtZAxis.Text = String.Format("{0,5:0.00}", reading.AccelerationZ);

                });
            }

            public MainPage()
            {
                this.InitializeComponent();
                _accelerometer = Accelerometer.GetDefault();

                if (_accelerometer != null)
                {
                    // Establish the report interval
                    uint minReportInterval = _accelerometer.MinimumReportInterval;
                    uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                    _accelerometer.ReportInterval = reportInterval;

                    // Assign an event handler for the reading-changed event
                    _accelerometer.ReadingChanged += new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);
                }
            }
        }
    }
```

You'll need to rename the namespace in the previous snippet with the name you gave your project. For example, if you created a project named **AccelerometerCS**, you'd replace `namespace App1` with `namespace AccelerometerCS`.

-   Open the file MainPage.xaml and replace the original contents with the following XML.

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
            <TextBlock HorizontalAlignment="Left" Height="25" Margin="8,20,0,0" TextWrapping="Wrap" Text="X-axis:" VerticalAlignment="Top" Width="62" Foreground="#FFEDE6E6"/>
            <TextBlock HorizontalAlignment="Left" Height="27" Margin="8,49,0,0" TextWrapping="Wrap" Text="Y-axis:" VerticalAlignment="Top" Width="62" Foreground="#FFF5F2F2"/>
            <TextBlock HorizontalAlignment="Left" Height="23" Margin="8,80,0,0" TextWrapping="Wrap" Text="Z-axis:" VerticalAlignment="Top" Width="62" Foreground="#FFF6F0F0"/>
            <TextBlock x:Name="txtXAxis" HorizontalAlignment="Left" Height="15" Margin="70,16,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="61" Foreground="#FFF2F2F2"/>
            <TextBlock x:Name="txtYAxis" HorizontalAlignment="Left" Height="15" Margin="70,49,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53" Foreground="#FFF2EEEE"/>
            <TextBlock x:Name="txtZAxis" HorizontalAlignment="Left" Height="15" Margin="70,80,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53" Foreground="#FFFFF8F8"/>

        </Grid>
    </Page>
```

You'll need to replace the first part of the class name in the previous snippet with the namespace of your app. For example, if you created a project named **AccelerometerCS**, you'd replace `x:Class="App1.MainPage"` with `x:Class="AccelerometerCS.MainPage"`. You should also replace `xmlns:local="using:App1"` with `xmlns:local="using:AccelerometerCS"`.

-   Press F5 or select **Debug** &gt; **Start Debugging** to build, deploy, and run the app.

Once the app is running, you can change the accelerometer values by moving the device or using the emulator tools.

-   Stop the app by returning to Visual Studio and pressing Shift+F5 or select **Debug** &gt; **Stop Debugging** to stop the app.

### Explanation

The previous example demonstrates how little code you'll need to write in order to integrate accelerometer input in your app.

The app establishes a connection with the default accelerometer in the **MainPage** method.

```csharp
_accelerometer = Accelerometer.GetDefault();
```

The app establishes the report interval within the **MainPage** method. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = _accelerometer.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
_accelerometer.ReportInterval = reportInterval;
```

The new accelerometer data is captured in the **ReadingChanged** method. Each time the sensor driver receives new data from the sensor, it passes the values to your app using this event handler. The app registers this event handler on the following line.

```csharp
_accelerometer.ReadingChanged += new TypedEventHandler<Accelerometer,
AccelerometerReadingChangedEventArgs>(ReadingChanged);
```

These new values are written to the TextBlocks found in the project's XAML.

```xml
<TextBlock x:Name="txtXAxis" HorizontalAlignment="Left" Height="15" Margin="70,16,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="61" Foreground="#FFF2F2F2"/>
 <TextBlock x:Name="txtYAxis" HorizontalAlignment="Left" Height="15" Margin="70,49,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53" Foreground="#FFF2EEEE"/>
 <TextBlock x:Name="txtZAxis" HorizontalAlignment="Left" Height="15" Margin="70,80,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="53" Foreground="#FFFFF8F8"/>
```