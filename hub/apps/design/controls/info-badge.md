---
description: An InfoBadge is a small circle that represents a notification or alert within an app.
title: InfoBadge
template: detail.hbs
ms.date: 09/10/2021
ms.topic: article
keywords: windows 10, winui, uwp
pm-contact: gabilka
design-contact: shurd
dev-contact: stpete
ms.custom: 20H2
ms.localizationpriority: medium
---

# InfoBadge

Badging is a non-intrusive and intuitive way to display notifications or bring focus to an area within an app - whether that be for notifications, indicating new content, or showing an alert. An [InfoBadge](/uwp/api/microsoft.ui.xaml.controls.infobadge) is a small piece of UI that can be added into an app and customized to display a number, icon, or a simple dot.

InfoBadge is built into [NavigationView](navigationview.md), but can also be placed as a standalone element in the XAML tree, allowing you to place InfoBadge into any control or piece of UI of your choosing. When you use an InfoBadge somewhere other than NavigationView, you are responsible for programmatically determining when to show and dismiss the InfoBadge, and where to place the InfoBadge.

:::image type="content" source="images/infobadge/infobadge-example-1.png" alt-text="Example of an InfoBadge in NavigationView":::

**Get the Windows UI Library**

:::row:::
   :::column:::
      ![WinUI logo](images/winui-logo-64x64.png)
   :::column-end:::
   :::column span="3":::
      The **InfoBadge** control requires the Windows UI Library, a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](/uwp/toolkits/winui/).
   :::column-end:::
   :::column:::
   :::column-end:::
:::row-end:::

> **Windows UI Library APIs:** [InfoBadge class](/uwp/api/microsoft.ui.xaml.controls.infobadge)

> [!TIP]
> Throughout this document, we use the **muxc** alias in XAML to represent the Windows UI Library APIs that we have included in our project. We have added this to our [Page](/uwp/api/windows.ui.xaml.controls.page) element: `xmlns:muxc="using:Microsoft.UI.Xaml.Controls"`
>
>In the code-behind, we also use the **muxc** alias in C# to represent the Windows UI Library APIs that we have included in our project. We have added this **using** statement at the top of the file: `using muxc = Microsoft.UI.Xaml.Controls;`

## Is this the right control?

An [InfoBadge](/uwp/api/microsoft.ui.xaml.controls.infobadge) should be used when you want to bring the user's focus to a certain area of your app in an unintrusive way. When an InfoBadge appears, it is meant to bring focus to an area and then let the user get back into their flow, giving them the choice of whether or not to look into the details of why the InfoBadge appeared. InfoBadges should only represent messages that are dismissible and non-permanent – an InfoBadge should have specific rules as to when it can appear, disappear, and change.

Examples of appropriate InfoBadge usage:

- To indicate new messages have arrived.
- To indicate new articles are available to read.
- To indicate that there are new options available on a page.
- To indicate that there might be an issue with an item on a certain page that does not block the app from functioning.

### When should a different control be used?

An [InfoBadge](/uwp/api/microsoft.ui.xaml.controls.infobadge) should not be used to display critical errors or convey highly important messages that need immediate action. InfoBadges should not be used in cases where they need to be interacted with immediately to continue using the app.

Examples of inappropriate InfoBadge usage:

- To indicate an urgent matter on a page within the app that needs to be addressed before continuing to use the app. For this scenario, use a [ContentDialog](dialogs-and-flyouts/dialogs.md).
- Appearing in an app with no way for the user to dismiss the InfoBadge. For a persistent alert like this, use the [InfoBar control](infobar.md).
- Using the InfoBadge as a permanent way of bringing the user's focus to an area, without a way for the user to dismiss the InfoBadge.
- Using an InfoBadge as a regular icon or image in your app. Instead, use an appropriate [IconSource](/uwp/api/windows.ui.xaml.controls.iconsource) or [Image](images-imagebrushes.md).

<table>
<th align="left">WinUI 2 Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="WinUI Gallery"></img></td>
<td>
    <p>If you have the <strong>WinUI 2 Gallery</strong> app installed, click here to <a href="winui2gallery:/item/InfoBadge">open the app and see the InfoBadge in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the WinUI 2 Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/WinUI-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Types of InfoBadges

