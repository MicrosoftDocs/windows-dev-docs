---
title: Compare platform features between iOS, Android, and Windows 10.
description: View a detailed comparison of development concepts and platform features between iOS, Android, and the Universal Windows Platform (UWP) on Windows 10.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 082736c8-2ac3-41b3-b246-e705edc23f34
ms.localizationpriority: medium
---
# Windows apps concept mapping for Android and iOS developers

If you're a developer with Android or iOS skills and/or code, and you want to make the move to Windows 10 and the Universal Windows Platform (UWP), then this resource has all you need to map platform features—and your knowledge—between the three platforms.

Also see the porting content in [Move from iOS to UWP](ios-to-uwp-root.md). This document is also available as a [download](https://www.microsoft.com/download/details.aspx?id=52041).

## User-interface (UI)


<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Design language.</strong><br><br>A set of conventions that prescribe how apps on the platform should look and behave.</td>
<td align="left"><strong>Android Material Design</strong> guidelines provide a visual language for Android designers and developers to follow.</td>
<td align="left"><strong>Human Interface Guidelines</strong> provide advice for iOS designers and developers.</td>
<td align="left"><a href="https://developer.microsoft.com/windows/apps/design"><strong>UWP Windows Apps Design</strong></a> shows you how to create an app that looks fantastic on all Windows 10 devices. You will find user-interface (UI) design fundamentals, responsive design techniques, and a full list of detailed guidelines.<br/></td>
</tr>
<tr class="even">
<td align="left"><strong>User interface markup language.</strong> <br><br>A markup language that renders and describes a UI and its components. Each platform provides an editor for both visual and markup editing.<br/></td>
<td align="left"><strong>XML layouts</strong>, edited using <strong>Android Studio</strong> or <strong>Eclipse</strong>.</td>
<td align="left"><strong>XIB</strong> and <strong>Storyboards</strong> edited using <strong>Interface Builder</strong> inside Xcode.</td>
<td align="left"><strong><a href="https://docs.microsoft.com/windows/uwp/xaml-platform/xaml-overview">XAML</a></strong>, edited using <strong><a href="https://visualstudio.microsoft.com/">Microsoft Visual Studio</a></strong> and <strong><a href="https://docs.microsoft.com/visualstudio/designers/creating-a-ui-by-using-blend-for-visual-studio?view=vs-2015">Blend for Visual Studio</a></strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/xaml-platform/index">XAML platform</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/design/basics/xaml-basics-ui">Create a UI with XAML</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/layouts-with-xaml">Define Layouts with XAML</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Built-in user interface controls.</strong> <br><br>Reusable UI elements provided by the platform such as buttons, list controls, and text controls.</td>
<td align="left">Prebuilt <strong>view</strong> and <strong>view group</strong> classes referred to as widgets, layouts, text fields, containers, date/time controls and expert controls.</td>
<td align="left"><strong>Views</strong> and <strong>controls</strong> found in the Xcode object library and listed in the UIKit user interface catalog. Views include image views, picker views and scroll views. Controls include buttons, date pickers and text fields.</td>
<td align="left">The XAML platform provides you with a generous set of <strong>built-in controls</strong> such as buttons, list controls, panels, text controls, command bars, pickers, media, and inking.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/controls-and-events-intro">Add controls and handle events</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Control event-handling.</strong> <br><br>Defining the logic that runs when events are triggered within UI controls.</td>
<td align="left"><strong>Event handlers</strong> and <strong>event listeners</strong> are added in XML or programmatically.</td>
<td align="left">Controls send <strong>action</strong> messages to <strong>targets</strong>.</td>
<td align="left">You can define methods to handle the events of a XAML control in a <strong>code-behind file</strong> attached to the XAML page. <strong>Event handlers</strong> are always written in code. But you can hook those handlers to events either in XAML markup or in code.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/controls-and-events-intro">Add controls and handle events</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/xaml-platform/events-and-routed-events-overview">Events and routed events overview</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Data binding.</strong> <br><br>A software design pattern that allows your app UI to render data and optionally stay in sync with that data.</td>
<td align="left">There is a <strong>Data Binding Library</strong> provided, although it is still in beta.</td>
<td align="left">No built-in bindings system exists on iOS. <strong>Key-value observing</strong> can be built upon to perform data binding, either with the use of a third-party library, or writing additional code. Controls use a delegate/callback approach for obtaining data.</td>
<td align="left">The UWP platform handles <strong>data binding</strong> for you. You use the <strong><a href="https://docs.microsoft.com/windows/uwp/xaml-platform/x-bind-markup-extension">{x:Bind}</a></strong> markup extension to take advantage of high performance binding or <strong><a href="https://docs.microsoft.com/windows/uwp/xaml-platform/binding-markup-extension">{Binding}</a></strong> to take advantage of more features. It’s then just a case of configuring your binding to choose whether the platform uses <strong>one-way binding</strong> to display values from a data source in your UI, or whether it also observes those values and updates your UI when they change with <strong>two-way binding</strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/data-binding/index">Data Binding</a></td>
</tr>
<tr class="even">
<td align="left"><strong>UI Automation.</strong> <br><br>Programmatic access to UI elements, making apps accessible to assistive technology products and enabling automated test scripts to interact with your UI.</td>
<td align="left"><strong>Text labels</strong>, <strong>contentDescription</strong> and <strong>hint</strong> values help ensure UI elements can be found by automation. Android Studio allows you to write UI tests using the <strong>UI Automator</strong> and <strong>Espresso</strong> testing frameworks.</td>
<td align="left">The <strong>Automation instrument</strong> allows you to write automated UI test scripts which identify elements using the <strong>accessibility</strong> settings or the element's position in the <strong>element hierarchy</strong>.</td>
<td align="left">You get programmatic access to built-in UI elements in UWP out-of-box with <strong><a href="https://docs.microsoft.com/windows/desktop/WinAuto/uiauto-uiautomationoverview">UI Automation</a></strong>.<br/><strong><a href="https://docs.microsoft.com/windows/uwp/accessibility/custom-automation-peers">Custom Automation Peers</a></strong> allow you to provide automation support for your own custom UI classes. The <strong><a href="https://docs.microsoft.com/visualstudio/test/use-ui-automation-to-test-your-code?view=vs-2015">Coded UI Test Project</a></strong> in Visual Studio allows you to automatically test your whole application through the UI, or to test the UI in isolation.</td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Changing the appearance of a control.</strong> <br><br>Editing size, color and other attributes.</td>
<td align="left">Controls have <strong>properties</strong> which can be edited using the designer tool, in XML markup or programmatically.</td>
<td align="left">Controls have <strong>attributes</strong> which you can edit using the <strong>Attributes Inspector</strong> in Interface Builder or programmatically.</td>
<td align="left">You can edit the <strong>properties</strong> of controls in the XAML markup or programmatically, using Visual Studio and Blend for Visual Studio.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/controls-and-events-intro">Add controls and handle events</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Reusable visual styles.</strong> <br><br>Apply visual changes to a number of controls, in a reusable format.</td>
<td align="left"><strong>XML styles</strong> are sets of properties that are applied to one or more controls.</td>
<td align="left">iOS does not support reusable visual styles out-of-box, but the UIAppearance protocol allows multiple controls to share common attributes.</td>
<td align="left">You can create reusable <strong><a href="https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Style">styles</a></strong>, which can be applied to multiple controls and stored in a <strong><a href="https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.ResourceDictionary">ResourceDictionary</a></strong> for easy reuse.<br/><br/><a href="https://docs.microsoft.com/previous-versions/windows/apps/hh465381(v=win.10)">Quickstart: Styling Controls</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Editing the visual structure of controls.</strong> <br><br>Customize the visual structure of a control beyond just modifying properties or attributes, for example, moving the checkbox text underneath the checkbox.</td>
<td align="left">No simple method of editing the visual structure of controls exists in Android.</td>
<td align="left">No simple method of editing the visual structure of controls exists in iOS.</td>
<td align="left">To customize the visual structure of a control, you can copy and edit its <strong><a href="https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ControlTemplate">control template</a></strong> in XAML markup.<br/><br/><a href="https://docs.microsoft.com/previous-versions/windows/apps/hh465374(v=win.10)">Quickstart: Control Templates</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Built-in touch gestures.</strong> <br><br>Provide customized touch support by handling high level abstracted gesture events such as tap and double tap in views and controls.</td>
<td align="left"><strong>Gesture detectors</strong> detect common touch gestures including scrolling, long-press, tap, double-tap and fling.</td>
<td align="left">UIKit framework provides built-in <strong>gesture recognizers</strong> which detect touch gestures including tap, pinch, pan, swipe, rotate and long-press.</td>
<td align="left"><strong>UI elements</strong> allow you to handle <strong>static gesture events</strong> including tap, double-tap, right-tap and holding, as well as <strong>manipulation gesture events</strong> including slide, swipe, turn, pinch and stretch. Gesture events are <strong>routed events</strong> and can be handled by parent objects containing the child UIElement.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/touch-interactions">Touch interactions</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/design/layout/index">Custom user interactions - gestures, manipulations, and interactions</a></td>
</tr>
</tbody>
</table>
<h2 id="navigation-and-app-structure">Navigation and app structure</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Layouts.</strong> <br><br>The layout defines the structure of the user interface.</td>
<td align="left">Layout is composed of <strong>view groups</strong> such as the <strong>LinearLayout</strong> and the <strong>RelativeLayout</strong> which can nest other view groups or views.</td>
<td align="left">Layout is composed of a <strong>UIViewController</strong> containing <strong>UIView</strong>'s which can be nested.</td>
<td align="left">XAML which provides a flexible layout system composed of <strong>layout panel classes</strong> such as <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.canvas">Canvas</a></strong>, <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.grid">Grid</a></strong>, <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.relativepanel">RelativePanel</a></strong> and <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.stackpanel">StackPanel</a></strong> for static and responsive layouts. <strong><a href="https://docs.microsoft.com/visualstudio/ide/reference/properties-window?view=vs-2015">Properties</a></strong> are used to control the size and position of the elements.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/layouts-with-xaml">Define layouts with XAML</a><br/></td>
</tr>
<tr class="even">
<td align="left"><strong>Peer navigation.</strong> <br><br>Presenting the user with methods of navigating between pages of equal hierarchical importance.</td>
<td align="left"><strong>Tabs</strong>, <strong>swipe views</strong> and <strong>navigation drawers</strong> provide <strong>lateral navigation</strong>.</td>
<td align="left"><strong>Tab bar controllers</strong>, <strong>split view controllers</strong> and <strong>page view controllers</strong> allow navigation between views of equal hierarchy.</td>
<td align="left">You can display a persistent list of links/tabs above the content using <strong><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tabs-pivot">tabs/pivots</a></strong>. The <strong><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/split-view">navigation pane/split view</a></strong> lets you display a list of links alongside the content.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/navigation-basics">Navigation</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/navigate-between-two-pages">Navigate between two pages</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Hierarchical navigation.</strong> <br><br>Navigating between parent and child pages of a hierarchy.</td>
<td align="left"><strong>Lists</strong>, and <strong>grid lists</strong>, <strong>buttons</strong> and other controls provide <strong>descendent navigation</strong> when used with <strong>intents</strong> to load other <strong>activities</strong>.</td>
<td align="left"><strong>Navigation controllers</strong> allow users to navigate between levels of a hierarchy.</td>
<td align="left"><strong><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/hub">Hubs</a></strong> let you show the user a preview of content which can be selected to navigate to child pages. <strong><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/master-details">Master/details</a></strong> let users pick from a list of item summaries which display next to the corresponding detail section.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/navigation-basics">Navigation</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/navigate-between-two-pages">Navigate between two pages</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Back button navigation.</strong> <br><br>Navigating back through an application.</td>
<td align="left">The <strong>back</strong> and <strong>up</strong> buttons inside the action bar provide <strong>ancestral</strong> and <strong>temporal</strong> navigation using the <strong>back stack</strong>.</td>
<td align="left">The <strong>navigation controller</strong> can have a back button added to it.<br/></td>
<td align="left">You can handle software or hardware back button presses easily using the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.frame.backstack">back stack property</a></strong> which allows your users to traverse the <strong>navigation history</strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/navigation-history-and-backwards-navigation">Back button navigation</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Splash screen.</strong> <br><br>Showing an image on app launch, used primarily for branding.</td>
<td align="left">Splash screens are not provided by default, and are implemented by editing the first activities <strong>theme background</strong>.</td>
<td align="left">Apps must either have a <strong>static launch image</strong> or <strong>XIB/storyboard launch file</strong>.</td>
<td align="left">You create a splash screen using an <strong>image</strong> and colored background. <a href="https://docs.microsoft.com/windows/uwp/launch-resume/create-a-customized-splash-screen">Splash screen time can be extended</a>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/add-a-splash-screen">Add a splash screen</a></td>
</tr>
</tbody>
</table>
<h2 id="custom-inputs">Custom inputs</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Voice.</strong> <br><br>Speech recognition for speech input, and additional voice capabilities.</td>
<td align="left">Speech input can be provided by any app which implements a <strong>RecognizerIntent</strong>, such as <strong>Google Voice Search</strong>. The <strong>SpeechRecognizer</strong> class allows apps to use Google's speech recognition API.</td>
<td align="left">Apps can use the <strong>SFSpeechRecognizer</strong> class to implement speech input and speech recognition.</td>
<td align="left">You can use the <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/speech-recognition">speech recognition</a></strong> API to interact with your app in the foreground. You can use speech-based <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/cortana-interactions">Cortana interactions</a></strong> to launch apps in the foreground or background, and to ​interact with background apps.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/speech-interactions">Speech interactions</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Custom user inputs.</strong> <br><br>Handling keyboard, mouse, stylus and other inputs.</td>
<td align="left">Support for interactions includes <strong>touch</strong>, <strong>touchpad</strong>, <strong>stylus</strong>, <strong>mouse</strong> and <strong>keyboard</strong>. Movements and inputs are reported in the same way as touch, but it is possible to detect more information about the <strong>input device</strong>.</td>
<td align="left">Support for <strong>touch</strong>, the <strong>Apple Pencil</strong> and hardware <strong>keyboards</strong> are provided.</td>
<td align="left">You will find support for a wide range of interactions including <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/touch-interactions">touch</a></strong>, <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/touchpad-interactions">touchpad</a></strong>, <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/pen-and-stylus-interactions">pen/stylus</a></strong> with digital ink, <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/mouse-interactions">mouse</a></strong> and <strong><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/keyboard-interactions">keyboard</a></strong>. Your apps can handle the data without needing to know which input device was used, and raw input device data can be accessed if needed.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/input-and-devices/handle-pointer-input">Handle pointer input</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/design/layout/index">Custom user interactions</a></td>
</tr>
</tbody>
</table>
<h2 id="data">Data</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Local app data.</strong> <br><br>Storing settings and files related to your app locally.</td>
<td align="left">Local files can be saved using <strong>openFileOutput</strong> and <strong>openFileInput</strong>. Settings in a <strong>shared preferences file</strong> can be accessed using <strong>getSharedPreferences</strong>.</td>
<td align="left">Local files can be stored in the <strong>application support</strong> directory, accessed via the <strong>NSFileManager</strong> class. Settings in <strong>preferences</strong> files can be accessed by the <strong>NSUserDefaults</strong> class.</td>
<td align="left">The <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Storage">Windows.Storage</a></strong> classes handle local data storage for you in a unified way. You store settings as an <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Storage.ApplicationDataContainer">ApplicationDataContainer</a></strong> object, accessed via the <strong><a href="https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.localsettings">ApplicationData.LocalSettings</a></strong> property. You store files in a <strong><a href="https://docs.microsoft.com/uwp/api/windows.storage.storagefolder">StorageFolder</a></strong> object accessed via the <strong><a href="https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.localfolder">ApplicationData.LocalFolder</a></strong> property.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/app-settings/store-and-retrieve-app-data">Store and retrieve settings and other app data</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Local database storage.</strong> <br><br>Storing app data in a relational database, with object-relational mappers (ORM) if applicable.</td>
<td align="left">The <strong>SQLite</strong> database is provided. No ORM is built-in. SQL queries are run using the <strong>SQLiteDatabase</strong> class.</td>
<td align="left">The <strong>SQLite</strong> database is provided. <strong>CoreData</strong> is the built-in object graph framework which can be used with SQLite and provide functionality comparable with an ORM.</td>
<td align="left">You can store data using <strong>SQLite</strong>. <strong><a href="https://docs.microsoft.com/windows/uwp/data-access/entity-framework-7-with-sqlite-for-csharp-apps">Entity Framework</a></strong> is a built-in ORM which eliminates the need to write lots of data access code and enables you to easily query the database without writing SQL. You can run SQL queries directly with the <a href="https://docs.microsoft.com/windows/uwp/data-access/sqlite-databases">SQLite library</a>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/data-access/index">Data Access</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>HTTP libraries for REST access.</strong> <br><br>Built-in libraries that let you communicate with web services and web servers using HTTP(S).<br/></td>
<td align="left">HTTP libraries <strong>HttpURLConnection</strong> and <strong>Volley</strong>.</td>
<td align="left"><strong>NSURLSession</strong>, <strong>NSURLConnection</strong> and <strong>NSURLDownload</strong>.</td>
<td align="left">You can use the built-in <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient">HttpClient</a></strong> API to access common HTTP functionality including GET, DELETE, PUT, POST, common authentication patterns, SSL, cookies and progress info.</td>
</tr>
<tr class="even">
<td align="left"><strong>Cloud backup services.</strong> <br><br>Platform-provided backup services for app data.</td>
<td align="left">Android's <strong>backup manager</strong> handles the backing up of application data in Google's <strong>Android Backup Service</strong>.</td>
<td align="left"><strong>iCloud Backup</strong> can be configured by a user to handle their backups, including app data. Apps which use iCloud compatible <strong>Core Data</strong>, the <strong>iCloud key-value store</strong> and <strong>iCloud document storage</strong>.</td>
<td align="left">Any app data that you store using the roaming <strong><a href="https://docs.microsoft.com/uwp/api/windows.storage.applicationdata">ApplicationData APIs</a></strong> (including <strong><a href="https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.roamingfolder">RoamingFolder</a></strong> and <a href="https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.roamingsettings"><strong>RoamingSettings</strong></a>) will be automatically synced to the cloud and to the user’s other devices, too. The syncing is done by way of the user’s Microsoft account.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/design/app-settings/store-and-retrieve-app-data">Guidelines for roaming app data</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>HTTP file downloads.</strong> <br><br>Downloading large and small files over HTTP.</td>
<td align="left"><strong>URLConnection</strong> and <strong>HTTPURLConnection</strong> are used to download over HTTP and FTP, it is also possible to make use of the system <strong>download manager</strong> to download in the background.</td>
<td align="left"><strong>NSURLSession</strong> and <strong>NSURLConnection</strong> can be used to download files over HTTP and FTP.</td>
<td align="left">The <strong><a href="https://docs.microsoft.com/uwp/api/windows.networking.backgroundtransfer">background transfer API</a></strong> lets you reliably transfer files over HTTP(S) and FTP, taking into account app suspension, connectivity loss and adjusting based on connectivity and battery life. You can also use <strong><a href="https://docs.microsoft.com/uwp/api/windows.web.http.httpclient">HttpClient</a></strong> which is ideal for smaller files.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/which-networking-technology">Which networking technology?</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/background-transfers">Background transfers</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Sockets.</strong> <br><br>Creating low level UDP datagram and TCP sockets to communicate with other devices using your own protocol.</td>
<td align="left"><strong>Socket</strong> class provides TCP sockets, <strong>DatagramSocket</strong> class provides a UDP socket.</td>
<td align="left"><strong>NSStream</strong> and <strong>CFStream</strong> provide TCP sockets, <strong>CFSocket</strong> provides UDP sockets.</td>
<td align="left">You can use the <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.DatagramSocket">DatagramSocket</a></strong> class to communicate using a UDP datagram socket and the <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket">StreamSocket</a></strong> class to communicate over TCP or Bluetooth RFCOMM.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/networking-basics">Networking basics</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/which-networking-technology">Which networking technology?</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/sockets">Sockets overview</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>WebSockets.</strong> <br><br>Provide two-way communication between a client and server, enabling real-time data transfer.</td>
<td align="left">No built-in WebSockets libraries exist on Android.</td>
<td align="left">No built-in WebSockets libraries exist on iOS.</td>
<td align="left">Secure connections to servers supporting WebSockets can be made with the <strong><a href="https://docs.microsoft.com/uwp/api/windows.networking.sockets.messagewebsocket">MessageWebSocket</a></strong> class for smaller messages with receipt notifications and <strong><a href="https://docs.microsoft.com/uwp/api/windows.networking.sockets.streamwebsocket">StreamWebSocket</a></strong> for larger binary file transfers which can be read in sections.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/networking-basics">Networking basics</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/which-networking-technology">Which networking technology?</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/networking/websockets">WebSockets overview</a></td>
</tr>
<tr class="even">
<td align="left"><strong>OAuth libraries.</strong> <br><br>OAuth libraries allowing access to third party OAuth providers, and any account management built into the platform.</td>
<td align="left">No generic OAuth library is provided. The <strong>GoogleAuthUtil</strong> class is provided for OAuth authentication with Google Play Services .<br/></td>
<td align="left">No generic OAuth library is provided. The <strong>accounts framework</strong> provides access to user accounts already stored on the device such as Facebook and Twitter.</td>
<td align="left">The generic OAuth library <strong><a href="https://docs.microsoft.com/windows/uwp/security/web-authentication-broker">Web authentication broker</a></strong> lets you connect to third-party identity provider services. The <strong><a href="https://docs.microsoft.com/windows/uwp/security/credential-locker">Credential locker</a></strong> allows your users to save their login and use it on multiple devices. The <strong><a href="https://docs.microsoft.com/previous-versions/office/developer/onedrive-live-sdk-reference/dn896755(v=office.15)">Microsoft.Live</a></strong> namespace lets you easily access Live SDK OAuth for access to Microsoft services.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/security/authentication-and-user-identity">Authentication and user identity</a><br/><br/><a href="https://docs.microsoft.com/uwp/api/windows.security.authentication.web">Windows.Security.Authentication.Web API documentation</a><br/><br/><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/WebAuthenticationBroker">WebAuthenticationBroker code example</a></td>
</tr>
</tbody>
</table>
<h2 id="tooling">Tooling</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>IDE.</strong> <br><br>The toolset used to create your app.</td>
<td align="left"><strong>Android Studio</strong> and <strong>Eclipse</strong>, with Google pushing developers toward the use of Android Studio.</td>
<td align="left"><strong>Xcode</strong></td>
<td align="left"><strong><a href="https://visualstudio.microsoft.com/features/universal-windows-platform-vs">Visual Studio</a></strong> and <strong><a href="https://docs.microsoft.com/visualstudio/designers/creating-a-ui-by-using-blend-for-visual-studio?view=vs-2015">Blend for Visual Studio</a></strong> has all the tools you need to code, design, connect, debug, analyze, optimize and test UWP apps. Visual Studio also provides you with <strong><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/test-with-the-emulator">emulators</a></strong> for Windows 10 devices, so you can test your app across a range of emulated devices.<br/><br/><a href="https://developer.microsoft.com/windows/downloads">Downloads and tools for UWP</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Code organization.</strong> <br><br>The basic folder structure of an app, often created from an initial template.</td>
<td align="left"><strong>AndroidManifest</strong> file, <strong>java</strong> folder containing source files, <strong>res</strong> folder with resources including layouts and values, <strong>Gradle</strong> build scripts in Android Studio and <strong>Ant</strong> build scripts in Eclipse.</td>
<td align="left">Source files and <strong>Supporting Files</strong>, <strong>Info.plist</strong> file, <strong>Main.storyboard</strong> and <strong>LaunchScreen.storyboard</strong>. Images are stored in <strong>Asset libraries</strong>.</td>
<td align="left">Your UWP app contains XAML and code files for your app named Example.xaml and Example.xaml.cs, various images in the <strong>Assets folder</strong>, a start page such as <strong>MainPage.xaml</strong> and <strong>MainPage.xaml.cs</strong> and a manifest.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/get-started/create-a-hello-world-app-xaml-universal">Create a hello world app</a></td>
</tr>
</tbody>
</table>
<h2 id="app-lifecycle">App lifecycle</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>App lifecycle.</strong> <br><br>Handling events on app launch, suspension, resume and close, providing an opportunity to save/restore application state and run other tasks.</td>
<td align="left">Each activity has its own <strong>activity lifecycle</strong> with states such as <strong>resumed</strong>. <strong>Lifecycle callbacks</strong> such as <strong>onResume</strong> are implemented in your <strong>activity classes</strong>.</td>
<td align="left">The <strong>application lifecycle</strong> has states such as <strong>suspended</strong>. Methods such as <strong>applicationDidEnterBackground:</strong> are implemented in the <strong>application delegate object</strong> to run code on state changes.</td>
<td align="left">Your application has the <strong>app execution states</strong> NotRunning, Activated, Running, Suspending, Suspended and Resuming.<br/><br/>You can implement the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.application">Application class</a></strong> methods OnLaunched, OnActivated, Suspending or Resuming in your app to run code when the state changes.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle">App lifecycle</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Background tasks.</strong> <br><br>Tasks that perform background operations and continue to run when the app is no longer in the foreground.</td>
<td align="left">Apps can launch <strong>services</strong> which perform background operations when the app is no longer in the foreground. Services have their own <strong>lifecycle</strong> and are registered in the manifest.</td>
<td align="left"><strong>Background execution</strong> is only permitted for specific task types.<br/><br/>Apps declare <strong>supported background tasks</strong> in the Info.plist file using the <strong>UIBackgroundModes</strong>.<br/><br/>The system controls when background tasks are run and for how long.</td>
<td align="left">You can create a background task by implementing the <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.ibackgroundtask">IBackgroundTask</a></strong> interface and registering the task in the application manifest. You can set a task to be triggered with a <a href="https://docs.microsoft.com/windows/uwp/launch-resume/run-a-background-task-on-a-timer-"><strong>timer</strong></a>, <a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.systemtriggertype"><strong>system trigger</strong></a>, and <a href="https://docs.microsoft.com/windows/uwp/launch-resume/use-a-maintenance-trigger"><strong>maintenance trigger</strong></a>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/support-your-app-with-background-tasks">Support your app with background tasks</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/create-and-register-a-background-task">Create and register a background task</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/guidelines-for-background-tasks">Guidelines for background tasks</a></td>
</tr>
</tbody>
</table>
<h2 id="performance">Performance</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Performance best practices.</strong> <br><br>Guidelines for building apps that are fast, responsive, considerate of battery life with a fast startup time.</td>
<td align="left">Android provides the <strong>Best Practices for Performance</strong> training guide.</td>
<td align="left">iOS provides the <strong>Performance Overview</strong> document.</td>
<td align="left">You can read the detailed <strong><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/performance-and-xaml-ui">Performance guide</a></strong> with sections covering topics such as; setting performance goals, measuring performance, memory management, smooth animations, efficient file system access and the tools available for profiling and performance.</td>
</tr>
<tr class="even">
<td align="left"><strong>View optimization for a responsive UI.</strong> <br><br>Improving performance by optimizing views.</td>
<td align="left">Optimizing <strong>layout hierarchies</strong> using the Hierarchy Viewer tool, <strong>reusing layouts</strong> and loading <strong>views on demand</strong> are all techniques to help keep the UI thread responsive and avoid &quot;Application Not Responding&quot; dialogs (<strong>ANR's</strong>).<br/></td>
<td align="left">Fixing UI issues with <strong>offscreen rendering</strong>, <strong>blended layers</strong>, <strong>rasterization</strong> using the <strong>Core Animation</strong> tool help keep the UI thread responsive.</td>
<td align="left">You can easily <strong>optimize</strong> XAML <strong>markup</strong> and <strong>layouts</strong> by following a few simple steps. Techniques include reducing layout structure, minimizing the element count and minimizing overdrawing. <br/><br/><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/keep-the-ui-thread-responsive">Keep the UI thread responsive</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/optimize-xaml-loading">Optimize your XAML markup</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/optimize-your-xaml-layout">Optimize your XAML layout</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Threading.</strong> <br><br>Use of threading to maintain a <strong>responsive UI</strong> and run multiple <strong>tasks in parallel</strong>.</td>
<td align="left">Threading is achieved using the classes <strong>Runnable</strong>, <strong>Handler</strong>, <strong>ThreadPoolExecutor</strong>, and the higher level <strong>AsyncTask</strong>.</td>
<td align="left">Threading is achieved using <strong>NSThread</strong>, <strong>Grand Central Dispatch</strong>, and the higher level <strong>NSOperation</strong>.</td>
<td align="left">You can work with threads by submitting <strong>work items</strong> to the <strong>threadpool</strong> with <strong><a href="https://docs.microsoft.com/uwp/api/windows.system.threading.threadpool.runasync">RunAsync</a></strong>. You can use a timer to submit a work item with <strong><a href="https://docs.microsoft.com/uwp/api/windows.system.threading.threadpooltimer.createtimer">CreateTimer</a></strong> and create a repeating work item with <strong><a href="https://docs.microsoft.com/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer">CreatePeriodicTimer</a></strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/threading-async/submit-a-work-item-to-the-thread-pool">Submit a work item to the thread pool</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/threading-async/use-a-timer-to-submit-a-work-item">Use a timer to submit a work item</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/threading-async/create-a-periodic-work-item">Create a periodic work item</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/threading-async/best-practices-for-using-the-thread-pool">Best practices for using the thread pool</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Asynchronous programming.</strong> <br><br>Avoid threading complexity by taking advantage of asynchronous programming patterns to keep the UI thread responsive.</td>
<td align="left">The use of <strong>threading is required</strong> to create your own asynchronous classes. Some built-in classes are asynchronous.</td>
<td align="left">The use of <strong>threading is required</strong> is required to create your own asynchronous classes. Some built-in classes are asynchronous.</td>
<td align="left">You can use asynchronous patterns to avoid blocking the main thread when you create your own APIs, for example, using <strong>async</strong> and <strong>await</strong> in C# and Visual Basic. You can use the asynchronous built-in APIs which end in the word <strong>Async</strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/threading-async/asynchronous-programming-universal-windows-platform-apps">Asynchronous programming</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/threading-async/call-asynchronous-apis-in-csharp-or-visual-basic">Call asynchronous APIs in C# or Visual Basic</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>List view optimization.</strong> <br><br>Built-in patterns to aid with optimizing lists of data, which often have poor performance when large amounts of data need to be shown</td>
<td align="left">The <strong>ViewHolder</strong> design pattern is used to avoid multiple view lookups, which allows you to use reusable UI elements.</td>
<td align="left">A range of optimizations can be made to improve the performance of <strong>UITableView</strong>, nothing is built-in.</td>
<td align="left">You can use the <a href="/uwp/api/windows.ui.xaml.controls.listview">ListView</a> and <a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.gridview">GridView</a> controls which provide <strong>UI virtualization</strong> out-of-box, providing a smooth panning and scrolling experience and a faster startup time. You can also implement <a href="/dotnet/api/system.collections.ilist">IList</a> and <a href="https://docs.microsoft.com/dotnet/api/system.collections.specialized.inotifycollectionchanged">INotifyCollectionChanged</a> in your data source, providing <strong>data virtualization</strong> and further improving performance.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/optimize-gridview-and-listview">ListView and GridView UI optimization</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/debug-test-perf/listview-and-gridview-data-optimization">ListView and GridView data virtualization</a></td>
</tr>
</tbody>
</table>
<h2 id="monetization">Monetization</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>In-app purchases.</strong> <br><br>Platform features that allow users to make purchases in your apps.</td>
<td align="left"><strong>In-app billing</strong> is provided by Google Services. Products are added to the <strong>Google Play Developer Console</strong>. In-app purchases are implemented with the <strong>Google Play Billing Library</strong>.</td>
<td align="left">Products are added to <strong>iTunes Connect</strong>. In-app purchases are implemented using the <strong>StoreKit</strong> framework.<br/><br/>Products are purchased using <strong>SKMutablePayment</strong> and <strong>SKPaymentQueue</strong>.</td>
<td align="left">You create in-app product purchases for your app by <a href="https://docs.microsoft.com/windows/uwp/publish/iap-submissions">adding them to your app and submitting them to the Store</a>. <br/><br/>You use the <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.currentapp">CurrentApp class</a></strong> to define in-app purchases. <br/><br/>You use <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync">CurrentApp.RequestProductPurchaseAsync</a></strong> to display the UI that allows customers to purchase the product.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/monetize/enable-in-app-product-purchases">Enable in-app product purchases</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Consumable in-app purchases.</strong> <br><br>In-app products which can be purchased, used and then purchased again.</td>
<td align="left">Consumable purchases are enabled by making a regular purchase and then consuming it with <strong>consumePurchase</strong>, enabling it to be purchased, used, and then purchased again.</td>
<td align="left">Consumable products are <strong>defined as consumable products</strong> in iTunes Connect.</td>
<td align="left">You can support consumables by <a href="https://docs.microsoft.com/windows/uwp/publish/enter-iap-properties">defining their product type as Consumable when you submit them to</a> the Store. You then call <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.currentapp.reportconsumablefulfillmentasync">CurrentApp.ReportConsumableFulfillmentAsync</a></strong> after a consumable purchase has been made to allow the customer to access it.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/monetize/enable-consumable-in-app-product-purchases">Enable consumable in-app purchases</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Testing in-app purchases.</strong> <br><br>Enabling you to test your in-app purchase code without putting your app in the Store.</td>
<td align="left">The <strong>in-app billing sandbox</strong> is used for testing.</td>
<td align="left"><strong>Sandbox tester accounts</strong> are used for testing.</td>
<td align="left">You can test in-app purchases by simply using the <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.currentappsimulator">CurrentAppSimulator</a></strong> class in place of CurrentApp.<br/><br/></td>
</tr>
<tr class="even">
<td align="left"><strong>Trials.</strong> <br><br>Enabling you to easily limit content or remove advertising based on a trial version of an app.</td>
<td align="left">Google Play <strong>doesn't officially support app trials</strong>. Trials or removing advertising is achieved by creating an in-app purchase and taking the appropriate code path when confirming the purchase was successful.</td>
<td align="left">The App Store <strong>doesn't officially support app trials</strong>. Trials or removing advertising is achieved by creating an in-app purchase and taking the appropriate code path when confirming the purchase was successful.</td>
<td align="left">You can offer a free trial version of your app by using the <strong><a href="https://docs.microsoft.com/windows/uwp/publish/set-app-pricing-and-availability">'Free Trial' option</a></strong> when submitting your app to the Store. You can then use <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.licenseinformation.istrial">LicenseInformation.IsTrial</a></strong> to check the trial status of the app and present different code paths accordingly. You can register for the <a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.licenseinformation.licensechanged">LicenseChanged event</a> to be notified when the user changes the trial status while the app is running.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/monetize/exclude-or-limit-features-in-a-trial-version-of-your-app">Exclude or limit features in a trial version</a></td>
</tr>
</tbody>
</table>
<h2 id="adapting-to-multiple-platforms">Adapting to multiple platforms</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Adaptive UI: flexible layouts.</strong> <br><br>Supporting different screen sizes with a flexible height and width.</td>
<td align="left">Flexible layouts can be achieved using the <strong>wrap_content</strong> and <strong>match_parent</strong> values in LinearLayout objects, or by making use of RelativeLayout objects for alignment.</td>
<td align="left">Flexible layouts can be achieved using the <strong>adaptive model</strong> with universal Storyboards, making use of <strong>Auto Layout</strong> with <strong>constraints</strong> and <strong>traits</strong> such as horizontalSizeClass and displayScale which are applied to view controllers.</td>
<td align="left">You can create a fluid layout using <strong>layout properties</strong> and <strong>panels</strong> with a combination of fixed and dynamic sizing.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/layouts-with-xaml">Define layouts with XAML - layout properties and panels</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/screen-sizes-and-breakpoints-for-responsive-design">Responsive design 101</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Adaptive UI: tailored layouts.</strong> <br><br>Supporting different screen sizes with separate targeted layouts.</td>
<td align="left">Providing alternative layout files for different screen configurations in the resources directory using <strong>configuration qualifiers</strong> such as <strong>small</strong>, <strong>large</strong>, <strong>ldpi</strong>, and <strong>hdpi</strong> allows you to target custom layouts to screens of varying size and density.</td>
<td align="left">Define a <strong>separate iPhone and iPad Storyboard</strong> to tailor layouts to different device families in a universal app.</td>
<td align="left">You can build a tailored layout by defining <strong>different XAML markup files</strong> per device family.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/layouts-with-xaml">Define layouts with XAML - tailored layouts</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Adaptive UI: responsive layouts.</strong> <br><br>Responding to changes in screen size, such as rotation, or a change in the size of a window.</td>
<td align="left">Use of flexible layouts with <strong>LinearLayout</strong> and <strong>RelativeLayout</strong>, or providing alternative layout files for different orientations enable responsive layouts.</td>
<td align="left">When the <strong>size</strong> or <strong>traits</strong> of a view change, the <strong>constraints</strong> specified in storyboards are applied.</td>
<td align="left">You can easily reflow, reposition, resize, reveal, or replace sections of your UI at runtime in response to window size changes using <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.visualstate">VisualState</a></strong>, the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.visualstatemanager">VisualStateManager</a></strong> and <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.adaptivetrigger">AdaptiveTrigger</a></strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/layouts-with-xaml">Define layouts with XAML - visual states and state triggers</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/layout/screen-sizes-and-breakpoints-for-responsive-design">Responsive design 101</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Supporting different device capabilities.</strong> <br><br>Take advantage of advanced hardware features while still supporting devices without them.</td>
<td align="left">Testing for device features at runtime using <strong>PackageManager.hasSystemFeature</strong> enables you to decide if hardware specific code can run.</td>
<td align="left">There is <strong>no single check</strong> you can perform at runtime to test for device features, you test for each feature in a specific way to decide if hardware specific code can be run.</td>
<td align="left">You can add <strong>platform extension SDKs</strong> to your package to target additional functionality found in different device families including phone, desktop, and IoT. You use the <strong><a href="https://docs.microsoft.com/uwp/api/windows.foundation.metadata.apiinformation">ApiInformation API</a></strong> to test for the presence of types and members at runtime, and can call those types and members only if they're present.</td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Supporting different device capabilities.</strong> <br><br>Take advantage of advanced hardware features while still supporting devices without them.</td>
<td align="left">The <strong>Android Support Library</strong> can be packaged with your app to make some newer APIs available to those with older versions of Android. Testing for the API level at runtime can be done using <strong>Build.Version.SDK_INT</strong>.</td>
<td align="left">Standard runtime checks are used to find out if APIs are available, such as the <strong>class</strong> method to check if a class exists and <strong>respondsToSelector:</strong> to check for methods on classes.</td>
<td align="left">You can use <strong><a href="https://docs.microsoft.com/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent">ApiInformation.IsApiContractPresent</a></strong> to identify if an API contract with a specified major and minor number is present. You also use the <strong><a href="https://docs.microsoft.com/uwp/api/windows.foundation.metadata.apiinformation">ApiInformation API</a></strong> to test for the presence of types and members at runtime, and can call those types and members only if they're present.</td>
</tr>
</tbody>
</table>
<h2 id="notifications">Notifications</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Tiles and badges.</strong> <br><br>Present updates to users on the home screen.</td>
<td align="left"><strong>App Widgets</strong> are views on your application that can be embedded into the home screen and can receive periodic updates. <strong>No badge system</strong> exists on Android. No identical system to tiles exists.</td>
  <td align="left"><strong>Widgets</strong> on iOS mappear in the Notification Center and are implemented as <strong>App Extensions</strong>. You can also add a <strong>badge</strong> to your icon with a number which can change in response to local or remote notifications. There is no tiles system.</td>
<td align="left">Your app has a <strong>tile</strong> which can be pinned to the start screen and is used to display your choice of text, images, and a <strong>badge</strong> with glyphs and numbers. You can update the content of tiles from the app; via push notifications or at predefined schedules. Tiles can be adaptive, and can change according to where they are being displayed.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-creating-tiles">Create tiles</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-create-adaptive-tiles">Create adaptive tiles</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-choosing-a-notification-delivery-method">Choose a notification delivery method</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-creating-tiles">Guidelines for tiles and badges</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Displaying notifications.</strong> <br><br>Types of notifications that can be displayed.</td>
<td align="left">Notifications can be shown in the <strong>notification area</strong> and <strong>notification drawer</strong>, <strong>heads-up notifications</strong> present a notification in a small floating window. Notifications can have actions added to them by defining a <strong>PendingIntent</strong>.</td>
<td align="left">Pop-up notifications appear as <strong>banners</strong> or <strong>alerts</strong>. You can add custom action buttons to <strong>actionable notifications</strong> which are defined with <strong>UIMutableUserNotificationAction</strong>.</td>
<td align="left">You can create adaptive pop-up notifications called <strong>toast notifications</strong>. You can define toasts in XML with visual content, <strong>actions</strong> which can be buttons, or inputs and audio.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-adaptive-interactive-toasts">Adaptive and interactive toast notifications</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-choosing-a-notification-delivery-method">Choose a notification delivery method</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-badges-notifications">Guidelines for toast notifications</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Scheduling local notifications.</strong> <br><br>Local notifications sent by your app at a scheduled time.</td>
<td align="left">Notifications and actions are defined using a <strong>NotificationCompat.Builder</strong> and can be scheduled and handled in-app using <strong>AlarmManager</strong> and <strong>BroadcastReceiver</strong>.</td>
<td align="left">Local notifications are created using <strong>UILocalNotification</strong>, and can be scheduled with <b> UILocalNotification.scheduleLocalNotification:<strong>. | You can schedule a toast notification using </strong><a href="https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.ScheduledToastNotification">ScheduledToastNotification</a><strong>. You can send a tile notification from your app using the </strong><a href="https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.TileNotification">TileNotification class</a><strong>, or schedule a tile notification with <a href="https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.ScheduledTileNotification">ScheduledTileNotification</a>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-adaptive-interactive-toasts">Adaptive and interactive toast notifications</a><br/><br/><a href="/windows/uwp/controls-and-patterns/tiles-and-notifications-sending-a-local-tile-notification">Send a local tile notification</a> | | </strong>Sending push notifications.</b> A notification sent from a push notification server and optionally handled in-app.</td>
<td align="left"><strong>Google Cloud Messaging</strong> provides push notification support for Android.</td>
</tr>
</tbody>
</table>
<h2 id="media-capture-and-rendering">Media capture and rendering</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Capturing media.</strong> <br><br>Recording audio and visual content.</td>
<td align="left">Using an <strong>intent</strong> such as MediaStore.ACTION_VIDEO_CAPTURE allows media to be captured with an existing camera app. Using the <strong>android.hardware.camera2</strong> or <strong>camera</strong> library enables the implementation of a custom camera interface. <strong>MediaRecorder</strong> APIs can be used to capture audio.</td>
<td align="left">The <strong>UIImagePickerController</strong> allows for the capture of video and photos with the system UI. The <strong>AVFoundation</strong> classes such as <strong>AVCaptureSession</strong> enable direct access to the camera. <br/>The <strong>AVAudioRecorder</strong> class enables audio recording.</td>
<td align="left">You can capture photos and video while using the built-in camera UI with the <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUI">CameraCaptureUI class</a></strong>. You can interact with the camera at a low level, and capture audio with classes in <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Media.Capture">Windows.Media.Capture</a></strong> such as the <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture">MediaCapture API</a></strong>. <br/><br/><a href="https://docs.microsoft.com/windows/uwp/audio-video-camera/capture-photos-and-video-with-cameracaptureui">Capture photos and video with CameraCaptureUI</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/audio-video-camera/capture-photos-and-video-with-mediacapture">Capture photos and video with MediaCapture</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Media playback.</strong> <br><br>Playing audio and video files.</td>
<td align="left">The <strong>MediaPlayer</strong> and <strong>AudioManager</strong> classes are used to play audio and video files.</td>
<td align="left">The <strong>AVKit framework</strong>, <strong>AVAudioPlayer</strong>, and <strong>Media Player Framework</strong> are used to play audio and video files.</td>
<td align="left">You can use the <strong><a href="https://docs.microsoft.com/uwp/api/Windows.Media.Core.MediaSource">MediaSource class</a></strong>, <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaelement">MediaElement</a></strong>, and <strong><a href="https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer">MediaPlayer</a></strong> classes to play back audio and video from sources such as local and remote files.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/audio-video-camera/media-playback-with-mediasource">Media playback with MediaSource</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Editing media.</strong> <br><br>Composing new media files from existing recordings and applying special effects.</td>
<td align="left">Low level classes such as <strong>MediaCodec</strong>, <strong>MediaMuxer</strong>, and <strong>android.media.effect</strong> can be used for content editing.</td>
<td align="left">Classes in the <strong>AV Foundation</strong> framework such as <strong>AVMutableComposition</strong>, <strong>AVMutableVideoComposition</strong>, and <strong>AVMutableAudioMix</strong> can be used for content editing.</td>
<td align="left">You can use the <strong><a href="https://docs.microsoft.com/uwp/api/windows.media.editing">Windows.Media.Editing</a></strong> APIs such as <strong><a href="https://docs.microsoft.com/uwp/api/windows.media.editing.mediacomposition">MediaComposition</a></strong> and <strong><a href="https://docs.microsoft.com/uwp/api/windows.media.editing.mediaclip">MediaClip</a></strong> to create media compositions from audio and video files. You are able to add video and image overlays, combine video clips, add background audio, and apply audio and video effects.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/audio-video-camera/media-compositions-and-editing">Media compositions and editing</a></td>
</tr>
</tbody>
</table>
<h2 id="sensors">Sensors</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Sensors.</strong> <br><br>Detect device movement, position and environmental properties.</td>
<td align="left">The <strong>sensor framework</strong> is used to access hardware and software sensors with classes such as <strong>SensorManager</strong> and <strong>SensorEvent</strong>.</td>
<td align="left">The <strong>Core Motion framework</strong> is used to access raw and processed sensor data.</td>
<td align="left">You can use classes in <strong><a href="https://docs.microsoft.com/uwp/api/windows.devices.sensors">Windows.Devices.Sensors</a></strong> to access sensor readings and events triggered when new reading data is received from the sensor.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/devices-sensors/sensors">Sensors</a></td>
</tr>
</tbody>
</table>
<h2 id="location-and-mapping">Location and mapping</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Location.</strong> <br><br>Finding the device's <strong>current</strong> location and tracking <strong>changes</strong>.</td>
<td align="left">The Google Play services location APIs provide high-level access to the <strong>last known location</strong> with the <strong>fused location provider</strong> using the <strong>getLastLocation</strong> and <strong>requestLocationUpdates</strong> methods. Low-level access is provided in the Android libraries with the <strong>LocationManager</strong>.</td>
<td align="left">The <strong>Core Location</strong> <strong>CLLocationManager</strong> class is used to monitor a device's location, with <strong>startUpdatingLocation</strong> for the standard location service and <strong>startMonitoringSignificantLocationChanges</strong> for the <strong>significant-change</strong> location service.</td>
<td align="left">You can track device location with classes in <strong><a href="https://docs.microsoft.com/uwp/api/windows.devices.geolocation">Windows.Devices.Geolocation</a></strong>. Use <strong><a href="https://docs.microsoft.com/uwp/api/windows.devices.geolocation.geolocator.getgeopositionasync">Geolocator.GetGeopositionAsync</a></strong> for a one-time reading. Use <strong><a href="https://docs.microsoft.com/uwp/api/windows.devices.geolocation.geolocator.positionchanged">Geolocator.PositionChanged</a></strong> to obtain the location regularly using a timer, or be informed when the location has changed.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/maps-and-location/get-location">Get the user's location</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Displaying maps.</strong> <br><br>Displaying an <strong>interactive built-in map</strong> and adding <strong>points of interest</strong>.</td>
<td align="left">The <strong>GoogleMap</strong>, <strong>MapFragment</strong>, and <strong>MapView</strong> classes within the <strong>Google Maps Android API</strong> allow maps to be embedded in apps. Points of interest can be displayed using <strong>markers</strong> and the customizable <strong>Marker</strong> class.</td>
<td align="left">Maps are embedded into iOS apps with the <strong>MKMapView</strong> class in the <strong>MapKit framework</strong>. <strong>Annotations</strong> can be added to apps to display points of interest using object classes such as <strong>MKPointAnnotation</strong> and view classes such as <strong>MKPinAnnotationView</strong>.</td>
<td align="left">You can embed maps in your apps using the built-in <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapcontrol">MapControl</a></strong> XAML control which provides 2D, 3D, and streetside views. You can add points of interest with a pushpin, image, or shape using classes such as <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapicon">MapIcon</a></strong>, <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mappolygon">MapPolygon</a></strong> and <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mappolyline">MapPolyline</a></strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/maps-and-location/display-maps">Display maps with 2D, 3D, and Streetside views</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/maps-and-location/display-poi">Display points of interest (POI) on a map</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Geofencing.</strong> <br><br>Monitor the entering and leaving of a particular geographic region.</td>
<td align="left">Geofences are monitored using the <strong>Location Services</strong> in the Google Play Services SDK.</td>
<td align="left">Regions are monitored with the <strong>CLCircularRegion</strong> class and registered with the <strong>CLLocationManager.startMonitoringForRegion:</strong>.</td>
<td align="left">You can create a geofence with the <strong><a href="https://docs.microsoft.com/uwp/api/windows.devices.geolocation.geofencing.geofence">Geofence</a></strong> class and define your <strong>monitored states</strong> such as entering or leaving a region. Handle geofence events in the foreground with the <strong><a href="https://docs.microsoft.com/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor">GeofenceMonitor class</a></strong>, and in the background with the <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.locationtrigger">LocationTrigger background class</a></strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/maps-and-location/set-up-a-geofence">Set up a geofence</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Geocoding and reverse geocoding.</strong> <br><br>Converting addresses to geographic locations (geocoding) and converting geographic locations to addresses (reverse geocoding).<br/></td>
<td align="left">The <strong>Geocoder</strong> class is used for geocoding and reverse geocoding.</td>
<td align="left">The <strong>CLGeocoder</strong> class is used for geocoding.</td>
<td align="left">You can perform geocoding using the <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps.maplocationfinder">MapLocationFinder class</a></strong> in <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps">Windows.Services.Maps</a></strong>. You use <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps.maplocationfinder.findlocationsasync">FindLocationsAsync</a></strong> for geocoding and <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps.maplocationfinder.findlocationsatasync">FindLocationsAtAsync</a></strong> for reverse geocoding.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/maps-and-location/geocoding">Perform geocoding and reverse geocoding</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Routes and directions.</strong> <br><br>Providing routes, distances, and directions between two geographical locations.</td>
<td align="left">Google provides the web service <strong>Google Maps Directions API</strong> which can be used on Android although no SDK is provided.</td>
<td align="left">Map Kit provides the <strong>MKDirections</strong> API which can be used to fetch information about a route and directions.</td>
<td align="left">You can request a walking or driving route with the <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps.maproutefinder">MapRouteFinder</a></strong> class in <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps">Windows.Services.Maps</a></strong>. Routes are returned as a <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps.maproute">MapRoute</a></strong> instance which can be easily shown on a MapControl. Directions are returned inside the <strong><a href="https://docs.microsoft.com/uwp/api/windows.services.maps.maproutemaneuver">MapRouteManeuver</a></strong> object.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/maps-and-location/routes-and-directions">Display routes and directions on a map</a></td>
</tr>
</tbody>
</table>
<h2 id="app-to-app-communication">App-to-app communication</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Invoking another app.</strong> <br><br>Launching another app, and optionally sharing data such as links, text, photos, videos, and files.</td>
<td align="left">An <strong>implicit intent</strong> is used to launch another app, by defining an <strong>action</strong> and optional data in an <strong>Intent</strong> and calling it with <strong>startActivityForResult</strong>.<br/></td>
<td align="left"><strong>App extensions</strong> can be used to provide access to app data to another app. <strong>URL schemes</strong> enable a URL to be passed to another app.</td>
<td align="left">You can launch another app which has registered for a URI with <strong><a href="https://docs.microsoft.com/uwp/api/windows.system.launcher.launchuriasync">Launcher.LaunchUriAsync</a></strong>, or <strong><a href="https://docs.microsoft.com/uwp/api/windows.system.launcher.launchuriforresultsasync">Launcher.LaunchUriForResultsAsync</a></strong> to launch for results and get data back from the launched app. You can use <strong><a href="https://docs.microsoft.com/uwp/api/windows.system.launcher.launchfileasync">Launcher.LaunchFileAsync</a></strong> to pass a file to another app to handle.<br/><br/>You can use a <strong>share contract</strong> to easily share data between apps.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/launch-default-app">Launch the default app for a URI</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/how-to-launch-an-app-for-results">Launch an app for results</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/launch-the-default-app-for-a-file">Launch the default app for a file</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/app-to-app/share-data">Share data</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Allowing your app to be invoked.</strong> <br><br>Allow your app to respond to a request from another app.</td>
<td align="left">Apps register an <strong>intent handling activity</strong> with an <strong>intent filter</strong> to respond to an implicit intent from another app.</td>
<td align="left">Packaging an <strong>app extension</strong> enables data to be shared with other apps. Apps can register a <strong>custom URL scheme</strong> using the <strong>CFBundleURLTypes</strong> key in Info.plist.</td>
<td align="left">You can register your app to be the default handler for a <strong>URI scheme name</strong> by registering a <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation.activationkind#Protocol">protocol</a></strong> in the package manifest and updating the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.onactivated">Application.OnActivated</a></strong> event handler, optionally returning results. In the same way you can register your app to be the default handler for certain file types by adding a declaration in the package manifest and handling the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.onfileactivated">Application.OnFileActivated</a></strong> event.<br/><br/>You can handle share contract requests by registering your app as a share target in the manifest and handling the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.onsharetargetactivated">Application.OnShareTargetActivated</a></strong> event.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/how-to-launch-an-app-for-results">Launch an app for results</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/launch-resume/handle-file-activation">Handle file activation</a><br/><br/><a href="https://docs.microsoft.com/windows/uwp/app-to-app/receive-data">Receive data</a></td>
</tr>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Copy and paste.</strong> <br><br>Copy and pasting text and other content between apps.</td>
<td align="left">The <strong>clipboard framework</strong> can be used to implement copy and paste with the <strong>ClipboardManager</strong> and <strong>ClipData</strong> classes.</td>
<td align="left">The <strong>UIPasteboard</strong>, <strong>UIMenuController</strong>, and <strong>UIResponderStandardEditActions</strong> can be used to implement copy and paste.</td>
<td align="left">Many default XAML controls already support copy and paste. You can implement copy and paste yourself using the <strong><a href="https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.datapackage">DataPackage</a></strong> and <strong><a href="https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.DataTransfer.Clipboard">Clipboard</a></strong> classes in <strong><a href="https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.DataTransfer">Windows.ApplicationModel.DataTransfer</a></strong>.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/app-to-app/copy-and-paste">Copy and paste</a></td>
</tr>
<tr class="even">
<td align="left"><strong>Drag and drop.</strong> <br><br>Dragging and dropping content between apps.</td>
<td align="left">Drag and drop can be implemented within a single application by using the <strong>Android drag/drop framework</strong>.</td>
<td align="left">No high-level drag and drop APIs are provided by iOS.</td>
<td align="left">You can implement dragging and dropping in your app to enable app-to-app, desktop-to-app, and app-to-desktop drag and drop capabilities. You implement drag and drop support in the UIElement class with the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.allowdrop">AllowDrop</a></strong>, and <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.candrag">CanDrag</a></strong> properties, and the <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.dragover">DragOver</a></strong>, and <strong><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.drop">Drop</a></strong> events.<br/><br/><a href="https://docs.microsoft.com/windows/uwp/app-to-app/drag-and-drop">Drag and drop</a></td>
</tr>
</tbody>
</table>
<h2 id="software-design">Software design</h2>
<table style="width:100%">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="40%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><strong>General concept</strong></th>
<th align="left"><strong>Android</strong></th>
<th align="left"><strong>iOS</strong></th>
<th align="left"><strong>Windows 10 UWP</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd" style="background-color: #f2f2f2">
<td align="left"><strong>Software design patterns.</strong> <br><br>Recommended or well-used patterns for the platform.</td>
<td align="left">No formal pattern has been recommended or provided for Android development, although the beta Data Binding Framework may enable more widespread use of the <strong>Model-View-ViewModel (MVVM)</strong> pattern. A number of third party articles and frameworks recommend the <strong>Model-View-Presenter (MVP)</strong> and <strong>MVVM</strong> approaches.</td>
<td align="left"><strong>Model-View-Controller (MVC)</strong> is a common pattern used with iOS and is integrated into the platform.</td>
<td align="left">You are not limited towards a specific pattern when building for UWP.<br/><br/>You can use the built-in <a href="https://docs.microsoft.com/windows/uwp/data-binding/index">data binding</a> pattern to ensure clean separation of data concerns and UI concerns, and avoid having to code up UI event handlers which then update property values.<br/><br/>You can extend data binding to follow the <strong>Model-View-ViewModel (MVVM)</strong> pattern, either by making use of third-party MVVM libraries such as <a href="https://archive.codeplex.com/?p=mvvmlight">MVVM Light Toolkit</a>, or rolling your own and keeping logic out of code-behind.<br/><br/><a href="https://docs.microsoft.com/previous-versions/msp-n-p/hh848246(v=pandp.10)">The MVVM Pattern</a><br/><br/><a href="https://github.com/Windows-XAML/Template10/wiki">Template 10 Visual Studio project templates</a></td>
</tr>
</tbody>
</table>