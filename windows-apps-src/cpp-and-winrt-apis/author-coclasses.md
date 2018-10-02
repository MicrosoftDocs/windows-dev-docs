---
author: stevewhims
description: C++/WinRT can help you to author classic COM components, just as it helps you to author Windows Runtime classes.
title: Author COM components with C++/WinRT
ms.author: stwhi
ms.date: 09/06/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, author, COM, component
ms.localizationpriority: medium
---

# Author COM components with C++/WinRT

[C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) can help you to author classic Component Object Model (COM) components (or coclasses), just as it helps you to author Windows Runtime classes. Here's a simple illustration, which you can test out if you paste the code into the `pch.h` and `main.cpp` of a new **Windows Console Application (C++/WinRT)** project.

```cppwinrt
// pch.h
#pragma once
#include <unknwn.h>
#include <winrt/Windows.Foundation.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"

struct __declspec(uuid("ddc36e02-18ac-47c4-ae17-d420eece2281")) IMyComInterface : ::IUnknown
{
    virtual HRESULT __stdcall Call() = 0;
};

using namespace winrt;
using namespace Windows::Foundation;

int main()
{
    winrt::init_apartment();

    struct MyCoclass : winrt::implements<MyCoclass, IPersist, IStringable, IMyComInterface>
    {
        HRESULT __stdcall Call() noexcept override
        {
            return S_OK;
        }

        HRESULT __stdcall GetClassID(CLSID* id) noexcept override
        {
            *id = IID_IPersist; // Doesn't matter what we return, for this example.
            return S_OK;
        }

        winrt::hstring ToString()
        {
            return L"MyCoclass as a string";
        }
    };

    auto mycoclass_instance{ winrt::make<MyCoclass>() };
    CLSID id{};
    winrt::check_hresult(mycoclass_instance->GetClassID(&id));
    winrt::check_hresult(mycoclass_instance.as<IMyComInterface>()->Call());
}
```

Also see [Consume COM components with C++/WinRT](consume-com.md).

## A more realistic and interesting example

The remainder of this topic walks through creating a minimal console application project that uses C++/WinRT to implement a basic coclass (COM component, or COM class) and class factory. The example application shows how to deliver a toast notification with a callback button on it, and the coclass (which implements the **INotificationActivationCallback** COM interface) allows the application to be launched and called back when the user clicks that button on the toast.

More background about the toast notification feature area can be found at [Send a local toast notification](/windows/uwp/design/shell/tiles-and-notifications/send-local-toast). None of the code examples in that section of the documentation use C++/WinRT, though, so we recommend that you prefer the code shown in this topic.

## Create a Windows Console Application project (ToastAndCallback)

Begin by creating a new project in Microsoft Visual Studio. Create a **Visual C++** > **Windows Desktop** > **Windows Console Application (C++/WinRT)** project, and name it *ToastAndCallback*.

Open `pch.h`, and add `#include <unknwn.h>` before the includes for any C++/WinRT headers.

```cppwinrt
// pch.h
#pragma once
#include <unknwn.h>
#include <winrt/Windows.Foundation.h>
```

Open `main.cpp`, and remove the using-directives that the project template generates. In their place, paste the following code (which gives us the libs, headers, and type names that we need).

```cppwinrt
#pragma comment(lib, "shell32")

#include <iomanip>
#include <iostream>
#include <notificationactivationcallback.h>
#include <propkey.h>
#include <propvarutil.h>
#include <shlobj.h>
#include <winrt/Windows.UI.Notifications.h>
#include <winrt/Windows.Data.Xml.Dom.h>

using namespace winrt;
using namespace Windows::Data::Xml::Dom;
using namespace Windows::UI::Notifications;
```

## Implement the coclass and class factory

In C++/WinRT, you implement coclasses, and class factories, by deriving from the [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) base struct. Immediately after the three using-directives shown above (and before `main`), paste this code to implement your toast notification COM activator component.