There are three styles of [InfoBadge](/uwp/api/microsoft.ui.xaml.controls.infobadge) that you can choose from - _dot_, _icon_, and _numeric_, as shown in order below.

:::image type="content" source="images/infobadge/infobadge-types.png" alt-text="Dot, icon, and numeric InfoBadges":::

### Dot InfoBadge

The dot InfoBadge is a simple ellipse with a diameter of 4px. It has no border, and is not meant to hold text or anything else inside of it.

You should use the dot InfoBadge for general scenarios in which you want to guide the user's focus towards the InfoBadge – for example, to indicate new content or updates are available.

### Icon InfoBadge

The icon InfoBadge is an ellipse with a diameter of 16px that holds an icon inside of it. InfoBadge has an [IconSource](/windows/winui/api/microsoft.ui.xaml.controls.infobadge.iconsource) property that provides flexibility for the types of supported icons.

You should use the icon InfoBadge to send a quick message along with getting the user's attention – for example, to alert the user that something non-blocking has gone wrong, an extra important update is available, or that something specific in the app is currently enabled (such as a countdown timer going).

If you'd like to use a [BitmapIconSource](/windows/winui/api/microsoft.ui.xaml.controls.bitmapiconsource) for the `IconSource` of your InfoBadge, you are responsible for ensuring that the bitmap fits inside of the InfoBadge (either by changing the size of the icon, or changing the size of the InfoBadge).

### Numeric InfoBadge

The numeric InfoBadge is the same shape and size as the icon InfoBadge, but it  holds a number inside of it, determined by the [Value](/windows/winui/api/microsoft.ui.xaml.controls.infobadge.value) property. Numbers must be whole integers and must be greater than or equal to zero. The width of the InfoBadge will automatically expand as the number being displayed grows to multiple digits, with a smooth animation.

You should use the numeric InfoBadge to show that there are a specific number of items that need attention – for example, new emails or messages.

### Create an InfoBadge

The kind of InfoBadge you create is determined by which properties you set.

#### Dot

To create a dot InfoBadge, use a default InfoBadge control with no properties set.

```xaml
<muxc:InfoBadge />
```

:::image type="content" source="images/infobadge/dot-infobadge.png" alt-text="Dot InfoBadge":::

#### Icon

To create an icon InfoBadge, set the [IconSource](/windows/winui/api/microsoft.ui.xaml.controls.infobadge.iconsource) property.

```xaml
<muxc:InfoBadge x:Name="SyncStatusInfoBadge">
    <muxc:InfoBadge.IconSource>
        <muxc:SymbolIconSource Symbol="Sync"/>
    </muxc:InfoBadge.IconSource>
</muxc:InfoBadge>
```

:::image type="content" source="images/infobadge/icon-infobadge.png" alt-text="Icon InfoBadge":::

#### Numeric

To create a numeric InfoBadge, set the [Value](/windows/winui/api/microsoft.ui.xaml.controls.infobadge.value) property.

```xaml
<muxc:InfoBadge x:Name="EmailInfoBadge" Value="{x:Bind numUnreadMail}"/>
```

:::image type="content" source="images/infobadge/numeric-infobadge.png" alt-text="Numeric InfoBadge":::

In most scenarios, you'll bind the `Value` property of the InfoBadge to a changing integer value in your app's backend so you can easily increment/decrement and show/hide the InfoBadge based on that specific value.

> [!NOTE]
> If both the `Icon` and `Value` properties are set, the `Value` property takes precedence and the InfoBadge appears as a numeric InfoBadge.

## Preset InfoBadge styles

To help support the most common scenarios in which InfoBadges are used, WinUI provides built-in preset InfoBadge styles. While you can customize your InfoBadge to use any color/icon/number combination that you want, these built-in presets are a quick option to make sure that your InfoBadge is compliant with accessibility guidelines and is proportional in terms of icon and number sizing.

The following style presets are available for InfoBadge:

- `AttentionDotInfoBadgeStyle`
- `AttentionIconInfoBadgeStyle`
- `AttentionValueInfoBadgeStyle`

:::image type="content" source="images/infobadge/attention-badges.png" alt-text="Attention InfoBadge styles":::

- `InformationalDotInfoBadgeStyle`
- `InformationalIconInfoBadgeStyle`
- `InformationalValueInfoBadgeStyle`

