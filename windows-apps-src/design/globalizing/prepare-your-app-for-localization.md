---
Description: A localized app is one that can be localized to other markets, languages, or regions without uncovering any functional defects in the app. The most essential property of a localizable app is that its executable code has been cleanly separated from its localizable resources.
title: Make your app localizable
ms.assetid: 06E1D4BB-59EA-4D71-99AC-7CB93D2A58A7
template: detail.hbs
ms.date: 11/07/2017
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---

# Make your app localizable

A localized app is one that can be localized for other markets, languages, or regions without uncovering any functional defects in the app. The most essential property of a localizable app is that its executable code has been cleanly separated from its localizable resources. So, you should determine which of your app's resources need to be localized. Ask yourself what needs to change if your app is to be localized for other markets.

We also recommend that you become familiar with the [guidelines for globalization](guidelines-and-checklist-for-globalizing-your-app.md).

## Put your strings into Resources Files (.resw)

Don't hard-code string literals in your imperative code, XAML markup, nor in your app package manifest. Instead, put your strings into Resources Files (.resw) so that they can be adapted to different local markets independently of your app's built binaries. For details, see [Localize strings in your UI and app package manifest](../../app-resources/localize-strings-ui-manifest.md).

That topic also shows you how to add comments to your default Resources File (.resw). For example, if you are adopting an informal voice or tone then be sure to explain that in comments. Also, to minimize expense, confirm that only the strings that need to be translated are provided to translators.

Set the default language for your app appropriately in your app package manifest source file (the `Package.appxmanifest` file). The default language determines the language that's used when the user's preferred languages don't match any of the supported languages of your app. Mark all of your resources with their language (even the ones in your default language, for example `\Assets\en-us\Logo.png`) so that the system can tell which language the resource is in and how it's used in particular situations.

## Tailor your images and other file resources for language

Ideally, you will be able to globalize your images&mdash;that is, make them culture-independent. For any images and other file resources where that's not possible, create as many different variants of them as you need and put the appropriate language qualifiers into their file or folder names. To learn more, see [Tailor your resources for language, scale, high contrast, and other qualifiers](../../app-resources/tailor-resources-lang-scale-contrast.md).

To minimize localization costs, don't put text nor culturally-sensitive material into images to begin with. An image that's appropriate in your own culture might be offensive or misinterpreted in other cultures. Avoid the use of culture-specific images such as mailboxes, which are not common around the world. Avoid religious symbols, animals, political, or gender-specific images. The display of flesh, body parts, or hand gestures can also be a sensitive topic. If you can't avoid all of these, then your images will need to be thoughtfully localized. If you're localizing to a language with a different reading direction than your own, using symmetrical images and effects make it easier to support mirroring.

Also avoid the use of text in images, and speech in audio/video files.

## The use of color in your app

Be mindful when using color. Using color combinations that are associated with national flags or political movements can be problematic. Color choices may need to be reviewed by culture experts. There is also an accessibility issues with using color. If you use color to convey meaning then you should also convey that same information by some other means, such as size, shape, or a label.

## Consider factoring your strings into sentences

