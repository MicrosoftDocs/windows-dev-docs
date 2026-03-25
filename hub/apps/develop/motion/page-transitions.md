---
title: Page transitions
description: Learn how to use WinUI page transitions to give users feedback about the relationship between pages in your app.
template: detail.hbs
ms.date: 10/29/2025
ms.topic: how-to
ms.localizationpriority: medium
ms.custom: RS5
---
# Page transitions

Page transitions navigate users between pages in an app, providing feedback as the relationship between pages. Page transitions help users understand if they are at the top of a navigation hierarchy, moving between sibling pages, or navigating deeper into the page hierarchy.

Two different animations are provided for navigation between pages in an app, *Page refresh* and *Drill*, and are represented by subclasses of [**NavigationTransitionInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.navigationtransitioninfo).

> [!div class="checklist"]
>
> - **Important APIs**: [NavigationTransitionInfo class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.navigationtransitioninfo), [EntranceNavigationTransitionInfo class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.entrancenavigationtransitioninfo), [DrillInNavigationTransitionInfo class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.drillinnavigationtransitioninfo), [SuppressNavigationTransitionInfo class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.suppressnavigationtransitioninfo)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see Implicit Transitions in action](winui3gallery://item/PageTransition)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## Page refresh

Page refresh is a combination of a slide up animation and a fade in animation for the incoming content. Use page refresh when the user is taken to the top of a navigational stack, such as navigating between tabs or left-nav items.

The desired feeling is that the user has started over.

![page refresh animation](../../design/motion/images/page-refresh.gif)

The page refresh animation is represented by the [**EntranceNavigationTransitionInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.entrancenavigationtransitioninfo) class.

```csharp
// Explicitly play the page refresh animation.
myFrame.Navigate(typeof(Page2), null, new EntranceNavigationTransitionInfo());

```

> [!NOTE]
> A [**Frame**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame) automatically uses [**NavigationThemeTransition**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.navigationthemetransition) to animate navigation between two pages. By default, the animation is page refresh.

## Drill

Use drill when users navigate deeper into an app, such as displaying more information after selecting an item.

The desired feeling is that the user has gone deeper into the app.

![drill animation](../../design/motion/images/drill.gif)

The drill animation is represented by the [**DrillInNavigationTransitionInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.drillinnavigationtransitioninfo) class.

```csharp
// Play the drill in animation.
myFrame.Navigate(typeof(Page2), null, new DrillInNavigationTransitionInfo());
```

## Horizontal slide

Use horizontal slide to show that sibling pages appear next to each other. The [NavigationView](../ui/controls/navigationview.md) control automatically uses this animation for top nav, but if you are building your own horizontal navigation experience, then you can implement horizontal slide with SlideNavigationTransitionInfo.

The desired feeling is that the user is navigating between pages that are next to each other. 

```csharp
// Navigate to the right, ie. from LeftPage to RightPage.
myFrame.Navigate(typeof(RightPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight } );

// Navigate to the left, ie. from RightPage to LeftPage.
myFrame.Navigate(typeof(LeftPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft } );
```

## Suppress

To avoid playing any animation during navigation, use [**SuppressNavigationTransitionInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.suppressnavigationtransitioninfo) in the place of other **NavigationTransitionInfo** subtypes.

```csharp
// Suppress the default animation.
myFrame.Navigate(typeof(Page2), null, new SuppressNavigationTransitionInfo());
```

Suppressing the animation is useful if you are building your own transition using [Connected Animations](connected-animation.md) or implicit show/hide animations.

## Backwards navigation

You can use `Frame.GoBack(NavigationTransitionInfo)` to play a specific transition when navigating backwards.

This can be useful when you modify navigation behavior dynamically based on screen size; for example, in a responsive list/detail scenario.

## Related topics

- [Navigate between two pages](../ui/navigation/navigate-between-two-pages.md)
- [Motion in Windows](../../design/signature-experiences/motion.md)
