---
author: stevewhims
Description: Follow these best practices when globalizing your apps for a wider audience and when localizing your apps for a specific market.
Search.Refinement.TopicID: 180
title: Guidelines for globalization and localization
ms.assetid: 0342DC3F-DDD1-4DD4-872E-A4EC340CAE79
label: Do's and don'ts
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Globalization and localization do's and don'ts
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Follow these best practices when globalizing your apps for a wider audience and when localizing your apps for a specific market.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813)</li>
<li>[**Globalization.NumberFormatting**](https://msdn.microsoft.com/library/windows/apps/br226136)</li>
<li>[**Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859)</li>
<li>[**Resources**](https://msdn.microsoft.com/library/windows/apps/br206022)</li>
<li>[**Resources.Core**](https://msdn.microsoft.com/library/windows/apps/br225039)</li>
</ul>
</div>



## Globalization

Prepare your app to easily adapt to different markets by choosing globally appropriate terms and images for your UI, using [**Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813) APIs to format app data, and avoiding assumptions based on location or language.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Recommendation</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>Use the correct formats for numbers, dates, times, addresses, and phone numbers.</p></td>
<td align="left"><p>The formatting used for numbers, dates, times, and other forms of data varies between cultures, regions, languages, and markets. If you're displaying numbers, dates, times, or other data, use [<strong>Globalization</strong>](https://msdn.microsoft.com/library/windows/apps/br206813) APIs to get the format appropriate for a particular audience.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Support international paper sizes.</p></td>
<td align="left"><p>The most common paper sizes differ between countries, so if you include features that depend on paper size, like printing, be sure to support and test common international sizes.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Support international units of measurement and currencies.</p></td>
<td align="left"><p>Different units and scales are used in different countries, although the most popular are the metric system and the imperial system. Be sure to support the correct system measurement if you deal with measurements, like length, temperature, or area. Use the [<strong>CurrenciesInUse</strong>](https://docs.microsoft.com/en-us/uwp/api/Windows.Globalization.GeographicRegion#Windows_Globalization_GeographicRegion_CurrenciesInUse) property to get the set of currencies in use in a region.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Display text and fonts correctly.</p></td>
<td align="left"><p>The ideal font, font size, and direction of text varies between different markets.</p>
<p>For more info, see [<strong>Adjust layout and fonts, and support RTL</strong>](adjust-layout-and-fonts--and-support-rtl.md).</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Use Unicode for character encoding.</p></td>
<td align="left"><p>By default, recent versions of Microsoft Visual Studio use Unicode character encoding for all documents. If you're using a different editor, be sure to save source files in the appropriate Unicode character encodings. All Windows Runtime APIs return UTF-16 encoded strings.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Record the language of input.</p></td>
<td align="left"><p>When your app asks users for text input, record the language of input. This ensures that when the input is displayed later it's presented to the user with the appropriate formatting. Use the [<strong>CurrentInputMethodLanguage</strong>](https://msdn.microsoft.com/library/windows/apps/hh700658) property to get the current input language.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Don't use language to assume a user's location, and don't use location to assume a user's language.</p></td>
<td align="left"><p>In Windows, the user's language and location are separate concepts. A user can speak a particular regional variant of a language, like en-gb for English as spoken in Great Britain, but the user can be in an entirely different country or region. Consider whether your apps require knowledge about the user's language, like for UI text, or location, like for licensing issues.</p>
<p>For more info, see [<strong>Manage language and region</strong>](manage-language-and-region.md).</p></td>
</tr>
<tr class="even">
<td align="left"><p>Don't use colloquialisms and metaphors.</p></td>
<td align="left"><p>Language that's specific to a demographic group, such as culture and age, can be hard to understand or translate, because only people in that demographic group use that language. Similarly, metaphors might make sense to one person but mean nothing to someone else. For example, a &quot;bluebird&quot; means something specific to those who are part of skiing culture, but those who aren’t part of that culture don’t understand the reference. If you plan to localize your app and you use an informal voice or tone, be sure that you adequately explain to localizers the meaning and voice to be translated.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Don't use technical jargon, abbreviations, or acronyms.</p></td>
<td align="left"><p>Technical language is less likely to be understood by non-technical audiences or people from other cultures or regions, and it's difficult to translate. People don't use these kinds of words in everyday conversations. Technical language often appears in error messages to identify hardware and software issues. At times, this might be be necessary, but you should rewrite strings to be non-technical.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Don't use images that might be offensive.</p></td>
<td align="left"><p>Images that might be appropriate in your own culture may be offensive or misinterpreted in other cultures. Avoid use of religious symbols, animals, or color combinations that are associated with national flags or political movements.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Avoid political offense in maps or when referring to regions.</p></td>
<td align="left"><p>Maps may include controversial regional or national boundaries, and they're a frequent source of political offense. Be careful that any UI used for selecting a nation refers to it as a &quot;country/region&quot;. Putting a disputed territory in a list labeled &quot;Countries&quot;, like in an address form, could get you in trouble.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Don't use string comparison by itself to compare language tags.</p></td>
<td align="left"><p>BCP-47 language tags are complex. There are a number of issues when comparing language tags, including issues with matching script information, legacy tags, and multiple regional variants. The resource management system in Windows takes care of matching for you. You can specify a set of resources in any languages, and the system chooses the appropriate one for the user and the app.</p>
<p>For more on resource management, see [<strong>Defining app resources</strong>](https://msdn.microsoft.com/library/windows/apps/xaml/hh965321).</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Don't assume that sorting is always alphabetic.</p></td>
<td align="left"><p>For languages that don't use Latin script, sorting is based on things like pronunciation, number of pen strokes, and other factors. Even languages that use Latin script don't always use alphabetic sorting. For example, in some cultures, a phone book might not be sorted alphabetically. The system can handle sorting for you, but if you create your own sorting algorithm, be sure to take into account the sorting methods used in your target markets.</p></td>
</tr>
</tbody>
</table>

 

## Localization

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Recommendation</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>Separate resources such as UI strings and images from code.</p></td>
<td align="left"><p>Design your apps so that resources, like strings and images, are separated from your code. This enables them to be independently maintained, localized, and customized for different scaling factors, accessibility options, and a number of other user and machine contexts.</p>
<p>Separate string resources from your app's code to create a single language-independent codebase. Always separate strings from app code and markup, and place them into a resource file, like a ResW or ResJSON file.</p>
<p>Use the resource infrastructure in Windows to handle the selection of the most appropriate resources to match the user's runtime environment.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Isolate other localizable resource files.</p></td>
<td align="left"><p>Take other files that require localization, like images that contain text to be translated or that need to be changed due to cultural sensitivity, and place them in folders tagged with language names.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Set your default language, and mark all of your resources, even the ones in your default language.</p></td>
<td align="left"><p>Always set the default language for your apps appropriately in the app manifest (package.appxmanifest). The default language determines the language that's used when the user doesn't speak any of the supported languages of the app. Mark default language resources, for example en-us/Logo.png, with their language, so the system can tell which language the resource is in and how it's used in particular situations.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Determine the resources of your app that require localization.</p></td>
<td align="left"><p>What needs to change if your app is to be localized for other markets? Text strings require translation into other languages. Images may need to be adapted for other cultures. Consider how localization affects other resources that your app uses, like audio or video.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Use resource identifiers in the code and markup to refer to resources.</p></td>
<td align="left"><p>Instead of having string literals or specific file names for images in your markup, use references to the resources. Be sure to use unique identifiers for each resource. For more info, see [<strong>How to name resources using qualifiers</strong>](https://msdn.microsoft.com/library/windows/apps/xaml/Hh965324).</p>
<p>Listen for events that fire when the system changes and it begins to use a different set of qualifiers. Reprocess the document so that the correct resources can be loaded.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Enable text size to increase.</p></td>
<td align="left"><p>Allocate text buffers dynamically, since text size may expand when translated. If you must use static buffers, make them extra-large (perhaps doubling the length of the English string) to accommodate potential expansion when strings are translated. There also may be limited space available for a user interface. To accommodate localized languages, ensure that your string length is approximately 40% longer than what you would need for the English language. For really short strings, such as single words, you may needs as much as 300% more space. In addition, enabling multiline support and text-wrapping in a control will leave more space to display each string.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Support mirroring.</p></td>
<td align="left"><p>Text alignment and reading order can be left-to-right, as in English, or right-to-left (RTL), as in Arabic or Hebrew. If you are localizing your product into languages that use a different reading order than your own, be sure that the layout of your UI elements supports mirroring. Even items such as back buttons, UI transition effects, and images may need to be mirrored.</p>
<p>For more info, see [<strong>Adjust layout and fonts, and support RTL</strong>](adjust-layout-and-fonts--and-support-rtl.md).</p></td>
</tr>
<tr class="even">
<td align="left"><p>Comment strings.</p></td>
<td align="left"><p>Ensure that strings are properly commented, and only the strings that need to be translated are provided to localizers. Over-localization is a common source of problems.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Use short strings.</p></td>
<td align="left"><p>Shorter strings are easier to translate and enable translation recycling. Translation recycling saves money because the same string isn't sent to the localizer twice.</p>
<p>Strings longer than 8192 characters may not be supported by some localization tools, so keep string length to 4000 or less.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Provide strings that contain an entire sentence.</p></td>
<td align="left"><p>Provide strings that contain an entire sentence, instead of breaking the sentence into individual words, because the translation of words may depend on their position in a sentence. Also, don't assume that a phrase with multiple parameters will keep those parameters in the same order for every language.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>Optimize image and audio files for localization.</p></td>
<td align="left"><p>Reduce localization costs by avoiding use of text in images or speech in audio files. If you're localizing to a language with a different reading direction than your own, using symmetrical images and effects make it easier to support mirroring.</p></td>
</tr>
<tr class="even">
<td align="left"><p>Don't re-use strings in different contexts.</p></td>
<td align="left"><p>Don't re-use strings in different contexts, because even simple words like &quot;on&quot; and &quot;off&quot; may be translated differently, depending on the context.</p></td>
</tr>
</tbody>
</table>

 

## Related articles


**Samples**
* [Application resources and localization sample](http://go.microsoft.com/fwlink/p/?linkid=254478)
* [Globalization preferences sample](http://go.microsoft.com/fwlink/p/?linkid=231608)
 

 



