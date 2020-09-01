---
title: Add retail demo (RDX) features to your app
description: Prepare your app for the retail demo mode, helping showcase your app on the retail sales floor.
ms.assetid: f83f950f-7fdd-4f18-8127-b92a8f400061
ms.date: 10/02/2018
ms.topic: article
keywords: windows 10, uwp, retail demo app
ms.localizationpriority: medium
---
# Add retail demo (RDX) features to your app

Include a retail demo mode in your Windows app so customers who try out PCs and devices on the sales floor can jump right in.

When customers are in a retail store, they expect to be able to try out demos of PCs and devices. They often spend a considerable chunk of their time playing around with apps through the [retail demo experience (RDX)](/windows-hardware/customize/desktop/retail-demo-experience).

You can set up your app to provide different experiences while in _normal_ or _retail_ modes. For example, if your app starts with a setup process, you might skip past it in retail mode, and prepopulate the app with sample data and default settings so they can jump right in.

From our customers' perspective, there is only one app. To help customers distinguish between the two modes, we recommend that while your app is in retail mode, it shows the word "Retail" prominently in the title bar or in a suitable location.

In addition to the Microsoft Store requirements for apps, RDX-aware apps must also be compatible with the RDX setup, cleanup, and update processes to ensure that customers have a consistently positive experience at the retail store.

## Design principles

* **Show your best**. Use the retail demo experience to showcase why your app rocks. This is likely the first time your customer will see your app, so show them the best piece!

* **Show it fast**. Customers can be impatient - The faster a user can experience the real value of your app, the better.

* **Keep the story simple**. The retail demo experience is an elevator pitch for your app’s value.

* **Focus on the experience**. Give the user time to digest your content. While getting them to the best part fast is important, designing suitable pauses can help them to fully enjoy the experience.

## Technical requirements

As RDX-aware apps are meant to showcase the best of your app to retail customers, they must meet technical requirements and adhere to privacy regulations that the Microsoft Store has for all retail demo experience apps.

This can be used as a checklist to help you prepare for the validation process and to provide clarity in the testing process. Note that these requirements have to be maintained, not just for the validation process, but for the entire lifetime of the retail demo experience app; as long as your app stays running on the retail demo devices.

### Critical requirements

RDX-aware apps that do not meet these critical requirements will be removed from all retail demo devices as soon as possible.

* **Don't ask for personal identifiable information (PII)**. This includes login info, Microsoft account info, or contact details.

* **Error-free experience**. Your app must run with no errors. Additionally, no error pop ups or notifications should be shown to customers using the retail demo devices. Errors reflect negatively on the app itself, your brand, the device's brand, the device's manufacturer's brand, and Microsoft's brand.

* **Paid apps must have a trial mode**. Your app either needs to be a free or include a [trial mode](./exclude-or-limit-features-in-a-trial-version-of-your-app.md). Customers aren't looking to pay for an experience in a retail store.

### High-priority requirements

RDX-aware apps that do not meet these high priority requirements need to be investigated for a fix immediately. If no immediate fix is found, this app may be removed from all retail demo devices.

* **Memorable offline experience**. Your app needs to demonstrate a great offline experience as about 50% of the devices are offline at retail locations. This is to ensure that customers interacting with your app offline are still able to have a meaningful and positive experience.

* **Updated content experience**. Your app should never be prompt for updates when online. If updates are needed, they should be performed silently.

* **No anonymous communication**. Because a customer using a retail demo device is an anonymous user, they should not be able to message or share content from the device.

