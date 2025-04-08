---
title: Getting started with WinUI
description: How to install and use WinUI. 
ms.topic: article
ms.date: 09/14/2021
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
---

# Getting started with WinUI 2

[WinUI 2.8](release-notes/winui-2.8.md) is the latest stable version of WinUI that can be used for building production UWP applications (and desktop applications using [XAML Islands](../../desktop/modernize/xaml-islands/xaml-islands.md)).

The library is available as a NuGet package that can be added to any new or existing Visual Studio project.

> [!NOTE]
> For more information on building Windows desktop apps with the latest version of **WinUI 3**, see [WinUI 3](../index.md).

## Download and install WinUI

1. Download [Visual Studio 2022](https://developer.microsoft.com/windows/downloads) and ensure you choose the **Universal Windows Platform development** Workload in the Visual Studio installer.

2. Open an existing project, or create a new project using the Blank App template under Visual C# -> Windows -> Universal, or the appropriate template for your language projection.  

    > [!IMPORTANT]
    > To use WinUI 2.8, you must set TargetPlatformVersion >= 10.0.18362.0 and TargetPlatformMinVersion >= 10.0.17763.0 in the project properties.

3. In the Solution Explorer panel, right click on your project name and select **Manage NuGet Packages**. 

    :::image type="content" source="images/ManageNugetPackages.png" alt-text="Screenshot of the Solution Explorer panel with the project right-clicked and the Manage NuGet Packages option highlighted.":::<br/>*The Solution Explorer panel with the project right-clicked and the Manage NuGet Packages option highlighted.*

4. In the **NuGet Package Manager**, select the **Browse** tab and search for **Microsoft.UI.Xaml** or **WinUI**. Select which [WinUI NuGet Packages](nuget-packages.md) you want to use (the **Microsoft.UI.Xaml** package contains Fluent controls and features suitable for all apps). Click Install. 

    Check the "Include prerelease" checkbox to see the latest prerelease versions that include experimental new features.

    :::image type="content" source="images/NugetPackages.png" alt-text="Screenshot of the NuGet Package Manager dialog box showing the Browse tab with winui in the search field and Include prerelease checked.":::<br/>*The NuGet Package Manager dialog box showing the Browse tab with winui in the search field and Include prerelease checked.*

5. Add the WinUI Theme Resources to your App.xaml file.

    There are two ways to do this, depending on whether you have additional application resources.

    a. If you don't need other application resources, add the WinUI resources element `<XamlControlsResources` as shown in the following example:

    ``` XAML
    <Application
        x:Class="ExampleApp.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        RequestedTheme="Light">

        <Application.Resources>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
        </Application.Resources>

    </Application>
    ```

    b. If you have other resources, then we recommend you add those to `XamlControlsResources.MergedDictionaries`. This works with the platform's resource system to allow overrides of the `XamlControlsResources` resources.

    ``` XAML
    <Application
        x:Class="ExampleApp.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:controls="using:Microsoft.UI.Xaml.Controls"
        RequestedTheme="Light">

        <Application.Resources>
            <controls:XamlControlsResources>
                <controls:XamlControlsResources.MergedDictionaries>
                    <ResourceDictionary Source="/Styles/Styles.xaml"/>
                    <!-- Other app resources here -->
                </controls:XamlControlsResources.MergedDictionaries>
            </controls:XamlControlsResources>
        </Application.Resources>

    </Application>
    ```

6. Add a reference to the WinUI package to both XAML pages and/or code-behind pages.

    * In your XAML page, add a reference at the top of your page

        ```xaml
        xmlns:muxc="using:Microsoft.UI.Xaml.Controls"
        ```

    * In your code (if you want to use the type names without qualifying them), you can add a using directive.

        ```csharp
        using MUXC = Microsoft.UI.Xaml.Controls;
        ```

## Additional steps for a C++/WinRT project

When you add a NuGet package to a C++/WinRT project, the tooling generates a set of projection headers in your project's `\Generated Files\winrt` folder. To bring those headers files into your project, so that references to those new types resolve, you can go into your precompiled header file (typically `pch.h`) and include them. Below is an example that includes the generated header files for the **Microsoft.UI.Xaml** package.

```cppwinrt
// pch.h
...
#include <winrt/Microsoft.UI.Xaml.Automation.Peers.h>
#include <winrt/Microsoft.UI.Xaml.Controls.Primitives.h>
#include <winrt/Microsoft.UI.Xaml.Media.h>
#include <winrt/Microsoft.UI.Xaml.XamlTypeInfo.h>
...
```

For a full, step-by-step walkthrough of adding simple support for WinUI to a C++/WinRT project, see [A simple C++/WinRT WinUI example](/windows/uwp/cpp-and-winrt-apis/simple-winui-example).

## Contributing to WinUI

We welcome bug reports, feature requests and community code contributions in the [microsoft-ui-xaml repo](https://github.com/microsoft/microsoft-ui-xaml/issues) on GitHub.

## Other resources

If you're new to UWP, then we recommend that you visit the [Getting Started with UWP Development](https://developer.microsoft.com/windows/getstarted) pages on the Developer portal.
