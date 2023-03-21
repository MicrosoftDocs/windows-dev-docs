---
title: How to target multiple platforms with your WinUI 3 app
description: Reach more users with a single WinUI 3 / .NET codebase using Uno Platform.
ms.topic: article
ms.date: 3/14/2023
keywords: uno platform, winui
ms.author: alvinashcraft
author: lukeblevins
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
---

# How to target multiple platforms with your WinUI 3 app

Now that you've created your first WinUI 3 app, you might be wondering how to reach more users with a single codebase. This how-to will use Uno Platform to expand the reach of your existing application enabling reuse of the business logic and UI layer across native mobile, web, and desktop.

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

## Install the solution templates

1. Launch Visual Studio 2022, then click `Continue without code`. Click `Extensions` -> `Manage Extensions` from the Menu Bar.

    :::image type="content" source="images/hello-world/manage-extensions.png" alt-text="Visual Studio Menu bar item that reads manage extensions":::

2. In the Extension Manager expand the **Online** node and search for `Uno`, install the `Uno Platform` extension or download it from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=unoplatform.uno-platform-addin-2022), then restart Visual Studio.

    :::image type="content" source="images/hello-world/uno-extensions.PNG" alt-text="Visual Studio Menu bar item that reads manage extensions":::

## Create an application

Now that we are ready to create a multi-platform application, the approach we'll take is to create a new Uno Platform application, and then copy the existing WinUI 3 project items into it. This will allow you to reuse the existing codebase, and then you can add the necessary changes over time to make new functionality work on other platforms. This approach is also useful if you have an existing application that you want to port to other platforms.

Soon enough, you will be able to reap the benefits of this approach, as you can target more platforms with a familiar XAML flavor and the codebase you already have.

To create an Uno Platform app:

1. Create a new C# solution using the **Uno Platform App** template, from Visual Studio's **Start Page**
2. Choose a base template to take your hello world application multi-platform

    :::image type="content" source="images/hello-world/vsix-new-project-options.png" alt-text="Uno solution template for project startup type":::

3. You can optionally choose to customize your app based on the sections on the left side:
    - **Framework** allows to choose which `TargetFramework` your app will use. [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) is a commonly appropriate choice.
    - **Platforms** provides a list of platforms your application will support. You can choose to support all platforms, or a subset of them
    - **Presentation** gives a choice about using MVVM (e.g. [MVVM Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)) or Uno Platform's MVUX and Feeds
    - **Projects** gives the ability to add a Server project for APIs and hosting for the WebAssembly project
    - **Testing** provides Unit Testing and [UI Testing projects](https://github.com/unoplatform/Uno.UITest)
    - **Features** provides support for WebAssembly PWA and optional VS Code support files
    - **Extensions** allows to choose for additional [Uno.Extensions](https://github.com/unoplatform/uno.extensions) to kickstart your app faster
    - **Application** sets the App ID for relevant platforms, used when publishing on various app stores.
    - **Theme** gives the ability to change between Fluent and Material themes

4. Click the create button

5. Wait for the projects to be created, and their dependencies to be restored

6. A banner at the top of the editor may ask to reload projects, click **Reload projects**:
    :::image type="content" source="images/hello-world/vs2022-project-reload.png" alt-text="Visual Studio banner offering to reload your projects to complete changes":::

## Copy your existing WinUI 3 project items

Now that you've generated the functional starting point of your multi-platform WinUI application, you can copy the existing WinUI 3 project items into it.

1. Make sure Visual Studio has your WinUI 3 project open, then copy the following items from the WinUI 3 project to the Uno Platform project:
    - `MainWindow.xaml`
    - `MainWindow.xaml.cs`

2. If you're prompted to do so, overwrite any existing files of the same name.

## Deploy your application

1. To run the **WebAssembly** (Wasm) head:
    - Right click on the `MyApp.Wasm` project, select **Set as startup project**
    - Press the `MyApp.Wasm` button to deploy the app
2. To run the ASP.NET Hosted **WebAssembly** (Server) head:
    - Right click on the `MyApp.Server` project, select **Set as startup project**
    - Press the `MyApp.Server` button to deploy the app
3. To debug for **iOS**:
    - Right click on the `MyApp.Mobile` project, select **Set as startup project**
    - In the "Debug toolbar" drop-down, select framework `net7.0-ios`:

      :::image type="content" source="images/hello-world/net7-ios-debug.png" alt-text="Visual Studio dropdown to select a target framework to deploy":::

    - Select an active device
4. To debug the **Android** platform:
    - Right click on the `MyApp.Mobile` project, select **Set as startup project**
    - In the **Debug toolbar** drop down, select framework `net7.0-android`
    - Select an active device in the "Device" sub-menu

Now you're ready to start building your multi-platform application!
