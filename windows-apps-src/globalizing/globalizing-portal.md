---
author: stevewhims
Description: Globalization is the process of designing and developing your app to act appropriately for different global markets without any changes or customization.
Search.SourceType: Video
title: Globalization and localization
ms.assetid: c0791eec-5bb8-4a13-8977-61d7d98e35ce
label: Intro
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Globalization and localization
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Windows is used worldwide, by audiences that vary in culture, region, and language. A user may speak any language, or even multiple languages. A user may be located anywhere in the world, and may speak any language in any location. You can increase the potential market for your app by designing it to be readily adaptable using *globalization* and *localization*.

**Globalization** is the process of designing and developing your app to act appropriately for different global markets without any changes or customization.

For example, you can:

-   Design the layout of your app to accommodate the different text lengths and font sizes of other languages in labels and text strings.
-   Retrieve text and culture-dependent images from resources that can be adapted to different local markets, instead of hard-coding them into your app's code or markup.
-   Use globalization APIs to display data that are formatted differently in different regions, such as numeric values, dates, times, and currencies.

**Localization** is the process of adapting your app to meet the language, cultural, and political requirements of a specific local market.

For example:

-   Translate the text and labels of the app for the new market, and create separate resources for its language.
-   Modify any culture-dependent images as necessary, and place in separate resources.

Watch this video for a brief introduction on how to prepare your app for the world: [Introduction to globalization and localization](https://channel9.msdn.com/Blogs/One-Dev-Minute/Introduction-to-globalization-and-localization).

## Articles
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Article</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>[Do's and don'ts](guidelines-and-checklist-for-globalizing-your-app.md)</p></td>
<td align="left"><p>Follow these best practices when globalizing your apps for a wider audience and when localizing your apps for a specific market.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Use global-ready formats](use-global-ready-formats.md)</p></td>
<td align="left"><p>Develop a global-ready app by appropriately formatting dates, times, numbers, and currencies.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Manage language and region](manage-language-and-region.md)</p></td>
<td align="left"><p>Control how Windows selects UI resources and formats the UI elements of the app, by using the various language and region settings provided by Windows.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Use patterns to format dates and times](use-patterns-to-format-dates-and-times.md)</p></td>
<td align="left"><p>Use the [<strong>Windows.Globalization.DateTimeFormatting</strong>](https://msdn.microsoft.com/library/windows/apps/br206859) API with custom patterns to display dates and times in exactly the format you wish.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Adjust layout and fonts, and support RTL](adjust-layout-and-fonts--and-support-rtl.md)</p></td>
<td align="left"><p>Develop your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Prepare your app for localization](prepare-your-app-for-localization.md)</p></td>
<td align="left"><p>Prepare your app for localization to other markets, languages, or regions.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Put UI strings into resources](put-ui-strings-into-resources.md)</p></td>
<td align="left"><p>Put string resources for your UI into resource files. You can then reference those strings from your code or markup.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Migrate legacy resources to the Windows 10 resource management platform](using-mrt-for-converted-desktop-apps-and-games.md)</p></td>
<td align="left"><p>Move legacy Win32 resources to Windows 10 resource management platform with minimal code changes.</p></td>
</tr>
</tbody>
</table>

 

See also the documentation originally created for Windows 8.x, which still applies to Universal Windows Platform (UWP) apps and Windows 10.

-   [Globalizing your app](https://msdn.microsoft.com/library/windows/apps/xaml/hh965328)
-   [Language matching](https://msdn.microsoft.com/library/windows/apps/xaml/jj673578.aspx)
-   [NumeralSystem values](https://msdn.microsoft.com/library/windows/apps/xaml/jj236471.aspx)
-   [International fonts](https://msdn.microsoft.com/library/windows/apps/xaml/dn263115.aspx)
-   [App resources and localization](https://msdn.microsoft.com/library/windows/apps/xaml/hh710212.aspx)

 

 



