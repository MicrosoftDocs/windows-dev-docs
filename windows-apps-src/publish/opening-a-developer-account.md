﻿---
ms.assetid: 284EBA1F-BFB4-4CDA-9F05-4927CDACDAA7
title: Opening a developer account
description: Here's an overview of how to register for a Winodws developer account for the Microsoft Store and other Microsoft programs in Partner Center.
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Opening a developer account

Here's an overview of how to register for a Windows developer account in [Partner Center](https://partner.microsoft.com/dashboard).

> [!NOTE]
> When you sign up for a developer account, we'll use the email address you provide in your contact info to send email communications related to your account. At times, these may include informational emails about our programs. If you choose not to receive these informational emails by [opting out](http://go.microsoft.com/fwlink/p/?LinkId=533280), be aware that we will still send you transactional emails (for example, to let you know that your app has passed certification or that a payment is on the way). These emails are a necessary part of your account, and unless you close your account, you will continue to receive these transactional emails.

## The account signup process

> [!NOTE]
> In some cases, the screens and fields you see when registering for a developer account may vary slightly from what is outlined below. The basic information and process will be the same.

1.  Go to the [registration page](http://go.microsoft.com/fwlink/p/?LinkId=615100) and click **Sign up**.
2.  If you're not already signed in with a Microsoft account, sign in now, or create a new Microsoft account. The Microsoft account you use here will be what you use to sign in to your developer account.
3.  Select the [country/region](account-types-locations-and-fees.md#developer-account-and-app-submission-markets) in which you live, or where your business is located. You won't be able to change this later.
4.  Select your [developer account type](account-types-locations-and-fees.md) (individual or company). You won't be able to change this later, so be sure to choose the right type of account.
5.  Enter the **publisher display name** that you wish to use (50 characters or fewer). Select this carefully, as customers will see this name when browsing and will come to know your apps by this name. For company accounts, be sure to use your organization's registered business name or trade name. Note that if you enter a name that someone else has already selected, or if it appears that someone else has the rights to use that name, we will not allow you to use that name. 

   > [!NOTE]
   > Make sure you have the rights to use the name you enter here. If someone else has trademarked or copyrighted the name you picked, your account could be closed. See the [App Developer Agreement](https://docs.microsoft.com/legal/windows/agreements/app-developer-agreement) for more info. If someone else is using a publisher display name for which you hold the trademark or other legal right, [contact Microsoft](http://go.microsoft.com/fwlink/p/?LinkId=233777).    

6.  Enter the contact info you want to use for your developer account.

   > [!NOTE]
   > We'll use this info to contact you about account-related matters. For example, you'll receive an email confirmation message after you complete your registration. After that, we'll send messages when we pay you, or if you need to fix something with your account. We may also send informational emails as described above, unless you opt out of receiving non-transactional emails.

   If you are registering as a company, you'll also need to enter the name, email address, and phone number of the person who will approve your company's account.

7.  Click **Next** to move on to the **Payment** section.

8.  Enter your payment info for the one-time registration fee. If you have a promo code that covers the cost of registration, you can enter that here. Otherwise, provide your credit card info (or PayPal in supported markets). Note that prepaid credit cards can't be used for this purchase. When you are finished, click **Next** to move on to the **Review** screen.

9.  Review your account info and confirm that everything is correct. Then, read and accept the terms and conditions of the [App Developer Agreement](https://docs.microsoft.com/legal/windows/agreements/app-developer-agreement). Check the box to indicate you have read and accepted these terms.

10.  Click **Finish** to confirm your registration. Your payment will be processed and we'll send a confirmation message to your email address.

After you've signed up, your account will go through the verification process. For individual accounts, we check to make sure another company isn't already using your publisher display name. For company accounts, the process takes a little longer, as we also need to confirm that you’re authorized to set up the account for your company. This verification can take from a few days to a couple of weeks, and often includes a phone call to your company. You can check your verification status on the **Account settings** page.


## Additional guidelines for company accounts

> [!IMPORTANT]
> To allow multiple users to access your developer account, we recommend using Azure Active Directory to assign roles to individual users (rather than sharing access to the Microsoft account). Each user can then access the developer account by signing in to Partner Center with their individual Azure AD credentials. For more info, see [Manage account users](manage-account-users.md).

If you will need to have multiple people access the company account by signing in with the Microsoft account that opened it (rather than as individual users added to the account), the following guidelines may be helpful:

-   Create the Microsoft account using an email address that doesn't already belong to you or another individual, such as MyCompany_PartnerCenter@outlook.com. Don't use an email address at your company's domain, especially if your company already uses Azure AD. (As noted above, you can add additional users from your company's Azure AD later.)
-   Limit access to this Microsoft account to the smallest possible number of users.
-   Set up a corporate email distribution list that includes everyone who needs to access the developer account, and add this email address to the [security info associated with the Microsoft account](https://account.microsoft.com/security). This allows all of the employees on the list to receive security codes sent to this alias. If setting up a distribution list is not feasible, you can add an individual's email address to your security info, but the owner of that email address will be the only one who can access and share the security code when prompted (such as when new security info is added to the account, or when it is accessed from a new device).
-   Add a company phone number to the Microsoft account's security info. Try to use a number that does not require an extension and is accessible to key team members.
-   In general, have developers use [trusted devices](https://support.microsoft.com/help/12369/microsoft-account-add-a-trusted-device) to log in to your company's developer account. All key team members should have access to these trusted devices. This will reduce the need for security codes to be sent when accessing the account. There is a limit to the number of codes that can be generated per account, per week.
-   If you need to allow access to the account from a non-trusted PC, limit that access to a maximum of five developers. Ideally, these developers should access the account from machines that share the same geographical and network location.
-   Frequently review your company’s security info at https://account.microsoft.com/security to make sure it's all current.


## Microsoft account security

We use security info that you provide to raise the security level of your Microsoft account by associating it with multiple forms of identification. This makes unauthorized access to your Microsoft account (and your developer account) substantially more difficult. Also, if you ever forget your password or if someone else tries to access your account, we’ll be able to reach you to confirm ownership and/or reestablish appropriate control of your account.

You must have at least two email addresses and/or phone numbers on your Microsoft account. We recommend adding as many as possible. Remember that some security info must be confirmed before it will be valid. Also, make sure to review your security info frequently and ensure it's up to date. You can manage your security info by going to https://account.microsoft.com/security and signing in with your Microsoft account. See [Security info & security codes](https://support.microsoft.com/help/12428/microsoft-account-security-info-and-security-codes) for more info.

When you sign in to Partner Center using your Microsoft account, the system may request that you verify your identity by sending a security code that you must supply to complete the sign-in process. We recommend designating PCs that you use frequently as *trusted devices*. When you sign in from a trusted device, you usually won’t be prompted for a code, although you may occasionally be prompted in specific situations or if you haven’t signed in on that device in a long time. See [Add a trusted device to your Microsoft account](https://support.microsoft.com/help/12369/microsoft-account-add-a-trusted-device) for more info.


## Closing your account

Developer accounts don't expire, so there is no need to renew your account in order to keep it open. If you decide to close your account completely, you can do so by contacting support.

When you close your account, it's important to understand what happens to any app that you have published in the Microsoft Store:

-   Your app's current customers will still be able to use the app. However, they will not be able to make in-app purchases.
-   Even though the app is still available to customers who have previously acquired it, your app listing will be removed from the Store. No new customers will be able to acquire your app.
-   Your app's name will be released for potential use by another developer.
-   If you have a balance due from previous app sales, you can request payment for that balance even if the amount due does not meet the usual payment threshold.
