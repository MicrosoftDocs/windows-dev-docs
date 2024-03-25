---
title: Use the Windows App SDK in a WPF app
description: This topic enables you to use Windows App SDK features (such as App Lifecycle, MRT Core, DWriteCore, and others) in a Windows Presentation Foundation (WPF) app.
ms.topic: article
ms.date: 03/25/2024
keywords: windows win32, windows app development, Windows App SDK, Windows Presentation Foundation, WPF
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Use the Windows App SDK in a WPF app

The [Windows App SDK](/windows/apps/windows-app-sdk/) is the next evolution in the Windows app development platform. But this topic shows how you can use Windows App SDK APIs (and Windows Runtime APIs) in a [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/) app!

* In many cases, you'll want to recreate your WPF app in the form of a [Windows UI Library 3 (WinUI 3)](/windows/apps/winui/winui3/) app. Just one of the advantages of moving to WinUI 3 is to have access to the [Fluent Design System](https://www.microsoft.com/design/fluent/) (also see [Design and code Windows apps](/windows/apps/design/)). And WinUI 3 is part of the Windows App SDK&mdash;so, naturally, a WinUI 3 app can use the other Windows App SDK features and APIs, as well. This topic doesn't cover the process of migrating your WPF app to WinUI 3.
* But if you find that you're using features of WPF that aren't yet available in WinUI 3, then you can still use Windows App SDK features (such as App Lifecycle, MRT Core, DWriteCore, and others) in your WPF app. This topic shows you how.

And in case you don't already have an existing WPF project&mdash;or you want to practice the process&mdash;this topic includes steps to create a WPF project so that you can follow along and configure it to call Windows App SDK APIs.

## Prerequisites

