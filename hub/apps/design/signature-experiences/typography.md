---
description: Segoe UI Variable and the way Windows 11 uses typography to communicate information
title: Typography in Windows 11
ms.assetid: D12BA18B-1281-4057-9716-2A6D420FAE1B
ms.date: 06/24/2021
ms.topic: article
keywords: windows 11, design, ui, uiux, typography, segoe, segoe ui variable
ms.localizationpriority: medium
---

# Typography in Windows 11

![Several words rendered in Segoe UI Variable](images/typography_QuickBrownFox.svg)

As the visual representation of language, typography's main task is to communicate information. The Windows 11 type system helps you create structure and hierarchy in your content in order to maximize legibility and readability in your UI.

[Segoe UI Variable](..\downloads\index.md#fonts) is the new system font for Windows. It is a refreshed take on the classic Segoe and uses variable font technology to dynamically provide great legibility at very small sizes, and improved outlines at display sizes.

## Metrics

![The word 'Segoe' rendered in Segoe UI Variable with several aspects of the typeface highlighted](images/typography_Metrics.svg)
https://github.com/MicrosoftDocs/windows-dev-docs/blob/docs/hub/apps/design/signature-experiences/typography.md
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

Segoe UI Variable supports two axes: **weight** and **optical size**. The weight axis is incremental, while the optical size axis is automatic and on by default. The optical size axis controls the shape and size of the counters in the font, to prioritize legibility at the small sizes and personality at the large sizes.

## Using Segoe Fluent Variable

### Type ramp

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

> [!TIP]
> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see Typography principles in action](winui3gallery:/item/Typography).

> The **WinUI 3 Gallery** app includes interactive examples of most WinUI 3 controls, features, and functionality. Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery)
