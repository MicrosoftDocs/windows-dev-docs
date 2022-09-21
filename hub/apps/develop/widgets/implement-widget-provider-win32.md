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

This article walks you through creating a simple widget provider that implements the **IWidgetProvider** interface. The methods of this interface are invoked by the widget host to request the data that defines a widget or to let the widget provider respond to a user action on a widget. Widget providers can support a single widget or multiple widgets. In this example, we will define two different widgets. One widget is a mock weather widget (TBD) that illustrates some of the formatting options provided by the Adaptive Cards framework. The second widget will demonstrate user actions and the custom widget state feature by maintaining a counter that is incremented whenever the user clicks on a button displayed on the widget.

TBD - Screenshots of the two widgets

This sample code in this article is adapted from the Windows App SDK Sample LINK TBD.

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

In Visual Studio, right-click the `ExampleWidgetProvider` project in **Solution Explorer** and select **Add->Class**. In the **Add class** dialog, name the class "WidgetProvider" and click **OK**.



## Declare a class that implements the IWidgetProvider interface

The **IWidgetProvider** interface defines methods that the widget host will invoke to initiate operations with the widget provider. Replace the empty class definition in the WidgetProvider.h file with the following code. This code declares a struct that implements the **IWidgetProvider** interface and declares prototypes for the interface methods. 

```cpp
// WidgetProvider.h
struct WidgetProvider : winrt::implements<WidgetProvider, winrt::Microsoft::Windows::Widgets::Providers::IWidgetProvider>
{
    WidgetProvider();

    /* IWidgetProvider required functions that need to be implemented */
    void CreateWidget(winrt::Microsoft::Windows::Widgets::Providers::WidgetContext WidgetContext);
    void DeleteWidget(winrt::hstring const& widgetId, winrt::hstring const& customState);
    void OnActionInvoked(winrt::Microsoft::Windows::Widgets::Providers::WidgetActionInvokedArgs actionInvokedArgs);
    void OnWidgetContextChanged(winrt::Microsoft::Windows::Widgets::Providers::WidgetContextChangedArgs contextChangedArgs);
    void Activate(winrt::Microsoft::Windows::Widgets::Providers::WidgetContext widgetContext);
    void Deactivate(winrt::hstring widgetId);
    /* IWidgetProvider required functions that need to be implemented */

    
};
```

Also, add a private method, **UpdateWidget**, which is a helper method that will send updates from our provider to the widget host.

```cpp
// WidgetProvider.h
private: 

void UpdateWidget(CompactWidgetInfo const& localWidgetInfo);
```

## Prepare to track enabled widgets

A widget provider can support a single widget or multiple widgets. Whenever the widget host initiates an operation with the widget provider, it passes an ID to identify the widget associated with the operation. Each widget also has a associated name and a state value that can be used to store custom data. For this example, we'll declare a simple helper structure to store the ID, name, and data for each pinned widget. Widgets also can be in an active state, which is discussed in the [Activate and Deactivate](#activate-and-deactivate) section below, and we will track this state for each widget with a boolean value. Add the following definition to the WidgetProvider.h file, above the **WidgetProvider** declaration.

```cpp
// WidgetProvider.h
struct CompactWidgetInfo
{
    winrt::hstring widgetId;
    winrt::hstring widgetName;
    int customState = 0;
    bool isActive = false;
};
```

Inside the **WidgetProvider** declaration in WidgetProvider.h, add a member for the map that will maintain the list of enabled widgets, using the widget ID as the key for each entry. 

```cpp
// WidgetProvider.h
struct WidgetProvider : winrt::implements<WidgetProvider, winrt::Microsoft::Windows::Widgets::Providers::IWidgetProvider>
{
...
    private:
        ...
        static std::unordered_map<winrt::hstring, CompactWidgetInfo> RunningWidgets;

        
```

## Declare widget template JSON strings

This example will declare some static strings to define the JSON templates for each widget. These variables should be declared outside of the **WidgetProvider** class definition. For more information on the widget template JSON format, see TBD.

