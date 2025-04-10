---
title: InitializeFailFast function
description: Calls [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) to initialize the calling process to use the specified version of the Windows App SDK's framework package. If the call fails, aborts the process (via **std::abort**).
ms.topic: article
ms.date: 03/23/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, app sdk, bootstrapper, bootstrapper api
ms.localizationpriority: low
---

# InitializeFailFast function

Calls [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) to initialize the calling process to use the specified version of the Windows App SDK's framework package. If the call fails, aborts the process (via **std::abort**).

## Syntax
```cpp
inline auto InitializeFailFast(
    uint32_t majorMinorVersion = WINDOWSAPPSDK_RELEASE_MAJORMINOR,
    PCWSTR versionTag = WINDOWSAPPSDK_RELEASE_VERSION_TAG_W,
    PackageVersion minVersion = WINDOWSAPPSDK_RUNTIME_VERSION_UINT64)
```

### Parameters
`majorMinorVersion`
See *majorMinorVersion* in [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize). Defaults to WINDOWSAPPSDK_RELEASE_MAJORMINOR.

`versionTag`
See *versionTag* in [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize). Defaults to WINDOWSAPPSDK_RELEASE_VERSION_TAG_W.

`minVersion`
See *minVersion* in [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize). Defaults to WINDOWSAPPSDK_RUNTIME_VERSION_UINT64.

### Return value 

On success, returns a resource acquisition is initialization (RAII) object which, when it goes out of scope, undoes the changes that were made by the call to [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize).

### Example

```cpp
#include <windows.h>

#include <WindowsAppSDK-VersionInfo.h>
#include <MddBootstrap.h>

#include <iostream>

namespace MddBootstrap {using namespace
    ::Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap; }

int main()
{
    auto mddBootstrapShutdown{ MddBootstrap::InitializeFailFast() };
    // Do work here.
    return 0;
}
```

## Requirements
**Minimum supported SDK:** Windows App SDK version 1.1

**Namespace:** Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap

**Header:** MddBootstrap.h

## See also

* [Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap namespace](microsoft.windows.applicationmodel.dynamicdependency.bootstrap.initializefailfast.md)
* [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize)
