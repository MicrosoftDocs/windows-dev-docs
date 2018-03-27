---
author: joannaleecy
title: Create a retail demo experience app
description: Create a Retail Demo Experience (RDX) app, which is a single app that can launch in both retail demo mode and normal mode
ms.assetid: f83f950f-7fdd-4f18-8127-b92a8f400061
ms.author: joanlee
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, retail demo app
ms.localizationpriority: medium
---
#  Create a Retail Demo Experience (RDX) app

When customers walk into a retail store or location, they expect to find the latest PCs and mobile phones on display and these devices on display are known as retail demo devices.
Retail demo devices and content installed on them are largely responsible for the customer experience at the stores because customers often spend a considerable chunk of their time playing around with these devices.

Apps that are installed on these PCs and mobile phones in the retail stores must be a retail demo experience (RDX) app. This article provides an overview of how to design and develop a retail demo version of an app to be installed on PCs and mobile demo devices at a retail store.

A retail demo experience app comes in a single build that can be launched in one of the two different modes- _normal_ or _retail_.
From our customers' perspective, there is only one app and to help our customers distinguish between the two versions, it is recommended that your app running in retail mode display the words "Retail" prominently in the title bar or in a suitable location.

In addition to the Store requirements for apps, RDX apps must also be fully compatible with the retail demo devices set up, clean up, and update system to ensure that customers have a consistently positive experience at the retail store.

## Design principles

### Show your best

Use the retail demo experience to showcase why your application rocks.  This is likely the first time your customer will see your application, so show them the best piece!

### Show it fast

Customers can be impatient - The faster a user can experience the real value of your app, the better.

### Keep the story simple

Remember that the retail demo experience is an elevator pitch for your app’s value.

### Focus on the experience

Give the user time to digest your content.  While getting them to the best part fast is important, designing suitable pauses can help them to fully enjoy the experience.

## Technical requirements

As retail demo experience apps are meant to showcase the best of your app to retail customers, it is essential that they meet these technical requirements and adhere to privacy regulations that the Store has for all retail demo experience apps.
This can also be used as a checklist to help you prepare for the validation process and to provide clarity in the testing process. Note that these requirements have to be maintained, not just for the validation process, but for the entire lifetime of the retail demo experience app; as long as your app stays running on the retail demo devices.

### Critical level requirements

RDX apps that do not meet these critical requirements will be removed from all retail demo devices as soon as possible.

* No asking for Personal Identifiable Information (PII)

    The app is not allowed to ask customers for any personal identifiable information.  This includes all Microsoft account information, contact details etc.

* Error free experience

    Your app must run with no errors. Additionally, no error pop ups or notifications should be shown to customers using the retail demo devices. This is important as we want to showcase our best to customers and the best should be error free.
    Another reason is that errors reflect negatively on the app itself, your brand, the device which the app is running on, the device's manufacturer's brand, and Microsoft's brand as well.

* Paid apps must have a Trial mode

    In order for apps to be installed on retail demo devices, your app either needs to be a free app or have an established Trial mode.  Customers aren't looking to pay for an experience in a retail store. For more information, see [Exclude or limit features in a trial version](https://msdn.microsoft.com/windows/uwp/monetize/exclude-or-limit-features-in-a-trial-version-of-your-app)

### High priority requirements

RDX apps that do not meet these high priority requirements need to be investigated for a fix immediately. If no immediate fix is found, this app may be removed from all retail demo devices.

* Memorable offline experience

    Your retail demo experience app needs to demonstrate a great offline experience as about 50% of the devices are offline at retail locations. This is to ensure that customers interacting with your app offline are still able to have a meaningful and positive experience.

* Updated content experience

    To deliver a great experience, your app needs to be always up to date and customers should never be prompted for application updates when your app is online.

* No anonymous communication

    Since a customer using a retail demo device is an anonymous user, they should not be able to message or share content from the device.

