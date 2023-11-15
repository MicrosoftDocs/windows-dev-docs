---
title: Implement a feed provider in a win32 app (C++/WinRT)
description: This article walks you through the process of creating a feed provider, implemented in WinRT/C++, that registers a feed content URI and responds to requests from the Widgets Board. 
ms.topic: article
ms.date: 11/02/2023
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Implement a feed provider in a win32 app (C++/WinRT)

This article walks you through creating a simple feed provider that registers a feed content URI and implements the [IFeedProvider](TBD) interface. The methods of this interface are invoked by the Widgets Board to request custom query string parameters, typically to support authentication scenarios. Feed providers can support a single feed or multiple feeds.


This sample code in this article is adapted from the TBD - sample URL [Windows App SDK Feeds Sample](). To implement a feed provider using C++/WinRT, see [Implement a feed provider in a C# Windows app (C++/WinRT)](implement-feed-provider-cs.md).

## Prerequisites

- Your device must have developer mode enabled. For more information see [Enable your device for development](/windows/apps/get-started/enable-your-device-for-development).
- Visual Studio 2022 or later with the **Universal Windows Platform development** workload. Make sure to add the component for C++ (v143) from the optional dropdown.

## Create a new C++/WinRT win32 console app

In Visual Studio, create a new project. In the **Create a new project** dialog, set the language filter to "C++" and the platform filter to Windows, then select the Windows Console Application (C++/WinRT) project template. Name the new project "ExampleFeedProvider". When prompted, set the target Windows version for the app to version 1809 or later.

## Add references to the Windows App SDK and Windows Implementation Library NuGet packages

This sample uses the latest stable Windows App SDK NuGet package. In **Solution Explorer**, right-click **References** and select **Manage NuGet packages...**. In the NuGet package manager, select the **Browse** tab and search for "Microsoft.WindowsAppSDK". Select the latest stable version in the **Version** drop-down and then click **Install**.

This sample also uses the Windows Implementation Library NuGet package. In **Solution Explorer**, right-click **References** and select **Manage NuGet packages...**. In the NuGet package manager, select the **Browse** tab and search for "Microsoft.Windows.ImplementationLibrary". Select the latest version in the **Version** drop-down and then click **Install**.

In the precompiled header file, pch.h, add the following include directives.

```cpp
//pch.h 
#pragma once
#include <wil/cppwinrt.h>
#include <wil/resource.h>
...
#include <winrt/Microsoft.Windows.Widgets.Providers.h>
```

> [!NOTE]
> You must include the wil/cppwinrt.h header first, before any WinRT headers.

In order to handle shutting down the feed provider app correctly, we need to a custom implementation of **winrt::get_module_lock**. We pre-declare the **SignalLocalServerShutdown** method which will be defined in our main.cpp file and will set an event that signals the app to exit. Add the following code to your pch.h file, just below the `#pragma once` directive, before the other includes.

```cpp
//pch.h
#include <stdint.h>
#include <combaseapi.h>

// In .exe local servers the class object must not contribute to the module ref count, and use
// winrt::no_module_lock, the other objects must and this is the hook into the C++ WinRT ref counting system
// that enables this.
void SignalLocalServerShutdown();

namespace winrt
{
    inline auto get_module_lock() noexcept
    {
        struct service_lock
        {
            uint32_t operator++() noexcept
            {
                return ::CoAddRefServerProcess();
            }

            uint32_t operator--() noexcept
            {
                const auto ref = ::CoReleaseServerProcess();

                if (ref == 0)
                {
                    SignalLocalServerShutdown();
                }
                return ref;
            }
        };

        return service_lock{};
    }
}


#define WINRT_CUSTOM_MODULE_LOCK
```

## Add a FeedProvider class to handle feed operations

In Visual Studio, right-click the `ExampleFeedProvider` project in **Solution Explorer** and select **Add->Class**. In the **Add class** dialog, name the class "FeedProvider" and click **Add**.



## Declare a class that implements the IFeedProvider interface

The **IFeedProvider** interface defines methods that the widget host will invoke to initiate operations with the feed provider. Replace the empty class definition in the FeedProvider.h file with the following code. This code declares a structure that implements the **IFeedProvider** interface and declares prototypes for the interface methods. 

```cpp
// WidgetProvider.h
struct FeedProvider : winrt::implements<WidgetProvider, winrt::Microsoft::Windows::Widgets::Providers::IWidgetProvider>
{
    WidgetProvider();

    /* IWidgetProvider required functions that need to be implemented */
    void OnFeedProviderEnabled(winrt::Microsoft::Windows::Widgets::Feeds::FeedProviderEnabledArgs args);
    void OnFeedProviderDisabled(winrt::Microsoft::Windows::Widgets::Feeds::FeedProviderDisabledArgs args)
    void OnFeedEnabled(winrt::Microsoft::Windows::Widgets::Feeds::FeedEnabledArgs args)
    void OnFeedDisabled(winrt::Microsoft::Windows::Widgets::Feeds::FeedDisabledArgs args)
    void OnCustomQueryParametersRequested(winrt::Microsoft::Windows::Widgets::Feeds::CustomQueryParametersRequestedArgs args)
    /* IWidgetProvider required functions that need to be implemented */

    
};
```

## Implement the IFeedProvider methods

In the next few sections, we'll implement the methods of the **IFeedProvider** interface. Before diving into the interface methods, add the following lines to `FeedProvider.cpp`, after the include directives, to pull the feed provider APIs into the **winrt** namespace and allow access to the map we declared in the previous step.


> [!NOTE]
> Objects passed into the callback methods of the **IFeedProvider** interface are only guaranteed to be valid within the callback. You should not store references to these objects because their behavior outside of the context of the callback is undefined.


```cpp
// WidgetProvider.cpp
namespace winrt
{
    using namespace Microsoft::Windows::Widgets::Feeds;
}

std::unordered_map<winrt::hstring, CompactWidgetInfo> WidgetProvider::RunningWidgets{};
```

## OnFeedProviderEnabled

The **OnFeedProviderEnabled** method is invoked when a feed associated with the provider is created by the Widgets Board host. In the implementation of this method, generate a query string that includes the necessary authentication tokens for the remote web service that provides the feed content. Create an instance of **CustomQueryParametersUpdateOptions**, passing in the **FeedProviderDefinitionId** from the event args that identifies the feed that has been enabled and the query string. Get the default **FeedManager** and call **SetCustomQueryParameters** to register the query string parameters with the Widgets Board.

```csharp
// WidgetProvider.cs

void OnFeedProviderEnabled(FeedProviderEnabledArgs args)
    {
        // Get CustomQueryParams that include OAuth token
        // for newly added provider:
        hstring customizationParam = L"userOAuth=" + MyFeedProvider::GetUserOAuth(args.FeedProviderDefinitionId());
        // Update CustomQueryParams for this feed provider:
        CustomQueryParametersUpdateOptions options{args.FeedProviderDefinitionId(), customizationParam};
        FeedManager::GetDefault().SetCustomQueryParameters(options);
    }
```

## OnFeedProviderDisabled

**OnFeedProviderDisabled** is called when the Widgets Board disables the feed provider. Feed providers are not required to perform any actions in response to these this method call. The method invocation can be used for telemetry information or to revoke authentication tokens, if needed. Also, the app may choose to shutdown in response to this call if the app is not servicing other active feed providers.

```csharp
// WidgetProvider.cs

void OnFeedProviderDisabled(FeedProviderDisabledArgs args)
    {
       // This call can be used for Telemtry and/or revoked OAuth token if needed
       // as well as the signal to shutdown the app if it's not serving any other
       // feed providers.
    }
```

## OnFeedEnabled, OnFeedDisabled

**OnFeedEnabled** and **OnFeedDisabled** are invoked by the Widgets Board when a feed is enabled or disabled, or if the feed provider is disabled. Feed providers are not required to perform any actions in response to these method calls. These method invocations can be used for telemetry information or to revoke authentication tokens, if needed.

```csharp
// WidgetProvider.cs

 void OnFeedEnabled(FeedEnabledArgs args)
    {
       // Use for Telemetry and/or modification of OAuth token if needed.
    }
```

```csharp
// WidgetProvider.cs

void OnFeedDisabled(FeedDisabledArgs args)
    {
       // Use for Telemetry and/or modification of OAuth token if needed.
    }
```

## OnCustomQueryParametersRequested

**OnCustomQueryParametersRequested** is raised when the Widgets Board determines that the custom query parameters associated with the feed provider need to be refreshed. For example, this method may be raised if the operation to fetch feed content from the remote web service fails. The **FeedProviderDefinitionId** property of the **CustomQueryParametersRequestedArgs** passed into this method specifies the feed for which query string params are being request. In the implementation of this method, feed providers should validate that the current query string parameters and auth tokens are still valid and unexpired. If not, the provider should regenerate the query string and pass it back to the Widgets Board by calling **SetCustomQueryParameters**.

```csharp
// WidgetProvider.cs

void OnCustomQueryParametersRequested(CustomQueryParametersRequestedArgs args)
    {
        // This FeedProvider has been requested to update their CustomQueryParams.
        // It can happen because a content fetch has failed (for example).
        // Get new CustomQueryParams that include refreshed OAuth token
        hstring customizationParam = L"userOAuth=" + MyFeedProvider::GetUserOAuth(args.FeedProviderDefinitionId());
        // Update CustomQueryParams for this feed provider:
        CustomQueryParametersUpdateOptions options{args.FeedProviderDefinitionId(), customizationParam};
        FeedManager::GetDefault().SetCustomQueryParameters(options);
    }
```


## Initialize the list of enabled feeds on startup

[TBD - this was the "what you do in the constructor" section for widgets. Needs to be updated for feeds.]

```cpp
// WidgetProvider.cpp
MyFeeedProvider::MyFeedProvider()
    {
        // Query FeedManager for currently enabled FeedProviders that this package offers.
        auto enabledFeedProviders = FeedManager::GetDefault().GetEnabledFeedProviders();
        
        // Iterate over all enabled Feed Providers
        for (auto feedProviderInfo: enabledFeedProviders)
        {
            // For every enabled FeedProvider update CustomQueryParameters
            hstring customizationParam = L"userOAuth=" + MyFeedProvider::GetUserOAuth(args.FeedProviderDefinitionId());
            CustomQueryParametersUpdateOptions options{args.FeedProviderDefinitionId(), customizationParam};
            FeedManager::GetDefault().SetCustomQueryParameters(options); 
        }
    }
```

## Register a class factory that will instantiate FeedProvider on request

Add the header that defines the **WidgetProvider** class to the includes at the top of your app's `main.cpp` file. We will also be including **mutex** here.

```cpp
// main.cpp
...
#include "FeedProvider.h"
#include <mutex>
```

Declare the event that will trigger our app to exit and the **SignalLocalServerShutdown** function that will set the event. Paste the following code in main.cpp.

```cpp
// main.cpp
wil::unique_event g_shudownEvent(wil::EventOptions::None);

void SignalLocalServerShutdown()
{
    g_shudownEvent.SetEvent();
}
```

Next, you will need to create a [CLSID](/windows/win32/com/com-class-objects-and-clsids) that will be used to identify your feed provider for COM activation. Generate a GUID in Visual Studio by going to **Tools->Create GUID**. Select the option "static const GUID =" and click **Copy** and then paste that into `main.cpp`. Update the GUID definition with the following C++/WinRT syntax, setting the GUID variable name widget_provider_clsid. Leave the commented version of the GUID because you will need this format later, when packaging your app.

```cpp
// main.cpp
...
// {80F4CB41-5758-4493-9180-4FB8D480E3F5}
static constexpr GUID widget_provider_clsid
{
    0x80f4cb41, 0x5758, 0x4493, { 0x91, 0x80, 0x4f, 0xb8, 0xd4, 0x80, 0xe3, 0xf5 }
};
```

Add the following class factory definition to `main.cpp`. This is mostly boilerplate code that is not specific to widget provider implementations. Note that **CoWaitForMultipleObjects** waits for our shutdown event to be triggered before the app exits.

```cpp
// main.cpp
template <typename T>
struct SingletonClassFactory : winrt::implements<SingletonClassFactory<T>, IClassFactory>
{
    STDMETHODIMP CreateInstance(
        ::IUnknown* outer,
        GUID const& iid,
        void** result) noexcept final
    {
        *result = nullptr;

        std::unique_lock lock(mutex);

        if (outer)
        {
            return CLASS_E_NOAGGREGATION;
        }

        if (!instance)
        {
            instance = winrt::make<FeedProvider>();
        }

        return instance.as(iid, result);
    }

    STDMETHODIMP LockServer(BOOL) noexcept final
    {
        return S_OK;
    }

private:
    T instance{ nullptr };
    std::mutex mutex;
};

int main()
{
    winrt::init_apartment();
    wil::unique_com_class_object_cookie feedProviderFactory;
    auto factory = winrt::make<SingletonClassFactory<winrt::Microsoft::Windows::Widgets::Feeds::IFeedProvider>>();

    winrt::check_hresult(CoRegisterClassObject(
        widget_provider_clsid,
        factory.get(),
        CLSCTX_LOCAL_SERVER,
        REGCLS_MULTIPLEUSE,
        feedProviderFactory.put()));

    DWORD index{};
    HANDLE events[] = { g_shudownEvent.get() };
    winrt::check_hresult(CoWaitForMultipleObjects(CWMO_DISPATCH_CALLS | CWMO_DISPATCH_WINDOW_MESSAGES,
        INFINITE,
        static_cast<ULONG>(std::size(events)), events, &index));

    return 0;
}
```


## Package your feed provider app

In the current release, only packaged apps can be registered as feed providers. The following steps will take you through the process of packaging your app and updating the app manifest to register your app with the OS as a feed provider.

### Create an MSIX packaging project 

In **Solution Explorer**, right-click your solution and select **Add->New Project...**. In the **Add a new project** dialog, select the "Windows Application Packaging Project" template and click **Next**. Set the project name to "ExampleFeedProviderPackage" and click **Create**. When prompted, set the target version to version 1809 or later and click **OK**.
Next, right-click the ExampleFeedProviderPackage project and select **Add->Project reference**. Select the **ExampleFeedProvider** project and click OK.


### Add Windows App SDK package reference to the packaging project

You need to add a reference to the Windows App SDK nuget package to the MSIX packaging project. In **Solution Explorer**, double-click the ExampleWidgetProviderPackage project to open the ExampleFeedProviderPackage.wapproj file. Add the following xml inside the **Project** element.

```xml
<!--ExampleWidgetProviderPackage.wapproj-->
<ItemGroup>
    <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.2.221116.1">
        <IncludeAssets>build</IncludeAssets>
    </PackageReference>  
</ItemGroup>
```

> [!NOTE]
> Make sure the **Version** specified in the **PackageReference** element matches the latest stable version you referenced in the previous step.

If the correct version of the Windows App SDK is already installed on the computer and you don't want to bundle the SDK runtime in your package, you can specify the package dependency in the Package.appxmanifest file for the ExampleFeedProviderPackage project.

```xml
<!--Package.appxmanifest-->
...
<Dependencies>
...
    <PackageDependency Name="Microsoft.WindowsAppRuntime.1.2-preview2" MinVersion="2000.638.7.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />
...
</Dependencies>
...
```


### Update the package manifest

In **Solution Explorer** right-click the `Package.appxmanifest` file and select **View Code** to open the manifest xml file. Next you need to add some namespace declarations for the app package extensions we will be using. Add the following namespace definitions to the top-level [Package](/uwp/schemas/appxpackage/uapmanifestschema/element-package) element.

```xml
<!-- Package.appmanifest -->
<Package
  ...
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
```


Inside the **Application** element, create a new empty element named **Extensions**. Make sure this comes after the closing tag for **uap:VisualElements**.

```xml
<!-- Package.appxmanifest -->
<Application>
...
    <Extensions>

    </Extensions>
</Application>
```

The first extension we need to add is the [ComServer](/uwp/schemas/appxpackage/uapmanifestschema/element-com-comserver) extension. This registers the entry point of the executable with the OS. This extension is the packaged app equivalent of registering a COM server by setting a registry key, and is not specific to feed providers.Add the following **com:Extension** element as a child of the **Extensions** element. Change the GUID in the **Id** attribute of the **com:Class** element to the GUID you generated in a previous step.

```xml
<!-- Package.appxmanifest -->
<Extensions>
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:ExeServer Executable="ExampleFeedProvider\ExampleWidgetProvider.exe" DisplayName="ExampleFeedProvider">
                <com:Class Id="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" DisplayName="ExampleFeedProvider" />
            </com:ExeServer>
        </com:ComServer>
    </com:Extension>
</Extensions>
```

Next, add the extension that registers the app as a feed provider. Paste the [uap3:Extension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-extension-manual) element in the following code snippet, as a child of the **Extensions** element. Be sure to replace the **ClassId** attribute of the **COM** element with the GUID you used in previous steps.

```xml
<!-- Package.appxmanifest -->
<Extensions>
    ...
    <uap3:Extension Category="windows.appExtension">
        <uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoApp" Id="ContosoApp" PublicFolder="Public">
            <uap3:Properties>
                <FeedProvider Description="ms-resource:ProviderDescription" SettingsUri="https://contoso.com/feeds/settings" Icon="ms-appx:Images\ContosoProviderIcon.png">
                    <Activation>
                        <CreateInstance ClassId="ECB883FD-3755-4E1C-BECA-D3397A3FF15C" />
                    </Activation>
                    <Definitions>
                        <Definition Id="Contoso_Feed" DisplayName="ms-resource:FeedDisplayName" Description="ms-resource:FeedDescription"
                                    ContentUri="https://contoso.com/news"
                                    Icon="ms-appx:Images\ContosoFeedIcon.png">
                        </Definition>
                    </Definitions>
                </FeedProvider>
            </uap3:Properties>
        </uap3:AppExtension>
    </uap3:Extension>
</Extensions>
```

For detailed descriptions and format information for all of these elements, see [Feed provider package manifest XML format](feed-provider-manifest.md).

## Add icons to your packaging project

In **Solution Explorer**, right-click your **ExampleFeedProviderPackage** and select **Add->New Folder**. Name this folder ProviderAssets as this is what was used in the `Package.appxmanifest` from the previous step. This is where we will store our **Icon** for our feeds. Once you add your desired Icons , make sure the image names match what comes after **Path=ProviderAssets\\** in your `Package.appxmanifest` or the widgets will not show up in the Widget Board.



## Testing your feed provider

[TBD - most of this probably needs to change]

Make sure you have selected the architecture that matches your development machine from the **Solution Platforms** drop-down, for example "x64". In **Solution Explorer**, right-click your solution and select **Build Solution**. Once this is done, right-click your **ExampleWidgetProviderPackage** and select **Deploy**. In the current release, the only supported widget host is the Widgets Board. To see the widgets you will need to open the Widgets Board and select **Add widgets** in the top right. Scroll to the bottom of the available widgets and you should see the mock **Weather Widget** and **Microsoft Counting Widget** that were created in this tutorial. Click on the widgets to pin them to your widgets board and test their functionality.

## Debugging your feed provider

After you have pinned your widgets, the Widget Platform will start your widget provider application in order to receive and send relevant information about the widget. To debug the running widget you can either attach a debugger to the running widget provider application or you can set up Visual Studio to automatically start debugging the widget provider process once it's started.

In order to attach to the running process:

1. In Visual Studio click **Debug -> Attach to process**.
1. Filter the processes and find your desired widget provider application.
1. Attach the debugger.

In order to automatically attach the debugger to the process when it's initially started:

1. In Visual Studio click **Debug -> Other Debug Targets -> Debug Installed App Package**.
1. Filter the packages and find your desired widget provider package.
1. Select it and check the box that says Do not launch, but debug my code when it starts.
1. Click **Attach**.

## Convert your console app to a Windows app

To convert the console app created in this walkthrough to a Windows app:
1. Right-click on the ExampleWidgetProvider project in **Solution Explorer** and select **Properties**. Navigate to **Linker -> System** and change **SubSystem** from "Console" to "Windows". This can also be done by adding &lt;SubSystem&gt;Windows&lt;/SubSystem&gt; to the &lt;Link&gt;..&lt;/Link&gt; section of the .vcxproj.
1. In main.cpp, change `int main()` to `int WINAPI wWinMain(_In_ HINSTANCE /*hInstance*/, _In_opt_ HINSTANCE /*hPrevInstance*/, _In_ PWSTR pCmdLine, _In_ int /*nCmdShow*/)`.

:::image type="content" source="images/convert-to-windows-app-cpp.png" alt-text="A screenshot showing the C++ widget provider project properties with the output type set to Windows Application":::

## Publishing your feed provider app

After you have developed and tested your feed provider you must publish your app on the Microsoft Store in order for users to install your widgets on their devices. For step by step guidance for publishing an app, see [Publish your app in the Microsoft Store](/windows/apps/publish/publish-your-app/overview?pivots=store-installer-msix).

