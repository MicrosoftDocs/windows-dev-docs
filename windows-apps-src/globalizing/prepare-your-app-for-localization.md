---
author: stevewhims
Description: Prepare your app for localization to other markets, languages, or regions.
title: Prepare your app for localization
ms.assetid: 06E1D4BB-59EA-4D71-99AC-7CB93D2A58A7
label: Prepare your app for localization
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Prepare your app for localization


<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Prepare your app for localization to other markets, languages, or regions. Before you get started, be sure to read through the [do's and don'ts](guidelines-and-checklist-for-globalizing-your-app.md).

## Use resource files and qualifiers.


Be sure to specify the UI strings of your app in resource files, instead of placing them in your code. For more detail, see [Put UI strings into resources](put-ui-strings-into-resources.md).

Specify images or other file resources with the appropriate language tag in their file or folder. Be aware that it takes a significant amount of system resources to localize images, audio, and video, so it’s best to use neutral media assets whenever you can. To learn more, see [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324).

## Add contextual comments.


Add localization comments to your app resource files. The comments are visible to the localizer, and should provide contextual information that helps the localizer to accurately translate the resources. The comments should also provide sufficient constraint information on the resource, so that translation does not break the software. Optionally, the comments can be logged by the Makepri.exe tool.

**XAML:** Resw files (resources created in Visual Studio for apps using XAML) have a comment element. For example:

```XML
<data name="String1">
    <value>Hello World</value>
    <comment>A greeting (This is a comment to the localizer)</comment>
</data>
```

## Localize sentences instead of words.


Consider the following string: "The {0} could not be synchronized."

A variety of words could replace {0}, such as appointment, task, or document. While this example would appear to work for the English language, it will not work in all cases for the corresponding sentence in German. Notice that in the following German sentences, some of the words in the template string ("Der", "Die", "Das") need to match the parameterized word:

| English                                    | German                                           |
|:------------------------------------------ |:------------------------------------------------ |
| The appointment could not be synchronized. | Der Termin konnte nicht synchronisiert werden.   |
| The task could not be synchronized.        | Die Aufgabe konnte nicht synchronisiert werden.  |
| The document could not be synchronized.    | Das Dokument konnte nicht synchronisiert werden. |

 

As another example, consider the sentence "Remind me in {0} minute(s)." While using "minute(s)" works for the English language, other languages might use different terms. For example, the Polish language uses "minuta", "minuty", or "minut" depending on the context.

To solve this problem, localize the entire sentence, rather than a single word. Doing this may seem like extra work and an inelegant solution, but it is the best solution because:

-   A clean error message will be displayed for all languages.
-   Your localizer will not need to ask about what the strings will be replaced with.
-   You will not need to implement a costly code fix when a problem like this surfaces after your app is completed.

## Ensure the correct parameter order.


Don't assume that all languages use parameters in the same order. For example, consider the string "Every %s %s", where the first %s is replaced by the name of a month, and the second %s is replaced by the date of a month. This example works for the English language, but will fail when the app is localized into the German language, where the date and month are displayed in the reverse order.

To solve this problem, change the string to "Every %1 %2", so that the order is interchangeable depending on the language.

## Don’t over localize.


Localize specific strings, not tags. Consider the following examples:

| Over-localized string                   | Correctly-localized string |
|:--------------------------------------- |:-------------------------- |
| &lt;link&gt;terms of use&lt;/link&gt;   | terms of use               |
| &lt;link&gt;privacy policy&lt;/link&gt; | privacy policy             |

 

Including the above &lt;link&gt; tag in the resources means that it too will be localized. This renders the tag not valid. Only the strings themselves should be localized. Generally, you should think of tags as code that should be kept separate from localizable content. However, some long strings should include markup to keep context and ensure ordering.

## Do not use the same strings in dissimilar contexts.


Reusing a string may seem like the best solution, but it can cause localization problems if the same word or phrase can have different meanings or contexts.

You can reuse strings if the two contexts are the same. For instance, you can reuse the string "Volume" for both sound effect volume and music volume because both refer to intensity of sound. You should not reuse that same string when referring to a hard disk volume because the context and meaning are different, and the word might be translated differently.

Another example is the use of the strings "on" and "off". In the English language, "on" and "off" can be used for a toggle for Flight Mode, Bluetooth, and devices. But in Italian, the translation depends on the context of what is being turned on and off. You would need to create a pair of strings for each context.

Additionally, a string like "text" or "fax" could be used as both a verb and a noun in the English language, which can confuse the translation process. Instead, create a separate string for both the verb and noun format. When you're not sure whether the contexts are the same, err on the safe side and use a distinct string.

## Identify resources with unique attributes.


Resource identifiers are case insensitive and must be unique per resource file. When accessing a resource, use the resource identifier, not the actual value of the resource. Resource identifiers don't change, but the actual values of the resources do change depending on the language.

Be sure to use meaningful resource identifiers to provide additional context for translation.

Don't change the resource identifiers after the string resources are sent to translation. Localization teams use the resource identifier to track additions, deletions, and updates in the resources. Changes in resource identifiers—also known as "resource identifiers shift"—require strings to be retranslated, because it will appear as though strings were deleted and others added.

