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

MRT Core is a streamlined version of MRT. For more info, see [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview).

## API differences between MRT and MRT Core

For ease of migration, MRT Core APIs are very similar to MRT APIs. For API reference documentation, see the [Microsoft.Windows.ApplicationModel.Resources namespace](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources).

> [!NOTE]
> Not all of the MRT APIs exist in MRT Core. But all of the APIs necessary for the basic functionality of MRT are included.

## Migration guidance

## Change namespace

In UWP, the MRT APIs are in the [**Windows.ApplicationModel.Resources.Core**](/uwp/api/windows.applicationmodel.resources.core) namespace. In the Windows App SDK, the MRT Core APIs are in the [**Microsoft.Windows.ApplicationModel.Resources**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace. So you'll need to change that namespace name in your source code (add `Microsoft.` at the start, and remove the `.Core` at the end).

> [!NOTE]
> In Windows App SDK 1.0 Preview 1 and later releases, MRT Core APIs are in the [**Microsoft.Windows.ApplicationModel.Resources**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace. In releases earlier than that, they are in the **Microsoft.ApplicationModel.Resources** namespace.

### ResourceManager class

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

### ResourceContext.GetForCurrentView, and ResourceContext.GetForViewIndependentUse

UWP's MRT [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext) class distinguishes between a **ResourceContext** for the current view, and one for view-independent use.

For the Windows App SDK's MRT Core [**ResourceContext**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcecontext) class, your app needs to determine the correct context (resource qualifier values), and the concepts of current view and view-independent use no longer apply.

* So if you're using the **ResourceContext.GetForCurrentView** API or the **ResourceContext.GetForViewIndependentUse** API, then use [**ResourceManager.CreateResourceContext**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager.createresourcecontext) instead.

### Resource qualifier values

In UWP's MRT, the resource context qualifier values are determined for the app. While in MRT Core, only the language value is populated. Your app needs to determine other values for itself. Here's an example:

```csharp
// In a Windows App SDK app
using Microsoft.Windows.ApplicationModel.Resources;
...
var currentResourceManager = new ResourceManager();
var resourceContext = currentResourceManager.CreateResourceContext();
resourceContext.QualifierValues[KnownResourceQualifierName.Scale] = "200";
```

```cppwinrt
// In a Windows App SDK app
#include <microsoft.ui.xaml.window.h>
#include <shellscalingapi.h>
#include <winrt/Microsoft.Windows.ApplicationModel.Resources.h>
#include <winrt/Windows.Graphics.Display.h>
#pragma comment(lib, "Shcore")
using namespace winrt::Microsoft::Windows::ApplicationModel::Resources;
...
ResourceManager currentResourceManager;
auto resourceContext{ currentResourceManager.CreateResourceContext() };

winrt::MYPROJECT::MainWindow projectedThis{ *this };
auto windowNative{ projectedThis.as<::IWindowNative>() };
HWND hWnd{ 0 };
windowNative->get_WindowHandle(&hWnd);

HMONITOR hMonitor{ ::MonitorFromWindow(hWnd, MONITOR_DEFAULTTONEAREST) };
DEVICE_SCALE_FACTOR deviceScaleFactor{ DEVICE_SCALE_FACTOR_INVALID };
winrt::check_hresult(
    ::GetScaleFactorForMonitor(hMonitor, &deviceScaleFactor)
);

std::wstring deviceScaleFactorAsString{ L"100" };
switch (deviceScaleFactor)
{
    case SCALE_100_PERCENT:
        deviceScaleFactorAsString = L"100"; break;
    case SCALE_120_PERCENT:
        deviceScaleFactorAsString = L"120"; break;
    case SCALE_125_PERCENT:
        deviceScaleFactorAsString = L"125"; break;
    case SCALE_140_PERCENT:
        deviceScaleFactorAsString = L"140"; break;
    case SCALE_150_PERCENT:
        deviceScaleFactorAsString = L"150"; break;
    case SCALE_160_PERCENT:
        deviceScaleFactorAsString = L"160"; break;
    case SCALE_175_PERCENT:
        deviceScaleFactorAsString = L"175"; break;
    case SCALE_180_PERCENT:
        deviceScaleFactorAsString = L"180"; break;
    case SCALE_200_PERCENT:
        deviceScaleFactorAsString = L"200"; break;
    case SCALE_225_PERCENT:
        deviceScaleFactorAsString = L"225"; break;
    case SCALE_250_PERCENT:
        deviceScaleFactorAsString = L"250"; break;
    case SCALE_300_PERCENT:
        deviceScaleFactorAsString = L"300"; break;
    case SCALE_350_PERCENT:
        deviceScaleFactorAsString = L"350"; break;
    case SCALE_400_PERCENT:
        deviceScaleFactorAsString = L"400"; break;
    case SCALE_450_PERCENT:
        deviceScaleFactorAsString = L"450"; break;
    case SCALE_500_PERCENT:
        deviceScaleFactorAsString = L"500"; break;
}
        
resourceContext.QualifierValues().Insert(L"Scale", deviceScaleFactorAsString);
```

### Resource qualifier value change

UWP's MRT provides the [**ResourceQualifierObservableMap.MapChanged**](/uwp/api/windows.applicationmodel.resources.core.resourcequalifierobservablemap.mapchanged) event. And this section applies if your UWP app is handling that event in order to listen to qualifier value changes.

MRT Core doesn't provide any notification mechanics around environment changes. So your Windows App SDK app needs to detect such changes on its own if you wish to update resources based on environment changes.

## An MRT Core sample app

Also see the [Load resources using MRT Core](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/ResourceManagement) sample app project, which demonstrates how to use the MRT Core API surface.

## Related Topics

* [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview)
* [MRT Core images](https://github.com/MicrosoftDocs/windows-uwp/tree/docs/hub/apps/windows-app-sdk/mrtcore/images)
* [Resource Management System](/windows/uwp/app-resources/resource-management-system)
* [Windows App SDK](/windows/apps/windows-app-sdk/)
