---
title: What's New in Windows 10, build 19041
description: New developer features in Windows 10 build 19041
keywords: what's new, whats new, Windows, Windows 10, update, updates, features, new, newest, developers, 19041, may
ms.date: 01/10/2022
ms.topic: article
---

# What's New for developers in Windows 10 build 19041

This is a collection of articles providing information and guidance on features added in Windows 10 build 19041 (also known as Version 2004). For a full list of new namespaces added to the Windows SDK, see the [Windows 10 build 19041 API changes](windows-10-build-19041-api-diff.md). For more information on the highlighted features of Windows 10, see [What's cool in Windows 10](https://developer.microsoft.com/windows/windows-10-for-developers).

## Windows 10 apps

Feature | Description
:------ | :------
Bluetooth audio playback | [Enable audio playback from remote Bluetooth-connected devices](../audio-video-camera/enable-remote-audio-playback.md) shows you how to use [AudioPlaybackConnection](/uwp/api/windows.media.audio.audioplaybackconnection) to enable Bluetooth-connected remote devices to play back audio on the local machine, enabling scenarios such as configuring a PC to behave like a Bluetooth speaker and allowing users to hear audio from their phone.
C# app porting | We’ve documented the process of porting a C# application to C++/WinRT. [Porting the Clipboard sample to C++/WinRT from C#](../cpp-and-winrt-apis/clipboard-to-winrt-from-csharp.md) is contextual, and based on a particular real-world porting experience. Its companion topic [Move to C++/WinRT from C#](../cpp-and-winrt-apis/move-to-winrt-from-csharp.md) is a more encyclopedic look at the technical details and steps involved in porting. 
C++/WinRT | Read about the updates to C++/WinRT regarding build-time and run-time performance improvements (achieved in concert with the Visual C++ compiler team), in [Rollup of recent improvements/additions](../cpp-and-winrt-apis/news.md#rollup-of-recent-improvementsadditions-as-of-march-2020). </br> For C++/WinRT, we added more info to these topics: [porting from C++/CX](../cpp-and-winrt-apis/move-to-winrt-from-cx.md#boxing-and-unboxing), [porting from C#](../cpp-and-winrt-apis/move-to-winrt-from-csharp.md#boxing-and-unboxing), [Simple C++/WinRT Windows UI Library example](../cpp-and-winrt-apis/simple-winui-example.md), [Concurrency](../cpp-and-winrt-apis/concurrency.md), [get_unknown()](/uwp/cpp-ref-for-winrt/get-unknown), and [XAML custom (templated) controls with C++/WinRT](../cpp-and-winrt-apis/xaml-cust-ctrl.md).
DirectX | We brought several DirectX-related "What's new" topics up to date for several past releases of Windows, from the Creators Update to Windows 10, version 1903. [What's new in DirectWrite](/windows/win32/directwrite/what-s-new-in-directwrite-for-windows-8-consumer-preview), [DXGI 1.6 improvements](/windows/win32/direct3ddxgi/dxgi-1-6-improvements), and [What's new in Direct3D 12](/windows/win32/direct3d12/new-releases).
DirectXMath | We published 21 new DirectXMath topics, covering two matrix structures and their member functions and free functions. The [XMFLOAT3X4 structure](/windows/win32/api/directxmath/ns-directxmath-xmfloat3x4) is an example.
Direct3D | [Using DirectX with high dynamic range displays and advanced color](/windows/win32/direct3darticles/high-dynamic-range) provides a list of best practices for Windows high-dynamic-rnge apps. </br> A new [ID3D11On12Device2](/windows/win32/api/d3d11on12/nn-d3d11on12-id3d11on12device2) interface, and its methods, enable you to take resources created through the Direct3D 11 APIs and use them in Direct3D 12.
Direct3D 12 | [The Direct3D 12 Core 1.0 Feature Level](/windows/win32/direct3d12/core-feature-levels) has been added, for use by *compute-only* devices. </br> New topics habe been added for the [ID3D12Debug3 interface](/windows/win32/api/d3d12sdklayers/nn-d3d12sdklayers-id3d12debug3).
Direct ML | There 18  operators have been added to DirectML, the low-level hardware-accelerated API on which WinML is built. An example is the [DML_ACTIVATION_SHRINK_OPERATOR_DESC structure](/windows/win32/api/directml/ns-directml-dml_activation_shrink_operator_desc).
Error reporting | The RoFailFastWithErrorContextInternal2 function has been added to Win32, which raises an exception which can contain additional error context.
Machine Learning | Windows Machine Learning [now supports ONNX version 1.4 and opset 9](/windows/ai/windows-ml/release-notes). </br>  The [CloseModelOnSessionCreation](/uwp/api/windows.ai.machinelearning.learningmodelsessionoptions.closemodelonsessioncreation) API allows you to save memory by closing a learning model automatically once it is no longer needed.
Wi-Fi | Several new Native WiFi functions and structures have been added, such as the [WlanDeviceServiceCommand function](/windows/win32/api/wlanapi/nf-wlanapi-wlandeviceservicecommand).
Wi-Fi Hotspot 2 | [Provision a Wi-Fi profile via a website](/windows/win32/nativewifi/prov-wifi-profile-via-website) describes new functionality for Wi-Fi Hotspot 2.
Windows Holographic interop | The [`windows.graphics.holographic.interop.h`](/windows/win32/api/windows.graphics.holographic.interop) header has been added, with 17 Win32 APIs. The APIs are for interoperating between Win32 and Windows Runtime. While the APIs were added in Windows 10 build 18362, the header is new for build 19041.
Windows Sockets | Enhancements have been made to the Windows Sockets 2 SPI content. An example of one of the many topics we improved and augmented is the [LPWSPEVENTSELECT callback function](/windows/win32/api/ws2spi/nc-ws2spi-lpwspeventselect) topic.
XAML Islands - basics | Host UWP XAMl controls in your desktop Windows apps with XAML islands. Learn how to [Use XAML Islands to host a UWP XAML control in a C# WPF app](/windows/apps/desktop/modernize/host-standard-control-with-xaml-islands), and [host a standard UWP control in a C++ Win32 app](/windows/apps/desktop/modernize/host-standard-control-with-xaml-islands-cpp).
XAML Islands - custom controls | The [Microsoft.Toolkit.Win32.UI.XamlApplication](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.XamlApplication) and [Microsoft.Toolkit.Win32.UI.SDK](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.SDK) NuGet packages make it easier to host custom UWP XAML controls in .NET and C++ Win32 apps. </br> For step-by-step walkthroughs, see [Host a custom UWP control in a WPF app](/windows/apps/desktop/modernize/host-custom-control-with-xaml-islands) and [Host a custom UWP control in a C++ Win32 app](/windows/apps/desktop/modernize/host-custom-control-with-xaml-islands-cpp). </br> Finally, for guidance on more complicated C++ Win32 scenarios, see [Advanced scenarios for XAML Islands](/windows/apps/desktop/modernize/advanced-scenarios-xaml-islands-cpp).

## Build with Windows

Feature | Description
:------ | :------
Windows development environment | The [Windows development environment](/windows/dev-environment/) docs provide resources for using Windows to develop across a variety of platforms, to accomplish whatever development goals you might have.
Python on Windows | The [Python on Windows](/windows/python/) section provides information for developers new to the Python language, as well as devs looking to optimize their Python development with other tools available on Windows. Learn how to set up your Python environment for [web development](/windows/python/web-frameworks) and [database interaction](/windows/python/databases).
NodeJS on Windows | The [recommended setup for your Node.js development environment](/windows/nodejs/setup-on-wsl2) provides detailed guidelines for advanced developers deploying to Linux servers. Also available are setup instructions for [popular Node.js web frameworks](/windows/nodejs/web-frameworks), [database interaction](/windows/nodejs/databases), and [Docker containers](/windows/nodejs/containers).
Mac to Windows | Our [guide to changing your dev environment](/windows/dev-environment/mac-to-windows) is geared towards users transitioning their development platform from Mac to Windows, and provides mappings for comparable shortcuts and development utilities.
Windows Terminal | A [modern terminal application](https://www.microsoft.com/p/windows-terminal/9n0dx20hk701?activetab=pivot:overviewtab) for users of command line tools and shells like Command Prompt, PowerShell, and Windows Subsystem for Linux (WSL). Its main features include multiple tabs, panes, Unicode and UTF-8 character support, a GPU accelerated text rendering engine, and the ability to create your own themes and customize text, colors, backgrounds, and shortcut key bindings.
WSL 2 | [A new version of the Windows Subsystem for Linux (WSL)](/windows/wsl/wsl2-about) is now available. WSL 2 features reconfigured architecture to run an actual Linux kernel on Windows, increasing file system performance and adding full system call compatibility. This new architecture changes how Linux binaries interact with Windows and your computer's hardware, but still provides the same user experience as in the previous version of WSL. Each individual Linux distribution can run as a WSL1 or WSL2 distro, can be run side by side, and can be changed at any time. </br> [Install WSL 2](/windows/wsl/wsl2-install) to get started. </br> Explore further information on [changes between WSL 1 and WSL 2](/windows/wsl/compare-versions). </br> Check out the [Frequently Asked Questions about WSL 2](/windows/wsl/wsl2-faq).

## MSIX, packaging, and deployment

Feature | Description
:------ | :------
MSIX | Significant updates to the [MSIX packaging format](/windows/msix/overview) have been made since the last release of the Windows 10 SDK. 
Packaging with services | MSIX and the MSIX Packaging Tool [now support app packages that contain services](/windows/msix/packaging-tool/convert-an-installer-with-services).
Scripts in MSIX packages | You can [use the Package Support Framework (PSF) to run scripts in an MSIX app package](/windows/msix/psf/run-scripts-with-package-support-framework), enabling IT Pros to customize an application dynamically to the user's environment after it is packaged using MSIX.
Enforced package integrity | You can now enforce package integrity on the contents of MSIX packages by using the [uap10:PackageIntegrity element](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-packageintegrity) in your package manifest. You can also enforce package integrity when you create MSIX packages via the MSIX Packaging Tool.
Package with external location | You can grant package identity by building and registering a package with external location (see [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps)). This option is useful if you're unable to adopt MSIX for installing your desktop app, and still use Windows extensibility features that require package identity.
Hosted apps | You can now [create hosted apps](../launch-resume/hosted-apps.md). Hosted apps share the same executable and definition as a parent host app, but they look and behave like a separate app on the system. Hosted apps are useful for scenarios where you want a component (such as an executable file or a script file) to behave like a standalone Windows app, but the component requires a host process in order to execute. A hosted app can have its own start tile, identity, and deep integration with Windows features such as background tasks, notifications, tiles, and share targets.

## Windows UI Library (WinUI)

Feature | Description
:------ | :------
WinUI 2.4 | [WinUI 2.4](/uwp/toolkits/winui/release-notes/winui-2.4) is the latest public release of the Windows UI Library. All versions of WinUI provide a wide assortment of official UI controls for your Windows apps, and are suppplied as a NuGet package independent of the Windows SDK, so they work on earlier versions of Windows 10. [Follow these instructions](/uwp/toolkits/winui) to install WinUI.
RadialGradientBrush | New in WinUI 2.4, a [RadialGradientBrush](/windows/apps/design/style/brushes#radial-gradient-brushes) is drawn within an ellipse defined by Center, RadiusX, and RadiusY properties. Colors for the gradient start at the center of the ellipse and end at the radius.
ProgressRing | New in WinUI 2.4, the [ProgressRing control](/windows/apps/design/controls/progress-controls) is used for modal interactions where the user is blocked until the ProgressRing disappears. Use this control if an operation requires that most interaction with the app be suspended until the operation is complete.
TabView | Updates to the [TabView control](/windows/apps/design/controls/tab-view) provide you with more control over how to render tabs. You can set the width of unselected tabs and show just an icon to save screen space, and can also hide the close button on unselected tabs until the user hovers over the tab.
TextBox controls | When dark theme is enabled, the background color of TextBox family controls now remains dark by default on text insertion. Affected controls are [TextBox](/uwp/api/windows.ui.xaml.controls.textbox), [RichEditBox](/uwp/api/windows.ui.xaml.controls.richtextblock), [PasswordBox](/uwp/api/windows.ui.xaml.controls.passwordbox), [Editable ComboBox](/uwp/api/windows.ui.xaml.controls.combobox), and [AutoSuggestBox](/uwp/api/windows.ui.xaml.controls.autosuggestbox).
NavigationView | The [NavigationView control](/uwp/api/microsoft.ui.xaml.controls.navigationview) now supports hierarchical navigation and includes Left, Top, and LeftCompact display modes. A hierarchical NavigationView is useful for displaying categories of pages, identifying pages with related child-pages, or using within apps that have hub-style pages linking to many other pages.
Windows UI Gallery | Examples of each WinUI feature are available in the XAML Controls Gallery. Download it on the [Microsoft Store](https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt), or [view the source code on Github](https://github.com/Microsoft/Xaml-Controls-Gallery).
Previous versions | Since the previous major release of the Windows 10 SDK, [WinUI 2.3](/uwp/toolkits/winui/release-notes/winui-2.3) and [WinUI 2.2](/uwp/toolkits/winui/release-notes/winui-2.2) were also released, providing further new UI features for Windows devs.

## Samples

The following sample apps have been updated to target Windows 10 build 19041.

* [Remote Sessions (Quiz Game)](https://github.com/microsoft/Windows-appsample-remote-system-sessions)
* [Customer Orders Database](https://github.com/Microsoft/Windows-appsample-customers-orders-database)
* [RSS Reader](https://github.com/Microsoft/Windows-appsample-rssreader)
* [Marble Maze](https://github.com/Microsoft/Windows-appsample-marble-maze)
* [Photo Editor](https://github.com/Microsoft/Windows-appsample-photo-editor)
* [Lunch Scheduler](https://github.com/Microsoft/Windows-appsample-lunch-scheduler)
* [Coloring Book](https://github.com/Microsoft/Windows-appsample-coloringbook)
* [Hue Light Controller](https://github.com/Microsoft/Windows-appsample-huelightcontroller)
* [Photo Lab](https://github.com/Microsoft/Windows-appsample-photo-lab)
* [Family Notes](https://github.com/Microsoft/Windows-appsample-familynotes)

## Videos

### Windows Terminal: the secret to command line happiness!

Learn about how to customize the Windows Terminal for your workflow, and see demos of its features in action. [Check out the video](https://www.youtube.com/watch?v=2dsnwlnNBzs), then [read the docs](https://github.com/microsoft/terminal#terminal--console-overview) for more information.

### WSL2: Code faster on the Windows Subsystem for Linux

Learn all about WSL2, the new version of the Windows Subsystem for Linux, and what changes have been made to improve performance. [Check out the video](https://www.youtube.com/watch?v=MrZolfGm8Zk), then [read the docs](/windows/wsl/wsl2-about) for more information.

### MSIX: Package desktop apps for Windows 10. Replace outdated installers.

Learn about MSIX, the package format for installing Windows apps, including how to package your existing code with Visual Studio and how to deploy and distribute your app. [Check out the video](https://www.youtube.com/watch?v=yhOnClQrvBk), then [read the docs](/windows/msix/) for more information.
