---
Description: The Feedback report in Partner Center lets you see the problems, suggestions, and upvotes that your Windows 10 customers have submitted through Feedback Hub.
title: Feedback report
ms.assetid: 9EA8B456-CA57-40CE-A55B-7BFDC55CA8A8
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Feedback report

> [!WARNING]
> Feedback report deprecation on April 15th 2020
> This report will no longer be supported after April 15th 2020. Data on this report will not refresh after this date and the report will be removed in future without further notice. You can continue to view feedback received from your customers directly in the Feedback Hub.

The **Feedback report** in Partner Center lets you see the problems, suggestions, and upvotes that your Windows 10 customers have submitted through Feedback Hub. You can view this data in Partner Center, or export the data to view offline.

> [!NOTE]
> You can also [respond to feedback](respond-to-customer-feedback.md) directly from this report to let your customers know you're listening.

Encouraging your customers to give you feedback about your app is a great way to learn about the problems and features that are most important to them. When your customers know they can send you feedback directly, they may be less likely to leave that feedback as a negative review in the Store.

You can use the Feedback API in the [Microsoft Store Services SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftStoreServicesSDK) to let customers [directly launch Feedback Hub from your app](../monetize/launch-feedback-hub-from-your-app.md). Keep in mind that any customer who has downloaded your app on a Windows 10 device that supports Feedback Hub has the ability to leave feedback for it by using the Feedback Hub app. Because of this, you may see customer feedback in this report even if you have not specifically asked for feedback from within your app.

Feedback can also be helpful when using [package flighting](package-flights.md), since the **Feedback** report shows you the specific package that each customer had installed on their device when they left the feedback.

> [!TIP]
> For a quick look at the reviews, ratings, and user feedback across all of your apps in the last 30 days, expand **Engage** in the left navigation menu and select **Reviews and feedback.** 


## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **Lifetime**, but you can choose to show data for 30 days, 3 months, 6 months, or 12 months.

You can also expand **Filters** to filter all of the data on this page by the following options.

- **Feedback type**: The default setting is **All**. You can select **Problem** or **Suggestion** to show only that type of feedback.
- **Device type**: The default setting is **All devices**. You can choose a specific device type if you want this page to only show feedback left by customers using that type of device.
- **Package version**: The default setting is **All packages**. You can select one of your packages to show only feedback left from customers who were using that particular package when they left feedback.
- **Market**: The default setting is **All markets**. You can choose a specific to show only feedback from customers in that market.
- **Group**: The default setting is **All**. You can choose to view only feedback submitted by [Windows Insiders](https://insider.windows.com).

> [!TIP]
> If you don't see any feedback on the page, check to make sure your filters haven't excluded all of your feedback. For example, if you filter by a **Device type** that your app doesn't support, you won't see any feedback.


## Viewing feedback details

In this report, you’ll see the individual feedback left by your customers. To the left of the feedback text for each item, you’ll see the number of times the feedback was upvoted by other customers in the Feedback Hub. You can sort the feedback in three ways:

- **Upvoted** (default): Shows feedback that has been upvoted by other customers, starting with the feedback which received the most upvotes.
- **Trending**: Shows feedback that has been upvoted by other customers in the last seven days, starting with the feedback which has been getting the most recent activity.
- **Most recent**: Shows all feedback, starting with the feedback most recently left.

Next to each comment you’ll see the date on which the feedback was left, and the type of feedback. You’ll also see the customer’s market, the specific package that was installed on the device they were using when they left the feedback, the type of that device, and **Windows Insider** if the customer submitting the feedback is a member of the Windows Insider program.

You'll also see an option here to [respond to the feedback](respond-to-customer-feedback.md).


## Translating feedback

By default, feedback that was not written in your preferred language is translated for you. If you prefer, feedback translation can be disabled by unchecking the **Translate feedback** checkbox at the upper right, near the page filters.

Please note that feedback is translated by an automatic translation system, and the resulting translation may not always be accurate. The original text is provided if you wish to compare it to the translation, or translate it through some other means.


## Launching Feedback Hub directly from your app

As noted above, we recommend incorporating a link to Feedback Hub directly in your app to encourage customers to provide feedback. For more info, see [Launch Feedback Hub from your app](../monetize/launch-feedback-hub-from-your-app.md).
