---
description: Partner Center gives you several options to let testers try out your app before you offer it to the public.
title: Beta testing and targeted distribution
ms.assetid: 38E4ED22-D6C1-40D8-9B16-6B3E51BD962E
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, beta test, limited distribution, beta, betas, testing, testers
ms.localizationpriority: medium
---
# Beta testing and targeted distribution

No matter how carefully you test your app, there’s nothing like the real-world test of having other people use it. Your testers may discover issues that you’ve overlooked, such as misspellings, confusing app flow, or errors that could cause the app to crash. You’ll then have a chance to fix those problems before you release the submission to the public, resulting in a more polished final product. 

Partner Center gives you several options to let testers try out your app before you offer it to the public.

Whichever method you choose, here are some things to keep in mind as you beta test your app.

- You can’t revoke access to the app after a tester has downloaded it. Once they have downloaded the app, they can continue to use it, and they’ll get any updates that you subsequently publish.
- You will need to determine how you’d like to collect feedback from your testers. Consider providing a link that lets your testers easily give feedback via email (or via [Feedback Hub](/windows/uwp/monetize/launch-feedback-hub-from-your-app), if confidentiality is not a concern). 
- You can review [analytic reports](analytics.md) for your app, including usage and health reports and any ratings or reviews left by your testers.
- You can include add-ons when you distribute your app to testers. Since you probably don’t want to charge them money for an add-on, you can [generate promotional codes](generate-promotional-codes.md) and distribute them to your testers to let them get the add-on for free, or you can set the price for the add-on to **Free** during  testing (then before you make the app available to other customers, create a new submission for the add-on to change its price). Note that each add-on can only be purchased once per Microsoft account, so the same tester won't be able to test the add-on acquisition process more than one time. 
- You can give your testers an updated version of your app at any time by creating a new submission with new packages. Your testers will get the update after it goes through the certification process, just like they got the original package, but no one else will be able to get it (unless you make additional changes, such as moving an app from **Private audience** to **Public audience** or changing the membership of groups who can get it).

## Private audience

If you want to let testers use your app before it’s available to others, and make sure that no one else can see its listing, use the **Private audience** option under [Visibility](publish-your-app/msix/visibility-options.md) (on the **Pricing and availability** page of your submission). This is the only method that lets you distribute your app to testers while completely preventing anyone else from seeing the app’s Store listing, even if they were able to type in its direct link. 

The **Private audience** option can only be used when you have not already published your app to a public audience. You can use this option with apps targeting any OS version, but your testers must be running Windows 10, version 1607 or later (including Xbox), and must be signed in with the Microsoft account associated with the email address that you provide.

For more info, see [Private audience](publish-your-app/msix/visibility-options.md#audience).


## Package flights

If you have published your app already, you can create package flights to distribute a different set of packages to the people that you specify. You can even create multiple package flights for the same app to use with different groups of people. This is a great way to try out different packages simultaneously, and you can pull packages from a flight into your non-flighted submission if you decide the packages are ready to distribute to everyone.

Package flights can be used with apps targeting any OS version, but your testers can only get the app if they are running Windows.Desktop build 10586 or later or Xbox One.

For more info, see [Package flights](package-flights.md).

## Hiding the app in the Store and using promotional codes

This option offers another way to limit distribution of an app to only a certain group of testers, while preventing anyone else from discovering your app in the Store (or acquiring it without a promotional code). However, unlike the private audience option, it could be possible for anyone to see your app’s listing if they have the direct link. If confidentiality is critical for your submission, we recommend publishing to a private audience instead.

Hiding the app and using promotional codes can be used with apps targeting any OS version, but your testers can only get the app if they are running Windows 10 or Windows 11.

To use this option:

- In the **Visibility** section of the **Pricing and availability** page, under [Discoverability](publish-your-app/msix/visibility-options.md#discoverability), select **Make this product available but not discoverable in the Store**. Choose the option for **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 or Windows 11 device**. 
- After the app passes certification, [generate promotional codes](generate-promotional-codes.md) for the app and distribute them to your testers. You can generate codes that allow up to 1600 redemptions for a single app in a six month period. These codes will give your testers a direct link to the app’s listing, and will allow them to download it for free, even if you have set a price for it when you created your submission.
- When you're ready to make your app available to the public, create a new submission and change the **Visibility** option to **Make this product available and discoverable in the Store** (along with any other changes you'd like).


## Targeted distribution with a link to your app's listing

With this, no customers will be able to find the app by searching or browsing the Store, but anyone with the direct link to its Store listing can download it on a device running on Windows 10 or Windows 11. Keep in mind that in order for your testers to download the app at no cost, you must set its price to **Free**.

To use this option:
- In the **Visibility** section of the **Pricing and availability** page, under [Discoverability](publish-your-app/msix/visibility-options.md#discoverability), select **Make this product available but not discoverable in the Store**. Choose the option for **Direct link only: Any customer with a direct link to the product’s listing can download it, except on Windows 8.x.**.
- After your product has been published, distribute the link (the **URL** on the [App identity page](view-app-identity-details.md)) to your testers so they can try it out.
- When you're ready to make your app available to the public, create a new submission and change the **Visibility** option to **Make this product available and discoverable in the Store** (along with any other changes you'd like).

