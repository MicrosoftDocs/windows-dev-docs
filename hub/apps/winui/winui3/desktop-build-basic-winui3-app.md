---
description: Build .NET and C++ desktop (Win32) apps with a WinUI 3 UI.
title: Build a basic WinUI 3 app for desktop
ms.date: 03/19/2021
ms.topic: article
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, Windows UI Library
ms.author: kbridge
author: Karl-Bridge-Microsoft
ms.localizationpriority: high
ms.custom: 19H1
---

# Build a basic WinUI 3 desktop app

In this article, we step through how to build basic C# .NET 5 and C++ desktop (Win32) applications with user interfaces composed entirely of WinUI 3 controls and features.

## Prerequisites

- [Set up your development environment](../../windows-app-sdk/set-up-your-development-environment.md).
- Test your configuration by [creating your first WinUI 3 project for C# and .NET 5](create-your-first-winui3-app.md).

    :::image type="content" source="images/build-basic/template-app.png" alt-text="The initial template app, running.":::<br/>
    *The initial template app, running.*

## Initial template app

We're going to start building from the initial template application, but before we start, lets have a look at the structure and content of the template app.

### The application solution

By default, the solution contains two projects: The application itself and another for creating an [MSIX](/windows/msix) app package.

:::image type="content" source="images/build-basic/template-app-solution-explorer.png" alt-text="Solution Explorer showing the file structure of the initial template app.":::<br/>
*Solution Explorer showing the file structure of the initial template app.*

### The application project

Now, double-click the application project file (or right click and select "Edit project file"), which opens the file in an XML text editor.

The following is the project file from the initial template app, which includes two items of note:

- The `TargetFramework` is [.NET 5](/dotnet/core/dotnet-five), the primary implementation of .NET going forward.
- The NuGet `PackageReference` elements for the various Windows App SDK features, including WinUI.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    <RootNamespace>WinUIApp2</RootNamespace>
    <Platforms>x86;x64;arm64</Platforms>
    <RuntimeIdentifiers>win10-x86;win10-x64;win10-arm64</RuntimeIdentifiers>
    <NoWin32Manifest>true</NoWin32Manifest>
    <ApplicationIcon />
    <StartupObject />
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.ProjectReunion" Version="0.5.0-prerelease" />
    <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.5.0-prerelease" />
    <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.5.0-prerelease" />
    <Manifest Include="$(ApplicationManifest)" />
  </ItemGroup>
</Project>
```

### The MainWindow.xaml file

With WinUI 3, you can now create instances of the [Window](/windows/winui/api/microsoft.ui.xaml.window) class in XAML markup.

The XAML Window class was extended to support desktop windows, turning it into an abstraction of each of the low-level window implementations used by the UWP and desktop app models. Specifically, UWP uses CoreWindow, while Win32 uses window handles (or HWNDs) and corresponding Win32 APIs.

The following code example is the MainWindow.xaml file from the initial template app, which uses the Window class as the root element for the app.

```xaml
<Window
    x:Class="WinUIApp2.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:WinUIApp2"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
        <Button x:Name="myButton" Click="MyButton_Click">Click Me</Button>
    </StackPanel>
</Window>
```

## Basic managed C#/.NET 5 app

For this first example, let's use the [ShowWindow](/windows/win32/api/winuser/nf-winuser-showwindow) API from User32.dll to programmatically maximize the Window.

1. First, to call Win32 APIs from User32.dll, add the **PInvoke.User32** NuGet package to your project (from the Visual Studio menus, select **Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution...**). For more details, see [Calling Native Functions from Managed Code](/cpp/dotnet/calling-native-functions-from-managed-code).
1. Once you have added the package, open the MainWindow.xaml.cs code-behind file and replace the `MyButton_Click` event handler with the following code:

    ```csharp
            private void MyButton_Click(object sender, RoutedEventArgs e)
            {
                myButton.Content = "Clicked";
    
                IntPtr hwnd = (App.Current as App).MainWindowWindowHandle;
                PInvoke.User32.ShowWindow(hwnd, PInvoke.User32.WindowShowStyle.SW_MAXIMIZE);
            }
    ```

    The [PInvoke](/dotnet/standard/native-interop/pinvoke) [ShowWindow](/windows/win32/api/winuser/nf-winuser-showwindow) method uses a window handle (`hwnd`) as the first parameter and uses the second parameter to specify that the window should be maximized. 

1. To get a window handle, use the [GetActiveWindow](/windows/win32/api/winuser/nf-winuser-getactivewindow) method. This method returns the window handle of the current active Window (call this method after activating the target Window).

    The `MainWindow` object (see MainWindow.xaml) is created, instantiated, and activated in the `OnLaunched` event handler found in the App.xaml.cs code-behind file. Replace the `OnLaunched` event with the following code:

    ```csharp
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
            m_windowhandle = PInvoke.User32.GetActiveWindow();
        }

        IntPtr m_windowhandle;
        public IntPtr MainWindowWindowHandle { get { return m_windowhandle; } }
    ```

1. Compile and run the app.
1. Finally, press the "Click me" button and the app window should maximize.

## "Full-trust" desktop app

WinUI 3 provides the ability for applications to run with "full trust permission", outside of the security sandbox of an [AppContainer](/windows/win32/secauthz/appcontainer-for-legacy-applications-).

WinUI 3 desktop applications can call .NET 5 APIs without restriction. In the following example (derived from the initial template app used in the previous example) we show how you can query the current process and get a list of all loaded modules (not possible with UWP apps).

1. In the MainWindow.xaml file, add a [ContentDialog](/windows/winui/api/microsoft.ui.xaml.controls.contentdialog) element.

    ```xaml
    <StackPanel 
        Orientation="Horizontal" 
        HorizontalAlignment="Center" 
        VerticalAlignment="Center">
        <Button x:Name="myButton" Click="MyButton_Click">Click Me</Button>
        <ContentDialog x:Name="contentDialog" CloseButtonText="Close">
            <StackPanel>
                <TextBlock Name="cdTextBlock"/>
            </StackPanel>
        </ContentDialog>
    </StackPanel>
    ```

1. In the MainWindow.xaml.cs code-behind file, call the .NET APIs from [System.Diagnostics](/dotnet/api/system.diagnostics) to get the modules loaded in the [current process](/dotnet/api/system.diagnostics.process.getcurrentprocess). For this example, we just iterate through each [ProcessModule](/dotnet/api/system.diagnostics.processmodule) in the [Process.Modules](/dotnet/api/system.diagnostics.process.modules) object.

    ```csharp
    private async void MyButton_Click(object sender, RoutedEventArgs e)
    {
        myButton.Content = "Clicked";

        var description = new System.Text.StringBuilder();
        var process = System.Diagnostics.Process.GetCurrentProcess();
        foreach (System.Diagnostics.ProcessModule module in process.Modules)
        {
            description.AppendLine(module.FileName);
        }

        cdTextBlock.Text = description.ToString();
        await contentDialog.ShowAsync();
    }
    ```
1. Compile and run the app.
1. Finally, press the "Click me" button and the dialog should appear with the list of processes.

    :::image type="content" source="images/build-basic/template-app-full-trust.png" alt-text="Example of a full-trust application.":::<br/>*Example of a full-trust application.*

## Summary

In this topic we covered accessing the underlying window implementation, in this case Win32 and HWNDs, and use Win32 APIs in your app along with the WinRT APIs of Windows 10 and later. This lets you use much of your existing desktop application code when creating new WinUI 3 desktop apps.

We also covered how you can use the .NET 5 APIs with WinUI 3 as your UI Framework.

## See also

- [Windows UI Library 3 - Project Reunion 0.5 (March 2021)](index.md)
- [Get started with the Windiows App SDK](../../windows-app-sdk/get-started.md)
- [Get started with WinUI 3 for desktop apps](./create-your-first-winui3-app.md)