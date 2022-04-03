---
title: Walkthrough&mdash;Create a C# component with WinUI 3 controls, and consume it from a C++/WinRT app that uses the Windows App SDK
description: Author a Windows Runtime component with C#/WinRT with WinUI controls, and consume it from any Windows App SDK app.
ms.date: 03/15/2022
ms.topic: article
ms.localizationpriority: medium
---

# Walkthrough&mdash;Create a C# component with WinUI 3 controls, and consume it from a C++/WinRT app that uses the Windows App SDK

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

C#/WinRT provides support for authoring components that implement WinUI custom types and custom controls. These components can be consumed from either C# or C++/WinRT applications that use the Windows App SDK. This support is available beginning with C#/WinRT v1.6.1 with some limitations, and is currently in development.

For more details about the supported scenarios and known issues, refer to [Authoring C#/WinRT components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md) on the C#/WinRT Github repo.

This walkthrough demonstrates how to author a C# component with a custom control, and how to consume that component from a C++/WinRT app, using the Windows App SDK project templates.

## Prerequisites

This walkthrough requires the following tools and components:

- [Visual Studio 2022](/visualstudio/releases/2022/release-notes)
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Windows App SDK VSIX](/windows/apps/windows-app-sdk/downloads) 

## Author your C#/WinRT component using the Windows App SDK

