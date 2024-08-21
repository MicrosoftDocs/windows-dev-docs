---
title: Using a SystemBackdropController with WinUI 3 XAML 
description: Sample code for applying Mica in a WinUI 3 application.
ms.topic: article
ms.date: 07/15/2024
keywords: windows, windows app development, Windows App SDK, Mica
ms.localizationpriority: medium
dev_langs: 
- csharp
- cppwinrt
---

# Apply Mica or Acrylic materials in desktop apps for Windows 11

[Materials in Windows 11](../design/signature-experiences/materials.md) are visual effects applied to UX surfaces that resemble real life artifacts. Occluding materials, like Mica and Acrylic, are used as base layers beneath interactive UI controls.

[Mica](../design/style/mica.md) is an opaque material that incorporates the user's theme and desktop wallpaper to create a highly personalized appearance. Mica is designed for performance as it only captures the background wallpaper once to create its visualization, so we recommend it for the foundation layer of your app, especially in the title bar area.

[Acrylic](../design/style/acrylic.md) is a semi-transparent material that replicates the effect of frosted glass. It's used only for transient, light-dismiss surfaces such as flyouts and context menus.

This article describes how to apply Mica or Acrylic as the base layer of your Windows App SDK/WinUI 3 XAML app.

> [!NOTE]
>
> - To use an in-app AcrylicBrush, see [Acrylic material](../design/style/acrylic.md).
> - To use backdrop materials in a Win32 app, see [Apply Mica in Win32 desktop apps for Windows 11](../desktop/modernize/ui/apply-mica-win32.md).
> - To use backdrop materials in a UWP/WinUI 2 app, see [Apply Mica with WinUI 2 for UWP](/windows/uwp/ui-input/mica-uwp) or [Acrylic material](../design/style/acrylic.md).

## How to use a backdrop material

