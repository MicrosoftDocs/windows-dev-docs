---
title: Create a simple Android app with Xamarin.Android
description: How to get started writing Android apps with Xamarin.Android
author: hickeys 
ms.author: hickeys 
manager: jken
ms.topic: article
keywords: 
ms.localizationpriority: medium
ms.date: 01/24/2020
ms.prod: "xamarin"
ms.technology: "xamarin-android"
---

# Create a sample app with Xamarin.Android

In this article, you will create a simple Android app using Xamarin.Android and Visual Studio 2019.

## Requirements

To use this tutorial, you'll need the following:

- Windows 10, version XXXX or higher
- [Visual Studio 2019: Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- The "Mobile development with .NET" workload for Visual Studio 2019

You will also to have an Android phone or configured emulator in which to run your app. See Configuring an Android emulator.

## Create a new Xamarin.Android project

Start Visual Studio. Click File > New > Project to create a new project.

In the new project dialog, select the **Android App (Xamarin)** template and click **Next**.

Name the project **TimeChangerAndroid** and click **Create**.

In the New Cross Platform App dialog, select **Blank App**. In the **Minimum Android Version**, select **Android 5.0 (Lollipop)**. Click **OK**.

Xamarin will create a new solution with a single project named **TimeChangerAndroid**.

## Use XAML to create a a UI

Open **activity_main.xml** located in **Resources\layout**. The XML in this file defines the first screen a user will see when opening TimeChanger.

TimeChanger's UI is simple. It displays the current time, and has buttons to adjust the time in increments of one hour. It uses a vertical LinearLayout to align the time above the buttons, and a horizontal LinearLayout to arrange the buttons side-by-side. The content is centered in the screen by setting the vertical LinearLayout's **android:gravity** attribute to **center**.

Replace the contents of activity_main.xml with the following code.

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

At this point you can run TimeChangerAndroid and see the UI you've created. Unfortunately, the current time will not be displayed and though the buttons can be clicked, they will do not do anything. In the next section, you will add functionality to your UI.

## Add functionality by adding C# code

Open **MainActivity.cs**. This file contains the code-behind that will add functionality to the UI.

### Set the current time

First, get a reference to the TextView that will display the time. Use **FindViewById** to search all UI elements for the one with the correct **android:id** (which was set to `"@+id/timeDisplay"` in the xml from the previous step). This is the TextView that will display the current time.

```csharp
var currentTime = FindViewById<TextView>(Resource.Id.timeDisplay);
```

UI controls must be updated on the UI thread. Changes made from another thread may not properly update the control as it displays on the screen. because there is no guarantee this code will always be running on the UI thread, use the **RunOnUiThread** method to make sure any updates display correctly. Here is the complete UpdateTimeLabel method.

```csharp
private void UpdateTimeLabel(object state = null)
{
    RunOnUiThread(() =>
    {
        var currentTime = FindViewById<TextView>(Resource.Id.timeDisplay);
        currentTime.Text = DateTime.Now.ToLongTimeString();
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
currentTime.Text = DateTime.Now.AddHours(HourOffset).ToLongTimeString();
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

To associate the buttons with their corresponding event handlers, first use FindViewById to find the buttons by their ids. Once you have a reference to the button object, you can add an event handler to its Click event.

```csharp
Button upButton = FindViewById<Button>(Resource.Id.upButton);
upButton.Click += UpButton_Click;
```

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
        public int HourOffset { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set the view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var clockRefresh = new Timer(dueTime: 0, period: 1000, callback: UpdateTimeLabel, state: null);

            Button upButton = FindViewById<Button>(Resource.Id.upButton);
            upButton.Click += UpButton_Click;

            Button downButton = FindViewById<Button>(Resource.Id.downButton);
            downButton.Click += DownButton_Click;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void UpdateTimeLabel(object state = null)
        {
            RunOnUiThread(() =>
            {
                var currentTime = FindViewById<TextView>(Resource.Id.timeDisplay);
                currentTime.Text = DateTime.Now.AddHours(HourOffset).ToLongTimeString();
            });
        }

        public void UpButton_Click(object sender, System.EventArgs e)
        {
            HourOffset++;
            UpdateTimeLabel();
        }

        //[Export("DownButton_Click")]
        public void DownButton_Click(object sender, System.EventArgs e)
        {
            HourOffset--;
            UpdateTimeLabel();
        }
    }
}
```

## Run the app

To run the app, press **F5** or click Debug > Start Debugging. If you are using an Android emulator, your app will start in the emulator you've configured. Otherwise, it will launch on your phone.

## Download the code

Download the complete code sample here.

## Related links

- Configure your dev machine to do Android development
- [Create an Android sample app using Xamarin Forms](xamarin-forms-android.md)
- Create an iOS sample app using Xamarin.iOS
