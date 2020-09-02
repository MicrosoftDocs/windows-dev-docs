---
Description: When you want to create a new desktop app, the first decision you make is whether to use the Win32 and COM API or .NET.
ms.assetid: 82705644-F1F0-40F3-99B1-7A97BFB32831
title: Choose your app platform
ms.topic: article
ms.date: 11/04/2019
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
keywords: windows win32, desktop development
---

# Choose your app platform

When you want to create a new desktop application for Windows PCs, the first decision you make is which application platform to use. Windows provides four main application platforms, each with different strengths:

* [Universal Windows Platform (UWP)](#uwp): This platform provides a common type system, APIs, and application model for all devices that run Windows 10. UWP applications can be native or managed.
* [WPF](#wpf) and [Windows Forms](#windows-forms): These .NET-based platforms provide a common type system, APIs, and application model for managed applications.
* [Win32](#win32): This is the original platform for native C/C++ Windows applications that require direct access to Windows and hardware. This makes the Win32 API the platform of choice for applications that need the highest level of performance and direct access to system hardware.

Each of these platforms include a complete UI framework and set of UI controls that let you create desktop apps like Word, Excel, and Photoshop that run in the classic Windows desktop and take full advantage of that environment's specific features. On Windows 10, each these platforms also support using the [Windows UI (WinUI) Library](#windows-ui-library) to create their user interfaces.

Some of these platforms share some traits and are better suited for certain types of applications. For example, both UWP and .NET have deep integration with Visual Studio. This provides many benefits, especially in the areas of developer productivity, sophisticated and customizable UI, and application security. Because these frameworks support visual designers and UI markup for rapidly creating UI, they are particularly well-suited for line-of-business applications.

> [!NOTE]
> No matter which app platform you choose, you can use many Windows 10 features to deliver a modern experience in your app. For example, even if your desktop app is built using WPF, Windows Forms, or the Win32 API, you can still use MSIX package deployment. For more information about all the ways to modernize your desktop apps, see [Modernize your desktop apps](modernize/index.md).

## UWP

UWP is the leading-edge platform for Windows 10 applications and games. It's a highly customizable platform that uses XAML markup to separate UI (presentation) from code (business logic). UWP is suitable for desktop applications that require a sophisticated UI, styles customization, and graphics-intensive scenarios. UWP also has built-in support for the [Fluent Design System](/windows/uwp/design/fluent-design-system/) for the default UX experience and provides access to the [Windows Runtime (WinRT) APIs](/windows/uwp/get-started/universal-application-platform-guide#how-the-universal-windows-platform-relates-to-windows-runtime-apis). By adopting Fluent, UWP automatically supports common input methods such as ink, touch, gamepad, keyboard, and mouse.

Not only can you use UWP to create desktop applications for Windows PCs, but UWP is also the only supported platform for Xbox, HoloLens, and Surface Hub applications. UWP is our newest, leading-edge application platform.

For more information about UWP, see the following articles:

* [Get started](/windows/uwp/get-started/)
* [Project templates](visual-studio-templates.md#uwp-templates)
* [Design and UI](/windows/uwp/design/)
* [Technologies and features](/windows/uwp/develop/)
* [API reference](/uwp/)
* [Samples](https://github.com/Microsoft/Windows-universal-samples)

## WPF

WPF is the established platform for managed Windows applications with access to .NET Core or the full .NET Framework, and it also uses XAML markup to separate UI from code. This platform is designed for desktop applications that require a sophisticated UI, styles customization, and graphics-intensive scenarios. WPF development skills are similar to UWP development skills, so migration from WPF to UWP apps is easier than migration from Windows Forms.

For more information about WPF, see the following articles:

* [Getting started (WPF)](/dotnet/framework/wpf/getting-started/)
* [Project templates](visual-studio-templates.md#net-templates)
* [Create your first app (.NET Core)](/visualstudio/get-started/csharp/tutorial-wpf/)
* [Create your first app (.NET Framework)](/dotnet/framework/wpf/getting-started/walkthrough-my-first-wpf-desktop-application/)
* [Migrate WPF apps to .NET Core](/dotnet/desktop-wpf/migration/convert-project-from-net-framework/)
* [API reference (.NET)](/dotnet/api/index)
* [Samples](https://github.com/Microsoft/WPF-Samples)

## Windows Forms

Windows Forms is the original platform for managed Windows applications with a lightweight UI model and access to .NET Core or the full .NET Framework. It excels at enabling developers to quickly get started building applications, even for developers new to the platform. This is a forms-based, rapid application development platform with a large built-in collection of visual and non-visual drag-and-drop controls. Windows Forms does not use XAML, so deciding later to extend your application to UWP entails a complete re-write of your UI.

For more information about Windows Forms, see the following articles:

* [Getting started with Windows Forms](/dotnet/framework/winforms/getting-started-with-windows-forms)
* [Project templates](visual-studio-templates.md#net-templates)
* [Create your first Windows Forms app](/dotnet/framework/winforms/creating-a-new-windows-form)
* [Tutorial: Create a picture viewer](/visualstudio/ide/tutorial-1-create-a-picture-viewer?view=vs-2019)
* [API reference (.NET)](/dotnet/api/index)
* [Enhancing Windows Forms apps](/dotnet/framework/winforms/advanced/)

## Win32

Using the Win32 API with C++ makes it possible to achieve the highest levels of performance and efficiency by taking more control of the target platform with unmanaged code than is possible on a managed runtime environment like WinRT and .NET. However, exercising such a level of control over your application's execution requires greater care and attention to get right, and trades development productivity for runtime performance.

Here are a few highlights of what the Win32 API and C++ offers to enable you to build high-performance applications.

* Hardware-level optimizations, including tight control over resource allocation, object lifetimes, data layout, alignment, byte packing, and more.
* Access to performance-oriented instruction sets like SSE and AVX through intrinsic functions.
* Efficient, type-safe generic programming by using templates.
* Efficient and safe containers and algorithms.
* DirectX, in particular Direct3D and DirectCompute (note that UWP also offers DirectX interop).

For more information, see the following articles:

* [Get started](/windows/win32/desktop-programming/)
* [Project templates](visual-studio-templates.md#cwin32-templates)
* [Create your first Win32 and C++ app](/windows/win32/learnwin32/learn-to-program-for-windows/)
* [Technologies and features](/windows/win32/desktop-app-technologies)
* [API reference](/windows/win32/apiindex/windows-api-list/)
* [Samples](https://github.com/Microsoft/Windows-classic-samples)

## Windows UI Library

On Windows 10, each of the main desktop platforms also support using the [Windows UI (WinUI) Library](../winui/index.md) to create their user interfaces. WinUI started as a toolkit that provided new and updated versions of UWP controls for UWP apps that target down-level versions of Windows 10. WinUI has grown in scope, and is now the modern native user interface (UI) platform for Windows 10 apps across UWP, .NET, and Win32.

You can use WinUI in the following ways in desktop apps:

* UWP apps can use WinUI controls in place of UWP controls provided by the Windows SDK.
* You can update existing WPF, Windows Forms, and C++/Win32 apps to use [XAML Islands](modernize/xaml-islands.md) to host WinUI 2.x controls in the apps.
* Starting with [WinUi 3.0](../winui/winui3/index.md), you can create [.NET and C++/Win32 apps that use an entirely WinUI-based UI](../winui/winui3/get-started-winui3-for-desktop.md).

## Platform comparison: UWP, WPF, and Windows Forms

The following table compares various characteristics of Windows Forms, WPF, and UWP in detail.

| Feature or scenario  |    UWP     |      WPF     |   Windows Forms  |
|--------|--------|--------|--------|
| **Supported versions**      |  Windows 10   |  Windows 7 and later |  Windows 7 and later  |
| **Languages**      |   C\#, C++/WinRT, C++/CX, VB, JavaScript   |  C\#, C++/CLI (Managed Extensions for C++), F\#, VB |  C\#, C++/CLI (Managed Extensions for C++), F\#, VB   |
| **UI runtime** |    Native (C++/WinRT and C++/CX) and managed (.NET Native)  |  Managed (.NET Framework and .NET Core 3) |   Managed (.NET Framework and .NET Core 3)   |
| **Open source** | [Yes (Windows UI Library only)](https://github.com/Microsoft/microsoft-ui-xaml)  |  [Yes (.NET Core only)](https://github.com/dotnet/wpf) | [Yes (.NET Core only)](https://github.com/dotnet/winforms)  |
| **Supports XAML** |   Yes   |  Yes  |   No   |
| **Strengths**  |  <ul><li>XAML markup for UI</li><li>Rich and customizable UX</li><li>Your existing code bases are .NET Standard compliant</li><li>High DPI support</li><li>Support for multiple input types across Windows devices (including touch, pen, gamepad, mouse, and keyboard)</li><li>Support for Xbox, HoloLens, IoT or Surface Hub</li><li>Optional dense (compact) UI</li><li>Support for native C++</li><li>Optimized battery life</li><li>Modern accessibility support (such as screen readers)</li><li>Rich text data capabilities (such as built-in spell check)</li><li>Inking support</li><li>Secure execution via application containers (for example, untrusted content is sandboxed)</li></ul>  |  <ul><li>XAML markup for UI</li><li>Rich and customizable UX</li><li>Large collection of controls from Microsoft and partners</li><li>Dense UI</li><li>Support for Windows 7</li><li>Platform support for input validation</li></ul> | <ul><li>Rapid application development</li><li>WYSIWYG editor for building UI</li><li>Large collection of controls from Microsoft and partners</li><li>Dense UI</li><li>Support for Windows 7</li><li>Keyboard and mouse input</li></ul>          |
| **Scenarios that have limited support** |  <ul><li>Multiple window support<sup>1</sup></li><li>Platform support for input validation<sup>1</sup></li><li>Windows 7 is not supported</li><li>Some Windows Runtime APIs require specific minimum versions of Windows 10</li><li>Full platform support and shell integration (for example, UWP currently doesn't support System Tray integration or full access to all devices)</li><li>Direct access to all files on disk</li><li>ADO.NET</li><li>Existing code-base class libraries that use non-.NET Standard or non-Windows App Certification Kit compliant APIs</li><li>Local network loopback support (that is, if your app needs to communicate with localhost without creating a loopback exemption on the target device)</li><li>Intensive file I/O</li></ul>     |  <ul><li>High DPI support<sup>2</sup></li><li>Touch input<sup>2</sup></li></ul>  |  <ul><li>High DPI support<sup>2</sup></li><li>Touch input<sup>2</sup></li><li>Customizable UI</li><li>Rich graphics and user experiences (such as touch and animations)</li><li>Rich abstraction of views and data models</li></ul>    |   |

<sup>1</sup> We have publicly announced features that will address this scenario in a future release of Windows 10.

<sup>2</sup> Although the platform lacks first-class API support for this scenario, developers can support this scenario with workarounds.

## Other app platforms

### Progressive Web Apps (PWAs)

PWAs let developers package their website code so it can be installed and run like an application on Windows 10 PCs. For more information, see [Progressive Web Apps](/microsoft-edge/progressive-web-apps/get-started).

### Xamarin

Use Xamarin to build cross-platform applications for Windows 10 that can also run on iOS and Android. For more information, see [Xamarin](https://docs.microsoft.com/xamarin/xamarin-forms/get-started/index).

### Uno Platform

The Uno Platform enables Windows UWP-based code (C# and XAML) to run on iOS, Android, and WebAssembly. It provides full API definitions for UWP in [Windows 10 2004 (19041)](/windows/uwp/whats-new/windows-10-build-19041), and the implementation of parts of the UWP API, such as [Windows.UI.Xaml](/uwp/api/windows.ui.xaml.documents?view=winrt-19041), to enable UWP applications to run on these platforms. For more information, see the [Uno Platform docs](https://platform.uno/docs/articles/intro.html).
