---
description: Describes the steps needed to ensure your Windows app is usable when a contrast theme is active.
title: Contrast themes
label: Contrast design guidelines
keywords: 
template: detail.hbs
ms.date: 02/27/2025
ms.topic: article
ms.localizationpriority: medium
---

# Contrast themes  

Contrast themes use a small palette of colors (with a contrast ratio of at least 7:1) to help make elements in the UI easier to see, reduce eye strain, improve text readability, and accommodate user preferences.

> [!NOTE]
> Don't confuse contrast themes with light and dark themes, which support a much larger color palette and don't necessarily increase contrast or make things easier to see. For more on light and dark themes, see [Color](../style/color.md).

To see how your app behaves with contrast themes, enable and customize them through the *Settings > Accessibility > Contrast themes* page.

> [!Tip]
> You can also press the left-Alt key + Shift key + Print screen (PrtScn on some keyboards) to quickly turn contrast themes on or off. If you have not selected a theme previously, the Aquatic theme is used by default (shown in the following image).
>
> :::image type="content" border="false" source="images/contrast-theme-calculators.png" alt-text="Calculator shown in Light theme and Aquatic contrast theme.":::

## Setting HighContrastAdjustment to None

Windows apps have [**HighContrastAdjustment**](/uwp/api/windows.ui.xaml.uielement.highcontrastadjustment) turned on by default. This sets all text color to white with a solid black highlight behind it, ensuring sufficient contrast against all backgrounds. If you are using brushes correctly, this setting should be turned off.

## Detecting high contrast

You can programmatically check if the current theme is a contrast theme through the [**AccessibilitySettings**](/uwp/api/Windows.UI.ViewManagement.AccessibilitySettings) class (you must call the **AccessibilitySettings** constructor from a scope where the app is initialized and is already displaying content).

## Creating theme dictionaries

A [**ResourceDictionary.ThemeDictionaries**](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) object can indicate theme colors that are different from the system-defined colors by specifying brushes for the **Default** (Dark), **Light**, and **HighContrast** contrast themes.

> [!Tip]
> **Contrast theme** refers to the feature in general, while **HighContrast** refers to the specific dictionary being referenced.

