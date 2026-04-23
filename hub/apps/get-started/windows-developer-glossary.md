---
title: Windows developer glossary
description:  A glossary of terms related to Windows application development.
ms.topic: glossary
ms.date: 12/05/2025
ms.localizationpriority: medium
ms.collection: windows11
audience: new-desktop-app-developers
content-type: glossary
---

# Windows developer glossary

This glossary promotes a common vocabulary among Windows developers.

#### App lifecycle management (ALM)

Manage an application's execution state: not running, running in the background, running in the foreground, or suspended. See [UWP app lifecycle](/windows/uwp/launch-resume/app-lifecycle).

#### Application model

Often referred to as "app model." The application model combines deployment, isolation, lifecycle, and presentation components that are unique to a given application development technology. For example, Windows App SDK and WinUI apps run on the Win32 app model, while WinUI for UWP apps run on the UWP app model.

#### Application packaging

Describes the way in which your app is packaged before being deployed and installed by users. An app can be packaged, unpackaged, or packaged with external location (see the [Windows developer FAQ](/windows/apps/get-started/windows-developer-faq#what-s-the-difference-between-apps-that-are-packaged--unpackaged--and-packaged-with-external-location)).


#### Bootstrapper

A redistributable component that provides an API to find and load the Windows App SDK framework package for the calling process. In a packaged-with-external-location or unpackaged app, you can load the Windows App SDK framework package explicitly by calling Bootstrapper APIs such as [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize). See [Reference the Windows App SDK framework package at run time](../windows-app-sdk/use-windows-app-sdk-run-time.md).

#### C++/WinRT

C++/WinRT is a standard C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-based library, and designed to provide first-class access to modern Windows APIs. See [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/).

#### DirectML

A high‑performance, hardware‑accelerated API for machine learning on Windows that runs on a broad range of GPUs (and increasingly NPUs) using the DirectX 12 stack. See [DirectML overview](/windows/ai/directml/dml-intro).

#### Dynamic Dependencies

[Dynamic Dependencies](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md) makes framework packages accessible to all kinds of apps: packaged and unpackaged.

#### Fluent Design

[Fluent Design](https://aka.ms/fluent) is a design system that lets you create reusable cross-platform user experiences. Fluent 2 is the latest design system for Windows and is used by WinUI.

#### GitHub Copilot

An AI pair programmer that helps you write code faster and with less work in Visual Studio or Visual Studio Code (VS Code). See [AI-assisted development in Visual Studio](/visualstudio/ide/ai-assisted-development-visual-studio) for more information.

#### Hot Reload

An app development feature that allows you to update your application's code and observe your changes while your application runs, eliminating the need to stop, rebuild, and re-run your apps while developing. See [Write and debug running code with Hot Reload](/visualstudio/debugger/hot-reload).

#### Hybrid app

An app that uses multiple technologies. For example, a .NET MAUI app that uses Blazor to render web content in a WebView2 control. See [ASP.NET Core Blazor Hybrid](/aspnet/core/blazor/hybrid/) for more information.

#### Hybrid CRT linkage

A C/C++ runtime library linkage technique that simplifies deployment. Also referred to simply as *Hybrid CRT*. See [Hybrid C/C++ runtime library linkage (hybrid CRT linkage)](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md).

#### Large language model (LLM)

A transformer-based model trained on large corpora to understand and generate natural language (and sometimes images/audio). Supports tasks like chat, summarization, and code generation. See [Azure OpenAI models](/azure/ai-services/openai/concepts/models).

#### Managed apps

"Managed" refers to the "managed runtime" of .NET, which provides managed services such as garbage collection and security assurances. If you're building an app with .NET, you're building a managed app.

#### MCP Servers

MCP is an open protocol designed to standardize integrations between AI apps and external tools and data sources. [Model Context Protocol (MCP) Servers](/windows/ai/mcp/overview).

#### Microsoft Foundation Classes (MFC)

You can use Microsoft Foundation Classes (MFC) to create complex user interfaces with multiple controls. You can use MFC to create applications with Office-style user interfaces. See: [MFC desktop applications](/cpp/mfc/mfc-desktop-applications).

#### Microsoft Foundry

A managed platform for building, evaluating, and deploying generative AI applications with foundation models. Formerly Azure AI Foundry. See [Microsoft Foundry documentation](/azure/ai-foundry/).

#### MSIX (Microsoft Installer package format)

MSIX is a Windows app package format that combines the best features of MSI, .appx, App-V, and ClickOnce to provide a modern and reliable packaging experience. It's a modern application package format that lets you easily deploy your Windows applications. MSIX can be used to package apps built using Windows App SDK, Win32, WPF, or Windows Forms. When you use MSIX to deploy your apps, your app is a *packaged* app. A packaged app can check for updates, and can control when updates are applied. [What is MSIX?](/windows/msix/overview).

#### Native apps

Traditionally, "native" refers to applications built without using the .NET runtime. In this case, "native" is synonymous with "unmanaged", and can be used to describe apps that manage their own memory and security concerns. Alternatively, some developers use "native" to indicate that an application has been built to run specifically on Windows, calling Windows APIs directly.

#### .NET MAUI

.NET Multi-platform App UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [What is .NET MAUI?](/dotnet/maui/what-is-maui).

#### Neural Processing Unit (NPU)

A dedicated on‑device AI accelerator optimized for transformer operations and other ML workloads. Windows apps can target NPUs via APIs included as part of [Foundry Local](/windows/ai/foundry-local/get-started).

#### ONNX Runtime (ORT)

A high‑performance, cross‑platform inference engine for models in the ONNX format. See [Run ONNX models](/windows/ai/new-windows-ml/run-onnx-models).

#### Packaged app

For definitions of apps that are packaged, unpackaged, and packaged with external location, see [Deployment overview](../package-and-deploy/index.md). That topic also explains the advantages and disadvantages of each option.

#### Packaged app with external location

For definitions of apps that are packaged, unpackaged, and packaged with external location, see [Deployment overview](../package-and-deploy/index.md). That topic also explains the advantages and disadvantages of each option.

#### PowerToys

A set of utilities for power users to tune and streamline their Windows experience for greater productivity. See [PowerToys documentation](/windows/powertoys/).

#### Progressive web app (PWA)

An application that you build by using web technologies, and that can be installed and can run on all devices, from one codebase. See [Overview of Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps-chromium/) for more information about building PWAs.

#### Project Reunion

The codename for the Windows App SDK. No longer in use.

#### React Native

[React Native](https://reactnative.dev/) is a development platform from Meta which allows developers to build fully native cross-platform apps using JavaScript, TypeScript, and React.

#### React Native for Desktop

[React Native for Desktop](https://aka.ms/reactnative) brings React Native support to the Windows SDKs, enabling developers to use JavaScript to build native Windows apps for devices supported by Windows 10 and Windows 11. This includes PCs, tablets, 2-in-1s, and Xbox. The term React Native for Desktop encompasses both React Native for Windows and React Native for macOS.

#### Responsible AI (RAI)

A set of recommended responsible development practices to use as you create applications and features. See [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai/) to learn more about the RAI principles and how they apply to Windows development.

#### Small language model (SLM)

A compact model (for example, Microsoft Phi 3) designed to run efficiently on client devices (CPU, GPU, or NPU) with lower latency and cost, suitable for many on device AI scenarios. See [Get started with Phi Silica](/windows/ai/apis/phi-silica).

#### Text recognition

Text recognition, also known as optical character recognition (OCR), is supported by Windows AI APIs that detect and extract text within images and convert it into machine-readable character streams. See [Get started with AI Text Recognition](/windows/ai/apis/text-recognition).

#### Universal Windows Platform (UWP)

An application development platform and application model that uses Windows Runtime (WinRT) APIs to deliver packaged apps. UWP apps run in a sandboxed environment, and they inherit the security of the UWP platform. [Learn more about UWP](/windows/uwp/).

>[!NOTE]
> Build Windows apps with [Windows App SDK and WinUI](/windows/apps/get-started/). You can also use [WPF](/dotnet/desktop/wpf/getting-started).

#### Unmanaged app

Apps that aren't managed by the .NET runtime. If you're handling your own memory management, you're building an unmanaged app.

#### Unpackaged app

For definitions of apps that are packaged, unpackaged, and packaged with external location, see [Deployment overview](../package-and-deploy/index.md). That topic also explains the advantages and disadvantages of each option.

#### Visual Studio extension (VSIX)

Lets you create, package, and deploy Visual Studio extensions. [Get started with the VSIX Project template](/visualstudio/extensibility/getting-started-with-the-vsix-project-template).

#### WebView2

A control that allows app developers to embed web content (HTML/CSS/JS) in their native apps using the Microsoft Edge (Chromium) rendering engine. You can use WebView2 in WinUI, Win32 C++, WPF, and WinForms, and it offers a developer preview for WinUI for UWP support. See [Introduction to Microsoft Edge WebView2](/microsoft-edge/webview2/).

#### Microsoft Foundry on Windows

Microsoft Foundry on Windows offers AI-backed features and APIs on Windows 11 PCs. These features are in active development. See [Windows AI APIs overview](/windows/ai/overview).

#### Windows API

Refers to the entire set of Windows APIs including Win32 APIs, COM APIs, UWP WinRT APIs, and the WinRT/Win32 APIs that are part of the Windows App SDK and WinUI.

#### Windows App SDK

A set of developer components and tools that represent the next evolution of the Windows app development platform. The successor to WinUI for UWP for desktop application development. It lifts libraries from the OS into a standalone SDK that you can use to build backward-compatible desktop apps and often ships new features and capabilities. See [Overview of app development options](./index.md).

#### Windows Forms

Also known as WinForms. A UI framework for building Windows desktop applications. It is a .NET wrapper over Windows user interface libraries, such as User32 and GDI+. It's a battle-tested way to create desktop applications using a visual designer within Visual Studio. See [Desktop Guide (Windows Forms .NET)](/dotnet/desktop/winforms/overview/).

#### Windows Presentation Foundation (WPF)

A UI framework for building Windows desktop applications. WPF applications are based on a vector graphics architecture. This enables applications to look great on high DPI monitors, as they can be infinitely scaled. See [What is Windows Presentation Foundation (WPF)?](/visualstudio/designers/getting-started-with-wpf).

#### Windows SDK

The Windows SDK is a collection of headers, libraries, metadata, and tools that allow you to build desktop and UWP Windows apps. The Windows SDK is not the same as the [Windows App SDK](#windows-app-sdk).

#### WinUI (previously referred to as WinUI 3)

The latest and recommended UI framework for Windows desktop apps. This framework is made available through the Windows App SDK, and has been decoupled from the Windows operating system. WinUI uses [Fluent Design](https://aka.ms/fluent) to provide a native UX framework for Windows desktop apps. It will feel very familiar if you've worked with WinUI for UWP. Note that WinUI apps are commonly referred to as "WinUI apps". See [WinUI](../winui/winui3/index.md).

#### WinUI for UWP (previously referred to as WinUI 2)

WinUI for UWP is tightly integrated with Windows SDKs and provides native Windows UI controls and other user interface elements for UWP applications and desktop applications using XAML Islands. See [WinUI for UWP](/windows/uwp/get-started/winui2/).

#### XAML Islands

XAML Islands lets you host WinRT XAML controls in non-UWP desktop (Win32, WinForms, WPF) apps starting in Windows 10, version 1903. [Host WinRT XAML controls in desktop apps (XAML Islands)](../desktop/modernize/xaml-islands/xaml-islands.md).

#### Windows ML

Windows APIs for running ONNX models locally in Windows apps, with hardware acceleration via DirectML where it's available. See [Windows ML](/windows/ai/new-windows-ml/overview).

## Related content

- [Windows developer FAQ](windows-developer-faq.md)
- [Overview of app development options](./index.md)
