---
description:  A glossary of terms related to Windows application development.
title: Windows Developer Glossary
ms.topic: article
ms.date: 01/24/2022
ms.author: mikben
author: mikben
ms.localizationpriority: medium
ms.collection: windows11
---
# Windows Developer Glossary

The following glossary of terms is meant to promote a common vocabulary among Windows developers.

<!--pending product signoff -->
##### Admin-managed apps
Apps that administrators manage through administrative capabilities such as Intune.

<!--pending product signoff -->
##### Admin-unmanaged apps
Apps that users install and manage without admin control.

<!--pending product signoff -->
##### App lifecycle management (ALM)
Describes the management of your application's execution state: not running, running in background, running in foreground, suspended, and so on. See [Windows 10 universal Windows platform (UWP) app lifecycle](https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle).


<!--pending product signoff -->
##### Application model
Describes the framework components that support a specific application. For example, the Universal Windows Platform (UWP) app model includes UWP, WinUI 2, and XAML.

<!--pending product signoff -->
##### Application packaging
Describes the manner in which your application is packaged before being distributed and installed by users. Applications can be packaged, unpackaged, or sparsely packaged.

<!--pending product signoff -->
##### Dynamic Dependencies
[Dynamic Dependencies](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md) makes framework packages accessible to all kinds of apps, packaged and unpackaged.

