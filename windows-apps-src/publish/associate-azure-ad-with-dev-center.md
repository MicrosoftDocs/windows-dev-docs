---
author: jnHs
Description: In order to add and manage account users, you must first associate your Dev Center account with your organization's Azure Active Directory.
title: Associate Azure Active Directory with your Dev Center account
ms.author: wdg-dev-content
ms.date: 02/22/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, azure ad, azure tenant, aad tenant, azure ad tenant, tenant management, tenants
ms.localizationpriority: high
---

# Associate Azure Active Directory with your Dev Center account

In order to [add and manage account users](add-users-groups-and-azure-ad-applications.md), you must first associate your Dev Center account with your organization's Azure Active Directory. 

Windows Dev Center leverages Azure AD for multi-user account access and management. If your organization already uses Office 365 or other business services from Microsoft, you already have Azure AD. Otherwise, you can create a new Azure AD tenant from within Dev Center at no additional charge.

> [!TIP]
> This topic is specific to the Windows apps developer program, but associating a tenant and managing users works similarly for accounts in the Windows Desktop Application Program (see [Windows Desktop Application Program](https://msdn.microsoft.com/library/windows/desktop/mt826504#users) for more info) and in the Windows Hardware Developer Program (where references to the **Manager** role would also apply to Hardware accounts with the **Administrator** role; see [Dashboard Administration](https://docs.microsoft.com/windows-hardware/drivers/dashboard/dashboard-administration) for more info).

A single Azure AD tenant can be associated with multiple Dev Center accounts. You only need to have one Azure AD tenant associated with your Dev Center account in order to add multiple account users, but you also have the option to add multiple Azure AD tenants to a single Dev Center account. Any user with the **Manager** role in the Dev Center account will have the option to add and remove Azure AD tenants.

> [!IMPORTANT]
> After you associate your Dev Center account with your Azure AD tenant, in order to add and manage account users in that tenant, you’ll need to sign in to Dev Center as a user in the same tenant who has the **Manager** role.


## Associate your Dev Center account with your organization’s existing Azure AD tenant

If your organization already uses Azure AD, follow these steps to link your Dev Center account.

1.  Go to your **Account settings** and click **Tenants**.
2.  Select **Associate Azure AD with your Dev Center account**.
3.  Enter your Azure AD credentials for the tenant that you want to associate.
4.  Review the organization and domain name for your Azure AD tenant. To complete the association, select **Confirm**.
5.  If the association is successful, you will then be ready to add and manage account users in the **Users** section in Dev Center.

> [!IMPORTANT]
> In order to create new users, or make other changes to your Azure AD, you’ll need to sign in to that Azure AD tenant using an account which has [global administrator permission](http://go.microsoft.com/fwlink/?LinkId=746654) for that tenant. However, you don’t need global administrator permission in order to associate the tenant, or to add users who already exist in that tenant to your Dev Center account.


## Create a brand new Azure AD to associate with your Dev Center account

If you need to set up a new Azure AD to link with your Dev Center account, follow these steps.

1.  Go to your **Account settings** and click **Tenants**.
2.  Select **Create new Azure AD**.
3.  Enter the directory information for your new Azure AD:
    - **Domain name**: The unique name that we’ll use for your Azure AD domain, along with “.onmicrosoft.com”. For example, if you entered “example”, your Azure AD domain would be “example.onmicrosoft.com”.
    - **Contact email**: An email address where we can contact you about your account if necessary.
    - **Global administrator user account info**: The first name, last name, username, and password that you want to use for the new global administrator account.
4.  Click **Create** to confirm the new domain and account info.
5.  Sign in with your new Azure AD global administrator username and password to begin [adding and managing additional account users](add-users-groups-and-azure-ad-applications.md).


## Manage Azure AD tenant associations

After you have associated an Azure AD tenant with your Dev Center account, you can add new tenants or remove existing tenants from the **Tenants** page.


### Add multiple Azure AD tenants to your Dev Center account

Any user who has the **Manager** role for a Dev Center account can associate Azure AD tenants with the account.

To associate a new tenant, select **Associate another Azure AD tenant**, then follow the steps indicated above. Note that you will be prompted for your credentials in the Azure AD tenant that you want to associate.


### Remove an Azure AD tenant from your Dev Center account

Any user who has the **Manager** role for a Dev Center account can remove Azure AD tenants from the account.

> [!IMPORTANT]
> When you remove a tenant, all users that were added to the Dev Center account from that tenant will no longer be able to sign in to the account. 

To remove a tenant, find its name on the **Tenants** page, then select **Remove**. You’ll be prompted to confirm that you want to remove the tenant. Once you do so, no Dev Center users in that tenant will be able to sign into the Dev Center account, and any permissions you have configured for those users will be removed.

> [!TIP]
> You can’t remove a tenant if you are currently signed into Dev Center using an account in the same tenant. To remove a tenant, you must sign in to Dev Center as an **Manager** for another tenant that is associated with the account. If there is only one tenant associated with the account, that tenant can only be removed after signing in with the Microsoft account that opened the account.


