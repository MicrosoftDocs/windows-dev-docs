---
author: jnHs
Description: You can generate promotional codes for an app or add-on that you have published in the Microsoft Store.
title: Generate promotional codes
ms.assetid: 9B632266-64EC-4D62-A4C4-55B6643D8750
ms.author: wdg-dev-content
ms.date: 03/28/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, promo code, promo codes, token, tokens
ms.localizationpriority: high
---

# Generate promotional codes


You can generate promotional codes for an app or add-on that you have published in the Microsoft Store. Promotional codes are an easy way to give influential users free access to your app or add-on. You might also use promotional codes to address customer service scenarios by giving users free access to your app or add-on, or for [beta testing](beta-testing-and-targeted-distribution.md) with Windows 10.

Each promotional code has a corresponding unique redeemable URL that you can distribute to a single customer or to a group of customers. A customer can simply click the URL to redeem the code and install your app or add-on from the Microsoft Store.

> [!TIP]
> You can use [targeted push notifications](send-push-notifications-to-your-apps-customers.md) to distribute a promotional code to a segment of your customers. When doing so, be sure to use a promotional code that allows multiple customers to use the same code.

In the Windows Dev Center dashboard, you can:

-   Order a set of promotional codes for your app or add-on.
-   Download a fulfilled promotional codes order.
-   Review promotional code usage.

> [!NOTE]
> You can generate promotional codes even if you have selected **Make this product available but not discoverable in the Store** with the **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 device** option in your submission's [Discoverability](choose-visibility-options.md#discoverability) section.

Note that your app must pass the final publishing phase of the [app certification process](the-app-certification-process.md) before customers can redeem a promotional code to install it.


## Promotional code policies

Be aware of the following policies for promotional codes:

-   You can generate promotional codes for any app or add-on (with the exception of subscription add-ons) that you published to the Microsoft Store. Customers can redeem the codes on any version of Windows that is supported by your app or add-on.
-   Promotional codes expire 6 months after the date you order them (unless you choose an earlier expiration date).
-   For each of your apps or add-ons, you can generate codes that allow up to 1600 redemptions every 6 months. The 6 month period begins when the first promotional code order is submitted, even if you choose an earlier expiration date. The total of 1600 redemptions per product applies to both single-use codes and codes that can be used multiple times.
-   You must follow the requirements defined in the [App Developer Agreement](https://docs.microsoft.com/legal/windows/agreements/app-developer-agreement), including section **3k. Promotional Codes**.


## Order promotional codes

To order promotional codes for an app or add-on that you published to the Microsoft Store:

1.  In the left navigation menu of the Windows Dev Center dashboard, expand **Attract** and then select **Promo codes**.

2.   On the **Promotional codes** page, click **Order codes**.

3.  On the **New promotional codes order** page, enter the following:
    -   Select the app or add-on for which you want to generate codes. (Note that you can't generate promotional codes for subscription add-ons.)
    -   Specify a name for the order. You can use this name to differentiate between different orders of codes when reviewing your promotional code usage data.
    -   Select the order type. You can choose to generate a set of promo codes that can each be used once, or you can choose to generate one promo code that can be used multiple times.
    -   Specify the number of codes to order (if generating a set of codes) or the number of times the code can be redeemed (if generating one code to be used multiple times).
    -   Specify when the promotional codes should become active. To choose a specific start date and time, clear the **Codes are active immediately** check box. Otherwise, the codes will become active right away.
    -   Specify when the promotional codes should expire. To choose a specific expire date and time earlier than 6 months, clear the **Codes expire after 6 months** check box.

4.  Click **Order codes**. You'll then be returned to the **Promotional codes** page, where you'll be able to see your new order in the summary table of promotional code orders for that app.


## Download and distribute promotional codes

To download a fulfilled promotional code order and distribute the codes to customers:

1.  In the left navigation menu of the Windows Dev Center dashboard, expand **Attract** and then select **Promo codes.**
2.  Click the **Download** link for the promotional code order, then save the generated file to your computer. This file contains information about your promotional codes order in tab-separated value (.tsv) format.
3.  Open the .tsv file in the editor of your choice. For the best experience, open the .tsv file in an application that can display the data in a tabular structure, such as Microsoft Excel. However, you can open the file in any text editor.

    The file contains the following columns of data for each code:

    -   **Product name**: The name of the app or add-on that the code is associated with.
    -   **Order name**: The name of the order in which this code was generated.
    -   **Promotional code**: The code itself. This is a 5x5 string of alphanumeric characters separated by hyphens. For example: DM3GY-M2GYM-6YMW6-4QHHT-23W2Z
    -   **Redeemable URL**: The URL that a customer can use to redeem the code and install your app or add-on. The URL has the following format: http://go.microsoft.com/fwlink/?LinkId=532540&mstoken=&lt;promotional_code>
    -   **Start date**: The date this code became active.
    -   **Expire date**: The date this code expires.
    -   **Code ID**: A unique ID for this code.
    -   **Order ID**: A unique ID for the order in which this code was generated.
    -   **Given to**: An empty field that you can use to keep track of which customer you gave the code to.
    -   **Available**: The number of times the code is still available to redeem (at the time the file was generated).
    -   **Redeemed**: The number of times that the code has been redeemed (at the time the file was generated).

4.  Distribute the redeemable URLs to your customers via any communication format you prefer (for example targeted notifications, email, SMS messages, or printed cards). We recommend that your communication includes the following:
    -   An explanation of which app or add-on the promotional code is for, and optionally a description of why the customer is receiving the code.
    -   The redeemable URL for the code.
    -   Instructions that guide the customer to visit the redeemable URL, log in using their Microsoft account, and follow the instructions to download and install your app.


## Code redemption user experience

After you distribute a promotional code (or its redeemable URL) to a customer, they can use that URL to get the product for free. Clicking the redeemable URL will launch an authenticated **Redeem your code** page at <https://account.microsoft.com/billing/redeem>. This page includes a description of the app the user is about to redeem. If the customer is not logged in with their Microsoft account, they may be prompted to do so. Your customer can also visit <https://account.microsoft.com/billing/redeem> and enter the code directly.

> [!IMPORTANT]
> We recommend that you don't distribute promotional codes to your customers until your product has completed the publishing process (even if you have selected **Make this product available but not discoverable in the Store**). Customers will see an error if they try to use a promotional code for a product which hasn't been published yet.

After the customer clicks **Redeem**, the Microsoft Store will open to the overview page for the app (if they are on a Windows 10 or Windows 8.1 device), where they can click **Install** to download and install the app for free. If the customer is on a computer or device that does not have the Microsoft Store installed, the link will launch the Microsoft Store web page for the app. The code will be applied to the customer's Microsoft account, so they can later download the app on a Windows device (that is associated with the same Microsoft account) for free.

> [!NOTE]
> In some cases the customer may see a **Buy** button instead of **Install**, even though the app was successfully redeemed via the promotional code. The customer can click **Buy** to install the app for no charge.


## Review your promotional codes

To review a detailed summary of promotional code orders for your apps and add-ons, navigate to the **Promotional codes** page (in the left navigation menu of the Dev Center dashboard, expand **Attract** and then select **Promo codes**). You can review the following details for all of your current and inactive promotional codes:
-   Order name
-   App or add-on
-   Start date
-   Expire date
-   Available
-   Redeemed

You can also [download](#download-and-distribute-promotional-codes) an order from this table.

 
