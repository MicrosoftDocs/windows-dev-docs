---
description: You can manage groups in your Partner Center account.
title: Manage groups in your Partner Center account
ms.date: 11/07/2022
ms.topic: how-to
ms.localizationpriority: medium
---

# Manage groups in your Partner Center account

To manage groups in your Partner Center account, go to the **User management** page under **Account settings** and select the **Groups** tab. You must be signed in with a Manager account that also has [global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permissions for the Microsoft Entra ID tenant you're working in. 

:::image type="content" source="../images/partner-center-account-settings-groups.png" alt-text="Screenshot showing groups tab in user management page.":::

To add groups, you will have additional options described below.

:::image type="content" source="../images/partner-center-account-settings-add-groups.png" alt-text="Screenshot showing different options to add a group.":::

## Create user group

1. From the **User management** page (under **Account settings**), click on **Add user group**, then choose **Create user group**.
1. Enter the display name and an optional description for the new group, then click **Next**.
1. In the **Users in this group** section, select the user(s) to assign to the new group from the list that appears, then click **Next**.
1. In the **Roles applicable to developer programs** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the group.
1. Click **Create**.

## Add user group

You can select groups that already exist in your organization's tenant and give them access to your Partner Center account.

1. From the **User management** page (under **Account settings**), click on **Add user group**, then choose **Add user group**.
1. Select one or more groups from the list that appears. You can use the search box to find specific groups.

    > [!TIP]
    > If you select more than one group to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple groups with different roles or permissions, repeat these steps for each role or set of custom permissions.
1. When you have finished selecting groups, click **Next**.
1. In the **Roles applicable to developer programs** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected group(s).
1. Click **Add**.

## Edit a group

1. From the **User management** page (under **Account settings**), click on the group that you want to edit.
1. Make your desired changes. For a group, you can edit the group's display name and description. To update group membership, add or remove users in the **Users in this group** section. Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
1. For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Microsoft Entra ID tenant.
1. Click **Update**.

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL for a Microsoft Entra ID application) will be reflected in your organization's Microsoft Entra ID tenant as well as in your Partner Center account.

## Remove a group

1. From the **User management** page (under **Account settings**), select the group that you want to remove.
1. Click on **Delete**.
1. After confirming the removal, that group will no longer have access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing a group means that it will no longer have access to your Partner Center account. It **doesn't** delete the group from your organization's directory.