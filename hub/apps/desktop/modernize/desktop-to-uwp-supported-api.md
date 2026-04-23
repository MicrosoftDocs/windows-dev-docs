---
description: This article describes WinRT APIs that aren't supported for use in desktop apps, or that have restrictions.
title: Windows Runtime APIs not supported in desktop apps
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 142b9c9b-3f7d-41b6-80da-1505de2810f9
ms.localizationpriority: medium
ms.custom: 19H1
---

# Windows Runtime APIs not supported in desktop apps

Although you can use most Windows Runtime (WinRT) APIs (see [Windows UWP namespaces](/uwp/api/)) in your C# or C++ desktop app, there are two main sets of WinRT APIs that aren't supported in desktop apps, or that have restrictions:

* APIs that have dependencies on user interface (UI) features that were designed for use only in a Universal Windows Platform (UWP) app.
* APIs that require package identity (see [Features that require package identity](./modernize-packaged-apps.md)). Such APIs are supported only in desktop apps that are packaged using [MSIX](/windows/msix/).

This article provides details about both of those sets of WinRT APIs. Where available, this article suggests alternative APIs to achieve the same functionality as the APIs that are unsupported in desktop apps. Most of the alternative APIs are available in [WinUI 3](../../winui/index.md) or via WinRT COM interfaces that are available in the Windows SDK.

> [!NOTE]
> Apps using .NET can make use of provided class implementations for some of the WinRT COM interfaces listed in this article. Those classes are easier to work with than using the WinRT COM interfaces directly. For more information about the available class implementations, see [Call interop APIs from a .NET app](winrt-com-interop-csharp.md). Note that those classes require the .NET 6 SDK or later.

