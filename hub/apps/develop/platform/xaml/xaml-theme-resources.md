---
description: Theme resources in XAML are a set of resources that apply different values depending on which system theme is active.
MS-HAID: dev\_ctrl\_layout\_txt.xaml\_theme\_resources
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: XAML theme resources
ms.assetid: 41B87DBF-E7A2-44E9-BEBA-AF6EEBABB81B
label: XAML theme resources
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML theme resources

Theme resources in XAML are a set of resources that apply different values depending on which system theme is active. There are 3 themes that the XAML framework supports: "Light", "Dark", and "HighContrast".

**Prerequisites**: This topic assumes that you have read [ResourceDictionary and XAML resource references](xaml-resource-dictionary.md).

## Theme resources v. static resources

There are two XAML markup extensions that can reference a XAML resource from an existing XAML resource dictionary: [{StaticResource} markup extension](staticresource-markup-extension.md) and [{ThemeResource} markup extension](themeresource-markup-extension.md).

Evaluation of a [{ThemeResource} markup extension](themeresource-markup-extension.md) occurs when the app loads and subsequently each time the theme changes at runtime. This is typically the result of the user changing their device settings or from a programmatic change within the app that alters its current theme.

In contrast, a [{StaticResource} markup extension](staticresource-markup-extension.md) is evaluated only when the XAML is first loaded by the app. It does not update. It's similar to a find and replace in your XAML with the actual runtime value at app launch.

## Theme resources in the resource dictionary structure

Each theme resource is part of the XAML file themeresources.xaml. For design purposes, themeresources.xaml is available in the \\(Program Files)\\Windows Kits\\10\\DesignTime\\CommonConfiguration\\Neutral\\UAP\\&lt;SDK version&gt;\\Generic folder from a Windows Software Development Kit (SDK) installation. The resource dictionaries in themeresources.xaml are also reproduced in generic.xaml in the same directory.

The Windows Runtime doesn't use these physical files for runtime lookup. That's why they are specifically in a DesignTime folder, and they aren't copied into apps by default. Instead, these resource dictionaries exist in memory as part of the Windows Runtime itself, and your app's XAML resource references to theme resources (or system resources) resolve there at runtime.

## Guidelines for custom theme resources

Follow these guidelines when you define and consume your own custom theme resources:

- Specify theme dictionaries for both "Light" and "Dark" in addition to your "HighContrast" dictionary. Although you can create a [ResourceDictionary](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.ResourceDictionary) with "Default" as the key, it's preferred to be explicit and instead use "Light", "Dark", and "HighContrast".

- Use the [{ThemeResource} markup extension](themeresource-markup-extension.md) in: Styles, Setters, Control templates, Property setters, and Animations.

- Don't use the [{ThemeResource} markup extension](themeresource-markup-extension.md) in your resource definitions inside your [ThemeDictionaries](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary.themedictionaries). Use [{StaticResource} markup extension](staticresource-markup-extension.md) instead.

    EXCEPTION: You can use the [{ThemeResource} markup extension](themeresource-markup-extension.md) to reference resources that are agnostic to the app theme in your [ThemeDictionaries](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary.themedictionaries). Examples of these resources are accent color resources like `SystemAccentColor`, or system color resources, which are typically prefixed with "SystemColor" like `SystemColorButtonFaceColor`.

> [!CAUTION]
> If you don't follow these guidelines, you might see unexpected behavior related to themes in your app. For more info, see the [Troubleshooting theme resources](#troubleshooting-theme-resources) section.

## The XAML color ramp and theme-dependent brushes

The combined set of colors for "Light", "Dark", and "HighContrast" themes make up the *Windows color ramp* in XAML. Whether you want to modify the system themes, or apply a theme to your own XAML elements, it's important to understand how the color resources are structured.

For additional information about how to apply color in your Windows app, please see [Color in Windows apps](../../../design/signature-experiences/color.md).

### Light and Dark theme colors

