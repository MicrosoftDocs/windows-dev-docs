---
ms.assetid: 98BD79B3-F420-43C5-98D3-52EBDDB479A0
description: This article lists the encoding options that can be used with BitmapEncoder.
title: BitmapEncoder options reference
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# BitmapEncoder options reference


This article lists the encoding options that can be used with [**BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder). An encoding option is defined by its name, which is a string, and a value in a particular data type ([**Windows.Foundation.PropertyType**](/uwp/api/Windows.Foundation.PropertyType)). For information about working with images, see [Create, edit, and save bitmap images](imaging.md).

| Name                    | PropertyType | Usage notes                                                                                        | Valid formats |
|-------------------------|--------------|----------------------------------------------------------------------------------------------------|---------------|
| ImageQuality            | single       | Valid values from 0 to 1.0. Higher values indicate higher quality                                 | JPEG, JPEG-XR |
| CompressionQuality      | single       | Valid values from 0 to 1.0. Higher values indicate a more efficient and slower compression scheme | TIFF          |
| Lossless                | boolean      | If this is set to true, the ImageQuality option is ignored                                        | JPEG-XR       |
| InterlaceOption         | boolean      | Whether to interlace the image                                                                    | PNG           |
| FilterOption            | uint8        | Use the [**PngFilterMode**](/uwp/api/Windows.Graphics.Imaging.PngFilterMode) enumeration                                | PNG           |
| TiffCompressionMethod   | uint8        | Use the [**TiffCompressionMode**](/uwp/api/Windows.Graphics.Imaging.TiffCompressionMode) enumeration                    | TIFF          |
| Luminance               | uint32Array  | An array of 64 elements containing luminance quantization constants                               | JPEG          |
| Chrominance             | uint32Array  | An array of 64 elements containing chrominance quantization constants                             | JPEG          |
| JpegYCrCbSubsampling    | uint8        | Use the [**JpegSubsamplingMode**](/uwp/api/Windows.Graphics.Imaging.JpegSubsamplingMode) enumeration                    | JPEG          |
| SuppressApp0            | boolean      | Whether to suppress the creation of an App0 metadata block                                        | JPEG          |
| EnableV5Header32bppBGRA | boolean      | Whether to encode to a version 5 BMP which supports alpha                                         | BMP           |

 

## Related topics

* [Create, edit, and save bitmap images](imaging.md)
* [Supported codecs](supported-codecs.md)

 