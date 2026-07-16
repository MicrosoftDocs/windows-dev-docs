---
description: Learn to use acrylic, a type of brush that creates a translucent texture to add depth and help establish a visual hierarchy.
title: Acrylic material
template: detail.hbs
ms.date: 07/22/2026
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Acrylic material

![hero image](images/header-acrylic.svg)

Acrylic is a type of [Brush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.brush) that creates a translucent texture. You can apply acrylic to app surfaces to add depth and help establish a visual hierarchy.

> **Important APIs**: [AcrylicBrush class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.acrylicbrush), [Background property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.background), [Window.SystemBackdrop property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop), [DesktopAcrylicBackdrop class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.desktopacrylicbackdrop)

:::row:::
    :::column:::
Acrylic in light theme
![Acrylic in light theme](images/acrylic-light-theme-base.png)
    :::column-end:::
    :::column:::
Acrylic in dark theme
![Acrylic in dark theme](images/acrylic-dark-theme-base.png)
    :::column-end:::
:::row-end:::

## Acrylic and the Fluent Design System

 The Fluent Design System helps you create modern, bold UI that incorporates light, depth, motion, material, and scale. Acrylic is a Fluent Design System component that adds physical texture (material) and depth to your app. To learn more, see [Fluent Design - Material](https://fluent2.microsoft.design/material).

## Acrylic blend types

Acrylic's most noticeable characteristic is its transparency. There are two acrylic blend types that change what's visible through the material:

- **Background acrylic** reveals the desktop wallpaper and other windows that are behind the currently active app, adding depth between application windows while celebrating the user's personalization preferences.
- **In-app acrylic** adds a sense of depth within the app frame, providing both focus and hierarchy.

 ![Background acrylic](images/background-acrylic-dark-theme.png)

 ![In-app acrylic](images/app-acrylic-dark-theme.png)

 Avoid layering multiple acrylic surfaces: multiple layers of background acrylic can create distracting optical illusions.

## When to use acrylic

Consider the following usage patterns to decide how best to incorporate acrylic into your app.

### Transient surfaces

- Use **background acrylic** for transient UI elements.

For apps with context menus, flyouts, non-modal popups, or light-dismiss panes, we recommend that you use background acrylic, especially if these surfaces draw outside the frame of the main app window. Using acrylic in transient scenarios helps maintain a visual relationship with the content that triggered the transient UI.

![The desktop background showing through an open context menu using background acrylic](images/acrylic-transient-context-menu.png)

Many XAML controls draw acrylic by default. [MenuFlyout](../../develop/ui/controls/menus.md), [AutoSuggestBox](../../develop/ui/controls/auto-suggest-box.md), [ComboBox](../../develop/ui/controls/combo-box.md), and similar controls with light-dismiss popups all use acrylic while open.

### Supporting UI and vertical panes

- Use **in-app acrylic** for supporting UI, such as on surfaces that may overlap content when scrolled or interacted with.

If you are using in-app acrylic on navigation surfaces, consider extending content beneath the acrylic pane to improve the flow in your app. Using [NavigationView](../../develop/ui/controls/navigationview.md) will do this for you automatically. However, to avoid creating a striping effect, try not to place multiple pieces of acrylic edge-to-edge - this can create an unwanted seam between the two blurred surfaces. Acrylic is a tool to bring visual harmony to your designs, but when used incorrectly can result in visual noise.

For vertical panes or surfaces that help section off content of your app, we recommend you use an opaque background instead of acrylic. If your vertical panes open on top of content, like in NavigationView's **Compact** or **Minimal** modes, we suggest you use in-app acrylic to help maintain the page's context when the user has this pane open.

> [!NOTE]
> Rendering acrylic surfaces is GPU-intensive, which can increase device power consumption and shorten battery life. Acrylic effects are automatically disabled when a device enters Battery Saver mode. Users can disable acrylic effects for all apps by turning off _Transparency effects_ in Settings > Personalization > Colors.

## Usability and adaptability

Acrylic automatically adapts its appearance for a wide variety of devices and contexts.

In High Contrast mode, users continue to see the familiar background color of their choosing in place of acrylic. In addition, both background acrylic and in-app acrylic appear as a solid color:

- When the user turns off _Transparency effects_ in Settings > Personalization > Colors.
- When Battery Saver mode is activated.
- When the app runs on low-end hardware.

In addition, only background acrylic will replace its translucency and texture with a solid color:

- When an app window on desktop deactivates.
- When the app is running on Xbox, HoloLens, or in tablet mode.

### Legibility considerations

It's important to ensure that any text your app presents to users meets contrast ratios (see [Accessible text requirements](../accessibility/accessible-text-requirements.md)). We've optimized the acrylic resources such that text meets contrast ratios on top of acrylic. We don't recommend placing accent-colored text on your acrylic surfaces because these combinations are likely to not pass minimum contrast ratio requirements at the default 14px font size. Try to avoid placing [hyperlinks](../../develop/ui/controls/hyperlinks.md) over acrylic elements. Also, if you choose to customize the acrylic tint color or opacity level, keep the impact on legibility in mind.

## Apply acrylic in your app

To learn how to apply background acrylic or in-app acrylic in your app, including how to create custom acrylic brushes, see [Apply Mica or Acrylic materials in desktop apps for Windows 11](/windows/apps/develop/ui/system-backdrops).

## Do's and don'ts

- **Do** use acrylic on transient surfaces.
- **Do** extend acrylic to at least one edge of your app to provide a seamless experience by subtly blending with the app's surroundings.
- **Don't** put desktop acrylic on large background surfaces of your app.
- **Don't** place multiple acrylic panes next to each other because this results in an undesirable visible seam.
- **Don't** place accent-colored text over acrylic surfaces.

## How we designed acrylic

We fine-tuned acrylic's key components to arrive at its unique appearance and properties. We started with translucency, blur, and noise to add visual depth and dimension to flat surfaces. We added an exclusion blend mode layer to ensure contrast and legibility of UI placed on an acrylic background. Finally, we added color tint for personalization opportunities. In concert these layers add up to a fresh, usable material.

![Acrylic recipe](images/acrylic-recipe-diagram.jpg)
<br/>The acrylic recipe: background, blur, exclusion blend, color/tint overlay, noise

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see background Acrylic in action](winui3gallery://item/SystemBackdrops)
>
> [Open the WinUI 3 Gallery app and see the in-app AcrylicBrush in action](winui3gallery://item/Acrylic)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## Related articles

[Fluent Design overview](../index.md)
- [Mica material](mica.md)
- [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery) — see Acrylic and other materials in action
- [Apply Mica or Acrylic materials in desktop apps](/windows/apps/develop/ui/system-backdrops)