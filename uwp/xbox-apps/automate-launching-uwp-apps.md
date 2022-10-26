---
title: Automate launching Windows 10 Universal Windows Platform (UWP) apps
description: Developers can use protocol activation and launch activation to automate launching their UWP apps or games for automated testing.
ms.topic: article
ms.localizationpriority: medium
ms.date: 02/08/2017
---
# Automate launching Windows 10 UWP apps

## Introduction

Developers have several options for achieving automated launching of Universal Windows Platform (UWP) apps. In this paper we will explore methods of launching an app by using protocol activation and launch activation.

*Protocol activation* allows an app to register itself as a handler for a given protocol. 

*Launch activation* is the normal launching of an app, such as launching from the app tile.

With each activation method, you have the option of using the command line or a launcher application. For all activation methods, if the app is currently running, the activation will bring the app to the foreground (which reactivates it) and provide the new activation arguments. This allows flexibility to use activation commands to provide new messages to the app. It is important to note that the project needs to be compiled and deployed for the activation method to run the newly updated app. 

## Protocol activation

Follow these steps to set up protocol activation for apps: 

1. Open the **Package.appxmanifest** file in Visual Studio.
2. Select the **Declarations** tab.
3. Under the **Available Declarations** drop-down, select **Protocol**, and then select **Add**.
4. Under **Properties**, in the **Name** field, enter a unique name to launch the app. 

	![Protocol activation](images/automate-uwp-apps-1.png)

5. Save the file and deploy the project. 
6. After the project has been deployed, the protocol activation should be set. 
7. Go to **Control Panel\All Control Panel Items\Default Programs** and select **Associate a file type or protocol with a specific program**. Scroll to the **Protocols** section to see if the protocol is listed. 

Now that protocol activation is set up, you have two options (the command line or launcher application) for activating the app by using the protocol. 

### Command line

The app can be protocol-activated by using the command line with the command start followed by the protocol name set previously, a colon (“:”), and any parameters. The parameters can be any arbitrary string; however, to take advantage of the Uniform Resource Identifier (URI) capabilities, it is advisable to follow the standard URI format: 

  ```
  scheme://username:password@host:port/path.extension?query#fragment
  ```

The Uri object has methods of parsing a URI string in this format. For more information, see [Uri class (MSDN)](/uwp/api/windows.foundation.uri). 

Examples:

  ```
  >start bingnews:
  >start myapplication:protocol-parameter
  >start myapplication://single-player/level3?godmode=1&ammo=200
  ```

Protocol command-line activation supports Unicode characters up to a 2038-character limit on the raw URI. 

### Launcher application

For launching, create a separate application that supports the WinRT API. The C++ code for launching with protocol activation in a launcher program is shown in the following sample, where **PackageURI** is the URI for the application with any arguments; for example `myapplication:` or `myapplication:protocol activation arguments`.

```
bool ProtocolLaunchURI(Platform::String^ URI)
{
       IAsyncOperation<bool>^ protocolLaunchAsyncOp;
       try
       {
              protocolLaunchAsyncOp = Windows::System::Launcher::LaunchUriAsync(ref new 
Uri(URI));
       }
       catch (Platform::Exception^ e)
       {
              Platform::String^ dbgStr = "ProtocolLaunchURI Exception Thrown: " 
+ e->ToString() + "\n";
              OutputDebugString(dbgStr->Data());
              return false;
       }

       concurrency::create_task(protocolLaunchAsyncOp).wait();

       if (protocolLaunchAsyncOp->Status == AsyncStatus::Completed)
       {
              bool LaunchResult = protocolLaunchAsyncOp->GetResults();
              Platform::String^ dbgStr = "ProtocolLaunchURI " + URI 
+ " completed. Launch result " + LaunchResult + "\n";
              OutputDebugString(dbgStr->Data());
              return LaunchResult;
       }
       else
       {
              Platform::String^ dbgStr = "ProtocolLaunchURI " + URI + " failed. Status:" 
+ protocolLaunchAsyncOp->Status.ToString() + " ErrorCode:" 
+ protocolLaunchAsyncOp->ErrorCode.ToString() + "\n";
              OutputDebugString(dbgStr->Data());
              return false;
       }
}
```
Protocol activation with the launcher application has the same limitations for arguments as protocol activation with the command line. Both support Unicode characters up to a 2038-character limit on the raw URI. 

