---
title: Overview of custom permissions for account users
description: Learn how to assign custom permissions when adding users to your Partner Center account.
ms.assetid: 99f3aa18-98b4-4919-bd7b-d78356b0bf78
ms.date: 11/09/2022
ms.topic: article
keywords: windows 10, windows 11, uwp, desktop bridge, msix, win32, msi, exe, pwa, user roles, user permission, custom roles, user access, customize permissions
ms.localizationpriority: medium
---
# Overview of custom permissions for account users

To assign custom permissions rather than standard roles, click **Customize permissions** in the **Roles** section when adding or editing the user account.

To enable a permission for the user, toggle the box to the appropriate setting.

![Guide to access settings](../images/permission-key.png)

- **No access**: The user will not have the indicated permission.
- **Read only**: The user will have access to view features related to the indicated area, but will not be able to make changes.
- **Read/write**: The user will have access to make changes associated with the area, as well as viewing it.
- **Mixed**: You can’t select this option directly, but the **Mixed** indicator will show if you have allowed a combination of access for that permission. For example, if you grant **Read only** access to **Pricing and availability** for **All products**, but then grant **Read/write** access to **Pricing and availability** for one specific product, the **Pricing and availability** indicator for **All products** will show as Mixed. The same applies if some products have **No access** for a permission, but others have **Read/write** and/or **Read only** access.

For some permissions, such as those related to viewing analytic data, only **Read only** access can be granted. Note that in the current implementation, some permissions have no distinction between **Read only** and **Read/write** access. Review the details for each permission to understand the specific capabilities granted by **Read only** and/or **Read/write** access.

The specific details about each permission are described in the next couple of pages.
