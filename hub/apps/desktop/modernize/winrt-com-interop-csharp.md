---
title: Call interop APIs from a .NET app
description: Enhance your desktop application for your users by calling interop functions, and WinRT COM interop interfaces, projected into .NET.
ms.date: 03/07/2022
ms.topic: article
ms.localizationpriority: medium
---

# Call interop APIs from a .NET app

As a C# desktop application developer, in .NET you can make use of C# interop classes that represent several interoperability functions and Windows Runtime (WinRT) COM interoperability interfaces. These include C# classes representing [IWindowNative](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative), [IInitializeWithWindow](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow), the [GetWindowIdFromWindow](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowidfromwindow) function, and many others.

This topic lists the available C# interop classes, and shows how to use them. The [Background](#background) section at the end of the topic describes how interop interfaces were used in previous versions of .NET, and why the change was made.

## Configure a .NET desktop project to use the C# interop classes

The C# interop classes listed in the next section ([Available C# interop classes](#available-c-interop-classes)) are available in .NET either as part of the [Windows App SDK](../../windows-app-sdk/index.md), or else by using a particular [Target Framework Moniker](desktop-to-uwp-enhance.md#net-6-and-later-use-the-target-framework-moniker-option), as we'll see.

### In a WinUI 3 C# desktop project

When you create a new WinUI 3 project in Visual Studio (see [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)), your project is already configured, and you can start using all of the C# interop classes right away.

### In other C# desktop project types (WPF or WinForms)

For other .NET desktop project types&mdash;such as [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/) or [Windows Forms (WinForms)](/dotnet/desktop/winforms/)&mdash;you'll need to configure your project before you can access the C# interop classes. For the first set of classes listed below, you'll need to reference the [Windows App SDK](../../windows-app-sdk/index.md). For the second set, you'll need to configure a [Target Framework Moniker](desktop-to-uwp-enhance.md#net-6-and-later-use-the-target-framework-moniker-option) that targets Windows 10, version 1809 or later, like this:

1. Open the project file for your C# .NET desktop project.

2. In the `.csproj` file, modify the **TargetFramework** element to target a specific .NET and Windows SDK version. For example, the following element is appropriate for a .NET 6 project that targets Windows 10, version 2004.

    ```xml
    <PropertyGroup>
      <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    </PropertyGroup>
    ```

