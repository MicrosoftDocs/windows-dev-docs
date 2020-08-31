---
description: Customize the title bar of a desktop app to match the personality of the app.
title: Title bar customization
template: detail.hbs
ms.date: 10/10/2017
ms.topic: article
keywords: windows 10, uwp, title bar
doc-status: Draft
ms.localizationpriority: medium
---
# Title bar customization



When your app is running in a desktop window, you can customize the title bars to match the personality of your app. The title bar customization APIs let you specify colors for title bar elements, or extend your app content into the title bar area and take full control of it.

> **Important APIs**: [ApplicationView.TitleBar property](/uwp/api/windows.ui.viewmanagement.applicationview), [ApplicationViewTitleBar class](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar), [CoreApplicationViewTitleBar class](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar)

## How much to customize the title bar

There are two levels of customization that you can apply to the title bar.

For simple color customization, you can set [ApplicationViewTitleBar](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar) properties to specify the colors you want to use for title bar elements. In this case, the system retains responsibility for all other aspects of the title bar, such as drawing the app title and defining draggable areas.

Your other option is to hide the default title bar and replace it with your own XAML content. For example, you can place text, buttons, or custom menus in the title bar area. You will also need to use this option to extend an [acrylic](../style/acrylic.md) background into the title bar area.

When you opt for full customization, you are responsible for putting content in the title bar area, and you can define your own draggable region. The system Back, Close, Minimize, and Maximize buttons are still available and handled by the system, but elements like the app title are not. You will need to create those elements yourself as needed by your app.

> [!NOTE]
> Simple color customization is available to Windows apps using XAML, DirectX, and HTML. Full customization is available only to Windows apps using XAML.

## Simple color customization

If you want only to customize the title bar colors and not do anything too fancy (such as putting tabs in your title bar), you can set the color properties on the [ApplicationViewTitleBar](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar) for your app window.

This example shows how to get an instance of ApplicationViewTitleBar and set its color properties.

```csharp
// using Windows.UI.ViewManagement;

var titleBar = ApplicationView.GetForCurrentView().TitleBar;

// Set active window colors
titleBar.ForegroundColor = Windows.UI.Colors.White;
titleBar.BackgroundColor = Windows.UI.Colors.Green;
titleBar.ButtonForegroundColor = Windows.UI.Colors.White;
titleBar.ButtonBackgroundColor = Windows.UI.Colors.SeaGreen;
titleBar.ButtonHoverForegroundColor = Windows.UI.Colors.White;
titleBar.ButtonHoverBackgroundColor = Windows.UI.Colors.DarkSeaGreen;
titleBar.ButtonPressedForegroundColor = Windows.UI.Colors.Gray;
titleBar.ButtonPressedBackgroundColor = Windows.UI.Colors.LightGreen;

// Set inactive window colors
titleBar.InactiveForegroundColor = Windows.UI.Colors.Gray;
titleBar.InactiveBackgroundColor = Windows.UI.Colors.SeaGreen;
titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.Gray;
titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.SeaGreen;
```

> [!NOTE]
> This code can be placed in your app's [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method (_App.xaml.cs_), after the call to [Window.Activate](/uwp/api/windows.ui.xaml.window.Activate), or in your app's first page.

> [!TIP]
> The Windows Community Toolkit provides extensions that let you set these color properties in XAML. For more info, see the [Windows Community Toolkit documentation](/windows/uwpcommunitytoolkit/extensions/viewextensions).

There are a few things to be aware of when setting title bar colors:

- The button background color is not applied to the close button hover and pressed states. The close button always uses the system-defined color for those states.
- The button color properties are applied to the system back button when it's used. ([See Navigation history and backwards navigation](../basics/navigation-history-and-backwards-navigation.md).)
- Setting a color property to **null** resets it to the default system color.
- You can't set transparent colors. The color's alpha channel is ignored.

