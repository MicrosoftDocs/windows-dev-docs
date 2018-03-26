---
author: serenaz
Description: The Universal Windows Platform (UWP) provides a consistent back navigation system for traversing the user's navigation history within an app and, depending on the device, from app to app.
title: Navigation history and backwards navigation (Windows apps)
ms.assetid: e9876b4c-242d-402d-a8ef-3487398ed9b3
isNew: true
label: History and backwards navigation
template: detail.hbs
op-migration-status: ready
ms.author: sezhen
ms.date: 11/22/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

#  Navigation history and backwards navigation for UWP apps

> **Important APIs**: [BackRequested event](https://docs.microsoft.com/uwp/api/Windows.UI.Core.SystemNavigationManager.BackRequested), [SystemNavigationManager class](https://docs.microsoft.com/uwp/api/Windows.UI.Core.SystemNavigationManager), [OnNavigatedTo](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.page.onnavigatedto#Windows_UI_Xaml_Controls_Page_OnNavigatedTo_Windows_UI_Xaml_Navigation_NavigationEventArgs_)

The Universal Windows Platform (UWP) provides a consistent back navigation system for traversing the user's navigation history within an app and, depending on the device, from app to app.

To implement backwards navigation in your app, place a [back button](#Back-button) at the top left corner of your app's UI. If your app uses the [NavigationView](../controls-and-patterns/navigationview.md) control, then you can use [NavigationView's built-in back button](../controls-and-patterns/navigationview.md#backwards-navigation). 

The user expects the back button to navigate to the previous location in the app's navigation history. Note that it's up to you to decide which navigation actions to add to the navigation history and how to respond to the back button press.

## Back button
To create a back button, use the [Button](../controls-and-patterns/buttons.md) control with the `NavigationBackButtonNormalStyle` style, and place the button at the top left hand corner of your app's UI.

![Back button in the top left of the app's UI](images/back-nav/BackEnabled.png)
```xaml
<Button Style="{StaticResource NavigationBackButtonNormalStyle}"/>
```

If your app has a top [CommandBar](../controls-and-patterns/app-bars.md), the Button control that is 44px in height will not align with 48px AppBarButtons very nicely. However, to avoid inconsistency, align the top of the Button control inside the 48px bounds.

![Back button on top command bar](images/back-nav/CommandBar.png)

```xaml
<Button VerticalAlignment="Top" HorizontalAlignment="Left" 
Style="{StaticResource NavigationBackButtonNormalStyle}"/>
```

In order to minimize UI elements moving around in your app, show a disabled back button when there is nothing in the backstack (see code example below).

![Back button states](images/back-nav/BackDisabled.png)

## Code example
The following code example demonstrates how to implement backwards navigation behavior with a back button. The code responds to the Button [**Click**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.Click) event and disables/enables the button visibility in [**OnNavigatedTo**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.page.onnavigatedto#Windows_UI_Xaml_Controls_Page_OnNavigatedTo_Windows_UI_Xaml_Navigation_NavigationEventArgs_), which is called when navigating to a new page. The code example also handles inputs from hardware and software system back keys by registering a listener for the [**BackRequested**](https://docs.microsoft.com/uwp/api/windows.ui.core.systemnavigationmanager.BackRequested) event.

```xaml
<Page x:Class="AppName.MainPage">
...
<Button x:Name="BackButton" Click="Back_Click" Style="{StaticResource NavigationBackButtonNormalStyle}"/>
...
<Page/>
```

Code-behind:
```csharp
public MainPage()
{
    KeyboardAccelerator GoBack = new KeyboardAccelerator();
    GoBack.Key = VirtualKey.GoBack;
    GoBack.Invoked += BackInvoked;
    KeyboardAccelerator AltLeft = new KeyboardAccelerator();
    AltLeft.Key = VirtualKey.Left;
    AltLeft.Invoked += BackInvoked;
    this.KeyboardAccelerators.Add(GoBack);
    this.KeyboardAccelerators.Add(AltLeft);
    // ALT routes here
    AltLeft.Modifiers = VirtualKeyModifiers.Menu;
}

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    BackButton.IsEnabled = thisFrame.CanGoBack;
}

private void Back_Click(object sender, RoutedEventArgs e)
{
    On_BackRequested();
}

// Handles system-level BackRequested events and page-level back button Click events
private bool On_BackRequested()
{
    if (this.Frame.CanGoBack)
    {
        this.Frame.GoBack();
        return true;
    }
    return false;
}

private void BackInvoked (KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
{
    On_BackRequested();
    args.Handled = true;
}
```
Here, we register a global listener for the [**BackRequested**](https://docs.microsoft.com/uwp/api/windows.ui.core.systemnavigationmanager.BackRequested) event in the `App.xaml` code-behind file. You can register for this event in each page if you want to exclude specific pages from back navigation, or you want to execute page-level code before displaying the page.

App.xaml code-behind:
```csharp
Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
Frame rootFrame = Window.Current.Content;
rootFrame.PointerPressed += On_PointerPressed;

private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
{
    e.Handled = On_BackRequested();
}

private void On_PointerPressed(object sender, PointerRoutedEventArgs e)
{
    bool isXButton1Pressed = e.GetCurrentPoint(sender as UIElement).Properties.PointerUpdateKind == PointerUpdateKind.XButton1Pressed;

    if (isXButton1Pressed)
    {
        e.Handled = On_BackRequested();
    }
}
```

## Optimizing for different device and form factors
This backwards navigation design guidance is applicable to all devices, but different device and form factors may benefit from optimization. This also depends on the hardware back button supported by different shells.

- **Phone/Tablet**: A hardware or software back button is always present on mobile and tablet, but we recommend drawing an in-app back button for clarity.
- **Desktop/Hub**: Draw the in-app back button on the top left corner of your app's UI.
- **Xbox/TV**: Do not draw a back button, for it will add unnecessary UI clutter. Instead, rely on the Gamepad B button to navigate backwards.

If your app will run on multiple devices, [create a custom visual trigger for Xbox](../devices/designing-for-tv.md#custom-visual-state-trigger-for-xbox) to toggle the visibility of button. The NavigationView control will automatically toggle the back button's visibility if your app is running on Xbox. 

We recommend supporting the following inputs for back navigation. (Note that some of these inputs are not supported by the system BackRequested and must be handled by separate events.)

<table>
<tr>
<th>Input</th><th>Event</th>
</tr>
<tr><td>Windows-Backspace key</td><td>BackRequested</td></tr>
<tr><td>Hardware back button</td><td>BackRequested</td></tr>
<tr><td>Shell tablet mode back button</td><td>BackRequested</td></tr>
<tr><td>VirtualKey.XButton1</td><td>PointerPressed</td></tr>
<tr><td>VirtualKey.GoBack</td><td>KeyboardAccelerator.BackInvoked</td></tr>
<tr><td>Alt+LeftArrow key</td><td>KeyboardAccelerator.BackInvoked</td></tr>
</table>

The code examples provided above demonstrate how to handle all of these inputs.

## System back behavior for backward compatibilities
Previously, UWP apps used [AppViewBackButtonVisibility](https://docs.microsoft.com/uwp/api/windows.ui.core.appviewbackbuttonvisibility) for backwards navigation. The API will continue to be supported for backward compatibility, but we no longer recommend relying on the title bar back button. Instead, your app should draw its own in-app back button.

If your app continues to use [AppViewBackButtonVisibility](https://docs.microsoft.com/uwp/api/windows.ui.core.appviewbackbuttonvisibility), then the back button will be rendered inside the title bar as usual.

![title bar back button](images/nav-back-pc.png)

## Guidelines for custom back navigation behavior

If you choose to provide your own back stack navigation, the experience should be consistent with other apps. We recommend that you follow the following patterns for navigation actions:

<div class="mx-responsive-img">
<table>
<thead>
<tr class="header">
<th align="left">Navigation action</th>
<th align="left">Add to navigation history?</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td style="vertical-align:top;"><strong>Page to page, different peer groups</strong></td>
<td style="vertical-align:top;"><strong>Yes</strong>
<p>In this illustration, the user navigates from level 1 of the app to level 2, crossing peer groups, so the navigation is added to the navigation history.</p>
<p><img src="images/back-nav/nav-pagetopage-diffpeers-imageonly1.png" alt="Navigation across peer groups" /></p>
<p>In the next illustration, the user navigates between two peer groups at the same level, again crossing peer groups, so the navigation is added to the navigation history.</p>
<p><img src="images/back-nav/nav-pagetopage-diffpeers-imageonly2.png" alt="Navigation across peer groups" /></p></td>
</tr>
<tr class="even">
<td style="vertical-align:top;"><strong>Page to page, same peer group, no on-screen navigation element</strong>
<p>The user navigates from one page to another with the same peer group. There is no navigation element that is always present (such as tabs/pivots or a docked navigation pane) that provides direct navigation to both pages.</p></td>
<td style="vertical-align:top;"><strong>Yes</strong>
<p>In the following illustration, the user navigates between two pages in the same peer group. The pages don't use tabs or a docked navigation pane, so the navigation is added to the navigation history.</p>
<p><img src="images/back-nav/nav-pagetopage-samepeer-noosnavelement.png" alt="Navigation within a peer group" /></p></td>
</tr>
<tr class="odd">
<td style="vertical-align:top;"><strong>Page to page, same peer group, with an on-screen navigation element</strong>
<p>The user navigates from one page to another in the same peer group. Both pages are shown in the same navigation element. For example, both pages use the same tabs/pivots element, or both pages appear in a docked navigation pane.</p></td>
<td style="vertical-align:top;"><strong>No</strong>
<p>When the user presses back, go back to the last page before the user navigated to the current peer group.</p>
<p><img src="images/back-nav/nav-pagetopage-samepeer-yesosnavelement.png" alt="Navigation across peer groups when a navigation element is present" /></p></td>
</tr>
<tr class="even">
<td style="vertical-align:top;"><strong>Show a transient UI</strong>
<p>The app displays a pop-up or child window, such as a dialog, splash screen, or on-screen keyboard, or the app enters a special mode, such as multiple selection mode.</p></td>
<td style="vertical-align:top;"><strong>No</strong>
<p>When the user presses the back button, dismiss the transient UI (hide the on-screen keyboard, cancel the dialog, etc) and return to the page that spawned the transient UI.</p>
<p><img src="images/back-nav/back-transui.png" alt="Showing a transient UI" /></p></td>
</tr>
<tr class="odd">
<td style="vertical-align:top;"><strong>Enumerate items</strong>
<p>The app displays content for an on-screen item, such as the details for the selected item in master/details list.</p></td>
<td style="vertical-align:top;"><strong>No</strong>
<p>Enumerating items is similar to navigating within a peer group. When the user presses back, navigate to the page that preceded the current page that has the item enumeration.</p>
<p><img src="images/back-nav/nav-enumerate.png" alt="Iterm enumeration" /></p></td>
</tr>
</tbody>
</table>
</div>

### Resuming

When the user switches to another app and returns to your app, we recommend returning to the last page in the navigation history

## Related articles
* [Navigation basics](navigation-basics.md)

 




