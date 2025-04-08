---
description: A calendar view lets a user view and interact with a calendar that they can navigate by month, year, or decade.
title: Calendar view
ms.assetid: d8ec5ba8-7a9d-405d-a1a5-5a1b502b9e64
label: Calendar view
template: detail.hbs
ms.date: 02/26/2025
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Calendar view

A calendar view lets a user view and interact with a calendar that they can navigate by month, year, or decade. A user can select a single date or a range of dates. It doesn't have a picker surface and the calendar is always visible.

## Is this the right control?

Use a calendar view to let a user pick a single date or a range of dates from an always visible calendar.

If you need to let a user select multiple dates at one time, you must use a calendar view. If you need to let a user pick only a single date and don't need a calendar to be always visible, consider using a [calendar date picker](calendar-date-picker.md) or [date picker](date-picker.md) control.

For more info about choosing the right control, see the [Date and time controls](date-and-time.md) article.

## Examples

The calendar view is made up of 3 separate views: the month view, year view, and decade view. By default, it starts with the month view open. You can specify a startup view by setting the [DisplayMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.displaymode) property.

![The 3 views of a calendar view](images/calendar-view-3-views.png)

Users click the header in the month view to open the year view, and click the header in the year view to open the decade view. Users pick a year in the decade view to return to the year view, and pick a month in the year view to return to the month view. The two arrows to the side of the header navigate forward or backward by month, by year, or by decade.

## Create a calendar view

> [!div class="checklist"]
>
> - **Important APIs:** [CalendarView class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.CalendarView), [SelectedDatesChanged event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selecteddateschanged)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the CalendarView in action](winui3gallery:/item/CalendarView)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

This example shows how to create a simple calendar view.

```xaml
<CalendarView/>
```

The resulting calendar view looks like this:

![Example of calendar view](images/controls-calendar-monthview.png)

### Selecting dates

By default, the [SelectionMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selectionmode) property is set to **Single**. This lets a user pick a single date in the calendar. Set SelectionMode to **None** to disable date selection.

Set SelectionMode to **Multiple** to let a user select multiple dates. You can select multiple dates programmatically by adding [DateTime](/dotnet/api/system.datetime)/[DateTimeOffset](/dotnet/api/system.datetimeoffset) objects to the [SelectedDates](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selecteddates) collection, as shown here:

```csharp
calendarView1.SelectedDates.Add(DateTimeOffset.Now);
calendarView1.SelectedDates.Add(new DateTime(1977, 1, 5));
```

A user can deselect a selected date by clicking or tapping it in the calendar grid.

You can handle the [SelectedDatesChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selecteddateschanged) event to be notified when the [SelectedDates](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selecteddates) collection has changed.

