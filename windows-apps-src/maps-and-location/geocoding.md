---
author: PatrickFarley
title: Perform geocoding and reverse geocoding
description: This guide shows you how to convert street addresses to geographic locations (geocoding) and convert geographic locations to street addresses (reverse geocoding) by calling the methods of the MapLocationFinder class in the Windows.Services.Maps namespace.
ms.assetid: B912BE80-3E1D-43BB-918F-7A43327597D2
ms.author: pafarley
ms.date: 07/02/2018
ms.topic: article


keywords: windows 10, uwp, geocoding, map, location
ms.localizationpriority: medium
---

# Perform geocoding and reverse geocoding

This guide shows you how to convert street addresses to geographic locations (geocoding) and convert geographic locations to street addresses (reverse geocoding) by calling the methods of the [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class in the [**Windows.Services.Maps**](https://msdn.microsoft.com/library/windows/apps/dn636979) namespace.

> [!TIP]
> To learn more about using maps in your app, download the [MapControl](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MapControl) sample from the [Windows universal samples repo](hhttps://github.com/Microsoft/Windows-universal-samples) on GitHub.

The classes involved in geocoding and reverse geocoding are organized as follows.

-   The [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class contains methods that handle geocoding ([**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925)) and reverse geocoding ([**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928)).
-   These methods both return a [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) instance.
-   The [**Locations**](https://msdn.microsoft.com/library/windows/apps/dn627552) property of the [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) exposes a collection of [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects. 
-   [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects have both an [**Address**](https://msdn.microsoft.com/library/windows/apps/dn636929) property, which exposes a [**MapAddress**](https://msdn.microsoft.com/library/windows/apps/dn627533) object representing a street address, and a [**Point**](https://docs.microsoft.com/uwp/api/windows.services.maps.maplocation.point) property, which exposes a [**Geopoint**](https://docs.microsoft.com/uwp/api/windows.devices.geolocation.geopoint) object representing a geographic location.

> [!IMPORTANT]
> You must specify a maps authentication key before you can use map services. For more info, see [Request a maps authentication key](authentication-key.md).

## Get a location (Geocode)

This section shows how to convert a street address or a place name to a geographic location (geocoding).

1.  Call one of the overloads of the [**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925) method of the [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class with a place name or street address.
2.  The [**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925) method returns a [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) object.
3.  Use the [**Locations**](https://msdn.microsoft.com/library/windows/apps/dn627552) property of the [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) to expose a collection [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects. There may be multiple [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects because the system may find multiple locations that correspond to the given input.

```csharp
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
...
private async void geocodeButton_Click(object sender, RoutedEventArgs e)
{
   // The address or business to geocode.
   string addressToGeocode = "Microsoft";

   // The nearby location to use as a query hint.
   BasicGeoposition queryHint = new BasicGeoposition();
   queryHint.Latitude = 47.643;
   queryHint.Longitude = -122.131;
   Geopoint hintPoint = new Geopoint(queryHint);

   // Geocode the specified address, using the specified reference point
   // as a query hint. Return no more than 3 results.
   MapLocationFinderResult result =
         await MapLocationFinder.FindLocationsAsync(
                           addressToGeocode,
                           hintPoint,
                           3);

   // If the query returns results, display the coordinates
   // of the first result.
   if (result.Status == MapLocationFinderStatus.Success)
   {
      tbOutputText.Text = "result = (" +
            result.Locations[0].Point.Position.Latitude.ToString() + "," +
            result.Locations[0].Point.Position.Longitude.ToString() + ")";
   }
}
```

This code displays the following results to the `tbOutputText` textbox.

``` syntax
result = (47.6406099647284,-122.129339994863)
```

## Get an address (reverse geocode)

This section shows how to convert a geographic location to an address (reverse geocoding).

1.  Call the [**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928) method of the [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class.
2.  The [**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928) method returns a [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) object that contains a collection of matching [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects.
3.  Use the [**Locations**](https://msdn.microsoft.com/library/windows/apps/dn627552) property of the [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) to expose a collection [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects. There may be multiple [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects because the system may find multiple locations that correspond to the given input.
4.  Access [**MapAddress**](https://msdn.microsoft.com/library/windows/apps/dn627533) objects through the [**Address**](https://msdn.microsoft.com/library/windows/apps/dn636929) property of each [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549).

```csharp
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
...
private async void reverseGeocodeButton_Click(object sender, RoutedEventArgs e)
{
   // The location to reverse geocode.
   BasicGeoposition location = new BasicGeoposition();
   location.Latitude = 47.643;
   location.Longitude = -122.131;
   Geopoint pointToReverseGeocode = new Geopoint(location);

   // Reverse geocode the specified geographic location.
   MapLocationFinderResult result =
         await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

   // If the query returns results, display the name of the town
   // contained in the address of the first result.
   if (result.Status == MapLocationFinderStatus.Success)
   {
      tbOutputText.Text = "town = " +
            result.Locations[0].Address.Town;
   }
}
```

This code displays the following results to the `tbOutputText` textbox.

``` syntax
town = Redmond
```

## Related topics

* [UWP map sample](http://go.microsoft.com/fwlink/p/?LinkId=619977)
* [UWP traffic app sample](http://go.microsoft.com/fwlink/p/?LinkId=619982)
* [Design guidelines for maps](https://msdn.microsoft.com/library/windows/apps/dn596102)
* [Video: Leveraging Maps and Location Across Phone, Tablet, and PC in Your Windows Apps](https://channel9.msdn.com/Events/Build/2015/2-757)
* [Bing Maps Developer Center](https://www.bingmapsportal.com/)
* [**MapLocationFinder** class](https://msdn.microsoft.com/library/windows/apps/dn627550)
* [**FindLocationsAsync** method](https://msdn.microsoft.com/library/windows/apps/dn636925)
* [**FindLocationsAtAsync** method](https://msdn.microsoft.com/library/windows/apps/dn636928)
