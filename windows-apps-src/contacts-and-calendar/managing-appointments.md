---
description: Through the Windows.ApplicationModel.Appointments namespace, you can create and manage appointments in a user's calendar app.
title: Manage appointments
ms.assetid: 292E9249-07C3-4791-B32C-6EC153C2B538
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, appointments, calendar
ms.localizationpriority: medium
---
# Manage appointments



Through the [**Windows.ApplicationModel.Appointments**](/uwp/api/Windows.ApplicationModel.Appointments) namespace, you can create and manage appointments in a user's calendar app. Here, we'll show you how to create an appointment, add it to a calendar app, replace it in the calendar app, and remove it from the calendar app. We'll also show how to display a time span for a calendar app and create an appointment-recurrence object.

## Create an appointment and apply data to it

Create a [**Windows.ApplicationModel.Appointments.Appointment**](/uwp/api/Windows.ApplicationModel.Appointments.Appointment) object and assign it to a variable. Then, apply to the **Appointment** the appointment properties that were supplied through the UI by a user.

```cs
private void Create-Click(object sender, RoutedEventArgs e)
{
    bool isAppointmentValid = true;
    var appointment = new Windows.ApplicationModel.Appointments.Appointment();

    // StartTime
    var date = StartTimeDatePicker.Date;
    var time = StartTimeTimePicker.Time;
    var timeZoneOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
    var startTime = new DateTimeOffset(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0, timeZoneOffset);
    appointment.StartTime = startTime;

    // Subject
    appointment.Subject = SubjectTextBox.Text;

    if (appointment.Subject.Length > 255)
    {
        isAppointmentValid = false;
        ResultTextBlock.Text = "The subject cannot be greater than 255 characters.";
    }

    // Location
    appointment.Location = LocationTextBox.Text;

    if (appointment.Location.Length > 32768)
    {
        isAppointmentValid = false;
        ResultTextBlock.Text = "The location cannot be greater than 32,768 characters.";
    }

    // Details
    appointment.Details = DetailsTextBox.Text;

    if (appointment.Details.Length > 1073741823)
    {
        isAppointmentValid = false;
        ResultTextBlock.Text = "The details cannot be greater than 1,073,741,823 characters.";
    }

    // Duration
    if (DurationComboBox.SelectedIndex == 0)
    {
        // 30 minute duration is selected
        appointment.Duration = TimeSpan.FromMinutes(30);
    }
    else
    {
        // 1 hour duration is selected
        appointment.Duration = TimeSpan.FromHours(1);
    }

    // All Day
    appointment.AllDay = AllDayCheckBox.IsChecked.Value;

    // Reminder
    if (ReminderCheckBox.IsChecked.Value)
    {
        switch (ReminderComboBox.SelectedIndex)
        {
            case 0:
                appointment.Reminder = TimeSpan.FromMinutes(15);
                break;
            case 1:
                appointment.Reminder = TimeSpan.FromHours(1);
                break;
            case 2:
                appointment.Reminder = TimeSpan.FromDays(1);
                break;
        }
    }

    //Busy Status
    switch (BusyStatusComboBox.SelectedIndex)
    {
        case 0:
            appointment.BusyStatus = Windows.ApplicationModel.Appointments.AppointmentBusyStatus.Busy;
            break;
        case 1:
            appointment.BusyStatus = Windows.ApplicationModel.Appointments.AppointmentBusyStatus.Tentative;
            break;
        case 2:
            appointment.BusyStatus = Windows.ApplicationModel.Appointments.AppointmentBusyStatus.Free;
            break;
        case 3:
            appointment.BusyStatus = Windows.ApplicationModel.Appointments.AppointmentBusyStatus.OutOfOffice;
            break;
        case 4:
            appointment.BusyStatus = Windows.ApplicationModel.Appointments.AppointmentBusyStatus.WorkingElsewhere;
            break;
    }

    // Sensitivity
    switch (SensitivityComboBox.SelectedIndex)
    {
        case 0:
            appointment.Sensitivity = Windows.ApplicationModel.Appointments.AppointmentSensitivity.Public;
            break;
        case 1:
            appointment.Sensitivity = Windows.ApplicationModel.Appointments.AppointmentSensitivity.Private;
            break;
    }

    // Uri
    if (UriTextBox.Text.Length > 0)
    {
        try
        {
            appointment.Uri = new System.Uri(UriTextBox.Text);
        }
        catch (Exception)
        {
            isAppointmentValid = false;
            ResultTextBlock.Text = "The Uri provided is invalid.";
        }
    }

    // Organizer
    // Note: Organizer can only be set if there are no invitees added to this appointment.
    if (OrganizerRadioButton.IsChecked.Value)
    {
        var organizer = new Windows.ApplicationModel.Appointments.AppointmentOrganizer();

        // Organizer Display Name
        organizer.DisplayName = OrganizerDisplayNameTextBox.Text;

        if (organizer.DisplayName.Length > 256)
        {
            isAppointmentValid = false;
            ResultTextBlock.Text = "The organizer display name cannot be greater than 256 characters.";
        }
        else
        {
            // Organizer Address (for example, Email Address)
            organizer.Address = OrganizerAddressTextBox.Text;

            if (organizer.Address.Length > 321)
            {
                isAppointmentValid = false;
                ResultTextBlock.Text = "The organizer address cannot be greater than 321 characters.";
            }
            else if (organizer.Address.Length == 0)
            {
                isAppointmentValid = false;
                ResultTextBlock.Text = "The organizer address must be greater than 0 characters.";
            }
            else
            {
                appointment.Organizer = organizer;
            }
        }
    }

    // Invitees
    // Note: If the size of the Invitees list is not zero, then an Organizer cannot be set.
    if (InviteeRadioButton.IsChecked.Value)
    {
        var invitee = new Windows.ApplicationModel.Appointments.AppointmentInvitee();

        // Invitee Display Name
        invitee.DisplayName = InviteeDisplayNameTextBox.Text;

        if (invitee.DisplayName.Length > 256)
        {
            isAppointmentValid = false;
            ResultTextBlock.Text = "The invitee display name cannot be greater than 256 characters.";
        }
        else
        {
            // Invitee Address (for example, Email Address)
            invitee.Address = InviteeAddressTextBox.Text;

            if (invitee.Address.Length > 321)
            {
                isAppointmentValid = false;
                ResultTextBlock.Text = "The invitee address cannot be greater than 321 characters.";
            }
            else if (invitee.Address.Length == 0)
            {
                isAppointmentValid = false;
                ResultTextBlock.Text = "The invitee address must be greater than 0 characters.";
            }
            else
            {
                // Invitee Role
                switch (RoleComboBox.SelectedIndex)
                {
                    case 0:
                        invitee.Role = Windows.ApplicationModel.Appointments.AppointmentParticipantRole.RequiredAttendee;
                        break;
                    case 1:
                        invitee.Role = Windows.ApplicationModel.Appointments.AppointmentParticipantRole.OptionalAttendee;
                        break;
                    case 2:
                        invitee.Role = Windows.ApplicationModel.Appointments.AppointmentParticipantRole.Resource;
                        break;
                }

                // Invitee Response
                switch (ResponseComboBox.SelectedIndex)
                {
                    case 0:
                        invitee.Response = Windows.ApplicationModel.Appointments.AppointmentParticipantResponse.None;
                        break;
                    case 1:
                        invitee.Response = Windows.ApplicationModel.Appointments.AppointmentParticipantResponse.Tentative;
                        break;
                    case 2:
                        invitee.Response = Windows.ApplicationModel.Appointments.AppointmentParticipantResponse.Accepted;
                        break;
                    case 3:
                        invitee.Response = Windows.ApplicationModel.Appointments.AppointmentParticipantResponse.Declined;
                        break;
                    case 4:
                        invitee.Response = Windows.ApplicationModel.Appointments.AppointmentParticipantResponse.Unknown;
                        break;
                }

                appointment.Invitees.Add(invitee);
            }
        }
    }

    if (isAppointmentValid)
    {
        ResultTextBlock.Text = "The appointment was created successfully and is valid.";
    }
}
```

