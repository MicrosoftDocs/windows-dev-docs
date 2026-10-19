---
title: Record contact activities
description: Record user interactions with contacts as activity signals so Windows can identify relevant contacts. Covers the payload, types, and lifecycle.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 10/19/2026
ms.localizationpriority: medium
keywords: contact activities, ActivityPayload, ContactAnnotation, interaction signals
---

# Record contact activities

Before you start, [contribute your contacts to Windows](people-contribute-contacts.md).

You can record contact activities to represent meaningful interactions between a user and a contact within your application. Contact activities act as lightweight interaction signals that help Windows understand how users engage with contacts across people-related experiences such as People Widgets, Share Sheet, and Windows Search Box in planned phases.

> [!NOTE]
> The [ContactAnnotationOperations](/uwp/api/windows.applicationmodel.contacts.contactannotationoperations) reference doesn't currently list a Windows or SDK version for the `Activity` value, and this article doesn't state which experiences use contact activities today. The version history in the reference lists `Share` (Windows 10 version 1703, SDK 15063) as the most recent documented value. Check the API reference for updates before you depend on `Activity` in an app that targets older SDK versions.

Each activity records three key pieces of information:

- **Who** — the contact involved in the interaction
- **What** — the type of interaction that occurred
- **When** — the time at which the interaction happened

To record a contact activity, an application must create a [ContactAnnotation](/uwp/api/windows.applicationmodel.contacts.contactannotation) associated with the contact and set `SupportedOperations` to `ContactAnnotationOperations.Activity`. Windows interprets annotations with this operation type as interaction signals associated with the contact.

Each activity represents a single interaction event and should be recorded separately. When submitting multiple contact activities, iterate through the interaction records and call `TrySaveAnnotationAsync` for each event. A loop doesn't provide a batch API or reduce synchronization overhead.

## Contact activity payload

To record a contact activity, set `ProviderProperties["ActivityPayload"]` on the [ContactAnnotation](/uwp/api/windows.applicationmodel.contacts.contactannotation) to a JSON string that describes the interaction.

The payload must include the following properties:

| Property | Description |
|---|---|
| `ActivityType` | Identifies the type of interaction. |
| `ActivityTimestampUnix` | Time when the interaction occurred, expressed as UTC Unix time in seconds. |

Use the key `ActivityPayload` and the payload property names `ActivityType` and `ActivityTimestampUnix` exactly as shown. These names form the string-based contract for describing the activity.

## Supported contact activity types

The `ActivityType` field identifies the interaction that occurred between the user and the contact. Windows currently supports the following activity types.

| ActivityType | Description |
|---|---|
| `message` | Sending or receiving a message with the contact. |
| `call` | Starting or answering an audio or video call. |
| `email` | Sending or receiving an email. |
| `meeting` | Joining or scheduling a meeting involving the contact. |
| `share` | Sharing content with the contact. |
| `social` | Interacting with the contact through social experiences. |
| `profileInteraction` | Opening or interacting with the contact's profile. |

Applications should select the activity type that best represents the user interaction that occurred within the application.

## Recording a contact activity example

The following example shows how to record a messaging interaction as a contact activity.

```csharp
long nowUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

var payload = new
{
    ActivityType = "message",
    ActivityTimestampUnix = nowUnix
};

var annotation = new ContactAnnotation
{
    ContactId = contact.Id,
    RemoteId = contact.RemoteId,
    SupportedOperations = ContactAnnotationOperations.Activity
};

annotation.ProviderProperties["ActivityPayload"] =
    JsonSerializer.Serialize(payload);

bool saved = await this.contactAnnotationList.TrySaveAnnotationAsync(annotation);
if (!saved)
{
    // The annotation wasn't saved, so Windows didn't receive this activity.
    // Log the failure or retry later. Don't treat the interaction as recorded.
    System.Diagnostics.Debug.WriteLine(
        $"Failed to save contact activity for contact {contact.RemoteId}.");
}
```

`TrySaveAnnotationAsync` returns `false` when it can't save the annotation, so check the result before you assume the activity was recorded. A `true` result means the annotation was saved. It doesn't indicate how Windows later processes the activity.

## Contact activities access model

Contact activities are write-only from your app's perspective. Your app can submit activity records, but Windows doesn't currently support retrieving or explicitly deleting previously submitted activity records. Windows-managed lifecycle cleanup is separate, as described in the next section.

If your app needs to retain or manage interaction history, keep it in your own storage or backend service.

## Contact activities lifecycle

Contact activities are associated with contacts using the application-provided `RemoteId`.

If a contact is removed or the associated `RemoteId` is deleted, the contact activities linked to that contact are automatically removed. Similarly, when an application is uninstalled or its associated `UserDataAccount` is removed, Windows automatically removes the contacts and contact activities recorded by that application.

## Contact activities processing and retention

Contact activities are processed by Windows as interaction signals.

The retention period, expiration (TTL), aggregation, and evaluation of contact activities are managed internally by Windows. Applications do not control how long activities are stored or how they are processed after submission.

Contact activities should represent genuine user interactions and should not be generated with the intent of influencing contact ranking or visibility. Windows determines how these signals contribute to people-related experiences using internal platform logic.

## Related content

- [ContactAnnotation](/uwp/api/windows.applicationmodel.contacts.contactannotation)
- [ContactAnnotationOperations](/uwp/api/windows.applicationmodel.contacts.contactannotationoperations)
- [ContactAnnotation.ProviderProperties](/uwp/api/windows.applicationmodel.contacts.contactannotation.providerproperties)
- [ContactAnnotationList.TrySaveAnnotationAsync](/uwp/api/windows.applicationmodel.contacts.contactannotationlist.trysaveannotationasync)
