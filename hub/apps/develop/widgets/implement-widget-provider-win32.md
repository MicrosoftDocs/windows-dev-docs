---
title: Implement a widget provider in a win32 app
description: This article walks you through the process of creating a widget provider that provides widget content and responds to widget actions. 
ms.topic: article
ms.date: 07/06/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Implement a widget provider in a win32 app

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**
> [!IMPORTANT]
> The self-contained feature described in this topic is available only in Windows App SDK 1.2 Preview 1.

## Prerequisites

## Create a new win32 console app

In Visual Studio, create a new project. In the **Create a new project** dialog, set the language filter to "C++" and the platform filter to Windows, then select the Windows Console Application (C++/WinRT) project template. Name the new project "ExampleWidgetProvider". When prompted, set the target Windows version for the app to TBD.

## Add a reference to the Windows widgets NuGet package

TBD

This sample also uses the Windows Implementation Library NuGet package. In **Solution Explorer**, right-click **References** and select **Manage NuGet packages...**. In the NuGet package manager, select the **Browse** tab and search for "Microsoft.Windows.ImplementationLibrary". Select the latest version in the **Version** drop-down and then click **Install**.

TBD

In the precompiled header file, pch.h, add the following include directives.

```cpp
//pch.h 
#pragma once
#include <wil/cppwinrt.h>
...
#include <winrt/Microsoft.Windows.Widgets.Providers.h>
```

> [!NOTE]
> You must include the wil/cppwinrt.h header first, before any WinRT headers.

## Add a WidgetProvider class to handle widget operations

In Visual Studio, right-click the ExampleWidgetProvider project in **Solution Explorer** and select **Add->Class**. In the **Add class** dialog, name the class "WidgetProvider" and click **OK**.



## Declare a class that implements the IWidgetProvider interface

The **IWidgetProvider** interface defines methods that the widget host will use to initiate operations with the Widget provider. Replace the empty class definition in the WidgetProvider.h file with the following code. This declares a struct that implements the **IWidgetProvider** interface and declares prototypes for the interface methods.

```cpp
// WidgetProvider.h
struct WidgetProvider : winrt::implements<WidgetProvider, winrt::Microsoft::Windows::Widgets::Providers::IWidgetProvider>
{
    WidgetProvider();

    /* IWidgetProvider required functions that need to be implemented */
    void CreateWidget(winrt::Microsoft::Windows::Widgets::Providers::WidgetContext WidgetContext);
    void DeleteWidget(winrt::hstring const& widgetId);
    void OnActionInvoked(winrt::Microsoft::Windows::Widgets::Providers::WidgetActionInvokedArgs actionInvokedArgs);
    void OnWidgetContextChanged(winrt::Microsoft::Windows::Widgets::Providers::WidgetContextChangedArgs contextChangedArgs);
    /* IWidgetProvider required functions that need to be implemented */
};
```

## Prepare to track active widgets

A widget provider can support a single widget or multiple widgets. When the widget host initiates an operation with the widget provider, it passes an ID to identify the widget associated with the operation. Each widget also has a associated name and a state value that can be used to store custom data. For this example, we'll declare a simple struct to store the ID, name, and data for each active widget. Add the following definition to the WidgetProvider.h file, above the **WidgetProvider** declaration.

```cpp
// WidgetProvider.h
struct CompactWidgetInfo
{
    winrt::hstring widgetId;
    winrt::hstring widgetName;
    int customState = 0;
};
```

Inside the **WidgetProvider** declaration in WidgetProvider.h, add a member for the map that will maintain the list of active widgets, using the widget ID as the key for each entry.

```cpp
// WidgetProvider.h
struct WidgetProvider : winrt::implements<WidgetProvider, winrt::Microsoft::Windows::Widgets::Providers::IWidgetProvider>
{
...
    private:
        static std::unordered_map<winrt::hstring, CompactWidgetInfo> RunningWidgets;
```

## Implement the IWidgetProvider methods

In the next few sections, we'll implement the methods of the **IWidgetProvider** interface. There are some helper methods used in these method implementations which will be shown later in this article. Before diving into the interface methods, add the following line to `WidgetProvider.cpp`, after the include directives, to pull the widget provider APIs into the **winrt** namespace.

```cpp
// WidgetProvider.cpp
namespace winrt
{
    using namespace Microsoft::Windows::Widgets::Providers;
}
```

## CreateWidget

The widget host calls **CreateWidget** when the user has pinned one of your app's widgets to the widget host. First, this method gets the ID and name of the associated widget and adds a new item to the collection of active widgets. Next, we send the initial template and data for the widget. In this example we encapsulate this task in the **UpdateWidget** helper method.

