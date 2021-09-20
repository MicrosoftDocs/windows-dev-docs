---
description: How to use AppLifecycle's rich activation features in unpackaged apps (Windows App SDK)
title: Rich activation for unpackaged apps in AppLifecycle (Windows App SDK)
ms.topic: article
ms.date: 05/25/2021
keywords: AppLifecycle, Windows, activation, activation contracts, rich activation, win32, win32 activation, unpackaged app, unpackaged app activation
ms.author: hickeys
author: hickeys
ms.localizationpriority: medium
---

# Rich activation in AppLifecycle

In the Windows App SDK, AppLifecycle brings support for UWP-style rich activation behavior to all apps, packaged and unpackaged alike. This first release focuses on bringing the most commonly-used activation kinds to unpackaged apps, and future releases aim to support more of UWP's [44 activation kinds](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind).

Supporting rich activations requires two steps:

- Tell the system that your app supports one or more rich activation kinds.
- Receive and process the rich activation payloads your app receives when it is activated.

## Prerequisites

> [!IMPORTANT]
> AppLifecycle APIs are currently supported in the [preview release channel](../preview-channel.md) and [experimental release channel](../experimental-channel.md) of the Windows App SDK. This feature is not currently supported for use by apps in production environments.

To use the AppLifecycle APIs in the Windows App SDK:

