---
title: How to build a Hello World app using Windows App SDK
description: Get started with Windows App SDK by building a simple app that displays "Hello, world!". 
ms.topic: article
ms.date: 10/20/2022
keywords: windows app sdk, winappsdk, winui3
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.custom: template-quickstart
---

# Build a Hello World app using Windows App SDK

In this beginner-oriented how-to, we'll use Visual Studio 2022 and Windows App SDK to build a packaged Windows desktop app that displays "Hello world!" when launched:

:::image type="content" source="https://i.imgur.com/qcbiNZh.png" alt-text="The 'Hello, world!' app we're building.":::

The source code for the app we're building in this how-to can be found [on Github](todo).

## Prerequisites

 - [Visual Studio 2022 and Tools for Windows App SDK](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/set-up-your-development-environment)


## Create and run the template project

Open Visual Studio and create a new project via `File` > `New` > `Project`:

:::image type="content" source="https://i.imgur.com/pmrKFqV.png" alt-text="Create a new project":::

Search for `WinUI` and select the `Blank App, Packaged (WinUI 3 in Desktop)` template:

:::image type="content" source="https://i.imgur.com/UUsrQ2m.png" alt-text="Blank, packaged WinUI 3 desktop app":::

Specify a project name, solution name, and directory. In this example, our `Hello World` project belongs to a `Hello World` solution. Our work will live in `C:\Projects\`:

:::image type="content" source="https://i.imgur.com/fMB0iYg.png" alt-text="Specify project details":::

After creating your project, you should see the following default file structure in your Solution Explorer:

:::image type="content" source="https://i.imgur.com/odJqdgj.png" alt-text="Default file structure":::

Click the "Start" button to build and run this templated project:

:::image type="content" source="https://i.imgur.com/iEfukVA.png" alt-text="Build and run your project":::

Visual Studio may prompt you to `Enable Developer Mode for Windows`:

:::image type="content" source="https://i.imgur.com/vq4zehs.png" alt-text="Enable Developer Mode":::

With Developer Mode enabled, your `Hello World` project should build and run:

:::image type="content" source="https://i.imgur.com/T6m6SW7.png" alt-text="Templated project built running":::

Click the `Click Me` button for a demonstration of event binding:

:::image type="content" source="https://i.imgur.com/gPP5o2U.png" alt-text="The 'Click Me' button":::

In this case, a [`Button` control](https://learn.microsoft.com/en-us/windows/apps/design/controls/buttons#create-a-button)'s [`Click` event](https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click?view=windows-app-sdk-1.1) is bound to the `myButton_Click` event handler located in `MainWindow.xaml.cs`:

:::image type="content" source="https://i.imgur.com/Fho4Fvz.png" alt-text="The 'Click Me' button's event handler, located in your main window's code-behind file":::

While `MainWindow.xaml.cs` contains our main window's **business logic** concerns in the form of a code-behind file, it's **presentation** concerns live in `MainWindow.xaml`:

:::image type="content" source="https://i.imgur.com/H7Ltii4.png" alt-text="The 'Click Me' button's XML markup, located in your main window's markup file":::

This separation of **business logic** and **presentation** concerns lets you bind data and events to your application's UI using a consistent application development pattern.

Let's review your project's file structure before making code changes.

## Review your project's file structure

Our project's file structure currently looks like this:

:::image type="content" source="https://i.imgur.com/QABCt2t.png" alt-text="File structure overview":::

Starting from the top and working our way down:

| Item                     | Description                                                                                                                                                                                                                     |
|--------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Solution 'Hello World'` | This is a **solution file**, a logical container for your **projects**. Projects are often apps, but they can also be supporting class libraries.                                                                               |
| `Hello World`            | This is a **project file**, a logical container for your app's files.                                                                                                                                                           |
| `Dependencies`           | Your app depends on frameworks (like .NET Core and the Windows SDK) and packages (like Windows App SDK). As you introduce more sophisticated functionality and third-party libraries, additional dependencies will appear here. |
| `Properties`             | By convention, WinUI 3 projects tuck publish profiles and launch configuration files into this folder.                                                                                                                          |
| `PublishProfiles`        | Your **publish profiles** specify your app's publishing configuration across a variety of platforms.                                                                                                                            |
| `launchSettings.json`    | This file lets you configure **launch profiles** that can be used when running your app via `dotnet run`.                                                                                                                       |
| `Assets`                 | This folder contains your app's logo, images, and other media assets.                                                                                                                                                           |
| `app.manifest`           | This app manifest file contains configuration related to the way that Windows displays your app when installed on user devices.                                                                                                 |
| `App.xaml`               | This markup file specifies the shared, globally accessible resources that your app depends on.                                                                                                                                  |
| `App.xaml.cs`            | This code-behind file represents the entry point to your app's business logic. It's responsible for creating and activating an instance of your `MainWindow`.                                                                   |
| `MainWindow.xaml`        | This markup file contains the presentation concerns for your app's main window.                                                                                                                                                 |
| `MainWindow.xaml.cs`     | This code-behind file contains the business logic concerns associated with your app's main window.                                                                                                                              |
| `Package.appxmanifest`   | This package manifest file lets you configure the way that your app is packaged and displayed within the Microsoft Store.                                                                                                       |


