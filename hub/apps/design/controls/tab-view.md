---
description: TabView is a flexible way to organize multiple documents in dynamic tabs
title: Tab View
template: detail.hbs
ms.date: 01/15/2025
ms.topic: article
keywords: windows 10, uwp
doc-status: Published
ms.localizationpriority: medium
---
# Tab view

The TabView control is a way to display a set of tabs and their respective content. TabView controls are useful for displaying several pages (or documents) of content while letting a user rearrange, close, or open new tabs.

:::image type="content" source="images/tabview/tab-introduction.png" alt-text="Example of a TabView":::

## Is this the right control?

In general, tabbed UIs come in one of two distinct styles that differ in function and appearance:

- **Static tabs** are the sort of tabs often found in settings windows. They contain a set number of pages in a fixed order that usually contain predefined content.
- **Document tabs** are the sort of tabs found in a browser, such as Microsoft Edge. Users can create, remove, and rearrange tabs; move tabs between windows; and change the content of tabs.

By default, [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) is configured to provide document tabs. We recommend TabView when users will be able to:

- Dynamically open, close, or rearrange tabs.
- Open documents or web pages directly into tabs.
- Drag and drop tabs between windows.

The TabView API does allow configuring the control for static tabs. However, to follow Windows design guidance, and if there are more than a few static navigation items, consider using a [NavigationView](./navigationview.md) control.

## Anatomy

Tabbed UI is created with a TabView control and one or more TabViewItem controls. The TabView hosts instances of TabViewItem, which represents a single tab and it's content.

### TabView parts

This image shows the parts of the [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) control. The _tab strip_ has a header and footer, but unlike a document, the tab strip's header and footer are on the far left and far right of the strip, respectively.

:::image type="content" source="images/tabview/tab-view-anatomy.png" alt-text="This image shows the parts of the tab view control. The tab strip contains four tabs and has a header and footer, which are on the far left and far right of the strip, respectively.":::

### TabViewItem parts

This image shows the parts of the [TabViewItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem) control. Although the content is displayed inside of the TabView control, the content is actually a part of the TabViewItem.

:::image type="content" source="images/tabview/tab-control-anatomy.png" alt-text="This image shows the parts of the tab view item control. An individual tab is selected, which has an icon and label, with a content area below the tab.":::

## Recommendations

### Tab selection

Most users have experience using document tabs simply by using a web browser. When they use document tabs in your app, their experience informs their expectations for how your tabs should behave.

No matter how the user interacts with a set of document tabs, there should always be an active tab. If the user closes the selected tab or breaks the selected tab out into another window, another tab should become the active tab. [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) attempts to do this automatically selecting the next tab. If you have a good reason that your app should allow a TabView with an unselected tab, the TabView's content area will simply be blank.

### Keyboard navigation

[TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) supports many common keyboard navigation scenarios by default. This section explains the built-in functionality, and provides recommendations on additional functionality that might be helpful for some apps.

#### Tab and cursor key behavior

When focus moves into the _TabStrip_ area, the selected [TabViewItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem) gains focus. The user can then use the Left and Right arrow keys to move focus (not selection) to other tabs in the tab strip. Arrow focus is trapped inside the tab strip and the add tab (+) button, if one is present. To move focus out of the tab strip area, the user can press the <kbd>Tab</kbd> key, which will move focus to the next focusable element.

_**Move focus via Tab**_

:::image type="content" source="images/tabview/tab-keyboard-behavior-1.png" alt-text="Move focus via tab":::

_**Arrow keys do not cycle focus**_

:::image type="content" source="images/tabview/tab-keyboard-behavior-3.png" alt-text="Arrow keys do not cycle focus":::

#### Selecting a tab

When a TabViewItem has focus, press <kbd>Space</kbd> or <kbd>Enter</kbd> to select that TabViewItem.

_**Use arrow keys to move focus, then press <kbd>Space</kbd> to select tab.**_

