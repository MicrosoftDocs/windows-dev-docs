---
title: PowerToys Image Resizer utility for Windows 10
description: A Windows shell extension for bulk image-resizing
ms.date: 05/28/2021
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, File Explorer, Image Resizer]
---

# Image Resizer utility

Image Resizer is a Windows shell extension for bulk image-resizing. After installing PowerToys, right-click on one or more selected image files in File Explorer, and then select **Resize pictures** from the menu.

![Image Resizer Demo](../images/powertoys-resize-images.gif)

## Drag and Drop

Image Resizer also allows you to resize images by dragging and dropping your selected files **with the right mouse button**. This allows you to quickly save your resized pictures in another folder.

![Image Resizer Drag And Drop Demo](../images/powertoys-resize-drag-drop.gif)

## Settings

Inside the PowerToys Image Resizer tab, you can configure the following settings.

![PowerToys Image Resize Settings Menu](../images/powertoys-imageresize-settings.png)

### Sizes

Add new preset sizes. Each size can be configured as Fill, Fit or Stretch. The dimension to be used for resizing can also be configured as Centimeters, Inches, Percent and Pixels.

#### Fill vs Fit vs Stretch

- **Fill:** Fills the entire specified size with the image. Scales the image proportionally. Crops the image as needed.
- **Fit:** Fits the entire image into the specified size. Scales the image proportionally. Does not crop the image.
- **Stretch:** Fills the entire specified size with the image. Stretches the image disproportionally as needed. Does not crop the image

The width and height of the specified size may be swapped to match the orientation (portrait/landscape) of the current image. To always use the width and height as specified, un-check: **Ignore the orientation of pictures**.

### Fallback encoding

The fallback encoder is used when the file cannot be saved in it's original format. For example, the Windows Metafile (.wmf) image format has a decoder to read the image, but no encoder to write a new image. In this case, the image cannot be saved in it's original format. Image Resizer enables you to specify what format the fallback encoder will use: PNG, JPEG, TIFF, BMP, GIF, or WMPhoto settings. *This is not a file type conversion tool, but only works as a fallback for unsupported file formats.*

### File

The file name of the resized image can be modified with the following parameters:

- `%1`: Original filename
- `%2`: Size name (as configured in the PowerToys Image Resizer settings)
- `%3`: Selected width
- `%4`: Selected height
- `%5`: Actual height
- `%6`: Actual width

For example, setting the filename format to: `%1 (%2)` on the file `example.png` and selecting the `Small` file size setting, would result in the file name `example (Small).png`.

Setting the format to `%1_%4` on the file `example.jpg` and selecting the size setting `Medium 1366 x 768px` would result in the file name: `example_768.jpg`.

You can also choose to retain the original *last modified* date on the resized image.

### Auto width/height

You can leave the height or width empty. This will honor the specified dimension and "lock" the other dimension to a value proportional to the original image aspect ratio.

### Sub-directories

You can specify a directory in the filename format to group resized images into sub-directories. For example, a value of `%2\%1` would save the resized image to `Small\Sample.jpg`
