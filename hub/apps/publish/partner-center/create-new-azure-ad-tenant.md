---
description:  Create a new Azure AD tenant to associate with your Partner Center account.
title: Create a new Azure AD tenant to associate with your Partner Center account
ms.date: 11/07/2022
ms.topic: article
ms.localizationpriority: medium
---

# Create a new Azure AD tenant to associate with your Partner Center account

If you need to set up a new Azure AD to link with your Partner Center account, follow these steps.

1. From [Partner Center](https://partner.microsoft.com/dashboard), select the gear icon (near the upper right corner of the dashboard) and then select **Account settings**. In the **Settings** menu, select **Tenants**.

    :::image type="content" source="../images/partner-center-gear-menu-account-settings.png" alt-text="Screenshot showing Account settings option from the Gear menu of Microsoft Partner Center.":::

    :::image type="content" source="../images/partner-center-account-settings-tenants.png" alt-text="Screenshot showing Tenants option in Account settings menu on Partner Center.":::

1. From *Tenants*, select **Create new Azure AD**.

    :::image type="content" source="../images/partner-center-create-new-azure-ad.png" alt-text="Screenshot showing option to Create new Azure AD from the Partner Center Tenants settings.":::

1. Enter the directory information for your new Azure AD:
    - **Domain name**: The unique name that we’ll use for your Azure AD domain, along with “.onmicrosoft.com”. For example, if you entered “example”, your Azure AD domain would be “example.onmicrosoft.com”.
    - **Contact email**: An email address where we can contact you about your account if necessary.
    - **Global administrator user account info**: The first name, last name, username, and password that you want to use for the new global administrator account.

    :::image type="content" source="../images/partner-center-create-new-azure-ad-form.png" alt-text="Screenshot showing form to Create a new Azure Active Directory including field to specify directory information and global admin user account.":::

    > [!TIP]
    > Before you click on *Create* to confirm the new domain, please make a note of the new *User name* and *Password* created offline as we do not currently support email notifications for this.

1. Click **Create** to confirm the new domain and account info. By default, the *User name* / *Password* used to create your Azure tenant becomes the *Global Administrator*.

1. Sign in with your new Azure AD global administrator username and password to begin [adding and managing additional account users](overview-users-groups-azure-ad-applications.md).

    :::image type="content" source="../images/partner-center-signin-azure-ad-credentials.png" alt-text="Sign in to Microsoft Partner Center with the Azure AD credentials for your tenant":::

> [!NOTE]
> Any user who has the **Manager** role for a Partner Center account can associate Azure AD tenants with the account.

You can associate multiple Azure AD tenants to a single Partner Center account. To associate a new tenant, select **Associate another Azure AD tenant**, then follow the steps from [associate existing azure ad with partner center](associate-existing-azure-ad-tenant-with-partner-center-account.md). Note that you will be prompted for your credentials in the Azure AD tenant that you want to associate.
