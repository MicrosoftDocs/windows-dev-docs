---
author: stevewhims
Description: Control how Windows selects UI resources and formats the UI elements of the app, by using the various language and region settings provided by Windows.
title: Manage language and region
ms.assetid: 22D3A937-736A-4121-8285-A55DED56E594
label: Manage language and region
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Manage language and region

<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Control how Windows selects UI resources and formats the UI elements of the app, by using the various language and region settings provided by Windows.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**Windows.Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813)</li>
<li>[**Windows.ApplicationModel.Resources**](https://msdn.microsoft.com/library/windows/apps/br206022)</li>
<li>[**WinJS.Resources Namespace**](https://msdn.microsoft.com/library/windows/apps/br229779)</li>
</ul>
</div>


## Introduction


For a sample app that demonstrates how to manage language and region settings, see [Application resources and localization sample](http://go.microsoft.com/fwlink/p/?linkid=231501).

A Windows user doesn't need to choose just one language from a limited set of languages. Instead, the user can tell Windows that they speak any language in the world, even if Windows itself isn't translated into that language. The user can even specify that they can speak multiple languages.

A Windows user can specify their location, which can be anywhere in the world. Also, the user can specify that they speak any language in any location. The location and language do not limit each other. Just because the user speaks French doesn't mean they are located in France, or just because the user is in France doesn't mean they prefer to speak French.

A Windows user can run apps in a completely different language than Windows. For example, the user can run an app in Spanish while Windows is running in English.

For Windows Store apps, a language is represented as a [BCP-47 language tag](http://go.microsoft.com/fwlink/p/?linkid=227302). Most APIs in the Windows Runtime, HTML, and XAML can return or accept string representations of these BCP-47 language tags. See also the [IANA list of languages](http://go.microsoft.com/fwlink/p/?linkid=227303).

See [Supported languages](https://msdn.microsoft.com/library/windows/apps/jj657969) for a list of the language tags specifically supported by the Windows Store.

## Tasks


### Users can set their language preferences.

The user language preferences list is an ordered list of languages that describe the user's languages in the order that they prefer them.

The user sets the list in **Settings** &gt; **Time & language** &gt; **Region & language**. Alternatively, they can use **Control Panel** &gt; **Clock, Language, and Region**.

The user's language preferences list can contain multiple languages and regional or otherwise specific variants. For example, the user might prefer fr-CA, but can also understand en-GB.

### Specify the supported languages in the app's manifest.

Specify the list of your app's supported languages in the [**Resources element**](https://msdn.microsoft.com/library/windows/apps/dn934770) of the app's manifest file (typically Package.appxmanifest), or Visual Studio automatically generates the list of languages in the manifest file based on the languages found in the project. The manifest should accurately describe the supported languages at the appropriate level of granularity. The languages listed in the manifest are the languages displayed to users in the Windows Store.

### Specify the default language.

Open package.appxmanifest in Visual Studio, go to the **Application** tab, and set your default language to the language you are using to author your application.

An app uses the default language when it doesn't support any of the languages that the user has chosen. Visual Studio uses the default language to add metadata to assets marked in that language, enabling the appropriate assets to be chosen at runtime.

The default language property must also be set as the first language in the manifest to appropriately set the application language (described in the step "Create the application language list", below). Resources in the default language must still be qualified with their language (for example, en-US/logo.png). The default language does not specify the implicit language of unqualified assets. To learn more, see [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324).

### Qualify resources with their language.

Consider your audience carefully and the language and location of users you want to target. Many people who live in a region don't prefer the primary language of that region. For example, there are millions of households in the United States in which the primary language is Spanish.

When you qualify resources with language:

-   Include script when there is no suppress script value defined for the language. See the [IANA subtag registry](http://go.microsoft.com/fwlink/p/?linkid=227303) for language tag details. For example, use zh-Hant, zh-Hant-TW, or zh-Hans, and not zh-CN or zh-TW.
-   Mark all linguistic content with a language. The default language project property is not the language of unmarked resources (that is, neutral language); it specifies which marked language resource should be chosen if no other marked language resource matches the user.

Mark assets with an accurate representation of the content.

-   Windows does complex matching, including across regional variants (such as en-US to en-GB), so applications are free to mark assets with an accurate representation of the content and let Windows match appropriately for each user.
-   The Windows Store displays what's in the manifest to users looking at the application.
-   Be aware that some tools and other components such as machine translators may find specific language tags, such as regional dialect info, helpful in understanding the data.
-   Be sure to mark assets with full details, especially when multiple variants are available. For example, mark en-GB and en-US if both are specific to that region.
-   For languages that have a single standard dialect, there is no need to add region. General tagging is reasonable in some situations, such as marking assets with ja instead of ja-JP.

Sometimes there are situations where not all resources need to be localized.

-   For resources such as UI strings that come in all languages, mark them with the appropriate language they are in and make sure to have all of these resources in the default language. There is no need to specify a neutral resource (one not marked with a language).
-   For resources that come in a subset of the entire application's set of languages (partial localization), specify the set of the languages the assets do come in and make sure to have all of these resources in the default language. Windows picks the best language possible for the user by looking at all the languages the user speaks in their preference order. For example, not all of an app's UI may be localized into Catalan if the app has a full set of resources in Spanish. For users who speak Catalan and then Spanish, the resources not available in Catalan appear in Spanish.
-   For resources that have specific exceptions in some languages and all other languages map to a common resource, the resource that should be used for all languages should be marked with the undetermined language tag 'und'. Windows interprets the 'und' language tag in a manner similar to '\*', in that it matches the top application language after any other specific match. For example, if a few resources (such as the width of an element) are different for Finnish, but the rest of the resources are the same for all languages, the Finnish resource should be marked with the Finnish language tag, and the rest should be marked with 'und'.
-   For resources that are based on a language's script instead of the language, such as a font or height of text, use the undetermined language tag with a specified script: 'und-&lt;script&gt;'. For example, for Latin fonts use und-Latn\\fonts.css and for Cyrillic fonts use und-Cryl\\fonts.css.

### Create the application language list.

At runtime, the system determines the user language preferences that the app declares support for in its manifest, and creates an *application language list*. It uses this list to determine the language(s) that the application should be in. The list determines the language(s) that is used for app and system resources, dates, times, and numbers, and other components. For example, the Resource Management System ([**Windows.ApplicationModel.Resources**](https://msdn.microsoft.com/library/windows/apps/br206022), [**Windows.ApplicationModel.Resources.Core**](https://msdn.microsoft.com/library/windows/apps/br225039) and [**WinJS.Resources Namespace**](https://msdn.microsoft.com/library/windows/apps/br229779)) loads UI resources according to the application language. [**Windows.Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813) also chooses formats based on the application language list. The application language list is available using [**Windows.Globalization.ApplicationLanguages.Languages**](https://msdn.microsoft.com/library/windows/apps/hh972396).

The matching of languages to resources is difficult. We recommend that you let Windows handle the matching because there are many optional components to a language tag that influence priority of match, and these can be encountered in practice.

Examples of optional components in a language tag are:

-   Script for suppress script languages. For example, en-Latn-US matches en-US.
-   Region. For example, en-US matches en.
-   Variants. For example, de-DE-1996 matches de-DE.
-   -x and other extensions. For example, en-US-x-Pirate matches en-US.

There are also many components to a language tag that are not of the form xx or xx-yy, and not all match.

-   zh-Hant does not match zh-Hans.

Windows prioritizes matching of languages in a standard well-understood way. For example, en-US matches, in priority order, en-US, en, en-GB, and so forth.

-   Windows does cross regional matching. For example, en-US matches en-US, then en, and then en-\*.
-   Windows has additional data for affinity matching within a region, such as the primary region for a language. For example, fr-FR is a better match for fr-BE than is fr-CA.
-   Any future improvements in language matching in Windows will be obtained for free when you depend on Windows APIs.

Matching for the first language in a list occurs before matching of the second language in a list, even for other regional variants. For example, a resource for en-GB is chosen over an fr-CA resource if the application language is en-US. Only if there are no resources for a form of en is a resource for fr-CA chosen.

The application language list is set to the user's regional variant, even if it is different than the regional variant that the app provided. For example, if the user speaks en-GB but the app supports en-US, the application language list would include en-GB. This ensures that dates, times, and numbers are formatted more closely to the user's expectations (en-GB), but the UI resources are still loaded (due to language matching) in the app's supported language (en-US).

The application language list is made up of the following items:

1.  **(Optional) Primary Language Override** The [**PrimaryLanguageOverride**](https://msdn.microsoft.com/library/windows/apps/hh972398) is a simple override setting for apps that give users their own independent language choice, or apps that have some strong reason to override the default language choices. To learn more, see the [Application resources and localization sample](http://go.microsoft.com/fwlink/p/?linkid=231501).
2.  **The user's languages supported by the app.** This is a list of the user's language preferences, in order of language preference. It is filtered by the list of supported languages in the app's manifest. Filtering the user's languages by those supported by the app maintains consistency among software development kits (SDKs), class libraries, dependent framework packages, and the app.
3.  **If 1 and 2 are empty, the default or first language supported by the app.** If the user doesn't speak any languages that the app supports, the chosen application language is the first language supported by the app.

See the Remarks section below for examples.

### Set the HTTP Accept Language header.

HTTP requests made from Windows Store apps and Desktop apps in typical web requests and XMLHttpRequest (XHR), use the standard HTTP Accept-Language header. By default, the HTTP header is set to the user's language preferences, in the user's preferred order, as specified in **Settings** &gt; **Time & language** &gt; **Region & language**. Each language in the list is further expanded to include neutrals of the language and a weighting (q). For example, a user's language list of fr-FR and en-US results in an HTTP Accept-Language header of fr-FR, fr, en-US, en ("fr-FR,fr;q=0.8,en-US;q=0.5,en;q=0.3").

### Use the APIs in the Windows.Globalization namespace.

Typically, the API elements in the [**Windows.Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813) namespace use the application language list to determine the language. If none of the languages has a matching format, the user locale is used. This is the same locale that is used for the system clock. The user locale is available from the **Settings** &gt; **Time & language** &gt; **Region & language** &gt; **Additional date, time, & regional settings** &gt; **Region: Change date, time, or number formats**. The **Windows.Globalization** APIs also accept an override to specify a list of languages to use, instead of the application language list.

[**Windows.Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813) also has a [**Language**](https://msdn.microsoft.com/library/windows/apps/br206804) object that is provided as a helper object. It lets apps inspect details about the language, such as the script of the language, the display name, and the native name.

### Use geographic region when appropriate.

Instead of language, you can use the user's home geographic region setting for choosing what content to display to the user. For example, a news app might default to displaying content from a user's home location, which is set when Windows is installed and is available in the Windows UI under **Region: Change date, time, or number formats** as described in the previous task. You can retrieve the current user's home region setting by using [**Windows.System.UserProfile.GlobalizationPreferences.HomeGeographicRegion**](https://msdn.microsoft.com/library/windows/apps/br241829).

[**Windows.Globalization**](https://msdn.microsoft.com/library/windows/apps/br206813) also has a [**GeographicRegion**](https://msdn.microsoft.com/library/windows/apps/br206795) object that is provided as a helper object. It lets apps inspect details about a particular region, such as its display name, native name, and currencies in use.

## Remarks


The following table contains examples of what the user would see in the app's UI under various language and region settings.

<table border="1">
<colgroup>
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="20%" />
<col width="20%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">App-supported languages (defined in manifest)</th>
<th align="left">User language preferences (set in control panel)</th>
<th align="left">App's primary language override (optional)</th>
<th align="left">App languages</th>
<th align="left">What the user sees in the app</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">English (GB) (default); German (Germany)</td>
<td align="left">English (GB)</td>
<td align="left">none</td>
<td align="left">English (GB)</td>
<td align="left">UI: English (GB)<br>Dates/Times/Numbers: English (GB)</td>
</tr>
<tr>
<td align="left">German (Germany) (default); French (France); Italian (Italy)</td>
<td align="left">French (Austria)</td>
<td align="left">none</td>
<td align="left">French (Austria)</td>
<td align="left">UI: French (France) (fallback from French (Austria))<br>Dates/Times/Numbers: French (Austria)</td>
</tr>
<tr>
<td align="left">English (US) (default); French (France); English (GB)</td>
<td align="left">English (Canada); French (Canada)</td>
<td align="left">none</td>
<td align="left">English (Canada); French (Canada)</td>
<td align="left">UI: English (US) (fallback from English (Canada))<br>Dates/Times/Numbers: English (Canada)</td>
</tr>
<tr>
<td align="left">Spanish (Spain) (default); Spanish (Mexico); Spanish (Latin America); Portuguese (Brazil)</td>
<td align="left">English (US)</td>
<td align="left">none</td>
<td align="left">Spanish (Spain)</td>
<td align="left">UI: Spanish (Spain) (uses default since no fallback available for English)<br>Dates/Times/Numbers Spanish (Spain)</td>
</tr>
<tr>
<td align="left">Catalan (default); Spanish (Spain); French (France)</td>
<td align="left">Catalan; French (France)</td>
<td align="left">none</td>
<td align="left">Catalan; French (France)</td>
<td align="left">UI: Mostly Catalan and some French (France) because not all the strings are in Catalan<br>Dates/Times/Numbers: Catalan</td>
</tr>
<tr>
<td align="left">English (GB) (default); French (France); German (Germany)</td>
<td align="left">German (Germany); English (GB)</td>
<td align="left">English (GB) (chosen by user in app's UI)</td>
<td align="left">English (GB); German (Germany)</td>
<td align="left">UI: English (GB) (language override)<br>Dates/Times/Numbers English (GB)</td>
</tr>
</tbody>
</table>

 

## Related topics


* [BCP-47 language tag](http://go.microsoft.com/fwlink/p/?linkid=227302)
* [IANA list of languages](http://go.microsoft.com/fwlink/p/?linkid=227303)
* [Application resources and localization sample](http://go.microsoft.com/fwlink/p/?linkid=231501)
* [Supported languages](https://msdn.microsoft.com/library/windows/apps/jj657969)
 

 



