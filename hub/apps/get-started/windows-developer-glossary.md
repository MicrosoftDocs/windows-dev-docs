---
description: A glossary of terms related to Windows application development.
title: Windows Developer Glossary
ms.topic: article
ms.date: 01/24/2022
keywords: windows win32, desktop development
ms.author: mikben
author: mikben
ms.localizationpriority: medium
ms.collection: windows11
---

# Windows Developer Glossary

The following glossary of terms is meant to promote a common vocabulary among Windows developers.




### A

<!-- docs status: definition clarified. product status: pending feedback. -->
##### Admin-managed apps
Admin-managed apps are apps that administrators manage through administrative capabilities such as Intune.

<!-- docs status: definition clarified. product status: pending feedback. -->
##### Admin-unmanaged apps
Admin-unmanaged apps are apps that users install and manage without admin control.

<!-- docs status: no comments. product status: pending feedback. -->
##### App lifecycle management (ALM)
App lifecycle management (ALM) describes the management of your application's execution state: not running, running in background, running in foreground, suspended, and so on. See [Windows 10 universal Windows platform (UWP) app lifecycle](https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle).

<!-- docs status: no comments. product status: pending feedback. -->
##### Application model
An application model describes the framework components that support a specific application. For example, the Universal Windows Platform (UWP) app model includes UWP, WinUI 2, and XAML.

<!-- docs status: need to include sparse packaging. product status: pending feedback. -->
<!-- duplication - ensure that when updating this entry, related Glossary and FAQ entries are updated. -->
##### Application packaging
Application packaging describes the manner in which your application is packaged before being distributed and installed by users. Applications can be packaged, unpackaged, or sparsely packaged.


### C

<!-- docs status: no comments. product status: pending feedback. -->
##### Capability-based access model
Capability-based access describes TODO...


### D

<!-- docs status: no comments. product status: pending feedback. -->
##### Dynamic Dependency Lifetime Manager (DDLM)
Dynamic Dependency Lifetime Manager. 


### F

<!-- docs status: no comments. product status: pending feedback. -->
##### Fluent Design


### H

<!-- docs status: no comments. product status: pending feedback. -->
##### Hot Reload
A .NET and C++ feature that allows you to update your application's code and observe your changes while your application runs, eliminating the need to stop, rebuild, and re-rerun your apps while developing. See: [Write and debug running code with Hot Reload](https://docs.microsoft.com/visualstudio/debugger/hot-reload).


### I

<!-- docs status: no comments. product status: pending feedback. -->
##### In-app updates
In-app updates allow you to update your applications without requiring your users to take any installation or update action. Packaged apps support in-app updates.


### M

<!-- docs status: definition clarified. product status: pending feedback. -->
##### Managed apps
"Managed" refers to the "managed runtime" of .NET, which provides managed services such as garbage collection and security assurances. If you're building an app with .NET, you're building a managed app.


