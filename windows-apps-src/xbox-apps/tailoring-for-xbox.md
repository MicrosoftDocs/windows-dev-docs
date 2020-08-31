---
title: Xbox best practices
description: Learn how to optimize your Universal Windows Platform (UWP) application for Xbox One by following these Xbox development best practices.
ms.date: 10/12/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Xbox best practices

By default, all UWP apps will run on Xbox One without any extra effort on your part. However, if want your app to shine, delight your customers, and compete with the best app experiences on Xbox, you should follow the practices below.
  > [!NOTE]
  > Before you start, take a look at the design guidelines laid out in [Designing for Xbox and TV](../design/devices/designing-for-tv.md).   

## To build the best experiences for Xbox One

### *Do:* Turn off mouse mode

Xbox users love their controllers. To optimize for controller input, [disable mouse mode](how-to-disable-mouse-mode.md) and enable directional navigation (also known as [XY focus navigation and interaction](../design/input/gamepad-and-remote-interactions.md#xy-focus-navigation-and-interaction)). Watch out for focus traps and inaccessible UI.

### *Do:* Draw a focus rectangle that is appropriate for a 10-foot experience

Most Xbox users are sitting across the living room from their TV, so keep in mind that the standard focus rectangle is hard to see from ten feet away. To ensure that the UI element with the input focus is clearly visible to the user at all times, follow the [Focus visual](../design/input/gamepad-and-remote-interactions.md#focus-visual) guidelines. In XAML you will get this behavior for free when your app runs on Xbox, but HTML apps will need to use a custom CSS style.

###	*Do:* Integrate with the SystemMediaTransportControls class

Xbox users want to control media apps with the Xbox Media Remote, Cortana (especially the "Play" and "Pause" voice commands), and Xbox SmartGlass. To get these features for free your app should use the [SystemMediaTransportControls](/uwp/api/windows.media.systemmediatransportcontrols) class, which is automatically included in the Xbox media controls. If your app has custom media controls, make sure to integrate with the **SystemMediaTransportControls** class to provide these features to your users. If you are creating a background music app, integrate with the **SystemMediaTransportControls** class to ensure that the background music controls work correctly in the Xbox multitasking tab.

<!-- ### *Do:* Use adaptive UI to account for snapped apps
One of the unique features of Xbox One is that users can snap apps such as Cortana next to any other app, so your app should respond gracefully when it runs in *fill mode*. Implement [adaptive UI](../get-started/universal-application-platform-guide.md#design-adaptive-ui-with-adaptive-panels) and make sure to test your app during development by snapping an app next to it. -->

### *Consider:* Draw to the edge of the screen

Many TVs cut off the edges of the display, so all of your app's important content should be displayed within the [TV-safe area](../design/devices/designing-for-tv.md#tv-safe-area). UWP uses *overscan* to keep the content within the TV-safe area, but  this default behavior can draw an obvious border around your app. To provide the best experience, turn off the default behavior and follow the instructions at [How to draw UI to the edge of the screen](turn-off-overscan.md).
> [!IMPORTANT]
  > If you disable overscan, it's your responsibility to make sure that interactive elements and text remain within the TV-safe area. 

###	*Consider:* Use TV-safe colors

TVs don't handle extreme color intensities as well as computer monitors do. Avoid high-intensity colors in your app so that users don't see odd banded effects or a washed-out image. Also, be aware that differences between TVs mean that colors that look great on *your* TV might look very different to your users. Read [Colors](../design/devices/designing-for-tv.md#colors) to understand how to make your app look great to everybody!

### *Remember:* You can disable scaling

UWP apps are automatically scaled to ensure that UI elements such as controls and fonts are legible on all devices. Apps that use XAML are scaled by 200%, while apps that use HTML are scaled by 150%. If you want more control over how your app looks on Xbox, disable the default scale factor to use the actual pixel dimensions of an HDTV (1920x1080). Take a look at [How to turn off scaling](disable-scaling.md) and [Effective pixels and scaling](../design/basics/design-and-ui-intro.md#effective-pixels-and-scaling) for information about tailoring your app to look great on Xbox.

If you want to get a glimpse of these practices applied to a UWP app, check out this video!
</br>
</br>
<iframe src="https://channel9.msdn.com/Blogs/One-Dev-Minute/Tailoring-your-UWP-app-for-Xbox/player" width="960" height="540" allowFullScreen frameBorder="0"></iframe>

## Channel 9

The following talks on [Channel 9](https://channel9.msdn.com/) are a great source of information for building amazing apps on Xbox:

- [Building Great Universal Windows Platform (UWP) Apps for Xbox](https://channel9.msdn.com/Events/Build/2016/B883)
- [Adapt Your App for Xbox One and TV](https://channel9.msdn.com/Events/Build/2016/T651-R1)
- [UWP Development 1: Building an Adaptive UI](https://channel9.msdn.com/Events/Build/2016/L724-R1)
- [Web Apps Beyond the Browser: Cross-Platform Meets Cross Device](https://channel9.msdn.com/Events/Build/2016/B888)

## App Dev on Xbox

The **App Dev on Xbox** event is a great starting point for developers new to building apps on Xbox.

* [Watch the recorded sessions](https://developer.microsoft.com/windows/projects/campaigns/app-dev-on-xbox-event#WatchNow)
* [Read the blog posts](https://developer.microsoft.com/windows/projects/campaigns/app-dev-on-xbox-event#BlogSeries)

## See also

- [UWP on Xbox One](index.md)
- [Designing for Xbox and TV](../design/devices/designing-for-tv.md)
- [Progressive Web Apps for Xbox One](/microsoft-edge/progressive-web-apps/xbox-considerations)