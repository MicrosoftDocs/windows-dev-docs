---
title: How to build a Hello World app using Windows App SDK
description: Get started with Windows App SDK by building a simple app that displays "Hello, world". 
ms.topic: article
ms.date: 10/01/2021
keywords: windows app sdk, winappsdk
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.custom: template-quickstart
---

# Build a Hello World app using Windows App SDK

In this beginner-oriented how-to, we'll use Visual Studio 2022 and Windows App SDK to build a packaged Windows desktop app that displays "Hello world!" when launched:

:::image type="content" source="https://i.imgur.com/qcbiNZh.png" alt-text="The 'Hello, world!' app we're building.":::

A sample of the app we're building can be found [on Github](todo).

## Prerequisites

 - [Visual Studio 2022 and Tools for Windows App SDK](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/set-up-your-development-environment)


## Create and run the template project

Open Visual Studio and create a new project via `File` > `New` > `Project`:

<img src='https://i.imgur.com/pmrKFqV.png'>

Search for `WinUI` and select the `Blank App, Packaged (WinUI 3 in Desktop)` template:

<img src='https://i.imgur.com/UUsrQ2m.png'>

Specify a project name, solution name, and directory. In this example, our `Hello World` project belongs to a `Hello World` solution. Our work will live in `C:\Projects\`:

<img src='https://i.imgur.com/fMB0iYg.png'>

After creating your project, you should see the following default file structure in your Solution Explorer:

<img src='https://i.imgur.com/odJqdgj.png'>

Click the "Start" button to build and run this templated project:

<img src='https://i.imgur.com/iEfukVA.png'>

Visual Studio may prompt you to `Enable Developer Mode for Windows`:

<img src='https://i.imgur.com/vq4zehs.png'>

With Developer Mode enabled, your `Hello World` project should build and run:

<img src='https://i.imgur.com/T6m6SW7.png'>

Click the `Click Me` button for a demonstration of event binding:

<img src='https://i.imgur.com/gPP5o2U.png'>

In this case, a [`Button` control](https://learn.microsoft.com/en-us/windows/apps/design/controls/buttons#create-a-button)'s [`Click` event](https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click?view=windows-app-sdk-1.1) is bound to the `myButton_Click` event handler located in `MainWindow.xaml.cs`:

<img src='https://i.imgur.com/Fho4Fvz.png'>

While `MainWindow.xaml.cs` contains our main window's **business logic** concerns, it's **presentation** concerns live in `MainWindow.xaml`:

<img src='https://i.imgur.com/H7Ltii4.png'>

This separation of **business logic** and **presentation** concerns makes it easy to bind data and events to your application's UI.

Let's review your project's structure before making code changes.


## Familiarize yourself with WinUI 3 project structure

Our project's file structure currently looks like this:

<img src='https://i.imgur.com/QABCt2t.png'>

Let's review each of these items, starting at the top and working our way down:

| Item                   | Description                                                                                                                                                                                                                         |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Solution 'Hello World' | This is a **solution file**, a logical container for your **projects**. Projects are often apps, but they can also be supporting class libraries.                                                                               |
| Hello World            | This is a **project file**, a logical container for your app's files.                                                                                                                                                           |
| Dependencies           | Your app depends on frameworks (like .NET Core and the Windows SDK) and packages (like Windows App SDK). As you introduce more sophisticated functionality and third-party libraries, additional dependencies will appear here. |
| Properties             | By convention, WinUI 3 projects tuck publish profiles and launch configuration files into this folder.                                                                                                                          |
| PublishProfiles        | Your **publish profiles** specify your app's publishing configuration across a variety of platforms.                                                                                     |
| launchSettings.json    | This file lets you configure **launch profiles** that can be used when running your app via `dotnet run`.                                                                                                                                                                                                                              |
| Assets                 | This folder contains your app's logo, images, and other media assets.                                                                                                                                                                                                                               |
| app.manifest           | This app manifest file contains configuration related to the way that Windows displays your app when installed on user devices.                                                                                                                                                                                                                               |
| App.xaml               | This markup file specifies the shared, globally accessible resources that your app depends on.                                                                                                                                                                                                                               |
| App.xaml.cs            | This code-behind file represents the entry point to your app's business logic. It's responsible for creating and activating an instance of your `MainWindow`.                                                                                                                                                                                                                              |
| MainWindow.xaml        | This markup file contains the presentation concerns for your app's main window.                                                                                                                                                                                                                               |
| MainWindow.xaml.cs     | This code-behind file contains the business logic concerns associated with your app's main window.                                                                                                                                                                                                                               |
| Package.appxmanifest   | This package manifest file lets you configure the way that your app is packaged and displayed within the Microsoft Store.                                                                                                                                                                                                                               |


## Display "Hello world!"

To display "Hello world!" instead of the "Click me" button, navigate to `MainWindow.xaml`. You should see a `StackPanel`:

```
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
   <Button x:Name="myButton" Click="myButton_Click">Click Me</Button>
</StackPanel>
```

> [!TIP]
> You'll frequently refer to **API reference docs** while building Windows apps. [StackPanel's reference docs](https://learn.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.stackpanel?view=winrt-22621) tell you more about the `StackPanel` control, and how to customize it.

Let's update our `StackPanel` control to display `Hello world!` using red text:

```
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
    <TextBlock x:Name="myText" Text="Hello world!" Foreground="Red"/>
</StackPanel>
```

If you try to run your app now, Visual Studio will throw an error along the lines of `The name 'myButton' does not exist in the current context`. This is because we updated the presentation layer with a new component, but we didn't update our business logic.

Navigate to `MainWindow.xaml.cs` and delete the `myButton_Click` event handler. We can do this because we've replaced the interactive `Click me` button with static `Hello world!` text. Our main window's business logic should now look like this:

```
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }
}
```

Your app should now display `Hello world!`: 

<img src='https://i.imgur.com/gsEaVR9.png'>


## Update your app's Window title

Add `this.Title = "Hello world!";` to your `App.xaml.cs` code-behind file:

```
public MainWindow()
{
    this.InitializeComponent();
    this.Title = "Hello world!"; // <- this is new
}

```

If you restart your app, you should now see `Hello world!` in both the body and title bar:

<img src='https://i.imgur.com/qcbiNZh.png'>

Congratulations! You've build your first Windows App SDK / WinUI 3 app. You started with Visual Studio's template, and then you modified both the presentation layer (XAML markup) and business logic (code-behind). 

In the next how-to, we'll reintroduce data binding with a basic form submission experience.