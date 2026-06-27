---
description: Learn how to design and develop your Windows app for global audiences using globalization and localization techniques.
title: Globalization and localization
ms.assetid: c0791eec-5bb8-4a13-8977-61d7d98e35ce
template: detail.hbs
ms.date: 03/25/2026
ms.topic: article
keywords: winui, globalization, localizability, localization, internationalization, rtl, bidi
ms.localizationpriority: medium
---
# Globalization and localization

Windows is used worldwide by audiences that are diverse in terms of language, region, and culture. By designing your app with globalization and localization in mind, you can reach a wider market and provide a better experience for all your users.

**Globalization** is the process of designing and developing your app so that it functions correctly across different languages, regions, and cultures — without requiring culture-specific code changes. This includes using culture-aware APIs for formatting dates, times, numbers, and currencies, as well as handling different sorting rules and writing systems.

**Localizability** is the process of preparing your app so that it can be localized without introducing functional defects. The key principle is cleanly separating executable code from localizable resources such as strings and images.

**Localization** is the process of adapting your app's resources — translating strings, adjusting images, and tailoring content — to meet the requirements of specific markets, languages, and regions.

## Guidelines for globalization

Design and develop your app in such a way that it functions appropriately on systems with different language and culture configurations. Use [**Globalization**](/uwp/api/Windows.Globalization?branch=live) APIs to format data; and avoid assumptions in your code about language, region, character classification, writing system, date/time formatting, numbers, currencies, weights, and sorting rules.

