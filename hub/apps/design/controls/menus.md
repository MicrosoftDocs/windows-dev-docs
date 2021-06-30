---
description: Menus and context menus display a list of commands or options when the user requests them.
title: Menu flyout and menu bar
label: Menu flyout and menu bar
template: detail.hbs
ms.date: 06/24/2021
ms.topic: article
ms.custom: RS5, 19H1
keywords: windows 10, uwp
ms.assetid: 0327d8c1-8329-4be2-84e3-66e1e9a0aa60
pm-contact: yulikl
design-contact: kimsea
dev-contact: llongley
doc-status: Published
ms.localizationpriority: medium
---
# Menu flyout and menu bar

Menu flyouts are used in menu and context menu scenarios to display a list of commands or options when requested by the user. A menu flyout shows a single, inline, top-level menu that can have menu items and sub-menus. To show a set of multiple top-level menus in a horizontal row, use menu bar (which you typically position at the top of the app window).

![Example of a typical context menu](images/contextmenu_rs2_icons.png)

**Get the Windows UI Library**

:::row:::
   :::column:::
      ![WinUI logo](images/winui-logo-64x64.png)
   :::column-end:::
   :::column span="3":::
      The **MenuBar** control is included as part of the Windows UI Library, a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see the [Windows UI Library overview](/uwp/toolkits/winui/).
   :::column-end:::
   :::column:::

   :::column-end:::
:::row-end:::

