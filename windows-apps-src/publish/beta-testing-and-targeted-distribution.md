---
author: jnHs
Description: The Windows Dev Center dashboard gives you the option to make your app available only to specified people so that you can have testers try it out before you offer it to the public.
title: Beta testing and targeted distribution
ms.assetid: 38E4ED22-D6C1-40D8-9B16-6B3E51BD962E
ms.author: wdg-dev-content
ms.date: 08/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: high
---

# Beta testing and targeted distribution

No matter how carefully you test your app, there’s nothing like the real-world test of having other people use it. The Windows Dev Center dashboard gives you options to make an app submission available only to specified people so that you can have testers try it out before you offer it to the public. Your testers may discover issues that you’ve overlooked, such as misspellings, confusing app flow, or errors that could cause the app to crash. You’ll then have a chance to fix those problems before you release the submission to the public, resulting in a more polished final product.

We provide several ways to limit distribution of your apps to only your testers, without needing to create a separate version of your app with a different name and package identity. (Of course, you can create a separate app for testing only if you prefer. If you do, make sure to give it a different name from what you intend to use as the final, public app name.)

The method of distributing your app to testers depends on which operating systems your app targets. Below you'll find options that work for Windows 10 and for Windows Phone 8.1 and earlier.

## Making your app available to testers on Windows 10 devices

We provide two options that let you limit distribution of your apps to only certain people on Windows 10 devices.

### Package flights

If you have published your app already, you can create package flights to distribute a different set of packages to the people that you specify. You can even create multiple package flights for the same app to use with different groups of people. This is a great way to try out different packages simultaneously, and you can pull packages from a flight into your non-flighted submission if you decide the packages are ready to distribute to everyone.

For more info, see [Package flights](package-flights.md).

> [!NOTE]
> To distribute specific packages to a random selection of your Windows 10 customers in a set percentage, rather than to a designated group of specific customers, you can use [gradual package rollout](gradual-package-rollout.md). You can also combine rollout with your package flights if you want to gradually distribute an update to one of your flight groups.

<span id="hide" />
### Hiding the app in the Store and using promotional codes

If you want to limit distribution of an app to only a certain group of testers, **without** first publishing a submission that's broadly available, you can use the same [app submission process](app-submissions.md) as any app you submit. To allow only certain people to get the app free of charge, and prevent other customers from seeing its listing or downloading it, do the following:

-   In your submission, in the [Visibility](set-app-pricing-and-availability.md#visibility) section of the **Pricing and availability** page, select **Make this product available but not discoverable in the Store**.  Choose the option for **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 device**. This prevents anyone from finding your app in the Store via searching or browsing.
-   After the app passes certification, [generate promotional codes](generate-promotional-codes.md) for the app and distribute them to your testers. You can generate codes that allow up to 1600 redemptions for a single app in a six month period. These codes will give your testers a direct link to the app’s listing, and will allow them to download it for free, even if you have set a price for it when you created your submission.

After you distribute the promotional code links to your testers, they can try it out and give you feedback to help you improve the app. Then, when you’re ready to make your app available to the public, create a new submission and change the **Visibility** option to **Make this app available and discoverable in the Store** (along with any other changes you'd like).

Here are some things to keep in mind when doing this:

-   You can give your testers an updated version of your app at any time by creating a new submission. Make sure to keep the **Visibility** option set to **Make this product available but not discoverable in the Store** with the **Stop acquisition** option. The testers will get the update after it goes through the certification process, but no one else will be able to get it.
-   Your testers must have a Windows 10 device on which they can install the app. (However, your app doesn't have to include Windows 10 packages in order to use this method of testing.)
-   You can create more [promotional codes](generate-promotional-codes.md) to distribute at any time (up to 1600 redemptions per app every six months).
-   You can’t revoke access to the app after your testers download it. Once they have downloaded the app, they can continue to use it, and they’ll get any updates that you subsequently publish.
-   You will need to determine how you’d like to collect feedback from your testers. Consider providing a link in the beta app that lets your testers easily give feedback via email or via [Feedback Hub](../monetize/launch-feedback-hub-from-your-app.md).
-   You can review [analytic reports](analytics.md) for your app, including usage and health reports and any ratings or reviews left by your testers.
-   You can include add-ons when you distribute your app to testers. Since you probably don’t want to charge them, make sure to set the price for the add-ons to **Free** while you’re doing your testing. Then, when you make the app available to other customers, you can create a new submission for each add-on to change its price.


## Other methods for distributing apps to testers

You can also limit distribution of your app only to a targeted group of people by using the additional options in the [Visibility](set-app-pricing-and-availability.md#visibility) section of the **Pricing and availability** page of an app submission. Keep in mind that these options don't work for customers on all OS versions. Specifically, none of them work for customers on Windows 8 or Windows 8.1.

### Targeted distribution to customers with a link to your app's listing

With this option, only people with a direct link to your app's listing can download it. You can find this **URL** on the [App identity](view-app-identity-details.md) page in the dashboard. No customers will be able to find the app by searching or browsing the Store, but anyone with the link can download it on a device running Windows Phone 8.1 or earlier or on Windows 10. 

> [!NOTE]
> In order for your testers to download the app at no cost, you must set its price to **Free**.

To use this option, select **Make this product available but not discoverable in the Store** in the [Visibility](set-app-pricing-and-availability.md#visibility) section of the **Pricing and availability** page. Then choose the option for **Direct link only: Any customer with a direct link to the product’s listing can download it, except on Windows 8.x.**.  


### Targeted distribution to customers with specified email addresses

For testing **on Windows Phone 8.1 and earlier only**, this option provides a way to limit distribution of your app. Only the people whose email addresses (associated with their Microsoft accounts) that you enter in the box can download your app by using the direct link to its listing.

> [!IMPORTANT]
> People with the email addresses you enter will only be able to download the app on devices running Windows Phone 8.1 or earlier.
 
You can find your app's direct link on the [App identity](view-app-identity-details.md) page in the dashboard. No customers will be able to find the app by searching or browsing the Store, and even if they have the link to your app's listing, they won't be able to download the app unless they are using a Microsoft account associated with an email address that you provided when submitting this app.

> [!NOTE]
If you use this option, you can still make the app available for testers on Windows 10 devices by [generating promotional codes](generate-promotional-codes.md) as described above. Anyone with one of your app's promotional codes can download it on a Windows 10 device, even if you didn't enter their email here.

To use this option, select **Make this product available but not discoverable in the Store** in the [Visibility](set-app-pricing-and-availability.md#visibility) section of the **Pricing and availability** page. Then choose the option for **Individuals on Windows Phone 8.x only: Only people you specify below can download this product on a Windows Phone 8.x device. Anyone with a direct link and a promotional code may download the product on a Windows 10 device.** 

If you choose this option, keep the following things in mind:

-   This option can only be selected if you have never previously published the app with the [Visibility](set-app-pricing-and-availability.md#visibility) option set to **Make this app available in the Store**.
-   Your app must be priced **Free** in order for your testers to download it at no cost.
-   Your testers can only download the app on Windows Phone 8.1 and earlier. Testers must have a retail Windows Phone device in order to use the app, but the device doesn’t need to be unlocked or registered.
-   Your testers will need to have a Microsoft account in order to access the Microsoft Store and download your app. You’ll need to know the email address associated with each tester’s Microsoft account in order to add them to your list. To create a new Microsoft account, testers can go to [Microsoft account setup](http://go.microsoft.com/fwlink/p/?LinkId=618945).
-   You can provide up to 10,000 email addresses in the text box.
-   Email addresses must be separated with semicolons.
-   You can add additional addresses later, but will need to create a new submission to do so.
-   You can’t revoke access to the app after your testers download it. Once they have downloaded the app, they can continue to use it, and they’ll get any updates you submit.
