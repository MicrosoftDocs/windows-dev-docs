---
description: Learn about cloud file provider integration with Windows Search.
title: Cloud file provider integration with Windows Search
ms.topic: article
ms.date: 05/08/2025
ms.localizationpriority: medium
---

Windows Search integrates with Cloud Sync Engines. When searching for files, Windows Search will call into all registered cloud file providers. Cloud file providers must implement [IStorageProviderSearchHandlerFactory](/uwp/api/windows.storage.provider.istorageprovidersearchhandlerfactory) in order to be integrated into the Windows Search experience. For more information about implementing a Cloud Sync Engine, see [Cloud Sync Engines](/windows/win32/cfapi/cloud-files-api-portal). 