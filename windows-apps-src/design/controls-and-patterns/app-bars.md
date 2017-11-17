---
author: serenaz
Description: Command bars give users easy access to your app's most common tasks.
title: Command bar
label: App bars/command bars
template: detail.hbs
op-migration-status: ready
ms.author: sezhen
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 868b4145-319b-4a97-82bd-c98d966144db
pm-contact: yulikl
design-contact: ksulliv
dev-contact: niallm
doc-status: Published
localizationpriority: medium
---
# Command bar

Command bars (also called "app bars") provide users with easy access to your app's most common tasks, and can be used to show commands or options that are specific to the user's context, such as a photo selection or drawing mode. They can also be used for navigation among app pages or between app sections. Command bars can be used with any navigation pattern.

> **Important APIs**: [CommandBar class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.commandbar.aspx), [AppBarButton class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.appbarbutton.aspx), [AppBarToggleButton class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.appbartogglebutton.aspx), [AppBarSeparator class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.appbarseparator.aspx)

![Example of a command bar with icons](images/controls_appbar_icons.png)

## Is this the right control?

The CommandBar control is a general-purpose, flexible, light-weight control that can display both complex content, such as images or text blocks, as well as simple commands such as [AppBarButton](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.appbarbutton.aspx), [AppBarToggleButton](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.appbartogglebutton.aspx), and [AppBarSeparator](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.appbarseparator.aspx) controls.

XAML provides both the AppBar control and the CommandBar control. You should use the AppBar only when you are upgrading a Universal Windows 8 app that uses the AppBar, and need to minimize changes. For new apps in Windows 10, we recommend using the CommandBar control instead. This document assumes you are using the CommandBar control.

## Examples

<div style="overflow: hidden; margin: 0 -8px;">
    <div style="float: left; margin: 0 8px 16px; min-width: calc(25% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <div style="height: 133px; width: 100%">
            <img src="images/xaml-controls-gallery.png" alt="XAML controls gallery"></img>
        </div>
    </div>
    <div style="float: left; margin: -22px 8px 16px; min-width: calc(75% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/CommandBar">open the app and see the CommandBar in action</a>.</p>
        <ul>
        <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
        <li><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics">Get the source code (GitHub)</a></li>
        </ul>
    </div>
</div>

An expanded command bar in the Microsoft Photos app.

![Command bar in Microsoft Photos app](images/control-examples/command-bar-photos.png)

A command bar in the Outlook Calendar on Windows Phone.

![Command bar in Outlook Calendar app](images/control-examples/command-bar-calendar-phone.png)

## Anatomy

By default, the command bar shows a row of icon buttons and an optional "see more" button, which is represented by an ellipsis \[•••\]. Here's the command bar created by the example code shown later. It's shown in its closed compact state.

![A closed command bar](images/command-bar-compact.png)

