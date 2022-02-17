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

<!-- 
    1. List terms for v1 [done]
    2. Define terms [we are here]
    3. Align with teams
    4. Add links to related resources and definitions when helpful
    5. Publish
    6. Explore terminology standardization with Monica Rush

-->


### A

<!-- docs status: no comments. product status: pending feedback. -->
##### App lifecycle management (ALM)
App lifecycle management (ALM) describes the management of your application's execution state: not running, running in background, running in foreground, suspended, and so on. See [Windows 10 universal Windows platform (UWP) app lifecycle](https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle).

<!-- docs status: no comments. product status: pending feedback. -->
##### Application model
An application model describes the framework components that support a specific application. For example, the Universal Windows Platform (UWP) app model includes UWP, WinUI 2, and XAML.

##### Application packaging
Application packaging describes the manner in which your application is packaged before being distributed and installed by users. Applications can be either packaged or unpackaged.


### C

##### Capability-based access model
Capability-based access describes TODO...


### D

##### Dynamic Dependency Lifetime Manager (DDLM)
Dynamic Dependency Lifetime Manager. See https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md 


### F

##### Fluent Design
Microsoft's [Fluent Design System](https://www.microsoft.com/design/fluent/#/) gives you an open-source design system that lets you create reusable cross-platform user interfaces.

### H

##### Hot Reload
A .NET and C++ feature that allows you to update your application's code and observe your changes while your application runs, eliminating the need to stop, rebuild, and re-rerun your apps while developing. See: [Write and debug running code with Hot Reload](https://docs.microsoft.com/visualstudio/debugger/hot-reload).


### I

##### In-app updates
In-app updates allow you to update your applications without requiring your users to take any installation or update action. Packaged apps support in-app updates.


### M

##### Managed apps
Managed apps are apps that administrators manage through administrative capabilities such as Intune. Unmanaged apps are apps that users install and manage without admin control.

