---
author: muhsinking
Description: The time picker gives you a standardized way to let users pick a time value using touch, mouse, or keyboard input.
title: Time picker
ms.assetid: 5124ecda-09e6-449e-9d4a-d969dca46aa3
label: Time picker
template: detail.hbs
ms.author: mukin
ms.date: 05/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: kisai
design-contact: ksulliv
dev-contact: joyate
doc-status: Published
localizationpriority: medium
---
# Time picker
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css"> 

The time picker gives you a standardized way to let users pick a time value using touch, mouse, or keyboard input. 

> **Important APIs**: [TimePicker class](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.timepicker.aspx), [Time property](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.timepicker.time.aspx)


## Is this the right control?
Use a time picker to let a user pick a single time value.

For more info about choosing the right control, see the [Date and time controls](date-and-time.md) article.

## Examples

The entry point displays the chosen time, and when the user selects the entry point, a picker surface expands vertically from the middle for the user to make a selection. The time picker overlays other UI; it doesn't push other UI out of the way.

![Example of the time picker expanding](images/controls_timepicker_expand.png)

## Create a time picker

This example shows how to create a simple time picker with a header.

```xaml
<TimePicker x:Name=arrivalTimePicker Header="Arrival time"/>
```

```csharp
TimePicker arrivalTimePicker = new TimePicker();
arrivalTimePicker.Header = "Arrival time";
```

The resulting time picker looks like this:

![Example of time picker](images/time-picker-closed.png)

> [!NOTE]
> For important info about date and time values, see [DateTime and Calendar values](date-and-time.md#datetime-and-calendar-values) in the *Date and time controls* article.



## Related topics

- [Date and time controls](date-and-time.md)
- [Calendar date picker](calendar-date-picker.md)
- [Calendar view](calendar-view.md)
- [Date picker](date-picker.md)