* Deliver consistent experience by utilizing the clean up process

    Every customer should have the same experience when they walk up to a retail demo device. Your app should utilize [clean up process](#clean-up-process) to return to the same default state after each use as we do not want the next customer to see what the last customer left behind.  This includes scoreboards, achievements, and unlocks.

* Age appropriate content

    All retail demo experience app content needs to be assigned a Teen or lower rating category. For more information, see [Getting your app rated by IARC](https://www.globalratings.com/for-developers.aspx) and [ESRB ratings](https://www.esrb.org/ratings/ratings_guide.aspx).

### Medium priority requirements

The Windows Retail Store team may reach out to developers directly to set up a discussion on how to fix these issues.

* Ability to run successfully over a range of devices

    Retail demo experience apps must run well on all devices, including devices with low-end specifications. If the retail demo experience app is installed on devices that did not meet the minimum specifications to run the app, the app needs to clearly inform the user about this. Minimum device requirements has to be made known so that the app can always run with high performance.

* Meet retail store app size requirements

    The app must be smaller than 800MB. Contact the Windows Retail Store team directly for further discussion if your retail demo experience app do not meet the size requirements.

## Preparing codebase for Retail Demo Mode development

The [**IsDemoModeEnabled**](https://docs.microsoft.com/uwp/api/windows.system.profile.retailinfo.isdemomodeenabled) property in the [**RetailInfo**](https://docs.microsoft.com/uwp/api/Windows.System.Profile.RetailInfo) utility class, which is part of the [Windows.System.Profile](https://docs.microsoft.com/uwp/api/windows.system.profile) namespace in the Windows 10 SDK, is used as a Boolean indicator to specify which code path your application runs on - the _normal_ mode or the _retail_ mode.

When [**IsDemoModeEnabled**](https://docs.microsoft.com/uwp/api/windows.system.profile.retailinfo.isdemomodeenabled) returns true, you can query for a set of properties about the device using [**RetailInfo.Properties**](https://docs.microsoft.com/uwp/api/windows.system.profile.retailinfo.properties) to build a more customized retail demo experience. These properties include [**ManufacturerName**](https://docs.microsoft.com/uwp/api/windows.system.profile.knownretailinfoproperties.manufacturername), [**Screensize**](https://docs.microsoft.com/uwp/api/windows.system.profile.knownretailinfoproperties.screensize), [**Memory**](https://docs.microsoft.com/uwp/api/windows.system.profile.knownretailinfoproperties.memory) and so on.


## Clean up process

The clean up process is used to automatically reset retail demo devices back to original default settings when there is no interaction with the device for fixed duration. This is to ensure that every user in the retail store can walk up to a device and have the exact default intended experience when interacting with the device. When developing a retail demo experience app, it is important to understand when and how the clean up process is triggered, what happens during the default clean up process, and learn how to customize this clean up process according to the requirements of your intended retail demo experience.

### When does clean up begin?

The clean up sequence begins after certain amount of device idle time. Idle time begins count when there is no input from touch, mouse, and keyboard on the device.

#### Desktop/PC

After 120 seconds of idle time, the idle attract app video will start playing on the device. 5 seconds later, the cleanup process kicks in.

#### Phone

After 60 seconds of idle time, the idle attract app video will start playing on the device and the cleanup process kicks in immediately.

### What happens during a default clean up process?

#### Step 1: clean up
* All Win32 and store apps are closed
* All files in known folders like __Pictures__, __Videos__, __Music__, __Documents__, __SavedPictures__, __CameraRoll__, __Desktop__ and __Downloads__ folders are deleted
* Unstructured and structured roaming states are deleted
* Structured local states are deleted

#### Step 2: Set up
* For offline devices: Folders remain empty
* For online devices: Retail demo assets can be pushed to the device from the Microsoft Store

### How to store data across user sessions?

If you want to store data across user sessions, you can store information in __ApplicationData.Current.TemporaryFolder__ as the default clean up process does not automatically delete data in this folder. Note that information stored using *LocalState* is deleted during the clean up process.

### How to customize the clean up process?

If you wish to customize the clean up process, you need to implement the `Microsoft-RetailDemo-Cleanup` app service into your app.

Scenarios where a custom clean up logic is needed includes running an expensive setup, downloading and caching data or not wanting *LocalState* data to be deleted.

Step 1: Declare the _Microsoft-RetailDemo-Cleanup_ service in your application manifest.
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

Step 2: Implement your custom clean up logic under the _AppdataCleanup_ case function using the sample template below.
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

* [Store and retrieve app data](https://msdn.microsoft.com/windows/uwp/app-settings/store-and-retrieve-app-data)
* [How to create and consume an app service](https://msdn.microsoft.com/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service)
* [Localizing app contents](https://msdn.microsoft.com/windows/uwp/globalizing/globalizing-portal)


 

 
