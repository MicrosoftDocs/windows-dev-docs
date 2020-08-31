---
ms.assetid: 284EBA1F-BFB4-4CDA-9F05-4927CDACDAA7
title: Opening a developer account
description: Here's an overview of how to register for a Windows developer account for Microsoft Store and other Microsoft programs in Partner Center.
ms.date: 3/30/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Opening a developer account

This article describes how to register for a Windows developer account in [Partner Center](https://partner.microsoft.com/dashboard).

> [!NOTE]
> When you sign up for a developer account, we'll use the email address you provide in your contact info to send messages related to your account. At times, these may include information about our programs. If you choose to [opt out](https://account.microsoft.com/account/Account?ru=https%3A%2F%2Faccount.microsoft.com%2Fprofile%2Fcontact-info&destrt=profile-landing) of these informational emails, be aware that we'll still send you transactional messages (for example, to let you know that your app has passed certification or that a payment is on the way). These transactional emails are a necessary part of your account, and unless you close your account, you'll continue to receive them.

## The account signup process

> [!NOTE]
> In some cases, the screens and fields you see when you register for a developer account may vary slightly from what's outlined in the following steps. But the basic information and process will match what these steps describe.

> [!NOTE]
> There is a known issue where users in some locales might be unable to finish completing their registration. Until we can confirm that it is resolved, we recommend that you manually change your browser's locale tag to **en-us** once you begin the sign-up process on partner.microsoft.com.

1.  Go to the [registration page](https://developer.microsoft.com/store/register) and select **Sign up**.
2.  If you're not already signed in with a Microsoft account, sign in now, or create a new Microsoft account. The Microsoft account you use here is what you'll use to sign in to your developer account.
3.  Select the [country/region](account-types-locations-and-fees.md#developer-account-and-app-submission-markets) where you live or where your business is located. You won't be able to change this later.
4.  Select your [developer account type](account-types-locations-and-fees.md) (individual or company). You won't be able to change this later, so be sure to choose the right type of account.
5.  Enter the **publisher display name** that you want to use (50 characters or fewer). Select this carefully, as customers will see this name when browsing and will come to know your apps by this name. For company accounts, be sure to use your organization's registered business name or trade name. If you enter a name that someone else has already selected, or if someone else has the rights to use that name, we won't permit you to use it.

    > [!NOTE]
    > Make sure you have the rights to use the name you enter here. If someone else has trademarked or copyrighted the name you picked, your account could be closed. See [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) for more info. If someone else is using a publisher display name for which you hold the trademark or other legal right, [contact Microsoft](https://www.microsoft.com/info/cpyrtInfrg.html).    

6.  Enter the contact info you want to use for your developer account.

    > [!NOTE]
    > We'll use this info to contact you about account-related matters. For example, you'll receive an email confirmation after you complete your registration. After that, we'll send messages when we pay you or if you need to fix something with your account. We may also send informational emails as described earlier, unless you opt out of receiving non-transactional emails.

    If you're registering as a company, you'll also need to enter the name, email address, and phone number of the person who will approve your company's account.

7.  Select **Next** to move on to the **Payment** section.

8.  Enter your payment info for the one-time registration fee. If you have a promo code that covers the cost of registration, you can enter that here. Otherwise, provide your credit card info (or PayPal info in supported markets). Note that prepaid credit cards can't be used for this purchase. When you're finished, select **Next** to move on to the **Review** screen.

9.  Review your account info and confirm that everything is correct. Then, read and accept the terms and conditions of the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement). Check the box to indicate you've read these terms and accept them.

10.  Select **Finish** to confirm your registration. Your payment will be processed and we'll send a confirmation message to your email address.

After you've signed up, your account will go through verification. For individual accounts, we check to make sure another user isn't already using your publisher display name. For company accounts, the process takes a little longer, as we also need to confirm that you’re authorized to set up the account for your company. This verification can take from a few days to a couple of weeks, and it often includes a phone call to your company. You can check your verification status on the **Account settings** page.


## Additional guidelines for company accounts

> [!IMPORTANT]
> To allow multiple users to access your developer account, we recommend using Azure Active Directory (Azure AD) to assign roles to individual users instead of sharing access to the Microsoft account. Each user can then access the developer account by signing in to Partner Center with their individual Azure AD credentials. For more info, see [Manage account users](manage-account-users.md).

If you want to let multiple people access the company account by signing in with the Microsoft account that opened it (instead of as individual users added to the account), see the following guidelines:

-   Create the Microsoft account by using an email address that doesn't already belong to you or another individual, such as MyCompany_PartnerCenter@outlook.com. Don't use an email address at your company's domain, particularly if your company already uses Azure AD. As noted earlier, you can add additional users from your company's Azure AD service later.
-   Limit access to this Microsoft account to the least number of users possible.
-   Set up a corporate email distribution list that includes everyone who needs to access the developer account. Add this email address to the [security info associated with the Microsoft account](https://account.microsoft.com/security). This approach allows all the employees on the list to receive security codes that are sent to this alias. If setting up a distribution list isn't feasible, you can add an individual's email address to your security info. But, the owner of that email address will be the only person who can access and share the security code when prompted (such as when new security info is added to the account or when the account is accessed from a new device).
-   Add a company phone number to the Microsoft account's security info. Try to use a number that doesn't require an extension and that's accessible to key team members.
-   Encourage developers to use [trusted devices](https://support.microsoft.com/help/12369/microsoft-account-add-a-trusted-device) to sign in to your company's developer account. All key team members should have access to these trusted devices. This arrangement reduces the need for security codes to be sent when team members access the account. There's a limit to the number of codes that can be generated per account per week.
-   If you need to allow access to the account from a non-trusted PC, limit that access to a maximum of five developers. Ideally, these developers should access the account from machines that share the same geographical and network location.
-   Frequently review your company’s security info at https://account.microsoft.com/security to make sure it's all current.


## Microsoft account security

We use security info that you provide to raise the security level of your Microsoft account by associating it with multiple forms of identification. This makes unauthorized access to your Microsoft account (and your developer account) substantially more difficult. Also, if you ever forget your password or if someone else tries to access your account, we’ll be able to reach you to confirm ownership and/or re-establish appropriate control of your account.

You must have at least two email addresses or phone numbers on your Microsoft account. We recommend adding as many as possible. Remember that some security info must be confirmed before it will be valid. Also, make sure to review your security info frequently and verify that it's up to date. You can manage your security info by going to https://account.microsoft.com/security and signing in with your Microsoft account. For more info, see [Account security info & verification codes](https://support.microsoft.com/help/12428/microsoft-account-security-info-verification-codes).

When you sign in to Partner Center through your Microsoft account, the system may prompt you to verify your identity by sending a security code, which you must enter to complete the sign-in process. We recommend designating PCs that you use frequently as *trusted devices*. When you sign in from a trusted device, you typically aren't prompted for a code, although you may occasionally be prompted in specific situations or if you haven’t signed in on that device in a long time. For more info, see [Add a trusted device to your Microsoft account](https://support.microsoft.com/help/12369/microsoft-account-add-a-trusted-device).


## Closing your account

Developer accounts don't expire, so there's no need to renew your account in order to keep it open. If you decide to close your account completely, you can do so by contacting support.

When you close your account, it's important to understand what happens to any app that you have published in the Microsoft Store:

-   Your app's current customers can still use the app. However, they can't make in-app purchases.
-   Even though the app is still available to customers who have previously acquired it, your app listing is removed from Microsoft Store. No new customers can acquire your app.
-   Your app's name will be released for potential use by another developer.
-   If you have a balance due from previous app sales, you can request payment for that balance even if the amount due doesn't meet the standard payment threshold.