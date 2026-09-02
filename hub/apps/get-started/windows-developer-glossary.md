---
title: Windows developer glossary
description: Definitions of current Windows app development terms, including WinUI, Windows App SDK, packaging, deployment, interoperability, and Windows AI.
author: GrantMeStrength
ms.author: jken
ms.topic: glossary
ms.date: 09/02/2026
ms.localizationpriority: medium
ms.collection: windows11
audience: new-desktop-app-developers
content-type: glossary
---

# Windows developer glossary

This glossary promotes a common vocabulary among Windows developers.

#### Advanced Windows Settings

The Windows 11 settings surface for developer-focused options such as Developer Mode, sudo, long paths, file extensions, Dev Drive, PowerShell script execution, and the default terminal. On Windows 11, version 25H2 and later, open **Settings > System > Advanced**. See [Advanced Windows Settings](../../advanced-settings/index.md).

#### App lifecycle management (ALM)

The processes and APIs that manage how an app starts, activates, runs, restarts, and terminates. UWP apps can transition between foreground, background, and suspended states. Windows App SDK desktop apps use the Win32 process lifecycle and aren't suspended by the UWP lifecycle manager. See [UWP app lifecycle](/windows/uwp/launch-resume/app-lifecycle) and [Windows App SDK app lifecycle](../windows-app-sdk/applifecycle/applifecycle.md).

#### Application model

Often referred to as "app model." The application model combines deployment, isolation, lifecycle, and presentation components that are unique to a given application development technology. For example, Windows App SDK and WinUI 3 apps run on the Win32 app model, while WinUI for UWP apps run on the UWP app model.

#### Application packaging

Describes the way in which your app is packaged before being deployed and installed by users. An app can be packaged, unpackaged, or packaged with external location (see the [Windows developer FAQ](windows-developer-faq.md)).

#### Bootstrapper

A redistributable component that provides an API to find and load the Windows App SDK framework package for the calling process. In a packaged-with-external-location or unpackaged app, you can load the Windows App SDK framework package explicitly by calling Bootstrapper APIs such as [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize). See [Reference the Windows App SDK framework package at run time](../windows-app-sdk/use-windows-app-sdk-run-time.md).

#### CsWin32

A .NET source generator that produces type-safe P/Invoke signatures for Win32 APIs at build time. You list the APIs you need in a `NativeMethods.txt` file and CsWin32 generates the correct interop code automatically. It's the recommended replacement for hand-written `[DllImport]` declarations. See [Call Win32 APIs from a C# Windows app (CsWin32)](../develop/interop/call-win32-apis.md).

#### C++/WinRT

C++/WinRT is a standard C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-based library, and designed to provide first-class access to modern Windows APIs. See [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/).

#### Dev Drive

A storage volume optimized for developer workloads. Dev Drive uses the Resilient File System (ReFS) and provides faster performance for common development I/O operations like builds, package installs, and source control. See [Set up a Dev Drive on Windows 11](../../dev-drive/index.md).

#### Dev Home

An open-source Windows developer dashboard that was retired in May 2025. Its repository is archived. Use [Advanced Windows Settings](../../advanced-settings/index.md), [WinGet Configuration](../../package-manager/configuration/index.md), and [Dev Drive](../../dev-drive/index.md) for current setup workflows.

#### DirectML

A high-performance, hardware-accelerated DirectX 12 API for machine learning workloads on supported GPUs. For higher-level ONNX inference across available CPUs, GPUs, and NPUs, see [Windows ML](/windows/ai/new-windows-ml/overview). See [DirectML overview](/windows/ai/directml/dml-intro).

#### Dynamic Dependencies

[Dynamic Dependencies](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md) make framework package contents available to packaged and unpackaged apps at run time.

#### Fluent Design

