---
description: Learn how to use the TemplateSettings classes to provide a set of properties that define a new control template.
title: Template settings classes
ms.assetid: CAE933C6-EF13-465A-9831-AB003AF23907
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Template settings classes


## Prerequisites

We assume that you can add controls to your UI, set their properties, and attach event handlers. For instructions for adding controls to your app, see [Add controls and handle events](../design/controls-and-patterns/controls-and-events-intro.md). We also assume that you know the basics of how to define a custom template for a control by making a copy of the default template and editing it. For more info on this, see [Quickstart: Control templates](/previous-versions/windows/apps/hh465374(v=win.10)).

## The scenario for **TemplateSettings** classes

**TemplateSettings** classes provide a set of properties that are used when you define a new control template for a control. The properties have values such as pixel measurements for the size of certain UI element parts. The values are sometimes calculated values that come from control logic that isn't typically easy to override or even access. Some of the properties are intended as **From** and **To** values that control transitions and animations of parts, and thus the relevant **TemplateSettings** properties come in pairs.

There are several **TemplateSettings** classes. All of them are in the [**Windows.UI.Xaml.Controls.Primitives**](/uwp/api/Windows.UI.Xaml.Controls.Primitives) namespace. Here's a list of the classes, and a link to the **TemplateSettings** property of the relevant control. This **TemplateSettings** property is how you access the **TemplateSettings** values for the control, and can establish template bindings to its properties:

-   [**ComboBoxTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings): value of [**ComboBox.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.combobox.templatesettings)
-   [**GridViewItemTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.GridViewItemTemplateSettings): value of [**GridViewItem.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.gridviewitem.templatesettings)
-   [**ListViewItemTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ListViewItemTemplateSettings): value of [**ListViewItem.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.listviewitem.templatesettings)
-   [**ProgressBarTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings): value of [**ProgressBar.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.progressbar.templatesettings)
-   [**ProgressRingTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ProgressRingTemplateSettings): value of [**ProgressRing.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.progressring.templatesettings)
-   [**SettingsFlyoutTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings): value of [**SettingsFlyout.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.settingsflyout.templatesettings)
-   [**ToggleSwitchTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings): value of [**ToggleSwitch.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.toggleswitch.templatesettings)
-   [**ToolTipTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToolTipTemplateSettings): value of [**ToolTip.TemplateSettings**](/uwp/api/windows.ui.xaml.controls.tooltip.templatesettings)

**TemplateSettings** properties are always intended to be used in XAML, not code. They are read-only sub-properties of a read-only **TemplateSettings** property of a parent control. For an advanced custom control scenario, where you're creating a new [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control)-based class and thus can influence the control logic, consider defining a custom **TemplateSettings** property on the control in order to communicate info that might be useful for anyone that is re-templating the control. As that property's read-only value, define a new **TemplateSettings** class related to your control that has read-only properties for each of the info items that are relevant for template measurements, animation positioning, and so on, and give callers the runtime instance of that class that's initialized using your control logic. **TemplateSettings** classes are derived from [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject), so that the properties can use the dependency property system for property-changed callbacks. But the dependency property identifiers for the properties aren't exposed as public API, because the **TemplateSettings** properties are meant to be read-only to callers.

## How to use **TemplateSettings** in a control template

Here's an example that comes from the starting default XAML control templates. This particular one is from the default template of [**ProgressRing**](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing):

```xml
<Ellipse
    x:Name="E1"
    Style="{StaticResource ProgressRingEllipseStyle}"
    Width="{Binding RelativeSource={RelativeSource TemplatedParent}, 
        Path=TemplateSettings.EllipseDiameter}"
    Height="{Binding RelativeSource={RelativeSource TemplatedParent}, 
        Path=TemplateSettings.EllipseDiameter}"
    Margin="{Binding RelativeSource={RelativeSource TemplatedParent}, 
        Path=TemplateSettings.EllipseOffset}"
    Fill="{TemplateBinding Foreground}"/>
```

The full XAML for the [**ProgressRing**](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing) template is hundreds of lines, so this is just a tiny excerpt. This XAML defines a control part that is one of 6 [**Ellipse**](/uwp/api/Windows.UI.Xaml.Shapes.Ellipse) elements that portray the spinning animation for indeterminate progress. As a developer, you might not like the circles and might use a different graphics primitive or a different basic shape for how the animation progresses. For example, you might compose a **ProgressRing** that uses a set of [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) elements arranged in a square instead. If so, each individual **Rectangle** component of your new template might look like this:

```xml
<Rectangle
    x:Name="R1"
    Width="{Binding RelativeSource={RelativeSource TemplatedParent}, 
        Path=TemplateSettings.EllipseDiameter}"
    Height="{Binding RelativeSource={RelativeSource TemplatedParent}, 
        Path=TemplateSettings.EllipseDiameter}"
    Margin="{Binding RelativeSource={RelativeSource TemplatedParent}, 
        Path=TemplateSettings.EllipseOffset}"
    Fill="{TemplateBinding Foreground}"/>
```

The reason that the **TemplateSettings** properties are useful here is because they are calculated values coming from the basic control logic of [**ProgressRing**](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing). The calculation is dividing up the overall [**ActualWidth**](/uwp/api/windows.ui.xaml.frameworkelement.actualwidth) and [**ActualHeight**](/uwp/api/windows.ui.xaml.frameworkelement.actualheight) of the **ProgressRing**, and allotting a calculated measurement for each of the motion elements in its templates so that the template parts can size to content.

Here's another example usage from the default XAML control templates, this time showing one of the property sets that are the **From** and **To** of an animation. This is from the [**ComboBox**](/uwp/api/Windows.UI.Xaml.Controls.ComboBox) default template:

```xml
<VisualStateGroup x:Name="DropDownStates">
    <VisualState x:Name="Opened">
        <Storyboard>
            <SplitOpenThemeAnimation
               OpenedTargetName="PopupBorder"
               ContentTargetName="ScrollViewer"
               ClosedTargetName="ContentPresenter"
               ContentTranslationOffset="0"
               OffsetFromCenter="{Binding RelativeSource={RelativeSource TemplatedParent}, 
                 Path=TemplateSettings.DropDownOffset}"
               OpenedLength="{Binding RelativeSource={RelativeSource TemplatedParent}, 
                 Path=TemplateSettings.DropDownOpenedHeight}"
               ClosedLength="{Binding RelativeSource={RelativeSource TemplatedParent},
                 Path=TemplateSettings.DropDownClosedHeight}" />
        </Storyboard>
   </VisualState>
...
</VisualStateGroup>
```

Again there is lots of XAML in the template so we only show an excerpt. And this is only one of several states and theme animations that each use the same [**ComboBoxTemplateSettings**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings) properties. For [**ComboBox**](/uwp/api/Windows.UI.Xaml.Controls.ComboBox), use of the **ComboBoxTemplateSettings** values through bindings enforces that related animations in the template will stop and start at positions that are based on shared values, and thus transition smoothly.

**Note**  
When you do use **TemplateSettings** values as part of your control template, make sure you're setting properties that match the type of the value. If not, you might need to create a value converter for the binding so that the target type of the binding can be converted from a different source type of the **TemplateSettings** value. For more info, see [**IValueConverter**](/uwp/api/Windows.UI.Xaml.Data.IValueConverter).

## Related topics

* [Quickstart: Control templates](/previous-versions/windows/apps/hh465374(v=win.10))