---
title: User interface migration (including WinUI 3)
description: This topic shows how to migrate your user interface (UI) code, including migrating to [WinUI 3](../../../winui/index.md).
ms.topic: article
ms.date: 06/23/2022
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, WinUI
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# User interface migration (including WinUI 3)

This topic shows how to migrate your user interface (UI) code, including migrating to [WinUI 3](../../../winui/index.md).

## Summary of API and/or feature differences

The **Window.Current** property migrates to **App.Window**. And the **CoreDispatcher.RunAsync** method migrates to **DispatcherQueue.TryEnqueue**.

You need to set your window's *handle* (**HWND**) on a **MessageDialog**, and on **Picker**s.

To use **DataTransferManager** APIs, you need to associate them with your window.

For **ContentDialog** and **Popup**, you need to set their [XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog#contentdialog-in-appwindow-or-xaml-islands) property.

You may need to refactor your Visual State Manager and **Page.Resources** XAML markup.

In the Windows App SDK, the [**AcrylicBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.acrylicbrush) always samples from the app content.

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

Your Windows App SDK app can add its own notion of a *current*, or *main* window by using a public static property on your **App** class.

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
auto width{ App::Window().Bounds().Width };
```

## MessageDialog, and Pickers

In your UWP app, if you use certain types from the [**Windows.UI.Popups**](/uwp/api/windows.ui.popups) or [**Windows.Storage.Pickers**](/uwp/api/windows.storage.pickers) namespaces, then this section contains info to help you migrate that code. The code examples below use **MessageDialog**, but you can apply exactly the same techniques to displaying a picker (for example, a [**FileOpenPicker**](/uwp/api/windows.storage.pickers.fileopenpicker), a [**FileSavePicker**](/uwp/api/windows.storage.pickers.filesavepicker), or a [**FolderPicker**](/uwp/api/windows.storage.pickers.folderpicker)).

The steps that you have to follow in a desktop app are described in [Display WinRT UI objects that depend on CoreWindow](../../../develop/ui-input/display-ui-objects.md).

> [!NOTE]
> For new apps, we recommend using the [**ContentDialog**](/uwp/api/windows.ui.xaml.controls.contentdialog) control instead of [**MessageDialog**](/uwp/api/windows.ui.popups.messagedialog). For more info, see the [ContentDialog, and Popup](#contentdialog-and-popup) section below.

Here's some typical UWP code to display a [**MessageDialog**](/uwp/api/windows.ui.popups.messagedialog).

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

And here's the equivalent code in a Windows App SDK app.

```csharp
// MainWindow.xaml.cs in a WinUI 3 app
var showDialog = new Windows.UI.Popups.MessageDialog("Message here");
WinRT.Interop.InitializeWithWindow.Initialize(showDialog,
    WinRT.Interop.WindowNative.GetWindowHandle(this));
await showDialog.ShowAsync();
```

```cppwinrt
// pch.h in a WinUI 3 app
...
#include <Shobjidl.h>
#include <microsoft.ui.xaml.window.h>
#include <winrt/Windows.UI.Popups.h>
...

// MainWindow.xaml.cpp
...
auto showDialog{ Windows::UI::Popups::MessageDialog(L"Message here") };

auto windowNative{ this->m_inner.as<::IWindowNative>() };
HWND hWnd{ 0 };
windowNative->get_WindowHandle(&hWnd);
showDialog.as<::IInitializeWithWindow>()->Initialize(hWnd);

co_await showDialog.ShowAsync();
```

## DataTransferManager

In your UWP app, if you call the [**DataTransferManager.ShowShareUI**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui#Windows_ApplicationModel_DataTransfer_DataTransferManager_ShowShareUI) method, then this section contains info to help you migrate that code.

Here's some typical UWP code that calls **ShowShareUI**.

```csharp
// In a UWP app
var dataTransferManager = Windows.ApplicationModel.DataTransfer.DataTransferManager.GetForCurrentView();

dataTransferManager.DataRequested += (sender, args) =>
{
    args.Request.Data.Properties.Title = "In a UWP app...";
    args.Request.Data.SetText("...display the user interface for sharing content with another app.");
    args.Request.Data.RequestedOperation =
        Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
};

Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
```

```cppwinrt
// In a UWP app
#include <winrt/Windows.ApplicationModel.DataTransfer.h>
...
auto dataTransferManager{ Windows::ApplicationModel::DataTransfer::DataTransferManager::GetForCurrentView() };

dataTransferManager.DataRequested([](Windows::ApplicationModel::DataTransfer::DataTransferManager const& /* sender */,
    Windows::ApplicationModel::DataTransfer::DataRequestedEventArgs const& args)
    {
        args.Request().Data().Properties().Title(L"In a UWP app...");
        args.Request().Data().SetText(L"...display the user interface for sharing content with another app.");
        args.Request().Data().RequestedOperation(Windows::ApplicationModel::DataTransfer::DataPackageOperation::Copy);
    });

Windows::ApplicationModel::DataTransfer::DataTransferManager::ShowShareUI();
```

To use **DataTransferManager.ShowShareUI** in your Windows App SDK app, you need to associate the Share UI with your window. And that needs to be done manually. For more info, and code examples, see [Display WinRT UI objects that depend on CoreWindow](../../../develop/ui-input/display-ui-objects.md).

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

In your Windows App SDK app, you just need to also set the dialog's [XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog#contentdialog-in-appwindow-or-xaml-islands) property. Here's how.

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

In a UWP project, by default there will be navigation code in the methods of the **App** class, even if your app is simple enough that it has only one **Page**.

When you create a new Windows App SDK project in Visual Studio, the project template provides you with a **MainWindow** class (of type [**Microsoft.UI.Xaml.Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window)), but no **Page**. And the project template doesn't provide any navigation code.

For a Windows App SDK app that's simple enough (a single-page app), you might be able to simplify it. It might be that you needn't create pages or user controls in your Windows App SDK project&mdash;but instead copy the XAML markup and code-behind of that single page into **MainWindow**. However, there are some things that **MainWindow** doesn't support. **Window** isn't a DependencyObject, so capabilities such as **Resources** and **DataContext** don't exist on it. Neither do events such as **Load** and **Unload**. For more info, and workarounds, see [Visual State Manager, and Page.Resources](#visual-state-manager-and-pageresources).

If on the other hand you want or need navigation between pages in your Windows App SDK app, then you can do so by migrating the **App.OnLaunched** and **App::OnNavigationFailed** methods from your UWP app. In **App.OnLaunched**, locate the navigation code (the code that creates *rootFrame*, and navigates to the first page of your app) and merge it right in between the two existing lines of code (the lines that create a window and then activate it). You'll also need to migrate the code you've copy-pasted. For a simple code example, see [**Page class**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page#examples).

## Visual State Manager, and Page.Resources

Also see [Do I need to implement page navigation?](#do-i-need-to-implement-page-navigation). If you *do* have a UWP app that's simple enough where you can copy your XAML markup and code-behind into **MainWindow**, then bear in mind these exceptions.

Your **MainWindow** class (of type [**Microsoft.UI.Xaml.Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window)) isn't a control, so it doesn't support Visual State Manager XAML markup and code-behind (see [Tutorial: Create adaptive layouts](../../../design/layout/layouts-with-xaml.md)). You have these two options, though:

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

The [**AcrylicBrush.BackgroundSource**](/uwp/api/windows.ui.xaml.media.acrylicbrush.backgroundsource) property exists in UWP, but not in the Windows App SDK. In the Windows App SDK, the [**AcrylicBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.acrylicbrush) always samples from the app content.

So if you're accessing the **AcrylicBrush.BackgroundSource** property in the source code of your UWP app (whether that's in XAML markup or in imperative code), then remove that code when migrating your app to the Windows App SDK. Instead, use the [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller) class.

## Related topics

* [Windows App SDK and supported Windows releases](../../support.md)
* [Display WinRT UI objects that depend on CoreWindow](../../../develop/ui-input/display-ui-objects.md)