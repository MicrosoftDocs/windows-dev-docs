---
description: You can respond directly to reviews of your app to let customers know you’re listening to their feedback.
title: Respond to customer reviews and feedback
ms.assetid: 96AA2108-E793-4DD0-8CDA-0D115423C68D
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, responding, responses, review
ms.localizationpriority: medium
---
# Respond to customer reviews and feedback

## Respond to customer reviews

You can respond to reviews of your app to let customers know you’re listening to their feedback. With a review response, you can tell customers about the features you’ve added or bugs you’ve fixed based on their comments, or get more specific feedback on how to improve your app. Your responses will be displayed in the Microsoft Store for all Windows 10 and 11 customers to see. You can also choose to send your response by email to the customer (if they haven’t opted out and are using a device running Windows 10, version 1803 or later).

To view your app's reviews and provide responses, select Insights in [Partner Center](https://partner.microsoft.com/dashboard). In the left navigation menu, click **Reviews** found under **Apps and games** to display the [Reviews report](reviews-report.md). Select **Respond to review** to provide your response.

> [!TIP]
> In addition to using Partner Center to respond to reviews, you can respond to reviews [programmatically](/windows/uwp/monetize/submit-responses-to-app-reviews).

By default, your response will be posted in the Store, directly below the original customer review. These responses will be visible to any customer viewing the Store on a Windows 10 or Windows 11 device. If the customer who left the review is using a device running Windows 10, version 1803 or later, and they haven't opted out of receiving email responses, a copy of your response will also be sent to that customer by email.  You'll need to provide a valid email address in order to submit your response, which we will include in the email to the customer. They can then use this email address to contact you directly.

If you don't want your response to appear in the Store, and instead want to respond only via email to the customer, uncheck the **Make this response public** box. Note that you will not be able to uncheck this box if the customer has opted out of receiving email responses and/or if they are using a device that is not running Windows 10, version 1803 or later.

## Guidelines for responses

When responding to a customer's review, you must follow these guidelines. These apply to all responses, whether they are public or not.

> [!IMPORTANT]
> You won’t be able to change the responses you post to the Store (unless the customer revises their original review), so review your response carefully. If a customer revises the original review, your response will be removed from the app's  Store listing page. You then have the option to submit a new response to the revised review by selecting **Update your response**.

-   Responses can't be longer than 1000 characters.
-   You may not offer any type of compensation, including digital app items, to users for changing the app rating. Remember, attempts to manipulate ratings are not permitted under the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905).
-   Don’t include any marketing content or ads in your response. Remember, your reviewer is already your customer.
-   Don’t promote other apps or services in your response.
-   Your response must be directly related to the specific app and review. Duplicating the same response to a large number of users isn’t allowed if the canned response doesn’t address the same question.
-   Don’t include any profane, aggressive, personal, or malicious comments in your response. Always be polite and keep in mind that happy customers will likely be your app’s biggest promoters.

> [!NOTE]
> Customers can report an inappropriate review response from a developer to Microsoft. They can also opt out of receiving review responses by email.
>
> Microsoft retains the right to revoke a developer’s permission to send responses for any reason, including if your responses prompt an unusually high number of inappropriate response reports, or if they prompt an unusually high number of customers to opt out of receiving review responses.

Your relationship with your customers is your own. Microsoft doesn’t get involved in disputes between developers and customers. However, if a review of your app contains offensive, profane, or abusive language, please open a [support request](https://developer.microsoft.com/windows/support).


## Use customer reviews to improve your app

Listening and responding to your customers is only the beginning. Acting on their feedback is also critical. If you make significant improvements, showcase them in the Store with confidence by [creating a new submission](publish-your-app/create-app-submission.md?pivots=store-installer-msix) to update your app.

## Respond to customer feedback

You can use the [Feedback report](feedback-report.md) to review the feedback that your Windows 10 or Windows 11 customers have left about your app in Feedback Hub, and then respond directly to that feedback. You can post your responses in Feedback Hub for everyone to see (either as individual comments, or by updating the status of a piece of feedback and adding a description) to tell customers about new features or bug fixes, or to ask for more specific feedback on how to improve your app. You can also send your response as an email directly to the customer who left the feedback.

> [!TIP]
> You can encourage customers to leave feedback by using the Feedback API in the [Microsoft Store Services SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftStoreServicesSDK) to add a control that lets customers directly [launch Feedback Hub from your UWP app](/windows/uwp/monetize/launch-feedback-hub-from-your-app). Keep in mind that any customer who has downloaded your app on a Windows 10 or Windows 11 device that supports Feedback Hub has the ability to leave feedback for it directly through the Feedback Hub app. Because of this, you may see customer feedback in this report, even if you have not specifically requested feedback from within your app.

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
