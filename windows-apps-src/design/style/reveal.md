---
author: mijacobs
description: Reveal is a lighting effect that helps bring depth and focus to your app's interactive elements.
title: Reveal highlight
template: detail.hbs
ms.author: mijacobs
ms.date: 08/9/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: kisai
design-contact: conrwi
dev-contact: jevansa
doc-status: Published
localizationpriority: high
---
# Reveal highlight

Reveal is a lighting effect that helps bring depth and focus to your app's interactive elements.

> **Important APIs**: [RevealBrush class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrush), [RevealBackgroundBrush class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbackgroundbrush), [RevealBorderBrush class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealborderbrush), [RevealBrushHelper class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrushhelper), [VisualState class](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.VisualState)

The Reveal behavior does this by revealing the clickable content’s container when the pointer is nearby.

![Reveal Visual](images/Nav_Reveal_Animation.gif)

By exposing the hidden borders around objects, Reveal gives users a better understanding of the space that they are interacting with, and helps them understand the actions available. This is especially important in list controls and groupings of buttons.

## Examples

<div style="overflow: hidden; margin: 0 -8px;">
    <div style="float: left; margin: 0 8px 16px; min-width: calc(25% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <div style="height: 133px; width: 100%">
            <img src="images/xaml-controls-gallery.png" alt="XAML controls gallery"></img>
        </div>
    </div>
    <div style="float: left; margin: -22px 8px 16px; min-width: calc(75% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/Reveal">open the app and see Reveal in action</a>.</p>
        <ul>
        <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
        <li><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics">Get the source code (GitHub)</a></li>
        </ul>
    </div>
</div>

## Reveal and the Fluent Design System

 The Fluent Design System helps you create modern, bold UI that incorporates light, depth, motion, material, and scale. Reveal is a Fluent Design System component that adds light to your app. To learn more, see the [Fluent Design for UWP overview](../fluent-design-system/index.md).

## How to use it

Reveal automatically works for some controls. For other controls, you can enable reveal by assigning a special style to the control.

## Controls that automatically use Reveal

- [**ListView**](../controls-and-patterns/lists.md)
- [**GridView**](../controls-and-patterns/lists.md)
- [**TreeView**](../controls-and-patterns/tree-view.md)
- [**NavigationView**](../controls-and-patterns/navigationview.md)
- [**AutosuggestBox**](../controls-and-patterns/auto-suggest-box.md)
- [**MediaTransportControl**](../controls-and-patterns/media-playback.md)
- [**CommandBar**](../controls-and-patterns/app-bars.md)
- [**ComboBox**](../controls-and-patterns/lists.md)

These illustrations show the reveal effect on several different controls:

![Reveal Examples](images/RevealExamples_Collage.png)

## Enabling Reveal on other common controls

If you have a scenario where Reveal should be applied (these controls are main content and/or are used in a list or collection orientation), we've provided opt-in resource styles that allow you to enable Reveal for those types of situations.

These controls do not have Reveal by default as they are smaller controls that are usually helper controls to the main focal points of your application; but every app is different, and if these controls are used the most in your app, we've provided some styles to aid with that:

| Control Name   | Resource Name |
|----------|:-------------:|
| Button |  ButtonRevealStyle |
| ToggleButton | ToggleButtonRevealStyle |
| RepeatButton | RepeatButtonRevealStyle |
| AppBarButton | AppBarButtonRevealStyle |
| SemanticZoom | SemanticZoomRevealStyle |

To apply these styles, simply update the Style property like so:

```XAML
<Button Content="Button Content" Style="{StaticResource ButtonRevealStyle}"/>
```

## Enabling Reveal on custom controls

The general rule to think about when deciding whether or not your custom control should get Reveal, is you must have a grouping of interactive elements that all relate to an overarching feature or action you wish to perform in your app.

For example, NavigationView's items are related to page navigation. CommandBar's buttons relate to menu actions or page feature actions. MediaTransportControl's buttons beneath all relate to the media being played.

The controls that get Reveal do not have to be related to each other, they just have to be in a high-density area, and serve a greater purpose.

To enable Reveal on custom controls or re-templated controls, you can go into the style for that control in the Visual States of that control's template and specify Reveal on the root grid:

```xaml
<VisualState x:Name="PointerOver">
    <VisualState.Setters>
        <Setter Target="RootGrid.(RevealBrush.State)" Value="PointerOver" />
        <Setter Target="RootGrid.Background" Value="{ThemeResource ButtonRevealBackgroundPointerOver}" />
        <Setter Target="ContentPresenter.BorderBrush" Value="Transparent"/>
        <Setter Target="ContentPresenter.Foreground" Value="{ThemeResource ButtonForegroundPointerOver}" />
    </VisualState.Setters>
</VisualState>
```

It's important to note that Reveal needs both the brush and the setters in it's Visual State in order to work fully. Simply setting a control's brush to one of our Reveal brush resources alone won't enable Reveal for that control. Conversely, having only the targets or settings without the values being Reveal brushes will also not enable Reveal.

