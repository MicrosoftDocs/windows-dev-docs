---
title: Implement a feed provider in a C# Windows App
description: This article walks you through the process of creating a feed provider, implemented in C#, that registers a feed content URI and responds to requests from the Widgets Board. 
ms.topic: article
ms.date: 11/02/2023
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Implement a feed provider in a C# Windows App


This article walks you through creating a simple feed provider that registers a feed content URI and implements the TBD - API ref URL - [IFeedProvider]() interface. The methods of this interface are invoked by the Widgets Board to request custom query string parameters, typically to support authentication scenarios. Feed providers can support a single feed or multiple feeds. In this example, we will...TBD

[TBD screen shot of feed]
:::image type="content" source="images/weather-widget-screenshot.png" alt-text="A screenshot of a simple weather widget. The widget shows some weather-related graphics an data as well as some diagnostic text illustrating that the template for the medium size widget is being displayed.":::


This sample code in this article is adapted from the TBD - sample URL [Windows App SDK Feeds Sample](). To implement a feed provider using C++/WinRT, see [Implement a feed provider in a win32 app (C++/WinRT)](implement-feed-provider-win32.md).

## Prerequisites

- Your device must have developer mode enabled. For more information see [Enable your device for development](/windows/apps/get-started/enable-your-device-for-development).
- Visual Studio 2022 or later with the **Universal Windows Platform development** workload. Make sure to add the component for C++ (v143) from the optional dropdown.

## Create a new C# console app

In Visual Studio, create a new project. In the **Create a new project** dialog, set the language filter to "C#" and the platform filter to Windows, then select the Console App project template. Name the new project "ExampleWidgetProvider". When prompted, set the target .NET version to 6.0. 

When the project loads, in **Solution Explorer** right-click the project name and select **Properties**. On the **General** page, scroll down to **Target OS** and select "Windows". Under **Target OS Version**, select version 10.0.19041.0 or later.

