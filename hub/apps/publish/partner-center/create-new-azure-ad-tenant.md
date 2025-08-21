---
description:  Create a new Microsoft Entra ID tenant to associate with your Partner Center account.
title: Create a new Microsoft Entra ID tenant to associate with your Partner Center account
ms.date: 11/07/2022
ms.topic: how-to
ms.localizationpriority: medium
ms.custom:
  - sfi-image-nochange
  - sfi-ga-nochange
---

# Create a new Microsoft Entra ID tenant in Partner Center

If you need to set up a new Microsoft Entra ID to link with your Partner Center account, follow these steps.

1. From [Partner Center](https://partner.microsoft.com/dashboard), select the gear icon (near the upper right corner of the dashboard) and then select **Account settings**. In the **Settings** menu, select **Tenants**.

    :::image type="content" source="../images/partner-center-gear-menu-account-settings.png" alt-text="Screenshot showing Account settings option from the Gear menu of Microsoft Partner Center.":::

    :::image type="content" source="../images/partner-center-account-settings-tenants.png" alt-text="Screenshot showing Tenants option in Account settings menu on Partner Center.":::

1. From *Tenants*, select **Create Microsoft Entra ID**.

    :::image type="content" source="../images/partner-center-create-new-azure-ad.png" alt-text="Screenshot showing option to Create new Microsoft Entra ID from the Partner Center Tenants settings.":::

1. Enter the Microsoft Entra ID credentials that will be used to create a new tenant.

    :::image type="content" source="../images/partner-center-create-new-azure-ad-form.png" alt-text="Screenshot showing form to Create a new Microsoft Entra ID directory including field to specify directory information and global admin user account.":::

1. Enter all required information as prompted by the tenant creation wizard.

1. Once the tenant is created, a confirmation dialog will appear.

    :::image type="content" source="../images/partner-center-account-settings-new-tenant.png" alt-text="Screenshot showing the tenant creationg is completed":::

> [!NOTE]
> Any user who has the **Manager** role for a Partner Center account can associate Microsoft Entra ID tenants with the account.

You can associate multiple Microsoft Entra ID tenants to a single Partner Center account. To associate a new tenant, select **Associate another Microsoft Entra ID tenant**, then follow the steps from [associate existing Microsoft Entra ID with partner center](associate-existing-azure-ad-tenant-with-partner-center-account.md). Note that you will be prompted for your credentials in the Microsoft Entra ID tenant that you want to associate.
