---
description: You can manage groups in your Partner Center account.
title: Manage groups in your Partner Center account
ms.date: 11/07/2022
ms.topic: article
keywords: windows 10, uwp, azure ad application, aad, user, group, multiple users, multi-user
ms.localizationpriority: medium
---
# Manage groups in your Partner Center account

You can add a group from your organization's directory to your Partner Center account. When you do so, every user who is a member of that group will be able to access it, with the permissions associated with the group's assigned role.

## Add groups from your organization's directory

1.  Select the gear icon (near the upper right corner of Partner Center) and then select **Developer settings**. In the **Settings** menu, select **Users**.
2. From the **Users** page, select **Add groups**.
2.  Select one or more groups from the list that appears. You can use the search box to search for specific groups.
    > [!TIP]
    > If you select more than one group to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple groups with different roles/permissions, repeat the steps below for each role or set of custom permissions.

3.  When you are finished selecting groups, click **Add selected**.
4.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected group(s). All members of the group will be able to access your Partner Center account with the permissions you apply to the group, regardless of the roles/permissions associated with their individual account.
5.  Click **Save**.


## Create a new group account in your organization's directory and add it to your Partner Center account

If you want to grant Partner Center access to a brand new group, you can create a new group in the **Users** section. Note that the new group will be created in your organization's directory, not just in your Partner Center account.

1.  From the **Users** page (under **Developer settings**), click **Add groups**.
2.  On the next page, select **New group**.
3.  Enter the display name for the new group.
4.  Specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the group. All members of the group will be able to access your Partner Center account with the permissions you apply to the group, regardless of the roles/permissions associated with their individual account.
5.  Select the user(s) to assign to the new group from the list that appears. You can use the search box to search for specific users.
6.  When you are finished selecting users, click **Add selected** to add them to the new group.
7.  Click **Save**.

## Edit a group

After you've added users, groups, and/or Azure AD applications to your Partner Center account, you can make changes to their account info. 

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL and App ID URI for an Azure AD application) will be reflected in your organization's Azure AD tenant as well as in your Partner Center account. 

1.  From the **Users** page (under **Account settings**), select the name of the user, group, or Azure AD application account that you want to edit.
2.  Make your desired changes. For a **group**, you can edit the name of the group. (To update group membership, edit the users you want to add or remove from the group and make changes to the **Group membership** section.)
    Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
3.  For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Azure AD tenant.
3.  Click **Save**.

## View history for account users

As an account owner, you can view the detailed browsing history for any additional users you’ve added to the account.

On the **Users** page (under **Account settings**), select the link shown under **Last activity** for the user whose browsing history you’d like to review. You'll be able to view the URLs for all pages that the user visited in the last 30 days.

<span id="remove" />

## Remove groups

To remove a group from your Partner Center account, select the **Remove** link that appears by their name on the **Users** page. After confirming that you want to remove it, that group will no longer be able to access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing a group means that it will no longer have access to your Partner Center account. It does **not** delete the group from your organization's directory.
