---
description: Learn how to enable your app's contacts to appear in the Windows Share Sheet suggestions row.
title: "Appear in suggestions row - integrate Windows Share"
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 10/19/2026
ms.localizationpriority: medium
---

# Appear in suggestions row - integrate Windows Share

The Windows Share Sheet can display suggested contacts in the **suggestions row**, so users can quickly share with their top contacts. By storing your app's contacts in Windows, you let those contacts appear as suggestions.

## How the suggestions row works

The suggestions row appears at the top of the Share Sheet and shows contacts the user is most likely to want to share with. Windows sources these suggestions from the **People on Windows** feature, which aggregates contact information from your app and other apps on the device.

## Enable contact suggestions for your app

To make your app's contacts appear in the suggestions row, your app needs [package identity](../../desktop/modernize/package-identity-overview.md). The high-level steps are:

1. Create a `UserDataAccount` for the People contract
2. Store your app's contacts in the Windows `ContactStore`
3. Add a `ContactAnnotation` with `SupportedOperations` set to `Share` and a `Rank` provider property for each top contact
4. Contacts surface as suggestions on the Share panel for relevant sharing scenarios

The `Rank` value is a non-negative integer, and lower values take precedence within your app's list. Assign a unique rank to each contact. Your app's ranks express priority within its own list; Windows uses internal logic based on usage to order contacts across apps. For details, see [Storing ranks for contacts](people-rank-contacts.md#storing-ranks-for-contacts).

## Learn more

For comprehensive guidance on implementing contact suggestions, see [People on Windows](cross-device-people-api.md).

The People on Windows guides cover:
- Creating a `UserDataAccount` for the People contract
- Storing contacts in the Windows `ContactStore`
- Required and optional contact fields
- Controlling access to your contacts with `ExplictReadAccessPackageFamilyNames` (note: this is the actual API spelling)
- Ranking contacts for the suggestions row (see [Rank contacts for Windows Share](people-rank-contacts.md))
- Communication actions and contact activities

## Related content

- [People on Windows (Cross-device People API)](cross-device-people-api.md)
- [Rank contacts for Windows Share](people-rank-contacts.md)
- [Share content from your app](integrate-sharesheet-send.md)
- [Receive content in your app](integrate-sharesheet-receive.md)
