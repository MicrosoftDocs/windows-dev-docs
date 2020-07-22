---
Description: Design and develop your app in such a way that it functions appropriately on systems with different language and culture configurations.
Search.Refinement.TopicID: 180
title: Guidelines for globalization
ms.assetid: 0342DC3F-DDD1-4DD4-872E-A4EC340CAE79
template: detail.hbs
ms.date: 11/02/2017
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---
# Guidelines for globalization

Design and develop your app in such a way that it functions appropriately on systems with different language and culture configurations. Use [**Globalization**](/uwp/api/Windows.Globalization?branch=live) APIs to format data; and avoid assumptions in your code about language, region, character classification, writing system, date/time formatting, numbers, currencies, weights, and sorting rules.

| Recommendation | Description |
| ------------- | ----------- |
| Take culture into account when manipulating and comparing strings. | For example, don't change the case of strings before comparing them. See [Recommendations for String Usage](/dotnet/standard/base-types/best-practices-strings?branch=live#recommendations_for_string_usage). |
| When collating (sorting) strings and other data, don't assume that it's is always done alphabetically. | For languages that don't use Latin script, collation is based on factors such as pronunciation, or number of pen strokes. Even languages that do use Latin script don't always use alphabetic sorting. For example, in some cultures, a phone book might not be sorted alphabetically. Windows can handle sorting for you, but if you create your own sorting algorithm then be sure to take into account the sorting methods used in your target markets. |
| Appropriately format numbers, dates, times, addresses, and phone numbers. | These formats vary between cultures, regions, languages, and markets. If you're displaying these data then use [**Globalization**](/uwp/api/Windows.Globalization?branch=live) APIs to get the format appropriate for a particular audience. See [Globalize your date/time/number formats](use-global-ready-formats.md). The order in which family and given names are displayed, and the format of addresses, can differ as well. Use standard date, time, and number displays. Use standard date and time picker controls. Use standard address information. |
| Support international units of measurement and currency. | Different units and scales are used in different countries, although the most popular are the metric system and the imperial system. Be sure to support the correct system measurement if you deal with measurements such as length, temperature, and area. Use the [**GeographicRegion.CurrenciesInUse**](/uwp/api/windows.globalization.geographicregion.CurrenciesInUse) property to get the set of currencies in use in a region. |
| Use Unicode for character encoding. | By default, Microsoft Visual Studio uses Unicode character encoding for all documents. If you're using a different editor, be sure to save source files in the appropriate Unicode character encodings. All Windows Runtime APIs return UTF-16 encoded strings. |
| Support international paper sizes. | The most common paper sizes differ between countries, so if you include features that depend on paper size, such as printing, be sure to support and test common international sizes. |
| Record the language of the keyboard or IME. | When your app asks the user for text input, record the language tag for the currently enabled keyboard layout or Input Method Editor (IME). This ensures that when the input is displayed later it's presented to the user with the appropriate formatting. Use the [**Language.CurrentInputMethodLanguageTag**](/uwp/api/windows.globalization.language.CurrentInputMethodLanguageTag) property to get the current input language. |
| Don't use language to assume a user's region; and don't use region to assume a user's language. | Language and region are separate concepts. A user can speak a particular regional variant of a language, like en-GB for English as spoken in the UK, but the user might be in an entirely different country or region. Consider whether your app requires knowledge about the user's language (for UI text, for example), or region (for licensing, for example). For more info, see [Understand user profile languages and app manifest languages](manage-language-and-region.md). |
| The rules for comparing language tags are non-trivial. | [BCP-47 language tags](https://tools.ietf.org/html/bcp47) are complex. There are a number of issues when comparing language tags, including issues with matching script information, legacy tags, and multiple regional variants. The Resource Management System in Windows takes care of matching for you. You can specify a set of resources in any languages, and the system chooses the appropriate one for the user and the app. See [App resources and the Resource Management System](../../app-resources/index.md) and [How the Resource Management System matches language tags](../../app-resources/how-rms-matches-lang-tags.md). |
| Design your UI to accommodate different text lengths and font sizes for labels and text input controls. | Strings translated into different languages can vary greatly in length, so you'll need your UI controls to dynamically size to their content. Common characters in other languages include marks above or below what is typically used in English (such as Å or Ņ). Use the standard font sizes and line heights to provide adequate vertical space. Be aware that fonts for other languages may require larger minimum font sizes to remain legible. See the classes in the [Windows.Globalization.Fonts](/uwp/api/windows.globalization.fonts?branch=live) namespace. |
| Support the mirroring of reading order. | Text alignment and reading order can be left to right (as in English, for example), or right to left (RTL) (as in Arabic or Hebrew, for example). If you are localizing your product into languages that use a different reading order than your own, then be sure that the layout of your UI elements supports mirroring. Even items such as back buttons, UI transition effects, and images might need to be mirrored. For more info, see [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md). |
| Display text and fonts correctly. | The ideal font, font size, and direction of text varies between different markets. For more info, see [**Adjust layout and fonts, and support RTL**](adjust-layout-and-fonts--and-support-rtl.md) and [International fonts](loc-international-fonts.md). |

## Important APIs
 
* [Globalization](/uwp/api/Windows.Globalization?branch=live)
* [GeographicRegion.CurrenciesInUse](/uwp/api/windows.globalization.geographicregion.CurrenciesInUse)
* [Language.CurrentInputMethodLanguageTag](/uwp/api/windows.globalization.language.CurrentInputMethodLanguageTag)
* [Windows.Globalization.Fonts](/uwp/api/windows.globalization.fonts?branch=live)

## Related topics

* [Recommendations for String Usage](/dotnet/standard/base-types/best-practices-strings?branch=live#recommendations_for_string_usage)
* [Globalize your date/time/number formats](use-global-ready-formats.md)
* [Understand user profile languages and app manifest languages](manage-language-and-region.md)
* [BCP-47 language tags](https://tools.ietf.org/html/bcp47)
* [App resources and the Resource Management System](../../app-resources/index.md)
* [How the Resource Management System matches language tags](../../app-resources/how-rms-matches-lang-tags.md)
* [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md)
* [International fonts](loc-international-fonts.md)
* [Make your app localizable](prepare-your-app-for-localization.md)

## Samples

* [Globalization preferences sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Globalization%20preferences%20sample%20(Windows%208))
