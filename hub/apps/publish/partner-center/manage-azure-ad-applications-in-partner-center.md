---
description: You can manage Microsoft Entra applications in your Partner Center account.
title: Manage Microsoft Entra applications in your Partner Center account
ms.date: 11/07/2022
ms.topic: article
ms.localizationpriority: medium
---

# Manage Microsoft Entra applications in your Partner Center account

You can allow applications or services that are part of your organization's Microsoft Entra ID to access your Partner Center account. These Microsoft Entra application user accounts can be used to call the REST APIs provided by the [Microsoft Store services](/windows/uwp/monetize/using-windows-store-services).

## Add Microsoft Entra applications from your organization's directory

1. Select the gear icon (near the upper right corner of Partner Center) and then select **Account settings**. In the **Settings** menu, select **Users**.
1. From the **Users** page, select **Add Microsoft Entra applications**.
1. Select one or more Microsoft Entra applications from the list that appears. You can use the search box to search for specific Microsoft Entra applications.

    > [!TIP]
    > If you select more than one Microsoft Entra application to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple Microsoft Entra applications with different roles/permissions, repeat the steps below for each role or set of custom permissions.

1. When you are finished selecting Microsoft Entra applications, select **Add selected**.
1. In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected Microsoft Entra application(s).
1. Select **Save**.

## Create a new Microsoft Entra application account in your organization's directory and add it to your Partner Center account

If you want to grant Partner Center access to a brand new Microsoft Entra application account, you can create one in the **Users** section. Note that this will create a new account in your organization's directory, not just in your Partner Center account.

> [!TIP]
> If you are primarily using this Microsoft Entra application for Partner Center authentication, and don't need users to access it directly, you can enter any valid address for the **Reply URL** and **App ID URI**, as long as those values are not used by any other Microsoft Entra application in your directory.

1. From the **Users** page (under **Account settings**), select **Add Microsoft Entra applications**.
1. On the next page, select **New Microsoft Entra application**.
1. Enter the **Reply URL** for the new Microsoft Entra application. This is the URL where users can sign in and use your Microsoft Entra application (sometimes also known as the App URL or Sign-On URL). The **Reply URL** can't be longer than 256 characters and must be unique within your directory.
1. Enter the **App ID URI** for the new Microsoft Entra application. This is a logical identifier for the Microsoft Entra application that is presented when it sends a single sign-on request to Microsoft Entra. Note that the **App ID URI** must be unique for each Microsoft Entra application in your directory, and it can't be longer than 256 characters. For more info about the **App ID URI**, see [Modify the accounts supported by an application](/entra/identity-platform/howto-modify-supported-accounts#change-the-application-registration-to-support-different-accounts).
1. In the **Roles** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the Microsoft Entra application.
1. Select **Save**.

After you add or create a Microsoft Entra application, you can return to the **Users** section and select the application name to review settings for the application, including the Tenant ID, Client ID, Reply URL, and App ID URI.

> [!NOTE]
> If you intend to use the REST APIs provided by the [Microsoft Store services](/windows/uwp/monetize/using-windows-store-services), you will need the Tenant ID and Client ID values shown on this page to obtain a Microsoft Entra access token that you can use to authenticate the calls to services.

## Manage keys for a Microsoft Entra application

If your Microsoft Entra application reads and writes data in Microsoft Entra ID, it will need a key. You can create keys for a Microsoft Entra application by editing its info in Partner Center. You can also remove keys that are no longer needed.

1. From the **User management** page (under **Account settings**), select the name of the Microsoft Entra application.

    > [!TIP]
    > When you select the name of the Microsoft Entra application, you'll see all of the active keys for the Microsoft Entra application, including the date on which the key was created and when it will expire. To remove a key that is no longer needed, select **Remove**.

1. To add a new key, select **Add new key**.
1. You will see a screen showing the **Client ID** and **Key** values.

    > [!IMPORTANT]
    > Be sure to print or copy this info, as you won't be able to access it again after you leave this page.

1. If you want to create more keys, select **Add another key**.

## Edit a Microsoft Entra application

After you've added users, groups, and/or Microsoft Entra applications to your Partner Center account, you can make changes to their account info.

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL and App ID URI for a Microsoft Entra application) will be reflected in your organization's Microsoft Entra tenant as well as in your Partner Center account.

1. From the **User management** page (under **Account settings**), select the name of the user, group, or Microsoft Entra application account that you want to edit.
1. Make your desired changes. For a **Microsoft Entra application**, you can enter new values for the **Reply URL** or **App ID URI**.
    Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
1. For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Microsoft Entra tenant.
1. Select **Save**.

## View history for account users

As an account owner, you can view the detailed browsing history for any additional users you’ve added to the account.

On the **User management** page (under **Account settings**), select the link shown under **Last activity** for the user whose browsing history you’d like to review. You'll be able to view the URLs for all pages that the user visited in the last 30 days.

## Remove Microsoft Entra applications

To remove Microsoft Entra application from your Partner Center account, select the radio button left to application, and then select **Remove** option on top of the table on the **User management** page. After confirming that you want to remove it, that Microsoft Entra application will no longer be able to access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing a Microsoft Entra application means that it will no longer have access to your Partner Center account. It **doesn't** delete the Microsoft Entra application from your organization's directory.

