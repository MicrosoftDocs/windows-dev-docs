---
author: mijacobs
description: Connected animations let you create a dynamic and compelling navigation experience by animating the transition of an element between two different views.
title: Connected animation
template: detail.hbs
ms.author: jimwalk
ms.date: 10/25/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: stmoy
design-contact: conrwi
doc-status: Published
ms.localizationpriority: high
---

# Connected animation for UWP apps

## What is connected animation?

Connected animations let you create a dynamic and compelling navigation experience by animating the transition of an element between two different views. This helps the user maintain their context and provides continuity between the views.
In a connected animation, an element appears to “continue” between two views during a change in UI content, flying across the screen from its location in the source view to its destination in the new view. This emphasizes the common content in between the views and creates a beautiful and dynamic effect as part of a transition.

## See it in action

In this short video, an app uses a connected animation to animate an item image as it “continues” to become part of the header of the next page. The effect helps maintain user context across the transition.

![UI example of continuous motion](images/continuous3.gif)

<!-- 
<iframe width=640 height=360 src='https://microsoft.sharepoint.com/portals/hub/_layouts/15/VideoEmbedHost.aspx?chId=552c725c%2De353%2D4118%2Dbd2b%2Dc2d0584c9848&amp;vId=b2daa5ee%2Dbe15%2D4503%2Db541%2D1328a6587c36&amp;width=640&amp;height=360&amp;autoPlay=false&amp;showInfo=true' allowfullscreen></iframe>
-->

## Video summary

> [!VIDEO https://channel9.msdn.com/Events/Windows/Windows-Developer-Day-Fall-Creators-Update/WinDev005/player]

## Connected animation and the Fluent Design System

 The Fluent Design System helps you create modern, bold UI that incorporates light, depth, motion, material, and scale. Connected animation is a Fluent Design System component that adds motion to your app. To learn more, see the [Fluent Design for UWP overview](../fluent-design-system/index.md).

## Why connected animation?

When navigating between pages, it’s important for the user to understand what new content is being presented after the navigation and how it relates to their intent when navigating. Connected animations provide a powerful visual metaphor that emphasizes the relationship between two views by drawing the user’s focus to the content shared between them. Additionally, connected animations add visual interest and polish to page navigation that can help differentiate the motion design of your app.

## When to use connected animation

Connected animations are generally used when changing pages, though they can be applied to any experience where you are changing content in a UI and want the user to maintain context. You should consider using a connected animation instead of a [drill in navigation transition](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.animation.navigationthemetransition.aspx) whenever there is an image or other piece of UI shared between the source and destination views.

## How to implement

Setting up a connected animation involves two steps:

1.	*Prepare* an animation object on the source page, which indicates to the system that the source element will participate in the connected animation 
2.	*Start* the animation on the destination page, passing a reference to the destination element

In between the two steps, the source element will appear frozen above other UI in the app, allowing you to perform any other transition animations simultaneously. For this reason, you shouldn’t wait more than ~250 milliseconds in between the two steps since the presence of the source element may become distracting. If you prepare an animation and do not start it within three seconds, the system will dispose of the animation and any subsequent calls to **TryStart** will fail.

You can get access to the current ConnectedAnimationService instance by calling **ConnectedAnimationService.GetForCurrentView**. To prepare an animation, call **PrepareToAnimate** on this instance, passing a reference to a unique key and the UI element you want to use in the transition. The unique key allows you to retrieve the animation later, for example on a different page.

```csharp
ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", SourceImage);
```

To start the animation, call **ConnectedAnimation.TryStart**. You can retrieve the right animation instance by calling **ConnectedAnimationService.GetAnimation** with the unique key you provided when creating the animation.

```csharp
ConnectedAnimation imageAnimation = 
    ConnectedAnimationService.GetForCurrentView().GetAnimation("image");
if (imageAnimation != null)
{
    imageAnimation.TryStart(DestinationImage);
}
```

Here is a full example of using ConnectedAnimationService to create a transition between two pages.

*SourcePage.xaml*

```xaml
<Image x:Name="SourceImage"
       Width="200"
       Height="200"
       Stretch="Fill"
       Source="Assets/StoreLogo.png" />
```

*SourcePage.xaml.cs*

```csharp
private void NavigateToDestinationPage()
{
    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", SourceImage);
    Frame.Navigate(typeof(DestinationPage));
}
```

*DestinationPage.xaml*

```xaml
<Image x:Name="DestinationImage"
       Width="400"
       Height="400"
       Stretch="Fill"
       Source="Assets/StoreLogo.png" />
```

*DestinationPage.xaml.cs*

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    base.OnNavigatedTo(e);

    ConnectedAnimation imageAnimation = 
        ConnectedAnimationService.GetForCurrentView().GetAnimation("image");
    if (imageAnimation != null)
    {
        imageAnimation.TryStart(DestinationImage);
    }
}
```

## Connected animation in list and grid experiences

Often, you will want to create a connected animation from or to a list or grid control. You can use two new methods of **ListView** and **GridView**, **PrepareConnectedAnimation** and **TryStartConnectedAnimationAsync**, to simplify this process.
For example, say you have a **ListView** that contains an element with the name "PortraitEllipse" in its data template.

```xaml
<ListView x:Name="ContactsListView" Loaded="ContactsListView_Loaded">
    <ListView.ItemTemplate>
        <DataTemplate x:DataType="vm:ContactsItem">
            <Grid>
                …
                <Ellipse x:Name="PortraitEllipse" … />
            </Grid>
        </DataTemplate>	
    </ListView.ItemTemplate>
