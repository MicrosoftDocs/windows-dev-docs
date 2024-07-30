---
description: An overview of where and how motion is used in Windows 11
title: Motion in Windows 11
ms.date: 07/24/2024
ms.topic: article
keywords: windows 11, design, ui, uiux, motion
ms.localizationpriority: medium
---

# Motion in Windows 11

Motion describes the way the interface animates and responds to user interaction. Motion in Windows is reactive, direct, and context appropriate. It provides feedback to user input and reinforces spatial paradigms that support way-finding.

:::image type="content" source="images/motion-resourceful.gif" alt-text="An animated image that shows several examples of motion in the Windows UI.":::

## Motion principles

These principles guide the use of motion in Windows.

### Connected: Elements of actions connect seamlessly

Elements that change position and size should visually connect from one state to another, even if they aren't connected under the hood. Users are guided to follow elements going from point to point, lowering the cognitive load of static state changes.

_Example:_ When a window transitions between floating, snapped, and maximized, it always feels like the same window.

:::image type="content" source="images/motion-light-effortless.png" lightbox="images/motion-light-effortless.gif" alt-text="An animated image that shows a Microsoft Edge window transitioning between floating, snapped, and maximized views.":::

> [!TIP]
> To improve accessibility and readability, this page uses still images in the default view. You can click an image to see the animated version.

### Consistent: Elements should behave in similar ways when sharing entry points

Surfaces that share the same UI entry point should invoke and dismiss the same way to bring consistency to interactions. Each transition should respect the timing, easing, and direction of other elements so a surface feels cohesive.

_Example:_ All taskbar flyouts slide up when invoked, and slide down when dismissed.

:::image type="content" source="images/motion-consistent.png" lightbox="images/motion-consistent.gif" alt-text="An animated image that shows several Windows UI surfaces in succession, such as the start menu and search pane. Each surface slides up from the taskbar when invoked, and slides down when dismissed, in a consistent manner.":::

_Click the image to see it animated._

### Responsive: The system responds and adapts to user input and choices

Clear indicators show the system recognizes and adapts gracefully to different input, postures, and orientations. Apps should build on OS behaviors to feel responsive, alive, and aid usage depending on input methods.

_Example:_ Taskbar icons spread out when keyboards are detached. Window edges invoke a different visual depending on cursor or touch input.

:::image type="content" source="images/motion-adaptive.png" lightbox="images/motion-adaptive.gif" alt-text="An animated image. On the left, taskbar icons spread out when a keyboard is detached. On the right, window edges have different visual effects when manipulated with the cursor or touch input.":::

_Click the image to see it animated._

### Delightful: Unexpected moments of joy with purpose

Motion adds personality and energy to the experience in order to transform simple actions into moments of delight. These moments are always brief and fleeting, and help reinforce user actions.

_Example:_ Minimizing a window causes an app icon to bounce down, while restoring bounces an app icon up.

:::image type="content" source="images/motion-minimize-restore.png" lightbox="images/motion-minimize-restore.gif" alt-text="An animated image that shows an app icon bounce down when the window is minimized, and bounce up when the window is restored.":::

_Click the image to see it animated._

### Resourceful: Utilizes existing controls to bring consistency where possible

Avoid custom animations where possible. Use animation resources like [WinUI](../../winui/index.md) controls for page transitions, in-page focus, and micro interactions. If you can't use WinUI controls, mimic existing OS behaviors based on where the app entry point lives.

_Example:_ [Page transitions](../motion/page-transitions.md), [connected animations](../motion/connected-animation.md), and [animated icons](../controls/animated-icon.md) are the recommended WinUI controls that add delightful and necessary motion to apps.

:::image type="content" source="images/motion-resourceful.png" lightbox="images/motion-resourceful.gif" alt-text="An animated image that shows examples of page  transitions, connected animations, and animated icons in the Windows UI.":::

_Click the image to see it animated._

## Usage

### Animation properties

Windows motion is fast, direct, and context-appropriate. Timing and easing curves are adjusted based on the purpose of the animation to create a coherent experience.

| Purpose | Definition | Ease | Timing | Used For |
|--|--|--|--|--|
| Direct Entrance | Fast – In| Cubic-bezier(0,0,0,1) | 167, 250, 333 | Position, Scale, Rotation|
| Existing Elements | Point to Point | Cubic-bezier(0.55,0.55,0,1) | 167, 250, 333ms | Position, Scale, Rotation|
| Direct Exit | Fast – Out | Cubic-bezier(0,0,0,1) |167ms| Position, Scale, Rotation (ALWAYS combine with fade out) |
| Gentle Exit | Soft – Out | Cubic-bezier(1,0,1,1) | 167ms | Position, Scale |
| Bare Minimum | Fade – In + Out | Linear | 83ms | Opacity |
| Strong Entrance  | Elastic In (3 Keyframes) |  (3 values below) | (3 values below)  | Position, Scale |
|                 | Keyframe 1 | Cubic-Bezier(0.85, 0, 0, 1) | 167ms | |
|                 | Keyframe 2 | Cubic-Bezier(0.85, 0, 0.75, 1) | 167ms | |
|                 | Keyframe 3 | Cubic-Bezier(0.85, 0, 0, 1) | 333ms | |

### Controls

This release of Windows introduces purposeful micro-interactions in [WinUI](../../winui/index.md) controls. Add these controls to your app to help better organize information, and help your app's users transition from page to page, layer to layer, and state to state of an interaction.

#### Page Transition: Page-to-page transitions within the same surface

Use [page transitions](../motion/page-transitions.md) to transition smoothly from page to page, and configure animation directions to respect the flow of an app.

Page transitions guide your user's eyes to incoming and outgoing content, lowering cognitive load.

:::image type="content" source="images/motion-page-transitions.png" lightbox="images/motion-page-transitions.gif" alt-text="An animated image that shows navigation between several pages in the Windows Settings app. Top-level pages slide up from the bottom. When navigating between top-level and sub-pages, pages slide left and right.":::

_Click the image to see it animated._

#### Connected Animation: Layer-to-layer transitions within the same page

Use [connected animations](../motion/connected-animation.md) to highlight specific pieces of information within a page or surface, while retaining context.

Connected animations give focus to selected elements, and seamlessly transition between the focused and non-focused states.

:::image type="content" source="images/motion-connected-animations.png" lightbox="images/motion-connected-animations.gif" alt-text="An animated image of the Microsoft Store app that shows an image in a page that animates to a zoomed-in view of the image.":::

_Click the image to see it animated._

#### Animated Icon: Adds delight and reveals information through micro interactions

Use [animated icons](../controls/animated-icon.md) to implement lightweight, vector-based icons and illustrations with motion using [Lottie](/windows/communitytoolkit/animations/lottie) animations.

Animated icons draw attention to specific entry points, provide feedback from state to state, and add delight to an interaction.

:::image type="content" source="images/motion-animated-icons.png" lightbox="images/motion-animated-icons.gif" alt-text="An animated image that shows a grid of various examples of animated icon controls.":::

_Click the image to see it animated._
