---
description: An overview of Windows app development frameworks
title: Overview of framework options
ms.topic: article
ms.date: 09/06/2024
keywords: windows, desktop development
ms.localizationpriority: medium
---

# Overview of framework options

This article contains the information you need to get started building apps for Windows.

Windows offers a wide range of languages, frameworks, and tools for building apps, including WinUI, WPF, C++, C#, .NET, and a variety of cross-platform frameworks. Here, we provide information to help you decide which option is best for you.

## WinUI

:::image type="content" source="images/winui-header.png" alt-text=".":::

We recommend WinUI and the Windows App SDK to create apps that look great and take advantage of the latest Windows releases. If you're new to Windows development, or starting work on a new Windows app, WinUI provides the resources you need to create great [apps for Windows 11](https://www.microsoft.com/en-us/windows/windows-11-apps).
<!-- The en-us is needed in this link. Please leave it there.  -->

[WinUI](../winui/index.md) is a XAML markup-based user interface layer that contains modern controls and styles for building Windows apps. As the native UI layer for the Windows App SDK, it embodies [Fluent Design](https://fluent2.microsoft.design/), giving each Windows app the polished feel that customers expect.

> [!div class="button"]
> [Get started with WinUI](start-here.md)

> [!NOTE]
> The [Windows App SDK](../windows-app-sdk/index.md) is a set of new developer components and tools that represent the latest evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by desktop apps on Windows 11 and downlevel to Windows 10, version 1809.
>
> While WinUI is the native UI layer, you can use the Windows App SDK with WPF, WinForms, or Win32 apps. If you've developed apps for Windows before, but are looking to get started with the Windows App SDK in an existing app, see [Framework-specific guides](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

## WPF

WPF is a well-established framework for Windows desktop applications with access to [.NET](/dotnet/desktop/wpf/overview/) or the [.NET Framework](/dotnet/framework/wpf/getting-started/). Like WinUI, it also uses XAML markup to separate UI from code. WPF provides a comprehensive set of application development features that include controls, data binding, layout, 2D and 3D graphics, animation, styles, templates, documents, media, text, and typography. WPF is part of .NET, so you can build applications that incorporate other elements of the .NET API.

Additionally, you can now integrate a sandbox environment into your packaged WPF applications, providing an additional layer of security. This enhancement requires little to no change to your code, thanks to the new [Win32 App Isolation](https://github.com/microsoft/win32-app-isolation) security feature.

> [!div class="button"]
> [Get started with WPF](/dotnet/desktop/wpf/overview/)

If you have a [WPF .NET](/dotnet/desktop/wpf/overview/) app, you also have access to modern Windows platform features and APIs provided by the **Windows App SDK**. For more information, see [Use the Windows App SDK in a WPF app](/windows/apps/windows-app-sdk/wpf-plus-winappsdk) and [Modernize your desktop apps](../desktop/modernize/index.md).

> [!TIP]
> If you need more help deciding which framework is the best choice for your app, see the [**Choose the best application framework for a Windows development project**](/training/modules/windows-choose-best-app-framework/) training module.

## Other native platform options

Many apps for Windows are written using [Win32](/windows/win32/), [Windows Forms](/dotnet/desktop/winforms/), or [UWP](/windows/uwp). Each of these frameworks is supported and will continue to receive bug, reliability, and security fixes, but varying levels of investment for new features and styles. For more information about these app types see the following tabs.

### [Win32](#tab/cpp-win32)

Win32 desktop apps (also sometimes called *classic desktop apps*) are the original app type for native Windows applications that require direct access to Windows and hardware. This makes Win32 the app type of choice for applications that need the highest level of performance and direct access to system hardware.

Using the Win32 API with C++ makes it possible to achieve the highest levels of performance and efficiency by taking more control of the target platform with un-managed code than is possible on a managed runtime environment like WinRT and .NET. However, exercising such a level of control over your application's execution requires greater care and attention to get right, and trades development productivity for runtime performance.

Here are a few highlights of what the Win32 API and C++ offers to enable you to build high-performance applications.

- Hardware-level optimizations, including tight control over resource allocation, object lifetimes, data layout, alignment, byte packing, and more.
- Access to performance-oriented instruction sets like SSE and AVX through intrinsic functions.
- Efficient, type-safe generic programming by using templates.
- Efficient and safe containers and algorithms.
- DirectX, in particular Direct3D and DirectCompute.
- Use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) to create modern desktop Win32 apps with first-class access to Windows Runtime (WinRT) APIs.

Additionally, you can now integrate a sandbox environment into your Win32 applications, providing an additional layer of security. This enhancement requires little to no change to your code, thanks to the new [Win32 App Isolation](https://github.com/microsoft/win32-app-isolation) security feature.

> [!div class="button"]
> [Get started with Win32](/windows/win32/desktop-programming/)

You also have access to modern Windows platform features and APIs provided by the **Windows App SDK**. For more information, see [Use the Windows App SDK in an existing project](/windows/apps/windows-app-sdk/use-windows-app-sdk-in-existing-project) and [Modernize your desktop apps](../desktop/modernize/index.md).

### [Windows Forms](#tab/windows-forms)

Windows Forms is the original platform for managed Windows applications with a lightweight UI model and access to [.NET](/dotnet/desktop/winforms/overview) or the [.NET Framework](/dotnet/framework/winforms/getting-started-with-windows-forms). It excels at enabling developers to quickly get started building applications, even for developers new to the platform. This is a forms-based, rapid application development platform with a large built-in collection of visual and non-visual drag-and-drop controls. Windows Forms does not use XAML, so deciding later to rewrite your application to WinUI entails a complete re-write of your UI.

Additionally, you can now integrate a sandbox environment into your packaged Windows Forms applications, providing an additional layer of security. This enhancement requires little to no change to your code, thanks to the new [Win32 App Isolation](https://github.com/microsoft/win32-app-isolation) security feature.

> [!div class="button"]
> [Get started with Windows Forms](/dotnet/desktop/winforms/overview)

If you have a [Windows Forms .NET](/dotnet/desktop/winforms/overview/) app, you also have access to modern Windows platform features and APIs provided by the **Windows App SDK**. For more information, see [Use the Windows App SDK in a Windows Forms (WinForms) app](/windows/apps/windows-app-sdk/winforms-plus-winappsdk) and [Modernize your desktop apps](../desktop/modernize/index.md).

### [UWP](#tab/uwp)

The Universal Windows Platform (UWP) provides a common type system, APIs, and application model for all devices in the Universal Windows Platform. Not only can you use UWP to create desktop applications for Windows PCs, but UWP is also the only supported platform to write a single native universal app that runs across Xbox, HoloLens, and Surface Hub. UWP apps can be native or managed.

> [!NOTE]
> Your existing UWP app will continue to function as expected. However, to take advantage of modern features in [WinUI 3](/windows/apps/winui/winui3) and the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/) we recommend migrating your app.

> [!div class="button"]
> [Get started with UWP](/windows/uwp/get-started/)

You will not have access to the APIs provided by the **Windows App SDK** or .NET 6 and later. To use the Windows App SDK, you will have to migrate your UWP app to WinUI and the Windows App SDK. For more information, see [Migrate to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md).

---

## Cross-platform options

If you need your app to be cross-platform, consider [.NET MAUI](/dotnet/maui/what-is-maui), a [Progressive Web App (PWA)](/microsoft-edge/progressive-web-apps-chromium/), or [React Native for Windows](../../dev-environment/javascript/react-native-for-windows.md). There are many other choices available ([here's a list of popular options](../../dev-environment/index.md)), but these are some good starting points.

.NET MAUI harnesses the power of WinUI on Windows, while also enabling execution on other operating systems. React Native for Windows lets you write apps that run on all devices supported by Windows 10 and Windows 11 (not just PCs). Another cross-platform option, Progressive Web Apps (PWAs), are websites that function like installed, native apps on Windows and other supported platforms, while functioning like regular websites on browsers.

For more information, see the following tabs.

### [.NET MAUI](#tab/net-maui)

.NET Multi-platform App UI (MAUI) is an open-source, cross-platform framework for building Android, iOS, macOS, and Windows applications that leverage the native UI and services of each platform from a single .NET code base. Because .NET MAUI favors platform native experiences, it uses WinUI and the Windows App SDK so apps get the latest user experience on Windows. This gives your apps access to everything you get with WinUI plus the ability to reach to other platforms.

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
- [Build Windows apps with .NET MAUI](../windows-dotnet-maui/index.md)
- [Resources for learning .NET MAUI](/dotnet/maui/get-started/resources/)
- [Video Series - .NET MAUI for Beginners](/shows/dotnet-maui-for-beginners/)
- [Build 2022: Build native apps for any device with .NET and Visual Studio](https://www.youtube.com/watch?v=IbwgHJPoE-M)

### [Progressive Web Apps (PWAs)](#tab/pwa)

[Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps-chromium/) provide access to open web technologies to provide cross-platform interoperability. PWAs provide your users with an app-like experience that's customized for their devices. PWAs are websites that are [progressively enhanced](https://alistapart.com/article/understandingprogressiveenhancement) to function like installed, native apps on supporting platforms (including Windows), while functioning like regular websites on other browsers.

When installed on Windows, PWAs are just like other apps. For example:

- A PWA can be added to the Start menu.
- A PWA can be pinned to the Taskbar.
- PWAs can handle files.
- PWAs can run when the user signs in.
- PWAs can be submitted to the Microsoft Store where millions of Windows users can discover and easily install them alongside other Windows apps.

> [!div class="button"]
> [Get started with PWAs](/microsoft-edge/progressive-web-apps-chromium/how-to/)

For more information about building PWAs, see the following links:

- [Overview of PWAs](/microsoft-edge/progressive-web-apps-chromium/)
- [Publish a PWA to the Microsoft Store](/microsoft-edge/progressive-web-apps-chromium/how-to/microsoft-store)
- [Re-engage users with badges, notifications, and push messages](/microsoft-edge/progressive-web-apps-chromium/how-to/notifications-badges)
- [Build PWA-driven widgets](/microsoft-edge/progressive-web-apps-chromium/how-to/widgets)
- [Progressive Web App demos](/microsoft-edge/progressive-web-apps-chromium/demo-pwas)
- [PWABuilder - Helping developers build and publish PWAs](https://www.pwabuilder.com/)

### [React Native for Windows](#tab/rnw)

[React Native](https://reactnative.dev) is a development platform which allows building cross-platform apps.
React Native for Windows brings React Native support to the Windows 10 and Windows 11 SDKs, letting you use JavaScript to build native Windows apps for all devices supported by Windows 10 and Windows 11. This includes PCs, tablets, 2-in-1s, Xbox, Mixed Reality devices, etc.

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

## App development framework feature comparison

There is a wide range of options for developing applications for Windows. The best option for you depends on your application requirements, your existing code, and your familiarity with the technology. The following table lists the most popular app development frameworks available on Windows and the features supported by each framework.

| Feature | .NET MAUI | Blazor Hybrid | React Native (RNW) | UWP XAML (Windows.UI.Xaml) | Win32 (MFC or ATL) | Windows Forms | WinUI 3 | WPF |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| **Language** | C# | C# | JavaScript, TypeScript | C#, C++, Visual Basic | C++, Rust | C#, Visual Basic | C#, C++ | C#, Visual Basic |
| **UI language** | XAML/Code | Razor | JSX | XAML | Code | Code | XAML | XAML |
| **UI designer**<br/>(drag & drop) | ❌ | ❌ | ❌ | ✅ | ❌ | ✅ | ❌ | ✅ |
| **UI debugging** | [Hot Reload](/dotnet/maui/xaml/hot-reload) | [Hot Reload](/aspnet/core/test/hot-reload) | [Fast Refresh](https://reactnative.dev/docs/fast-refresh) | [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) | - | [Hot Reload](/visualstudio/debugger/hot-reload) | [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) | [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) |
| **Fluent Design** | ✅ | ✅ | ✅ | ✅ (via [WinUI 2](/windows/apps/winui/winui2)) | ❌ | ❌ | ✅ | ❌ |
| **.NET** | .NET | .NET | N/A | .NET Core & .NET Native | N/A | .NET & .NET Framework | .NET | .NET & .NET Framework |
| **Windows App SDK** | ✅ ([more info](/dotnet/maui/platform-integration/invoke-platform-code)) | ✅ [via MAUI](/dotnet/maui/platform-integration/invoke-platform-code) | ✅ ([more info](https://techcommunity.microsoft.com/t5/modern-work-app-consult-blog/getting-started-with-react-native-for-windows/ba-p/912093)) | ❌ | ✅ | ✅ ([more info](../windows-app-sdk/migrate-to-windows-app-sdk/winforms-plus-winappsdk.md)) | ✅ | ✅ ([more info](../windows-app-sdk/migrate-to-windows-app-sdk/wpf-plus-winappsdk.md)) |
| **Great for touch** | ✅ | ✅ | ✅ | ✅ | ❌ | ❌ | ✅ | ❌ |
| **Cross-platform** | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Xbox/HoloLens apps** | ❌ | ❌ | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| **Sandboxing (AppContainer)** | ❌ | ❌ | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| **Currently supported** | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Receiving updates** | ✅ | ✅ | ✅ | ✅ (security & bugfix) | ✅ | ✅ | ✅ | ✅ |
| **Roadmap** | [GitHub](https://github.com/dotnet/maui/wiki/Roadmap) | [GitHub](https://aka.ms/aspnet/roadmap) | [GitHub](https://aka.ms/rnw-roadmap) | n/a | n/a | [GitHub](https://github.com/dotnet/winforms/blob/main/docs/roadmap.md) | [GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/roadmap.md) | [GitHub](https://github.com/dotnet/wpf/blob/main/roadmap.md) |

Learn more about each of these options:

- [.NET Multi-platform App UI (.NET MAUI)](/dotnet/maui/)
- [ASP.NET Core Blazor Hybrid](/aspnet/core/blazor/hybrid)
- [React Native for Windows (RNW)](/windows/dev-environment/javascript/react-native-for-windows)
- [Universal Windows Platform (UWP)](/windows/uwp/)
- [Recommendations for Choosing Between ATL and MFC](/cpp/atl/recommendations-for-choosing-between-atl-and-mfc)
- [Windows Forms](/dotnet/desktop/winforms/)
- [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
- [WinUI in the Windows App SDK (WinUI 3)](/windows/apps/winui/winui3/)

## Next steps

- [Use WinUI to start developing apps for Windows](start-here.md)
  > WinUI is our recommended platform for Windows apps, and these steps will quickly get you started.
- [Set up your development environment on Windows](/windows/dev-environment/)
  > Windows isn't just great for developing apps that run on Windows, it's also a powerful environment for developing apps for any platform. Learn more about the tools and options available to maximize your development.
