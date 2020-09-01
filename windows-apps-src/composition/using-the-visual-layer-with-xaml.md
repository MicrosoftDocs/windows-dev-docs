---
ms.assetid: b7a4ac8a-d91e-461b-a060-cc6fcea8e778
title: Using the Visual Layer with XAML
description: Learn techniques for using the Visual Layer API's in combination with existing XAML content to create advanced animations and effects.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Using the Visual Layer with XAML

Most apps that consume Visual Layer capabilities will use XAML to define the main UI content. In the Windows 10 Anniversary Update, there are new features in the XAML framework and the Visual Layer that make it easier to combine these two technologies to create stunning user experiences.
XAML and Visual Layer interop functionality can be used to create advanced animations and effects not available using XAML APIs alone. This includes:

- Brush effects like blur and frosted glass
- Dynamic lighting effects
- Scroll driven animations and parallax
- Automatic layout animations
- Pixel-perfect drop shadows

These effects and animations can be applied to existing XAML content, so you don't have to dramatically restructure your XAML app to take advantage of the new functionality.
Layout animations, shadows, and blur effects are covered in the Recipes section below. For a code sample implementing parallax, see the [ParallaxingListItems sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/SampleGallery/Samples/SDK%2010586/ParallaxingListItems). The [WindowsUIDevLabs repository](https://github.com/microsoft/WindowsCompositionSamples) also has several other samples for implementing animations, shadows and effects.

## The XamlCompositionBrushBase class

**XamlCompositionBrush** provides a base class for XAML brushes that paint an area with a **CompositionBrush**. This can be used to easily apply composition effects like blur or frosted glass to XAML UI elements.

See the [**Brushes**](../design/style/brushes.md#xamlcompositionbrushbase) section for more info on using brushes with XAML UI.

For code examples, see the reference page for [**XamlCompositionBrushBase**](/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase).

## The XamlLight class

**XamlLight** provides a base class for XAML lighting effects that dynamically light an area with a **CompositionLight**.

See the [**Lighting**](xaml-lighting.md) section for more info on using lights, including lighting XAML UI elements.

For code examples, see the reference page for [**XamlLight**](/uwp/api/windows.ui.xaml.media.xamllight).

## The ElementCompositionPreview class

[**ElementCompositionPreview**](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview) is a static class that provides XAML and Visual Layer interop functionality. For an overview of the Visual Layer and its functionality, see [Visual Layer](./visual-layer.md). The **ElementCompositionPreview** class provides the following methods:

-   [**GetElementVisual**](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.getelementvisual): Get a "handout" Visual that is used to render this element
-   [**SetElementChildVisual**](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.setelementchildvisual): Sets a "handin" Visual as the last child of this element’s visual tree. This Visual will draw on top of the rest of the element. 
-   [**GetElementChildVisual**](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.getelementvisual): Retrieve the Visual set using **SetElementChildVisual**
-   [**GetScrollViewerManipulationPropertySet**](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.getelementvisual): Get an object that can be used to create 60fps animations based on scroll offset in a **ScrollViewer**

## Remarks on ElementCompositionPreview.GetElementVisual

**ElementCompositionPreview.GetElementVisual** returns a “handout” Visual that is used to render the given **UIElement**. Properties such as **Visual.Opacity**, **Visual.Offset**, and **Visual.Size** are set by the XAML framework based on the state of the UIElement. This enables techniques such as implicit reposition animations (see *Recipes*).

Note that since **Offset** and **Size** are set as the result of XAML framework layout, developers should be careful when modifying or animating these properties. Developers should only modify or animate Offset when the element’s top-left corner has the same position as that of its parent in layout. Size should generally not be modified, but accessing the property may be useful. For example, the Drop Shadow and Frosted Glass samples below use Size of a handout Visual as input to an animation.

As an additional caveat, updated properties of the handout Visual will not be reflected in the corresponding UIElement. So for example, setting **UIElement.Opacity** to 0.5 will set the corresponding handout Visual’s Opacity to 0.5. However, setting the handout Visual’s **Opacity** to 0.5 will cause the content to appear at 50% opacity, but will not change the value of the corresponding UIElement’s Opacity property.

### Example of **Offset** animation

#### Incorrect

```xaml
<Border>
      <Image x:Name="MyImage" Margin="5" />
</Border>
```

```csharp
// Doesn’t work because Image has a margin!
ElementCompositionPreview.GetElementVisual(MyImage).StartAnimation("Offset", parallaxAnimation);
```

#### Correct

```xaml
<Border>
    <Canvas Margin="5">
        <Image x:Name="MyImage" />
    </Canvas>
</Border>
```

```csharp
// This works because the Canvas parent doesn’t generate a layout offset.
ElementCompositionPreview.GetElementVisual(MyImage).StartAnimation("Offset", parallaxAnimation);
```

## The **ElementCompositionPreview.SetElementChildVisual** method

**ElementCompositionPreview.SetElementChildVisual** allows the developer to supply a “handin” Visual that will appear as part of an element’s Visual Tree. This allows developers to create a “Composition Island” where Visual-based content can appear inside a XAML UI. Developers should be conservative about using this technique because Visual-based content will not have the same accessibility and user experience guarantees of XAML content. Therefore, it is generally recommended that this technique only be used when necessary to implement custom effects such as those found in the Recipes section below.

## **GetAlphaMask** methods

[**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image), [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock), and [**Shape**](/uwp/api/Windows.UI.Xaml.Shapes.Shape) each implement a method called **GetAlphaMask** that returns a **CompositionBrush** representing a grayscale image with the shape of the element. This **CompositionBrush** can serve as an input for a Composition **DropShadow**, so the shadow can reflect the shape of the element instead of a rectangle. This enables pixel perfect, contour-based shadows for text, images with alpha, and shapes. See *Drop Shadow* below for an example of this API.

## Recipes

### Reposition animation

Using Composition Implicit Animations, a developer can automatically animate changes in an element’s layout relative to its parent. For example, if you change the **Margin** of the button below, it will automatically animate to its new layout position.

#### Implementation overview

1. Get the handout **Visual** for the target element
1. Create an **ImplicitAnimationCollection** that automatically animates changes in the **Offset** property
1. Associate the **ImplicitAnimationCollection** with the backing Visual

```xaml
<Button x:Name="RepositionTarget" Content="Click Me" />
```

```csharp
public MainPage()
{
    InitializeComponent();
    InitializeRepositionAnimation(RepositionTarget);
}

private void InitializeRepositionAnimation(UIElement repositionTarget)
{
    var targetVisual = ElementCompositionPreview.GetElementVisual(repositionTarget);
    Compositor compositor = targetVisual.Compositor;

    // Create an animation to animate targetVisual's Offset property to its final value
    var repositionAnimation = compositor.CreateVector3KeyFrameAnimation();
    repositionAnimation.Duration = TimeSpan.FromSeconds(0.66);
    repositionAnimation.Target = "Offset";
    repositionAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");

    // Run this animation when the Offset Property is changed
    var repositionAnimations = compositor.CreateImplicitAnimationCollection();
    repositionAnimations["Offset"] = repositionAnimation;

    targetVisual.ImplicitAnimations = repositionAnimations;
}
```

### Drop shadow

Apply a pixel-perfect drop shadow to a **UIElement**, for example an **Ellipse** containing a picture. Since the shadow requires a **SpriteVisual** created by the app, we need to create a “host” element which will contain the **SpriteVisual** using **ElementCompositionPreview.SetElementChildVisual**.

#### Implementation overview

1. Get the handout **Visual** for the host element
2. Create a Windows.UI.Composition **DropShadow**
3. Configure the **DropShadow** to get its shape from the target element via a mask
    - **DropShadow** is rectangular by default, so this is not necessary if the target is rectangular
4. Attach shadow to a new **SpriteVisual**, and set the **SpriteVisual** as the child of the host element
5. Bind size of the **SpriteVisual** to the size of the host using an **ExpressionAnimation**

```xaml
<Grid Width="200" Height="200">
    <Canvas x:Name="ShadowHost" />
    <Ellipse x:Name="CircleImage">
        <Ellipse.Fill>
            <ImageBrush ImageSource="Assets/Images/2.jpg" Stretch="UniformToFill" />
        </Ellipse.Fill>
    </Ellipse>
</Grid>
```

```csharp
public MainPage()
{
    InitializeComponent();
    InitializeDropShadow(ShadowHost, CircleImage);
}

private void InitializeDropShadow(UIElement shadowHost, Shape shadowTarget)
{
    Visual hostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);
    Compositor compositor = hostVisual.Compositor;

    // Create a drop shadow
    var dropShadow = compositor.CreateDropShadow();
    dropShadow.Color = Color.FromArgb(255, 75, 75, 80);
    dropShadow.BlurRadius = 15.0f;
    dropShadow.Offset = new Vector3(2.5f, 2.5f, 0.0f);
    // Associate the shape of the shadow with the shape of the target element
    dropShadow.Mask = shadowTarget.GetAlphaMask();

    // Create a Visual to hold the shadow
    var shadowVisual = compositor.CreateSpriteVisual();
    shadowVisual.Shadow = dropShadow;

    // Add the shadow as a child of the host in the visual tree
   ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

    // Make sure size of shadow host and shadow visual always stay in sync
    var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
    bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

    shadowVisual.StartAnimation("Size", bindSizeAnimation);
}
```

The following two listings show the [C++/WinRT](../cpp-and-winrt-apis/index.md) and [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) equivalents of the previous C&#35; code using the same XAML structure.

```cppwinrt
#include <winrt/Windows.UI.Composition.h>
#include <winrt/Windows.UI.Xaml.h>
#include <winrt/Windows.UI.Xaml.Hosting.h>
#include <winrt/Windows.UI.Xaml.Shapes.h>
...
MainPage()
{
    InitializeComponent();
    InitializeDropShadow(ShadowHost(), CircleImage());
}

int32_t MyProperty();
void MyProperty(int32_t value);

void InitializeDropShadow(Windows::UI::Xaml::UIElement const& shadowHost, Windows::UI::Xaml::Shapes::Shape const& shadowTarget)
{
    auto hostVisual{ Windows::UI::Xaml::Hosting::ElementCompositionPreview::GetElementVisual(shadowHost) };
    auto compositor{ hostVisual.Compositor() };

    // Create a drop shadow
    auto dropShadow{ compositor.CreateDropShadow() };
    dropShadow.Color(Windows::UI::ColorHelper::FromArgb(255, 75, 75, 80));
    dropShadow.BlurRadius(15.0f);
    dropShadow.Offset(Windows::Foundation::Numerics::float3{ 2.5f, 2.5f, 0.0f });
    // Associate the shape of the shadow with the shape of the target element
    dropShadow.Mask(shadowTarget.GetAlphaMask());

    // Create a Visual to hold the shadow
    auto shadowVisual = compositor.CreateSpriteVisual();
    shadowVisual.Shadow(dropShadow);

    // Add the shadow as a child of the host in the visual tree
    Windows::UI::Xaml::Hosting::ElementCompositionPreview::SetElementChildVisual(shadowHost, shadowVisual);

    // Make sure size of shadow host and shadow visual always stay in sync
    auto bindSizeAnimation{ compositor.CreateExpressionAnimation(L"hostVisual.Size") };
    bindSizeAnimation.SetReferenceParameter(L"hostVisual", hostVisual);

    shadowVisual.StartAnimation(L"Size", bindSizeAnimation);
}
```

```cpp
#include "WindowsNumerics.h"

MainPage::MainPage()
{
    InitializeComponent();
    InitializeDropShadow(ShadowHost, CircleImage);
}

void MainPage::InitializeDropShadow(Windows::UI::Xaml::UIElement^ shadowHost, Windows::UI::Xaml::Shapes::Shape^ shadowTarget)
{
    auto hostVisual = Windows::UI::Xaml::Hosting::ElementCompositionPreview::GetElementVisual(shadowHost);
    auto compositor = hostVisual->Compositor;

    // Create a drop shadow
    auto dropShadow = compositor->CreateDropShadow();
    dropShadow->Color = Windows::UI::ColorHelper::FromArgb(255, 75, 75, 80);
    dropShadow->BlurRadius = 15.0f;
    dropShadow->Offset = Windows::Foundation::Numerics::float3(2.5f, 2.5f, 0.0f);
    // Associate the shape of the shadow with the shape of the target element
    dropShadow->Mask = shadowTarget->GetAlphaMask();

    // Create a Visual to hold the shadow
    auto shadowVisual = compositor->CreateSpriteVisual();
    shadowVisual->Shadow = dropShadow;

    // Add the shadow as a child of the host in the visual tree
    Windows::UI::Xaml::Hosting::ElementCompositionPreview::SetElementChildVisual(shadowHost, shadowVisual);

    // Make sure size of shadow host and shadow visual always stay in sync
    auto bindSizeAnimation = compositor->CreateExpressionAnimation("hostVisual.Size");
    bindSizeAnimation->SetReferenceParameter("hostVisual", hostVisual);

    shadowVisual->StartAnimation("Size", bindSizeAnimation);
}
```

### Frosted glass

Create an effect that blurs and tints background content. Note that developers need to install the Win2D NuGet package to use effects. See the [Win2D homepage](https://microsoft.github.io/Win2D/html/Introduction.htm) for installation instructions.

#### Implementation overview

1.  Get handout **Visual** for the host element
2.  Create a blur effect tree using Win2D and **CompositionEffectSourceParameter**
3.  Create a **CompositionEffectBrush** based on the effect tree
4.  Set input of the **CompositionEffectBrush** to a **CompositionBackdropBrush**, which allows an effect to be applied to the content behind a **SpriteVisual**
5.  Set the **CompositionEffectBrush** as the content of a new **SpriteVisual**, and set the **SpriteVisual** as the child of the host element. You could alternative use a XamlCompositionBrushBase.
6.  Bind size of the **SpriteVisual** to the size of the host using an **ExpressionAnimation**

```xaml
<Grid Width="300" Height="300" Grid.Column="1">
    <Image
        Source="Assets/Images/2.jpg"
        Width="200"
        Height="200" />
    <Canvas
        x:Name="GlassHost"
        Width="150"
        Height="300"
        HorizontalAlignment="Right" />
</Grid>
```

```csharp
public MainPage()
{
    InitializeComponent();
    InitializeFrostedGlass(GlassHost);
}

private void InitializeFrostedGlass(UIElement glassHost)
{
    Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
    Compositor compositor = hostVisual.Compositor;

    // Create a glass effect, requires Win2D NuGet package
    var glassEffect = new GaussianBlurEffect
    { 
        BlurAmount = 15.0f,
        BorderMode = EffectBorderMode.Hard,
        Source = new ArithmeticCompositeEffect
        {
            MultiplyAmount = 0,
            Source1Amount = 0.5f,
            Source2Amount = 0.5f,
            Source1 = new CompositionEffectSourceParameter("backdropBrush"),
            Source2 = new ColorSourceEffect
            {
                Color = Color.FromArgb(255, 245, 245, 245)
            }
        }
    };

    //  Create an instance of the effect and set its source to a CompositionBackdropBrush
    var effectFactory = compositor.CreateEffectFactory(glassEffect);
    var backdropBrush = compositor.CreateBackdropBrush();
    var effectBrush = effectFactory.CreateBrush();

    effectBrush.SetSourceParameter("backdropBrush", backdropBrush);

    // Create a Visual to contain the frosted glass effect
    var glassVisual = compositor.CreateSpriteVisual();
    glassVisual.Brush = effectBrush;

    // Add the blur as a child of the host in the visual tree
    ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);

    // Make sure size of glass host and glass visual always stay in sync
    var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
    bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

    glassVisual.StartAnimation("Size", bindSizeAnimation);
}
```

## Additional Resources

- [Visual Layer overview](./visual-layer.md)
- [**ElementCompositionPreview** class](/uwp/api/Windows.UI.Xaml.Hosting.ElementCompositionPreview)
- Advanced UI and Composition samples in the [WindowsUIDevLabs GitHub](https://github.com/microsoft/WindowsCompositionSamples)
- [BasicXamlInterop sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/SampleGallery/Samples/SDK%2010586/BasicXamlInterop)
- [ParallaxingListItems sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/SampleGallery/Samples/SDK%2010586/ParallaxingListItems)