</ListView>
```

To prepare a connected animation with the ellipse corresponding to a given list item, call the **PrepareConnectedAnimation** method with a unique key, the item, and the name “PortraitEllipse”.

```csharp
void PrepareAnimationWithItem(ContactsItem item)
{
     ContactsListView.PrepareConnectedAnimation("portrait", item, "PortraitEllipse");
}
```

Alternatively, to start an animation with this element as the destination, for example when navigating back from a detail view, use **TryStartConnectedAnimationAsync**. If you have just loaded the data source for the **ListView**, **TryStartConnectedAnimationAsync** will wait to start the animation until the corresponding item container has been created.

```csharp
private void ContactsListView_Loaded(object sender, RoutedEventArgs e)
{
    ContactsItem item = GetPersistedItem(); // Get persisted item
    if (item != null)
    {
        ContactsListView.ScrollIntoView(item);
        ConnectedAnimation animation = 
            ConnectedAnimationService.GetForCurrentView().GetAnimation("portrait");
        if (animation != null)
        {
            await ContactsListView.TryStartConnectedAnimationAsync(
                animation, item, "PortraitEllipse");
        }
    }
}
```

## Coordinated animation

![Coordinated Animation](images/connected-animations/coordinated_example.gif)

<!--
<iframe width=640 height=360 src='https://microsoft.sharepoint.com/portals/hub/_layouts/15/VideoEmbedHost.aspx?chId=552c725c%2De353%2D4118%2Dbd2b%2Dc2d0584c9848&amp;vId=9066bbbe%2Dcf58%2D4ab4%2Db274%2D595616f5d0a0&amp;width=640&amp;height=360&amp;autoPlay=false&amp;showInfo=true' allowfullscreen></iframe>
-->

A *coordinated animation* is a special type of entrance animation where an element will appear alongside the connected animation target, animating in tandem with the connected animation element as it moves across the screen. Coordinated animations can add more visual interest to a transition and further draw the user’s attention to the context that is shared between the source and destination views. In these images, the caption UI for the item is animating using a coordinated animation.

Use the two-parameter overload of **TryStart** to add coordinated elements to a connected animation. This example demonstrates a coordinated animation of a Grid layout named “DescriptionRoot” that will enter in tandem with a connected animation element named “CoverImage”.

*DestinationPage.xaml*

```xaml
<Grid>
    <Image x:Name="CoverImage" />
    <Grid x:Name="DescriptionRoot" />
</Grid>
```

*DestinationPage.xaml.cs*

```csharp
void OnNavigatedTo(NavigationEventArgs e)
{
    var animationService = ConnectedAnimationService.GetForCurrentView();
    var animation = animationService.GetAnimation("coverImage");
    
    if (animation != null)
    {
        // Don’t need to capture the return value as we are not scheduling any subsequent
        // animations
        animation.TryStart(CoverImage, new UIElement[] { DescriptionRoot });
     }
}
```

## Do’s and don’ts

- Use a connected animation in page transitions where an element is shared between the source and destination pages.
- Don’t wait on network requests or other long-running asynchronous operations in between preparing and starting a connected animation. You may need to pre-load the necessary information to run the transition ahead of time, or use a low-resolution placeholder image while a high-resolution image loads in the destination view.
- Use [SuppressNavigationTransitionInfo](https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.media.animation.suppressnavigationtransitioninfo) to prevent a transition animation in a **Frame** if you are using **ConnectedAnimationService**, since connected animations aren’t meant to be used simultaneously with the default navigation transitions. See [NavigationThemeTransition](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.animation.navigationthemetransition.aspx) for more info on how to use navigation transitions.


## Download the code samples

See the [Connected Animation sample](https://github.com/Microsoft/WindowsUIDevLabs/tree/master/SampleGallery/Samples/SDK%2014393/ConnectedAnimationSample) in the [WindowsUIDevLabs](https://github.com/Microsoft/WindowsUIDevLabs) sample gallery.

## Related articles

- [ConnectedAnimation](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.connectedanimation)
- [ConnectedAnimationService](https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.media.animation.connectedanimation.aspx)
- [NavigationThemeTransition](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.animation.navigationthemetransition.aspx)
