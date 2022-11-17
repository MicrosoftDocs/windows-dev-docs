---
description: You can manage users in your Partner Center account.
title: Manage users in your Partner Center account
ms.date: 11/07/2022
ms.topic: article
ms.localizationpriority: medium
---

# Manage users in your Partner Center account

To add users to your Partner Center account, go to the **Users** page in **Account settings** and select **Add users.** You must be signed in with a Manager account for the Azure AD tenant that you want to work in.

## Add existing users

You can select users who already exist in your organization's tenant and give them access to your Partner Center account.

1. Select the gear icon (near the upper right corner of Partner Center) and then select **Developer settings**. In the **Settings** menu, select **Users**.
2. From the **Users** page, select **Add users**.
3. Select one or more users from the list that appears. You can use the search box to search for specific users.

    > [!TIP]
    > If you select more than one user to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple users with different roles/permissions, repeat the steps below for each role or set of custom permissions.
4. When you are finished selecting users, click **Add selected**.
5. In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected user(s).
6. Click **Save**.

## Additional methods for adding users

If you are signed in with a Manager account which also has [global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permissions for the Azure AD tenant you're working in, you will have additional options to add users to your Partner Center account. You'll need to select one of the following:

- **Add existing users**: Choose users who already exist in your organization's directory and give them access to your Partner Center account, using the method described above.
- **Create new users**: Create brand new user accounts to add to both your organization's directory and your Partner Center account
- **Invite outside users**: Send email invites to users who are not currently in your organization's directory. They will be invited to access your Partner Center account, and a new [guest user](/azure/active-directory/active-directory-b2b-what-is-azure-ad-b2b) account will be created for them in your Azure AD tenant.

## Create new users

> [!IMPORTANT]
> You must be signed in with a global administrator account in your Azure AD tenant in order to create new users.

1. From the **Users** page (under **Account settings**), select **Add users**, then choose **Create new users**.
2. Enter the first name, last name, and username for the new user.
3. If you want the new user to have a [global administrator account](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) in your organization's directory, check the box labeled **Make this user a Global administrator in your Azure AD, with full control over all directory resources**. This will give the user full access to all administrative features in your company's Azure AD. They'll be able to add and manage users in your organization's directory (though not in Partner Center, unless you grant the account the appropriate [role/permissions](set-custom-permissions-for-account-users.md)). If you check this box, you'll need to provide a **Password recovery email** for the user.
4. If you checked the box to **Make this user a Global administrator in your Azure AD**, enter an email that the user can use if they need to recover their password.
5. In the **Group membership** section, select any groups to which you want the new user to belong.
6. In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the user.
7. Click **Save**.
8. On the confirmation page, you'll see login info for the new user, including a temporary password. Be sure to note this info and provide it to the new user, as you won't be able to access the temporary password after you leave this page.

## Invite outside users

> [!IMPORTANT]
> You must be signed in with a global administrator account in your Azure AD tenant in order to invite outside users.

1. From the **Users** page (under **Account settings**), select **Add users**, then choose **Invite users by email**.
1. Enter one or more email addresses (up to ten), separated by commas or semicolons.
1. In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the user.
1. Click **Save**.

The users you invited will get an email invitation to join your account, and a new [guest user](/azure/active-directory/active-directory-b2b-what-is-azure-ad-b2b) account will be created for them in your Azure AD tenant. Each user will need to accept their invitation before they can access your account.

If you need to resend an invitation, find the user on your **Users** page and select their email address (or the text that says **Invitation pending**). Then, at the bottom of the page, click **Resend invitation**.

> [!IMPORTANT]
> Outside users that you invite to join your Partner Center account can be assigned the same roles and permissions as other users. However, outside users will not be able to perform certain tasks in Visual Studio, such as associating an app with the Store, or creating packages to upload to the Store. If a user needs to perform those tasks, choose **Create new users** instead of **Invite outside users**. (If you don’t want to add these users to your existing Azure AD tenant, you can [create a new tenant](create-new-azure-ad-tenant.md), then create new user accounts for them in that tenant.)

## Changing a user's directory password

If one of your users needs to change their password they can do so themselves if you provided a **Password recovery email** when creating the user account. You can also update a user's password by following the steps below (if you are signed in with a global administrator account in your Azure AD tenant in order to change a user's password). Note that this will change the user's password in your Azure AD tenant, along with the password they use to access Partner Center.

1. From the **Users** page (under **Account settings**), select the name of the user account that you want to edit.
2. Select the **Reset password** button at the bottom of the page.
3. A confirmation page will appear showing the login info for the user, including a temporary password.

    > [!IMPORTANT]
    >  Be sure to print or copy this info and provide it to the user, as you won't be able to access the temporary password after you leave this page.

## Edit a user

After you've added users to your Partner Center account, you can make changes to their account info.

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL and App ID URI for an Azure AD application) will be reflected in your organization's Azure AD tenant as well as in your Partner Center account.

1. From the **Users** page (under **Account settings**), select the name of the user that you want to edit.
1. Make your desired changes. For a **user**, you can edit the user's first name, last name, or username. You can also select or deselect groups in the **Group membership** section to update their group membership.
    Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
1. For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Azure AD tenant.
1. Click **Save**.

## View history for account users

As an account owner, you can view the detailed browsing history for any additional users you’ve added to the account.

On the **Users** page (under **Account settings**), select the link shown under **Last activity** for the user whose browsing history you’d like to review. You'll be able to view the URLs for all pages that the user visited in the last 30 days.

## Remove a user

To remove a user from your Partner Center account, select the **Remove** link that appears by their name on the **Users** page. After confirming that you want to remove it, that user will no longer be able to access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing a user means that it will no longer have access to your Partner Center account. It does **not** delete the user from your organization's directory.
