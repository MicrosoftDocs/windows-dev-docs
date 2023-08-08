---
description: Discover the latest additions to the Windows developer docs.
title: Latest updates to the Windows API and developer documentation
ms.topic: article
ms.date: 6/16/2023
ms.localizationpriority: medium
ms.author: quradic
author: QuinnRadich
---

# Latest updates to the Windows developer docs

The Windows developer docs are regularly updated with new and improved information and content. Here is a summary of changes as of June 16th, 2023.

Note: For information regarding Windows 11, please see [What's cool for developers](https://developer.microsoft.com/windows/windows-for-developers/) and the [Windows Developer Center](https://developer.microsoft.com/windows/).

For the latest Windows Developer Documentation news, or to reach out to us with comments and questions, feel free to find us on Twitter, where our handle is [@WindowsDocs](https://twitter.com/windowsdocs).

Don't forget to visit the [Windows Developer Center](https://developer.microsoft.com/windows/), where we highlight some of the latest technologies, frameworks and news for Windows developers.

_Many thanks to everyone who has contributed to the documentation. Your corrections and suggestions are very welcome! For information on contributing, please see our [contributor guide](/contribute/)._

Highlights this month include:

## Microsoft Build announcements for Windows developers

* [Dev Home](/windows/dev-home/): Monitor your work in the centralized dashboard, GitHub and System performance widgets. Get setup and onboard new projects with the Machine configuration tool. Learn about Dev Home’s:  
  * [Dashboard widgets](/windows/dev-home/), including the ability to set up customized GitHub feeds,
  * [Machine configuration tool](/windows/dev-home/setup) for setting up a new machine or onboard a new project, and  
  * [Customizable extensions](/windows/dev-home/extensions), with the ability to set up GitHub notifications or build and share your own Open Source extension for Dev Home.

* [Dev Drive](/windows/dev-drive/): Improve your dev workload performance using this new storage format, using ReFS and specifically designed for developer scenarios, with the ability to designate trust, manage antivirus configurations, and attach security filters.
  * Microsoft Defender has a new [performance mode](/microsoft-365/security/defender-endpoint/microsoft-defender-endpoint-antivirus-performance-mode?view=o365-worldwide) specifically designed for Dev Drive.

* [WinGet Configuration](/windows/package-manager/configuration/): Consolidate manual machine setup and project onboarding to a single command that is reliable and repeatable. Learn how to:
  * [Author a WinGet Configuration file](/windows/package-manager/configuration/create),
  * [Check the trustworthiness of a WinGet Configuration file](/windows/package-manager/configuration/check),
  * [Use the winget configure command](/windows/package-manager/winget/configure) to begin setting up your machine to the desired configuration state.

* [PowerToys](/windows/powertoys/): These open source utilities are suggested by and developed with the help of the developer community using Windows. New utilities in the .70 release include:
  * [Mouse without Borders](/windows/powertoys/mouse-without-borders), for seamless transition between multiple machines from the same keyboard and mouse, sharing clipboard contents and files,  
  * [Peek](/windows/powertoys/peek), for previewing file content without the need to open multiple apps or interrupt your workflow,
  * [Paste as Plain Text](/windows/powertoys/paste-as-plain-text), for pasting text from your clipboard, excluding any text-formatting.

* [Windows Terminal](/windows/terminal/): Customize your terminal and running multiple command lines, all set up to their specific preferences. Learn more about how to customize your prompt and the new Tab Tearout feature.

* [Windows Copilot](/windows/dev-environment/): The first PC platform to provide centralized AI assistance and designed to help people easily take action and get things done is coming soon!

* [Windows on Arm](/windows/arm/overview): We’ve got a new tutorial that can help you update your apps to run natively on Arm.

<hr>

The following list of topics have also seen significant updates in the past month:

## Windows App SDK / WinUI

* [WinAppSDK 1.4 Experimental Release Notes](/windows/apps/windows-app-sdk/experimental-channel)

## Code samples, tutorials and Learn Module updates

* Added [examples page for a new Win32 bindlink.h header](/windows/win32/bindlink/bindlink-example).
* Added [examples page for a new Win32 hwreqchkapi.h header](/windows/win32/hwreqchkapi/hwreqchk-examples), including four code examples.
* Rust: Fixed the code sample in [RSS reader tutorial](/windows/dev-environment/rust/rss-reader-rust-for-windows).
* C++/WinRT: Updated [A completion source sample](/windows/uwp/cpp-and-winrt-apis/concurrency-3).

## Updated content

* [WindowsTabManager Class](/uwp/api/windows.ui.shell.windowtabmanager?view=winrt-22621)
* [NavigationView](/windows/apps/design/controls/navigationview)
* [Apply Mica or Acrylic materials](/windows/apps/windows-app-sdk/system-backdrop-controller)
* [Use the Windows App SDK in a WPF app](/windows/apps/windows-app-sdk/wpf-plus-winappsdk)
* [Win2D](/windows/apps/develop/win2d/) content migrated from GitHub

## Developer tool updates

* [Windows Subsystem for Linux, Enterprise and Security Control Options](/windows/wsl/enterprise).
* [Windows Subsystem for Android updates](/windows/android/wsa/).
* [Windows Package Manager updates](/windows/package-manager/).
* [PowerToys](/windows/powertoys/install).

## Win32 Conceptual

* [LVM_ENABLEGROUPVIEW message (Commctrl.h)](/windows/desktop/Controls/lvm-enablegroupview)
* [LVM_GETGROUPINFO message (Commctrl.h)](/windows/desktop/Controls/lvm-getgroupinfo)
* [LVM_HASGROUP message (Commctrl.h)](/windows/desktop/Controls/lvm-hasgroup)
* [LVM_HITTEST message (Commctrl.h)](/windows/desktop/Controls/lvm-hittest)
* [LVM_INSERTGROUP message (Commctrl.h)](/windows/desktop/Controls/lvm-insertgroup)
* [LVM_REMOVEALLGROUPS message (Commctrl.h)](/windows/desktop/Controls/lvm-removeallgroups)
* [LVM_SETGROUPINFO message (Commctrl.h)](/windows/desktop/Controls/lvm-setgroupinfo)
* [LVM_SUBITEMHITTEST message (Commctrl.h)](/windows/desktop/Controls/lvm-subitemhittest)
* [PE Format](/windows/desktop/Debug/pe-format)
* [TelIsOsInProcessorMode function](/windows/desktop/DevNotes/tellsIsosinprocessormode)
* [Scale effect](/windows/desktop/Direct2D/high-quality-scale)
* [File Attribute Constants (WinNT.h)](/windows/desktop/FileIO/file-attribute-constants)
* [Setting the Cursor Image](/windows/desktop/LearnWin32/setting-the-cursor-image)
* [Working Set](/windows/desktop/Memory/working-set)
* [Playing a MIDI File](/windows/desktop/Multimedia/playing-a-midi-file)
* [Bootstrap profile sample](/windows/desktop/NativeWiFi/bootstrap-profile-sample)
* [FIPS profile sample](/windows/desktop/NativeWiFi/fips-profile-sample)
* [OneX Schema](/windows/desktop/NativeWiFi/onexschema-schema)
* [Network Policy Server](/windows/desktop/Nps/portal)
* [Application manifests](/windows/desktop/SbsCs/application-manifests)
* [Assembly Manifests](/windows/desktop/SbsCs/assembly-manifests)
* [Cipher Suites in TLS/SSL (Schannel SSP)](/windows/desktop/SecAuthN/cipher-suites-in-schannel)
* [CNG Algorithm Identifiers (Bcrypt.h)](/windows/desktop/SecCNG/cng-algorithm-identifiers)
* [CNG DPAPI](/windows/desktop/SecCNG/cng-dpapi)
* [Child Sessions](/windows/desktop/TermServ/child-sessions)
* [pragma classflags](/windows/desktop/WmiSdk/pragma-classflags)
* [Capturing WinHTTP Logs](/windows/desktop/WsdApi/capturing-winhttp-logs)
* [API set loader operation](/windows/desktop/apiindex/api-set-loader-operation)
* [Windows API index](/windows/desktop/apiindex/windows-api-list)
* [Windows API sets](/windows/desktop/apiindex/windows-apisets)
* [Windows umbrella libraries](/windows/desktop/apiindex/windows-umbrella-libraries)
* [Bind link API enums](/windows/desktop/bindlink/bindlink-api-enums)
* [Bind link API functions](/windows/desktop/bindlink/bindlink-api-functions)
* [Bind link API examples](/windows/desktop/bindlink/bindlink-example)
* [Overview of the Bindlink API](/windows/desktop/bindlink/bindlink-overview)
* [Bindlink API (bindlink.h)](/windows/desktop/bindlink/index)
* [DirectComposition interfaces](/windows/desktop/directcomp/interfaces)
* [About the EAP and EAPHost relationship](/windows/desktop/eap/about-eaphost)
* [Configuration User Interface](/windows/desktop/eap/configuration-user-interface)
* [EAP Enumerations](/windows/desktop/eap/eap-enumerations)
* [EAP Frequently Asked Questions](/windows/desktop/eap/eap-frequently-asked-questions)
* [EAP Functions](/windows/desktop/eap/eap-functions)
* [EAP Implementation Details](/windows/desktop/eap/eap-implementation-details)
* [EAP Installation](/windows/desktop/eap/eap-installation)
* [EAP Interfaces](/windows/desktop/eap/eap-interfaces)
* [Extensible Authentication Protocol](/windows/desktop/eap/eap-start-page)
* [EAP Structures](/windows/desktop/eap/eap-structures)
* [Interactive User Interface](/windows/desktop/eap/interactive-user-interface)
* [Obtaining Identity Information](/windows/desktop/eap/obtaining-identity-information)
* [Access Point Initialization of EAP](/windows/desktop/eap/ras-initialization-of-eap)
* [Registry Values Example](/windows/desktop/eap/registry-values-example)
* [User Authentication](/windows/desktop/eap/user-authentication)
* [Extensible Authentication Protocol Host](/windows/desktop/eaphost/portal)
* [HWREQCHK API enums](/windows/desktop/hwreqchkapi/hwreqchk-api-enums)
* [HWREQCHK API functions](/windows/desktop/hwreqchkapi/hwreqchk-api-functions)
* [HWREQCHK API structures](/windows/desktop/hwreqchkapi/hwreqchk-api-structures)
* [HWREQCHK API examples](/windows/desktop/hwreqchkapi/hwreqchk-examples)
* [Overview of the HWREQCHK API](/windows/desktop/hwreqchkapi/hwreqchk-overview)
* [HWREQCHK API](/windows/desktop/hwreqchkapi/index)
* [Image Stride](/windows/desktop/medfound/image-stride)
* [About Cursors](/windows/desktop/menurc/about-cursors)
* [About Icons](/windows/desktop/menurc/about-icons)
* [STRINGTABLE resource](/windows/desktop/menurc/stringtable-resource)

## Win32 API reference

* [Data Access and Storage](/windows/win32/api/_fs/index)
* [Human Interface Devices Reference](/windows/win32/api/_hid/index)
* [Hardware Requirement Evaluator (HWREQCHK)](/windows/win32/api/_hwreqchk/index)
* [bindlink (bindlink.h)](/windows/win32/api/bindlink/index)
* [hwreqchkapi (hwreqchkapi.h)](/windows/win32/api/hwreqchkapi/index)
* [CopyFile function (winbase.h)](/windows/win32/api/winbase/nf-winbase-copyfile)
* [MoveFile function (winbase.h)](/windows/win32/api/winbase/nf-winbase-movefile)
* [ToAscii function (winuser.h)](/windows/win32/api/winuser/nf-winuser-toascii)

## UWP reference

* [Windows.Devices.Input.KeyboardCapabilities](/uwp/api/windows.devices.input.keyboardcapabilities)
* [Windows.Devices.Lights.Lamp.AvailabilityChanged](/uwp/api/windows.devices.lights.lamp.availabilitychanged)
* [Windows.Devices.Lights.LampArray](/uwp/api/windows.devices.lights.lamparray)
* [Windows.Devices.Lights.LampArray.BoundingBox](/uwp/api/windows.devices.lights.lamparray.boundingbox)
* [Windows.Devices.Lights.LampArray.DeviceId](/uwp/api/windows.devices.lights.lamparray.deviceid)
* [Windows.Devices.Lights.LampArray.IsAvailable](/uwp/api/windows.devices.lights.lamparray.isavailable)
* [Windows.Devices.Lights.LampArray.IsConnected](/uwp/api/windows.devices.lights.lamparray.isconnected)
* [Windows.Devices.Lights.LampArray.IsEnabled](/uwp/api/windows.devices.lights.lamparray.isenabled)
* [Windows.Devices.Lights.LampArray.LampArrayKind](/uwp/api/windows.devices.lights.lamparray.lamparraykind)
* [Windows.Devices.Lights.LampArray.LampCount](/uwp/api/windows.devices.lights.lamparray.lampcount)
* [Windows.Devices.Lights.LampArrayKind](/uwp/api/windows.devices.lights.lamparraykind)
* [N:Windows.Devices.Lights](/uwp/api/windows.devices.lights.windows.devices.lights)
* [Windows.Devices.Sensors.ActivitySensor](/uwp/api/windows.devices.sensors.activitysensor)
* [Windows.Devices.Sensors.AdaptiveDimmingOptions](/uwp/api/windows.devices.sensors.adaptivedimmingoptions)
* [Windows.Devices.Sensors.LockOnLeaveOptions](/uwp/api/windows.devices.sensors.lockonleaveoptions)
* [Windows.Devices.Sensors.WakeOnApproachOptions](/uwp/api/windows.devices.sensors.wakeonapproachoptions)
* [Windows.UI.Composition.CompositionTexture](/uwp/api/windows.ui.composition.compositiontexture)
* [Windows.UI.Shell.WindowTab](/uwp/api/windows.ui.shell.windowtab)
* [Windows.UI.Shell.WindowTab.Group](/uwp/api/windows.ui.shell.windowtab.group)
* [Windows.UI.Shell.WindowTab.Icon](/uwp/api/windows.ui.shell.windowtab.icon)
* [Windows.UI.Shell.WindowTab.Tag](/uwp/api/windows.ui.shell.windowtab.tag)
* [Windows.UI.Shell.WindowTab.Title](/uwp/api/windows.ui.shell.windowtab.title)
* [Windows.UI.Shell.WindowTab.#ctor](/uwp/api/windows.ui.shell.windowtab.windowtab)
* [Windows.UI.Shell.WindowTabCollection](/uwp/api/windows.ui.shell.windowtabcollection)
* [Windows.UI.Shell.WindowTabCollection.Size](/uwp/api/windows.ui.shell.windowtabcollection.size)
* [Windows.UI.Shell.WindowTabGroup](/uwp/api/windows.ui.shell.windowtabgroup)
* [Windows.UI.Shell.WindowTabGroup.Icon](/uwp/api/windows.ui.shell.windowtabgroup.icon)
* [Windows.UI.Shell.WindowTabGroup.Title](/uwp/api/windows.ui.shell.windowtabgroup.title)
* [Windows.UI.Shell.WindowTabIcon](/uwp/api/windows.ui.shell.windowtabicon)
* [Windows.UI.Shell.WindowTabManager](/uwp/api/windows.ui.shell.windowtabmanager)
* [Windows.UI.Shell.WindowTabManager.Tabs](/uwp/api/windows.ui.shell.windowtabmanager.tabs)
