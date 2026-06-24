---
description: TwoPaneView is a layout control that helps you manage the display of apps that have 2 distinct areas of content.
title: Two-pane view
template: detail.hbs
ms.date: 04/29/2025
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Two-pane view

Two-pane view is a layout control that helps you manage the display of apps that have 2 distinct areas of content, like a list/detail view.

## Is this the right control?

Use the two-pane view when you have two distinct but related areas of content and:

- The content should automatically rearrange and resize to best fit the window.
- The secondary area of content should show/hide based on available space.

If you need to display two areas of content but don't need the resizing and rearranging provided by the two-pane view, consider using a [Split view](split-view.md) instead.

For navigation options, use a [Navigation view](navigationview.md).


## How it works

The two-pane view has two panes where you place your content. It adjusts the size and arrangement of the panes depending on the space available to the window. The possible pane layouts are defined by the [TwoPaneViewMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneviewmode) enumeration:

| Enum&nbsp;value | Description |
| - | - |
| `SinglePane` | Only one pane is shown, as specified by the [PanePriority](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.panepriority) property. |
| `Wide` | Panes are shown side-by-side, or a single pane is shown, as specified by the [WideModeConfiguration](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.widemodeconfiguration) property. |
| `Tall` | Panes are shown top-bottom, or a single pane is shown, as specified by the [TallModeConfiguration](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.tallmodeconfiguration) property. |

:::image type="content" source="images/two-pane-view/tpv-dual-wide.png" alt-text="Two-pane view app in wide mode, with a photo of a mountain on the left and information about the photo on the right.":::

> _App in wide mode._

:::image type="content" source="images/two-pane-view/tpv-dual-tall.png" alt-text="Two-pane view app in tall mode, with a photo of a mountain on the top and information about the photo on the bottom.":::

> _App in tall mode._

You configure the two-pane view by setting the [PanePriority](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.panepriority) to specify which pane is shown when there is space for only one pane. Then, you specify whether `Pane1` is shown on the top or bottom for tall windows, or on the left or right for wide windows.

The two-pane view handles the size and arrangement of the panes, but you still need to make the content inside the pane adapt to the changes in size and orientation. See [Responsive layouts with XAML](../layouts-with-xaml.md) and [Layout panels](../layout-panels.md) for more info about creating an adaptive UI.

## Create a two-pane view

> [!div class="checklist"]
>
> - **Important APIs:** [TwoPaneView class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview)

This XAML shows how to create a basic `TwoPaneView`.

```xaml
<TwoPaneView>
    <TwoPaneView.Pane1>
        <Grid Background="{ThemeResource LayerFillColorDefaultBrush}">
            <TextBlock Text="Pane 1" Margin="24"
                       Style="{ThemeResource HeaderTextBlockStyle}"/>
        </Grid>
    </TwoPaneView.Pane1>

    <TwoPaneView.Pane2>
        <Grid Background="{ThemeResource LayerFillColorAltBrush}">
            <TextBlock Text="Pane 2" Margin="24"
                       Style="{ThemeResource HeaderTextBlockStyle}"/>
        </Grid>
    </TwoPaneView.Pane2>
</TwoPaneView>
```

![Two-pane view with panes set to default sizes](images/two-pane-view/tpv-size-default.png)

The [TwoPaneView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview) doesn't have to be the root element of your page layout. In fact, you'll often use it inside a [NavigationView](/uwp/api/microsoft.ui.xaml.controls.navigationview) control that provides the overall navigation for your app. The `TwoPaneView` adapts appropriately regardless of where it is in the XAML tree.

### Add content to the panes

Each pane of a two-pane view can hold a single XAML `UIElement`. To add content, you typically place a XAML layout panel in each pane, and then add other controls and content to the panel. The panes can change size and switch between wide and tall modes, so you need to make sure the content in each pane can adapt to these changes. See [Responsive layouts with XAML](../layouts-with-xaml.md) and [Layout panels](../layout-panels.md) for more info about creating an adaptive UI.

