---
description:  Associate your Partner Center account with your existing Azure Active tenant.
title: Associate an existing Azure AD tenant with your Partner Center account
ms.date: 11/07/2022
ms.topic: article
ms.localizationpriority: medium
---

# Associate an existing Azure AD tenant with your Partner Center account

In order to [add and manage account users](overview-users-groups-azure-ad-applications.md), you must first associate your Partner Center account with your organization's Azure Active Directory (Azure AD).

[Partner Center](https://partner.microsoft.com/dashboard) leverages Azure AD for multi-user account access and management. If your organization already uses Microsoft 365 or other business services from Microsoft, you already have Azure AD. Otherwise, you can [create a new Azure AD tenant](create-new-azure-ad-tenant.md) from within Partner Center at no additional charge.

If your organization already uses Azure AD, follow these steps to link your Partner Center account.

1. From [Partner Center](https://partner.microsoft.com/dashboard), select the gear icon (near the upper right corner of the dashboard) and then select **Account settings**. In the **Settings** menu, select **Tenants**.

    :::image type="content" source="../images/partner-center-gear-menu-account-settings.png" alt-text="Screenshot showing Account settings option from the Gear menu of Microsoft Partner Center.":::

    :::image type="content" source="../images/partner-center-account-settings-tenants.png" alt-text="Screenshot showing Tenants option from Account settings menu in Partner Center.":::

2. Select **Associate Azure AD with your Partner Center account**.

    :::image type="content" source="../images/partner-center-connect-azure-ad.png" alt-text="Screenshot showing option to Associate Azure AD with your Partner Center account.":::

3. On the Microsoft Partner Center sign in page, enter the Azure AD credentials for the tenant that you want to associate.

    :::image type="content" source="../images/partner-center-signin-azure-ad-credentials.png" alt-text="Screenshot showing Microsoft Partner Center sign-in dialog where you should sign in using Azure AD credentials for your tenant.":::

4. Review the organization and domain name for your Azure AD tenant. To complete the association, select **Confirm**.

5. If the association is successful, you will then be ready to add and manage account users in the **Users** section in Partner Center.

> [!IMPORTANT]
> In order to create new users, or make other changes to your Azure AD, you’ll need to sign in to that Azure AD tenant using an account which has [global administrator permission](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) for that tenant. However, you don’t need global administrator permission in order to associate the tenant, or to add users who already exist in that tenant to your Partner Center account.

To add and manage Partner Center account users in your tenant, sign in to Partner Center as a user in the same tenant who has the **Manager** role.

> [!NOTE]
> Any user who has the **Manager** role for a Partner Center account can associate Azure AD tenants with the account.

You can associate multiple Azure AD tenants to a single Partner Center account. To associate a new tenant, select **Associate another Azure AD tenant**, then follow the steps indicated above. Note that you will be prompted for your credentials in the Azure AD tenant that you want to associate.