For more information&mdash;including a list of other supported values&mdash;see [Use the Target Framework Moniker option](desktop-to-uwp-enhance.md#net-6-and-later-use-the-target-framework-moniker-option).

## Available C# interop classes

> [!NOTE]
> The classes below require the .NET 6 SDK or later.

Here are the available C# interop classes, mapped from their underlying interop function or WinRT COM interop interface. Each class listed implements the function/methods of its underlying interop API, and provides type-safe wrappers for parameters and return values. For example, **Windows.ApplicationModel.DataTransfer.DragDrop.Core.DragDropManagerInterop.GetForWindow** requires an **IntPtr** window handle (HWND) parameter, and returns a [**CoreDragDropManager**](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragdropmanager) object. All of the C# interop classes below and associated methods are static.

### Available as part of the Windows App SDK

The [**Microsoft.UI.Win32Interop**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.md) class implements the C# interop methods in the table below. For a code example, see [Manage app windows](../../windows-app-sdk/windowing/windowing-overview.md#code-example).

|Interop function|C# interop method|
|-|-|
|[**GetDisplayIdFromMonitor**](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getdisplayidfrommonitor)|(**Microsoft.UI**) DisplayId [**Win32Interop.GetDisplayIdFromMonitor**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.getdisplayidfrommonitor.md)(IntPtr hmonitor)|
|[**GetIconFromIconId**](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-geticonfromiconid)|(**Microsoft.UI**) IntPtr [**Win32Interop.GetIconFromIconId**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.geticonfromiconid.md)(IconId iconId)|
|[**GetIconIdFromIcon**](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-geticonidfromicon)|(**Microsoft.UI**) IconId [**Win32Interop.GetIconIdFromIcon**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.geticonidfromicon.md)(IntPtr hicon)|
|[**GetMonitorFromDisplayId**](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getmonitorfromdisplayid)|(**Microsoft.UI**) IntPtr [**Win32Interop.GetMonitorFromDisplayId**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.getmonitorfromdisplayid.md)(DisplayId displayId)|
|[**GetWindowFromWindowId**](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowfromwindowid)|(**Microsoft.UI**) IntPtr [**Win32Interop.GetWindowFromWindowId**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.getwindowfromwindowid.md)(WindowId windowId)|
|[**GetWindowIdFromWindow**](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowidfromwindow)|(**Microsoft.UI**) WindowId [**Win32Interop.GetWindowIdFromWindow**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.getwindowidfromwindow.md)(IntPtr hwnd)|

### Available via Target Framework Moniker

|WinRT COM interop interface|C# interop class|
|-|-|
|[**IAccountsSettingsPaneInterop**](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop)|(**Windows.UI.ApplicationSettings**) **AccountsSettingsPaneInterop**|
|[**IDisplayInformationStaticsInterop**](/windows/win32/api/windows.graphics.display.interop/nn-windows-graphics-display-interop-idisplayinformationstaticsinterop)|Introduced with TFM `net6.0-windows10.0.22621.0` and .NET 6.0.7.<br/><br/>(**Windows.Graphics.Display**) **DisplayInformationInterop**|
|[**IDragDropManagerInterop**](/windows/win32/api/dragdropinterop/nn-dragdropinterop-idragdropmanagerinterop)|(**Windows.ApplicationModel.DataTransfer.DragDrop.Core**) **DragDropManagerInterop**|
|[**IInitializeWithWindow**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow)|(**WinRT.Interop**) **InitializeWithWindow**|
|[**IInputPaneInterop**](/windows/win32/api/inputpaneinterop/nn-inputpaneinterop-iinputpaneinterop)|(**Windows.UI.ViewManagement**) **InputPaneInterop**|
|[**IPlayToManagerInterop**](/windows/win32/api/playtomanagerinterop/nn-playtomanagerinterop-iplaytomanagerinterop)|(**Windows.Media.PlayTo**) **PlayToManagerInterop**|
|[**IPrintManagerInterop**](/windows/win32/api/printmanagerinterop/nn-printmanagerinterop-iprintmanagerinterop)|(**Windows.Graphics.Printing**) **PrintManagerInterop**|
|[**IRadialControllerConfigurationInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerconfigurationinterop)|(**Windows.UI.Input**) **RadialControllerConfigurationInterop**|
|**IRadialControllerIndependentInputSourceInterop**|(**Windows.UI.Input.Core**) **RadialControllerIndependentInputSourceInterop**|
|[**IRadialControllerInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerinterop)|(**Windows.UI.Input**) **RadialControllerInterop**|
|[**ISpatialInteractionManagerInterop**](/windows/win32/api/spatialinteractionmanagerinterop/nn-spatialinteractionmanagerinterop-ispatialinteractionmanagerinterop)|(**Windows.UI.Input.Spatial**) **SpatialInteractionManagerInterop**|
|[**ISystemMediaTransportControlsInterop**](/windows/win32/api/systemmediatransportcontrolsinterop/nn-systemmediatransportcontrolsinterop-isystemmediatransportcontrolsinterop)|(**Windows.Media**) **SystemMediaTransportControlsInterop**|
|[**IUIViewSettingsInterop**](/windows/win32/api/uiviewsettingsinterop/nn-uiviewsettingsinterop-iuiviewsettingsinterop)|(**Windows.UI.ViewManagement**) **UIViewSettingsInterop**|
|[**IUserConsentVerifierInterop**](/windows/win32/api/userconsentverifierinterop/nn-userconsentverifierinterop-iuserconsentverifierinterop)|(**Windows.Security.Credentials.UI**) **UserConsentVerifierInterop**|
|[**IWebAuthenticationCoreManagerInterop**](/windows/win32/api/webauthenticationcoremanagerinterop/nn-webauthenticationcoremanagerinterop-iwebauthenticationcoremanagerinterop)|(**Windows.Security.Authentication.Web.Core**) **WebAuthenticationCoreManagerInterop**|
|[**IWindowNative**](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative)|WinUI 3 only<br/><br/>(**WinRT.Interop**) **WindowNative**<br/><br/>For alternatives for WPF and WinForms, see [Retrieve a window handle (HWND)](../../develop/ui-input/retrieve-hwnd.md).|

## Code example

This code example demonstrates how to use two of the C# interop classes in a WinUI 3 application (see [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)). The example scenario is to display a [**Windows.Storage.Pickers.FolderPicker**](/uwp/api/windows.storage.pickers.folderpicker). But before displaying the picker in a desktop app, it's necessary to initialize it with the handle (HWND) of the owner window.

1. You can obtain a window handle (HWND) by using the [**IWindowNative**](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative) WinRT COM interop interface. And (looking in the table in the previous section) that interface is represented by the **WinRT.Interop.WindowNative** C# interop class. Here, the `this` object is a reference to a [**Microsoft.UI.Xaml.Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) object from the main window code-behind file.
2. To initialize a piece of UI with an owner window, you use the [**IInitializeWithWindow**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) WinRT COM interop interface. And that interface is represented by the **WinRT.Interop.InitializeWithWindow** C# interop class.

```csharp
private async void myButton_Click(object sender, RoutedEventArgs e)
{
    // Create a folder picker.
    var folderPicker = new Windows.Storage.Pickers.FolderPicker();

    // 1. Retrieve the window handle (HWND) of the current WinUI 3 window.
    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

    // 2. Initialize the folder picker with the window handle (HWND).
    WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

    // Use the folder picker as usual.
    folderPicker.FileTypeFilter.Add("*");
    var folder = await folderPicker.PickSingleFolderAsync();
}
```

Also see [Retrieve a window handle (HWND)](../../develop/ui-input/retrieve-hwnd.md) and [Display WinRT UI objects that depend on CoreWindow](../../develop/ui-input/display-ui-objects.md).

## Background

Previous versions of the .NET Framework and .NET Core had built-in knowledge of WinRT. With those previous versions, you could define an interop interface directly in C# with the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) attribute, and then directly cast a projected class to that interop interface.

Since WinRT is a Windows-specific technology, to support the portability and efficiency goals of .NET, we lifted the WinRT projection support out of the C# compiler and .NET runtime, and moved it into the [C#/WinRT](/windows/uwp/csharp-winrt/) toolkit (see [Built-in support for WinRT is removed from .NET](/dotnet/core/compatibility/interop/5.0/built-in-support-for-winrt-removed)).

While the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) technique still works for **IUnknown**-based interop interfaces, it no longer works for the [IInspectable](/windows/win32/api/inspectable/nn-inspectable-iinspectable)-based interfaces that are used for interoperating with WinRT.

So as a replacement, in .NET, you can make use of the C# interop classes described in this topic.

## Troubleshooting and known issues

There are currently no known issues for the C# interop classes. To provide feedback, or to report other issues, add your feedback to an existing issue, or file a new issue on the [WindowsAppSDK GitHub repo](https://github.com/microsoft/WindowsAppSDK/issues/new/choose).

## Related topics

* [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)
* [Manage app windows](../../windows-app-sdk/windowing/windowing-overview.md)
* [Retrieve a window handle (HWND)](../../develop/ui-input/retrieve-hwnd.md)
* [Display WinRT UI objects that depend on CoreWindow](../../develop/ui-input/display-ui-objects.md)