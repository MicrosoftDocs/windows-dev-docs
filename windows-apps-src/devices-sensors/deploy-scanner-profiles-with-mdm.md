---
title: Deploy barcode scanner profiles with MDM
description: Learn how to deploy barcode scanner profiles with a Mobile Device Management (MDM) server using the EnterpriseExtFileSystem configuration service provider (CSP).
ms.assetid: 99ED3BD8-022C-40C2-9C65-F599186548FE
ms.date: 09/26/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Deploy barcode scanner profiles with MDM

**Note**  This feature requires Windows 10 Mobile or later.

Barcode scanner profiles can be deployed with an MDM server. To deploy the profiles, use *OemProfile* in the [EnterpriseExtFileSystem CSP](/windows/client-management/mdm/enterpriseextfilessystem-csp) to place them into the \\Data\\SharedData\\OEM\\Public\\Profile folder. These scanner profiles can then be used by driver manufacturers to configure settings that are not exposed through the API surface.

Microsoft does not define the specifics of a scanner profile or how to implement them.

## Related topics
- [EnterpriseExtFileSystem CSP](/windows/client-management/mdm/enterpriseextfilessystem-csp)
- [Barcode scanner device support](./pos-device-support.md#barcode-scanner)