## Add an appointment to the user's calendar

Create a [**Windows.ApplicationModel.Appointments.Appointment**](/uwp/api/Windows.ApplicationModel.Appointments.Appointment) object and assign it to a variable. Then, call the [**AppointmentManager.ShowAddAppointmentAsync(Appointment, Rect, Placement)**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showaddappointmentasync) method to show the default appointments provider add-appointment UI, to enable the user to add an appointment. If the user clicked **Add**, the sample prints the appointment identifier that **ShowAddAppointmentAsync** returned.

```cs
private async void Add-Click(object sender, RoutedEventArgs e)
{
    // Create an Appointment that should be added the user's appointments provider app.
    var appointment = new Windows.ApplicationModel.Appointments.Appointment();

    // Get the selection rect of the button pressed to add this appointment
    var rect = GetElementRect(sender as FrameworkElement);

    // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user's calendar.
    // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
    // An empty string return value indicates that the user canceled the operation before the appointment was added.
    String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
                           appointment, rect, Windows.UI.Popups.Placement.Default);
    if (appointmentId != String.Empty)
    {
        ResultTextBlock.Text = "Appointment Id: " + appointmentId;
    }
    else
    {
        ResultTextBlock.Text = "Appointment not added.";
    }
}
```

**Note**  For Windows Phone Store apps, [**ShowAddAppointment**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showaddappointmentasync) functions just like [**ShowEditNewAppointment**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showeditnewappointmentasync) in that the dialog displayed for adding the appointment is editable.

