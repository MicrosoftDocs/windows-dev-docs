---
author: jwmsft
pm-contact: kisai
design-contact: ksulliv
dev-contact: Shmazlou
doc-status: Published
Description: Touch menu accelerant for any scenario
title: Swipe
label: Swipe
template: detail.hbs
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Swipe

Swipe commanding is an accelerator for context menus that enables touch users to easily access common menu actions without needing to change states within the app.

![Execute and Reveal light theme](images/LightThemeSwipe.png)

## Is this the right control?

Swipe commanding saves space, is useful in situations where the same operation may be repeated multiple times over in quick succession, and provides “quick actions” on items that don’t need a full popup or state change within the page.

This commanding type should be used when you have a potentially large group of items which all have 1-3 actions that a user may want to perform on them regularly. These actions may include, but are not limited to:

-	Deleting an item
-	Marking or archiving an item
-	Saving/downloading
-	Replying

Remember to keep the menu items you have in your swipe content to short, and concise text labels. These actions should be the primary ones that a user may want to perform multiple times over a short period.

## Examples

<div style="overflow: hidden; margin: 0 -8px;">
    <div style="float: left; margin: 0 8px 16px; min-width: calc(25% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <div style="height: 133px; width: 100%">
            <img src="images/xaml-controls-gallery.png" alt="XAML controls gallery"></img>
        </div>
    </div>
    <div style="float: left; margin: -22px 8px 16px; min-width: calc(75% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/SwipeControl">open the app and see the SwipeControl in action</a>.</p>
        <ul>
        <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
        <li><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics">Get the source code (GitHub)</a></li>
        </ul>
    </div>
</div>

## How does Swipe work?

UWP swipe commanding has two modes: Reveal and Execute, and four different swipe directions: up, down, left, and right.

### Reveal mode

In reveal mode, the user swipes an item to open a menu of one or more commands, and must explicitly tap a command to execute it. When the user swipes and releases an item, the menu remains open until either a command is selected, or the menu is closed again through swiping back, tapping off, or scrolling the opened swipe item off the screen.

![Swipe to Reveal](images/SwipeCommand-Reveal_v2.gif)

Reveal mode is a safer, more versatile swipe mode, and can be used for most types of menu actions, even potentially destructive actions, such as deletion.

Selecting one of the options shown in the reveal’s open and resting state, invokes the Execute mode for that item and the Swipe control is closed.

### Execute mode

In Execute mode, the user swipes an item open to execute a single command with that one swipe. If the user released the item being dragged before they swipe past a threshold, the menu closes and the command is not executed. If the user swipes past the threshold and then releases the item, the command is executed immediately.

![Swipe to Execute](images/SwipeCommand_Delete_v2.gif)

Not releasing the finger after the threshold is reached, or pulling the swipe item closed again will cancel the execution and no action will be performed on the item.

Execute mode provides more visual feedback through color and label orientation while an item is being swiped.

Execute is best used when the action the user is performing is most common.

It may also be used for more destructive actions like deleting an item, however, keep in mind that execute only requires one action of swiping in a direction, as opposed to swipe to reveal which requires an explicit clicking on a button.

### Swipe directions

Swipe works in all cardinal directions: up, down, left and right. Each swipe direction can hold their own swipe items or content, but only one instance of a direction can be set at a time on a single swipe-able element.

For example, you *cannot* have two ``` LeftItems ``` definitions on the same SwipeControl.

## How to create a Swipe command

There are two components to Swipe that need to be defined: the **SwipeControl** which wraps around your content (and sits within your DataTemplate if in a collection), and the **SwipeContent** which is defined in the Resources of your page/app.

An example of a SwipeControl wrapped around some text:

```XAML
<SwipeControl x:Name="ListViewSwipeContainer"
             LeftItems="{StaticResource RevealOptions}"
             RightItems="{StaticResource ExecuteDelete}">
    <StackPanel Orientation="Vertical" Margin="5" Background="DarkGray">
        <TextBlock Text="Label" FontSize="18"/>
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit..." FontSize="12"/>
        </StackPanel>
    </StackPanel>
</SwipeControl>
```

