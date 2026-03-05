---
title: System backdrops (Mica/Acrylic)
description: Learn how to apply Mica or Desktop Acrylic system backdrops to your WinUI app windows.
ms.topic: how-to
ms.date: 02/27/2026
keywords: windows, windows app development, Windows App SDK, Mica, Acrylic, backdrop
ms.localizationpriority: medium
dev_langs: 
- csharp
- cppwinrt
---


# Apply Mica or Acrylic materials in desktop apps for Windows 11

[Materials in Windows 11](../../design/signature-experiences/materials.md) are visual effects applied to UX surfaces that resemble real life artifacts. Occluding materials, like Mica and Acrylic, are used as base layers beneath interactive UI controls.

[Mica](../../design/style/mica.md) is an opaque material that incorporates the user's theme and desktop wallpaper to create a highly personalized appearance. Mica is designed for performance as it only captures the background wallpaper once to create its visualization, so we recommend it for the foundation layer of your app, especially in the title bar area.

[Acrylic](../../design/style/acrylic.md) is a semi-transparent material that replicates the effect of frosted glass. It's used only for transient, light-dismiss surfaces such as flyouts and context menus.

This article describes how to apply Mica or Acrylic as the base layer of your Windows App SDK/WinUI 3 XAML app.

> [!NOTE]
>
> - To use backdrop materials in a Win32 app, see [Apply Mica in Win32 desktop apps for Windows 11](../../desktop/modernize/ui/apply-mica-win32.md).
> - For in-app acrylic applied to UI elements (not the window background), see [In-app acrylic](in-app-acrylic.md).

## How to use a backdrop material

> [!div class="checklist"]
>
> - **Important APIs**: [Window.SystemBackdrop property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop), [MicaBackdrop class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop), [DesktopAcrylicBackdrop class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.desktopacrylicbackdrop), [SystemBackdropConfiguration class](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.systembackdropconfiguration)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the System Backdrops in action](winui3gallery://item/SystemBackdrops)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

To apply Mica or Acrylic material to your app, you set the `SystemBackdrop` property to a XAML [SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.systembackdrop) (typically, one of the built-in backdrops, [MicaBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop) or [DesktopAcrylicBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.desktopacrylicbackdrop)).

These elements have a `SystemBackdrop` property:

- [CommandBarFlyoutCommandBar.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.commandbarflyoutcommandbar.systembackdrop)
- [ContentIsland.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentisland.systembackdrop)
- [DesktopWindowXamlSource.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.hosting.desktopwindowxamlsource.systembackdrop)
- [FlyoutBase.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.flyoutbase.systembackdrop)
- [MenuFlyoutPresenter.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutpresenter.systembackdrop)
- [Popup.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.popup.systembackdrop)
- [Window.SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop)

These examples show how to set the system backdrop in XAML.

### Mica

Mica is typically used as the backdrop for an app Window.

```xaml
<Window
    ... >

    <Window.SystemBackdrop>
        <MicaBackdrop/>
    </Window.SystemBackdrop>

</Window>
```

### Mica Alt

To use the Mica Alt variant, set the `Kind` property to `BaseAlt`.

```xaml
<Window
    ... >

    <Window.SystemBackdrop>
        <MicaBackdrop Kind="BaseAlt"/>
    </Window.SystemBackdrop>

</Window>
```

### Acrylic

Desktop Acrylic can be used as the backdrop for a Window.

```xaml
<Window
    ... >

    <Window.SystemBackdrop>
        <DesktopAcrylicBackdrop/>
    </Window.SystemBackdrop>

</Window>
```

### Acrylic on transient UI

Acrylic is also commonly used as the backdrop for transient UI, like a flyout. You can set this in XAML or in code, since flyouts are often created dynamically.

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

## Advanced: How to use a system backdrop controller

> [!NOTE]
> Starting with Windows App SDK 1.3, you can apply material by setting the `Window.SystemBackdrop` property to a XAML `SystemBackdrop` as described in the previous section. This is the recommended way to apply a material for most apps.
>
> The remainder of this article shows how to use the Composition [MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) and [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller) APIs, which give you more control over the backdrop behavior.

To use a backdrop material in your app, you can use one of the controllers that implements the [ISystemBackdropController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.isystembackdropcontroller) interface ([MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) or [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller)). These classes manage both the rendering of the system backdrop material as well as the handling of system policy for the material.

To use Mica as your backdrop material, create a [MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) object. To use Acrylic, create a [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller) object. The set up and supporting code is the same for each type of system backdrop material.

### Create a MicaController

#### [C#](#tab/cs)

```csharp
MicaController micaController;

bool TrySetMicaBackdrop(bool useMicaAlt)
{
    if (MicaController.IsSupported())
    {
        ...
        micaController = new MicaController();
        micaController.Kind = useMicaAlt ? MicaKind.BaseAlt : MicaKind.Base;
        ...
    }
}
```

#### [C++](#tab/cpp)

```cppwinrt
// namespace MUCSB = Microsoft::UI::Composition::SystemBackdrops;

winrt::MUCSB::MicaController m_backdropController{ nullptr };

void SetBackground(bool useMicaAlt)
{
    if (winrt::MUCSB::MicaController::IsSupported())
    {
        ...
        m_backdropController = winrt::MUCSB::MicaController();
        m_backdropController.Kind(useMicaAlt 
            ? winrt::MUCSB::MicaKind::BaseAlt 
            : winrt::MUCSB::MicaKind::Base);
        ...
    }
}
```

---

### Create a DesktopAcrylicController

#### [C#](#tab/cs2)

```csharp
DesktopAcrylicController acrylicController;

bool TrySetAcrylicBackdrop(bool useAcrylicThin)
{
    if (DesktopAcrylicController.IsSupported())
    {
        ...
        acrylicController = new DesktopAcrylicController();
        acrylicController.Kind = useAcrylicThin 
            ? DesktopAcrylicKind.Thin : DesktopAcrylicKind.Base;
        ...
    }
}
```

#### [C++](#tab/cpp2)

```cppwinrt
// namespace MUCSB = Microsoft::UI::Composition::SystemBackdrops;

winrt::MUCSB::DesktopAcrylicController m_backdropController{ nullptr };

void SetBackground(bool useAcrylicThin)
{
    if (winrt::MUCSB::DesktopAcrylicController::IsSupported())
    {
        ...
        m_backdropController = winrt::MUCSB::DesktopAcrylicController();
        m_backdropController.Kind(useAcrylicThin 
            ? winrt::MUCSB::DesktopAcrylicKind::Thin 
            : winrt::MUCSB::DesktopAcrylicKind::Base);
        ...
    }
}
```

---

The controller reacts to the system Light and Dark themes by default. To override this behavior, you can set the following properties on the controller:

- [FallbackColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.fallbackcolor)
- [LuminosityOpacity](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.luminosityopacity)
- [TintColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.tintcolor)
- [TintOpacity](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.tintopacity)

> [!NOTE]
>
> After customizing any of the controller's four properties, it no longer applies default Light or Dark values when the associated [SystemBackdropConfiguration.Theme](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.systembackdropconfiguration.theme) changes. You need to manually update those properties to match the new theme.

In order to use the backdrop material in your app, the following items are required:

- **System support**

   The system where the app runs must support the backdrop material. Call the [MicaController.IsSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.issupported) or [DesktopAcrylicController.IsSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller.issupported) method to ensure the backdrop material is supported at runtime.

- **A valid target**

   You must provide a target that implements the [ICompositionSupportsSystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.icompositionsupportssystembackdrop) interface. In a XAML app, the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) implements this interface and is used as the backdrop target.