1. Create a new C# library project using the **Class Library (WinUI 3 in Desktop)** template provided by the Windows App SDK. For this walkthrough, we've named the library project **WinUIComponentCs**, and the solution **AuthoringWinUI**. 

    Leave the **Place solution and project in the same directory** box unchecked (otherwise, the `packages` folder for the C++ application in the preceding section will end up interfering with the C# library project).

    ![New library dialog](images\create-winui-library.png)

1. Delete the `Class1.cs` file that's included by default.

1. Install the latest [Microsoft.Windows.CsWinRT](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT/) NuGet package in your project.

    i. In Solution Explorer, right-click on the project node, and select **Manage NuGet Packages**.

    ii. Search for the **Microsoft.Windows.CsWinRT** NuGet package, and install the latest version. 

1. Add the following properties to your library project:

    ```xml
    <PropertyGroup>   
        <CsWinRTComponent>true</CsWinRTComponent>
        <WindowsAppContainer>true</WindowsAppContainer>
    </PropertyGroup>
    ```
    
    - The `CsWinRTComponent` property specifies that your project is a Windows Runtime component so that a `.winmd` file is generated when you build the project.
    - The `WindowsAppContainer` property allows native C++ projects to reference the C# component.

1. Add a custom control or user control to your library. To do this, right-click on your project in Visual Studio, click **Add** > **New Item**, and select **WinUI** on the left pane. For this walkthrough, we added a new **User Control (WinUI 3)** and named it `NameReporter.xaml`. The **NameReporter** user control allows a user to enter a first and last name into the appropriate **TextBox** control, and click a button. The control then displays a message box with the name that the user entered.

1. Paste the following code in the `NameReporter.xaml` file:

    ```xml
    <UserControl
    x:Class="WinUIComponentCs.NameReporter"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:WinUIComponentCs"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

        <StackPanel HorizontalAlignment="Center">
            <StackPanel.Resources>
                <Style x:Key="BasicTextStyle" TargetType="TextBlock" BasedOn="{StaticResource BodyTextBlockStyle}">
                    <Setter Property="Margin" Value="10,10,10,10"/>
                </Style>
            </StackPanel.Resources>
            
            <TextBlock Text="Enter your name." Margin="0,0,0,10"/>
            <StackPanel Orientation="Horizontal" Margin="0,0,0,10">
                <TextBlock Style="{StaticResource BasicTextStyle}">
                    First Name:
                </TextBlock>
                <TextBox Name="firstName" />
            </StackPanel>
            <StackPanel Orientation="Horizontal" Margin="0,0,0,10">
                <TextBlock Style="{StaticResource BasicTextStyle}">
                    Last Name:
                </TextBlock>
                <TextBox Name="lastName" />
            </StackPanel>
            <Button Content="Submit" Click="Button_Click" Margin="0,0,0,10"/>
            <TextBlock Name="result" Style="{StaticResource BasicTextStyle}" Margin="0,0,0,10"/>
        </StackPanel>
    </UserControl>
    ```

1. Add the following method to `NameReporter.xaml.cs`:

    ```csharp
    using System.Text;
    ...
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        StringBuilder displayText = new StringBuilder("Hello, ");
        displayText.AppendFormat("{0} {1}.", firstName.Text, lastName.Text);
        result.Text = displayText.ToString();
    }
    ```
 
1. You can now build the **WinUIComponentCs** project to generate a `.winmd` file for the component.

## Reference the component from a Windows App SDK C++/WinRT app

The following steps show how to consume the component created from the previous section from a C++/WinRT Windows App SDK application. Consuming a C#/WinRT component from C++ currently requires using the single-project **Blank App, Packaged (WinUI 3 in Desktop)** template. Note that C# components can also be referenced from C# packaged apps without class registrations.

Consumption from MSIX-packaged apps that use a separate **Windows Application Packaging (WAP)** project, or from apps that are not MSIX-packaged, is not currently supported. To follow the status of, and provide input on, supporting these scenarios, see [Authoring C#/WinRT components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md) on the C#/WinRT Github repo.

1. Add a new C++ Windows App SDK application project to your solution. Right-click on your solution in Visual Studio, and select **Add** > **New Project**. Select the C++ **Blank App, Packaged (WinUI 3 in Desktop)** template provided by the Windows App SDK. For this walkthrough, we named the app **CppApp**.

1. Add a project reference from the C++ app to the C# component. In Visual Studio, right-click on the C++ project and choose **Add** > **Reference**, and select the **WinUIComponentCs** project.

    > [!NOTE]
    > Consuming components as a NuGet package reference is supported with some limitations. Namely, components with custom user controls can't currently be consumed as a NuGet package reference.

1. In the app's `pch.h` header file, add the following lines:

      ```cpp
      #include <winrt/WinUIComponentCs.h>
      #include <winrt/WinUIComponentCs.WinUIComponentCs_XamlTypeInfo.h>
      ```

1.  Open up the package manifest file, `Package.appxmanifest`.

    > [!NOTE]
    > There's a known issue where the `Package.appxmanifest` file doesn't appear in Visual Studio Solution Explorer. To workaround that, right-click on your C++ project, select **Unload Project**, and double-click on the project to open the `CppApp.vcxproj` file. Add the following entry to the project file, and then reload the project:

    ```xml
    <ItemGroup>
        <AppxManifest Include="Package.appxmanifest">
        <SubType>Designer</SubType>
        </AppxManifest>
    </ItemGroup>
    ```

    In `Package.appxmanifest`, add the following activatable class registrations. You'll also need an additional `ActivatableClass` entry for the **WinUIComponentCs.WinUIComponentCs_XamlTypeInfo.XamlMetaDataProvider** class in order to activate the WinUI types. Right-click on the `Package.appxmanifest` file and select **Open With** > **XML (Text Editor)** in order to edit the file.

    ```xml
    <!--In order to host the C# component from C++, you must add the following Extension group and list the activatable classes-->
    <Extensions>
        <Extension Category="windows.activatableClass.inProcessServer">
            <InProcessServer>
                <Path>WinRT.Host.dll</Path>
                <ActivatableClass ActivatableClassId="WinUIComponentCs.NameReporter" ThreadingModel="both" />
                <ActivatableClass ActivatableClassId="WinUIComponentCs.WinUIComponentCs_XamlTypeInfo.XamlMetaDataProvider" ThreadingModel="both" />
            </InProcessServer>
        </Extension>
    </Extensions>
    ```

1. Open the `MainWindow.xaml` file. 

    i. Add a reference to the component's namespace at the top of the file.

    ```xml
    xmlns:custom="using:WinUIComponentCs"
    ```
    
    ii. Add the user control to the existing XAML code.

    ```xml
    <StackPanel>
        ...
        <custom:NameReporter/>
    </StackPanel>
    ```

1. Set **CppApp** as the startup project&mdash;right-click on **CppApp**, and select **Set as Startup Project**. Set the solution configuration to `x86`. Before building, you might also need to retarget your solution to build with the Visual Studio 2022 build tools. Right-click on the solution, select **Retarget solution**, and upgrade the Platform Toolset to **v143**.

1. Build and run the app to see the custom **NameReporter** control.

## Known issues

- Consuming a C# component built for `AnyCPU` from C++ is supported only from `x86` applications currently. `x64` and `ARM64` apps result in a runtime error similar to: *%1 is not a valid Win32 application.*
      
## Related topics

- [C#/WinRT Authoring Sample app](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/AuthoringDemo)
- [Authoring C#/WinRT Components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md)
- [Managed component hosting](https://github.com/microsoft/CsWinRT/blob/master/docs/hosting.md)
