---
description: You can help customers discover your app by linking to your app's listing in the Microsoft Store.
title: Link to your app
ms.assetid: 5420B65C-7ECE-4364-8959-D1683684E146
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, link, windows store protocol, linking to an app, link to app
ms.localizationpriority: medium
---
# Link to your app

You can help customers discover your app by linking to your app's listing in the Microsoft Store on Windows.

### Getting the link to your app's Store listing

To get the URL for your app's Store listing, navigate to the app's [Product Identity](../view-app-identity-details.md) page in the **Product management** section of [Partner Center](https://partner.microsoft.com/). The URL is in the format **`https://apps.microsoft.com/store/detail/<your app's Store ID>`**.

When a customer clicks this link, it opens the web-based listing page for your app. From there, customers on Windows devices can launch the Microsoft Store to download and install your app.

### Linking to your app's Store listing with the Microsoft Store badge

You can link directly to your app's listing with a custom badge to let customers know your app is in the Microsoft Store.

To create your badge, visit the [Microsoft Store badges](https://developer.microsoft.com/store/badges) page. You'll need to have your app's 12-character **Store ID** in order to generate the badge and link. You can find your app's **Store ID** in [Partner Center](https://partner.microsoft.com/) on the [Product identity](../view-app-identity-details.md) page in the **Product management** section.

> [!NOTE]
> See [App marketing guidelines](#microsoft-store-marketing-guidelines-for-apps) for info and requirements related to your use of the Microsoft Store badge.

### Linking directly to your app in the Microsoft Store

You can create a link that launches the Microsoft Store and goes directly to your app's listing page without opening a browser by using the **ms-windows-store:** URI scheme.

These links are useful if you know your users are on a Windows device and you want them to arrive directly at the listing page in the Store. For example, you might want to use this link after checking user agent strings in a browser to confirm that the user's operating system supports the Store, or when you are already communicating via a UWP app.

To use this URI scheme to link directly to your app's Store listing, append your app's Store ID to this link:

**`ms-windows-store://pdp/?ProductId=<your app's Store ID>`**

For more about launching the Microsoft Store app using a URI, see [Launch the Microsoft app](/windows/uwp/launch-resume/launch-store-app).

## Microsoft Store marketing guidelines for apps

Learn how to promote your apps and content in the Microsoft Store. These guidelines cover how to use the assets that are available to you, along with recommendations for promoting your apps in print, TV, social media and digital advertising.

### Store badges

We’ve created special promotional badges to help you drive more customers to your app's listing in the Microsoft Store. These badges are available in 44 languages. Keep in mind that there are certain requirements you need to follow when using these images; these requirements, along with usage examples and guidelines, are available in a PDF file.

[![Download button](../images/downloadbutton.png)](https://download.microsoft.com/download/0/7/D/07DF43D4-B1A8-4D38-BC02-4903BB36CEE8/Microsoft_Store_Badge_Guidelines.pdf) **Microsoft Store Badge Guidelines (PDF, English)**


### Badge generator and images

You can use our [badge generator](https://apps.microsoft.com/store/app-badge) to generate HTML that displays the Store badge (in the language of your choice) and links directly to your app's Store listing.

You can also download the complete set of badge images (in PNG and PDF format) from the link below.

[![Download button](../images/downloadbutton.png)](https://download.microsoft.com/download/6/6/6/66641831-E662-4898-BB21-75D6C193A8F9/All%20Badges.zip) **All badge images**

### Device images

To promote your app, you may want to show how it looks when running on a Windows device. We have a variety of device chassis images for you to select from, including phones, tablets, laptops and PCs. Best practices and technical specifications for how to use these images can be found in the [Microsoft Store Marketing Guidelines](https://download.microsoft.com/download/0/7/D/07DF43D4-B1A8-4D38-BC02-4903BB36CEE8/Microsoft_Store_Badge_Guidelines.pdf).

[![Download button](../images/downloadbutton.png)](https://download.microsoft.com/download/1/A/5/1A58A23A-1388-4097-B441-A3E8DBC14849/Windows_Store_Device_Art.zip) **Windows device art**

### License to Microsoft Marks

*Microsoft Marks* means the *Microsoft badge* described on the [badge generator](https://apps.microsoft.com/store/app-badge) page. To use these badges, you must:

-   Have your app or other content available in the Microsoft Store, or be part of the [Microsoft Affiliate Program](https://www.microsoft.com/microsoft-365/business/microsoft-365-affiliate-program).

-   If you are registered as an app developer in Partner Center, comply with the [“License to Microsoft Marks”](/legal/windows/agreements/app-developer-agreement#license_to_mark) section of the App Developer Agreement.

-   If you are not registered as an app developer in Partner Center, Microsoft grants you a worldwide, nonexclusive, nontransferable, royalty-free license to use the badges solely as described in the Microsoft Store Badge Guidelines. Microsoft may change these guidelines, but if it does, Microsoft will use reasonable means to redirect you to any new URLs where these specifications are posted. Microsoft reserves all rights not expressly granted herein.

-   Follow the logo usage specifications described in the [Microsoft Store Badge Guidelines](https://download.microsoft.com/download/0/7/D/07DF43D4-B1A8-4D38-BC02-4903BB36CEE8/Microsoft_Store_Badge_Guidelines.pdf).

Microsoft is the sole owner of the Microsoft Marks and associated goodwill, and the sole beneficiary of the goodwill associated with your use of the Microsoft Marks. Microsoft may revoke this license at any time and at its sole discretion.

## The new Microsoft Store badges are here

We've refreshed the Microsoft Store badge to feature the new logo, with a more refined call-to-action to give users more confidence to acquire your app. These will go live on our [badge creator page](https://apps.microsoft.com/badge) on **August 7, 2024** in all supported languages.

### How do I get the new badge?

If you're a developer with an app published on the Microsoft Store, and if you've been using these badges externally using code from our badge generator page linked above, you'll get the new badge automatically once it goes live. However, if you've got a custom implementation of the badge on your website, you should switch to the new design.

### Old

| <img src="../../images/old-badge-dark.png" width="200" alt="Old Store badge for dark mode">  | <img src="../../images/old-badge-light.png" width="200" alt="Old Store badge for light mode"> |
| ------------- | ------------- |

### New

| <img src="../../images/new-badge-dark.png" width="256" alt="New Store badge for dark mode">  | <img src="../../images/new-badge-light.png" width="256" alt="New Store badge for light mode"> |
| ------------- | ------------- |


If you don't have a badge yet, visit the [Microsoft Store badge creator page](https://apps.microsoft.com/badge) to choose the options that work best for your badge and hit Generate to create one for your app.

#### Some tips to make the most of your badge
* We'll have a different badge color for dark and light modes, and you should pick what works best for your website or allow it to be automatically detected.
* We recommend using the standard JavaScript-based badge, but if your website doesn't use JavaScript, you'll be able to create a non-JS version too.
* Remember to add a campaign ID with a unique string so you can better track your traffic sources on the Microsoft Store Partner Center dashboard.

Please avoid altering the badge – including changing the color, the text, or the elements within.
