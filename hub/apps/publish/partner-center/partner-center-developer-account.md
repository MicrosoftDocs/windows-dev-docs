---
title: Types of developer accounts in Partner Center
description: Here's an overview of the types of developer accounts available in Partner Center for submitting apps to Microsoft Store.
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Developer accounts

To publish your app in the Microsoft Store, you'll need to register as a Windows app developer in [Partner Center](https://developer.microsoft.com/microsoft-store/register/). You can register with an existing Microsoft Account or by creating a new Microsoft account.

## Types of developer accounts

There are 2 types of developer accounts available in Partner Center: **Individual and Company**

| Who should choose individual account | Who should choose company account |
|--------------------------------------|-----------------------------------|
|<ul><li> **Independent developers** whose distribution of apps through the Store is **not in relation to their business, trade, or profession**</li> <li> **Small scale creators** producing content for non-commercial purposes</li> <li> Individuals creating digital content as a **hobbyist, amateur, school, or personal project** </li></ui> | <ul><li> **Independent developers and freelancers** whose distribution of apps through the Store is **in relation to their business, trade, or profession** </li> <li> **Businesses and Organizations** such as corporations, LLCs, partnerships, non-profits, or government organizations </li> <li> **Teams or Groups** within a company or organization </li><ul> |


Here are the additional differences between the two account types.

| Individual account | Company account |
|--------------------|-----------------|
| <ul><li>Costs approximately $19 USD (one-time registration fee; the exact amount varies depending on your country or region)</li><li>Shorter account verification process</li><li>Coming soon: Legal disclaimer added to published products about consumer rights</li></ul> | <ul><li>Costs approximately $99 USD (one-time registration fee; the exact amount varies depending on your country or region)</li><li>Requires greater account verification including business identification documents to be stored 6 months after account closure</li><li>Requires that your company is recognized as such in the country or region in which it is located</li><li>Able to submit apps with restricted functionality (as described in the [Microsoft Store Policies](../store-policies.md#1014-account-type))</li><li>Requires that you submit your email, business address, and phone number which will be visible to users on product pages.</li></ul> |

Company accounts are a little more expensive, mostly because we take some additional steps to ensure that you are authorized to represent your company, verify and store identification information, and set up the account. Per the [Store Policies](../store-policies.md#1014-account-type), apps with certain functionality, such as those that access financial account information or that require authentication to access primary functionality (without using a secure dedicated third-party authentication provider), can only be published by company accounts.

## Additional guidelines for company accounts

> [!IMPORTANT]
> To allow multiple users to access your developer account, we recommend using Azure Active Directory (Azure AD) to assign roles to individual users instead of sharing access to the Microsoft account. Each user can then access the developer account by signing in to Partner Center with their individual Azure AD credentials. For more info, see [Manage account users](manage-account-users.md).

If you want to let multiple people access the company account by signing in with the Microsoft account that opened it (instead of as individual users added to the account), see the following guidelines:

- Email ownership verifies that the primary contact (primary email) address is valid. The primary contact email address must be a work account that is monitored and can send/receive email. Partners should **not** use: (1) a personal email address not associated with the company domain, or (2) a tenant user sign-in not associated to email (for example, jsmith@testcompany.onmicrosoft.com).
- Limit access to this Microsoft account to the least number of users possible.
- Set up a corporate email distribution list that includes everyone who needs to access the developer account. Add this email address to the [security info associated with the Microsoft account](https://account.microsoft.com/security). This approach allows all the employees on the list to receive security codes that are sent to this alias. If setting up a distribution list isn't feasible, you can add an individual's email address to your security info. But, the owner of that email address will be the only person who can access and share the security code when prompted (such as when new security info is added to the account or when the account is accessed from a new device).
- Add a company phone number to the Microsoft account's security info. Try to use a number that doesn't require an extension and that's accessible to key team members.
- Encourage developers to use [trusted devices](https://support.microsoft.com/help/12369/microsoft-account-add-a-trusted-device) to sign in to your company's developer account. All key team members should have access to these trusted devices. This arrangement reduces the need for security codes to be sent when team members access the account. There's a limit to the number of codes that can be generated per account per week.
- If you need to allow access to the account from a non-trusted PC, limit that access to a maximum of five developers. Ideally, these developers should access the account from machines that share the same geographical and network location.
- Frequently review your company’s security info at https://account.microsoft.com/security to make sure it's all current.

> [!NOTE]
> When you sign up for a developer account, we'll use the email address you provide in your contact info to send messages related to your account. At times, these may include information about our programs. If you choose to [opt out](https://account.microsoft.com/account/Account?ru=https%3A%2F%2Faccount.microsoft.com%2Fprofile%2Fcontact-info&destrt=profile-landing) of these informational emails, be aware that we'll still send you transactional messages (for example, to let you know that a payment is on the way). These transactional emails are a necessary part of your account, and unless you close your account, you'll continue to receive them.

## Microsoft account security

We use security info that you provide to raise the security level of your Microsoft account by associating it with multiple forms of identification. This makes unauthorized access to your Microsoft account (and your developer account) substantially more difficult. Also, if you ever forget your password or if someone else tries to access your account, we’ll be able to reach you to confirm ownership and/or re-establish appropriate control of your account.

You must have at least two email addresses or phone numbers on your Microsoft account. We recommend adding as many as possible. Remember that some security info must be confirmed before it will be valid. Also, make sure to review your security info frequently and verify that it's up to date. You can manage your security info by going to https://account.microsoft.com/security and signing in with your Microsoft account. For more info, see [Account security info & verification codes](https://support.microsoft.com/help/12428/microsoft-account-security-info-verification-codes).

When you sign in to Partner Center through your Microsoft account, the system may prompt you to verify your identity by sending a security code, which you must enter to complete the sign-in process. We recommend designating PCs that you use frequently as *trusted devices*. When you sign in from a trusted device, you typically aren't prompted for a code, although you may occasionally be prompted in specific situations or if you haven’t signed in on that device in a long time. For more info, see [Add a trusted device to your Microsoft account](https://support.microsoft.com/help/12369/microsoft-account-add-a-trusted-device).
