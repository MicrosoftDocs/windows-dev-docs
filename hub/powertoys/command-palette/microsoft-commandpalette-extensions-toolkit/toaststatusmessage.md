---
title: ToastStatusMessage Class
description: The ToastStatusMessage class is used to create and display toast notifications in the Windows notification center.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToastStatusMessage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ToastStatusMessage** class is used to create and display toast notifications in Windows. It provides a way to show status messages to users in a non-intrusive manner.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [ToastStatusMessage(StatusMessage)](toaststatusmessage_constructor.md#toaststatusmessagestatusmessage-constructor) | Creates a new instance of the **ToastStatusMessage** class with the specified status message. |
| [ToastStatusMessage(String)](toaststatusmessage_constructor.md#toaststatusmessagestring-constructor) | Creates a new instance of the **ToastStatusMessage** class with the specified message. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Duration | **Integer** | The duration for which the toast message will be displayed. |
| Message | [StatusMessage](statusmessage.md) | The status message to be displayed in the toast notification. |

## Methods

| Method | Description |
| :--- | :--- |
| [Show()](toaststatusmessage_show.md) | Displays the toast notification with the specified message. |