> [!div class="checklist"]
>
> - **Important APIs**: [Window.SystemBackdrop property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop), [MicaBackdrop class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop), [DesktopAcrylicBackdrop class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.desktopacrylicbackdrop), [SystemBackdropConfiguration class](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.systembackdropconfiguration)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the System Backdrops in action](winui3gallery://item/SystemBackdrops).

[!INCLUDE [winui-3-gallery](../../includes/winui-3-gallery.md)]

To apply Mica or Acrylic material to your app, you set the `SystemBackdrop` property to a XAML [SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.systembackdrop) (typically, one of the built-in backdrops, [MicaBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop) or [DesktopAcrylicBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.desktopacrylicbackdrop)).

These elements have a `SystemBackdrop` property:

- [CommandBarFlyoutCommandBar.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.commandbarflyoutcommandbar.systembackdrop)
- [ContentIsland.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentisland.systembackdrop)
- [DesktopWindowXamlSource.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.hosting.desktopwindowxamlsource.systembackdrop)
- [FlyoutBase.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.flyoutbase.systembackdrop)
- [MenuFlyoutPresenter.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutpresenter.systembackdrop)
- [Popup.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.popup.systembackdrop)
- [Window.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop)

These examples show how to set the system backdrop in XAML and in code.

### Mica

Mica is typically used as the backdrop for an app Window.

```xaml
<Window
    ... >

    <Window.SystemBackdrop>
        <MicaBackdrop Kind="BaseAlt"/>
    </Window.SystemBackdrop>

</Window>
```

```csharp
public MainWindow()
{
    this.InitializeComponent();

    SystemBackdrop = new MicaBackdrop() 
                        { Kind = MicaKind.BaseAlt };
}
```

### Acrylic

Acrylic is typically used as the backdrop for transient UI, like a flyout.

```xaml
<Flyout
    ... >

    <Flyout.SystemBackdrop>
        <DesktopAcrylicBackdrop/>
    </Flyout.SystemBackdrop>
</Flyout>
```

```csharp
Flyout flyout = new Flyout()
{
    SystemBackdrop = new DesktopAcrylicBackdrop()
};
```

## How to use a system backdrop controller

> [!NOTE]
> Starting with Windows App SDK 1.3, you can apply material by setting the `Window.SystemBackdrop` property to a XAML `SystemBackdrop` as described in the previous section. This is the recommended way to apply a material.
>
> The remainder of this article shows how to use the Composition [MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) and [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller) APIs.

To use a backdrop material in your app, you can use one of the controllers that implements the [ISystemBackdropController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.isystembackdropcontroller) interface ([MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) or [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller)). These classes manage both the rendering of the system backdrop material as well as the handling of system policy for the material.

To use Mica as your backdrop material, create a [MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) object. To use Acrylic, create a [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller) object. The set up and supporting code is the same for each type of system backdrop material.

This code shows how to create a `MicaController`.

```csharp
MicaController m_backdropController;

bool TrySetSystemBackdrop()
{
    if (MicaController.IsSupported())
    {
        ...
        m_backdropController = new MicaController();
        ...
    }
}
```

```cppwinrt
// namespace MUCSB = Microsoft::UI::Composition::SystemBackdrops;

winrt::MUCSB::MicaController m_backdropController{ nullptr };

void SetBackground()
{
    if (winrt::MUCSB::MicaController::IsSupported())
    {
        ...
        m_backdropController = winrt::MUCSB::MicaController();
        ...
    }
}
```

To use the Mica Alt variant of Mica, create a `MicaController` object and set the [Kind](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.kind) property to [MicaKind.BaseAlt](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micakind).

```csharp
MicaController m_backdropController;

bool TrySetSystemBackdrop()
{
    if (MicaController.IsSupported())
    {
        ...
        m_backdropController = new MicaController()
        {
            Kind = MicaKind.BaseAlt
        };
        ...
    }
}
```

```cppwinrt
// namespace MUCSB = Microsoft::UI::Composition::SystemBackdrops;

winrt::MUCSB::MicaController m_backdropController{ nullptr };

void SetBackground()
{
    if (winrt::MUCSB::MicaController::IsSupported())
    {
        ...
        m_backdropController = winrt::MUCSB::MicaController();
        m_backdropController.Kind(winrt::MUCSB::MicaKind::BaseAlt);
        ...
    }
}
```

This code shows how to create a `DesktopAcrylicController`.

```csharp
DesktopAcrylicController m_backdropController;

bool TrySetSystemBackdrop()
{
    if (DesktopAcrylicController.IsSupported())
    {
        ...
        m_backdropController = new DesktopAcrylicController();
        ...
    }
}
```

```cppwinrt
// namespace MUCSB = Microsoft::UI::Composition::SystemBackdrops;

winrt::MUCSB::DesktopAcrylicController m_backdropController{ nullptr };

void SetBackground()
{
    if (winrt::MUCSB::DesktopAcrylicController::IsSupported())
    {
        ...
        m_backdropController = winrt::MUCSB::DesktopAcrylicController();
        ...
    }
}
```

The controller reacts to the system Light and Dark themes by default. To override this behavior, you can set the following properties on the controller:

- [FallbackColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.fallbackcolor)
- [LuminosityOpacity](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.luminosityopacity)
- [TintColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.tintcolor)
- [TintOpacity](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.tintopacity)

In order to use the backdrop material in your app, the following items are required:

- **System support**

   The system where the app runs must support the backdrop material. Call the [MicaController.IsSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.issupported) or [DesktopAcrylicController.IsSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller.issupported) method to ensure the backdrop material is supported at runtime.

- **A valid target**

   You must provide a target that implements the [ICompositionSupportsSystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.icompositionsupportssystembackdrop) interface. In a XAML app, the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) implements this interface and is used as the backdrop target.

- **A SystemBackdropConfiguration object**

   The [SystemBackdropConfiguration](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.systembackdropconfiguration) provides the system backdrop controller with app-specific policy information to properly configure the system backdrop material.

