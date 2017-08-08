---
author: jnHs
Description: The Store listings section of the app submission process is where you provide the text and images that customers will see in your app's Store listing.
title: Create app Store listings
ms.assetid: 50D67219-B6C6-4EF0-B76A-926A5F24997D
ms.author: wdg-dev-content
ms.date: 08/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Create app Store listings


The **Store listings** section of the [app submission process](app-submissions.md) is where you provide the text and [images](app-screenshots-and-images.md) that customers will see in your app's Store listing.

> [!NOTE]
> We have recently updated the options on this page. If you had an in-progress submission from before the newer options were available, that submission will still show the older options. You can delete that submission and then create a new one if you want to use the new options for that app. Otherwise, the newer options will become available with the next update after you publish your in-progress submission.

Many of the fields in a **Store listing** are optional, but we suggest providing multiple images and as much info as possible to make your listing stand out. The minimum required for the **Store listings** step to be considered complete is a text description and at least one [screenshot](app-screenshots-and-images.md#screenshots).

> [!TIP]
> You can also [import and export Store listings](import-and-export-store-listings.md) if you'd like to enter your listing info offline in a .csv file, rather than providing this info directly in the dashboard. This may be especially convenient if you have listings in many languages.

By default, we'll use the same Store listing (per language) for all of your targeted operating systems. If you'd like to use a customized Store listing for a specific operating system, you can [create platform-specific Store listings](create-platform-specific-store-listings.md). Your default listing will always be shown to customers on Windows 10.

## Store listing languages

You must complete the **Store listing** page for at least one language. We recommend providing a Store listing in each language that your packages support, but you have flexibility to remove languages for which you don’t wish to provide a Store listing. You can also create Store listings in additional languages which aren’t supported by your packages.

> [!NOTE]
> If your submission includes packages already, we’ll show the [languages](supported-languages.md) supported in your packages on the submission overview page (unless you remove any of them).

To add or remove languages for your Store listings, click **Add/remove languages** from the submission overview page. If you‘ve already uploaded packages, you’ll see their languages listed in the **Languages supported by your packages** section. To remove one or more of these languages, click **Remove**. If you later decide to include a language that you previously removed from this section, you can click **Add**.

In the **Additional Store listing languages** section, you can click **Manage additional languages** to add or remove languages that are *not* included in your packages. Check the boxes for the languages that you’d like to add, then click **Update**. The languages you’ve selected will be displayed in the **Additional Store listing languages** section. To remove one or more of these languages, click **Remove** (or click **Manage additional languages** and uncheck the box for languages you’d like to remove).

When you have finished making your selections, click **Save** to return to the submission overview page.

> [!NOTE]
> When creating a Store listing in a language that isn't supported by your packages, you'll need to indicate which of your reserved app names should be displayed in that Store listing, since there isn't an associated package in that language from which to pull the name. The name you choose here only applies to the Store listing for this language and does not impact the name displayed when a customer installs the app.

To edit a Store listing, click the language name from the submission overview page.

At the top of the **Store listing** page are the fields associated with your default Store listing for the selected language. These fields will be shown to all of your customers, unless you have packages targeting earlier OS versions (Windows 8.x or earlier; Windows Phone 8.x or earlier) and create platform-specific Store listings to include different screenshots or info to display to customers on specified OS versions. For more info, see [Create platform-specific Store listings](create-platform-specific-store-listings.md).

## Description

The description field is where you can tell customers what your app does. This field is required, and will accept up to 10,000 characters of plain text.

For some tips on making your description stand out, see [Write a great app description](write-a-great-app-description.md).

## Release notes

If this is the first time you're submitting your app, you'll probably want to leave this field blank. For an update to an existing app, this is where you can let customer know what's changed in the latest release. This field has a 1500 character limit.

## Screenshots

One screenshot is required in order to submit your app. We recommend providing at least one screenshot for each device type that your app supports.

For more info, see [App screenshots and images](app-screenshots-and-images.md#screenshots).

## Store logos 

Store logos are optional images that you can upload to enhance the way your app is displayed to customers. You can also optionally specify that only images you upload here should be used in your app’s Store listing for Windows 10 customers, rather than allowing the Store to use logo images from your app’s packages.

> [!IMPORTANT]
> If your app supports Xbox, or if it supports Windows Phone 8.1 or earlier, you must provide certain images here in order for the listing to appear properly in the Store. 

For more info, see [Store logos](app-screenshots-and-images.md#store-logos).

## Additional art assets

You can submit additional assets for your product, including trailers and promotional images. These are all optional, but we recommend that you consider uploading as many of them as possible. These images can help give customers a better idea of what your product is and make a more enticing listing.

For more info, see [Additional art assets](app-screenshots-and-images.md#additional-art-assets).

## Additional information

The fields in this section are all optional, but can be used to help customers understand more about what your app does and what is required for the best experience. We suggest reviewing the options described below and providing any information that customers might need to know about your app, or that could help entice them to download it.

### App features

These are short summaries of your app's key features. They are displayed to the customer as a bulleted list in your app's Store listing, in addition to the Description. Keep these brief, with just a few words (and no more than 200 characters) per feature. You may include up to 20 features.

> [!NOTE]
> Your app features will appear bulleted in your Store listing, so don't add your own bullets.

### Additional system requirements

If needed, you can describe the hardware configurations that your app requires to work properly (beyond the info you provided in the **System requirements** section in [App properties](enter-app-properties.md#system-requirements). This is especially important if your app requires hardware that might not be available on every computer.

You can enter up to 11 items for both **Minimum hardware** and **Recommended hardware**.  They are displayed to the customer as a bulleted list in your app's listing. Keep these brief, with just a few words (and no more than 200 characters) per item.

The info you enter here will be shown to customers viewing your app's Store listing on Windows 10, version 1607 or later, along with the requirements you indicated on the product's properties page.

> [!NOTE]
> Your additional system requirements will appear bulleted in your Store listing, so don't add your own bullets.

### Developed by

Enter text here if you want to include a **Developed by** field in your app's Store listing. (The **Published by** field will list the publisher display name associated with your account, whether or not you provide a value for the **Developed by** field.)

This field has a 255 character limit.


## Shared fields

The items described below help customers discover and understand your product. The info you enter here will apply to all of your Store listings in a given language, regardless of operating system, even if you [create platform-specific Store listings](create-platform-specific-store-listings.md).

### Search terms

Search terms (formerly called keywords) are single words or short phrases that are not displayed to customers, but can help your app appear in search results related to the term. You can include up to 7 search terms with a maximum of 30 characters each, and can use no more than 21 separate words across all search terms.

When adding search terms, think about the words that customers might use when searching for apps like yours, especially if they're not part of your app's name. Be sure not to use any search terms that are not actually relevant to your app.


### Privacy policy

If you have a privacy policy for your app, enter its URL here. You are responsible for ensuring your app complies with privacy laws and regulations, and for providing a privacy policy, if required.

> [!IMPORTANT]
> Microsoft doesn't provide a default privacy policy for your app. Likewise, your app is not covered by any Microsoft privacy policy. To determine if your app requires a privacy policy, review the [App Developer Agreement](https://msdn.microsoft.com/library/windows/apps/hh694058) and the [Windows Store Policies](https://msdn.microsoft.com/library/windows/apps/dn764944.aspx#pol_10_5_1).

### Copyright and trademark info

If you'd like to provide additional copyright and/or trademark info, enter it here. This field has a 200 character limit.

### Additional license terms

Leave this field blank if you want your app to be licensed to customers under the terms of the **Standard Application License Terms** (which are linked to from the [App Developer Agreement](https://msdn.microsoft.com/library/windows/apps/hh694058)).

If your license terms are different from the **Standard Application License Terms**, enter them here.

If you enter a single URL into this field, it will be displayed to customers as a link that they can click to read your additional license terms. This is useful if your additional license terms are very long, or if you want to include clickable links or formatting in your additional license terms.

You can also enter up to 10,000 characters of text in this field. If you do that, customers will see these additional license terms displayed as plain text.

### Website

Enter the URL of the web page for your app. This URL must point to a page on your own website, not your app's web listing in the Store.

### Support contact info

Enter the URL of the web page where your customers can go for support on your app, or an email address that customers can contact for support.

> [!IMPORTANT]
> Microsoft doesn't provide your customers with support for your app.