* **Deliver consistent experiences by using the cleanup process**. Every customer should have the same experience when they walk up to a retail demo device. Your app should use [clean up process](#cleanup-process) to return to the same default state after each use. We don't want the next customer to see what the last customer left behind. This includes scoreboards, achievements, and unlocks.

* **Age appropriate content**. All app content needs to be assigned a Teen or lower rating category. To learn more, see [Getting your app rated by IARC](https://www.globalratings.com/for-developers.aspx) and [ESRB ratings](https://www.esrb.org/ratings/ratings_guide.aspx).

### Medium-priority requirements

The Windows Retail Store team may reach out to developers directly to set up a discussion on how to fix these issues.

* **Ability to run successfully over a range of devices**. Apps must run well on all devices, including devices with low-end specifications. If the app is installed on devices that did not meet the minimum specifications, the app needs to clearly inform the user about this. Minimum device requirements has to be made known so that the app can always run with high performance.

* **Meet retail store app size requirements**. The app must be smaller than 800MB. Contact the Windows Retail Store team directly for further discussion if your RDX-aware app does not meet the size requirements.

## RetailInfo API: Preparing your code for demo mode

### IsDemoModeEnabled
The [**IsDemoModeEnabled**](/uwp/api/windows.system.profile.retailinfo.isdemomodeenabled) property in the [**RetailInfo**](/uwp/api/Windows.System.Profile.RetailInfo) utility class, which is part of the [Windows.System.Profile](/uwp/api/windows.system.profile) namespace in the Windows 10 SDK, is used as a Boolean indicator to specify which code path your app runs on - the _normal_ mode or the _retail_ mode.

``` csharp
using Windows.Storage;

StorageFolder folder = ApplicationData.Current.LocalFolder;

if (Windows.System.Profile.RetailInfo.IsDemoModeEnabled) 
{
    // Use the demo specific directory
    folder = await folder.GetFolderAsync("demo");
}

StorageFile file = await folder.GetFileAsync("hello.txt");
// Now read from file
```

``` cpp
using namespace Windows::Storage;

StorageFolder^ localFolder = ApplicationData::Current->LocalFolder;

if (Windows::System::Profile::RetailInfo::IsDemoModeEnabled) 
{
    // Use the demo specific directory
    create_task(localFolder->GetFolderAsync("demo").then([this](StorageFolder^ demoFolder)
    {
        return demoFolder->GetFileAsync("hello.txt");
    }).then([this](task<StorageFile^> fileTask)
    {
        StorageFile^ file = fileTask.get();
    });
    // Do something with file
}
else
{
    create_task(localFolder->GetFileAsync("hello.txt").then([this](StorageFile^ file)
    {
        // Do something with file
    });
}
```

``` javascript
if (Windows.System.Profile.retailInfo.isDemoModeEnabled) {
    console.log("Retail mode is enabled.");
} else {
    Console.log("Retail mode is not enabled.");
}
```

### RetailInfo.Properties

When [**IsDemoModeEnabled**](/uwp/api/windows.system.profile.retailinfo.isdemomodeenabled) returns true, you can query for a set of properties about the device using [**RetailInfo.Properties**](/uwp/api/windows.system.profile.retailinfo.properties) to build a more customized retail demo experience. These properties include [**ManufacturerName**](/uwp/api/windows.system.profile.knownretailinfoproperties.manufacturername), [**Screensize**](/uwp/api/windows.system.profile.knownretailinfoproperties.screensize), [**Memory**](/uwp/api/windows.system.profile.knownretailinfoproperties.memory) and so on.

```csharp
using Windows.UI.Xaml.Controls;
using Windows.System.Profile

TextBlock priceText = new TextBlock();
priceText.Text = RetailInfo.Properties[KnownRetailInfo.Price];
// Assume infoPanel is a StackPanel declared in XAML
this.infoPanel.Children.Add(priceText);
```

```cpp
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::System::Profile;

TextBlock ^manufacturerText = ref new TextBlock();
manufacturerText.set_Text(RetailInfo::Properties[KnownRetailInfoProperties::Price]);
// Assume infoPanel is a StackPanel declared in XAML
this->infoPanel->Children->Add(manufacturerText);
```

```javascript
var pro = Windows.System.Profile;
console.log(pro.retailInfo.properties[pro.KnownRetailInfoProperties.price);
```

#### IDL

```cpp
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//  WindowsRuntimeAPISet

import "oaidl.idl";
import "inspectable.idl";
import "Windows.Foundation.idl";
#include <sdkddkver.h>

namespace Windows.System.Profile
{
    runtimeclass RetailInfo;
    runtimeclass KnownRetailInfoProperties;

    [version(NTDDI_WINTHRESHOLD), uuid(0712C6B8-8B92-4F2A-8499-031F1798D6EF), exclusiveto(RetailInfo)]
    [version(NTDDI_WINTHRESHOLD, Platform.WindowsPhone)]
    interface IRetailInfoStatics : IInspectable
    {
        [propget] HRESULT IsDemoModeEnabled([out, retval] boolean *value);
        [propget] HRESULT Properties([out, retval, hasvariant] Windows.Foundation.Collections.IMapView<HSTRING, IInspectable *> **value);
    }

    [version(NTDDI_WINTHRESHOLD), uuid(50BA207B-33C4-4A5C-AD8A-CD39F0A9C2E9), exclusiveto(KnownRetailInfoProperties)]
    [version(NTDDI_WINTHRESHOLD, Platform.WindowsPhone)]
    interface IKnownRetailInfoPropertiesStatics : IInspectable
    {
        [propget] HRESULT RetailAccessCode([out, retval] HSTRING *value);
        [propget] HRESULT ManufacturerName([out, retval] HSTRING *value);
        [propget] HRESULT ModelName([out, retval] HSTRING *value);
        [propget] HRESULT DisplayModelName([out, retval] HSTRING *value);
        [propget] HRESULT Price([out, retval] HSTRING *value);
        [propget] HRESULT IsFeatured([out, retval] HSTRING *value);
        [propget] HRESULT FormFactor([out, retval] HSTRING *value);
        [propget] HRESULT ScreenSize([out, retval] HSTRING *value);
        [propget] HRESULT Weight([out, retval] HSTRING *value);
        [propget] HRESULT DisplayDescription([out, retval] HSTRING *value);
        [propget] HRESULT BatteryLifeDescription([out, retval] HSTRING *value);
        [propget] HRESULT ProcessorDescription([out, retval] HSTRING *value);
        [propget] HRESULT Memory([out, retval] HSTRING *value);
        [propget] HRESULT StorageDescription([out, retval] HSTRING *value);
        [propget] HRESULT GraphicsDescription([out, retval] HSTRING *value);
        [propget] HRESULT FrontCameraDescription([out, retval] HSTRING *value);
        [propget] HRESULT RearCameraDescription([out, retval] HSTRING *value);
        [propget] HRESULT HasNfc([out, retval] HSTRING *value);
        [propget] HRESULT HasSdSlot([out, retval] HSTRING *value);
        [propget] HRESULT HasOpticalDrive([out, retval] HSTRING *value);
        [propget] HRESULT IsOfficeInstalled([out, retval] HSTRING *value);
        [propget] HRESULT WindowsVersion([out, retval] HSTRING *value);
    }

    [version(NTDDI_WINTHRESHOLD), static(IRetailInfoStatics, NTDDI_WINTHRESHOLD)]
    [version(NTDDI_WINTHRESHOLD, Platform.WindowsPhone), static(IRetailInfoStatics, NTDDI_WINTHRESHOLD, Platform.WindowsPhone)]
    [threading(both)]
    [marshaling_behavior(agile)]
    runtimeclass RetailInfo
    {
    }

    [version(NTDDI_WINTHRESHOLD), static(IKnownRetailInfoPropertiesStatics, NTDDI_WINTHRESHOLD)]
    [version(NTDDI_WINTHRESHOLD, Platform.WindowsPhone), static(IKnownRetailInfoPropertiesStatics, NTDDI_WINTHRESHOLD, Platform.WindowsPhone)]
    [threading(both)]
    [marshaling_behavior(agile)]
    runtimeclass KnownRetailInfoProperties
    {
    }
}
```

## Cleanup process

Cleanup begins two minutes after a shopper stops interacting with the device. The retail demo plays, and Windows begins resetting any sample data in the contacts, photos, and other apps. Depending on the device, this could take between 1-5 minutes to fully reset everything back to normal. This ensures that every customer in the retail store can walk up to a device and have the same experience when interacting with the device.

Step 1: Cleanup
* All Win32 and store apps are closed
* All files in known folders like __Pictures__, __Videos__, __Music__, __Documents__, __SavedPictures__, __CameraRoll__, __Desktop__ and __Downloads__ folders are deleted
* Unstructured and structured roaming states are deleted
* Structured local states are deleted

Step 2:  Setup
* For offline devices: Folders remain empty
* For online devices: Retail demo assets can be pushed to the device from the Microsoft Store

### Store data across user sessions

To store data across user sessions, you can store information in __ApplicationData.Current.TemporaryFolder__ as the default cleanup process does not automatically delete data in this folder. Note that information stored using *LocalState* is deleted during the cleanup process.

### Customize the cleanup process

To customize the cleanup process, implement the `Microsoft-RetailDemo-Cleanup` app service into your app.

Scenarios where a custom cleanup logic is needed includes running an extensive setup, downloading and caching data, or not wanting *LocalState* data to be deleted.

Step 1: Declare the _Microsoft-RetailDemo-Cleanup_ service in your app manifest.
``` CSharp
  <Applications>
      <Extensions>
        <uap:Extension Category="windows.appService" EntryPoint="MyCompany.MyApp.RDXCustomCleanupTask">
          <uap:AppService Name="Microsoft-RetailDemo-Cleanup" />
        </uap:Extension>
      </Extensions>
   </Application>
  </Applications>

```

Step 2: Implement your custom cleanup logic under the _AppdataCleanup_ case function using the sample template below.
``` CSharp
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace MyCompany.MyApp
{
    public sealed class RDXCustomCleanupTask : IBackgroundTask
    {
        BackgroundTaskCancellationReason _cancelReason = BackgroundTaskCancellationReason.Abort;
        BackgroundTaskDeferral _deferral = null;
        IBackgroundTaskInstance _taskInstance = null;
        AppServiceConnection _appServiceConnection = null;

        const string MessageCommand = "Command";

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get the deferral object from the task instance, and take a reference to the taskInstance;
            _deferral = taskInstance.GetDeferral();
            _taskInstance = taskInstance;
            _taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);

            AppServiceTriggerDetails appService = _taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if ((appService != null) && (appService.Name == "Microsoft-RetailDemo-Cleanup"))
            {
                _appServiceConnection = appService.AppServiceConnection;
                _appServiceConnection.RequestReceived += _appServiceConnection_RequestReceived;
                _appServiceConnection.ServiceClosed += _appServiceConnection_ServiceClosed;
            }
            else
            {
                _deferral.Complete();
            }
        }

        void _appServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
        }

        async void _appServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            //Get a deferral because we will be calling async code
            AppServiceDeferral requestDeferral = args.GetDeferral();
            string command = null;
            var returnData = new ValueSet();

            try
            {
                ValueSet message = args.Request.Message;
                if (message.ContainsKey(MessageCommand))
                {
                    command = message[MessageCommand] as string;
                }

                if (command != null)
                {
                    switch (command)
                    {
                        case "AppdataCleanup":
                            {
                                // Do custom clean up logic here
                                break;
                            }
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                requestDeferral.Complete();
                // Also release the task deferral since we only process one request per instance.
                _deferral.Complete();
            }
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _cancelReason = reason;
        }
    }
}
```

## Related links

* [Store and retrieve app data](../design/app-settings/store-and-retrieve-app-data.md)
* [How to create and consume an app service](../launch-resume/how-to-create-and-consume-an-app-service.md)
* [Localizing app contents](../design/globalizing/globalizing-portal.md)
* [Retail demo experience (RDX)](/windows-hardware/customize/desktop/retail-demo-experience)