---
author: stevewhims
Description: Follow these best practices when globalizing your apps for a wider audience, and to make your apps localizable for specific markets.
Search.Refinement.TopicID: 180
title: Guidelines for globalization and localizability
ms.assetid: 0342DC3F-DDD1-4DD4-872E-A4EC340CAE79
template: detail.hbs
ms.author: stwhi
ms.date: 11/02/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, globalization, localization
localizationpriority: medium
---
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

# Guidelines for globalization and localizability

Follow these best practices when globalizing your apps for a wider audience, and to make your apps localizable for specific markets.

## Globalization

Design and develop your app in such a way that it functions appropriately on systems with different language and culture configurations. Use [**Globalization**](/uwp/api/Windows.Globalization?branch=live) APIs to format data; and avoid assumptions in your code about language, region, character classification, writing system, date/time formatting, numbers, currencies, weights, and sorting rules.

| Recommendation | Description |
| ------------- | ----------- |
| Take culture into account when manipulating and comparing strings. | For example, don't change the case of strings before comparing them. See [Recommendations for String Usage](/dotnet/standard/base-types/best-practices-strings?branch=live#recommendations_for_string_usage). |
| When collating (sorting) strings and other data, don't assume that it's is always done alphabetically. | For languages that don't use Latin script, collation is based on factors such as pronunciation, or number of pen strokes. Even languages that do use Latin script don't always use alphabetic sorting. For example, in some cultures, a phone book might not be sorted alphabetically. Windows can handle sorting for you, but if you create your own sorting algorithm then be sure to take into account the sorting methods used in your target markets. |
| Appropriately format numbers, dates, times, addresses, and phone numbers. | These formats vary between cultures, regions, languages, and markets. If you're displaying these data then use [**Globalization**](/uwp/api/Windows.Globalization?branch=live) APIs to get the format appropriate for a particular audience. See [Use global-ready formats](use-global-ready-formats.md). |
| Support international units of measurement and currency. | Different units and scales are used in different countries, although the most popular are the metric system and the imperial system. Be sure to support the correct system measurement if you deal with measurements such as length, temperature, and area. Use the [**GeographicRegion.CurrenciesInUse**](/uwp/api/windows.globalization.geographicregion?branch=live#windows_globalization_geographicregion_currenciesinuse) property to get the set of currencies in use in a region. |
| Use Unicode for character encoding. | By default, Microsoft Visual Studio uses Unicode character encoding for all documents. If you're using a different editor, be sure to save source files in the appropriate Unicode character encodings. All UWP APIs return UTF-16 encoded strings. |
| Support international paper sizes. | The most common paper sizes differ between countries, so if you include features that depend on paper size, such as printing, be sure to support and test common international sizes. |
| Record the language of the keyboard or IME. | When your app asks the user for text input, record the language tag for the currently enabled keyboard layout or Input Method Editor (IME). This ensures that when the input is displayed later it's presented to the user with the appropriate formatting. Use the [**Language.CurrentInputMethodLanguageTag**](/uwp/api/windows.globalization.language#windows_globalization_language_currentinputmethodlanguagetag) property to get the current input language. |
| Don't use language to assume a user's region; and don't use region to assume a user's language. | Language and region are separate concepts. A user can speak a particular regional variant of a language, like en-GB for English as spoken in the UK, but the user might be in an entirely different country or region. Consider whether your app requires knowledge about the user's language (for UI text, for example), or region (for licensing, for example). For more info, see [**Manage language and region**](manage-language-and-region.md). |
| The rules for comparing language tags are non-trivial. | [BCP-47](http://go.microsoft.com/fwlink/p/?linkid=227302) language tags are complex. There are a number of issues when comparing language tags, including issues with matching script information, legacy tags, and multiple regional variants. The Resource Management System in Windows takes care of matching for you. You can specify a set of resources in any languages, and the system chooses the appropriate one for the user and the app. See [App resources and the Resource Management System](../app-resources/index.md) and [How the Resource Management System matches language tags](../app-resources/how-rms-matches-lang-tags.md). |
| Support the mirroring of reading order. | Text alignment and reading order can be left to right (as in English, for example), or right to left (RTL) (as in Arabic or Hebrew, for example). If you are localizing your product into languages that use a different reading order than your own, then be sure that the layout of your UI elements supports mirroring. Even items such as back buttons, UI transition effects, and images might need to be mirrored. For more info, see [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md). |

## Localizability

It's important to make your app localizable so that the later localization process won't uncover any functional defects in your app. The most essential property of a localizable app is that its executable code has been cleanly separated from the app's localizable resources.

| Recommendation | Description |
| ------------- | ----------- |
| Determine which of your app's resources need to be localized. | Ask yourself what needs to change if your app is to be localized for other markets. Text strings require translation into other languages. Images might need to be adapted for other cultures. |
| Don't hard-code strings and culture-dependent images in your app's code and markup. | Instead, store them as string and image resources so that they can be adapted to different local markets independently of your app's built binaries. See [Localize strings in your UI and app package manifest](../app-resources/localize-strings-ui-manifest.md). |
| Isolate other localizable resource files. | Take other files that require localization&mdash;such as images that contain text to be translated or that need to be changed due to cultural sensitivity&mdash;and place them in folders tagged with language names. |
| You can minimize localization costs by not putting text nor culturally-sensitive material into images to begin with. | Use globally appropriate terms and images where you can&mdash;an image that's appropriate in your own culture might be offensive or misinterpreted in other cultures. Avoid the use of religious symbols, animals, or color combinations that are associated with national flags or political movements. If you can't avoid this, then your images will need to be thoughtfully localized. |
| Other considerations for images and audio/video files. | You can reduce localization costs by avoiding the use of text in images, or speech in audio/video files. If you're localizing to a language with a different reading direction than your own, using symmetrical images and effects make it easier to support mirroring. |
| Design your UI to accommodate different text lengths and font sizes for labels and text input controls. | Strings translated into different languages can vary greatly in length, so you'll need your UI controls to dynamically size to their content. |
| Pseudo-localize your app to uncover any localizability issues. | Pseudo-localization is a kind of localization dry-run, or disclosure test. You produce a set of resources that are not really translated; they only look that way. Your strings are approximately 40% longer than in the default language, for example, and they have delimiters in them so that you can see at a glance whether they have been truncated in the UI. |
| Avoid political offense in maps or when referring to regions. | Maps might include controversial regional or national boundaries, and they're a frequent source of political offense. Be careful that any UI used for selecting a nation refers to it as a &quot;country/region&quot;. Listing a disputed territory in a list labeled &quot;countries&quot;&mdash;such as in an address form&mdash;might offend some users. |
| Display text and fonts correctly. | The ideal font, font size, and direction of text varies between different markets. For more info, see [**Adjust layout and fonts, and support RTL**](adjust-layout-and-fonts--and-support-rtl.md) and [International fonts](loc-international-fonts.md). |
| Don't use colloquialisms and metaphors in strings in the default language. | Language that's specific to a demographic group, such as culture and age, can be hard to understand or translate, because only people in that demographic group use that language. Similarly, metaphors might make sense to one person but mean nothing to someone else. For example, a &quot;bluebird&quot; means something specific to those who are part of skiing culture, but those who aren’t part of that culture don’t understand the reference. If you plan to localize your app and you use an informal voice or tone, be sure that you adequately explain to localizers the meaning and voice to be translated. |
| Don't use technical jargon, abbreviations, or acronyms. | Technical language is less likely to be understood by non-technical audiences or people from other cultures or regions, and it's difficult to translate. People don't use these kinds of words in everyday conversations. Technical language often appears in error messages to identify hardware and software issues, but you should strings to be technical *only if the user needs that level of information, and can either action it or find someone who can*. |
| Set your default language, and mark all of your resources&mdash;even the ones in your default language. | Set the default language for your app appropriately in your app package manifest (the `Package.appxmanifest` file). The default language determines the language that's used when the user's preferred languages don't match any of the supported languages of your app. Mark default language resources&mdash;for example `\Assets\en-us\Logo.png`&mdash;with their language so that the system can tell which language the resource is in and how it's used in particular situations. |
| Subscribe to events that are raised when the system's language and region settings change. | Do this so that you can re-load resources, if appropriate. For details, see [Updating strings in response to qualifier value change events](../app-resources/localize-strings-ui-manifest.md#updating-strings-in-response-to-qualifier-value-change-events) and [Updating images in response to qualifier value change events](../app-resources/images-tailored-for-scale-theme-contrast.md#updating-images-in-response-to-qualifier-value-change-events). |
| Add comments to your default Resources File (.resw). | Ensure that strings are properly commented, and only the strings that need to be translated are provided to localizers. Over-localization is a common source of problems, not to mention expense. |
| Use appropriately-sized strings. | Short strings are easier to translate, and they enable translation recycling (which saves expense because the same string isn't sent to the localizer more than once). Also, extremely long strings might not be supported by localization tools. In tension with this guideline is the risk of re-using a string in different contexts. Even simple words such as &quot;on&quot; and &quot;off&quot; might be translated differently, depending on the context. So, factor your strings into pieces that work in all contexts. There will be cases where a string will need to be an entire sentence. |

## Important APIs
 
* [Globalization](/uwp/api/Windows.Globalization?branch=live)
* [GeographicRegion.CurrenciesInUse](/uwp/api/windows.globalization.geographicregion?branch=live#windows_globalization_geographicregion_currenciesinuse)
* [Language.CurrentInputMethodLanguageTag](/uwp/api/windows.globalization.language#windows_globalization_language_currentinputmethodlanguagetag)

## Related topics

* [Recommendations for String Usage](/dotnet/standard/base-types/best-practices-strings?branch=live#recommendations_for_string_usage)
* [Use global-ready formats](use-global-ready-formats.md)
* [Manage language and region](manage-language-and-region.md)
* [BCP-47](http://go.microsoft.com/fwlink/p/?linkid=227302)
* [App resources and the Resource Management System](../app-resources/index.md)
* [How the Resource Management System matches language tags](../app-resources/how-rms-matches-lang-tags.md)
* [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md)
* [Localize strings in your UI and app package manifest](../app-resources/localize-strings-ui-manifest.md)
* [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md)
* [International fonts](loc-international-fonts.md)
* [Updating strings in response to qualifier value change events](../app-resources/localize-strings-ui-manifest.md#updating-strings-in-response-to-qualifier-value-change-events)
* [Updating images in response to qualifier value change events](../app-resources/images-tailored-for-scale-theme-contrast.md#updating-images-in-response-to-qualifier-value-change-events)

## Samples

* [Application resources and localization sample](http://go.microsoft.com/fwlink/p/?linkid=254478)
* [Globalization preferences sample](http://go.microsoft.com/fwlink/p/?linkid=231608)