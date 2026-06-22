---
title: People on Windows
description: Learn how to integrate your app with People on Windows - donate contacts, declare supported operations, and surface your app across the People Widget, Windows Search, and the Share Sheet.
ms.date: 05/27/2026
ms.topic: how-to
keywords: people on windows, windows people, cross-device people, contacts integration, people widget, windows search contacts
# customer intent: As a Windows developer, I want to learn how to integrate my app with Windows People experiences so that I can provide a seamless experience for my users.
---

# People on Windows

Windows is an ideal platform for third party apps to integrate their top people contacts. This integration enables users to interact with the personas for various people experiences. Windows now provides third party WinUI and other apps with [package identity](/windows/apps/desktop/modernize/package-identity-overview) with APIs to store all their contacts.

Once your apps store their contacts in Windows, users will be able to see these contact suggestions on the **Share** panel in Windows to seamlessly share with their top contacts. See [How to share files in File Explorer on Windows](https://support.microsoft.com/windows/how-to-share-files-in-file-explorer-on-windows-dcf7d3dc-40f7-111a-0c9e-a8981c4bbc32) for more information about the **Share** panel.

## Creating a UserDataAccount for People Contract

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

## Storing ranks for contacts

You can create an annotation list for the `UserDataAccount` to store ranks for your contacts. Apps can store ranks for their top contacts by adding annotations to the contacts. These annotations are stored as part of an annotation list in the contact store.

```csharp
ContactAnnotationStore annotationStore = await
    ContactManager.RequestAnnotationStoreAsync(ContactAnnotationStoreAccessType.AppAnnotationsReadWrite);
this.contactAnnotationList = await annotationStore.CreateAnnotationListAsync(uda.Id);
```

You can store ranks for your top contacts by using the annotations on contacts. Ranks are stored as part of [ProviderProperties](/uwp/api/windows.applicationmodel.contacts.contact.providerproperties) on a contact annotation. Along with rank, apps must set the [SupportedOperations](/uwp/api/windows.applicationmodel.contacts.contactannotation.supportedoperations) on a contact annotation as `Share`.

```csharp
foreach (var appContact in topAppContacts)
{
  Contact contact = await list.GetContactFromRemoteIdAsync(topAppContact.RemoteID);
  var annotation = new ContactAnnotation
  {
    ContactId = contact.Id,
    SupportedOperations = ContactAnnotationOperations.Share
  };
  annotation.ProviderProperties.Add("Rank", rank);
  await annotationsLst.TrySaveAnnotationAsync(annotation);
}
```

## Updating contact ranks

It's at the discretion of apps when to update the ranks of the contacts stored in Windows. Windows recommends that ranked lists be updated regularly to provide the best user experience. Whenever you need to update a ranked list, you'll need to follow several steps.

1. Delete the [ContactAnnotationList](/uwp/api/windows.applicationmodel.contacts.contactannotationlist).

   Once the app has an updated list of top contacts, the annotation list can be deleted and a new annotation list with updated annotations for their top contacts can be created.

   ```csharp
   await this.contactAnnotationList.DeleteAsync();
   ```

1. Create a new `ContactAnnotationList`. Follow the steps in the [Storing ranks for contacts](#storing-ranks-for-contacts) section to create a new annotation list and store ranks for your top contacts.

## Best practices for ranking

To maximize the relevance of your app's contacts in the Share Sheet suggestions row, follow these ranking principles:

### Rank by recency and frequency

Calculate rank as a combination of:
- **Recency**: When the user last interacted with each contact
- **Frequency**: How often the user interacts with each contact

For example, a contact the user messaged yesterday might have rank 95, while a contact messaged 2 weeks ago might have rank 60.

```csharp
// lastInteraction and interactionCount come from your app's own interaction
// telemetry. The Windows Contact class does not expose interaction history.
private int CalculateRank(DateTime lastInteraction, int interactionCount)
{
    TimeSpan daysSinceLastInteraction = DateTime.Now - lastInteraction;
    int frequencyScore = interactionCount * 10; // Max ~100
    int recencyScore = Math.Max(0, 100 - (daysSinceLastInteraction.Days * 3));

    return (recencyScore + frequencyScore) / 2;
}
```

### Remove stale contacts

Contacts the user hasn't interacted with in 30+ days should be removed or down-ranked significantly. This keeps suggestions fresh and relevant:

```csharp
private async Task PruneStaleContactsAsync()
{
    var now = DateTime.Now;
    var staleCutoff = now.AddDays(-30);
    
    foreach (var contact in this.AllTrackedContacts)
    {
        if (contact.LastInteractionDate < staleCutoff)
        {
            // Remove the annotation or set rank to 0
            var annotation = await GetAnnotationForContactAsync(contact);
            if (annotation != null)
            {
                annotation.ProviderProperties["Rank"] = 0;
                await this.contactAnnotationList.TrySaveAnnotationAsync(annotation);
            }
        }
    }
}
```

### Update ranks regularly

Schedule rank updates on a cadence that makes sense for your app - daily for messaging apps, weekly for email, or monthly for calendar apps. Use background tasks if necessary:

```csharp
// Example: Update ranks when the app comes to foreground or on a timer
private async void UpdateRanksOnAppActivated()
{
    var topContacts = GetTopContactsByRecentActivity(50);
    await UpdateAnnotationRanksAsync(topContacts);
}
```

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

## Integration with the Share Sheet

When you rank and maintain your contacts with the guidance above, users will see your app's top contacts in the **suggestions row** of the Windows Share Sheet. This path currently supports People contacts.

To ensure your contacts appear:
1. Create a `UserDataAccount` with `DisplayName = "com.microsoft.peoplecontract"`
2. Store contacts with required fields (`FirstName`, `RemoteId`, `DisplayPicture`)
3. Create a `ContactAnnotationList` with ranks
4. Set `SupportedOperations = Share` on each annotation
5. Update ranks regularly based on recency and frequency
6. Prune stale contacts after 30+ days

See [Share content from your app](integrate-sharesheet-send.md) and [Receive content in your app](integrate-sharesheet-receive.md) for the complete Share Sheet integration guide.

## Related content

- [Share content from your app](integrate-sharesheet-send.md)
- [Receive content in your app](integrate-sharesheet-receive.md)
- [Integrate Share options in your Windows app](integrate-sharesheet-overview.md)
- [UserDataAccount](/uwp/api/windows.applicationmodel.userdataaccounts.userdataaccount)
- [ContactStore](/uwp/api/windows.applicationmodel.contacts.contactstore)
- [Windows.ApplicationModel.Contacts Namespace](/uwp/api/windows.applicationmodel.contacts)
