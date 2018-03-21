---
author: jebishop
ms.assetid: fb8ae71d-5c88-4c85-9257-a9607d5179b1
title: lighting
description: Light objects are used in conjunction with SceneLightingEffect to simulate dynamic lighting and reflectivity.
ms.author: jimwalk
ms.date: 03/29/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Lighting

[**CompositionLight**](https://docs.microsoft.com/uwp/api/Windows.UI.Composition.CompositionLight) objects are used in conjunction with [**SceneLightingEffect**](https://docs.microsoft.com/uwp/api/Windows.UI.Composition.Effects.SceneLightingEffect) to simulate dynamic lighting and reflectivity.

You can apply lights to [**Visuals**](https://msdn.microsoft.com/library/windows/apps/Dn706858) and XAML [**UIElements**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.UIElement).

## Applying lights to XAML UIElements

[**XamlLight**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.xamllight) objects are used to apply [**CompositionLights**](https://docs.microsoft.com/uwp/api/Windows.UI.Composition.CompositionLight) to dynamically light XAML UIElements. XamlLight provides methods for targeting UIElements or other XAML Brushes, applying lights to trees of UIElements, and helping manage the lifetime of CompositionLight resources based on whether they're currently in use.

* If you target a **Brush** with a XamlLight then the portions of any UIElements using that Brush are lit by the light.
* If you target a **UIElement** with a XamlLight then the entire UIElement and its child UIElements are all lit by the light.

## Creating and using a XamlLight

[**XamlLight**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.xamllight) is a base class which can be used to create custom lights.

Here is an example of a custom XamlLight that uses an attached property to target specific elements:

```csharp
public sealed class FancyOrangeSpotLight : XamlLight
{
    // Register an attached proeprty that enables apps to set a UIElement or Brush as a target for this light type in markup.
    public static readonly DependencyProperty IsTargetProperty =
        DependencyProperty.RegisterAttached(
        "IsTarget",
        typeof(Boolean),
        typeof(FancyOrangeSpotLight),
        new PropertyMetadata(null, OnIsTargetChanged)
    );
    public static void SetIsTarget(DependencyObject target, Boolean value)
    {
        target.SetValue(IsTargetProperty, value);
    }
    public static Boolean GetIsTarget(DependencyObject target)
    {
        return (Boolean) target.GetValue(IsTargetProperty);
    }

    // Handle attached property changed to automatically target and untarget UIElements and Brushes.
    private static void OnIsTargetChanged(DependencyObject obj,
                                            DependencyPropertyChangedEventArgs e)
    {
        var isAdding = (Boolean)e.NewValue;

        if (isAdding)
        {
            if (obj is UIElement)
            {
                XamlLight.AddTargetElement(GetIdStatic(), obj as UIElement);
            }
            else if (obj is Brush)
            {
                XamlLight.AddTargetBrush(GetIdStatic(), obj as Brush);
            }
        }
        else
        {
            if (obj is UIElement)
            {
                XamlLight.RemoveTargetElement(GetIdStatic(), obj as UIElement);
            }
            else if (obj is Brush)
            {
                XamlLight.RemoveTargetBrush(GetIdStatic(), obj as Brush);
            }
        }
    }

    protected override void OnConnected(UIElement newElement)
    {
        // OnConnected is called when the first target UIElement is shown on the screen. This enables delaying composition object creation until it's actually necessary.
        var spotLight = Window.Current.Compositor.CreateSpotLight();
        spotLight.InnerConeColor = Colors.Orange;
        spotLight.OuterConeColor = Colors.Yellow;
        spotLight.InnerConeAngleInDegrees = 30;
        spotLight.OuterConeAngleInDegrees = 45;
        CompositionLight = spotLight;
    }

    protected override void OnDisconnected(UIElement oldElement)
    {
        // OnDisconnected is called when there are no more target UIElements on the screen. The CompositionLight should be disposed when no longer required.
        CompositionLight.Dispose();
        CompositionLight = null;
    }

    protected override string GetId()
    {
        return GetIdStatic();
    }

    private static string GetIdStatic()
    {
        // This specifies the unique name of the light. In most cases you should use the type's FullName.
        return typeof(FancyPointerTrackerLight).FullName;
    }
}
```

And here is an example showing different possible uses of the custom light defined above:

```xml
<Page>
    <Page.Lights>
        <local:SimpleOrangeSpotLight />
    </Page.Lights>

    <StackPanel>
        <!-- this border will be lit by a FancyOrangeSpotLight, but not its children -->
        <Border BorderThickness="1">
            <Border.BorderBrush>
                <SolidColorBrush Color="Orange" local:FancyOrangeSpotLight.IsTarget="true" />
            </Border.BorderBrush>
            <TextBlock Text="hello world" />
        </Border>

        <!-- this border and its children will be lit by FancyOrangeSpotLight -->
        <Border BorderThickness="2" local:FancyOrangeSpotLight.IsTarget="true">
            <Border.BorderBrush>
                <SolidColorBrush Color="Purple" />
            </Border.BorderBrush>
            <TextBlock Text="hello world" />
        </Border>

        <!-- this border will not be lit -->
        <Border BorderThickness="2">
            <Border.BorderBrush>
                <SolidColorBrush Color="Green" />
            </Border.BorderBrush>
            <TextBlock Text="hello world" />
        </Border>
    </StackPanel>
<Page>
```

> [!Important]
> Setting UIElement.Lights in markup as shown in the above example is only supported for apps with a Minimum Version equal to the Windows 10 Creators Update or later. For apps that target earlier versions, lights must be created in code-behind.

## Additional Resources

* Advanced UI and Composition samples in the [WindowsUIDevLabs GitHub](https://github.com/microsoft/windowsuidevlabs).
