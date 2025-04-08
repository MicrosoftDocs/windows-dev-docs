---
title: Overlay tiled images on a map
description: Overlay third-party or custom tiled images on a map by using tile sources. Use tile sources to overlay specialized information such as weather data, population data, or seismic data; or use tile sources to replace the default map entirely.
ms.assetid: 066BD6E2-C22B-4F5B-AA94-5D6C86A09BDF
ms.date: 06/21/2024
ms.topic: article
keywords: windows 10, uwp, map, location, images, overlay
ms.localizationpriority: medium
---
# Overlay tiled images on a map

> [!IMPORTANT]
> **Bing Maps for Enterprise service retirement**
>
> The UWP [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services from the [**Windows.Services.Maps**](/uwp/api/Windows.Services.Maps) namespace rely on Bing Maps. Bing Maps for Enterprise is deprecated and will be retired, at which point the MapControl and services will no longer receive data.
>
> For more information, see the [Bing Maps Developer Center](https://www.bingmapsportal.com/) and [Bing Maps documentation](/bingmaps/getting-started/).

> [!NOTE]
> [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) and map services require a maps authentication key called a [**MapServiceToken**](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol.mapservicetoken). For more info about getting and setting a maps authentication key, see [Request a maps authentication key](authentication-key.md).

Overlay third-party or custom tiled images on a map by using tile sources. Use tile sources to overlay specialized information such as weather data, population data, or seismic data; or use tile sources to replace the default map entirely.

<a id="tileintro"></a>

## Tiled image overview

Map services like Bing Maps cut maps into square tiles for quick retrieval and display. These tiles are 256 pixels by 256 pixels in size, and are pre-rendered at multiple levels of detail. Many third-party services also provide map-based data that's cut into tiles. Use tile sources to retrieve third-party tiles, or to create your own custom tiles, and overlay them on the map displayed in the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl).

When you use tile sources, you don't have to write code to request or to position individual tiles. The [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl) requests tiles as it needs them. Each request specifies the X and Y coordinates and the zoom level for the individual tile. You simply specify the format of the Uri or filename to use to retrieve the tiles in the **UriFormatString** property. That is, you insert replaceable parameters in the base Uri or filename to indicate where to pass the X and Y coordinates and the zoom level for each tile.

Here's an example of the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) property for an [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource) that shows the replaceable parameters for the X and Y coordinates and the zoom level.

```syntax
http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}
```

(The X and Y coordinates represent the location of the individual tile within the map of the world at the specified level of detail. The tile numbering system starts from {0, 0} in the upper left corner of the map. For example, the tile at {1, 2} is in the second column of the third row of the grid of tiles.)

For more info about the tile system used by mapping services, see [Bing Maps Tile System](/bingmaps/articles/bing-maps-tile-system).

### Overlay tiles from a tile source

Overlay tiled images from a tile source on a map by using the [**MapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileDataSource).

1.  Instantiate one of the three tile data source classes that inherit from [**MapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileDataSource).

    -   [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource)
    -   [**LocalMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.LocalMapTileDataSource)
    -   [**CustomMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.CustomMapTileDataSource)

    Configure the **UriFormatString** to use to request the tiles by inserting replaceable parameters in the base Uri or filename.

    The following example instantiates an [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource). This example specifies the value of the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) in the constructor of the **HttpMapTileDataSource**.

    ```csharp
        HttpMapTileDataSource dataSource = new HttpMapTileDataSource(
          "http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}");
    ```

2.  Instantiate and configure a [**MapTileSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileSource). Specify the [**MapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileDataSource) that you configured in the previous step as the [**DataSource**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.datasource) of the **MapTileSource**.

    The following example specifies the [**DataSource**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.datasource) in the constructor of the [**MapTileSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileSource).

    ```csharp
        MapTileSource tileSource = new MapTileSource(dataSource);
    ```

    You can restrict the conditions in which the tiles are displayed by using properties of the [**MapTileSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileSource).

    -   Display tiles only within a specific geographic area by providing a value for the [**Bounds**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.bounds) property.
    -   Display tiles only at certain levels of detail by providing a value for the [**ZoomLevelRange**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.zoomlevelrange) property.

    Optionally, configure other properties of the [**MapTileSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileSource) that affect the loading or the display of the tiles, such as [**Layer**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.layer), [**AllowOverstretch**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.allowoverstretch), [**IsRetryEnabled**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.isretryenabled), and [**IsTransparencyEnabled**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.istransparencyenabled).

3.  Add the [**MapTileSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileSource) to the [**TileSources**](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol.tilesources) collection of the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl).

    ```csharp
         MapControl1.TileSources.Add(tileSource);
    ```

## Overlay tiles from a web service

Overlay tiled images retrieved from a web service by using the [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource).

