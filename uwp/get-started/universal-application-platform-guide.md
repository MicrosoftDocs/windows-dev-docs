---
title: What's a Universal Windows Platform (UWP) app?
description: Learn about Universal Windows Platform (UWP) apps that can run across a wide variety of devices that run Windows.
ms.assetid: 59849197-B5C7-493C-8581-ADD6F5F8800B
ms.date: 08/21/2024
ms.topic: article
keywords: windows 10, uwp, universal
ms.localizationpriority: medium
---

# What's a Universal Windows Platform (UWP) app?

UWP is one of many ways to create client applications for Windows. 

> [!NOTE]
> If you are starting to develop Windows apps, we recommend you consider using the [Windows App SDK](/windows/apps/windows-app-sdk/), and [WinUI](/windows/apps/develop/) rather than UWP. Although still supported, UWP is not under active development. Please see [Start developing Windows apps](/windows/apps/get-started/start-here) for more information.

To download the tools you will need to start creating Windows apps, see [Install tools for the Windows App SDK](/windows/apps/windows-app-sdk/set-up-your-development-environment), and then [write your first app](your-first-app.md).

## Where does UWP fit in the Microsoft development story?

UWP is one choice for creating apps that run on Windows 10 and Windows 11 devices, and can be combined with other platforms. UWP apps can make use of Win32 APIs and .NET classes (see [API Sets for UWP apps](/previous-versions/mt186421(v=vs.85)), [Dlls for UWP apps](/previous-versions/mt186422(v=vs.85)), and [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0&preserve-view=true)).



## Features of a UWP app

A UWP app is:

- Secure: UWP apps declare which device resources and data they access. The user must authorize that access.
- Able to use a common API on all devices that run Windows.
- Able to use device specific capabilities and adapt the UI to different device screen sizes, resolutions, and DPI.
- Available from the Microsoft Store on all devices (or only those that you specify) that run on Windows 10 or Windows 11. The Microsoft Store provides multiple ways to make money on your app.
- Able to be installed and uninstalled without risk to the machine or incurring "machine rot".
- Engaging: use live tiles, push notifications, and user activities that interact with Windows Timeline and Cortana's Pick Up Where I Left Off, to engage users.
- Programmable in C#, C++, Visual Basic, and JavaScript. For UI, use WinUI, XAML, HTML, or DirectX.

Let's look at these in more detail.

### Secure

UWP apps declare in their manifest the device capabilities they need such as access to the microphone, location, Webcam, USB devices, files, and so on. The user must acknowledge and authorize that access before the app is granted the capability.

### A common API surface across all devices

Windows 10 introduced the Universal Windows Platform (UWP), which provides a common app platform on every device that runs Windows. The UWP core APIs are the same on all Windows devices. If your app only uses the core APIs, it will run on any Windows device no matter whether you are targeting a desktop PC, Xbox, Mixed-reality headset, and so on.

A UWP app written in [C++/WinRT](../cpp-and-winrt-apis/index.md) has access to the Win32 APIs that are part of the UWP. These Win32 APIs are implemented by all Windows devices.

### Extension SDKs expose the unique capabilities of specific device types

If you target the universal APIs, then your app can run on all devices that run Windows 10 or later. But if you want your UWP app to take advantage of device-specific APIs, then you can do that, too.

