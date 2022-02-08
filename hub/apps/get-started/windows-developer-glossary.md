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
    4. Add links to related resources when helpful
    5. Publish
    6. Explore terminology standardization with Monica Rush

-->


### A

##### App Lifecycle (Management)
suspend, resume, etc

##### App SDK
Shorthand for Windows App SDK.


##### Application Model
TODO

##### Application Packaging
TODO


### C

##### Capability-based access model
TODO


### D

##### DDLM
Dynamic Dependency Lifetime Manager. See https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md 


### F

##### Fluent Design
TODO

### H

##### Hot Reloading
TODO


### I

##### In-app updates
In-app updates allow you to update your applications without requiring your users to take any installation or update action. Packaged apps support in-app updates.


### M

##### Managed Apps
TODO

##### MAUI
See .NET MAUI below.

##### MFC
TODO

##### Mica
TODO

##### MSIX
A modern application package format that allows you to easily deploy your Windows applications (built using Windows App SDK, Win32, WPF, or Windows Forms). When you use MSIX to deploy your apps, your app is a "packaged" app. Packaged apps can check for updates and can control when updates are applied. [Learn more about MSIX](https://docs.microsoft.com/en-us/windows/msix/overview ).


##### MVVM



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
XAML Islands lets you use new UI components in existing desktop (Win32, WinForms WPF) apps.
<!-- note: definition duplicated in FAQ -->

##### Xamarin
TODO