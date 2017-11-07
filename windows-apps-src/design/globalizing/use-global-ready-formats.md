---
author: stevewhims
Description: Design your app to be global-ready by appropriately formatting dates, times, numbers, phone numbers, and currencies.
title: Use global-ready formats
ms.assetid: 6ECE8BA4-9A7D-49A6-81EE-AB2BE7F0254F
label: Use global-ready formats
template: detail.hbs
ms.author: stwhi
ms.date: 11/06/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, globalization, localization
localizationpriority: medium
---


# Use global-ready formats

Design your app to be global-ready by appropriately formatting dates, times, numbers, phone numbers, and currencies. You'll then be able later to adapt your app for additional cultures, regions, and languages in the global market.

## Introduction

When creating your app, if you think more broadly than a single language and culture then you'll have fewer (if any) unexpected issues when your app grows into new markets. For example, dates, times, numbers, calendars, currency, telephone numbers, units of measurement, and paper sizes are all items that can be displayed differently in different cultures or languages.

## Format dates and times appropriately

There are many different date and time formats in use globally. Different regions and cultures use different conventions for the order of day and month in the date, for the separation of hours and minutes in the time, and even for what punctuation is used as a separator. In addition, dates may be displayed in various long formats ("Wednesday, March 28, 2012") or short formats ("3/28/12"), which can vary across cultures. And, of course, the names and abbreviations for the days of the week and months of the year differ between languages.

If you need to allow users to choose a date, or to select a time, then use the standard [calendar, date, and time controls](../controls-and-patterns/date-and-time.md). These automatically use the date and time formats for the user's preferred language and region.

If you need to display dates or times yourself, use the [**DateTimeFormatter**](/uwp/api/windows.globalization.datetimeformatting?branch=live) class to automatically display the user's preferred format for dates and times. The code below formats a given **DateTime** by using the preferred language and region. For example, if the current date is Nov 6 2017, then if the user prefers English (United States) the formatter gives "11/6/2017". And if the user prefers German (Germany) then it gives "06.11.2017".

**C#**
```csharp
    // Use the DateTimeFormatter class to display dates and times using basic formatters.

    var userLanguages = Windows.System.UserProfile.GlobalizationPreferences.Languages;

    var userShortDateFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shortdate", userLanguages);
    var userShortTimeFormatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shorttime", userLanguages);

    var dateTimeToFormat = DateTime.Now;

    var shortDate = userShortDateFormatter.Format(dateTimeToFormat);
    var shortTime = userShortTimeFormatter.Format(dateTimeToFormat);

    var results = "Short Date: " + shortDate + "\n" +
                  "Short Time: " + shortTime;
```

You can test the code above on your own PC by changing the display language in **Settings** > **Time & Language** > **Region & language** > **Languages**. Add German (Germany), make it the default, and run the code again.

## Format numbers and currencies appropriately

Different cultures format numbers differently. Format differences may include how many decimal digits to display, what characters to use as decimal separators, and what currency symbol to use. Use classes in the [**NumberFormatting**](/uwp/api/windows.globalization.numberformatting?branch=live) namespace to display decimal, percent, or permille numbers, and currencies. In most cases you simply display numbers or currencies according to the user's current preferences. But you may also use the formatters to display a currency for a particular region or format.

This example shows how to display currencies per the user's preferred language and region, or for a specific given currency system.

**C#**
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
  
**C#**
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

## Respect the user's language and cultural preferences

For scenarios where you provide different functionality based on the user's language, region, or cultural preferences, Windows gives you a way to access those preferences, through [**Windows.System.UserProfile.GlobalizationPreferences**](/uwp/api/windows.system.userprofile.globalizationpreferences?branch=live). When needed, use the **GlobalizationPreferences** class to get the value of the user's current geographic region, preferred languages, preferred currencies, and so on.

## Important APIs

[DateTimeFormatter](/uwp/api/windows.globalization.datetimeformatting?branch=live)
[NumberFormatting](/uwp/api/windows.globalization.numberformatting?branch=live)
[Calendar](/uwp/api/windows.globalization.calendar?branch=live)
[PhoneNumberFormatting](/uwp/api/windows.globalization.phonenumberformatting?branch=live)
[GlobalizationPreferences](/uwp/api/windows.system.userprofile.globalizationpreferences?branch=live)

## Related topics

[calendar, date, and time controls](../controls-and-patterns/date-and-time.md)

## Samples

* [Calendar details and math sample](http://go.microsoft.com/fwlink/p/?linkid=231636)
* [Date and time formatting sample](http://go.microsoft.com/fwlink/p/?linkid=231618)
* [Globalization preferences sample](http://go.microsoft.com/fwlink/p/?linkid=231608)
* [Number formatting and parsing sample](http://go.microsoft.com/fwlink/p/?linkid=231620)