```cppwinrt
static constexpr GUID callback_guid // BAF2FA85-E121-4CC9-A942-CE335B6F917F
{
    0xBAF2FA85, 0xE121, 0x4CC9, {0xA9, 0x42, 0xCE, 0x33, 0x5B, 0x6F, 0x91, 0x7F}
};

std::wstring const this_app_name{ L"ToastAndCallback" };

struct callback : winrt::implements<callback, INotificationActivationCallback>
{
	HRESULT __stdcall Activate(
		LPCWSTR app,
		LPCWSTR args,
		[[maybe_unused]] NOTIFICATION_USER_INPUT_DATA const* data,
		[[maybe_unused]] ULONG count) noexcept final
	{
		try
		{
			std::wcout << this_app_name << L" has been called back from a notification." << std::endl;
			std::wcout << L"Value of the 'app' parameter is '" << app << L"'." << std::endl;
			std::wcout << L"Value of the 'args' parameter is '" << args << L"'." << std::endl;
            return S_OK;
        }
		catch (...)
		{
            return winrt::to_hresult();
		}
	}
};

struct callback_factory : implements<callback_factory, IClassFactory>
{
    HRESULT __stdcall CreateInstance(
        IUnknown* outer,
        GUID const& iid,
        void** result) noexcept final
    {
        *result = nullptr;

        if (outer)
        {
            return CLASS_E_NOAGGREGATION;
        }

        return make<callback>()->QueryInterface(iid, result);
    }

    HRESULT __stdcall LockServer(BOOL) noexcept final
    {
        return S_OK;
    }
};
```

The implementation of the coclass above follows the same pattern that's demonstrated in [Author APIs with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/author-apis#if-youre-not-authoring-a-runtime-class). So, you can use the same technique to implement COM interfaces as well as Windows Runtime interfaces. COM components and Windows Runtime classes expose their features via interfaces. Every COM interface ultimately derives from the [**IUnknown interface**](https://msdn.microsoft.com/library/windows/desktop/ms680509) interface. The Windows Runtime is based on COM&mdash;one distinction being that Windows Runtime interfaces ultimately derive from the [**IInspectable interface**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) (and **IInspectable** derives from **IUnknown**).

In the coclass in the code above, we implement the **INotificationActivationCallback::Activate** method, which is the function that's called when the user clicks the callback button on a toast notification. But before that function can be called, an instance of the coclass needs to be created, and that's the job of the **IClassFactory::CreateInstance** function.

The coclass that we just implemented is known as the *COM activator* for notifications, and it has its class id (CLSID) in the form of the `callback_guid` identifier (of type **GUID**) that you see above. We'll be using that identifier later, in the form of a Start menu shortcut and a Windows Registry entry. The COM activator CLSID, and the path to its associated COM server (which is the path to the executable that we're building here) is the mechanism by which a toast notification knows what class to create an instance of when its callback button is clicked (whether the notification is clicked in Action Center or not).

## Best practices for implementing COM methods

Techniques for error handling and for resource management can go hand-in-hand. It's more convenient and practical to use exceptions than error codes. And if you employ the resource-acquisition-is-initialization (RAII) idiom, then you can avoid explicitly checking for error codes and then explicitly releasing resources. Such explicit checks make your code more convoluted than necessary, and it gives bugs plenty of places to hide. Instead, use RAII, and throw/catch exceptions. That way, your resource allocations are exception-safe, and your code is simple.

However, you mustn't allow exceptions to escape your COM method implementations. You can ensure that by using the `noexcept` specifier on your COM methods. It's ok for exceptions to be thrown anywhere in the call graph of your method, as long as you handle them before your method exits. If you use `noexcept`, but you then allow an exception to escape your method, then your application will terminate.

## Add helper types and functions

In this step, we'll add some helper types and functions that the rest of the code makes use of. So, before `main`, add the following.

