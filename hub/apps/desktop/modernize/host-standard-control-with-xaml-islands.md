---
description: This article demonstrates how to host a standard WinRT XAML control in a WPF app by using XAML Islands.
title: Host a standard WinRT XAML control in a WPF app using XAML Islands
ms.date: 10/02/2020
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, xaml islands, wrapped controls, standard controls, InkCanvas, InkToolbar
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: 19H1
---

# Host a standard WinRT XAML control in a WPF app using XAML Islands

This article demonstrates two ways to use [XAML Islands](xaml-islands.md) to host a standard WinRT XAML control (that is, a first-party WinRT XAML control provided by the Windows SDK) in a WPF app that targets .NET Core 3.1:

* It shows how to host a UWP [InkCanvas](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas) and [InkToolbar](/uwp/api/windows.ui.xaml.controls.inktoolbar) controls by using [wrapped controls](xaml-islands.md#wrapped-controls) in the Windows Community Toolkit. These controls wrap the interface and functionality of a small set of useful WinRT XAML controls. You can add them directly to the design surface of your WPF or Windows Forms project and then use them like any other WPF or Windows Forms control in the designer.

* It also shows how to host a UWP [CalendarView](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) control by using the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control in the Windows Community Toolkit. Because only a small set of WinRT XAML controls are available as wrapped controls, you can use [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) to host any other standard WinRT XAML control.

Although this article demonstrates how to host WinRT XAML controls in a WPF app, the process is similar for a Windows Forms app.

> [!NOTE]
> Using XAML Islands to host WinRT XAML controls in WPF and Windows Forms apps is currently supported only in apps that target .NET Core 3.x. XAML Islands are not yet supported in apps that target .NET 5, or in apps that target any version of the .NET Framework.

## Required components

To host a WinRT XAML control in a WPF (or Windows Forms) app, you'll need the following components in your solution. This article provides instructions for creating each of these components.

* **The project and source code for your app**. Using the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control to host WinRT XAML controls is currently supported only in apps that target .NET Core 3.x.

* **A UWP app project that defines a root Application class that derives from XamlApplication**. Your WPF or Windows Forms project must have access to an instance of the [Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Win32.UI.XamlApplication) class provided by the Windows Community Toolkit so that it can discover and load custom UWP XAML controls. The recommended way to do this is to define this object in a separate UWP app project that is part of the solution for your WPF or Windows Forms app. 

    > [!NOTE]
    > Although the `XamlApplication` object isn't required for hosting a first-party WinRT XAML control, your app needs this object to support the full range of XAML Island scenarios, including hosting custom WinRT XAML controls. Therefore, we recommend that you always define a `XamlApplication` object in any solution in which you are using XAML Islands.

    > [!NOTE]
    > Your solution can contain only one project that defines a `XamlApplication` object. The project that defines the `XamlApplication` object must include references to all other libraries and projects that are used to host WinRT XAML controls on the XAML Island.

## Create a WPF project

Before getting started, follow these instructions to create a WPF project and configure it to host XAML Islands. If you have an existing WPF project, you can adapt these steps and code examples for your project.

1. In Visual Studio 2019, create a new **WPF App (.NET Core)** project. You must first install the latest version of the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/current) if you haven't done so already.

2. Make sure [package references](/nuget/consume-packages/package-references-in-project-files) are enabled:

    1. In Visual Studio, click **Tools -> NuGet Package Manager -> Package Manager Settings**.
    2. Make sure **PackageReference** is selected for **Default package management format**.

3. Right-click your WPF project in **Solution Explorer** and choose **Manage NuGet Packages**.

