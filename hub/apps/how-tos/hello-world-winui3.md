---
title: How to build a Hello World app using C# / WinUI 3 / Windows App SDK
description: Get started with WinUI 3 / Windows App SDK by building a simple Windows desktop app that displays "Hello world!". 
ms.topic: article
ms.date: 2/20/2023
keywords: windows app sdk, winappsdk, winui3
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
---

# Build a Hello World app using C# and WinUI 3 / Windows App SDK

> [!IMPORTANT]
> This how-to is currently **experimental** and is likely to evolve significantly based on feedback from developers like you. Want to help us improve? [Grade this doc](https://forms.office.com/pages/responsepage.aspx?id=v4j5cvGGr0GRqy180BHbR6A2NSrqm-tGt2feGYAotohUMjEzS1pMTzJTU1VZN1RPNjNDOE4yUFRSSi4u) or [request an update on Github](https://github.com/MicrosoftDocs/windows-dev-docs/issues/new?title=Update%20request%3A%20How%20to%20build%20a%20hello%20world%20app&body=%28How%20can%20we%20help%3F%29&assignee=matchamatch) - we're listening!

In this how-to, we'll use Visual Studio 2022 and WinUI 3 / Windows App SDK to build a Windows desktop app that displays "Hello world!" when launched:

:::image type="content" source="images/hello-world/end-result.png" alt-text="The 'Hello world' app we're building.":::

This how-to is targeted at **beginners** and makes no assumptions about your familiarity with Windows desktop development. 

<!--todo: The source code for the app we're building in this how-to is available [on Github](https://github.com/microsoft/WindowsAppSDK-Samples).-->

## Prerequisites

 - [Visual Studio 2022 and Tools for Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)


## Create a new project using the WinUI 3 C# project template

Open Visual Studio and create a new project via `File` > `New` > `Project`:

:::image type="content" source="images/hello-world/create-project.png" alt-text="Create a new project":::

Search for `WinUI` and select the `Blank App, Packaged (WinUI 3 in Desktop)` C# project template:

:::image type="content" source="images/hello-world/vsix.png" alt-text="Blank, packaged WinUI 3 C# desktop app":::

Specify a project name, solution name, and directory. In this example, our `Hello World` project belongs to a `Hello World` solution, which will live in `C:\Projects\`:

:::image type="content" source="images/hello-world/configure-project.png" alt-text="Specify project details":::

After creating your project, you should see the following default file structure in your Solution Explorer:

:::image type="content" source="images/hello-world/collapsed-file-structure.png" alt-text="Default file structure":::

## Build and run your project

Click the "Start" button to build and run your project:

:::image type="content" source="images/hello-world/start-click.png" alt-text="Build and run your project":::

Visual Studio may prompt you to `Enable Developer Mode for Windows`:

:::image type="content" source="images/hello-world/enable-developer-mode.png" alt-text="Enable Developer Mode":::

With Developer Mode enabled, your `Hello World` project should build and run:

:::image type="content" source="images/hello-world/click-me.png" alt-text="Templated project built running":::

Click the `Click Me` button for a demonstration of event [binding](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.binding):

:::image type="content" source="images/hello-world/clicked-me.png" alt-text="The 'Click Me' button":::

In this case, a [`Button` control](../design/controls/buttons.md#create-a-button)'s [`Click` event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click) is bound to the `myButton_Click` event handler located in `MainWindow.xaml.cs`:

:::image type="content" source="images/hello-world/code-screenshot.png" alt-text="The 'Click Me' button's event handler, located in your main window's code-behind file":::

While `MainWindow.xaml.cs` contains our main window's **business logic** concerns in the form of a code-behind file, its **presentation** concerns live in `MainWindow.xaml`:

:::image type="content" source="images/hello-world/markup-screenshot.png" alt-text="The 'Click Me' button's XML markup, located in your main window's markup file":::

This separation of **business logic** and **presentation** concerns lets you bind data and events to and from your application's UI using a consistent application development pattern.

Let's review your project's file structure before making code changes.

## Review your project's file structure

Our project's file structure currently looks like this:

:::image type="content" source="images/hello-world/expanded-file-structure.png" alt-text="File structure overview":::

Starting from the top and working our way down:

| Item                     | Description                                                                                                                                                                                                                             |
| ------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Solution 'Hello World'` | This is a **solution file**, a logical container for your **projects**. Projects are often apps, but they can also be supporting class libraries.                                                                                       |
| `Hello World`            | This is a **project file**, a logical container for your app's files.                                                                                                                                                                   |
| `Dependencies`           | Your app depends on **frameworks** (like [.NET](/dotnet/fundamentals/) and the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/)) and **packages** (like [Windows App SDK](https://www.nuget.org/packages/Microsoft.WindowsAppSDK/#versions-body-tab)). As you introduce more sophisticated functionality and third-party libraries into your app, additional dependencies will appear here. |
| `Properties`             | By convention, WinUI 3 projects tuck publish profiles and launch configuration files into this folder.                                                                                                                                  |
| `PublishProfiles`        | Your **publish profiles** specify your app's publishing configuration across a variety of platforms.                                                                                                                                    |
| `launchSettings.json`    | This file lets you configure **launch profiles** that can be used when running your app via `dotnet run`.                                                                                                                               |
| `Assets`                 | This folder contains your app's logo, images, and other media assets.                                                                                                                                                                   |
| `app.manifest`           | This app manifest file contains configuration related to the way that Windows displays your app when installed on user devices.                                                                                                         |
| `App.xaml`               | This markup file specifies the shared, globally accessible resources that your app depends on.                                                                                                                                          |
| `App.xaml.cs`            | This code-behind file represents the entry point to your app's business logic. It's responsible for creating and activating an instance of your `MainWindow`.                                                                           |
| `MainWindow.xaml`        | This markup file contains the presentation concerns for your app's main window.                                                                                                                                                         |
| `MainWindow.xaml.cs`     | This code-behind file contains the business logic concerns associated with your app's main window.                                                                                                                                      |
| `Package.appxmanifest`   | This [package manifest file](/uwp/schemas/appxpackage/uapmanifestschema/generate-package-manifest) lets you configure publisher information, logos, processor architectures, and other details that determine how your app appears in the Windows Store.                                                                                                               |


## Display "Hello world!"

To display "Hello world!" instead of the "Click me" button, navigate to `MainWindow.xaml`. You should see a `StackPanel` control's XAML markup:

```xml
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
   <Button x:Name="myButton" Click="myButton_Click">Click Me</Button>
</StackPanel>
```

> [!TIP]
> You'll frequently refer to **API reference docs** while building Windows apps. [StackPanel's reference docs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stackpanel) tell you more about the `StackPanel` control and how to customize it.

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

    // ↓ you can delete this ↓
    //private void myButton_Click(object sender, RoutedEventArgs e)
    //{
    //    myButton.Content = "Clicked";
    //}
}
```

If you restart your app, you should see a red `Hello world!`:

:::image type="content" source="images/hello-world/red-hello.png" alt-text="A red 'Hello world!'":::

## Update your app's title bar

Add `this.Title = "Hello world!";` to your `MainWindow.xaml.cs` code-behind file:

```csharp 
public MainWindow()
{
    this.InitializeComponent();
    this.Title = "Hello world!"; // <- this is new
}
```

If you restart your app, you should now see `Hello world!` in both the body and title bar:

:::image type="content" source="images/hello-world/red-hello-titled.png" alt-text="The 'Hello, world!' app we're building.":::

Congratulations! You've built your first Windows App SDK / WinUI 3 app.


## Recap

Here's what you accomplished in this how-to:

 1. You started with Visual Studio's **project template**.
 2. You experienced an **event handler** that bound a **`Button` control's** **`Click` event** to a UI update.
 3. You familiarized yourself with the **convention of separating presentation concerns** from **business logic** using tightly-coupled **XAML markup files** and **C# code-behind files**, respectively.
 4. You reviewed the default WinUI 3 project **file structure**.
 5. You modified both the presentation layer (XAML markup) and business logic (code-behind) to support a new **`TextBlock` control** within a **`StackPanel`**.
 6. You reviewed **reference docs** to better understand the **`StackPanel` control's properties**.
 7. You updated your main window's **title bar**.


## Full code files

<!--todo: embed from github -->

```xml MainWindow.xaml
<!-- MainWindow.xaml -->
<Window
    x:Class="Hello_World.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:Hello_World"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
        <TextBlock x:Name="myText" Text="Hello world!" Foreground="Red"/>
    </StackPanel>
