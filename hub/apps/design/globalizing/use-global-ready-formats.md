---
description: Design your app to be global-ready by appropriately formatting dates, times, numbers, phone numbers, and currencies. You'll then be able later to adapt your app for additional cultures, regions, and languages in the global market.
title: Globalize your date/time/number formats
ms.assetid: 6ECE8BA4-9A7D-49A6-81EE-AB2BE7F0254F
template: detail.hbs
ms.date: 11/07/2017
ms.topic: how-to
keywords: globalization, localizability, localization
ms.localizationpriority: medium
---
# Globalize your date/time/number formats

Design your app to be global-ready by appropriately formatting dates, times, numbers, phone numbers, and currencies. You'll then be able later to adapt your app for additional cultures, regions, and languages in the global market.

## Introduction

When creating your app, if you think more broadly than a single language and culture then you'll have fewer (if any) unexpected issues when your app grows into new markets. For example, dates, times, numbers, calendars, currency, telephone numbers, units of measurement, and paper sizes are all items that can be displayed differently in different cultures or languages.

Different regions and cultures use different date and time formats. These include conventions for the order of day and month in the date, for the separation of hours and minutes in the time, and even for what punctuation is used as a separator. In addition, dates may be displayed in various long formats ("Wednesday, March 28, 2012") or short formats ("3/28/12"), which vary across cultures. And, of course, the names and abbreviations for the days of the week and months of the year differ between languages.

You can preview the formats used for different languages. Go to **Settings** > **Time & Language** > **Region & language**, and click **Additional date, time, & regional settings** > **Change date, time, or number formats**. On the **Formats** tab, select a language from the **Format** drop-down and preview the formats in **Examples**.

This topic uses the terms "user profile language list", "app manifest language list", and "app runtime language list". For details on exactly what those terms mean and how to access their values, see [Understand user profile languages and app manifest languages](manage-language-and-region.md).

## Format dates and times for the app runtime language list

If you need to allow users to choose a date, or to select a time, then use the standard [calendar, date, and time controls](../../develop/ui/controls/date-and-time.md). These automatically use the best date and time format for the app runtime language list.

If you need to display dates or times yourself then you can use the [**DateTimeFormatter**](/uwp/api/windows.globalization.datetimeformatting?branch=live) class. By default, **DateTimeFormatter** automatically uses the best date and time format for the app runtime language list. So, the code below formats a given **DateTime** in the best way for that list. As an example, assume that your app manifest language list includes English (United States), which is also your default, and German (Germany). If the current date is Nov 6 2017 and the user profile language list contains German (Germany) first, then the formatter gives "06.11.2017". If the user profile language list contains English (United States) first (or if it contains neither English nor German), then the formatter gives "11/6/2017" (since "en-US" matches, or is used as the default).

```csharp
    // Use the DateTimeFormatter class to display dates and times using basic formatters.

    var shortDateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shortdate");
    var shortTimeFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shorttime");

    var dateTimeToFormat = DateTime.Now;

    var shortDate = shortDateFormatter.Format(dateTimeToFormat);
    var shortTime = shortTimeFormatter.Format(dateTimeToFormat);

    var results = "Short Date: " + shortDate + "\n" +
                  "Short Time: " + shortTime;
```

You can test the code above on your own PC like this.

- Make sure that you have resource files in your project qualified for both "en-US" and "de-DE" (see [Tailor your resources for language, scale, high contrast, and other qualifiers](/windows/apps/windows-app-sdk/mrtcore/tailor-resources-lang-scale-contrast)).
- Change your user profile language list in **Settings** > **Time & Language** > **Region & language** > **Languages**. Add German (Germany), make it the default, and run the code again.

## Format dates and times for the user profile language list

Remember that, by default, **DateTimeFormatter** matches the app runtime language list. That way, if you display strings such as "The date is &lt;date&gt;", then the language will match the date format.

If for whatever reason you want to format dates and/or times only according to the user profile language list, then you can do that using code like the example below. But if you do so then understand that the user can choose a language for which your app doesn't have translated strings. For example, if your app is not localized into German (Germany), but the user chooses that as their preferred language, then that could result in the display of arguably odd-looking strings such as "The date is 06.11.2017".

