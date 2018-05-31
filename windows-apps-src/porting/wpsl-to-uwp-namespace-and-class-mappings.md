---
author: stevewhims
description: This topic provides a comprehensive mapping of Windows Phone Silverlight APIs to their Universal Windows Platform (UWP) equivalents.
title: Windows Phone Silverlight to UWP namespace and class mappings
ms.assetid: 33f06706-4790-48f3-a2e4-ebef9ddb61a4
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
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
| **Microsoft.Advertising.Mobile.UI.AdControl** class | [AdControl](http://msdn.microsoft.com/library/advertising-windows-sdk-api-reference-adcontrol.aspx) class |
| Alarms, reminders, and background agents | |
| **Microsoft.Phone.BackgroundAgent** class | [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) class |
| **Microsoft.Phone.Scheduler** namespace | [**Windows.ApplicationModel.Background**](https://msdn.microsoft.com/library/windows/apps/br224847) namespace |
| **Microsoft.Phone.Scheduler.Alarm** class | [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) and [**ToastNotificationManager**](https://msdn.microsoft.com/library/windows/apps/br208642) classes |
| **Microsoft.Phone.Scheduler.PeriodicTask**, **ScheduledAction**, **ScheduledActionService**, **ScheduledTask** , **ScheduledTaskAgent** classes | [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) class |
| **Microsoft.Phone.Scheduler.Reminder** class | [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) and [**ToastNotificationManager**](https://msdn.microsoft.com/library/windows/apps/br208642) classes |
| **Microsoft.Phone.PictureDecoder** class | [**BitmapDecoder**](https://msdn.microsoft.com/library/windows/apps/br226176) class |
| **Microsoft.Phone.BackgroundAudio** namespace | [**Windows.Media.Playback**](https://msdn.microsoft.com/library/windows/apps/dn640562) namespace |
| **Microsoft.Phone.BackgroundTransfer** namespace | [**Windows.Networking.BackgroundTransfer**](https://msdn.microsoft.com/library/windows/apps/br207242) namespace |
| App model and environment | |
| **System.AppDomain** class | No direct equivalent. See [**Application**](https://msdn.microsoft.com/library/windows/apps/br242324), [**CoreApplication**](https://msdn.microsoft.com/library/windows/apps/br225016), classes |
| **System.Environment** class | No direct equivalent |
| **System.ComponentModel.Annotations** class  | No direct equivalent |
| **System.ComponentModel.BackgroundWorker** class | [**ThreadPool**](https://msdn.microsoft.com/library/windows/apps/br229621) class |
| **System.ComponentModel.DesignerProperties** class | [**DesignMode**](https://msdn.microsoft.com/library/windows/apps/br224664) class |
| **System.Threading.Thread**, **System.Threading.ThreadPool** classes | [**ThreadPool**](https://msdn.microsoft.com/library/windows/apps/br229621) class |
| (ST = **System.Threading**) <br/> **ST.Thread.MemoryBarrier** method | (ST = **System.Threading**) <br/> **ST.Interlocked.MemoryBarrier** method |
| (ST = **System.Threading**) <br/> **ST.Thread.ManagedThreadId** property | (S = **System**) <br/> **S.Environment.ManagedThreadId** property |
| **System.Threading.Timer** class | [**ThreadPoolTimer**](https://msdn.microsoft.com/library/windows/apps/br230587) class |
| (SWT = **System.Windows.Threading**) <br/> **SWT.Dispatcher** class | [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) class |
| (SWT = **System.Windows.Threading**) <br/> **SWT.DispatcherTimer** class | [**DispatcherTimer**](https://msdn.microsoft.com/library/windows/apps/br244250) class |
| Blend for Visual Studio | |
| (MEDC = **Microsoft.Expression.Drawing.Core**) <br/> **MEDC.GeometryHelper** class | No direct equivalent |
| **Microsoft.Expression.Interactivity** namespace | [Microsoft.Xaml.Interactivity](http://go.microsoft.com/fwlink/p/?LinkId=328776) namespace |
| **Microsoft.Expression.Interactivity.Core** namespace | [Microsoft.Xaml.Interactions.Core](http://go.microsoft.com/fwlink/p/?LinkId=328773) namespace |
| (MEIC = **Microsoft.Expression.Interactivity.Core**) <br/> **MEIC.ExtendedVisualStateManager** class | No direct equivalent |
| **Microsoft.Expression.Interactivity.Input** namespace | No direct equivalent |
| **Microsoft.Expression.Interactivity.Media** namespace | [Microsoft.Xaml.Interactions.Media](http://go.microsoft.com/fwlink/p/?LinkId=328775) namespace |
| **Microsoft.Expression.Shapes** namespace | No direct equivalent |
| (MI = **Microsoft.Internal**) <br/> **MI.IManagedFrameworkInternalHelper** interface | No direct equivalent |
| Contact and calendar data | |
| **Microsoft.Phone.UserData** namespace | [**Windows.ApplicationModel.Contacts**](https://msdn.microsoft.com/library/windows/apps/br225002), [**Windows.ApplicationModel.Appointments**](https://msdn.microsoft.com/library/windows/apps/dn263359) namespaces |
| (MPU = **Microsoft.Phone.UserData**) <br/> **MPU.Account**, **ContactAddress**, **ContactCompanyInformation**, **ContactEmailAddress**, **ContactPhoneNumber** classes | [**Contact**](https://msdn.microsoft.com/library/windows/apps/br224849) class |
| (MPU = **Microsoft.Phone.UserData**) <br/> **MPU.Appointments** class | [**AppointmentCalendar**](https://msdn.microsoft.com/library/windows/apps/dn596134) class |
| (MPU = **Microsoft.Phone.UserData**) <br/> **MPU.Contacts** class | [**ContactStore**](https://msdn.microsoft.com/library/windows/apps/dn624859) class |
| Controls and UI infrastructure | |
| **ControlTiltEffect.TiltEffect** class | Animations from the Windows Runtime animation library are built into the default Styles of the common controls. See [Animation](wpsl-to-uwp-porting-xaml-and-ui.md). |
| **Microsoft.Phone.Controls** namespace | [**Windows.UI.Xaml.Controls**](https://msdn.microsoft.com/library/windows/apps/br227716) namespace |
| (MPC = **Microsoft.Phone.Controls**) <br/> **MPC.ContextMenu** class | [**PopupMenu**](https://msdn.microsoft.com/library/windows/apps/br208693) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.DatePickerPage** class | [**DatePickerFlyout**](https://msdn.microsoft.com/library/windows/apps/dn625013) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.GestureListener** class | [**GestureRecognizer**](https://msdn.microsoft.com/library/windows/apps/br241937) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.LongListSelector** class | [**SemanticZoom**](https://msdn.microsoft.com/library/windows/apps/hh702601) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.ObscuredEventArgs** class | [**SystemProtection**](https://msdn.microsoft.com/library/windows/apps/jj585394), [**WindowActivatedEventArgs**](https://msdn.microsoft.com/library/windows/apps/br208377) classes |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.Panorama** class | [**Hub**](https://msdn.microsoft.com/library/windows/apps/dn251843) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.PhoneApplicationFrame**,<br/>(SWN = **System.Windows.Navigation**) <br/>**SWN.NavigationService** classes | [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.PhoneApplicationPage** class | [**Page**](https://msdn.microsoft.com/library/windows/apps/br227503) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.TiltEffect** class | [**PointerDownThemeAnimation**](https://msdn.microsoft.com/library/windows/apps/hh969164) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.TimePickerPage** class | [**TimePickerFlyout**](https://msdn.microsoft.com/library/windows/apps/dn608313) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.WebBrowser** class | [**WebView**](https://msdn.microsoft.com/library/windows/apps/br227702) class |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.WebBrowserExtensions** class | No direct equivalent |
| (MPC = **Microsoft.Phone.Controls**) <br/>**MPC.WrapPanel** class | No direct equivalent for general layout purposes. [**ItemsWrapGrid**](https://msdn.microsoft.com/library/windows/apps/dn298849) and [**WrapGrid**](https://msdn.microsoft.com/library/windows/apps/br227717) can be used in the items panel template of an items control. |
| (MPD = **Microsoft.Phone.Data**) <br/>**MPD.Linq** namespace | No direct equivalent |
| (MPD = **Microsoft.Phone.Data**) <br/>**MPD.Linq.Mapping** namespace | No direct equivalent |
| **Microsoft.Phone.Globalization** namespace | No direct equivalent |
| (MPI = **Microsoft.Phone.Info**) <br/>**MPI.DeviceExtendedProperties**, **DeviceStatus** classes | [**EasClientDeviceInformation**](https://msdn.microsoft.com/library/windows/apps/hh701390), [**MemoryManager**](https://msdn.microsoft.com/library/windows/apps/dn633831) classes. For more details, see [Device status](wpsl-to-uwp-input-and-sensors.md). |
| (MPI = **Microsoft.Phone.Info**) <br/>**MPI.MediaCapabilities** class | No direct equivalent |
| (MPI = **Microsoft.Phone.Info**) <br/>**MPI.UserExtendedProperties** class | [**AdvertisingManager**](https://msdn.microsoft.com/library/windows/apps/dn363391) class |
| **System.Windows** namespace | [**Windows.UI.Xaml**](https://msdn.microsoft.com/library/windows/apps/br209045) namespace |
| **System.Windows.Automation** namespace | [**Windows.UI.Xaml.Automation**](https://msdn.microsoft.com/library/windows/apps/br209179) namespace |
| **System.Windows.Controls**, **System.Windows.Input** namespaces | [**Windows.UI.Core**](https://msdn.microsoft.com/library/windows/apps/br208383), [**Windows.UI.Input**](https://msdn.microsoft.com/library/windows/apps/br242084), [**Windows.UI.Xaml.Controls**](https://msdn.microsoft.com/library/windows/apps/br227716) namespaces |
| **System.Windows.Controls.DrawingSurface**, **DrawingSurfaceBackgroundGrid** classes | [**SwapChainPanel**](https://msdn.microsoft.com/library/windows/apps/dn252834) class |
| **System.Windows.Controls.RichTextBox** class | [**RichEditBox**](https://msdn.microsoft.com/library/windows/apps/br227548) class |
| **System.Windows.Controls.WrapPanel** class | No direct equivalent for general layout purposes. [**ItemsWrapGrid**](https://msdn.microsoft.com/library/windows/apps/dn298849) and [**WrapGrid**](https://msdn.microsoft.com/library/windows/apps/br227717) can be used in the items panel template of an items control. |
| **System.Windows.Controls.Primitives** namespace | [**Windows.UI.Xaml.Controls.Primitives**](https://msdn.microsoft.com/library/windows/apps/br209818) namespace |
| **System.Windows.Controls.Shapes** namespace | [**Windows.UI.Xaml.Controls.Shapes**](/uwp/api/Windows.UI.Xaml.Shapes) namespace |
| **System.Windows.Data** namespace | [**Windows.UI.Xaml.Data**](https://msdn.microsoft.com/library/windows/apps/br209917) namespace |
| **System.Windows.Documents** namespace | [**Windows.UI.Xaml.Documents**](https://msdn.microsoft.com/library/windows/apps/br209984) namespace |
| **System.Windows.Ink** namespace | No direct equivalent |
| **System.Windows.Markup** namespace | [**Windows.UI.Xaml.Markup**](https://msdn.microsoft.com/library/windows/apps/br228046) namespace | 
| **System.Windows.Navigation** namespace | [**Windows.UI.Xaml.Navigation**](https://msdn.microsoft.com/library/windows/apps/br243300) namespace |
| **System.Windows.UIElement.Tap** event, **EventHandler&lt;GestureEventArgs&gt;** delegate | [**Tapped**](https://msdn.microsoft.com/library/windows/apps/br208985) event, [**TappedEventHandler**](https://msdn.microsoft.com/library/windows/apps/br227993) delegate |
| Data and services |  |
| **System.Data.Linq.DataContext** class | No direct equivalent |
| **System.Data.Linq.Mapping.ColumnAttribute** class | No direct equivalent |
| **System.Data.Linq.SqlClient.SqlHelpers** class | No direct equivalent |
| Devices | |
| **Microsoft.Devices**, **Microsoft.Devices.Sensors** namespaces | [**Windows.Devices.Enumeration**](https://msdn.microsoft.com/library/windows/apps/br225459), [**Windows.Devices.Enumeration.Pnp**](https://msdn.microsoft.com/library/windows/apps/br225517), [**Windows.Devices.Input**](https://msdn.microsoft.com/library/windows/apps/br225648), [**Windows.Devices.Sensors**](https://msdn.microsoft.com/library/windows/apps/br206408) namespaces |
| **Microsoft.Devices.Camera**, **Microsoft.Devices.PhotoCamera** classes | [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/br241124) class. Also, [**CameraCaptureUI**](https://msdn.microsoft.com/library/windows/apps/br241030) class (Windows only). |
| **Microsoft.Devices.CameraButtons** class | [**HardwareButtons**](https://msdn.microsoft.com/library/windows/apps/jj207557) class |
| **Microsoft.Devices.CameraVideoBrushExtensions** class | [**CaptureElement**](https://msdn.microsoft.com/library/windows/apps/br209278) class |
| **Microsoft.Devices.Environment** class | No direct equivalent. As a workaround, use conditional compilation and define a custom symbol. Or you may be able to engineer a workaround using the [IsAttached](http://msdn.microsoft.com/library/e299w87h.aspx) property. |
| **Microsoft.Devices.MediaHistory** class | No direct equivalent |
| **Microsoft.Devices.VibrateController** class | [**VibrationDevice**](https://msdn.microsoft.com/library/windows/apps/jj207230) class |
| **Microsoft.Devices.Radio.FMRadio** class | No direct equivalent |
| **Microsoft.Devices.Sensors.Accelerometer**, **Compass** classes | In the [**Windows.Devices.Sensors**](https://msdn.microsoft.com/library/windows/apps/br206408) namespace |
| **Microsoft.Devices.Sensors.Gyroscope** class | [**Gyrometer**](https://msdn.microsoft.com/library/windows/apps/br225718) class |
| **Microsoft.Devices.Sensors.Motion** class | [**Inclinometer**](https://msdn.microsoft.com/library/windows/apps/br225766) class |
| Globalization | |
| **System.Globalization** namespace | [**Windows.Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813) namespace |
| (ST = **System.Threading**) <br/> **ST.Thread.CurrentCulture** property | (SG = **System.Globalization**) <br/> **S.CultureInfo.CurrentCulture** property |
| (ST = **System.Threading**) <br/> **ST.Thread.CurrentUICulture** property | (SG = **System.Globalization**) <br/> **S.CultureInfo.CurrentUICulture** property |
| Graphics and animation | |
| **Microsoft.Xna.Framework.\*** namespaces, [XNA Framework Class Library](http://go.microsoft.com/fwlink/p/?LinkId=263769), [Content Pipeline Class Library](http://go.microsoft.com/fwlink/p/?LinkId=263770) | No direct equivalent. In general, use [Microsoft DirectX](https://msdn.microsoft.com/library/windows/desktop/ee663274) with C++. See [Developing games](https://msdn.microsoft.com/library/windows/apps/hh452744) and [DirectX and XAML interop](https://msdn.microsoft.com/library/windows/apps/hh825871). |
| **Microsoft.Xna.Framework.Audio.Microphone** class | [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/br241124) class |
| **Microsoft.Xna.Framework.Audio.SoundEffect** class | [**MediaElement**](https://msdn.microsoft.com/library/windows/apps/br242926) class |
| **Microsoft.Xna.Framework.GamerServices** namespace | (WPS = **Windows.Phone.System**) <br/> [**WPS.UserProfile.GameServices.Core**](https://msdn.microsoft.com/library/windows/apps/jj207609) namespace |
| **Microsoft.Xna.Framework.GamerServices.Guide** class | No direct equivalent |
| **Microsoft.Xna.Framework.Input.GamePad** class | [**HardwareButtons**](https://msdn.microsoft.com/library/windows/apps/jj207557) class |
| **Microsoft.Xna.Framework.Input.Touch.TouchPanel** class | [**GestureRecognizer**](https://msdn.microsoft.com/library/windows/apps/br241937) class |
| (MXFM = **Microsoft.Xna.Framework.Media**) <br/> **MXFM.MediaLibrary**, **MXFM.PhoneExtensions.MediaLibraryExtensions** classes | [**KnownFolders**](https://msdn.microsoft.com/library/windows/apps/br227151) class |
| **Microsoft.Xna.Framework.Media.MediaQueue** class | [**SystemMediaTransportControls**](https://msdn.microsoft.com/library/windows/apps/dn278677) class |
| **Microsoft.Xna.Framework.Media.Playlist** class | [**BackgroundMediaPlayer**](https://msdn.microsoft.com/library/windows/apps/dn652527) class |
| **System.Windows.Media** namespace | [**Windows.UI.Xaml.Media**](/uwp/api/Windows.UI.Xaml.Media) namespace |
| **System.Windows.Media.RadialGradientBrush** class | No direct equivalent. See [Media and graphics](wpsl-to-uwp-porting-xaml-and-ui.md). |
| **System.Windows.Media.Animation** namespace | [**Windows.UI.Xaml.Media.Animation**](https://msdn.microsoft.com/library/windows/apps/br243232) namespace |
| **System.Windows.Media.Effects** namespace | No direct equivalent |
| **System.Windows.Media.Imaging** namespace | [**Windows.UI.Xaml.Media.Imaging**](https://msdn.microsoft.com/library/windows/apps/br243258) namespace |
| **System.Windows.Media.Media3D** namespace | [**Windows.UI.Xaml.Media.Media3D**](https://msdn.microsoft.com/library/windows/apps/br243274) namespace |
| **System.Windows.Shapes** namespace | [**Windows.UI.Xaml.Shapes**](/uwp/api/Windows.UI.Xaml.Shapes) namespace |
| Launchers and Choosers | |
| **Microsoft.Phone.Tasks.AddressChooserTask**, **EmailAddressChooserTask**, **PhoneNumberChooserTask** classes | [**ContactPicker**](https://msdn.microsoft.com/library/windows/apps/br224913) class |
| **Microsoft.Phone.Tasks.AddWalletItemTask**, **AddWalletItemResult** classes | [**Windows.ApplicationModel.Wallet**](https://msdn.microsoft.com/library/windows/apps/dn631399) namespace |
| **Microsoft.Phone.Tasks.BingMapsDirectionsTask**, **BingMapsTask** classes | No direct equivalent |
| **Microsoft.Phone.Tasks.CameraCaptureTask** class | [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/br241124) class. Also, [**CameraCaptureUI**](https://msdn.microsoft.com/library/windows/apps/br241030) class (Windows only). |
| **Microsoft.Phone.Tasks.MarketplaceDetailTask** | [**CurrentApp**](https://msdn.microsoft.com/library/windows/apps/hh779765) class ([**RequestAppPurchaseAsync**](https://msdn.microsoft.com/library/windows/apps/hh967813) method) |
| **Microsoft.Phone.Tasks.ConnectionSettingsTask**, **MarketplaceHubTask**, **MarketplaceReviewTask**, **MarketplaceSearchTask**, **MediaPlayerLauncher**, **SearchTask**, **SmsComposeTask**, **WebBrowserTask** classes | [**Launcher**](https://msdn.microsoft.com/library/windows/apps/br241801) class |
| **Microsoft.Phone.Tasks.EmailComposeTask** class | [**EmailMessage**](https://msdn.microsoft.com/library/windows/apps/dn631270) class |
| **Microsoft.Phone.Tasks.GameInviteTask** class | No direct equivalent |
| **Microsoft.Phone.Tasks.MapDownloaderTask**, **MapsDirectionsTask**, **MapsTask**, **MapUpdaterTask** classes | No direct equivalent |
| **Microsoft.Phone.Tasks.PhoneCallTask** class | [**PhoneCallManager**](https://msdn.microsoft.com/library/windows/apps/dn624832) class |
| **Microsoft.Phone.Tasks.PhotoChooserTask** class | [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/br207847) class |
| **Microsoft.Phone.Tasks.SaveAppointmentTask** class | [**AppointmentManager**](https://msdn.microsoft.com/library/windows/apps/dn297254) class |
| **Microsoft.Phone.Tasks.SaveContactTask**, **SaveEmailAddressTask**, **SavePhoneNumberTask** classes | [**StoredContact**](https://msdn.microsoft.com/library/windows/apps/jj207727) class (Windows Phone only) | 
| **Microsoft.Phone.Tasks.SaveRingtoneTask** class | No direct equivalent |
| **Microsoft.Phone.Tasks.ShareLinkTask**, **ShareMediaTask**, **ShareStatusTask** classes | [**DataPackage**](https://msdn.microsoft.com/library/windows/apps/br205873) class |
| Location | |
| **System.Device.Location** namespace | [**Windows.Devices.Geolocation**](https://msdn.microsoft.com/library/windows/apps/br225603) namespace |
| **System.Device.GeoCoordinateWatcher** class | [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) class |
| Maps | |
| **Microsoft.Phone.Maps** namespaces | [**Windows.Services.Maps**](https://msdn.microsoft.com/library/windows/apps/dn636979) namespace |
| **Microsoft.Phone.Maps.Controls** namespace | [**Windows.UI.Xaml.Controls.Maps**](https://msdn.microsoft.com/library/windows/apps/dn610751) namespace |
| **Microsoft.Phone.Maps.Controls.Map** class | [**MapControl**](https://msdn.microsoft.com/library/windows/apps/dn637004) class |
| **Microsoft.Phone.Maps.Services** namespace | [**Windows.Services.Maps**](https://msdn.microsoft.com/library/windows/apps/dn636979) namespace |
| **Microsoft.Phone.Maps.Services.GeocodeQuery**, **ReverseGeocodeQuery** classes | [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class |
| **System.Device.Location.GeoCoordinate** class | [**Geopoint**](https://msdn.microsoft.com/library/windows/apps/dn263675) class |
| **Microsoft.Phone.Maps.Services.Route** class | [**MapRoute**](https://msdn.microsoft.com/library/windows/apps/dn636937) class |
| **Microsoft.Phone.Maps.Services.RouteQuery** class | [**MapRouteFinder**](https://msdn.microsoft.com/library/windows/apps/dn636938) class |
| Monetization | |
| **Microsoft.Phone.Marketplace** namespace | [**Windows.ApplicationModel.Store**](https://msdn.microsoft.com/library/windows/apps/br225197) namespace |
| Media | |
| **Microsoft.Phone.Media** namespace | [**MediaElement**](https://msdn.microsoft.com/library/windows/apps/br242926) class |
| Networking | |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.DeviceNetworkInformation** class | [**Hostname**](https://msdn.microsoft.com/library/windows/apps/br207113), [**NetworkInformation**](https://msdn.microsoft.com/library/windows/apps/br207293) classes |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.NetworkInterface** class | [**NetworkInformation**](https://msdn.microsoft.com/library/windows/apps/br207293) class |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.NetworkInterfaceInfo** class | [**ConnectionProfile**](https://msdn.microsoft.com/library/windows/apps/br207249) class |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.NetworkInterfaceList** class | [**NetworkInformation**](https://msdn.microsoft.com/library/windows/apps/br207293) class |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.SocketExtensions** class | No direct equivalent |
| (MPNN = **Microsoft.Phone.Net.NetworkInformation**) <br/> **MPNN.WebRequestExtensions** class | No direct equivalent |
| **Microsoft.Phone.Networking.Voip** namespace | No direct equivalent |
| **System.Net.CookieCollection** class | Still supported, but some properties are missing (for example, IsReadOnly) |
| **System.Net.DownloadProgressChangedEventArgs** class, and similar classes related to **System.Net.WebClient** | [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(v=vs.118).aspx)). Derive from [System.Net.Http.StreamContent](https://msdn.microsoft.com/library/system.net.http.streamcontent.aspx) to measure progress. |
| **System.Net.DnsEndPoint**, **IPAddress** classes | These classes are still supported, but some properties are missing. Alternatively, port to the [**HostName**](https://msdn.microsoft.com/library/windows/apps/br207113) class. |
| **System.Net.HttpUtility** class | [**HtmlFormatHelper**](https://msdn.microsoft.com/library/windows/apps/hh738437) class |
| **System.Net.HttpWebRequest** class | Partial support, but the recommended, forward-looking alternative is the [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(v=vs.118).aspx)). These APIs use [System.Net.Http.HttpRequestMessage](https://msdn.microsoft.com/library/system.net.http.httprequestmessage.aspx) to represent an HTTP request. |
| **System.Net.HttpWebResponse** class | Still supported, but use Dispose() instead of Close(). But, the recommended, forward-looking alternative is the [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(v=vs.118).aspx)). These APIs use [System.Net.Http.HttpResponseMessage](https://msdn.microsoft.com/library/system.net.http.httpresponsemessage.aspx) to represent an HTTP response. |
| (SNN = **System.Net.NetworkInformation**) <br/> **SNN.NetworkChange** class | Still supported, except for the constructor. |
| **System.Net.OpenReadCompletedEventArgs** class, and similar classes related to **System.Net.WebClient** | [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient.aspx)) |
| **System.Net.Sockets.Socket** class | Still supported, but use Dispose() instead of Close(). Alternatively, port to the[**StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882) class. |
| **System.Net.Sockets.SocketException** class | Still supported, but use the SocketErrorCode property instead of ErrorCode. |
| **System.Net.Sockets.UdpAnySourceMulticastClient**, **UdpSingleSourceMulticastClient** classes | [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319) class |
| **System.Net.UploadProgressChangedEventArgs** class, and similar classes related to **System.Net.WebClient** | [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient.aspx)) |
| **System.Net.WebClient** class | [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient.aspx)) |
| **System.Net.WebRequest** class | Partial support (a different set of properties), but the recommended, forward-looking alternative is the [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(v=vs.118).aspx)). These APIs use [System.Net.Http.HttpRequestMessage](https://msdn.microsoft.com/library/system.net.http.httprequestmessage.aspx) to represent an HTTP request. |
| **System.Net.WebResponse** class | Still supported, but use Dispose() instead of Close(). But, the recommended, forward-looking alternative is the [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(v=vs.118).aspx)). These APIs use [System.Net.Http.HttpResponseMessage](https://msdn.microsoft.com/library/system.net.http.httpresponsemessage.aspx) to represent an HTTP response. |
| (SN = **System.Net**) <br/> **SN.WriteStreamClosedEventArgs** class | [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient.aspx)) |
| (SN = **System.Net**) <br/> **SN.WriteStreamClosedEventHandler** class | [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class (or [System.Net.Http.HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient.aspx)) |
| **System.UriFormatException** class | **System.FormatException** class |
| Notifications | |
| MPN = **Microsoft.Phone.Notification** namespace | [**Windows.UI.Notifications**](https://msdn.microsoft.com/library/windows/apps/br208661), [**Windows.Networking.PushNotifications**](https://msdn.microsoft.com/library/windows/apps/br241307) namespaces |
| MPN = **Microsoft.Phone.Notification** <br/> **MPN.HttpNotification** class | [**TileNotification**](https://msdn.microsoft.com/library/windows/apps/br208616) class |
| MPN = **Microsoft.Phone.Notification** <br/> **MPN.HttpNotificationChannel** class | [**PushNotificationChannel**](https://msdn.microsoft.com/library/windows/apps/br241283) class |
| Programming | |
| **System** namespace | [**Windows.Foundation**](https://msdn.microsoft.com/library/windows/apps/br226021) namespace |
| **System.Diagnostics.StackFrame**, **StackTrace** classes | No direct equivalent |
| **System.Diagnostics** namespace | [**Windows.Foundation.Diagnostics**](https://msdn.microsoft.com/library/windows/apps/br206677) namespace |
| **System.ICloneable** interface | A custom method that returns the appropriate type. |
| **System.Reflection.Emit.ILGenerator** class | No direct equivalent |
| Reactive Extensions | |
| **Microsoft.Phone.Reactive** namespace | No direct equivalent |
| Reflection | |
| **System.Type** class | **System.Reflection.TypeInfo** class. See [Reflection in the .NET Framework for UWP apps](https://msdn.microsoft.com/library/hh535795.aspx). |
| Resources | |
| **System.Resources.ResourceManager** class | (WA = **Windows.ApplicationModel**)<br/>[**WA.Resources.Core**](https://msdn.microsoft.com/library/windows/apps/br225039) and [**WA.Resources**](https://msdn.microsoft.com/library/windows/apps/br206022) namespaces, [**ResourceManager**](https://msdn.microsoft.com/library/windows/apps/br206078) class. See [Creating and retrieving resources in Windows Runtime apps](https://msdn.microsoft.com/library/windows/apps/xaml/hh694557.aspx). |
| Secure Element | |
| (MPS = **Microsoft.Phone.SecureElement**) <br/> **MPS.SecureElementChannel**, **MPS.SecureElementSession** classes | [**SmartCardConnection**](https://msdn.microsoft.com/library/windows/apps/dn608002) class |
| (MPS = **Microsoft.Phone.SecureElement**) <br/> **MPS.SecureElementReader** class | [**SmartCardReader**](https://msdn.microsoft.com/library/windows/apps/dn263857) class |
| Security | |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.Aes**, **SSC.RSA** classes | [**CryptographicEngine**](https://msdn.microsoft.com/library/windows/apps/br241490) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.HMACSHA256**, **SSC.SHA256** classes | [**HashAlgorithmProvider**](https://msdn.microsoft.com/library/windows/apps/br241511) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.ProtectedData** class | [**DataProtectionProvider**](https://msdn.microsoft.com/library/windows/apps/br241559) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.RandomNumberGenerator** class | [**CryptographicBuffer**](https://msdn.microsoft.com/library/windows/apps/br227092) class |
| (SSC = **System.Security.Cryptography**) <br/> **SSC.X509Certificates.X509Certificate** class | [**CertificateEnrollmentManager**](https://msdn.microsoft.com/library/windows/apps/br212075) class |
| Shell | |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ApplicationBar** class | [**CommandBar**](https://msdn.microsoft.com/library/windows/apps/dn279427) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ApplicationBarIconButton** class | [**AppBarButton**](https://msdn.microsoft.com/library/windows/apps/dn279244) class (when used inside the [**PrimaryCommands**](https://msdn.microsoft.com/library/windows/apps/dn279430) property) |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ApplicationBarMenuItem** class | [**AppBarButton**](https://msdn.microsoft.com/library/windows/apps/dn279244) class (when used inside the [**SecondaryCommands**](https://msdn.microsoft.com/library/windows/apps/dn279434) property) |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.CycleTileData**, **MPSh.FlipTileData**, **MPSh.IconicTileData**, **MPSh.ShellTileData**, **MPSh.StandardTileData** classes | [**TileTemplateType**](https://msdn.microsoft.com/library/windows/apps/br208621) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.PhoneApplicationService** class | [**CoreApplication**](https://msdn.microsoft.com/library/windows/apps/br225016), [**DisplayRequest**](https://msdn.microsoft.com/library/windows/apps/br241816) classes |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ProgressIndicator** class | [**StatusBarProgressIndicator**](https://msdn.microsoft.com/library/windows/apps/dn633865) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ShellTile** class | [**SecondaryTile**](https://msdn.microsoft.com/library/windows/apps/br242183) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ShellTileSchedule** class | [**TileUpdater**](https://msdn.microsoft.com/library/windows/apps/br208628) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.ShellToast** class | [**ToastNotificationManager**](https://msdn.microsoft.com/library/windows/apps/br208642) class |
| (MPSh = **Microsoft.Phone.Shell**) <br/> **MPSh.SystemTray** class | [**StatusBar**](https://msdn.microsoft.com/library/windows/apps/dn633864) class |
| Storage and I/O | |
| **Microsoft.Phone.Storage.ExternalStorage**, **ExternalStorageDevice**, **ExternalStorageFile**, **ExternalStorageFolder** classes | [**KnownFolders**](https://msdn.microsoft.com/library/windows/apps/br227151) class |
| **System.IO** namespace | [**Windows.Storage**](https://msdn.microsoft.com/library/windows/apps/br227346), [**Windows.Storage.Streams**](https://msdn.microsoft.com/library/windows/apps/br241791) namespaces |
| **System.IO.Directory** class | [**StorageFolder**](https://msdn.microsoft.com/library/windows/apps/br227230) class |
| **System.IO.File** class | [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) and [**PathIO**](https://msdn.microsoft.com/library/windows/apps/hh701663) classes
| (SII = **System.IO.IsolatedStorage**) <br/> **SII.IsolatedStorageFile** class |[**ApplicationData.LocalFolder**](https://msdn.microsoft.com/library/windows/apps/br241621) property |
| (SII = **System.IO.IsolatedStorage**) <br/> **SII.IsolatedStorageSettings** class | [**ApplicationData.LocalSettings**](https://msdn.microsoft.com/library/windows/apps/windows.storage.applicationdata.localsettings.aspx) property |
| **System.IO.Stream** class | Still supported, but use ReadAsync() and WriteAsync() instead of BeginRead()/EndRead() and BeginWrite()/EndWrite(). |
| Wallet | |
| **Microsoft.Phone.Wallet** namespace | [**Windows.ApplicationModel.Wallet**](https://msdn.microsoft.com/library/windows/apps/dn631399) namespace |
| Xml | |
| (SX = **System.Xml**) | **SX.XmlConvert.ToDateTime** method |
| (SX = **System.Xml**) | **SX.XmlConvert.ToDateTimeOffset** method |


The next topic is [Porting the project](wpsl-to-uwp-porting-to-a-uwp-project.md).