```cpp
// WidgetProvider.h
const std::string weatherWidgetTemplate = R"(
{
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "type": "AdaptiveCard",
    "version": "1.0",
    "speak": "<s>The forecast for Seattle January 20 is mostly clear with a High of 51 degrees and Low of 40 degrees</s>",
    "backgroundImage": "https://messagecardplayground.azurewebsites.net/assets/Mostly%20Cloudy-Background.jpg",
    "body": [
        {
            "type": "TextBlock",
            "text": "Redmond, WA",
            "size": "large",
            "isSubtle": true,
            "wrap": true
        },
        {
            "type": "TextBlock",
            "text": "Mon, Nov 4, 2019 6:21 PM",
            "spacing": "none",
            "wrap": true
        },
        {
            "type": "ColumnSet",
            "columns": [
                {
                    "type": "Column",
                    "width": "auto",
                    "items": [
                        {
                            "type": "Image",
                            "url": "https://messagecardplayground.azurewebsites.net/assets/Mostly%20Cloudy-Square.png",
                            "size": "small",
                            "altText": "Mostly cloudy weather"
                        }
                    ]
                },
                {
                    "type": "Column",
                    "width": "auto",
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": "46",
                            "size": "extraLarge",
                            "spacing": "none",
                            "wrap": true
                        }
                    ]
                },
                {
                    "type": "Column",
                    "width": "stretch",
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": "°F",
                            "weight": "bolder",
                            "spacing": "small",
                            "wrap": true
                        }
                    ]
                },
                {
                    "type": "Column",
                    "width": "stretch",
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": "Hi 50",
                            "horizontalAlignment": "left",
                            "wrap": true
                        },
                        {
                            "type": "TextBlock",
                            "text": "Lo 41",
                            "horizontalAlignment": "left",
                            "spacing": "none",
                            "wrap": true
                        }
                    ]
                }
            ]
        }
    ]
})";

const std::string countWidgetTemplate = R"(
{                                                                     
    "type": "AdaptiveCard",                                         
    "body": [                                                         
        {                                                               
            "type": "TextBlock",                                    
            "text": "You have clicked the button ${count} times"    
        },
        {
             "text":"Rendering Only if Medium",
             "type":"TextBlock",
             "$when":"${$host.widgetSize==\"medium\"}"
        },
        {
             "text":"Rendering Only if Small",
             "type":"TextBlock",
             "$when":"${$host.widgetSize==\"small\"}"
        },
        {
         "text":"Rendering Only if Large",
         "type":"TextBlock",
         "$when":"${$host.widgetSize==\"large\"}"
        }                                                                    
    ],                                                                  
    "actions": [                                                      
        {                                                               
            "type": "Action.Execute",                               
            "title": "Increment",                                   
            "verb": "inc"                                           
        }                                                               
    ],                                                                  
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "version": "1.5"                                                
})";
```

## Implement the IWidgetProvider methods

In the next few sections, we'll implement the methods of the **IWidgetProvider** interface. The helper method **UpdateWidget** that is called in several of these method implementations which will be shown later in this article. Before diving into the interface methods, add the following line to `WidgetProvider.cpp`, after the include directives, to pull the widget provider APIs into the **winrt** namespace.

> [!NOTE]
> Objects passed into the callback methods of the **IWidgetProvider** interface are only guaranteed to be valid within the callback. You should not store references to these objects because their behavior outside of the context of the callback is undefined.

```cpp
// WidgetProvider.cpp
namespace winrt
{
    using namespace Microsoft::Windows::Widgets::Providers;
}
```

## CreateWidget

The widget host calls **CreateWidget** when the user has enabled one of your app's widgets in the widget host. First, this method gets the ID and name of the associated widget and adds a new instance of our helper structure, **CompactWidgetInfo**, to the collection of enabled widgets. Next, we send the initial template and data for the widget, which is encapsulated in the **UpdateWidget** helper method.

```cpp
// WidgetProvider.cpp
void WidgetProvider::CreateWidget(winrt::WidgetContext widgetContext)
{
    auto widgetId = widgetContext.Id(); // To save RPC calls
    auto widgetName = widgetContext.DefinitionId();
    CompactWidgetInfo runningWidgetInfo{ widgetId, widgetName };
    RunningWidgets[widgetId] = runningWidgetInfo;

    
    // Update the widget
    UpdateWidget(runningWidgetInfo);
}
```

## DeleteWidget

The widget host calls **DeleteWidget** when the user has unpinned one of your app's widgets from the widget host. When this occurs, we will remove the associated widget from our list of enabled widgets so that we don't send any further updates for that widget.

```cpp
// WidgetProvider.cpp
void WidgetProvider::DeleteWidget(winrt::hstring const& widgetId, winrt::hstring const& customState)
{
    RunningWidgets.erase(widgetId);
}
```

## OnActionInvoked

The widget host calls **OnActionInvoked** when the user interacts with an action you defined in your widget template. For the counter widget used in this example, an action was declared with a **verb** value of "inc" in the JSON template for the widget. The widget provider code will use this **verb** value to determine what action to take in response to the user interaction.  

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