```cpp
// WidgetProvider.cpp
void WidgetProvider::CreateWidget(winrt::WidgetContext widgetContext)
{
    auto widgetId = widgetContext.WidgetId(); // To save RPC calls
    auto widgetName = widgetContext.WidgetName();
    CompactWidgetInfo runningWidgetInfo{ widgetId, widgetName };
    RunningWidgets[widgetId] = runningWidgetInfo;

    // Call UpdateWidget() to send data/template to the newly created widget.
    winrt::WidgetUpdateRequestOptions updateOptions{ widgetId };
    // !! updateOptions.Template(GetTemplateForWidget(runningWidgetInfo));
    //updateOptions.Data(GetDataForWidget(runningWidgetInfo));
    // !!  You can store some custom state in the widget service that you will be able to query at any time.
    updateOptions.CustomState(winrt::to_hstring(runningWidgetInfo.customState));

    // Update the widget
    // !! UpdateWidget(updateOptions);
}
```

## DeleteWidget

The widget host calls **DeleteWidget** when the user has unpinned one of your app's widgets from the widget host. When this occurs, we will remove the associated widget from our list of active widgets so that we don't send any further updates for that widget.

```cpp
// WidgetProvider.cpp
void WidgetProvider::DeleteWidget(winrt::hstring const& widgetId)
{
    RunningWidgets.erase(widgetId);
}
```

## OnActionInvoked

The widget host calls **OnActionInvoked** when the user interacts with an action you defined in your widget template. The following example shows an action definition from an example widget template. Note that the **verb** feel has the value "inc". The widget provider will use this value to determine what action to take in response to the user interaction.

```json
...
    "actions": [                                                      
        {                                                               
            "type": "Action.Execute",                               
            "title": "Increment",                                   
            "verb": "inc"                                           
        }                                                               
    ], 
...
```
In the **OnActionInvoked** method, get the verb value by checking the **Verb** property of the **WidgetActionInvokedArgs** passed into the method. If the verb is "inc", then we know we are going to increment the count in the custom state for the widget. From the **WidgetActionInvokedArgs**, get the **WidgetContext** object and then the **WidgetId** to get the ID for the widget that is being updated. Find the entry in our active widgets map with the specified ID and then update the increment the custom state value. Finally, update the widget content with the new value with the **UpdateWidget** helper function.

```cpp
// WidgetProvider.cpp
void WidgetProvider::OnActionInvoked(winrt::WidgetActionInvokedArgs actionInvokedArgs)
{
    auto verb = actionInvokedArgs.Verb();
    if (verb == L"inc")
    {
        auto widgetId = actionInvokedArgs.WidgetContext().WidgetId();
        // If you need to use some data that was passed in after
        // Action was invoked, you can get it from the args:
        auto data = actionInvokedArgs.Data();
        if (const auto iter = RunningWidgets.find(widgetId); iter != RunningWidgets.end())
        {
            auto& localWidgetInfo = iter->second;
            // Increment the count
            localWidgetInfo.customState++;

            // Generate template/data you want to send back
            // !! auto widgetTemplate = GetTemplateForWidget(localWidgetInfo);
            // !! auto widgetData = GetDataForWidget(localWidgetInfo);

            // Build update options and update the widget
            winrt::WidgetUpdateRequestOptions updateOptions{ widgetId };
            // !! updateOptions.Template(widgetTemplate);
            // !! updateOptions.Data(widgetData);
            updateOptions.CustomState(winrt::to_hstring(localWidgetInfo.customState));

            // !! UpdateWidget(updateOptions);
        }
    }
}
```


