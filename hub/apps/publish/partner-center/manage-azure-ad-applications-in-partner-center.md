---
description: You can manage Azure AD applications in your Partner Center account.
title: Manage Azure AD applications in your Partner Center account
ms.date: 11/07/2022
ms.topic: article
keywords: windows 10, uwp, azure ad application, aad, user, group, multiple users, multi-user
ms.localizationpriority: medium
---
# Manage Azure AD applications in your Partner Center account

You can allow applications or services that are part of your organization's Azure AD to access your Partner Center account. These Azure AD application user accounts can be used to call the REST APIs provided by the [Microsoft Store services](/windows/uwp/monetize/using-windows-store-services).


## Add Azure AD applications from your organization's directory

1. Select the gear icon (near the upper right corner of Partner Center) and then select **Developer settings**. In the **Settings** menu, select **Users**.
2. From the **Users** page, select **Add Azure AD applications**.
3.  Select one or more Azure AD applications from the list that appears. You can use the search box to search for specific Azure AD applications.
    > [!TIP]
    > If you select more than one Azure AD application to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple Azure AD applications with different roles/permissions, repeat the steps below for each role or set of custom permissions.

4.  When you are finished selecting Azure AD applications, click **Add selected**.
5.  In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected Azure AD application(s).
6.  Click **Save**.


## Create a new Azure AD application account in your organization's directory and add it to your Partner Center account

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
> If you intend to use the REST APIs provided by the [Microsoft Store services](../../../../uwp/monetize/using-windows-store-services.md), you will need the Tenant ID and Client ID values shown on this page to obtain an Azure AD access token that you can use to authenticate the calls to services.   

<span id="manage-keys" />

## Manage keys for an Azure AD application

If your Azure AD application reads and writes data in Microsoft Azure AD, it will need a key. You can create keys for an Azure AD application by editing its info in Partner Center. You can also remove keys that are no longer needed.

1.  From the **Users** page (under **Account settings**), select the name of the Azure AD application.
    > [!TIP]
    > When you click the name of the Azure AD application, you'll see all of the active keys for the Azure AD application, including the date on which the key was created and when it will expire. To remove a key that is no longer needed, click **Remove**.

2.  To add a new key, select **Add new key**.
3.  You will see a screen showing the **Client ID** and **Key** values.
    > [!IMPORTANT]
    > Be sure to print or copy this info, as you won't be able to access it again after you leave this page.

4.  If you want to create more keys, select **Add another key**.

## Edit an Azure AD application

After you've added users, groups, and/or Azure AD applications to your Partner Center account, you can make changes to their account info. 

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL and App ID URI for an Azure AD application) will be reflected in your organization's Azure AD tenant as well as in your Partner Center account. 

1.  From the **Users** page (under **Account settings**), select the name of the user, group, or Azure AD application account that you want to edit.
2.  Make your desired changes. For an **Azure AD application**, you can enter new values for the **Reply URL** or **App ID URI**.
    Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
3.  For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Azure AD tenant.
3.  Click **Save**.


## View history for account users

As an account owner, you can view the detailed browsing history for any additional users you’ve added to the account.

On the **Users** page (under **Account settings**), select the link shown under **Last activity** for the user whose browsing history you’d like to review. You'll be able to view the URLs for all pages that the user visited in the last 30 days.

<span id="remove" />

## Remove Azure AD applications

To remove Azure AD application from your Partner Center account, select the **Remove** link that appears by their name on the **Users** page. After confirming that you want to remove it, that Azure AD application will no longer be able to access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing an Azure AD application means that it will no longer have access to your Partner Center account. It does **not** delete the Azure AD application from your organization's directory.
