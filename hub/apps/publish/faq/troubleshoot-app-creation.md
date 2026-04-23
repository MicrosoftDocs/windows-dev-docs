---
title: Troubleshoot App Creation in Partner Center
description: Troubleshoot app creation errors in Partner Center
ms.date: 10/30/2022
ms.topic: troubleshooting-general
ms.localizationpriority: medium
---

# Troubleshoot App creation

If you encounter a permission error when you attempt to create a new app in Partner Center, please ensure the following settings are configured correctly in your [Microsoft Entra ID portal](https://portal.azure.com).

## Enable app registrations

1. Navigate to the [User settings blade](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/UserSettings) in Microsoft Entra ID.
1. Ensure that the **Users can register applications** setting is set to **Yes**.

## Assign Application developer permissions

1. Navigate to the [Roles and administrators](https://aka.ms/AADRolesAdmin) blade in Microsoft Entra ID portal.
1. Ensure that the user attempting to create a new app has **Application developer** permissions.

## Create a new Microsoft Entra ID tenant

If the above steps do not resolve the error, create a new Entra ID tenant and associate it with your Partner Center account. For more information, see [Associate Azure Entra ID with your Partner Center account](../partner-center/associate-existing-azure-ad-tenant-with-partner-center-account.md).
