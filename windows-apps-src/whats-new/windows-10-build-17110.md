---
author: QuinnRadich
title: What's New in Windows 10 for Developers, Tools & Features
description: Windows 10 build 17110 and new developer tools provide the tools, features, and experiences powered by the Universal Windows Platform.
keywords: what's new, whats new, update, updates, features, new, Windows 10, newest, developers, 17110, preview
ms.author: quradic
ms.date: 3/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: high
---

# What's New in Windows 10 for developers, SDK preview build 17110

Windows 10 SDK preview build 17110, in combination with Visual Studio 2017 and the updated SDK, provide the tools, features, and experiences to make remarkable Universal Windows Platform apps. [Install the tools and SDK](http://go.microsoft.com/fwlink/?LinkId=821431) on Windows 10 and you’re ready to either [create a new Universal Windows app](../get-started/create-uwp-apps.md) or explore how you can use your [existing app code on Windows](../porting/index.md).

This is a collection of new and improved features and guidance of interest to Windows developers in this SDK preview. For now, these features are accessible to members of the [Windows Insider Program](https://insider.windows.com/en-us/), and will be made publicly available in the next major update of Windows 10. For a full list of new namespaces added to the Windows SDK, see the [Windows 10 build 17110 API changes](windows-10-build-17110-api-diff.md).For more information on the highlighted features of Windows 10, see [What's cool in Windows 10](http://go.microsoft.com/fwlink/?LinkId=823181). In addition, see [Windows Developer Platform features](https://developer.microsoft.com/windows/platform/features) for a high-level overview of both past and future additions to the Windows platform.

## Design & UI

Feature | Description
 :------ | :------
Adaptive and interactive toast notifications | Enhance your app with adaptive and interactive notifications. Get started with our [updated guidance on toast notifications](../design/shell/tiles-and-notifications/adaptive-interactive-toasts.md), and explore the new information on image size restrictions, progress bars, and adding input options.
Content links | The new [Content links](../design/controls-and-patterns/content-links.md) control provides a way to embed rich data in your text controls, which lets a user find and use more information about a person or place without leaving the context of your app.
Design samples | The BuildCast sample has been added to the [Design toolkits and samples](../design/downloads/index.md) page. BuildCast is an end-to-end sample built to showcase the Fluent Design System and other capabilities of the Universal Windows Platform.
Embedded handwriting | The pen input feature has been added to [text controls](../design/controls-and-patterns/text-controls.md), enabling users to write directly into text boxes with Windows Ink. As the user writes, the text is converted to a script that maintains the feel of natural writing.
Fluent Design updates | We've updated many of our Fluent Design pages with new information and guidance: </br> * The [Fluent design overview](../design/fluent-design-system/index.md) has been updated to align to the latest Fluent features. </br> * [Reveal highlight](../design/style/reveal.md) has new guidance on themes and custom controls. </br> * [Navigation history and backwards navigation](../design/basics/navigation-history-and-backwards-navigation.md) has been revamped, with detailed examples, guidance for device optimization, and guidelines for custom behavior.
Page layouts | We've updated our [XAML page layout](../design/layout/layouts-with-xaml.md) docs with new information on fluid layouts and visual states. These features allow for greater control over how the position of elements in your app respond and adapt to the available visual space.
Pull to refresh | The [Pull to refresh](../design/controls-and-patterns/pull-to-refresh.md) control allows a user to pull down a list of data in order to retrieve more data. It is widely used on devices with a touch screen.
Navigation view | The [Navigation view](../design/controls-and-patterns/navigationview.md) control provides a collapsible navigation menu for top-level navigation in your app. This control implements the nav pane, or hamburger menu, pattern and automatically adapts the pane's display mode to different window sizes.
Reveal focus | The new [Reveal focus](../design/style/reveal-focus.md) effect provides lighting for experiences such as Xbox One and television screens. It animates the border of focusable elements, such as buttons, when the user moves gamepad or keyboard focus to them.
Sound | XAML now supports 3D Audio with the **SpatialAudioMode** property. See [Sound](../design/style/sound.md) for information on how it can be configured.
Tree View | The [TreeView](../design/controls-and-patterns/tree-view.md) control enables a hierarchical list with expanding and collapsing nodes that contain nested items. It can be used to illustrate a folder structure or nested relationships in your UI.
Writing style | We've upgraded and expanded our article on voice and tone, transforming it into [Writing style guidance](../design/style/writing-style.md). This new information provides principles for creating effective text in your app, and recommends best practices for writing for controls such as error messages or dialogs.

## Gaming
Feature | Description
 :------ | :------
Getting started for game development | Interested in developing games for Windows 10? The new [Getting started for game development](../gaming/getting-started.md) page gives you a full overview of what you need to do to get yourself set up, registered, and ready to submit your apps and games.
Graphics adapters | The following DXGI APIs have been added, which are related to graphics adapter preference and removal: </br> * The [IDXGIFactory6](https://msdn.microsoft.com/library/windows/desktop/mt814823) interface enables a single method that enumerates graphics adapters based on a given GPU preference. </br> * The [DXGIDeclareAdapterRemovalSupport](https://msdn.microsoft.com/library/windows/desktop/mt814821) function allows a process to indicate that it's resilient to any of its graphics devices being removed. </br> * The [DXGI_GPU_PREFERENCE](https://msdn.microsoft.com/library/windows/desktop/mt814822) enumeration describes the preference of GPU for the app to run on.


## Develop Windows apps

Feature | Description
 :------ | :------
Adaptive Cards | [Adaptive cards](https://docs.microsoft.com/adaptive-cards/) are an open card exchange format enbling developers to exchange UI content in a common and consistent way. They describe their content as a JSON object that can be rendered to automatically adapt to the look and feel of the host application.
App Resource Group | The [AppResourceGroupInfo](https://docs.microsoft.com/uwp/api/windows.system.appresourcegroupinfo) class has new methods that you can use to initiate the transition to the app suspended, active (resumed), and terminated states.
Broad file-system access | The **broadFileSystemAccess** capability grants apps the same access to the file system as the user who is currently running the app without file-picker style prompts. For more info, see [File access permissions](../files/file-access-permissions.md) and the **broadFileSystemAccess** entry in [App capability declarations](../packaging/app-capability-declarations.md).
Console UWP apps | You can now write C++ /WinRT or /CX UWP console apps that run in a console window such as a DOS or PowerShell console window. Console apps use the console window for input and output. UWP console apps can be published to the Microsoft Store, have an entry in the app list, and a primary tile that can be pinned to the Start menu. For more info, see [Create a Universal Windows Platform console app](../launch-resume/console-uwp.md)
Landmarks and Headings supported for accessible technology (AT) | Landmarks and headings define sections of a user interface that aid in efficient navigation for users of assistive technology such as screen readers. For more information see [Landmarks and Headings](../design/accessibility/landmarks-and-headings.md).
Machine Learning | Windows Machine Learning allows you to build apps that evaluate pre-trained machine learning models locally on your Windows 10 devices. To learn more about the platform, see [Windows Machine Learning](../machine-learning/index.md). </br> The [MachineLearning](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview) namespace contains classes that enable apps to load machine learning models, bind data as inputs, and evaluate the results.
Map Controls | The [MapControl](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapcontrol) class has a new property named **Region** that you can use to show contents in a map control based on the language of a specific region (for example, the state or province).
Map Elements | The [MapElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapelement) class has a new property named **IsEnabled** that you can use to specify whether users can interact with the [MapElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapelement).
Map Place Info | The [PlaceInfo](https://docs.microsoft.com/uwp/api/windows.services.maps.placeinfo) class contains a new method **CreateFromAddress** that you can use to create a [PlaceInfo](https://docs.microsoft.com/uwp/api/windows.services.maps.placeinfo) by using an address and display name.
Map Services | The [MapRouteDrivingOptions](https://docs.microsoft.com/uwp/api/windows.services.maps.maproutedrivingoptions) class contains a new property named **DepartureTime** that you can use to compute a route with the traffic conditions that are typical for the specified day and time.
Multi-instance UWP apps | A UWP app can opt-in to support multiple instances. If an instance of an multi-instance UWP app is running, and a subsequent activation request comes through, the platform will not activate the existing instance. Instead, it will create a new instance, running in a separate process. For more info, see [Create a multi-instance Universal Windows App](../launch-resume/multi-instance-uwp.md).
PlayReady | Microsoft PlayReady is a set of technologies for protecting digital content from unauthorized usage. PlayReady runs on all sorts of devices and apps, and across all operating systems. [Learn how to incorporate PlayReady in your app.](https://docs.microsoft.com/playready/)
Screen capture | The [Windows.Graphics.Capture namespace](https://docs.microsoft.com/uwp/api/windows.graphics.capture) provides APIs to acquire frames from a display or application window, to create video streams or snapshots to build collaborative and interactive experiences. See [Screen capture](../audio-video-camera/screen-capture.md) for more information.
System Triggers | The [CustomSystemEventTrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.customsystemeventtrigger) allows you to define a system trigger when the OS doesn't provide a system trigger that you need. Such as when a hardware driver and the UWP app both belong to 3rd party, and the hardware driver needs to raise a custom event that its app handles. For example, an audio card that needs to notify a user when an audio jack is plugged in.
User Activities | The **UserActivitySessionHistoryItem** class has new methods that retrieve recent user activities. See [GetRecentUserActivitiesAsync](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitychannel#Windows_ApplicationModel_UserActivities_UserActivityChannel_GetRecentUserActivitiesAsync_System_Int32_), and its overload, for details.
Windows Mixed Reality | To support the growing Windows Mixed Reality platform, new APIs have been added to the [Windows.Graphic.Holographic](https://docs.microsoft.com/uwp/api/Windows.Graphics.Holographic) and [Windows.UI.Input.Spatial](https://docs.microsoft.com/uwp/api/Windows.UI.Input.Spatial) namespaces.

## Publish & Monetize Windows apps

Feature | Description
 :------ | :------
Enter free-form prices in a specific market's local currency | When you override your app's base price for a specific market, you are no longer limited to choosing one of the standard price tiers; you now have the option to enter a free-form price in the market's local currency. For more info, see [Set and schedule app pricing](../publish/set-and-schedule-app-pricing.md). **This feature is available to all Windows developers and does not require the updated SDK.**
Store Context | The [StoreContext](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext) class has been updated with a selection of new methods. These methods manage the downloading and installation of package updates and add-ons for an app.
Subscription add-ons are now available to all developers | Create and publish subscription add-ons to sell digital products in your apps and games (such as app features or digital content) with automated recurring billing periods. For more details, see [Enable subscription add-ons for your app](../monetize/enable-subscription-add-ons-for-your-app.md). **This feature is available to all Windows developers and does not require the updated SDK.**

## Videos

The following videos have been published since the Fall Creator's Update, highlighting new and improved features in Windows 10 for developers.

### Package a .NET app in Visual Studio

It's easier than ever to bring your desktop app to the Universal Windows Platform. [Watch the video](https://www.youtube.com/watch?v=fJkbYPyd08w) to learn how to package your .NET app for distribution, then [check out this page](../porting/desktop-to-uwp-packaging-dot-net.md) for more information.

### Xbox Live Creators Program

The Xbox Live Creators Program allows developers to quickly publish their UWP games to Xbox One and Windows 10.[Watch the video](https://www.youtube.com/watch?v=zpFfHHBkVq4) to learn about the program, then [check out this page](https://www.xbox.com/developers/creators-program) to get started.

### Creating 3D app launchers for Windows Mixed Reality

3D launchers provide a unique way for users to place a truly volumetric representation of your app in their Mixed Reality home environment. [Watch the video](https://www.youtube.com/watch?v=TxIslHsEXno) to learn how to prepare your 3D model and assign it as the launcher for your app, then [read the developer docs](https://developer.microsoft.com/windows/mixed-reality/implementing_3d_app_launchers) and [check out our design guidance](https://developer.microsoft.com/windows/mixed-reality/3d_app_launcher_design_guidance) for more information.

### Motion controller tracking

Motion controllers represent a user's hands in Windows Mixed Reality. [Watch the video](https://www.youtube.com/watch?v=rkDpRllbLII) to learn how the motion controllers work when they are both in and out of the field of view of the Mixed Reality headset, and [read more about controller tracking here.](https://developer.microsoft.com/windows/mixed-reality/motion_controllers#controller_tracking_state%E2%80%9D)

### Accessibility tools for Windows developers

The Windows 10 SDK includes several tools to help you test for and improve the accessibility of your app. The Inspect and AccEvent tools help you ensure your apps are available to all. [Watch the video](https://www.youtube.com/watch?v=ce0hKQfY9B8&list=PLWs4_NfqMtoycBFndriDmkQlMLwflyoFF&t=0s&index=1) to learn about these tools, then [read more about accessibility testing](../design/accessibility/accessibility-testing.md) for more information.