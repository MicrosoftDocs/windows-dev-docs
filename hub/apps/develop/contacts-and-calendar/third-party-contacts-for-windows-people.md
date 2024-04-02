---
title: Third party contacts for Windows People Experiences
description: Explains how third party apps can integrate with Windows People Experiences with APIs to store all their contacts.
ms.date: 04/02/2024
ms.topic: article
keywords: windows 11, windows people experiences, contacts, share, people contract, winrt
---

# Third party contacts for Windows People Experiences

Windows is an ideal platform for many third parties to integrate their top people contacts. This enables users to interact with these personas for various people experiences. Windows now provides third party apps with APIs to store all their contacts.

> [!IMPORTANT]
> Some information relates to a prerelease product which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.

> [!NOTE]
> Apps can try this feature as soon as it is released to the [Windows Insider Program](https://www.microsoft.com/windowsinsider/) (Beta Channel) in Windows Update settings (see [Get started with the Windows Insider Program](/windows-insider/get-started) for more information).

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

## See also

- [UserDataAccount](/uwp/api/windows.applicationmodel.userdataaccounts.userdataaccount)
- [ContactStore](/uwp/api/windows.applicationmodel.contacts.contactstore)
- [Windows.ApplicationModel.Contacts Namespace](/uwp/api/windows.applicationmodel.contacts)