> **Windows UI Library APIs:** [MenuBar class](/uwp/api/microsoft.ui.xaml.controls.menubar)
>
> **Platform APIs:** [MenuFlyout class](/uwp/api/windows.ui.xaml.controls.menuflyout), [MenuBar class](/uwp/api/windows.ui.xaml.controls.menubar), [ContextFlyout property](/uwp/api/windows.ui.xaml.uielement.contextflyout), [FlyoutBase.AttachedFlyout property](/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase#xaml-attached-properties)

## Is this the right control?

See [menus and context menus](menus-and-context-menus.md) for help identifying menu vs. context menu scenarios and guidance on when to use menu flyout vs. [command bar flyout](command-bar-flyout.md).

Menu flyouts can be used as menus and context menus to organize commands. To display arbitrary content, such as a notification or confirmation request, use a [dialog or a flyout](./dialogs-and-flyouts/index.md).

If a particular command will be used frequently and you have the space available, see [collection commanding](collection-commanding.md) for examples on placing a command directly in its own element so that users don't have to go through a menu to get to it. 

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/MenuFlyout">open the app and see the MenuFlyout in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Create a menu flyout

To create a menu flyout, you use the [MenuFlyout class](/uwp/api/windows.ui.xaml.controls.menuflyout). You define the contents of the menu by adding [MenuFlyoutItem](/uwp/api/windows.ui.xaml.controls.menuflyoutitem), [MenuFlyoutSubItem](/uwp/api/windows.ui.xaml.controls.menuflyoutsubitem), [ToggleMenuFlyoutItem](/uwp/api/windows.ui.xaml.controls.togglemenuflyoutitem), [RadioMenuFlyoutItem](/uwp/api/microsoft.ui.xaml.controls.radiomenuflyoutitem) and [MenuFlyoutSeparator](/uwp/api/windows.ui.xaml.controls.menuflyoutseparator) objects to the MenuFlyout.

These objects are for:

- [MenuFlyoutItem](/uwp/api/windows.ui.xaml.controls.menuflyoutitem)—Performing an immediate action.
- [MenuFlyoutSubItem](/uwp/api/windows.ui.xaml.controls.menuflyoutsubitem)—Containing a cascading list of menu items.
- [ToggleMenuFlyoutItem](/uwp/api/windows.ui.xaml.controls.togglemenuflyoutitem)—Switching an option on or off.
- [RadioMenuFlyoutItem](/uwp/api/microsoft.ui.xaml.controls.radiomenuflyoutitem)—Switching between mutually-exclusive menu items.
- [MenuFlyoutSeparator](/uwp/api/windows.ui.xaml.controls.menuflyoutseparator)—Visually separating menu items.

This example creates a [MenuFlyout](/uwp/api/windows.ui.xaml.controls.menuflyout) and uses the [ContextFlyout](/uwp/api/windows.ui.xaml.uielement.contextflyout) property, a property available to most controls, to show the MenuFlyout as a context menu.

````xaml
<Rectangle
  Height="100" Width="100">
  <Rectangle.ContextFlyout>
    <MenuFlyout>
      <MenuFlyoutItem Text="Change color" Click="ChangeColorItem_Click" />
    </MenuFlyout>
  </Rectangle.ContextFlyout>
  <Rectangle.Fill>
    <SolidColorBrush x:Name="rectangleFill" Color="Red" />
  </Rectangle.Fill>
</Rectangle>
````

````csharp
private void ChangeColorItem_Click(object sender, RoutedEventArgs e)
{
    // Change the color from red to blue or blue to red.
    if (rectangleFill.Color == Windows.UI.Colors.Red)
    {
        rectangleFill.Color = Windows.UI.Colors.Blue;
    }
    else
    {
        rectangleFill.Color = Windows.UI.Colors.Red;
    }
}
````

The next example is nearly identical, but instead of using the [ContextFlyout](/uwp/api/windows.ui.xaml.uielement.contextflyout) property to show the [MenuFlyout class](/uwp/api/windows.ui.xaml.controls.menuflyout) as a context menu, the example uses the [FlyoutBase.ShowAttachedFlyout](/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase.showattachedflyout) property to show it as a menu.

````xaml
<Rectangle
  Height="100" Width="100"
  Tapped="Rectangle_Tapped">
  <FlyoutBase.AttachedFlyout>
    <MenuFlyout>
      <MenuFlyoutItem Text="Change color" Click="ChangeColorItem_Click" />
    </MenuFlyout>
  </FlyoutBase.AttachedFlyout>
  <Rectangle.Fill>
    <SolidColorBrush x:Name="rectangleFill" Color="Red" />
  </Rectangle.Fill>
</Rectangle>
````

````csharp
private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
{
    FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
}

private void ChangeColorItem_Click(object sender, RoutedEventArgs e)
{
    // Change the color from red to blue or blue to red.
    if (rectangleFill.Color == Windows.UI.Colors.Red)
    {
        rectangleFill.Color = Windows.UI.Colors.Blue;
    }
    else
    {
        rectangleFill.Color = Windows.UI.Colors.Red;
    }
}
````

## Icons

Consider providing menu item icons for:

- The most commonly used items.
- Menu items whose icon is standard or well known.
- Menu items whose icon well illustrates what the command does.

Don't feel obligated to provide icons for commands that don't have a standard visualization. Cryptic icons aren't helpful, create visual clutter, and prevent users from focusing on the important menu items.

![Example context menu with icons](images/contextmenu_rs2_icons.png)

````xaml
<MenuFlyout>
  <MenuFlyoutItem Text="Share" >
    <MenuFlyoutItem.Icon>
      <FontIcon Glyph="&#xE72D;" />
    </MenuFlyoutItem.Icon>
  </MenuFlyoutItem>
  <MenuFlyoutItem Text="Copy" Icon="Copy" />
  <MenuFlyoutItem Text="Delete" Icon="Delete" />
  <MenuFlyoutSeparator />
  <MenuFlyoutItem Text="Rename" />
  <MenuFlyoutItem Text="Select" />
</MenuFlyout>
````

> [!TIP]
> The size of the icon in a MenuFlyoutItem is 16x16px. If you use SymbolIcon, FontIcon, or PathIcon, the icon automatically scales to the correct size with no loss of fidelity. If you use BitmapIcon, ensure that your asset is 16x16px.  


### Light dismiss

Light dismiss controls such as menus, context menus, and other flyouts, trap keyboard and gamepad focus inside the transient UI until dismissed. To provide a visual cue for this behavior, light dismiss controls on Xbox will draw an overlay that dims the visibility of out of scope UI. This behavior can be modified with the  [LightDismissOverlayMode](/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase.lightdismissoverlaymode) property. By default, transient UIs will draw the light dismiss overlay on Xbox (**Auto**) but not other device families. You can choose to force the overlay to be always **On** or always **Off**.

```xaml
<MenuFlyout LightDismissOverlayMode="Off" />
```

## Create a menu bar

> [!IMPORTANT]
> MenuBar requires Windows 10, version 1809 ([SDK 17763](https://developer.microsoft.com/windows/downloads/windows-10-sdk)) or later, or the [Windows UI Library](/uwp/toolkits/winui/).

You use the same elements to create menus in a menu bar as in a menu flyout. However, instead of grouping MenuFlyoutItem objects in a MenuFlyout, you group them in a MenuBarItem element. Each MenuBarItem is added to the MenuBar as a top level menu.

![Example of a menu bar](images/menu-bar-submenu.png)

> [!NOTE]
> This example shows only how to create the UI structure, but does not show implementation of any of the commands.

```xaml
<muxc:MenuBar>
    <muxc:MenuBarItem Title="File">
        <MenuFlyoutSubItem Text="New">
            <MenuFlyoutItem Text="Plain Text Document"/>
            <MenuFlyoutItem Text="Rich Text Document"/>
            <MenuFlyoutItem Text="Other Formats..."/>
        </MenuFlyoutSubItem>
        <MenuFlyoutItem Text="Open..."/>
        <MenuFlyoutItem Text="Save"/>
        <MenuFlyoutSeparator />
        <MenuFlyoutItem Text="Exit"/>
    </muxc:MenuBarItem>

    <muxc:MenuBarItem Title="Edit">
        <MenuFlyoutItem Text="Undo"/>
        <MenuFlyoutItem Text="Cut"/>
        <MenuFlyoutItem Text="Copy"/>
        <MenuFlyoutItem Text="Paste"/>
    </muxc:MenuBarItem>

    <muxc:MenuBarItem Title="View">
        <MenuFlyoutItem Text="Output"/>
        <MenuFlyoutSeparator/>
        <muxc:RadioMenuFlyoutItem Text="Landscape" GroupName="OrientationGroup"/>
        <muxc:RadioMenuFlyoutItem Text="Portrait" GroupName="OrientationGroup" IsChecked="True"/>
        <MenuFlyoutSeparator/>
        <muxc:RadioMenuFlyoutItem Text="Small icons" GroupName="SizeGroup"/>
        <muxc:RadioMenuFlyoutItem Text="Medium icons" IsChecked="True" GroupName="SizeGroup"/>
        <muxc:RadioMenuFlyoutItem Text="Large icons" GroupName="SizeGroup"/>
    </muxc:MenuBarItem>

    <muxc:MenuBarItem Title="Help">
        <MenuFlyoutItem Text="About"/>
    </muxc:MenuBarItem>
</muxc:MenuBar>
```

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.
- [XAML Context Menu sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlContextMenu)

## Related articles

- [Command design basics for Windows apps](../basics/commanding-basics.md)
- [Menus and context menus](menus-and-context-menus.md)
- [Contextual commanding for collections and lists](collection-commanding.md)
- [MenuFlyout class](/uwp/api/windows.ui.xaml.controls.menuflyout)
- [MenuBar class](/uwp/api/microsoft.ui.xaml.controls.menubar)
