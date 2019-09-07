---
Description: When you want to create a new desktop app, the first decision you make is whether to use the Win32 and COM API or .NET.
ms.assetid: 82705644-F1F0-40F3-99B1-7A97BFB32831
title: Choose your app platform
ms.topic: article
ms.date: 03/18/2019
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Choose your app platform

When you want to create a new application for Windows, the first decision you make is which application platform to use. Windows provides four main application platforms, each with different strengths:

* [Universal Windows Platform (UWP)](#uwp)
* [WPF (.NET)](#wpf)
* [Windows Forms (.NET)](#windows-forms)
* [Win32](#win32)

All of these application platforms let you create desktop apps like Word, Excel, and Photoshop that run in the classic Windows desktop and take full advantage of that environment's specific features. However, some of these platforms share some traits and are better suited for certain types of applications:

* **UWP, WPF, and Windows Forms**. These platforms provide managed runtime environments (the Windows Runtime for UWP, and .NET for Windows Forms and WPF) with many benefits, especially in the areas of developer productivity, sophisticated and customizable UI, and application security. Because these frameworks support visual designers and UI markup for rapidly creating UI, they are particularly well-suited for line-of-business applications.

* **Win32 API**. The Win32 API (also called the Windows API) is the original platform for native C/C++ Windows applications that require direct access to Windows and hardware. It provides a first-class development experience without depending on a managed runtime environment like .NET and WinRT. This makes the Win32 API the platform of choice for applications that need the highest level of performance and direct access to system hardware.

This article describes these platforms in more detail and helps you determine the best one for your application. 

> [!NOTE]
> No matter which app platform you choose, you can use many features of the Universal Windows Platform (UWP) to deliver a modern experience in your app on Windows 10. For example, even if your desktop app is built using WPF, Windows Forms, or the Win32 API, you can still use many features first introduced with UWP, such as MSIX package deployment and UWP XAML controls. For more information, see [Modernize your desktop apps](modernize/index.md).

## UWP

UWP is the leading-edge platform for Windows 10 applications and games. It's a highly customizable platform that uses XAML markup to separate UX (presentation) from code (business logic). UWP is suitable for desktop applications that require a sophisticated UI, styles customization, and graphics-intensive scenarios. UWP also has built-in support for the [Fluent Design System](/windows/uwp/design/fluent-design-system/) for the default UX experience and provides access to the [Windows Runtime (WinRT) APIs](/windows/uwp/get-started/universal-application-platform-guide#how-the-universal-windows-platform-relates-to-windows-runtime-apis). By adopting Fluent, UWP automatically supports common input methods such as ink, touch, gamepad, keyboard, and mouse.

Not only can you use UWP to create desktop applications for Windows PCs, but UWP is also the only supported platform for Xbox, HoloLens, and Surface Hub applications. UWP is our newest, leading-edge application platform.

For more information about UWP, see [Get started with Windows 10 apps](/windows/uwp/get-started/).

## WPF

WPF is the established platform for managed Windows applications with access to the full .NET Framework, and it also uses XAML markup to separate UX from code. This platform is designed for desktop applications that require a sophisticated UI, styles customization, and graphics-intensive scenarios. WPF development skills are similar to UWP development skills, so migration from WPF to UWP apps is easier than migration from Windows Forms.

For more information about WPF, see [Getting started (WPF)](https://docs.microsoft.com/dotnet/framework/wpf/getting-started/).

## Windows Forms

Windows Forms is the original platform for managed Windows applications with a lightweight UI model and access to the full .NET Framework. It excels at enabling developers to quickly get started building applications, even for developers new to the platform. This is a forms-based, rapid application development platform with a large built-in collection of visual and non-visual drag-and-drop controls. Windows Forms does not use XAML, so deciding later to extend your application to UWP entails a complete re-write of your UI.

For more information about Windows Forms, see [Getting started with Windows Forms](https://docs.microsoft.com/dotnet/framework/winforms/getting-started-with-windows-forms).

## Platform comparison: UWP, WPF, and Windows Forms

The following table compares various characteristics of Windows Forms, WPF, and UWP in detail.

| Feature or scenario  |    UWP     |      WPF     |   Windows Forms  |
|--------|--------|--------|--------|
| **Supported versions**      |  Windows 10   |  Windows 7 and later |  Windows 7 and later  |
| **Languages**      |   C\#, C++/WinRT, C++/CX, VB, JavaScript   |  C\#, C++/CLI (Managed Extensions for C++), F\#, VB |  C\#, C++/CLI (Managed Extensions for C++), F\#, VB   |
| **UI runtime** |    Native (C++/WinRT and C++/CX) and managed (.NET Native)  |  Managed (.NET Framework)<br/><br/>Support for .NET Core 3 is coming soon  |   Managed (.NET Framework)<br/><br/>Support for .NET Core 3 is coming soon    |
| **Open source** | [Yes (Windows UI Library only)](https://github.com/Microsoft/microsoft-ui-xaml)  |  [Yes (.NET Core only)](https://github.com/dotnet/wpf) | [Yes (.NET Core only)](https://github.com/dotnet/winforms)  |
| **Supports XAML** |   Yes   |  Yes  |   No   |
| **Strengths**  |  <ul><li>XAML markup for UI</li><li>Rich and customizable UX</li><li>Your existing code bases are .NET Standard compliant</li><li>High DPI support</li><li>Support for multiple input types across Windows devices (including touch, pen, gamepad, mouse, and keyboard)</li><li>Support for Xbox, HoloLens, IoT or Surface Hub</li><li>Support for native C++</li><li>Optimized battery life</li><li>Modern accessibility support (such as screen readers)</li><li>Rich text data capabilities (such as built-in spell check)</li><li>Inking support</li><li>Secure execution via application containers (for example, untrusted content is sandboxed)</li></ul>  |  <ul><li>XAML markup for UI</li><li>Rich and customizable UX</li><li>Large collection of controls from Microsoft and partners</li><li>Dense UI</li><li>Support for Windows 7</li><li>Platform support for input validation</li></ul> | <ul><li>Rapid application development</li><li>WYSIWYG editor for building UI</li><li>Large collection of controls from Microsoft and partners</li><li>Dense UI</li><li>Support for Windows 7</li><li>Keyboard and mouse input</li></ul>          |
| **Scenarios that have limited support** |  <ul><li>Dense UI (creating a dense UI requires custom styles)<sup>1</sup></li><li>Multiple window support<sup>1</sup></li><li>Platform support for input validation<sup>1</sup></li><li>Windows 7 is not supported</li><li>Some UWP APIs require specific minimum versions of Windows 10</li><li>Full platform support and shell integration (for example, UWP currently doesn't support System Tray integration or full access to all devices)</li><li>Direct access to all files on disk</li><li>ADO.NET</li><li>Existing code-base class libraries that use non-.NET Standard or non-Windows App Certification Kit compliant APIs</li><li>Local network loopback support (that is, if your app needs to communicate with localhost without creating a loopback exemption on the target device)</li><li>Intensive file I/O</li></ul>     |  <ul><li>High DPI support<sup>2</sup></li><li>Touch input<sup>2</sup></li></ul>  |  <ul><li>High DPI support<sup>2</sup></li><li>Touch input<sup>2</sup></li><li>Customizable UI</li><li>Rich graphics and user experiences (such as touch and animations)</li><li>Rich abstraction of views and data models</li></ul>    |   |

<sup>1</sup> We have publicly announced features that will address this scenario in a future release of Windows 10.

<sup>2</sup> Although the platform lacks first-class API support for this scenario, developers can support this scenario with workarounds.

## Win32

Using the Win32 API with C++ makes it possible to achieve the highest levels of performance and efficiency by taking more control of the target platform with unmanaged code than is possible on a managed runtime environment like WinRT and .NET. However, exercising such a level of control over your application's execution requires greater care and attention to get right, and trades development productivity for runtime performance.

Here are a few highlights of what the Win32 API and C++ offers to enable you to build high-performance applications.

-   Hardware-level optimizations, including tight control over resource allocation, object lifetimes, data layout, alignment, byte packing, and more.
-   Access to performance-oriented instruction sets like SSE and AVX through intrinsic functions.
-   Efficient, type-safe generic programming by using templates.
-   Efficient and safe containers and algorithms.
-   DirectX, in particular Direct3D and DirectCompute (note that UWP also offers DirectX interop).
-   C++ AMP.

For more information, see [Get started with desktop Windows apps that use the Win32 API](/windows/desktop/desktop-programming) and [Desktop app technologies](/windows/desktop/desktop-app-technologies).

### Win32 and C++ for traditional desktop applications

When writing a desktop application in C++, you can choose Win32 or MFC for the UI, or a host of third-party application frameworks that also support non-Windows platforms.

-   **Win32:** This is the handle-based, C-language API of the Windows platform, including but not limited to UI functionality such as windowing, drawing, and UI controls. Because it is a low-level, C-language API based on handles, it's an infrequent choice for creating modern, UI-intensive apps. However, it supplies the basic APIs necessary to interact with the Windows platform, and is a suitable choice for apps that have simple UI requirements or that just want the Windows UI to stay out of the way as much as possible for example, games.
-   **MFC (Microsoft Foundation Class Library):** This is the venerable application framework and UI library that has served Windows developers since 1992. It's a thin C++ wrapper over the handle-based, C-language Win32 API and provides object-oriented interfaces for many of the predefined windows, common controls, and other Windows objects. Although many modern UI frameworks in the .NET ecosystem surpass MFC in convenience, it is still the native UI framework of choice for many C++ developers creating applications for the Windows desktop.
-   **Third-party application frameworks:** Because C++ can run on a wide variety of platforms and isn't tied to Windows or the .NET runtime, third-parties have developed new application and UI frameworks for C++ to ease development of cross-platform applications with rich user interfaces. Some of these frameworks provide their own look & feel, while others such as wxWidgets or Qt use or emulate the native control set of the platform. Using these libraries, it's possible to share nearly all of an application's source code between versions of the application that run on Windows or other platforms, such as OSX or Linux.

## Other app platforms

### Progressive Web Apps (PWAs)

PWAs let developers package their website code so it can be installed and run like an application on Windows 10 PCs. For more information, see [Progressive Web Apps](https://docs.microsoft.com/microsoft-edge/progressive-web-apps/get-started).

### Xamarin

Use Xamarin to build cross-platform applications for Windows 10 that can also run on iOS and Android. For more information, see [Xamarin](https://docs.microsoft.com/xamarin/xamarin-forms/get-started/index).
