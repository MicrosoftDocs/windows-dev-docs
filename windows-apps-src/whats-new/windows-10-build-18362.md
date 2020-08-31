---
title: What's New in Windows 10, build 18362
description: Windows 10 build 18362 and new developer tools provide the tools, features, and experiences powered by the Universal Windows Platform.
keywords: Windows 10, 18362, 1903
ms.date: 04/19/2019
ms.topic: article
ms.localizationpriority: medium
ms.custom: 19H1
---

# What's New in Windows 10 for developers, build 18362

Windows 10 build 18362 (also known as SDK version 1903), in combination with Visual Studio 2019, provides the tools, features, and experiences to make remarkable Windows apps. [Install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank) on Windows 10 and you’re ready to either [create a new Universal Windows app](../get-started/create-uwp-apps.md) or explore how you can use your [existing app code on Windows](../porting/index.md).

This is a collection of new and improved features and guidance of interest to Windows developers in this release. For a full list of new namespaces added to the Windows SDK, see the [Windows 10 build 18362 API changes](windows-10-build-18362-api-diff.md). For more information on the highlighted features of Windows 10, see [What's cool in Windows 10](https://developer.microsoft.com/windows/windows-10-for-developers).

## Design & UI

Feature | Description
:------ | :------
AnimatedVisualPlayer | The [AnimatedVisualPlayer](/uwp/api/microsoft.ui.xaml.controls.animatedvisualplayer) API hosts and controls playback of animated visuals in your app. This API is used to control and display content like [Lottie](/windows/communitytoolkit/animations/lottie) visuals, which allow you to render Adobe AfterEffects animations natively in your applications.
CompactDensity | Enabling [Compact mode](../design/style/spacing.md) in your app enables dense, information-rich groups of controls. This can help with browsing large amounts of content, maximizing the visible content on a page, or aid navigation and interaction when the user is using pointer input.
Items Repeater | An [ItemsRepeater](../design/controls-and-patterns/items-repeater.md) control can create a custom experience for displaying collections to your users. ItemsRepeater does not provide a comprehensive end-user experience or a default UI. Instead, it’s a building block that you can use to create your own unique collection-based experiences and custom controls.
Teaching tip | A [teaching tip](../design/controls-and-patterns/dialogs-and-flyouts/teaching-tip.md) is a semi-persistent and content-rich flyout that provides contextual information. You can use this control for informing, reminding, and teaching users about new or important features.
UI commanding | With [commanding in UWP apps](../design/controls-and-patterns/commanding.md), use the [XamlUICommand](/uwp/api/windows.ui.xaml.input.standarduicommand) and [StandardUICommand](/uwp/api/windows.ui.xaml.input.standarduicommand) classes (along with the ICommand interface) to share and manage commands across various control types, regardless of the device and input type being used.
Windows UI Library | The latest official version of the Windows UI Library – [WinUI 2.1](/uwp/toolkits/winui/release-notes/winui-2.1) – provides vibrant new XAML controls for your Windows app. WinUI library APIs run on earlier versions of Windows 10, so you don’t have to include version checks or conditional XAML to supports users who aren’t on the latest OS.
Visual Layer in Desktop apps | You can now [use the UWP Visual layer APIs in desktop applications](/windows/apps/desktop/modernize/visual-layer-in-desktop-apps). These APIs provide high performance retrained-mode API for graphics, effects, and animations, and are the foundation for UI across Windows devices.
Z-depth and shadow | Use [Z-depth and shadow](../design/layout/depth-shadow.md) to create elevation in your UWP app. These new features lets you make your app's UI easier to scan, and better conveys what's important for your users to focus on.

## Develop Windows apps

Feature | Description
:------ | :------
Antimalware Scan Interface (AMSI) | Learn [how the Antimalware Scan Interface (AMSI) helps you defend against malware](/windows/desktop/amsi/how-amsi-helps), then check out the [sample code](/windows/desktop/amsi/dev-audience) to learn how to implement it in your Desktop app.
C++/WinRT 2.0 | Version 2.0 of C++/WinRT has been released. Check out [what's new in C++/WinRT](../cpp-and-winrt-apis/news.md) for a full run-down of all the new changes and additions.
Choose your platform | Interested in creating a new desktop application? Check out our revamped [Choose your platform](/windows/desktop/choose-your-technology) page for detailed descriptions and comparisons of the UWP, WPF, and Windows Forms platforms, and further information on the Win32 API.
Conversational agent | The [Windows.ApplicationModel.ConversationalAgent](/uwp/api/windows.applicationmodel.conversationalagent) namespace lets you add any digital assistance supported by the Windows platform Agent Activation Runtime (AAR) to your Windows app.
Cloud files API | The *cloud files API* allows you to [build a cloud sync engine that supports placeholder files](/windows/desktop/cfapi/build-a-cloud-file-sync-engine).
Direct 3D 12 | [Direct3D 12 render passes](/windows/desktop/direct3d12/direct3d-12-render-passes) can improve the performance of your renderer if it's based on Tile-Based Deferred Rendering (TBDR), among other techniques. The technique helps your renderer improve GPU efficiency by enabling your application to better identify resource rendering ordering requirements and data dependencies. This reduces memory traffic to/from off-chip memory.
Direct Machine Learning (DirectML) | [DirectML](/windows/desktop/direct3d12/dml) is a low-level hardware-accelerated API for machine learning. It has a familiar (native C++, nano-COM) programming interface and workflow in the style of DirectX 12. You can integrate machine learning inferencing workloads into your game, engine, middleware, backend, or other application. DirectML is supported by all DirectX 12-compatible hardware.
DirectX HLSL | [HLSL Shader Model 6.4](/windows/desktop/direct3dhlsl/hlsl-shader-model-6-4-features-for-direct3d-12) provides new machine learning intrinsics for use with DirectML.
Driver development | New audio, camera, display, networking, mobile broadband, print, sensor, storage, and wifi features have been added for Windows driver developers. Check out [What's new in driver development](/windows-hardware/drivers/what-s-new-in-driver-development#whats-new-in-windows-10-version-1903-latest) for further details.
File system operations | This [best practice guide](../files/best-practices-for-writing-to-files.md) can help you best use the Windows.Storage.FileIO and Windows.Storage.PathIO classes to perform file system I/O operations.
Gamepad and remote control interactions | Use [gamepad and remote control interactions](../design/input/gamepad-and-remote-interactions.md) to build usable and accessible interaction experiences. With these interactions, your application can be as intuitive and easy to use from two feet away as it is from ten feet away.
Japanese era change | We've provided [these instructions](../design/globalizing/japanese-era-change.md) to show you how to ensure your Windows application is ready for the Japanese era change set to take place on May 1, 2019. This page is also available in Japanese (at the bottom of the article, click the language control and select Japanese).
Open Source of WPF, Windows Forms, and WinUI | The WPF, Windows Forms, and WinUI UX frameworks are now available for open-source contributions on GitHub. For more information and links, see the [building Windows apps blog](https://blogs.windows.com/buildingapps/2018/12/04/announcing-open-source-of-wpf-windows-forms-and-winui-at-microsoft-connect-2018/#OKZjJs1VVTrMMtkL.97).
Progressive Web Apps for Xbox | With [Progressive Web Apps for Xbox One](/microsoft-edge/progressive-web-apps/xbox-considerations), you can extend a web application and make it available as an Xbox One app via Microsoft Store while still continuing to use your existing frameworks, CDN and server backend. For the most part, you can package your PWA for Xbox One in the same way you would for Windows. This guide will walk you through the process, and highlight the key differences.
Project Rome | The Project Rome SDK is now available for Android and iOS. Learn how to integrate Graph notifications with each platform: [Android](/windows/project-rome/notifications/how-to-guide-for-android) and [iOS](/windows/project-rome/notifications/how-to-guide-for-ios).
Remote cameras | Use the DeviceWatcher class to [connect to remote cameras](../audio-video-camera/connect-to-remote-cameras.md), and read frames from those cameras into your Windows app.
UWP controls in desktop applications (XAML islands) | The APIs in the Windows SDK for hosting UWP controls in WPF, Windows Forms, and C++ Win32 desktop applications are no longer in developer preview. For more information, see [UWP controls in desktop applications](/windows/apps/desktop/modernize/xaml-islands).
Visual Studio 2019 | Visual Studio 2019 has been released, with the latest tools and services for any developer, app, or platform. Check out [What's new in Visual Studio 2019](/visualstudio/ide/whats-new-visual-studio-2019?view=vs-2019) to learn the latest and to get started.
Win32 WebView | Our [frequently asked questions](/windows/communitytoolkit/controls/wpf-winforms/webview#frequently-asked-questions-faqs) provide answers to common questions when using the Microsoft Edge WebView in desktop applications, as well as links to samples and additional resources.
Windows Command Line | [New Console features](https://devblogs.microsoft.com/commandline/new-experimental-console-features/) include the experimental Terminal tab, with settings for scrolling, Cursor shape, and Cursor colors. Learn more on the [Windows Command Line Tools For Developers blog](https://devblogs.microsoft.com/commandline/).
Windows Community Toolkit | Windows Community Toolkit v5.1 provides exciting updates for animation, remote devices, image cropping, and accessibility. </br> • The new [Lottie-Windows library](/windows/communitytoolkit/animations/lottie) provides high quality animation support on Windows 10 (1809) by utilizing the Windows.UI.Composition APIs, and allows for the consumption of [Bodymovin](https://aescripts.com/bodymovin/) JSON files or optimized code-generated classes for playback in your Windows apps. Try the new [Lottie Viewer app](https://www.microsoft.com/p/lottie-viewer/9p7x9k692tmw) from the Microsoft Store to test out animations and generate optimized code for your Windows apps. </br> • The new [Remote Device Picker](/windows/communitytoolkit/controls/remotedevicepicker) allows a user to select a device (proximally or cloud accessible), launch an app on that device, or communicate with app services on the remote device. </br> • The new [ImageCropper control](/windows/communitytoolkit/controls/imagecropper) integrates cropping functionality for selecting profile pictures or for using photo editing tools. </br> • In addition, there have been accessibility improvements on the controls, a [Microsoft.Toolkit.Win32](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32) 6.0 preview package update for WPF and WinForms, and more features that you can read about in the [release notes](https://github.com/windows-toolkit/WindowsCommunityToolkit/releases/tag/v5.1.0).
Windows Machine Learning | We've redesigned the Windows AI docs, splitting them into three areas: Windows Machine Learning (WinML), Windows Vision Skills, and Direct Machine Learning (DirectML). Check out the new [landing page](/windows/ai/) </br> • The [*MLGen* experience](/windows/ai/mlgen) is changing in Visual Studio. In Windows 10, version 1903 and later, *mlgen* is no longer included in the Windows 10 SDK. If you're using VS 2017, you should instead download and install the Visual Studio extension, [Windows Machine Learning Code Generator VS 2017](https://marketplace.visualstudio.com/items?itemName=WinML.mlgen). If you're using Visual Studio 2019, you should install the [Windows Machine Learning Code Generator](https://marketplace.visualstudio.com/items?itemName=WinML.mlgenv2) extension. </br> •  We're also proud to announce new support for weight packing. Developers now can reduce the disk footprint of their ML models by using a technique called weight packing, made available through the [WinMLTools converter](/windows/ai/convert-model-winmltools).
WinRT consolidated reference | We've added full description of the [WinRT type system](/uwp/winrt-cref/winrt-type-system) and [WinMD files](/uwp/winrt-cref/winmd-files), to provide specific in-depth notes about the definitions about the structure of WinRT APIs.
Windows Subsystem for Linux (WSL) | [Recent updates to WSL](https://devblogs.microsoft.com/commandline/whats-new-for-wsl-in-windows-10-version-1903/) include the ability to access Linux files from Windows using File Explorer, and some new commands for wsl.exe and wslconfig.exe.
Windows Vision Skills | [Windows Vision Skills](/windows/ai/windows-vision-skills) is a set of APIs that lets you create “skills,” like facial recognition, and then package them up as a NuGet package that other apps can consume, without even needing to include a machine learning model.

## Publish & Monetize Windows apps

Feature | Description
 :------ | :------
MSIX | [MSIX support on Windows 10 builds 1709 and 1803](/windows/msix/msix-1709-and-1803-support) describes which MSIX features are supported on versions before Windows 10, version 1809.
MSIX packaging and deployment | We introduced several [improvements related to modification packages](/windows/msix/modification-package-insider-preview-build-18312) to make it easier to package customizations in an MSIX package. These improvements include the new **rescap6:ModificationPackage** element in the package manifest, the ability to override a file in the main package with a modification package, and the ability to package a file system based plug-in as an MSIX modification package.
MSIX Packaging Tool | • We added [support for performing conversions on a remote machine](/windows/msix/packaging-tool/remote-conversion-setup). We also introduced the [MSIX Packaging Tool Insider Program](/windows/msix/packaging-tool/insider-program) to offer early access to new tool features. </br> • [MSIX Package support on 1709 and later](/windows/msix/packaging-tool/support-on-1709-and-later) provides guidance about using the MSIX Packaging Tool to build packages specifically for Windows 10, versions 1709 and 1803. </br> • [MSIX packaging environment on Hyper-V Quick Create](/windows/msix/packaging-tool/quick-create-vm) shows how to create a virtual environment for MSIX packaging projects. </br> • [Bundle MSIX packages](/windows/msix/packaging-tool/bundle-msix-packages) provides instructions for creating a package bundle using the MSIX Packaging Tool. </br> • [Modification packages on Windows 10 version 1809](/windows/msix/modification-package-1809-update) contains instructions for creating a modification package for Windows 10 version 1809 and later versions using the MSIX Packaging Tool and MakeApp.exe.
MSIX SDK | [Use the MSIX SDK to build a package for cross-platform use](/windows/msix/msix-sdk/sdk-guidance), and learn how to specify the target platforms to which you want your packages to extract.

## Microsoft Learn

Microsoft Learn provides new hands-on learning and training opportunities to Microsoft developers.

* If you're interested in learning how to develop Windows apps, check out [our new learning path](/learn/paths/develop-windows10-apps/) for a thorough introduction to the platform, the tools, and how to write your first few apps.

* Want to learn how to add UI features to your Windows app? Learn how to [Create a UI](/learn/modules/create-ui-for-windows-10-apps/), [add navigation and media to your UI](/learn/modules/enhance-ui-of-windows-10-app/), or [Implement data binding](/learn/modules/implement-data-binding-in-windows-10-app/).

* If you're interested in Web development, check out [Develop web applications with Visual Studio Code](/learn/modules/develop-web-apps-with-vs-code/) or [Build a simple website](/learn/modules/build-simple-website/).

* Alternately, feel free to browse [all the Windows developer modules on Microsoft Learn](/learn/browse/?products=windows&resource_type=module).

## Videos

### Progressive Web Apps

Progressive Web Apps are web sites that function like native apps across different browsers and a wide variety of Windows 10 devices. [Watch the video](https://youtu.be/ugAewC3308Y) to learn more, and then [check out the docs](https://developer.microsoft.com/windows/pwa) to get started.

### VS Code series

Check out our [new video series on Visual Studio Code](https://www.youtube.com/playlist?list=PLlrxD0HtieHjQX77y-0sWH9IZBTmv1tTx) for information about what VSCode is, how to use it, and how it was created.

### Mixed Reality services

HoloLens 2 was recently announced. Check out this [video series on Mixed Reality](https://www.youtube.com/watch?v=pdB7Ukf3u0I&list=PLlrxD0HtieHjh2Nt2BhcluIZeQg0uBZST) for the latest information, and how you can get involved and start developing.

### One Dev Question

In the One Dev Question video series, longtime Microsoft developers cover a series of questions about Windows development, team culture, and history.

* [Raymond Chen on Windows development and history](https://www.youtube.com/playlist?list=PLWs4_NfqMtoxjy3LrIdf2oamq1coolpZ7)

* [Larry Osterman on Windows development and history](https://www.youtube.com/playlist?list=PLWs4_NfqMtoyPUkYGpJU0RzvY6PBSEA4K)

* [Aaron Gustafson on Progressive Web Apps](https://www.youtube.com/playlist?list=PLWs4_NfqMtoyPHoI-CIB71mEq-om6m35I)

* [Chris Heilmann on the webhint tool](https://www.youtube.com/playlist?list=PLWs4_NfqMtow00LM-vgyECAlMDxx84Q2v)