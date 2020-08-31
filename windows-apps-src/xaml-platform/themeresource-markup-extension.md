---
description: Provides a value for any XAML attribute by evaluating a reference to a resource, with additional system logic that retrieves different resources depending on the currently active theme.
title: ThemeResource markup extension
ms.assetid: 8A1C79D2-9566-44AA-B8E1-CC7ADAD1BCC5
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# {ThemeResource} markup extension

Provides a value for any XAML attribute by evaluating a reference to a resource, with additional system logic that retrieves different resources depending on the currently active theme. Similar to [{StaticResource} markup extension](staticresource-markup-extension.md), resources are defined in a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary), and a **ThemeResource** usage references the key of that resource in the **ResourceDictionary**.

## XAML attribute usage

``` syntax
<object property="{ThemeResource key}" .../>
```

## XAML values

| Term | Description |
|------|-------------|
| key | The key for the requested resource. This key is initially assigned by the [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). A resource key can be any string defined in the XamlName Grammar. |

## Remarks

A **ThemeResource** is a technique for obtaining values for a XAML attribute that are defined elsewhere in a XAML resource dictionary. The markup extension serves the same basic purpose as the [{StaticResource} markup extension](staticresource-markup-extension.md). The difference in behavior versus {StaticResource} markup extension is that a **ThemeResource** reference can dynamically use different dictionaries as the primary lookup location, depending on which theme is currently being used by the system.

When the app first starts, any resource reference made by a **ThemeResource** reference is evaluated based on the theme in use at startup. But if the user subsequently changes the active theme at run-time, the system will re-evaluate every **ThemeResource** reference, retrieve a theme-specific resource that may be different, and redisplay the app with new resource values in all appropriate places in the visual tree. A **StaticResource** is determined at XAML load time / app startup and won't be re-evaluated at run-time. (There are other techniques such as visual states that reload XAML dynamically, but those techniques operate at a higher level that the basic resource evaluation enabled by [{StaticResource} markup extension](staticresource-markup-extension.md)).

**ThemeResource** takes one argument, which specifies the key for the requested resource. A resource key is always a string in Windows Runtime XAML. For more info on how the resource key is initially specified, see [x:Key attribute](x-key-attribute.md).

For more info on how to define resources and properly use a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary), including sample code, see [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md).

**Important**
As with **StaticResource**, a **ThemeResource** must not attempt to make a forward reference to a resource that is defined lexically further within the XAML file. Attempting to do so is not supported. Even if the forward reference doesn't fail, trying to make one carries a performance penalty. For best results, adjust the composition of your resource dictionaries so that forward references are avoided.

Attempting to specify a **ThemeResource** to a key that cannot resolve throws a XAML parse exception at run time. Design tools may also offer warnings or errors.

In the Windows Runtime XAML processor implementation, there is no backing class representation for **ThemeResource**. The closest equivalent in code is to use the collection API of a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary), for example calling [**Contains**](/uwp/api/windows.ui.xaml.resourcedictionary.contains) or [**TryGetValue**](/uwp/api/windows.ui.xaml.resourcedictionary.trygetvalue).

**ThemeResource** is a markup extension. Markup extensions are typically implemented when there is a requirement to escape attribute values to be other than literal values or handler names, and the requirement is more global than just putting type converters on certain types or properties. All markup extensions in XAML use the "{" and "}" characters in their attribute syntax, which is the convention by which a XAML processor recognizes that a markup extension must process the attribute.

### When and how to use {ThemeResource} rather than {StaticResource}

The rules by which a **ThemeResource** resolves to an item in a resource dictionary are generally the same as **StaticResource**. A **ThemeResource** lookup can extend into the [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary) files that are referenced in a [**ThemeDictionaries**](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) collection, but a **StaticResource** can do that also. The difference is that a **ThemeResource** can re-evaluate at run-time and a **StaticResource** can't.

The set of keys in each theme dictionary should provide the same set of keyed resources no matter which theme is active. If a given keyed resource exists in the **HighContrast** theme dictionary, then another resource with that name should also exist in **Light** and **Default**. If that isn't true, resource lookup might fail when the user switches themes and your app won't look right. It is possible though that a theme dictionary can contain keyed resources that are only referenced from within the same scope to provide sub-values; these don't need to be equivalent in all themes.

