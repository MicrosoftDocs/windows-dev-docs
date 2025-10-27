---
title: Accelerate warm launches of your Windows app
description: Learn how to make your already-running Windows app respond to URI and tile launches faster with an experimental API. Reduce launch latency and improve performance.
ms.date: 10/22/2025
ms.topic: how-to
keywords: windows 11
ms.localizationpriority: low
# customer-intent: As a Windows developer, I want to learn how to speed up how quickly my app responds to URI and other launches when it is already running in the background.
---

# Register your application for accelerated warm launches

This article describes how to accelerate warm launches for single-instance Windows applications. Learn how to register a running process instance to be directly notified when a launch occurs, avoiding overhead associated with the full launch flow. This approach reduces overall latency by eliminating the intermediary process that unifies with already-running instances.

*Typical flow today:*

:::image type="content" source="images/accelerated-launch-normal.png" alt-text="Screenshot of flow diagram showing normal warm-launch pathway with intermediary process.":::

*With an accelerated launch:*

:::image type="content" source="images/accelerated-launch-fast.png" alt-text="Screenshot of flow diagram showing accelerated warm-launch pathway without intermediary process.":::

This accelerated launch functionality doesn't cover every kind of launch, from every source application, on every version of Windows. As a result, your application must still maintain its own implementation of single-instancing (see [App instancing with the app lifecycle API](/windows/apps/windows-app-sdk/applifecycle/applifecycle-instancing) for one approach).

The rest of this article walks through creating a simple C++ WinUI 3 app that supports accelerated warm Start Menu and Protocol launches.