<!--pending product signoff -->
##### Fluent Design
[Fluent Design](https://www.microsoft.com/design/fluent/#/) gives you an open-source design system that lets you create reusable cross-platform user interfaces.

<!--pending product signoff -->
##### Hot Reload
A .NET and C++ feature that allows you to update your application's code and observe your changes while your application runs, eliminating the need to stop, rebuild, and re-rerun your apps while developing. See [Write and debug running code with Hot Reload](https://docs.microsoft.com/visualstudio/debugger/hot-reload).

<!--pending product signoff -->
##### In-app updates
Allow you to update your applications without requiring your users to take any installation or update action. Packaged apps support in-app updates.

<!--pending product signoff -->
##### Managed apps
"Managed" refers to the "managed runtime" of .NET, which provides managed services such as garbage collection and security assurances. If you're building an app with .NET, you're building a managed app.

<!--pending product signoff -->
##### Microsoft Foundation Classes (MFC)
You can use Microsoft Foundation Classes (MFC) to create complex user interfaces with multiple controls. You can use MFC to create applications with Office-style user interfaces. See: [MFC desktop applications](https://docs.microsoft.com/cpp/mfc/mfc-desktop-applications).

<!--pending product signoff -->
##### MSIX (Microsoft Installer package format)
MSIX is a Windows app package format that combines the best features of MSI, .appx, App-V, and ClickOnce to provide a modern and reliable packaging experience. It's a modern application package format that lets you easily deploy your Windows applications. MSIX can be used to package apps built using Windows App SDK, Win32, WPF, or Windows Forms. When you use MSIX to deploy your apps, your app is a "packaged" app. MSIX-packaged apps can check for updates and can control when updates are applied. [What is MSIX?](https://docs.microsoft.com/windows/msix/overview).

<!--pending product signoff -->
##### Native apps
Traditionally, "native" refers to applications built without using the .NET runtime. In this case, "native" is synonymous with "unmanaged", and can be used to describe win32 apps that manage their own memory and security concerns. Some developers use "native" to indicate that an application has been built to run specifically on Windows, calling Windows APIs directly. We'll rarely use "native apps" in our docs because of this ambiguity.

<!--pending product signoff -->
<!-- todo: try to add a "in other words" plain explanation that speaks to devs who don't know or care about IL/runtime layers/etc. make it easy for new grads to understand what this means and how to use the information at low cognitive cost. -->
##### Native compilation
Native compilation refers to applications compiled into a binary that runs directly on the hardware. As contrasted with (as in the case of .NET) being compiled into an intermediate language (IL) assembly, which then runs on top of a managed runtime layer.

<!--pending product signoff -->
##### .NET MAUI
.NET Multi-platform App UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [What is .NET MAUI?](https://docs.microsoft.com/dotnet/maui/what-is-maui).

<!--pending product signoff -->
##### Project Reunion
The codename for the Windows App SDK. No longer in use.

<!--pending product signoff -->
<!-- cc howard k, peter torr, andrew leader, mike hillberg -->
##### Packaged app
Apps that are packaged using MSIX. Packaged apps give end-users an easy installation, uninstallation, and update experience. These run with package identity. Packaged apps can be installed through the Microsoft Store or Windows App Installer. See also: Unpackaged app, Sparsed app (links pending)

<!--pending product signoff -->
##### React Native
React Native for Windows + macOS brings React Native support for the Windows SDK as well as the macOS 10.14 SDK. [React Native for Windows and macOS](https://microsoft.github.io/react-native-windows/).

<!--pending product signoff -->
##### Sparse package
TODO

<!--pending product signoff -->
##### Universal Windows Platform (UWP)
An application development platform that uses WinRT APIs to deliver packaged apps. UWP apps are generally locked down, they inherit the security of the UWP platform, and they run in a sandbox. The UWP platform is not being actively developed. WinUI 3 and the Windows App SDK are the latest and recommended alternatives for new app development. [Learn more about UWP](https://docs.microsoft.com/en-us/windows/uwp/).

<!--pending product signoff -->
##### Unmanaged app
Apps that aren't managed by the .NET runtime. If you're handling your own memory management, you're building an unmanaged app. "Unmanaged" is synonymous with "native".

<!--pending product signoff -->
##### Unpackaged app
Apps that don't use MSIX. They're typically installed and updated through `.exe`, Squirrel, or `.msi` files. These run without package identity. Both packaged and unpackaged apps can be published to the Microsoft Store.

<!--pending product signoff -->
##### Visual Studio extension (VSIX)
Lets you create, package, and deploy Visual Studio extensions. [Get started with the VSIX Project template](https://docs.microsoft.com/visualstudio/extensibility/getting-started-with-the-vsix-project-template).

<!--pending product signoff -->
##### WebView2
Lets you include web content in your native Windows apps. You can use WebView2 with WinUI 3, WPF, and WinForms. [[ANNOUNCEMENT] UWP WebView2 on WinUI 2](https://github.com/MicrosoftEdge/WebView2Feedback/issues/1604).

<!--pending product signoff -->
##### Win32
The Win32 API is a platform for native C/C++ Windows applications that require direct access to Windows and hardware. It provides a first-class development experience without depending on a managed runtime environment like .NET and WinRT (for UWP apps for Windows 10). This makes the Win32 API the platform of choice for applications that need the highest level of performance and direct access to system hardware. [Get started with desktop Windows apps that use the Win32 API](https://docs.microsoft.com/windows/win32/desktop-programming).

<!--pending product signoff -->
##### Windows API
Refers to the entire set of Windows APIs including Win32 APIs, COM APIs, UWP WinRT APIs, and the WinRT APIs that are part of WinAppSDK and WinUI 3.

<!--pending product signoff -->
##### Windows App SDK
A set of new developer components and tools that represent the next evolution in the Windows app development platform. The successor to UWP. It lifts libraries from the OS into a standalone SDK that you can use to build backwards-compatible desktop apps. See [Overview of app development options](https://docs.microsoft.com/en-us/windows/apps/get-started/?tabs=cpp-win32).

<!--pending product signoff -->
##### Windows Forms
Also known as WinForms. A thin layer over Windows APIs with minimal layout and styling options. Not being actively developed. WinUI 3 and Windows App SDK are the latest and recommended alternatives for new app development.

<!--pending product signoff -->
##### Windows SDK
The Windows SDK is a collection of headers, libraries, metadata, and tools that allow you to build desktop and UWP Windows apps. See [Windows App SDK](#windows-app-sdk), which is a modern abstracton around Windows APIs that succeeds the Windows SDK.

<!--pending product signoff -->
##### WinRT
Also known as C++/WinRT. C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to modern Windows APIs. [C++/WinRT](https://docs.microsoft.com/windows/uwp/cpp-and-winrt-apis/).

<!--pending product signoff -->
##### WinUI
The Windows UI Library (WinUI) is a native user interface (UX) framework for both Windows desktop and UWP applications. [Windows UI Library (WinUI)](https://docs.microsoft.com/windows/apps/winui/).

<!--pending product signoff -->
##### WinUI 2
A convenient control library for UWP's UI stack.

<!--pending product signoff -->
##### WinUI 3
The latest and recommended UI framework for Windows desktop apps. This framework is made available through the Windows App SDK, and has been decoupled from the Windows operating system. WinUI 3 uses Fluent Design to provide a native UX framework for windows desktop and UWP apps. It will feel very familiar if you've worked with UWP XAML.

<!--pending product signoff -->
##### Windows Presentation Foundation (WPF)
More advanced than WinForms, but not being actively developed. The Windows App SDK and WinUI 3 are the latest and recommended alternatives for new app development.

<!--pending product signoff -->
##### XAML Islands
XAML Islands lets you host WinRT XAML controls in non-UWP desktop (Win32, WinForms, WPF) apps starting in Windows 10, version 1903. [Host WinRT XAML controls in desktop apps (XAML Islands)](https://docs.microsoft.com/windows/apps/desktop/modernize/xaml-islands).

<!--pending product signoff -->
##### Xamarin
Xamarin is an open-source app platform that lets you build Android and iOS apps with .NET and C#. [Xamarin](https://dotnet.microsoft.com/apps/xamarin).

## Related articles
  - [Windows Developer FAQ](windows-developer-faq.md)
  - [Build your first Windows App](https://docs.microsoft.com/windows/apps/get-started/?tabs=cpp-win32#app-types)