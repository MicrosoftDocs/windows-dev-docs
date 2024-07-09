---
description: This article provides an overview of Visual Studio project and item templates for Windows apps.
title: Visual Studio project and item templates for Windows apps
ms.date: 11/17/2020
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, xaml islands
ms.localizationpriority: high
---

# Visual Studio project and item templates for Windows apps

Visual Studio 2019 (and later) provides many project and item templates that help you build apps for Windows 11 and Windows 10 devices by using C\# or C++. This topic describes the templates and helps you choose one for your scenario.

* Project templates include project files, code files, and other assets that are configured to build an app or a component that can be loaded and used by an app.
* Item templates are project files that contain commonly used code and XAML that can be added to a project to reduce development time. For example, you can use an item template to add a new window, page, or control to your app.

For more information about installing and configuring Visual Studio to get access to these templates, see [Get started with WinUI](../get-started/start-here.md).

## WinUI templates

[WinUI](../winui/index.md) is the modern native user interface (UI) platform for Windows apps across desktop (.NET and native Win32) and UWP app platforms. [WinUI 3](../winui/index.md) is the latest major version of WinUI, and it transforms WinUI into a full UX framework for desktop Windows apps.

WinUI 3 is available as part of [the Windows App SDK](../windows-app-sdk/index.md). It includes a VSIX package for Visual Studio 2019 (and later) that provides project and item templates that help you get started building apps with a WinUI-based interface.

