---
author: TylerMSFT
title: What's a Universal Windows Platform (UWP) app?
description: Learn about Universal Windows Platform (UWP) apps that can run across a wide variety of devices that run Windows 10.
ms.assetid: 59849197-B5C7-493C-8581-ADD6F5F8800B
ms.author: twhitney
ms.date: 3/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, what, universal
ms.localizationpriority: high
---
# What's a Universal Windows Platform (UWP) app?

Windows 10 introduces the Universal Windows Platform (UWP), which provides a common app platform on every device that runs Windows 10. The UWP provides a guaranteed core API across devices. New adaptive controls and layout panels help you to tailor your UI across a broad range of device screen resolutions and sizes, and respond to multiple kinds of device input. A unified app store makes your app available on Windows 10 devices such as PC, tablet, Xbox, HoloLens, Surface Hub, and Internet of Things (IoT) devices.

UWP is flexible. You can use languages such as C#, C++, Javascript, and VB. Have a C++ desktop app that you want to modernize with UWP features and sell in the Microsoft store? That's okay, too.

![Universal Windows Platform apps run on a variety of devices, support adaptive user interface, natural user input, one store, one dev center, and cloud services](images/universalapps-overview.png)

Because UWP apps can run on a wide variety of devices with different form factors and types of input, UWP makes it possible to unlock the unique capabilities of each device with extension SDKs allow you to exploit the capabilities of specific devices.

## UWP app characteristics

Here are some of the characteristics that make UWP apps on Windows 10 different from its predecessors.

- **There is a common API surface across all devices**

    The Universal Windows Platform (UWP) core APIs are the same for all Windows devices. If your app only uses the core APIs, it will run on any Windows 10 device no matter whether you are targeting a desktop PC, Xbox, Mixed Reality headset, and so on.

    A UWP app written in C++ /WinRT or C++ CX has access to the Win32 APIs that are part of the UWP. These Win32 APIs are implemented by all Windows 10 devices.

- **Extension SDKs expose the unique capabilities of specific device types**

    If you target the universal APIs, your app can run on all devices that run Windows 10. But if you want your UWP app to take advantage of device specific APIs, you can.

    An extension SDK lets you call specialized APIs for a device. For example, if your UWP app targets an IoT device, you can add the IoT extension SDK to your project to target features specific to IoT devices.

    You can write your app so that you expect it to run only on a particular type of device, and then limit its distribution from the Microsoft Store to just that type of device. Or, you can conditionally test for the presence of an API at runtime and adapt your app's behavior accordingly. For more information, see [Device families overview](https://docs.microsoft.com/en-us/uwp/extension-sdks/device-families-overview#writing-code).<br>

    <iframe src="https://channel9.msdn.com/Blogs/One-Dev-Minute/Introduction-to-UWP-and-Device-Families/player" width="640" height="360" allowFullScreen frameBorder="0"></iframe>

