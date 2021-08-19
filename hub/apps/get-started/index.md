---
description: Learn how to get started building new apps for Windows desktop and modernizing existing apps.
title: Get started developing apps for Windows desktop
ms.topic: article
ms.date: 06/24/2021
keywords: windows win32, desktop development
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Get started developing apps for Windows desktop

This article provides the info you need to get started building apps for the Windows desktop environment or updating existing apps to adopt the latest experiences in the Windows OS.

## Create new apps

When you want to create a new app for Windows 11 or Windows 10, the first decision you make is what type of app to build. The Windows and .NET development tools in Visual Studio provide several different types of apps you can build, each with their own Visual Studio project types and different strengths.

Each app type includes an app model that defines the lifecycle of the app, a default UI framework that lets you create apps like Word and Excel that run in the Windows desktop environment, and access to a comprehensive set of managed and native APIs for using Windows features. Some of these platforms share certain traits and are better suited for specific application types.

No matter which app type you choose to start with, you have access to most Windows platform features to deliver a modern experience in your app. For example, even if you build a WPF, Windows Forms, or classic Win32 desktop app, you can still use MSIX package deployment, Windows Runtime (WinRT) APIs provided by the Windows OS and the Windows SDK, and APIs provided by the [Windows App SDK](../windows-app-sdk/index.md). For more information, see [Update existing apps](#update-existing-apps) later in this article.

### App types

For more information about the app types you can choose from, see the following tabs.

#### [WinUI 3](#tab/winui3)

The Windows UI Library (WinUI) 3 is the premiere native user interface (UI) framework for Windows desktop apps, including managed apps that use C# and .NET and native apps that use C++ with the Win32 API. By incorporating the [Fluent Design System](https://www.microsoft.com/design/fluent/#/) into all experiences, controls, and styles, WinUI provides consistent, intuitive, and accessible experiences using the latest UI patterns.

To build a WinUI 3 app, start with one of the project templates available in the [Windows App SDK](../windows-app-sdk/index.md). The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any C++ Win32 or C# .NET app on a broad set of target Windows OS versions.

For more information about WinUI 3 apps, see see the following articles:

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Create your first WinUI 3 app](../winui/winui3/create-your-first-winui3-app.md)
- [WinUI project templates in Visual Studio](../winui/winui3/winui-project-templates-in-visual-studio.md)
- [WinUI 3 desktop apps and basic Win32 interop](../winui/winui3/desktop-winui3-app-with-basic-interop.md)
- [API reference](/windows/winui/api)
- [Samples](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3preview)

#### [Native Win32](#tab/cpp-win32)

Native Win32 desktop apps (also sometimes called *classic desktop apps*) are the original app type for native Windows applications that require direct access to Windows and hardware. This makes this the app type of choice for applications that need the highest level of performance and direct access to system hardware.

Using the Win32 API with C++ makes it possible to achieve the highest levels of performance and efficiency by taking more control of the target platform with unmanaged code than is possible on a managed runtime environment like WinRT and .NET. However, exercising such a level of control over your application's execution requires greater care and attention to get right, and trades development productivity for runtime performance.

Here are a few highlights of what the Win32 API and C++ offers to enable you to build high-performance applications.

- Hardware-level optimizations, including tight control over resource allocation, object lifetimes, data layout, alignment, byte packing, and more.
- Access to performance-oriented instruction sets like SSE and AVX through intrinsic functions.
- Efficient, type-safe generic programming by using templates.
- Efficient and safe containers and algorithms.
- DirectX, in particular Direct3D and DirectCompute (note that UWP also offers DirectX interop).
- Use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) to create modern desktop Win32 apps with first-class access to Windows Runtime (WinRT) APIs.