In general you should place resources in theme dictionaries and make references to those resources using **ThemeResource** only when those values can change between themes or are supported by values that change. This is appropriate for these kinds of resources:

-   Brushes, in particular colors for [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush). These make up about 80% of the **ThemeResource** usages in the default XAML control templates (generic.xaml).
-   Pixel values for borders, offsets, margin and padding and so on.
-   Font properties such as **FontFamily** or **FontSize**.
-   Complete templates for a limited number of controls that are usually system-styled and used for dynamic presentation, like [**GridViewItem**](/uwp/api/Windows.UI.Xaml.Controls.GridViewItem) and [**ListViewItem**](/uwp/api/Windows.UI.Xaml.Controls.ListViewItem).
-   Text display styles (usually to change font color, background, and possibly size).

The Windows Runtime provides a set of resources that are specifically intended to be referenced by **ThemeResource**. These are all listed as part of the XAML file themeresources.xaml, which is available in the include/winrt/xaml/design folder as part of the Windows Software Development Kit (SDK). For documentation on the theme brushes and additional styles that are defined in themeresources.xaml, see [XAML theme resources](../design/controls-and-patterns/xaml-theme-resources.md). The brushes are documented in a table that tells you what color value each brush has for each of the three possible active themes.

The XAML definitions of visual states in a control template should use **ThemeResource** references whenever there's an underlying resource that might change because of a theme change. A system theme change won't typically also cause a visual state change. The resources need to use **ThemeResource** references in this case so that values can be re-evaluated for the still-active visual state. For example, if you have a visual state that changes a brush color of a particular UI part and one of its properties, and that brush color is different per-theme, you should use a **ThemeResource** reference for providing that property's value in the default template and also any visual state modification to that default template.

**ThemeResource** usages might be seen in a series of dependent values. For example, a [**Color**](/uwp/api/Windows.UI.Color) value used by a [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush) that is also a keyed resource might use a **ThemeResource** reference. But any UI properties that use the keyed **SolidColorBrush** resource would also use a **ThemeResource** reference, so that it's specifically each [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush)-type property that's enabling a dynamic value change when the theme changes.

**Note**  `{ThemeResource}` and run-time resource evaluation on theme switching is supported in Windows 8.1 XAML but not supported in XAML for apps targeting Windows 8.

### System resources

Some theme resources reference system resource values as an underlying sub-value. A system resource is a special resource value that isn't found in any XAML resource dictionary. These values rely on behavior in Windows Runtime XAML support to forward values from the system itself, and represent them in a form that a XAML resource can reference. For example, there is a system resource named "SystemColorButtonFaceColor" that represents an RGB color. This color comes from the aspects of system colors and themes that aren't just specific to Windows Runtime and Windows Runtime apps.

System resources are often the underlying values for a high-contrast theme. The user is in control of the color choices for their high-contrast theme, and the user makes these choices using system features that also aren't specific to Windows Runtime apps. By referencing the system resources as **ThemeResource** references, the default behavior of the high-contrast themes for Windows Runtime apps can use these theme-specific values that are controlled by the user and exposed by the system. Also, the references are now marked for re-evaluation if the system detects a run-time theme change.

### An example {ThemeResource} usage

Here's some example XAML taken from the default generic.xaml and themeresources.xaml files to illustrate how to use **ThemeResource**. We'll look at just one template (the default [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button)) and how two properties are declared ([**Background**](/uwp/api/windows.ui.xaml.controls.control.background) and [**Foreground**](/uwp/api/windows.ui.xaml.controls.control.foreground)) to be responsive to theme changes.

```xml
    <!-- Default style for Windows.UI.Xaml.Controls.Button -->
    <Style TargetType="Button">
        <Setter Property="Background" Value="{ThemeResource ButtonBackgroundThemeBrush}" />
        <Setter Property="Foreground" Value="{ThemeResource ButtonForegroundThemeBrush}"/>
...
```

Here, the properties take a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) value, and the reference to [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush) resources named `ButtonBackgroundThemeBrush` and `ButtonForegroundThemeBrush` are made using **ThemeResource**.

