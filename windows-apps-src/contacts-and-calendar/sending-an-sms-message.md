---
description: This topic shows you how to launch the compose SMS dialog to allow the user to send an SMS message. You can pre-populate the fields of the SMS with data before showing the dialog. The message will not be sent until the user taps the send button.
title: Send an SMS message
ms.assetid: 4D7B509B-1CF0-4852-9691-E96D8352A4D6
keywords: contacts, SMS, send
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Send an SMS message

This topic shows you how to launch the compose SMS dialog to allow the user to send an SMS message. You can pre-populate the fields of the SMS with data before showing the dialog. The message will not be sent until the user taps the send button.

To call this code, declare the **chat**, **smsSend**, and **chatSystem** capabilities in your package manifest. These are [restricted capabilities](../packaging/app-capability-declarations.md#special-and-restricted-capabilities) but you can use them in your app. You need approval only if you intend to publish your app to the Store. See [Account types, locations, and fees](../publish/account-types-locations-and-fees.md).

## Launch the compose SMS dialog

Create a new [**ChatMessage**](/uwp/api/windows.applicationmodel.chat.chatmessage) object and set the data that you want to be pre-populated in the compose email dialog. Call [**ShowComposeSmsMessageAsync**](/uwp/api/windows.applicationmodel.chat.chatmessagemanager.showcomposesmsmessageasync) to show the dialog.

```cs
private async void ComposeSms(Windows.ApplicationModel.Contacts.Contact recipient,
    string messageBody,
    StorageFile attachmentFile,
    string mimeType)
{
    var chatMessage = new Windows.ApplicationModel.Chat.ChatMessage();
    chatMessage.Body = messageBody;

    if (attachmentFile != null)
    {
        var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

        var attachment = new Windows.ApplicationModel.Chat.ChatMessageAttachment(
            mimeType,
            stream);

        chatMessage.Attachments.Add(attachment);
    }

    var phone = recipient.Phones.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactPhone>();
    if (phone != null)
    {
        chatMessage.Recipients.Add(phone.Number);
    }
    await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatMessage);
}
```

You can use the following code to determine whether the device that is running your app is able to send SMS messages.

```csharp
if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.ApplicationModel.Chat"))
{
   // Call code here.
}
```

## Summary and next steps

This topic has shown you how to launch the compose SMS dialog. For information on selecting contacts to use as recipients for an SMS message, see [Select contacts](selecting-contacts.md). Download the [Universal Windows app samples](https://github.com/Microsoft/Windows-universal-samples) from GitHub to see more examples of how to send and receive SMS messages by using a background task.

## Related topics

* [Select contacts](selecting-contacts.md)