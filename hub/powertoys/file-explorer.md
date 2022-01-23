---
title: PowerToys File Explorer utility for Windows
description: A File Explorer add on that enables Markdown and SVG previews
ms.date: 05/28/2021
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, File Explorer]
---

# File Explorer add-ons utility

File Explorer add-ons currently include:

- Preview Pane rendering of SVG icons (.svg)
- Preview Pane rendering of Markdown files (.md)
- Preview Pane rendering of PDF files (.pdf)
- Icon thumb preview for SVG icons
- Icon thumb preview for PDF files

## Preview Pane

Preview Pane is an existing feature in the Windows File Explorer which shows a lightweight, rich, read-only preview of the file's contents in the view's reading pane. PowerToys adds three extensions: Markdown, SVG and PDF.

## Enabling Preview Pane

To enable, first ensure that in the PowerToys Settings all are set to **On**.

![PowerToys Settings Enable File Explorer screenshot](../images/powertoys-settings-fileexplorer.png)

> [!NOTE]
> Windows Explorer has an additional setting that needs to be checked in order for preview handlers to work. Open Explorer's Folder options, go to tab "View", under "Advanced settings" check **Show preview handlers in preview pane**.

Next, open Windows File Explorer, select the **View** tab in the File Explorer ribbon, then select **Preview Pane**.

![PowerToys Preview Pane Demo](../images/powertoys-fileexplorer.gif)
