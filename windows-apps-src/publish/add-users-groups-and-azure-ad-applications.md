---
Description: You can add users, groups, and Azure AD applications to your Partner Center account.
title: Add users, groups, and Azure AD applications to your Partner Center account
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, azure ad application, aad, user, group, multiple users, multi-user
ms.localizationpriority: medium
---
# Add users, groups, and Azure AD applications to your Partner Center account

The **Users** section of [Partner Center](https://partner.microsoft.com/dashboard) (under **Account settings**) lets you use Azure Active Directory to add users to your Partner Center account. Each user is assigned a role (or set of custom permissions) that defines their access to the account. You can also add [groups of users](#groups) and [Azure AD applications](#azure-ad-applications) to grant them access to your Partner Center account.

After users have been added to the account, you can [edit account details](#edit), change [roles and permissions](set-custom-permissions-for-account-users.md), or [remove users](#remove).

> [!IMPORTANT]
> In order to add users to your account, you must first [associate your Partner Center account with your organization's Azure Active Directory tenant](associate-azure-ad-with-partner-center.md). 

When adding users, you will need to specify their access to your Partner Center account by assigning them a [role or set of custom permissions](set-custom-permissions-for-account-users.md). 

Keep in mind that all Partner Center users (including groups and Azure AD applications) must have an active account in [an Azure AD tenant that is associated with your Partner Center account](associate-azure-ad-with-partner-center.md). User management is done in one tenant at a time; you must sign in with a Manager account for the tenant in which you want to add or edit users. Creating a new user in Partner Center will also create an account for that user in the Azure AD tenant to which you are signed in, and making changes to a user's name in Partner Center will make the same changes in your organization's Azure AD tenant.

> [!NOTE]
> If your organization uses [directory integration](/previous-versions/azure/azure-services/jj573653(v=azure.100)) to sync the on-premises directory service with your Azure AD, you won't be able to create new users, groups, or Azure AD applications in Partner Center. You (or another admin in your on-premises directory) will need to create them directly in the on-premises directory before you'll be able to see and add them in Partner Center.


<span id="users" />

## Add users to your Partner Center account

To add users to your Partner Center account, go to the **Users** page in **Account settings** and select **Add users.** You must be signed in with a Manager account for the Azure AD tenant that you want to work in. 

### Add existing users 

You can select users who already exist in your organization's tenant and give them access to your Partner Center account. 

<span id="from-directory" />

1.  Select the gear icon (near the upper right corner of Partner Center) and then select **Developer settings**. In the **Settings** menu, select **Users**.
2.  From the **Users** page, select **Add users**. 
3.  Select one or more users from the list that appears. You can use the search box to search for specific users.
    > [!TIP]
    > If you select more than one user to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple users with different roles/permissions, repeat the steps below for each role or set of custom permissions.
4.  When you are finished selecting users, click **Add selected**.
5.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected user(s).
6.  Click **Save**.

### Additional methods for adding users

If you are signed in with a Manager account which also has [global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permissions for the Azure AD tenant you're working in, you will have additional options to add users to your Partner Center account. You'll need to select one of the following:

-   **Add existing users**: Choose users who already exist in your organization's directory and give them access to your Partner Center account, using the method described above.
-   **Create new users**: Create brand new user accounts to add to both your organization's directory and your Partner Center account
-   **Invite outside users**: Send email invites to users who are not currently in your organization's directory. They will be invited to access your Partner Center account, and a new [guest user](/azure/active-directory/active-directory-b2b-what-is-azure-ad-b2b) account will be created for them in your Azure AD tenant.

<span id="new-user" />

### Create new users

> [!IMPORTANT]
> You must be signed in with a global administrator account in your Azure AD tenant in order to create new users.

1.  From the **Users** page (under **Account settings**), select **Add users**, then choose **Create new users**.
2.  Enter the first name, last name, and username for the new user.
3.  If you want the new user to have a [global administrator account](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) in your organization's directory, check the box labeled **Make this user a Global administrator in your Azure AD, with full control over all directory resources**. This will give the user full access to all administrative features in your company's Azure AD. They'll be able to add and manage users in your organization's directory (though not in Partner Center, unless you grant the account the appropriate [role/permissions](set-custom-permissions-for-account-users.md)). If you check this box, you'll need to provide a **Password recovery email** for the user.
4.  If you checked the box to **Make this user a Global administrator in your Azure AD**, enter an email that the user can use if they need to recover their password.
5.  In the **Group membership** section, select any groups to which you want the new user to belong.
6.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the user.
7.  Click **Save**.
8.  On the confirmation page, you'll see login info for the new user, including a temporary password. Be sure to note this info and provide it to the new user, as you won't be able to access the temporary password after you leave this page.


<span id="email" />

### Invite outside users

> [!IMPORTANT]
> You must be signed in with a global administrator account in your Azure AD tenant in order to invite outside users.

1.  From the **Users** page (under **Account settings**), select **Add users**, then choose **Invite users by email**.
1.  Enter one or more email addresses (up to ten), separated by commas or semicolons.
2.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the user.
3.  Click **Save**.

The users you invited will get an email invitation to join your account, and a new [guest user](/azure/active-directory/active-directory-b2b-what-is-azure-ad-b2b) account will be created for them in your Azure AD tenant. Each user will need to accept their invitation before they can access your account.

If you need to resend an invitation, find the user on your **Users** page and select their email address (or the text that says **Invitation pending**). Then, at the bottom of the page, click **Resend invitation**.

> [!IMPORTANT]
> Outside users that you invite to join your Partner Center account can be assigned the same roles and permissions as other users. However, outside users will not be able to perform certain tasks in Visual Studio, such as associating an app with the Store, or creating packages to upload to the Store. If a user needs to perform those tasks, choose **Create new users** instead of **Invite outside users**. (If you don’t want to add these users to your existing Azure AD tenant, you can [create a new tenant](../publish/associate-azure-ad-with-partner-center.md#create-a-brand-new-azure-ad-to-associate-with-your-partner-center-account), then create new user accounts for them in that tenant.) 


### Changing a user's directory password

If one of your users needs to change their password they can do so themselves if you provided a **Password recovery email** when creating the user account. You can also update a user's password by following the steps below (if you are signed in with a global administrator account in your Azure AD tenant in order to change a user's password). Note that this will change the user's password in your Azure AD tenant, along with the password they use to access Partner Center. 

1.  From the **Users** page (under **Account settings**), select the name of the user account that you want to edit.
2.  Select the **Reset password** button at the bottom of the page.
3.  A confirmation page will appear showing the login info for the user, including a temporary password.

    > [!IMPORTANT]
    >  Be sure to print or copy this info and provide it to the user, as you won't be able to access the temporary password after you leave this page.

<span id="groups" />

## Add groups to your Partner Center account

You can add a group from your organization's directory to your Partner Center account. When you do so, every user who is a member of that group will be able to access it, with the permissions associated with the group's assigned role.

### Add groups from your organization's directory

1.  Select the gear icon (near the upper right corner of Partner Center) and then select **Developer settings**. In the **Settings** menu, select **Users**.
2. From the **Users** page, select **Add groups**.
2.  Select one or more groups from the list that appears. You can use the search box to search for specific groups.
    > [!TIP]
    > If you select more than one group to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple groups with different roles/permissions, repeat the steps below for each role or set of custom permissions.

3.  When you are finished selecting groups, click **Add selected**.
4.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected group(s). All members of the group will be able to access your Partner Center account with the permissions you apply to the group, regardless of the roles/permissions associated with their individual account.
5.  Click **Save**.


### Create a new group account in your organization's directory and add it to your Partner Center account

If you want to grant Partner Center access to a brand new group, you can create a new group in the **Users** section. Note that the new group will be created in your organization's directory, not just in your Partner Center account.

1.  From the **Users** page (under **Developer settings**), click **Add groups**.
2.  On the next page, select **New group**.
3.  Enter the display name for the new group.
4.  Specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the group. All members of the group will be able to access your Partner Center account with the permissions you apply to the group, regardless of the roles/permissions associated with their individual account.
5.  Select the user(s) to assign to the new group from the list that appears. You can use the search box to search for specific users.
6.  When you are finished selecting users, click **Add selected** to add them to the new group.
7.  Click **Save**.


<span id="azure-ad-applications" />

## Add Azure AD applications to your Partner Center account

You can allow applications or services that are part of your organization's Azure AD to access your Partner Center account. These Azure AD application user accounts can be used to call the REST APIs provided by the [Microsoft Store services](../monetize/using-windows-store-services.md).


### Add Azure AD applications from your organization's directory

1.  1.  Select the gear icon (near the upper right corner of Partner Center) and then select **Developer settings**. In the **Settings** menu, select **Users**.
2. From the **Users** page, select **Add Azure AD applications**.
3.  Select one or more Azure AD applications from the list that appears. You can use the search box to search for specific Azure AD applications.
    > [!TIP]
    > If you select more than one Azure AD application to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple Azure AD applications with different roles/permissions, repeat the steps below for each role or set of custom permissions.

4.  When you are finished selecting Azure AD applications, click **Add selected**.
5.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected Azure AD application(s).
6.  Click **Save**.


### Create a new Azure AD application account in your organization's directory and add it to your Partner Center account

If you want to grant Partner Center access to a brand new Azure AD application account, you can create one in the **Users** section. Note that this will create a new account in your organization's directory, not just in your Partner Center account.

> [!TIP]
> If you are primarily using this Azure AD application for Partner Center authentication, and don't need users to access it directly, you can enter any valid address for the **Reply URL** and **App ID URI**, as long as those values are not used by any other Azure AD application in your directory.

1.  From the **Users** page (under **Account settings**), select **Add Azure AD applications**.
2.  On the next page, select **New Azure AD application**.
3.  Enter the **Reply URL** for the new Azure AD application. This is the URL where users can sign in and use your Azure AD application (sometimes also known as the App URL or Sign-On URL). The **Reply URL** can't be longer than 256 characters and must be unique within your directory.
4.  Enter the **App ID URI** for the new Azure AD application. This is a logical identifier for the Azure AD application that is presented when it sends a single sign-on request to Azure AD. Note that the **App ID URI** must be unique for each Azure AD application in your directory, and it can't be longer than 256 characters. For more info about the **App ID URI**, see [Integrating applications with Azure Active Directory](/azure/active-directory/develop/active-directory-integrating-applications#changing-the-application-registration-to-support-multi-tenant).
5.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the Azure AD application.
6.  Click **Save**.

After you add or create an Azure AD application, you can return to the **Users** section and select the application name to review settings for the application, including the Tenant ID, Client ID, Reply URL, and App ID URI.

> [!NOTE]
> If you intend to use the REST APIs provided by the [Microsoft Store services](../monetize/using-windows-store-services.md), you will need the Tenant ID and Client ID values shown on this page to obtain an Azure AD access token that you can use to authenticate the calls to services.   

<span id="manage-keys" />

### Manage keys for an Azure AD application

If your Azure AD application reads and writes data in Microsoft Azure AD, it will need a key. You can create keys for an Azure AD application by editing its info in Partner Center. You can also remove keys that are no longer needed.

1.  From the **Users** page (under **Account settings**), select the name of the Azure AD application.
    > [!TIP]
    > When you click the name of the Azure AD application, you'll see all of the active keys for the Azure AD application, including the date on which the key was created and when it will expire. To remove a key that is no longer needed, click **Remove**.

2.  To add a new key, select **Add new key**.
3.  You will see a screen showing the **Client ID** and **Key** values.
    > [!IMPORTANT]
    > Be sure to print or copy this info, as you won't be able to access it again after you leave this page.

4.  If you want to create more keys, select **Add another key**.

<span id="edit" />

## Edit a user, group, or Azure AD application

After you've added users, groups, and/or Azure AD applications to your Partner Center account, you can make changes to their account info. 

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL and App ID URI for an Azure AD application) will be reflected in your organization's Azure AD tenant as well as in your Partner Center account. 

1.  From the **Users** page (under **Account settings**), select the name of the user, group, or Azure AD application account that you want to edit.
2.  Make your desired changes. The items you can edit are as follows:
    -   For a **user**, you can edit the user's first name, last name, or username. You can also select or deselect groups in the **Group membership** section to update their group membership.
    -   For a **group**, you can edit the name of the group. (To update group membership, edit the users you want to add or remove from the group and make changes to the **Group membership** section.)
    -   For an **Azure AD application**, you can enter new values for the **Reply URL** or **App ID URI**.
    Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
3.  For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Azure AD tenant.
3.  Click **Save**.


## View history for account users

As an account owner, you can view the detailed browsing history for any additional users you’ve added to the account.

On the **Users** page (under **Account settings**), select the link shown under **Last activity** for the user whose browsing history you’d like to review. You'll be able to view the URLs for all pages that the user visited in the last 30 days.

<span id="remove" />

## Remove users, groups, and Azure AD applications

To remove a user, group, or Azure AD application from your Partner Center account, select the **Remove** link that appears by their name on the **Users** page. After confirming that you want to remove it, that user, group, or Azure AD application will no longer be able to access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing a user, group, or Azure AD application means that it will no longer have access to your Partner Center account. It does **not** delete the user, group, or Azure AD application from your organization's directory.

 