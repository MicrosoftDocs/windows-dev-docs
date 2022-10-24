---
title: Troubleshoot App Creation in Partner Center
description: Troubleshoot app creation errors in Partner Center
ms.date: 10/24/2022
ms.topic: article
ms.localizationpriority: medium
---

# Troubleshoot App Creation in Partner Center

If you encounter a permission error when you attempt to create a new app in Partner Center, please ensure the following settings are configured correctly in your [Azure AAD portal](https://portal.azure.com).

## Enable app registrations

1. Navigate to the [User settings blade](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/UserSettings) in Azure AAD portal.
1. Ensure that the App registrations setting is set to **Yes**.

## Assign Application developer permissions

1. Navigate to the [Roles and administrators](https://aka.ms/AADRolesAdmin) blade in Azure AAD portal.
1. Ensure that the user attempting to create a new app has **Application developer' permissions.

## Create a new AAD tenant

If the above steps do not resolve the error, create a new AAD tenant and associate it with your Partner Center account. For more information, see [Associate Azure Active Directory with your Partner Center account](associate-azure-ad-with-partner-center).
