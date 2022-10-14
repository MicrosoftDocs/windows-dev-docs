---
description: Manage your app's performance in the store through ratings, reviews, and responses
title: Ratings, reviews, and responses
ms.date: 05/16/2022
ms.topic: article
keywords: windows 10, uwp, price, available, discoverable, free trial, trials, trial, apps, release date
ms.localizationpriority: medium
---

# Ratings, reviews and responses

Users can rate and review any products they’ve installed in the Microsoft Store. This helps others decide what products they should get next. Reviews also help developers get feedback to improve their products. 

## Ratings

Users can rate a product on a scale of one to five stars. The rating score is calculated based on a product’s recent ratings to give users insight to the current product experience.

The total lifetime count of ratings includes all ratings submitted since the product was published to the Microsoft Store. It’s visible on the product page and in search results.

The total count of ratings and rating score are specific to a user’s country and type of device.

## Analyzing ratings and reviews

Analyze ratings and reviews with the Ratings and reviews report in Partner Center. This helps developers monitor the performance of their products over time. For more details, developers can also analyze ratings and reviews by country, device type, OS, and more.

## Ratings and reviews report

:::image type="content" source="../images/ratings-and-reviews-page.png" lightbox="../images/ratings-and-reviews-page.png" alt-text="A screenshot of the ratings and reviews report on Partner Center.":::

The Ratings and Reviews report in Partner Center lets you see how customers rated your app in the Store. You can view this data in Partner Center or download the report to view offline.

### Apply filters

Near the top of the page, you can select the time period for which you want to show reviews. The default selection is Lifetime, but you can choose to show reviews for Lifetime, Last 24 hours, Last 7 days, Last 1 month, Last 3 months, Last 6 months, or Last 12 months, or for a custom data range that you specify.

You can expand Filters to filter the reviews shown on this page by the following options. These filters will not apply to the Ratings breakdown and Average rating over time charts.

- Rating: By default reviews with all star ratings are checked, but you can check and uncheck specific ratings (from 1 to 5 stars) if you want to only see reviews associated with particular star ratings.
- Market: The default setting is All markets. You can choose a specific market if you want this page to only show ratings from customers in that market.
- Device type: The default filter is All devices. You can choose a specific device type if you want this page to only show ratings left by customers using that type of device.
- OS version: The default setting is All. You can choose a specific OS version if you want this page to only show ratings left by customers on that OS version.
- Review content: The default setting is Ratings with review content, which means that only ratings with review content will be shown. You can select All to show all ratings, even those that don't include any written review text. Note that the Ratings breakdown and Ratings over time charts will always show all reviews, regardless of your selection.
- Responses: The default setting is All. You can choose to filter the reviews to only show the reviews where you have responded to customers, or only those where you have not yet responded.

> [!TIP]
> If you don't see any ratings on the page, check to make sure your filters haven't excluded all of your ratings. For example, if you filter by a Target OS that your app doesn't support, you won't see any ratings.

### Ratings breakdown

The Rating breakdown chart shows:

- The star rating for the app.
- The total number of ratings of your app over the selected time period past 12 months.
- The total number of ratings for each star rating.
- The number of ratings for each type of rating (new or revised) per star rating over the selected time period past 12 months.
- New ratings are ratings that customers have submitted but haven't changed at all.
- Revised ratings are ratings that have been changed by the customer in any way, even just changing the text of the review.
- The total number of ratings for each type of rating (new or revised)
- Shows how customers rated the app in a given week. This can help you identify trends or determine if ratings were affected by updates or other factors.

> [!TIP]
> The app rating that a customer sees in the Store takes into account the customer’s market and device type, so it may differ from what you see in this report.

### Ratings over time

The Ratings over time chart shows:

- The star rating for the app.
- The total number of ratings of your app over the selected time period past 12 months.
- A timeline of the app ratings over the time period selected.
- The total number of ratings for each type of rating (ratings with or without reviews)
- Shows how customers rated the app in a given week. This can help you identify trends or determine if ratings were affected by updates or other factors.

> [!TIP]
> The app rating that a customer sees in the Store takes into account the customer’s market and device type, so it may differ from what you see in this report.

