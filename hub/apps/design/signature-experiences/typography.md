---
description: Segoe UI Variable and the way Windows uses typography to communicate information
title: Typography in Windows
ms.assetid: D12BA18B-1281-4057-9716-2A6D420FAE1B
ms.date: 06/24/2021
ms.topic: article
keywords: windows 11, design, ui, uiux, typography, segoe, segoe ui variable
ms.localizationpriority: medium
---

# Typography in Windows

![Several words rendered in Segoe UI Variable](images/typography_QuickBrownFox.svg)

As the visual representation of language, typography's main task is to communicate information. The Windows type system helps you create structure and hierarchy in your content in order to maximize legibility and readability in your UI.

[Segoe UI Variable](..\downloads\index.md#fonts) is the new system font for Windows. It is a refreshed take on the classic Segoe and uses variable font technology to dynamically provide great legibility at very small sizes, and improved outlines at display sizes.

> [!TIP]
> This article describes how the [Fluent Design language](https://fluent2.microsoft.design/) is applied to Windows apps. For more information, see [**Fluent Design - Typography**](https://fluent2.microsoft.design/typography).

## Using Segoe Fluent Variable

Segoe UI Variable supports two axes for finer control of text: **weight** and **optical size**.

- The weight axis (`wght`) is incremental with weights from Thin (100) to Bold (700).
- The optical size axis (`opsz`) is automatic and on by default. It controls the shape and size of the counters in the font, to prioritize legibility at the small sizes and personality at the large sizes (for optical scaling from 8pt to 36pt).

When using XAML common controls, the Segoe UI Variable font will be selected by default for supported [languages](#languages). When this font or another variable font with an optical axis is used, the optical size will automatically match the requested font-size. When using HTML, optical scaling is also automatic, but you will need to specify the Segoe UI Variable font in CSS.

![The word 'Segoe' rendered in Segoe UI Variable with several aspects of the typeface highlighted](images/typography_Metrics.svg)

### Weights

| Weight name   | Weight axis value | Visual |
|---------------|:-----------------:|--------|
| **Light**     | 300               | ![The word 'Segoe' rendered in Segoe UI Variable light](images/typography_SegoeLight.svg) |
| **Semilight** | 350               | ![The word 'Segoe' rendered in Segoe UI Variable semilight](images/typography_SegoeSemiLight.svg) |
| **Regular**   | 400               | ![The word 'Segoe' rendered in Segoe UI Variable regular](images/typography_SegoeRegular.svg) |
| **Semibold**  | 600               | ![The word 'Segoe' rendered in Segoe UI Variable semibold](images/typography_SegoeSemiBold.svg) |
| **Bold**      | 700               | ![The word 'Segoe' rendered in Segoe UI Variable bold](images/typography_SegoeBold.svg) |

### Optical axis

![A lower case letter a rendered in Segoe UI Variable with outlines of the different shapes it can have based on the context in which it is being rendered](images/typography_OpticalAxis.svg)

## Typography best practices in Windows 11

Windows 11 uses Segoe UI Variable with the following attributes based on the context in which the text is being displayed.

| Attribute          | Value                       | Notes       |
|--------------------|-----------------------------|-------------|
| **Weight**         | Regular, Semibold           | Use regular weight for most text, use Semibold for titles |
| **Alignment**      | Left, Center                | Align left by default, Align center only in rare cases such as text below icons |
| **Minimum values** | 14px Semibold, 12px Regular | Text smaller than these sizes and weights are illegible in some languages |
| **Casing**         | Sentence case               | Use sentence casing for all UI text, including titles |
| **Truncation**     | Ellipses and clipping       | Use ellipses in most cases; clipping is only used in rare cases |

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see Typography principles in action](winui3gallery:/item/Typography)

> The **WinUI 3 Gallery** app includes interactive examples of most WinUI 3 controls, features, and functionality. Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery)

## Typography in Windows Apps

![hero image](../style/images/header-typography.svg)

As the visual representation of language, typography's main task is to communicate information. Its style should never get in the way of that goal. In this article, we'll discuss how to style typography in your Windows app to help users understand content easily and efficiently.

### Font

You should use one font throughout your app's UI, and we recommend sticking with the default font for Windows apps, **Segoe UI Variable**. It's designed to maintain optimal legibility across sizes and pixel densities and offers a clean, light, and open aesthetic that complements the content of the system.

![Sample text of Segoe UI Variable font.](../style/images/type/segoe-sample.svg)

To display non-English languages or to select a different font for your app, please see [Languages](#languages) and [Fonts](#fonts) for our recommended fonts for Windows apps.

### Size and scaling

Font sizes in XAML apps automatically scale on all devices. The scaling algorithm ensures that a 24 px font on a large screen 10 feet away is just as legible as a 24 px font on a small screen that's a few inches away.

![viewing distances for different devices.](../style/images/type/scaling-chart.svg)

Because of how the scaling system works, you're designing in effective pixels, not actual physical pixels, and you shouldn't have to alter font sizes for different screens sizes or resolutions.

### Hierarchy

:::row:::
    :::column:::
Users rely on visual hierarchy when scanning a page: headers summarize content, and body text provides more detail. To create a clear visual hierarchy in your app, follow the Windows type ramp.
    :::column-end:::
    :::column:::
![Screenshot of three lines of text where the font size gets smaller from one line to the next.](../style/images/type/type-hierarchy.svg)
    :::column-end:::
:::row-end:::

### Type ramp

The Windows type ramp establishes crucial relationships between the type styles on a page, helping users read content easily. All sizes are in effective pixels and are optimized for Windows apps running on all screen sizes.

Windows 11 uses the following values for various types of text in the UI.

| Example                                                           | Weight           | Size/line height |
|-------------------------------------------------------------------|------------------|------------------|
| ![Example of caption text](images/typography_caption.svg)| Small            | 12/16 epx        |
| ![Example of body text](images/typography_body.svg) | Text             | 14/20 epx        |
| ![Example of body strong text](images/typography_body_strong.svg)| Text semibold    | 14/20 epx        |
| ![Example of body large text](images/typography_body_large.svg)| Text             | 18/24 epx        |
| ![Example of subtitle text](images/typography_subtitle.svg)| Display semibold | 20/28 epx        |
| ![Example of title text](images/typography_title.svg)| Display semibold | 28/36 epx        |
| ![Example of title large text](images/typography_title_large.svg)| Display semibold | 40/52 epx        |
| ![Example of display text](images/typography_display.svg)| Display semibold | 68/92 epx        |

Check out the guidance on using the [XAML type ramp](../../develop/platform/xaml/xaml-theme-resources.md#the-xaml-type-ramp) for more details.

### Alignment

The default [TextAlignment](/uwp/api/windows.ui.xaml.textalignment) is Left, and in most instances, flush-left and ragged right provides consistent anchoring of the content and a uniform layout. For RTL languages, see [Adjusting layout and fonts to support globalization](../globalizing/adjust-layout-and-fonts--and-support-rtl.md).

![Shows flush-left text.](../style/images/type/alignment.svg)

```xaml
<TextBlock TextAlignment="Left">
```

### Character count

:::row:::
    :::column:::
![Fourth screenshot of a green bar that has a green check mark and the word Do in it.](../style/images/do.svg)
Keep to 50–60 letters per line for ease of reading.
    :::column-end:::
    :::column:::
![don't](../style/images/dont.svg)
Don't use fewer than 20 characters or more than 60 characters per line as this is difficult to read.
    :::column-end:::
:::row-end:::

### Clipping and ellipses

When the amount of text extends beyond the space available, we recommend clipping the text and inserting ellipses [...], which is the default behavior of most [UWP text controls](../controls/text-controls.md).

![Shows a device frame with some text clipping.](../style/images/type/clipping.svg)

```xaml
<TextBlock TextWrapping="WrapWholeWords" TextTrimming="Clip"/>
```

:::row:::
    :::column:::
![Fifth screenshot of a green bar that has a green check mark and the word Do in it.](../style/images/do.svg)
Clip text, and wrap if multiple lines are enabled.
    :::column-end:::
    :::column:::
![don't](../style/images/dont.svg)
Don't use ellipses to avoid visual clutter.
    :::column-end:::
:::row-end:::

> [!NOTE]
> If containers are not well-defined (for example, no differentiating background color), or when there is a link to see more text, then use ellipses.

## Languages

Segoe UI Variable is our font for English, European languages, Greek, and Russian. For other languages, see the following recommendations.

### Globalizing/localizing fonts

Use the [LanguageFont font-mapping APIs](/uwp/api/windows.globalization.fonts.languagefont) for programmatic access to the recommended font family, size, weight, and style for a particular language. The LanguageFont object provides access to the correct font info for various categories of content including UI headers, notifications, body text, and user-editable document body fonts. For more info, see [Adjusting layout and fonts to support globalization](../globalizing/adjust-layout-and-fonts--and-support-rtl.md).

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
| Segoe MDL2 Assets | Regular | User-interface font for app icons. For more info, see the [Segoe Fluent Icons font](../style/segoe-fluent-icons-font.md) article. |
| Segoe UI Emoji | Regular | User-interface font for Emoji. |
| Segoe UI Symbol | Regular | Fallback font for symbols. |

## Related articles

- [Text controls](../controls/text-controls.md)
- [XAML theme resources](../../develop/platform/xaml/xaml-theme-resources.md#the-xaml-type-ramp)
- [XAML styles](../../develop/platform/xaml/xaml-styles.md)
- [Microsoft Typography](/typography/)
- [Variable Fonts](/typography/develop/font-variations)
