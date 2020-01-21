---
description: This topic provides a comprehensive mapping of Windows Phone Silverlight APIs to their Universal Windows Platform (UWP) equivalents.
title: WPSL to UWP namespace and class mappings
ms.assetid: 33f06706-4790-48f3-a2e4-ebef9ddb61a4
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Windows Phone Silverlight to UWP API mappings


This topic provides a comprehensive mapping of Windows Phone Silverlight APIs to their Universal Windows Platform (UWP) equivalents. There is generally not a one-to-one mapping of functionality, though: either platform may have more or less functionality than its counterpart in a namespace or class.

The mapping table will help you when you're working in a UWP project and you're re-using source code from a Windows Phone Silverlight project. There are differences in the names of namespaces and classes (including UI controls) between the two platforms. In many cases, it's as easy as changing a namespace name and then your code will compile. Sometimes, a class or API name has changed as well as the namespace name. Other times, the mapping takes a bit more work, and in rare cases requires a change in approach.

**How to use the table:  ** First, search for the name of the class you're using. Classes are listed whenever the mapping is more complicated than simply changing the namespace name. If your class is not listed, then the mapping is just a namespace change. So, find your class's namespace name and you'll find the equivalent UWP namespace name. Your class will be in that namespace. If your namespace is not listed, then its name has not changed.

**Note**  Windows 10 supports much more of the .NET Framework than a Windows Phone Store app does. For example, Windows 10 has several System.ServiceModel.\* namespaces as well as System.Net, System.Net.NetworkInformation, and System.Net.Sockets.
Also, in a Windows 10 app, you will benefit from .NET Native, which an ahead-of-time compilation technology that converts MSIL into natively-runnable machine code. .NET Native apps start faster, use less memory, and use less battery than their MSIL counterparts.