1.  Instantiate an [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource).
2.  Specify the format of the Uri that the web service expects as the value of the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) property. To create this value, insert replaceable parameters in the base Uri. For example, in the following code sample, the value of the **UriFormatString** is:

    ``` syntax
    http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}
    ```

    The web service has to support a Uri that contains the replaceable parameters {x}, {y}, and {zoomlevel}. Most web services (for example, Nokia, Bing, and Google) support Uris in this format. If the web service requires additional arguments that aren't available with the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) property, then you have to create a custom Uri. Create and return a custom Uri by handling the [**UriRequested**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.urirequested) event. For more info, see the [Provide a custom URI](#customuri) section later in this topic.

3.  Then, follow the remaining steps described previously in the [Tiled image overview](#tileintro).

The following example overlays tiles from a fictitious web service on a map of North America. The value of the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) is specified in the constructor of the [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource). In this example, tiles are only displayed within the geographic boundaries specified by the optional [**Bounds**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.bounds) property.

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

```cppwinrt
...
#include <winrt/Windows.Devices.Geolocation.h>
#include <winrt/Windows.UI.Xaml.Controls.Maps.h>
...
void MainPage::AddHttpMapTileSource()
{
    Windows::Devices::Geolocation::BasicGeoposition northWest{ 48.38544, -124.667360 };
    Windows::Devices::Geolocation::BasicGeoposition southEast{ 25.26954, -80.30182 };
    Windows::Devices::Geolocation::GeoboundingBox boundingBox{ northWest, southEast };

    Windows::UI::Xaml::Controls::Maps::HttpMapTileDataSource dataSource{
        L"http://www.<web service name>.com/z={zoomlevel}&x={x}&y={y}" };

    dataSource.AdditionalRequestHeaders().Insert(L"header name", L"header value");

    Windows::UI::Xaml::Controls::Maps::MapTileSource tileSource{ dataSource };
    tileSource.Bounds(boundingBox);

    MapControl1().TileSources().Append(tileSource);
}
...
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

Overlay tiled images stored as files in local storage by using the [**LocalMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.LocalMapTileDataSource). Typically, you package and distribute these files with your app.

1.  Instantiate a [**LocalMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.LocalMapTileDataSource).
2.  Specify the format of the file names as the value of the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.localmaptiledatasource.uriformatstring) property. To create this value, insert replaceable parameters in the base filename. For example, in the following code sample, the value of the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) is:

    ``` syntax
        Tile_{zoomlevel}_{x}_{y}.png
    ```

    If the format of the file names requires additional arguments that aren't available with the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.localmaptiledatasource.uriformatstring) property, then you have to create a custom Uri. Create and return a custom Uri by handling the [**UriRequested**](/uwp/api/windows.ui.xaml.controls.maps.localmaptiledatasource.urirequested) event. For more info, see the [Provide a custom URI](#customuri) section later in this topic.

3.  Then, follow the remaining steps described previously in the [Tiled image overview](#tileintro).

You can use the following protocols and locations to load tiles from local storage:

| Uri | More info |
|---------------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| ms-appx:/// | Points to the root of the app's installation folder. |
|  | This is the location referenced by the [Package.InstalledLocation](/uwp/api/windows.applicationmodel.package.installedlocation) property. |
| ms-appdata:///local | Points to the root of the app's local storage. |
|  | This is the location referenced by the [ApplicationData.LocalFolder](/uwp/api/windows.storage.applicationdata.localfolder) property. |
| ms-appdata:///temp | Points to the app's temp folder. |
|  | This is the location referenced by the [ApplicationData.TemporaryFolder](/uwp/api/windows.storage.applicationdata.temporaryfolder) property. |

The following example loads tiles that are stored as files in the app's installation folder by using the `ms-appx:///` protocol. The value for the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.localmaptiledatasource.uriformatstring) is specified in the constructor of the [**LocalMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.LocalMapTileDataSource). In this example, tiles are only displayed when the zoom level of the map is within the range specified by the optional [**ZoomLevelRange**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.zoomlevelrange) property.

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

<a id="customuri" ></a>

## Provide a custom URI

If the replaceable parameters available with the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.httpmaptiledatasource.uriformatstring) property of the [**HttpMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource) or the [**UriFormatString**](/uwp/api/windows.ui.xaml.controls.maps.localmaptiledatasource.uriformatstring) property of the [**LocalMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.LocalMapTileDataSource) aren't sufficient to retrieve your tiles, then you have to create a custom Uri. Create and return a custom Uri by providing a custom handler for the **UriRequested** event. The **UriRequested** event is raised for each individual tile.