In the **OnActionInvoked** method, get the verb value by checking the **Verb** property of the **WidgetActionInvokedArgs** passed into the method. If the verb is "inc", then we know we are going to increment the count in the custom state for the widget. From the **WidgetActionInvokedArgs**, get the **WidgetContext** object and then the **WidgetId** to get the ID for the widget that is being updated. Find the entry in our enabled widgets map with the specified ID and then update the increment the custom state value. Finally, update the widget content with the new value with the **UpdateWidget** helper function.

```cpp
// WidgetProvider.cpp
void WidgetProvider::OnActionInvoked(winrt::WidgetActionInvokedArgs actionInvokedArgs)
{
    auto verb = actionInvokedArgs.Verb();
    if (verb == L"inc")
    {
        auto widgetId = actionInvokedArgs.WidgetContext().Id();
        // If you need to use some data that was passed in after
        // Action was invoked, you can get it from the args:
        auto data = actionInvokedArgs.Data();
        if (const auto iter = RunningWidgets.find(widgetId); iter != RunningWidgets.end())
        {
            auto& localWidgetInfo = iter->second;
            // Increment the count
            localWidgetInfo.customState++;
            UpdateWidget(localWidgetInfo);
        }
    }
}
```


For information about the **Action.Execute** syntax for Adaptive Cards, see [Action.Execute](https://adaptivecards.io/explorer/Action.Execute.html). For guidance about designing interaction for widgets, see [Widget interaction design guidance](/windows/apps/design/widgets/widgets-interaction-design)


## OnWidgetContextChanged

In the current release, **OnWidgetContextChanged** is only called when the user changes the size of a pinned widget. You can choose to return a different JSON template to the widget host depending on what size is requested. You can also choose to include the template JSON for all supported widget sizes in a single template. If you send all sizes in a single template, you can use the **OnWidgetContextChanged** for telemetry purposes.

```cpp
// WidgetProvider.cpp
void WidgetProvider::OnWidgetContextChanged(winrt::WidgetContextChangedArgs contextChangedArgs)
{
    auto widgetContext = contextChangedArgs.WidgetContext();
    auto widgetId = widgetContext.Id();
    auto widgetSize = widgetContext.Size();
    if (const auto iter = RunningWidgets.find(widgetId); iter != RunningWidgets.end())
    {
        auto localWidgetInfo = iter->second;

        UpdateWidget(localWidgetInfo);
    }
}
    
```

## Activate and Deactivate

The **Activate** method is called to notify the widget provider that the widget host is currently interested in receiving updated content from the provider. For example, it could mean that the user is currently actively viewing the widget host. The **Deactivate** method is called to notify the widget provider that the widget host is no longer requesting content updates. These two methods define a window in which the widget host is most interested in showing the most up-to-date content. Widget providers can send updates to the widget at any time, such as in response to a push notification, but as with any background task, it's important to balance providing up-to-date content with resource concerns like battery life. 

**Activate** and **Deactivate** are called on a per-widget basis. This example tracks the active status of each widget in the **CompactWidgetInfo** helper struct. In the **Activate** method, we call the **UpdateWidget** helper method to update our widget. Note that the time window between **Activate** and **Deactivate** may be small, so it's recommended that you try to make your widget update code path as quick as possible.

```cpp
void WidgetProvider::Activate(winrt::Microsoft::Windows::Widgets::Providers::WidgetContext widgetContext)
{
    auto widgetId = widgetContext.Id();

    if (const auto iter = RunningWidgets.find(widgetId); iter != RunningWidgets.end())
    {
        auto localWidgetInfo = iter->second;
        localWidgetInfo.isActive = true;

        UpdateWidget(localWidgetInfo);
    }
}
void WidgetProvider::Deactivate(winrt::hstring widgetId)
{

    if (const auto iter = RunningWidgets.find(widgetId); iter != RunningWidgets.end())
    {
        auto localWidgetInfo = iter->second;
        localWidgetInfo.isActive = false;
    }
}
```


## Update a widget

Define the **UpdateWidget** helper method to update an enabled widget.  Call **WidgetManager::GetDefault** to get the default widget manager instance for the app. In this example, we check the name of the widget in the **CompatWidgetInfo** helper struct passed into the method, and then set the appropriate template and data JSON based on which widget is being updated. A **WidgetUpdateRequestOptions** is initialized with the template, data, and custom state for the widget being updated and then call **UpdateWidget** to send the updated widget data to the widget host.

```cpp
// WidgetProvider.cpp
void WidgetProvider::UpdateWidget(CompactWidgetInfo const& localWidgetInfo)
{
    winrt::WidgetUpdateRequestOptions updateOptions{ localWidgetInfo.widgetId };

    winrt::hstring templateJson;
    if (localWidgetInfo.widgetName == L"Weather_Widget")
    {
        templateJson = winrt::to_hstring(weatherWidgetTemplate);
    }
    else if (localWidgetInfo.widgetName == L"Counting_Widget")
    {
        templateJson = winrt::to_hstring(countWidgetTemplate);
    }

    winrt::hstring dataJson;
    if (localWidgetInfo.widgetName == L"Weather_Widget")
    {
        L"{}";
    }
    else if (localWidgetInfo.widgetName == L"Counting_Widget")
    {
        L"{ \"count\": " + winrt::to_hstring(localWidgetInfo.customState) + L" }";
    }

    updateOptions.Template(templateJson);
    updateOptions.Data(dataJson);
    // !!  You can store some custom state in the widget service that you will be able to query at any time.
    updateOptions.CustomState(winrt::to_hstring(localWidgetInfo.customState));
}
```

## Initialize the list of enabled widgets on startup

When our widget provider is first initialized, we need to populate our list of enabled widgets. Call **WidgetManager::GetDefault** to get the default widget manager instance for the app. Then call **GetWidgetInfos**, which returns an array of **WidgetInfo** objects. Copy the widget IDs, names, and custom state into the helper struct **CompactWidgetInfo** and save it to the **RunningWidgets** member variable. Paste the following code into the constructor for the **WidgetProvider** class.

```cpp
// WidgetProvider.cpp
WidgetProvider::WidgetProvider()
{

    for (auto widgetInfo : winrt::WidgetManager::GetDefault().GetWidgetInfos())
    {
        auto widgetContext = widgetInfo.WidgetContext();
        auto widgetId = widgetContext.Id();
        auto widgetName = widgetContext.DefinitionId();
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

Add the header that defines the **WidgetProvider** class to the includes at the top of your app's `main.cpp` file.

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
        <uap3:AppExtension Name="com.microsoft.windows.widgets" DisplayName="WidgetTestApp" Id="ContosoWidgetApp" PublicFolder="Public">
            <uap3:Properties>
                <WidgetProvider>
                    <ProviderIcons>
                        <Icon Path="Images\StoreLogo.png" />
                    </ProviderIcons>
                    <Activation>
                        <!-- Apps exports COM interface which implements IWidgetProvider -->
                        <CreateInstance ClassId="ECB883FD-3755-4E1C-BECA-D3397A3FF15C" />
                    </Activation>

                    <TrustedPackageFamilyNames>
                        <TrustedPackageFamilyName>Microsoft.MicrosoftEdge.Stable_8wekyb3d8bbwe</TrustedPackageFamilyName>
                    </TrustedPackageFamilyNames>

                    <Definitions>
                        <Definition Id="Weather_Widget"
                            DisplayName="Weather Widget"
                            Description="Weather Widget Description"
                            AllowMultiple="true">
                            <Capabilities>
                                <Capability>
                                    <Size Name="small" />
                                </Capability>
                                <Capability>
                                    <Size Name="medium" />
                                </Capability>
                                <Capability>
                                    <Size Name="large" />
                                </Capability>
                            </Capabilities>
                            <ThemeResources>
                                <Icons>
                                    <Icon Path="ProviderAssets\Weather_Icon.png" />
                                </Icons>
                                <Screenshots>
                                    <Screenshot Path="ProviderAssets\Weather_Screenshot.png" DisplayAltText="For accessibility" />
                                </Screenshots>
                                <!-- DarkMode and LightMode are optional -->
                                <DarkMode />
                                <LightMode />
                            </ThemeResources>
                        </Definition>
                        <Definition Id="Counting_Widget"
                                DisplayName="Microsoft Counting Widget"
                                Description="Couting Widget Description">
                            <Capabilities>
                                <Capability>
                                    <Size Name="small" />
                                </Capability>
                            </Capabilities>
                            <ThemeResources>
                                <Icons>
                                    <Icon Path="ProviderAssets\Counting_Icon.png" />
                                </Icons>
                                <Screenshots>
                                    <Screenshot Path="ProviderAssets\Counting_Screenshot.png" DisplayAltText="For accessibility" />
                                </Screenshots>
                                <!-- DarkMode and LightMode are optional -->
                                <DarkMode>

                                </DarkMode>
                                <LightMode />
                            </ThemeResources>
                        </Definition>
                    </Definitions>
                </WidgetProvider>
            </uap3:Properties>
        </uap3:AppExtension>
    </uap3:Extension>
<Extensions>
```

For detailed descriptions and format information for all of these elements, see [widget-provider-manifest.md](Widget provider package manifest XML format).

## Add icons and other images to your packaging project

TBD - Waiting for final decision about the weather widget.

## Testing your widget provider

TBD