For information about the **Action.Execute** syntax for Adaptive Cards, see [Action.Execute](https://adaptivecards.io/explorer/Action.Execute.html). For guidance about designing interaction for widgets, see [Widget interaction design guidance](/windows/apps/design/widgets/widgets-interaction-design)


## OnWidgetContextChanged

In the current release, **OnWidgetContextChanged** is only called when the user changes the size of a pinned widget.

```cpp
// WidgetProvider.cpp
void WidgetProvider::OnWidgetContextChanged(winrt::WidgetContextChangedArgs contextChangedArgs)
{
    auto widgetContext = contextChangedArgs.WidgetContext();
    auto widgetId = widgetContext.WidgetId();
    auto widgetSize = widgetContext.WidgetSize();
    if (const auto iter = RunningWidgets.find(widgetId); iter != RunningWidgets.end())
    {
        auto localWidgetInfo = iter->second;

        // Generate template/data you want to send back
        // !! auto widgetTemplate = GetTemplateForWidget(localWidgetInfo);
        // !! auto widgetData = GetDataForWidget(localWidgetInfo);

        // We could updat template or data to account for the new size.
        // Leaving that as exercise for the provider author :)

        // Build update options and update the widget
        winrt::WidgetUpdateRequestOptions updateOptions{ widgetId };
        // !! updateOptions.Template(widgetTemplate);
        // !! updateOptions.Data(widgetData);
        updateOptions.CustomState(winrt::to_hstring(localWidgetInfo.customState));

        // !! UpdateWidget(updateOptions);
    
```

## Update a widget

Create the **UpdateWidget** helper method to update an active widget.  Call **WidgetManager::GetDefault** to get the default widget manager instance for the app. Then call **UpdateWidget**, passing in a **WidgetUpdateRequestOptions** object that identifies the widget to be updated and provides the template and data information for the widget.

```cpp
// WidgetProvider.cpp
void WidgetProvider::UpdateWidget(winrt::Microsoft::Windows::Widgets::Providers::WidgetUpdateRequestOptions const& updateRequestArgs)
{
    winrt::WidgetManager::GetDefault().UpdateWidget(updateRequestArgs);
}
```

## Initialize the list of active widgets on startup

When our widget provider is first initialized, we need to populate our list of active widgets. Call **WidgetManager::GetDefault** to get the default widget manager instance for the app. Then call **GetWidgetInfos**, which returns an array of **WidgetInfo** objects. Copy the widget IDs, names, and custom state into the helper struct **CompactWidgetInfo** and save it to the **RunningWidgets** member variable. For this example, paste the following code into the constructor for the **WidgetProvider** class.

```cpp
// WidgetProvider.cpp
WidgetProvider::WidgetProvider()
{

    for (auto widgetInfo : winrt::WidgetManager::GetDefault().GetWidgetInfos())
    {
        auto widgetContext = widgetInfo.WidgetContext();
        auto widgetId = widgetContext.WidgetId();
        auto widgetName = widgetContext.WidgetName();
        auto customState = widgetInfo.CustomState();
        if (RunningWidgets.find(widgetId) == RunningWidgets.end())
        {
            CompactWidgetInfo runningWidgetInfo{ widgetName, widgetId };
            try
            {
                // If we had any save state (in this case we might have some state saved for Counting widget)
                // convert string to required type if needed.
                int count = std::stoi(winrt::to_string(customState));
                runningWidgetInfo.customState = count;
            }
            catch (...)
            {

            }
            RunningWidgets[widgetId] = runningWidgetInfo;
        }
    }
}
```

## Instantiate the WidgetProvider class from main

AAdd the header that defines the WidgetProvider class to the includes at the top of your app's `main.cpp` file.

```cpp
// main.cpp
...
#include "WidgetProvider.h"
```

Next, you will need to create a [CLSID](/windows/win32/com/com-class-objects-and-clsids) that will be used to identify your widget provider for COM activation. Generate a GUID in Visual Studio by going to **Tools->Create GUID**. Select the option "static const GUID =" and click **Copy** and then paste that into `main.cpp`. Update the GUID definition with the following C++/WinRT syntax, setting the GUID variable name SAMPLE_PROVIDER_CLSID. Leave the commented version of the GUID because you will need this format later, when packaging your app.

```cpp
// main.cpp
...
// {80F4CB41-5758-4493-9180-4FB8D480E3F5}
constexpr winrt::guid SAMPLE_PROVIDER_CLSID =
{ 0x80f4cb41, 0x5758, 0x4493, { 0x91, 0x80, 0x4f, 0xb8, 0xd4, 0x80, 0xe3, 0xf5 } };
```

Add the following class factory definition to `main.cpp`. This is boilerplate code that is not specific to widget provider implementations.

```cpp
// main.cpp
template<typename T>
struct ClassFactory : public winrt::implements<ClassFactory<T>, IClassFactory>
{
    STDMETHODIMP CreateInstance(
        ::IUnknown* outer,
        GUID const& iid,
        void** result) noexcept final
    {
        *result = nullptr;

        if (outer)
        {
            return CLASS_E_NOAGGREGATION;
        }

        if (!instance)
        {
            instance = winrt::make_self<T>();
        }

        return instance->QueryInterface(iid, result);
    }

    STDMETHODIMP LockServer(BOOL) noexcept final
    {
        return S_OK;
    }

private:
    winrt::com_ptr<T> instance{ nullptr };
};
```


## Package your widget provider app

In the current release, only packaged apps can be registered as a widget providers. The following steps will take you through the process of packaging your app and updating the app manifest to register your app with the OS as a widget provider.

### Create an MSIX packaging project 

In **Solution Explorer**, right-click your solution and select **Add->New Project...**. In the **Add a new project** dialog, select the "Windows Application Packaging Project" template and click **Next**. Set the project name to "ExampleWidgetProviderPackage" and click **Create**. When prompted, set the target version to TBD and click **OK**.
Next, right-click the ExampleWidgetProviderPackage project and select **Add->Project reference**. Select the **ExampleWidgetProvider** project and click O

### Update the package manifest

In **Solution Explorer** right-click the `Package.appmanifest` file and select **View Code** to open the manifest xml file. Next you need to add some namespace declarations for the app package extensions we will be using. Add the following namespace definitions to the top-level [Package](/uwp/schemas/appxpackage/uapmanifestschema/element-package) element.

```xml
<!-- Package.appmanifest -->
<Package
  ...
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
```


Inside the **Application** element, create a new empty element named **Extensions**.

```xml
<!-- Package.appmanifest -->
<Application>
...
    <Extensions>

    </Extensions>
</Application>
```

The first extension we need to add is the [ComServer](/uwp/schemas/appxpackage/uapmanifestschema/element-com-comserver) extension. This registers the entry point of the executable with the OS. This extension is the packaged app equivalent of registering a COM server by setting a registry key, and is not specific to widget providers.Add the following **com:Extension** element as a child of the **Extensions** element. Change the GUID in the **Id** attribute of the **com:Class** element to the GUID you generated in a previous step.

```xml
<!-- Package.appmanifest -->
<Extensions>
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:ExeServer Executable="ExampleWidgetProvider\ExampleWidgetProvider.exe" DisplayName="ExampleWidgetProvider">
                <com:Class Id="80F4CB41-5758-4493-9180-4FB8D480E3F5" DisplayName="ExampleWidgetProvider" />
            </com:ExeServer>
        </com:ComServer>
    </com:Extension>
</Extensions>
```

Next, add the extension that registers the app as a widget provider. Paste the [uap3:Extension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-extension-manual) element in the following code snippet, as a child of the **Extensions** element. Be sure to replace the **ClassId** attribute of the **COM** element with the GUID you used in previous steps.

```xml
<!-- Package.appmanifest -->
<Extensions>
    ...
    <uap3:Extension Category="windows.appExtension">
        <uap3:AppExtension Name="com.microsoft.windows.widgets" DisplayName="WidgetProviderSample" Id="ContosoWidgetApp" PublicFolder="Public">
            <uap3:Properties>
                <WidgetProvider Icon="Images\StoreLogo.png">
                    <Activation>
                        <!-- Apps exports COM interface which implements IWidgetProvider -->
                        <COM ClassId="80F4CB41-5758-4493-9180-4FB8D480E3F5" />
                    </Activation>
                    <Widgets>
                        <Widget Name="Weather_Widget"
                                DisplayTitle="Microsoft Weather Widget"
                                Description="Weather Widget Description"
                                AllowMultiple="True">
                            <WidgetCapabilities>
                                <WidgetCapability WidgetSize="Small" />
                                <WidgetCapability WidgetSize="Medium" />
                                <WidgetCapability WidgetSize="Large" />
                            </WidgetCapabilities>
                            <WidgetThemeResources>
                                <Icon Source="ProviderAssets\Weather_Icon.png" />
                                <Screenshots>
                                    <Screenshot Source="ProviderAssets\Weather_Screenshot.png" DisplayLabel="For accessibility" />
                                </Screenshots>
                                <!-- DarkMode and LightMode are optional -->
                                <DarkMode />
                                <LightMode />
                            </WidgetThemeResources>
                        </Widget>
                        <Widget Name="Counting_Widget"
                            DisplayTitle="Microsoft Counting Widget"
                            Description="Couting Widget Description">
                        <WidgetCapabilities>
                            <WidgetCapability WidgetSize="Small" />
                        </WidgetCapabilities>
                        <WidgetThemeResources>
                            <Icon Source="ProviderAssets\Counting_Icon.png" />
                            <Screenshots>
                                <Screenshot Source="ProviderAssets\Counting_Screenshot.png" DisplayLabel="For accessibility" />
                            </Screenshots>
                        </WidgetThemeResources>
                    </Widget>
                </Widgets>
            </WidgetProvider>
        </uap3:Properties>
    </uap3:AppExtension>
<Extensions>
```

There is a lot of xml in this example. The following section will call out some high-level concepts about the format. For detailed descriptions and format information for all of these elements, see [widget-provider-manifest.md](Widget provider package manifest XML format).

- The **uap3:Extension** and [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual) element represents a general extensibility for apps. It's not specific to widget providers. The [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/elemnt-uap3-properties-manual) element doesn't make any restrictions on it's child content, other than requiring well-formed xml. This means that all descendent elements are not validated by the app packaging mechanism and are only validated by the widget host.
- The **Activation** element is used by the widget host to launch your app. The **ClassId** you specify must match the GUID you specified in the **ComServer** extension.
- The **WidgetProvider** element contains one **Widget** element for each widget your provider supports.
- All image URLs are package-relative. (TBD image of "ProviderAssets" folder in VS)

## Testing your widget provider













