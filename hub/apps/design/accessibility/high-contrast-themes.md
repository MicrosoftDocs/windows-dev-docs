---
description: Describes the steps needed to ensure your Windows app is usable when a contrast theme is active.
title: Contrast themes
label: Contrast design guidelines
keywords: 
template: detail.hbs
ms.date: 03/17/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Contrast themes

Contrast themes use a constrained color palette (with contrast ratios typically at or above 7:1) to improve legibility, reduce visual fatigue, and support users who rely on high visual separation between foreground and background content.

> [!NOTE]
> Do not confuse contrast themes with light and dark themes. Light and dark themes use broader color palettes and are not specifically optimized for maximum contrast. For more on light and dark themes, see [Color](../style/color.md).

To evaluate your app behavior under contrast themes, enable and customize them in *Settings > Accessibility > Contrast themes*.

> [!Tip]
> You can also press Left Alt + Left Shift + Print Screen (PrtScn on some keyboards) to toggle contrast themes quickly. If no contrast theme has been selected previously, **Aquatic** is applied by default (shown in the following image).
>
> :::image type="content" border="false" source="images/contrast-theme-calculators.png" alt-text="Calculator shown in Light theme and Aquatic contrast theme.":::

## Setting HighContrastAdjustment to None

Windows apps enable [**HighContrastAdjustment**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.highcontrastadjustment) by default. This forces white foreground text with a black highlight background to maintain high contrast in all content. If your app already uses correct system-aware brushes, set this to **None** so your intended contrast-theme styling can flow through.

## Detecting high contrast

You can programmatically detect contrast mode by using [Microsoft.UI.System.ThemeSettings](/windows/windows-app-sdk/api/winrt/microsoft.ui.system.themesettings) and checking its [HighContrast](/windows/windows-app-sdk/api/winrt/microsoft.ui.system.themesettings.highcontrast) property.

## Creating theme dictionaries

Use [**ResourceDictionary.ThemeDictionaries**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary.themedictionaries) to define theme-specific resources. This lets your app provide explicit values for **Default** (Dark), **Light**, and **HighContrast**, while still participating in runtime theme switching.

> [!Tip]
> **Contrast theme** refers to the feature in general, while **HighContrast** refers to the specific dictionary being referenced.

