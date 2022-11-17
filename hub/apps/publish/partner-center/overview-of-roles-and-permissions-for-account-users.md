---
title: Overview of roles and permissions for account users
description: Learn how to set standard roles or custom permissions when adding users to your Partner Center account.
ms.date: 10/31/2018
ms.topic: article
ms.localizationpriority: medium
---

# Overview of roles and permissions for account users

When you [add users to your Partner Center account](overview-users-groups-azure-ad-applications.md), you'll need to specify what access they have within the account. You can do this by assigning them [standard roles](assign-roles-to-account-users.md) which applies to the entire account, or you can [customize their permissions](overview-of-custom-permissions-for-account-users.md) to provide the appropriate level of access. Some of the custom permissions apply to the entire account, and some can be limited to one or more specific products (or granted to all products, if you prefer).

> [!NOTE]
> The same roles and permissions can be applied regardless of whether you are adding a user, a group, or an Azure AD application.

> [!NOTE]
> This section is not applicable for the Microsoft Edge program. Microsoft Edge program does not support assigning roles to users.

When determining what role or permissions to apply, keep in mind:

- Users (including groups and Azure AD applications) will be able to access the entire Partner Center account with the permissions associated with their assigned role(s), unless you [customize permissions](overview-of-custom-permissions-for-account-users.md) and assign [product-level permissions](assign-product-level-custom-permissions-to-account-users.md) so that they can only work with specific apps and/or add-ons.
- You can allow a user, group, or Azure AD application to have access to more than one role's functionality by selecting multiple roles, or by using custom permissions to grant the access you'd like.
- A user with a certain role (or set of custom permissions) may also be part of a group that has a different role (or set of permissions). In that case, the user will have access to all of the functionality associated with both the group and the individual account.

> [!TIP]
> This topic is specific to the Windows apps developer program in [Partner Center](https://partner.microsoft.com/dashboard). For info about user roles in the Hardware Developer Program, see [Managing User Roles](/windows-hardware/drivers/dashboard/managing-user-roles). For info about user roles in the Windows Desktop Application Program, see [Windows Desktop Application Program](/windows/desktop/appxpkg/windows-desktop-application-program#add-and-manage-account-users).
