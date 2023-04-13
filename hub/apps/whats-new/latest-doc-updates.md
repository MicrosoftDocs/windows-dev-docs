---
description: Discover the latest additions to the Windows developer docs.
title: Latest updates to the Windows API and developer documentation
ms.topic: article
ms.date: 4/13/2023
ms.localizationpriority: medium
ms.author: quradic
author: QuinnRadich
---

# Latest updates to the Windows developer docs

The Windows developer docs are regularly updated with new and improved information and content. Here is a summary of changes as of April 13th, 2023.

Note: For information regarding Windows 11, please see [What's cool for developers](https://developer.microsoft.com/windows/windows-for-developers/) and the [Windows Developer Center](https://developer.microsoft.com/windows/).

For the latest Windows Developer Documentation news, or to reach out to us with comments and questions, feel free to find us on Twitter, where our handle is [@WindowsDocs](https://twitter.com/windowsdocs).

*Many thanks to everyone who has contributed to the documentation. Your corrections and suggestions are very welcome! For information on contributing, please see our [contributor guide](/contribute/).*

Highlights this month include:

## Windows App SDK / WinUI

* [WinAppSDK 1.3-preview1 Release Notes](/windows/apps/windows-app-sdk/preview-channel)
* [Get Started updates to Windows VS page](/visualstudio/get-started/csharp/tutorial-wasdk?view=vs-2022)
* Three new topics for managing app resources in Windows App SDK (ported from UWP topics): [Tailor your resources](/windows/apps/windows-app-sdk/mrtcore/tailor-resources-lang-scale-contrast), [Localize strings](/windows/apps/windows-app-sdk/mrtcore/localize-strings), [Load images and assets](/windows/apps/windows-app-sdk/mrtcore/images-tailored-for-scale-theme-contrast).
* Updated Remarks and examples in [Frame, Page, and related classes](/windows/apps/winui/winui3/) to be accurate for WinUI3 navigation.
* Updated [HandwritingView](/uwp/api/windows.ui.xaml.controls.handwritingview?view=winrt-22621) for WinAppSDK.


## Code samples, tutorials and Learn Module updates

* Added [C++/WinRT code to the Create a simple photo viewer](/windows/apps/get-started/simple-photo-viewer-winui3) with WinUI 3 topic.
* New [Uno Platform tutorial](https://learn.microsoft.com/windows/apps/how-tos/uno-multiplatform), taking our WinUI 3 Hello World how-to and extending it cross-platform with Uno.
* Updated example code in [Binding with ADsOpenObject](/windows/win32/adsi/binding-with-adsopenobject-and-iadsopendsobject-opendsobject) topic.
* Updated example code in [FileSavePicker.FileTypeChoices](/uwp/api/windows.storage.pickers.filesavepicker.filetypechoices?view=winrt-22621).

## Updated content

* Updated the [FileSavePicker](/uwp/api/windows.storage.pickers.filesavepicker) class topic with how to use Win32 picker APIs from an elevated WinUI 3 app.
* Updated the [What's supported when migrating from UWP to WinUI 3](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported) topic for version 1.3.
* [DirectStorage](/gaming/gdk/_content/gc/system/overviews/directstorage/directstorage-drop-in-decompression)
* Updated and expanded docs for [49 cfapi.h APIs](/windows/win32/cfapi/cloud-filter-reference) based on latest specifications.
* Updated [NtCreateNamedPipeFile](/windows/win32/devnotes/nt-create-named-pipe-file) after receiving community feedback on social media.
* New APIs for SV2 core audio APO extensions, including [IAcousticEchoCancellationControl](/windows/win32/api/audioclient/nn-audioclient-iacousticechocancellationcontrol) and [IAudioProcessingObjectNotifications2](/windows/win32/api/audioengineextensionapo/nn-audioengineextensionapo-iaudioprocessingobjectnotifications2).


## Developer tool updates

* [Windows Subsystem for Linux, Enterprise and Security Control Options](/windows/wsl/enterprise).
* [Windows Subsystem for Android updates](/windows/android/wsa/).
* [Windows Package Manager updates](/windows/package-manager/).
* [PowerToys](/windows/powertoys/install).


<hr>

## Updated in the last month

The following list of topics have seen significant updates in the past month, as per GitHub logs:

## Win32 Conceptual

<ul>
<li><a href="/windows/desktop/CIMWin32Prov/win32-operatingsystem">Win32_OperatingSystem class</a></li>
<li><a href="/windows/desktop/Controls/themesfileformat-overview">Theme File Format</a></li>
<li><a href="/windows/desktop/CoreAudio/spatial-sound">Spatial Sound for app developers for Windows, Xbox, and Hololens 2</a></li>
<li><a href="/windows/desktop/Debug/pe-format">PE Format</a></li>
<li><a href="/windows/desktop/DevNotes/-win32-__chkstk">__chkstk Routine</a></li>
<li><a href="/windows/desktop/DevNotes/etwlogtraceevent">EtwLogTraceEvent</a></li>
<li><a href="/windows/desktop/DevNotes/nt-create-named-pipe-file">NtCreateNamedPipeFile function</a></li>
<li><a href="/windows/desktop/DevNotes/rtldllshutdowninprogress">RtlDllShutdownInProgress function</a></li>
<li><a href="/windows/desktop/DevNotes/wldpisdebugallowed">WldpIsDebugAllowed function</a></li>
<li><a href="/windows/desktop/ETW/tdhgetalleventsinformation">TdhGetAllEventsInformation function</a></li>
<li><a href="/windows/desktop/Intl/unicode-subset-bitfields">Unicode Subset Bitfields</a></li>
<li><a href="/windows/desktop/Intl/uniscribe-glossary">Uniscribe Glossary</a></li>
<li><a href="/windows/desktop/LearnWin32/creating-a-window">Create a window</a></li>
<li><a href="/windows/desktop/Msi/error-codes">MsiExec.exe and InstMsi.exe error messages (for developers)</a></li>
<li><a href="/windows/desktop/NetMgmt/odj-idl">Offline domain join IDL definitions</a></li>
<li><a href="/windows/desktop/NetMgmt/odj-op_join_prov4_part">OP_JOINPROV4_PART</a></li>
<li><a href="/windows/desktop/SbsCs/assembly-manifests">Assembly Manifests</a></li>
<li><a href="/windows/desktop/Sync/registerwaitforsingleobjectex">RegisterWaitForSingleObjectEx function</a></li>
<li><a href="/windows/desktop/TermServ/child-sessions">Child Sessions</a></li>
<li><a href="/windows/desktop/VSS/setting-vss-restore-methods">Setting VSS Restore Methods</a></li>
<li><a href="/windows/desktop/WinSock/pseudo-vs--true-blocking-2">Pseudo-blocking and true blocking</a></li>
<li><a href="/windows/desktop/WmiSdk/wmi-start-page">Windows Management Instrumentation</a></li>
<li><a href="/windows/desktop/WmiSdk/wmic">WMI command-line (WMIC) utility</a></li>
<li><a href="/windows/desktop/cfApi/cloud-files-functions">Cloud Filter Functions</a></li>
<li><a href="/windows/desktop/data-access-and-storage">Data Access and Storage</a></li>
<li><a href="/windows/desktop/direct3dhlsl/atomic-and--sm5---asm-">atomic_and (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/atomic-or--sm5---asm-">atomic_or (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/atomic-xor--sm5---asm-">atomic_xor (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/dcl-indexabletemp">dcl_indexableTemp (sm4 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/dcl-output-sgv">dcl_output_sgv (sm4 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/dcl-output-siv">dcl_output_siv (sm4 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/dcl-output">dcl_output (sm4 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/ld-raw--sm5---asm-">ld_raw (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/store-raw--sm5---asm-">store_raw (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/swapc--sm5---asm-">swapc (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/uaddc--sm5---asm-">uaddc (sm5 - asm)</a></li>
<li><a href="/windows/desktop/direct3dhlsl/usubb--sm5---asm-">usubb (sm5 - asm)</a></li>
<li><a href="/windows/desktop/imapi/portal">Image Mastering API (IMAPI.h)</a></li>
<li><a href="/windows/desktop/inputdev/about-keyboard-input">Keyboard Input Overview</a></li>
<li><a href="/windows/desktop/lwef/-search-2x-wds-aqsreference">Advanced Query Syntax</a></li>
<li><a href="/windows/desktop/medfound/codecapi-avencaacenablevbr">CODECAPI_AVEncAACEnableVBR property (Codecapi.h)</a></li>
<li><a href="/windows/desktop/menurc/using-cursors">Using Cursors</a></li>
<li><a href="/windows/desktop/shell/foldertypeid">FOLDERTYPEID (Shlguid.h)</a></li>
<li><a href="/windows/desktop/winmsg/translatemessageex">TranslateMessageEx</a></li>
<li><a href="/windows/desktop/winmsg/wm-inputlangchange">WM_INPUTLANGCHANGE message (Winuser.h)</a></li>
</ul>

## Win32 API reference
<ul>
<li><a href="/windows/win32/api/_direct3dhlsl/index">HLSL </a></li>
<li><a href="/windows/win32/api/_security/index">Security and Identity </a></li>
<li><a href="/windows/win32/api/cfapi/ne-cfapi-cf_pin_state">CF_PIN_STATE (cfapi.h) </a></li>
<li><a href="/windows/win32/api/cfapi/nf-cfapi-cfexecute">CfExecute function (cfapi.h) </a></li>
<li><a href="/windows/win32/api/dxcapi/index">Dxcapi.h header </a></li>
<li><a href="/windows/win32/api/wingdi/ns-wingdi-logfonta">LOGFONTA (wingdi.h) </a></li>
<li><a href="/windows/win32/api/wingdi/ns-wingdi-logfontw">LOGFONTW (wingdi.h) </a></li>
<li><a href="/windows/win32/api/winnt/nf-winnt-rtliseccode">RtlIsEcCode </a></li>
<li><a href="/windows/win32/api/winuser/ns-winuser-iconinfo">ICONINFO (winuser.h) </a></li>
<li><a href="/windows/win32/api/winuser/ns-winuser-rawinput">RAWINPUT (winuser.h) </a></li>
</ul>

## UWP reference
<ul>
<li><a href="/uwp/api/windows.applicationmodel.datatransfer.clipboard">Windows.ApplicationModel.DataTransfer.Clipboard</a></li>
<li><a href="/uwp/api/windows.applicationmodel.search.searchpane">Windows.ApplicationModel.Search.SearchPane</a></li>
<li><a href="/uwp/api/windows.applicationmodel.packagesignaturekind">Windows.ApplicationModel.PackageSignatureKind</a></li>
<li><a href="/uwp/api/windows.devices.input.keyboardcapabilities">Windows.Devices.Input.KeyboardCapabilities</a></li>
<li><a href="/uwp/api/windows.management.deployment.addpackageoptions">Windows.Management.Deployment.AddPackageOptions</a></li>
<li><a href="/uwp/api/windows.management.policies.namedpolicykind">Windows.Management.Policies.NamedPolicyKind</a></li>
<li><a href="/uwp/api/windows.storage.pickers.filesavepicker">Windows.Storage.Pickers.FileSavePicker</a></li>
<li><a href="/uwp/api/windows.storage.pickers.windows.storage.pickers">N:Windows.Storage.Pickers</a></li>
<li><a href="/uwp/api/windows.storage.provider.storageprovidererror">Windows.Storage.Provider.StorageProviderError</a></li>
<li><a href="/uwp/api/windows.storage.provider.storageproviderstate">Windows.Storage.Provider.StorageProviderState</a></li>
<li><a href="/uwp/api/windows.storage.provider.storageproviderstatus">Windows.Storage.Provider.StorageProviderStatus</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdateitem">Windows.System.Update.SystemUpdateItem</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdateitem.id">Windows.System.Update.SystemUpdateItem.Id</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdateitem.revision">Windows.System.Update.SystemUpdateItem.Revision</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdateitem.state">Windows.System.Update.SystemUpdateItem.State</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdateitem.title">Windows.System.Update.SystemUpdateItem.Title</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdateitemstate">Windows.System.Update.SystemUpdateItemState</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdatelasterrorinfo">Windows.System.Update.SystemUpdateLastErrorInfo</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdatemanager">Windows.System.Update.SystemUpdateManager</a></li>
<li><a href="/uwp/api/windows.system.update.systemupdatemanagerstate">Windows.System.Update.SystemUpdateManagerState</a></li>
<li><a href="/uwp/api/windows.system.update.windows.system.update">N:Windows.System.Update</a></li>
<li><a href="/uwp/api/windows.ui.composition.ianimationobject">Windows.UI.Composition.IAnimationObject</a></li>
<li><a href="/uwp/api/windows.ui.input.inking.inkpresenter">Windows.UI.Input.Inking.InkPresenter</a></li>
<li><a href="/uwp/api/windows.ui.input.crossslidethresholds">Windows.UI.Input.CrossSlideThresholds</a></li>
<li><a href="/uwp/api/windows.ui.input.crossslidingstate">Windows.UI.Input.CrossSlidingState</a></li>
<li><a href="/uwp/api/windows.ui.input.edgegesturekind">Windows.UI.Input.EdgeGestureKind</a></li>
<li><a href="/uwp/api/windows.ui.viewmanagement.core.coreinputview">Windows.UI.ViewManagement.Core.CoreInputView</a></li>
<li><a href="/uwp/api/windows.ui.webui.webuisearchactivatedeventargs">Windows.UI.WebUI.WebUISearchActivatedEventArgs</a></li>
<li><a href="/uwp/api/windows.ui.xaml.automation.automationproperties">Windows.UI.Xaml.Automation.AutomationProperties</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.control.tabindex">Windows.UI.Xaml.Controls.Control.TabIndex</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.inkcanvas">Windows.UI.Xaml.Controls.InkCanvas</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.inkcanvas.inkpresenter">Windows.UI.Xaml.Controls.InkCanvas.InkPresenter</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.inktoolbar.activetool">Windows.UI.Xaml.Controls.InkToolbar.ActiveTool</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.inktoolbar.children">Windows.UI.Xaml.Controls.InkToolbar.Children</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.textblock.inlines">Windows.UI.Xaml.Controls.TextBlock.Inlines</a></li>
<li><a href="/uwp/api/windows.ui.xaml.input.inputscopenamevalue">Windows.UI.Xaml.Input.InputScopeNameValue</a></li>
<li><a href="/uwp/api/windows.ui.xaml.media.solidcolorbrush">Windows.UI.Xaml.Media.SolidColorBrush</a></li>
<li><a href="/uwp/api/windows.ui.xaml.recthelper.empty">Windows.UI.Xaml.RectHelper.Empty</a></li>
<li><a href="/uwp/api/windows.ui.xaml.recthelper.getbottom">Windows.UI.Xaml.RectHelper.GetBottom(Windows.Foundation.Rect)</a></li>
<li><a href="/uwp/api/windows.ui.xaml.recthelper.getleft">Windows.UI.Xaml.RectHelper.GetLeft(Windows.Foundation.Rect)</a></li>
<li><a href="/uwp/api/windows.ui.xaml.recthelper.getright">Windows.UI.Xaml.RectHelper.GetRight(Windows.Foundation.Rect)</a></li>
<li><a href="/uwp/api/windows.ui.xaml.recthelper.gettop">Windows.UI.Xaml.RectHelper.GetTop(Windows.Foundation.Rect)</a></li>
<li><a href="/uwp/api/windows.ui.xaml.sizehelper.empty">Windows.UI.Xaml.SizeHelper.Empty</a></li>
<li><a href="/uwp/api/windows.ui.xaml.window.activate">Windows.UI.Xaml.Window.Activate</a></li>
</ul>