---
description: Learn how to detect Windows focus session state and respond to changes in a WinUI 3 app, with API checks and event cleanup.
title: Detect and react to focus session state
ms.topic: how-to
ms.date: 10/23/2026
author: GrantMeStrength
ms.author: jken
keywords: AppLifecycle, Windows, ApplicationModel, focus
ms.localizationpriority: medium
---

# Detect and react to focus session state

The Focus feature in Windows 11 helps users minimize distractions. By default, a focus session turns on Do not disturb and hides flashing and badges for apps on the taskbar. Users can change these options in **Settings > System > Focus**. For more information, see [Focus: stay on task without distractions in Windows](https://support.microsoft.com/en-us/windows/experience/focus-stay-on-task-without-distractions-in-windows).

This article shows you how to use [FocusSessionManager](/uwp/api/windows.ui.shell.focussessionmanager) to detect an active focus session and respond to state changes. The focus session state isn't the same as the Do not disturb state: Do not disturb can be enabled independently and can remain on after a focus session ends.

## Prerequisites

The examples are code fragments for a WinUI 3 app built with [Windows SDK 10.0.26100.0 or later](../../windows-sdk/index.md). **FocusSessionManager** is a Windows Runtime API in **Windows.UI.Shell**, not a Windows App SDK API. Keep that namespace when you use it from WinUI 3.

If your app runs on Windows versions that don't expose this API, check [ApiInformation.IsTypePresent](/uwp/api/windows.foundation.metadata.apiinformation.istypepresent) before accessing **FocusSessionManager**. Then check **IsSupported** before getting a manager instance. The presence of the Focus feature in Windows 11 doesn't guarantee that this API is available.

## Get the current Focus session state

After checking API availability and [IsSupported](/uwp/api/windows.ui.shell.focussessionmanager.issupported), call **GetDefault** and read [IsFocusActive](/uwp/api/windows.ui.shell.focussessionmanager.isfocusactive). Call the following helper on the UI thread. It assumes your page contains a text block named **StatusTextBlock**.

#### [C#](#tab/csharp)
```csharp
private void UpdateStatusBar(string message)
{
    var focusActive = false;
    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(
            "Windows.UI.Shell.FocusSessionManager") &&
        Windows.UI.Shell.FocusSessionManager.IsSupported)
    {
        var manager = Windows.UI.Shell.FocusSessionManager.GetDefault();
        focusActive = manager.IsFocusActive;
    }

    if (!focusActive)
    {
        StatusTextBlock.Text = message;
    }
}
```

#### [C++/WinRT](#tab/cppwinrt)
```cpp
// pch.h
...
#include <winrt/Windows.UI.Shell.h>
#include <winrt/Windows.Foundation.Metadata.h>


// MainPage.xaml.h
...
void UpdateStatusBar(winrt::hstring const& message);

// MainPage.xaml.cpp
...
void MainPage::UpdateStatusBar(winrt::hstring const& message)
{
    auto focusActive = false;
    if (Windows::Foundation::Metadata::ApiInformation::IsTypePresent(
            L"Windows.UI.Shell.FocusSessionManager") &&
        Windows::UI::Shell::FocusSessionManager::IsSupported())
    {
        auto manager = Windows::UI::Shell::FocusSessionManager::GetDefault();
        focusActive = manager.IsFocusActive();
    }

    if (!focusActive)
    {
        StatusTextBlock().Text(message);
    }
}
```

---

## Continuously monitor focus state

Register for [IsFocusActiveChanged](/uwp/api/windows.ui.shell.focussessionmanager.isfocusactivechanged) to receive state changes. Marshal UI work through your page's **DispatcherQueue**, and unsubscribe when you leave the page. The following fragments belong to a WinUI 3 **Page**, not a **Window**, because navigation callbacks are page methods.

In these examples, **SetAnimatedGifAutoPlay** is a helper you implement to enable or disable animated GIF playback. Read the initial state after subscribing, and read it again on the UI thread when an event arrives.

#### [C#](#tab/csharp)
```csharp
// MainPage.xaml.cs
private Windows.UI.Shell.FocusSessionManager? _focusSessionManager;

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    base.OnNavigatedTo(e);

    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(
            "Windows.UI.Shell.FocusSessionManager") &&
        Windows.UI.Shell.FocusSessionManager.IsSupported)
    {
        _focusSessionManager = Windows.UI.Shell.FocusSessionManager.GetDefault();
        _focusSessionManager.IsFocusActiveChanged += Manager_IsFocusActiveChanged;
        SetAnimatedGifAutoPlay(!_focusSessionManager.IsFocusActive);
    }
}

protected override void OnNavigatedFrom(NavigationEventArgs e)
{
    if (_focusSessionManager is not null)
    {
        _focusSessionManager.IsFocusActiveChanged -= Manager_IsFocusActiveChanged;
        _focusSessionManager = null;
    }

    base.OnNavigatedFrom(e);
}

private void Manager_IsFocusActiveChanged(Windows.UI.Shell.FocusSessionManager sender, object args)
{
    if (!DispatcherQueue.TryEnqueue(() =>
        {
            if (_focusSessionManager is not null)
            {
                SetAnimatedGifAutoPlay(!_focusSessionManager.IsFocusActive);
            }
        }))
    {
        System.Diagnostics.Debug.WriteLine("Focus update wasn't queued because the dispatcher is shutting down.");
    }
}
```

#### [C++/WinRT](#tab/cppwinrt)
```cpp
// pch.h
...
#include <winrt/Windows.UI.Shell.h>
#include <winrt/Windows.Foundation.Metadata.h>
#include <winrt/Microsoft.UI.Xaml.Navigation.h>
#include <winrt/Microsoft.UI.Dispatching.h>

// MainPage.xaml.h
...
void OnNavigatedTo(Microsoft::UI::Xaml::Navigation::NavigationEventArgs const&);
void OnNavigatedFrom(Microsoft::UI::Xaml::Navigation::NavigationEventArgs const&);

Windows::UI::Shell::FocusSessionManager m_focusSessionManager{ nullptr };
Windows::UI::Shell::FocusSessionManager::IsFocusActiveChanged_revoker m_focusStateChangedRevoker;

void OnFocusStateChanged(Windows::UI::Shell::FocusSessionManager const& sender, Windows::Foundation::IInspectable const&);

// MainPage.xaml.cpp
...
void MainPage::OnNavigatedTo(Microsoft::UI::Xaml::Navigation::NavigationEventArgs const&)
{
    if (Windows::Foundation::Metadata::ApiInformation::IsTypePresent(
            L"Windows.UI.Shell.FocusSessionManager") &&
        Windows::UI::Shell::FocusSessionManager::IsSupported())
    {
        m_focusSessionManager = Windows::UI::Shell::FocusSessionManager::GetDefault();
        m_focusStateChangedRevoker = m_focusSessionManager.IsFocusActiveChanged(
            winrt::auto_revoke, { get_weak(), &MainPage::OnFocusStateChanged });

        SetAnimatedGifAutoPlay(!m_focusSessionManager.IsFocusActive());
    }
}

void MainPage::OnNavigatedFrom(Microsoft::UI::Xaml::Navigation::NavigationEventArgs const&)
{
    m_focusStateChangedRevoker.revoke();
    m_focusSessionManager = nullptr;
}

void MainPage::OnFocusStateChanged(Windows::UI::Shell::FocusSessionManager const&,
        Windows::Foundation::IInspectable const&)
{
    auto weakThis = get_weak();
    if (!DispatcherQueue().TryEnqueue([weakThis]()
        {
            if (auto page = weakThis.get(); page && page->m_focusSessionManager)
            {
                page->SetAnimatedGifAutoPlay(!page->m_focusSessionManager.IsFocusActive());
            }
        }))
    {
        ::OutputDebugStringW(L"Focus update wasn't queued because the dispatcher is shutting down.\n");
    }
}
```

---

For C#, add `using Microsoft.UI.Xaml.Navigation;` for **NavigationEventArgs**. If you monitor focus state from a window instead of a page, subscribe after initializing the window and unsubscribe in its **Closed** handler.

## Starting or ending focus sessions

Detecting focus state doesn't grant permission to control sessions. [TryStartFocusSession](/uwp/api/windows.ui.shell.focussessionmanager.trystartfocussession) and [DeactivateFocus](/uwp/api/windows.ui.shell.focussessionmanager.deactivatefocus) are Limited Access Feature APIs. You must obtain approval and an unlock token before using them. For details, see [LimitedAccessFeatures](/uwp/api/windows.applicationmodel.limitedaccessfeatures) and the access-token request link in the API reference.
