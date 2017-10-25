---
author: stevewhims
Description: Use the Windows.Globalization.DateTimeFormatting API with custom patterns to display dates and times in exactly the format you wish.
title: Use patterns to format dates and times
ms.assetid: 012028B3-9DA2-4E72-8C0E-3E06BEC3B3FE
label: Use patterns to format dates and times
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

# Use patterns to format dates and times

Use the [**Windows.Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859) API with custom patterns to display dates and times in exactly the format you wish.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**Windows.Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859)</li>
<li>[**DateTimeFormatter**](https://msdn.microsoft.com/library/windows/apps/br206828)</li>
<li>[**DateTime**](https://msdn.microsoft.com/library/windows/apps/br206576)</li>
</ul>
</div>

## Introduction

[**Windows.Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859) provides various ways to properly format dates and times for languages and regions around the world. You can use standard formats for year, month, day, and so on, or you can use standard string templates, such as "longdate" or "month day".

But when you want more control over the order and format of the constituents of the [**DateTime**](https://msdn.microsoft.com/library/windows/apps/br206576) string you wish to display, you can use a special syntax for the string template parameter, called a "pattern". The pattern syntax allows you to obtain individual constituents of a **DateTime** object—just the month name, or just the year value, for example—in order to display them in whatever custom format you choose. Furthermore, the pattern can be localized to adapt to other languages and regions.

**Note**  This is an overview of format patterns. For a more complete discussion of format templates and format patterns see the Remarks section of the [**DateTimeFormatter**](https://msdn.microsoft.com/library/windows/apps/br206828) class.

## What you need to know

It's important to note that when you use patterns, you are building a custom format that is not guaranteed to be valid across cultures. For example, consider the "month day" template:

**C#**
```CSharp
var datefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day");
```
**JavaScript**
```JavaScript
var datefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day");
```

This creates a formatter based on the language and region value of the current context. Therefore, it always displays the month and day together in an appropriate global format. For example, it displays "January 1" for English (US), but "1 janvier" for French (France) and "1月1日" for Japanese. That is because the template is based on a culture-specific pattern string, which can be accessed via the pattern property:

**C#**
```CSharp
var monthdaypattern = datefmt.Patterns;
```
**JavaScript**
```JavaScript
var monthdaypattern = datefmt.patterns;
```

This yields different results depending on the language and region of the formatter. Note that different regions may use different constituents, in different orders, with or without additional characters and spacing:

``` syntax
En-US: "{month.full} {day.integer}"
Fr-FR: "{day.integer} {month.full}"
Ja-JP: "{month.integer}月{day.integer}日"
```

You can use patterns to construct a custom [**DateTimeFormatter**](https://msdn.microsoft.com/library/windows/apps/br206828), for instance this one based on the US English pattern:

**C#**
```CSharp
var datefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("{month.full} {day.integer}");
```
**JavaScript**
```JavaScript
var datefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("{month.full} {day.integer}");
```

Windows returns culture-specific values for the individual constituents inside the brackets {}. But with the pattern syntax, the constituent order is invariant. You get precisely what you ask for, which may not be culturally appropriate:

``` syntax
En-US: January 1
Fr-FR: janvier 1 (inappropriate for France; non-standard order)
Ja-JP: 1月1 (inappropriate for Japan; the day symbol is missing)
```

Furthermore, patterns are not guaranteed to remain consistent over time. Countries or regions may change their calendar systems, which alters a format template. Windows updates the output of the formatters to accommodate such changes. Therefore, you should only use the pattern syntax for formatting [**DateTime**](https://msdn.microsoft.com/library/windows/apps/br206576)s when:

-   You are not dependent on a particular output for a format.
-   You do not need the format to follow some culture-specific standard.
-   You specifically intend the pattern to be invariant across cultures.
-   You intend to localize the pattern.

To summarize the differences between the standard string templates and non-standard string patterns:

**String templates, such as "month day":**

-   Abstracted representation of a [**DateTime**](https://msdn.microsoft.com/library/windows/apps/br206576) format that includes values for the month and the day, in some order.
-   Guaranteed to return a valid standard format across all language-region values supported by Windows.
-   Guaranteed to give you a culturally-appropriate formatted string for the given language-region.
-   Not all combinations of constituents are valid. For example, there is no string template for "dayofweek day".

**String patterns, such as "{month.full} {day.integer}":**

-   Explicitly ordered string that expresses the full month name, followed by a space, followed by the day integer, in that order.
-   May not correspond to a valid standard format for any language-region pair.
-   Not guaranteed to be culturally appropriate.
-   Any combination of constituents may be specified, in any order.

## Tasks

Suppose you wish to display the current month and day together with the current time, in a specific format. For example, you would like US English users to see something like this:

``` syntax
June 25 | 1:38 PM
```

The date part corresponds to the "month day" template, and the time part corresponds to the "hour minute" template. So, you can create a custom format that concatenates the patterns which make up those templates.

First, get the formatters for the relevant date and time templates, and then get the patterns of those templates:

**C#**
```CSharp
// Get formatters for the date part and the time part.
var mydate = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day");
var mytime = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("hour minute");

// Get the patterns from these formatters.
var mydatepattern = mydate.Patterns[0];
var mytimepattern = mytime.Patterns[0];
```
**JavaScript**
```JavaScript
// Get formatters for the date part and the time part.
var dtf = Windows.Globalization.DateTimeFormatting;
var mydate = dtf.DateTimeFormatter("month day");
var mytime = dtf.DateTimeFormatter("hour minute");

// Get the patterns from these formatters.
var mydatepattern = mydate.patterns[0];
var mytimepattern = mytime.patterns[0];
```

You should store your custom format as a localizable resource string. For example, the string for English (United States) would be "{date} | {time}". Localizers can adjust this string as needed. For example, they can change the order of the constituents, if it seems more natural in some language or region to have the time precede the date. Or, they can replace "|" with some other separator character. At runtime you replace the {date} and {time} portions of the string with the relevant pattern:

**C#**
```CSharp
// Assemble the custom pattern. This string comes from a resource, and should be localizable. 
var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
var mydateplustime = resourceLoader.GetString("date_plus_time");
mydateplustime = mydateplustime.replace("{date}", mydatepattern);
mydateplustime = mydateplustime.replace("{time}", mytimepattern);
```
**JavaScript**
```JavaScript
// Assemble the custom pattern. This string comes from a resource, and should be localizable. 
var mydateplustime = WinJS.Resources.getString("date_plus_time");
mydateplustime = mydateplustime.replace("{date}", mydatepattern);
mydateplustime = mydateplustime.replace("{time}", mytimepattern);
```

Then you can construct a new formatter based on the custom pattern:

**C#**
```CSharp
// Get the custom formatter.
var mydateplustimefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter(mydateplustime);
```
**JavaScript**
```JavaScript
// Get the custom formatter.
var mydateplustimefmt = new dtf.DateTimeFormatter(mydateplustime);
```

## Related topics

* [Date and time formatting sample](http://go.microsoft.com/fwlink/p/?LinkId=231618)
* [**Windows.Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859)
* [**Windows.Foundation.DateTime**](https://msdn.microsoft.com/library/windows/apps/br206576)
