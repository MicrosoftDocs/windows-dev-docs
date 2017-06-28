---
author: jnHs
Description: Add users to your Dev Center account and assign them roles with specific permissions.
title: Manage account users
ms.assetid: 9245F0D0-7D8F-4741-AFB4-FBA5601D0A9B
ms.author: wdg-dev-content
ms.date: 06/29/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Manage account users

You can use Azure Active Directory to add users to your Dev Center account. Each user is assigned a role that gives them a specific set of permissions to the account. You can also assign a role to a group of users, or to an Azure AD application.

> [!IMPORTANT]
> In order to add and manage account users, you must first associate your Dev Center account with your organization's Azure Active Directory. This requires you to sign in to Azure AD with a [Global administrator](http://go.microsoft.com/fwlink/?LinkId=746654) account. Once you establish this association, you won't be able to remove it without contacting support.


## Associate your Dev Center account with your organization's Azure Active Directory

Windows Dev Center leverages Azure Active Directory for multi-user management and roles assignment. If your organization already uses Office 365 or other business services from Microsoft, you already have Azure AD. Otherwise, you can create a new Azure AD from within Dev Center at no additional charge.

Note that only one Dev Center account can be associated with an Azure AD. Similarly, only one Azure AD can be associated with a Dev Center account.

> [!TIP]
> If the users you want to add are not part of your organization’s Azure AD, and you don't want to create new Azure AD accounts for them, you can [invite users by email](#email).


### Associate your Dev Center account with your organization’s existing Azure AD

If your organization already uses Azure AD, follow these steps to link your Dev Center account.