1. In *App.xaml*, create a **ThemeDictionaries** collection that includes at least **Default** and **HighContrast** [**ResourceDictionary**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.ResourceDictionary) entries (a **Light** dictionary is optional for this example).
2. In **Default**, define the [Brush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Brush) resources you need, typically **SolidColorBrush** entries keyed by semantic usage.
3. In **HighContrast**, map each resource to an appropriate dynamic **SystemColor** value. See [Contrast colors](#contrast-colors) for pairing guidance.

    ```xaml
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.ThemeDictionaries>
                <!-- Default is a fallback if a more precise theme isn't called
                out below -->
                <ResourceDictionary x:Key="Default">
                    <SolidColorBrush x:Key="BrandedPageBackgroundBrush" Color="#E6E6E6" />
                </ResourceDictionary>
    
                <!-- Optional, Light is used in light theme.
                If included, Default will be used for Dark theme -->
                <ResourceDictionary x:Key="Light">
                    <SolidColorBrush x:Key="BrandedPageBackgroundBrush" Color="#E6E6E6" />
                </ResourceDictionary>
    
                <!-- HighContrast is used in all high contrast themes -->
                <ResourceDictionary x:Key="HighContrast">
                    <SolidColorBrush x:Key="BrandedPageBackgroundBrush" Color="{ThemeResource SystemColorWindowColor}" />
                </ResourceDictionary>
            </ResourceDictionary.ThemeDictionaries>
        </ResourceDictionary>
    </Application.Resources>
    ```

## Contrast colors

On *Settings > Accessibility > Contrast themes* (shown below), users can choose one of four built-in themes: **Aquatic**, **Desert**, **Dusk**, and **Night sky**.

:::image type="content" border="false" source="images/contrast-theme-settings.png" alt-text="Contrast theme settings.":::

After selecting a theme, users can apply it directly or edit individual colors. The following image shows the edit dialog for **Aquatic**.

:::image type="content" border="false" source="images/contrast-theme-resources.png" alt-text="Settings - Edit theme dialog for the **Aquatic** contrast theme.":::

The following table lists the contrast-theme **SystemColor** resources and recommended foreground/background pairings. Each **SystemColor** value updates automatically when the user switches themes.

| Color swatch | Description |
|---------|---------|
|:::image source="images/sys-color/aquatic-color-window.png" alt-text="Color swatch of SystemColorWindowColor used for background of pages, panes, popups, and windows.":::|  **SystemColorWindowColor**</br>Background of pages, panes, popups, and windows.</br></br>Pair with **SystemColorWindowTextColor**       |
|:::image source="images/sys-color/aquatic-color-windowtext.png" alt-text="Color swatch of SystemColorWindowTextColor used for headings, body copy, lists, placeholder text, app and window borders, any UI that can't be interacted with."::: | **SystemColorWindowTextColor**</br>Headings, body copy, lists, placeholder text, app and window borders, any UI that can't be interacted with.</br></br>Pair with **SystemColorWindowColor**        |
|:::image source="images/sys-color/aquatic-color-hotlight.png" alt-text="Color swatch of SystemColorHotlightColor used for hyperlinks.":::| **SystemColorHotlightColor**</br>Hyperlinks.</br></br>Pair with **SystemColorWindowColor**        |
|:::image source="images/sys-color/aquatic-color-graytext.png" alt-text="Color swatch of SystemColorGrayTextColor used for inactive or disabled UI.":::|  **SystemColorGrayTextColor**</br>Inactive or disabled UI.</br></br>Pair with **SystemColorWindowColor**       |
|:::image source="images/sys-color/aquatic-color-highlighttext.png" alt-text="Color swatch of SystemColorHighlightTextColor used for foreground color of text or UI that is selected, interacted with (hover, pressed), or in progress.":::| **SystemColorHighlightTextColor**</br>Foreground color of text or UI that is selected, interacted with (hover, pressed), or in progress.</br></br>Pair with **SystemColorHighlightColor**        |
|:::image source="images/sys-color/aquatic-color-highlight.png" alt-text="Color swatch of SystemColorHighlightColor used for background or accent color of UI that is selected, interacted with (hover, pressed), or in progress.":::| **SystemColorHighlightColor**</br>Background or accent color of UI that is selected, interacted with (hover, pressed), or in progress.</br></br>Pair with **SystemColorHighlightTextColor**        |
|:::image source="images/sys-color/aquatic-color-btntext.png" alt-text="Color swatch of SystemColorButtonTextColor used for foreground color of buttons and any UI that can be interacted with.":::| **SystemColorButtonTextColor**</br>Foreground color of buttons and any UI that can be interacted with.</br></br>Pair with **SystemColorButtonFaceColor**        |
|:::image source="images/sys-color/aquatic-color-3dface.png" alt-text="Color swatch of SystemColorButtonFaceColor used for background color of buttons and any UI that can be interacted with.":::| **SystemColorButtonFaceColor**</br>Background color of buttons and any UI that can be interacted with.</br></br>Pair with **SystemColorButtonTextColor**        |

The next table shows practical examples using **SystemColorWindowColor** as the background baseline.

| Example | Values |
|---------|---------|
|:::image type="content" source="images/sys-color/aquatic-example-windowtext.png" alt-text="A window with text using the window text color." border="false"::: | **SystemColorWindowTextColor** |
|:::image type="content" source="images/sys-color/aquatic-example-hotlight.png" alt-text="A window with hyperlink text using the hot light color." border="false":::| **SystemColorHotlightColor** |
|:::image type="content" source="images/sys-color/aquatic-example-graytext.png" alt-text="A window with inactive text using the gray text color." border="false":::| **SystemColorGrayTextColor** |
|:::image type="content" source="images/sys-color/aquatic-example-highlighttext+highlight.png" alt-text="A window with text using the highlight text color on the highlight color." border="false":::|**SystemColorHighlightTextColor** + **SystemColorHighlightColor** |
|:::image type="content" source="images/sys-color/aquatic-example-btntext+3dface.png" alt-text="A window with a button using the 3d face color and button text using the button text color." border="false":::| **SystemColorButtonTextColor** + **SystemColorButtonFaceColor**

The following code snippet shows how to select a value for **BrandedPageBackgroundBrush**. In this case, **SystemColorWindowColor** is appropriate because the resource is used as a page-level background.

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.ThemeDictionaries>
            <!-- Default is a fallback if a more precise theme isn't called
            out below -->
            <ResourceDictionary x:Key="Default">
                <SolidColorBrush x:Key="BrandedPageBackgroundBrush" Color="#E6E6E6" />
            </ResourceDictionary>

            <!-- Optional, Light is used in light theme.
            If included, Default will be used for Dark theme -->
            <ResourceDictionary x:Key="Light">
                <SolidColorBrush x:Key="BrandedPageBackgroundBrush" Color="#E6E6E6" />
            </ResourceDictionary>

            <!-- HighContrast is used in all high contrast themes -->
            <ResourceDictionary x:Key="HighContrast">
                <SolidColorBrush x:Key="BrandedPageBackgroundBrush" Color="{ThemeResource SystemColorWindowColor}" />
            </ResourceDictionary>
        </ResourceDictionary.ThemeDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

Then apply the resource to the target element background.

```xaml
<Grid Background="{ThemeResource BrandedPageBackgroundBrush}">
```

The preceding example uses `{ThemeResource}` twice: once to resolve **SystemColorWindowColor** and again to resolve **BrandedPageBackgroundBrush**. This two-stage lookup is important for runtime theme updates. With this setup, the **Grid** background updates automatically when users switch into or between contrast themes.

## Best practices

Use the following recommendations when customizing contrast-theme colors in your app.

- Test your app in all four built-in contrast themes while it is running.
- Be consistent.
- Set **HighContrastAdjustment** to `None` when your theme resources are correctly defined. See [Setting HighContrastAdjustment to None](#setting-highcontrastadjustment-to-none).
- **Do not** hard-code colors in **HighContrast**. Use **SystemColor** `Color` and `ColorBrush` resources. See [Hard-coded colors](#hard-coded-colors).
- **Do not** mix incompatible foreground/background pairs.
- **Do not** choose resources for aesthetics alone; users can, and do, modify theme colors.
- **Do not** use `SystemColorGrayTextColor` for secondary body text or hint text. Reserve it for disabled content.
- **Do not** use `SystemColorHotlightColor` for anything other than hyperlinks.


> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see high-contrast colors in action](winui3gallery://item/Color)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

### Hard-coded colors

Platform controls provide built-in contrast-theme behavior, but customization can easily break it. Two common failure modes are hard-coded colors and incorrect **SystemColor** pairings.

In the following snippet, a Grid background is hard-coded to `#E6E6E6` (light gray). This forces the same background across all themes. In a contrast theme such as **Aquatic**, foreground text can switch to white while the background remains light, resulting in poor or unusable contrast.

```xaml
<Grid Background="#E6E6E6">
```

Instead, use [**{ThemeResource} markup extension**](/windows/apps/develop/platform/xaml/themeresource-markup-extension) to resolve values from [**ThemeDictionaries**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary.themedictionaries) in a [**ResourceDictionary**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.ResourceDictionary). This enables automatic runtime substitution based on the active user theme.

```xaml
<Grid Background="{ThemeResource BrandedPageBackgroundBrush}">
```

### Borders

Pages, panes, popups, and bars should generally use **SystemColorWindowColor** as the background baseline. Add contrast-theme-only borders only where you need to preserve important visual boundaries.

> [!Tip]
> We recommend using 2px borders for transitory surfaces such as flyouts and dialogs.

For example, when a navigation pane and content page share the same background color, a contrast-theme-only border is often necessary to maintain separation.

:::image type="content" border="false" source="images/contrast-theme-border.png" alt-text="A navigation pane separated from the rest of the page.":::

### List items with colored text

In contrast themes, [ListView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) items use **SystemColorHighlightColor** during hover, pressed, and selected states. A common failure in complex item templates is that inner content does not invert correctly, which can make selected rows unreadable.

Be careful when setting `TextBlock.Foreground` directly in a [**DataTemplate**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemtemplate) for **ListView**. **ListViewItem** sets **Foreground** for state transitions, and nested **TextBlock** elements should inherit that value. Explicit `Foreground` settings break this inheritance chain.

:::image type="content" border="false" source="images/high-contrast-list1.png" alt-text="Complex list in Light theme and Aquatic theme (note how the text color is not inverted in HighContrast).":::

You can resolve this by defining conditional **Style** resources in [**ThemeDictionaries**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary.themedictionaries). In **HighContrast**, omit the explicit **Foreground** setter so inheritance restores correct inversion behavior.

:::image type="content" border="false" source="images/high-contrast-list2.png" alt-text="Complex list in Light theme and Aquatic theme (note how the text color is inverted in HighContrast).":::

The following code snippet (from an App.xaml file) shows an example [**ThemeDictionaries**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary.themedictionaries) collection in a **ListView** data template.

```xaml
<ResourceDictionary.ThemeDictionaries>
    <ResourceDictionary x:Key="Default">
        <Style
            x:Key="SecondaryBodyTextBlockStyle"
            TargetType="TextBlock"
            BasedOn="{StaticResource BodyTextBlockStyle}">
            <Setter Property="Foreground" 
                Value="{StaticResource SystemControlForegroundBaseMediumBrush}" />
        </Style>
    </ResourceDictionary>

    <ResourceDictionary x:Key="Light">
        <Style
            x:Key="SecondaryBodyTextBlockStyle"
            TargetType="TextBlock"
            BasedOn="{StaticResource BodyTextBlockStyle}">
            <Setter Property="Foreground" 
                Value="{StaticResource SystemControlForegroundBaseMediumBrush}" />
        </Style>
    </ResourceDictionary>

    <ResourceDictionary x:Key="HighContrast">
        <!-- The Foreground Setter is omitted in HighContrast -->
        <Style
            x:Key="SecondaryBodyTextBlockStyle"
            TargetType="TextBlock"
            BasedOn="{StaticResource BodyTextBlockStyle}" />
    </ResourceDictionary>
</ResourceDictionary.ThemeDictionaries>

<!-- Usage in your DataTemplate... -->
<DataTemplate>
    <StackPanel>
        <TextBlock Style="{StaticResource BodyTextBlockStyle}" Text="Double line list item" />

        <!-- Note how ThemeResource is used to reference the Style -->
        <TextBlock Style="{ThemeResource SecondaryBodyTextBlockStyle}" Text="Second line of text" />
    </StackPanel>
</DataTemplate>
```

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see color contrast accessibility support in action](winui3gallery://item/AccessibilityColorContrast)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## Related topics

- [Accessibility overview](accessibility-overview.md)
- [UI contrast and settings sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/XAML%20high%20contrast%20style%20sample%20(Windows%208)) (archived legacy sample)
- [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample) (archived legacy sample)
- [XAML high contrast sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/XAML%20high%20contrast%20style%20sample%20(Windows%208)) (archived legacy sample)
- [**ThemeSettings.HighContrast**](/windows/windows-app-sdk/api/winrt/microsoft.ui.system.themesettings.highcontrast)
