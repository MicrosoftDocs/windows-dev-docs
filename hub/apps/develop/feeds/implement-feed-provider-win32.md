---
title: Implement a feed provider in a Win32 app (C++/WinRT)
description: This article walks you through the process of creating a feed provider, implemented in WinRT/C++, that registers a feed content URI and responds to requests from the Widgets Board. 
ms.topic: article
ms.date: 11/02/2023
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Implement a feed provider in a win32 app (C++/WinRT)

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This article walks you through creating a simple feed provider that registers a feed content URI and implements the **IFeedProvider** interface. The methods of this interface are invoked by the Widgets Board to request custom query string parameters, typically to support authentication scenarios. Feed providers can support a single feed or multiple feeds.

To implement a feed provider using C++/WinRT, see [Implement a feed provider in a C# Windows app (C++/WinRT)](implement-feed-provider-cs.md).

## Prerequisites

- Your device must have developer mode enabled. For more information see [Enable your device for development](/windows/apps/get-started/enable-your-device-for-development).
- Visual Studio 2022 or later with the **Universal Windows Platform development** workload. Make sure to add the component for C++ (v143) from the optional dropdown.

## Create a new C++/WinRT win32 console app

In Visual Studio, create a new project. In the **Create a new project** dialog, set the language filter to "C++" and the platform filter to Windows, then select the Windows Console Application (C++/WinRT) project template. Name the new project "ExampleFeedProvider". For this walkthrough, make sure that **Place solution and project in the same directory** is unchecked. When prompted, set the target Windows version for the app to 10.022631.2787 or later.

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

The **IFeedProvider** interface defines methods that the Widgets Board will invoke to initiate operations with the feed provider. Replace the empty class definition in the FeedProvider.h file with the following code. This code declares a structure that implements the **IFeedProvider** interface and declares prototypes for the interface methods. 

```cpp
// FeedProvider.h
#pragma once
struct FeedProvider : winrt::implements<FeedProvider, winrt::Microsoft::Windows::Widgets::Feeds::Providers::IFeedProvider>
{
    FeedProvider() {}

    /* IFeedrovider required functions that need to be implemented */
    void OnFeedProviderEnabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedProviderEnabledArgs args);
    void OnFeedProviderDisabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedProviderDisabledArgs args);
    void OnFeedEnabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedEnabledArgs args);
    void OnFeedDisabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedDisabledArgs args);
    void OnCustomQueryParametersRequested(winrt::Microsoft::Windows::Widgets::Feeds::Providers::CustomQueryParametersRequestedArgs args);
    /* IFeedProvider required functions that need to be implemented */

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
    using namespace Microsoft::Windows::Widgets::Feeds::Providers;
}

```

## OnFeedProviderEnabled

The **OnFeedProviderEnabled** method is invoked when a feed associated with the provider is created by the Widgets Board host. In the implementation of this method, generate a query string with the parameters that will be passed to the URL that provides the feed content, including any necessary authentication tokens. Create an instance of **CustomQueryParametersUpdateOptions**, passing in the **FeedProviderDefinitionId** from the event args that identifies the feed that has been enabled and the query string. Get the default **FeedManager** and call **SetCustomQueryParameters** to register the query string parameters with the Widgets Board.

```csharp
// FeedProvider.cs
void FeedProvider::OnFeedProviderEnabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedProviderEnabledArgs args)
{
    std::wstringstream wstringstream;
wstringstream << args.FeedProviderDefinitionId().c_str() << L" feed provider was enabled." << std::endl;
    _putws(wstringstream.str().c_str());

    auto updateOptions = winrt::CustomQueryParametersUpdateOptions(args.FeedProviderDefinitionId(), L"param1&param2");
    winrt::FeedManager::GetDefault().SetCustomQueryParameters(updateOptions);
}

```

## OnFeedProviderDisabled

**OnFeedProviderDisabled** is called when the Widgets Board when all of the feeds for this provider have been disabled. Feed providers are not required to perform any actions in response to these this method call. The method invocation can be used for telemetry purposes or to update the query string parameters or revoke authentication tokens, if needed. If the app only supports a single feed provider or if all feed providers supported by the app have been disabled, then the app can exit in response to this callback.

```csharp
// FeedProvider.cs

void FeedProvider::OnFeedProviderDisabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedProviderDisabledArgs args)
{
    std::wstringstream wstringstream;
    wstringstream << args.FeedProviderDefinitionId().c_str() << L" feed provider was disabled." << std::endl;
    _putws(wstringstream.str().c_str());
}
```

## OnFeedEnabled, OnFeedDisabled

**OnFeedEnabled** and **OnFeedDisabled** are invoked by the Widgets Board when a feed is enabled or disabled. Feed providers are not required to perform any actions in response to these method calls. The method invocation can be used for telemetry purposes or to update the query string parameters or revoke authentication tokens, if needed.

```csharp
// FeedProvider.cs

void FeedProvider::OnFeedEnabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedEnabledArgs args)
{
    std::wstringstream wstringstream;
    wstringstream << args.FeedDefinitionId().c_str() << L" feed was enabled." << std::endl;
    _putws(wstringstream.str().c_str());
}
```

```csharp
// FeedProvider.cs

void FeedProvider::OnFeedDisabled(winrt::Microsoft::Windows::Widgets::Feeds::Providers::FeedDisabledArgs args)
{
    std::wstringstream wstringstream;
    wstringstream << args.FeedDefinitionId().c_str() << L" feed was disabled." << std::endl;
    _putws(wstringstream.str().c_str());
}
```

## OnCustomQueryParametersRequested

**OnCustomQueryParametersRequested** is raised when the Widgets Board determines that the custom query parameters associated with the feed provider need to be refreshed. For example, this method may be raised if the operation to fetch feed content from the remote web service fails. The **FeedProviderDefinitionId** property of the **CustomQueryParametersRequestedArgs** passed into this method specifies the feed for which query string params are being requested. The provider should regenerate the query string and pass it back to the Widgets Board by calling **SetCustomQueryParameters**.

```csharp
// FeedProvider.cs

void FeedProvider::OnCustomQueryParametersRequested(winrt::Microsoft::Windows::Widgets::Feeds::Providers::CustomQueryParametersRequestedArgs args)
{
    std::wstringstream wstringstream;
    wstringstream << L"CustomQueryParameters were requested for " << args.FeedProviderDefinitionId().c_str() << std::endl;
    _putws(wstringstream.str().c_str());

    auto updateOptions = winrt::CustomQueryParametersUpdateOptions(args.FeedProviderDefinitionId(), L"param1&param2");
    winrt::FeedManager::GetDefault().SetCustomQueryParameters(updateOptions);
}
```

## Register a class factory that will instantiate FeedProvider on request

Add the header that defines the **FeedProvider** class to the includes at the top of your app's `main.cpp` file. We will also be including **mutex** here.

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

Next, you will need to create a [CLSID](/windows/win32/com/com-class-objects-and-clsids) that will be used to identify your feed provider for COM activation. Generate a GUID in Visual Studio by going to **Tools->Create GUID**. Select the option "static const GUID =" and click **Copy** and then paste that into `main.cpp`. Update the GUID definition with the following C++/WinRT syntax, setting the GUID variable name feed_provider_clsid. Leave the commented version of the GUID because you will need this format later, when packaging your app.

```cpp
// main.cpp
...
// {80F4CB41-5758-4493-9180-4FB8D480E3F5}
static constexpr GUID feed_provider_clsid
{
    0x80f4cb41, 0x5758, 0x4493, { 0x91, 0x80, 0x4f, 0xb8, 0xd4, 0x80, 0xe3, 0xf5 }
};
```

Add the following class factory definition to `main.cpp`. This is mostly boilerplate code that is not specific to feed provider implementations. Note that **CoWaitForMultipleObjects** waits for our shutdown event to be triggered before the app exits.

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
    auto factory = winrt::make<SingletonClassFactory<winrt::Microsoft::Windows::Widgets::Feeds::Providers::IFeedProvider>>();

    winrt::check_hresult(CoRegisterClassObject(
        feed_provider_clsid,
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

You need to add a reference to the Windows App SDK nuget package to the MSIX packaging project. In **Solution Explorer**, double-click the ExampleFeedProviderPackage project to open the ExampleFeedProviderPackage.wapproj file. Add the following xml inside the **Project** element.

```xml
<!--ExampleFeedProviderPackage.wapproj-->
<ItemGroup>
    <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.5.231116003-experimentalpr">
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
    <PackageDependency Name="Microsoft.WindowsAppRuntime.1.5.233430000-experimental1" MinVersion="2000.638.7.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />
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
            <com:ExeServer Executable="ExampleFeedProvider\ExampleFeedProvider.exe" Arguments="-RegisterProcessAsComServer" DisplayName="C++ Feed Provider App">
                <com:Class Id="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" DisplayName="FeedProvider" />
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
        <uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoFeed" Id="com.examplewidgets.examplefeed" PublicFolder="Public">
            <uap3:Properties>
                <FeedProvider Icon="ms-appx:Assets\StoreLogo.png" Description="FeedDescription">
                    <Activation>
                        <!-- Apps exports COM interface which implements IFeedProvider -->
                        <CreateInstance ClassId="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" />
                    </Activation>
                    <Definitions>
                        <Definition Id="Contoso_Feed"
                            DisplayName="Contoso_Feed Feed"
                            Description="Feed representing Contoso"
                            ContentUri="https://www.contoso.com/"
                            Icon="ms-appx:Images\StoreLogo.png">
                        </Definition>
                        <Definition Id="Fabrikam_Feed"
                            DisplayName="Fabrikam Feed"
                            Description="Feed representing Example"
                            ContentUri="https://www.fabrikam.com/"
                            Icon="ms-appx:Images\StoreLogo.png">
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

In **Solution Explorer**, right-click your **ExampleFeedProviderPackage** and select **Add->New Folder**. Name this folder ProviderAssets as this is what was used in the `Package.appxmanifest` from the previous step. This is where we will store our **Icon** for our feeds. Once you add your desired Icons , make sure the image names match what comes after **Path=ProviderAssets\\** in your `Package.appxmanifest` or the feeds will not show up in the Widget Board.



## Testing your feed provider

Make sure you have selected the architecture that matches your development machine from the **Solution Platforms** drop-down, for example "x64". In **Solution Explorer**, right-click your solution and select **Build Solution**.  Once this is done, right-click your **ExampleWidgetProviderPackage** and select **Deploy**. The console app should launch on deploy and you will see the feeds get enabled in the console output. Open the Widgets Board and you should see the new feeds in the tabs along the top of the feeds section.

## Debugging your feed provider

After you have pinned your feeds, the Widget Platform will start your feed provider application in order to receive and send relevant information about the feed. To debug the running feed you can either attach a debugger to the running feed provider application or you can set up Visual Studio to automatically start debugging the feed provider process once it's started.

In order to attach to the running process:

1. In Visual Studio click **Debug -> Attach to process**.
1. Filter the processes and find your desired feed provider application.
1. Attach the debugger.

In order to automatically attach the debugger to the process when it's initially started:

1. In Visual Studio click **Debug -> Other Debug Targets -> Debug Installed App Package**.
1. Filter the packages and find your desired feed provider package.
1. Select it and check the box that says Do not launch, but debug my code when it starts.
1. Click **Attach**.

## Convert your console app to a Windows app

To convert the console app created in this walkthrough to a Windows app:
1. Right-click on the ExampleWidgetProvider project in **Solution Explorer** and select **Properties**. Navigate to **Linker -> System** and change **SubSystem** from "Console" to "Windows". This can also be done by adding &lt;SubSystem&gt;Windows&lt;/SubSystem&gt; to the &lt;Link&gt;..&lt;/Link&gt; section of the .vcxproj.
1. In main.cpp, change `int main()` to `int WINAPI wWinMain(_In_ HINSTANCE /*hInstance*/, _In_opt_ HINSTANCE /*hPrevInstance*/, _In_ PWSTR pCmdLine, _In_ int /*nCmdShow*/)`.

:::image type="content" source="images/convert-to-windows-app-cs.png" alt-text="A screenshot showing the C++ feed provider project properties with the output type set to Windows Application":::

## Publishing your feed provider app

After you have developed and tested your feed provider you can publish your app on the Microsoft Store in order for users to install your feeds on their devices. For step by step guidance for publishing an app, see [Publish your app in the Microsoft Store](/windows/apps/publish/publish-your-app/overview?pivots=store-installer-msix).