- **There's one store for all devices.**

    You can submit your app to the store and make it available to all types of devices, or only those you choose. You submit and manage all your apps for Windows devices in one place.

    UWP apps integrate with [Application Insights](http://azure.microsoft.com/en-us/services/application-insights/) for detailed telemetry and analytics—a crucial tool for understanding your users and improving your apps.

- **Apps distributed from the Store provide a seamless install, uninstall, and upgrade experience**

    All UWP apps are distributed using a packaging system that protects the user, device, and system. Users never need to regret installing an app. UWP apps can be uninstalled without leaving anything behind. Apps can be deployed and updated seamlessly. App packaging can be modularized so that you can download content and extensions on demand.

- **Apps support adaptive controls and input**

    UI elements respond to the size and DPI of the screen the app is running on by adjusting their layout and scale. And UWP apps work well with multiple types of input such as keyboard, mouse, touch, pen, and Xbox One controllers. If you need to further tailor your UI to a specific screen size or device, new layout panels and tooling help you design UI that can adapt to the devices your app may run on.

    ![Windows-powered devices](images/1894834-hig-device-primer-01-500.png)

## Use a language you already know

UWP apps use the Windows Runtime, the native API provided by the operating system. This API is implemented in C++ and is supported in C#, Visual Basic, C++, and JavaScript. Some options for writing UWP apps include:

- XAML UI and C#, VB, or C++
- DirectX UI and C++
- JavaScript and HTML

## UI and universal input

A UWP app can run on devices that have different forms of input, screen resolutions, DPI density, and other unique characteristics. Windows helps you target your UI to multiple devices with the following features:

- Universal controls and layout panels help you to optimize your UI for the screen resolution of the device. For example, controls such as buttons and sliders automatically adapt to device screen size and DPI density. Layout panels help adjust the layout of content based on the size of the screen. Adaptive scaling adjusts to resolution and DPI differences across devices.
- Common input handling allows you to receive input through touch, a pen, a mouse, a keyboard, or a controller such as a Microsoft Xbox controller.
- Tooling that helps you to design UI that can adapt to different screen resolutions.

Some aspects of your app's UI will automatically adapt across devices. Your app's user-experience design, however, may need to adapt depending on the device the app is running on. For example, a photo app should adapt to the UI when running on a small, hand-held device to ensure that usage is ideal for single-hand use. When a photo app is running on a desktop computer, the UI should adapt to take advantage of the additional screen space.

## UWP keeps users engaged with your app

Your UWP app can deliver relevant, real-time info to your users to keep them coming back:

- Live tiles and lock screen tiles that show contextually relevant and timely info from your app at a glance.
- Push notifications that bring real-time alerts to your user’s attention.
- The Action Center which organizes notifications from your app.
- Background execution and triggers that bring your app into action when the user needs it.
- Your app can use voice and Bluetooth LE devices to help users interact with the world around them.
- Integrate Cortana to add voice command capability to your app.

Finally, you can use roaming data and the Windows Credential Locker to enable a consistent roaming experience across all of the Windows screens where users run your app. Roaming data gives you an easy way to store a user’s preferences and settings in the cloud, without having to build your own sync infrastructure. And you can store user credentials in the Credential Locker, where security and reliability are the top priority.

## Monetize your app

You can choose how you'll monetize your app across tablets, PCs, and other devices. There are a number of ways to make money with your app. All you need to do is choose the one that works best for you, for example:

- A paid download is the simplest option. Just name your price.
- Trials let users try your app before buying it, providing easier discoverability and conversion than the more traditional "freemium" options.
- Sale prices to incentivize users.
- In-app purchases and ads are also available.

## Links to help you get going

### Get set up

Check out [Get set up](get-set-up.md) to download the tools you need to start creating apps, and then [write your first app](your-first-app.md).

### Design your app

The Microsoft design system is named Fluent. The Fluent Design System is a set of UWP features combined with best practices for creating apps that perform beautifully on all types of Windows-powered devices. Fluent experiences adapt and feel natural on devices from tablets to laptops, from PCs to televisions, and on virtual reality devices. See [The Fluent Design System for UWP apps](https://docs.microsoft.com/windows/uwp/design/fluent-design-system) for an introduction to Fluent Design.

Good [design](http://go.microsoft.com/fwlink/?LinkId=258848) is the process of deciding how users will interact with your app, in addition to how it will look and function. User experience plays a huge part in determining how happy people will be with your app, so don't skimp on this step. [Design basics](https://dev.windows.com/design) introduces you to designing a Universal Windows app. See the [Introduction to Universal Windows Platform (UWP) apps for designers](https://msdn.microsoft.com/library/windows/apps/dn958439) for information on designing UWP apps that delight your users. Before you start coding, see the [device primer](../design/devices/index.md) to help you think through the interaction experience of using your app on all the different form factors you want to target.

In addition to interaction on different devices, [plan your app](https://msdn.microsoft.com/library/windows/apps/hh465427) to embrace the benefits of working across multiple devices. For example:

- Design your workflow using [Navigation design basics for UWP apps](https://msdn.microsoft.com/library/windows/apps/dn958438) to accommodate mobile, small-screen, and large-screen devices. [Lay out your user interface](https://msdn.microsoft.com/library/windows/apps/dn958435) to respond to different screen sizes and resolutions.

- Consider how you'll accommodate multiple kinds of input. See the [Guidelines for interactions](https://msdn.microsoft.com/library/windows/apps/dn611861) to learn how users can interact with your app by using [Cortana](https://msdn.microsoft.com/library/windows/apps/dn974233), [Speech](https://msdn.microsoft.com/library/windows/apps/dn596121), [Touch interactions](https://msdn.microsoft.com/library/windows/apps/hh465370), the [Touch keyboard](https://msdn.microsoft.com/library/windows/apps/hh972345) and more.  Or, see the [Guidelines for text and text input](https://msdn.microsoft.com/library/windows/apps/dn611864) for more traditional interaction experiences.

### Add services

- Use [cloud services](http://go.microsoft.com/fwlink/?LinkId=526377) to sync across devices.
- Learn how to [connect to web services](https://msdn.microsoft.com/library/windows/apps/xaml/hh761504) to support your app experience.
- Learn how to [Add Cortana to your app](https://mva.microsoft.com/en-us/training-courses/integrating-cortana-in-your-apps-8487?l=20D3s5Xz_5904984382) so that your app can respond to voice commands.
- Include [notifications](https://msdn.microsoft.com/library/windows/apps/mt187203) and [in-app purchases](https://msdn.microsoft.com/library/windows/apps/mt219684) in your planning. These features should work across devices.

### Submit your app to the store

The new unified Windows Dev Center dashboard lets you manage and submit all of your apps for Windows devices in one place. See [Using the unified Windows Dev Center dashboard](../publish/using-the-windows-dev-center-dashboard.md) to learn how to submit your apps for publication in the Microsoft Store.

New features simplify processes while giving you more control. You'll also find detailed [analytic reports](https://msdn.microsoft.com/library/windows/apps/mt148522) combined [payout details](https://msdn.microsoft.com/library/windows/apps/dn986925), ways to [promote your app and engage with your customers](https://msdn.microsoft.com/library/windows/apps/mt148526), and much more.

For more introductory material, see [Windows 10 - An Introduction to Building Windows Apps for Windows 10 Devices](https://msdn.microsoft.com/magazine/dn973012.aspx)

### More advanced topics

- Learn how to add modern experiences for Windows 10 users to your existing desktop app and distribute it in the Microsoft Store with the [Desktop Bridge](https://developer.microsoft.com/en-us/windows/bridges/desktop).
- For the full list of Win32 APIs available to UWP apps, see [API Sets for UWP apps](https://msdn.microsoft.com/library/windows/desktop/mt186421) and [Dlls for UWP apps](https://msdn.microsoft.com/library/windows/desktop/mt186422).
- For a list of .NET types that you can use in a UWP app, see [.NET for UWP apps](https://msdn.microsoft.com/library/mt185501.aspx)
- See [Universal Windows apps in .NET](https://blogs.msdn.microsoft.com/dotnet/2015/07/30/universal-windows-apps-in-net) for an overview of writing .NET UWP apps.
- [.NET Native - What it means for Universal Windows Platform (UWP) developers](https://blogs.windows.com/buildingapps/2015/08/20/net-native-what-it-means-for-universal-windows-platform-uwp-developers/#TYsD3tJuBJpK3Hc7.97)