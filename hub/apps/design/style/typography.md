---
description: Learn how to use typography in your app to help users understand content easily.
title: Typography in Windows apps
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: RS5
---
# Typography in Windows Apps

![hero image](images/header-typography.svg)

As the visual representation of language, typography's main task is to communicate information. Its style should never get in the way of that goal. In this article, we'll discuss how to style typography in your Windows app to help users understand content easily and efficiently.

## Font

You should use one font throughout your app's UI, and we recommend sticking with the default font for Windows apps, **Segoe UI Variable**. It's designed to maintain optimal legibility across sizes and pixel densities and offers a clean, light, and open aesthetic that complements the content of the system.

![Sample text of Segoe UI Variable font.](images/type/segoe-sample.svg)

To display non-English languages or to select a different font for your app, please see [Languages](#languages) and [Fonts](#fonts) for our recommended fonts for Windows apps.

:::row:::
    :::column:::
![First screenshot of a green bar that has a green check mark and the word Do in it.](images/do.svg)
Pick one font for your UI.
    :::column-end:::
    :::column:::
![don't](images/dont.svg)
Don't mix multiple fonts.
    :::column-end:::
:::row-end:::

## Variable font axes

The **Segoe UI Variable** font contains two axes for finer control of text. This font has a weight axis (`wght`) with weights from Thin (100) to Bold (700). It also has an optical size axis (`opsz`) for optical scaling from 8pt to 36pt. When using XAML common controls, the Segoe UI Variable font will be selected by default for supported [languages](#languages). When this font or another variable font with an optical axis is used, the optical size will automatically match the requested font-size. When using HTML, optical scaling is also automatic, but you will need to specify the Segoe UI Variable font in CSS.

## Size and scaling

Font sizes in UWP apps automatically scale on all devices. The scaling algorithm ensures that a 24 px font on Surface Hub 10 feet away is just as legible as a 24 px font on 5" phone that's a few inches away.

![viewing distances for different devices.](images/type/scaling-chart.svg)

Because of how the scaling system works, you're designing in effective pixels, not actual physical pixels, and you shouldn't have to alter font sizes for different screens sizes or resolutions.

:::row:::
    :::column:::
![Second screenshot of a green bar that has a green check mark and the word Do in it.](images/do.svg)
Follow the Windows [type ramp](#type-ramp) sizing.
    :::column-end:::
    :::column:::
![don't](images/dont.svg)
Don't use a font size smaller than 12 px.
    :::column-end:::
:::row-end:::

## Hierarchy

:::row:::
    :::column:::
Users rely on visual hierarchy when scanning a page: headers summarize content, and body text provides more detail. To create a clear visual hierarchy in your app, follow the Windows type ramp.
    :::column-end:::
    :::column:::
![Screenshot of three lines of text where the font size gets smaller from one line to the next.](images/type/type-hierarchy.svg)
    :::column-end:::
:::row-end:::

### Type ramp

The Windows type ramp establishes crucial relationships between the type styles on a page, helping users read content easily. All sizes are in effective pixels and are optimized for UWP apps running on all devices.

![The Windows type ramp.](images/type/text-block-type-ramp.svg)

Check out the guidance on using the [XAML type ramp](../style/xaml-theme-resources.md#the-xaml-type-ramp) for more details.

## Alignment

The default [TextAlignment](/uwp/api/windows.ui.xaml.textalignment) is Left, and in most instances, flush-left and ragged right provides consistent anchoring of the content and a uniform layout. For RTL languages, see [Adjusting layout and fonts to support globalization](../globalizing/adjust-layout-and-fonts--and-support-rtl.md).

![Shows flush-left text.](images/type/alignment.svg)

```xaml
<TextBlock TextAlignment="Left">
```

## Character count

:::row:::
    :::column:::
![Fourth screenshot of a green bar that has a green check mark and the word Do in it.](images/do.svg)
Keep to 50–60 letters per line for ease of reading.
    :::column-end:::
    :::column:::
![don't](images/dont.svg)
Don't use fewer than 20 characters or more than 60 characters per line as this is difficult to read.
    :::column-end:::
:::row-end:::

## Clipping and ellipses

When the amount of text extends beyond the space available, we recommend clipping the text and inserting ellipses [...], which is the default behavior of most [UWP text controls](../controls/text-controls.md).

![Shows a device frame with some text clipping.](images/type/clipping.svg)

```xaml
<TextBlock TextWrapping="WrapWholeWords" TextTrimming="Clip"/>
```

:::row:::
    :::column:::
![Fifth screenshot of a green bar that has a green check mark and the word Do in it.](images/do.svg)
Clip text, and wrap if multiple lines are enabled.
    :::column-end:::
    :::column:::
![don't](images/dont.svg)
Don't use ellipses to avoid visual clutter.
    :::column-end:::
:::row-end:::

> [!NOTE]
> If containers are not well-defined (for example, no differentiating background color), or when there is a link to see more text, then use ellipses.

## Languages

Segoe UI Variable is our font for English, European languages, Greek, and Russian. For other languages, see the following recommendations.

### Globalizing/localizing fonts

Use the [LanguageFont font-mapping APIs](/uwp/api/Windows.Globalization.Fonts.LanguageFont) for programmatic access to the recommended font family, size, weight, and style for a particular language. The LanguageFont object provides access to the correct font info for various categories of content including UI headers, notifications, body text, and user-editable document body fonts. For more info, see [Adjusting layout and fonts to support globalization](../globalizing/adjust-layout-and-fonts--and-support-rtl.md).

### Fonts for non-Latin languages

| Font-family | Styles | Notes |
|---------|---------|---------|
| Ebrima | Regular, Bold | User-interface font for African scripts (ADLaM, Ethiopic, N'Ko, Osmanya, Tifinagh, Vai). |
| Gadugi | Regular, Bold | User-interface font for North American scripts (Canadian Syllabics, Cherokee, Osage). |
| Leelawadee UI</p> | Regular, Semilight, Bold | User-interface font for Southeast Asian scripts (Buginese, Khmer, Lao, Thai). |
| Malgun Gothic</p> | Regular | User-interface font for Korean. |
| Microsoft JhengHei UI</p> | Regular, Bold, Light | User-interface font for Traditional Chinese. |
| Microsoft YaHei UI</p> | Regular, Bold, Light | User-interface font for Simplified Chinese. |
| Myanmar Text</p> | Regular | Fallback font for Myanmar script. |
| Nirmala UI</p> | Regular, Semilight, Bold | User-interface font for South Asian scripts (Bangla, Chakma, Devanagari, Gujarati, Gurmukhi, Kannada, Malayalam, Meetei Mayek, Odia, Ol Chiki, Sinhala, Sora Sompeng, Tamil, Telugu). |
| Segoe UI</p> | Regular, Italic, Light Italic, Black Italic, Bold, Bold Italic, Light, Semilight, Semibold, Black | User-interface font for Arabic, Armenian, Georgian, and Hebrew. |
| SimSun</p> | Regular | A legacy Chinese UI font. |
| Yu Gothic UI</p> | Light, Semilight, Regular, Semibold, Bold | User-interface font for Japanese. |

## Fonts

### Sans-serif fonts

Sans-serif fonts are a great choice for headings and UI elements.

| Font-family | Styles | Notes |
|---------|---------|---------|
| Arial | Regular, Italic, Bold, Bold Italic, Black | Supports European and Middle Eastern scripts (Latin, Greek, Cyrillic, Arabic, Armenian, and Hebrew). Black weight supports European scripts only. |
| Calibri | Regular, Italic, Bold, Bold Italic, Light, Light Italic | Supports European and Middle Eastern scripts (Latin, Greek, Cyrillic, Arabic and Hebrew). Arabic available in the uprights only. |
| Consolas | Regular, Italic, Bold, Bold Italic | Fixed width font that supports European scripts (Latin, Greek and Cyrillic). |
| Segoe UI | Regular, Italic, Light Italic, Black Italic, Bold, Bold Italic, Light, Semilight, Semibold, Black | User-interface font for European and Middle East scripts (Arabic, Armenian, Cyrillic, Georgian, Greek, Hebrew, Latin), and also Lisu script. |
| Selawik | Regular, Semilight, Light, Bold, Semibold | An open-source font that's metrically compatible with Segoe UI, intended for apps on other platforms that don't want to bundle Segoe UI. [Get Selawik on GitHub](https://github.com/Microsoft/Selawik). |

### Serif fonts

Serif fonts are good for presenting large amounts of text.

| Font-family | Styles | Notes |
|---------|---------|---------|
| Cambria | Regular | Serif font that supports European scripts (Latin, Greek, Cyrillic). |
| Courier New | Regular, Italic, Bold, Bold Italic | Serif fixed width font that supports European and Middle Eastern scripts (Latin, Greek, Cyrillic, Arabic, Armenian, and Hebrew). |
| Georgia | Regular, Italic, Bold, Bold Italic | Supports European scripts (Latin, Greek and Cyrillic). |
| Times New Roman | Regular, Italic, Bold, Bold Italic | Legacy font that supports European scripts (Latin, Greek, Cyrillic, Arabic, Armenian, Hebrew). |

### Variable fonts

Variable fonts are good for precisely controlling the appearance of text.

| Font-family | Axes | Notes |
|---------|---------|---------|
| Bahnschrift | Weight, Width | Variable font that supports Latin, Greek, and Cyrillic. |
| Segoe UI Variable | Weight, Optical Size | Variable font that supports Latin, Greek, and Cyrillic. |

### Symbols and icons

| Font-family | Styles | Notes |
|---------|---------|---------|
| Segoe MDL2 Assets | Regular | User-interface font for app icons. For more info, see the [Segoe MDL2 assets](segoe-ui-symbol-font.md) article. |
| Segoe UI Emoji | Regular | User-interface font for Emoji. |
| Segoe UI Symbol | Regular | Fallback font for symbols. |

## Related articles

* [Text controls](../controls/text-controls.md)
* [XAML theme resources](../style/xaml-theme-resources.md#the-xaml-type-ramp)
* [XAML styles](../style/xaml-styles.md)
* [Microsoft Typography](/typography/)
* [Variable Fonts](/typography/develop/font-variations)