In this example we'll be setting up both an **Execute** mode and a **Swipe to Reveal** mode, both will be defined in the **<Page.Resources>** of our page:

```XAML
<SwipeItems x:Key="RevealOptions" Mode="Reveal">
    <SwipeItem Text="Reply" IconSource="{StaticResource ReplyIcon}"/>
    <SwipeItem Text="Pin" IconSource="{StaticResource PinIcon}"/>
</SwipeItems>

<SwipeItems x:Key="ExecuteDelete" Mode="Execute">
    <SwipeItem Text="Delete" IconSource="{StaticResource DeleteIcon}" Background="Red"/>
</SwipeItems>
```

SwipeItems also take a new type for their Icons, called IconSource, and this can also be defined in **<Page.Resources>**:

```XAML
<SymbolIconSource x:Key="ReplyIcon" Symbol="MailReply"/>
<SymbolIconSource x:Key="DeleteIcon" Symbol="Delete"/>
<SymbolIconSource x:Key="PinIcon" Symbol="Pin"/>
```

Setting up Swipe to work in a collection or ListView is exactly the same as defining a single swipe (example above), except you define your SwipeControl in a DataTemplate instead of simply defining it like shown above.

Like so:

```XAML
<ListView x:Name="lv" Width="400" Height="300">
    <ListView.ItemTemplate>
        <DataTemplate>
            <SwipeControl x:Name="ListViewSwipeContainer"
                     LeftItems="{StaticResource RevealOptions}"
                     RightItems="{StaticResource ExecuteDelete}">
                <StackPanel Orientation="Vertical" Margin="5">
                    <TextBlock Text="{x:Bind}" FontSize="18"/>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit..." FontSize="12"/>
                    </StackPanel>
                </StackPanel>
           </SwipeControl>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Accessing an invoked Swipe command

In the common case, Swipe will be in a ListView or list-like scenario. In that case when a Swipe is invoked or executed on, you will want to perform an action on that swiped item. To do so you will want to set the **Invoked();** property on the SwipeItem:

```XAML
<SwipeItems x:Key="ExecuteDelete" Mode="Execute">
    <SwipeItem Text="Delete" IconSource="{StaticResource DeleteIcon}" Invoked="delete_Invoked"/>
</SwipeItems>
```

Then, in you're corresponding cs file, you can grab the index of the item in the list, that that Invoked event was fired on:

```csharp
private void delete_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
{
    int i = sampleList.Items.IndexOf(args.Parent.DataContext);
    sampleList.Items.RemoveAt(i);
}
```

In this particular instance, we are removing the item from the list, but in situations where you simply want to perform an action and then have the Swipe collapse again, you can set it up to do so by setting the **BehaviorOnInvoked** enum to either **Auto**, **Close**, or **RemainOpen**.

- **Auto** will mean that when the SwipeItems is in Execute mode, the opened Swipe will *not* collapse when invoked and when in Reveal mode, it *will* collapse when invoked
- **Close** means that when the invoked event is called, the Swipe will always collapse and return to normal no matter the mode
- **RemainOpen** means that the Swipe will remain open after the invoked event is called

```XAML
<SwipeItem Text="Reply" Background="#ff4286f4" IconSource="{StaticResource ReplyIcon}" Invoked="reply_Invoked" BehaviorOnInvoked ="Close"/>
```

You can also reset the Swipe (essentially collapsing it and returning it to normal) by calling **Reset()** from the SwipeControl in code-behind on an Invoked event:

```csharp
private void reply_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
{
    ((SwipeControl)args.Parent).Reset();
}
```

## Dos and don'ts

- Don’t use Swipe in FlipViews, Hubs or Pivots, the combination may be confusing for the user because of conflicting swipe directions
- Don’t combine horizontal Swipe with horizontal navigation, or vertical Swipe with vertical navigation
- Do make sure what the user is swiping is the same action, and is consistent across all related items that can be swiped
- Do use Swipe on items where the same action is repeated many times
- Do use horizontal swiping on wider items, and vertical swiping on taller items

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics) - See all the XAML controls in an interactive format.

## Related articles

- [**Pull to refresh**](pull-to-refresh.md)