## Choose an appropriate translation approach.


After strings are separated into resource files, they can be translated. The ideal time to translate strings is after the strings in your project are finalized, which usually happens toward the end of a project. You can approach the translation process in number of ways. This may depend on the volume of strings to be translated, the number of languages to be translated, and how the translation will be done (such as in-house versus hiring an external vendor).

Consider the following options:

-   **The resource files can be translated by opening them directly in the project.** This approach works well for a project that has a small volume of strings and that needs to be translated into two or three languages. It could be suitable for a scenario where a developer speaks more than one language and is willing to handle the translation process. This approach benefits by being quick, requires no tools, and minimizes the risk of mistranslations, but it is not scalable. In particular, the resources in different languages can easily get out of sync, causing bad user experiences and maintenance headaches.
-   **The string resource files are in XML or ResJSON text format, so could be handed off for translation using any text editor. The translated files would then be copied back into the project.** This approach carries a risk of translators accidentally editing the XML tags, but it lets translation work take place outside of the Microsoft Visual Studio project. This approach could work well for projects that need to be translated into a small number of languages. The XLIFF format is an XML format specifically designed for use in localization, and should be well supported by some localization vendors or localization tools. You can use the [Multilingual App Toolkit](https://msdn.microsoft.com/en-us/library/windows/apps/xaml/jj572370.aspx) to generate XLIFF files from other resource files, such as .resw or .resjson.

Handoffs to localizers may need to occur for other files, such as images or audio files. Typically, we don't recommend creating culturally dependent files because they can be difficult to localize.

Additionally, consider the following suggestions:

-   **Use a localization tool.** A number of localization tools are available for parsing resource files and allowing only the translatable strings to be edited by translators. This approach reduces the risk of a translator accidentally editing the XML tags. But it has the drawback of introducing a new tool and process to the localization process. A localization tool is good for projects with a large volume of strings, but a small number of languages. To learn more, see [How to use the Multilingual App Toolkit](https://msdn.microsoft.com/en-us/library/windows/apps/xaml/jj572370.aspx).
-   **Use a localization vendor.** Consider using a localization vendor if your project contains a large volume of strings and needs to be translated for many languages. A localization vendor can give advice about tools and processes, as well as translating your resource files. This is an ideal solution, but is also the most costly option, and may increase the turnaround time for your translated content.
-   **Keep your localizers informed.** Inform localizers of strings that can be considered a noun or a verb. Explain fabricated words to your localizers by using terminology tools. Keep strings grammatically correct, unambiguous, and as nontechnical as possible to avoid confusion.

## Keep access keys and labels consistent.


It is a challenge to "synchronize" the access keys used in accessibility with the display of the localized access keys, because the two string resources are categorized in two separate sections. Be sure to provide comments for the label string such as: `Make sure that the emphasized shortcut key  is synchronized with the access key.`


## Support Furigana for Japanese strings that can be sorted.


Japanese Kanji characters have the unique property of having more than one pronunciation depending on the word and context they are used in. This leads to problems when you try to sort Japanese named objects, such as application names, files, songs, and so on. Japanese Kanji have, in the past, usually been sorted in a machine-understandable order called XJIS. Unfortunately, because this sorting order is not phonetic it is not very useful for humans.

Furigana works around this problem by allowing the user or creator to specify the phonetics for the characters they are using. If you use the following procedure to add Furigana to your app name, you can ensure that it is sorted in the proper location in the app list. If your app name contains Kanji characters and Furigana is not provided when the user’s UI language or the sort order is set to Japanese, Windows makes its best effort to generate the appropriate pronunciation. However, there is a possibility for app names containing rare or unique readings to be sorted under a more common reading instead. Therefore, the best practice for Japanese applications (especially those containing Kanji characters in their names) is to provide a Furigana version of their app name as part of the Japanese localization process.

1.  Add "ms-resource:Appname" as the Package Display Name and the Application Display Name.
2.  Create a ja-JP folder under strings, and add two resource files as follows:

    ``` syntax
    strings\
        en-us\
        ja-jp\
            Resources.altform-msft-phonetic.resw
            Resources.resw
    ```

3.  In Resources.resw for general ja-JP: Add a string resource for Appname "希蒼"
4.  In Resources.altform-msft-phonetic.resw for Japanese furigana resources: Add Furigana value for AppName "のあ"

The user can search for the app name "希蒼" using both the Furigana value "のあ" (noa),　and the phonetic value (using the **GetPhonetic** function from Input Method Editor (IME)) "まれあお" (mare-ao).

Sorting follows the **Regional Control Panel** format:

-   Under Japanese user locale,
    -   If Furigana is enabled, the "希蒼" is sorted under "の".
    -   If Furigana is missing, the "希蒼" is sorted under "ま".
-   Under non-Japanese user locale,
    -   If Furigana is enabled, the "希蒼" is sorted under "の".
    -   If Furigana is missing, the "希蒼" is sorted under "漢字".

## Related topics


* [Globalization and localization do's and don'ts](guidelines-and-checklist-for-globalizing-your-app.md)
* [Put UI strings into resources](put-ui-strings-into-resources.md)
* [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324)
 

 



