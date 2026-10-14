---
description: Menus and context menus display a list of commands or options when the user requests them.
title: Menu flyout and menu bar
label: Menu flyout and menu bar
template: detail.hbs
ms.date: 10/13/2026
ms.topic: article
ms.custom: RS5, 19H1
ms.assetid: 0327d8c1-8329-4be2-84e3-66e1e9a0aa60
doc-status: Published
ms.localizationpriority: medium
---
# Menu flyout and menu bar

Menu flyouts are used in menu and context menu scenarios to display a list of commands or options when requested by the user. A menu flyout shows a single, inline, top-level menu that can have menu items and sub-menus. To show a set of multiple top-level menus in a horizontal row, use menu bar (which you typically position at the top of the app window).

![Example of a typical context menu](images/contextmenu_rs2_icons.png)

## Is this the right control?

See [menus and context menus](menus-and-context-menus.md) for help identifying menu vs. context menu scenarios and guidance on when to use menu flyout vs. [command bar flyout](command-bar-flyout.md).

Menu flyouts can be used as menus and context menus to organize commands. To display arbitrary content, such as a notification or confirmation request, use a [dialog or a flyout](dialogs-and-flyouts/index.md).

If a particular command will be used frequently and you have the space available, see [collection commanding](collection-commanding.md) for examples on placing a command directly in its own element so that users don't have to go through a menu to get to it.

## Create a menu flyout

> [!div class="checklist"]
>
> - **Important APIs:** [MenuFlyout class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyout), [ContextFlyout property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.contextflyout), [FlyoutBase.AttachedFlyout property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.flyoutbase.attachedflyout)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see MenuFlyout in action](winui3gallery://item/MenuFlyout)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

To create a menu flyout, you use the [MenuFlyout class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyout). You define the contents of the menu by adding [MenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutitem), [MenuFlyoutSubItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutsubitem), [SplitMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem), [ToggleMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.togglemenuflyoutitem), [RadioMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.radiomenuflyoutitem), and [MenuFlyoutSeparator](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutseparator) objects to the MenuFlyout.

These objects are for:

- [MenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutitem)—Performing an immediate action.
- [MenuFlyoutSubItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutsubitem)—Containing a cascading list of menu items.
- [SplitMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem)—Performing a default action, with related actions in a cascading list. For more info, see [Add a split menu item](#add-a-split-menu-item).
- [ToggleMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.togglemenuflyoutitem)—Switching an option on or off.
- [RadioMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.radiomenuflyoutitem)—Switching between mutually-exclusive menu items.
- [MenuFlyoutSeparator](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutseparator)—Visually separating menu items.

This example creates a [MenuFlyout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyout) and uses the [ContextFlyout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.contextflyout) property, a property available to most controls, to show the MenuFlyout as a context menu.

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
    if (rectangleFill.Color == Microsoft.UI.Colors.Red)
    {
        rectangleFill.Color = Microsoft.UI.Colors.Blue;
    }
    else
    {
        rectangleFill.Color = Microsoft.UI.Colors.Red;
    }
}
````

The next example is nearly identical, but instead of using the [ContextFlyout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.contextflyout) property to show the [MenuFlyout class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyout) as a context menu, the example uses the [FlyoutBase.ShowAttachedFlyout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.flyoutbase.showattachedflyout) property to show it as a menu.

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
    if (rectangleFill.Color == Microsoft.UI.Colors.Red)
    {
        rectangleFill.Color = Microsoft.UI.Colors.Blue;
    }
    else
    {
        rectangleFill.Color = Microsoft.UI.Colors.Red;
    }
}
````

## Add a split menu item

