---
description: Use the Windows.Globalization.DateTimeFormatting API with custom templates and patterns to display dates and times in exactly the format you wish.
title: Use patterns to format dates and times
ms.assetid: 012028B3-9DA2-4E72-8C0E-3E06BEC3B3FE
label: Use patterns to format dates and times
template: detail.hbs
ms.date: 11/09/2017
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---
# Use templates and patterns to format dates and times

Use classes in the [**Windows.Globalization.DateTimeFormatting**](/uwp/api/windows.globalization.datetimeformatting?branch=live) namespace with custom templates and patterns to display dates and times in exactly the format you wish.

## Introduction

The [**DateTimeFormatter**](/uwp/api/windows.globalization.datetimeformatting?branch=live) class provides various ways to properly format dates and times for languages and regions around the world. You can use standard formats for year, month, day, and so on. Or you can pass a format template to the *formatTemplate* argument of the **DateTimeFormatter** constructor, such as "longdate" or "month day".

But when you want even more control over the order and format of the components of the [**DateTime**](/uwp/api/windows.foundation.datetime?branch=live) object you wish to display, you can pass a format pattern to the *formatTemplate* argument of the constructor. A format pattern uses a special syntax, which allows you to obtain individual components of a **DateTime** object&mdash;just the month name, or just the year value, for example&mdash;in order to display them in whatever custom format you choose. Furthermore, the pattern can be localized to adapt to other languages and regions.

**Note**  This is only an overview of format patterns. For a more complete discussion of format templates and format patterns see the Remarks section of the [**DateTimeFormatter**](/uwp/api/windows.globalization.datetimeformatting?branch=live) class.

## The difference between format templates and format patterns

A format template is a culture-agnostic format string. So, if you construct a **DateTimeFormatter** using a format template, then the formatter displays your format components in the right order for the current language. Conversely, a format pattern is culture-specific. If you construct a **DateTimeFormatter** using a format pattern, then the formatter will use the pattern exactly as given. Consequently, a pattern isn't necessarily valid across cultures.

Let's illustrate this distinction with an example. We'll pass a simple format template (not a pattern) to the **DateTimeFormatter** constructor. This is the format template "month day".

```csharp
var dateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day");
```

This creates a formatter based on the language and region value of the current context. The order of the components in a format template doesn't matter; the formatter displays them in the right order for the current language. So, it displays "January 1" for English (United States), "1 janvier" for French (France), and "1月1日" for Japanese.

On the other hand, a format pattern is culture-specific. Let's access the format pattern for our format template.

```csharp
IReadOnlyList<string> monthDayPatterns = dateFormatter.Patterns;
```

This yields different results depending on the runtime language and region. Different regions might use different components, in different orders, with or without additional characters and spacing.

```syntax
En-US: "{month.full} {day.integer}"
Fr-FR: "{day.integer} {month.full}"
Ja-JP: "{month.integer}月{day.integer}日"
```

In the example above, we inputted a culture-agnostic format string, and we got back a culture-specific format string (which was a function of the language and region that happened to be in effect when we called `dateFormatter.Patterns`). It follows therefore that if you construct a **DateTimeFormatter** from a culture-specific format pattern, then it will only be valid for specific languages/regions.

```csharp
var dateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("{month.full} {day.integer}");
```

The formatter above returns culture-specific values for the individual components inside the brackets {}. But the order of components in a format pattern is invariant. You get precisely what you ask for, which may or may not be culturally appropriate. This formatter is valid for English (United States), but not for French (France) nor for Japanese.

``` syntax
En-US: January 1
Fr-FR: janvier 1 (inappropriate for France; non-standard order)
Ja-JP: 1月1 (inappropriate for Japan; the day symbol 日 is missing)
```

Furthermore, a pattern that's correct today might not be correct in the future. Countries or regions might change their calendar systems, which alters a format template. Windows updates the output of formatters based on format templates to accommodate such changes. Therefore, you should only use the pattern syntax under one or more of these conditions.

-   You are not dependent on a particular output for a format.
-   You do not need the format to follow some culture-specific standard.
-   You specifically intend the pattern to be invariant across cultures.
-   You intend to localize the actual format pattern string itself.

Here's a summary of the distinction between format templates and format patterns.

**Format templates, such as "month day"**

-   Abstracted representation of a [DateTime](/uwp/api/windows.foundation.datetime?branch=live) format that includes values for the month, day, etc., in any order.
-   Guaranteed to return a valid standard format across all language-region values supported by Windows.
-   Guaranteed to give you a culturally-appropriate formatted string for the given language-region.
-   Not all combinations of components are valid. For example, "dayofweek day" is not valid.

**Format patterns, such as "{month.full} {day.integer}"**

-   Explicitly ordered string that expresses the full month name, followed by a space, followed by the day integer, in that order, or whatever specific format pattern you specify.
-   May not correspond to a valid standard format for any language-region pair.
-   Not guaranteed to be culturally appropriate.
-   Any combination of components may be specified, in any order.

## Examples

Suppose you wish to display the current month and day together with the current time, in a specific format. For example, you would like US English users to see something like this:

``` syntax
June 25 | 1:38 PM
```

The date part corresponds to the "month day" format template, and the time part corresponds to the "hour minute" format template. So, you can construct formatters for the relevant date and time format templates, and then concatenate their output together using a localizable format string.

```csharp
var dateToFormat = System.DateTime.Now;
var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

var dateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day");
var timeFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("hour minute");

var date = dateFormatter.Format(dateToFormat);
var time = timeFormatter.Format(dateToFormat);

string output = string.Format(resourceLoader.GetString("CustomDateTimeFormatString"), date, time);
```

`CustomDateTimeFormatString` is a resource identifier referring to a localizable resource in a Resources File (.resw). For a default language of English (United States), this would be set to a value of "{0} | {1}" along with a comment indicating that "{0}" is the date and "{1}" is the time. That way, translators can adjust the format items as needed. For example, they can change the order of the items if it seems more natural in some language or region to have the time precede the date. Or, they can replace "|" with some other separator character.

Another way to implement this example is to query the two formatters for their format patterns, concatenate those together, and then construct a third formatter from the resultant format pattern.

```csharp
var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

var dateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("month day");
var timeFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("hour minute");

string dateFormatterPattern = dateFormatter.Patterns[0];
string timeFormatterPattern = timeFormatter.Patterns[0];

string pattern = string.Format(resourceLoader.GetString("CustomDateTimeFormatString"), dateFormatterPattern, timeFormatterPattern);

var patternFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter(pattern);

string output = patternFormatter.Format(System.DateTime.Now);
```

## Important APIs

* [Windows.Globalization.DateTimeFormatting](/uwp/api/windows.globalization.datetimeformatting?branch=live)
* [DateTimeFormatter](/uwp/api/windows.globalization.datetimeformatting?branch=live)
* [DateTime](/uwp/api/windows.foundation.datetime?branch=live)

## Related topics

* [Date and time formatting sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Date%20and%20time%20formatting%20sample%20(Windows%208))
