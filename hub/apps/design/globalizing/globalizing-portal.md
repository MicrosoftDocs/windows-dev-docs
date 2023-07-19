---
description: Learn about the benefits of globalizing and localizing your app, and exactly what these terms mean.
Search.SourceType: Video
title: Globalization and localization
ms.assetid: c0791eec-5bb8-4a13-8977-61d7d98e35ce
label: Intro
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---
# Globalization and localization

Windows is used worldwide by audiences that are diverse in terms of language, region, and culture. Your users speak a variety of different languages and in a variety of different countries and regions. Some users speak more than one language. So, your app runs on configurations that involve many permutations of system settings for language, region, and culture. You can increase the potential market for your app by designing it to be readily adaptable, using *globalization* and *localization*.

For a short video that demonstrates how to prepare your app for the world, see [Introduction to globalization and localization](https://web.archive.org/web/20210123201259/https://channel9.msdn.com/Blogs/One-Dev-Minute/Introduction-to-globalization-and-localization).

**Globalization** is the process of designing and developing your app in such a way that it functions appropriately in different global markets (on systems with different language and culture configurations) without requiring culture-specific changes or customization.

- Take culture into account when manipulating strings, for example don't change the case of strings before comparing them.
- Use calendars that are appropriate for the current culture.
- Use globalization APIs to display data that are formatted appropriately for the country or region, such as numbers, dates, times, and currencies.
- Take into account that different cultures have different rules for collating (sorting) text and other data.

Your code needs to function equally well in any of the cultures that you've determined that your app will support. Ideally, your code will function equally well in the context of *any* language, region, or culture. The most efficient way to globalize your app's functions is to use the concept of cultures/locales. A culture/locale is a set of rules and a set of data that are specific to a given language and geographic area. These rules and data include information about the following.

- Character classification
- Writing system
- Date and time formatting
- Numeric, currency, weight, and measure conventions
- Sorting rules

>[!NOTE]
> For a list of supported locale names by Windows operating system version, see the Language tag column of the table in [Appendix A: Product Behavior](/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c) in the [Windows Language Code Identifier (LCID) Reference](/openspecs/windows_protocols/ms-lcid/70feba9f-294e-491e-b6eb-56532684c37f).

**Localizability** is the process of preparing a globalized app for localization and/or verifying that the app is ready for localization. Correctly making an app localizable means that the later localization process will not uncover any functional defects in the app. The most essential property of a localizable app is that its executable code has been cleanly separated from the app's localizable resources.

- Strings translated into different languages can vary greatly in length. So, design your UI to accommodate different text lengths and font sizes for labels and text input controls.
- Try to avoid text and/or culturally-sensitive material in images.
- Don't hard-code strings and culture-dependent images in your app's code and markup. Instead, store them as string and image resources so that they can be adapted to different local markets independently of your app's built binaries.
- Pseudo-localize your app to disclose any localizability issues.

**Localization** is the process of adapting or translating your app's localizable resources to meet the language, cultural, and political requirements of the specific local markets that the app is intended to support. By the time you reach the stage of localizing your app, if your app is localizable then you will not have to modify any logic during this process.

- Translate the string resources and other assets of the app for the new market.
- Modify any culture-dependent images as necessary.
- Files can also vary depending on a user's region, separate from their language. For example, a map may have different borders depending on the user's location, but the labels should follow the user's preferred language.

Most localization teams use special tools to aid the process. For example, by recycling translations of recurring text.

| Article | Description |
|---------|-------------|
| [Guidelines for globalization](guidelines-and-checklist-for-globalizing-your-app.md) | Design and develop your app in such a way that it functions appropriately on systems with different language and culture configurations. |
| [Understand user profile languages and app manifest languages](manage-language-and-region.md) | This topic defines the terms "user profile language list", "app manifest language list", and "app runtime language list". We'll be using these terms in this topic and other topics in this feature area, so it's important to know what they mean. |
| [Globalize your date/time/number formats](use-global-ready-formats.md) | Design your app to be global-ready by appropriately formatting dates, times, numbers, phone numbers, and currencies. You'll then be able later to adapt your app for additional cultures, regions, and languages in the global market. |
| [Use templates and patterns to format dates and times](use-patterns-to-format-dates-and-times.md) | Use classes in the [**Windows.Globalization.DateTimeFormatting**](/uwp/api/windows.globalization.datetimeformatting?branch=live) namespace with custom templates and patterns to display dates and times in exactly the format you wish. |
| [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md) | Design your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction. |
| [NumeralSystem values](glob-numeralsystem-values.md) | This topic lists the values available to the **NumeralSystem** property of various classes in the [**Windows.Globalization**](/uwp/api/windows.globalization?branch=live) namespace. |
| [Make your app localizable](prepare-your-app-for-localization.md) | A localized app is one that can be localized to other markets, languages, or regions without uncovering any functional defects in the app. The most essential property of a localizable app is that its executable code has been cleanly separated from its localizable resources. |
| [International fonts](loc-international-fonts.md) | This topic lists the fonts available for Windows apps that are localized into languages other than U.S. English. |
| [Design your app for bidirectional text](design-for-bidi-text.md) | Design your app to provide bi-directional text support (BiDi) so that you can combine script from left-to-right and right-to-left writing systems. |
| [Use the Multilingual App Toolkit 4.0](use-mat.md) | The Multilingual App Toolkit (MAT) 4.0 integrates with Microsoft Visual Studio 2017 and later to provide Windows apps with translation support, translation file management, and editor tools. |
| [Multilingual App Toolkit 4.0 FAQ & troubleshooting](mat-faq-troubleshooting.yml) | This topic provides answers to frequently-asked questions and issues related to the Multilingual App Toolkit (MAT) 4.0. |
| [Use the UTF-8 code page](use-utf8-code-page.md) | UTF-8 is the universal code page for internationalization. |
| [Prepare your application for the Japanese era change](japanese-era-change.md) | Learn about the May 2019 Japanese era change and how to prepare your application. |
