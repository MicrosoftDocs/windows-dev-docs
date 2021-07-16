---
description: Instancing models available in Windows.ApplicationModel and how to use them (Windows App SDK)
title: App instancing in AppLifecycle (Windows App SDK)
ms.topic: article
ms.date: 05/25/2021
keywords: AppLifecycle, Windows, ApplicationModel, instancing, single instance, multi instance
ms.author: hickeys
author: hickeys
ms.localizationpriority: medium
---

# App instancing in AppLifecycle

> [!IMPORTANT]
> AppLifecycle is an experimental feature that is currently supported only in the [experimental release channel](../experimental-channel.md) of the Windows App SDK. This feature is not supported for use by apps in production environments.

An app's instancing model determines whether multiple instances of your app's process can run at the same time.

### Single-instance apps

Apps are single-instanced if there can be only one main process running at a time. Attempting to launch a second instance of a single-instanced app typically results in the first instance's main window being activated instead. Note that this only applies to the main process. Single-instanced apps can create multiple background processes and still be considered single instanced.

UWP apps are single-instanced by default. but have the ability to become multi-instanced by deciding at launch-time whether to create an additional instance or activate an existing instance instead.

The Windows 10 Mail app is a good example of a single instanced app. When you launch Mail for the first time, a new window will be created. If you attempt to launch Mail again, the existing Mail window will be activated instead.

### Multi-instanced apps

Apps are multi-instanced if the main process can be run multiple times simultaneously. Attempting to launch a second instance of a multi-instanced app creates a new process and main window.

Traditionally, unpacked apps are multi-instanced by default, but can implement single-instancing when necessarily. Typically this is done using a single named mutex to indicate if an app is already running.

Notepad is a good example of a multi instanced app. Each time you attempt to launch Notepad, a new instance of Notepad will be created regardless of how many instances are already running.

## How the Windows App SDK instancing differs from UWP instancing

Instancing behavior in the Windows App SDK is based on UWP's model, class, but with some key differences:

### AppInstance class

- **UWP**: The [AppInstance](/uwp/api/windows.applicationmodel.appinstance) class is focused purely on instance redirection scenarios.
- **Windows App SDK**: The `AppInstance` class supports instance redirection scenarios, and contains additional functionality to support new features in later releases.

### List of Instances

- **UWP**: [GetInstances](/uwp/api/windows.applicationmodel.appinstance.getinstances) returns only the instances that the app explicitly registered for potential redirection.
- **Windows App SDK**: `GetInstances` returns all running instances of the app, including the current instance. Separate lists are maintained for different versions of the same app, as well as instances of apps launched by different users.

### Registering Keys

Each instance of a multi-instanced app can register an arbitrary key via the `FindOrRegisterForKey` API. Keys have no inherent meaning; apps can use keys in whatever form or way they wish.

An instance of an app can set its key at any time, but only one key is allowed for each instance; setting a new value overwrites the previous value.

An instance of an app cannot set its key to the same value that another instance has already registered. Attempting to register an existing key will result in `FindOrRegisterForKey` returning the app instance that has already registered that key.

- **UWP**: An instance must register a key in order to be included in the list returned from [GetInstances](/uwp/api/windows.applicationmodel.appinstance.getinstances).
- **Windows App SDK**: Registering a key is decoupled from the list of instances. An instance does not need to register a key in order to be included in the list.

### Unregistering keys

An instance of an app can unregister its key.

- **UWP**: When an instance unregisters its key, it is no longer available for activation redirection and is not included in the list of instances returned from [GetInstances](/uwp/api/windows.applicationmodel.appinstance.getinstances).
- **Windows App SDK**: An instance that has unregistered its key is still available for activation redirection and is still included in the list of instances returned from `GetInstances`.

### Instance Redirection Targets

Multiple instances of an app can activate each other, a process called "activation redirection". For example, an app might implement single instancing by only initializing itself if no other instances of the app are found at startup, and instead redirect and exit if another instance exists. Multi-instanced apps can redirect activations when appropriate according to that app's business logic. When an activation is redirected to another instance, it uses that instance's `Activated` callback, the same callback that's used in all other activation scenarios.

- **UWP**: Only instances that have registered a key can be a target for redirection.
- **Windows App SDK**: Any instance can be a redirection target, whether or not it has a registered key.

### Post-Redirection Behavior

- **UWP**: Redirection is a terminal operation; the app is terminated after redirecting the activation, even if the redirect failed.

