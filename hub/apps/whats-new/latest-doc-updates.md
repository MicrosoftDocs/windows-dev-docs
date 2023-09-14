---
description: Discover the latest additions to the Windows developer docs.
title: Latest updates to the Windows API and developer documentation
ms.topic: article
ms.date: 9/14/2023
ms.localizationpriority: medium
ms.author: quradic
author: QuinnRadich
---

# Latest updates to the Windows developer docs

The Windows developer docs are regularly updated with new and improved information and content. Here is a summary of changes as of September 14th, 2023.

For the latest Windows Developer Documentation news, or to reach out to us with comments and questions, feel free to find us on Twitter, where our handle is [@WindowsDocs](https://twitter.com/windowsdocs).

Don't forget to visit the [Windows Developer Center](https://developer.microsoft.com/windows/), where we highlight some of the latest technologies, frameworks and news for Windows developers.

Many thanks to everyone who has contributed to the documentation. Your corrections and suggestions are very welcome! For information on contributing, please see our new [contributor hub](/contribute/).

Our most popular topic this month was: [Install WSL](/windows/wsl/install).

Highlights this month include:

* [Windows on ARM FAQ](/windows/arm/faq)
* [Windows App SDK 1.4 Stable Release Notes](/windows/apps/windows-app-sdk/stable-channel)
* [WinGet v1.5 including Windows Package Manager](/windows/package-manager/)
* [PowerToys 0.73](/windows/powertoys/)
* [Dev Home](/windows/dev-home/)
* [Dev Drive](/windows/dev-drive/)

<hr>

The following list of topics have also seen significant updates in the past month:

## Windows App SDK / WinUI3

* Documented new APIs in [Microsoft.Windows.ApplicationModel.WindowsAppRuntime](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime).
* Documented new APIs in [Microsoft.UI.Dispatching](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching); also create a new conceptual topic explaining [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue?view=windows-app-sdk-1.4), together with several code examples.
* Improved the topic [Which kinds of apps do app capabilities apply to?](/windows/uwp/packaging/app-capability-declarations#which-kinds-of-apps-do-app-capabilities-apply-to).
* Added info to push notifications APIs (for example, [PushNotificationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager)) about their dependency on the Singleton package.
* Created API documentation for [ScrollPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollview.scrollpresenter?view=windows-app-sdk-1.4) & [IScrollController](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.iscrollcontroller?view=windows-app-sdk-1.4).
* Created API documentation for [ScrollView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.scrollview?view=windows-app-sdk-1.4).
* Created API documentation for [ItemContainer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.choosingitemcontainereventargs.itemcontainer?view=windows-app-sdk-1.4).
* Created API documentation for [Layout](/windows/winui/api/microsoft.ui.xaml.controls.layout?view=winui-2.8) & [VirtualizingLayoutContext](/windows/winui/api/microsoft.ui.xaml.controls.virtualizinglayoutcontext?view=winui-2.8).
* Created API documentation for [TreeView.SelectionChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.treeview.selectionchanged?view=windows-app-sdk-1.4).
* Created new content for Windows App SDK 1.4 release, including: [ThemeSettings](/windows/windows-app-sdk/api/winrt/microsoft.ui.system.themesettings?view=windows-app-sdk-1.4), [AppInstance.Restart](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.restart?view=windows-app-sdk-1.4), [SecurityDescriptorHeaders](/windows/windows-app-sdk/api/winrt/microsoft.windows.security.accesscontrol.securitydescriptorhelpers.getsddlforappcontainernames?view=windows-app-sdk-1.4), [AppContainerNameAndAccess](/windows/windows-app-sdk/api/winrt/microsoft.windows.security.accesscontrol.appcontainernameandaccess?view=windows-app-sdk-1.4), and [PowerManager.EffectivePowerMode2](/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager.effectivepowermode2?view=windows-app-sdk-1.4).


## Updated content

* Documented the PDL pass-through feature in [Windows.Devices.Printing](/uwp/api/windows.devices.printers).
* Documented new APIs in [Windows.Networking.NetworkOperators](/uwp/api/windows.networking.networkoperators).
* Deprecated all [Windows.Phone.Networking.Voip](/uwp/api/windows.phone.networking.voip) APIs, and linked to new types.
* Deprecated PnP APIs in [Windows.Devices.Enumeration](/uwp/api/windows.devices.enumeration).
* Published 112 new topics for the [Windows.Management.Update](/uwp/api/windows.management.update) namespace.
* Updated [Rust for Windows](/windows/dev-environment/rust/rust-for-windows), and the windows crate now that Xaml support is no longer in the crate.
* Documented a [new registry key for parental control](/windows/win32/parcon/using-parental-controls-reg-key).
* Created API reference documentation for [IAudioProcessingObjectPreferredFormatSupport - Win32 apps | Microsoft Learn](/windows/win32/api/audioengineextensionapo/nn-audioengineextensionapo-iaudioprocessingobjectpreferredformatsupport) and related APIs.
* Added new APIs to [Windows.Media.MediaProperties Namespace - Windows UWP applications | Microsoft Learn](/windows.media.mediaproperties).
* Updated [PROCESS_MITIGATION_SYSTEM_CALL_DISABLE_POLICY](/windows/win32/api/winnt/ns-winnt-process_mitigation_system_call_disable_policy) (winnt.h).