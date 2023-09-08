---
title: Display WinRT UI objects that depend on CoreWindow
description: You can use certain pickers, popups, dialogs, and other Windows Runtime (WinRT) objects in your desktop app by adding a little bit of interoperation code.
ms.topic: article
ms.date: 02/28/2023
keywords: Windows, App, SDK, desktop, C#, C++, cpp, window, handle, HWND, Windows UI Library, WinUI, interop, IInitializeWithWindow, IInitializeWithWindow::Initialize, WinRT.Interop.InitializeWithWindow, IDataTransferManagerInterop, IUserConsentVerifierInterop
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Display WinRT UI objects that depend on CoreWindow

Certain pickers, popups, dialogs, and other Windows Runtime (WinRT) objects depend on a [CoreWindow](/uwp/api/windows.ui.core.corewindow); typically to display a user-interface (UI). Even though **CoreWindow** isn't supported in desktop apps (see [Core unsupported classes](../../desktop/modernize/desktop-to-uwp-supported-api.md#core-unsupported-classes)), you can still use many of those WinRT classes in your desktop app by adding a little bit of interoperation code.

Your desktop app can be [Windows UI Library (WinUI) 3](../../winui/winui3/index.md), [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/), or [Windows Forms (WinForms)](/dotnet/desktop/winforms/) apps. Code examples are presented in C# and [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/).

## Set the owner window handle (HWND) for a WinRT UI object

For classes that implement the [**IInitializeWithWindow**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) interface (or the equivalent [**IDataTransferManagerInterop**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop) interface), you can use that interface to set an owner window on the object before you display it. It's a two-step process.