- **Windows App SDK**: In the Windows App SDK, redirection is not a terminal operation. This in part reflects the potential problems in arbitrarily terminating a Win32 app that may have already allocated some memory, but also allows support of more sophisticated redirection scenarios. Consider a multi-instanced app where an instance receives an activation request while performing a large amount of CPU-intensive work. That app can redirect the activation request to another instance and continue its processing. That scenario would not be possible if the app was terminated after redirection.

An activation request can be redirected multiple times. Instance A could redirect to instance B, which could in turn redirect to instance C. Windows App SDK apps taking advantage of this functionality must guard against circular redirection - if C redirects to A in the example above, there is a potential infinite activation loop. It is up to the app to determine how to handle circular redirection depending on what makes sense for the workflows that app supports.

### Activation Events

In order to handle reactivation, the app can register for an Activated event.

- **UWP**: The event passes an [IActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.iactivatedeventargs) to the app.
- **Windows App SDK**: The event passes a `Microsoft.Windows.AppLifecycle.AppActivationArguments` instance to the app, which contains one of the `-ActivatedEventArgs` instances.

## Examples

### Handling activations

This example demonstrates how an app registers for and handles an `Activated` event. When it receives an `Activated` event, this app uses the event arguments to determine what sort of action caused the activation, and responds appropriately.

