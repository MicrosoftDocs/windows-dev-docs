---
description: An overview of the Windows developer platform, SDKs, and app frameworks
title: Windows developer platform overview
ms.topic: overview
ms.date: 06/16/2026
keywords: windows, desktop development, windows sdk, windows app sdk, winui
ms.localizationpriority: medium
#customer intent: To understand the Windows developer platform, how the SDKs fit together, and how to choose the right app framework.
---

# Windows developer platform overview

The Windows developer platform provides everything you need to build apps for Windows — from system-level access to modern UI frameworks. At the core are two SDKs: the **Windows SDK**, which gives you access to OS-level APIs, and the **Windows App SDK**, which provides a modern, decoupled set of APIs and tools — including WinUI 3, the recommended UI framework for new Windows apps.

Together, these SDKs support a range of native and cross-platform app frameworks. Whether you're starting a new project with WinUI, modernizing an existing WPF or Windows Forms app, or building cross-platform with .NET MAUI or React Native, the platform gives you the flexibility to choose the right approach for your needs.

## Windows SDK

The [Windows SDK](../windows-sdk/index.md) provides the platform headers, libraries, and tools that give you direct access to the full set of Windows OS APIs. Every Windows app — regardless of framework — ultimately relies on the Windows SDK. It's tied to Windows OS releases and is the right choice when you need access to the latest OS features, low-level system APIs, or hardware capabilities like DirectX.

> [!div class="nextstepaction"]
> [Learn more about the Windows SDK](../windows-sdk/index.md)

---

## Windows App SDK

The [Windows App SDK](../windows-app-sdk/index.md) is a modern SDK that builds on top of the Windows SDK. Delivered as NuGet packages and decoupled from the OS, it provides APIs and tools — including [WinUI](../winui/winui3/index.md), app lifecycle, windowing, and notifications — on a faster release cycle than Windows itself. WinUI is built directly on the Windows App SDK, and you can also integrate its APIs into frameworks like WPF, Windows Forms, Win32, and others that target Windows. It supports Windows 10 (1809) and later.

> [!div class="nextstepaction"]
> [Learn more about the Windows App SDK](../windows-app-sdk/index.md)

---

## Choose your app framework

### WinUI

:::image type="content" source="images/winui-header.png" alt-text="WinUI logo.":::

[WinUI](../winui/winui3/index.md) is the recommended native UI framework for building modern Windows apps. Built on the [Windows App SDK](../windows-app-sdk/index.md), WinUI uses XAML markup and C# or C++ to create apps with the [Fluent Design](https://fluent2.microsoft.design/) look and feel that Windows users expect. If you're new to Windows development or starting a new project, WinUI is the best place to start.

<div class="buttons margin-top-xs">
    <a href="../winui/winui3/index.md"
       class="button button-sm">
        <span>Learn more about WinUI</span>
    </a>
    <a href="start-here.md"
       class="button button-sm">
        <span>Get started with WinUI</span>
    </a>
</div>

### Other frameworks

Windows also supports a variety of other native and cross-platform frameworks for building desktop apps.

#### [Native frameworks](#tab/native)

##### WPF

[WPF](/dotnet/desktop/wpf/overview/) is a well-established XAML-based framework for Windows desktop apps built on .NET. It provides a comprehensive set of features including controls, data binding, layout, graphics, and styles. If you have an existing WPF app, you can modernize it with the [Windows App SDK](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

[Get started with WPF →](/dotnet/desktop/wpf/overview/)

##### Windows Forms

[Windows Forms](/dotnet/desktop/winforms/overview/) is a rapid application development platform for .NET with a drag-and-drop visual designer and a large collection of built-in controls. It's a great choice for quickly building line-of-business and data-driven desktop apps. Existing Windows Forms apps can be modernized with the [Windows App SDK](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

[Get started with Windows Forms →](/dotnet/desktop/winforms/overview/)

##### Win32

[Win32](/windows/win32/) desktop apps (also called *classic desktop apps*) use C++ for direct access to Windows and hardware. This is the best choice for apps that need the highest levels of performance, hardware-level optimizations, and access to DirectX. You can use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) for modern access to Windows Runtime APIs.

[Get started with Win32 →](/windows/win32/desktop-programming/)

##### UWP

The [Universal Windows Platform (UWP)](/windows/uwp/) provides a common API surface for apps across all Windows device families. Existing UWP apps continue to function, but to take advantage of the latest features in WinUI and the Windows App SDK, consider [migrating your app](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md).

[Get started with UWP →](/windows/uwp/get-started/)

#### [Cross-platform frameworks](#tab/cross-platform)

##### React Native for Desktop

[React Native for Desktop](/windows/dev-environment/javascript/react-native-for-windows) lets you build native Windows apps using JavaScript or TypeScript. It's ideal for teams with web development experience who want to share code across platforms while still delivering a native user experience on Windows.

[Get started with React Native for Desktop →](https://aka.ms/ReactNativeGuideWindows)

##### .NET MAUI

[.NET MAUI](/dotnet/maui/) is a cross-platform framework for building apps that run on Android, iOS, macOS, and Windows from a single C# codebase. On Windows, .NET MAUI uses WinUI and the Windows App SDK under the hood, giving your apps a native experience while also reaching other platforms.

[Get started with .NET MAUI →](/dotnet/maui/get-started/installation)

##### Progressive Web Apps (PWAs)

[Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps-chromium/) are websites that function like installed native apps on Windows. Built with standard web technologies, PWAs can be pinned to the taskbar, added to the Start menu, and published to the Microsoft Store.

[Get started with PWAs →](/microsoft-edge/progressive-web-apps-chromium/how-to/)

---

## Feature comparison

| Feature | WinUI | WPF | Windows Forms | .NET MAUI | React Native | UWP | Win32 |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **Language** | C#, C++ | C#, Visual Basic | C#, Visual Basic | C# | JavaScript, TypeScript | C#, C++, Visual Basic | C++, Rust |
| **UI language** | XAML | XAML | Code | XAML/Code | JSX | XAML | Code |
| **UI designer** (drag & drop) | ❌ | ✅ | ✅ | ❌ | ❌ | ✅ | ❌ |
| **Modern UI** | ✅ | ✅ ([Fluent theme](/dotnet/desktop/wpf/whats-new/net90#fluent-theme)) | ❌ | ✅ | ✅ | ✅ ([WinUI 2](/windows/apps/winui/winui2)) | ❌ |
| **Cross-platform** | ❌ | ❌ | ❌ | ✅ | ✅ | ❌ | ❌ |
| **Sandboxing (AppContainer)** | ✅ | ❌ | ❌ | ❌ | ✅ | ✅ | ❌ |
| **Actively maintained** | ✅ | ✅ | ✅ | ✅ | ✅ | ⚠️ Security & bug fixes only | ✅ |