This article will be updated as more workarounds and replacements are identified. If you encounter an issue with an API not listed here, please [create an issue](https://github.com/microsoft/microsoft-ui-xaml/issues/new?assignees=&labels=&template=bug_report.md&title=) in the [microsoft-ui-xaml](https://github.com/microsoft/microsoft-ui-xaml) repo with the API name and details about what you're trying to achieve by using it.

## APIs that have dependencies on UWP-only UI features

Some WinRT APIs were designed specifically for UI scenarios in a UWP app. Those APIs do not behave properly in desktop apps due to threading model and other platform differences. Those APIs, and other WinRT APIs that have dependencies on them, aren't supported for use in desktop apps.

### Core unsupported classes

These WinRT classes aren't supported in desktop apps:

|  Class  |  Alternative APIs |
|-|-|
| [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview) | None |
| [**CoreApplicationView**](/uwp/api/windows.applicationmodel.core.coreapplicationview) | Use the [**Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) class provided by WinUI 3 instead. |
| [**CoreApplicationViewTitleBar**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar) | Instead of the [**ExtendViewIntoTitleBar**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.extendviewintotitlebar) property, use the [**Window.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.extendscontentintotitlebar) property provided by WinUI 3. |
| [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) | Use the [**Microsoft.UI.Xaml.Window.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcherqueue) property provided by WinUI 3 instead.<br/><br/>Note that the [**Windows.UI.Xaml.Window.Dispatcher**](/uwp/api/Windows.UI.Xaml.Window.Dispatcher) and [**Windows.UI.Xaml.DependencyObject.Dispatcher**](/uwp/api/Windows.UI.Xaml.DependencyObject.Dispatcher) properties return `null` in a desktop app.  |
| [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) | Also see the [Classes that implement IInitializeWithWindow](#classes-that-implement-iinitializewithwindow) section below.<br/><br/>Instead of the [**GetKeyState**](/uwp/api/windows.ui.core.corewindow.getkeystate) method, use the [**InputKeyboardSource.GetKeyStateForCurrentThread**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputkeyboardsource.getkeystateforcurrentthread) method provided by WinUI 3.<br/><br/>Instead of the [**PointerCursor**](/uwp/api/windows.ui.core.corewindow.pointercursor) property, use the [**UIElement.ProtectedCursor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.protectedcursor) property provided by WinUI 3. You'll need to have a subclass of [**UIElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement) to access that property. |
| [**UserActivity**](/uwp/api/windows.applicationmodel.useractivities.useractivity) | Use the [**IUserActivitySourceHostInterop**](/windows/win32/api/useractivityinterop/nn-useractivityinterop-iuseractivitysourcehostinterop) COM interface instead (in `useractivityinterop.h`). |

For other WinRT APIs that aren't supported in desktop apps, see [Unsupported members](#unsupported-members) later in this topic.

### Classes with an XxxForCurrentView method

Many WinRT classes have a static **GetForCurrentView** or **CreateForCurrentView** method, such as [**UIViewSettings.GetForCurrentView**](/uwp/api/Windows.UI.ViewManagement.UIViewSettings.GetForCurrentView). Those **XxxForCurrentView** methods have an implicit dependency on the [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview) type, which isn't supported in desktop apps. Because **ApplicationView** isn't supported in desktop apps, none of the **XxxForCurrentView** methods are supported either. Some unsupported **XxxForCurrentView** methods not only return `null`, but also throw exceptions.

> [!NOTE]
> [**CoreInputView.GetForCurrentView**](/uwp/api/windows.ui.viewmanagement.core.coreinputview.getforcurrentview) *is* supported in desktop apps, and it *can* be used even without a [**CoreWindow**](/uwp/api/windows.ui.core.corewindow). You can use that method to retrieve a [**CoreInputView**](/uwp/api/windows.ui.viewmanagement.core.coreinputview) object on any thread; and if that thread has a foreground window, then that object will produce events.

The following classes *are* supported in desktop apps; but to retrieve an instance of one in a desktop app, you use a mechanism that's different from the **GetForCurrentView** or **CreateForCurrentView** methods. For the classes below that have a COM interface listed as the alternative API, C# developers can also consume those WinRT COM interfaces (see [Call interop APIs from a .NET app](winrt-com-interop-csharp.md)). The list might not be comprehensive.

|  Class  |  Alternative APIs |
|---------|-------------------|
| [**AccountsSettingsPane**](/uwp/api/windows.ui.applicationsettings.accountssettingspane) | Use the [**IAccountsSettingsPaneInterop**](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop) COM interface instead (in `accountssettingspaneinterop.h`). |
| [**CoreDragDropManager**](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragdropmanager) | Use the [**IDragDropManagerInterop**](/windows/win32/api/dragdropinterop/nn-dragdropinterop-idragdropmanagerinterop) COM interface instead (in `dragdropinterop.h`). |
| [**CoreTextServicesManager**](/uwp/api/windows.ui.text.core.coretextservicesmanager) | This class is currently supported in desktop apps only in Windows Insider Preview builds. |
| [**DataTransferManager**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager) | Use the [**IDataTransferManagerInterop**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop) COM interface instead (in `shobjidl_core.h`). |
| [**DisplayInformation**](/uwp/api/windows.graphics.display.displayinformation) | To retrieve an instance of **DisplayInformation**, use the [**IDisplayInformationStaticsInterop**](/windows/win32/api/windows.graphics.display.interop/nn-windows-graphics-display-interop-idisplayinformationstaticsinterop) interface.<br/><br/>Alternatively, instead of the [**LogicalDpi**](/uwp/api/windows.graphics.display.displayinformation.logicaldpi) property, you can use the [**XamlRoot.RasterizationScale**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.rasterizationscale) property, and listen for changes via the [**XamlRoot.Changed**](/uwp/api/windows.ui.xaml.xamlroot.changed) event (the [**XamlRoot.RasterizationScale**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.rasterizationscale) property is provided in WinUI 3).<br/><br/>And, instead of the [**RawPixelsPerViewPixel**](/uwp/api/windows.graphics.display.displayinformation.rawpixelsperviewpixel) property, you have the option to use the [**XamlRoot.RasterizationScale**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.rasterizationscale) property provided by WinUI 3.|
| [**InputPane**](/uwp/api/windows.ui.viewmanagement.inputpane) | Use the [**IInputPaneInterop**](/windows/win32/api/inputpaneinterop/nn-inputpaneinterop-iinputpaneinterop) COM interface instead (in `inputpaneinterop.h`). |
| [**PlayToManager**](/uwp/api/windows.media.playto.playtomanager.getforcurrentview) | Use the [**IPlayToManagerInterop**](/windows/win32/api/playtomanagerinterop/nn-playtomanagerinterop-iplaytomanagerinterop) COM interface instead (in `playtomanagerinterop.h`). |
| [**Print3DManager**](/uwp/api/windows.graphics.printing3d.print3dmanager) | Use the [**IPrinting3DManagerInterop**](/windows/win32/api/print3dmanagerinterop/nn-print3dmanagerinterop-iprinting3dmanagerinterop) COM interface instead (in `print3dmanagerinterop.h`). |
| [**PrintManager**](/uwp/api/windows.graphics.printing.printmanager) | Use the [**IPrintManagerInterop**](/windows/win32/api/printmanagerinterop/nn-printmanagerinterop-iprintmanagerinterop) COM interface instead (in `printmanagerinterop.h`). |
| [**RadialController**](/uwp/api/windows.ui.input.radialcontroller) | Use the [**IRadialControllerInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerinterop) COM interface instead (in `radialcontrollerinterop.h`). |
| [**RadialControllerConfiguration**](/uwp/api/windows.ui.input.radialcontrollerconfiguration) | Use the [**IRadialControllerConfigurationInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerconfigurationinterop) COM interface instead (in `radialcontrollerinterop.h`). |
| [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext) | See [MRT to MRT Core migration](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/mrtcore.md). |
| [**ResourceLoader**](/uwp/api/windows.applicationmodel.resources.resourceloader) | See [MRT to MRT Core migration](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/mrtcore.md). |
| [**SpatialInteractionManager**](/uwp/api/windows.ui.input.spatial.spatialinteractionmanager) | Use the [**ISpatialInteractionManagerInterop**](/windows/win32/api/spatialinteractionmanagerinterop/nn-spatialinteractionmanagerinterop-ispatialinteractionmanagerinterop) COM interface instead (in `spatialinteractionmanagerinterop.h`). |
| [**SystemMediaTransportControls**](/uwp/api/windows.media.systemmediatransportcontrols) | Use the [**ISystemMediaTransportControlsInterop**](/windows/win32/api/systemmediatransportcontrolsinterop/nn-systemmediatransportcontrolsinterop-isystemmediatransportcontrolsinterop) COM interface instead (in `systemmediatransportcontrolsinterop.h`). |
| [**UserActivityRequestManager**](/uwp/api/windows.applicationmodel.useractivities.useractivityrequestmanager) | Use the [**IUserActivityRequestManagerInterop**](/windows/win32/api/useractivityinterop/nn-useractivityinterop-iuseractivityrequestmanagerinterop) COM interface instead (in `useractivityinterop.h`). |
| [**UIViewSettings**](/uwp/api/windows.ui.viewmanagement.uiviewsettings) | Use the [**IUIViewSettingsInterop**](/windows/win32/api/uiviewsettingsinterop/nn-uiviewsettingsinterop-iuiviewsettingsinterop) COM interface instead (in `uiviewsettingsinterop.h`). |

The following classes are *not* supported in desktop apps because the APIs don't provide an alternative to their **GetForCurrentView** or **CreateForCurrentView** method. The list might not be comprehensive.

|  Class  |  Alternative APIs |
|---------|-------------------|
| [**AppCapture**](/uwp/api/windows.media.capture.appcapture) | None |
| [**BrightnessOverride**](/uwp/api/windows.graphics.display.brightnessoverride) | None |
| [**ConnectedAnimationService**](/uwp/api/windows.ui.xaml.media.animation.connectedanimationservice) | None |
| [**CoreInputView**](/uwp/api/windows.ui.viewmanagement.core.coreinputview) | None |
| [**CoreWindowResizeManager**](/uwp/api/windows.ui.core.corewindowresizemanager) | None |
| [**DisplayEnhancementOverride**](/uwp/api/windows.graphics.display.displayenhancementoverride) | None |
| [**EdgeGesture**](/uwp/api/windows.ui.input.edgegesture) | None |
| [**GazeInputSourcePreview**](/uwp/api/windows.devices.input.preview.gazeinputsourcepreview) | None |
| [**HdmiDisplayInformation**](/uwp/api/windows.graphics.display.core.hdmidisplayinformation) | None |
| [**HolographicKeyboardPlacementOverridePreview**](/uwp/api/windows.applicationmodel.preview.holographic.holographickeyboardplacementoverridepreview) | None |
| [**KeyboardDeliveryInterceptor**](/uwp/api/windows.ui.input.keyboarddeliveryinterceptor) | None |
| [**LockApplicationHost**](/uwp/api/windows.applicationmodel.lockscreen.lockapplicationhost) | None |
| [**MouseDevice**](/uwp/api/windows.devices.input.mousedevice) | None |
| [**PointerVisualizationSettings**](/uwp/api/windows.ui.input.pointervisualizationsettings) | None |
| [**ProtectionPolicyManager**](/uwp/api/windows.security.enterprisedata.protectionpolicymanager) | None |
| [**SearchPane**](/uwp/api/windows.applicationmodel.search.searchpane) | None |
| [**SettingsPane**](/uwp/api/windows.ui.applicationsettings.settingspane) | None |
| [**SystemNavigationManager**](/uwp/api/windows.ui.core.systemnavigationmanager) | None |
| [**SystemNavigationManagerPreview**](/uwp/api/windows.ui.core.preview.systemnavigationmanagerpreview) | None |
| [**WebAuthenticationBroker**](/uwp/api/Windows.Security.Authentication.Web.WebAuthenticationBroker) | None. for more details, see the [WebAuthenticationBroker.AuthenticateAsync throws COMException](https://github.com/microsoft/ProjectReunion/issues/398) GitHub issue. |

### Classes that implement IInitializeWithWindow

Certain pickers, popups, dialogs, and other Windows Runtime (WinRT) objects depend on a [**CoreWindow**](/uwp/api/windows.ui.core.corewindow); typically, to display a UI. Even though **CoreWindow** isn't supported in desktop apps (see [Core unsupported classes](#core-unsupported-classes) above), you can still use many of those WinRT classes in your desktop app by adding a little bit of interoperation code.

For more info (including a list of affected types), and code examples, see [Display WinRT UI objects that depend on CoreWindow](../../develop/ui-input/display-ui-objects.md).

### Unsupported members

This section lists (or describes, where a comprehensive list isn't possible) specific members of WinRT classes that aren't supported for use in desktop apps. Unless otherwise noted, the rest of the classes apart from these members are supported in desktop apps.

#### Events

The following classes are supported in desktop apps, except for the specified event(s).

|  Class  |  Unsupported events |
|---------|-------------------|
| [**UISettings**](/uwp/api/Windows.UI.ViewManagement.UISettings) | [**ColorValuesChanged**](/uwp/api/Windows.UI.ViewManagement.UISettings.ColorValuesChanged) |
| [**AccessibilitySettings**](/uwp/api/Windows.UI.ViewManagement.AccessibilitySettings) | [**HighContrastChanged**](/uwp/api/Windows.UI.ViewManagement.AccessibilitySettings.HighContrastChanged) |

#### Methods

The following classes are supported in desktop apps, except for the specified method(s).

|  Class  |  Unsupported methods |
|---------|-------------------|
| [**DeviceInformationPairing**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationPairing)  | [**PairAsync**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationPairing.PairAsync) |

#### Methods that use the Request naming pattern

Most methods that follow the **Request** naming pattern&mdash;such as [**AppCapability.RequestAccessAsync**](/uwp/api/windows.security.authorization.appcapabilityaccess.appcapability.requestaccessasync) and [**StoreContext.RequestPurchaseAsync**](/uwp/api/windows.services.store.storecontext.requestpurchaseasync)&mdash;aren't supported in desktop apps. Internally, these methods use the [**Windows.UI.Popups**](/uwp/api/windows.ui.popups) class. That class requires that the thread have a [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object, which isn't supported in desktop apps.

The full list of methods that follow the **Request** naming pattern is very long, and this article doesn't provide a comprehensive list of those methods.

## APIs that require package identity

The following WinRT classes require package identity (see [Features that require package identity](./modernize-packaged-apps.md)). These APIs are supported only in desktop apps that are packaged (that is, that have package identity at runtime). The list might not be comprehensive.

* [**Windows.ApplicationModel.DataTransfer.DataProviderHandler**](/uwp/api/windows.applicationmodel.datatransfer.dataproviderhandler)
* [**Windows.ApplicationModel.DataTransfer.DataRequest**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataRequest)
* [**Windows.ApplicationModel.DataTransfer.DataRequestDeferral**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataRequestDeferral)
* [**Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs)
* [**Windows.ApplicationModel.DataTransfer.DataTransferManager**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataTransferManager)
* [**Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager**](/uwp/api/Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager)
* [**Windows.ApplicationModel.DataTransfer.TargetApplicationChosenEventArgs**](/uwp/api/Windows.ApplicationModel.DataTransfer.TargetApplicationChosenEventArgs)
* [**Windows.ApplicationModel.Resources.Core.NamedResource**](/uwp/api/Windows.ApplicationModel.Resources.Core.NamedResource)
* [**Windows.ApplicationModel.Resources.Core.ResourceCandidate**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceCandidate)
* [**Windows.ApplicationModel.Resources.Core.ResourceCandidateVectorView**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceCandidateVectorView)
* [**Windows.ApplicationModel.Resources.Core.ResourceContext**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceContext)
* [**Windows.ApplicationModel.Resources.Core.ResourceContextLanguagesVectorView**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceContextLanguagesVectorView)
* [**Windows.ApplicationModel.Resources.Core.ResourceManager**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceManager)
* [**Windows.ApplicationModel.Resources.Core.ResourceMap**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceMap)
* [**Windows.ApplicationModel.Resources.Core.ResourceMapIterator**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceMapIterator)
* [**Windows.ApplicationModel.Resources.Core.ResourceMapMapView**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceMapMapView)
* [**Windows.ApplicationModel.Resources.Core.ResourceMapMapViewIterator**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceMapMapViewIterator)
* [**Windows.ApplicationModel.Resources.Core.ResourceQualifier**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceQualifier)
* [**Windows.ApplicationModel.Resources.Core.ResourceQualifierMapView**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceQualifierMapView)
* [**Windows.ApplicationModel.Resources.Core.ResourceQualifierObservableMap**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceQualifierObservableMap)
* [**Windows.ApplicationModel.Resources.Core.ResourceQualifierVectorView**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceQualifierVectorView)
* [**Windows.ApplicationModel.Resources.ResourceLoader**](/uwp/api/Windows.ApplicationModel.Resources.ResourceLoader)
* [**Windows.Data.Pdf.PdfDocument**](/uwp/api/Windows.Data.Pdf.PdfDocument)
* [**Windows.Data.Pdf.PdfPage**](/uwp/api/Windows.Data.Pdf.PdfPage)
* [**Windows.Data.Pdf.PdfPageDimensions**](/uwp/api/Windows.Data.Pdf.PdfPageDimensions)
* [**Windows.Data.Pdf.PdfPageRenderOptions**](/uwp/api/Windows.Data.Pdf.PdfPageRenderOptions)
* [**Windows.Data.Text.SelectableWordSegmentsTokenizingHandler**](/uwp/api/windows.data.text.selectablewordsegmentstokenizinghandler)
* [**Windows.Data.Text.SemanticTextQuery**](/uwp/api/Windows.Data.Text.SemanticTextQuery)
* [**Windows.Data.Text.TextConversionGenerator**](/uwp/api/Windows.Data.Text.TextConversionGenerator)
* [**Windows.Data.Text.TextPredictionGenerator**](/uwp/api/Windows.Data.Text.TextPredictionGenerator)
* [**Windows.Data.Text.TextReverseConversionGenerator**](/uwp/api/Windows.Data.Text.TextReverseConversionGenerator)
* [**Windows.Data.Text.WordSegmentsTokenizingHandler**](/uwp/api/windows.data.text.wordsegmentstokenizinghandler)
* [**Windows.Data.Xml.Dom.DtdEntity**](/uwp/api/Windows.Data.Xml.Dom.DtdEntity)
* [**Windows.Data.Xml.Dom.DtdNotation**](/uwp/api/Windows.Data.Xml.Dom.DtdNotation)
* [**Windows.Data.Xml.Dom.XmlAttribute**](/uwp/api/Windows.Data.Xml.Dom.XmlAttribute)
* [**Windows.Data.Xml.Dom.XmlCDataSection**](/uwp/api/Windows.Data.Xml.Dom.XmlCDataSection)
* [**Windows.Data.Xml.Dom.XmlComment**](/uwp/api/Windows.Data.Xml.Dom.XmlComment)
* [**Windows.Data.Xml.Dom.XmlDocument**](/uwp/api/Windows.Data.Xml.Dom.XmlDocument)
* [**Windows.Data.Xml.Dom.XmlDocumentFragment**](/uwp/api/Windows.Data.Xml.Dom.XmlDocumentFragment)
* [**Windows.Data.Xml.Dom.XmlDocumentType**](/uwp/api/Windows.Data.Xml.Dom.XmlDocumentType)
* [**Windows.Data.Xml.Dom.XmlDomImplementation**](/uwp/api/Windows.Data.Xml.Dom.XmlDomImplementation)
* [**Windows.Data.Xml.Dom.XmlElement**](/uwp/api/Windows.Data.Xml.Dom.XmlElement)
* [**Windows.Data.Xml.Dom.XmlEntityReference**](/uwp/api/Windows.Data.Xml.Dom.XmlEntityReference)
* [**Windows.Data.Xml.Dom.XmlLoadSettings**](/uwp/api/Windows.Data.Xml.Dom.XmlLoadSettings)
* [**Windows.Data.Xml.Dom.XmlNamedNodeMap**](/uwp/api/Windows.Data.Xml.Dom.XmlNamedNodeMap)
* [**Windows.Data.Xml.Dom.XmlNodeList**](/uwp/api/Windows.Data.Xml.Dom.XmlNodeList)
* [**Windows.Data.Xml.Dom.XmlProcessingInstruction**](/uwp/api/Windows.Data.Xml.Dom.XmlProcessingInstruction)
* [**Windows.Data.Xml.Dom.XmlText**](/uwp/api/Windows.Data.Xml.Dom.XmlText)
* [**Windows.Data.Xml.Xsl.XsltProcessor**](/uwp/api/Windows.Data.Xml.Xsl.XsltProcessor)
* [**Windows.Devices.Input.KeyboardCapabilities**](/uwp/api/Windows.Devices.Input.KeyboardCapabilities)
* [**Windows.Devices.Input.MouseCapabilities**](/uwp/api/Windows.Devices.Input.MouseCapabilities)
* [**Windows.Devices.Input.MouseDevice**](/uwp/api/Windows.Devices.Input.MouseDevice)
* [**Windows.Devices.Input.MouseEventArgs**](/uwp/api/Windows.Devices.Input.MouseEventArgs)
* [**Windows.Devices.Input.PointerDevice**](/uwp/api/Windows.Devices.Input.PointerDevice)
* [**Windows.Devices.Input.TouchCapabilities**](/uwp/api/Windows.Devices.Input.TouchCapabilities)
* [**Windows.Devices.Lights.Lamp**](/uwp/api/Windows.Devices.Lights.Lamp)
* [**Windows.Devices.Lights.LampAvailabilityChangedEventArgs**](/uwp/api/Windows.Devices.Lights.LampAvailabilityChangedEventArgs)
* [**Windows.Devices.Perception.Provider.PerceptionStartFaceAuthenticationHandler**](/uwp/api/windows.devices.perception.provider.perceptionstartfaceauthenticationhandler)
* [**Windows.Devices.Perception.Provider.PerceptionStopFaceAuthenticationHandler**](/uwp/api/windows.devices.perception.provider.perceptionstopfaceauthenticationhandler)
* [**Windows.Devices.PointOfService.MagneticStripeReader**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReader)
* [**Windows.Devices.PointOfService.MagneticStripeReaderAamvaCardDataReceivedEventArgs**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderAamvaCardDataReceivedEventArgs)
* [**Windows.Devices.PointOfService.MagneticStripeReaderBankCardDataReceivedEventArgs**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderBankCardDataReceivedEventArgs)
* [**Windows.Devices.PointOfService.MagneticStripeReaderCapabilities**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderCapabilities)
* [**Windows.Devices.PointOfService.MagneticStripeReaderCardTypes**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderCardTypes)
* [**Windows.Devices.PointOfService.MagneticStripeReaderEncryptionAlgorithms**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderEncryptionAlgorithms)
* [**Windows.Devices.PointOfService.MagneticStripeReaderErrorOccurredEventArgs**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderErrorOccurredEventArgs)
* [**Windows.Devices.PointOfService.MagneticStripeReaderReport**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderReport)
* [**Windows.Devices.PointOfService.MagneticStripeReaderStatusUpdatedEventArgs**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderStatusUpdatedEventArgs)
* [**Windows.Devices.PointOfService.MagneticStripeReaderTrackData**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderTrackData)
* [**Windows.Devices.PointOfService.MagneticStripeReaderVendorSpecificCardDataReceivedEventArgs**](/uwp/api/Windows.Devices.PointOfService.MagneticStripeReaderVendorSpecificCardDataReceivedEventArgs)
* [**Windows.Devices.Portable.ServiceDevice**](/uwp/api/Windows.Devices.Portable.ServiceDevice)
* [**Windows.Devices.Portable.StorageDevice**](/uwp/api/Windows.Devices.Portable.StorageDevice)
* [**Windows.Devices.Printers.Print3DDevice**](/uwp/api/Windows.Devices.Printers.Print3DDevice)
* [**Windows.Devices.Printers.PrintSchema**](/uwp/api/Windows.Devices.Printers.PrintSchema)
* [**Windows.Devices.SmartCards.SmartCard**](/uwp/api/Windows.Devices.SmartCards.SmartCard)
* [**Windows.Devices.SmartCards.SmartCardConnection**](/uwp/api/Windows.Devices.SmartCards.SmartCardConnection)
* [**Windows.Devices.SmartCards.SmartCardReader**](/uwp/api/Windows.Devices.SmartCards.SmartCardReader)
* [**Windows.Foundation.AsyncActionCompletedHandler**](/uwp/api/windows.foundation.asyncactioncompletedhandler)
* [**Windows.Foundation.AsyncActionProgressHandler\<TProgress\>**](/uwp/api/windows.foundation.asyncactionprogresshandler-1)
* [**Windows.Foundation.AsyncActionWithProgressCompletedHandler\<TProgress\>**](/uwp/api/windows.foundation.asyncactionwithprogresscompletedhandler-1)
* [**Windows.Foundation.AsyncOperationCompletedHandler\<TProgress\>**](/uwp/api/windows.foundation.asyncoperationcompletedhandler-1)
* [**Windows.Foundation.Collections.VectorChangedEventHandler\<T\>**](/uwp/api/windows.foundation.collections.vectorchangedeventhandler-1)
* [**Windows.Foundation.DeferralCompletedHandler**](/uwp/api/windows.foundation.deferralcompletedhandler)
* [**Windows.Foundation.Diagnostics.FileLoggingSession**](/uwp/api/Windows.Foundation.Diagnostics.FileLoggingSession)
* [**Windows.Foundation.Diagnostics.LogFileGeneratedEventArgs**](/uwp/api/Windows.Foundation.Diagnostics.LogFileGeneratedEventArgs)
* [**Windows.Foundation.Diagnostics.LoggingActivity**](/uwp/api/Windows.Foundation.Diagnostics.LoggingActivity)
* [**Windows.Foundation.Diagnostics.LoggingChannel**](/uwp/api/Windows.Foundation.Diagnostics.LoggingChannel)
* [**Windows.Foundation.Diagnostics.LoggingChannelOptions**](/uwp/api/Windows.Foundation.Diagnostics.LoggingChannelOptions)
* [**Windows.Foundation.Diagnostics.LoggingFields**](/uwp/api/Windows.Foundation.Diagnostics.LoggingFields)
* [**Windows.Foundation.Diagnostics.LoggingOptions**](/uwp/api/Windows.Foundation.Diagnostics.LoggingOptions)
* [**Windows.Foundation.Diagnostics.LoggingSession**](/uwp/api/Windows.Foundation.Diagnostics.LoggingSession)
* [**Windows.Foundation.EventHandler\<T\>**](/uwp/api/windows.foundation.eventhandler-1)
* [**Windows.Foundation.MemoryBuffer**](/uwp/api/Windows.Foundation.MemoryBuffer)
* [**Windows.Globalization.ApplicationLanguages**](/uwp/api/Windows.Globalization.ApplicationLanguages)
* [**Windows.Globalization.JapanesePhoneme**](/uwp/api/Windows.Globalization.JapanesePhoneme)
* [**Windows.Globalization.JapanesePhoneticAnalyzer**](/uwp/api/Windows.Globalization.JapanesePhoneticAnalyzer)
* [**Windows.Globalization.PhoneNumberFormatting.PhoneNumberFormatter**](/uwp/api/Windows.Globalization.PhoneNumberFormatting.PhoneNumberFormatter)
* [**Windows.Globalization.PhoneNumberFormatting.PhoneNumberInfo**](/uwp/api/Windows.Globalization.PhoneNumberFormatting.PhoneNumberInfo)
* [**Windows.Graphics.Imaging.BitmapBuffer**](/uwp/api/Windows.Graphics.Imaging.BitmapBuffer)
* [**Windows.Graphics.Imaging.BitmapCodecInformation**](/uwp/api/Windows.Graphics.Imaging.BitmapCodecInformation)
* [**Windows.Graphics.Imaging.BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder)
* [**Windows.Graphics.Imaging.BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder)
* [**Windows.Graphics.Imaging.BitmapFrame**](/uwp/api/Windows.Graphics.Imaging.BitmapFrame)
* [**Windows.Graphics.Imaging.BitmapProperties**](/uwp/api/Windows.Graphics.Imaging.BitmapProperties)
* [**Windows.Graphics.Imaging.BitmapPropertiesView**](/uwp/api/Windows.Graphics.Imaging.BitmapPropertiesView)
* [**Windows.Graphics.Imaging.BitmapPropertySet**](/uwp/api/Windows.Graphics.Imaging.BitmapPropertySet)
* [**Windows.Graphics.Imaging.BitmapTransform**](/uwp/api/Windows.Graphics.Imaging.BitmapTransform)
* [**Windows.Graphics.Imaging.BitmapTypedValue**](/uwp/api/Windows.Graphics.Imaging.BitmapTypedValue)
* [**Windows.Graphics.Imaging.ImageStream**](/uwp/api/Windows.Graphics.Imaging.ImageStream)
* [**Windows.Graphics.Imaging.PixelDataProvider**](/uwp/api/Windows.Graphics.Imaging.PixelDataProvider)
* [**Windows.Graphics.Imaging.SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap)
* [**Windows.Graphics.Printing3D.Print3DTaskRequestedEventArgs**](/uwp/api/Windows.Graphics.Printing3D.Print3DTaskRequestedEventArgs)
* [**Windows.Graphics.Printing3D.Print3DTaskSourceRequestedHandler**](/uwp/api/windows.graphics.printing3d.print3dtasksourcerequestedhandler)
* [**Windows.Graphics.Printing3D.Printing3D3MFPackage**](/uwp/api/Windows.Graphics.Printing3D.Printing3D3MFPackage)
* [**Windows.Graphics.Printing3D.Printing3DBaseMaterial**](/uwp/api/Windows.Graphics.Printing3D.Printing3DBaseMaterial)
* [**Windows.Graphics.Printing3D.Printing3DBaseMaterialGroup**](/uwp/api/Windows.Graphics.Printing3D.Printing3DBaseMaterialGroup)
* [**Windows.Graphics.Printing3D.Printing3DColorMaterial**](/uwp/api/Windows.Graphics.Printing3D.Printing3DColorMaterial)
* [**Windows.Graphics.Printing3D.Printing3DColorMaterialGroup**](/uwp/api/Windows.Graphics.Printing3D.Printing3DColorMaterialGroup)
* [**Windows.Graphics.Printing3D.Printing3DComponent**](/uwp/api/Windows.Graphics.Printing3D.Printing3DComponent)
* [**Windows.Graphics.Printing3D.Printing3DComponentWithMatrix**](/uwp/api/Windows.Graphics.Printing3D.Printing3DComponentWithMatrix)
* [**Windows.Graphics.Printing3D.Printing3DCompositeMaterial**](/uwp/api/Windows.Graphics.Printing3D.Printing3DCompositeMaterial)
* [**Windows.Graphics.Printing3D.Printing3DCompositeMaterialGroup**](/uwp/api/Windows.Graphics.Printing3D.Printing3DCompositeMaterialGroup)
* [**Windows.Graphics.Printing3D.Printing3DFaceReductionOptions**](/uwp/api/Windows.Graphics.Printing3D.Printing3DFaceReductionOptions)
* [**Windows.Graphics.Printing3D.Printing3DMaterial**](/uwp/api/Windows.Graphics.Printing3D.Printing3DMaterial)
* [**Windows.Graphics.Printing3D.Printing3DMesh**](/uwp/api/Windows.Graphics.Printing3D.Printing3DMesh)
* [**Windows.Graphics.Printing3D.Printing3DMeshVerificationResult**](/uwp/api/Windows.Graphics.Printing3D.Printing3DMeshVerificationResult)
* [**Windows.Graphics.Printing3D.Printing3DModel**](/uwp/api/Windows.Graphics.Printing3D.Printing3DModel)
* [**Windows.Graphics.Printing3D.Printing3DModelTexture**](/uwp/api/Windows.Graphics.Printing3D.Printing3DModelTexture)
* [**Windows.Graphics.Printing3D.Printing3DMultiplePropertyMaterial**](/uwp/api/Windows.Graphics.Printing3D.Printing3DMultiplePropertyMaterial)
* [**Windows.Graphics.Printing3D.Printing3DMultiplePropertyMaterialGroup**](/uwp/api/Windows.Graphics.Printing3D.Printing3DMultiplePropertyMaterialGroup)
* [**Windows.Graphics.Printing3D.Printing3DTexture2CoordMaterial**](/uwp/api/Windows.Graphics.Printing3D.Printing3DTexture2CoordMaterial)
* [**Windows.Graphics.Printing3D.Printing3DTexture2CoordMaterialGroup**](/uwp/api/Windows.Graphics.Printing3D.Printing3DTexture2CoordMaterialGroup)
* [**Windows.Graphics.Printing3D.Printing3DTextureResource**](/uwp/api/Windows.Graphics.Printing3D.Printing3DTextureResource)
* [**Windows.Management.Core.ApplicationDataManager**](/uwp/api/Windows.Management.Core.ApplicationDataManager)
* [**Windows.Management.Deployment.DeploymentResult**](/uwp/api/Windows.Management.Deployment.DeploymentResult)
* [**Windows.Management.Deployment.PackageManager**](/uwp/api/Windows.Management.Deployment.PackageManager)
* [**Windows.Management.Deployment.PackageUserInformation**](/uwp/api/Windows.Management.Deployment.PackageUserInformation)
* [**Windows.Management.Deployment.PackageVolume**](/uwp/api/Windows.Management.Deployment.PackageVolume)
* [**Windows.Management.Workplace.MdmPolicy**](/uwp/api/Windows.Management.Workplace.MdmPolicy)
* [**Windows.Management.Workplace.WorkplaceSettings**](/uwp/api/Windows.Management.Workplace.WorkplaceSettings)
* [**Windows.Media.AudioBuffer**](/uwp/api/Windows.Media.AudioBuffer)
* [**Windows.Media.Capture.AdvancedCapturedPhoto**](/uwp/api/Windows.Media.Capture.AdvancedCapturedPhoto)
* [**Windows.Media.Capture.AppCaptureAlternateShortcutKeys**](/uwp/api/Windows.Media.Capture.AppCaptureAlternateShortcutKeys)
* [**Windows.Media.Capture.AppCaptureManager**](/uwp/api/Windows.Media.Capture.AppCaptureManager)
* [**Windows.Media.Capture.AppCaptureSettings**](/uwp/api/Windows.Media.Capture.AppCaptureSettings)
* [**Windows.Media.Capture.CapturedFrame**](/uwp/api/Windows.Media.Capture.CapturedFrame)
* [**Windows.Media.Capture.MediaCaptureFailedEventArgs**](/uwp/api/Windows.Media.Capture.MediaCaptureFailedEventArgs)
* [**Windows.Media.Capture.MediaCaptureFailedEventHandler**](/uwp/api/windows.media.capture.mediacapturefailedeventhandler)
* [**Windows.Media.Capture.MediaCapturePauseResult**](/uwp/api/Windows.Media.Capture.MediaCapturePauseResult)
* [**Windows.Media.Capture.MediaCaptureStopResult**](/uwp/api/Windows.Media.Capture.MediaCaptureStopResult)
* [**Windows.Media.Capture.OptionalReferencePhotoCapturedEventArgs**](/uwp/api/Windows.Media.Capture.OptionalReferencePhotoCapturedEventArgs)
* [**Windows.Media.Capture.RecordLimitationExceededEventHandler**](/uwp/api/windows.media.capture.recordlimitationexceededeventhandler)
* [**Windows.Media.ClosedCaptioning.ClosedCaptionProperties**](/uwp/api/Windows.Media.ClosedCaptioning.ClosedCaptionProperties)
* [**Windows.Media.Devices.DefaultAudioCaptureDeviceChangedEventArgs**](/uwp/api/Windows.Media.Devices.DefaultAudioCaptureDeviceChangedEventArgs)
* [**Windows.Media.Devices.DefaultAudioRenderDeviceChangedEventArgs**](/uwp/api/Windows.Media.Devices.DefaultAudioRenderDeviceChangedEventArgs)
* [**Windows.Media.Devices.MediaDevice**](/uwp/api/Windows.Media.Devices.MediaDevice)
* [**Windows.Media.DialProtocol.DialApp**](/uwp/api/Windows.Media.DialProtocol.DialApp)
* [**Windows.Media.DialProtocol.DialAppStateDetails**](/uwp/api/Windows.Media.DialProtocol.DialAppStateDetails)
* [**Windows.Media.DialProtocol.DialDevice**](/uwp/api/Windows.Media.DialProtocol.DialDevice)
* [**Windows.Media.FaceAnalysis.DetectedFace**](/uwp/api/Windows.Media.FaceAnalysis.DetectedFace)
* [**Windows.Media.FaceAnalysis.FaceDetector**](/uwp/api/Windows.Media.FaceAnalysis.FaceDetector)
* [**Windows.Media.FaceAnalysis.FaceTracker**](/uwp/api/Windows.Media.FaceAnalysis.FaceTracker)
* [**Windows.Media.MediaExtensionManager**](/uwp/api/Windows.Media.MediaExtensionManager)
* [**Windows.Media.MediaProperties.H264ProfileIds**](/uwp/api/Windows.Media.MediaProperties.H264ProfileIds)
* [**Windows.Media.MediaProperties.MediaEncodingSubtypes**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingSubtypes)
* [**Windows.Media.MediaProperties.Mpeg2ProfileIds**](/uwp/api/Windows.Media.MediaProperties.Mpeg2ProfileIds)
* [**Windows.Media.Ocr.OcrEngine**](/uwp/api/Windows.Media.Ocr.OcrEngine)
* [**Windows.Media.Ocr.OcrLine**](/uwp/api/Windows.Media.Ocr.OcrLine)
* [**Windows.Media.Ocr.OcrResult**](/uwp/api/Windows.Media.Ocr.OcrResult)
* [**Windows.Media.Ocr.OcrWord**](/uwp/api/Windows.Media.Ocr.OcrWord)
* [**Windows.Media.Playback.PlaybackMediaMarker**](/uwp/api/Windows.Media.Playback.PlaybackMediaMarker)
* [**Windows.Media.Playback.PlaybackMediaMarkerReachedEventArgs**](/uwp/api/Windows.Media.Playback.PlaybackMediaMarkerReachedEventArgs)
* [**Windows.Media.Playback.PlaybackMediaMarkerSequence**](/uwp/api/Windows.Media.Playback.PlaybackMediaMarkerSequence)
* [**Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs**](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs)
* [**Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs**](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs)
* [**Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession**](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionGrammarFileConstraint**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionGrammarFileConstraint)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionHypothesis**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionHypothesis)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionHypothesisGeneratedEventArgs**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionHypothesisGeneratedEventArgs)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionQualityDegradingEventArgs**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionQualityDegradingEventArgs)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionResult**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionSemanticInterpretation**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionSemanticInterpretation)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint)
* [**Windows.Media.SpeechRecognition.SpeechRecognitionVoiceCommandDefinitionConstraint**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionVoiceCommandDefinitionConstraint)
* [**Windows.Media.SpeechRecognition.SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer)
* [**Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs)
* [**Windows.Media.SpeechRecognition.SpeechRecognizerTimeouts**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizerTimeouts)
* [**Windows.Media.SpeechRecognition.SpeechRecognizerUIOptions**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizerUIOptions)
* [**Windows.Media.SpeechSynthesis.SpeechSynthesisStream**](/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesisStream)
* [**Windows.Media.SpeechSynthesis.SpeechSynthesizer**](/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer)
* [**Windows.Media.SpeechSynthesis.VoiceInformation**](/uwp/api/Windows.Media.SpeechSynthesis.VoiceInformation)
* [**Windows.Networking.PushNotifications.PushNotificationChannel**](/uwp/api/Windows.Networking.PushNotifications.PushNotificationChannel)
* [**Windows.Networking.PushNotifications.PushNotificationChannelManager**](/uwp/api/Windows.Networking.PushNotifications.PushNotificationChannelManager)
* [**Windows.Networking.PushNotifications.PushNotificationReceivedEventArgs**](/uwp/api/Windows.Networking.PushNotifications.PushNotificationReceivedEventArgs)
* [**Windows.Networking.PushNotifications.RawNotification**](/uwp/api/Windows.Networking.PushNotifications.RawNotification)
* [**Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs**](/uwp/api/Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs)
* [**Windows.Networking.Sockets.MessageWebSocketMessageReceivedEventArgs**](/uwp/api/Windows.Networking.Sockets.MessageWebSocketMessageReceivedEventArgs)
* [**Windows.Services.Maps.Guidance.GuidanceAudioNotificationRequestedEventArgs**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceAudioNotificationRequestedEventArgs)
* [**Windows.Services.Maps.Guidance.GuidanceLaneInfo**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceLaneInfo)
* [**Windows.Services.Maps.Guidance.GuidanceManeuver**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceManeuver)
* [**Windows.Services.Maps.Guidance.GuidanceMapMatchedCoordinate**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceMapMatchedCoordinate)
* [**Windows.Services.Maps.Guidance.GuidanceNavigator**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceNavigator)
* [**Windows.Services.Maps.Guidance.GuidanceReroutedEventArgs**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceReroutedEventArgs)
* [**Windows.Services.Maps.Guidance.GuidanceRoadSegment**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceRoadSegment)
* [**Windows.Services.Maps.Guidance.GuidanceRoadSignpost**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceRoadSignpost)
* [**Windows.Services.Maps.Guidance.GuidanceRoute**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceRoute)
* [**Windows.Services.Maps.Guidance.GuidanceTelemetryCollector**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceTelemetryCollector)
* [**Windows.Services.Maps.Guidance.GuidanceUpdatedEventArgs**](/uwp/api/Windows.Services.Maps.Guidance.GuidanceUpdatedEventArgs)
* [**Windows.Services.Maps.LocalSearch.LocalCategories**](/uwp/api/Windows.Services.Maps.LocalSearch.LocalCategories)
* [**Windows.Services.Maps.LocalSearch.LocalLocation**](/uwp/api/Windows.Services.Maps.LocalSearch.LocalLocation)
* [**Windows.Services.Maps.LocalSearch.LocalLocationFinder**](/uwp/api/Windows.Services.Maps.LocalSearch.LocalLocationFinder)
* [**Windows.Services.Maps.LocalSearch.LocalLocationFinderResult**](/uwp/api/Windows.Services.Maps.LocalSearch.LocalLocationFinderResult)
* [**Windows.Services.Maps.LocalSearch.LocalLocationHoursOfOperationItem**](/uwp/api/Windows.Services.Maps.LocalSearch.LocalLocationHoursOfOperationItem)
* [**Windows.Services.Maps.LocalSearch.LocalLocationRatingInfo**](/uwp/api/Windows.Services.Maps.LocalSearch.LocalLocationRatingInfo)
* [**Windows.Services.Maps.MapAddress**](/uwp/api/Windows.Services.Maps.MapAddress)
* [**Windows.Services.Maps.MapLocation**](/uwp/api/Windows.Services.Maps.MapLocation)
* [**Windows.Services.Maps.MapLocationFinder**](/uwp/api/Windows.Services.Maps.MapLocationFinder)
* [**Windows.Services.Maps.MapLocationFinderResult**](/uwp/api/Windows.Services.Maps.MapLocationFinderResult)
* [**Windows.Services.Maps.MapManager**](/uwp/api/Windows.Services.Maps.MapManager)
* [**Windows.Services.Maps.MapRoute**](/uwp/api/Windows.Services.Maps.MapRoute)
* [**Windows.Services.Maps.MapRouteDrivingOptions**](/uwp/api/Windows.Services.Maps.MapRouteDrivingOptions)
* [**Windows.Services.Maps.MapRouteFinder**](/uwp/api/Windows.Services.Maps.MapRouteFinder)
* [**Windows.Services.Maps.MapRouteFinderResult**](/uwp/api/Windows.Services.Maps.MapRouteFinderResult)
* [**Windows.Services.Maps.MapRouteLeg**](/uwp/api/Windows.Services.Maps.MapRouteLeg)
* [**Windows.Services.Maps.MapRouteManeuver**](/uwp/api/Windows.Services.Maps.MapRouteManeuver)
* [**Windows.Services.Maps.MapService**](/uwp/api/Windows.Services.Maps.MapService)
* [**Windows.Services.Store.StoreAcquireLicenseResult**](/uwp/api/Windows.Services.Store.StoreAcquireLicenseResult)
* [**Windows.Services.Store.StoreAppLicense**](/uwp/api/Windows.Services.Store.StoreAppLicense)
* [**Windows.Services.Store.StoreAvailability**](/uwp/api/Windows.Services.Store.StoreAvailability)
* [**Windows.Services.Store.StoreCollectionData**](/uwp/api/Windows.Services.Store.StoreCollectionData)
* [**Windows.Services.Store.StoreConsumableResult**](/uwp/api/Windows.Services.Store.StoreConsumableResult)
* [**Windows.Services.Store.StoreContext**](/uwp/api/Windows.Services.Store.StoreContext)
* [**Windows.Services.Store.StoreImage**](/uwp/api/Windows.Services.Store.StoreImage)
* [**Windows.Services.Store.StoreLicense**](/uwp/api/Windows.Services.Store.StoreLicense)
* [**Windows.Services.Store.StorePackageLicense**](/uwp/api/Windows.Services.Store.StorePackageLicense)
* [**Windows.Services.Store.StorePackageUpdate**](/uwp/api/Windows.Services.Store.StorePackageUpdate)
* [**Windows.Services.Store.StorePackageUpdateResult**](/uwp/api/Windows.Services.Store.StorePackageUpdateResult)
* [**Windows.Services.Store.StorePrice**](/uwp/api/Windows.Services.Store.StorePrice)
* [**Windows.Services.Store.StoreProduct**](/uwp/api/Windows.Services.Store.StoreProduct)
* [**Windows.Services.Store.StoreProductPagedQueryResult**](/uwp/api/Windows.Services.Store.StoreProductPagedQueryResult)
* [**Windows.Services.Store.StoreProductQueryResult**](/uwp/api/Windows.Services.Store.StoreProductQueryResult)
* [**Windows.Services.Store.StoreProductResult**](/uwp/api/Windows.Services.Store.StoreProductResult)
* [**Windows.Services.Store.StorePurchaseProperties**](/uwp/api/Windows.Services.Store.StorePurchaseProperties)
* [**Windows.Services.Store.StorePurchaseResult**](/uwp/api/Windows.Services.Store.StorePurchaseResult)
* [**Windows.Services.Store.StoreRequestHelper**](/uwp/api/Windows.Services.Store.StoreRequestHelper)
* [**Windows.Services.Store.StoreSendRequestResult**](/uwp/api/Windows.Services.Store.StoreSendRequestResult)
* [**Windows.Services.Store.StoreSku**](/uwp/api/Windows.Services.Store.StoreSku)
* [**Windows.Services.Store.StoreVideo**](/uwp/api/Windows.Services.Store.StoreVideo)
* [**Windows.Storage.AccessCache.StorageApplicationPermissions**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions)
* [**Windows.Storage.ApplicationData**](/uwp/api/windows.storage.applicationdata)
* [**Windows.Storage.ApplicationDataSetVersionHandler**](/uwp/api/windows.storage.applicationdatasetversionhandler)
* [**Windows.Storage.CachedFileManager**](/uwp/api/Windows.Storage.CachedFileManager)
* [**Windows.Storage.DownloadsFolder**](/uwp/api/Windows.Storage.DownloadsFolder)
* [**Windows.Storage.FileIO**](/uwp/api/Windows.Storage.FileIO)
* [**Windows.Storage.FileProperties.BasicProperties**](/uwp/api/Windows.Storage.FileProperties.BasicProperties)
* [**Windows.Storage.FileProperties.DocumentProperties**](/uwp/api/Windows.Storage.FileProperties.DocumentProperties)
* [**Windows.Storage.FileProperties.ImageProperties**](/uwp/api/Windows.Storage.FileProperties.ImageProperties)
* [**Windows.Storage.FileProperties.MusicProperties**](/uwp/api/Windows.Storage.FileProperties.MusicProperties)
* [**Windows.Storage.FileProperties.StorageItemContentProperties**](/uwp/api/Windows.Storage.FileProperties.StorageItemContentProperties)
* [**Windows.Storage.FileProperties.StorageItemThumbnail**](/uwp/api/Windows.Storage.FileProperties.StorageItemThumbnail)
* [**Windows.Storage.FileProperties.VideoProperties**](/uwp/api/Windows.Storage.FileProperties.VideoProperties)
* [**Windows.Storage.KnownFolders**](/uwp/api/Windows.Storage.KnownFolders)
* [**Windows.Storage.PathIO**](/uwp/api/Windows.Storage.PathIO)
* [**Windows.Storage.StorageFile**](/uwp/api/Windows.Storage.StorageFile)
* [**Windows.Storage.StorageFolder**](/uwp/api/Windows.Storage.StorageFolder)
* [**Windows.Storage.StorageLibrary**](/uwp/api/Windows.Storage.StorageLibrary)
* [**Windows.Storage.StorageProvider**](/uwp/api/Windows.Storage.StorageProvider)
* [**Windows.Storage.StorageStreamTransaction**](/uwp/api/Windows.Storage.StorageStreamTransaction)
* [**Windows.Storage.StreamedFileDataRequest**](/uwp/api/Windows.Storage.StreamedFileDataRequest)
* [**Windows.Storage.StreamedFileDataRequestedHandler**](/uwp/api/windows.storage.streamedfiledatarequestedhandler)
* [**Windows.Storage.Streams.Buffer**](/uwp/api/Windows.Storage.Streams.Buffer)
* [**Windows.Storage.Streams.DataReader**](/uwp/api/Windows.Storage.Streams.DataReader)
* [**Windows.Storage.Streams.DataReaderLoadOperation**](/uwp/api/Windows.Storage.Streams.DataReaderLoadOperation)
* [**Windows.Storage.Streams.DataWriter**](/uwp/api/Windows.Storage.Streams.DataWriter)
* [**Windows.Storage.Streams.DataWriterStoreOperation**](/uwp/api/Windows.Storage.Streams.DataWriterStoreOperation)
* [**Windows.Storage.Streams.FileInputStream**](/uwp/api/Windows.Storage.Streams.FileInputStream)
* [**Windows.Storage.Streams.FileOutputStream**](/uwp/api/Windows.Storage.Streams.FileOutputStream)
* [**Windows.Storage.Streams.FileRandomAccessStream**](/uwp/api/Windows.Storage.Streams.FileRandomAccessStream)
* [**Windows.Storage.Streams.InMemoryRandomAccessStream**](/uwp/api/Windows.Storage.Streams.InMemoryRandomAccessStream)
* [**Windows.Storage.Streams.InputStreamOverStream**](/uwp/api/Windows.Storage.Streams.InputStreamOverStream)
* [**Windows.Storage.Streams.OutputStreamOverStream**](/uwp/api/Windows.Storage.Streams.OutputStreamOverStream)
* [**Windows.Storage.Streams.RandomAccessStream**](/uwp/api/Windows.Storage.Streams.RandomAccessStream)
* [**Windows.Storage.Streams.RandomAccessStreamOverStream**](/uwp/api/Windows.Storage.Streams.RandomAccessStreamOverStream)
* [**Windows.Storage.Streams.RandomAccessStreamReference**](/uwp/api/Windows.Storage.Streams.RandomAccessStreamReference)
* [**Windows.Storage.SystemAudioProperties**](/uwp/api/Windows.Storage.SystemAudioProperties)
* [**Windows.Storage.SystemGPSProperties**](/uwp/api/Windows.Storage.SystemGPSProperties)
* [**Windows.Storage.SystemImageProperties**](/uwp/api/Windows.Storage.SystemImageProperties)
* [**Windows.Storage.SystemMediaProperties**](/uwp/api/Windows.Storage.SystemMediaProperties)
* [**Windows.Storage.SystemMusicProperties**](/uwp/api/Windows.Storage.SystemMusicProperties)
* [**Windows.Storage.SystemPhotoProperties**](/uwp/api/Windows.Storage.SystemPhotoProperties)
* [**Windows.Storage.SystemProperties**](/uwp/api/Windows.Storage.SystemProperties)
* [**Windows.Storage.SystemVideoProperties**](/uwp/api/Windows.Storage.SystemVideoProperties)
* [**Windows.System.Diagnostics.ProcessCpuUsage**](/uwp/api/Windows.System.Diagnostics.ProcessCpuUsage)
* [**Windows.System.Diagnostics.ProcessCpuUsageReport**](/uwp/api/Windows.System.Diagnostics.ProcessCpuUsageReport)
* [**Windows.System.Diagnostics.ProcessDiagnosticInfo**](/uwp/api/Windows.System.Diagnostics.ProcessDiagnosticInfo)
* [**Windows.System.Diagnostics.ProcessDiskUsage**](/uwp/api/Windows.System.Diagnostics.ProcessDiskUsage)
* [**Windows.System.Diagnostics.ProcessDiskUsageReport**](/uwp/api/Windows.System.Diagnostics.ProcessDiskUsageReport)
* [**Windows.System.Diagnostics.ProcessMemoryUsage**](/uwp/api/Windows.System.Diagnostics.ProcessMemoryUsage)
* [**Windows.System.Diagnostics.ProcessMemoryUsageReport**](/uwp/api/Windows.System.Diagnostics.ProcessMemoryUsageReport)
* [**Windows.System.Profile.AnalyticsInfo**](/uwp/api/Windows.System.Profile.AnalyticsInfo)
* [**Windows.System.Profile.AnalyticsVersionInfo**](/uwp/api/Windows.System.Profile.AnalyticsVersionInfo)
* [**Windows.System.Threading.Core.PreallocatedWorkItem**](/uwp/api/Windows.System.Threading.Core.PreallocatedWorkItem)
* [**Windows.System.Threading.Core.SignalHandler**](/uwp/api/windows.system.threading.core.signalhandler)
* [**Windows.System.Threading.Core.SignalNotifier**](/uwp/api/Windows.System.Threading.Core.SignalNotifier)
* [**Windows.System.Threading.ThreadPool**](/uwp/api/Windows.System.Threading.ThreadPool)
* [**Windows.System.Threading.ThreadPoolTimer**](/uwp/api/Windows.System.Threading.ThreadPoolTimer)
* [**Windows.System.Threading.TimerDestroyedHandler**](/uwp/api/windows.system.threading.timerdestroyedhandler)
* [**Windows.System.Threading.TimerElapsedHandler**](/uwp/api/windows.system.threading.timerelapsedhandler)
* [**Windows.System.Threading.WorkItemHandler**](/uwp/api/windows.system.threading.workitemhandler)
* [**Windows.System.TimeZoneSettings**](/uwp/api/Windows.System.TimeZoneSettings)
* [**Windows.UI.Notifications.BadgeNotification**](/uwp/api/Windows.UI.Notifications.BadgeNotification)
* [**Windows.UI.Notifications.BadgeUpdateManager**](/uwp/api/Windows.UI.Notifications.BadgeUpdateManager)
* [**Windows.UI.Notifications.BadgeUpdater**](/uwp/api/Windows.UI.Notifications.BadgeUpdater)
* [**Windows.UI.Notifications.ScheduledTileNotification**](/uwp/api/Windows.UI.Notifications.ScheduledTileNotification)
* [**Windows.UI.Notifications.ScheduledToastNotification**](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)
* [**Windows.UI.Notifications.TileNotification**](/uwp/api/Windows.UI.Notifications.TileNotification)
* [**Windows.UI.Notifications.TileUpdateManager**](/uwp/api/Windows.UI.Notifications.TileUpdateManager)
* [**Windows.UI.Notifications.TileUpdater**](/uwp/api/Windows.UI.Notifications.TileUpdater)
* [**Windows.UI.Notifications.ToastNotificationHistory**](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory)
* [**Windows.UI.StartScreen.JumpList**](/uwp/api/Windows.UI.StartScreen.JumpList)
* [**Windows.UI.StartScreen.JumpListItem**](/uwp/api/Windows.UI.StartScreen.JumpListItem)

In addition, when called from a desktop app that doesn't have package identity, the [**AdaptiveMediaSource.CreateFromUriAsync**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource.CreateFromUriAsync) methods don't support the `ms-appx` and `ms-resource` URI formats.
