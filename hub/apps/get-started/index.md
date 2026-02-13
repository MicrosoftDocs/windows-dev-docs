---
description: An overview of the Windows developer platform, SDKs, and app frameworks
title: Choosing your app framework
ms.topic: overview
ms.date: 02/13/2026
keywords: windows, desktop development, windows sdk, windows app sdk, winui
ms.localizationpriority: medium
#customer intent: To understand the Windows developer platform, how the SDKs fit together, and how to choose the right app framework.
---

# Windows developer platform

The Windows developer platform provides SDKs, frameworks, and tools for building apps that run on Windows. Whether you're creating a brand-new app or modernizing an existing one, Windows gives you the flexibility to choose the right approach for your project.

At the core of the platform are two SDKs that work together: the **Windows SDK** and the **Windows App SDK**. The Windows SDK gives you access to the full breadth of OS-level APIs, while the Windows App SDK provides a modern, decoupled layer of APIs and tools on top — including WinUI, the recommended UI framework for new Windows apps.

Once you understand how the SDKs fit together, the next step is choosing an app framework. Windows supports a wide range of options — from native frameworks like WinUI, WPF, and Win32 to cross-platform solutions like .NET MAUI and React Native. The right choice depends on your project requirements, your team's skillset, and whether you need to target platforms beyond Windows.

## Windows SDK

The [Windows SDK](../windows-sdk/index.md) is the foundational SDK for building Windows applications. It contains the platform headers, libraries, WinRT metadata, and build tools that give you direct access to the full set of Windows OS APIs. Every Windows app — whether it uses Win32, UWP, or a higher-level framework — ultimately relies on the capabilities the Windows SDK provides.

The Windows SDK is tied to Windows OS releases and is the right choice when you need access to the latest OS-level features, low-level system APIs, or hardware capabilities like DirectX graphics and driver interfaces.

[Learn more about the Windows SDK →](../windows-sdk/index.md)

## Windows App SDK

The [Windows App SDK](../windows-app-sdk/index.md) is a modern SDK that builds on top of the Windows SDK. It provides a unified set of APIs and tools — delivered as NuGet packages — that you can use across WinUI, WPF, Windows Forms, and Win32 apps. Because it's decoupled from the OS, you get access to new features on a faster release cycle without requiring your users to update Windows.

The Windows App SDK includes [WinUI](../winui/winui3/index.md) (the native UI framework), along with APIs for app lifecycle management, windowing, notifications, resource management, and more. If you're building a new app or modernizing an existing one, the Windows App SDK gives you a consistent, modern foundation that works across Windows 10 (version 1809) and later.

[Learn more about the Windows App SDK →](../windows-app-sdk/index.md)

## Choose your app framework

### WinUI

:::image type="content" source="images/winui-header.png" alt-text="WinUI logo.":::

[WinUI](../winui/index.md) is the recommended native UI framework for building modern Windows apps. Built on the [Windows App SDK](../windows-app-sdk/index.md), WinUI uses XAML markup and C# or C++ to create apps with the [Fluent Design](https://fluent2.microsoft.design/) look and feel that Windows users expect. If you're new to Windows development or starting a new project, WinUI is the best place to start.

:::row:::
    :::column:::
        > [!div class="nextstepaction"]
        > [Learn more about WinUI](../winui/index.md)
    :::column-end:::
    :::column:::
        > [!div class="nextstepaction"]
        > [Get started with WinUI](start-here.md)
    :::column-end:::
:::row-end:::

### [Native frameworks](#tab/native)

#### WPF

[WPF](/dotnet/desktop/wpf/overview/) is a well-established XAML-based framework for Windows desktop apps built on .NET. It provides a comprehensive set of features including controls, data binding, layout, graphics, and styles. If you have an existing WPF app, you can modernize it with the [Windows App SDK](../windows-app-sdk/wpf-plus-winappsdk.md).

[Get started with WPF →](/dotnet/desktop/wpf/overview/)

#### Windows Forms

[Windows Forms](/dotnet/desktop/winforms/overview/) is a rapid application development platform for .NET with a drag-and-drop visual designer and a large collection of built-in controls. It's a great choice for quickly building line-of-business and data-driven desktop apps. Existing Windows Forms apps can be modernized with the [Windows App SDK](../windows-app-sdk/winforms-plus-winappsdk.md).

[Get started with Windows Forms →](/dotnet/desktop/winforms/overview/)

#### Win32

[Win32](/windows/win32/) desktop apps (also called *classic desktop apps*) use C++ for direct access to Windows and hardware. This is the best choice for apps that need the highest levels of performance, hardware-level optimizations, and access to DirectX. You can use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) for modern access to Windows Runtime APIs.

[Get started with Win32 →](/windows/win32/desktop-programming/)

#### UWP

The [Universal Windows Platform (UWP)](/windows/uwp/) provides a common API surface for apps across all Windows device families. Existing UWP apps continue to function, but to take advantage of the latest features in WinUI and the Windows App SDK, consider [migrating your app](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md).

[Get started with UWP →](/windows/uwp/get-started/)

### [Cross-platform frameworks](#tab/cross-platform)

#### React Native for Desktop

[React Native for Desktop](/windows/dev-environment/javascript/react-native-for-windows) lets you build native Windows apps using JavaScript or TypeScript. It's ideal for teams with web development experience who want to share code across platforms while still delivering a native user experience on Windows.

[Get started with React Native for Desktop →](https://aka.ms/ReactNativeGuideWindows)

#### .NET MAUI

[.NET MAUI](/dotnet/maui/) is a cross-platform framework for building apps that run on Android, iOS, macOS, and Windows from a single C# codebase. On Windows, .NET MAUI uses WinUI and the Windows App SDK under the hood, giving your apps a native experience while also reaching other platforms.

[Get started with .NET MAUI →](/dotnet/maui/get-started/installation)

#### Progressive Web Apps (PWAs)

[Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps-chromium/) are websites that function like installed native apps on Windows. Built with standard web technologies, PWAs can be pinned to the taskbar, added to the Start menu, and published to the Microsoft Store.

[Get started with PWAs →](/microsoft-edge/progressive-web-apps-chromium/how-to/)

---

## Feature comparison

| Feature | WinUI | WPF | Windows Forms | .NET MAUI | React Native | UWP | Win32 |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **Language** | C#, C++ | C#, Visual Basic | C#, Visual Basic | C# | JavaScript, TypeScript | C#, C++, Visual Basic | C++, Rust |
| **UI language** | XAML | XAML | Code | XAML/Code | JSX | XAML | Code |
| **UI designer** (drag & drop) | ❌ | ✅ | ✅ | ❌ | ❌ | ✅ | ❌ |
| **Modern UI** | ✅ | ✅ ([Fluent theme](/dotnet/desktop/wpf/whats-new/net10#fluent-theme)) | ❌ | ✅ | ✅ | ✅ ([WinUI 2](/windows/apps/winui/winui2)) | ❌ |
| **Cross-platform** | ❌ | ❌ | ❌ | ✅ | ✅ | ❌ | ❌ |
| **Sandboxing (AppContainer)** | ❌ | ❌ | ❌ | ❌ | ✅ | ✅ | ❌ |
| **Actively maintained** | ✅ | ✅ | ✅ | ✅ | ✅ | ⚠️ Security & bug fixes only | ✅ |
