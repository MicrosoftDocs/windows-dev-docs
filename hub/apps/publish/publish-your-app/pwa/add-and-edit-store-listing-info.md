---
description: This section will guide you in adding or editing all the store listing information for your PWA app.
title: Add and edit Store listing info for PWA
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Add and edit Store listing info

## Create store listings

The **Store listings** section of the [app submission process](./create-app-submission.md) is where you provide the text and [images](./screenshots-and-images.md) that customers will see when viewing your app's listing in the Microsoft Store.

Many of the fields in a **Store listing** are optional, but we suggest providing multiple images and as much info as possible to make your listing stand out. The minimum required for the **Store listings** step to be considered complete is a text description and at least one [screenshot](./screenshots-and-images.md).

> [!TIP]
> You can optionally [import and export Store listings](./import-and-export-store-listings.md) if you'd prefer to enter your listing info offline in a .csv file, rather than providing info and uploading files directly in Partner Center. Using the import and export option can be especially convenient if you have listings in many languages, since it lets you make multiple updates at once.

## Manage Store listing languages

You must complete the **Store listing** page for at least one language. We recommend providing a Store listing in each language that your packages support, but you have flexibility to remove languages for which you don’t wish to provide a Store listing. You can also create Store listings in additional languages which aren’t supported by your packages.

