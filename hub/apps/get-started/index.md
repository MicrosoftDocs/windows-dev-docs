---
description: Learn how to get started building new apps for Windows desktop and modernizing existing apps.
title: Overview of app development options
ms.topic: article
ms.date: 06/07/2022
keywords: windows win32, desktop development
ms.localizationpriority: medium
---

# Overview of app development options

This article contains all the information you need to get started building apps for the Windows desktop environment.

When you want to create a new app for Windows 11 or Windows 10, the first decision you make is what type of app to build. The Windows and .NET development tools in Visual Studio provide several different types of apps you can build, each with their own Visual Studio project types and different strengths. Each app type includes an app model that defines the lifecycle of the app, a default UI framework, and access to a comprehensive set APIs for using Windows features.

> [!VIDEO https://www.microsoft.com/en-us/videoplayer/embed/RWQwHD]

## Create a WinUI 3 app

The Windows UI Library (WinUI) 3 is the latest and recommended user interface (UI) framework for Windows desktop apps, including managed apps that use C# and .NET and native apps that use C++ with the Win32 API. By incorporating the [Fluent Design System](https://www.microsoft.com/design/fluent/#/) into all experiences, controls, and styles, WinUI provides consistent, intuitive, and accessible experiences using the latest UI patterns.

WinUI 3 is available as part of the **[Windows App SDK](../windows-app-sdk/index.md)**. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any C++ Win32 or C# .NET app on a broad set of target Windows OS versions.

> [!div class="button"]
> [Install tools for the Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)

If you have already installed the required developer tools, you are ready to [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md).

## Cross-platform options

WinUI also serves as the basis for cross-platform technologies that provide great native Windows experiences using a variety of coding languages. These frameworks harness the power of WinUI on Windows, while also enabling execution on other operating systems.

### [.NET MAUI](#tab/net-maui)

.NET Multi-platform App UI (MAUI) is an open-source, cross-platform framework for building Android, iOS, macOS, and Windows applications that leverage the native UI and services of each platform from a single .NET code base. Because .NET MAUI favors platform native experiences, it uses WinUI 3 and the Windows App SDK so apps get the latest user experience on Windows. This gives your apps access to everything you get with WinUI 3 plus the ability to reach to other platforms.

.NET MAUI for Windows is a great choice if:

- You want to share as much .NET code as possible across mobile and desktop applications.
- You want to ship your application beyond Windows to other desktop and mobile targets with native platform experiences.
- You want to use C# and/or XAML for building cross-platform apps.
- You're using Blazor for web development and wish to include all or part of that in a mobile or desktop application.

> [!div class="button"]
> [Get started with .NET MAUI](/dotnet/maui/get-started/installation)

For more information about .NET MAUI, see the following links:

- [.NET MAUI documentation](/dotnet/maui/)
- [.NET MAUI on GitHub](https://github.com/dotnet/maui)
- [.NET MAUI Product Roadmap](https://github.com/dotnet/maui/wiki/Roadmap)
- [Build Windows apps with .NET MAUI](/windows/apps/windows-dotnet-maui/)
- [Resources for learning .NET MAUI](/dotnet/maui/get-started/resources/)
- [Video Series - .NET MAUI for Beginners](/shows/dotnet-maui-for-beginners/)
- [Build 2022: Build native apps for any device with .NET and Visual Studio](https://www.youtube.com/watch?v=IbwgHJPoE-M)

### [React Native for Windows](#tab/rnw)

[React Native](https://reactnative.dev) is a development platform which allows building cross-platform apps.
React Native for Windows brings React Native support to the Windows 10 and Windows 11 SDKs, enabling you to use JavaScript to build native Windows apps for all devices supported by Windows 10 and Windows 11. This includes PCs, tablets, 2-in-1s, Xbox, Mixed reality devices, etc.

With React Native for Windows, you write most or all of your app code in JavaScript - or TypeScript - and the framework produces a native UWP XAML application. If your app needs to call a platform API, you can usually do so through one of the many [community modules](https://reactnative.directory), or if a module does not yet exist, you can easily [write a native module to expose it](https://aka.ms/RNW-NativeModules).

Here are some reasons to choose React Native for Windows:

- You want to share code across platforms as much as possible, or you have web properties that you want to share code with.
- Improved developer productivity and inner loop, thanks to fast refresh.
- Your app's fundamentals (performance, accessibility, internationalization) are as good as a native UWP app.
- You have experience with and a preference for JavaScript or TypeScript
- You would like to leverage JavaScript-only libraries on [npmjs.com](https://www.npmjs.com/), and many native libraries too.
- Your app will use the native controls, visual appearance, animations and colors, and therefore will feel integrated into the design language used in Windows. In addition, React Native for Windows apps do not have to compromise on the set of APIs they can call, as the framework allows you to call platform APIs as well as write your own view managers and native modules.
- Large and growing community momentum, with lots of [community modules](https://reactnative.directory).

> [!div class="button"]
> [Get started with React Native for Windows](https://aka.ms/ReactNativeGuideWindows)

For more information about React Native for Windows, see the following links:

- [React Native for Windows repo on GitHub](https://github.com/microsoft/react-native-windows)
- [Native modules in React Native for Windows](https://aka.ms/RNW-NativeModules)
- [API reference](https://microsoft.github.io/react-native-windows/docs/Native-API-Reference)
- [Community modules directory](https://reactnative.directory)
- [More resources](https://microsoft.github.io/react-native-windows/resources)

---

## Other app types

For more information about the app types you can choose from, see the following tabs.

### [Win32](#tab/cpp-win32)

Win32 desktop apps (also sometimes called *classic desktop apps*) are the original app type for native Windows applications that require direct access to Windows and hardware. This makes this the app type of choice for applications that need the highest level of performance and direct access to system hardware.

Using the Win32 API with C++ makes it possible to achieve the highest levels of performance and efficiency by taking more control of the target platform with unmanaged code than is possible on a managed runtime environment like WinRT and .NET. However, exercising such a level of control over your application's execution requires greater care and attention to get right, and trades development productivity for runtime performance.

Here are a few highlights of what the Win32 API and C++ offers to enable you to build high-performance applications.

- Hardware-level optimizations, including tight control over resource allocation, object lifetimes, data layout, alignment, byte packing, and more.
- Access to performance-oriented instruction sets like SSE and AVX through intrinsic functions.
- Efficient, type-safe generic programming by using templates.
- Efficient and safe containers and algorithms.
- DirectX, in particular Direct3D and DirectCompute (note that UWP also offers DirectX interop).
- Use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) to create modern desktop Win32 apps with first-class access to Windows Runtime (WinRT) APIs.

> [!div class="button"]
> [Get started with Win32](/windows/win32/desktop-programming/)

You also have access to modern Windows platform features and APIs provided by the **Windows App SDK**. For more information, see [Modernize your desktop apps](../desktop/modernize/index.md).

### [WPF](#tab/wpf)

WPF is a well-established platform for managed Windows applications with access to .NET or the .NET Framework, and it also uses XAML markup to separate UI from code. This platform is designed for desktop applications that require a sophisticated UI, styles customization, and graphics-intensive scenarios. WPF development skills are similar to WinUI 3 development skills, so migrating from WPF to WinUI 3 is easier than migrating from Windows Forms.

> [!div class="button"]
> [Get started with WPF](/dotnet/framework/wpf/getting-started/)

You also have access to modern Windows platform features and APIs provided by the **Windows App SDK**. For more information, see [Modernize your desktop apps](../desktop/modernize/index.md).

### [Windows Forms](#tab/windows-forms)

Windows Forms is the original platform for managed Windows applications with a lightweight UI model and access to .NET or the .NET Framework. It excels at enabling developers to quickly get started building applications, even for developers new to the platform. This is a forms-based, rapid application development platform with a large built-in collection of visual and non-visual drag-and-drop controls. Windows Forms does not use XAML, so deciding later to rewrite your application to WinUI 3 entails a complete re-write of your UI.

> [!div class="button"]
> [Get started with Windows Forms](/dotnet/framework/winforms/getting-started-with-windows-forms)

You also have access to modern Windows platform features and APIs provided by the **Windows App SDK**. For more information, see [Modernize your desktop apps](../desktop/modernize/index.md).

### [UWP](#tab/uwp)

The Universal Windows Platform (UWP) provides a common type system, APIs, and application model for all devices in the Universal Windows Platform. Not only can you use UWP to create desktop applications for Windows PCs, but UWP is also the only supported platform to write a single native universal app that runs across Xbox, HoloLens, and Surface Hub. UWP apps can be native or managed.

UWP is a highly customizable platform that uses XAML markup to separate UI (presentation) from code (business logic). UWP is suitable for desktop apps that require a sophisticated UI, styles customization, and graphics-intensive scenarios. UWP also has built-in support for the [Fluent Design System](/windows/uwp/design/fluent-design-system/) for the default UX experience and provides access to the [Windows Runtime (WinRT) APIs](/windows/uwp/get-started/universal-application-platform-guide#how-the-universal-windows-platform-relates-to-windows-runtime-apis).

> [!div class="button"]
> [Get started with UWP](/windows/uwp/get-started/)

You will not have access to the APIs provided by the **Windows App SDK**. To use the Windows App SDK, you will have to migrate your UWP app to WinUI 3. For more information, see [Migrate to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md).