> [!NOTE]
> For important info about date values, see [DateTime and Calendar values](date-and-time.md#datetime-and-calendar-values) in the Date and time controls article.

### Customizing the calendar view's appearance

The calendar view is composed of both XAML elements defined in the ControlTemplate and visual elements rendered directly by the control.

- The XAML elements defined in the control template include the border that encloses the control, the header, previous and next buttons, and DayOfWeek elements. You can style and re-template these elements like any XAML control.
- The calendar grid is composed of [CalendarViewDayItem](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.CalendarViewDayItem) objects. You can't style or re-template these elements, but various properties are provided to let you to customize their appearance.

This diagram shows the elements that make up the month view of the calendar. For more info, see the Remarks on the [CalendarViewDayItem](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.CalendarViewDayItem) class.

![The elements of a calendar month view](images/calendar-view-month-elements.png)

This table lists the properties you can change to modify the appearance of calendar elements.

Element | Properties
--------|-----------
DayOfWeek | [DayOfWeekFormat](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.dayofweekformat)
CalendarItem | [CalendarItemBackground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.calendaritembackground), [CalendarItemBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.calendaritemborderbrush), [CalendarItemBorderThickness](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.calendaritemborderthickness), [CalendarItemForeground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.calendaritemforeground)
DayItem | [DayItemFontFamily](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.dayitemfontfamily), [DayItemFontSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.dayitemfontsize), [DayItemFontStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.dayitemfontstyle), [DayItemFontWeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.dayitemfontweight), [HorizontalDayItemAlignment](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.horizontaldayitemalignment), [VerticalDayItemAlignment](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.verticaldayitemalignment), [CalendarViewDayItemStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.calendarviewdayitemstyle)
MonthYearItem (in the year and decade views, equivalent to DayItem) | [MonthYearItemFontFamily](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.monthyearitemfontfamily), [MonthYearItemFontSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.monthyearitemfontsize), [MonthYearItemFontStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.monthyearitemfontstyle), [MonthYearItemFontWeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.monthyearitemfontweight)
FirstOfMonthLabel | [FirstOfMonthLabelFontFamily](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofmonthlabelfontfamily), [FirstOfMonthLabelFontSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofmonthlabelfontsize), [FirstOfMonthLabelFontStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofmonthlabelfontstyle), [FirstOfMonthLabelFontWeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofmonthlabelfontweight), [HorizontalFirstOfMonthLabelAlignment](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.horizontalfirstofmonthlabelalignment), [VerticalFirstOfMonthLabelAlignment](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.verticalfirstofmonthlabelalignment), [IsGroupLabelVisible](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.isgrouplabelvisible)
FirstofYearDecadeLabel (in the year and decade views, equivalent to FirstOfMonthLabel) | [FirstOfYearDecadeLabelFontFamily](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofyeardecadelabelfontfamily), [FirstOfYearDecadeLabelFontSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofyeardecadelabelfontsize), [FirstOfYearDecadeLabelFontStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofyeardecadelabelfontstyle), [FirstOfYearDecadeLabelFontWeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.firstofyeardecadelabelfontweight)
Visual State Borders | [FocusBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.focusborderbrush), [HoverBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.hoverborderbrush), [PressedBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.pressedborderbrush), [SelectedBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selectedborderbrush), [SelectedForeground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selectedforeground), [SelectedHoverBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selectedhoverborderbrush), [SelectedPressedBorderBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.selectedpressedborderbrush)
OutofScope | [IsOutOfScopeEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.isoutofscopeenabled), [OutOfScopeBackground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.outofscopebackground), [OutOfScopeForeground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.outofscopeforeground)
Today | [IsTodayHighlighted](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.istodayhighlighted), [TodayFontWeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.todayfontweight), [TodayForeground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.todayforeground)

 By default, the month view shows 6 weeks at a time. You can change the number of weeks shown by setting the [NumberOfWeeksInView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.numberofweeksinview) property. The minimum number of weeks to show is 2; the maximum is 8.

By default, the year and decade views show in a 4x4 grid. To change the number of rows or columns, call [SetYearDecadeDisplayDimensions](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.setyeardecadedisplaydimensions) with the your desired number of rows and columns. This will change the grid for both the year and decade views.

Here, the year and decade views are set to show in a 3x4 grid.

```csharp
calendarView1.SetYearDecadeDisplayDimensions(3, 4);
```

By default, the minimum date shown in the calendar view is 100 years prior to the current date, and the maximum date shown is 100 years past the current date. You can change the minimum and maximum dates that the calendar shows by setting the [MinDate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.mindate) and [MaxDate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.maxdate) properties.

```csharp
calendarView1.MinDate = new DateTime(2000, 1, 1);
calendarView1.MaxDate = new DateTime(2099, 12, 31);
```

### Updating calendar day items

Each day in the calendar is represented by a [CalendarViewDayItem](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.CalendarViewDayItem) object. To access an individual day item and use its properties and methods, handle the [CalendarViewDayItemChanging](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarview.calendarviewdayitemchanging) event and use the Item property of the event args to access the CalendarViewDayItem.

You can make a day not selectable in the calendar view by setting its [CalendarViewDayItem.IsBlackout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarviewdayitem.isblackout) property to **true**.

You can show contextual information about the density of events in a day by calling the [CalendarViewDayItem.SetDensityColors](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.calendarviewdayitem.setdensitycolors) method. You can show from 0 to 10 density bars for each day, and set the color of each bar.

Here are some day items in a calendar. Days 1 and 2 are blacked out. Days 2, 3, and 4 have various density bars set.

![Calendar days with density bars](images/calendar-view-density-bars.png)

### Phased rendering

A calendar view can contain a large number of CalendarViewDayItem objects. To keep the UI responsive and enable smooth navigation through the calendar, calendar view supports phased rendering. This lets you break up processing of a day item into phases. If a day is moved out of view before all the phases are complete, no more time is used trying to process and render that item.

This example shows phased rendering of a calendar view for scheduling appointments.

- In phase 0, the default day item is rendered.
- In phase 1, you blackout dates that can't be booked. This includes past dates, Sundays, and dates that are already fully booked.
- In phase 2, you check each appointment that's booked for the day. You show a green density bar for each confirmed appointment and a blue density bar for each tentative appointment.

The `Bookings` class in this example is from a fictitious appointment booking app, and is not shown.

```xaml
<CalendarView CalendarViewDayItemChanging="CalendarView_CalendarViewDayItemChanging"/>
```

```csharp
private void CalendarView_CalendarViewDayItemChanging(CalendarView sender,
                                   CalendarViewDayItemChangingEventArgs args)
{
    // Render basic day items.
    if (args.Phase == 0)
    {
        // Register callback for next phase.
        args.RegisterUpdateCallback(CalendarView_CalendarViewDayItemChanging);
    }
    // Set blackout dates.
    else if (args.Phase == 1)
    {
        // Blackout dates in the past, Sundays, and dates that are fully booked.
        if (args.Item.Date < DateTimeOffset.Now ||
            args.Item.Date.DayOfWeek == DayOfWeek.Sunday ||
            Bookings.HasOpenings(args.Item.Date) == false)
        {
            args.Item.IsBlackout = true;
        }
        // Register callback for next phase.
        args.RegisterUpdateCallback(CalendarView_CalendarViewDayItemChanging);
    }
    // Set density bars.
    else if (args.Phase == 2)
    {
        // Avoid unnecessary processing.
        // You don't need to set bars on past dates or Sundays.
        if (args.Item.Date > DateTimeOffset.Now &&
            args.Item.Date.DayOfWeek != DayOfWeek.Sunday)
        {
            // Get bookings for the date being rendered.
            var currentBookings = Bookings.GetBookings(args.Item.Date);

            List<Color> densityColors = new List<Color>();
            // Set a density bar color for each of the days bookings.
            // It's assumed that there can't be more than 10 bookings in a day. Otherwise,
            // further processing is needed to fit within the max of 10 density bars.
            foreach (booking in currentBookings)
            {
                if (booking.IsConfirmed == true)
                {
                    densityColors.Add(Colors.Green);
                }
                else
                {
                    densityColors.Add(Colors.Blue);
                }
            }
            args.Item.SetDensityColors(densityColors);
        }
    }
}
```

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

APIs for this control exist in the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) namespace.

> [!div class="checklist"]
>
> - **UWP APIs:** [CalendarView class](/uwp/api/Windows.UI.Xaml.Controls.CalendarView), [SelectedDatesChanged event](/uwp/api/windows.ui.xaml.controls.calendarview.selecteddateschanged)
> - [Open the WinUI 2 Gallery app and see the CalendarView in action](winui2gallery:/item/CalendarView). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles and templates for all controls. WinUI 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md).

## Related articles

- [Date and time controls](date-and-time.md)
- [Calendar date picker](calendar-date-picker.md)
- [Date picker](date-picker.md)
- [Time picker](time-picker.md)