Use appropriately-sized strings. Short strings are easier to translate, and they enable translation recycling (which saves expense because the same string isn't sent to the localizer more than once). Also, extremely long strings might not be supported by localization tools.

But in tension with this guideline is the risk of re-using a string in different contexts. Even simple words such as &quot;on&quot; and &quot;off&quot; might be translated differently, depending on the context. In the English language, "on" and "off" can be used for a toggle for Flight Mode, Bluetooth, and devices. But in Italian, the translation depends on the context of what is being turned on and off. You would need to create a pair of strings for each context. You can reuse strings if the two contexts are the same. For instance, you can reuse the string "Volume" for both sound effect volume and music volume because both refer to intensity of sound. You should not reuse that same string when referring to a hard disk volume because the context and meaning are different, and the word might be translated differently.

Additionally, a string like "text" or "fax" could be used as both a verb and a noun in the English language, which can confuse the translation process. Instead, create a separate string for both the verb and noun format. When you're not sure whether the contexts are the same, err on the safe side and use a distinct string.

In short, factor your strings into pieces that work in all contexts. There will be cases where a string will need to be an entire sentence.

Consider the following string: "The {0} could not be synchronized."

A variety of words could replace {0}, such as "appointment", "task", or "document". While this example works for the English language, it will not work in all cases for the corresponding sentence in, for example, German. Notice that in the following German sentences, some of the words in the template string ("Der", "Die", "Das") need to match the parameterized word:

| English                                    | German                                           |
|:------------------------------------------ |:------------------------------------------------ |
| The appointment could not be synchronized. | Der Termin konnte nicht synchronisiert werden.   |
| The task could not be synchronized.        | Die Aufgabe konnte nicht synchronisiert werden.  |
| The document could not be synchronized.    | Das Dokument konnte nicht synchronisiert werden. |

As another example, consider the sentence "Remind me in {0} minute(s)." Using "minute(s)" works for the English language, but other languages might use different terms. For example, the Polish language uses "minuta", "minuty", or "minut" depending on the context.

To solve this problem, localize the entire sentence, rather than a single word. Doing this may seem like extra work and an inelegant solution, but it is the best solution because:

- A grammatically correct message will be displayed for all languages.
- Your translator will not need to ask about what the strings will be replaced with.
- You will not need to implement a costly code fix when a problem like this surfaces after your app is completed.

## Other considerations for strings

Avoid colloquialisms and metaphors in the strings that you author in your default language. Language that's specific to a demographic group, such as culture and age, can be hard to understand or translate because only people in that demographic group use that language. Similarly, metaphors might make sense to one person but mean nothing to someone else. For example, a &quot;bluebird&quot; means something specific to those who are part of skiing culture, but those who aren’t part of that culture don’t understand the reference.

Don't use technical jargon, abbreviations, or acronyms. Technical language is less likely to be understood by non-technical audiences or people from other cultures or regions, and it's difficult to translate. People don't use these kinds of words in everyday conversations. Technical language often appears in error messages to identify hardware and software issues, but you should strings to be technical *only if the user needs that level of information, and can either action it or find someone who can*.

Using an informal voice or tone in your strings is a valid choice. You can use comments in your default Resources File (.resw) to indicate that intention.

## Pseudo-localization

Pseudo-localize your app to uncover any localizability issues. Pseudo-localization is a kind of localization dry-run, or disclosure test. You produce a set of resources that are not really translated; they only look that way. Your strings are approximately 40% longer than in the default language, for example, and they have delimiters in them so that you can see at a glance whether they have been truncated in the UI.

## Deployment Considerations

When you install an app that contains localized language data, you might find that only the default language is available for the app even though you initially included resources for multiple languages. This is because the installation process is optimized to only install language resources that match the current language and culture of the device. Therefore, if your device is configured for en-US, only the en-US language resources are installed with your app.

> [!NOTE]
> It is not possible to install additional language support for your app after the initial installation. If you change the default language after installing an app, the app continues to use only the original language resources.

If you want to ensure all language resources are available after installation, create a configuration file for the app package that specifies that certain resources are required during installation (including language resources). This optimized installation feature is automatically enabled when your application's .appxbundle is generated during packaging. For more information, see [Ensure that resources are installed on a device regardless of whether a device requires them](/previous-versions/dn482043(v=vs.140)).

Optionally, to ensure all resources are installed (not just a subset), you can disable .appxbundle generation when you package your app. This is not recommended however as it can increase the installation time of your app.

Disable automatic generation of the .appxbundle by setting the "Generate App Bundle" attribute to “never”:

1. In Visual Studio, right-click the project name
2. Select **Store** -> **Create app packages...**
3. In the **Create Your Packages** dialog, select **I want to create packages to upload to the Microsoft Store using a new app name** and then click **Next**.
4. In the **Select an app name** dialog, select/create an app name for your package.
5. In the **Select and Configure Packages** dialog, set **Generate app bundle** to **Never**.

## Geopolitical awareness

Avoid political offense in maps or when referring to regions. Maps might include controversial regional or national boundaries, and they're a frequent source of political offense. Be careful that any UI used for selecting a nation refers to it as a &quot;country/region&quot;. Listing a disputed territory in a list labeled &quot;countries&quot;&mdash;such as in an address form&mdash;might offend some users.

## Language- and region-changed events

Subscribe to events that are raised when the system's language and region settings change. Do this so that you can re-load resources, if appropriate. For details, see [Updating strings in response to qualifier value change events](../../app-resources/localize-strings-ui-manifest.md#updating-strings-in-response-to-qualifier-value-change-events) and [Updating images in response to qualifier value change events](../../app-resources/images-tailored-for-scale-theme-contrast.md#updating-images-in-response-to-qualifier-value-change-events).

## Ensure the correct parameter order when formatting strings

Don't assume that all languages express parameters in the same order. For example, consider this format.

```csharp
    string.Format("Every {0} {1}", monthName, dayNumber); // For example, "Every April 1".
```

The format string in this example works for English (United States). But it is not appropriate for German (Germany), for example, where the day and month are displayed in the reverse order. Ensure that the translator knows the intent of each of the parameters so that they can reverse the order of the format items in the format string (for example, "{1} {0}") as appropriate for the target language.

## Don’t over-localize

Only submit natural language to translators; not programming language nor markup. A `<link>` tag is not natural language. Consider these examples.

| Don't localize this                   | Localize this |
|:--------------------------------------- |:-------------------------- |
| &lt;link&gt;terms of use&lt;/link&gt;   | terms of use               |
| &lt;link&gt;privacy policy&lt;/link&gt; | privacy policy             |

Including the `<link>` tag in your Resources File (.resw) means that it, too, is likely to be translated. That would render the tag invalid. If you have long strings that need to include markup in order to maintain context and ensure ordering, then make it clear in comments what not to translate.

## Choose an appropriate translation approach

After strings are separated into resource files, they can be translated. The ideal time to translate strings is after the strings in your project are finalized, which usually happens toward the end of a project. You can approach the translation process in number of ways. This may depend on the volume of strings to be translated, the number of languages to be translated, and how the translation will be done (such as in-house versus hiring an external vendor).

Consider these options.

- **The resource files can be translated by opening them directly in the project.** This approach works well for a project that has a small volume of strings that need to be translated into two or three languages. It could be suitable for a scenario where a developer speaks more than one language and is willing to handle the translation process. This approach benefits from being quick, requires no tools, and minimizes the risk of mistranslations. But it is not scalable. In particular, the resources in different languages can easily get out of sync, causing bad user experiences and maintenance headaches.
- **The string resource files are in XML or ResJSON text format, so could be handed off for translation using any text editor. The translated files would then be copied back into the project.** This approach carries a risk of translators accidentally editing the XML tags, but it lets translation work take place outside of the Microsoft Visual Studio project. This approach could work well for projects that need to be translated into a small number of languages. The XLIFF format is an XML format specifically designed for use in localization, and should be well supported by some localization vendors or localization tools. You can use the [Multilingual App Toolkit](/previous-versions/windows/apps/jj572370(v=win.10)) to generate XLIFF files from other resource files, such as .resw or .resjson.

> [!NOTE]
> Localization might also be necessary for other assets, including images and audio files.

You should also consider the following:

- **Localization tools** A number of localization tools are available for parsing resource files and allowing only the translatable strings to be edited by translators. This approach reduces the risk of a translator accidentally editing the XML tags. But it has the drawback of introducing a new tool and process to the localization process. A localization tool is good for projects with a large volume of strings but a small number of languages. To learn more, see [How to use the Multilingual App Toolkit](/previous-versions/windows/apps/jj572370(v=win.10)).
- **Localization vendors** Consider using a localization vendor if your application contains extensive strings that need to be translated into a large number of languages. A localization vendor can give advice about tools and processes, as well as translating your resource files. This is an ideal solution, but is also the most costly option, and may increase the turnaround time for your translated content.

## Keep access keys and labels consistent

It is a challenge to "synchronize" the access keys used in accessibility with the display of the localized access keys, because the two string resources are categorized in two separate sections. Be sure to provide comments for the label string such as: `Make sure that the emphasized shortcut key  is synchronized with the access key.`

## Support furigana for Japanese strings that can be sorted

Japanese kanji characters have the property of having more than one reading (pronunciation) depending on the word in which they are used. This leads to problems when you try to sort Japanese named objects, such as application names, files, songs, and so on. Japanese kanji have, in the past, usually been sorted in a machine-understandable order called XJIS. Unfortunately, because this sorting order is not phonetic it is not very useful for humans.

*Furigana* works around this problem by allowing the user or creator to specify the phonetics for the characters they are using. If you use the following procedure to add furigana to your app name, you can ensure that it is sorted in the proper location in the app list. If your app name contains kanji characters and furigana is not provided when the user’s UI language or the sort order is set to Japanese, Windows makes its best effort to generate the appropriate pronunciation. However, there is a possibility for app names containing rare or unique readings to be sorted under a more common reading instead. Therefore, the best practice for Japanese applications (especially those containing kanji characters in their names) is to provide a furigana version of their app name as part of the Japanese localization process.

1. Add "ms-resource:Appname" as the Package Display Name and the Application Display Name.
2. Create a ja-JP folder under strings, and add two resource files as follows:

    ``` syntax
    strings\
        en-us\
        ja-jp\
            Resources.altform-msft-phonetic.resw
            Resources.resw
    ```

3. In Resources.resw for general ja-JP: Add a string resource for Appname "希蒼"
4. In Resources.altform-msft-phonetic.resw for Japanese furigana resources: Add furigana value for AppName "のあ"

The user can search for the app name "希蒼" using both the furigana value "のあ" (noa), and the phonetic value (using the **GetPhonetic** function from the Input Method Editor (IME)) "まれあお" (mare-ao).

Sorting follows the **Regional Control Panel** format:

- Under a Japanese user locale,
  - If furigana is enabled, then "希蒼" is sorted under "の".
  - If furigana is missing, then "希蒼" is sorted under "ま".
- Under a non-Japanese user locale,
  - If furigana is enabled, then "希蒼" is sorted under "の".
  - If furigana is missing, then "希蒼" is sorted under "漢字".

## Related topics

- [Guidelines for globalization](guidelines-and-checklist-for-globalizing-your-app.md)
- [Localize strings in your UI and app package manifest](../../app-resources/localize-strings-ui-manifest.md)
- [Tailor your resources for language, scale, high contrast, and other qualifiers](../../app-resources/tailor-resources-lang-scale-contrast.md)
- [Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md)
- [Updating images in response to qualifier value change events](../../app-resources/images-tailored-for-scale-theme-contrast.md#updating-images-in-response-to-qualifier-value-change-events)

## Samples

- [Application resources and localization sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Application%20resources%20and%20localization%20sample%20(Windows%208))