We've created a set of system Reveal brushes you can use to customize your UI. For example, you can use the **ButtonRevealBackground** brush to create a custom button background, or the **ListViewItemRevealBackground** brush for custom lists, and so on.

(To learn about how resources work in XAMl, check out the [Xaml Resource Dictionary](../controls-and-patterns/resourcedictionary-and-xaml-resource-references.md) article.)

### Reveal on ListView controls with nested buttons

If you have a [ListView](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ListView) and also have buttons or invokable content nested inside its [ListViewItem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.listviewitem) elements, you should enable Reveal for the nested items.

In the case of Buttons, or button-like controls in a ListViewItem, simply set the control's  [Style](https://docs.microsoft.com/uwp/api/windows.ui.xaml.frameworkelement#Windows_UI_Xaml_FrameworkElement_Style) property to the **ButtonRevealStyle** static resource. 

![Nested Reveal](images/NestedListContent.png)

This example enables Reveal on several buttons inside a ListViewItem. 

```XAML
<ListViewItem>
	<StackPanel Orientation="Horizontal">
		<TextBlock Margin="5">Test Text: lorem ipsum.</TextBlock>
        <StackPanel Orientation="Horizontal">
			<Button Content="&#xE71B;" FontFamily="Segoe MDL2 Assets" Width="45" Height="45" Margin="5" Style="{StaticResource ButtonRevealStyle}"/>
			<Button Content="&#xE728;" FontFamily="Segoe MDL2 Assets" Width="45" Height="45" Margin="5" Style="{StaticResource ButtonRevealStyle}"/>
			<Button Content="&#xE74D;" FontFamily="Segoe MDL2 Assets" Width="45" Height="45" Margin="5" Style="{StaticResource ButtonRevealStyle}"/>
         </StackPanel>
    </StackPanel>
</ListViewItem>
```

### ListViewItemPresenter with Reveal

Lists are a bit special in XAML, and for Reveal's case we'll have to define the Visual State Manager for Reveal only inside the ListViewItemPresenter:

```XAML
<ListViewItemPresenter>
<!-- ContentTransitions, SelectedForeground, etc. properties -->
RevealBackground="{ThemeResource ListViewItemRevealBackground}"
RevealBorderThickness="{ThemeResource ListViewItemRevealBorderThemeThickness}"
RevealBorderBrush="{ThemeResource ListViewItemRevealBorderBrush}">
    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="CommonStates">
        <VisualState x:Name="Normal" />
        <VisualState x:Name="Selected" />
        <VisualState x:Name="PointerOver">
            <VisualState.Setters>
                <Setter Target="Root.(RevealBrush.State)" Value="PointerOver" />
            </VisualState.Setters>
            </VisualState>
            <VisualState x:Name="PointerOverSelected">
                <VisualState.Setters>
                    <Setter Target="Root.(RevealBrush.State)" Value="PointerOver" />
                </VisualState.Setters>
            </VisualState>
            <VisualState x:Name="PointerOverPressed">
                <VisualState.Setters>
                    <Setter Target="Root.(RevealBrush.State)" Value="Pressed" />
                </VisualState.Setters>
            </VisualState>
            <VisualState x:Name="Pressed">
                <VisualState.Setters>
                    <Setter Target="Root.(RevealBrush.State)" Value="Pressed" />
                </VisualState.Setters>
            </VisualState>
            <VisualState x:Name="PressedSelected">
                <VisualState.Setters>
                    <Setter Target="Root.(RevealBrush.State)" Value="Pressed" />
                </VisualState.Setters>
            </VisualState>
            </VisualStateGroup>
                <VisualStateGroup x:Name="EnabledGroup">
                    <VisualState x:Name="Enabled" />
                    <VisualState x:Name="Disabled">
                        <VisualState.Setters>
                             <Setter Target="Root.RevealBorderThickness" Value="0"/>
                        </VisualState.Setters>
                    </VisualState>
                </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>
</ListViewItemPresenter>
```

This means appending to the end of the property collection within the ListViewItemPresenter with the Reveal specific Visual State setters.

### Full Template Example

Here's an entire template for what a Reveal Button would look like:

