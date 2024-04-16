---
description: See ways to let users access their contacts and appointments to share content, email, calendar information, or messages.
title: Contacts and calendar
ms.assetid: b7e53ab5-2828-4fb7-8656-2bec70b3467f
ms.date: 05/18/2018
ms.topic: article
keywords: windows 10, uwp, contacts, calendar, appointments, email messages
ms.localizationpriority: medium
---

# Contacts, My People, and calendar

You can let your users access their contacts and appointments so they can share content, email, calendar info, or messages with each other, or whatever functionality you design.

To see a few different ways in which your app can access contacts and appointments, see these topics:

| Topic | Description |
|-------|-------------|
| [Select contacts](selecting-contacts.md) | Through the [<strong>Windows.ApplicationModel.Contacts</strong>](/uwp/api/Windows.ApplicationModel.Contacts) namespace, you have several options for selecting contacts. Here, we'll show you how to select a single contact or multiple contacts, and we'll show you how to configure the contact picker to retrieve only the contact information that your app needs. |
| [Send email](sending-email.md) | Shows how to launch the compose email dialog to allow the user to send an email message. You can pre-populate the fields of the email with data before showing the dialog. The message will not be sent until the user taps the send button. |
| [Send an SMS message](sending-an-sms-message.md) | This topic shows you how to launch the compose SMS dialog to allow the user to send an SMS message. You can pre-populate the fields of the SMS with data before showing the dialog. The message will not be sent until the user taps the send button. |
| [Manage appointments](managing-appointments.md) | Through the [<strong>Windows.ApplicationModel.Appointments</strong>](/uwp/api/Windows.ApplicationModel.Appointments) namespace, you can create and manage appointments in a user's calendar app. Here, we'll show you how to create an appointment, add it to a calendar app, replace it in the calendar app, and remove it from the calendar app. We'll also show how to display a time span for a calendar app and create an appointment-recurrence object. |
| [Connect your app to actions on a contact card](integrating-with-contacts.md) | Shows how to make your app appear next to actions on a contact card or mini contact card. Users can choose your app to perform an action such as open a profile page, place a call, or send a message. |
| [Adding My People support to an application](my-people-support.md) | Shows how to add My People support to an application and how to pin and unpin contacts on the taskbar. |
| [My People sharing](my-people-sharing.md) | Shows how to add support for My People sharing, which lets users share content with their pinned contacts by dragging files from the File Explorer to a My People pin. |
| [My People notifications](my-people-notifications.md) | Shows how to create and use My People notifications, a new kind of toast notification that's sent from a pinned contact. |
| [Third party contacts for Windows People Experiences](/windows/apps/develop/contacts-and-calendar/cross-device-people-api) | Shows how third party packaged apps can integrate with Windows People Experiences by leveraging Windows APIs to store their contacts. |

## Related topics

* [Appointments API sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Appointments)
* [Contact manager API sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/Contact%20manager%20API%20sample)
* [Contact Picker app sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/ContactPicker)
* [Handling Contact Actions sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/Windows%208.1%20Store%20app%20samples/99866-Windows%208.1%20Store%20app%20samples/Handling%20Contact%20Actions)
* [Contact Card Integration Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ContactCardIntegration)