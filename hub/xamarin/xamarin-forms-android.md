---
title: Create a simple Android app with Xamarin Forms
description: How to get started writing Android apps with Xamarin Forms
author: hickeys 
ms.author: hickeys 
manager: jken
ms.topic: article
keywords: 
ms.localizationpriority: medium
ms.date: 01/24/2020
ms.technology: "xamarin"
---

# Create a sample app with Xamarin Forms

In this article, you will create a simple Android app using Xamarin Forms and Visual Studio 2019.

## Requirements

To use this tutorial, you'll need the following:

- Windows 10, version XXXX or higher
- [Visual Studio 2019: Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- The "Mobile development with .NET" workload for Visual Studio 2019

You will also to have an Android phone or configured emulator in which to run your app. See Configuring an Android emulator.

## Create a new Xamarin Forms project

Start Visual Studio. Click File > New > Project to create a new project.

In the new project dialog, select the **Mobile App (Xamarin.Forms)** template and click **Next**.

Name the project **TimeChanger** and click **Create**.

In the New Cross Platform App dialog, select **Blank**. In the Platform section, check **Android** and uncheck all other boxes. Click **OK**.

Xamarin will create a new solution with two projects: **TimeChanger** and **TimeChanger.Android.**

## Use XAML to create a a UI

Expand the **TimeChanger** project and open **MainPage.xaml**. The XAML in this file will be the first screen a user will see when they open TimeChanger.

TimeChanger's UI is simple. It displays the current time, and has buttons to adjust the time in increments of one hour. It uses a vertical StackLayout to align the time above the buttons, and a horizontal StackLayout to arrange the buttons side-by-side.

Replace the contents of MainPage.xaml with the following code.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="TimeChanger.MainPage">

    <StackLayout HorizontalOptions="CenterAndExpand"
                    VerticalOptions="CenterAndExpand">
        <Label HorizontalOptions="CenterAndExpand"
               VerticalOptions="CenterAndExpand"
               x:Name="time">
            Current Time
        </Label>
        <StackLayout Orientation="Horizontal">
            <Button HorizontalOptions="End"
                    VerticalOptions="End"
                    Text="Plus"
                    Clicked="PlusButton_Clicked"/>
            <Button HorizontalOptions="Start"
                    VerticalOptions="End"
                    Text="Minus"
                    Clicked="MinusButton_Clicked"/>
        </StackLayout>
    </StackLayout>
</ContentPage>
```

## Add functionality by adding C# code

In the XAML you just added, both buttons have a reference to a **Clicked** event handler that we need to define in the code behind. Additionally, the label that displays the current time must be populated with the system time when the app runs. These tasks will need to be accomplished in MainPage.xaml.cs, the codebehind "half" of the MainPage.

First, populate the label with the current time when the app launches. To do this, hook into the **OnAppearing** event, which will be raised when your app is about to appear to the user. The following code puts the current system time into the **time** label in the XAML. It also kicks off a timer to periodically update the time so it stays current.

> [!NOTE]
> The timer must be run on the UI thread, otherwise the UI will not refresh and the updated time will not display. The code below uses `Device.BeginInvokeOnMainThread()` to ensure that `Timer_Elapsed()` is called from the main thread.

```csharp
        public int HourOffset { get; private set; } = 0;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Ensure the timer runs on the main thread so the UI will get updated
            Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());

            // Update the time once per second
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());
                return true; // repeat
            });
        }

        private void UpdateTimeLabel()
        {
            time.Text = DateTime.Now.AddHours(HourOffset).ToString(System.Globalization.CultureInfo.CurrentUICulture);
        }
```

You'll also need to add code to handle the button press events. This code simply increments or decrements the HourOffset property and updates the UI.

```csharp
        private void PlusButton_Clicked(object sender, EventArgs e)
        {
            HourOffset++;
            Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());
        }

        private void MinusButton_Clicked(object sender, EventArgs e)
        {
            HourOffset--;
            Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());
        }
```

When you're finished, MainPage.xaml.cs should look like this:

```csharp
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace TimeChanger
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public int HourOffset { get; private set; } = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Ensure the timer runs on the main thread so the UI will get updated
            Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());

            // Update the time once per second
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());
                return true; // repeat
            });
        }

        private void UpdateTimeLabel()
        {
            time.Text = DateTime.Now.AddHours(HourOffset).ToString(System.Globalization.CultureInfo.CurrentUICulture);
        }

        private void PlusButton_Clicked(object sender, EventArgs e)
        {
            HourOffset++;
            Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());
        }

        private void MinusButton_Clicked(object sender, EventArgs e)
        {
            HourOffset--;
            Device.BeginInvokeOnMainThread(() => UpdateTimeLabel());
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
- Create an Android sample app using Xamarin.Android
- Create an iOS sample app using Xamarin.iOS