##### MAUI
See [.NET MAUI](#net-maui) below.

##### Microsoft Foundation Classes (MFC)
You can use Microsoft Foundation Classes (MFC) to create complex user interfaces with multiple controls. You can use MFC to create applications with Office-style user interfaces. See: [MFC desktop applications](https://docs.microsoft.com/cpp/mfc/mfc-desktop-applications).


##### Mica
Mica is a user interface (UI) technology that subtly tints your app based on the user's background. [Learn more about Mica](https://docs.microsoft.com/windows/apps/design/style/mica).


##### MSIX
A modern application package format that allows you to easily deploy your Windows applications (built using Windows App SDK, Win32, WPF, or Windows Forms). When you use MSIX to deploy your apps, your app is a "packaged" app. Packaged apps can check for updates and can control when updates are applied. [What is MSIX?](https://docs.microsoft.com/windows/msix/overview).




### N

##### Native apps
Native Windows apps are apps that have been built to run on Windows.


##### Native compilation
TODO


##### Native sandboxing
TODO


##### .NET MAUI
.NET multi-platform app UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [What is .NET MAUI?](https://docs.microsoft.com/dotnet/maui/what-is-maui).

 


### P


##### Package identity
TODO

##### Project Reunion
This was the one-time codename for the Windows App SDK.


##### Packaged app
Apps that are packaged using MSIX. Packaged apps give end-users an easy installation, uninstallation, and update experience. These run with package identity. Packaged apps can be installed through the Microsoft Store or Windows App Installer.

### Q

##### Qt
TODO

### R

##### React Native
React Native for Windows + macOS brings React Native support for the Windows SDK as well as the macOS 10.14 SDK. [React Native for Windows and macOS](https://microsoft.github.io/react-native-windows/).



### S

##### Sandboxing
TODO

##### Side-loading
TODO

##### Sparse package
TODO



### U

##### Unmanaged app
Unmanaged apps are apps that users install and manage without admin control. Managed apps are apps that administrators manage through administrative capabilities like Intune.


##### Unpackaged app
Unpackaged apps don't use MSIX. They're typically installed and updated through `.exe`, Squirrel, or `.msi` files. These run without package identity. Both packaged and unpackaged apps can be published to the Microsoft Store.


##### UWP



### V

##### Visual Studio extension (VSIX)
You can use the VSIX Project template to create an extension, or to package an existing extension for deployment. [Get started with the VSIX Project template](https://docs.microsoft.com/visualstudio/extensibility/getting-started-with-the-vsix-project-template).

##### Visual Studio
The Visual Studio IDE is a creative launching pad that you can use to edit, debug, and build code, and then publish an app. [Welcome to the Visual Studio IDE](https://docs.microsoft.com/visualstudio/get-started/visual-studio-ide).


##### Visual Studio Code
Visual Studio Code is a lightweight but powerful source code editor, which runs on your desktop and is available for Windows, macOS and Linux. [Getting Started with Visual Studio Code](https://code.visualstudio.com/docs).


### U

##### Universal Windows Platform (UWP)
A development platform that uses WinRT APIs. Not being actively developed. WinUI 3 and the Windows App SDK are the latest and recommended alternatives for new app development.


### W

##### WebView2
WebView2 is the best way to include web content in your native Windows apps. You can use WebView2 with WinUI 3, WPF, and WinForms. [[ANNOUNCEMENT] UWP WebView2 on WinUI 2](https://github.com/MicrosoftEdge/WebView2Feedback/issues/1604).


##### Win32
The Win32 API (also called the Windows API) is the original platform for native C/C++ Windows applications that require direct access to Windows and hardware. It provides a first-class development experience without depending on a managed runtime environment like .NET and WinRT (for UWP apps for Windows 10). This makes the Win32 API the platform of choice for applications that need the highest level of performance and direct access to system hardware. [Get started with desktop Windows apps that use the Win32 API](https://docs.microsoft.com/windows/win32/desktop-programming).


##### Windows app
An application that can run on Windows. Windows apps can be built using a variety of technologies including the Windows App SDK, .NET Maui, Win32, and WPF. See the [overview of application development options](index.md) if you need help deciding which technology to use.

##### Windows app container
TODO

##### Windows App SDK
A set of new developer components and tools that represent the next evolution in the Windows app development platform. The successor to UWP. It lifts libraries from the OS into a standalone SDK that you can use to build backwards-compatible desktop apps. [Developing for Windows with the Windows App SDK](https://github.com/microsoft/WindowsAppSDK/discussions/1615).

##### Windows Forms
Known as WinForms for short. A thin layer over Windows APIs with minimal layout and styling options. Not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.

##### Windows project
A project that you work on through an IDE. You generate application builds from your projects before packaging and deploying them.

##### Windows SDK
The Windows SDK is a collection of headers, libraries, metadata, and tools that allow you to build desktop and UWP Windows apps.


##### C++/WinRT
C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to modern Windows APIs. [C++/WinRT](https://docs.microsoft.com/windows/uwp/cpp-and-winrt-apis/).


##### WinUI
The Windows UI Library (WinUI) is a native user interface (UX) framework for both Windows desktop and UWP applications. [Windows UI Library (WinUI)](https://docs.microsoft.com/windows/apps/winui/).


##### WinUI 2
A convenient control library for UWP's UI stack.


##### WinUI 3
The latest and recommended UI framework for Windows desktop apps. This framework is made available through the Windows App SDK, and has been decoupled from the Windows operating system. WinUI 3 uses Fluent Design to provide a native UX framework for windows desktop and UWP apps. It will feel very familiar if you've worked with UWP XAML.


##### Windows Presentation Foundation (WPF)
More advanced than WinForms, but not being actively developed. The Windows App SDK and WinUI 3 are the latest and recommended alternatives for new app development.




### X

##### XAML Islands
XAML Islands lets you host WinRT XAML controls in non-UWP desktop (Win32, WinForms, WPF) apps starting in Windows 10, version 1903. [Host WinRT XAML controls in desktop apps (XAML Islands)](https://docs.microsoft.com/windows/apps/desktop/modernize/xaml-islands).

##### Xamarin
Xamarin is an open-source app platform that lets you build Android and iOS apps with .NET and C#. [Xamarin](https://dotnet.microsoft.com/apps/xamarin).
