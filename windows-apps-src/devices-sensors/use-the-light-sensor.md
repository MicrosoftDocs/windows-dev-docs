---
ms.assetid: 15BAB25C-DA8C-4F13-9B8F-EA9E4270BCE9
title: Use the light sensor
description: Learn how to use the ambient light sensor to detect changes in lighting.
ms.date: 06/06/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Use the light sensor


**Important APIs**

-   [**Windows.Devices.Sensors**](/uwp/api/Windows.Devices.Sensors)
-   [**LightSensor**](/uwp/api/Windows.Devices.Sensors.LightSensor)

**Sample**

-   For a more complete implementation, see the [light sensor sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LightSensor).

Learn how to use the ambient light sensor to detect changes in lighting.

An ambient light sensor is one of the several types of environmental sensors that allow apps to respond to changes in the user's environment.

## Prerequisites

You should be familiar with Extensible Application Markup Language (XAML), Microsoft Visual C#, and events.

The device or emulator that you're using must support an ambient light sensor.

## Create a simple light-sensor app

This section is divided into two subsections. The first subsection will take you through the steps necessary to create a simple light-sensor application from scratch. The following subsection explains the app you have just created.

###  Instructions

-   Create a new project, choosing a **Blank App (Universal Windows)** from the **Visual C#** project templates.

-   Open your project's BlankPage.xaml.cs file and replace the existing code with the following.

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

    using Windows.UI.Core; // Required to access the core dispatcher object
    using Windows.Devices.Sensors; // Required to access the sensor platform and the ALS

    // The Blank Page item template is documented at https://go.microsoft.com/fwlink/p/?linkid=234238

    namespace App1
    {
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class BlankPage : Page
        {
            private LightSensor _lightsensor; // Our app' s lightsensor object

            // This event handler writes the current light-sensor reading to
            // the textbox named "txtLUX" on the app' s main page.

            private void ReadingChanged(object sender, LightSensorReadingChangedEventArgs e)
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (s, a) =>
                {
                    LightSensorReading reading = (a.Context as LightSensorReadingChangedEventArgs).Reading;
                    txtLuxValue.Text = String.Format("{0,5:0.00}", reading.IlluminanceInLux);
                });
            }

            public BlankPage()
            {
                InitializeComponent();
                _lightsensor = LightSensor.GetDefault(); // Get the default light sensor object

                // Assign an event handler for the ALS reading-changed event
                if (_lightsensor != null)
                {
                    // Establish the report interval for all scenarios
                    uint minReportInterval = _lightsensor.MinimumReportInterval;
                    uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                    _lightsensor.ReportInterval = reportInterval;

                    // Establish the even thandler
                    _lightsensor.ReadingChanged += new TypedEventHandler<LightSensor, LightSensorReadingChangedEventArgs>(ReadingChanged);
                }

            }

        }
    }
```

You'll need to rename the namespace in the previous snippet with the name you gave your project. For example, if you created a project named **LightingCS**, you'd replace `namespace App1` with `namespace LightingCS`.

-   Open the file MainPage.xaml and replace the original contents with the following XML.

```xml
    <Page
        x:Class="App1.BlankPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:App1"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">

        <Grid x:Name="LayoutRoot" Background="Black">
            <TextBlock HorizontalAlignment="Left" Height="44" Margin="52,38,0,0" TextWrapping="Wrap" Text="LUX Reading" VerticalAlignment="Top" Width="150"/>
            <TextBlock x:Name="txtLuxValue" HorizontalAlignment="Left" Height="44" Margin="224,38,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="217"/>


        </Grid>

    </Page>
```

You'll need to replace the first part of the class name in the previous snippet with the namespace of your app. For example, if you created a project named **LightingCS**, you'd replace `x:Class="App1.MainPage"` with `x:Class="LightingCS.MainPage"`. You should also replace `xmlns:local="using:App1"` with `xmlns:local="using:LightingCS"`.

-   Press F5 or select **Debug** > **Start Debugging** to build, deploy, and run the app.

Once the app is running, you can change the light sensor values by altering the light available to the sensor or using the emulator tools.

-   Stop the app by returning to Visual Studio and pressing Shift+F5 or select **Debug** > **Stop Debugging** to stop the app.

###  Explanation

The previous example demonstrates how little code you'll need to write in order to integrate light-sensor input in your app.

The app establishes a connection with the default sensor in the **BlankPage** method.

```csharp
_lightsensor = LightSensor.GetDefault(); // Get the default light sensor object
```

The app establishes the report interval within the **BlankPage** method. This code retrieves the minimum interval supported by the device and compares it to a requested interval of 16 milliseconds (which approximates a 60-Hz refresh rate). If the minimum supported interval is greater than the requested interval, the code sets the value to the minimum. Otherwise, it sets the value to the requested interval.

```csharp
uint minReportInterval = _lightsensor.MinimumReportInterval;
uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
_lightsensor.ReportInterval = reportInterval;
```
The new light-sensor data is captured in the **ReadingChanged** method. Each time the sensor driver receives new data from the sensor, it passes the value to your app using this event handler. The app registers this event handler on the following line.

```csharp
_lightsensor.ReadingChanged += new TypedEventHandler<LightSensor,
LightSensorReadingChangedEventArgs>(ReadingChanged);
```

These new values are written to a TextBlock found in the project's XAML.

```xml
<TextBlock HorizontalAlignment="Left" Height="44" Margin="52,38,0,0" TextWrapping="Wrap" Text="LUX Reading" VerticalAlignment="Top" Width="150"/>
 <TextBlock x:Name="txtLuxValue" HorizontalAlignment="Left" Height="44" Margin="224,38,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Width="217"/>
```