> [!div class="checklist"]
>
> - **Important APIs:** [SplitMenuFlyoutItem class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem), [Items property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem.items)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see SplitMenuFlyoutItem in action](winui3gallery://item/MenuFlyout)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

A [SplitMenuFlyoutItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem) provides a [split button](buttons.md#create-a-split-button) experience in a menu flyout. It has two parts:

- The primary part performs a default action, like a MenuFlyoutItem. To respond when the user selects it, handle the [Click](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutitem.click) event or set the [Command](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyoutitem.command) property.
- The secondary part opens a submenu of related options, like a MenuFlyoutSubItem.

Use a split menu item when a command has a common default action and a few variations, such as saving a file in different formats. It keeps your menu shorter than it would be if you listed each variation as a separate item.

> [!NOTE]
> SplitMenuFlyoutItem requires Windows App SDK 1.8.6 or later.

This example, from the WinUI 3 Gallery app, adds two split menu items to a MenuFlyout. The submenu items are in the [Items](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem.items) property. Because `Items` is the XAML content property of SplitMenuFlyoutItem, you can add the submenu items directly inside the SplitMenuFlyoutItem element.

```xaml
<StackPanel Orientation="Horizontal" Spacing="12">
    <Button Content="File Options">
        <Button.Flyout>
            <MenuFlyout>
                <SplitMenuFlyoutItem Text="Save" Click="SplitMenuFlyoutItem_Click">
                    <SplitMenuFlyoutItem.Icon>
                        <FontIcon Glyph="&#xE74E;" />
                    </SplitMenuFlyoutItem.Icon>
                    <MenuFlyoutItem Text="Save as .docx" Click="SplitMenuFlyoutItem_Click" />
                    <MenuFlyoutItem Text="Save as .pdf" Click="SplitMenuFlyoutItem_Click" />
                    <MenuFlyoutItem Text="Save as .txt" Click="SplitMenuFlyoutItem_Click" />
                </SplitMenuFlyoutItem>
                <SplitMenuFlyoutItem Text="Share" Icon="Share" Click="SplitMenuFlyoutItem_Click">
                    <MenuFlyoutItem Text="Share via email" Click="SplitMenuFlyoutItem_Click" />
                    <MenuFlyoutItem Text="Share via link" Click="SplitMenuFlyoutItem_Click" />
                </SplitMenuFlyoutItem>
            </MenuFlyout>
        </Button.Flyout>
    </Button>
    <TextBlock x:Name="SelectedCommandText" VerticalAlignment="Center" />
</StackPanel>
```

```csharp
private void SplitMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
{
    // SplitMenuFlyoutItem derives from MenuFlyoutItem, so one handler works for both.
    if (sender is MenuFlyoutItem selectedItem)
    {
        SelectedCommandText.Text = "Clicked: " + selectedItem.Text;
    }
}
```

The submenu can also contain MenuFlyoutSeparator objects and other SplitMenuFlyoutItem objects. If the `Items` collection is empty, the secondary part is disabled.

To customize the submenu, set [SubMenuPresenterStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem.submenupresenterstyle) to a style for the submenu's presenter, and [SubMenuItemStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem.submenuitemstyle) to a style for the items in the submenu.

Users interact with the two parts separately when they use the keyboard:

- When the primary part has focus, press Enter or Space to perform the default action, or press the Right arrow key to move focus to the secondary part.
- When the secondary part has focus, press Enter or the Right arrow key to open the submenu.
- When the submenu is open, press Esc or the Left arrow key to close it.

Screen readers also announce the two parts separately. The secondary part is announced as a button that provides more options for the menu item.

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

Light dismiss controls such as menus, context menus, and other flyouts, trap keyboard and gamepad focus inside the transient UI until dismissed. To provide a visual cue for this behavior, light dismiss controls on Xbox will draw an overlay that dims the visibility of out of scope UI. This behavior can be modified with the  [LightDismissOverlayMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.flyoutbase.lightdismissoverlaymode) property. By default, transient UIs will draw the light dismiss overlay on Xbox (**Auto**) but not other device families. You can choose to force the overlay to be always **On** or always **Off**.

```xaml
<MenuFlyout LightDismissOverlayMode="Off" />
```

## Create a menu bar

> [!div class="checklist"]
>
> - **Important APIs:** [MenuBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menubar). [MenuBarItem class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menubaritem)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see MenuBar in action](winui3gallery://item/MenuBar)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

You use the same elements to create menus in a menu bar as in a menu flyout. However, instead of grouping MenuFlyoutItem objects in a MenuFlyout, you group them in a MenuBarItem element. Each MenuBarItem is added to the MenuBar as a top level menu.

![Example of a menu bar](images/menu-bar-submenu.png)

> [!NOTE]
> This example shows only how to create the UI structure, but does not show implementation of any of the commands.

```xaml
<MenuBar>
    <MenuBarItem Title="File">
        <MenuFlyoutSubItem Text="New">
            <MenuFlyoutItem Text="Plain Text Document"/>
            <MenuFlyoutItem Text="Rich Text Document"/>
            <MenuFlyoutItem Text="Other Formats..."/>
        </MenuFlyoutSubItem>
        <MenuFlyoutItem Text="Open..."/>
        <MenuFlyoutItem Text="Save"/>
        <MenuFlyoutSeparator />
        <MenuFlyoutItem Text="Exit"/>
    </MenuBarItem>

    <MenuBarItem Title="Edit">
        <MenuFlyoutItem Text="Undo"/>
        <MenuFlyoutItem Text="Cut"/>
        <MenuFlyoutItem Text="Copy"/>
        <MenuFlyoutItem Text="Paste"/>
    </MenuBarItem>

    <MenuBarItem Title="View">
        <MenuFlyoutItem Text="Output"/>
        <MenuFlyoutSeparator/>
        <RadioMenuFlyoutItem Text="Landscape" GroupName="OrientationGroup"/>
        <RadioMenuFlyoutItem Text="Portrait" GroupName="OrientationGroup" IsChecked="True"/>
        <MenuFlyoutSeparator/>
        <RadioMenuFlyoutItem Text="Small icons" GroupName="SizeGroup"/>
        <RadioMenuFlyoutItem Text="Medium icons" IsChecked="True" GroupName="SizeGroup"/>
        <RadioMenuFlyoutItem Text="Large icons" GroupName="SizeGroup"/>
    </MenuBarItem>

    <MenuBarItem Title="Help">
        <MenuFlyoutItem Text="About"/>
    </MenuBarItem>
</MenuBar>
```

## Related articles

- [Command design basics for Windows apps](../../../design/basics/commanding-basics.md)
- [Menus and context menus](menus-and-context-menus.md)
- [Contextual commanding for collections and lists](collection-commanding.md)
- [MenuFlyout class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menuflyout)
- [SplitMenuFlyoutItem class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitmenuflyoutitem)
- [MenuBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.menubar)
