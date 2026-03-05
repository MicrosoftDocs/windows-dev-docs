---
title: Materials overview
description: Learn about materials in Windows apps, including Mica, Acrylic, and in-app acrylic brushes.
ms.topic: concept-article
ms.date: 02/27/2026
keywords: windows, windows app development, Windows App SDK, Mica, Acrylic, materials
ms.localizationpriority: medium
---

# Materials in Windows apps

[Materials](../../design/signature-experiences/materials.md) are visual effects applied to UX surfaces that resemble real life artifacts. They play a key role in creating depth, hierarchy, and visual interest in your app's interface.

Windows provides two primary material types:

- [**Mica**](../../design/style/mica.md) — An opaque material that incorporates the user's theme and desktop wallpaper to create a highly personalized appearance. Mica is designed for performance as it only captures the background wallpaper once to create its visualization, making it ideal for the foundation layer of your app, especially in the title bar area.

- [**Acrylic**](../../design/style/acrylic.md) — A semi-transparent material that replicates the effect of frosted glass. Acrylic can be used as a system backdrop for transient, light-dismiss surfaces (such as flyouts and context menus), or as an in-app brush applied directly to UI elements.

## How to use materials

There are two ways to use materials in your WinUI apps:

- **[System backdrops (Mica/Acrylic)](system-backdrops.md)** — Apply Mica or Desktop Acrylic as the background of your app window or transient UI surfaces like flyouts and popups. This is set via the `SystemBackdrop` property on `Window`, `Flyout`, `Popup`, and other elements.

- **[In-app acrylic](in-app-acrylic.md)** — Apply acrylic effects directly to UI elements within your app using `AcrylicBrush` theme resources or custom acrylic brushes. This is useful for panels, sidebars, and other content areas where you want a translucent effect.

## Related articles

- [Materials in Windows 11](../../design/signature-experiences/materials.md)
- [Mica design guidance](../../design/style/mica.md)
- [Acrylic design guidance](../../design/style/acrylic.md)
