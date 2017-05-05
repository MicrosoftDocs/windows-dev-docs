---
author: jnHs
Description: If your app uses an AdMediatorControl or AdControl to display banner ads, you could increase your ad fill rate and revenue by showing Microsoft affiliate ads in your app.
title: About affiliate ads
ms.assetid: 7B5478FB-7E68-4956-82EF-B43C2873E3EF
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# About affiliate ads

If your app uses an **AdMediatorControl** or **AdControl** to display banner ads, you could increase your revenue and ad fill rate by showing Microsoft affiliate ads in your app for products in the Store. When users click the affiliate ads and buy products within a given attribution window, you earn revenue on approved purchases.

Here's how this program works:

* After you [opt-in to the Microsoft affiliate ads program](#opt-in) in Dev Center, Microsoft picks ads for top products in the Store to serve to your app. These products may include apps, games, music, movies, hardware, and software.

 > **Note** Microsoft only serves affiliate ads into the following ad unit sizes: 300 x 50, 480 x 80, 160 x 600, 300 x 250, or 728 x 90.

* Microsoft will serve affiliate ads to your app only when no ads from other ad networks are available. This is intended to help to monetize your unfilled impressions and maximize your ad fill rate.
* When a user clicks an affiliate ad and buys any product in the Store within a given attribution window, you will be paid a revenue share or a fixed commission for the purchase (up to 10% commission).

  To be eligible for a commission, the affiliate ad must result in an eligible sale in either the Store on Windows 10 devices or the web-based Store; sales in the Store on Windows 8.x devices are not eligible for a commission. Affiliate ads for digital Store products (including apps, games, music and movies) are only served on Windows 10 devices. Affiliate ads for products in the web-based Store (including devices and software) are served on Windows 10 and Windows 8.x devices.

    > **Note**  You can get paid for *any* product the user purchases within the attribution window, not just the product that was promoted in your app. For free apps that are promoted in your app, you can earn a revenue share for in-app purchases made by the user within the attribution window.

* Any earnings you accrue in connection with the Microsoft affiliate ads program will be distributed to you in the [payout account you set up in Dev Center](setting-up-your-payout-account-and-tax-forms.md), along with your other advertising earnings.
* To track the performance of the affiliate ads in your app, refer to the [affiliates performance report](affiliates-performance-report.md). You can track daily purchases made through affiliate ads in your app and the revenue share that you received.  

  > **Note** After a user buys a product in the Store, there is a 45 day waiting period before the purchase can be approved for the affiliate ads program. Because of this waiting period, the **Estimated revenue**, **Purchases (approved)** and **Purchases (pending approval)** data on the [affiliates performance report](affiliates-performance-report.md) for a given day can change after purchases are approved or rejected.

<span id="opt-in" />
## How to opt in to the affiliate ads program

To opt in to the Microsoft affiliate ads program:

1. Go to the **Monetization** &gt; **Monetize with ads** page in the Windows Dev Center dashboard.
2. In the **Microsoft affiliate ads** section, check the **Show Microsoft affiliate ads in my app** box.

After you check or uncheck this box, you do not need to republish your app for the changes to take effect.


## Related topics


* [Monetize with ads](monetize-with-ads.md)
* [Affiliates performance report](affiliates-performance-report.md)