The XAML framework provides a set of named [Color](/uwp/api/Windows.UI.Color) resources with values that are tailored for the "Light" and "Dark" themes. For WinUI 2, the theme resources are defined in the [Common theme resources Xaml file](https://github.com/microsoft/microsoft-ui-xaml/blob/winui2/main/dev/CommonStyles/Common_themeresources_any.xaml). The color names are very descriptive of their intended usage, and there's a corresponding SolidColorBrush resource for every Color resource.

> [!TIP]
> [!div class="nextstepaction"]
> For a visual overview of these colors, see the WinUI 3 Gallery app: [Colors](winui3gallery://item/Colors)

> The **WinUI 3 Gallery** app includes interactive examples of most WinUI 3 controls, features, and functionality. Get the app from the [Microsoft Store](https://apps.microsoft.com/detail/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery)

### Windows system contrast theme colors

In addition to the set of resources provided by the XAML framework, there's a set of color values derived from the Windows system palette. These colors are not specific to the Windows Runtime or Windows apps. However, many of the XAML [Brush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Brush) resources consume these colors when the system is operating (and the app is running) using the "HighContrast" theme. The XAML framework provides these system-wide colors as keyed resources. The keys follow the naming format: `SystemColor[name]Color`.

For more information about supporting contrast themes, see [Contrast themes](../../../design/accessibility/high-contrast-themes.md).

### System accent color

In addition to the system contrast theme colors, the system accent color is provided as a special color resource using the key `SystemAccentColor`. At runtime, this resource gets the color that the user has specified as the accent color in the Windows personalization settings.

> [!NOTE]
> While it's possible to override the system color resources, it's a best practice to respect the user's color choices, especially for contrast theme settings.

### Theme-dependent brushes

The color resources shown in the preceding sections are used to set the [Color](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush.Color) property of [SolidColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush) resources in the system theme resource dictionaries. You use the brush resources to apply the color to XAML elements.

Let's look at how the color value for this brush is determined at run-time. In the "Light" and "Dark" resource dictionaries, this brush is defined like this:

`<SolidColorBrush x:Key="TextFillColorPrimaryBrush" Color="{StaticResource TextFillColorPrimary}"/>`

In the "HighContrast" resource dictionary, this brush is defined like this:

`<SolidColorBrush x:Key="TextFillColorPrimaryBrush" Color="{ThemeResource SystemColorWindowTextColor}"/>`

When this brush is applied to a XAML element, its color is determined at run-time by the current theme, as shown in this table.

| Theme        | Color resource             | Runtime value                                              |
|--------------|----------------------------|------------------------------------------------------------|
| Light        | TextFillColorPrimary       | \#E4000000                                                 |
| Dark         | TextFillColorPrimary       | \#FFFFFFFF                                                 |
| HighContrast | SystemColorWindowTextColor | The color specified in settings for Text. |

## The XAML type ramp

The themeresources.xaml file defines several resources that define a [Style](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Style) that you can apply to text containers in your UI, specifically for either [TextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.TextBlock) or [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RichTextBlock). These are not the default implicit styles. They are provided to make it easier for you to create XAML UI definitions that match the *Windows type ramp* documented in [Guidelines for fonts](../../../design/signature-experiences/typography.md).

These styles are for text attributes that you want applied to the whole text container. If you want styles applied just to sections of the text, set attributes on the text elements within the container, such as on a [Run](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Documents.Run) in [TextBlock.Inlines](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.inlines) or on a [Paragraph](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Documents.Paragraph) in [RichTextBlock.Blocks](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock.blocks).

The styles look like this when applied to a [TextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.TextBlock):

![text block styles](images/text-block-type-ramp.svg)

| Style      | Weight  | Size |
|------------|---------|------|
|Caption     |Regular  |    12|
|Body        |Regular  |    14|
|Body Strong |Semibold |    14|
|Body Large  |Regular  |    18|
|Subtitle    |Semibold |    20|
|Title       |Semibold |    28|
|Title Large |Semibold |    40|
|Display     |Semibold |    68|

```xaml
<TextBlock Text="Caption" Style="{StaticResource CaptionTextBlockStyle}"/>
<TextBlock Text="Body" Style="{StaticResource BodyTextBlockStyle}"/>
<TextBlock Text="Body Strong" Style="{StaticResource BodyStrongTextBlockStyle}"/>
<TextBlock Text="Body Large" Style="{StaticResource BodyLargeTextBlockStyle}"/>
<TextBlock Text="Subtitle" Style="{StaticResource SubtitleTextBlockStyle}"/>
<TextBlock Text="Title" Style="{StaticResource TitleTextBlockStyle}"/>
<TextBlock Text="Title Large" Style="{StaticResource TitleLargeTextBlockStyle}"/>
<TextBlock Text="Display" Style="{StaticResource DisplayTextBlockStyle}"/>
```

For guidance on how to use the Windows type ramp in your app, see [Typography in Windows apps](../../../design/signature-experiences/typography.md).

For details of the XAML styles, see WinUI on GitHub:

- [Caption TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L21)
- [Body TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L27)
- [Body Strong TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L33)
 <!--- [Body Large TextBlockStyle]()-->
- [Subtitle TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L37)
- [Title TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L42)
- [Title Large TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L47)
- [Display TextBlockStyle](https://github.com/microsoft/microsoft-ui-xaml/blob/cb181acede22577c59c5dc250361d3340252f4e9/dev/CommonStyles/TextBlock_themeresources.xaml#L52)

> [!TIP]
> [!div class="nextstepaction"]
> For a visual overview of these styles, see the WinUI 3 Gallery app: [Typography](winui3gallery://item/Typography)

### BaseRichTextBlockStyle

**TargetType**: [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RichTextBlock)

Supplies the common properties for all the other [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RichTextBlock) container styles.

```xaml
<!-- Usage -->
<RichTextBlock Style="{StaticResource BaseRichTextBlockStyle}">
    <Paragraph>Rich text.</Paragraph>
</RichTextBlock>

<!-- Style definition -->
<Style x:Key="BaseRichTextBlockStyle" TargetType="RichTextBlock">
    <Setter Property="FontFamily" Value="Segoe UI"/>
    <Setter Property="FontWeight" Value="SemiBold"/>
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="TextTrimming" Value="None"/>
    <Setter Property="TextWrapping" Value="Wrap"/>
    <Setter Property="LineStackingStrategy" Value="MaxHeight"/>
    <Setter Property="TextLineBounds" Value="Full"/>
    <Setter Property="OpticalMarginAlignment" Value="TrimSideBearings"/>
</Style>
```

### BodyRichTextBlockStyle

```xaml
<!-- Usage -->
<RichTextBlock Style="{StaticResource BodyRichTextBlockStyle}">
    <Paragraph>Rich text.</Paragraph>
</RichTextBlock>

<!-- Style definition -->
<Style x:Key="BodyRichTextBlockStyle" TargetType="RichTextBlock" BasedOn="{StaticResource BaseRichTextBlockStyle}">
    <Setter Property="FontWeight" Value="Normal"/>
</Style>
```

**Note**:  The [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RichTextBlock) styles don't have all the text ramp styles that [TextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.TextBlock) does, mainly because the block-based document object model for **RichTextBlock** makes it easier to set attributes on the individual text elements. Also, setting [TextBlock.Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.text) using the XAML content property introduces a situation where there is no text element to style and thus you'd have to style the container. That isn't an issue for **RichTextBlock** because its text content always has to be in specific text elements like [Paragraph](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Documents.Paragraph), which is where you might apply XAML styles for page header, page subheader and similar text ramp definitions.

## Miscellaneous Named styles

There's an additional set of keyed [Style](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Style) definitions you can apply to style a [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) differently than its default implicit style.

### NavigationBackButtonNormalStyle

**TargetType**: [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button)

This [Style](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Style) provides a complete template for a [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) that can be the navigation back button for a navigation app. The default dimensions are 40 x 40 pixels. To tailor the styling you can either explicitly set the [Height](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.FrameworkElement.Height), [Width](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.FrameworkElement.Width), [FontSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.fontsize), and other properties on your **Button** or create a derived style using [BasedOn](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.style.basedon).

Here's a [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) with the **NavigationBackButtonNormalStyle** resource applied to it.

```xaml
<Button Style="{StaticResource NavigationBackButtonNormalStyle}" />
```

It looks like this:

![A button styled as a back button](images/styles-back-button-normal.png)

### NavigationBackButtonSmallStyle

**TargetType**: [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button)

This [Style](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Style) provides a complete template for a [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) that can be the navigation back button for a navigation app. It's similar to **NavigationBackButtonNormalStyle**, but its dimensions are 30 x 30 pixels.

Here's a [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) with the **NavigationBackButtonSmallStyle** resource applied to it.

```xaml
<Button Style="{StaticResource NavigationBackButtonSmallStyle}" />
```

## Troubleshooting theme resources

If you don't follow the [guidelines for using theme resources](#guidelines-for-custom-theme-resources), you might see unexpected behavior related to themes in your app.

For example, when you open a light-themed flyout, parts of your dark-themed app also change as if they were in the light theme. Or if you navigate to a light-themed page and then navigate back, the original dark-themed page (or parts of it) now looks as though it is in the light theme.

Typically, these types of issues occur when you provide a "Default" theme and a "HighContrast" theme to support high-contrast scenarios, and then use both "Light" and "Dark" themes in different parts of your app.

For example, consider this theme dictionary definition:

```xaml
<!-- DO NOT USE. THIS XAML DEMONSTRATES AN ERROR. -->
<ResourceDictionary>
  <ResourceDictionary.ThemeDictionaries>
    <ResourceDictionary x:Key="Default">
      <SolidColorBrush x:Key="myBrush" Color="{ThemeResource ControlFillColorDefault}"/>
    </ResourceDictionary>
    <ResourceDictionary x:Key="HighContrast">
      <SolidColorBrush x:Key="myBrush" Color="{ThemeResource SystemColorButtonFaceColor}"/>
    </ResourceDictionary>
  </ResourceDictionary.ThemeDictionaries>
</ResourceDictionary>
```

Intuitively, this looks correct. You want to change the color pointed to by `myBrush` when in high-contrast, but when not in high-contrast, you rely on the [{ThemeResource} markup extension](themeresource-markup-extension.md) to make sure that `myBrush` points to the right color for your theme. If your app never has [FrameworkElement.RequestedTheme](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.requestedtheme) set on elements within its visual tree, this will typically work as expected. However, you run into problems in your app as soon as you start to re-theme different parts of your visual tree.

The problem occurs because brushes are shared resources, unlike most other XAML types. If you have 2 elements in XAML sub-trees with different themes that reference the same brush resource, then as the framework walks each sub-tree to update its [{ThemeResource} markup extension](themeresource-markup-extension.md) expressions, changes to the shared brush resource are reflected in the other sub-tree, which is not your intended result.

To fix this, replace the "Default" dictionary with separate theme dictionaries for both "Light" and "Dark" themes in addition to "HighContrast":

```xaml
<!-- DO NOT USE. THIS XAML DEMONSTRATES AN ERROR. -->
<ResourceDictionary>
  <ResourceDictionary.ThemeDictionaries>
    <ResourceDictionary x:Key="Light">
      <SolidColorBrush x:Key="myBrush" Color="{ThemeResource ControlFillColorDefault}"/>
    </ResourceDictionary>
    <ResourceDictionary x:Key="Dark">
      <SolidColorBrush x:Key="myBrush" Color="{ThemeResource ControlFillColorDefault}"/>
    </ResourceDictionary>
    <ResourceDictionary x:Key="HighContrast">
      <SolidColorBrush x:Key="myBrush" Color="{ThemeResource SystemColorButtonFaceColor}"/>
    </ResourceDictionary>
  </ResourceDictionary.ThemeDictionaries>
</ResourceDictionary>
```

However, problems still occur if any of these resources are referenced in inherited properties like [Foreground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.foreground). Your custom control template might specify the foreground color of an element using the [{ThemeResource} markup extension](themeresource-markup-extension.md), but when the framework propagates the inherited value to child elements, it provides a direct reference to the resource that was resolved by the {ThemeResource} markup extension expression. This causes problems when the framework processes theme changes as it walks your control's visual tree. It re-evaluates the {ThemeResource} markup extension expression to get a new brush resource but doesn't yet propagate this reference down to the children of your control; this happens later, such as during the next measure pass.

As a result, after walking the control visual tree in response to a theme change, the framework walks the children and updates any [{ThemeResource} markup extension](themeresource-markup-extension.md) expressions set on them, or on objects set on their properties. This is where the problem occurs; the framework walks the brush resource and because it specifies its color using a {ThemeResource} markup extension, it's re-evaluated.

At this point, the framework appears to have polluted your theme dictionary because it now has a resource from one dictionary that has its color set from another dictionary.

To fix this problem, use the [{StaticResource} markup extension](staticresource-markup-extension.md) instead of [{ThemeResource} markup extension](themeresource-markup-extension.md). With the guidelines applied, the theme dictionaries look like this:

```xaml
<ResourceDictionary>
  <ResourceDictionary.ThemeDictionaries>
    <ResourceDictionary x:Key="Light">
      <SolidColorBrush x:Key="myBrush" Color="{StaticResource ControlFillColorDefault}"/>
    </ResourceDictionary>
    <ResourceDictionary x:Key="Dark">
      <SolidColorBrush x:Key="myBrush" Color="{StaticResource ControlFillColorDefault}"/>
    </ResourceDictionary>
    <ResourceDictionary x:Key="HighContrast">
      <SolidColorBrush x:Key="myBrush" Color="{ThemeResource SystemColorButtonFaceColor}"/>
    </ResourceDictionary>
  </ResourceDictionary.ThemeDictionaries>
</ResourceDictionary>
```

Notice that the [{ThemeResource} markup extension](themeresource-markup-extension.md) is still used in the "HighContrast" dictionary instead of [{StaticResource} markup extension](staticresource-markup-extension.md). This situation falls under the exception given earlier in the guidelines. Most of the brush values that are used for the "HighContrast" theme are using color choices that are globally controlled by the system, but exposed to XAML as a specially-named resource (those prefixed with 'SystemColor' in the name). The system enables the user to set the specific colors that should be used for their contrast theme settings through the Ease of Access Center. Those color choices are applied to the specially-named resources. The XAML framework uses the same theme changed event to also update these brushes when it detects they've changed at the system level. This is why the {ThemeResource} markup extension is used here.