## Display "Hello world!"

To display "Hello world!" instead of the "Click me" button, navigate to `MainWindow.xaml`. You should see a `StackPanel` control's XAML markup:

```xml
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
   <Button x:Name="myButton" Click="myButton_Click">Click Me</Button>
</StackPanel>
```

> [!TIP]
> You'll frequently refer to **API reference docs** while building Windows apps. [StackPanel's reference docs](https://learn.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.stackpanel?view=winrt-22621) tell you more about the `StackPanel` control, and how to customize it.

Let's update our `StackPanel` control to display `Hello world!` with red text:

```xml
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
    <TextBlock x:Name="myText" Text="Hello world!" Foreground="Red"/>
</StackPanel>
```

If you try to run your app now, Visual Studio will throw an error along the lines of `The name 'myButton' does not exist in the current context`. This is because we updated the presentation layer with a new control, but we didn't update the old control's business logic in our code-behind file.

Navigate to `MainWindow.xaml.cs` and delete the `myButton_Click` event handler. We can do this because we've replaced the interactive `Click me` button with static `Hello world!` text. Our main window's business logic should now look like this:

```csharp
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    // ↓ you can delete this
    //private void myButton_Click(object sender, RoutedEventArgs e)
    //{
    //    myButton.Content = "Clicked";
    //}
}
```

If you restart your app, you should see a red `Hello world!`:

:::image type="content" source="https://i.imgur.com/gsEaVR9.png" alt-text="A red 'Hello world!'":::

## Update your app's title bar

Add `this.Title = "Hello world!";` to your `App.xaml.cs` code-behind file:

```csharp
public MainWindow()
{
    this.InitializeComponent();
    this.Title = "Hello world!"; // <- this is new
}

```

If you restart your app, you should now see `Hello world!` in both the body and title bar:

:::image type="content" source="https://i.imgur.com/qcbiNZh.png" alt-text="The 'Hello, world!' app we're building.":::

Congratulations! You've built your first Windows App SDK / WinUI 3 app. 


## Recap

Here's what you accomplished in this how-to:

 1. You started with Visual Studio's **project template**.
 2. You experienced an **event handler** that bound a **`Button` control's** **`Click` event** to a UI update.
 3. You familiarized yourself with the **convention of separating presentation concerns** from **business logic** using tightly-coupled **XAML markup files** and **C# code-behind files**, respectively.
 4. You reviewed the default WinUI 3 project **file structure**.
 5. You modified both the presentation layer (XAML markup) and business logic (code-behind) to support a new **`TextBlock` control** within a `StackPanel`.
 6. You reviewed **reference docs** to better understand the **`StackPanel` control's properties**.
 7. You updated your app's **title bar**.


## FAQ

**Q: What's the difference between a "Packaged" and "Unpackaged" app?**
TODO
