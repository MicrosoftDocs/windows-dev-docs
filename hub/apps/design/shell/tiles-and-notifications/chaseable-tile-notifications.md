---
description: Use chaseable tile notifications to find out what your app displayed on its Live Tile when the user clicked it.
title: Chaseable tile notifications
ms.assetid: E9AB7156-A29E-4ED7-B286-DA4A6E683638
label: Chaseable tile notifications
template: detail.hbs
ms.date: 06/13/2017
ms.topic: article
keywords: windows 10, uwp, chaseable tiles, live tiles, chaseable tile notifications
ms.localizationpriority: medium
---
# Chaseable tile notifications

Chaseable tile notifications let you determine which tile notifications your app's Live Tile was displaying when the user clicked the tile.  
For example, a news app could use this feature to determine which news story its Live Tile was displaying when the user launched it; it could that ensure that the story is prominently displayed so that the user can find it. 

> [!IMPORTANT]
> **Requires Anniversary Update**: To use chaseable tile notifications with C#, C++, or VB-based UWP apps, you must target SDK 14393 and be running build 14393 or later. For JavaScript-based UWP apps, you must target SDK 17134 and be running build 17134 or later. 


> **Important APIs**: [LaunchActivatedEventArgs.TileActivatedInfo property](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs.TileActivatedInfo), [TileActivatedInfo class](/uwp/api/windows.applicationmodel.activation.tileactivatedinfo)


## How it works

To enable chaseable tile notifications, you use the **Arguments** property on the tile notification payload, similar to the launch property on the toast notification payload, to embed info about the content in the tile notification.

When your app is launched via the Live Tile, the system returns a list of arguments from the current/recently displayed tile notifications.


## When to use chaseable tile notifications

Chaseable tile notifications are typically used when you're using the notification queue on your Live Tile (which means you are cycling through up to 5 different notifications). They're also beneficial when the content on your Live Tile is potentially out of sync with the latest content in the app. For example, the News app refreshes its Live Tile every 30 minutes, but when the app is launched, it loads the latest news (which may not include something that was on the tile from the last polling interval). When this happens, the user might get frustrated about not being able to find the story they saw on their Live Tile. That's where chaseable tile notifications can help, by allowing you to make sure that what the user saw on their Tile is easily discoverable.

## What to do with a chaseable tile notifications

The most important thing to note is that in most scenarios, **you should NOT directly navigate to the specific notification** that was on the Tile when the user clicked it. Your Live Tile is used as an entry point to your application. There can be two scenarios when a user clicks your Live Tile: (1) they wanted to launch your app normally, or (2) they wanted to see more information about a specific notification that was on the Live Tile. Since there's no way for the user to explicitly say which behavior they want, the ideal experience is to **launch your app normally, while making sure that the notification the user saw is easily discoverable**.

For example, clicking the MSN News app's Live Tile launches the app normally: it displays the home page, or whichever article the user was previously reading. However, on the home page, the app ensures that the story from the Live Tile is easily discoverable. That way, both scenarios are supported: the scenario where you simply want to launch/resume the app, and the scenario where you want to view the specific story.


## How to include the Arguments property in your tile notification payload

In a notification payload, the arguments property enables your app to provide data you can use to later identify the notification. For example, your arguments might include the story's id, so that when launched, you can retrieve and display the story. The property accepts a string, which can be serialized however you like (query string, JSON, etc), but we typically recommend query string format, since it's lightweight and XML-encodes nicely.

The property can be set on both the **TileVisual** and the **TileBinding** elements, and will cascade down. If you want the same arguments on every tile size, simply set the arguments on the **TileVisual**. If you need specific arguments for specific tile sizes, you can set the arguments on individual **TileBinding** elements.

This example creates a notification payload that uses the arguments property so that notification can be identified later. 

```csharp
// Uses the following NuGet packages
// - Microsoft.Toolkit.Uwp.Notifications
// - QueryString.NET
 
TileContent content = new TileContent()
{
    Visual = new TileVisual()
    {
        // These arguments cascade down to Medium and Wide
        Arguments = new QueryString()
        {
            { "action", "storyClicked" },
            { "story", "201c9b1" }
        }.ToString(),
 
 
        // Medium tile
        TileMedium = new TileBinding()
        {
            Content = new TileBindingContentAdaptive()
            {
                // Omitted
            }
        },
 
 
        // Wide tile is same as Medium
        TileWide = new TileBinding() { /* Omitted */ },
 
 
        // Large tile is an aggregate of multiple stories
        // and therefore needs different arguments
        TileLarge = new TileBinding()
        {
            Arguments = new QueryString()
            {
                { "action", "storiesClicked" },
                { "story", "43f939ag" },
                { "story", "201c9b1" },
                { "story", "d9481ca" }
            }.ToString(),
 
            Content = new TileBindingContentAdaptive() { /* Omitted */ }
        }
    }
};
```


