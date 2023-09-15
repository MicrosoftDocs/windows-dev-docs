---
description: Panning and scrolling allows users to reach content that extends beyond the bounds of the screen.
title: Scroll viewer controls
ms.assetid: 1BFF0E81-BF9C-43F7-95F6-EFC6BDD5EC31
label: Scrollbars
template: detail.hbs
ms.date: 06/24/2021
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Scroll viewer controls

When there is more UI content to show than you can fit in an area, use the scroll viewer control.

Scroll viewers enable content to extend beyond the bounds of the viewport (visible area). Users reach this content by manipulating the scroll viewer surface through touch, mousewheel, keyboard, or a gamepad, or by using the mouse or pen cursor to interact with the scroll viewer's scrollbar.

![A screenshot that illustrates a panning scrollbar control](images/scrollbar-panning-1.png)
![A screenshot that illustrates the standard scrollbar control](images/scrollBar-standard-1.png)

Depending on the situation, the scroll viewer's scrollbar uses two different visualizations, shown in the following illustration: the panning indicator (left) and the traditional scrollbar (right).

![A sample of what panning indicator look like on a scrollbar](images/scrollbar-panning.png)
![A sample of what a standard scrollbar looks like](images/scrollbar-traditional.png)

The scroll viewer is conscious of the user's input method and uses it to determine which visualization to display.

* When the region is scrolled without manipulating the scrollbar directly, for example, by touch, the panning indicator appears, displaying the current scroll position.
* When the mouse or pen cursor moves over the panning indicator, it morphs into the traditional scrollbar.  Dragging the scrollbar thumb manipulates the scrolling region.

![Scroll bars in action](images/conscious-scroll.gif)

> [!NOTE]
> When the scrollbar is visible it is overlaid as 16px on top of the content inside your ScrollViewer. In order to ensure good UX design you will want to ensure that no interactive content is obscured by this overlay. Additionally if you would prefer not to have UX overlap, leave 16px of padding on the edge of the viewport to allow for the scrollbar.

## Recommendations

- Whenever possible, design for vertical scrolling rather than horizontal.
- Use one-axis panning for content regions that extend beyond one viewport boundary (vertical or horizontal). Use two-axis panning for content regions that extend beyond both viewport boundaries (vertical and horizontal).
- Use the built-in scroll functionality in the list view, grid view, combo box, list box, text input box, and hub controls. With those controls, if there are too many items to show all at once, the user is able to scroll either horizontally or vertically over the list of items.
- If you want the user to pan in both directions around a larger area, and possibly to zoom, too, for example, if you want to allow the user to pan and zoom over a full-sized image (rather than an image sized to fit the screen) then place the image inside a scroll viewer.
- If the user will scroll through a long passage of text, configure the scroll viewer to scroll vertically only.
- Use a scroll viewer to contain one object only. Note that the one object can be a layout panel, in turn containing any number of objects of its own.
- If you need to handle pointer events for a [UIElement](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.UIElement) in a scrollable view (such as a ScrollViewer or ListView), you must explicitly disable support for manipulation events on the element in the view by calling [UIElement.CancelDirectmanipulation()](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.canceldirectmanipulations). To re-enable manipulation events in the view, call [UIElement.TryStartDirectManipulation()](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.trystartdirectmanipulation).

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

APIs for this control exist in the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) namespace.

> [!div class="checklist"]
>
> - **UWP APIs:** [ScrollViewer class](/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer), [ScrollBar class](/uwp/api/windows.ui.xaml.controls.primitives.scrollbar)
> - [Open the WinUI 2 Gallery app and see the ScrollViewer in action](winui2gallery:/item/ScrollViewer). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles and templates for all controls. WinUI 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md).

## Create a scroll viewer

> [!div class="checklist"]
>
> - **Important APIs:** [ScrollViewer class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ScrollViewer), [ScrollBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.scrollbar)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the ScrollViewer in action](winui3gallery:/item/ScrollViewer).

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

To add vertical scrolling to your page, wrap the page content in a scroll viewer.

```xaml
<Page
    x:Class="App1.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:App1">

    <ScrollViewer>
        <StackPanel>
            <TextBlock Text="My Page Title" Style="{StaticResource TitleTextBlockStyle}"/>
            <!-- more page content -->
        </StackPanel>
    </ScrollViewer>
</Page>
```

