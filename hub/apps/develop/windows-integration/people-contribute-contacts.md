---
title: Contribute contacts to Windows
description: Create a UserDataAccount, store your app's contacts, and control access so contacts can appear in the People Widget, Windows Search, and Share.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 10/19/2026
ms.localizationpriority: medium
keywords: people on windows, contacts, UserDataAccount, ContactStore, people contract
---

# Contribute contacts to Windows

Apps with [package identity](../../desktop/modernize/package-identity-overview.md) can store their contacts in Windows. These contacts can appear as suggestions in the People Widget, Windows Search, and the Share Sheet. This article shows how to create a user data account, store contacts, and configure access for Windows experiences.

## Create a UserDataAccount for the People contract

Start by creating a user data account. Third party apps are required to create a [UserDataAccount](/uwp/api/windows.applicationmodel.userdataaccounts.userdataaccount) with `UserDisplayName` as `"com.microsoft.peoplecontract"`.

```csharp
UserDataAccountStore udas =
    await UserDataAccountManager.RequestStoreAsync(UserDataAccountStoreAccessType.AppAccountsReadWrite);
UserDataAccount uda = await udas.CreateAccountAsync("com.microsoft.peoplecontract");
```

Next, add `"com.microsoft.windows.system"` to the list of [ExplictReadAccessPackageFamilyNames](/uwp/api/windows.applicationmodel.userdataaccounts.userdataaccount.explictreadaccesspackagefamilynames) for the account. This will provide restricted access of third party contacts to Windows experiences.

```csharp
uda.ExplictReadAccessPackageFamilyNames.Add("com.microsoft.windows.system");
await uda.SaveAsync();
```

## Storing contacts

The first step in storing contacts is to create a contact list. To do this, third party apps must create the new contact list for a `UserDataAccount` in the Windows [ContactStore](/uwp/api/windows.applicationmodel.contacts.contactstore). Apps can choose to keep the default `OtherAppReadAccess` access type for the contact list, while setting it to `None` will prevent other apps from having any access to these contacts. See the [ContactListOtherAppReadAccess](/uwp/api/windows.applicationmodel.contacts.contactlistotherappreadaccess) enum for the full list of available access types.

```csharp
ContactStore store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
this.contactList = await store.CreateContactListAsync(contactListsName, uda.Id);
contactList.OtherAppReadAccess = ContactListOtherAppReadAccess.None;
await contactList.SaveAsync();
```

While storing a contact, third party apps must include all relevant information required for Windows experiences to power a [Contact](/uwp/api/windows.applicationmodel.contacts.contact).

The following fields are required when storing a contact:

- `FirstName`
- `RemoteId`
- `DisplayPicture`

The following fields are optional:

- `LastName`
- `Phones`
- `Emails`

This code snippet demonstrates how to store a contact:

```csharp
foreach (var appContact in AppContacts)
{
  var cont = new Contact
  {
    FirstName = appContact.FirstName,
    LastName = appContact.LastName,
    RemoteId = appContact.Id,
    SourceDisplayPicture = RandomAccessStreamReference.CreateFromUri(new Uri(appContact.ProfilePicPath)),
    Phones = { new ContactPhone { Number = appContact.Phone } }
  };

  await this.contactList.SaveContactAsync(cont);
}
```

> [!NOTE]
> The `DisplayName` for the [Contact](/uwp/api/windows.applicationmodel.contacts.contact) is constructed using `FirstName` and `LastName`. If the last name is not provided, `DisplayName` will be identical to the string provided for the first name.

## Privacy and consent

### Only contribute explicit contacts

Your app should contribute only the contacts the user has explicitly added or authorized. **Never**:
- Upload the entire address book
- Auto-sync contacts without user consent
- Share contacts from corporate directories without explicit permission

Ask for permission before storing contacts in the People system:

```csharp
private async Task<bool> RequestContactStoreAccessAsync()
{
    // Requesting a writable ContactStore prompts the user for consent.
    // Your app must declare the contacts capability in its manifest.
    ContactStore store = await ContactManager.RequestStoreAsync(
        ContactStoreAccessType.AppContactsReadWrite);
    return store != null;
}
```

### Respect user privacy settings

Let users:
- Choose which contacts to share with Windows
- Opt out of the People integration
- Delete their contacts from Windows at any time

```csharp
// Provide a setting to disable sync
if (this.ShouldSyncContactsWithWindows)
{
    await SyncContactsAsync();
}
else
{
    // Clear contacts if the user disables sync
    await ClearWindowsContactsAsync();
}
```

## Related content

- [Rank contacts for Windows Share](people-rank-contacts.md)
- [Enable communication actions for contacts](people-communication-actions.md)
- [Record contact activities](people-contact-activities.md)
- [Windows.ApplicationModel.Contacts namespace](/uwp/api/windows.applicationmodel.contacts)
- [UserDataAccount](/uwp/api/windows.applicationmodel.userdataaccounts.userdataaccount)
- [ContactStore](/uwp/api/windows.applicationmodel.contacts.contactstore)
- [ContactConnectedServiceAccount](/uwp/api/windows.applicationmodel.contacts.contactconnectedserviceaccount)
- [ContactListOtherAppReadAccess](/uwp/api/windows.applicationmodel.contacts.contactlistotherappreadaccess)