## Replace an appointment in the user's calendar

Create a [**Windows.ApplicationModel.Appointments.Appointment**](/uwp/api/Windows.ApplicationModel.Appointments.Appointment) object and assign it to a variable. Then, call the appropriate [**AppointmentManager.ShowReplaceAppointmentAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showreplaceappointmentasync) method to show the default appointments provider replace-appointment UI to enable the user to replace an appointment. The user also provides the appointment identifier that they want to replace. This identifier was returned from [**AppointmentManager.ShowAddAppointmentAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showaddappointmentasync). If the user clicked **Replace**, the sample prints that it updated that appointment identifier.

```cs
private async void Replace-Click(object sender, RoutedEventArgs e)
{
    // The appointment id argument for ReplaceAppointmentAsync is typically retrieved from AddAppointmentAsync and stored in app data.
    String appointmentIdOfAppointmentToReplace = AppointmentIdTextBox.Text;

    if (String.IsNullOrEmpty(appointmentIdOfAppointmentToReplace))
    {
        ResultTextBlock.Text = "The appointment id cannot be empty";
    }
    else
    {
        // The Appointment argument for ReplaceAppointmentAsync should contain all of the Appointment' s properties including those that may have changed.
        var appointment = new Windows.ApplicationModel.Appointments.Appointment();

        // Get the selection rect of the button pressed to replace this appointment
        var rect = GetElementRect(sender as FrameworkElement);

        // ReplaceAppointmentAsync returns an updated appointment id when the appointment was successfully replaced.
        // The updated id may or may not be the same as the original one retrieved from AddAppointmentAsync.
        // An optional instance start time can be provided to indicate that a specific instance on that date should be replaced
        // in the case of a recurring appointment.
        // If the appointment id returned is an empty string, that indicates that the appointment was not replaced.
        String updatedAppointmentId;
        if (InstanceStartDateCheckBox.IsChecked.Value)
        {
            // Replace a specific instance starting on the date provided.
            var instanceStartDate = InstanceStartDateDatePicker.Date;
            updatedAppointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowReplaceAppointmentAsync(
                                   appointmentIdOfAppointmentToReplace, appointment, rect, Windows.UI.Popups.Placement.Default, instanceStartDate);
        }
        else
        {
            // Replace an appointment that occurs only once or in the case of a recurring appointment, replace the entire series.
            updatedAppointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowReplaceAppointmentAsync(
                                   appointmentIdOfAppointmentToReplace, appointment, rect, Windows.UI.Popups.Placement.Default);
        }

        if (updatedAppointmentId != String.Empty)
        {
            ResultTextBlock.Text = "Updated Appointment Id: " + updatedAppointmentId;
        }
        else
        {
            ResultTextBlock.Text = "Appointment not replaced.";
        }
    }
}
```

## Remove an appointment from the user's calendar

Call the appropriate [**AppointmentManager.ShowRemoveAppointmentAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showremoveappointmentasync) method to show the default appointments provider remove-appointment UI, to enable the user to remove an appointment. The user also provides the appointment identifier that they want to remove. This identifier was returned from [**AppointmentManager.ShowAddAppointmentAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showaddappointmentasync). If the user clicked **Delete**, the sample prints that it removed the appointment specified by that appointment identifier.