Windows gives a user the option to apply their selected [accent color](../style/color.md#accent-color) to the title bar. If you set any title bar color, we recommend that you explicitly set all the colors. This ensures that there are no unintended color combinations that occur because of user defined color settings.

## Full customization

When you opt-in to full title bar customization, your app’s client area is extended to cover the entire window, including the title bar area. You are responsible for drawing and input-handling for the entire window except the caption buttons, which are overlaid on top of the app’s canvas.

To hide the default title bar and extend your content into the title bar area, set the [CoreApplicationViewTitleBar.ExtendViewIntoTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar) property to **true**.

This example shows how to get the CoreApplicationViewTitleBar and set the ExtendViewIntoTitleBar property to **true**. This can be done in your app's [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method (_App.xaml.cs_), or in your app's first page.

```csharp
// using Windows.ApplicationModel.Core;

// Hide default title bar.
var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
coreTitleBar.ExtendViewIntoTitleBar = true;
```

> [!TIP]
> This setting persists when your app is closed and restarted. In Visual Studio, if you set ExtendViewIntoTitleBar to **true**, and then want to revert to the default, you should explicitly set it to **false** and run your app to overwrite the persisted setting.

### Draggable regions

The draggable region of the title bar defines where the user can click and drag to move the window around (as opposed to simply dragging content within the app’s canvas). You specify the draggable region by calling the [Window.SetTitleBar](/uwp/api/windows.ui.xaml.window.settitlebar) method and passing in a UIElement that defines the draggable region. (The UIElement is often a panel that contains other elements.)

Here's how to set a Grid of content as the draggable title bar region. This code goes in the XAML and code-behind for your app's first page. See the [Full customization example](./title-bar.md#full-customization-example) section for the full code.


> [!IMPORTANT]
> By default, some UI elements such as Grid do not participate in hit testing when they don't have a background set.
>  For the grid `AppTitleBar` in the sample below to allow dragging, we therefore need to set the background to `Transparent`.

```xaml
<Grid x:Name="AppTitleBar" Background="Transparent">
    <!-- Width of the padding columns is set in LayoutMetricsChanged handler. -->
    <!-- Using padding columns instead of Margin ensures that the background
         paints the area under the caption control buttons (for transparent buttons). -->
    <Grid.ColumnDefinitions>
        <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
        <ColumnDefinition/>
        <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
    </Grid.ColumnDefinitions>
    <Image Source="Assets/Square44x44Logo.png" 
           Grid.Column="1" HorizontalAlignment="Left" 
           Width="20" Height="20" Margin="12,0"/>
    <TextBlock Text="Custom Title Bar" 
               Grid.Column="1" 
               Style="{StaticResource CaptionTextBlockStyle}" 
               Margin="44,8,0,0"/>
</Grid>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;
    coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
    // Set XAML element as a draggable region.
    Window.Current.SetTitleBar(AppTitleBar);
}

private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    AppTitleBar.Height = sender.Height;
}
```

The UIElement (`AppTitleBar`) is part of the XAML for your app. You can either declare and set the title bar in a root page that doesn’t change, or declare and set a title bar region in each page that your app can navigate to. If you set it in each page, you should make sure the draggable region stays consistent as a user navigates around your app.

You can call SetTitleBar to switch to a new title bar element while your app is running. You can also pass **null** as the parameter to SetTitleBar to revert to the default dragging behavior. (See "Default draggable region" for more info.)

> [!IMPORTANT]
> The draggable region you specify needs to be hit testable, which means, for some elements, you might need to set a transparent background brush. See the remarks on [VisualTreeHelper.FindElementsInHostCoordinates](/uwp/api/windows.ui.xaml.media.visualtreehelper.findelementsinhostcoordinates) for more info.
>
>For example, if you define a Grid as your draggable region, set `Background="Transparent"` to make it draggable.
>
>This Grid is not draggable (but visible elements within it are): `<Grid x:Name="AppTitleBar">`.
>
>This Grid looks the same, but the whole Grid is draggable: `<Grid x:Name="AppTitleBar" Background="Transparent">`.

#### Default draggable region

If you don’t specify a draggable region, a rectangle that is the width of the window, the height of the caption buttons, and positioned along the top edge of the window is set as the default draggable region.

If you do define a draggable region, the system shrinks the default draggable region down to a small area the size of a caption button, positioned to the left of the caption buttons (or to the right if the captions buttons are on the left side of the window). This ensures that there is always a consistent area the user can drag.

### System caption buttons

The system reserves the upper-left or upper-right corner of the app window for the system caption buttons (Back, Minimize, Maximize, Close). The system retains control of the caption control area to guarantee that minimum functionality is provided for dragging, minimizing, maximizing, and closing the window. The system draws the Close button in the upper-right for left-to-right languages and the upper-left for right-to-left languages.

The dimensions and position of the caption control area is communicated by the CoreApplicationViewTitleBar class so that you can account for it in the layout of your title bar UI. The width of the reserved region on each side is given by the [SystemOverlayLeftInset](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.SystemOverlayLeftInset) or [SystemOverlayRightInset](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.SystemOverlayRightInset) properties, and its height is given by the [Height](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.Height) property.

You can draw content underneath the caption control area defined by these properties, such as your app background, but you should not put any UI that you expect the user to be able to interact with. It does not receive any input because input for the caption controls is handled by the system.

You can handle the [LayoutMetricsChanged](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.LayoutMetricsChanged) event to respond to changes in the size of the caption buttons. For example, this can happen when the system back button is shown or hidden. Handle this event to verify and update the positioning of UI elements that depend on the title bar's size.

This example shows how to adjust the layout of your title bar to account for changes like the system back button being shown or hidden. `AppTitleBar`, `LeftPaddingColumn`, and `RightPaddingColumn` are declared in the XAML shown previously.

```csharp
private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    UpdateTitleBarLayout(sender);
}

private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
{
    // Get the size of the caption controls area and back button 
    // (returned in logical pixels), and move your content around as necessary.
    LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
    RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
    TitleBarButton.Margin = new Thickness(0,0,coreTitleBar.SystemOverlayRightInset,0);

    // Update title bar control size as needed to account for system size changes.
    AppTitleBar.Height = coreTitleBar.Height;
}
```

### Interactive content

You can place interactive controls, like buttons, menus, or a search box, in the top part of the app so they appear to be in the title bar. However, there are a few rules you must follow to ensure that your interactive elements receive user input.
- You must call SetTitleBar to define an area as the draggable title bar region. If you don’t, the system sets the default draggable region at the top of the page. The system will then handle all user input to this area, and prevent input from reaching your controls.
- Place your interactive controls over the top of the draggable region defined by the call to SetTitleBar (with a higher z-order). Don’t make your interactive controls children of the UIElement passed to SetTitleBar. After you pass an element to SetTitleBar, the system treats it like the system title bar and handles all pointer input to that element.

Here, the `TitleBarButton` element has a higher z-order than `AppTitleBar`, so it receives user input.

```xaml
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition />
    </Grid.RowDefinitions>

    <Grid x:Name="AppTitleBar" Background="Transparent">
        <!-- Width of the padding columns is set in LayoutMetricsChanged handler. -->
        <!-- Using padding columns instead of Margin ensures that the background
             paints the area under the caption control buttons (for transparent buttons). -->
        <Grid.ColumnDefinitions>
            <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
            <ColumnDefinition/>
            <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
        </Grid.ColumnDefinitions>
        <Image Source="Assets/Square44x44Logo.png"
               Grid.Column="1" HorizontalAlignment="Left"
               Width="20" Height="20" Margin="12,0"/>
        <TextBlock Text="Custom Title Bar"
                   Grid.Column="1"
                   Style="{StaticResource CaptionTextBlockStyle}"
                   Margin="44,8,0,0"/>
    </Grid>

    <!-- This Button has a higher z-order than AppTitleBar, 
         so it receives user input. -->
    <Button x:Name="TitleBarButton" Content="Button in the title bar"
        HorizontalAlignment="Right"/>
</Grid>
```

### Transparency in caption buttons

When you set ExtendViewIntoTitleBar to **true**, you can make the background of the caption buttons transparent to let your app background show through. You typically set the background to [Colors.Transparent](/uwp/api/windows.ui.colors.Transparent) for full transparency. For partial transparency, set the alpha channel for the [Color](/uwp/api/windows.ui.color) you set the property to.

These ApplicationViewTitleBar properties can be transparent:

- ButtonBackgroundColor
- ButtonHoverBackgroundColor
- ButtonPressedBackgroundColor
- ButtonInactiveBackgroundColor

The button background color is not applied to the close button hover and pressed states. The close button always uses the system-defined color for those states.

All other color properties will continue to ignore the alpha channel. If ExtendViewIntoTitleBar is set to **false**, the alpha channel is always ignored for all ApplicationViewTitleBar color properties.

### Full screen and tablet mode

When your app runs in _full screen_ or _tablet mode_, the system hides the title bar and caption control buttons. However, the user can invoke the title bar to have it shown as an overlay on top of the app’s UI.
You can handle the [CoreApplicationViewTitleBar.IsVisibleChanged](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.IsVisibleChanged) event to be notified when the title bar is hidden or invoked, and show or hide your custom title bar content as needed.

This example shows how to handle IsVisibleChanged to show and hide the `AppTitleBar` element shown previously.

```csharp
public MainPage()
{
    this.InitializeComponent();

    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

    // Register a handler for when the title bar visibility changes.
    // For example, when the title bar is invoked in full screen mode.
    coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
}

private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
{
    if (sender.IsVisible)
    {
        AppTitleBar.Visibility = Visibility.Visible;
    }
    else
    {
        AppTitleBar.Visibility = Visibility.Collapsed;
    }
}
```

>[!NOTE]
>_Full screen_ mode can be entered only if supported by your app. See [ApplicationView.IsFullScreenMode](/uwp/api/windows.ui.viewmanagement.applicationview.IsFullScreenMode) for more info. [_Tablet mode_](https://support.microsoft.com/help/17210/windows-10-use-your-pc-like-a-tablet) is a user option on supported hardware, so a user can choose to run any app in tablet mode.

## Full customization example

```xaml
<Page
    x:Class="CustomTitleBar.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:CustomTitleBar"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition />
        </Grid.RowDefinitions>

        <Grid x:Name="AppTitleBar" Background="Transparent">
            <!-- Width of the padding columns is set in LayoutMetricsChanged handler. -->
            <!-- Using padding columns instead of Margin ensures that the background
                 paints the area under the caption control buttons (for transparent buttons). -->
            <Grid.ColumnDefinitions>
                <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
                <ColumnDefinition/>
                <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
            </Grid.ColumnDefinitions>
            <Image Source="Assets/Square44x44Logo.png" 
                   Grid.Column="1" HorizontalAlignment="Left" 
                   Width="20" Height="20" Margin="12,0"/>
            <TextBlock Text="Custom Title Bar" 
                       Grid.Column="1" 
                       Style="{StaticResource CaptionTextBlockStyle}" 
                       Margin="44,8,0,0"/>
        </Grid>

        <!-- This Button has a higher z-order than MyTitleBar, 
             so it receives user input. -->
        <Button x:Name="TitleBarButton" Content="Button in the title bar"
                HorizontalAlignment="Right"/>
    </Grid>
</Page>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    // Hide default title bar.
    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;
    UpdateTitleBarLayout(coreTitleBar);

    // Set XAML element as a draggable region.
    Window.Current.SetTitleBar(AppTitleBar);

    // Register a handler for when the size of the overlaid caption control changes.
    // For example, when the app moves to a screen with a different DPI.
    coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

    // Register a handler for when the title bar visibility changes.
    // For example, when the title bar is invoked in full screen mode.
    coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
}

private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    UpdateTitleBarLayout(sender);
}

private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
{
    // Get the size of the caption controls area and back button 
    // (returned in logical pixels), and move your content around as necessary.
    LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
    RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
    TitleBarButton.Margin = new Thickness(0,0,coreTitleBar.SystemOverlayRightInset,0);

    // Update title bar control size as needed to account for system size changes.
    AppTitleBar.Height = coreTitleBar.Height;
}

private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
{
    if (sender.IsVisible)
    {
        AppTitleBar.Visibility = Visibility.Visible;
    }
    else
    {
        AppTitleBar.Visibility = Visibility.Collapsed;
    }
}
```

## Do's and don'ts

- Do make it obvious when your window is active or inactive. At a minimum, change the color of the text, icons, and buttons in your title bar.
- Do define a draggable region along the top edge of the app canvas. Matching the placement of system title bars makes it easier for users to find.
- Do define a draggable region that matches the visual title bar (if any) on the app’s canvas.

## Related articles

- [Acrylic](../style/acrylic.md)
- [Color](../style/color.md)