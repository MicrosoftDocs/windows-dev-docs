---
Description: Design your app to be global-ready by appropriately formatting dates, times, numbers, phone numbers, and currencies. You'll then be able later to adapt your app for additional cultures, regions, and languages in the global market.
title: Globalize your date/time/number formats
ms.assetid: 6ECE8BA4-9A7D-49A6-81EE-AB2BE7F0254F
template: detail.hbs
ms.date: 11/07/2017
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
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

If you need to allow users to choose a date, or to select a time, then use the standard [calendar, date, and time controls](../controls-and-patterns/date-and-time.md). These automatically use the best date and time format for the app runtime language list.

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

- Make sure that you have resource files in your project qualified for both "en-US" and "de-DE" (see [Tailor your resources for language, scale, high contrast, and other qualifiers](../../app-resources/tailor-resources-lang-scale-contrast.md)).
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

To ensure that the preferred calendar format is used, you can use the standard [calendar, date, and time controls](../controls-and-patterns/date-and-time.md). For more complex scenarios, where working directly with operations on calendar dates may be required, **Windows.Globalization** provides a [**Calendar**](/uwp/api/windows.globalization.calendar?branch=live) class that gives an appropriate calendar representation for the given culture, region, and calendar type.

## Format phone numbers appropriately

Phone numbers are formatted differently across regions. The number of digits, how the digits are grouped, and the significance of certain parts of the phone number vary between countries. Starting in Windows 10, version 1607, you can use classes in the [**PhoneNumberFormatting**](/uwp/api/windows.globalization.phonenumberformatting?branch=live) namespace to format phone numbers appropriately for the current region.

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
* [NumberFormatting](/uwp/api/windows.globalization.numberformatting?branch=live)
* [Calendar](/uwp/api/windows.globalization.calendar?branch=live)
* [PhoneNumberFormatting](/uwp/api/windows.globalization.phonenumberformatting?branch=live)
* [GlobalizationPreferences](/uwp/api/windows.system.userprofile.globalizationpreferences?branch=live)

## Related topics

* [Calendar, date, and time controls](../controls-and-patterns/date-and-time.md)
* [Understand user profile languages and app manifest languages](manage-language-and-region.md)
* [Tailor your resources for language, scale, high contrast, and other qualifiers](../../app-resources/tailor-resources-lang-scale-contrast.md)

## Samples

* [Calendar details and math sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Calendar%20details%20and%20math%20sample%20(Windows%208))
* [Date and time formatting sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Date%20and%20time%20formatting%20sample%20(Windows%208))
* [Globalization preferences sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Globalization%20preferences%20sample%20(Windows%208))
* [Number formatting and parsing sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Number%20formatting%20and%20parsing%20sample%20(Windows%208))
