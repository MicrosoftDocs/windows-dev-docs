---
title: What's New for Developers in Windows 11
description: Learn what's new for developers in Windows 11 and Build 22000 of the Windows SDK
keywords: what's new, Windows 11, Windows, developers, build 22000, version 2110, sdk
ms.date: 10/4/2021
ms.topic: article
ms.localizationpriority: medium
---

# What's New for developers in Windows 11

Windows 11 has now released to developers, and is joined by Build 22000 of the Windows SDK. (also known as SDK version 2110). [Install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank) on Windows 11 and you’re ready to create a new Windows app. Learn about the [Windows UI Library](https://docs.microsoft.com/windows/apps/winui/winui2/release-notes/winui-2.7) or the [Windows App SDK](https://docs.microsoft.com/windows/apps/windows-app-sdk/) to enhance your app with more specialized Windows features.

This is a collection of new and improved features and guidance of interest to Windows developers in this release. Keep checking back for more information on how to use the latest Windows 11 features, in conjuction with the Windows App SDK and the Windows UI Library, to create and upgrade apps with the latest Windows capabilities.

## Windows App SDK

Feature | Description
:------ | :------
Windows Apps SDK | [The Windows App SDK](https://docs.microsoft.com/windows/apps/windows-app-sdk/) is a set of new developer components and tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 (and downlevel to Windows 10, version 1809).
Create a new app with the Windows App SDK | The Windows App SDK includes WinUI 3 project templates that enable you to create desktop and UWP apps with an entirely WinUI-based user interface. [When you create apps using these project templates](https://docs.microsoft.com/windows/apps/winui/winui3/create-your-first-winui3-app), the entire user interface of your application is implemented using windows, controls, and other UI types provided by WinUI 3.
Use the Windows App SDK in an existing project | If you have an existing project in which you want to use the Windows App SDK, [you can install the latest version of the Windows App SDK NuGet package in your project](https://docs.microsoft.com/windows/apps/windows-app-sdk/use-windows-app-sdk-in-existing-project). Unpackaged apps (that is, apps that do not use MSIX for their deployment technology) must follow this procedure to use the Windows App SDK, but packaged apps can do this too.
Download the Windows App SDK | There are several packages and release channels for the Windows App SDK. The [Download the Windows App SDK page](https://docs.microsoft.com/windows/apps/windows-app-sdk/downloads) provides guidance on which ones you need, download links, and installation instructions.

## Windows UI Library (WinUI)

Feature | Description
:------ | :------
WinUI 2.7 | [WinUI 2.7](https://docs.microsoft.com/windows/apps/winui/winui2/release-notes/winui-2.7) is the latest stable release of the Windows UI Library for UWP applications, and desktop applications using XAML islands. Highlighted features in WinUI 2.7 include the new InfoBadge control and additions made to the ColorPicker control.
InfoBadge control | An [InfoBadge](https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.controls.infobadge) is a small piece of UI that can be added into an app and customized to display a number, icon, or a simple dot. InfoBadge is built into [NavigationView](https://docs.microsoft.com/windows/apps/design/controls/navigationview) but can also be placed as a standalone element in the XAML tree, allowing you to place InfoBadge into any control or piece of UI of your choosing. </br> If you have the [XAML Controls Gallery app](https://www.microsoft.com/store/productId/9MSVH128X2ZT) installed, [click here to open the app and see the InfoBadge in action](xamlcontrolsgallery:/item/InfoBadge).
ColorPicker control | [The new orientation property of the ColorPicker control](https://docs.microsoft.com/windows/apps/design/controls/color-picker?#specify-the-layout-direction) allows you to control where the editing controls display relative to the color spectrum.
WinUI 3.0 Preview | [Version 1.0 Preview of WinUI 3.0 is now available](https://docs.microsoft.com/windows/apps/windows-app-sdk/preview-channel#version-10-preview-1-100-preview1). This release contains bug fixes to WinUI 3.0 features, building towards the stable release.

## Windows features

Feature | Description
:------ | :------
Bluetooth LE | New ConnectionParameters APIs have been added to the Bluetooth LE namespace. See the [BluetoothLEConnectionParameters class](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothleconnectionparameters), [BluetoothLEPreferredConnectionParameters](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothlepreferredconnectionparameters), and [BluetoothLEPreferredConnectionParametersRequest](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothlepreferredconnectionparametersrequest) for detailed information.
Call-control | [The PhoneCall class](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.phonecall) lets you programmatically control active or incoming phone calls.
Compositor clock | [The compositor clock API](https://docs.microsoft.com/windows/win32/directcomp/compositor-clock/compositor-clock) offers statistics and frame rate control for presenting on-screen content smoothly, at the fastest possible cadence, and on a variety of hardware configurations. 
Composition swapchain | [The composition swapchain API](https://docs.microsoft.com/windows/win32/comp_swapchain/comp-swapchain-portal) allows applications using composition APIs to host content that can be independently rendered and presented to.
DirectDisplay | New APIs have been added to the DirectDisplay namespaces. See [DisplayDevice.CreateSimpleScanoutWithDirtyRectsAndOptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaydevice.createsimplescanoutwithdirtyrectsandoptions) and [DisplayTaskPool.TryExecuteTask](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytaskpool.tryexecutetask) for detailed information.
DNS application settings | [The DnsGetApplicationSettings function](https://docs.microsoft.com/windows/win32/api/windns/nf-windns-dnsgetapplicationsettings) retrueves application-specific settings for a DNS server.
DNS custom server | New [DNS_CUSTOM_SERVER structure](https://docs.microsoft.com/windows/win32/api/windns/ns-windns-dns_custom_server) and [ADDRINFO_DNS_SERVER structure](https://docs.microsoft.com/windows/win32/api/ws2def/ns-ws2def-addrinfo_dns_server) allow you to configure a custom DNS server.
Firewall dynamic keywords | [Firewall dynamic keywords](https://docs.microsoft.com/windows/win32/ics/firewall-dynamic-keywords) allow you to manage dynamic keyword addresses in Microsoft Defender Firewall. A dynamic keyword address is used to create a set of IP addresses to which one or more firewall rules can refer. Dynamic keyword addresses support both IPv4 and IPv6.
On-Air | [The ShareWindowCommandSource class](https://docs.microsoft.com/uwp/api/windows.ui.shell.sharewindowcommandsource) provides a framework to communicate with the Windows shell to present a UI that controls window sharing.
Pen haptics | [Pen haptic feedback](https://docs.microsoft.com/windows/apps/design/input/pen-haptics), introduced in Windows 11, allows users to feel their pen interacting in a tactile manner with the user interface of an app. [The KnownSimpleHapticsControllerWaveforms class](https://docs.microsoft.com/uwp/api/windows.devices.haptics.knownsimplehapticscontrollerwaveforms) allows you to configure this experience for your app's users.
WinHttp connection groups | [The WinHttpQueryConnectionGroup function](https://docs.microsoft.com/windows/win32/api/winhttp/nf-winhttp-winhttpqueryconnectiongroup) allows you to pull the current state of WinHttp's connections.
VPN foreground activation | New APIs have been added to VPN foreground activation, which is often used to let a user input VPN credentials. See the [VpnForegroundActivationOperation class](https://docs.microsoft.com/uwp/api/windows.networking.vpn.vpnforegroundactivatedeventargs) and [VpnForegroundActivatedEventArgs](https://docs.microsoft.com/uwp/api/windows.networking.vpn.vpnforegroundactivatedeventargs) for more information.


## Samples

[The Pen Haptics sample](https://github.com/microsoft/Windows-universal-samples/tree/dev/Samples/PenHaptics) shows how to use Windows 11 pen haptics API to trigger haptic feedback on a pen that supports haptics. It shows how to:

* Get SimpleHapticsController from pen input: this sample shows how to go from pointer ID to PenDevice and then to SimpleHapticsController. This requires haptics support from both the pen and a compliant machine that supports the particular pen.
* Check pen haptics capabilities: SimpleHapticsController has properties for pen hardware capabilities, such as IsIntensitySupported, IsPlayCountSupported, SupportedFeedback, etc.
* Start and stop haptic feedback: start and stop feedback using variations of SendHapticFeedback and StopFeedback API
* Trigger both inking and interaction haptic feedback: the code shows how to trigger inking feedback for inking scenarios and interaction feedback for user interactions

[The XAML Controls Gallery](https://docs.microsoft.com/samples/microsoft/xaml-controls-gallery/xaml-controls-gallery/) has been updated to showcase the latest additions to the Windows UI Library in WinUI 2.7.