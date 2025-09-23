---
description:  Associate your Partner Center account with your existing Microsoft Entra ID tenant.
title: Associate an existing Microsoft Entra ID tenant with your Partner Center account
ms.date: 11/07/2022
ms.topic: how-to
ms.localizationpriority: medium
ms.custom: sfi-ga-nochange
---

# Associate an existing Microsoft Entra ID tenant in Partner Center

In order to [add and manage account users](overview-users-groups-azure-ad-applications.md), you must first associate your Partner Center account with your organization's Microsoft Entra ID.

[Partner Center](https://partner.microsoft.com/dashboard) leverages Microsoft Entra ID for multi-user account access and management. If your organization already uses Microsoft 365 or other business services from Microsoft, you already have Microsoft Entra ID. Otherwise, you can [create a new Microsoft Entra ID tenant](create-new-azure-ad-tenant.md) from within Partner Center at no additional charge.

If your organization already uses Microsoft Entra ID, follow these steps to link your Partner Center account.

1. From [Partner Center](https://partner.microsoft.com/dashboard), select the gear icon (near the upper right corner of the dashboard) and then select **Account settings**. In the **Settings** menu, select **Tenants**.

    :::image type="content" source="../images/partner-center-gear-menu-account-settings.png" alt-text="Screenshot showing Account settings option from the Gear menu of Microsoft Partner Center.":::

    :::image type="content" source="../images/partner-center-account-settings-tenants.png" alt-text="Screenshot showing Tenants option from Account settings menu in Partner Center.":::

2. Select **Associate Microsoft Entra ID with your Partner Center account**.

    :::image type="content" source="../images/partner-center-connect-azure-ad.png" alt-text="Screenshot showing option to Associate Microsoft Entra ID with your Partner Center account.":::

3. On the Microsoft Partner Center sign in page, enter the Microsoft Entra ID credentials for the tenant that you want to associate.

    :::image type="content" source="../images/partner-center-signin-azure-ad-credentials.png" alt-text="Screenshot showing Microsoft Partner Center sign-in dialog where you should sign in using Microsoft Entra ID credentials for your tenant.":::

4. Review the domain name for your Microsoft Entra ID tenant. To complete the association, select **Confirm**.

    :::image type="content" source="../images/partner-center-account-settings-tenant-confirmation.png" alt-text="Screenshot showing information about the tenant to associate.":::

5. If the association is successful, you will then be ready to add and manage account users in the **User management** section in Partner Center.

    :::image type="content" source="../images/partner-center-account-settings-tenant-associated.png" alt-text="Screenshot showing a confirmation after the associationg was completed.":::

> [!IMPORTANT]
> In order to create new users, or make other changes to your Microsoft Entra ID, you’ll need to sign in to that Microsoft Entra ID tenant using an account which has [global administrator permission](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) for that tenant. However, you don’t need global administrator permission in order to associate the tenant, or to add users who already exist in that tenant to your Partner Center account.

To add and manage Partner Center account users in your tenant, sign in to Partner Center as a user in the same tenant who has the **Manager** role.

> [!NOTE]
> Any user who has the **Manager** role for a Partner Center account can associate Microsoft Entra ID tenants with the account.


> [!TIP]
> To refer to common questions, please refer to [Frequently Asked Questions](../faq/manage-your-account.md) section

You can associate multiple Microsoft Entra ID tenants to a single Partner Center account. To associate a new tenant, select **Associate another Microsoft Entra ID tenant**, then follow the steps indicated above. Note that you will be prompted for your credentials in the Microsoft Entra ID tenant that you want to associate.
