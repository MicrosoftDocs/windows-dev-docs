---
author: jnHs
Description: Once you've created your app by reserving a name, you can start working on getting it published. The first step is to create a submission.
title: App submissions
ms.assetid: 363BB9E4-4437-4238-A80F-ABDFC70D96E4
keywords: checklist, windows, uwp, submission, submit, game, app, submitting
ms.author: wdg-dev-content
ms.date: 04/03/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: high
---

# App submissions


Once you've [created your app by reserving a name](create-your-app-by-reserving-a-name.md), you can start working on getting it published. The first step is to create a **submission**.

You can start your submission when your app is complete and ready to publish, or you can start entering info even before you have written a single line of code. The submission will be saved in your dashboard, so you can work on it whenever you're ready.

After your app is published, you can publish an updated version by creating another submission in your dashboard. Creating a new submission lets you make and publish whatever changes are needed, whether you're uploading new packages or just changing details such as price or category. To create a new submission for a published app, click **Update** next to the most recent submission shown on the App overview page.

> [!NOTE]
> This section of the documentation describes how to create an app submission on the Dev Center dashboard. Alternatively, you can use the [Microsoft Store submission API](../monetize/create-and-manage-submissions-using-windows-store-services.md) to automate app submissions.

## App submission checklist

Here are the details that you can provide when creating your app submission, with links to more info.

Items that you are required to provide or specify are noted below. Some areas are optional, or have default values provided that you can change as desired.

### Pricing and availability page
| Field name                    | Notes                                       | For more info                                                             |
|-------------------------------|---------------------------------------------|---------------------------------------------------------------------------|
| **Markets**                   | Default: All possible markets,  | [Define pricing and market selection](define-pricing-and-market-selection.md)         |
| **Audience**                | Default: Public audience | [Audience](choose-visibility-options.md#audience) |
| **Discoverability**                | Default: Make this app available and discoverable in the Store | [Discoverability](choose-visibility-options.md#discoverability) |
| **Schedule**                  | Default: Release as soon as possible        | [Configure precise release scheduling](configure-precise-release-scheduling.md) |
| **Base price**                | Required                                    | [Set and schedule app pricing](set-and-schedule-app-pricing.md)              |
| **Free trial**                | Default: No free trial                      | [Free trial](set-app-pricing-and-availability.md#free-trial)              |
| **Sale pricing**              | Optional                                    | [Put apps and add-ons on sale](put-apps-and-add-ons-on-sale.md)           |
| **Organizational licensing**    | Default: Allow volume acquisition by organizations | [Organizational licensing options](organizational-licensing.md)        |
      |


### Properties page

| Field name                    | Notes                                       | For more info                                                             |
|-------------------------------|---------------------------------------------|---------------------------------------------------------------------------|
| **Category and subcategory**  | Required                                    | [Category and subcategory table](category-and-subcategory-table.md)       |
| **Privacy policy URL**            | Required for many apps. See the [App Developer Agreement](https://docs.microsoft.com/legal/windows/agreements/app-developer-agreement) and the [Microsoft Store Policies](https://docs.microsoft.com/en-us/legal/windows/agreements/store-policies#105-personal-information) | [Privacy policy URL](enter-app-properties.md#privacy-policy-url)        |
| **Website**                   | Optional                                    | [Website](enter-app-properties.md#website)                   |
| **Support contact info**      | Required if your product is available on Xbox; otherwise optional (but recommended)                                   | [Support contact info](enter-app-properties.md#support-contact-info)              |
| **Game settings**             | Optional (only applicable to games)         | [Game settings](enter-app-properties.md#game-settings) |
| **Display mode**             | Optional                   | [Display mode](enter-app-properties.md#display-mode) |
| **Product declarations**          | Default: Customers can install this app to alternate drives or removable storage; Windows can include this app's data in automatic backups to OneDrive | [Product declarations](app-declarations.md) |
| **System requirements**      | Optional                                    | [System requirements](enter-app-properties.md#system-requirements)      |

<span/>

### Age ratings page

| Field name                    | Notes                                       | For more info                          |
|-------------------------------|---------------------------------------------|----------------------------------------|
| **Age ratings**               | Required                                    | [Age ratings](age-ratings.md)          |

<span/>

### Packages page

| Field name                    | Notes                                  | For more info                          |
|-------------------------------|----------------------------------------|----------------------------------------|
| **Package upload control**    | Required (at least one package)        | [Upload app packages](upload-app-packages.md) |
| **Device family availability** | Default: based on your packages       | [Device family availability](device-family-availability.md) |
| **Gradual package rollout**   | Optional (for updates only)            | [Gradual package rollout](gradual-package-rollout.md) |
| **Mandatory update**          | Optional (for updates only)            | [Mandatory update](upload-app-packages.md#mandatory-update)


### Store listings

You'll need all the required info for at least one of the languages that your app supports. We recommend providing [Store listings](create-app-store-listings.md) in all of the languages your app supports, and you can also [provide Store listings in additional languages](create-app-store-listings.md#store-listing-languages). To make it easier to manage multiple listings for the same product, you can [import and export Store listings](import-and-export-store-listings.md).

| Field name                    | Notes                                       | For more info                                                     |
|-------------------------------|---------------------------------------------|-------------------------------------------------------------------|
| **Description**               | Required                                    | [Write a great app description](write-a-great-app-description.md) |
| **What's new in this version**   | Optional                                 | [Release notes](create-app-store-listings.md#whats-new-in-this-version)       |
| **App features**              | Optional                                    | [App features](create-app-store-listings.md#app-features)         |
| **Screenshots**               | Required (at least one screenshot; four or more recommended)          | [Screenshots](app-screenshots-and-images.md#screenshots)          |
| **Store logos**               | Recommended; required for some OS versions | [Store logos](app-screenshots-and-images.md#store-logos)             |
| **Additional art assets**     | Recommended (especially for some OS versions)         | [Additional art assets](app-screenshots-and-images.md#additional-art-assets) |
| **Trailers**                  | Optional                                    | [Trailers](app-screenshots-and-images.md#trailers)                | 
| **Supplemental information**  | Optional                                    | [Supplemental information](create-app-store-listings.md#supplemental-information) 
| **Search terms**              | Optional                                    | [Search terms](create-app-store-listings.md#search-terms)         |
| **Copyright and trademark info** | Optional                                 | [Copyright and trademark info](create-app-store-listings.md#copyright-and-trademark-info) |
| **Additional license terms**  | Optional                                    | [Additional license terms](create-app-store-listings.md#additional-license-terms) |
| **Developed by**              | Optional                                    | [Developed by](create-app-store-listings.md#developed-by)                   |
| **Platform-specific Store listings** | Optional                               | [Create platform-specific Store listings](create-platform-specific-store-listings.md)  |

<span/>

### Submission options page

| Field name                    | Notes                                       | For more info                                                     |
|-------------------------------|---------------------------------------------|-------------------------------------------------------------------|
| **Publishing hold options**                | Default: Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)      | [Publishing hold options](manage-submission-options.md#publishing-hold-options)    
| **Notes for certification**                     | Recommended                                    | [Notes for certification](notes-for-certification.md)             |

<span/>

> [!NOTE]
> For info about publishing line-of-business (LOB) apps directly to enterprises, see [Distribute LOB apps to enterprises](distribute-lob-apps-to-enterprises.md).
