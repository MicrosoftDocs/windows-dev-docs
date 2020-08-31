---
Description: The calendar date picker is a drop down control that's optimized for picking a single date from a calendar view where contextual information like the day of the week or fullness of the calendar is important.
title: Calendar date picker
ms.assetid: 9e0213e0-046a-4906-ba86-0b49be51ca99
label: Calendar date picker
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
pm-contact: kisai
design-contact: ksulliv
dev-contact: joyate
doc-status: Published
ms.localizationpriority: medium
---
# Calendar date picker

The calendar date picker is a drop down control that's optimized for picking a single date from a calendar view where contextual information like the day of the week or fullness of the calendar is important. You can modify the calendar to provide additional context or to limit available dates.

**Get the Windows UI Library**

|  |  |
| - | - |
| ![WinUI logo](images/winui-logo-64x64.png) | Windows UI Library 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md). WinUI is a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](/uwp/toolkits/winui/). |

> **Platform APIs**: [CalendarDatePicker class](/uwp/api/Windows.UI.Xaml.Controls.CalendarDatePicker), [Date property](/uwp/api/windows.ui.xaml.controls.calendardatepicker.date), [DateChanged event](/uwp/api/windows.ui.xaml.controls.calendardatepicker.datechanged)

## Is this the right control?

Use a **calendar date picker** to let a user pick a single date from a contextual calendar view. Use it for things like choosing an appointment or departure date.

To let a user pick a known date, such as a date of birth, where the context of the calendar is not important, consider using a [date picker](date-picker.md).

For more info about choosing the right control, see the [Date and time controls](date-and-time.md) article.

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/CalendarDatePicker">open the app and see the CalendarDatePicker in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

The entry point displays placeholder text if a date has not been set; otherwise, it displays the chosen date. When the user selects the entry point, a calendar view expands for the user to make a date selection. The calendar view overlays other UI; it doesn't push other UI out of the way.

![Example of calendar date picker](images/calendar-date-picker-2-views.png)

## Create a date picker

```xaml
<CalendarDatePicker x:Name="arrivalCalendarDatePicker" Header="Arrival date"/>
```

```csharp
CalendarDatePicker arrivalCalendarDatePicker = new CalendarDatePicker();
arrivalCalendarDatePicker.Header = "Arrival date";
```

The resulting calendar date picker looks like this:

![Example of calendar date picker](images/calendar-date-picker-closed.png)

The calendar date picker has an internal [CalendarView](/uwp/api/Windows.UI.Xaml.Controls.CalendarView) for picking a date. A subset of CalendarView properties, like [IsTodayHighlighted](/uwp/api/windows.ui.xaml.controls.calendardatepicker.istodayhighlighted) and [FirstDayOfWeek](/uwp/api/windows.ui.xaml.controls.calendardatepicker.firstdayofweek), exist on CalendarDatePicker and are forwarded to the internal CalendarView to let you modify it. 

However, you can't change the [SelectionMode](/uwp/api/windows.ui.xaml.controls.calendarview.selectionmode) of the internal CalendarView to allow multiple selection. If you need to let a user pick multiple dates or need a calendar to be always visible, consider using a calendar view instead of a calendar date picker. See the [Calendar view](calendar-view.md) article for more info on how you can modify the calendar display.

### Selecting dates

Use the [Date](/uwp/api/windows.ui.xaml.controls.calendardatepicker.date) property to get or set the selected date. By default, the Date property is **null**. When a user selects a date in the calendar view, this property is updated. A user can clear the date by clicking the selected date in the calendar view to deselect it. 

You can set the date in your code like this.

```csharp
myCalendarDatePicker.Date = new DateTime(1977, 1, 5);
```

When you set the Date in code, the value is constrained by the [MinDate](/uwp/api/windows.ui.xaml.controls.calendardatepicker.mindate) and [MaxDate](/uwp/api/windows.ui.xaml.controls.calendardatepicker.maxdate) properties.
- If **Date** is smaller than **MinDate**, the value is set to **MinDate**.
- If **Date** is greater than **MaxDate**, the value is set to **MaxDate**.

You can handle the [DateChanged](/uwp/api/windows.ui.xaml.controls.calendardatepicker.datechanged) event to be notified when the Date value has changed.

> [!NOTE]
> For important info about date values, see [DateTime and Calendar values](date-and-time.md#datetime-and-calendar-values) in the Date and time controls article.

### Setting a header and placeholder text

You can add a [Header](/uwp/api/windows.ui.xaml.controls.calendardatepicker.header) (or label) and [PlaceholderText](/uwp/api/windows.ui.xaml.controls.calendardatepicker.placeholdertext) (or watermark) to the calendar date picker to give the user an indication of what it's used for. To customize the look of the header, you can set the [HeaderTemplate](/uwp/api/windows.ui.xaml.controls.calendardatepicker.headertemplate) property instead of Header.

The default placeholder text is "select a date". You can remove this by setting the PlaceholderText property to an empty string, or you can provide custom text as shown here.

```xaml
<CalendarDatePicker x:Name="arrivalCalendarDatePicker" Header="Arrival date" 
                    PlaceholderText="Choose your arrival date"/>
```

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related articles

- [Date and time controls](date-and-time.md)
- [Calendar view](calendar-view.md)
- [Date picker](date-picker.md)
- [Time picker](time-picker.md)