---
title: Use MRT Core to manage resources in a .NET app
description: This topic demonstrates how to use  MRT Core from the Windows App SDK in a .NET app.
ms.topic: how-to
ms.date: 05/07/2026
keywords: windows win32, windows app development, Windows App SDK, Windows Presentation Foundation, WPF
ms.localizationpriority: medium
zone_pivot_groups: dotnet-app-type
---

# Manage resources with MRT Core in a .NET app

This article shows a very simple example of how to use the MRT Core feature from the Windows App SDK in a .NET app. See [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview) for complete details of the feature.

## Prerequisites

- A WPF or WinForms app project.
- You must configure the project to:
    - [Call Windows Runtime APIs](../../../desktop/modernize/winrt-apis-desktop-apps.md)
    - [Use the Windows App SDK](../../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)

:::zone pivot="wpf"

1. Add the following markup to `MainWindow.xaml` (you could paste it inside the root **Grid**):

    ```xaml
    <StackPanel>
        <Button HorizontalAlignment="Center" Click="Button_Click">Click me!</Button>
        <TextBlock HorizontalAlignment="Center" x:Name="myTextBlock">Hello, World!</TextBlock>
    </StackPanel>
    ```

:::zone-end

:::zone pivot="winforms"

1. Open `Form1.cs` (using the **View Designer** command), and drag a **Button** and a **Label** out of the **Toolbox** and onto the designer.

:::zone-end

1. Now add code that uses the [ResourceManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager) class in the Windows App SDK to load a string resource.

    1. Add a new **Resources File (.resw)** item to your project (leave it with the default name of *Resources.resw*).
    
        (Click **Project > Add New Item..., C# Items > WinUI > Resources File (.resw), Add**)

    1. With the resources file open in the editor, click the + sign to create a new string resource with the following properties.
        - Name: **Message**
        - Value: **Hello, resources!**

    1. Save and close the resources file.

:::zone pivot="wpf"

1. In `MainWindow.xaml.cs`, add the following event handler:

    ```csharp
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Construct a resource manager using the resource index generated during build.
        var manager = 
          new Microsoft.Windows.ApplicationModel.Resources.ResourceManager();

        // Look up a string in the resources file using the string's name.
        myTextBlock.Text = manager.MainResourceMap.GetValue("Resources/Message").ValueAsString;
    }
    ```
:::zone-end

:::zone pivot="winforms"

1. Double-click *button1* to generate an event handler. Open `Form1.cs` (using the **View Code** command), and edit the event handler to look like this:

    ```csharp
    private void button1_Click(object sender, EventArgs e)
    {
        // Construct a resource manager using the resource index generated during build.
        var manager =
            new Microsoft.Windows.ApplicationModel.Resources.ResourceManager();

        // Look up a string in the resources file using the string's name.
        label1.Text = manager.MainResourceMap.GetValue("Resources/Message").ValueAsString;
    }
    ```

:::zone-end

1. Build the project, and run the app. Click the button to see the string `Hello, resources!` displayed.

## Related topics

- [Manage resources with MRT Core](../../../windows-app-sdk/mrtcore/mrtcore-overview.md)
- [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
- [Windows Forms (WinForms)](/dotnet/desktop/winforms/)
- [Call Windows Runtime APIs](../../../desktop/modernize/winrt-apis-desktop-apps.md)
- [Use the Windows App SDK](../../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)