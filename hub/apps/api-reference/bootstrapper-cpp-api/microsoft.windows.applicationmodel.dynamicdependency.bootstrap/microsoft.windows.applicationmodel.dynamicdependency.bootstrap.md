---
title: Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap namespace
description: Types and functions from the [Bootstrapper C++ API](api-reference/bootstrapper-cpp-api/index.md) that are in the **Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap** namespace.
ms.topic: article
ms.date: 03/22/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, app sdk, bootstrapper, bootstrapper api
ms.localizationpriority: low
---

# Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap namespace

Types and functions from the [Bootstrapper C++ API](../index.md) that are in the **Microsoft::Windows::ApplicationModel::DynamicDependency::Bootstrap** namespace. For example, helper functions that wrap calls to the [Bootstrapper API](/windows/windows-app-sdk/api/win32/_bootstrap/).

## Functions in theMicrosoft::Windows::ApplicationModel::DynamicDependency::Bootstrap namespace

| Function | Description |
| - | - |
| [Initialize function](microsoft.windows.applicationmodel.dynamicdependency.bootstrap.initialize.md) | Calls [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) to initialize the calling process to use the specified version of the Windows App SDK's framework package. If the call fails, throws an exception. |
| [InitializeFailFast function](microsoft.windows.applicationmodel.dynamicdependency.bootstrap.initializefailfast.md) | Calls [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) to initialize the calling process to use the specified version of the Windows App SDK's framework package. If the call fails, aborts the process (via **std::abort**). |
| [InitializeNoThrow function](microsoft.windows.applicationmodel.dynamicdependency.bootstrap.initializenothrow.md) | Calls [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) to initialize the calling process to use the specified version of the Windows App SDK's framework package. If the call fails, returns a failure **HRESULT**. |

## See also 

[Bootstrapper C++ API](../index.md)
