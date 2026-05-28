---
title: In-app acrylic
description: Learn how to use AcrylicBrush to apply in-app acrylic effects to UI elements in your WinUI app.
ms.topic: how-to
ms.date: 02/27/2026
keywords: windows, windows app development, Windows App SDK, Acrylic, AcrylicBrush, materials
ms.localizationpriority: medium
---

# In-app acrylic

You can apply in-app acrylic to your app's surfaces using a XAML [AcrylicBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.acrylicbrush) or predefined `AcrylicBrush` theme resources.

> [!div class="nextstepaction"]
> [Open the WinUI Gallery app and see Acrylic in action](winui3gallery://item/Acrylic)

WinUI includes a collection of brush theme resources that respect the app's theme and fall back to solid colors as needed. To paint a specific surface, apply one of the theme resources to element backgrounds just as you would apply any other brush resource.

```xaml
<Grid Background="{ThemeResource AcrylicInAppFillColorDefaultBrush}">
```

> [!NOTE]
> You can view these resources in the [AcrylicBrush theme resources file, in the microsoft-ui-xaml GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/blob/6aed8d97fdecfe9b19d70c36bd1dacd9c6add7c1/dev/Materials/Acrylic/AcrylicBrush_19h1_themeresources.xaml#L11).

## Custom acrylic brush

You may choose to add a color tint to your app's acrylic to show branding or provide visual balance with other elements on the page. To show color rather than grayscale, you need to define your own acrylic brushes using the following properties.

- **TintColor**: the color/tint overlay layer.
- **TintOpacity**: the opacity of the tint layer.
- **TintLuminosityOpacity**: controls the amount of saturation that is allowed through the acrylic surface from the background.
- **FallbackColor**: the solid color that replaces acrylic in Battery Saver. For background acrylic, fallback color also replaces acrylic when your app isn't in the active desktop window.

![Light theme acrylic swatches](../../design/style/images/custom-acrylic-swatches-light-theme.png)

![Dark theme acrylic swatches](../../design/style/images/custom-acrylic-swatches-dark-theme.png)

![Luminosity opacity compared to tint opacity](../../design/style/images/luminosity-vs-tint.png)

To add an acrylic brush, define the three resources for dark, light, and high contrast themes. In high contrast, we recommend that you use a [SolidColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.solidcolorbrush) with the same `x:Key` as the dark/light AcrylicBrush.

> [!NOTE]
> If you don't specify a `TintLuminosityOpacity` value, the system will automatically adjust its value based on your TintColor and TintOpacity.

```xaml
<ResourceDictionary.ThemeDictionaries>
    <ResourceDictionary x:Key="Default">
        <AcrylicBrush x:Key="MyAcrylicBrush"
            TintColor="#FFFF0000"
            TintOpacity="0.8"
            TintLuminosityOpacity="0.5"
            FallbackColor="#FF7F0000"/>
    </ResourceDictionary>

    <ResourceDictionary x:Key="HighContrast">
        <SolidColorBrush x:Key="MyAcrylicBrush"
            Color="{ThemeResource SystemColorWindowColor}"/>
    </ResourceDictionary>

    <ResourceDictionary x:Key="Light">
        <AcrylicBrush x:Key="MyAcrylicBrush"
            TintColor="#FFFF0000"
            TintOpacity="0.8"
            TintLuminosityOpacity="0.5"
            FallbackColor="#FFFF7F7F"/>
    </ResourceDictionary>
</ResourceDictionary.ThemeDictionaries>
```

The following sample shows how to declare an AcrylicBrush in code.

```csharp
AcrylicBrush myBrush = new AcrylicBrush();
myBrush.TintColor = Color.FromArgb(255, 202, 24, 37);
myBrush.FallbackColor = Color.FromArgb(255, 202, 24, 37);
myBrush.TintOpacity = 0.6;

grid.Background = myBrush;
```

## Related articles

- [Materials overview](materials.md)
- [System backdrops (Mica/Acrylic)](system-backdrops.md)
- [Acrylic design guidance](../../design/style/acrylic.md)
