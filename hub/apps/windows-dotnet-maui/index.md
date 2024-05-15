---
title: Build Windows apps with .NET MAUI
description: Learn about .NET MAUI, benefits it provides to Windows developers, and how take your Windows apps cross-platform.
ms.topic: article
ms.date: 05/09/2024
keywords: windows win32, desktop development, Windows App SDK, .net maui
ms.localizationpriority: medium
---

# Build Windows apps with .NET MAUI

.NET Multi-platform App UI (.NET MAUI) is a cross-platform framework for creating native mobile and desktop apps with C# and _optionally_ XAML. Using [.NET MAUI](/dotnet/maui/), you can develop apps that can run on Windows, Android, iOS, macOS, and Samsung Tizen from a single shared code-base. If you build a Windows app with .NET MAUI, it will use [WinUI 3](../winui/winui3/index.md) as its native platform, and therefore run on Windows 10 version 1809 or later and Windows 11.

## Why use .NET MAUI on Windows?

Building apps for Windows with .NET MAUI provides several benefits:

- **Native on Windows**: .NET MAUI creates a [WinUI](../winui/winui3/index.md) app when targeting Windows. This means that your .NET MAUI app will provide the same user experience on Windows as your Windows App SDK applications.
- **User interface in code**: The [.NET MAUI Community Toolkit](/dotnet/communitytoolkit/maui/) includes [C# Markup](/dotnet/communitytoolkit/maui/markup/markup), a set of fluent helper methods for building user interfaces in C# code. This enables a separation of concerns between the UI and the business logic of your app, just as with XAML. C# Markup is available on all platforms supported by .NET MAUI.
- **Cross-platform**: Take your Windows apps to [all supported platforms](/dotnet/maui/supported-platforms), including Android, iOS, macOS, and Samsung Tizen devices.
- **Simplicity**: Develop in a [single shared project](/dotnet/maui/fundamentals/single-project) that can target every platform supported by .NET MAUI.
- **Hot Reload**: Save time while debugging with [.NET Hot Reload](/visualstudio/debugger/hot-reload) and [XAML Hot Reload](/dotnet/maui/xaml/hot-reload) support in .NET MAUI. Make edits while the app is running and the changes are automatically applied.
- **Native APIs**: .NET MAUI provides [cross-platform APIs](/dotnet/maui/platform-integration/) for native features on each platform. For native APIs that are not available in .NET MAUI's cross-platform APIs, you can [invoke platform-specific code](/dotnet/maui/platform-integration/invoke-platform-code).

If you are planning to build a new app for Windows and want to target additional platforms, you should consider using .NET MAUI. If you are only targeting Windows with your app, there are some good reasons to continue using the Windows App SDK:

- **Familiarity**: .NET MAUI XAML and Windows App SDK XAML have some differences. If you are comfortable with XAML in UWP and Windows App SDK, you will have a bit of a learning curve with the .NET MAUI controls and XAML syntax.
- **Native Controls**: .NET MAUI does not currently support using Windows App SDK controls. If you have existing controls from other Windows App SDK projects you intend to re-use or rely on 3rd Party or open source controls, you will need to find alternatives for .NET MAUI projects.
- **Closer to Windows**: When writing .NET MAUI apps, it outputs a Windows App SDK app, but there is some translation to get from your code to the native Windows app. With Windows App SDK, you are eliminating that translation step and are less likely to encounter issues with styles, API compatibility, or layout.

## .NET MAUI resources for Windows developers

### .NET MAUI documentation

The [.NET MAUI docs](/dotnet/maui/) include resources for learning about .NET MAUI development for every platform, including Windows.

### WinUI documentation

Explore the [WinUI docs](../winui/winui3/index.md) to learn about the features of the Windows UI library.

### .NET Conf: Focus on MAUI

This [one-day live stream](https://www.youtube.com/playlist?list=PLdo4fOcmZ0oWePZU3W162NJ9vcXqgpMVc) from August 2022 featured speakers from Microsoft and the .NET MAUI developer community. Learn how to build apps and hear from the team building .NET MAUI.

### .NET MAUI blog posts

- [5 .NET MAUI Features for Building Great Desktop Apps](https://devblogs.microsoft.com/dotnet/5-dotnet-maui-desktop-features/)
- [Build Your Own Podcast App with .NET 7, Blazor, and .NET MAUI](https://devblogs.microsoft.com/dotnet/build-your-own-podcast-app-with-dotnet-blazor-and-dotnet-maui/)

### GitHub

.NET MAUI is open source and hosted on GitHub. Use the [.NET MAUI repository](https://github.com/dotnet/maui) to file feature requests or bugs, interact with the development team, and explore the [wiki](https://github.com/dotnet/maui/wiki).

### Code samples

Explore the .NET MAUI code samples in the [samples browser](/samples/browse/?expanded=dotnet&products=dotnet-maui) or on GitHub at [dotnet/maui-samples](https://github.com/dotnet/maui-samples).

#### Samples that demonstrate specific tasks

| Sample Name/Link | Task |
|--|--|
| [Animations](https://github.com/dotnet/maui-samples/tree/main/6.0/Animations) | **Use animations** to bring your app to life. |
| [Behaviors](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/BehaviorsDemos) | **Add functionality without subclassing** using behaviors, instead attaching behavior classes to your controls. |
| [Control Templates](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/ControlTemplateDemos) | **Define the visual structure** of `ContentView` derived custom controls, and `ContentPage` derived pages. |
| [Data Binding](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/DataBindingDemos) | **Bind objects** to the actual UI elements, responsively updating one another when one changes. |
| [Shell App](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/Shell) | **Use a .NET Shell app** to reduce complexity, reuse code, and integrate existing Windows functionality (such as URI navigation and integrated search bars) into your app. |
| [Triggers](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/TriggersDemos) | **Use triggers** to selectively update, hide, or display specific controls when an event or data changes. |

## Get started with .NET MAUI on Windows

To get started with .NET MAUI on Windows, install Visual Studio 2022 version 17.3 or later.

- [Download 2022 Community](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=Community&channel=Release&Version=VS2022&source=VSLandingPage&add=Microsoft.VisualStudio.Workload.CoreEditor&add=Microsoft.VisualStudio.Workload.NetCrossPlat;includeRecommended&cid=2302)
- [Download 2022 Professional](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=Professional&channel=Release&Version=VS2022&source=VSLandingPage&add=Microsoft.VisualStudio.Workload.CoreEditor&add=Microsoft.VisualStudio.Workload.NetCrossPlat;includeRecommended&cid=2302)
- [Download 2022 Enterprise](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=Enterprise&channel=Release&Version=VS2022&source=VSLandingPage&add=Microsoft.VisualStudio.Workload.CoreEditor&add=Microsoft.VisualStudio.Workload.NetCrossPlat;includeRecommended&cid=2302)

When installing or modifying Visual Studio, select the **.NET Multi-platform App UI development** workload with the default optional installation options selected. For more information about managing workloads in Visual Studio, see [Modify Visual Studio workloads, components, and language packs](/visualstudio/install/modify-visual-studio). If you are using Visual Studio Code and the .NET CLI, you will use the [dotnet workload command](/dotnet/core/tools/dotnet-workload) for workload management.

If you haven't enabled development mode on your PC, see [Enable your device for development](../get-started/enable-your-device-for-development.md). If it isn't enabled, Visual Studio will prompt you to enable development mode when you try to run your first .NET MAUI project on Windows.

## Next steps

Check out a walk-through of [creating your first .NET MAUI app on Windows](./walkthrough-first-app.md).

## Related topics

[What is .NET MAUI](/dotnet/maui/what-is-maui)

[.NET MAUI supported platforms](/dotnet/maui/supported-platforms)

[WinUI overview](../winui/winui3/index.md)

[.NET MAUI Community Toolkit documentation](/dotnet/communitytoolkit/maui/)