```cppwinrt
struct prop_variant : PROPVARIANT
{
    prop_variant() noexcept : PROPVARIANT{}
    {
    }

    ~prop_variant() noexcept
    {
        clear();
    }

    void clear() noexcept
    {
        WINRT_VERIFY_(S_OK, ::PropVariantClear(this));
    }
};

struct registry_traits
{
    using type = HKEY;

    static void close(type value) noexcept
    {
        WINRT_VERIFY_(ERROR_SUCCESS, ::RegCloseKey(value));
    }

    static constexpr type invalid() noexcept
    {
        return nullptr;
    }
};

using registry_key = winrt::handle_type<registry_traits>;

std::wstring get_module_path()
{
    std::wstring path(100, L'?');
    uint32_t path_size{};
    DWORD actual_size{};

    do
    {
        path_size = static_cast<uint32_t>(path.size());
        actual_size = ::GetModuleFileName(nullptr, path.data(), path_size);

        if (actual_size + 1 > path_size)
        {
            path.resize(path_size * 2, L'?');
        }
    } while (actual_size + 1 > path_size);

    path.resize(actual_size);
    return path;
}

std::wstring get_shortcut_path()
{
    std::wstring format{ LR"(%ProgramData%\Microsoft\Windows\Start Menu\Programs\)" };
    format += (this_app_name + L".lnk");

    auto required{ ::ExpandEnvironmentStrings(format.c_str(), nullptr, 0) };
    std::wstring path(required - 1, L'?');
    ::ExpandEnvironmentStrings(format.c_str(), path.data(), required);
    return path;
}
```

## Implement the remaining functions, and the wmain entry point function

The project template generates a `main` function for you. Delete that `main` function, and in its place paste this code listing, which includes code to register your coclass, and then to deliver a toast capable of calling back your application.

```cppwinrt
void register_callback()
{
    DWORD registration{};

    winrt::check_hresult(::CoRegisterClassObject(
        callback_guid,
        make<callback_factory>().get(),
        CLSCTX_LOCAL_SERVER,
        REGCLS_SINGLEUSE,
        &registration));
}

void create_shortcut()
{
    auto link{ winrt::create_instance<IShellLink>(CLSID_ShellLink) };
    std::wstring module_path{ get_module_path() };
    winrt::check_hresult(link->SetPath(module_path.c_str()));

    auto store = link.as<IPropertyStore>();
    prop_variant value;
    winrt::check_hresult(::InitPropVariantFromString(this_app_name.c_str(), &value));
    winrt::check_hresult(store->SetValue(PKEY_AppUserModel_ID, value));
    value.clear();
    winrt::check_hresult(::InitPropVariantFromCLSID(callback_guid, &value));
    winrt::check_hresult(store->SetValue(PKEY_AppUserModel_ToastActivatorCLSID, value));

    auto file{ store.as<IPersistFile>() };
    std::wstring shortcut_path{ get_shortcut_path() };
    winrt::check_hresult(file->Save(shortcut_path.c_str(), TRUE));

    std::wcout << L"In " << shortcut_path << L", created a shortcut to " << module_path << std::endl;
}

void update_registry()
{
    std::wstring key_path{ LR"(SOFTWARE\Classes\CLSID\{????????-????-????-????-????????????})" };
    ::StringFromGUID2(callback_guid, key_path.data() + 23, 39);
    key_path += LR"(\LocalServer32)";
    registry_key key;

    winrt::check_win32(::RegCreateKeyEx(
        HKEY_CURRENT_USER,
        key_path.c_str(),
        0,
        nullptr,
        0,
        KEY_WRITE,
        nullptr,
        key.put(),
        nullptr));
    ::RegDeleteValue(key.get(), nullptr);

    std::wstring path{ get_module_path() };

    winrt::check_win32(::RegSetValueEx(
        key.get(),
        nullptr,
        0,
        REG_SZ,
        reinterpret_cast<BYTE const*>(path.c_str()),
        static_cast<uint32_t>((path.size() + 1) * sizeof(wchar_t))));

    std::wcout << L"In " << key_path << L", registered local server at " << path << std::endl;
}

void create_toast()
{
    XmlDocument xml;

    std::wstring toastPayload
    {
        LR"(
<toast>
  <visual>
    <binding template='ToastGeneric'>
      <text>)"
    };
    toastPayload += this_app_name;
    toastPayload += LR"(
      </text>
    </binding>
  </visual>
  <actions>
    <action content='Call back )";
    toastPayload += this_app_name;
    toastPayload += LR"(
' arguments='the_args' activationKind='Foreground' />
  </actions>
</toast>)";
    xml.LoadXml(toastPayload);

    ToastNotification toast{ xml };
    ToastNotifier notifier{ ToastNotificationManager::CreateToastNotifier(this_app_name) };
    notifier.Show(toast);
}

void LaunchedNormally(HANDLE, INPUT_RECORD &, DWORD &);
void LaunchedFromNotification(HANDLE, INPUT_RECORD &, DWORD &);

int wmain(int argc, wchar_t * argv[], wchar_t * /* envp */[])
{
    winrt::init_apartment();

    register_callback();

    HANDLE consoleHandle{ ::GetStdHandle(STD_INPUT_HANDLE) };
    INPUT_RECORD buffer{};
    DWORD events{};
    ::FlushConsoleInputBuffer(consoleHandle);

    if (argc == 1)
    {
        LaunchedNormally(consoleHandle, buffer, events);
    }
    else if (argc == 2 && wcscmp(argv[1], L"-Embedding") == 0)
    {
        LaunchedFromNotification(consoleHandle, buffer, events);
    }
}

void LaunchedNormally(HANDLE consoleHandle, INPUT_RECORD & buffer, DWORD & events)
{
    try
    {
        bool runningAsAdmin{ ::IsUserAnAdmin() == TRUE };
        std::wcout << this_app_name << L" is running" << (runningAsAdmin ? L" (Administrator)." : L".") << std::endl;

        if (runningAsAdmin)
        {
            create_shortcut();
            update_registry();
        }

        std::wcout << std::endl << L"Press 'T' to display a toast notification (press any other key to exit)." << std::endl;

        ::ReadConsoleInput(consoleHandle, &buffer, 1, &events);
        if (towupper(buffer.Event.KeyEvent.uChar.UnicodeChar) == L'T')
        {
            create_toast();
        }
    }
    catch (winrt::hresult_error const& e)
    {
        std::wcout << L"Error: " << e.message().c_str() << L" (" << std::hex << std::showbase << std::setw(8) << static_cast<uint32_t>(e.code()) << L")" << std::endl;
    }
}

void LaunchedFromNotification(HANDLE consoleHandle, INPUT_RECORD & buffer, DWORD & events)
{
    ::Sleep(50); // Give the callback chance to display its message.
    std::wcout << std::endl << L"Press any key to exit." << std::endl;
    ::ReadConsoleInput(consoleHandle, &buffer, 1, &events);
}
```

