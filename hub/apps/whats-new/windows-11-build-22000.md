---
title: What's New for Developers in Windows 11
description: Learn what's new for developers in Windows 11 and Build 22000 of the Windows SDK
keywords: what's new, Windows 11, Windows, developers, build 22000, version 2110, sdk
ms.date: 12/7/2021
ms.topic: article
ms.localizationpriority: medium
---

# What's New for developers in Windows 11

Windows 11 is now widely available, and is joined by Build 22000 of the Windows SDK (also known as SDK version 2110).

It is an exciting time for Windows developers, as new tools and frameworks are in active development. They're all designed to bring support for the latest Windows features to the widest possible audience. For example, Win32 developers will appreciate the new Windows App SDK model, and fans of modern user interface design will enjoy working with WinUI. Developers who love C++, can use WinRT/C++ to create apps in familiar ways.

To get started, [install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank), and then learn about the [Windows UI Library](../winui/index.md) and the [Windows App SDK](../windows-app-sdk/index.md).


## Windows App SDK

Feature | Description
:------ | :------
Windows App SDK | [The Windows App SDK](../windows-app-sdk/index.md) is a set of new developer components and tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 (and downlevel to Windows 10, version 1809).
Windows App SDK release notes | Details on [the latest stable release of the Windows App SDK](../windows-app-sdk/stable-channel.md), which can be used by apps in production environments and by apps published to the Microsoft Store.
Create a new app with the Windows App SDK | The Windows App SDK includes WinUI 3 project templates that enable you to create apps with an entirely WinUI-based user interface. When you create a project using these templates (see [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)), the entire user interface of your application is implemented using windows, controls, and other UI types provided by WinUI 3.
Use the Windows App SDK in an existing project | If you have an existing project in which you want to use the Windows App SDK, [you can install the latest version of the Windows App SDK NuGet package in your project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md). Unpackaged apps must follow this procedure to use the Windows App SDK, but packaged apps can do this too.
Download the Windows App SDK | There are several packages and release channels for the Windows App SDK. The [Download the Windows App SDK page](../windows-app-sdk/downloads.md) provides guidance on which ones you need, download links, and installation instructions.

## Windows UI Library (WinUI)