:::image type="content" source="images/infobadge/informational-badges.png" alt-text="Informational InfoBadge styles":::

- `SuccessDotInfoBadgeStyle`
- `SuccessIconInfoBadgeStyle`
- `SuccessValueInfoBadgeStyle`

:::image type="content" source="images/infobadge/success-badges.png" alt-text="Success InfoBadge styles":::

- `CautionDotInfoBadgeStyle`
- `CautionIconInfoBadgeStyle`
- `CautionValueInfoBadgeStyle`

:::image type="content" source="images/infobadge/caution-badges.png" alt-text="Caution InfoBadge styles":::

- `CriticalDotInfoBadgeStyle`
- `CriticalIconInfoBadgeStyle`
- `CriticalValueInfoBadgeStyle`

:::image type="content" source="images/infobadge/critical-badges.png" alt-text="Critical InfoBadge styles":::

If a style is set on an InfoBadge and a conflicting property is also set, the property will overwrite the conflicting part of the style, but non-conflicting style elements will stay applied.

For example, if you apply the `CriticalIconInfoBadgeStyle` to an InfoBadge, but also set `InfoBadge.Value = "1"`, you would end up with an InfoBadge that has the "Critical" background color but displays the number 1 inside of it, rather than displaying the preset icon.

This example creates an InfoBadge that takes on the color and icon of the _Attention Icon_ preset style.

```xaml
<muxc:InfoBadge Style="{ThemeResource AttentionIconInfoBadgeStyle}"/>
```

:::image type="content" source="images/infobadge/attention-icon-style.png" alt-text="Blue InfoBadge with an asterisk symbol":::

## Using an InfoBadge in NavigationView

If you're using a NavigationView in your app, we recommend that you use an InfoBadge in the NavigationView to show app-wide notifications and alerts. To place the InfoBadge on a NavigationViewItem, assign the InfoBadge object to the `NavigationViewItem.InfoBadge` property.

In Left-Expanded mode, the InfoBadge appears right-aligned to the edge of the NavigationViewItem.

:::image type="content" source="images/infobadge/navview-expanded.png" alt-text="Left-expanded NavigationView with an InfoBadge":::

In Left-Compact mode, the InfoBadge appears overlayed on the top right corner of the icon.

:::image type="content" source="images/infobadge/navview-compact.png" alt-text="Left-compact NavigationView with an InfoBadge":::

In Top mode, the InfoBadge is aligned to the upper right hand corner of the overall item.

:::image type="content" source="images/infobadge/navview-top.png" alt-text="Top mode NavigationView with an InfoBadge":::

We recommend that you not use different types of InfoBadges in one NavigationView, such as attaching a numeric InfoBadge to one NavigationViewItem and a dot InfoBadge to another NavigationViewItem in the same NavigationView.

### Example: Incrementing a numeric InfoBadge in a NavigationView

This example simulates how an email app could use an InfoBadge in a NavigationView to display the number of new emails in the inbox, and increment the number shown in the InfoBadge when a new message is received.

```xaml
<muxc:NavigationView SelectionChanged="NavigationView_SelectionChanged">
    <muxc:NavigationView.MenuItems>
        <muxc:NavigationViewItem Content="Home" Icon="Home"/>
        <muxc:NavigationViewItem Content="Account" Icon="Contact"/>
        <muxc:NavigationViewItem x:Name="InboxPage" Content="Inbox" Icon="Mail">
            <muxc:NavigationViewItem.InfoBadge>
                <muxc:InfoBadge x:Name="bg1"
                                Value="{x:Bind mailBox.NewMailCount, Mode=OneWay}"
                                Visibility="{x:Bind mailBox.HasNewMail, Mode=OneWay}"/>
            </muxc:NavigationViewItem.InfoBadge>
        </muxc:NavigationViewItem>
    </muxc:NavigationView.MenuItems>
    <Frame x:Name="contentFrame" />
</muxc:NavigationView>
```

