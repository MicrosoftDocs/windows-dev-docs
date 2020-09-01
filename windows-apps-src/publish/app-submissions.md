---
Description: Once you've created your app by reserving a name, you can start working on getting it published. The first step is to create a submission.
title: App submissions
ms.assetid: 363BB9E4-4437-4238-A80F-ABDFC70D96E4
keywords: checklist, windows, uwp, submission, submit, game, app, submitting
ms.date: 10/31/2018
ms.topic: article


ms.localizationpriority: medium
---
# App submissions


Once you've [created your app by reserving a name](create-your-app-by-reserving-a-name.md), you can start working on getting it published. The first step is to create a **submission**.

You can start your submission when your app is complete and ready to publish, or you can start entering info even before you have written a single line of code. Updates you make to your submission are saved, so you can come back and work on it whenever you're ready.

> [!NOTE]
> You must have an active [developer account](https://developer.microsoft.com/store/register) in [Partner Center](https://partner.microsoft.com/dashboard) in order to submit apps to the Microsoft Store.

After your app is published, you can publish an updated version by creating another submission in Partner Center. Creating a new submission lets you make and publish whatever changes are needed, whether you're uploading new packages or just changing details such as price or category. To create a new submission for a published app, click **Update** next to the most recent submission shown on its **Overview** page. You can also [remove an app from the Store](guidance-for-app-package-management.md#removing-an-app-from-the-store) if you need to do so (and then make it available again later, if you'd like).

> [!NOTE]
> This section of the documentation describes how to create an app submission in Partner Center. Alternatively, you can use the [Microsoft Store submission API](../monetize/create-and-manage-submissions-using-windows-store-services.md) to automate app submissions.

> [!IMPORTANT]
> You can no longer upload new XAP packages built using the Windows Phone 8.x SDK(s). Apps that are already in Store with XAP packages will continue to work on Windows 10 Mobile devices. For more info, see this [blog post](https://blogs.windows.com/windowsdeveloper/2018/08/20/important-dates-regarding-apps-with-windows-phone-8-x-and-earlier-and-windows-8-8-1-packages-submitted-to-microsoft-store).

## App submission checklist

Here are the details that you can provide when creating your app submission, with links to more info.

Items that you are required to provide or specify are noted below. Some areas are optional, or have default values provided that you can change as desired. You don't have to work on these sections in the order listed here.

### Pricing and availability page
| Field name                    | Notes                                       | For more info                                                             |
|-------------------------------|---------------------------------------------|---------------------------------------------------------------------------|
| **Markets**                   | Default: All possible markets  | [Define pricing and market selection](./define-market-selection.md)         |
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
| **Privacy policy URL**            | Required for many apps. See the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) and the [Microsoft Store Policies](store-policies.md#105-personal-information) | [Privacy policy URL](enter-app-properties.md#privacy-policy-url)        |
| **Website**                   | Optional                                    | [Website](enter-app-properties.md#website)                   |
| **Support contact info**      | Required if your product is available on Xbox; otherwise optional (but recommended)                                   | [Support contact info](enter-app-properties.md#support-contact-info)              |
| **Game settings**             | Optional (only applicable to games)         | [Game settings](enter-app-properties.md#game-settings) |
| **Display mode**             | Optional                   | [Display mode](enter-app-properties.md#display-mode) |
| **Product declarations**          | Default: Customers can install this app to alternate drives or removable storage; Windows can include this app's data in automatic backups to OneDrive | [Product declarations](./product-declarations.md) |
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
| **App features**              | Optional                                    | [Product features](create-app-store-listings.md#product-features)         |
| **Screenshots**               | Required (at least one screenshot; four or more recommended)          | [Screenshots](app-screenshots-and-images.md#screenshots)          |
| **Store logos**               | Recommended; required for some OS versions | [Store logos](app-screenshots-and-images.md#store-logos)             |
| **Trailers**                  | Optional                                    | [Trailers](app-screenshots-and-images.md#trailers)                | 
| **Windows 10 and Xbox image (16:9 Super hero art)**     | Recommended        | [Windows 10 and Xbox image (16:9 Super hero art)](app-screenshots-and-images.md#windows-10-and-xbox-image-169-super-hero-art) |
| **Xbox images**     | Required for proper display if you publish to Xbox        | [Xbox images](app-screenshots-and-images.md#xbox-images) |
| **Supplemental fields**  | Optional                                    | [Supplemental fields](create-app-store-listings.md#supplemental-fields) 
| **Search terms**              | Optional                                    | [Search terms](create-app-store-listings.md#search-terms)         |
| **Copyright and trademark info** | Optional                                 | [Copyright and trademark info](create-app-store-listings.md#copyright-and-trademark-info) |
| **Additional license terms**  | Optional                                    | [Additional license terms](create-app-store-listings.md#additional-license-terms) |
| **Developed by**              | Optional                                    | [Developed by](create-app-store-listings.md#developed-by)                   |


<span/>

### Submission options page

| Field name                    | Notes                                       | For more info                                                     |
|-------------------------------|---------------------------------------------|-------------------------------------------------------------------|
| **Publishing hold options**     | Default: Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)      | [Publishing hold options](manage-submission-options.md#publishing-hold-options)    
| **Notes for certification**     | Recommended          | [Notes for certification](notes-for-certification.md)             |
| **Restricted capabilities**     | Required if your product declares any [restricted capabilities](../packaging/app-capability-declarations.md#restricted-capabilities)    | [Restricted capabilities](manage-submission-options.md#publishing-hold-options)       

<span/>

> [!NOTE]
> For info about publishing line-of-business (LOB) apps directly to enterprises, see [Distribute LOB apps to enterprises](distribute-lob-apps-to-enterprises.md).