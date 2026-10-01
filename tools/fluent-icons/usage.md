---
title: Export Fluent documentation icons
description: Export the bundled, licensed Fluent System Icons to SVG and PNG with consistent color and sizing for Windows documentation pages.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 09/07/2026
---

# Export Fluent documentation icons

Use `tools\export-fluent-icons.ps1` to create matching SVG and PNG assets from the seven bundled **Fluent System Icons**. The originals are pinned to the commit in `manifest.json`, so normal exports require no network access or package installation.

These are Microsoft's MIT-licensed Fluent System Icons, not the **Segoe Fluent Icons** font. The exporter does not read, convert, embed, or redistribute a Windows font. Review the [Microsoft font usage FAQ](https://learn.microsoft.com/typography/fonts/font-faq) before creating a separate font-derived asset pipeline.

## Export the documentation set

From the repository root, run Windows PowerShell in STA mode:

```powershell
powershell.exe -NoProfile -STA -File .\tools\export-fluent-icons.ps1 `
    -OutputDirectory .\hub\apps\images\fluent -Format Both -Force
```

The defaults produce 24-pixel SVGs and transparent 48-by-48 PNGs (2x scale) in graphite `#737373`. Keep the output `LICENSE.txt` with the assets. On Learn, use `class="image is-24x24"` as well as `width="24" height="24"`; HTML size attributes alone did not constrain the earlier preview.

## Export another size or color

```powershell
powershell.exe -NoProfile -STA -File .\tools\export-fluent-icons.ps1 `
    -Icon design -Size 32 -Color '#57506B' -Format Both `
    -OutputDirectory .\icon-exports
```

| Option | Purpose |
|---|---|
| `-Icon` | `all`, `apps`, `design`, `feature`, `package`, `code`, `editor`, or `terminal`. |
| `-Size` | Logical size: 16, 20, 24, 32, 40, 48, or 64 pixels. |
| `-Color` | An explicit six-digit RGB hex color. Check its contrast in the target page's themes. |
| `-Format` | `Svg`, `Png`, or `Both`. |
| `-PngScale` | PNG pixel scale from 1 through 4; default 2. |
| `-Force` | Replace existing outputs. Without it, the command fails before exporting if an output exists. |

SVG output contains paths, not font references. PNG export uses WPF's geometry and bitmap APIs, requiring Windows and an STA thread. Both preserve the source viewBox and fill rules; this is a filled-path icon exporter, not a general SVG renderer.

## Update the source set

Review an upstream icon's license, add the original SVG to `sources`, and record its pinned upstream path and revision in `manifest.json`. The exporter deliberately rejects unsupported elements, transforms, or path attributes instead of producing an incomplete image. Update the script's `-Icon` choices when adding a named preset.

Preserve the full MIT license and source attribution when redistributing generated assets. Do not substitute a locally installed font or a different icon family without changing the documented source and permissions.
