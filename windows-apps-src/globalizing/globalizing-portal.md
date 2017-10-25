---
author: stevewhims
Description: Learn about the benefits of globalizing and localizing your app, and exactly what these terms mean.
Search.SourceType: Video
title: Globalization and localization
ms.assetid: c0791eec-5bb8-4a13-8977-61d7d98e35ce
label: Intro
template: detail.hbs
ms.author: stwhi
ms.date: 10/18/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Globalization and localization
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Windows is used worldwide by audiences that are diverse in terms of language, region, and culture. Your users speak a variety of different languages and in a variety of different countries and regions. Some users speak more than one language. So, your app runs on configurations that involve many permutations of system settings for language, region, and culture. You can increase the potential market for your app by designing it to be readily adaptable, using *globalization* and *localization*.

This video provides a brief introduction on how to prepare your app for the world: [Introduction to globalization and localization](https://channel9.msdn.com/Blogs/One-Dev-Minute/Introduction-to-globalization-and-localization).

**Globalization** is the process of designing and developing your app in such a way that it functions appropriately in different global markets (on systems with different language and culture configurations) without requiring culture-specific changes or customization.

- Take culture into account when manipulating strings, for example when changing the case of strings for comparison.
- Use calendars that are appropriate for the current culture.
- Use globalization APIs to display data that are formatted appropriately for the country or region, such as numbers, dates, times, and currencies.
- Take into account that different cultures have different rules for collating (sorting) text and other data.

Your code needs to function equally well in any of the cultures that you've determined that your app will support. Ideally, your code will function equally well in the context of *any* language, region, or culture. The most efficient way to globalize your app's functions is to use the concept of cultures/locales. A culture/locale is a set of rules and a set of data that are specific to a given language and geographic area. These rules and data include information about the following.

- Character classification
- Writing system
- Date and time formatting
- Numeric, currency, weight, and measure conventions
- Sorting rules

**Localizability** is the process of preparing a globalized app for localization and/or verifying that the app is ready for localization. Correctly making an app localizable means that the later localization process will not uncover any functional defects in the app. The most essential property of a localizable app is that its executable code has been cleanly separated from the app's localizable resources.

- Strings translated into different languages can vary greatly in length. So, design your UI to accommodate different text lengths and font sizes for labels and text input controls.
- Don't include text in images.
- Don't hard-code strings and culture-dependent images in your app's code and markup. Instead, store them as string and image resources so that they can be adapted to different local markets independently of your app's built binaries.
- Pseudo-localize your app to disclose any localizability issues.

**Localization** is the process of adapting or translating your app's localizable resources to meet the language, cultural, and political requirements of the specific local markets that the app is intended to support. By the time you reach the stage of localizing your app, if your app is localizable then you will not have to modify any logic during this process.

- Translate the string resources and other assets of the app for the new market.
- Modify any culture-dependent images as necessary.

Most localization teams use special tools to aid the process. For example, by recycling translations of recurring text.

| Article | Description |
|---------|-------------|
| [Globalization and localization do's and don'ts](guidelines-and-checklist-for-globalizing-your-app.md) | Follow these best practices when globalizing your apps for a wider audience and when localizing your apps for a specific market. |
| [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md) | Develop your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction. |
| [Use patterns to format dates and times](use-patterns-to-format-dates-and-times.md) | Use the [**Windows.Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859) API with custom patterns to display dates and times in exactly the format you wish. |
| [Manage language and region](manage-language-and-region.md) | Control how Windows selects UI resources and formats the UI elements of the app, by using the various language and region settings provided by Windows. |
| [Prepare your app for localization](prepare-your-app-for-localization.md) | Prepare your app for localization to other markets, languages, or regions. |
| [Use global-ready formats](use-global-ready-formats.md) | Develop a global-ready app by appropriately formatting dates, times, numbers, phone numbers, and currencies. |

Also see the documentation originally created for Windows 8.x, which still applies to Universal Windows Platform (UWP) apps and Windows 10.

-   [Globalizing your app](https://msdn.microsoft.com/library/windows/apps/xaml/hh965328)
-   [Language matching](https://msdn.microsoft.com/library/windows/apps/xaml/jj673578.aspx)
-   [NumeralSystem values](https://msdn.microsoft.com/library/windows/apps/xaml/jj236471.aspx)
-   [International fonts](https://msdn.microsoft.com/library/windows/apps/xaml/dn263115.aspx)
-   [App resources and localization](https://msdn.microsoft.com/library/windows/apps/xaml/hh710212.aspx)
