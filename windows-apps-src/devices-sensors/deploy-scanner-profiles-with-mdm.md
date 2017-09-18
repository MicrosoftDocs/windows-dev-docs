---
title: Deploy barcode scanner profiles with MDM
author: PatrickFarley
description: Barcode scanner profiles can be deployed with an MDM server.
ms.assetid: 99ED3BD8-022C-40C2-9C65-F599186548FE
---

# Deploy barcode scanner profiles with MDM

**Note**  This feature requires Windows 10 Mobile or later.

Barcode scanner profiles can be deployed with an MDM server. To deploy the profiles, use *OemProfile* in the [EnterpriseExtFileSystem CSP](https://msdn.microsoft.com/library/windows/hardware/mt157025) to place them into the \\Data\\SharedData\\OEM\\Public\\Profile folder. These scanner profiles can then be used by driver manufacturers to configure settings that are not exposed through the API surface.

Microsoft does not define the specifics of a scanner profile or how to implement them.

## Related topics
- [EnterpriseExtFileSystem CSP](https://msdn.microsoft.com/library/windows/hardware/mt157025)
- [Barcode scanner device support](https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/pos-device-support#barcode-scanner)