[Template Studio for WinUI (C#)](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForWinUICs) is a Visual Studio 2022 extension that accelerates the creation of new .NET WinUI apps using a wizard-based UI. Select from a variety of project types and features to generate a project template customized for you.

For more information about the available WinUI project and item templates, see [WinUI 3 templates in Visual Studio](../winui/winui3/winui-project-templates-in-visual-studio.md).

## UWP templates

Visual Studio provides a variety of project templates for building UWP apps with C# or C++. To use these project templates, you must include the **Universal Windows Platform development** workload when you install Visual Studio. For the C++ project templates, you must also include the **C++ (v142) Universal Windows Platforms tools** optional component for the **Universal Windows Platform development** workload.

[Template Studio for UWP](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForUWP) is a Visual Studio 2022 extension that accelerates the creation of new .NET UWP apps using a wizard-based UI. Select from a variety of project types and features to generate a project template customized for you.

### Project templates for C# and UWP

To access the UWP C# project templates when you create a new project in Visual Studio, filter the language to **C#**, the platform to **Windows**, and the project type to **UWP**.

![UWP C# project templates](images/uwp-projects-csharp.png)

You can use these project templates to create C# UWP apps.

| Template | Description |
|----------|-------------|
| Blank App (Universal Windows) | Creates a UWP app. The generated project includes a basic page that derives from the [Windows.UI.Xaml.Controls.Page](/uwp/api/windows.ui.xaml.controls.page) class that you can use to start building your UI. |
| Unit Test App (Universal Windows) | Creates a unit test project in C# for a UWP app. For more information, see [Unit test C# code](/visualstudio/test/unit-testing-visual-csharp-code-in-a-store-app). |

You can use these project templates to build pieces of a C# UWP app.

| Template | Description |
|----------|-------------|
| Class Library (Universal Windows) | Creates a managed class library (DLL) in C# that can be used by other UWP apps written in managed code. |
| Windows Runtime Component (Universal Windows) | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) in C# that can be consumed by any UWP app, regardless of the programming language in which the app is written. |
| Optional Code Package (Universal Windows) | Creates an optional package with executable C# code that can be loaded by an app. For more information, see [Optional packages with executable code](/windows/msix/package/optional-packages-with-executable-code).  |

### Project templates for C++ and UWP

There are two different technologies you can use to build C++ UWP apps:

* The recommended technology is [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/). This is a C++ language projection that is implemented entirely in header files, and designed to provide you with first-class access to the modern WinRT API.
* Alternatively, you can use the older [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) set of extensions. C++/CX is still supported, but we recommend that you use C++/WinRT instead.

To access the UWP C++ project templates when you create a new project in Visual Studio, filter the language to **C++**, the platform to **Windows**, and the project type to **UWP**.

> [!NOTE]
> By default, the **Universal Windows Platform development** workload in Visual Studio only provides access to the C++/CX project templates. To access the C++/WinRT project templates, you must install the [C++/WinRT VSIX](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package) package.

![UWP C++ project templates](images/uwp-projects-cpp.png)

You can use these project templates to create C++ UWP apps.

| Template | Description |
|----------|-------------|
| Blank App (C++/WinRT) | Creates a C++/WinRT UWP app with a XAML user interface. The generated project includes a basic page that derives from the [Windows.UI.Xaml.Controls.Page](/uwp/api/windows.ui.xaml.controls.page) class that you can use to start building your UI. |
| Core App (C++/WinRT) | Creates a C++/WinRT UWP app that uses [CoreApplication](/uwp/api/Windows.ApplicationModel.Core.CoreApplication) to integrate with a variety of UI frameworks instead of a XAML user interface. For a walkthrough that demonstrates how to use this project template to create a simple game that uses DirectX, see [Create a simple UWP game with DirectX](/windows/uwp/gaming/tutorial--create-your-first-uwp-directx-game). |
| Blank App (Universal Windows - C++/CX) | Creates a C++/WinRT UWP app with a XAML user interface. The generated project includes a basic page that derives from the [Windows.UI.Xaml.Controls.Page](/uwp/api/windows.ui.xaml.controls.page) class in the WinUI library that you can use to start building your UI. |
| DirectX 11 and XAML App (Universal Windows - C++/CX) | Creates a UWP app that uses DirectX 11 and a **SwapChainPanel** so you can use XAML UI controls. For more information, see [DirectX game project templates](/windows/uwp/gaming/user-interface). |
| DirectX 11 App (Universal Windows - C++/CX) | Creates a UWP app that uses DirectX 11. For more information, see [DirectX game project templates](/windows/uwp/gaming/user-interface). |
| DirectX 12 App (Universal Windows - C++/CX) | Creates a UWP app that uses DirectX 12. For more information, see [DirectX game project templates](/windows/uwp/gaming/user-interface). |
| Unit Test App (Universal Windows - C++/CX) | Creates a unit test project in C++/CX for a UWP app. For more information, see [How to test a C++ UWP DLL](/visualstudio/test/unit-testing-a-visual-cpp-dll-for-store-apps). |

You can use these project templates to build pieces of a C++ UWP app.

| Template | Description |
|----------|-------------|
| Windows Runtime Component (C++/WinRT) | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) in C++/WinRT that can be consumed by any UWP app, regardless of the programming language in which the app is written. |
| Windows Runtime Component (Universal Windows) | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) in C++/CX that can be consumed by any UWP app, regardless of the programming language in which the app is written. |
| DLL (Universal Windows) | A project for creating a dynamic-link library (DLL) in C++/CX that can be used in a UWP app. For more information, see [DLLs (C++/CX)](/cpp/cppcx/dlls-c-cx). |
| Static Library (Universal Windows) | A project for creating a static library (LIB) in C++/CX that can be used in a UWP app. For more information, see [Static libraries (C++/CX)](/cpp/cppcx/static-libraries-c-cx). |

## C++ desktop (Win32) templates

Visual Studio provides a variety of project templates for building desktop Windows apps with native C++ and direct access to the Win32 API. To use these project templates, you must include the **Desktop development with C++** workload when you install Visual Studio. This workload includes project templates for building desktop apps, console apps, and libraries.

The recommended technology is [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/). This is a C++ language projection that is implemented entirely in header files, and designed to provide you with first-class access to the modern WinRT API.

### Project templates for C++ desktop apps

To access the C++ project templates for desktop apps when you create a new project in Visual Studio, filter the language to **C++**, the platform to **Windows**, and the project type to **Desktop**.

![Native C++ app project templates](images/desktop-app-projects-cpp.png)