1. Decide which window will be the owner of the UI object that you want to display, and retrieve that window's HWND. For more details and code examples for this step, see [Retrieve a window handle (HWND)](./retrieve-hwnd.md).
2. Then call the appropriate interoperatability API (for C# or C++/WinRT) to set an owner window handle (HWND) for the WinRT UI object.

## For classes that implement IInitializeWithWindow

These classes implement [**IInitializeWithWindow**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow):

* [**Windows.ApplicationModel.Contacts.PinnedContactManager**](/uwp/api/windows.applicationmodel.contacts.pinnedcontactmanager)
* [**Windows.ApplicationModel.Payments.PaymentMediator**](/uwp/api/windows.applicationmodel.payments.paymentmediator)
* [**Windows.Devices.Enumeration.DevicePicker**](/uwp/api/windows.devices.enumeration.devicepicker)
* [**Windows.Graphics.Capture.GraphicsCapturePicker**](/uwp/api/windows.graphics.capture.graphicscapturepicker)
* [**Windows.Media.Casting.CastingDevicePicker**](/uwp/api/windows.media.casting.castingdevicepicker)
* [**Windows.Media.DialProtocol.DialDevicePicker**](/uwp/api/windows.media.dialprotocol.dialdevicepicker)
* [**Windows.Networking.NetworkOperators.ProvisioningAgent**](/uwp/api/windows.networking.networkoperators.provisioningagent)
* [**Windows.Security.Authentication.OnlineId.OnlineIdAuthenticator**](/uwp/api/windows.security.authentication.onlineid.onlineidauthenticator)
* [**Windows.Services.Store.StoreContext**](/uwp/api/windows.services.store.storecontext)
* [**Windows.Storage.Pickers.FileOpenPicker**](/uwp/api/windows.storage.pickers.fileopenpicker)
* [**Windows.Storage.Pickers.FileSavePicker**](/uwp/api/windows.storage.pickers.filesavepicker)
* [**Windows.Storage.Pickers.FolderPicker**](/uwp/api/windows.storage.pickers.folderpicker)
* [**Windows.System.FolderLauncherOptions**](/uwp/api/windows.system.folderlauncheroptions)&mdash;Windows 10, version 1903 (10.0; Build 18362) and later
* [**Windows.System.LauncherOptions**](/uwp/api/windows.system.launcheroptions)&mdash;Windows 10, version 1903 (10.0; Build 18362) and later
* [**Windows.UI.Core.CoreWindowDialog**](/uwp/api/windows.ui.core.corewindowdialog)
* [**Windows.UI.Core.CoreWindowFlyout**](/uwp/api/windows.ui.core.corewindowflyout)
* [**Windows.UI.Popups.MessageDialog**](/uwp/api/windows.ui.popups.messagedialog). But for new apps we recommend using the [**ContentDialog**](/uwp/api/windows.ui.xaml.controls.contentdialog) control instead.
* [**Windows.UI.Popups.PopupMenu**](/uwp/api/windows.ui.popups.popupmenu)
* [**Windows.UI.StartScreen.SecondaryTile**](/uwp/api/windows.ui.startscreen.secondarytile)
* [**Windows.Web.Http.Filters.HttpBaseProtocolFilter**](/uwp/api/windows.web.http.filters.httpbaseprotocolfilter)

> [!NOTE]
> The list above is necessarily incomplete&mdash;refer to a type's documentation to see whether it implements **IInitializeWithWindow** (or an equivalent interop interface).

The next sections contain code examples to display a [**FolderPicker**](/uwp/api/windows.storage.pickers.fileopenpicker). But it's the same technique to display any of the APIs listed above.

### WinUI 3 with C# (also WPF/WinForms with .NET 6 or later)

> [!NOTE]
> The code examples in this section use the **WinRT.Interop.WindowNative** C# interop class. If you target .NET 6 or later, then you can use that class in a WPF or WinForms project. For info about setting up your project to do that, see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

The C# code below expects that you've already used the pattern documented in [Retrieve a window handle (HWND)](./retrieve-hwnd.md). Then, to set the owner window for the UI object that you want to display, the code calls the **Initialize** method on the **WinRT.Interop.InitializeWithWindow** C# interop class. For more info about the C# interop classes, see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

```csharp
// MainWindow.xaml.cs
private async void ShowFolderPickerAsync(IntPtr hWnd)
{
    // Create a folder picker.
    var folderPicker = new Windows.Storage.Pickers.FolderPicker();

    // Initialize the folder picker with the window handle (HWND).
    WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

    // Use the folder picker as usual.
    folderPicker.FileTypeFilter.Add("*");
    var folder = await folderPicker.PickSingleFolderAsync();
}
```

### WinUI 3 with C++

The C++/WinRT code below expects that you've already used the pattern documented in [Retrieve a window handle (HWND)](./retrieve-hwnd.md). Then, to set the owner window for the UI object that you want to display, the code calls the interoperatability method [**IInitializeWithWindow::Initialize**](/windows/win32/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize).

```cppwinrt
// pch.h
...
#include <microsoft.ui.xaml.window.h>
#include <Shobjidl.h>
#include <winrt/Windows.Storage.Pickers.h>

// MainWindow.xaml.cpp
winrt::fire_and_forget ShowFolderPickerAsync(HWND hWnd)
{
    // Create a folder picker.
    Windows::Storage::Pickers::FolderPicker folderPicker;

    // Initialize the folder picker with the window handle (HWND).
    auto initializeWithWindow{ folderPicker.as<::IInitializeWithWindow>() };
    initializeWithWindow->Initialize(hWnd);

    // Use the folder picker as usual.
    folderPicker.FileTypeFilter().Append(L"*");
    auto folder{ co_await folderPicker.PickSingleFolderAsync() };
}
```

## For classes that implement IDataTransferManagerInterop

The [**Windows.ApplicationModel.DataTransfer.DataTransferManager**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager) class implements the [**IDataTransferManagerInterop**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop) interface (which, like **IInitializeWithWindow**, lets you set an owner window).

In a desktop app, instead of calling the [**DataTransferManager.ShowShareUI**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui) method, you call [**IDataTransferManagerInterop::ShowShareUIForWindow**](/windows/win32/api/shobjidl_core/nf-shobjidl_core-idatatransfermanagerinterop-showshareuiforwindow), as shown in the code examples below.

### WinUI 3 with C# (also WPF/WinForms with .NET 6 or later)

```csharp
// MainWindow.xaml.cs
...
public sealed partial class MainWindow : Window
{
    ...

    [System.Runtime.InteropServices.ComImport]
    [System.Runtime.InteropServices.Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
    [System.Runtime.InteropServices.InterfaceType(
        System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
    interface IDataTransferManagerInterop
    {
        IntPtr GetForWindow([System.Runtime.InteropServices.In] IntPtr appWindow,
            [System.Runtime.InteropServices.In] ref Guid riid);
        void ShowShareUIForWindow(IntPtr appWindow);
    }

    static readonly Guid _dtm_iid = 
        new Guid(0xa5caee9b, 0x8708, 0x49d1, 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c);

    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        IDataTransferManagerInterop interop =
        Windows.ApplicationModel.DataTransfer.DataTransferManager.As
            <IDataTransferManagerInterop>();

        IntPtr result = interop.GetForWindow(hWnd, _dtm_iid);
        var dataTransferManager = WinRT.MarshalInterface
            <Windows.ApplicationModel.DataTransfer.DataTransferManager>.FromAbi(result);

        dataTransferManager.DataRequested += (sender, args) =>
        {
            args.Request.Data.Properties.Title = "In a desktop app...";
            args.Request.Data.SetText("...display WinRT UI objects that depend on CoreWindow.");
            args.Request.Data.RequestedOperation = 
                Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        };

        // Show the Share UI
        interop.ShowShareUIForWindow(hWnd);
    }
}
...
```

### WinUI 3 with C++

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
    // Retrieve the window handle (HWND) of the current WinUI 3 window.
    auto windowNative{ this->try_as<::IWindowNative>() };
    winrt::check_bool(windowNative);
    HWND hWnd{ 0 };
    windowNative->get_WindowHandle(&hWnd);

    winrt::com_ptr<IDataTransferManagerInterop> interop = 
        winrt::get_activation_factory<Windows::ApplicationModel::DataTransfer::DataTransferManager,
        IDataTransferManagerInterop>();

    winrt::guid _dtm_iid{ 0xa5caee9b, 0x8708, 0x49d1, { 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c } };
    Windows::ApplicationModel::DataTransfer::DataTransferManager dataTransferManager{ nullptr };
    interop->GetForWindow(hWnd, _dtm_iid, winrt::put_abi(dataTransferManager));

    dataTransferManager.DataRequested([](Windows::ApplicationModel::DataTransfer::DataTransferManager const& /* sender */,
        Windows::ApplicationModel::DataTransfer::DataRequestedEventArgs const& args)
    {
        args.Request().Data().Properties().Title(L"In a desktop app...");
        args.Request().Data().SetText(L"...display WinRT UI objects that depend on CoreWindow.");
        args.Request().Data().RequestedOperation(Windows::ApplicationModel::DataTransfer::DataPackageOperation::Copy);
    });

    interop->ShowShareUIForWindow(hWnd);
}
...
```

## For classes that implement IUserConsentVerifierInterop

The [**Windows.Security.Credentials.UI.UserConsentVerifier**](/uwp/api/windows.security.credentials.ui.userconsentverifier) class implements the [**IUserConsentVerifierInterop**](/windows/win32/api/userconsentverifierinterop/nn-userconsentverifierinterop-iuserconsentverifierinterop) interface (which, like **IInitializeWithWindow**, lets you set an owner window).

In a desktop app, instead of calling the [**UserConsentVerifier.RequestVerificationAsync**](/uwp/api/windows.security.credentials.ui.userconsentverifier.requestverificationasync) method:

* **C#**. Call the **RequestVerificationForWindowAsync** method of the **Windows.Security.Credentials.UI.UserConsentVerifierInterop** C# interop class. For more info about the C# interop classes, see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).
* **C++/WinRT**. Call [**IUserConsentVerifierInterop::RequestVerificationForWindowAsync**](/windows/win32/api/userconsentverifierinterop/nf-userconsentverifierinterop-iuserconsentverifierinterop-requestverificationforwindowasync).

For more info, and code examples, see [**UserConsentVerifier**](/uwp/api/windows.security.credentials.ui.userconsentverifier).

## For classes that implement other interop interfaces

These interfaces have **XxxForWindow** methods, which let you set an owner window handle (HWND). You can use these interfaces directly from C++/WinRT. Versions of the interfaces also exist in the form of C# classes&mdash;for more details, see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

* [**IAccountsSettingsPaneInterop**](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop)
* [**IDragDropManagerInterop**](/windows/win32/api/dragdropinterop/nn-dragdropinterop-idragdropmanagerinterop)
* [**IInputPaneInterop**](/windows/win32/api/inputpaneinterop/nn-inputpaneinterop-iinputpaneinterop)
* [**IPlayToManagerInterop**](/windows/win32/api/playtomanagerinterop/nn-playtomanagerinterop-iplaytomanagerinterop)
* [**IPrintManagerInterop**](/windows/win32/api/printmanagerinterop/nn-printmanagerinterop-iprintmanagerinterop)
* [**IRadialControllerConfigurationInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerconfigurationinterop)
* **IRadialControllerIndependentInputSourceInterop**
* [**IRadialControllerInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerinterop)
* [**ISpatialInteractionManagerInterop**](/windows/win32/api/spatialinteractionmanagerinterop/nn-spatialinteractionmanagerinterop-ispatialinteractionmanagerinterop)
* [**ISystemMediaTransportControlsInterop**](/windows/win32/api/systemmediatransportcontrolsinterop/nn-systemmediatransportcontrolsinterop-isystemmediatransportcontrolsinterop)
* [**IUIViewSettingsInterop**](/windows/win32/api/uiviewsettingsinterop/nn-uiviewsettingsinterop-iuiviewsettingsinterop)
* [**IWebAuthenticationCoreManagerInterop**](/windows/win32/api/webauthenticationcoremanagerinterop/nn-webauthenticationcoremanagerinterop-iwebauthenticationcoremanagerinterop)

## Related topics

* [Retrieve a window handle (HWND)](./retrieve-hwnd.md)
* [Windows UI Library (WinUI) 3](../../winui/winui3/index.md)
* [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
* [Windows Forms (WinForms)](/dotnet/desktop/winforms/)
* [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/)
* [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md)