4. Select the **Browse** tab, search for the [Microsoft.Toolkit.Wpf.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls) package and install the latest stable version. This package provides everything you need to use the wrapped WinRT XAML controls for WPF (including [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) and the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control.
    > [!NOTE]
    > Windows Forms apps must use the [Microsoft.Toolkit.Forms.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls) package.

5. Configure your solution to target a specific platform such as x86 or x64. Most XAML Islands scenarios are not supported in projects that target **Any CPU**.

    1. In **Solution Explorer**, right-click the solution node and select **Properties** -> **Configuration Properties** -> **Configuration Manager**. 
    2. Under **Active solution platform**, select **New**. 
    3. In the **New Solution Platform** dialog, select **x64** or **x86** and press **OK**. 
    4. Close the open dialog boxes.

## Define a XamlApplication class in a UWP app project

Next, add a UWP app project to your solution and revise the default `App` class in this project to derive from the [Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Win32.UI.XamlApplication) class provided by the Windows Community Toolkit. This class supports the [IXamlMetadaraProvider](/uwp/api/Windows.UI.Xaml.Markup.IXamlMetadataProvider) interface, which enables your app to discover and load metadata for custom UWP XAML controls in assemblies in the current directory of your application at run time. This class also initializes the UWP XAML framework for the current thread.

> [!NOTE]
> Although this step isn't required for hosting a first-party WinRT XAML control, your app needs the `XamlApplication` object to support the full range of XAML Island scenarios, including hosting custom WinRT XAML controls. Therefore, we recommend that you always define a `XamlApplication` object in any solution in which you are using XAML Islands.

1. In **Solution Explorer**, right-click the solution node and select **Add** -> **New Project**.
2. Add a **Blank App (Universal Windows)** project to your solution. Make sure the target version and minimum version are both set to **Windows 10, version 1903 (Build 18362)** or a later release. Also make sure this new UWP project is not in a subfolder of the WPF project. Otherwise, the WPF app will later try to build the UWP XAML markup as if it were WPF XAML.
3. In the UWP app project, install the [Microsoft.Toolkit.Win32.UI.XamlApplication](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.XamlApplication) NuGet package (latest stable version).
4. Open the **App.xaml** file and replace the contents of this file with the following XAML. Replace `MyUWPApp` with the namespace of your UWP app project.

    ```xml
    <xaml:XamlApplication
        x:Class="MyUWPApp.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:xaml="using:Microsoft.Toolkit.Win32.UI.XamlHost"
        xmlns:local="using:MyUWPApp">
    </xaml:XamlApplication>
    ```

5. Open the **App.xaml.cs** file and replace the contents of this file with the following code. Replace `MyUWPApp` with the namespace of your UWP app project.

    ```csharp
    namespace MyUWPApp
    {
        public sealed partial class App : Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication
        {
            public App()
            {
                this.Initialize();
            }
        }
    }
    ```

6. Delete the **MainPage.xaml** file from the UWP app project.
7. Build the UWP app project.

## Add a reference to the UWP project in your WPF project

1. Specify the compatible framework version in the WPF project file. 

    1. In **Solution Explorer**, double-click the WPF project node to open the project file in the editor.
    2. In the first **PropertyGroup** element, add the following child element. Change the `19041` portion of the value as necessary to match the target and minimum OS build of the UWP project.

        ```xml
        <AssetTargetFallback>uap10.0.19041</AssetTargetFallback>
        ```

        When you're done, the **PropertyGroup** element should look similar to this.

        ```xml
        <PropertyGroup>
            <OutputType>WinExe</OutputType>
            <TargetFramework>netcoreapp3.1</TargetFramework>
            <UseWPF>true</UseWPF>
            <Platforms>AnyCPU;x64</Platforms>
            <AssetTargetFallback>uap10.0.19041</AssetTargetFallback>
        </PropertyGroup>
        ```

2. In **Solution Explorer**, right-click the **Dependencies** node under the WPF project and add a reference to your UWP app project.

## Instantiate the XamlApplication object in the entry point of your WPF app

Next, add code to the entry point for your WPF app to create an instance of the `App` class you just defined in the UWP project (this is the class that now derives from `XamlApplication`).

1. In your WPF project, right-click the project node, select **Add** -> **New Item**, and then select **Class**. Name the class **Program** and click **Add**.

2. Replace the generated `Program` class with the following code and then save the file. Replace `MyUWPApp` with the namespace of your UWP app project, and replace `MyWPFApp` with the namespace of your WPF app project.

    ```csharp
    public class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            using (new MyUWPApp.App())
            {
                MyWPFApp.App app = new MyWPFApp.App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }
    ```

3. Right-click the project node and choose **Properties**.

4. On the **Application** tab of the properties, click the **Startup object** drop-down and choose the fully qualified name of the `Program` class you added in the previous step. 
    > [!NOTE]
    > By default, WPF projects define a `Main` entry point function in a generated code file that isn't intended to be modified. This step changes the entry point for your project to the `Main` method of the new `Program` class, which enables you to add code that runs as early in the startup process of the app as possible. 

5. Save your changes to the project properties.

## Host an InkCanvas and InkToolbar by using wrapped controls

Now that you have configured your project to use UWP XAML Islands, you are now ready to add the [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) wrapped WinRT XAML controls to the app.

1. In your WPF project, open the **MainWindow.xaml** file.

2. In the **Window** element near the top of the XAML file, add the following attribute. This references the XAML namespace for the [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) wrapped WinRT XAML control.

    ```xml
    xmlns:Controls="clr-namespace:Microsoft.Toolkit.Wpf.UI.Controls;assembly=Microsoft.Toolkit.Wpf.UI.Controls"
    ```

    After adding this attribute, the **Window** element should now look similar to this.

    ```xml
    <Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:local="clr-namespace:WpfApp"
            xmlns:Controls="clr-namespace:Microsoft.Toolkit.Wpf.UI.Controls;assembly=Microsoft.Toolkit.Wpf.UI.Controls"
            x:Class="WpfApp.MainWindow"
            mc:Ignorable="d"
            Title="MainWindow" Height="800" Width="800">
    ```

3. In the **MainWindow.xaml** file, replace the existing `<Grid>` element with the following XAML. This XAML adds an [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) control (prefixed by the **Controls** keyword you defined earlier as a namespace) to the `<Grid>`.

    ```xml
    <Grid Margin="10,50,10,10">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Controls:InkToolbar x:Name="myInkToolbar" TargetInkCanvas="{x:Reference myInkCanvas}" Grid.Row="0" Width="300"
            Height="50" Margin="10,10,10,10" HorizontalAlignment="Left" VerticalAlignment="Top" />
        <Controls:InkCanvas x:Name="myInkCanvas" Grid.Row="1" HorizontalAlignment="Left" Width="600" Height="400"
            Margin="10,10,10,10" VerticalAlignment="Top" />
    </Grid>
    ```

    > [!NOTE]
    > You can also add these and other wrapped controls to the window by dragging them from the **Windows Community Toolkit** section of the **Toolbox** to the designer.

4. Save the **MainWindow.xaml** file.

    If you have a device which supports a digital pen, like a Surface, and you're running this lab on a physical machine, you could now build and run the app and draw digital ink on the screen with the pen. However, if you don't have a pen capable device and you try to sign with your mouse, nothing will happen. This is happening because the **InkCanvas** control is enabled only for digital pens by default. However, you can change this behavior.

5. Open the **MainWindow.xaml.cs** file.

6. Add the following namespace declaration at the top of the file:

    ```csharp
    using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
    ```

7. Locate the `MainWindow()` constructor. Add the following line of code after the `InitializeComponent()` method and save the code file.

    ```csharp
    this.myInkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen;
    ```

    You can use the **InkPresenter** object to customize the default inking experience. This code uses the **InputDeviceTypes** property to enable mouse as well as pen input.

8. Press F5 again to rebuild and run the app in the debugger. If you're using a computer with a mouse, confirm that you can draw something in the ink canvas space with the mouse.

## Host a CalendarView by using the host control

Now that you have added the [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) wrapped WinRT XAML controls to the app, you are now ready to use the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control to add a [CalendarView](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) to the app.

> [!NOTE]
> The [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control is provided by the [Microsoft.Toolkit.Wpf.UI.XamlHost](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.XamlHost) package. This package is included with the [Microsoft.Toolkit.Wpf.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls) package you installed earlier.

1. In **Solution Explorer**, open the **MainWindow.xaml** file.

2. In the **Window** element near the top of the XAML file, add the following attribute. This references the XAML namespace for the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control.

    ```xml
    xmlns:xamlhost="clr-namespace:Microsoft.Toolkit.Wpf.UI.XamlHost;assembly=Microsoft.Toolkit.Wpf.UI.XamlHost"
    ```

    After adding this attribute, the **Window** element should now look similar to this.

    ```xml
    <Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:local="clr-namespace:WpfApp"
            xmlns:Controls="clr-namespace:Microsoft.Toolkit.Wpf.UI.Controls;assembly=Microsoft.Toolkit.Wpf.UI.Controls"
            xmlns:xamlhost="clr-namespace:Microsoft.Toolkit.Wpf.UI.XamlHost;assembly=Microsoft.Toolkit.Wpf.UI.XamlHost"
            x:Class="WpfApp.MainWindow"
            mc:Ignorable="d"
            Title="MainWindow" Height="800" Width="800">
    ```

4. In the **MainWindow.xaml** file, replace the existing `<Grid>` element with the following XAML. This XAML adds a row to the grid and adds a [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) object to the last row. To host a UWP [CalendarView](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) control, this XAML sets the `InitialTypeName` property to the fully qualified name of the control. This XAML also defines an event handler for the `ChildChanged` event, which is raised when the hosted control has been rendered.

    ```xml
    <Grid Margin="10,50,10,10">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Controls:InkToolbar x:Name="myInkToolbar" TargetInkCanvas="{x:Reference myInkCanvas}" Grid.Row="0" Width="300"
            Height="50" Margin="10,10,10,10" HorizontalAlignment="Left" VerticalAlignment="Top" />
        <Controls:InkCanvas x:Name="myInkCanvas" Grid.Row="1" HorizontalAlignment="Left" Width="600" Height="400" 
            Margin="10,10,10,10" VerticalAlignment="Top" />
        <xamlhost:WindowsXamlHost x:Name="myCalendar" InitialTypeName="Windows.UI.Xaml.Controls.CalendarView" Grid.Row="2" 
              Margin="10,10,10,10" Width="600" Height="300" ChildChanged="MyCalendar_ChildChanged"  />
    </Grid>
    ```

5. Save the **MainWindow.xaml** file and open the **MainWindow.xaml.cs** file.

7. Add the following namespace declaration at the top of the file:

    ```csharp
    using Microsoft.Toolkit.Wpf.UI.XamlHost;
    ```

10. Add the following `ChildChanged` event handler method to the `MainWindow` class and save the code file. When the host control has been rendered, this event handler runs and creates a simple event handler for the `SelectedDatesChanged` event of the calendar control.

    ```csharp
    private void MyCalendar_ChildChanged(object sender, EventArgs e)
    {
        WindowsXamlHost windowsXamlHost = (WindowsXamlHost)sender;

        Windows.UI.Xaml.Controls.CalendarView calendarView =
            (Windows.UI.Xaml.Controls.CalendarView)windowsXamlHost.Child;

        if (calendarView != null)
        {
            calendarView.SelectedDatesChanged += (obj, args) =>
            {
                if (args.AddedDates.Count > 0)
                {
                    MessageBox.Show("The user selected a new date: " + 
                        args.AddedDates[0].DateTime.ToString());
                }
            };
        }
    }
    ```

11. Press F5 again to rebuild and run the app in the debugger. Confirm that the calendar control now displays on the bottom of the window.

## Package the app

You can optionally package the WPF app in an [MSIX package](/windows/msix) for deployment. MSIX is the modern app packaging technology for Windows, and it is based on a combination of MSI, .appx, App-V and ClickOnce installation technologies.

The following instructions show you how to package the all the components in the solution in an MSIX package by using the [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) in Visual Studio 2019. These steps are necessary only if you want to package the WPF app in an MSIX package.

> [!NOTE]
> If you choose to not package your application in an [MSIX package](/windows/msix) for deployment, computers that run your app must have the [Visual C++ Runtime](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

1. Add a new [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to your solution. As you create the project, select the same **Target version** and **Minimum version** as you selected for the UWP project.

2. In the packaging project, right-click the **Applications** node and choose **Add reference**. In the list of projects, select the WPF project in your solution and click **OK**.

    > [!NOTE]
    > If you want to publish your app in the Microsoft Store, you have to add reference to the UWP project in the packaging project.

3. Configure your solution to target a specific platform such as x86 or x64. This is required to build the WPF app into an MSIX package using the Windows Application Packaging Project.

    1. In **Solution Explorer**, right-click the solution node and select **Properties** -> **Configuration Properties** -> **Configuration Manager**.
    2. Under **Active solution platform**, select **x64** or **x86**.
    3. In the row for your WPF project, in the **Platform** column select **New**.
    4. In the **New Solution Platform** dialog, select **x64** or **x86** (the same platform you selected for **Active solution platform**) and click **OK**.
    5. Close the open dialog boxes.

5. Build and run the packaging project. Confirm that the WPF runs and the UWP control displays as expected.

## Related topics

* [Host UWP XAML controls in desktop apps (XAML Islands)](xaml-islands.md)
* [XAML Islands code samples](https://github.com/microsoft/Xaml-Islands-Samples)
* [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas)
* [InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar)
* [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost)
