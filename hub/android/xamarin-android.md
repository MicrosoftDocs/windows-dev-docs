---
title: Create a simple Android app with Xamarin.Android
description: How to get started writing Android apps with Xamarin.Android
author: hickeys 
ms.author: hickeys 
manager: jken
ms.topic: article
keywords: android, windows, xamarin.android, tutorial, xaml
ms.date: 04/28/2020
---

# Get started developing for Android using Xamarin.Android

This guide will help you to get started using Xamarin.Android on Windows to create a cross-platform app that will work on Android devices.

In this article, you will create a simple Android app using Xamarin.Android and Visual Studio 2019.

## Requirements

To use this tutorial, you'll need the following:

- Windows 10
- [Visual Studio 2019: Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/) (see note)
- The "Mobile development with .NET" workload for Visual Studio 2019

> [!NOTE]
> This guide will work with Visual Studio 2017 or 2019. If you are using Visual Studio 2017, some instructions may be incorrect due to UI differences between the two versions of Visual Studio.

You will also to have an Android phone or configured emulator in which to run your app. See [Configuring an Android emulator](emulator.md).

## Create a new Xamarin.Android project

Start Visual Studio. Select File > New > Project to create a new project.

In the new project dialog, select the **Android App (Xamarin)** template and click **Next**.

Name the project **TimeChangerAndroid** and click **Create**.

In the New Cross Platform App dialog, select **Blank App**. In the **Minimum Android Version**, select **Android 5.0 (Lollipop)**. Click **OK**.

Xamarin will create a new solution with a single project named **TimeChangerAndroid**.

## Create a UI with XAML

In the **Resources\layout** directory of your project, open **activity_main.xml**. The XML in this file defines the first screen a user will see when opening TimeChanger.

TimeChanger's UI is simple. It displays the current time and has buttons to adjust the time in increments of one hour. It uses a vertical `LinearLayout` to align the time above the buttons and a horizontal `LinearLayout` to arrange the buttons side-by-side. The content is centered in the screen by setting **android:gravity** attribute to **center** in the vertical `LinearLayout`.

Replace the contents of **activity_main.xml** with the following code.

```xml
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:app="http://schemas.android.com/apk/res-auto"
    xmlns:tools="http://schemas.android.com/tools"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:orientation="vertical"
    android:gravity="center">
    <TextView
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:text="At runtime, I will display current time"
        android:id="@+id/timeDisplay"
    />
    <LinearLayout
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:orientation="horizontal">
        <Button
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="Up"
            android:id="@+id/upButton"/>
        <Button
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="Down"
            android:id="@+id/downButton"/>
    </LinearLayout>
</LinearLayout>
```

At this point you can run **TimeChangerAndroid** and see the UI you've created. In the next section, you will add functionality to your UI displaying the current time and enabling the buttons to perform an action.

## Add logic code with C#

Open **MainActivity.cs**. This file contains the code-behind logic that will add functionality to the UI.

### Set the current time

First, get a reference to the `TextView` that will display the time. Use **FindViewById** to search all UI elements for the one with the correct **android:id** (which was set to `"@+id/timeDisplay"` in the xml from the previous step). This is the `TextView` that will display the current time.

```csharp
var timeDisplay = FindViewById<TextView>(Resource.Id.timeDisplay);
```

UI controls must be updated on the UI thread. Changes made from another thread may not properly update the control as it displays on the screen. Because there is no guarantee this code will always be running on the UI thread, use the **RunOnUiThread** method to make sure any updates display correctly. Here is the complete `UpdateTimeLabel` method.

```csharp
private void UpdateTimeLabel(object state = null)
{
    RunOnUiThread(() =>
    {
        TimeDisplay.Text = DateTime.Now.ToLongTimeString();
    });
}
```

### Update the current time once every second

At this point, the current time will be accurate for, at most, one second after TimeChangerAndroid is launched. The label must be periodically updated to keep the time accurate. A **Timer** object will periodically call a callback method that updates the label with the current time.

```csharp
var clockRefresh = new Timer(dueTime: 0, period: 1000, callback: UpdateTimeLabel, state: null);
```

### Add HourOffset

The up and down buttons adjust the time in increments of one hour. Add an **HourOffset** property to track the current adjustment.

```csharp
public int HourOffset { get; private set; }
```

Now update the UpdateTimeLabel method to be aware of the HourOffset property.

```csharp
TimeDisplay.Text = DateTime.Now.AddHours(HourOffset).ToLongTimeString();
```

### Create the button Click event handlers

All the up and down buttons need to do is increment or decrement the HourOffset property and call UpdateTimeLabel.

```csharp
public void UpButton_Click(object sender, System.EventArgs e)
{
    HourOffset++;
    UpdateTimeLabel();
}
```

### Wire up the up and down buttons to their corresponding event handlers

To associate the buttons with their corresponding event handlers, first use FindViewById to find the buttons by their ids. Once you have a reference to the button object, you can add an event handler to its `Click` event.

```csharp
Button upButton = FindViewById<Button>(Resource.Id.upButton);
upButton.Click += UpButton_Click;
```

## Completed MainActivity.cs file

When you're finished, MainActivity.cs should look like this:

```csharp
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Threading;

namespace TimeChangerAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public TextView TimeDisplay { get; private set; }
        public int HourOffset { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var clockRefresh = new Timer(dueTime: 0, period: 1000, callback: UpdateTimeLabel, state: null);

            Button upButton = FindViewById<Button>(Resource.Id.upButton);
            upButton.Click += OnUpButton_Click;

            Button downButton = FindViewById<Button>(Resource.Id.downButton);
            downButton.Click += OnDownButton_Click;

            TimeDisplay = FindViewById<TextView>(Resource.Id.timeDisplay);
        }

        private void UpdateTimeLabel(object state = null)
        {
            // Timer callbacks run on a background thread, but UI updates must run on the UI thread.
            RunOnUiThread(() =>
            {
                TimeDisplay.Text = DateTime.Now.AddHours(HourOffset).ToLongTimeString();
            });
        }

        public void OnUpButton_Click(object sender, System.EventArgs e)
        {
            HourOffset++;
            UpdateTimeLabel();
        }

        public void OnDownButton_Click(object sender, System.EventArgs e)
        {
            HourOffset--;
            UpdateTimeLabel();
        }
    }
}
```

## Run your app

To run the app, press **F5** or click Debug > Start Debugging. Depending on how your [debugger is configured](emulator.md), your app will launch on a device or in an emulator.

## Related links

- [Test on an Android device or emulator](emulator.md).
- [Create an Android sample app using Xamarin.Forms](xamarin-forms.md)