```cs
private async void Remove-Click(object sender, RoutedEventArgs e)
{
    // The appointment id argument for ShowRemoveAppointmentAsync is typically retrieved from AddAppointmentAsync and stored in app data.
    String appointmentId = AppointmentIdTextBox.Text;

    // The appointment id cannot be null or empty.
    if (String.IsNullOrEmpty(appointmentId))
    {
        ResultTextBlock.Text = "The appointment id cannot be empty";
    }
    else
    {
        // Get the selection rect of the button pressed to remove this appointment
        var rect = GetElementRect(sender as FrameworkElement);

        // ShowRemoveAppointmentAsync returns a boolean indicating whether or not the appointment related to the appointment id given was removed.
        // An optional instance start time can be provided to indicate that a specific instance on that date should be removed
        // in the case of a recurring appointment.
        bool removed;
        if (InstanceStartDateCheckBox.IsChecked.Value)
        {
            // Remove a specific instance starting on the date provided.
            var instanceStartDate = InstanceStartDateDatePicker.Date;
            removed = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowRemoveAppointmentAsync(
                      appointmentId, rect, Windows.UI.Popups.Placement.Default, instanceStartDate);
        }
        else
        {
            // Remove an appointment that occurs only once or in the case of a recurring appointment, replace the entire series.
            removed = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowRemoveAppointmentAsync(
                      appointmentId, rect, Windows.UI.Popups.Placement.Default);
        }

        if (removed)
        {
            ResultTextBlock.Text = "Appointment removed";
        }
        else
        {
            ResultTextBlock.Text = "Appointment not removed";
        }
    }
}
```

## Show a time span for the appointments provider

Call the [**AppointmentManager.ShowTimeFrameAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showtimeframeasync) method to show a specific time span for the default appointments provider's primary UI if the user clicked **Show**. The sample prints that the default appointments provider appeared on screen.

```cs
private async void Show-Click(object sender, RoutedEventArgs e)
{
    var dateToShow = new DateTimeOffset(2015, 6, 12, 18, 32, 0, 0, TimeSpan.FromHours(-8));
    var duration = TimeSpan.FromHours(1);
    await Windows.ApplicationModel.Appointments.AppointmentManager.ShowTimeFrameAsync(dateToShow, duration);
    ResultTextBlock.Text = "The default appointments provider should have appeared on screen.";
}
```

## Create an appointment-recurrence object and apply data to it

Create an [**Windows.ApplicationModel.Appointments.AppointmentRecurrence**](/uwp/api/windows.applicationmodel.appointments.appointmentrecurrence) object and assign it to a variable. Then, apply to the **AppointmentRecurrence** the recurrence properties that were supplied through the UI by a user.