```csharp
    // Use the DateTimeFormatter class to display dates and times using basic formatters.

    var userLanguages = Windows.System.UserProfile.GlobalizationPreferences.Languages;

    var shortDateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shortdate", userLanguages);

    var results = "Short Date: " + shortDateFormatter.Format(DateTime.Now);
```

## Use templates and patterns for advanced date/time formatting

You can use classes in the [**Windows.Globalization.DateTimeFormatting**](/uwp/api/windows.globalization.datetimeformatting?branch=live) namespace with custom templates and patterns to display dates and times in exactly the format you wish.

### The difference between format templates and format patterns

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

### Combining date and time templates with custom formatting

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

## Format numbers and currencies appropriately

Different cultures format numbers differently. Format differences may include how many decimal digits to display, what characters to use as decimal separators, and what currency symbol to use. Use classes in the [**NumberFormatting**](/uwp/api/windows.globalization.numberformatting?branch=live) namespace to display decimal, percent, or permille numbers, and currencies. Most of the time, you will want these formatter classes to use the best format for the user profile. But you may use the formatters to display a currency for any region or format.

This example shows how to display currencies both per the user profile, and for a specific given currency system.

```csharp
    // This scenario uses the CurrencyFormatter class to format a number as a currency.

    var userCurrency = Windows.System.UserProfile.GlobalizationPreferences.Currencies[0];

    var valueToBeFormatted = 12345.67;

    var userCurrencyFormatter = new Windows.Globalization.NumberFormatting.CurrencyFormatter(userCurrency);
    var userCurrencyValue = userCurrencyFormatter.Format(valueToBeFormatted);

    // Create a formatter initialized to a specific currency,
    // in this case US Dollar (specified as an ISO 4217 code) 
    // but with the default number formatting for the current user.
    var currencyFormatUSD = new Windows.Globalization.NumberFormatting.CurrencyFormatter("USD");
    var currencyValueUSD = currencyFormatUSD.Format(valueToBeFormatted);

    // Create a formatter initialized to a specific currency.
    // In this case it's the Euro with the default number formatting for France.
    var currencyFormatEuroFR = new Windows.Globalization.NumberFormatting.CurrencyFormatter("EUR", new[] { "fr-FR" }, "FR");
    var currencyValueEuroFR = currencyFormatEuroFR.Format(valueToBeFormatted);

    // Results for display.
    var results = "Fixed number (" + valueToBeFormatted + ")\n" +
                    "With user's default currency: " + userCurrencyValue + "\n" +
                    "Formatted US Dollar: " + currencyValueUSD + "\n" +
                    "Formatted Euro (fr-FR defaults): " + currencyValueEuroFR;
```

You can test the code above on your own PC by changing the country or region in **Settings** > **Time & Language** > **Region & language** > **Country or region**. Choose a country or region (perhaps Iceland), and run the code again.

## Use a culturally appropriate calendar

The calendar differs across regions and languages. The Gregorian calendar is not the default for every region. Users in some regions may choose alternate calendars, such as the Japanese era calendar, or Arabic lunar calendars. Dates and times on the calendar are also sensitive to different time zones and daylight-saving time.

To ensure that the preferred calendar format is used, you can use the standard [calendar, date, and time controls](../../develop/ui/controls/date-and-time.md). For more complex scenarios, where working directly with operations on calendar dates may be required, **Windows.Globalization** provides a [**Calendar**](/uwp/api/windows.globalization.calendar?branch=live) class that gives an appropriate calendar representation for the given culture, region, and calendar type.

## Format phone numbers appropriately

Phone numbers are formatted differently across regions. The number of digits, how the digits are grouped, and the significance of certain parts of the phone number vary between countries/regions. Starting in Windows 10, version 1607, you can use classes in the [**PhoneNumberFormatting**](/uwp/api/windows.globalization.phonenumberformatting?branch=live) namespace to format phone numbers appropriately for the current region.