## How to test the example application

Build the application, and then run it at least once as Administrator to cause the registration, and other setup, code to run. Whether or not you're running it as Administrator, then press 'T' to cause a toast to be displayed. You can then click the **Call back ToastAndCallback** button either directly from the toast notification that pops up, or from the Action Center, and your application will be launched, the coclass instantiated, and the **INotificationActivationCallback::Activate** method executed.

## In-process COM server

The *ToastAndCallback* example app above functions as a local (or out-of-process) COM server. This is indicated by the [LocalServer32](/windows/desktop/com/localserver32) Windows Registry key that you use to register it. A local COM server hosts its coclass(es) inside an executable binary (an `.exe`).

Alternatively (and arguably more likely), you can choose to host your coclass(es) inside a dynamic-link library (a `.dll`). A COM server in the form of a DLL is known as an in-process COM server, and it's indicated by being registered by using the [InprocServer32](/windows/desktop/com/inprocserver32) Windows Registry key.

### Create a Dynamic-Link Library (DLL) project

You can begin the task of creating an in-process COM server by creating a new project in Microsoft Visual Studio. Create a **Visual C++** > **Windows Desktop** > **Dynamic-Link Library (DLL)** project.

### Set project properties

Go to project property **General** \> **Windows SDK Version**, and select **All Configurations** and **All Platforms**. Set **Windows SDK Version** to *10.0.17134.0 (Windows 10, version 1803)*, or later.

To add Visual Studio support for C++/WinRT to your project, edit your `.vcxproj` file, find `<PropertyGroup Label="Globals">` and, inside that property group, set the property `<CppWinRTEnabled>true</CppWinRTEnabled>`.

Because C++/WinRT uses features from the C++17 standard, set project property **C/C++** > **Language** > **C++ Language Standard** to *ISO C++17 Standard (/std:c++17)*.

### The precompiled header

Rename your `stdafx.h` and `stdafx.cpp` to `pch.h` and `pch.cpp`, respectively. Set project property **C/C++** > **Precompiled Headers** > **Precompiled Header File** to *pch.h*.

Find and replace all `#include "stdafx.h"` with `#include "pch.h"`.

