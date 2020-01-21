---
title: Respond to customer feedback
description: You can respond directly to feedback that your customers leave in Feedback Hub.
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 04983b80-2a18-4ace-93d3-e8c33c04bfb9
ms.localizationpriority: medium
---
# Respond to customer feedback

You can use the [Feedback report](feedback-report.md) to review the feedback that your Windows 10 customers have left about your app in Feedback Hub, and then respond directly to that feedback. You can post your responses in Feedback Hub for everyone to see (either as individual comments, or by updating the status of a piece of feedback and adding a description) to tell customers about new features or bug fixes, or to ask for more specific feedback on how to improve your app. You can also send your response as an email directly to the customer who left the feedback.

> [!TIP]
> You can encourage customers to leave feedback by using the Feedback API in the [Microsoft Store Services SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftStoreServicesSDK) to add a control that lets customers directly [launch Feedback Hub from your UWP app](../monetize/launch-feedback-hub-from-your-app.md). Keep in mind that any customer who has downloaded your app on a Windows 10 device that supports Feedback Hub has the ability to leave feedback for it directly through the Feedback Hub app. Because of this, you may see customer feedback in this report, even if you have not specifically requested feedback from within your app.

To provide a response to any piece of feedback, click the **Respond to feedback** link that appears by the piece of feedback in your **Feedback report**.

[Partner Center](https://partner.microsoft.com/dashboard) supports three options for responding to customers who provide feedback about your app. Regardless of which option you choose, keep in mind that there is a 1000-character limit for each response.

## Public comments in Feedback Hub

By default, the radio button for **Comment** is selected after you click **Respond to feedback**. To post a public response to the customer’s feedback, leave this button selected. Enter your comment in the box, and then click **Submit**.

The comment you entered will be displayed as a comment in the Feedback Hub, along with the comments submitted by other customers. Your publisher name and app name will be displayed with your comment to identify you as the developer. There is no limit on the number of comments you can write for a piece of feedback, but note that you can’t edit or delete comments after you submit them. The five most recent comments to a piece of feedback will be shown in your **Feedback report** (as well as in Feedback Hub). When there are more than five comments, you can click **Show all comments** to see all of them in Feedback Hub.


## Private responses via email

If you’d prefer not to post a public response, you can check the **Send comment as email** box to send a private response directly to the customer (if they have provided an email address and haven’t opted out of receiving responses via email). When you do so, Microsoft sends an email to the customer on your behalf. The email will contain their original feedback as well as the response you write.

After you check the **Send comment as email** box, enter your comment and then click **Submit**. Note that you must provide an email address in the **Support contact email** field when using this option. By default, we use the email address that you provided in your account contact info. If you prefer to use a different email address, you can update the **Support contact email** field to use a different one. The customer who receives your response will be able to reply directly to this email address.


## Public status updates and descriptions in Feedback Hub

A third option for a public response is to set the status on a piece of feedback to let your customers that you’re working on the issue, or have fixed it. When you update the status of a piece of feedback, it is displayed along with the feedback in the Feedback Hub.

To use this option, select the **Update status** radio button. Then select one of the following options:

- **Investigating**: You’re aware of an issue and you’re looking into it.
- **Working on it**: You’re in the process of fixing a problem or adding a requested feature.
- **Completed**: You’ve published an update to fix the issue or add the requested feature.

Along with updating the status, you can enter a comment to provide more info, such as an estimate for when you think it a problem will be fixed, or more info about the latest changes. This description will be displayed at the top of the list of comments (and the Feedback report will display the current status and description).

Using the **Update status** option allows you to change the status whenever you want (along with providing updated descriptions for each status change). Whenever you change the status of a piece of feedback, the status will be updated in Feedback Hub so that customers viewing your response will see the latest status.


## Guidelines for responses

No matter which method you use to respond to a customer’s feedback, you must follow these guidelines for all responses.
- Responses must be no longer than 1000 characters.
- You may not offer any type of compensation, including digital app items, to users for their public comments.
- Don’t include any marketing content or ads in your response. Remember, the person who left feedback is already your customer.
- Don’t promote other apps or services in your response.
- Your response should be directly related to the specific app and feedback.
- Don’t include any profane, aggressive, personal, or malicious comments in your response. Always be polite and keep in mind that happy customers will likely be your app’s biggest promoters.

> [!NOTE]
> Customers can report a developer to Microsoft if they receive an inappropriate feedback response. They can also opt out of receiving feedback responses by email.

Your relationship with your customers is your own. Microsoft doesn’t get involved in disputes between developers and customers. However, if you think that the content of a customer’s feedback on your product is inappropriate, please submit a [support ticket](https://developer.microsoft.com/windows/support).