## Reviews

Reviews are a great way for users to share their experience with a product. Reviews are visible to everyone on the product page along with user’s name. If a review is edited, the latest review will be shown on the product page.

The Reviews section in Partner Center lets you see the reviews (comments) that customers entered when rating your app in the Store. You can view this data in Partner Center or download the report to view offline.

You can also respond to customer reviews directly from this page.

Each customer review contains:

- The title and review text provided by the customer.
- The date of the review.
- The name of the reviewer as it appears in the Microsoft Store.
- The OS version of the device which the customer was using when the review was left.
- The name of the device which the customer was using when the review was left.

Note that customers can leave a rating for your app without adding any comments, so you will typically see fewer reviews than ratings.

You can sort the reviews on the page by date and/or by rating, in ascending or descending order. Click the Sort by link to view options to sort by Date and/or Rating.

You can also use the search box to search for specific words or phrases in your app's reviews. Note that only the original review text written by the customer is searched, even if the review was written in a different language. Translated review text is not searched.

> [!NOTE]
> You may occasionally notice that reviews disappear from this report. This can happen because Microsoft removes reviews from the Store that are written by customers running certain pre-release and Insider builds of Windows 10 or Windows 11. We do this to reduce the possibility of a negative review that is caused by a problem in a pre-release Windows build. We may also remove reviews from the Store that have been identified as spam, inappropriate, offensive, or have other policy violations. We expect this action will result in a better customer experience.

### Translating reviews

By default, reviews that were not written in your preferred language are translated for you. If you prefer, review translation can be disabled by unchecking the Translate reviews checkbox at the upper right, above the list of reviews.

Please note that reviews are translated by an automatic translation system, and the resulting translation may not always be accurate. The original text is provided if you wish to compare it to the translation or translate it through some other means.

As noted above, when searching through your reviews, only the original text left by the customer is searched (and not any translated text), even if you have the Translate reviews box checked.

### Respond to customer reviews

You can use Partner Center to post responses to many of your customers' reviews. You can respond to reviews of your app to let customers know you’re listening to their feedback. With a review response, you can tell customers about the features you’ve added or bugs you’ve fixed based on their comments, or get more specific feedback on how to improve your app. Your responses will be displayed in the Microsoft Store for all customers to see.

To view your app's reviews and provide responses, find the app in Partner Center. In the left navigation menu, expand Analytics and then click Ratings and reviews to display the Reviews section. Select Reply to provide your response.

By default, your response will be posted in the Store, directly below the original customer review. These responses will be visible to any customer viewing the Store.

### Guidelines for responses

When responding to a customer's review, you must follow these guidelines. These apply to all responses, whether they are public or not.

- Responses can't be longer than 1000 characters.
- You may not offer any type of compensation, including digital app items, to users for changing the app rating. Remember, attempts to manipulate ratings are not permitted under the App Developer Agreement.
- Don’t include any marketing content or ads in your response. Remember, your reviewer is already your customer.
- Don’t promote other apps or services in your response.
- Your response must be directly related to the specific app and review. Duplicating the same response to a large number of users isn’t allowed if the canned response doesn’t address the same question.
- Don’t include any profane, aggressive, personal, or malicious comments in your response. Always be polite and keep in mind that happy customers will likely be your app’s biggest promoters.

Customers can report an inappropriate review response from a developer to Microsoft.  Microsoft retains the right to revoke a developer’s permission to send responses for any reason, including if your responses prompt an unusually high number of inappropriate response reports.

### Reporting concerns

If a review has spam, advertising, profanity, or offensive content, find the review on the product page, and report it through Support.

## Use customer reviews to improve your app

Listening and responding to your customers is only the beginning. Acting on their feedback is also critical. Here are some additional actions you may wish to consider, based on the ratings and reviews you're seeing.

- If you notice many reviews that suggest a new or changed feature, or complain about a problem, consider releasing a new version that addresses the specific feedback. (Be sure to update your app's description to indicate that the issue has been fixed.)
- If the average rating is high, but your number of downloads is low, you might want to look for ways to expose your app to more people, since it's been well-received by those who have tried it out.