Extension SDKs let you call specialized APIs for different devices. For example, if your UWP app targets an IoT device, you can add the IoT extension SDK to your project to target features specific to IoT devices. For more information about adding extension SDKs, see the **Extension SDKs** section in [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview#extension-sdks).

You can write your app so that you expect it to run only on a particular type of device, and then limit its distribution from the Microsoft Store to just that type of device. Or, you can conditionally test for the presence of an API at runtime and adapt your app's behavior accordingly. For more information, see the **Writing code** section in [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview#writing-code).<br>


### Adaptive controls and input

UI elements respond to the size and DPI of the screen the app is running on by adjusting their layout and scale. UWP apps work well with multiple types of input such as keyboard, mouse, touch, pen, and game controllers. If you need to further tailor your UI to a specific screen size or device, new layout panels and tooling help you design UI that can adapt to the different devices and form factors that your app may run on.

![Windows-powered devices](images/1894834-hig-device-primer-01-500.png)

Windows helps you target your UI to multiple devices with the following features:

- Universal controls and layout panels help you to optimize your UI for the screen resolution of the device. For example, controls such as buttons and sliders automatically adapt to device screen size and DPI density. Layout panels help adjust the layout of content based on the size of the screen. Adaptive scaling adjusts to resolution and DPI differences across devices.
- Common input handling allows you to receive input through touch, a pen, a mouse, a keyboard, or a game controller.
- Tooling that helps you to design UI that can adapt to different screen resolutions.

Some aspects of your app's UI will automatically adapt across devices. Your app's user-experience design, however, may need to adapt depending on the device the app is running on. For example, a photo app could adapt its UI when running on a small, handheld device to ensure that usage is ideal for single-handed use. When a photo app is running on a desktop computer, the UI should adapt to take advantage of the additional screen space.

### There's one store for all devices

A unified app store makes your app available on Windows devices such as PC, tablet, Xbox, HoloLens, Surface Hub, and Internet of Things (IoT) devices. You can submit your app to the store and make it available to all types of devices, or only those you choose. You submit and manage all your apps for Windows devices in one place. Have a C++ desktop app that you want to modernize with UWP features and sell in the Microsoft store? That's okay, too.

UWP apps integrate with [Application Insights](https://azure.microsoft.com/services/application-insights/) for detailed telemetry and analytics—a crucial tool for understanding your users and improving your apps.

UWP apps can be packaged with [MSIX](/windows/msix/) and distributed via the Microsoft Store, or in other ways. MSIX allows apps to be updated no matter how they are distributed, see [Update non-Store published app packages from your code](/windows/msix/non-store-developer-updates).

### Monetize your app

You can choose how you'll monetize your app. There are a number of ways to make money with your app. All you need to do is choose the one that works best for you, for example:

- A paid download is the simplest option. Just name the price.
- Trials let users try your app before buying it, providing easier discoverability and conversion than the more traditional "freemium" options.
- Sale prices to incentivize users.
- In-app purchases.


### Deliver relevant, real-time info to your users to keep them coming back

There are a variety of ways to keep users engaged with your UWP app:

- Live tiles and lock screen tiles that show contextually relevant and timely info from your app at a glance.
- Push notifications that bring real-time alerts to your user's attention.
- User Activities allow users to pick up where they left off in your app, even across devices.
- The Action Center organizes notifications from your app.
- Background execution and triggers bring your app into action when the user needs it.
- Your app can use voice and Bluetooth LE devices to help users interact with the world around them.
- Integrate Cortana to add voice command capability to your app.

##  Use a language you already know

UWP apps use the Windows Runtime, the native API provided by the operating system. This API is implemented in C++ and is supported in C#, Visual Basic, C++, and JavaScript. Some options for writing UWP apps include:

- XAML UI and C#, VB, or C++
- DirectX UI and C++
- JavaScript and HTML
- WinUI

## Links to help you get going

### Get set up

Check out [Get set up](/windows/apps/get-started/get-set-up) to download the tools you need to start creating apps, and then [write your first app](your-first-app.md).

### Design your app

The Microsoft design system is named Fluent. The Fluent Design System is a set of UWP features combined with best practices for creating apps that perform beautifully on all types of Windows-powered devices. Fluent experiences adapt and feel natural on devices from tablets to laptops, from PCs to televisions, and on virtual reality devices. See [The Fluent Design System for UWP apps](/windows/uwp/design/fluent-design-system) for an introduction to Fluent Design.

Good [design](/windows/apps/design/) is the process of deciding how users will interact with your app, in addition to how it will look and function. User experience plays a huge part in determining how happy people will be with your app, so don't skimp on this step. [Design basics](https://developer.microsoft.com/windows/apps/design) introduces you to designing a Universal Windows app. See the [device primer](/windows/apps/design/devices/index) to help you think through the interaction experience of using your app on all the different form factors you want to target.

In addition to interaction on different devices, [plan your app](./plan-your-app.md) to embrace the benefits of working across multiple devices. For example:

- Design your workflow using [Navigation design basics for UWP apps](/windows/apps/design/basics/navigation-basics) to accommodate mobile, small-screen, and large-screen devices. [Lay out your user interface](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design) to respond to different screen sizes and resolutions.

- Consider how you'll accommodate multiple kinds of input. See the [Guidelines for interactions](/windows/apps/design/layout/index) to learn how users can interact with your app by using [Speech](/windows/apps/design/input/speech-interactions), [Touch interactions](/windows/apps/design/input/touch-interactions), the [Touch keyboard](/windows/apps/design/input/keyboard-interactions) and more.  Or, see the [Guidelines for text and text input](/windows/apps/design/controls/text-controls) for more traditional interaction experiences.

### Add services

- Use [Azure Cloud Services](/azure/cloud-services) to sync across devices.
- Learn how to [connect to web services](/previous-versions/windows/apps/hh761504(v=win.10)) to support your app experience.
- Include [Push notifications](/windows/apps/design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview) and [in-app purchases](../monetize/enable-in-app-product-purchases.md) in your planning. These features should work across devices.

### Submit your app to the Store

[Partner Center](https://partner.microsoft.com/dashboard) lets you manage and submit all of your apps for Windows devices in one place. See [Publish Windows apps and games](/windows/apps/publish/index) to learn how to submit your apps for publication in the Microsoft Store.

New features simplify processes while giving you more control. You'll also find detailed [analytic reports](/windows/apps/publish/analytics) combined [payout details](/partner-center/payout-statement), ways to [promote your app and engage with your customers](/windows/apps/publish/attract-customers-and-promote-your-apps), and much more.

For more introductory material, see [An Introduction to Building Windows Apps for Windows 10 Devices](/archive/msdn-magazine/2015/may/windows-10-an-introduction-to-building-windows-apps-for-windows-10-devices)

### More advanced topics

- Learn how to use [User Activities](https://blogs.windows.com/buildingapps/2017/12/19/application-engagement-windows-timeline-user-activities/#tHuZ6tLPtCXqYKvw.97) so that user activity in your app appear in Windows Timeline and Cortana's Pick Up Where I Left Off feature.
- Learn how to use [Tiles, badges, and notifications for UWP apps](/windows/apps/design/shell/tiles-and-notifications/index).
- For the full list of Win32 APIs available to UWP apps, see [API Sets for UWP apps](/previous-versions/mt186421(v=vs.85)) and [Dlls for UWP apps](/previous-versions/mt186422(v=vs.85)).
- See [Universal Windows apps in .NET](https://devblogs.microsoft.com/dotnet/universal-windows-apps-in-net/) for an overview of writing .NET UWP apps.
- For a list of .NET types that you can use in a UWP app, see [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0&preserve-view=true)
- [Compiling apps with .NET Native](/dotnet/framework/net-native/)
- Learn how to add modern experiences for Windows users to your existing desktop app, and distribute it in the Microsoft Store with the [Desktop Bridge](/windows/msix/desktop/source-code-overview).

## How the Universal Windows Platform relates to Windows Runtime APIs
If you're building a Universal Windows Platform (UWP) app, then you can get a lot of mileage and convenience out of treating the terms "Universal Windows Platform (UWP)" and "Windows Runtime (WinRT)" as more or less synonymous. But it *is* possible to look under the covers of the technology, and determine just what the difference is between those ideas. If you're curious about that, then this last section is for you.

The Windows Runtime, and WinRT APIs, are an evolution of Windows APIs. Originally, Windows was programmed via flat, C-style Win32 APIs. To those were added COM APIs ([DirectX](/windows/desktop/directx) being a prominent example). Windows Forms, WPF, .NET, and managed languages brought their own way of writing Windows apps, and their own flavor of API technology. The Windows Runtime is, under the covers, the next stage of COM. At the actual application binary interface (ABI) layer, its roots in COM become visible. But the Windows Runtime was designed to be callable from a great range of different programming languages. And callable in a way that's very natural to each of those languages. To this end, access to the Windows Runtime is made available via what are known as language projections. There is a Windows Runtime language projection into C#, into Visual Basic, into standard C++, into JavaScript, and so on. Furthermore, once packaged appropriately (see [Desktop Bridge](/windows/msix/desktop/source-code-overview)), you can call WinRT APIs from an app built in one of a great range of application models: Win32, .NET, WinForms, and WPF.

And, of course, you can call WinRT APIs from your UWP app. UWP is an application model built on top of the Windows Runtime. Technically, the UWP application model is based on [CoreApplication](/uwp/api/windows.applicationmodel.core.coreapplication), although that detail may be hidden from you, depending on your choice of programming language. As this topic has explained, from a value proposition point of view, the UWP lends itself to writing a single binary that can, should you choose, be published to the Microsoft Store and run on any one of a great range of device form factors. The device reach of your UWP app depends on the subset of Windows Runtime APIs that you limit your app to calling, or that you call conditionally.

Hopefully, this section has been successful in describing the difference between the technology underlying Windows Runtime APIs, and the mechanism and business value of the Universal Windows Platform.