<!-- docs status: pending feedback. product status: pending feedback. -->
##### MAUI
See [.NET MAUI](#net-maui) below.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Microsoft Foundation Classes (MFC)
You can use Microsoft Foundation Classes (MFC) to create complex user interfaces with multiple controls. You can use MFC to create applications with Office-style user interfaces. See: [MFC desktop applications](https://docs.microsoft.com/cpp/mfc/mfc-desktop-applications).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Mica
Mica is a user interface (UI) technology that subtly tints your app based on the user's background. [Learn more about Mica](https://docs.microsoft.com/windows/apps/design/style/mica).

<!-- docs status: meaning clarified. product status: pending feedback. -->
##### MSIX - Modern Microsoft Installer Packaging
MSIX is a Windows app package format that combines the best features of MSI, .appx, App-V, and ClickOnce to provide a modern and reliable packaging experience. It's a modern application package format that lets you easily deploy your Windows applications. MSIX can be used to package apps built using Windows App SDK, Win32, WPF, or Windows Forms. When you use MSIX to deploy your apps, your app is a "packaged" app. MSIX-packaged apps can check for updates and can control when updates are applied. [What is MSIX?](https://docs.microsoft.com/windows/msix/overview).




### N

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Native apps
Traditionally, "native" refers to applications built without using the .NET runtime. In this case, "native" is synonymous with "unmanaged", and can be used to describe win32 apps that manage their own memory and security concerns.

Some developers use "native" to indicate that an application has been built to run specifically on Windows, calling Windows APIs directly. We'll rarely use "native apps" in our docs because of this ambiguity.

<!-- docs status: very low-confidence, pending feedback. product status: pending feedback. -->
##### Native compilation
Native compilation refers to applications compiled without using .NET.


<!-- docs status: very low-confidence, pending feedback. product status: pending feedback. -->
##### Native sandboxing
Native sandboxing refers to applications sandboxed without using .NET.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### .NET MAUI
.NET Multi-platform App UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [What is .NET MAUI?](https://docs.microsoft.com/dotnet/maui/what-is-maui).

 


### P

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Package identity
TODO

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Project Reunion
This was the one-time codename for the Windows App SDK.

<!-- docs status: pending feedback. product status: pending feedback. -->
<!-- duplication - ensure that when updating this entry, related Glossary and FAQ entries are updated. -->
##### Packaged app
Apps that are packaged using MSIX. Packaged apps give end-users an easy installation, uninstallation, and update experience. These run with package identity. Packaged apps can be installed through the Microsoft Store or Windows App Installer. Refer to [Sparse-package](#sparse-package) to learn about an alternative packaging format.

### Q

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Qt
TODO

### R

<!-- docs status: pending feedback. product status: pending feedback. -->
##### React Native
React Native for Windows + macOS brings React Native support for the Windows SDK as well as the macOS 10.14 SDK. [React Native for Windows and macOS](https://microsoft.github.io/react-native-windows/).



### S

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Sandboxing
TODO

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Side-loading
TODO

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Sparse package
TODO. Refer to [unpackaged](#unpackaged-app) and [packaged](#packaged-app) to learn about alternative packaging formats.



### U

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Universal Windows Platform (UWP)
An application development platform that uses WinRT APIs to deliver packaged apps. UWP apps are generally locked down, they inherit the security of the UWP platform, and they run in a sandbox. The UWP platform is not being actively developed. WinUI 3 and the Windows App SDK are the latest and recommended alternatives for new app development. [Learn more about UWP](https://docs.microsoft.com/en-us/windows/uwp/).


<!-- docs status: pending feedback. product status: pending feedback. -->
##### Unmanaged app
"Unmanaged apps" are apps that are not managed by the .NET runtime. If you're handling your own memory management, you're building an unmanaged app. "Unmanaged" is synonymous with "native".

<!-- docs status: pending feedback. product status: pending feedback. -->
<!-- duplication - ensure that when updating this entry, related Glossary and FAQ entries are updated. -->
##### Unpackaged app
Unpackaged apps don't use MSIX. They're typically installed and updated through `.exe`, Squirrel, or `.msi` files. These run without package identity. Both packaged and unpackaged apps can be published to the Microsoft Store. Refer to [Sparse-package](#sparse-package) to learn about an alternative packaging format.



### V

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Visual Studio extension (VSIX)
You can use the VSIX Project template to create an extension, or to package an existing extension for deployment. [Get started with the VSIX Project template](https://docs.microsoft.com/visualstudio/extensibility/getting-started-with-the-vsix-project-template).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Visual Studio
The Visual Studio IDE is a creative launching pad that you can use to edit, debug, and build code, and then publish an app. [Welcome to the Visual Studio IDE](https://docs.microsoft.com/visualstudio/get-started/visual-studio-ide).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Visual Studio Code
Visual Studio Code is a lightweight but powerful source code editor, which runs on your desktop and is available for Windows, macOS and Linux. [Getting Started with Visual Studio Code](https://code.visualstudio.com/docs).



### W

<!-- docs status: pending feedback. product status: pending feedback. -->
##### WebView2
WebView2 is the best way to include web content in your native Windows apps. You can use WebView2 with WinUI 3, WPF, and WinForms. [[ANNOUNCEMENT] UWP WebView2 on WinUI 2](https://github.com/MicrosoftEdge/WebView2Feedback/issues/1604).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Win32
The Win32 API is a platform for native C/C++ Windows applications that require direct access to Windows and hardware. It provides a first-class development experience without depending on a managed runtime environment like .NET and WinRT (for UWP apps for Windows 10). This makes the Win32 API the platform of choice for applications that need the highest level of performance and direct access to system hardware. [Get started with desktop Windows apps that use the Win32 API](https://docs.microsoft.com/windows/win32/desktop-programming).


<!-- docs status: pending feedback. product status: pending feedback. -->
##### Windows API
Refers to the entire set of Windows APIs including Win32 APIs, COM APIs, UWP WinRT APIs, and the WinRT APIs that are part of WinAppSDK and WinUI 3.


<!-- docs status: pending feedback. product status: pending feedback. -->
##### Windows app
An application that can run on Windows. Windows apps can be built using a variety of technologies including the Windows App SDK, .NET Maui, Win32, and WPF. See the [overview of application development options](index.md) if you need help deciding which technology to use.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Windows app container
TODO

<!-- docs status: pending feedback. product status: pending feedback. -->
<!-- duplication - ensure that when updating this entry, related Glossary and FAQ entries are updated. -->
##### Windows App SDK
A set of new developer components and tools that represent the next evolution in the Windows app development platform. The successor to UWP. It lifts libraries from the OS into a standalone SDK that you can use to build backwards-compatible desktop apps. [Developing for Windows with the Windows App SDK](https://github.com/microsoft/WindowsAppSDK/discussions/1615).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Windows Forms
Known as WinForms for short. A thin layer over Windows APIs with minimal layout and styling options. Not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Windows project
A project that you work on through an IDE. You generate application builds from your projects before packaging and deploying them.

<!-- docs status: pending feedback. product status: pending feedback. -->
<!-- duplication - ensure that when updating this entry, related Glossary and FAQ entries are updated. -->
##### Windows SDK
The Windows SDK is a collection of headers, libraries, metadata, and tools that allow you to build desktop and UWP Windows apps. Not to be confused with the Windows App SDK, which is a modern abstracton around Windows APIs that succeeds the Windows SDK.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### C++/WinRT
C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to modern Windows APIs. [C++/WinRT](https://docs.microsoft.com/windows/uwp/cpp-and-winrt-apis/).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### WinUI
The Windows UI Library (WinUI) is a native user interface (UX) framework for both Windows desktop and UWP applications. [Windows UI Library (WinUI)](https://docs.microsoft.com/windows/apps/winui/).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### WinUI 2
A convenient control library for UWP's UI stack.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### WinUI 3
The latest and recommended UI framework for Windows desktop apps. This framework is made available through the Windows App SDK, and has been decoupled from the Windows operating system. WinUI 3 uses Fluent Design to provide a native UX framework for windows desktop and UWP apps. It will feel very familiar if you've worked with UWP XAML.

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Windows Presentation Foundation (WPF)
More advanced than WinForms, but not being actively developed. The Windows App SDK and WinUI 3 are the latest and recommended alternatives for new app development.




### X

<!-- docs status: pending feedback. product status: pending feedback. -->
##### XAML Islands
XAML Islands lets you host WinRT XAML controls in non-UWP desktop (Win32, WinForms, WPF) apps starting in Windows 10, version 1903. [Host WinRT XAML controls in desktop apps (XAML Islands)](https://docs.microsoft.com/windows/apps/desktop/modernize/xaml-islands).

<!-- docs status: pending feedback. product status: pending feedback. -->
##### Xamarin
Xamarin is an open-source app platform that lets you build Android and iOS apps with .NET and C#. [Xamarin](https://dotnet.microsoft.com/apps/xamarin).
