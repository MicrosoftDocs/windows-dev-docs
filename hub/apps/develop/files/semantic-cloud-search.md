---
title: Search files with semantic matching and cloud providers
description: Use QueryOptions to add AI-powered semantic search and cloud provider results when querying files in your Windows app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 08/19/2026
---

# Search files with semantic matching and cloud providers

You can extend file queries in your Windows app to include AI-powered semantic matching and results from cloud storage providers such as OneDrive. Two properties on the [QueryOptions](/uwp/api/windows.storage.search.queryoptions) class control these capabilities:

- **IncludeLocalSemanticIndex** — Expands search results by matching synonyms and related concepts. For example, a search for "car" also returns files tagged with "automobile", "vehicle", or "sedan".
- **IncludeCloudProviders** — Adds results from cloud storage providers (such as OneDrive) alongside local results.

Both properties default to `false`, so existing queries continue to work without changes. Each property is independent — you can enable one, both, or neither.

## Prerequisites

- **Windows OS with UniversalApiContract v19 or later.** These properties are available on any Windows platform that supports Windows.Foundation.UniversalApiContract version 19.
- **Existing file access permissions.** The same [file access permissions](file-access-permissions.md) that apply to `Windows.Storage.Search` queries apply here.
- **Pictures Library access for the sample.** The sample uses `KnownFolders.PicturesLibrary`. Declare the `picturesLibrary` capability in your package manifest, or adapt the sample to use a folder that the user selects.
- **Cloud provider configured and online** (for cloud provider results). Cloud search requires the user to be signed in to a supported cloud provider (currently OneDrive) and connected to the internet.

> [!NOTE]
> Because feature enablement may be more granular than the API contract version, use [ApiInformation.IsPropertyPresent](/uwp/api/windows.foundation.metadata.apiinformation.ispropertypresent) to check at runtime whether the properties are available:
>
> ```csharp
> bool semanticAvailable = Windows.Foundation.Metadata.ApiInformation.IsPropertyPresent(
>     "Windows.Storage.Search.QueryOptions", "IncludeLocalSemanticIndex");
>
> bool cloudAvailable = Windows.Foundation.Metadata.ApiInformation.IsPropertyPresent(
>     "Windows.Storage.Search.QueryOptions", "IncludeCloudProviders");
> ```

## How it works

Both properties are **additive**. The base Windows Search indexer always runs, and each property adds an extra source of results on top:

| Property | What it adds | Fallback behavior |
|----------|-------------|-------------------|
| `IncludeLocalSemanticIndex` | AI synonym and concept expansion (e.g., "sunset" matches "dusk", "twilight") | If the semantic index is unavailable, the query falls back to standard keyword matching silently. |
| `IncludeCloudProviders` | Cloud provider results (such as OneDrive) merged with local results | If the device is offline or no cloud provider is configured, the query returns local results only. |

These option-specific fallbacks are automatic and silent. You don't need special-case error handling when either option is unavailable, but your app should continue to handle errors from file access and query operations.

> [!NOTE]
> Both semantic index search and cloud files search require Windows 11, version 24H2 or later running on a [Copilot+ PC](https://www.microsoft.com/windows/shop-pcs/high-performance-computers) or an [AI-enabled Cloud PC](/windows-365/enterprise/ai-enabled-cloud-pcs). On other devices, these properties are silently ignored and the query returns standard local results only.

## Search for photos using semantic matching and cloud results

The following example searches the user's Pictures library for images matching a search term, using both semantic matching and cloud results ranked by relevance.

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

// Searches the Pictures library using semantic (concept/synonym) matching
// and federated cloud results (e.g. OneDrive), ordered by search relevance.
public async Task<IReadOnlyList<StorageFile>> SearchPicturesAsync(string searchText)
{
    var queryOptions = new QueryOptions(CommonFileQuery.OrderBySearchRank,
        new[] { ".jpg", ".jpeg", ".png", ".heic", ".gif" })
    {
        UserSearchFilter = searchText,          // e.g. "car", "sunset"
        IncludeLocalSemanticIndex = true,       // match related concepts (car → vehicle, sedan)
        IncludeCloudProviders = true            // include OneDrive / cloud-backed results
    };

    StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
    StorageFileQueryResult queryResult = picturesFolder.CreateFileQueryWithOptions(queryOptions);

    IReadOnlyList<StorageFile> files = await queryResult.GetFilesAsync();

    foreach (StorageFile file in files)
    {
        // Results include local, semantic, and cloud matches, ranked by relevance.
        System.Diagnostics.Debug.WriteLine($"{file.Name} — {file.Path}");
    }

    return files;
}
```

<!-- TODO: Add C++/WinRT code sample if the SME confirms it's needed. -->

> [!TIP]
> You can enable just one property. For example, set `IncludeLocalSemanticIndex = true` and leave `IncludeCloudProviders` at its default (`false`) to get semantic matching without cloud results.

## Graceful degradation

You don't need special-case error handling when these properties are unavailable. Continue to handle errors from file permissions, storage availability, and query operations. The search pipeline handles property unavailability as follows:

- **Semantic index not available** (older OS, index not built yet, unsupported language): The query returns standard keyword results. No exception is thrown.
- **Cloud provider offline** (no internet, cloud provider not configured): The query returns local results only. No exception is thrown.
- **Both properties set to `false`** (the default): Traditional `Windows.Storage.Search` behavior, fully backward compatible.

## Known limitations

<!-- TODO: Confirm these are still accurate with the SME before publishing. -->

- **Cloud search currently supports OneDrive only.** The `IncludeCloudProviders` API is public and can be implemented by other cloud storage providers, but OneDrive is the only provider in the initial release.
- **Semantic matching supports six languages**: English, Spanish, French, German, Japanese, and Chinese. Additional languages are planned for future releases.
- **Feature availability may be gated independently of the API contract.** Use `ApiInformation.IsPropertyPresent` (shown above) rather than contract version checks to confirm the feature is enabled on a given device.

## Related content

- [QueryOptions class](/uwp/api/windows.storage.search.queryoptions)
- [QueryOptions.IncludeLocalSemanticIndex property](/uwp/api/windows.storage.search.queryoptions.includelocalsemanticindex)
- [QueryOptions.IncludeCloudProviders property](/uwp/api/windows.storage.search.queryoptions.includecloudproviders)
- [Enumerate and query files and folders](list-files-folders.md)
- [Fast access to file properties](fast-file-properties.md)
- [File access permissions](file-access-permissions.md)
