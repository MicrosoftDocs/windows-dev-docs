---
title: Materials overview
description: Learn when to use Mica, Desktop Acrylic, SystemBackdropElement, and AcrylicBrush to add visual depth to WinUI 3 app windows and UI elements.
ms.topic: concept-article
ms.date: 07/07/2026
keywords: windows, windows app development, Windows App SDK, Mica, Acrylic, SystemBackdropElement, materials
ms.localizationpriority: medium
---

# Materials in Windows apps

Materials are visual effects applied to UI surfaces to create depth, hierarchy, and visual interest. WinUI 3 provides four mechanisms for applying materials, each suited to a different scenario.

> [!IMPORTANT]
> `AcrylicBrush` is **in-app acrylic only** — it blurs XAML content within the app window. It does not show the desktop or other windows behind the app. For desktop-see-through effects, use `Window.SystemBackdrop` or `SystemBackdropElement`.

## Which material should I use?

| Scenario | Material | API |
|---|---|---|
| App window with a Mica background (wallpaper-tinted) | **Mica** | `Window.SystemBackdrop = new MicaBackdrop()` |
| App window with a see-through frosted-glass background | **Desktop Acrylic** | `Window.SystemBackdrop = new DesktopAcrylicBackdrop()` |
| Flyout, popup, or context menu with an Acrylic background | **System backdrop on a surface** | Set `SystemBackdrop` on `FlyoutBase`, `Popup`, etc. |
| Sidebar, panel, or element with Mica or Acrylic (not the whole window) | **SystemBackdropElement** | `<SystemBackdropElement>` control |
| Navigation pane or content panel with an in-app blur effect | **In-app Acrylic** | `{ThemeResource AcrylicInAppFillColorDefaultBrush}` |

## No material (default)

When no material is applied, the window background is a solid color drawn from the active light or dark theme — no blur or translucency.

:::image type="content" source="images/materials-no-backdrop.png" alt-text="Screenshot of a WinUI 3 app window with no material applied, showing a flat white background.":::

Materials rely on compositor effects that require hardware support and user preferences. WinUI 3 falls back gracefully to a solid color when a material cannot be rendered:

- **Remote Desktop or virtual machines** — the compositor cannot blend with desktop content over RDP, so `SystemBackdrop` materials fall back automatically.
- **Insufficient graphics hardware** — Mica and Desktop Acrylic require DirectX 11 and adequate GPU memory. Devices that don't meet the threshold fall back to a solid theme color.
- **Transparency effects disabled** — if the user turns off *Transparency effects* in **Settings > Personalization > Colors**, all `SystemBackdrop` materials and `AcrylicBrush` fall back to their solid-color alternatives.
- **Battery Saver active** — Windows disables acrylic (`DesktopAcrylicBackdrop` and `AcrylicBrush`) when Battery Saver is on. Mica is not affected.
- **High contrast mode** — all materials are suppressed; the system applies high-contrast theme colors instead.

Your app doesn't need to detect these conditions — the APIs handle fallback automatically. Make sure any `FallbackColor` or theme background you configure reads well as a plain solid color.

## Mica

Mica incorporates the user's desktop wallpaper color into a muted, personalized background. It is designed for the main app window background, especially in the title bar and navigation pane areas.

- **API**: `Window.SystemBackdrop = new MicaBackdrop()`
- **Variants**: `MicaKind.Base` (default) or `MicaKind.BaseAlt` (lighter, for secondary surfaces)
- **Windows version**: Windows 11 only. Falls back to a solid theme color on Windows 10.

:::image type="content" source="images/materials-mica-backdrop.png" alt-text="Screenshot of a WinUI 3 app with MicaBackdrop applied, showing a subtle blue-gray background tinted by the user's desktop wallpaper.":::

## Desktop Acrylic

Desktop Acrylic shows a live, blurred view of the desktop and content behind the app window, creating a frosted-glass appearance.

- **API**: `Window.SystemBackdrop = new DesktopAcrylicBackdrop()`
- **Variants**: `DesktopAcrylicKind.Base` (more opaque) or `DesktopAcrylicKind.Thin` (more transparent)
- **Windows version**: Windows 10 (build 17763) and later.

:::image type="content" source="images/materials-desktop-acrylic-backdrop.png" alt-text="Screenshot of a WinUI 3 app with DesktopAcrylicBackdrop applied, showing a frosted-glass blur of desktop content behind the window.":::

Acrylic can also be applied to transient surfaces — set `SystemBackdrop` on `FlyoutBase`, `Popup`, `MenuFlyoutPresenter`, or `CommandBarFlyoutCommandBar`. See [System backdrops](system-backdrops.md) for the full list.

## SystemBackdropElement

`SystemBackdropElement` applies a Mica or Desktop Acrylic material from the OS compositor to a specific XAML element — not the whole window. Use it when a sidebar, panel, or card needs its own Mica or Acrylic background independently of the rest of the window.

- **Minimum SDK**: Windows App SDK 1.6.3
- For details and code samples, see [Apply a system backdrop to any XAML element](system-backdrops.md#apply-a-system-backdrop-to-any-xaml-element).

## In-app Acrylic (AcrylicBrush)

`AcrylicBrush` blurs XAML content within the app window. It does not show the desktop or other windows through the surface. Use it for navigation panes, sidebars, or content panels where you want a translucent in-app effect.

- **API**: Apply `{ThemeResource AcrylicInAppFillColorDefaultBrush}` to an element `Background`, or define a custom `AcrylicBrush` with `TintColor`, `TintOpacity`, and `TintLuminosityOpacity`.
- The UWP `BackgroundSource = HostBackdrop` property is not available in WinUI 3.
- For details and code samples, see [In-app acrylic](in-app-acrylic.md).

:::image type="content" source="images/materials-acrylic-brush.png" alt-text="Screenshot of a WinUI 3 app with AcrylicBrush applied, showing a translucent panel that blurs XAML content within the app window.":::

## Related articles

- [System backdrops (Mica and Desktop Acrylic)](system-backdrops.md)
- [In-app acrylic](in-app-acrylic.md)
- [Materials in Windows 11 design guidance](../../design/signature-experiences/materials.md)
- [Mica design guidance](../../design/style/mica.md)
- [Acrylic design guidance](../../design/style/acrylic.md)
