---
description: Displays a symbolic map of the Earth using Azure Maps, with support for pins, layers, and interactive controls.
title: MapControl
template: detail.hbs
ms.date: 02/20/2026
ms.topic: article
---

# MapControl

The [MapControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol) displays a symbolic, interactive map of the Earth powered by [Azure Maps](/azure/azure-maps/about-azure-maps). You can show locations, add pins and custom layers, and let users interact with the map using pan, zoom, rotation, and pitch controls.

The MapControl requires an Azure Maps account. Follow the instructions at [Manage your Azure Maps account](/azure/azure-maps/how-to-manage-account-keys) to create an account and obtain a map service token.

## Is this the right control?

Use a MapControl when you want to display geographic data in your app, such as:

- Showing a location on a map with a pin.
- Displaying a collection of points of interest.
- Providing an interactive map experience with zoom and pan.

## Create a MapControl

> [!div class="checklist"]
>
> - **Important APIs**: [MapControl class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol), [MapIcon class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapicon), [MapElementsLayer class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapelementslayer)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the MapControl in action](winui3gallery://item/MapControl)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

Add a MapControl to your page and set the [MapServiceToken](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.mapservicetoken) to your Azure Maps key.

```xaml
<MapControl x:Name="myMap"
            MapServiceToken="YOUR_AZURE_MAPS_TOKEN"
            Height="400" />
```

## Set the map location

Set the [Center](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.center) and [ZoomLevel](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.zoomlevel) properties to control what the map displays.

```csharp
using Windows.Devices.Geolocation;

var position = new BasicGeoposition { Latitude = 47.6062, Longitude = -122.3321 };
myMap.Center = new Geopoint(position);
myMap.ZoomLevel = 12;
```

```xaml
<!-- Set initial center and zoom in XAML is not supported; set in code-behind -->
<MapControl x:Name="myMap"
            MapServiceToken="YOUR_AZURE_MAPS_TOKEN"
            Height="400" />
```

## Add pins to the map

Use [MapIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapicon) to display pushpins on the map. Add icons to a [MapElementsLayer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapelementslayer), then add the layer to the map's [Layers](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.layers) collection.

```csharp
using Windows.Devices.Geolocation;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

var position = new BasicGeoposition
{
    Latitude = 47.6062,
    Longitude = -122.3321
};

var icon = new MapIcon
{
    Location = new Geopoint(position),
};

var layer = new MapElementsLayer
{
    MapElements = new List<MapElement> { icon }
};

myMap.Layers.Add(layer);
```

## Show or hide interactive controls

The [InteractiveControlsVisible](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.interactivecontrolsvisible) property controls whether the map displays built-in overlay controls for zoom, rotation, pitch, and map style.

```xaml
<MapControl x:Name="myMap"
            MapServiceToken="YOUR_AZURE_MAPS_TOKEN"
            InteractiveControlsVisible="True"
            Height="400" />
```

## Handle map element clicks

Subscribe to the [MapElementClick](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.mapelementclick) event to respond when the user clicks a map element such as a pin.

```csharp
myMap.MapElementClick += (sender, args) =>
{
    foreach (var element in args.MapElements)
    {
        if (element is MapIcon clickedIcon)
        {
            // Handle the clicked icon
        }
    }
};
```

## Handle map service errors

Subscribe to the [MapServiceErrorOccurred](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.mapserviceerroroccurred) event to detect issues communicating with the map service, such as an invalid or missing [MapServiceToken](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol.mapservicetoken).

```csharp
myMap.MapServiceErrorOccurred += (sender, args) =>
{
    // Log or display the error
    System.Diagnostics.Debug.WriteLine("Map service error occurred.");
};
```

## Related articles

- [Maps and location overview](../../maps-and-location/index.md)
- [Get the user's location](../../maps-and-location/get-location.md)
- [Set up a geofence](../../maps-and-location/geofencing.md)
- [MapControl class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol)
- [MapIcon class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapicon)
- [MapElementsLayer class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapelementslayer)
- [Azure Maps documentation](/azure/azure-maps/about-azure-maps)
- [Manage your Azure Maps account keys](/azure/azure-maps/how-to-manage-account-keys)
