---
title: ContentIsland
description: Host non-XAML rendering content, such as Composition visuals or Direct3D surfaces, inside a WinUI 3 app using ContentIsland and ChildSiteLink.
ms.topic: how-to
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# ContentIsland

`ContentIsland` is a WinUI 3 and Windows App SDK API that lets you host non-XAML rendering content, such as Composition visuals, Win2D content, or Direct3D surfaces, inside a WinUI 3 app. Each island is an isolated rendering surface with its own input, output, layout, and accessibility state. You connect a child island to a place in the parent island by using a `ChildSiteLink`.

Use `ContentIsland` for advanced interop scenarios where you need content that isn't expressed as standard XAML elements.

> [!div class="checklist"]
>
> - **Important APIs**: [ContentIsland class](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentisland), [ChildSiteLink class](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.childsitelink), [XamlRoot.ContentIsland property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.contentisland)

> [!NOTE]
> `ContentIsland` is intended for advanced rendering scenarios. For most app UI, use standard WinUI 3 XAML controls.

## How ContentIsland works

A WinUI 3 XAML app already runs inside a `ContentIsland`. You can get that parent island from `XamlRoot.ContentIsland`. To place additional non-XAML content inside the XAML tree, you:

1. Reserve space in XAML.
2. Create a placement visual in the parent island.
3. Create a `ChildSiteLink` for that placement visual.
4. Create a child `ContentIsland` with its own root visual.
5. Connect the child island and keep its size and transform synchronized with the XAML layout.

The child island can then host Composition content, Win2D drawing, Direct3D-backed visuals, or other non-XAML rendering content.

## Reserve space in XAML

Start with a root container and a placeholder element where the child island will appear.

```xaml
<Grid x:Name="RootPanel">
    <Border x:Name="IslandHost"
            Width="320"
            Height="200"
            HorizontalAlignment="Left"
            VerticalAlignment="Top" />
</Grid>
```

## Create a child island

Create a placement visual in the parent island, then create and connect the child island.

```csharp
using System.Numerics;
using Microsoft.UI.Composition;
using Microsoft.UI.Content;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;

private ChildSiteLink? _childSiteLink;
private ContentIsland? _childIsland;
private ContainerVisual? _placementVisual;
private ContainerVisual? _childRootVisual;

private void CreateContentIsland()
{
    ContentIsland parentIsland = IslandHost.XamlRoot.ContentIsland;
    Compositor compositor = ElementCompositionPreview.GetElementVisual(RootPanel).Compositor;

    // This visual marks where the child island appears inside the parent island.
    _placementVisual = compositor.CreateContainerVisual();
    ElementCompositionPreview.SetElementChildVisual(RootPanel, _placementVisual);

    _childSiteLink = ChildSiteLink.Create(parentIsland, _placementVisual);

    // Create the root visual for the child island's own scene graph.
    _childRootVisual = compositor.CreateContainerVisual();
    _childIsland = ContentIsland.Create(_childRootVisual);

    _childSiteLink.Connect(_childIsland);

    UpdateContentIslandLayout();
}
```

`ContentIsland.Create` takes the root `Visual` for the island's scene. After the island is connected, you can add Composition content under that root visual.

## Position the island relative to a XAML element

Keep the placement visual and the `ChildSiteLink` synchronized with the placeholder element. This example maps the placeholder's bounds into the root panel, then updates both the placement visual and the link.

```csharp
using System.Numerics;
using Windows.Foundation;

private void UpdateContentIslandLayout()
{
    if (_placementVisual is null || _childSiteLink is null)
    {
        return;
    }

    GeneralTransform transform = IslandHost.TransformToVisual(RootPanel);
    Point position = transform.TransformPoint(new Point(0, 0));

    Vector2 size = new((float)IslandHost.ActualWidth, (float)IslandHost.ActualHeight);
    Vector3 offset = new((float)position.X, (float)position.Y, 0);

    _placementVisual.Offset = offset;
    _placementVisual.Size = size;

    _childSiteLink.ActualSize = size;
    _childSiteLink.LocalToParentTransformMatrix =
        Matrix4x4.CreateTranslation(offset);
}
```

## Synchronize layout changes

Subscribe to layout changes so the child island stays aligned with the XAML placeholder.

```csharp
public MainPage()
{
    this.InitializeComponent();

    // XamlRoot is null until the element is in the visual tree.
    // Defer ContentIsland creation to the Loaded event.
    IslandHost.Loaded += OnIslandHostLoaded;
    IslandHost.LayoutUpdated += OnIslandHostLayoutUpdated;
    IslandHost.Unloaded += OnIslandHostUnloaded;
}

private void OnIslandHostLoaded(object sender, RoutedEventArgs e)
{
    IslandHost.Loaded -= OnIslandHostLoaded;
    CreateContentIsland();
}

private void OnIslandHostLayoutUpdated(object sender, object e)
{
    UpdateContentIslandLayout();
}
```

If you animate or resize the placeholder, call `UpdateContentIslandLayout` whenever its effective size or position changes.

## Add content to the child island

After you create the child island, add Composition content under the child root visual.

```csharp
private void AddSpriteVisual()
{
    if (_childRootVisual is null)
    {
        return;
    }

    Compositor compositor = _childRootVisual.Compositor;

    SpriteVisual sprite = compositor.CreateSpriteVisual();
    sprite.Size = new Vector2(320, 200);
    sprite.Brush = compositor.CreateColorBrush(Microsoft.UI.Colors.DodgerBlue);

    _childRootVisual.Children.InsertAtTop(sprite);
}
```

You can replace the `SpriteVisual` with your own Win2D or Direct3D-backed visual tree.

## Clean up islands

`ContentIsland` and `ChildSiteLink` are closable resources. Close or dispose them when they're no longer needed.

```csharp
private void OnIslandHostUnloaded(object sender, RoutedEventArgs e)
{
    IslandHost.LayoutUpdated -= OnIslandHostLayoutUpdated;
    IslandHost.Unloaded -= OnIslandHostUnloaded;

    _childIsland?.Close();
    _childSiteLink?.Close();

    _childIsland = null;
    _childSiteLink = null;
    _placementVisual = null;
    _childRootVisual = null;
}
```

Handle cleanup in `Unloaded` or during window shutdown so you release rendering resources promptly.

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see ContentIsland in action](winui3gallery://item/ContentIsland)

The [WinUI 3 Gallery](https://apps.microsoft.com/detail/9P3JFPWWDZRC) app includes interactive examples of many WinUI 3 controls and platform features.

## Related articles

- [Visual layer overview](visual-layer.md)
- [Composition visual tree](composition-visual-tree.md)
- [Use the Visual layer with WinUI XAML](using-the-visual-layer-with-xaml.md)
