---
description: Learn how cloud storage providers register search handlers to contribute file results to Windows Search on supported devices.
title: Cloud files provider integration with Windows Search
author: GrantMeStrength
ms.author: jken
ms.topic: article
ms.date: 08/30/2026
ms.localizationpriority: medium
---

# Cloud files provider integration with Windows Search

Cloud files search allows cloud storage providers to contribute results from remote storage to File Explorer and Windows Search. This feature is supported in Windows 11, version 24H2 and later on Copilot+ PCs and AI-enabled Cloud PCs.

Registering a sync root with the Cloud Files API doesn't automatically enable cloud files search. To participate in search, a cloud storage provider must:

1. Set the **SearchHandlerFactory** registry value under its sync root registry key to the class identifier (CLSID) of a COM local server.
1. Implement [IStorageProviderSearchHandlerFactory](/uwp/api/windows.storage.provider.istorageprovidersearchhandlerfactory) in that local server.
1. Return an [IStorageProviderSearchHandler](/uwp/api/windows.storage.provider.istorageprovidersearchhandler) implementation from the factory.

When Windows searches an eligible sync root, it calls the provider's [Find](/uwp/api/windows.storage.provider.istorageprovidersearchhandler.find) method with the search query options. Only providers that register and implement this search handler can contribute cloud results.

For more information about the registration requirements and implementing a Cloud Sync Engine, see [Build a Cloud Sync Engine that supports placeholder files](/windows/win32/cfapi/build-a-cloud-file-sync-engine#cloud-files-search).