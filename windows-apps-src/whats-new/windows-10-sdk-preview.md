---
author: QuinnRadich
title: What's New in Windows 10, Version 1607 Preview
description: Windows 10, Version 1607 Preview and new developer tools provide the tools, features, and experiences powered by the new Universal Windows Platform.
keywords: what's new, whats new, update, updates, features, new, Windows 10, 1607 preview
ms.author: quradic
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.assetid: 835d5393-427f-4155-a737-d509ea1de99f
---

# What's New in Windows

Windows 10, Version 1607 Preview and updates to Windows developer tools continue to provide the tools, features, and experiences powered by the Universal Windows Platform. [Install the tools and SDK](http://go.microsoft.com/fwlink/?LinkId=821431) on Windows 10 and you’re ready to either [create a new Universal Windows app](https://msdn.microsoft.com/library/windows/apps/bg124288) or explore how you can use your [existing app code on Windows](https://msdn.microsoft.com/library/windows/apps/mt238321).

## Windows 10, Version 1607 Preview

Feature | Description
 :---- | :----
Networking | You can now provide your own custom validation of server SSL/TLS certificates by subscribing to the [HttpBaseProtocolFilter.ServerCustomValidationRequest](https://msdn.microsoft.com/library/windows/apps/windows.web.http.filters.httpbaseprotocolfilter.aspx#_blank) event. You can also completely disable reading of HTTP responses from the cache by specifying the  [HttpCacheReadBehavior.NoCache](https://msdn.microsoft.com/library/windows/apps/windows.web.http.filters.httpcachereadbehavior.aspx#_blank) enumeration value in an HTTP request. Clearing authentication credentials to enable a "log out" scenario is now possible by calling the [HttpBaseProtocolFilter.ClearAuthenticationCache](https://msdn.microsoft.com/library/windows/apps/windows.web.http.filters.httpbaseprotocolfilter.aspx#_blank) method.
Extensions | New to Microsoft Edge is the ability to use extensions. With extensions, users are able to extend the abilities of Microsoft Edge, providing niche functionality that is important to targeted audiences. Check out the [extensions documentation](https://developer.microsoft.com/microsoft-edge/platform/documentation/extensions/#_blank) for more info.
Bluetooth APIs | Apps are now able to access RFCOMM services on remote Bluetooth peripherals via [Windows.Devices.Bluetooth and Windows.Devices.Bluetooth.Rfcomm](https://msdn.microsoft.com/library/windows/apps/windows.devices.bluetooth.aspx#_blank) without first needing to pair with the peripheral. New methods allow apps to search and access RFCOMM services on non-paired devices.
Chat APIs | With the new [ChatSyncManager](https://msdn.microsoft.com/library/windows/apps/mt414181.aspx#_blank) class, you can sync text messages to and from the cloud.
[Windows apps concept mapping for Android and iOS developers](https://msdn.microsoft.com/windows/uwp/porting/android-ios-uwp-map#_blank) | If you're a developer with Android or iOS skills and/or code, and you want to make the move to Windows 10 and the Universal Windows Platform (UWP), then this resource has all you need to map platform features—and your knowledge—between the three platforms.
[Enterprise data protection (EDP)](https://msdn.microsoft.com/windows/uwp/enterprise/wip-hub) | EDP is a set of features on desktops, laptops, tablets, and phones for Mobile Device Management (MDM). EDP gives an enterprise greater control over how its data (enterprise files and data blobs) is handled on devices that the enterprise manages.
[Windows.ApplicationModel.AppExtensions](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.appextensions.aspx#_blank) | The new AppExtensions namespace allows your UWP app to host content provided by other UWP apps. You can discover, enumerate, and access read-only content from those apps.
Windows IoT | Windows 10 IoT Core allows you to create IoT applications in the familiarity of Windows, and is now available on Raspberry Pi 3 - the newest Raspberry Pi board.
Media APIs | New MediaBreak APIs in the Windows.Media.Playback namespace allow you to easily schedule and manage media breaks when playing back media using MediaSource and MediaPlaybackItem. New AudioGraph APIs in the Windows.Media.Audio namespace add spatial audio processing that lets you assign 3D-positioned emitters and listeners to audio graph nodes.
Maps APIs | The [MapControl](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.maps.mapcontrol.aspx#_blank) has been improved to allow developers to get a visible region that is near to the camera, excluding regions that are far-off in the distance and near the horizon in deeply pitched view. The [MapLocationFinder](https://msdn.microsoft.com/library/windows/apps/windows.services.maps.maplocationfinder.aspx#_blank) class has been extended, allowing developers optimize network traffic when reverse geocoding by specifying a desired accuracy. Developers can now take advantage of downloading offline maps using the [LaunchUriAsync](https://msdn.microsoft.com/library/windows/apps/hh701480.aspx#_blank) method and specifying latitude and longitude. For more info, see [Launch the Windows Maps app](https://msdn.microsoft.com/windows/uwp/launch-resume/launch-maps-app#_blank).
