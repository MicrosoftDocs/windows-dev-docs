---
description: Learn where to find current guidance for handling camera privacy settings and access-denied errors in UWP camera apps.
title: Handle the Windows camera privacy setting
ms.date: 09/08/2026
ms.topic: how-to
keywords: windows 10, uwp
dev_langs:
- csharp
ms.localizationpriority: medium
---

# Handle the Windows camera privacy setting

UWP apps must account for the Windows camera privacy setting before they initialize camera capture. Because UWP apps are packaged, you can use the `AppCapability` class to check access to the webcam capability. You must also handle `E_ACCESSDENIED` when camera initialization fails.

The camera privacy setting and camera capture APIs are shared across Windows app models. For the current guidance and code examples, see [Handle the Windows camera privacy setting](/windows/apps/develop/camera/camera-privacy-setting).