For more information about C++ Win32 apps, see the following articles:

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Get started with classic Win32 desktop apps](/windows/win32/desktop-programming/)
- [Get started with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/get-started)
- [Project templates](../desktop/visual-studio-templates.md#c-desktop-win32-templates)
- [Create your first Win32 and C++ app](/windows/win32/learnwin32/learn-to-program-for-windows/)
- [Technologies and features provided by the Win32 API](/windows/win32/desktop-app-technologies)
- [Win32 API reference](/windows/win32/apiindex/windows-api-list/)
- [Samples](https://github.com/Microsoft/Windows-classic-samples)

#### [WPF](#tab/wpf)

WPF is the established platform for managed Windows applications with access to .NET 5 or the .NET Framework, and it also uses XAML markup to separate UI from code. This platform is designed for desktop applications that require a sophisticated UI, styles customization, and graphics-intensive scenarios. WPF development skills are similar to UWP development skills, so migration from WPF to UWP apps is easier than migration from Windows Forms.

For more information about WPF apps, see the following articles:

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Getting started (WPF)](/dotnet/framework/wpf/getting-started/)
- [Project templates](../desktop/visual-studio-templates.md#net-templates)
- [Create your first app](/dotnet/desktop/wpf/get-started/create-app-visual-studio)
- [API reference (.NET)](/dotnet/api/index)
- [Samples](https://github.com/Microsoft/WPF-Samples)

#### [Windows Forms](#tab/windows-forms)

Windows Forms is the original platform for managed Windows applications with a lightweight UI model and access to .NET 5 or the .NET Framework. It excels at enabling developers to quickly get started building applications, even for developers new to the platform. This is a forms-based, rapid application development platform with a large built-in collection of visual and non-visual drag-and-drop controls. Windows Forms does not use XAML, so deciding later to extend your application to UWP entails a complete re-write of your UI.

For more information about Windows Forms apps, see the following articles:

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Getting started with Windows Forms](/dotnet/framework/winforms/getting-started-with-windows-forms)
- [Project templates](../desktop/visual-studio-templates.md#net-templates)
- [Create your first Windows Forms app](/dotnet/framework/winforms/creating-a-new-windows-form)
- [Tutorial: Create a picture viewer](/visualstudio/ide/tutorial-1-create-a-picture-viewer)
- [API reference (.NET)](/dotnet/api/index)
- [Enhancing Windows Forms apps](/dotnet/framework/winforms/advanced/)

#### [React Native for Windows](#tab/rnw)
[React Native](https://reactnative.dev) is a development platform from Facebook which allows building cross-platform apps.
[React Native for Windows](https://aka.ms/reactnative) brings React Native support for the Windows 10 and Windows 11 SDKs, enabling you to use JavaScript to build native Windows apps for all devices supported by Windows 10 and Windows 11, including PCs, tablets, 2-in-1s, Xbox, Mixed reality devices, etc. 

With React Native for Windows, you write most or all of your app code in JavaScript - or TypeScript - and the framework produces a native UWP XAML application. If your app needs to call a platform API, you can usually do so through one of the many [community modules](https://reactnative.directory), or if a module does not yet exist, you can easily [write a native module to expose it](https://aka.ms/RNW-NativeModules).

Here are some reasons to choose React Native for Windows:
- You want to share code across platforms as much as possible, or you have web properties that you want to share code with.
- Improved developer productivity and inner loop, thanks to fast refresh.
- Your app's fundamentals (performance, accessibility, internationalization) are as good as a native UWP app.
- You have experience with and a preference for JavaScript or TypeScript
- You would like to leverage JavaScript-only libraries on [npmjs.com](https://www.npmjs.com/), and many native libraries too.
- Your app will use the native controls, visual appearance, animations and colors, and therefore will feel integrated into the design language used in Windows. In addition, React Native for Windows apps do not have to compromise on the set of APIs they can call, as the framework allows you to call platform APIs as well as write your own view managers and native modules.
- Large and growing community momentum, with lots of [community modules](https://reactnative.directory).

For more information about React Native for Windows, see the following links:
* [React Native for Windows repo on GitHub](https://github.com/microsoft/react-native-windows)
* [Getting started with React Native for Windows](https://aka.ms/ReactNativeGuideWindows)
* [Native modules in React Native for Windows](https://aka.ms/RNW-NativeModules)
* [API reference](https://microsoft.github.io/react-native-windows/docs/Native-API-Reference)
* [Community modules directory](https://reactnative.directory)
* [More resources](https://microsoft.github.io/react-native-windows/resources)


#### [UWP](#tab/uwp)

The Universal Windows Platform (UWP) provides a common type system, APIs, and application model for all devices that run Windows 10 and later versions. Not only can you use UWP to create desktop applications for Windows PCs, but UWP is also the only supported platform for Xbox, HoloLens, and Surface Hub applications. UWP apps can be native or managed.

UWP is a highly customizable platform that uses XAML markup to separate UI (presentation) from code (business logic). UWP is suitable for desktop apps that require a sophisticated UI, styles customization, and graphics-intensive scenarios. UWP also has built-in support for the [Fluent Design System](/windows/uwp/design/fluent-design-system/) for the default UX experience and provides access to the [Windows Runtime (WinRT) APIs](/windows/uwp/get-started/universal-application-platform-guide#how-the-universal-windows-platform-relates-to-windows-runtime-apis).

For more information about UWP apps, see the following articles:

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Get started](/windows/uwp/get-started/)
- [Project templates](../desktop/visual-studio-templates.md#uwp-templates)
- [Design and UI](/windows/uwp/design/)
- [Technologies and features](/windows/uwp/develop/)
- [API reference](/uwp/)
- [Samples](https://github.com/Microsoft/Windows-universal-samples)

---

## Update existing apps

If you have an existing WPF, Windows Forms, or native Win32 desktop app, the Windows OS and the Windows App SDK offer many features you can use to deliver a modern experience in your app. Most of these features are available as modular components that you can adopt in your app at your own pace without having to rewrite your app for a different platform.

Here are just a few of the features available to enhance your existing desktop apps:

- [Install the Windows App SDK NuGet package](/windows-app-sdk/get-started.md#use-the-windows-app-sdk-in-an-existing-project) in your existing project to call Windows App SDK APIs for localizing resources, rendering text, and more in your app.
- [Call Windows Runtime (WinRT) APIs](../desktop/modernize/desktop-to-uwp-enhance.md) to enhance your desktop app with the latest Windows features.
- Use [package extensions](../desktop/modernize/desktop-to-uwp-extensions.md) to integrate your desktop app with modern Windows experiences. For example, point Start tiles to your app, make your app a share target, or send toast notifications from your app.
- Use [XAML Islands](../desktop/modernize/xaml-islands.md) to host WinRT XAML controls in your desktop app. Many of the latest Windows UI features are only available to WinRT XAML controls.
- Use [MSIX](/windows/msix/) to package and deploy your desktop apps. MSIX is a modern Windows app package format that provides a universal packaging experience for all Windows apps. MSIX brings together the best aspects of MSI, .appx, App-V and ClickOnce installation technologies and is built to be safe, secure, and reliable.

For more information, see [Modernize desktop apps](../desktop/modernize/index.md).

## Related topics

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