> [!IMPORTANT]
> The accelerated launch APIs are part of a Limited Access Feature (see [LimitedAccessFeatures class](/uwp/api/windows.applicationmodel.limitedaccessfeatures)). For more information or to request an unlock token, use the [LAF Access Token Request Form](https://go.microsoft.com/fwlink/?linkid=2271232&clcid=0x409).

## Create a new project

In Visual Studio, create a new project. For this example, in the **Create a new project** dialog, set the language filter to C++ and the project type to WinUI, then select **WinUI Blank App (Packaged)**. If you don't see this template, make sure you have the **WinUI application development** workload installed in the Visual Studio Installer.

## Implement single-instancing

You can use any technique of your choice to implement single-instancing within your app. In this example, use the [Windows App SDK’s App Lifecycle APIs](/windows/apps/windows-app-sdk/applifecycle/applifecycle-instancing).

``` cpp
void App::OnLaunched([[maybe_unused]] LaunchActivatedEventArgs const& e)
{
    AppInstance mainInstance = AppInstance::FindOrRegisterForKey(L"SingleInstance");
    if (mainInstance.IsCurrent())
    {
        // We're the main instance. Register to receive redirected launches.
        EventHandler<AppActivationArguments> handler(this, &App::OnRedirectedActivation);
        g_activatedRevoker = mainInstance.Activated(winrt::auto_revoke, handler);
        // Proceed with creating the main window.
        window = make<MainWindow>();
        window.Activate();

        window.SetMessage(L"Launched normally");
    }
    else
    {
        // We are a secondary instance. Redirect to the main instance and terminate when we're done.
        wil::unique_event redirectionComplete;
        redirectionComplete.create();

        auto redirectionOperation = mainInstance.RedirectActivationToAsync(mainInstance.GetActivatedEventArgs());
        redirectionOperation.Completed([&redirectionComplete](auto&&, auto&&)
        {
            redirectionComplete.SetEvent();
        });

        DWORD indexIgnored{};
        CoWaitForMultipleObjects(CWMO_DEFAULT, INFINITE, 1, redirectionComplete.addressof(), &indexIgnored);
        App::Exit();
    }
}

winrt::fire_and_forget App::OnRedirectedActivation(const IInspectable&, const AppActivationArguments& args)
{
    co_await wil::resume_foreground(window.DispatcherQueue());
    ::SwitchToThisWindow(GetWindowFromWindowId(window.AppWindow().Id()), TRUE);
    
    window.SetMessage(L"Launched via redirection");
}
```

## Add a protocol association for the app

To receive accelerated protocol launches, your app must be the default target of a protocol registration. In this sample, use a packaged registration. For more information about how to register as the default handler for a URI scheme, see [Handle URI activation](handle-uri-activation.md).

In your `Package.appxmanifest`, add an entry for a protocol association:

``` xml
<Applications>
  <Application Id="App" … />
    …
    <Extensions>
      <uap:Extension Category="windows.protocol">
        <uap:Protocol Name="acceleratedlaunchsample"/>
      </uap:Extension>
    </Extensions>
  </Application>
</Applications>
```

Your app can now be launched via its tile in the Start Menu or by typing `acceleratedlaunchsample:` into the Run dialog. In both cases, an intermediary process launches and redirects to the already-running instance.

## Register for accelerated launches

Now the app is ready to opt in to the optimizations.

The accelerated launch messages are delivered as window messages to an HWND within your process. You tell the system which window should receive the messages, and which messages you want to use, with the interface `IExperimentalAPIInvoker` which is implemented by `CLSID_ExperimentalAPIInvoker`.

`IExperimentalAPIInvoker` and `CLSID_ExperimentalAPIInvoker` are defined as follows:

``` cpp
[uuid("6fb3e48c-ba77-4f80-b9c9-77635a08a7f8")]
interface DECLSPEC_NOVTABLE IExperimentalAPIInvoker : public IUnknown
{
    IFACEMETHOD(IsSupported)(PCWSTR methodName, BOOL* isSupported) = 0;
    IFACEMETHOD(Invoke)(PCWSTR methodName, INamedPropertyStore* args, BOOL* isSupported) = 0;
};

DEFINE_GUID(CLSID_ExperimentalAPIInvoker, 0x81AF2611, 0xE262, 0x4090, 0xA1, 0x5B, 0xC0, 0x88, 0x95, 0xB1, 0xA0, 0xF0);
```

`IExperimentalAPIInvoker` allows you to call methods by name with arguments packaged in a named property store. The methods for accelerated launch are:

### Method: “RegisterAcceleratedUriLaunch”

Register for protocol launches delivered as a null-terminated UTF-16 string (PCWSTR) via WM_COPYDATA to the target HWND.

You can call this method only once per `IExperimentalAPIInvoker` object instance. Once you register, don't release the object until after you unregister.

| Parameter name | Type | Description |
|-|-|-|
| `"targetWindow"` | `VT_I8` | The HWND, cast to a signed LONGLONG, that receives a window message when your app is launched for its primary tile in the Start Menu or other locations. |
| `"copyDataFormatId"` | `VT_UI4` | Value that's passed as the COPYDATASTRUCT.dwData when delivering the URI string. |
| `"schemes"` | `VT_LPWSTR \| VT_VECTOR` | List of URI schemes to register for. Only those that the calling process is the default handler for are accelerated. |

### Method: “UnregisterAcceleratedUriLaunch”

Unregister from URI launches. No arguments. Must be called on the same object that was previously registered with.

### Method: “RegisterAcceleratedTileLaunch”

Register for tile launches delivered by a chosen window message. You can call this method only once for each `IExperimentalAPIInvoker` object instance. After you register, don't release the object until you unregister.

| Parameter name | Type | Description |
|-|-|-|
| `"targetWindow"` | `VT_I8` | The HWND, cast to a signed LONGLONG, that receives a window message when your app is launched for its primary tile in the Start Menu or other locations. |
| `"messageId"` | `VT_UI4` | The window message ID that goes to the specified target window. |

### Method: “UnregisterAcceleratedTileLaunch”

Unregister from tile launches. This method takes no arguments. You must call it on the same object that you previously registered with.

### Putting it together

To simplify the preceding steps, especially for apps that don't have easy access to an existing window procedure, this sample provides a self-contained helper that manages a worker window and registers it for accelerated launches. You must create this object on a thread with an active message pump (such as the UI thread of a WinUI app).

First, copy the following code into a header file in your project:

``` cpp
#pragma once
#include <propsys.h>
#include <propvarutil.h>
#include <initguid.h>
#include <vector>
#include <functional>
#include <wil/result.h>
#include <wil/resource.h>

[uuid("6fb3e48c-ba77-4f80-b9c9-77635a08a7f8")]
interface DECLSPEC_NOVTABLE IExperimentalAPIInvoker : public IUnknown
{
    IFACEMETHOD(IsSupported)(PCWSTR methodName, BOOL* isSupported) = 0;
    IFACEMETHOD(Invoke)(PCWSTR methodName, INamedPropertyStore* args, BOOL* isSupported) = 0;
};

DEFINE_GUID(CLSID_ExperimentalAPIInvoker, 0x81AF2611, 0xE262, 0x4090, 0xA1, 0x5B, 0xC0, 0x88, 0x95, 0xB1, 0xA0, 0xF0);

class AcceleratedLaunchHelper
{
public:
    ~AcceleratedLaunchHelper()
    {
        if (m_hwnd != nullptr)
        {
            FAIL_FAST_IF(::GetWindowThreadProcessId(m_hwnd, nullptr) != ::GetCurrentThreadId());

            if (m_tileLaunchCallback)
            {
                BOOL isSupported;
                m_invoker->Invoke(L"UnregisterAcceleratedTileLaunch", nullptr, &isSupported);
            }

            if (m_uriLaunchCallback)
            {
                BOOL isSupported;
                m_invoker->Invoke(L"UnregisterAcceleratedUriLaunch", nullptr, &isSupported);
            }

            ::DestroyWindow(m_hwnd);
        }
    }

    inline bool TryRegisterForUris(
        const std::vector<PCWSTR>& uris, std::function<void(PCWSTR)> callback)
    {
        EnsureWorkerWindow();

        if (!m_invoker)
        {
            return false;
        }

        winrt::com_ptr<INamedPropertyStore> args;
        THROW_IF_FAILED(PSCreateMemoryPropertyStore(IID_PPV_ARGS(&args)));

        wil::unique_prop_variant targetWindowParam;
        THROW_IF_FAILED(InitPropVariantFromInt64(reinterpret_cast<LONGLONG>(m_hwnd), targetWindowParam.reset_and_addressof()));
        args->SetNamedValue(L"targetWindow", targetWindowParam);

        wil::unique_prop_variant copyDataFormatIdParam;
        THROW_IF_FAILED(InitPropVariantFromUInt32(0,
            copyDataFormatIdParam.reset_and_addressof()));
        args->SetNamedValue(L"copyDataFormatId", copyDataFormatIdParam);

        wil::unique_prop_variant schemesParam;
        THROW_IF_FAILED(InitPropVariantFromStringVector(
            const_cast<PCWSTR*>(uris.data()), uris.size(), schemesParam.reset_and_addressof()));
        THROW_IF_FAILED(args->SetNamedValue(L"schemes", schemesParam));

        BOOL isSupported{ FALSE };
        THROW_IF_FAILED(m_invoker->Invoke(L"RegisterAcceleratedUriLaunch", args.get(),
            &isSupported));

        if (isSupported)
        {
            m_uriLaunchCallback = callback;
            m_uriSchemes = uris;
        }

        return isSupported;
    }

    inline bool TryRegisterForTileLaunch(std::function<void()> callback)
    {
        EnsureWorkerWindow();

        if (!m_invoker)
        {
            return false;
        }

        winrt::com_ptr<INamedPropertyStore> args;
        THROW_IF_FAILED(PSCreateMemoryPropertyStore(IID_PPV_ARGS(&args)));

        wil::unique_prop_variant targetWindowParam;
        THROW_IF_FAILED(InitPropVariantFromInt64(reinterpret_cast<LONGLONG>(m_hwnd), targetWindowParam.reset_and_addressof()));
        args->SetNamedValue(L"targetWindow", targetWindowParam);

        wil::unique_prop_variant tileLaunchMessageIdParam;
        THROW_IF_FAILED(InitPropVariantFromUInt32(WM_USER,
            tileLaunchMessageIdParam.reset_and_addressof()));
        THROW_IF_FAILED(args->SetNamedValue(L"messageId", tileLaunchMessageIdParam));

        BOOL isSupported{ FALSE };
        THROW_IF_FAILED(m_invoker->Invoke(L"RegisterAcceleratedTileLaunch", args.get(),
            &isSupported));
    
        if (isSupported)
        {
            m_tileLaunchCallback = callback;
        }

        return isSupported;
    }

private:
    static inline LRESULT s_WorkerWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
    {
        switch (msg)
        {
        case WM_NCCREATE:
        {
            auto pThis = reinterpret_cast<AcceleratedLaunchHelper*>(
                (reinterpret_cast<CREATESTRUCT*>(lParam))->lpCreateParams);
            if (pThis)
            {
                ::SetWindowLongPtr(hwnd, 0, reinterpret_cast<LONG_PTR>(pThis));
            }
            break;
        }
        }

        auto pThis = reinterpret_cast<AcceleratedLaunchHelper*>(GetWindowLongPtr(hwnd, 0));
        if (pThis)
        {
            return pThis->WorkerWndProc(hwnd, msg, wParam, lParam);
        }

        return 0;
    }

    inline LRESULT WorkerWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
    {
        switch (msg)
        {
        case WM_COPYDATA:
        {
            const auto copyData = reinterpret_cast<COPYDATASTRUCT*>(lParam);
            if (copyData->dwData == 0)
            {
                const auto uri = reinterpret_cast<PCWSTR>(copyData->lpData);
                if (m_uriLaunchCallback)
                {
                    m_uriLaunchCallback(uri);
                }
            }
        }
        break;

        case WM_USER:
            if (m_tileLaunchCallback)
            {
                m_tileLaunchCallback();
            }
            break;
        }
        return DefWindowProc(hwnd, msg, wParam, lParam);
    }

    inline void EnsureWorkerWindow()
    {
        if (m_hwnd != nullptr)
        {
            return;
        }

        // Best-effort: Try to acquire the IExperimentalAPIInvoker.
        // If we can't, the OS does not currently support the feature.
        if (FAILED(CoCreateInstance(CLSID_ExperimentalAPIInvoker, nullptr, CLSCTX_INPROC_SERVER,
            IID_PPV_ARGS(&m_invoker)))
        {
            return;
        }

        WNDCLASSEX wcex{ sizeof(wcex) };
        wcex.lpszClassName = L"AcceleratedLaunchWorkerClass";
        wcex.cbWndExtra = sizeof(this);
        wcex.lpfnWndProc = s_WorkerWndProc;
        wcex.hInstance = GetModuleHandle(nullptr);
        ::RegisterClassEx(&wcex);

        m_hwnd = ::CreateWindowEx(0, wcex.lpszClassName, nullptr, 0, 0, 0, 0, 0,
            HWND_MESSAGE, 0, wcex.hInstance, this);

        FAIL_FAST_LAST_ERROR_IF_NULL(m_hwnd);
    }

    // Worker window for receiving notifications
    HWND m_hwnd{ nullptr };

    // Entry point for registration & unregistration
    winrt::com_ptr<IExperimentalAPIInvoker> m_invoker;

    // Callbacks
    std::function<void()> m_tileLaunchCallback;
    std::function<void(PCWSTR)> m_uriLaunchCallback;

    // Registered URI schemes
    std::vector<PCWSTR> m_uriSchemes;
};
```

Now you can integrate this code into your app:

``` cpp
void App::OnLaunched([[maybe_unused]] LaunchActivatedEventArgs const& e)
{
    …
    if (mainInstance.IsCurrent())
    {
        …

        m_acceleratedLaunchHelper = std::make_unique<AcceleratedLaunchHelper>();
        m_acceleratedLaunchHelper->TryRegisterForTileLaunch(
            [this]() { OnAcceleratedTileLaunch(); });

        std::vector<PCWSTR> schemes = { L"acceleratedlaunchsample" };
        m_acceleratedLaunchHelper->TryRegisterForUris(schemes,
            [this](PCWSTR uri) { OnAcceleratedUriLaunch(uri); });
    }
    …
}

void App::OnAcceleratedTileLaunch()
{
    ::SwitchToThisWindow(GetWindowFromWindowId(window.AppWindow().Id()), TRUE);

    window.SetMessage(L"Launched via accelerated tile launch.");
}

void App::OnAcceleratedUriLaunch(PCWSTR uri)
{
    ::SwitchToThisWindow(GetWindowFromWindowId(window.AppWindow().Id()), TRUE);

    std::wstring message = L"Launched via accelerated URI launch: ";
    message += uri;
    window.SetMessage(message);
}
```

With this code, your app benefits from accelerated launches on OS versions that support this feature.

## Related content

[Handle URI activation](handle-uri-activation.md)

[App instancing with the app lifecycle API](/windows/apps/windows-app-sdk/applifecycle/applifecycle-instancing)

[Windows App SDK’s App Lifecycle APIs](/windows/apps/windows-app-sdk/applifecycle/applifecycle-instancing)
