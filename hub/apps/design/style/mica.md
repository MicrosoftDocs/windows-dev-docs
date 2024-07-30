---
description: Learn to use Mica and Mica Alt, opaque, dynamic materials that incorporate theme and desktop wallpaper to delight users and create visual hierarchy. 
title: Mica material
template: detail.hbs
ms.date: 09/01/2022
ms.topic: article
keywords: windows 11
ms.localizationpriority: medium
---

# Mica material

_Mica_ is an opaque, dynamic material that incorporates theme and desktop wallpaper to paint the background of long-lived windows such as apps and settings. You can apply Mica to your application backdrop to delight users and create visual hierarchy, aiding productivity, by increasing clarity about which window is in focus. Mica is specifically designed for app performance as it only samples the desktop wallpaper once to create its visualization. Mica is available for UWP apps that use WinUI 2 and apps that use Windows App SDK 1.1 or later, while running on Windows 11 version 22000 or later.

![hero image](images/materials/mica-header.png)

:::row:::
    :::column:::
Mica in light theme</br>
![Mica in light theme](images/materials/mica-light-theme.png)
    :::column-end:::
    :::column:::
Mica in dark theme</br>
![Mica in dark theme](images/materials/mica-dark-theme.png)
    :::column-end:::
:::row-end:::

_Mica Alt_ is a variant of Mica, with stronger tinting of the user's desktop background color. You can apply Mica Alt to your app's backdrop to provide a deeper visual hierarchy than Mica, especially when creating an app with a tabbed title bar. Mica Alt is available for apps that use Windows App SDK 1.1 or later, while running on Windows 11 version 22000 or later.

These images show the difference between Mica and Mica Alt in a title bar with tabs. The first image uses Mica and the second image uses Mica Alt.

:::image type="content" source="images/mica-tabs.png" alt-text="Screenshot of Mica in a title bar with tabs.":::

:::image type="content" source="images/mica-alt-tabs.png" alt-text="Screenshot of Mica Alt in a title bar with tabs.":::

## When to use Mica or Mica Alt

Mica and Mica Alt are materials that appear on the backdrop of your application — behind all other content. Each material is opaque and incorporates the user's theme and desktop wallpaper to create its highly personalized appearance. As the user moves the window across the screen, the Mica material dynamically adapts to create a rich visualization using the wallpaper underneath the application. In addition, the material helps users focus on the current task by falling back to a neutral color when the app is inactive.