Note that this walkthrough uses a console app that displays the console window when the widget is activated to enable easy debugging. When you are ready to publish your widget provider app, you can convert the console application to a Windows application by following the steps in [Convert your console app to a Windows app](#convert-your-console-app-to-a-windows-app).

## Add references to the Windows App SDK and Windows Implementation Library NuGet packages

This sample uses the latest stable Windows App SDK NuGet package. In **Solution Explorer**, right-click **Dependencies** and select **Manage NuGet packages...**. In the NuGet package manager, select the **Browse** tab and search for "Microsoft.WindowsAppSDK". Select the latest stable version in the **Version** drop-down and then click **Install**.

## Add a WidgetProvider class to handle widget operations

In Visual Studio, right-click the `ExampleWidgetProvider` project in **Solution Explorer** and select **Add->Class**. In the **Add class** dialog, name the class "WidgetProvider" and click **Add**. In the generated WidgetProvider.cs file, update the class definition to indicate that it implements the **IWidgetProvider** interface.

```csharp
// WidgetProvider.cs
internal class WidgetProvider : IWidgetProvider
```

## Prepare to track enabled widgets

[TBD - I can't tell if you need to implement separate feeds in separate feed providers. Leaving this section here for now as a reminder]

A widget provider can support a single widget or multiple widgets. Whenever the widget host initiates an operation with the widget provider, it passes an ID to identify the widget associated with the operation. Each widget also has an associated name and a state value that can be used to store custom data. For this example, we'll declare a simple helper structure to store the ID, name, and data for each pinned widget. Widgets also can be in an active state, which is discussed in the [Activate and Deactivate](#activate-and-deactivate) section below, and we will track this state for each widget with a boolean value. Add the following definition to the WidgetProvider.cs file, inside the **ExampleWidgetProvider** namespace, but outside of the **WidgetProvider** class definition.

```csharp
// WidgetProvider.cs

public class CompactWidgetInfo
{
    public string widgetId { get; set; }
    public string widgetName { get; set; }
    public int customState = 0;
    public bool isActive = false;

}
```

Inside the **FeedProvider** class definition in FeedProvider.cs, add a member for the map that will maintain the list of enabled widgets, using the widget ID as the key for each entry.

```csharp
// WidgetProvider.cs

// Class member of WidgetProvider
public static Dictionary<string, CompactWidgetInfo> RunningWidgets = new Dictionary<string, CompactWidgetInfo>(); 
```



## Implement the IFeedProvider methods

In the next few sections, we'll implement the methods of the **IFeedProvider** interface. 

> [!NOTE]
> Objects passed into the callback methods of the **IFeedProvider** interface are only guaranteed to be valid within the callback. You should not store references to these objects because their behavior outside of the context of the callback is undefined.

## OnFeedProviderEnabled

[TBD - Just pasting C++/WinRT code from the spec for now. Update to C# if supported]
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

[TBD - Just pasting C++/WinRT code from the spec for now. Update to C# if supported]
```csharp
// WidgetProvider.cs

void OnFeedProviderDisabled(FeedProviderDisabledArgs args)
    {
       // This call can be used for Telemtry and/or revoked OAuth token if needed
       // as well as the signal to shutdown the app if it's not serving any other
       // feed providers.
    }
```

## OnFeedEnabled

[TBD - Just pasting C++/WinRT code from the spec for now. Update to C# if supported]
```csharp
// WidgetProvider.cs

 void OnFeedEnabled(FeedEnabledArgs args)
    {
       // Use for Telemetry and/or modification of OAuth token if needed.
    }
```

## OnFeedDisabled

[TBD - Just pasting C++/WinRT code from the spec for now. Update to C# if supported]
```csharp
// WidgetProvider.cs

void OnFeedDisabled(FeedDisabledArgs args)
    {
       // Use for Telemetry and/or modification of OAuth token if needed.
    }
```

## OnCustomQueryParametersRequested

[TBD - Just pasting C++/WinRT code from the spec for now. Update to C# if supported]
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

[TBD - this was the "what you do in the constructor" section for widgets. Needs to be updated.]

When our widget provider is first initialized, it's a good idea to ask **WidgetManager** if there are any running widgets that our provider is currently serving. It will help to recover the app to the previous state in case of the computer restart or the provider crash. Call **WidgetManager.GetDefault** to get the default widget manager instance for the app. Then call [GetWidgetInfos](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetmanager.getwidgetinfos), which returns an array of [WidgetInfo](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetinfo) objects. Copy the widget IDs, names, and custom state into the helper struct **CompactWidgetInfo** and save it to the **RunningWidgets** member variable. Paste the following code into the class definition for the **WidgetProvider** class.

```csharp
// WidgetProvider.cs

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

## Implement a class factory that will instantiate FeedProvider on request

In order for the widget host to communicate with our feed provider, we must call [CoRegisterClassObject](/windows/win32/api/combaseapi/nf-combaseapi-coregisterclassobject). This function requires us to create an implementation of the [IClassFactory](/windows/win32/api/unknwn/nn-unknwn-iclassfactory) that will create a class object for our **FeedProvider** class.  We will implement our class factory in a self-contained helper class. 

In Visual Studio, right-click the `ExampleFeedProvider` project in **Solution Explorer** and select **Add->Class**. In the **Add class** dialog, name the class "FactoryHelper" and click **Add**.

Replace the contents of the FactoryHelper.cs file with the following code. This code defines the **IClassFactory** interface and implements it's two methods, [CreateInstance](/windows/win32/api/unknwn/nf-unknwn-iclassfactory-createinstance) and [LockServer](/windows/win32/api/unknwn/nf-unknwn-iclassfactory-lockserver). This code is typical boilerplate for implementing a class factory and is not specific to the functionality of a widget provider except that we indicate that the class object being created implements the **IFeedProvider** interface. 

```csharp
// FactoryHelper.cs

using Microsoft.Windows.Widgets.Providers;
using System.Runtime.InteropServices;
using WinRT;

namespace COM
{
    static class Guids
    {
        public const string IClassFactory = "00000001-0000-0000-C000-000000000046";
        public const string IUnknown = "00000000-0000-0000-C000-000000000046";
    }

    /// 
    /// IClassFactory declaration
    /// 
    [ComImport, ComVisible(false), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid(COM.Guids.IClassFactory)]
    internal interface IClassFactory
    {
        [PreserveSig]
        int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject);
        [PreserveSig]
        int LockServer(bool fLock);
    }

    [ComVisible(true)]
    class FeedProviderFactory<T> : IClassFactory
    where T : IFeedProvider, new()
    {
        public int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject)
        {
            ppvObject = IntPtr.Zero;

            if (pUnkOuter != IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(CLASS_E_NOAGGREGATION);
            }

            if (riid == typeof(T).GUID || riid == Guid.Parse(COM.Guids.IUnknown))
            {
                // Create the instance of the .NET object
                ppvObject = MarshalInspectable<IFeedProvider>.FromManaged(new T());
            }
            else
            {
                // The object that ppvObject points to does not support the
                // interface identified by riid.
                Marshal.ThrowExceptionForHR(E_NOINTERFACE);
            }

            return 0;
        }

        int IClassFactory.LockServer(bool fLock)
        {
            return 0;
        }

        private const int CLASS_E_NOAGGREGATION = -2147221232;
        private const int E_NOINTERFACE = -2147467262;

    }
}

```

## Create a GUID representing the CLSID for your feed provider

Next, you need to create a GUID representing the [CLSID](/windows/win32/com/com-class-objects-and-clsids) that will be used to identify your feed provider for COM activation. The same value will also be used when packaging your app. Generate a GUID in Visual Studio by going to **Tools->Create GUID**. Select the registry format option and click **Copy** and then paste that into a text file so that you can copy it later.

## Register the feed provider class object with OLE

In the Program.cs file for our executable, we will call **CoRegisterClassObject** to register our widget provider with OLE, so that the Widgets Board can interact with it. Replace the contents of Program.cs with the following code. This code imports the **CoRegisterClassObject** function and calls it, passing in the **FeedProviderFactory** interface we defined in a previous step. Be sure to update the **CLSID_Factory** variable declaration to use the GUID you generated in the previous step.

```csharp
// Program.cs

using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;
using Microsoft.Windows.Widgets;
using ExampleWidgetProvider;
using COM;
using System;

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("ole32.dll")]

static extern int CoRegisterClassObject(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnk,
            uint dwClsContext,
            uint flags,
            out uint lpdwRegister);

[DllImport("ole32.dll")] static extern int CoRevokeClassObject(uint dwRegister);

Console.WriteLine("Registering Feed Provider");
uint cookie;

Guid CLSID_Factory = Guid.Parse("XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX");
CoRegisterClassObject(CLSID_Factory, new FeedProviderFactory<WidgetProvider>(), 0x4, 0x1, out cookie);
Console.WriteLine("Registered successfully. Press ENTER to exit.");
Console.ReadLine();

if (GetConsoleWindow() != IntPtr.Zero)
{
    Console.WriteLine("Registered successfully. Press ENTER to exit.");
    Console.ReadLine();
}
else
{
    // Wait until the manager has disposed of the last widget provider.
    using (var emptyWidgetListEvent = WidgetProvider.GetEmptyWidgetListEvent())
    {
        emptyWidgetListEvent.WaitOne();
    }

    CoRevokeClassObject(cookie);
}
```

Note that this code example imports the [GetConsoleWindow](/windows/console/getconsolewindow) function to determine if the app is running as a console application, the default behavior for this walkthrough. If function returns a valid pointer, we write debug information to the console. Otherwise, the app is running as a Windows app. In that case, we wait for the event that we set in [DeleteWidget](#deletewidget) method when the list of enabled widgets is empty, and the we exit the app. For information on converting the example console app to a Windows app, see [Convert your console app to a Windows app](#convert-your-console-app-to-a-windows-app).

## Package your feed provider app

In the current release, only packaged apps can be registered as feed providers. The following steps will take you through the process of packaging your app and updating the app manifest to register your app with the OS as a feed provider.

### Create an MSIX packaging project 

In **Solution Explorer**, right-click your solution and select **Add->New Project...**. In the **Add a new project** dialog, select the "Windows Application Packaging Project" template and click **Next**. Set the project name to "ExampleFeedProviderPackage" and click **Create**. When prompted, set the target version to version 1809 or later and click **OK**.
Next, right-click the ExampleFeedProviderPackage project and select **Add->Project reference**. Select the **ExampleFeedProvider** project and click OK.


### Add Windows App SDK package reference to the packaging project

You need to add a reference to the Windows App SDK nuget package to the MSIX packaging project. In **Solution Explorer**, double-click the ExampleWidgetProviderPackage project to open the ExampleWidgetProviderPackage.wapproj file. Add the following xml inside the **Project** element.

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

If the correct version of the Windows App SDK is already installed on the computer and you don't want to bundle the SDK runtime in your package, you can specify the package dependency in the Package.appxmanifest file for the ExampleWidgetProviderPackage project.

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

The first extension we need to add is the [ComServer](/uwp/schemas/appxpackage/uapmanifestschema/element-com-comserver) extension. This registers the entry point of the executable with the OS. This extension is the packaged app equivalent of registering a COM server by setting a registry key, and is not specific to widget providers.Add the following **com:Extension** element as a child of the **Extensions** element. Change the GUID in the **Id** attribute of the **com:Class** element to the GUID you generated in a previous step.

```xml
<!-- Package.appxmanifest -->
<Extensions>
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:ExeServer Executable="ExampleFeedProvider\ExampleFeedProvider.exe" DisplayName="ExampleFeedProvider">
                <com:Class Id="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" DisplayName="ExampleFeedProvider" />
            </com:ExeServer>
        </com:ComServer>
    </com:Extension>
</Extensions>
```

Next, add the extension that registers the app as a widget provider. Paste the [uap3:Extension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-extension-manual) element in the following code snippet, as a child of the **Extensions** element. Be sure to replace the **ClassId** attribute of the **COM** element with the GUID you used in previous steps.

```xml
<!-- Package.appxmanifest -->
<Extensions>
    ...
    <uap3:Extension Category="windows.appExtension">
        <uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoApp" Id="ContosoApp" PublicFolder="Public">
            <uap3:Properties>
                <FeedProvider Description="Contoso FeedProvider Description" SettingsUri="https://contoso.com/feeds/settings" Icon="ms-appx:Images\ContosoProviderIcon.png">
                    <Activation>
                        <CreateInstance ClassId="ECB883FD-3755-4E1C-BECA-D3397A3FF15C" />
                    </Activation>
                    <Definitions>
                        <Definition Id="Contoso_Feed" DisplayName="Contoso" Description="Contoso Feed"
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

## Add icons and other images to your packaging project

In **Solution Explorer**, right-click your **ExampleFeedProviderPackage** and select **Add->New Folder**. Name this folder ProviderAssets as this is what was used in the `Package.appxmanifest` from the previous step. This is where we will store our feed's **Icon**. Make sure the image names match what comes after **Path=ProviderAssets\\** in your `Package.appxmanifest` or the widgets will not show up in the widget host.



## Testing your feed provider

[TBD - most of this probably needs to change]

Make sure you have selected the architecture that matches your development machine from the **Solution Platforms** drop-down, for example "x64". In **Solution Explorer**, right-click your solution and select **Build Solution**.  Once this is done, right-click your **ExampleWidgetProviderPackage** and select **Deploy**. In the current release, the only supported widget host is the Widgets Board. To see the widgets you will need to open the Widgets Board and select **Add widgets** in the top right. Scroll to the bottom of the available widgets and you should see the mock **Weather Widget** and **Microsoft Counting Widget** that were created in this tutorial. Click on the widgets to pin them to your widgets board and test their functionality.

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

To convert the console app created in this walkthrough to a Windows app, right-click the **ExampleFeedProvider** project in **Solution Explorer** and select **Properties**. Under Application->General change the **Output type** from "Console Application" to "Windows Application".

[TBD - update image for feeds]
:::image type="content" source="images/convert-to-windows-app-cs.png" alt-text="A screenshot showing the C# widget provider project properties with the output type set to Windows Application":::

## Publishing your feed provider app

After you have developed and tested your feed provider you must publish your app on the Microsoft Store in order for users to install your feeds on their devices. For step by step guidance for publishing an app, see [Publish your app in the Microsoft Store](/windows/apps/publish/publish-your-app/overview?pivots=store-installer-msix).