This XAML shows how to enable horizontal scrolling, place an image in a scroll viewer and enable zooming.

```xaml
<ScrollViewer ZoomMode="Enabled" MaxZoomFactor="10"
              HorizontalScrollMode="Enabled" HorizontalScrollBarVisibility="Visible"
              Height="200" Width="200">
    <Image Source="Assets/Logo.png" Height="400" Width="400"/>
</ScrollViewer>
```

## ScrollViewer in a control template

It's typical for a ScrollViewer control to exist as a composite part of other controls. A ScrollViewer part, along with the [ScrollContentPresenter](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ScrollContentPresenter) class for support, will display a viewport along with scrollbars only when the host control's layout space is being constrained smaller than the expanded content size. This is often the case for lists, so [ListView](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ListView) and [GridView](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.GridView) templates always include a ScrollViewer. [TextBox](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.TextBox) and [RichEditBox](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.RichEditBox) also include a ScrollViewer in their templates.

When a **ScrollViewer** part exists in a control, the host control often has built-in event handling for certain input events and manipulations that enable the content to scroll. For example, a GridView interprets a swipe gesture and this causes the content to scroll horizontally. The input events and raw manipulations that the host control receives are considered handled by the control, and lower-level events such as [PointerPressed](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.pointerpressed) won't be raised and won't bubble to any parent containers either. You can change some of the built-in control handling by overriding a control class and the **On**_Event_ virtual methods for events, or by retemplating the control. But in either case it's not trivial to reproduce the original default behavior, which is typically there so that the control reacts in expected ways to events and to a user's input actions and gestures. So you should consider whether you really need that input event to fire. You might want to investigate whether there are other input events or gestures that are not being handled by the control, and use those in your app or control interaction design.

To make it possible for controls that include a ScrollViewer to influence some of the behavior and properties that are from within the ScrollViewer part, ScrollViewer defines a number of XAML attached properties that can be set in styles and used in template bindings. For more info about attached properties, see [Attached properties overview](/windows/uwp/xaml-platform/attached-properties-overview).

**ScrollViewer XAML attached properties**

ScrollViewer defines the following XAML attached properties:

- [ScrollViewer.BringIntoViewOnFocusChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.bringintoviewonfocuschange)
- [ScrollViewer.HorizontalScrollBarVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.horizontalscrollbarvisibility)
- [ScrollViewer.HorizontalScrollMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.horizontalscrollmode)
- [ScrollViewer.IsDeferredScrollingEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.isdeferredscrollingenabled)
- [ScrollViewer.IsHorizontalRailEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.ishorizontalrailenabled)
- [ScrollViewer.IsHorizontalScrollChainingEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.ishorizontalscrollchainingenabled)
- [ScrollViewer.IsScrollInertiaEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.isscrollinertiaenabled)
- [ScrollViewer.IsVerticalRailEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.isverticalrailenabled)
- [ScrollViewer.IsVerticalScrollChainingEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.isverticalscrollchainingenabled)
- [ScrollViewer.IsZoomChainingEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.iszoomchainingenabled)
- [ScrollViewer.IsZoomInertiaEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.iszoominertiaenabled)
- [ScrollViewer.VerticalScrollBarVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.verticalscrollbarvisibilityproperty)
- [ScrollViewer.VerticalScrollMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.verticalscrollmode)
- [ScrollViewer.ZoomMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer.zoommode)

These XAML attached properties are intended for cases where the ScrollViewer is implicit, such as when the ScrollViewer exists in the default template for a ListView or GridView, and you want to be able to influence the scrolling behavior of the control without accessing template parts.

For example, here's how to make the vertical scrollbars always visible for a ListView's built in scroll viewer.

```xaml
<ListView ScrollViewer.VerticalScrollBarVisibility="Visible"/>
```

For cases where a ScrollViewer is explicit in your XAML, as is shown in the example code, you don't need to use attached property syntax. Just use attribute syntax, for example `<ScrollViewer VerticalScrollBarVisibility="Visible"/>`.

## Get the sample code

- [WinUI Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.

## Related topics

* [ScrollViewer class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ScrollViewer)
