---
author: jnHs
Description: Managing your add-ons in bulk allows you to make changes to multiple add-ons at once rather than submitting each update individually.
title: Manage add-ons in bulk
ms.author: wdg-dev-content
ms.date: 08/03/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 6d1ffcc1-b3c6-4e2f-8fbe-d243b20a6272
ms.localizationpriority: medium
---

# Manage add-ons in bulk

> [!IMPORTANT]
> This feature was previously available to developer accounts which joined the [Dev Center Insider Program](dev-center-insider-program.md), and is not currently supported.

Managing your add-ons in bulk allows you to make changes to multiple add-ons at once rather than submitting each update individually. You can access this functionality from your app’s overview page by clicking **Manage add-ons in bulk**.

## Export current add-on info

To get started, you’ll first need to download a .csv template file. If you’ve already created add-ons, this file will include info about them. If not, it will be a blank file that you can use to enter info for new add-ons.

To generate and download this template file, click **Export add-ons** and save the .csv file to your computer.

The .csv file contains the following columns. 

| Column name               | Description                            | Required?      |
|---------------------------|----------------------------------|----------------------|
| Product ID	|  The unique [product ID](set-your-add-on-product-id.md#product-id) of the add-on.  | Yes. Can’t be changed after the add-on is published. |
| Action |The action you want to apply when you import the template. Supported values are **Submit** (to submit a new add-on or update a previously-published add-on) and **CreateDraft** (to save the changes without submitting them to the Store). |	 Yes |
| Product type	| The [product type](set-your-add-on-product-id.md#product-type) of the add-on. Supported values are **Consumable** or **Durable**. |	Yes. Can’t be changed after add-on is published. |
| Product lifetime	| For a Durable add-on, this is either **Forever** (for a product that never expires) or a set duration. Acceptable duration values are: **1day, 3days, 5days, 7days, 14days, 30days, 60days, 90days, 180days, 365days**	| Yes (if Product type is Durable) |
| Content type	| The [content type](enter-add-on-properties.md#content-type) of the add-on. For most add-ons. this should be **ElectronicSoftwareDownload**. Other acceptable values are: **ElectronicBooks, ElectronicMagazineSingleIssue, ElectronicNewspaperSingleIssue, MusicDownload, MusicStreaming, OnlineDataStorageServices, VideoDownload, VideoStreaming, SoftwareAsAService** |	Yes |
| Tag	| Optional [Tag](enter-add-on-properties.md#custom-developer-data) (also known as **Custom developer data**) info used in your app’s implementation. | No |
| Base price	| The price tier at which you want to offer the add-on. Must either be **Free** or a valid price tier in the format **0.99USD**. |	Yes |
| Release date	| The date at which you want to publish the add-on. Acceptable values are **Immediate**, **Manual**, or a date string that complies with the [ISO 8601 standard](http://go.microsoft.com/fwlink/p/?LinkId=817237). | Yes |
| Titles	| The name that customers will see for the add-on, preceded by the language code and a semicolon. For example, to use the title “Example Title” in English/United States, you would *enter en-us;Example Title*. Additional titles for other languages can be separated by semicolons. Each title must be 100 characters or fewer. 	| Yes |
|Descriptions	| Optional additional info to display to customers, preceded by the language-locale code and a semicolon. For example, to use the description “This is an example” in English/United States, you would enter *en-us;This is an example*. Additional titles for other languages can be separated by semicolons. Each description must be 200 characters or fewer.	| No |
| Markets |	One or more [markets](define-pricing-and-market-selection.md#microsoft-store-consumer-markets) in which you want to offer the add-on. Separate each market by a semicolon. |	Yes |
|Keywords |	Optional [keywords](enter-add-on-properties.md#keywords) used in your app’s implementation. | No |

## Import add-ons

Before you can import changes, you’ll need to update the downloaded .csv file with the changes you’d like to make.

To make changes to add-ons that you’ve already published, update the values you wish to change in your copy of the spreadsheet. You can remove any rows for add-ons that you don’t want to update, or leave them as is. Note that if there is already a submission in progress for that add-on, you won’t be able to make changes using the .csv file.

> **Important** When submitting updates to add-ons that you’ve already published, you can’t change the **Product ID** and **Product type** fields.

To submit a new add-on, add a new row and enter the info for your new add-on. Be sure to enter all of the required info. 

When you have made all of the changes, save the .csv file (with the same filename), then upload the file by dragging it to the specified field (or click **Browse your files**). A summary of your changes will be shown, along with any errors that must be fixed before submitting. After you’ve verified that the info is correct, you can click **Submit to the Store**. Each add-on will go through the submission process using the info that you’ve provided.

