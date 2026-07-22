---
title: Overview of roles and custom permissions for account users
description: Learn how to set standard roles or custom permissions when adding users to your Partner Center account.
ms.date: 10/30/2022
ms.topic: concept-article
ms.localizationpriority: medium
---

# Overview of roles and custom permissions for account users

## Roles and permissions

When you [add users to your Partner Center account](overview-users-groups-azure-ad-applications.md), you'll need to specify what access they have within the account. You can do this by assigning them [standard roles](assign-roles-to-account-users.md) which applies to the entire account, or you can [customize their permissions](#custom-permissions) to provide the appropriate level of access. Some of the custom permissions apply to the entire account, and some can be limited to one or more specific products (or granted to all products, if you prefer).

> [!NOTE]
> The same roles and permissions can be applied regardless of whether you are adding a user, a group, or a Microsoft Entra ID application.

> [!NOTE]
> This section is not applicable for the Microsoft Edge program. Microsoft Edge program does not support assigning roles to users.

When determining what role or permissions to apply, keep in mind:

- Users (including groups and Microsoft Entra ID applications) will be able to access the entire Partner Center account with the permissions associated with their assigned role(s), unless you [customize permissions](overview-of-roles-and-permissions-for-account-users.md#custom-permissions) and assign [product-level permissions](assign-product-level-custom-permissions-to-account-users.md) so that they can only work with specific apps and/or add-ons.
- You can allow a user, group, or Microsoft Entra ID application to have access to more than one role's functionality by selecting multiple roles, or by using custom permissions to grant the access you'd like.
- A user with a certain role (or set of custom permissions) may also be part of a group that has a different role (or set of permissions). In that case, the user will have access to all of the functionality associated with both the group and the individual account.

> [!TIP]
> This topic is specific to the Windows apps developer program in [Partner Center](https://partner.microsoft.com/dashboard). For info about user roles in the Hardware Developer Program, see [Managing User Roles](/windows-hardware/drivers/dashboard/hardware-dashboard-users-manage). For info about user roles in the Windows Desktop Application Program, see [Windows Desktop Application Program](/windows/desktop/appxpkg/windows-desktop-application-program#add-and-manage-account-users).

## Custom permissions

To assign custom permissions rather than standard roles, click **Customize permissions** in the **Roles** section when adding or editing the user account.

To enable a permission for the user, toggle the box to the appropriate setting.
> [!NOTE]
> Any user with a standard role of Owner, Manager or Developer will have access to all products irrespective of custom permissions set.

![Guide to access settings](../images/permission-key.png)

- **No access**: The user will not have the indicated permission.
- **Read only**: The user will have access to view features related to the indicated area, but will not be able to make changes.
- **Read/write**: The user will have access to make changes associated with the area, as well as viewing it.
- **Mixed**: You can’t select this option directly, but the **Mixed** indicator will show if you have allowed a combination of access for that permission. For example, if you grant **Read only** access to **Pricing and availability** for **All products**, but then grant **Read/write** access to **Pricing and availability** for one specific product, the **Pricing and availability** indicator for **All products** will show as Mixed. The same applies if some products have **No access** for a permission, but others have **Read/write** and/or **Read only** access.

For some permissions, such as those related to viewing analytic data, only **Read only** access can be granted. Note that in the current implementation, some permissions have no distinction between **Read only** and **Read/write** access. Review the details for each permission to understand the specific capabilities granted by **Read only** and/or **Read/write** access.

The specific details about each permission are described in the next couple of pages.