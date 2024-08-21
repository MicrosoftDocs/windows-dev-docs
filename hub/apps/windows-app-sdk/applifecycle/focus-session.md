---
description: Describes how to detect and react to the current focus session state.
title: Detect and react to focus session state
ms.topic: article
ms.date: 07/18/2022
keywords: AppLifecycle, Windows, ApplicationModel, focus
ms.localizationpriority: medium
---

# Detect and react to focus session state

Windows 11 introduced the Focus feature which helps users minimize distractions by turning on Do Not Disturb and silencing icon flashing and badge notifications for apps in the taskbar. This article shows you how to use the [FocusSessionManager](/uwp/api/windows.ui.shell.focussessionmanager) API to detect whether a Focus session is currently active or receive updates when the Focus session state changes, allowing you to customize your app's behavior to minimize distractions when a Focus session is active. For more information on the Focus feature, see [How to use focus in Windows 11](https://support.microsoft.com/en-us/windows/how-to-use-focus-in-windows-11-cbcc9ddb-8164-43fa-8919-b9a2af072382).

## Get the current Focus session state

Before accessing the properties of the **FocusSessionManager**, make sure it's supported on the current device by checking the [IsSupported](/uwp/api/windows.ui.shell.focussessionmanager.issupported) property. After verifying that the feature is supported you can determine if a Focus session is currently active by checking the [IsFocusActive](/uwp/api/windows.ui.shell.focussessionmanager.isfocusactive) property.

#### [C#](#tab/csharp)
```csharp
private void UpdateStatusBar(string message)
{
    var focusActive = false;
    if (Windows.UI.Shell.FocusSessionManager.IsSupported)
    {
        var manager = Windows.UI.Shell.FocusSessionManager.GetDefault();
        focusActive = manager.IsFocusActive;
    }

    if (!focusActive)
    {
        statusTextBlock.Text = message;
    }
}
```

#### [C++/WinRT](#tab/cppwinrt)
```cpp
// pch.h
...
#include <winrt/Windows.UI.Shell.h>


// MainWindow.xaml.h
...
Windows::UI::Shell::FocusSessionManager m_focusSessionManager = Windows::UI::Shell::FocusSessionManager::GetDefault();
void UpdateStatusBar(winrt::hstring const& message);

// MainWindow.xaml.cpp
...
void MainWindow::UpdateStatusBar(winrt::hstring const& message)
{
    auto focusActive = false;
    if (Windows::UI::Shell::FocusSessionManager::IsSupported())
    {
        focusActive = m_focusSessionManager.IsFocusActive();
    }

    if (!focusActive)
    {
        StatusTextBlock().Text(message);
    }
}
```

---

## Continuously monitor focus state

You can subscribe to be notified when the Focus session state on the device changes by registering for the [IsFocusActiveChanged](/uwp/api/windows.ui.shell.focussessionmanager.isfocusactivechanged) event. In the following example **SetAnimatedGifAutoPlay** is an app-implemented helper method that changes whether the app auto-plays animated gifs based on the current Focus session state.

#### [C#](#tab/csharp)
```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    if (Windows.UI.Shell.FocusSessionManager.IsSupported)
    {
        var manager = Windows.UI.Shell.FocusSessionManager.GetDefault();
        manager.IsFocusActiveChanged += Manager_IsFocusActiveChanged;
        SetAnimatedGifAutoPlay(true);
    }
}

private void Manager_IsFocusActiveChanged(Windows.UI.Shell.FocusSessionManager sender, object args)
{
    if(sender.IsFocusActive)
    {
        SetAnimatedGifAutoPlay(true);
    }
    else
    {
        SetAnimatedGifAutoPlay(false);
    }
}
```

#### [C++/WinRT](#tab/cppwinrt)
```cpp
// pch.h
...
#include <winrt/Windows.UI.Shell.h
#include <winrt/Windows.UI.Xaml.Navigation.h>

// MainWindow.xaml.h
...
void OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs const&);

Windows::UI::Shell::FocusSessionManager m_focusSessionManager = Windows::UI::Shell::FocusSessionManager::GetDefault();
winrt::event_token m_focusStateChangedToken;

void OnFocusStateChanged(Windows::UI::Shell::FocusSessionManager const& sender, Windows::Foundation::IInspectable const&);

// MainWindow.xaml.cpp
...
void MainWindow::OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs const&)
{
    if (Windows::UI::Shell::FocusSessionManager::IsSupported())
    {

        m_focusStateChangedToken = m_focusSessionManager.IsFocusActiveChanged(
            { get_weak(), &MainWindow::OnFocusStateChanged });

        SetAnimatedGifAutoPlay(true);
    }
}

void MainWindow::OnFocusStateChanged(Windows::UI::Shell::FocusSessionManager const& sender,
        Windows::Foundation::IInspectable const&)
{
    auto temp = m_focusSessionManager.IsFocusActive();
    SetAnimatedGifAutoPlay(m_focusSessionManager.IsFocusActive());
}
```

---
