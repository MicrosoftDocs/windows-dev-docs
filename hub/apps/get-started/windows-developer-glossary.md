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

##### App Lifecycle (Management)
App Lifecycle Management describes the management of your application's execution state: not running, running in background, running in foreground, suspended, etc. [Learn more about App Lifecycle](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/app-lifecycle).

##### App SDK
Shorthand for Windows App SDK.

##### Application Model
An application model describes the framework components that support a specific application. For example, the UWP app model includes UWP, WinUI 2, and XAML.

##### Application Packaging
Application packaging describes the manner in which your application is packaged before being distributed and installed by users. Applications can be either packaged or unpackaged.


### C

##### Capability-based access model
Capability-based access describes TODO...


### D

##### DDLM
Dynamic Dependency Lifetime Manager. See https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md 


### F

##### Fluent Design
Microsoft's [Fluent Design System](https://www.microsoft.com/design/fluent/#/) gives you an open-source design system that lets you create reusable cross-platform user interfaces.

### H

##### Hot Reloading
A .NET and C++ feature that allows you to update your application's code and observe your changes while your application runs, eliminating the need to stop, rebuild, and re-rerun your apps while developing. See: [Visual Studio Hot Reloading](https://docs.microsoft.com/en-us/visualstudio/debugger/hot-reload?view=vs-2022).


### I

##### In-app updates
In-app updates allow you to update your applications without requiring your users to take any installation or update action. Packaged apps support in-app updates.


### M

##### Managed Apps
Managed apps are apps that administrators manage through administrative capabilities like Intune. Unmanaged apps are apps that users install and manage without admin control.

##### MAUI
See .NET MAUI below.

##### MFC
The Microsoft Foundation Class (MFC) Library can be used to create complex user interfaces with multiple controls. You can use MFC to create applications with Office-style user interfaces. See: [MFC Desktop Applications](https://docs.microsoft.com/en-us/cpp/mfc/mfc-desktop-applications?view=msvc-170).


##### Mica
Mica is a UI technology that subtly tints your app based on the user's background. [Learn more about Mica](https://docs.microsoft.com/en-us/windows/apps/design/style/mica).


##### MSIX
A modern application package format that allows you to easily deploy your Windows applications (built using Windows App SDK, Win32, WPF, or Windows Forms). When you use MSIX to deploy your apps, your app is a "packaged" app. Packaged apps can check for updates and can control when updates are applied. [Learn more about MSIX](https://docs.microsoft.com/en-us/windows/msix/overview ).




### N

##### Native Apps
Native Windows apps are apps that have been built to run on Windows.


##### Native Compilation
TODO


##### Native Sandboxing
TODO


##### .NET MAUI
.NET Multi-platform App UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [Learn more about .NET MAUI](https://docs.microsoft.com/en-us/dotnet/maui/what-is-maui).

 


### P


##### Package identity
TODO

##### Project Reunion
This was the codename used to refer to Windows App SDK.


##### Packaged App
Apps that are packaged using MSIX. Packaged apps give end-users an easy installation, uninstallation, and update experience. These run with package identity. Packaged apps can be installed through the Windows Store or Windows App Installer.

### Q

##### Qt
TODO

### R

##### React Native
React Native for Windows + macOS brings React Native support for the Windows SDK as well as the macOS 10.14 SDK. [Learn more about React Native](https://microsoft.github.io/react-native-windows/).



### S

##### Sandboxing
TODO

##### Side loading
TODO

##### Sparse Packaging
TODO



### U

##### Unmanaged App
Unmanaged apps are apps that users install and manage without admin control. Managed apps are apps that administrators manage through administrative capabilities like Intune.


##### Unpackaged App
Unpackaged apps don’t use MSIX. They’re typically installed and updated through .exe, Squirrel, or .msi files. These run without package identity. Both packaged and unpackaged apps can be published to the Microsoft Store.


##### UWP



### V

##### VSIX
You can use the VSIX Project template to create an extension or to package an existing extension for deployment. [Learn more about VSIX](https://docs.microsoft.com/en-us/visualstudio/extensibility/getting-started-with-the-vsix-project-template?view=vs-2022).

##### Visual Studio
The Visual Studio IDE is a creative launching pad that you can use to edit, debug, and build code, and then publish an app. [Learn more about Visual Studio](https://docs.microsoft.com/en-us/visualstudio/get-started/visual-studio-ide?view=vs-2022).


##### Visual Studio Code
Visual Studio Code is a lightweight but powerful source code editor which runs on your desktop and is available for Windows, macOS and Linux. [Learn more about VS Code](https://code.visualstudio.com/docs).


### U

##### UWP
A development platform that uses WinRT APIs. Not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.


### W

##### WebView2
WebView2 is the best way to include web content in your native Windows apps. You can use WebView2 with WinUI 3, WPF, and WinForms. [It will soon be supported by WinUI 2](https://github.com/MicrosoftEdge/WebView2Feedback/issues/1604).


##### Win32
The Win32 API (also called the Windows API) is the original platform for native C/C++ Windows applications that require direct access to Windows and hardware. It provides a first-class development experience without depending on a managed runtime environment like .NET and WinRT (for UWP apps for Windows 10). This makes the Win32 API the platform of choice for applications that need the highest level of performance and direct access to system hardware. [Learn more about Win32](https://docs.microsoft.com/en-us/windows/win32/desktop-programming).


##### Windows App
An application that can run on Windows. Windows Apps can be built using a variety of technologies including Windows App SDK, .NET Maui, Win32, and WPF. See the [overview of application development options](index.md) if you need help deciding which technology to use.

##### Windows App Container
TODO

##### Windows App SDK
A set of new developer components and tools that represent the next evolution in the Windows app development platform. The successor to UWP. It lifts libraries from the OS into a standalone SDK that you can use to build backwards-compatible desktop apps. [Learn more on Github](https://github.com/microsoft/WindowsAppSDK/discussions/1615).

##### Windows Forms
Otherwise known as WinForms. A thin layer over Windows APIs with minimal layout and styling options. Not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.

##### Windows Project
A project that you work on through an IDE. You generate application builds from your projects before packaging and deploying them.

##### Windows SDK
The Windows SDK is a collection of headers, libraries, metadata, and tools that allow you to build Win32 and UWP Windows apps.


##### WinRT
C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. [Learn more about WinRT](https://docs.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/).


##### WinUI
The Windows UI Library (WinUI) is a native user experience (UX) framework for both Windows desktop and UWP applications. [Learn more about WinUI](https://docs.microsoft.com/en-us/windows/apps/winui/).


##### WinUI 2
A convenient control library for UWP's UI stack.


##### WinUI 3
The latest and recommended UI framework for Windows desktop apps. This framework is made available through the Windows App SDK, and has been decoupled from the Windows operating system. WinUI 3 uses Fluent Design to provide a native UX framework for windows desktop and UWP apps. It will feel very familiar if you've worked with UWP XAML.


##### WPF
More advanced than WinForms, but not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.




### X

##### XAML Islands
XAML Islands lets you host WinRT XAML controls in non-UWP desktop (Win32, WinForms WPF) apps starting in Windows 10, version 1903. [Learn more about XAML Islands](https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/xaml-islands).

##### Xamarin
Xamarin is an open-source app platform that lets you build Android and iOS apps with .NET and C#. [Learn more about Xamarin](https://dotnet.microsoft.com/en-us/apps/xamarin).