[**PhoneNumberInfo**](/uwp/api/windows.globalization.phonenumberformatting.phonenumberinfo?branch=live) parses a string of digits and allows you to: determine whether the digits are a valid phone number in the current region; compare two numbers for equality; and to extract the different functional parts of the phone number, such as country code or geographical area code.

[**PhoneNumberFormatter**](/uwp/api/windows.globalization.phonenumberformatting.phonenumberformatter?branch=live) formats a string of digits or a **PhoneNumberInfo** for display, even when the string of digits represents a partial phone number. You can use this partial number formatting to format a number as a user is entering the number.

The example below shows how to use **PhoneNumberFormatter** to format a phone number as it is being entered. Each time text changes in a **TextBox** named phoneNumberInputTextBox, the contents of the text box are formatted using the current default region and displayed in a **TextBlock** named phoneNumberOutputTextBlock. For demonstration purposes, the string is also formatted using the region for New Zealand, and displayed in a TextBlock named phoneNumberOutputTextBlockNZ.
  
```csharp
    using Windows.Globalization.PhoneNumberFormatting;

    PhoneNumberFormatter currentFormatter, NZFormatter;

    public MainPage()
    {
        this.InitializeComponent();

        // Use the default formatter for the current region
        this.currentFormatter = new PhoneNumberFormatter();

        // Create an explicit formatter for New Zealand. 
        PhoneNumberFormatter.TryCreate("NZ", out this.NZFormatter);
    }

    private void phoneNumberInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Format for the default region.
        this.phoneNumberOutputTextBlock.Text = currentFormatter.FormatPartialString(this.phoneNumberInputTextBox.Text);

        // If the NZFormatter was created successfully, format the partial string for the NZ TextBlock.
        if(this.NZFormatter != null)
        {
            this.phoneNumberOutputTextBlockNZ.Text = this.NZFormatter.FormatPartialString(this.phoneNumberInputTextBox.Text);
        }
    }
```    

You can test the code above on your own PC by changing the country or region in **Settings** > **Time & Language** > **Region & language** > **Country or region**. Choose a country or region (perhaps New Zealand to confirm that the formats match), and run the code again. For test data, you can do a web search for the phone number of a business in New Zealand.

## The user's language and cultural preferences

For scenarios where you wish to provide different functionality based solely on the user's language, region, or cultural preferences, Windows gives you a way to access those preferences, through [**Windows.System.UserProfile.GlobalizationPreferences**](/uwp/api/windows.system.userprofile.globalizationpreferences?branch=live). When needed, use the **GlobalizationPreferences** class to get the value of the user's current geographic region, preferred languages, preferred currencies, and so on. But remember that if your app's strings/images aren't localized for the user's preferred language then dates and times and other data formatted for that preferred language won't match the strings that you display.

## Important APIs

* [DateTimeFormatter](/uwp/api/windows.globalization.datetimeformatting?branch=live)
* [DateTime](/uwp/api/windows.foundation.datetime?branch=live)
* [NumberFormatting](/uwp/api/windows.globalization.numberformatting?branch=live)
* [Calendar](/uwp/api/windows.globalization.calendar?branch=live)
* [PhoneNumberFormatting](/uwp/api/windows.globalization.phonenumberformatting?branch=live)
* [GlobalizationPreferences](/uwp/api/windows.system.userprofile.globalizationpreferences?branch=live)

## Related topics

* [NumeralSystem values](glob-numeralsystem-values.md)
* [Calendar, date, and time controls](../../develop/ui/controls/date-and-time.md)
* [Understand user profile languages and app manifest languages](manage-language-and-region.md)
* [Tailor your resources for language, scale, high contrast, and other qualifiers](/windows/apps/windows-app-sdk/mrtcore/tailor-resources-lang-scale-contrast)

## Samples

* [Calendar details and math sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Calendar%20details%20and%20math%20sample%20(Windows%208))
* [Date and time formatting sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Date%20and%20time%20formatting%20sample%20(Windows%208))
* [Globalization preferences sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Globalization%20preferences%20sample%20(Windows%208))
* [Number formatting and parsing sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Number%20formatting%20and%20parsing%20sample%20(Windows%208))