## Launch activation

You can also launch the app by using launch activation. No setup is required, but the Application User Model ID (AUMID) of the UWP app is needed. The AUMID is the package family name followed by an exclamation point and the application ID. 

The best way to obtain the package family name is to complete these steps:

1. Open the **Package.appxmanifest** file.
2. On the **Packaging** tab, enter the **Package name**.

	![Launch activation](images/automate-uwp-apps-2.png)

3. If the **Package family name** is not listed, open PowerShell and run `>get-appxpackage MyPackageName` to find the **PackageFamilyName**.

The application ID can be found in the **Package.appxmanifest** file (opened in XML view) under the `<Applications>` element.

### Command line

A tool for performing a launch activation of a UWP app is installed with the Windows 10 SDK. It can be run from the command line, and it takes the AUMID of the app to be launched as an argument.

```
C:\Program Files (x86)\Windows Kits\10\App Certification Kit\microsoft.windows.softwarelogo.appxlauncher.exe <AUMID>
```

It would look something like this:

```
"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\microsoft.windows.softwarelogo.appxlauncher.exe" MyPackageName_ph1m9x8skttmg!AppId
```

This option does not support command-line arguments. 

### Launcher application

You can create a separate application that supports using COM to use for launching. The following example shows C++ code for launching with launch activation in a launcher program. With this code, you can create an **ApplicationActivationManager** object and call **ActivateApplication** passing in the AUMID found previously and any arguments. For more information about the other parameters, see [IApplicationActivationManager::ActivateApplication method (MSDN)](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iapplicationactivationmanager-activateapplication).

```
#include <ShObjIdl.h>
#include <atlbase.h>

HRESULT LaunchApp(LPCWSTR AUMID)
{
     HRESULT hr = CoInitializeEx(nullptr, COINIT_APARTMENTTHREADED);
     if (FAILED(hr))
     {
            wprintf(L"LaunchApp %s: Failed to init COM. hr = 0x%08lx \n", AUMID, hr);
     }
     {
            CComPtr<IApplicationActivationManager> AppActivationMgr = nullptr;
            if (SUCCEEDED(hr))
            {
                   hr = CoCreateInstance(CLSID_ApplicationActivationManager, nullptr,  
CLSCTX_LOCAL_SERVER, IID_PPV_ARGS(&AppActivationMgr));
                   if (FAILED(hr))
                   {
                         wprintf(L"LaunchApp %s: Failed to create Application Activation 
Manager. hr = 0x%08lx \n", AUMID, hr);
                   }
            }
            if (SUCCEEDED(hr))
            {
                   DWORD pid = 0;
                   hr = AppActivationMgr->ActivateApplication(AUMID, nullptr, AO_NONE, 
&pid);
                   if (FAILED(hr))
                   {
                         wprintf(L"LaunchApp %s: Failed to Activate App. hr = 0x%08lx 
\n", AUMID, hr);
                   }
            }
     }
     CoUninitialize();
     return hr;
}
```

It is worth noting that this method does support arguments being passed in, unlike the previous method for launching (that is, using the command line).

## Accepting arguments

To accept arguments passed in on activation of the UWP app, you must add some code to the app. To determine if protocol activation or launch activation occurred, override the **OnActivated** event and check the argument type, and then get the raw string or Uri object’s pre-parsed values. 

This example shows how to get the raw string.

```
void OnActivated(IActivatedEventArgs^ args)
{
		// Check for launch activation
		if (args->Kind == ActivationKind::Launch)
		{
			auto launchArgs = static_cast<LaunchActivatedEventArgs^>(args);	
			Platform::String^ argval = launchArgs->Arguments;
			// Manipulate arguments …
		}

		// Check for protocol activation
		if (args->Kind == ActivationKind::Protocol)
		{
			auto protocolArgs = static_cast< ProtocolActivatedEventArgs^>(args);
			Platform::String^ argval = protocolArgs->Uri->ToString();
			// Manipulate arguments …
		}
}
```

## Summary
In summary, you can use various methods to launch the UWP app. Depending on the requirements and use cases, different methods may be better suited than others. 

## See also
- [UWP on Xbox One](index.md)