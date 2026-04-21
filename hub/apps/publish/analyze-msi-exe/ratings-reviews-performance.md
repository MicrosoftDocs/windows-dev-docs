---
description: Manage your app's performance in the store through ratings, reviews, and responses
title: Ratings, reviews, and responses
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, windows 11, uwp, msix, msi, exe, price, available, discoverable, free trial, trials, trial, apps, release date, reviews, ratings, responses, customer feedback
ms.localizationpriority: medium
---

# Ratings, reviews and responses

The new [**Ratings and reviews**](https://partner.microsoft.com/dashboard/insights/analytics/store/reviews) experience in Partner Center provides a streamlined way to analyze customer feedback for **Windows apps**, with a consistent and aligned set of insights available for **both Win32 (MSI/EXE) and MSIX apps**. 

Recent updates provide a unified set of experiences for both Win32 and MSIX, thus ensuring the same Ratings and reviews capabilities, layout, and analysis views are available regardless of app packaging. The experience brings together ratings breakdowns, trends over time, geographical distribution, and detailed customer reviews into a single view to help you understand customer sentiment and respond to feedback more effectively. 

### What’s included in the experience 

The Ratings and reviews dashboard presents a consolidated view of customer feedback across supported Windows app types. The experience is designed to help you move quickly from a high‑level view to detailed review data using the below insights: 

#### Ratings breakdown

:::image type="content" source="../images/rnr-ratings-breakdown.png" lightbox="../images/rnr-ratings-breakdown.png" alt-text="A screenshot showing ratings breakdown in Partner Center.":::

The **Ratings breakdown** chart shows a snapshot of overall customer sentiment, including: 

* The average star rating for the app
* The total number of ratings for the selected duration
* The total number of ratings for each star rating (1–5 stars)
* A breakdown of original and revised ratings for each star rating 

**Original ratings** are ratings that customers have submitted without making any changes and **Revised ratings** are ratings that customers have modified in any way, including changes only to the review text. 

> [!NOTE]
> The average rating shown to customers in the Microsoft Store takes into account the customer’s market and device type. As a result, it may differ from the average rating shown in Partner Center.

#### Ratings over time

:::image type="content" source="../images/rnr-ratings-over-time.png" lightbox="../images/rnr-ratings-over-time.png" alt-text="A screenshot showing ratings over time in Partner Center.":::

The **Ratings over time** section displays a time‑series chart that shows how your app’s ratings change over time. 

This chart shows two data series: 

* **Average rating** plotted over time, representing how the overall star rating of the app changes as customers submit ratings
* **Total ratings** plotted over the same time range, showing the volume of ratings received during each period 

The chart is displayed across a timeline and uses separate axes to represent average rating values and total rating counts. Together, these trends show how changes in rating volume relate to changes in overall customer sentiment over time.

#### Reviews

:::image type="content" source="../images/rnr-reviews.png" lightbox="../images/rnr-reviews.png" alt-text="A screenshot showing customer reviews in Partner Center.":::

Reviews allow customers to share their experience with your app and are visible publicly on the app product page in the Microsoft Store. If a review is edited, the most recent version is shown. 

The **Reviews** section in Partner Center lets you view the comments that customers submitted when rating your app. You can review this data directly in Partner Center or download it for offline analysis. 

Each review includes: 

* The star rating
* The review title and text provided by the customer
* The app package version installed on the customer’s device at the time the review was submitted
* The country or region of the reviewer
* The reviewer name as shown in the Microsoft Store
* The date the review was submitted 

> [!NOTE]
> Customers can submit a rating without adding a written review, so the total number of reviews is typically lower than the total number of ratings. 

You can: 

* Sort reviews by **newest, oldest, highest rating, lowest rating**, or **most helpful**
* Use the search box to find specific words or phrases within reviews. Only the **original review text** written by the customer is searched, even if the review was written in another language. Translated review text is not searched.

> [!NOTE]
> You may occasionally notice that reviews disappear from this report. This can happen because Microsoft removes reviews from the Store that are written by customers running certain pre-release and Insider builds of Windows 10 or Windows 11. We do this to reduce the possibility of a negative review that is caused by a problem in a pre-release Windows build. We may also remove reviews from the Store that have been identified as spam, inappropriate, offensive, or have other policy violations. We expect this action will result in a better customer experience.

#### Translating reviews

By default, reviews that are not written in your preferred language are shown in their original language. To view translated content, turn on the Translate reviews toggle in the Reviews toolbar, above the list of reviews.

When the Translate reviews toggle is turned on, reviews are translated using an automatic translation system. Because translations are generated automatically, they may not always be fully accurate. The original review text remains available so you can compare it with the translation or translate it using another method if needed.

When searching through your reviews, only the original text written by the customer is searched. Translated text is not included in search results, even when the Translate reviews toggle is turned on.

#### Respond to customer reviews

You can use Partner Center to post responses to many of your customers' reviews. You can respond to reviews of your app to let customers know you’re listening to their feedback. With a review response, you can tell customers about the features you’ve added or bugs you’ve fixed based on their comments, or get more specific feedback on how to improve your app. Your responses will be displayed in the Microsoft Store for all customers to see.

To provide responses, click on Reply next to the review you want to respond to. By default, your response will be posted in the Store, directly below the original customer review. These responses will be visible to any customer viewing the Store.

#### Guidelines for responses

When responding to a customer's review, you must follow these guidelines. These apply to all responses, whether they are public or not.

- Responses can't be longer than 1000 characters.
- You may not offer any type of compensation, including digital app items, to users for changing the app rating. Remember, attempts to manipulate ratings are not permitted under the [App Developer Agreement.](https://go.microsoft.com/fwlink/?linkid=528905)
- Don’t include any marketing content or ads in your response. Remember, your reviewer is already your customer.
- Don’t promote other apps or services in your response.
- Your response must be directly related to the specific app and review. Duplicating the same response to a large number of users isn’t allowed if the canned response doesn’t address the same question.
- Don’t include any profane, aggressive, personal, or malicious comments in your response. Always be polite and keep in mind that happy customers will likely be your app’s biggest promoters.

> [!IMPORTANT]
> You won’t be able to change the responses you post to the Store (unless the customer revises their original review), so review your response carefully. If a customer revises the original review, your response will be removed from the app's Store listing page. You can make a new reply to the customer's updated review.

Customers can report an inappropriate review response from a developer to Microsoft.  Microsoft retains the right to revoke a developer’s permission to send responses for any reason, including if your responses prompt an unusually high number of inappropriate response reports.

#### Reporting concerns

If a review has spam, advertising, profanity, or offensive content, find the review on the product page, and report it through [Support.](https://developer.microsoft.com/windows/support)

#### Private responses via email

If you’d prefer not to post a public response, you can uncheck the **Make this response public** box to send a private response directly to the customer (if they have provided an email address and haven’t opted out of receiving responses via email). When you do so, Microsoft sends an email to the customer on your behalf. The email will contain their original feedback as well as the response you write.

After you uncheck the **Make this response public** box, enter your comment and then click **Send reply**. Note that you must provide an email address in the **Support contact email** field when using this option. By default, we use the email address that you provided in your account contact info. If you prefer to use a different email address, you can update the **Support contact email** field to use a different one. The customer who receives your response will be able to reply directly to this email address.

#### Geographical spread

:::image type="content" source="../images/rnr-geographical-spread.png" lightbox="../images/rnr-geographical-spread.png" alt-text="A screenshot showing geographical spread of reviews in Partner Center.":::

The **Geographical spread** section displays a tabular view of ratings and reviews grouped by country or region. 

This table shows the following information for each market: 

* **Country or region** where ratings and reviews were submitted
* **Average rating** for that market
* **Total ratings**, representing the number of ratings received from that market
* **Total reviews**, representing the number of written reviews submitted 

The table is sortable and paginated, allowing you to browse, search by country, and compare rating volume and average ratings across different markets.

#### Apply filters

Near the top of the page, you can select the time period for which you want to show reviews. The default selection is Lifetime, but you can choose to show reviews for Lifetime, Last 1 month, Last 3 months, Last 6 months, or Last 12 months, or for a custom date range that you specify.

You can expand Filters to filter the reviews shown on this page by the following options. These filters will not apply to the Ratings breakdown and Average rating over time charts.

* Market: The default setting is All markets. You can choose a specific market if you want this page to only show ratings from customers in that market.
* Device type: The default filter is All devices. You can choose a specific device type if you want this page to only show ratings left by customers using that type of device.
* Package Version: You can choose a specific package or version if you want this page to only show ratings and reviews submitted for that package version of your app.
* OS version: The default setting is All. You can choose a specific OS version if you want this page to only show ratings left by customers on that OS version.
* Source (New): You can choose a specific source if you want this page to only show ratings and reviews submitted from that source (All, Store or InApp).
* Rating: By default reviews with all star ratings are checked, but you can check and uncheck specific ratings (from 1 to 5 stars) if you want to only see reviews associated with particular star ratings.
* Review content: The default setting is Ratings with review content, which means that only ratings with review content will be shown. You can select All to show all ratings, even those that don't include any written review text. Note that the Ratings breakdown and Ratings over time charts will always show all reviews, regardless of your selection.
* Responses: The default setting is All. You can choose to filter the reviews to only show the reviews where you have responded to customers, or only those where you have not yet responded.
* Updates: You can choose to filter reviews based on whether they were submitted before or after an app update, helping you understand how specific changes may have affected customer feedback.

#### Filter reviews by source

:::image type="content" source="../images/rnr-filter-by-source.png" lightbox="../images/rnr-filter-by-source.png" alt-text="A screenshot showing source filter for reviews in Partner Center.":::

The updated Ratings and reviews experience introduces a review source filter, a key new capability that helps you understand where customer reviews originate. 

You can filter reviews by the following sources: 

* **All sources** – View a combined set of all reviews
* **In‑app reviews** – Reviews submitted directly from within the app experience
* **Store reviews** – Reviews submitted on the Microsoft Store 

Using the review source filter, you can:  

* Analyze ratings and reviews by source
* Compare sentiment between in‑app and Store reviews
* Identify whether feedback patterns are specific to a particular source or consistent across sources
* Combine review source with other filters, such as date range, market, OS version, and package version, for more targeted analysis 

This filter is useful when you want to understand how customer feedback differs based on how and where users submit reviews. 

### Use customer ratings and reviews to improve your app

Listening and responding to your customers is only the beginning. Acting on their feedback is also critical. Here are some additional actions you may wish to consider, based on the ratings and reviews you're seeing.

- If you notice many reviews that suggest a new or changed feature, or complain about a problem, consider releasing a new version that addresses the specific feedback. (Be sure to update your app's description to indicate that the issue has been fixed.)
- If the average rating is high, but your number of downloads is low, you might want to look for ways to expose your app to more people, since it's been well-received by those who have tried it out.
