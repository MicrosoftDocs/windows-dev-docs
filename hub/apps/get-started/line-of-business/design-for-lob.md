---
title: Design for productivity in WinUI LOB apps
description: Design WinUI 3 line-of-business apps for productivity with guidance on theming, materials, accessibility, layouts, and navigation.
ms.topic: concept-article
ms.date: 09/02/2026
author: GrantMeStrength
ms.author: jken
---

# Design for productivity in WinUI LOB apps

Productivity-focused apps must make frequent tasks efficient while remaining readable, accessible, and predictable. Start with WinUI controls and theme resources, then customize only where the workflow requires it.

:::image type="content" source="images/04-design-showcase.png" alt-text="A WinUI 3 dashboard with a NavigationView, Mica backdrop, and summary cards in the light theme.":::

## Use theme resources

WinUI controls respond to light, dark, and contrast themes when you keep their default styles and use theme resources instead of hardcoded colors.

For example, use `CardBackgroundFillColorDefaultBrush` for a layered card surface and `TextFillColorSecondaryBrush` for secondary text. Don't use an old application-page brush as a universal background, and don't assume the default window surface is an opaque solid color.

Test every customized color combination in:

- Light theme
- Dark theme
- Windows contrast themes
- Disabled, pointer-over, pressed, selected, and focused states

See [Theming](../../develop/ui/theming.md).

## Preserve usable control sizing

WinUI doesn't provide a general-purpose compact mode that is appropriate for every control and workflow. Start with the default control sizes. If a screen needs to show more information, first improve grouping, filtering, progressive disclosure, and window adaptation.

When you adjust spacing for a specific workflow, preserve readable text, keyboard focus visuals, pointer targets, and touch targets. Test the actual controls and input methods rather than applying an app-wide density override.

## Use Mica and Acrylic materials deliberately

Use Mica as a base layer that establishes the window hierarchy, such as behind app chrome or navigation. Place readable content on appropriate layered surfaces, including theme-resource card brushes.

Acrylic materials provide separation for transient UI such as flyouts and menus. Avoid placing dense, persistent data directly on a highly translucent surface. Let the platform controls use their intended materials instead of applying a backdrop type to every surface.

- [System backdrops](../../develop/ui/system-backdrops.md)
- [In-app Acrylic](../../develop/ui/in-app-acrylic.md)
- [Mica](../../design/style/mica.md)

## Choose dialogs and windows

Use a `ContentDialog` for a short modal interaction within an existing window. Set the dialog's `XamlRoot` before calling `ShowAsync`.

Create another `Microsoft.UI.Xaml.Window` when users need to work with an independent document, tool window, or secondary view. A separate window has its own content tree and lifetime. Don't treat a secondary `Window` as a direct replacement for every owned or modal window pattern from older desktop frameworks; design and test the owner, activation, and closing behavior your scenario requires.

- [Multiple windows](../../develop/ui/multiple-windows.md)
- [Dialogs and flyouts](../../develop/ui/controls/dialogs-and-flyouts/dialogs.md)

## Design for accessibility

Default WinUI controls provide accessibility behavior, but the app is responsible for meaningful names, relationships, order, and custom interaction.

- Give icon-only controls an `AutomationProperties.Name`.
- Use labels or headers that identify editable fields.
- Keep interactive controls reachable by keyboard in a logical order.
- Keep visible focus indicators.
- Don't communicate state or validation by color alone.
- Test text scaling, contrast themes, Narrator, and keyboard-only operation.

```xml
<Button
    AutomationProperties.AutomationId="DeleteCustomerButton"
    AutomationProperties.Name="Delete selected customer">
    <FontIcon Glyph="&#xE74D;" />
</Button>
```

See [Accessibility overview](../../design/accessibility/accessibility-overview.md) and [Accessibility testing](../../design/accessibility/accessibility-testing.md).

## Adapt the layout

Use `Grid` sizing and visual states to adapt the information hierarchy as the window changes. Avoid simply shrinking every element.

At narrower widths, you can:

- Move a details pane below or behind the list.
- Change `NavigationView` display mode.
- Collapse secondary metadata while keeping essential status visible.
- Move less-frequent commands into an overflow menu.

See [Responsive design](../../design/layout/responsive-design.md).

## Choose navigation and commanding

| Pattern | Control | Best for |
|---|---|---|
| App sections | `NavigationView` | A stable set of destinations or modules |
| Multiple open records or documents | `TabView` | Work that benefits from several closable contexts |
| A broad command hierarchy | `MenuBar` | Command-heavy desktop workflows |

These patterns can be combined. For example, use `NavigationView` for modules, `TabView` for open records, and `MenuBar` for commands that apply across the app. Avoid duplicating the same command in ways that make state or keyboard access inconsistent.

- [NavigationView](../../develop/ui/controls/navigationview.md)
- [TabView](../../develop/ui/controls/tab-view.md)
- [Menus and menu bars](../../develop/ui/controls/menus.md)

## Related content

- [Theming](../../develop/ui/theming.md)
- [System backdrops](../../develop/ui/system-backdrops.md)
- [Responsive design](../../design/layout/responsive-design.md)
- [Accessibility overview](../../design/accessibility/accessibility-overview.md)