- **A SystemBackdropConfiguration object**

   The [SystemBackdropConfiguration](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.systembackdropconfiguration) provides the system backdrop controller with app-specific policy information to properly configure the system backdrop material.

- **A DispatcherQueue object**

   You need an available [Windows.System.DispatcherQueue](/uwp/api/windows.system.dispatcherqueue) on the main XAML thread. Call `DispatcherQueue.EnsureSystemDispatcherQueue()` to ensure one exists.

## Example: Use Mica with a controller in a WinUI app

This example shows how to set up the Mica backdrop material using a controller in a XAML app.

> [!TIP]
> Also, see these example projects on GitHub:
>
> **C#**: [SampleSystemBackdropsWindow in the WinUI Gallery](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/SamplePages/SampleSystemBackdropsWindow.xaml.cs).
>
> **C++/WinRT**: [Windows App SDK Mica sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-win32).

#### [C#](#tab/cs3)

```csharp
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()

public sealed partial class MainWindow : Window
{
    MicaController micaController;
    SystemBackdropConfiguration configurationSource;

    public MainWindow()
    {
        this.InitializeComponent();

        TrySetMicaBackdrop(false);
    }

    bool TrySetMicaBackdrop(bool useMicaAlt)
    {
        if (MicaController.IsSupported())
        {
            DispatcherQueue.EnsureSystemDispatcherQueue();

            // Hooking up the policy object.
            configurationSource = new SystemBackdropConfiguration();
            Activated += Window_Activated;
            Closed += Window_Closed;
            ((FrameworkElement)Content).ActualThemeChanged += Window_ThemeChanged;

            // Initial configuration state.
            configurationSource.IsInputActive = true;
            SetConfigurationSourceTheme();

            micaController = new MicaController
            {
                Kind = useMicaAlt ? MicaKind.BaseAlt : MicaKind.Base
            };

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            micaController.AddSystemBackdropTarget(
                this.As<ICompositionSupportsSystemBackdrop>());
            micaController.SetSystemBackdropConfiguration(configurationSource);
            return true; // Succeeded.
        }

        return false; // Mica is not supported on this system.
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        if (configurationSource != null)
            configurationSource.IsInputActive = 
                args.WindowActivationState != WindowActivationState.Deactivated;
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        // Make sure any Mica/Acrylic controller is disposed
        // so it doesn't try to use this closed window.
        micaController?.Dispose();
        micaController = null;
        Activated -= Window_Activated;
        configurationSource = null;
    }

    private void Window_ThemeChanged(FrameworkElement sender, object args)
    {
        if (configurationSource != null)
            SetConfigurationSourceTheme();
    }

    private void SetConfigurationSourceTheme()
    {
        if (configurationSource != null)
            configurationSource.Theme = 
                (SystemBackdropTheme)((FrameworkElement)Content).ActualTheme;
    }
}
```

#### [C++](#tab/cpp3)

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

---

## Related articles

- [Materials overview](materials.md)
- [In-app acrylic](in-app-acrylic.md)
- [Materials in Windows 11](../../design/signature-experiences/materials.md)
- [Mica](../../design/style/mica.md)
- [Acrylic](../../design/style/acrylic.md)
- [Apply Mica in Win32 desktop apps for Windows 11](../../desktop/modernize/ui/apply-mica-win32.md)
