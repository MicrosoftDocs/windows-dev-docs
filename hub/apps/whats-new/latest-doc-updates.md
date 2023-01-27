---
description: Discover the latest additions to the Windows developer docs.
title: Latest updates to the Windows API and developer documentation
ms.topic: article
ms.date: 1/26/2023
ms.localizationpriority: medium
ms.author: quradic
author: QuinnRadich
---

# Latest updates to the Windows developer docs

The Windows developer docs are regularly updated with new and improved information and content. Here is a summary of changes as of January 26th, 2022.

Note: For information regarding Windows 11, please see [What's cool for developers](https://developer.microsoft.com/windows/windows-for-developers/) and the [Windows Developer Center](https://developer.microsoft.com/windows/).

For the latest Windows Developer Documentation news, or to reach out to us with comments and questions, feel free to find us on Twitter, where our handle is [@WindowsDocs](https://twitter.com/windowsdocs).

*Many thanks to everyone who has contributed to the documentation. Your corrections and suggestions are very welcome! For information on contributing, please see our [contributor guide](/contribute/).*

Highlights this month include:

## Windows App SDK / WinUI

* [Data binding content migrated from UWP to WinAppSDK](/windows/apps/develop/data-binding/) 
* [Data Access WinAppSDK content migrated from UWP](/windows/apps/develop/data-access/) 
* [Using Custom Annotations](/windows/win32/winauto/uiauto-using-custom-annotations)
* [DirectML 1.10 documentation refresh](/windows/ai/directml/dml-version-history)
* New Fileapi.h reference APIs added: [DISK_SPACE_INFORMATION](/windows/win32/api/fileapi/ns-fileapi-disk_space_information), [GetDiskSpaceInformationA](/windows/win32/api/fileapi/nf-fileapi-getdiskspaceinformationa), and [GetDiskSpaceInformationW](/windows/win32/api/fileapi/nf-fileapi-getdiskspaceinformationw), [CREATEFILE2_EXTENDED_PARAMETERS](/windows/win32/api/fileapi/ns-fileapi-createfile2_extended_parameters)

## Code samples, tutorials and Learn Module updates

* If you're interested in using Linux on your Windows machine, there is a new training module: [Get started with Windows Subsystem for Linux (WSL)](/training/modules/wsl/wsl-introduction/).
* Updated the [.NET MAUI for Windows tutorial](/windows/apps/windows-dotnet-maui/walkthrough-first-app).
* Updated the [RSS reader tutorial (Rust for Windows with VS Code)](/windows/dev-environment/rust/rss-reader-rust-for-windows).

## Updated content


* PWA tab added to Cross-platform options in [Windows Dev Overview page](/windows/apps/get-started/?tabs=net-maui%2Ccpp-win32).
* Updated the "Start building apps" section of the [Build Desktop Apps page](/windows/apps/desktop/).
* Updated our [.NET MAUI for Windows overview](/windows/apps/windows-dotnet-maui/) with some new, relevant blog post links.
* In [Initialize the Windows App SDK](/windows/apps/package-and-deploy/deploy-overview), documented the need to call [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/Microsoft.Windows.ApplicationModel.WindowsAppRuntime.DeploymentManager.Initialize?view=windows-app-sdk-1.2)
 during app startup.
* Improved [Windows App SDK deployment guide for framework-dependent packaged apps](/windows/apps/windows-app-sdk/deploy-packaged-apps).
* Updated the [PyTorch with DirectML](/windows/ai/directml/gpu-pytorch-windows) docs.
* Updated [Create an unsigned MSIX package for testing](/windows/msix/package/unsigned-package) to explain why admin privilege is needed to install an unsigned package.
* Quality and correctness improvements to [Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix?tabs=csharp), [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net), and [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time).
* Updated the topics [App capability declarations](/windows/uwp/packaging/app-capability-declarations), [Create firewall exception for your app](/windows/apps/desktop/modernize/desktop-to-uwp-extensions), [Application (Windows 10)](/uwp/schemas/appxpackage/uapmanifestschema/element-application), [Understanding how packaged desktop apps run on Windows](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes), and [Generating MSIX package components](/windows/msix/desktop/desktop-to-uwp-manual-conversion).
* Improved [ISpecialSystemProperties](/windows/win32/immact/immact/nn-immact-ispecialsystemproperties) and [ISpecialSystemProperties::SetLUARunLevel](/windows/win32/immact/immact/nf-immact-ispecialsystemproperties-setluarunlevel).


## Developer tool updates

* [Windows Subsystem for Linux, Enterprise and Security Control Options](/windows/wsl/enterprise)
* [Windows Subsystem for Android updates](/windows/android/wsa/)
* [Windows Package Manager updates](/windows/package-manager/)
* [PowerToys](/windows/powertoys/install)


<hr>

## Updated in the last month

The following list of topics have seen significant updates in the past month, as per GitHub logs:

## Win32 Conceptual

<ul>
<li><a href="/windows/desktop/Controls/em-posfromchar">EM_POSFROMCHAR message (Winuser.h)</a></li>
<li><a href="/windows/desktop/inputdev/about-keyboard-input">Keyboard Input Overview</a></li>
<li><a href="/windows/desktop/menurc/resdir">RESDIR structure</a></li>
</ul>



## UWP reference
<ul>
<li><a href="/uwp/api/windows.media.protection.protectioncapabilities">Windows.Media.Protection.ProtectionCapabilities</a></li>
<li><a href="/uwp/api/windows.networking.networkoperators.dataclasses">Windows.Networking.NetworkOperators.DataClasses</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.appbar">Windows.UI.Xaml.Controls.AppBar</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.appbarbutton">Windows.UI.Xaml.Controls.AppBarButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.appbarseparator">Windows.UI.Xaml.Controls.AppBarSeparator</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.appbartogglebutton">Windows.UI.Xaml.Controls.AppBarToggleButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.autosuggestbox">Windows.UI.Xaml.Controls.AutoSuggestBox</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.button">Windows.UI.Xaml.Controls.Button</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.calendardatepicker">Windows.UI.Xaml.Controls.CalendarDatePicker</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.calendarview">Windows.UI.Xaml.Controls.CalendarView</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.checkbox">Windows.UI.Xaml.Controls.CheckBox</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.colorpicker">Windows.UI.Xaml.Controls.ColorPicker</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.combobox">Windows.UI.Xaml.Controls.ComboBox</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.comboboxitem">Windows.UI.Xaml.Controls.ComboBoxItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.commandbar">Windows.UI.Xaml.Controls.CommandBar</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.commandbarflyout">Windows.UI.Xaml.Controls.CommandBarFlyout</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.contentdialog">Windows.UI.Xaml.Controls.ContentDialog</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.datepicker">Windows.UI.Xaml.Controls.DatePicker</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.dropdownbutton">Windows.UI.Xaml.Controls.DropDownButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.flipview">Windows.UI.Xaml.Controls.FlipView</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.flipviewitem">Windows.UI.Xaml.Controls.FlipViewItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.gridviewheaderitem">Windows.UI.Xaml.Controls.GridViewHeaderItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.gridviewitem">Windows.UI.Xaml.Controls.GridViewItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.hub">Windows.UI.Xaml.Controls.Hub</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.hubsection">Windows.UI.Xaml.Controls.HubSection</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.hyperlinkbutton">Windows.UI.Xaml.Controls.HyperlinkButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.listviewheaderitem">Windows.UI.Xaml.Controls.ListViewHeaderItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.listviewitem">Windows.UI.Xaml.Controls.ListViewItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.mediatransportcontrols">Windows.UI.Xaml.Controls.MediaTransportControls</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.menubar">Windows.UI.Xaml.Controls.MenuBar</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.menuflyout">Windows.UI.Xaml.Controls.MenuFlyout</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.menuflyoutitem">Windows.UI.Xaml.Controls.MenuFlyoutItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.menuflyoutpresenter">Windows.UI.Xaml.Controls.MenuFlyoutPresenter</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.menuflyoutsubitem">Windows.UI.Xaml.Controls.MenuFlyoutSubItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.navigationview">Windows.UI.Xaml.Controls.NavigationView</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.passwordbox">Windows.UI.Xaml.Controls.PasswordBox</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.personpicture">Windows.UI.Xaml.Controls.PersonPicture</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.pivot">Windows.UI.Xaml.Controls.Pivot</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.pivotitem">Windows.UI.Xaml.Controls.PivotItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.progressbar">Windows.UI.Xaml.Controls.ProgressBar</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.progressring">Windows.UI.Xaml.Controls.ProgressRing</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.radiobutton">Windows.UI.Xaml.Controls.RadioButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.ratingcontrol">Windows.UI.Xaml.Controls.RatingControl</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.refreshcontainer">Windows.UI.Xaml.Controls.RefreshContainer</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.refreshvisualizer">Windows.UI.Xaml.Controls.RefreshVisualizer</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.richeditbox">Windows.UI.Xaml.Controls.RichEditBox</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.scrollviewer">Windows.UI.Xaml.Controls.ScrollViewer</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.slider">Windows.UI.Xaml.Controls.Slider</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.splitbutton">Windows.UI.Xaml.Controls.SplitButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.swipecontrol">Windows.UI.Xaml.Controls.SwipeControl</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.textbox">Windows.UI.Xaml.Controls.TextBox</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.timepicker">Windows.UI.Xaml.Controls.TimePicker</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.togglemenuflyoutitem">Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.togglesplitbutton">Windows.UI.Xaml.Controls.ToggleSplitButton</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.toggleswitch">Windows.UI.Xaml.Controls.ToggleSwitch</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.tooltip">Windows.UI.Xaml.Controls.ToolTip</a></li>
<li><a href="/uwp/api/windows.ui.xaml.controls.treeview">Windows.UI.Xaml.Controls.TreeView</a></li>
</ul>
