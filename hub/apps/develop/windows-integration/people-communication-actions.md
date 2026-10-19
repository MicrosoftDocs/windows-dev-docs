---
title: Enable communication actions for contacts
description: Declare the communication actions your app supports for a contact, such as messaging or calling, and handle protocol activation from Windows.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 10/19/2026
ms.localizationpriority: medium
keywords: contact annotation, supported operations, ms-voip-call, ms-ipmessaging, protocol activation
---

# Enable communication actions for contacts

Contact annotations describe the communication actions an app supports for a contact, such as viewing a profile, messaging, or calling. This article shows how to declare those capabilities and how Windows opens the relevant contact in your app.

Before you start, [contribute your contacts to Windows](people-contribute-contacts.md).

## Declare supported communication operations

Apps can declare the communication capabilities supported for a contact by using the [SupportedOperations](/uwp/api/windows.applicationmodel.contacts.contactannotation.supportedoperations) property on [ContactAnnotation](/uwp/api/windows.applicationmodel.contacts.contactannotation).

The `SupportedOperations` property specifies the types of communication actions that the app supports for interacting with a contact. These operations are defined by the [ContactAnnotationOperations](/uwp/api/windows.applicationmodel.contacts.contactannotationoperations) enumeration.

Because `ContactAnnotationOperations` is a flags enumeration, multiple operations can be combined using the binary OR (`|`) operator. This allows apps to indicate all supported communication capabilities for a contact in a single annotation. For more information about this property, see the API reference for [ContactAnnotation.SupportedOperations](/uwp/api/windows.applicationmodel.contacts.contactannotation.supportedoperations).

The following example shows how to declare the supported communication operations for a contact.

```csharp
var annotation = new ContactAnnotation
{
    ContactId = contact.Id,
    RemoteId = contact.RemoteId,
    SupportedOperations =
        ContactAnnotationOperations.ContactProfile |
        ContactAnnotationOperations.Message |
        ContactAnnotationOperations.AudioCall |
        ContactAnnotationOperations.VideoCall |
        ContactAnnotationOperations.SocialFeeds
};

await this.contactAnnotationList.TrySaveAnnotationAsync(annotation);
```

In this example, the annotation declares to Windows that the app supports viewing the contact profile, sending messages, making audio or video calls, and accessing social feed updates for the specified contact.

Apps should set the supported operations to accurately represent the interaction and communication capabilities available for the contact within the app.

## Handle communication activation

When users select a communication action from a Windows surface, Windows launches your app by using a registered URI protocol and passes the contact's `RemoteId` to the app.

The following URI schemes correspond to `ContactAnnotationOperations` values.

| Operation | URI scheme | `SupportedOperations` flag |
|---|---|---|
| Call | `ms-voip-call:` | `AudioCall` |
| Video | `ms-voip-video:` | `VideoCall` |
| Message | `ms-ipmessaging:` | `Message` |
| Profile | `ms-contact-profile:` | `ContactProfile` |

For example, if an app declares `AudioCall | VideoCall`, Windows can activate the app with a URI such as:

```text
ms-voip-video:<RemoteId>
```

`<RemoteId>` is the same `RemoteId` value you stored on the `ContactAnnotation`. Use it to locate the corresponding user or conversation in your app and start the requested experience.

Register URI schemes only for operations your app supports. Windows surfaces a communication action only when the matching protocol registration is available.

### Register the URI schemes

Declare each URI scheme in the package manifest as a `windows.protocol` extension. The following example registers protocols for profile viewing, messaging, audio calling, and video calling.

```xml
<Extensions>
  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="ms-contact-profile">
      <uap:DisplayName>TestProfileApp</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>

  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="ms-ipmessaging">
      <uap:DisplayName>TestMsgApp</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>

  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="ms-voip-video">
      <uap:DisplayName>TestVideoApp</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>

  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="ms-voip-call">
      <uap:DisplayName>TestCallApp</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>
</Extensions>
```

## Related content

- [ContactAnnotation.SupportedOperations](/uwp/api/windows.applicationmodel.contacts.contactannotation.supportedoperations)
- [ContactAnnotationOperations](/uwp/api/windows.applicationmodel.contacts.contactannotationoperations)
- [ContactAnnotation.RemoteId](/uwp/api/windows.applicationmodel.contacts.contactannotation.remoteid)
- [uap:Protocol schema reference](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol)
- [Handle URI activation](/windows/apps/develop/launch/handle-uri-activation)
