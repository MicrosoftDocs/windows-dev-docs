---
author: PatrickFarley
title: Perform geocoding and reverse geocoding
description: Convert addresses to geographic locations (geocoding) and convert geographic locations to addresses (reverse geocoding) by calling the methods of the MapLocationFinder class in the Windows.Services.Maps namespace.
ms.assetid: B912BE80-3E1D-43BB-918F-7A43327597D2
ms.author: pafarley
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, geocoding, map, location
ms.localizationpriority: medium
---

# Perform geocoding and reverse geocoding




Convert addresses to geographic locations (geocoding) and convert geographic locations to addresses (reverse geocoding) by calling the methods of the [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class in the [**Windows.Services.Maps**](https://msdn.microsoft.com/library/windows/apps/dn636979) namespace.

**Tip** To learn more about using maps in your app, download the following sample from the [Windows-universal-samples repo](http://go.microsoft.com/fwlink/p/?LinkId=619979) on GitHub.

-   [Universal Windows Platform (UWP) map sample](http://go.microsoft.com/fwlink/p/?LinkId=619977)

Here's how the classes for geocoding and reverse geocoding are related:

-   The [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class has methods that do geocoding ([**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925)) and reverse geocoding ([**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928)).
-   These methods return a [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551).
-   The [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) contains a collection of [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects. Access this collection through the [**Locations**](https://msdn.microsoft.com/library/windows/apps/dn627552) property of the **MapLocationFinderResult**.
-   Each [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) object contains a [**MapAddress**](https://msdn.microsoft.com/library/windows/apps/dn627533) object. Access this object through the [**Address**](https://msdn.microsoft.com/library/windows/apps/dn636929) property of each **MapLocation**.

**Important**  You must specify a maps authentication key before you can use map services. For more info, see [Request a maps authentication key](authentication-key.md).

 

## Get a location (Geocode)


Convert an address or a place name to a geographic location (geocoding) by performing the following steps.

1.  Call one of the overloads of the [**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925) method of the [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class.
2.  The [**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925) method returns a [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) object that contains a collection of matching [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects.
3.  Access this collection through the [**Locations**](https://msdn.microsoft.com/library/windows/apps/dn627552) property of the [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551).

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


Convert a geographic location to an address (reverse geocoding) by performing the following steps.

1.  Call the [**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928) method of the [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550) class.
2.  The [**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928) method returns a [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551) object that contains a collection of matching [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549) objects.
3.  Access this collection through the [**Locations**](https://msdn.microsoft.com/library/windows/apps/dn627552) property of the [**MapLocationFinderResult**](https://msdn.microsoft.com/library/windows/apps/dn627551).
4.  Access the [**MapAddress**](https://msdn.microsoft.com/library/windows/apps/dn627533) object through the [**Address**](https://msdn.microsoft.com/library/windows/apps/dn636929) property of each [**MapLocation**](https://msdn.microsoft.com/library/windows/apps/dn627549).

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

* [Bing Maps Developer Center](https://www.bingmapsportal.com/)
* [UWP map sample](http://go.microsoft.com/fwlink/p/?LinkId=619977)
* [Design guidelines for maps](https://msdn.microsoft.com/library/windows/apps/dn596102)
* [Build 2015 video: Leveraging Maps and Location Across Phone, Tablet, and PC in Your Windows Apps](https://channel9.msdn.com/Events/Build/2015/2-757)
* [UWP traffic app sample](http://go.microsoft.com/fwlink/p/?LinkId=619982)
* [**MapLocationFinder**](https://msdn.microsoft.com/library/windows/apps/dn627550)
* [**FindLocationsAsync**](https://msdn.microsoft.com/library/windows/apps/dn636925)
* [**FindLocationsAtAsync**](https://msdn.microsoft.com/library/windows/apps/dn636928)
