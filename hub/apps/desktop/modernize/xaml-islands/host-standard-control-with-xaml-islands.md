---
title: Use XAML Islands to host a UWP XAML control in a C# WPF app
description: This topic demonstrates two ways to use XAML Islands to host a Universal Windows Platform (UWP) XAML control (that is, a first-party control provided by the Windows SDK) in a Windows Presentation Foundation (WPF) app that targets .NET Core 3.1.
ms.date: 02/24/2022
ms.topic: article
keywords: windows 11, windows 10, uwp, wpf, windows forms, xaml islands, wrapped controls, standard controls, InkCanvas, InkToolbar
ms.localizationpriority: medium
---

# Use XAML Islands to host a UWP XAML control in a C# WPF app

> [!IMPORTANT]
> This topic uses or mentions types from the [CommunityToolkit/Microsoft.Toolkit.Win32](https://github.com/CommunityToolkit/Microsoft.Toolkit.Win32) GitHub repo. For important info about XAML Islands support, please see the [XAML Islands Notice](https://github.com/CommunityToolkit/Microsoft.Toolkit.Win32#xaml-islands-notice) in that repo.

This topic shows how to build a C# Windows Presentation Foundation (WPF) app (targeting .NET Core 3.1) that uses [XAML Islands](xaml-islands.md) to host a Universal Windows Platform (UWP) XAML control (that is, a first-party control provided by the Windows SDK). We show how to do that in two ways:

* We show how to host UWP [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas) and [**InkToolbar**](/uwp/api/windows.ui.xaml.controls.inktoolbar) controls by using [wrapped controls](xaml-islands.md#wrapped-controls) (available in the Windows Community Toolkit). Wrapped controls wrap the interface and functionality of a small set of useful UWP XAML controls. You can add a wrapped control directly to the design surface of your WPF or Windows Forms project, and then use it in the designer like any other WPF or Windows Forms control.

* We also show how to host a UWP [**CalendarView**](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) control by using the [**WindowsXamlHost**](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control (available in the Windows Community Toolkit). Because only a small set of UWP XAML controls are available as wrapped controls, you can use **WindowsXamlHost** to host any UWP XAML control.

The process for hosting a UWP XAML control in a WPF app is similar for a Windows Forms app.

> [!IMPORTANT]
> Using XAML Islands (wrapped controls or [**WindowsXamlHost**](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost)) to host UWP XAML controls is supported only in apps that target .NET Core 3.x. XAML Islands are not supported in apps that target .NET, or in apps that target any version of the .NET Framework.

## Recommended components

To host a UWP XAML control in a WPF or Windows Forms app, we recommend that you have the following components in your solution. This topic provides instructions for creating each of these components:

* **The project and source code for your WPF or Windows Forms app**.

* **A UWP project that defines a root Application class that derives from XamlApplication**. The [**Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication**](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Win32.UI.XamlApplication) class is available in the Windows Community Toolkit). We recommended that you define your **XamlApplication**-derived **Application** class a separate UWP app project that's part of your WPF or Windows Forms Visual Studio solution.

    > [!NOTE]
    > Making a **XamlApplication**-derived object available to your WPF or Windows Forms project isn't actually required in order to host a first-party UWP XAML control. But it *is* necessary in order to discover, load, and host custom UWP XAML controls. So&mdash;to support the full range of XAML Island scenarios&mdash;we recommend that you always define a **XamlApplication**-derived object in any solution in which you use XAML Islands.

    > [!NOTE]
    > Your solution can contain only one project that defines a **XamlApplication**-derived object. That one project must reference any other libraries and projects that host UWP XAML controls via XAML Islands.

## Create a WPF project

You can follow these instructions to create a new WPF project, and configure it to host XAML Islands. If you have an existing WPF project, then you can adapt these steps and code examples for your project.

1. If you haven't done so already, then install the latest version of [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1).

1. In Visual Studio, create a new C# project from the **WPF Application** project template. Set the **Project name** to *MyWPFApp* so that you won't need to edit any of the steps or source code in this topic. Set the **Framework** to *.NET Core 3.1**, and click **Create**.

> [!IMPORTANT]
> Be sure not to use the **WPF App (.NET Framework)** project template.

## Configure your WPF project

1. These steps enable [package references](/nuget/consume-packages/package-references-in-project-files):

    1. In Visual Studio, click **Tools** > **NuGet Package Manager** > **Package Manager Settings**.
    1. On the right, find the **Package Management** > **Default package management format** setting, and set it to **PackageReference**.

1. Use these steps to install the [**Microsoft.Toolkit.Wpf.UI.Controls**](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls) NuGet Package:

    1. Right-click the *MyWPFApp* project node in **Solution Explorer**, and choose **Manage NuGet Packages...**.

    1. On the **Browse** tab, type or paste *Microsoft.Toolkit.Wpf.UI.Controls* into the search box. Select the latest stable version, and click **Install**. That package provides everything you need to use the wrapped UWP XAML controls for WPF (including [**InkCanvas**](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas), [**InkToolbar**](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar), and the [**WindowsXamlHost**](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control).

    > [!NOTE]
    > For a Windows Forms app, reference the [**Microsoft.Toolkit.Forms.UI.Controls**](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls) package instead.

1. Most XAML Islands scenarios aren't supported in projects that target **Any CPU**. So to target a specific architecture (such as **x86** or **x64**), do the following:

    1. Right-click the solution node (*not* the project node) in **Solution Explorer**, and choose **Properties**.
    1. Select **Configuration Properties** on the left.
    1. Click the **Configuration Manager...** button.
    1. Under **Active solution platform**, select **New**. 
    1. In the **New Solution Platform** dialog, select **x64** or **x86**, and press **OK**. 
    1. Close the open dialog boxes.

## Define a XamlApplication class in a new UWP project

In this section we'll be adding a UWP project to the solution, and revising the default **App** class in that project to derive from the [**Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication**](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Win32.UI.XamlApplication) class provided by the Windows Community Toolkit. That class supports the [**IXamlMetadataProvider**](/uwp/api/Windows.UI.Xaml.Markup.IXamlMetadataProvider) interface, which enables your app to discover and load metadata for custom UWP XAML controls in assemblies in the current directory of your application at run time. That class also initializes the UWP XAML framework for the current thread.

1. In **Solution Explorer**, right-click the solution node, and select **Add** > **New Project...**.

1. Select the C# **Blank App (Universal Windows)** project template. Set the **Project name** to *MyUWPApp* so that you won't need to edit any of the steps or source code in this topic. Set the **Target version** and **Minimum version** to either **Windows 10, version 1903 (Build 18362)** or later.

    > [!NOTE]
    > Be sure not to create *MyUWPApp* in a subfolder of *MyWPFApp*. If you do that, then *MyWPFApp* will try to build the UWP XAML markup as if it were WPF XAML.

1. In *MyUWPApp*, install the [**Microsoft.Toolkit.Win32.UI.XamlApplication**](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.XamlApplication) NuGet package (latest stable version). The process for installng a NuGet package was described in the previous section.

1. In *MyUWPApp*, open the `App.xaml` file, and replace its contents with the following XAML:

    ```xml
    <xaml:XamlApplication
        x:Class="MyUWPApp.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:xaml="using:Microsoft.Toolkit.Win32.UI.XamlHost"
        xmlns:local="using:MyUWPApp">
    </xaml:XamlApplication>
    ```

1. Similarly, open `App.xaml.cs`, and replace its contents with the following code:

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

1. Delete the `MainPage.xaml` and `MainPage.xaml.cs` files.
1. Build the *MyUWPApp* project.

## In MyWPFApp, add a reference to the MyUWPApp project

1. Specify the compatible framework version in *MyWPFApp*'s project file like this:

    1. In **Solution Explorer**, click the *MyWPFApp* project node to open the project file in the editor.
    1. Inside the first **PropertyGroup** element, add the following child element. Change the `19041` portion of the value as necessary to match the target and minimum OS build of the *MyWPFApp* project.

        ```xml
        <AssetTargetFallback>uap10.0.19041</AssetTargetFallback>
        ```

1. In **Solution Explorer**, right-click *MyWPFApp* > **Dependencies**, choose **Add Project Reference...**, and add a reference to the *MyUWPApp* project.

## Instantiate the XamlApplication object in the entry point of MyWPFApp

Next, add code to the entry point of *MyWPFApp* to create an instance of the **App** class that you just defined in *MyUWPApp* (the class that derives from **XamlApplication**).

1. Right-click the *MyWPFApp* project node, select **Add** > **New Item...**, and then select **Class**. Set **Name** to *Program.cs*, and click **Add**.

1. Replace the contents of `Program.cs` with the following XAML (and then save the file and build the *MyWPFApp* project):

    ```csharp
    namespace MyWPFApp
    {
        public class Program
        {
            [System.STAThreadAttribute()]
            public static void Main()
            {
                using (new MyUWPApp.App())
                {
                    var app = new MyWPFApp.App();
                    app.InitializeComponent();
                    app.Run();
                }
            }
        }
    }
    ```

1. Right-click the *MyWPFApp* project node, and choose **Properties**.

1. In **Application** > **General**, click the **Startup object** drop-down, and choose *MyWPFApp.Program* (which is the fully-qualified name of the **Program** class that you just added). If you don't see it, then try closing and reopening Visual Studio.

    > [!NOTE]
    > By default, a WPF project defines a **Main** entry point function in a generated code file that isn't intended to be modified. The step above changes the entry point for your project to the **Main** method of the new **Program** class, which enables you to add code that runs as early in the startup process of the app as possible.

1. Save your changes to the project properties.

## Use wrapped controls to host an InkCanvas and InkToolbar

Now that you've configured your project to use UWP XAML Islands, you're ready to add the [**InkCanvas**](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [**InkToolbar**](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) wrapped UWP XAML controls to the app.

1. In *MyWPFApp*, open the `MainWindow.xaml` file.

1. In the **Window** element near the top of the XAML file, add the following attribute. This attribute references the XAML namespace for the **InkCanvas** and **InkToolbar** wrapped UWP XAML controls, and maps it to the **controls** XML namespace.

    ```xml
    xmlns:controls="clr-namespace:Microsoft.Toolkit.Wpf.UI.Controls;assembly=Microsoft.Toolkit.Wpf.UI.Controls"
    ```

1. Still in `MainWindow.xaml`, edit the existing **Grid** element so that it looks like the XAML below. This XAML adds to the **Grid** an [**InkCanvas**](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and an [**InkToolbar**](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) control (prefixed by the **controls** XML namespace that you defined in the previous step).

    ```xml
    <Grid Margin="10,50,10,10">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <controls:InkToolbar x:Name="myInkToolbar" TargetInkCanvas="{x:Reference myInkCanvas}" Grid.Row="0" Width="300"
            Height="50" Margin="10,10,10,10" HorizontalAlignment="Left" VerticalAlignment="Top" />
        <controls:InkCanvas x:Name="myInkCanvas" Grid.Row="1" HorizontalAlignment="Left" Width="600" Height="400"
            Margin="10,10,10,10" VerticalAlignment="Top" />
    </Grid>
    ```

    > [!NOTE]
    > You can also add these and other wrapped controls to the **Window** by dragging them to the designer from the **Windows Community Toolkit** section of the **Toolbox**.

1. Save `MainWindow.xaml`.

    If you have a device that supports a digital pen (such as a Surface), and you're following along on a physical machine, then you could now build and run the app, and draw digital ink on the screen with the pen. But if you try to write with your mouse, then nothing will happen, because by default **InkCanvas** is enabled only for digital pens. Here's how to enable **InkCanvas** for the mouse.

1. Still in *MyWPFApp*, open `MainWindow.xaml.cs`.

1. Add the following namespace directive to the top of the file:

    ```csharp
    using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
    ```

1. Locate the **MainWindow** constructor. Immediately after the call to **InitializeComponent**, add the following line of code:

    ```csharp
    myInkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen;
    ```

    You can use the **InkPresenter** object to customize the default inking experience. The code above uses the **InputDeviceTypes** property to enable mouse as well as pen input.

1. Save, build, and run. If you're using a computer with a mouse, then confirm that you can draw something in the ink canvas space with the mouse.

## Host a CalendarView by using the host control

In this section, we'll use the [**WindowsXamlHost**](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control to add a [**CalendarView**](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) to the app.

> [!NOTE]
> The [**WindowsXamlHost**](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control is provided by the [**Microsoft.Toolkit.Wpf.UI.XamlHost**](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.XamlHost) package. That package is included with the [**Microsoft.Toolkit.Wpf.UI.Controls**](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls) package that you installed earlier.

1. In **Solution Explorer**, in *MyWPFApp*, open the `MainWindow.xaml` file.

1. In the **Window** element near the top of the XAML file, add the following attribute. This attribute references the XAML namespace for the **WindowsXamlHost** control, and maps it to the **xamlhost** XML namespace.

    ```xml
    xmlns:xamlhost="clr-namespace:Microsoft.Toolkit.Wpf.UI.XamlHost;assembly=Microsoft.Toolkit.Wpf.UI.XamlHost"
    ```

1. Still in `MainWindow.xaml`, edit the existing **Grid** element so that it looks like the XAML below. This XAML adds to the **Grid** a [**WindowsXamlHost**](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control (prefixed by the **xamlhost** XML namespace that you defined in the previous step). To host a UWP [**CalendarView**](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) control, this XAML sets the **InitialTypeName** property to the fully-qualified name of the control. The XAML also defines an event handler for the **ChildChanged** event, which is raised when the hosted control has been rendered.

    ```xml
    <Grid Margin="10,50,10,10">
        <xamlhost:WindowsXamlHost x:Name="myCalendar" InitialTypeName="Windows.UI.Xaml.Controls.CalendarView"
              Margin="10,10,10,10" Width="600" Height="300" ChildChanged="MyCalendar_ChildChanged" />
    </Grid>
    ```

1. Save `MainWindow.xaml`, and open `MainWindow.xaml.cs`.

1. Delete this line of code, which we added in the previous section: `myInkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen;`.

1. Add the following namespace directive to the top of the file:

    ```csharp
    using Microsoft.Toolkit.Wpf.UI.XamlHost;
    ```

1. Add the following **ChildChanged** event handler method to the **MainWindow** class. When the host control has been rendered, this event handler runs and creates a simple event handler for the **SelectedDatesChanged** event of the calendar control.

    ```csharp
    private void MyCalendar_ChildChanged(object sender, EventArgs e)
    {
        WindowsXamlHost windowsXamlHost = (WindowsXamlHost)sender;

        var calendarView =
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

1. Save, build, and run. Confirm that the calendar control is shown in the window, and that a message box is displayed when you select a date.

## Package the app

You can optionally package your WPF app in an [MSIX package](/windows/msix) for deployment. MSIX is the modern and reliable app packaging technology for Windows.

The following instructions show you how to package all the components in the solution into an MSIX package by using the **Windows Application Packaging Project** in Visual Studio (see [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net)). These steps are necessary only if you want to package the WPF app in an MSIX package.

> [!NOTE]
> If you choose not to package your application in an [MSIX package](/windows/msix) for deployment, then computers that run your app must have the [Visual C++ Runtime](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads) installed.

1. Add a new project to your solution created from the **Windows Application Packaging Project** project template. As you create the project, select the same **Target version** and **Minimum version** as you selected for the UWP project.

1. In the packaging project, right-click the **Dependencies** node, and choose **Add Project Reference...**. In the list of projects, select *MyWPFApp*, and click **OK**.

    > [!NOTE]
    > If you want to publish your app in the Microsoft Store, then you also have to add a reference to the UWP project in the packaging project.

1. If you followed the steps up to this point, then all of the projects in your solution will target the same specific platform (x86 or x64). And that's necessary in order to build the WPF app into an MSIX package using the Windows Application Packaging Project. To confirm that, you can follow these steps:

    1. Right-click the solution node (*not* the project node) in **Solution Explorer**, and choose **Properties**.
    1. Select **Configuration Properties** on the left.
    1. Click the **Configuration Manager...** button.
    1. Confirm that all of the listed projects have the same value under **Platform**: either *x86* or *x64*.

1. Right-click the project node for the packaging project that you just added, and click **Set as Startup project**.

1. Build and run the packaging project. Confirm that the WPF app runs, and that the UWP control(s) display as expected.

1. For info about distributing/deploying the package, see [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview). 

## Related topics

* [Host WinRT XAML controls in desktop apps (XAML Islands)](xaml-islands.md)
* [InkCanvas class (UWP)](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas)
* [InkToolbar class (UWP)](/uwp/api/windows.ui.xaml.controls.inktoolbar)
* [CalendarView class (UWP)](/uwp/api/Windows.UI.Xaml.Controls.CalendarView)
* [WindowsXamlHost control for Windows Forms and WPF](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost)
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
* [PackageReference in project files](/nuget/consume-packages/package-references-in-project-files)
* [InkCanvas control for Windows Forms and WPF](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas)
* [InkToolbar control for Windows Forms and WPF](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar)
* [IXamlMetadataProvider interface](/uwp/api/Windows.UI.Xaml.Markup.IXamlMetadataProvider)
* [MSIX documentation](/windows/msix)
* [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net)
* [Microsoft Visual C++ Redistributable latest supported downloads](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads)