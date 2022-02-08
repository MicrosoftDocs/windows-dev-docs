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
TODO


##### Native Compilation
TODO


##### Native Sandboxing
TODO


##### .NET MAUI
.NET Multi-platform App UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [Learn more about .NET MAUI](https://docs.microsoft.com/en-us/dotnet/maui/what-is-maui).

 


### P

##### Packaging
TODO

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
TODO



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
TODO

##### Visual Studio
TODO

##### Visual Studio Code
TODO



### U

##### UWP
A development platform that uses WinRT APIs. Not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.


### W

##### WebView2
WebView2 is the best way to include web content in your native Windows apps. You can use WebView2 with WinUI 3, WPF, and WinForms. [It will soon be supported by WinUI 2](https://github.com/MicrosoftEdge/WebView2Feedback/issues/1604).


##### Win32
TODO


##### Windows App
An application that can run on Windows. Windows Apps can be built using a variety of technologies including Windows App SDK, .NET Maui, Win32, and WPF. See the [overview of application development options](index.md) if you need help deciding which technology to use.

##### Windows App Container
TODO

##### Windows App SDK
A set of new developer components and tools that represent the next evolution in the Windows app development platform. The successor to UWP.

##### Windows Forms
Otherwise known as WinForms. A thin layer over Windows APIs with minimal layout and styling options. Not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.

##### Windows Project
A project that you work on through an IDE. You generate application builds from your projects before packaging and deploying them.

##### WinRT
TODO


##### WinUI
TODO

##### WinUI 2
A convenient control library for UWP's UI stack.


##### WinUI 3
The latest and recommended UI framework for Windows desktop apps. This framework is made available through the Windows App SDK, and has been decoupled from the Windows operating system. WinUI 3 uses Fluent Design to provide a native UX framework for windows desktop and UWP apps. It will feel very familiar if you've worked with UWP XAML.


##### WPF
More advanced than WinForms, but not being actively developed. WinUI 3 and Win App SDK are the latest and recommended alternatives for new app development.


##### WTL
TODO


### X

##### XAML Islands
XAML Islands lets you host WinRT XAML controls in non-UWP desktop (Win32, WinForms WPF) apps starting in Windows 10, version 1903. [Learn more about XAML Islands](https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/xaml-islands).

##### Xamarin
TODO