```csharp
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    MailBox mailBox = new MailBox();

    private void NavigationView_SelectionChanged(muxc.NavigationView sender, 
                               muxc.NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem == InboxPage)
        {
            mailBox.ResetNewMailCount();
        }
        else
        {
            mailBox.CheckMail();
        }
    }
}

public class MailBox : DependencyObject
{
    System.Timers.Timer timer = new System.Timers.Timer();

    // Dependency Properties for binding.
    public int NewMailCount
        {
            get { return (int)GetValue(NewMailCountProperty); }
            set { SetValue(NewMailCountProperty, value); }
        }
    public static readonly DependencyProperty NewMailCountProperty =
        DependencyProperty.Register("NewMailCount", typeof(int), typeof(MailBox), new PropertyMetadata(0));

    public bool HasNewMail
    {
        get { return (bool)GetValue(HasNewMailProperty); }
        set { SetValue(HasNewMailProperty, value); }
    }
    public static readonly DependencyProperty HasNewMailProperty =
        DependencyProperty.Register("HasNewMail", typeof(bool), typeof(MailBox), new PropertyMetadata(false));

    public MailBox()
    {
        timer.Interval = 1500;
        timer.Elapsed += async (s, e) =>
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                NewMailCount++;
                if (HasNewMail == false) { HasNewMail = true; }
            });
        timer.Start();
    }

    public void ResetNewMailCount()
    {
        NewMailCount = 0;
        HasNewMail = false;
        timer.Stop();
    }

    public void CheckMail()
    {
        timer.Start();
    }
}
```

:::image type="content" source="images/infobadge/email-count-infobadge.png" alt-text="NavigationView with an InfoBadge on the Inbox item that's displaying the number 17":::

### Hierarchy in NavigationView