1. Download and install the latest preview or experimental release of the Windows App SDK. For more information, see [Install developer tools](../set-up-your-development-environment.md#4-install-the-windows-app-sdk-extension-for-visual-studio).
2. Follow the instructions to [create a new project that uses the Windows App SDK](../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

## Activation details for unpackaged apps

The current version of the Windows App SDK supports the four most common activation kinds to unpackaged apps:

| Activation kind | Description                                                  |
| --------------- | ------------------------------------------------------------ |
| Launch          | Activate the app from the command line, when the user double-clicks the app's icon, or programmatically via ShellExecute/CreateProcess. |
| File            | Activate an app that has registered for a file type when a file of tht type is opened via ShellExecute, LaunchFileAsync, or the command line. |
| Protocol        | Activate an app that has registered for a protocol when a string of that protocol is executed via ShellExecute, LaunchUriAsync, or the command-line. |
| StartupTask     | Activate the app when the user logs into Windows, either because of a registry key, or because of a shortcut in a well-known startup folder. |

Each type of unpackaged app retrieves its command line arguments in different ways. For example, **Win32** apps expect to receive activation arguments to be passed into WinMain in the form of a string (though they also have the option to call [GetCommandLineW](/windows/win32/api/processenv/nf-processenv-getcommandlinew)). **Windows Forms** apps, however, *must* call [Environment.GetCommandLineArgs](/dotnet/api/system.environment.getcommandlineargs), as arguments will not be automatically passed to them.

## Activation details for packaged apps

Packaged apps that use the Windows App SDK support all 44 of UWP's [activation kinds](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind). Each activation kind has its own corresponding implementation of [IActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.iactivatedeventargs) which contain properties relevant to that specific kind of activation.

Packaged apps will always receive activation event arguments in their [AppInstance.Activated](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.activated) event handler, and also have the option of calling [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs).

### Activation registration

All apps support the Launch activation kind by default. Unlike UWP, the Windows App SDK Launch activation kind includes command line launches. Apps can register for additional activation kinds in several ways.

- Unpackaged apps that use the Windows App SDK can register (and unregister) for additional activation kinds via APIs in the Windows App SDK version of AppLifecycle.
- Unpackaged apps can continue to register for additional activation kinds using the traditional method of writing registry keys.
- Packaged apps can register for additional activation kinds via entries in their application manifest.

Activation registrations are per-user. If your app is installed for multiple users, you will need to re-register activations for each user.

## Examples

### Register for rich activation

Though apps can call the registration APIs at any time, the most common scenario is checking registrations on app startup.

This example shows how an unpackaged app can use the registration APIs to register for several activation kinds when the app is launched. Note that an unpackaged app must use the [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) function to initialize the Windows App SDK framework package. For more information, see [Reference the Windows App SDK framework package at run time](../reference-framework-package-run-time.md).

> [!NOTE]
> This example registers associations with three image file types at once. This is convenient, but the outcome is the same as registering each file type individually; registering new image types does not overwrite previous registrations. However, if an app re-registers an already registered file type with a different set of verbs, the previous set of verbs will be overwritten for that file type.

```c++
const UINT32 majorMinorVersion{ 0x00010000 };
PCWSTR versionTag{ L"preview1" };
const PACKAGE_VERSION minVersion{};
WCHAR szExePath[MAX_PATH];
WCHAR szExePathAndIconIndex[MAX_PATH + 8];

int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // Initialize Windows App SDK framework package for unpackaged apps.
    HRESULT hr{ MddBootstrapInitialize(majorMinorVersion, versionTag, minVersion) };
    if (FAILED(hr))
    {
        wprintf(L"Error 0x%X in MddBootstrapInitialize(0x%08X, %s, %hu.%hu.%hu.%hu)\n",
            hr, majorMinorVersion, versionTag, minVersion.Major, 
            minVersion.Minor, minVersion.Build, minVersion.Revision);
        return hr;
    }

    // Get the current executable filesystem path, so we can
    // use it later in registering for activation kinds.
    GetModuleFileName(NULL, szExePath, MAX_PATH);
    wcscpy_s(szExePathAndIconIndex, szExePath);
    wcscat_s(szExePathAndIconIndex, L",1");

    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_CLASSNAME, szWindowClass, MAX_LOADSTRING);
    RegisterWindowClass(hInstance);
    if (!InitInstance(hInstance, nCmdShow))
    {
        return FALSE;
    }

    MSG msg;
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    // Uninitialize Windows App SDK.
    MddBootstrapShutdown();
    return (int)msg.wParam;
}

void RegisterForActivation()
{
    OutputMessage(L"Registering for rich activation");

    // Register one or more supported filetypes, specifying 
    // an icon (specified by binary file path plus resource index),
    // a display name to use in Shell and Settings,
    // zero or more verbs for the File Explorer context menu,
    // and the path to the EXE to register for activation.
    hstring myFileTypes[3] = { L".foo", L".foo2", L".foo3" };
    hstring verbs[2] = { L"view", L"edit" };
    ActivationRegistrationManager::RegisterForFileTypeActivation(
        myFileTypes,
        szExePathAndIconIndex,
        L"Contoso File Types",
        verbs,
        szExePath
    );

    // Register a URI scheme for protocol activation,
    // specifying the scheme name, icon, display name and EXE path.
    ActivationRegistrationManager::RegisterForProtocolActivation(
        L"foo",
        szExePathAndIconIndex,
        L"Contoso Foo Protocol",
        szExePath
    );

    // Register for startup activation.
    // As we're registering for startup activation multiple times,
    // and this is a multi-instance app, we'll get multiple instances
    // activated at startup.
    ActivationRegistrationManager::RegisterForStartupActivation(
        L"ContosoStartupId",
        szExePath
    );

    // If we don't specify the EXE, it will default to this EXE.
    ActivationRegistrationManager::RegisterForStartupActivation(
        L"ContosoStartupId2",
        L""
    );
}
```

### Get rich activation event arguments

Once activated, an app must retrieve its activation event arguments. In this example, an unpackaged app calls an AppLifecycle API to get the event args for the activation event.

> [!NOTE]
> Win32 apps typically get command-line arguments very early their `WinMain` method. Similarly, these apps should call [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs) in the same place where they previously would have used the supplied the `lpCmdLine` parameter or called `GetCommandLineW`.

```c++
void GetActivationInfo()
{
    AppActivationArguments args = AppInstance::GetCurrent().GetActivatedEventArgs();
    ExtendedActivationKind kind = args.Kind();
    if (kind == ExtendedActivationKind::Launch)
    {
        ILaunchActivatedEventArgs launchArgs = 
            args.Data().as<ILaunchActivatedEventArgs>();
        if (launchArgs != NULL)
        {
            winrt::hstring argString = launchArgs.Arguments().c_str();
            std::vector<std::wstring> argStrings = split_strings(argString);
            OutputMessage(L"Launch activation");
            for (std::wstring s : argStrings)
            {
                OutputMessage(s.c_str());
            }
        }
    }
    else if (kind == ExtendedActivationKind::File)
    {
        IFileActivatedEventArgs fileArgs = 
            args.Data().as<IFileActivatedEventArgs>();
        if (fileArgs != NULL)
        {
            IStorageItem file = fileArgs.Files().GetAt(0);
            OutputFormattedMessage(
                L"File activation: %s", file.Name().c_str());
        }
    }
    else if (kind == ExtendedActivationKind::Protocol)
    {
        IProtocolActivatedEventArgs protocolArgs = 
            args.Data().as<IProtocolActivatedEventArgs>();
        if (protocolArgs != NULL)
        {
            Uri uri = protocolArgs.Uri();
            OutputFormattedMessage(
                L"Protocol activation: %s", uri.RawUri().c_str());
        }
    }
    else if (kind == ExtendedActivationKind::StartupTask)
    {
        IStartupTaskActivatedEventArgs startupArgs = 
            args.Data().as<IStartupTaskActivatedEventArgs>();
        if (startupArgs != NULL)
        {
            OutputFormattedMessage(
                L"Startup activation: %s", startupArgs.TaskId().c_str());
        }
    }
}
```

### Unregister

This example demonstrates how an unpackaged app can unregister for specific activation kinds dynamically, using the AppLifecycle APIs.

> [!NOTE]
> When unregistering for startup activation, the app must use the same taskId that it used when it originally registered.

```c++
void UnregisterForActivation()
{
    OutputMessage(L"Unregistering for rich activation");
    
    // Unregister one or more registered filetypes.
    try
    {
        hstring myFileTypes[3] = { L".foo", L".foo2", L".foo3" };
        ActivationRegistrationManager::UnregisterForFileTypeActivation(
            myFileTypes,
            szExePath
        );
    }
    catch (...)
    {
        OutputMessage(L"Error unregistering file types");
    }

    // Unregister a protocol scheme.
    ActivationRegistrationManager::UnregisterForProtocolActivation(
        L"foo",
        L"");

    // Unregister for startup activation.
    ActivationRegistrationManager::UnregisterForStartupActivation(
        L"ContosoStartupId");
    ActivationRegistrationManager::UnregisterForStartupActivation(
        L"ContosoStartupId2");
}
```
