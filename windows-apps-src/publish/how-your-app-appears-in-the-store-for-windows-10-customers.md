---
author: jnHs
Description: If you have previously published apps to the Store for either Windows or Windows Phone, those apps will be made available to customers on Windows 10 devices as well.
title: How your app appears in the Store for Windows 10 customers
ms.assetid: 487BB57E-F547-4764-99B1-84FE396E6D3A
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# How your app appears in the Store for Windows 10 customers


If you have previously published apps to the Store for either Windows or Windows Phone, those apps will be made available to customers on Windows 10 devices as well. Since there are some changes in the way the Store presents and categorizes apps to customers running Windows 10, this topic will help you understand what may have changed.

**Note**  If you want to change any of these details, [create a new submission](app-submissions.md) and make your changes, then submit the update to the Store.

 

## Apps that shared identity in the Windows Store and Windows Phone Store


If you have used the same reserved name for an app published to both Stores (often referred to as sharing your app’s identity), these will now be considered one app, not two. In the dashboard, you'll see them as a single app with Windows and Windows Phone packages.

Most developers had set the same pricing and other properties for the app and any add-ons in each Store, but if some of these values were different, it's important to understand which ones are shown to your Windows 10 customers.

### Pricing
If you had chosen different base prices for your app (or add-on) in each Store, the base price from the Windows Store is used.

**Note**  If you had set per-market pricing in the Windows Phone Store, custom prices will also be shown to your Windows 10 customers.

### Free trials
Trial options were different in the two earlier Stores, so if you used them, it's possible that chose different options for each Store. The trial option available to your Windows 10 customers are determined based on the following table.

| Windows 8 app       | Windows Phone app   | Trial setting for Windows 10                                                  |
|---------------------|---------------------|-------------------------------------------------------------------------------|
| No free trial       | No free trial       | No free trial                                                                 |
| No free trial       | Trial never expires | No free trial                                                                 |
| Trial never expires | Trial never expires | Trial never expires                                                           |
| Trial never expires | No free trial       | No free trial                                                                 |
| Time-limited trial  | Trial never expires | No free trial on Windows Phone 8.1 and earlier; otherwise, time-limited trial |
| Time-limited trial  | No free trial       | No free trial on Windows Phone 8.1 and earlier; otherwise, time-limited trial |

### Markets
Your app will be available to Windows 10 customers in every market where you had previously published the app. This applies even if you had different market selections for each Store.

### Categories
If your app appeared in different categories in the two Stores, we'll use the category from the Windows Store to determine its new category. Note that some categories are different in the Store for Windows 10 customers, so be sure to review the [table](#category-changes) below.

### Age rating
If you provided different age ratings, the stricter (higher age) rating is used.

### Privacy policy
If your app has a privacy policy, the one you provided when submitting your Windows 8 app is shown to your Windows 10 customers as well.

### Screenshots
We take all of the screenshots you've submitted and use the appropriate version to display to Windows 10 customers, based on the type of device they're using. In the rare case where your supported languages differ for each Store, some customers might see a screenshot from another language that best represents the experience they will get when buying the app.

### Store listings
We try to show the most appropriate Store listing to your Windows 10 customers, based on their language. When Store listings are available from more than one source in the same language, the listing fron your Windows Store app is shown to your Windows 10 customers. In the rare cases where your supported languages differ for each Store, some customers might see a Store listing from your Windows Phone app, if that is the only Store listing you provided in that language.

If you want to update the Store listing that your Windows 10 customers see to let them know about experiences that work across multiple devices, you can do this by updating [your app's description](create-app-store-listings.md). Customers on Windows 10 will see your app's default description, but you can also [create platform-specific Store listings](create-platform-specific-store-listings.md) if you want your Store listing to appear differently for customers on different OS versions.

## Category changes


In many cases, the new [categories and subcategories](category-and-subcategory-table.md) for apps and games are the same as they have been in the Store for previous OS versions. However, there have been a few changes. Review the table below to understand how your app is categorized in the Store for customers on Windows 10, based on its previous category.

**Note**  You'll see the new category listed in the dashboard when viewing your [app's category](category-and-subcategory-table.md) in the [App properties](enter-app-properties.md) page of a submission, and customers viewing the Store on Windows 10 devices will see your app in the new category. However, customers viewing the Store from an earlier operating system will continue to see the app listed in its original category.


**Category changes for Windows Phone apps:**

| Previous category                       | New category                  |
|-----------------------------------------|-------------------------------|
| Government + politics &gt; commentary   | Government + politics         |
| Government + politics &gt; legal issues | Government + politics         |
| Government + politics &gt; politics     | Government + politics         |
| Government + politics &gt; resources    | Government + politics         |
| Health + fitness &gt; diet + nutrition  | Health + fitness              |
| Health + fitness &gt; fitness           | Health + fitness              |
| Health + fitness &gt; health            | Health + fitness              |
| Lifestyle &gt; art + entertainment      | Lifestyle                     |
| Lifestyle &gt; out + about              | Lifestyle                     |
| Lifestyle &gt; food + dining            | Food + dining                 |
| Lifestyle &gt; shopping                 | Shopping                      |
| News + weather &gt; international       | News + weather                |
| News + weather &gt; local + national    | News + weather                |
| Utilities + productivity                | Utilities + tools             |
| Travel + navigation                     | Travel                        |
| Travel + navigation &gt; planning       | Travel                        |
| Travel + navigation &gt; tools          | Travel                        |
| Travel + navigation &gt; with kids      | Kids + family &gt; travel     |
| Travel + navigation &gt; language       | Education &gt; Language       |
| Travel + navigation &gt; mapping        | Navigation + maps             |
| Travel + navigation &gt; navigation     | Navigation + maps             |
| Games &gt; classics                     | Games &gt; Action + adventure |
| Games &gt; family                       | Games &gt; Family + kids      |
| Games &gt; sports + recreation          | Games &gt; Sports             |
| Games &gt; strategy + simulation        | Games &gt; Strategy           |

 

**Category changes for Windows 8 apps:**

| Previous category           | New category                         |
|-----------------------------|--------------------------------------|
| Books + Reference &gt; Kids | Kids + family &gt; Books + reference |
| Music + Videos &gt; Video   | Photo + video                        |
| Music + Videos &gt; Music   | Music                                |
| Government                  | Government + politics                |
| Finance                     | Personal Finance                     |
| Games &gt; Action           | Games &gt; Action + adventure        |
| Games &gt; Adventure        | Games &gt; Action + adventure        |
| Games &gt; Arcade           | Games &gt; Action + adventure        |
| Games &gt; Card             | Games &gt; Card + board              |
| Games &gt; Kids             | Games &gt; Family + kids             |
| Games &gt; Family           | Games &gt; Family + kids             |
| Games &gt; Puzzle           | Games &gt; Puzzle + trivia           |
| Games &gt; Racing           | Games &gt; Racing + flying           |
