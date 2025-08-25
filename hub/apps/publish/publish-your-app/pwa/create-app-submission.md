---
title: Create an app submission for your PWA
description: Create an app submission for your PWA app
ms.topic: how-to
ms.date: 10/30/2022
---

# Create app submission

> [!NOTE]
> This section of the documentation describes how to create an app submission in Partner Center. Alternatively, you can use the [Microsoft Store submission API](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services) to automate app submissions.

Once you've [created your app by reserving a name](reserve-your-apps-name.md), you can start working on getting it published. The first step is to create a submission. After you have reserved your app name, you will be redirected to your app's application overview page. From the Product release section, click on **Start submission**. A product submission in draft status will appear. This draft includes all the submission steps that need to be completed. Refer to the app submission checklist below to complete the steps:

## App submission checklist

Here are the details that you can provide when creating your app submission, with links to more info.

Items that you are required to provide or specify are noted below. Some areas are optional, or have default values provided that you can change as desired. You don't have to work on these sections in the order listed here.

### Pricing and availability page

| Field name                   | Required     | Notes                |
| ---------------------------- | ------------ |----------------------|
| **Markets**                  | **Required** | Default: All possible markets |
| **Visibility**               | **Required** | Audience - Default: Public audience - Discoverability - Default: Make this product available and discoverable in the Microsoft Store. |
| **Discoverability**          | **Required** | Default: Make this product available and discoverable in the Microsoft Store |
| **Schedule**                 | **Required** | Default: Release - as soon as possible; Stop acquisition - never |
| **Pricing**                  | **Required** |                      |
| **Free trial**               | Not required |                      |
| **Sale pricing**             | Not required |                      |
| **Organizational licensing** | Not required |                      |

> [!TIP]
> For detailed information about the **Pricing and availability** fields, see the [Set app pricing and availability for PWA](./price-and-availability.md) section.

### Properties page

| Field name                   | Required     |
| ---------------------------- | ------------ |
| **Category**                 | **Required** |
| **Subcategory**              | Not required |
| **Secondary category**       | Not required |
| **Privacy policy**           | **Required** if your app accesses, collects, or transmits personal information |  
| **Support info**             | Not required |
| **Display mode**             | Not required |
| **Product declarations**     | Not required |
| **System requirements**      | Not required |

> [!TIP]
> For detailed information about the **Properties** fields, see the [Enter app properties for PWA](./enter-app-properties.md) section.

### Age ratings page

| Field name        | Notes        |
| ------------------| ------------ |
| **All questions** | **Required** |

> [!TIP]
> For detailed information about the **Age rating** fields, see the [Generate age ratings for PWA](./age-ratings.md) section.

### Packages page

| Field name                     | Required     | Notes                          |
| ------------------------------ | ------------ | ------------------------------ |
| **App package**                | **Required** | At least one package required. |
| **Device family availability** | Not required |                                |

> [!TIP]
> For detailed information about the **Packages** fields, see the [Upload app packages for PWA](./upload-app-packages.md) section.

### Store listings page

You'll need all the required info for at least one of the languages that your app supports. We recommend providing [Store listings](./add-and-edit-store-listing-info.md) in all of the languages your app supports, and you can also provide Store listings in additional languages. To make it easier to manage multiple listings for the same product, you can import and export Store listings.

| Field name                                                        | Required     | Notes                                                      |
| ----------------------------------------------------------------- | ------------ | ---------------------------------------------------------- |
| **Description**                                                   | **Required** |                                                            |
| **What's new in this version**                                    | Not required |                                                            |
| **Product features**                                              | Not required |                                                            |
| **Screenshots**                                                   | **Required** | At least one screenshot required; four or more recommended |
| **Store logos**                                                   | **Required** for some OS versions |                          |
| **Trailers and additional assets**                                | Not required |                                                            |
| **Windows and Xbox images**                                       | **Recommended** for proper display if you publish to Xbox  |     |
| **Supplemental fields**                                           | Not required |                                                            |
| **Additional information**                                        | Not required |                                                            |


### Submission options page

| Field name                           | Required     | 
| ------------------------------------ | ------------ | 
| **Publishing hold options**          | Not required |
| **Notes for certification**          | Not required |
| **Restricted capabilities**          | **Required** if your product declares any restricted capabilities |
| **Submission notification audience** | Not required |

Once you have completed all the sections, you can submit your app for certification by clicking **Submit for certification** button on the Application overview page.

>[!TIP]
> To refer to common questions, please refer to [Frequently Asked Questions](../../faq/submit-your-app.md) section.

> [!NOTE]
> You must have an active [developer account](https://developer.microsoft.com/store/register) in [Partner Center](https://partner.microsoft.com/dashboard) in order to submit apps to the Microsoft Store. All the users added to your developer account in Partner Center can submit EXE or MSI apps to the Microsoft Store. They can also modify all the existing EXE or MSI apps in Partner Center. The roles and permissions set for account users do not currently apply to EXE or MSI apps.