## How to check for the arguments property when your app launches

Most apps have an App.xaml.cs file that contains an override for the [OnLaunched](/uwp/api/windows.ui.xaml.application#Windows_UI_Xaml_Application_OnLaunched_Windows_ApplicationModel_Activation_LaunchActivatedEventArgs_) method. As its name suggests, your app calls this method when it's launched. It takes a single argument, a [LaunchActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs) object.

The LaunchActivatedEventArgs object has a property that enables chaseable notifications: the [TileActivatedInfo property](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs.TileActivatedInfo), which provides access to a [TileActivatedInfo object](/uwp/api/windows.applicationmodel.activation.tileactivatedinfo). When the user launches your app from its tile (rather than the app list, search, or any other entry point), your app initializes this property.

The [TileActivatedInfo object](/uwp/api/windows.applicationmodel.activation.tileactivatedinfo) contains a property called [RecentlyShownNotifications](/uwp/api/windows.applicationmodel.activation.tileactivatedinfo.RecentlyShownNotifications), which contains a list of notifications that have been shown on the tile within the last 15 minutes. The first item in the list represents the notification currently on the tile, and the subsequent items represent the notifications that the user saw before the current one. If your tile has been cleared, this list is empty.

Each ShownTileNotification has an Arguments property. The Arguments property will be initialized with the arguments string from your tile notification payload, or null if your payload didn't include the arguments string.

```csharp
protected override void OnLaunched(LaunchActivatedEventArgs args)
{
    // If the API is present (doesn't exist on 10240 and 10586)
    if (ApiInformation.IsPropertyPresent(typeof(LaunchActivatedEventArgs).FullName, nameof(LaunchActivatedEventArgs.TileActivatedInfo)))
    {
        // If clicked on from tile
        if (args.TileActivatedInfo != null)
        {
            // If tile notification(s) were present
            if (args.TileActivatedInfo.RecentlyShownNotifications.Count > 0)
            {
                // Get arguments from the notifications that were recently displayed
                string[] allArgs = args.TileActivatedInfo.RecentlyShownNotifications
                .Select(i => i.Arguments)
                .ToArray();
 
                // TODO: Highlight each story in the app
            }
        }
    }
 
    // TODO: Initialize app
}
```


### Accessing OnLaunched from desktop applications

Desktop apps (like WPF, etc) using the [Desktop Bridge](/windows/msix/desktop/source-code-overview), can use chaseable tiles too! The only difference is accessing the OnLaunched arguments. Note that you first must [package your app with the Desktop Bridge](/windows/msix/desktop/source-code-overview).

> [!IMPORTANT]
> **Requires October 2018 Update**: To use the `AppInstance.GetActivatedEventArgs()` API, you must target SDK 17763 and be running build 17763 or later.

For desktop applications, to access the launch arguments, do the following...

```csharp

static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    // API only available on build 17763 or later
    var args = AppInstance.GetActivatedEventArgs();
    switch (args.Kind)
    {
        case ActivationKind.Launch:

            var launchArgs = args as LaunchActivatedEventArgs;

            // If clicked on from tile
            if (launchArgs.TileActivatedInfo != null)
            {
                // If tile notification(s) were present
                if (launchArgs.TileActivatedInfo.RecentlyShownNotifications.Count > 0)
                {
                    // Get arguments from the notifications that were recently displayed
                    string[] allTileArgs = launchArgs.TileActivatedInfo.RecentlyShownNotifications
                    .Select(i => i.Arguments)
                    .ToArray();
     
                    // TODO: Highlight each story in the app
                }
            }
    
            break;
```


## Raw XML example

If you're using raw XML instead of the Notifications library, here's the XML.

```xml
<tile>
  <visual arguments="action=storyClicked&amp;story=201c9b1">
 
    <binding template="TileMedium">
       
      <text>Kitten learns how to drive a car...</text>
      ... (omitted)
     
    </binding>
 
    <binding template="TileWide">
      ... (same as Medium)
    </binding>
     
    <!--Large tile is an aggregate of multiple stories-->
    <binding
      template="TileLarge"
      arguments="action=storiesClicked&amp;story=43f939ag&amp;story=201c9b1&amp;story=d9481ca">
   
      <text>Can your dog understand what you're saying?</text>
      ... (another story)
      ... (one more story)
   
    </binding>
 
  </visual>
</tile>
```



## Related articles

- [LaunchActivatedEventArgs.TileActivatedInfo property](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs#Windows_ApplicationModel_Activation_LaunchActivatedEventArgs_TileActivatedInfo_)
- [TileActivatedInfo class](/uwp/api/windows.applicationmodel.activation.tileactivatedinfo)