```XAML
<Style TargetType="Button" x:Key="ButtonStyle1">
    <Setter Property="Background" Value="{ThemeResource ButtonRevealBackground}" />
    <Setter Property="Foreground" Value="{ThemeResource ButtonForeground}" />
    <Setter Property="BorderBrush" Value="{ThemeResource ButtonRevealBorderBrush}" />
    <Setter Property="BorderThickness" Value="2" />
    <Setter Property="Padding" Value="8,4,8,4" />
    <Setter Property="HorizontalAlignment" Value="Left" />
    <Setter Property="VerticalAlignment" Value="Center" />
    <Setter Property="FontFamily" Value="{ThemeResource ContentControlThemeFontFamily}" />
    <Setter Property="FontWeight" Value="Normal" />
    <Setter Property="FontSize" Value="{ThemeResource ControlContentThemeFontSize}" />
    <Setter Property="UseSystemFocusVisuals" Value="True" />
    <Setter Property="FocusVisualMargin" Value="-3" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Grid x:Name="RootGrid" Background="{TemplateBinding Background}">

                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal">

                                <Storyboard>
                                    <PointerUpThemeAnimation Storyboard.TargetName="RootGrid" />
                                </Storyboard>
                            </VisualState>

                            <VisualState x:Name="PointerOver">
                                <VisualState.Setters>
                                    <Setter Target="RootGrid.(RevealBrush.State)" Value="PointerOver" />
                                    <Setter Target="RootGrid.Background" Value="{ThemeResource ButtonRevealBackgroundPointerOver}" />
                                    <Setter Target="ContentPresenter.BorderBrush" Value="Transparent"/>
                                    <Setter Target="ContentPresenter.Foreground" Value="{ThemeResource ButtonForegroundPointerOver}" />
                                </VisualState.Setters>

                                <Storyboard>
                                    <PointerUpThemeAnimation Storyboard.TargetName="RootGrid" />
                                </Storyboard>
                            </VisualState>

                            <VisualState x:Name="Pressed">
                                <VisualState.Setters>
                                    <Setter Target="RootGrid.(RevealBrush.State)" Value="Pressed" />
                                    <Setter Target="RootGrid.Background" Value="{ThemeResource ButtonRevealBackgroundPressed}" />
                                    <Setter Target="ContentPresenter.BorderBrush" Value="{ThemeResource ButtonRevealBackgroundPressed}" />
                                    <Setter Target="ContentPresenter.Foreground" Value="{ThemeResource ButtonForegroundPressed}" />
                                </VisualState.Setters>

                                <Storyboard>
                                    <PointerDownThemeAnimation Storyboard.TargetName="RootGrid" />
                                </Storyboard>
                            </VisualState>

                            <VisualState x:Name="Disabled">
                                <VisualState.Setters>
                                    <Setter Target="RootGrid.Background" Value="{ThemeResource ButtonRevealBackgroundDisabled}" />
                                    <Setter Target="ContentPresenter.BorderBrush" Value="{ThemeResource ButtonRevealBorderBrushDisabled}" />
                                    <Setter Target="ContentPresenter.Foreground" Value="{ThemeResource ButtonForegroundDisabled}" />
                                </VisualState.Setters>
                            </VisualState>
                        </VisualStateGroup>

              </VisualStateManager.VisualStateGroups>
                    <ContentPresenter x:Name="ContentPresenter"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    Content="{TemplateBinding Content}"
                    ContentTransitions="{TemplateBinding ContentTransitions}"
                    ContentTemplate="{TemplateBinding ContentTemplate}"
                    Padding="{TemplateBinding Padding}"
                    HorizontalContentAlignment="{TemplateBinding HorizontalContentAlignment}"
                    VerticalContentAlignment="{TemplateBinding VerticalContentAlignment}"
                    AutomationProperties.AccessibilityView="Raw" />
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Do's and don'ts
- Do use Reveal on elements where the user can take action (buttons, selections)
- Do use Reveal in groupings of interactive elements that do not have visual separators by default (lists, command bars)
- Do use Reveal in areas with a high density of interactive elements
- Don’t use Reveal on static content (backgrounds, text)
- Don’t use Reveal in one-off, isolated situations
- Don’t use Reveal on very large items (greater than 500epx)
- Don’t use Reveal in security decisions, as it may draw attention away from the message you need to
  deliver to your user

## How we designed Reveal

There are two main visual components to Reveal: the **Hover Reveal** behavior, and the **Border Reveal** behavior.

![Reveal layers](images/RevealLayers.png)

The Hover Reveal is tied directly to the content being hovered over (via pointer or focus input), and applies a gentle halo shape around the hovered or focused item, letting you know you can interact with it.

The Border Reveal is applied to the focused item and items nearby. This shows you that those nearby objects can take actions similar to the one currently focused.

The Reveal recipe breakdown is:

- Border Reveal will be on top of all content but on the designated edges
- Text and content will be displayed directly under Border Reveal
- Hover Reveal will be beneath content and text
- The backplate (that turns on and enables Hover Reveal)
- The background (background of control)

<!--
<div class=”microsoft-internal-note”>
To create your own Reveal lighting effect for static comps or prototype purposes, see the full [uni design guidance](http://uni/DesignDepot.FrontEnd/#/ProductNav/3020/1/dv/?t=Resources%7CToolkit%7CReveal&f=Neon) for this effect in illustrator.
</div>
-->  

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics) - See all the XAML controls in an interactive format.

## Related articles

- [RevealBrush class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrush)
- [Acrylic](acrylic.md)
- [Composition Effects](https://msdn.microsoft.com/windows/uwp/graphics/composition-effects)
- [Fluent Design for UWP](../fluent-design-system/index.md)
- [Science in the System: Fluent Design and Depth](https://medium.com/microsoft-design/science-in-the-system-fluent-design-and-depth-fb6d0f23a53f)
- [Science in the System: Fluent Design and Light](https://medium.com/microsoft-design/the-science-in-the-system-fluent-design-and-light-94a17e0b3a4f)