1.  Go to your **Account settings** and click **Manage users**.
2.  Click the **Associate Azure AD with your Dev Center account** button.
3.  Sign in to your Azure AD account. This account must have [Global administrator](http://go.microsoft.com/fwlink/?LinkId=746654) permission in order to set up the association.
4.  Review the organization and domain name for your Azure AD account. To complete the association, click **Confirm**.
5.  If the association is successful, you will then be ready to add and manage account users on the **Manage users** page of your account as described in the sections below.


### Create a brand new Azure AD to associate with your Dev Center account

If you need to set up a new Azure AD to link with your Dev Center account, follow these steps.

1.	Go to your **Account settings** and click **Manage users**.
2.	Click the **Create new Azure AD** button.
3.	Enter the directory information for your new Azure AD:
 - **Domain name**: The unique name that we’ll use for your Azure AD domain, along with “.onmicrosoft.com”. For example, if you entered “example”, your Azure AD domain would be “example.onmicrosoft.com”.
 - **Contact email**: An email address where we can contact you about your account if necessary.
 - **Global administrator user account info**: The first name, last name, username, and password that you want to use for the new administrator account.
4.	Click **Create** to confirm the new domain and account info.
5.	Sign in with your new Azure AD global administrator username and password to begin adding and managing additional account users on the **Manage users** page of your account as described in the sections below.

> [!IMPORTANT]
> After you associate your Dev Center account with Azure AD, you’ll always need sign in to Dev Center using the Azure AD global administrator account (and not a personal Microsoft account) in order to add and manage account users.


## Add and manage account users, groups, and Azure AD applications

Once you have established the association, you can add users, groups, and Azure AD applications to your account. You can also change roles, edit account details, or remove users.

> [!NOTE]
> If your organization uses [directory integration](http://go.microsoft.com/fwlink/p/?LinkID=724033) to sync the on-premises directory service with your Azure AD, you won't be able to create new users, groups, or Azure AD applications in Dev Center. You (or another admin in your on-premises directory) will need to create them directly in the on-premises directory before you'll be able to see and add them in Dev Center.

When managing users, keep the following in mind:

-   All Dev Center users must have an active account in your organization's Azure AD (unless you [invite them by email](#email).
-   Creating a **new** user or group in Dev Center will also add that user or group to your organization's Azure AD.
-   Making changes to a user or group's name in Dev Center will make the same changes in your organization's Azure AD.
-   Users (including groups and Azure AD applications) will be able to access the entire Dev Center account with the permissions associated with their assigned role, unless you [customize permissions](set-custom-permissions-for-account-users.md) and assign [product-level permissions](set-custom-permissions-for-account-users.md#product-level-permissions) so that they can only work with specific apps and/or add-ons.
-   You can allow a user, group, or Azure AD application to have access to more than one role's functionality by selecting multiple roles, or by using [custom permissions](set-custom-permissions-for-account-users.md) to grant the access you'd like.
-   A user with a certain role (or set of custom permissions) may also be part of a group that has a different role (or set of permissions). In that case, the user will have access to all of the functionality associated with both the group and the individual account.


## Roles and permissions

When adding a user, group, or Azure AD application, you must specify their permissions. You can do this by granting them a **standard role**, or by [customizing their permissions](set-custom-permissions-for-account-users.md).

Unless you use custom permissions, each user, group, or Azure AD application that you add to an account must be assigned at least one of the following standard roles. Each role has a specific set of permissions in order to perform certain functions within the account. 

> [!NOTE]
> The owner of the account is the person who first created it with a Microsoft account (and not any user(s) added through Azure AD). This account owner is the only person with complete access to the account, including the ability to delete apps, create and edit all account users, and change all financial and account settings. 


| Role                 | Description              |
|----------------------|--------------------------|
| Manager              | Has complete access to the account, except for changing tax and payout settings. This includes managing users in Dev Center, but note that the ability to create and delete users is dependent on the account's permission in Azure AD. That is, if a user is assigned the Manager role, but does not have admin permissions in the organization's Azure AD, they will not be able to create new users or delete users from the directory (though they can change a user's Dev Center role). |
| Developer            | Can upload packages and submit apps and add-ons, and can view the [Usage report](usage-report.md) for telemetry details. Can’t view financial info or account settings.   |
| Business Contributor | Can view [Health](health-report.md) and [Usage](usage-report.md) reports. Can't create or submit products, change account settings, or view financial info.                                         |
| Finance Contributor  | Can view [payout reports](payout-summary.md), financial info, and acquisition reports. Can’t make any changes to apps, add-ons, or account settings.                                                                                                                                   |
| Marketer             | Can [respond to customer reviews](respond-to-customer-reviews.md) and view non-financial [analytic reports](analytics.md). Can’t make any changes to apps, add-ons, or account settings.      |

The table below shows some of the specific features available to each of these roles (and to the account owner).

|                                 |    Account owner                 |    Manager                       |    Developer                     |    Business Contributor    |    Finance Contributor    |    Marketer                      |
|---------------------------------|----------------------------------|----------------------------------|----------------------------------|----------------------------|---------------------------|----------------------------------|
|    Acquisition report           |    Can view                      |    Can view                      |     No access                    |     No access              |    Can view               |    No access                     |
|    Feedback report/responses    |    Can view and send feedback    |    Can view and send feedback    |    Can view and send feedback    |     No access              |     No access             |    Can view and send feedback    |
|    Health report                |    Can view                      |    Can view                      |    Can view                      |    Can view                |     No access             |    No access                     |
|    Usage report                 |    Can view                      |    Can view                      |    Can view                      |    Can view                |     No access             |    No access                     |
|    Payout account               |    Can update                    |    No access                     |    No access                     |    No access               |    Can view               |    No access                     |
|    Tax profile                  |    Can update                    |    No access                     |    No access                     |    No access               |    Can view               |    No access                     |
|    Payout summary               |    Can view                      |    No access                     |    No access                     |    No access               |    Can view               |    No access                     |

If none of the standard roles are appropriate, or you wish to limit access to specific apps and/or add-ons, you can grant custom permissions to the user by clicking **Customize permissions**. For more info, see [Set custom permissions for account users](set-custom-permissions-for-account-users.md).


## Add and manage account users

To identify users that you want to add to your Dev Center account and assign them a role, click **Add users**.

You can add users from your organization's directory, create new users to add to your Dev Center account and  organization's directory, or invite users to access your Dev Center account without adding them to your organization's directory.

Note that when you add more than one user at the same time, you must assign the same role. If you want to add multiple users but assign them different roles, repeat the steps below for each role.


### Add users from your organization's directory

1.  From the **Manage users** page, click **Add users**.
2.  Select one or more users from the list that appears. You can use the search box to search for specific users.
3.  When you are finished selecting users, click **Add selected**.
4.  In the **Roles** section, select one or more roles to assign to this set of users.
5.  Click **Save**.



### Create a new user account in Dev Center and in your organization's directory

If you want to grant Dev Center access to a brand new user account, you can create one in the **Manage users** section by clicking **New user**.

If you don't want to create new accounts in your organization's directory, but wish to let users access your account using their Microsoft accounts to log in, select **Invite users by email** instead.

If you want the new user to have a [Global administrator account](http://go.microsoft.com/fwlink/p/?LinkId=746654) in your organization's directory, check the box labeled **Make this user a Global administrator in your Azure AD, with full control over all directory resources**. This will give the user full access to all administrative features in your company's Azure AD. They'll be able to add and manage users in your organization's directory (though not in Dev Center, unless you grant the account the appropriate [role/permissions](#roles-and-permissions)). If you check this box, you'll need to provide a **Password recovery email** for the user.

1.  From the **Manage users** page, click **Add users**.
2.  On the next page, click **New user**.
3.  Ensure that the **Add to Azure AD** radio button is selected to create a new account in your organization's directory and add that user to your Dev Center account. 
4.  Enter the first name, last name, and username for the new user.
5.  Enter an email that the user can use if they need to recover their password. This is only required if you checked the box to **Make this user a Global administrator in your Azure AD**.
6.  In the **Group membership** section, select any groups to which you want the new user to belong.
7.  In the **Roles** section, select one or more roles to assign to the new user, or assign customized permissions.
8.  Click **Save**.
9.  On the confirmation page, you'll see login info for the new user, including a temporary password. Be sure to note this info and provide it to the new user, as you won't be able to access the temporary password after you leave this page.


<span id="email" />
### Create a new user account in Dev Center without adding the user to your organization's directory

To invite users by email without creating a new user in your organization's directory, follow the steps below. Note that you can't add these users to groups, since those are managed through Azure AD.

1.  From the **Manage users** page, click **Add users**.
2.  On the next page, click **New user**.
3.  Select the **Invite users by email** radio button.
3.  Enter one or more email addresses (up to ten), separated by commas or semicolons.
4.  In the **Roles** section, select one or more roles to assign to the new user, or assign customized permissions.
6.  Click **Save**.

The users you've invited will get an email with an invitation to access your Dev Center account. Each user will need to accept their invitation before they can access your account.

If you need to resend an invitation, find the user on your **Manage users** page and select their email address (or the text that says **Invitation pending**) to edit the account. Then, at the bottom of the page, click **Resend invitation**.


### Edit a user account

You can make changes to user accounts that you've added to your Dev Center account in the **Manage users** section. Note that changes to the user's name or group membership will be reflected in your organization's directory, not just in your Dev Center account. Changes made to a user's role will only affect their Dev Center access.

1.  From the **Manage users** page, click the name of the user account that you want to edit.
2.  Make any of the following changes:
    -   Edit the user's first name, last name, or username. Remember, these changes will be made in your organization's directory.
    -   In the **Roles** section, select or deselect the role(s) that you want to add or remove for this user, or assign customized permissions.
    -   In the **Group membership** section, select or deselect the group(s) that you want the user to join or be removed from. Remember, these changes will be made in your organization's directory.

3.  Click **Save**.


### Changing a user's directory password

If you need to change a password for a user account that you've added to your Dev Center account, you can do so in the **Manage users** section. Note that this will change the user's directory password, not just the password for their Dev Center access.

If you've provided a **Password recovery email** when creating the user account, they will be able to reset their own password. You can also update a user's password by following the steps below.

1.  From the **Manage users** page, click the name of the user account that you want to edit.
2.  Click the **Reset password** button at the bottom of the page.
3.  A confirmation page will appear showing the login info for the user, including a temporary password.

   > [!IMPORTANT]
   >  Be sure to print or copy this info and provide it to the user, as you won't be able to access the temporary password after you leave this page.


## Add and manage groups

When you add a group from your organization's directory to your Dev Center account, every user who is a member of that group will be able to access it, with the permissions associated with the group's assigned role. Keep in mind that any changes made to groups (including their name or membership) will be reflected in your organization's directory.

Note that when you add more than one group at the same time, you must assign the same role. If you want to add groups but assign them different roles, repeat the steps below for each role.


### Add groups from your organization's directory

1.  From the **Manage users** page, click **Add groups**.
2.  Select one or more groups from the list that appears. You can use the search box to search for specific groups.
3.  When you are finished selecting groups, click **Add selected**.
4.  In the **Roles** section, select one or more roles to assign to this set of groups, or assign customized permissions.
5.  Click **Save**.


### Create a new group account

If you want to grant Dev Center access to a brand new group, you can create a new group in the **Manage users** section. Note that the new group will be created in your organization's directory, not just in your Dev Center account.

1.  From the **Manage users** page, click **Add groups**.
2.  On the next page, click **New group**.
3.  Enter the display name for the new group.
4.  Select one or more roles to assign to the new group, or assign customized permissions. All members of the group will be able to access your Dev Center account with the permissions associated with that role
5.  Select one or more users from the list that appears. You can use the search box to search for specific users.
6.  When you are finished selecting users, click **Add selected**.
7.  Click **Save**.


### Edit a group account

You can make changes to group accounts that you've added to your Dev Center account in the **Manage users** section. Note that changes to the group's name and membership will be reflected in your organization's directory, not just in your Dev Center account. Changes made to a group's role will only affect that group's Dev Center access.

1.  From the **Manage users** page, click the name of the group account that you want to edit.
2.  To change group info, make any desired changes to the group's name. Remember, these changes will be made in your organization's directory.
3.  To change the group role, select or deselect the role(s) that you want to apply to the group, or assign customized permissions.
4.  Click **Save**.


## Add and manage Azure AD applications

You can allow applications or services that are part of your organization's Azure AD to access your Dev Center account.

Note that when you add more than one Azure AD application at the same time, you must assign the same role. If you want to add groups but assign them different roles, repeat the steps below for each role.


### Add Azure AD applications from your organization's directory

1.  From the **Manage users** page, click **Add Azure AD applications**.
2.  Select one or more Azure AD applications from the list that appears. You can use the search box to search for specific Azure AD applications.
3.  When you are finished selecting Azure AD applications, click **Add selected**.
4.  In the **Roles** section, select one or more roles to assign to this set of Azure AD applications, or assign customized permissions.
5.  Click **Save**.

### Create a new Azure AD application

If you want to grant Dev Center access to a brand new Azure AD application account, you can create one in the **Manage users** section. Note that this will create a new account in your organization's directory, not just in your Dev Center account.

> [!TIP]
> If you are primarily using this Azure AD application for Dev Center authentication, and don't need users to access it directly, you can enter any valid address for the **Reply URL** and **App ID URI**, as long as those values are not used by any other Azure AD application in your directory.

1.  From the **Manage users** page, click **Add Azure AD applications**.
2.  On the next page, click **New Azure AD application**.
3.  Enter the **Reply URL** for the new Azure AD application. This is the URL where users can sign in and use your Azure AD application (sometimes also known as the App URL or Sign-On URL). The **Reply URL** can't be longer than 256 characters.
4.  Enter the **App ID URI** for the new Azure AD application. This is a logical identifier for the Azure AD application that is presented when it sends a single sign-on request to Azure AD. Note that the **App ID URI** must be unique for each Azure AD application in your directory, and it can't be longer than 256 characters.
5.  In the **Roles** section, select one or more roles to assign to the new Azure AD application, or assign customized permissions.
6.  Click **Save**.

After you add or create an Azure AD application, you can return to the **Manage users** section and click the application name to review settings for the application, including the Tenant ID, Client ID, Reply URL, and App ID URI.

> [!NOTE]
> If you intend to use the REST APIs provided by the [Windows Store services](../monetize/using-windows-store-services.md), you will need the Tenant ID and Client ID values shown on this page to obtain an Azure AD access token that you can use to authenticate the calls to services.   


### Edit an Azure AD application

You can make changes to Azure AD applications that you've added to your Dev Center account in the **Manage users** section. Note that changes to the Reply URL and App ID URI will be reflected in your organization's directory, not just in your Dev Center account. Role changes will only affect the Azure AD application's permissions within Dev Center.

1.  From the **Manage users** page, click the name of the Azure AD application account that you want to edit.
2.  To change the **Reply URL** or **App ID URI**, enter the new values here. Remember, these changes will be made in your organization's directory.
3.  To change the Azure AD application's role, select or deselect the role(s) that you want to apply, or assign customized permissions.
4.  Click **Save**.


### Manage keys for an Azure AD application

If your Azure AD application reads and writes data in Microsoft Azure AD, it will need a key. You can create keys for an Azure AD application by editing its info in Dev Center. You can also remove keys that are no longer needed.

1.  From the **Manage users** page, click the name of the Azure AD application.

    > [!TIP]
    > When you click the name of the Azure AD application, you'll see all of the active keys for the Azure AD application, including the date on which the key was created and when it will expire. To remove a key that is no longer needed, click **Remove**.

2.  To add a new key, click **Add new key**.

3.  You will see a screen showing the **Client ID** and **Key** values.

    > [!IMPORTANT]
    > Be sure to print or copy this info, as you won't be able to access it again after you leave this page.

4.  If you want to create more keys, click **Add another key**.

## View history for account users

As an account owner, you can view the detailed browsing history for any additional users you’ve added to the account.

On the **Manage users** page, click the link shown under **Last activity** for the user whose browsing history you’d like to review. You'll be able to view the URLs for all pages that the user visited in the last 30 days.

## Removing users, groups, and Azure AD applications

To remove a user, group, or Azure AD application from your Dev Center account, click the **Remove** link that appears by their name on the **Manage users** page. After confirming that you want to remove it, that user, group, or Azure AD application will no longer be able to access to your Dev Center account (unless you add it again later).

> [!NOTE] 
> Removing a user, group, or Azure AD application means that it will no longer have access to your Dev Center account. It does not delete the user, group, or Azure AD application from your organization's directory.

 

 

 
