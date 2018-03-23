---
author: normesta
title: Overlay tiled images on a map
description: Overlay third-party or custom tiled images on a map by using tile sources. Use tile sources to overlay specialized information such as weather data, population data, or seismic data; or use tile sources to replace the default map entirely.
ms.assetid: 066BD6E2-C22B-4F5B-AA94-5D6C86A09BDF
ms.author: normesta
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, map, location, images, overlay
ms.localizationpriority: medium
---

# Overlay tiled images on a map

Overlay third-party or custom tiled images on a map by using tile sources. Use tile sources to overlay specialized information such as weather data, population data, or seismic data; or use tile sources to replace the default map entirely.

**Tip** To learn more about using maps in your app, download the [Universal Windows Platform (UWP) map sample](http://go.microsoft.com/fwlink/p/?LinkId=619977) on Github.

<a id="tileintro" />

## Tiled image overview

Map services such as Nokia Maps and Bing Maps cut maps into square tiles for quick retrieval and display. These tiles are 256 pixels by 256 pixels in size, and are pre-rendered at multiple levels of detail. Many third-party services also provide map-based data that's cut into tiles. Use tile sources to retrieve third-party tiles, or to create your own custom tiles, and overlay them on the map displayed in the [**MapControl**](https://msdn.microsoft.com/library/windows/apps/dn637004).

**Important**  
When you use tile sources, you don't have to write code to request or to position individual tiles. The [**MapControl**](https://msdn.microsoft.com/library/windows/apps/dn637004) requests tiles as it needs them. Each request specifies the X and Y coordinates and the zoom level for the individual tile. You simply specify the format of the Uri or filename to use to retrieve the tiles in the **UriFormatString** property. That is, you insert replaceable parameters in the base Uri or filename to indicate where to pass the X and Y coordinates and the zoom level for each tile.

Here's an example of the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) property for an [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986) that shows the replaceable parameters for the X and Y coordinates and the zoom level.

``` syntax
    http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}
```

 

(The X and Y coordinates represent the location of the individual tile within the map of the world at the specified level of detail. The tile numbering system starts from {0, 0} in the upper left corner of the map. For example, the tile at {1, 2} is in the second column of the third row of the grid of tiles.)

For more info about the tile system used by mapping services, see [Bing Maps Tile System](http://go.microsoft.com/fwlink/p/?LinkId=626692).

### Overlay tiles from a tile source

Overlay tiled images from a tile source on a map by using the [**MapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn637141).

1.  Instantiate one of the three tile data source classes that inherit from [**MapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn637141).

    -   [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986)
    -   [**LocalMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636994)
    -   [**CustomMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636983)

    Configure the **UriFormatString** to use to request the tiles by inserting replaceable parameters in the base Uri or filename.

    The following example instantiates an [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986). This example specifies the value of the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) in the constructor of the **HttpMapTileDataSource**.

    ```cs
        HttpMapTileDataSource dataSource = new HttpMapTileDataSource(
          "http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}");
    ```

2.  Instantiate and configure a [**MapTileSource**](https://msdn.microsoft.com/library/windows/apps/dn637144). Specify the [**MapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn637141) that you configured in the previous step as the [**DataSource**](https://msdn.microsoft.com/library/windows/apps/dn637149) of the **MapTileSource**.

    The following example specifies the [**DataSource**](https://msdn.microsoft.com/library/windows/apps/dn637149) in the constructor of the [**MapTileSource**](https://msdn.microsoft.com/library/windows/apps/dn637144).

    ```cs
        MapTileSource tileSource = new MapTileSource(dataSource);
    ```

    You can restrict the conditions in which the tiles are displayed by using properties of the [**MapTileSource**](https://msdn.microsoft.com/library/windows/apps/dn637144).

    -   Display tiles only within a specific geographic area by providing a value for the [**Bounds**](https://msdn.microsoft.com/library/windows/apps/dn637147) property.
    -   Display tiles only at certain levels of detail by providing a value for the [**ZoomLevelRange**](https://msdn.microsoft.com/library/windows/apps/dn637171) property.

    Optionally, configure other properties of the [**MapTileSource**](https://msdn.microsoft.com/library/windows/apps/dn637144) that affect the loading or the display of the tiles, such as [**Layer**](https://msdn.microsoft.com/library/windows/apps/dn637157), [**AllowOverstretch**](https://msdn.microsoft.com/library/windows/apps/dn637145), [**IsRetryEnabled**](https://msdn.microsoft.com/library/windows/apps/dn637153), and [**IsTransparencyEnabled**](https://msdn.microsoft.com/library/windows/apps/dn637155).

3.  Add the [**MapTileSource**](https://msdn.microsoft.com/library/windows/apps/dn637144) to the [**TileSources**](https://msdn.microsoft.com/library/windows/apps/dn637053) collection of the [**MapControl**](https://msdn.microsoft.com/library/windows/apps/dn637004).

    ```cs
         MapControl1.TileSources.Add(tileSource);
    ```

## Overlay tiles from a web service


Overlay tiled images retrieved from a web service by using the [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986).

1.  Instantiate an [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986).
2.  Specify the format of the Uri that the web service expects as the value of the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) property. To create this value, insert replaceable parameters in the base Uri. For example, in the following code sample, the value of the **UriFormatString** is:

    ``` syntax
        http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}
    ```

    The web service has to support a Uri that contains the replaceable parameters {x}, {y}, and {zoomlevel}. Most web services (for example, Nokia, Bing, and Google) support Uris in this format. If the web service requires additional arguments that aren't available with the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) property, then you have to create a custom Uri. Create and return a custom Uri by handling the [**UriRequested**](https://msdn.microsoft.com/library/windows/apps/dn636993) event. For more info, see the [Provide a custom URI](#customuri) section later in this topic.

3.  Then, follow the remaining steps described previously in the [Tiled image overview](#tileintro).

The following example overlays tiles from a fictitious web service on a map of North America. The value of the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) is specified in the constructor of the [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986). In this example, tiles are only displayed within the geographic boundaries specified by the optional [**Bounds**](https://msdn.microsoft.com/library/windows/apps/dn637147) property.

```csharp
        private void AddHttpMapTileSource()
        {
            // Create the bounding box in which the tiles are displayed.
            // This example represents North America.
            BasicGeoposition northWestCorner =
                new BasicGeoposition() { Latitude = 48.38544, Longitude = -124.667360 };
            BasicGeoposition southEastCorner =
                new BasicGeoposition() { Latitude = 25.26954, Longitude = -80.30182 };
            GeoboundingBox boundingBox = new GeoboundingBox(northWestCorner, southEastCorner);

            // Create an HTTP data source.
            // This example retrieves tiles from a fictitious web service.
            HttpMapTileDataSource dataSource = new HttpMapTileDataSource(
                "http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}");

            // Optionally, add custom HTTP headers if the web service requires them.
            dataSource.AdditionalRequestHeaders.Add("header name", "header value");

            // Create a tile source and add it to the Map control.
            MapTileSource tileSource = new MapTileSource(dataSource);
            tileSource.Bounds = boundingBox;
            MapControl1.TileSources.Add(tileSource);
        }
```

```cpp
void MainPage::AddHttpMapTileSource()
{
       BasicGeoposition northWest = { 48.38544, -124.667360 };
       BasicGeoposition southEast = { 25.26954, -80.30182 };
       GeoboundingBox^ boundingBox = ref new GeoboundingBox(northWest, southEast);

       auto dataSource = ref new Windows::UI::Xaml::Controls::Maps::HttpMapTileDataSource(
             "http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}");

       dataSource->AdditionalRequestHeaders->Insert("header name", "header value");

       auto tileSource = ref new Windows::UI::Xaml::Controls::Maps::MapTileSource(dataSource);
       tileSource->Bounds = boundingBox;

       this->MapControl1->TileSources->Append(tileSource);
}
```

## Overlay tiles from local storage


Overlay tiled images stored as files in local storage by using the [**LocalMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636994). Typically, you package and distribute these files with your app.

1.  Instantiate a [**LocalMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636994).
2.  Specify the format of the file names as the value of the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636998) property. To create this value, insert replaceable parameters in the base filename. For example, in the following code sample, the value of the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) is:

    ``` syntax
        Tile_{zoomlevel}_{x}_{y}.png
    ```

    If the format of the file names requires additional arguments that aren't available with the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636998) property, then you have to create a custom Uri. Create and return a custom Uri by handling the [**UriRequested**](https://msdn.microsoft.com/library/windows/apps/dn637001) event. For more info, see the [Provide a custom URI](#customuri) section later in this topic.

3.  Then, follow the remaining steps described previously in the [Tiled image overview](#tileintro).

You can use the following protocols and locations to load tiles from local storage:

| Uri | More info |
|---------------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| ms-appx:/// | Points to the root of the app's installation folder. |
|  | This is the location referenced by the [Package.InstalledLocation](https://msdn.microsoft.com/library/windows/apps/br224681) property. |
| ms-appdata:///local | Points to the root of the app's local storage. |
|  | This is the location referenced by the [ApplicationData.LocalFolder](https://msdn.microsoft.com/library/windows/apps/br241621) property. |
| ms-appdata:///temp | Points to the app's temp folder. |
|  | This is the location referenced by the [ApplicationData.TemporaryFolder](https://msdn.microsoft.com/library/windows/apps/br241629) property. |

 

The following example loads tiles that are stored as files in the app's installation folder by using the `ms-appx:///` protocol. The value for the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636998) is specified in the constructor of the [**LocalMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636994). In this example, tiles are only displayed when the zoom level of the map is within the range specified by the optional [**ZoomLevelRange**](https://msdn.microsoft.com/library/windows/apps/dn637171) property.

```csharp
        void AddLocalMapTileSource()
        {
            // Specify the range of zoom levels
            // at which the overlaid tiles are displayed.
            MapZoomLevelRange range;
            range.Min = 11;
            range.Max = 20;

            // Create a local data source.
            LocalMapTileDataSource dataSource = new LocalMapTileDataSource(
                "ms-appx:///TileSourceAssets/Tile_{zoomlevel}_{x}_{y}.png");

            // Create a tile source and add it to the Map control.
            MapTileSource tileSource = new MapTileSource(dataSource);
            tileSource.ZoomLevelRange = range;
            MapControl1.TileSources.Add(tileSource);
        }
```

<a id="customuri" />

## Provide a custom URI


If the replaceable parameters available with the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636992) property of the [**HttpMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636986) or the [**UriFormatString**](https://msdn.microsoft.com/library/windows/apps/dn636998) property of the [**LocalMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636994) aren't sufficient to retrieve your tiles, then you have to create a custom Uri. Create and return a custom Uri by providing a custom handler for the **UriRequested** event. The **UriRequested** event is raised for each individual tile.

1.  In your custom handler for the **UriRequested** event, combine the required custom arguments with the [**X**](https://msdn.microsoft.com/library/windows/apps/dn610743), [**Y**](https://msdn.microsoft.com/library/windows/apps/dn610744), and [**ZoomLevel**](https://msdn.microsoft.com/library/windows/apps/dn610745) properties of the [**MapTileUriRequestedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn637177) to create the custom Uri.
2.  Return the custom Uri in the [**Uri**](https://msdn.microsoft.com/library/windows/apps/dn610748) property of the [**MapTileUriRequest**](https://msdn.microsoft.com/library/windows/apps/dn637173), which is contained in the [**Request**](https://msdn.microsoft.com/library/windows/apps/dn637179) property of the [**MapTileUriRequestedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn637177).

The following example shows how to provide a custom Uri by creating a custom handler for the **UriRequested** event. It also shows how to implement the deferral pattern if you have to do something asynchronously to create the custom Uri.

```csharp
using Windows.UI.Xaml.Controls.Maps;
using System.Threading.Tasks;
...
            var httpTileDataSource = new HttpMapTileDataSource();
            // Attach a handler for the UriRequested event.
            httpTileDataSource.UriRequested += HandleUriRequestAsync;
            MapTileSource httpTileSource = new MapTileSource(httpTileDataSource);
            MapControl1.TileSources.Add(httpTileSource);
...
        // Handle the UriRequested event.
        private async void HandleUriRequestAsync(HttpMapTileDataSource sender,
            MapTileUriRequestedEventArgs args)
        {
            // Get a deferral to do something asynchronously.
            // Omit this line if you don't have to do something asynchronously.
            var deferral = args.Request.GetDeferral();

            // Get the custom Uri.
            var uri = await GetCustomUriAsync(args.X, args.Y, args.ZoomLevel);

            // Specify the Uri in the Uri property of the MapTileUriRequest.
            args.Request.Uri = uri;

            // Notify the app that the custom Uri is ready.
            // Omit this line also if you don't have to do something asynchronously.
            deferral.Complete();
        }

        // Create the custom Uri.
        private async Task<Uri> GetCustomUriAsync(int x, int y, int zoomLevel)
        {
            // Do something asynchronously to create and return the custom Uri.        }
        }
```

## Overlay tiles from a custom source


Overlay custom tiles by using the [**CustomMapTileDataSource**](https://msdn.microsoft.com/library/windows/apps/dn636983). Create tiles programmatically in memory on the fly, or write your own code to load existing tiles from another source.

To create or load custom tiles, provide a custom handler for the [**BitmapRequested**](https://msdn.microsoft.com/library/windows/apps/dn636984) event. The **BitmapRequested** event is raised for each individual tile.

1.  In your custom handler for the [**BitmapRequested**](https://msdn.microsoft.com/library/windows/apps/dn636984) event, combine the required custom arguments with the [**X**](https://msdn.microsoft.com/library/windows/apps/dn637135), [**Y**](https://msdn.microsoft.com/library/windows/apps/dn637136), and [**ZoomLevel**](https://msdn.microsoft.com/library/windows/apps/dn637137) properties of the [**MapTileBitmapRequestedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn637132) to create or retrieve a custom tile.
2.  Return the custom tile in the [**PixelData**](https://msdn.microsoft.com/library/windows/apps/dn637140) property of the [**MapTileBitmapRequest**](https://msdn.microsoft.com/library/windows/apps/dn637128), which is contained in the [**Request**](https://msdn.microsoft.com/library/windows/apps/dn637134) property of the [**MapTileBitmapRequestedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn637132). The **PixelData** property is of type [**IRandomAccessStreamReference**](https://msdn.microsoft.com/library/windows/apps/hh701664).

The following example shows how to provide custom tiles by creating a custom handler for the **BitmapRequested** event. This example creates identical red tiles that are partially opaque. The example ignores the [**X**](https://msdn.microsoft.com/library/windows/apps/dn637135), [**Y**](https://msdn.microsoft.com/library/windows/apps/dn637136), and [**ZoomLevel**](https://msdn.microsoft.com/library/windows/apps/dn637137) properties of the [**MapTileBitmapRequestedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn637132). Although this is not a real world example, the example demonstrates how you can create in-memory custom tiles on the fly. The example also shows how to implement the deferral pattern if you have to do something asynchronously to create the custom tiles.

```csharp
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;
using System.Threading.Tasks;
...
        CustomMapTileDataSource customDataSource = new CustomMapTileDataSource();
        // Attach a handler for the BitmapRequested event.
        customDataSource.BitmapRequested += customDataSource_BitmapRequestedAsync;
        customTileSource = new MapTileSource(customDataSource);
        MapControl1.TileSources.Add(customTileSource);
...
        // Handle the BitmapRequested event.
        private async void customDataSource_BitmapRequestedAsync(
            CustomMapTileDataSource sender,
            MapTileBitmapRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();
            args.Request.PixelData = await CreateBitmapAsStreamAsync();
            deferral.Complete();
        }

        // Create the custom tiles.
        // This example creates red tiles that are partially opaque.
        private async Task<RandomAccessStreamReference> CreateBitmapAsStreamAsync()
        {
            int pixelHeight = 256;
            int pixelWidth = 256;
            int bpp = 4;

            byte[] bytes = new byte[pixelHeight * pixelWidth * bpp];

            for (int y = 0; y < pixelHeight; y++)
            {
                for (int x = 0; x < pixelWidth; x++)
                {
                    int pixelIndex = y * pixelWidth + x;
                    int byteIndex = pixelIndex * bpp;

                    // Set the current pixel bytes.
                    bytes[byteIndex] = 0xff;        // Red
                    bytes[byteIndex + 1] = 0x00;    // Green
                    bytes[byteIndex + 2] = 0x00;    // Blue
                    bytes[byteIndex + 3] = 0x80;    // Alpha (0xff = fully opaque)
                }
            }

            // Create RandomAccessStream from byte array.
            InMemoryRandomAccessStream randomAccessStream =
                new InMemoryRandomAccessStream();
            IOutputStream outputStream = randomAccessStream.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBytes(bytes);
            await writer.StoreAsync();
            await writer.FlushAsync();
            return RandomAccessStreamReference.CreateFromStream(randomAccessStream);
        }
```

```cpp
InMemoryRandomAccessStream^ TileSources::CustomRandomAccessSteram::get()
{
       int pixelHeight = 256;
       int pixelWidth = 256;
       int bpp = 4;

       Array<byte>^ bytes = ref new Array<byte>(pixelHeight * pixelWidth * bpp);

       for (int y = 0; y < pixelHeight; y++)
       {
              for (int x = 0; x < pixelWidth; x++)
              {
                     int pixelIndex = y * pixelWidth + x;
                     int byteIndex = pixelIndex * bpp;

                     // Set the current pixel bytes.
                     bytes[byteIndex] = (byte)(std::rand() % 256);        // Red
                     bytes[byteIndex + 1] = (byte)(std::rand() % 256);    // Green
                     bytes[byteIndex + 2] = (byte)(std::rand() % 256);    // Blue
                     bytes[byteIndex + 3] = (byte)((std::rand() % 56) + 200);    // Alpha (0xff = fully opaque)
              }
       }

       // Create RandomAccessStream from byte array.
       InMemoryRandomAccessStream^ randomAccessStream = ref new InMemoryRandomAccessStream();
       IOutputStream^ outputStream = randomAccessStream->GetOutputStreamAt(0);
       DataWriter^ writer = ref new DataWriter(outputStream);
       writer->WriteBytes(bytes);

       create_task(writer->StoreAsync()).then([writer](unsigned int)
       {
              create_task(writer->FlushAsync());
       });

       return randomAccessStream;
}
```

## Replace the default map


To replace the default map entirely with third-party or custom tiles:

-   Specify [**MapTileLayer**](https://msdn.microsoft.com/library/windows/apps/dn637143).**BackgroundReplacement** as the value of the [**Layer**](https://msdn.microsoft.com/library/windows/apps/dn637157) property of the [**MapTileSource**](https://msdn.microsoft.com/library/windows/apps/dn637144).
-   Specify [**MapStyle**](https://msdn.microsoft.com/library/windows/apps/dn637127).**None** as the value of the [**Style**](https://msdn.microsoft.com/library/windows/apps/dn637051) property of the [**MapControl**](https://msdn.microsoft.com/library/windows/apps/dn637004).

## Related topics

* [Bing Maps Developer Center](https://www.bingmapsportal.com/)
* [UWP map sample](http://go.microsoft.com/fwlink/p/?LinkId=619977)
* [Design guidelines for maps](https://msdn.microsoft.com/library/windows/apps/dn596102)
* [Build 2015 video: Leveraging Maps and Location Across Phone, Tablet, and PC in Your Windows Apps](https://channel9.msdn.com/Events/Build/2015/2-757)
* [UWP traffic app sample](http://go.microsoft.com/fwlink/p/?LinkId=619982)
