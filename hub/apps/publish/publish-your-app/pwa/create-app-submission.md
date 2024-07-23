---
title: Create an app submission for your PWA
description: Create an app submission for your PWA app
ms.topic: article
ms.date: 10/30/2022
---

# Create an app submission for your PWA

> [!NOTE]
> This section of the documentation describes how to create an app submission in Partner Center. Alternatively, you can use the [Microsoft Store submission API](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services) to automate app submissions.

Once you've [created your app by reserving a name](reserve-your-apps-name.md), you can start working on getting it published. The first step is to create a submission. After you have reserved your app name, you will be redirected to your app's application oveview page. From the Product release section, click on **Start submission**. A product submission in draft status will appear. This draft includes all the submission steps that need to be completed. Refer the app submission checklist below to complete the steps:

## App submission checklist

Here are the details that you can provide when creating your app submission, with links to more info.

Items that you are required to provide or specify are noted below. Some areas are optional, or have default values provided that you can change as desired. You don't have to work on these sections in the order listed here.

### Pricing and availability page

| Field name                   | Required     | Notes                |
| ---------------------------- | ------------ |----------------------|
| **Markets**                  | **Required** | Default: All possible markets |
| **Audience**                 | **Required** | Default: Public audience |
| **Discoverability**          | **Required** | Default: Make this product available and discoverable in the Microsoft Store |
| **Schedule**                 | **Required** | Default: Release - as soon as possible; Stop acquisition - never |
| **Base price**               | **Required** |                      |
| **Free trial**               | Not required |                      |
| **Sale pricing**             | Not required |                      |
| **Organizational licensing** | Not required |                      |

### Properties page

| Field name                   | Required     |
| ---------------------------- | ------------ |
| **Category**                 | **Required** |
| **Subcategory**              | Not required |
| **Secondary category**       | Not required |
| **Privacy policy URL**       | **Required** if your app collects/trasmits personal information |  
| **Website**                  | Not required |
| **Support contact info**     | **Required** if your product is available on Xbox. |
| **Contact details**          | **Required** for business/company accounts |
| **Game settings**            | Not required |
| **Display mode**             | Not required |
| **Product declarations**     | Not required |
| **System requirements**      | Not required |

### Age ratings page

| Field name        | Notes        |
| ------------------| ------------ |
| **All questions** | **Required** |

### Packages page

| Field name                     | Required     | Notes                          |
| ------------------------------ | ------------ | ------------------------------ |
| **App package**                | **Required** | At least one package required. |
| **Device family availability** | Not required |                                |
| **Gradual package rollout**    | Not required |                                |
| **Mandatory update**           | Not required |                                |

### Store listings

You'll need all the required info for at least one of the languages that your app supports. We recommend providing [Store listings](./create-app-store-listing.md) in all of the languages your app supports, and you can also provide Store listings in additional languages. To make it easier to manage multiple listings for the same product, you can import and export Store listings.

| Field name                                                        | Required     | Notes                                                      |
| ----------------------------------------------------------------- | ------------ | ---------------------------------------------------------- |
| **Description**                                                   | **Required** |                                                            |
| **What's new in this version**                                    | Not required |                                                            |
| **App features**                                                  | Not required |                                                            |
| **Screenshots**                                                   | **Required** | At least one screenshot required; four or more recommended |
| **Store logos**                                                   | **Required** for some OS versions |                          |
| **Trailers**                                                      | Not required |                                                            |
| **Windows 10 or Windows 11 and Xbox image (16:9 Super hero art)** | Not required |                                                            |
| **Xbox images**                                                   | **Required** for proper display if you publish to Xbox  |     |
| **Supplemental fields**                                           | Not required |                                                            |
| **Keywords**                                                      | Not required |                                                            |
| **Copyright and trademark info**                                  | Not required |                                                            |
| **Additional license terms**                                      | Not required |                                                            |
| **Developed by**                                                  | Not required |                                                            |

### Submission options page

| Field name                           | Required     | 
| ------------------------------------ | ------------ | 
| **Publishing hold options**          | Not required |
| **Notes for certification**          | Not required |
| **Restricted capabilities**          | **Required** if your product declares any restricted capabilities |
| **Submission notification audience** | Not required |

Once you have completed all the sections, you can submit your app for certification by clicking **Submit for certification** button on the Application overview page.

> [!NOTE]
> You must have an active [developer account](https://developer.microsoft.com/store/register) in [Partner Center](https://partner.microsoft.com/dashboard) in order to submit apps to the Microsoft Store. All the users added to your developer account in Partner Center can submit EXE or MSI apps to the Microsoft Store. They can also modify all the existing EXE or MSI apps in Partner Center. The roles and permissions set for account users do not currently apply to EXE or MSI apps.

## Notifications

> [!IMPORTANT]
> To ensure that you receive critical email notifications, you'll be required to verify your email address in Action Center. Go to [My Preferences](https://partner.microsoft.com/dashboard/actioncenter/mypreferences) in Action Center to verify.

After publishing an app, the [owner](../../partner-center/assign-account-level-custom-permissions-to-account-users.md) of your developer account is always notified of the publishing status and required actions through email and the [Action Center](/partner-center/action-center-overview) in Partner Center.

In addition, you can add members in either **developer** or **manager** role within your developer account to receive same notifications or remove those who no longer need be notified.

To add or remove:

1. On the Submission options page, look for the field of “Submission notification audience”
2. Click “Click here” to open Notification audience overview page
3. On the Notification audience overview page, add or remove audience

> [!NOTE]
>
> - The owner of your developer account is always notified and can’t be removed from the audience list.
> - The audience list is product specific and applied to all submissions of the product. To modify the notification recipients for a different product, follow the steps above for each product.
> - Add-on inherits parent product’s audience list and can’t be managed separately.