If you have a [hierarchical NavigationView](/windows/uwp/design/controls-and-patterns/navigationview#hierarchical-navigation), with NavigationViewItems nested in other NavigationViewItems, parent items will follow the same design/placement patterns as described above.

The parent NavigationViewItem and child NavigationViewItems will each have their own InfoBadge property. You can bind the value of the parent's InfoBadge to factors that determine the children's InfoBadge values, such as showing the sum of the children's numeric InfoBadges on the parent's InfoBadge.

This image shows a hierarchical NavigationView with its PaneDisplayMode set to Top, where the top-level (parent) item shows a numeric InfoBadge. The app has set the parent item InfoBadge to represent what's being displayed in the child items' InfoBadges, as the child items are currently not expanded (and therefore not visible).

:::image type="content" source="images/infobadge/navview-top-hierarchical.png" alt-text="Hierarchical NavigationView with an InfoBadge":::

## Using an InfoBadge in another control

You might want to show alerts or notifications on elements within your app other than NavigationView. You might have a [ListViewItem](/uwp/api/windows.ui.xaml.controls.listviewitem) that needs special attention, or a menu item that displays a notification. In these cases, you can integrate InfoBadge directly into your UI with other controls.

InfoBadge is a [UIElement](/uwp/api/windows.ui.xaml.uielement) and therefore cannot be used as a shared resource.

To do this, use InfoBadge as you would any other control – simply add the InfoBadge markup where you'd like it to appear. Since InfoBadge inherits from [Control](/windows/winui/api/microsoft.ui.xaml.controls.control), it has all the built-in positioning properties, such as margin, alignment, padding, and more, which you can use to position your InfoBadge exactly where you want it.

If you place an InfoBadge inside of another control, such as a [Button](/uwp/api/windows.ui.xaml.controls.button) or a [ListViewItem](/uwp/api/windows.ui.xaml.controls.listviewitem), it will likely get cropped if you position it to extend beyond the bounding box of the parent control. If your InfoBadge is inside of another control, it should not be positioned past the corners of the control's overall bounding box.

### Example: Placing an InfoBadge inside another control

Here's a Button that has an InfoBadge placed in its upper right hand corner, with the badge layered on top of the content. This example can be applied to many controls other than Button as well – it simply shows how to place, position, and show an InfoBadge inside of another WinUI control.

```xaml
<Button Width="200" Height="60" Padding="4"
        HorizontalContentAlignment="Stretch" VerticalContentAlignment="Stretch">
    <Grid>
        <SymbolIcon Symbol="Sync"/>
        <muxc:InfoBadge x:Name="buttonInfoBadge"
                        Background="#C42B1C"
                        HorizontalAlignment="Right" 
                        VerticalAlignment="Top"
                        Width="16" Height="16">
            <muxc:InfoBadge.IconSource>
                <muxc:FontIconSource Glyph="&#xEA6A;"/>
            </muxc:InfoBadge.IconSource>
        </muxc:InfoBadge>
    </Grid>
</Button>
```

:::image type="content" source="images/infobadge/button-infobadge.png" alt-text="Button with an icon InfoBadge":::

## Managing an InfoBadge

An InfoBadge typically displays a transient alert, so it's common to show or hide it, and change it's style periodically while your app is running.

### Showing and hiding an InfoBadge

You can use either the [Visibility](/uwp/api/windows.ui.xaml.uielement.visibility) property or [Opacity](/uwp/api/windows.ui.xaml.uielement.opacity) property to show and hide an InfoBadge based on user actions, program logic, counters, etc.

As with other UIElements, setting `Visibility.Collapsed` will make the InfoBadge not take space in your layout, so it might cause other elements to move around when it's shown and hidden.

If elements being repositioned is a concern, you can use the `Opacity` property to show and hide the InfoBadge. `Opacity` is set to `1.0` by default; you can set it to `0` to hide the InfoBadge. When you use the `Opacity` property, InfoBadge will still take up space in the layout even if it is currently hidden.

### Change the InfoBadge style

You can change the icon or number displayed in an InfoBadge while it is being shown. Decrementing or incrementing a numeric InfoBadge based on user action can be achieved by changing the value of `InfoBadge.Value`. Changing the icon of an InfoBadge can be achieved by setting `InfoBadge.IconSource` to a new `IconSource` object. When changing icons, ensure that the new icon is the same size as the old icon to avoid a jarring visual effect.

### Default behavior

If neither `InfoBadge.Value` nor `InfoBadge.IconSource` are set, the InfoBadge defaults to showing a dot (specifically if `Value` is set to `-1` and `IconSource` is set to `null`, which are the default values). If both the `Value` and `IconSource` properties are set, the InfoBadge will honor the `Value` property and display a number value.

You can also change the InfoBadge's type while it is being shown. To change the type of InfoBadge, be sure that the current type's corresponding property (`Value` or `IconSource`) is set to its default value (`-1` or `null`), and set the new type's property equal to an appropriate value. To change the type of InfoBadge from numeric or icon to a dot type InfoBadge, make sure that `InfoBadge.Value` is set to `-1` and `InfoBadge.IconSource` is set to `null`.

Depending on how you've positioned your InfoBadge, be aware that this may cause items to shift as the size and shape of the InfoBadge may change.

## Accessibility

The InfoBadge control does not have any screenreader functionality or UI automation built in to it on its own, as the control is not focusable and cannot be interacted with.

If you're using an InfoBadge inside of a NavigationView, the NavigationView provides built-in screenreader and assistive technology support. When you're tabbing through a NavigationView and you land on a NavigationViewItem with an InfoBadge on it, the screenreader will announce that there is an InfoBadge on this item. If the InfoBadge in question is numeric, the screenreader will announce the InfoBadge's value as well.

If you are using InfoBadge outside of a NavigationView, we recommend the following to ensure your app is fully accessible:

- The parent element of the InfoBadge should be focusable and accessible by tab.
- The parent element announces the InfoBadge to screenreaders.
- The app sends a UIA notification when the InfoBadge appears for the first time.
- The app sends a UIA notification when an InfoBadge disappears from the UI.
- The app sends a UIA notification when a significant change has occurred with an existing InfoBadge.
  - The definition of "significant change" is up to you as the individual developer. Examples of this can include: an InfoBadge switching between different types, an InfoBadge changing color to represent its status, or an InfoBadge's value exceeding a certain significant number.

To control what the parent element announces to screenreaders, you can use attached properties of the [AutomationProperties](/uwp/api/windows.ui.xaml.automation.automationproperties) class. For InfoBadge, it's recommended that you set either the `AutomationProperties.FullDescription` or `AutomationProperties.ItemStatus` attached properties on the parent element.

To send UIA notifications upon the InfoBadge's appearance or dismissal, you can use the [AutomationPeer.RaiseAutomationEvent](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.raiseautomationevent) method.

The InfoBadge comes at a default size that meets accessibility requirements. Developers can customize many aspects of the InfoBadge including its height/width/color, etc. but it's important that the default InfoBadge adheres to our accessibility guidelines for size and color.

## Related articles

- [NavigationView](./navigationview.md)