| Windows Phone Silverlight | Windows Runtime |
| ------------------------- | --------------- |
| Advertising | |
| **Microsoft.Advertising.Mobile.UI.AdControl** class | [AdControl](https://docs.microsoft.com/windows/uwp/monetize/display-ads-using-the-microsoft-advertising-libraries) class |
| Alarms, reminders, and background agents | |
| **Microsoft.Phone.BackgroundAgent** class | [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) class |
| **Microsoft.Phone.Scheduler** namespace | [**Windows.ApplicationModel.Background**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background) namespace |
| **Microsoft.Phone.Scheduler.Alarm** class | [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) and [**ToastNotificationManager**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.ToastNotificationManager) classes |
| **Microsoft.Phone.Scheduler.PeriodicTask**, **ScheduledAction**, **ScheduledActionService**, **ScheduledTask** , **ScheduledTaskAgent** classes | [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) class |
| **Microsoft.Phone.Scheduler.Reminder** class | [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) and [**ToastNotificationManager**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.ToastNotificationManager) classes |
| **Microsoft.Phone.PictureDecoder** class | [**BitmapDecoder**](https://docs.microsoft.com/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) class |
| **Microsoft.Phone.BackgroundAudio** namespace | [**Windows.Media.Playback**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback) namespace |
| **Microsoft.Phone.BackgroundTransfer** namespace | [**Windows.Networking.BackgroundTransfer**](https://docs.microsoft.com/uwp/api/Windows.Networking.BackgroundTransfer) namespace |
| App model and environment | |
| **System.AppDomain** class | No direct equivalent. See [**Application**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Application), [**CoreApplication**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Core.CoreApplication), classes |
| **System.Environment** class | No direct equivalent |
| **System.ComponentModel.Annotations** class  | No direct equivalent |
| **System.ComponentModel.BackgroundWorker** class | [**ThreadPool**](https://docs.microsoft.com/uwp/api/Windows.System.Threading.ThreadPool) class |
| **System.ComponentModel.DesignerProperties** class | [**DesignMode**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.DesignMode) class |
| **System.Threading.Thread**, **System.Threading.ThreadPool** classes | [**ThreadPool**](https://docs.microsoft.com/uwp/api/Windows.System.Threading.ThreadPool) class |
| (ST = **System.Threading**) <br/> **ST.Thread.MemoryBarrier** method | (ST = **System.Threading**) <br/> **ST.Interlocked.MemoryBarrier** method |
| (ST = **System.Threading**) <br/> **ST.Thread.ManagedThreadId** property | (S = **System**) <br/> **S.Environment.ManagedThreadId** property |
| **System.Threading.Timer** class | [**ThreadPoolTimer**](https://docs.microsoft.com/uwp/api/Windows.System.Threading.ThreadPoolTimer) class |
| (SWT = **System.Windows.Threading**) <br/> **SWT.Dispatcher** class | [**CoreDispatcher**](https://docs.microsoft.com/uwp/api/Windows.UI.Core.CoreDispatcher) class |
| (SWT = **System.Windows.Threading**) <br/> **SWT.DispatcherTimer** class | [**DispatcherTimer**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.DispatcherTimer) class |
| Blend for Visual Studio | |
| (MEDC = **Microsoft.Expression.Drawing.Core**) <br/> **MEDC.GeometryHelper** class | No direct equivalent |
| **Microsoft.Expression.Interactivity** namespace | [Microsoft.Xaml.Interactivity](https://msdn.microsoft.com/library/windows/apps/microsoft.xaml.interactivity.aspx) namespace |
| **Microsoft.Expression.Interactivity.Core** namespace | [Microsoft.Xaml.Interactions.Core](https://msdn.microsoft.com/library/windows/apps/microsoft.xaml.interactions.core.aspx) namespace |
| (MEIC = **Microsoft.Expression.Interactivity.Core**) <br/> **MEIC.ExtendedVisualStateManager** class | No direct equivalent |
| **Microsoft.Expression.Interactivity.Input** namespace | No direct equivalent |
| **Microsoft.Expression.Interactivity.Media** namespace | [Microsoft.Xaml.Interactions.Media](https://msdn.microsoft.com/library/windows/apps/microsoft.xaml.interactions.media.aspx) namespace |
| **Microsoft.Expression.Shapes** namespace | No direct equivalent |
| (MI = **Microsoft.Internal**) <br/> **MI.IManagedFrameworkInternalHelper** interface | No direct equivalent |
| Contact and calendar data | |
| **Microsoft.Phone.UserData** namespace | [**Windows.ApplicationModel.Contacts**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Contacts), [**Windows.ApplicationModel.Appointments**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Appointments) namespaces |
| (MPU = **Microsoft.Phone.UserData**) <br/> **MPU.Account**, **ContactAddress**, **ContactCompanyInformation**, **ContactEmailAddress**, **ContactPhoneNumber** classes | [**Contact**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Contacts.Contact) class |
| (MPU = **Microsoft.Phone.UserData**) <br/> **MPU.Appointments** class | [**AppointmentCalendar**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Appointments.AppointmentCalendar) class |
| (MPU = **Microsoft.Phone.UserData**) <br/> **MPU.Contacts** class | [**ContactStore**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Contacts.ContactStore) class |
| Controls and UI infrastructure | |
| **ControlTiltEffect.TiltEffect** class | Animations from the Windows Runtime animation library are built into the default Styles of the common controls. See [Animation](wpsl-to-uwp-porting-xaml-and-ui.md). |
| **Microsoft.Phone.Controls** namespace | [**Windows.UI.Xaml.Controls**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls) namespace |
| (MPC = **Microsoft.Phone.Controls**) <br/> **MPC.ContextMenu** class | [**PopupMenu**](https://docs.microsoft.com/uwp/api/Windows.UI.Popups.PopupMenu) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.DatePickerPage** class | [**DatePickerFlyout**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.DatePickerFlyout) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.GestureListener** class | [**GestureRecognizer**](https://docs.microsoft.com/uwp/api/Windows.UI.Input.GestureRecognizer) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.LongListSelector** class | [**SemanticZoom**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.SemanticZoom) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.ObscuredEventArgs** class | [**SystemProtection**](https://docs.microsoft.com/uwp/api/Windows.Phone.System.SystemProtection), [**WindowActivatedEventArgs**](https://docs.microsoft.com/uwp/api/Windows.UI.Core.WindowActivatedEventArgs) classes |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.Panorama** class | [**Hub**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Hub) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.PhoneApplicationFrame**,<br/>(SWN = **System.Windows.Navigation**) <br/>**SWN.NavigationService** classes | [**Frame**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Frame) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.PhoneApplicationPage** class | [**Page**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Page) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.TiltEffect** class | [**PointerDownThemeAnimation**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Animation.PointerDownThemeAnimation) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.TimePickerPage** class | [**TimePickerFlyout**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.TimePickerFlyout) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.WebBrowser** class | [**WebView**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.WebView) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.WebBrowserExtensions** class | No direct equivalent |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.WrapPanel** class | No direct equivalent for general layout purposes. [**ItemsWrapGrid**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ItemsWrapGrid) and [**WrapGrid**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.WrapGrid) can be used in the items panel template of an items control. |
| (MPD = **Microsoft.Phone.Data**) <br/>**MPD.Linq** namespace | No direct equivalent |
| (MPD = **Microsoft.Phone.Data**) <br/>**MPD.Linq.Mapping** namespace | No direct equivalent |
| **Microsoft.Phone.Globalization** namespace | No direct equivalent |
| (MPI = **Microsoft.Phone.Info**) <br/>**MPI.DeviceExtendedProperties**, **DeviceStatus** classes | [**EasClientDeviceInformation**](https://docs.microsoft.com/uwp/api/Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation), [**MemoryManager**](https://docs.microsoft.com/uwp/api/Windows.System.MemoryManager) classes. For more details, see [Device status](wpsl-to-uwp-input-and-sensors.md). |
| (MPI = **Microsoft.Phone.Info**) <br/>**MPI.MediaCapabilities** class | No direct equivalent |
| (MPI = **Microsoft.Phone.Info**) <br/>**MPI.UserExtendedProperties** class | [**AdvertisingManager**](https://docs.microsoft.com/uwp/api/Windows.System.UserProfile.AdvertisingManager) class |
| **System.Windows** namespace | [**Windows.UI.Xaml**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml) namespace |
| **System.Windows.Automation** namespace | [**Windows.UI.Xaml.Automation**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Automation) namespace |
| **System.Windows.Controls**, **System.Windows.Input** namespaces | [**Windows.UI.Core**](https://docs.microsoft.com/uwp/api/Windows.UI.Core), [**Windows.UI.Input**](https://docs.microsoft.com/uwp/api/Windows.UI.Input), [**Windows.UI.Xaml.Controls**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls) namespaces |
| **System.Windows.Controls.DrawingSurface**, **DrawingSurfaceBackgroundGrid** classes | [**SwapChainPanel**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) class |
| **System.Windows.Controls.RichTextBox** class | [**RichEditBox**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.RichEditBox) class |
| **System.Windows.Controls.WrapPanel** class | No direct equivalent for general layout purposes. [**ItemsWrapGrid**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ItemsWrapGrid) and [**WrapGrid**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.WrapGrid) can be used in the items panel template of an items control. |
| **System.Windows.Controls.Primitives** namespace | [**Windows.UI.Xaml.Controls.Primitives**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives) namespace |
| **System.Windows.Controls.Shapes** namespace | [**Windows.UI.Xaml.Controls.Shapes**](/uwp/api/Windows.UI.Xaml.Shapes) namespace |
| **System.Windows.Data** namespace | [**Windows.UI.Xaml.Data**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Data) namespace |
| **System.Windows.Documents** namespace | [**Windows.UI.Xaml.Documents**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Documents) namespace |
| **System.Windows.Ink** namespace | No direct equivalent |
| **System.Windows.Markup** namespace | [**Windows.UI.Xaml.Markup**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Markup) namespace | 
| **System.Windows.Navigation** namespace | [**Windows.UI.Xaml.Navigation**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Navigation) namespace |
| **System.Windows.UIElement.Tap** event, **EventHandler&lt;GestureEventArgs&gt;** delegate | [**Tapped**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.tapped) event, [**TappedEventHandler**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.tappedeventhandler) delegate |
| Data and services |  |
| **System.Data.Linq.DataContext** class | No direct equivalent |
| **System.Data.Linq.Mapping.ColumnAttribute** class | No direct equivalent |
| **System.Data.Linq.SqlClient.SqlHelpers** class | No direct equivalent |
| Devices | |
| **Microsoft.Devices**, **Microsoft.Devices.Sensors** namespaces | [**Windows.Devices.Enumeration**](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration), [**Windows.Devices.Enumeration.Pnp**](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration.Pnp), [**Windows.Devices.Input**](https://docs.microsoft.com/uwp/api/Windows.Devices.Input), [**Windows.Devices.Sensors**](https://docs.microsoft.com/uwp/api/Windows.Devices.Sensors) namespaces |
| **Microsoft.Devices.Camera**, **Microsoft.Devices.PhotoCamera** classes | [**MediaCapture**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture) class. Also, [**CameraCaptureUI**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUI) class (Windows only). |
| **Microsoft.Devices.CameraButtons** class | [**HardwareButtons**](https://docs.microsoft.com/uwp/api/Windows.Phone.UI.Input.HardwareButtons) class |
| **Microsoft.Devices.CameraVideoBrushExtensions** class | [**CaptureElement**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) class |
| **Microsoft.Devices.Environment** class | No direct equivalent. As a workaround, use conditional compilation and define a custom symbol. Or you may be able to engineer a workaround using the [IsAttached](https://docs.microsoft.com/dotnet/api/system.diagnostics.debugger.isattached#System_Diagnostics_Debugger_IsAttached) property. |
| **Microsoft.Devices.MediaHistory** class | No direct equivalent |
| **Microsoft.Devices.VibrateController** class | [**VibrationDevice**](https://docs.microsoft.com/uwp/api/Windows.Phone.Devices.Notification.VibrationDevice) class |
| **Microsoft.Devices.Radio.FMRadio** class | No direct equivalent |
| **Microsoft.Devices.Sensors.Accelerometer**, **Compass** classes | In the [**Windows.Devices.Sensors**](https://docs.microsoft.com/uwp/api/Windows.Devices.Sensors) namespace |
| **Microsoft.Devices.Sensors.Gyroscope** class | [**Gyrometer**](https://docs.microsoft.com/uwp/api/Windows.Devices.Sensors.Gyrometer) class |
| **Microsoft.Devices.Sensors.Motion** class | [**Inclinometer**](https://docs.microsoft.com/uwp/api/Windows.Devices.Sensors.Inclinometer) class |
| Globalization | |
| **System.Globalization** namespace | [**Windows.Globalization**](https://docs.microsoft.com/uwp/api/Windows.Globalization) namespace |
| (ST = **System.Threading**) <br/> **ST.Thread.CurrentCulture** property | (SG = **System.Globalization**) <br/> **S.CultureInfo.CurrentCulture** property |
| (ST = **System.Threading**) <br/> **ST.Thread.CurrentUICulture** property | (SG = **System.Globalization**) <br/> **S.CultureInfo.CurrentUICulture** property |
| Graphics and animation | |
| **Microsoft.Xna.Framework.\*** namespaces, [XNA Framework Class Library](https://msdn.microsoft.com/library/bb203940.aspx), [Content Pipeline Class Library](https://msdn.microsoft.com/library/bb195587(v=XNAGameStudio.40).aspx) | No direct equivalent. In general, use [Microsoft DirectX](https://docs.microsoft.com/windows/desktop/directx) with C++. See [Developing games](https://docs.microsoft.com/previous-versions/windows/apps/hh452744(v=win.10)) and [DirectX and XAML interop](https://docs.microsoft.com/previous-versions/windows/apps/hh825871(v=win.10)). |
| **Microsoft.Xna.Framework.Audio.Microphone** class | [**MediaCapture**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture) class |
| **Microsoft.Xna.Framework.Audio.SoundEffect** class | [**MediaElement**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaElement) class |
| **Microsoft.Xna.Framework.GamerServices** namespace | (WPS = **Windows.Phone.System**) <br/> [**WPS.UserProfile.GameServices.Core**](https://docs.microsoft.com/uwp/api/Windows.Phone.System.UserProfile.GameServices.Core) namespace |
| **Microsoft.Xna.Framework.GamerServices.Guide** class | No direct equivalent |
| **Microsoft.Xna.Framework.Input.GamePad** class | [**HardwareButtons**](https://docs.microsoft.com/uwp/api/Windows.Phone.UI.Input.HardwareButtons) class |
| **Microsoft.Xna.Framework.Input.Touch.TouchPanel** class | [**GestureRecognizer**](https://docs.microsoft.com/uwp/api/Windows.UI.Input.GestureRecognizer) class |
| (MXFM = **Microsoft.Xna.Framework.Media**) <br/> **MXFM.MediaLibrary**, **MXFM.PhoneExtensions.MediaLibraryExtensions** classes | [**KnownFolders**](https://docs.microsoft.com/uwp/api/Windows.Storage.KnownFolders) class |
| **Microsoft.Xna.Framework.Media.MediaQueue** class | [**SystemMediaTransportControls**](https://docs.microsoft.com/uwp/api/Windows.Media.SystemMediaTransportControls) class |
| **Microsoft.Xna.Framework.Media.Playlist** class | [**BackgroundMediaPlayer**](https://docs.microsoft.com/uwp/api/Windows.Media.Playback.BackgroundMediaPlayer) class |
| **System.Windows.Media** namespace | [**Windows.UI.Xaml.Media**](/uwp/api/Windows.UI.Xaml.Media) namespace |
| **System.Windows.Media.RadialGradientBrush** class | No direct equivalent. See [Media and graphics](wpsl-to-uwp-porting-xaml-and-ui.md). |
| **System.Windows.Media.Animation** namespace | [**Windows.UI.Xaml.Media.Animation**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Animation) namespace |
| **System.Windows.Media.Effects** namespace | No direct equivalent |
| **System.Windows.Media.Imaging** namespace | [**Windows.UI.Xaml.Media.Imaging**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Imaging) namespace |
| **System.Windows.Media.Media3D** namespace | [**Windows.UI.Xaml.Media.Media3D**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Media3D) namespace |
| **System.Windows.Shapes** namespace | [**Windows.UI.Xaml.Shapes**](/uwp/api/Windows.UI.Xaml.Shapes) namespace |
| Launchers and Choosers | |
| **Microsoft.Phone.Tasks.AddressChooserTask**, **EmailAddressChooserTask**, **PhoneNumberChooserTask** classes | [**ContactPicker**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Contacts.ContactPicker) class |
| **Microsoft.Phone.Tasks.AddWalletItemTask**, **AddWalletItemResult** classes | [**Windows.ApplicationModel.Wallet**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Wallet) namespace |
| **Microsoft.Phone.Tasks.BingMapsDirectionsTask**, **BingMapsTask** classes | No direct equivalent |
| **Microsoft.Phone.Tasks.CameraCaptureTask** class | [**MediaCapture**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture) class. Also, [**CameraCaptureUI**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUI) class (Windows only). |
| **Microsoft.Phone.Tasks.MarketplaceDetailTask** | [**CurrentApp**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store.CurrentApp) class ([**RequestAppPurchaseAsync**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.currentapp.requestapppurchaseasync) method) |
| **Microsoft.Phone.Tasks.ConnectionSettingsTask**, **MarketplaceHubTask**, **MarketplaceReviewTask**, **MarketplaceSearchTask**, **MediaPlayerLauncher**, **SearchTask**, **SmsComposeTask**, **WebBrowserTask** classes | [**Launcher**](https://docs.microsoft.com/uwp/api/Windows.System.Launcher) class |
| **Microsoft.Phone.Tasks.EmailComposeTask** class | [**EmailMessage**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Email.EmailMessage) class |
| **Microsoft.Phone.Tasks.GameInviteTask** class | No direct equivalent |
| **Microsoft.Phone.Tasks.MapDownloaderTask**, **MapsDirectionsTask**, **MapsTask**, **MapUpdaterTask** classes | No direct equivalent |
| **Microsoft.Phone.Tasks.PhoneCallTask** class | [**PhoneCallManager**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Calls.PhoneCallManager) class |
| **Microsoft.Phone.Tasks.PhotoChooserTask** class | [**FileOpenPicker**](https://docs.microsoft.com/uwp/api/Windows.Storage.Pickers.FileOpenPicker) class |
| **Microsoft.Phone.Tasks.SaveAppointmentTask** class | [**AppointmentManager**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Appointments.AppointmentManager) class |
| **Microsoft.Phone.Tasks.SaveContactTask**, **SaveEmailAddressTask**, **SavePhoneNumberTask** classes | [**StoredContact**](https://docs.microsoft.com/uwp/api/Windows.Phone.PersonalInformation.StoredContact) class (Windows Phone only) | 
| **Microsoft.Phone.Tasks.SaveRingtoneTask** class | No direct equivalent |
| **Microsoft.Phone.Tasks.ShareLinkTask**, **ShareMediaTask**, **ShareStatusTask** classes | [**DataPackage**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) class |
| Location | |
| **System.Device.Location** namespace | [**Windows.Devices.Geolocation**](https://docs.microsoft.com/uwp/api/Windows.Devices.Geolocation) namespace |
| **System.Device.GeoCoordinateWatcher** class | [**Geolocator**](https://docs.microsoft.com/uwp/api/Windows.Devices.Geolocation.Geolocator) class |
| Maps | |
| **Microsoft.Phone.Maps** namespaces | [**Windows.Services.Maps**](https://docs.microsoft.com/uwp/api/Windows.Services.Maps) namespace |
| **Microsoft.Phone.Maps.Controls** namespace | [**Windows.UI.Xaml.Controls.Maps**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Maps) namespace |
| **Microsoft.Phone.Maps.Controls.Map** class | [**MapControl**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) class |
| **Microsoft.Phone.Maps.Services** namespace | [**Windows.Services.Maps**](https://docs.microsoft.com/uwp/api/Windows.Services.Maps) namespace |
| **Microsoft.Phone.Maps.Services.GeocodeQuery**, **ReverseGeocodeQuery** classes | [**MapLocationFinder**](https://docs.microsoft.com/uwp/api/Windows.Services.Maps.MapLocationFinder) class |
| **System.Device.Location.GeoCoordinate** class | [**Geopoint**](https://docs.microsoft.com/uwp/api/Windows.Devices.Geolocation.Geopoint) class |
| **Microsoft.Phone.Maps.Services.Route** class | [**MapRoute**](https://docs.microsoft.com/uwp/api/Windows.Services.Maps.MapRoute) class |
| **Microsoft.Phone.Maps.Services.RouteQuery** class | [**MapRouteFinder**](https://docs.microsoft.com/uwp/api/Windows.Services.Maps.MapRouteFinder) class |
| Monetization | |
| **Microsoft.Phone.Marketplace** namespace | [**Windows.ApplicationModel.Store**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store) namespace |
| Media | |
| **Microsoft.Phone.Media** namespace | [**MediaElement**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaElement) class |
| Networking | |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.DeviceNetworkInformation** class | [**Hostname**](https://docs.microsoft.com/uwp/api/Windows.Networking.HostName), [**NetworkInformation**](https://docs.microsoft.com/uwp/api/Windows.Networking.Connectivity.NetworkInformation) classes |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.NetworkInterface** class | [**NetworkInformation**](https://docs.microsoft.com/uwp/api/Windows.Networking.Connectivity.NetworkInformation) class |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.NetworkInterfaceInfo** class | [**ConnectionProfile**](https://docs.microsoft.com/uwp/api/Windows.Networking.Connectivity.ConnectionProfile) class |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.NetworkInterfaceList** class | [**NetworkInformation**](https://docs.microsoft.com/uwp/api/Windows.Networking.Connectivity.NetworkInformation) class |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.SocketExtensions** class | No direct equivalent |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.WebRequestExtensions** class | No direct equivalent |
| **Microsoft.Phone.Networking.Voip** namespace | No direct equivalent |
| **System.Net.CookieCollection** class | Still supported, but some properties are missing (for example, IsReadOnly) |
| **System.Net.DownloadProgressChangedEventArgs** class, and similar classes related to **System.Net.WebClient** | [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))). Derive from [System.Net.Http.StreamContent](https://docs.microsoft.com/previous-versions/visualstudio/hh138119(v=vs.118)) to measure progress. |
| **System.Net.DnsEndPoint**, **IPAddress** classes | These classes are still supported, but some properties are missing. Alternatively, port to the [**HostName**](https://docs.microsoft.com/uwp/api/Windows.Networking.HostName) class. |
| **System.Net.HttpUtility** class | [**HtmlFormatHelper**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.DataTransfer.HtmlFormatHelper) class |
| **System.Net.HttpWebRequest** class | Partial support, but the recommended, forward-looking alternative is the [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))). These APIs use [System.Net.Http.HttpRequestMessage](https://docs.microsoft.com/previous-versions/visualstudio/hh159020(v=vs.118)) to represent an HTTP request. |
| **System.Net.HttpWebResponse** class | Still supported, but use Dispose() instead of Close(). But, the recommended, forward-looking alternative is the [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))). These APIs use [System.Net.Http.HttpResponseMessage](https://docs.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) to represent an HTTP response. |
| (SNN = **System.Net.NetworkInformation**) <br/> **SNN.NetworkChange** class | Still supported, except for the constructor. |
| **System.Net.OpenReadCompletedEventArgs** class, and similar classes related to **System.Net.WebClient** | [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))) |
| **System.Net.Sockets.Socket** class | Still supported, but use Dispose() instead of Close(). Alternatively, port to the[**StreamSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket) class. |
| **System.Net.Sockets.SocketException** class | Still supported, but use the SocketErrorCode property instead of ErrorCode. |
| **System.Net.Sockets.UdpAnySourceMulticastClient**, **UdpSingleSourceMulticastClient** classes | [**DatagramSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.DatagramSocket) class |
| **System.Net.UploadProgressChangedEventArgs** class, and similar classes related to **System.Net.WebClient** | [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))) |
| **System.Net.WebClient** class | [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))) |
| **System.Net.WebRequest** class | Partial support (a different set of properties), but the recommended, forward-looking alternative is the [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))). These APIs use [System.Net.Http.HttpRequestMessage](https://docs.microsoft.com/previous-versions/visualstudio/hh159020(v=vs.118)) to represent an HTTP request. |
| **System.Net.WebResponse** class | Still supported, but use Dispose() instead of Close(). But, the recommended, forward-looking alternative is the [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))). These APIs use [System.Net.Http.HttpResponseMessage](https://docs.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) to represent an HTTP response. |
| (SN = **System.Net**) <br/> **SN.WriteStreamClosedEventArgs** class | [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))) |
| (SN = **System.Net**) <br/> **SN.WriteStreamClosedEventHandler** class | [**HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](https://docs.microsoft.com/previous-versions/visualstudio/hh193681(v=vs.118))) |
| **System.UriFormatException** class | **System.FormatException** class |
| Notifications | |
| MPN = **Microsoft.Phone.Notification** namespace | [**Windows.UI.Notifications**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications), [**Windows.Networking.PushNotifications**](https://docs.microsoft.com/uwp/api/Windows.Networking.PushNotifications) namespaces |
| MPN = **Microsoft.Phone.Notification** <br/> **MPN.HttpNotification** class | [**TileNotification**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.TileNotification) class |
| MPN = **Microsoft.Phone.Notification** <br/> **MPN.HttpNotificationChannel** class | [**PushNotificationChannel**](https://docs.microsoft.com/uwp/api/Windows.Networking.PushNotifications.PushNotificationChannel) class |
| Programming | |
| **System** namespace | [**Windows.Foundation**](https://docs.microsoft.com/uwp/api/Windows.Foundation) namespace |
| **System.Diagnostics.StackFrame**, **StackTrace** classes | No direct equivalent |
| **System.Diagnostics** namespace | [**Windows.Foundation.Diagnostics**](https://docs.microsoft.com/uwp/api/Windows.Foundation.Diagnostics) namespace |
| **System.ICloneable** interface | A custom method that returns the appropriate type. |
| **System.Reflection.Emit.ILGenerator** class | No direct equivalent |
| Reactive Extensions | |
| **Microsoft.Phone.Reactive** namespace | No direct equivalent |
| Reflection | |
| **System.Type** class | **System.Reflection.TypeInfo** class. See [Reflection in the .NET Framework for UWP apps](https://docs.microsoft.com/dotnet/framework/reflection-and-codedom/reflection-for-windows-store-apps). |
| Resources | |
| **System.Resources.ResourceManager** class | (WA = **Windows.ApplicationModel**)<br/>[**WA.Resources.Core**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Resources.Core) and [**WA.Resources**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Resources) namespaces, [**ResourceManager**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceManager) class. See [Creating and retrieving resources in Windows Runtime apps](https://docs.microsoft.com/previous-versions/windows/apps/hh694557(v=vs.140)). |
| Secure Element | |
| (MPS = **Microsoft.Phone.SecureElement**) <br/> **MPS.SecureElementChannel**, **MPS.SecureElementSession** classes | [**SmartCardConnection**](https://docs.microsoft.com/uwp/api/Windows.Devices.SmartCards.SmartCardConnection) class |
| (MPS = **Microsoft.Phone.SecureElement**) <br/> **MPS.SecureElementReader** class | [**SmartCardReader**](https://docs.microsoft.com/uwp/api/Windows.Devices.SmartCards.SmartCardReader) class |
| Security | |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.Aes**, **SSC.RSA** classes | [**CryptographicEngine**](https://docs.microsoft.com/uwp/api/Windows.Security.Cryptography.Core.CryptographicEngine) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.HMACSHA256**, **SSC.SHA256** classes | [**HashAlgorithmProvider**](https://docs.microsoft.com/uwp/api/Windows.Security.Cryptography.Core.HashAlgorithmProvider) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.ProtectedData** class | [**DataProtectionProvider**](https://docs.microsoft.com/uwp/api/Windows.Security.Cryptography.DataProtection.DataProtectionProvider) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.RandomNumberGenerator** class | [**CryptographicBuffer**](https://docs.microsoft.com/uwp/api/Windows.Security.Cryptography.CryptographicBuffer) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.X509Certificates.X509Certificate** class | [**CertificateEnrollmentManager**](https://docs.microsoft.com/uwp/api/Windows.Security.Cryptography.Certificates.CertificateEnrollmentManager) class |
| Shell | |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ApplicationBar** class | [**CommandBar**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.CommandBar) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ApplicationBarIconButton** class | [**AppBarButton**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.AppBarButton) class (when used inside the [**PrimaryCommands**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.commandbar.primarycommands) property) |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ApplicationBarMenuItem** class | [**AppBarButton**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.AppBarButton) class (when used inside the [**SecondaryCommands**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.commandbar.secondarycommands) property) |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.CycleTileData**, **MPSh.FlipTileData**, **MPSh.IconicTileData**, **MPSh.ShellTileData**, **MPSh.StandardTileData** classes | [**TileTemplateType**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.TileTemplateType) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.PhoneApplicationService** class | [**CoreApplication**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Core.CoreApplication), [**DisplayRequest**](https://docs.microsoft.com/uwp/api/Windows.System.Display.DisplayRequest) classes |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ProgressIndicator** class | [**StatusBarProgressIndicator**](https://docs.microsoft.com/uwp/api/Windows.UI.ViewManagement.StatusBarProgressIndicator) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ShellTile** class | [**SecondaryTile**](https://docs.microsoft.com/uwp/api/Windows.UI.StartScreen.SecondaryTile) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ShellTileSchedule** class | [**TileUpdater**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.TileUpdater) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ShellToast** class | [**ToastNotificationManager**](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.ToastNotificationManager) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.SystemTray** class | [**StatusBar**](https://docs.microsoft.com/uwp/api/Windows.UI.ViewManagement.StatusBar) class |
| Storage and I/O | |
| **Microsoft.Phone.Storage.ExternalStorage**, **ExternalStorageDevice**, **ExternalStorageFile**, **ExternalStorageFolder** classes | [**KnownFolders**](https://docs.microsoft.com/uwp/api/Windows.Storage.KnownFolders) class |
| **System.IO** namespace | [**Windows.Storage**](https://docs.microsoft.com/uwp/api/Windows.Storage), [**Windows.Storage.Streams**](https://docs.microsoft.com/uwp/api/Windows.Storage.Streams) namespaces |
| **System.IO.Directory** class | [**StorageFolder**](https://docs.microsoft.com/uwp/api/Windows.Storage.StorageFolder) class |
| **System.IO.File** class | [**StorageFile**](https://docs.microsoft.com/uwp/api/Windows.Storage.StorageFile) and [**PathIO**](https://docs.microsoft.com/uwp/api/Windows.Storage.PathIO) classes
| (SII = **System.IO.IsolatedStorage**) <br/> **SII.IsolatedStorageFile** class |[**ApplicationData.LocalFolder**](https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.localfolder) property |
| (SII = **System.IO.IsolatedStorage**) <br/> **SII.IsolatedStorageSettings** class | [**ApplicationData.LocalSettings**](https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.localsettings) property |
| **System.IO.Stream** class | Still supported, but use ReadAsync() and WriteAsync() instead of BeginRead()/EndRead() and BeginWrite()/EndWrite(). |
| Wallet | |
| **Microsoft.Phone.Wallet** namespace | [**Windows.ApplicationModel.Wallet**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Wallet) namespace |
| Xml | |
| (SX = **System.Xml**) | **SX.XmlConvert.ToDateTime** method |
| (SX = **System.Xml**) | **SX.XmlConvert.ToDateTimeOffset** method |


The next topic is [Porting the project](wpsl-to-uwp-porting-to-a-uwp-project.md).

