---
description: This topic defines the terms user profile language list, app manifest language list, and app runtime language list. We'll be using these terms in this topic and other topics in this feature area, so it's important to know what they mean.
title: Understand user profile languages and app manifest languages
ms.assetid: 22D3A937-736A-4121-8285-A55DED56E594
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---
# Understand user profile languages and app manifest languages
A Windows user can use **Settings** > **Time & Language** > **Region & language** to configure an ordered list of preferred display languages, or just a single preferred display language. A language can have a regional variant. For example, you can select Spanish as spoken in Spain, Spanish as spoken in Mexico, Spanish as spoken in the United States, among others.

Also in **Settings** > **Time & Language** > **Region & language**, but separate from language, the user can specify their location (known as region) in the world. Note that the display language (and regional variant) setting isn't a determiner of the region setting, and vice versa. For example, a user might be currently living in France but choose a preferred Windows display language of Español (México).

For Windows apps, a language is represented as a [BCP-47 language tag](https://tools.ietf.org/html/bcp47). For example, the BCP-47 language tag "en-US" corresponds to English (United States) in **Settings**. Appropriate Windows Runtime APIs accept and return string representations of BCP-47 language tags.

Also see the [IANA language subtag registry](https://www.iana.org/assignments/language-subtag-registry).

The following three sections define the terms "user profile language list", "app manifest language list", and "app runtime language list". We'll be using these terms in this topic and other topics in this feature area, so it's important to know what they mean.

## User profile language list
The user profile language list is the name of the list that's configured by the user in **Settings** > **Time & Language** > **Region & language** > **Languages**. In code you can use the [**GlobalizationPreferences.Languages**](/uwp/api/windows.system.userprofile.globalizationpreferences.Languages) property to access the user profile language list as a read-only list of strings, where each string is a single [BCP-47 language tag](https://tools.ietf.org/html/bcp47) such as "en-US" or "ja-JP".

```csharp
    IReadOnlyList<string> userLanguages = Windows.System.UserProfile.GlobalizationPreferences.Languages;
```

## App manifest language list
The app manifest language list is the list of languages for which your app declares (or will declare) support. This list grows as you progress your app through the development lifecycle all the way to localization.

The list is determined at compile time, but you have two options for controlling exactly how that happens. One option is to let Visual Studio determine the list from the files in your project. To do that, first set your app's **Default language** on the **Application** tab in your app package manifest source file (`Package.appxmanifest`). Then, confirm that the same file contains this configuration (which it does by default).

```xml
  <Resources>
    <Resource Language="x-generate" />
  </Resources>
```

Each time Visual Studio produces your built app package manifest file (`AppxManifest.xml`), it expands that single `Resource` element in the source file into a union of all the language qualifiers that it finds in your project (see [Tailor your resources for language, scale, high contrast, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast)). For example, if you've begun localizing and you have string, image, and/or file resources whose folder or file names include "en-US", "ja-JP", and "fr-FR", then your built `AppxManifest.xml` file will contain the following (the first entry in the list is the default language that you set).

```xml
  <Resources>
    <Resource Language="EN-US" />
    <Resource Language="JA-JP" />
    <Resource Language="FR-FR" />
  </Resources>
```

The other option is to replace that single "x-generate" `<Resource>` element in your app package manifest source file (`Package.appxmanifest`) with the expanded list of `<Resource>` elements (being careful to list the default language first). That option involves more maintenance work for you, but it might be an appropriate option for you if you use a custom build system.

To begin with, your app manifest language list will only contain one language. Perhaps that's en-US. But eventually&mdash;as you either manually configure your manifest, or as you add translated resources to your project&mdash;that list will grow.

When your app is in the Microsoft Store, the languages in the app manifest language list are the ones that are displayed to customers. For a list of BCP-47 language tags specifically supported by the Microsoft Store, see [Supported languages](../../publish/publish-your-app/msix/app-package-requirements.md#supported-languages).

In code you can use the [**ApplicationLanguages.ManifestLanguages**](/uwp/api/windows.globalization.applicationlanguages.ManifestLanguages) property to access the app manifest language list as a read-only list of strings, where each string is a single BCP-47 language tag.

```csharp
    IReadOnlyList<string> userLanguages = Windows.Globalization.ApplicationLanguages.ManifestLanguages;
```

## App runtime language list
The third language list of interest is the intersection between the two lists that we've just described. At runtime, the list of languages for which your app has declared support (the app manifest language list) is compared with the list of languages for which the user has declared a preference (the user profile language list). The app runtime language list is set to this intersection (if the intersection is not empty), or to just the app's default language (if the intersection is empty).

More specifically, the app runtime language list is made up of these items.

1.  **(Optional) Primary Language Override**. The [**PrimaryLanguageOverride**](/uwp/api/Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride) is a simple override setting for apps that give users their own independent language choice, or apps that have some strong reason to override the default language choices. To learn more, see the [Application resources and localization sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Application%20resources%20and%20localization%20sample%20(Windows%208)).
2.  **The user's languages that are supported by the app**. This is the user profile language list filtered by the app manifest language list. Filtering the user's languages by those supported by the app maintains consistency among software development kits (SDKs), class libraries, dependent framework packages, and the app.
3.  **If 1 and 2 are empty, then the default or first language supported by the app**. If the user profile language list doesn't contain any languages that the app supports, then the app runtime language is the first language supported by the app.

In code you can use the [ResourceContext.QualifierValues](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues) property to access the app runtime language list in the form of a string containing a semicolon-delimited list of BCP-47 language tags.

```csharp
    string runtimeLanguages = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues["Language"];
```

You can also access it as a read-only list of strings, each containing a single BCP-47 language tag. You can use the [**ResourceContext.Languages**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.Languages) property or the [**ApplicationLanguages.Languages**](/uwp/api/windows.globalization.applicationlanguages.Languages) property to do this.

```csharp
    IReadOnlyList<string> runtimeLanguages = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Languages;

    runtimeLanguages = Windows.Globalization.ApplicationLanguages.Languages;
```

The app runtime language list determines the resources that Windows loads for your app and also the language(s) used to format dates, times, numbers, and other components. See [Globalize your date/time/number formats](use-global-ready-formats.md).

**Note** If the user profile language and the app manifest language are regional variants of one another, then the user's regional variant is used as the app runtime language. For example, if the user prefers en-GB and the app supports en-US, then the app runtime language is en-GB. This ensures that dates, times, and numbers are formatted more closely to the user's expectations (en-GB), but localized resources are still loaded (due to language matching) in the app's supported language (en-US).

## Qualify resource files with their language
Name your resource files, or their folders, with language resource qualifiers. To learn more about resource qualifiers, see [Tailor your resources for language, scale, high contrast, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast). A resource file can be an image (or other asset), or it can be a resource container file, such as a *.resw* that contains text strings.

**Note** Even resources in your app's default language must specify the language qualifier. For example, if your app's default language is English (United States), then qualify your assets as `\Assets\Images\en-US\logo.png`.

- Windows performs complex matching, including across regional variants such as en-US and en-GB. So include the region sub-tag as appropriate. See [How the Resource Management System matches language tags](/windows/uwp/app-resources/how-rms-matches-lang-tags).
- Specify a language script sub-tag in the qualifier when there is no Suppress-Script value defined for the language. For example, instead of zh-CN or zh-TW, use zh-Hant, zh-Hant-TW, or zh-Hans (for more detail, see the [IANA language subtag registry](https://www.iana.org/assignments/language-subtag-registry)).
- For languages that have a single standard dialect, there is no need to include the region qualifier. For example, use ja instead of ja-JP.
- Some tools and other components such as machine translators might find specific language tags, such as regional dialect info, helpful in understanding the data.

### Not all resources need to be localized

Localization might not be required for all resources.

- At a minimum, ensure all resources exist in the default language.
- A subset of some resources might suffice for a closely related language (partial localization). For example, you might not localize all of your app's UI into Catalan if your app has a full set of resources in Spanish. For users who speak Catalan and then Spanish, the resources that are not available in Catalan appear in Spanish.
- Some resources might require exceptions for specific languages, while the majority of other resources map to a common resource. In this case, mark the resource intended to be used for all languages with the undetermined language tag 'und'. Windows interprets the 'und' language tag as a wildcard (similar to '\*') in that it matches the top app language after any other specific match. For example, if a few resources are different for Finnish, but the rest of the resources are the same for all languages, then the Finnish resource should be marked with the Finnish language tag, and the rest should be marked with 'und'.
- For resources that are based on a language script, such as a font or height of text, use the undetermined language tag with a specified script: 'und-&lt;script&gt;'. For example, for Latin fonts use `und-Latn\\fonts.css` and for Cyrillic fonts use `und-Cryl\\fonts.css`.

## Set the HTTP Accept-Language request header
Consider whether the web services that you call have the same extent of localization as your app does. HTTP requests made from Windows apps in typical web requests, and XMLHttpRequest (XHR), use the standard HTTP Accept-Language request header. By default, the HTTP header is set to the user profile language list. Each language in the list is further expanded to include neutrals of the language and a weighting (q). For example, a user's language list of fr-FR and en-US results in an HTTP Accept-Language request header of fr-FR, fr, en-US, en ("fr-FR,fr;q=0.8,en-US;q=0.5,en;q=0.3"). But if your weather app (for example) is displaying a UI in French (France), but the user's top language in their preference list is German, then you'll need to explicitly request French (France) from the service in order to remain consistent within your app.

## APIs in the Windows.Globalization namespace
Typically, the APIs in the [**Windows.Globalization**](/uwp/api/windows.globalization?branch=live) namespace use the app runtime language list to determine the language. If none of the languages has a matching format, then the user locale is used. This is the same locale that is used for the system clock. The user locale is available from **Settings** > **Time & Language** > **Region & language** > **Additional date, time, & regional settings** > **Region: Change date, time, or number formats**. The **Windows.Globalization** APIs also have overrides to specify a list of languages to use, instead of the app runtime language list.

Using the [**Language**](/uwp/api/windows.globalization.language?branch=live) class, you can inspect details about a particular language, such as the script of the language, the display name, and the native name.

## Use geographic region when appropriate
In **Settings** > **Time & Language** > **Region & language** > **Country or region**, the user can specify their location in the world. You can use this settings, instead of language, for choosing what content to display to the user. For example, a news app might default to displaying content from this region.

In code, you can access this setting by using the [**GlobalizationPreferences.HomeGeographicRegion**](/uwp/api/windows.system.userprofile.globalizationpreferences.HomeGeographicRegion) property.

Using the [**GeographicRegion**](/uwp/api/windows.globalization.geographicregion?branch=live) class, you can inspect details about a particular region, such as its display name, native name, and currencies in use.

## Examples
The following table contains examples of what the user would see in your app's UI under various language and region settings.

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
<th align="left">App manifest language list</th>
<th align="left">User profile language list</th>
<th align="left">App's primary language override (optional)</th>
<th align="left">App runtime language list</th>
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

>[!NOTE]
> For a list of standard country/region codes used by Microsoft, see the [Official Country/Region List](../../publish/publish-your-app/msix/app-package-requirements.md#supported-languages).

## Important APIs
* [GlobalizationPreferences.Languages](/uwp/api/windows.system.userprofile.globalizationpreferences.Languages)
* [ApplicationLanguages.ManifestLanguages](/uwp/api/windows.globalization.applicationlanguages.ManifestLanguages)
* [PrimaryLanguageOverride](/uwp/api/Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride)
* [ResourceContext.QualifierValues](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues)
* [ResourceContext.Languages](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.Languages)
* [ApplicationLanguages.Languages](/uwp/api/windows.globalization.applicationlanguages.Languages)
* [Windows.Globalization](/uwp/api/windows.globalization?branch=live)
* [Language](/uwp/api/windows.globalization.language?branch=live)
* [GlobalizationPreferences.HomeGeographicRegion](/uwp/api/windows.system.userprofile.globalizationpreferences.HomeGeographicRegion)
* [GeographicRegion](/uwp/api/windows.globalization.geographicregion?branch=live)

## Related topics
* [BCP-47 language tag](https://tools.ietf.org/html/bcp47)
* [IANA language subtag registry](https://www.iana.org/assignments/language-subtag-registry)
* [Tailor your resources for language, scale, high contrast, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast)
* [Supported languages](../../publish/publish-your-app/msix/app-package-requirements.md#supported-languages)
* [Globalize your date/time/number formats](use-global-ready-formats.md)
* [How the Resource Management System matches language tags](/windows/uwp/app-resources/how-rms-matches-lang-tags)

## Samples
* [Application resources and localization sample](https://code.msdn.microsoft.com/windowsapps/Application-resources-and-cd0c6eaa)
