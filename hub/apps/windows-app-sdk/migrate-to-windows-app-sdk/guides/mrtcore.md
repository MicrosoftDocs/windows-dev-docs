---
title: MRT to MRT Core migration
description: This topic contains guidance for migrating from UWP's [Resource Management System](/windows/uwp/app-resources/resource-management-system) (also known as MRT) to the Windows App SDK's MRT Core.
ms.topic: article
ms.date: 10/01/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, MRT, MRTCore
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# MRT to MRT Core migration

This topic contains guidance for migrating from UWP's [Resource Management System](/windows/uwp/app-resources/resource-management-system) (also known as MRT) to the Windows App SDK's MRT Core.

MRT Core is a streamlined version of MRT. For more info, see [Manage resources with MRT Core](../../mrtcore/mrtcore-overview.md).

## Summary of API and/or feature differences

For ease of migration, MRT Core APIs are very similar to MRT APIs. For API reference documentation, see the [Microsoft.Windows.ApplicationModel.Resources namespace](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources).

> [!NOTE]
> Not all of the MRT APIs exist in MRT Core. But all of the APIs necessary for the basic functionality of MRT are included.

## Change namespace

In UWP, the MRT APIs are in the [**Windows.ApplicationModel.Resources.Core**](/uwp/api/windows.applicationmodel.resources.core) namespace. In the Windows App SDK, the MRT Core APIs are in the [**Microsoft.Windows.ApplicationModel.Resources**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace. So you'll need to change that namespace name in your source code (add `Microsoft.` at the start, and remove the `.Core` at the end).

> [!NOTE]
> In Windows App SDK 1.0 Preview 1 and later releases, MRT Core APIs are in the [**Microsoft.Windows.ApplicationModel.Resources**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace. In releases earlier than that, they are in the **Microsoft.ApplicationModel.Resources** namespace.

## ResourceManager class

This section applies if you're using the [**Windows.ApplicationModel.Resources.Core.ResourceManager.Current**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager.current) property in your UWP app.

```csharp
// In a UWP app
using Windows.ApplicationModel.Resources.Core;
...
var currentResourceManager = ResourceManager.Current;
```

```cppwinrt
// In a UWP app
#include <winrt/Windows.ApplicationModel.Resources.Core.h>
using namespace winrt::Windows::ApplicationModel::Resources::Core;
...
auto currentResourceManager{ ResourceManager::Current() };
```

Instead, in your Windows App SDK app, create a new [**Microsoft.Windows.ApplicationModel.Resources.ResourceManager**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager).

```csharp
// In a Windows App SDK app
using Microsoft.Windows.ApplicationModel.Resources;
...
var currentResourceManager = new ResourceManager();
```

```cppwinrt
// In a Windows App SDK app
#include <winrt/Microsoft.Windows.ApplicationModel.Resources.h>
using namespace winrt::Microsoft::Windows::ApplicationModel::Resources;
...
ResourceManager currentResourceManager;
```

## ResourceContext.GetForCurrentView, and ResourceContext.GetForViewIndependentUse

UWP's MRT [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext) class distinguishes between a **ResourceContext** for the current view, and one for view-independent use.

For the Windows App SDK's MRT Core [**ResourceContext**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcecontext) class, your app needs to determine the correct context (resource qualifier values), and the concepts of current view and view-independent use no longer apply.

* So if you're using the **ResourceContext.GetForCurrentView** API or the **ResourceContext.GetForViewIndependentUse** API, then use [**ResourceManager.CreateResourceContext**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager.createresourcecontext) instead.

## Resource qualifier values

In UWP's MRT, the resource context qualifier values are determined for the app. While in MRT Core, only the language value is populated. Your app needs to determine other values for itself. Here's an example, where it's assumed your XAML view contains an element named *layoutRoot*.

```csharp
// In a Windows App SDK app
using Microsoft.Windows.ApplicationModel.Resources;
...
var currentResourceManager = new ResourceManager();
var resourceContext = currentResourceManager.CreateResourceContext();
int scaleFactor = Convert.ToInt32(layoutRoot.XamlRoot.RasterizationScale * 100);
resourceContext.QualifierValues[KnownResourceQualifierName.Scale] = scaleFactor.ToString();
string s = resourceContext.QualifierValues[KnownResourceQualifierName.Scale];
```

```cppwinrt
// In a Windows App SDK app
#include <winrt/Microsoft.Windows.ApplicationModel.Resources.h>
using namespace winrt::Microsoft::Windows::ApplicationModel::Resources;
...
ResourceManager currentResourceManager;
auto resourceContext{ currentResourceManager.CreateResourceContext() };
auto scaleFactor{ layoutRoot().XamlRoot().RasterizationScale() * 100 };
resourceContext.QualifierValues().Insert(L"Scale", std::to_wstring((int)scaleFactor));
auto s{ resourceContext.QualifierValues().Lookup(L"Scale") };
```

## Resource qualifier value change

UWP's MRT provides the [**ResourceQualifierObservableMap.MapChanged**](/uwp/api/windows.applicationmodel.resources.core.resourcequalifierobservablemap.mapchanged) event. And this section applies if your UWP app is handling that event in order to listen to qualifier value changes.

MRT Core doesn't provide any notification mechanics around environment changes. So your Windows App SDK app needs to detect such changes on its own if you wish to update resources based on environment changes.

## An MRT Core sample app

Also see the [Load resources using MRT Core](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/ResourceManagement) sample app project, which demonstrates how to use the MRT Core API surface.

## Related Topics

* [Windows App SDK and suppported Windows releases](../../support.md)
* [Manage resources with MRT Core](../../mrtcore/mrtcore-overview.md)
* [MRT Core images](https://github.com/MicrosoftDocs/windows-uwp/tree/docs/hub/apps/windows-app-sdk/mrtcore/images)
* [Resource Management System](/windows/uwp/app-resources/resource-management-system)
* [Windows App SDK](../../index.md)