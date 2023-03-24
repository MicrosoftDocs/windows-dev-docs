---
title: How to target multiple platforms with your WinUI 3 app
description: Reach more users with a single WinUI 3 / .NET codebase using Uno Platform.
ms.topic: article
ms.date: 3/14/2023
keywords: uno platform, winui
ms.author: aashcraft
author: alvinashcraft
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
---

# How to target multiple platforms with your WinUI 3 app

Once you've [created](/hub/apps/how-tos/hello-world-winui3.md) a starter Hello World WinUI 3 app, you might be wondering how to reach more users with a single codebase. This how-to will use Uno Platform to expand the reach of your existing application enabling reuse of the business logic and UI layer across native mobile, web, and desktop.

:::image type="content" source="images/hello-world/uno-hello-world-web.png" alt-text="The 'Hello world' app running in the browser.":::

## Prerequisites

- [Visual Studio 2022 17.4 or later](https://visualstudio.microsoft.com/#vs-section)
- [Tools for Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)
- ASP.NET and web development workload (for WebAssembly development)
- .NET Multi-platform App UI development installed (for iOS, Android, Mac Catalyst development).
- .NET desktop development installed (for Gtk, Wpf, and Linux Framebuffer development)

## Finalize your environment

1. Open a command-line prompt, Windows Terminal if you have it installed, or else Command Prompt or Windows Powershell from the Start Menu.

2. Install the `uno-check` tool:
    - Use the following command:

        `dotnet tool install -g uno.check`

    - To update the tool, if you already have an existing one:

        `dotnet tool update -g uno.check`

3. Run the tool with the following command:

    `uno-check`

4. Follow the instructions indicated by the tool.

## Install the Uno Platform solution templates

1. Launch Visual Studio 2022, then click `Continue without code`. Click `Extensions` -> `Manage Extensions` from the Menu Bar.

    :::image type="content" source="images/hello-world/manage-extensions.png" alt-text="Visual Studio Menu bar item that reads manage extensions":::

2. In the Extension Manager expand the **Online** node and search for `Uno`, install the `Uno Platform` extension or download it from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=unoplatform.uno-platform-addin-2022), then restart Visual Studio.

    :::image type="content" source="images/hello-world/uno-extensions.PNG" alt-text="Manage Extensions window in Visual Studio with Uno Platform extension as a search result":::

## Create an application

Now that we are ready to create a multi-platform application, the approach we'll take is to create a new Uno Platform application. We will copy XAML code from the previous tutorial's Hello World WinUI 3 project into our multi-platform project. This is possible because Uno Platform lets you reuse your existing codebase. For features dependent on OS APIs provided by each platform, you can easily make them work over time. This approach is especially useful if you have an existing application that you want to port to other platforms.

Soon enough, you will be able to reap the benefits of this approach, as you can target more platforms with a familiar XAML flavor and the codebase you already have.

To create an Uno Platform app:

1. Create a new C# solution using the **Uno Platform App** type from Visual Studio's **Start Page**. You may need to choose a different name for your solution.

2. Choose a base template to take your Hello World application multi-platform

    :::image type="content" source="images/hello-world/vsix-new-project-options.png" alt-text="Uno solution template for project startup type":::

    While the **Blank** template could be used for simplicity, we normally recommend using the **Default** template for production-ready code. The **Default** template already includes a series of [features](https://platform.uno/docs/articles/external/uno.extensions/doc/ExtensionsOverview.html#learn-about-unoextensions-features) and good practices which will jumpstart your application development.

3. You can optionally choose to customize your app based on the sections on the left side:
    - **Framework** allows to choose which `TargetFramework` your app will use. [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) is a commonly appropriate choice.
    - **Platforms** provides a list of platforms your application will support. You can choose to support all platforms, or a subset of them
    - **Presentation** gives a choice about using MVVM (e.g. [MVVM Toolkit](/dotnet/communitytoolkit/mvvm/)) or Uno Platform's MVUX and Feeds
    - **Projects** gives the ability to add a Server project for APIs and hosting for the WebAssembly project
    - **Testing** provides Unit Testing and [UI Testing projects](https://github.com/unoplatform/Uno.UITest)
    - **Features** provides support for WebAssembly PWA and optional VS Code support files
    - **Extensions** allows to choose for additional [Uno.Extensions](https://github.com/unoplatform/uno.extensions) to kickstart your app faster (described above)
    - **Application** sets the App ID for relevant platforms, used when publishing on various app stores.
    - **Theme** gives the ability to change between Fluent and Material themes

4. Click the **Create** button

5. Wait for the projects to be created, and their dependencies to be restored

6. A banner at the top of the editor may ask to reload projects, click **Reload projects**:
    :::image type="content" source="images/hello-world/vs2022-project-reload.png" alt-text="Visual Studio banner offering to reload your projects to complete changes":::

## Building your app

Now that you've generated the functional starting point of your multi-platform WinUI application, you can copy markup into it from the Hello World WinUI 3 project outlined in the [previous](/hub/apps/how-tos/hello-world-winui3.md) tutorial.

1. Make sure Visual Studio has your WinUI 3 project open, then copy the child XAML elements from `MainWindow.xaml` in the WinUI 3 project to the your `MainPage.xaml` file in the Uno Platform project. The `MainPage` view markup should look like this:

```xml
<Page x:Class="HelloWorld.MainPage"
	  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	  xmlns:local="using:HelloWorld"
	  xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	  xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	  mc:Ignorable="d"
	  Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

      <!-- Below is the code you copied from MainWindow: -->
      <StackPanel Orientation="Horizontal"
                  HorizontalAlignment="Center"
                  VerticalAlignment="Center">
          <TextBlock x:Name="myText" 
                     Text="Hello world!"
                     Foreground="Red"/>
      </StackPanel>
</Page>
```



2. To run the **WebAssembly** (Wasm) head:
    - Right-click on the `MyApp.Wasm` project, select **Set as startup project**
    - Press the `MyApp.Wasm` button to deploy the app
3. To run the ASP.NET Hosted **WebAssembly** (Server) head:
    - Right-click on the `MyApp.Server` project, select **Set as startup project**
    - Press the `MyApp.Server` button to deploy the app
4. To debug for **iOS**:
    - Right-click on the `MyApp.Mobile` project, select **Set as startup project**
    - In the "Debug toolbar" drop-down, select framework `net7.0-ios`:

      :::image type="content" source="images/hello-world/net7-ios-debug.png" alt-text="Visual Studio dropdown to select a target framework to deploy":::

    - Select an active device
5. To debug the **Android** platform:
    - Right-click on the `MyApp.Mobile` project, select **Set as startup project**
    - In the **Debug toolbar** drop-down, select framework `net7.0-android`
    - Select an active device in the "Device" sub-menu

Now you're ready to start building your multi-platform application!


## See also

- [Uno Platform documentation](https://platform.uno/docs/articles/intro.html)
- [Uno Extensions features](https://platform.uno/docs/articles/external/uno.extensions/doc/ExtensionsOverview.html#learn-about-unoextensions-features)