- **A DispatcherQueue object**.

   You need an available [Windows.System.DispatcherQueue](/uwp/api/windows.system.dispatcherqueue) on the main XAML thread. See the `WindowsSystemDispatcherQueueHelper` class in the example code, or in the [WinUI 3 Gallery sample](https://github.com/microsoft/WinUI-Gallery/blob/8cb9da922c48c5ea2e3190c579ebb77c75a2ca20/WinUIGallery/SamplePages/SampleSystemBackdropsWindow.xaml.cs#L11).

## Example: Use Mica in a Windows AppSDK/WinUI 3 app

This example shows how to set up the Mica backdrop material in a XAML app.

> [!TIP]
>Also, see these example projects on GitHub:
>
> **C#**: [SampleSystemBackdropsWindow in the WinUI3 Gallery](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/SamplePages/SampleSystemBackdropsWindow.xaml.cs).
>
> **C++/WinRT**: [Windows App SDK Mica sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-win32).

```csharp
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using System.Runtime.InteropServices; // For DllImport
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()

public sealed partial class MainWindow : Window
{
    WindowsSystemDispatcherQueueHelper m_wsdqHelper; // See below for implementation.
    MicaController m_backdropController;
    SystemBackdropConfiguration m_configurationSource;

    public MainWindow()
    {
        this.InitializeComponent();

        TrySetSystemBackdrop();
    }

    bool TrySetSystemBackdrop()
    {
        if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
        {
            m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
            m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

            // Create the policy object.
            m_configurationSource = new SystemBackdropConfiguration();
            this.Activated += Window_Activated;
            this.Closed += Window_Closed;
            ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;
            SetConfigurationSourceTheme();

            m_backdropController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_backdropController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_backdropController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Mica is not supported on this system
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

    private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed
            // so it doesn't try to use this closed window.
            if (m_backdropController != null)
            {
                m_backdropController.Dispose();
                m_backdropController = null;
            }
            this.Activated -= Window_Activated;
            m_configurationSource = null;
        }

    private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                SetConfigurationSourceTheme();
            }
        }

    private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }
        }
}

class WindowsSystemDispatcherQueueHelper
{
    [StructLayout(LayoutKind.Sequential)]
    struct DispatcherQueueOptions
    {
        internal int dwSize;
        internal int threadType;
        internal int apartmentType;
    }

    [DllImport("CoreMessaging.dll")]
    private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

    object m_dispatcherQueueController = null;
    public void EnsureWindowsSystemDispatcherQueueController()
    {
        if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
        {
            // one already exists, so we'll just use it.
            return;
        }

        if (m_dispatcherQueueController == null)
        {
            DispatcherQueueOptions options;
            options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
            options.threadType = 2;    // DQTYPE_THREAD_CURRENT
            options.apartmentType = 2; // DQTAT_COM_STA

            CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
        }
    }
}
```

```cppwinrt
// pch.h
...
#include <winrt/Microsoft.UI.Composition.SystemBackdrops.h>
#include <winrt/Windows.System.h>
#include <dispatcherqueue.h>

// MainWindow.xaml.h
...
namespace winrt
{
    namespace MUC = Microsoft::UI::Composition;
    namespace MUCSB = Microsoft::UI::Composition::SystemBackdrops;
    namespace MUX = Microsoft::UI::Xaml;
    namespace WS = Windows::System;
}
...
struct MainWindow : MainWindowT<MainWindow>
{
    winrt::MUCSB::SystemBackdropConfiguration m_configuration{ nullptr };
    winrt::MUCSB::MicaController m_backdropController{ nullptr };
    winrt::MUX::Window::Activated_revoker m_activatedRevoker;
    winrt::MUX::Window::Closed_revoker m_closedRevoker;
    winrt::MUX::FrameworkElement::ActualThemeChanged_revoker m_themeChangedRevoker;
    winrt::MUX::FrameworkElement m_rootElement{ nullptr };
    winrt::WS::DispatcherQueueController m_dispatcherQueueController{ nullptr };

    MainWindow::MainWindow()
    {
        InitializeComponent();

        SetBackground();

        m_closedRevoker = this->Closed(winrt::auto_revoke, [&](auto&&, auto&&)
        {
            if (nullptr != m_backdropController)
            {
                m_backdropController.Close();
                m_backdropController = nullptr;
            }

            if (nullptr != m_dispatcherQueueController)
            {
                m_dispatcherQueueController.ShutdownQueueAsync();
                m_dispatcherQueueController = nullptr;
            }
        });
    }

    void SetBackground()
    {
        if (winrt::MUCSB::MicaController::IsSupported())
        {
            // We ensure that there is a Windows.System.DispatcherQueue on the current thread.
            // Always check if one already exists before attempting to create a new one.
            if (nullptr == winrt::WS::DispatcherQueue::GetForCurrentThread() &&
                nullptr == m_dispatcherQueueController)
            {
                m_dispatcherQueueController = CreateSystemDispatcherQueueController();
            }

            // Setup the SystemBackdropConfiguration object.
            SetupSystemBackdropConfiguration();

            // Setup Mica on the current Window.
            m_backdropController = winrt::MUCSB::MicaController();
            m_backdropController.SetSystemBackdropConfiguration(m_configuration);
            m_backdropController.AddSystemBackdropTarget(
                this->m_inner.as<winrt::MUC::ICompositionSupportsSystemBackdrop>());
        }
        else
        {
            // The backdrop material is not supported.
        }
    }

    winrt::WS::DispatcherQueueController CreateSystemDispatcherQueueController()
    {
        DispatcherQueueOptions options
        {
            sizeof(DispatcherQueueOptions),
            DQTYPE_THREAD_CURRENT,
            DQTAT_COM_NONE
        };

        ::ABI::Windows::System::IDispatcherQueueController* ptr{ nullptr };
        winrt::check_hresult(CreateDispatcherQueueController(options, &ptr));
        return { ptr, take_ownership_from_abi };
    }

    void SetupSystemBackdropConfiguration()
    {
        m_configuration = winrt::MUCSB::SystemBackdropConfiguration();

        // Activation state.
        m_activatedRevoker = this->Activated(winrt::auto_revoke,
            [&](auto&&, MUX::WindowActivatedEventArgs const& args)
            {
                m_configuration.IsInputActive(
                    winrt::MUX::WindowActivationState::Deactivated != args.WindowActivationState());
            });

        // Initial state.
        m_configuration.IsInputActive(true);

        // Application theme.
        m_rootElement = this->Content().try_as<winrt::MUX::FrameworkElement>();
        if (nullptr != m_rootElement)
        {
            m_themeChangedRevoker = m_rootElement.ActualThemeChanged(winrt::auto_revoke,
                [&](auto&&, auto&&)
                {
                    m_configuration.Theme(
                        ConvertToSystemBackdropTheme(m_rootElement.ActualTheme()));
                });

            // Initial state.
            m_configuration.Theme(
                ConvertToSystemBackdropTheme(m_rootElement.ActualTheme()));
        }
    }

    winrt::MUCSB::SystemBackdropTheme ConvertToSystemBackdropTheme(
        winrt::MUX::ElementTheme const& theme)
    {
        switch (theme)
        {
        case winrt::MUX::ElementTheme::Dark:
            return winrt::MUCSB::SystemBackdropTheme::Dark;
        case winrt::MUX::ElementTheme::Light:
            return winrt::MUCSB::SystemBackdropTheme::Light;
        default:
            return winrt::MUCSB::SystemBackdropTheme::Default;
        }
    }
    ...
};
...
```

## Related articles

- [Materials in Windows 11](../design/signature-experiences/materials.md)
- [Mica](../design/style/mica.md)
- [Acrylic](../design/style/acrylic.md)
- [Apply Mica in Win32 desktop apps for Windows 11](../desktop/modernize/ui/apply-mica-win32.md)
