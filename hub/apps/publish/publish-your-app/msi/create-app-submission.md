---
title: Create an app submission for your MSI/EXE app
description: Create an app submission for your MSI/EXE app
ms.topic: article
ms.date: 10/30/2022
---

# Create an app submission for your MSI/EXE app

> [!NOTE]
> This section of the documentation describes how to create an app submission in Partner Center. Alternatively, you can use the [Microsoft Store submission API](../../store-submission-api.md) to automate app submissions.

Once you've [created your app by reserving a name](./reserve-your-apps-name.md), you can start working on getting it published by following the checklist below:

## App submission checklist

Here are the details that you can provide when creating your app submission, with links to more info.

Items that you are required to provide or specify are noted below. Some areas are optional, or have default values provided that you can change as desired. You don't have to work on these sections in the order listed here.

### Availability page

| Field name     | Required     | Notes                                                 |
| -------------- | ------------ | ----------------------------------------------------- |
| **Markets**    | **Required** | Default: All possible markets                         |
| **Discoverability** | **Required** | One of: Available in microsoft store; Available through link.|
| **Pricing**    | **Required** | One of: Free; Paid; Freemium; Subscription.           |
| **Free Trial** | **Required** | _Not_ required if pricing is set to Free or Freemium. |

> [!TIP]
> For detailed information about the **Availability** fields, see the [Set pricing and availability](./price-and-availability.md) section.

### Properties page, support info section

| Field name                      | Required     | Notes                                                      |
| ------------------------------- | ------------ | ---------------------------------------------------------- |
| **Category**                    | **Required** |                                                            |
| **Subcategory**                 | Not required |                                                            |
| **Secondary category**          | Not required |                                                            |
| **Does this product access...** | **Required** |                                                            |
| **Privacy policy URL**          | See notes    | Only required if you answered yes to the previous question |
| **Website**                     | Not required |                                                            |
| **Contact details**             | **Required** for business/company accounts |                              |
| **Support contact info**        | Not required |                                                            |

> [!TIP]
> For detailed information about the **Properties** fields, see the [Support info](./support-info.md) section.

### Properties page, products declaration section

| Field name                                                     | Required     | Notes                  |
| -------------------------------------------------------------- | ------------ | ---------------------- |
| **This app depends on non-Microsoft drivers or NT services.**  | Not required |                        |
| **This app has been tested to meet accessibility guidelines.** | Not required |                        |
| **This product supports pen and ink input.**                   | Not required |                        |
| **Notes for certification**                                    | Recommended  | Character limit: 2,000 |

> [!TIP]
> For detailed information about the **Properties** fields, see the [Product declarations](./product-declarations.md) section.

### Properties page, system requirements section

| Field name               | Required     | Notes |
| ------------------------ | ------------ | ----- |
| **Touch screen**         | Not required |       |
| **Keyboard**             | Not required |       |
| **Mouse**                | Not required |       |
| **Camera**               | Not required |       |
| **NFC HCE**              | Not required |       |
| **NFC Proximity**        | Not required |       |
| **Bluetooth LE**         | Not required |       |
| **Telephony**            | Not required |       |
| **Microphone**           | Not required |       |
| **Memory**               | Not required |       |
| **DirectX**              | Not required |       |
| **Dedicated GPU Memory** | Not required |       |
| **Processor**            | Not required |       |
| **Graphics**             | Not required |       |

> [!TIP]
> For detailed information about the **Properties** fields, see the [System requirements](./system-requirements.md) section.

### Age ratings page

| Field name      | Notes        |
| --------------- | ------------ |
| **All questions** | **Required** |

> [!TIP]
> For detailed information about the **Age ratings** fields, see the [Age ratings](./age-ratings.md) section.

### Packages page

| Field name                 | Required     | Notes                                                                 |
| -------------------------- | ------------ | --------------------------------------------------------------------- |
| **Package URL**            | **Required** | At least one package URL is required                                  |
| **Architecture**           | **Required** |                                                                       |
| **Installer parameters**   | **Required** | Support for silent install is required. Other parameters are optional |
| **Language**               | **Required** | At least one language is required                                     |
| **App type**               | **Requited** | Specify between EXE and MSI                                           |
                                          |

> [!TIP]
> For detailed information about the **Packages** fields, see the [Upload package](./upload-app-packages.md) section.

## Store listings page

Each language has a separate store listing page. One listing page is required. It is recommended to provide complete listing page information for each language your app supports.

| Field name                         | Required     | Notes                                                                                                                 |
| ---------------------------------- | ------------ | --------------------------------------------------------------------------------------------------------------------- |
| **Description**                    | **Required** | Character limit: 10,000                                                                                               |
| **Product name**                    | Not required | Select on dropdown                                             |
| **What’s new in this version**     | Not required | Character limit: 1,500                                                                                                |
| **App features**                   | Not required | Character limit: 200 per feature; Feature limit: 20.                                                                  |
| **Screenshots**                    | **Required** | Required: 1; Recommended: 4+; Maximum: 10                                                                             |
| **Store logos**                    | Required     | 1:1 Box art required, 2:3 Poster art recommended                                                                      |
| **Short description**              | Not required | Character limit: 1,000                                                                                                |
| **Additional system requirements** | Not required | Character limit: 200 characters per requirement; Requirements limit: 11 for each of minimum and recommended hardware. |
| **Keywords**                       | Not required | Character limit: 40 per term; Term limit: 7; Maximum of 21 unique words total among all terms.                        |
| **Copyright and trademark info**   | Not required | Character limit: 200                                                                                                  |
| **Applicable license terms**       | **Required** | Character limit: 10,000                                                                                               |
| **Developed by**                   | Not required | Character limit: 255                                                                                                  |

> [!TIP]
> For detailed information about the **Store listings** fields, see the [Store listings](./add-and-edit-store-listing-info.md) section.

Once you have completed all the sections, you can submit your app for certification by clicking on **Publish** button on the Store listing page or by clicking **Submit** button on the Application overview page.

> [!NOTE]
> You must have an active [developer account](https://developer.microsoft.com/store/register) in [Partner Center](https://partner.microsoft.com/dashboard) in order to submit apps to the Microsoft Store. All the users added to your developer account in Partner Center can submit EXE or MSI apps to the Microsoft Store. They can also modify all the existing EXE or MSI apps in Partner Center. The roles and permissions set for account users do not currently apply to EXE or MSI apps.

> [!TIP]
> To refer to common questions, please refer to [Frequently Asked Questions](../../faq/submit-your-app.md) section.