This example creates the simple picture/info app UI shown previously. The content can be shown in two panes, or combined into a single pane, depending on how much space is available. (When there's only space for one pane, you move the content of Pane2 into Pane1, and let the user scroll to see any hidden content. You'll see the code for this later in the _Responding to mode changes_ section.)

![Small image of example app spanned on dual-screens](images/two-pane-view/tpv-left-right.png)

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" MinHeight="40"/>
        <RowDefinition Height="*"/>
    </Grid.RowDefinitions>

    <CommandBar DefaultLabelPosition="Right">
        <AppBarButton x:Name="Share" Icon="Share" Label="Share" Click="Share_Click"/>
        <AppBarButton x:Name="Print" Icon="Print" Label="Print" Click="Print_Click"/>
    </CommandBar>

    <TwoPaneView
        x:Name="MyTwoPaneView"
        Grid.Row="1"
        MinWideModeWidth="959"
        MinTallModeHeight="863"
        ModeChanged="TwoPaneView_ModeChanged">

        <TwoPaneView.Pane1>
            <Grid x:Name="Pane1Root">
                <ScrollViewer>
                    <StackPanel x:Name="Pane1StackPanel">
                        <Image Source="Assets\LandscapeImage8.jpg"
                               VerticalAlignment="Top" HorizontalAlignment="Center"
                               Margin="16,0"/>
                    </StackPanel>
                </ScrollViewer>
            </Grid>
        </TwoPaneView.Pane1>

        <TwoPaneView.Pane2
            <Grid x:Name="Pane2Root">
                <ScrollViewer x:Name="DetailsContent">
                    <StackPanel Padding="16">
                        <TextBlock Text="Mountain.jpg" MaxLines="1"
                                       Style="{ThemeResource HeaderTextBlockStyle}"/>
                        <TextBlock Text="Date Taken:"
                                       Style="{ThemeResource SubheaderTextBlockStyle}"
                                       Margin="0,24,0,0"/>
                        <TextBlock Text="8/29/2019 9:55am"
                                       Style="{ThemeResource SubtitleTextBlockStyle}"/>
                        <TextBlock Text="Dimensions:"
                                       Style="{ThemeResource SubheaderTextBlockStyle}"
                                       Margin="0,24,0,0"/>
                        <TextBlock Text="800x536"
                                       Style="{ThemeResource SubtitleTextBlockStyle}"/>
                        <TextBlock Text="Resolution:"
                                       Style="{ThemeResource SubheaderTextBlockStyle}"
                                       Margin="0,24,0,0"/>
                        <TextBlock Text="96 dpi"
                                       Style="{ThemeResource SubtitleTextBlockStyle}"/>
                        <TextBlock Text="Description:"
                                       Style="{ThemeResource SubheaderTextBlockStyle}"
                                       Margin="0,24,0,0"/>
                        <TextBlock Text="Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna."
                                       Style="{ThemeResource SubtitleTextBlockStyle}"
                                       TextWrapping="Wrap"/>
                    </StackPanel>
                </ScrollViewer>
            </Grid>
        </TwoPaneView.Pane2>
    </TwoPaneView>

    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="TwoPaneViewStates">
            <VisualState x:Name="Normal"/>
            <VisualState x:Name="Wide">
                <VisualState.Setters>
                    <Setter Target="MyTwoPaneView.Pane1Length"
                            Value="2*"/>
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>
</Grid>
```

### Specify which pane to display

When the two-pane view can only display a single pane, it uses the [PanePriority](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.panepriority) property to determine which pane to display. By default, PanePriority is set to **Pane1**. Here's how you can set this property in XAML or in code.

```xaml
<TwoPaneView x:Name="MyTwoPaneView" PanePriority="Pane2">
```

```csharp
MyTwoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
```

### Pane sizing

The size of the panes is determined by the [Pane1Length](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.pane1length) and [Pane2Length](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.pane2length) properties. These use [GridLength](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.gridlength) values that support _auto_ and _star_(\*) sizing. See the _Layout properties_ section of [Responsive layouts with XAML](../layouts-with-xaml.md#layout-properties) for an explanation of auto and star sizing.

By default, `Pane1Length` is set to `Auto` and it sizes itself to fit its content. `Pane2Length` is set to `*` and it uses all the remaining space.

![Two-pane view with panes set to default sizes](images/two-pane-view/tpv-size-default.png)

> _Panes with default sizing_

The default values are useful for a typical list/detail layout, where you have a list of items in `Pane1`, and a lot of details in `Pane2`. However, depending on your content, you might prefer to divide the space differently. Here, `Pane1Length` is set to `2*` so it gets twice as much space as `Pane2`.

```xaml
<TwoPaneView x:Name="MyTwoPaneView" Pane1Length="2*">
```

![Two-pane view with pane 1 using two-thirds of screen, and pane 2 using one-third](images/two-pane-view/tpv-size-2.png)

> _Panes sized 2* and *_

If you set a pane to use auto sizing, you can control the size by setting the height and width of the `Panel` that holds the pane's content. In this case, you might need to handle the `ModeChanged` event and set the height and width constraints of the content as appropriate for the current mode.

### Display in wide or tall mode

On a single screen, the two-pane view's display [Mode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.mode) is determined by the [MinWideModeWidth](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.minwidemodewidth) and [MinTallModeHeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.mintallmodeheight) properties. Both properties have a default value of 641px, the same as [NavigationView.CompactThresholdWidth](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.navigationview.compactmodethresholdwidth).

This table shows how the `Height` and `Width` of the `TwoPaneView` determine which display mode is used.

| TwoPaneView condition  | Mode |
|---------|---------|
| `Width` > `MinWideModeWidth` | `Wide` mode is used |
| `Width` <= `MinWideModeWidth`, and `Height` > `MinTallModeHeight` | `Tall` mode is used |
| `Width` <= `MinWideModeWidth`, and `Height` <= `MinTallModeHeight` | `SinglePane` mode is used |

#### Wide configuration options

`MinWideModeWidth` controls when the two-pane view enters wide mode. The two-pane view enters `Wide` mode when the available space is wider than the `MinWideModeWidth` property. The default value is 641px, but you can change it to whatever you want. In general, you should set this property to whatever you want the minimum width of your pane to be.

When the two-pane view is in wide mode, the [WideModeConfiguration](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.widemodeconfiguration) property determines what to show:

| [Enum&nbsp;value](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneviewwidemodeconfiguration) | Description |
|---------|---------|
| `SinglePane` | A single pane (as determined by `PanePriority`). The pane takes up the full size of the `TwoPaneView` (ie, it's star sized in both directions). |
| `LeftRight` | `Pane1` on the left/`Pane2` on the right. Both panes are star sized vertically, `Pane1`'s width is autosized, and `Pane2`'s width is star sized. |
| `RightLeft` | `Pane1` on the right/`Pane2` on the left. Both panes are star sized vertically, `Pane2`'s width is autosized, and `Pane1`'s width is star sized. |

The default setting is `LeftRight`.

| LeftRight | RightLeft |
| - | - |
| ![Two-pane view configured left-right](images/two-pane-view/tpv-left-right.png)  | ![Two-pane view configured right-left](images/two-pane-view/tpv-right-left.png)  |

> [!NOTE]
> When the device uses a right-to-left (RTL) language, the two-pane view automatically swaps the order: `RightLeft` renders as `LeftRight`, and `LeftRight` renders as `RightLeft`.

#### Tall configuration options

The two-pane view enters `Tall` mode when the available space is narrower than `MinWideModeWidth`, and taller than `MinTallModeHeight`. The default value is 641px, but you can change it to whatever you want. In general, you should set this property to whatever you want the minimum height of your pane to be.

When the two-pane view is in tall mode, the [TallModeConfiguration](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.tallmodeconfiguration) property determines what to show:

| [Enum&nbsp;value](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneviewtallmodeconfiguration) | Description |
|---------|---------|
| `SinglePane` | A single pane (as determined by `PanePriority`). The pane takes up the full size of the `TwoPaneView` (ie, it's star sized in both directions). |
| `TopBottom` | `Pane1` on the top/`Pane2` on the bottom. Both panes are star sized horizontally, `Pane1`'s height is autosized, and `Pane2`'s height is star sized. |
| `BottomTop` | `Pane1` on the bottom/`Pane2` on the top. Both panes are star sized horizontally, `Pane2`'s height is autosized, and `Pane1`'s height is star sized. |

The default is `TopBottom`.

| TopBottom | BottomTop |
| - | - |
| ![Two-pane view configured top-bottom](images/two-pane-view/tpv-top-bottom.png)  | ![Two-pane view configured bottom-top](images/two-pane-view/tpv-bottom-top.png)  |

#### Special values for MinWideModeWidth and MinTallModeHeight

You can use the `MinWideModeWidth` property to prevent the two-pane view from entering `Wide` mode - just set `MinWideModeWidth` to [Double.PositiveInfinity](/dotnet/api/system.double.positiveinfinity?view=dotnet-uwp-10.0&preserve-view=true).

If you set `MinTallModeHeight` to [Double.PositiveInfinity](/dotnet/api/system.double.positiveinfinity?view=dotnet-uwp-10.0&preserve-view=true), it prevents the two-pane view from entering `Tall` mode.

If you set `MinTallModeHeight` to 0, it prevents the two-pane view from entering `SinglePane` mode.

#### Responding to mode changes

You can use the read-only [Mode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.mode) property to get the current display mode. Whenever the two-pane view changes which pane or panes it's displaying, the [ModeChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.twopaneview.modechanged) event occurs before it renders the updated content. You can handle the event to respond to changes in the display mode.

> [!IMPORTANT]
> The `ModeChanged` event does not occur when the page is initially loaded, so your default XAML should represent the UI as it should appear when first loaded.

One way you can use this event is to update your app's UI so users can view all the content in `SinglePane` mode. For example, the example app has a primary pane (the image) and an info pane.

![Small image of example app spanned in tall mode](images/two-pane-view/tpv-top-bottom.png)

> _Tall mode_

When there's only enough space to display one pane, you can move the content of `Pane2` into `Pane1` so the user can scroll to see all the content. It looks like this.

![Image of sample app on one screen with all content scrolling in a single pane](images/two-pane-view/tpv-single-pane.png)

> _SinglePane mode_

Remember that the `MinWideModeWidth` and `MinTallModeHeight` properties determine when the display mode changes, so you can change when the content is moved between panes by adjusting the values of these properties.

Here's the `ModeChanged` event handler code that moves the content between `Pane1` and `Pane2`. It also sets a [VisualState](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visualstate) to constrain the width of the image in `Wide` mode.

```csharp
private void TwoPaneView_ModeChanged(TwoPaneView sender, object args)
{
    // Remove details content from it's parent panel.
    ((Panel)DetailsContent.Parent).Children.Remove(DetailsContent);
    // Set Normal visual state.
    VisualStateManager.GoToState(this, "Normal", true);

    // Single pane
    if (sender.Mode == TwoPaneViewMode.SinglePane)
    {
        // Add the details content to Pane1.
        Pane1StackPanel.Children.Add(DetailsContent);
    }
    // Dual pane.
    else
    {
        // Put details content in Pane2.
        Pane2Root.Children.Add(DetailsContent);

        // If also in Wide mode, set Wide visual state
        // to constrain the width of the image to 2*.
        if (sender.Mode == TwoPaneViewMode.Wide)
        {
            VisualStateManager.GoToState(this, "Wide", true);
        }
    }
}
```

## Related articles

- [Layout overview](../../../design/layout/index.md)
- [Split view](split-view.md)
