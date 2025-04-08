---
title: WinUI 2.0 Release Notes
description: Release notes for WinUI 2.0.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
ms.date: 07/15/2020
ms.topic: article
---

# WinUI 2.0

WinUI 2.0 is the first public release of WinUI (released October 2018).

WinUI is the easiest way to build great Fluent Design experiences for Windows.

It includes two NuGet packages:

* [Microsoft.UI.Xaml](https://www.nuget.org/packages/Microsoft.UI.Xaml): Controls and Fluent Design for UWP apps. This is the main WinUI package.

* [Microsoft.UI.Xaml.Core.Direct](https://www.nuget.org/packages/Microsoft.UI.Xaml.Core.Direct): Low-level APIs for use in middleware components.

You can download and use WinUI packages in your app using the NuGet package manager: see [Getting Started with WinUI](/uwp/toolkits/winui/getting-started) for more information.

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports, feature requests and community code contributions.

## Microsoft.UI.Xaml 2.0.181011001

October 2018

This is the first release of the [Microsoft.UI.Xaml NuGet package](https://www.nuget.org/packages/Microsoft.UI.Xaml). It includes official native Fluent controls and features for Windows UWP apps.

### New Features

Controls and patterns in this release include:

| Feature | Description |
| --- | --- |
|[AcrylicBrush]( /uwp/api/microsoft.ui.xaml.media.acrylicbrush)| Paints an area with a semi-transparent material that uses multiple effects including blur and a noise texture.|
|[BitmapIconSource]( /uwp/api/microsoft.ui.xaml.controls.bitmapiconsource)| Represents an icon source that uses a bitmap as its content.|
|[ColorPicker]( /uwp/api/microsoft.ui.xaml.controls.colorpicker)| Represents a control that lets a user pick a color using a color spectrum, sliders, and text input.|
|[CommandBarFlyout](/uwp/api/microsoft.ui.xaml.controls.commandbarflyout)|Represents a specialized flyout that provides layout for AppBarButton and related command elements.|
|[DropDownButton](/uwp/api/microsoft.ui.xaml.controls.dropdownbutton)|Represents a button with a chevron intended to open a menu.|
|[FontIconSource ](/uwp/api/microsoft.ui.xaml.controls.fonticonsource)|Represents an icon source that uses a glyph from the specified font.|
|[MenuBar](/uwp/api/microsoft.ui.xaml.controls.menubar)|Represents a specialized container that presents a set of menus in a horizontal row, typically at the top of an app window.|
|[MenuBarItem](/uwp/api/microsoft.ui.xaml.controls.menubaritem)|Represents a top-level menu in a MenuBar control.|
|[NavigationView](/uwp/api/microsoft.ui.xaml.controls.navigationview)|Represents a container that enables navigation of app content. It has a header, a view for the main content, and a menu pane for navigation commands.|
|[ParallaxView](/uwp/api/microsoft.ui.xaml.controls.parallaxview)|Represents a container that ties the scroll position of a foreground element, such as a list, to a background element, such as an image. As you scroll through the foreground element, it animates the background element to create a parallax effect.|
|[PersonPicture](/uwp/api/microsoft.ui.xaml.controls.personpicture)|Represents a control that displays the avatar image for a person, if one is available; if not, it displays the person's initials or a generic glyph.|
|[RatingControl](/uwp/api/microsoft.ui.xaml.controls.ratingcontrol)|Represents a control that lets a user enter a star rating.|
|[RefreshContainer](/uwp/api/microsoft.ui.xaml.controls.refreshcontainer)|Represents a container control that provides a RefreshVisualizer and pull-to-refresh functionality for scrollable content.|
|[RefreshVisualizer](/uwp/api/microsoft.ui.xaml.controls.refreshvisualizer)|Represents a control that provides animated state indicators for content refresh.|
|[RevealBackgroundBrush](/uwp/api/microsoft.ui.xaml.media.revealbackgroundbrush)|Paints a control background with a reveal effect using composition brush and light effects.|
|[RevealBorderBrush](/uwp/api/microsoft.ui.xaml.media.revealborderbrush)|Paints a control border with a reveal effect using composition brush and light effects.|
|[RevealBrush](/uwp/api/microsoft.ui.xaml.media.revealbrush)|Base class for brushes that use composition effects and lighting to implement the reveal visual design treatment.|
|[SplitButton](/uwp/api/microsoft.ui.xaml.controls.splitbutton)|Represents a button with two parts that can be invoked separately. One part behaves like a standard button and the other part invokes a flyout.|
|[SwipeControl](/uwp/api/microsoft.ui.xaml.controls.swipecontrol)|Represents a container that provides access to contextual commands through touch interactions.|
|[SymbolIconSource](/uwp/api/microsoft.ui.xaml.controls.symboliconsource)|Represents an icon source that uses a glyph from the Segoe MDL2 Assets font as its content.|
|[TextCommandBarFlyout](/uwp/api/microsoft.ui.xaml.controls.textcommandbarflyout)|Represents a specialized command bar flyout that contains commands for editing text.|
|[ToggleSplitButton](/uwp/api/microsoft.ui.xaml.controls.togglesplitbutton)|Represents a button with two parts that can be invoked separately. One part behaves like a toggle button and the other part invokes a flyout.|
|[TreeView](/uwp/api/microsoft.ui.xaml.controls.treeview)|Represents a hierarchical list with expanding and collapsing nodes that contain nested items.|

## Examples

> [!TIP]
> For more info, design guidance, and code examples, see [Design and code Windows apps](../../../design/index.md).
>
> The **WinUI 3 Gallery** and **WinUI 2 Gallery** apps include interactive examples of most WinUI 3 and WinUI 2 controls, features, and functionality.
>
> If installed already, open them by clicking the following links: [**WinUI 3 Gallery**](winui3gallery:/item/AnimatedIcon) or [**WinUI 2 Gallery**](winui2gallery:/item/AnimatedIcon).
>
> If they are not installed, you can download the [**WinUI 3 Gallery**](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) and the [**WinUI 2 Gallery**](https://www.microsoft.com/store/productId/9MSVH128X2ZT) from the Microsoft Store.
>
> You can also get the source code for both from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (use the *main* branch for WinUI 3 and the *winui2* branch for WinUI 2).

## Documentation

How-to articles for WinUI controls are included with the [Universal Windows Platform controls documentation](/windows/uwp/design/controls-and-patterns/).

API reference docs are located here: [WinUI APIs](/windows/winui/api/).