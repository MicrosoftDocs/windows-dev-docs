---
title: Display routes and directions on a map
description: Learn how to retrieve routes and directions using the MapRouteFinder class and display them on a MapControl in a Universal Windows Platform (UWP) app.
ms.assetid: BBB4C23A-8F10-41D1-81EA-271BE01AED81
ms.date: 06/21/2024
ms.topic: article
keywords: windows 10, uwp, route, map, location, directions
ms.localizationpriority: medium
---
# Display routes and directions on a map

> [!IMPORTANT]
> **Bing Maps for Enterprise service retirement**
>
> The UWP [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services from the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace rely on Bing Maps. Bing Maps for Enterprise is deprecated and will be retired, at which point the MapControl and services will no longer receive data.
>
> For more information, see the [Bing Maps Developer Center](https://www.bingmapsportal.com/) and [Bing Maps documentation](/bingmaps/getting-started/).

> [!NOTE]
> [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services require a maps authentication key called a [**MapServiceToken**](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol.mapservicetoken). For more info about getting and setting a maps authentication key, see [Request a maps authentication key](authentication-key.md).

Request routes and directions, and display them in your app.

> [!NOTE]
> If mapping isn't a core feature of your app, consider launching the Windows Maps app instead. You can use the `bingmaps:`, `ms-drive-to:`, and `ms-walk-to:` URI schemes to launch the Windows Maps app to specific maps and turn-by-turn directions. For more info, see [Launch the Windows Maps app](../launch-resume/launch-maps-app.md).

## An intro to MapRouteFinder results

Here's how the classes for routes and directions are related:

* The [**MapRouteFinder**](/uwp/api/Windows.Services.Maps.MapRouteFinder) class has methods that get routes and directions. These methods return a [**MapRouteFinderResult**](/uwp/api/Windows.Services.Maps.MapRouteFinderResult).

* The [**MapRouteFinderResult**](/uwp/api/Windows.Services.Maps.MapRouteFinderResult) contains a [**MapRoute**](/uwp/api/Windows.Services.Maps.MapRoute) object. Access this object through the [**Route**](/uwp/api/windows.services.maps.maproutefinderresult.route) property of the **MapRouteFinderResult**.

* The [**MapRoute**](/uwp/api/Windows.Services.Maps.MapRoute) contains a collection of [**MapRouteLeg**](/uwp/api/Windows.Services.Maps.MapRouteLeg) objects. Access this collection through the [**Legs**](/uwp/api/windows.services.maps.maproute.legs) property of the **MapRoute**.

* Each [**MapRouteLeg**](/uwp/api/Windows.Services.Maps.MapRouteLeg) contains a collection of [**MapRouteManeuver**](/uwp/api/Windows.Services.Maps.MapRouteManeuver) objects. Access this collection through the [**Maneuvers**](/uwp/api/windows.services.maps.maprouteleg.maneuvers) property of the **MapRouteLeg**.

Get a driving or walking route and directions by calling the methods of the [**MapRouteFinder**](/uwp/api/Windows.Services.Maps.MapRouteFinder) class. For example, [**GetDrivingRouteAsync**](/uwp/api/windows.services.maps.maproutefinder.getdrivingrouteasync) or [**GetWalkingRouteAsync**](/uwp/api/windows.services.maps.maproutefinder.getwalkingrouteasync).

When you request a route, you can specify the following things:

* You can provide a start point and end point only, or you can provide a series of waypoints to compute the route.

    *Stop* waypoints adds additional route legs, each with their own Itinerary. To specify *stop* waypoints, use any of the [**GetDrivingRouteFromWaypointsAsync**](/uwp/api/windows.services.maps.maproutefinder.getwalkingroutefromwaypointsasync) overloads.

    *Via* waypoint defines intermediate locations between *stop* waypoints. They do not add route legs.  They are merely waypoints that a route must pass through. To specify *via* waypoints, use any of the [**GetDrivingRouteFromEnhancedWaypointsAsync**](/uwp/api/windows.services.maps.maproutefinder.getdrivingroutefromenhancedwaypointsasync) overloads.

* You can specify optimizations (For example: minimize the distance).

* You can specify restrictions (For example: avoid highways).

## Display directions

The [**MapRouteFinderResult**](/uwp/api/Windows.Services.Maps.MapRouteFinderResult) object contains a [**MapRoute**](/uwp/api/Windows.Services.Maps.MapRoute) object that you can access through its [**Route**](/uwp/api/windows.services.maps.maproutefinderresult.route) property.

The computed [**MapRoute**](/uwp/api/Windows.Services.Maps.MapRoute) has properties that provide the time to traverse the route, the length of the route, and the collection of [**MapRouteLeg**](/uwp/api/Windows.Services.Maps.MapRouteLeg) objects that contain the legs of the route. Each **MapRouteLeg** object contains a collection of [**MapRouteManeuver**](/uwp/api/Windows.Services.Maps.MapRouteManeuver) objects. The **MapRouteManeuver** object contains directions that you can access through its [**InstructionText**](/uwp/api/windows.services.maps.maproutemaneuver.instructiontext) property.

> [!IMPORTANT]
> You must specify a maps authentication key before you can use map services. For more info, see [Request a maps authentication key](authentication-key.md).

```csharp
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
...
private async void button_Click(object sender, RoutedEventArgs e)
{
   // Start at Microsoft in Redmond, Washington.
   BasicGeoposition startLocation = new BasicGeoposition() {Latitude=47.643,Longitude=-122.131};

   // End at the city of Seattle, Washington.
   BasicGeoposition endLocation = new BasicGeoposition() {Latitude = 47.604,Longitude= -122.329};

   // Get the route between the points.
   MapRouteFinderResult routeResult =
         await MapRouteFinder.GetDrivingRouteAsync(
         new Geopoint(startLocation),
         new Geopoint(endLocation),
         MapRouteOptimization.Time,
         MapRouteRestrictions.None);

   if (routeResult.Status == MapRouteFinderStatus.Success)
   {
      System.Text.StringBuilder routeInfo = new System.Text.StringBuilder();

      // Display summary info about the route.
      routeInfo.Append("Total estimated time (minutes) = ");
      routeInfo.Append(routeResult.Route.EstimatedDuration.TotalMinutes.ToString());
      routeInfo.Append("\nTotal length (kilometers) = ");
      routeInfo.Append((routeResult.Route.LengthInMeters / 1000).ToString());

      // Display the directions.
      routeInfo.Append("\n\nDIRECTIONS\n");

      foreach (MapRouteLeg leg in routeResult.Route.Legs)
      {
         foreach (MapRouteManeuver maneuver in leg.Maneuvers)
         {
            routeInfo.AppendLine(maneuver.InstructionText);
         }
      }

      // Load the text box.
      tbOutputText.Text = routeInfo.ToString();
   }
   else
   {
      tbOutputText.Text =
            "A problem occurred: " + routeResult.Status.ToString();
   }
}
```

This example displays the following results to the `tbOutputText` text box.

```output
Total estimated time (minutes) = 18.4833333333333
Total length (kilometers) = 21.847

DIRECTIONS
Head north on 157th Ave NE.
Turn left onto 159th Ave NE.
Turn left onto NE 40th St.
Turn left onto WA-520 W.
Enter the freeway WA-520 from the right.
Keep left onto I-5 S/Portland.
Keep right and leave the freeway at exit 165A towards James St..
Turn right onto James St.
You have reached your destination.
```

## Display routes

To display a [**MapRoute**](/uwp/api/Windows.Services.Maps.MapRoute) on a [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl), construct a [**MapRouteView**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapRouteView) with the **MapRoute**. Then, add the **MapRouteView** to the [**Routes**](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol.routes) collection of the **MapControl**.

> [!IMPORTANT]
> You must specify a maps authentication key before you can use map services or the map control. For more info, see [Request a maps authentication key](authentication-key.md).

```csharp
using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
...
private async void ShowRouteOnMap()
{
   // Start at Microsoft in Redmond, Washington.
   BasicGeoposition startLocation = new BasicGeoposition() { Latitude = 47.643, Longitude = -122.131 };

   // End at the city of Seattle, Washington.
   BasicGeoposition endLocation = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };


   // Get the route between the points.
   MapRouteFinderResult routeResult =
         await MapRouteFinder.GetDrivingRouteAsync(
         new Geopoint(startLocation),
         new Geopoint(endLocation),
         MapRouteOptimization.Time,
         MapRouteRestrictions.None);

   if (routeResult.Status == MapRouteFinderStatus.Success)
   {
      // Use the route to initialize a MapRouteView.
      MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
      viewOfRoute.RouteColor = Colors.Yellow;
      viewOfRoute.OutlineColor = Colors.Black;

      // Add the new MapRouteView to the Routes collection
      // of the MapControl.
      MapWithRoute.Routes.Add(viewOfRoute);

      // Fit the MapControl to the route.
      await MapWithRoute.TrySetViewBoundsAsync(
            routeResult.Route.BoundingBox,
            null,
            Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
   }
}
```

This example displays the following on a [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) named **MapWithRoute**.

![map control with route displayed.](images/routeonmap.png)

Here's a version of this example that uses a *via* waypoint in between two *stop* waypoints:

```csharp
using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
...
private async void ShowRouteOnMap()
{
  Geolocator locator = new Geolocator();
  locator.DesiredAccuracyInMeters = 1;
  locator.PositionChanged += Locator_PositionChanged;

  BasicGeoposition point1 = new BasicGeoposition() { Latitude = 47.649693, Longitude = -122.144908 };
  BasicGeoposition point2 = new BasicGeoposition() { Latitude = 47.6205, Longitude = -122.3493 };
  BasicGeoposition point3 = new BasicGeoposition() { Latitude = 48.649693, Longitude = -122.144908 };

  // Get Driving Route from point A  to point B thru point C
  var path = new List<EnhancedWaypoint>();

  path.Add(new EnhancedWaypoint(new Geopoint(point1), WaypointKind.Stop));
  path.Add(new EnhancedWaypoint(new Geopoint(point2), WaypointKind.Via));
  path.Add(new EnhancedWaypoint(new Geopoint(point3), WaypointKind.Stop));

  MapRouteFinderResult routeResult =  await MapRouteFinder.GetDrivingRouteFromEnhancedWaypointsAsync(path);

  if (routeResult.Status == MapRouteFinderStatus.Success)
  {
      MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
      viewOfRoute.RouteColor = Colors.Yellow;
      viewOfRoute.OutlineColor = Colors.Black;

      myMap.Routes.Add(viewOfRoute);

      await myMap.TrySetViewBoundsAsync(
            routeResult.Route.BoundingBox,
            null,
            Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
  }
}
```

## Related topics

* [Bing Maps Developer Center](https://www.bingmapsportal.com/)
* [UWP map sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MapControl)
* [Design guidelines for maps](./display-maps.md)
