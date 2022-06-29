---
title: PowerToys File Explorer add-ons utility for Windows
description: A File Explorer add-on that enables different preview pane and thumbnail renderers for different file types.
ms.date: 04/27/2022
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, File Explorer, Monaco]
---

# File Explorer add-ons utility

File Explorer add-ons currently include:

- Preview Pane rendering for:
  - SVG icons (.svg)
  - Markdown files (.md)
  - Source code files (.cs, .cpp, .rs, ...)
  - PDF files (.pdf)
  - G-code files (.gcode)
- Thumbnail preview for:
  - SVG icons (.svg)
  - PDF files (.pdf)
  - G-code files (.gcode)
  - STL files (.stl)


## Thumbnail preview

Showing thumbnails is a built-in Windows feature. For thumbnail preview, PowerToys adds multiple extensions: SVG, PDF, G-code and STL.

### Enabling PowerToys Thumbnail previews

To enable, set which you would like enabled to **On**.

## Preview Pane previewers in PowerToys

Preview Pane is an existing feature in the Windows File Explorer which allows you to see a preview of the file's contents in the view's reading pane. PowerToys adds multiple extensions: Markdown, SVG, PDF, and G-code. In addition to those, PowerToys also adds support for source code files (for more than 150 file extensions).

### Enabling PowerToys Preview Pane previewers

To enable, set which you would like enabled to **On**.

![PowerToys Settings Enable File Explorer screenshot.](../images/powertoys-settings-fileexplorer.png)

### Enabling Preview Pane in File Explorer on Windows

> [!NOTE]
> There is an advanced setting in Windows that might be turned off. If preview handlers (including ones like jpeg images) don't seem to work, please verify this setting is turned on. Open Explorer's Folder options, go to tab "View", then under "Advanced settings" check **Show preview handlers in preview pane**.

#### Enabling in Windows 11
Open Windows File Explorer, select the **View** menu in the File Explorer ribbon. Hover over **Show**, and then select **Preview pane**.

![PowerToys Preview Pane demo for Windows 11.](../images/powertoys-fileexplorer-win11.gif)

#### Enabling in Windows 10

Open Windows File Explorer, select the **View** tab in the File Explorer ribbon, and then select **Preview Pane**.

![PowerToys Preview Pane demo for Windows 10.](../images/powertoys-fileexplorer.gif)