| Recommendation | Description |
| ------------- | ----------- |
| Take culture into account when manipulating and comparing strings. | For example, don't change the case of strings before comparing them. See [Recommendations for String Usage](/dotnet/standard/base-types/best-practices-strings?branch=live#recommendations_for_string_usage). |
| When collating (sorting) strings and other data, don't assume that it's always done alphabetically. | For languages that don't use Latin script, collation is based on factors such as pronunciation, or number of pen strokes. Even languages that do use Latin script don't always use alphabetic sorting. For example, in some cultures, a phone book might not be sorted alphabetically. Windows can handle sorting for you, but if you create your own sorting algorithm then be sure to take into account the sorting methods used in your target markets. |
| Appropriately format numbers, dates, times, addresses, and phone numbers. | These formats vary between cultures, regions, languages, and markets. If you're displaying these data then use [**Globalization**](/uwp/api/Windows.Globalization?branch=live) APIs to get the format appropriate for a particular audience. See [Globalize your date/time/number formats](use-global-ready-formats.md). The order in which family and given names are displayed, and the format of addresses, can differ as well. Use standard date, time, and number displays. Use standard date and time picker controls. Use standard address information. |
| Support international units of measurement and currency. | Different units and scales are used in different countries, although the most popular are the metric system and the imperial system. Be sure to support the correct system measurement if you deal with measurements such as length, temperature, and area. Use the [**GeographicRegion.CurrenciesInUse**](/uwp/api/windows.globalization.geographicregion.CurrenciesInUse) property to get the set of currencies in use in a region. |
| Use Unicode for character encoding. | By default, Microsoft Visual Studio uses Unicode character encoding for all documents. If you're using a different editor, be sure to save source files in the appropriate Unicode character encodings. All Windows Runtime APIs return UTF-16 encoded strings. |
| Support international paper sizes. | The most common paper sizes differ between countries/regions, so if you include features that depend on paper size, such as printing, be sure to support and test common international sizes. |
| Record the language of the keyboard or IME. | When your app asks the user for text input, record the language tag for the currently enabled keyboard layout or Input Method Editor (IME). This ensures that when the input is displayed later it's presented to the user with the appropriate formatting. Use the [**Language.CurrentInputMethodLanguageTag**](/uwp/api/windows.globalization.language.CurrentInputMethodLanguageTag) property to get the current input language. |
| Don't use language to assume a user's region; and don't use region to assume a user's language. | Language and region are separate concepts. A user can speak a particular regional variant of a language, like en-GB for English as spoken in the UK, but the user might be in an entirely different country or region. Consider whether your app requires knowledge about the user's language (for UI text, for example), or region (for licensing, for example). For more info, see [Understand user profile languages and app manifest languages](manage-language-and-region.md). |
| The rules for comparing language tags are non-trivial. | [BCP-47 language tags](https://tools.ietf.org/html/bcp47) are complex. There are a number of issues when comparing language tags, including issues with matching script information, legacy tags, and multiple regional variants. The Resource Management System in Windows takes care of matching for you. You can specify a set of resources in any languages, and the system chooses the appropriate one for the user and the app. See [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview) and [How the Resource Management System matches language tags](/windows/uwp/app-resources/how-rms-matches-lang-tags). |
| Design your UI to accommodate different text lengths and font sizes for labels and text input controls. | Strings translated into different languages can vary greatly in length, so you'll need your UI controls to dynamically size to their content. Common characters in other languages include marks above or below what is typically used in English (such as Å or Ņ). Use the standard font sizes and line heights to provide adequate vertical space. Be aware that fonts for other languages may require larger minimum font sizes to remain legible. See the classes in the [Windows.Globalization.Fonts](/uwp/api/windows.globalization.fonts?branch=live) namespace. |
| Support the mirroring of reading order. | Text alignment and reading order can be left to right (as in English, for example), or right to left (RTL) (as in Arabic or Hebrew, for example). If you are localizing your product into languages that use a different reading order than your own, then be sure that the layout of your UI elements supports mirroring. Even items such as back buttons, UI transition effects, and images might need to be mirrored. For more info, see [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md). |
| Display text and fonts correctly. | The ideal font, font size, and direction of text varies between different markets. For more info, see [**Adjust layout and fonts, and support RTL**](adjust-layout-and-fonts--and-support-rtl.md) and [International fonts](loc-international-fonts.md). |

## Implementation guides

### Getting started

| Article | Description |
|---------|-------------|
| [Understand user profile and app manifest languages](manage-language-and-region.md) | Learn about user profile language lists, app manifest language lists, and app runtime language lists, and how they affect your app's behavior. |
| [Make your app localizable](prepare-your-app-for-localization.md) | Prepare your app for localization by separating executable code from localizable resources. |
| [Design your app for bidirectional text](design-for-bidi-text.md) | Design guidance for bidirectional text support (BiDi) so that you can combine script from left-to-right and right-to-left writing systems. |

### Formatting

| Article | Description |
|---------|-------------|
| [Globalize your date/time/number formats](use-global-ready-formats.md) | Use globalization APIs to format dates, times, numbers, phone numbers, and currencies appropriately for different cultures. Includes advanced guidance on using templates and patterns for custom date/time formatting. |
| [NumeralSystem values](glob-numeralsystem-values.md) | Reference for the **NumeralSystem** property values available in the [**Windows.Globalization**](/uwp/api/windows.globalization) namespace. |

### Layout and fonts

| Article | Description |
|---------|-------------|
| [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md) | Design your app to support the layouts and fonts of multiple languages, including RTL flow direction. |
| [International fonts](loc-international-fonts.md) | Fonts available for Windows apps localized into languages other than U.S. English. |

### App resources

| Article | Description |
|---------|-------------|
| [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview) | Use the MRT Core resource management system in the Windows App SDK. |
| [Tailor your resources for language, scale, contrast, and others](/windows/apps/windows-app-sdk/mrtcore/tailor-resources-lang-scale-contrast) | Tailor your app resources for specific language, scale, and contrast settings. |
| [Localize strings in your UI and app package manifest](/windows/apps/windows-app-sdk/mrtcore/localize-strings) | Localize string resources in your UI and app package manifest. Includes WinUI 3 packaged app setup. |
| [Load images and assets tailored for scale, theme, contrast, and others](/windows/apps/windows-app-sdk/mrtcore/images-tailored-for-scale-theme-contrast) | Load image resources tailored for display scale, theme, high contrast, and other contexts. |

### Additional topics

| Article | Description |
|---------|-------------|
| [Use UTF-8 code pages in Windows apps](use-utf8-code-page.md) | UTF-8 is the universal code page for internationalization. Learn how to use it in your Windows app. |
| [Prepare your application for the Japanese era change](japanese-era-change.md) | Learn about the May 2019 Japanese era change and how to prepare your application. |
| [Multilingual App Toolkit (MAT)](mat-announcements.md) | The Multilingual App Toolkit (MAT) reached end-of-support on October 15, 2025. |

## Related resources

- [Globalization preferences sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Globalization%20preferences%20sample%20(Windows%208))
- For a list of supported locale names by Windows operating system version, see [Appendix A: Product Behavior](/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c) in the [Windows Language Code Identifier (LCID) Reference](/openspecs/windows_protocols/ms-lcid/70feba9f-294e-491e-b6eb-56532684c37f).
