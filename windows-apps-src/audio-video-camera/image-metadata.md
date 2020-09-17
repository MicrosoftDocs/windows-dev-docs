---
ms.assetid: D5D98044-7221-4C2A-9724-56E59F341AB0
description: This article shows how to read and write image metadata properties and how to geotag files using the GeotagHelper utility class.
title: Image Metadata
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Image Metadata



This article shows how to read and write image metadata properties and how to geotag files using the [**GeotagHelper**](/uwp/api/Windows.Storage.FileProperties.GeotagHelper) utility class.

## Image properties

The [**StorageFile.Properties**](/uwp/api/windows.storage.storagefile.properties) property returns a [**StorageItemContentProperties**](/uwp/api/Windows.Storage.FileProperties.StorageItemContentProperties) object that provides access to content-related information about the file. Get the image-specific properties by calling [**GetImagePropertiesAsync**](/uwp/api/windows.storage.fileproperties.storageitemcontentproperties.getimagepropertiesasync). The returned [**ImageProperties**](/uwp/api/Windows.Storage.FileProperties.ImageProperties) object exposes members that contain basic image metadata fields, like the title of the image and the capture date.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetGetImageProperties":::

To access a larger set of file metadata, use the Windows Property System, a set of file metadata properties that can be retrieved with a unique string identifier. Create a list of strings and add the identifier for each property you want to retrieve. The [**ImageProperties.RetrievePropertiesAsync**](/uwp/api/windows.storage.fileproperties.imageproperties.retrievepropertiesasync) method takes this list of strings and returns a dictionary of key/value pairs where the key is the property identifier and the value is the property value.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetGetWindowsProperties":::

-   For a complete list of Windows Properties, including the identifiers and type for each property, see [Windows Properties](/windows/desktop/properties/props).

-   Some properties are only supported for certain file containers and image codecs. For a listing of the image metadata supported for each image type, see [Photo Metadata Policies](/windows/desktop/wic/photo-metadata-policies).

-   Because properties that are unsupported may return a null value when retrieved, always check for null before using a returned metadata value.

## Geotag helper

GeotagHelper is a utility class that makes it easy to tag images with geographic data using the [**Windows.Devices.Geolocation**](/uwp/api/Windows.Devices.Geolocation) APIs directly, without having to manually parse or construct the metadata format.

If you already have a [**Geopoint**](/uwp/api/Windows.Devices.Geolocation.Geopoint) object representing the location you want to tag in the image, either from a previous use of the geolocation APIs or some other source, you can set the geotag data by calling [**GeotagHelper.SetGeotagAsync**](/uwp/api/windows.storage.fileproperties.geotaghelper.setgeotagasync) and passing in a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) and the **Geopoint**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetSetGeoDataFromPoint":::

To set the geotag data using the device's current location, create a new [**Geolocator**](/uwp/api/Windows.Devices.Geolocation.Geolocator) object and call [**GeotagHelper.SetGeotagFromGeolocatorAsync**](/uwp/api/windows.storage.fileproperties.geotaghelper.setgeotagfromgeolocatorasync) passing in the **Geolocator** and the file to be tagged.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetSetGeoDataFromGeolocator":::

-   You must include the **location** device capability in your app manifest in order to use the [**SetGeotagFromGeolocatorAsync**](/uwp/api/windows.storage.fileproperties.geotaghelper.setgeotagfromgeolocatorasync) API.

-   You must call [**RequestAccessAsync**](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) before calling [**SetGeotagFromGeolocatorAsync**](/uwp/api/windows.storage.fileproperties.geotaghelper.setgeotagfromgeolocatorasync) to ensure the user has granted your app permission to use their location.

-   For more information on the geolocation APIs, see [Maps and location](../maps-and-location/index.md).

To get a GeoPoint representing the geotagged location of an image file, call [**GetGeotagAsync**](/uwp/api/windows.storage.fileproperties.geotaghelper.getgeotagasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetGetGeoData":::

## Decode and encode image metadata

The most advanced way of working with image data is to read and write the properties on the stream level using a [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) or a [BitmapEncoder](bitmapencoder-options-reference.md). For these operations you can use Windows Properties to specify the data you are reading or writing, but you can also use the metadata query language provided by the Windows Imaging Component (WIC) to specify the path to a requested property.

Reading image metadata using this technique requires you to have a [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) that was created with the source image file stream. For information on how to do this, see [Imaging](imaging.md).

Once you have the decoder, create a list of strings and add a new entry for each metadata property you want to retrieve, using either the Windows Property identifier string or a WIC metadata query. Call the [**BitmapPropertiesView.GetPropertiesAsync**](/uwp/api/windows.graphics.imaging.bitmappropertiesview.getpropertiesasync) method on the decoder's [**BitmapProperties**](/uwp/api/Windows.Graphics.Imaging.BitmapProperties) member to request the specified properties. The properties are returned in a dictionary of key/value pairs containing the property name or path and the property value.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetReadImageMetadata":::

-   For information on the WIC metadata query language and the properties supported, see [WIC image format native metadata queries](/windows/desktop/wic/-wic-native-image-format-metadata-queries).

-   Many metadata properties are only supported by a subset of image types. [**GetPropertiesAsync**](/uwp/api/windows.graphics.imaging.bitmappropertiesview.getpropertiesasync) will fail with the error code 0x88982F41 if one of the requested properties is not supported by the image associated with the decoder and 0x88982F81 if the image does not support metadata at all. The constants associated with these error codes are WINCODEC\_ERR\_PROPERTYNOTSUPPORTED and WINCODEC\_ERR\_UNSUPPORTEDOPERATION and are defined in the winerror.h header file.
-   Because an image may or may not contain a value for a particular property, use the **IDictionary.ContainsKey** to verify that a property is present in the results before attempting to access it.

Writing image metadata to the stream requires a **BitmapEncoder** associated with the image output file.

Create a [**BitmapPropertySet**](/uwp/api/Windows.Graphics.Imaging.BitmapPropertySet) object to contain the property values you want set. Create a [**BitmapTypedValue**](/uwp/api/Windows.Graphics.Imaging.BitmapTypedValue) object to represent the property value. This object uses an **object** as the value and member of the [**PropertyType**](/uwp/api/Windows.Foundation.PropertyType) enumeration that defines the type of the value. Add the **BitmapTypedValue** to the **BitmapPropertySet** and then call [**BitmapProperties.SetPropertiesAsync**](/uwp/api/windows.graphics.imaging.bitmapproperties.setpropertiesasync) to cause the encoder to write the properties to the stream.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ImagingWin10/cs/MainPage.xaml.cs" id="SnippetWriteImageMetadata":::

-   For details on which properties are supported for which image file types, see [Windows Properties](/windows/desktop/properties/props), [Photo Metadata Policies](/windows/desktop/wic/photo-metadata-policies), and [WIC image format native metadata queries](/windows/desktop/wic/-wic-native-image-format-metadata-queries).

-   [**SetPropertiesAsync**](/uwp/api/windows.graphics.imaging.bitmapproperties.setpropertiesasync) will fail with the error code 0x88982F41 if one of the requested properties is not supported by the image associated with the encoder.

## Related topics

* [Imaging](imaging.md)
 

 
