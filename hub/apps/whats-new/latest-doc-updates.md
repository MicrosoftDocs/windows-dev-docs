---
description: Discover the latest additions to the Windows developer docs.
title: Latest updates to the Windows API and developer documentation
ms.topic: article
ms.date: 8/11/2023
ms.localizationpriority: medium
ms.author: quradic
author: QuinnRadich
---

# Latest updates to the Windows developer docs

The Windows developer docs are regularly updated with new and improved information and content. Here is a summary of changes as of August 11th, 2023.

For the latest Windows Developer Documentation news, or to reach out to us with comments and questions, feel free to find us on Twitter, where our handle is [@WindowsDocs](https://twitter.com/windowsdocs).

Don't forget to visit the [Windows Developer Center](https://developer.microsoft.com/windows/), where we highlight some of the latest technologies, frameworks and news for Windows developers.

Many thanks to everyone who has contributed to the documentation. Your corrections and suggestions are very welcome! For information on contributing, please see our new [contributor hub](/contribute/).

Highlights this month include:

* [Dev Home](/windows/dev-home/): Monitor your work in the centralized dashboard, GitHub and System performance widgets. Get setup and onboard new projects with the Machine configuration tool.  
* [Dev Drive](/windows/dev-drive/): Improve your dev workload performance using this new storage format, using ReFS and specifically designed for developer scenarios, with the ability to designate trust, manage antivirus configurations, and attach security filters.
* Microsoft Defender has a new [performance mode](/microsoft-365/security/defender-endpoint/microsoft-defender-endpoint-antivirus-performance-mode?view=o365-worldwide) specifically designed for Dev Drive.
* [WinGet v1.5 including Windows Package Manager](/windows/package-manager/): Consolidate manual machine setup and project onboarding to a single command that is reliable and repeatable. 
* [PowerToys 0.72](/windows/powertoys/): These open source utilities are suggested by and developed with the help of the developer community using Windows. 
* [Windows Copilot](/windows/dev-environment/): The first PC platform to provide centralized AI assistance and designed to help people easily take action and get things done is coming soon!

<hr>

The following list of topics have also seen significant updates in the past month:

## Windows App SDK / WinUI

* [WinAppSDK 1.4 Preview Release Notes](/windows/apps/windows-app-sdk/preview-channel)
* Updated topics in [ContentDialog class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog?view=windows-app-sdk-1.3).
* Updated topics in [DebugSettings class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.debugsettings?view=windows-app-sdk-1.3).
* New content about how to configure an appContainer app in 1- and 2-project WinAppSDK apps, WndProc, WPF, and WinForms apps (MSIX appContainer apps).
* Updated topics on [MSIX appContainer apps](/windows/msix/msix-container).
* Updated [Use the dynamic dependency API to reference MSIX packages at run time](/windows/apps/desktop/modernize/framework-packages/use-the-dynamic-dependency-api).

## Code samples, tutorials and Learn Module updates

* Updated the [Microsoft Graph API tutorial for .NET MAUI on Windows development](https://learn.microsoft.com/windows/apps/windows-dotnet-maui/tutorial-graph-api), improving the clarify when using the latest Graph API release.

## Updated content

* Documented [IAccessKeyManagerStaticsDisplayMode.EnterDisplayMode](/windows/apps/api-reference/interface-members/iaccesskeymanagerstaticsdisplaymode-enterdisplaymode).
* Added sqlite APIs missing from the list on [Extension APIs for Windows 10 devices (grouped by module) - Windows UWP applications](/uwp/win32-and-com/win32-extension-apis#apis-from-winsqlite3dll).
* Added 60 new topics missing from the [EAPHost and Legacy Schema](/windows/win32/eaphost/eaphost-schemas) documentation.
* Updated [Opportunistic Locks](/windows/win32/fileio/opportunistic-locks) documentation.
* Updated [DispatcherQueueTimer.IsRepeating](/windows.system.dispatcherqueuetimer.isrepeating?view=winrt-22621).


The following list of new and updated content is automatically generated from the GitHub repo storing our documentation:

## WinRT topics

<ul>
<li><a href="https://learn.microsoft.com/windows/apps/cpp-ref-for-winrt/com-ptr">winrt::com_ptr struct template (C++/WinRT)</a></li>
<li><a href="https://learn.microsoft.com/windows/apps/cpp-ref-for-winrt/hstring">winrt::hstring struct (C++/WinRT)</a></li>
<li><a href="https://learn.microsoft.com/windows/apps/win32-and-com/win32-apis">APIs present on all Windows devices</a></li>
</ul>

## UWP topics

<ul>
<li><a href="https://learn.microsoft.com/windows/uwp/develop/index">Develop UWP apps</a></li>
<li><a href="https://learn.microsoft.com/windows/uwp/porting/index">Porting apps to Windows 10</a></li>
</ul>

## Win32 reference

<ul>
<li><a href="https://learn.microsoft.com/windows/win32/winrt/gamechattranscription/nf-gamechattranscription-igamechattranscriber-processencodedaudio">8 new topics for GameChatTranscription APIs; for example, IGameChatTranscriber::ProcessEncodedAudio</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/ADSchema/attributes-all">All Attributes</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DNS/dns-constants">DNS constants</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DevNotes/edpauditaction-function">EdpAuditAction function</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DevNotes/fileprotectionstatus-enum">FileProtectionStatus enumeration</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DevNotes/msdelta-createdeltaw">CreateDeltaW function</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DevNotes/nt-create-named-pipe-file">NtCreateNamedPipeFile function</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DevNotes/tip-testcreate-function">TestCreate function</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/DevNotes/tip-testinfo-structure">TestInfo structure</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/FileIO/file-attribute-constants">File Attribute Constants (WinNT.h)</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/FileIO/reparse-point-tags">Reparse Point Tags</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/FileIO/volume-management-structures">Volume management structures</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/Intl/unicode-subset-bitfields">Unicode Subset Bitfields</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/Msi/error-codes">MsiExec.exe and InstMsi.exe error messages (for developers)</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/NativeWiFi/dot11-auth-algorithm">DOT11_AUTH_ALGORITHM enumeration (Wlantypes.h)</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/NativeWiFi/onexschema-onex-element">OneX element</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/Power/system-power-states">System power states</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/SbsCs/application-manifests">Application manifests</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/SecCrypto/signtool">SignTool</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/Sync/registerwaitforsingleobjectex">RegisterWaitForSingleObjectEx function</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/WinAuto/inspect-objects">Accessibility tools - Inspect</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/WmiSdk/win32-serverfeature">Win32_ServerFeature class</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/WmiSdk/wmic">WMI command-line (WMIC) utility</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/bindlink/bindlink-example">Bind link API examples</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/bindlink/bindlink-overview">Overview of the Bindlink API</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/dataxchg/html-clipboard-format">HTML Clipboard Format</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/directcomp/interfaces">DirectComposition interfaces</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/hwreqchkapi/hwreqchk-examples">HWREQCHK API examples</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/lwef/-search-2x-wds-aqsreference">Advanced Query Syntax</a></li>
<li><a href="https://learn.microsoft.com/windows/desktop/menurc/using-cursors">Using Cursors</a></li>
</ul>


## UWP API reference

<ul>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.contacts.contactpicker">Windows.ApplicationModel.Contacts.ContactPicker</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboard">Windows.ApplicationModel.DataTransfer.Clipboard</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.search.searchpane">Windows.ApplicationModel.Search.SearchPane</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeatures">Windows.ApplicationModel.LimitedAccessFeatures</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.package.description">Windows.ApplicationModel.Package.Description</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.package.displayname">Windows.ApplicationModel.Package.DisplayName</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.package.logo">Windows.ApplicationModel.Package.Logo</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.applicationmodel.packagesignaturekind">Windows.ApplicationModel.PackageSignatureKind</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.alljoyn.alljoynstatus">Windows.Devices.AllJoyn.AllJoynStatus</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.enumeration.pnp.pnpobject">Windows.Devices.Enumeration.Pnp.PnpObject</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.enumeration.pnp.pnpobject.id">Windows.Devices.Enumeration.Pnp.PnpObject.Id</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.enumeration.pnp.pnpobject.type">Windows.Devices.Enumeration.Pnp.PnpObject.Type</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.enumeration.pnp.pnpobjecttype">Windows.Devices.Enumeration.Pnp.PnpObjectType</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.enumeration.pnp.pnpobjectupdate">Windows.Devices.Enumeration.Pnp.PnpObjectUpdate</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.input.keyboardcapabilities">Windows.Devices.Input.KeyboardCapabilities</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.input.pointerdeviceusage">Windows.Devices.Input.PointerDeviceUsage</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamp.availabilitychanged">Windows.Devices.Lights.Lamp.AvailabilityChanged</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray">Windows.Devices.Lights.LampArray</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.boundingbox">Windows.Devices.Lights.LampArray.BoundingBox</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.deviceid">Windows.Devices.Lights.LampArray.DeviceId</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.isavailable">Windows.Devices.Lights.LampArray.IsAvailable</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.isconnected">Windows.Devices.Lights.LampArray.IsConnected</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.isenabled">Windows.Devices.Lights.LampArray.IsEnabled</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.lamparraykind">Windows.Devices.Lights.LampArray.LampArrayKind</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparray.lampcount">Windows.Devices.Lights.LampArray.LampCount</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.lamparraykind">Windows.Devices.Lights.LampArrayKind</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.lights.windows.devices.lights">N:Windows.Devices.Lights</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.sensors.activitysensor">Windows.Devices.Sensors.ActivitySensor</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.sensors.adaptivedimmingoptions">Windows.Devices.Sensors.AdaptiveDimmingOptions</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.sensors.lockonleaveoptions">Windows.Devices.Sensors.LockOnLeaveOptions</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.devices.sensors.wakeonapproachoptions">Windows.Devices.Sensors.WakeOnApproachOptions</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.display.resolutionscale">Windows.Graphics.Display.ResolutionScale</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.imaging.bitmapplanedescription">Windows.Graphics.Imaging.BitmapPlaneDescription</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.imaging.softwarebitmap">Windows.Graphics.Imaging.SoftwareBitmap</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.print3dmanager">Windows.Graphics.Printing3D.Print3DManager</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.print3dtask">Windows.Graphics.Printing3D.Print3DTask</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.print3dtaskdetail">Windows.Graphics.Printing3D.Print3DTaskDetail</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.print3dtaskrequest">Windows.Graphics.Printing3D.Print3DTaskRequest</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.printing3dcomponent">Windows.Graphics.Printing3D.Printing3DComponent</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.printing3dmaterial">Windows.Graphics.Printing3D.Printing3DMaterial</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.printing3dmesh">Windows.Graphics.Printing3D.Printing3DMesh</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.printing3dmodel">Windows.Graphics.Printing3D.Printing3DModel</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.graphics.printing3d.printing3dmodelunit">Windows.Graphics.Printing3D.Printing3DModelUnit</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.deployment.addpackageoptions">Windows.Management.Deployment.AddPackageOptions</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.policies.namedpolicykind">Windows.Management.Policies.NamedPolicyKind</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.update.windowsupdate">Windows.Management.Update.WindowsUpdate</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.update.windowsupdate.isforos">Windows.Management.Update.WindowsUpdate.IsForOS</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.update.windowsupdate.title">Windows.Management.Update.WindowsUpdate.Title</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.update.windowsupdateitem">Windows.Management.Update.WindowsUpdateItem</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.management.update.windowsupdatemanager">Windows.Management.Update.WindowsUpdateManager</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.networking.xboxlive.xboxlivesocketkind">Windows.Networking.XboxLive.XboxLiveSocketKind</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.storage.pickers.fileopenpicker">Windows.Storage.Pickers.FileOpenPicker</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.storage.pickers.filesavepicker">Windows.Storage.Pickers.FileSavePicker</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.storage.pickers.windows.storage.pickers">N:Windows.Storage.Pickers</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.storage.provider.storageprovidererror">Windows.Storage.Provider.StorageProviderError</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.storage.provider.storageproviderstate">Windows.Storage.Provider.StorageProviderState</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.storage.provider.storageproviderstatus">Windows.Storage.Provider.StorageProviderStatus</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdateitem">Windows.System.Update.SystemUpdateItem</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdateitem.id">Windows.System.Update.SystemUpdateItem.Id</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdateitem.revision">Windows.System.Update.SystemUpdateItem.Revision</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdateitem.state">Windows.System.Update.SystemUpdateItem.State</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdateitem.title">Windows.System.Update.SystemUpdateItem.Title</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdateitemstate">Windows.System.Update.SystemUpdateItemState</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdatelasterrorinfo">Windows.System.Update.SystemUpdateLastErrorInfo</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdatemanager">Windows.System.Update.SystemUpdateManager</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.systemupdatemanagerstate">Windows.System.Update.SystemUpdateManagerState</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.update.windows.system.update">N:Windows.System.Update</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.system.dispatcherqueuetimer.isrepeating">Windows.System.DispatcherQueueTimer.IsRepeating</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.composition.compositiontexture">Windows.UI.Composition.CompositionTexture</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.composition.ianimationobject">Windows.UI.Composition.IAnimationObject</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.composition.rectangleclip">Windows.UI.Composition.RectangleClip</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.input.inking.iinkrecognizercontainer">Windows.UI.Input.Inking.IInkRecognizerContainer</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.input.inking.inkpresenter">Windows.UI.Input.Inking.InkPresenter</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.input.crossslidethresholds">Windows.UI.Input.CrossSlideThresholds</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.input.crossslidingstate">Windows.UI.Input.CrossSlidingState</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.input.edgegesturekind">Windows.UI.Input.EdgeGestureKind</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtab">Windows.UI.Shell.WindowTab</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtab.group">Windows.UI.Shell.WindowTab.Group</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtab.icon">Windows.UI.Shell.WindowTab.Icon</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtab.tag">Windows.UI.Shell.WindowTab.Tag</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtab.title">Windows.UI.Shell.WindowTab.Title</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtab.windowtab">Windows.UI.Shell.WindowTab.#ctor</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabcollection">Windows.UI.Shell.WindowTabCollection</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabcollection.size">Windows.UI.Shell.WindowTabCollection.Size</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabgroup">Windows.UI.Shell.WindowTabGroup</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabgroup.icon">Windows.UI.Shell.WindowTabGroup.Icon</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabgroup.title">Windows.UI.Shell.WindowTabGroup.Title</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabicon">Windows.UI.Shell.WindowTabIcon</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabmanager">Windows.UI.Shell.WindowTabManager</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.shell.windowtabmanager.tabs">Windows.UI.Shell.WindowTabManager.Tabs</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputview">Windows.UI.ViewManagement.Core.CoreInputView</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.webui.webuisearchactivatedeventargs">Windows.UI.WebUI.WebUISearchActivatedEventArgs</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.automation.automationproperties">Windows.UI.Xaml.Automation.AutomationProperties</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.control.tabindex">Windows.UI.Xaml.Controls.Control.TabIndex</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.inkcanvas">Windows.UI.Xaml.Controls.InkCanvas</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.inkcanvas.inkpresenter">Windows.UI.Xaml.Controls.InkCanvas.InkPresenter</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.inktoolbar.activetool">Windows.UI.Xaml.Controls.InkToolbar.ActiveTool</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.inktoolbar.children">Windows.UI.Xaml.Controls.InkToolbar.Children</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaelement">Windows.UI.Xaml.Controls.MediaElement</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.passwordbox.inputscope">Windows.UI.Xaml.Controls.PasswordBox.InputScope</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditbox.inputscope">Windows.UI.Xaml.Controls.RichEditBox.InputScope</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.textblock.inlines">Windows.UI.Xaml.Controls.TextBlock.Inlines</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.controls.textbox.inputscope">Windows.UI.Xaml.Controls.TextBox.InputScope</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.input.accesskeymanager">Windows.UI.Xaml.Input.AccessKeyManager</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.input.inputscopenamevalue">Windows.UI.Xaml.Input.InputScopeNameValue</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.media.solidcolorbrush">Windows.UI.Xaml.Media.SolidColorBrush</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.recthelper.empty">Windows.UI.Xaml.RectHelper.Empty</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.recthelper.getbottom">Windows.UI.Xaml.RectHelper.GetBottom(Windows.Foundation.Rect)</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.recthelper.getleft">Windows.UI.Xaml.RectHelper.GetLeft(Windows.Foundation.Rect)</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.recthelper.getright">Windows.UI.Xaml.RectHelper.GetRight(Windows.Foundation.Rect)</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.recthelper.gettop">Windows.UI.Xaml.RectHelper.GetTop(Windows.Foundation.Rect)</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.sizehelper.empty">Windows.UI.Xaml.SizeHelper.Empty</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.uielement.xamlroot">Windows.UI.Xaml.UIElement.XamlRoot</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.window">Windows.UI.Xaml.Window</a></li>
<li><a href="https://learn.microsoft.com/uwp/api/windows.ui.xaml.window.activate">Windows.UI.Xaml.Window.Activate</a></li>
</ul>

