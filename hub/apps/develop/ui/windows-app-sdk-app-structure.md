---
title: Structure a modern WinUI 3 desktop app
description: Combine Mica backdrop, custom title bar, NavigationView, and InfoBar for a polished WinUI 3 desktop app with Windows 11 chrome.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/15/2026
---

# Structure a modern WinUI 3 desktop app

This article shows you how to assemble the standard chrome elements of a modern Windows desktop app using WinUI 3. By combining a few foundational components, your app gets the polished look users expect on Windows 11: a translucent Mica backdrop, integrated title bar, sidebar navigation, and consistent status messaging.

## The pattern at a glance

A typical modern WinUI 3 desktop app uses these elements:

| Element | Purpose |
|---------|---------|
| [Mica backdrop](#add-mica-backdrop) | Translucent material that connects the window to the desktop wallpaper |
| [Custom TitleBar](#set-up-a-custom-title-bar) | App icon + title in the draggable title area, replacing the default caption bar |
| [NavigationView (Left pane)](#add-navigationview) | Primary navigation shell with a left sidebar |
| [InfoBar](#use-infobar-for-status-messages) | Non-modal status and error messages |
| Transparent page backgrounds | Pages don't set their own background, allowing Mica to show through |

The following screenshot shows these elements working together in a sample app:

:::image type="content" source="images/winui3-app-structure-example.png" alt-text="Screenshot of a WinUI 3 desktop app showing Mica backdrop, custom title bar, left NavigationView pane, InfoBar notification, and content cards.":::

## Add Mica backdrop

Mica is the recommended backdrop for primary app windows. In WinUI 3, you enable it with a single XAML element:

```xaml
<Window ...>
    <Window.SystemBackdrop>
        <MicaBackdrop />
    </Window.SystemBackdrop>

    <!-- Window content goes here -->
</Window>
```

No code-behind is required. The system handles fallback on older Windows versions automatically.

> [!TIP]
> Don't set a `Background` on your `Window`, `NavigationView`, or page `Grid` elements. Any opaque background covers the Mica effect. If you need section backgrounds, use semi-transparent brushes from the theme resources (such as `CardBackgroundFillColorDefaultBrush`).

For more options and C++ examples, see [Apply Mica or Acrylic materials in desktop apps](system-backdrops.md).

## Set up a custom title bar

Extend your app content into the title bar area for a seamless look. Set this up in your `MainWindow` constructor or `Loaded` handler:

```csharp
public MainWindow()
{
    InitializeComponent();

    // Extend content into the title bar area
    ExtendsContentIntoTitleBar = true;

    // Tell the system which element is the draggable title bar region
    SetTitleBar(AppTitleBar);
}
```

In XAML, define the title bar element:

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" /> <!-- Title bar row -->
        <RowDefinition Height="*" />    <!-- Content row -->
    </Grid.RowDefinitions>

    <!-- Custom title bar: right padding reserves space for caption buttons -->
    <Grid x:Name="AppTitleBar" Height="48"
          Padding="16,0,188,0">
        <StackPanel Orientation="Horizontal" Spacing="12">
            <Image Source="/Assets/AppIcon.png" Width="16" Height="16"
                   VerticalAlignment="Center" />
            <TextBlock Text="My App" Style="{StaticResource CaptionTextBlockStyle}"
                       VerticalAlignment="Center" />
        </StackPanel>
    </Grid>

    <!-- Main content below the title bar -->
    <NavigationView Grid.Row="1" ... />
</Grid>
```

> [!IMPORTANT]
> Keep app identity in the title bar only. Don't repeat the app name or icon in the page content, because this creates visual clutter and wastes vertical space.

> [!NOTE]
> When you extend content into the title bar, you must account for the system caption button area (minimize, maximize, close). The hard-coded right padding of `188` in the example above is a reasonable default for 100% scale. For a precise, DPI-aware approach, read `AppWindow.TitleBar.RightInset` and `AppWindow.TitleBar.LeftInset` at runtime and update the padding when the layout changes. For details, see [Title bar customization](../title-bar.md).

For the full title bar API reference, see [Title bar customization](../title-bar.md).

## Add NavigationView

Use [NavigationView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.navigationview) with `PaneDisplayMode="Left"` as your top-level app shell. This replaces the UWP-era `SplitView` pattern.

```xaml
<NavigationView x:Name="NavView"
                PaneDisplayMode="Left"
                IsBackButtonVisible="Collapsed"
                SelectionChanged="NavView_SelectionChanged">
    <NavigationView.MenuItems>
        <NavigationViewItem Icon="Home" Content="Home" Tag="home" />
        <NavigationViewItem Icon="Library" Content="Library" Tag="library" />
        <NavigationViewItem Icon="Setting" Content="Settings" Tag="settings" />
    </NavigationView.MenuItems>

    <!-- Page content appears here -->
    <Frame x:Name="ContentFrame" />
</NavigationView>
```

```csharp
private void NavView_SelectionChanged(NavigationView sender,
    NavigationViewSelectionChangedEventArgs args)
{
    if (args.SelectedItemContainer?.Tag is string tag)
    {
        Type pageType = tag switch
        {
            "home" => typeof(HomePage),
            "library" => typeof(LibraryPage),
            "settings" => typeof(SettingsPage),
            _ => typeof(HomePage)
        };
        ContentFrame.Navigate(pageType);
    }
}
```

For complete NavigationView guidance, see [NavigationView control](/windows/apps/design/controls/navigationview).

## Use InfoBar for status messages

Replace colored status borders, toast-like popups, or custom status bars with [InfoBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobar). It provides a consistent, accessible way to show informational, success, warning, or error messages inline.

```xaml
<InfoBar x:Name="StatusInfoBar"
         Title="Update available"
         Message="Version 2.1 is ready to install."
         Severity="Informational"
         IsOpen="True"
         IsClosable="True" />
```

Place `InfoBar` at the top of your content area (inside the `NavigationView` but above the `Frame`) so it's visible regardless of which page is loaded.

For usage patterns, see [InfoBar control](/windows/apps/design/controls/infobar).

## Transparent page backgrounds

To let Mica show through your pages, don't set explicit backgrounds on your page root elements:

```xaml
<!-- Do this — no Background attribute -->
<Page ...>
    <Grid Padding="24">
        <!-- Page content -->
    </Grid>
</Page>

<!-- Don't do this -->
<Page Background="{ThemeResource ApplicationPageBackgroundThemeBrush}" ...>
```

If individual content cards need a background, use the layered `CardBackgroundFillColorDefaultBrush` from the theme dictionary. It's semi-transparent and works with Mica.

## MainWindow.xaml example

Here's a starter `MainWindow.xaml` that brings the elements together. For production use, add caption button inset handling as described in [Title bar customization](../title-bar.md).

```xaml
<Window
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Window.SystemBackdrop>
        <MicaBackdrop />
    </Window.SystemBackdrop>

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>

        <!-- Title bar -->
        <Grid x:Name="AppTitleBar" Height="48">
            <StackPanel Orientation="Horizontal" Spacing="12" Margin="16,0">
                <Image Source="/Assets/AppIcon.png" Width="16" Height="16"
                       VerticalAlignment="Center" />
                <TextBlock Text="My App" Style="{StaticResource CaptionTextBlockStyle}"
                           VerticalAlignment="Center" />
            </StackPanel>
        </Grid>

        <!-- Navigation shell -->
        <NavigationView x:Name="NavView"
                        Grid.Row="1"
                        PaneDisplayMode="Left"
                        IsBackButtonVisible="Collapsed"
                        SelectionChanged="NavView_SelectionChanged">
            <NavigationView.MenuItems>
                <NavigationViewItem Icon="Home" Content="Home" Tag="home" />
                <NavigationViewItem Icon="Library" Content="Library" Tag="library" />
                <NavigationViewItem Icon="Setting" Content="Settings" Tag="settings" />
            </NavigationView.MenuItems>

            <Grid>
                <Grid.RowDefinitions>
                    <RowDefinition Height="Auto" />
                    <RowDefinition Height="*" />
                </Grid.RowDefinitions>

                <!-- Status bar -->
                <InfoBar x:Name="StatusInfoBar"
                         IsOpen="False" />

                <!-- Page host -->
                <Frame x:Name="ContentFrame" Grid.Row="1" />
            </Grid>
        </NavigationView>
    </Grid>
</Window>
```

## Related content

- [Apply Mica or Acrylic materials in desktop apps](system-backdrops.md)
- [Title bar customization](../title-bar.md)
- [NavigationView control](/windows/apps/design/controls/navigationview)
- [InfoBar control](/windows/apps/design/controls/infobar)
- [App silhouette patterns](/windows/apps/design/basics/app-silhouette)
