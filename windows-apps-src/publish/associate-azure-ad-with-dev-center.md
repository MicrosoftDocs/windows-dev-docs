---
author: jnHs
Description: In order to add and manage account users, you must first associate your Dev Center account with your organization's Azure Active Directory.
title: Associate Azure Active Directory with your Dev Center account
ms.author: wdg-dev-content
ms.date: 07/17/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Associate Azure Active Directory with your Dev Center account

In order to [add and manage account users](add-users-groups-and-azure-ad-applications.md), you must first associate your Dev Center account with your organization's Azure Active Directory. 

Windows Dev Center leverages Azure AD for multi-user management and roles assignment. If your organization already uses Office 365 or other business services from Microsoft, you already have Azure AD. Otherwise, you can create a new Azure AD tenant from within Dev Center at no additional charge.

> [!IMPORTANT]
> To associate Azure AD with your Dev Center account, you will need to sign in to your Azure AD tenant with a [global administrator](http://go.microsoft.com/fwlink/?LinkId=746654) account.
> 
> After you associate your Dev Center account with Azure AD, you’ll always need sign in to Dev Center using the Azure AD global administrator account (and not a personal Microsoft account) in order to add and manage account users.

Note that only one Dev Center account can be associated with an Azure AD tenant. Similarly, only one Azure AD tenant can be associated with a Dev Center account. Once you establish the association, you won't be able to remove it without contacting support.


## Associate your Dev Center account with your organization’s existing Azure AD tenant

If your organization already uses Azure AD, follow these steps to link your Dev Center account.

1.  Go to your **Account settings** and click **Manage users**.
2.  Click the **Associate Azure AD with your Dev Center account** button.
3.  Sign in to your Azure AD account. This account must have [global administrator](http://go.microsoft.com/fwlink/?LinkId=746654) permission in order to set up the association.
4.  Review the organization and domain name for your Azure AD tenant. To complete the association, click **Confirm**.
5.  If the association is successful, you will then be ready to add and manage account users in the **Manage users** section in Dev Center.


## Create a brand new Azure AD to associate with your Dev Center account

If you need to set up a new Azure AD to link with your Dev Center account, follow these steps.

1.  Go to your **Account settings** and click **Manage users**.
2.  Click the **Create new Azure AD** button.
3.  Enter the directory information for your new Azure AD:
 - **Domain name**: The unique name that we’ll use for your Azure AD domain, along with “.onmicrosoft.com”. For example, if you entered “example”, your Azure AD domain would be “example.onmicrosoft.com”.
 - **Contact email**: An email address where we can contact you about your account if necessary.
 - **Global administrator user account info**: The first name, last name, username, and password that you want to use for the new global administrator account.
4.  Click **Create** to confirm the new domain and account info.
5.  Sign in with your new Azure AD global administrator username and password to begin [adding and managing additional account users](add-users-groups-and-azure-ad-applications.md).