```cs
private void Create-Click(object sender, RoutedEventArgs e)
{
    bool isRecurrenceValid = true;
    var recurrence = new Windows.ApplicationModel.Appointments.AppointmentRecurrence();

    // Unit
    switch (UnitComboBox.SelectedIndex)
    {
        case 0:
            recurrence.Unit = Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.Daily;
            break;
        case 1:
            recurrence.Unit = Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.Weekly;
            break;
        case 2:
            recurrence.Unit = Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.Monthly;
            break;
        case 3:
            recurrence.Unit = Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.MonthlyOnDay;
            break;
        case 4:
            recurrence.Unit = Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.Yearly;
            break;
        case 5:
            recurrence.Unit = Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.YearlyOnDay;
            break;
    }

    // Occurrences
    // Note: Occurrences and Until properties are mutually exclusive.
    if (OccurrencesRadioButton.IsChecked.Value)
    {
        recurrence.Occurrences = (uint)OccurrencesSlider.Value;
    }

    // Until
    // Note: Until and Occurrences properties are mutually exclusive.
    if (UntilRadioButton.IsChecked.Value)
    {
        recurrence.Until = UntilDatePicker.Date;
    }

    // Interval
    recurrence.Interval = (uint)IntervalSlider.Value;

    // Week of the month
    switch (WeekOfMonthComboBox.SelectedIndex)
    {
        case 0:
            recurrence.WeekOfMonth = Windows.ApplicationModel.Appointments.AppointmentWeekOfMonth.First;
            break;
        case 1:
            recurrence.WeekOfMonth = Windows.ApplicationModel.Appointments.AppointmentWeekOfMonth.Second;
            break;
        case 2:
            recurrence.WeekOfMonth = Windows.ApplicationModel.Appointments.AppointmentWeekOfMonth.Third;
            break;
        case 3:
            recurrence.WeekOfMonth = Windows.ApplicationModel.Appointments.AppointmentWeekOfMonth.Fourth;
            break;
        case 4:
            recurrence.WeekOfMonth = Windows.ApplicationModel.Appointments.AppointmentWeekOfMonth.Last;
            break;
    }

    // Days of the Week
    // Note: For Weekly, MonthlyOnDay or YearlyOnDay recurrence unit values, at least one day must be specified.
    if (SundayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Sunday; }
    if (MondayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Monday; }
    if (TuesdayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Tuesday; }
    if (WednesdayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Wednesday; }
    if (ThursdayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Thursday; }
    if (FridayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Friday; }
    if (SaturdayCheckBox.IsChecked.Value) { recurrence.DaysOfWeek |= Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.Saturday; }

    if (((recurrence.Unit == Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.Weekly) ||
         (recurrence.Unit == Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.MonthlyOnDay) ||
         (recurrence.Unit == Windows.ApplicationModel.Appointments.AppointmentRecurrenceUnit.YearlyOnDay)) &&
        (recurrence.DaysOfWeek == Windows.ApplicationModel.Appointments.AppointmentDaysOfWeek.None))
    {
        isRecurrenceValid = false;
        ResultTextBlock.Text = "The recurrence specified is invalid. For Weekly, MonthlyOnDay or YearlyOnDay recurrence unit values, " +
                               "at least one day must be specified.";
    }

    // Month of the year
    recurrence.Month = (uint)MonthSlider.Value;

    // Day of the month
    recurrence.Day = (uint)DaySlider.Value;

    if (isRecurrenceValid)
    {
        ResultTextBlock.Text = "The recurrence specified was created successfully and is valid.";
    }
}
```

## Add a new editable appointment

[**ShowEditNewAppointmentAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showeditnewappointmentasync) works just like [**ShowAddAppointmentAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showaddappointmentasync) except that the dialog for adding the appointment is editable so that the user can modify the appointment data before saving it.

``` cs
private async void AddAndEdit-Click(object sender, RoutedEventArgs e)
{
    // Create an Appointment that should be added the user' s appointments provider app.
    var appointment = new Windows.ApplicationModel.Appointments.Appointment();

    appointment.StartTime = DateTime.Now + TimeSpan.FromDays(1);
    appointment.Duration = TimeSpan.FromHours(1);
    appointment.Location = "Meeting location";
    appointment.Subject = "Meeting subject";
    appointment.Details = "Meeting description";
    appointment.Reminder = TimeSpan.FromMinutes(15); // Remind me 15 minutes prior


    // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user' s calendar.
    // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
    // An empty string return value indicates that the user canceled the operation before the appointment was added.
    String appointmentId =
        await Windows.ApplicationModel.Appointments.AppointmentManager.ShowEditNewAppointmentAsync(appointment);

    if (appointmentId != String.Empty)
    {
        ResultTextBlock.Text = "Appointment Id: " + appointmentId;
    }
    else
    {
        ResultTextBlock.Text = "Appointment not added.";
    }
}
```

## Show appointment details

[**ShowAppointmentDetailsAsync**](/uwp/api/windows.applicationmodel.appointments.appointmentmanager.showappointmentdetailsasync) causes the system to show details for the specified appointment. An app that implements app calendars may choose to be activated to show details for appointments in calendars it owns. Otherwise, the system will show the appointment details. An overload of the method that accepts a start date argument is provided to show details for an instance of a recurring appointment.

```cs
private async void ShowAppointmentDetails-Click(object sender, RoutedEventArgs e)
{

    if (instanceStartTime == null)
    {
        await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAppointmentDetailsAsync(
            currentAppointment.LocalId);
    }
    else
    {
        // Specify a start time to show an instance of a recurring appointment
        await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAppointmentDetailsAsync(
            currentAppointment.LocalId, instanceStartTime);
    }

}
```

## Summary and next steps

Now you have a basic understanding of how to manage appointments. Download the [Universal Windows app samples](https://github.com/Microsoft/Windows-universal-samples) from GitHub to see more examples of how to manage appointments.

## Related topics

* [Appointments API sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Appointments)
 

 