---
description: Create a submission that contains all the info necessary to publish your MSI or EXE app on the Microsoft Store.
title: Create an app submission for your MSI or EXE app
ms.assetid: 1739463F-8723-4278-9D0F-9F5F7345C0B7
ms.date: 06/24/2021
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, win32
ms.localizationpriority: medium
---

# Create an app submission for your MSI or EXE app

> [!NOTE]
> MSI and EXE support in the Microsoft Store is currently in a limited public preview phase. As the size of the preview expands, we'll be adding new participants from the wait list. To join the wait list, click [here](https://aka.ms/storepreviewwaitlist).

Once you've created your app by reserving a name, you can start working on getting it published. The first step is to create a submission.

You can start your submission when your app is complete and ready to publish, or you can start entering info before you have written a single line of code. Updates you make to your submission are saved, so you can come back and work on it whenever you're ready.

> [!NOTE]
> You must have an active developer account in Partner Center in order to submit apps to the Microsoft Store.

After your app is published, you can publish an updated version by creating another submission in Partner Center. Creating a new submission lets you make  whatever changes are needed, whether you're providing new packages or just changing details such as What’s new or Description. To create a new submission for a published app, click Update next to the most recent submission shown on its Overview page. You can also remove an app from the Store if you need to (and then make it available again later if you'd like).

## App submission checklist

Here are the details that you will need when creating your app submission. Required fields are marked. Some areas are optional or have default values provided which you can change. You do not have to work on these sections in the order listed here.

### Availability page

| Field name     | Required   | Notes |
|----------------|------------|-------|
| **Markets**    | Required   | Default: All possible markets<br>For more information see [Define pricing and market selection](../define-market-selection.md). |
| **Pricing**    | Required   | One of: Free; Paid; Freemium; Subscription. |
| **Free Trial** | Required   | *Not* required if pricing is set to Free or Freemium. |

### Properties page, support info section

| Field name                      | Required | Notes |
|---------------------------------|----------|-------|
| **Category**                    | Required | [Category and subcategory table](enter-app-properties.md) |
| **Subcategory**                 |          | [Category and subcategory table](enter-app-properties.md) |
| **Does this product access...** | Required |       |
| **Privacy policy URL**          | Required | Only required if you answered yes to the previous question. |
| **Website**                     |          |       |
| **Support contact info**        |          |       |

### Properties page, products declaration section

| Field name                                                     | Required    | Notes |
|----------------------------------------------------------------|-------------|-------|
| **This app depends on non-Microsoft drivers or NT services.**  |             |       |
| **This app has been tested to meet accessibility guidelines.** |             |       |
| **This product supports pen and ink input.**                   |             |       |
| **Notes for certification**                                    | Recommended | Character limit: 2,000. |

### Properties page, system requirements section

| Field name                  | Required | Notes |
|-----------------------------|----------|-------|
| **Touch screen**            |          |       |
| **Keyboard**                |          |       |
| **Mouse**                   |          |       |
| **Camera**                  |          |       |
| **NFC HCE**                 |          |       |
| **NFC Proximity**           |          |       |
| **Bluetooth LE**            |          |       |
| **Telephony**               |          |       |
| **Microphone**              |          |       |
| **Memory**                  |          |       |
| **DirectX**                 |          |       |
| **Video Memory**            |          |       |
| **Processor**               |          |       |
| **Graphics**                |          |       |

### Packages page

| Field name                  | Required | Notes |
|-----------------------------|----------|-------|
| **Package URL**             | Required | At least one package URL is required. |
| **Language**                | Required | At least one language is required. |
| **Architecture**            | Required |       |
| **Installer parameters**    | Required | Support for silent install is required. Other parameters are optional. |
| **Let Microsoft decide...** |          | If selected, Microsoft will automatically make this app available to any appropriate future device families. |

## Store listings page

Each language has a separate store listing page. One listing page is required. It is recommended to provide complete listing page information for each language your app supports.

| Field name                         | Required | Notes |
|------------------------------------|----------|-------|
| **Description**                    | Required | Character limit: 10,000. |
| **What’s new in this version**     |          | Character limit: 1,500.  |
| **App features**                   |          | Character limit: 200 per feature; Feature limit: 20. |
| **Screenshots**                    | Required | Required: 1; Recommended: 4+; Maximum: 10<br>For more information see [Create app Store listings for your MSI or EXE app](create-app-store-listings.md). |
| **Store logos**                    | Required | 1:1 Box art required, 2:3 Poster art recommended<br>For more information see [Create app Store listings for your MSI or EXE app](create-app-store-listings.md).  |
| **Sort title**                     |          | Character limit: 255    |
| **Short description**              |          | Character limit: 1,000  |
| **Additional system requirements** |          | Character limit: 200 characters per requirement; Requirements limit: 11 for each of minimum and recommended hardware. |
| **Search terms**                   |          | Character limit: 30 per term; Term limit: 7; Maximum of 21 unique words total among all terms. |
| **Copyright and trademark info**   |          | Character limit: 200    |
| **Additional license terms**       | Required | Character limit: 10,000 |
| **Developed by**                   |          | Character limit: 255    |
