---
title: Compact sizing for controls
description: Use WinUI 3 compact sizing to reduce control height and padding, making your app more information-dense for desktop and productivity scenarios.
ms.topic: how-to
ms.date: 07/06/2026
template: detail.hbs
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Compact sizing for controls

WinUI 3 controls are designed with spacing optimized for touch interaction. For desktop applications where users primarily use a mouse or keyboard, you can apply a compact density style to reduce the height and padding of controls. This makes more content visible on screen without requiring the user to scroll.

Compact sizing applies to common interactive controls such as `Button`, `CheckBox`, `ComboBox`, `TextBox`, `Slider`, and others.

## Apply compact sizing to a page

Add the compact sizing resource dictionary to your page or panel's resources:

```xaml
<Page.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="ms-appx:///Microsoft.UI.Xaml/DensityStyles/Compact.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Page.Resources>
```

This override applies to all controls within the page. You can scope it to a specific panel by placing the resource dictionary in the panel's `Resources` instead of the page's `Resources`.

## Apply compact sizing to the entire app

To apply compact sizing to all pages in your application, add the resource dictionary to `App.xaml`:

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
            <ResourceDictionary Source="ms-appx:///Microsoft.UI.Xaml/DensityStyles/Compact.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

> [!IMPORTANT]
> Merge the compact dictionary *after* `XamlControlsResources` so that it correctly overrides the default size values.

## Switch sizing at runtime

You can toggle compact sizing at runtime by adding or removing the resource dictionary from a panel's resources:

```csharp
// Apply compact sizing
var compactDict = new ResourceDictionary
{
    Source = new Uri("ms-appx:///Microsoft.UI.Xaml/DensityStyles/Compact.xaml")
};
MyPanel.Resources.MergedDictionaries.Add(compactDict);

// Remove compact sizing (restore default sizing)
MyPanel.Resources.MergedDictionaries.Remove(compactDict);
```

## Choosing a density

| Scenario | Recommendation |
|---|---|
| Productivity or data-entry apps (tables, forms, settings) | Compact sizing |
| Consumer apps or apps designed for touch input | Default sizing |
| Mixed apps with a settings panel | Apply compact sizing scoped to the settings panel only |

WinUI 3 provides only two density levels: default and compact. There is no API to set an arbitrary density value.

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see compact sizing in action](winui3gallery://item/CompactSizing)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

## Related articles

- [XAML theme resources](../../platform/xaml/xaml-theme-resources.md)
- [Layout panels](../layout-panels.md)
