---
title: Rank contacts for Windows Share
description: Provide ranks for your app's top contacts so Windows can show them in the Share Sheet suggestions row. Includes rank format and update guidance.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 10/19/2026
ms.localizationpriority: medium
keywords: share sheet, suggestions row, contact rank, ContactAnnotation, Rank
---

# Rank contacts for Windows Share

Apps can provide ranks for their top contacts by adding contact annotations for Windows Share. This article covers creating an annotation list, storing ranks with the `Share` operation, updating the ranked list, and ranking best practices.

Before you start, [contribute your contacts to Windows](people-contribute-contacts.md).

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
  await this.contactAnnotationList.TrySaveAnnotationAsync(annotation);
}
```

### Rank value reference

| Aspect | Guidance |
|--------|----------|
| Property key | `"Rank"` in the annotation's `ProviderProperties` dictionary |
| Value type | Non-negative integer. |
| Direction | Lower values take precedence within your app's list. A rank of 0 isn't "ignore"; it follows the same lower-value-first ordering. |
| Uniqueness | Assign a unique rank to each contact in your list. The behavior for duplicate ranks isn't specified. |
| Scope | Your app's ranks express priority within your own list only. Windows reads all `Share` annotations and uses internal logic based on usage to order contacts across apps. Reading all annotations is separate from how many suggestions Windows displays. |
| Who defines the value | Your app. Choose how to calculate it, for example from recency and frequency of interaction. |

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

Because lower rank values take precedence, sort your contacts by score from highest to lowest and assign ranks 1, 2, 3, and so on. For example, a contact the user messaged yesterday might get rank 1, while a contact messaged 2 weeks ago might get rank 5.

```csharp
// lastInteraction and interactionCount come from your app's own interaction
// telemetry. The Windows Contact class does not expose interaction history.
private int CalculateScore(DateTime lastInteraction, int interactionCount)
{
    TimeSpan daysSinceLastInteraction = DateTime.Now - lastInteraction;
    int frequencyScore = interactionCount * 10; // Max ~100
    int recencyScore = Math.Max(0, 100 - (daysSinceLastInteraction.Days * 3));

    return (recencyScore + frequencyScore) / 2;
}

// A higher score means a more relevant contact, so it gets a lower rank value.
// Assign unique ranks 1, 2, 3, ... in descending score order.
var rankedContacts = this.AllTrackedContacts
    .OrderByDescending(c => CalculateScore(c.LastInteractionDate, c.InteractionCount))
    .Select((c, index) => (Contact: c, Rank: index + 1));
```

### Remove stale contacts

Contacts the user hasn't interacted with in 30+ days should be removed from your ranked list. This keeps suggestions fresh and relevant:

```csharp
// Exclude stale contacts when you rebuild the ranked list. Don't save a rank of 0
// for them: lower values take precedence, so 0 is the highest priority.
var staleCutoff = DateTime.Now.AddDays(-30);
var activeContacts = this.AllTrackedContacts
    .Where(c => c.LastInteractionDate >= staleCutoff);
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

## Checklist for the suggestions row

When you rank and maintain your contacts with the guidance above, users will see your app's top contacts in the **suggestions row** of the Windows Share Sheet. This path currently supports People contacts.

To ensure your contacts appear:
1. Create a `UserDataAccount` with `DisplayName = "com.microsoft.peoplecontract"`
2. Store contacts with required fields (`FirstName`, `RemoteId`, `DisplayPicture`)
3. Create a `ContactAnnotationList` with ranks (lower values take precedence)
4. Set `SupportedOperations = Share` on each annotation
5. Update ranks regularly based on recency and frequency
6. Remove contacts the user hasn't interacted with in 30+ days from your ranked list

See [Share content from your app](integrate-sharesheet-send.md) and [Receive content in your app](integrate-sharesheet-receive.md) for the complete Share Sheet integration guide.

## Related content

- [Appear in suggestions row](integrate-sharesheet-contacts.md)
- [Contribute contacts to Windows](people-contribute-contacts.md)
- [Send content from your app](integrate-sharesheet-send.md)
- [Receive content in your app](integrate-sharesheet-receive.md)
- [ContactAnnotation](/uwp/api/windows.applicationmodel.contacts.contactannotation)
- [ContactAnnotationOperations](/uwp/api/windows.applicationmodel.contacts.contactannotationoperations)