In `pch.h`, include `winrt/base.h`.

```cppwinrt
// pch.h
...
#include <winrt/base.h>
```

Confirm that you're not affected by [Why won't my new project compile?](/windows/uwp/cpp-and-winrt-apis/faq).

### Implement the coclass, class factory, and in-proc server exports

Open `dllmain.cpp`, and add to it the code listing shown below.

If you already have a DLL that implements C++/WinRT Windows Runtime classes, then you'll already have the **DllCanUnloadNow** function shown below. If you want to add coclasses to that DLL, then you can add the **DllGetClassObject** function.

If don't have existing [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) code that you want to stay compatible with, then you can remove the WRL parts from the code shown.

```cppwinrt
// dllmain.cpp

struct MyCoclass : winrt::implements<MyCoclass, IPersist>
{
    HRESULT STDMETHODCALLTYPE GetClassID(CLSID* id) noexcept override
    {
        *id = IID_IPersist; // Doesn't matter what we return, for this example.
        return S_OK;
    }
};

struct __declspec(uuid("85d6672d-0606-4389-a50a-356ce7bded09"))
    MyCoclassFactory : winrt::implements<MyCoclassFactory, IClassFactory>
{
    HRESULT STDMETHODCALLTYPE CreateInstance(IUnknown *pUnkOuter, REFIID riid, void **ppvObject) noexcept override
    {
        try
        {
            *ppvObject = winrt::make<MyCoclass>().get();
            return S_OK;
        }
        catch (...)
        {
            return winrt::to_hresult();
        }
    }

    HRESULT STDMETHODCALLTYPE LockServer(BOOL fLock) noexcept override
    {
        // ...
        return S_OK;
    }

    // ...
};

HRESULT __stdcall DllCanUnloadNow()
{
#ifdef _WRL_MODULE_H_
    if (!::Microsoft::WRL::Module<::Microsoft::WRL::InProc>::GetModule().Terminate())
    {
        return S_FALSE;
    }
#endif

    if (winrt::get_module_lock())
    {
        return S_FALSE;
    }

    winrt::clear_factory_cache();
    return S_OK;
}

HRESULT __stdcall DllGetClassObject(GUID const& clsid, GUID const& iid, void** result)
{
    try
    {
        *result = nullptr;

        if (clsid == __uuidof(MyCoclassFactory))
        {
            return winrt::make<MyCoclassFactory>()->QueryInterface(iid, result);
        }

#ifdef _WRL_MODULE_H_
        return ::Microsoft::WRL::Module<::Microsoft::WRL::InProc>::GetModule().GetClassObject(clsid, iid, result);
#else
        return winrt::hresult_class_not_available().to_abi();
#endif
    }
    catch (...)
    {
        return winrt::to_hresult();
    }
}
```

### Support for weak references

Also see [Weak references in C++/WinRT](weak-references.md#weak-references-in-cwinrt).

C++/WinRT (specifically, the [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) base struct template) implements [**IWeakReferenceSource**](/windows/desktop/api/weakreference/nn-weakreference-iweakreferencesource) for you if your type implements [**IInspectable**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) (or any interface that derives from **IInspectable**).

This is because **IWeakReferenceSource** and [**IWeakReference**](/windows/desktop/api/weakreference/nn-weakreference-iweakreference) are designed for Windows Runtime types. So, you can turn on weak reference support for your coclass simply by adding **winrt::Windows::Foundation::IInspectable** (or an interface that derives from **IInspectable**) to your implementation.

```cppwinrt
struct MyCoclass : winrt::implements<MyCoclass, IMyComInterface, winrt::Windows::Foundation::IInspectable>
{
    //  ...
};
```

## Important APIs
* [IInspectable interface](/windows/desktop/api/inspectable/nn-inspectable-iinspectable)
* [IUnknown interface](https://msdn.microsoft.com/library/windows/desktop/ms680509)
* [winrt::implements struct template](/uwp/cpp-ref-for-winrt/implements)

## Related topics
* [Author APIs with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/author-apis)
* [Consume COM components with C++/WinRT](consume-com.md)
* [Send a local toast notification](/windows/uwp/design/shell/tiles-and-notifications/send-local-toast)
