---
title: People on Windows
description: Store your app's contacts in Windows so they appear in the People Widget, Windows Search, and Share, and learn which guide covers each task.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 10/19/2026
ms.localizationpriority: medium
keywords: people on windows, windows people, cross-device people, contacts integration, people widget, windows search contacts
---

# People on Windows

People on Windows lets apps with [package identity](../../desktop/modernize/package-identity-overview.md) store their contacts for people experiences across Windows. Those contacts can appear as suggestions in the People Widget, Windows Search, and the Share Sheet. See [How to share files in File Explorer on Windows](https://support.microsoft.com/windows/how-to-share-files-in-file-explorer-on-windows-dcf7d3dc-40f7-111a-0c9e-a8981c4bbc32) for more information about the Share Sheet.

Beyond storing contacts, your app can:

- Provide ranks so your top contacts appear in the Share Sheet suggestions row.
- Declare the communication actions it supports for a contact, such as messaging or calling.
- Record contact activities, which are interaction signals Windows uses to identify relevant contacts.

## Choose a guide

Start with **Contribute contacts to Windows**. The other guides build on it.

| Task | What you do | Guide |
|---|---|---|
| Contribute contacts | Create a user data account, store contacts, and configure access. | [Contribute contacts to Windows](people-contribute-contacts.md) |
| Share ranking | Provide ranks for contact suggestions. | [Rank contacts for Windows Share](people-rank-contacts.md) |
| Communication actions | Declare supported actions and handle activation. | [Enable communication actions for contacts](people-communication-actions.md) |
| Contact activities | Record interactions to help Windows identify relevant contacts. | [Record contact activities](people-contact-activities.md) |

## Related content

- [Integrate Share options in your Windows app](integrate-sharesheet-overview.md)
- [Appear in suggestions row](integrate-sharesheet-contacts.md)
- [Package identity overview](../../desktop/modernize/package-identity-overview.md)
- [Windows.ApplicationModel.Contacts namespace](/uwp/api/windows.applicationmodel.contacts)
