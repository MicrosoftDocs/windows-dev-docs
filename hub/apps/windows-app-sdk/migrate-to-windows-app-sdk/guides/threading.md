---
title: Threading functionality migration
description: This topic describes how to migrate the threading code in a Universal Windows Platform (UWP) application to the Windows App SDK.
ms.topic: article
ms.date: 03/29/2023
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, windowing, reentrancy
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# Threading functionality migration

This topic describes how to migrate the threading code in a Universal Windows Platform (UWP) application to the Windows App SDK.

## Summary of API and/or feature differences

UWP's threading model is a variation of the single-threaded apartment (STA) model called Application STA (ASTA), which blocks reentrancy and helps avoid various reentrancy bugs and deadlocks. An ASTA thread is also known as a UI thread.

The Windows App SDK uses a standard STA threading model, which doesn't provide the same reentrancy safeguards.

The **CoreDispatcher** type migrates to **DispatcherQueue**. And the **CoreDispatcher.RunAsync** method migrates to **DispatcherQueue.TryEnqueue**.

**C++/WinRT**. If you're using **winrt::resume_foreground** with **CoreDispatcher**, then migrate that to use **DispatcherQueue** instead.

## ASTA to STA threading model

For more detail on the ASTA threading model, see the blog post [What is so special about the Application STA?](https://devblogs.microsoft.com/oldnewthing/20210224-00/?p=104901).

As the Windows App SDK's STA threading model doesn't have the same assurances around preventing reentrancy issues, if your UWP app assumes the non re-entrant behavior of the ASTA threading model, then your code might not behave as expected.

One thing to watch out for is reentrancy into XAML controls (see the example in [A Windows App SDK migration of the UWP Photo Editor sample app (C++/WinRT)](../case-study-2.md)). And for some crashes, such as access violations, the direct crash callstack is usually the right stack to use. But if it's a *stowed exception* crash&mdash;which has exception code: 0xc000027b&mdash;then more work is required to get the right callstack.

### Stowed exceptions

Stowed exception crashes save away a possible error, and that gets used later if no part of the code handles the exception. XAML sometimes decides the error is fatal immediately, in which case the direct crash stack might be good. But more frequently the stack has unwound before it was determined to be fatal. For more details about stowed exceptions, see the Inside Show episode [Stowed Exception C000027B](/shows/inside/c000027b).

For stowed exception crashes (to see a nested message pump, or to see the XAML control's specific exception being thrown), you can get more info on the crash by loading a crash dump in the Windows Debugger (WinDbg) (see [Download debugging tools for Windows](/windows-hardware/drivers/debugger/debugger-download-tools)), and then using `!pde.dse` to dump the stowed exceptions.

The PDE debugger extension (for the `!pde.dse` command) is available by downloading the [PDE*.zip](https://onedrive.live.com/?authkey=%21AJeSzeiu8SQ7T4w&id=DAE128BD454CF957%217152&cid=DAE128BD454CF957) file from OneDrive. Put the appropiate x64 or x86 `.dll` from that zip file into the `winext` directory of your WinDbg install, and then `!pde.dse` will work on stowed exception crash dumps.

Frequently there'll be multiple stowed exceptions, with some at the end that were handled/ignored. Most commonly, the first stowed exception is the interesting one. In some cases, the first stowed exception might be a re-throw of the second, so if the second stowed exception shows deeper into the same stack as the first, then the second exception might be the origination of the error. The error code shown with each stowed exception is also valuable, since that provides the **HRESULT** associated with that exception.

## Change Windows.UI.Core.CoreDispatcher to Microsoft.UI.Dispatching.DispatcherQueue

This section applies if you're using the [**Windows.UI.Core.CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) class in your UWP app. That includes the use of any methods or properties that take or return a **CoreDispatcher**, such as the [**DependencyObject.Dispatcher**](/uwp/api/windows.ui.xaml.dependencyobject.dispatcher) and [**CoreWindow.Dispatcher**](/uwp/api/windows.ui.core.corewindow.dispatcher) properties. For example, you'll be calling **DependencyObject.Dispatcher** when you retrieve the **CoreDispatcher** belonging to a [**Windows.UI.Xaml.Controls.Page**](/uwp/api/windows.ui.xaml.controls.page).

```csharp
// MainPage.xaml.cs in a UWP app
if (this.Dispatcher.HasThreadAccess)
{
    ...
}
```

```cppwinrt
// MainPage.xaml.cpp in a UWP app
if (this->Dispatcher().HasThreadAccess())
{
    ...
}
```

Instead, in your Windows App SDK app, you'll need to use the [**Microsoft.UI.Dispatching.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) class. And the corresponding methods or properties that take or return a **DispatcherQueue**, such as the [**DependencyObject.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyobject.dispatcherqueue) and [**Microsoft.UI.Xaml.Window.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcherqueue) properties. For example, you'll be calling **DependencyObject.DispatcherQueue** when you retrieve the **DispatcherQueue** belonging to a [**Microsoft.UI.Xaml.Controls.Page**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) (most XAML objects are **DependencyObject**s).

```csharp
// MainPage.xaml.cs in a Windows App SDK app
if (this.DispatcherQueue.HasThreadAccess)
{
    ...
}
```

```cppwinrt
// MainPage.xaml.cpp in a Windows App SDK app
#include <winrt/Microsoft.UI.Dispatching.h>
...
if (this->DispatcherQueue().HasThreadAccess())
{
    ...
}
```

## Change CoreDispatcher.RunAsync to DispatcherQueue.TryEnqueue

This section applies if you're using the [**Windows.UI.Core.CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) method to schedule a task to run on the main UI thread (or on the thread associated with a particular [**Windows.UI.Core.CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher)).

```csharp
// MainPage.xaml.cs in a UWP app
public void NotifyUser(string strMessage)
{
    if (this.Dispatcher.HasThreadAccess)
    {
        StatusBlock.Text = strMessage;
    }
    else
    {
        var task = this.Dispatcher.RunAsync(
            Windows.UI.Core.CoreDispatcherPriority.Normal,
            () => StatusBlock.Text = strMessage);
    }
}
```

```cppwinrt
// MainPage.cpp in a UWP app
void MainPage::NotifyUser(std::wstring strMessage)
{
    if (this->Dispatcher().HasThreadAccess())
    {
        StatusBlock().Text(strMessage);
    }
    else
    {
        auto task = this->Dispatcher().RunAsync(
            Windows::UI::Core::CoreDispatcherPriority::Normal,
            [strMessage, this]()
            {
                StatusBlock().Text(strMessage);
            });
    }
}
```

In your Windows App SDK app, use the [Microsoft.UI.Dispatching.DispatcherQueue.TryEnqueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue)) method instead. It adds to the [**Microsoft.UI.Dispatching.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) a task that will be executed on the thread associated with the **DispatcherQueue**.

```csharp
// MainPage.xaml.cs in a Windows App SDK app
public void NotifyUser(string strMessage)
{
    if (this.DispatcherQueue.HasThreadAccess)
    {
        StatusBlock.Text = strMessage;
    }
    else
    {
        bool isQueued = this.DispatcherQueue.TryEnqueue(
        Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal,
        () => StatusBlock.Text = strMessage);
    }
}
```

```cppwinrt
// MainPage.xaml.cpp in a Windows App SDK app
#include <winrt/Microsoft.UI.Dispatching.h>
...
void MainPage::NotifyUser(std::wstring strMessage)
{
    if (this->DispatcherQueue().HasThreadAccess())
    {
        StatusBlock().Text(strMessage);
    }
    else
    {
        bool isQueued = this->DispatcherQueue().TryEnqueue(
            Microsoft::UI::Dispatching::DispatcherQueuePriority::Normal,
            [strMessage, this]()
            {
                StatusBlock().Text(strMessage);
            });
    }
}
```

## Migrate winrt::resume_foreground (C++/WinRT)

This section applies if you use the [**winrt::resume_foreground**](/uwp/cpp-ref-for-winrt/resume-foreground) function in a coroutine in your C++/WinRT UWP app.

In UWP, the use case for [**winrt::resume_foreground**](/uwp/cpp-ref-for-winrt/resume-foreground) is to switch execution to a foreground thread (that foreground thread is often the one that's associated with a [**Windows.UI.Core.CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher)). Here's an example of that.

```
// MainPage.cpp in a UWP app
winrt::fire_and_forget MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
{
    ...
    co_await winrt::resume_foreground(this->Dispatcher());
    ...
}
```

In your Windows App SDK app:

* Instead of **winrt::resume_foreground**, you'll need to use **wil::resume_foreground** (from the [Windows Implementation Libraries (WIL)](https://github.com/Microsoft/wil)).
* And instead of **CoreDispatcher**, you'll need to use the [**Microsoft.UI.Dispatching.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) class, as described in [Change Windows.UI.Core.CoreDispatcher to Microsoft.UI.Dispatching.DispatcherQueue](#change-windowsuicorecoredispatcher-to-microsoftuidispatchingdispatcherqueue).

So first add a reference to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.

Then add the following include to `pch.h` in the target project.

```cppwinrt
#include <wil/cppwinrt_helpers.h>
```

And then follow the pattern shown below.

```cppwinrt
// MainPage.xaml.cpp in a Windows App SDK app
...
winrt::fire_and_forget MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
{
    ...
    co_await wil::resume_foreground(this->DispatcherQueue());
    ...
}
```

## See Also

* [Windows App SDK and supported Windows releases](../../support.md)
