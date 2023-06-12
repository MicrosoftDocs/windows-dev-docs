---
title: Deploy barcode scanner profiles with MDM
description: Learn how to deploy barcode scanner profiles with a Mobile Device Management (MDM) server using the EnterpriseExtFileSystem configuration service provider (CSP).

ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Deploy barcode scanner profiles with a Mobile Device Management server

Barcode scanner profiles can be deployed with a Mobile Device Management (MDM) server. To deploy the profiles, use *OemProfile* in the [EnterpriseExtFileSystem CSP](/windows/client-management/mdm/enterpriseextfilesystem-csp) to place them into the \\Data\\SharedData\\OEM\\Public\\Profile folder. These scanner profiles can then be used by driver manufacturers to configure settings that are not exposed through the API surface.

Microsoft does not define the specifics of a scanner profile or how to implement them.

> [!NOTE]
> This feature requires Windows 10 Mobile or later.

## Related topics

- [EnterpriseExtFileSystem CSP](/windows/client-management/mdm/enterpriseextfilesystem-csp)
- [Barcode scanner device support](./pos-device-support.md#barcode-scanner)
