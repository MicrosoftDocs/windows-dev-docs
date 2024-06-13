---
title: What's New in Windows 10, build 17134
description: Windows 10 build 17134 and new developer tools provide the tools, features, and experiences powered by the Universal Windows Platform.
keywords: Windows 10, 17134, 1803
ms.date: 04/10/2018
ms.topic: article


ms.localizationpriority: medium
---
# What's New in Windows 10 for developers, build 17134

Windows 10 build 17134 (also known as the April Update or version 1803), in combination with Visual Studio 2019 and the updated SDK, provide the tools, features, and experiences to make remarkable Universal Windows Platform apps. [Install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank) on Windows 10 and you’re ready to either [create a new Universal Windows app](../get-started/create-uwp-apps.md) or explore how you can use your [existing app code on Windows](../porting/index.md).

This is a collection of new and improved features and guidance of interest to Windows developers in this release. For a full list of new namespaces added to the Windows SDK, see the [Windows 10 build 17134 API changes](windows-10-build-17134-api-diff.md). For more information on the highlighted features of Windows 10, see [What's cool in Windows 10](https://developer.microsoft.com/windows/windows-10-for-developers). In addition, see [Windows Developer Platform features](https://developer.microsoft.com/windows/) for a high-level overview of both past and future additions to the Windows platform.

## Design & UI

Feature | Description
 :------ | :------
Adaptive and interactive toast notifications | Enhance your app with adaptive and interactive notifications. Get started with our [updated guidance on toast notifications](/windows/apps/design/shell/tiles-and-notifications/adaptive-interactive-toasts), and explore the new information on image size restrictions, progress bars, and adding input options.<br><br>[ExpirationTime](/uwp/api/windows.ui.notifications.scheduledtoastnotification.expirationtime#Windows_UI_Notifications_ScheduledToastNotification_ExpirationTime) is now supported on scheduled toast notifications.
Content links | The new [Content links](/windows/apps/design/controls/content-links) control provides a way to embed rich data in your text controls, which lets a user find and use more information about a person or place without leaving the context of your app.
Design samples | The BuildCast sample has been added to the [Design toolkits and samples](/windows/apps/design/downloads/index) page. BuildCast is an end-to-end sample built to showcase the Fluent Design System and other capabilities of the Universal Windows Platform.
Embedded handwriting | The pen input feature has been added to [text controls](/windows/apps/design/controls/text-controls), enabling users to write directly into text boxes with Windows Ink. As the user writes, the text is converted to a script that maintains the feel of natural writing.
Fluent Design updates | We've updated many of our Fluent Design pages with new information and guidance: </br> * The [Fluent design overview](/windows/apps/fluent-design-system) has been updated to align to the latest Fluent features. </br> * [Navigation history and backwards navigation](/windows/apps/design/basics/navigation-history-and-backwards-navigation) has been revamped, with detailed examples, guidance for device optimization, and guidelines for custom behavior.
Focus navigation | The new [focus navigation](/windows/apps/design/input/focus-navigation) topic describes how to optimize a UWP application for users that rely on non-pointing input types, such as keyboards, gamepads, or remote controls. In addition, [programmatic focus navigation](/windows/apps/design/input/focus-navigation-programmatic) describes the APIs you can use to enhance these experiences.
Keyboard shortcuts | Our guidance on [keyboard accelerators](/windows/apps/design/input/keyboard-accelerators) has been updated with new usability information. Add tooltips to your keyboard accelerators and labels to your controls to improve discoverability, or override default keyboard accelerator behavior with new APIs.
Page layouts | We've updated our [XAML page layout](/windows/apps/design/layout/layouts-with-xaml) docs with new information on fluid layouts and visual states. These features allow for greater control over how the position of elements in your app respond and adapt to the available visual space.
Pull to refresh | The [Pull to refresh](/windows/apps/design/controls/pull-to-refresh) control allows a user to pull down a list of data in order to retrieve more data. It is widely used on devices with a touch screen.
Navigation view | The [Navigation view](/windows/apps/design/controls/navigationview) control provides a collapsible navigation menu for top-level navigation in your app. This control implements the nav pane, or hamburger menu, pattern and automatically adapts the pane's display mode to different window sizes.
Reveal focus | The new [Reveal focus](/windows/apps/design/style/reveal-focus) effect provides lighting for experiences such as Xbox One and television screens. It animates the border of focusable elements, such as buttons, when the user moves gamepad or keyboard focus to them.
Sound | XAML now supports 3D Audio with the **SpatialAudioMode** property. See [Sound](/windows/apps/design/style/sound) for information on how it can be configured.
Tiles | [Chaseable tile notifications](/windows/apps/design/shell/tiles-and-notifications/chaseable-tile-notifications) are now supported in JavaScript-based UWP apps.<br><br>Secondary tile and badge notifications are [now supported from Desktop Bridge apps](/windows/apps/design/shell/tiles-and-notifications/secondary-tiles-desktop-pinning#send-tile-notifications).
Tree View | The [TreeView](/windows/apps/design/controls/tree-view) control enables a hierarchical list with expanding and collapsing nodes that contain nested items. It can be used to illustrate a folder structure or nested relationships in your UI.
Writing style | We've upgraded and expanded our article on voice and tone, transforming it into [Writing style guidance](/windows/apps/design/style/writing-style). This new information provides principles for creating effective text in your app, and recommends best practices for writing for controls such as error messages or dialogs.

## Gaming
Feature | Description
 :------ | :------
Getting started for game development | Interested in developing games for Windows 10? The new [Getting started for game development](../gaming/getting-started.md) page gives you a full overview of what you need to do to get yourself set up, registered, and ready to submit your apps and games.
Graphics adapters | The following DXGI APIs have been added, which are related to graphics adapter preference and removal: </br> * The [IDXGIFactory6](/windows/desktop/api/dxgi1_6/nn-dxgi1_6-idxgifactory6) interface enables a single method that enumerates graphics adapters based on a given GPU preference. </br> * The [DXGIDeclareAdapterRemovalSupport](/windows/desktop/api/dxgi1_6/nf-dxgi1_6-dxgideclareadapterremovalsupport) function allows a process to indicate that it's resilient to any of its graphics devices being removed. </br> * The [DXGI_GPU_PREFERENCE](/windows/desktop/api/dxgi1_6/ne-dxgi1_6-dxgi_gpu_preference) enumeration describes the preference of GPU for the app to run on.

## Develop Windows apps

Feature | Description
 :------ | :------
Adaptive Cards | [Adaptive cards](/adaptive-cards/) are an open card exchange format enbling developers to exchange UI content in a common and consistent way. They describe their content as a JSON object that can be rendered to automatically adapt to the look and feel of the host application.
App Resource Group | The [AppResourceGroupInfo](/uwp/api/windows.system.appresourcegroupinfo) class has new methods that you can use to initiate the transition to the app suspended, active (resumed), and terminated states.
Broad file-system access | The **broadFileSystemAccess** capability grants apps the same access to the file system as the user who is currently running the app without file-picker style prompts. For more info, see [File access permissions](../files/file-access-permissions.md) and the **broadFileSystemAccess** entry in [App capability declarations](../packaging/app-capability-declarations.md).
C++/WinRT | [C++/WinRT](../cpp-and-winrt-apis/index.md) is a new, entirely standard, modern C++17 language projection for Windows Runtime (WinRT) APIs. It's implemented solely in header files, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume WinRT APIs using any standards-compliant C++17 compiler. For your C++ applications — from Win32 to UWP — use C++/WinRT to keep your code standard, modern, and clean, and your application lightweight and fast.
Console UWP apps | You can now write C++ /WinRT or /CX UWP console apps that run in a console window such as a DOS or PowerShell console window. Console apps use the console window for input and output. UWP console apps can be published to the Microsoft Store, have an entry in the app list, and a primary tile that can be pinned to the Start menu. For more info, see [Create a Universal Windows Platform console app](../launch-resume/console-uwp.md)
Expanded app manifest capabilities | Several features have been added to the App Package Manifest schema, including: broad file system access, enabling barcode scanners for point-of-service devices, defining a UWP console app, and more. See [app manifest changes in Windows 10](/uwp/schemas/appxpackage/uapmanifestschema/what-s-changed-in-windows-10) for more details.
Landmarks and Headings supported for accessible technology (AT) | Landmarks and headings define sections of a user interface that aid in efficient navigation for users of assistive technology such as screen readers. For more information see [Landmarks and Headings](/windows/apps/design/accessibility/landmarks-and-headings).
Machine Learning | Windows Machine Learning allows you to build apps that evaluate pre-trained machine learning models locally on your Windows 10 devices. To learn more about the platform, see [Windows Machine Learning](/windows/ai/). </br> The [MachineLearning](/uwp/api/windows.ai.machinelearning.preview) namespace contains classes that enable apps to load machine learning models, bind data as inputs, and evaluate the results.
Map Controls | The [MapControl](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol) class has a new property named **Region** that you can use to show contents in a map control based on the language of a specific region (for example, the state or province).
Map Elements | The [MapElement](/uwp/api/windows.ui.xaml.controls.maps.mapelement) class has a new property named **IsEnabled** that you can use to specify whether users can interact with the [MapElement](/uwp/api/windows.ui.xaml.controls.maps.mapelement).
Map Place Info | The [PlaceInfo](/uwp/api/windows.services.maps.placeinfo) class contains a new method **CreateFromAddress** that you can use to create a [PlaceInfo](/uwp/api/windows.services.maps.placeinfo) by using an address and display name.
Map Services | The [MapRouteDrivingOptions](/uwp/api/windows.services.maps.maproutedrivingoptions) class contains a new property named **DepartureTime** that you can use to compute a route with the traffic conditions that are typical for the specified day and time.
Multi-instance UWP apps | A UWP app can opt-in to support multiple instances. If an instance of a multi-instance UWP app is running, and a subsequent activation request comes through, the platform will not activate the existing instance. Instead, it will create a new instance, running in a separate process. For more info, see [Create a multi-instance Universal Windows App](../launch-resume/multi-instance-uwp.md).
Package resource indexing APIs and custom build systems | With [package resource indexing (PRI) APIs](../app-resources/pri-apis-custom-build-systems.md), you can develop a custom build system for your UWP app's resources. The build system will be able to create, version, and dump PRI files to whatever level of complexity your UWP app needs. If you have a custom build system that currently uses the MakePri.exe command-line tool, we recommend that you switch over to calling the PRI APIs instead, as they provide increased performance and control.
PlayReady | Microsoft PlayReady is a set of technologies for protecting digital content from unauthorized usage. PlayReady runs on all sorts of devices and apps, and across all operating systems. [Learn how to incorporate PlayReady in your app.](/playready/)
Private Audience | If you want your app’s Store listing to be visible only to selected people that you specify, use the new **Private audience** option. The app will not be discoverable or available to anyone other than people in the group(s) you specify. This option is useful for beta testing, as it lets you distribute your app to testers without anyone else being able to get the app, or even see its Store listing. For more info, see [Choose visibility options](/windows/apps/publish/publish-your-app/visibility-options?pivots=store-installer-msix).
Progressive Web Apps | Microsoft Edge and UWP web apps now support [Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps)! </br> * Using [standards-based web technologies](https://developer.mozilla.org/Apps/Progressive) and [feature detection](https://github.com/tomayac/pwa-feature-detector), you can enhance your web apps to provide native app experiences, including push notifications, offline support, and OS integration, while still offering a great baseline web app experience on browsers and platforms that don’t yet support PWA technologies. </br> * [Adding a manifest file](/microsoft-edge/progressive-web-apps-chromium/how-to/web-app-manifests) to your app enables it to be installed across the entire UWP device family (including secure [Windows 10 S-mode devices](https://www.microsoft.com/windows/windows-10-s)) and distributed from the [Microsoft Store](https://apps.microsoft.com/store/apps?hl=en-us&gl=us&rtc=1). </br> PWAs are a natural evolution of *Hosted Web Apps*, but with standards-based support for offline scenarios, thanks to the *Service Workers*, *Cache*, and *Push APIs*.
Screen capture | The [Windows.Graphics.Capture namespace](/uwp/api/windows.graphics.capture) provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences. See [Screen capture](../audio-video-camera/screen-capture.md) for more information.
System Triggers | The [CustomSystemEventTrigger](/uwp/api/windows.applicationmodel.background.customsystemeventtrigger) allows you to define a system trigger when the OS doesn't provide a system trigger that you need. Such as when a hardware driver and the UWP app both belong to 3rd party, and the hardware driver needs to raise a custom event that its app handles. For example, an audio card that needs to notify a user when an audio jack is plugged in.
User Activities | New [UserActivity documentation](../launch-resume/useractivities.md) explains how to help users resume what they were doing in your app, even across multiple devices.</br>The **UserActivitySessionHistoryItem** class has new methods that retrieve recent user activities. See [GetRecentUserActivitiesAsync](/uwp/api/windows.applicationmodel.useractivities.useractivitychannel.getrecentuseractivitiesasync), and its overload, for details.
Windows Mixed Reality APIs | To support the growing Windows Mixed Reality platform, new APIs have been added to the [Windows.Graphic.Holographic](/uwp/api/Windows.Graphics.Holographic) and [Windows.UI.Input.Spatial](/uwp/api/Windows.UI.Input.Spatial) namespaces.
Windows Mixed Reality docs | Developer guidance is published to [Windows Mixed Reality documentation](/windows/mixed-reality/). Just like in these docs, you can now file feedback with GitHub Issues or submit your own contributions via a pull request.

## Publish & Monetize Windows apps

Feature | Description
 :------ | :------
Download and install package updates from the Store | We've updated [Download and install package updates from the Store](../packaging/self-install-package-updates.md) with new guidance and examples about how to download and install package updates without displaying a notification UI to the user, uninstall an optional package, and get info about packages in the download and install queue for your app.
Enter free-form prices in a specific market's local currency | When you override your app's base price for a specific market, you are no longer limited to choosing one of the standard price tiers; you now have the option to enter a free-form price in the market's local currency. For more info, see [Set and schedule app pricing](/windows/apps/publish/publish-your-app/schedule-pricing-changes?pivots=store-installer-msix). **This feature is available to all Windows developers and does not require the updated SDK.**
Store Context | The [StoreContext](/uwp/api/windows.services.store.storecontext) class has been updated with a selection of new methods. These methods manage the downloading and installation of package updates and add-ons for an app.
Subscription add-ons are now available to all developers | Create and publish subscription add-ons to sell digital products in your apps and games (such as app features or digital content) with automated recurring billing periods. For more details, see [Enable subscription add-ons for your app](../monetize/enable-subscription-add-ons-for-your-app.md). **This feature is available to all Windows developers and does not require the updated SDK.**

## Videos

The following videos have been published since the Fall Creator's Update, highlighting new and improved features in Windows 10 for developers.

### Accessibility tools for Windows developers

The Windows 10 SDK includes several tools to help you test for and improve the accessibility of your app. The Inspect and AccEvent tools help you ensure your apps are available to all. [Watch the video](https://www.youtube.com/watch?v=ce0hKQfY9B8&list=PLWs4_NfqMtoycBFndriDmkQlMLwflyoFF&t=0s&index=1) to learn about these tools, then [read more about accessibility testing](/windows/apps/design/accessibility/accessibility-testing) for more information.

### Creating 3D app launchers for Windows Mixed Reality

3D launchers provide a unique way for users to place a truly volumetric representation of your app in their Mixed Reality home environment. [Watch the video](https://www.youtube.com/watch?v=TxIslHsEXno) to learn how to prepare your 3D model and assign it as the launcher for your app, then [read the developer docs](https://developer.microsoft.com/windows/mixed-reality/implementing_3d_app_launchers) and [check out our design guidance](https://developer.microsoft.com/windows/mixed-reality/3d_app_launcher_design_guidance) for more information.

### Creating a UWP Console App

You can now create UWP apps that run inside a PowerShell or DOS console window. [Watch the video](https://www.youtube.com/watch?v=bwvfrguY20s&t=0s&index=1&list=PLWs4_NfqMtoycBFndriDmkQlMLwflyoFF) to learn how, then [check out the docs](../launch-resume/console-uwp.md) for more information. 

### How to use Windows ML in your app

Windows Machine Learning allows you to build apps that evaluate pre-trained machine learning models locally on your Windows 10 devices. [Watch the video](https://www.youtube.com/watch?v=8MCDSlm326U&index=2&list=PLWs4_NfqMtoycBFndriDmkQlMLwflyoFF) for a quick walkthrough, then [read the docs](/windows/uwp/machine-learning/) for the full story.

### Motion controller tracking

Motion controllers represent a user's hands in Windows Mixed Reality. [Watch the video](https://www.youtube.com/watch?v=rkDpRllbLII) to learn how the motion controllers work when they are both in and out of the field of view of the Mixed Reality headset, and [read more about controller tracking here.](/windows/mixed-reality/motion-controllers#controller_tracking_state%E2%80%9D)

### Package a .NET app in Visual Studio

It's easier than ever to bring your desktop app to the Universal Windows Platform. [Watch the video](https://www.youtube.com/watch?v=fJkbYPyd08w) to learn how to package your .NET app for distribution, then [check out this page](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) for more information.

### Xbox Live Creators Program

The Xbox Live Creators Program allows developers to quickly publish their UWP games to Xbox One and Windows 10. [Watch the video](https://www.youtube.com/watch?v=zpFfHHBkVq4) to learn about the program, then [check out this page](https://www.xbox.com/developers/creators-program) to get started.

### One Dev Question - Why was Docments and Settings renamed Users?

Curious why the Documents and Settings directory was renamed? [Raymond Chen explains where the name came from, and why it was changed](https://www.youtube.com/watch?v=4vDHQewVmM8&index=1&list=PLWs4_NfqMtoxjy3LrIdf2oamq1coolpZ7). For more developement details about Windows and its history, check out [Raymond's blog.](https://devblogs.microsoft.com/oldnewthing/)


## Samples

### Coloring Book

The [Coloring Book sample](https://github.com/Microsoft/Windows-appsample-coloringbook) has received a major update to incorporate advanced Ink scenarios including improved ink rendering performance using the custom ink drying APIs. It also includes support for flood fill and coloring inside the lines for regions defined by the artwork. 

### Photo Lab

The [Photo Lab sample](https://github.com/Microsoft/Windows-appsample-photo-lab) has been updated to load images from the Pictures library using data virtualization to increase performance when there are numerous files. Also, the image editing page in the sample now uses the [XamlCompositionBrushBase](/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase) class to apply effects.