:::image type="content" source="images/tabview/tab-keyboard-behavior-2.png" alt-text="Space to select tab":::

#### Shortcuts for selecting adjacent tabs

Press <kbd>Ctrl+Tab</kbd> to select the next [TabViewItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem). Press <kbd>Ctrl+Shift+Tab</kbd> to select the previous TabViewItem. For these purposes, the tab list is "looped," so selecting the next tab while the last tab is selected will cause the first tab to become selected.

#### Closing a tab

Press <kbd>Ctrl + F4</kbd> to raise  the [TabCloseRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabcloserequested) event. Handle the event and close the tab if appropriate.

> [!TIP]
> For more information, see [Keyboard guidance for developers](#keyboard-guidance-for-developers) later in this article.

## Create a tab view

> [!div class="checklist"]
>
> - **Important APIs**: [TabView class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview), [TabViewItem class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the TabView in action](winui3gallery://item/TabView)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

The examples in this section show a variety of ways to configure a [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) control.

### Tab view items

Each tab in a [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) is represented by a [TabViewItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem) control, which includes both the tab that's shown in the tab strip and the content shown below the tab strip.

#### Configure a tab

For each TabViewItem, you can set a header and an icon, and specify whether the user can close the tab.

- The [Header](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem.header) property is typically set to a string value that provides a descriptive label for the tab. However, the `Header` property can be any object. You can also use the [HeaderTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem.headertemplate) property to specify a [DataTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.datatemplate) that defines how bound header data should be displayed.
- Set the [IconSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem.iconsource) property to specify an icon for the tab.
- By default, the tab shows a _close button_ (X). You can set the [IsClosable](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem.isclosable) property to `false` to hide the close button and ensure that a user can't close the tab. (If you close tabs in your app code outside of a _close requested_ event, you should first check that `IsClosable` is `true`.)

For the TabView, you can configure several options that apply to all tabs.

- By default, the close button is always shown for closable tabs. You can set the [CloseButtonOverlayMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.closebuttonoverlaymode) property to `OnPointerOver` to change this behavior. In this case, the selected tab always shows the close button if it is closable, but unselected tabs show the close button only when the tab is closable and the user has their pointer over it.
- You can set the [TabWidthMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabwidthmode) property to change how tabs are sized. (The `Width` property is ignored on `TabViewItem`.) These are the options in the [TabViewWidthMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewwidthmode) enumeration:
  - `Equal` - Each tab has the same width. This is the default.
  - `SizeToContent` - Each tab adjusts its width to the content within the tab.
  - `Compact` - Unselected tabs collapse to show only their icon. The selected tab adjusts to display the content within the tab.

#### Content

The elements displayed in the selected tab are added to the [Content](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol.content) property of the [TabViewItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem). TabViewItem is a [ContentControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol), so you can add any type of object as content. You can also apply a [DataTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.datatemplate) to the [ContentTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol.contenttemplate) property. See the [ContentControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol) class for more information.

The examples in this article show a simple case of adding text directly to the `Content` element in XAML. However, real UI is typically more complex. A common way to add complex UI as the content of a tab is to encapsulate it in a [UserControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.usercontrol) or a [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page), and add that as the content of the TabViewItem. This example assumes your app has a XAML UserControl called `PictureSettingsControl`.

```xaml
<TabViewItem>
    <TabViewItem.Content>
        <local:PictureSettingsControl/>
    </TabViewItem.Content>
</TabViewItem>
```

### Static tabs

This example shows a simple [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) with two _static tabs_. Both tab items are added in XAML as content of the TabView.

To make a TabView static, use these settings:

- Set the [IsAddTabButtonVisible](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.isaddtabbuttonvisible) property to `false` to hide the _add tab_ button and prevent the [AddTabButtonClick](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.addtabbuttonclick) event from being raised.
- Set the [CanReorderTabs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.canreordertabs) property to `false` to prevent the user from dragging tabs into a different order.
- On each [TabViewItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem), set the [IsClosable](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewitem.isclosable) property to false to hide the _tab close_ button prevent the user from raising the [TabCloseRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabcloserequested) event.

```xaml
<TabView VerticalAlignment="Stretch"
         IsAddTabButtonVisible="False"
         CanReorderTabs="False">
    <TabViewItem Header="Picture" IsClosable="False">
        <TabViewItem.IconSource>
            <SymbolIconSource Symbol="Pictures"/>
        </TabViewItem.IconSource>
        <TabViewItem.Content>
            <StackPanel Padding="12">
                <TextBlock Text="Picture settings" 
                    Style="{ThemeResource TitleTextBlockStyle}"/>
            </StackPanel>
        </TabViewItem.Content>
    </TabViewItem>
    <TabViewItem Header="Sound" IsClosable="False">
        <TabViewItem.IconSource>
            <SymbolIconSource Symbol="Audio"/>
        </TabViewItem.IconSource>
        <TabViewItem.Content>
            <StackPanel Padding="12">
                <TextBlock Text="Sound settings" 
                    Style="{ThemeResource TitleTextBlockStyle}"/>
            </StackPanel>
        </TabViewItem.Content>
    </TabViewItem>
</TabView>
```

### Document tabs

By default, the TabView is configured for _document tabs_. The user can add new tabs, rearrange tabs, and close tabs. In this configuration, you need to handle the [AddTabButtonClick](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.addtabbuttonclick) and [TabCloseRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabcloserequested) events to enable the functionality.

When tabs are added to a TabView, there might eventually be too many tabs to display in your tab strip. In this case, scroll bumpers will appear that let the user scroll the tab strip left and right to access hidden tabs.

This example creates a simple [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) along with event handlers to support opening and closing tabs. The `TabView_AddTabButtonClick` event handler shows how to add a TabViewItem in code.

```xaml
<TabView VerticalAlignment="Stretch"
         AddTabButtonClick="TabView_AddTabButtonClick"
         TabCloseRequested="TabView_TabCloseRequested">
    <TabViewItem Header="Home" IsClosable="False">
        <TabViewItem.IconSource>
            <SymbolIconSource Symbol="Home" />
        </TabViewItem.IconSource>
        <TabViewItem.Content>
            <StackPanel Padding="12">
                <TextBlock Text="TabView content" 
                           Style="{ThemeResource TitleTextBlockStyle}"/>
            </StackPanel>
        </TabViewItem.Content>
    </TabViewItem>
</TabView>
```

```csharp
// Add a new tab to the TabView.
private void TabView_AddTabButtonClick(TabView sender, object args)
{
    var newTab = new TabViewItem();
    newTab.Header = $"New Document {sender.TabItems.Count}";
    newTab.IconSource = new SymbolIconSource() { Symbol = Symbol.Document };
    newTab.Content = new TextBlock() { Text = $"Content for new tab {sender.TabItems.Count}.",
                                       Padding = new Thickness(12) };
    sender.TabItems.Add(newTab);
    sender.SelectedItem = newTab;
}

// Remove the requested tab from the TabView.
private void TabView_TabCloseRequested(TabView sender, 
                                       TabViewTabCloseRequestedEventArgs args)
{
    sender.TabItems.Remove(args.Tab);
}
```

#### Close the window when the last tab is closed

If all tabs in your app are closeable and your app window should close when the last tab is closed, you should also close the window in the [TabCloseRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabcloserequested) event handler.

First, in the `App.xaml.cs` file, add a `public static` property that will let you access the `Window` instance from the `Page` that hosts the TabView. (See [User interface migration](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/winui3.md#change-windowsuixamlwindowcurrent-to-appwindow).)

```csharp App.xaml.cs
public partial class App : Application
{
    // ... code removed.

    // Add this.
    public static Window Window { get { return m_window; } }
    // Update this to make it static.
    private static Window m_window;
}
```

Then, modify the [TabCloseRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabcloserequested) event handler to call [Window.Close](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.close) if all tabs have been removed from the TabView.

```csharp
// Remove the requested tab from the TabView.
// If all tabs have been removed, close the Window.
private void TabView_TabCloseRequested(TabView sender, 
                                       TabViewTabCloseRequestedEventArgs args)
{
    sender.TabItems.Remove(args.Tab);

    if (sender.TabItems.Count == 0)
    {
        App.Window.Close();
    }
}
```

> [!NOTE]
> This example works for an app with a single window (`MainWindow`). If your app has multiple windows, or you have enabled tab tear-out, you need to track the windows and then find the correct one to close. See the next section for an example of this.

## Tab tear-out

_Tab tear-out_ describes what happens when a user drags a tab out of the TabView's tab strip and moves it to another TabView control, typically in a new window.

Starting in Windows App SDK 1.6, TabView has a [CanTearOutTabs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.cantearouttabs) property that you can set to provide an enhanced experience for dragging tabs out to a new window. When a user drags a tab out of the tab strip with this option is enabled, a new window is immediately created during the drag, allowing the user to drag it to the edge of the screen to maximize or [snap the window](https://www.microsoft.com/windows/tips/snap) in one smooth motion. This implementation also doesn't use drag-and-drop APIs, so it isn't impacted by any limitations in those APIs.

When you set the CanTearOutTabs property to `true`, it causes the _tab tear-out_ events to be raised instead of _drag-and-drop_ events. To implement tab tear-out, you must handle these events:

- [TabTearOutWindowRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabtearoutwindowrequested)
  
  This event occurs when a tab is first dragged out of the tab strip. Handle it to create a new Window and TabView that the tab will be moved to.
- [TabTearOutRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabtearoutrequested)
  
  This event occurs after a new Window has been provided. Handle it to move the torn-out tab from the originating TabView to a TabView in the new window.
- [ExternalTornOutTabsDropping](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.externaltornouttabsdropping)
  
  This event occurs when a torn-out tab is dragged over an existing TabView. Handle it in the TabView that is receiving the torn-out tab to indicate whether or not the tab should be accepted.
- [ExternalTornOutTabsDropped](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.externaltornouttabsdropped)
  
  This event occurs when a torn-out tab is dragged over an existing TabView and the `ExternalTornOutTabsDropping` event has indicated that the drop is allowed. Handle it in the TabView that is receiving the torn-out tab to remove the tab from the originating TabView and insert it into the receiving TabView at the specified index.

These events are not raised when tab tear-out is enabled: [TabDragStarting](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabdragstarting), [TabStripDragOver](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabstripdragover), [TabStripDrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabstripdrop), [TabDragCompleted](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabdragcompleted), [TabDroppedOutside](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabdroppedoutside).

> [!CAUTION]
> Tab tear-out is supported in processes running elevated as Administrator.

The following examples show how to implement the event handlers to support tab tear-out.

### Set up the TabView

This XAML sets the [CanTearOutTabs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.cantearouttabs) property to `true` and sets up the tab tear-out event handlers.

```xaml MainWindow.xaml
<TabView x:Name="tabView"
     CanTearOutTabs="True"
     TabTearOutWindowRequested="TabView_TabTearOutWindowRequested"
     TabTearOutRequested="TabView_TabTearOutRequested"
     ExternalTornOutTabsDropping="TabView_ExternalTornOutTabsDropping"
     ExternalTornOutTabsDropped="TabView_ExternalTornOutTabsDropped">
    <!-- TabView content -->
</TabView>
```

### Create and track a new window

Tab tear-out requires that you create and manage new windows in your app.

> [!TIP]
> The WinUI Gallery app includes a `WindowHelper` class that makes it easier to manage windows in your app. You can copy it from GitHub in the WinUI Gallery repo: [WindowHelper.cs](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/Helper/WindowHelper.cs). We recommend this helper class to implement tab tear-out. See the [TabViewWindowingSamplePage](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/TabViewPages/TabViewWindowingSamplePage.xaml.cs) on GitHub to see how it's used.
>
> In this article, helper methods are copied from `WindowHelper.cs`, but are modified and shown inline for readability.

Here, a list for tracking all active windows is created in `App.xaml.cs`. The `OnLaunched` method is updated to track the window after it's created. (This is not needed if you use the `WindowHelper` class.)

```csharp App.xaml.cs
static public List<Window> ActiveWindows = new List<Window>();

protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    m_window = new MainWindow();
    // Track this window.
    ActiveWindows.Add(m_window);
    m_window.Activate();
}
```

When tab tear-out begins, a new window is requested. Here, the variable `tabTearOutWindow` provides access to the new window after it's created. The `CreateWindow` and `TrackWindow` helper methods create a new window and add it to the active window tracking list.

After you create the new window, you need to create a new Page and set it as the content of the window. The new [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) must contain a TabView control that you will move the torn-out tab into in the `TabTearOutRequested` event handler.

> [!TIP]
> In this example, we create a new `MainPage` class, since it contains only an empty TabView (no tabs are added directly in XAML). If `MainPage` includes other UI elements that should not appear in the torn-out window, you can create a separate page that includes only elements you require (including at least a TabView), and create an instance of that page.

Finally, assign the [AppWindow.Id](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.id) of the new window to the `args.`[NewWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewtabtearoutwindowrequestedeventargs.newwindowid) property. This will be used in the [TabViewTabTearOutRequestedEventArgs.NewWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewtabtearoutrequestedeventargs.newwindowid) property so you can access the window from that event handler.

```csharp MainWindow.xaml.cs
private Window? tabTearOutWindow = null;

private void TabView_TabTearOutWindowRequested(TabView sender, TabViewTabTearOutWindowRequestedEventArgs args)
{
    tabTearOutWindow = CreateWindow();
    tabTearOutWindow.Content = new MainPage();
    // Optional window setup, such as setting the icon or
    // extending content into the title bar happens here.
    args.NewWindowId = tabTearOutWindow.AppWindow.Id;
}

private Window CreateWindow()
{
    Window newWindow = new Window
    {
        SystemBackdrop = new MicaBackdrop()
    };
    newWindow.Title = "Torn Out Window";
    TrackWindow(newWindow);
    return newWindow;
}

private void TrackWindow(Window window)
{
    window.Closed += (sender, args) => {
        App.ActiveWindows.Remove(window);
    };
    App.ActiveWindows.Add(window);
}
```

#### Close a window when the last tab is closed

As mentioned earlier, you, might want to close the window when the last tab in a TabView is closed. If your app has multiple windows, you need to find the correct window to close in your list of tracked windows. This example shows how to do that.

```csharp
// Remove the requested tab from the TabView.
// If all tabs have been removed, close the Window.
private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
{
    sender.TabItems.Remove(args.Tab);

    if (sender.TabItems.Count == 0)
    {
        GetWindowForElement(this)?.Close();
    }
}

public Window? GetWindowForElement(UIElement element)
{
    if (element.XamlRoot != null)
    {
        foreach (Window window in App.ActiveWindows)
        {
            if (element.XamlRoot == window.Content.XamlRoot)
            {
                return window;
            }
        }
    }
    return null;
}
```

### Move the tab to the new window

After the new window has been provided, you need to remove the torn-out tab from the `sender` TabView and add it to the TabView in the new window. In this example, the `public AddTabToTabs` helper method let's you access the TabView in the new `MainPage` instance from the original page instance in order to add the torn-out tab to it.

```csharp
private void TabView_TabTearOutRequested(TabView sender, TabViewTabTearOutRequestedEventArgs args)
{
    if (tabTearOutWindow?.Content is MainPage newPage
        && args.Tabs.FirstOrDefault() is TabViewItem tab)
    {
        sender.TabItems.Remove(tab);
        newPage.AddTabToTabs(tab);
    }
}

// This method provides access to the TabView from
// another page instance so you can add the torn-out tab.
public void AddTabToTabs(TabViewItem tab)
{
    tabView.TabItems.Add(tab);
}
```

### Drag a torn-out tab onto another TabView

When a tab has been torn-out and placed into a new window, as shown in the previous steps, one of two things can happen:

- The user can drop the tab and it remains in the new window. The tear-out process ends here and no more events are raised.
- The user can continue to drag the torn-out tab back onto an existing TabView control. In this case, the process continues and several more events are raised to let you remove the tab from the original TabView and insert the external tab into an existing TabView.

When the tab is dragged over the existing TabView, the [ExternalTornOutTabsDropping](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.externaltornouttabsdropping) event it raised. In the event handler, you can determine whether inserting the tab into this TabView is allowed. In most cases, you only need to set the `args.`[AllowDrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewexternaltornouttabsdroppingeventargs.allowdrop) property to `true`. However, if you need to perform any checks before setting that property, you can do that here. If `AllowDrop` is set to `false`, the tab drag action continues and the [ExternalTornOutTabsDropped](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.externaltornouttabsdropped) event is not raised.

```csharp
private void TabView_ExternalTornOutTabsDropping(TabView sender, 
                        TabViewExternalTornOutTabsDroppingEventArgs args)
{
    args.AllowDrop = true;
}
```

If `AllowDrop` is set to `true` in the `ExternalTornOutTabsDropping` event handler, the `ExternalTornOutTabsDropped` event is immediately raised.

> [!NOTE]
> The _`Dropped`_ in the event name does not directly correspond to the idea of a _drop_ action in the drag-and-drop APIs. Here, the user does not need to release the tab to perform a _drop_ action. The event is raised while the tab is held over the tab strip, and the code is executed to _drop_ the tab into the TabView.

The [ExternalTornOutTabsDropped](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.externaltornouttabsdropped) event handler follows the same pattern as the [TabTearOutRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabtearoutrequested) event, but inverted; you need to remove the tab from the originating TabView and insert it into the `sender` TabView.

The `sender` TabView is the control that the tab is being inserted into, so we use the `GetParentTabView` helper method to find the originating tab. It starts with the torn-out TabViewItem and uses [VisualTreeHelper](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.visualtreehelper) to walk up the visual tree and find the TabView the item belongs to. After the TabView is found, the TabViewItem is removed from it's [TabItems](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabitems) collection and inserted into the `sender` TabView's `TabItems` collection at the index specified by `args.`[DropIndex](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabviewexternaltornouttabsdroppedeventargs.dropindex).

```csharp
private void TabView_ExternalTornOutTabsDropped(TabView sender, 
                             TabViewExternalTornOutTabsDroppedEventArgs args)
{
    if (args.Tabs.FirstOrDefault() is TabViewItem tab)
    {
        GetParentTabView(tab)?.TabItems.Remove(tab);
        sender.TabItems.Insert(args.DropIndex, tab);
    }
}

// Starting with the TabViewItem, walk up the
// visual tree until you get to the TabView.
private TabView? GetParentTabView(TabViewItem tab)
{
    DependencyObject current = tab;
    while (current != null)
    {
        if (current is TabView tabView)
        {
            return tabView;
        }
        current = VisualTreeHelper.GetParent(current);
    }
    return null;
}
```

> [!TIP]
> If you're using the [Windows Community Toolkit](/dotnet/communitytoolkit/windows/), you can use the `FindAscendant` helper method in the toolkit's [DependencyObjectExtensions](/dotnet/communitytoolkit/windows/extensions/dependencyobjectextensions) instead of `GetParentTabView`.

## Display TabView tabs in a window's title bar

Instead of having tabs occupy their own row below a Window's title bar, you can merge the two into the same area. This saves vertical space for your content, and gives your app a modern feel.

Because a user can drag a window by its title bar to reposition the Window, it is important that the title bar is not completely filled with tabs. Therefore, when displaying tabs in a title bar, you must specify a portion of the title bar to be reserved as a draggable area. If you do not specify a draggable region, the entire title bar will be draggable, which prevents your tabs from receiving input events. If your TabView will display in a window's title bar, you should always include a [TabStripFooter](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview.tabstripfooter) in your [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) and mark it as a draggable region.

For more information, see [Title bar customization](../../develop/title-bar.md)

:::image type="content" source="images/tabview/tab-extend-to-title.png" alt-text="Tabs in title bar":::

```xaml
<TabView VerticalAlignment="Stretch">
    <TabViewItem Header="Home" IsClosable="False">
        <TabViewItem.IconSource>
            <SymbolIconSource Symbol="Home" />
        </TabViewItem.IconSource>
    </TabViewItem>

    <TabView.TabStripFooter>
        <Grid x:Name="CustomDragRegion" Background="Transparent" />
    </TabView.TabStripFooter>
</TabView>
```

```csharp
private void MainPage_Loaded(object sender, RoutedEventArgs e)
{
    App.Window.ExtendsContentIntoTitleBar = true;
    App.Window.SetTitleBar(CustomDragRegion);
    CustomDragRegion.MinWidth = 188;
}
```

> [!NOTE]
> How you get a reference to the window may vary depending on how you track windows in your app. For more information, see [Close the window when the last tab is closed](#close-the-window-when-the-last-tab-is-closed) and [Create and track a new window](#create-and-track-a-new-window) in this article.

## Keyboard guidance for developers

> [!TIP]
> For more information about built-in keyboard support, see [Keyboard navigation](#keyboard-navigation) earlier in this article.

Some applications may require more advanced keyboard control. Consider implementing the following shortcuts if they are appropriate for your app.

> [!WARNING]
> If you are adding a [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) to an existing app, you may have already created keyboard shortcuts that map to the key combinations of the recommended TabView keyboard shortcuts. In this case, you will have to consider whether to keep your existing shortcuts or offer an intuitive tab experience for the user.

- <kbd>Ctrl + T</kbd> should open a new tab. Generally this tab is populated with a predefined document, or is created empty with a simple way to choose its content. If the user must choose content for a new tab, consider giving input focus to the content selection control.
- <kbd>Ctrl + W</kbd> should close the selected tab. Remember, TabView will select the next tab automatically.
- <kbd>Ctrl + Shift + T</kbd> should open recently closed tabs (or more accurately, open new tabs with the same content as recently closed tabs). Start with the most recently closed tab, and move backwards in time for each subsequent time the shortcut is invoked. Note that this will require maintaining a list of recently closed tabs.
- <kbd>Ctrl + 1</kbd> should select the first tab in the tab list. Likewise, Ctrl + 2 should select the second tab, Ctrl + 3 should select the third, and so on through Ctrl + 8.
- <kbd>Ctrl + 9</kbd> should select the last tab in the tab list, regardless of how many tabs are in the list.
- If tabs offer more than just the close command (such as duplicating or pinning a tab), use a context menu to show all available actions that can be performed on a tab.

### Implement browser-style keyboarding behavior

This example implements a number of the above recommendations on a [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview). Specifically, This example implements <kbd>Ctrl + T</kbd>, <kbd>Ctrl + W</kbd>, <kbd>Ctrl + 1-8</kbd>, and <kbd>Ctrl + 9</kbd>.

```xaml
<TabView>
    <!-- ... some tabs ... -->
    <TabView.KeyboardAccelerators>
        <KeyboardAccelerator Key="T" Modifiers="Control"
                             Invoked="NewTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="W" Modifiers="Control"
                             Invoked="CloseSelectedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number1" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number2" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number3" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number4" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number5" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number6" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number7" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number8" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
        <KeyboardAccelerator Key="Number9" Modifiers="Control"
                             Invoked="NavigateToNumberedTabKeyboardAccelerator_Invoked" />
    </TabView.KeyboardAccelerators>
</TabView>

```

```csharp
private void NewTabKeyboardAccelerator_Invoked(KeyboardAccelerator sender,
                                      KeyboardAcceleratorInvokedEventArgs args)
{
    // Create new tab.
    TabView senderTabView = (TabView)args.Element;
    if (senderTabView is not null)
    {
        // (Click handler defined in previous example.)
        TabView_AddTabButtonClick(senderTabView, new EventArgs());
    }
    args.Handled = true;
}

private void CloseSelectedTabKeyboardAccelerator_Invoked(KeyboardAccelerator sender,
                                                KeyboardAcceleratorInvokedEventArgs args)
{
    TabView tabView = (TabView)args.Element;
    TabViewItem tab = (TabViewItem)tabView.SelectedItem;
    if (tab is not null)
    {
        CloseSelectedTab(tabView, tab);
    }
    args.Handled = true;
}

private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
{
    CloseSelectedTab(sender, args.Tab);
}

private void CloseSelectedTab(TabView tabView, TabViewItem tab)
{
    // Only remove the selected tab if it can be closed.
    if (tab.IsClosable == true)
    {
        tabView.TabItems.Remove(tab);
    }
}


private void NavigateToNumberedTabKeyboardAccelerator_Invoked(KeyboardAccelerator sender,
                                                     KeyboardAcceleratorInvokedEventArgs args)
{
    TabView tabView = (TabView)args.Element;
    int tabToSelect = 0;

    switch (sender.Key)
    {
        case Windows.System.VirtualKey.Number1:
            tabToSelect = 0;
            break;
        case Windows.System.VirtualKey.Number2:
            tabToSelect = 1;
            break;
        case Windows.System.VirtualKey.Number3:
            tabToSelect = 2;
            break;
        case Windows.System.VirtualKey.Number4:
            tabToSelect = 3;
            break;
        case Windows.System.VirtualKey.Number5:
            tabToSelect = 4;
            break;
        case Windows.System.VirtualKey.Number6:
            tabToSelect = 5;
            break;
        case Windows.System.VirtualKey.Number7:
            tabToSelect = 6;
            break;
        case Windows.System.VirtualKey.Number8:
            tabToSelect = 7;
            break;
        case Windows.System.VirtualKey.Number9:
            // Select the last tab
            tabToSelect = tabView.TabItems.Count - 1;
            break;
    }

    // Only select the tab if it is in the list.
    if (tabToSelect < tabView.TabItems.Count)
    {
        tabView.SelectedIndex = tabToSelect;
    }
}
```

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

The TabView control for UWP apps is included as part of WinUI 2. For more info, including installation instructions, see [WinUI 2](../../winui/winui2/index.md). APIs for this control exist in the [Microsoft.UI.Xaml.Controls](/windows/winui/api/microsoft.ui.xaml.controls) namespace.

Tab tear-out APIs are not included in the WinUI 2 version of TabView.

> [!div class="checklist"]
>
> - **WinUI 2 Apis:** [TabView class](/uwp/api/microsoft.ui.xaml.controls.tabview), [TabViewItem class](/uwp/api/microsoft.ui.xaml.controls.tabviewitem)
> - [Open the WinUI 2 Gallery app and see the TabView in action](winui2gallery:/item/TabView). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles, templates, and features for all controls. WinUI 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md).

[!INCLUDE [muxc-alias-note](../../../includes/muxc-alias-note.md)]

```xaml
xmlns:muxc="using:Microsoft.UI.Xaml.Controls"

<muxc:TabView />
```

## Related articles

- [List/Details](./list-details.md)
- [NavigationView](./navigationview.md)