> [!NOTE]
> If your submission includes packages already, we’ll show the [languages](./app-package-requirements.md#supported-languages) supported in your packages on the app overview page (unless you remove any of them).

To add or remove languages for your Store listings, click **Add/remove languages** from the app overview page. If you‘ve already uploaded packages, you’ll see their languages listed in the **Languages supported by your packages** section. To remove one or more of these languages, click **Remove**. If you later decide to include a language that you previously removed from this section, you can click **Add**.

:::image type="content" source="../msix/images/msix-manage-store-listing.png" lightbox="../msix/images/msix-manage-store-listing.png" alt-text="A screenshot showing the manage store listing page for MSIX/PWA app.":::

In the **Additional Store listing languages** section, you can click **Manage additional languages** to add or remove languages that are _not_ included in your packages. Check the boxes for the languages that you’d like to add, then click **Update**. The languages you’ve selected will be displayed in the **Additional Store listing languages** section. To remove one or more of these languages, click **Remove** (or click **Manage additional languages** and uncheck the box for languages you’d like to remove).

:::image type="content" source="../msix/images/msix-listing-languages-pop-up.png" lightbox="../msix/images/msix-listing-languages-pop-up.png" alt-text="A screenshot showing the list of languages available for listing a MSIX/PWA app.":::

:::image type="content" source="../msix/images/msix-listing-language-added.png" lightbox="../msix/images/msix-listing-language-added.png" alt-text="A screenshot showing a listing language has ben added for MSIX/PWA app.":::

When you have finished making your selections, click **Save** to return to the app overview page.

To edit a Store listing, select the language name from the app overview page. You must edit each language separately, unless you choose to export your Store listings and work offline, then import all of the listing data at once. For more about how that works, see [Import and export Store listings](./import-and-export-store-listings.md).

:::image type="content" source="../msix/images/msix-listing-overview.png" lightbox="../msix/images/msix-listing-overview.png" alt-text="A screenshot showing the overview of the listing page for MSIX/PWA app.":::

The available fields are described below.

### Product name

This drop-down box lets you specify which name should be used in the Store listing (if you have reserved more than one name for the app).

If you have uploaded packages in the same language as the Store listing you're working on, the name used in those packages will be selected. If you need to [rename the app](../../partner-center/pwa/manage-app-name-reservations.md#rename-an-app-that-has-already-been-published) after it's already been published, you can select a different reserved name here when you create a new submission, after you've uploaded packages that use the new name.

If you haven't uploaded packages for the language you're working on, and you've reserved more than one name, you'll need to select one of your reserved app names, since there isn't an associated package in that language from which to pull the name.

> [!NOTE]
> The **Product name** you select only applies to the Store listing in the language you're working in. It does not impact the name displayed when a customer installs the app; that name comes from the manifest of the package that gets installed. To avoid confusion, we recommend that each language's package(s) and Store listing use the same name.

### Description

The description field is where you can tell customers what your app does. This field is required, and will accept up to 10,000 characters of plain text.

For some tips on making your description stand out, see [How can I write an effective app description for the Microsoft Store](../../faq/submit-your-app.md) in the FAQ section.

### What's new in this version

If this is the first time you're submitting your app, leave this field blank. For an update to an existing app, this is where you can let customers know what's changed in the latest release. This field has a 1500 character limit. (Previously, this field was called **Release notes**).

### Product features

These are short summaries of your app's key features. They are displayed to the customer as a bulleted list in the **Features** section of your app's Store listing, in addition to the **Description**. Keep these brief, with just a few words (and no more than 200 characters) per feature. You may include up to 20 features.

> [!NOTE]
> These features will appear bulleted in your Store listing, so don't add your own bullets.

### Screenshots

One screenshot is required in order to submit your app. We recommend providing at least four screenshots for each device type that your app supports so that people can see how the app will look on their device type.

For more info, see [App screenshots and images](./screenshots-and-images.md).

### Store logos

Store logos are optional images that you can upload to enhance the way your app is displayed to customers. You can also optionally specify that only images you upload here should be used in your app’s Store listing for customers on Windows 10 or Windows 11 (including Xbox), rather than allowing the Store to use logo images from your app’s packages.

> [!IMPORTANT]
> If your app supports Xbox, you must provide certain images here in order for the listing to appear properly in the Store.

For more info, see [Store logos](./screenshots-and-images.md#store-logos).

### Trailers and additional assets

You can submit additional assets for your product, including video trailers and promotional images. These are all optional, but we recommend that you consider uploading as many of them as possible. These images can help give customers a better idea of what your product is and make a more enticing listing.

For more info, see [Trailers and additional assets](./screenshots-and-images.md#trailers-and-additional-assets).

## Supplemental fields

The fields in this section are all optional. Review the info below to determine if providing this info makes sense for your submission. In particular, the **Short description** is recommended for most submissions. The other fields may help provide an optimal experience for your product in different scenarios.

### Short title

A shorter version of your product’s name. If provided, this shorter name may appear in various places on Xbox One (during installation, in Achievements, etc.) in place of the full title of your product.

This field has a 50 character limit.

### Voice title

An alternate name for your product that, if provided, may be used in the audio experience on Xbox One when using Kinect or a headset.

This field has a 255 character limit.

### Short description

A shorter, catchy description that may be used in the top of your product’s Store listing. If not provided, the first paragraph (up to 500 characters) of your longer [description](#description) will be used instead. Because your description also appears below this text, we recommend providing a short description with different text so that your Store listing isn’t repetitive.

For games, the short description may also appear in the Information section of the Game Hub on Xbox One.

For best results, keep your short description under 270 characters. The field has a 1000 character limit, but in some views, only the first 270 characters will be shown (with a link available to view the rest of the short description).

## Additional information

The items described below help customers discover and understand your product.

:::image type="content" source="../msix/images/msix-additional-info.png" lightbox="../msix/images/msix-additional-info.png" alt-text="A screenshot showing additional information requirements for listing of a MSIX/PWA app.":::

## Keywords

Keywords (formerly called Search terms) are single words or short phrases that are not displayed to customers, but can help your make your app discoverable in the Store when customers search using those keywords. You can include up to 7 keywords with a maximum of 40 characters each, and can use no more than 21 separate words across all keywords.

When adding keywords, think about the words that customers might use when searching for apps like yours, especially if they're not part of your app's name. Be sure not to use any keywords that are not actually relevant to your app.

You can also use AI-Generated keywords for your app. You just need to enter the app description and AI will recommend you keywords for your app. You will see the recommended keywords in the keyword field dropdown. You can click on any recommended keyword to add it to your app submission.

## Copyright and trademark info

If you'd like to provide additional copyright and/or trademark info, enter it here. This field has a 200 character limit.

## Additional license terms

Leave this field blank if you want your app to be licensed to customers under the terms of the **Standard Application License Terms** (which are linked to from the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905)).

If your license terms are different from the **Standard Application License Terms**, enter them here.

If you enter a single URL into this field, it will be displayed to customers as a link that they can click to read your additional license terms. This is useful if your additional license terms are very long, or if you want to include clickable links or formatting in your additional license terms.

You can also enter up to 10,000 characters of text in this field. If you do that, customers will see these additional license terms displayed as plain text.

## Developed by

Enter text here if you want to include a **Developed by** field in your app's Store listing. (The **Published by** field will list the publisher display name associated with your account, whether or not you provide a value for the **Developed by** field.)

This field has a 255 character limit.