</Window>
```

```csharp MainWindow.xaml.cs
// MainWindow.xaml.cs
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Hello_World
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "Hello world!";
        }
    }
}
```

```xml App.xaml
<!-- App.xaml -->
<Application
    x:Class="Hello_World.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:Hello_World">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
                <!-- Other merged dictionaries here -->
            </ResourceDictionary.MergedDictionaries>
            <!-- Other app resources here -->
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

```csharp App.xaml.cs
// App.xaml.cs
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Hello_World
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
```


## FAQ

**Q: What does "packaged" mean?**

Windows apps can be delivered to end-users using a variety of application packaging formats. When working with WinUI 3 / Windows App SDK, **packaged apps** use MSIX to bundle your app in a way that offers convenient installation and updates to end-users. Visit [Deployment architecture and overview for framework-dependent apps](../windows-app-sdk/deployment-architecture.md) to learn more.

**Q: Can I use VS Code to build WinUI 3 apps?**

Although technically possible, we strongly recommend using Visual Studio 2019 / 2022 to build desktop apps with WinUI 3 / Windows App SDK. See [the Windows developer FAQ](../get-started/windows-developer-faq.yml#do-i-need-to-use-visual-studio-to-build-winui-3-apps) for more information. 

**Q: Can I use C++ to build WinUI 3 apps?**

Yes! If you'd like to see this how-to updated with C++ guidance, [request an update on Github](https://github.com/MicrosoftDocs/windows-dev-docs/issues/new?title=Update%20request%3A%20How%20to%20build%20a%20hello%20world%20app&body=%28How%20can%20we%20help%3F%29&assignee=matchamatch) and we'll make it happen.


## Related

 - [Sample applications for Windows development](../get-started/samples.md)
 - [Windows developer FAQ](../get-started/windows-developer-faq.yml)
 - [Windows developer glossary](../get-started/windows-developer-glossary.md)
 - [Windows development best practices](../get-started/best-practices.md)
<!-- - [How to build a form with bidirectional data binding](todo) -->