The command bar can also be shown in a closed minimal state that looks like this. See the [Open and closed states](#open-and-closed-states) section for more info.

![A closed command bar](images/command-bar-minimal.png)

Here's the same command bar in its open state. The labels identify the main parts of the control.

![A closed command bar](images/commandbar_anatomy_open.png)

The command bar is divided into 4 main areas:
- The "see more" \[•••\] button is shown on the right of the bar. Pressing the "see more" \[•••\] button has 2 effects: it reveals the labels on the primary command buttons, and it opens the overflow menu if any secondary commands are present. In the newest SDK, the button will not be visible when no secondary commands and no hidden labels are present. [OverflowButtonVisibility](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.commandbar.overflowbuttonvisibility.aspx) property allows apps to change this default auto-hide behavior.
- The content area is aligned to the left side of the bar. It is shown if the [Content](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.contentcontrol.content.aspx) property is populated.
- The primary command area is aligned to the right side of the bar, next to the "see more" \[•••\] button. It is shown if the [PrimaryCommands](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.commandbar.primarycommands.aspx) property is populated.  
- The overflow menu is shown only when the command bar is open and the [SecondaryCommands](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.commandbar.secondarycommands.aspx) property is populated. The new dynamic overflow behavior will automatically move primary commands into the SecondaryCommands area when space is limited.

The layout is reversed when the [FlowDirection](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.frameworkelement.flowdirection.aspx) is **RightToLeft**.

## Create a command bar
This example creates the command bar shown previously.

```xaml
<CommandBar>
    <AppBarToggleButton Icon="Shuffle" Label="Shuffle" Click="AppBarButton_Click" />
    <AppBarToggleButton Icon="RepeatAll" Label="Repeat" Click="AppBarButton_Click"/>
    <AppBarSeparator/>
    <AppBarButton Icon="Back" Label="Back" Click="AppBarButton_Click"/>
    <AppBarButton Icon="Stop" Label="Stop" Click="AppBarButton_Click"/>
    <AppBarButton Icon="Play" Label="Play" Click="AppBarButton_Click"/>
    <AppBarButton Icon="Forward" Label="Forward" Click="AppBarButton_Click"/>

    <CommandBar.SecondaryCommands>
        <AppBarButton Icon="Like" Label="Like" Click="AppBarButton_Click"/>
        <AppBarButton Icon="Dislike" Label="Dislike" Click="AppBarButton_Click"/>
    </CommandBar.SecondaryCommands>

    <CommandBar.Content>
        <TextBlock Text="Now playing..." Margin="12,14"/>
    </CommandBar.Content>
</CommandBar>
```

## Commands and content
The CommandBar control has 3 properties you can use to add commands and content: [PrimaryCommands](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.commandbar.primarycommands.aspx), [SecondaryCommands](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.commandbar.secondarycommands.aspx), and [Content](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.contentcontrol.content.aspx).


### Primary actions and overflow

By default, items you add to the command bar are added to the **PrimaryCommands** collection. These commands are shown to the left of the "see more" \[•••\] button, in what we call the action space. Place the most important commands, the ones that you want to remain visible in the bar, in the action space. On the smallest screens (320 epx width), a maximum of 4 items will fit in the command bar's action space.

You can add commands to the **SecondaryCommands** collection, and these items are shown in the overflow area. Place less important commands within the overflow area.

The default overflow area is styled to be distinct from the bar. You can adjust the styling by setting the [CommandBarOverflowPresenterStyle](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.commandbar.commandbaroverflowpresenterstyle.aspx) property to a [Style](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.style.aspx) that targets the [CommandBarOverflowPresenter](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.commandbaroverflowpresenter.aspx).

You can programmatically move commands between the PrimaryCommands and SecondaryCommands as needed.

<!-- 
<div class="microsoft-internal-note">
Commands can also automatically move in or out of the overflow as the command bar width changes, for example when users resize their app window. Dynamic overflow is on by default but apps can turn off this behavior by changing the value of `IsDynamicOverflowEnabled` property.
</div>
-->

### App bar buttons

Both the PrimaryCommands and SecondaryCommands can be populated only with [AppBarButton](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbarbutton.aspx), [AppBarToggleButton](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbartogglebutton.aspx), and [AppBarSeparator](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbarseparator.aspx) command elements. These controls are optimized for use in a command bar, and their appearance changes depending on whether the control is used in the action space or overflow area.

The app bar button controls are characterized by an icon and associated label. They have two sizes; normal and compact. By default, the text label is shown. When the [IsCompact](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbarbutton.iscompact.aspx) property is set to **true**, the text label is hidden. When used in a CommandBar control, the command bar overwrites the button's IsCompact property automatically as the command bar is opened and closed.

To position app bar button labels to the right of their icons, apps can use CommandBar's new [DefaultLabelPosition](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.commandbar.defaultlabelposition.aspx) property.

```xaml
<CommandBar DefaultLabelPosition="Right">
    <AppBarToggleButton Icon="Shuffle" Label="Shuffle"/>
    <AppBarToggleButton Icon="RepeatAll" Label="Repeat"/>
</CommandBar>
```

Here is what the code snippet above looks like when drawn by an app.

![Command bar with labels on the right](images/app-bar-labels-on-right.png)

Individual app bar buttons cannot move their label position, this must be done on the command bar as a whole. App bar buttons can specify that their labels never show by setting the new [LabelPosition](https://msdn.microsoft.com/library/windows/apps/mt710920.aspx) property to **Collapsed**. We recommend limiting the use of this setting to universally recognizable iconography such as '+'.

When you place an app bar button in the overflow menu (SecondaryCommands), it's shown as text only. The **LabelPosition** of app bar buttons in the overflow will be ignored. Here's the same app bar toggle button shown in the action space as a primary command (top), and in the overflow area as a secondary command (bottom).

![App bar button as primary and secondary command](images/app-bar-toggle-button-two-modes.png)

- *If there is a command that would appear consistently across pages, it's best to keep that command in a consistent location.*
- *We recommended placing Accept, Yes, and OK commands to the left of Reject, No, and Cancel. Consistency gives users the confidence to move around the system and helps them transfer their knowledge of app navigation from app to app.*

### Button labels

We recommend keeping app bar button labels short, preferably a single word. Longer labels positioned bellow an app bar button's icon will wrap to multiple lines thus increasing the overall height of the opened command bar. You can include a soft-hyphen character (0x00AD) in the text for a label to hint at the character boundary where a word break should occur. In XAML, you express this using an escape sequence, like this:

```xaml
<AppBarButton Icon="Back" Label="Areally&#x00AD;longlabel"/>
```

When the label wraps at the hinted location, it looks like this.

![App bar button with wrapping label](images/app-bar-button-label-wrap.png)

### Other content

You can add any XAML elements to the content area by setting the **Content** property. If you want to add more than one element, you need to place them in a panel container and make the panel the single child of the Content property.

When there are both primary commands and content, the primary commands take precedence and may cause the content to be clipped.

<!--
<div class="microsoft-internal-note">
Content will not clip when dynamic overflow is enabled because the primary commands would move into the overflow menu freeing up space for content.
</div>
-->

When the [ClosedDisplayMode](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.closeddisplaymode.aspx) is **Compact**, the content can be clipped if it is larger than the compact size of the command bar. You should handle the [Opening](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.opening.aspx) and [Closed](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.closed.aspx) events to show or hide parts of the UI in the content area so that they aren't clipped. See the [Open and closed states](#open-and-closed-states) section for more info.

## Open and closed states

The command bar can be open or closed. A user can switch between these states by pressing the "see more" \[•••\] button. You can switch between them programmatically by setting the [IsOpen](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.isopen.aspx) property. When open, the primary command buttons are shown with text labels and the overflow menu is open if secondary commands are present, as shown previously.

You can use the [Opening](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.opening.aspx), [Opened](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.opened.aspx), [Closing](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.closing.aspx), and [Closed](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.closed.aspx) events to respond to the command bar being opened or closed.  
- The Opening and Closing events occur before the transition animation begins.
- The Opened and Closed events occur after the transition completes.

In this example, the Opening and Closing events are used to change the opacity of the command bar. When the command bar is closed, it's semi-transparent so the app background shows through. When the command bar is opened, the command bar is made opaque so the user can focus on the commands.

```xaml
<CommandBar Opening="CommandBar_Opening"
            Closing="CommandBar_Closing">
    <AppBarButton Icon="Accept" Label="Accept"/>
    <AppBarButton Icon="Edit" Label="Edit"/>
    <AppBarButton Icon="Save" Label="Save"/>
    <AppBarButton Icon="Cancel" Label="Cancel"/>
</CommandBar>
```

```csharp
private void CommandBar_Opening(object sender, object e)
{
    CommandBar cb = sender as CommandBar;
    if (cb != null) cb.Background.Opacity = 1.0;
}

private void CommandBar_Closing(object sender, object e)
{
    CommandBar cb = sender as CommandBar;
    if (cb != null) cb.Background.Opacity = 0.5;
}

```

### ClosedDisplayMode

You can control how the command bar is shown in its closed state by setting the [ClosedDisplayMode](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.closeddisplaymode.aspx) property. There are 3 closed display modes to choose from:
- **Compact**: The default mode. Shows content, primary command icons without labels, and the "see more" \[•••\] button.
- **Minimal**: Shows only a thin bar that acts as the "see more" \[•••\] button. The user can press anywhere on the bar to open it.
- **Hidden**: The command bar is not shown when it's closed. This can be useful for showing contextual commands with an inline command bar. In this case, you must open the command bar programmatically by setting the **IsOpen** property or changing the ClosedDisplayMode to **Minimal** or **Compact**.

Here, a command bar is used to hold simple formatting commands for a [RichEditBox](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.richeditbox.aspx). When the edit box doesn't have focus, the formatting commands can be distracting, so they're hidden. When the edit box is being used, the command bar's ClosedDisplayMode is changed to Compact so the formatting commands are visible.

```xaml
<StackPanel Width="300"
            GotFocus="EditStackPanel_GotFocus"
            LostFocus="EditStackPanel_LostFocus">
    <CommandBar x:Name="FormattingCommandBar" ClosedDisplayMode="Hidden">
        <AppBarButton Icon="Bold" Label="Bold" ToolTipService.ToolTip="Bold"/>
        <AppBarButton Icon="Italic" Label="Italic" ToolTipService.ToolTip="Italic"/>
        <AppBarButton Icon="Underline" Label="Underline" ToolTipService.ToolTip="Underline"/>
    </CommandBar>
    <RichEditBox Height="200"/>
</StackPanel>
```

```csharp
private void EditStackPanel_GotFocus(object sender, RoutedEventArgs e)
{
    FormattingCommandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
}

private void EditStackPanel_LostFocus(object sender, RoutedEventArgs e)
{
    FormattingCommandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Hidden;
}
```

>**Note**&nbsp;&nbsp;The implementation of the editing commands is beyond the scope of this example. For more info, see the [RichEditBox](rich-edit-box.md) article.

Although the Minimal and Hidden modes are useful in some situations, keep in mind that hiding all actions could confuse users.

Changing the ClosedDisplayMode to provide more or less of a hint to the user affects the layout of surrounding elements. In contrast, when the CommandBar transitions between closed and open, it does not affect the layout of other elements.

### IsSticky

After opening the command bar, if the user interacts with the app anywhere outside of the control then by default the overflow menu is dismissed and the labels are hidden. Closing it in this way is called *light dismiss*. You can control how the bar is dismissed by setting the [IsSticky](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.appbar.issticky.aspx) property. When the bar is sticky (`IsSticky="true"`), it's not closed by a light dismiss gesture. The bar remains open until the user presses the "see more" \[•••\] button or selects an item from the overflow menu. We recommend avoiding sticky command bars because they don't conform to users' expectations around light dismiss.

## Do's and don'ts

### Placement

Command bars can be placed at the top of the app window, at the bottom of the app window, and inline.

![Example 1 of app bar placement](images/AppbarGuidelines_Placement1.png)

-   For small handheld devices, we recommend positioning command bars at the bottom of the screen for easy reachability.
-   For devices with larger screens, if you're placing just one command bar, we recommend placing it near the top of the window.
Use the [DiagonalSizeInInches](https://msdn.microsoft.com/library/windows/apps/windows.graphics.display.displayinformation.diagonalsizeininches.aspx) API to determine physical screen size.

Command bars can be placed in the following screen regions on single-view screens (left example) and on multi-view screens (right example). Inline command bars can be placed anywhere in the action space.

![Example 2 of app bar placement](images/AppbarGuidelines_Placement2.png)

>**Touch devices**: If the command bar must remain visible to a user when the touch keyboard, or Soft Input Panel (SIP), appears then you can assign the command bar to the [BottomAppBar](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.page.bottomappbar.aspx) property of a Page and it will move to remain visible when the SIP is present. Otherwise, you should place the command bar inline and positioned relative to your app content.

### Actions

Prioritize the actions that go in the command bar based on their visibility.

-   Place the most important commands, the ones that you want to remain visible in the bar, in the first few slots of the action space. On the smallest screens (320 epx width), between 2-4 items will fit in the command bar's action space, depending on other on-screen UI.
-   Place less-important commands later in the bar's action space or within the first few slots of the overflow area. These commands will be visible when the bar has enough screen real estate, but will fall into the overflow area's drop-down menu when there isn't enough room.
-   Place the least-important commands within the overflow area. These commands will always appear in the drop-down menu.

If there is a command that would appear consistently across pages, it's best to keep that command in a consistent location. We recommended placing Accept, Yes, and OK commands to the left of Reject, No, and Cancel. Consistency gives users the confidence to move around the system and helps them transfer their knowledge of app navigation from app to app.

Although you can place all actions within the overflow area so that only the "see more" \[•••\] button is visible on the command bar, keep in mind that hiding all actions could confuse users.

### Command bar flyouts

Consider logical groupings for the commands, such as placing Reply, Reply All, and Forward in a Respond menu. While typically an app bar button activates a single command, an app bar button can be used to show a [MenuFlyout](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.menuflyout.aspx) or [Flyout](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.flyout.aspx) with custom content.

![Example of flyouts on a command bar](images/AppbarGuidelines_Flyouts.png)

### Overflow menu

![Example of a command bar with "More" area and icons](images/appbar_rs2_overflow_icons.png)

-   The overflow menu is represented by the "see more" \[•••\] button, the visible entry point for the menu. It's on the far-right of the toolbar, adjacent to primary actions.
-   The overflow area is allocated for actions that are less frequently used.
-   Actions can come and go between the primary action space and the overflow menu at breakpoints. You can also designate actions to always remain in the primary action space regardless of screen or app window size.
-   Infrequently used actions can remain in the overflow menu even when the app bar is expanded on larger screens.
- AppBarButtons in the overflow menu will automatically show their icons.

> The size of the icons in the overflow menu is 16x16px, which is smaller than the icons in the primary command area (which are 20x20px). If you use SymbolIcon, FontIcon, or PathIcon, the icon will automatically scale to the correct size with no loss of fidelity when the command enters the secondary command area.

## Adaptability

-   The same number of actions in the app bar should be visible in both portrait and landscape orientation, which reduces the user's cognitive load. The number of actions available should be determined by the device's width in portrait orientation.
-   On small screens that are likely to be used one-handed, app bars should be positioned near the bottom of the screen.
-   On larger screens, placing app bars closer to the top of the window makes them more noticeable and discoverable.
-   By targeting breakpoints, you can move actions in and out of the menu as the window size changes.
-   By targeting screen diagonal, you can modify app bar position based on device screen size.
-   Consider moving labels to the right of app bar button icons to improve legibility. Labels on the bottom require users to open the command bar to reveal labels, while labels on the right are visible even when command bar is closed. This optimization works well on larger windows.

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics) - See all the XAML controls in an interactive format.
- [XAML Commanding sample](http://go.microsoft.com/fwlink/p/?LinkId=620019)

## Related articles

* [Command design basics for UWP apps](../basics/commanding-basics.md)
* [CommandBar class](https://msdn.microsoft.com/library/windows/apps/dn279427)
