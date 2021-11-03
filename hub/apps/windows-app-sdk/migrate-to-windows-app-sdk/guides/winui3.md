---
title: User interface migration (including WinUI 3)
description: This topic shows how to migrate your user interface (UI) code, including migrating to the [Windows UI Library (WinUI) 3](/windows/apps/winui/).
ms.topic: article
ms.date: 09/16/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, Windows UI Library, WinUI
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# User interface migration (including WinUI 3)

This topic shows how to migrate your user interface (UI) code, including migrating to the [Windows UI Library (WinUI) 3](/windows/apps/winui/).

## Summary of API and/or feature differences

The **Window.Current** property migrates to **App.Window**. And the **CoreDispatcher.RunAsync** method migrates to **DispatcherQueue.TryEnqueue**.

You need to set your window's *handle* (**HWND**) on a **MessageDialog**, and on **Picker**s.

To use **DataTransferManager** APIs, you need to associate them with your window.

For **ContentDialog** and **Popup**, you need to set their [XamlRoot](/windows/winui/api/microsoft.ui.xaml.controls.contentdialog#contentdialog-in-appwindow-or-xaml-islands) property.

You may need to refactor your Visual State Manager and **Page.Resources** XAML markup.

In the Windows App SDK, the [**AcrylicBrush**](/windows/winui/api/microsoft.ui.xaml.media.acrylicbrush) always samples from the app content.

## Change Windows.UI.Xaml.Window.Current to App.Window

This section applies if you're using the [**Windows.UI.Xaml.Window.Current**](/uwp/api/windows.ui.xaml.window.current) property in your UWP app. That property isn't supported in the Windows App SDK, so this section describes how to port UWP code that uses **Window.Current**.

```csharp
// MainPage.xaml.cs in a UWP app
var width = Window.Current.Bounds.Width;
```

```cppwinrt
// MainPage.xaml.cpp in a UWP app
auto width{ Window::Current().Bounds().Width };
```

Your Windows App SDK app can add its own notion of a *current*, or *main* window by using a public static property on your *App** class.

```csharp
// App.xaml.cs in a Windows App SDK app
public partial class App : Application
{
    ...
    public static Window Window { get { return m_window; } }
    private static Window m_window;
}
```

```cppwinrt
// App.xaml.h in a Windows App SDK app
...
struct App : AppT<App>
{
    ...
    static winrt::Microsoft::UI::Xaml::Window Window(){ return window; };

private:
    static winrt::Microsoft::UI::Xaml::Window window;
};
...

// App.xaml.cpp
...
winrt::Microsoft::UI::Xaml::Window App::window{ nullptr };
...
```

Then, within the **App** class itself, you can change `Window.Current` to simply `window`. Outside of the **App** class, change `Window.Current` to `App.Window`, like this:

```csharp
// MainPage.xaml.cs in a UWP app
var width = App.Window.Bounds.Width;
```

```cppwinrt
// MainPage.xaml.cpp in a UWP app
#include <App.xaml.h>
auto width{ App.Window()::Bounds().Width };
```

## MessageDialog, and Pickers

In your UWP app, if you use certain types from the [**Windows.UI.Popups**](/uwp/api/windows.ui.popups) or [**Windows.Storage.Pickers**](/uwp/api/windows.storage.pickers) namespaces, then this section contains info to help you migrate that code. The code examples below use **MessageDialog**, but you can apply exactly the same techniques to displaying a picker (for example, a [**FileOpenPicker**](/uwp/api/windows.storage.pickers.fileopenpicker), a [**FileSavePicker**](/uwp/api/windows.storage.pickers.filesavepicker), or a [**FolderPicker**](/uwp/api/windows.storage.pickers.folderpicker)).

At a high level, the technique involves these steps (code examples later in this section show exactly how to implement these steps).

* First use the interoperatability method [**IWindowNative::get_WindowHandle**](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nf-microsoft-ui-xaml-window-iwindownative-get_windowhandle) to obtain your window's *handle* (which is of type [**HWND**](/windows/win32/winprog/windows-data-types)).
* Then call the interoperatability method [**IInitializeWithWindow::Initialize**](/windows/win32/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize) to *parent* the UI object that you want to display to the window that owns that **HWND**.

> [!NOTE]
> For new apps, we recommend using the [**ContentDialog**](/uwp/api/windows.ui.xaml.controls.contentdialog) control instead of [**MessageDialog**](/uwp/api/windows.ui.popups.messagedialog). For more info, see the [ContentDialog, and Popup](#contentdialog-and-popup) section below.

Here's some typical UWP code to display a **MessageDialog**.

```csharp
// In a UWP app
var showDialog = new Windows.UI.Popups.MessageDialog("Message here");
await showDialog.ShowAsync();
```

```cppwinrt
// In a UWP app
auto showDialog{ Windows::UI::Popups::MessageDialog(L"Message here") };
co_await showDialog.ShowAsync();
```

In your Windows App SDK app, you'll be following the two steps described above.

First, obtain your your window's *handle* (**HWND**). Find the appropriate source code file indicated in the listing below, and add the code that's shown.

```csharp
// In App.xaml.cs in a Windows App SDK app
...
using System.Runtime.InteropServices;
using WinRT;
...
public partial class App : Application
{
    ...
    public App()
    {
        this.InitializeComponent();
        WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
    }

    public static IntPtr WindowHandle { get; private set; }
}
```

```cppwinrt
// pch.h in a Windows App SDK app
...
#include <microsoft.ui.xaml.window.h>
...

// App.xaml.h
...
struct App : AppT<App>
{
    ...
    static HWND WindowHandle() { return m_hWnd; }

private:
    static HWND m_hWnd;
};
...

// App.xaml.cpp
...
HWND App::m_hWnd{ 0 };
...
void App::OnLaunched(LaunchActivatedEventArgs const&)
{
    window = make<MainWindow>();
    window.Activate();

    auto windowNative{ window.as<::IWindowNative>() };
    HWND hWnd{ 0 };
    windowNative->get_WindowHandle(&hWnd);
    App::m_hWnd = hWnd;
}
...
```

And then we can use that window *handle* (**HWND**) in a helper function like this.

```csharp
// In MainWindow.xaml.cs in a Windows App SDK app
...
using System.Runtime.InteropServices;
using WinRT;
...
public sealed partial class MainWindow : Window
{
    ...
    private static void ParentDialogToWindow(object dialog)
    {
        WinRT.Interop.InitializeWithWindow.Initialize(dialog, App.WindowHandle);
    }
}
```

```cppwinrt
// pch.h in a Windows App SDK app
...
#include <Shobjidl.h>
...

// MainWindow.xaml.h
...
#include <App.xaml.h>
...
struct MainWindow : MainWindowT<MainWindow>
{
  ...
  private:
    static void ParentDialogToWindow(Windows::Foundation::IInspectable const&);
};
...

// MainWindow.xaml.cpp
...
void MainWindow::ParentDialogToWindow(IInspectable const& dialog)
{
    auto initializeWithWindow{ dialog.as<::IInitializeWithWindow>() };
    initializeWithWindow->Initialize(App::WindowHandle());
}
...
```

Lastly, we can call that helper function like this.

```csharp
// In MainWindow.xaml.cs in a Windows App SDK app
var showDialog = new Windows.UI.Popups.MessageDialog("Message here");
ParentDialogToWindow(showDialog);
await showDialog.ShowAsync();
```

```cppwinrt
// pch.h in a Windows App SDK app
...
#include <winrt/Windows.UI.Popups.h>
...

// MainWindow.xaml.cpp
auto showDialog{ Windows::UI::Popups::MessageDialog(L"Message here") };
ParentDialogToWindow(showDialog);
co_await showDialog.ShowAsync();
```

## DataTransferManager

In your UWP app, if you call the [**DataTransferManager.ShowShareUI**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui#Windows_ApplicationModel_DataTransfer_DataTransferManager_ShowShareUI) method, then this section contains info to help you migrate that code.

Here's some typical UWP code that calls **ShowShareUI**.

```csharp
// In a UWP app
Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
```

```cppwinrt
// In a UWP app
Windows::ApplicationModel::DataTransfer::DataTransferManager::ShowShareUI();
```

To use **DataTransferManager.ShowShareUI** in your Windows App SDK app, you need to associate the Share UI with your window. And that needs to be done manually. Use the code shown below.

```csharp
// In MainWindow.xaml.cs in a Windows App SDK app
...
public sealed partial class MainWindow : Window
{
    ...
    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        // `this` in `GetWindowHandle(this)` must be a reference to
        // your current window
        IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);

        IDataTransferManagerInterop interop = Windows.ApplicationModel.DataTransfer.DataTransferManager.As<IDataTransferManagerInterop>();

        // Show the Share UI
        interop.ShowShareUIForWindow(windowHandle);
    }

    [System.Runtime.InteropServices.ComImport, System.Runtime.InteropServices.Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
    [System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
    interface IDataTransferManagerInterop
    {
        Windows.ApplicationModel.DataTransfer.DataTransferManager GetForWindow([System.Runtime.InteropServices.In] IntPtr appWindow, [System.Runtime.InteropServices.In] ref Guid riid);
        void ShowShareUIForWindow(IntPtr appWindow);
    }
}
...
```

```cppwinrt
// pch.h in a Windows App SDK app
...
#include <shobjidl_core.h>
#include <microsoft.ui.xaml.window.h>
#include <winrt/Windows.ApplicationModel.DataTransfer.h>
...

// MainWindow.xaml.cpp
...
void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
{
    winrt::Microsoft::UI::Xaml::Window window{ *this };
    auto windowNative{ window.as<::IWindowNative>() };
    HWND hWnd{ 0 };
    windowNative->get_WindowHandle(&hWnd);

    auto interop = winrt::get_activation_factory<Windows::ApplicationModel::DataTransfer::DataTransferManager, IDataTransferManagerInterop>();
    interop->ShowShareUIForWindow(hWnd);
}
...
```

## ContentDialog, and Popup

If in your UWP app you're using the [**Windows.UI.Xaml.Controls.ContentDialog**](/uwp/api/windows.ui.xaml.controls.contentdialog) or [**Windows.UI.Xaml.Controls.Primitives.Popup**](/uwp/api/windows.ui.xaml.controls.primitives.popup) classes, then this section contains info to help you migrate that code. The code examples below use **ContentDialog**, but you can apply exactly the same techniques to displaying a **Popup** object.

Here's some typical UWP code to display a **ContentDialog**.

```csharp
// MainPage.xaml.cs in a UWP app
var unsupportedFilesDialog = new ContentDialog();
// Set Title, Content, etc.
await unsupportedFilesDialog.ShowAsync();
```

```cppwinrt
// MainPage.xaml.cpp in a UWP app
ContentDialog unsupportedFilesDialog{};
// Set Title, Content, etc.
co_await unsupportedFilesDialog.ShowAsync();
```

In your Windows App SDK app, you just need to also set the dialog's [XamlRoot](/windows/winui/api/microsoft.ui.xaml.controls.contentdialog#contentdialog-in-appwindow-or-xaml-islands) property. Here's how.

```csharp
// MainPage.xaml.cs in a Windows App SDK app
var unsupportedFilesDialog = new ContentDialog();
// Set Title, Content, etc.
unsupportedFilesDialog.XamlRoot = this.Content.XamlRoot;
await unsupportedFilesDialog.ShowAsync();
```

```cppwinrt
// MainPage.xaml.cpp in a Windows App SDK app
ContentDialog unsupportedFilesDialog{};
// Set Title, Content, etc.
unsupportedFilesDialog.XamlRoot(this->Content().XamlRoot());
co_await unsupportedFilesDialog.ShowAsync();
```

## Do I need to implement page navigation?

In a UWP project, by default there will be navigation code in the methods of the **App** class, even the your app is simple enough that it has only one **Page**.

When you create a new Windows App SDK project in Visual Studio, the project template provides you with a **MainWindow** class (of type [**Microsoft.UI.Xaml.Window**](/windows/winui/api/microsoft.ui.xaml.window)), but no **Page**. And the project template doesn't provide any navigation code.

For a Windows App SDK app that's simple enough (a single-page app), you might be able to simplify it. It might be that you needn't create pages or user controls in your Windows App SDK project&mdash;but instead copy the XAML markup and code-behind of that single page into **MainWindow**. However, there are some things that **MainWindow** doesn't support. **Window** isn't a DependencyObject, so capabilities such as **Resources** and **DataContext** don't exist on it. Neither do events such as **Load** and **Unload**. For more info, and workarounds, see [Visual State Manager, and Page.Resources](#visual-state-manager-and-pageresources).

If on the other hand you want or need navigation between pages in your Windows App SDK app, then you can do so by migrating the **App.OnLaunched**, **App::CreateRootFrame**, and **App::OnNavigationFailed** methods from your UWP app. In **App.OnLaunched**, locate the navigation code (the code that creates *rootFrame*, and navigates to the first page of your app) and merge it right in between the two existing lines of code (the lines that create a window and then activate it).

## Visual State Manager, and Page.Resources

Also see [Do I need to implement page navigation?](#do-i-need-to-implement-page-navigation). If you *do* have a UWP app that's simple enough where you can copy your XAML markup and code-behind into **MainWindow**, then bear in mind these exceptions.

Your **MainWindow** class (of type [**Microsoft.UI.Xaml.Window**](/windows/winui/api/microsoft.ui.xaml.window)) isn't a control, so it doesn't support Visual State Manager XAML markup and code-behind (see [Tutorial: Create adaptive layouts](/windows/apps/design/basics/xaml-basics-adaptive-layout)). You have these two options, though:

* Add a **UserControl** item to the project, and migrate your markup and code-behind to that. Then place an instance of that user control in **MainWindow**.
* Add a **Page** item to the project, and migrate your markup and code-behind to that. Then add code to your **App** class to navigate to that **Page** on startup, as described in [Do I need to implement page navigation?](#do-i-need-to-implement-page-navigation).

In addition, you won't be able to copy a `<Page.Resources>` element over to **MainWindow** and just rename it to `<Window.Resources>`. Instead, parent the **Resources** element under the root layout container (for example, a **Grid**) in the XAML markup for **MainWindow**. That will look like this:

```xaml
<Window ...>
    <Grid>
        <Grid.Resources>...</Grid.Resources>
        ...
    </Grid>
</Window>
```

## AcrylicBrush.BackgroundSource property

The [**AcrylicBrush.BackgroundSource**](/uwp/api/windows.ui.xaml.media.acrylicbrush.backgroundsource) property exists in UWP, but not in the Windows App SDK. In the Windows App SDK, the [**AcrylicBrush**](/windows/winui/api/microsoft.ui.xaml.media.acrylicbrush) always samples from the app content.

So if you're accessing the **AcrylicBrush.BackgroundSource** property in the source code of your UWP app (whether that's in XAML markup or in imperative code), then remove that code when migrating your app to the Windows App SDK.
