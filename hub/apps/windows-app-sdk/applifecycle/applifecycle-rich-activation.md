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

> [!IMPORTANT]
> AppLifecycle is an experimental feature that is currently supported only in the [experimental release channel](../experimental-channel.md) of the Windows App SDK. This feature is not supported for use by apps in production environments.

In the Windows App SDK, AppLifecycle brings support for UWP-style rich activation behavior to all apps, packaged and unpackaged alike. This first release focuses on bringing the most commonly-used activation kinds to unpackaged apps, and future releases aim to support more of UWP's [44 activation kinds](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind).

Supporting rich activations requires two steps:

- Tell the system that your app supports one or more rich activation kinds.
- Receive and process the rich activation payloads your app receives when it is activated.

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

Packaged apps will always receive activation event arguments in their `Activation` event handler, and also have the option of calling `AppInstance.GetActivatedEventArgs`.

### Activation registration

All apps support the Launch activation kind by default. Unlike UWP, the Windows App SDK Launch activation kind includes command line launches. Apps can register for additional activation kinds in several ways.

- All apps that use the Windows App SDK can register (and unregister) for additional activation kinds via APIs in the Windows App SDK version of AppLifecycle.
- Unpackaged apps can continue to register for additional activation kinds using the traditional method of writing registry keys.
- Packaged apps can register for additional activation kinds via entries in their application manifest.

Activation registrations are per-user. If your app is installed for multiple users, you will need to re-register activations for each user.

## Examples

### Register for rich activation

Though apps can call the registration APIs at any time, the most common scenario is checking registrations on app startup.

This example shows how an unpackaged app can use the registration APIs to register for several activation kinds when the app is launched.

> [!NOTE]
> This example registers associations with three image file types at once. This is convenient, but the outcome is the same as registering each file type individually; registering new image types does not overwrite previous registrations. However, if an app re-registers an already registered file type with a different set of verbs, the previous set of verbs will be overwritten for that file type.

```c++
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance,
    _In_opt_ HINSTANCE,
    _In_ LPWSTR,
    _In_ int nCmdShow)
{
    // Initialize COM.
    winrt::init_apartment();

    // Registering for rich activation kinds can be done in the
    // app's installer or in the app itself.
    RegisterForActivation();

    // When the app starts, it can get its activated eventargs, and perform
    // any required operations based on the activation kind and payload.
    RespondToActivation();

    ///////////////////////////////////////////////////////////////////////////
    // Standard Win32 window configuration/creation and message pump:
    // ie, whatever the app would normally do - nothing new here.
    RegisterClassAndStartMessagePump(hInstance, nCmdShow);
    return 1;
}

void RegisterForActivation()
{
    // Register one or more supported image filetypes,
    // an icon (specified by binary file path plus resource index),
    // a display name to use in Shell and Settings,
    // zero or more verbs for the File Explorer context menu,
    // and the path to the EXE to register for activation.
    // Note that localizable resource strings are not supported in v1.
    std::wstring imageFileTypes[3] = { L".jpg", L".png", L".bmp" };
    std::wstring verbs[2] = { L"view", L"edit" };
    ActivationRegistrationManager::RegisterForFileTypeActivation(
        imageFileTypes,
        L"C:\\Program Files\\Contoso\\MyResources.dll, -123",
        L"Contoso File Types",
        verbs,
        L"C:\\Program Files\\Contoso\\MyApp.exe");

    // Register some URI schemes for protocol activation,
    // specifying the scheme name, icon, display name and EXE path.
    ActivationRegistrationManager::RegisterForProtocolActivation(
        L"foo",
        L"C:\\Program Files\\Contoso\\MyResources.dll, -45",
        L"Contoso Foo Protocol",
        L"C:\\Program Files\\Contoso\\MyApp.exe");

    // Register for startup activation.
    ActivationRegistrationManager::RegisterForStartupActivation(
        L"MyTaskId",
        L"C:\\Program Files\\Contoso\\MyApp.exe");

    ActivationRegistrationManager::RegisterForStartupActivation(
        L"AnotherTaskId",
        L"");
}
```

### Get rich activation event arguments

Once activated, an app must retrieve its activation event arguments. In this example, an unpackaged app calls an AppLifecycle API to get the event args for the activation event.

> [!NOTE]
> Win32 apps typically get command-line arguments very early their WinMain method. Similarly, these apps should `AppInstance::GetActivatedEventArgs` in the same place where they previously would have used the supplied lpCmdLine parameter or GetCommandLineW.

```c++
void RespondToActivation()
{
    AppActivationArguments args = AppInstance::GetCurrent().GetActivatedEventArgs();
    ExtendedActivationKind kind = args.Kind();
    if (kind == ExtendedActivationKind::Launch)
    {
        auto launchArgs = args.Data().as<LaunchActivatedEventArgs>();
        DoSomethingWithLaunchArgs(launchArgs.Arguments());
    }
    else if (kind == ExtendedActivationKind::File)
    {
        auto fileArgs = args.Data().as<FileActivatedEventArgs>();
        DoSomethingWithFileArgs(fileArgs.Files());
    }
    else if (kind == ExtendedActivationKind::Protocol)
    {
        auto protocolArgs = args.Data().as<ProtocolActivatedEventArgs>();
        DoSomethingWithProtocolArgs(protocolArgs.Uri());
    }
    else if (kind == ExtendedActivationKind::StartupTask)
    {
        auto startupArgs = args.Data().as<StartupTaskActivatedEventArgs>();
        DoSomethingWithStartupArgs(startupArgs.TaskId());
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
    // Unregister one or more registered filetypes.
    std::wstring imageFileTypes[3] = { L".jpg", L".png", L".bmp" };
    ActivationRegistrationManager::UnregisterForFileTypeActivation(
        imageFileTypes,
        L"C:\\Program Files\\Contoso\\MyApp.exe");

    // Unregister a protocol scheme.
    ActivationRegistrationManager::UnregisterForProtocolActivation(
        L"foo",
        L"");

    // Unregister for startup activation.
    ActivationRegistrationManager::UnregisterForStartupActivation(
        L"AnotherTaskId",
        L"");
}
```
