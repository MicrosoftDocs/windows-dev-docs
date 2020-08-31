---
description: Shows how to launch the compose email dialog to allow the user to send an email message. You can pre-populate the fields of the email with data before showing the dialog. The message will not be sent until the user taps the send button.
title: Send email
ms.assetid: 74511E90-9438-430E-B2DE-24E196A111E5
keywords: contacts, email, send
ms.date: 10/11/2017
ms.topic: article


ms.localizationpriority: medium
---
# Send email

Shows how to launch the compose email dialog to allow the user to send an email message. You can pre-populate the fields of the email with data before showing the dialog. The message will not be sent until the user taps the send button.

**In this article**

-   [Launch the compose email dialog](#launch-the-compose-email-dialog)
-   [Summary and next steps](#summary-and-next-steps)
-   [Related topics](#related-topics)

## Launch the compose email dialog

Create a new [**EmailMessage**](/uwp/api/Windows.ApplicationModel.Email.EmailMessage) object and set the data that you want to be pre-populated in the compose email dialog. Call [**ShowComposeNewEmailAsync**](/uwp/api/windows.applicationmodel.email.emailmanager.showcomposenewemailasync) to show the dialog.

``` cs
private async Task ComposeEmail(Windows.ApplicationModel.Contacts.Contact recipient,
    string subject, string messageBody)
{
    var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
    emailMessage.Body = messageBody;

    var email = recipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
    if (email != null)
    {
        var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(email.Address);
        emailMessage.To.Add(emailRecipient);
        emailMessage.Subject = subject;
    }

    await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);
}
```

>[!NOTE]
> Attachments that you add to an email by using the [EmailAttachment](/uwp/api/windows.applicationmodel.email.emailattachment) class will appear only in the Mail app. If users have any other mail program configured as their default mail program, the compose window will appear without the attachment. This is a known issue.

## Summary and next steps

This topic has shown you how to launch the compose email dialog. For information on selecting contacts to use as recipients for an email message, see [Select contacts](selecting-contacts.md). See [**PickSingleFileAsync**](/uwp/api/windows.storage.pickers.fileopenpicker.picksinglefileasync) to select a file to use as an email attachment.

## Related topics

* [Selecting contacts](selecting-contacts.md)
 