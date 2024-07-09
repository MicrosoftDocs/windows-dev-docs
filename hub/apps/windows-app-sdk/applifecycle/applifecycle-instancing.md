---
description: Describes how to use app instancing features with the app lifecycle API (Windows App SDK).
title: App instancing with the app lifecycle API (Windows App SDK)
ms.topic: article
ms.date: 03/07/2024
keywords: AppLifecycle, Windows, ApplicationModel, instancing, single instance, multi instance
ms.localizationpriority: medium
---

# App instancing with the app lifecycle API

An app's instancing model determines whether multiple instances of your app's process can run at the same time.

## Prerequisites

To use the app lifecycle API in the Windows App SDK:

1. Download and install the latest release of the Windows App SDK. For more information, see [Get started with WinUI](../../get-started/start-here.md).
2. Follow the instructions to [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

## Single-instance apps

> [!NOTE]
> For an example of how to implement single instancing in a WinUI 3 app with C#, see [Making the app single-instanced](https://blogs.windows.com/windowsdeveloper/2022/01/28/making-the-app-single-instanced-part-3/) on the Windows developer blog.

Apps are single-instanced if there can be only one main process running at a time. Attempting to launch a second instance of a single-instanced app typically results in the first instance's main window being activated instead. Note that this only applies to the main process. Single-instanced apps can create multiple background processes and still be considered single instanced.

UWP apps are single-instanced by default. but have the ability to become multi-instanced by deciding at launch-time whether to create an additional instance or activate an existing instance instead.

The Windows Mail app is a good example of a single instanced app. When you launch Mail for the first time, a new window will be created. If you attempt to launch Mail again, the existing Mail window will be activated instead.

## Multi-instanced apps

Apps are multi-instanced if the main process can be run multiple times simultaneously. Attempting to launch a second instance of a multi-instanced app creates a new process and main window.

Traditionally, unpacked apps are multi-instanced by default, but can implement single-instancing when necessarily. Typically this is done using a single named mutex to indicate if an app is already running.

Notepad is a good example of a multi instanced app. Each time you attempt to launch Notepad, a new instance of Notepad will be created regardless of how many instances are already running.

## How the Windows App SDK instancing differs from UWP instancing

Instancing behavior in the Windows App SDK is based on UWP's model, class, but with some key differences:

### AppInstance class

- **UWP**: The [Windows.ApplicationModel.AppInstance](/uwp/api/windows.applicationmodel.appinstance) class is focused purely on instance redirection scenarios.
- **Windows App SDK**: The [Microsoft.Windows.AppLifeycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance) class supports instance redirection scenarios, and contains additional functionality to support new features in later releases.

### List of instances

- **UWP**: [GetInstances](/uwp/api/windows.applicationmodel.appinstance.getinstances) returns only the instances that the app explicitly registered for potential redirection.
- **Windows App SDK**: [GetInstances](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getinstances) returns all running instances of the app that are using the AppInstance API, whether or not they have registered a key. This can include the current instance. If you want the current instance to be included in the list, call `AppInstance.GetCurrent`. Separate lists are maintained for different versions of the same app, as well as instances of apps launched by different users.

### Registering keys

Each instance of a multi-instanced app can register an arbitrary key via the `FindOrRegisterForKey` method. Keys have no inherent meaning; apps can use keys in whatever form or way they wish.

An instance of an app can set its key at any time, but only one key is allowed for each instance; setting a new value overwrites the previous value.

An instance of an app cannot set its key to the same value that another instance has already registered. Attempting to register an existing key will result in `FindOrRegisterForKey` returning the app instance that has already registered that key.

- **UWP**: An instance must register a key in order to be included in the list returned from [GetInstances](/uwp/api/windows.applicationmodel.appinstance.getinstances).
- **Windows App SDK**: Registering a key is decoupled from the list of instances. An instance does not need to register a key in order to be included in the list.

### Unregistering keys

An instance of an app can unregister its key.

- **UWP**: When an instance unregisters its key, it is no longer available for activation redirection and is not included in the list of instances returned from [GetInstances](/uwp/api/windows.applicationmodel.appinstance.getinstances).
- **Windows App SDK**: An instance that has unregistered its key is still available for activation redirection and is still included in the list of instances returned from [GetInstances](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getinstances).

### Instance redirection targets

Multiple instances of an app can activate each other, a process called "activation redirection". For example, an app might implement single instancing by only initializing itself if no other instances of the app are found at startup, and instead redirect and exit if another instance exists. Multi-instanced apps can redirect activations when appropriate according to that app's business logic. When an activation is redirected to another instance, it uses that instance's `Activated` callback, the same callback that's used in all other activation scenarios.

- **UWP**: Only instances that have registered a key can be a target for redirection.
- **Windows App SDK**: Any instance can be a redirection target, whether or not it has a registered key.

### Post-redirection behavior

- **UWP**: Redirection is a terminal operation; the app is terminated after redirecting the activation, even if the redirect failed.

- **Windows App SDK**: In the Windows App SDK, redirection is not a terminal operation. This in part reflects the potential problems in arbitrarily terminating a Win32 app that may have already allocated some memory, but also allows support of more sophisticated redirection scenarios. Consider a multi-instanced app where an instance receives an activation request while performing a large amount of CPU-intensive work. That app can redirect the activation request to another instance and continue its processing. That scenario would not be possible if the app was terminated after redirection.

An activation request can be redirected multiple times. Instance A could redirect to instance B, which could in turn redirect to instance C. Windows App SDK apps taking advantage of this functionality must guard against circular redirection - if C redirects to A in the example above, there is a potential infinite activation loop. It is up to the app to determine how to handle circular redirection depending on what makes sense for the workflows that app supports.

### Activation events

In order to handle reactivation, the app can register for an Activated event.

- **UWP**: The event passes an [IActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.iactivatedeventargs) to the app.
- **Windows App SDK**: The event passes a [Microsoft.Windows.AppLifecycle.AppActivationArguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments) instance to the app, which contains one of the `-ActivatedEventArgs` instances.

## Examples

### Handling activations

This example demonstrates how an app registers for and handles an `Activated` event. When it receives an `Activated` event, this app uses the event arguments to determine what sort of action caused the activation, and responds appropriately.

```cpp
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // Initialize the Windows App SDK framework package for unpackaged apps.
    HRESULT hr{ MddBootstrapInitialize(majorMinorVersion, versionTag, minVersion) };
    if (FAILED(hr))
    {
        OutputFormattedDebugString(
            L"Error 0x%X in MddBootstrapInitialize(0x%08X, %s, %hu.%hu.%hu.%hu)\n",
            hr, majorMinorVersion, versionTag, 
            minVersion.Major, minVersion.Minor, minVersion.Build, minVersion.Revision);
        return hr;
    }

    if (DecideRedirection())
    {
        return 1;
    }

    // Connect the Activated event, to allow for this instance of the app
    // getting reactivated as a result of multi-instance redirection.
    AppInstance thisInstance = AppInstance::GetCurrent();
    auto activationToken = thisInstance.Activated(
        auto_revoke, [&thisInstance](
            const auto& sender, const AppActivationArguments& args)
        { OnActivated(sender, args); }
    );

    // Carry on with regular Windows initialization.
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

    MddBootstrapShutdown();
    return (int)msg.wParam;
}

void OnActivated(const IInspectable&, const AppActivationArguments& args)
{
    int const arraysize = 4096;
    WCHAR szTmp[arraysize];
    size_t cbTmp = arraysize * sizeof(WCHAR);
    StringCbPrintf(szTmp, cbTmp, L"OnActivated (%d)", activationCount++);

    ExtendedActivationKind kind = args.Kind();
    if (kind == ExtendedActivationKind::Launch)
    {
        ReportLaunchArgs(szTmp, args);
    }
    else if (kind == ExtendedActivationKind::File)
    {
        ReportFileArgs(szTmp, args);
    }
}
```

### Redirection logic based on activation kind

In this example, the app registers a handler for the [Activated](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.activated) event, and also checks for the activation event args to decide whether to redirect activation to another instance.

For most types of activations, the app continues with its regular initialization process. However, if the activation was caused by an associated file type being opened, and if another instance of this app already has the file opened, the current instance will redirect the activation to the existing instance and exit.

This app uses key registration to determine which files are open in which instances. When an instance opens a file, it registers a key that includes that filename. Other instances can then examine the registered keys and look for particular filenames, and register themselves as that file's instance if no other instance already has.

Note that, though key registration itself is part of the app lifecycle API in the Windows App SDK's, the contents of the key are specified only within the app itself. An app does not need to register a file name, or any other meaningful data. This app, however, has decided to track open files via keys based on its particular needs and supported workflows.

```cpp
bool DecideRedirection()
{
    // Get the current executable filesystem path, so we can
    // use it later in registering for activation kinds.
    GetModuleFileName(NULL, szExePath, MAX_PATH);
    wcscpy_s(szExePathAndIconIndex, szExePath);
    wcscat_s(szExePathAndIconIndex, L",1");

    // Find out what kind of activation this is.
    AppActivationArguments args = AppInstance::GetCurrent().GetActivatedEventArgs();
    ExtendedActivationKind kind = args.Kind();
    if (kind == ExtendedActivationKind::Launch)
    {
        ReportLaunchArgs(L"WinMain", args);
    }
    else if (kind == ExtendedActivationKind::File)
    {
        ReportFileArgs(L"WinMain", args);

        try
        {
            // This is a file activation: here we'll get the file information,
            // and register the file name as our instance key.
            IFileActivatedEventArgs fileArgs = args.Data().as<IFileActivatedEventArgs>();
            if (fileArgs != NULL)
            {
                IStorageItem file = fileArgs.Files().GetAt(0);
                AppInstance keyInstance = AppInstance::FindOrRegisterForKey(file.Name());
                OutputFormattedMessage(
                    L"Registered key = %ls", keyInstance.Key().c_str());

                // If we successfully registered the file name, we must be the
                // only instance running that was activated for this file.
                if (keyInstance.IsCurrent())
                {
                    // Report successful file name key registration.
                    OutputFormattedMessage(
                        L"IsCurrent=true; registered this instance for %ls",
                        file.Name().c_str());
                }
                else
                {
                    keyInstance.RedirectActivationToAsync(args).get();
                    return true;
                }
            }
        }
        catch (...)
        {
            OutputErrorString(L"Error getting instance information");
        }
    }
    return false;
}
```

### Arbitrary redirection

This example expands on the previous example by adding more sophisticated redirection rules. The app still performs the open file check from the previous example. However, where the previous example would always create a new instance if it did not redirect based on the open file check, this example adds the concept of a "reusable" instance. If a reusable instance is found, the current instance redirects to the reusable instance and exits. Otherwise, it registers itself as reusable and continues with its normal initialization.

Again, note that the concept of a "reusable" instance does not exist in the app lifecycle API; it is created and used only within the app itself.

```cpp
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    // Initialize COM.
    winrt::init_apartment();

    AppActivationArguments activationArgs =
        AppInstance::GetCurrent().GetActivatedEventArgs();

    // Check for any specific activation kind we care about.
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
void OnActivated(const IInspectable&, const AppActivationArguments& args)
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

Unlike the UWP version of `RedirectActivationTo`, the Windows App SDK's implementation of [RedirectActivationToAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.redirectactivationtoasync) requires explicitly passing event arguments when redirecting activations. This is necessary because whereas UWP tightly controls activations and can ensure the correct activation arguments are passed to the correct instances, the Windows App SDK's version supports many platforms, and cannot rely on UWP-specific features. One benefit of this model is that apps that use the Windows App SDK have the chance to modify or replace the arguments that will be passed to the target instance.

### Redirection without blocking

Most apps will want to redirect as early as possible, before doing unnecessary initialization work. For some app types, initialization logic runs on an STA thread, which must not be blocked. AppInstance.RedirectActivationToAsync method is asynchronous, and the calling app must wait for the method to complete, otherwise the redirection will fail. However, waiting on an async call will block the STA. In these situations, call RedirectActivationToAsync in another thread, and set an event when the call completes. Then wait on that event using non-blocking APIs such as CoWaitForMultipleObjects. Here’s a C# sample for a WPF app.

```csharp
private static bool DecideRedirection()
{
    bool isRedirect = false;

    // Find out what kind of activation this is.
    AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
    ExtendedActivationKind kind = args.Kind;
    if (kind == ExtendedActivationKind.File)
    {
        try
        {
            // This is a file activation: here we'll get the file information,
            // and register the file name as our instance key.
            if (args.Data is IFileActivatedEventArgs fileArgs)
            {
                IStorageItem file = fileArgs.Files[0];
                AppInstance keyInstance = AppInstance.FindOrRegisterForKey(file.Name);

                // If we successfully registered the file name, we must be the
                // only instance running that was activated for this file.
                if (keyInstance.IsCurrent)
                {
                    // Hook up the Activated event, to allow for this instance of the app
                    // getting reactivated as a result of multi-instance redirection.
                    keyInstance.Activated += OnActivated;
                }
                else
                {
                    isRedirect = true;

                    // Ensure we don't block the STA, by doing the redirect operation
                    // in another thread, and using an event to signal when it has completed.
                    redirectEventHandle = CreateEvent(IntPtr.Zero, true, false, null);
                    if (redirectEventHandle != IntPtr.Zero)
                    {
                        Task.Run(() =>
                        {
                            keyInstance.RedirectActivationToAsync(args).AsTask().Wait();
                            SetEvent(redirectEventHandle);
                        });
                        uint CWMO_DEFAULT = 0;
                        uint INFINITE = 0xFFFFFFFF;
                        _ = CoWaitForMultipleObjects(
                            CWMO_DEFAULT, INFINITE, 1, 
                            new IntPtr[] { redirectEventHandle }, out uint handleIndex);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting instance information: {ex.Message}");
        }
    }

    return isRedirect;
}
```

### Unregister for redirection

Apps that have registered a key can unregister that key at any time. This example assumes the current instance had previously registered a key indicating that it had a specific file opened, meaning subsequent attempts to open that file would be redirected to it. When that file is closed, the key that contains the filename must be deleted.

```cpp
void CALLBACK OnFileClosed(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    AppInstance::GetCurrent().UnregisterKey();
}
```

> [!WARNING]
> Although keys are automatically unregistered when their process terminates, race conditions are possible where another instance may have initiated a redirection to the terminated instance before the terminated instance was unregistered. To mitigate this possibility, an app can use [UnregisterKey](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.unregisterkey) to manually unregister its key before it is terminated, giving the app a chance to redirect activations to another app that is not in the process of exiting.

### Instance information

The [Microsoft.Windows.AppLifeycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance) class represents a single instance of an app. In the current preview, `AppInstance` only includes the methods and properties necessary to support activation redirection. In later releases, `AppInstance` will expand to include other methods and properties relevant to an app instance.

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
