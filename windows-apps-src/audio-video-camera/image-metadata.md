---
author: laurenhughes
ms.assetid: D5D98044-7221-4C2A-9724-56E59F341AB0
description: This article shows how to read and write image metadata properties and how to geotag files using the GeotagHelper utility class.
title: Image Metadata
ms.author: lahugh
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Image Metadata



This article shows how to read and write image metadata properties and how to geotag files using the [**GeotagHelper**](https://msdn.microsoft.com/library/windows/apps/dn903683) utility class.

## Image properties

The [**StorageFile.Properties**](https://msdn.microsoft.com/library/windows/apps/br227225) property returns a [**StorageItemContentProperties**](https://msdn.microsoft.com/library/windows/apps/hh770642) object that provides access to content-related information about the file. Get the image-specific properties by calling [**GetImagePropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/hh770646). The returned [**ImageProperties**](https://msdn.microsoft.com/library/windows/apps/br207718) object exposes members that contain basic image metadata fields, like the title of the image and the capture date.

[!code-cs[GetImageProperties](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetGetImageProperties)]

To access a larger set of file metadata, use the Windows Property System, a set of file metadata properties that can be retrieved with a unique string identifier. Create a list of strings and add the identifier for each property you want to retrieve. The [**ImageProperties.RetrievePropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/br207732) method takes this list of strings and returns a dictionary of key/value pairs where the key is the property identifier and the value is the property value.

[!code-cs[GetWindowsProperties](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetGetWindowsProperties)]

-   For a complete list of Windows Properties, including the identifiers and type for each property, see [Windows Properties](https://msdn.microsoft.com/library/windows/desktop/dd561977).

-   Some properties are only supported for certain file containers and image codecs. For a listing of the image metadata supported for each image type, see [Photo Metadata Policies](https://msdn.microsoft.com/library/windows/desktop/ee872003).

-   Because properties that are unsupported may return a null value when retrieved, always check for null before using a returned metadata value.

## Geotag helper

GeotagHelper is a utility class that makes it easy to tag images with geographic data using the [**Windows.Devices.Geolocation**](https://msdn.microsoft.com/library/windows/apps/br225603) APIs directly, without having to manually parse or construct the metadata format.

If you already have a [**Geopoint**](https://msdn.microsoft.com/library/windows/apps/dn263675) object representing the location you want to tag in the image, either from a previous use of the geolocation APIs or some other source, you can set the geotag data by calling [**GeotagHelper.SetGeotagAsync**](https://msdn.microsoft.com/library/windows/apps/dn903685) and passing in a [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) and the **Geopoint**.

[!code-cs[SetGeoDataFromPoint](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetSetGeoDataFromPoint)]

To set the geotag data using the device's current location, create a new [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) object and call [**GeotagHelper.SetGeotagFromGeolocatorAsync**](https://msdn.microsoft.com/library/windows/apps/dn903686) passing in the **Geolocator** and the file to be tagged.

[!code-cs[SetGeoDataFromGeolocator](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetSetGeoDataFromGeolocator)]

-   You must include the **location** device capability in your app manifest in order to use the [**SetGeotagFromGeolocatorAsync**](https://msdn.microsoft.com/library/windows/apps/dn903686) API.

-   You must call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/dn859152) before calling [**SetGeotagFromGeolocatorAsync**](https://msdn.microsoft.com/library/windows/apps/dn903686) to ensure the user has granted your app permission to use their location.

-   For more information on the geolocation APIs, see [Maps and location](https://msdn.microsoft.com/library/windows/apps/mt219699).

To get a GeoPoint representing the geotagged location of an image file, call [**GetGeotagAsync**](https://msdn.microsoft.com/library/windows/apps/dn903684).

[!code-cs[GetGeoData](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetGetGeoData)]

## Decode and encode image metadata

The most advanced way of working with image data is to read and write the properties on the stream level using a [**BitmapDecoder**](https://msdn.microsoft.com/library/windows/apps/br226176) or a [BitmapEncoder](bitmapencoder-options-reference.md). For these operations you can use Windows Properties to specify the data you are reading or writing, but you can also use the metadata query language provided by the Windows Imaging Component (WIC) to specify the path to a requested property.

Reading image metadata using this technique requires you to have a [**BitmapDecoder**](https://msdn.microsoft.com/library/windows/apps/br226176) that was created with the source image file stream. For information on how to do this, see [Imaging](imaging.md).

Once you have the decoder, create a list of strings and add a new entry for each metadata property you want to retrieve, using either the Windows Property identifier string or a WIC metadata query. Call the [**BitmapPropertiesView.GetPropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/br226250) method on the decoder's [**BitmapProperties**](https://msdn.microsoft.com/library/windows/apps/br226248) member to request the specified properties. The properties are returned in a dictionary of key/value pairs containing the property name or path and the property value.

[!code-cs[ReadImageMetadata](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetReadImageMetadata)]

-   For information on the WIC metadata query language and the properties supported, see [WIC image format native metadata queries](https://msdn.microsoft.com/library/windows/desktop/ee719904).

-   Many metadata properties are only supported by a subset of image types. [**GetPropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/br226250) will fail with the error code 0x88982F41 if one of the requested properties is not supported by the image associated with the decoder and 0x88982F81 if the image does not support metadata at all. The constants associated with these error codes are WINCODEC\_ERR\_PROPERTYNOTSUPPORTED and WINCODEC\_ERR\_UNSUPPORTEDOPERATION and are defined in the winerror.h header file.
-   Because an image may or may not contain a value for a particular property, use the **IDictionary.ContainsKey** to verify that a property is present in the results before attempting to access it.

Writing image metadata to the stream requires a **BitmapEncoder** associated with the image output file.

Create a [**BitmapPropertySet**](https://msdn.microsoft.com/library/windows/apps/hh974338) object to contain the property values you want set. Create a [**BitmapTypedValue**](https://msdn.microsoft.com/library/windows/apps/hh700687) object to represent the property value. This object uses an **object** as the value and member of the [**PropertyType**](https://msdn.microsoft.com/library/windows/apps/br225871) enumeration that defines the type of the value. Add the **BitmapTypedValue** to the **BitmapPropertySet** and then call [**BitmapProperties.SetPropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/br226252) to cause the encoder to write the properties to the stream.

[!code-cs[WriteImageMetadata](./code/ImagingWin10/cs/MainPage.xaml.cs#SnippetWriteImageMetadata)]

-   For details on which properties are supported for which image file types, see [Windows Properties](https://msdn.microsoft.com/library/windows/desktop/dd561977), [Photo Metadata Policies](https://msdn.microsoft.com/library/windows/desktop/ee872003), and [WIC image format native metadata queries](https://msdn.microsoft.com/library/windows/desktop/ee719904).

-   [**SetPropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/br226252) will fail with the error code 0x88982F41 if one of the requested properties is not supported by the image associated with the encoder.

## Related topics

* [Imaging](imaging.md)
 

 