```cpp
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    // Initialize COM.
    winrt::init_apartment();

    // First, hook up the Activated event, to allow for this instance of the app
    // getting reactivated as a result of multi-instance redirection.
    Microsoft::ProjectReunion::AppInstance::GetCurrent().Activated([](
        AppActivationArguments const& args)
        { OnActivated(args); });

    //...etc - the rest of WinMain as normal.
}

void OnActivated(AppActivationArguments const& args)
{
    ExtendedActivationKind kind = args.Kind;
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

### Redirection logic based on activation kind

In this example, the app registers a handler for the `Activated` event, but then immediately checks for the activation event args in the `wWinMain` method instead of waiting for an `Activated` callback. This allows the app to implement a single-instance model for certain scenarios.

For most types of activations, the app continues with its regular initialization process. However, if the activation was caused by an associated file type being opened, and if another instance of this app already has the file opened, the current instance will redirect the activation to the existing instance and exit.

This app uses key registration to determine which files are open in which instances. When an instance opens a file, it registers a key that includes that filename. Other instances can then examine the registered keys and look for particular filenames, and register themselves as that file's instance if no other instance already has.

Note that, though key registration itself is part of the Windows App SDK's AppLifecycle API, the contents of the key are specified only within the app itself. An app does not need to register a file name, or any other meaningful data. This app, however, has decided to track open files via keys based on its particular needs and supported workflows.

```cpp
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    // Initialize COM.
    winrt::init_apartment();

    // First, we'll get our rich activation event args.
    AppActivationArguments activationArgs =
        AppInstance::GetCurrent().GetActivatedEventArgs();

    // An app might want to set itself up for possible redirection in
    // the case where it opens files - for example, to prevent multiple
    // instances from working on the same file.
    ExtendedActivationKind kind = activationArgs.Kind;
    if (kind == ExtendedActivationKind::File)
    {
        auto fileArgs = activationArgs.Data.as<FileActivatedEventArgs>();
        IStorageItem file = fileArgs.Files().GetAt(0);

        // Let's try to register this instance for this file.
        AppInstance instance =
            AppInstance::FindOrRegisterForKey(file.Name());
        if (instance.IsCurrent)
        {
            // If we successfully registered this instance, we can now just
            // go ahead and do normal initialization.
            RegisterClassAndStartMessagePump(hInstance, nCmdShow);
        }
        else
        {
            // Some other instance has already registered for this file,
            // so we'll redirect this activation to that instance instead.
            // This is an async operation: to ensure the target can get
            // the payload before this instance terminates, we should
            // wait for the call to complete.
            instance.RedirectActivationToAsync(activationArgs).get();
        }
    }
    return 1;
}
```

### Arbitrary redirection

This example expands on the previous example by adding more sophisticated redirection rules. The app still performs the open file check from the previous example. However, where the previous example would always create a new instance if it did not redirect based on the open file check, this example adds the concept of a "reusable" instance. If a reusable instance is found, the current instance redirects to the reusable instance and exits. Otherwise, it registers itself as reusable and continues with its normal initialization.

Again, note that the concept of a "reusable" instance does not exist in the AppLifecycle API; it is created and used only within the app itself.

```cpp
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    // Initialize COM.
    winrt::init_apartment();

    AppActivationArguments activationArgs =
        AppInstance::GetCurrent().GetActivatedEventArgs();

    // As above, check for any specific activation kind we care about.
    ExtendedActivationKind kind = activationArgs.Kind;
    if (kind == ExtendedActivationKind::File)
    {
        // etc... as in previous scenario.
    }
    else
    {
        // For other activation kinds, we'll trawl all instances to see if
        // any are suitable for redirecting this request. First, get a list
        // of all running instances of this app.
        auto instances = AppInstance::GetInstances();

        // In the simple case, we'll redirect to any other instance.
        AppInstance instance = instances.GetAt(0);

        // If the app re-registers re-usable instances, we can filter for these instead.
        // In this example, the app uses the string "REUSABLE" to indicate to itself
        // that it can redirect to a particular instance.
        bool isFound = false;
        for (AppInstance instance : instances)
        {
            if (instance.Key == L"REUSABLE")
            {
                isFound = true;
                instance.RedirectActivationToAsync(activationArgs).get();
                break;
            }
        }
        if (!isFound)
        {
            // We'll register this as a reusable instance, and then
            // go ahead and do normal initialization.
            winrt::hstring szKey = L"REUSABLE";
            AppInstance::FindOrRegisterForKey(szKey);
            RegisterClassAndStartMessagePump(hInstance, nCmdShow);
        }
    }
    return 1;
}
```

### Redirection orchestration

This example again adds more sophisticated redirection behavior. Here, an app instance can register itself as the instance that handles all activations of a specific kind. When an instance of an app receives a `Protocol` activation, it first checks for an instance that has already registered to handle `Protocol` activations. If it finds one, it redirects the activation to that instance. If not, the current instance registers itself for `Protocol` activations, and then applies additional logic (not shown) which may redirect the activation for some other reason.

```cpp
void OnActivated(AppActivationArguments const& args)
{
    const ExtendedActivationKind kind = args.Kind;

    // For example, we might want to redirect protocol activations.
    if (kind == ExtendedActivationKind::Protocol)
    {
        auto protocolArgs = args.Data().as<ProtocolActivatedEventArgs>();
        Uri uri = protocolArgs.Uri();

        // We'll try to find the instance that handles protocol activations.
        // If there isn't one, then this instance will take over that duty.
        auto instance = AppInstance::FindOrRegisterForKey(uri.AbsoluteUri());
        if (!instance.IsCurrent)
        {
            instance.RedirectActivationToAsync(args).get();
        }
        else
        {
            DoSomethingWithProtocolArgs(uri);
        }
    }
    else
    {
        // In this example, this instance of the app handles all other
        // activation kinds.
        DoSomethingWithNewActivationArgs(args);
    }
}
```

Unlike the UWP version of RedirectActivationTo, the Windows App SDK's version requires explicitly passing event arguments when redirecting activations. This is necessary because whereas UWP tightly controls activations and can ensure the correct activation arguments are passed to the correct instances, the Windows App SDK's version supports many platforms, and cannot rely on UWP-specific features. One benefit of this model is that apps that use the Windows App SDK have the chance to modify or replace the arguments that will be passed to the target instance.

### Unregister for redirection

Apps that have registered a key can unregister that key at any time. This example assumes the current instance had previously registered a key indicating that it had a specific file opened, meaning subsequent attempts to open that file would be redirected to it. When that file is closed, the key that contains the filename must be deleted.

```cpp
void CALLBACK OnFileClosed(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    AppInstance::GetCurrent().UnregisterKey();
}
```

> [!Warning]
> Although keys are automatically unregistered when their process terminates, race conditions are possible where another instance may have initiated a redirection to the terminated instance before the terminated instance was unregistered. To mitigate this possibility, an app can use `UnregisterKey` to manually unregister its key before it is terminated, giving the app a chance to redirect activations to another app that is not in the process of exiting.

### Instance information

The `AppInstance` class represents a single instance of an app. In this preview, `AppInstance` only includes the methods and properties necessary to support activation redirection. In later releases, `AppInstance` will expand to include other methods and properties relevant to an app instance.

```cpp
void DumpExistingInstances()
{
    for (AppInstance const& instance : AppInstance::GetInstances())
    {
        std::wostringstream sStream;
        sStream << L"Instance: ProcessId = " << instance.ProcessId
            << L", Key = " << instance.Key().c_str() << std::endl;
        ::OutputDebugString(sStream.str().c_str());
    }
}
```
