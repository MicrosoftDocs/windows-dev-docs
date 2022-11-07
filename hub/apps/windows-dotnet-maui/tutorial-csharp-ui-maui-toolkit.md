---
title: Tutorial--Create a .NET MAUI app with C# Markup and the Community Toolkit
description: Build a .NET MAUI app with a user interface created without XAML by using C# Markup from the .NET MAUI Community Toolkit.
ms.topic: article
ms.date: 11/02/2022
keywords: windows win32, desktop development, Windows App SDK, .net maui
ms.localizationpriority: medium
---

# Tutorial: Create a .NET MAUI app with C# Markup and the Community Toolkit

Build a .NET MAUI app with a user interface created without XAML by using C# Markup from the [.NET MAUI Community Toolkit](/dotnet/communitytoolkit/maui/).

## Introduction

## Setting up the environment

If you haven't already set up your environment for .NET MAUI development, please follow the steps to [Get started with .NET MAUI on Windows](index.md#get-started-with-net-maui-on-windows).

## Creating the .NET MAUI project

> [!NOTE]
> If you are already familiar with setting up a .NET MAUI project, you can skip to the next section.

Launch Visual Studio, and in the start window click **Create a new project** to create a new project.

In the **Create a new project** window, select **MAUI** in the All project types drop-down, select the **.NET MAUI App** template, and click the **Next** button:

![.NET MAUI App template.](images/maui-markup-create-project.png)

Next, on the **Configure your new project** screen, give your project a name, choose a location for it, and click the **Next** button.

On the final screen, **Additional information**, click the **Create** button.

Wait for the project to be created, and for its dependencies to be restored.

In the Visual Studio toolbar, press the **Windows Machine** button to build and run the app. Click the **Click me** button and verify that the button content updates with the number of clicks.

Now that you have verified that the .NET MAUI app on Windows is working as expected, we can integrate the MVVM Toolkit and C# Markup packages. In the next section, you'll add these packages to your new project.

## Add C# Markup from the .NET MAUI Community Toolkit

Now that you have your .NET MAUI app running on Windows, let's add a couple of NuGet packages to the project to integrate with the **MVVM Toolkit** and **C# Markup** from the **.NET MAUI Community Toolkit**.

Right-click the project in **Solution Explorer** and select **Manage NuGet Packages...** from the context menu.

In the **NuGet Package Manager** window, select the **Browse** tab and search for **CommunityToolkit.MVVM**:

![CommunityToolkit.MVVM package.](images/maui-markup-add-mvvm-pkg.png)

Add the latest stable version of the **CommunityToolkit.MVVM** package to the project by clicking **Install**.

Next, search for CommunityToolkit.Maui:

![CommunityToolkit.Maui packages.](images/maui-markup-install-nuget-pkg.png)

Add the latest stable version of the **CommunityToolkit.Maui.Markup** package to the project by clicking **Install**.

Close the **NuGet Package Manager** window after the new packages have finished installing.

## Add a ViewModel to the project

We are going to add a simple **Model-View-ViewModel (MVVM)** implementation with the MVVM Toolkit. Let's start by creating a viewmodel to pair with our view (**MainPage**). Right-click the project again and select **Add | Class** from the context menu.

In the **Add New Item** window that appears, name the class **MainViewModel** and click **Add**:

![Adding a MainViewModel class to the project.](images/maui-markup-add-viewmodel-class.png)

We are going to leverage the power of the MVVM Toolkit in `MainViewModel`. Replace the contents of the class with the following code:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Diagnostics;

namespace MauiMarkupSample
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        [ObservableProperty]
        private string name;
        partial void OnNameChanging(string value)
        {
            Debug.WriteLine($"Name is about to change to {value}");
        }
        partial void OnNameChanged(string value)
        {
            Debug.WriteLine($"Name has changed to {value}");
        }
    }
}
```

If you have completed the [Build your first .NET MAUI app for Windows](walkthrough-first-app.md) tutorial, you will understand what the code above does. The `MainViewModel` class is decorated with the `INotifyPropertyChanged` attribute, which allows the MVVM Toolkit to generate the `INotifyPropertyChanged` implementation for the class. Marking `MainViewModel` as a `partial class` is required for the .NET source generator to work. The `ObservableProperty` attribute on the `name` private field will a `Name` property for the class with the proper `INotifyPropertyChanged` implementation. Adding the `OnNameChanging` and `OnNameChanged` partial methods is optional, but allows you to add custom logic when the `Name` property is changing or has changed.

## Build a UI with C# Markup

When building a UI with C# Markup, the first step is to update the CreateMauiApp() method in MauiProgram.cs. Replace the contents of the method with the following code:

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    builder
        .UseMauiApp<App>()
        .UseMauiCommunityToolkitMarkup();

    return builder.Build();
}
```

You also need to add a new `using` statement to the top of the file: `using CommunityToolkit.Maui.Markup;`. The call to `UseMauiCommunityToolkitMarkup()` will add the C# Markup support to the app, allowing you to construct your UI with C# code instead of XAML.

The **MainPage.xaml** file will no longer be used when rendering the UI, so you can remove the contents of the `ContentPage`.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiMarkupSample.MainPage">
</ContentPage>
```

In **MainPage.xaml.cs**, remove the click event handler and add three private members to the class:

```csharp
private readonly MainViewModel ViewModel = new();
private enum Row { TextEntry }
private enum Column { Description, Input }
```

## Related topics

[Resources for learning .NET MAUI](/dotnet/maui/get-started/resources)

[.NET MAUI Community Toolkit documentation](/dotnet/communitytoolkit/maui/)