1.  In your custom handler for the **UriRequested** event, combine the required custom arguments with the [**X**](/uwp/api/windows.ui.xaml.controls.maps.maptileurirequestedeventargs.x), [**Y**](/uwp/api/windows.ui.xaml.controls.maps.maptileurirequestedeventargs.y), and [**ZoomLevel**](/uwp/api/windows.ui.xaml.controls.maps.maptileurirequestedeventargs.zoomlevel) properties of the [**MapTileUriRequestedEventArgs**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileUriRequestedEventArgs) to create the custom Uri.
2.  Return the custom Uri in the [**Uri**](/uwp/api/windows.ui.xaml.controls.maps.maptileurirequest.uri) property of the [**MapTileUriRequest**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileUriRequest), which is contained in the [**Request**](/uwp/api/windows.ui.xaml.controls.maps.maptileurirequestedeventargs.request) property of the [**MapTileUriRequestedEventArgs**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileUriRequestedEventArgs).

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

Overlay custom tiles by using the [**CustomMapTileDataSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.CustomMapTileDataSource). Create tiles programmatically in memory on the fly, or write your own code to load existing tiles from another source.

To create or load custom tiles, provide a custom handler for the [**BitmapRequested**](/uwp/api/windows.ui.xaml.controls.maps.custommaptiledatasource.bitmaprequested) event. The **BitmapRequested** event is raised for each individual tile.

1.  In your custom handler for the [**BitmapRequested**](/uwp/api/windows.ui.xaml.controls.maps.custommaptiledatasource.bitmaprequested) event, combine the required custom arguments with the [**X**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.x), [**Y**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.y), and [**ZoomLevel**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.zoomlevel) properties of the [**MapTileBitmapRequestedEventArgs**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileBitmapRequestedEventArgs) to create or retrieve a custom tile.
2.  Return the custom tile in the [**PixelData**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequest.pixeldata) property of the [**MapTileBitmapRequest**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileBitmapRequest), which is contained in the [**Request**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.request) property of the [**MapTileBitmapRequestedEventArgs**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileBitmapRequestedEventArgs). The **PixelData** property is of type [**IRandomAccessStreamReference**](/uwp/api/Windows.Storage.Streams.IRandomAccessStreamReference).

The following example shows how to provide custom tiles by creating a custom handler for the **BitmapRequested** event. This example creates identical red tiles that are partially opaque. The example ignores the [**X**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.x), [**Y**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.y), and [**ZoomLevel**](/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs.zoomlevel) properties of the [**MapTileBitmapRequestedEventArgs**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileBitmapRequestedEventArgs). Although this is not a real world example, the example demonstrates how you can create in-memory custom tiles on the fly. The example also shows how to implement the deferral pattern if you have to do something asynchronously to create the custom tiles.

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

```cppwinrt
...
#include <winrt/Windows.Storage.Streams.h>
...
Windows::Foundation::IAsyncOperation<Windows::Storage::Streams::InMemoryRandomAccessStream> MainPage::CustomRandomAccessStream()
{
    constexpr int pixelHeight{ 256 };
    constexpr int pixelWidth{ 256 };
    constexpr int bpp{ 4 };

    std::array<uint8_t, pixelHeight * pixelWidth * bpp> bytes;

    for (int y = 0; y < pixelHeight; y++)
    {
        for (int x = 0; x < pixelWidth; x++)
        {
            int pixelIndex{ y * pixelWidth + x };
            int byteIndex{ pixelIndex * bpp };

            // Set the current pixel bytes.
            bytes[byteIndex] = (byte)(std::rand() % 256);        // Red
            bytes[byteIndex + 1] = (byte)(std::rand() % 256);    // Green
            bytes[byteIndex + 2] = (byte)(std::rand() % 256);    // Blue
            bytes[byteIndex + 3] = (byte)((std::rand() % 56) + 200);    // Alpha (0xff = fully opaque)
        }
    }

    // Create RandomAccessStream from byte array.
    Windows::Storage::Streams::InMemoryRandomAccessStream randomAccessStream;
    Windows::Storage::Streams::IOutputStream outputStream{ randomAccessStream.GetOutputStreamAt(0) };
    Windows::Storage::Streams::DataWriter writer{ outputStream };
    writer.WriteBytes(bytes);

    co_await writer.StoreAsync();
    co_await writer.FlushAsync();

    co_return randomAccessStream;
}
...
```

```cpp
InMemoryRandomAccessStream^ TileSources::CustomRandomAccessStream::get()
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

-   Specify [**MapTileLayer**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileLayer).**BackgroundReplacement** as the value of the [**Layer**](/uwp/api/windows.ui.xaml.controls.maps.maptilesource.layer) property of the [**MapTileSource**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapTileSource).
-   Specify [**MapStyle**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapStyle).**None** as the value of the [**Style**](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol.style) property of the [**MapControl**](/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl).

## Related topics

* [Bing Maps Developer Center](https://www.bingmapsportal.com/)
* [UWP map sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MapControl)
* [Design guidelines for maps](./display-maps.md)
