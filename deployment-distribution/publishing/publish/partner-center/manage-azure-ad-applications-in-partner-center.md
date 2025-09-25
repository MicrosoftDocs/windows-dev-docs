---
description: You can manage Microsoft Entra applications in your Partner Center account.
title: Manage Microsoft Entra applications in your Partner Center account
ms.date: 11/07/2022
ms.topic: how-to
ms.localizationpriority: medium
---

# Manage Microsoft Entra applications in your Partner Center account

You can allow applications or services that are part of your organization's Microsoft Entra ID to access your Partner Center account. These Microsoft Entra application user accounts can be used to call the REST APIs provided by the [Microsoft Store services](/windows/uwp/monetize/using-windows-store-services).

To manage Microsoft Entra applications in your Partner Center account, go to the **User management** page under **Account settings** and select the **Microsoft Entra applications** tab. You must be signed in with a Manager account that also has [global administrator](/azure/active-directory/users-groups-roles/directory-assign-admin-roles) permissions for the Microsoft Entra ID tenant you're working in. 

:::image type="content" source="../images/partner-center-account-settings-apps.png" alt-text="Screenshot showing microsoft entra applications tab in user management page.":::

To add applications, you will have additional options described below.

:::image type="content" source="../images/partner-center-account-settings-add-apps.png" alt-text="Screenshot showing different options to add a microsoft entra application.":::

## Create Microsoft Entra application

1. From the **User management** page (under **Account settings**), click on **Add Microsoft Entra application**, then choose **Create Microsoft Entra application**.
1. Enter the display name for the new application.
1. Enter the Reply URL for the new application. This is the URL where users can sign in and use your Microsoft Entra application (sometimes also known as the App URL or Sign-On URL). The Reply URL can't be longer than 256 characters and must be unique within your directory. Then click **Next**.
1. In the **Roles applicable to developer programs** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the application.
1. Click **Create**.

After you add or create a Microsoft Entra application, you can return to this section and select the application name to review settings for the application, including the Tenant ID, Client ID, and Reply URL.

> [!NOTE]
> If you intend to use the REST APIs provided by the Microsoft Store services, you will need the Tenant ID and Client ID values shown on this page to obtain a Microsoft Entra access token that you can use to authenticate the calls to services.

## Add Microsoft Entra application

1. From the **User management** page (under **Account settings**), click on **Add Microsoft Entra application**, then choose **Add Microsoft Entra application**.
1. Select one or more Microsoft Entra applications from the list that appears. You can use the search box to find specific applications.

    > [!TIP]
    > If you select more than one Microsoft Entra application to add to your Partner Center account, you must assign them the same role or set of custom permissions. To add multiple Microsoft Entra applications with different roles or permissions, repeat these steps for each role or set of custom permissions.
1. When you have finished selecting applications, click **Next**.
1. In the **Roles applicable to developer programs** section, specify the [role(s) or customized permissions](set-custom-permissions-for-account-users.md) for the selected application(s).
1. Click **Add**.

## Manage keys for a Microsoft Entra application

If your Microsoft Entra application reads and writes data in Microsoft Entra ID, it will need a key. You can create keys for a Microsoft Entra application by editing its info in Partner Center. You can also remove keys that are no longer needed.

1. From the **User management** page (under **Account settings**), click on the Microsoft Entra application.

    > [!TIP]
    > When you click on the name of the Microsoft Entra application, you'll see all of the active keys for the Microsoft Entra application, including the date on which the key was created and when it will expire. To remove a key that is no longer needed, click on **Remove**.

1. To add a new key, click on **Add new key**.
1. You will see a screen showing the **Client ID** and **Key** values.

    > [!IMPORTANT]
    > Be sure to print or copy this info, as you won't be able to access it again after you leave this page.

1. If you want to create more keys, select **Add another key**.

## Edit a Microsoft Entra application

1. From the **User management** page (under **Account settings**), click on the Microsoft Entra application that you want to edit.
1. You will see a screen showing all the available keys, then click **Cancel**.
1. Make your desired changes. For a Microsoft Entra application, you can enter new values for the display name and Reply URL. Remember that these changes will be made in your organization's directory as well as in your Partner Center account.
1. When you have finished updating the application information, click **Next**.
1. For changes related to Partner Center access, select or deselect the role(s) that you want to apply, or select **Customize permissions** and make the desired changes. These changes only impact Partner Center access and will not change any permissions within your organization's Microsoft Entra ID tenant.
1. Click **Update**.

> [!IMPORTANT]
> Changes made to [roles or permissions](set-custom-permissions-for-account-users.md) will only affect Partner Center access. All other changes (such as changing a user's name or group membership, or the Reply URL for a Microsoft Entra ID application) will be reflected in your organization's Microsoft Entra ID tenant as well as in your Partner Center account.

## Remove a Microsoft Entra application

1. From the **User management** page (under **Account settings**), select the Microsoft Entra application that you want to remove.
1. Click on **Delete**.
1. After confirming the removal, that Microsoft Entra application will no longer have access to your Partner Center account (unless you add it again later).

> [!IMPORTANT]
> Removing a Microsoft Entra application means that it will no longer have access to your Partner Center account. It **doesn't** delete the Microsoft Entra application from your organization's directory.