1. [Install tools for the Windows App SDK](set-up-your-development-environment.md#install-visual-studio).
1. This topic covers both unpackaged and packaged WPF apps. If your WPF app is unpackaged (which WPF apps are by default), then ensure that all dependencies for unpackaged apps are installed (see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md#prerequisites)). A quick way to do that is to visit [Latest downloads for the Windows App SDK](/windows/apps/windows-app-sdk/downloads), then download and unzip and run one of the stable release **Runtime downloads**.

> [!IMPORTANT]
> The version of the **Runtime** that you install needs to match the version of the **Microsoft.WindowsAppSDK** NuGet package that you'll install in a later step.

For more info about the terms *unpackaged* and *packaged*, see [Advantages and disadvantages of packaging your app](/windows/apps/package-and-deploy/).

## Create a WPF project if you don't already have one

If you already have a WPF project, then you can move on to the next section.

1. In Visual Studio, create a new C# **WPF Application** project (which is a .NET project). Be careful that you choose the project template with the exact name **WPF Application**, and not the **WPF App (.NET Framework)** one.
2. Give the project a name, and accept any default options.

You now have a project that builds an unpackaged WPF app.

## Configure your WPF project for Windows App SDK support

First we'll edit the project file.

1. In **Solution Explorer**, right-click your project, and choose **Edit Project File**.

2. This step enables you to call [Windows Runtime (WinRT) APIs](/uwp/api/) (including [Windows App SDK APIs](/windows/windows-app-sdk/api/winrt/)). Inside the **PropertyGroup** element is the **TargetFramework** element, which is set to a value such as *net6.0*. Append to that target framework value a moniker (specifically, a [Target Framework Moniker](/windows/apps/desktop/modernize/desktop-to-uwp-enhance#net-6-and-later-use-the-target-framework-moniker-option)). For example, use the following if your app targets Windows 10, version 2004:

    ```xml
    <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
    ```

3. Also inside the **PropertyGroup** element, add a [RuntimeIdentifiers](/dotnet/core/project-sdk/msbuild-props#runtimeidentifiers) element, as shown below. If you're targeting .NET 8 or later, then use the value `win-x86;win-x64;win-arm64` instead.

    ```xml
    <RuntimeIdentifiers>win10-x86;win10-x64;win10-arm64</RuntimeIdentifiers>
    ```

4. By default, a WPF app is unpackaged (meaning that it isn't installed by using MSIX). An unpackaged app must initialize the Windows App SDK runtime before using any other feature of the Windows App SDK. You can do that automatically when your app starts via *auto-initialization*. You just set (also inside the **PropertyGroup** element) the `WindowsPackageType` project property appropriately, like this:

    ```xml
    <WindowsPackageType>None</WindowsPackageType>
    ```

    If you have advanced needs (such as custom error handling, or to load a specific version of the Windows App SDK), then instead of *auto-initialization* you can call the bootstrapper API explicitly&mdash;for more info, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time).

5. Save and close the project file.

Next, we'll install the Windows App SDK NuGet package in the project.

6. In **Solution Explorer**, right-click the **Dependencies** node of your project, and choose **Manage Nuget Packages...**.
7. In the **NuGet Package Manager** window, select the **Browse** tab, and install the *Latest stable* **Microsoft.WindowsAppSDK** package.

## Use some Windows App SDK features in your WPF app

This section offers a very simple example of calling Windows App SDK APIs from a WPF app. It uses the MRT Core feature (see [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview)). If this example works for your WPF project (and if you created a new one for this walkthrough, then it will), then you can follow these steps.

1. Add the following markup to `MainWindow.xaml` (you could paste it inside the root **Grid**):

    ```xaml
    <StackPanel>
        <Button HorizontalAlignment="Center" Click="Button_Click">Click me!</Button>
        <TextBlock HorizontalAlignment="Center" x:Name="myTextBlock">Hello, World!</TextBlock>
    </StackPanel>
    ```

2. Now we'll add some code that uses the [ResourceManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager) class in the Windows App SDK to load a string resource.

    1. Add a new **Resources File (.resw)** item to your project (leave it with the default name of *Resources.resw*).

    2. With the resources file open in the editor, create a new string resource with the following properties.
        - Name: **Message**
        - Value: **Hello, resources!**

    3. Save and close the resources file.

    4. In `MainWindow.xaml.cs`, add the following event handler:

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

3. Build the project, and run the app. Click the button to see the string `Hello, resources!` displayed.

> [!TIP]
> If at runtime you see a message box indicating that the application needs a particular version of the Windows App Runtime, and asks whether you want to install it now, then click **Yes**. That will take you to [Latest downloads for the Windows App SDK](/windows/apps/windows-app-sdk/downloads). For more info, see the [Prerequisites](#prerequisites) section above.

Also see [Runtime architecture](deployment-architecture.md) to learn more about the *Framework* package dependency that your app takes when it uses the Windows App SDK, and the additional components required to work in an unpackaged app.

## Package and deploy your WPF app with MSIX

Some Windows features and APIs (including the Windows App SDK [notifications APIs](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager)) require your app to have *package identity* at runtime (in other words, your app needs to be *packaged*). For more info, see [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps).

1. In **Solution Explorer** in Visual Studio, right-click the solution, and choose **Add** > **New Project...**.
1. In the **Add a new project** dialog box, search for *packaging*, choose the C# **Windows Application Packaging Project** project template, and click **Next**.
1. Name the project, and click **Create**.
1. We want to specify which applications in the solution are to be included in the package. So in the packaging project (*not* the WPF project), right-click the **Dependencies** node, and choose **Add Project Reference...**.
1. In the list of projects in the solution, choose your WPF project, and click **OK**.
1. Expand the packaging project's **Dependencies** > **Applications** node, and confirm that your WPF project is referenced and highlighted in bold. This means that it will be used as a starting point for the package.
1. Right-click the packaging project, and choose **Set As Startup Project**.
1. Right-click the WPF project, and choose **Edit Project File**.
1. Delete `<WindowsPackageType>None</WindowsPackageType>`, save, and close.
1. In the **Solution Platforms** drop-down, pick *x64* (instead of *Any Cpu*).
1. Confirm that you can build and run.

Now that you've packaged your WPF app, you can call APIs that require package identity. So in `MainWindow.xaml.cs`, edit your event handler to look like this:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    var notification = new AppNotificationBuilder()
        .AddArgument("action", "viewConversation")
        .AddArgument("conversationId", "9813")
        .AddText("Andrew sent you a picture")
        .AddText("Check this out, The Enchantments in Washington!")
        .BuildNotification();

    AppNotificationManager.Default.Show(notification);
}
```

Build and run again. Click the button, and confirm that a toast notification is displayed. When called from a process that lacks package identity at runtime, the notifications APIs throw an exception.

> [!NOTE]
> The steps in this section showed you how to create a *packaged app*. An alternative is to create a *packaged app with external location*. For a reminder of all these terms, see [Advantages and disadvantages of packaging your app](/windows/apps/package-and-deploy/).

## Related topics

* [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
* [Install tools for the Windows App SDK](set-up-your-development-environment.md#install-visual-studio)
* [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md#prerequisites)
* [Latest downloads for the Windows App SDK](/windows/apps/windows-app-sdk/downloads)
* [Advantages and disadvantages of packaging your app](/windows/apps/package-and-deploy/)
* [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time)
* [Runtime architecture](deployment-architecture.md)
* [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps)