1. In App.xaml, create a **ThemeDictionaries** collection with both a **Default** and a **HighContrast** [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary) (a **Light** [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary) is not necessary for this example).
2. In the **Default** dictionary, create the type of [Brush](/uwp/api/Windows.UI.Xaml.Media.Brush) you need (usually a **SolidColorBrush**). Give it an *x:Key* name corresponding to its intended use (a **StaticResource** referencing an existing system brush would also be appropriate).
3. In the **HighContrast** ResourceDictionary (shown in the following code snippet), specify an appropriate **SystemColor** brush. See [Contrast colors](#contrast-colors) for details on picking one of the dynamic system **HighContrast** colors for the **SystemColor** brush.

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

On the *Settings > Ease of access > Contrast themes* page (shown in the following image), users can select from four default contrast themes: **Aquatic**, **Desert**, **Dusk**, and **Night sky**.

:::image type="content" border="false" source="images/contrast-theme-settings.png" alt-text="Contrast theme settings.":::

After the user selects an option, they can choose to immediately apply it, or they can edit the theme. The following image shows the Edit theme dialog for the **Aquatic** contrast theme.

:::image type="content" border="false" source="images/contrast-theme-resources.png" alt-text="Settings - Edit theme dialog for the **Aquatic** contrast theme.":::

This table shows the contrast theme colors and their recommended pairings. Each **SystemColor** resource is a variable that automatically updates the color when the user switches contrast themes.

| Color swatch | Description |
|---------|---------|
|:::image source="images/sys-color/aquatic-color-window.png" alt-text="Color swatch of SystemColorWindowColor used for background of pages, panes, popups, and windows.":::|  **SystemColorWindowColor**</br>Background of pages, panes, popups, and windows.</br></br>Pair with **SystemColorWindowTextColor**       |
|:::image source="images/sys-color/aquatic-color-windowtext.png" alt-text="Color swatch of SystemColorWindowTextColor used for headings, body copy, lists, placeholder text, app and window borders, any UI that can't be interacted with."::: | **SystemColorWindowTextColor**</br>Headings, body copy, lists, placeholder text, app and window borders, any UI that can't be interacted with.</br></br>Pair with **SystemColorWindowColor**        |
|:::image source="images/sys-color/aquatic-color-hotlight.png" alt-text="Color swatch of SystemColorWindowTextColor used for hyperlinks.":::| **SystemColorHotlightColor**</br>Hyperlinks.</br></br>Pair with **SystemColorWindowColor**        |
|:::image source="images/sys-color/aquatic-color-graytext.png" alt-text="Color swatch of SystemColorWindowTextColor used for inactive or disabled UI.":::|  **SystemColorGrayTextColor**</br>Inactive or disabled UI.</br></br>Pair with **SystemColorWindowColor**       |
|:::image source="images/sys-color/aquatic-color-highlighttext.png" alt-text="Color swatch of SystemColorWindowTextColor used for foreground color of text or UI that is selected, interacted with (hover, pressed), or in progress.":::| **SystemColorHighlightTextColor**</br>Foreground color of text or UI that is selected, interacted with (hover, pressed), or in progress.</br></br>Pair with **SystemColorHighlightColor**        |
|:::image source="images/sys-color/aquatic-color-highlight.png" alt-text="Color swatch of SystemColorWindowTextColor used for background or accent color of UI that is selected, interacted with (hover, pressed), or in progress.":::| **SystemColorHighlightColor**</br>Background or accent color of UI that is selected, interacted with (hover, pressed), or in progress.</br></br>Pair with **SystemColorHighlightTextColor**        |
|:::image source="images/sys-color/aquatic-color-btntext.png" alt-text="Color swatch of SystemColorWindowTextColor used for foreground color of buttons and any UI that can be interacted with.":::| **SystemColorButtonTextColor**</br>Foreground color of buttons and any UI that can be interacted with.</br></br>Pair with **SystemColorButtonFaceColor**        |
|:::image source="images/sys-color/aquatic-color-3dface.png" alt-text="Color swatch of SystemColorWindowTextColor used for background color of buttons and any UI that can be interacted with.":::| **SystemColorButtonFaceColor**</br>Background color of buttons and any UI that can be interacted with.</br></br>Pair with **SystemColorButtonTextColor**        |

The next table shows how the colors appear when used on a background set to **SystemColorWindowColor**.

| Example | Values |
|---------|---------|
|:::image type="content" source="images/sys-color/aquatic-example-windowtext.png" alt-text="A window with text using the window text color." border="false"::: | **SystemColorWindowTextColor** |
|:::image type="content" source="images/sys-color/aquatic-example-hotlight.png" alt-text="A window with hyperlink text using the hot light color." border="false":::| **SystemColorHotlightColor** |
|:::image type="content" source="images/sys-color/aquatic-example-graytext.png" alt-text="A window with inactive text using the gray text color." border="false":::| **SystemColorGrayTextColor** |
|:::image type="content" source="images/sys-color/aquatic-example-highlighttext+highlight.png" alt-text="A window with text using the highlight text color on the highlight color." border="false":::|**SystemColorHighlightTextColor** + **SystemColorHighlightColor** |
|:::image type="content" source="images/sys-color/aquatic-example-btntext+3dface.png" alt-text="A window with a button using the 3d face color and button text using the button text color." border="false":::| **SystemColorButtonTextColor** + **SystemColorButtonFaceColor**

In the following code snippet, we show how to pick a resource for **BrandedPageBackgroundBrush**. **SystemColorWindowColor** is a good choice here as **BrandedPageBackgroundBrush** indicates that it will be used for a background.

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

The resource is then assigned to the background of an element.

```xaml
<Grid Background="{ThemeResource BrandedPageBackgroundBrush}">
```

We use `{ThemeResource}` twice in the preceding example, once to reference **SystemColorWindowColor** and again to reference **BrandedPageBackgroundBrush**. Both are required for your app to theme correctly at run time. This is a good time to test out the functionality in your app. The **Grid** background will automatically update as you switch to a high contrast theme. It will also update when switching between different high contrast themes.

> [!NOTE]
>**WinUI 2.6 and newer**
>
> There are eight high contrast system brushes available for referencing through a **ResourceKey** (see the following example for **SystemColorWindowTextColorBrush**).
>
> `<StaticResource x:Key="MyControlBackground" ResourceKey="SystemColorWindowTextColorBrush" />`
>
> The brush names match one of the eight previously mentioned system colors exactly (with "Brush" appended). We recommend using a **StaticResource** instead of a local **SolidColorBrush** for performance reasons.

## Best practices

Here are some recommendations for customizing the contrast theme colors in your Windows app.

- Test in all four high contrast themes while your app is running.
- Be consistent.
- Make sure **HighContrastAdjustment** is set to `None` in your app (it is turned on by default). See [Setting HighContrastAdjustment to None](#setting-highcontrastadjustment-to-none).
- **Do not** hard code a color in the HighContrast theme. Instead, use the **SystemColor** `Color` and `ColorBrush` resources. For more detail, see [Hard-coded colors](#hard-coded-colors).
- **Do not** mix background/foreground pairs that are not compatible
- **Do not** choose color resource for aesthetics. Remember, the colors change with the theme.
- **Do not** use `SystemColorGrayTextColor` for body copy that is secondary or acts as hint text. This is intended for disabled content only.
- **Do not** use `SystemColorHotlightColor` and corresponding brush as both are reserved for hyperlinks.

> [!TIP]
> It's often helpful to look at the WinUI Gallery app to see how common controls use the **SystemColor** brushes. If installed already, open them by clicking the following links: [**WinUI 3 Gallery**](winui3gallery:) or [**WinUI 2 Gallery**](winui2gallery:).
>
> If they are not installed, you can download the [**WinUI 3 Gallery**](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) and the [**WinUI 2 Gallery**](https://www.microsoft.com/store/productId/9MSVH128X2ZT) from the Microsoft Store.
>
> You can also get the source code for both from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (use the *main* branch for WinUI 3 and the *winui2* branch for WinUI 2).

### Hard-coded colors

Platform controls provide built-in support for contrast themes, but you should be careful when customizing your application UI. Two of the most common issues occur when either the color of an element is hard-coded or an incorrect **SystemColor** resource is used.

In the following code snippet, we show a Grid element declared with a background color set to `#E6E6E6` (a very light grey). If you hard-code the color in this way, you also override the background color across all themes. For example, if the user selects the **Aquatic** contrast theme, instead of white text on a near black background, the text color in this app changes to white while the background remains light grey. The very low contrast between text and background could make this app very difficult to use.

```xaml
<Grid Background="#E6E6E6">
```

Instead, we recommend using the [**{ThemeResource} markup extension**](/windows/uwp/xaml-platform/themeresource-markup-extension) to reference a color in the [**ThemeDictionaries**](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) collection of a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). This enables the automatic substitution of colors and brushes based on the user's current theme.

```xaml
<Grid Background="{ThemeResource BrandedPageBackgroundBrush}">
```

### Borders

Pages, panes, popups, and bars should all use **SystemColorWindowColor** for their background. Add a contrast theme-only border only where necessary to preserve important boundaries in your UI.

> [!Tip]
> We recommend using 2px borders for transitory surfaces such as flyouts and dialogs.

The navigation pane and the page both share the same background color in contrast themes. To distinguish them, a contrast theme-only border is essential.

:::image type="content" border="false" source="images/contrast-theme-border.png" alt-text="A navigation pane separated from the rest of the page.":::

### List items with colored text

In contrast themes, items in a [ListView](/uwp/api/windows.ui.xaml.controls.listview) have their background set to **SystemColorHighlightColor** when the user hovers over, presses, or selects them. A common issue with complex list items occurs when the content of the list item fails to invert its color, making the items impossible to read.

Be careful when you set the TextBlock.Foreground in the [**DataTemplate**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemtemplate) of the **ListView** (typically done to establish visual hierarchy). The **Foreground** property is set on the **ListViewItem**, and each **TextBlock** in the DataTemplate inherits the correct **Foreground** color. Setting **Foreground** breaks this inheritance.

:::image type="content" border="false" source="images/high-contrast-list1.png" alt-text="Complex list in Light theme and Aquatic theme (note how the text color is not inverted in HighContrast).":::

You can resolve this by setting **Foreground** conditionally through a **Style** in a [**ThemeDictionaries**](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) collection. As the **Foreground** is not set by **SecondaryBodyTextBlockStyle** in **HighContrast**, the color will invert correctly.

:::image type="content" border="false" source="images/high-contrast-list2.png" alt-text="Complex list in Light theme and Aquatic theme (note how the text color is inverted in HighContrast).":::

The following code snippet (from an App.xaml file) shows an example [**ThemeDictionaries**](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) collection in a **ListView** data template.

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

- [Accessibility](accessibility.md)
- [UI contrast and settings sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/XAML%20high%20contrast%20style%20sample%20(Windows%208))
- [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
- [XAML high contrast sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/XAML%20high%20contrast%20style%20sample%20(Windows%208))
- [**AccessibilitySettings**](/uwp/api/Windows.UI.ViewManagement.AccessibilitySettings)
