---
author: stevewhims
Description: Develop a global-ready app by appropriately formatting dates, times, numbers, phone numbers, and currencies.
title: Use global-ready formats
ms.assetid: 6ECE8BA4-9A7D-49A6-81EE-AB2BE7F0254F
label: Use global-ready formats
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

# Use global-ready formats

Develop a global-ready app by appropriately formatting dates, times, numbers, phone numbers, and currencies. This permits you to adapt your app later for additional cultures, regions, and languages in the global market.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**Windows.Globalization.Calendar**](https://msdn.microsoft.com/library/windows/apps/br206724)</li>
<li>[**Windows.Globalization.DateTimeFormatting**](https://msdn.microsoft.com/library/windows/apps/br206859)</li>
<li>[**Windows.Globalization.NumberFormatting**](https://msdn.microsoft.com/library/windows/apps/br226136)</li>
<li>[**Windows.Globalization.PhoneNumberFormatting**](https://msdn.microsoft.com/library/windows/apps/Windows.Globalization.PhoneNumberFormatting)</li>
</ul>
</div>

## Introduction

Many app developers naturally create their apps thinking only of their own language and culture. But when the app begins to grow into other markets, adapting the app for new languages and regions can be difficult in unexpected ways. For example, dates, times, numbers, calendars, currency, telephone numbers, units of measurement, and paper sizes are all items that can be displayed differently in different cultures or languages.

The process of adapting to new markets can be simplified by taking a few things into account as you develop your app.

## Format dates and times appropriately

There are many different ways to properly display dates and times. Different regions and cultures use different conventions for the order of day and month in the date, for the separation of hours and minutes in the time, and even for what punctuation is used as a separator. In addition, dates may be displayed in various long formats ("Wednesday, March 28, 2012") or short formats ("3/28/12"), which can vary across cultures. And of course, the names and abbreviations for the days of the week and months of the year differ for every language.

If you need to allow users to choose a date or select a time, use the standard [date and time picker](https://msdn.microsoft.com/library/windows/apps/hh465466) controls. These will automatically use the date and time formats for the user's preferred language and region.

If you need to display dates or times yourself, use [**Date/Time**](https://msdn.microsoft.com/library/windows/apps/br206859) and [**Number**](https://msdn.microsoft.com/library/windows/apps/br226136) formatters to automatically display the user's preferred format for dates, times and numbers. The code below formats a given DateTime by using the preferred language and region. For example, if the current date is 3 June 2012, the formatter gives "6/3/2012", if the user prefers English (United States), but it gives "03.06.2012" if the user prefers German (Germany):

```CSharp
    // Use the Windows.Globalization.DateTimeFormatting.DateTimeFormatter class
    // to display dates and times using basic formatters.

    // Formatters for dates and times, using shortdate format.
    var sdatefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shortdate");
    var stimefmt = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("shorttime");

    // Obtain the date that will be formatted.
    var dateToFormat = DateTime.Now;

    // Perform the actual formatting.
    var sdate = sdatefmt.Format(dateToFormat);
    var stime = stimefmt.Format(dateToFormat);

    // Results for display.
    var results = "Short Date: " + sdate + "\n" +
                  "Short Time: " + stime;
```

## Format numbers and currencies appropriately

Different cultures format numbers differently. Format differences may include how many decimal digits to display, what characters to use as decimal separators, and what currency symbol to use. Use [**NumberFormatting**](https://msdn.microsoft.com/library/windows/apps/br226136) to display decimal, percent, or permille numbers, and currencies. In most cases you simply display numbers or currencies according to the user's current preferences. But you may also use the formatters to display a currency for a particular region or format.

The code below gives an example of how to display currencies per the user's preferred language and region, or for a specific given currency system:

```CSharp
    // This scenario uses the Windows.Globalization.NumberFormatting.CurrencyFormatter class
    // to format a number as a currency.

    // Determine the current user's default currency.
    var userCurrency = Windows.System.UserProfile.GlobalizationPreferences.Currencies[0];

    // Number to be formatted.
    var fractionalNumber = 12345.67;

    // Currency formatter using the current user's preference settings for number formatting.
    var userCurrencyFormat = new Windows.Globalization.NumberFormatting.CurrencyFormatter(userCurrency);
    var currencyDefault = userCurrencyFormat.Format(fractionalNumber);

    // Create a formatter initialized to a specific currency,
    // in this case US Dollar (specified as an ISO 4217 code) 
    // but with the default number formatting for the current user.
    var currencyFormatUSD = new Windows.Globalization.NumberFormatting.CurrencyFormatter("USD"); 
    var currencyUSD = currencyFormatUSD.Format(fractionalNumber);

    // Create a formatter initialized to a specific currency.
    // In this case it's the Euro with the default number formatting for France.
    var currencyFormatEuroFR = new Windows.Globalization.NumberFormatting.CurrencyFormatter("EUR", new[] { "fr-FR" }, "FR");
    var currencyEuroFR = currencyFormatEuroFR.Format(fractionalNumber);

    // Results for display.
    var results = "Fixed number (" + fractionalNumber + ")\n" +
                  "With user's default currency: " + currencyDefault + "\n" +
                  "Formatted US Dollar: " + currencyUSD + "\n" +
                  "Formatted Euro (fr-FR defaults): " + currencyEuroFR;
```

## Use a culturally appropriate calendar

The calendar differs across regions and languages. The Gregorian calendar is not the default for every region. Users in some regions may choose alternate calendars, such as the Japanese era calendar or Arabic lunar calendars. Dates and times on the calendar are also sensitive to different time zones and daylight saving time.

Use the standard [date and time picker](https://msdn.microsoft.com/library/windows/apps/hh465466) controls to allow users to choose a date, to ensure that the preferred calendar format is used. For more complex scenarios, where working directly with operations on calendar dates may be required, Windows.Globalization provides a [**Calendar**](https://msdn.microsoft.com/library/windows/apps/br206724) class that gives an appropriate calendar representation for the given culture, region, and calendar type.

## Format phone numbers appropriately

Phone numbers are formatted differently across regions. The number of digits, how the digits are grouped, and the significance of certain parts of the phone number vary from one country to the next. Starting in Windows 10, version 1607, you can use  [**PhoneNumberFormatting**](https://msdn.microsoft.com/library/windows/apps/Windows.Globalization.PhoneNumberFormatting) to format phone numbers appropriately for the current region.

[**PhoneNumberInfo**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.globalization.phonenumberformatting.phonenumberinfo.aspx) parses a string of digits and allows you to determine if the digits are a valid phone number in the current region, compare two numbers for equality, and to extract the different functional parts of the phone number, such as country code or geographical area code.

[**PhoneNumberFormatter**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.globalization.phonenumberformatting.phonenumberformatter.aspx) formats a string of digits or a PhoneNumberInfo for display, even when the string of digits represents a partial phone number. (You can use this partial number formatting to format a number as a user is entering the number.) 

The code below shows how to use PhoneNumberFormatter to format a phone number as it is being entered. Each time text changes in a TextBox named gradualInput, the contents of the text box are formatted using the current default region and displayed in a TextBlock named outBox. For demonstration purposes, the string is also formatted using the region for New Zealand, and displayed in a TextBlock named NZOutBox.
    
```csharp
    using Windows.Globalization;

    PhoneNumberFormatter currentFormatter, NZFormatter;

    public MainPage()
    {
        this.InitializeComponent();

        // Use the default formatter for the current region
        currentFormatter = new PhoneNumberFormatter();

        // Create an explicit formatter for New Zealand. 
        // Note that you must check the results of TryCreate before you use the formatter.
        PhoneNumberFormatter.TryCreate("NZ", out NZFormatter);

    }

    private void gradualInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Format for the default region into outBox.
        outBox.Text = currentFormatter.FormatPartialString(gradualInput.Text);

        // If the NZFormatter was created successfully, format the partial string for the NZOutBox.
        if(NZFormatter != null)
        {
            NZOutBox.Text = NZFormatter.FormatPartialString(gradualInput.Text);
        }
    }
```    

## Respect the user's language and cultural preferences

For scenarios where you provide different functionality based on the user's language, region, or cultural preferences, Windows gives you a way to access those preferences, through [**Windows.System.UserProfile.GlobalizationPreferences**](https://msdn.microsoft.com/library/windows/apps/br241825). When needed, use the **GlobalizationPreferences** class to get the value of the user's current geographic region, preferred languages, preferred currencies, and so on.

## Important APIs

* [Windows.Globalization.Calendar](https://msdn.microsoft.com/library/windows/apps/br206724)
* [Windows.Globalization.DateTimeFormatting](https://msdn.microsoft.com/library/windows/apps/br206859)
* [Windows.Globalization.NumberFormatting](https://msdn.microsoft.com/library/windows/apps/br226136)
* [Windows.System.UserProfile.GlobalizationPreferences](https://msdn.microsoft.com/library/windows/apps/br241825)

## Related topics

* [Plan for a global market](https://msdn.microsoft.com/library/windows/apps/hh465405)
* [Guidelines for date and time controls](https://msdn.microsoft.com/library/windows/apps/hh465466)

## Samples

* [Calendar details and math sample](http://go.microsoft.com/fwlink/p/?linkid=231636)
* [Date and time formatting sample](http://go.microsoft.com/fwlink/p/?linkid=231618)
* [Globalization preferences sample](http://go.microsoft.com/fwlink/p/?linkid=231608)
* [Number formatting and parsing sample](http://go.microsoft.com/fwlink/p/?linkid=231620)
