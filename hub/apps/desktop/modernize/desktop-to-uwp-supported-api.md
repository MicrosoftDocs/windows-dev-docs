---
description: This article describes the WinRT APIs that are not supported for use in desktop apps.
title: Windows Runtime APIs not supported in desktop apps
ms.date: 04/23/2021
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 142b9c9b-3f7d-41b6-80da-1505de2810f9
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: 19H1
---

# Windows Runtime APIs not supported in desktop apps

Most [Windows Runtime (WinRT) APIs](/uwp/api/) can be used by desktop (.NET 5 and native C++) apps. However, some WinRT classes were designed for use only in UWP apps and don't work properly in desktop apps. Other WinRT classes work in desktop apps except for specific members.

This article lists the WinRT APIs that are not supported in desktop apps. Where available, this article suggests alternative APIs to achieve the same functionality as the unsupported APIs. Most of the alternative APIs are available in [WinUI 3](../../winui/winui3/index.md) or via COM interfaces that are available in the Windows SDK.

This article will be updated as more workarounds and replacements are identified. If you encounter an issue with an API not listed here, [create an issue](https://github.com/microsoft/microsoft-ui-xaml/issues/new?assignees=&labels=&template=bug_report.md&title=) in the [microsoft-ui-xaml](https://github.com/microsoft/microsoft-ui-xaml) repo with the API and and provide details about what you are trying to achieve by using it.

## Unsupported classes

This section lists (or describes, where a comprehensive list is not possible) WinRT classes that are not supported for use in desktop apps.

### Core classes

The following classes were designed for use only in UWP apps and do not behave properly in desktop apps.

|  Class  |  Alternative APIs |
|---------|-------------------|
| [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview) | None |
| [CoreApplicationView](/uwp/api/windows.applicationmodel.core.coreapplicationview) | Use the [Window](/windows/winui/api/microsoft.ui.xaml.window) class provided by WinUI 3 instead. |
| [CoreApplicationViewTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar)  |  Instead of the [ExtendViewIntoTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.extendviewintotitlebar) property, use the [Window.ExtendsContentIntoTitleBar](/windows/winui/api/microsoft.ui.xaml.window.extendscontentintotitlebar) property provided by WinUI 3 instead. |
| [CoreDispatcher](/uwp/api/Windows.UI.Core.CoreDispatcher) | Use the [DispatcherQueue](/windows/winui/api/microsoft.ui.xaml.window.dispatcherqueue) class provided by WinUI 3 instead.<br/><br/>Note that the [Window.Dispatcher](/uwp/api/Windows.UI.Xaml.Window.Dispatcher) and [DependencyObject.Dispatcher](/uwp/api/Windows.UI.Xaml.DependencyObject.Dispatcher) properties return `null` in desktop apps.  |
| [CoreWindow](/uwp/api/Windows.UI.Core.CoreWindow) | Instead of the [GetKeyState](/uwp/api/windows.ui.core.corewindow.getkeystate) method, use the [KeyboardInput.GetKeyStateForCurrentThread](/windows/winui/api/microsoft.ui.input.keyboardinput.getkeystateforcurrentthread) method provided by WinUI 3 instead.<br/><br/>Instead of the [PointerCursor](/uwp/api/windows.ui.core.corewindow.pointercursor) property, use the [UIElement.ProtectedCursor](/windows/winui/api/microsoft.ui.xaml.uielement.protectedcursor) property provided by WinUI 3 instead. You'll need to have a subclass of [UIElement](/windows/winui/api/microsoft.ui.xaml.uielement) to access this property. |
| [UserActivity](/uwp/api/windows.applicationmodel.useractivities.useractivity) | Use the **IUserActivitySourceHostInterop** COM interface instead (in useractivityinterop.h). |

### Classes with GetForCurrentView methods

Many classes have a static `GetForCurrentView` method, such as [UIViewSettings.GetForCurrentView](/uwp/api/Windows.UI.ViewManagement.UIViewSettings.GetForCurrentView). These `GetForCurrentView` methods have an implicit dependency on the [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview) class, which isn't supported in desktop apps. Because **ApplicationView** isn't supported in desktop apps, none of these other classes with `GetForCurrentView` methods are supported either. Note that some unsupported `GetForCurrentView` methods will not only return **null**, but will also throw exceptions.

> [!NOTE]
> One exception to this is [CoreInputView.GetForCurrentView](/uwp/api/windows.ui.viewmanagement.core.coreinputview.getforcurrentview), which is supported in desktop apps and can be used even without a [CoreWindow](/uwp/api/windows.ui.core.corewindow). This method can be used to get a [CoreInputView](/uwp/api/windows.ui.viewmanagement.core.coreinputview) object on any thread, and if that thread has a foreground window, that object will produce events.

The following classes are not supported in desktop apps because they have `GetForCurrentView` methods. This list may not be comprehensive.

|  Class  |  Alternative APIs |
|---------|-------------------|
| [AccountsSettingsPane](/uwp/api/windows.ui.applicationsettings.accountssettingspane) | Use the **IAccountsSettingsPaneInterop** COM interface instead (in accountssettingspaneinterop.h). |
| [AppCapture](/uwp/api/windows.media.capture.appcapture) | None |
| [BrightnessOverride](/uwp/api/windows.graphics.display.brightnessoverride) | None |
| [ConnectedAnimationService](/uwp/api/windows.ui.xaml.media.animation.connectedanimationservice) | None |
| [CoreDragDropManager](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragdropmanager) | Use the **IDragDropManagerInterop** COM interface instead (in dragdropinterop.h). |
| [CoreInputView](/uwp/api/windows.ui.viewmanagement.core.coreinputview) | None |
| [CoreTextServicesManager](/uwp/api/windows.ui.text.core.coretextservicesmanager) | This class is currently supported in desktop apps only in Windows Insider Preview builds. |
| [CoreWindowResizeManager](/uwp/api/windows.ui.core.corewindowresizemanager) | None |
| [DataTransferManager](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager) | Use the [IDataTransferManagerInterop](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop) COM interface instead (in shobjidl_core.h). |
| [DisplayEnhancementOverride](/uwp/api/windows.graphics.display.displayenhancementoverride) | None |
| [DisplayInformation](/uwp/api/windows.graphics.display.displayinformation) | Instead of the [LogicalDpi](/uwp/api/windows.graphics.display.displayinformation.logicaldpi) property, use the [XamlRoot.RasterizationScale](/windows/winui/api/microsoft.ui.xaml.xamlroot.rasterizationscale) property instead and listen for changes on the [XamlRoot.Changed](/uwp/api/windows.ui.xaml.xamlroot.changed) event (the [XamlRoot.RasterizationScale](/windows/winui/api/microsoft.ui.xaml.xamlroot.rasterizationscale) property is provided in WinUI 3).<br/><br/>Instead of the [RawPixelsPerViewPixel](/uwp/api/windows.graphics.display.displayinformation.rawpixelsperviewpixel) property, use the [XamlRoot.RasterizationScale](/windows/winui/api/microsoft.ui.xaml.xamlroot.rasterizationscale) property provided by WinUI 3 instead.  |
| [EdgeGesture](/uwp/api/windows.ui.input.edgegesture) | None |
| [GazeInputSourcePreview](/uwp/api/windows.devices.input.preview.gazeinputsourcepreview) | None |
| [HdmiDisplayInformation](/uwp/api/windows.graphics.display.core.hdmidisplayinformation) | None |
| [HolographicKeyboardPlacementOverridePreview](/uwp/api/windows.applicationmodel.preview.holographic.holographickeyboardplacementoverridepreview) | None |
| [InputPane](/uwp/api/windows.ui.viewmanagement.inputpane) | Use the **IInputPaneInterop** COM interface instead (in inputpaneinterop.h). |
| [KeyboardDeliveryInterceptor](/uwp/api/windows.ui.input.keyboarddeliveryinterceptor) | None |
| [LockApplicationHost](/uwp/api/windows.applicationmodel.lockscreen.lockapplicationhost) | None |
| [MouseDevice](/uwp/api/windows.devices.input.mousedevice) | None |
| [PlayToManager](/uwp/api/windows.media.playto.playtomanager.getforcurrentview) | Use the **IPlayToManagerInterop** COM interface instead (in playtomanagerinterop.h). |
| [PointerVisualizationSettings](/uwp/api/windows.ui.input.pointervisualizationsettings) | None |
| [Print3DManager](/uwp/api/windows.graphics.printing3d.print3dmanager) | Use the **IPrinting3DManagerInterop** COM interface instead (in print3dmanagerinterop.h). |
| [PrintManager](/uwp/api/windows.graphics.printing.printmanager) | Use the **IPrintManagerInterop** COM interface instead (in printmanagerinterop.h). |
| [ProtectionPolicyManager](/uwp/api/windows.security.enterprisedata.protectionpolicymanager) | None |
| [RadialControllerConfiguration](/uwp/api/windows.ui.input.radialcontrollerconfiguration) | Use the **IRadialControllerConfigurationInterop** COM interface instead (in radialcontrollerinterop.h). |
| [ResourceContext](/uwp/api/windows.applicationmodel.resources.core.resourcecontext) | None |
| [ResourceLoader](/uwp/api/windows.applicationmodel.resources.resourceloader) | None |
| [SearchPane](/uwp/api/windows.applicationmodel.search.searchpane) | None |
| [SettingsPane](/uwp/api/windows.ui.applicationsettings.settingspane) | None |
| [SpatialInteractionManager](/uwp/api/windows.ui.input.spatial.spatialinteractionmanager) | Use the **ISpatialInteractionManagerInterop** COM interface instead (in spatialinteractionmanagerinterop.h). |
| [SystemMediaTransportControls](/uwp/api/windows.media.systemmediatransportcontrols) | Use the **ISystemMediaTransportControlsInterop** COM interface instead (in systemmediatransportcontrolsinterop.h). |
| [SystemNavigationManager](/uwp/api/windows.ui.core.systemnavigationmanager) | None |
| [SystemNavigationManagerPreview](/uwp/api/windows.ui.core.preview.systemnavigationmanagerpreview) | None |
| [UserActivityRequestManager](/uwp/api/windows.applicationmodel.useractivities.useractivityrequestmanager) | Use the **IUserActivityRequestManagerInterop** COM interface insead (in useractivityinterop.h). |
| [UIViewSettings](/uwp/api/windows.ui.viewmanagement.uiviewsettings) | Use the **IUIViewSettingsInterop** COM interface instead (in uiviewsettingsinterop.h). |
| [WebAuthenticationBroker](/uwp/api/Windows.Security.Authentication.Web.WebAuthenticationBroker) | None. for more details, see [this GitHub issue](https://github.com/microsoft/ProjectReunion/issues/398). |

## Unsupported members

This section lists (or describes, where a comprehensive list is not possible) specific members of WinRT classes that are not supported for use in desktop apps. Unless otherwise noted, the rest of the classes apart from these members are supported in desktop apps.

### Events

The following classes are supported in desktop apps except for the specified events.

|  Class  |  Unsupported events |
|---------|-------------------|
| [UISettings](/uwp/api/Windows.UI.ViewManagement.UISettings) | [ColorValuesChanged](/uwp/api/Windows.UI.ViewManagement.UISettings.ColorValuesChanged) |
| [AccessibilitySettings](/uwp/api/Windows.UI.ViewManagement.AccessibilitySettings) | [HighContrastChanged](/uwp/api/Windows.UI.ViewManagement.AccessibilitySettings.HighContrastChanged) |

### Methods that use the Request naming pattern

Methods that follow the `Request` naming pattern, such as [AppCapability.RequestAccessAsync](/uwp/api/windows.security.authorization.appcapabilityaccess.appcapability.requestaccessasync) and [StoreContext.RequestPurchaseAsync](/uwp/api/windows.services.store.storecontext.requestpurchaseasync), are not supported in desktop apps. Internally, these methods use the [Windows.UI.Popups](/uwp/api/windows.ui.popups) class. This class requires that the thread have a [CoreWindow](/uwp/api/Windows.UI.Core.CoreWindow) object, which isn't supported in desktop apps.

The full list of methods that follow the `Requestxxx` naming pattern is very long, and this article does not provide a comprehensive list of these methods.
