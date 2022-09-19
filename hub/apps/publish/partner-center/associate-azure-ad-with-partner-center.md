---
description:  Associate your Partner Center account with your organization Azure Active Directory to add and manage account users.
title: Associate Azure Active Directory with your Partner Center account
ms.date: 07/05/2022
ms.topic: article
keywords: windows 10, uwp, azure ad, azure tenant, aad tenant, azure ad tenant, tenant management, tenants
ms.localizationpriority: medium
---
# Associate Azure Active Directory with your Partner Center account

In order to [add and manage account users](add-users-groups-and-azure-ad-applications.md), you must first associate your Partner Center account with your organization's Azure Active Directory (Azure AD).

[Partner Center](https://partner.microsoft.com/dashboard) leverages Azure AD for multi-user account access and management. If your organization already uses Microsoft 365 or other business services from Microsoft, you already have Azure AD. Otherwise, you can create a new Azure AD tenant from within Partner Center at no additional charge.

> [!TIP]
> This topic is specific to the Windows apps developer program in [Partner Center](https://partner.microsoft.com/dashboard), but associating a tenant and managing users works similarly for accounts in the Windows Desktop Application Program (see [Windows Desktop Application Program](/windows/desktop/appxpkg/windows-desktop-application-program#add-and-manage-account-users) for more info) and in the Windows Hardware Developer Program (where references to the **Manager** role would also apply to Hardware accounts with the **Administrator** role; see [Dashboard Administration](/azure/azure-portal/azure-portal-dashboards) for more info).

A single Azure AD tenant can be associated with multiple Partner Center accounts. You only need to have one Azure AD tenant associated with your Partner Center account in order to add multiple account users, but you also have the option to add multiple Azure AD tenants to a single Partner Center account. Any user with the **Manager** role in the Partner Center account will have the option to add and remove Azure AD tenants from the account.

You can access the **Tenants** page in Partner Center by selecting the gear icon at the corner of the dashboard.

:::image type="content" source="images/partner-center-gear-menu-account-settings.png" alt-text="Screenshot showing *Account settings* option from the the *Gear* menu of Microsoft Partner Center.":::

From *Account settings*, select **Tenants** (under *Organizational profile*).

:::image type="content" source="images/partner-center-account-settings-tenants.png" alt-text="Screenshot showing *Tenants* option from *Account settings* menu in Partner Center.":::

## Associate an existing Azure AD tenant with your Partner Center account

If your organization already uses Azure AD, follow these steps to link your Partner Center account.

1.  From [Partner Center](https://partner.microsoft.com/dashboard), select the gear icon (near the upper right corner of the dashboard) and then select **Account settings**. In the **Settings** menu, select **Tenants**.

1.  Select **Associate Azure AD with your Partner Center account**.

    :::image type="content" source="images/partner-center-connect-azure-ad.png" alt-text="Screenshot showing option to *Associate Azure AD with your Partner Center account*.":::

1.  A the Microsoft Partner Center sign in page, enter the Azure AD credentials for the tenant that you want to associate.

    :::image type="content" source="images/partner-center-signin-azure-ad-credentials.png" alt-text="Screenshot showing Microsoft Partner Center sign-in dialog where you should sign in using Azure AD credentials for your tenant.":::

1.  Review the organization and domain name for your Azure AD tenant. To complete the association, select **Confirm**.

1.  If the association is successful, you will then be ready to add and manage account users in the **Users** section in Partner Center.

> [!IMPORTANT]
> In order to create new users, or make other changes to your Azure AD, you’ll need to sign in to that Azure AD tenant using an account which has [global administrator permission](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) for that tenant. However, you don’t need global administrator permission in order to associate the tenant, or to add users who already exist in that tenant to your Partner Center account.

To add and manage Partner Center account users in your tenant, sign in to Partner Center as a user in the same tenant who has the **Manager** role.

## Create a new Azure AD tenant to associate with your Partner Center account

If you need to set up a new Azure AD to link with your Partner Center account, follow these steps.

1.  From [Partner Center](https://partner.microsoft.com/dashboard), select the gear icon (near the upper right corner of the dashboard) and then select **Account settings**. In the **Settings** menu, select **Tenants**.

    :::image type="content" source="images/partner-center-account-settings-tenants.png" alt-text="Screenshot showing *Tenants* option in *Account settings* menu on Partner Center.":::

1.  From *Tenants*, select **Create new Azure AD**.

    :::image type="content" source="images/partner-center-create-new-azure-ad.png" alt-text="Screenshot showing option to *Create new Azure AD* from the Partner Center *Tenants* settings.":::

1.  Enter the directory information for your new Azure AD:
    - **Domain name**: The unique name that we’ll use for your Azure AD domain, along with “.onmicrosoft.com”. For example, if you entered “example”, your Azure AD domain would be “example.onmicrosoft.com”.
    - **Contact email**: An email address where we can contact you about your account if necessary.
    - **Global administrator user account info**: The first name, last name, username, and password that you want to use for the new global administrator account.
    
    :::image type="content" source="images/partner-center-create-new-azure-ad-form.png" alt-text="Screenshot showing form to *Create a new Azure Active Directory* including field to specify directory information and global admin user account.":::

    > [!TIP]
    > Before you click on *Create* to confirm the new domain, please make a note of the new *User name* and *Password* created offline as we do not currently support email notifications for this.

1.  Click **Create** to confirm the new domain and account info. By default, the *User name* / *Password* used to create your Azure tenant becomes the *Global Administrator*.

1.  Sign in with your new Azure AD global administrator username and password to begin [adding and managing additional account users](add-users-groups-and-azure-ad-applications.md).

    :::image type="content" source="images/partner-center-signin-azure-ad-credentials.png" alt-text="Sign in to Microsoft Partner Center with the Azure AD credentials for your tenant":::

## Manage Azure AD tenant associations

After you have associated an Azure AD tenant with your Partner Center account, you can add new tenants or remove existing tenants from the **Tenants** page.

### Add multiple Azure AD tenants to your Partner Center account

Any user who has the **Manager** role for a Partner Center account can associate Azure AD tenants with the account.

You can associate multiple Azure AD tenants to a single Partner Center account. To associate a new tenant, select **Associate another Azure AD tenant**, then follow the steps indicated above. Note that you will be prompted for your credentials in the Azure AD tenant that you want to associate.

:::image type="content" source="images/partner-center-signin-azure-ad-credentials.png" alt-text="Screenshot showing Microsoft Partner Center sign-in dialog where you should sign in using Azure AD credentials for your tenant.":::

### Remove an Azure AD tenant from your Partner Center account

Any user who has the **Manager** role for a Partner Center account can remove Azure AD tenants from the account.

> [!IMPORTANT]
> When you remove a tenant, all users that were added to the Partner Center account from that tenant will no longer be able to sign in to the account.

To remove a tenant, find its name on the **Tenants** page (in **Account settings**), then select **Remove**. You’ll be prompted to confirm that you want to remove the tenant. Once you do so, no users in that tenant will be able to sign into the Partner Center account, and any permissions you have configured for those users will be removed.

:::image type="content" source="images/partner-center-account-settings-tenants-remove-azure-ad.png" alt-text="Screenshot showing *Remove* option in Partner Center tenant settings.":::

> [!TIP]
> You can’t remove a tenant if you are currently signed into Partner Center using an account in the same tenant. To remove a tenant, you must sign in to Partner Center as an **Manager** for another tenant that is associated with the account. If there is only one tenant associated with the account, that tenant can only be removed after signing in with the Microsoft account that opened the account.
