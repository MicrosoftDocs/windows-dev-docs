---
title: Image Resizer utility for Windows - PowerToys
description: Resize images in bulk with PowerToys Image Resizer utility. Learn how to quickly resize multiple images using this Windows shell extension with drag-and-drop support.
ms.date: 10/14/2026
ms.topic: concept-article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, File Explorer, Image Resizer]
# Customer intent: As a Windows power user, I want to learn how to use the PowerToys Image Resizer utility to resize images in bulk.
---

# Image Resizer utility

Image Resizer is a Windows shell extension for bulk image-resizing that helps you quickly resize multiple images at once. After installing PowerToys, you can right-click on one or more selected image files in File Explorer and select **Resize with Image Resizer** from the menu to streamline your image processing workflow.

:::image type="content" source="images/image-resizer/resize-images.gif" alt-text="An animated GIF demo of PowerToys Image Resizer utility showing bulk image resizing process in Windows File Explorer.":::

Image Resizer allows you to resize images by dragging and dropping your selected files with the right mouse button. This allows resized pictures to quickly be saved in a folder.

:::image type="content" source="images/image-resizer/resize-drag-drop.gif" alt-text="An animated GIF demo of PowerToys Image Resizer drag and drop functionality for bulk image resizing.":::

> [!NOTE]
> If **Ignore the orientation of pictures** is selected, the width and height of the specified size *may* be swapped to match the orientation (portrait/landscape) of the current image. In other words: If selected, the **smallest** number (in width/height) in the settings will be applied to the **smallest** dimension of the picture. Regardless if this is declared as width or height. The idea is that different photos with different orientations will still be the same size.

## Settings

On the **Image Resizer** page, configure the following settings.

:::image type="content" source="images/image-resizer/imageresize-settings.png" alt-text="A screenshot of PowerToys Image Resizer settings page showing size presets and configuration options.":::

### Sizes

Add new preset sizes. Each size can be configured as Fill, Fit, or Stretch. The dimension to be used for resizing can be centimeters, inches, percent, or pixels.

Select **Add new size** to create a preset, or select an existing preset to edit it. PowerToys opens a dialog for the preset details. Select **Save** when you add a preset, **Update** when you edit one, or **Cancel** to discard your changes.

#### Fill versus Fit versus Stretch

- **Fill**: Fills the entire specified size with the image. Scales the image proportionally. Crops the image as needed.
- **Fit**: Fits the entire image into the specified size. Scales the image proportionally. Doesn't crop the image.
- **Stretch**: Fills the entire specified size with the image. Stretches the image disproportionally as needed. Doesn't crop the image.

> [!TIP]
> You can leave the width or height empty. The dimension will be calculated to a value proportional to the original image aspect ratio.

### Fallback encoding

The fallback encoder is used when the file can't be saved in its original format. For example, the Windows Meta File (.wmf) image format has a decoder to read the image, but no encoder to write a new image. In this case, the image can't be saved in its original format. Specify the format the fallback encoder will use: PNG, JPEG, TIFF, BMP, GIF, or WMPhoto settings. **This isn't a file type conversion tool. It only works as a fallback for unsupported file formats.**

### Encoding options

| Setting | Description |
| :--- | :--- |
| PNG interlacing | Set PNG interlacing for resized images. Options: **Default**, **On**, or **Off**. Interlaced PNGs load progressively, showing a low-resolution version first. |
| TIFF compression | Set the compression algorithm for resized TIFF images. Options: **Default**, **None**, **CCITT3**, **CCITT4**, **LZW**, **RLE**, or **Zip**. |

### File

The file name of the resized image can use the following parameters:

| Parameter | Result |
| :--- | :--- |
| `%1` | Original filename |
| `%2` | Size name (as configured in the PowerToys Image Resizer settings) |
| `%3` | Selected width |
| `%4` | Selected height |
| `%5` | Actual height |
| `%6` | Actual width |

Example: setting the filename format to `%1 (%2)` on the file `example.png` and selecting the `Small` file size setting, would result in the file name `example (Small).png`. Setting the format to `%1_%4` on the file `example.jpg` and selecting the size setting `Medium 1366 × 768px` would result in the file name `example_768.jpg`.

You can specify a directory in the filename format to group resized images into sub-directories. Example: a value of `%2\%1` would save the resized image(s) to `Small\example.jpg`

[Characters that are illegal in file names](/windows/win32/fileio/naming-a-file#file-and-directory-names) will be replaced by an underscore `_`.

You can choose to retain the original *last modified* date on the resized image or reset it at the time of the resizing action.

> [!TIP]
> Image Resizer watches its `settings.json` for external changes and reloads them immediately. If you edit settings with another tool (for example, a [PowerToys DSC configuration](dsc-configure/overview.md)), your new sizes and options take effect on the next resize without restarting PowerToys.

## Command-line reference

The Image Resizer CLI lets you resize one or more images from the command line. PowerToys adds its `bin` folder to your `PATH`, so you can run `PowerToys.ImageResizer.CLI.exe` from any folder in Windows Terminal, PowerShell, or Command Prompt. If a terminal was already open when you installed PowerToys, open a new one so it picks up the updated `PATH`.

| Option | Aliases | Description |
| :--- | :--- | :--- |
| `--help` | `-?`, `/?` | Show help |
| `--show-config` | `--config` | Print current effective configuration |
| `--destination` | `-d`, `/d` | Output directory (optional) |
| `--width` | `-w` | Width |
| `--height` | `-h` | Height |
| `--unit` | `-u` | Unit (Pixel / Percent / Inch / Centimeter) |
| `--fit` | `-f` | Fit mode (Fill / Fit / Stretch) |
| `--size` |  | Use a preset size by its 0-based index in the **Sizes** list |
| `--shrink-only` |  | Only shrink images for non-percentage sizes (do not enlarge) |
| `--replace` | `-r` | Replace original files |
| `--ignore-orientation` |  | Ignore image orientation |
| `--remove-metadata` |  | Strip metadata |
| `--quality` | `-q` | JPEG quality (1–100) |
| `--keep-date-modified` |  | Preserve source last-write time |
| `--filename` | `-n` | Output filename format (for example, `%1` for the original name and `%2` for the size name) |
| `--progress-lines` | `--accessible` | Report progress as separate lines at 0%, 25%, 50%, 75%, and 100% for screen reader accessibility |

If you specify `--width` or `--height`, the CLI resizes to those custom dimensions instead of the preset selected with `--size`. If you use `Percent` with **Fill** or **Fit**, specify a width value. Height-only percentage resizing is only supported with **Stretch**. Also, `--size` must refer to an existing preset index; if the index is out of range, the CLI returns an error and doesn't modify the input files.

**Usage example**

```powershell
# Show help
PowerToys.ImageResizer.CLI.exe --help

# Show current config
PowerToys.ImageResizer.CLI.exe --show-config

# Resize with explicit dimensions
PowerToys.ImageResizer.CLI.exe --width 800 --height 600 .\image.png

# Resize all JPEG files in the current folder to 50 percent
PowerToys.ImageResizer.CLI.exe -w 50 -h 50 -u Percent "*.jpg"

# Use the first preset size and output to a folder
PowerToys.ImageResizer.CLI.exe --size 0 -d "C:\Output" .\photo.png

# Preserve source LastWriteTime
PowerToys.ImageResizer.CLI.exe --width 800 --height 600 --keep-date-modified -d "C:\Output" .\image.png
```

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
