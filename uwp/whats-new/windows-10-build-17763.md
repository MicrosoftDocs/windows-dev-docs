---
title: What's New in Windows 10, build 17763
description: Windows 10 build 17763 and new developer tools provide the tools, features, and experiences powered by the Universal Windows Platform.
keywords: Windows 10, 17763, 1809
ms.date: 10/03/2018
ms.topic: article
ms.localizationpriority: medium
ms.custom: RS5
---
# What's New in Windows 10 for developers, build 17763

Windows 10 build 17763 (also known as the October 2018 Update or version 1809), in combination with Visual Studio 2019 and the updated SDK, provide the tools, features, and experiences to make remarkable Universal Windows Platform apps. [Install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank) on Windows 10 and you’re ready to either [create a new Universal Windows app](../get-started/create-uwp-apps.md) or explore how you can use your [existing app code on Windows](../porting/index.md).

This is a collection of new and improved features and guidance of interest to Windows developers in this release. For a full list of new namespaces added to the Windows SDK, see the [Windows 10 build 17763 API changes](windows-10-build-17763-api-diff.md). For more information on the highlighted features of Windows 10, see [What's cool in Windows 10](https://developer.microsoft.com/windows/windows-10-for-developers). In addition, see [Windows Developer Platform features](https://developer.microsoft.com/windows/) for a high-level overview of both past and future additions to the Windows platform.

## Design & UI

Feature | Description
 :------ | :------
App icons and logos | The [app icons and logos page](/windows/apps/design/style/app-icons-and-logos) has been rewritten, and now shows the latest Visual Studio icon tools and provides information on adding images to your app's listing in the Microsoft Store.
Design landing page | The [updated Design landing page](https://developer.microsoft.com/windows/apps/design) has an at-a-glance overview of UWP design areas and information on the latest additions to Fluent Design.
Fluent Design controls | The following new UI controls have been added, to enhance the Fluent Design System and the apparence of your apps: </br> * [CommandBarFlyout](/windows/apps/design/controls/command-bar-flyout) lets you show common user tasks in the context of an item on your UI canvas. </br> * [DropDownButton](/windows/apps/design/controls/buttons#create-a-drop-down-button), [SplitButton](/windows/apps/design/controls/buttons#create-a-split-button), and [ToggleSplitButton](/windows/apps/design/controls/buttons#create-a-toggle-split-button) provide button controls with specialized features to enhance your app's user interface. </br> * [MenuBar](/windows/apps/design/controls/menus) shows a set of multiple top-level menus in horizontal rows. </br> * [NavigationView](/windows/apps/design/controls/navigationview) now supports Top navigation, for cases in which your app has a smaller number of navigation options and requires more space for content. </br> * [TreeView](/windows/apps/design/controls/tree-view) has been enhanced to support data binding, item templates, and drag and drop.
Fluent Design updates | Visual updates and minor changes have been made to the following Fluent Design pages: </br> * [Alignment, padding, margins](/windows/apps/design/layout/alignment-margin-padding) </br> * [Color](/windows/apps/design/style/color) </br> * [Fluent Design for Windows apps](/windows/apps/fluent-design-system) </br> * [Introduction to app design](/windows/apps/design/basics/) </br> * [Navigation basics](/windows/apps/design/basics/navigation-basics) </br> * [Responsive design techniques](/windows/apps/design/layout/responsive-design) </br> * [Screen sizes and breakpoints](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design) </br> * [Style overview](/windows/apps/design/style/index) </br> * [Writing style](/windows/apps/design/style/writing-style) </br> In addition, we've rewritten the following pages with all-new information on their content areas: </br> * [Icons](/windows/apps/design/style/icons) now provides practical recommendations for using icons and making them clickable. </br> * [Typography](/windows/apps/design/style/typography) consolidates information from similar articles, putting everything in a single place with updated guidance and illustrations.
Gaze input and interactions | [Gaze interactions](/windows/apps/design/input/gaze-interactions) allow your app to track a user's gaze, attention, and presence based on the location and movement of their eyes. This feature can be used as an assistive technology, and provides opportunities for gaming and other interactive scenarios where traditional input devices are not available.
Handwriting view | [HandwritingView](/windows/apps/design/controls/text-handwriting-view) is the new ink input surface for TextBox and RichEditBox.​ Users can tap a text control with their pen to expand the control into a writing surface. This guidance explains how to manage and customize the HandwritingView in your application.
Motion in Fluent Design | The use of motion in the Fluent Design System is evolving, built on the fundamentals of timing, easing, directionality, and gravity. Applying these fundamentals will help guide the user through your app, and connects them with their digital experience by reflecting the natural world. Learn more in these articles: </br> * [The Motion overview](/windows/apps/design/motion/index) has been updated to reflect these fundamentals. </br> * [Motion-in-practice](/windows/apps/design/motion/motion-in-practice) provides examples of how to apply these fundamentals within your app. It also contains information on Implicit Animations, which allow for easy interpolation between old and new value when a XAML element's property is changed. </br> * [Directionality and gravity](/windows/apps/design/motion/directionality-and-gravity) solidifies the user's mental model of your app. </br> * [Timing and easing](/windows/apps/design/motion/timing-and-easing) adds realism to the motion in your app. </br> * [XAML property animations](/windows/apps/design/motion/xaml-property-animations) allow you to directly animate properties of a XAML element, without needing to interact with the underlying composition Visual.
Page transitions | [Page transitions](/windows/apps/design/motion/page-transitions) navigate users between pages in an app. They help users understand where they are in the navigation hierarchy, and provide feedback about the relationship between pages.
Text scaling | The new [text scaling guidance](/windows/apps/design/input/text-scaling) explains how to update your applications to accommodate the new text scaling behaviors, which provide the ability for users to change relative font size across both the OS and individual applications. Instead of using a magnifier app (which typically just enlarges everything within an area of the screen and introduces its own usability issues), changing display resolution, or relying on DPI scaling (which resizes everything based on display and typical viewing distance), a user can quickly access a setting to resize just text, ranging from 100% (the default size) up to 225%.
Toolkits | The [Adobe XD and Adobe Illustrator toolkits](/windows/apps/design/downloads/index) have been updated with new features. These design toolkits provide controls and layout templates for designing UWP apps.
UI commanding | Updates to the [UWP commanding infrastructure](/windows/apps/design/basics/commanding-basics) include a better encapsulation of a command object (behavior, label, icon, keyboard accelerators, access key, and description) and a standard set of common commands including cut, copy, paste, exit, etc., which eliminates the need to set these properties manually. </br> The new [XamlUICommand](/uwp/api/windows.ui.xaml.input.xamluicommand) class provides a base class for defining the command behavior of an interactive UI element that performs an action when invoked. This is the parent class for [StandardUICommand](/uwp/api/windows.ui.xaml.input.standarduicommand), which exposes a set of standard platform commands with pre-defined properties. 
Windows UI Library | The [Windows UI Library](/uwp/toolkits/winui/) is a set of NuGet packages that provide controls and other user interface elements for UWP apps. These packages are also compatible with earlier versions of Windows 10, so your app works even if your users don't have the latest OS version. </br> For more information on what's in the Windows UI Library, see [this list of API namespaces included in the NuGet package.](/windows/winui/api/)

## Develop Windows apps

Feature | Description
 :------ | :------
Barcode scanner | The [Barcode scanner](../devices-sensors/pos-barcodescanner.md) documentation has been reorganized, and improved with more detail and code snippets. We have also added a new topic, [Obtain and understand barcode data](../devices-sensors/pos-barcodescanner-scan-data.md), which explains how to obtain and work with data from a barcode scanner.
C++/WinRT | [C++/WinRT](../cpp-and-winrt-apis/index.md) contains many new features, changes, and fixes for this release. There are new functions and base classes to support you in implementing your own [collection properties and collection types](../cpp-and-winrt-apis/collections.md); and you can now use the [{Binding}](../xaml-platform/binding-markup-extension.md) XAML markup extension with your C++/WinRT runtime classes (for code examples, see [Data binding overview](../data-binding/data-binding-quickstart.md)). For a full description of everything new and changed in this release, see [What's new in C++/WinRT](../cpp-and-winrt-apis/news.md).</br></br>Other new C++/WinRT content includes: [XAML custom controls](../cpp-and-winrt-apis/xaml-cust-ctrl.md); [Author COM components](../cpp-and-winrt-apis/author-coclasses.md); [Value categories](../cpp-and-winrt-apis/cpp-value-categories.md); and [Strong and weak references](../cpp-and-winrt-apis/weak-references.md).
C++/WinRT code examples | We've added 250 C++/WinRT code listings to topics in our documentation, accompanying existing C++/CX code examples.
Contributing guidance | We've updated [our contributing guidance](https://github.com/MicrosoftDocs/windows-uwp/blob/docs/CONTRIBUTING.md) for our UWP documentation. This new guidance clarifies the workflow and expectations for external contributions to our docs.
DirectX Graphics Infastructure (DXGI) | New documentation has been added for missing DXGI APIs, and we've provided an article about best practices when presenting on Windows 10. </br> * [For best performance, use DXGI flip model](/windows/desktop/direct3ddxgi/for-best-performance--use-dxgi-flip-model): Provides guidance on how to maximize performance and efficiency in the presentation stack on modern versions of Windows. </br> * [IDXGIOutput6::CheckHardwareCompositionSupport method](/windows/desktop/api/dxgi1_6/nf-dxgi1_6-idxgioutput6-checkhardwarecompositionsupport): Notifies applications that hardware stretching is supported. </br> * [DXGI_HARDWARE_COMPOSITION_SUPPORT_FLAGS enumeration](/windows/desktop/api/dxgi1_6/ne-dxgi1_6-dxgi_hardware_composition_support_flags): Describes which levels of hardware composition are supported.
Get Started | Our [Get Started](../get-started/index.md) content has been revitalized with new topics, providing information and guidance on how developers new to Windows 10 may accomplish the following common tasks: </br> * [Construct a form](../get-started/construct-form-learning-track.md) </br> * [Display customers in a list](../get-started/display-customers-in-list-learning-track.md) </br> * [Save and load settings](../get-started/settings-learning-track.md) </br> * [Work with files](../get-started/fileio-learning-track.md)
Map Style Sheet Editor | Use the new [Map Style Sheet Editor](https://www.microsoft.com/p/map-style-sheet-editor/9nbhtcjt72ft?rtc=1#activetab=pivot:overviewtab) application to interactively customize the appearance of maps that you add to your application.
Microsoft Learn | The new [Microsoft Learn site](https://www.microsoft.com/learning/default.aspx) provides new hands-on learning and training opportunities to Microsoft developers. Currently, Microsoft Learn provides training and certification for Microsoft 365, Microsoft Azure, and Windows Server.
Notepad | [Notepad has been updated](https://blogs.windows.com/windowsexperience/2018/07/11/announcing-windows-10-insider-preview-build-17713/), adding zooming, wrap-around find/replace, and support for Unix/Linux (LF) and Mac (CR) line endings.
Project Rome | [Project Rome](/windows/project-rome/) now provides a consistent programming experience across the supported platforms and SDKs. </br>  New [Microsoft Graph Notifications](/graph/notifications-concept-overview) use Project Rome to offer a people-centric, cross-platform notifications platform  for your app.
Screen snipping | New [URI schemes](../launch-resume/launch-screen-snipping.md) let your app programmatically open up a new snip, or launch the Snip & Sketch app with a specific image for annotation.
UWP Controls in desktop applications | Windows 10 now enables you to use UWP controls in WPF, Windows Forms, and C++ Win32 desktop applications. This means that you can enhance the look, feel, and functionality of your existing desktop applications with the latest Windows 10 UI features that are only available via UWP controls, such as Windows Ink and controls that support the Fluent Design System. This feature is called *XAML islands*. </br> We provide several ways to use XAML islands in your applications, depending on the application platform you are using. WPF and Windows Forms applications can use a set of controls in the [Windows Community Toolkit](/windows/uwpcommunitytoolkit/) that provide a designer-oriented development experience. C++ Win32 applications must use the *UWP XAML hosting API* in the [Windows.UI.Xaml.Hosting](/uwp/api/windows.ui.xaml.hosting) namespace. For more information, see [UWP controls in desktop applications](/windows/apps/desktop/modernize/xaml-islands). </br> **NOTE:** The APIs and controls that enable XAML islands are currently available as a developer preview. Although we encourage you to try them out in your own prototype code now, we do not recommend that you use them in production code at this time.
Windows Machine Learning | [Windows Machine Learning](/windows/ai/) has now officially launched, providing features like faster evaluation and support for cutting-edge machine learning models. To support developers who want to integrate it into their applications, we have created a new documentation site with several new and updated resources: </br> * [Tutorial: Create a Windows Machine Learning Desktop application (C++)](/windows/ai/get-started-desktop): This tutorial shows how to build a simple Windows ML application for desktop. </br> * [Tutorial: Create a Windows Machine Learning UWP application (C#)](/windows/ai/get-started-uwp): Create your first UWP application with Windows ML in this step-by-step tutorial. </br> * [Windows.AI.MachineLearning Namespace](/uwp/api/windows.ai.machinelearning): The API reference has been updated for the latest release of the Windows 10 SDK, and developers can now use this API for both Win32 and UWP applications.
Windows Mixed Reality | Developers can now request hardware-protected backbuffer textures if supported by the display hardware, allowing applications to use hardware-protected content from sources like PlayReady. Hardware protection support and setting is available for the primary layer by using new properties of [Windows.Graphics.Holographic.HolographicCamera](/uwp/api/windows.graphics.holographic.holographiccamera), and for Quad layers via [Windows.Graphics.Holographic.HolographicQuadLayerUpdateParameters](/uwp/api/windows.graphics.holographic.holographicquadlayerupdateparameters).

## IoT Core

Feature | Description
 :------ | :------
AssignedAccessSettings | The [AssignedAccessSettings class](/uwp/api/windows.system.userprofile.assignedaccesssettings) enables calls for different methods and properties to access the user's assigned access settings for a specific device.
Default App Overview | The [Windows 10 IoT Core Default App](/windows/iot-core/develop-your-app/iotcoredefaultapp) has been updated with new features and capabilities, such as weather, inking, and audio.
Dashboard | The [Windows 10 Iot Core Dashboard](/windows/iot-core/tutorials/quickstarter/devicesetup) now allows developers using a Dragonboard 410C or NXP to flash custom FFUs onto their device.
On-Screen Keyboard | The [on-screen keyboard for IoT devices](/windows/iot-core/develop-your-app/onscreenkeyboard) now uses the same touch keyboard components as the desktop edition of Windows. This enables features such as dictation mode, IME support, and a full set of input scopes.
Title bars for sign-in dialogs | Windows 10 IoT Core now provides the option to configure [title bars for system dialog boxes](/windows/iot-core/develop-your-app/signindialogtitlebars).
Wake on Touch | [Wake on touch](/windows/iot-core/learn-about-hardware/wakeontouch) enables your device's screen to turn off while not in use, while quickly turning on when a user touches its screen.
Windows.System.Update | The new [Windows.System.Update namespace](/uwp/api/windows.system.update) enables interactive control of system updates. This namespace is only available for Windows 10 IoT Core.

## Web development

Feature | Description
 :------ | :------
EdgeHTML 18 | The Windows 10 October 2018 update ships with [EdgeHTML 18](/microsoft-edge/dev-guide), the most recent update to the Microsoft Edge browser and the JavaScript engine for UWP apps. EdgeHTML 18 brings modernized and expanded support for the Web Authentication API, new WebView control features, and more! On the tooling side, EdgeHTML 18 brings new WebDriver capabilities and automatic updates, and enhancements to the Edge DevTools and Edge DevTools Protocol. Check out [What’s new in EdgeHTML 18](/microsoft-edge/dev-guide) and [DevTools in the latest Windows 10 update (EdgeHTML 18)](/microsoft-edge/devtools-guide/whats-new) for all the details.
Progressive Web Apps | Windows 10 JavaScript apps (web apps running in a *WWAHost.exe* process) now support an optional [per-application background script](/microsoft-edge/dev-guide#progressive-web-apps) that starts before any views are activated and runs for the duration of the process. With this, you can monitor and modify navigations, track state across navigations, monitor navigation errors, and run code before views are activated. When specified as the [`StartPage`](/uwp/schemas/appxpackage/appxmanifestschema2010-v2/element-application) in your [app manifest](/uwp/schemas/appxpackage/appx-package-manifest), each of the app's views (windows) are exposed to the script as instances of the new [`WebUIView`](/uwp/api/windows.ui.webui.webuiview) class, providing the same events, properties, and methods as a general (Win32) [WebView](/uwp/api/windows.web.ui.iwebviewcontrol).
Web API extensions | A list of [legacy Microsoft API extensions](https://developer.mozilla.org/en-US/docs/Web/API) has been added to the Mozilla Developer Network documentation for cross-browser web development. These API extensions are unique to Internet Explorer or Microsoft Edge, and supplement existing information about compatibility and broswer support in the MDN web docs. Legacy Microsoft [CSS extensions](https://developer.mozilla.org/en-US/docs/Web/CSS) is also available, and you can find rich web API information from MDN surfaced directly in [Visual Studio Code.](https://code.visualstudio.com/updates/v1_25#_new-css-pseudo-selectors-and-pseudo-elements-from-mdn)
WebVR | We have made major updates to the [WebVR Developer's Guide](/microsoft-edge/webvr/), including a complete redesign of the home page and reorganization of the table of contents. We have also written several new topics, including: </br> * [What is WebVR?](/microsoft-edge/webvr/what-is-webvr) Explains what WebVR is, why you should use it, and how to get started developing for it. </br> * [WebVR in Progressive Web Apps](/microsoft-edge/webvr/webvr-in-pwas): Learn how to add WebVR to a Progressive Web App (PWA). </br> * [WebVR in WebView](/microsoft-edge/webvr/webvr-in-webview): Learn how to add WebVR to a WebView control in a Windows 10 application. </br> * [WebVR demos](/microsoft-edge/webvr/demos): Check out some WebVR demos using Microsoft Edge and a Windows Mixed Reality immersive headset.

## Publish & Monetize Windows apps

Feature | Description
 :------ | :------
MSIX | [MSIX](/windows/msix/overview) is the new Windows app package format that provides a modern packaging experience to all Windows apps. The open-source MSIX format preserves the functionality of existing packages, while enabling modern deployment features.
MSIX Packaging Tool | The new [MSIX Packaging Tool](/windows/msix/mpt-overview)) lets you repackage your existing desktop applications in the MSIX format, even if you don’t have access to their source code. It can be run in the command line, or via its interactive UI.
Desktop App Converter support for MSIX | You can use the [Desktop App Converter](/windows/msix/desktop/source-code-overview) to output an MSIX package, by using the `-MakeMSIX` parameter.
MakeAppx.exe tool support for MSIX | You can use the MakeAppx.exe tool to create an MSIX package for UWP apps or traditional desktop applications. This tool is included in the Windows 10 SDK and can be used from a command prompt or a script file. </br> For UWP apps, see [Create an app package with the MakeAppx.exe tool](/windows/msix/package/create-app-package-with-makeappx-tool). </br> For desktop applications, see [Package a desktop application manually](/windows/msix/desktop/desktop-to-uwp-manual-conversion).
Package Support Framework | The [Package Support Framework](/windows/msix/package-support-framework-overview) is an open source kit that helps you apply fixes to your existing desktop application when you don't have access to the source code, so that it can run in an MSIX container.
Store Analytics API | The [Microsoft Store analytics API](../monetize/access-analytics-data-using-windows-store-services.md) now includes the following new methods: </br> * [Get insights data for your UWP app](../monetize/get-insights-data-for-your-app.md) </br> * [Get insights data for your desktop application](../monetize/get-insights-data-for-your-desktop-app.md) </br>* [Get upgrade blocks for your desktop application](../monetize/get-desktop-block-data.md) </br> * [Get upgrade block details for your desktop application](../monetize/get-desktop-block-data-details.md)

## Videos

The following videos have been published since the Fall Creator's Update, highlighting new and improved features in Windows 10 for developers.

### C++/WinRT

C++/WinRT is a new way of authoring and consuming Windows Runtime APIs. It's implemented solely in header files, and designed to provide you with first-class access to modern app features. [Watch the video](https://www.youtube.com/watch?v=TLSul1XxppA&feature=youtu.be) to learn how it works, then [read the developer docs](../cpp-and-winrt-apis/index.md) for more info.

### Get Started for Devs: Create and customize a form on Windows 10

Our [Get Started docs](../get-started/index.md) for Windows developers now provide hands-on experience with basic app development task. This video walks you through one of those topics, and covers the basics of creating a form UI in your app. [Watch the video](https://www.youtube.com/watch?v=AgngKzq4hKI&feature=youtu.be) to see the code in action, then [check out the topic yourself.](../get-started/construct-form-learning-track.md)

### Enhance your Bot with Project Personality chat

Project Personality Chat lets you add a customizable persona to your chat bots. By integrating with the Microsoft Bot Framework SDK, you can add small-talk capabilities for a more conversational way to interact with the customers. [Watch the video](https://www.youtube.com/watch?v=5C_uD8g2QKg&feature=youtu.be) to learn how to implement it, then [try out the interactive demo](https://www.microsoft.com/research/project/personality-chat/) for a hands-on experience.

### Multi-instance UWP apps

Windows now allows you to run multiple instances of your UWP app, with each in its own separate process. [Watch the video](https://www.youtube.com/watch?v=clnnf4cigd0&feature=youtu.be) to learn how to create a new app that supports this feature, then [read the developer docs](../launch-resume/multi-instance-uwp.md) for more guidance on how and why to use this feature.

### Xbox Live Unity plugin

The Xbox Live plugin for Unity contains support for adding Xbox Live signing, stats, friends lists, cloud storage, and leaderboards to your title. [Watch the video](https://youtu.be/fVQZ-YgwNpY) to learn more, then [download the GitHub package](/gaming/xbox-live/get-started/setup-ide/creators/unity-win10/live-cr-unity-win10-nav?WT.mc_id=windowsdocs-twi) to get started.

### One Dev Question

In the One Dev Question video series, longtime Microsoft developers cover a series of questions about Windows development, team culture, and history.

* [Raymond Chen on Windows development and history](https://www.youtube.com/playlist?list=PLWs4_NfqMtoxjy3LrIdf2oamq1coolpZ7)

* [Larry Osterman on Windows development and history](https://www.youtube.com/playlist?list=PLWs4_NfqMtoyPUkYGpJU0RzvY6PBSEA4K)

* [Aaron Gustafson on Progressive Web Apps](https://www.youtube.com/playlist?list=PLWs4_NfqMtoyPHoI-CIB71mEq-om6m35I)

* [Chris Heilmann on the webhint tool](https://www.youtube.com/playlist?list=PLWs4_NfqMtow00LM-vgyECAlMDxx84Q2v)

## Samples

### Customer Orders Database

The [Customers Orders Database sample](https://github.com/Microsoft/Windows-appsample-customers-orders-database) has been updated to use new controls like [DataGrid](/windows/communitytoolkit/controls/datagrid), [NavigationView](/uwp/api/windows.ui.xaml.controls.navigationview), and [Expander](/windows/communitytoolkit/controls/expander).

### Customer database tutorial

The [Customer database tutorial](../enterprise/customer-database-tutorial.md) creates a basic UWP app for managing a list of customers, and introduces concepts and practices useful in enterprise development. It walks you through implementing UI elements and adding operations against a local SQLite database, and provides loose guidance for connecting to a remote REST database if you wish to go further.

### Photo Editor C++/WinRT

The [Photo Editor sample app](https://github.com/Microsoft/Windows-appsample-photo-editor) showcases development with the [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) language projection. The app allows you to retrieve photos from the **Pictures** library, and then edit an elected image with associated photo effects.

### Windows Machine Learning

The [Windows-Machine-Learning](https://github.com/Microsoft/Windows-Machine-Learning) repository has been updated to work with the latest Windows 10 SDK, and contains samples written in C#, C++, and JavaScript.

### XAML Hosting API

The [XAML Hosting API sample](https://github.com/Microsoft/Windows-appsample-Xaml-Hosting) is a Win32 desktop app that highlights assorted scenarios using the UWP XAML hosting API (also called XAML islands). The project incorporates Windows Ink, Media Player, and Navigation View controls in a gallery-style presentation. Outside of general controls usage, the sample also demonstrates handling XAML and native Windows events/messages, and basic XAML data binding.