[Fluent Design](https://aka.ms/fluent) is the design system for Windows experiences. WinUI provides controls, materials, typography, and interaction patterns that implement Fluent guidance.

#### Foundry Local

A lightweight runtime for running small language models locally on Windows devices using available hardware (CPU, GPU, or NPU). Enables offline AI scenarios with low latency. See [Get started with Foundry Local](/windows/ai/foundry-local/get-started).

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

#### MSIX

MSIX is a Windows app package format for reliable installation, update, and removal. You can use MSIX with apps built using Windows App SDK, Win32, WPF, or Windows Forms. An app deployed with MSIX is a *packaged app*. See [What is MSIX?](/windows/msix/overview).

#### Native apps

A Windows-specific app that integrates with Windows UI and platform capabilities. A native Windows app can use managed C# and .NET, unmanaged C++, or both. This meaning is distinct from *native code*, which means code that executes outside a managed runtime.

#### P/Invoke (Platform Invocation Services)

The .NET mechanism for calling unmanaged (native) functions from managed C# code. Modern Windows apps should use [CsWin32](../develop/interop/call-win32-apis.md) to generate P/Invoke signatures automatically rather than writing `[DllImport]` declarations by hand.

#### .NET MAUI

.NET Multi-platform App UI. A cross-platform framework for creating native mobile and desktop apps with C# and XAML. An evolution of `Xamarin.Forms` extended from mobile to desktop scenarios, with UI controls rebuilt from the ground up for performance and extensibility. [What is .NET MAUI?](/dotnet/maui/what-is-maui).

#### Neural Processing Unit (NPU)

A dedicated on-device AI accelerator optimized for machine learning workloads. Windows apps can use NPUs through supported [Windows AI APIs](/windows/ai/apis/), [Foundry Local](/windows/ai/foundry-local/get-started), or [Windows ML execution providers](/windows/ai/new-windows-ml/supported-execution-providers).

#### ONNX Runtime (ORT)

A high‑performance, cross‑platform inference engine for models in the ONNX format. See [Run ONNX models](/windows/ai/new-windows-ml/run-onnx-models).

#### Packaged app

An app whose files, manifest, identity, and deployment information are contained in a package such as MSIX. Packaged apps have package identity and can use package-managed installation, update, and removal. See [Packaging overview](../package-and-deploy/packaging/index.md).

#### Packaged app with external location

An app that uses a small identity package while keeping its binaries outside the package and retaining its existing installer and update process. Also called an app with external location or a sparse package. See [Packaging overview](../package-and-deploy/packaging/index.md).

#### Package identity

A system-managed identity defined by a package manifest. Some Windows features, including certain background, push notification, shell integration, and Windows AI scenarios, require package identity. See [Features that require package identity](../desktop/modernize/modernize-packaged-apps.md).

#### Framework-dependent deployment

A Windows App SDK deployment mode in which the app uses Windows App SDK runtime components installed separately on the device. It produces a smaller app deployment but requires the matching Windows App SDK runtime on the device. See [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md).

#### PowerToys

A set of utilities for power users to tune and streamline their Windows experience for greater productivity. See [PowerToys documentation](../../powertoys/index.md).

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

A compact model (for example, Phi Silica) designed to run efficiently on client devices (CPU, GPU, or NPU) with lower latency and cost, suitable for many on-device AI scenarios. See [Get started with Phi Silica](/windows/ai/apis/phi-silica).

#### Text recognition

Text recognition, also known as optical character recognition (OCR), is supported by Windows AI APIs that detect and extract text within images and convert it into machine-readable character streams. See [Get started with AI Text Recognition](/windows/ai/apis/text-recognition).

#### Universal Windows Platform (UWP)

An application development platform and application model that uses Windows Runtime (WinRT) APIs to deliver packaged apps. UWP apps run in a sandboxed environment, and they inherit the security of the UWP platform. [Learn more about UWP](/windows/uwp/).

>[!NOTE]
> Build Windows apps with [Windows App SDK and WinUI](index.md). You can also use [WPF](/dotnet/desktop/wpf/getting-started).

#### Unmanaged app

Apps that aren't managed by the .NET runtime. If you're handling your own memory management, you're building an unmanaged app.

#### Unpackaged app

An app installed and updated outside the Windows package deployment system, such as through MSI, EXE, ClickOnce, scripts, or xcopy deployment. An unpackaged app doesn't have package identity. To retain externally located binaries while adding identity, use the packaged-with-external-location model. See [Packaging overview](../package-and-deploy/packaging/index.md).

#### Self-contained deployment

A Windows App SDK deployment mode that includes the framework components with the app instead of using separately installed runtime packages. It increases deployment size but lets the app service its Windows App SDK dependencies with the app. See [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md).

#### Visual Studio extension (VSIX)

Lets you create, package, and deploy Visual Studio extensions. [Get started with the VSIX Project template](/visualstudio/extensibility/getting-started-with-the-vsix-project-template).

#### WebView2

A control that allows app developers to embed web content (HTML/CSS/JS) in their native apps using the Microsoft Edge (Chromium) rendering engine. You can use WebView2 in WinUI, Win32 C++, WPF, and WinForms. See [Introduction to Microsoft Edge WebView2](/microsoft-edge/webview2/).

#### Microsoft Foundry on Windows

Microsoft Foundry on Windows offers AI-backed features and APIs on Windows 10 and later PCs. Some features like Phi Silica require Copilot+ PC hardware. See [Windows AI APIs overview](/windows/ai/overview).

#### Windows API

Refers to the entire set of Windows APIs including Win32 APIs, COM APIs, UWP WinRT APIs, and the WinRT/Win32 APIs that are part of the Windows App SDK and WinUI.

#### Windows App Development CLI (winapp CLI)

An open-source, public-preview command-line tool for managing Windows SDKs, package identity, manifests, certificates, builds, packaging, and UI automation across Windows app frameworks. See [Windows App Development CLI](../dev-tools/winapp-cli/index.md).

#### Windows App SDK

A set of independently serviced components and tools for Windows desktop app development. It includes WinUI, app lifecycle, windowing, notifications, resources, text, and other APIs. The Windows App SDK complements rather than replaces the Windows SDK. See [Windows App SDK](../windows-app-sdk/index.md).

#### Windows App SDK runtime

The framework, Main, and Singleton packages used by framework-dependent Windows App SDK apps. Packaged apps normally acquire the runtime through package deployment; unpackaged apps use automatic or explicit dynamic dependency initialization. See [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md).

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

A hosting technique for placing XAML content inside an app that uses another desktop UI framework. Legacy [system XAML Islands](/windows/uwp/xaml-islands/xaml-islands) host UWP XAML controls. [WinUI XAML Islands](../desktop/modernize/host-controls-existing-desktop-apps.md) host Windows App SDK controls and use different APIs, namespaces, and host requirements.

#### Windows ML

Windows APIs for running ONNX models locally in Windows apps, with automatic execution provider management across CPUs, GPUs, and NPUs. See [Windows ML](/windows/ai/new-windows-ml/overview).

## Related content

- [Windows developer FAQ](windows-developer-faq.md)
- [Overview of app development options](./index.md)