These same properties are also adjusted by some of the visual states for a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button). Notably, the background color changes when a button is clicked. Here too, the [**Background**](/uwp/api/windows.ui.xaml.controls.control.background) and [**Foreground**](/uwp/api/windows.ui.xaml.controls.control.foreground) animations in the visual state storyboard use [**DiscreteObjectKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame) objects and references to brushes with **ThemeResource** as the key frame value.

```xml
<VisualState x:Name="Pressed">
  <Storyboard>
    <ObjectAnimationUsingKeyFrames Storyboard.TargetName="Border"
        Storyboard.TargetProperty="Background">
      <DiscreteObjectKeyFrame KeyTime="0" Value="{ThemeResource ButtonPressedBackgroundThemeBrush}" />
    </ObjectAnimationUsingKeyFrames>
    <ObjectAnimationUsingKeyFrames Storyboard.TargetName="ContentPresenter"
         Storyboard.TargetProperty="Foreground">
       <DiscreteObjectKeyFrame KeyTime="0" Value="{ThemeResource ButtonPressedForegroundThemeBrush}" />
    </ObjectAnimationUsingKeyFrames>
  </Storyboard>
</VisualState>
```

Each of these brushes is defined earlier in generic.xaml: they had to be defined prior to any templates using them to avoid XAML forward references. Here's those definitions, for the "Default" theme dictionary.

```xml
    <ResourceDictionary.ThemeDictionaries>
        <ResourceDictionary x:Key="Default">
...
            <SolidColorBrush x:Key="ButtonBackgroundThemeBrush" Color="Transparent" />
            <SolidColorBrush x:Key="ButtonForegroundThemeBrush" Color="#FFFFFFFF" />
...
            <SolidColorBrush x:Key="ButtonPressedBackgroundThemeBrush" Color="#FFFFFFFF" />
            <SolidColorBrush x:Key="ButtonPressedForegroundThemeBrush" Color="#FF000000" />
...
```

Then each of the other theme dictionaries also has these brushes defined, for example:

```xml
        <ResourceDictionary x:Key="HighContrast">
            <!-- High Contrast theme resources -->
...
            <SolidColorBrush x:Key="ButtonBackgroundThemeBrush" Color="{ThemeResource SystemColorButtonFaceColor}" />
            <SolidColorBrush x:Key="ButtonForegroundThemeBrush" Color="{ThemeResource SystemColorButtonTextColor}" />

...
            <SolidColorBrush x:Key="ButtonPressedBackgroundThemeBrush" Color="{ThemeResource SystemColorButtonTextColor}" />
            <SolidColorBrush x:Key="ButtonPressedForegroundThemeBrush" Color="{ThemeResource SystemColorButtonFaceColor}" />
```

Here the [**Color**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush.Color) value is another **ThemeResource** reference to a system resource. If you reference a system resource, and you want it to change in response to a theme change, you should use **ThemeResource** to make the reference.

## Windows 8 behavior

Windows 8 did not support the **ThemeResource** markup extension, it is available starting with Windows 8.1. Also, Windows 8 did not support dynamically switching the theme-related resources for a Windows Runtime app. The app had to be restarted in order to pick up the theme change for the XAML templates and styles. This isn't a good user experience, so apps are strongly encouraged to recompile and target Windows 8.1 so that they can use styles with **ThemeResource** usages and can dynamically switch themes when the user does. Apps that were compiled for Windows 8 but running on Windows 8.1 continue to use the Windows 8 behavior.

## Design-time tools support for the **{ThemeResource}** markup extension

Microsoft Visual Studio 2013 can include possible key values in the Microsoft IntelliSense dropdowns when you use the **{ThemeResource}** markup extension in a XAML page. For example, as soon as you type "{ThemeResource", any of the resource keys from the [XAML theme resources](../design/controls-and-patterns/xaml-theme-resources.md) are displayed.

Once a resource key exists as part of any **{ThemeResource}** usage, the **Go To Definition** (F12) feature can resolve that resource and show you the generic.xaml for design time, where the theme resource is defined. Because theme resources are defined more than once (per-theme) **Go To Definition** takes you to the first definition found in the file, which is the definition for **Default**. If you want the other definitions you can search for the key name within the file and find the other themes' definitions.

## Related topics

* [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md)
* [XAML theme resources](../design/controls-and-patterns/xaml-theme-resources.md)
* [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary)
* [x:Key attribute](x-key-attribute.md)
 