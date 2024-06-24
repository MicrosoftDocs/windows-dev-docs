---
title: Maps and location overview
description: This section explains how you can display maps, use map services, find the location, and set up a geofence in your app. This section also shows you how to launch the Windows Maps app to a specific map, route, or a set of turn-by-turn directions.
ms.assetid: F4C1F094-CF46-4B15-9D80-C1A26A314521
ms.date: 06/21/2024
ms.topic: article
keywords: windows 10, uwp, map, location, map services
ms.localizationpriority: medium
---
# Maps and location overview

> [!IMPORTANT]
> **Bing Maps for Enterprise service retirement**
>
> The UWP [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services from the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace rely on Bing Maps. Bing Maps for Enterprise is deprecated and will be retired, at which point the MapControl and services will no longer receive data.
>
> For more information, see the [Bing Maps Developer Center](https://www.bingmapsportal.com/) and [Bing Maps documentation](/bingmaps/getting-started/).

This section explains how you can display maps, use map services, find the location, and set up a geofence in your app. This section also shows you how to launch the Windows Maps app to a specific map, route, or a set of turn-by-turn directions.

[**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services require a maps authentication key called a [**MapServiceToken**](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol.mapservicetoken). For more info about getting and setting a maps authentication key, see [Request a maps authentication key](authentication-key.md).

## Display maps

Display maps with 2D, 3D, or Streetside views in your app by using APIs from the [**Windows.UI.Xaml.Controls.Maps**](/uwp/api/Windows.UI.Xaml.Controls.Maps) namespace. You can mark points of interest (POI) on the map by using pushpins, images, shapes, or XAML UI elements. You can also overlay tiled images or replace the map images altogether.

| Topic | Description |
|-------|-------------|
| [Request a maps authentication key](authentication-key.md) | Your app must be authenticated before it can use the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services in the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace. To authenticate your app, you must specify a maps authentication key. This article describes how to request a maps authentication key from the [Bing Maps Developer Center](https://www.bingmapsportal.com/) and add it to your app. |
| [Display maps with 2D, 3D, and Streetside views](display-maps.md) | Display customizable maps in your app by using the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) class. This topic also introduces aerial 3D and Streetside views. |
| [Display points of interest (POI) on a map](display-poi.md) | Add points of interest (POI) to a map by using pushpins, images, shapes, and XAML UI elements. |
| [Overlay tiled images on a map](overlay-tiled-images.md) | Overlay third-party or custom tiled images on a map by using tile sources. Use tile sources to overlay specialized information such as weather data, population data, or seismic data; or use tile sources to replace the default map entirely. |

## Access map services

Add routes, directions, and geocoding capabilities to your app by using APIs from the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace.

| Topic | Description |
|-----------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Request a maps authentication key](authentication-key.md) | Your app must be authenticated before it can use the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services in the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace. To authenticate your app, you must specify a maps authentication key. This article describes how to request a maps authentication key from the [Bing Maps Developer Center](https://www.bingmapsportal.com/) and add it to your app. |
| [Display points of interest (POI) on a map](display-poi.md) | Add points of interest (POI) to a map by using pushpins, images, shapes, and XAML UI elements. |
| [Display routes and directions](routes-and-directions.md) | Request routes and directions, and display them in your app. |
| [Perform geocoding and reverse geocoding](geocoding.md) | Convert addresses to geographic locations (geocoding) and convert geographic locations to addresses (reverse geocoding) by calling the methods of the [**MapLocationFinder**](/uwp/api/Windows.Services.Maps.MapLocationFinder) class in the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace. |
| [Find and download map packages for offline use](/uwp/api/windows.services.maps.offlinemaps)| In the past, your app had to direct users to the Settings app to download offline Maps. Now, you can use classes in the [Windows.Services.Maps.OfflineMaps](/uwp/api/windows.services.maps.offlinemaps) namespace to find downloaded packages in a given area (based on a [Geopoint](/uwp/api/Windows.Devices.Geolocation.Geopoint), [GeoboundingBox](/uwp/api/windows.devices.geolocation.geoboundingbox), etc.). <br> You can also check and listen for the downloaded status of map packages as well as start a download without requiring the user to leave your app. <br> You'll find examples of how to do this in both the reference content and the [Universal Windows Platform (UWP) map sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MapControl).

## Get the user's location

Get the user's current location and be notified when the location changes in your app by using APIs from the [**Windows.Devices.Geolocation**](/uwp/api/Windows.Devices.Geolocation) namespace. These API members are also frequently used in parameters of the maps APIs. APIs from the [**Windows.Devices.Geolocation.Geofencing**](/uwp/api/Windows.Devices.Geolocation.Geofencing) namespace notify your app when the user enters or exits a geofence (a predefined geographical area).

| Topic | Description |
|-------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Request a maps authentication key](authentication-key.md) | Your app must be authenticated before it can use the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services in the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace. To authenticate your app, you must specify a maps authentication key. This article describes how to request a maps authentication key from the [Bing Maps Developer Center](https://www.bingmapsportal.com/) and add it to your app. |
| [Design guidelines for location-aware apps](guidelines-and-checklist-for-detecting-location.md) | Performance guidelines for apps that require access to a user's location. |
| [Get the user's location](get-location.md) | Get access to the user's location, then retreive it. | 
| [Guidelines for using Visits tracking](guidelines-for-visits.md) | Learn how to use the powerful Visits Tracking feature for more practical location tracking. |
| [Design guidance for geofencing](guidelines-for-geofencing.md) | Performance guidelines for apps that utilize the geofencing feature. |
| [Set up a geofence](set-up-a-geofence.md) | Set up a geofence in your app, and learn how to handle notifications in the foreground and background. |

## Launch the Windows Maps app

Your app can launch the Windows Maps app as shown here to display specific maps and turn-by-turn directions. Rather than provide map functionality directly in your own app, consider using the Windows Maps app to provide that functionality. For more info, see [Launch the Windows Maps app](../launch-resume/launch-maps-app.md).

![an example of the windows maps app.](images/mapnyc.png)

## Related topics

* [UWP map sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MapControl)
* [UWP geolocation sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Geolocation)
* [Bing Maps Developer Center](https://www.bingmapsportal.com/)
* [Get current location](get-location.md)
* [Design guidelines for location-aware apps](guidelines-and-checklist-for-detecting-location.md)
* [Design guidelines for maps](./display-maps.md)
* [Design guidelines for privacy-aware apps](../security/index.md)
