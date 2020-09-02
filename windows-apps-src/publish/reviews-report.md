---
Description: The Reviews report in Partner Center lets you see the reviews (comments) that customers entered when rating your app in the Store.
title: Reviews report
ms.assetid: E50C3A4D-1D8A-4E5B-8182-3FAD049F2A2D
ms.date: 08/16/2018
ms.topic: article
keywords: windows 10, uwp, review, comment, reviewer
ms.localizationpriority: medium
---
# Reviews report


The **Reviews** report in [Partner Center](https://partner.microsoft.com/dashboard) lets you see the reviews (comments) that customers entered when rating your app in the Store.

You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline. Alternatively, you can programmatically retrieve this data by using the [get app reviews](../monetize/get-app-reviews.md) method in the [Microsoft Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

You can also respond to customer reviews [directly from this page](respond-to-customer-reviews.md) or programmatically [via the Microsoft Store reviews API](../monetize/submit-responses-to-app-reviews.md).

> [!TIP]
> For a quick look at the reviews, ratings, and user feedback for all of your apps in the last 30 days, expand **Engage** in the left navigation menu and select **Reviews and feedback.** 


## Apply filters

Near the top of the page, you can select the time period for which you want to show reviews. The default selection is **Lifetime**, but you can choose to show reviews for 30 days, 3 months, 6 months, or 12 months, or for a custom data range that you specify.

You can expand **Filters** to filter the reviews shown on this page by the following options. These filters will not apply to the **Ratings breakdown** and **Average rating over time** charts.

-   **Rating**: By default reviews with all star ratings are checked, but you can check and uncheck specific ratings (from 1 to 5 stars) if you want to only see reviews associated with particular star ratings.
- **Review content**: The default setting is **Ratings with review content**, which means that only ratings with review content will be shown. You can select **All** to show all ratings, even those that don't include any written review text. Note that the **Ratings breakdown** chart will always show all reviews, regardless of your selection.
-   **OS version**: The default setting is **All**. You can choose a specific OS version if you want this page to only show reviews left by customers on that OS version.
-   **Package version**: The default setting is **All**. If your app includes more than one package, you can choose a specific one here to only show reviews left by customers who had that package when they reviewed your app.
-   **Responses**: The default setting is **All**. You can choose to filter the reviews to only show the reviews where you have [responded to customers](respond-to-customer-reviews.md), or only those where you have not yet responded.
-   **Updates**: The default setting is **All**. You can choose to filter the reviews to only show the reviews that have been updated by the customer since you [responded to a review](respond-to-customer-reviews.md), or only those which have not yet been updated by the customer.
-   **Market**: The default setting is **All markets**. You can choose a specific market if you want this page to only show reviews from customers in that market.
-   **Device type**: The default filter is **All devices**. You can choose a specific device type if you want this page to only show reviews left by customers using that type of device.
-   **Category name**: The default filter is **All**. You can choose a specific [review insight category](#review-insight-categories) to only show reviews that we’ve associated with that category. 

> [!TIP]
> If you don't see any reviews on the page, check to make sure your filters haven't excluded all of your reviews. For example, if you filter by a Target OS that your app doesn't support, you won't see any reviews.


## Ratings breakdown

The **Ratings breakdown** chart appears at the top of this report so that you can get a quick look at the following: 
- The average rating star rating for the app.
- The total number of ratings of your app over the past 12 months.
- The total number of ratings for each star rating.
- The number of ratings for each type of rating (new or revised) per star rating over the past 12 months.
 - **New ratings** are ratings that customers have submitted but haven't changed at all.
 - **Revised ratings** are ratings that have been changed by the customer in any way, even just changing the text of the review.

> [!TIP]
> The average rating that a customer sees in the Store takes into account the customer’s market and device type, so it may differ from what you see in this report.

Note that this chart always includes all of your reviews, even if you selected **Ratings with review content** in the **Review content** page filter.

This chart can also be seen in the [Ratings report](ratings-report.md), along with more details about your app's ratings.


<span id = "review-insight-categories" />

## Insight categories

The **Insight categories** chart groups your reviews according to categories that we've determined may be associated with the review.

> [!NOTE]
> Reviews less than 24 hours old and/or in a language other than English are not included when viewing reviews by categories.

Near the top of the page you will see colored blocks representing reviews by category. Select one of these categories to view only reviews that we've associated with that category. You can also use the [page filters](#apply-filters) to filter by category.

To see a breakdown of the number of reviews per category, select **Show details**. 


## Reviews

Each customer review contains:

-   The title and review text provided by the customer. (Reviews written by customers on Windows Phone 8.1 and earlier will not have a title.)
-   The date of the review.
-   The name of the reviewer as it appears in the Microsoft Store.
-   The reviewer's country/region.
-   The package version of the app on the customer's device at the time the review was left. (This info is not available for reviews submitted online or submitted by customers on Windows 8.1 and earlier.)
-   The OS version of the device which the customer was using when the review was left.
-   The name of the device which the customer was using when the review was left. (This info is not available for reviews submitted online or submitted by customers on Windows 8.1 and earlier.)
-   The review's "usefulness count," as rated by other customers when reading that review. These are shown as a series of two numbers: the first number shows how many customers rated it as useful, and the second number is the total number of customers who rated the review. For example, a usefulness count of 4/10 means that out of 10 raters, 4 found the review useful and 6 did not. (If there are no usefulness votes for a review, no usefulness count is displayed.)

Note that customers can leave a rating for your app without adding any comments, so you will typically see fewer reviews than ratings.

You can sort the reviews on the page by date and/or by rating, in ascending or descending order. Click the **Sort by** link to view options to sort by **Date** and/or **Rating**.

You can also use the search box to search for specific words or phrases in your app's reviews. Note that only the original review text written by the customer is searched, even if the review was written in a different language. Translated review text is not searched.

> [!NOTE]
> You may occasionally notice that reviews disappear from this report. This can happen because Microsoft removes reviews from the Store that are written by customers running certain pre-release and Insider builds of Windows 10. We do this to reduce the possibility of a negative review that is caused by a problem in a pre-release Windows build. We may also remove reviews from the Store that have been identified as spam, inappropriate, offensive or have other policy violations. We expect this action will result in a better customer experience.


## Translating reviews

By default, reviews that were not written in your preferred language are translated for you. If you prefer, review translation can be disabled by unchecking the **Translate reviews** checkbox at the upper right, above the list of reviews.

Please note that reviews are translated by an automatic translation system, and the resulting translation may not always be accurate. The original text is provided if you wish to compare it to the translation, or translate it through some other means.

As noted above, when searching your reviews, only the original text left by the customer is searched (and not any translated text), even if you have the **Translate reviews** box checked.


## Responding to customer reviews

You can use [Partner Center](https://partner.microsoft.com/dashboard) or the [Microsoft Store reviews API](../monetize/submit-responses-to-app-reviews.md) to send responses to many of your customers' reviews. For more info, see [Respond to customer reviews](respond-to-customer-reviews.md).

Here are some additional actions you may wish to consider, based on the ratings and reviews you're seeing.

-   If you notice many reviews that suggest a new or changed feature, or complain about a problem, consider releasing a new version that addresses the specific feedback. (Be sure to update your app's [description](./create-app-store-listings.md) to indicate that the issue has been fixed.)
-   If the average rating is high, but your number of downloads is low, you might want to look for ways to [expose your app to more people](attract-customers-and-promote-your-apps.md), since it's been well-received by those who have tried it out.


 

 

 