Feature | Description
:------ | :------
WinUI | [WinUI 2.8](../winui/winui2/index.md) and [WinUI 3](../winui/winui3/index.md) are versions of the new Windows UI Library. Which version you use depends on the tools you are using: for example, WinUI 2.8 supports UWP apps. 
InfoBadge control | An [InfoBadge](/windows/winui/api/microsoft.ui.xaml.controls.infobadge) is a small piece of UI that can be added into an app and customized to display a number, icon, or a simple dot. InfoBadge is built into [NavigationView](../design/controls/navigationview.md) but can also be placed as a standalone element in the XAML tree, allowing you to place InfoBadge into any control or piece of UI of your choosing. </br> If you have the [WinUI 2 Gallery app](https://www.microsoft.com/store/productId/9MSVH128X2ZT) installed, [click here to open the app and see the InfoBadge in action](winui2gallery:/item/InfoBadge).
ColorPicker control | [The new orientation property of the ColorPicker control](../design/controls/color-picker.md#specify-the-layout-direction) allows you to control where the editing controls display relative to the color spectrum.


## Windows features

Feature | Description
:------ | :------
Bluetooth LE | New ConnectionParameters APIs have been added to the Bluetooth LE namespace. See the [BluetoothLEConnectionParameters class](/uwp/api/windows.devices.bluetooth.bluetoothleconnectionparameters), [BluetoothLEPreferredConnectionParameters](/uwp/api/windows.devices.bluetooth.bluetoothlepreferredconnectionparameters), and [BluetoothLEPreferredConnectionParametersRequest](/uwp/api/windows.devices.bluetooth.bluetoothlepreferredconnectionparametersrequest) for detailed information.
Call-control | [The PhoneCall class](/uwp/api/windows.applicationmodel.calls.phonecall) lets you programmatically control active or incoming phone calls.
Compositor clock | [The compositor clock API](/windows/win32/directcomp/compositor-clock/compositor-clock) offers statistics and frame rate control for presenting on-screen content smoothly, at the fastest possible cadence, and on a variety of hardware configurations. 
Composition swapchain | [The composition swapchain API](/windows/win32/comp_swapchain/comp-swapchain-portal) allows applications using composition APIs to host content that can be independently rendered and presented to.
DirectDisplay | New APIs have been added to the DirectDisplay namespaces. See [DisplayDevice.CreateSimpleScanoutWithDirtyRectsAndOptions](/uwp/api/windows.devices.display.core.displaydevice.createsimplescanoutwithdirtyrectsandoptions) and [DisplayTaskPool.TryExecuteTask](/uwp/api/windows.devices.display.core.displaytaskpool.tryexecutetask) for detailed information.
DNS application settings | [The DnsGetApplicationSettings function](/windows/win32/api/windns/nf-windns-dnsgetapplicationsettings) retrieves application-specific settings for a DNS server.
DNS custom server | New [DNS_CUSTOM_SERVER structure](/windows/win32/api/windns/ns-windns-dns_custom_server) and [ADDRINFO_DNS_SERVER structure](/windows/win32/api/ws2def/ns-ws2def-addrinfo_dns_server) allow you to configure a custom DNS server.
Firewall dynamic keywords | [Firewall dynamic keywords](/windows/win32/ics/firewall-dynamic-keywords) allow you to manage dynamic keyword addresses in Microsoft Defender Firewall. A dynamic keyword address is used to create a set of IP addresses to which one or more firewall rules can refer. Dynamic keyword addresses support both IPv4 and IPv6.
On-Air | [The ShareWindowCommandSource class](/uwp/api/windows.ui.shell.sharewindowcommandsource) provides a framework to communicate with the Windows shell to present a UI that controls window sharing.
Pen haptics | [Pen haptic feedback](../design/input/pen-haptics.md), introduced in Windows 11, allows users to feel their pen interacting in a tactile manner with the user interface of an app. [The KnownSimpleHapticsControllerWaveforms class](/uwp/api/windows.devices.haptics.knownsimplehapticscontrollerwaveforms) allows you to configure this experience for your app's users.
WinHttp connection groups | [The WinHttpQueryConnectionGroup function](/windows/win32/api/winhttp/nf-winhttp-winhttpqueryconnectiongroup) allows you to pull the current state of WinHttp's connections.
VPN foreground activation | New APIs have been added to VPN foreground activation, which is often used to let a user input VPN credentials. See the [VpnForegroundActivationOperation class](/uwp/api/windows.networking.vpn.vpnforegroundactivatedeventargs) and [VpnForegroundActivatedEventArgs](/uwp/api/windows.networking.vpn.vpnforegroundactivatedeventargs) for more information.

## Samples

The [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery) on GitHub is updated regularly to showcase the latest additions and improvements to WinUI in the Windows App SDK. The gallery app can also be downloaded from the [Microsoft Store](https://apps.microsoft.com/detail/9p3jfpwwdzrc).

The [Family Notes](https://github.com/Microsoft/Windows-appsample-familynotes) has been updated with a user interface created using WinUI.

[The Pen Haptics sample](https://github.com/microsoft/Windows-universal-samples/tree/dev/Samples/PenHaptics) shows how to use Windows 11 pen haptics API to trigger haptic feedback on a pen that supports haptics. It shows how to:

* Get SimpleHapticsController from pen input: this sample shows how to go from pointer ID to PenDevice and then to SimpleHapticsController. This requires haptics support from both the pen and a compliant machine that supports the particular pen.
* Check pen haptics capabilities: SimpleHapticsController has properties for pen hardware capabilities, such as IsIntensitySupported, IsPlayCountSupported, SupportedFeedback, etc.
* Start and stop haptic feedback: start and stop feedback using variations of SendHapticFeedback and StopFeedback API
* Trigger both inking and interaction haptic feedback: the code shows how to trigger inking feedback for inking scenarios and interaction feedback for user interactions
