---
description: Users, groups, and applications in Azure Active Directory
title: Overview of several types of entities in Azure AD
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Users, groups, and applications in Azure Active Directory

The **Users** section of [Partner Center](https://partner.microsoft.com/dashboard) (under **Account settings**) lets you use Azure Active Directory to add users to your Partner Center account. Each user is assigned a role (or set of custom permissions) that defines their access to the account. You can also add [groups of users](manage-groups-in-partner-center.md) and [Azure AD applications](manage-azure-ad-applications-in-partner-center.md) to grant them access to your Partner Center account.

After users have been added to the account, you can edit account details, change roles and permissions or remove users.

> [!IMPORTANT]
>In order to add users to your account, you must first [associate your Partner Center account with your organization's Azure Active Directory tenant](associate-azure-ad-with-partner-center.md).

When adding users, you will need to specify their access to your Partner Center account by assigning them a [role or set of custom permissions](set-custom-permissions-for-account-users.md).

Keep in mind that all Partner Center users (including groups and Azure AD applications) must have an active account in [an Azure AD tenant that is associated with your Partner Center account](associate-azure-ad-with-partner-center.md). User management is done in one tenant at a time; you must sign in with a Manager account for the tenant in which you want to add or edit users. Creating a new user in Partner Center will also create an account for that user in the Azure AD tenant to which you are signed in, and making changes to a user's name in Partner Center will make the same changes in your organization's Azure AD tenant.

> [!NOTE]
> If your organization uses [directory integration](/previous-versions/azure/azure-services/jj573653(v=azure.100)) to sync the on-premises directory service with your Azure AD, you won't be able to create new users, groups, or Azure AD applications in Partner Center. You (or another admin in your on-premises directory) will need to create them directly in the on-premises directory before you'll be able to see and add them in Partner Center.