| Template | Description |
|----------|----------|
| Windows Desktop Application (C++/WinRT) | Creates a [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) client application for Windows desktop. For more information, see [Windows Desktop Application (C++/WinRT)](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#windows-desktop-application-cwinrt). This project template requires the [C++/WinRT VSIX](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package). |
| Windows Desktop Application | Creates a classic Windows desktop app with C++. For more information, see [Walkthrough: Create a traditional Windows Desktop application](/cpp/windows/walkthrough-creating-windows-desktop-applications-cpp). |
| Windows Desktop Wizard | Provides a step-by-step wizard you can use to create one of the following types of projects: a classic Windows desktop app, a console app, a dynamic-link library (DLL), or a static library. For more information, see [Windows Desktop Wizard](/cpp/windows/windows-desktop-wizard) and [Walkthrough: Create a traditional Windows Desktop application](/cpp/windows/walkthrough-creating-windows-desktop-applications-cpp).         |
| Windows Application Packaging Project | Creates a project that you can use to build a desktop app into an [MSIX package](/windows/msix/overview). This provides a modern deployment experience, the ability to integrate with Windows features via package extensions, and much more. For more information, see [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net).  |

### Project templates for C++ console apps

To access the C++ project templates for console apps, filter the language to **C++**, the platform to **Windows**, and the project type to **Console**.

![Native C++ console project templates](images/desktop-console-projects-cpp.png)

| Template | Description |
|----------|----------|
| Windows Console Application (C++/WinRT) | Creates a [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) console app without a user interface. For more information, see [C++/WinRT quick-start](/windows/uwp/cpp-and-winrt-apis/get-started#a-cwinrt-quick-start). This project template requires the [C++/WinRT VSIX](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).  |
| Console App | Creates a console app without a user interface. For more information, see [Walkthrough: Creating a Standard C++ Program](/cpp/windows/walkthrough-creating-a-standard-cpp-program-cpp). |
| Empty Project | An empty project for creating an application, library, or DLL. You must add any code or resources required. |

### Project templates for C++ libraries

To access the C++ project templates for libraries, filter the language to **C++**, the platform to **Windows**, and the project type to **Library**.

![Native C++ library project templates](images/desktop-library-projects-cpp.png)

| Template | Description |
|----------|----------|
| Dynamic-Link Library (DLL) | A project for creating a dynamic-link library (DLL). For more information, see [Walkthrough: Creating and using a dynamic link library](/cpp/build/walkthrough-creating-and-using-a-dynamic-link-library-cpp). |
| Static Library | A project for creating a static library (LIB). For more information, see [Walkthrough: Create and use a static library](/cpp/build/walkthrough-creating-and-using-a-static-library-cpp). |

### Item templates for C++ desktop apps

The C++ project templates for include many item templates that you can use to perform tasks like adding new files and resources to your project. For a comprehensive list, see [Using Visual C++ Add New Item Templates](/cpp/build/reference/using-visual-cpp-add-new-item-templates).

## .NET templates

Visual Studio provides a variety of project templates for building desktop Windows apps that use .NET and C#. To use these project templates, you must include the **.NET desktop development** workload when you install Visual Studio.

To access the .NET C# project templates when you create a new project in Visual Studio, filter the language to **C#**, the platform to **Windows**, and the project type to **Desktop**.

![.NET C# project templates](images/dotnet-projects-csharp.png)

You can use these project templates to create apps using C# and .NET.

| Template | Description |
|----------|----------|
| WPF Application | Creates a [WPF](/dotnet/framework/wpf/) app that targets [.NET 6](/dotnet/core/whats-new/dotnet-6) (or later). For a walkthrough of this project template, see [Create a WPF application](/visualstudio/get-started/csharp/tutorial-wpf). |
| WPF App (.NET Framework) | Creates a [WPF](/dotnet/framework/wpf/) app that targets the [.NET Framework](/dotnet/framework/). For a walkthrough of this project template, see [Tutorial: Create your first WPF application](/dotnet/framework/wpf/getting-started/walkthrough-my-first-wpf-desktop-application). |
| Windows Forms App | Creates a [Windows Forms](/dotnet/framework/winforms/) app that targets [.NET 6](/dotnet/core/whats-new/dotnet-6) (or later).  |
| Windows Forms App (.NET Framework) | Creates a [Windows Forms](/dotnet/framework/winforms/) app that targets the [.NET Framework](/dotnet/framework/). For a walkthrough of this project template, see [Create a Windows Forms app in Visual Studio with C#](/visualstudio/ide/create-csharp-winform-visual-studio). |
| Windows Application Packaging Project | Creates a project that you can use to build a WPF or Windows Forms app into an [MSIX package](/windows/msix/overview). This provides a modern deployment experience, the ability to integrate with Windows features via package extensions, and much more. For more information, see [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net). |