We recommend that you apply Mica or Mica Alt as the base layer of your app, and prioritize visibility in the title bar area. For more specific layering guidance see [Layering and Elevation](../signature-experiences/layering.md) and the [App layering with Mica](#app-layering-with-mica) section of this article.

## Usability and adaptability

The Mica materials automatically adapt their appearance for a wide variety of devices and contexts. They are designed for performance as they capture the background wallpaper only once to create their visualizations.

In High Contrast mode, users continue to see the familiar background color of their choosing in place of Mica or Mica Alt. In addition, the Mica materials will appear as a solid fallback color (`SolidBackgroundFillColorBase` for Mica, `SolidBackgroundFillColorBaseAlt` for Mica Alt) when:

* The user turns off transparency in Settings > Personalization > Color.
* Battery Saver mode is activated.
* The app runs on low-end hardware.
* An app window on desktop deactivates.
* The Windows app is running on Xbox or HoloLens.
* The Windows version is below 22000.

## App layering with Mica

:::row:::
    :::column:::
Standard pattern content layer<br/>
![Standard content layer](images/materials/mica-l-content.png)
    :::column-end:::
    :::column:::
Card pattern content layer<br/>
![Card pattern content layer](images/materials/mica-card-content.png)
    :::column-end:::
:::row-end:::

Mica is ideal as a foundation layer in your app's hierarchy due to its inactive and active states and subtle personalization. To follow the two-layer [Layering and Elevation](../signature-experiences/layering.md) system, we encourage you to apply Mica as the base layer of your app and add an additional content layer that sits on top of the base layer. The content layer should pick up the material behind it, Mica, using the `LayerFillColorDefaultBrush`, a low-opacity solid color, as its background. Our recommended content layer patterns are:

* **Standard pattern**: A contiguous background for large areas that need a distinct hierarchical differentiation from the base layer. The `LayerFillColorDefaultBrush` should be applied to the container backgrounds of your WinUI app surfaces (e.g. Grids, StackPanels, Frames, etc.).
* **Card pattern**: Segmented cards for apps that are designed with multiple sectioned and discontinuous UI components. For the definition of the card UI using the `LayerFillColorDefaultBrush`, see [Layering and Elevation](../signature-experiences/layering.md) guidance.

To give your app's window a seamless look, Mica should be visible in the title bar if you choose to apply the material to your app. You can show Mica in the title bar by extending your app into the non-client area and creating a transparent custom title bar. For more info, see [Title bar](../basics/titlebar-design.md).

The following examples showcase common implementations of the layering strategy with [NavigationView](../controls/navigationview.md) where Mica is visible in the title bar area.

* Standard pattern in Left NavigationView.
* Standard pattern in Top NavigationView.
* Card pattern in Left NavigationView.

### Standard pattern in Left NavigationView

By default, NavigationView in Left mode includes the content layer in its content area. This example extends Mica into the title bar area and creates a custom title bar.

![Nav View in standard pattern with custom title bar in Left mode](images/materials/mica-light-theme.png)

### Standard pattern in Top NavigationView

By default, NavigationView in Top mode includes the content layer in its content area. This example extends Mica into the title bar area and creates a custom title bar.

![NavigationView in standard pattern with custom title bar in Top mode](images/materials/mica-top.png)

### Card pattern in Left NavigationView

To follow the card pattern using a NavigationView you will need to remove the default content layer by overriding the background and border theme resources. Then, you can create the cards in the content area of the control. This example creates several cards, extends Mica into the title bar area, and creates a custom title bar. For more information on card UI, see [Layering and Elevation](../signature-experiences/layering.md) guidance.

![NavigationView in standard pattern with custom title bar in Left mode](images/materials/mica-left-card.png)

## App layering with Mica Alt

Mica Alt is an alternative to Mica as a foundation layer in your app's hierarchy with the same features like inactive and active states and subtle personalization. We encourage you to apply Mica Alt as the base layer of your app when you need contrast between title bar elements and the commanding areas of your app (e.g. navigation, menus).

A common scenario for using Mica Alt is when you are creating an application with a tabbed title bar. To follow the [Layering and Elevation](../signature-experiences/layering.md) guidance we encourage you to apply Mica Alt as the base layer of your app, add a commanding layer that sits on top of the base layer, and finally add an additional content layer that sits on top of the commanding layer. The commanding layer should pick up the material behind it, Mica Alt, using the `LayerOnMicaBaseAltFillColorDefaultBrush`, a low-opacity solid color, as its background. The content layer should pick up the layers below it, using the `LayerFillColorDefaultBrush`, another low-opacity solid color. The layer system is as follows:

* **Mica Alt**: The base layer.
* **Commanding layer**: Requires distinct hierarchical differentiation from the base layer. The `LayerOnMicaBaseAltFillColorDefaultBrush` should be applied to the commanding areas of your WinUI app surfaces (e.g. MenuBar, navigation structure, etc.)
* **Content layer**: A contiguous background for large areas that need a distinct hierarchical differentiation from the commanding layer. The `LayerFillColorDefaultBrush` should be applied to the container backgrounds of your WinUI app surfaces (e.g. Grids, StackPanels, Frames, etc.).

To give your app's window a seamless look, Mica Alt should be visible in the title bar if you choose to apply the material to your app. You can show Mica Alt in the title bar by extending your app into the non-client area and creating a transparent custom title bar.

## Recommendations

* **Do** set the background to _transparent_ for all layers where you want to see Mica so the Mica shows through.  
* **Don't** apply backdrop material more than once in an application.
* **Don't** apply backdrop material to a UI element. The backdrop material will not appear on the element itself. It will only appear if all layers between the UI element and the window are set to transparent.

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see Mica in action](winui3gallery://item/SystemBackdrops).

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## How to use Mica

You can use Mica in UWP apps that use WinUI 2, or in apps that use Windows App SDK 1.1 or later. You can use Mica Alt in apps that use Windows App SDK 1.1 or later.

### Use Mica with the Windows App SDK

To use Mica in a WinUI 3 XAML app, see [Apply Mica or Acrylic materials in desktop apps for Windows 11](../../windows-app-sdk/system-backdrop-controller.md).

To use Mica in a Win32 app, see [Apply Mica in Win32 desktop apps for Windows 11](../../desktop/modernize/apply-mica-win32.md).

### Use Mica with WinUI 2 for UWP

To use Mica in a UWP app with WinUI 2, see [Apply Mica with WinUI 2 for UWP](/windows/uwp/ui-input/mica-uwp).

## Related articles

- [Materials](../signature-experiences/materials.md)
- [Layering and Elevation](../signature-experiences/layering.md)
- [Apply Mica or Acrylic materials in desktop apps for Windows 11](../../windows-app-sdk/system-backdrop-controller.md)
- [Apply Mica in Win32 desktop apps for Windows 11](../../desktop/modernize/apply-mica-win32.md)
- [NavigationView](../controls/navigationview.md)
